namespace WmsGfxSpriteEditor.Extensions
{
    internal static class ToolStripItemCollectionExtensions
    {
        public static bool Any(this ToolStripItemCollection items, Func<ToolStripItem, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(predicate);

            foreach (ToolStripItem item in items)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
