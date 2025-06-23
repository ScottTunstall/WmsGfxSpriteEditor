using System.Drawing;

namespace WmsGfxSpriteEditor.Sprites.Commands;

public class SetSpritePixelsFromBitmapCommand
{
    private readonly UndoableSpriteHelper _undoHelper;

    public SetSpritePixelsFromBitmapCommand(IHistory history)
    {
        _undoHelper = new UndoableSpriteHelper(history);
    }

    public void Execute(Bitmap source, ISprite target)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        _undoHelper.ExecuteActionWithUndoRedo(target, () =>
        {
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixelColor = source.GetPixel(x, y);
                    int paletteIndex = Array.IndexOf(target.Palette, pixelColor);
                    target.SetPixelByPaletteIndex(x,y, paletteIndex);
                }
            }
        } );
    }
}
    

