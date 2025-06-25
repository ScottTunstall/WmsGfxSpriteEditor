using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuFileLoad = new ToolStripMenuItem();
            mnuFileLoadRobotron = new ToolStripMenuItem();
            mnuFileLoadRobotronBlueLabel = new ToolStripMenuItem();
            mnuFileLoadRobotronTieDieWDPU = new ToolStripMenuItem();
            mnuFileSave = new ToolStripMenuItem();
            mnuEdit = new ToolStripMenuItem();
            mnuEditUndo = new ToolStripMenuItem();
            mnuEditRedo = new ToolStripMenuItem();
            mnuEditSeparator = new ToolStripSeparator();
            mnuEditCopy = new ToolStripMenuItem();
            mnuEditPaste = new ToolStripMenuItem();
            mnuView = new ToolStripMenuItem();
            mnuViewZoomIn = new ToolStripMenuItem();
            mnuViewZoomOut = new ToolStripMenuItem();
            mnuSprite = new ToolStripMenuItem();
            mnuSpriteFlipHorizontal = new ToolStripMenuItem();
            mnuSpriteFlipVertical = new ToolStripMenuItem();
            mnuSpriteSeparator = new ToolStripSeparator();
            mnuSpriteShiftLeft = new ToolStripMenuItem();
            mnuSpriteShiftRight = new ToolStripMenuItem();
            mnuSpriteShiftUp = new ToolStripMenuItem();
            mnuSpriteShiftDown = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            Spacer = new ToolStripStatusLabel();
            CoordinatesLabel = new ToolStripStatusLabel();
            topPanel = new Panel();
            tableLayoutPanel = new TableLayoutPanel();
            lblSprite = new Label();
            cboSprite = new ComboBox();
            lblZoom = new Label();
            nudZoom = new NumericUpDown();
            lblPalette = new Label();
            pnlPalette = new PalettePanel();
            spriteDisplay = new SpriteDisplayControl();
            magnifierPanel = new Panel();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            topPanel.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudZoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spriteDisplay).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuFile, mnuEdit, mnuView, mnuSprite });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(882, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLoad, mnuFileSave });
            mnuFile.Name = "mnuFile";
            mnuFile.ShortcutKeys = Keys.Alt | Keys.F;
            mnuFile.Size = new Size(37, 20);
            mnuFile.Text = "&File";
            // 
            // mnuFileLoad
            // 
            mnuFileLoad.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLoadRobotron });
            mnuFileLoad.Name = "mnuFileLoad";
            mnuFileLoad.ShortcutKeys = Keys.Control | Keys.O;
            mnuFileLoad.Size = new Size(143, 22);
            mnuFileLoad.Text = "&Load";
            // 
            // mnuFileLoadRobotron
            // 
            mnuFileLoadRobotron.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLoadRobotronBlueLabel, mnuFileLoadRobotronTieDieWDPU });
            mnuFileLoadRobotron.Name = "mnuFileLoadRobotron";
            mnuFileLoadRobotron.Size = new Size(154, 22);
            mnuFileLoadRobotron.Text = "Robotron: 2084";
            // 
            // mnuFileLoadRobotronBlueLabel
            // 
            mnuFileLoadRobotronBlueLabel.Name = "mnuFileLoadRobotronBlueLabel";
            mnuFileLoadRobotronBlueLabel.ShortcutKeys = Keys.Control | Keys.B;
            mnuFileLoadRobotronBlueLabel.Size = new Size(200, 22);
            mnuFileLoadRobotronBlueLabel.Text = "Blue Label";
            mnuFileLoadRobotronBlueLabel.Click += mnuFileLoadRobotronBlueLabel_Click;
            // 
            // mnuFileLoadRobotronTieDieWDPU
            // 
            mnuFileLoadRobotronTieDieWDPU.Name = "mnuFileLoadRobotronTieDieWDPU";
            mnuFileLoadRobotronTieDieWDPU.ShortcutKeys = Keys.Control | Keys.W;
            mnuFileLoadRobotronTieDieWDPU.Size = new Size(200, 22);
            mnuFileLoadRobotronTieDieWDPU.Text = "Tie Die (WDPU)";
            mnuFileLoadRobotronTieDieWDPU.Click += mnuFileLoadRobotronTieDieWDPU_Click;
            // 
            // mnuFileSave
            // 
            mnuFileSave.Name = "mnuFileSave";
            mnuFileSave.ShortcutKeys = Keys.Control | Keys.S;
            mnuFileSave.Size = new Size(143, 22);
            mnuFileSave.Text = "&Save";
            mnuFileSave.Click += mnuFileSave_Click;
            // 
            // mnuEdit
            // 
            mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuEditUndo, mnuEditRedo, mnuEditSeparator, mnuEditCopy, mnuEditPaste });
            mnuEdit.Name = "mnuEdit";
            mnuEdit.ShortcutKeys = Keys.Alt | Keys.E;
            mnuEdit.Size = new Size(39, 20);
            mnuEdit.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            mnuEditUndo.Enabled = false;
            mnuEditUndo.Name = "mnuEditUndo";
            mnuEditUndo.ShortcutKeys = Keys.Control | Keys.Z;
            mnuEditUndo.Size = new Size(144, 22);
            mnuEditUndo.Text = "&Undo";
            mnuEditUndo.Click += mnuEditUndo_Click;
            // 
            // mnuEditRedo
            // 
            mnuEditRedo.Enabled = false;
            mnuEditRedo.Name = "mnuEditRedo";
            mnuEditRedo.ShortcutKeys = Keys.Control | Keys.Y;
            mnuEditRedo.Size = new Size(144, 22);
            mnuEditRedo.Text = "&Redo";
            mnuEditRedo.Click += mnuEditRedo_Click;
            // 
            // mnuEditSeparator
            // 
            mnuEditSeparator.Name = "mnuEditSeparator";
            mnuEditSeparator.Size = new Size(141, 6);
            // 
            // mnuEditCopy
            // 
            mnuEditCopy.Name = "mnuEditCopy";
            mnuEditCopy.ShortcutKeys = Keys.Control | Keys.C;
            mnuEditCopy.Size = new Size(144, 22);
            mnuEditCopy.Text = "&Copy";
            mnuEditCopy.Click += mnuEditCopy_Click;
            // 
            // mnuEditPaste
            // 
            mnuEditPaste.Name = "mnuEditPaste";
            mnuEditPaste.ShortcutKeys = Keys.Control | Keys.V;
            mnuEditPaste.Size = new Size(144, 22);
            mnuEditPaste.Text = "&Paste";
            mnuEditPaste.Click += mnuEditPaste_Click;
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new ToolStripItem[] { mnuViewZoomIn, mnuViewZoomOut });
            mnuView.Name = "mnuView";
            mnuView.ShortcutKeys = Keys.Alt | Keys.V;
            mnuView.Size = new Size(44, 20);
            mnuView.Text = "&View";
            // 
            // mnuViewZoomIn
            // 
            mnuViewZoomIn.Enabled = false;
            mnuViewZoomIn.Name = "mnuViewZoomIn";
            mnuViewZoomIn.ShortcutKeys = Keys.Control | Keys.Add;
            mnuViewZoomIn.Size = new Size(207, 22);
            mnuViewZoomIn.Text = "Zoom &In";
            mnuViewZoomIn.Click += mnuViewZoomIn_Click;
            // 
            // mnuViewZoomOut
            // 
            mnuViewZoomOut.Enabled = false;
            mnuViewZoomOut.Name = "mnuViewZoomOut";
            mnuViewZoomOut.ShortcutKeys = Keys.Control | Keys.Subtract;
            mnuViewZoomOut.Size = new Size(207, 22);
            mnuViewZoomOut.Text = "Zoom &Out";
            mnuViewZoomOut.Click += mnuViewZoomOut_Click;
            // 
            // mnuSprite
            // 
            mnuSprite.DropDownItems.AddRange(new ToolStripItem[] { mnuSpriteFlipHorizontal, mnuSpriteFlipVertical, mnuSpriteSeparator, mnuSpriteShiftLeft, mnuSpriteShiftRight, mnuSpriteShiftUp, mnuSpriteShiftDown });
            mnuSprite.Name = "mnuSprite";
            mnuSprite.ShortcutKeys = Keys.Alt | Keys.S;
            mnuSprite.Size = new Size(49, 20);
            mnuSprite.Text = "&Sprite";
            // 
            // mnuSpriteFlipHorizontal
            // 
            mnuSpriteFlipHorizontal.Name = "mnuSpriteFlipHorizontal";
            mnuSpriteFlipHorizontal.ShortcutKeys = Keys.Control | Keys.Shift | Keys.H;
            mnuSpriteFlipHorizontal.Size = new Size(276, 22);
            mnuSpriteFlipHorizontal.Text = "Flip &Horizontal";
            mnuSpriteFlipHorizontal.Click += mnuSpriteFlipHorizontal_Click;
            // 
            // mnuSpriteFlipVertical
            // 
            mnuSpriteFlipVertical.Name = "mnuSpriteFlipVertical";
            mnuSpriteFlipVertical.ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;
            mnuSpriteFlipVertical.Size = new Size(276, 22);
            mnuSpriteFlipVertical.Text = "Flip &Vertical";
            mnuSpriteFlipVertical.Click += mnuSpriteFlipVertical_Click;
            // 
            // mnuSpriteSeparator
            // 
            mnuSpriteSeparator.Name = "mnuSpriteSeparator";
            mnuSpriteSeparator.Size = new Size(273, 6);
            // 
            // mnuSpriteShiftLeft
            // 
            mnuSpriteShiftLeft.Name = "mnuSpriteShiftLeft";
            mnuSpriteShiftLeft.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Left;
            mnuSpriteShiftLeft.Size = new Size(276, 22);
            mnuSpriteShiftLeft.Text = "Shift all pixels &Left";
            mnuSpriteShiftLeft.Click += mnuSpriteShiftLeft_Click;
            // 
            // mnuSpriteShiftRight
            // 
            mnuSpriteShiftRight.Name = "mnuSpriteShiftRight";
            mnuSpriteShiftRight.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Right;
            mnuSpriteShiftRight.Size = new Size(276, 22);
            mnuSpriteShiftRight.Text = "Shift all pixels &Right";
            mnuSpriteShiftRight.Click += mnuSpriteShiftRight_Click;
            // 
            // mnuSpriteShiftUp
            // 
            mnuSpriteShiftUp.Name = "mnuSpriteShiftUp";
            mnuSpriteShiftUp.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Up;
            mnuSpriteShiftUp.Size = new Size(276, 22);
            mnuSpriteShiftUp.Text = "Shift all pixels &Up";
            mnuSpriteShiftUp.Click += mnuSpriteShiftUp_Click;
            // 
            // mnuSpriteShiftDown
            // 
            mnuSpriteShiftDown.Name = "mnuSpriteShiftDown";
            mnuSpriteShiftDown.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Down;
            mnuSpriteShiftDown.Size = new Size(276, 22);
            mnuSpriteShiftDown.Text = "Shift all pixels &Down";
            mnuSpriteShiftDown.Click += mnuSpriteShiftDown_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { StatusLabel, Spacer, CoordinatesLabel });
            statusStrip.Location = new Point(0, 523);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(882, 24);
            statusStrip.TabIndex = 1;
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(110, 19);
            StatusLabel.Text = "No ROMset loaded.";
            // 
            // Spacer
            // 
            Spacer.Name = "Spacer";
            Spacer.Size = new Size(707, 19);
            Spacer.Spring = true;
            // 
            // CoordinatesLabel
            // 
            CoordinatesLabel.Alignment = ToolStripItemAlignment.Right;
            CoordinatesLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            CoordinatesLabel.Name = "CoordinatesLabel";
            CoordinatesLabel.Size = new Size(50, 19);
            CoordinatesLabel.Text = "X: - Y: -";
            // 
            // topPanel
            // 
            topPanel.Controls.Add(tableLayoutPanel);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 24);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(882, 40);
            topPanel.TabIndex = 2;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 6;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel.Controls.Add(lblSprite, 0, 0);
            tableLayoutPanel.Controls.Add(cboSprite, 1, 0);
            tableLayoutPanel.Controls.Add(lblZoom, 2, 0);
            tableLayoutPanel.Controls.Add(nudZoom, 3, 0);
            tableLayoutPanel.Controls.Add(lblPalette, 4, 0);
            tableLayoutPanel.Controls.Add(pnlPalette, 5, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.Size = new Size(882, 40);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblSprite
            // 
            lblSprite.AutoSize = true;
            lblSprite.Dock = DockStyle.Fill;
            lblSprite.Location = new Point(3, 0);
            lblSprite.Name = "lblSprite";
            lblSprite.Size = new Size(40, 40);
            lblSprite.TabIndex = 0;
            lblSprite.Text = "Sprite:";
            lblSprite.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboSprite
            // 
            cboSprite.Dock = DockStyle.Fill;
            cboSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSprite.Enabled = false;
            cboSprite.FormattingEnabled = true;
            cboSprite.Location = new Point(49, 8);
            cboSprite.Margin = new Padding(3, 8, 3, 3);
            cboSprite.Name = "cboSprite";
            cboSprite.Size = new Size(288, 23);
            cboSprite.TabIndex = 1;
            cboSprite.SelectedIndexChanged += cboSprite_SelectedIndexChanged;
            // 
            // lblZoom
            // 
            lblZoom.AutoSize = true;
            lblZoom.Dock = DockStyle.Fill;
            lblZoom.Location = new Point(343, 0);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(42, 40);
            lblZoom.TabIndex = 2;
            lblZoom.Text = "Zoom:";
            lblZoom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nudZoom
            // 
            nudZoom.Dock = DockStyle.Fill;
            nudZoom.Enabled = false;
            nudZoom.Location = new Point(391, 8);
            nudZoom.Margin = new Padding(3, 8, 3, 3);
            nudZoom.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            nudZoom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudZoom.Name = "nudZoom";
            nudZoom.Size = new Size(104, 23);
            nudZoom.TabIndex = 3;
            nudZoom.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudZoom.ValueChanged += nudZoom_ValueChanged;
            // 
            // lblPalette
            // 
            lblPalette.AutoSize = true;
            lblPalette.Dock = DockStyle.Fill;
            lblPalette.Location = new Point(501, 0);
            lblPalette.Name = "lblPalette";
            lblPalette.Size = new Size(46, 40);
            lblPalette.TabIndex = 4;
            lblPalette.Text = "Palette:";
            lblPalette.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlPalette
            // 
            pnlPalette.BackColor = Color.DimGray;
            pnlPalette.BorderStyle = BorderStyle.FixedSingle;
            pnlPalette.Dock = DockStyle.Fill;
            pnlPalette.Location = new Point(553, 8);
            pnlPalette.Margin = new Padding(3, 8, 3, 3);
            pnlPalette.Name = "pnlPalette";
            pnlPalette.Size = new Size(326, 29);
            pnlPalette.TabIndex = 5;
            // 
            // spriteDisplay
            // 
            spriteDisplay.BackColor = Color.Transparent;
            spriteDisplay.Dock = DockStyle.Fill;
            spriteDisplay.GridColor = Color.FromArgb(80, 80, 80);
            spriteDisplay.Location = new Point(0, 64);
            spriteDisplay.Name = "spriteDisplay";
            spriteDisplay.Size = new Size(882, 459);
            spriteDisplay.Sprite = null;
            spriteDisplay.SpriteRenderer = null;
            spriteDisplay.TabIndex = 0;
            spriteDisplay.TabStop = false;
            spriteDisplay.ZoomLevel = 1;
            spriteDisplay.ZoomLevelThreshold = 3;
            spriteDisplay.GridCellMouseMove += SpriteDisplay_GridCellMouseMove;
            spriteDisplay.GridCellMouseDown += spriteDisplay_GridCellMouseDown;
            spriteDisplay.GridCellMouseUp += spriteDisplay_GridCellMouseUp;
            // 
            // magnifierPanel
            // 
            magnifierPanel.Location = new Point(0, 0);
            magnifierPanel.Name = "magnifierPanel";
            magnifierPanel.Size = new Size(200, 100);
            magnifierPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 547);
            Controls.Add(spriteDisplay);
            Controls.Add(topPanel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "Williams Graphics Editor";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            topPanel.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudZoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)spriteDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void MnuEditCopy_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileLoad;
        private ToolStripMenuItem mnuFileLoadRobotron;
        private ToolStripMenuItem mnuFileLoadRobotronBlueLabel;
        private ToolStripMenuItem mnuFileLoadRobotronTieDieWDPU;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditUndo;
        private ToolStripMenuItem mnuEditRedo;
        private ToolStripSeparator mnuEditSeparator;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewZoomIn;
        private ToolStripMenuItem mnuViewZoomOut;
        private ToolStripMenuItem mnuSprite;
        private ToolStripMenuItem mnuSpriteFlipHorizontal;
        private ToolStripMenuItem mnuSpriteFlipVertical;
        private ToolStripSeparator mnuSpriteSeparator;
        private ToolStripMenuItem mnuSpriteShiftLeft;
        private ToolStripMenuItem mnuSpriteShiftRight;
        private ToolStripMenuItem mnuSpriteShiftUp;
        private ToolStripMenuItem mnuSpriteShiftDown;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel StatusLabel;
        private ToolStripStatusLabel Spacer;
        private ToolStripStatusLabel CoordinatesLabel;
        private Panel topPanel;
        private TableLayoutPanel tableLayoutPanel;
        private Label lblSprite;
        private ComboBox cboSprite;
        private Label lblZoom;
        private NumericUpDown nudZoom;
        private Label lblPalette;
        private PalettePanel pnlPalette;
        private Panel magnifierPanel;
        private SpriteDisplayControl spriteDisplay;
    }
}
