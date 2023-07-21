namespace WhereAreWe
{
    partial class MM2GameInformationControl
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
            this.cmEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miEditEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.chAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmDepth = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDepthTown = new System.Windows.Forms.ToolStripMenuItem();
            this.miDepthSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.miDepth10Under = new System.Windows.Forms.ToolStripMenuItem();
            this.miDepth20Under = new System.Windows.Forms.ToolStripMenuItem();
            this.miDepth30Under = new System.Windows.Forms.ToolStripMenuItem();
            this.miDepth40Under = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBoolean = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glvAffect = new WhereAreWe.GameInfoListView();
            this.chEffectInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAffectValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.glvMisc = new WhereAreWe.GameInfoListView();
            this.chMiscInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMiscValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.glvMap = new WhereAreWe.GameInfoListView();
            this.chMapInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMapValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmEdit.SuspendLayout();
            this.cmDepth.SuspendLayout();
            this.cmBoolean.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmEdit
            // 
            this.cmEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditEdit});
            this.cmEdit.Name = "cmForbiddenSpells";
            this.cmEdit.ShowImageMargin = false;
            this.cmEdit.Size = new System.Drawing.Size(68, 26);
            // 
            // miEditEdit
            // 
            this.miEditEdit.Name = "miEditEdit";
            this.miEditEdit.Size = new System.Drawing.Size(67, 22);
            this.miEditEdit.Text = "&Edit";
            // 
            // chAction
            // 
            this.chAction.Text = "Action";
            this.chAction.Width = 111;
            // 
            // chLocation
            // 
            this.chLocation.Text = "Location";
            this.chLocation.Width = 306;
            // 
            // cmDepth
            // 
            this.cmDepth.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDepthTown,
            this.miDepthSurface,
            this.miDepth10Under,
            this.miDepth20Under,
            this.miDepth30Under,
            this.miDepth40Under});
            this.cmDepth.Name = "cmForbiddenSpells";
            this.cmDepth.ShowImageMargin = false;
            this.cmDepth.Size = new System.Drawing.Size(96, 136);
            // 
            // miDepthTown
            // 
            this.miDepthTown.Name = "miDepthTown";
            this.miDepthTown.Size = new System.Drawing.Size(95, 22);
            this.miDepthTown.Text = "&Town";
            // 
            // miDepthSurface
            // 
            this.miDepthSurface.Name = "miDepthSurface";
            this.miDepthSurface.Size = new System.Drawing.Size(95, 22);
            this.miDepthSurface.Text = "&Surface";
            // 
            // miDepth10Under
            // 
            this.miDepth10Under.Name = "miDepth10Under";
            this.miDepth10Under.Size = new System.Drawing.Size(95, 22);
            this.miDepth10Under.Text = "&10\' Under";
            // 
            // miDepth20Under
            // 
            this.miDepth20Under.Name = "miDepth20Under";
            this.miDepth20Under.Size = new System.Drawing.Size(95, 22);
            this.miDepth20Under.Text = "&20\' Under";
            // 
            // miDepth30Under
            // 
            this.miDepth30Under.Name = "miDepth30Under";
            this.miDepth30Under.Size = new System.Drawing.Size(95, 22);
            this.miDepth30Under.Text = "&30\' Under";
            // 
            // miDepth40Under
            // 
            this.miDepth40Under.Name = "miDepth40Under";
            this.miDepth40Under.Size = new System.Drawing.Size(95, 22);
            this.miDepth40Under.Text = "&40\' Under";
            // 
            // cmBoolean
            // 
            this.cmBoolean.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToggle});
            this.cmBoolean.Name = "cmForbiddenSpells";
            this.cmBoolean.ShowImageMargin = false;
            this.cmBoolean.Size = new System.Drawing.Size(82, 26);
            // 
            // miToggle
            // 
            this.miToggle.Name = "miToggle";
            this.miToggle.Size = new System.Drawing.Size(81, 22);
            this.miToggle.Text = "&Toggle";
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
            this.splitContainer1.Size = new System.Drawing.Size(474, 408);
            this.splitContainer1.SplitterDistance = 238;
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
            this.glvAffect.Size = new System.Drawing.Size(238, 408);
            this.glvAffect.TabIndex = 21;
            this.glvAffect.UseCompatibleStateImageBehavior = false;
            this.glvAffect.ValueEnabled = true;
            this.glvAffect.View = System.Windows.Forms.View.Details;
            // 
            // chEffectInfo
            // 
            this.chEffectInfo.Text = "Effect";
            this.chEffectInfo.Width = 55;
            // 
            // chAffectValue
            // 
            this.chAffectValue.Text = "Value";
            this.chAffectValue.Width = 161;
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
            this.glvMisc.Size = new System.Drawing.Size(232, 408);
            this.glvMisc.TabIndex = 21;
            this.glvMisc.UseCompatibleStateImageBehavior = false;
            this.glvMisc.ValueEnabled = true;
            this.glvMisc.View = System.Windows.Forms.View.Details;
            // 
            // chMiscInfo
            // 
            this.chMiscInfo.Text = "Misc Info";
            this.chMiscInfo.Width = 56;
            // 
            // chMiscValue
            // 
            this.chMiscValue.Text = "Value";
            this.chMiscValue.Width = 154;
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
            this.splitContainer2.Size = new System.Drawing.Size(474, 508);
            this.splitContainer2.SplitterDistance = 96;
            this.splitContainer2.TabIndex = 23;
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
            this.glvMap.Size = new System.Drawing.Size(474, 96);
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
            this.chMapValue.Width = 398;
            // 
            // MM2GameInformationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "MM2GameInformationControl";
            this.Size = new System.Drawing.Size(474, 508);
            this.Splitters = new int[0];
            this.cmEdit.ResumeLayout(false);
            this.cmDepth.ResumeLayout(false);
            this.cmBoolean.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chAction;
        private System.Windows.Forms.ColumnHeader chLocation;
        private System.Windows.Forms.ContextMenuStrip cmDepth;
        private System.Windows.Forms.ToolStripMenuItem miDepthTown;
        private System.Windows.Forms.ToolStripMenuItem miDepthSurface;
        private System.Windows.Forms.ToolStripMenuItem miDepth10Under;
        private System.Windows.Forms.ToolStripMenuItem miDepth20Under;
        private System.Windows.Forms.ToolStripMenuItem miDepth30Under;
        private System.Windows.Forms.ToolStripMenuItem miDepth40Under;
        private System.Windows.Forms.ContextMenuStrip cmBoolean;
        private System.Windows.Forms.ToolStripMenuItem miToggle;
        private System.Windows.Forms.ContextMenuStrip cmEdit;
        private System.Windows.Forms.ToolStripMenuItem miEditEdit;
        private GameInfoListView glvAffect;
        private System.Windows.Forms.ColumnHeader chEffectInfo;
        private System.Windows.Forms.ColumnHeader chAffectValue;
        private GameInfoListView glvMisc;
        private System.Windows.Forms.ColumnHeader chMiscInfo;
        private System.Windows.Forms.ColumnHeader chMiscValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private GameInfoListView glvMap;
        private System.Windows.Forms.ColumnHeader chMapInfo;
        private System.Windows.Forms.ColumnHeader chMapValue;
    }
}
