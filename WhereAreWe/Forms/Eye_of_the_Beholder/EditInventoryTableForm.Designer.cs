namespace WhereAreWe
{
    partial class EditInventoryTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInventoryTableForm));
            this.scInventoryTable = new System.Windows.Forms.SplitContainer();
            this.labelCharInventory = new System.Windows.Forms.Label();
            this.lvCharInventory = new WhereAreWe.TipListView();
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPointer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmInventory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miInvRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.miInvChange = new System.Windows.Forms.ToolStripMenuItem();
            this.miInvShowInMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCharName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.scMasterFind = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.lvMasterItems = new WhereAreWe.TipListView();
            this.chIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIdentified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMagical = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCharges = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBasicItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDamage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chArmorClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDungeonLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chModifier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPrev = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmMasterTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMasterRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterPlaceInBackpack = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterNext = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterIdentified = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scInventoryTable)).BeginInit();
            this.scInventoryTable.Panel1.SuspendLayout();
            this.scInventoryTable.Panel2.SuspendLayout();
            this.scInventoryTable.SuspendLayout();
            this.cmInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMasterFind)).BeginInit();
            this.scMasterFind.Panel1.SuspendLayout();
            this.scMasterFind.Panel2.SuspendLayout();
            this.scMasterFind.SuspendLayout();
            this.cmMasterTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // scInventoryTable
            // 
            this.scInventoryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInventoryTable.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInventoryTable.Location = new System.Drawing.Point(0, 0);
            this.scInventoryTable.Name = "scInventoryTable";
            // 
            // scInventoryTable.Panel1
            // 
            this.scInventoryTable.Panel1.Controls.Add(this.labelCharInventory);
            this.scInventoryTable.Panel1.Controls.Add(this.lvCharInventory);
            this.scInventoryTable.Panel1.Controls.Add(this.labelCharName);
            // 
            // scInventoryTable.Panel2
            // 
            this.scInventoryTable.Panel2.Controls.Add(this.btnOK);
            this.scInventoryTable.Panel2.Controls.Add(this.btnCancel);
            this.scInventoryTable.Panel2.Controls.Add(this.scMasterFind);
            this.scInventoryTable.Size = new System.Drawing.Size(1076, 462);
            this.scInventoryTable.SplitterDistance = 308;
            this.scInventoryTable.TabIndex = 0;
            // 
            // labelCharInventory
            // 
            this.labelCharInventory.AutoSize = true;
            this.labelCharInventory.Location = new System.Drawing.Point(3, 6);
            this.labelCharInventory.Name = "labelCharInventory";
            this.labelCharInventory.Size = new System.Drawing.Size(103, 13);
            this.labelCharInventory.TabIndex = 0;
            this.labelCharInventory.Text = "Character Inventory:";
            // 
            // lvCharInventory
            // 
            this.lvCharInventory.AllowDrop = true;
            this.lvCharInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCharInventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLocation,
            this.chPointer,
            this.chDescription});
            this.lvCharInventory.ContextMenuStrip = this.cmInventory;
            this.lvCharInventory.FullRowSelect = true;
            this.lvCharInventory.HideSelection = false;
            this.lvCharInventory.Location = new System.Drawing.Point(3, 22);
            this.lvCharInventory.Name = "lvCharInventory";
            this.lvCharInventory.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvCharInventory.Size = new System.Drawing.Size(304, 437);
            this.lvCharInventory.TabIndex = 1;
            this.lvCharInventory.TipDelay = 700;
            this.lvCharInventory.TipDuration = 30000;
            this.lvCharInventory.UseCompatibleStateImageBehavior = false;
            this.lvCharInventory.View = System.Windows.Forms.View.Details;
            this.lvCharInventory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvCharInventory_ColumnClick);
            this.lvCharInventory.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvCharInventory_DragDrop);
            this.lvCharInventory.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvCharInventory_DragEnter);
            this.lvCharInventory.DragOver += new System.Windows.Forms.DragEventHandler(this.lvCharInventory_DragOver);
            this.lvCharInventory.DoubleClick += new System.EventHandler(this.lvCharInventory_DoubleClick);
            this.lvCharInventory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvCharInventory_KeyDown);
            // 
            // chLocation
            // 
            this.chLocation.Text = "Location";
            this.chLocation.Width = 75;
            // 
            // chPointer
            // 
            this.chPointer.Text = "ID";
            this.chPointer.Width = 35;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 99;
            // 
            // cmInventory
            // 
            this.cmInventory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInvRemove,
            this.miInvChange,
            this.miInvShowInMaster});
            this.cmInventory.Name = "cmInventory";
            this.cmInventory.ShowImageMargin = false;
            this.cmInventory.Size = new System.Drawing.Size(152, 70);
            // 
            // miInvRemove
            // 
            this.miInvRemove.Name = "miInvRemove";
            this.miInvRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miInvRemove.Size = new System.Drawing.Size(151, 22);
            this.miInvRemove.Text = "&Remove";
            this.miInvRemove.Click += new System.EventHandler(this.miInvRemove_Click);
            // 
            // miInvChange
            // 
            this.miInvChange.Name = "miInvChange";
            this.miInvChange.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miInvChange.Size = new System.Drawing.Size(151, 22);
            this.miInvChange.Text = "&Change";
            this.miInvChange.Click += new System.EventHandler(this.miInvChange_Click);
            // 
            // miInvShowInMaster
            // 
            this.miInvShowInMaster.Name = "miInvShowInMaster";
            this.miInvShowInMaster.Size = new System.Drawing.Size(151, 22);
            this.miInvShowInMaster.Text = "&Show in Master List";
            this.miInvShowInMaster.Click += new System.EventHandler(this.miInvShowInMaster_Click);
            // 
            // labelCharName
            // 
            this.labelCharName.AutoSize = true;
            this.labelCharName.Location = new System.Drawing.Point(108, 6);
            this.labelCharName.Name = "labelCharName";
            this.labelCharName.Size = new System.Drawing.Size(38, 13);
            this.labelCharName.TabIndex = 0;
            this.labelCharName.Text = "NAME";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(603, 435);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(684, 435);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // scMasterFind
            // 
            this.scMasterFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scMasterFind.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMasterFind.IsSplitterFixed = true;
            this.scMasterFind.Location = new System.Drawing.Point(3, 3);
            this.scMasterFind.Name = "scMasterFind";
            this.scMasterFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMasterFind.Panel1
            // 
            this.scMasterFind.Panel1.Controls.Add(this.label1);
            this.scMasterFind.Panel1.Controls.Add(this.lvMasterItems);
            // 
            // scMasterFind.Panel2
            // 
            this.scMasterFind.Panel2.Controls.Add(this.label52);
            this.scMasterFind.Panel2.Controls.Add(this.tbFind);
            this.scMasterFind.Size = new System.Drawing.Size(756, 426);
            this.scMasterFind.SplitterDistance = 397;
            this.scMasterFind.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master Item Table";
            // 
            // lvMasterItems
            // 
            this.lvMasterItems.AllowDrop = true;
            this.lvMasterItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMasterItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndex,
            this.chIdentified,
            this.chMagical,
            this.chCharges,
            this.chBasicItem,
            this.chDisplayName,
            this.chDamage,
            this.chArmorClass,
            this.chDungeonLocation,
            this.chModifier,
            this.chPrev,
            this.chNext});
            this.lvMasterItems.ContextMenuStrip = this.cmMasterTable;
            this.lvMasterItems.FullRowSelect = true;
            this.lvMasterItems.HideSelection = false;
            this.lvMasterItems.Location = new System.Drawing.Point(0, 19);
            this.lvMasterItems.Name = "lvMasterItems";
            this.lvMasterItems.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvMasterItems.Size = new System.Drawing.Size(754, 378);
            this.lvMasterItems.TabIndex = 1;
            this.lvMasterItems.TipDelay = 700;
            this.lvMasterItems.TipDuration = 30000;
            this.lvMasterItems.UseCompatibleStateImageBehavior = false;
            this.lvMasterItems.View = System.Windows.Forms.View.Details;
            this.lvMasterItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMasterItems_ColumnClick);
            this.lvMasterItems.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvMasterItems_ItemDrag);
            this.lvMasterItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvMasterItems_DragEnter);
            this.lvMasterItems.DragOver += new System.Windows.Forms.DragEventHandler(this.lvMasterItems_DragOver);
            this.lvMasterItems.DoubleClick += new System.EventHandler(this.lvMasterItems_DoubleClick);
            // 
            // chIndex
            // 
            this.chIndex.Text = "ID";
            this.chIndex.Width = 34;
            // 
            // chIdentified
            // 
            this.chIdentified.Text = "Iden.";
            this.chIdentified.Width = 37;
            // 
            // chMagical
            // 
            this.chMagical.Text = "Mag.";
            this.chMagical.Width = 36;
            // 
            // chCharges
            // 
            this.chCharges.Text = "Chg.";
            this.chCharges.Width = 36;
            // 
            // chBasicItem
            // 
            this.chBasicItem.Text = "Basic Item";
            this.chBasicItem.Width = 78;
            // 
            // chDisplayName
            // 
            this.chDisplayName.Text = "Display Name";
            this.chDisplayName.Width = 162;
            // 
            // chDamage
            // 
            this.chDamage.Text = "Damage";
            this.chDamage.Width = 119;
            // 
            // chArmorClass
            // 
            this.chArmorClass.Text = "AC";
            this.chArmorClass.Width = 31;
            // 
            // chDungeonLocation
            // 
            this.chDungeonLocation.Text = "Location";
            this.chDungeonLocation.Width = 116;
            // 
            // chModifier
            // 
            this.chModifier.Text = "Modifier";
            this.chModifier.Width = 81;
            // 
            // chPrev
            // 
            this.chPrev.Text = "Prev";
            // 
            // chNext
            // 
            this.chNext.Text = "Next";
            // 
            // cmMasterTable
            // 
            this.cmMasterTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMasterRemove,
            this.miMasterDuplicate,
            this.miMasterPlaceInBackpack,
            this.miMasterNext,
            this.miMasterPrevious,
            this.miMasterIdentified,
            this.miMasterEdit,
            this.miMasterCopy});
            this.cmMasterTable.Name = "cmMasterTable";
            this.cmMasterTable.ShowCheckMargin = true;
            this.cmMasterTable.ShowImageMargin = false;
            this.cmMasterTable.Size = new System.Drawing.Size(201, 202);
            this.cmMasterTable.Opening += new System.ComponentModel.CancelEventHandler(this.cmMasterTable_Opening);
            // 
            // miMasterRemove
            // 
            this.miMasterRemove.Name = "miMasterRemove";
            this.miMasterRemove.Size = new System.Drawing.Size(200, 22);
            this.miMasterRemove.Text = "&Remove";
            this.miMasterRemove.Click += new System.EventHandler(this.miMasterRemove_Click);
            // 
            // miMasterDuplicate
            // 
            this.miMasterDuplicate.Name = "miMasterDuplicate";
            this.miMasterDuplicate.Size = new System.Drawing.Size(200, 22);
            this.miMasterDuplicate.Text = "&Duplicate";
            this.miMasterDuplicate.Click += new System.EventHandler(this.miMasterDuplicate_Click);
            // 
            // miMasterPlaceInBackpack
            // 
            this.miMasterPlaceInBackpack.Name = "miMasterPlaceInBackpack";
            this.miMasterPlaceInBackpack.Size = new System.Drawing.Size(200, 22);
            this.miMasterPlaceInBackpack.Text = "Place in &Backpack";
            this.miMasterPlaceInBackpack.Click += new System.EventHandler(this.miMasterPlaceInBackpack_Click);
            // 
            // miMasterNext
            // 
            this.miMasterNext.Name = "miMasterNext";
            this.miMasterNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.miMasterNext.Size = new System.Drawing.Size(200, 22);
            this.miMasterNext.Text = "&Next Item";
            this.miMasterNext.Click += new System.EventHandler(this.miMasterNext_Click);
            // 
            // miMasterPrevious
            // 
            this.miMasterPrevious.Name = "miMasterPrevious";
            this.miMasterPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.miMasterPrevious.Size = new System.Drawing.Size(200, 22);
            this.miMasterPrevious.Text = "&Previous Item";
            this.miMasterPrevious.Click += new System.EventHandler(this.miMasterPrevious_Click);
            // 
            // miMasterIdentified
            // 
            this.miMasterIdentified.Name = "miMasterIdentified";
            this.miMasterIdentified.Size = new System.Drawing.Size(200, 22);
            this.miMasterIdentified.Text = "&Identified";
            this.miMasterIdentified.Click += new System.EventHandler(this.miMasterIdentified_Click);
            // 
            // miMasterEdit
            // 
            this.miMasterEdit.Name = "miMasterEdit";
            this.miMasterEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.miMasterEdit.Size = new System.Drawing.Size(200, 22);
            this.miMasterEdit.Text = "&Edit";
            this.miMasterEdit.Click += new System.EventHandler(this.miMasterEdit_Click);
            // 
            // miMasterCopy
            // 
            this.miMasterCopy.Name = "miMasterCopy";
            this.miMasterCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miMasterCopy.Size = new System.Drawing.Size(200, 22);
            this.miMasterCopy.Text = "&Copy";
            this.miMasterCopy.Click += new System.EventHandler(this.miMasterCopy_Click);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 5);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 2;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(39, 2);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(715, 20);
            this.tbFind.TabIndex = 3;
            // 
            // EditInventoryTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 462);
            this.Controls.Add(this.scInventoryTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "EditInventoryTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory and Item Table";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditInventoryTableForm_FormClosing);
            this.Load += new System.EventHandler(this.EditInventoryTableForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditInventoryTableForm_KeyDown);
            this.scInventoryTable.Panel1.ResumeLayout(false);
            this.scInventoryTable.Panel1.PerformLayout();
            this.scInventoryTable.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInventoryTable)).EndInit();
            this.scInventoryTable.ResumeLayout(false);
            this.cmInventory.ResumeLayout(false);
            this.scMasterFind.Panel1.ResumeLayout(false);
            this.scMasterFind.Panel1.PerformLayout();
            this.scMasterFind.Panel2.ResumeLayout(false);
            this.scMasterFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMasterFind)).EndInit();
            this.scMasterFind.ResumeLayout(false);
            this.cmMasterTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scInventoryTable;
        private TipListView lvCharInventory;
        private System.Windows.Forms.ColumnHeader chLocation;
        private System.Windows.Forms.ColumnHeader chPointer;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.Label labelCharName;
        private System.Windows.Forms.Label labelCharInventory;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private TipListView lvMasterItems;
        private System.Windows.Forms.ColumnHeader chIndex;
        private System.Windows.Forms.ColumnHeader chIdentified;
        private System.Windows.Forms.ColumnHeader chMagical;
        private System.Windows.Forms.ColumnHeader chCharges;
        private System.Windows.Forms.ColumnHeader chBasicItem;
        private System.Windows.Forms.ColumnHeader chDisplayName;
        private System.Windows.Forms.ColumnHeader chDungeonLocation;
        private System.Windows.Forms.ColumnHeader chModifier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer scMasterFind;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.ContextMenuStrip cmInventory;
        private System.Windows.Forms.ToolStripMenuItem miInvRemove;
        private System.Windows.Forms.ToolStripMenuItem miInvChange;
        private System.Windows.Forms.ContextMenuStrip cmMasterTable;
        private System.Windows.Forms.ToolStripMenuItem miMasterRemove;
        private System.Windows.Forms.ToolStripMenuItem miMasterPlaceInBackpack;
        private System.Windows.Forms.ColumnHeader chDamage;
        private System.Windows.Forms.ColumnHeader chArmorClass;
        private System.Windows.Forms.ToolStripMenuItem miInvShowInMaster;
        private System.Windows.Forms.ColumnHeader chNext;
        private System.Windows.Forms.ColumnHeader chPrev;
        private System.Windows.Forms.ToolStripMenuItem miMasterNext;
        private System.Windows.Forms.ToolStripMenuItem miMasterPrevious;
        private System.Windows.Forms.ToolStripMenuItem miMasterIdentified;
        private System.Windows.Forms.ToolStripMenuItem miMasterEdit;
        private System.Windows.Forms.ToolStripMenuItem miMasterCopy;
        private System.Windows.Forms.ToolStripMenuItem miMasterDuplicate;
    }
}