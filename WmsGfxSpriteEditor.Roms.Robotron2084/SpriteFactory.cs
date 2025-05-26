using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Roms.Robotron2084
{
    public class SpriteFactory : ISpriteFactory
    {
        public ISprite CreateSpriteFromRomData(RomData romData, SpriteInfo spriteInfo)
        {
            return CreateSprite4Bpp(romData, spriteInfo);
        }

        private ISprite CreateSprite4Bpp(RomData romData, SpriteInfo spriteInfo)
        {
            int bytesToRead = spriteInfo.WidthInBytes * spriteInfo.Height;
            Memory<byte> spriteData = romData!.ReadAsMemory(spriteInfo.Offset, bytesToRead);

            return new Sprite4Bpp(spriteData, spriteInfo.WidthInBytes, spriteInfo.Height, spriteInfo.IsLinear);
        }
    }
}