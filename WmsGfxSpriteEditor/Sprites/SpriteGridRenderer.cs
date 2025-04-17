using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public class SpriteGridRenderer : ISpriteGridRenderer
    {
        public Size CalculateSize(int spriteWidthInBytes, int spriteHeight, int cellSize)
        {
            return new(spriteWidthInBytes * 2 * cellSize, spriteHeight * cellSize);
        }

        public Point GetGridCellFromXY(int x, int y, int cellSize)
        {
            // Calculate grid coordinates based on mouse position and zoom level
            int gridX = x / cellSize;
            int gridY = y / cellSize;
            return new(gridX, gridY);
        }

        public void RenderSprite(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int cellSize,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (spriteData.Length == 0)
            {
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
                    int colorIndex2 = pixelByte & 0x0F; // Second pixel (lower nibble)

                    // Get colors from the palette (clamp to ensure valid indices)
                    Color pixelColor1 = GetColorFromPalette(palette, colorIndex1);
                    Color pixelColor2 = GetColorFromPalette(palette, colorIndex2);

                    // Draw the first pixel
                    int pixelX = startX + ((byteX * 2) * cellSize);
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, pixelColor1, cellSize);

                    // Draw the second pixel
                    DrawPixel(graphics, pixelX + cellSize, pixelY, pixelColor2, cellSize);
                }
            }
        }
        

        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSpriteWithGrid(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int cellSize,
            Color gridColor,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (spriteData.Length == 0)
            {
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
                    int colorIndex2 = pixelByte & 0x0F; // Second pixel (lower nibble)

                    // Get colors from the palette (clamp to ensure valid indices)
                    Color pixelColor1 = GetColorFromPalette(palette, colorIndex1);
                    Color pixelColor2 = GetColorFromPalette(palette, colorIndex2);

                    // Draw the first pixel
                    int pixelX = startX + ((byteX * 2) * cellSize);
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, pixelColor1, cellSize);
                    
                    // Draw the second pixel
                    DrawPixel(graphics, pixelX + cellSize, pixelY, pixelColor2, cellSize);

                    // Draw grid
                    DrawGrid(graphics, pixelX, pixelY, gridColor, cellSize);
                    DrawGrid(graphics, pixelX + cellSize, pixelY, gridColor, cellSize);
                }
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
            int x,
            int y,
            Color pixelColor,
            int cellSize)
        {
            // Draw the pixel as a colored rectangle
            using SolidBrush? brush = new(pixelColor);
            graphics.FillRectangle(
                brush,
                x,
                y,
                cellSize,
                cellSize);
        }


        private void DrawGrid(
            Graphics graphics,
            int x,
            int y,
            Color gridColor,
            int cellSize)
        {
            using Pen? pen = new(gridColor);
            graphics.DrawRectangle(
                pen,
                x,
                y,
                cellSize,
                cellSize);
        }
    }
}
