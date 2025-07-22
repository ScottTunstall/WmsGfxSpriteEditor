using System.Diagnostics;
using System.Runtime.InteropServices;
using WmsGfxSpriteEditor.Cursors;
using WmsGfxSpriteEditor.Dialogs;
using WmsGfxSpriteEditor.Extensions;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.Palette;
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
#pragma warning disable SA1201 // Elements should appear in the correct order
        private const int MinZoomLevel = 1;
#pragma warning restore SA1201 // Elements should appear in the correct order

        private const int MaxZoomLevel = 32;
        private const int DefaultZoomLevel = 3;

        private Color _gridColor = Color.Gray;

        // Service dependencies (I may inject these in future. No need just now.)
        protected IHistory? History { get; private set; }

        protected IRomService? RomService { get; private set; }
        protected ISpriteGridRenderer? SpriteRenderer { get; private set; }
        protected ISpriteFactory? SpriteFactory { get; private set; }
        protected ISpriteService? SpriteService { get; private set; }
        protected ISpriteClipboardService? ClipboardService { get; private set; }
        protected IPaletteClipboardService? PaletteService { get; private set; }

        // Dialogs
        private ColorPickerDialog? _colorPickerDialog;

        // Rom specific
        public bool IsRomsetLoaded { get; private set; }

        protected string RomSetName { get; private set; } = string.Empty;
        protected RomData? RomData { get; private set; }

        private bool _suppressControlChangeEvents;
        private Color[] _palette = default!;
        private IReadOnlyList<SpriteInfo> _availableSprites = [];
        private SpriteInfo? _activeSpriteInfo;
        private ISprite? _activeSprite;
        private int _zoomLevel = DefaultZoomLevel;
        private int _activePaletteIndex = -1;

        public MainForm()
        {
            InitializeComponent();

            _suppressControlChangeEvents = true;

            DisableEditingControls();

            spriteDisplay.Cursor = CrosshairCursor.CreateCrosshair(Color.White, Color.Black, 12);

            toolStripZoomTrackBar.Minimum = MinZoomLevel;
            toolStripZoomTrackBar.Maximum = MaxZoomLevel;
            toolStripZoomTrackBar.Value = ZoomLevel;

            _ = AddClipboardFormatListener(Handle);

            _suppressControlChangeEvents = false;
        }

        // This code will need to be refactored. The individual sections defined by regions will need to be extracted to separate classes.
        // For now - disable the warning about ordering of elements
