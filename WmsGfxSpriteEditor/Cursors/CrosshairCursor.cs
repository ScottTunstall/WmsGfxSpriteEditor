namespace WmsGfxSpriteEditor.Cursors
{
    internal class CrosshairCursor
    {
        public static Cursor CreateCrosshair(Color colour, Color outlineColour, int size = 12)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                
                int center = size / 2;
                
                // Draw outline first - creates a contiguous border around the crosshair
                using (Pen outlinePen = new Pen(outlineColour, 1))
                {
                    // Vertical white outline lines (left and right of the center vertical line)
                    g.DrawLine(outlinePen, center - 1, 0, center - 1, size - 1);     // left side
                    g.DrawLine(outlinePen, center + 1, 0, center + 1, size - 1);     // right side
                    
                    // Horizontal white outline lines (above and below the center horizontal line)
                    g.DrawLine(outlinePen, 0, center - 1, size - 1, center - 1);     // top side
                    g.DrawLine(outlinePen, 0, center + 1, size - 1, center + 1);     // bottom side
                }
                
                // Draw the main crosshair (using the provided colour parameter)
                using (Pen pen = new Pen(colour, 1))
                {
                    g.DrawLine(pen, center, 0, center, size - 1);         // vertical line
                    g.DrawLine(pen, 0, center, size - 1, center);         // horizontal line
                }
            }
            return new Cursor(bmp.GetHicon());
        }
    }
}