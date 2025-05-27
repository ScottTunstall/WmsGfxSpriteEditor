using WmsGfxSpriteEditor.Roms.Robotron2084.BlueLabel;
using WmsGfxSpriteEditor.Roms.Robotron2084.WDPUTieDie;

namespace WmsGfxSpriteEditor.Roms.Robotron2084
{
    public class RomServiceFactory
    {
        public static IRomService Create(RomSetType romSetType)
        {
            return romSetType switch
            {
                RomSetType.BlueLabel => new RobotronBlueLabelRomService(),
                RomSetType.TieDieWDPU => new RobotronWDPUTieDieRomService(),
                _ => throw new ArgumentException($"Unsupported {nameof(romSetType)}.", nameof(romSetType))
            };
        }
    }
}
