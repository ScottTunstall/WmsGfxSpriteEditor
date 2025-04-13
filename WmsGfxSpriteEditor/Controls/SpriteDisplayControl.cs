using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Event arguments for the GridCellClicked event
    /// </summary>
    public class GridCoordinateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the X coordinate in the sprite grid
        /// </summary>
        public int GridX { get; }

        /// <summary>
        /// Gets the Y coordinate in the sprite grid
        /// </summary>
        public int GridY { get; }

        /// <summary>
        /// Gets the byte index (X coordinate / 2) in the sprite data
        /// </summary>
        public int ByteX => GridX / 2;

        /// <summary>
        /// Gets whether this is the first (0) or second (1) pixel in the byte
        /// </summary>
        public int PixelInByte => GridX % 2;

        /// <summary>
        /// Initializes a new instance of the GridCoordinateEventArgs class
        /// </summary>
        /// <param name="gridX">X coordinate in the sprite grid</param>
        /// <param name="gridY">Y coordinate in the sprite grid</param>
        public GridCoordinateEventArgs(int gridX, int gridY)
        {
            GridX = gridX;
            GridY = gridY;
        }
    }

    /// <summary>
    /// Custom PictureBox control for displaying and interacting with sprites
    /// </summary>
    public class SpriteDisplayControl : PictureBox
    {
        // Required services
        private ISpriteRenderer? _spriteRenderer;

        // Sprite properties
        private MemoryStream? _romData;
        private int _spriteOffset;
        private int _spriteWidthInBytes;
        private int _spriteHeight;
        private bool _spriteIsLinear;
        private Color[] _palette = Array.Empty<Color>();
        private Color _gridColor = Color.FromArgb(80, 80, 80);
        private int _zoomLevel = 1;

        /// <summary>
        /// Event fired when a grid cell is clicked
        /// </summary>
        public event EventHandler<GridCoordinateEventArgs>? GridCellClicked;

        public SpriteDisplayControl()
        {
            // Enable double buffering for smoother rendering
            DoubleBuffered = true;
            BackColor = Color.Black;
        }

        /// <summary>
        /// Sets the sprite renderer to use for rendering
        /// </summary>
        public ISpriteRenderer? SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }

        /// <summary>
        /// Sets the ROM data containing sprite information
        /// </summary>
        public MemoryStream? RomData
        {
            get => _romData;
            set
            {
                _romData = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets the sprite information
        /// </summary>
        public void SetSpriteInfo(SpriteInfo sprite)
        {
            _spriteOffset = sprite.Offset;
            _spriteWidthInBytes = sprite.WidthInBytes;
            _spriteHeight = sprite.Height;
            _spriteIsLinear = sprite.IsLinear;
            UpdateSizeForZoom();
            Invalidate();
        }

        /// <summary>
        /// Sets the color palette to use for rendering
        /// </summary>
        public Color[] Palette
        {
            get => _palette;
            set
            {
                _palette = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the grid color
        /// </summary>
        public Color GridColor
        {
            get => _gridColor;
            set
            {
                _gridColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the zoom level
        /// </summary>
        public int ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                if (value < 1)
                    value = 1;

                _zoomLevel = value;
                UpdateSizeForZoom();
                Invalidate();
            }
        }

        /// <summary>
        /// Updates the control size based on sprite dimensions and zoom level
        /// </summary>
        private void UpdateSizeForZoom()
        {
            if (_spriteWidthInBytes > 0 && _spriteHeight > 0)
            {
                int spriteWidth = _spriteWidthInBytes * 2 * _zoomLevel; // 2 pixels per byte
                int spriteHeight = _spriteHeight * _zoomLevel;
                Size = new Size(spriteWidth, spriteHeight);
            }
        }

        /// <summary>
        /// Handles the Paint event
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_romData == null || _romData.Length == 0 || _spriteRenderer == null)
            {
                // Simply fill with a black rectangle if no ROM data is available
                e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
                return;
            }

            // Use the sprite renderer to render directly to the Graphics object
            _spriteRenderer.RenderSprite(
                e.Graphics,
                _romData,
                _spriteOffset,
                _spriteWidthInBytes,
                _spriteHeight,
                _spriteIsLinear,
                _palette,
                _gridColor,
                _zoomLevel,
                new Rectangle(Point.Empty, Size));
        }

        /// <summary>
        /// Handles the MouseClick event
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (_zoomLevel <= 0)
                return;

            // Calculate grid coordinates based on mouse position and zoom level
            int gridX = e.X / _zoomLevel;
            int gridY = e.Y / _zoomLevel;

            // Ensure the coordinates are within sprite bounds
            if (gridX >= 0 && gridX < _spriteWidthInBytes * 2 && // 2 pixels per byte
                gridY >= 0 && gridY < _spriteHeight)
            {
                // Raise the event with the grid coordinates
                OnGridCellClicked(new GridCoordinateEventArgs(gridX, gridY));
            }
        }

        /// <summary>
        /// Raises the GridCellClicked event
        /// </summary>
        protected virtual void OnGridCellClicked(GridCoordinateEventArgs e)
        {
            GridCellClicked?.Invoke(this, e);
        }
    }
}