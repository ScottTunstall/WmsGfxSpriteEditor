using System.Drawing;

namespace WmsGfxSpriteEditor.Sprites
{
    public interface ISprite
    {
        // We use Memory<byte> so that we can share memory between the Sprite being edited and the MemoryStream it originates from
        Memory<byte> PixelData { get; }

        int BitsPerPixel { get; }
        int Width { get; }
        int WidthInBytes { get; }
        int Height { get; }
        Size Size => new(Width, Height);

        bool IsLinear { get; }

        bool IsPixelDataDirty { get; }

        void ClearPixelDataDirtyFlag();

        bool IsInBounds(int x, int y);

        int GetPaletteIndexFromPixel(int x, int y);

        void SetPixelByPaletteIndex(int x, int y, int paletteIndex);

        byte[] ClonePixelData();

        UInt128 GetPixelDataHash();
    }
}