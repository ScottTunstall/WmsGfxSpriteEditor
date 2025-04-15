using System.Text;
using WmsGfxSpriteEditor.Controls;
using WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared.Palettes;
using WmsGfxSpriteEditor.ROMs.Robotron.WDPUTieDie.Loader;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private IRomService _romService;
        private ISpriteRenderer _spriteRenderer;
        private ISpriteRepository _spriteRepository;

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
            
            // Set the default zoom level
            nudZoom.Value = _zoomLevel;

            splitContainer.SplitterDistance = (int)(splitContainer.Width * 0.2);

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;
        }

        private void PnlPalette_ColorSelected(object? sender, ColorSelectedEventArgs e)
        {
            _selectedColor = e.SelectedColor;

            // You could use this for sprite editing functionality
            StatusLabel.Text = $"Selected color: {e.ColorIndex:X} - RGB({e.SelectedColor.R},{e.SelectedColor.G},{e.SelectedColor.B})";
        }


        private void mnuFileLoadRobotronBlueLabel_Click(object sender, EventArgs e)
        {
            if (TryLoadRoms("Robotron Blue Label", new RobotronBlueLabelRomFileService(), new RobotronBlueLabelSpriteRepository(), new RobotronPalette()))
            {
                _palette = new RobotronPalette().GetPalette();
            }
        }
        
        private void mnuFileLoadRobotronTieDieWDPU_Click(object sender, EventArgs e)
        {
            if (TryLoadRoms("Robotron Tie Die (WDPU)", new RobotronWDPUTieDieRomFileService(), new RobotronBlueLabelSpriteRepository(), new RobotronPalette()))
            {
            }
        }

        private void mnuFileLoadRobotronTieDieMAME_Click(object sender, EventArgs e)
        {
            // Empty click handler for Tie Die (MAME) ROM
        }

        private bool TryLoadRoms(string heading, IRomService loader, ISpriteRepository spriteRepository, IPalette palette)
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder containing the {heading} ROM files";
            folderDialog.UseDescriptionForTitle = true;

            if (folderDialog.ShowDialog() != DialogResult.OK) return false;

            var directory = folderDialog.SelectedPath;
            var missingFiles = loader.GetMissingRomFiles(directory);

            if (!missingFiles.Any())
            {
                _romService = loader;
                _romData = loader.LoadRomFiles(directory);
                _spriteRepository = spriteRepository;
                _palette = palette.GetPalette();

                _spriteRenderer = new SpriteRenderer();
                spriteDisplay.SpriteRenderer = _spriteRenderer;
                spriteDisplay.Palette = _palette;
                spriteDisplay.GridColor = _gridColor;
                spriteDisplay.ZoomLevel = _zoomLevel;
                spriteDisplay.RomData = _romData;

                UpdateSpriteDropdown();

                MessageBox.Show($"Loaded {heading} ROM files successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            StringBuilder sb = new("MISSING FILES:");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendJoin(Environment.NewLine, missingFiles);

            MessageBox.Show(sb.ToString(), $"Could not load {heading} ROM files.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        private void RefreshSpriteDisplay()
        {
            // Update the sprite display
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                spriteDisplay.SetSpriteInfo(selectedSprite);
                UpdateStatusWithSpriteInfo(selectedSprite);
            }
        }

        private void UpdateSpriteDropdown()
        {
            // Update the dropdown with sprites from the repository
            cboSprite.DataSource = null;
            cboSprite.DisplayMember = "ToString";
            cboSprite.ValueMember = "Offset";
            cboSprite.DataSource = _spriteRepository.GetAllSprites().ToList();

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
                               $"Format: {(sprite.IsLinear ? "Linear" : "Non-linear")} | " +
                               $"Zoom: {_zoomLevel}x";
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshSpriteDisplay();
            pnlPalette.Invalidate();
        }
    }
}
