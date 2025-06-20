using System.Diagnostics;

namespace WmsGfxSpriteEditor.History
{
    public class History : IHistory
    {
        private readonly List<HistoryItem> _historyItems = new();

        /// <summary>
        /// Points to the current item in history
        /// </summary>
        public int Index { get; private set; } = -1;

        public int Count => _historyItems.Count;

        public void Add(HistoryItem item)
        {
            // If Index is not equal to the last item in the list, the user has gone back in history with the undo function.
            // So, we need to replace *all* of the history starting from Index+1, with the new item
            if (Index < _historyItems.Count - 1)
            {
                Debug.WriteLine("History index is {0}, removing items from {1} to {2}", Index, Index + 1, _historyItems.Count - 1);
                // Remove all items after the current index
                _historyItems.RemoveRange(Index + 1, _historyItems.Count - Index - 1);
            }

            _historyItems.Add(item);
            Index = _historyItems.Count - 1;

            DumpHistory();
        }

        /// <summary>
        /// Returns the last <see cref="HistoryItem"/> in the history that matches the specified predicate, searching backward from a given start index.
        /// </summary>
        /// <param name="predicate">A function to test each <see cref="HistoryItem"/> for a condition.</param>
        /// <param name="startIndex">The index to start searching backward from. If less than 0, starts from the item before the current <see cref="Index"/>.</param>
        /// <returns>The last <see cref="HistoryItem"/> that matches the predicate, or <c>null</c> if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="startIndex"/> is 0 or greater than or equal to the number of history items.</exception>
        public HistoryItem? Last(Predicate<HistoryItem> predicate, int startIndex = -1)
        {
            if (startIndex < 0)
            {
                startIndex = Index; // Start from the last added item
            }

            if (startIndex >= _historyItems.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index is out of range.");
            }

            Debug.WriteLine("Looking for {0} starting from {1}", predicate, startIndex);

            for (int i = startIndex; i >= 0; i--)
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
                throw new InvalidOperationException($"Cannot go back, {nameof(Index)} is zero.");
            }

            --Index;
            Debug.WriteLine("Back() - History index is now {0}", Index);
            DumpHistory();
            HistoryItem item = _historyItems[Index];
            return item;
        }

        public HistoryItem? Forward()
        {
            if (Index >= _historyItems.Count - 1)
            {
                throw new InvalidOperationException($"Cannot go forward, {nameof(Index)} is at the end of the history.");
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
            for (int i = 0; i < _historyItems.Count; i++)
            {
                HistoryItem item = _historyItems[i];
                Debug.WriteLine("Item {0}: {1}", i, item);
            }
        }
    }
}