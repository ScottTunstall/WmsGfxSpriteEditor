using WmsGfxSpriteEditor.Palettes;
using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public record SpriteEditorDependencies(IRomService RomService, IPaletteService PaletteService, ISpriteRepository SpriteRepository, ISpriteFactory SpriteFactory, ISpriteGridRenderer SpriteRenderer)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    {
    }
}
