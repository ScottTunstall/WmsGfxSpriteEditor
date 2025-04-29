using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    public enum OperationType
    {
        None = 0,
        Zoom,
        SpriteSelectionChanging,
        SpriteDataChanging,
    }


    public class HistoryItem
    {
        public OperationType OperationType { get; set; }
        public int SpriteIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PaletteIndex { get; set; }
        public ISprite Sprite { get; set; } = default!;
        public decimal ZoomValue { get; set; }

        public static HistoryItem CreateZoomHistoryItem(decimal zoomValue)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.Zoom,
                ZoomValue = zoomValue
            };
        }
        public static HistoryItem CreateSpriteSelectionChangingHistoryItem(int cboSpriteSelectedIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpriteSelectionChanging,
                SpriteIndex = cboSpriteSelectedIndex
            };
        }

        public static HistoryItem CreateSpriteDataChangingHistoryItem(int selectedSpriteIndex, ISprite spriteDataBeforeChange)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpriteDataChanging,
                SpriteIndex = selectedSpriteIndex,
                Sprite = spriteDataBeforeChange.Clone()
            };
        }


    }
}
