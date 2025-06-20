namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class ShiftSpritePixelsRightCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public ShiftSpritePixelsRightCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsRight);
        }
    }
}