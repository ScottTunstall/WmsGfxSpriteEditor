namespace WmsGfxSpriteEditor
{
    public interface IPaletteClipboardService
    {
        string CopyAsRGBString(Color color);
        string CopyAsHexString(Color color);
    }
}