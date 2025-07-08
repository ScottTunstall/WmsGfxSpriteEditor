namespace WmsGfxSpriteEditor.Dialogs
{
    internal class LoadRomDialog
    {
        public virtual string? BrowseForFolder(string romSetName)
        {
            if (string.IsNullOrWhiteSpace(romSetName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(romSetName));
            }

            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder containing the {romSetName} ROM files";
            folderDialog.UseDescriptionForTitle = true;

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return folderDialog.SelectedPath;
        }
    }
}
