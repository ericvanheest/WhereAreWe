namespace WhereAreWe
{
    partial class SpellReferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpellReferenceForm));
            this.timerRefreshSpellInfo = new System.Windows.Forms.Timer(this.components);
            this.timerRefocus = new System.Windows.Forms.Timer(this.components);
            this.cmSpell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxViewSpellInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxSendSpellKeys = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxCopyAllInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxRemoveAllFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxFilterUncastable = new System.Windows.Forms.ToolStripMenuItem();
            this.scMainFind = new System.Windows.Forms.SplitContainer();
            this.tcSpells = new System.Windows.Forms.TabControl();
            this.tpFavorites = new System.Windows.Forms.TabPage();
            this.spellListFavorites = new WhereAreWe.SpellRefPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.spellList1 = new WhereAreWe.SpellRefPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.spellList2 = new WhereAreWe.SpellRefPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.spellList3 = new WhereAreWe.SpellRefPanel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.spellList4 = new WhereAreWe.SpellRefPanel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.spellList5 = new WhereAreWe.SpellRefPanel();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.spellList6 = new WhereAreWe.SpellRefPanel();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.spellList7 = new WhereAreWe.SpellRefPanel();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.spellList8 = new WhereAreWe.SpellRefPanel();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.spellList9 = new WhereAreWe.SpellRefPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.cmSpell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMainFind)).BeginInit();
            this.scMainFind.Panel1.SuspendLayout();
            this.scMainFind.Panel2.SuspendLayout();
            this.scMainFind.SuspendLayout();
            this.tcSpells.SuspendLayout();
            this.tpFavorites.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRefreshSpellInfo
            // 
            this.timerRefreshSpellInfo.Interval = 250;
            this.timerRefreshSpellInfo.Tick += new System.EventHandler(this.timerRefreshSpellInfo_Tick);
            // 
            // timerRefocus
            // 
            this.timerRefocus.Tick += new System.EventHandler(this.timerRefocus_Tick);
            // 
            // cmSpell
            // 
            this.cmSpell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxViewSpellInfo,
            this.miCtxSendSpellKeys,
            this.miCtxCopyAllInfo,
            this.miCtxFavorites,
            this.miCtxRemoveAllFavorites,
            this.miCtxFilterUncastable});
            this.cmSpell.Name = "cmSpell";
            this.cmSpell.Size = new System.Drawing.Size(292, 136);
            this.cmSpell.Opening += new System.ComponentModel.CancelEventHandler(this.cmSpell_Opening);
            // 
            // miCtxViewSpellInfo
            // 
            this.miCtxViewSpellInfo.Name = "miCtxViewSpellInfo";
            this.miCtxViewSpellInfo.Size = new System.Drawing.Size(291, 22);
            this.miCtxViewSpellInfo.Text = "&View spell info";
            this.miCtxViewSpellInfo.Click += new System.EventHandler(this.miCtxViewSpellInfo_Click);
            // 
            // miCtxSendSpellKeys
            // 
            this.miCtxSendSpellKeys.Name = "miCtxSendSpellKeys";
            this.miCtxSendSpellKeys.Size = new System.Drawing.Size(291, 22);
            this.miCtxSendSpellKeys.Text = "&Send spell keys";
            this.miCtxSendSpellKeys.Click += new System.EventHandler(this.miCtxSendSpellKeys_Click);
            // 
            // miCtxCopyAllInfo
            // 
            this.miCtxCopyAllInfo.Name = "miCtxCopyAllInfo";
            this.miCtxCopyAllInfo.Size = new System.Drawing.Size(291, 22);
            this.miCtxCopyAllInfo.Text = "&Copy all spell information to clipboard";
            this.miCtxCopyAllInfo.Click += new System.EventHandler(this.miCtxCopyAllInfo_Click);
            // 
            // miCtxFavorites
            // 
            this.miCtxFavorites.Name = "miCtxFavorites";
            this.miCtxFavorites.Size = new System.Drawing.Size(291, 22);
            this.miCtxFavorites.Text = "Add to &Favorites";
            this.miCtxFavorites.Click += new System.EventHandler(this.miCtxFavorites_Click);
            // 
            // miCtxRemoveAllFavorites
            // 
            this.miCtxRemoveAllFavorites.Name = "miCtxRemoveAllFavorites";
            this.miCtxRemoveAllFavorites.Size = new System.Drawing.Size(291, 22);
            this.miCtxRemoveAllFavorites.Text = "&Remove all favorites for this character";
            this.miCtxRemoveAllFavorites.Click += new System.EventHandler(this.miCtxRemoveAllFavorites_Click);
            // 
            // miCtxFilterUncastable
            // 
            this.miCtxFilterUncastable.Checked = true;
            this.miCtxFilterUncastable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miCtxFilterUncastable.Name = "miCtxFilterUncastable";
            this.miCtxFilterUncastable.Size = new System.Drawing.Size(291, 22);
            this.miCtxFilterUncastable.Text = "Filter out &uncastable spells (location, combat)";
            this.miCtxFilterUncastable.Click += new System.EventHandler(this.miCtxFilterUncastable_Click);
            // 
            // scMainFind
            // 
            this.scMainFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainFind.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMainFind.IsSplitterFixed = true;
            this.scMainFind.Location = new System.Drawing.Point(0, 0);
            this.scMainFind.Name = "scMainFind";
            this.scMainFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMainFind.Panel1
            // 
            this.scMainFind.Panel1.Controls.Add(this.tcSpells);
            // 
            // scMainFind.Panel2
            // 
            this.scMainFind.Panel2.Controls.Add(this.label1);
            this.scMainFind.Panel2.Controls.Add(this.tbFind);
            this.scMainFind.Panel2MinSize = 20;
            this.scMainFind.Size = new System.Drawing.Size(693, 573);
            this.scMainFind.SplitterDistance = 544;
            this.scMainFind.TabIndex = 3;
            // 
            // tcSpells
            // 
            this.tcSpells.Controls.Add(this.tpFavorites);
            this.tcSpells.Controls.Add(this.tabPage1);
            this.tcSpells.Controls.Add(this.tabPage2);
            this.tcSpells.Controls.Add(this.tabPage3);
            this.tcSpells.Controls.Add(this.tabPage4);
            this.tcSpells.Controls.Add(this.tabPage5);
            this.tcSpells.Controls.Add(this.tabPage6);
            this.tcSpells.Controls.Add(this.tabPage7);
            this.tcSpells.Controls.Add(this.tabPage8);
            this.tcSpells.Controls.Add(this.tabPage9);
            this.tcSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSpells.Location = new System.Drawing.Point(0, 0);
            this.tcSpells.Name = "tcSpells";
            this.tcSpells.SelectedIndex = 0;
            this.tcSpells.Size = new System.Drawing.Size(693, 544);
            this.tcSpells.TabIndex = 4;
            this.tcSpells.SelectedIndexChanged += new System.EventHandler(this.tcSpells_SelectedIndexChanged);
            // 
            // tpFavorites
            // 
            this.tpFavorites.Controls.Add(this.spellListFavorites);
            this.tpFavorites.Location = new System.Drawing.Point(4, 22);
            this.tpFavorites.Name = "tpFavorites";
            this.tpFavorites.Padding = new System.Windows.Forms.Padding(3);
            this.tpFavorites.Size = new System.Drawing.Size(685, 518);
            this.tpFavorites.TabIndex = 9;
            this.tpFavorites.Text = "Favorites";
            this.tpFavorites.UseVisualStyleBackColor = true;
            // 
            // spellListFavorites
            // 
            this.spellListFavorites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellListFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellListFavorites.Location = new System.Drawing.Point(3, 3);
            this.spellListFavorites.Name = "spellListFavorites";
            this.spellListFavorites.ParentTab = null;
            this.spellListFavorites.ParentTabControl = null;
            this.spellListFavorites.ShowDuration = false;
            this.spellListFavorites.ShowSpellType = false;
            this.spellListFavorites.Size = new System.Drawing.Size(679, 512);
            this.spellListFavorites.TabIndex = 4;
            this.spellListFavorites.Title = "Favorite Spells";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.spellList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(685, 518);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Spells 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // spellList1
            // 
            this.spellList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList1.Location = new System.Drawing.Point(3, 3);
            this.spellList1.Name = "spellList1";
            this.spellList1.ParentTab = null;
            this.spellList1.ParentTabControl = null;
            this.spellList1.ShowDuration = false;
            this.spellList1.ShowSpellType = false;
            this.spellList1.Size = new System.Drawing.Size(679, 512);
            this.spellList1.TabIndex = 3;
            this.spellList1.Title = "Spells";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.spellList2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 518);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Spells 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // spellList2
            // 
            this.spellList2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList2.Location = new System.Drawing.Point(3, 3);
            this.spellList2.Name = "spellList2";
            this.spellList2.ParentTab = null;
            this.spellList2.ParentTabControl = null;
            this.spellList2.ShowDuration = false;
            this.spellList2.ShowSpellType = false;
            this.spellList2.Size = new System.Drawing.Size(679, 512);
            this.spellList2.TabIndex = 3;
            this.spellList2.Title = "Spells";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.spellList3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(685, 518);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Spells 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // spellList3
            // 
            this.spellList3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList3.Location = new System.Drawing.Point(3, 3);
            this.spellList3.Name = "spellList3";
            this.spellList3.ParentTab = null;
            this.spellList3.ParentTabControl = null;
            this.spellList3.ShowDuration = false;
            this.spellList3.ShowSpellType = false;
            this.spellList3.Size = new System.Drawing.Size(679, 512);
            this.spellList3.TabIndex = 3;
            this.spellList3.Title = "Spells";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.spellList4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(685, 518);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Spells 4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // spellList4
            // 
            this.spellList4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList4.Location = new System.Drawing.Point(3, 3);
            this.spellList4.Name = "spellList4";
            this.spellList4.ParentTab = null;
            this.spellList4.ParentTabControl = null;
            this.spellList4.ShowDuration = false;
            this.spellList4.ShowSpellType = false;
            this.spellList4.Size = new System.Drawing.Size(679, 512);
            this.spellList4.TabIndex = 3;
            this.spellList4.Title = "Spells";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.spellList5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(685, 518);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Spells 5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // spellList5
            // 
            this.spellList5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList5.Location = new System.Drawing.Point(3, 3);
            this.spellList5.Name = "spellList5";
            this.spellList5.ParentTab = null;
            this.spellList5.ParentTabControl = null;
            this.spellList5.ShowDuration = false;
            this.spellList5.ShowSpellType = false;
            this.spellList5.Size = new System.Drawing.Size(679, 512);
            this.spellList5.TabIndex = 4;
            this.spellList5.Title = "Spells";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.spellList6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(685, 518);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Spells 6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // spellList6
            // 
            this.spellList6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList6.Location = new System.Drawing.Point(3, 3);
            this.spellList6.Name = "spellList6";
            this.spellList6.ParentTab = null;
            this.spellList6.ParentTabControl = null;
            this.spellList6.ShowDuration = false;
            this.spellList6.ShowSpellType = false;
            this.spellList6.Size = new System.Drawing.Size(679, 512);
            this.spellList6.TabIndex = 5;
            this.spellList6.Title = "Spells";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.spellList7);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(685, 518);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Spells 7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // spellList7
            // 
            this.spellList7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList7.Location = new System.Drawing.Point(3, 3);
            this.spellList7.Name = "spellList7";
            this.spellList7.ParentTab = null;
            this.spellList7.ParentTabControl = null;
            this.spellList7.ShowDuration = false;
            this.spellList7.ShowSpellType = false;
            this.spellList7.Size = new System.Drawing.Size(679, 512);
            this.spellList7.TabIndex = 6;
            this.spellList7.Title = "Spells";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.spellList8);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(685, 518);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Spells 8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // spellList8
            // 
            this.spellList8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList8.Location = new System.Drawing.Point(3, 3);
            this.spellList8.Name = "spellList8";
            this.spellList8.ParentTab = null;
            this.spellList8.ParentTabControl = null;
            this.spellList8.ShowDuration = false;
            this.spellList8.ShowSpellType = false;
            this.spellList8.Size = new System.Drawing.Size(679, 512);
            this.spellList8.TabIndex = 6;
            this.spellList8.Title = "Spells";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.spellList9);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(685, 518);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Spells 9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // spellList9
            // 
            this.spellList9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spellList9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spellList9.Location = new System.Drawing.Point(3, 3);
            this.spellList9.Name = "spellList9";
            this.spellList9.ParentTab = null;
            this.spellList9.ParentTabControl = null;
            this.spellList9.ShowDuration = false;
            this.spellList9.ShowSpellType = false;
            this.spellList9.Size = new System.Drawing.Size(679, 512);
            this.spellList9.TabIndex = 6;
            this.spellList9.Title = "Spells";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 0);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(654, 20);
            this.tbFind.TabIndex = 1;
            // 
            // SpellReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 573);
            this.Controls.Add(this.scMainFind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "SpellReferenceForm";
            this.Text = "Spell Reference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpellReferenceForm_FormClosing);
            this.Load += new System.EventHandler(this.SpellReferenceForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SpellReferenceForm_KeyDown);
            this.cmSpell.ResumeLayout(false);
            this.scMainFind.Panel1.ResumeLayout(false);
            this.scMainFind.Panel2.ResumeLayout(false);
            this.scMainFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMainFind)).EndInit();
            this.scMainFind.ResumeLayout(false);
            this.tcSpells.ResumeLayout(false);
            this.tpFavorites.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRefreshSpellInfo;
        private System.Windows.Forms.Timer timerRefocus;
        private System.Windows.Forms.ContextMenuStrip cmSpell;
        private System.Windows.Forms.ToolStripMenuItem miCtxViewSpellInfo;
        private System.Windows.Forms.ToolStripMenuItem miCtxSendSpellKeys;
        private System.Windows.Forms.SplitContainer scMainFind;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopyAllInfo;
        private SpellRefPanel spellList1;
        private SpellRefPanel spellList3;
        private SpellRefPanel spellList2;
        private System.Windows.Forms.TabControl tcSpells;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private SpellRefPanel spellList4;
        private System.Windows.Forms.TabPage tabPage5;
        private SpellRefPanel spellList5;
        private System.Windows.Forms.TabPage tabPage6;
        private SpellRefPanel spellList6;
        private System.Windows.Forms.TabPage tabPage7;
        private SpellRefPanel spellList7;
        private System.Windows.Forms.TabPage tabPage8;
        private SpellRefPanel spellList8;
        private System.Windows.Forms.TabPage tabPage9;
        private SpellRefPanel spellList9;
        private System.Windows.Forms.TabPage tpFavorites;
        private SpellRefPanel spellListFavorites;
        private System.Windows.Forms.ToolStripMenuItem miCtxFavorites;
        private System.Windows.Forms.ToolStripMenuItem miCtxRemoveAllFavorites;
        private System.Windows.Forms.ToolStripMenuItem miCtxFilterUncastable;
    }
}