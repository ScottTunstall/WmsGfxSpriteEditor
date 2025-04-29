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
        private ISpriteRenderer? _spriteRenderer;

        private ISprite? _sprite;

        private Color _gridColor = Color.FromArgb(80, 80, 80);
        private int _zoomLevel = 1;
        private int _zoomLevelGridThreshold = 3;

        /// <summary>
        /// Event fired when the mouse moves over a grid cell
        /// </summary>
        public event EventHandler<GridEventArgs>? GridCellMouseMove;

        /// <summary>
        /// Event fired when the mouse button is held down over a grid cell
        /// </summary>
        public event EventHandler<GridCellMouseEventArgs>? GridCellMouseDown;

        public event EventHandler<GridCellMouseEventArgs>? GridCellMouseUp;

        /// <summary>
        /// Event fired when a grid cell is clicked
        /// </summary>
        public event EventHandler<GridCellMouseEventArgs>? GridCellClicked;

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
        /// Sprite to render
        /// </summary>
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
            if (_sprite == null || _spriteRenderer == null)
            {
                return;
            }

            if (_sprite.Width > 0 && _sprite.Height > 0)
            {
                this.Size = _spriteRenderer!.GetSize(_sprite.Width, _sprite.Height, _zoomLevel * CellSize);
            }
        }

        /// <summary>
        /// Handles the Paint event
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (_sprite == null || _spriteRenderer == null || _sprite.Data.Length == 0)
            {
                // Cannot render sprite, so just draw black
                pe.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
                return;
            }

            if (_zoomLevel < _zoomLevelGridThreshold)
            {
                _spriteRenderer.RenderSprite(
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

            if (GridCellMouseMove == null || _zoomLevel <= 0 || _spriteRenderer == null || _sprite == null)
                return;

            Point pt = _spriteRenderer.GetGridCellFromXY(e.X, e.Y, CellSize * _zoomLevel);

            // Ensure the coordinates are within sprite bounds
            if (pt.X > 0 && pt.X <= _sprite.Width ||
                pt.Y > 0 && pt.Y <= _sprite.Height)
            {
                // Raise the event with the grid coordinates - note: coords are one-based
                GridCellMouseMove.Invoke(this, new(pt.X, pt.Y));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (GridCellMouseDown == null || _zoomLevel <= 0 || _spriteRenderer == null || _sprite == null)
                return;

            Point pt = _spriteRenderer.GetGridCellFromXY(e.X, e.Y, CellSize * _zoomLevel);

            // Ensure the coordinates are within sprite bounds
            if (pt.X > 0 && pt.X <= _sprite.Width ||
                pt.Y > 0 && pt.Y <= _sprite.Height)
            {
                // Raise the event with the grid coordinates - note: coords are one-based
                GridCellMouseDown.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (GridCellMouseUp == null || _zoomLevel <= 0 || _spriteRenderer == null || _sprite == null)
                return;

            Point pt = _spriteRenderer.GetGridCellFromXY(e.X, e.Y, CellSize * _zoomLevel);

            // Ensure the coordinates are within sprite bounds
            if (pt.X > 0 && pt.X <= _sprite.Width ||
                pt.Y > 0 && pt.Y <= _sprite.Height)
            {
                // Raise the event with the grid coordinates - note: coords are one-based
                GridCellMouseUp.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }

        /// <summary>
        /// Handles the MouseClick event
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (GridCellClicked == null || _zoomLevel <= 0 || _spriteRenderer == null || _sprite == null)
                return;

            Point pt = _spriteRenderer!.GetGridCellFromXY(e.X, e.Y, CellSize * _zoomLevel);

            // Ensure the coordinates are within sprite bounds
            if (pt.X >= 0 && pt.X < _sprite.Width && // 2 pixels per byte
                pt.Y >= 0 && pt.Y < _sprite.Height)
            {
                GridCellClicked.Invoke(this, new(e.Button, e.Clicks, pt.X, pt.Y));
            }
        }
    }
}