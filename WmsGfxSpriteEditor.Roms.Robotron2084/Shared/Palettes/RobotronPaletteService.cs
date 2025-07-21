// Credit to Sean Riddle for designing the algorithm which converts byte colour values to their RGB equivalent
// See: https://www.seanriddle.com/ripper.html

using System.Drawing;
using WmsGfxSpriteEditor.Palettes;

namespace WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Palettes
{
    /// <summary>
    /// Provides the color palette from the Robotron arcade game
    /// Colours are stored as a byte, in BBGGGRRR format
    /// </summary>
    public class RobotronPaletteService : IPaletteService
    {
        // Original color values from Robotron ROM - as supplied by Sean Riddle
        // and present in my disassembly at $DA51.

        // private static readonly byte[] _colorValues =
        // [
        //     0x00, 0x07, 0x17, 0xc7, 0x1f, 0x3f, 0x38, 0xc0,
        //     0xa4, 0xff, 0x38, 0x17, 0xcc, 0x81, 0x81, 0x07
        // ];

        // Modified to prevent duplicate colours, which would break the clipboard|paste functionality in the sprite editor.
        // NB: Colour index 10 (zero based index) onwards are used for colour cycling in game.
        private static readonly byte[] _colorValues =
        [
            0x00, 0x07, 0x17, 0xc7, 0x1f, 0x3f, 0x38, 0xc0,
            0xa4, 0xff,
            // Cycling colours that I can't really map, so given unique values
            0xc4, 0xf4, 0xcc, 0x81, 0x45, 0x2f
        ];

        // The actual RGB colors derived from the hardware values
        private readonly Color[] _palette;

        /// <summary>
        /// Initializes a new instance of the RobotronPalette class
        /// </summary>
        public RobotronPaletteService()
        {
            _palette = new Color[16];

            // Convert the color values to RGB colors
            for (int i = 0; i < 16; i++)
            {
                _palette[i] = ConvertColorValue(_colorValues[i]);
            }
        }

        /// <summary>
        /// Gets the color palette
        /// </summary>
        /// <returns>An array of 16 colors</returns>
        public Color[] GetPalette() => _palette;

        // This code which converts bytes to RGB was ported from Sean Riddle's Williams Graphics Ripper
        private static Color ConvertColorValue(byte value)
        {
            // The red component is stored in bits 0-2
            int red = (value & 0x7) << 1;
            if (red > 6)
            {
                red++;
            }

            // The green component is stored in bits 3-5
            int green = (value & 0x38) >> 2;
            if (green > 6)
            {
                green++;
            }

            // The blue component is stored in the top 2 bits
            int blue = ((value & 0xc0) >> 6) * 5;

            // Ensure values are in valid range
            red = Math.Min(255, red << 4);
            green = Math.Min(255, green << 4);
            blue = Math.Min(255, blue << 4);

            return Color.FromArgb(255, red, green, blue);
        }
    }
}