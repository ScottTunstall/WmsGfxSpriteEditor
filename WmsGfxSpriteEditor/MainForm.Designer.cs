using WmsGfxSpriteEditor.Controls;

namespace WmsGfxSpriteEditor
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            mnuFileLoadRobotronTieDieMAME = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            mnuFileSave = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            mnuEdit = new ToolStripMenuItem();
            mnuEditUndo = new ToolStripMenuItem();
            mnuEditRedo = new ToolStripMenuItem();
            mnuEditSeparator = new ToolStripSeparator();
            mnuEditCopy = new ToolStripMenuItem();
            mnuCopySprite = new ToolStripMenuItem();
            mnuCopySelectedColour = new ToolStripMenuItem();
            mnuCopySelectedColourHex = new ToolStripMenuItem();
            mnuCopySelectedColourRgb = new ToolStripMenuItem();
            mnuEditPaste = new ToolStripMenuItem();
            mnuView = new ToolStripMenuItem();
            mnuViewZoomIn = new ToolStripMenuItem();
            mnuViewZoomOut = new ToolStripMenuItem();
            mnuViewZoomToWindow = new ToolStripMenuItem();
            mnuViewPaletteSeparator = new ToolStripSeparator();
            mnuViewPalette = new ToolStripMenuItem();
            mnuSprite = new ToolStripMenuItem();
            mnuSpriteGotoNextSprite = new ToolStripMenuItem();
            mnuSpriteGoToPreviousSprite = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            mnuSpriteFlipHorizontal = new ToolStripMenuItem();
            mnuSpriteFlipVertical = new ToolStripMenuItem();
            mnuSpriteSeparator = new ToolStripSeparator();
            mnuSpriteShiftLeft = new ToolStripMenuItem();
            mnuSpriteShiftRight = new ToolStripMenuItem();
            mnuSpriteShiftUp = new ToolStripMenuItem();
            mnuSpriteShiftDown = new ToolStripMenuItem();
            mnuHelp = new ToolStripMenuItem();
            mnuHelpAbout = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            Spacer = new ToolStripStatusLabel();
            CoordinatesLabel = new ToolStripStatusLabel();
            btnZoomOut = new ToolStripButton();
            trackerZoom = new TrackBar();
            trackerZoomHost = new ToolStripControlHost(trackerZoom);
            btnZoomIn = new ToolStripButton();
            topPanel = new Panel();
            tableLayoutPanel = new TableLayoutPanel();
            lblSprite = new Label();
            cboSprite = new ComboBox();
            lblZoom = new Label();
            nudZoom = new NumericUpDown();
            toolStripQuickAccess = new ToolStrip();
            btnShowPalette = new ToolStripButton();
            magnificationPanel = new MagnificationPanel();
            spriteDisplay = new SpriteDisplayControl();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            topPanel.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudZoom).BeginInit();
            toolStripQuickAccess.SuspendLayout();
            magnificationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spriteDisplay).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuFile, mnuEdit, mnuView, mnuSprite, mnuHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(882, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLoad, toolStripMenuItem1, mnuFileSave, toolStripMenuItem2 });
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
            mnuFileLoad.Size = new Size(146, 22);
            mnuFileLoad.Text = "&Open";
            // 
            // mnuFileLoadRobotron
            // 
            mnuFileLoadRobotron.DropDownItems.AddRange(new ToolStripItem[] { mnuFileLoadRobotronBlueLabel, mnuFileLoadRobotronTieDieWDPU, mnuFileLoadRobotronTieDieMAME });
            mnuFileLoadRobotron.Name = "mnuFileLoadRobotron";
            mnuFileLoadRobotron.Size = new Size(154, 22);
            mnuFileLoadRobotron.Text = "&Robotron: 2084";
            // 
            // mnuFileLoadRobotronBlueLabel
            // 
            mnuFileLoadRobotronBlueLabel.Name = "mnuFileLoadRobotronBlueLabel";
            mnuFileLoadRobotronBlueLabel.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            mnuFileLoadRobotronBlueLabel.Size = new Size(201, 22);
            mnuFileLoadRobotronBlueLabel.Text = "&Blue Label";
            mnuFileLoadRobotronBlueLabel.Click += mnuFileLoadRobotronBlueLabel_Click;
            // 
            // mnuFileLoadRobotronTieDieWDPU
            // 
            mnuFileLoadRobotronTieDieWDPU.Name = "mnuFileLoadRobotronTieDieWDPU";
            mnuFileLoadRobotronTieDieWDPU.Size = new Size(201, 22);
            mnuFileLoadRobotronTieDieWDPU.Text = "Tie Die (&WDPU)";
            mnuFileLoadRobotronTieDieWDPU.Click += mnuFileLoadRobotronTieDieWDPU_Click;
            // 
            // mnuFileLoadRobotronTieDieMAME
            // 
            mnuFileLoadRobotronTieDieMAME.Name = "mnuFileLoadRobotronTieDieMAME";
            mnuFileLoadRobotronTieDieMAME.Size = new Size(201, 22);
            mnuFileLoadRobotronTieDieMAME.Text = "Tie Die (&MAME 0.265)";
            mnuFileLoadRobotronTieDieMAME.Click += mnuFileLoadRobotronTieDieMAME_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(143, 6);
            // 
            // mnuFileSave
            // 
            mnuFileSave.Enabled = false;
            mnuFileSave.Name = "mnuFileSave";
            mnuFileSave.ShortcutKeys = Keys.Control | Keys.S;
            mnuFileSave.Size = new Size(146, 22);
            mnuFileSave.Text = "&Save";
            mnuFileSave.Click += mnuFileSave_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(143, 6);
            // 
            // mnuEdit
            // 
            mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuEditUndo, mnuEditRedo, mnuEditSeparator, mnuEditCopy, mnuEditPaste });
            mnuEdit.Enabled = false;
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
            mnuEditCopy.DropDownItems.AddRange(new ToolStripItem[] { mnuCopySprite, mnuCopySelectedColour });
            mnuEditCopy.Enabled = false;
            mnuEditCopy.Name = "mnuEditCopy";
            mnuEditCopy.Size = new Size(144, 22);
            mnuEditCopy.Text = "&Copy";
            // 
            // mnuCopySprite
            // 
            mnuCopySprite.Name = "mnuCopySprite";
            mnuCopySprite.ShortcutKeys = Keys.Control | Keys.C;
            mnuCopySprite.Size = new Size(157, 22);
            mnuCopySprite.Text = "&Sprite";
            mnuCopySprite.Click += mnuEditCopy_Click;
            // 
            // mnuCopySelectedColour
            // 
            mnuCopySelectedColour.DropDownItems.AddRange(new ToolStripItem[] { mnuCopySelectedColourHex, mnuCopySelectedColourRgb });
            mnuCopySelectedColour.Name = "mnuCopySelectedColour";
            mnuCopySelectedColour.Size = new Size(157, 22);
            mnuCopySelectedColour.Text = "Selected Colour";
            // 
            // mnuCopySelectedColourHex
            // 
            mnuCopySelectedColourHex.Name = "mnuCopySelectedColourHex";
            mnuCopySelectedColourHex.Size = new Size(96, 22);
            mnuCopySelectedColourHex.Text = "&Hex";
            mnuCopySelectedColourHex.Click += mnuCopySelectedColourHex_Click;
            // 
            // mnuCopySelectedColourRgb
            // 
            mnuCopySelectedColourRgb.Name = "mnuCopySelectedColourRgb";
            mnuCopySelectedColourRgb.Size = new Size(96, 22);
            mnuCopySelectedColourRgb.Text = "&RGB";
            mnuCopySelectedColourRgb.Click += mnuCopySelectedColourRgb_Click;
            // 
            // mnuEditPaste
            // 
            mnuEditPaste.Enabled = false;
            mnuEditPaste.Name = "mnuEditPaste";
            mnuEditPaste.ShortcutKeys = Keys.Control | Keys.V;
            mnuEditPaste.Size = new Size(144, 22);
            mnuEditPaste.Text = "&Paste";
            mnuEditPaste.Click += mnuEditPaste_Click;
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new ToolStripItem[] { mnuViewZoomIn, mnuViewZoomOut, mnuViewZoomToWindow, mnuViewPaletteSeparator, mnuViewPalette });
            mnuView.Enabled = false;
            mnuView.Name = "mnuView";
            mnuView.ShortcutKeys = Keys.Alt | Keys.V;
            mnuView.Size = new Size(44, 20);
            mnuView.Text = "&View";
            // 
            // mnuViewZoomIn
            // 
            mnuViewZoomIn.Enabled = false;
            mnuViewZoomIn.Name = "mnuViewZoomIn";
            mnuViewZoomIn.ShortcutKeyDisplayString = "Ctrl ++";
            mnuViewZoomIn.ShortcutKeys = Keys.Control | Keys.Oemplus;
            mnuViewZoomIn.Size = new Size(208, 22);
            mnuViewZoomIn.Text = "Zoom &In";
            mnuViewZoomIn.Click += mnuViewZoomIn_Click;
            // 
            // mnuViewZoomOut
            // 
            mnuViewZoomOut.Enabled = false;
            mnuViewZoomOut.Name = "mnuViewZoomOut";
            mnuViewZoomOut.ShortcutKeyDisplayString = "Ctrl +-";
            mnuViewZoomOut.ShortcutKeys = Keys.Control | Keys.OemMinus;
            mnuViewZoomOut.Size = new Size(208, 22);
            mnuViewZoomOut.Text = "Zoom &Out";
            mnuViewZoomOut.Click += mnuViewZoomOut_Click;
            // 
            // mnuViewZoomToWindow
            // 
            mnuViewZoomToWindow.Enabled = false;
            mnuViewZoomToWindow.Name = "mnuViewZoomToWindow";
            mnuViewZoomToWindow.ShortcutKeys = Keys.Control | Keys.B;
            mnuViewZoomToWindow.Size = new Size(208, 22);
            mnuViewZoomToWindow.Text = "Zoom to Window";
            mnuViewZoomToWindow.Click += mnuViewZoomToWindow_Click;
            // 
            // mnuViewPaletteSeparator
            // 
            mnuViewPaletteSeparator.Name = "mnuViewPaletteSeparator";
            mnuViewPaletteSeparator.Size = new Size(205, 6);
            // 
            // mnuViewPalette
            // 
            mnuViewPalette.Enabled = false;
            mnuViewPalette.Name = "mnuViewPalette";
            mnuViewPalette.ShortcutKeys = Keys.F8;
            mnuViewPalette.Size = new Size(208, 22);
            mnuViewPalette.Text = "Palette";
            mnuViewPalette.Click += mnuViewPalette_Click;
            // 
            // mnuSprite
            // 
            mnuSprite.DropDownItems.AddRange(new ToolStripItem[] { mnuSpriteGotoNextSprite, mnuSpriteGoToPreviousSprite, toolStripMenuItem3, mnuSpriteFlipHorizontal, mnuSpriteFlipVertical, mnuSpriteSeparator, mnuSpriteShiftLeft, mnuSpriteShiftRight, mnuSpriteShiftUp, mnuSpriteShiftDown });
            mnuSprite.Enabled = false;
            mnuSprite.Name = "mnuSprite";
            mnuSprite.ShortcutKeys = Keys.Alt | Keys.S;
            mnuSprite.Size = new Size(49, 20);
            mnuSprite.Text = "&Sprite";
            // 
            // mnuSpriteGotoNextSprite
            // 
            mnuSpriteGotoNextSprite.Enabled = false;
            mnuSpriteGotoNextSprite.Name = "mnuSpriteGotoNextSprite";
            mnuSpriteGotoNextSprite.ShortcutKeys = Keys.Control | Keys.Down;
            mnuSpriteGotoNextSprite.Size = new Size(276, 22);
            mnuSpriteGotoNextSprite.Text = "&Next";
            mnuSpriteGotoNextSprite.Click += mnuSpriteGotoNextSprite_Click;
            // 
            // mnuSpriteGoToPreviousSprite
            // 
            mnuSpriteGoToPreviousSprite.Enabled = false;
            mnuSpriteGoToPreviousSprite.Name = "mnuSpriteGoToPreviousSprite";
            mnuSpriteGoToPreviousSprite.ShortcutKeys = Keys.Control | Keys.Up;
            mnuSpriteGoToPreviousSprite.Size = new Size(276, 22);
            mnuSpriteGoToPreviousSprite.Text = "&Previous";
            mnuSpriteGoToPreviousSprite.Click += mnuSpriteGoToPreviousSprite_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(273, 6);
            // 
            // mnuSpriteFlipHorizontal
            // 
            mnuSpriteFlipHorizontal.Enabled = false;
            mnuSpriteFlipHorizontal.Name = "mnuSpriteFlipHorizontal";
            mnuSpriteFlipHorizontal.ShortcutKeys = Keys.Control | Keys.Shift | Keys.H;
            mnuSpriteFlipHorizontal.Size = new Size(276, 22);
            mnuSpriteFlipHorizontal.Text = "Flip &Horizontal";
            mnuSpriteFlipHorizontal.Click += mnuSpriteFlipHorizontal_Click;
            // 
            // mnuSpriteFlipVertical
            // 
            mnuSpriteFlipVertical.Enabled = false;
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
            mnuSpriteShiftLeft.Enabled = false;
            mnuSpriteShiftLeft.Name = "mnuSpriteShiftLeft";
            mnuSpriteShiftLeft.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Left;
            mnuSpriteShiftLeft.Size = new Size(276, 22);
            mnuSpriteShiftLeft.Text = "Shift all pixels &Left";
            mnuSpriteShiftLeft.Click += mnuSpriteShiftLeft_Click;
            // 
            // mnuSpriteShiftRight
            // 
            mnuSpriteShiftRight.Enabled = false;
            mnuSpriteShiftRight.Name = "mnuSpriteShiftRight";
            mnuSpriteShiftRight.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Right;
            mnuSpriteShiftRight.Size = new Size(276, 22);
            mnuSpriteShiftRight.Text = "Shift all pixels &Right";
            mnuSpriteShiftRight.Click += mnuSpriteShiftRight_Click;
            // 
            // mnuSpriteShiftUp
            // 
            mnuSpriteShiftUp.Enabled = false;
            mnuSpriteShiftUp.Name = "mnuSpriteShiftUp";
            mnuSpriteShiftUp.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Up;
            mnuSpriteShiftUp.Size = new Size(276, 22);
            mnuSpriteShiftUp.Text = "Shift all pixels &Up";
            mnuSpriteShiftUp.Click += mnuSpriteShiftUp_Click;
            // 
            // mnuSpriteShiftDown
            // 
            mnuSpriteShiftDown.Enabled = false;
            mnuSpriteShiftDown.Name = "mnuSpriteShiftDown";
            mnuSpriteShiftDown.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Down;
            mnuSpriteShiftDown.Size = new Size(276, 22);
            mnuSpriteShiftDown.Text = "Shift all pixels &Down";
            mnuSpriteShiftDown.Click += mnuSpriteShiftDown_Click;
            // 
            // mnuHelp
            // 
            mnuHelp.DropDownItems.AddRange(new ToolStripItem[] { mnuHelpAbout });
            mnuHelp.Name = "mnuHelp";
            mnuHelp.ShortcutKeys = Keys.Alt | Keys.H;
            mnuHelp.Size = new Size(44, 20);
            mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            mnuHelpAbout.Name = "mnuHelpAbout";
            mnuHelpAbout.ShortcutKeys = Keys.F1;
            mnuHelpAbout.Size = new Size(135, 22);
            mnuHelpAbout.Text = "About...";
            mnuHelpAbout.Click += mnuHelpAbout_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { StatusLabel, Spacer, CoordinatesLabel, btnZoomOut, trackerZoomHost, btnZoomIn });
            statusStrip.Location = new Point(0, 523);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(882, 24);
            statusStrip.TabIndex = 1;
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(110, 42);
            StatusLabel.Text = "No ROMset loaded.";
            // 
            // Spacer
            // 
            Spacer.Name = "Spacer";
            Spacer.Size = new Size(530, 42);
            Spacer.Spring = true;
            // 
            // CoordinatesLabel
            // 
            CoordinatesLabel.Alignment = ToolStripItemAlignment.Right;
            CoordinatesLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            CoordinatesLabel.Name = "CoordinatesLabel";
            CoordinatesLabel.Size = new Size(50, 42);
            CoordinatesLabel.Text = "X: - Y: -";
            // 
            // btnZoomOut
            // 
            btnZoomOut.Image = (Image)resources.GetObject("btnZoomOut.Image");
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(23, 22);
            btnZoomOut.Click += mnuViewZoomOut_Click;


            // 
            // trackerZoomHost
            // 
            trackerZoomHost.Alignment = ToolStripItemAlignment.Right;
            trackerZoomHost.AutoSize = false;
            trackerZoomHost.Size = new Size(100, 22);

            //
            // trackerZoom
            // 
            trackerZoom.Maximum = 32;
            trackerZoom.Minimum = 1;
            trackerZoom.Name = "trackerZoom";
            trackerZoom.Size = new Size(100, 18);
            trackerZoom.Value = 1;

            // 
            // btnZoomIn
            // 
            btnZoomIn.Image = (Image)resources.GetObject("btnZoomIn.Image");
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(23, 45);
            btnZoomIn.Click += mnuViewZoomIn_Click;
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
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.Controls.Add(lblSprite, 0, 0);
            tableLayoutPanel.Controls.Add(cboSprite, 1, 0);
            tableLayoutPanel.Controls.Add(lblZoom, 2, 0);
            tableLayoutPanel.Controls.Add(nudZoom, 3, 0);
            tableLayoutPanel.Controls.Add(toolStripQuickAccess, 5, 0);
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
            lblSprite.Font = new Font("Segoe UI", 9F);
            lblSprite.Location = new Point(3, 0);
            lblSprite.Name = "lblSprite";
            lblSprite.Size = new Size(40, 40);
            lblSprite.TabIndex = 0;
            lblSprite.Text = "Sprite:";
            lblSprite.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboSprite
            // 
            cboSprite.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSprite.Enabled = false;
            cboSprite.FormattingEnabled = true;
            cboSprite.Location = new Point(49, 8);
            cboSprite.Margin = new Padding(3, 8, 3, 3);
            cboSprite.Name = "cboSprite";
            cboSprite.Size = new Size(277, 23);
            cboSprite.TabIndex = 1;
            cboSprite.SelectedIndexChanged += cboSprite_SelectedIndexChanged;
            // 
            // lblZoom
            // 
            lblZoom.AutoSize = true;
            lblZoom.Dock = DockStyle.Fill;
            lblZoom.Location = new Point(332, 0);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new Size(42, 40);
            lblZoom.TabIndex = 2;
            lblZoom.Text = "Zoom:";
            lblZoom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // nudZoom
            // 
            nudZoom.Enabled = false;
            nudZoom.Location = new Point(380, 8);
            nudZoom.Margin = new Padding(3, 8, 3, 3);
            nudZoom.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            nudZoom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudZoom.Name = "nudZoom";
            nudZoom.Size = new Size(76, 23);
            nudZoom.TabIndex = 3;
            nudZoom.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudZoom.ValueChanged += nudZoom_ValueChanged;
            // 
            // toolStripQuickAccess
            // 
            toolStripQuickAccess.Dock = DockStyle.Fill;
            toolStripQuickAccess.GripStyle = ToolStripGripStyle.Hidden;
            toolStripQuickAccess.Items.AddRange(new ToolStripItem[] { btnShowPalette });
            toolStripQuickAccess.Location = new Point(856, 0);
            toolStripQuickAccess.Name = "toolStripQuickAccess";
            toolStripQuickAccess.Size = new Size(26, 40);
            toolStripQuickAccess.TabIndex = 5;
            toolStripQuickAccess.Text = "toolStrip1";
            // 
            // btnShowPalette
            // 
            btnShowPalette.CheckOnClick = true;
            btnShowPalette.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnShowPalette.Enabled = false;
            btnShowPalette.Image = (Image)resources.GetObject("btnShowPalette.Image");
            btnShowPalette.ImageTransparentColor = Color.Transparent;
            btnShowPalette.Name = "btnShowPalette";
            btnShowPalette.Size = new Size(23, 37);
            btnShowPalette.Text = "Show Palette (F8)";
            btnShowPalette.Click += btnShowPalette_Click;
            // 
            // magnificationPanel
            // 
            magnificationPanel.AutoScroll = true;
            magnificationPanel.BackColor = Color.LightGray;
            magnificationPanel.Controls.Add(spriteDisplay);
            magnificationPanel.Dock = DockStyle.Fill;
            magnificationPanel.Location = new Point(0, 64);
            magnificationPanel.Name = "magnificationPanel";
            magnificationPanel.Size = new Size(882, 459);
            magnificationPanel.TabIndex = 3;
            magnificationPanel.ZoomMouseWheel += MagnificationPanel_MouseWheel;
            // 
            // spriteDisplay
            // 
            spriteDisplay.BackColor = Color.LightGray;
            spriteDisplay.GridColor = Color.FromArgb(80, 80, 80);
            spriteDisplay.Location = new Point(0, 0);
            spriteDisplay.Name = "spriteDisplay";
            spriteDisplay.Size = new Size(882, 456);
            spriteDisplay.Sprite = null;
            spriteDisplay.SpriteRenderer = null;
            spriteDisplay.TabIndex = 1;
            spriteDisplay.TabStop = false;
            spriteDisplay.ZoomLevel = 1;
            spriteDisplay.ZoomLevelThreshold = 3;
            spriteDisplay.GridCellMouseMove += spriteDisplay_GridCellMouseMove;
            spriteDisplay.GridCellMouseDown += spriteDisplay_GridCellMouseDown;
            spriteDisplay.GridCellMouseUp += spriteDisplay_GridCellMouseUp;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 547);
            Controls.Add(magnificationPanel);
            Controls.Add(topPanel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Williams Graphics Editor";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            topPanel.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudZoom).EndInit();
            toolStripQuickAccess.ResumeLayout(false);
            toolStripQuickAccess.PerformLayout();
            magnificationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spriteDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem mnuViewZoomToWindow;
        private ToolStripSeparator mnuViewPaletteSeparator;
        private ToolStripMenuItem mnuViewPalette;
        private ToolStripMenuItem mnuSprite;
        private ToolStripMenuItem mnuSpriteFlipHorizontal;
        private ToolStripMenuItem mnuSpriteFlipVertical;
        private ToolStripSeparator mnuSpriteSeparator;
        private ToolStripMenuItem mnuSpriteShiftLeft;
        private ToolStripMenuItem mnuSpriteShiftRight;
        private ToolStripMenuItem mnuSpriteShiftUp;
        private ToolStripMenuItem mnuSpriteShiftDown;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
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
        private ToolStripMenuItem mnuCopySelectedColour;
        private ToolStripMenuItem mnuCopySelectedColourHex;
        private ToolStripMenuItem mnuCopySelectedColourRgb;
        private ToolStripMenuItem mnuCopySprite;
        private ToolStrip toolStripQuickAccess;
        private ToolStripButton btnShowPalette;
        private ToolStripButton btnZoomOut;
        private TrackBar trackerZoom;
        private ToolStripButton btnZoomIn;
        private MagnificationPanel magnificationPanel;
        private SpriteDisplayControl spriteDisplay;
        private ToolStripMenuItem mnuFileLoadRobotronTieDieMAME;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuSpriteGotoNextSprite;
        private ToolStripMenuItem mnuSpriteGoToPreviousSprite;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripControlHost trackerZoomHost;
    }
}
