namespace WmsGfxSpriteEditor.Roms
{
    public record RomFileAuditInfo
    {
        public string[] PresentRomFiles { get; init; } = [];
        public string[] MissingRomFiles { get; init; } = [];
    }
}
