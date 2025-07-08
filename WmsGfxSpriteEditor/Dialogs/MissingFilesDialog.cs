using System.Text;

namespace WmsGfxSpriteEditor.Dialogs
{
    internal class MissingFilesDialog
    {
        public void ShowDialog(string romSetName, string[] missingRomFiles, IWin32Window? owner = null)
        {
            if (string.IsNullOrWhiteSpace(romSetName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(romSetName));
            }

            if (missingRomFiles.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(missingRomFiles));
            }
            
            StringBuilder sb = new("MISSING FILES:");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendJoin(Environment.NewLine, missingRomFiles);

            MessageBox.Show(owner, sb.ToString(), $"Could not load {romSetName} ROM files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
