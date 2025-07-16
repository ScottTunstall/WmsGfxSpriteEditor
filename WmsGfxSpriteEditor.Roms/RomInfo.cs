namespace WmsGfxSpriteEditor.Roms
{
    /// <summary>
    /// Represents information about a ROM file in the system
    /// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public record RomFileInfo(string FileName, int Offset, int Size)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
    {
        /// <summary>
        /// Gets the name of the ROM file
        /// </summary>
        public string FileName { get; init; } = !string.IsNullOrEmpty(FileName) ? FileName : throw new ArgumentException("Value cannot be null or empty.", nameof(FileName));

        /// <summary>
        /// Gets the offset of the ROM data
        /// </summary>
        public int Offset { get; init; } = Offset < 0 ? throw new ArgumentOutOfRangeException(nameof(Offset)) : Offset;

        /// <summary>
        /// Gets the size of the ROM data in bytes
        /// </summary>
        public int Size { get; init; } = Size < 0 ? throw new ArgumentOutOfRangeException(nameof(Size)) : Size;

        /// <summary>
        /// Returns a string representation of the ROM information
        /// </summary>
        public override string ToString() => $"{FileName} (0x{Offset:X4}, Size: 0x{Size:X4})";
    }
}