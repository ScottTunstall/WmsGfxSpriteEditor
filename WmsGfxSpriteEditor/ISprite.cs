using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public interface ISprite
    {
        byte[] Data { get; set; }
        Color[] Palette { get; set; }
        int Width { get; set; }
        int WidthInBytes { get; set; }
        int Height { get; set; }
        bool IsLinear { get; set; }

        Color GetPixel(int x, int y);
        int GetPaletteIndex(int x, int y);
        void SetPixelByPaletteIndex(int x, int y, int paletteIndex);
        ISprite Clone();
    }

}