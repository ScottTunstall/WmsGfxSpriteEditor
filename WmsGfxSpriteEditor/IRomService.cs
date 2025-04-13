using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WmsGfxSpriteEditor
{
    public interface IRomService
    {
        /// <summary>
        /// Loads ROM files from the specified folder
        /// </summary>
        /// <param name="folderPath">Path to the folder containing ROM files</param>
        /// <returns>MemoryStream containing the loaded ROM data</returns>
        /// <exception cref="FileNotFoundException">Thrown when a required ROM file is missing</exception>
        MemoryStream LoadRomFiles(string folderPath);
    }
}