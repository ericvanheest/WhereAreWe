namespace WhereAreWe
{
    partial class EditLabelsForm : HackerBasedForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmLabels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.nudBorderOpacity = new System.Windows.Forms.NumericUpDown();
            this.nudBackgroundOpacity = new System.Windows.Forms.NumericUpDown();
            this.nudForegroundOpacity = new System.Windows.Forms.NumericUpDown();
            this.labelWarning = new System.Windows.Forms.Label();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.pbBorderColor = new System.Windows.Forms.PictureBox();
            this.pbBackgroundColor = new System.Windows.Forms.PictureBox();
            this.pbTextColor = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvLabels = new WhereAreWe.DBListView();
            this.chText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAnchor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.llAnchors = new System.Windows.Forms.LinkLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbShowAnchors = new System.Windows.Forms.CheckBox();
            this.lvAnchors = new System.Windows.Forms.ListView();
            this.chRectX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRectY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmAnchors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAnchorAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miAnchorDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.miAnchorDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudRectHeight = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudRectWidth = new System.Windows.Forms.NumericUpDown();
            this.nudRectY = new System.Windows.Forms.NumericUpDown();
            this.nudRectX = new System.Windows.Forms.NumericUpDown();
            this.cmLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorderOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackgroundOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForegroundOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmAnchors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectX)).BeginInit();
            this.SuspendLayout();
            // 
            // cmLabels
            // 
            this.cmLabels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxUndo,
            this.miCtxAdd,
            this.miCtxDuplicate,
            this.miCtxCopy,
            this.miCtxDelete});
            this.cmLabels.Name = "cmLabelContext";
            this.cmLabels.Size = new System.Drawing.Size(198, 114);
            this.cmLabels.Opening += new System.ComponentModel.CancelEventHandler(this.cmLabels_Opening);
            // 
            // miCtxUndo
            // 
            this.miCtxUndo.Name = "miCtxUndo";
            this.miCtxUndo.Size = new System.Drawing.Size(197, 22);
            this.miCtxUndo.Text = "&Undo";
            this.miCtxUndo.Click += new System.EventHandler(this.miCtxUndo_Click);
            // 
            // miCtxAdd
            // 
            this.miCtxAdd.Name = "miCtxAdd";
            this.miCtxAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miCtxAdd.Size = new System.Drawing.Size(197, 22);
            this.miCtxAdd.Text = "&Add new label";
            this.miCtxAdd.Click += new System.EventHandler(this.miCtxAdd_Click);
            // 
            // miCtxDuplicate
            // 
            this.miCtxDuplicate.Name = "miCtxDuplicate";
            this.miCtxDuplicate.Size = new System.Drawing.Size(197, 22);
            this.miCtxDuplicate.Text = "Duplicate";
            this.miCtxDuplicate.Click += new System.EventHandler(this.miCtxDuplicate_Click);
            // 
            // miCtxCopy
            // 
            this.miCtxCopy.Name = "miCtxCopy";
            this.miCtxCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCtxCopy.Size = new System.Drawing.Size(197, 22);
            this.miCtxCopy.Text = "&Copy to clipboard";
            this.miCtxCopy.Click += new System.EventHandler(this.miCtxCopy_Click);
            // 
            // miCtxDelete
            // 
            this.miCtxDelete.Name = "miCtxDelete";
            this.miCtxDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miCtxDelete.Size = new System.Drawing.Size(197, 22);
            this.miCtxDelete.Text = "&Delete";
            this.miCtxDelete.Click += new System.EventHandler(this.miCtxDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 28);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // nudBorderOpacity
            // 
            this.nudBorderOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBorderOpacity.Location = new System.Drawing.Point(420, 102);
            this.nudBorderOpacity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBorderOpacity.Name = "nudBorderOpacity";
            this.nudBorderOpacity.Size = new System.Drawing.Size(60, 20);
            this.nudBorderOpacity.TabIndex = 10;
            this.nudBorderOpacity.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBorderOpacity.ValueChanged += new System.EventHandler(this.nudBorderOpacity_ValueChanged);
            this.nudBorderOpacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudBorderOpacity_KeyDown);
            // 
            // nudBackgroundOpacity
            // 
            this.nudBackgroundOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBackgroundOpacity.Location = new System.Drawing.Point(420, 76);
            this.nudBackgroundOpacity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBackgroundOpacity.Name = "nudBackgroundOpacity";
            this.nudBackgroundOpacity.Size = new System.Drawing.Size(60, 20);
            this.nudBackgroundOpacity.TabIndex = 8;
            this.nudBackgroundOpacity.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBackgroundOpacity.ValueChanged += new System.EventHandler(this.nudBackgroundOpacity_ValueChanged);
            this.nudBackgroundOpacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudBackgroundOpacity_KeyDown);
            // 
            // nudForegroundOpacity
            // 
            this.nudForegroundOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudForegroundOpacity.Location = new System.Drawing.Point(420, 50);
            this.nudForegroundOpacity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudForegroundOpacity.Name = "nudForegroundOpacity";
            this.nudForegroundOpacity.Size = new System.Drawing.Size(60, 20);
            this.nudForegroundOpacity.TabIndex = 6;
            this.nudForegroundOpacity.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudForegroundOpacity.ValueChanged += new System.EventHandler(this.nudForegroundOpacity_ValueChanged);
            this.nudForegroundOpacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudForegroundOpacity_KeyDown);
            // 
            // labelWarning
            // 
            this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(313, 239);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(167, 93);
            this.labelWarning.TabIndex = 19;
            this.labelWarning.Text = "Warning:\r\nThere may only be one label at a given location.  Duplicate coordinates" +
    " will be offset by a small amount.";
            this.labelWarning.Visible = false;
            // 
            // nudSize
            // 
            this.nudSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSize.Enabled = false;
            this.nudSize.Location = new System.Drawing.Point(346, 202);
            this.nudSize.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(65, 20);
            this.nudSize.TabIndex = 18;
            this.nudSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            65536});
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            this.nudSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudSize_KeyDown);
            // 
            // nudY
            // 
            this.nudY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudY.DecimalPlaces = 2;
            this.nudY.Enabled = false;
            this.nudY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudY.Location = new System.Drawing.Point(346, 176);
            this.nudY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(65, 20);
            this.nudY.TabIndex = 16;
            this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
            this.nudY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudY_KeyDown);
            // 
            // nudX
            // 
            this.nudX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudX.DecimalPlaces = 2;
            this.nudX.Enabled = false;
            this.nudX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudX.Location = new System.Drawing.Point(346, 150);
            this.nudX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(65, 20);
            this.nudX.TabIndex = 14;
            this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
            this.nudX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudX_KeyDown);
            // 
            // pbBorderColor
            // 
            this.pbBorderColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBorderColor.Location = new System.Drawing.Point(385, 102);
            this.pbBorderColor.Name = "pbBorderColor";
            this.pbBorderColor.Size = new System.Drawing.Size(20, 20);
            this.pbBorderColor.TabIndex = 4;
            this.pbBorderColor.TabStop = false;
            this.pbBorderColor.Click += new System.EventHandler(this.pbBorderColor_Click);
            // 
            // pbBackgroundColor
            // 
            this.pbBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBackgroundColor.Location = new System.Drawing.Point(385, 76);
            this.pbBackgroundColor.Name = "pbBackgroundColor";
            this.pbBackgroundColor.Size = new System.Drawing.Size(20, 20);
            this.pbBackgroundColor.TabIndex = 4;
            this.pbBackgroundColor.TabStop = false;
            this.pbBackgroundColor.Click += new System.EventHandler(this.pbBackgroundColor_Click);
            // 
            // pbTextColor
            // 
            this.pbTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTextColor.Location = new System.Drawing.Point(385, 50);
            this.pbTextColor.Name = "pbTextColor";
            this.pbTextColor.Size = new System.Drawing.Size(20, 20);
            this.pbTextColor.TabIndex = 4;
            this.pbTextColor.TabStop = false;
            this.pbTextColor.Click += new System.EventHandler(this.pbTextColor_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Size:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Y:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "X:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Border:";
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Enabled = false;
            this.tbText.Location = new System.Drawing.Point(345, 6);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(135, 20);
            this.tbText.TabIndex = 3;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Background:";
            // 
            // labelLocation
            // 
            this.labelLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(346, 134);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(48, 13);
            this.labelLocation.TabIndex = 11;
            this.labelLocation.Text = "Location";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(420, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Opacity";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Foreground:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text:";
            // 
            // lvLabels
            // 
            this.lvLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLabels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chText,
            this.chX,
            this.chY,
            this.chSize,
            this.chAnchor});
            this.lvLabels.ContextMenuStrip = this.cmLabels;
            this.lvLabels.FullRowSelect = true;
            this.lvLabels.HideSelection = false;
            this.lvLabels.Location = new System.Drawing.Point(0, 0);
            this.lvLabels.Name = "lvLabels";
            this.lvLabels.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvLabels.Size = new System.Drawing.Size(307, 332);
            this.lvLabels.TabIndex = 1;
            this.lvLabels.UseCompatibleStateImageBehavior = false;
            this.lvLabels.View = System.Windows.Forms.View.Details;
            this.lvLabels.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvLabels_ColumnClick);
            this.lvLabels.SelectedIndexChanged += new System.EventHandler(this.lvLabels_SelectedIndexChanged);
            this.lvLabels.DoubleClick += new System.EventHandler(this.lvLabels_DoubleClick);
            this.lvLabels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvLabels_KeyDown);
            this.lvLabels.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvLabels_MouseDown);
            // 
            // chText
            // 
            this.chText.Text = "Text";
            this.chText.Width = 110;
            // 
            // chX
            // 
            this.chX.Text = "X";
            this.chX.Width = 40;
            // 
            // chY
            // 
            this.chY.Text = "Y";
            this.chY.Width = 40;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 41;
            // 
            // chAnchor
            // 
            this.chAnchor.Text = "Anchors";
            this.chAnchor.Width = 52;
            // 
            // llAnchors
            // 
            this.llAnchors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llAnchors.AutoSize = true;
            this.llAnchors.Location = new System.Drawing.Point(417, 134);
            this.llAnchors.Name = "llAnchors";
            this.llAnchors.Size = new System.Drawing.Size(61, 13);
            this.llAnchors.TabIndex = 12;
            this.llAnchors.TabStop = true;
            this.llAnchors.Text = "&Anchors >>";
            this.llAnchors.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAnchors_LinkClicked);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvLabels);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.llAnchors);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.nudBorderOpacity);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.nudBackgroundOpacity);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.nudForegroundOpacity);
            this.splitContainer1.Panel1.Controls.Add(this.labelLocation);
            this.splitContainer1.Panel1.Controls.Add(this.labelWarning);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.nudSize);
            this.splitContainer1.Panel1.Controls.Add(this.tbText);
            this.splitContainer1.Panel1.Controls.Add(this.nudY);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.nudX);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.pbBorderColor);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.pbBackgroundColor);
            this.splitContainer1.Panel1.Controls.Add(this.pbTextColor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cbShowAnchors);
            this.splitContainer1.Panel2.Controls.Add(this.lvAnchors);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.nudRectHeight);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.nudRectWidth);
            this.splitContainer1.Panel2.Controls.Add(this.nudRectY);
            this.splitContainer1.Panel2.Controls.Add(this.nudRectX);
            this.splitContainer1.Size = new System.Drawing.Size(688, 332);
            this.splitContainer1.SplitterDistance = 481;
            this.splitContainer1.TabIndex = 19;
            // 
            // cbShowAnchors
            // 
            this.cbShowAnchors.AutoSize = true;
            this.cbShowAnchors.Location = new System.Drawing.Point(3, 4);
            this.cbShowAnchors.Name = "cbShowAnchors";
            this.cbShowAnchors.Size = new System.Drawing.Size(167, 17);
            this.cbShowAnchors.TabIndex = 0;
            this.cbShowAnchors.Text = "Show anchor squares on map";
            this.cbShowAnchors.UseVisualStyleBackColor = true;
            this.cbShowAnchors.CheckedChanged += new System.EventHandler(this.cbShowAnchors_CheckedChanged);
            // 
            // lvAnchors
            // 
            this.lvAnchors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAnchors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRectX,
            this.chRectY,
            this.chWidth,
            this.chHeight});
            this.lvAnchors.ContextMenuStrip = this.cmAnchors;
            this.lvAnchors.FullRowSelect = true;
            this.lvAnchors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvAnchors.HideSelection = false;
            this.lvAnchors.Location = new System.Drawing.Point(1, 22);
            this.lvAnchors.Name = "lvAnchors";
            this.lvAnchors.Size = new System.Drawing.Size(201, 263);
            this.lvAnchors.TabIndex = 1;
            this.lvAnchors.UseCompatibleStateImageBehavior = false;
            this.lvAnchors.View = System.Windows.Forms.View.Details;
            this.lvAnchors.SelectedIndexChanged += new System.EventHandler(this.lvAnchors_SelectedIndexChanged);
            // 
            // chRectX
            // 
            this.chRectX.Text = "X";
            this.chRectX.Width = 45;
            // 
            // chRectY
            // 
            this.chRectY.Text = "Y";
            this.chRectY.Width = 45;
            // 
            // chWidth
            // 
            this.chWidth.Text = "Width";
            this.chWidth.Width = 45;
            // 
            // chHeight
            // 
            this.chHeight.Text = "Height";
            this.chHeight.Width = 45;
            // 
            // cmAnchors
            // 
            this.cmAnchors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAnchorAdd,
            this.miAnchorDuplicate,
            this.miAnchorDelete});
            this.cmAnchors.Name = "cmLabelContext";
            this.cmAnchors.Size = new System.Drawing.Size(128, 70);
            this.cmAnchors.Opening += new System.ComponentModel.CancelEventHandler(this.cmAnchors_Opening);
            // 
            // miAnchorAdd
            // 
            this.miAnchorAdd.Name = "miAnchorAdd";
            this.miAnchorAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miAnchorAdd.Size = new System.Drawing.Size(127, 22);
            this.miAnchorAdd.Text = "&Add";
            this.miAnchorAdd.Click += new System.EventHandler(this.miAnchorAdd_Click);
            // 
            // miAnchorDuplicate
            // 
            this.miAnchorDuplicate.Name = "miAnchorDuplicate";
            this.miAnchorDuplicate.Size = new System.Drawing.Size(127, 22);
            this.miAnchorDuplicate.Text = "Duplicate";
            this.miAnchorDuplicate.Click += new System.EventHandler(this.miAnchorDuplicate_Click);
            // 
            // miAnchorDelete
            // 
            this.miAnchorDelete.Name = "miAnchorDelete";
            this.miAnchorDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miAnchorDelete.Size = new System.Drawing.Size(127, 22);
            this.miAnchorDelete.Text = "&Delete";
            this.miAnchorDelete.Click += new System.EventHandler(this.miAnchorDelete_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(100, 292);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Width";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(100, 314);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Height";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 292);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "X:";
            // 
            // nudRectHeight
            // 
            this.nudRectHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudRectHeight.Enabled = false;
            this.nudRectHeight.Location = new System.Drawing.Point(139, 310);
            this.nudRectHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRectHeight.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudRectHeight.Name = "nudRectHeight";
            this.nudRectHeight.Size = new System.Drawing.Size(60, 20);
            this.nudRectHeight.TabIndex = 9;
            this.nudRectHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRectHeight.ValueChanged += new System.EventHandler(this.nudRectValue_ValueChanged);
            this.nudRectHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudRectValue_KeyDown);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 314);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Y:";
            // 
            // nudRectWidth
            // 
            this.nudRectWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudRectWidth.Enabled = false;
            this.nudRectWidth.Location = new System.Drawing.Point(139, 288);
            this.nudRectWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRectWidth.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudRectWidth.Name = "nudRectWidth";
            this.nudRectWidth.Size = new System.Drawing.Size(60, 20);
            this.nudRectWidth.TabIndex = 5;
            this.nudRectWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRectWidth.ValueChanged += new System.EventHandler(this.nudRectValue_ValueChanged);
            this.nudRectWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudRectValue_KeyDown);
            // 
            // nudRectY
            // 
            this.nudRectY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudRectY.Enabled = false;
            this.nudRectY.Location = new System.Drawing.Point(22, 310);
            this.nudRectY.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRectY.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudRectY.Name = "nudRectY";
            this.nudRectY.Size = new System.Drawing.Size(60, 20);
            this.nudRectY.TabIndex = 7;
            this.nudRectY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRectY.ValueChanged += new System.EventHandler(this.nudRectValue_ValueChanged);
            this.nudRectY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudRectValue_KeyDown);
            // 
            // nudRectX
            // 
            this.nudRectX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudRectX.Enabled = false;
            this.nudRectX.Location = new System.Drawing.Point(22, 288);
            this.nudRectX.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudRectX.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudRectX.Name = "nudRectX";
            this.nudRectX.Size = new System.Drawing.Size(60, 20);
            this.nudRectX.TabIndex = 3;
            this.nudRectX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRectX.ValueChanged += new System.EventHandler(this.nudRectValue_ValueChanged);
            this.nudRectX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudRectValue_KeyDown);
            // 
            // EditLabelsForm
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 332);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(500, 335);
            this.Name = "EditLabelsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Labels";
            this.Activated += new System.EventHandler(this.EditLabelsForm_Activated);
            this.Deactivate += new System.EventHandler(this.EditLabelsForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditLabelsForm_FormClosing);
            this.Load += new System.EventHandler(this.EditLabelsForm_Load);
            this.cmLabels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBorderOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackgroundOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForegroundOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextColor)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmAnchors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRectHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRectX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WhereAreWe.DBListView lvLabels;
        private System.Windows.Forms.ColumnHeader chX;
        private System.Windows.Forms.ColumnHeader chY;
        private System.Windows.Forms.ColumnHeader chText;
        private System.Windows.Forms.ContextMenuStrip cmLabels;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopy;
        private System.Windows.Forms.ToolStripMenuItem miCtxDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbTextColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbBackgroundColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbBorderColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem miCtxAdd;
        private System.Windows.Forms.ToolStripMenuItem miCtxUndo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudForegroundOpacity;
        private System.Windows.Forms.NumericUpDown nudBackgroundOpacity;
        private System.Windows.Forms.NumericUpDown nudBorderOpacity;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.ColumnHeader chAnchor;
        private System.Windows.Forms.ToolStripMenuItem miCtxDuplicate;
        private System.Windows.Forms.LinkLabel llAnchors;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvAnchors;
        private System.Windows.Forms.ColumnHeader chRectX;
        private System.Windows.Forms.ColumnHeader chRectY;
        private System.Windows.Forms.ColumnHeader chWidth;
        private System.Windows.Forms.ColumnHeader chHeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudRectHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudRectWidth;
        private System.Windows.Forms.NumericUpDown nudRectY;
        private System.Windows.Forms.NumericUpDown nudRectX;
        private System.Windows.Forms.ContextMenuStrip cmAnchors;
        private System.Windows.Forms.ToolStripMenuItem miAnchorAdd;
        private System.Windows.Forms.ToolStripMenuItem miAnchorDuplicate;
        private System.Windows.Forms.ToolStripMenuItem miAnchorDelete;
        private System.Windows.Forms.CheckBox cbShowAnchors;
    }
}