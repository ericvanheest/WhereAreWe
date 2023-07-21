namespace WhereAreWe
{
    partial class EditDnDSpellsForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvSpells = new System.Windows.Forms.ListView();
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBook = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSelected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMemorized = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMemorizeMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.miUnmemorize = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLong = new System.Windows.Forms.Label();
            this.labelShort = new System.Windows.Forms.Label();
            this.labelLearned = new System.Windows.Forms.Label();
            this.labelTarget = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.scSpellList = new System.Windows.Forms.SplitContainer();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.scSpellsInfo = new System.Windows.Forms.SplitContainer();
            this.nudMemorized = new System.Windows.Forms.NumericUpDown();
            this.nudSelected = new System.Windows.Forms.NumericUpDown();
            this.cbInSpellbook = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalMemorized = new System.Windows.Forms.Label();
            this.labelTotalSelected = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellList)).BeginInit();
            this.scSpellList.Panel1.SuspendLayout();
            this.scSpellList.Panel2.SuspendLayout();
            this.scSpellList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellsInfo)).BeginInit();
            this.scSpellsInfo.Panel1.SuspendLayout();
            this.scSpellsInfo.Panel2.SuspendLayout();
            this.scSpellsInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMemorized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(224, 317);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(307, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvSpells
            // 
            this.lvSpells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chType,
            this.chLevel,
            this.chName,
            this.chBook,
            this.chSelected,
            this.chMemorized});
            this.lvSpells.ContextMenuStrip = this.contextMenuStrip1;
            this.lvSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSpells.FullRowSelect = true;
            this.lvSpells.HideSelection = false;
            this.lvSpells.Location = new System.Drawing.Point(0, 0);
            this.lvSpells.Name = "lvSpells";
            this.lvSpells.Size = new System.Drawing.Size(437, 309);
            this.lvSpells.TabIndex = 0;
            this.lvSpells.UseCompatibleStateImageBehavior = false;
            this.lvSpells.View = System.Windows.Forms.View.Details;
            this.lvSpells.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSpells_ColumnClick);
            this.lvSpells.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSpells_ItemCheck);
            this.lvSpells.SelectedIndexChanged += new System.EventHandler(this.lvSpells_SelectedIndexChanged);
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 50;
            // 
            // chLevel
            // 
            this.chLevel.Text = "Lev";
            this.chLevel.Width = 31;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 219;
            // 
            // chBook
            // 
            this.chBook.Text = "Book";
            this.chBook.Width = 37;
            // 
            // chSelected
            // 
            this.chSelected.Text = "Sel.";
            this.chSelected.Width = 31;
            // 
            // chMemorized
            // 
            this.chMemorized.Text = "Mem.";
            this.chMemorized.Width = 38;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMemorizeMaximum,
            this.miUnmemorize});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // miMemorizeMaximum
            // 
            this.miMemorizeMaximum.Name = "miMemorizeMaximum";
            this.miMemorizeMaximum.Size = new System.Drawing.Size(211, 22);
            this.miMemorizeMaximum.Text = "&Memorize maximum";
            this.miMemorizeMaximum.Click += new System.EventHandler(this.miMemorizeMaximum_Click);
            // 
            // miUnmemorize
            // 
            this.miUnmemorize.Name = "miUnmemorize";
            this.miUnmemorize.Size = new System.Drawing.Size(211, 22);
            this.miUnmemorize.Text = "&Unmemorize and deselect";
            this.miUnmemorize.Click += new System.EventHandler(this.miUnmemorize_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Long:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Short:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Learned:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Target:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level:";
            // 
            // labelLong
            // 
            this.labelLong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLong.Location = new System.Drawing.Point(52, 215);
            this.labelLong.Name = "labelLong";
            this.labelLong.Size = new System.Drawing.Size(321, 90);
            this.labelLong.TabIndex = 21;
            this.labelLong.Text = "SPELL LONG DESCRIPTION";
            // 
            // labelShort
            // 
            this.labelShort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShort.Location = new System.Drawing.Point(52, 179);
            this.labelShort.Name = "labelShort";
            this.labelShort.Size = new System.Drawing.Size(321, 36);
            this.labelShort.TabIndex = 19;
            this.labelShort.Text = "SPELL SHORT DESCRIPTION";
            // 
            // labelLearned
            // 
            this.labelLearned.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLearned.Location = new System.Drawing.Point(52, 147);
            this.labelLearned.Name = "labelLearned";
            this.labelLearned.Size = new System.Drawing.Size(321, 32);
            this.labelLearned.TabIndex = 17;
            this.labelLearned.Text = "SPELL LEARNED";
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(63, 23);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(87, 13);
            this.labelTarget.TabIndex = 8;
            this.labelTarget.Text = "SPELL TARGET";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(192, 3);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(27, 13);
            this.labelLevel.TabIndex = 4;
            this.labelLevel.Text = "LEV";
            // 
            // labelIndex
            // 
            this.labelIndex.AutoSize = true;
            this.labelIndex.Location = new System.Drawing.Point(138, 3);
            this.labelIndex.Name = "labelIndex";
            this.labelIndex.Size = new System.Drawing.Size(14, 13);
            this.labelIndex.TabIndex = 2;
            this.labelIndex.Text = "#";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(259, 3);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(35, 13);
            this.labelType.TabIndex = 6;
            this.labelType.Text = "TYPE";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(105, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Index:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Type:";
            // 
            // scSpellList
            // 
            this.scSpellList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSpellList.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSpellList.Location = new System.Drawing.Point(0, 0);
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
            this.scSpellList.Size = new System.Drawing.Size(437, 343);
            this.scSpellList.SplitterDistance = 309;
            this.scSpellList.TabIndex = 80;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(4, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 0;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(40, 5);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(394, 20);
            this.tbFind.TabIndex = 1;
            // 
            // scSpellsInfo
            // 
            this.scSpellsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSpellsInfo.Location = new System.Drawing.Point(0, 0);
            this.scSpellsInfo.Name = "scSpellsInfo";
            // 
            // scSpellsInfo.Panel1
            // 
            this.scSpellsInfo.Panel1.Controls.Add(this.scSpellList);
            // 
            // scSpellsInfo.Panel2
            // 
            this.scSpellsInfo.Panel2.Controls.Add(this.nudMemorized);
            this.scSpellsInfo.Panel2.Controls.Add(this.nudSelected);
            this.scSpellsInfo.Panel2.Controls.Add(this.cbInSpellbook);
            this.scSpellsInfo.Panel2.Controls.Add(this.label11);
            this.scSpellsInfo.Panel2.Controls.Add(this.btnCancel);
            this.scSpellsInfo.Panel2.Controls.Add(this.label8);
            this.scSpellsInfo.Panel2.Controls.Add(this.btnOK);
            this.scSpellsInfo.Panel2.Controls.Add(this.label10);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelType);
            this.scSpellsInfo.Panel2.Controls.Add(this.label3);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelTotalMemorized);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelTotalSelected);
            this.scSpellsInfo.Panel2.Controls.Add(this.label4);
            this.scSpellsInfo.Panel2.Controls.Add(this.label2);
            this.scSpellsInfo.Panel2.Controls.Add(this.label9);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelIndex);
            this.scSpellsInfo.Panel2.Controls.Add(this.label7);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelLevel);
            this.scSpellsInfo.Panel2.Controls.Add(this.label6);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelTarget);
            this.scSpellsInfo.Panel2.Controls.Add(this.label1);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelLearned);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelLong);
            this.scSpellsInfo.Panel2.Controls.Add(this.labelShort);
            this.scSpellsInfo.Size = new System.Drawing.Size(826, 343);
            this.scSpellsInfo.SplitterDistance = 437;
            this.scSpellsInfo.TabIndex = 62;
            // 
            // nudMemorized
            // 
            this.nudMemorized.Location = new System.Drawing.Point(65, 65);
            this.nudMemorized.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudMemorized.Name = "nudMemorized";
            this.nudMemorized.Size = new System.Drawing.Size(55, 20);
            this.nudMemorized.TabIndex = 13;
            this.nudMemorized.ValueChanged += new System.EventHandler(this.nudMemorized_ValueChanged);
            // 
            // nudSelected
            // 
            this.nudSelected.Location = new System.Drawing.Point(65, 40);
            this.nudSelected.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudSelected.Name = "nudSelected";
            this.nudSelected.Size = new System.Drawing.Size(55, 20);
            this.nudSelected.TabIndex = 10;
            this.nudSelected.ValueChanged += new System.EventHandler(this.nudSelected_ValueChanged);
            // 
            // cbInSpellbook
            // 
            this.cbInSpellbook.AutoSize = true;
            this.cbInSpellbook.Location = new System.Drawing.Point(2, 2);
            this.cbInSpellbook.Name = "cbInSpellbook";
            this.cbInSpellbook.Size = new System.Drawing.Size(83, 17);
            this.cbInSpellbook.TabIndex = 0;
            this.cbInSpellbook.Text = "In spellbook";
            this.cbInSpellbook.UseVisualStyleBackColor = true;
            this.cbInSpellbook.CheckedChanged += new System.EventHandler(this.cbInSpellbook_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Memorized:";
            // 
            // labelTotalMemorized
            // 
            this.labelTotalMemorized.AutoSize = true;
            this.labelTotalMemorized.Location = new System.Drawing.Point(138, 67);
            this.labelTotalMemorized.Name = "labelTotalMemorized";
            this.labelTotalMemorized.Size = new System.Drawing.Size(114, 13);
            this.labelTotalMemorized.TabIndex = 14;
            this.labelTotalMemorized.Text = "Total: 0 Mage, 0 Cleric";
            // 
            // labelTotalSelected
            // 
            this.labelTotalSelected.AutoSize = true;
            this.labelTotalSelected.Location = new System.Drawing.Point(138, 42);
            this.labelTotalSelected.Name = "labelTotalSelected";
            this.labelTotalSelected.Size = new System.Drawing.Size(114, 13);
            this.labelTotalSelected.TabIndex = 11;
            this.labelTotalSelected.Text = "Total: 0 Mage, 0 Cleric";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 32);
            this.label4.TabIndex = 15;
            this.label4.Text = "The maximum number of selected and memorized spells combined is 30 per spell type" +
    " (Mage, Cleric)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Selected:";
            // 
            // EditDnDSpellsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 343);
            this.Controls.Add(this.scSpellsInfo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(491, 304);
            this.Name = "EditDnDSpellsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Memorized Spells";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditDnDSpellsForm_FormClosing);
            this.Load += new System.EventHandler(this.KnownSpellsEditForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KnownSpellsEditForm_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.scSpellList.Panel1.ResumeLayout(false);
            this.scSpellList.Panel2.ResumeLayout(false);
            this.scSpellList.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellList)).EndInit();
            this.scSpellList.ResumeLayout(false);
            this.scSpellsInfo.Panel1.ResumeLayout(false);
            this.scSpellsInfo.Panel2.ResumeLayout(false);
            this.scSpellsInfo.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSpellsInfo)).EndInit();
            this.scSpellsInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMemorized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvSpells;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLong;
        private System.Windows.Forms.Label labelShort;
        private System.Windows.Forms.Label labelLearned;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelIndex;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer scSpellList;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.SplitContainer scSpellsInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miMemorizeMaximum;
        private System.Windows.Forms.ToolStripMenuItem miUnmemorize;
        private System.Windows.Forms.ColumnHeader chBook;
        private System.Windows.Forms.ColumnHeader chSelected;
        private System.Windows.Forms.ColumnHeader chMemorized;
        private System.Windows.Forms.NumericUpDown nudMemorized;
        private System.Windows.Forms.NumericUpDown nudSelected;
        private System.Windows.Forms.CheckBox cbInSpellbook;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTotalMemorized;
        private System.Windows.Forms.Label labelTotalSelected;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}