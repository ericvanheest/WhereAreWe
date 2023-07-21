namespace WhereAreWe
{
    partial class EditCartographyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCartographyForm));
            this.ofdDataFile = new System.Windows.Forms.OpenFileDialog();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rbClearCurrent = new System.Windows.Forms.RadioButton();
            this.rbFillAll = new System.Windows.Forms.RadioButton();
            this.rbFillCurrent = new System.Windows.Forms.RadioButton();
            this.rbClearAll = new System.Windows.Forms.RadioButton();
            this.labelFile2 = new System.Windows.Forms.Label();
            this.tbFile2 = new System.Windows.Forms.TextBox();
            this.labelFile1 = new System.Windows.Forms.Label();
            this.tbFile1 = new System.Windows.Forms.TextBox();
            this.labelWarning2 = new System.Windows.Forms.Label();
            this.labelWarning1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.btnBrowse1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(399, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(480, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 8);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rbClearCurrent);
            this.splitContainer1.Panel1.Controls.Add(this.rbFillAll);
            this.splitContainer1.Panel1.Controls.Add(this.rbFillCurrent);
            this.splitContainer1.Panel1.Controls.Add(this.rbClearAll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelFile2);
            this.splitContainer1.Panel2.Controls.Add(this.tbFile2);
            this.splitContainer1.Panel2.Controls.Add(this.labelFile1);
            this.splitContainer1.Panel2.Controls.Add(this.tbFile1);
            this.splitContainer1.Panel2.Controls.Add(this.labelWarning2);
            this.splitContainer1.Panel2.Controls.Add(this.labelWarning1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnBrowse2);
            this.splitContainer1.Panel2.Controls.Add(this.btnBrowse1);
            this.splitContainer1.Size = new System.Drawing.Size(542, 212);
            this.splitContainer1.SplitterDistance = 93;
            this.splitContainer1.TabIndex = 10;
            // 
            // rbClearCurrent
            // 
            this.rbClearCurrent.AutoSize = true;
            this.rbClearCurrent.Location = new System.Drawing.Point(2, 24);
            this.rbClearCurrent.Name = "rbClearCurrent";
            this.rbClearCurrent.Size = new System.Drawing.Size(253, 17);
            this.rbClearCurrent.TabIndex = 1;
            this.rbClearCurrent.Text = "Mark all squares as &unvisited on the current map";
            this.rbClearCurrent.UseVisualStyleBackColor = true;
            this.rbClearCurrent.CheckedChanged += new System.EventHandler(this.rbClearCurrent_CheckedChanged);
            // 
            // rbFillAll
            // 
            this.rbFillAll.AutoSize = true;
            this.rbFillAll.Location = new System.Drawing.Point(2, 47);
            this.rbFillAll.Name = "rbFillAll";
            this.rbFillAll.Size = new System.Drawing.Size(214, 17);
            this.rbFillAll.TabIndex = 2;
            this.rbFillAll.Text = "Mark &all squares as visited on ALL maps";
            this.rbFillAll.UseVisualStyleBackColor = true;
            this.rbFillAll.CheckedChanged += new System.EventHandler(this.rbFillAll_CheckedChanged);
            // 
            // rbFillCurrent
            // 
            this.rbFillCurrent.AutoSize = true;
            this.rbFillCurrent.Checked = true;
            this.rbFillCurrent.Location = new System.Drawing.Point(2, 1);
            this.rbFillCurrent.Name = "rbFillCurrent";
            this.rbFillCurrent.Size = new System.Drawing.Size(241, 17);
            this.rbFillCurrent.TabIndex = 0;
            this.rbFillCurrent.TabStop = true;
            this.rbFillCurrent.Text = "Mark all squares as &visited on the current map";
            this.rbFillCurrent.UseVisualStyleBackColor = true;
            this.rbFillCurrent.CheckedChanged += new System.EventHandler(this.rbFillCurrent_CheckedChanged);
            // 
            // rbClearAll
            // 
            this.rbClearAll.AutoSize = true;
            this.rbClearAll.Location = new System.Drawing.Point(2, 70);
            this.rbClearAll.Name = "rbClearAll";
            this.rbClearAll.Size = new System.Drawing.Size(226, 17);
            this.rbClearAll.TabIndex = 3;
            this.rbClearAll.Text = "&Mark all squares as unvisited on ALL maps";
            this.rbClearAll.UseVisualStyleBackColor = true;
            this.rbClearAll.CheckedChanged += new System.EventHandler(this.rbClearAll_CheckedChanged);
            // 
            // labelFile2
            // 
            this.labelFile2.AutoSize = true;
            this.labelFile2.Location = new System.Drawing.Point(3, 76);
            this.labelFile2.Name = "labelFile2";
            this.labelFile2.Size = new System.Drawing.Size(63, 13);
            this.labelFile2.TabIndex = 4;
            this.labelFile2.Text = "DARK.CUR";
            // 
            // tbFile2
            // 
            this.tbFile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile2.Enabled = false;
            this.tbFile2.Location = new System.Drawing.Point(71, 73);
            this.tbFile2.Name = "tbFile2";
            this.tbFile2.Size = new System.Drawing.Size(391, 20);
            this.tbFile2.TabIndex = 5;
            this.tbFile2.TextChanged += new System.EventHandler(this.tbCurrentFile_TextChanged);
            // 
            // labelFile1
            // 
            this.labelFile1.AutoSize = true;
            this.labelFile1.Location = new System.Drawing.Point(2, 31);
            this.labelFile1.Name = "labelFile1";
            this.labelFile1.Size = new System.Drawing.Size(62, 13);
            this.labelFile1.TabIndex = 1;
            this.labelFile1.Text = "XEEN.CUR";
            // 
            // tbFile1
            // 
            this.tbFile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile1.Enabled = false;
            this.tbFile1.Location = new System.Drawing.Point(70, 28);
            this.tbFile1.Name = "tbFile1";
            this.tbFile1.Size = new System.Drawing.Size(392, 20);
            this.tbFile1.TabIndex = 2;
            this.tbFile1.TextChanged += new System.EventHandler(this.tbCurrentFile_TextChanged);
            // 
            // labelWarning2
            // 
            this.labelWarning2.AutoSize = true;
            this.labelWarning2.ForeColor = System.Drawing.Color.Red;
            this.labelWarning2.Location = new System.Drawing.Point(68, 96);
            this.labelWarning2.Name = "labelWarning2";
            this.labelWarning2.Size = new System.Drawing.Size(137, 13);
            this.labelWarning2.TabIndex = 6;
            this.labelWarning2.Text = "Warning: File does not exist";
            this.labelWarning2.Visible = false;
            // 
            // labelWarning1
            // 
            this.labelWarning1.AutoSize = true;
            this.labelWarning1.ForeColor = System.Drawing.Color.Red;
            this.labelWarning1.Location = new System.Drawing.Point(67, 51);
            this.labelWarning1.Name = "labelWarning1";
            this.labelWarning1.Size = new System.Drawing.Size(137, 13);
            this.labelWarning1.TabIndex = 3;
            this.labelWarning1.Text = "Warning: File does not exist";
            this.labelWarning1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current data files (used for actions that affect all maps)";
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse2.Enabled = false;
            this.btnBrowse2.Location = new System.Drawing.Point(468, 73);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(72, 20);
            this.btnBrowse2.TabIndex = 2;
            this.btnBrowse2.Text = "&Browse";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // btnBrowse1
            // 
            this.btnBrowse1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse1.Enabled = false;
            this.btnBrowse1.Location = new System.Drawing.Point(467, 28);
            this.btnBrowse1.Name = "btnBrowse1";
            this.btnBrowse1.Size = new System.Drawing.Size(72, 20);
            this.btnBrowse1.TabIndex = 1;
            this.btnBrowse1.Text = "&Browse";
            this.btnBrowse1.UseVisualStyleBackColor = true;
            this.btnBrowse1.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // EditCartographyForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(567, 255);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(314, 168);
            this.Name = "EditCartographyForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Cartography Data";
            this.Load += new System.EventHandler(this.EditCartographyForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbClearCurrent;
        private System.Windows.Forms.RadioButton rbFillCurrent;
        private System.Windows.Forms.RadioButton rbClearAll;
        private System.Windows.Forms.RadioButton rbFillAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFile1;
        private System.Windows.Forms.Button btnBrowse1;
        private System.Windows.Forms.Label labelWarning1;
        private System.Windows.Forms.OpenFileDialog ofdDataFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelFile2;
        private System.Windows.Forms.Label labelFile1;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.Label labelWarning2;
        private System.Windows.Forms.TextBox tbFile2;
    }
}