namespace WhereAreWe
{
    partial class BT123CharacterInfoControl
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
            this.labelDexterityHeader = new System.Windows.Forms.Label();
            this.labelStrength = new WhereAreWe.EditableAttributeLabel();
            this.labelConstitutionHeader = new System.Windows.Forms.Label();
            this.labelDexterity = new WhereAreWe.EditableAttributeLabel();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelConstitution = new WhereAreWe.EditableAttributeLabel();
            this.labelLuckHeader = new System.Windows.Forms.Label();
            this.labelLuck = new WhereAreWe.EditableAttributeLabel();
            this.labelStatPos0 = new System.Windows.Forms.Label();
            this.labelStatPos1 = new System.Windows.Forms.Label();
            this.labelStatPos2 = new System.Windows.Forms.Label();
            this.labelStatPos3 = new System.Windows.Forms.Label();
            this.labelStatPos6 = new System.Windows.Forms.Label();
            this.labelKnownSpells = new WhereAreWe.EditableAttributeLabel();
            this.label29 = new System.Windows.Forms.Label();
            this.cmView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miViewView = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSongs = new WhereAreWe.EditableAttributeLabel();
            this.labelSongsHeader = new System.Windows.Forms.Label();
            this.labelWon = new WhereAreWe.EditableAttributeLabel();
            this.labelWonHeader = new System.Windows.Forms.Label();
            this.labelHide = new WhereAreWe.EditableAttributeLabel();
            this.labelHideHeader = new System.Windows.Forms.Label();
            this.labelNumAttacks = new WhereAreWe.EditableAttributeLabel();
            this.labelNumAttacksHeader = new System.Windows.Forms.Label();
            this.labelCritical = new WhereAreWe.EditableAttributeLabel();
            this.labelCriticalHeader = new System.Windows.Forms.Label();
            this.labelIdentify = new WhereAreWe.EditableAttributeLabel();
            this.labelIdentifyHeader = new System.Windows.Forms.Label();
            this.labelDisarm = new WhereAreWe.EditableAttributeLabel();
            this.labelDisarmHeader = new System.Windows.Forms.Label();
            this.llAwards = new System.Windows.Forms.LinkLabel();
            this.labelQuestHeader = new System.Windows.Forms.Label();
            this.labelQuest = new WhereAreWe.EditableAttributeLabel();
            this.labelRangedHeader = new System.Windows.Forms.Label();
            this.labelRanged = new WhereAreWe.EditableAttributeLabel();
            this.groupPrimaryStats.SuspendLayout();
            this.cmView.SuspendLayout();
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
            this.groupPrimaryStats.Controls.Add(this.labelDexterityHeader);
            this.groupPrimaryStats.Controls.Add(this.labelStrength);
            this.groupPrimaryStats.Controls.Add(this.labelConstitutionHeader);
            this.groupPrimaryStats.Controls.Add(this.labelDexterity);
            this.groupPrimaryStats.Controls.Add(this.label24);
            this.groupPrimaryStats.Controls.Add(this.label2);
            this.groupPrimaryStats.Controls.Add(this.labelConstitution);
            this.groupPrimaryStats.Controls.Add(this.labelLuckHeader);
            this.groupPrimaryStats.Controls.Add(this.labelLuck);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos0);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos1);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos2);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos3);
            this.groupPrimaryStats.Controls.Add(this.labelStatPos6);
            this.groupPrimaryStats.Location = new System.Drawing.Point(3, 19);
            this.groupPrimaryStats.Name = "groupPrimaryStats";
            this.groupPrimaryStats.Size = new System.Drawing.Size(89, 96);
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
            this.labelStrengthHeader.Size = new System.Drawing.Size(23, 13);
            this.labelStrengthHeader.TabIndex = 3;
            this.labelStrengthHeader.Text = "Str.";
            this.labelStrengthHeader.MouseEnter += new System.EventHandler(this.labelStrengthHeader_MouseEnter);
            this.labelStrengthHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelIQ
            // 
            this.labelIQ.AutoSize = true;
            this.labelIQ.Location = new System.Drawing.Point(36, 30);
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
            // labelDexterityHeader
            // 
            this.labelDexterityHeader.AutoSize = true;
            this.labelDexterityHeader.Location = new System.Drawing.Point(3, 45);
            this.labelDexterityHeader.Name = "labelDexterityHeader";
            this.labelDexterityHeader.Size = new System.Drawing.Size(29, 13);
            this.labelDexterityHeader.TabIndex = 6;
            this.labelDexterityHeader.Text = "Dex.";
            this.labelDexterityHeader.MouseEnter += new System.EventHandler(this.labelDexterityHeader_MouseEnter);
            this.labelDexterityHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelStrength
            // 
            this.labelStrength.AutoSize = true;
            this.labelStrength.Location = new System.Drawing.Point(36, 15);
            this.labelStrength.Name = "labelStrength";
            this.labelStrength.Size = new System.Drawing.Size(29, 13);
            this.labelStrength.TabIndex = 4;
            this.labelStrength.Text = "STR";
            this.labelStrength.MouseEnter += new System.EventHandler(this.labelStrength_MouseEnter);
            this.labelStrength.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelConstitutionHeader
            // 
            this.labelConstitutionHeader.AutoSize = true;
            this.labelConstitutionHeader.Location = new System.Drawing.Point(3, 60);
            this.labelConstitutionHeader.Name = "labelConstitutionHeader";
            this.labelConstitutionHeader.Size = new System.Drawing.Size(29, 13);
            this.labelConstitutionHeader.TabIndex = 9;
            this.labelConstitutionHeader.Text = "Con.";
            this.labelConstitutionHeader.MouseEnter += new System.EventHandler(this.labelConstitutionHeader_MouseEnter);
            this.labelConstitutionHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelDexterity
            // 
            this.labelDexterity.AutoSize = true;
            this.labelDexterity.Location = new System.Drawing.Point(36, 45);
            this.labelDexterity.Name = "labelDexterity";
            this.labelDexterity.Size = new System.Drawing.Size(29, 13);
            this.labelDexterity.TabIndex = 7;
            this.labelDexterity.Text = "DEX";
            this.labelDexterity.MouseEnter += new System.EventHandler(this.labelDexterity_MouseEnter);
            this.labelDexterity.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Melee";
            // 
            // labelConstitution
            // 
            this.labelConstitution.AutoSize = true;
            this.labelConstitution.Location = new System.Drawing.Point(36, 60);
            this.labelConstitution.Name = "labelConstitution";
            this.labelConstitution.Size = new System.Drawing.Size(30, 13);
            this.labelConstitution.TabIndex = 10;
            this.labelConstitution.Text = "CON";
            this.labelConstitution.MouseEnter += new System.EventHandler(this.labelConstitution_MouseEnter);
            this.labelConstitution.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelLuckHeader
            // 
            this.labelLuckHeader.AutoSize = true;
            this.labelLuckHeader.Location = new System.Drawing.Point(3, 75);
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
            this.labelLuck.Location = new System.Drawing.Point(36, 75);
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
            this.labelStatPos0.Location = new System.Drawing.Point(65, 30);
            this.labelStatPos0.Name = "labelStatPos0";
            this.labelStatPos0.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos0.TabIndex = 2;
            this.labelStatPos0.Text = "...";
            this.labelStatPos0.Visible = false;
            // 
            // labelStatPos1
            // 
            this.labelStatPos1.AutoSize = true;
            this.labelStatPos1.Location = new System.Drawing.Point(65, 15);
            this.labelStatPos1.Name = "labelStatPos1";
            this.labelStatPos1.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos1.TabIndex = 5;
            this.labelStatPos1.Text = "...";
            this.labelStatPos1.Visible = false;
            // 
            // labelStatPos2
            // 
            this.labelStatPos2.AutoSize = true;
            this.labelStatPos2.Location = new System.Drawing.Point(65, 45);
            this.labelStatPos2.Name = "labelStatPos2";
            this.labelStatPos2.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos2.TabIndex = 8;
            this.labelStatPos2.Text = "...";
            this.labelStatPos2.Visible = false;
            // 
            // labelStatPos3
            // 
            this.labelStatPos3.AutoSize = true;
            this.labelStatPos3.Location = new System.Drawing.Point(65, 60);
            this.labelStatPos3.Name = "labelStatPos3";
            this.labelStatPos3.Size = new System.Drawing.Size(16, 13);
            this.labelStatPos3.TabIndex = 11;
            this.labelStatPos3.Text = "...";
            this.labelStatPos3.Visible = false;
            // 
            // labelStatPos6
            // 
            this.labelStatPos6.AutoSize = true;
            this.labelStatPos6.Location = new System.Drawing.Point(65, 75);
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
            this.cmView.Size = new System.Drawing.Size(75, 26);
            // 
            // miViewView
            // 
            this.miViewView.Name = "miViewView";
            this.miViewView.Size = new System.Drawing.Size(74, 22);
            this.miViewView.Text = "Vi&ew";
            // 
            // labelSongs
            // 
            this.labelSongs.AutoSize = true;
            this.labelSongs.Location = new System.Drawing.Point(157, 49);
            this.labelSongs.Name = "labelSongs";
            this.labelSongs.Size = new System.Drawing.Size(45, 13);
            this.labelSongs.TabIndex = 41;
            this.labelSongs.Text = "SONGS";
            // 
            // labelSongsHeader
            // 
            this.labelSongsHeader.AutoSize = true;
            this.labelSongsHeader.Location = new System.Drawing.Point(109, 49);
            this.labelSongsHeader.Name = "labelSongsHeader";
            this.labelSongsHeader.Size = new System.Drawing.Size(37, 13);
            this.labelSongsHeader.TabIndex = 38;
            this.labelSongsHeader.Text = "Songs";
            // 
            // labelWon
            // 
            this.labelWon.AutoSize = true;
            this.labelWon.Location = new System.Drawing.Point(157, 64);
            this.labelWon.Name = "labelWon";
            this.labelWon.Size = new System.Drawing.Size(34, 13);
            this.labelWon.TabIndex = 41;
            this.labelWon.Text = "WON";
            // 
            // labelWonHeader
            // 
            this.labelWonHeader.AutoSize = true;
            this.labelWonHeader.Location = new System.Drawing.Point(109, 64);
            this.labelWonHeader.Name = "labelWonHeader";
            this.labelWonHeader.Size = new System.Drawing.Size(30, 13);
            this.labelWonHeader.TabIndex = 38;
            this.labelWonHeader.Text = "Won";
            // 
            // labelHide
            // 
            this.labelHide.AutoSize = true;
            this.labelHide.Location = new System.Drawing.Point(267, 79);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(33, 13);
            this.labelHide.TabIndex = 41;
            this.labelHide.Text = "HIDE";
            // 
            // labelHideHeader
            // 
            this.labelHideHeader.AutoSize = true;
            this.labelHideHeader.Location = new System.Drawing.Point(220, 79);
            this.labelHideHeader.Name = "labelHideHeader";
            this.labelHideHeader.Size = new System.Drawing.Size(29, 13);
            this.labelHideHeader.TabIndex = 38;
            this.labelHideHeader.Text = "Hide";
            // 
            // labelNumAttacks
            // 
            this.labelNumAttacks.AutoSize = true;
            this.labelNumAttacks.Location = new System.Drawing.Point(157, 79);
            this.labelNumAttacks.Name = "labelNumAttacks";
            this.labelNumAttacks.Size = new System.Drawing.Size(27, 13);
            this.labelNumAttacks.TabIndex = 41;
            this.labelNumAttacks.Text = "#Att";
            // 
            // labelNumAttacksHeader
            // 
            this.labelNumAttacksHeader.AutoSize = true;
            this.labelNumAttacksHeader.Location = new System.Drawing.Point(110, 79);
            this.labelNumAttacksHeader.Name = "labelNumAttacksHeader";
            this.labelNumAttacksHeader.Size = new System.Drawing.Size(43, 13);
            this.labelNumAttacksHeader.TabIndex = 38;
            this.labelNumAttacksHeader.Text = "Attacks";
            // 
            // labelCritical
            // 
            this.labelCritical.AutoSize = true;
            this.labelCritical.Location = new System.Drawing.Point(157, 124);
            this.labelCritical.Name = "labelCritical";
            this.labelCritical.Size = new System.Drawing.Size(32, 13);
            this.labelCritical.TabIndex = 41;
            this.labelCritical.Text = "CRIT";
            // 
            // labelCriticalHeader
            // 
            this.labelCriticalHeader.AutoSize = true;
            this.labelCriticalHeader.Location = new System.Drawing.Point(110, 124);
            this.labelCriticalHeader.Name = "labelCriticalHeader";
            this.labelCriticalHeader.Size = new System.Drawing.Size(38, 13);
            this.labelCriticalHeader.TabIndex = 38;
            this.labelCriticalHeader.Text = "Critical";
            // 
            // labelIdentify
            // 
            this.labelIdentify.AutoSize = true;
            this.labelIdentify.Location = new System.Drawing.Point(267, 94);
            this.labelIdentify.Name = "labelIdentify";
            this.labelIdentify.Size = new System.Drawing.Size(40, 13);
            this.labelIdentify.TabIndex = 41;
            this.labelIdentify.Text = "IDENT";
            // 
            // labelIdentifyHeader
            // 
            this.labelIdentifyHeader.AutoSize = true;
            this.labelIdentifyHeader.Location = new System.Drawing.Point(220, 94);
            this.labelIdentifyHeader.Name = "labelIdentifyHeader";
            this.labelIdentifyHeader.Size = new System.Drawing.Size(41, 13);
            this.labelIdentifyHeader.TabIndex = 38;
            this.labelIdentifyHeader.Text = "Identify";
            // 
            // labelDisarm
            // 
            this.labelDisarm.AutoSize = true;
            this.labelDisarm.Location = new System.Drawing.Point(267, 109);
            this.labelDisarm.Name = "labelDisarm";
            this.labelDisarm.Size = new System.Drawing.Size(49, 13);
            this.labelDisarm.TabIndex = 41;
            this.labelDisarm.Text = "DISARM";
            // 
            // labelDisarmHeader
            // 
            this.labelDisarmHeader.AutoSize = true;
            this.labelDisarmHeader.Location = new System.Drawing.Point(220, 109);
            this.labelDisarmHeader.Name = "labelDisarmHeader";
            this.labelDisarmHeader.Size = new System.Drawing.Size(39, 13);
            this.labelDisarmHeader.TabIndex = 38;
            this.labelDisarmHeader.Text = "Disarm";
            // 
            // llAwards
            // 
            this.llAwards.AutoSize = true;
            this.llAwards.Location = new System.Drawing.Point(247, 125);
            this.llAwards.Name = "llAwards";
            this.llAwards.Size = new System.Drawing.Size(42, 13);
            this.llAwards.TabIndex = 42;
            this.llAwards.TabStop = true;
            this.llAwards.Text = "Awards";
            this.llAwards.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAwards_LinkClicked);
            // 
            // labelQuestHeader
            // 
            this.labelQuestHeader.AutoSize = true;
            this.labelQuestHeader.Location = new System.Drawing.Point(6, 176);
            this.labelQuestHeader.Name = "labelQuestHeader";
            this.labelQuestHeader.Size = new System.Drawing.Size(38, 13);
            this.labelQuestHeader.TabIndex = 48;
            this.labelQuestHeader.Text = "Quest:";
            // 
            // labelQuest
            // 
            this.labelQuest.AutoSize = true;
            this.labelQuest.Location = new System.Drawing.Point(59, 176);
            this.labelQuest.Name = "labelQuest";
            this.labelQuest.Size = new System.Drawing.Size(44, 13);
            this.labelQuest.TabIndex = 47;
            this.labelQuest.Text = "QUEST";
            // 
            // labelRangedHeader
            // 
            this.labelRangedHeader.AutoSize = true;
            this.labelRangedHeader.Location = new System.Drawing.Point(110, 109);
            this.labelRangedHeader.Name = "labelRangedHeader";
            this.labelRangedHeader.Size = new System.Drawing.Size(45, 13);
            this.labelRangedHeader.TabIndex = 49;
            this.labelRangedHeader.Text = "Ranged";
            this.labelRangedHeader.MouseEnter += new System.EventHandler(this.labelRangedHeader_MouseEnter);
            // 
            // labelRanged
            // 
            this.labelRanged.AutoSize = true;
            this.labelRanged.Location = new System.Drawing.Point(157, 109);
            this.labelRanged.Name = "labelRanged";
            this.labelRanged.Size = new System.Drawing.Size(53, 13);
            this.labelRanged.TabIndex = 50;
            this.labelRanged.Text = "RANGED";
            this.labelRanged.MouseEnter += new System.EventHandler(this.labelRanged_MouseEnter);
            // 
            // BT123CharacterInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelRangedHeader);
            this.Controls.Add(this.labelRanged);
            this.Controls.Add(this.llAwards);
            this.Controls.Add(this.labelQuest);
            this.Controls.Add(this.labelKnownSpells);
            this.Controls.Add(this.labelQuestHeader);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.groupPrimaryStats);
            this.Controls.Add(this.labelNumAttacksHeader);
            this.Controls.Add(this.labelNumAttacks);
            this.Controls.Add(this.labelCriticalHeader);
            this.Controls.Add(this.labelCritical);
            this.Controls.Add(this.labelDisarmHeader);
            this.Controls.Add(this.labelDisarm);
            this.Controls.Add(this.labelIdentifyHeader);
            this.Controls.Add(this.labelIdentify);
            this.Controls.Add(this.labelHideHeader);
            this.Controls.Add(this.labelHide);
            this.Controls.Add(this.labelWonHeader);
            this.Controls.Add(this.labelWon);
            this.Controls.Add(this.labelSongsHeader);
            this.Controls.Add(this.labelSongs);
            this.Controls.Add(this.labelGoldHeader);
            this.Controls.Add(this.labelGold);
            this.Name = "BT123CharacterInfoControl";
            this.Controls.SetChildIndex(this.labelGold, 0);
            this.Controls.SetChildIndex(this.labelGoldHeader, 0);
            this.Controls.SetChildIndex(this.labelSongs, 0);
            this.Controls.SetChildIndex(this.labelSongsHeader, 0);
            this.Controls.SetChildIndex(this.labelWon, 0);
            this.Controls.SetChildIndex(this.labelWonHeader, 0);
            this.Controls.SetChildIndex(this.labelHide, 0);
            this.Controls.SetChildIndex(this.labelHideHeader, 0);
            this.Controls.SetChildIndex(this.labelIdentify, 0);
            this.Controls.SetChildIndex(this.labelIdentifyHeader, 0);
            this.Controls.SetChildIndex(this.labelDisarm, 0);
            this.Controls.SetChildIndex(this.labelDisarmHeader, 0);
            this.Controls.SetChildIndex(this.labelCritical, 0);
            this.Controls.SetChildIndex(this.labelCriticalHeader, 0);
            this.Controls.SetChildIndex(this.labelNumAttacks, 0);
            this.Controls.SetChildIndex(this.labelNumAttacksHeader, 0);
            this.Controls.SetChildIndex(this.groupPrimaryStats, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.labelQuestHeader, 0);
            this.Controls.SetChildIndex(this.labelKnownSpells, 0);
            this.Controls.SetChildIndex(this.labelQuest, 0);
            this.Controls.SetChildIndex(this.llAwards, 0);
            this.Controls.SetChildIndex(this.labelRanged, 0);
            this.Controls.SetChildIndex(this.labelRangedHeader, 0);
            this.groupPrimaryStats.ResumeLayout(false);
            this.groupPrimaryStats.PerformLayout();
            this.cmView.ResumeLayout(false);
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
        private System.Windows.Forms.Label labelDexterityHeader;
        private System.Windows.Forms.Label labelStatPos3;
        private EditableAttributeLabel labelStrength;
        private System.Windows.Forms.Label labelConstitutionHeader;
        private EditableAttributeLabel labelDexterity;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label2;
        private EditableAttributeLabel labelConstitution;
        private System.Windows.Forms.Label labelStatPos6;
        private System.Windows.Forms.Label labelLuckHeader;
        private EditableAttributeLabel labelLuck;
        private EditableAttributeLabel labelKnownSpells;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ContextMenuStrip cmView;
        private System.Windows.Forms.ToolStripMenuItem miViewView;
        private EditableAttributeLabel labelSongs;
        private System.Windows.Forms.Label labelSongsHeader;
        private EditableAttributeLabel labelWon;
        private System.Windows.Forms.Label labelWonHeader;
        private EditableAttributeLabel labelHide;
        private System.Windows.Forms.Label labelHideHeader;
        private EditableAttributeLabel labelNumAttacks;
        private System.Windows.Forms.Label labelNumAttacksHeader;
        private EditableAttributeLabel labelCritical;
        private System.Windows.Forms.Label labelCriticalHeader;
        private EditableAttributeLabel labelIdentify;
        private System.Windows.Forms.Label labelIdentifyHeader;
        private EditableAttributeLabel labelDisarm;
        private System.Windows.Forms.Label labelDisarmHeader;
        private System.Windows.Forms.LinkLabel llAwards;
        private System.Windows.Forms.Label labelQuestHeader;
        private EditableAttributeLabel labelQuest;
        private System.Windows.Forms.Label labelRangedHeader;
        private EditableAttributeLabel labelRanged;
    }
}
