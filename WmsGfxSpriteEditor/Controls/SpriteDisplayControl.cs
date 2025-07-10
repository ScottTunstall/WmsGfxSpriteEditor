using System.ComponentModel;
using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// Custom PictureBox control for displaying and interacting with sprites
    /// </summary>
    public class SpriteDisplayControl : PictureBox
    {
        private const int CellSize = 8;

        // Required services
        private ISpriteGridRenderer _spriteRenderer = new DefaultSpriteGridRenderer();
        private ISprite? _sprite;

        private Color _gridColor = Color.FromArgb(80, 80, 80);
        private int _zoomLevel = 1;
        private int _zoomLevelGridThreshold = 3;

        /// <summary>
        /// Event fired when the mouse moves over a grid cell
        /// </summary>
        public event EventHandler<SpriteGridMouseEventArgs>? GridCellMouseMove;

        /// <summary>
        /// Event fired when the mouse button is held down over a grid cell
        /// </summary>
        public event EventHandler<SpriteGridMouseEventArgs>? GridCellMouseDown;

        /// <summary>
        /// Event fired when the mouse button is released over a grid cell
        /// </summary>
        public event EventHandler<SpriteGridMouseEventArgs>? GridCellMouseUp;

        /// <summary>
        /// Event fired when a grid cell is clicked
        /// </summary>
        public event EventHandler<SpriteGridMouseEventArgs>? GridCellClicked;

        public SpriteDisplayControl()
        {
            // Enable double buffering for smoother rendering
            DoubleBuffered = true;
        }


        [Browsable(false)]
        public ISpriteGridRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }

        /// <summary>
        /// Sprite to render
        /// </summary>
        [Browsable(false)]
        public ISprite? Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                UpdateSizeForZoom();
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
        /// Get or set the "Show grid when zoom level meets or exceeds the supplied value" threshold
        /// </summary>
        public int ZoomLevelThreshold
        {
            get => _zoomLevelGridThreshold;
            set => _zoomLevelGridThreshold = Math.Max(value, 0);
        }

        /// <summary>
        /// Updates the control size based on sprite dimensions and zoom level
        /// </summary>
        private void UpdateSizeForZoom()
        {
            if (_sprite == null)
            {
                return;
            }

            if (_sprite.Width > 0 && _sprite.Height > 0)
            {
                this.Size = _spriteRenderer!.CalculateMinimumClientSize(_sprite.Width, _sprite.Height, _zoomLevel * CellSize);
            }
        }

        /// <summary>
        /// Handles the Paint event
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if ( _sprite == null || _sprite.PixelData.Length == 0)
            {
                return;
            }

            if (_zoomLevel < _zoomLevelGridThreshold)
            {
                _spriteRenderer.RenderSpriteWithoutGrid(
                    pe.Graphics,
                    _sprite,
                    _zoomLevel * CellSize,
                    new(Point.Empty, Size)
                    );
            }
            else
            {
                _spriteRenderer.RenderSpriteWithGrid(
                    pe.Graphics,
                    _sprite,
                    _zoomLevel * CellSize,
                    _gridColor,
                    new(Point.Empty, Size));
            }
        }

        /// <summary>
        /// Handles the MouseMove event
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            if (GridCellMouseMove == null || _zoomLevel <= 0 || _sprite == null)
                return;

            GridCell pt = _spriteRenderer.GridCellFromClient(e.X, e.Y, CellSize * _zoomLevel, Size);

            // Ensure the coordinates are within sprite bounds
            if (_sprite.IsInBounds(pt.X, pt.Y))
            {
                GridCellMouseMove.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (GridCellMouseDown == null || _zoomLevel <= 0 || _sprite == null)
                return;

            GridCell cell = _spriteRenderer.GridCellFromClient(e.X, e.Y, CellSize * _zoomLevel, Size);

            // Ensure the coordinates are within sprite bounds
            if (_sprite.IsInBounds(cell.X, cell.Y))
            {
                GridCellMouseDown.Invoke(this, new(e.Button, e.Clicks, cell.X, cell.Y));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (GridCellMouseUp == null || _zoomLevel <= 0 || _sprite == null)
                return;

            GridCell pt = _spriteRenderer.GridCellFromClient(e.X, e.Y, CellSize * _zoomLevel, Size);

            // Ensure the coordinates are within sprite bounds
            if (_sprite.IsInBounds(pt.X, pt.Y))
            {
                GridCellMouseUp.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }

        /// <summary>
        /// Handles the MouseClick event
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (GridCellClicked == null || _zoomLevel <= 0 || _sprite == null)
                return;

            GridCell pt = _spriteRenderer!.GridCellFromClient(e.X, e.Y, CellSize * _zoomLevel, Size);

            // Ensure the coordinates are within sprite bounds
            if (_sprite.IsInBounds(pt.X, pt.Y))
            {
                GridCellClicked.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }
    }
}