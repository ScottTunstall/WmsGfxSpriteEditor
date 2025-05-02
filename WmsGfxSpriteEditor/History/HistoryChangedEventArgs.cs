using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.History
{
    public class HistoryChangedEventArgs: EventArgs
    {
        public HistoryChangedEventArgs(HistoryItem itemAdded, int historyItemsCount)
        {
            ItemAdded = itemAdded;
            ItemCount = historyItemsCount;
        }

        public HistoryItem ItemAdded { get; }
        public int ItemCount { get; }
    }
}
