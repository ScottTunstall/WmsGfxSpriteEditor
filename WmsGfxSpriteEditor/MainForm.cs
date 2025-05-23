using System.Text;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.ROMs;
using WmsGfxSpriteEditor.ROMs.Robotron;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private IRomService? _romService;

        private ISpriteGridRenderer? _spriteRenderer;
        private ISpriteFactory? _spriteFactory;
        private ISpriteClipboardService? _clipboardService = new DefaultSpriteClipboardService();
        private ISprite? _sprite;

        private string _romSetName;
        private RomData? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);

        // User selections
        private Color _selectedColour = Color.Black;

        private IReadOnlyList<SpriteInfo> _allSprites;
        private bool _haveSpritesToSelect;
        private int _selectedPaletteIndex;
        private SpriteInfo? _selectedSpriteInfo;
        private UInt128 _spriteHash;
        private int _selectedSpriteIndex;
        private int _zoomLevel = 3; // Default zoom for the normal view

        private bool _suspendChangeEvents;
        private readonly History.History _history = new();

        // Palette
        private Color[] _palette = default!;

        public MainForm()
        {
            InitializeComponent();

            _suspendChangeEvents = true;

            DisableEditingControls();

            nudZoom.Value = _zoomLevel;

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;

            _suspendChangeEvents = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pnlPalette.Invalidate();
        }

        #region FILE MENU EVENT HANDLERS

        private void mnuFileLoadRobotronBlueLabel_Click(object sender, EventArgs e)
        {
            BrowseForRobotronRom(RomSetNames.BlueLabel, RomSetType.BlueLabel);
        }

        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            BrowseForRobotronRom(RomSetNames.BlueLabel, RomSetType.TieDieWDPU);
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            SaveRomData();
        }

        #endregion FILE MENU EVENT HANDLERS

        #region EDIT MENU EVENT HANDLERS

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            CopySpriteToClipboard();
        }

        #endregion EDIT MENU EVENT HANDLERS

        #region VIEW MENU EVENT HANDLERS

        private void mnuViewZoomIn_Click(object sender, EventArgs e)
        {
            if (_zoomLevel <= nudZoom.Maximum)
            {
                SetZoom(_zoomLevel + 1);
            }
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            if (_zoomLevel >= nudZoom.Minimum)
            {
                SetZoom(_zoomLevel - 1);
            }
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SetZoom((int)nudZoom.Value);
        }

        #endregion VIEW MENU EVENT HANDLERS

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SelectSpriteByIndex(cboSprite.SelectedIndex);
        }

        #endregion SPRITE COMBO BOX EVENT HANDLERS

        #region PALETTE CONTROL EVENT HANDLERS

        private void PnlPalette_ColorSelected(object? sender, ColourSelectedEventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SelectPalette(e.SelectedColour, e.ColourIndex);
        }

        #endregion PALETTE CONTROL EVENT HANDLERS

        #region SPRITE GRID EVENT HANDLERS

        private void SpriteDisplay_GridCellMouseMove(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _sprite!.SetPixelByPaletteIndex(e.GridCell.X, e.GridCell.Y, _selectedPaletteIndex);
                spriteDisplay.Invalidate();
            }

            // Convert zero based coordinates to 1 based
            CoordinatesLabel.Text = $"X: {e.GridCell.X+1} Y: {e.GridCell.Y+1}";
        }

        private void spriteDisplay_GridCellMouseDown(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            UInt128 spriteHash = _sprite!.GetPixelDataHash();
            if (_spriteHash != spriteHash)
            {
                _history.Add(HistoryItem.CreateBeforeSpritePixelDataChangedHistoryItem(_sprite!.ClonePixelData(), spriteHash, _selectedSpriteIndex));
                _spriteHash = _sprite.GetPixelDataHash();
            }

            _sprite!.SetPixelByPaletteIndex(e.GridCell.X, e.GridCell.Y, _selectedPaletteIndex);

            spriteDisplay.Invalidate();
            OnDisplayStateChanged();
        }

        private void spriteDisplay_GridCellMouseUp(object sender, SpriteGridMouseEventArgs e)
        {
            UInt128 spriteHash = _sprite!.GetPixelDataHash();
            if (_spriteHash != spriteHash)
            {
                _history.Add(HistoryItem.CreateAfterSpritePixelDataChangedHistoryItem(_sprite!.ClonePixelData(), spriteHash, _selectedSpriteIndex));
                _spriteHash = spriteHash;
            }

            OnDisplayStateChanged();
        }

        #endregion SPRITE GRID EVENT HANDLERS

        #region PALETTE FUNCS

        private void SelectPalette(Color selectedColour, int colourIndex)
        {
            _selectedColour = selectedColour;
            _selectedPaletteIndex = colourIndex;
            OnDisplayStateChanged();
        }

        #endregion PALETTE FUNCS

        #region EDIT FUNCS

        private void Undo()
        {
            HistoryItem? item = _history.Back();
            if (item == null)
            {
                throw new InvalidOperationException("No history item to undo.");
            }

            SetStateFromHistory(item);
            OnDisplayStateChanged();
        }

        private void Redo()
        {
            HistoryItem item = _history.Forward()!;
            if (item == null)
            {
                throw new InvalidOperationException("No history item to redo.");
            }

            SetStateFromHistory(item!);
            OnDisplayStateChanged();
        }

        private void CopySpriteToClipboard()
        {
            _clipboardService!.Copy(_sprite!, _palette);
        }

        #endregion EDIT FUNCS

        #region VIEW FUNCS

        private void SetZoom(int newZoomLevel)
        {
            _zoomLevel = newZoomLevel;
            _suspendChangeEvents = true;
            nudZoom.Value = newZoomLevel;
            spriteDisplay.ZoomLevel = newZoomLevel;
            _suspendChangeEvents = false;

            OnDisplayStateChanged();
        }

        #endregion VIEW FUNCS

        #region SPRITE FUNCS

        private SpriteInfo SetSpriteSelectDropdown(List<SpriteInfo> spriteInfos, int index = 0)
        {
            _allSprites = spriteInfos;
            _haveSpritesToSelect = spriteInfos.Count > 0;

            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = spriteInfos;

            cboSprite.SelectedIndex = index;
            _selectedSpriteIndex = index;
            SpriteInfo spriteInfo = spriteInfos[index]!;
            return spriteInfo;
        }

        private void SelectSpriteByIndex(int spriteIndex)
        {
            _selectedSpriteIndex = spriteIndex;
            _selectedSpriteInfo = _allSprites[spriteIndex]!;

            if (_selectedSpriteInfo != null)
            {
                SetSpriteDisplay(CreateSpriteFromRomData(_romData!, _selectedSpriteInfo!));
            }
            else
            {
                SetSpriteDisplay(null);
            }

            UpdateStatusBarWithSpriteInfo(_selectedSpriteInfo);
            OnDisplayStateChanged();
        }
        

        private void SetSpriteDisplay(ISprite? sprite)
        {
            _sprite = sprite;
            _spriteHash = 0;
            spriteDisplay.Sprite = sprite;
            spriteDisplay.Invalidate();
            OnDisplayStateChanged();
        }

        #endregion SPRITE FUNCS

        /// <summary>
        /// Updates the status bar with complete sprite information
        /// </summary>
        private void UpdateStatusBarWithSpriteInfo(SpriteInfo? spriteInfo)
        {
            if (spriteInfo == null)
            {
                StatusLabel.Text = "No sprite selected.";
            }
            else
            {
                // Include the sprite offset in both hex and decimal format
                StatusLabel.Text = $"Sprite: {spriteInfo.Name} | Offset: 0x{spriteInfo.Offset:X4} ({spriteInfo.Offset}) | " +
                                   $"Size: {spriteInfo.WidthInPixels}x{spriteInfo.Height} pixels " +
                                   $"({spriteInfo.WidthInBytes} bytes x {spriteInfo.Height} rows) | " +
                                   $"Format: {(spriteInfo.IsLinear ? "Linear" : "Non-linear")}";
            }
        }

        protected virtual void OnDisplayStateChanged()
        {
            mnuEditUndo.Enabled = _sprite != null && _history.CanGoBack;
            mnuEditRedo.Enabled = _sprite != null && _history.CanGoForward;
            mnuViewZoomIn.Enabled = _sprite != null && _zoomLevel < nudZoom.Maximum;
            mnuViewZoomOut.Enabled = _sprite != null && _zoomLevel > nudZoom.Minimum;

            cboSprite.Enabled = _haveSpritesToSelect;
            nudZoom.Enabled = _sprite != null;
            pnlPalette.Enabled = _sprite != null;
            spriteDisplay.Visible = _sprite != null;
        }

        #region HISTORY

        private void SetStateFromHistory(HistoryItem item)
        {
            switch (item.OperationType)
            {
                case OperationType.BeforeSpritePixelDataChanged:
                case OperationType.AfterSpritePixelDataChanged:
                    int offset = _allSprites[item.SpriteIndex].Offset;
                    _romData!.WriteBytes(offset, item.PixelData!);
                    SelectSpriteByIndex(item.SpriteIndex);
                    break;
            }
        }

        #endregion HISTORY

        private void BrowseForRobotronRom(string label, RomSetType romSetType)
        {
            IRomService service = RomServiceFactory.Create(romSetType);

            RomData? romData = LoadRomData(label, service);
            if (romData == null)
            {
                return;
            }

            SpriteEditorDependencies dependencies = SpriteEditorDependenciesFactory.Create(romSetType);
            OnBeginEdit(label, romData, service, dependencies);
        }

        private void OnBeginEdit(string romSetName, RomData romData, IRomService romService, SpriteEditorDependencies editorDependencies)
        {
            _suspendChangeEvents = true;

            _history.Clear();

            _romSetName = romSetName;

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _spriteFactory = editorDependencies.SpriteFactory;
            _palette = editorDependencies.PaletteService.GetPalette();
            pnlPalette.Palette = _palette;
            _spriteRenderer = editorDependencies.SpriteRenderer;

            SpriteInfo spriteInfo = SetSpriteSelectDropdown(editorDependencies.SpriteRepository.GetAllSprites().ToList());
            _selectedSpriteInfo = spriteInfo;

            _sprite = CreateSpriteFromRomData(romData, spriteInfo);

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = _sprite;
            spriteDisplay.Palette = _palette;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = _zoomLevel;

            UpdateStatusBarWithSpriteInfo(_selectedSpriteInfo);
            OnDisplayStateChanged();

            _suspendChangeEvents = false;
        }

        private void DisableEditingControls()
        {
            cboSprite.Enabled = false;
            cboSprite.DataSource = null;
            cboSprite.SelectedIndex = -1;
            nudZoom.Enabled = false;
            pnlPalette.Enabled = false;
        }

        #region ROM

        /// <summary>
        /// Create a Sprite to render from the sprite info
        /// </summary>
        private ISprite CreateSpriteFromRomData(RomData romData, SpriteInfo spriteInfo)
        {
            return _spriteFactory!.CreateSpriteFromRomData(romData, spriteInfo);
        }


        private RomData? LoadRomData(string romSetName, IRomService romService)
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder containing the {romSetName} ROM files";
            folderDialog.UseDescriptionForTitle = true;

            if (folderDialog.ShowDialog() != DialogResult.OK) return null;

            string directory = folderDialog.SelectedPath;
            RomFileAuditInfo auditInfo = romService.Audit(directory);

            if (auditInfo.MissingRomFiles.Length > 0)
            {
                StringBuilder sb = new("MISSING FILES:");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.AppendJoin(Environment.NewLine, auditInfo.MissingRomFiles);

                MessageBox.Show(sb.ToString(), $"Could not load {romSetName} ROM files.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            RomData romData = romService.LoadRomData(directory);

            MessageBox.Show($"Loaded {romSetName} ROM files successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return romData;
        }

        private void SaveRomData()
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder to write the {_romSetName} ROM files.";

            if (folderDialog.ShowDialog() != DialogResult.OK) return;

            string directory = folderDialog.SelectedPath;

            _romService!.SaveRomData(_romData!, directory);
        }

        #endregion ROM
    }
}