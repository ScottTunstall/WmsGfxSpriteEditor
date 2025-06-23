using WmsGfxSpriteEditor.Dialogs;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Roms.Commands;
using WmsGfxSpriteEditor.Roms.Robotron2084;
using WmsGfxSpriteEditor.Sprites;
using WmsGfxSpriteEditor.Sprites.Commands;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private IRomService? _romService;

        private readonly IHistory _history;
        private ISpriteGridRenderer? _spriteRenderer;
        private ISpriteFactory? _spriteFactory;
        private readonly ISpriteClipboardService _clipboardService;

        private string _romSetName = string.Empty;
        private RomData? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);

        protected IReadOnlyList<SpriteInfo> AvailableSprites { get; private set; } = [];

        // User selections
        protected Color ActivePaletteColour { get; private set; } = Color.Black;

        protected int ActivePaletteIndex { get; private set; }
        protected int ActiveSpriteIndex { get; private set; }
        protected SpriteInfo? ActiveSpriteInfo { get; private set; }
        protected ISprite? ActiveSprite { get; private set; }
        protected int ZoomLevel { get; private set; } = 3;

        private bool _suppressControlChangeEvents;

        // Palette
        protected Color[] Palette { get; private set; } = default!;

        public MainForm()
        {
            _history = new History.History();
            _clipboardService = new DefaultSpriteClipboardService(_history);

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
            spriteDisplay.Invalidate();
            pnlPalette.Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ActiveSprite != null)
            {
                // Only zoom if CTRL is held
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {

                    if (e.Delta > 0)
                    {
                        // Zoom in
                        if (ZoomLevel < nudZoom.Maximum)
                        {
                            SetZoom(ZoomLevel + 1, true);
                        }
                    }
                    else if (e.Delta < 0)
                    {
                        // Zoom out
                        if (ZoomLevel > nudZoom.Minimum)
                        {
                            SetZoom(ZoomLevel - 1, true);
                        }
                    }
                }
            }

            base.OnMouseWheel(e);
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

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            CopyClipboardToSprite();
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
            ShiftSpritePixelsLeft();
        }

        private void mnuSpriteShiftRight_Click(object sender, EventArgs e)
        {
            ShiftSpritePixelsRight();
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

            SelectActiveSpriteByIndex(cboSprite.SelectedIndex, false);
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

        private void spriteDisplay_GridCellMouseDown(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            BeginSpriteDrawOp(e.GridCell.X, e.GridCell.Y, ActivePaletteIndex);
            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }

        private void SpriteDisplay_GridCellMouseMove(object sender, SpriteGridMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContinueSpriteDrawOp(e.GridCell.X, e.GridCell.Y, ActivePaletteIndex);
            }

            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }

        private void spriteDisplay_GridCellMouseUp(object sender, SpriteGridMouseEventArgs e)
        {
            EndSpriteDrawOp();
            UpdateStatusBarGridCoordinates(e.GridCell.X, e.GridCell.Y);
        }

        #endregion SPRITE EDITOR EVENT HANDLERS

        #region PALETTE FUNCS

        protected void SelectPalette(Color selectedColour, int colourIndex)
        {
            ActivePaletteColour = selectedColour;
            ActivePaletteIndex = colourIndex;
        }

        #endregion PALETTE FUNCS

        #region FILE MENU INVOKED FUNCS

        protected void BrowseForRobotronRom(string label, RomSetType romSetType)
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

        protected virtual RomData? LoadRomData(string romSetName, IRomService romService)
        {
            string? directory = new LoadRomDialog(romSetName).BrowseForFolder();
            if (directory == null)
            {
                return null;
            }

            RomFileAuditInfo auditInfo = romService.Audit(directory);

            if (auditInfo.MissingRomFiles.Length > 0)
            {
                new MissingFilesDialog(romSetName).ShowDialog(auditInfo.MissingRomFiles, this);
                return null;
            }

            RomData romData = romService.LoadRomData(directory);

            new InformationDialog().ShowDialog($"Loaded {romSetName} ROM files successfully.", "Success", this);
            return romData;
        }

        protected virtual void SaveRomData()
        {
            string? directory = new SaveRomDialog(_romSetName).BrowseForFolder();
            if (directory == null)
            {
                return;
            }

            _romService!.SaveRomData(_romData!, directory);

            new InformationDialog().ShowDialog($"Saved {_romSetName} ROM files successfully.", "Success", this);
        }

        protected virtual void OnBeginEdit(string romSetName, RomData romData, IRomService romService, SpriteEditorDependencies editorDependencies)
        {
            _suppressControlChangeEvents = true;

            _history.Clear();

            _romSetName = romSetName;

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _spriteFactory = editorDependencies.SpriteFactory;
            Palette = editorDependencies.PaletteService.GetPalette();
            pnlPalette.Palette = Palette;
            _spriteRenderer = editorDependencies.SpriteRenderer;

            List<SpriteInfo> allSprites = editorDependencies.SpriteRepository.GetAllSprites().ToList();
            SpriteInfo spriteInfo = SetSpriteSelectDropdown(allSprites);
            ActiveSpriteInfo = spriteInfo;

            ActiveSprite = CreateSpriteFromRomData();

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = ActiveSprite;
            spriteDisplay.Palette = Palette;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = ZoomLevel;

            OnRomSetLoaded();

            _suppressControlChangeEvents = false;
        }

        #endregion FILE MENU INVOKED FUNCS

        #region EDIT MENU INVOKED FUNCS

        protected void Undo()
        {
            if (!_history.CanGoBack)
            {
                throw new InvalidOperationException("No history item to undo.");
            }

            HistoryItem? item = _history.Back();
            SetStateFromHistory(item!);
        }

        protected void Redo()
        {
            if (!_history.CanGoForward)
            {
                throw new InvalidOperationException("No history item to redo.");
            }

            HistoryItem item = _history.Forward()!;
            SetStateFromHistory(item!);
        }

        protected void CopySpriteToClipboard()
        {
            ThrowIfNoActiveSprite();

            _clipboardService!.Copy(ActiveSprite!);
        }

        private void CopyClipboardToSprite()
        {
            ThrowIfNoActiveSprite();

            _clipboardService!.Paste(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        #endregion EDIT MENU INVOKED FUNCS

        #region VIEW MENU INVOKED FUNCS

        protected void SetZoom(int zoomLevel, bool syncZoomControl = true)
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

        protected void FlipSpriteHorizontal()
        {
            ThrowIfNoActiveSprite();
            // Note: I could implement this via Mediatr but its overkill just now.
            new FlipSpritePixelsHorizontalCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void FlipSpriteVertical()
        {
            ThrowIfNoActiveSprite();
            new FlipSpritePixelsVerticalCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpritePixelsLeft()
        {
            ThrowIfNoActiveSprite();
            new ShiftSpritePixelsLeftCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpritePixelsRight()
        {
            ThrowIfNoActiveSprite();
            new ShiftSpritePixelsRightCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpriteUp()
        {
            ThrowIfNoActiveSprite();
            new ShiftSpritePixelsUpCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpriteDown()
        {
            ThrowIfNoActiveSprite();
            new ShiftSpritePixelsDownCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        private void ThrowIfNoActiveSprite()
        {
            if (ActiveSprite == null)
            {
                throw new InvalidOperationException("Operation cannot be performed without an active sprite");
            }
        }

        #endregion SPRITE MENU INVOKED FUNCS

        #region SPRITE SELECT COMBO BOX INVOKED FUNCS

        protected SpriteInfo SetSpriteSelectDropdown(List<SpriteInfo> spriteInfos, int index = 0)
        {
            AvailableSprites = spriteInfos;

            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = spriteInfos;

            SetSpriteSelectComboBox(index);

            ActiveSpriteIndex = index;
            SpriteInfo spriteInfo = spriteInfos[index]!;
            return spriteInfo;
        }

        protected void SelectActiveSpriteByIndex(int index, bool syncControls = true)
        {
            ActiveSpriteIndex = index;
            ActiveSpriteInfo = AvailableSprites[index]!;

            if (syncControls)
            {
                SetSpriteSelectComboBox(index);
            }

            if (ActiveSpriteInfo != null)
            {
                SetActiveSpriteDisplay(CreateSpriteFromRomData());
            }
            else
            {
                SetActiveSpriteDisplay(null);
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

        private void SetActiveSpriteDisplay(ISprite? sprite)
        {
            ActiveSprite = sprite;
            spriteDisplay.Sprite = sprite;
            OnSpritePixelDataChanged();
        }

        #endregion SPRITE SELECT COMBO BOX INVOKED FUNCS

        #region SPRITE GRID INVOKED FUNCS

        protected virtual void BeginSpriteDrawOp(int startX, int startY, int paletteIndex)
        {
            ThrowIfNoActiveSprite();
            new BeginSpritePixelOpCommand(_history).Execute(ActiveSprite!, startX, startY, paletteIndex);
            OnSpritePixelDataChanged();
        }

        protected virtual void ContinueSpriteDrawOp(int x, int y, int paletteIndex)
        {
            ThrowIfNoActiveSprite();
            new SpritePixelOpCommand().Execute(ActiveSprite!, x, y, paletteIndex);
            OnSpritePixelDataChanged();
        }

        protected virtual void EndSpriteDrawOp()
        {
            ThrowIfNoActiveSprite();
            new EndSpritePixelOpCommand(_history).Execute(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        #endregion SPRITE GRID INVOKED FUNCS

        #region HISTORY

        protected virtual void SetStateFromHistory(HistoryItem item)
        {
            switch (item.OperationType)
            {
                case OperationType.SpritePixelDataSnapshot:
                    RestoreSpriteFromHistoryItem(item);
                    break;
            }
        }

        private void RestoreSpriteFromHistoryItem(HistoryItem item)
        {
            int offset = AvailableSprites[item.SpriteIndex].Offset;

            new UpdateRomDataFromPixelDataCommand(_romData!).Execute(offset, item.PixelData!);
            SelectActiveSpriteByIndex(item.SpriteIndex, true);
            OnSpritePixelDataChanged();
        }

        #endregion HISTORY

        #region STATUS BAR

        /// <summary>
        /// Updates the status bar with complete sprite information
        /// </summary>
        private void UpdateStatusBarSpriteInfo()
        {
            if (ActiveSpriteInfo == null)
            {
                StatusLabel.Text = "No sprite selected.";
            }
            else
            {
                // Include the sprite offset in both hex and decimal format
                StatusLabel.Text = $"Sprite: {ActiveSpriteInfo.Name} | Offset: 0x{ActiveSpriteInfo.Offset:X4} ({ActiveSpriteInfo.Offset}) | " +
                                   $"Size: {ActiveSpriteInfo.WidthInPixels}x{ActiveSpriteInfo.Height} pixels " +
                                   $"({ActiveSpriteInfo.WidthInBytes} bytes x {ActiveSpriteInfo.Height} rows) | " +
                                   $"Format: {(ActiveSpriteInfo.IsLinear ? "Linear" : "Non-linear")}";
            }
        }

        private void UpdateStatusBarGridCoordinates(int x, int y)
        {
            // Convert zero based coordinates to 1 based
            CoordinatesLabel.Text = $"X: {x + 1}  Y: {y + 1}";
        }

        #endregion STATUS BAR

        protected virtual void OnRomSetLoaded()
        {
            cboSprite.Enabled = AvailableSprites.Count > 0;
            OnPaletteChanged();
            OnSpriteSelectionChanged();
            OnSpritePixelDataChanged();
        }

        protected virtual void OnZoomChanged()
        {
            bool haveSprite = ActiveSprite != null;
            mnuViewZoomIn.Enabled = haveSprite && ZoomLevel < nudZoom.Maximum;
            mnuViewZoomOut.Enabled = haveSprite && ZoomLevel > nudZoom.Minimum;
            spriteDisplay.ZoomLevel = ZoomLevel;
            spriteDisplay.Invalidate();
        }

        protected virtual void OnPaletteChanged()
        {
            pnlPalette.Enabled = ActiveSprite != null && Palette.Length >= 2;
        }

        protected virtual void OnSpriteSelectionChanged()
        {
            bool haveSprite = ActiveSprite != null;

            // Edit menu
            mnuEditCopy.Enabled = haveSprite;
            mnuEditPaste.Enabled = haveSprite;

            // View menu
            mnuViewZoomIn.Enabled = haveSprite;
            mnuViewZoomOut.Enabled = haveSprite;

            // Sprite menu
            mnuSpriteFlipHorizontal.Enabled = haveSprite;
            mnuSpriteFlipVertical.Enabled = haveSprite;
            mnuSpriteShiftUp.Enabled = haveSprite;
            mnuSpriteShiftDown.Enabled = haveSprite;
            mnuSpriteShiftLeft.Enabled = haveSprite;
            mnuSpriteShiftRight.Enabled = haveSprite;

            // Zoom control
            nudZoom.Enabled = haveSprite;

            spriteDisplay.Visible = haveSprite;
            spriteDisplay.Invalidate();
            UpdateStatusBarSpriteInfo();
        }

        protected virtual void OnSpritePixelDataChanged()
        {
            mnuEditUndo.Enabled = ActiveSprite != null && _history.CanGoBack;
            mnuEditRedo.Enabled = ActiveSprite != null && _history.CanGoForward;

            spriteDisplay.Invalidate();
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
        private ISprite CreateSpriteFromRomData()
        {
            return new CreateSpriteFromRomDataCommand(_romData!, _spriteFactory!).Execute(ActiveSpriteInfo!, Palette);
        }

        #endregion ROM
    }
}