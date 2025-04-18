using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for sprite rendering functionality
    /// </summary>
    public interface ISpriteGridRenderer
    {
        /// <summary>
        /// Calculate area required to display sprite in full
        /// </summary>
        Size GetExtent(int spriteWidthInBytes, int spriteHeight, int cellSize);

        /// <summary>
        /// Return the grid cell column and row referred to by pixel coordinates X,Y, taking into account cell Size
        /// </summary>
        Point GetGridCellFromXY(int x, int y, int cellSize);

        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSprite(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int cellSize,
            Rectangle renderArea);

        public void RenderSpriteWithGrid(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int cellSize,
            Color gridColor,
            Rectangle renderArea);



    }
}
