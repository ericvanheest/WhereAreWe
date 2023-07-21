namespace WhereAreWe
{
    partial class EditNotificationForm
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
            this.labelAction = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbAudioFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ofdAudioFile = new System.Windows.Forms.OpenFileDialog();
            this.cbMessage = new System.Windows.Forms.CheckBox();
            this.cbAudioFile = new System.Windows.Forms.CheckBox();
            this.llVariables = new System.Windows.Forms.LinkLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lvVariables = new System.Windows.Forms.ListView();
            this.chVariable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAction.Location = new System.Drawing.Point(86, 8);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(53, 13);
            this.labelAction.TabIndex = 1;
            this.labelAction.Text = "ACTION";
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Enabled = false;
            this.tbMessage.HideSelection = false;
            this.tbMessage.Location = new System.Drawing.Point(89, 28);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(518, 20);
            this.tbMessage.TabIndex = 3;
            // 
            // tbAudioFile
            // 
            this.tbAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAudioFile.Enabled = false;
            this.tbAudioFile.Location = new System.Drawing.Point(89, 53);
            this.tbAudioFile.Name = "tbAudioFile";
            this.tbAudioFile.Size = new System.Drawing.Size(404, 20);
            this.tbAudioFile.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(549, 52);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(58, 22);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(451, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(532, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofdAudioFile
            // 
            this.ofdAudioFile.Filter = "WAV Files|*.wav|All Files|*.*";
            this.ofdAudioFile.Title = "Select an audio file for the notification";
            // 
            // cbMessage
            // 
            this.cbMessage.AutoSize = true;
            this.cbMessage.Location = new System.Drawing.Point(8, 31);
            this.cbMessage.Name = "cbMessage";
            this.cbMessage.Size = new System.Drawing.Size(69, 17);
            this.cbMessage.TabIndex = 2;
            this.cbMessage.Text = "&Message";
            this.cbMessage.UseVisualStyleBackColor = true;
            this.cbMessage.CheckedChanged += new System.EventHandler(this.cbMessage_CheckedChanged);
            // 
            // cbAudioFile
            // 
            this.cbAudioFile.AutoSize = true;
            this.cbAudioFile.Location = new System.Drawing.Point(8, 56);
            this.cbAudioFile.Name = "cbAudioFile";
            this.cbAudioFile.Size = new System.Drawing.Size(72, 17);
            this.cbAudioFile.TabIndex = 4;
            this.cbAudioFile.Text = "&Audio File";
            this.cbAudioFile.UseVisualStyleBackColor = true;
            this.cbAudioFile.CheckedChanged += new System.EventHandler(this.cbAudioFile_CheckedChanged);
            // 
            // llVariables
            // 
            this.llVariables.AutoSize = true;
            this.llVariables.Location = new System.Drawing.Point(5, 87);
            this.llVariables.Name = "llVariables";
            this.llVariables.Size = new System.Drawing.Size(80, 13);
            this.llVariables.TabIndex = 8;
            this.llVariables.TabStop = true;
            this.llVariables.Text = "Show &Variables";
            this.llVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVariables_LinkClicked);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnPlay);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.llVariables);
            this.splitContainer1.Panel1.Controls.Add(this.labelAction);
            this.splitContainer1.Panel1.Controls.Add(this.cbAudioFile);
            this.splitContainer1.Panel1.Controls.Add(this.tbMessage);
            this.splitContainer1.Panel1.Controls.Add(this.cbMessage);
            this.splitContainer1.Panel1.Controls.Add(this.tbAudioFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowse);
            this.splitContainer1.Panel1.Controls.Add(this.btnOK);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvVariables);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(619, 417);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(499, 52);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(44, 22);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "&Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Action:";
            // 
            // lvVariables
            // 
            this.lvVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chVariable,
            this.chDescription});
            this.lvVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVariables.FullRowSelect = true;
            this.lvVariables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvVariables.Location = new System.Drawing.Point(0, 0);
            this.lvVariables.Name = "lvVariables";
            this.lvVariables.Size = new System.Drawing.Size(619, 303);
            this.lvVariables.TabIndex = 0;
            this.lvVariables.UseCompatibleStateImageBehavior = false;
            this.lvVariables.View = System.Windows.Forms.View.Details;
            this.lvVariables.DoubleClick += new System.EventHandler(this.lvVariables_DoubleClick);
            // 
            // chVariable
            // 
            this.chVariable.Text = "Variable (double-click to insert)";
            this.chVariable.Width = 161;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 427;
            // 
            // EditNotificationForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(619, 417);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 152);
            this.Name = "EditNotificationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Notification";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbAudioFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdAudioFile;
        private System.Windows.Forms.CheckBox cbMessage;
        private System.Windows.Forms.CheckBox cbAudioFile;
        private System.Windows.Forms.LinkLabel llVariables;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvVariables;
        private System.Windows.Forms.ColumnHeader chVariable;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPlay;
    }
}