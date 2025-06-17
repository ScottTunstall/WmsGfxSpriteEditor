namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class FlipSpriteVerticalCommand : UndoableSpriteCommand
    {
        public FlipSpriteVerticalCommand(IHistory history) : base(history)
        {
        }
        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite,sprite.YFlip);
        }
    }
}