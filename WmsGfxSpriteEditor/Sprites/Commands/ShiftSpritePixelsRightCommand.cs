namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsRightCommand : UndoableSpriteCommand
    {
        public ShiftSpritePixelsRightCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsRight);
        }
    }
}