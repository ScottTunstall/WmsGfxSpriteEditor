using System.ComponentModel;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Custom panel control that renders a palette of colors using a PictureBox (no ToolStrip)
    /// </summary>
    [DesignerCategory("Code")]
    [DefaultEvent("ColorSelected")]
    [DefaultProperty("Palette")]
    [ToolboxItem(true)]
    public class PalettePanel3 : Panel
    {
        private readonly PictureBox _pictureBox;
        private readonly ToolTip _toolTip = new();
        private Color[] _palette = [];
        private int _selectedPaletteIndex = -1;
        private int _hoveredPaletteIndex = -1;

        public event EventHandler<ColourSelectedEventArgs>? ColourSelected;

        public PalettePanel3()
        {
            _pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DimGray,
                SizeMode = PictureBoxSizeMode.Normal
            };
            _pictureBox.Paint += PictureBox_Paint;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseLeave += PictureBox_MouseLeave;
            _pictureBox.MouseClick += PictureBox_MouseClick;
            Controls.Add(_pictureBox);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value ?? [];
                _pictureBox.Invalidate();
            }
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedPaletteIndex
        {
            get => _selectedPaletteIndex;
            set
            {
                if (_selectedPaletteIndex != value)
                {
                    _selectedPaletteIndex = value;
                    _pictureBox.Invalidate();
                }
            }
        }

        private void PictureBox_Paint(object? sender, PaintEventArgs e)
        {
            using (SolidBrush backColorBrush = new(BackColor))
            {
                e.Graphics.FillRectangle(backColorBrush, e.ClipRectangle);
            }

            if (_palette.Length == 0)
            {
                return;
            }
            
            int colorCount = _palette.Length;
            int cellWidth = 24;
            int cellHeight = _pictureBox.ClientSize.Height;

            for (int i = 0; i < colorCount; i++)
            {
                int x = i * cellWidth;
                Rectangle rect = new(x, 0, cellWidth, cellHeight);
                using (Brush brush = new SolidBrush(_palette[i]))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                
                // Draw selection rectangle if this is the selected color
                if (i == _selectedPaletteIndex)
                {
                    using Pen highlightPen = new(Color.Yellow, 3);
                    Rectangle highlightRect = Rectangle.Inflate(rect, -2, -2);
                    e.Graphics.DrawRectangle(highlightPen, highlightRect);
                }
            }
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx != _hoveredPaletteIndex)
            {
                _hoveredPaletteIndex = idx;
                if (idx >= 0 && idx < _palette.Length)
                {
                    _pictureBox.Cursor = Cursors.Hand;
                    Color c = _palette[idx];
                    string hex = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                    string dec = $"R:{c.R}, G:{c.G}, B:{c.B}";
                    _toolTip.SetToolTip(_pictureBox, $"{hex}\n{dec}");
                }
                else
                {
                    _pictureBox.Cursor = Cursors.Default;
                    _toolTip.SetToolTip(_pictureBox, null);
                }
            }
        }

        private void PictureBox_MouseLeave(object? sender, EventArgs e)
        {
            _hoveredPaletteIndex = -1;
            _pictureBox.Cursor = Cursors.Default;
            _toolTip.SetToolTip(_pictureBox, null);
        }

        private void PictureBox_MouseClick(object? sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx >= 0 && idx < _palette.Length)
            {
                SelectedPaletteIndex = idx;
                ColourSelected?.Invoke(this, new ColourSelectedEventArgs(_palette[idx], idx));
            }
        }

        private int HitTest(Point location)
        {
            if (_palette.Length == 0)
            {
                return -1;
            }

            int colorCount = _palette.Length;
            int cellWidth = 24;
            int idx = location.X / cellWidth;
            if (idx >= 0 && idx < colorCount && location.Y >= 0 && location.Y < _pictureBox.ClientSize.Height)
            {
                return idx;
            }
            return -1;
        }
    }
}
