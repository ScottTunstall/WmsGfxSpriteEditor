namespace WmsGfxSpriteEditor.Palette
{
    public class DefaultPaletteClipboardService : IDefaultPaletteClipboardService
    {
        public string CopyAsRGBString(Color color)
        {
            string asRGB =  $"{color.R},{color.G},{color.B}";
            Clipboard.SetText(asRGB);
            return asRGB;
        }

        public string CopyAsHexString(Color color)
        {
            string asHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            Clipboard.SetText(asHex);
            return asHex;
        }
    }
}
