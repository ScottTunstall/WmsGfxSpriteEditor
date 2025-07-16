using System.Drawing;
using WmsGfxSpriteEditor.Roms;

namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Factory interface for creating <see cref="ISprite"/> instances from ROM data and sprite information.
    /// </summary>
    public interface ISpriteFactory
    {
        /// <summary>
        /// Creates a sprite from ROM data, sprite metadata, and a color palette.
        /// </summary>
        /// <param name="romData">The ROM data containing the sprite's pixel information.</param>
        /// <param name="spriteInfo">Metadata describing the sprite's location, size, and format.</param>
        /// <param name="palette">The color palette to use for the sprite.</param>
        /// <returns>A new <see cref="ISprite"/> instance.</returns>
        ISprite CreateSpriteFromRomData(RomData romData, SpriteInfo spriteInfo, Color[] palette);
    }
}
