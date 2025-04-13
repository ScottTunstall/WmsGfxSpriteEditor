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
        void RenderSprite(
            Graphics graphics,
            MemoryStream? romData,
            int spriteOffset,
            int widthInBytes,
            int height,
            bool isLinear,
            Color[] palette,
            Color gridColor,
            int zoomLevel,
            Rectangle renderArea);
    }
}
