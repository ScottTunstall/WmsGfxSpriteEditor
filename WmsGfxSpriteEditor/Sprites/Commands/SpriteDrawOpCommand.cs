namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class SpriteDrawOpCommand
    {
        public void Execute(ISprite sprite, int startX, int startY, int paletteIndex)
        {
            sprite.SetPixelByPaletteIndex(startX, startY, paletteIndex);
        }
    }
}
