using System.Drawing;

namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Sprite renderer, 4 bits per pixel
    /// </summary>
    public class SpriteRenderer : ISpriteRenderer
    {
        public Size GetSize(int spriteWidth, int spriteHeight, int cellSize)
        {
            return new Size(spriteWidth * cellSize, spriteHeight * cellSize);
        }

        public Point GetGridCellFromXY(int x, int y, int cellSize)
        {
            // Calculate grid coordinates based on mouse position and zoom level
            int gridX = (x / cellSize);
            int gridY = (y / cellSize);
            return new Point(gridX, gridY);
        }


        public void RenderSprite(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (sprite.PixelData.Length == 0)
            {
                return;
            }

            // Start rendering from the top-left corner (0,0)
            int startX = renderArea.X;
            int startY = renderArea.Y;
            
            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x= 0; x< sprite.Width; x++)
                {
                    Color pixelColor = sprite.GetPixel(x,y);

                    // Draw the first pixel
                    int pixelX = startX + x * cellSize;
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, pixelColor, cellSize);
                }
            }
        }


        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSpriteWithGrid(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Color gridColour,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (sprite.PixelData.Length == 0)
            {
                return;
            }

            int startX = renderArea.X;
            int startY = renderArea.Y;

            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x = 0; x < sprite.Width; x++)
                {
                    Color pixelColour = sprite.GetPixel(x, y);

                    // Draw the first pixel
                    int pixelX = startX + x * cellSize;
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, pixelColour, cellSize);

                    // Draw grid cells around the pixels
                    DrawGridCell(graphics, pixelX, pixelY, gridColour, cellSize);
                }
            }
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
            using SolidBrush brush = new(pixelColor);
            graphics.FillRectangle(
                brush,
                x,
                y,
                cellSize,
                cellSize);
        }


        private void DrawGridCell(
            Graphics graphics,
            int x,
            int y,
            Color gridColor,
            int cellSize)
        {
            using Pen pen = new(gridColor);
            graphics.DrawRectangle(
                pen,
                x,
                y,
                cellSize,
                cellSize);
        }
    }
}

