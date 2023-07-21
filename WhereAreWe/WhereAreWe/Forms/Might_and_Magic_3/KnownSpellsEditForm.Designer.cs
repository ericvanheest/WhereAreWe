namespace WhereAreWe
{
    partial class KnownSpellsEditForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.lvSpells = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLong = new System.Windows.Forms.Label();
            this.labelShort = new System.Windows.Forms.Label();
            this.labelLearned = new System.Windows.Forms.Label();
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelWhen = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.scSpellList = new System.Windows.Forms.SplitContainer();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellList)).BeginInit();
            this.scSpellList.Panel1.SuspendLayout();
            this.scSpellList.Panel2.SuspendLayout();
            this.scSpellList.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(441, 315);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(524, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 60;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAll.Location = new System.Drawing.Point(240, 315);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(50, 23);
            this.btnAll.TabIndex = 58;
            this.btnAll.Text = "&All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNone.Location = new System.Drawing.Point(240, 286);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(50, 23);
            this.btnNone.TabIndex = 57;
            this.btnNone.Text = "&None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // lvSpells
            // 
            this.lvSpells.CheckBoxes = true;
            this.lvSpells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chLevel});
            this.lvSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSpells.FullRowSelect = true;
            this.lvSpells.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSpells.HideSelection = false;
            this.lvSpells.Location = new System.Drawing.Point(0, 0);
            this.lvSpells.MultiSelect = false;
            this.lvSpells.Name = "lvSpells";
            this.lvSpells.Size = new System.Drawing.Size(228, 298);
            this.lvSpells.TabIndex = 61;
            this.lvSpells.UseCompatibleStateImageBehavior = false;
            this.lvSpells.View = System.Windows.Forms.View.Details;
            this.lvSpells.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSpells_ItemCheck);
            this.lvSpells.SelectedIndexChanged += new System.EventHandler(this.lvSpells_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 170;
            // 
            // chLevel
            // 
            this.chLevel.Text = "Lev";
            this.chLevel.Width = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Long:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(246, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 77;
            this.label9.Text = "Short:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "Learned:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "Target:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "When:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Cost:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Level:";
            // 
            // labelLong
            // 
            this.labelLong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLong.Location = new System.Drawing.Point(296, 192);
            this.labelLong.Name = "labelLong";
            this.labelLong.Size = new System.Drawing.Size(303, 115);
            this.labelLong.TabIndex = 71;
            this.labelLong.Text = "SPELL LONG DESCRIPTION";
            // 
            // labelShort
            // 
            this.labelShort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShort.Location = new System.Drawing.Point(296, 156);
            this.labelShort.Name = "labelShort";
            this.labelShort.Size = new System.Drawing.Size(303, 36);
            this.labelShort.TabIndex = 70;
            this.labelShort.Text = "SPELL SHORT DESCRIPTION";
            // 
            // labelLearned
            // 
            this.labelLearned.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLearned.Location = new System.Drawing.Point(296, 90);
            this.labelLearned.Name = "labelLearned";
            this.labelLearned.Size = new System.Drawing.Size(303, 66);
            this.labelLearned.TabIndex = 69;
            this.labelLearned.Text = "SPELL LEARNED";
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(296, 70);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(87, 13);
            this.labelTarget.TabIndex = 68;
            this.labelTarget.Text = "SPELL TARGET";
            // 
            // labelWhen
            // 
            this.labelWhen.AutoSize = true;
            this.labelWhen.Location = new System.Drawing.Point(296, 50);
            this.labelWhen.Name = "labelWhen";
            this.labelWhen.Size = new System.Drawing.Size(77, 13);
            this.labelWhen.TabIndex = 67;
            this.labelWhen.Text = "SPELL WHEN";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(296, 30);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(72, 13);
            this.labelCost.TabIndex = 66;
            this.labelCost.Text = "SPELL COST";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(478, 10);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(40, 13);
            this.labelLevel.TabIndex = 65;
            this.labelLevel.Text = "LEVEL";
            // 
            // labelIndex
            // 
            this.labelIndex.AutoSize = true;
            this.labelIndex.Location = new System.Drawing.Point(279, 10);
            this.labelIndex.Name = "labelIndex";
            this.labelIndex.Size = new System.Drawing.Size(40, 13);
            this.labelIndex.TabIndex = 64;
            this.labelIndex.Text = "INDEX";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(360, 10);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(35, 13);
            this.labelType.TabIndex = 63;
            this.labelType.Text = "TYPE";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(246, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 78;
            this.label11.Text = "Index:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Type:";
            // 
            // scSpellList
            // 
            this.scSpellList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scSpellList.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSpellList.Location = new System.Drawing.Point(6, 6);
            this.scSpellList.Name = "scSpellList";
            this.scSpellList.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSpellList.Panel1
            // 
            this.scSpellList.Panel1.Controls.Add(this.lvSpells);
            // 
            // scSpellList.Panel2
            // 
            this.scSpellList.Panel2.Controls.Add(this.label52);
            this.scSpellList.Panel2.Controls.Add(this.tbFind);
            this.scSpellList.Size = new System.Drawing.Size(228, 332);
            this.scSpellList.SplitterDistance = 298;
            this.scSpellList.TabIndex = 80;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(4, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 8;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(40, 5);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(185, 20);
            this.tbFind.TabIndex = 7;
            // 
            // KnownSpellsEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 343);
            this.Controls.Add(this.scSpellList);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLong);
            this.Controls.Add(this.labelShort);
            this.Controls.Add(this.labelLearned);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.labelWhen);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelIndex);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(491, 304);
            this.Name = "KnownSpellsEditForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Known Spells";
            this.Load += new System.EventHandler(this.KnownSpellsEditForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KnownSpellsEditForm_KeyDown);
            this.scSpellList.Panel1.ResumeLayout(false);
            this.scSpellList.Panel2.ResumeLayout(false);
            this.scSpellList.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellList)).EndInit();
            this.scSpellList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.ListView lvSpells;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLong;
        private System.Windows.Forms.Label labelShort;
        private System.Windows.Forms.Label labelLearned;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelWhen;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelIndex;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer scSpellList;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
    }
}