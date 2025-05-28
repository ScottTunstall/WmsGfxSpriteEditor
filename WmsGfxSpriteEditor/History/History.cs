using System.Diagnostics;

namespace WmsGfxSpriteEditor.History
{
    public class History
    {
        private readonly List<HistoryItem> _historyItems = new();

        /// <summary>
        /// Points to the current item in history
        /// </summary>
        public int Index { get; private set; } = -1;

        public void Add(HistoryItem item)
        {
            // If Index is not equal to the last item in the list, the user has gone back in history with the undo function. 
            // So, we need to replace *all* of the history starting from Index+1, with the new item
            if (Index < _historyItems.Count - 1)
            {
                Debug.WriteLine("History index is {0}, removing items from {1} to {2}", Index, Index + 1, _historyItems.Count - 1);
                // Remove all items after the current index
                _historyItems.RemoveRange(Index+1, _historyItems.Count - Index - 1);
            }

            _historyItems.Add(item);
            Index = _historyItems.Count-1;

            DumpHistory();
        }

        public HistoryItem? Last(Func<HistoryItem, bool> predicate)
        {
            for (int i = _historyItems.Count - 1; i >= 0; i--)
            {
                HistoryItem item = _historyItems[i];
                if (predicate(item))
                {
                    return item;
                }
            }
            return null;
        }


        public bool CanGoBack => Index > 0;

        public bool CanGoForward => Index < (_historyItems.Count - 1);

        public HistoryItem? Back()
        {
            if (Index == 0)
            {
                return null;
            }

            --Index;
            Debug.WriteLine("Back() - History index is now {0}", Index);
            DumpHistory();
            HistoryItem item = _historyItems[Index];
            return item;
        }

        public HistoryItem? Forward()
        {
            if (Index >= _historyItems.Count)
            {
                return null;
            }

            Index++;
            Debug.WriteLine("Forward() - History index is now {0}", Index);
            DumpHistory();
            HistoryItem item = _historyItems[Index];
            return item;
        }

        public void Clear()
        {
            _historyItems.Clear();
            Index = -1;
            DumpHistory();
        }



        [Conditional("DEBUG")]
        private void DumpHistory()
        {
            Debug.WriteLine("History dump:");
            Debug.WriteLine("Index: {0}", Index);
            for (int i=0;i< _historyItems.Count; i++)
            {
                HistoryItem item = _historyItems[i];
                Debug.WriteLine("Item {0}: {1}", i, item);
            }
        }
    }
}