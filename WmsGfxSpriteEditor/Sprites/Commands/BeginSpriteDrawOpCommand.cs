using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class BeginSpriteDrawOpCommand: UndoableSpriteCommand
    {
        public BeginSpriteDrawOpCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
