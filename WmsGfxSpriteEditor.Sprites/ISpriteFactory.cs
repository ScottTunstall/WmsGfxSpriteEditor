using WmsGfxSpriteEditor.Roms;

namespace WmsGfxSpriteEditor.Sprites
{
    public interface ISpriteFactory
    {
        public ISprite CreateSpriteFromRomData(RomData romData, SpriteInfo spriteInfo);
    }
}
