namespace WhereAreWe
{
    partial class GameInfoListView
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
            this.cmGameInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miInfoEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmGameInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmGameInfo
            // 
            this.cmGameInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInfoEdit});
            this.cmGameInfo.Name = "cmGameInfo";
            this.cmGameInfo.ShowImageMargin = false;
            this.cmGameInfo.Size = new System.Drawing.Size(97, 26);
            this.cmGameInfo.Opening += new System.ComponentModel.CancelEventHandler(this.cmEdit_Opening);
            // 
            // miInfoEdit
            // 
            this.miInfoEdit.Name = "miInfoEdit";
            this.miInfoEdit.Size = new System.Drawing.Size(96, 22);
            this.miInfoEdit.Text = "&Edit Value";
            this.miInfoEdit.Click += new System.EventHandler(this.miActiveEdit_Click);
            // 
            // GameInfoListView
            // 
            this.Size = new System.Drawing.Size(196, 158);
            this.cmGameInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmGameInfo;
        private System.Windows.Forms.ToolStripMenuItem miInfoEdit;

    }
}
