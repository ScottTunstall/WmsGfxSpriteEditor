namespace WmsGfxSpriteEditor.Sprites.Commands
{
    internal class FlipSpriteHorizontalCommand: UndoableSpriteCommand
    {
        public FlipSpriteHorizontalCommand(IHistory history) : base(history)
        {
        }

        public override void Execute(ISprite sprite)
        {
            ExecuteActionWithUndoRedo(sprite, sprite.XFlip);
        }
    }
}
