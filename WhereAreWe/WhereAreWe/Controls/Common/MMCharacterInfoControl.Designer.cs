namespace WhereAreWe
{
    partial class MMCharacterInfoControl
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
            this.labelRangedHeader = new System.Windows.Forms.Label();
            this.groupPrimaryStats = new System.Windows.Forms.GroupBox();
            this.labelStatPos0 = new System.Windows.Forms.Label();
            this.labelStatPos1 = new System.Windows.Forms.Label();
            this.labelIntellectHeader = new System.Windows.Forms.Label();
            this.labelMightHeader = new System.Windows.Forms.Label();
            this.labelStatPos2 = new System.Windows.Forms.Label();
            this.labelIntellect = new WhereAreWe.EditableAttributeLabel();
            this.label28 = new System.Windows.Forms.Label();
            this.labelPersonalityHeader = new System.Windows.Forms.Label();
            this.labelStatPos3 = new System.Windows.Forms.Label();
            this.labelMight = new WhereAreWe.EditableAttributeLabel();
            this.labelEnduranceHeader = new System.Windows.Forms.Label();
            this.labelStatPos4 = new System.Windows.Forms.Label();
            this.labelPersonality = new WhereAreWe.EditableAttributeLabel();
            this.label24 = new System.Windows.Forms.Label();
            this.labelSpeedHeader = new System.Windows.Forms.Label();
            this.labelStatPos5 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.labelEndurance = new WhereAreWe.EditableAttributeLabel();
            this.labelAccuracyHeader = new System.Windows.Forms.Label();
            this.labelStatPos6 = new System.Windows.Forms.Label();
            this.labelSpeed = new WhereAreWe.EditableAttributeLabel();
            this.labelLuckHeader = new System.Windows.Forms.Label();
            this.labelAccuracy = new WhereAreWe.EditableAttributeLabel();
            this.labelLuck = new WhereAreWe.EditableAttributeLabel();
            this.labelRanged = new System.Windows.Forms.Label();
            this.labelThieveryHeader = new System.Windows.Forms.Label();
            this.labelThievery = new WhereAreWe.EditableAttributeLabel();
            this.groupPrimaryStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRangedHeader
            // 
            this.labelRangedHeader.AutoSize = true;
            this.labelRangedHeader.Location = new System.Drawing.Point(109, 109);
            this.labelRangedHeader.Name = "labelRangedHeader";
            this.labelRangedHeader.Size = new System.Drawing.Size(45, 13);
            this.labelRangedHeader.TabIndex = 37;
            this.labelRangedHeader.Text = "Ranged";
            this.labelRangedHeader.MouseEnter += new System.EventHandler(this.labelRanged_MouseEnter);
            this.labelRangedHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // groupPrimaryStats
            // 
            this.groupPrimaryStats.Controls.Add(this.labelStatPos0);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos1);
            this.groupPrimaryStats.Controls.Add(this.labelIntellectHeader);
            this.groupPrimaryStats.Controls.Add(this.labelMightHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos2);
            this.groupPrimaryStats.Controls.Add(this.labelIntellect);
            this.groupPrimaryStats.Controls.Add(this.label28);
            this.groupPrimaryStats.Controls.Add(this.labelPersonalityHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos3);
            this.groupPrimaryStats.Controls.Add(this.labelMight);
            this.groupPrimaryStats.Controls.Add(this.labelEnduranceHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos4);
            this.groupPrimaryStats.Controls.Add(this.labelPersonality);
            this.groupPrimaryStats.Controls.Add(this.label24);
            this.groupPrimaryStats.Controls.Add(this.labelSpeedHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos5);
            this.groupPrimaryStats.Controls.Add(this.label23);
            this.groupPrimaryStats.Controls.Add(this.labelEndurance);
            this.groupPrimaryStats.Controls.Add(this.labelAccuracyHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos6);
            this.groupPrimaryStats.Controls.Add(this.labelSpeed);
            this.groupPrimaryStats.Controls.Add(this.labelLuckHeader);
            this.groupPrimaryStats.Controls.Add(this.labelAccuracy);
            this.groupPrimaryStats.Controls.Add(this.labelLuck);
            this.groupPrimaryStats.Location = new System.Drawing.Point(3, 19);
            this.groupPrimaryStats.Name = "groupPrimaryStats";
            this.groupPrimaryStats.Size = new System.Drawing.Size(106, 125);
            this.groupPrimaryStats.TabIndex = 34;
            this.groupPrimaryStats.TabStop = false;
            this.groupPrimaryStats.Text = "Primary Stats";
            // 
            // labelStatPos0
            // 
            this.labelStatPos0.AutoSize = true;
            this.labelStatPos0.Location = new System.Drawing.Point(83, 15);
            this.labelStatPos0.Name = "labelStatPos0";
            this.labelStatPos0.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos0.TabIndex = 2;
            this.labelStatPos0.Text = "...";
            this.labelStatPos0.Visible = false;
            // 
            // labelStatPos1
            // 
            this.labelStatPos1.AutoSize = true;
            this.labelStatPos1.Location = new System.Drawing.Point(83, 30);
            this.labelStatPos1.Name = "labelStatPos1";
            this.labelStatPos1.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos1.TabIndex = 5;
            this.labelStatPos1.Text = "...";
            this.labelStatPos1.Visible = false;
            // 
            // labelIntellectHeader
            // 
            this.labelIntellectHeader.AutoSize = true;
            this.labelIntellectHeader.Location = new System.Drawing.Point(3, 15);
            this.labelIntellectHeader.Name = "labelIntellectHeader";
            this.labelIntellectHeader.Size = new System.Drawing.Size(25, 13);
            this.labelIntellectHeader.TabIndex = 0;
            this.labelIntellectHeader.Text = "INT";
            this.labelIntellectHeader.MouseEnter += new System.EventHandler(this.labelIntellect_MouseEnter);
            this.labelIntellectHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelMightHeader
            // 
            this.labelMightHeader.AutoSize = true;
            this.labelMightHeader.Location = new System.Drawing.Point(3, 30);
            this.labelMightHeader.Name = "labelMightHeader";
            this.labelMightHeader.Size = new System.Drawing.Size(31, 13);
            this.labelMightHeader.TabIndex = 3;
            this.labelMightHeader.Text = "MGT";
            this.labelMightHeader.MouseEnter += new System.EventHandler(this.labelMight_MouseEnter);
            this.labelMightHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos2
            // 
            this.labelStatPos2.AutoSize = true;
            this.labelStatPos2.Location = new System.Drawing.Point(83, 45);
            this.labelStatPos2.Name = "labelStatPos2";
            this.labelStatPos2.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos2.TabIndex = 8;
            this.labelStatPos2.Text = "...";
            this.labelStatPos2.Visible = false;
            // 
            // labelIntellect
            // 
            this.labelIntellect.AutoSize = true;
            this.labelIntellect.Location = new System.Drawing.Point(34, 15);
            this.labelIntellect.Name = "labelIntellect";
            this.labelIntellect.Size = new System.Drawing.Size(25, 13);
            this.labelIntellect.TabIndex = 1;
            this.labelIntellect.Text = "INT";
            this.labelIntellect.MouseEnter += new System.EventHandler(this.labelIntellect_MouseEnter);
            this.labelIntellect.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(106, 90);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 14;
            this.label28.Text = "Ranged";
            // 
            // labelPersonalityHeader
            // 
            this.labelPersonalityHeader.AutoSize = true;
            this.labelPersonalityHeader.Location = new System.Drawing.Point(3, 45);
            this.labelPersonalityHeader.Name = "labelPersonalityHeader";
            this.labelPersonalityHeader.Size = new System.Drawing.Size(29, 13);
            this.labelPersonalityHeader.TabIndex = 6;
            this.labelPersonalityHeader.Text = "PER";
            this.labelPersonalityHeader.MouseEnter += new System.EventHandler(this.labelEndurance_MouseEnter);
            this.labelPersonalityHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos3
            // 
            this.labelStatPos3.AutoSize = true;
            this.labelStatPos3.Location = new System.Drawing.Point(83, 60);
            this.labelStatPos3.Name = "labelStatPos3";
            this.labelStatPos3.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos3.TabIndex = 11;
            this.labelStatPos3.Text = "...";
            this.labelStatPos3.Visible = false;
            // 
            // labelMight
            // 
            this.labelMight.AutoSize = true;
            this.labelMight.Location = new System.Drawing.Point(34, 30);
            this.labelMight.Name = "labelMight";
            this.labelMight.Size = new System.Drawing.Size(31, 13);
            this.labelMight.TabIndex = 4;
            this.labelMight.Text = "MGT";
            this.labelMight.MouseEnter += new System.EventHandler(this.labelMight_MouseEnter);
            this.labelMight.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelEnduranceHeader
            // 
            this.labelEnduranceHeader.AutoSize = true;
            this.labelEnduranceHeader.Location = new System.Drawing.Point(3, 60);
            this.labelEnduranceHeader.Name = "labelEnduranceHeader";
            this.labelEnduranceHeader.Size = new System.Drawing.Size(30, 13);
            this.labelEnduranceHeader.TabIndex = 9;
            this.labelEnduranceHeader.Text = "END";
            this.labelEnduranceHeader.MouseEnter += new System.EventHandler(this.labelEndurance_MouseEnter);
            this.labelEnduranceHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos4
            // 
            this.labelStatPos4.AutoSize = true;
            this.labelStatPos4.Location = new System.Drawing.Point(83, 75);
            this.labelStatPos4.Name = "labelStatPos4";
            this.labelStatPos4.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos4.TabIndex = 14;
            this.labelStatPos4.Text = "...";
            this.labelStatPos4.Visible = false;
            // 
            // labelPersonality
            // 
            this.labelPersonality.AutoSize = true;
            this.labelPersonality.Location = new System.Drawing.Point(34, 45);
            this.labelPersonality.Name = "labelPersonality";
            this.labelPersonality.Size = new System.Drawing.Size(29, 13);
            this.labelPersonality.TabIndex = 7;
            this.labelPersonality.Text = "PER";
            this.labelPersonality.MouseEnter += new System.EventHandler(this.labelEndurance_MouseEnter);
            this.labelPersonality.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(106, 60);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 8;
            this.label24.Text = "Thievery";
            // 
            // labelSpeedHeader
            // 
            this.labelSpeedHeader.AutoSize = true;
            this.labelSpeedHeader.Location = new System.Drawing.Point(3, 75);
            this.labelSpeedHeader.Name = "labelSpeedHeader";
            this.labelSpeedHeader.Size = new System.Drawing.Size(29, 13);
            this.labelSpeedHeader.TabIndex = 12;
            this.labelSpeedHeader.Text = "SPD";
            this.labelSpeedHeader.MouseEnter += new System.EventHandler(this.labelSpeed_MouseEnter);
            this.labelSpeedHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos5
            // 
            this.labelStatPos5.AutoSize = true;
            this.labelStatPos5.Location = new System.Drawing.Point(83, 90);
            this.labelStatPos5.Name = "labelStatPos5";
            this.labelStatPos5.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos5.TabIndex = 17;
            this.labelStatPos5.Text = "...";
            this.labelStatPos5.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(106, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(36, 13);
            this.label23.TabIndex = 12;
            this.label23.Text = "Melee";
            // 
            // labelEndurance
            // 
            this.labelEndurance.AutoSize = true;
            this.labelEndurance.Location = new System.Drawing.Point(34, 60);
            this.labelEndurance.Name = "labelEndurance";
            this.labelEndurance.Size = new System.Drawing.Size(30, 13);
            this.labelEndurance.TabIndex = 10;
            this.labelEndurance.Text = "END";
            this.labelEndurance.MouseEnter += new System.EventHandler(this.labelEndurance_MouseEnter);
            this.labelEndurance.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelAccuracyHeader
            // 
            this.labelAccuracyHeader.AutoSize = true;
            this.labelAccuracyHeader.Location = new System.Drawing.Point(3, 90);
            this.labelAccuracyHeader.Name = "labelAccuracyHeader";
            this.labelAccuracyHeader.Size = new System.Drawing.Size(28, 13);
            this.labelAccuracyHeader.TabIndex = 15;
            this.labelAccuracyHeader.Text = "ACY";
            this.labelAccuracyHeader.MouseEnter += new System.EventHandler(this.labelAccuracy_MouseEnter);
            this.labelAccuracyHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos6
            // 
            this.labelStatPos6.AutoSize = true;
            this.labelStatPos6.Location = new System.Drawing.Point(83, 105);
            this.labelStatPos6.Name = "labelStatPos6";
            this.labelStatPos6.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos6.TabIndex = 20;
            this.labelStatPos6.Text = "...";
            this.labelStatPos6.Visible = false;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(34, 75);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(29, 13);
            this.labelSpeed.TabIndex = 13;
            this.labelSpeed.Text = "SPD";
            this.labelSpeed.MouseEnter += new System.EventHandler(this.labelSpeed_MouseEnter);
            this.labelSpeed.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelLuckHeader
            // 
            this.labelLuckHeader.AutoSize = true;
            this.labelLuckHeader.Location = new System.Drawing.Point(3, 105);
            this.labelLuckHeader.Name = "labelLuckHeader";
            this.labelLuckHeader.Size = new System.Drawing.Size(27, 13);
            this.labelLuckHeader.TabIndex = 18;
            this.labelLuckHeader.Text = "LCK";
            this.labelLuckHeader.MouseEnter += new System.EventHandler(this.labelLuckHeader_MouseEnter);
            this.labelLuckHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelAccuracy
            // 
            this.labelAccuracy.AutoSize = true;
            this.labelAccuracy.Location = new System.Drawing.Point(34, 90);
            this.labelAccuracy.Name = "labelAccuracy";
            this.labelAccuracy.Size = new System.Drawing.Size(28, 13);
            this.labelAccuracy.TabIndex = 16;
            this.labelAccuracy.Text = "ACY";
            this.labelAccuracy.MouseEnter += new System.EventHandler(this.labelAccuracy_MouseEnter);
            this.labelAccuracy.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelLuck
            // 
            this.labelLuck.AutoSize = true;
            this.labelLuck.Location = new System.Drawing.Point(34, 105);
            this.labelLuck.Name = "labelLuck";
            this.labelLuck.Size = new System.Drawing.Size(28, 13);
            this.labelLuck.TabIndex = 19;
            this.labelLuck.Text = "LUC";
            this.labelLuck.MouseEnter += new System.EventHandler(this.labelLuckHeader_MouseEnter);
            this.labelLuck.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelRanged
            // 
            this.labelRanged.AutoSize = true;
            this.labelRanged.Location = new System.Drawing.Point(157, 109);
            this.labelRanged.Name = "labelRanged";
            this.labelRanged.Size = new System.Drawing.Size(45, 13);
            this.labelRanged.TabIndex = 38;
            this.labelRanged.Text = "RANGE";
            this.labelRanged.MouseEnter += new System.EventHandler(this.labelRanged_MouseEnter);
            this.labelRanged.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelThieveryHeader
            // 
            this.labelThieveryHeader.AutoSize = true;
            this.labelThieveryHeader.Location = new System.Drawing.Point(109, 79);
            this.labelThieveryHeader.Name = "labelThieveryHeader";
            this.labelThieveryHeader.Size = new System.Drawing.Size(48, 13);
            this.labelThieveryHeader.TabIndex = 35;
            this.labelThieveryHeader.Text = "Thievery";
            this.labelThieveryHeader.MouseEnter += new System.EventHandler(this.labelThievery_MouseEnter);
            this.labelThieveryHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelThievery
            // 
            this.labelThievery.AutoSize = true;
            this.labelThievery.Location = new System.Drawing.Point(157, 79);
            this.labelThievery.Name = "labelThievery";
            this.labelThievery.Size = new System.Drawing.Size(38, 13);
            this.labelThievery.TabIndex = 36;
            this.labelThievery.Text = "THIEF";
            this.labelThievery.MouseEnter += new System.EventHandler(this.labelThievery_MouseEnter);
            this.labelThievery.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // MMCharacterInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelRangedHeader);
            this.Controls.Add(this.groupPrimaryStats);
            this.Controls.Add(this.labelRanged);
            this.Controls.Add(this.labelThieveryHeader);
            this.Controls.Add(this.labelThievery);
            this.Name = "MMCharacterInfoControl";
            this.Controls.SetChildIndex(this.labelThievery, 0);
            this.Controls.SetChildIndex(this.labelThieveryHeader, 0);
            this.Controls.SetChildIndex(this.labelRanged, 0);
            this.Controls.SetChildIndex(this.groupPrimaryStats, 0);
            this.Controls.SetChildIndex(this.labelRangedHeader, 0);
            this.groupPrimaryStats.ResumeLayout(false);
            this.groupPrimaryStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRangedHeader;
        private System.Windows.Forms.GroupBox groupPrimaryStats;
        private System.Windows.Forms.Label labelStatPos0;
        private System.Windows.Forms.Label labelStatPos1;
        private System.Windows.Forms.Label labelIntellectHeader;
        private System.Windows.Forms.Label labelMightHeader;
        private System.Windows.Forms.Label labelStatPos2;
        private EditableAttributeLabel labelIntellect;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label labelPersonalityHeader;
        private System.Windows.Forms.Label labelStatPos3;
        private EditableAttributeLabel labelMight;
        private System.Windows.Forms.Label labelEnduranceHeader;
        private System.Windows.Forms.Label labelStatPos4;
        private EditableAttributeLabel labelPersonality;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label labelSpeedHeader;
        private System.Windows.Forms.Label labelStatPos5;
        private System.Windows.Forms.Label label23;
        private EditableAttributeLabel labelEndurance;
        private System.Windows.Forms.Label labelAccuracyHeader;
        private System.Windows.Forms.Label labelStatPos6;
        private EditableAttributeLabel labelSpeed;
        private System.Windows.Forms.Label labelLuckHeader;
        private EditableAttributeLabel labelAccuracy;
        private EditableAttributeLabel labelLuck;
        private System.Windows.Forms.Label labelRanged;
        private System.Windows.Forms.Label labelThieveryHeader;
        private EditableAttributeLabel labelThievery;
    }
}
