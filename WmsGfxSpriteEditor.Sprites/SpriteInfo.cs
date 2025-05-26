using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Represents information about a sprite in the ROM data
    /// </summary>
    public record SpriteInfo
    {
        /// <summary>
        /// Gets the name of the sprite
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the offset of the sprite data in the memory stream
        /// </summary>
        public int Offset { get; init; }

        /// <summary>
        /// Gets the width of the sprite in bytes (each byte contains 2 pixels)
        /// </summary>
        public int WidthInBytes { get; init; }

        /// <summary>
        /// Gets the width of the sprite in pixels (WidthInBytes * 2)
        /// </summary>
        public int WidthInPixels => WidthInBytes * 2;

        /// <summary>
        /// Gets the height of the sprite in pixels
        /// </summary>
        public int Height { get; init; }

        public int BitsPerPixel { get; init; }

        /// <summary>
        /// Gets whether the sprite data is stored in a linear format
        /// When true, data is stored row by row
        /// When false, data may be stored in a different format (e.g., planar)
        /// </summary>
        public bool IsLinear { get; init; } = true;


        /// <summary>
        /// Initializes a new instance of the SpriteInfo record
        /// </summary>
        /// <param name="name">The name of the sprite</param>
        /// <param name="offset">The offset of the sprite in the memory stream</param>
        /// <param name="widthInBytes">The width of the sprite in bytes (each byte contains 2 pixels)</param>
        /// <param name="height">The height of the sprite in pixels</param>
        /// <param name="bitsPerPixel">Bits per pixel in the sprite.</param>
        /// <param name="isLinear">Whether the sprite data is stored linearly</param>
        public SpriteInfo(string name, int offset, int widthInBytes, int height, int bitsPerPixel, bool isLinear = true)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Offset = offset;
            WidthInBytes = widthInBytes > 0 ? widthInBytes : throw new ArgumentOutOfRangeException(nameof(widthInBytes), "Width in bytes must be greater than 0");
            Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0");
            BitsPerPixel = bitsPerPixel > 0 ? bitsPerPixel : throw new ArgumentOutOfRangeException(nameof(bitsPerPixel), "Bits per pixel must be greater than 0");
            IsLinear = isLinear;
        }

        /// <summary>
        /// Returns a string representation of the sprite information
        /// </summary>
        /// <returns>A string containing the sprite name, offset, and dimensions</returns>
        public override string ToString() => $"{Name} (0x{Offset:X4}) [{WidthInPixels}x{Height}]";
    }
}
