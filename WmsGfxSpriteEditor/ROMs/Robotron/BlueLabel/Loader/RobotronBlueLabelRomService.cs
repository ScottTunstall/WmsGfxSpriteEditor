using WmsGfxSpriteEditor.Roms;

namespace WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader
{
    public class RobotronBlueLabelRomService: RomServiceBase 
    {
        /// <summary>
        /// Array of ROM file information in order
        /// </summary>
        protected override RomFileInfo[] RequiredRoms => new[]
        {
            // offsets taken from src\mame\midway\williams.cpp in MAME source code
            new RomFileInfo("2084_rom_1b_3005-13.e4", 0x0000, 0x1000),
            new RomFileInfo("2084_rom_2b_3005-14.c4", 0x1000, 0x1000),
            new RomFileInfo("2084_rom_3b_3005-15.a4", 0x2000, 0x1000),
            new RomFileInfo("2084_rom_4b_3005-16.e5", 0x3000, 0x1000),
            new RomFileInfo("2084_rom_5b_3005-17.c5", 0x4000, 0x1000),
            new RomFileInfo("2084_rom_6b_3005-18.a5", 0x5000, 0x1000),
            new RomFileInfo("2084_rom_7b_3005-19.e6", 0x6000, 0x1000),
            new RomFileInfo("2084_rom_8b_3005-20.c6", 0x7000, 0x1000),
            new RomFileInfo("2084_rom_9b_3005-21.a6", 0x8000, 0x1000),
            new RomFileInfo("2084_rom_10b_3005-22.a7", 0xD000, 0x1000),
            new RomFileInfo("2084_rom_11b_3005-23.c7", 0xE000, 0x1000),
            new RomFileInfo("2084_rom_12b_3005-24.e7", 0xF000, 0x1000)
        };
    }
}
