namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class FlipSpritePixelsHorizontalCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public FlipSpritePixelsHorizontalCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.XFlip);
        }
    }
}
