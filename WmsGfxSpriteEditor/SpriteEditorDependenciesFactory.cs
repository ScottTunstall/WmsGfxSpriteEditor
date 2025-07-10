using WmsGfxSpriteEditor.Roms.Robotron2084;
using WmsGfxSpriteEditor.Roms.Robotron2084.BlueLabel;
using WmsGfxSpriteEditor.Roms.Robotron2084.MAMETieDie;
using WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Palettes;
using WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Sprites;
using WmsGfxSpriteEditor.Roms.Robotron2084.WDPUTieDie;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    public class SpriteEditorDependenciesFactory
    {
        public static SpriteEditorDependencies CreateForRobotron(RobotronRomSetType romSetTypes)
        {
            return romSetTypes switch
            {
                RobotronRomSetType.BlueLabel => new SpriteEditorDependencies(new RobotronBlueLabelRomService(), new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new RobotronSpriteFactory(), new DefaultSpriteGridRenderer()),
                RobotronRomSetType.TieDieWDPU => new SpriteEditorDependencies( new RobotronWDPUTieDieRomService(), new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new RobotronSpriteFactory(), new DefaultSpriteGridRenderer()),
                RobotronRomSetType.TieDieMAME => new SpriteEditorDependencies( new RobotronMAMETieDieRomService(), new RobotronPaletteService(), new RobotronBlueLabelSpriteRepository(), new RobotronSpriteFactory(), new DefaultSpriteGridRenderer()),
                _ => throw new NotSupportedException($"Robotron ROM set type {romSetTypes} is not supported.")
            };
        }
    }
}
