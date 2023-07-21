namespace WhereAreWe
{
    partial class BT123TrainingAssistantControl : TrainingAssistantControl
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
            this.panelInvalid = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbGiveMaxHP = new System.Windows.Forms.CheckBox();
            this.labelDoneHP = new System.Windows.Forms.Label();
            this.lvBonusTable = new WhereAreWe.DBListView();
            this.chBonusRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonusValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvIQBonus = new WhereAreWe.DBListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.lvParty = new System.Windows.Forms.ListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExperience = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chConstitution = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHitPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHPPerLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSPPerLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbGiveMaxSP = new System.Windows.Forms.CheckBox();
            this.labelDoneSP = new System.Windows.Forms.Label();
            this.panelInvalid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInvalid
            // 
            this.panelInvalid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelInvalid.Controls.Add(this.label14);
            this.panelInvalid.Location = new System.Drawing.Point(6, 254);
            this.panelInvalid.Name = "panelInvalid";
            this.panelInvalid.Size = new System.Drawing.Size(170, 158);
            this.panelInvalid.TabIndex = 3;
            this.panelInvalid.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Party is not in a training hall!";
            // 
            // cbGiveMaxHP
            // 
            this.cbGiveMaxHP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGiveMaxHP.AutoSize = true;
            this.cbGiveMaxHP.Location = new System.Drawing.Point(6, 168);
            this.cbGiveMaxHP.Name = "cbGiveMaxHP";
            this.cbGiveMaxHP.Size = new System.Drawing.Size(167, 17);
            this.cbGiveMaxHP.TabIndex = 1;
            this.cbGiveMaxHP.Text = "Give maximum &HP on level-up";
            this.cbGiveMaxHP.UseVisualStyleBackColor = true;
            this.cbGiveMaxHP.CheckedChanged += new System.EventHandler(this.cbGiveMaxHP_CheckedChanged);
            // 
            // labelDoneHP
            // 
            this.labelDoneHP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDoneHP.AutoSize = true;
            this.labelDoneHP.Location = new System.Drawing.Point(23, 189);
            this.labelDoneHP.Name = "labelDoneHP";
            this.labelDoneHP.Size = new System.Drawing.Size(53, 13);
            this.labelDoneHP.TabIndex = 2;
            this.labelDoneHP.Text = "** done **";
            this.labelDoneHP.Visible = false;
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
            this.lvBonusTable.FullRowSelect = true;
            this.lvBonusTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBonusTable.HideSelection = false;
            this.lvBonusTable.Location = new System.Drawing.Point(3, 32);
            this.lvBonusTable.MultiSelect = false;
            this.lvBonusTable.Name = "lvBonusTable";
            this.lvBonusTable.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBonusTable.ShowItemToolTips = true;
            this.lvBonusTable.Size = new System.Drawing.Size(123, 120);
            this.lvBonusTable.TabIndex = 0;
            this.lvBonusTable.UseCompatibleStateImageBehavior = false;
            this.lvBonusTable.View = System.Windows.Forms.View.Details;
            // 
            // chBonusRange
            // 
            this.chBonusRange.Text = "Range";
            this.chBonusRange.Width = 59;
            // 
            // chBonusValue
            // 
            this.chBonusValue.Text = "Bonus";
            this.chBonusValue.Width = 42;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvBonusTable);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(208, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 155);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Constitution Bonus";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "HP/Level";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lvIQBonus);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(343, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 155);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IQ Bonus";
            // 
            // lvIQBonus
            // 
            this.lvIQBonus.AllowDrop = true;
            this.lvIQBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvIQBonus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvIQBonus.FullRowSelect = true;
            this.lvIQBonus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvIQBonus.HideSelection = false;
            this.lvIQBonus.Location = new System.Drawing.Point(3, 32);
            this.lvIQBonus.MultiSelect = false;
            this.lvIQBonus.Name = "lvIQBonus";
            this.lvIQBonus.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvIQBonus.ShowItemToolTips = true;
            this.lvIQBonus.Size = new System.Drawing.Size(123, 120);
            this.lvIQBonus.TabIndex = 0;
            this.lvIQBonus.UseCompatibleStateImageBehavior = false;
            this.lvIQBonus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Range";
            this.columnHeader1.Width = 56;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bonus";
            this.columnHeader2.Width = 42;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "SP/Level";
            // 
            // lvParty
            // 
            this.lvParty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvParty.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chName,
            this.chLevel,
            this.chExperience,
            this.chConstitution,
            this.chIQ,
            this.chHitPoints,
            this.chHPPerLevel,
            this.chSP,
            this.chSPPerLevel});
            this.lvParty.FullRowSelect = true;
            this.lvParty.Location = new System.Drawing.Point(3, 3);
            this.lvParty.Name = "lvParty";
            this.lvParty.Size = new System.Drawing.Size(469, 149);
            this.lvParty.TabIndex = 0;
            this.lvParty.UseCompatibleStateImageBehavior = false;
            this.lvParty.View = System.Windows.Forms.View.Details;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 20;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 100;
            // 
            // chLevel
            // 
            this.chLevel.Text = "Level";
            this.chLevel.Width = 40;
            // 
            // chExperience
            // 
            this.chExperience.Text = "Exp";
            // 
            // chConstitution
            // 
            this.chConstitution.Text = "Con";
            this.chConstitution.Width = 32;
            // 
            // chIQ
            // 
            this.chIQ.Text = "IQ";
            this.chIQ.Width = 32;
            // 
            // chHitPoints
            // 
            this.chHitPoints.Text = "HP";
            this.chHitPoints.Width = 40;
            // 
            // chHPPerLevel
            // 
            this.chHPPerLevel.Text = "HP/Lev";
            this.chHPPerLevel.Width = 50;
            // 
            // chSP
            // 
            this.chSP.Text = "SP";
            this.chSP.Width = 40;
            // 
            // chSPPerLevel
            // 
            this.chSPPerLevel.Text = "SP/Lev";
            this.chSPPerLevel.Width = 50;
            // 
            // cbGiveMaxSP
            // 
            this.cbGiveMaxSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGiveMaxSP.AutoSize = true;
            this.cbGiveMaxSP.Location = new System.Drawing.Point(6, 213);
            this.cbGiveMaxSP.Name = "cbGiveMaxSP";
            this.cbGiveMaxSP.Size = new System.Drawing.Size(166, 17);
            this.cbGiveMaxSP.TabIndex = 1;
            this.cbGiveMaxSP.Text = "Give maximum &SP on level-up";
            this.cbGiveMaxSP.UseVisualStyleBackColor = true;
            this.cbGiveMaxSP.CheckedChanged += new System.EventHandler(this.cbGiveMaxSP_CheckedChanged);
            // 
            // labelDoneSP
            // 
            this.labelDoneSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDoneSP.AutoSize = true;
            this.labelDoneSP.Location = new System.Drawing.Point(23, 235);
            this.labelDoneSP.Name = "labelDoneSP";
            this.labelDoneSP.Size = new System.Drawing.Size(53, 13);
            this.labelDoneSP.TabIndex = 2;
            this.labelDoneSP.Text = "** done **";
            this.labelDoneSP.Visible = false;
            // 
            // BT123TrainingAssistantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvParty);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelInvalid);
            this.Controls.Add(this.cbGiveMaxSP);
            this.Controls.Add(this.cbGiveMaxHP);
            this.Controls.Add(this.labelDoneSP);
            this.Controls.Add(this.labelDoneHP);
            this.MinimumSize = new System.Drawing.Size(475, 312);
            this.Name = "BT123TrainingAssistantControl";
            this.Size = new System.Drawing.Size(475, 312);
            this.panelInvalid.ResumeLayout(false);
            this.panelInvalid.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelInvalid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbGiveMaxHP;
        private System.Windows.Forms.Label labelDoneHP;
        private DBListView lvBonusTable;
        private System.Windows.Forms.ColumnHeader chBonusRange;
        private System.Windows.Forms.ColumnHeader chBonusValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DBListView lvIQBonus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvParty;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chExperience;
        private System.Windows.Forms.ColumnHeader chConstitution;
        private System.Windows.Forms.ColumnHeader chIQ;
        private System.Windows.Forms.ColumnHeader chHitPoints;
        private System.Windows.Forms.ColumnHeader chHPPerLevel;
        private System.Windows.Forms.ColumnHeader chSP;
        private System.Windows.Forms.ColumnHeader chSPPerLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbGiveMaxSP;
        private System.Windows.Forms.Label labelDoneSP;


    }
}
