namespace WmsGfxSpriteEditor.Roms.Commands
{
    public class UpdateRomDataFromPixelDataCommand
    {
        private readonly RomData _romData;

        public UpdateRomDataFromPixelDataCommand(RomData romData)
        {
            _romData = romData ?? throw new ArgumentNullException(nameof(romData));
        }

        public void Execute(int offset, byte[] pixelData)
        {
            _romData!.PokeBytes(offset, pixelData);
        }
    }
}
