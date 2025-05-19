using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for sprite rendering functionality
    /// </summary>
    public interface ISpriteRenderer
    {
        /// <summary>
        /// Calculate area required to render sprite in full
        /// </summary>
        Size CalculateMinimumClientSize(int spriteWidth, int spriteHeight, int cellSize);

        /// <summary>
        /// Return the grid cell column and row referred to by pixel coordinates X,Y, taking into account grid cell Size and render area size
        /// </summary>
        Point GridCellFromClient(int x, int y, int cellSize, Size size);

        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSprite(Graphics graphics,
            ISprite sprite,
            Color[] palette,
            int cellSize,
            Rectangle clientArea);

        /// <summary>
        /// Renders a sprite with grid to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSpriteWithGrid(Graphics graphics,
            ISprite sprite,
            Color[] palette,
            int cellSize,
            Color gridColour,
            Rectangle clientArea);
    }
}