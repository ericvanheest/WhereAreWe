namespace WhereAreWe
{
    partial class EditRosterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRosterForm));
            this.ofdRoster = new System.Windows.Forms.OpenFileDialog();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvCharacters = new WhereAreWe.DraggableListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTown = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnResetHirelings = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ofdRoster
            // 
            this.ofdRoster.DefaultExt = "DTA";
            this.ofdRoster.FileName = "ROSTER.DTA";
            this.ofdRoster.Filter = resources.GetString("ofdRoster.Filter");
            this.ofdRoster.Title = "Select a roster file";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(555, 423);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(570, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbFilename
            // 
            this.tbFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilename.Location = new System.Drawing.Point(62, 6);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(502, 20);
            this.tbFilename.TabIndex = 1;
            this.tbFilename.Text = "C:\\GOG Games\\Might & Magic VI Limited Edition\\Might and Magic 1 - Book I\\ROSTER.D" +
    "TA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Roster file";
            // 
            // lvCharacters
            // 
            this.lvCharacters.AllowDrop = true;
            this.lvCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCharacters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPosition,
            this.chTown,
            this.chLevel,
            this.chClass,
            this.chStats,
            this.chHP,
            this.chSP,
            this.chAge});
            this.lvCharacters.FullRowSelect = true;
            this.lvCharacters.Location = new System.Drawing.Point(0, 32);
            this.lvCharacters.Name = "lvCharacters";
            this.lvCharacters.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvCharacters.ShowItemToolTips = true;
            this.lvCharacters.Size = new System.Drawing.Size(632, 385);
            this.lvCharacters.TabIndex = 3;
            this.lvCharacters.UseCompatibleStateImageBehavior = false;
            this.lvCharacters.View = System.Windows.Forms.View.Details;
            this.lvCharacters.ItemsRearranged += new WhereAreWe.DraggableListView.ItemsRearrangedEventHandler(this.lvCharacters_ItemsRearranged);
            this.lvCharacters.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvCharacters_ColumnClick);
            // 
            // chName
            // 
            this.chName.Text = "Character Name";
            this.chName.Width = 100;
            // 
            // chPosition
            // 
            this.chPosition.Text = "Pos";
            this.chPosition.Width = 34;
            // 
            // chTown
            // 
            this.chTown.Text = "Town";
            // 
            // chLevel
            // 
            this.chLevel.Text = "Level";
            // 
            // chClass
            // 
            this.chClass.Text = "Class";
            // 
            // chStats
            // 
            this.chStats.Text = "I/M/P/E/S/A/L";
            // 
            // chHP
            // 
            this.chHP.Text = "HP";
            // 
            // chSP
            // 
            this.chSP.Text = "SP";
            // 
            // chAge
            // 
            this.chAge.Text = "Age";
            // 
            // btnResetHirelings
            // 
            this.btnResetHirelings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetHirelings.Location = new System.Drawing.Point(83, 423);
            this.btnResetHirelings.Name = "btnResetHirelings";
            this.btnResetHirelings.Size = new System.Drawing.Size(88, 23);
            this.btnResetHirelings.TabIndex = 5;
            this.btnResetHirelings.Text = "&Reset Hirelings";
            this.btnResetHirelings.UseVisualStyleBackColor = true;
            this.btnResetHirelings.Click += new System.EventHandler(this.btnResetHirelings_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.Location = new System.Drawing.Point(2, 423);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "&Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(474, 423);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditRosterForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(632, 449);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvCharacters);
            this.Controls.Add(this.btnResetHirelings);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "EditRosterForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Roster Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditRosterForm_FormClosing);
            this.Load += new System.EventHandler(this.EditRosterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraggableListView lvCharacters;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColumnHeader chPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.OpenFileDialog ofdRoster;
        private System.Windows.Forms.ColumnHeader chTown;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chClass;
        private System.Windows.Forms.ColumnHeader chStats;
        private System.Windows.Forms.ColumnHeader chHP;
        private System.Windows.Forms.ColumnHeader chSP;
        private System.Windows.Forms.ColumnHeader chAge;
        private System.Windows.Forms.Button btnResetHirelings;
    }
}