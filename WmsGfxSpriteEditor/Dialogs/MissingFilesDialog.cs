using System.Text;

namespace WmsGfxSpriteEditor.Dialogs
{
    internal class MissingFilesDialog
    {
        private readonly string _romSetName;

        public MissingFilesDialog(string romSetName)
        {
            _romSetName = romSetName;
        }

        public void ShowDialog(string[] missingRomFiles, IWin32Window? owner = null)
        {
            StringBuilder sb = new("MISSING FILES:");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendJoin(Environment.NewLine, missingRomFiles);

            MessageBox.Show(owner, sb.ToString(), $"Could not load {_romSetName} ROM files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
