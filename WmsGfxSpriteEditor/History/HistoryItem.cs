namespace WmsGfxSpriteEditor.History
{
    public enum OperationType
    {
        None = 0,
        SpritePixelDataSnapshot
    }

    public record HistoryItem
    {
        public required OperationType OperationType { get; init; }
        public required int SpriteIndex { get; init; }
        public byte[]? PixelData { get; set; }
        public UInt128? PixelDataHash { get; set; }
        


        public static HistoryItem CreateSpritePixelDataChangedHistoryItem(byte[] pixelDataBeforeChangeMade, UInt128 hash, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SpritePixelDataSnapshot,
                SpriteIndex = selectedSpriteIndex,
                PixelData = pixelDataBeforeChangeMade,
                PixelDataHash = hash,
            };
        }
    }
}