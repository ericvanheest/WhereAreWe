namespace WhereAreWe
{
    partial class ShopInventoryForm
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
            this.timerRefreshInfo = new System.Windows.Forms.Timer(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcInventories = new System.Windows.Forms.TabControl();
            this.tpCurrent = new System.Windows.Forms.TabPage();
            this.lvCurrent = new WhereAreWe.DBListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxItemDisplayFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tpTown1 = new System.Windows.Forms.TabPage();
            this.lvTown1 = new WhereAreWe.DBListView();
            this.chItem1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown2 = new System.Windows.Forms.TabPage();
            this.lvTown2 = new WhereAreWe.DBListView();
            this.chItem2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown3 = new System.Windows.Forms.TabPage();
            this.lvTown3 = new WhereAreWe.DBListView();
            this.chItem3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown4 = new System.Windows.Forms.TabPage();
            this.lvTown4 = new WhereAreWe.DBListView();
            this.chItems4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown5 = new System.Windows.Forms.TabPage();
            this.lvTown5 = new WhereAreWe.DBListView();
            this.chItems5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProperties5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown6 = new System.Windows.Forms.TabPage();
            this.lvTown6 = new WhereAreWe.DBListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown7 = new System.Windows.Forms.TabPage();
            this.lvTown7 = new WhereAreWe.DBListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpTown8 = new System.Windows.Forms.TabPage();
            this.lvTown8 = new WhereAreWe.DBListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcInventories.SuspendLayout();
            this.tpCurrent.SuspendLayout();
            this.cmEdit.SuspendLayout();
            this.tpTown1.SuspendLayout();
            this.tpTown2.SuspendLayout();
            this.tpTown3.SuspendLayout();
            this.tpTown4.SuspendLayout();
            this.tpTown5.SuspendLayout();
            this.tpTown6.SuspendLayout();
            this.tpTown7.SuspendLayout();
            this.tpTown8.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRefreshInfo
            // 
            this.timerRefreshInfo.Interval = 250;
            this.timerRefreshInfo.Tick += new System.EventHandler(this.timerRefreshInfo_Tick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(413, 335);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(37, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            // 
            // tcInventories
            // 
            this.tcInventories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcInventories.Controls.Add(this.tpCurrent);
            this.tcInventories.Controls.Add(this.tpTown1);
            this.tcInventories.Controls.Add(this.tpTown2);
            this.tcInventories.Controls.Add(this.tpTown3);
            this.tcInventories.Controls.Add(this.tpTown4);
            this.tcInventories.Controls.Add(this.tpTown5);
            this.tcInventories.Controls.Add(this.tpTown6);
            this.tcInventories.Controls.Add(this.tpTown7);
            this.tcInventories.Controls.Add(this.tpTown8);
            this.tcInventories.Location = new System.Drawing.Point(0, 5);
            this.tcInventories.Name = "tcInventories";
            this.tcInventories.SelectedIndex = 0;
            this.tcInventories.Size = new System.Drawing.Size(495, 360);
            this.tcInventories.TabIndex = 1;
            this.tcInventories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // tpCurrent
            // 
            this.tpCurrent.Controls.Add(this.lvCurrent);
            this.tpCurrent.Location = new System.Drawing.Point(4, 22);
            this.tpCurrent.Name = "tpCurrent";
            this.tpCurrent.Padding = new System.Windows.Forms.Padding(3);
            this.tpCurrent.Size = new System.Drawing.Size(487, 334);
            this.tpCurrent.TabIndex = 0;
            this.tpCurrent.Text = "Current";
            this.tpCurrent.UseVisualStyleBackColor = true;
            // 
            // lvCurrent
            // 
            this.lvCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCurrent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chProperties});
            this.lvCurrent.ContextMenuStrip = this.cmEdit;
            this.lvCurrent.FullRowSelect = true;
            this.lvCurrent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCurrent.Location = new System.Drawing.Point(0, 0);
            this.lvCurrent.MultiSelect = false;
            this.lvCurrent.Name = "lvCurrent";
            this.lvCurrent.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvCurrent.ShowItemToolTips = true;
            this.lvCurrent.Size = new System.Drawing.Size(487, 334);
            this.lvCurrent.TabIndex = 0;
            this.lvCurrent.UseCompatibleStateImageBehavior = false;
            this.lvCurrent.View = System.Windows.Forms.View.Details;
            this.lvCurrent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItem
            // 
            this.chItem.Text = "Item";
            this.chItem.Width = 206;
            // 
            // chProperties
            // 
            this.chProperties.Text = "Properties";
            this.chProperties.Width = 217;
            // 
            // cmEdit
            // 
            this.cmEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxEdit,
            this.miCtxItemDisplayFormat,
            this.miCtxCopyAll});
            this.cmEdit.Name = "cmEdit";
            this.cmEdit.ShowImageMargin = false;
            this.cmEdit.Size = new System.Drawing.Size(160, 70);
            this.cmEdit.Opening += new System.ComponentModel.CancelEventHandler(this.cmEdit_Opening);
            // 
            // miCtxEdit
            // 
            this.miCtxEdit.Name = "miCtxEdit";
            this.miCtxEdit.Size = new System.Drawing.Size(159, 22);
            this.miCtxEdit.Text = "&Edit";
            this.miCtxEdit.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // miCtxItemDisplayFormat
            // 
            this.miCtxItemDisplayFormat.Name = "miCtxItemDisplayFormat";
            this.miCtxItemDisplayFormat.Size = new System.Drawing.Size(159, 22);
            this.miCtxItemDisplayFormat.Text = "&Item display format";
            this.miCtxItemDisplayFormat.Click += new System.EventHandler(this.miCtxItemDisplayFormat_Click);
            // 
            // miCtxCopyAll
            // 
            this.miCtxCopyAll.Name = "miCtxCopyAll";
            this.miCtxCopyAll.Size = new System.Drawing.Size(159, 22);
            this.miCtxCopyAll.Text = "&Copy all to clipboard";
            this.miCtxCopyAll.Click += new System.EventHandler(this.miCtxCopyAll_Click);
            // 
            // tpTown1
            // 
            this.tpTown1.Controls.Add(this.lvTown1);
            this.tpTown1.Location = new System.Drawing.Point(4, 22);
            this.tpTown1.Name = "tpTown1";
            this.tpTown1.Padding = new System.Windows.Forms.Padding(3);
            this.tpTown1.Size = new System.Drawing.Size(487, 334);
            this.tpTown1.TabIndex = 1;
            this.tpTown1.Text = "Town 1";
            this.tpTown1.UseVisualStyleBackColor = true;
            // 
            // lvTown1
            // 
            this.lvTown1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem1,
            this.chProperties1});
            this.lvTown1.ContextMenuStrip = this.cmEdit;
            this.lvTown1.FullRowSelect = true;
            this.lvTown1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown1.Location = new System.Drawing.Point(0, 0);
            this.lvTown1.MultiSelect = false;
            this.lvTown1.Name = "lvTown1";
            this.lvTown1.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown1.ShowItemToolTips = true;
            this.lvTown1.Size = new System.Drawing.Size(487, 334);
            this.lvTown1.TabIndex = 1;
            this.lvTown1.UseCompatibleStateImageBehavior = false;
            this.lvTown1.View = System.Windows.Forms.View.Details;
            this.lvTown1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItem1
            // 
            this.chItem1.Text = "Item";
            this.chItem1.Width = 206;
            // 
            // chProperties1
            // 
            this.chProperties1.Text = "Properties";
            this.chProperties1.Width = 217;
            // 
            // tpTown2
            // 
            this.tpTown2.Controls.Add(this.lvTown2);
            this.tpTown2.Location = new System.Drawing.Point(4, 22);
            this.tpTown2.Name = "tpTown2";
            this.tpTown2.Size = new System.Drawing.Size(487, 334);
            this.tpTown2.TabIndex = 2;
            this.tpTown2.Text = "Town 2";
            this.tpTown2.UseVisualStyleBackColor = true;
            // 
            // lvTown2
            // 
            this.lvTown2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem2,
            this.chProperties2});
            this.lvTown2.ContextMenuStrip = this.cmEdit;
            this.lvTown2.FullRowSelect = true;
            this.lvTown2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown2.Location = new System.Drawing.Point(0, 0);
            this.lvTown2.MultiSelect = false;
            this.lvTown2.Name = "lvTown2";
            this.lvTown2.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown2.ShowItemToolTips = true;
            this.lvTown2.Size = new System.Drawing.Size(487, 334);
            this.lvTown2.TabIndex = 1;
            this.lvTown2.UseCompatibleStateImageBehavior = false;
            this.lvTown2.View = System.Windows.Forms.View.Details;
            this.lvTown2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItem2
            // 
            this.chItem2.Text = "Item";
            this.chItem2.Width = 206;
            // 
            // chProperties2
            // 
            this.chProperties2.Text = "Properties";
            this.chProperties2.Width = 217;
            // 
            // tpTown3
            // 
            this.tpTown3.Controls.Add(this.lvTown3);
            this.tpTown3.Location = new System.Drawing.Point(4, 22);
            this.tpTown3.Name = "tpTown3";
            this.tpTown3.Size = new System.Drawing.Size(487, 334);
            this.tpTown3.TabIndex = 3;
            this.tpTown3.Text = "Town 3";
            this.tpTown3.UseVisualStyleBackColor = true;
            // 
            // lvTown3
            // 
            this.lvTown3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem3,
            this.chProperties3});
            this.lvTown3.ContextMenuStrip = this.cmEdit;
            this.lvTown3.FullRowSelect = true;
            this.lvTown3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown3.Location = new System.Drawing.Point(0, 0);
            this.lvTown3.MultiSelect = false;
            this.lvTown3.Name = "lvTown3";
            this.lvTown3.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown3.ShowItemToolTips = true;
            this.lvTown3.Size = new System.Drawing.Size(487, 334);
            this.lvTown3.TabIndex = 1;
            this.lvTown3.UseCompatibleStateImageBehavior = false;
            this.lvTown3.View = System.Windows.Forms.View.Details;
            this.lvTown3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItem3
            // 
            this.chItem3.Text = "Item";
            this.chItem3.Width = 206;
            // 
            // chProperties3
            // 
            this.chProperties3.Text = "Properties";
            this.chProperties3.Width = 217;
            // 
            // tpTown4
            // 
            this.tpTown4.Controls.Add(this.lvTown4);
            this.tpTown4.Location = new System.Drawing.Point(4, 22);
            this.tpTown4.Name = "tpTown4";
            this.tpTown4.Size = new System.Drawing.Size(487, 334);
            this.tpTown4.TabIndex = 4;
            this.tpTown4.Text = "Town 4";
            this.tpTown4.UseVisualStyleBackColor = true;
            // 
            // lvTown4
            // 
            this.lvTown4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItems4,
            this.chProperties4});
            this.lvTown4.ContextMenuStrip = this.cmEdit;
            this.lvTown4.FullRowSelect = true;
            this.lvTown4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown4.Location = new System.Drawing.Point(0, 0);
            this.lvTown4.MultiSelect = false;
            this.lvTown4.Name = "lvTown4";
            this.lvTown4.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown4.ShowItemToolTips = true;
            this.lvTown4.Size = new System.Drawing.Size(487, 334);
            this.lvTown4.TabIndex = 1;
            this.lvTown4.UseCompatibleStateImageBehavior = false;
            this.lvTown4.View = System.Windows.Forms.View.Details;
            this.lvTown4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItems4
            // 
            this.chItems4.Text = "Item";
            this.chItems4.Width = 206;
            // 
            // chProperties4
            // 
            this.chProperties4.Text = "Properties";
            this.chProperties4.Width = 217;
            // 
            // tpTown5
            // 
            this.tpTown5.Controls.Add(this.lvTown5);
            this.tpTown5.Location = new System.Drawing.Point(4, 22);
            this.tpTown5.Name = "tpTown5";
            this.tpTown5.Size = new System.Drawing.Size(487, 334);
            this.tpTown5.TabIndex = 5;
            this.tpTown5.Text = "Town 5";
            this.tpTown5.UseVisualStyleBackColor = true;
            // 
            // lvTown5
            // 
            this.lvTown5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItems5,
            this.chProperties5});
            this.lvTown5.ContextMenuStrip = this.cmEdit;
            this.lvTown5.FullRowSelect = true;
            this.lvTown5.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown5.Location = new System.Drawing.Point(0, 0);
            this.lvTown5.MultiSelect = false;
            this.lvTown5.Name = "lvTown5";
            this.lvTown5.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown5.ShowItemToolTips = true;
            this.lvTown5.Size = new System.Drawing.Size(487, 334);
            this.lvTown5.TabIndex = 1;
            this.lvTown5.UseCompatibleStateImageBehavior = false;
            this.lvTown5.View = System.Windows.Forms.View.Details;
            this.lvTown5.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // chItems5
            // 
            this.chItems5.Text = "Item";
            this.chItems5.Width = 206;
            // 
            // chProperties5
            // 
            this.chProperties5.Text = "Properties";
            this.chProperties5.Width = 217;
            // 
            // tpTown6
            // 
            this.tpTown6.Controls.Add(this.lvTown6);
            this.tpTown6.Location = new System.Drawing.Point(4, 22);
            this.tpTown6.Name = "tpTown6";
            this.tpTown6.Padding = new System.Windows.Forms.Padding(3);
            this.tpTown6.Size = new System.Drawing.Size(487, 334);
            this.tpTown6.TabIndex = 6;
            this.tpTown6.Text = "Town 6";
            this.tpTown6.UseVisualStyleBackColor = true;
            // 
            // lvTown6
            // 
            this.lvTown6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown6.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvTown6.ContextMenuStrip = this.cmEdit;
            this.lvTown6.FullRowSelect = true;
            this.lvTown6.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown6.Location = new System.Drawing.Point(0, 0);
            this.lvTown6.MultiSelect = false;
            this.lvTown6.Name = "lvTown6";
            this.lvTown6.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown6.ShowItemToolTips = true;
            this.lvTown6.Size = new System.Drawing.Size(487, 334);
            this.lvTown6.TabIndex = 2;
            this.lvTown6.UseCompatibleStateImageBehavior = false;
            this.lvTown6.View = System.Windows.Forms.View.Details;
            this.lvTown6.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 206;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Properties";
            this.columnHeader2.Width = 217;
            // 
            // tpTown7
            // 
            this.tpTown7.Controls.Add(this.lvTown7);
            this.tpTown7.Location = new System.Drawing.Point(4, 22);
            this.tpTown7.Name = "tpTown7";
            this.tpTown7.Padding = new System.Windows.Forms.Padding(3);
            this.tpTown7.Size = new System.Drawing.Size(487, 334);
            this.tpTown7.TabIndex = 7;
            this.tpTown7.Text = "Town 7";
            this.tpTown7.UseVisualStyleBackColor = true;
            // 
            // lvTown7
            // 
            this.lvTown7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown7.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvTown7.ContextMenuStrip = this.cmEdit;
            this.lvTown7.FullRowSelect = true;
            this.lvTown7.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown7.Location = new System.Drawing.Point(0, 0);
            this.lvTown7.MultiSelect = false;
            this.lvTown7.Name = "lvTown7";
            this.lvTown7.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown7.ShowItemToolTips = true;
            this.lvTown7.Size = new System.Drawing.Size(487, 334);
            this.lvTown7.TabIndex = 2;
            this.lvTown7.UseCompatibleStateImageBehavior = false;
            this.lvTown7.View = System.Windows.Forms.View.Details;
            this.lvTown7.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 206;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Properties";
            this.columnHeader4.Width = 217;
            // 
            // tpTown8
            // 
            this.tpTown8.Controls.Add(this.lvTown8);
            this.tpTown8.Location = new System.Drawing.Point(4, 22);
            this.tpTown8.Name = "tpTown8";
            this.tpTown8.Padding = new System.Windows.Forms.Padding(3);
            this.tpTown8.Size = new System.Drawing.Size(487, 334);
            this.tpTown8.TabIndex = 8;
            this.tpTown8.Text = "Town 8";
            this.tpTown8.UseVisualStyleBackColor = true;
            // 
            // lvTown8
            // 
            this.lvTown8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTown8.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvTown8.ContextMenuStrip = this.cmEdit;
            this.lvTown8.FullRowSelect = true;
            this.lvTown8.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTown8.Location = new System.Drawing.Point(0, 0);
            this.lvTown8.MultiSelect = false;
            this.lvTown8.Name = "lvTown8";
            this.lvTown8.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTown8.ShowItemToolTips = true;
            this.lvTown8.Size = new System.Drawing.Size(487, 334);
            this.lvTown8.TabIndex = 2;
            this.lvTown8.UseCompatibleStateImageBehavior = false;
            this.lvTown8.View = System.Windows.Forms.View.Details;
            this.lvTown8.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAny_MouseDoubleClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item";
            this.columnHeader5.Width = 206;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Properties";
            this.columnHeader6.Width = 217;
            // 
            // ShopInventoryForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(495, 365);
            this.Controls.Add(this.tcInventories);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(229, 221);
            this.Name = "ShopInventoryForm";
            this.ShowIcon = false;
            this.Text = "Shop Inventories";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShopInventoryForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShopInventoryForm_KeyDown);
            this.tcInventories.ResumeLayout(false);
            this.tpCurrent.ResumeLayout(false);
            this.cmEdit.ResumeLayout(false);
            this.tpTown1.ResumeLayout(false);
            this.tpTown2.ResumeLayout(false);
            this.tpTown3.ResumeLayout(false);
            this.tpTown4.ResumeLayout(false);
            this.tpTown5.ResumeLayout(false);
            this.tpTown6.ResumeLayout(false);
            this.tpTown7.ResumeLayout(false);
            this.tpTown8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WhereAreWe.DBListView lvCurrent;
        private System.Windows.Forms.TabControl tcInventories;
        private System.Windows.Forms.TabPage tpCurrent;
        private System.Windows.Forms.TabPage tpTown1;
        private System.Windows.Forms.TabPage tpTown2;
        private System.Windows.Forms.TabPage tpTown3;
        private System.Windows.Forms.TabPage tpTown4;
        private System.Windows.Forms.TabPage tpTown5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.Timer timerRefreshInfo;
        private System.Windows.Forms.ColumnHeader chProperties;
        private WhereAreWe.DBListView lvTown1;
        private System.Windows.Forms.ColumnHeader chItem1;
        private System.Windows.Forms.ColumnHeader chProperties1;
        private WhereAreWe.DBListView lvTown2;
        private System.Windows.Forms.ColumnHeader chItem2;
        private System.Windows.Forms.ColumnHeader chProperties2;
        private WhereAreWe.DBListView lvTown3;
        private System.Windows.Forms.ColumnHeader chItem3;
        private System.Windows.Forms.ColumnHeader chProperties3;
        private WhereAreWe.DBListView lvTown4;
        private System.Windows.Forms.ColumnHeader chItems4;
        private System.Windows.Forms.ColumnHeader chProperties4;
        private WhereAreWe.DBListView lvTown5;
        private System.Windows.Forms.ColumnHeader chItems5;
        private System.Windows.Forms.ColumnHeader chProperties5;
        private System.Windows.Forms.ContextMenuStrip cmEdit;
        private System.Windows.Forms.ToolStripMenuItem miCtxEdit;
        private System.Windows.Forms.TabPage tpTown6;
        private System.Windows.Forms.TabPage tpTown7;
        private System.Windows.Forms.TabPage tpTown8;
        private WhereAreWe.DBListView lvTown6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private WhereAreWe.DBListView lvTown7;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private WhereAreWe.DBListView lvTown8;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripMenuItem miCtxItemDisplayFormat;
        private System.Windows.Forms.ToolStripMenuItem miCtxCopyAll;
    }
}