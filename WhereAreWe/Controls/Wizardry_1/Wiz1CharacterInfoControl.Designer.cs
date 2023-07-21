namespace WhereAreWe
{
    partial class Wiz1CharacterInfoControl
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
            this.labelGoldHeader = new System.Windows.Forms.Label();
            this.labelGold = new WhereAreWe.EditableAttributeLabel();
            this.groupPrimaryStats = new System.Windows.Forms.GroupBox();
            this.labelIQHeader = new System.Windows.Forms.Label();
            this.labelStrengthHeader = new System.Windows.Forms.Label();
            this.labelIQ = new WhereAreWe.EditableAttributeLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPietyHeader = new System.Windows.Forms.Label();
            this.labelStrength = new WhereAreWe.EditableAttributeLabel();
            this.labelVitalityHeader = new System.Windows.Forms.Label();
            this.labelPiety = new WhereAreWe.EditableAttributeLabel();
            this.label24 = new System.Windows.Forms.Label();
            this.labelAgilityHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelVitality = new WhereAreWe.EditableAttributeLabel();
            this.labelAgility = new WhereAreWe.EditableAttributeLabel();
            this.labelLuckHeader = new System.Windows.Forms.Label();
            this.labelLuck = new WhereAreWe.EditableAttributeLabel();
            this.labelStatPos0 = new System.Windows.Forms.Label();
            this.labelStatPos1 = new System.Windows.Forms.Label();
            this.labelStatPos2 = new System.Windows.Forms.Label();
            this.labelStatPos3 = new System.Windows.Forms.Label();
            this.labelStatPos4 = new System.Windows.Forms.Label();
            this.labelStatPos6 = new System.Windows.Forms.Label();
            this.labelKnownSpells = new WhereAreWe.EditableAttributeLabel();
            this.label29 = new System.Windows.Forms.Label();
            this.cmView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miViewView = new System.Windows.Forms.ToolStripMenuItem();
            this.labelMoveHP = new System.Windows.Forms.Label();
            this.labelMoveSP = new System.Windows.Forms.Label();
            this.labelMoveAC = new System.Windows.Forms.Label();
            this.labelRegen = new WhereAreWe.EditableAttributeLabel();
            this.labelRegenHeader = new System.Windows.Forms.Label();
            this.labelPoison = new WhereAreWe.EditableAttributeLabel();
            this.labelPoisonHeader = new System.Windows.Forms.Label();
            this.labelIDTraps = new WhereAreWe.EditableAttributeLabel();
            this.labelIDTrapsHeader = new System.Windows.Forms.Label();
            this.gbTraps = new System.Windows.Forms.GroupBox();
            this.labelDisarmHeader = new System.Windows.Forms.Label();
            this.labelDisarm = new WhereAreWe.EditableAttributeLabel();
            this.labelSpellLevel = new WhereAreWe.EditableAttributeLabel();
            this.groupPrimaryStats.SuspendLayout();
            this.cmView.SuspendLayout();
            this.gbTraps.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelGoldHeader
            // 
            this.labelGoldHeader.AutoSize = true;
            this.labelGoldHeader.Location = new System.Drawing.Point(109, 34);
            this.labelGoldHeader.Name = "labelGoldHeader";
            this.labelGoldHeader.Size = new System.Drawing.Size(29, 13);
            this.labelGoldHeader.TabIndex = 38;
            this.labelGoldHeader.Text = "Gold";
            // 
            // labelGold
            // 
            this.labelGold.AutoSize = true;
            this.labelGold.Location = new System.Drawing.Point(146, 34);
            this.labelGold.Name = "labelGold";
            this.labelGold.Size = new System.Drawing.Size(37, 13);
            this.labelGold.TabIndex = 41;
            this.labelGold.Text = "GOLD";
            // 
            // groupPrimaryStats
            // 
            this.groupPrimaryStats.Controls.Add(this.labelIQHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStrengthHeader);
            this.groupPrimaryStats.Controls.Add(this.labelIQ);
            this.groupPrimaryStats.Controls.Add(this.label1);
            this.groupPrimaryStats.Controls.Add(this.labelPietyHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStrength);
            this.groupPrimaryStats.Controls.Add(this.labelVitalityHeader);
            this.groupPrimaryStats.Controls.Add(this.labelPiety);
            this.groupPrimaryStats.Controls.Add(this.label24);
            this.groupPrimaryStats.Controls.Add(this.labelAgilityHeader);
            this.groupPrimaryStats.Controls.Add(this.label2);
            this.groupPrimaryStats.Controls.Add(this.labelVitality);
            this.groupPrimaryStats.Controls.Add(this.labelAgility);
            this.groupPrimaryStats.Controls.Add(this.labelLuckHeader);
            this.groupPrimaryStats.Controls.Add(this.labelLuck);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos0);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos1);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos2);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos3);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos4);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos6);
            this.groupPrimaryStats.Location = new System.Drawing.Point(3, 19);
            this.groupPrimaryStats.Name = "groupPrimaryStats";
            this.groupPrimaryStats.Size = new System.Drawing.Size(100, 110);
            this.groupPrimaryStats.TabIndex = 46;
            this.groupPrimaryStats.TabStop = false;
            this.groupPrimaryStats.Text = "Primary Stats";
            // 
            // labelIQHeader
            // 
            this.labelIQHeader.AutoSize = true;
            this.labelIQHeader.Location = new System.Drawing.Point(3, 30);
            this.labelIQHeader.Name = "labelIQHeader";
            this.labelIQHeader.Size = new System.Drawing.Size(24, 13);
            this.labelIQHeader.TabIndex = 0;
            this.labelIQHeader.Text = "I.Q.";
            this.labelIQHeader.MouseEnter += new System.EventHandler(this.labelIQHeader_MouseEnter);
            this.labelIQHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStrengthHeader
            // 
            this.labelStrengthHeader.AutoSize = true;
            this.labelStrengthHeader.Location = new System.Drawing.Point(3, 15);
            this.labelStrengthHeader.Name = "labelStrengthHeader";
            this.labelStrengthHeader.Size = new System.Drawing.Size(47, 13);
            this.labelStrengthHeader.TabIndex = 3;
            this.labelStrengthHeader.Text = "Strength";
            this.labelStrengthHeader.MouseEnter += new System.EventHandler(this.labelStrengthHeader_MouseEnter);
            this.labelStrengthHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelIQ
            // 
            this.labelIQ.AutoSize = true;
            this.labelIQ.Location = new System.Drawing.Point(53, 30);
            this.labelIQ.Name = "labelIQ";
            this.labelIQ.Size = new System.Drawing.Size(18, 13);
            this.labelIQ.TabIndex = 1;
            this.labelIQ.Text = "IQ";
            this.labelIQ.MouseEnter += new System.EventHandler(this.labelIQ_MouseEnter);
            this.labelIQ.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Ranged";
            // 
            // labelPietyHeader
            // 
            this.labelPietyHeader.AutoSize = true;
            this.labelPietyHeader.Location = new System.Drawing.Point(3, 45);
            this.labelPietyHeader.Name = "labelPietyHeader";
            this.labelPietyHeader.Size = new System.Drawing.Size(30, 13);
            this.labelPietyHeader.TabIndex = 6;
            this.labelPietyHeader.Text = "Piety";
            this.labelPietyHeader.MouseEnter += new System.EventHandler(this.labelPietyHeader_MouseEnter);
            this.labelPietyHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStrength
            // 
            this.labelStrength.AutoSize = true;
            this.labelStrength.Location = new System.Drawing.Point(53, 15);
            this.labelStrength.Name = "labelStrength";
            this.labelStrength.Size = new System.Drawing.Size(29, 13);
            this.labelStrength.TabIndex = 4;
            this.labelStrength.Text = "STR";
            this.labelStrength.MouseEnter += new System.EventHandler(this.labelStrength_MouseEnter);
            this.labelStrength.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelVitalityHeader
            // 
            this.labelVitalityHeader.AutoSize = true;
            this.labelVitalityHeader.Location = new System.Drawing.Point(3, 60);
            this.labelVitalityHeader.Name = "labelVitalityHeader";
            this.labelVitalityHeader.Size = new System.Drawing.Size(37, 13);
            this.labelVitalityHeader.TabIndex = 9;
            this.labelVitalityHeader.Text = "Vitality";
            this.labelVitalityHeader.MouseEnter += new System.EventHandler(this.labelVitalityHeader_MouseEnter);
            this.labelVitalityHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelPiety
            // 
            this.labelPiety.AutoSize = true;
            this.labelPiety.Location = new System.Drawing.Point(53, 45);
            this.labelPiety.Name = "labelPiety";
            this.labelPiety.Size = new System.Drawing.Size(24, 13);
            this.labelPiety.TabIndex = 7;
            this.labelPiety.Text = "PIE";
            this.labelPiety.MouseEnter += new System.EventHandler(this.labelPiety_MouseEnter);
            this.labelPiety.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
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
            // labelAgilityHeader
            // 
            this.labelAgilityHeader.AutoSize = true;
            this.labelAgilityHeader.Location = new System.Drawing.Point(3, 75);
            this.labelAgilityHeader.Name = "labelAgilityHeader";
            this.labelAgilityHeader.Size = new System.Drawing.Size(34, 13);
            this.labelAgilityHeader.TabIndex = 12;
            this.labelAgilityHeader.Text = "Agility";
            this.labelAgilityHeader.MouseEnter += new System.EventHandler(this.labelAgilityHeader_MouseEnter);
            this.labelAgilityHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Melee";
            // 
            // labelVitality
            // 
            this.labelVitality.AutoSize = true;
            this.labelVitality.Location = new System.Drawing.Point(53, 60);
            this.labelVitality.Name = "labelVitality";
            this.labelVitality.Size = new System.Drawing.Size(24, 13);
            this.labelVitality.TabIndex = 10;
            this.labelVitality.Text = "VIT";
            this.labelVitality.MouseEnter += new System.EventHandler(this.labelVitality_MouseEnter);
            this.labelVitality.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelAgility
            // 
            this.labelAgility.AutoSize = true;
            this.labelAgility.Location = new System.Drawing.Point(53, 75);
            this.labelAgility.Name = "labelAgility";
            this.labelAgility.Size = new System.Drawing.Size(25, 13);
            this.labelAgility.TabIndex = 13;
            this.labelAgility.Text = "AGI";
            this.labelAgility.MouseEnter += new System.EventHandler(this.labelAgility_MouseEnter);
            this.labelAgility.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelLuckHeader
            // 
            this.labelLuckHeader.AutoSize = true;
            this.labelLuckHeader.Location = new System.Drawing.Point(3, 90);
            this.labelLuckHeader.Name = "labelLuckHeader";
            this.labelLuckHeader.Size = new System.Drawing.Size(31, 13);
            this.labelLuckHeader.TabIndex = 18;
            this.labelLuckHeader.Text = "Luck";
            this.labelLuckHeader.MouseEnter += new System.EventHandler(this.labelLuckHeader_MouseEnter);
            this.labelLuckHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelLuck
            // 
            this.labelLuck.AutoSize = true;
            this.labelLuck.Location = new System.Drawing.Point(53, 90);
            this.labelLuck.Name = "labelLuck";
            this.labelLuck.Size = new System.Drawing.Size(28, 13);
            this.labelLuck.TabIndex = 19;
            this.labelLuck.Text = "LUC";
            this.labelLuck.MouseEnter += new System.EventHandler(this.labelLuck_MouseEnter);
            this.labelLuck.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStatPos0
            // 
            this.labelStatPos0.AutoSize = true;
            this.labelStatPos0.Location = new System.Drawing.Point(79, 30);
            this.labelStatPos0.Name = "labelStatPos0";
            this.labelStatPos0.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos0.TabIndex = 2;
            this.labelStatPos0.Text = "...";
            this.labelStatPos0.Visible = false;
            // 
            // labelStatPos1
            // 
            this.labelStatPos1.AutoSize = true;
            this.labelStatPos1.Location = new System.Drawing.Point(79, 15);
            this.labelStatPos1.Name = "labelStatPos1";
            this.labelStatPos1.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos1.TabIndex = 5;
            this.labelStatPos1.Text = "...";
            this.labelStatPos1.Visible = false;
            // 
            // labelStatPos2
            // 
            this.labelStatPos2.AutoSize = true;
            this.labelStatPos2.Location = new System.Drawing.Point(79, 45);
            this.labelStatPos2.Name = "labelStatPos2";
            this.labelStatPos2.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos2.TabIndex = 8;
            this.labelStatPos2.Text = "...";
            this.labelStatPos2.Visible = false;
            // 
            // labelStatPos3
            // 
            this.labelStatPos3.AutoSize = true;
            this.labelStatPos3.Location = new System.Drawing.Point(79, 60);
            this.labelStatPos3.Name = "labelStatPos3";
            this.labelStatPos3.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos3.TabIndex = 11;
            this.labelStatPos3.Text = "...";
            this.labelStatPos3.Visible = false;
            // 
            // labelStatPos4
            // 
            this.labelStatPos4.AutoSize = true;
            this.labelStatPos4.Location = new System.Drawing.Point(79, 75);
            this.labelStatPos4.Name = "labelStatPos4";
            this.labelStatPos4.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos4.TabIndex = 14;
            this.labelStatPos4.Text = "...";
            this.labelStatPos4.Visible = false;
            // 
            // labelStatPos6
            // 
            this.labelStatPos6.AutoSize = true;
            this.labelStatPos6.Location = new System.Drawing.Point(79, 90);
            this.labelStatPos6.Name = "labelStatPos6";
            this.labelStatPos6.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos6.TabIndex = 20;
            this.labelStatPos6.Text = "...";
            this.labelStatPos6.Visible = false;
            // 
            // labelKnownSpells
            // 
            this.labelKnownSpells.AutoSize = true;
            this.labelKnownSpells.Location = new System.Drawing.Point(59, 161);
            this.labelKnownSpells.Name = "labelKnownSpells";
            this.labelKnownSpells.Size = new System.Drawing.Size(47, 13);
            this.labelKnownSpells.TabIndex = 47;
            this.labelKnownSpells.Text = "SPELLS";
            this.labelKnownSpells.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelKnownSpells_MouseUp);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 161);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(57, 13);
            this.label29.TabIndex = 48;
            this.label29.Text = "Spellbook:";
            // 
            // cmView
            // 
            this.cmView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miViewView});
            this.cmView.Name = "cmView";
            this.cmView.ShowImageMargin = false;
            this.cmView.Size = new System.Drawing.Size(72, 26);
            // 
            // miViewView
            // 
            this.miViewView.Name = "miViewView";
            this.miViewView.Size = new System.Drawing.Size(71, 22);
            this.miViewView.Text = "Vi&ew";
            this.miViewView.Click += new System.EventHandler(this.miViewView_Click);
            // 
            // labelMoveHP
            // 
            this.labelMoveHP.AutoSize = true;
            this.labelMoveHP.Location = new System.Drawing.Point(109, 49);
            this.labelMoveHP.Name = "labelMoveHP";
            this.labelMoveHP.Size = new System.Drawing.Size(34, 13);
            this.labelMoveHP.TabIndex = 38;
            this.labelMoveHP.Text = "<HP>";
            this.labelMoveHP.Visible = false;
            // 
            // labelMoveSP
            // 
            this.labelMoveSP.AutoSize = true;
            this.labelMoveSP.Location = new System.Drawing.Point(109, 64);
            this.labelMoveSP.Name = "labelMoveSP";
            this.labelMoveSP.Size = new System.Drawing.Size(33, 13);
            this.labelMoveSP.TabIndex = 38;
            this.labelMoveSP.Text = "<SP>";
            this.labelMoveSP.Visible = false;
            // 
            // labelMoveAC
            // 
            this.labelMoveAC.AutoSize = true;
            this.labelMoveAC.Location = new System.Drawing.Point(109, 79);
            this.labelMoveAC.Name = "labelMoveAC";
            this.labelMoveAC.Size = new System.Drawing.Size(33, 13);
            this.labelMoveAC.TabIndex = 38;
            this.labelMoveAC.Text = "<AC>";
            this.labelMoveAC.Visible = false;
            // 
            // labelRegen
            // 
            this.labelRegen.AutoSize = true;
            this.labelRegen.Location = new System.Drawing.Point(157, 109);
            this.labelRegen.Name = "labelRegen";
            this.labelRegen.Size = new System.Drawing.Size(45, 13);
            this.labelRegen.TabIndex = 41;
            this.labelRegen.Text = "REGEN";
            this.labelRegen.MouseEnter += new System.EventHandler(this.labelRegen_MouseEnter);
            this.labelRegen.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelRegenHeader
            // 
            this.labelRegenHeader.AutoSize = true;
            this.labelRegenHeader.Location = new System.Drawing.Point(109, 109);
            this.labelRegenHeader.Name = "labelRegenHeader";
            this.labelRegenHeader.Size = new System.Drawing.Size(39, 13);
            this.labelRegenHeader.TabIndex = 38;
            this.labelRegenHeader.Text = "Regen";
            this.labelRegenHeader.MouseEnter += new System.EventHandler(this.labelRegenHeader_MouseEnter);
            this.labelRegenHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelPoison
            // 
            this.labelPoison.AutoSize = true;
            this.labelPoison.Location = new System.Drawing.Point(157, 125);
            this.labelPoison.Name = "labelPoison";
            this.labelPoison.Size = new System.Drawing.Size(48, 13);
            this.labelPoison.TabIndex = 41;
            this.labelPoison.Text = "POISON";
            this.labelPoison.MouseEnter += new System.EventHandler(this.labelPoison_MouseEnter);
            this.labelPoison.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelPoisonHeader
            // 
            this.labelPoisonHeader.AutoSize = true;
            this.labelPoisonHeader.Location = new System.Drawing.Point(109, 125);
            this.labelPoisonHeader.Name = "labelPoisonHeader";
            this.labelPoisonHeader.Size = new System.Drawing.Size(39, 13);
            this.labelPoisonHeader.TabIndex = 38;
            this.labelPoisonHeader.Text = "Poison";
            this.labelPoisonHeader.MouseEnter += new System.EventHandler(this.labelPoisonHeader_MouseEnter);
            this.labelPoisonHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelIDTraps
            // 
            this.labelIDTraps.AutoSize = true;
            this.labelIDTraps.Location = new System.Drawing.Point(43, 16);
            this.labelIDTraps.Name = "labelIDTraps";
            this.labelIDTraps.Size = new System.Drawing.Size(26, 13);
            this.labelIDTraps.TabIndex = 41;
            this.labelIDTraps.Text = "ID%";
            this.labelIDTraps.MouseEnter += new System.EventHandler(this.labelIDTraps_MouseEnter);
            this.labelIDTraps.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelIDTrapsHeader
            // 
            this.labelIDTrapsHeader.AutoSize = true;
            this.labelIDTrapsHeader.Location = new System.Drawing.Point(3, 16);
            this.labelIDTrapsHeader.Name = "labelIDTrapsHeader";
            this.labelIDTrapsHeader.Size = new System.Drawing.Size(41, 13);
            this.labelIDTrapsHeader.TabIndex = 38;
            this.labelIDTrapsHeader.Text = "Identify";
            this.labelIDTrapsHeader.MouseEnter += new System.EventHandler(this.labelIDTrapsHeader_MouseEnter);
            this.labelIDTrapsHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // gbTraps
            // 
            this.gbTraps.Controls.Add(this.labelDisarmHeader);
            this.gbTraps.Controls.Add(this.labelDisarm);
            this.gbTraps.Controls.Add(this.labelIDTrapsHeader);
            this.gbTraps.Controls.Add(this.labelIDTraps);
            this.gbTraps.Location = new System.Drawing.Point(256, 49);
            this.gbTraps.Name = "gbTraps";
            this.gbTraps.Size = new System.Drawing.Size(78, 52);
            this.gbTraps.TabIndex = 49;
            this.gbTraps.TabStop = false;
            this.gbTraps.Text = "Traps";
            // 
            // labelDisarmHeader
            // 
            this.labelDisarmHeader.AutoSize = true;
            this.labelDisarmHeader.Location = new System.Drawing.Point(3, 31);
            this.labelDisarmHeader.Name = "labelDisarmHeader";
            this.labelDisarmHeader.Size = new System.Drawing.Size(39, 13);
            this.labelDisarmHeader.TabIndex = 38;
            this.labelDisarmHeader.Text = "Disarm";
            this.labelDisarmHeader.MouseEnter += new System.EventHandler(this.labelDisarmHeader_MouseEnter);
            this.labelDisarmHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelDisarm
            // 
            this.labelDisarm.AutoSize = true;
            this.labelDisarm.Location = new System.Drawing.Point(43, 31);
            this.labelDisarm.Name = "labelDisarm";
            this.labelDisarm.Size = new System.Drawing.Size(33, 13);
            this.labelDisarm.TabIndex = 41;
            this.labelDisarm.Text = "DIS%";
            this.labelDisarm.MouseEnter += new System.EventHandler(this.labelDisarm_MouseEnter);
            this.labelDisarm.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelSpellLevel
            // 
            this.labelSpellLevel.AutoSize = true;
            this.labelSpellLevel.Location = new System.Drawing.Point(59, 176);
            this.labelSpellLevel.Name = "labelSpellLevel";
            this.labelSpellLevel.Size = new System.Drawing.Size(60, 13);
            this.labelSpellLevel.TabIndex = 47;
            this.labelSpellLevel.Text = "SPELLLEV";
            this.labelSpellLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelKnownSpells_MouseUp);
            // 
            // Wiz1CharacterInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTraps);
            this.Controls.Add(this.labelSpellLevel);
            this.Controls.Add(this.labelKnownSpells);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.groupPrimaryStats);
            this.Controls.Add(this.labelMoveAC);
            this.Controls.Add(this.labelMoveSP);
            this.Controls.Add(this.labelMoveHP);
            this.Controls.Add(this.labelPoisonHeader);
            this.Controls.Add(this.labelPoison);
            this.Controls.Add(this.labelRegenHeader);
            this.Controls.Add(this.labelRegen);
            this.Controls.Add(this.labelGoldHeader);
            this.Controls.Add(this.labelGold);
            this.Name = "Wiz1CharacterInfoControl";
            this.Controls.SetChildIndex(this.labelGold, 0);
            this.Controls.SetChildIndex(this.labelGoldHeader, 0);
            this.Controls.SetChildIndex(this.labelRegen, 0);
            this.Controls.SetChildIndex(this.labelRegenHeader, 0);
            this.Controls.SetChildIndex(this.labelPoison, 0);
            this.Controls.SetChildIndex(this.labelPoisonHeader, 0);
            this.Controls.SetChildIndex(this.labelMoveHP, 0);
            this.Controls.SetChildIndex(this.labelMoveSP, 0);
            this.Controls.SetChildIndex(this.labelMoveAC, 0);
            this.Controls.SetChildIndex(this.groupPrimaryStats, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.labelKnownSpells, 0);
            this.Controls.SetChildIndex(this.labelSpellLevel, 0);
            this.Controls.SetChildIndex(this.gbTraps, 0);
            this.groupPrimaryStats.ResumeLayout(false);
            this.groupPrimaryStats.PerformLayout();
            this.cmView.ResumeLayout(false);
            this.gbTraps.ResumeLayout(false);
            this.gbTraps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGoldHeader;
        private EditableAttributeLabel labelGold;
        private System.Windows.Forms.GroupBox groupPrimaryStats;
        private System.Windows.Forms.Label labelStatPos0;
        private System.Windows.Forms.Label labelStatPos1;
        private System.Windows.Forms.Label labelIQHeader;
        private System.Windows.Forms.Label labelStrengthHeader;
        private System.Windows.Forms.Label labelStatPos2;
        private EditableAttributeLabel labelIQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPietyHeader;
        private System.Windows.Forms.Label labelStatPos3;
        private EditableAttributeLabel labelStrength;
        private System.Windows.Forms.Label labelVitalityHeader;
        private System.Windows.Forms.Label labelStatPos4;
        private EditableAttributeLabel labelPiety;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label labelAgilityHeader;
        private System.Windows.Forms.Label label2;
        private EditableAttributeLabel labelVitality;
        private System.Windows.Forms.Label labelStatPos6;
        private EditableAttributeLabel labelAgility;
        private System.Windows.Forms.Label labelLuckHeader;
        private EditableAttributeLabel labelLuck;
        private EditableAttributeLabel labelKnownSpells;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ContextMenuStrip cmView;
        private System.Windows.Forms.ToolStripMenuItem miViewView;
        private System.Windows.Forms.Label labelMoveHP;
        private System.Windows.Forms.Label labelMoveSP;
        private System.Windows.Forms.Label labelMoveAC;
        private EditableAttributeLabel labelRegen;
        private System.Windows.Forms.Label labelRegenHeader;
        private EditableAttributeLabel labelPoison;
        private System.Windows.Forms.Label labelPoisonHeader;
        private EditableAttributeLabel labelIDTraps;
        private System.Windows.Forms.Label labelIDTrapsHeader;
        private System.Windows.Forms.GroupBox gbTraps;
        private System.Windows.Forms.Label labelDisarmHeader;
        private EditableAttributeLabel labelDisarm;
        private EditableAttributeLabel labelSpellLevel;
    }
}
