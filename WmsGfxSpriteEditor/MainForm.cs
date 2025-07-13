using System.Runtime.InteropServices;
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
#pragma warning disable SYSLIB1054

        // Windows API constants
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

#pragma warning restore SYSLIB1054

        // Consts
        private const int MinZoomLevel = 1;

        private const int MaxZoomLevel = 32;
        private const int DefaultZoomLevel = 3;

        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);

        // Service dependencies
        private IRomService? _romService;

        private readonly IHistory _history;
        private ISpriteGridRenderer? _spriteRenderer;
        private ISpriteFactory? _spriteFactory;
        private readonly ISpriteService _spriteService;
        private readonly ISpriteClipboardService _clipboardService;

        // Dialogs
        private ColorPickerDialog? _colorPickerDialog;

        // Rom specific
        private string _romSetName = string.Empty;

        private RomData? _romData;

        private bool _suppressControlChangeEvents;
        private Color[] _palette = default!;
        private ISprite? _activeSprite;
        private int _zoomLevel = DefaultZoomLevel;
        private int _activePaletteIndex = -1;

        protected IReadOnlyList<SpriteInfo> AvailableSprites { get; private set; } = [];

        // User selections
        protected Color ActivePaletteColour { get; private set; } = Color.Black;

        protected int ActivePaletteIndex
        {
            get => _activePaletteIndex;
            private set
            {
                if (value != _activePaletteIndex)
                {
                    _activePaletteIndex = value;
                    ActivePaletteColour = Palette[value];
                    OnActivePaletteIndexChanged();
                }
            }
        }

        protected int ActiveSpriteIndex { get; private set; }
        protected SpriteInfo? ActiveSpriteInfo { get; private set; }

        protected ISprite? ActiveSprite
        {
            get => _activeSprite;
            private set
            {
                if (value != _activeSprite)
                {
                    _activeSprite = value;
                    _activeSprite?.ClearPixelDataDirtyFlag();
                    OnActiveSpriteChanged();
                }
            }
        }

        protected int ZoomLevel
        {
            get => _zoomLevel;
            private set
            {
                if (value is < MinZoomLevel or > MaxZoomLevel)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Zoom level must be between {MinZoomLevel} and {MaxZoomLevel}.");
                }

                if (value == _zoomLevel)
                {
                    return; // No change
                }

                _zoomLevel = value;
                OnZoomChanged();
            }
        }

        // Palette
        protected Color[] Palette
        {
            get => _palette;
            private set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentException("Palette must have at least 2 colors.", nameof(value));
                }

                _palette = value;
                OnPaletteChanged();
            }
        }

        public MainForm()
        {
            _history = new History.History();
            _spriteService = new SpriteService(_history);
            _clipboardService = new DefaultSpriteClipboardService(_history);

            InitializeComponent();

            _suppressControlChangeEvents = true;

            DisableEditingControls();

            nudZoom.Minimum = MinZoomLevel;
            nudZoom.Maximum = MaxZoomLevel;
            nudZoom.Value = ZoomLevel;

            _ = AddClipboardFormatListener(this.Handle);

            _suppressControlChangeEvents = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WindowsMessages.WM_CLIPBOARDUPDATE)
            {
                OnClipboardChanged();
            }

            base.WndProc(ref m);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ = RemoveClipboardFormatListener(this.Handle);
            base.OnFormClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            spriteDisplay.Invalidate();
        }

        private void MagnificationPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (ActiveSprite != null)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (e.Delta > 0)
                    {
                        if (ZoomLevel < MaxZoomLevel)
                        {
                            ZoomLevel++;
                        }
                    }
                    else if (e.Delta < 0)
                    {
                        if (ZoomLevel > MinZoomLevel)
                        {
                            ZoomLevel--;
                        }
                    }
                }
            }
            // No need to set Handled, as MagPanel prevents scrolling
        }

        #region FILE MENU EVENT HANDLERS

        private void mnuFileLoadRobotronBlueLabel_Click(object sender, EventArgs e)
        {
            BrowseForRobotronRom(RobotronRomSetNames.BlueLabel, RobotronRomSetType.BlueLabel);
        }

        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            BrowseForRobotronRom(RobotronRomSetNames.TieDieWDPU, RobotronRomSetType.TieDieWDPU);
        }

        private void mnuFileLoadRobotronTieDieMAME_Click(object sender, EventArgs e)
        {
            BrowseForRobotronRom(RobotronRomSetNames.TieDieMAME, RobotronRomSetType.TieDieMAME);
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
            if (ZoomLevel < MaxZoomLevel)
            {
                ZoomLevel++;
            }
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            if (ZoomLevel > MinZoomLevel)
            {
                ZoomLevel--;
            }
        }

        // Handler for Zoom to Window
        private void mnuViewZoomToWindow_Click(object sender, EventArgs e)
        {
            ZoomToFit();
        }

        private void mnuViewPalette_Click(object sender, EventArgs e)
        {
            ShowColourPickerDialog();
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
                return;

            ZoomLevel = (int)nudZoom.Value;
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
            ShiftSpritePixelsUp();
        }

        private void mnuSpriteShiftDown_Click(object sender, EventArgs e)
        {
            ShiftSpriteDown();
        }

        #endregion SPRITE MENU EVENT HANDLERS

        #region PALETTE MENU EVENT HANDLERS

        // Handler for Copy Selected Colour as Hex
        private void mnuCopySelectedColourHex_Click(object sender, EventArgs e)
        {
            string hex = CopyActivePaletteColourAsHex();
            new InformationDialog().ShowDialog(
                $"Copied selected colour {hex} to clipboard as hex.",
                "Colour Copied",
                this
            );
        }

        // Handler for Copy Selected Colour as RGB
        private void mnuCopySelectedColourRgb_Click(object sender, EventArgs e)
        {
            string rgb = CopyActivePaletteColourAsRGB();
            new InformationDialog().ShowDialog(
                $"Copied selected colour {rgb} to clipboard as RGB.",
                "Colour Copied",
                this
            );
        }

        #endregion PALETTE MENU EVENT HANDLERS

        #region HELP MENU EVENT HANDLERS

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                "Williams Graphics Sprite Editor." + Environment.NewLine +
                Environment.NewLine +
                "Designed and developed by Scott Tunstall." + Environment.NewLine +
                "Sprite offsets discovered and documented by Sean Riddle." + Environment.NewLine +
                "All rights reserved.",
                "About Williams Graphics Sprite Editor",
                MessageBoxButtons.OK,
                MessageBoxIcon.None
            );
        }

        #endregion HELP MENU EVENT HANDLERS

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
                return;

            SelectActiveSpriteByIndex(cboSprite.SelectedIndex, false);
        }

        #endregion SPRITE COMBO BOX EVENT HANDLERS

        #region TOOLSTRIP EVENT HANDLERS

        private void btnShowPalette_Click(object sender, EventArgs e)
        {
            ToggleColourPickerDialog();
        }

        #endregion TOOLSTRIP EVENT HANDLERS

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

        private void spriteDisplay_GridCellMouseMove(object sender, SpriteGridMouseEventArgs e)
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

        protected void SelectActivePaletteColour(Color selectedColour, int colourIndex)
        {
            ActivePaletteColour = selectedColour;
            ActivePaletteIndex = colourIndex;
        }

        #endregion PALETTE FUNCS

        #region FILE MENU INVOKED FUNCS

        protected void BrowseForRobotronRom(string label, RobotronRomSetType romSetType)
        {
            SpriteEditorDependencies dependencies = SpriteEditorDependenciesFactory.CreateForRobotron(romSetType);

            RomData? romData = LoadRomData(label, dependencies.RomService);
            if (romData == null)
            {
                return;
            }

            OnBeginEdit(label, romData, dependencies);
        }

        protected virtual RomData? LoadRomData(string romSetName, IRomService romService)
        {
            string? directory = new LoadRomDialog().BrowseForFolder(romSetName);
            if (directory == null)
            {
                return null;
            }

            RomFileAuditInfo auditInfo = romService.Audit(directory);

            if (auditInfo.MissingRomFiles.Length > 0)
            {
                new MissingFilesDialog().ShowDialog(romSetName, auditInfo.MissingRomFiles, this);
                return null;
            }

            RomData romData = romService.LoadRomData(directory);

            new InformationDialog().ShowDialog($"Loaded {romSetName} ROM files successfully.", "Success", this);
            return romData;
        }

        protected virtual void SaveRomData()
        {
            string? directory = new SaveRomDialog().BrowseForFolder(_romSetName);
            if (directory == null)
            {
                return;
            }

            _romService!.SaveRomData(_romData!, directory);

            new InformationDialog().ShowDialog($"Saved {_romSetName} ROM files successfully.", "Success", this);
        }

        protected virtual void OnBeginEdit(string romSetName, RomData romData, SpriteEditorDependencies editorDependencies)
        {
            _suppressControlChangeEvents = true;

            _history.Clear();

            _romSetName = romSetName;

            _romData?.Dispose();
            _romData = romData;

            _romService = editorDependencies.RomService;
            _spriteFactory = editorDependencies.SpriteFactory;
            Palette = editorDependencies.PaletteService.GetPalette();

            _spriteRenderer = editorDependencies.SpriteRenderer;

            List<SpriteInfo> allSprites = editorDependencies.SpriteRepository.GetAllSprites().ToList();
            SpriteInfo spriteInfo = SetSpriteSelectDropdown(allSprites);
            ActiveSpriteInfo = spriteInfo;

            ActiveSprite = CreateSpriteFromRomData();

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = ActiveSprite;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = ZoomLevel;

            cboSprite.Enabled = AvailableSprites.Count > 0;
            OnSpritePixelDataChanged();

            _suppressControlChangeEvents = false;

            OnReady();
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

        protected void CopyClipboardToSprite()
        {
            ThrowIfNoActiveSprite();

            _clipboardService!.Paste(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        #endregion EDIT MENU INVOKED FUNCS

        #region VIEW MENU INVOKED FUNCS

        protected void ZoomToFit()
        {
            spriteDisplay.Zoom(magnificationPanel.ClientSize);
            ZoomLevel = spriteDisplay.ZoomLevel;
        }

        protected void ToggleColourPickerDialog()
        {
            if (_colorPickerDialog == null || !_colorPickerDialog.Visible)
            {
                ShowColourPickerDialog();
            }
            else
            {
                HideColourPickerDialog();
            }
        }

        protected void ShowColourPickerDialog()
        {
            if (_colorPickerDialog != null && !_colorPickerDialog.IsDisposed)
            {
                _colorPickerDialog.Show();
                _colorPickerDialog.BringToFront();
                OnColourPickerShown();
            }
            else
            {
                _colorPickerDialog = new ColorPickerDialog(Palette)
                {
                    SelectedPaletteIndex = ActivePaletteIndex,
                    StartPosition = FormStartPosition.Manual,
                };

                _colorPickerDialog.Location = new Point(
                    this.Location.X + (this.Width - _colorPickerDialog.Width) / 2,
                    this.Location.Y + (this.Height - _colorPickerDialog.Height) / 2
                );

                _colorPickerDialog.SelectedColorChanged += (s, args) =>
                {
                    if (_colorPickerDialog.SelectedPaletteIndex >= 0)
                    {
                        SelectActivePaletteColour(_colorPickerDialog.Palette[_colorPickerDialog.SelectedPaletteIndex],
                            _colorPickerDialog.SelectedPaletteIndex);
                    }
                };

                _colorPickerDialog.Shown += (s, args) =>
                {
                    OnColourPickerShown();
                };

                _colorPickerDialog.FormClosing += (s, args) =>
                {
                    if (args.CloseReason != CloseReason.FormOwnerClosing)
                    {
                        args.Cancel = true;
                        _colorPickerDialog.Hide();
                        OnColourPickerDialogHidden();
                    }
                };

                _colorPickerDialog.Show(this);
            }
        }

        protected void HideColourPickerDialog()
        {
            _colorPickerDialog?.Hide();
        }

        #endregion VIEW MENU INVOKED FUNCS

        #region SPRITE MENU INVOKED FUNCS

        protected void FlipSpriteHorizontal()
        {
            ThrowIfNoActiveSprite();
            _spriteService.FlipSpriteHorizontal(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void FlipSpriteVertical()
        {
            ThrowIfNoActiveSprite();
            _spriteService.FlipSpriteVertical(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpritePixelsLeft()
        {
            ThrowIfNoActiveSprite();
            _spriteService.ShiftSpritePixelsLeft(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpritePixelsRight()
        {
            ThrowIfNoActiveSprite();
            _spriteService.ShiftSpritePixelsRight(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpritePixelsUp()
        {
            ThrowIfNoActiveSprite();
            _spriteService.ShiftSpritePixelsUp(ActiveSprite!);
            OnSpritePixelDataChanged();
        }

        protected void ShiftSpriteDown()
        {
            ThrowIfNoActiveSprite();
            _spriteService.ShiftSpritePixelsDown(ActiveSprite!);
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

        #region PALETTE MENU INVOKED FUNCS

        protected string CopyActivePaletteColourAsHex()
        {
            // Copy ActivePaletteColour as #RRGGBB
            string hex = $"#{ActivePaletteColour.R:X2}{ActivePaletteColour.G:X2}{ActivePaletteColour.B:X2}";
            Clipboard.SetText(hex);
            return hex;
        }

        protected string CopyActivePaletteColourAsRGB()
        {
            // Copy ActivePaletteColour as R,G,B
            string rgb = $"{ActivePaletteColour.R},{ActivePaletteColour.G},{ActivePaletteColour.B}";
            Clipboard.SetText(rgb);
            return rgb;
        }

        #endregion PALETTE MENU INVOKED FUNCS

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

            OnActiveSpriteChanged();
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
            _spriteService.BeginSpriteDrawOp(ActiveSprite!, startX, startY, paletteIndex);
            OnSpritePixelDataChanged();
        }

        protected virtual void ContinueSpriteDrawOp(int x, int y, int paletteIndex)
        {
            ThrowIfNoActiveSprite();
            _spriteService.SpriteDrawOp(ActiveSprite!, x, y, paletteIndex);
            OnSpritePixelDataChanged();
        }

        protected virtual void EndSpriteDrawOp()
        {
            ThrowIfNoActiveSprite();
            _spriteService.EndSpriteDrawOp(ActiveSprite!);
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

        // Called when the sprite is ready to be edited. Override this method to perform any additional setup.
        protected virtual void OnReady()
        {
        }

        protected virtual void OnZoomChanged()
        {
            bool haveSprite = ActiveSprite != null;
            mnuViewZoomIn.Enabled = haveSprite && ZoomLevel < nudZoom.Maximum;
            mnuViewZoomOut.Enabled = haveSprite && ZoomLevel > nudZoom.Minimum;

            bool oldValue = _suppressControlChangeEvents;

            _suppressControlChangeEvents = true;

            if (nudZoom.Value != ZoomLevel)
            {
                nudZoom.Value = ZoomLevel;
            }

            spriteDisplay.ZoomLevel = ZoomLevel;
            _suppressControlChangeEvents = oldValue;
            spriteDisplay.Invalidate();
        }

        protected virtual void OnPaletteChanged()
        {
            bool havePalette = Palette.Length > 1;
            mnuViewPalette.Enabled = havePalette;
            btnShowPalette.Enabled = havePalette;
        }

        protected virtual void OnClipboardChanged()
        {
            if (ActiveSprite == null)
            {
                mnuEditPaste.Enabled = false;
                return;
            }

            mnuEditPaste.Enabled = _clipboardService.HasCompatibleBitmap(ActiveSprite!);
        }

        protected virtual void OnActivePaletteIndexChanged()
        {
            //pnlPalette.SelectedPaletteIndex = ActivePaletteIndex;
        }

        protected virtual void OnColourPickerShown()
        {
            btnShowPalette.Checked = true;
        }

        protected virtual void OnColourPickerDialogHidden()
        {
            btnShowPalette.Checked = false;
        }

        protected virtual void OnActiveSpriteChanged()
        {
            bool haveSprite = ActiveSprite != null;

            // Edit menu
            mnuEditCopy.Enabled = haveSprite;
            mnuEditPaste.Enabled = haveSprite && _clipboardService.HasCompatibleBitmap(ActiveSprite!);

            // View menu
            mnuViewZoomIn.Enabled = haveSprite;
            mnuViewZoomOut.Enabled = haveSprite;
            mnuViewZoomToWindow.Enabled = haveSprite;

            // Sprite menu
            mnuSprite.Enabled = haveSprite;
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
            mnuEditUndo.Enabled = _history.CanGoBack;
            mnuEditRedo.Enabled = _history.CanGoForward;

            spriteDisplay.Invalidate();
        }

        private void DisableEditingControls()
        {
            cboSprite.Enabled = false;
            cboSprite.DataSource = null;
            cboSprite.SelectedIndex = -1;
            nudZoom.Enabled = false;
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