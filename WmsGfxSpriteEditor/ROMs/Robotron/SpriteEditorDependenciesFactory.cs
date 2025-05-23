using WmsGfxSpriteEditor.ROMs.Robotron.BlueLabel.Loader;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared;
using WmsGfxSpriteEditor.ROMs.Robotron.Shared.Palettes;
using WmsGfxSpriteEditor.ROMs.Robotron.WDPUTieDie.Loader;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.ROMs.Robotron
{
    public class SpriteEditorDependenciesFactory
    {
        public static SpriteEditorDependencies Create(RomSetType romSetTypes)
        {
            return romSetTypes switch
            {
                RomSetType.BlueLabel => new SpriteEditorDependencies( new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteRenderer()),
                RomSetType.TieDieWDPU => new SpriteEditorDependencies( new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteRenderer()),
                _ => throw new NotSupportedException($"Robotron ROM set type {romSetTypes} is not supported.")
            };
        }
    }
}
