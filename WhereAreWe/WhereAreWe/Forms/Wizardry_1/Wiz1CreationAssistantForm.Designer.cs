﻿namespace WhereAreWe
{
    partial class Wiz1CreationAssistantForm : HackerBasedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wiz1CreationAssistantForm));
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.cmStats = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.miMinimum = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateMemory = new System.Windows.Forms.Timer(this.components);
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
            this.miGenerate100 = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBonus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBonusCopyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvPrevious = new WhereAreWe.DraggableListView();
            this.chTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFirstBonus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSecondBonus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelWorking = new System.Windows.Forms.Panel();
            this.labelWorking = new System.Windows.Forms.Label();
            this.gbBonusTable = new System.Windows.Forms.GroupBox();
            this.lvBonusTable = new WhereAreWe.DBListView();
            this.chBonusRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonusValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.comboAttrib = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelHobbit = new System.Windows.Forms.Label();
            this.labelGnome = new System.Windows.Forms.Label();
            this.labelDwarf = new System.Windows.Forms.Label();
            this.labelElf = new System.Windows.Forms.Label();
            this.labelHuman = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvStats = new WhereAreWe.DBListView();
            this.chAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInvalidRolls = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelExtraPoints = new System.Windows.Forms.Label();
            this.gbClass = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFighter = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelMage = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelPriest = new System.Windows.Forms.Label();
            this.labelNinja = new System.Windows.Forms.Label();
            this.labelLord = new System.Windows.Forms.Label();
            this.labelSamurai = new System.Windows.Forms.Label();
            this.labelBishop = new System.Windows.Forms.Label();
            this.labelThief = new System.Windows.Forms.Label();
            this.gbRaces = new System.Windows.Forms.GroupBox();
            this.cmStats.SuspendLayout();
            this.cmPrevious.SuspendLayout();
            this.cmBonus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelWorking.SuspendLayout();
            this.gbBonusTable.SuspendLayout();
            this.panelInvalidRolls.SuspendLayout();
            this.gbClass.SuspendLayout();
            this.gbRaces.SuspendLayout();
            this.SuspendLayout();
            // 
            // miEditPath
            // 
            this.miEditPath.Name = "miEditPath";
            this.miEditPath.Size = new System.Drawing.Size(32, 19);
            // 
            // cmStats
            // 
            this.cmStats.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMaximum,
            this.miMinimum});
            this.cmStats.Name = "cmStats";
            this.cmStats.ShowImageMargin = false;
            this.cmStats.Size = new System.Drawing.Size(134, 48);
            // 
            // miMaximum
            // 
            this.miMaximum.Name = "miMaximum";
            this.miMaximum.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.miMaximum.Size = new System.Drawing.Size(133, 22);
            this.miMaximum.Text = "&Maximum";
            this.miMaximum.Click += new System.EventHandler(this.miMaximum_Click);
            // 
            // miMinimum
            // 
            this.miMinimum.Name = "miMinimum";
            this.miMinimum.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miMinimum.Size = new System.Drawing.Size(133, 22);
            this.miMinimum.Text = "Mi&nimum";
            this.miMinimum.Click += new System.EventHandler(this.miMinimum_Click);
            // 
            // timerUpdateMemory
            // 
            this.timerUpdateMemory.Interval = 50;
            this.timerUpdateMemory.Tick += new System.EventHandler(this.timerUpdateMemory_Tick);
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
            this.miGenerate1000});
            this.cmPrevious.Name = "cmPrevious";
            this.cmPrevious.ShowImageMargin = false;
            this.cmPrevious.Size = new System.Drawing.Size(213, 192);
            this.cmPrevious.Opening += new System.ComponentModel.CancelEventHandler(this.cmPrevious_Opening);
            // 
            // useTheseRollsToolStripMenuItem
            // 
            this.useTheseRollsToolStripMenuItem.Name = "useTheseRollsToolStripMenuItem";
            this.useTheseRollsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.useTheseRollsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.useTheseRollsToolStripMenuItem.Text = "&Use these rolls";
            this.useTheseRollsToolStripMenuItem.Click += new System.EventHandler(this.useTheseRollsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miDelete.Size = new System.Drawing.Size(212, 22);
            this.miDelete.Text = "&Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miDeleteExcept
            // 
            this.miDeleteExcept.Name = "miDeleteExcept";
            this.miDeleteExcept.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.miDeleteExcept.Size = new System.Drawing.Size(212, 22);
            this.miDeleteExcept.Text = "Delete all &except";
            this.miDeleteExcept.Click += new System.EventHandler(this.miDeleteExcept_Click);
            // 
            // miDeleteLowerUnweighted
            // 
            this.miDeleteLowerUnweighted.Name = "miDeleteLowerUnweighted";
            this.miDeleteLowerUnweighted.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.miDeleteLowerUnweighted.Size = new System.Drawing.Size(212, 22);
            this.miDeleteLowerUnweighted.Text = "Delete &lower unweighted";
            this.miDeleteLowerUnweighted.Click += new System.EventHandler(this.miDeleteLowerUnweighted_Click);
            // 
            // miDeleteLower
            // 
            this.miDeleteLower.Name = "miDeleteLower";
            this.miDeleteLower.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.miDeleteLower.Size = new System.Drawing.Size(212, 22);
            this.miDeleteLower.Text = "Delete lower &weighted";
            this.miDeleteLower.Click += new System.EventHandler(this.miDeleteLower_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
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
            this.miRemoveUnusableBarbarian});
            this.miRemoveUnusable.Name = "miRemoveUnusable";
            this.miRemoveUnusable.Size = new System.Drawing.Size(212, 22);
            this.miRemoveUnusable.Text = "&Remove unusable by";
            this.miRemoveUnusable.DropDownOpening += new System.EventHandler(this.miRemoveUnusable_DropDownOpening);
            // 
            // miRemoveUnusableKnight
            // 
            this.miRemoveUnusableKnight.Name = "miRemoveUnusableKnight";
            this.miRemoveUnusableKnight.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableKnight.Text = "&Knight";
            this.miRemoveUnusableKnight.Click += new System.EventHandler(this.miRemoveUnusableKnight_Click);
            // 
            // miRemoveUnusablePaladin
            // 
            this.miRemoveUnusablePaladin.Name = "miRemoveUnusablePaladin";
            this.miRemoveUnusablePaladin.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusablePaladin.Text = "&Paladin";
            this.miRemoveUnusablePaladin.Click += new System.EventHandler(this.miRemoveUnusablePaladin_Click);
            // 
            // miRemoveUnusableArcher
            // 
            this.miRemoveUnusableArcher.Name = "miRemoveUnusableArcher";
            this.miRemoveUnusableArcher.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableArcher.Text = "&Archer";
            this.miRemoveUnusableArcher.Click += new System.EventHandler(this.miRemoveUnusableArcher_Click);
            // 
            // miRemoveUnusableCleric
            // 
            this.miRemoveUnusableCleric.Name = "miRemoveUnusableCleric";
            this.miRemoveUnusableCleric.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableCleric.Text = "&Cleric";
            this.miRemoveUnusableCleric.Click += new System.EventHandler(this.miRemoveUnusableCleric_Click);
            // 
            // miRemoveUnusableSorcerer
            // 
            this.miRemoveUnusableSorcerer.Name = "miRemoveUnusableSorcerer";
            this.miRemoveUnusableSorcerer.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableSorcerer.Text = "&Sorcerer";
            this.miRemoveUnusableSorcerer.Click += new System.EventHandler(this.miRemoveUnusableSorcerer_Click);
            // 
            // miRemoveUnusableRobber
            // 
            this.miRemoveUnusableRobber.Name = "miRemoveUnusableRobber";
            this.miRemoveUnusableRobber.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableRobber.Text = "&Robber";
            this.miRemoveUnusableRobber.Click += new System.EventHandler(this.miRemoveUnusableRobber_Click);
            // 
            // miRemoveUnusableNinja
            // 
            this.miRemoveUnusableNinja.Name = "miRemoveUnusableNinja";
            this.miRemoveUnusableNinja.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableNinja.Text = "&Ninja";
            this.miRemoveUnusableNinja.Click += new System.EventHandler(this.miRemoveUnusableNinja_Click);
            // 
            // miRemoveUnusableBarbarian
            // 
            this.miRemoveUnusableBarbarian.Name = "miRemoveUnusableBarbarian";
            this.miRemoveUnusableBarbarian.Size = new System.Drawing.Size(120, 22);
            this.miRemoveUnusableBarbarian.Text = "&Barbarian";
            this.miRemoveUnusableBarbarian.Click += new System.EventHandler(this.miRemoveUnusableBarbarian_Click);
            // 
            // miGenerate100
            // 
            this.miGenerate100.Name = "miGenerate100";
            this.miGenerate100.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.miGenerate100.Size = new System.Drawing.Size(212, 22);
            this.miGenerate100.Text = "&Generate 100 rolls";
            this.miGenerate100.Click += new System.EventHandler(this.miGenerate100_Click);
            // 
            // miGenerate1000
            // 
            this.miGenerate1000.Name = "miGenerate1000";
            this.miGenerate1000.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.miGenerate1000.Size = new System.Drawing.Size(212, 22);
            this.miGenerate1000.Text = "&Generate 1000 rolls";
            this.miGenerate1000.Click += new System.EventHandler(this.miGenerate1000_Click);
            // 
            // cmBonus
            // 
            this.cmBonus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBonusCopyTable});
            this.cmBonus.Name = "cmBonus";
            this.cmBonus.Size = new System.Drawing.Size(130, 26);
            // 
            // miBonusCopyTable
            // 
            this.miBonusCopyTable.Name = "miBonusCopyTable";
            this.miBonusCopyTable.Size = new System.Drawing.Size(129, 22);
            this.miBonusCopyTable.Text = "&Copy table ";
            this.miBonusCopyTable.Click += new System.EventHandler(this.miBonusCopyTable_Click);
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
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lvPrevious);
            this.groupBox3.Controls.Add(this.btnOK);
            this.groupBox3.Controls.Add(this.panelWorking);
            this.groupBox3.Location = new System.Drawing.Point(3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 418);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Previous Bonses";
            // 
            // lvPrevious
            // 
            this.lvPrevious.AllowDrop = true;
            this.lvPrevious.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvPrevious.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTotal,
            this.chMain,
            this.chFirstBonus,
            this.chSecondBonus});
            this.lvPrevious.ContextMenuStrip = this.cmPrevious;
            this.lvPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrevious.FullRowSelect = true;
            this.lvPrevious.HideSelection = false;
            this.lvPrevious.Location = new System.Drawing.Point(3, 16);
            this.lvPrevious.Name = "lvPrevious";
            this.lvPrevious.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvPrevious.Size = new System.Drawing.Size(177, 399);
            this.lvPrevious.TabIndex = 0;
            this.lvPrevious.UseCompatibleStateImageBehavior = false;
            this.lvPrevious.View = System.Windows.Forms.View.Details;
            this.lvPrevious.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPrevious_ColumnClick);
            this.lvPrevious.DoubleClick += new System.EventHandler(this.lvPrevious_DoubleClick);
            this.lvPrevious.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPrevious_KeyDown);
            // 
            // chTotal
            // 
            this.chTotal.Text = "Total";
            this.chTotal.Width = 40;
            // 
            // chMain
            // 
            this.chMain.Text = "Main";
            this.chMain.Width = 40;
            // 
            // chFirstBonus
            // 
            this.chFirstBonus.Text = "10%";
            this.chFirstBonus.Width = 40;
            // 
            // chSecondBonus
            // 
            this.chSecondBonus.Text = "1%";
            this.chSecondBonus.Width = 40;
            // 
            // panelWorking
            // 
            this.panelWorking.Controls.Add(this.labelWorking);
            this.panelWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorking.Location = new System.Drawing.Point(3, 16);
            this.panelWorking.Name = "panelWorking";
            this.panelWorking.Size = new System.Drawing.Size(177, 399);
            this.panelWorking.TabIndex = 2;
            this.panelWorking.Visible = false;
            // 
            // labelWorking
            // 
            this.labelWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWorking.Location = new System.Drawing.Point(0, 0);
            this.labelWorking.Name = "labelWorking";
            this.labelWorking.Size = new System.Drawing.Size(177, 399);
            this.labelWorking.TabIndex = 0;
            this.labelWorking.Text = "Working...";
            this.labelWorking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbBonusTable
            // 
            this.gbBonusTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBonusTable.Controls.Add(this.lvBonusTable);
            this.gbBonusTable.Controls.Add(this.label3);
            this.gbBonusTable.Controls.Add(this.comboAttrib);
            this.gbBonusTable.Location = new System.Drawing.Point(329, 2);
            this.gbBonusTable.Name = "gbBonusTable";
            this.gbBonusTable.Size = new System.Drawing.Size(149, 170);
            this.gbBonusTable.TabIndex = 6;
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
            this.lvBonusTable.Size = new System.Drawing.Size(144, 132);
            this.lvBonusTable.TabIndex = 0;
            this.lvBonusTable.UseCompatibleStateImageBehavior = false;
            this.lvBonusTable.View = System.Windows.Forms.View.Details;
            this.lvBonusTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSheets_KeyDown);
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
            this.label3.Location = new System.Drawing.Point(3, 151);
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
            this.comboAttrib.Location = new System.Drawing.Point(40, 146);
            this.comboAttrib.Name = "comboAttrib";
            this.comboAttrib.Size = new System.Drawing.Size(106, 21);
            this.comboAttrib.TabIndex = 6;
            this.comboAttrib.SelectedIndexChanged += new System.EventHandler(this.comboAttrib_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Hobbit:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Gnome:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dwarf:";
            // 
            // labelHobbit
            // 
            this.labelHobbit.AutoSize = true;
            this.labelHobbit.Location = new System.Drawing.Point(54, 75);
            this.labelHobbit.Name = "labelHobbit";
            this.labelHobbit.Size = new System.Drawing.Size(47, 13);
            this.labelHobbit.TabIndex = 3;
            this.labelHobbit.Text = "HOBBIT";
            // 
            // labelGnome
            // 
            this.labelGnome.AutoSize = true;
            this.labelGnome.Location = new System.Drawing.Point(54, 60);
            this.labelGnome.Name = "labelGnome";
            this.labelGnome.Size = new System.Drawing.Size(47, 13);
            this.labelGnome.TabIndex = 3;
            this.labelGnome.Text = "GNOME";
            // 
            // labelDwarf
            // 
            this.labelDwarf.AutoSize = true;
            this.labelDwarf.Location = new System.Drawing.Point(54, 45);
            this.labelDwarf.Name = "labelDwarf";
            this.labelDwarf.Size = new System.Drawing.Size(47, 13);
            this.labelDwarf.TabIndex = 3;
            this.labelDwarf.Text = "DWARF";
            // 
            // labelElf
            // 
            this.labelElf.AutoSize = true;
            this.labelElf.Location = new System.Drawing.Point(54, 30);
            this.labelElf.Name = "labelElf";
            this.labelElf.Size = new System.Drawing.Size(26, 13);
            this.labelElf.TabIndex = 3;
            this.labelElf.Text = "ELF";
            // 
            // labelHuman
            // 
            this.labelHuman.AutoSize = true;
            this.labelHuman.Location = new System.Drawing.Point(54, 15);
            this.labelHuman.Name = "labelHuman";
            this.labelHuman.Size = new System.Drawing.Size(47, 13);
            this.labelHuman.TabIndex = 3;
            this.labelHuman.Text = "HUMAN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Elf:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Human:";
            // 
            // lvStats
            // 
            this.lvStats.AllowDrop = true;
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAttribute,
            this.chValue});
            this.lvStats.ContextMenuStrip = this.cmStats;
            this.lvStats.FullRowSelect = true;
            this.lvStats.HideSelection = false;
            this.lvStats.Location = new System.Drawing.Point(0, 0);
            this.lvStats.MultiSelect = false;
            this.lvStats.Name = "lvStats";
            this.lvStats.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvStats.ShowItemToolTips = true;
            this.lvStats.Size = new System.Drawing.Size(131, 140);
            this.lvStats.TabIndex = 1;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            this.lvStats.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSheets_KeyDown);
            // 
            // chAttribute
            // 
            this.chAttribute.Text = "Attribute";
            this.chAttribute.Width = 75;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 45;
            // 
            // panelInvalidRolls
            // 
            this.panelInvalidRolls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInvalidRolls.Controls.Add(this.lvStats);
            this.panelInvalidRolls.Controls.Add(this.label20);
            this.panelInvalidRolls.Controls.Add(this.label14);
            this.panelInvalidRolls.Location = new System.Drawing.Point(192, 7);
            this.panelInvalidRolls.Name = "panelInvalidRolls";
            this.panelInvalidRolls.Size = new System.Drawing.Size(131, 140);
            this.panelInvalidRolls.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Location = new System.Drawing.Point(13, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(107, 55);
            this.label20.TabIndex = 3;
            this.label20.Text = "Please ensure that you are on the character creation screen!";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(13, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 31);
            this.label14.TabIndex = 3;
            this.label14.Text = "Invalid rolls obtained from game!";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(192, 158);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Bonus Points:";
            // 
            // labelExtraPoints
            // 
            this.labelExtraPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExtraPoints.AutoSize = true;
            this.labelExtraPoints.Location = new System.Drawing.Point(270, 158);
            this.labelExtraPoints.Name = "labelExtraPoints";
            this.labelExtraPoints.Size = new System.Drawing.Size(13, 13);
            this.labelExtraPoints.TabIndex = 3;
            this.labelExtraPoints.Text = "0";
            // 
            // gbClass
            // 
            this.gbClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbClass.Controls.Add(this.label18);
            this.gbClass.Controls.Add(this.label17);
            this.gbClass.Controls.Add(this.label22);
            this.gbClass.Controls.Add(this.label1);
            this.gbClass.Controls.Add(this.labelFighter);
            this.gbClass.Controls.Add(this.label21);
            this.gbClass.Controls.Add(this.label6);
            this.gbClass.Controls.Add(this.labelMage);
            this.gbClass.Controls.Add(this.label19);
            this.gbClass.Controls.Add(this.label9);
            this.gbClass.Controls.Add(this.labelPriest);
            this.gbClass.Controls.Add(this.labelNinja);
            this.gbClass.Controls.Add(this.labelLord);
            this.gbClass.Controls.Add(this.labelSamurai);
            this.gbClass.Controls.Add(this.labelBishop);
            this.gbClass.Controls.Add(this.labelThief);
            this.gbClass.Location = new System.Drawing.Point(192, 178);
            this.gbClass.Name = "gbClass";
            this.gbClass.Size = new System.Drawing.Size(369, 141);
            this.gbClass.TabIndex = 8;
            this.gbClass.TabStop = false;
            this.gbClass.Text = "Class bonuses";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Fighter:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Mage:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 105);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Lord:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Thief:";
            // 
            // labelFighter
            // 
            this.labelFighter.AutoSize = true;
            this.labelFighter.Location = new System.Drawing.Point(54, 15);
            this.labelFighter.Name = "labelFighter";
            this.labelFighter.Size = new System.Drawing.Size(54, 13);
            this.labelFighter.TabIndex = 3;
            this.labelFighter.Text = "FIGHTER";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 120);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(34, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Ninja:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Bishop:";
            // 
            // labelMage
            // 
            this.labelMage.AutoSize = true;
            this.labelMage.Location = new System.Drawing.Point(54, 30);
            this.labelMage.Name = "labelMage";
            this.labelMage.Size = new System.Drawing.Size(38, 13);
            this.labelMage.TabIndex = 3;
            this.labelMage.Text = "MAGE";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 90);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(48, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Samurai:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Priest:";
            // 
            // labelPriest
            // 
            this.labelPriest.AutoSize = true;
            this.labelPriest.Location = new System.Drawing.Point(54, 45);
            this.labelPriest.Name = "labelPriest";
            this.labelPriest.Size = new System.Drawing.Size(46, 13);
            this.labelPriest.TabIndex = 3;
            this.labelPriest.Text = "PRIEST";
            // 
            // labelNinja
            // 
            this.labelNinja.AutoSize = true;
            this.labelNinja.Location = new System.Drawing.Point(54, 120);
            this.labelNinja.Name = "labelNinja";
            this.labelNinja.Size = new System.Drawing.Size(38, 13);
            this.labelNinja.TabIndex = 3;
            this.labelNinja.Text = "NINJA";
            // 
            // labelLord
            // 
            this.labelLord.AutoSize = true;
            this.labelLord.Location = new System.Drawing.Point(54, 105);
            this.labelLord.Name = "labelLord";
            this.labelLord.Size = new System.Drawing.Size(37, 13);
            this.labelLord.TabIndex = 3;
            this.labelLord.Text = "LORD";
            // 
            // labelSamurai
            // 
            this.labelSamurai.AutoSize = true;
            this.labelSamurai.Location = new System.Drawing.Point(54, 90);
            this.labelSamurai.Name = "labelSamurai";
            this.labelSamurai.Size = new System.Drawing.Size(56, 13);
            this.labelSamurai.TabIndex = 3;
            this.labelSamurai.Text = "SAMURAI";
            // 
            // labelBishop
            // 
            this.labelBishop.AutoSize = true;
            this.labelBishop.Location = new System.Drawing.Point(54, 75);
            this.labelBishop.Name = "labelBishop";
            this.labelBishop.Size = new System.Drawing.Size(47, 13);
            this.labelBishop.TabIndex = 3;
            this.labelBishop.Text = "BISHOP";
            // 
            // labelThief
            // 
            this.labelThief.AutoSize = true;
            this.labelThief.Location = new System.Drawing.Point(54, 60);
            this.labelThief.Name = "labelThief";
            this.labelThief.Size = new System.Drawing.Size(38, 13);
            this.labelThief.TabIndex = 3;
            this.labelThief.Text = "THIEF";
            // 
            // gbRaces
            // 
            this.gbRaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRaces.Controls.Add(this.label2);
            this.gbRaces.Controls.Add(this.label7);
            this.gbRaces.Controls.Add(this.labelHuman);
            this.gbRaces.Controls.Add(this.labelElf);
            this.gbRaces.Controls.Add(this.labelDwarf);
            this.gbRaces.Controls.Add(this.labelGnome);
            this.gbRaces.Controls.Add(this.labelHobbit);
            this.gbRaces.Controls.Add(this.label4);
            this.gbRaces.Controls.Add(this.label8);
            this.gbRaces.Controls.Add(this.label5);
            this.gbRaces.Location = new System.Drawing.Point(192, 325);
            this.gbRaces.Name = "gbRaces";
            this.gbRaces.Size = new System.Drawing.Size(369, 94);
            this.gbRaces.TabIndex = 0;
            this.gbRaces.TabStop = false;
            this.gbRaces.Text = "Race Bonuses";
            // 
            // Wiz1CreationAssistantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(564, 423);
            this.Controls.Add(this.gbRaces);
            this.Controls.Add(this.gbClass);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbBonusTable);
            this.Controls.Add(this.labelExtraPoints);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panelInvalidRolls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "Wiz1CreationAssistantForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Character Creation Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharCreationForm_FormClosing);
            this.Load += new System.EventHandler(this.CharCreationForm_Load);
            this.cmStats.ResumeLayout(false);
            this.cmPrevious.ResumeLayout(false);
            this.cmBonus.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panelWorking.ResumeLayout(false);
            this.gbBonusTable.ResumeLayout(false);
            this.gbBonusTable.PerformLayout();
            this.panelInvalidRolls.ResumeLayout(false);
            this.gbClass.ResumeLayout(false);
            this.gbClass.PerformLayout();
            this.gbRaces.ResumeLayout(false);
            this.gbRaces.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WhereAreWe.DBListView lvStats;
        private System.Windows.Forms.ColumnHeader chAttribute;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelElf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelHuman;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelDwarf;
        private System.Windows.Forms.Label labelGnome;
        private System.Windows.Forms.Label labelHobbit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timerUpdateMemory;
        private System.Windows.Forms.GroupBox gbBonusTable;
        private System.Windows.Forms.Panel panelInvalidRolls;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private WhereAreWe.DraggableListView lvPrevious;
        private System.Windows.Forms.ColumnHeader chMain;
        private System.Windows.Forms.ColumnHeader chFirstBonus;
        private System.Windows.Forms.ColumnHeader chSecondBonus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ContextMenuStrip cmPrevious;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miDeleteExcept;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLower;
        private System.Windows.Forms.ToolStripMenuItem miGenerate100;
        private System.Windows.Forms.ToolStripMenuItem useTheseRollsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panelWorking;
        private System.Windows.Forms.Label labelWorking;
        private System.Windows.Forms.ColumnHeader chTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusable;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableKnight;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusablePaladin;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableArcher;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableCleric;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableSorcerer;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLowerUnweighted;
        private System.Windows.Forms.ToolStripMenuItem miGenerate1000;
        private System.Windows.Forms.ContextMenuStrip cmStats;
        private System.Windows.Forms.ToolStripMenuItem miMaximum;
        private System.Windows.Forms.ToolStripMenuItem miMinimum;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableRobber;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableNinja;
        private System.Windows.Forms.ToolStripMenuItem miRemoveUnusableBarbarian;
        private DBListView lvBonusTable;
        private System.Windows.Forms.ColumnHeader chBonusRange;
        private System.Windows.Forms.ColumnHeader chBonusValue;
        private System.Windows.Forms.ContextMenuStrip cmBonus;
        private System.Windows.Forms.ToolStripMenuItem miBonusCopyTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboAttrib;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelExtraPoints;
        private System.Windows.Forms.GroupBox gbClass;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFighter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelMage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelPriest;
        private System.Windows.Forms.Label labelBishop;
        private System.Windows.Forms.Label labelThief;
        private System.Windows.Forms.GroupBox gbRaces;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label labelNinja;
        private System.Windows.Forms.Label labelLord;
        private System.Windows.Forms.Label labelSamurai;
    }
}