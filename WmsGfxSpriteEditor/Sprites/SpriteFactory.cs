namespace WmsGfxSpriteEditor.Sprites
{
    public class SpriteFactory: ISpriteFactory
    {
        public ISprite CreateSpriteFromSpriteInfo(RomData romData, SpriteInfo spriteInfo)
        {
            switch (spriteInfo.BitsPerPixel)
            {
                case 4:
                    return CreateSprite4Bpp(romData, spriteInfo);

                default:
                    throw new NotSupportedException($"Sprite with {spriteInfo.BitsPerPixel} bits per pixel is not supported.");
            }
        }


        private ISprite CreateSprite4Bpp(RomData romData, SpriteInfo spriteInfo)
        {
            int bytesToRead = spriteInfo.WidthInBytes * spriteInfo.Height;
            Memory<byte> spriteData = romData!.ReadBytes(spriteInfo.Offset, bytesToRead);

            return new Sprite4Bpp(spriteData, spriteInfo.WidthInBytes, spriteInfo.Height, spriteInfo.IsLinear);

        }
    }
}
