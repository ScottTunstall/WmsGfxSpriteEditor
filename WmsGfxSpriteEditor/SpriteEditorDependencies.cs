using WmsGfxSpriteEditor.Palettes;
using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public record SpriteEditorDependencies(IRomService RomService, IPaletteService PaletteService, ISpriteRepository SpriteRepository, ISpriteFactory SpriteFactory, ISpriteGridRenderer SpriteRenderer)
    {
    }
}
