namespace WmsGfxSpriteEditor
{
    public record RomData: IDisposable
    {

        private readonly MemoryStream _romData;
        private bool _disposedValue;

        public RomData(MemoryStream romData)
        {
            _romData = romData;
        }

        public Memory<byte> ReadAsMemory(int offset, int length)
        {
            byte[] buffer = _romData!.GetBuffer();
            Memory<byte> spriteData = new(buffer, offset, length);
            return spriteData;
        }

        public byte[] ReadAsBytes(int offset, int length)
        {
            byte[] buffer = _romData!.GetBuffer();
            byte[] spriteData = new byte[length];
            Array.Copy(buffer, offset, spriteData, 0, length);
            return spriteData;
        }

        public void WriteBytes(int offset, byte[] data)
        {
            byte[] buffer = _romData!.GetBuffer();
            data.CopyTo(buffer.AsSpan(offset));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _romData?.Dispose();
                }

                _disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
