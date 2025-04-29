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

        public HistoryItem? Previous()
        {
            if (Index < 0)
            {
                return null;
            }

            HistoryItem item = _historyItems[Index];
            Index--;
            return item;
        }

        public HistoryItem? Next()
        {
            if (Index >= _historyItems.Count)
            {
                return null;
            }

            Index++;
            HistoryItem item = _historyItems[Index];
            return item;

        }

    }
}
