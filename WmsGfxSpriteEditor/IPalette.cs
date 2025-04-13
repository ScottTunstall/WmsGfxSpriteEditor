using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for palette providers
    /// </summary>
    public interface IPalette
    {
        /// <summary>
        /// Gets the color palette
        /// </summary>
        /// <returns>An array of 16 colors</returns>
        Color[] GetPalette();
    }
}
