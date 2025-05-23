using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    public class SpriteGridEventArgs : EventArgs
    {
        public GridCell GridCell { get; set; }

        /// <summary>
        /// Initializes a new instance of the SpriteGridEventArgs class
        /// </summary>
        /// <param name="gridX">X coordinate in the sprite grid</param>
        /// <param name="gridY">Y coordinate in the sprite grid</param>
        public SpriteGridEventArgs(int gridX, int gridY)
        {
            GridCell = new GridCell(gridX, gridY);
        }
    }
}
