namespace WmsGfxSpriteEditor.Sprites
{
    public class DefaultSpriteClipboardService : ISpriteClipboardService
    {
        public void Copy(ISprite sprite, Color[] palette)
        {
            if (sprite == null || palette.Length == 0)
            {
                throw new InvalidOperationException("No sprite to copy.");
            }

            DataObject dataObject = new();
            SpriteClipboardData clipboardData = SpriteClipboardData.FromSprite(sprite);
            dataObject.SetData("SpriteClipboard.ClipboardData", clipboardData);

            using Bitmap bmp = CreateBitmapFromSprite(sprite, palette);
            dataObject.SetImage(bmp);

            Clipboard.SetDataObject(dataObject, true);
        }


        // TODO: Probably not the best place to have this, keep for now..
        private Bitmap CreateBitmapFromSprite(ISprite sprite, Color[] palette)
        {
            // Create a Bitmap from the sprite and palette
            Bitmap bmp = new(sprite.Width, sprite.Height);

            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x = 0; x < sprite.Width; x++)
                {
                    int paletteIndex = sprite.GetPaletteIndexFromPixel(x, y);
                    Color color = palette[paletteIndex % palette.Length];
                    bmp.SetPixel(x, y, color);
                }
            }

            return bmp;
        }
    }
}
