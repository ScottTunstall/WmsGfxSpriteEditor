using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WmsGfxSpriteEditor.ROMs;

namespace WmsGfxSpriteEditor
{
    public interface IRomService
    {
        public RomFileAuditInfo Audit(string folderPath);

        /// <summary>
        /// Loads ROM files from the specified folder into a <see cref="RomData"/> object.
        /// </summary>
        /// <param name="folderPath">Path to the folder containing ROM files</param>
        /// <returns>MemoryStream containing the loaded ROM data.</returns>
        /// <exception cref="FileNotFoundException">Thrown when a required ROM file is missing</exception>
        RomData LoadRomData(string folderPath);

        /// <summary>
        /// Saves the <see cref="RomData"/> object into ROM files in the specified directory
        /// </summary>
        /// <param name="romData">ROM data</param>
        /// <param name="folderPath">Path to the folder where ROM files should be generated</param>
        void SaveRomData(RomData romData, string folderPath);
    }
}