namespace WmsGfxSpriteEditor.Sprites
{
    /// <summary>
    /// Defines a repository for accessing and retrieving sprite information.
    /// </summary>
    public interface ISpriteRepository
    {
        /// <summary>
        /// Gets the number of sprites in the repository.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets all available sprites in the repository.
        /// </summary>
        /// <returns>A collection of sprite information.</returns>
        IReadOnlyCollection<SpriteInfo> GetAllSprites();

        /// <summary>
        /// Gets a sprite by its index.
        /// </summary>
        /// <param name="index">The zero-based index of the sprite.</param>
        /// <returns>The sprite information or null if the index is out of range.</returns>
        SpriteInfo? GetSpriteInfoByIndex(int index);
    }
}