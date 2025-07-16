using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor
{
    public class SpriteGridMouseEventArgs : SpriteGridEventArgs
    {
        public SpriteGridMouseEventArgs(MouseButtons button, int clicks, int gridX, int gridY) : base(gridX, gridY)
        {
            Button = button;
            Clicks = clicks;
        }

        /// <summary>
        ///  Gets the number of times the mouse button was pressed and released.
        /// </summary>
        public int Clicks { get; }

        /// <summary>
        ///  Gets which mouse button was pressed.
        /// </summary>
        public MouseButtons Button { get; }
    }
}