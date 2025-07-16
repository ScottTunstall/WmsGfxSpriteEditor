using System.Diagnostics;

namespace WmsGfxSpriteEditor.History
{
    /// <summary>
    /// Concrete implementation of <see cref="IHistory"/> for managing a list of history items and supporting undo/redo operations.
    /// </summary>
    public class History : IHistory
    {
        private readonly List<HistoryItem> _historyItems = new();

        /// <inheritdoc/>
        public int Index { get; private set; } = -1;

        /// <inheritdoc/>
        public int Count => _historyItems.Count;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool CanGoBack => Index > 0;

        /// <inheritdoc/>
        public bool CanGoForward => Index < (_historyItems.Count - 1);

        /// <inheritdoc/>
        public HistoryItem Back()
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

        /// <inheritdoc/>
        public HistoryItem Forward()
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

        /// <inheritdoc/>
        public void Clear()
        {
            _historyItems.Clear();
            Index = -1;
            DumpHistory();
        }

        /// <summary>
        /// Writes the current history state to the debug output (only in DEBUG builds).
        /// </summary>
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