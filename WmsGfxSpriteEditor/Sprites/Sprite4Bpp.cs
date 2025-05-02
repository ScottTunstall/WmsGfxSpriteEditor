using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public class Sprite4Bpp : ISprite
    {
        public Sprite4Bpp(byte[] pixelData, Color[] palette, int widthInBytes, int height, bool isLinear = true)
        {
            PixelData = pixelData;
            Palette = palette;
            WidthInBytes = widthInBytes;
            Width = widthInBytes * 2; // Each byte contains 2 pixels
            Height = height;
            IsLinear = isLinear;
        }

        public byte[] PixelData { get; set; } = default!;
        public Color[] Palette { get; set; } = default!;

        public int Width { get; set; } 
        public int WidthInBytes { get; set; }
        public int Height { get; set; }
        public bool IsLinear { get; set; }


        public int GetPaletteIndexFromPixel(int x, int y)
        {
            int offset = y * WidthInBytes + (x / 2);
            byte pixelByte = PixelData[offset];
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

        public Color GetPixel(int x, int y)
        {
            int paletteIndex = GetPaletteIndexFromPixel(x, y);
            return Palette[paletteIndex];
        }


        public void SetPixelByPaletteIndex(int x, int y, int paletteIndex)
        {
            int offset = y * WidthInBytes + (x/ 2);
            paletteIndex &= 0x0F; // Ensure palette index is within bounds (0-15)
            if (x % 2 == 0)
            {
                // Set the upper nibble (first pixel)
                PixelData[offset] = (byte)((PixelData[offset] & 0x0F) | paletteIndex << 4);
            }
            else
            {
                // Set the lower nibble (second pixel)
                PixelData[offset] = (byte)((PixelData[offset] & 0xF0) | paletteIndex);
            }

        }

        public ISprite Clone()
        {
            byte[] dataCopy = ClonePixelData();
            Color[] paletteCopy = ClonePalette();

            return new Sprite4Bpp(dataCopy, paletteCopy, WidthInBytes, Height, IsLinear);
        }

        public byte[] ClonePixelData()
        {
            byte[] dataCopy = new byte[PixelData.Length];
            Array.Copy(PixelData, dataCopy, PixelData.Length);
            return dataCopy;
        }

        public Color[] ClonePalette()
        {
            Color[] paletteCopy = new Color[Palette.Length];
            Array.Copy(Palette, paletteCopy, Palette.Length);
            return paletteCopy;
        }
    }
}