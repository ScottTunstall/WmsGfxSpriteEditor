namespace WmsGfxSpriteEditor.Dialogs
{
    internal class LoadRomDialog
    {
        private readonly string _romSetName;

        public LoadRomDialog(string romSetName)
        {
            _romSetName = romSetName;
        }

        public virtual string? BrowseForFolder()
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder containing the {_romSetName} ROM files";
            folderDialog.UseDescriptionForTitle = true;

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return folderDialog.SelectedPath;
        }
    }
}
