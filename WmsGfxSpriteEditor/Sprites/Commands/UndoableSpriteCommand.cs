using WmsGfxSpriteEditor.History;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal abstract class UndoableSpriteCommand
    {
        private readonly IHistory _history;

        protected UndoableSpriteCommand(IHistory history)
        {
            _history = history ?? throw new ArgumentNullException(nameof(history));
        }

        public abstract void Execute(ISprite sprite);

        protected void ExecuteActionWithUndoRedo(ISprite sprite, Action actionToExecute)
        {
            // for undo
            SnapshotPixelDataIfChanged(sprite);

            // execute action that may change pixel data
            actionToExecute();

            // for redo
            SnapshotPixelDataIfChanged(sprite);
        }

        /// <summary>
        /// Take a snapshot of the sprite's pixel data, if it has changed
        /// </summary>
        /// <param name="sprite"></param>
        private void SnapshotPixelDataIfChanged(ISprite sprite)
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