namespace WhereAreWe
{
    partial class MM1GameInformationControl
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
            this.chExit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActiveEffect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.glvMap = new WhereAreWe.GameInfoListView();
            this.chMapInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMapValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glvAffect = new WhereAreWe.GameInfoListView();
            this.chEffectInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAffectValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.glvMisc = new WhereAreWe.GameInfoListView();
            this.chMiscInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMiscValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chExit
            // 
            this.chExit.Text = "Exit";
            this.chExit.Width = 111;
            // 
            // chMap
            // 
            this.chMap.Text = "Map";
            this.chMap.Width = 204;
            // 
            // chActiveEffect
            // 
            this.chActiveEffect.Text = "Active Effect";
            this.chActiveEffect.Width = 132;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 39;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.glvMap);
            this.splitContainer2.Panel1MinSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(364, 293);
            this.splitContainer2.SplitterDistance = 96;
            this.splitContainer2.TabIndex = 24;
            // 
            // glvMap
            // 
            this.glvMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMapInfo,
            this.chMapValue});
            this.glvMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glvMap.FullRowSelect = true;
            this.glvMap.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.glvMap.Location = new System.Drawing.Point(0, 0);
            this.glvMap.Name = "glvMap";
            this.glvMap.Size = new System.Drawing.Size(364, 96);
            this.glvMap.TabIndex = 22;
            this.glvMap.UseCompatibleStateImageBehavior = false;
            this.glvMap.ValueEnabled = true;
            this.glvMap.View = System.Windows.Forms.View.Details;
            // 
            // chMapInfo
            // 
            this.chMapInfo.Text = "Map Info";
            this.chMapInfo.Width = 54;
            // 
            // chMapValue
            // 
            this.chMapValue.Text = "Value";
            this.chMapValue.Width = 300;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glvAffect);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.glvMisc);
            this.splitContainer1.Size = new System.Drawing.Size(364, 193);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 22;
            // 
            // glvAffect
            // 
            this.glvAffect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEffectInfo,
            this.chAffectValue});
            this.glvAffect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glvAffect.FullRowSelect = true;
            this.glvAffect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.glvAffect.Location = new System.Drawing.Point(0, 0);
            this.glvAffect.Name = "glvAffect";
            this.glvAffect.Size = new System.Drawing.Size(169, 193);
            this.glvAffect.TabIndex = 21;
            this.glvAffect.UseCompatibleStateImageBehavior = false;
            this.glvAffect.ValueEnabled = true;
            this.glvAffect.View = System.Windows.Forms.View.Details;
            // 
            // chEffectInfo
            // 
            this.chEffectInfo.Text = "Effect";
            this.chEffectInfo.Width = 40;
            // 
            // chAffectValue
            // 
            this.chAffectValue.Text = "Value";
            this.chAffectValue.Width = 119;
            // 
            // glvMisc
            // 
            this.glvMisc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMiscInfo,
            this.chMiscValue});
            this.glvMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glvMisc.FullRowSelect = true;
            this.glvMisc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.glvMisc.Location = new System.Drawing.Point(0, 0);
            this.glvMisc.Name = "glvMisc";
            this.glvMisc.Size = new System.Drawing.Size(191, 193);
            this.glvMisc.TabIndex = 21;
            this.glvMisc.UseCompatibleStateImageBehavior = false;
            this.glvMisc.ValueEnabled = true;
            this.glvMisc.View = System.Windows.Forms.View.Details;
            // 
            // chMiscInfo
            // 
            this.chMiscInfo.Text = "Misc Info";
            this.chMiscInfo.Width = 55;
            // 
            // chMiscValue
            // 
            this.chMiscValue.Text = "Value";
            this.chMiscValue.Width = 126;
            // 
            // MM1GameInformationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer2);
            this.Name = "MM1GameInformationControl";
            this.Size = new System.Drawing.Size(364, 293);
            this.Splitters = new int[0];
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chExit;
        private System.Windows.Forms.ColumnHeader chMap;
        private System.Windows.Forms.ColumnHeader chActiveEffect;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private GameInfoListView glvMap;
        private System.Windows.Forms.ColumnHeader chMapInfo;
        private System.Windows.Forms.ColumnHeader chMapValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GameInfoListView glvAffect;
        private System.Windows.Forms.ColumnHeader chEffectInfo;
        private System.Windows.Forms.ColumnHeader chAffectValue;
        private GameInfoListView glvMisc;
        private System.Windows.Forms.ColumnHeader chMiscInfo;
        private System.Windows.Forms.ColumnHeader chMiscValue;
    }
}
