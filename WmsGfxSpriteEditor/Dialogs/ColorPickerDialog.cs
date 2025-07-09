using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private ColorPickerPanel _colourPickerPanel;
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
            get => _colourPickerPanel.Palette;
            set => _colourPickerPanel.Palette = value;
        }

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

        private void InitializeComponent()
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
            _colourPickerPanel.Size = new Size(119, 117);
            _colourPickerPanel.TabIndex = 0;
            _colourPickerPanel.SelectedColorChanged += OnSelectedColorChanged;
            // 
            // ColorPickerDialog
            // 
            ClientSize = new Size(116, 116);
            Controls.Add(_colourPickerPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(132, 155);
            Name = "ColorPickerDialog";
            ShowInTaskbar = false;
            Text = "Palette";
            ResumeLayout(false);
        }

        private void OnSelectedColorChanged(object? s, EventArgs e)
        {
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
