using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for sprite rendering functionality
    /// </summary>
    public interface ISpriteRenderer
    {
        /// <summary>
        /// Calculate area required to display sprite in full
        /// </summary>
        Size GetSize(int spriteWidth, int spriteHeight, int cellSize);

        /// <summary>
        /// Return the grid cell column and row referred to by pixel coordinates X,Y, taking into account cell Size
        /// </summary>
        Point GetGridCellFromXY(int x, int y, int cellSize);

        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSprite(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Rectangle renderArea);

        public void RenderSpriteWithGrid(Graphics graphics,
            ISprite sprite,
            int cellSize,
            Color gridColour,
            Rectangle renderArea);
    }
}