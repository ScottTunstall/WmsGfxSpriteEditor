using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WmsGfxSpriteEditor.Roms.Robotron2084;

namespace WmsGfxSpriteEditor.Dialogs
{
    internal class InformationDialog
    {
        public void ShowDialog(string text, string caption, IWin32Window? owner = null)
        {
            MessageBox.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
