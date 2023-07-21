namespace WhereAreWe
{
    partial class WizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardForm));
            this.tcWizard = new System.Windows.Forms.TabControl();
            this.tpGamePaths = new System.Windows.Forms.TabPage();
            this.comboLaunch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbForceWindowed = new System.Windows.Forms.CheckBox();
            this.lvGamePaths = new System.Windows.Forms.ListView();
            this.chGamePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmPaths = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPathsBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.miPathsLaunch = new System.Windows.Forms.ToolStripMenuItem();
            this.miPathsRescan = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tpPlayStyle = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMinimalHelp = new System.Windows.Forms.RadioButton();
            this.rbFullVisiblity = new System.Windows.Forms.RadioButton();
            this.rbFaqStyle = new System.Windows.Forms.RadioButton();
            this.labelFAQ = new System.Windows.Forms.Label();
            this.labelMinimal = new System.Windows.Forms.Label();
            this.labelFullVisibility = new System.Windows.Forms.Label();
            this.cbTrainer = new System.Windows.Forms.CheckBox();
            this.labelTrainer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.ofdBrowseShortcut = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcWizard.SuspendLayout();
            this.tpGamePaths.SuspendLayout();
            this.cmPaths.SuspendLayout();
            this.tpPlayStyle.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcWizard
            // 
            this.tcWizard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcWizard.Controls.Add(this.tpGamePaths);
            this.tcWizard.Controls.Add(this.tpPlayStyle);
            this.tcWizard.Location = new System.Drawing.Point(12, 12);
            this.tcWizard.Name = "tcWizard";
            this.tcWizard.SelectedIndex = 0;
            this.tcWizard.Size = new System.Drawing.Size(644, 286);
            this.tcWizard.TabIndex = 0;
            this.tcWizard.SelectedIndexChanged += new System.EventHandler(this.tcWizard_SelectedIndexChanged);
            // 
            // tpGamePaths
            // 
            this.tpGamePaths.Controls.Add(this.comboLaunch);
            this.tpGamePaths.Controls.Add(this.label3);
            this.tpGamePaths.Controls.Add(this.cbForceWindowed);
            this.tpGamePaths.Controls.Add(this.lvGamePaths);
            this.tpGamePaths.Controls.Add(this.label1);
            this.tpGamePaths.Location = new System.Drawing.Point(4, 22);
            this.tpGamePaths.Name = "tpGamePaths";
            this.tpGamePaths.Padding = new System.Windows.Forms.Padding(3);
            this.tpGamePaths.Size = new System.Drawing.Size(636, 260);
            this.tpGamePaths.TabIndex = 0;
            this.tpGamePaths.Text = "Game Paths";
            this.tpGamePaths.UseVisualStyleBackColor = true;
            // 
            // comboLaunch
            // 
            this.comboLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboLaunch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLaunch.FormattingEnabled = true;
            this.comboLaunch.Location = new System.Drawing.Point(257, 238);
            this.comboLaunch.Name = "comboLaunch";
            this.comboLaunch.Size = new System.Drawing.Size(379, 21);
            this.comboLaunch.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Launch the following game after finishing this wizard:";
            // 
            // cbForceWindowed
            // 
            this.cbForceWindowed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbForceWindowed.AutoSize = true;
            this.cbForceWindowed.Checked = true;
            this.cbForceWindowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbForceWindowed.Location = new System.Drawing.Point(0, 218);
            this.cbForceWindowed.Name = "cbForceWindowed";
            this.cbForceWindowed.Size = new System.Drawing.Size(515, 17);
            this.cbForceWindowed.TabIndex = 2;
            this.cbForceWindowed.Text = "Change the DOSBox settings for the above games to be windowed instead of fullscre" +
    "en (recommended)";
            this.cbForceWindowed.UseVisualStyleBackColor = true;
            // 
            // lvGamePaths
            // 
            this.lvGamePaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGamePaths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGamePath,
            this.chGameName});
            this.lvGamePaths.ContextMenuStrip = this.cmPaths;
            this.lvGamePaths.FullRowSelect = true;
            this.lvGamePaths.Location = new System.Drawing.Point(0, 36);
            this.lvGamePaths.MultiSelect = false;
            this.lvGamePaths.Name = "lvGamePaths";
            this.lvGamePaths.Size = new System.Drawing.Size(640, 177);
            this.lvGamePaths.TabIndex = 1;
            this.lvGamePaths.UseCompatibleStateImageBehavior = false;
            this.lvGamePaths.View = System.Windows.Forms.View.Details;
            this.lvGamePaths.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGamePaths_MouseDoubleClick);
            // 
            // chGamePath
            // 
            this.chGamePath.Text = "Full path to game shortcut";
            this.chGamePath.Width = 468;
            // 
            // chGameName
            // 
            this.chGameName.Text = "Game Name";
            this.chGameName.Width = 146;
            // 
            // cmPaths
            // 
            this.cmPaths.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPathsBrowse,
            this.miPathsLaunch,
            this.miPathsRescan});
            this.cmPaths.Name = "cmPaths";
            this.cmPaths.Size = new System.Drawing.Size(210, 70);
            this.cmPaths.Opening += new System.ComponentModel.CancelEventHandler(this.cmPaths_Opening);
            // 
            // miPathsBrowse
            // 
            this.miPathsBrowse.Name = "miPathsBrowse";
            this.miPathsBrowse.Size = new System.Drawing.Size(209, 22);
            this.miPathsBrowse.Text = "&Browse for shortcut";
            this.miPathsBrowse.Click += new System.EventHandler(this.miPathsBrowse_Click);
            // 
            // miPathsLaunch
            // 
            this.miPathsLaunch.Name = "miPathsLaunch";
            this.miPathsLaunch.Size = new System.Drawing.Size(209, 22);
            this.miPathsLaunch.Text = "&Test shortcut (launch game)";
            this.miPathsLaunch.Click += new System.EventHandler(this.miPathsLaunch_Click);
            // 
            // miPathsRescan
            // 
            this.miPathsRescan.Name = "miPathsRescan";
            this.miPathsRescan.Size = new System.Drawing.Size(209, 22);
            this.miPathsRescan.Text = "&Re-scan for all games";
            this.miPathsRescan.Click += new System.EventHandler(this.miPathsRescan_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(630, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "The following game paths have been determined automatically.  Please double-click" +
    " on any incorrect or unknown item to set it manually (if that game has been inst" +
    "alled).";
            // 
            // tpPlayStyle
            // 
            this.tpPlayStyle.Controls.Add(this.groupBox1);
            this.tpPlayStyle.Controls.Add(this.cbTrainer);
            this.tpPlayStyle.Controls.Add(this.labelTrainer);
            this.tpPlayStyle.Controls.Add(this.label2);
            this.tpPlayStyle.Location = new System.Drawing.Point(4, 22);
            this.tpPlayStyle.Name = "tpPlayStyle";
            this.tpPlayStyle.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayStyle.Size = new System.Drawing.Size(636, 260);
            this.tpPlayStyle.TabIndex = 1;
            this.tpPlayStyle.Text = "Play Style";
            this.tpPlayStyle.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbMinimalHelp);
            this.groupBox1.Controls.Add(this.rbFullVisiblity);
            this.groupBox1.Controls.Add(this.rbFaqStyle);
            this.groupBox1.Controls.Add(this.labelFAQ);
            this.groupBox1.Controls.Add(this.labelMinimal);
            this.groupBox1.Controls.Add(this.labelFullVisibility);
            this.groupBox1.Location = new System.Drawing.Point(9, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 163);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maps and Monsters";
            // 
            // rbMinimalHelp
            // 
            this.rbMinimalHelp.AutoSize = true;
            this.rbMinimalHelp.Checked = true;
            this.rbMinimalHelp.Location = new System.Drawing.Point(16, 19);
            this.rbMinimalHelp.Name = "rbMinimalHelp";
            this.rbMinimalHelp.Size = new System.Drawing.Size(60, 17);
            this.rbMinimalHelp.TabIndex = 0;
            this.rbMinimalHelp.TabStop = true;
            this.rbMinimalHelp.Text = "&Minimal";
            this.rbMinimalHelp.UseVisualStyleBackColor = true;
            // 
            // rbFullVisiblity
            // 
            this.rbFullVisiblity.AutoSize = true;
            this.rbFullVisiblity.Location = new System.Drawing.Point(16, 113);
            this.rbFullVisiblity.Name = "rbFullVisiblity";
            this.rbFullVisiblity.Size = new System.Drawing.Size(80, 17);
            this.rbFullVisiblity.TabIndex = 4;
            this.rbFullVisiblity.Text = "&Full Visibility";
            this.rbFullVisiblity.UseVisualStyleBackColor = true;
            // 
            // rbFaqStyle
            // 
            this.rbFaqStyle.AutoSize = true;
            this.rbFaqStyle.Location = new System.Drawing.Point(16, 66);
            this.rbFaqStyle.Name = "rbFaqStyle";
            this.rbFaqStyle.Size = new System.Drawing.Size(72, 17);
            this.rbFaqStyle.TabIndex = 2;
            this.rbFaqStyle.Text = "FA&Q-Style";
            this.rbFaqStyle.UseVisualStyleBackColor = true;
            // 
            // labelFAQ
            // 
            this.labelFAQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFAQ.Location = new System.Drawing.Point(113, 68);
            this.labelFAQ.Name = "labelFAQ";
            this.labelFAQ.Size = new System.Drawing.Size(508, 42);
            this.labelFAQ.TabIndex = 3;
            this.labelFAQ.Text = resources.GetString("labelFAQ.Text");
            this.labelFAQ.Click += new System.EventHandler(this.labelFAQ_Click);
            // 
            // labelMinimal
            // 
            this.labelMinimal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinimal.Location = new System.Drawing.Point(113, 21);
            this.labelMinimal.Name = "labelMinimal";
            this.labelMinimal.Size = new System.Drawing.Size(508, 42);
            this.labelMinimal.TabIndex = 1;
            this.labelMinimal.Text = resources.GetString("labelMinimal.Text");
            this.labelMinimal.Click += new System.EventHandler(this.labelMinimal_Click);
            // 
            // labelFullVisibility
            // 
            this.labelFullVisibility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFullVisibility.Location = new System.Drawing.Point(113, 115);
            this.labelFullVisibility.Name = "labelFullVisibility";
            this.labelFullVisibility.Size = new System.Drawing.Size(508, 42);
            this.labelFullVisibility.TabIndex = 5;
            this.labelFullVisibility.Text = "All information about the current map and monsters will be shown regardless of de" +
    "tection range or whether the area has been visited.";
            this.labelFullVisibility.Click += new System.EventHandler(this.labelFullVisibility_Click);
            // 
            // cbTrainer
            // 
            this.cbTrainer.AutoSize = true;
            this.cbTrainer.Location = new System.Drawing.Point(10, 211);
            this.cbTrainer.Name = "cbTrainer";
            this.cbTrainer.Size = new System.Drawing.Size(59, 17);
            this.cbTrainer.TabIndex = 3;
            this.cbTrainer.Text = "&Trainer";
            this.cbTrainer.UseVisualStyleBackColor = true;
            // 
            // labelTrainer
            // 
            this.labelTrainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrainer.Location = new System.Drawing.Point(73, 213);
            this.labelTrainer.Name = "labelTrainer";
            this.labelTrainer.Size = new System.Drawing.Size(557, 42);
            this.labelTrainer.TabIndex = 0;
            this.labelTrainer.Text = resources.GetString("labelTrainer.Text");
            this.labelTrainer.Click += new System.EventHandler(this.labelTrainer_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(6, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(630, 42);
            this.label2.TabIndex = 0;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(581, 304);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "&Next >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(500, 304);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "<< &Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ofdBrowseShortcut
            // 
            this.ofdBrowseShortcut.DefaultExt = "LNK";
            this.ofdBrowseShortcut.Filter = "Shortcut Files|*.lnk|Executable Files|*.exe|All Files|*.*";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WizardForm
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(668, 335);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.tcWizard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(606, 361);
            this.Name = "WizardForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Welcome to \"Where Are We\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WizardForm_FormClosing);
            this.Load += new System.EventHandler(this.WizardForm_Load);
            this.tcWizard.ResumeLayout(false);
            this.tpGamePaths.ResumeLayout(false);
            this.tpGamePaths.PerformLayout();
            this.cmPaths.ResumeLayout(false);
            this.tpPlayStyle.ResumeLayout(false);
            this.tpPlayStyle.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcWizard;
        private System.Windows.Forms.TabPage tpGamePaths;
        private System.Windows.Forms.TabPage tpPlayStyle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ListView lvGamePaths;
        private System.Windows.Forms.ColumnHeader chGamePath;
        private System.Windows.Forms.ColumnHeader chGameName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmPaths;
        private System.Windows.Forms.ToolStripMenuItem miPathsBrowse;
        private System.Windows.Forms.OpenFileDialog ofdBrowseShortcut;
        private System.Windows.Forms.ToolStripMenuItem miPathsLaunch;
        private System.Windows.Forms.ToolStripMenuItem miPathsRescan;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbMinimalHelp;
        private System.Windows.Forms.Label labelTrainer;
        private System.Windows.Forms.Label labelFullVisibility;
        private System.Windows.Forms.Label labelMinimal;
        private System.Windows.Forms.RadioButton rbFullVisiblity;
        private System.Windows.Forms.Label labelFAQ;
        private System.Windows.Forms.RadioButton rbFaqStyle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbTrainer;
        private System.Windows.Forms.CheckBox cbForceWindowed;
        private System.Windows.Forms.ComboBox comboLaunch;
        private System.Windows.Forms.Label label3;
    }
}