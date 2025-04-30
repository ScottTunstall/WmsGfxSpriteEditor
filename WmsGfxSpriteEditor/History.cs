using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    public class History
    {
        private List<HistoryItem> _historyItems = new();

        public int Index { get; set; } = -1;

        public void Add(HistoryItem item)
        {
            if (Index < _historyItems.Count - 1)
            {
                // Remove all items after the current index
                _historyItems.RemoveRange(Index + 1, _historyItems.Count - Index - 1);
            }

            _historyItems.Add(item);
            Index++;
        }

        public bool CanUndo => Index > 0;

        public bool CanRedo => Index < _historyItems.Count - 1;

        public HistoryItem? Undo()
        {
            if (Index < 1)
            {
                return null;
            }

            Index--;
            HistoryItem item = _historyItems[Index];
            return item;
        }

        public HistoryItem? Redo()
        {
            if (Index >= _historyItems.Count)
            {
                return null;
            }

            Index++;
            HistoryItem item = _historyItems[Index];
            return item;
        }


        public void Clear()
        {
            _historyItems.Clear();
            Index = -1;
        }

    }
}
