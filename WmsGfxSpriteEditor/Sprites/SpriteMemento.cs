using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public class SpriteMemento
    {
        private readonly byte[] _data;

        public SpriteMemento(byte[] data)
        {
            _data = new byte[data.Length];
            Array.Copy(data, _data, data.Length);
        }

        private byte[] Data => _data;
    }
}
