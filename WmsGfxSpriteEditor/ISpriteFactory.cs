using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsGfxSpriteEditor.ROMs;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public interface ISpriteFactory
    {
        public ISprite CreateSpriteFromSpriteInfo(RomData romData, SpriteInfo spriteInfo);
    }
}
