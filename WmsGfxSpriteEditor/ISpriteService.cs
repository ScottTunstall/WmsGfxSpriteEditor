using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public interface ISpriteService
    {
        void BeginSpriteDrawOp(ISprite sprite, int startX, int startY, int paletteIndex);

        void SpriteDrawOp(ISprite sprite, int x, int y, int paletteIndex);

        void EndSpriteDrawOp(ISprite sprite);

        void FlipSpriteHorizontal(ISprite sprite);

        void FlipSpriteVertical(ISprite sprite);

        void ShiftSpritePixelsLeft(ISprite sprite);

        void ShiftSpritePixelsRight(ISprite sprite);

        void ShiftSpritePixelsUp(ISprite sprite);

        void ShiftSpritePixelsDown(ISprite sprite);
    }
}
