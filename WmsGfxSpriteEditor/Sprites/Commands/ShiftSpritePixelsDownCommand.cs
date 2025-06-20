namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsDownCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public ShiftSpritePixelsDownCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsDown);
        }
    }
}
