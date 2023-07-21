namespace WhereAreWe
{
    partial class MM1TrainingAssistantControl : TrainingAssistantControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvBonusTable = new WhereAreWe.DBListView();
            this.chBonusRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonusValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInvalid = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbGiveMax = new System.Windows.Forms.CheckBox();
            this.labelMapName = new System.Windows.Forms.Label();
            this.labelCharName = new System.Windows.Forms.Label();
            this.labelHPLevel = new System.Windows.Forms.Label();
            this.labelHitPoints = new System.Windows.Forms.Label();
            this.labelEndurance = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelExperience = new System.Windows.Forms.Label();
            this.labelDone = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panelInvalid.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvBonusTable);
            this.groupBox1.Location = new System.Drawing.Point(195, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 149);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Endurance Bonus Table";
            // 
            // lvBonusTable
            // 
            this.lvBonusTable.AllowDrop = true;
            this.lvBonusTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBonusRange,
            this.chBonusValue});
            this.lvBonusTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBonusTable.FullRowSelect = true;
            this.lvBonusTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBonusTable.HideSelection = false;
            this.lvBonusTable.Location = new System.Drawing.Point(3, 16);
            this.lvBonusTable.MultiSelect = false;
            this.lvBonusTable.Name = "lvBonusTable";
            this.lvBonusTable.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBonusTable.ShowItemToolTips = true;
            this.lvBonusTable.Size = new System.Drawing.Size(139, 130);
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
            // panelInvalid
            // 
            this.panelInvalid.Controls.Add(this.label14);
            this.panelInvalid.Location = new System.Drawing.Point(3, 163);
            this.panelInvalid.Name = "panelInvalid";
            this.panelInvalid.Size = new System.Drawing.Size(170, 158);
            this.panelInvalid.TabIndex = 29;
            this.panelInvalid.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Party is not in a training hall!";
            // 
            // cbGiveMax
            // 
            this.cbGiveMax.AutoSize = true;
            this.cbGiveMax.Location = new System.Drawing.Point(6, 119);
            this.cbGiveMax.Name = "cbGiveMax";
            this.cbGiveMax.Size = new System.Drawing.Size(167, 17);
            this.cbGiveMax.TabIndex = 28;
            this.cbGiveMax.Text = "&Give maximum HP on level-up";
            this.cbGiveMax.UseVisualStyleBackColor = true;
            // 
            // labelMapName
            // 
            this.labelMapName.AutoSize = true;
            this.labelMapName.Location = new System.Drawing.Point(72, 3);
            this.labelMapName.Name = "labelMapName";
            this.labelMapName.Size = new System.Drawing.Size(61, 13);
            this.labelMapName.TabIndex = 25;
            this.labelMapName.Text = "MAPNAME";
            // 
            // labelCharName
            // 
            this.labelCharName.AutoSize = true;
            this.labelCharName.Location = new System.Drawing.Point(72, 18);
            this.labelCharName.Name = "labelCharName";
            this.labelCharName.Size = new System.Drawing.Size(68, 13);
            this.labelCharName.TabIndex = 24;
            this.labelCharName.Text = "CHARNAME";
            // 
            // labelHPLevel
            // 
            this.labelHPLevel.AutoSize = true;
            this.labelHPLevel.Location = new System.Drawing.Point(72, 93);
            this.labelHPLevel.Name = "labelHPLevel";
            this.labelHPLevel.Size = new System.Drawing.Size(60, 13);
            this.labelHPLevel.TabIndex = 23;
            this.labelHPLevel.Text = "HP/LEVEL";
            // 
            // labelHitPoints
            // 
            this.labelHitPoints.AutoSize = true;
            this.labelHitPoints.Location = new System.Drawing.Point(72, 78);
            this.labelHitPoints.Name = "labelHitPoints";
            this.labelHitPoints.Size = new System.Drawing.Size(22, 13);
            this.labelHitPoints.TabIndex = 22;
            this.labelHitPoints.Text = "HP";
            // 
            // labelEndurance
            // 
            this.labelEndurance.AutoSize = true;
            this.labelEndurance.Location = new System.Drawing.Point(72, 63);
            this.labelEndurance.Name = "labelEndurance";
            this.labelEndurance.Size = new System.Drawing.Size(30, 13);
            this.labelEndurance.TabIndex = 26;
            this.labelEndurance.Text = "END";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(72, 33);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(40, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "LEVEL";
            // 
            // labelExperience
            // 
            this.labelExperience.AutoSize = true;
            this.labelExperience.Location = new System.Drawing.Point(72, 48);
            this.labelExperience.Name = "labelExperience";
            this.labelExperience.Size = new System.Drawing.Size(28, 13);
            this.labelExperience.TabIndex = 19;
            this.labelExperience.Text = "EXP";
            // 
            // labelDone
            // 
            this.labelDone.AutoSize = true;
            this.labelDone.Location = new System.Drawing.Point(25, 139);
            this.labelDone.Name = "labelDone";
            this.labelDone.Size = new System.Drawing.Size(53, 13);
            this.labelDone.TabIndex = 18;
            this.labelDone.Text = "** done **";
            this.labelDone.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "HP/Level:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Hit Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Endurance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Experience:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Map:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Character:";
            // 
            // MM1TrainingAssistantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelInvalid);
            this.Controls.Add(this.cbGiveMax);
            this.Controls.Add(this.labelMapName);
            this.Controls.Add(this.labelCharName);
            this.Controls.Add(this.labelHPLevel);
            this.Controls.Add(this.labelHitPoints);
            this.Controls.Add(this.labelEndurance);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelExperience);
            this.Controls.Add(this.labelDone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(340, 170);
            this.Name = "MM1TrainingAssistantControl";
            this.Size = new System.Drawing.Size(340, 170);
            this.groupBox1.ResumeLayout(false);
            this.panelInvalid.ResumeLayout(false);
            this.panelInvalid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DBListView lvBonusTable;
        private System.Windows.Forms.ColumnHeader chBonusRange;
        private System.Windows.Forms.ColumnHeader chBonusValue;
        private System.Windows.Forms.Panel panelInvalid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbGiveMax;
        private System.Windows.Forms.Label labelMapName;
        private System.Windows.Forms.Label labelCharName;
        private System.Windows.Forms.Label labelHPLevel;
        private System.Windows.Forms.Label labelHitPoints;
        private System.Windows.Forms.Label labelEndurance;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelExperience;
        private System.Windows.Forms.Label labelDone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;


    }
}
