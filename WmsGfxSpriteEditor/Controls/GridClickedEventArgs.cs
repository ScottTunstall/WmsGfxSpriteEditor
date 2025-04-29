using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Controls
{
    public class GridCellMouseEventArgs: GridEventArgs
    {
        /// <summary>
        ///  Gets which mouse button was pressed.
        /// </summary>
        public MouseButtons Button { get; }

        /// <summary>
        ///  Gets the number of times the mouse button was pressed and released.
        /// </summary>
        public int Clicks { get; }

        public GridCellMouseEventArgs(MouseButtons button, int clicks, int gridX, int gridY) : base(gridX, gridY)
        {
            Button = button;
            Clicks = clicks;
        }
    }
}
