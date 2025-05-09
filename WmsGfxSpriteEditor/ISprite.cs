namespace WmsGfxSpriteEditor
{
    public interface ISprite
    {
        // We use Memory<byte> so that we can share memory between the Sprite being edited and the MemoryStream it originates from
        Memory<byte> PixelData { get; set; }
        Color[] Palette { get; set; }
        int Width { get; set; }
        int WidthInBytes { get; set; }
        int Height { get; set; }
        bool IsLinear { get; set; }

        Color GetPixel(int x, int y);
        int GetPaletteIndexFromPixel(int x, int y);
        void SetPixelByPaletteIndex(int x, int y, int paletteIndex);
        ISprite Clone();

        byte[] ClonePixelData();
        Color[] ClonePalette();
    }

}