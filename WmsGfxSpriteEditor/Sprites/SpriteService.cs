using WmsGfxSpriteEditor.History;

namespace WmsGfxSpriteEditor.Sprites
{
    public class SpriteService : ISpriteService
    {
        private readonly UndoableSpriteHelper _undoHelper;

        public SpriteService(IHistory history)
        {
            ArgumentNullException.ThrowIfNull(history, nameof(history));
            _undoHelper = new UndoableSpriteHelper(history);
        }

        public void BeginSpriteDrawOp(ISprite sprite, int startX, int startY, int paletteIndex)
        {
            _undoHelper.SnapshotPixelDataIfChanged(sprite);
            sprite.ClearPixelDataDirtyFlag();
            sprite.SetPixelByPaletteIndex(startX, startY, paletteIndex);
        }

        public void SpriteDrawOp(ISprite sprite, int x, int y, int paletteIndex)
        {
            sprite.SetPixelByPaletteIndex(x, y, paletteIndex);
        }

        public void EndSpriteDrawOp(ISprite sprite)
        {
            _undoHelper.SnapshotPixelDataIfChanged(sprite);
            sprite.ClearPixelDataDirtyFlag();
        }

        public void FlipSpriteHorizontal(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.XFlip);
        }

        public void FlipSpriteVertical(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.YFlip);
        }

        public void ShiftSpritePixelsLeft(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsLeft);
        }

        public void ShiftSpritePixelsRight(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsRight);
        }

        public void ShiftSpritePixelsUp(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsUp);
        }

        public void ShiftSpritePixelsDown(ISprite sprite)
        {
            _undoHelper.ExecuteActionWithUndoRedo(sprite, sprite.ShiftPixelsDown);
        }
    }
}
