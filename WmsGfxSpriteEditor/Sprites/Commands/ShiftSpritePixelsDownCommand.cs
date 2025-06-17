using System.Security.Policy;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsDownCommand: UndoableSpriteCommand
    {
        public ShiftSpritePixelsDownCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsDown);
        }
    }
}
