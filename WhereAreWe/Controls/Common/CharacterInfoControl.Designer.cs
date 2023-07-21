namespace WhereAreWe
{
    partial class CharacterInfoControl
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
            this.cmBackpack = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBackpackTrade = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackBagofHolding = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackFillRandom = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackDropTrash = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackDebugMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackDebugClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackItemDisplayFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackStackCharges = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCheat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCheatSubtract1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatAdd1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatMinimum = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatNextLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheatCreateSupercharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.scInventory = new System.Windows.Forms.SplitContainer();
            this.lvEquipped = new WhereAreWe.DBListView();
            this.chEquipped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvBackpack = new WhereAreWe.DBListView();
            this.chBackpackItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scCharQuickref = new System.Windows.Forms.SplitContainer();
            this.scStatsResistances = new System.Windows.Forms.SplitContainer();
            this.labelLevel = new WhereAreWe.EditableAttributeLabel();
            this.llCureAll = new System.Windows.Forms.LinkLabel();
            this.labelSP = new WhereAreWe.EditableAttributeLabel();
            this.llQuests = new System.Windows.Forms.LinkLabel();
            this.labelHP = new WhereAreWe.EditableAttributeLabel();
            this.labelConditionHeader = new System.Windows.Forms.Label();
            this.labelSPHeader = new System.Windows.Forms.Label();
            this.labelMelee = new WhereAreWe.EditableAttributeLabel();
            this.labelHPHeader = new System.Windows.Forms.Label();
            this.labelMeleeHeader = new System.Windows.Forms.Label();
            this.labelAC = new WhereAreWe.EditableAttributeLabel();
            this.label25 = new System.Windows.Forms.Label();
            this.labelACHeader = new System.Windows.Forms.Label();
            this.labelExpHeader = new System.Windows.Forms.Label();
            this.labelCondition = new WhereAreWe.EditableAttributeLabel();
            this.labelExp = new WhereAreWe.EditableAttributeLabel();
            this.lvResistances = new WhereAreWe.TipListView();
            this.chResistance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResistValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmBackpack.SuspendLayout();
            this.cmCheat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scInventory)).BeginInit();
            this.scInventory.Panel1.SuspendLayout();
            this.scInventory.Panel2.SuspendLayout();
            this.scInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCharQuickref)).BeginInit();
            this.scCharQuickref.Panel1.SuspendLayout();
            this.scCharQuickref.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scStatsResistances)).BeginInit();
            this.scStatsResistances.Panel1.SuspendLayout();
            this.scStatsResistances.Panel2.SuspendLayout();
            this.scStatsResistances.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmBackpack
            // 
            this.cmBackpack.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBackpackTrade,
            this.miBackpackBagofHolding,
            this.miBackpackFillRandom,
            this.miBackpackEdit,
            this.miBackpackAdd,
            this.miBackpackDuplicate,
            this.miBackpackClearAll,
            this.miBackpackDeleteItem,
            this.miBackpackDropTrash,
            this.miBackpackDebugMonitor,
            this.miBackpackDebugClearAll,
            this.miBackpackItemDisplayFormat,
            this.miBackpackStackCharges});
            this.cmBackpack.Name = "cmBackpack";
            this.cmBackpack.ShowCheckMargin = true;
            this.cmBackpack.ShowImageMargin = false;
            this.cmBackpack.Size = new System.Drawing.Size(216, 290);
            this.cmBackpack.Opening += new System.ComponentModel.CancelEventHandler(this.cmBackpack_Opening);
            // 
            // miBackpackTrade
            // 
            this.miBackpackTrade.Name = "miBackpackTrade";
            this.miBackpackTrade.Size = new System.Drawing.Size(215, 22);
            this.miBackpackTrade.Text = "&Trade backpacks with...";
            // 
            // miBackpackBagofHolding
            // 
            this.miBackpackBagofHolding.Name = "miBackpackBagofHolding";
            this.miBackpackBagofHolding.Size = new System.Drawing.Size(215, 22);
            this.miBackpackBagofHolding.Text = "&Bag of holding";
            this.miBackpackBagofHolding.Click += new System.EventHandler(this.miBackpackBagofHolding_Click);
            // 
            // miBackpackFillRandom
            // 
            this.miBackpackFillRandom.Name = "miBackpackFillRandom";
            this.miBackpackFillRandom.Size = new System.Drawing.Size(215, 22);
            this.miBackpackFillRandom.Text = "&Fill with random items";
            this.miBackpackFillRandom.Click += new System.EventHandler(this.miBackpackFillRandom_Click);
            // 
            // miBackpackEdit
            // 
            this.miBackpackEdit.Name = "miBackpackEdit";
            this.miBackpackEdit.Size = new System.Drawing.Size(215, 22);
            this.miBackpackEdit.Text = "&Edit";
            this.miBackpackEdit.Click += new System.EventHandler(this.miBackpackEdit_Click);
            // 
            // miBackpackAdd
            // 
            this.miBackpackAdd.Name = "miBackpackAdd";
            this.miBackpackAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miBackpackAdd.ShowShortcutKeys = false;
            this.miBackpackAdd.Size = new System.Drawing.Size(215, 22);
            this.miBackpackAdd.Text = "&Add";
            this.miBackpackAdd.Click += new System.EventHandler(this.miBackpackAdd_Click);
            // 
            // miBackpackDuplicate
            // 
            this.miBackpackDuplicate.Name = "miBackpackDuplicate";
            this.miBackpackDuplicate.Size = new System.Drawing.Size(215, 22);
            this.miBackpackDuplicate.Text = "D&uplicate";
            this.miBackpackDuplicate.Click += new System.EventHandler(this.miBackpackDuplicate_Click);
            // 
            // miBackpackClearAll
            // 
            this.miBackpackClearAll.Name = "miBackpackClearAll";
            this.miBackpackClearAll.Size = new System.Drawing.Size(215, 22);
            this.miBackpackClearAll.Text = "&Clear all items";
            this.miBackpackClearAll.Click += new System.EventHandler(this.miBackpackClearAll_Click);
            // 
            // miBackpackDeleteItem
            // 
            this.miBackpackDeleteItem.Name = "miBackpackDeleteItem";
            this.miBackpackDeleteItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miBackpackDeleteItem.ShowShortcutKeys = false;
            this.miBackpackDeleteItem.Size = new System.Drawing.Size(215, 22);
            this.miBackpackDeleteItem.Text = "&Delete item";
            this.miBackpackDeleteItem.Click += new System.EventHandler(this.miBackpackDeleteItem_Click);
            // 
            // miBackpackDropTrash
            // 
            this.miBackpackDropTrash.Name = "miBackpackDropTrash";
            this.miBackpackDropTrash.Size = new System.Drawing.Size(215, 22);
            this.miBackpackDropTrash.Text = "D&rop trash...";
            this.miBackpackDropTrash.Click += new System.EventHandler(this.miBackpackDropTrash_Click);
            // 
            // miBackpackDebugMonitor
            // 
            this.miBackpackDebugMonitor.Name = "miBackpackDebugMonitor";
            this.miBackpackDebugMonitor.Size = new System.Drawing.Size(215, 22);
            this.miBackpackDebugMonitor.Text = "Debug: Monitor backpack";
            this.miBackpackDebugMonitor.Click += new System.EventHandler(this.miBackpackDebugMonitor_Click);
            // 
            // miBackpackDebugClearAll
            // 
            this.miBackpackDebugClearAll.Name = "miBackpackDebugClearAll";
            this.miBackpackDebugClearAll.Size = new System.Drawing.Size(215, 22);
            this.miBackpackDebugClearAll.Text = "Debug: Clear all backpacks";
            this.miBackpackDebugClearAll.Click += new System.EventHandler(this.miBackpackDebugClearAll_Click);
            // 
            // miBackpackItemDisplayFormat
            // 
            this.miBackpackItemDisplayFormat.Name = "miBackpackItemDisplayFormat";
            this.miBackpackItemDisplayFormat.Size = new System.Drawing.Size(215, 22);
            this.miBackpackItemDisplayFormat.Text = "&Item display format";
            this.miBackpackItemDisplayFormat.Click += new System.EventHandler(this.miBackpackItemDisplayFormat_Click);
            // 
            // miBackpackStackCharges
            // 
            this.miBackpackStackCharges.Name = "miBackpackStackCharges";
            this.miBackpackStackCharges.Size = new System.Drawing.Size(215, 22);
            this.miBackpackStackCharges.Text = "&Stack charges";
            this.miBackpackStackCharges.Click += new System.EventHandler(this.miBackpackStackCharges_Click);
            // 
            // cmCheat
            // 
            this.cmCheat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCheatSubtract1,
            this.miCheatAdd1,
            this.miCheatMinimum,
            this.miCheatMaximum,
            this.miCheatNextLevel,
            this.miCheatEdit,
            this.miCheatCreateSupercharacter});
            this.cmCheat.Name = "cmCheat";
            this.cmCheat.ShowImageMargin = false;
            this.cmCheat.Size = new System.Drawing.Size(166, 158);
            this.cmCheat.Opening += new System.ComponentModel.CancelEventHandler(this.cmCheat_Opening);
            // 
            // miCheatSubtract1
            // 
            this.miCheatSubtract1.Name = "miCheatSubtract1";
            this.miCheatSubtract1.Size = new System.Drawing.Size(165, 22);
            this.miCheatSubtract1.Text = "&Subtract 1";
            this.miCheatSubtract1.Click += new System.EventHandler(this.miCheatSubtract1_Click);
            // 
            // miCheatAdd1
            // 
            this.miCheatAdd1.Name = "miCheatAdd1";
            this.miCheatAdd1.Size = new System.Drawing.Size(165, 22);
            this.miCheatAdd1.Text = "&Add 1";
            this.miCheatAdd1.Click += new System.EventHandler(this.miCheatAdd1_Click);
            // 
            // miCheatMinimum
            // 
            this.miCheatMinimum.Name = "miCheatMinimum";
            this.miCheatMinimum.Size = new System.Drawing.Size(165, 22);
            this.miCheatMinimum.Text = "Mi&nimum";
            this.miCheatMinimum.Click += new System.EventHandler(this.miCheatMinimum_Click);
            // 
            // miCheatMaximum
            // 
            this.miCheatMaximum.Name = "miCheatMaximum";
            this.miCheatMaximum.Size = new System.Drawing.Size(165, 22);
            this.miCheatMaximum.Text = "&Maximum";
            this.miCheatMaximum.Click += new System.EventHandler(this.miCheatMaximum_Click);
            // 
            // miCheatNextLevel
            // 
            this.miCheatNextLevel.Name = "miCheatNextLevel";
            this.miCheatNextLevel.Size = new System.Drawing.Size(165, 22);
            this.miCheatNextLevel.Text = "Next &Level";
            this.miCheatNextLevel.Click += new System.EventHandler(this.miCheatNextLevel_Click);
            // 
            // miCheatEdit
            // 
            this.miCheatEdit.Name = "miCheatEdit";
            this.miCheatEdit.Size = new System.Drawing.Size(165, 22);
            this.miCheatEdit.Text = "&Edit";
            this.miCheatEdit.Click += new System.EventHandler(this.miCheatEdit_Click);
            // 
            // miCheatCreateSupercharacter
            // 
            this.miCheatCreateSupercharacter.Name = "miCheatCreateSupercharacter";
            this.miCheatCreateSupercharacter.Size = new System.Drawing.Size(165, 22);
            this.miCheatCreateSupercharacter.Text = "&Create Supercharacter";
            this.miCheatCreateSupercharacter.Click += new System.EventHandler(this.miCheatCreateSupercharacter_Click);
            // 
            // scInventory
            // 
            this.scInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scInventory.IsSplitterFixed = true;
            this.scInventory.Location = new System.Drawing.Point(0, 194);
            this.scInventory.Name = "scInventory";
            // 
            // scInventory.Panel1
            // 
            this.scInventory.Panel1.Controls.Add(this.lvEquipped);
            // 
            // scInventory.Panel2
            // 
            this.scInventory.Panel2.Controls.Add(this.lvBackpack);
            this.scInventory.Size = new System.Drawing.Size(953, 119);
            this.scInventory.SplitterDistance = 483;
            this.scInventory.TabIndex = 33;
            // 
            // lvEquipped
            // 
            this.lvEquipped.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEquipped});
            this.lvEquipped.ContextMenuStrip = this.cmBackpack;
            this.lvEquipped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEquipped.FullRowSelect = true;
            this.lvEquipped.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvEquipped.Location = new System.Drawing.Point(0, 0);
            this.lvEquipped.MultiSelect = false;
            this.lvEquipped.Name = "lvEquipped";
            this.lvEquipped.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvEquipped.ShowItemToolTips = true;
            this.lvEquipped.Size = new System.Drawing.Size(483, 119);
            this.lvEquipped.TabIndex = 0;
            this.lvEquipped.UseCompatibleStateImageBehavior = false;
            this.lvEquipped.View = System.Windows.Forms.View.Details;
            this.lvEquipped.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvEquipped_KeyDown);
            this.lvEquipped.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvEquipped_MouseDoubleClick);
            // 
            // chEquipped
            // 
            this.chEquipped.Text = "Equipped Items";
            this.chEquipped.Width = 411;
            // 
            // lvBackpack
            // 
            this.lvBackpack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBackpackItems});
            this.lvBackpack.ContextMenuStrip = this.cmBackpack;
            this.lvBackpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBackpack.FullRowSelect = true;
            this.lvBackpack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBackpack.Location = new System.Drawing.Point(0, 0);
            this.lvBackpack.MultiSelect = false;
            this.lvBackpack.Name = "lvBackpack";
            this.lvBackpack.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBackpack.ShowItemToolTips = true;
            this.lvBackpack.Size = new System.Drawing.Size(466, 119);
            this.lvBackpack.TabIndex = 0;
            this.lvBackpack.UseCompatibleStateImageBehavior = false;
            this.lvBackpack.View = System.Windows.Forms.View.Details;
            this.lvBackpack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBackpack_KeyDown);
            this.lvBackpack.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBackpack_MouseDoubleClick);
            // 
            // chBackpackItems
            // 
            this.chBackpackItems.Text = "Backpack Items";
            this.chBackpackItems.Width = 412;
            // 
            // scCharQuickref
            // 
            this.scCharQuickref.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scCharQuickref.Location = new System.Drawing.Point(0, 0);
            this.scCharQuickref.Name = "scCharQuickref";
            // 
            // scCharQuickref.Panel1
            // 
            this.scCharQuickref.Panel1.Controls.Add(this.scStatsResistances);
            this.scCharQuickref.Panel1MinSize = 337;
            this.scCharQuickref.Size = new System.Drawing.Size(953, 195);
            this.scCharQuickref.SplitterDistance = 485;
            this.scCharQuickref.TabIndex = 34;
            this.scCharQuickref.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scCharQuickref_SplitterMoved);
            // 
            // scStatsResistances
            // 
            this.scStatsResistances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scStatsResistances.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scStatsResistances.Location = new System.Drawing.Point(0, 0);
            this.scStatsResistances.Name = "scStatsResistances";
            // 
            // scStatsResistances.Panel1
            // 
            this.scStatsResistances.Panel1.Controls.Add(this.labelLevel);
            this.scStatsResistances.Panel1.Controls.Add(this.llCureAll);
            this.scStatsResistances.Panel1.Controls.Add(this.labelSP);
            this.scStatsResistances.Panel1.Controls.Add(this.llQuests);
            this.scStatsResistances.Panel1.Controls.Add(this.labelHP);
            this.scStatsResistances.Panel1.Controls.Add(this.labelConditionHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelSPHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelMelee);
            this.scStatsResistances.Panel1.Controls.Add(this.labelHPHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelMeleeHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelAC);
            this.scStatsResistances.Panel1.Controls.Add(this.label25);
            this.scStatsResistances.Panel1.Controls.Add(this.labelACHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelExpHeader);
            this.scStatsResistances.Panel1.Controls.Add(this.labelCondition);
            this.scStatsResistances.Panel1.Controls.Add(this.labelExp);
            // 
            // scStatsResistances.Panel2
            // 
            this.scStatsResistances.Panel2.Controls.Add(this.lvResistances);
            this.scStatsResistances.Size = new System.Drawing.Size(485, 195);
            this.scStatsResistances.SplitterDistance = 359;
            this.scStatsResistances.TabIndex = 38;
            this.scStatsResistances.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scStatsResistances_SplitterMoved);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(0, 2);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(176, 13);
            this.labelLevel.TabIndex = 21;
            this.labelLevel.Text = "LEVEL/SEX/ALIGN/RACE/CLASS";
            // 
            // llCureAll
            // 
            this.llCureAll.AutoSize = true;
            this.llCureAll.Location = new System.Drawing.Point(253, 125);
            this.llCureAll.Name = "llCureAll";
            this.llCureAll.Size = new System.Drawing.Size(43, 13);
            this.llCureAll.TabIndex = 35;
            this.llCureAll.TabStop = true;
            this.llCureAll.Text = "Cure All";
            this.llCureAll.Click += new System.EventHandler(this.btnCureAll_Click);
            // 
            // labelSP
            // 
            this.labelSP.AutoSize = true;
            this.labelSP.Location = new System.Drawing.Point(245, 49);
            this.labelSP.Name = "labelSP";
            this.labelSP.Size = new System.Drawing.Size(21, 13);
            this.labelSP.TabIndex = 27;
            this.labelSP.Text = "SP";
            this.labelSP.MouseEnter += new System.EventHandler(this.labelSP_MouseEnter);
            // 
            // llQuests
            // 
            this.llQuests.AutoSize = true;
            this.llQuests.Location = new System.Drawing.Point(295, 125);
            this.llQuests.Name = "llQuests";
            this.llQuests.Size = new System.Drawing.Size(40, 13);
            this.llQuests.TabIndex = 36;
            this.llQuests.TabStop = true;
            this.llQuests.Text = "Quests";
            this.llQuests.Click += new System.EventHandler(this.btnQuests_Click);
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.Location = new System.Drawing.Point(245, 34);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(22, 13);
            this.labelHP.TabIndex = 25;
            this.labelHP.Text = "HP";
            this.labelHP.MouseEnter += new System.EventHandler(this.labelHP_MouseEnter);
            // 
            // labelConditionHeader
            // 
            this.labelConditionHeader.AutoSize = true;
            this.labelConditionHeader.Location = new System.Drawing.Point(6, 146);
            this.labelConditionHeader.Name = "labelConditionHeader";
            this.labelConditionHeader.Size = new System.Drawing.Size(54, 13);
            this.labelConditionHeader.TabIndex = 33;
            this.labelConditionHeader.Text = "Condition:";
            // 
            // labelSPHeader
            // 
            this.labelSPHeader.AutoSize = true;
            this.labelSPHeader.Location = new System.Drawing.Point(220, 49);
            this.labelSPHeader.Name = "labelSPHeader";
            this.labelSPHeader.Size = new System.Drawing.Size(21, 13);
            this.labelSPHeader.TabIndex = 26;
            this.labelSPHeader.Text = "SP";
            this.labelSPHeader.MouseEnter += new System.EventHandler(this.labelSPHeader_MouseEnter);
            // 
            // labelMelee
            // 
            this.labelMelee.AutoSize = true;
            this.labelMelee.Location = new System.Drawing.Point(157, 94);
            this.labelMelee.Name = "labelMelee";
            this.labelMelee.Size = new System.Drawing.Size(43, 13);
            this.labelMelee.TabIndex = 32;
            this.labelMelee.Text = "MELEE";
            this.labelMelee.MouseEnter += new System.EventHandler(this.labelMelee_MouseEnter);
            // 
            // labelHPHeader
            // 
            this.labelHPHeader.AutoSize = true;
            this.labelHPHeader.Location = new System.Drawing.Point(220, 34);
            this.labelHPHeader.Name = "labelHPHeader";
            this.labelHPHeader.Size = new System.Drawing.Size(22, 13);
            this.labelHPHeader.TabIndex = 24;
            this.labelHPHeader.Text = "HP";
            this.labelHPHeader.MouseEnter += new System.EventHandler(this.labelHPHeader_MouseEnter);
            // 
            // labelMeleeHeader
            // 
            this.labelMeleeHeader.AutoSize = true;
            this.labelMeleeHeader.Location = new System.Drawing.Point(109, 94);
            this.labelMeleeHeader.Name = "labelMeleeHeader";
            this.labelMeleeHeader.Size = new System.Drawing.Size(36, 13);
            this.labelMeleeHeader.TabIndex = 30;
            this.labelMeleeHeader.Text = "Melee";
            this.labelMeleeHeader.MouseEnter += new System.EventHandler(this.labelMeleeHeader_MouseEnter);
            // 
            // labelAC
            // 
            this.labelAC.AutoSize = true;
            this.labelAC.Location = new System.Drawing.Point(245, 64);
            this.labelAC.Name = "labelAC";
            this.labelAC.Size = new System.Drawing.Size(21, 13);
            this.labelAC.TabIndex = 29;
            this.labelAC.Text = "AC";
            this.labelAC.MouseEnter += new System.EventHandler(this.labelAC_MouseEnter);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(109, 94);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 31;
            this.label25.Text = "Melee";
            // 
            // labelACHeader
            // 
            this.labelACHeader.AutoSize = true;
            this.labelACHeader.Location = new System.Drawing.Point(220, 64);
            this.labelACHeader.Name = "labelACHeader";
            this.labelACHeader.Size = new System.Drawing.Size(21, 13);
            this.labelACHeader.TabIndex = 28;
            this.labelACHeader.Text = "AC";
            this.labelACHeader.MouseEnter += new System.EventHandler(this.labelAC_MouseEnter);
            // 
            // labelExpHeader
            // 
            this.labelExpHeader.AutoSize = true;
            this.labelExpHeader.Location = new System.Drawing.Point(109, 19);
            this.labelExpHeader.Name = "labelExpHeader";
            this.labelExpHeader.Size = new System.Drawing.Size(25, 13);
            this.labelExpHeader.TabIndex = 22;
            this.labelExpHeader.Text = "Exp";
            this.labelExpHeader.MouseEnter += new System.EventHandler(this.labelExpHeader_MouseEnter);
            // 
            // labelCondition
            // 
            this.labelCondition.AutoEllipsis = true;
            this.labelCondition.AutoSize = true;
            this.labelCondition.Location = new System.Drawing.Point(59, 146);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(38, 13);
            this.labelCondition.TabIndex = 34;
            this.labelCondition.Text = "COND";
            // 
            // labelExp
            // 
            this.labelExp.AutoSize = true;
            this.labelExp.Location = new System.Drawing.Point(146, 19);
            this.labelExp.Name = "labelExp";
            this.labelExp.Size = new System.Drawing.Size(28, 13);
            this.labelExp.TabIndex = 23;
            this.labelExp.Text = "EXP";
            this.labelExp.MouseEnter += new System.EventHandler(this.labelExp_MouseEnter);
            // 
            // lvResistances
            // 
            this.lvResistances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chResistance,
            this.chResistValue});
            this.lvResistances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResistances.FullRowSelect = true;
            this.lvResistances.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResistances.Location = new System.Drawing.Point(0, 0);
            this.lvResistances.Name = "lvResistances";
            this.lvResistances.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvResistances.Size = new System.Drawing.Size(122, 195);
            this.lvResistances.TabIndex = 37;
            this.lvResistances.TipDelay = 700;
            this.lvResistances.TipDuration = 30000;
            this.lvResistances.UseCompatibleStateImageBehavior = false;
            this.lvResistances.View = System.Windows.Forms.View.Details;
            this.lvResistances.SelectedIndexChanged += new System.EventHandler(this.lvResistances_SelectedIndexChanged);
            this.lvResistances.MouseEnter += new System.EventHandler(this.lvResistances_MouseEnter);
            // 
            // chResistance
            // 
            this.chResistance.Text = "Resist";
            this.chResistance.Width = 51;
            // 
            // chResistValue
            // 
            this.chResistValue.Text = "Value";
            this.chResistValue.Width = 65;
            // 
            // CharacterInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scCharQuickref);
            this.Controls.Add(this.scInventory);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CharacterInfoControl";
            this.Size = new System.Drawing.Size(953, 313);
            this.Resize += new System.EventHandler(this.CharacterInfoControl_Resize);
            this.cmBackpack.ResumeLayout(false);
            this.cmCheat.ResumeLayout(false);
            this.scInventory.Panel1.ResumeLayout(false);
            this.scInventory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInventory)).EndInit();
            this.scInventory.ResumeLayout(false);
            this.scCharQuickref.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCharQuickref)).EndInit();
            this.scCharQuickref.ResumeLayout(false);
            this.scStatsResistances.Panel1.ResumeLayout(false);
            this.scStatsResistances.Panel1.PerformLayout();
            this.scStatsResistances.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scStatsResistances)).EndInit();
            this.scStatsResistances.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmCheat;
        private System.Windows.Forms.ToolStripMenuItem miCheatSubtract1;
        private System.Windows.Forms.ToolStripMenuItem miCheatAdd1;
        private System.Windows.Forms.ToolStripMenuItem miCheatMinimum;
        private System.Windows.Forms.ToolStripMenuItem miCheatMaximum;
        private System.Windows.Forms.ToolStripMenuItem miCheatNextLevel;
        private System.Windows.Forms.ToolStripMenuItem miCheatEdit;
        private System.Windows.Forms.ContextMenuStrip cmBackpack;
        private System.Windows.Forms.ToolStripMenuItem miBackpackTrade;
        private System.Windows.Forms.ToolStripMenuItem miBackpackBagofHolding;
        private System.Windows.Forms.ToolStripMenuItem miBackpackFillRandom;
        private WhereAreWe.DBListView lvEquipped;
        private System.Windows.Forms.ColumnHeader chEquipped;
        private WhereAreWe.DBListView lvBackpack;
        private System.Windows.Forms.ColumnHeader chBackpackItems;
        private System.Windows.Forms.ToolStripMenuItem miBackpackEdit;
        private System.Windows.Forms.ToolStripMenuItem miBackpackAdd;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDuplicate;
        private System.Windows.Forms.ToolStripMenuItem miBackpackClearAll;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDeleteItem;
        private System.Windows.Forms.SplitContainer scInventory;
        private System.Windows.Forms.ToolStripMenuItem miCheatCreateSupercharacter;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDropTrash;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDebugMonitor;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDebugClearAll;
        private System.Windows.Forms.ToolStripMenuItem miBackpackItemDisplayFormat;
        private System.Windows.Forms.SplitContainer scCharQuickref;
        private System.Windows.Forms.LinkLabel llCureAll;
        private System.Windows.Forms.LinkLabel llQuests;
        private TipListView lvResistances;
        private System.Windows.Forms.ColumnHeader chResistance;
        private System.Windows.Forms.ColumnHeader chResistValue;
        private System.Windows.Forms.Label labelConditionHeader;
        private EditableAttributeLabel labelMelee;
        private System.Windows.Forms.Label labelMeleeHeader;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label labelExpHeader;
        private EditableAttributeLabel labelExp;
        private EditableAttributeLabel labelCondition;
        private System.Windows.Forms.Label labelACHeader;
        private EditableAttributeLabel labelAC;
        private System.Windows.Forms.Label labelHPHeader;
        private System.Windows.Forms.Label labelSPHeader;
        private EditableAttributeLabel labelHP;
        private EditableAttributeLabel labelSP;
        private EditableAttributeLabel labelLevel;
        private System.Windows.Forms.ToolStripMenuItem miBackpackStackCharges;
        private System.Windows.Forms.SplitContainer scStatsResistances;
    }
}
