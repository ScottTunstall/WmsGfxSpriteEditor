namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class BeginSpritePixelOpCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public BeginSpritePixelOpCommand(IHistory history) 
        {
            _undoHelper = new UndoableSpriteHelper(history ?? throw new ArgumentNullException(nameof(history)));
        }

        public void Execute(ISprite sprite, int startX, int startY, int paletteIndex)
        {
            // Take a snapshot of the pixel data for undo purposes
            _undoHelper.SnapshotPixelDataIfChanged(sprite);

            sprite.ClearPixelDataDirtyFlag();

            new SpritePixelOpCommand().Execute(sprite, startX, startY, paletteIndex);
        }
    }
}
