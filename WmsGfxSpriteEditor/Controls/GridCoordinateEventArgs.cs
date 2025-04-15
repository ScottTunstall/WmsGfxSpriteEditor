using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Event arguments for the GridCellClicked event
    /// </summary>
    public class GridCoordinateEventArgs : EventArgs
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
        /// Gets the byte index (X coordinate / 2) in the sprite data
        /// </summary>
        public int ByteX => GridX / 2;

        /// <summary>
        /// Gets whether this is the first (0) or second (1) pixel in the byte
        /// </summary>
        public int PixelInByte => GridX % 2;

        /// <summary>
        /// Initializes a new instance of the GridCoordinateEventArgs class
        /// </summary>
        /// <param name="gridX">X coordinate in the sprite grid</param>
        /// <param name="gridY">Y coordinate in the sprite grid</param>
        public GridCoordinateEventArgs(int gridX, int gridY)
        {
            GridX = gridX;
            GridY = gridY;
        }
    }
}
