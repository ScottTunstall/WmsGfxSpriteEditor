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
    public interface ISpriteRenderer
    {
        /// <summary>
        /// Renders a sprite to the specified graphics surface, starting from the top-left corner
        /// </summary>
        public void RenderSprite(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int zoomLevel,
            Rectangle renderArea);

        public void RenderSpriteWithGrid(Graphics graphics,
            ReadOnlySpan<byte> spriteData,
            Color[] palette,
            int widthInBytes,
            int height,
            bool isLinear,
            int zoomLevel,
            Color gridColor,
            Rectangle renderArea);
    }
}
