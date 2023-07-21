namespace WhereAreWe
{
    partial class InventoryManipulatorForm : HackerBasedForm
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
            this.cmBackpack = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBackpackDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackItemDisplayFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackpackBrowseForRosterFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBag = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmBagDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miBagItemDisplayFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.miBagBrowseForRosterFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.llAddAll = new System.Windows.Forms.LinkLabel();
            this.comboCharacter = new System.Windows.Forms.ComboBox();
            this.lvBackpack = new WhereAreWe.DBListView();
            this.chBackpackItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scBagFind = new System.Windows.Forms.SplitContainer();
            this.lvBag = new WhereAreWe.DBListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUsableClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUsableAlign = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBonus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label52 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.btnBrowseRoster = new System.Windows.Forms.Button();
            this.labelBagHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scUsableBy = new System.Windows.Forms.SplitContainer();
            this.lvFilterAlign = new System.Windows.Forms.ListView();
            this.chFilterAlign = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvFilterClass = new System.Windows.Forms.ListView();
            this.chFilterClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.llNoneClass = new System.Windows.Forms.LinkLabel();
            this.llAllClass = new System.Windows.Forms.LinkLabel();
            this.cmBackpack.SuspendLayout();
            this.cmBag.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBagFind)).BeginInit();
            this.scBagFind.Panel1.SuspendLayout();
            this.scBagFind.Panel2.SuspendLayout();
            this.scBagFind.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scUsableBy)).BeginInit();
            this.scUsableBy.Panel1.SuspendLayout();
            this.scUsableBy.Panel2.SuspendLayout();
            this.scUsableBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmBackpack
            // 
            this.cmBackpack.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBackpackDelete,
            this.miBackpackItemDisplayFormat,
            this.miBackpackBrowseForRosterFile});
            this.cmBackpack.Name = "cmBag";
            this.cmBackpack.ShowImageMargin = false;
            this.cmBackpack.Size = new System.Drawing.Size(167, 70);
            // 
            // miBackpackDelete
            // 
            this.miBackpackDelete.Name = "miBackpackDelete";
            this.miBackpackDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miBackpackDelete.Size = new System.Drawing.Size(166, 22);
            this.miBackpackDelete.Text = "&Delete";
            this.miBackpackDelete.Click += new System.EventHandler(this.miBackpackDelete_Click);
            // 
            // miBackpackItemDisplayFormat
            // 
            this.miBackpackItemDisplayFormat.Name = "miBackpackItemDisplayFormat";
            this.miBackpackItemDisplayFormat.Size = new System.Drawing.Size(166, 22);
            this.miBackpackItemDisplayFormat.Text = "&Item display format";
            this.miBackpackItemDisplayFormat.Click += new System.EventHandler(this.miBackpackItemDisplayFormat_Click);
            // 
            // miBackpackBrowseForRosterFile
            // 
            this.miBackpackBrowseForRosterFile.Name = "miBackpackBrowseForRosterFile";
            this.miBackpackBrowseForRosterFile.Size = new System.Drawing.Size(166, 22);
            this.miBackpackBrowseForRosterFile.Text = "&Browse for roster file...";
            this.miBackpackBrowseForRosterFile.Click += new System.EventHandler(this.miBackpackBrowseForRosterFile_Click);
            // 
            // cmBag
            // 
            this.cmBag.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmBagDelete,
            this.miBagItemDisplayFormat,
            this.miBagBrowseForRosterFile});
            this.cmBag.Name = "cmBag";
            this.cmBag.ShowImageMargin = false;
            this.cmBag.Size = new System.Drawing.Size(167, 92);
            // 
            // cmBagDelete
            // 
            this.cmBagDelete.Name = "cmBagDelete";
            this.cmBagDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cmBagDelete.Size = new System.Drawing.Size(166, 22);
            this.cmBagDelete.Text = "&Delete";
            this.cmBagDelete.Click += new System.EventHandler(this.cmBagDelete_Click);
            // 
            // miBagItemDisplayFormat
            // 
            this.miBagItemDisplayFormat.Name = "miBagItemDisplayFormat";
            this.miBagItemDisplayFormat.Size = new System.Drawing.Size(166, 22);
            this.miBagItemDisplayFormat.Text = "&Item display format";
            this.miBagItemDisplayFormat.Click += new System.EventHandler(this.miBagItemDisplayFormat_Click);
            // 
            // miBagBrowseForRosterFile
            // 
            this.miBagBrowseForRosterFile.Name = "miBagBrowseForRosterFile";
            this.miBagBrowseForRosterFile.Size = new System.Drawing.Size(166, 22);
            this.miBagBrowseForRosterFile.Text = "&Browse for roster file...";
            this.miBagBrowseForRosterFile.Click += new System.EventHandler(this.miBagBrowseForRosterFile_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(750, 361);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(831, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(42, 17);
            this.labelStatus.Text = "Ready.";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.llAddAll);
            this.splitContainer1.Panel1.Controls.Add(this.comboCharacter);
            this.splitContainer1.Panel1.Controls.Add(this.lvBackpack);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scBagFind);
            this.splitContainer1.Panel2.Controls.Add(this.btnBrowseRoster);
            this.splitContainer1.Panel2.Controls.Add(this.labelBagHeader);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(927, 354);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 0;
            // 
            // llAddAll
            // 
            this.llAddAll.AutoSize = true;
            this.llAddAll.Location = new System.Drawing.Point(130, 6);
            this.llAddAll.Name = "llAddAll";
            this.llAddAll.Size = new System.Drawing.Size(83, 13);
            this.llAddAll.TabIndex = 1;
            this.llAddAll.TabStop = true;
            this.llAddAll.Text = "Add All Items >>";
            this.llAddAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAddAll_LinkClicked);
            // 
            // comboCharacter
            // 
            this.comboCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCharacter.FormattingEnabled = true;
            this.comboCharacter.Location = new System.Drawing.Point(0, 1);
            this.comboCharacter.Name = "comboCharacter";
            this.comboCharacter.Size = new System.Drawing.Size(128, 21);
            this.comboCharacter.TabIndex = 0;
            this.comboCharacter.SelectedIndexChanged += new System.EventHandler(this.comboCharacter_SelectedIndexChanged);
            // 
            // lvBackpack
            // 
            this.lvBackpack.AllowDrop = true;
            this.lvBackpack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBackpack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBackpackItem});
            this.lvBackpack.ContextMenuStrip = this.cmBackpack;
            this.lvBackpack.FullRowSelect = true;
            this.lvBackpack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBackpack.HideSelection = false;
            this.lvBackpack.Location = new System.Drawing.Point(0, 28);
            this.lvBackpack.Name = "lvBackpack";
            this.lvBackpack.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBackpack.ShowItemToolTips = true;
            this.lvBackpack.Size = new System.Drawing.Size(307, 325);
            this.lvBackpack.TabIndex = 2;
            this.lvBackpack.UseCompatibleStateImageBehavior = false;
            this.lvBackpack.View = System.Windows.Forms.View.Details;
            this.lvBackpack.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvBackpack_ItemDrag);
            this.lvBackpack.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvBackpack_DragDrop);
            this.lvBackpack.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvBackpack_DragEnter);
            this.lvBackpack.DragOver += new System.Windows.Forms.DragEventHandler(this.lvBackpack_DragOver);
            this.lvBackpack.DoubleClick += new System.EventHandler(this.lvBackpack_DoubleClick);
            this.lvBackpack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBackpack_KeyDown);
            // 
            // chBackpackItem
            // 
            this.chBackpackItem.Text = "Character Backpack";
            this.chBackpackItem.Width = 286;
            // 
            // scBagFind
            // 
            this.scBagFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scBagFind.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scBagFind.IsSplitterFixed = true;
            this.scBagFind.Location = new System.Drawing.Point(118, 22);
            this.scBagFind.Name = "scBagFind";
            this.scBagFind.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBagFind.Panel1
            // 
            this.scBagFind.Panel1.Controls.Add(this.lvBag);
            this.scBagFind.Panel1MinSize = 20;
            // 
            // scBagFind.Panel2
            // 
            this.scBagFind.Panel2.Controls.Add(this.label52);
            this.scBagFind.Panel2.Controls.Add(this.tbFind);
            this.scBagFind.Panel2MinSize = 20;
            this.scBagFind.Size = new System.Drawing.Size(493, 332);
            this.scBagFind.SplitterDistance = 303;
            this.scBagFind.TabIndex = 5;
            // 
            // lvBag
            // 
            this.lvBag.AllowColumnReorder = true;
            this.lvBag.AllowDrop = true;
            this.lvBag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chType,
            this.chUsableClass,
            this.chUsableAlign,
            this.chAttribute,
            this.chBonus});
            this.lvBag.ContextMenuStrip = this.cmBag;
            this.lvBag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBag.FullRowSelect = true;
            this.lvBag.HideSelection = false;
            this.lvBag.Location = new System.Drawing.Point(0, 0);
            this.lvBag.Name = "lvBag";
            this.lvBag.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvBag.ShowItemToolTips = true;
            this.lvBag.Size = new System.Drawing.Size(493, 303);
            this.lvBag.TabIndex = 6;
            this.lvBag.UseCompatibleStateImageBehavior = false;
            this.lvBag.View = System.Windows.Forms.View.Details;
            this.lvBag.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvBag_ColumnClick);
            this.lvBag.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvBag_ItemDrag);
            this.lvBag.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvBag_DragDrop);
            this.lvBag.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvBag_DragEnter);
            this.lvBag.DragOver += new System.Windows.Forms.DragEventHandler(this.lvBag_DragOver);
            this.lvBag.DoubleClick += new System.EventHandler(this.lvBag_DoubleClick);
            this.lvBag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBag_KeyDown);
            // 
            // chItem
            // 
            this.chItem.Text = "Item Description";
            this.chItem.Width = 152;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 74;
            // 
            // chUsableClass
            // 
            this.chUsableClass.Text = "Classes";
            this.chUsableClass.Width = 85;
            // 
            // chUsableAlign
            // 
            this.chUsableAlign.DisplayIndex = 5;
            this.chUsableAlign.Text = "Align";
            // 
            // chAttribute
            // 
            this.chAttribute.DisplayIndex = 3;
            this.chAttribute.Text = "Attribute";
            this.chAttribute.Width = 72;
            // 
            // chBonus
            // 
            this.chBonus.DisplayIndex = 4;
            this.chBonus.Text = "Bonus";
            this.chBonus.Width = 73;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(3, 3);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(30, 13);
            this.label52.TabIndex = 8;
            this.label52.Text = "Find:";
            // 
            // tbFind
            // 
            this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFind.Location = new System.Drawing.Point(35, 0);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(458, 20);
            this.tbFind.TabIndex = 7;
            // 
            // btnBrowseRoster
            // 
            this.btnBrowseRoster.Location = new System.Drawing.Point(1, 0);
            this.btnBrowseRoster.Name = "btnBrowseRoster";
            this.btnBrowseRoster.Size = new System.Drawing.Size(114, 21);
            this.btnBrowseRoster.TabIndex = 0;
            this.btnBrowseRoster.Text = "&Browse for Roster";
            this.btnBrowseRoster.UseVisualStyleBackColor = true;
            this.btnBrowseRoster.Click += new System.EventHandler(this.btnBrowseRoster_Click);
            // 
            // labelBagHeader
            // 
            this.labelBagHeader.AutoSize = true;
            this.labelBagHeader.Location = new System.Drawing.Point(118, 6);
            this.labelBagHeader.Name = "labelBagHeader";
            this.labelBagHeader.Size = new System.Drawing.Size(77, 13);
            this.labelBagHeader.TabIndex = 2;
            this.labelBagHeader.Text = "Bag of Holding";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.scUsableBy);
            this.groupBox1.Location = new System.Drawing.Point(1, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 332);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show usable by:";
            // 
            // scUsableBy
            // 
            this.scUsableBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scUsableBy.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scUsableBy.Location = new System.Drawing.Point(3, 16);
            this.scUsableBy.Name = "scUsableBy";
            this.scUsableBy.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scUsableBy.Panel1
            // 
            this.scUsableBy.Panel1.Controls.Add(this.lvFilterAlign);
            // 
            // scUsableBy.Panel2
            // 
            this.scUsableBy.Panel2.Controls.Add(this.lvFilterClass);
            this.scUsableBy.Panel2.Controls.Add(this.llNoneClass);
            this.scUsableBy.Panel2.Controls.Add(this.llAllClass);
            this.scUsableBy.Size = new System.Drawing.Size(108, 313);
            this.scUsableBy.SplitterDistance = 78;
            this.scUsableBy.TabIndex = 4;
            // 
            // lvFilterAlign
            // 
            this.lvFilterAlign.CheckBoxes = true;
            this.lvFilterAlign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilterAlign});
            this.lvFilterAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFilterAlign.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFilterAlign.Location = new System.Drawing.Point(0, 0);
            this.lvFilterAlign.Name = "lvFilterAlign";
            this.lvFilterAlign.Size = new System.Drawing.Size(108, 78);
            this.lvFilterAlign.TabIndex = 0;
            this.lvFilterAlign.UseCompatibleStateImageBehavior = false;
            this.lvFilterAlign.View = System.Windows.Forms.View.Details;
            this.lvFilterAlign.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFilterAlign_ItemChecked);
            this.lvFilterAlign.SelectedIndexChanged += new System.EventHandler(this.lvFilterAlign_SelectedIndexChanged);
            // 
            // chFilterAlign
            // 
            this.chFilterAlign.Text = "Alignment";
            this.chFilterAlign.Width = 77;
            // 
            // lvFilterClass
            // 
            this.lvFilterClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFilterClass.CheckBoxes = true;
            this.lvFilterClass.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilterClass});
            this.lvFilterClass.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFilterClass.Location = new System.Drawing.Point(0, 0);
            this.lvFilterClass.Name = "lvFilterClass";
            this.lvFilterClass.Size = new System.Drawing.Size(108, 210);
            this.lvFilterClass.TabIndex = 1;
            this.lvFilterClass.UseCompatibleStateImageBehavior = false;
            this.lvFilterClass.View = System.Windows.Forms.View.Details;
            this.lvFilterClass.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFilter_ItemChecked);
            this.lvFilterClass.SelectedIndexChanged += new System.EventHandler(this.lvFilter_SelectedIndexChanged);
            // 
            // chFilterClass
            // 
            this.chFilterClass.Text = "Class";
            this.chFilterClass.Width = 84;
            // 
            // llNoneClass
            // 
            this.llNoneClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llNoneClass.AutoSize = true;
            this.llNoneClass.Location = new System.Drawing.Point(26, 214);
            this.llNoneClass.Name = "llNoneClass";
            this.llNoneClass.Size = new System.Drawing.Size(33, 13);
            this.llNoneClass.TabIndex = 3;
            this.llNoneClass.TabStop = true;
            this.llNoneClass.Text = "None";
            this.llNoneClass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNoneClass_LinkClicked);
            // 
            // llAllClass
            // 
            this.llAllClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llAllClass.AutoSize = true;
            this.llAllClass.Location = new System.Drawing.Point(2, 214);
            this.llAllClass.Name = "llAllClass";
            this.llAllClass.Size = new System.Drawing.Size(18, 13);
            this.llAllClass.TabIndex = 2;
            this.llAllClass.TabStop = true;
            this.llAllClass.Text = "All";
            this.llAllClass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAllClass_LinkClicked);
            // 
            // InventoryManipulatorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 381);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(740, 320);
            this.Name = "InventoryManipulatorForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bag of Holding";
            this.Load += new System.EventHandler(this.InventoryManipulatorForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InventoryManipulatorForm_KeyDown);
            this.cmBackpack.ResumeLayout(false);
            this.cmBag.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.scBagFind.Panel1.ResumeLayout(false);
            this.scBagFind.Panel2.ResumeLayout(false);
            this.scBagFind.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBagFind)).EndInit();
            this.scBagFind.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.scUsableBy.Panel1.ResumeLayout(false);
            this.scUsableBy.Panel2.ResumeLayout(false);
            this.scUsableBy.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scUsableBy)).EndInit();
            this.scUsableBy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private WhereAreWe.DBListView lvBackpack;
        private System.Windows.Forms.ColumnHeader chBackpackItem;
        private WhereAreWe.DBListView lvBag;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboCharacter;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chUsableClass;
        private System.Windows.Forms.ColumnHeader chUsableAlign;
        private System.Windows.Forms.ContextMenuStrip cmBag;
        private System.Windows.Forms.ToolStripMenuItem cmBagDelete;
        private System.Windows.Forms.ContextMenuStrip cmBackpack;
        private System.Windows.Forms.ToolStripMenuItem miBackpackDelete;
        private System.Windows.Forms.ListView lvFilterClass;
        private System.Windows.Forms.ColumnHeader chFilterClass;
        private System.Windows.Forms.ListView lvFilterAlign;
        private System.Windows.Forms.ColumnHeader chFilterAlign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseRoster;
        private System.Windows.Forms.LinkLabel llNoneClass;
        private System.Windows.Forms.LinkLabel llAllClass;
        private System.Windows.Forms.Label labelBagHeader;
        private System.Windows.Forms.LinkLabel llAddAll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ColumnHeader chAttribute;
        private System.Windows.Forms.ColumnHeader chBonus;
        private System.Windows.Forms.ToolStripMenuItem miBackpackItemDisplayFormat;
        private System.Windows.Forms.ToolStripMenuItem miBagItemDisplayFormat;
        private System.Windows.Forms.SplitContainer scUsableBy;
        private System.Windows.Forms.SplitContainer scBagFind;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.ToolStripMenuItem miBackpackBrowseForRosterFile;
        private System.Windows.Forms.ToolStripMenuItem miBagBrowseForRosterFile;
    }
}