namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class FlipSpriteVerticalCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public FlipSpriteVerticalCommand(IHistory history)
        {
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void Execute(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.YFlip);
        }
    }
}