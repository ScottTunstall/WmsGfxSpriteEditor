using System.Security.Cryptography;

namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Represents a sprite which has 4 bits per pixel (4BPP) colour depth.
    /// </summary>
    public class Sprite4Bpp : ISprite
    {
        private Sprite4Bpp()
        { }

        public Sprite4Bpp(Memory<byte> pixelData, int widthInBytes, int height, bool isLinear = true)
        {
            PixelData = pixelData;
            WidthInBytes = widthInBytes;
            Width = widthInBytes * 2; // Each byte contains 2 pixels
            Height = height;
            IsLinear = isLinear;
        }

        public int BitsPerPixel => 4;

        public Memory<byte> PixelData { get; set; }

        public int Width { get; set; }
        public int WidthInBytes { get; set; }
        public int Height { get; set; }
        public bool IsLinear { get; set; }

        /// <summary>
        /// Flag to indicate if the pixel data has been modified.
        /// </summary>
        public bool IsPixelDataDirty { get; private set; }

        

        /// <summary>
        /// Clears the pixel data dirty flag.
        /// </summary>
        public void ClearPixelDataDirtyFlag()
        {
            IsPixelDataDirty = false;
        }

        public bool IsInBounds(int x, int y)
        {
            return (x > -1 && x < Width) && (y > -1 && y < Height);
        }

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

            byte currentPixelData = span[offset];
            byte newPixelData;

            if (x % 2 == 0)
            {
                // Set the upper nibble (first pixel)
                newPixelData = (byte)((currentPixelData & 0x0F) | paletteIndex << 4);
            }
            else
            {
                // Set the lower nibble (second pixel)
                newPixelData = (byte)((currentPixelData & 0xF0) | paletteIndex);
            }

            // Will the pixel pair change? If so, set the dirty flag
            if (newPixelData != currentPixelData)
            {
                IsPixelDataDirty = true;

                span[offset] = newPixelData;
            }
        }

        public byte[] ClonePixelData()
        {
            byte[] dataCopy = new byte[PixelData.Span.Length];
            Array.Copy(PixelData.Span.ToArray(), dataCopy, PixelData.Span.Length);
            return dataCopy;
        }

        public UInt128 GetPixelDataHash()
        {
            byte[] data = PixelData.ToArray();
            byte[] hash = SHA256.HashData(data); // 16 bytes

            ulong lo = BitConverter.ToUInt64(hash, 0);
            ulong hi = BitConverter.ToUInt64(hash, 8);
            return ((UInt128)hi << 64) | lo;
        }
    }
}