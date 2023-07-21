namespace WhereAreWe
{
    partial class NoteTemplatesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteTemplatesForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.lvNotes = new WhereAreWe.DBListView();
            this.chNoteSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNoteText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmNotes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNotesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotesResetToDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNoteText = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnInsertVariable = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.comboVariables = new System.Windows.Forms.ComboBox();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbSymbol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(445, 388);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvNotes
            // 
            this.lvNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNoteSymbol,
            this.chNoteText});
            this.lvNotes.ContextMenuStrip = this.cmNotes;
            this.lvNotes.FullRowSelect = true;
            this.lvNotes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvNotes.HideSelection = false;
            this.lvNotes.Location = new System.Drawing.Point(0, 0);
            this.lvNotes.Name = "lvNotes";
            this.lvNotes.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvNotes.Size = new System.Drawing.Size(595, 235);
            this.lvNotes.TabIndex = 0;
            this.lvNotes.UseCompatibleStateImageBehavior = false;
            this.lvNotes.View = System.Windows.Forms.View.Details;
            this.lvNotes.SelectedIndexChanged += new System.EventHandler(this.lvNotes_SelectedIndexChanged);
            // 
            // chNoteSymbol
            // 
            this.chNoteSymbol.Text = "Symbol";
            this.chNoteSymbol.Width = 54;
            // 
            // chNoteText
            // 
            this.chNoteText.Text = "Note Text";
            this.chNoteText.Width = 511;
            // 
            // cmNotes
            // 
            this.cmNotes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNotesAdd,
            this.miNotesDelete,
            this.miNotesResetToDefault});
            this.cmNotes.Name = "cmNotes";
            this.cmNotes.ShowImageMargin = false;
            this.cmNotes.Size = new System.Drawing.Size(128, 70);
            this.cmNotes.Opening += new System.ComponentModel.CancelEventHandler(this.cmNotes_Opening);
            // 
            // miNotesAdd
            // 
            this.miNotesAdd.Name = "miNotesAdd";
            this.miNotesAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.miNotesAdd.Size = new System.Drawing.Size(127, 22);
            this.miNotesAdd.Text = "&Add new";
            this.miNotesAdd.Click += new System.EventHandler(this.miNotesAdd_Click);
            // 
            // miNotesDelete
            // 
            this.miNotesDelete.Name = "miNotesDelete";
            this.miNotesDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miNotesDelete.Size = new System.Drawing.Size(127, 22);
            this.miNotesDelete.Text = "&Delete";
            this.miNotesDelete.Click += new System.EventHandler(this.miNotesDelete_Click);
            // 
            // miNotesResetToDefault
            // 
            this.miNotesResetToDefault.Name = "miNotesResetToDefault";
            this.miNotesResetToDefault.Size = new System.Drawing.Size(127, 22);
            this.miNotesResetToDefault.Text = "&Reset to default";
            this.miNotesResetToDefault.Click += new System.EventHandler(this.miNotesResetToDefault_Click);
            // 
            // tbNoteText
            // 
            this.tbNoteText.AcceptsReturn = true;
            this.tbNoteText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoteText.HideSelection = false;
            this.tbNoteText.Location = new System.Drawing.Point(0, 0);
            this.tbNoteText.Multiline = true;
            this.tbNoteText.Name = "tbNoteText";
            this.tbNoteText.Size = new System.Drawing.Size(595, 106);
            this.tbNoteText.TabIndex = 0;
            this.tbNoteText.Leave += new System.EventHandler(this.tbNoteText_Leave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 8);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvNotes);
            this.splitContainer1.Panel1.Controls.Add(this.btnInsertVariable);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.comboVariables);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddNote);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbNoteText);
            this.splitContainer1.Size = new System.Drawing.Size(595, 374);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.TabIndex = 9;
            // 
            // btnInsertVariable
            // 
            this.btnInsertVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertVariable.Location = new System.Drawing.Point(513, 238);
            this.btnInsertVariable.Name = "btnInsertVariable";
            this.btnInsertVariable.Size = new System.Drawing.Size(75, 23);
            this.btnInsertVariable.TabIndex = 5;
            this.btnInsertVariable.Text = "&Insert";
            this.btnInsertVariable.UseVisualStyleBackColor = true;
            this.btnInsertVariable.Click += new System.EventHandler(this.btnInsertVariable_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(81, 239);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // comboVariables
            // 
            this.comboVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVariables.FormattingEnabled = true;
            this.comboVariables.Items.AddRange(new object[] {
            "$uniqueEncounterMonsters",
            "$currentSheetTitle"});
            this.comboVariables.Location = new System.Drawing.Point(296, 239);
            this.comboVariables.Name = "comboVariables";
            this.comboVariables.Size = new System.Drawing.Size(211, 21);
            this.comboVariables.TabIndex = 4;
            // 
            // btnAddNote
            // 
            this.btnAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNote.Location = new System.Drawing.Point(0, 239);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(75, 23);
            this.btnAddNote.TabIndex = 1;
            this.btnAddNote.Text = "&Add new";
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Variables:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(526, 388);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbSymbol
            // 
            this.tbSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSymbol.Location = new System.Drawing.Point(53, 389);
            this.tbSymbol.Name = "tbSymbol";
            this.tbSymbol.Size = new System.Drawing.Size(30, 20);
            this.tbSymbol.TabIndex = 1;
            this.tbSymbol.Leave += new System.EventHandler(this.tbSymbol_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Symbol:";
            // 
            // NoteTemplatesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(606, 417);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSymbol);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(545, 256);
            this.Name = "NoteTemplatesForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Note Templates";
            this.Load += new System.EventHandler(this.StringsViewForm_Load);
            this.cmNotes.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private WhereAreWe.DBListView lvNotes;
        private System.Windows.Forms.ContextMenuStrip cmNotes;
        private System.Windows.Forms.ColumnHeader chNoteSymbol;
        private System.Windows.Forms.ColumnHeader chNoteText;
        private System.Windows.Forms.TextBox tbNoteText;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboVariables;
        private System.Windows.Forms.ToolStripMenuItem miNotesAdd;
        private System.Windows.Forms.ToolStripMenuItem miNotesDelete;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Button btnInsertVariable;
        private System.Windows.Forms.ToolStripMenuItem miNotesResetToDefault;
    }
}