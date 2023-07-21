namespace WhereAreWe
{
    partial class SheetOrganizerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetOrganizerForm));
            this.cmListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.miGotoMap = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboZoom = new System.Windows.Forms.ComboBox();
            this.llSetZoom = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvSheets = new WhereAreWe.DraggableListView();
            this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSheetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chZoom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmListView
            // 
            this.cmListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditPath,
            this.miGotoMap});
            this.cmListView.Name = "cmListView";
            this.cmListView.ShowImageMargin = false;
            this.cmListView.Size = new System.Drawing.Size(119, 48);
            this.cmListView.Opening += new System.ComponentModel.CancelEventHandler(this.cmListView_Opening);
            // 
            // miEditPath
            // 
            this.miEditPath.Name = "miEditPath";
            this.miEditPath.Size = new System.Drawing.Size(118, 22);
            this.miEditPath.Text = "&Edit path";
            this.miEditPath.Click += new System.EventHandler(this.miEditPath_Click);
            // 
            // miGotoMap
            // 
            this.miGotoMap.Name = "miGotoMap";
            this.miGotoMap.Size = new System.Drawing.Size(118, 22);
            this.miGotoMap.Text = "&Go to this map";
            this.miGotoMap.Click += new System.EventHandler(this.miGotoMap_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(386, 410);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(467, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboZoom
            // 
            this.comboZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZoom.FormattingEnabled = true;
            this.comboZoom.Items.AddRange(new object[] {
            "Set minimum to...",
            "Set minimum to 100%",
            "Set minimum to 150%",
            "Set minimum to 200%",
            "Set all to...",
            "Set all to 100%",
            "Set all to 150%",
            "Set all to 200%",
            "Set all to 300%"});
            this.comboZoom.Location = new System.Drawing.Point(1, 412);
            this.comboZoom.Name = "comboZoom";
            this.comboZoom.Size = new System.Drawing.Size(153, 21);
            this.comboZoom.TabIndex = 3;
            // 
            // llSetZoom
            // 
            this.llSetZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llSetZoom.AutoSize = true;
            this.llSetZoom.Location = new System.Drawing.Point(160, 415);
            this.llSetZoom.Name = "llSetZoom";
            this.llSetZoom.Size = new System.Drawing.Size(158, 13);
            this.llSetZoom.TabIndex = 4;
            this.llSetZoom.TabStop = true;
            this.llSetZoom.Text = "Change zoom levels for all maps";
            this.llSetZoom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSetZoom_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(-2, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(553, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Examples:  \\Outside\\Desert, \\Outside\\Ocean, \\Inside\\Towns, etc.";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(-2, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(553, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paths are relative to the \"Maps\" menu item.";
            // 
            // lvSheets
            // 
            this.lvSheets.AllowDrop = true;
            this.lvSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSheets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPath,
            this.chSheetName,
            this.chZoom});
            this.lvSheets.ContextMenuStrip = this.cmListView;
            this.lvSheets.FullRowSelect = true;
            this.lvSheets.HideSelection = false;
            this.lvSheets.LabelEdit = true;
            this.lvSheets.Location = new System.Drawing.Point(1, 0);
            this.lvSheets.Name = "lvSheets";
            this.lvSheets.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvSheets.Size = new System.Drawing.Size(550, 366);
            this.lvSheets.TabIndex = 0;
            this.lvSheets.UseCompatibleStateImageBehavior = false;
            this.lvSheets.View = System.Windows.Forms.View.Details;
            this.lvSheets.ItemsRearranged += new WhereAreWe.DraggableListView.ItemsRearrangedEventHandler(this.lvSheets_ItemsRearranged);
            this.lvSheets.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvSheets_AfterLabelEdit);
            this.lvSheets.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSheets_ColumnClick);
            this.lvSheets.SelectedIndexChanged += new System.EventHandler(this.lvSheets_SelectedIndexChanged);
            this.lvSheets.DoubleClick += new System.EventHandler(this.lvSheets_DoubleClick);
            this.lvSheets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSheets_KeyDown);
            // 
            // chPath
            // 
            this.chPath.Text = "Path";
            this.chPath.Width = 183;
            // 
            // chSheetName
            // 
            this.chSheetName.Text = "Sheet Name";
            this.chSheetName.Width = 281;
            // 
            // chZoom
            // 
            this.chZoom.Text = "Zoom %";
            // 
            // SheetOrganizerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(552, 444);
            this.Controls.Add(this.comboZoom);
            this.Controls.Add(this.llSetZoom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvSheets);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "SheetOrganizerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Organize Map Sheets";
            this.Load += new System.EventHandler(this.SheetOrganizerForm_Load);
            this.cmListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraggableListView lvSheets;
        private System.Windows.Forms.ColumnHeader chPath;
        private System.Windows.Forms.ColumnHeader chSheetName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmListView;
        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.ColumnHeader chZoom;
        private System.Windows.Forms.LinkLabel llSetZoom;
        private System.Windows.Forms.ComboBox comboZoom;
        private System.Windows.Forms.ToolStripMenuItem miGotoMap;
    }
}