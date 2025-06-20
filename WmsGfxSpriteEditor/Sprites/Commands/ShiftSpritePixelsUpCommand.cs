namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsUpCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public ShiftSpritePixelsUpCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsUp);
        }
    }
}
