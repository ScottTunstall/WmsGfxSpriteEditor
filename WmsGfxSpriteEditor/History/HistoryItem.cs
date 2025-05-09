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


    public record HistoryItem
    {
        public OperationType OperationType { get; set; }
        public int SpriteIndex { get; set; }
        public int ZoomLevel { get; set; }
        public byte[]? PixelData { get; set; }


        // Override Equals to handle byte[] comparison
        public virtual bool Equals(HistoryItem? other)
        {
            if (other is null) return false;

            if (OperationType != other.OperationType ||
                SpriteIndex != other.SpriteIndex ||
                ZoomLevel != other.ZoomLevel)
                return false;

            // Are both PixelData set to null. Then compare succeeds
            if (PixelData == null && other.PixelData == null)
                return true;

            // One of the PixelData is null, but not both
            if (PixelData == null || other.PixelData == null)
                return false;
            
            // Compare array contents
            return PixelData!.SequenceEqual(other.PixelData);
        }

        // Override GetHashCode to be consistent with Equals
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + OperationType.GetHashCode();
                hash = hash * 23 + SpriteIndex.GetHashCode();
                hash = hash * 23 + ZoomLevel.GetHashCode();

                // Include SpriteData in hash calculation if it exists
                if (PixelData != null)
                {
                    foreach (var b in PixelData)
                    {
                        hash = hash * 23 + b.GetHashCode();
                    }
                }

                return hash;
            }
        }


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
                PixelData = sprite.ClonePixelData(),
            };
        }
    }
}
