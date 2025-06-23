namespace WmsGfxSpriteEditor.Sprites.Commands
{
    public class SpritePixelOpCommand
    {
        public void Execute(ISprite sprite, int startX, int startY, int paletteIndex)
        {
            // TODO: if there's other operations that can be done (e.g. flood fill, spray can) call them here

            sprite.SetPixelByPaletteIndex(startX, startY, paletteIndex);
        }
    }
}
