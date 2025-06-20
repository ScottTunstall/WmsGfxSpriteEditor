namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class BeginSpriteDrawOpCommand
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public BeginSpriteDrawOpCommand(IHistory history) 
        {
            _undoHelper = new UndoableSpriteHelper(history ?? throw new ArgumentNullException(nameof(history)));
        }

        public void Execute(ISprite sprite, int startX, int startY, int paletteIndex)
        {
            // Take a snapshot of the pixel data for undo purposes
            _undoHelper.SnapshotPixelDataIfChanged(sprite);

            sprite.ClearPixelDataDirtyFlag();

            new SpriteDrawOpCommand().Execute(sprite, startX, startY, paletteIndex);
        }
    }
}
