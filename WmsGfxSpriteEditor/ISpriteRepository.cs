using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Defines a repository for accessing sprite information
    /// </summary>
    public interface ISpriteRepository
    {
        /// <summary>
        /// Gets all available sprites
        /// </summary>
        /// <returns>A collection of sprite information</returns>
        IReadOnlyCollection<SpriteInfo> GetAllSprites();

        /// <summary>
        /// Gets a sprite by its index
        /// </summary>
        /// <param name="index">The zero-based index of the sprite</param>
        /// <returns>The sprite information or null if the index is out of range</returns>
        SpriteInfo? GetSpriteByIndex(int index);

        /// <summary>
        /// Gets the number of sprites in the repository
        /// </summary>
        int Count { get; }
    }
}
