using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WmsGfxSpriteEditor.Controls
{
    public class ColorPickerPanel : Panel
    {
        private Color[] _palette = [];
        private int _selectedPaletteIndex = -1;
        private int _hoveredIndex = -1;
        private readonly ToolTip _toolTip = new();
        private readonly PictureBox _pictureBox;
        public event EventHandler? SelectedColorChanged;

        public ColorPickerPanel()
        {
            DoubleBuffered = true;
            _pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Normal
            };

            _pictureBox.Paint += PictureBox_Paint;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseLeave += PictureBox_MouseLeave;
            _pictureBox.MouseClick += PictureBox_MouseClick;
            Controls.Add(_pictureBox);
        }

        public ColorPickerPanel(Color[] palette) : this()
        {
            Palette = palette;
        }


        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value;
                if (_selectedPaletteIndex >= _palette.Length)
                    _selectedPaletteIndex = _palette.Length - 1;

                _pictureBox.Invalidate();
            }
        }

        public int SelectedPaletteIndex
        {
            get => _selectedPaletteIndex;
            set
            {
                if (value < 0 || value >= _palette.Length)
                    return;

                _selectedPaletteIndex = value;
                _pictureBox.Invalidate();
                SelectedColorChanged?.Invoke(this, EventArgs.Empty);
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


        public Size CalculateSize(int columns, int rows)
        {
            return new Size(
                ColorBoxMargin + (columns * (ColorBoxSize + ColorBoxMargin)),
                ColorBoxMargin + (rows * (ColorBoxSize + ColorBoxMargin))
            );
        }



        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            for (int i = 0; i < _palette.Length; i++)
            {
                Rectangle rect = GetColorRect(i);
                using (Brush b = new SolidBrush(_palette[i]))
                    e.Graphics.FillRectangle(b, rect);

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
            int cols = this.Size.Width / (ColorBoxSize + ColorBoxMargin);
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
    }
}
