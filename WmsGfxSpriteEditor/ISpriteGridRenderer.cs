using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for sprite grid rendering functionality, including grid and non-grid rendering and coordinate mapping.
    /// </summary>
    public interface ISpriteGridRenderer
    {
        /// <summary>
        /// Calculates the minimum client area required to render a sprite at the given cell size.
        /// </summary>
        /// <param name="spriteWidth">The width of the sprite in cells.</param>
        /// <param name="spriteHeight">The height of the sprite in cells.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <returns>The minimum client area size required.</returns>
        Size CalculateMinimumClientSize(int spriteWidth, int spriteHeight, int cellSize);

        /// <summary>
        /// Gets the grid cell (column and row) from client pixel coordinates, considering cell size and client area.
        /// </summary>
        /// <param name="x">The X pixel coordinate.</param>
        /// <param name="y">The Y pixel coordinate.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <param name="clientSize">The size of the client area.</param>
        /// <returns>The grid cell corresponding to the coordinates.</returns>
        GridCell GridCellFromClient(int x, int y, int cellSize, Size clientSize);

        /// <summary>
        /// Renders a sprite to the specified graphics surface without drawing a grid.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <param name="clientArea">The area to render in.</param>
        void RenderSpriteWithoutGrid(Graphics graphics, ISprite sprite, int cellSize, Rectangle clientArea);

        /// <summary>
        /// Renders a sprite with a grid to the specified graphics surface.
        /// </summary>
        /// <param name="graphics">The graphics context to render to.</param>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="cellSize">The size of each cell in pixels.</param>
        /// <param name="gridColour">The color of the grid lines.</param>
        /// <param name="clientArea">The area to render in.</param>
        void RenderSpriteWithGrid(Graphics graphics, ISprite sprite, int cellSize, Color gridColour, Rectangle clientArea);
    }
}