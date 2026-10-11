namespace WmsGfxSpriteEditor.Controls
{
    public class MagnificationPanel : Panel
    {
        public event EventHandler<MouseEventArgs>? ZoomMouseWheel;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Raise custom event for zoom
            ZoomMouseWheel?.Invoke(this, e);

            // Do not call base.OnMouseWheel, so scrollbars do not move
        }
    }
}