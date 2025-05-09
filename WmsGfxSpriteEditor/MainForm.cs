using System.Text;
using WmsGfxSpriteEditor.Controls;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.ROMs.Robotron;
using WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared.Palettes;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private IRomService? _romService;
        private ISpriteRenderer? _spriteRenderer;
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
        private int _selectedSpriteIndex;
        private int _zoomLevel = 1; // Default zoom for the normal view

        private bool _suspendChangeEvents;
        private readonly History.History _history = new();

        // Palette
        private Color[] _palette = default!;

        private bool _mouseDown;


        public MainForm()
        {
            InitializeComponent();

            _suspendChangeEvents = true;

            DisableEditingControls();

            // Set the default zoom level
            nudZoom.Value = _zoomLevel;

            splitContainer.SplitterDistance = (int)(splitContainer.Width * 0.2);

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
            RobotronBlueLabelRomFileService service = new();

            RomData? romData = LoadRomSetIntoMemoryStream(RomSetNames.BlueLabel, service);
            if (romData != null)
            {
                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();
                SpriteRenderer spriteRenderer = new();

                OnBeginEdit(RomSetNames.BlueLabel, romData, service, repo, spriteRenderer, palette);
            }
        }

        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            RobotronBlueLabelRomFileService service = new();

            RomData? romData = LoadRomSetIntoMemoryStream(RomSetNames.TieDieWDPU, service);
            if (romData != null)
            {
                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();
                SpriteRenderer spriteRenderer = new();

                OnBeginEdit(RomSetNames.TieDieWDPU, romData, service, repo, spriteRenderer, palette);
            }
        }

        private void mnuFileLoadRobotronTieDieMAME_Click(object sender, EventArgs e)
        {
            // Empty click handler for Tie Die (MAME) ROM
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            // Save functionality would be implemented here
            MessageBox.Show("Save functionality not implemented in this demo.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion EDIT MENU EVENT HANDLERS

        #region VIEW MENU EVENT HANDLERS

        private void mnuViewZoomIn_Click(object sender, EventArgs e)
        {
            if (_zoomLevel <= nudZoom.Maximum)
            {
                SetZoom(_zoomLevel + 1, true);
            }
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            if (_zoomLevel >= nudZoom.Minimum)
            {
                SetZoom(_zoomLevel - 1, true);
            }
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SetZoom((int)nudZoom.Value, true);
        }

        #endregion VIEW MENU EVENT HANDLERS

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SelectSpriteByIndex(cboSprite.SelectedIndex, true);
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

        private void spriteDisplay_GridCellMouseDown(object sender, GridCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = true;

                SaveSelectedSpriteStateToHistory();

                _sprite!.SetPixelByPaletteIndex(e.GridX, e.GridY, _selectedPaletteIndex);

                spriteDisplay.Invalidate();
                OnDisplayStateChanged();
            }
        }

        private void SpriteDisplay_GridCellMouseMove(object sender, GridEventArgs e)
        {
            if (_mouseDown)
            {
                _sprite!.SetPixelByPaletteIndex(e.GridX, e.GridY, _selectedPaletteIndex);
                spriteDisplay.Invalidate();
            }

            CoordinatesLabel.Text = $"X: {e.GridX} Y: {e.GridY}";
        }

        private void spriteDisplay_GridCellMouseUp(object sender, GridEventArgs e)
        {
            _mouseDown = false;

            SaveSelectedSpriteStateToHistory();
        }

        #endregion SPRITE GRID EVENT HANDLERS

        private void OnBeginEdit(string romSetName, RomData romData, IRomService romService, ISpriteRepository spriteRepository, ISpriteRenderer spriteRenderer, IPalette palette)
        {
            _history.Clear();

            _romSetName = romSetName;

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _palette = palette.GetPalette();
            pnlPalette.Palette = _palette;

            _spriteRenderer = spriteRenderer;

            _suspendChangeEvents = true;
            _selectedSpriteInfo = SetSpriteSelectDropdown(spriteRepository.GetAllSprites().ToList());
            _suspendChangeEvents = false;

            _sprite = CreateSpriteFromRomData(_selectedSpriteInfo);

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = _sprite;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = _zoomLevel;

            OnDisplayStateChanged();
        }

        private void DisableEditingControls()
        {
            cboSprite.Enabled = false;
            cboSprite.DataSource = null;
            cboSprite.SelectedIndex = -1;
            nudZoom.Enabled = false;
            pnlPalette.Enabled = false;
        }

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
            HistoryItem item = _history.Back()!;
            SetStateFromHistory(item!);
            OnDisplayStateChanged();
        }

        private void Redo()
        {
            HistoryItem item = _history.Forward()!;
            SetStateFromHistory(item!);
            OnDisplayStateChanged();
        }

        #endregion EDIT FUNCS

        #region VIEW FUNCS

        private void SetZoom(int newZoomLevel, bool saveStateToHistory)
        {
            if (saveStateToHistory)
            {
                SaveZoomStateToHistory();
            }

            _zoomLevel = newZoomLevel;
            _suspendChangeEvents = true;
            nudZoom.Value = newZoomLevel;
            spriteDisplay.ZoomLevel = newZoomLevel;
            _suspendChangeEvents = false;

            if (saveStateToHistory)
            {
                SaveZoomStateToHistory();
            }

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

        private void SelectSpriteByIndex(int spriteIndex, bool saveStateToHistory)
        {
            if (saveStateToHistory)
            {
                SaveSelectedSpriteIndexToHistory();
            }

            _selectedSpriteIndex = spriteIndex;
            _selectedSpriteInfo = _allSprites[spriteIndex]!;

            if (saveStateToHistory)
            {
                SaveSelectedSpriteIndexToHistory();
            }

            if (_selectedSpriteInfo != null)
            {
                SetSpriteDisplay(CreateSpriteFromRomData(_selectedSpriteInfo));
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

        private void SaveZoomStateToHistory()
        {
            _history.Add(HistoryItem.CreateZoomHistoryItem(_zoomLevel));
        }

        private void SaveSelectedSpriteIndexToHistory()
        {
            _history.Add(HistoryItem.CreateSpriteSelectionChangingHistoryItem(_selectedSpriteIndex));
        }

        private void SaveSelectedSpriteStateToHistory()
        {
            _history.Add(HistoryItem.CreateSpriteDataChangingHistoryItem(_sprite!, _selectedSpriteIndex, _selectedSpriteInfo!.Offset));
        }

        private void SetStateFromHistory(HistoryItem item)
        {
            switch (item.OperationType)
            {
                case OperationType.Zoom:
                    SetZoom((int)item.ZoomLevel, false);
                    break;

                case OperationType.SpriteSelectionChanging:
                    SelectSpriteByIndex(item.SpriteIndex, false);
                    break;

                case OperationType.SpriteDataChanging:
                    RestoreSprite(item.SpriteIndex, item.SpriteData!);
                    break;
            }
        }

        private void RestoreSprite(int itemSpriteIndex, byte[] itemSpriteData)
        {
            WriteSpriteDataToRomData(itemSpriteIndex, itemSpriteData!);
            SelectSpriteByIndex(itemSpriteIndex, false);
        }

        #endregion HISTORY

        #region ROM

        /// <summary>
        /// Create a Sprite to render from the sprite info
        /// </summary>
        private ISprite CreateSpriteFromRomData(SpriteInfo spriteInfo)
        {
            int bytesToRead = spriteInfo.WidthInBytes * spriteInfo.Height;
            Memory<byte> spriteData = _romData!.ReadBytes(spriteInfo.Offset, bytesToRead);

            // TODO: Move into factory method in Sprite4Bpp class
            return new Sprite4Bpp(spriteData, _palette, spriteInfo.WidthInBytes, spriteInfo.Height, spriteInfo.IsLinear);
        }

        private void WriteSpriteDataToRomData(int spriteIndex, byte[] spriteData)
        {
            _romData!.WriteBytes(_allSprites[spriteIndex].Offset, spriteData);
        }

        #endregion ROM

#pragma warning disable CA1859

        private RomData? LoadRomSetIntoMemoryStream(string romSetName, IRomService romService)
#pragma warning restore CA1859
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

            // Free previous ROM data
            RomData romFiles = romService.LoadRomData(directory);

            MessageBox.Show($"Loaded {romSetName} ROM files successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return romFiles;
        }

        private void SaveMemoryStreamToRomSet()
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder to write the {_romSetName} ROM files.";

            if (folderDialog.ShowDialog() != DialogResult.OK) return;

            string directory = folderDialog.SelectedPath;

            _romService.SaveRomData(_romData!, directory);
        }
    }
}