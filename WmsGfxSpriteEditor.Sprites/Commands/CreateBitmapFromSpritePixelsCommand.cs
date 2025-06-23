using System.Drawing;

namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class CreateBitmapFromSpritePixelsCommand
    {
        public Bitmap Execute(ISprite source)
        {
            return source.CreateBitmapFromSprite();
        }
    }
}
