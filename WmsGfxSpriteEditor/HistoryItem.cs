using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsGfxSpriteEditor.Sprites;

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
        public SpriteInfo? SpriteInfo { get; set; }
        public decimal ZoomValue { get; set; }
        public byte[]? SpriteData { get; set; }
        public Color[]? Palette { get; set; }

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

        public static HistoryItem CreateSpriteDataChangingHistoryItem(SpriteInfo currentSpriteInfo, ISprite sprite, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpriteDataChanging,
                SpriteIndex = selectedSpriteIndex,
                SpriteData = sprite.CloneData(),
                Palette = sprite.ClonePalette(),
            };
        }
    }
}
