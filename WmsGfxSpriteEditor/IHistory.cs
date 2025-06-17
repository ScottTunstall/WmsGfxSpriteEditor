using WmsGfxSpriteEditor.History;

namespace WmsGfxSpriteEditor
{
    public interface IHistory
    {
        /// <summary>
        /// Points to the current item in history
        /// </summary>
        int Index { get; }

        int Count { get; }
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        void Add(HistoryItem item);

        /// <summary>
        /// Returns the last <see cref="HistoryItem"/> in the history that matches the specified predicate, searching backward from a given start index.
        /// </summary>
        /// <param name="predicate">A function to test each <see cref="HistoryItem"/> for a condition.</param>
        /// <param name="startIndex">The index to start searching backward from. If less than 0, starts from the item before the current <see cref="History.Index"/>.</param>
        /// <returns>The last <see cref="HistoryItem"/> that matches the predicate, or <c>null</c> if no match is found.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="startIndex"/> is 0 or greater than or equal to the number of history items.</exception>
        HistoryItem? Last(Predicate<HistoryItem> predicate, int startIndex = -1);

        HistoryItem? Back();
        HistoryItem? Forward();
        void Clear();
    }
}