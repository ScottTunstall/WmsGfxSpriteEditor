using System.Drawing;
using System.Runtime.Versioning;
using WmsGfxSpriteEditor.History;

namespace WmsGfxSpriteEditor.Sprites.Commands;

public class SetSpritePixelsFromBitmapCommand
{
    private readonly UndoableSpriteHelper _undoHelper;

    public SetSpritePixelsFromBitmapCommand(IHistory history)
    {
        _undoHelper = new UndoableSpriteHelper(history);
    }

    [SupportedOSPlatform("windows")]
    public void Execute(Bitmap source, ISprite target)
    {
        ArgumentNullException.ThrowIfNull(source);

        _undoHelper.ExecuteActionWithUndoRedo(target, () =>
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixelColor = source.GetPixel(x, y);

                    // Careful - if the palette has duplicates this may select the wrong colour index!
                    int paletteIndex = Array.IndexOf(target.Palette, pixelColor);
                    target.SetPixelByPaletteIndex(x, y, paletteIndex);
                }
            }
        });
    }
}