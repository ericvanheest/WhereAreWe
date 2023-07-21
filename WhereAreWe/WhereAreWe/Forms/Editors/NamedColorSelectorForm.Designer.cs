namespace WhereAreWe
{
    partial class NamedColorSelectorForm
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
            this.lvForeground = new System.Windows.Forms.ListView();
            this.chForeground = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelSample = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvBackground = new System.Windows.Forms.ListView();
            this.chBackground = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvForeground
            // 
            this.lvForeground.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chForeground});
            this.lvForeground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvForeground.FullRowSelect = true;
            this.lvForeground.HideSelection = false;
            this.lvForeground.Location = new System.Drawing.Point(0, 0);
            this.lvForeground.MultiSelect = false;
            this.lvForeground.Name = "lvForeground";
            this.lvForeground.Size = new System.Drawing.Size(233, 394);
            this.lvForeground.TabIndex = 0;
            this.lvForeground.UseCompatibleStateImageBehavior = false;
            this.lvForeground.View = System.Windows.Forms.View.Details;
            this.lvForeground.SelectedIndexChanged += new System.EventHandler(this.lvForeground_SelectedIndexChanged);
            this.lvForeground.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvForeground_MouseDoubleClick);
            // 
            // chForeground
            // 
            this.chForeground.Text = "Foreground Color";
            this.chForeground.Width = 209;
            // 
            // labelSample
            // 
            this.labelSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSample.Location = new System.Drawing.Point(79, 408);
            this.labelSample.Name = "labelSample";
            this.labelSample.Size = new System.Drawing.Size(232, 16);
            this.labelSample.TabIndex = 1;
            this.labelSample.Text = "Sample Text";
            this.labelSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(398, 407);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(317, 407);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(7, 7);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvForeground);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvBackground);
            this.splitContainer1.Size = new System.Drawing.Size(466, 394);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 4;
            // 
            // lvBackground
            // 
            this.lvBackground.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBackground});
            this.lvBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBackground.FullRowSelect = true;
            this.lvBackground.HideSelection = false;
            this.lvBackground.Location = new System.Drawing.Point(0, 0);
            this.lvBackground.MultiSelect = false;
            this.lvBackground.Name = "lvBackground";
            this.lvBackground.Size = new System.Drawing.Size(229, 394);
            this.lvBackground.TabIndex = 1;
            this.lvBackground.UseCompatibleStateImageBehavior = false;
            this.lvBackground.View = System.Windows.Forms.View.Details;
            this.lvBackground.SelectedIndexChanged += new System.EventHandler(this.lvBackground_SelectedIndexChanged);
            this.lvBackground.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBackground_MouseDoubleClick);
            // 
            // chBackground
            // 
            this.chBackground.Text = "Background Color";
            this.chBackground.Width = 214;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Double-click an item to set all of the opposing list\'s colors. ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sample Text:";
            // 
            // NamedColorSelectorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(485, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelSample);
            this.KeyPreview = true;
            this.Name = "NamedColorSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a foreground and background color";
            this.Load += new System.EventHandler(this.NamedColorSelectorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvForeground;
        private System.Windows.Forms.ColumnHeader chForeground;
        private System.Windows.Forms.Label labelSample;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvBackground;
        private System.Windows.Forms.ColumnHeader chBackground;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}