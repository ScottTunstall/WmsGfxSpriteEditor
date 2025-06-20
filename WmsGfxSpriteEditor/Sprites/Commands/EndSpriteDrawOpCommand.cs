namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class EndSpriteDrawOpCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public EndSpriteDrawOpCommand(IHistory history) 
        {
            _undoHelper = new UndoableSpriteHelper(history ?? throw new ArgumentNullException(nameof(history)));
        }

        public void Execute(ISprite sprite)
        {
            // Take a snapshot of the pixel data for redo purposes
            _undoHelper.SnapshotPixelDataIfChanged(sprite);

            // Mark sprite pixel data as clean when drawing operation ends
            sprite.ClearPixelDataDirtyFlag();
        }
    }
}
