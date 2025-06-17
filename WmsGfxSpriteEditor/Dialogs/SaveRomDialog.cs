using WmsGfxSpriteEditor.Roms.Robotron2084;

namespace WmsGfxSpriteEditor.Dialogs
{
    internal class SaveRomDialog
    {
        private readonly string _romSetName;

        public SaveRomDialog(string romSetName)
        {
            _romSetName = romSetName;
        }

        public string? BrowseForFolder()
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = $"Select the folder to write the {_romSetName} ROM files.";

            if (folderDialog.ShowDialog() != DialogResult.OK)
                return null;

            return folderDialog.SelectedPath;
        }
    }
}
