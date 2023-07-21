namespace WhereAreWe
{
    partial class BT2EncounterEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BT2EncounterEditForm));
            this.lvGroups = new System.Windows.Forms.ListView();
            this.chGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMonster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAlive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGroupSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttacks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDistance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpecial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttack1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttack2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttack3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttack4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miEditMonsterGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMonsterGroups = new System.Windows.Forms.GroupBox();
            this.gbGroupInfo = new System.Windows.Forms.GroupBox();
            this.llKillAllMonsters = new System.Windows.Forms.LinkLabel();
            this.nudDistance = new System.Windows.Forms.NumericUpDown();
            this.nudLiving = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbMonsters = new System.Windows.Forms.GroupBox();
            this.lvMonsters = new System.Windows.Forms.ListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHitPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chArmorClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCondition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudHitPoints = new System.Windows.Forms.NumericUpDown();
            this.gbMonsterInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmGroup.SuspendLayout();
            this.gbMonsterGroups.SuspendLayout();
            this.gbGroupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiving)).BeginInit();
            this.gbMonsters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitPoints)).BeginInit();
            this.gbMonsterInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvGroups
            // 
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGroup,
            this.chMonster,
            this.chAlive,
            this.chGroupSize,
            this.chAC,
            this.chAttacks,
            this.chDamage,
            this.chDistance,
            this.chSpecial,
            this.chExp,
            this.chAttack1,
            this.chAttack2,
            this.chAttack3,
            this.chAttack4,
            this.chSpeed});
            this.lvGroups.ContextMenuStrip = this.cmGroup;
            this.lvGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGroups.HideSelection = false;
            this.lvGroups.Location = new System.Drawing.Point(3, 16);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(657, 113);
            this.lvGroups.TabIndex = 0;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            this.lvGroups.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGroups_MouseDoubleClick);
            // 
            // chGroup
            // 
            this.chGroup.Text = "Grp";
            this.chGroup.Width = 30;
            // 
            // chMonster
            // 
            this.chMonster.Text = "Monster";
            this.chMonster.Width = 92;
            // 
            // chAlive
            // 
            this.chAlive.Text = "Alive";
            this.chAlive.Width = 35;
            // 
            // chGroupSize
            // 
            this.chGroupSize.Text = "Size";
            this.chGroupSize.Width = 35;
            // 
            // chAC
            // 
            this.chAC.Text = "AC";
            this.chAC.Width = 35;
            // 
            // chAttacks
            // 
            this.chAttacks.Text = "#Att";
            this.chAttacks.Width = 35;
            // 
            // chDamage
            // 
            this.chDamage.Text = "Dmg";
            this.chDamage.Width = 43;
            // 
            // chDistance
            // 
            this.chDistance.Text = "Dist";
            this.chDistance.Width = 35;
            // 
            // chSpecial
            // 
            this.chSpecial.Text = "Touch";
            this.chSpecial.Width = 46;
            // 
            // chExp
            // 
            this.chExp.Text = "Exp";
            this.chExp.Width = 45;
            // 
            // chAttack1
            // 
            this.chAttack1.Text = "Att 1";
            this.chAttack1.Width = 44;
            // 
            // chAttack2
            // 
            this.chAttack2.Text = "Att 2";
            this.chAttack2.Width = 44;
            // 
            // chAttack3
            // 
            this.chAttack3.Text = "Att 3";
            this.chAttack3.Width = 44;
            // 
            // chAttack4
            // 
            this.chAttack4.Text = "Att 4";
            this.chAttack4.Width = 44;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Spd";
            this.chSpeed.Width = 35;
            // 
            // cmGroup
            // 
            this.cmGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditMonsterGroup});
            this.cmGroup.Name = "cmGroup";
            this.cmGroup.Size = new System.Drawing.Size(167, 26);
            this.cmGroup.Opening += new System.ComponentModel.CancelEventHandler(this.cmGroup_Opening);
            // 
            // miEditMonsterGroup
            // 
            this.miEditMonsterGroup.Name = "miEditMonsterGroup";
            this.miEditMonsterGroup.Size = new System.Drawing.Size(166, 22);
            this.miEditMonsterGroup.Text = "&Edit Monster Group";
            this.miEditMonsterGroup.Click += new System.EventHandler(this.editMonsterGroupToolStripMenuItem_Click);
            // 
            // gbMonsterGroups
            // 
            this.gbMonsterGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMonsterGroups.Controls.Add(this.lvGroups);
            this.gbMonsterGroups.Location = new System.Drawing.Point(12, 12);
            this.gbMonsterGroups.Name = "gbMonsterGroups";
            this.gbMonsterGroups.Size = new System.Drawing.Size(663, 132);
            this.gbMonsterGroups.TabIndex = 0;
            this.gbMonsterGroups.TabStop = false;
            this.gbMonsterGroups.Text = "Monster Groups";
            // 
            // gbGroupInfo
            // 
            this.gbGroupInfo.Controls.Add(this.llKillAllMonsters);
            this.gbGroupInfo.Controls.Add(this.nudDistance);
            this.gbGroupInfo.Controls.Add(this.nudLiving);
            this.gbGroupInfo.Controls.Add(this.label1);
            this.gbGroupInfo.Controls.Add(this.label3);
            this.gbGroupInfo.Location = new System.Drawing.Point(130, 150);
            this.gbGroupInfo.Name = "gbGroupInfo";
            this.gbGroupInfo.Size = new System.Drawing.Size(226, 78);
            this.gbGroupInfo.TabIndex = 2;
            this.gbGroupInfo.TabStop = false;
            this.gbGroupInfo.Text = "Group Info";
            // 
            // llKillAllMonsters
            // 
            this.llKillAllMonsters.AutoSize = true;
            this.llKillAllMonsters.Location = new System.Drawing.Point(134, 26);
            this.llKillAllMonsters.Name = "llKillAllMonsters";
            this.llKillAllMonsters.Size = new System.Drawing.Size(78, 13);
            this.llKillAllMonsters.TabIndex = 2;
            this.llKillAllMonsters.TabStop = true;
            this.llKillAllMonsters.Text = "&Kill all monsters";
            this.llKillAllMonsters.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llKillAllMonsters_LinkClicked);
            // 
            // nudDistance
            // 
            this.nudDistance.Enabled = false;
            this.nudDistance.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDistance.Location = new System.Drawing.Point(60, 44);
            this.nudDistance.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudDistance.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDistance.Name = "nudDistance";
            this.nudDistance.Size = new System.Drawing.Size(70, 20);
            this.nudDistance.TabIndex = 4;
            this.nudDistance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDistance.ValueChanged += new System.EventHandler(this.nudDistance_ValueChanged);
            this.nudDistance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudDistance_KeyDown);
            // 
            // nudLiving
            // 
            this.nudLiving.Enabled = false;
            this.nudLiving.Location = new System.Drawing.Point(60, 22);
            this.nudLiving.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudLiving.Name = "nudLiving";
            this.nudLiving.Size = new System.Drawing.Size(70, 20);
            this.nudLiving.TabIndex = 1;
            this.nudLiving.ValueChanged += new System.EventHandler(this.nudLiving_ValueChanged);
            this.nudLiving.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudLiving_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Distance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Living";
            // 
            // gbMonsters
            // 
            this.gbMonsters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbMonsters.Controls.Add(this.lvMonsters);
            this.gbMonsters.Location = new System.Drawing.Point(12, 150);
            this.gbMonsters.Name = "gbMonsters";
            this.gbMonsters.Size = new System.Drawing.Size(115, 188);
            this.gbMonsters.TabIndex = 1;
            this.gbMonsters.TabStop = false;
            this.gbMonsters.Text = "Monsters in Group";
            // 
            // lvMonsters
            // 
            this.lvMonsters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chHitPoints,
            this.chArmorClass,
            this.chCondition});
            this.lvMonsters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMonsters.FullRowSelect = true;
            this.lvMonsters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMonsters.HideSelection = false;
            this.lvMonsters.Location = new System.Drawing.Point(3, 16);
            this.lvMonsters.Name = "lvMonsters";
            this.lvMonsters.Size = new System.Drawing.Size(109, 169);
            this.lvMonsters.TabIndex = 0;
            this.lvMonsters.UseCompatibleStateImageBehavior = false;
            this.lvMonsters.View = System.Windows.Forms.View.Details;
            this.lvMonsters.SelectedIndexChanged += new System.EventHandler(this.lvMonsters_SelectedIndexChanged);
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 20;
            // 
            // chHitPoints
            // 
            this.chHitPoints.Text = "HP";
            this.chHitPoints.Width = 52;
            // 
            // chArmorClass
            // 
            this.chArmorClass.Text = "AC";
            this.chArmorClass.Width = 0;
            // 
            // chCondition
            // 
            this.chCondition.Text = "Condition";
            this.chCondition.Width = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(600, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(600, 286);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hit Points";
            // 
            // nudHitPoints
            // 
            this.nudHitPoints.Enabled = false;
            this.nudHitPoints.Location = new System.Drawing.Point(62, 18);
            this.nudHitPoints.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudHitPoints.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.nudHitPoints.Name = "nudHitPoints";
            this.nudHitPoints.Size = new System.Drawing.Size(68, 20);
            this.nudHitPoints.TabIndex = 1;
            this.nudHitPoints.ValueChanged += new System.EventHandler(this.nudHitPoints_ValueChanged);
            this.nudHitPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudHitPoints_KeyDown);
            // 
            // gbMonsterInfo
            // 
            this.gbMonsterInfo.Controls.Add(this.nudHitPoints);
            this.gbMonsterInfo.Controls.Add(this.label4);
            this.gbMonsterInfo.Location = new System.Drawing.Point(130, 234);
            this.gbMonsterInfo.Name = "gbMonsterInfo";
            this.gbMonsterInfo.Size = new System.Drawing.Size(227, 51);
            this.gbMonsterInfo.TabIndex = 3;
            this.gbMonsterInfo.TabStop = false;
            this.gbMonsterInfo.Text = "Monster Info";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(130, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 64);
            this.label2.TabIndex = 4;
            this.label2.Text = "Note: Setting any individual monster\'s HP to zero will kill it and any other mons" +
    "ters further down in the list.";
            // 
            // BT2EncounterEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(686, 349);
            this.Controls.Add(this.gbMonsterGroups);
            this.Controls.Add(this.gbGroupInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbMonsterInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbMonsters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(480, 284);
            this.Name = "BT2EncounterEditForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Encounter";
            this.cmGroup.ResumeLayout(false);
            this.gbMonsterGroups.ResumeLayout(false);
            this.gbGroupInfo.ResumeLayout(false);
            this.gbGroupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiving)).EndInit();
            this.gbMonsters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudHitPoints)).EndInit();
            this.gbMonsterInfo.ResumeLayout(false);
            this.gbMonsterInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGroups;
        private System.Windows.Forms.ColumnHeader chGroup;
        private System.Windows.Forms.ColumnHeader chMonster;
        private System.Windows.Forms.ColumnHeader chAlive;
        private System.Windows.Forms.GroupBox gbMonsterGroups;
        private System.Windows.Forms.GroupBox gbGroupInfo;
        private System.Windows.Forms.GroupBox gbMonsters;
        private System.Windows.Forms.ListView lvMonsters;
        private System.Windows.Forms.ColumnHeader chHitPoints;
        private System.Windows.Forms.ColumnHeader chArmorClass;
        private System.Windows.Forms.ColumnHeader chCondition;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudHitPoints;
        private System.Windows.Forms.GroupBox gbMonsterInfo;
        private System.Windows.Forms.LinkLabel llKillAllMonsters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLiving;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader chGroupSize;
        private System.Windows.Forms.ColumnHeader chAC;
        private System.Windows.Forms.ColumnHeader chAttacks;
        private System.Windows.Forms.ColumnHeader chDamage;
        private System.Windows.Forms.ColumnHeader chDistance;
        private System.Windows.Forms.ColumnHeader chSpecial;
        private System.Windows.Forms.ColumnHeader chExp;
        private System.Windows.Forms.ColumnHeader chAttack1;
        private System.Windows.Forms.ColumnHeader chAttack2;
        private System.Windows.Forms.ColumnHeader chAttack3;
        private System.Windows.Forms.ColumnHeader chAttack4;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ContextMenuStrip cmGroup;
        private System.Windows.Forms.ToolStripMenuItem miEditMonsterGroup;
        private System.Windows.Forms.NumericUpDown nudDistance;
        private System.Windows.Forms.Label label1;
    }
}