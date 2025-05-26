using WmsGfxSpriteEditor.Roms.Robotron2084;
using WmsGfxSpriteEditor.Roms.Robotron2084.Shared;
using WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Palettes;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public class SpriteEditorDependenciesFactory
    {
        public static SpriteEditorDependencies Create(RomSetType romSetTypes)
        {
            return romSetTypes switch
            {
                RomSetType.BlueLabel => new SpriteEditorDependencies( new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteGridRenderer()),
                RomSetType.TieDieWDPU => new SpriteEditorDependencies( new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new SpriteFactory(), new DefaultSpriteGridRenderer()),
                _ => throw new NotSupportedException($"Robotron ROM set type {romSetTypes} is not supported.")
            };
        }
    }
}
