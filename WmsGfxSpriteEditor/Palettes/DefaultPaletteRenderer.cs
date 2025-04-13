using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Palettes
{
    /// <summary>
    /// Default implementation of the palette renderer that shows colors in a grid
    /// </summary>
    public class DefaultPaletteRenderer : IPaletteRenderer
    {
        private const int ColorsPerRow = 16;
        private const int TotalRows = 1;

        /// <summary>
        /// Renders a color palette to the specified graphics surface
        /// </summary>
        /// <param name="graphics">Graphics context to render to</param>
        /// <param name="palette">Array of colors in the palette</param>
        /// <param name="renderArea">Rectangle defining the area to render in</param>
        /// <param name="selectedColorIndex">Index of the currently selected color</param>
        public void RenderPalette(Graphics graphics, Color[] palette, Rectangle renderArea, int selectedColorIndex)
        {
            ArgumentNullException.ThrowIfNull(graphics);
            ArgumentNullException.ThrowIfNull(palette);

            if (palette.Length != 16)
                throw new ArgumentException("Palette must contain exactly 16 colors", nameof(palette));

            int blockWidth = renderArea.Width / ColorsPerRow;
            int blockHeight = renderArea.Height;

            // Draw each color block
            for (int i = 0; i < 16; i++)
            {
                int col = i % ColorsPerRow;

                var colorRect = new Rectangle(
                    renderArea.Left + col * blockWidth,
                    renderArea.Top,
                    blockWidth,
                    blockHeight
                );

                // Fill with the palette color
                using var brush = new SolidBrush(palette[i]);
                graphics.FillRectangle(brush, colorRect);

                // Draw selection indicator for the currently selected color
                if (i == selectedColorIndex)
                {
                    // Draw a thicker highlight border
                    using var highlightPen = new Pen(Color.White, 2);
                    Rectangle highlightRect = new Rectangle(
                        colorRect.Left + 2,
                        colorRect.Top + 2,
                        colorRect.Width - 4,
                        colorRect.Height - 4
                    );
                    graphics.DrawRectangle(highlightPen, highlightRect);
                }

                // Draw border
                using var pen = new Pen(Color.DarkGray);
                graphics.DrawRectangle(pen, colorRect);

                // Draw index number
                using var font = new Font("Arial", 8, FontStyle.Bold);
                using var indexBrush = new SolidBrush(GetContrastingColor(palette[i]));
                string indexText = i.ToString("X");
                SizeF textSize = graphics.MeasureString(indexText, font);
                graphics.DrawString(
                    indexText,
                    font,
                    indexBrush,
                    colorRect.Left + (colorRect.Width - textSize.Width) / 2,
                    colorRect.Top + (colorRect.Height - textSize.Height) / 2
                );
            }
        }

        /// <summary>
        /// Gets the color index at the specified coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="palette">The palette array</param>
        /// <param name="renderArea">The rendering area rectangle</param>
        /// <returns>Tuple containing the color index and whether the coordinates are valid</returns>
        public (int colorIndex, bool isValid) GetColorIndexAt(int x, int y, Color[] palette, Rectangle renderArea)
        {
            ArgumentNullException.ThrowIfNull(palette);

            if (palette.Length != 16 || !renderArea.Contains(x, y))
                return (-1, false);

            // Calculate which color block was clicked
            int blockWidth = renderArea.Width / ColorsPerRow;

            // Calculate the color index based on x position
            int colorIndex = (x - renderArea.Left) / blockWidth;

            // Ensure it's a valid index
            if (colorIndex < 0 || colorIndex >= 16)
                return (-1, false);

            return (colorIndex, true);
        }

        /// <summary>
        /// Helper to get a contrasting color for text visibility
        /// </summary>
        private static Color GetContrastingColor(Color color)
        {
            // Simple calculation to determine if white or black would be more visible
            int brightness = (int)Math.Sqrt(
                color.R * color.R * 0.299 +
                color.G * color.G * 0.587 +
                color.B * color.B * 0.114
            );
            return brightness > 130 ? Color.Black : Color.White;
        }
    }
}
