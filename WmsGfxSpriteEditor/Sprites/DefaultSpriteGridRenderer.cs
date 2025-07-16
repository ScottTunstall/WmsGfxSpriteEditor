namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Default implementation of <see cref="ISpriteGridRenderer"/> for rendering sprites with or without a grid.
    /// </summary>
    public class DefaultSpriteGridRenderer : ISpriteGridRenderer
    {
        /// <inheritdoc/>
        public Size CalculateMinimumClientSize(int spriteWidth, int spriteHeight, int cellSize)
        {
            return new Size(spriteWidth * cellSize, spriteHeight * cellSize);
        }

        /// <inheritdoc/>
        public GridCell GridCellFromClient(int x, int y, int cellSize, Size clientSize)
        {
            if (x >= clientSize.Width || y >= clientSize.Height)
            {
                return new GridCell(); // Out of bounds
            }

            // Calculate grid coordinates based on mouse position and zoom level
            int gridX = x / cellSize;
            int gridY = y / cellSize;
            return new GridCell(gridX, gridY);
        }

        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <param name="clientArea">The area to render in.</param>
        public void RenderSpriteWithoutGrid(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Rectangle clientArea)
        {
            ArgumentNullException.ThrowIfNull(sprite);

            // If we have no sprite data, exit without rendering anything
            if (sprite.PixelData.Length == 0)
            {
                return;
            }

            // Start rendering from the top-left corner (0,0)
            int startX = clientArea.X;
            int startY = clientArea.Y;

            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x = 0; x < sprite.Width; x++)
                {
                    int paletteIndex = sprite.GetPaletteIndexFromPixel(x, y);

                    // Draw the first pixel
                    int pixelX = startX + (x * cellSize);
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, sprite.Palette[paletteIndex], cellSize);
                }
            }
        }

        /// <summary>
        /// Renders a sprite with grid to the specified graphics surface, starting from the top-left corner.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <param name="gridColour">The color of the grid lines.</param>
        /// <param name="clientArea">The area to render in.</param>
        public void RenderSpriteWithGrid(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Color gridColour,
            Rectangle clientArea)
        {
            // If we have no sprite data, exit without rendering anything
            if (sprite.PixelData.Length == 0)
            {
                return;
            }

            int startX = clientArea.X;
            int startY = clientArea.Y;

            // Draw each pixel of the sprite
            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x = 0; x < sprite.Width; x++)
                {
                    int paletteIndex = sprite.GetPaletteIndexFromPixel(x, y);

                    // Draw the first pixel
                    int pixelX = startX + (x * cellSize);
                    int pixelY = startY + (y * cellSize);
                    DrawPixel(graphics, pixelX, pixelY, sprite.Palette[paletteIndex], cellSize);

                    // Draw grid cells around the pixels
                    DrawGridCell(graphics, pixelX, pixelY, gridColour, cellSize);
                }
            }
        }

        /// <summary>
        /// Helper method to draw a single pixel of the sprite.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <param name="pixelColor">The color of the pixel.</param>
        /// <param name="cellSize">The size of the cell in pixels.</param>
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

        /// <summary>
        /// Helper method to draw a grid cell around a pixel.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <param name="gridColor">The color of the grid line.</param>
        /// <param name="cellSize">The size of the cell in pixels.</param>
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