using System.ComponentModel;
using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private ColorPickerPanel _colourPickerPanel;

        public event EventHandler? SelectedColorChanged;

        public ColorPickerDialog(Color[] palette)
        {
            if (palette.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(palette));
            }

            InitializeComponent(palette);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color[] Palette
        {
            get => _colourPickerPanel.Palette;
            set => _colourPickerPanel.Palette = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int SelectedPaletteIndex
        {
            get => _colourPickerPanel.SelectedPaletteIndex;
            set => _colourPickerPanel.SelectedPaletteIndex = value;
        }

        protected override void OnResize(EventArgs e)
        {
            _colourPickerPanel.Invalidate();
            base.OnResize(e);
        }

        private void InitializeComponent(Color[] palette)
        {
            _colourPickerPanel = new ColorPickerPanel();
            SuspendLayout();
            //
            // _colourPickerPanel
            //
            _colourPickerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _colourPickerPanel.ColorBoxMargin = 4;
            _colourPickerPanel.ColorBoxSize = 24;
            _colourPickerPanel.Location = new Point(0, 0);
            _colourPickerPanel.Name = "_colourPickerPanel";
            _colourPickerPanel.SelectedPaletteIndex = -1;
            _colourPickerPanel.TabIndex = 0;
            _colourPickerPanel.SelectedColorChanged += OnSelectedColorChanged;
            _colourPickerPanel.Palette = palette;
            _colourPickerPanel.ClientSize = _colourPickerPanel.GetPreferredSize();

            //
            // ColorPickerDialog
            //

            Controls.Add(_colourPickerPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "ColorPickerDialog";
            ShowInTaskbar = false;
            Text = "Palette";
            ClientSize = _colourPickerPanel.ClientSize;
            MinimumSize = Size;
            ResumeLayout(false);
        }

        private void OnSelectedColorChanged(object? s, EventArgs e)
        {
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}