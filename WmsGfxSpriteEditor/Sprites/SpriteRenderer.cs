using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public class SpriteRenderer : ISpriteRenderer
    {
        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSprite(
            Graphics graphics,
            MemoryStream? romData,
            int spriteOffset,
            int widthInBytes,
            int height,
            bool isLinear,
            Color[] palette,
            Color gridColor,
            int zoomLevel,
            Rectangle renderArea)
        {
            // If we have no ROM data, exit without rendering anything
            if (romData == null || romData.Length == 0)
            {
                return;
            }

            // Calculate pixel dimensions
            int widthInPixels = widthInBytes * 2; // 2 pixels per byte

            // Calculate cell size based on zoom level
            int cellSize = zoomLevel * 8;

            try
            {
                // Position the ROM stream at the sprite offset
                romData.Position = spriteOffset;

                // Read the sprite data
                byte[] spriteData = new byte[widthInBytes * height];
                int bytesRead = romData.Read(spriteData, 0, spriteData.Length);

                if (bytesRead < spriteData.Length)
                {
                    // Not enough data, show warning
                    using var font = new Font("Arial", 12, FontStyle.Bold);
                    using var brush = new SolidBrush(Color.Red);
                    graphics.DrawString("Incomplete Sprite Data", font, brush, 10, 10);
                    return;
                }

                // Start rendering from the top-left corner (0,0)
                int startX = renderArea.X;
                int startY = renderArea.Y;

                // Draw each pixel of the sprite
                for (int y = 0; y < height; y++)
                {
                    for (int byteX = 0; byteX < widthInBytes; byteX++)
                    {
                        // Calculate the index in the sprite data
                        int dataIndex = y * widthInBytes + byteX;

                        if (dataIndex >= spriteData.Length)
                            continue;

                        byte pixelByte = spriteData[dataIndex];

                        // Extract the two pixels from the byte
                        // Upper nibble (bits 7-4) is the first pixel
                        // Lower nibble (bits 3-0) is the second pixel
                        int colorIndex1 = (pixelByte >> 4) & 0x0F; // First pixel (upper nibble)
                        int colorIndex2 = pixelByte & 0x0F;        // Second pixel (lower nibble)

                        // Get colors from the palette (clamp to ensure valid indices)
                        Color pixelColor1 = GetColorFromPalette(palette, colorIndex1);
                        Color pixelColor2 = GetColorFromPalette(palette, colorIndex2);

                        // Draw the first pixel
                        int pixelX1 = byteX * 2;
                        DrawPixel(graphics, startX, startY, pixelX1, y, pixelColor1, gridColor, cellSize);

                        // Draw the second pixel
                        int pixelX2 = byteX * 2 + 1;
                        DrawPixel(graphics, startX, startY, pixelX2, y, pixelColor2, gridColor, cellSize);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message if rendering fails
                using var font = new Font("Arial", 12, FontStyle.Bold);
                using var brush = new SolidBrush(Color.Red);
                graphics.DrawString($"Error: {ex.Message}", font, brush, 10, 10);
            }
        }

        /// <summary>
        /// Helper method to safely get a color from a palette with index clamping
        /// </summary>
        private static Color GetColorFromPalette(Color[] palette, int colorIndex)
        {
            // Clamp the index to valid range
            int safeIndex = Math.Min(Math.Max(colorIndex, 0), palette.Length - 1);
            return palette[safeIndex];
        }

        /// <summary>
        /// Helper method to draw a single pixel of the sprite
        /// </summary>
        private void DrawPixel(
            Graphics graphics,
            int startX,
            int startY,
            int x,
            int y,
            Color pixelColor,
            Color gridColor,
            int cellSize)
        {
            // Calculate the pixel position
            int pixelX = startX + (x * cellSize);
            int pixelY = startY + (y * cellSize);

            // Draw the pixel as a colored rectangle
            using var brush = new SolidBrush(pixelColor);
            graphics.FillRectangle(
                brush,
                pixelX,
                pixelY,
                cellSize,
                cellSize);

            // Draw grid lines if zoom level is high enough
            if (cellSize >= 3)
            {
                using var pen = new Pen(gridColor);
                graphics.DrawRectangle(
                    pen,
                    pixelX,
                    pixelY,
                    cellSize,
                    cellSize);
            }
        }
    }
}
