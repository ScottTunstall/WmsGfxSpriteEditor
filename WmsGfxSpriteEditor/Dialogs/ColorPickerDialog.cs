using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private ColorPickerPanel _palettePanel;
        public event EventHandler? SelectedColorChanged;

        public Color[] Palette
        {
            get => _palettePanel.Palette;
            set => _palettePanel.Palette = value;
        }

        public int SelectedPaletteIndex
        {
            get => _palettePanel.SelectedPaletteIndex;
            set => _palettePanel.SelectedPaletteIndex = value;
        }

        public Color? SelectedColor => _palettePanel.SelectedColor;

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        public ColorPickerDialog(Color[] palette) : this()
        {
            Palette = palette;
        }


        protected override void OnResize(EventArgs e)
        {
            _palettePanel.Invalidate();
            base.OnResize(e);
        }

        private void InitializeComponent()
        {
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            _palettePanel = new ColorPickerPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Dock = DockStyle.Fill
            };
            _palettePanel.SelectedColorChanged += OnSelectedColorChanged;

            SuspendLayout();
            // 
            // ColorPickerDialog
            // 
            ClientSize = _palettePanel.CalculateSize(4, 4);
            MinimumSize = this.Size;
            Name = "ColorPickerDialog";
            Text = "Palette";

            Controls.Add(_palettePanel);
            ResumeLayout(false);
        }

        private void OnSelectedColorChanged(object? s, EventArgs e)
        {
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
