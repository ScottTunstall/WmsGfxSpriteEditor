using System.Drawing;
using WmsGfxSpriteEditor.Roms;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class CreateSpriteFromRomDataCommand
    {
        private readonly RomData _romData;
        private readonly ISpriteFactory _spriteFactory;

        public CreateSpriteFromRomDataCommand(RomData romData, ISpriteFactory spriteFactory)
        {
            _romData = romData ?? throw new ArgumentNullException(nameof(romData));
            _spriteFactory = spriteFactory ?? throw new ArgumentNullException(nameof(spriteFactory));
        }

        public ISprite Execute(SpriteInfo spriteInfo, Color[] palette)
        {
            return _spriteFactory!.CreateSpriteFromRomData(_romData, spriteInfo, palette);
        }
    }
}
