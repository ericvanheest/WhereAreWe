namespace WhereAreWe
{
    partial class ItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsForm));
            this.lvItems = new WhereAreWe.TipListView();
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUsableClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUsableAlignments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEquipAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEquipBonus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaterial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResistance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxRemoveAllFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxFilterUsableBy = new System.Windows.Forms.ToolStripMenuItem();
            this.nAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxFilterText = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvItems
            // 
            this.lvItems.AllowColumnReorder = true;
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chType,
            this.chIndex,
            this.chName,
            this.chUsableClass,
            this.chUsableAlignments,
            this.chAC,
            this.chDamage,
            this.chEquipAttribute,
            this.chEquipBonus,
            this.chUse,
            this.chMaterial,
            this.chElement,
            this.chResistance,
            this.chValue,
            this.chRange});
            this.lvItems.ContextMenuStrip = this.cmItems;
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.FullRowSelect = true;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(0, 0);
            this.lvItems.Name = "lvItems";
            this.lvItems.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvItems.Size = new System.Drawing.Size(1090, 626);
            this.lvItems.TabIndex = 0;
            this.lvItems.TipDelay = 1000;
            this.lvItems.TipDuration = 30000;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            this.lvItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvItems_ColumnClick);
            this.lvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvItems_KeyDown);
            this.lvItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvItems_MouseDoubleClick);
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 76;
            // 
            // chIndex
            // 
            this.chIndex.Text = "#";
            this.chIndex.Width = 34;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 71;
            // 
            // chUsableClass
            // 
            this.chUsableClass.Text = "Class";
            // 
            // chUsableAlignments
            // 
            this.chUsableAlignments.Text = "Align";
            // 
            // chAC
            // 
            this.chAC.Text = "AC";
            this.chAC.Width = 27;
            // 
            // chDamage
            // 
            this.chDamage.Text = "Damage";
            this.chDamage.Width = 52;
            // 
            // chEquipAttribute
            // 
            this.chEquipAttribute.Text = "Attribute";
            this.chEquipAttribute.Width = 87;
            // 
            // chEquipBonus
            // 
            this.chEquipBonus.Text = "Equip";
            this.chEquipBonus.Width = 54;
            // 
            // chUse
            // 
            this.chUse.Text = "Use Effect";
            this.chUse.Width = 203;
            // 
            // chMaterial
            // 
            this.chMaterial.Text = "Material";
            this.chMaterial.Width = 79;
            // 
            // chElement
            // 
            this.chElement.Text = "Element";
            this.chElement.Width = 99;
            // 
            // chResistance
            // 
            this.chResistance.Text = "Resists";
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            // 
            // chRange
            // 
            this.chRange.Text = "Range";
            this.chRange.Width = 45;
            // 
            // cmItems
            // 
            this.cmItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxCopy,
            this.miCtxRemoveAllFilters,
            this.miCtxFilterUsableBy,
            this.miCtxFilterText});
            this.cmItems.Name = "cmMonsters";
            this.cmItems.ShowCheckMargin = true;
            this.cmItems.ShowImageMargin = false;
            this.cmItems.Size = new System.Drawing.Size(237, 114);
            this.cmItems.Opening += new System.ComponentModel.CancelEventHandler(this.cmItems_Opening);
            // 
            // miCtxCopy
            // 
            this.miCtxCopy.Name = "miCtxCopy";
            this.miCtxCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCtxCopy.Size = new System.Drawing.Size(236, 22);
            this.miCtxCopy.Text = "&Copy";
            this.miCtxCopy.Click += new System.EventHandler(this.miCtxCopy_Click);
            // 
            // miCtxRemoveAllFilters
            // 
            this.miCtxRemoveAllFilters.Name = "miCtxRemoveAllFilters";
            this.miCtxRemoveAllFilters.Size = new System.Drawing.Size(236, 22);
            this.miCtxRemoveAllFilters.Text = "&Remove all filters";
            this.miCtxRemoveAllFilters.Click += new System.EventHandler(this.miCtxRemoveAllFilters_Click);
            // 
            // miCtxFilterUsableBy
            // 
            this.miCtxFilterUsableBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nAToolStripMenuItem});
            this.miCtxFilterUsableBy.Name = "miCtxFilterUsableBy";
            this.miCtxFilterUsableBy.Size = new System.Drawing.Size(236, 22);
            this.miCtxFilterUsableBy.Text = "Show only usable by";
            this.miCtxFilterUsableBy.DropDownOpening += new System.EventHandler(this.miCtxFilterUsableBy_DropDownOpening);
            // 
            // nAToolStripMenuItem
            // 
            this.nAToolStripMenuItem.Name = "nAToolStripMenuItem";
            this.nAToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.nAToolStripMenuItem.Text = "N/A";
            // 
            // miCtxFilterText
            // 
            this.miCtxFilterText.Name = "miCtxFilterText";
            this.miCtxFilterText.Size = new System.Drawing.Size(236, 22);
            this.miCtxFilterText.Text = "Show only entries matching \"text\"";
            this.miCtxFilterText.Click += new System.EventHandler(this.miCtxFilterText_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(104, 362);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.lvItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label52);
            this.splitContainer1.Panel2.Controls.Add(this.tbFind);
            this.splitContainer1.Panel2MinSize = 20;
            this.splitContainer1.Size = new System.Drawing.Size(1090, 655);
            this.splitContainer1.SplitterDistance = 626;
            this.splitContainer1.TabIndex = 2;
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
            this.tbFind.Size = new System.Drawing.Size(1051, 20);
            this.tbFind.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(554, 444);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1090, 655);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ItemsForm";
            this.Text = "Item List";
            this.Load += new System.EventHandler(this.ItemsForm_Load);
            this.cmItems.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WhereAreWe.TipListView lvItems;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chAC;
        private System.Windows.Forms.ColumnHeader chDamage;
        private System.Windows.Forms.ColumnHeader chEquipAttribute;
        private System.Windows.Forms.ColumnHeader chElement;
        private System.Windows.Forms.ColumnHeader chUse;
        private System.Windows.Forms.ColumnHeader chEquipBonus;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chMaterial;
        private System.Windows.Forms.ColumnHeader chUsableClass;
        private System.Windows.Forms.ColumnHeader chUsableAlignments;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ColumnHeader chResistance;
        private System.Windows.Forms.ContextMenuStrip cmItems;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopy;
        private System.Windows.Forms.ColumnHeader chRange;
        private System.Windows.Forms.ToolStripMenuItem miCtxRemoveAllFilters;
        private System.Windows.Forms.ToolStripMenuItem miCtxFilterUsableBy;
        private System.Windows.Forms.ToolStripMenuItem nAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCtxFilterText;
    }
}