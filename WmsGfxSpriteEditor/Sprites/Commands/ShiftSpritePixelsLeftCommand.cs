using WmsGfxSpriteEditor.Sprites;
using WmsGfxSpriteEditor.Sprites.Commands;

namespace WmsGfxSpriteEditor.Commands.Sprite
{
    internal class ShiftSpritePixelsLeftCommand: UndoableSpriteCommand
    {
        public ShiftSpritePixelsLeftCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsLeft);
        }
    }
}
