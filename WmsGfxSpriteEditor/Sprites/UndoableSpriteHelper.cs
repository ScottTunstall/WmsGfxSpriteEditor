using WmsGfxSpriteEditor.History;

namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Helper class for sprite commands to provide undo/redo support via composition.
    /// </summary>
    internal class UndoableSpriteHelper
    {
        private readonly IHistory _history;

        public UndoableSpriteHelper(IHistory history)
        {
            _history = history ?? throw new ArgumentNullException(nameof(history));
        }

        public void ExecuteActionWithUndoRedo(ISprite sprite, Action actionToExecute)
        {
            sprite.ClearPixelDataDirtyFlag();

            // for undo
            SnapshotPixelData(sprite);

            // execute action that may change pixel data
            actionToExecute();

            // for redo - has sprite data changed?
            if (sprite.IsPixelDataDirty)
            {
                SnapshotPixelData(sprite);
                sprite.ClearPixelDataDirtyFlag();
            }
        }

        /// <summary>
        /// Take a snapshot of the sprite's pixel data, if it has changed
        /// </summary>
        /// <param name="sprite"></param>
        public void SnapshotPixelData(ISprite sprite)
        {
            HistoryItem? historyItem = _history.Last(x => x.SpriteIndex == sprite.SpriteIndex && x.OperationType == OperationType.SpritePixelDataSnapshot);
            UInt128 spriteHash = sprite.GetPixelDataHash();

            if (historyItem == null || historyItem.PixelDataHash != spriteHash)
            {
                _history.Add(HistoryItem.CreateSpritePixelDataChangedHistoryItem(sprite.ClonePixelData(), spriteHash, sprite.SpriteIndex));
            }
        }
    }
}