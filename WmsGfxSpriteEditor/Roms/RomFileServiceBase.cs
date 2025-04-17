using System.IO;

namespace WmsGfxSpriteEditor.ROMs
{
    public abstract class RomFileServiceBase: IRomService
    {
        protected abstract RomInfo[] RequiredRoms { get; }

        public string[] GetMissingRomFiles(string folderPath)
        {
            List<string> missingRomFiles = new();

            foreach (RomInfo? romInfo in RequiredRoms)
            {
                string filePath = Path.Combine(folderPath, romInfo.FileName);

                if (!File.Exists(filePath))
                {
                    missingRomFiles.Add(romInfo.FileName);
                }
            }

            return missingRomFiles.ToArray();
        }



        /// <summary>
        /// Loads ROM files from the specified directory
        /// </summary>
        /// <param name="folderPath">The directory containing the ROM files</param>
        /// <returns>A memory stream containing the combined ROM data</returns>
        /// <exception cref="FileNotFoundException">Thrown when a required ROM file is missing</exception>
        /// <exception cref="InvalidDataException">Thrown when a ROM file has an incorrect size</exception>
        public MemoryStream? LoadRomFiles(string folderPath)
        {
            // Validate directory
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {folderPath}");
            }

            // Calculate required memory stream size based on highest ROM offset + size
            int requiredSize = RequiredRoms.Select(rom => rom.Offset + rom.Size).Max();

            // Create a memory stream to hold all ROM data
            MemoryStream? memoryStream = new(requiredSize);

            // Initialize the memory stream with zeros
            byte[] emptyBuffer = new byte[requiredSize];
            memoryStream.Write(emptyBuffer, 0, emptyBuffer.Length);

            // Load each ROM file and place it at its specified offset
            foreach (RomInfo romInfo in RequiredRoms)
            {
                string filePath = Path.Combine(folderPath, romInfo.FileName);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Required ROM file not found", romInfo.FileName);
                }

                // Read the ROM file
                byte[] romData = File.ReadAllBytes(filePath);

                // Validate ROM size
                if (romData.Length != romInfo.Size)
                {
                    throw new InvalidDataException(
                        $"ROM file {romInfo.FileName} has incorrect size. Expected: {romInfo.Size} bytes, Actual: {romData.Length} bytes");
                }

                // Validate that the ROM offset + size doesn't exceed the memory stream capacity
                if (romInfo.Offset + romInfo.Size > requiredSize)
                {
                    throw new InvalidOperationException(
                        $"ROM {romInfo.FileName} (offset: 0x{romInfo.Offset:X}, size: 0x{romInfo.Size:X}) exceeds allocated memory size (0x{requiredSize:X})");
                }

                // Position the stream at the ROM's specified offset
                memoryStream.Position = romInfo.Offset;

                // Write the ROM data to the memory stream at the specified offset
                memoryStream.Write(romData, 0, romData.Length);
            }

            // Reset the position to the beginning of the stream
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
