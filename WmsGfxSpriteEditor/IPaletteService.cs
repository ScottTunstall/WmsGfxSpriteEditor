namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Interface for palette providers
    /// </summary>
    public interface IPaletteService
    {
        /// <summary>
        /// Gets the color palette
        /// </summary>
        /// <returns>An array of colors</returns>
        Color[] GetPalette();
    }
}
