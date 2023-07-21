namespace WhereAreWe
{
    partial class MMCreationAssistantControl : CreationAssistantControl
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
            this.cmPrevious = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.useTheseRollsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteExcept = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLowerUnweighted = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLower = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miRemoveUnusable = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableKnight = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusablePaladin = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableArcher = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableCleric = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableSorcerer = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableRobber = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableNinja = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableBarbarian = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableRanger = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveUnusableDruid = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate100 = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate100k = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miCtxCopyRolls = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBonus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBonusCopyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmStats = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.miMinimum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miMaximizeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miDistributeEvenly = new System.Windows.Forms.ToolStripMenuItem();
            this.gbPreviousRolls = new System.Windows.Forms.GroupBox();
            this.lvPrevious = new WhereAreWe.DraggableListView();
            this.chUnweighted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWeighted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIntellect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPersonality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEndurance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAccuracy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLuck = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.panelWorking = new System.Windows.Forms.Panel();
            this.labelWorking = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelWeightBasedOn = new System.Windows.Forms.Label();
            this.labelWeightStats = new System.Windows.Forms.Label();
            this.labelWeightValues = new System.Windows.Forms.Label();
            this.gbBonusTable = new System.Windows.Forms.GroupBox();
            this.lvBonusTable = new WhereAreWe.DBListView();
            this.chBonusRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonusValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.comboAttrib = new System.Windows.Forms.ComboBox();
            this.btnDecrease = new System.Windows.Forms.Button();
            this.btnUpdateGameUI = new System.Windows.Forms.Button();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.labelExtraPoints = new System.Windows.Forms.Label();
            this.labelTotalWeighted = new System.Windows.Forms.Label();
            this.labelTotalRawPoints = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelHalfOrc = new System.Windows.Forms.Label();
            this.labelGnome = new System.Windows.Forms.Label();
            this.labelDwarf = new System.Windows.Forms.Label();
            this.labelElf = new System.Windows.Forms.Label();
            this.labelHuman = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelOnlyOnReroll = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvStats = new WhereAreWe.DBListView();
            this.chAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInvalidRolls = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmPrevious.SuspendLayout();
            this.cmBonus.SuspendLayout();
            this.cmStats.SuspendLayout();
            this.gbPreviousRolls.SuspendLayout();
            this.panelWorking.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbBonusTable.SuspendLayout();
            this.panelInvalidRolls.SuspendLayout();
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
            // cmPrevious
            // 
            this.cmPrevious.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useTheseRollsToolStripMenuItem,
            this.toolStripSeparator1,
            this.miDelete,
            this.miDeleteExcept,
            this.miDeleteLowerUnweighted,
            this.miDeleteLower,
            this.toolStripSeparator2,
            this.miRemoveUnusable,
            this.miGenerate100,
            this.miGenerate1000,
            this.miGenerate100k,
            this.toolStripSeparator3,
            this.miCtxCopyRolls});
            this.cmPrevious.Name = "cmPrevious";
            this.cmPrevious.ShowImageMargin = false;
            this.cmPrevious.Size = new System.Drawing.Size(261, 264);
            // 
            // useTheseRollsToolStripMenuItem
            // 
            this.useTheseRollsToolStripMenuItem.Name = "useTheseRollsToolStripMenuItem";
            this.useTheseRollsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.useTheseRollsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.useTheseRollsToolStripMenuItem.Text = "&Use these rolls";
            this.useTheseRollsToolStripMenuItem.Click += new System.EventHandler(this.useTheseRollsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miDelete.Size = new System.Drawing.Size(260, 22);
            this.miDelete.Text = "&Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miDeleteExcept
            // 
            this.miDeleteExcept.Name = "miDeleteExcept";
            this.miDeleteExcept.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.miDeleteExcept.Size = new System.Drawing.Size(260, 22);
            this.miDeleteExcept.Text = "Delete all &except";
            this.miDeleteExcept.Click += new System.EventHandler(this.miDeleteExcept_Click);
            // 
            // miDeleteLowerUnweighted
            // 
            this.miDeleteLowerUnweighted.Name = "miDeleteLowerUnweighted";
            this.miDeleteLowerUnweighted.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.miDeleteLowerUnweighted.Size = new System.Drawing.Size(260, 22);
            this.miDeleteLowerUnweighted.Text = "Delete &lower unweighted";
            this.miDeleteLowerUnweighted.Click += new System.EventHandler(this.miDeleteLowerUnweighted_Click);
            // 
            // miDeleteLower
            // 
            this.miDeleteLower.Name = "miDeleteLower";
            this.miDeleteLower.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.miDeleteLower.Size = new System.Drawing.Size(260, 22);
            this.miDeleteLower.Text = "Delete lower &weighted";
            this.miDeleteLower.Click += new System.EventHandler(this.miDeleteLower_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(257, 6);
            // 
            // miRemoveUnusable
            // 
            this.miRemoveUnusable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRemoveUnusableKnight,
            this.miRemoveUnusablePaladin,
            this.miRemoveUnusableArcher,
            this.miRemoveUnusableCleric,
            this.miRemoveUnusableSorcerer,
            this.miRemoveUnusableRobber,
            this.miRemoveUnusableNinja,
            this.miRemoveUnusableBarbarian,
            this.miRemoveUnusableRanger,
            this.miRemoveUnusableDruid});
            this.miRemoveUnusable.Name = "miRemoveUnusable";
            this.miRemoveUnusable.Size = new System.Drawing.Size(260, 22);
            this.miRemoveUnusable.Text = "&Remove unusable by";
            this.miRemoveUnusable.DropDownOpening += new System.EventHandler(this.miRemoveUnusable_DropDownOpening);
            // 
            // miRemoveUnusableKnight
            // 
            this.miRemoveUnusableKnight.Name = "miRemoveUnusableKnight";
            this.miRemoveUnusableKnight.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableKnight.Text = "&Knight";
            this.miRemoveUnusableKnight.Click += new System.EventHandler(this.miRemoveUnusableKnight_Click);
            // 
            // miRemoveUnusablePaladin
            // 
            this.miRemoveUnusablePaladin.Name = "miRemoveUnusablePaladin";
            this.miRemoveUnusablePaladin.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusablePaladin.Text = "&Paladin";
            this.miRemoveUnusablePaladin.Click += new System.EventHandler(this.miRemoveUnusablePaladin_Click);
            // 
            // miRemoveUnusableArcher
            // 
            this.miRemoveUnusableArcher.Name = "miRemoveUnusableArcher";
            this.miRemoveUnusableArcher.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableArcher.Text = "&Archer";
            this.miRemoveUnusableArcher.Click += new System.EventHandler(this.miRemoveUnusableArcher_Click);
            // 
            // miRemoveUnusableCleric
            // 
            this.miRemoveUnusableCleric.Name = "miRemoveUnusableCleric";
            this.miRemoveUnusableCleric.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableCleric.Text = "&Cleric";
            this.miRemoveUnusableCleric.Click += new System.EventHandler(this.miRemoveUnusableCleric_Click);
            // 
            // miRemoveUnusableSorcerer
            // 
            this.miRemoveUnusableSorcerer.Name = "miRemoveUnusableSorcerer";
            this.miRemoveUnusableSorcerer.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableSorcerer.Text = "&Sorcerer";
            this.miRemoveUnusableSorcerer.Click += new System.EventHandler(this.miRemoveUnusableSorcerer_Click);
            // 
            // miRemoveUnusableRobber
            // 
            this.miRemoveUnusableRobber.Name = "miRemoveUnusableRobber";
            this.miRemoveUnusableRobber.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableRobber.Text = "&Robber";
            this.miRemoveUnusableRobber.Click += new System.EventHandler(this.miRemoveUnusableRobber_Click);
            // 
            // miRemoveUnusableNinja
            // 
            this.miRemoveUnusableNinja.Name = "miRemoveUnusableNinja";
            this.miRemoveUnusableNinja.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableNinja.Text = "&Ninja";
            this.miRemoveUnusableNinja.Click += new System.EventHandler(this.miRemoveUnusableNinja_Click);
            // 
            // miRemoveUnusableBarbarian
            // 
            this.miRemoveUnusableBarbarian.Name = "miRemoveUnusableBarbarian";
            this.miRemoveUnusableBarbarian.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableBarbarian.Text = "&Barbarian";
            this.miRemoveUnusableBarbarian.Click += new System.EventHandler(this.miRemoveUnusableBarbarian_Click);
            // 
            // miRemoveUnusableRanger
            // 
            this.miRemoveUnusableRanger.Name = "miRemoveUnusableRanger";
            this.miRemoveUnusableRanger.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableRanger.Text = "Ran&ger";
            this.miRemoveUnusableRanger.Click += new System.EventHandler(this.miRemoveUnusableRanger_Click);
            // 
            // miRemoveUnusableDruid
            // 
            this.miRemoveUnusableDruid.Name = "miRemoveUnusableDruid";
            this.miRemoveUnusableDruid.Size = new System.Drawing.Size(124, 22);
            this.miRemoveUnusableDruid.Text = "&Druid";
            this.miRemoveUnusableDruid.Click += new System.EventHandler(this.miRemoveUnusableDruid_Click);
            // 
            // miGenerate100
            // 
            this.miGenerate100.Name = "miGenerate100";
            this.miGenerate100.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.miGenerate100.Size = new System.Drawing.Size(260, 22);
            this.miGenerate100.Text = "&Generate 100 rolls";
            this.miGenerate100.Click += new System.EventHandler(this.miGenerate100_Click);
            // 
            // miGenerate1000
            // 
            this.miGenerate1000.Name = "miGenerate1000";
            this.miGenerate1000.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.miGenerate1000.Size = new System.Drawing.Size(260, 22);
            this.miGenerate1000.Text = "&Generate 1000 rolls";
            this.miGenerate1000.Click += new System.EventHandler(this.miGenerate1000_Click);
            // 
            // miGenerate100k
            // 
            this.miGenerate100k.Name = "miGenerate100k";
            this.miGenerate100k.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.miGenerate100k.Size = new System.Drawing.Size(260, 22);
            this.miGenerate100k.Text = "&Generate 100,000 rolls";
            this.miGenerate100k.Click += new System.EventHandler(this.miGenerate100k_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(257, 6);
            // 
            // miCtxCopyRolls
            // 
            this.miCtxCopyRolls.Name = "miCtxCopyRolls";
            this.miCtxCopyRolls.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCtxCopyRolls.Size = new System.Drawing.Size(260, 22);
            this.miCtxCopyRolls.Text = "&Copy rolls";
            this.miCtxCopyRolls.Click += new System.EventHandler(this.miCtxCopyRolls_Click);
            // 
            // cmBonus
            // 
            this.cmBonus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBonusCopyTable});
            this.cmBonus.Name = "cmBonus";
            this.cmBonus.Size = new System.Drawing.Size(135, 26);
            // 
            // miBonusCopyTable
            // 
            this.miBonusCopyTable.Name = "miBonusCopyTable";
            this.miBonusCopyTable.Size = new System.Drawing.Size(134, 22);
            this.miBonusCopyTable.Text = "&Copy table ";
            this.miBonusCopyTable.Click += new System.EventHandler(this.miBonusCopyTable_Click);
            // 
            // cmStats
            // 
            this.cmStats.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMaximum,
            this.miMinimum,
            this.toolStripSeparator4,
            this.miMaximizeAll,
            this.miDistributeEvenly});
            this.cmStats.Name = "cmStats";
            this.cmStats.ShowImageMargin = false;
            this.cmStats.Size = new System.Drawing.Size(149, 98);
            // 
            // miMaximum
            // 
            this.miMaximum.Name = "miMaximum";
            this.miMaximum.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.miMaximum.Size = new System.Drawing.Size(148, 22);
            this.miMaximum.Text = "&Maximum";
            this.miMaximum.Click += new System.EventHandler(this.miMaximum_Click);
            // 
            // miMinimum
            // 
            this.miMinimum.Name = "miMinimum";
            this.miMinimum.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miMinimum.Size = new System.Drawing.Size(148, 22);
            this.miMinimum.Text = "Mi&nimum";
            this.miMinimum.Click += new System.EventHandler(this.miMinimum_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // miMaximizeAll
            // 
            this.miMaximizeAll.Name = "miMaximizeAll";
            this.miMaximizeAll.Size = new System.Drawing.Size(148, 22);
            this.miMaximizeAll.Text = "Minimize &all";
            this.miMaximizeAll.Click += new System.EventHandler(this.miMaximizeAll_Click);
            // 
            // miDistributeEvenly
            // 
            this.miDistributeEvenly.Name = "miDistributeEvenly";
            this.miDistributeEvenly.Size = new System.Drawing.Size(148, 22);
            this.miDistributeEvenly.Text = "&Distribute evenly";
            this.miDistributeEvenly.Click += new System.EventHandler(this.miDistributeEvenly_Click);
            // 
            // gbPreviousRolls
            // 
            this.gbPreviousRolls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreviousRolls.Controls.Add(this.lvPrevious);
            this.gbPreviousRolls.Controls.Add(this.btnOK);
            this.gbPreviousRolls.Controls.Add(this.panelWorking);
            this.gbPreviousRolls.Location = new System.Drawing.Point(1, 0);
            this.gbPreviousRolls.Name = "gbPreviousRolls";
            this.gbPreviousRolls.Size = new System.Drawing.Size(332, 292);
            this.gbPreviousRolls.TabIndex = 8;
            this.gbPreviousRolls.TabStop = false;
            this.gbPreviousRolls.Text = "Previous Rolls";
            // 
            // lvPrevious
            // 
            this.lvPrevious.AllowDrop = true;
            this.lvPrevious.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvPrevious.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUnweighted,
            this.chWeighted,
            this.chIntellect,
            this.chMight,
            this.chPersonality,
            this.chEndurance,
            this.chSpeed,
            this.chAccuracy,
            this.chLuck});
            this.lvPrevious.ContextMenuStrip = this.cmPrevious;
            this.lvPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrevious.FullRowSelect = true;
            this.lvPrevious.HideSelection = false;
            this.lvPrevious.Location = new System.Drawing.Point(3, 16);
            this.lvPrevious.Name = "lvPrevious";
            this.lvPrevious.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvPrevious.Size = new System.Drawing.Size(326, 273);
            this.lvPrevious.TabIndex = 0;
            this.lvPrevious.TipDelay = 700;
            this.lvPrevious.TipDuration = 30000;
            this.lvPrevious.UseCompatibleStateImageBehavior = false;
            this.lvPrevious.View = System.Windows.Forms.View.Details;
            this.lvPrevious.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPrevious_ColumnClick);
            this.lvPrevious.DoubleClick += new System.EventHandler(this.lvPrevious_DoubleClick);
            this.lvPrevious.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPrevious_KeyDown);
            // 
            // chUnweighted
            // 
            this.chUnweighted.Text = "Unw";
            this.chUnweighted.Width = 35;
            // 
            // chWeighted
            // 
            this.chWeighted.Text = "Wgt";
            this.chWeighted.Width = 34;
            // 
            // chIntellect
            // 
            this.chIntellect.Text = "INT";
            this.chIntellect.Width = 33;
            // 
            // chMight
            // 
            this.chMight.Text = "MGT";
            this.chMight.Width = 36;
            // 
            // chPersonality
            // 
            this.chPersonality.Text = "PER";
            this.chPersonality.Width = 35;
            // 
            // chEndurance
            // 
            this.chEndurance.Text = "END";
            this.chEndurance.Width = 35;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "SPD";
            this.chSpeed.Width = 34;
            // 
            // chAccuracy
            // 
            this.chAccuracy.Text = "ACY";
            this.chAccuracy.Width = 35;
            // 
            // chLuck
            // 
            this.chLuck.Text = "LUC";
            this.chLuck.Width = 35;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(14, 20);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panelWorking
            // 
            this.panelWorking.Controls.Add(this.labelWorking);
            this.panelWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorking.Location = new System.Drawing.Point(3, 16);
            this.panelWorking.Name = "panelWorking";
            this.panelWorking.Size = new System.Drawing.Size(326, 273);
            this.panelWorking.TabIndex = 2;
            this.panelWorking.Visible = false;
            // 
            // labelWorking
            // 
            this.labelWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWorking.Location = new System.Drawing.Point(0, 0);
            this.labelWorking.Name = "labelWorking";
            this.labelWorking.Size = new System.Drawing.Size(326, 273);
            this.labelWorking.TabIndex = 0;
            this.labelWorking.Text = "Working...";
            this.labelWorking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelWeightBasedOn);
            this.groupBox2.Controls.Add(this.labelWeightStats);
            this.groupBox2.Controls.Add(this.labelWeightValues);
            this.groupBox2.Location = new System.Drawing.Point(339, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 133);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Weighted Points";
            // 
            // labelWeightBasedOn
            // 
            this.labelWeightBasedOn.AutoSize = true;
            this.labelWeightBasedOn.Location = new System.Drawing.Point(8, 16);
            this.labelWeightBasedOn.Name = "labelWeightBasedOn";
            this.labelWeightBasedOn.Size = new System.Drawing.Size(78, 13);
            this.labelWeightBasedOn.TabIndex = 3;
            this.labelWeightBasedOn.Text = "(based on 3d6)";
            // 
            // labelWeightStats
            // 
            this.labelWeightStats.Location = new System.Drawing.Point(20, 32);
            this.labelWeightStats.Name = "labelWeightStats";
            this.labelWeightStats.Size = new System.Drawing.Size(59, 96);
            this.labelWeightStats.TabIndex = 3;
            this.labelWeightStats.Text = "Up to 13\r\n13 to 14\r\n14 to 15\r\n15 to 16\r\n16 to 17\r\n17 to 18";
            this.labelWeightStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWeightValues
            // 
            this.labelWeightValues.Location = new System.Drawing.Point(77, 33);
            this.labelWeightValues.Name = "labelWeightValues";
            this.labelWeightValues.Size = new System.Drawing.Size(24, 95);
            this.labelWeightValues.TabIndex = 3;
            this.labelWeightValues.Text = "1\r\n3\r\n6\r\n10\r\n15\r\n21";
            this.labelWeightValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbBonusTable
            // 
            this.gbBonusTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBonusTable.Controls.Add(this.lvBonusTable);
            this.gbBonusTable.Controls.Add(this.label3);
            this.gbBonusTable.Controls.Add(this.comboAttrib);
            this.gbBonusTable.Location = new System.Drawing.Point(473, 159);
            this.gbBonusTable.Name = "gbBonusTable";
            this.gbBonusTable.Size = new System.Drawing.Size(149, 208);
            this.gbBonusTable.TabIndex = 30;
            this.gbBonusTable.TabStop = false;
            this.gbBonusTable.Text = "Bonus Table";
            // 
            // lvBonusTable
            // 
            this.lvBonusTable.AllowDrop = true;
            this.lvBonusTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBonusTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBonusRange,
            this.chBonusValue});
            this.lvBonusTable.ContextMenuStrip = this.cmBonus;
            this.lvBonusTable.FullRowSelect = true;
            this.lvBonusTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBonusTable.HideSelection = false;
            this.lvBonusTable.Location = new System.Drawing.Point(2, 13);
            this.lvBonusTable.MultiSelect = false;
            this.lvBonusTable.Name = "lvBonusTable";
            this.lvBonusTable.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBonusTable.ShowItemToolTips = true;
            this.lvBonusTable.Size = new System.Drawing.Size(144, 168);
            this.lvBonusTable.TabIndex = 0;
            this.lvBonusTable.UseCompatibleStateImageBehavior = false;
            this.lvBonusTable.View = System.Windows.Forms.View.Details;
            // 
            // chBonusRange
            // 
            this.chBonusRange.Text = "Range";
            this.chBonusRange.Width = 62;
            // 
            // chBonusValue
            // 
            this.chBonusValue.Text = "Bonus";
            this.chBonusValue.Width = 42;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Attrib";
            // 
            // comboAttrib
            // 
            this.comboAttrib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAttrib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttrib.FormattingEnabled = true;
            this.comboAttrib.Items.AddRange(new object[] {
            "Might",
            "Intellect",
            "Personality",
            "Endurance",
            "Speed",
            "Accuracy",
            "Luck"});
            this.comboAttrib.Location = new System.Drawing.Point(40, 183);
            this.comboAttrib.Name = "comboAttrib";
            this.comboAttrib.Size = new System.Drawing.Size(106, 21);
            this.comboAttrib.TabIndex = 6;
            this.comboAttrib.SelectedIndexChanged += new System.EventHandler(this.comboAttrib_SelectedIndexChanged);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecrease.Location = new System.Drawing.Point(469, 3);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(61, 23);
            this.btnDecrease.TabIndex = 26;
            this.btnDecrease.Text = "&Decrease";
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnUpdateGameUI
            // 
            this.btnUpdateGameUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateGameUI.Location = new System.Drawing.Point(469, 68);
            this.btnUpdateGameUI.Name = "btnUpdateGameUI";
            this.btnUpdateGameUI.Size = new System.Drawing.Size(129, 23);
            this.btnUpdateGameUI.TabIndex = 29;
            this.btnUpdateGameUI.Text = "&Update Game UI";
            this.btnUpdateGameUI.UseVisualStyleBackColor = true;
            this.btnUpdateGameUI.Click += new System.EventHandler(this.btnUpdateGameUI_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncrease.Location = new System.Drawing.Point(537, 3);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(61, 23);
            this.btnIncrease.TabIndex = 28;
            this.btnIncrease.Text = "&Increase";
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // labelExtraPoints
            // 
            this.labelExtraPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExtraPoints.AutoSize = true;
            this.labelExtraPoints.Location = new System.Drawing.Point(590, 97);
            this.labelExtraPoints.Name = "labelExtraPoints";
            this.labelExtraPoints.Size = new System.Drawing.Size(13, 13);
            this.labelExtraPoints.TabIndex = 25;
            this.labelExtraPoints.Text = "0";
            // 
            // labelTotalWeighted
            // 
            this.labelTotalWeighted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalWeighted.AutoSize = true;
            this.labelTotalWeighted.Location = new System.Drawing.Point(590, 139);
            this.labelTotalWeighted.Name = "labelTotalWeighted";
            this.labelTotalWeighted.Size = new System.Drawing.Size(13, 13);
            this.labelTotalWeighted.TabIndex = 24;
            this.labelTotalWeighted.Text = "0";
            // 
            // labelTotalRawPoints
            // 
            this.labelTotalRawPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalRawPoints.AutoSize = true;
            this.labelTotalRawPoints.Location = new System.Drawing.Point(590, 124);
            this.labelTotalRawPoints.Name = "labelTotalRawPoints";
            this.labelTotalRawPoints.Size = new System.Drawing.Size(13, 13);
            this.labelTotalRawPoints.TabIndex = 23;
            this.labelTotalRawPoints.Text = "0";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Half-Orc:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 337);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Gnome:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Dwarf:";
            // 
            // labelHalfOrc
            // 
            this.labelHalfOrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHalfOrc.AutoSize = true;
            this.labelHalfOrc.Location = new System.Drawing.Point(50, 351);
            this.labelHalfOrc.Name = "labelHalfOrc";
            this.labelHalfOrc.Size = new System.Drawing.Size(60, 13);
            this.labelHalfOrc.TabIndex = 20;
            this.labelHalfOrc.Text = "HALF-ORC";
            // 
            // labelGnome
            // 
            this.labelGnome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelGnome.AutoSize = true;
            this.labelGnome.Location = new System.Drawing.Point(50, 337);
            this.labelGnome.Name = "labelGnome";
            this.labelGnome.Size = new System.Drawing.Size(47, 13);
            this.labelGnome.TabIndex = 19;
            this.labelGnome.Text = "GNOME";
            // 
            // labelDwarf
            // 
            this.labelDwarf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDwarf.AutoSize = true;
            this.labelDwarf.Location = new System.Drawing.Point(50, 323);
            this.labelDwarf.Name = "labelDwarf";
            this.labelDwarf.Size = new System.Drawing.Size(47, 13);
            this.labelDwarf.TabIndex = 18;
            this.labelDwarf.Text = "DWARF";
            // 
            // labelElf
            // 
            this.labelElf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelElf.AutoSize = true;
            this.labelElf.Location = new System.Drawing.Point(50, 309);
            this.labelElf.Name = "labelElf";
            this.labelElf.Size = new System.Drawing.Size(26, 13);
            this.labelElf.TabIndex = 17;
            this.labelElf.Text = "ELF";
            // 
            // labelHuman
            // 
            this.labelHuman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHuman.AutoSize = true;
            this.labelHuman.Location = new System.Drawing.Point(50, 295);
            this.labelHuman.Name = "labelHuman";
            this.labelHuman.Size = new System.Drawing.Size(47, 13);
            this.labelHuman.TabIndex = 16;
            this.labelHuman.Text = "HUMAN";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Elf:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Human:";
            // 
            // labelOnlyOnReroll
            // 
            this.labelOnlyOnReroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOnlyOnReroll.Location = new System.Drawing.Point(469, 31);
            this.labelOnlyOnReroll.Name = "labelOnlyOnReroll";
            this.labelOnlyOnReroll.Size = new System.Drawing.Size(141, 31);
            this.labelOnlyOnReroll.TabIndex = 10;
            this.labelOnlyOnReroll.Text = "Changes can only be made on the reroll screen!";
            this.labelOnlyOnReroll.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(470, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Extra Weighted Points:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(470, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Total Weighted Points:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(470, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Total Raw Points:";
            // 
            // lvStats
            // 
            this.lvStats.AllowDrop = true;
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAttribute,
            this.columnHeader1});
            this.lvStats.ContextMenuStrip = this.cmStats;
            this.lvStats.FullRowSelect = true;
            this.lvStats.HideSelection = false;
            this.lvStats.Location = new System.Drawing.Point(339, 3);
            this.lvStats.MultiSelect = false;
            this.lvStats.Name = "lvStats";
            this.lvStats.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvStats.ShowItemToolTips = true;
            this.lvStats.Size = new System.Drawing.Size(124, 150);
            this.lvStats.TabIndex = 9;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            this.lvStats.SelectedIndexChanged += new System.EventHandler(this.lvStats_SelectedIndexChanged);
            this.lvStats.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvStats_KeyDown);
            // 
            // chAttribute
            // 
            this.chAttribute.Text = "Attribute";
            this.chAttribute.Width = 75;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Value";
            this.columnHeader1.Width = 45;
            // 
            // panelInvalidRolls
            // 
            this.panelInvalidRolls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInvalidRolls.Controls.Add(this.label20);
            this.panelInvalidRolls.Controls.Add(this.label14);
            this.panelInvalidRolls.Location = new System.Drawing.Point(339, 3);
            this.panelInvalidRolls.Name = "panelInvalidRolls";
            this.panelInvalidRolls.Size = new System.Drawing.Size(124, 150);
            this.panelInvalidRolls.TabIndex = 32;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Location = new System.Drawing.Point(13, 45);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(108, 55);
            this.label20.TabIndex = 3;
            this.label20.Text = "Please ensure that you are on the character creation screen!";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Invalid rolls obtained from game!";
            // 
            // MMCreationAssistantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.gbPreviousRolls);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbBonusTable);
            this.Controls.Add(this.btnDecrease);
            this.Controls.Add(this.btnUpdateGameUI);
            this.Controls.Add(this.btnIncrease);
            this.Controls.Add(this.labelExtraPoints);
            this.Controls.Add(this.labelTotalWeighted);
            this.Controls.Add(this.labelTotalRawPoints);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelHalfOrc);
            this.Controls.Add(this.labelGnome);
            this.Controls.Add(this.labelDwarf);
            this.Controls.Add(this.labelElf);
            this.Controls.Add(this.labelHuman);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelOnlyOnReroll);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvStats);
            this.Controls.Add(this.panelInvalidRolls);
            this.MinimumSize = new System.Drawing.Size(624, 367);
            this.Name = "MMCreationAssistantControl";
            this.Size = new System.Drawing.Size(624, 367);
            this.cmPrevious.ResumeLayout(false);
            this.cmBonus.ResumeLayout(false);
            this.cmStats.ResumeLayout(false);
            this.gbPreviousRolls.ResumeLayout(false);
            this.panelWorking.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbBonusTable.ResumeLayout(false);
            this.gbBonusTable.PerformLayout();
            this.panelInvalidRolls.ResumeLayout(false);
            this.panelInvalidRolls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chExit;
        private System.Windows.Forms.ColumnHeader chMap;
        private System.Windows.Forms.ColumnHeader chActiveEffect;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.GroupBox gbPreviousRolls;
        private DraggableListView lvPrevious;
        private System.Windows.Forms.ColumnHeader chUnweighted;
        private System.Windows.Forms.ColumnHeader chWeighted;
        private System.Windows.Forms.ColumnHeader chIntellect;
        private System.Windows.Forms.ColumnHeader chMight;
        private System.Windows.Forms.ColumnHeader chPersonality;
        private System.Windows.Forms.ColumnHeader chEndurance;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chAccuracy;
        private System.Windows.Forms.ColumnHeader chLuck;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelWorking;
        private System.Windows.Forms.Label labelWorking;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelWeightBasedOn;
        private System.Windows.Forms.Label labelWeightStats;
        private System.Windows.Forms.Label labelWeightValues;
        private System.Windows.Forms.GroupBox gbBonusTable;
        private DBListView lvBonusTable;
        private System.Windows.Forms.ColumnHeader chBonusRange;
        private System.Windows.Forms.ColumnHeader chBonusValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboAttrib;
        private System.Windows.Forms.Button btnDecrease;
        private System.Windows.Forms.Button btnUpdateGameUI;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.Label labelExtraPoints;
        private System.Windows.Forms.Label labelTotalWeighted;
        private System.Windows.Forms.Label labelTotalRawPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelHalfOrc;
        private System.Windows.Forms.Label labelGnome;
        private System.Windows.Forms.Label labelDwarf;
        private System.Windows.Forms.Label labelElf;
        private System.Windows.Forms.Label labelHuman;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelOnlyOnReroll;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private DBListView lvStats;
        private System.Windows.Forms.ColumnHeader chAttribute;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panelInvalidRolls;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ContextMenuStrip cmPrevious;
        private System.Windows.Forms.ToolStripMenuItem useTheseRollsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miDeleteExcept;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLowerUnweighted;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLower;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusable;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableKnight;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusablePaladin;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableArcher;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableCleric;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableSorcerer;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableRobber;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableNinja;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableBarbarian;
        private System.Windows.Forms.ToolStripMenuItem miGenerate100;
        private System.Windows.Forms.ToolStripMenuItem miGenerate100k;
        private System.Windows.Forms.ContextMenuStrip cmBonus;
        private System.Windows.Forms.ToolStripMenuItem miBonusCopyTable;
        private System.Windows.Forms.ContextMenuStrip cmStats;
        private System.Windows.Forms.ToolStripMenuItem miMaximum;
        private System.Windows.Forms.ToolStripMenuItem miMinimum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopyRolls;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miMaximizeAll;
        private System.Windows.Forms.ToolStripMenuItem miDistributeEvenly;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableRanger;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableDruid;
        private System.Windows.Forms.ToolStripMenuItem miGenerate1000;
    }
}
