using System.ComponentModel;

namespace WmsGfxSpriteEditor.Controls
{
    public class ColorPickerPanel : Panel
    {
        private Color[] _palette = [];
        private int _selectedPaletteIndex = -1;
        private int _hoveredIndex = -1;
        private ToolTip _toolTip = new();
        private PictureBox _pictureBox = new();

        public event EventHandler? SelectedColorChanged;

        public ColorPickerPanel()
        {
            InitialiseComponent();
        }

        public ColorPickerPanel(Color[] palette) : this()
        {
            Palette = palette;
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value;
                if (_selectedPaletteIndex >= _palette.Length)
                {
                    _selectedPaletteIndex = _palette.Length - 1;
                }

                _pictureBox.Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int SelectedPaletteIndex
        {
            get => _selectedPaletteIndex;
            set
            {
                _selectedPaletteIndex = value;
                _pictureBox.Invalidate();
            }
        }

        [EditorBrowsable]
        [Browsable(true)]
        public int ColorBoxSize
        {
            get;
            set;
        } = 24;

        [EditorBrowsable]
        [Browsable(true)]
        public int ColorBoxMargin
        {
            get;
            set;
        } = 4;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Color? SelectedColor => (_selectedPaletteIndex >= 0 && _selectedPaletteIndex < _palette.Length) ? _palette[_selectedPaletteIndex] : null;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public ContextMenuStrip? ColourContextMenuStrip
        {
            get;
            set;
        }


        public Size GetPreferredClientSize()
        {
            if (_palette.Length == 0)
            {
                return Size.Empty;
            }

            int columns = (int)Math.Ceiling(Math.Sqrt(_palette.Length));
            int rows = (int)Math.Ceiling(_palette.Length / (double)columns);

            int width = ColorBoxMargin * 2 + columns * (ColorBoxSize + ColorBoxMargin);
            int height = ColorBoxMargin * 2 + rows * (ColorBoxSize + ColorBoxMargin);

            return new Size(width, height);
        }


        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            for (int i = 0; i < _palette.Length; i++)
            {
                Rectangle rect = GetColorRect(i);
                using (Brush b = new SolidBrush(_palette[i]))
                {
                    e.Graphics.FillRectangle(b, rect);
                }

                if (i == _selectedPaletteIndex)
                {
                    Color highlight = GetHighlightColor(_palette[i]);
                    using Pen p = new(highlight, 3);
                    e.Graphics.DrawRectangle(p, Rectangle.Inflate(rect, -1, -1));
                }
                else if (i == _hoveredIndex)
                {
                    using Pen p = new(Color.Gray, 2);
                    e.Graphics.DrawRectangle(p, Rectangle.Inflate(rect, -1, -1));
                }
            }
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx != _hoveredIndex)
            {
                _hoveredIndex = idx;
                _pictureBox.Invalidate();
            }
            if (idx >= 0 && idx < _palette.Length)
            {
                Cursor = Cursors.Hand;
                Color c = _palette[idx];
                string hex = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                string dec = $"R:{c.R}, G:{c.G}, B:{c.B}";
                _toolTip.SetToolTip(_pictureBox, $"{hex}\n{dec}");
            }
            else
            {
                Cursor = Cursors.Default;
                _toolTip.SetToolTip(_pictureBox, "");
            }
        }

        private void PictureBox_MouseLeave(object? sender, EventArgs e)
        {
            _hoveredIndex = -1;
            Cursor = Cursors.Default;
            _toolTip.SetToolTip(_pictureBox, "");
            _pictureBox.Invalidate();
        }

        private void PictureBox_MouseClick(object? sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx >= 0 && idx < _palette.Length)
            {
                SelectedPaletteIndex = idx;
                SelectedColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void PictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ColourContextMenuStrip!=null)
            {
                int idx = HitTest(e.Location);
                if (idx >= 0 && idx < _palette.Length)
                {
                    SelectedPaletteIndex = idx;
                    ColourContextMenuStrip!.Show(_pictureBox, e.Location);
                }
            }
        }

        private int HitTest(Point location)
        {
            for (int i = 0; i < _palette.Length; i++)
            {
                if (GetColorRect(i).Contains(location))
                    return i;
            }

            return -1;
        }

        private Rectangle GetColorRect(int index)
        {
            int cols = (this.ClientSize.Width - (ColorBoxMargin * 2)) / (ColorBoxSize + ColorBoxMargin);
            int row = index / cols;
            int col = index % cols;
            int x = ColorBoxMargin + col * (ColorBoxSize + ColorBoxMargin);
            int y = ColorBoxMargin + row * (ColorBoxSize + ColorBoxMargin);
            return new Rectangle(x, y, ColorBoxSize, ColorBoxSize);
        }

        private static Color GetHighlightColor(Color color)
        {
            int brightness = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
            if (brightness > 180)
                return Color.FromArgb(255, 40, 40, 40); // dark
            else if (brightness < 75)
                return Color.FromArgb(255, 255, 255, 255); // white
            else
                return Color.FromArgb(255, 255, 215, 0); // gold
        }

        private void InitialiseComponent()
        {
            DoubleBuffered = true;

            _toolTip = new();
            
            _pictureBox = new PictureBox
            {
                Location = new Point(0, 0),
                ClientSize = this.ClientSize,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Normal
            };

            _pictureBox.Paint += PictureBox_Paint;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseLeave += PictureBox_MouseLeave;
            _pictureBox.MouseClick += PictureBox_MouseClick;
            _pictureBox.MouseUp += PictureBox_MouseUp;

            Controls.Add(_pictureBox);
        }


    }
}