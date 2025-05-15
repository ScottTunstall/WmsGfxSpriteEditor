using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Controls
{
    public class SpriteGridEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the X coordinate in the sprite grid
        /// </summary>
        public int GridX { get; }

        /// <summary>
        /// Gets the Y coordinate in the sprite grid
        /// </summary>
        public int GridY { get; }

        /// <summary>
        /// Initializes a new instance of the SpriteGridEventArgs class
        /// </summary>
        /// <param name="gridX">X coordinate in the sprite grid</param>
        /// <param name="gridY">Y coordinate in the sprite grid</param>
        public SpriteGridEventArgs(int gridX, int gridY)
        {
            GridX = gridX;
            GridY = gridY;
        }
    }
}
