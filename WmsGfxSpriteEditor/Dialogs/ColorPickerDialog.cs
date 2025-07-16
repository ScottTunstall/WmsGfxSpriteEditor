using System.ComponentModel;
using WmsGfxSpriteEditor.Controls;
using WmsGfxSpriteEditor.Palette;

namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private ColorPickerPanel _colourPickerPanel;
        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem _copyRgbMenuItem;
        private ToolStripMenuItem _copyHexMenuItem;
        private IContainer _components;
        private readonly DefaultPaletteClipboardService _paletteService = new();

        public event EventHandler? SelectedColorChanged;

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color[] Palette
        {
            get => _colourPickerPanel.Palette;
            set
            {
                _colourPickerPanel.Palette = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int SelectedPaletteIndex
        {
            get => _colourPickerPanel.SelectedPaletteIndex;
            set => _colourPickerPanel.SelectedPaletteIndex = value;
        }

        public void ShrinkToFit()
        {
            this.ClientSize = _colourPickerPanel.GetPreferredClientSize();
        }

        protected override void OnResize(EventArgs e)
        {
            _colourPickerPanel.Invalidate();
            base.OnResize(e);
        }

        private void InitializeComponent()
        {
            _components = new Container();
            _colourPickerPanel = new ColorPickerPanel();
            _contextMenu = new ContextMenuStrip(_components);
            _copyRgbMenuItem = new ToolStripMenuItem();
            _copyHexMenuItem = new ToolStripMenuItem();
            _contextMenu.SuspendLayout();
            SuspendLayout();
            //
            // _colourPickerPanel
            //
            _colourPickerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _colourPickerPanel.ColorBoxMargin = 4;
            _colourPickerPanel.ColorBoxSize = 24;
            _colourPickerPanel.ColourContextMenuStrip = _contextMenu;
            _colourPickerPanel.Location = new Point(0, 0);
            _colourPickerPanel.Name = "_colourPickerPanel";
            _colourPickerPanel.SelectedPaletteIndex = -1;
            _colourPickerPanel.Size = new Size(284, 261);
            _colourPickerPanel.TabIndex = 0;
            _colourPickerPanel.SelectedColorChanged += OnSelectedColorChanged;
            //
            // _contextMenu
            //
            _contextMenu.Items.AddRange(new ToolStripItem[] { _copyRgbMenuItem, _copyHexMenuItem });
            _contextMenu.Name = "_contextMenu";
            _contextMenu.Size = new Size(169, 26);
            //
            // _copyRgbMenuItem
            //
            _copyRgbMenuItem.Name = "_copyRgbMenuItem";
            _copyRgbMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            _copyRgbMenuItem.Size = new Size(168, 22);
            _copyRgbMenuItem.Text = "Copy RGB";
            _copyRgbMenuItem.Click += CopyRgbMenuItem_Click;
            //
            // _copyHexMenuItem
            //
            _copyHexMenuItem.Name = "_copyHexMenuItem";
            _copyHexMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            _copyHexMenuItem.Size = new Size(32, 19);
            _copyHexMenuItem.Text = "Copy RGB as Hex";
            _copyHexMenuItem.Click += CopyHexMenuItem_Click;
            //
            // ColorPickerDialog
            //
            ClientSize = new Size(284, 261);
            Controls.Add(_colourPickerPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(128, 128);
            Name = "ColorPickerDialog";
            ShowInTaskbar = false;
            Text = "Palette";
            _contextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void CopyRgbMenuItem_Click(object? sender, EventArgs e)
        {
            int idx = _colourPickerPanel.SelectedPaletteIndex;
            if (idx >= 0 && idx < _colourPickerPanel.Palette.Length)
            {
                Color c = _colourPickerPanel.Palette[idx];
                _ = _paletteService.CopyAsRGBString(c);
            }
        }

        private void CopyHexMenuItem_Click(object? sender, EventArgs e)
        {
            int idx = _colourPickerPanel.SelectedPaletteIndex;
            if (idx >= 0 && idx < _colourPickerPanel.Palette.Length)
            {
                Color c = _colourPickerPanel.Palette[idx];
                _ = _paletteService.CopyAsHexString(c);
            }
        }

        private void OnSelectedColorChanged(object? s, EventArgs e)
        {
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}