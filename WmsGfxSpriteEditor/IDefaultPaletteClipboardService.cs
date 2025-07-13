namespace WmsGfxSpriteEditor
{
    public interface IDefaultPaletteClipboardService
    {
        string CopyAsRGBString(Color color);
        string CopyAsHexString(Color color);
    }
}