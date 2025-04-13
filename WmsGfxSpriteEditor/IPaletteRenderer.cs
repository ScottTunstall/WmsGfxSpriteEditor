using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for rendering color palettes
    /// </summary>

    public interface IPaletteRenderer
    {
        /// <summary>
        /// Renders a color palette to the specified graphics surface
        /// </summary>
        /// <param name="graphics">Graphics context to render to</param>
        /// <param name="palette">Array of colors in the palette</param>
        /// <param name="renderArea">Rectangle defining the area to render in</param>
        /// <param name="selectedColorIndex">Index of the currently selected color</param>
        public void RenderPalette(Graphics graphics, Color[] palette, Rectangle renderArea, int selectedColorIndex);

        /// <summary>
        /// Gets the color index at the specified coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="palette">The palette array</param>
        /// <param name="renderArea">The rendering area rectangle</param>
        /// <returns>Tuple containing the color index and whether the coordinates are valid</returns>
        public (int colorIndex, bool isValid) GetColorIndexAt(int x, int y, Color[] palette, Rectangle renderArea);
    }
}