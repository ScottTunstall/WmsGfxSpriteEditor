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

        // State variables
        private int _zoomLevel = 1; // Default zoom for the normal view

        private MemoryStream? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);
        private Color _selectedColour = Color.Black;
        private int _selectedColourIndex;

        private bool _suspendEvents;
        private History _history = default!;

        // Palette
        private Color[] _palette = default!;

        private bool _mouseDown;

        public MainForm()
        {
            InitializeComponent();

            _suspendEvents = true;

            DisableEditingControls();

            // Set the default zoom level
            nudZoom.Value = _zoomLevel;

            splitContainer.SplitterDistance = (int)(splitContainer.Width * 0.2);

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;

            _suspendEvents = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pnlPalette.Invalidate();
        }

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

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            // Implement Undo functionality here
            MessageBox.Show("Undo action triggered.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            // Implement Redo functionality here
            MessageBox.Show("Redo action triggered.", "Redo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            // Save functionality would be implemented here
            MessageBox.Show("Save functionality not implemented in this demo.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuViewZoomIn_Click(object sender, EventArgs e)
        {
            SetZoom(_zoomLevel + 1, true);
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            SetZoom(_zoomLevel - 1, true);
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_suspendEvents)
                return;

            SetZoom((int)nudZoom.Value, true);
        }

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suspendEvents)
                return;

            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                SelectSprite(selectedSprite, true);
            }

            RefreshSpriteDisplay();
        }

        private void PnlPalette_ColorSelected(object? sender, ColourSelectedEventArgs e)
        {
            if (_suspendEvents)
                return;

            _selectedColour = e.SelectedColour;
            _selectedColourIndex = e.ColourIndex;
        }

        private void spriteDisplay_GridCellMouseDown(object sender, GridCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = true;

                // Save what sprite looked like before it was changed, to history
                SaveSelectedSpriteStateToHistory();
                
                _sprite!.SetPixelByPaletteIndex(e.GridX, e.GridY, _selectedColourIndex);

                spriteDisplay.Invalidate();
            }
        }


        private void SpriteDisplay_GridCellMouseMove(object sender, GridEventArgs e)
        {
            if (_mouseDown)
            {
                _sprite!.SetPixelByPaletteIndex(e.GridX, e.GridY, _selectedColourIndex);
                spriteDisplay.Invalidate();
            }

            CoordinatesLabel.Text = $"X: {e.GridX} Y: {e.GridY}";
        }

        private void spriteDisplay_GridCellMouseUp(object sender, GridEventArgs e)
        {
            _mouseDown = false;
        }

        private void OnBeginEdit(MemoryStream romData, IRomService romService, ISpriteRepository spriteRepository, ISpriteRenderer spriteRenderer, IPalette palette)
        {
            _history = new History();

            _romData?.Dispose();
            _romData = romData;

            _romService = romService;
            _palette = palette.GetPalette();
            pnlPalette.Palette = _palette;

            _spriteRenderer = spriteRenderer;

            IReadOnlyCollection<SpriteInfo> allSpriteInfo = spriteRepository.GetAllSprites();

            SpriteInfo firstSprite = allSpriteInfo.First();
            _sprite = CreateSpriteFromSpriteInfo(firstSprite);

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Sprite = _sprite;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = _zoomLevel;

            _suspendEvents = true;
            UpdateSpriteDropdown(allSpriteInfo);
            EnableEditingControls();
            _suspendEvents = false;
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

        private void SetZoom(int newZoomLevel, bool saveState)
        {
            if (newZoomLevel < nudZoom.Minimum || newZoomLevel > nudZoom.Maximum)
            {
                return;
            }

            if (saveState)
            {
                SaveZoomState();
            }

            _zoomLevel = newZoomLevel;
            nudZoom.Value = newZoomLevel;
            spriteDisplay.ZoomLevel = newZoomLevel;
        }

        private void SaveZoomState()
        {
            _history.Add(HistoryItem.CreateZoomHistoryItem(_zoomLevel));
        }

        
        private void SelectSprite(SpriteInfo spriteInfo, bool saveState)
        {
            if (saveState)
            {
                SaveSelectedSpriteIndexToHistory();
            }

            SetSprite(CreateSpriteFromSpriteInfo(spriteInfo));
            UpdateStatusWithSpriteInfo(spriteInfo);
        }

        private void SaveSelectedSpriteIndexToHistory()
        {
            _history.Add(HistoryItem.CreateSpriteSelectionChangingHistoryItem(cboSprite.SelectedIndex));
        }


        private void SetSprite(ISprite sprite)
        {
            _sprite = sprite;
            spriteDisplay.Sprite = sprite;
            spriteDisplay.Invalidate();
        }

        private void SaveSelectedSpriteStateToHistory()
        {
            _history.Add(HistoryItem.CreateSpriteDataChangingHistoryItem(cboSprite.SelectedIndex, _sprite.Clone()));
        }

        private void RefreshSpriteDisplay()
        {
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                UpdateStatusWithSpriteInfo(selectedSprite);
            }
        }

        private void UpdateSpriteDropdown(IReadOnlyCollection<SpriteInfo> sprites)
        {
            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = sprites;

            if (cboSprite.Items.Count > 0)
            {
                cboSprite.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Updates the status bar with complete sprite information
        /// </summary>
        private void UpdateStatusWithSpriteInfo(SpriteInfo sprite)
        {
            // Include the sprite offset in both hex and decimal format
            StatusLabel.Text = $"Sprite: {sprite.Name} | Offset: 0x{sprite.Offset:X4} ({sprite.Offset}) | " +
                               $"Size: {sprite.WidthInPixels}x{sprite.Height} pixels " +
                               $"({sprite.WidthInBytes} bytes x {sprite.Height} rows) | " +
                               $"Format: {(sprite.IsLinear ? "Linear" : "Non-linear")}";
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