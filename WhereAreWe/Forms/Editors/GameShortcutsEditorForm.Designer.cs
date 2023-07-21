namespace WhereAreWe
{
    partial class GameShortcutsEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameShortcutsEditorForm));
            this.lvPaths = new System.Windows.Forms.ListView();
            this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBrowseShortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ofdBrowseShortcut = new System.Windows.Forms.OpenFileDialog();
            this.cmListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPaths
            // 
            this.lvPaths.AllowDrop = true;
            this.lvPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPaths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFile,
            this.chGame});
            this.lvPaths.ContextMenuStrip = this.cmListView;
            this.lvPaths.FullRowSelect = true;
            this.lvPaths.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPaths.LabelEdit = true;
            this.lvPaths.Location = new System.Drawing.Point(1, 0);
            this.lvPaths.MultiSelect = false;
            this.lvPaths.Name = "lvPaths";
            this.lvPaths.Size = new System.Drawing.Size(740, 256);
            this.lvPaths.TabIndex = 0;
            this.lvPaths.UseCompatibleStateImageBehavior = false;
            this.lvPaths.View = System.Windows.Forms.View.Details;
            this.lvPaths.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvPaths_AfterLabelEdit);
            this.lvPaths.SelectedIndexChanged += new System.EventHandler(this.lvPaths_SelectedIndexChanged);
            this.lvPaths.DoubleClick += new System.EventHandler(this.lvPaths_DoubleClick);
            this.lvPaths.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPaths_KeyDown);
            // 
            // chFile
            // 
            this.chFile.Text = "File";
            this.chFile.Width = 488;
            // 
            // chGame
            // 
            this.chGame.Text = "Game";
            this.chGame.Width = 245;
            // 
            // cmListView
            // 
            this.cmListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBrowseShortcut});
            this.cmListView.Name = "cmListView";
            this.cmListView.ShowImageMargin = false;
            this.cmListView.Size = new System.Drawing.Size(145, 26);
            this.cmListView.Opening += new System.ComponentModel.CancelEventHandler(this.cmListView_Opening);
            // 
            // miBrowseShortcut
            // 
            this.miBrowseShortcut.Name = "miBrowseShortcut";
            this.miBrowseShortcut.Size = new System.Drawing.Size(144, 22);
            this.miBrowseShortcut.Text = "&Browse for shortcut";
            this.miBrowseShortcut.Click += new System.EventHandler(this.miBrowseShortcut_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(585, 262);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(666, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowse.Location = new System.Drawing.Point(1, 262);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ofdBrowseShortcut
            // 
            this.ofdBrowseShortcut.DefaultExt = "LNK";
            this.ofdBrowseShortcut.Filter = "Shortcut Files|*.lnk|Executable Files|*.exe|All Files|*.*";
            // 
            // GameShortcutsEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(742, 296);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvPaths);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "GameShortcutsEditorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Game Shortcuts";
            this.Load += new System.EventHandler(this.GameShortcutsEditorForm_Load);
            this.cmListView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPaths;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.ColumnHeader chGame;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip cmListView;
        private System.Windows.Forms.ToolStripMenuItem miBrowseShortcut;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofdBrowseShortcut;
    }
}