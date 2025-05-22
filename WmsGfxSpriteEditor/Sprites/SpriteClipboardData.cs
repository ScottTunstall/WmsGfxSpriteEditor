using System;

namespace WmsGfxSpriteEditor.Sprites
{
    [Serializable]
    public record SpriteClipboardData(int Width, int Height, int WidthInBytes, int BitsPerPixel, bool IsLinear, byte[] PixelData)
    {
        public static SpriteClipboardData FromSprite(ISprite sprite)
        {
            return new SpriteClipboardData(sprite!.Width, sprite.Height, sprite.WidthInBytes, sprite.BitsPerPixel, sprite.IsLinear, sprite.ClonePixelData());
        }
    }
}
