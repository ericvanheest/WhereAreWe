namespace WhereAreWe
{
    partial class EditBytesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBytesForm));
            this.cmBytes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBytesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miBytesSet0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBytesSet1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBytesSet255 = new System.Windows.Forms.ToolStripMenuItem();
            this.miBytesHex = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelSpacing = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.scBytesFind = new System.Windows.Forms.SplitContainer();
            this.lvBytes = new WhereAreWe.DBListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelInvalid1 = new System.Windows.Forms.Label();
            this.tbNew = new System.Windows.Forms.TextBox();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelCountText = new System.Windows.Forms.Label();
            this.cmBytes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBytesFind)).BeginInit();
            this.scBytesFind.Panel1.SuspendLayout();
            this.scBytesFind.Panel2.SuspendLayout();
            this.scBytesFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmBytes
            // 
            this.cmBytes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBytesEdit,
            this.miBytesSet0,
            this.miBytesSet1,
            this.miBytesSet255,
            this.miBytesHex});
            this.cmBytes.Name = "cmBytes";
            this.cmBytes.Size = new System.Drawing.Size(171, 114);
            this.cmBytes.Opening += new System.ComponentModel.CancelEventHandler(this.cmBytes_Opening);
            // 
            // miBytesEdit
            // 
            this.miBytesEdit.Name = "miBytesEdit";
            this.miBytesEdit.Size = new System.Drawing.Size(170, 22);
            this.miBytesEdit.Text = "&Edit";
            this.miBytesEdit.Click += new System.EventHandler(this.miBytesEdit_Click);
            // 
            // miBytesSet0
            // 
            this.miBytesSet0.Name = "miBytesSet0";
            this.miBytesSet0.Size = new System.Drawing.Size(170, 22);
            this.miBytesSet0.Text = "Set to &0";
            this.miBytesSet0.Click += new System.EventHandler(this.miBytesSet0_Click);
            // 
            // miBytesSet1
            // 
            this.miBytesSet1.Name = "miBytesSet1";
            this.miBytesSet1.Size = new System.Drawing.Size(170, 22);
            this.miBytesSet1.Text = "Set to &1";
            this.miBytesSet1.Click += new System.EventHandler(this.miBytesSet1_Click);
            // 
            // miBytesSet255
            // 
            this.miBytesSet255.Name = "miBytesSet255";
            this.miBytesSet255.Size = new System.Drawing.Size(170, 22);
            this.miBytesSet255.Text = "Set to &255";
            this.miBytesSet255.Click += new System.EventHandler(this.miBytesSet255_Click);
            // 
            // miBytesHex
            // 
            this.miBytesHex.Name = "miBytesHex";
            this.miBytesHex.Size = new System.Drawing.Size(170, 22);
            this.miBytesHex.Text = "&Hexadecimal display";
            this.miBytesHex.Click += new System.EventHandler(this.miBytesHex_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(282, 341);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(363, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelSpacing
            // 
            this.panelSpacing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpacing.Location = new System.Drawing.Point(0, 335);
            this.panelSpacing.Name = "panelSpacing";
            this.panelSpacing.Size = new System.Drawing.Size(10, 38);
            this.panelSpacing.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scBytesFind);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.labelInvalid1);
            this.splitContainer1.Panel2.Controls.Add(this.tbNew);
            this.splitContainer1.Panel2.Controls.Add(this.labelCurrent);
            this.splitContainer1.Size = new System.Drawing.Size(444, 335);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.TabIndex = 13;
            // 
            // scBytesFind
            // 
            this.scBytesFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBytesFind.Location = new System.Drawing.Point(0, 0);
            this.scBytesFind.Name = "scBytesFind";
            this.scBytesFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBytesFind.Panel1
            // 
            this.scBytesFind.Panel1.Controls.Add(this.lvBytes);
            // 
            // scBytesFind.Panel2
            // 
            this.scBytesFind.Panel2.Controls.Add(this.label52);
            this.scBytesFind.Panel2.Controls.Add(this.tbFind);
            this.scBytesFind.Size = new System.Drawing.Size(444, 264);
            this.scBytesFind.SplitterDistance = 235;
            this.scBytesFind.TabIndex = 1;
            // 
            // lvBytes
            // 
            this.lvBytes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chValue,
            this.chDescription});
            this.lvBytes.ContextMenuStrip = this.cmBytes;
            this.lvBytes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBytes.FullRowSelect = true;
            this.lvBytes.HideSelection = false;
            this.lvBytes.Location = new System.Drawing.Point(0, 0);
            this.lvBytes.Name = "lvBytes";
            this.lvBytes.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBytes.Size = new System.Drawing.Size(444, 235);
            this.lvBytes.TabIndex = 0;
            this.lvBytes.UseCompatibleStateImageBehavior = false;
            this.lvBytes.View = System.Windows.Forms.View.Details;
            this.lvBytes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvBytes_ColumnClick);
            this.lvBytes.DoubleClick += new System.EventHandler(this.lvBytes_DoubleClick);
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 28;
            // 
            // chValue
            // 
            this.chValue.Text = "Val";
            this.chValue.Width = 38;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 352;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 6);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 8;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 3);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(405, 20);
            this.tbFind.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "New:";
            // 
            // labelInvalid1
            // 
            this.labelInvalid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInvalid1.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid1.Location = new System.Drawing.Point(56, 48);
            this.labelInvalid1.Name = "labelInvalid1";
            this.labelInvalid1.Size = new System.Drawing.Size(382, 18);
            this.labelInvalid1.TabIndex = 4;
            this.labelInvalid1.Text = "Invalid Value";
            this.labelInvalid1.Visible = false;
            // 
            // tbNew
            // 
            this.tbNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNew.Location = new System.Drawing.Point(56, 23);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(376, 20);
            this.tbNew.TabIndex = 3;
            this.tbNew.TextChanged += new System.EventHandler(this.tbNew_TextChanged);
            // 
            // labelCurrent
            // 
            this.labelCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrent.Location = new System.Drawing.Point(56, 5);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(376, 15);
            this.labelCurrent.TabIndex = 1;
            this.labelCurrent.Text = "00 00 00 00";
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(3, 347);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(38, 13);
            this.labelCount.TabIndex = 2;
            this.labelCount.Text = "Count:";
            // 
            // labelCountText
            // 
            this.labelCountText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCountText.AutoSize = true;
            this.labelCountText.Location = new System.Drawing.Point(56, 347);
            this.labelCountText.Name = "labelCountText";
            this.labelCountText.Size = new System.Drawing.Size(13, 13);
            this.labelCountText.TabIndex = 1;
            this.labelCountText.Text = "0";
            // 
            // EditBytesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 369);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelCountText);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelSpacing);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(318, 127);
            this.Name = "EditBytesForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Bytes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditBytesForm_FormClosing);
            this.Load += new System.EventHandler(this.EditBytesForm_Load);
            this.cmBytes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.scBytesFind.Panel1.ResumeLayout(false);
            this.scBytesFind.Panel2.ResumeLayout(false);
            this.scBytesFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBytesFind)).EndInit();
            this.scBytesFind.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNew;
        private System.Windows.Forms.Label labelInvalid1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private WhereAreWe.DBListView lvBytes;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ContextMenuStrip cmBytes;
        private System.Windows.Forms.ToolStripMenuItem miBytesHex;
        private System.Windows.Forms.ToolStripMenuItem miBytesEdit;
        private System.Windows.Forms.ToolStripMenuItem miBytesSet0;
        private System.Windows.Forms.ToolStripMenuItem miBytesSet1;
        private System.Windows.Forms.ToolStripMenuItem miBytesSet255;
        private System.Windows.Forms.Panel panelSpacing;
        private System.Windows.Forms.SplitContainer scBytesFind;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelCountText;
    }
}