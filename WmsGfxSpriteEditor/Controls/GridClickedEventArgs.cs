using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Controls
{
    public class GridCellClickedEventArgs: GridEventArgs
    {
        public GridCellClickedEventArgs(int gridX, int gridY) : base(gridX, gridY)
        {
        }
    }
}
