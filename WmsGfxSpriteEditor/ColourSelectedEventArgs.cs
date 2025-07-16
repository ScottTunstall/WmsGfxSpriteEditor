using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Event arguments for the ColorSelected event
    /// </summary>
    public class ColourSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ColorSelectedEventArgs class
        /// </summary>
        /// <param name="selectedColour">The selected colour</param>
        /// <param name="colourIndex">The index of the selected colour in the palette</param>
        public ColourSelectedEventArgs(Color selectedColour, int colourIndex)
        {
            SelectedColour = selectedColour;
            ColourIndex = colourIndex;
        }

        /// <summary>
        /// Gets the selected colour (Color to our American cousins)
        /// </summary>
        public Color SelectedColour { get; }

        /// <summary>
        /// Gets the index of the selected color in the palette
        /// </summary>
        public int ColourIndex { get; }
    }
}