#pragma warning disable SA1202 // Elements should be ordered by access

        // User selections

        // Palette
        protected Color[] ActivePalette
        {
            get => _palette;
            private set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentException($"{nameof(ActivePalette)} must have at least 2 colours.", nameof(value));
                }

                _palette = value;
                OnActivePaletteChanged();
            }
        }

        protected int ActivePaletteIndex
        {
            get => _activePaletteIndex;
            set
            {
                if (ActivePalette.Length == 0)
                {
                    throw new InvalidOperationException($"{nameof(ActivePalette)} must be initialised before {nameof(ActivePaletteIndex)} can be set.");
                }

                if (value < 0 || value >= ActivePalette.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Active palette index must be between 0 and {ActivePalette.Length - 1}.");
                }

                if (value != _activePaletteIndex)
                {
                    _activePaletteIndex = value;
                    OnActivePaletteIndexChanged();
                }
            }
        }

        protected Color ActivePaletteColour { get; private set; } = Color.Black;

        /// <summary>
        /// The list of sprites that can be selected from the sprite dropdown.
        /// </summary>
        protected IReadOnlyList<SpriteInfo> AvailableSprites
        {
            get => _availableSprites;
            set
            {
                if (value.Count == 0)
                {
                    throw new ArgumentException($"{nameof(value)} must not be an empty collection.");
                }

                _availableSprites = value;
                OnAvailableSpritesChanged();
            }
        }

        /// <summary>
        /// Metadata for the currently selected sprite
        /// </summary>
        protected SpriteInfo? ActiveSpriteInfo
        {
            get => _activeSpriteInfo;
            set
            {
                if (value != _activeSpriteInfo)
                {
                    _activeSpriteInfo = value;
                    OnActiveSpriteInfoChanged();
                }
            }
        }

        /// <summary>
        /// Manifestation of the currently selected sprite, constructed from ROM data.
        /// </summary>
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
            set
            {
                if (value is < MinZoomLevel or > MaxZoomLevel)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Zoom level must be between {MinZoomLevel} and {MaxZoomLevel}.");
                }

                if (value != _zoomLevel)
                {
                    _zoomLevel = value;
                    OnZoomLevelChanged();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WindowsMessages.WM_CLIPBOARDUPDATE)
            {
                OnClipboardChanged();
            }

            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!IsRomsetLoaded)
            {
                base.OnFormClosing(e);
                return;
            }

            DialogResult result = MessageBox.Show(
                    "Are you sure you want to close the application? Any unsaved work will be lost.",
                    "Confirm Exit",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

            if (result != DialogResult.Yes)
            {
                e.Cancel = true; // Cancel the close operation
            }

            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ = RemoveClipboardFormatListener(Handle);
            base.OnFormClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            spriteDisplay.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }

                if (_colorPickerDialog != null && !_colorPickerDialog.IsDisposed)
                {
                    _colorPickerDialog.Dispose();
                }
                _colorPickerDialog = null;

                if (RomData != null)
                {
                    RomData.Dispose();
                    RomData = null;
                }
            }
            base.Dispose(disposing);
        }

