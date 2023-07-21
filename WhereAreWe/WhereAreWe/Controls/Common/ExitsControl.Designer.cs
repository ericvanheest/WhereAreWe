namespace WhereAreWe
{
    partial class ExitsControl
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
            this.lvLocations = new WhereAreWe.DBListView();
            this.chExit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmExit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miExitEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvLocations
            // 
            this.lvLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExit,
            this.chMap});
            this.lvLocations.ContextMenuStrip = this.cmExit;
            this.lvLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLocations.FullRowSelect = true;
            this.lvLocations.HideSelection = false;
            this.lvLocations.Location = new System.Drawing.Point(0, 0);
            this.lvLocations.MultiSelect = false;
            this.lvLocations.Name = "lvLocations";
            this.lvLocations.Size = new System.Drawing.Size(258, 105);
            this.lvLocations.TabIndex = 22;
            this.lvLocations.UseCompatibleStateImageBehavior = false;
            this.lvLocations.View = System.Windows.Forms.View.Details;
            this.lvLocations.SizeChanged += new System.EventHandler(this.lvLocations_SizeChanged);
            // 
            // chExit
            // 
            this.chExit.Text = "Exit";
            this.chExit.Width = 78;
            // 
            // chMap
            // 
            this.chMap.Text = "Location";
            this.chMap.Width = 154;
            // 
            // cmExit
            // 
            this.cmExit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExitEdit});
            this.cmExit.Name = "cmExit";
            this.cmExit.ShowImageMargin = false;
            this.cmExit.Size = new System.Drawing.Size(128, 48);
            this.cmExit.Opening += new System.ComponentModel.CancelEventHandler(this.cmExit_Opening);
            // 
            // miExitEdit
            // 
            this.miExitEdit.Name = "miExitEdit";
            this.miExitEdit.Size = new System.Drawing.Size(127, 22);
            this.miExitEdit.Text = "&Edit";
            this.miExitEdit.Click += new System.EventHandler(this.miExitEdit_Click);
            // 
            // ExitsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.lvLocations);
            this.Name = "ExitsControl";
            this.Size = new System.Drawing.Size(258, 105);
            this.cmExit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DBListView lvLocations;
        private System.Windows.Forms.ColumnHeader chExit;
        private System.Windows.Forms.ColumnHeader chMap;
        private System.Windows.Forms.ContextMenuStrip cmExit;
        private System.Windows.Forms.ToolStripMenuItem miExitEdit;
    }
}
