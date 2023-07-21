namespace WhereAreWe
{
    partial class MonstersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonstersForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvMonsters = new WhereAreWe.TipListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHitPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chArmorClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResistances = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpecialPowers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExperience = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTreasure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmMonsters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.miCtxCopyFull = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmMonsters.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(54, 269);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(135, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvMonsters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label52);
            this.splitContainer1.Panel2.Controls.Add(this.tbFind);
            this.splitContainer1.Panel2MinSize = 20;
            this.splitContainer1.Size = new System.Drawing.Size(774, 655);
            this.splitContainer1.SplitterDistance = 626;
            this.splitContainer1.TabIndex = 3;
            // 
            // lvMonsters
            // 
            this.lvMonsters.AllowColumnReorder = true;
            this.lvMonsters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chIndex,
            this.chHitPoints,
            this.chArmorClass,
            this.chDamage,
            this.chSpeed,
            this.chResistances,
            this.chSpecialPowers,
            this.chExperience,
            this.chTreasure});
            this.lvMonsters.ContextMenuStrip = this.cmMonsters;
            this.lvMonsters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMonsters.FullRowSelect = true;
            this.lvMonsters.HideSelection = false;
            this.lvMonsters.Location = new System.Drawing.Point(0, 0);
            this.lvMonsters.Name = "lvMonsters";
            this.lvMonsters.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvMonsters.Size = new System.Drawing.Size(774, 626);
            this.lvMonsters.TabIndex = 0;
            this.lvMonsters.TipDelay = 1000;
            this.lvMonsters.TipDuration = 30000;
            this.lvMonsters.UseCompatibleStateImageBehavior = false;
            this.lvMonsters.View = System.Windows.Forms.View.Details;
            this.lvMonsters.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMonsters_ColumnClick);
            this.lvMonsters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMonsters_KeyDown);
            this.lvMonsters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMonsters_MouseDoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Monster Name";
            this.chName.Width = 100;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 34;
            // 
            // chHitPoints
            // 
            this.chHitPoints.Text = "HP";
            this.chHitPoints.Width = 33;
            // 
            // chArmorClass
            // 
            this.chArmorClass.Text = "AC";
            this.chArmorClass.Width = 27;
            // 
            // chDamage
            // 
            this.chDamage.Text = "Damage";
            this.chDamage.Width = 85;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Spd";
            this.chSpeed.Width = 34;
            // 
            // chResistances
            // 
            this.chResistances.Text = "Resistances";
            this.chResistances.Width = 114;
            // 
            // chSpecialPowers
            // 
            this.chSpecialPowers.DisplayIndex = 9;
            this.chSpecialPowers.Text = "Special Powers";
            this.chSpecialPowers.Width = 203;
            // 
            // chExperience
            // 
            this.chExperience.DisplayIndex = 7;
            this.chExperience.Text = "Exp";
            this.chExperience.Width = 54;
            // 
            // chTreasure
            // 
            this.chTreasure.DisplayIndex = 8;
            this.chTreasure.Text = "Treasure";
            // 
            // cmMonsters
            // 
            this.cmMonsters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxCopy,
            this.miCtxCopyFull});
            this.cmMonsters.Name = "cmMonsters";
            this.cmMonsters.ShowImageMargin = false;
            this.cmMonsters.Size = new System.Drawing.Size(152, 70);
            this.cmMonsters.Opening += new System.ComponentModel.CancelEventHandler(this.cmMonsters_Opening);
            // 
            // miCtxCopy
            // 
            this.miCtxCopy.Name = "miCtxCopy";
            this.miCtxCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCtxCopy.Size = new System.Drawing.Size(151, 22);
            this.miCtxCopy.Text = "&Copy";
            this.miCtxCopy.Click += new System.EventHandler(this.miCtxCopy_Click);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 3);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 6;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 0);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(735, 20);
            this.tbFind.TabIndex = 5;
            // 
            // miCtxCopyFull
            // 
            this.miCtxCopyFull.Name = "miCtxCopyFull";
            this.miCtxCopyFull.Size = new System.Drawing.Size(151, 22);
            this.miCtxCopyFull.Text = "Copy &full descriptions";
            this.miCtxCopyFull.Click += new System.EventHandler(this.miCtxCopyFull_Click);
            // 
            // MonstersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 655);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "MonstersForm";
            this.Text = "Monster List";
            this.Load += new System.EventHandler(this.MonstersForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmMonsters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WhereAreWe.TipListView lvMonsters;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chHitPoints;
        private System.Windows.Forms.ColumnHeader chArmorClass;
        private System.Windows.Forms.ColumnHeader chDamage;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chResistances;
        private System.Windows.Forms.ColumnHeader chSpecialPowers;
        private System.Windows.Forms.ColumnHeader chExperience;
        private System.Windows.Forms.ColumnHeader chTreasure;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ContextMenuStrip cmMonsters;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopy;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopyFull;
    }
}