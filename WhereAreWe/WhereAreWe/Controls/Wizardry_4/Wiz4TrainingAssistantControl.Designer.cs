namespace WhereAreWe
{
    partial class Wiz4TrainingAssistantControl : TrainingAssistantControl
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
            this.lvGroups = new WhereAreWe.TipListView();
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCreature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbMonsters = new System.Windows.Forms.GroupBox();
            this.cbGiveMaxCreatures = new System.Windows.Forms.CheckBox();
            this.labelNotAtPentagram = new System.Windows.Forms.Label();
            this.labelMaximized = new System.Windows.Forms.Label();
            this.gbMonsters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvGroups
            // 
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNumber,
            this.chSize,
            this.chCreature});
            this.lvGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.Location = new System.Drawing.Point(3, 16);
            this.lvGroups.MultiSelect = false;
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvGroups.Size = new System.Drawing.Size(446, 95);
            this.lvGroups.TabIndex = 0;
            this.lvGroups.TipDelay = 700;
            this.lvGroups.TipDuration = 30000;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            // 
            // chNumber
            // 
            this.chNumber.Text = "#";
            this.chNumber.Width = 24;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 52;
            // 
            // chCreature
            // 
            this.chCreature.Text = "Creature";
            this.chCreature.Width = 349;
            // 
            // gbMonsters
            // 
            this.gbMonsters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMonsters.Controls.Add(this.lvGroups);
            this.gbMonsters.Location = new System.Drawing.Point(3, 3);
            this.gbMonsters.Name = "gbMonsters";
            this.gbMonsters.Size = new System.Drawing.Size(452, 114);
            this.gbMonsters.TabIndex = 1;
            this.gbMonsters.TabStop = false;
            this.gbMonsters.Text = "Monster Groups";
            // 
            // cbGiveMaxCreatures
            // 
            this.cbGiveMaxCreatures.AutoSize = true;
            this.cbGiveMaxCreatures.Location = new System.Drawing.Point(6, 123);
            this.cbGiveMaxCreatures.Name = "cbGiveMaxCreatures";
            this.cbGiveMaxCreatures.Size = new System.Drawing.Size(255, 17);
            this.cbGiveMaxCreatures.TabIndex = 1;
            this.cbGiveMaxCreatures.Text = "&Maximize the die rolls when summoning monsters";
            this.cbGiveMaxCreatures.UseVisualStyleBackColor = true;
            this.cbGiveMaxCreatures.CheckedChanged += new System.EventHandler(this.cbGiveMaxCreatures_CheckedChanged);
            // 
            // labelNotAtPentagram
            // 
            this.labelNotAtPentagram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNotAtPentagram.Location = new System.Drawing.Point(3, 153);
            this.labelNotAtPentagram.Name = "labelNotAtPentagram";
            this.labelNotAtPentagram.Size = new System.Drawing.Size(449, 40);
            this.labelNotAtPentagram.TabIndex = 2;
            this.labelNotAtPentagram.Text = "You are not at a pentagram.  This assistant will only be active when Werdna is us" +
    "ing a pentagram to summon monsters.";
            // 
            // labelMaximized
            // 
            this.labelMaximized.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaximized.AutoSize = true;
            this.labelMaximized.Location = new System.Drawing.Point(371, 124);
            this.labelMaximized.Name = "labelMaximized";
            this.labelMaximized.Size = new System.Drawing.Size(81, 13);
            this.labelMaximized.TabIndex = 1;
            this.labelMaximized.Text = "** Maximized! **";
            this.labelMaximized.Visible = false;
            // 
            // Wiz4TrainingAssistantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labelMaximized);
            this.Controls.Add(this.labelNotAtPentagram);
            this.Controls.Add(this.cbGiveMaxCreatures);
            this.Controls.Add(this.gbMonsters);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "Wiz4TrainingAssistantControl";
            this.Size = new System.Drawing.Size(458, 200);
            this.gbMonsters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TipListView lvGroups;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chCreature;
        private System.Windows.Forms.GroupBox gbMonsters;
        private System.Windows.Forms.CheckBox cbGiveMaxCreatures;
        private System.Windows.Forms.Label labelNotAtPentagram;
        private System.Windows.Forms.Label labelMaximized;


    }
}
