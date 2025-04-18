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
        private IRomService _romService = default!;
        private ISpriteGridRenderer _spriteRenderer = default!;

        // State variables
        private int _zoomLevel = 1; // Default zoom for the normal view
        private MemoryStream? _romData;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);
        private Color _selectedColor = Color.Black;

        // Palette
        private Color[] _palette;

        public MainForm()
        {
            InitializeComponent();
            DisableEditingControls();
            
            // Set the default zoom level
            nudZoom.Value = _zoomLevel;

            splitContainer.SplitterDistance = (int)(splitContainer.Width * 0.2);

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;
        }


        private void mnuFileLoadRobotronBlueLabel_Click(object sender, EventArgs e)
        {
            RobotronBlueLabelRomFileService service = new();

            MemoryStream? romData = LoadRomSetIntoMemoryStream(RomSetNames.BlueLabel, service);
            if (romData != null)
            {
                _romData?.Dispose();
                _romData = romData;

                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();

                OnBeginEdit(service, repo, palette);
            }
        }
        
        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            RobotronBlueLabelRomFileService service = new();

            var romData = LoadRomSetIntoMemoryStream(RomSetNames.TieDieWDPU, service);
            if (romData!=null)
            {
                RobotronBlueLabelSpriteRepository repo = new();
                RobotronPalette palette = new();

                OnBeginEdit(service, repo, palette);
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

        private void mnuViewZoomIn_Click(object sender, EventArgs e)
        {
            if (nudZoom.Value < nudZoom.Maximum)
            {
                nudZoom.Value++;
            }
        }

        private void mnuViewZoomOut_Click(object sender, EventArgs e)
        {
            if (nudZoom.Value > nudZoom.Minimum)
            {
                nudZoom.Value--;
            }
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            _zoomLevel = (int)nudZoom.Value;
            spriteDisplay.ZoomLevel = _zoomLevel;

            // Update status bar with zoom level if a sprite is selected
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                UpdateStatusWithSpriteInfo(selectedSprite);
            }
        }

        private void cboSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                UpdateStatusWithSpriteInfo(selectedSprite);
            }

            RefreshSpriteDisplay();
        }

        private void PnlPalette_ColorSelected(object? sender, ColourSelectedEventArgs e)
        {
            _selectedColor = e.SelectedColour;
        }


        private void SpriteDisplay_GridCellMouseMove(object sender, GridEventArgs e)
        {
            CoordinatesLabel.Text = $"X: {e.GridX} Y: {e.GridY}";
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshSpriteDisplay();
            pnlPalette.Invalidate();
        }


        private void OnBeginEdit(IRomService romService, ISpriteRepository spriteRepository, IPalette palette)
        {
            _romService = romService;
            _palette = palette.GetPalette();
            pnlPalette.Palette = _palette;

            _spriteRenderer = new SpriteGridRenderer();
            spriteDisplay.SpriteGridRenderer = _spriteRenderer;
            spriteDisplay.Palette = _palette;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = _zoomLevel;
            spriteDisplay.RomData = _romData;

            UpdateSpriteDropdown(spriteRepository.GetAllSprites());
            EnableEditingControls();
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


        private void RefreshSpriteDisplay()
        {
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                spriteDisplay.SetSpriteInfo(selectedSprite);
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
