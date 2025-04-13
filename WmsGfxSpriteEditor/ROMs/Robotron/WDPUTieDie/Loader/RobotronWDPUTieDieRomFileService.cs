namespace WmsGfxSpriteEditor.ROMs.Robotron.WDPUTieDie.Loader
{
    /// <summary>
    /// Robotron Tie Die ROMS from Williams Defender Players Unite (WDPU)
    /// </summary>
    public class RobotronWDPUTieDieRomFileService: RomFileServiceBase
    {
        protected override RomInfo[] RequiredRoms => new RomInfo[]
        {
            new RomInfo("robotron.sb1", 0x0000, 0x1000),
            new RomInfo("robotron.sb2", 0x1000, 0x1000),
            new RomInfo("robotron.sb3", 0x2000, 0x1000),
            new RomInfo("robotron.sb4", 0x3000, 0x1000),
            new RomInfo("robotron.sb5", 0x4000, 0x1000),
            new RomInfo("robotron.sb6", 0x5000, 0x1000),
            new RomInfo("robotron.sb7", 0x6000, 0x1000),
            new RomInfo("robotron.sb8", 0x7000, 0x1000),
            new RomInfo("robotron.sb9", 0x8000, 0x1000),
            new RomInfo("robotron.sba", 0xD000, 0x1000),
            new RomInfo("robotron.sbb", 0xE000, 0x1000),
            new RomInfo("robotron.sbc", 0xF000, 0x1000)
        };
    }
}
