namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Sprite renderer, 4 bits per pixel
    /// </summary>
    public class SpriteRenderer4Bpp : ISpriteRenderer
    {
        public Size GetSize(int spriteWidthInBytes, int spriteHeight, int cellSize)
        {
            return new Size(spriteWidthInBytes * 2 * cellSize, spriteHeight * cellSize);
        }

        public Point GetGridCellFromXY(int x, int y, int cellSize)
        {
            // Calculate grid coordinates based on mouse position and zoom level
            int gridX = (x / cellSize) + 1;
            int gridY = (y / cellSize) + 1;
            return new Point(gridX, gridY);
        }

        public void RenderSprite(Graphics graphics,
            Sprite sprite,
            int cellSize,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (sprite.Data.Length == 0)
            {
                return;
            }

            // Start rendering from the top-left corner (0,0)
            int startX = renderArea.X;
            int startY = renderArea.Y;
            
            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int byteX = 0; byteX < sprite.WidthInBytes; byteX++)
                {
                    // Calculate the index in the sprite data
                    int dataIndex = y * sprite.WidthInBytes+ byteX;

                    if (dataIndex >= sprite.Data.Length)
                        continue;

                    Color pixelColor1 = sprite.GetFirstPixelColour(dataIndex);
                    Color pixelColor2 = sprite.GetSecondPixelColour(dataIndex);

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
            Sprite sprite,
            int cellSize,
            Color gridColor,
            Rectangle renderArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (sprite.Data.Length == 0)
            {
                return;
            }

            int startX = renderArea.X;
            int startY = renderArea.Y;

            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int byteX = 0; byteX < sprite.WidthInBytes; byteX++)
                {
                    // Calculate the index in the sprite data
                    int dataIndex = y * sprite.WidthInBytes+ byteX;

                    if (dataIndex >= sprite.Data.Length)
                        continue;

                    Color pixelColor1 = sprite.GetFirstPixelColour(dataIndex);
                    Color pixelColor2 = sprite.GetSecondPixelColour(dataIndex);

                    // Draw the first pixel
                    int pixelX = startX + ((byteX * 2) * cellSize);
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, pixelColor1, cellSize);

                    // Draw the second pixel
                    DrawPixel(graphics, pixelX + cellSize, pixelY, pixelColor2, cellSize);

                    // Draw grid cells around the pixels
                    DrawGridCell(graphics, pixelX, pixelY, gridColor, cellSize);
                    DrawGridCell(graphics, pixelX + cellSize, pixelY, gridColor, cellSize);
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

