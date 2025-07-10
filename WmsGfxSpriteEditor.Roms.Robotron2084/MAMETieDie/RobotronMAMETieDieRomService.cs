namespace WmsGfxSpriteEditor.Roms.Robotron2084.MAMETieDie
{
    /// <summary>
    /// Robotron Tie Die ROMS for MAME as of 10-July-2025
    /// </summary>
    public class RobotronMAMETieDieRomService: RomServiceBase
    {
        public override string RomSetName => RobotronRomSetNames.TieDieMAME;

        protected override RomFileInfo[] RequiredRoms =>
        [
            // offsets taken from src\mame\midway\williams.cpp in MAME source code
            new RomFileInfo("2084_rom_1b_3005-13.e4", 0x0000, 0x1000),
            new RomFileInfo("2084_rom_2b_3005-14.c4", 0x1000, 0x1000),
            new RomFileInfo("2084_rom_3b_3005-15.a4", 0x2000, 0x1000),
            new RomFileInfo("tiedie_rom_4b.e5", 0x3000, 0x1000),
            new RomFileInfo("fixrobo_rom_5b.c5", 0x4000, 0x1000),
            new RomFileInfo("2084_rom_6b_3005-18.a5", 0x5000, 0x1000),
            new RomFileInfo("tiedie_rom_7b.e6", 0x6000, 0x1000),
            new RomFileInfo("tiedie_rom_8b.c6", 0x7000, 0x1000),
            new RomFileInfo("2084_rom_9b_3005-21.a6", 0x8000, 0x1000),
            new RomFileInfo("tiedie_rom_10b.a7", 0xD000, 0x1000),
            new RomFileInfo("tiedie_rom_11b.c7", 0xE000, 0x1000),
            new RomFileInfo("2084_rom_12b_3005-24.e7", 0xF000, 0x1000)
        ];
    }
}
