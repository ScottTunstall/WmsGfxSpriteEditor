namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class FlipSpriteHorizontalCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public FlipSpriteHorizontalCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.XFlip);
        }
    }
}
