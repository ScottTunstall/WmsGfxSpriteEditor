using System.Drawing;
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

        public Sprite4Bpp(int spriteIndex, Memory<byte> pixelData, int widthInBytes, int height, bool isLinear = true)
        {
            if (pixelData.Length != widthInBytes * height)
            {
                throw new ArgumentException($"Pixel data length {pixelData.Length} does not match expected size {widthInBytes * height} for width {widthInBytes} and height {height}.");
            }

            ArgumentOutOfRangeException.ThrowIfLessThan(widthInBytes,1);
            ArgumentOutOfRangeException.ThrowIfLessThan(height, 1);

            SpriteIndex = spriteIndex; 
            PixelData = pixelData;
            WidthInBytes = widthInBytes;
            Height = height;
            IsLinear = isLinear;
        }

        public int BitsPerPixel => 4;

        public int SpriteIndex { get; }
        public Memory<byte> PixelData { get; }
        public int Width => WidthInBytes * 2; // Each byte contains 2 pixels
        public int WidthInBytes { get; }
        public int Height { get; }
        public Size Size => new(Width, Height);
        public bool IsLinear { get; }

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

        public void XFlip()
        {
            // No need to flip a 1px wide sprite
            if (Width <= 1)
            {
                return;
            }

            byte[] newSpriteData = new byte[PixelData.Span.Length];
            
            int sourceOffset = 0;
            for (int y = 0; y < Height; y++)
            {
                int destOffset = sourceOffset + WidthInBytes - 1;
                for (int x = 0; x < WidthInBytes; x++)
                {
                    byte leftByte = PixelData.Span[sourceOffset + x];
                    byte swapped = leftByte.SwapNibbles();
                    newSpriteData[destOffset--] = swapped;
                }

                sourceOffset += WidthInBytes;
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
        }

        public void YFlip()
        {
            // No need to flip a 1px high sprite
            if (Height <= 1) 
            {
                return;
            }

            byte[] newSpriteData = new byte[PixelData.Span.Length];
            int sourceOffset = 0;
            int destOffset = PixelData.Span.Length - WidthInBytes;
            for (int y = 0; y < Height; y++)
            {
                Span<byte> source = PixelData.Span.Slice(sourceOffset, WidthInBytes);
                Span<byte> destination = newSpriteData.AsSpan(destOffset, WidthInBytes);
                source.CopyTo(destination);
                sourceOffset += WidthInBytes;
                destOffset -= WidthInBytes;
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
        }

        public void ShiftPixelsUp()
        {
            byte[] newSpriteData = new byte[PixelData.Span.Length];
            if (Height > 1)
            {
                Span<byte> source = PixelData.Span.Slice(WidthInBytes);
                Span<byte> destination = newSpriteData.AsSpan(0, PixelData.Span.Length - WidthInBytes);
                source.CopyTo(destination);
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
        }

        public void ShiftPixelsDown()
        {
            byte[] newSpriteData = new byte[PixelData.Span.Length];
            if (Height > 1)
            {
                Span<byte> source = PixelData.Span.Slice(0, PixelData.Span.Length - WidthInBytes);
                Span<byte> destination = newSpriteData.AsSpan(WidthInBytes, PixelData.Span.Length - WidthInBytes);
                source.CopyTo(destination);
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
        }

        public void ShiftPixelsLeft()
        {
            byte[] newSpriteData = new byte[PixelData.Span.Length];

            if (Width > 1)
            {
                int sourceOffset = 0;
                for (int y = 0; y < Height; y++)
                {
                    int destOffset = sourceOffset;
                    // Shift each byte left by one pixel (nibble)
                    for (int x = 0; x < WidthInBytes - 1; x++)
                    {
                        byte leftByte = PixelData.Span[sourceOffset + x];
                        byte rightByte = PixelData.Span[sourceOffset + x + 1];
                        byte upperNibble = (byte)(leftByte << 4);
                        byte lowerNibble = (byte)(rightByte >> 4);

                        byte shifted = (byte)(upperNibble | lowerNibble);
                        newSpriteData[destOffset++] = shifted;
                    }
                    // Handle the last byte in the row
                    byte lastByte = PixelData.Span[sourceOffset + WidthInBytes - 1];
                    newSpriteData[destOffset] = (byte)(lastByte << 4);
                    sourceOffset += WidthInBytes;
                }
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
        }

        public void ShiftPixelsRight()
        {
            byte[] newSpriteData = new byte[PixelData.Span.Length];

            if (Width > 1)
            {
                int sourceOffset = 0;
                for (int y = 0; y < Height; y++)
                {
                    int destOffset = sourceOffset + WidthInBytes - 1;
                    // Shift each byte right by one pixel (nibble)
                    for (int x = WidthInBytes - 1; x > 0; x--)
                    {
                        byte leftByte = PixelData.Span[sourceOffset + x - 1];
                        byte rightByte = PixelData.Span[sourceOffset + x];
                        byte upperNibble = (byte)(leftByte << 4);
                        byte lowerNibble = (byte)(rightByte >> 4);
                        byte shifted = (byte)(upperNibble | lowerNibble);
                        newSpriteData[destOffset--] = shifted;
                    }

                    // Handle the first byte in the row
                    byte firstByte = PixelData.Span[sourceOffset];
                    newSpriteData[destOffset] = (byte)(firstByte >> 4);
                    sourceOffset += WidthInBytes;
                }
            }

            newSpriteData.CopyTo(PixelData.Span);
            IsPixelDataDirty = true;
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