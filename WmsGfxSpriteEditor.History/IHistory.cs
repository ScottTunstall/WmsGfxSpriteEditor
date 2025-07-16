namespace WmsGfxSpriteEditor.History
{
    /// <summary>
    /// Interface for managing a history of changes, supporting undo/redo operations and querying history items.
    /// </summary>
    public interface IHistory
    {
        /// <summary>
        /// Points to the current item in history.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the total number of history items.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets a value indicating whether it is possible to go back in history.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets a value indicating whether it is possible to go forward in history.
        /// </summary>
        bool CanGoForward { get; }

        /// <summary>
        /// Moves back in history and returns the previous history item.
        /// </summary>
        /// <returns>The previous <see cref="HistoryItem"/>.</returns>
        HistoryItem Back();

        /// <summary>
        /// Moves forward in history and returns the next history item.
        /// </summary>
        /// <returns>The next <see cref="HistoryItem"/>.</returns>
        HistoryItem Forward();

        /// <summary>
        /// Adds a new history item to the history.
        /// </summary>
        /// <param name="item">The history item to add.</param>
        void Add(HistoryItem item);

        /// <summary>
        /// Returns the last <see cref="HistoryItem"/> in the history that matches the specified predicate, searching backward from a given start index.
        /// </summary>
        /// <param name="predicate">A function to test each <see cref="HistoryItem"/> for a condition.</param>
        /// <param name="startIndex">The index to start searching backward from. If less than 0, starts from the item before the current <see cref="Index"/>.</param>
        /// <returns>The last <see cref="HistoryItem"/> that matches the predicate, or <c>null</c> if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="startIndex"/> is 0 or greater than or equal to the number of history items.</exception>
        HistoryItem? Last(Predicate<HistoryItem> predicate, int startIndex = -1);



        /// <summary>
        /// Clears all history items.
        /// </summary>
        void Clear();
    }
}