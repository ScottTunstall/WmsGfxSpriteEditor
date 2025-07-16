// Credit to Sean Riddle for designing the algorithm which converts byte colour values to their RGB equivalent

using System.Drawing;
using WmsGfxSpriteEditor.Palettes;

namespace WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Palettes
{
    /// <summary>
    /// Provides the color palette from the Robotron arcade game
    /// </summary>
    public class RobotronPaletteService : IPaletteService
    {
        // Original color values from Robotron ROM
        private static readonly byte[] _colorValues =
        [
            0x00, 0x07, 0x17, 0xc7, 0x1f, 0x3f, 0x38, 0xc0,
            0xa4, 0xff, 0x38, 0x17, 0xcc, 0x81, 0x81, 0x07
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
            int red = (value & 0x7) << 1;
            if (red > 6)
            {
                red++;
            }

            int green = (value & 0x38) >> 2; 
            if (green > 6)
            {
                green++;
            }

            int blue = ((value & 0xc0) >> 6) * 5;

            // Ensure values are in valid range
            red = Math.Min(255, red << 4 );
            green = Math.Min(255, green << 4);
            blue = Math.Min(255, blue << 4);

            return Color.FromArgb(255, red, green, blue);
        }
    }
}
