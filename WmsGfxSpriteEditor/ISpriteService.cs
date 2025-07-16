using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor
{
    /// <summary>
    /// Provides sprite editing and manipulation operations such as drawing, flipping, and shifting pixels.
    /// </summary>
    public interface ISpriteService
    {
        /// <summary>
        /// Begins a sprite drawing operation at the specified start position and palette index.
        /// </summary>
        /// <param name="sprite">The sprite to operate on.</param>
        /// <param name="startX">The X coordinate, within the sprite, to start drawing.</param>
        /// <param name="startY">The Y coordinate, within the sprite, to start drawing.</param>
        /// <param name="paletteIndex">The palette index to use for drawing.</param>
        void BeginSpriteDrawOp(ISprite sprite, int startX, int startY, int paletteIndex);

        /// <summary>
        /// Continues a sprite drawing operation at the specified position and palette index.
        /// </summary>
        /// <param name="sprite">The sprite to operate on.</param>
        /// <param name="x">The X coordinate, within the sprite, to draw at.</param>
        /// <param name="y">The Y coordinate, within the sprite, to draw at.</param>
        /// <param name="paletteIndex">The palette index to use for drawing.</param>
        void SpriteDrawOp(ISprite sprite, int x, int y, int paletteIndex);

        /// <summary>
        /// Ends the current sprite drawing operation.
        /// </summary>
        /// <param name="sprite">The sprite to operate on.</param>
        void EndSpriteDrawOp(ISprite sprite);

        /// <summary>
        /// Flips the sprite horizontally.
        /// </summary>
        /// <param name="sprite">The sprite to flip.</param>
        void FlipSpriteHorizontal(ISprite sprite);

        /// <summary>
        /// Flips the sprite vertically.
        /// </summary>
        /// <param name="sprite">The sprite to flip.</param>
        void FlipSpriteVertical(ISprite sprite);

        /// <summary>
        /// Shifts all sprite pixels left by one column.
        /// </summary>
        /// <param name="sprite">The sprite to shift.</param>
        void ShiftSpritePixelsLeft(ISprite sprite);

        /// <summary>
        /// Shifts all sprite pixels right by one column.
        /// </summary>
        /// <param name="sprite">The sprite to shift.</param>
        void ShiftSpritePixelsRight(ISprite sprite);

        /// <summary>
        /// Shifts all sprite pixels up by one row.
        /// </summary>
        /// <param name="sprite">The sprite to shift.</param>
        void ShiftSpritePixelsUp(ISprite sprite);

        /// <summary>
        /// Shifts all sprite pixels down by one row.
        /// </summary>
        /// <param name="sprite">The sprite to shift.</param>
        void ShiftSpritePixelsDown(ISprite sprite);
    }
}
