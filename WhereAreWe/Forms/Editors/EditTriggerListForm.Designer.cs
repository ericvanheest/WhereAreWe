namespace WhereAreWe
{
    partial class EditTriggerListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTriggerListForm));
            this.lvTriggers = new WhereAreWe.DraggableListView();
            this.chEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrigger = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmTriggers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCtxEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCtxAddExample = new System.Windows.Forms.ToolStripMenuItem();
            this.exampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.miCtxDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.llAdd = new System.Windows.Forms.LinkLabel();
            this.llRemove = new System.Windows.Forms.LinkLabel();
            this.llEdit = new System.Windows.Forms.LinkLabel();
            this.cbDisableAllTriggers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llImport = new System.Windows.Forms.LinkLabel();
            this.llExport = new System.Windows.Forms.LinkLabel();
            this.cmTriggers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvTriggers
            // 
            this.lvTriggers.AllowDrop = true;
            this.lvTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTriggers.CheckBoxes = true;
            this.lvTriggers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEntity,
            this.chTrigger});
            this.lvTriggers.ContextMenuStrip = this.cmTriggers;
            this.lvTriggers.FullRowSelect = true;
            this.lvTriggers.Location = new System.Drawing.Point(12, 45);
            this.lvTriggers.Name = "lvTriggers";
            this.lvTriggers.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvTriggers.Size = new System.Drawing.Size(641, 297);
            this.lvTriggers.TabIndex = 0;
            this.lvTriggers.TipDelay = 700;
            this.lvTriggers.TipDuration = 30000;
            this.lvTriggers.UseCompatibleStateImageBehavior = false;
            this.lvTriggers.View = System.Windows.Forms.View.Details;
            this.lvTriggers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTriggers_ItemChecked);
            this.lvTriggers.SelectedIndexChanged += new System.EventHandler(this.lvTriggers_SelectedIndexChanged);
            this.lvTriggers.DoubleClick += new System.EventHandler(this.lvTriggers_DoubleClick);
            // 
            // chEntity
            // 
            this.chEntity.Text = "Item (check to enable)";
            this.chEntity.Width = 157;
            // 
            // chTrigger
            // 
            this.chTrigger.Text = "Trigger";
            this.chTrigger.Width = 462;
            // 
            // cmTriggers
            // 
            this.cmTriggers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCtxEdit,
            this.miCtxAdd,
            this.cmCtxAddExample,
            this.miCtxRemove,
            this.miCtxDuplicate});
            this.cmTriggers.Name = "cmTriggers";
            this.cmTriggers.Size = new System.Drawing.Size(167, 114);
            this.cmTriggers.Opening += new System.ComponentModel.CancelEventHandler(this.cmTriggers_Opening);
            // 
            // miCtxEdit
            // 
            this.miCtxEdit.Name = "miCtxEdit";
            this.miCtxEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.miCtxEdit.Size = new System.Drawing.Size(166, 22);
            this.miCtxEdit.Text = "&Edit";
            this.miCtxEdit.Click += new System.EventHandler(this.miCtxEdit_Click);
            // 
            // miCtxAdd
            // 
            this.miCtxAdd.Name = "miCtxAdd";
            this.miCtxAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miCtxAdd.Size = new System.Drawing.Size(166, 22);
            this.miCtxAdd.Text = "&Add new";
            this.miCtxAdd.Click += new System.EventHandler(this.miCtxAdd_Click);
            // 
            // cmCtxAddExample
            // 
            this.cmCtxAddExample.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exampleToolStripMenuItem});
            this.cmCtxAddExample.Name = "cmCtxAddExample";
            this.cmCtxAddExample.Size = new System.Drawing.Size(166, 22);
            this.cmCtxAddExample.Text = "Add e&xample";
            // 
            // exampleToolStripMenuItem
            // 
            this.exampleToolStripMenuItem.Name = "exampleToolStripMenuItem";
            this.exampleToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exampleToolStripMenuItem.Text = "Example";
            // 
            // miCtxRemove
            // 
            this.miCtxRemove.Name = "miCtxRemove";
            this.miCtxRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miCtxRemove.Size = new System.Drawing.Size(166, 22);
            this.miCtxRemove.Text = "&Remove";
            this.miCtxRemove.Click += new System.EventHandler(this.miCtxRemove_Click);
            // 
            // miCtxDuplicate
            // 
            this.miCtxDuplicate.Name = "miCtxDuplicate";
            this.miCtxDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.miCtxDuplicate.Size = new System.Drawing.Size(166, 22);
            this.miCtxDuplicate.Text = "&Duplicate";
            this.miCtxDuplicate.Click += new System.EventHandler(this.miCtxDuplicate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(578, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(497, 365);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // llAdd
            // 
            this.llAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llAdd.AutoSize = true;
            this.llAdd.Location = new System.Drawing.Point(12, 351);
            this.llAdd.Name = "llAdd";
            this.llAdd.Size = new System.Drawing.Size(26, 13);
            this.llAdd.TabIndex = 1;
            this.llAdd.TabStop = true;
            this.llAdd.Text = "&Add";
            this.llAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAdd_LinkClicked);
            // 
            // llRemove
            // 
            this.llRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llRemove.AutoSize = true;
            this.llRemove.Enabled = false;
            this.llRemove.Location = new System.Drawing.Point(61, 351);
            this.llRemove.Name = "llRemove";
            this.llRemove.Size = new System.Drawing.Size(47, 13);
            this.llRemove.TabIndex = 2;
            this.llRemove.TabStop = true;
            this.llRemove.Text = "&Remove";
            this.llRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRemove_LinkClicked);
            // 
            // llEdit
            // 
            this.llEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llEdit.AutoSize = true;
            this.llEdit.Enabled = false;
            this.llEdit.Location = new System.Drawing.Point(136, 351);
            this.llEdit.Name = "llEdit";
            this.llEdit.Size = new System.Drawing.Size(25, 13);
            this.llEdit.TabIndex = 3;
            this.llEdit.TabStop = true;
            this.llEdit.Text = "&Edit";
            this.llEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llEdit_LinkClicked);
            // 
            // cbDisableAllTriggers
            // 
            this.cbDisableAllTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDisableAllTriggers.AutoSize = true;
            this.cbDisableAllTriggers.Location = new System.Drawing.Point(210, 350);
            this.cbDisableAllTriggers.Name = "cbDisableAllTriggers";
            this.cbDisableAllTriggers.Size = new System.Drawing.Size(111, 17);
            this.cbDisableAllTriggers.TabIndex = 4;
            this.cbDisableAllTriggers.Text = "&Disable all triggers";
            this.cbDisableAllTriggers.UseVisualStyleBackColor = true;
            this.cbDisableAllTriggers.CheckedChanged += new System.EventHandler(this.cbDisableAllTriggers_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(644, 33);
            this.label1.TabIndex = 7;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // llImport
            // 
            this.llImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llImport.AutoSize = true;
            this.llImport.Location = new System.Drawing.Point(12, 375);
            this.llImport.Name = "llImport";
            this.llImport.Size = new System.Drawing.Size(36, 13);
            this.llImport.TabIndex = 1;
            this.llImport.TabStop = true;
            this.llImport.Text = "&Import";
            this.llImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llImport_LinkClicked);
            // 
            // llExport
            // 
            this.llExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llExport.AutoSize = true;
            this.llExport.Location = new System.Drawing.Point(61, 375);
            this.llExport.Name = "llExport";
            this.llExport.Size = new System.Drawing.Size(51, 13);
            this.llExport.TabIndex = 2;
            this.llExport.TabStop = true;
            this.llExport.Text = "E&xport All";
            this.llExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llExport_LinkClicked);
            // 
            // EditTriggerListForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(665, 400);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDisableAllTriggers);
            this.Controls.Add(this.llEdit);
            this.Controls.Add(this.llExport);
            this.Controls.Add(this.llImport);
            this.Controls.Add(this.llRemove);
            this.Controls.Add(this.llAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvTriggers);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(588, 171);
            this.Name = "EditTriggerListForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Party Triggers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditTriggerListForm_FormClosing);
            this.Load += new System.EventHandler(this.EditTriggerListForm_Load);
            this.cmTriggers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraggableListView lvTriggers;
        private System.Windows.Forms.ColumnHeader chEntity;
        private System.Windows.Forms.ColumnHeader chTrigger;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip cmTriggers;
        private System.Windows.Forms.ToolStripMenuItem miCtxAdd;
        private System.Windows.Forms.ToolStripMenuItem cmCtxAddExample;
        private System.Windows.Forms.ToolStripMenuItem exampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCtxRemove;
        private System.Windows.Forms.ToolStripMenuItem miCtxDuplicate;
        private System.Windows.Forms.LinkLabel llAdd;
        private System.Windows.Forms.LinkLabel llRemove;
        private System.Windows.Forms.ToolStripMenuItem miCtxEdit;
        private System.Windows.Forms.LinkLabel llEdit;
        private System.Windows.Forms.CheckBox cbDisableAllTriggers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llImport;
        private System.Windows.Forms.LinkLabel llExport;
    }
}