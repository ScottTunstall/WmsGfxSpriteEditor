using WmsGfxSpriteEditor.Controls;
using WmsGfxSpriteEditor.Palettes;
using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public partial class MainForm : Form
    {
        // Service dependencies
        private readonly IRomService _romService;
        private readonly ISpriteRenderer _spriteRenderer;
        private readonly ISpriteRepository _spriteRepository;

        // State variables
        private int _zoomLevel = 1; // Default zoom for the normal view
        private MemoryStream? _romData;
        private int _currentSpriteOffset = 0;
        private int _currentSpriteWidthInBytes = 4;
        private int _currentSpriteHeight = 8;
        private bool _currentSpriteIsLinear = true;
        private readonly Color _gridColor = Color.FromArgb(80, 80, 80);
        private Color _selectedColor = Color.Black;

        // Palette
        private readonly Color[] _palette;

        public MainForm()
        {
            InitializeComponent();

            // Initialize services
            _romService = new RobotronWDPUTieDieRomFileService();
            _spriteRenderer = new SpriteRenderer();
            _spriteRepository = new RobotronBlueLabelSpriteRepository(); // Use the Robotron sprite repository

            // Create the Robotron palette
            IPalette robotronPalette = new RobotronPalette();
            _palette = robotronPalette.GetPalette();

            spriteDisplay.SpriteRenderer = _spriteRenderer;
            spriteDisplay.Palette = _palette;
            spriteDisplay.GridColor = _gridColor;
            spriteDisplay.ZoomLevel = _zoomLevel;
            spriteDisplay.RomData = _romData;
            
            // Set the default zoom level
            nudZoom.Value = _zoomLevel;

            splitContainer.SplitterDistance = (int)(splitContainer.Width * 0.2);

            // Set up the palette panel - This MUST be done after InitializeComponent
            pnlPalette.Palette = _palette;
            pnlPalette.ColorSelected += PnlPalette_ColorSelected;

            // Update the sprite dropdown with the Robotron sprites
            UpdateSpriteDropdown();
        }

        private void PnlPalette_ColorSelected(object? sender, ColorSelectedEventArgs e)
        {
            _selectedColor = e.SelectedColor;

            // You could use this for sprite editing functionality
            StatusLabel.Text = $"Selected color: {e.ColorIndex:X} - RGB({e.SelectedColor.R},{e.SelectedColor.G},{e.SelectedColor.B})";
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

        private void RefreshSpriteDisplay()
        {
            // Update the sprite display
            if (cboSprite.SelectedItem is SpriteInfo selectedSprite)
            {
                spriteDisplay.SetSpriteInfo(selectedSprite);
                UpdateStatusWithSpriteInfo(selectedSprite);
            }
        }

        private void mnuFileLoad_Click(object sender, EventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog
            {
                Description = "Select the folder containing the ROM files",
                UseDescriptionForTitle = true
            };

            if (folderDialog.ShowDialog() != DialogResult.OK) return;

            string folderPath = folderDialog.SelectedPath;

            try
            {
                _romData = _romService.LoadRomFiles(folderPath);
                spriteDisplay.RomData = _romData;

                UpdateSpriteDropdown();

                RefreshSpriteDisplay();

                StatusLabel.Text = $"ROM files loaded successfully. Total size: 0x{_romData.Length:X} bytes";
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Missing file: {ex.FileName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ROM files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
