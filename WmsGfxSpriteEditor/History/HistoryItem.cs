namespace WmsGfxSpriteEditor.History
{
    public enum OperationType
    {
        None = 0,
        SelectedSpriteChanged,
        BeforeSpritePixelDataChanged,
        AfterSpritePixelDataChanged,
    }

    public record HistoryItem
    {
        public required OperationType OperationType { get; init; }
        public required int SpriteIndex { get; init; }
        public byte[]? PixelData { get; set; }
        public UInt128? PixelDataHash { get; set; }

        public static HistoryItem CreateSpriteSelectionChangedHistoryItem(int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.SelectedSpriteChanged,
                SpriteIndex = selectedSpriteIndex
            };
        }

        public static HistoryItem CreateBeforeSpritePixelDataChangedHistoryItem(ISprite sprite, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.BeforeSpritePixelDataChanged,
                SpriteIndex = selectedSpriteIndex,
                PixelData = sprite.ClonePixelData(),
                PixelDataHash = sprite.GetPixelDataHash(),
            };
        }

        public static HistoryItem CreateAfterSpritePixelDataChangedHistoryItem(ISprite sprite, int selectedSpriteIndex)
        {
            return new HistoryItem()
            {
                OperationType = OperationType.AfterSpritePixelDataChanged,
                SpriteIndex = selectedSpriteIndex,
                PixelData = sprite.ClonePixelData(),
                PixelDataHash = sprite.GetPixelDataHash()
            };
        }
    }
}