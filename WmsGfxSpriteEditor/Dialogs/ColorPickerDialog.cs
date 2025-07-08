namespace WmsGfxSpriteEditor.Dialogs
{
    public class ColorPickerDialog : Form
    {
        private Color[] _palette = Array.Empty<Color>();
        private int _selectedPaletteIndex = -1;
        private readonly ToolTip _toolTip = new();
        private const int ColorBoxSize = 24;
        private const int ColorBoxMargin = 4;
        private int _hoveredIndex = -1;
        public event EventHandler? SelectedColorChanged;

        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value ?? [];
                if (_selectedPaletteIndex >= _palette.Length)
                {
                    _selectedPaletteIndex = _palette.Length - 1;
                }

                Invalidate();
                ResizeToFit();
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
                Invalidate();
                SelectedColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Color? SelectedColor => (_selectedPaletteIndex >= 0 && _selectedPaletteIndex < _palette.Length) ? _palette[_selectedPaletteIndex] : null;

        public ColorPickerDialog()
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            MouseMove += ColorPickerDialog_MouseMove;
            MouseLeave += ColorPickerDialog_MouseLeave;
            MouseClick += ColorPickerDialog_MouseClick;
            Paint += ColorPickerDialog_Paint;
        }

        private void ResizeToFit()
        {
            int count = _palette.Length;
            int cols = Math.Min(8, count);
            int rows = (count + cols - 1) / cols;
            int width = cols * (ColorBoxSize + ColorBoxMargin) + ColorBoxMargin;
            int height = rows * (ColorBoxSize + ColorBoxMargin) + ColorBoxMargin;
            ClientSize = new Size(width, height);
        }

        private void ColorPickerDialog_Paint(object? sender, PaintEventArgs e)
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

        private Rectangle GetColorRect(int index)
        {
            int cols = Math.Min(8, _palette.Length);
            int row = index / cols;
            int col = index % cols;
            int x = ColorBoxMargin + col * (ColorBoxSize + ColorBoxMargin);
            int y = ColorBoxMargin + row * (ColorBoxSize + ColorBoxMargin);
            return new Rectangle(x, y, ColorBoxSize, ColorBoxSize);
        }

        private void ColorPickerDialog_MouseMove(object? sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx != _hoveredIndex)
            {
                _hoveredIndex = idx;
                Invalidate();
            }
            if (idx >= 0 && idx < _palette.Length)
            {
                Cursor = Cursors.Hand;
                var c = _palette[idx];
                string hex = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                string dec = $"R:{c.R}, G:{c.G}, B:{c.B}";
                _toolTip.SetToolTip(this, $"{hex}\n{dec}");
            }
            else
            {
                Cursor = Cursors.Default;
                _toolTip.SetToolTip(this, "");
            }
        }

        private void ColorPickerDialog_MouseLeave(object? sender, EventArgs e)
        {
            _hoveredIndex = -1;
            Cursor = Cursors.Default;
            _toolTip.SetToolTip(this, "");
            Invalidate();
        }

        private void ColorPickerDialog_MouseClick(object? sender, MouseEventArgs e)
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

        private static Color GetHighlightColor(Color color)
        {
            // Use a contrasting color for highlight
            int brightness = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
            // If color is light, use dark highlight, else use light highlight
            if (brightness > 180)
                return Color.FromArgb(255, 40, 40, 40); // dark
            else if (brightness < 75)
                return Color.FromArgb(255, 255, 255, 255); // white
            else
                return Color.FromArgb(255, 255, 215, 0); // gold
        }
    }
}
