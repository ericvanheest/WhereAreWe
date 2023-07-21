namespace WhereAreWe
{
    partial class ViewPartyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewPartyForm));
            this.timerRefreshPartyInfo = new System.Windows.Forms.Timer(this.components);
            this.cmQuickRef = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miQuickCureAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuickAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.lvQuickRef = new WhereAreWe.DBListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNeedsXP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCondition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelNoPartyDetected = new System.Windows.Forms.Panel();
            this.labelNoParty = new System.Windows.Forms.Label();
            this.tcCharacters = new System.Windows.Forms.TabControl();
            this.tpChar1 = new System.Windows.Forms.TabPage();
            this.tpChar2 = new System.Windows.Forms.TabPage();
            this.tpChar3 = new System.Windows.Forms.TabPage();
            this.tpChar4 = new System.Windows.Forms.TabPage();
            this.tpChar5 = new System.Windows.Forms.TabPage();
            this.tpChar6 = new System.Windows.Forms.TabPage();
            this.tpChar7 = new System.Windows.Forms.TabPage();
            this.tpChar8 = new System.Windows.Forms.TabPage();
            this.cmTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxShowIndicatorForItems = new System.Windows.Forms.ToolStripMenuItem();
            this.cmQuickRef.SuspendLayout();
            this.panelNoPartyDetected.SuspendLayout();
            this.tcCharacters.SuspendLayout();
            this.cmTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRefreshPartyInfo
            // 
            this.timerRefreshPartyInfo.Interval = 200;
            this.timerRefreshPartyInfo.Tick += new System.EventHandler(this.timerRefreshPartyInfo_Tick);
            // 
            // cmQuickRef
            // 
            this.cmQuickRef.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miQuickCureAll,
            this.miQuickAdvanced});
            this.cmQuickRef.Name = "cmQuickRef";
            this.cmQuickRef.ShowCheckMargin = true;
            this.cmQuickRef.ShowImageMargin = false;
            this.cmQuickRef.Size = new System.Drawing.Size(224, 48);
            this.cmQuickRef.Opening += new System.ComponentModel.CancelEventHandler(this.cmQuickRef_Opening);
            // 
            // miQuickCureAll
            // 
            this.miQuickCureAll.Name = "miQuickCureAll";
            this.miQuickCureAll.Size = new System.Drawing.Size(223, 22);
            this.miQuickCureAll.Text = "&Cure All";
            this.miQuickCureAll.Click += new System.EventHandler(this.miQuickCureAll_Click);
            // 
            // miQuickAdvanced
            // 
            this.miQuickAdvanced.Name = "miQuickAdvanced";
            this.miQuickAdvanced.Size = new System.Drawing.Size(223, 22);
            this.miQuickAdvanced.Text = "Show detailed &Quick Reference";
            this.miQuickAdvanced.Click += new System.EventHandler(this.miQuickAdvanced_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(883, 263);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvQuickRef
            // 
            this.lvQuickRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvQuickRef.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chName,
            this.chNeedsXP,
            this.chHP,
            this.chSP,
            this.chGems,
            this.chCondition});
            this.lvQuickRef.ContextMenuStrip = this.cmQuickRef;
            this.lvQuickRef.FullRowSelect = true;
            this.lvQuickRef.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvQuickRef.Location = new System.Drawing.Point(484, 25);
            this.lvQuickRef.Name = "lvQuickRef";
            this.lvQuickRef.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvQuickRef.Size = new System.Drawing.Size(474, 140);
            this.lvQuickRef.TabIndex = 1;
            this.lvQuickRef.UseCompatibleStateImageBehavior = false;
            this.lvQuickRef.View = System.Windows.Forms.View.Details;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 19;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 55;
            // 
            // chNeedsXP
            // 
            this.chNeedsXP.Text = "XP Req";
            // 
            // chHP
            // 
            this.chHP.Text = "HP";
            this.chHP.Width = 47;
            // 
            // chSP
            // 
            this.chSP.Text = "SP";
            this.chSP.Width = 55;
            // 
            // chGems
            // 
            this.chGems.Text = "Gem";
            this.chGems.Width = 49;
            // 
            // chCondition
            // 
            this.chCondition.Text = "Condition";
            this.chCondition.Width = 110;
            // 
            // panelNoPartyDetected
            // 
            this.panelNoPartyDetected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNoPartyDetected.Controls.Add(this.labelNoParty);
            this.panelNoPartyDetected.Location = new System.Drawing.Point(4, 176);
            this.panelNoPartyDetected.Name = "panelNoPartyDetected";
            this.panelNoPartyDetected.Size = new System.Drawing.Size(452, 159);
            this.panelNoPartyDetected.TabIndex = 2;
            // 
            // labelNoParty
            // 
            this.labelNoParty.AutoSize = true;
            this.labelNoParty.Location = new System.Drawing.Point(88, 67);
            this.labelNoParty.Name = "labelNoParty";
            this.labelNoParty.Size = new System.Drawing.Size(269, 13);
            this.labelNoParty.TabIndex = 0;
            this.labelNoParty.Text = "No party detected!  Please form a party and exit the inn.";
            // 
            // tcCharacters
            // 
            this.tcCharacters.Controls.Add(this.tpChar1);
            this.tcCharacters.Controls.Add(this.tpChar2);
            this.tcCharacters.Controls.Add(this.tpChar3);
            this.tcCharacters.Controls.Add(this.tpChar4);
            this.tcCharacters.Controls.Add(this.tpChar5);
            this.tcCharacters.Controls.Add(this.tpChar6);
            this.tcCharacters.Controls.Add(this.tpChar7);
            this.tcCharacters.Controls.Add(this.tpChar8);
            this.tcCharacters.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcCharacters.Location = new System.Drawing.Point(0, 0);
            this.tcCharacters.Margin = new System.Windows.Forms.Padding(0);
            this.tcCharacters.Name = "tcCharacters";
            this.tcCharacters.SelectedIndex = 0;
            this.tcCharacters.Size = new System.Drawing.Size(374, 173);
            this.tcCharacters.TabIndex = 0;
            this.tcCharacters.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tcCharacters_DrawItem);
            this.tcCharacters.SelectedIndexChanged += new System.EventHandler(this.tcCharacters_SelectedIndexChanged);
            this.tcCharacters.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcCharacters_Selecting);
            this.tcCharacters.DoubleClick += new System.EventHandler(this.tcCharacters_DoubleClick);
            this.tcCharacters.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tcCharacters_MouseUp);
            // 
            // tpChar1
            // 
            this.tpChar1.Location = new System.Drawing.Point(4, 22);
            this.tpChar1.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar1.Name = "tpChar1";
            this.tpChar1.Size = new System.Drawing.Size(366, 147);
            this.tpChar1.TabIndex = 0;
            this.tpChar1.Text = "Char1";
            this.tpChar1.UseVisualStyleBackColor = true;
            // 
            // tpChar2
            // 
            this.tpChar2.Location = new System.Drawing.Point(4, 22);
            this.tpChar2.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar2.Name = "tpChar2";
            this.tpChar2.Size = new System.Drawing.Size(366, 147);
            this.tpChar2.TabIndex = 1;
            this.tpChar2.Text = "Char2";
            this.tpChar2.UseVisualStyleBackColor = true;
            // 
            // tpChar3
            // 
            this.tpChar3.Location = new System.Drawing.Point(4, 22);
            this.tpChar3.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar3.Name = "tpChar3";
            this.tpChar3.Size = new System.Drawing.Size(366, 147);
            this.tpChar3.TabIndex = 2;
            this.tpChar3.Text = "Char3";
            this.tpChar3.UseVisualStyleBackColor = true;
            // 
            // tpChar4
            // 
            this.tpChar4.Location = new System.Drawing.Point(4, 22);
            this.tpChar4.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar4.Name = "tpChar4";
            this.tpChar4.Size = new System.Drawing.Size(366, 147);
            this.tpChar4.TabIndex = 3;
            this.tpChar4.Text = "Char4";
            this.tpChar4.UseVisualStyleBackColor = true;
            // 
            // tpChar5
            // 
            this.tpChar5.Location = new System.Drawing.Point(4, 22);
            this.tpChar5.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar5.Name = "tpChar5";
            this.tpChar5.Size = new System.Drawing.Size(366, 147);
            this.tpChar5.TabIndex = 4;
            this.tpChar5.Text = "Char5";
            this.tpChar5.UseVisualStyleBackColor = true;
            // 
            // tpChar6
            // 
            this.tpChar6.Location = new System.Drawing.Point(4, 22);
            this.tpChar6.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar6.Name = "tpChar6";
            this.tpChar6.Size = new System.Drawing.Size(366, 147);
            this.tpChar6.TabIndex = 5;
            this.tpChar6.Text = "Char6";
            this.tpChar6.UseVisualStyleBackColor = true;
            // 
            // tpChar7
            // 
            this.tpChar7.Location = new System.Drawing.Point(4, 22);
            this.tpChar7.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar7.Name = "tpChar7";
            this.tpChar7.Size = new System.Drawing.Size(366, 147);
            this.tpChar7.TabIndex = 6;
            this.tpChar7.Text = "Char7";
            this.tpChar7.UseVisualStyleBackColor = true;
            // 
            // tpChar8
            // 
            this.tpChar8.Location = new System.Drawing.Point(4, 22);
            this.tpChar8.Margin = new System.Windows.Forms.Padding(0);
            this.tpChar8.Name = "tpChar8";
            this.tpChar8.Size = new System.Drawing.Size(366, 147);
            this.tpChar8.TabIndex = 7;
            this.tpChar8.Text = "Char8";
            this.tpChar8.UseVisualStyleBackColor = true;
            // 
            // cmTab
            // 
            this.cmTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxShowIndicatorForItems});
            this.cmTab.Name = "cmTab";
            this.cmTab.Size = new System.Drawing.Size(265, 26);
            this.cmTab.Opening += new System.ComponentModel.CancelEventHandler(this.cmTab_Opening);
            // 
            // miCtxShowIndicatorForItems
            // 
            this.miCtxShowIndicatorForItems.Name = "miCtxShowIndicatorForItems";
            this.miCtxShowIndicatorForItems.Size = new System.Drawing.Size(264, 22);
            this.miCtxShowIndicatorForItems.Text = "&Show indicator when items are received";
            this.miCtxShowIndicatorForItems.Click += new System.EventHandler(this.miCtxShowIndicatorForItems_Click);
            // 
            // ViewPartyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 347);
            this.Controls.Add(this.lvQuickRef);
            this.Controls.Add(this.tcCharacters);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelNoPartyDetected);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ViewPartyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Party Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewPartyForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewPartyForm_KeyDown);
            this.cmQuickRef.ResumeLayout(false);
            this.panelNoPartyDetected.ResumeLayout(false);
            this.panelNoPartyDetected.PerformLayout();
            this.tcCharacters.ResumeLayout(false);
            this.cmTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timerRefreshPartyInfo;
        private WhereAreWe.DBListView lvQuickRef;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chHP;
        private System.Windows.Forms.ColumnHeader chSP;
        private System.Windows.Forms.ColumnHeader chCondition;
        private System.Windows.Forms.ContextMenuStrip cmQuickRef;
        private System.Windows.Forms.ToolStripMenuItem miQuickCureAll;
        private System.Windows.Forms.ColumnHeader chGems;
        private System.Windows.Forms.ColumnHeader chNeedsXP;
        private System.Windows.Forms.TabControl tcCharacters;
        private System.Windows.Forms.TabPage tpChar1;
        private System.Windows.Forms.TabPage tpChar2;
        private System.Windows.Forms.TabPage tpChar3;
        private System.Windows.Forms.TabPage tpChar4;
        private System.Windows.Forms.TabPage tpChar5;
        private System.Windows.Forms.TabPage tpChar6;
        private System.Windows.Forms.TabPage tpChar7;
        private System.Windows.Forms.TabPage tpChar8;
        private System.Windows.Forms.Panel panelNoPartyDetected;
        private System.Windows.Forms.Label labelNoParty;
        private System.Windows.Forms.ToolStripMenuItem miQuickAdvanced;
        private System.Windows.Forms.ContextMenuStrip cmTab;
        private System.Windows.Forms.ToolStripMenuItem miCtxShowIndicatorForItems;
    }
}