namespace WhereAreWe
{
    partial class BTEncounterEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BTEncounterEditForm));
            this.lvGroups = new System.Windows.Forms.ListView();
            this.chGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMonster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAlive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbMonsterGroups = new System.Windows.Forms.GroupBox();
            this.gbGroupInfo = new System.Windows.Forms.GroupBox();
            this.llKillAllMonsters = new System.Windows.Forms.LinkLabel();
            this.nudLiving = new System.Windows.Forms.NumericUpDown();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.comboMonster = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
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
            this.nudACModifier = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.comboCondition = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.scGroupsInfo = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.gbMonsterGroups.SuspendLayout();
            this.gbGroupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiving)).BeginInit();
            this.gbMonsters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitPoints)).BeginInit();
            this.gbMonsterInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudACModifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scGroupsInfo)).BeginInit();
            this.scGroupsInfo.Panel1.SuspendLayout();
            this.scGroupsInfo.Panel2.SuspendLayout();
            this.scGroupsInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvGroups
            // 
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGroup,
            this.chMonster,
            this.chAlive});
            this.lvGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGroups.HideSelection = false;
            this.lvGroups.Location = new System.Drawing.Point(3, 16);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(220, 100);
            this.lvGroups.TabIndex = 0;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // chGroup
            // 
            this.chGroup.Text = "#";
            this.chGroup.Width = 20;
            // 
            // chMonster
            // 
            this.chMonster.Text = "Monster";
            this.chMonster.Width = 110;
            // 
            // chAlive
            // 
            this.chAlive.Text = "Alive";
            this.chAlive.Width = 37;
            // 
            // gbMonsterGroups
            // 
            this.gbMonsterGroups.Controls.Add(this.lvGroups);
            this.gbMonsterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMonsterGroups.Location = new System.Drawing.Point(0, 0);
            this.gbMonsterGroups.Name = "gbMonsterGroups";
            this.gbMonsterGroups.Size = new System.Drawing.Size(226, 119);
            this.gbMonsterGroups.TabIndex = 0;
            this.gbMonsterGroups.TabStop = false;
            this.gbMonsterGroups.Text = "Monster Groups";
            // 
            // gbGroupInfo
            // 
            this.gbGroupInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGroupInfo.Controls.Add(this.llKillAllMonsters);
            this.gbGroupInfo.Controls.Add(this.nudLiving);
            this.gbGroupInfo.Controls.Add(this.tbSearch);
            this.gbGroupInfo.Controls.Add(this.comboMonster);
            this.gbGroupInfo.Controls.Add(this.label12);
            this.gbGroupInfo.Controls.Add(this.label1);
            this.gbGroupInfo.Controls.Add(this.label3);
            this.gbGroupInfo.Location = new System.Drawing.Point(1, 0);
            this.gbGroupInfo.Name = "gbGroupInfo";
            this.gbGroupInfo.Size = new System.Drawing.Size(254, 119);
            this.gbGroupInfo.TabIndex = 0;
            this.gbGroupInfo.TabStop = false;
            this.gbGroupInfo.Text = "Group Info";
            // 
            // llKillAllMonsters
            // 
            this.llKillAllMonsters.AutoSize = true;
            this.llKillAllMonsters.Location = new System.Drawing.Point(170, 87);
            this.llKillAllMonsters.Name = "llKillAllMonsters";
            this.llKillAllMonsters.Size = new System.Drawing.Size(78, 13);
            this.llKillAllMonsters.TabIndex = 6;
            this.llKillAllMonsters.TabStop = true;
            this.llKillAllMonsters.Text = "&Kill all monsters";
            this.llKillAllMonsters.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llKillAllMonsters_LinkClicked);
            // 
            // nudLiving
            // 
            this.nudLiving.Enabled = false;
            this.nudLiving.Location = new System.Drawing.Point(60, 83);
            this.nudLiving.Name = "nudLiving";
            this.nudLiving.Size = new System.Drawing.Size(59, 20);
            this.nudLiving.TabIndex = 5;
            this.nudLiving.ValueChanged += new System.EventHandler(this.nudLiving_ValueChanged);
            this.nudLiving.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudLiving_KeyDown);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(60, 23);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(188, 20);
            this.tbSearch.TabIndex = 1;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // comboMonster
            // 
            this.comboMonster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMonster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMonster.FormattingEnabled = true;
            this.comboMonster.Location = new System.Drawing.Point(60, 52);
            this.comboMonster.Name = "comboMonster";
            this.comboMonster.Size = new System.Drawing.Size(188, 21);
            this.comboMonster.TabIndex = 3;
            this.comboMonster.SelectedIndexChanged += new System.EventHandler(this.comboMonster_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Search";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monster";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Living";
            // 
            // gbMonsters
            // 
            this.gbMonsters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMonsters.Controls.Add(this.lvMonsters);
            this.gbMonsters.Location = new System.Drawing.Point(12, 135);
            this.gbMonsters.Name = "gbMonsters";
            this.gbMonsters.Size = new System.Drawing.Size(226, 171);
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
            this.lvMonsters.Size = new System.Drawing.Size(220, 152);
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
            this.chHitPoints.Width = 78;
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
            this.btnCancel.Location = new System.Drawing.Point(422, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(422, 254);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
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
            this.nudHitPoints.Name = "nudHitPoints";
            this.nudHitPoints.Size = new System.Drawing.Size(89, 20);
            this.nudHitPoints.TabIndex = 1;
            this.nudHitPoints.ValueChanged += new System.EventHandler(this.nudHitPoints_ValueChanged);
            this.nudHitPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudHitPoints_KeyDown);
            // 
            // gbMonsterInfo
            // 
            this.gbMonsterInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMonsterInfo.Controls.Add(this.nudACModifier);
            this.gbMonsterInfo.Controls.Add(this.nudHitPoints);
            this.gbMonsterInfo.Controls.Add(this.label6);
            this.gbMonsterInfo.Controls.Add(this.comboCondition);
            this.gbMonsterInfo.Controls.Add(this.label5);
            this.gbMonsterInfo.Controls.Add(this.label4);
            this.gbMonsterInfo.Location = new System.Drawing.Point(243, 135);
            this.gbMonsterInfo.Name = "gbMonsterInfo";
            this.gbMonsterInfo.Size = new System.Drawing.Size(157, 93);
            this.gbMonsterInfo.TabIndex = 2;
            this.gbMonsterInfo.TabStop = false;
            this.gbMonsterInfo.Text = "Monster Info";
            // 
            // nudACModifier
            // 
            this.nudACModifier.Enabled = false;
            this.nudACModifier.Location = new System.Drawing.Point(62, 41);
            this.nudACModifier.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudACModifier.Minimum = new decimal(new int[] {
            32767,
            0,
            0,
            -2147483648});
            this.nudACModifier.Name = "nudACModifier";
            this.nudACModifier.Size = new System.Drawing.Size(89, 20);
            this.nudACModifier.TabIndex = 3;
            this.nudACModifier.Visible = false;
            this.nudACModifier.ValueChanged += new System.EventHandler(this.nudACModifier_ValueChanged);
            this.nudACModifier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudACModifier_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Condition";
            this.label6.Visible = false;
            // 
            // comboCondition
            // 
            this.comboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCondition.Enabled = false;
            this.comboCondition.FormattingEnabled = true;
            this.comboCondition.Location = new System.Drawing.Point(62, 64);
            this.comboCondition.Name = "comboCondition";
            this.comboCondition.Size = new System.Drawing.Size(89, 21);
            this.comboCondition.TabIndex = 5;
            this.comboCondition.Visible = false;
            this.comboCondition.SelectedIndexChanged += new System.EventHandler(this.comboCondition_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "AC";
            this.label5.Visible = false;
            // 
            // scGroupsInfo
            // 
            this.scGroupsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scGroupsInfo.Location = new System.Drawing.Point(12, 12);
            this.scGroupsInfo.Name = "scGroupsInfo";
            // 
            // scGroupsInfo.Panel1
            // 
            this.scGroupsInfo.Panel1.Controls.Add(this.gbMonsterGroups);
            // 
            // scGroupsInfo.Panel2
            // 
            this.scGroupsInfo.Panel2.Controls.Add(this.gbGroupInfo);
            this.scGroupsInfo.Size = new System.Drawing.Size(485, 119);
            this.scGroupsInfo.SplitterDistance = 226;
            this.scGroupsInfo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(241, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 70);
            this.label2.TabIndex = 3;
            this.label2.Text = "Note: Setting any individual monster\'s HP to zero will kill it and any other mons" +
    "ters further down in the list.";
            // 
            // BTEncounterEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(508, 317);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scGroupsInfo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbMonsterInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbMonsters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(516, 284);
            this.Name = "BTEncounterEditForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Encounter";
            this.gbMonsterGroups.ResumeLayout(false);
            this.gbGroupInfo.ResumeLayout(false);
            this.gbGroupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiving)).EndInit();
            this.gbMonsters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudHitPoints)).EndInit();
            this.gbMonsterInfo.ResumeLayout(false);
            this.gbMonsterInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudACModifier)).EndInit();
            this.scGroupsInfo.Panel1.ResumeLayout(false);
            this.scGroupsInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scGroupsInfo)).EndInit();
            this.scGroupsInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGroups;
        private System.Windows.Forms.ColumnHeader chGroup;
        private System.Windows.Forms.ColumnHeader chMonster;
        private System.Windows.Forms.ColumnHeader chAlive;
        private System.Windows.Forms.GroupBox gbMonsterGroups;
        private System.Windows.Forms.GroupBox gbGroupInfo;
        private System.Windows.Forms.ComboBox comboMonster;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.NumericUpDown nudACModifier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboCondition;
        private System.Windows.Forms.SplitContainer scGroupsInfo;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel llKillAllMonsters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLiving;
        private System.Windows.Forms.Label label3;
    }
}