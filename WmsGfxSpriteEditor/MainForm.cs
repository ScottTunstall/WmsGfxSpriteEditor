using System.Text;
using WmsGfxSpriteEditor.Controls;
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

        private MemoryStream? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);

        // User selections
        private Color _selectedColour = Color.Black;

        private bool _haveSpritesToSelect;
        private int _selectedPaletteIndex;
        private SpriteInfo? _selectedSpriteInfo;
        private int _selectedSpriteIndex;
        private int _zoomLevel = 1; // Default zoom for the normal view

        private bool _suspendChangeEvents;
        private History.History _history = new();

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

            MemoryStream? romData = LoadRomSetIntoMemoryStream(RomSetNames.BlueLabel, service);
            if (romData != null)
            {
                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();
                SpriteRenderer spriteRenderer = new();

                OnBeginEdit(romData, service, repo, spriteRenderer, palette);
            }
        }

        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            RobotronBlueLabelRomFileService service = new();

            MemoryStream? romData = LoadRomSetIntoMemoryStream(RomSetNames.TieDieWDPU, service);
            if (romData != null)
            {
                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();
                SpriteRenderer spriteRenderer = new();

                OnBeginEdit(romData, service, repo, spriteRenderer, palette);
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

        #endregion


        #region EDIT MENU EVENT HANDLERS

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        #endregion


        #region VIEW MENU EVENT HANDLERS

        private void mnuViewZoomIn_Click(object sender, EventArgs e)
        {
            if (_zoomLevel <= nudZoom.Maximum)
            {
                SetZoom(_zoomLevel + 1, true);
                OnDisplayStateChanged();
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


        #endregion

        #region SPRITE COMBO BOX EVENT HANDLERS

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SelectSprite(cboSprite.SelectedItem as SpriteInfo, cboSprite.SelectedIndex, true);
        }

        #endregion


        #region PALETTE CONTROL EVENT HANDLERS
        private void PnlPalette_ColorSelected(object? sender, ColourSelectedEventArgs e)
        {
            if (_suspendChangeEvents)
                return;

            SelectPalette(e.SelectedColour, e.ColourIndex);
        }

        #endregion


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

        #endregion

        private void OnBeginEdit(MemoryStream romData, IRomService romService, ISpriteRepository spriteRepository, ISpriteRenderer spriteRenderer, IPalette palette)
        {
            _history.Clear();

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _palette = palette.GetPalette();
            pnlPalette.Palette = _palette;

            _spriteRenderer = spriteRenderer;

            IReadOnlyCollection<SpriteInfo> allSpriteInfo = spriteRepository.GetAllSprites();
            _haveSpritesToSelect = allSpriteInfo.Any();

            _suspendChangeEvents = true;
            UpdateSpriteDropdown(allSpriteInfo);
            _suspendChangeEvents = false;
            
            SpriteInfo firstSprite = allSpriteInfo.First();
            _sprite = CreateSpriteFromSpriteInfo(firstSprite);

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

        private void EnableEditingControls()
        {
            cboSprite.Enabled = true;
            nudZoom.Enabled = true;
            pnlPalette.Enabled = true;
        }

        #region PALETTE FUNCS

        private void SelectPalette(Color selectedColour, int colourIndex)
        {
            _selectedColour = selectedColour;
            _selectedPaletteIndex = colourIndex;
            OnDisplayStateChanged();
        }


        #endregion



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

        #endregion


        #region VIEW FUNCS

        private void SetZoom(int newZoomLevel, bool saveStateToHistory)
        {
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


        #endregion 

        #region SPRITE FUNCS

        private void UpdateSpriteDropdown(IReadOnlyCollection<SpriteInfo> sprites)
        {
            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = sprites;

            _haveSpritesToSelect = sprites.Count > 0;
            if (_haveSpritesToSelect)
            {
                cboSprite.SelectedIndex = 0;
                _selectedSpriteIndex = 0;
                _selectedSpriteInfo = (SpriteInfo)cboSprite.Items[0]!;
            }

            OnDisplayStateChanged();
        }

        #endregion 

        private void SelectSprite(SpriteInfo? spriteInfo, int spriteIndex, bool saveStateToHistory)
        {
            _selectedSpriteInfo = spriteInfo;
            _selectedSpriteIndex = spriteIndex;

            if (saveStateToHistory)
            {
                SaveSelectedSpriteIndexToHistory();
            }

            if (spriteInfo != null)
            {
                SetSpriteDisplay(CreateSpriteFromSpriteInfo(spriteInfo));
            }
            else
            {
                SetSpriteDisplay(null);
            }

            UpdateStatusBarWithSpriteInfo(spriteInfo);
            OnDisplayStateChanged();
        }


        private void SetSpriteDisplay(ISprite? sprite)
        {
            _sprite = sprite;
            spriteDisplay.Sprite = sprite;
            spriteDisplay.Invalidate();
            OnDisplayStateChanged();
        }


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
            mnuEditUndo.Enabled = _sprite!=null && _history.CanGoBack;
            mnuEditRedo.Enabled = _sprite != null && _history.CanGoForward;
            mnuViewZoomIn.Enabled = _sprite != null && _zoomLevel < nudZoom.Maximum;
            mnuViewZoomOut.Enabled = _sprite != null && _zoomLevel > nudZoom.Minimum;

            cboSprite.Enabled = _haveSpritesToSelect;
            nudZoom.Enabled = _sprite != null;
            pnlPalette.Enabled = _sprite != null;
            spriteDisplay.Visible = _sprite!= null;
        }
        
        

        /// <summary>
        /// Create a Sprite to render from the sprite info
        /// </summary>
        private ISprite CreateSpriteFromSpriteInfo(SpriteInfo spriteInfo)
        {
            int bytesToRead = spriteInfo.WidthInBytes * spriteInfo.Height;
            byte[] spriteData = new byte[bytesToRead];
            _romData!.Position = spriteInfo.Offset;
            _ = _romData!.Read(spriteData, 0, bytesToRead);

            // TODO: Move into factory method in Sprite4Bpp class
            return new Sprite4Bpp(spriteData, _palette, spriteInfo.WidthInBytes, spriteInfo.Height, spriteInfo.IsLinear);
        }

        private void RestoreSprite(SpriteInfo spriteInfo, byte[] spriteData, Color[] palette)
        {
            _romData!.Position = spriteInfo.Offset;
            _romData!.Write(spriteData, 0, spriteData.Length);
            _palette = palette;
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
            _history.Add(HistoryItem.CreateSpriteDataChangingHistoryItem(_selectedSpriteInfo!, _sprite!, _selectedSpriteIndex));
        }

        private void SetStateFromHistory(HistoryItem item)
        {
            switch (item.OperationType)
            {
                case OperationType.Zoom:
                    SetZoom((int)item.ZoomValue, false);
                    break;

                case OperationType.SpriteSelectionChanging:
                    SelectSprite(item.SpriteInfo!, item.SpriteIndex, false);
                    break;

                case OperationType.SpriteDataChanging:
                    //RestoreSprite(item.SpriteInfo!, item.SpriteData!, item.Palette!);
                    break;
            }
        }

        #endregion




#pragma warning disable CA1859

        private MemoryStream? LoadRomSetIntoMemoryStream(string romsetName, IRomService loader)
#pragma warning restore CA1859
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder containing the {romsetName} ROM files";
            folderDialog.UseDescriptionForTitle = true;

            if (folderDialog.ShowDialog() != DialogResult.OK) return null;

            string directory = folderDialog.SelectedPath;
            string[] missingFiles = loader.GetMissingRomFiles(directory);

            if (missingFiles.Length > 0)
            {
                StringBuilder sb = new("MISSING FILES:");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.AppendJoin(Environment.NewLine, missingFiles);

                MessageBox.Show(sb.ToString(), $"Could not load {romsetName} ROM files.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            // Free previous ROM data
            MemoryStream? romData = loader.LoadRomFiles(directory);

            MessageBox.Show($"Loaded {romsetName} ROM files successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return romData;
        }
    }
}