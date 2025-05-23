namespace WmsGfxSpriteEditor
{
    public record SpriteEditorDependencies(IPaletteService PaletteService, ISpriteRepository SpriteRepository, ISpriteFactory SpriteFactory, ISpriteGridRenderer SpriteRenderer)
    {
    }
}
