namespace WmsGfxSpriteEditor.ROMs
{
    /// <summary>
    /// Represents information about a ROM file in the system
    /// </summary>
    public record RomFileInfo
    {
        /// <summary>
        /// Gets the name of the ROM file
        /// </summary>
        public string FileName { get; init; }

        /// <summary>
        /// Gets the offset of the ROM data in the memory stream
        /// </summary>
        public int Offset { get; init; }

        /// <summary>
        /// Gets the size of the ROM data in bytes
        /// </summary>
        public int Size { get; init; }

        /// <summary>
        /// Initializes a new instance of the RomInfo record
        /// </summary>
        /// <param name="fileName">The name of the ROM file</param>
        /// <param name="offset">The offset of the ROM data in the memory stream</param>
        /// <param name="size">The size of the ROM data in bytes</param>
        public RomFileInfo(string fileName, int offset, int size)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Offset = offset;
            Size = size;
        }

        /// <summary>
        /// Returns a string representation of the ROM information
        /// </summary>
        public override string ToString() => $"{FileName} (0x{Offset:X4}, Size: 0x{Size:X4})";
    }
}
