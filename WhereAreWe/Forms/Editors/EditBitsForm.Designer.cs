namespace WhereAreWe
{
    partial class EditBitsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBitsForm));
            this.cmBits = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBitsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitsSetBeacon = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitsGotoMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitIndex0to7 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitIndex7to0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBitIndexDefaultForGame = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBytes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBytesCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miBytesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.llNone = new System.Windows.Forms.LinkLabel();
            this.llAll = new System.Windows.Forms.LinkLabel();
            this.lvBits = new WhereAreWe.DBListView();
            this.chBitIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chByteIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSetWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSetWhere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCheckedWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCheckedWhere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClearedWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClearedWhere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelBytes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.cmBits.SuspendLayout();
            this.cmBytes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmBits
            // 
            this.cmBits.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBitsCopy,
            this.miBitsSetBeacon,
            this.miBitsGotoMap,
            this.miBitIndex});
            this.cmBits.Name = "cmBits";
            this.cmBits.ShowCheckMargin = true;
            this.cmBits.ShowImageMargin = false;
            this.cmBits.Size = new System.Drawing.Size(133, 92);
            this.cmBits.Opening += new System.ComponentModel.CancelEventHandler(this.cmBits_Opening);
            // 
            // miBitsCopy
            // 
            this.miBitsCopy.Name = "miBitsCopy";
            this.miBitsCopy.Size = new System.Drawing.Size(132, 22);
            this.miBitsCopy.Text = "&Copy";
            this.miBitsCopy.Click += new System.EventHandler(this.miBitsCopy_Click);
            // 
            // miBitsSetBeacon
            // 
            this.miBitsSetBeacon.Name = "miBitsSetBeacon";
            this.miBitsSetBeacon.Size = new System.Drawing.Size(132, 22);
            this.miBitsSetBeacon.Text = "&Set beacon";
            this.miBitsSetBeacon.DropDownOpening += new System.EventHandler(this.miBitsSetBeacon_DropDownOpening);
            this.miBitsSetBeacon.Click += new System.EventHandler(this.miBitsSetBeacon_Click);
            // 
            // miBitsGotoMap
            // 
            this.miBitsGotoMap.Name = "miBitsGotoMap";
            this.miBitsGotoMap.Size = new System.Drawing.Size(132, 22);
            this.miBitsGotoMap.Text = "&Go to map";
            this.miBitsGotoMap.DropDownOpening += new System.EventHandler(this.miBitsGotoMap_DropDownOpening);
            this.miBitsGotoMap.Click += new System.EventHandler(this.miBitsGotoMap_Click);
            // 
            // miBitIndex
            // 
            this.miBitIndex.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBitIndex0to7,
            this.miBitIndex7to0,
            this.miBitIndexDefaultForGame});
            this.miBitIndex.Name = "miBitIndex";
            this.miBitIndex.Size = new System.Drawing.Size(132, 22);
            this.miBitIndex.Text = "&Bit index";
            // 
            // miBitIndex0to7
            // 
            this.miBitIndex0to7.Name = "miBitIndex0to7";
            this.miBitIndex0to7.Size = new System.Drawing.Size(180, 22);
            this.miBitIndex0to7.Text = "&0-7";
            this.miBitIndex0to7.Click += new System.EventHandler(this.miBitIndex0to7_Click);
            // 
            // miBitIndex7to0
            // 
            this.miBitIndex7to0.Name = "miBitIndex7to0";
            this.miBitIndex7to0.Size = new System.Drawing.Size(180, 22);
            this.miBitIndex7to0.Text = "&7-0";
            this.miBitIndex7to0.Click += new System.EventHandler(this.miBitIndex7to0_Click);
            // 
            // miBitIndexDefaultForGame
            // 
            this.miBitIndexDefaultForGame.Name = "miBitIndexDefaultForGame";
            this.miBitIndexDefaultForGame.Size = new System.Drawing.Size(180, 22);
            this.miBitIndexDefaultForGame.Text = "&Default For Game";
            this.miBitIndexDefaultForGame.Click += new System.EventHandler(this.miBitIndexDefaultForGame_Click);
            // 
            // cmBytes
            // 
            this.cmBytes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBytesCopy,
            this.miBytesEdit});
            this.cmBytes.Name = "cmForbiddenSpells";
            this.cmBytes.ShowImageMargin = false;
            this.cmBytes.Size = new System.Drawing.Size(78, 48);
            // 
            // miBytesCopy
            // 
            this.miBytesCopy.Name = "miBytesCopy";
            this.miBytesCopy.Size = new System.Drawing.Size(77, 22);
            this.miBytesCopy.Text = "&Copy";
            this.miBytesCopy.Click += new System.EventHandler(this.miBytesCopy_Click);
            // 
            // miBytesEdit
            // 
            this.miBytesEdit.Name = "miBytesEdit";
            this.miBytesEdit.Size = new System.Drawing.Size(77, 22);
            this.miBytesEdit.Text = "&Edit";
            this.miBytesEdit.Click += new System.EventHandler(this.miBytesEdit_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(567, 422);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(648, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // llNone
            // 
            this.llNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llNone.AutoSize = true;
            this.llNone.Location = new System.Drawing.Point(54, 427);
            this.llNone.Name = "llNone";
            this.llNone.Size = new System.Drawing.Size(44, 13);
            this.llNone.TabIndex = 4;
            this.llNone.TabStop = true;
            this.llNone.Text = "&Clear all";
            this.llNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNone_LinkClicked);
            // 
            // llAll
            // 
            this.llAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llAll.AutoSize = true;
            this.llAll.Location = new System.Drawing.Point(5, 427);
            this.llAll.Name = "llAll";
            this.llAll.Size = new System.Drawing.Size(36, 13);
            this.llAll.TabIndex = 3;
            this.llAll.TabStop = true;
            this.llAll.Text = "&Set all";
            this.llAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAll_LinkClicked);
            // 
            // lvBits
            // 
            this.lvBits.CheckBoxes = true;
            this.lvBits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBitIndex,
            this.chByteIndex,
            this.chSetWhen,
            this.chSetWhere,
            this.chCheckedWhen,
            this.chCheckedWhere,
            this.chClearedWhen,
            this.chClearedWhere});
            this.lvBits.ContextMenuStrip = this.cmBits;
            this.lvBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBits.FullRowSelect = true;
            this.lvBits.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBits.HideSelection = false;
            this.lvBits.Location = new System.Drawing.Point(0, 0);
            this.lvBits.Name = "lvBits";
            this.lvBits.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBits.Size = new System.Drawing.Size(715, 336);
            this.lvBits.TabIndex = 0;
            this.lvBits.UseCompatibleStateImageBehavior = false;
            this.lvBits.View = System.Windows.Forms.View.Details;
            this.lvBits.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvBits_ItemCheck);
            this.lvBits.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvBits_ItemChecked);
            this.lvBits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBits_KeyDown);
            // 
            // chBitIndex
            // 
            this.chBitIndex.Text = "Bit";
            this.chBitIndex.Width = 50;
            // 
            // chByteIndex
            // 
            this.chByteIndex.Text = "Byte";
            this.chByteIndex.Width = 40;
            // 
            // chSetWhen
            // 
            this.chSetWhen.Text = "Set When";
            this.chSetWhen.Width = 216;
            // 
            // chSetWhere
            // 
            this.chSetWhere.Text = "Set Where";
            this.chSetWhere.Width = 210;
            // 
            // chCheckedWhen
            // 
            this.chCheckedWhen.Text = "Checked When";
            this.chCheckedWhen.Width = 169;
            // 
            // chCheckedWhere
            // 
            this.chCheckedWhere.Text = "Checked Where";
            this.chCheckedWhere.Width = 174;
            // 
            // chClearedWhen
            // 
            this.chClearedWhen.Text = "Cleared When";
            this.chClearedWhen.Width = 120;
            // 
            // chClearedWhere
            // 
            this.chClearedWhere.Text = "Cleared Where";
            this.chClearedWhere.Width = 120;
            // 
            // labelBytes
            // 
            this.labelBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBytes.Location = new System.Drawing.Point(47, 376);
            this.labelBytes.Name = "labelBytes";
            this.labelBytes.Size = new System.Drawing.Size(676, 43);
            this.labelBytes.TabIndex = 2;
            this.labelBytes.Text = "00 00 00 00";
            this.labelBytes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelBytes_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bytes:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(8, 8);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvBits);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label52);
            this.splitContainer1.Panel2.Controls.Add(this.tbFind);
            this.splitContainer1.Panel2MinSize = 20;
            this.splitContainer1.Size = new System.Drawing.Size(715, 365);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 7;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 3);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 6;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 0);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(676, 20);
            this.tbFind.TabIndex = 5;
            // 
            // EditBitsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.llNone);
            this.Controls.Add(this.llAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelBytes);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(390, 252);
            this.Name = "EditBitsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Bits";
            this.Load += new System.EventHandler(this.EditBitsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditBitsForm_KeyDown);
            this.cmBits.ResumeLayout(false);
            this.cmBytes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBytes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private WhereAreWe.DBListView lvBits;
        private System.Windows.Forms.ColumnHeader chBitIndex;
        private System.Windows.Forms.ColumnHeader chByteIndex;
        private System.Windows.Forms.ContextMenuStrip cmBytes;
        private System.Windows.Forms.ToolStripMenuItem miBytesCopy;
        private System.Windows.Forms.ToolStripMenuItem miBytesEdit;
        private System.Windows.Forms.LinkLabel llAll;
        private System.Windows.Forms.LinkLabel llNone;
        private System.Windows.Forms.ColumnHeader chSetWhen;
        private System.Windows.Forms.ContextMenuStrip cmBits;
        private System.Windows.Forms.ToolStripMenuItem miBitsCopy;
        private System.Windows.Forms.ColumnHeader chSetWhere;
        private System.Windows.Forms.ColumnHeader chCheckedWhen;
        private System.Windows.Forms.ColumnHeader chCheckedWhere;
        private System.Windows.Forms.ColumnHeader chClearedWhen;
        private System.Windows.Forms.ColumnHeader chClearedWhere;
        private System.Windows.Forms.ToolStripMenuItem miBitsSetBeacon;
        private System.Windows.Forms.ToolStripMenuItem miBitsGotoMap;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.ToolStripMenuItem miBitIndex;
        private System.Windows.Forms.ToolStripMenuItem miBitIndex0to7;
        private System.Windows.Forms.ToolStripMenuItem miBitIndex7to0;
        private System.Windows.Forms.ToolStripMenuItem miBitIndexDefaultForGame;
    }
}