namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsUpCommand: UndoableSpriteCommand
    {
        public ShiftSpritePixelsUpCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsUp);
        }
    }
}
