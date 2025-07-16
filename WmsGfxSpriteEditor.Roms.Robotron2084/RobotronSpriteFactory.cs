using System.Drawing;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Roms.Robotron2084
{
    public class RobotronSpriteFactory : ISpriteFactory
    {
        public ISprite CreateSpriteFromRomData(RomData romData, SpriteInfo spriteInfo, Color[] palette)
        {
            ArgumentNullException.ThrowIfNull(romData);
            ArgumentNullException.ThrowIfNull(spriteInfo);

            if (palette.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(palette));
            }

            // We currently only support 4bpp sprites, but if we ever support more, we can add a switch on spriteInfo.BitsPerPixel here.
            return CreateSprite4Bpp(romData, spriteInfo, palette);
        }

        private ISprite CreateSprite4Bpp(RomData romData, SpriteInfo spriteInfo, Color[] palette)
        {
            int bytesToRead = spriteInfo.WidthInBytes * spriteInfo.Height;
            Memory<byte> spriteData = romData!.AsMemory(spriteInfo.Offset, bytesToRead);

            ISprite sprite = new Sprite4Bpp(spriteInfo.Index, spriteData, spriteInfo.WidthInBytes, spriteInfo.Height, palette, spriteInfo.IsLinear);
            return sprite;
        }
    }
}