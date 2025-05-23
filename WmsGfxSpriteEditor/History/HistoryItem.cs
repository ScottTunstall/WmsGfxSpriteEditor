namespace WmsGfxSpriteEditor.History
{
    public enum OperationType
    {
        None = 0,
        BeforeSpritePixelDataChanged,
        AfterSpritePixelDataChanged,
    }

    public record HistoryItem
    {
        public required OperationType OperationType { get; init; }
        public required int SpriteIndex { get; init; }
        public byte[]? PixelData { get; set; }
        public UInt128? PixelDataHash { get; set; }



        public static HistoryItem CreateBeforeSpritePixelDataChangedHistoryItem(byte[] pixelDataBeforeChangeMade, UInt128 hash, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.BeforeSpritePixelDataChanged,
                SpriteIndex = selectedSpriteIndex,
                PixelData = pixelDataBeforeChangeMade,
                PixelDataHash = hash,
            };
        }

        public static HistoryItem CreateAfterSpritePixelDataChangedHistoryItem(byte[] pixelDataAfterChangeMade, UInt128 hash, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.AfterSpritePixelDataChanged,
                SpriteIndex = selectedSpriteIndex,
                PixelData = pixelDataAfterChangeMade,
                PixelDataHash = hash
            };
        }
    }
}