namespace WhereAreWe
{
    partial class QuestsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestsForm));
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateMemory = new System.Windows.Forms.Timer(this.components);
            this.cmAllQuests = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAllQuestsClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsSet = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAllQuestsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsMarkComplete = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAllQuestsShowGoals = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllQuestsSelectFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSelectedQuest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miQuestsGoToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuestsSetBeacon = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuestsTeleport = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuestsSetSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuestsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuestsHideInvalid = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuestsToggleCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListGoals = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tvQuests = new WhereAreWe.DBTreeView();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvInfo = new WhereAreWe.DBListView();
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.tbFindGoal = new System.Windows.Forms.TextBox();
            this.miSelectedSelectGameFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAllQuests.SuspendLayout();
            this.cmSelectedQuest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // miEditPath
            // 
            this.miEditPath.Name = "miEditPath";
            this.miEditPath.Size = new System.Drawing.Size(32, 19);
            // 
            // timerUpdateMemory
            // 
            this.timerUpdateMemory.Interval = 400;
            this.timerUpdateMemory.Tick += new System.EventHandler(this.timerUpdateMemory_Tick);
            // 
            // cmAllQuests
            // 
            this.cmAllQuests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAllQuestsClear,
            this.miAllQuestsSet,
            this.cmAllQuestsRefresh,
            this.miAllQuestsFlag,
            this.miAllQuestsMarkComplete,
            this.miAllQuestsExpand,
            this.miAllQuestsCollapse,
            this.toolStripSeparator2,
            this.miAllQuestsShowGoals,
            this.miAllQuestsSelectFiles});
            this.cmAllQuests.Name = "cmAllQuests";
            this.cmAllQuests.ShowCheckMargin = true;
            this.cmAllQuests.ShowImageMargin = false;
            this.cmAllQuests.Size = new System.Drawing.Size(195, 208);
            this.cmAllQuests.Opening += new System.ComponentModel.CancelEventHandler(this.cmAllQuests_Opening);
            // 
            // miAllQuestsClear
            // 
            this.miAllQuestsClear.Name = "miAllQuestsClear";
            this.miAllQuestsClear.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsClear.Text = "Clear &quest bits";
            this.miAllQuestsClear.Click += new System.EventHandler(this.miAllQuestsClear_Click);
            // 
            // miAllQuestsSet
            // 
            this.miAllQuestsSet.Name = "miAllQuestsSet";
            this.miAllQuestsSet.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsSet.Text = "&Set quest bits";
            this.miAllQuestsSet.Click += new System.EventHandler(this.miAllQuestsSet_Click);
            // 
            // cmAllQuestsRefresh
            // 
            this.cmAllQuestsRefresh.Name = "cmAllQuestsRefresh";
            this.cmAllQuestsRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.cmAllQuestsRefresh.Size = new System.Drawing.Size(194, 22);
            this.cmAllQuestsRefresh.Text = "&Refresh";
            this.cmAllQuestsRefresh.Click += new System.EventHandler(this.cmAllQuestsRefresh_Click);
            // 
            // miAllQuestsFlag
            // 
            this.miAllQuestsFlag.Name = "miAllQuestsFlag";
            this.miAllQuestsFlag.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.miAllQuestsFlag.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsFlag.Text = "&Flag this quest";
            this.miAllQuestsFlag.Click += new System.EventHandler(this.miAllQuestsFlag_Click);
            // 
            // miAllQuestsMarkComplete
            // 
            this.miAllQuestsMarkComplete.Name = "miAllQuestsMarkComplete";
            this.miAllQuestsMarkComplete.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsMarkComplete.Text = "&Complete quest manually";
            this.miAllQuestsMarkComplete.Click += new System.EventHandler(this.miAllQuestsMarkComplete_Click);
            // 
            // miAllQuestsExpand
            // 
            this.miAllQuestsExpand.Name = "miAllQuestsExpand";
            this.miAllQuestsExpand.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsExpand.Text = "&Expand all";
            this.miAllQuestsExpand.Click += new System.EventHandler(this.miAllQuestsExpand_Click);
            // 
            // miAllQuestsCollapse
            // 
            this.miAllQuestsCollapse.Name = "miAllQuestsCollapse";
            this.miAllQuestsCollapse.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsCollapse.Text = "Collapse &all";
            this.miAllQuestsCollapse.Click += new System.EventHandler(this.miAllQuestsCollapse_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // miAllQuestsShowGoals
            // 
            this.miAllQuestsShowGoals.Name = "miAllQuestsShowGoals";
            this.miAllQuestsShowGoals.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsShowGoals.Text = "Show quest &goals";
            this.miAllQuestsShowGoals.Click += new System.EventHandler(this.miAllQuestsHideGoals_Click);
            // 
            // miAllQuestsSelectFiles
            // 
            this.miAllQuestsSelectFiles.Name = "miAllQuestsSelectFiles";
            this.miAllQuestsSelectFiles.Size = new System.Drawing.Size(194, 22);
            this.miAllQuestsSelectFiles.Text = "Select game f&iles";
            this.miAllQuestsSelectFiles.Click += new System.EventHandler(this.miAllQuestsSelectFiles_Click);
            // 
            // cmSelectedQuest
            // 
            this.cmSelectedQuest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miQuestsGoToMap,
            this.miQuestsSetBeacon,
            this.miQuestsTeleport,
            this.miQuestsSetSurface,
            this.toolStripSeparator1,
            this.miQuestsRefresh,
            this.miQuestsHideInvalid,
            this.miQuestsToggleCompleted,
            this.miSelectedSelectGameFiles});
            this.cmSelectedQuest.Name = "cmNotes";
            this.cmSelectedQuest.Size = new System.Drawing.Size(266, 208);
            this.cmSelectedQuest.Opening += new System.ComponentModel.CancelEventHandler(this.cmQuests_Opening);
            // 
            // miQuestsGoToMap
            // 
            this.miQuestsGoToMap.Name = "miQuestsGoToMap";
            this.miQuestsGoToMap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.miQuestsGoToMap.Size = new System.Drawing.Size(265, 22);
            this.miQuestsGoToMap.Text = "&Go to this map";
            this.miQuestsGoToMap.Click += new System.EventHandler(this.miQuestsGoToMap_Click);
            // 
            // miQuestsSetBeacon
            // 
            this.miQuestsSetBeacon.Name = "miQuestsSetBeacon";
            this.miQuestsSetBeacon.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.miQuestsSetBeacon.Size = new System.Drawing.Size(265, 22);
            this.miQuestsSetBeacon.Text = "Set Lloyd\'s &Beacon here";
            this.miQuestsSetBeacon.Click += new System.EventHandler(this.miQuestsSetBeacon_Click);
            // 
            // miQuestsTeleport
            // 
            this.miQuestsTeleport.Name = "miQuestsTeleport";
            this.miQuestsTeleport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.miQuestsTeleport.Size = new System.Drawing.Size(265, 22);
            this.miQuestsTeleport.Text = "&Teleport to these coordinates";
            this.miQuestsTeleport.Click += new System.EventHandler(this.miQuestsTeleport_Click);
            // 
            // miQuestsSetSurface
            // 
            this.miQuestsSetSurface.Name = "miQuestsSetSurface";
            this.miQuestsSetSurface.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miQuestsSetSurface.Size = new System.Drawing.Size(265, 22);
            this.miQuestsSetSurface.Text = "Set &Surface coordinates to here";
            this.miQuestsSetSurface.Click += new System.EventHandler(this.miQuestsSetSurface_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(262, 6);
            // 
            // miQuestsRefresh
            // 
            this.miQuestsRefresh.Name = "miQuestsRefresh";
            this.miQuestsRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.miQuestsRefresh.Size = new System.Drawing.Size(265, 22);
            this.miQuestsRefresh.Text = "&Refresh";
            this.miQuestsRefresh.Click += new System.EventHandler(this.miQuestsRefresh_Click);
            // 
            // miQuestsHideInvalid
            // 
            this.miQuestsHideInvalid.Name = "miQuestsHideInvalid";
            this.miQuestsHideInvalid.Size = new System.Drawing.Size(265, 22);
            this.miQuestsHideInvalid.Text = "Hide &invalid quest goals";
            this.miQuestsHideInvalid.Click += new System.EventHandler(this.miQuestsHideInvalid_Click);
            // 
            // miQuestsToggleCompleted
            // 
            this.miQuestsToggleCompleted.Name = "miQuestsToggleCompleted";
            this.miQuestsToggleCompleted.Size = new System.Drawing.Size(265, 22);
            this.miQuestsToggleCompleted.Text = "Toggle &Completed";
            this.miQuestsToggleCompleted.Click += new System.EventHandler(this.miQuestsToggleCompleted_Click);
            // 
            // imageListGoals
            // 
            this.imageListGoals.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGoals.ImageStream")));
            this.imageListGoals.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGoals.Images.SetKeyName(0, "pngSequentialGoal.png");
            this.imageListGoals.Images.SetKeyName(1, "pngSimultaneousGoal.png");
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(420, 435);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(947, 486);
            this.splitContainer1.SplitterDistance = 442;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tvQuests);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label52);
            this.splitContainer3.Panel2.Controls.Add(this.tbFind);
            this.splitContainer3.Panel2MinSize = 20;
            this.splitContainer3.Size = new System.Drawing.Size(442, 486);
            this.splitContainer3.SplitterDistance = 457;
            this.splitContainer3.TabIndex = 0;
            // 
            // tvQuests
            // 
            this.tvQuests.ContextMenuStrip = this.cmAllQuests;
            this.tvQuests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvQuests.FullRowSelect = true;
            this.tvQuests.HideSelection = false;
            this.tvQuests.Location = new System.Drawing.Point(0, 0);
            this.tvQuests.Name = "tvQuests";
            this.tvQuests.ScrollPosition = new System.Drawing.Point(0, 0);
            this.tvQuests.Size = new System.Drawing.Size(442, 457);
            this.tvQuests.TabIndex = 0;
            this.tvQuests.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvQuests_AfterExpand);
            this.tvQuests.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQuests_AfterSelect);
            this.tvQuests.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvQuests_NodeMouseClick);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 4);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 0;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 0);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(403, 20);
            this.tbFind.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvInfo);
            this.splitContainer2.Panel1.Controls.Add(this.btnClose);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.tbFindGoal);
            this.splitContainer2.Panel2MinSize = 20;
            this.splitContainer2.Size = new System.Drawing.Size(498, 486);
            this.splitContainer2.SplitterDistance = 457;
            this.splitContainer2.TabIndex = 3;
            // 
            // lvInfo
            // 
            this.lvInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription,
            this.chLocation});
            this.lvInfo.ContextMenuStrip = this.cmSelectedQuest;
            this.lvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInfo.FullRowSelect = true;
            this.lvInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvInfo.HideSelection = false;
            this.lvInfo.Location = new System.Drawing.Point(0, 0);
            this.lvInfo.MultiSelect = false;
            this.lvInfo.Name = "lvInfo";
            this.lvInfo.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvInfo.ShowItemToolTips = true;
            this.lvInfo.Size = new System.Drawing.Size(498, 457);
            this.lvInfo.TabIndex = 0;
            this.lvInfo.UseCompatibleStateImageBehavior = false;
            this.lvInfo.View = System.Windows.Forms.View.Details;
            this.lvInfo.SelectedIndexChanged += new System.EventHandler(this.lvInfo_SelectedIndexChanged);
            this.lvInfo.DoubleClick += new System.EventHandler(this.lvInfo_DoubleClick);
            this.lvInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvInfo_KeyDown);
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 188;
            // 
            // chLocation
            // 
            this.chLocation.Text = "Location";
            this.chLocation.Width = 247;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find:";
            // 
            // tbFindGoal
            // 
            this.tbFindGoal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFindGoal.Location = new System.Drawing.Point(39, 0);
            this.tbFindGoal.Name = "tbFindGoal";
            this.tbFindGoal.Size = new System.Drawing.Size(459, 20);
            this.tbFindGoal.TabIndex = 1;
            // 
            // miSelectedSelectGameFiles
            // 
            this.miSelectedSelectGameFiles.Name = "miSelectedSelectGameFiles";
            this.miSelectedSelectGameFiles.Size = new System.Drawing.Size(265, 22);
            this.miSelectedSelectGameFiles.Text = "Select game f&iles";
            this.miSelectedSelectGameFiles.Click += new System.EventHandler(this.miAllQuestsSelectFiles_Click);
            // 
            // QuestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(947, 486);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "QuestsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quests";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestForm_FormClosing);
            this.cmAllQuests.ResumeLayout(false);
            this.cmSelectedQuest.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.Timer timerUpdateMemory;
        private WhereAreWe.DBListView lvInfo;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ContextMenuStrip cmSelectedQuest;
        private System.Windows.Forms.ToolStripMenuItem miQuestsGoToMap;
        private System.Windows.Forms.ToolStripMenuItem miQuestsSetBeacon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miQuestsRefresh;
        private System.Windows.Forms.ContextMenuStrip cmAllQuests;
        private System.Windows.Forms.ToolStripMenuItem cmAllQuestsRefresh;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsClear;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsSet;
        private System.Windows.Forms.ToolStripMenuItem miQuestsTeleport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem miQuestsSetSurface;
        private System.Windows.Forms.ColumnHeader chLocation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private WhereAreWe.DBTreeView tvQuests;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsFlag;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsExpand;
        private System.Windows.Forms.ToolStripMenuItem miQuestsHideInvalid;
        private System.Windows.Forms.ImageList imageListGoals;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsCollapse;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsShowGoals;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsSelectFiles;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFindGoal;
        private System.Windows.Forms.ToolStripMenuItem miQuestsToggleCompleted;
        private System.Windows.Forms.ToolStripMenuItem miAllQuestsMarkComplete;
        private System.Windows.Forms.ToolStripMenuItem miSelectedSelectGameFiles;
    }
}