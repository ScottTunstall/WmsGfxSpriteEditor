using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private ColorPickerPanel _palettePanel;
        public event EventHandler? SelectedColorChanged;

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        public ColorPickerDialog(Color[] palette) : this()
        {
            Palette = palette;
        }


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

        protected override void OnResize(EventArgs e)
        {
            _palettePanel.Invalidate();
            base.OnResize(e);
        }

        private void InitializeComponent()
        {
            _palettePanel = new ColorPickerPanel();
            SuspendLayout();
            // 
            // _palettePanel
            // 
            _palettePanel.ColorBoxMargin = 4;
            _palettePanel.ColorBoxSize = 24;
            _palettePanel.Location = new Point(0, 0);
            _palettePanel.Name = "_palettePanel";
            _palettePanel.SelectedPaletteIndex = -1;
            _palettePanel.Size = new Size(200, 100);
            _palettePanel.TabIndex = 0;
            _palettePanel.SelectedColorChanged += OnSelectedColorChanged;
            // 
            // ColorPickerDialog
            // 
            ClientSize = new Size(116, 116);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(132, 155);
            Name = "ColorPickerDialog";
            ShowInTaskbar = false;
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
