using WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared.Palettes;
using WmsGfxSpriteEditor.ROMs.Robotron.WDPUTieDie.Loader;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.ROMs.Robotron
{
    public class SpriteEditorDependenciesFactory
    {
        public static SpriteEditorDependencies Create(RobotronRomSetType romSetTypes)
        {
            return romSetTypes switch
            {
                RobotronRomSetType.BlueLabel => new SpriteEditorDependencies(RomSetNames.BlueLabel, new RobotronBlueLabelRomFileService(), new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteRenderer()),
                RobotronRomSetType.TieDieWDPU => new SpriteEditorDependencies(RomSetNames.TieDieWDPU, new RobotronWDPUTieDieRomFileService(), new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteRenderer()),
                _ => throw new NotSupportedException($"Robotron ROM set type {romSetTypes} is not supported.")
            };
        }
    }
}
