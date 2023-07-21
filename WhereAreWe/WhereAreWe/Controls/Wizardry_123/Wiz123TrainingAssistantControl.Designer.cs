namespace WhereAreWe
{
    partial class Wiz123TrainingAssistantControl : TrainingAssistantControl
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
            this.gbChances = new System.Windows.Forms.GroupBox();
            this.lvEffects = new WhereAreWe.TipListView();
            this.chChance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEffect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvBonusTable = new WhereAreWe.DBListView();
            this.chBonusRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonusValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInvalid = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.cbGiveMax = new System.Windows.Forms.CheckBox();
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
            this.labelCharacter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.llNotes = new System.Windows.Forms.LinkLabel();
            this.cbPreventStatLoss = new System.Windows.Forms.CheckBox();
            this.gbChances.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelInvalid.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbChances
            // 
            this.gbChances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbChances.Controls.Add(this.lvEffects);
            this.gbChances.Location = new System.Drawing.Point(195, 3);
            this.gbChances.Name = "gbChances";
            this.gbChances.Size = new System.Drawing.Size(224, 265);
            this.gbChances.TabIndex = 18;
            this.gbChances.TabStop = false;
            this.gbChances.Text = "Level-Up Effects";
            // 
            // lvEffects
            // 
            this.lvEffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chChance,
            this.chEffect});
            this.lvEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEffects.FullRowSelect = true;
            this.lvEffects.Location = new System.Drawing.Point(3, 16);
            this.lvEffects.Name = "lvEffects";
            this.lvEffects.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvEffects.Size = new System.Drawing.Size(218, 246);
            this.lvEffects.TabIndex = 0;
            this.lvEffects.TipDelay = 700;
            this.lvEffects.TipDuration = 30000;
            this.lvEffects.UseCompatibleStateImageBehavior = false;
            this.lvEffects.View = System.Windows.Forms.View.Details;
            // 
            // chChance
            // 
            this.chChance.Text = "Chance";
            this.chChance.Width = 49;
            // 
            // chEffect
            // 
            this.chEffect.Text = "Effect";
            this.chEffect.Width = 147;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lvBonusTable);
            this.groupBox1.Location = new System.Drawing.Point(6, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 148);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vitality Bonus";
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
            this.lvBonusTable.Size = new System.Drawing.Size(100, 129);
            this.lvBonusTable.TabIndex = 0;
            this.lvBonusTable.UseCompatibleStateImageBehavior = false;
            this.lvBonusTable.View = System.Windows.Forms.View.Details;
            // 
            // chBonusRange
            // 
            this.chBonusRange.Text = "Range";
            this.chBonusRange.Width = 45;
            // 
            // chBonusValue
            // 
            this.chBonusValue.Text = "Bonus";
            this.chBonusValue.Width = 42;
            // 
            // panelInvalid
            // 
            this.panelInvalid.Controls.Add(this.label14);
            this.panelInvalid.Location = new System.Drawing.Point(139, 4);
            this.panelInvalid.Name = "panelInvalid";
            this.panelInvalid.Size = new System.Drawing.Size(161, 111);
            this.panelInvalid.TabIndex = 17;
            this.panelInvalid.Visible = false;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(13, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 39);
            this.label14.TabIndex = 0;
            this.label14.Text = "Party is not in the Adventurer\'s Inn!";
            // 
            // cbGiveMax
            // 
            this.cbGiveMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbGiveMax.AutoSize = true;
            this.cbGiveMax.Location = new System.Drawing.Point(7, 274);
            this.cbGiveMax.Name = "cbGiveMax";
            this.cbGiveMax.Size = new System.Drawing.Size(167, 17);
            this.cbGiveMax.TabIndex = 15;
            this.cbGiveMax.Text = "&Give maximum HP on level-up";
            this.cbGiveMax.UseVisualStyleBackColor = true;
            // 
            // labelCharName
            // 
            this.labelCharName.AutoSize = true;
            this.labelCharName.Location = new System.Drawing.Point(72, 9);
            this.labelCharName.Name = "labelCharName";
            this.labelCharName.Size = new System.Drawing.Size(68, 13);
            this.labelCharName.TabIndex = 1;
            this.labelCharName.Text = "CHARNAME";
            // 
            // labelHPLevel
            // 
            this.labelHPLevel.AutoSize = true;
            this.labelHPLevel.Location = new System.Drawing.Point(72, 99);
            this.labelHPLevel.Name = "labelHPLevel";
            this.labelHPLevel.Size = new System.Drawing.Size(60, 13);
            this.labelHPLevel.TabIndex = 13;
            this.labelHPLevel.Text = "HP/LEVEL";
            // 
            // labelHitPoints
            // 
            this.labelHitPoints.AutoSize = true;
            this.labelHitPoints.Location = new System.Drawing.Point(72, 84);
            this.labelHitPoints.Name = "labelHitPoints";
            this.labelHitPoints.Size = new System.Drawing.Size(22, 13);
            this.labelHitPoints.TabIndex = 11;
            this.labelHitPoints.Text = "HP";
            // 
            // labelEndurance
            // 
            this.labelEndurance.AutoSize = true;
            this.labelEndurance.Location = new System.Drawing.Point(72, 69);
            this.labelEndurance.Name = "labelEndurance";
            this.labelEndurance.Size = new System.Drawing.Size(30, 13);
            this.labelEndurance.TabIndex = 9;
            this.labelEndurance.Text = "END";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(72, 24);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(40, 13);
            this.labelLevel.TabIndex = 3;
            this.labelLevel.Text = "LEVEL";
            // 
            // labelExperience
            // 
            this.labelExperience.AutoSize = true;
            this.labelExperience.Location = new System.Drawing.Point(72, 54);
            this.labelExperience.Name = "labelExperience";
            this.labelExperience.Size = new System.Drawing.Size(28, 13);
            this.labelExperience.TabIndex = 7;
            this.labelExperience.Text = "EXP";
            // 
            // labelDone
            // 
            this.labelDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDone.AutoSize = true;
            this.labelDone.Location = new System.Drawing.Point(198, 294);
            this.labelDone.Name = "labelDone";
            this.labelDone.Size = new System.Drawing.Size(53, 13);
            this.labelDone.TabIndex = 20;
            this.labelDone.Text = "** done **";
            this.labelDone.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "HP/Level:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hit Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Vitality:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Experience:";
            // 
            // labelCharacter
            // 
            this.labelCharacter.AutoSize = true;
            this.labelCharacter.Location = new System.Drawing.Point(3, 9);
            this.labelCharacter.Name = "labelCharacter";
            this.labelCharacter.Size = new System.Drawing.Size(56, 13);
            this.labelCharacter.TabIndex = 0;
            this.labelCharacter.Text = "Character:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Age:";
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Location = new System.Drawing.Point(72, 39);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(29, 13);
            this.labelAge.TabIndex = 5;
            this.labelAge.Text = "AGE";
            // 
            // llNotes
            // 
            this.llNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llNotes.AutoSize = true;
            this.llNotes.Location = new System.Drawing.Point(6, 294);
            this.llNotes.Name = "llNotes";
            this.llNotes.Size = new System.Drawing.Size(159, 13);
            this.llNotes.TabIndex = 16;
            this.llNotes.TabStop = true;
            this.llNotes.Text = "&Notes about hit point calculation";
            this.llNotes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.llNotes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNotes_LinkClicked);
            // 
            // cbPreventStatLoss
            // 
            this.cbPreventStatLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPreventStatLoss.AutoSize = true;
            this.cbPreventStatLoss.Location = new System.Drawing.Point(201, 274);
            this.cbPreventStatLoss.Name = "cbPreventStatLoss";
            this.cbPreventStatLoss.Size = new System.Drawing.Size(176, 17);
            this.cbPreventStatLoss.TabIndex = 19;
            this.cbPreventStatLoss.Text = "&Prevent loss of stats on level-up";
            this.cbPreventStatLoss.UseVisualStyleBackColor = true;
            this.cbPreventStatLoss.CheckedChanged += new System.EventHandler(this.cbPreventStatLoss_CheckedChanged);
            // 
            // Wiz1TrainingAssistantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.llNotes);
            this.Controls.Add(this.gbChances);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbPreventStatLoss);
            this.Controls.Add(this.cbGiveMax);
            this.Controls.Add(this.labelCharName);
            this.Controls.Add(this.labelHPLevel);
            this.Controls.Add(this.labelHitPoints);
            this.Controls.Add(this.labelEndurance);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelExperience);
            this.Controls.Add(this.labelDone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelCharacter);
            this.Controls.Add(this.panelInvalid);
            this.MinimumSize = new System.Drawing.Size(419, 313);
            this.Name = "Wiz1TrainingAssistantControl";
            this.Size = new System.Drawing.Size(419, 313);
            this.gbChances.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panelInvalid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChances;
        private TipListView lvEffects;
        private System.Windows.Forms.ColumnHeader chChance;
        private System.Windows.Forms.ColumnHeader chEffect;
        private System.Windows.Forms.GroupBox groupBox1;
        private DBListView lvBonusTable;
        private System.Windows.Forms.ColumnHeader chBonusRange;
        private System.Windows.Forms.ColumnHeader chBonusValue;
        private System.Windows.Forms.Panel panelInvalid;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbGiveMax;
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
        private System.Windows.Forms.Label labelCharacter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.LinkLabel llNotes;
        private System.Windows.Forms.CheckBox cbPreventStatLoss;

    }
}
