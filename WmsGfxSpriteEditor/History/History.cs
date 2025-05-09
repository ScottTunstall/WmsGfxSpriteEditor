namespace WmsGfxSpriteEditor.History
{
    public class History
    {
        private readonly List<HistoryItem> _historyItems = new();
        

        /// <summary>
        /// Points to the current item in history
        /// </summary>
        public int Index { get; set; } = -1;

        public void Add(HistoryItem item)
        {
            if (Index < _historyItems.Count - 1)
            {
                // Remove all items after the current index
                _historyItems.RemoveRange(Index, _historyItems.Count - Index - 1);
            }

            // Check if the item is the same as the last one added. We don't want duplicates in our history - pointless
            if (Index > 0)
            {
                if (item.Equals(_historyItems[Index]))
                {
                    return;
                }
            }

            _historyItems.Add(item);
            Index++;
        }

        public bool CanGoBack => Index > 0;

        public bool CanGoForward => Index < (_historyItems.Count - 1);

        public HistoryItem? Back()
        {
            if (Index == 0)
            {
                return null;
            }

            HistoryItem item = _historyItems[--Index];
            return item;
        }

        public HistoryItem? Forward()
        {
            if (Index >= _historyItems.Count)
            {
                return null;
            }

            HistoryItem item = _historyItems[++Index];
            return item;
        }

        public void Clear()
        {
            _historyItems.Clear();
            Index = -1;
        }
    }
}