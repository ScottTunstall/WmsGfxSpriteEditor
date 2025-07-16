namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Provides clipboard operations for palette colors, such as copying as RGB or hex string.
    /// </summary>
    public interface IPaletteClipboardService
    {
        /// <summary>
        /// Copies the specified color as an RGB string (e.g., "R,G,B") to the clipboard.
        /// </summary>
        /// <param name="color">The color to copy.</param>
        /// <returns>The RGB string representation of the color.</returns>
        string CopyAsRGBString(Color color);

        /// <summary>
        /// Copies the specified color as a hexadecimal string (e.g., "#RRGGBB") to the clipboard.
        /// </summary>
        /// <param name="color">The color to copy.</param>
        /// <returns>The hexadecimal string representation of the color.</returns>
        string CopyAsHexString(Color color);
    }
}