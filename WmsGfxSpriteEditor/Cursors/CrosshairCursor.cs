namespace WmsGfxSpriteEditor.Cursors
{
    internal class CrosshairCursor
    {
        public static Cursor CreateCrosshair(Color colour, int size = 16)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent); 
                using (Pen pen = new Pen(colour, 1))
                {
                    g.DrawLine(pen, size / 2, 0, size / 2, size); // vertical line
                    g.DrawLine(pen, 0, size / 2, size, size / 2); // horizontal line
                }
            }
            return new Cursor(bmp.GetHicon());
        }
    }
}