#pragma warning disable IDE1006 // Element should begin with upper-case letter
#pragma warning disable SA1124 // Do not use regions
#pragma warning disable SA1300 // Element should begin with upper-case letter

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
            PasteSprite();
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

        #endregion VIEW MENU EVENT HANDLERS

        #region SPRITE MENU EVENT HANDLERS

        private void mnuSpriteGotoNextSprite_Click(object sender, EventArgs e)
        {
            GotoNextSprite();
        }

        private void mnuSpriteGoToPreviousSprite_Click(object sender, EventArgs e)
        {
            GotoPreviousSprite();
        }

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
            ShowAboutDialog();
        }

        #endregion HELP MENU EVENT HANDLERS

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
            {
                return;
            }

            SelectActiveSpriteByIndex(cboSprite.SelectedIndex, false);
        }

        #endregion SPRITE COMBO BOX EVENT HANDLERS

        #region TOOLSTRIP EVENT HANDLERS

        private void btnShowPalette_Click(object sender, EventArgs e)
        {
            ToggleColourPickerDialog();
        }

        #endregion TOOLSTRIP EVENT HANDLERS

        #region MAGNIFICATION PANEL EVENT HANDLERS

        private void MagnificationPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (ActiveSprite == null)
            {
                return;
            }

            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {
                return;
            }

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

        #endregion MAGNIFICATION PANEL EVENT HANDLERS

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

        #region STATUS BAR EVENT HANDLERS

        private void toolStripTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressControlChangeEvents)
            {
                return;
            }

            ZoomLevel = toolStripZoomTrackBar.Value;
        }

        #endregion STATUS BAR EVENT HANDLERS

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
            string? directory = new SaveRomDialog().BrowseForFolder(RomSetName);
            if (directory == null)
            {
                return;
            }

            RomService!.SaveRomData(RomData!, directory);

            new InformationDialog().ShowDialog($"Saved {RomSetName} ROM files successfully.", "Success", this);
        }

        protected virtual void OnBeginEdit(string romSetName, RomData romData, SpriteEditorDependencies editorDependencies)
        {
            _suppressControlChangeEvents = true;

            RomSetName = romSetName;

            RomData?.Dispose();
            RomData = romData;

            History = new History.History();
            SpriteService = new SpriteService(History);
            ClipboardService = new DefaultSpriteClipboardService(History);
            PaletteService = new DefaultPaletteClipboardService();

            RomService = editorDependencies.RomService;
            SpriteFactory = editorDependencies.SpriteFactory;
            ActivePalette = editorDependencies.PaletteService.GetPalette();

            SpriteRenderer = editorDependencies.SpriteRenderer;
            spriteDisplay.SpriteRenderer = SpriteRenderer;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = ZoomLevel;

            List<SpriteInfo> allSprites = [.. editorDependencies.SpriteRepository.GetAllSprites()];
            AvailableSprites = allSprites;

            _suppressControlChangeEvents = false;
            IsRomsetLoaded = true;

            OnReadyToEdit();
        }

        #endregion FILE MENU INVOKED FUNCS

        #region EDIT MENU INVOKED FUNCS

        protected void Undo()
        {
            ThrowIfNoHistory();

            if (!History!.CanGoBack)
            {
                throw new InvalidOperationException("No history item to undo.");
            }

            HistoryItem? item = History.Back();
            SetStateFromHistory(item!);
        }

        protected void Redo()
        {
            ThrowIfNoHistory();

            if (!History!.CanGoForward)
            {
                throw new InvalidOperationException("No history item to redo.");
            }

            HistoryItem item = History.Forward()!;
            SetStateFromHistory(item!);
        }

        protected void CopySpriteToClipboard()
        {
            ThrowIfNoActiveSprite();

            ClipboardService!.Copy(ActiveSprite!);
        }

        protected void PasteSprite()
        {
            ThrowIfNoActiveSprite();

            ClipboardService!.Paste(ActiveSprite!);
            OnSpritePixelsMaybeChanged();
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
                OnColourPickerDialogShown();
            }
            else
            {
                _colorPickerDialog = new ColorPickerDialog()
                {
                    Palette = ActivePalette,
                    SelectedPaletteIndex = ActivePaletteIndex,
                    StartPosition = FormStartPosition.Manual,
                };

                _colorPickerDialog.ShrinkToFit();

                _colorPickerDialog.Location = new Point(
                    Location.X + ((Width - _colorPickerDialog.Width) / 2),
                    Location.Y + ((Height - _colorPickerDialog.Height) / 2)
                );

                _colorPickerDialog.SelectedColorChanged += (s, args) =>
                {
                    ActivePaletteIndex = _colorPickerDialog.SelectedPaletteIndex;
                };

                _colorPickerDialog.Shown += (s, args) =>
                {
                    OnColourPickerDialogShown();
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

        protected void GotoNextSprite()
        {
            ThrowIfNoAvailableSprites();

            if (ActiveSpriteInfo!.Index < AvailableSprites.Count - 1)
            {
                SelectActiveSpriteByIndex(ActiveSpriteInfo.Index + 1);
            }
        }

        protected void GotoPreviousSprite()
        {
            ThrowIfNoAvailableSprites();

            if (ActiveSpriteInfo!.Index > 0)
            {
                SelectActiveSpriteByIndex(ActiveSpriteInfo!.Index - 1);
            }
        }

        protected void FlipSpriteHorizontal()
        {
            ThrowIfNoActiveSprite();
            SpriteService!.FlipSpriteHorizontal(ActiveSprite!);
            OnSpritePixelsMaybeChanged();
        }

        protected void FlipSpriteVertical()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.FlipSpriteVertical(ActiveSprite!);

            OnSpritePixelsMaybeChanged();
        }

        protected void ShiftSpritePixelsLeft()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.ShiftSpritePixelsLeft(ActiveSprite!);

            OnSpritePixelsMaybeChanged();
        }

        protected void ShiftSpritePixelsRight()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.ShiftSpritePixelsRight(ActiveSprite!);

            OnSpritePixelsMaybeChanged();
        }

        protected void ShiftSpritePixelsUp()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.ShiftSpritePixelsUp(ActiveSprite!);

            OnSpritePixelsMaybeChanged();
        }

        protected void ShiftSpriteDown()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.ShiftSpritePixelsDown(ActiveSprite!);

            OnSpritePixelsMaybeChanged();
        }

        #endregion SPRITE MENU INVOKED FUNCS

        #region PALETTE MENU INVOKED FUNCS

        protected string CopyActivePaletteColourAsHex()
        {
            ThrowIfNoPaletteService();

            string hex = PaletteService!.CopyAsHexString(ActivePaletteColour);
            return hex;
        }

        protected string CopyActivePaletteColourAsRGB()
        {
            ThrowIfNoPaletteService();
            string rgb = PaletteService!.CopyAsRGBString(ActivePaletteColour);
            return rgb;
        }

        #endregion PALETTE MENU INVOKED FUNCS

        #region HELP MENU INVOKED FUNCS

        private void ShowAboutDialog()
        {
            new AboutDialog().ShowDialog(this);
        }

        #endregion HELP MENU INVOKED FUNCS

        #region SPRITE SELECT COMBO BOX INVOKED FUNCS

        protected void SelectActiveSpriteByIndex(int index, bool syncControls = true)
        {
            ActiveSpriteInfo = AvailableSprites[index];

            if (syncControls)
            {
                SetSpriteSelectComboBox(index);
            }
        }

        private void SetSpriteSelectComboBox(int index = 0)
        {
            bool oldValue = _suppressControlChangeEvents;
            _suppressControlChangeEvents = true;
            cboSprite.SelectedIndex = index;
            _suppressControlChangeEvents = oldValue;
        }

        #endregion SPRITE SELECT COMBO BOX INVOKED FUNCS

        #region SPRITE GRID INVOKED FUNCS

        protected virtual void BeginSpriteDrawOp(int startX, int startY, int paletteIndex)
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.BeginSpriteDrawOp(ActiveSprite!, startX, startY, paletteIndex);
            OnSpritePixelsMaybeChanged();
        }

        protected virtual void ContinueSpriteDrawOp(int x, int y, int paletteIndex)
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.SpriteDrawOp(ActiveSprite!, x, y, paletteIndex);
            OnSpritePixelsMaybeChanged();
        }

        protected virtual void EndSpriteDrawOp()
        {
            ThrowIfNoActiveSprite();
            ThrowIfNoSpriteService();

            SpriteService!.EndSpriteDrawOp(ActiveSprite!);
            OnSpritePixelsMaybeChanged();
        }

        #endregion SPRITE GRID INVOKED FUNCS

        #region HISTORY

        protected virtual void SetStateFromHistory(HistoryItem item)
        {
            if (item.OperationType == OperationType.SpritePixelDataSnapshot)
            {
                RestoreSpriteFromHistoryItem(item);
            }
        }

        private void RestoreSpriteFromHistoryItem(HistoryItem item)
        {
            int offset = AvailableSprites[item.SpriteIndex].Offset;

            new UpdateRomDataFromPixelDataCommand(RomData!).Execute(offset, item.PixelData!);
            SelectActiveSpriteByIndex(item.SpriteIndex, true);
            OnSpritePixelsMaybeChanged();
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

        // Called when the ROM is loaded and sprites can be edited. Override this method to perform any additional setup.
        protected virtual void OnReadyToEdit()
        {
            mnuFileSave.Enabled = true;
            cboSprite.Enabled = true;
            spriteDisplay.Visible = true;
        }

        protected virtual void OnClipboardChanged()
        {
            if (!IsRomsetLoaded)
            {
                return;
            }

            if (ActiveSprite == null)
            {
                mnuEditPaste.Enabled = false;
                return;
            }

            // If we have an active sprite but no clipboard service, something has gone wrong in the app.
            ThrowIfNoClipboardService();

            mnuEditPaste.Enabled = ClipboardService!.HasCompatibleBitmap(ActiveSprite!);
        }

        protected virtual void OnActivePaletteChanged()
        {
            ThrowIfNoActivePalette();

            mnuViewPalette.Enabled = true;
            btnShowPalette.Enabled = true;

            ActivePaletteIndex = 0;
        }

        protected virtual void OnActivePaletteIndexChanged()
        {
            ThrowIfNoActivePalette();
            ThrowIfActivePaletteIndexInvalid();

            mnuCopySelectedColour.Enabled = true;
            mnuCopySelectedColourHex.Enabled = true;
            mnuCopySelectedColourRgb.Enabled = true;

            ActivePaletteColour = ActivePalette[ActivePaletteIndex];
        }

        protected virtual void OnColourPickerDialogShown()
        {
            ThrowIfNoActivePalette();

            btnShowPalette.Checked = true;
        }

        protected virtual void OnColourPickerDialogHidden()
        {
            ThrowIfNoActivePalette();

            btnShowPalette.Checked = false;
        }

        protected virtual void OnAvailableSpritesChanged()
        {
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = AvailableSprites;
            cboSprite.Enabled = AvailableSprites.Count > 0;
            SelectActiveSpriteByIndex(0, true); // safety measure to ensure we have a valid sprite selected
        }

        protected virtual void OnActiveSpriteInfoChanged()
        {
            bool haveSpriteInfo = ActiveSpriteInfo != null;
            int spriteIndex = haveSpriteInfo ? ActiveSpriteInfo!.Index : -1;

            // Sprite menu
            mnuSpriteGoToPreviousSprite.Enabled = spriteIndex > 0;
            mnuSpriteGotoNextSprite.Enabled = spriteIndex < AvailableSprites.Count - 1;
            mnuSprite.Enabled = mnuSprite.DropDownItems.Any(item => item.Enabled);

            if (haveSpriteInfo)
            {
                ActiveSprite = CreateSpriteFromRomData();
            }
            else
            {
                ActiveSprite = null;
            }

            UpdateStatusBarSpriteInfo();
        }

        protected virtual void OnActiveSpriteChanged()
        {
            bool haveSprite = ActiveSprite != null;

            // Edit menu
            mnuEditCopy.Enabled = haveSprite;
            mnuCopySprite.Enabled = haveSprite;
            mnuEditPaste.Enabled = haveSprite && ClipboardService!.HasCompatibleBitmap(ActiveSprite!);
            mnuEdit.Enabled = mnuEdit.DropDownItems.Any(item => item.Enabled);

            // View menu
            mnuViewZoomIn.Enabled = haveSprite && ZoomLevel < MaxZoomLevel;
            mnuViewZoomOut.Enabled = haveSprite && ZoomLevel > MinZoomLevel;
            mnuViewZoomToWindow.Enabled = haveSprite;
            mnuView.Enabled = mnuView.DropDownItems.Any(item => item.Enabled);

            // Sprite menu
            mnuSpriteFlipHorizontal.Enabled = haveSprite;
            mnuSpriteFlipVertical.Enabled = haveSprite;
            mnuSpriteShiftUp.Enabled = haveSprite;
            mnuSpriteShiftDown.Enabled = haveSprite;
            mnuSpriteShiftLeft.Enabled = haveSprite;
            mnuSpriteShiftRight.Enabled = haveSprite;
            mnuSprite.Enabled = mnuSprite.DropDownItems.Any(item => item.Enabled);

            // Trackbar zoom control
            toolStripZoomTrackBar.Enabled = haveSprite;
            btnZoomIn.Enabled = haveSprite && ZoomLevel < MaxZoomLevel;
            btnZoomOut.Enabled = haveSprite && ZoomLevel > MinZoomLevel;

            // Coordinate label on status bar
            CoordinatesLabel.Enabled = haveSprite;

            // Zoom label on status bar
            ZoomLabel.Enabled = haveSprite;
            ZoomLabel.Text = haveSprite ? $"{ZoomLevel}x" : string.Empty;

            // Sprite grid
            spriteDisplay.Sprite = ActiveSprite;
            spriteDisplay.Visible = haveSprite;
        }

        /// <summary>
        /// Called when the sprite pixel data may have changed.
        /// </summary>
        protected virtual void OnSpritePixelsMaybeChanged()
        {
            ThrowIfNoHistory();

            mnuEditUndo.Enabled = History!.CanGoBack;
            mnuEditRedo.Enabled = History.CanGoForward;

            spriteDisplay.Invalidate();
        }

        protected virtual void OnZoomLevelChanged()
        {
            ThrowIfNoActiveSprite();

            // Zoom menu items
            mnuViewZoomIn.Enabled = ZoomLevel < MaxZoomLevel;
            mnuViewZoomOut.Enabled = ZoomLevel > MinZoomLevel;

            // Zoom buttons on status strip
            btnZoomIn.Enabled = ZoomLevel < MaxZoomLevel;
            btnZoomOut.Enabled = ZoomLevel > MinZoomLevel;

            bool oldValue = _suppressControlChangeEvents;

            _suppressControlChangeEvents = true;

            if (toolStripZoomTrackBar.Value != ZoomLevel)
            {
                toolStripZoomTrackBar.Value = ZoomLevel;
            }

            ZoomLabel.Text = $"{ZoomLevel}x";

            spriteDisplay.ZoomLevel = ZoomLevel;
            _suppressControlChangeEvents = oldValue;
            spriteDisplay.Invalidate();
        }

        private void DisableEditingControls()
        {
            cboSprite.Enabled = false;
            cboSprite.DataSource = null;
            cboSprite.SelectedIndex = -1;

            spriteDisplay.Visible = false;
        }

        #region ROM

        /// <summary>
        /// Create a Sprite to render from the sprite info
        /// </summary>
        private ISprite CreateSpriteFromRomData()
        {
            return new CreateSpriteFromRomDataCommand(RomData!, SpriteFactory!).Execute(ActiveSpriteInfo!, ActivePalette);
        }

        #endregion ROM

#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore SA1202 // Elements should be ordered by access
#pragma warning restore SA1124 // Do not use regions
#pragma warning restore IDE1006 // Element should begin with upper-case letter
#pragma warning restore SA1202 // Elements should be ordered by access

        // The properties below in the Throw() methods should sanity check themselves for correct values.
        // When I'm developing, I may miss something so these give me peace of mind.
        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoHistory()
        {
            if (History == null)
            {
                throw new InvalidOperationException($"{nameof(History)} is null.");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoClipboardService()
        {
            if (ClipboardService == null)
            {
                throw new InvalidOperationException($"{nameof(ClipboardService)} is null.");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoPaletteService()
        {
            if (PaletteService == null)
            {
                throw new InvalidOperationException($"{nameof(PaletteService)} is null.");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoSpriteService()
        {
            if (SpriteService == null)
            {
                throw new InvalidOperationException($"{nameof(SpriteService)} is null.");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoAvailableSprites()
        {
            if (AvailableSprites.Count == 0)
            {
                throw new InvalidOperationException("No sprites available to select.");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoActiveSprite()
        {
            if (ActiveSprite == null)
            {
                throw new InvalidOperationException("Operation cannot be performed without an active sprite");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfNoActivePalette()
        {
            if (ActivePalette.Length < 2)
            {
                throw new InvalidOperationException("Operation cannot be performed without an active palette");
            }
        }

        [Conditional("DEBUG")]
        [Conditional("PRODBUGFIX")]
        protected void ThrowIfActivePaletteIndexInvalid()
        {
            if (ActivePaletteIndex < 0 || ActivePaletteIndex >= ActivePalette.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(ActivePaletteIndex), "Active palette index is out of range.");
            }
        }
    }
}