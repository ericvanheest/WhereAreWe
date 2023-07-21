namespace WhereAreWe
{
    partial class TreasurePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbTreasure = new System.Windows.Forms.GroupBox();
            this.labelContents = new System.Windows.Forms.Label();
            this.labelTreasureHeader = new System.Windows.Forms.Label();
            this.timerUpdateSplitter = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbTreasure.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.gbTreasure);
            this.splitContainer1.Panel1.Controls.Add(this.labelTreasureHeader);
            this.splitContainer1.Size = new System.Drawing.Size(280, 237);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // gbTreasure
            // 
            this.gbTreasure.Controls.Add(this.labelContents);
            this.gbTreasure.Location = new System.Drawing.Point(4, 21);
            this.gbTreasure.Name = "gbTreasure";
            this.gbTreasure.Size = new System.Drawing.Size(52, 92);
            this.gbTreasure.TabIndex = 1;
            this.gbTreasure.TabStop = false;
            this.gbTreasure.Text = "Treasure";
            // 
            // labelContents
            // 
            this.labelContents.AutoSize = true;
            this.labelContents.Location = new System.Drawing.Point(9, 16);
            this.labelContents.Name = "labelContents";
            this.labelContents.Size = new System.Drawing.Size(34, 65);
            this.labelContents.TabIndex = 0;
            this.labelContents.Text = "Gems\r\nGold\r\nItem1\r\nItem2\r\nItem3";
            this.labelContents.UseMnemonic = false;
            this.labelContents.SizeChanged += new System.EventHandler(this.labelContents_SizeChanged);
            // 
            // labelTreasureHeader
            // 
            this.labelTreasureHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTreasureHeader.Location = new System.Drawing.Point(1, 0);
            this.labelTreasureHeader.Name = "labelTreasureHeader";
            this.labelTreasureHeader.Size = new System.Drawing.Size(279, 18);
            this.labelTreasureHeader.TabIndex = 0;
            this.labelTreasureHeader.Text = "Treasure in the area!  Search!";
            this.labelTreasureHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerUpdateSplitter
            // 
            this.timerUpdateSplitter.Interval = 30;
            this.timerUpdateSplitter.Tick += new System.EventHandler(this.timerUpdateSplitter_Tick);
            // 
            // TreasurePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TreasurePanel";
            this.Size = new System.Drawing.Size(280, 237);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbTreasure.ResumeLayout(false);
            this.gbTreasure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTreasureHeader;
        private System.Windows.Forms.Label labelContents;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbTreasure;
        private System.Windows.Forms.Timer timerUpdateSplitter;
    }
}
