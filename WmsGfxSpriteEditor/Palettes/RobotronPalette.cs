using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Palettes
{
    /// <summary>
    /// Provides the color palette from the Robotron arcade game
    /// </summary>
    public class RobotronPalette : IPalette
    {
        // Original color values from Robotron hardware
        private static readonly byte[] _colorValues = new byte[]
        {
            0x00, 0x07, 0x17, 0xc7, 0x1f, 0x3f, 0x38, 0xc0,
            0xa4, 0xff, 0x38, 0x17, 0xcc, 0x81, 0x81, 0x07
        };

        // The actual RGB colors derived from the hardware values
        private readonly Color[] _palette;

        /// <summary>
        /// Initializes a new instance of the RobotronPalette class
        /// </summary>
        public RobotronPalette()
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


        private static Color ConvertColorValue(byte value)
        {
            int red = (value & 0x7) << 1;
            if (red > 6)
                red++;

            int green = (value & 0x38) >> 2; 
            if (green > 6)
                green++;

            int blue = ((value & 0xc0) >> 6) * 5;

            // Ensure values are in valid range
            red = Math.Min(255, red << 4 );
            green = Math.Min(255, green << 4);
            blue = Math.Min(255, blue << 4);

            return Color.FromArgb(255, red, green, blue);
        }
    }
}
