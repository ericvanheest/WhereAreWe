namespace WhereAreWe
{
    partial class EncounterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncounterForm));
            this.cmMonsters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMonsterEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonsterShowOffMap = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonsterRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonsterTeleportParty = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonsterMoveMonster = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonsterMonitorHP = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListMonsters = new System.Windows.Forms.ImageList(this.components);
            this.timerRefreshInfo = new System.Windows.Forms.Timer(this.components);
            this.timerRefocus = new System.Windows.Forms.Timer(this.components);
            this.cmColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miColumnsName = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsProximity = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsHP = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsAC = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsDamage = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsSpeed = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsAccuracy = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsResistances = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsExperience = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsTreasure = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctlTreasure = new WhereAreWe.TreasurePanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvMonsters = new WhereAreWe.DBListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProximity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHitPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chArmorClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAccuracy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResistances = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpecialPowers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExperience = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTreasure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ccl1 = new WhereAreWe.CharCombatLabel();
            this.ccl2 = new WhereAreWe.CharCombatLabel();
            this.ccl3 = new WhereAreWe.CharCombatLabel();
            this.ccl5 = new WhereAreWe.CharCombatLabel();
            this.ccl8 = new WhereAreWe.CharCombatLabel();
            this.ccl7 = new WhereAreWe.CharCombatLabel();
            this.ccl6 = new WhereAreWe.CharCombatLabel();
            this.ccl4 = new WhereAreWe.CharCombatLabel();
            this.tbExtraText = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cmMonsters.SuspendLayout();
            this.cmColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmMonsters
            // 
            this.cmMonsters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMonsterEdit,
            this.miMonsterShowOffMap,
            this.miMonsterRemoveAll,
            this.miMonsterTeleportParty,
            this.miMonsterMoveMonster,
            this.miMonsterMonitorHP});
            this.cmMonsters.Name = "cmMonsters";
            this.cmMonsters.ShowCheckMargin = true;
            this.cmMonsters.ShowImageMargin = false;
            this.cmMonsters.Size = new System.Drawing.Size(240, 136);
            this.cmMonsters.Opening += new System.ComponentModel.CancelEventHandler(this.cmMonsters_Opening);
            // 
            // miMonsterEdit
            // 
            this.miMonsterEdit.Name = "miMonsterEdit";
            this.miMonsterEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.miMonsterEdit.Size = new System.Drawing.Size(239, 22);
            this.miMonsterEdit.Text = "&Edit";
            this.miMonsterEdit.Click += new System.EventHandler(this.miMonsterEdit_Click);
            // 
            // miMonsterShowOffMap
            // 
            this.miMonsterShowOffMap.Name = "miMonsterShowOffMap";
            this.miMonsterShowOffMap.Size = new System.Drawing.Size(239, 22);
            this.miMonsterShowOffMap.Text = "&Show dead (off-map) monsters";
            this.miMonsterShowOffMap.Click += new System.EventHandler(this.miMonsterShowDead_Click);
            // 
            // miMonsterRemoveAll
            // 
            this.miMonsterRemoveAll.Name = "miMonsterRemoveAll";
            this.miMonsterRemoveAll.Size = new System.Drawing.Size(239, 22);
            this.miMonsterRemoveAll.Text = "&Remove all monsters";
            this.miMonsterRemoveAll.Click += new System.EventHandler(this.miMonsterRemoveAll_Click);
            // 
            // miMonsterTeleportParty
            // 
            this.miMonsterTeleportParty.Name = "miMonsterTeleportParty";
            this.miMonsterTeleportParty.Size = new System.Drawing.Size(239, 22);
            this.miMonsterTeleportParty.Text = "&Teleport party to monster";
            this.miMonsterTeleportParty.Click += new System.EventHandler(this.miMonsterTeleportParty_Click);
            // 
            // miMonsterMoveMonster
            // 
            this.miMonsterMoveMonster.Name = "miMonsterMoveMonster";
            this.miMonsterMoveMonster.Size = new System.Drawing.Size(239, 22);
            this.miMonsterMoveMonster.Text = "&Move monster to party";
            this.miMonsterMoveMonster.Click += new System.EventHandler(this.miMonsterMoveMonster_Click);
            // 
            // miMonsterMonitorHP
            // 
            this.miMonsterMonitorHP.Name = "miMonsterMonitorHP";
            this.miMonsterMonitorHP.Size = new System.Drawing.Size(239, 22);
            this.miMonsterMonitorHP.Text = "Debug: Monitor HP changes";
            this.miMonsterMonitorHP.Click += new System.EventHandler(this.miMonsterMonitorHP_Click);
            // 
            // imageListMonsters
            // 
            this.imageListMonsters.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMonsters.ImageStream")));
            this.imageListMonsters.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMonsters.Images.SetKeyName(0, "MeleeCheck.png");
            this.imageListMonsters.Images.SetKeyName(1, "pngActive.png");
            // 
            // timerRefreshInfo
            // 
            this.timerRefreshInfo.Interval = 200;
            this.timerRefreshInfo.Tick += new System.EventHandler(this.timerRefreshInfo_Tick);
            // 
            // timerRefocus
            // 
            this.timerRefocus.Tick += new System.EventHandler(this.timerRefocus_Tick);
            // 
            // cmColumns
            // 
            this.cmColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miColumnsName,
            this.miColumnsProximity,
            this.miColumnsPosition,
            this.miColumnsHP,
            this.miColumnsAC,
            this.miColumnsDamage,
            this.miColumnsSpeed,
            this.miColumnsAccuracy,
            this.miColumnsResistances,
            this.miColumnsSpecial,
            this.miColumnsExperience,
            this.miColumnsTreasure,
            this.miColumnsTarget,
            this.miColumnsShowAll});
            this.cmColumns.Name = "cmMonsters";
            this.cmColumns.ShowCheckMargin = true;
            this.cmColumns.ShowImageMargin = false;
            this.cmColumns.Size = new System.Drawing.Size(154, 312);
            // 
            // miColumnsName
            // 
            this.miColumnsName.Checked = true;
            this.miColumnsName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsName.Name = "miColumnsName";
            this.miColumnsName.Size = new System.Drawing.Size(153, 22);
            this.miColumnsName.Text = "&Name";
            this.miColumnsName.Click += new System.EventHandler(this.miColumnsName_Click);
            // 
            // miColumnsProximity
            // 
            this.miColumnsProximity.Checked = true;
            this.miColumnsProximity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsProximity.Name = "miColumnsProximity";
            this.miColumnsProximity.Size = new System.Drawing.Size(153, 22);
            this.miColumnsProximity.Text = "Pro&ximity";
            this.miColumnsProximity.Click += new System.EventHandler(this.miColumnsProximity_Click);
            // 
            // miColumnsPosition
            // 
            this.miColumnsPosition.Checked = true;
            this.miColumnsPosition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsPosition.Name = "miColumnsPosition";
            this.miColumnsPosition.Size = new System.Drawing.Size(153, 22);
            this.miColumnsPosition.Text = "&Position";
            this.miColumnsPosition.Click += new System.EventHandler(this.miColumnsPosition_Click);
            // 
            // miColumnsHP
            // 
            this.miColumnsHP.Checked = true;
            this.miColumnsHP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsHP.Name = "miColumnsHP";
            this.miColumnsHP.Size = new System.Drawing.Size(153, 22);
            this.miColumnsHP.Text = "&Hit Points";
            this.miColumnsHP.Click += new System.EventHandler(this.miColumnsHP_Click);
            // 
            // miColumnsAC
            // 
            this.miColumnsAC.Checked = true;
            this.miColumnsAC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsAC.Name = "miColumnsAC";
            this.miColumnsAC.Size = new System.Drawing.Size(153, 22);
            this.miColumnsAC.Text = "&Armor Class";
            this.miColumnsAC.Click += new System.EventHandler(this.miColumnsAC_Click);
            // 
            // miColumnsDamage
            // 
            this.miColumnsDamage.Checked = true;
            this.miColumnsDamage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsDamage.Name = "miColumnsDamage";
            this.miColumnsDamage.Size = new System.Drawing.Size(153, 22);
            this.miColumnsDamage.Text = "&Damage";
            this.miColumnsDamage.Click += new System.EventHandler(this.miColumnsDamage_Click);
            // 
            // miColumnsSpeed
            // 
            this.miColumnsSpeed.Checked = true;
            this.miColumnsSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsSpeed.Name = "miColumnsSpeed";
            this.miColumnsSpeed.Size = new System.Drawing.Size(153, 22);
            this.miColumnsSpeed.Text = "&Speed";
            this.miColumnsSpeed.Click += new System.EventHandler(this.miColumnsSpeed_Click);
            // 
            // miColumnsAccuracy
            // 
            this.miColumnsAccuracy.Checked = true;
            this.miColumnsAccuracy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsAccuracy.Name = "miColumnsAccuracy";
            this.miColumnsAccuracy.Size = new System.Drawing.Size(153, 22);
            this.miColumnsAccuracy.Text = "A&ccuracy";
            this.miColumnsAccuracy.Click += new System.EventHandler(this.miColumnsAccuracy_Click);
            // 
            // miColumnsResistances
            // 
            this.miColumnsResistances.Checked = true;
            this.miColumnsResistances.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsResistances.Name = "miColumnsResistances";
            this.miColumnsResistances.Size = new System.Drawing.Size(153, 22);
            this.miColumnsResistances.Text = "&Resistances";
            this.miColumnsResistances.Click += new System.EventHandler(this.miColumnsResistances_Click);
            // 
            // miColumnsSpecial
            // 
            this.miColumnsSpecial.Checked = true;
            this.miColumnsSpecial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsSpecial.Name = "miColumnsSpecial";
            this.miColumnsSpecial.Size = new System.Drawing.Size(153, 22);
            this.miColumnsSpecial.Text = "Sp&ecial";
            this.miColumnsSpecial.Click += new System.EventHandler(this.miColumnsSpecial_Click);
            // 
            // miColumnsExperience
            // 
            this.miColumnsExperience.Checked = true;
            this.miColumnsExperience.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsExperience.Name = "miColumnsExperience";
            this.miColumnsExperience.Size = new System.Drawing.Size(153, 22);
            this.miColumnsExperience.Text = "&Experience";
            this.miColumnsExperience.Click += new System.EventHandler(this.miColumnsExperience_Click);
            // 
            // miColumnsTreasure
            // 
            this.miColumnsTreasure.Checked = true;
            this.miColumnsTreasure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsTreasure.Name = "miColumnsTreasure";
            this.miColumnsTreasure.Size = new System.Drawing.Size(153, 22);
            this.miColumnsTreasure.Text = "&Treasure";
            this.miColumnsTreasure.Click += new System.EventHandler(this.miColumnsTreasure_Click);
            // 
            // miColumnsTarget
            // 
            this.miColumnsTarget.Checked = true;
            this.miColumnsTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsTarget.Name = "miColumnsTarget";
            this.miColumnsTarget.Size = new System.Drawing.Size(153, 22);
            this.miColumnsTarget.Text = "Tar&get";
            this.miColumnsTarget.Click += new System.EventHandler(this.miColumnsTarget_Click);
            // 
            // miColumnsShowAll
            // 
            this.miColumnsShowAll.Name = "miColumnsShowAll";
            this.miColumnsShowAll.Size = new System.Drawing.Size(153, 22);
            this.miColumnsShowAll.Text = "Reset Columns";
            this.miColumnsShowAll.Click += new System.EventHandler(this.miColumnsShowAll_Click);
            // 
            // ctlTreasure
            // 
            this.ctlTreasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlTreasure.Location = new System.Drawing.Point(0, 0);
            this.ctlTreasure.Name = "ctlTreasure";
            this.ctlTreasure.Size = new System.Drawing.Size(604, 357);
            this.ctlTreasure.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvMonsters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(604, 357);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvMonsters
            // 
            this.lvMonsters.AllowColumnReorder = true;
            this.lvMonsters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chProximity,
            this.chPosition,
            this.chHitPoints,
            this.chArmorClass,
            this.chDamage,
            this.chSpeed,
            this.chAccuracy,
            this.chResistances,
            this.chSpecialPowers,
            this.chExperience,
            this.chTreasure,
            this.chTarget});
            this.lvMonsters.ContextMenuStrip = this.cmMonsters;
            this.lvMonsters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMonsters.FullRowSelect = true;
            this.lvMonsters.HideSelection = false;
            this.lvMonsters.Location = new System.Drawing.Point(0, 0);
            this.lvMonsters.Name = "lvMonsters";
            this.lvMonsters.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvMonsters.ShowItemToolTips = true;
            this.lvMonsters.Size = new System.Drawing.Size(604, 160);
            this.lvMonsters.SmallImageList = this.imageListMonsters;
            this.lvMonsters.TabIndex = 0;
            this.lvMonsters.UseCompatibleStateImageBehavior = false;
            this.lvMonsters.View = System.Windows.Forms.View.Details;
            this.lvMonsters.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMonsters_ColumnClick);
            this.lvMonsters.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvMonsters_ColumnReordered);
            this.lvMonsters.SelectedIndexChanged += new System.EventHandler(this.lvMonsters_SelectedIndexChanged);
            this.lvMonsters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMonsters_KeyDown);
            this.lvMonsters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMonsters_MouseDoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Monster Name";
            this.chName.Width = 100;
            // 
            // chProximity
            // 
            this.chProximity.Text = "Prox";
            this.chProximity.Width = 42;
            // 
            // chPosition
            // 
            this.chPosition.Text = "Pos";
            this.chPosition.Width = 41;
            // 
            // chHitPoints
            // 
            this.chHitPoints.Text = "HP";
            this.chHitPoints.Width = 33;
            // 
            // chArmorClass
            // 
            this.chArmorClass.Text = "AC";
            this.chArmorClass.Width = 27;
            // 
            // chDamage
            // 
            this.chDamage.Text = "Dmg";
            this.chDamage.Width = 85;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Spd";
            this.chSpeed.Width = 34;
            // 
            // chAccuracy
            // 
            this.chAccuracy.Text = "Acc";
            this.chAccuracy.Width = 38;
            // 
            // chResistances
            // 
            this.chResistances.Text = "Resistances";
            this.chResistances.Width = 114;
            // 
            // chSpecialPowers
            // 
            this.chSpecialPowers.Text = "Special";
            this.chSpecialPowers.Width = 203;
            // 
            // chExperience
            // 
            this.chExperience.Text = "Exp";
            this.chExperience.Width = 54;
            // 
            // chTreasure
            // 
            this.chTreasure.Text = "Treasure";
            // 
            // chTarget
            // 
            this.chTarget.Text = "Target";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.tbExtraText);
            this.splitContainer2.Panel2MinSize = 10;
            this.splitContainer2.Size = new System.Drawing.Size(604, 193);
            this.splitContainer2.SplitterDistance = 65;
            this.splitContainer2.TabIndex = 5;
            // 
            // ccl1
            // 
            this.ccl1.AutoSize = true;
            this.ccl1.CharName = "MAX_NAME_LENGTH";
            this.ccl1.HP = "9999";
            this.ccl1.Location = new System.Drawing.Point(4, 2);
            this.ccl1.Name = "ccl1";
            this.ccl1.Size = new System.Drawing.Size(285, 14);
            this.ccl1.SP = "9999";
            this.ccl1.TabIndex = 3;
            this.ccl1.ToolTip = "";
            // 
            // ccl2
            // 
            this.ccl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ccl2.AutoSize = true;
            this.ccl2.CharName = "MAX_NAME_LENGTH";
            this.ccl2.HP = "9999";
            this.ccl2.Location = new System.Drawing.Point(9, 2);
            this.ccl2.Name = "ccl2";
            this.ccl2.Size = new System.Drawing.Size(279, 14);
            this.ccl2.SP = "9999";
            this.ccl2.TabIndex = 3;
            this.ccl2.ToolTip = "";
            // 
            // ccl3
            // 
            this.ccl3.AutoSize = true;
            this.ccl3.CharName = "MAX_NAME_LENGTH";
            this.ccl3.HP = "9999";
            this.ccl3.Location = new System.Drawing.Point(4, 17);
            this.ccl3.Name = "ccl3";
            this.ccl3.Size = new System.Drawing.Size(285, 14);
            this.ccl3.SP = "9999";
            this.ccl3.TabIndex = 3;
            this.ccl3.ToolTip = "";
            // 
            // ccl5
            // 
            this.ccl5.AutoSize = true;
            this.ccl5.CharName = "MAX_NAME_LENGTH";
            this.ccl5.HP = "9999";
            this.ccl5.Location = new System.Drawing.Point(4, 32);
            this.ccl5.Name = "ccl5";
            this.ccl5.Size = new System.Drawing.Size(285, 14);
            this.ccl5.SP = "9999";
            this.ccl5.TabIndex = 3;
            this.ccl5.ToolTip = "";
            // 
            // ccl8
            // 
            this.ccl8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ccl8.AutoSize = true;
            this.ccl8.CharName = "MAX_NAME_LENGTH";
            this.ccl8.HP = "9999";
            this.ccl8.Location = new System.Drawing.Point(9, 47);
            this.ccl8.Name = "ccl8";
            this.ccl8.Size = new System.Drawing.Size(279, 14);
            this.ccl8.SP = "9999";
            this.ccl8.TabIndex = 3;
            this.ccl8.ToolTip = "";
            // 
            // ccl7
            // 
            this.ccl7.AutoSize = true;
            this.ccl7.CharName = "MAX_NAME_LENGTH";
            this.ccl7.HP = "9999";
            this.ccl7.Location = new System.Drawing.Point(4, 47);
            this.ccl7.Name = "ccl7";
            this.ccl7.Size = new System.Drawing.Size(285, 14);
            this.ccl7.SP = "9999";
            this.ccl7.TabIndex = 3;
            this.ccl7.ToolTip = "";
            // 
            // ccl6
            // 
            this.ccl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ccl6.AutoSize = true;
            this.ccl6.CharName = "MAX_NAME_LENGTH";
            this.ccl6.HP = "9999";
            this.ccl6.Location = new System.Drawing.Point(9, 32);
            this.ccl6.Name = "ccl6";
            this.ccl6.Size = new System.Drawing.Size(279, 14);
            this.ccl6.SP = "9999";
            this.ccl6.TabIndex = 3;
            this.ccl6.ToolTip = "";
            // 
            // ccl4
            // 
            this.ccl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ccl4.AutoSize = true;
            this.ccl4.CharName = "MAX_NAME_LENGTH";
            this.ccl4.HP = "9999";
            this.ccl4.Location = new System.Drawing.Point(9, 17);
            this.ccl4.Name = "ccl4";
            this.ccl4.Size = new System.Drawing.Size(279, 14);
            this.ccl4.SP = "9999";
            this.ccl4.TabIndex = 3;
            this.ccl4.ToolTip = "";
            // 
            // tbExtraText
            // 
            this.tbExtraText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbExtraText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExtraText.Location = new System.Drawing.Point(0, 0);
            this.tbExtraText.Multiline = true;
            this.tbExtraText.Name = "tbExtraText";
            this.tbExtraText.ReadOnly = true;
            this.tbExtraText.Size = new System.Drawing.Size(604, 124);
            this.tbExtraText.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer3.Panel1.Controls.Add(this.ccl1);
            this.splitContainer3.Panel1.Controls.Add(this.ccl7);
            this.splitContainer3.Panel1.Controls.Add(this.ccl3);
            this.splitContainer3.Panel1.Controls.Add(this.ccl5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer3.Panel2.Controls.Add(this.ccl2);
            this.splitContainer3.Panel2.Controls.Add(this.ccl4);
            this.splitContainer3.Panel2.Controls.Add(this.ccl6);
            this.splitContainer3.Panel2.Controls.Add(this.ccl8);
            this.splitContainer3.Size = new System.Drawing.Size(604, 65);
            this.splitContainer3.SplitterDistance = 300;
            this.splitContainer3.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(299, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 65);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1, 65);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // EncounterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 357);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctlTreasure);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "EncounterForm";
            this.Text = "Combat!  Round 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EncounterForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.EncounterForm_SizeChanged);
            this.cmMonsters.ResumeLayout(false);
            this.cmColumns.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WhereAreWe.DBListView lvMonsters;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timerRefreshInfo;
        private System.Windows.Forms.ColumnHeader chHitPoints;
        private System.Windows.Forms.ColumnHeader chArmorClass;
        private System.Windows.Forms.ColumnHeader chDamage;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chResistances;
        private System.Windows.Forms.ColumnHeader chSpecialPowers;
        private System.Windows.Forms.ColumnHeader chExperience;
        private System.Windows.Forms.ColumnHeader chTreasure;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CharCombatLabel ccl6;
        private CharCombatLabel ccl4;
        private CharCombatLabel ccl5;
        private CharCombatLabel ccl3;
        private CharCombatLabel ccl2;
        private CharCombatLabel ccl1;
        private TreasurePanel ctlTreasure;
        private System.Windows.Forms.Timer timerRefocus;
        private CharCombatLabel ccl8;
        private CharCombatLabel ccl7;
        private System.Windows.Forms.ImageList imageListMonsters;
        private System.Windows.Forms.ContextMenuStrip cmMonsters;
        private System.Windows.Forms.ToolStripMenuItem miMonsterEdit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ColumnHeader chPosition;
        private System.Windows.Forms.ColumnHeader chAccuracy;
        private System.Windows.Forms.ColumnHeader chTarget;
        private System.Windows.Forms.ContextMenuStrip cmColumns;
        private System.Windows.Forms.ToolStripMenuItem miColumnsName;
        private System.Windows.Forms.ToolStripMenuItem miColumnsPosition;
        private System.Windows.Forms.ToolStripMenuItem miColumnsHP;
        private System.Windows.Forms.ToolStripMenuItem miColumnsAC;
        private System.Windows.Forms.ToolStripMenuItem miColumnsDamage;
        private System.Windows.Forms.ToolStripMenuItem miColumnsSpeed;
        private System.Windows.Forms.ToolStripMenuItem miColumnsAccuracy;
        private System.Windows.Forms.ToolStripMenuItem miColumnsResistances;
        private System.Windows.Forms.ToolStripMenuItem miColumnsSpecial;
        private System.Windows.Forms.ToolStripMenuItem miColumnsExperience;
        private System.Windows.Forms.ToolStripMenuItem miColumnsTreasure;
        private System.Windows.Forms.ToolStripMenuItem miColumnsTarget;
        private System.Windows.Forms.ToolStripMenuItem miColumnsShowAll;
        private System.Windows.Forms.ToolStripMenuItem miMonsterShowOffMap;
        private System.Windows.Forms.ToolStripMenuItem miMonsterRemoveAll;
        private System.Windows.Forms.TextBox tbExtraText;
        private System.Windows.Forms.ToolStripMenuItem miMonsterTeleportParty;
        private System.Windows.Forms.ToolStripMenuItem miMonsterMoveMonster;
        private System.Windows.Forms.ColumnHeader chProximity;
        private System.Windows.Forms.ToolStripMenuItem miColumnsProximity;
        private System.Windows.Forms.ToolStripMenuItem miMonsterMonitorHP;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}