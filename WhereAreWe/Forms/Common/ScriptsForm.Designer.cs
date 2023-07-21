namespace WhereAreWe
{
    partial class ScriptsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptsForm));
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateMemory = new System.Windows.Forms.Timer(this.components);
            this.cmAllScripts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAllScriptsTeleport = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllScriptsSetNote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAllScriptsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllScriptsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miAllScriptsGame = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllScriptsInternal = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllScriptsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllScriptsReinterpret = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSelectedScript = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miScriptsGoToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miSelectedScriptTeleport = new System.Windows.Forms.ToolStripMenuItem();
            this.miScriptsSetBeacon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miScriptsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miScriptCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miScriptEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miScriptReinterpret = new System.Windows.Forms.ToolStripMenuItem();
            this.miScriptCopyCommandOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.scScriptsFind = new System.Windows.Forms.SplitContainer();
            this.lvScripts = new WhereAreWe.DBListView();
            this.chScriptIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAuto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNumBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSummary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cbHideAddresses = new System.Windows.Forms.CheckBox();
            this.cbHideOffMapScripts = new System.Windows.Forms.CheckBox();
            this.cbHideUnreachable = new System.Windows.Forms.CheckBox();
            this.cbHideEmptyScripts = new System.Windows.Forms.CheckBox();
            this.cbHideEmptyLines = new System.Windows.Forms.CheckBox();
            this.cbHideInlineSubscripts = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFindScripts = new System.Windows.Forms.TextBox();
            this.scSelectedFind = new System.Windows.Forms.SplitContainer();
            this.lvSelectedScript = new WhereAreWe.DBListView();
            this.chLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHeaderBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCommandBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFindSelected = new System.Windows.Forms.TextBox();
            this.cmAllScripts.SuspendLayout();
            this.cmSelectedScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scScriptsFind)).BeginInit();
            this.scScriptsFind.Panel1.SuspendLayout();
            this.scScriptsFind.Panel2.SuspendLayout();
            this.scScriptsFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSelectedFind)).BeginInit();
            this.scSelectedFind.Panel1.SuspendLayout();
            this.scSelectedFind.Panel2.SuspendLayout();
            this.scSelectedFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // miEditPath
            // 
            this.miEditPath.Name = "miEditPath";
            this.miEditPath.Size = new System.Drawing.Size(32, 19);
            // 
            // timerUpdateMemory
            // 
            this.timerUpdateMemory.Interval = 500;
            this.timerUpdateMemory.Tick += new System.EventHandler(this.timerUpdateMemory_Tick);
            // 
            // cmAllScripts
            // 
            this.cmAllScripts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAllScriptsTeleport,
            this.miAllScriptsSetNote,
            this.toolStripSeparator2,
            this.miAllScriptsRefresh,
            this.miAllScriptsCopy,
            this.toolStripSeparator3,
            this.miAllScriptsGame,
            this.miAllScriptsInternal,
            this.miAllScriptsFile,
            this.miAllScriptsReinterpret});
            this.cmAllScripts.Name = "cmAllQuests";
            this.cmAllScripts.ShowImageMargin = false;
            this.cmAllScripts.Size = new System.Drawing.Size(306, 192);
            this.cmAllScripts.Opening += new System.ComponentModel.CancelEventHandler(this.cmAllScripts_Opening);
            // 
            // miAllScriptsTeleport
            // 
            this.miAllScriptsTeleport.Name = "miAllScriptsTeleport";
            this.miAllScriptsTeleport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.miAllScriptsTeleport.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsTeleport.Text = "&Teleport to these coordinates";
            this.miAllScriptsTeleport.Click += new System.EventHandler(this.miAllScriptsTeleport_Click);
            // 
            // miAllScriptsSetNote
            // 
            this.miAllScriptsSetNote.Name = "miAllScriptsSetNote";
            this.miAllScriptsSetNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miAllScriptsSetNote.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsSetNote.Text = "Set encounter text for this script\'s square";
            this.miAllScriptsSetNote.Click += new System.EventHandler(this.miAllScriptsSetNote_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(302, 6);
            // 
            // miAllScriptsRefresh
            // 
            this.miAllScriptsRefresh.Name = "miAllScriptsRefresh";
            this.miAllScriptsRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miAllScriptsRefresh.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsRefresh.Text = "&Refresh";
            this.miAllScriptsRefresh.Click += new System.EventHandler(this.cmAllScriptsRefresh_Click);
            // 
            // miAllScriptsCopy
            // 
            this.miAllScriptsCopy.Name = "miAllScriptsCopy";
            this.miAllScriptsCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miAllScriptsCopy.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsCopy.Text = "&Copy";
            this.miAllScriptsCopy.Click += new System.EventHandler(this.miAllScriptsCopy_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(302, 6);
            // 
            // miAllScriptsGame
            // 
            this.miAllScriptsGame.Checked = true;
            this.miAllScriptsGame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miAllScriptsGame.Name = "miAllScriptsGame";
            this.miAllScriptsGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.miAllScriptsGame.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsGame.Text = "Use &game memory";
            this.miAllScriptsGame.Click += new System.EventHandler(this.miAllScriptsGame_Click);
            // 
            // miAllScriptsInternal
            // 
            this.miAllScriptsInternal.Name = "miAllScriptsInternal";
            this.miAllScriptsInternal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.miAllScriptsInternal.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsInternal.Text = "Use &internal data";
            this.miAllScriptsInternal.Click += new System.EventHandler(this.miAllScriptsInternal_Click);
            // 
            // miAllScriptsFile
            // 
            this.miAllScriptsFile.Name = "miAllScriptsFile";
            this.miAllScriptsFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.miAllScriptsFile.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsFile.Text = "Use external &file";
            this.miAllScriptsFile.Click += new System.EventHandler(this.miAllScriptsFile_Click);
            // 
            // miAllScriptsReinterpret
            // 
            this.miAllScriptsReinterpret.Name = "miAllScriptsReinterpret";
            this.miAllScriptsReinterpret.Size = new System.Drawing.Size(305, 22);
            this.miAllScriptsReinterpret.Text = "Debug: Reinterpret";
            this.miAllScriptsReinterpret.Click += new System.EventHandler(this.miAllScriptsReinterpret_Click);
            // 
            // cmSelectedScript
            // 
            this.cmSelectedScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miScriptsGoToMap,
            this.miSelectedScriptTeleport,
            this.miScriptsSetBeacon,
            this.toolStripSeparator1,
            this.miScriptsRefresh,
            this.miScriptCopy,
            this.miScriptEdit,
            this.miScriptReinterpret,
            this.miScriptCopyCommandOffset});
            this.cmSelectedScript.Name = "cmNotes";
            this.cmSelectedScript.ShowImageMargin = false;
            this.cmSelectedScript.Size = new System.Drawing.Size(288, 186);
            this.cmSelectedScript.Opening += new System.ComponentModel.CancelEventHandler(this.cmSelectedScript_Opening);
            // 
            // miScriptsGoToMap
            // 
            this.miScriptsGoToMap.Name = "miScriptsGoToMap";
            this.miScriptsGoToMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.miScriptsGoToMap.Size = new System.Drawing.Size(287, 22);
            this.miScriptsGoToMap.Text = "&Go to this map";
            this.miScriptsGoToMap.Click += new System.EventHandler(this.miScriptsGoToMap_Click);
            // 
            // miSelectedScriptTeleport
            // 
            this.miSelectedScriptTeleport.Name = "miSelectedScriptTeleport";
            this.miSelectedScriptTeleport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.miSelectedScriptTeleport.Size = new System.Drawing.Size(287, 22);
            this.miSelectedScriptTeleport.Text = "&Teleport to these coordinates";
            this.miSelectedScriptTeleport.Click += new System.EventHandler(this.miSelectedScriptTeleport_Click);
            // 
            // miScriptsSetBeacon
            // 
            this.miScriptsSetBeacon.Name = "miScriptsSetBeacon";
            this.miScriptsSetBeacon.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.miScriptsSetBeacon.Size = new System.Drawing.Size(287, 22);
            this.miScriptsSetBeacon.Text = "Set Lloyd\'s &Beacon here";
            this.miScriptsSetBeacon.Click += new System.EventHandler(this.miScriptsSetBeacon_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(284, 6);
            // 
            // miScriptsRefresh
            // 
            this.miScriptsRefresh.Name = "miScriptsRefresh";
            this.miScriptsRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miScriptsRefresh.Size = new System.Drawing.Size(287, 22);
            this.miScriptsRefresh.Text = "&Refresh";
            this.miScriptsRefresh.Click += new System.EventHandler(this.miScriptsRefresh_Click);
            // 
            // miScriptCopy
            // 
            this.miScriptCopy.Name = "miScriptCopy";
            this.miScriptCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miScriptCopy.Size = new System.Drawing.Size(287, 22);
            this.miScriptCopy.Text = "&Copy";
            this.miScriptCopy.Click += new System.EventHandler(this.miScriptCopy_Click);
            // 
            // miScriptEdit
            // 
            this.miScriptEdit.Name = "miScriptEdit";
            this.miScriptEdit.Size = new System.Drawing.Size(287, 22);
            this.miScriptEdit.Text = "&Edit";
            this.miScriptEdit.Click += new System.EventHandler(this.miScriptEdit_Click);
            // 
            // miScriptReinterpret
            // 
            this.miScriptReinterpret.Name = "miScriptReinterpret";
            this.miScriptReinterpret.Size = new System.Drawing.Size(287, 22);
            this.miScriptReinterpret.Text = "Debug: Reinterpret";
            this.miScriptReinterpret.Click += new System.EventHandler(this.miScriptReinterpret_Click);
            // 
            // miScriptCopyCommandOffset
            // 
            this.miScriptCopyCommandOffset.Name = "miScriptCopyCommandOffset";
            this.miScriptCopyCommandOffset.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.miScriptCopyCommandOffset.Size = new System.Drawing.Size(287, 22);
            this.miScriptCopyCommandOffset.Text = "Debug: Copy Command Offset";
            this.miScriptCopyCommandOffset.Click += new System.EventHandler(this.miScriptCopyCommandOffset_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(654, 227);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scScriptsFind);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scSelectedFind);
            this.splitContainer1.Size = new System.Drawing.Size(733, 546);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // scScriptsFind
            // 
            this.scScriptsFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scScriptsFind.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scScriptsFind.IsSplitterFixed = true;
            this.scScriptsFind.Location = new System.Drawing.Point(0, 0);
            this.scScriptsFind.Name = "scScriptsFind";
            this.scScriptsFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scScriptsFind.Panel1
            // 
            this.scScriptsFind.Panel1.Controls.Add(this.lvScripts);
            this.scScriptsFind.Panel1.Controls.Add(this.label1);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideAddresses);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideOffMapScripts);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideUnreachable);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideEmptyScripts);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideEmptyLines);
            this.scScriptsFind.Panel1.Controls.Add(this.cbHideInlineSubscripts);
            // 
            // scScriptsFind.Panel2
            // 
            this.scScriptsFind.Panel2.Controls.Add(this.label2);
            this.scScriptsFind.Panel2.Controls.Add(this.tbFindScripts);
            this.scScriptsFind.Size = new System.Drawing.Size(729, 256);
            this.scScriptsFind.SplitterDistance = 227;
            this.scScriptsFind.TabIndex = 8;
            // 
            // lvScripts
            // 
            this.lvScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chScriptIndex,
            this.chPosition,
            this.chDirection,
            this.chAuto,
            this.chLines,
            this.chNumBytes,
            this.chSummary});
            this.lvScripts.ContextMenuStrip = this.cmAllScripts;
            this.lvScripts.FullRowSelect = true;
            this.lvScripts.HideSelection = false;
            this.lvScripts.Location = new System.Drawing.Point(0, 0);
            this.lvScripts.MultiSelect = false;
            this.lvScripts.Name = "lvScripts";
            this.lvScripts.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvScripts.Size = new System.Drawing.Size(729, 204);
            this.lvScripts.TabIndex = 0;
            this.lvScripts.UseCompatibleStateImageBehavior = false;
            this.lvScripts.View = System.Windows.Forms.View.Details;
            this.lvScripts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvScripts_ColumnClick);
            this.lvScripts.SelectedIndexChanged += new System.EventHandler(this.lvScripts_SelectedIndexChanged);
            // 
            // chScriptIndex
            // 
            this.chScriptIndex.Text = "Idx";
            this.chScriptIndex.Width = 33;
            // 
            // chPosition
            // 
            this.chPosition.Text = "Pos";
            this.chPosition.Width = 54;
            // 
            // chDirection
            // 
            this.chDirection.Text = "Dir";
            this.chDirection.Width = 43;
            // 
            // chAuto
            // 
            this.chAuto.Text = "Auto";
            this.chAuto.Width = 40;
            // 
            // chLines
            // 
            this.chLines.Text = "Lines";
            this.chLines.Width = 52;
            // 
            // chNumBytes
            // 
            this.chNumBytes.Text = "Bytes";
            // 
            // chSummary
            // 
            this.chSummary.Text = "Summary";
            this.chSummary.Width = 105;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hide:";
            // 
            // cbHideAddresses
            // 
            this.cbHideAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideAddresses.AutoSize = true;
            this.cbHideAddresses.Checked = true;
            this.cbHideAddresses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideAddresses.Location = new System.Drawing.Point(582, 206);
            this.cbHideAddresses.Name = "cbHideAddresses";
            this.cbHideAddresses.Size = new System.Drawing.Size(75, 17);
            this.cbHideAddresses.TabIndex = 7;
            this.cbHideAddresses.Text = "&Addresses";
            this.cbHideAddresses.UseVisualStyleBackColor = true;
            this.cbHideAddresses.CheckedChanged += new System.EventHandler(this.cbHideAddresses_CheckedChanged);
            // 
            // cbHideOffMapScripts
            // 
            this.cbHideOffMapScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideOffMapScripts.AutoSize = true;
            this.cbHideOffMapScripts.Location = new System.Drawing.Point(41, 206);
            this.cbHideOffMapScripts.Name = "cbHideOffMapScripts";
            this.cbHideOffMapScripts.Size = new System.Drawing.Size(99, 17);
            this.cbHideOffMapScripts.TabIndex = 2;
            this.cbHideOffMapScripts.Text = "&Off-Map Scripts";
            this.cbHideOffMapScripts.UseVisualStyleBackColor = true;
            this.cbHideOffMapScripts.CheckedChanged += new System.EventHandler(this.cbHideOffMap_CheckedChanged);
            // 
            // cbHideUnreachable
            // 
            this.cbHideUnreachable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideUnreachable.AutoSize = true;
            this.cbHideUnreachable.Checked = true;
            this.cbHideUnreachable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideUnreachable.Location = new System.Drawing.Point(461, 206);
            this.cbHideUnreachable.Name = "cbHideUnreachable";
            this.cbHideUnreachable.Size = new System.Drawing.Size(115, 17);
            this.cbHideUnreachable.TabIndex = 6;
            this.cbHideUnreachable.Text = "&Unreachable Code";
            this.cbHideUnreachable.UseVisualStyleBackColor = true;
            this.cbHideUnreachable.CheckedChanged += new System.EventHandler(this.cbHideUnreachable_CheckedChanged);
            // 
            // cbHideEmptyScripts
            // 
            this.cbHideEmptyScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideEmptyScripts.AutoSize = true;
            this.cbHideEmptyScripts.Location = new System.Drawing.Point(151, 206);
            this.cbHideEmptyScripts.Name = "cbHideEmptyScripts";
            this.cbHideEmptyScripts.Size = new System.Drawing.Size(90, 17);
            this.cbHideEmptyScripts.TabIndex = 3;
            this.cbHideEmptyScripts.Text = "Empty &Scripts";
            this.cbHideEmptyScripts.UseVisualStyleBackColor = true;
            this.cbHideEmptyScripts.CheckedChanged += new System.EventHandler(this.cbHideDoNothing_CheckedChanged);
            // 
            // cbHideEmptyLines
            // 
            this.cbHideEmptyLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideEmptyLines.AutoSize = true;
            this.cbHideEmptyLines.Checked = true;
            this.cbHideEmptyLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideEmptyLines.Location = new System.Drawing.Point(366, 206);
            this.cbHideEmptyLines.Name = "cbHideEmptyLines";
            this.cbHideEmptyLines.Size = new System.Drawing.Size(83, 17);
            this.cbHideEmptyLines.TabIndex = 5;
            this.cbHideEmptyLines.Text = "&Empty Lines";
            this.cbHideEmptyLines.UseVisualStyleBackColor = true;
            this.cbHideEmptyLines.CheckedChanged += new System.EventHandler(this.cbHideEmptyLines_CheckedChanged);
            // 
            // cbHideInlineSubscripts
            // 
            this.cbHideInlineSubscripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHideInlineSubscripts.AutoSize = true;
            this.cbHideInlineSubscripts.Location = new System.Drawing.Point(252, 206);
            this.cbHideInlineSubscripts.Name = "cbHideInlineSubscripts";
            this.cbHideInlineSubscripts.Size = new System.Drawing.Size(103, 17);
            this.cbHideInlineSubscripts.TabIndex = 4;
            this.cbHideInlineSubscripts.Text = "&Inline Subscripts";
            this.cbHideInlineSubscripts.UseVisualStyleBackColor = true;
            this.cbHideInlineSubscripts.CheckedChanged += new System.EventHandler(this.cbHideSubscriptLines_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Find:";
            // 
            // tbFindScripts
            // 
            this.tbFindScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFindScripts.Location = new System.Drawing.Point(42, 3);
            this.tbFindScripts.Name = "tbFindScripts";
            this.tbFindScripts.Size = new System.Drawing.Size(684, 20);
            this.tbFindScripts.TabIndex = 3;
            // 
            // scSelectedFind
            // 
            this.scSelectedFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSelectedFind.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSelectedFind.IsSplitterFixed = true;
            this.scSelectedFind.Location = new System.Drawing.Point(0, 0);
            this.scSelectedFind.Name = "scSelectedFind";
            this.scSelectedFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSelectedFind.Panel1
            // 
            this.scSelectedFind.Panel1.Controls.Add(this.lvSelectedScript);
            this.scSelectedFind.Panel1.Controls.Add(this.btnClose);
            this.scSelectedFind.Panel1.Controls.Add(this.btnOK);
            // 
            // scSelectedFind.Panel2
            // 
            this.scSelectedFind.Panel2.Controls.Add(this.label3);
            this.scSelectedFind.Panel2.Controls.Add(this.tbFindSelected);
            this.scSelectedFind.Size = new System.Drawing.Size(729, 278);
            this.scSelectedFind.SplitterDistance = 249;
            this.scSelectedFind.TabIndex = 2;
            // 
            // lvSelectedScript
            // 
            this.lvSelectedScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSelectedScript.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLine,
            this.chHeaderBytes,
            this.chCommandBytes,
            this.chDescription});
            this.lvSelectedScript.ContextMenuStrip = this.cmSelectedScript;
            this.lvSelectedScript.FullRowSelect = true;
            this.lvSelectedScript.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSelectedScript.HideSelection = false;
            this.lvSelectedScript.Location = new System.Drawing.Point(0, 0);
            this.lvSelectedScript.MultiSelect = false;
            this.lvSelectedScript.Name = "lvSelectedScript";
            this.lvSelectedScript.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvSelectedScript.Size = new System.Drawing.Size(729, 221);
            this.lvSelectedScript.TabIndex = 0;
            this.lvSelectedScript.UseCompatibleStateImageBehavior = false;
            this.lvSelectedScript.View = System.Windows.Forms.View.Details;
            this.lvSelectedScript.DoubleClick += new System.EventHandler(this.lvSelectedScript_DoubleClick);
            // 
            // chLine
            // 
            this.chLine.Text = "Line";
            this.chLine.Width = 45;
            // 
            // chHeaderBytes
            // 
            this.chHeaderBytes.Text = "Header Bytes";
            this.chHeaderBytes.Width = 83;
            // 
            // chCommandBytes
            // 
            this.chCommandBytes.Text = "Command Bytes";
            this.chCommandBytes.Width = 99;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 79;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(654, 227);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Find:";
            // 
            // tbFindSelected
            // 
            this.tbFindSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFindSelected.Location = new System.Drawing.Point(39, 3);
            this.tbFindSelected.Name = "tbFindSelected";
            this.tbFindSelected.Size = new System.Drawing.Size(687, 20);
            this.tbFindSelected.TabIndex = 5;
            // 
            // ScriptsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(733, 546);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "ScriptsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scripts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptsForm_FormClosing);
            this.Load += new System.EventHandler(this.ScriptsForm_Load);
            this.cmAllScripts.ResumeLayout(false);
            this.cmSelectedScript.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.scScriptsFind.Panel1.ResumeLayout(false);
            this.scScriptsFind.Panel1.PerformLayout();
            this.scScriptsFind.Panel2.ResumeLayout(false);
            this.scScriptsFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scScriptsFind)).EndInit();
            this.scScriptsFind.ResumeLayout(false);
            this.scSelectedFind.Panel1.ResumeLayout(false);
            this.scSelectedFind.Panel2.ResumeLayout(false);
            this.scSelectedFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSelectedFind)).EndInit();
            this.scSelectedFind.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.Timer timerUpdateMemory;
        private WhereAreWe.DBListView lvScripts;
        private System.Windows.Forms.ColumnHeader chPosition;
        private System.Windows.Forms.ColumnHeader chLines;
        private System.Windows.Forms.ColumnHeader chSummary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private WhereAreWe.DBListView lvSelectedScript;
        private System.Windows.Forms.ColumnHeader chLine;
        private System.Windows.Forms.ColumnHeader chHeaderBytes;
        private System.Windows.Forms.ColumnHeader chDirection;
        private System.Windows.Forms.ContextMenuStrip cmSelectedScript;
        private System.Windows.Forms.ToolStripMenuItem miScriptsGoToMap;
        private System.Windows.Forms.ToolStripMenuItem miScriptsSetBeacon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miScriptsRefresh;
        private System.Windows.Forms.CheckBox cbHideOffMapScripts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbHideEmptyScripts;
        private System.Windows.Forms.ContextMenuStrip cmAllScripts;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsRefresh;
        private System.Windows.Forms.ToolStripMenuItem miSelectedScriptTeleport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ToolStripMenuItem miScriptCopy;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsCopy;
        private System.Windows.Forms.ColumnHeader chCommandBytes;
        private System.Windows.Forms.ToolStripMenuItem miScriptEdit;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsTeleport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox cbHideEmptyLines;
        private System.Windows.Forms.CheckBox cbHideInlineSubscripts;
        private System.Windows.Forms.ColumnHeader chScriptIndex;
        private System.Windows.Forms.ColumnHeader chNumBytes;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsFile;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsInternal;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsSetNote;
        private System.Windows.Forms.ToolStripMenuItem miScriptReinterpret;
        private System.Windows.Forms.ToolStripMenuItem miAllScriptsReinterpret;
        private System.Windows.Forms.CheckBox cbHideUnreachable;
        private System.Windows.Forms.ColumnHeader chAuto;
        private System.Windows.Forms.CheckBox cbHideAddresses;
        private System.Windows.Forms.ToolStripMenuItem miScriptCopyCommandOffset;
        private System.Windows.Forms.SplitContainer scScriptsFind;
        private System.Windows.Forms.SplitContainer scSelectedFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFindScripts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFindSelected;
        private System.Windows.Forms.Button btnOK;
    }
}