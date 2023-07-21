namespace WhereAreWe
{
    partial class ItemCompareForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemCompareForm));
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelWhichSlot = new System.Windows.Forms.Label();
            this.lvCompare = new System.Windows.Forms.ListView();
            this.chCompare = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEquip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCharacter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmCompare = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxMoveToCharacter = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmCompare.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInfo
            // 
            this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo.Location = new System.Drawing.Point(0, 0);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ReadOnly = true;
            this.tbInfo.Size = new System.Drawing.Size(261, 25);
            this.tbInfo.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(20, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(5, 9);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelWhichSlot);
            this.splitContainer1.Panel2.Controls.Add(this.lvCompare);
            this.splitContainer1.Size = new System.Drawing.Size(261, 134);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 2;
            // 
            // labelWhichSlot
            // 
            this.labelWhichSlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWhichSlot.Location = new System.Drawing.Point(3, 0);
            this.labelWhichSlot.Name = "labelWhichSlot";
            this.labelWhichSlot.Size = new System.Drawing.Size(260, 17);
            this.labelWhichSlot.TabIndex = 1;
            this.labelWhichSlot.Text = "In the ? slot of characters who could use this item";
            this.labelWhichSlot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvCompare
            // 
            this.lvCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCompare.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCompare,
            this.chIndex,
            this.chDamAC,
            this.chEquip,
            this.chItemName,
            this.chCharacter});
            this.lvCompare.ContextMenuStrip = this.cmCompare;
            this.lvCompare.FullRowSelect = true;
            this.lvCompare.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCompare.HideSelection = false;
            this.lvCompare.Location = new System.Drawing.Point(0, 20);
            this.lvCompare.MultiSelect = false;
            this.lvCompare.Name = "lvCompare";
            this.lvCompare.Size = new System.Drawing.Size(261, 80);
            this.lvCompare.TabIndex = 0;
            this.lvCompare.UseCompatibleStateImageBehavior = false;
            this.lvCompare.View = System.Windows.Forms.View.Details;
            this.lvCompare.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvCompare_KeyDown);
            this.lvCompare.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvCompare_MouseDoubleClick);
            // 
            // chCompare
            // 
            this.chCompare.Text = "±";
            this.chCompare.Width = 18;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 19;
            // 
            // chDamAC
            // 
            this.chDamAC.Text = "Dam/AC";
            this.chDamAC.Width = 61;
            // 
            // chEquip
            // 
            this.chEquip.Text = "Equip";
            this.chEquip.Width = 143;
            // 
            // chItemName
            // 
            this.chItemName.Text = "Item";
            this.chItemName.Width = 129;
            // 
            // chCharacter
            // 
            this.chCharacter.Text = "Character";
            this.chCharacter.Width = 59;
            // 
            // cmCompare
            // 
            this.cmCompare.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxMoveToCharacter});
            this.cmCompare.Name = "cmCompare";
            this.cmCompare.ShowImageMargin = false;
            this.cmCompare.Size = new System.Drawing.Size(158, 26);
            // 
            // miCtxMoveToCharacter
            // 
            this.miCtxMoveToCharacter.Name = "miCtxMoveToCharacter";
            this.miCtxMoveToCharacter.Size = new System.Drawing.Size(157, 22);
            this.miCtxMoveToCharacter.Text = "&Move to this character";
            this.miCtxMoveToCharacter.Click += new System.EventHandler(this.miCtxMoveToCharacter_Click);
            // 
            // ItemCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(270, 145);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(278, 100);
            this.Name = "ItemCompareForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compare Items";
            this.Load += new System.EventHandler(this.ViewInfoForm_Load);
            this.SizeChanged += new System.EventHandler(this.ViewInfoForm_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmCompare.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelWhichSlot;
        private System.Windows.Forms.ListView lvCompare;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chCharacter;
        private System.Windows.Forms.ColumnHeader chDamAC;
        private System.Windows.Forms.ColumnHeader chEquip;
        private System.Windows.Forms.ColumnHeader chItemName;
        private System.Windows.Forms.ColumnHeader chCompare;
        private System.Windows.Forms.ContextMenuStrip cmCompare;
        private System.Windows.Forms.ToolStripMenuItem miCtxMoveToCharacter;
    }
}