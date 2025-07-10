namespace WmsGfxSpriteEditor.Roms.Robotron2084.WDPUTieDie
{
    /// <summary>
    /// Robotron Tie Die ROMS from Williams Defender Players Unite (WDPU)
    /// </summary>
    public class RobotronWDPUTieDieRomService: RomServiceBase
    {
        public override string RomSetName => RobotronRomSetNames.TieDieWDPU;

        protected override RomFileInfo[] RequiredRoms => new RomFileInfo[]
        {
            new("robotron.sb1", 0x0000, 0x1000),
            new("robotron.sb2", 0x1000, 0x1000),
            new("robotron.sb3", 0x2000, 0x1000),
            new("robotron.sb4", 0x3000, 0x1000),
            new("robotron.sb5", 0x4000, 0x1000),
            new("robotron.sb6", 0x5000, 0x1000),
            new("robotron.sb7", 0x6000, 0x1000),
            new("robotron.sb8", 0x7000, 0x1000),
            new("robotron.sb9", 0x8000, 0x1000),
            new("robotron.sba", 0xD000, 0x1000),
            new("robotron.sbb", 0xE000, 0x1000),
            new("robotron.sbc", 0xF000, 0x1000)
        };
    }
}
