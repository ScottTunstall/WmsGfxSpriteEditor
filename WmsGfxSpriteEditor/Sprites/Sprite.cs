using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public record Sprite
    {
        public Sprite(byte[] data, Color[] palette, int widthInBytes, int height, bool isLinear = true)
        {
            Data = data;
            Palette = palette;
            WidthInBytes = widthInBytes;
            Height = height;
            IsLinear = isLinear;
        }

        public byte[] Data { get; set; } = default!;
        public Color[] Palette { get; set; } = default!;
        public int WidthInBytes { get; set; }
        public int Height { get; set; }
        public bool IsLinear { get; set; }

        public Color GetFirstPixelColour(int dataIndex)
        {
            byte pixelByte = Data[dataIndex];
            int index = (pixelByte >> 4) & 0x0F; // First pixel (upper nibble)
            return Palette[index];
        }
        
        public Color GetSecondPixelColour(int dataIndex)
        {
            byte pixelByte = Data[dataIndex];
            int index = pixelByte & 0x0F;       // Second pixel (lower nibble)
            return Palette[index];
        }
    }
}