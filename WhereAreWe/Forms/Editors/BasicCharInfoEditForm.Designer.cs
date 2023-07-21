namespace WhereAreWe
{
    partial class BasicCharInfoEditForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelCharName = new System.Windows.Forms.Label();
            this.comboSex = new System.Windows.Forms.ComboBox();
            this.nudLevelTemp = new System.Windows.Forms.NumericUpDown();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelSex = new System.Windows.Forms.Label();
            this.labelAlignment = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.comboAlignTemp = new System.Windows.Forms.ComboBox();
            this.comboRace = new System.Windows.Forms.ComboBox();
            this.comboClass = new System.Windows.Forms.ComboBox();
            this.labelLevelCurrent = new System.Windows.Forms.Label();
            this.labelLevelPerm = new System.Windows.Forms.Label();
            this.comboAlignPermanent = new System.Windows.Forms.ComboBox();
            this.nudLevelPermanent = new System.Windows.Forms.NumericUpDown();
            this.labelAlignCurrent = new System.Windows.Forms.Label();
            this.labelAlignPerm = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelBirthYear = new System.Windows.Forms.Label();
            this.nudAgeYears = new System.Windows.Forms.NumericUpDown();
            this.labelBirthDay = new System.Windows.Forms.Label();
            this.nudAgeDays = new System.Windows.Forms.NumericUpDown();
            this.labelAgeModifier = new System.Windows.Forms.Label();
            this.nudAgeModifier = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelPermanent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeModifier)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(227, 185);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelCharName
            // 
            this.labelCharName.AutoSize = true;
            this.labelCharName.Location = new System.Drawing.Point(12, 10);
            this.labelCharName.Name = "labelCharName";
            this.labelCharName.Size = new System.Drawing.Size(35, 13);
            this.labelCharName.TabIndex = 0;
            this.labelCharName.Text = "Name";
            // 
            // comboSex
            // 
            this.comboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSex.FormattingEnabled = true;
            this.comboSex.Items.AddRange(new object[] {
            "None",
            "Male",
            "Female"});
            this.comboSex.Location = new System.Drawing.Point(76, 77);
            this.comboSex.Name = "comboSex";
            this.comboSex.Size = new System.Drawing.Size(125, 21);
            this.comboSex.TabIndex = 13;
            // 
            // nudLevelTemp
            // 
            this.nudLevelTemp.Location = new System.Drawing.Point(232, 31);
            this.nudLevelTemp.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevelTemp.Name = "nudLevelTemp";
            this.nudLevelTemp.Size = new System.Drawing.Size(56, 20);
            this.nudLevelTemp.TabIndex = 5;
            this.nudLevelTemp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(12, 34);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 13);
            this.labelLevel.TabIndex = 2;
            this.labelLevel.Text = "Level";
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(12, 82);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(25, 13);
            this.labelSex.TabIndex = 12;
            this.labelSex.Text = "Sex";
            // 
            // labelAlignment
            // 
            this.labelAlignment.AutoSize = true;
            this.labelAlignment.Location = new System.Drawing.Point(12, 58);
            this.labelAlignment.Name = "labelAlignment";
            this.labelAlignment.Size = new System.Drawing.Size(53, 13);
            this.labelAlignment.TabIndex = 7;
            this.labelAlignment.Text = "Alignment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Race";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Class";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(76, 7);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(307, 20);
            this.tbName.TabIndex = 1;
            // 
            // comboAlignTemp
            // 
            this.comboAlignTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlignTemp.FormattingEnabled = true;
            this.comboAlignTemp.Items.AddRange(new object[] {
            "None",
            "Good",
            "Neutral",
            "Evil"});
            this.comboAlignTemp.Location = new System.Drawing.Point(232, 54);
            this.comboAlignTemp.Name = "comboAlignTemp";
            this.comboAlignTemp.Size = new System.Drawing.Size(82, 21);
            this.comboAlignTemp.TabIndex = 10;
            // 
            // comboRace
            // 
            this.comboRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRace.FormattingEnabled = true;
            this.comboRace.Location = new System.Drawing.Point(76, 101);
            this.comboRace.Name = "comboRace";
            this.comboRace.Size = new System.Drawing.Size(125, 21);
            this.comboRace.TabIndex = 15;
            // 
            // comboClass
            // 
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(76, 125);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(125, 21);
            this.comboClass.TabIndex = 17;
            // 
            // labelLevelCurrent
            // 
            this.labelLevelCurrent.AutoSize = true;
            this.labelLevelCurrent.Location = new System.Drawing.Point(294, 34);
            this.labelLevelCurrent.Name = "labelLevelCurrent";
            this.labelLevelCurrent.Size = new System.Drawing.Size(46, 13);
            this.labelLevelCurrent.TabIndex = 6;
            this.labelLevelCurrent.Text = "(current)";
            // 
            // labelLevelPerm
            // 
            this.labelLevelPerm.AutoSize = true;
            this.labelLevelPerm.Location = new System.Drawing.Point(138, 34);
            this.labelLevelPerm.Name = "labelLevelPerm";
            this.labelLevelPerm.Size = new System.Drawing.Size(63, 13);
            this.labelLevelPerm.TabIndex = 4;
            this.labelLevelPerm.Text = "(permanent)";
            // 
            // comboAlignPermanent
            // 
            this.comboAlignPermanent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlignPermanent.FormattingEnabled = true;
            this.comboAlignPermanent.Items.AddRange(new object[] {
            "None",
            "Good",
            "Neutral",
            "Evil"});
            this.comboAlignPermanent.Location = new System.Drawing.Point(76, 53);
            this.comboAlignPermanent.Name = "comboAlignPermanent";
            this.comboAlignPermanent.Size = new System.Drawing.Size(82, 21);
            this.comboAlignPermanent.TabIndex = 8;
            // 
            // nudLevelPermanent
            // 
            this.nudLevelPermanent.Location = new System.Drawing.Point(76, 30);
            this.nudLevelPermanent.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevelPermanent.Name = "nudLevelPermanent";
            this.nudLevelPermanent.Size = new System.Drawing.Size(56, 20);
            this.nudLevelPermanent.TabIndex = 3;
            this.nudLevelPermanent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelAlignCurrent
            // 
            this.labelAlignCurrent.AutoSize = true;
            this.labelAlignCurrent.Location = new System.Drawing.Point(320, 57);
            this.labelAlignCurrent.Name = "labelAlignCurrent";
            this.labelAlignCurrent.Size = new System.Drawing.Size(46, 13);
            this.labelAlignCurrent.TabIndex = 11;
            this.labelAlignCurrent.Text = "(current)";
            // 
            // labelAlignPerm
            // 
            this.labelAlignPerm.AutoSize = true;
            this.labelAlignPerm.Location = new System.Drawing.Point(164, 57);
            this.labelAlignPerm.Name = "labelAlignPerm";
            this.labelAlignPerm.Size = new System.Drawing.Size(63, 13);
            this.labelAlignPerm.TabIndex = 9;
            this.labelAlignPerm.Text = "(permanent)";
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Location = new System.Drawing.Point(12, 154);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(26, 13);
            this.labelAge.TabIndex = 18;
            this.labelAge.Text = "Age";
            // 
            // labelBirthYear
            // 
            this.labelBirthYear.AutoSize = true;
            this.labelBirthYear.Location = new System.Drawing.Point(143, 153);
            this.labelBirthYear.Name = "labelBirthYear";
            this.labelBirthYear.Size = new System.Drawing.Size(32, 13);
            this.labelBirthYear.TabIndex = 20;
            this.labelBirthYear.Text = "years";
            // 
            // nudAgeYears
            // 
            this.nudAgeYears.Location = new System.Drawing.Point(76, 149);
            this.nudAgeYears.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAgeYears.Name = "nudAgeYears";
            this.nudAgeYears.Size = new System.Drawing.Size(66, 20);
            this.nudAgeYears.TabIndex = 19;
            this.nudAgeYears.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelBirthDay
            // 
            this.labelBirthDay.AutoSize = true;
            this.labelBirthDay.Location = new System.Drawing.Point(243, 153);
            this.labelBirthDay.Name = "labelBirthDay";
            this.labelBirthDay.Size = new System.Drawing.Size(29, 13);
            this.labelBirthDay.TabIndex = 22;
            this.labelBirthDay.Text = "days";
            // 
            // nudAgeDays
            // 
            this.nudAgeDays.Location = new System.Drawing.Point(186, 151);
            this.nudAgeDays.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAgeDays.Name = "nudAgeDays";
            this.nudAgeDays.Size = new System.Drawing.Size(56, 20);
            this.nudAgeDays.TabIndex = 21;
            this.nudAgeDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelAgeModifier
            // 
            this.labelAgeModifier.AutoSize = true;
            this.labelAgeModifier.Location = new System.Drawing.Point(341, 153);
            this.labelAgeModifier.Name = "labelAgeModifier";
            this.labelAgeModifier.Size = new System.Drawing.Size(43, 13);
            this.labelAgeModifier.TabIndex = 24;
            this.labelAgeModifier.Text = "modifier";
            this.labelAgeModifier.Visible = false;
            // 
            // nudAgeModifier
            // 
            this.nudAgeModifier.Location = new System.Drawing.Point(284, 151);
            this.nudAgeModifier.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAgeModifier.Name = "nudAgeModifier";
            this.nudAgeModifier.Size = new System.Drawing.Size(56, 20);
            this.nudAgeModifier.TabIndex = 23;
            this.nudAgeModifier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAgeModifier.Visible = false;
            // 
            // BasicCharInfoEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 220);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.nudAgeModifier);
            this.Controls.Add(this.nudAgeDays);
            this.Controls.Add(this.nudAgeYears);
            this.Controls.Add(this.nudLevelPermanent);
            this.Controls.Add(this.nudLevelTemp);
            this.Controls.Add(this.comboClass);
            this.Controls.Add(this.comboRace);
            this.Controls.Add(this.comboAlignPermanent);
            this.Controls.Add(this.comboAlignTemp);
            this.Controls.Add(this.comboSex);
            this.Controls.Add(this.labelAgeModifier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelBirthDay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelBirthYear);
            this.Controls.Add(this.labelAlignPerm);
            this.Controls.Add(this.labelLevelPerm);
            this.Controls.Add(this.labelAlignCurrent);
            this.Controls.Add(this.labelLevelCurrent);
            this.Controls.Add(this.labelAlignment);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelSex);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelCharName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(401, 245);
            this.Name = "BasicCharInfoEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Basic Character Info";
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelPermanent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgeModifier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelCharName;
        private System.Windows.Forms.ComboBox comboSex;
        private System.Windows.Forms.NumericUpDown nudLevelTemp;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelAlignment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox comboAlignTemp;
        private System.Windows.Forms.ComboBox comboRace;
        private System.Windows.Forms.ComboBox comboClass;
        private System.Windows.Forms.Label labelLevelCurrent;
        private System.Windows.Forms.Label labelLevelPerm;
        private System.Windows.Forms.ComboBox comboAlignPermanent;
        private System.Windows.Forms.NumericUpDown nudLevelPermanent;
        private System.Windows.Forms.Label labelAlignCurrent;
        private System.Windows.Forms.Label labelAlignPerm;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelBirthYear;
        private System.Windows.Forms.NumericUpDown nudAgeYears;
        private System.Windows.Forms.Label labelBirthDay;
        private System.Windows.Forms.NumericUpDown nudAgeDays;
        private System.Windows.Forms.Label labelAgeModifier;
        private System.Windows.Forms.NumericUpDown nudAgeModifier;
    }
}