namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class ShiftSpritePixelsLeftCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public ShiftSpritePixelsLeftCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsLeft);
        }
    }
}
