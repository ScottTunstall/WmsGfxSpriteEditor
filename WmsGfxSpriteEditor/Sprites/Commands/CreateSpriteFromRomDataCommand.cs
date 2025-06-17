using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class CreateSpriteFromRomDataCommand
    {
        private readonly RomData _romData;
        private readonly ISpriteFactory _spriteFactory;

        public CreateSpriteFromRomDataCommand(RomData romData, ISpriteFactory? spriteFactory)
        {
            _romData = romData ?? throw new ArgumentNullException(nameof(romData));
            _spriteFactory = spriteFactory ?? throw new ArgumentNullException(nameof(spriteFactory));
        }

        public ISprite FromSpriteInfo(SpriteInfo spriteInfo)
        {
            return _spriteFactory!.CreateSpriteFromRomData(_romData, spriteInfo);
        }
    }
}
