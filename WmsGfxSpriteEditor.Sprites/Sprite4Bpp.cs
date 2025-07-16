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

        public Sprite4Bpp(int spriteIndex, Memory<byte> pixelData, int widthInBytes, int height, Color[] palette, bool isLinear = true)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(spriteIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(widthInBytes, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(height, 1);

            if (pixelData.Length != widthInBytes * height)
            {
                throw new ArgumentException($"Pixel data length {pixelData.Length} does not match expected size {widthInBytes * height} for width {widthInBytes} and height {height}.");
            }

            SpriteIndex = spriteIndex;
            PixelData = pixelData;
            WidthInBytes = widthInBytes;
            Height = height;
            Palette = palette;
            IsLinear = isLinear;
        }

        public int BitsPerPixel => 4;

        public int SpriteIndex { get; }
        public Memory<byte> PixelData { get; }
        public object Tag { get; set; } = default!;
        public int Width => WidthInBytes * 2; // Each byte contains 2 pixels
        public int WidthInBytes { get; }
        public int Height { get; }
        public Size Size => new(Width, Height);
        public Color[] Palette { get; }
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

            // Flip by swapping columns using SetPixelByPaletteIndex
            // We do this because we want the dirty flag to be set if the pixel data is changed
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width / 2; x++)
                {
                    int oppositeX = Width - 1 - x;
                    int leftPalette = GetPaletteIndexFromPixel(x, y);
                    int rightPalette = GetPaletteIndexFromPixel(oppositeX, y);
                    // Swap
                    SetPixelByPaletteIndex(x, y, rightPalette);
                    SetPixelByPaletteIndex(oppositeX, y, leftPalette);
                }
            }
        }

        public void YFlip()
        {
            // No need to flip a 1px high sprite
            if (Height <= 1)
            {
                return;
            }

            // Flip by swapping rows using SetPixelByPaletteIndex
            for (int y = 0; y < Height / 2; y++)
            {
                int oppositeY = Height - 1 - y;
                for (int x = 0; x < Width; x++)
                {
                    int topPalette = GetPaletteIndexFromPixel(x, y);
                    int bottomPalette = GetPaletteIndexFromPixel(x, oppositeY);
                    // Swap
                    SetPixelByPaletteIndex(x, y, bottomPalette);
                    SetPixelByPaletteIndex(x, oppositeY, topPalette);
                }
            }
        }

        public void ShiftPixelsUp()
        {
            if (Height > 1)
            {
                // Shift all rows up by copying the row below
                for (int y = 0; y < Height - 1; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        int belowPalette = GetPaletteIndexFromPixel(x, y + 1);
                        SetPixelByPaletteIndex(x, y, belowPalette);
                    }
                }
            }

            // Set the bottom row to palette index 0
            int lastRow = Height - 1;
            for (int x = 0; x < Width; x++)
            {
                SetPixelByPaletteIndex(x, lastRow, 0);
            }
        }

        public void ShiftPixelsDown()
        {
            if (Height > 1)
            {
                // Shift all rows down by copying the row above
                for (int y = Height - 1; y > 0; y--)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        int abovePalette = GetPaletteIndexFromPixel(x, y - 1);
                        SetPixelByPaletteIndex(x, y, abovePalette);
                    }
                }
            }

            // Set the top row to palette index 0
            for (int x = 0; x < Width; x++)
            {
                SetPixelByPaletteIndex(x, 0, 0);
            }
        }

        public void ShiftPixelsLeft()
        {
            if (Width > 1)
            {
                // Shift all columns left by copying the column to the right
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width - 1; x++)
                    {
                        int rightPalette = GetPaletteIndexFromPixel(x + 1, y);
                        SetPixelByPaletteIndex(x, y, rightPalette);
                    }
                }
            }

            // Set the rightmost column to palette index 0
            int lastCol = Width - 1;
            for (int y = 0; y < Height; y++)
            {
                SetPixelByPaletteIndex(lastCol, y, 0);
            }
        }

        public void ShiftPixelsRight()
        {
            if (Width > 1)
            {
                // Shift all columns right by copying the column to the left
                for (int y = 0; y < Height; y++)
                {
                    for (int x = Width - 1; x > 0; x--)
                    {
                        int leftPalette = GetPaletteIndexFromPixel(x - 1, y);
                        SetPixelByPaletteIndex(x, y, leftPalette);
                    }
                }
            }

            // Set the leftmost column to palette index 0
            for (int y = 0; y < Height; y++)
            {
                SetPixelByPaletteIndex(0, y, 0);
            }
        }

        public Bitmap CreateBitmapFromSprite()
        {
            Bitmap bmp = new(Width, Height);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int paletteIndex = GetPaletteIndexFromPixel(x, y);
                    Color color = Palette[paletteIndex % Palette.Length];
                    bmp.SetPixel(x, y, color);
                }
            }

            return bmp;
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