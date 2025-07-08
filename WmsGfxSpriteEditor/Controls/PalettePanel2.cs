using System.ComponentModel;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Custom panel control that renders a palette of colors as buttons within a ToolStrip
    /// </summary>
    [Designer(typeof(System.Windows.Forms.Design.ParentControlDesigner))]
    [DesignerCategory("Code")]
    [DefaultEvent("ColorSelected")]
    [DefaultProperty("Palette")]
    [ToolboxItem(true)]
    public class PalettePanel2 : Panel
    {
        // Constants for UI configuration
        private const int DefaultButtonSize = 24;
        private const int DefaultButtonMargin = 1;
        private const int DefaultToolStripPadding = 0;

        private Color[] _palette = [];
        private ToolStrip _toolStrip = null!;
        private int _selectedColorIndex = -1;

        /// <summary>
        /// Event raised when a color in the palette is selected
        /// </summary>
        [Category("Action")]
        [Description("Occurs when a color in the palette is selected")]
        public event EventHandler<ColourSelectedEventArgs>? ColorSelected;

        /// <summary>
        /// Gets or sets the palette used by this control
        /// </summary>
        [Category("Appearance")]
        [Description("The color palette displayed by this control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value ?? [];
                RefreshPalette();
            }
        }

        /// <summary>
        /// Gets or sets the index of the selected color in the palette
        /// </summary>
        [Category("Appearance")]
        [Description("The index of the currently selected color")]
        [DefaultValue(-1)]
        public int SelectedColorIndex
        {
            get => _selectedColorIndex;
            set
            {
                if (value > -1 && _palette.Length == 0)
                {
                    throw new ArgumentException("SelectedColorIndex cannot be set to a positive integer when the palette is empty.", nameof(value));
                }

                if (value < -1 || value >= _palette.Length)
                {
                    throw new ArgumentException($"SelectedColorIndex must be between -1 and {_palette.Length - 1} (inclusive), but was {value}.", nameof(value));
                }

                _selectedColorIndex = value;
                UpdateSelectionDisplay();
            }
        }

        /// <summary>
        /// Initializes a new instance of the PalettePanel2 class
        /// </summary>
        public PalettePanel2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the component
        /// </summary>
        private void InitializeComponent()
        {
            // Set default properties
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.DimGray;

            // Create ToolStrip
            _toolStrip = new ToolStrip
            {
                Dock = DockStyle.Fill,
                LayoutStyle = ToolStripLayoutStyle.Flow,
                BackColor = Color.DimGray,
                GripStyle = ToolStripGripStyle.Hidden,
                RenderMode = ToolStripRenderMode.System,
                AutoSize = true,
                Padding = new Padding(DefaultToolStripPadding)
            };

            // Add ToolStrip to panel
            Controls.Add(_toolStrip);
        }

        /// <summary>
        /// Refreshes the palette display
        /// </summary>
        private void RefreshPalette()
        {
            _toolStrip.Items.Clear();

            if (_palette.Length == 0)
            {
                return;
            }

            for (int i = 0; i < _palette.Length; i++)
            {
                Color color = _palette[i];
                ToolStripButton button = new()
                {
                    DisplayStyle = ToolStripItemDisplayStyle.None,
                    BackColor = color,
                    Size = new Size(DefaultButtonSize, DefaultButtonSize),
                    Margin = new Padding(DefaultButtonMargin),
                    Tag = i, // Store the color index
                    AutoSize = false
                };

                // Set tooltip with RGB values
                string hexValue = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                string decimalValue = $"R:{color.R}, G:{color.G}, B:{color.B}";
                string toolTipText = $"{hexValue}\n{decimalValue}";
                button.ToolTipText = toolTipText;

                // Handle click event
                button.Click += ColorButton_Click;

                // Handle mouse events for custom tooltip and cursor changes
                button.MouseEnter += ColorButton_MouseEnter;
                button.MouseLeave += ColorButton_MouseLeave;

                _ = _toolStrip.Items.Add(button);
            }

            UpdateSelectionDisplay();
        }

        /// <summary>
        /// Handles mouse enter event for color buttons
        /// </summary>
        private void ColorButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is ToolStripButton button && button.Tag != null)
            {
                // Change cursor to hand to indicate clickable
                Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// Handles mouse leave event for color buttons
        /// </summary>
        private void ColorButton_MouseLeave(object? sender, EventArgs e)
        {
            // Reset cursor to default
            Cursor = Cursors.Default;

        }

        /// <summary>
        /// Handles click event for color buttons
        /// </summary>
        private void ColorButton_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripButton { Tag: not null } button)
            {
                int colorIndex = (int)button.Tag;
                Color color = _palette[colorIndex];

                _selectedColorIndex = colorIndex;
                UpdateSelectionDisplay();

                // Raise the color selected event
                OnColorSelected(new ColourSelectedEventArgs(color, colorIndex));
            }
        }

        /// <summary>
        /// Updates the selection display
        /// </summary>
        private void UpdateSelectionDisplay()
        {
            foreach (ToolStripButton item in _toolStrip.Items.OfType<ToolStripButton>())
            {
                if (item.Tag != null)
                {
                    int colorIndex = (int)item.Tag;
                    item.Checked = colorIndex == _selectedColorIndex;
                }
            }
        }

        /// <summary>
        /// Raises the ColorSelected event
        /// </summary>
        protected virtual void OnColorSelected(ColourSelectedEventArgs e)
        {
            ColorSelected?.Invoke(this, e);
        }

        /// <summary>
        /// Clean up any resources being used
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolStrip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}