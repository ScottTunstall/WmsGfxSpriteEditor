using WmsGfxSpriteEditor.Roms;
using WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader;
using WmsGfxSpriteEditor.ROMs.Robotron.WDPUTieDie.Loader;

namespace WmsGfxSpriteEditor.ROMs.Robotron
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
