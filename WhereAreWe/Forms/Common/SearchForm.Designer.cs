namespace WhereAreWe
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.cmNotes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNotesGoToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesFindPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesSetSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesSetBeacon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miNotesRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesCopyLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesCopyAllLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesHideUnvisited = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvNotes = new WhereAreWe.DBListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMapTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNoteSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNoteText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbNoteText = new System.Windows.Forms.RichTextBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindPrevious = new System.Windows.Forms.Button();
            this.cmNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmNotes
            // 
            this.cmNotes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNotesGoToMap,
            this.miNotesFindNext,
            this.miNotesFindPrevious,
            this.miNotesSetSurface,
            this.miNotesSetBeacon,
            this.toolStripSeparator1,
            this.miNotesRefresh,
            this.miNotesCopy,
            this.miNotesCopyLocation,
            this.miNotesCopyAllLocations,
            this.miNotesHideUnvisited});
            this.cmNotes.Name = "cmNotes";
            this.cmNotes.ShowCheckMargin = true;
            this.cmNotes.ShowImageMargin = false;
            this.cmNotes.Size = new System.Drawing.Size(238, 252);
            this.cmNotes.Opening += new System.ComponentModel.CancelEventHandler(this.cmNotes_Opening);
            // 
            // miNotesGoToMap
            // 
            this.miNotesGoToMap.Name = "miNotesGoToMap";
            this.miNotesGoToMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.miNotesGoToMap.Size = new System.Drawing.Size(237, 22);
            this.miNotesGoToMap.Text = "&Go to this map";
            this.miNotesGoToMap.Click += new System.EventHandler(this.miNotesGoToMap_Click);
            // 
            // miNotesFindNext
            // 
            this.miNotesFindNext.Name = "miNotesFindNext";
            this.miNotesFindNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.miNotesFindNext.Size = new System.Drawing.Size(237, 22);
            this.miNotesFindNext.Text = "&Find next";
            this.miNotesFindNext.Click += new System.EventHandler(this.miNotesFindNext_Click);
            // 
            // miNotesFindPrevious
            // 
            this.miNotesFindPrevious.Name = "miNotesFindPrevious";
            this.miNotesFindPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.miNotesFindPrevious.Size = new System.Drawing.Size(237, 22);
            this.miNotesFindPrevious.Text = "Find &previous";
            this.miNotesFindPrevious.Click += new System.EventHandler(this.miNotesFindPrevious_Click);
            // 
            // miNotesSetSurface
            // 
            this.miNotesSetSurface.Name = "miNotesSetSurface";
            this.miNotesSetSurface.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.miNotesSetSurface.Size = new System.Drawing.Size(237, 22);
            this.miNotesSetSurface.Text = "Set &Surface location here";
            this.miNotesSetSurface.Click += new System.EventHandler(this.miNotesSetSurface_Click);
            // 
            // miNotesSetBeacon
            // 
            this.miNotesSetBeacon.Name = "miNotesSetBeacon";
            this.miNotesSetBeacon.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.miNotesSetBeacon.Size = new System.Drawing.Size(237, 22);
            this.miNotesSetBeacon.Text = "Set Lloyd\'s &Beacon here";
            this.miNotesSetBeacon.Click += new System.EventHandler(this.miNotesSetBeacon_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(234, 6);
            // 
            // miNotesRefresh
            // 
            this.miNotesRefresh.Name = "miNotesRefresh";
            this.miNotesRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miNotesRefresh.Size = new System.Drawing.Size(237, 22);
            this.miNotesRefresh.Text = "&Refresh";
            this.miNotesRefresh.Click += new System.EventHandler(this.miNotesRefresh_Click);
            // 
            // miNotesCopy
            // 
            this.miNotesCopy.Name = "miNotesCopy";
            this.miNotesCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miNotesCopy.Size = new System.Drawing.Size(237, 22);
            this.miNotesCopy.Text = "&Copy entire note";
            this.miNotesCopy.Click += new System.EventHandler(this.miNotesCopy_Click);
            // 
            // miNotesCopyLocation
            // 
            this.miNotesCopyLocation.Name = "miNotesCopyLocation";
            this.miNotesCopyLocation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.miNotesCopyLocation.Size = new System.Drawing.Size(237, 22);
            this.miNotesCopyLocation.Text = "Copy &location";
            this.miNotesCopyLocation.Click += new System.EventHandler(this.miNotesCopyLocation_Click);
            // 
            // miNotesCopyAllLocations
            // 
            this.miNotesCopyAllLocations.Name = "miNotesCopyAllLocations";
            this.miNotesCopyAllLocations.Size = new System.Drawing.Size(237, 22);
            this.miNotesCopyAllLocations.Text = "Copy &all matching locations";
            this.miNotesCopyAllLocations.Click += new System.EventHandler(this.miNotesCopyAllLocations_Click);
            // 
            // miNotesHideUnvisited
            // 
            this.miNotesHideUnvisited.Checked = true;
            this.miNotesHideUnvisited.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miNotesHideUnvisited.Name = "miNotesHideUnvisited";
            this.miNotesHideUnvisited.Size = new System.Drawing.Size(237, 22);
            this.miNotesHideUnvisited.Text = "&Hide notes from unvisited squares";
            this.miNotesHideUnvisited.Click += new System.EventHandler(this.miNotesHideUnvisited_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(415, 389);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 20);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "&Next";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(526, 387);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 8);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvNotes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbNoteText);
            this.splitContainer1.Size = new System.Drawing.Size(595, 374);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 9;
            // 
            // lvNotes
            // 
            this.lvNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chMapTitle,
            this.chLocation,
            this.chNoteSymbol,
            this.chNoteText});
            this.lvNotes.ContextMenuStrip = this.cmNotes;
            this.lvNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNotes.FullRowSelect = true;
            this.lvNotes.HideSelection = false;
            this.lvNotes.Location = new System.Drawing.Point(0, 0);
            this.lvNotes.MultiSelect = false;
            this.lvNotes.Name = "lvNotes";
            this.lvNotes.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvNotes.Size = new System.Drawing.Size(595, 278);
            this.lvNotes.TabIndex = 0;
            this.lvNotes.UseCompatibleStateImageBehavior = false;
            this.lvNotes.View = System.Windows.Forms.View.Details;
            this.lvNotes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvNotes_ColumnClick);
            this.lvNotes.SelectedIndexChanged += new System.EventHandler(this.lvNotes_SelectedIndexChanged);
            this.lvNotes.DoubleClick += new System.EventHandler(this.lvNotes_DoubleClick);
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 28;
            // 
            // chMapTitle
            // 
            this.chMapTitle.Text = "Map Title";
            this.chMapTitle.Width = 163;
            // 
            // chLocation
            // 
            this.chLocation.Text = "Loc";
            this.chLocation.Width = 39;
            // 
            // chNoteSymbol
            // 
            this.chNoteSymbol.Text = "Sym";
            this.chNoteSymbol.Width = 32;
            // 
            // chNoteText
            // 
            this.chNoteText.Text = "Note Text";
            this.chNoteText.Width = 755;
            // 
            // tbNoteText
            // 
            this.tbNoteText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoteText.Location = new System.Drawing.Point(0, 0);
            this.tbNoteText.Multiline = true;
            this.tbNoteText.Name = "tbNoteText";
            this.tbNoteText.Size = new System.Drawing.Size(595, 92);
            this.tbNoteText.TabIndex = 0;
            this.tbNoteText.Leave += new System.EventHandler(this.tbNoteText_Leave);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(56, 389);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(272, 20);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search:";
            // 
            // btnFindPrevious
            // 
            this.btnFindPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindPrevious.Location = new System.Drawing.Point(334, 389);
            this.btnFindPrevious.Name = "btnFindPrevious";
            this.btnFindPrevious.Size = new System.Drawing.Size(75, 20);
            this.btnFindPrevious.TabIndex = 3;
            this.btnFindPrevious.Text = "&Previous";
            this.btnFindPrevious.UseVisualStyleBackColor = true;
            this.btnFindPrevious.Click += new System.EventHandler(this.btnFindPrevious_Click);
            // 
            // SearchForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(606, 417);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFindPrevious);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(281, 155);
            this.Name = "SearchForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search for note text / Go to a map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StringsViewForm_FormClosing);
            this.Load += new System.EventHandler(this.StringsViewForm_Load);
            this.cmNotes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnSearch;
        private WhereAreWe.DBListView lvNotes;
        private System.Windows.Forms.ContextMenuStrip cmNotes;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chMapTitle;
        private System.Windows.Forms.ColumnHeader chNoteSymbol;
        private System.Windows.Forms.ColumnHeader chNoteText;
        private System.Windows.Forms.ToolStripMenuItem miNotesGoToMap;
        private System.Windows.Forms.ToolStripMenuItem miNotesFindNext;
        private System.Windows.Forms.RichTextBox tbNoteText;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader chLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miNotesRefresh;
        private System.Windows.Forms.ToolStripMenuItem miNotesFindPrevious;
        private System.Windows.Forms.Button btnFindPrevious;
        private System.Windows.Forms.ToolStripMenuItem miNotesSetBeacon;
        private System.Windows.Forms.ToolStripMenuItem miNotesCopy;
        private System.Windows.Forms.ToolStripMenuItem miNotesHideUnvisited;
        private System.Windows.Forms.ToolStripMenuItem miNotesSetSurface;
        private System.Windows.Forms.ToolStripMenuItem miNotesCopyLocation;
        private System.Windows.Forms.ToolStripMenuItem miNotesCopyAllLocations;
    }
}