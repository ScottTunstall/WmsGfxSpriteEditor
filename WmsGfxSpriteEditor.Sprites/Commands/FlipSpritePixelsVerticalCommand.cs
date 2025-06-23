namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class FlipSpritePixelsVerticalCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public FlipSpritePixelsVerticalCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.YFlip);
        }
    }
}