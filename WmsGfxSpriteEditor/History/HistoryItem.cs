using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.History
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
        public int ZoomLevel { get; set; }
        public byte[]? SpriteData { get; set; }
        

        public static HistoryItem CreateZoomHistoryItem( int zoomLevel)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.Zoom,
                ZoomLevel = zoomLevel
            };
        }
        public static HistoryItem CreateSpriteSelectionChangingHistoryItem(int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpriteSelectionChanging,
                SpriteIndex = selectedSpriteIndex
            };
        }

        public static HistoryItem CreateSpriteDataChangingHistoryItem(ISprite sprite, int selectedSpriteIndex, int spriteOffset)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpriteDataChanging,
                SpriteIndex = selectedSpriteIndex,
                SpriteData = sprite.ClonePixelData(),
            };
        }
    }
}
