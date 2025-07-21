namespace WmsGfxSpriteEditor.Dialogs
{
    internal class AboutDialog
    {
        public void ShowDialog(IWin32Window? owner = null)
        {
            MessageBox.Show(
                owner,
                "Williams Graphics Sprite Editor." + Environment.NewLine +
                Environment.NewLine +
                "Designed and developed by Scott Tunstall." + Environment.NewLine +
                "Sprite offsets and colour palette documented by Sean Riddle." + Environment.NewLine +
                "All rights reserved.",
                "About Williams Graphics Sprite Editor",
                MessageBoxButtons.OK,
                MessageBoxIcon.None
            );
        }
    }
}
