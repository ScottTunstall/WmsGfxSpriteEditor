using WmsGfxSpriteEditor.Roms.Robotron2084.BlueLabel;
using WmsGfxSpriteEditor.Roms.Robotron2084.MAMETieDie;
using WmsGfxSpriteEditor.Roms.Robotron2084.WDPUTieDie;

namespace WmsGfxSpriteEditor.Roms.Robotron2084
{
    public class RobotronRomServiceFactory
    {
        public static IRomService Create(RobotronRomSetType romSetType)
        {
            return romSetType switch
            {
                RobotronRomSetType.BlueLabel => new RobotronBlueLabelRomService(),
                RobotronRomSetType.TieDieWDPU => new RobotronWDPUTieDieRomService(),
                RobotronRomSetType.TieDieMAME => new RobotronMAMETieDieRomService(),
                _ => throw new ArgumentException($"Unsupported {nameof(romSetType)}.", nameof(romSetType))
            };
        }
    }
}
