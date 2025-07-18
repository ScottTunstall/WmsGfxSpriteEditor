using System.ComponentModel;
using System.Windows.Forms.Design;

namespace WmsGfxSpriteEditor.Controls
{
    /// <summary>
    /// A ToolStripItem that hosts a TrackBar control with configurable properties visible in the designer.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripTrackBar : ToolStripControlHost
    {
        /// <summary>
        /// Parameterless constructor for the designer.
        /// </summary>
        public ToolStripTrackBar() : base(CreateTrackBar())
        {
        }

        /// <summary>
        /// Occurs when the Value property of the track bar changes.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the Value property of the track bar changes.")]
        public event EventHandler? ValueChanged
        {
            add => TrackBar.ValueChanged += value;
            remove => TrackBar.ValueChanged -= value;
        }

        /// <summary>
        /// Occurs when the user moves the track bar slider with the mouse or arrow keys.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the user moves the track bar slider with the mouse or arrow keys.")]
        public event EventHandler? Scroll
        {
            add => TrackBar.Scroll += value;
            remove => TrackBar.Scroll -= value;
        }

        /// <summary>
        /// Gets the embedded TrackBar control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Behavior")]
        [Description("The embedded TrackBar control.")]
        public TrackBar TrackBar => Control as TrackBar ?? throw new InvalidOperationException("Control is not a TrackBar");

        /// <summary>
        /// Gets or sets the minimum value of the track bar.
        /// </summary>
        [Category("Behavior")]
        [Description("The minimum value of the track bar.")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int Minimum
        {
            get => TrackBar.Minimum;
            set => TrackBar.Minimum = value;
        }

        /// <summary>
        /// Gets or sets the maximum value of the track bar.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum value of the track bar.")]
        [DefaultValue(32)]
        [Browsable(true)]
        public int Maximum
        {
            get => TrackBar.Maximum;
            set => TrackBar.Maximum = value;
        }

        /// <summary>
        /// Gets or sets the current value of the track bar.
        /// </summary>
        [Category("Behavior")]
        [Description("The current value of the track bar.")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int Value
        {
            get => TrackBar.Value;
            set => TrackBar.Value = value;
        }

        /// <summary>
        /// Gets or sets the tick style of the track bar.
        /// </summary>
        [Category("Appearance")]
        [Description("The tick style of the track bar.")]
        [DefaultValue(TickStyle.None)]
        [Browsable(true)]
        public TickStyle TickStyle
        {
            get => TrackBar.TickStyle;
            set => TrackBar.TickStyle = value;
        }

        /// <summary>
        /// Gets or sets the tick frequency of the track bar.
        /// </summary>
        [Category("Behavior")]
        [Description("The tick frequency of the track bar.")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int TickFrequency
        {
            get => TrackBar.TickFrequency;
            set => TrackBar.TickFrequency = value;
        }

        /// <summary>
        /// Create and configure the actual TrackBar instance.
        /// </summary>
        private static Control CreateTrackBar()
        {
            TrackBar tb = new()
            {
                AutoSize = false,
                TickStyle = TickStyle.None,
                Size = new Size(100, 18)
            };
            return tb;
        }
    }
}