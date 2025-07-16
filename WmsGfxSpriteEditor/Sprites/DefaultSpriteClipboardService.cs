using System.Drawing.Imaging;
using WmsGfxSpriteEditor.History;
using WmsGfxSpriteEditor.Sprites.Commands;

namespace WmsGfxSpriteEditor.Sprites
{
    public class DefaultSpriteClipboardService : ISpriteClipboardService
    {
        private readonly IHistory _history;

        public DefaultSpriteClipboardService(IHistory history)
        {
            _history = history ?? throw new ArgumentNullException(nameof(history));
        }

        public void Copy(ISprite source)
        {
            ArgumentNullException.ThrowIfNull(source);

            using Bitmap bmp = source.CreateBitmapFromSprite();
            Clipboard.SetImage(bmp);
        }

        public void Paste(ISprite target)
        {
            ArgumentNullException.ThrowIfNull(target);

            if (!TryGetCompatibleBitmap(target, out Bitmap? bitmap, out Color[] _))
            {
                throw new InvalidOperationException("No compatible image in clipboard.");
            }

            new SetSpritePixelsFromBitmapCommand(_history).Execute(bitmap!, target);
        }

        public bool HasCompatibleBitmap(ISprite target)
        {
            return TryGetCompatibleBitmap(target, out _, out _);
        }

        private bool TryGetCompatibleBitmap(ISprite target, out Bitmap? bitmap, out Color[] palette)
        {
            ArgumentNullException.ThrowIfNull(target);

            bitmap = null;
            palette = [];

            if (!Clipboard.ContainsImage())
            {
                return false;
            }

            Image clipboardImage = Clipboard.GetImage()!;
            if (clipboardImage is not Bitmap clipboardBmp)
            {
                return false;
            }

            if (clipboardBmp.Width > target.Width || clipboardBmp.Height > target.Height)
            {
                return false; // Bitmap is too big to paste into target sprite
            }

            // Check palette compatibility
            HashSet<Color> targetPaletteSet = [.. target.Palette];
            Color[] imagePalette;
            if ((clipboardBmp.PixelFormat & PixelFormat.Indexed) != 0)
            {
                ColorPalette pal = clipboardBmp.Palette;
                imagePalette = pal.Entries;
            }
            else
            {
                // If not indexed, extract unique colours
                HashSet<Color> colourSet = [];
                for (int y = 0; y < clipboardBmp.Height; y++)
                {
                    for (int x = 0; x < clipboardBmp.Width; x++)
                    {
                        colourSet.Add(clipboardBmp.GetPixel(x, y));

                        // if there's more unique colours in the image than the target palette, the image can't be pasted
                        if (colourSet.Count > targetPaletteSet.Count)
                        {
                            return false;
                        }
                    }
                }

                imagePalette = colourSet.ToArray();
            }

            // Check if all colours in imagePalette exist in targetPalette (order doesn't matter)
            if (!imagePalette.All(colour => targetPaletteSet.Contains(colour)))
            {
                return false;
            }

            bitmap = clipboardBmp;
            palette = imagePalette;
            return true;
        }
    }
}