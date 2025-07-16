namespace WmsGfxSpriteEditor.Dialogs
{
    internal class SaveRomDialog
    {
        public string? BrowseForFolder(string romSetName)
        {
            if (string.IsNullOrWhiteSpace(romSetName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(romSetName));
            }

            using FolderBrowserDialog folderDialog = new();
            folderDialog.UseDescriptionForTitle = true;
            folderDialog.ShowNewFolderButton = true;
            folderDialog.Description = $"Select the folder to write the {romSetName} ROM files.";

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return folderDialog.SelectedPath;
        }
    }
}