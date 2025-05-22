namespace WmsGfxSpriteEditor
{
    public record SpriteEditorDependencies(string RomSetName, IRomService RomService, IPaletteService PaletteService, ISpriteRepository SpriteRepository, ISpriteFactory SpriteFactory, ISpriteRenderer SpriteRenderer)
    {
    }
}
