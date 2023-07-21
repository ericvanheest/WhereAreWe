namespace WhereAreWe
{
    partial class SelectImageRectangleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectImageRectangleForm));
            this.pbImage = new WhereAreWe.PictureBoxBlockMessages();
            this.cmImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSelected = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCursor = new System.Windows.Forms.Label();
            this.cbFullSize = new System.Windows.Forms.CheckBox();
            this.panelImageContainer = new WhereAreWe.PanelBlockMessages();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.cmImage.SuspendLayout();
            this.panelImageContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.ContextMenuStrip = this.cmImage;
            this.pbImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(698, 406);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.SizeChanged += new System.EventHandler(this.pbImage_SizeChanged);
            this.pbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseDown);
            this.pbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseMove);
            this.pbImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseUp);
            // 
            // cmImage
            // 
            this.cmImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxSelectAll});
            this.cmImage.Name = "cmImage";
            this.cmImage.Size = new System.Drawing.Size(117, 26);
            // 
            // miCtxSelectAll
            // 
            this.miCtxSelectAll.Name = "miCtxSelectAll";
            this.miCtxSelectAll.Size = new System.Drawing.Size(116, 22);
            this.miCtxSelectAll.Text = "&Select all";
            this.miCtxSelectAll.Click += new System.EventHandler(this.miCtxSelectAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(520, 411);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "&Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(601, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selected:";
            // 
            // labelSelected
            // 
            this.labelSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelected.AutoSize = true;
            this.labelSelected.Location = new System.Drawing.Point(224, 416);
            this.labelSelected.Name = "labelSelected";
            this.labelSelected.Size = new System.Drawing.Size(33, 13);
            this.labelSelected.TabIndex = 5;
            this.labelSelected.Text = "None";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cursor:";
            // 
            // labelCursor
            // 
            this.labelCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCursor.AutoSize = true;
            this.labelCursor.Location = new System.Drawing.Point(109, 416);
            this.labelCursor.Name = "labelCursor";
            this.labelCursor.Size = new System.Drawing.Size(25, 13);
            this.labelCursor.TabIndex = 3;
            this.labelCursor.Text = "0, 0";
            // 
            // cbFullSize
            // 
            this.cbFullSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullSize.AutoSize = true;
            this.cbFullSize.Location = new System.Drawing.Point(4, 414);
            this.cbFullSize.Name = "cbFullSize";
            this.cbFullSize.Size = new System.Drawing.Size(65, 17);
            this.cbFullSize.TabIndex = 1;
            this.cbFullSize.Text = "&Full Size";
            this.cbFullSize.UseVisualStyleBackColor = true;
            this.cbFullSize.CheckedChanged += new System.EventHandler(this.cbFullSize_CheckedChanged);
            // 
            // panelImageContainer
            // 
            this.panelImageContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImageContainer.AutoScroll = true;
            this.panelImageContainer.Controls.Add(this.pbImage);
            this.panelImageContainer.LastManualScroll = new System.Drawing.Point(0, 0);
            this.panelImageContainer.Location = new System.Drawing.Point(1, 2);
            this.panelImageContainer.Name = "panelImageContainer";
            this.panelImageContainer.Size = new System.Drawing.Size(698, 406);
            this.panelImageContainer.TabIndex = 0;
            this.panelImageContainer.SizeChanged += new System.EventHandler(this.panelImageContainer_SizeChanged);
            // 
            // SelectImageRectangleForm
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(699, 435);
            this.Controls.Add(this.panelImageContainer);
            this.Controls.Add(this.cbFullSize);
            this.Controls.Add(this.labelCursor);
            this.Controls.Add(this.labelSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(707, 462);
            this.Name = "SelectImageRectangleForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a portion of the image to use";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectImageRectangleForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectImageRectangleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.cmImage.ResumeLayout(false);
            this.panelImageContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBoxBlockMessages pbImage;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSelected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCursor;
        private System.Windows.Forms.ContextMenuStrip cmImage;
        private System.Windows.Forms.ToolStripMenuItem miCtxSelectAll;
        private System.Windows.Forms.CheckBox cbFullSize;
        private PanelBlockMessages panelImageContainer;
    }
}