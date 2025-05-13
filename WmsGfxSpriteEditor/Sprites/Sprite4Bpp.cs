namespace WmsGfxSpriteEditor.Sprites
{
    public class Sprite4Bpp : ISprite
    {
        public Sprite4Bpp(Memory<byte> pixelData, int widthInBytes, int height, bool isLinear = true)
        {
            PixelData = pixelData;
            WidthInBytes = widthInBytes;
            Width = widthInBytes * 2; // Each byte contains 2 pixels
            Height = height;
            IsLinear = isLinear;
        }

        public Memory<byte> PixelData { get; set; } = default!;

        public int Width { get; set; }
        public int WidthInBytes { get; set; }
        public int Height { get; set; }
        public bool IsLinear { get; set; }

        public int GetPaletteIndexFromPixel(int x, int y)
        {
            int offset = y * WidthInBytes + (x / 2);
            byte pixelByte = PixelData.Span[offset];
            if (x % 2 == 0)
            {
                // Get the upper nibble (first pixel)
                return (pixelByte >> 4) & 0x0F;
            }
            else
            {
                // Get the lower nibble (second pixel)
                return pixelByte & 0x0F;
            }
        }

        public void SetPixelByPaletteIndex(int x, int y, int paletteIndex)
        {
            int offset = y * WidthInBytes + (x / 2);
            paletteIndex &= 0x0F; // Ensure palette index is within bounds (0-15)
            Span<byte> span = PixelData.Span;

            if (x % 2 == 0)
            {
                // Set the upper nibble (first pixel)
                span[offset] = (byte)((span[offset] & 0x0F) | paletteIndex << 4);
            }
            else
            {
                // Set the lower nibble (second pixel)
                span[offset] = (byte)((PixelData.Span[offset] & 0xF0) | paletteIndex);
            }
        }

        public byte[] ClonePixelData()
        {
            byte[] dataCopy = new byte[PixelData.Span.Length];
            Array.Copy(PixelData.Span.ToArray(), dataCopy, PixelData.Span.Length);
            return dataCopy;
        }
    }
}