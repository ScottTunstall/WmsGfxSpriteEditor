using System.Text;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Roms.Robotron2084;
using WmsGfxSpriteEditor.Sprites;
using static System.Windows.Forms.AxHost;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private IRomService? _romService;

        private ISpriteGridRenderer? _spriteRenderer;
        private ISpriteFactory? _spriteFactory;
        private ISpriteClipboardService? _clipboardService = new DefaultSpriteClipboardService();

        private string _romSetName = string.Empty;
        private RomData? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);

        // User selections
        protected Color SelectedColour { get; private set; } = Color.Black;

        protected IReadOnlyList<SpriteInfo> AllSprites { get; private set; } = [];
        protected bool HaveSpritesToSelect => AllSprites.Count > 0;
        protected int PaletteIndex { get; private set; }
        protected SpriteInfo? SpriteInfo { get; private set; }
        protected ISprite? Sprite { get; private set; }
        protected UInt128 SpriteHash { get; private set; }
        protected int SpriteIndex { get; private set; }
        protected int ZoomLevel { get; private set; } = 3;

        private bool _suppressControlChangeEvents;
        private readonly History.History _history = new();

        // Palette
        private Color[] _palette = default!;

        public MainForm()
        {
            InitializeComponent();

            _suppressControlChangeEvents = true;

            DisableEditingControls();

            nudZoom.Value = ZoomLevel;

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;

            _suppressControlChangeEvents = false;
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
            if (ZoomLevel <= nudZoom.Maximum)
            {
                SetZoom(ZoomLevel + 1, true);
            }
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            if (ZoomLevel >= nudZoom.Minimum)
            {
                SetZoom(ZoomLevel - 1, true);
            }
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
                return;

            SetZoom((int)nudZoom.Value, false);
        }

        #endregion VIEW MENU EVENT HANDLERS

        #region SPRITE MENU EVENT HANDLERS

        private void mnuSpriteFlipHorizontal_Click(object sender, EventArgs e)
        {
            FlipSpriteHorizontal();
        }

        private void mnuSpriteFlipVertical_Click(object sender, EventArgs e)
        {
            FlipSpriteVertical();
        }

        private void mnuSpriteShiftLeft_Click(object sender, EventArgs e)
        {
            ShiftSpriteLeft();
        }

        private void mnuSpriteShiftRight_Click(object sender, EventArgs e)
        {
            ShiftSpriteRight();
        }

        private void mnuSpriteShiftUp_Click(object sender, EventArgs e)
        {
            ShiftSpriteUp();
        }

        private void mnuSpriteShiftDown_Click(object sender, EventArgs e)
        {
            ShiftSpriteDown();
        }

        #endregion SPRITE MENU EVENT HANDLERS

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
                return;

            SelectSpriteByIndex(cboSprite.SelectedIndex, false);
        }

        #endregion SPRITE COMBO BOX EVENT HANDLERS

        #region PALETTE CONTROL EVENT HANDLERS

        private void PnlPalette_ColorSelected(object? sender, ColourSelectedEventArgs e)
        {
            if (_suppressControlChangeEvents)
                return;

            SelectPalette(e.SelectedColour, e.ColourIndex);
        }

        #endregion PALETTE CONTROL EVENT HANDLERS

        #region SPRITE EDITOR EVENT HANDLERS

        private void SpriteDisplay_GridCellMouseMove(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContinueDrawOp(e.GridCell.X, e.GridCell.Y, PaletteIndex);
                // Don't need to fire OnSpriteChanged, wait until the MouseUp for that.
            }

            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }


        private void spriteDisplay_GridCellMouseDown(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            BeginDrawOp(e.GridCell.X, e.GridCell.Y, PaletteIndex);
            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }


        private void spriteDisplay_GridCellMouseUp(object sender, SpriteGridMouseEventArgs e)
        {
            EndDrawOp();
            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }


        #endregion

        #region PALETTE FUNCS

        private void SelectPalette(Color selectedColour, int colourIndex)
        {
            SelectedColour = selectedColour;
            PaletteIndex = colourIndex;
        }

        #endregion PALETTE FUNCS

        #region FILE MENU INVOKED FUNCS

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

        private void OnBeginEdit(string romSetName, RomData romData, IRomService romService, SpriteEditorDependencies editorDependencies)
        {
            _suppressControlChangeEvents = true;

            _history.Clear();

            _romSetName = romSetName;

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _spriteFactory = editorDependencies.SpriteFactory;
            _palette = editorDependencies.PaletteService.GetPalette();
            pnlPalette.Palette = _palette;
            _spriteRenderer = editorDependencies.SpriteRenderer;

            List<SpriteInfo> allSprites = editorDependencies.SpriteRepository.GetAllSprites().ToList();
            SpriteInfo spriteInfo = SetSpriteSelectDropdown(allSprites);
            SpriteInfo = spriteInfo;

            Sprite = CreateSpriteFromRomData();

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = Sprite;
            spriteDisplay.Palette = _palette;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = ZoomLevel;

            OnRomSetLoaded();

            _suppressControlChangeEvents = false;
        }

        #endregion FILE MENU INVOKED FUNCS

        #region EDIT MENU INVOKED FUNCS

        private void Undo()
        {
            HistoryItem? item = _history.Back();
            if (item == null)
            {
                throw new InvalidOperationException("No history item to undo.");
            }

            SetStateFromHistory(item);
        }

        private void Redo()
        {
            HistoryItem item = _history.Forward()!;
            if (item == null)
            {
                throw new InvalidOperationException("No history item to redo.");
            }

            SetStateFromHistory(item!);
        }

        private void CopySpriteToClipboard()
        {
            _clipboardService!.Copy(Sprite!, _palette);
        }

        #endregion EDIT MENU INVOKED FUNCS

        #region VIEW MENU INVOKED FUNCS

        private void SetZoom(int zoomLevel, bool syncZoomControl = true)
        {
            ZoomLevel = zoomLevel;
            if (syncZoomControl)
            {
                SetZoomControls(zoomLevel);
            }

            OnZoomChanged();
        }

        private void SetZoomControls(int zoomLevel)
        {
            bool oldValue = _suppressControlChangeEvents;
            _suppressControlChangeEvents = true;
            nudZoom.Value = zoomLevel;
            spriteDisplay.ZoomLevel = zoomLevel;
            _suppressControlChangeEvents = oldValue;
        }

        #endregion VIEW MENU INVOKED FUNCS

        #region SPRITE MENU INVOKED FUNCS

        private void FlipSpriteHorizontal()
        {
            Sprite!.XFlip();
            OnSpritePixelDataChanged();
        }

        private void FlipSpriteVertical()
        {
            Sprite!.YFlip();
            OnSpritePixelDataChanged();
        }

        private void ShiftSpriteLeft()
        {
            Sprite!.ShiftPixelsLeft();
            OnSpritePixelDataChanged();
        }

        private void ShiftSpriteRight()
        {
            Sprite!.ShiftPixelsRight();
            OnSpritePixelDataChanged();
        }

        private void ShiftSpriteUp()
        {
            Sprite!.ShiftPixelsUp();
            OnSpritePixelDataChanged();
        }

        private void ShiftSpriteDown()
        {
            Sprite!.ShiftPixelsDown();
            OnSpritePixelDataChanged();
        }

        #endregion SPRITE MENU INVOKED FUNCS

        #region SPRITE SELECT COMBO BOX INVOKED FUNCS

        private SpriteInfo SetSpriteSelectDropdown(List<SpriteInfo> spriteInfos, int index = 0)
        {
            AllSprites = spriteInfos;

            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = spriteInfos;

            SetSpriteSelectComboBox(index);

            SpriteIndex = index;
            SpriteInfo spriteInfo = spriteInfos[index]!;
            return spriteInfo;
        }

        private void SelectSpriteByIndex(int index, bool syncControls = true)
        {
            SpriteIndex = index;
            SpriteInfo = AllSprites[index]!;

            if (syncControls)
            {
                SetSpriteSelectComboBox(index);
            }

            if (SpriteInfo != null)
            {
                SetSpriteDisplay(CreateSpriteFromRomData());
            }
            else
            {
                SetSpriteDisplay(null);
            }

            OnSpriteSelectionChanged();
        }

        private void SetSpriteSelectComboBox(int index = 0)
        {
            bool oldValue = _suppressControlChangeEvents;
            _suppressControlChangeEvents = true;
            cboSprite.SelectedIndex = index;
            _suppressControlChangeEvents = oldValue;
        }

        private void SetSpriteDisplay(ISprite? sprite)
        {
            Sprite = sprite;
            SpriteHash = 0;
            spriteDisplay.Sprite = sprite;
            OnSpritePixelDataChanged();
        }

        /// <summary>
        /// Updates the status bar with complete sprite information
        /// </summary>
        private void UpdateStatusBarSpriteInfo()
        {
            if (SpriteInfo == null)
            {
                StatusLabel.Text = "No sprite selected.";
            }
            else
            {
                // Include the sprite offset in both hex and decimal format
                StatusLabel.Text = $"Sprite: {SpriteInfo.Name} | Offset: 0x{SpriteInfo.Offset:X4} ({SpriteInfo.Offset}) | " +
                                   $"Size: {SpriteInfo.WidthInPixels}x{SpriteInfo.Height} pixels " +
                                   $"({SpriteInfo.WidthInBytes} bytes x {SpriteInfo.Height} rows) | " +
                                   $"Format: {(SpriteInfo.IsLinear ? "Linear" : "Non-linear")}";
            }
        }

        #endregion SPRITE SELECT COMBO BOX INVOKED FUNCS

        #region SPRITE EDITOR INVOKED FUNCS

        private void BeginDrawOp(int startX, int startY, int paletteIndex)
        {
            UInt128 spriteHash = Sprite!.GetPixelDataHash();
            if (SpriteHash != spriteHash)
            {
                _history.Add(HistoryItem.CreateBeforeSpritePixelDataChangedHistoryItem(Sprite!.ClonePixelData(), spriteHash, SpriteIndex));
                SpriteHash = Sprite.GetPixelDataHash();
            }

            Sprite!.SetPixelByPaletteIndex(startX, startY, paletteIndex);

            OnSpritePixelDataChanged();
        }

        private void ContinueDrawOp(int x, int y, int paletteIndex)
        {
            Sprite!.SetPixelByPaletteIndex(x, y, paletteIndex);
            OnSpritePixelDataChanged();
        }

        private void EndDrawOp()
        {
            UInt128 spriteHash = Sprite!.GetPixelDataHash();
            if (SpriteHash != spriteHash)
            {
                _history.Add(HistoryItem.CreateAfterSpritePixelDataChangedHistoryItem(Sprite!.ClonePixelData(), spriteHash, SpriteIndex));
                SpriteHash = spriteHash;
                OnSpritePixelDataChanged();
            }
        }

        private void UpdateStatusBarGridCoordinates(int x, int y)
        {
            // Convert zero based coordinates to 1 based
            CoordinatesLabel.Text = $"X: {x+ 1}  Y: {y + 1}";
        }

        #endregion


        protected virtual void OnRomSetLoaded()
        {
            cboSprite.Enabled = HaveSpritesToSelect;
            nudZoom.Enabled = Sprite != null;

            OnPaletteChanged();
            OnSpriteSelectionChanged();
            OnSpritePixelDataChanged();
        }

        protected virtual void OnZoomChanged()
        {
            bool haveSprite = Sprite != null;
            mnuViewZoomIn.Enabled = haveSprite && ZoomLevel < nudZoom.Maximum;
            mnuViewZoomOut.Enabled = haveSprite && ZoomLevel > nudZoom.Minimum;
            spriteDisplay.ZoomLevel = ZoomLevel;
            spriteDisplay.Invalidate();
        }

        protected virtual void OnPaletteChanged()
        {
            pnlPalette.Enabled = Sprite != null && _palette.Length >= 2;
        }

        protected virtual void OnSpriteSelectionChanged()
        {
            bool haveSprite = Sprite != null;
            mnuSpriteFlipHorizontal.Enabled = haveSprite;
            mnuSpriteFlipVertical.Enabled = haveSprite;
            mnuSpriteShiftUp.Enabled = haveSprite;
            mnuSpriteShiftDown.Enabled = haveSprite;
            mnuSpriteShiftLeft.Enabled = haveSprite;
            mnuSpriteShiftRight.Enabled = haveSprite;

            spriteDisplay.Visible = haveSprite;
            spriteDisplay.Invalidate();
            UpdateStatusBarSpriteInfo();
        }

        protected virtual void OnSpritePixelDataChanged()
        {
            mnuEditUndo.Enabled = Sprite != null && _history.CanGoBack;
            mnuEditRedo.Enabled = Sprite != null && _history.CanGoForward;

            spriteDisplay.Invalidate();
        }

        #region HISTORY

        private void SetStateFromHistory(HistoryItem item)
        {
            switch (item.OperationType)
            {
                case OperationType.BeforeSpritePixelDataChanged:
                case OperationType.AfterSpritePixelDataChanged:
                    int offset = AllSprites[item.SpriteIndex].Offset;
                    _romData!.PokeBytes(offset, item.PixelData!);
                    SelectSpriteByIndex(item.SpriteIndex, true);
                    OnSpritePixelDataChanged();
                    break;
            }
        }

        #endregion HISTORY

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
        private ISprite CreateSpriteFromRomData()
        {
            return _spriteFactory!.CreateSpriteFromRomData(_romData!, SpriteInfo!);
        }

        #endregion ROM
    }
}