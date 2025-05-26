using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.ROMs
{
    public record RomFileAuditInfo
    {
        public string[] PresentRomFiles { get; init; } = [];
        public string[] MissingRomFiles { get; init; } = [];
    }
}
