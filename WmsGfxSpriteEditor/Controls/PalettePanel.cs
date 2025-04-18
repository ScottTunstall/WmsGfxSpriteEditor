using System.ComponentModel;
using WmsGfxSpriteEditor.Palettes;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Custom panel for displaying and selecting colors from a palette
    /// </summary>
 
    /// <summary>
    /// Custom panel for displaying and selecting colors from a palette
    /// </summary>
    [Designer(typeof(System.Windows.Forms.Design.ParentControlDesigner))]
    [DesignerCategory("Code")]
    [DefaultEvent("ColorSelected")]
    [DefaultProperty("Palette")]
    [ToolboxItem(true)]
    public class PalettePanel : Panel
    {
        private IPaletteRenderer _paletteRenderer;
        private Color[] _palette = Array.Empty<Color>();
        private int _selectedColorIndex;

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
                _palette = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the index of the selected color in the palette
        /// </summary>
        [Category("Appearance")]
        [Description("The index of the currently selected color")]
        [DefaultValue(0)]
        public int SelectedColorIndex
        {
            get => _selectedColorIndex;
            set
            {
                if (value >= 0 && value < _palette.Length)
                {
                    _selectedColorIndex = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the PalettePanel class
        /// </summary>
        public PalettePanel()
        {
            // Set default properties for designer support
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.DimGray;
            DoubleBuffered = true;

            // Create a default palette renderer
            _paletteRenderer = new DefaultPaletteRenderer();
        }

        /// <summary>
        /// Gets or sets the palette renderer used by this control
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPaletteRenderer PaletteRenderer
        {
            get => _paletteRenderer;
            set => _paletteRenderer = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Handles the Paint event
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_palette.Length > 0)
            {
                _paletteRenderer.RenderPalette(e.Graphics, _palette, ClientRectangle, _selectedColorIndex);
            }
        }

        /// <summary>
        /// Handles the MouseClick event
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (_palette.Length > 0)
            {
                // Ask the renderer which color index corresponds to the clicked position
                (int colorIndex, bool isValid) = _paletteRenderer.GetColorIndexAt(e.X, e.Y, _palette, ClientRectangle);

                if (isValid)
                {
                    // Update selected color index
                    _selectedColorIndex = colorIndex;

                    // Raise the color selected event
                    OnColorSelected(new(_palette[colorIndex], colorIndex));

                    // Refresh the display
                    Invalidate();
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
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources if needed
            }
            base.Dispose(disposing);
        }
    }
}
