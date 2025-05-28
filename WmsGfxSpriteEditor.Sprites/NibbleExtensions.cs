using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    internal static class NibbleExtensions
    {
        internal static byte SwapNibbles(this byte value)
        {
            var upperNibble = (byte)(value >> 4);
            var lowerNibble = (byte)(value & 0x0f);
            byte swappedNibbles = (byte)((lowerNibble << 4) | upperNibble);
            return swappedNibbles;
        }
    }
}
