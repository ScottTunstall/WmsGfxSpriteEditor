using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public interface ISpriteClipboardService
    {
        void Copy(ISprite source);
        void Paste(ISprite target);
        bool HasCompatibleBitmap(ISprite target);
    }
}