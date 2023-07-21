namespace WhereAreWe
{
    partial class EditDndBasicCharInfoForm
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
            this.labelLevel1 = new System.Windows.Forms.Label();
            this.labelSex = new System.Windows.Forms.Label();
            this.labelAlignment = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.comboRace = new System.Windows.Forms.ComboBox();
            this.comboClass = new System.Windows.Forms.ComboBox();
            this.comboAlignment = new System.Windows.Forms.ComboBox();
            this.nudLevel1 = new System.Windows.Forms.NumericUpDown();
            this.labelLevel2 = new System.Windows.Forms.Label();
            this.labelLevel3 = new System.Windows.Forms.Label();
            this.nudLevel2 = new System.Windows.Forms.NumericUpDown();
            this.nudLevel3 = new System.Windows.Forms.NumericUpDown();
            this.labelClass1 = new System.Windows.Forms.Label();
            this.labelClass2 = new System.Windows.Forms.Label();
            this.labelClass3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(323, 141);
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
            this.btnCancel.Location = new System.Drawing.Point(404, 141);
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
            "Male",
            "Female"});
            this.comboSex.Location = new System.Drawing.Point(248, 34);
            this.comboSex.Name = "comboSex";
            this.comboSex.Size = new System.Drawing.Size(82, 21);
            this.comboSex.TabIndex = 13;
            // 
            // labelLevel1
            // 
            this.labelLevel1.AutoSize = true;
            this.labelLevel1.Location = new System.Drawing.Point(93, 95);
            this.labelLevel1.Name = "labelLevel1";
            this.labelLevel1.Size = new System.Drawing.Size(33, 13);
            this.labelLevel1.TabIndex = 2;
            this.labelLevel1.Text = "Level";
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(217, 39);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(25, 13);
            this.labelSex.TabIndex = 12;
            this.labelSex.Text = "Sex";
            // 
            // labelAlignment
            // 
            this.labelAlignment.AutoSize = true;
            this.labelAlignment.Location = new System.Drawing.Point(12, 39);
            this.labelAlignment.Name = "labelAlignment";
            this.labelAlignment.Size = new System.Drawing.Size(53, 13);
            this.labelAlignment.TabIndex = 7;
            this.labelAlignment.Text = "Alignment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Race";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 67);
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
            this.tbName.Size = new System.Drawing.Size(403, 20);
            this.tbName.TabIndex = 1;
            // 
            // comboRace
            // 
            this.comboRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRace.FormattingEnabled = true;
            this.comboRace.Location = new System.Drawing.Point(396, 34);
            this.comboRace.Name = "comboRace";
            this.comboRace.Size = new System.Drawing.Size(82, 21);
            this.comboRace.TabIndex = 15;
            // 
            // comboClass
            // 
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(76, 62);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(254, 21);
            this.comboClass.TabIndex = 17;
            this.comboClass.SelectedIndexChanged += new System.EventHandler(this.comboClass_SelectedIndexChanged);
            // 
            // comboAlignment
            // 
            this.comboAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlignment.FormattingEnabled = true;
            this.comboAlignment.Location = new System.Drawing.Point(76, 34);
            this.comboAlignment.Name = "comboAlignment";
            this.comboAlignment.Size = new System.Drawing.Size(125, 21);
            this.comboAlignment.TabIndex = 8;
            // 
            // nudLevel1
            // 
            this.nudLevel1.Location = new System.Drawing.Point(133, 93);
            this.nudLevel1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevel1.Name = "nudLevel1";
            this.nudLevel1.Size = new System.Drawing.Size(56, 20);
            this.nudLevel1.TabIndex = 3;
            this.nudLevel1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelLevel2
            // 
            this.labelLevel2.AutoSize = true;
            this.labelLevel2.Location = new System.Drawing.Point(93, 119);
            this.labelLevel2.Name = "labelLevel2";
            this.labelLevel2.Size = new System.Drawing.Size(33, 13);
            this.labelLevel2.TabIndex = 2;
            this.labelLevel2.Text = "Level";
            // 
            // labelLevel3
            // 
            this.labelLevel3.AutoSize = true;
            this.labelLevel3.Location = new System.Drawing.Point(93, 143);
            this.labelLevel3.Name = "labelLevel3";
            this.labelLevel3.Size = new System.Drawing.Size(33, 13);
            this.labelLevel3.TabIndex = 2;
            this.labelLevel3.Text = "Level";
            // 
            // nudLevel2
            // 
            this.nudLevel2.Location = new System.Drawing.Point(133, 117);
            this.nudLevel2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevel2.Name = "nudLevel2";
            this.nudLevel2.Size = new System.Drawing.Size(56, 20);
            this.nudLevel2.TabIndex = 3;
            this.nudLevel2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudLevel3
            // 
            this.nudLevel3.Location = new System.Drawing.Point(133, 141);
            this.nudLevel3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevel3.Name = "nudLevel3";
            this.nudLevel3.Size = new System.Drawing.Size(56, 20);
            this.nudLevel3.TabIndex = 3;
            this.nudLevel3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelClass1
            // 
            this.labelClass1.AutoSize = true;
            this.labelClass1.Location = new System.Drawing.Point(195, 95);
            this.labelClass1.Name = "labelClass1";
            this.labelClass1.Size = new System.Drawing.Size(47, 13);
            this.labelClass1.TabIndex = 2;
            this.labelClass1.Text = "(Class 1)";
            // 
            // labelClass2
            // 
            this.labelClass2.AutoSize = true;
            this.labelClass2.Location = new System.Drawing.Point(195, 119);
            this.labelClass2.Name = "labelClass2";
            this.labelClass2.Size = new System.Drawing.Size(47, 13);
            this.labelClass2.TabIndex = 2;
            this.labelClass2.Text = "(Class 2)";
            // 
            // labelClass3
            // 
            this.labelClass3.AutoSize = true;
            this.labelClass3.Location = new System.Drawing.Point(195, 143);
            this.labelClass3.Name = "labelClass3";
            this.labelClass3.Size = new System.Drawing.Size(47, 13);
            this.labelClass3.TabIndex = 2;
            this.labelClass3.Text = "(Class 3)";
            // 
            // EditDndBasicCharInfoForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 176);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.nudLevel3);
            this.Controls.Add(this.nudLevel2);
            this.Controls.Add(this.nudLevel1);
            this.Controls.Add(this.comboClass);
            this.Controls.Add(this.comboRace);
            this.Controls.Add(this.comboAlignment);
            this.Controls.Add(this.comboSex);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelAlignment);
            this.Controls.Add(this.labelSex);
            this.Controls.Add(this.labelLevel3);
            this.Controls.Add(this.labelLevel2);
            this.Controls.Add(this.labelClass3);
            this.Controls.Add(this.labelClass2);
            this.Controls.Add(this.labelClass1);
            this.Controls.Add(this.labelLevel1);
            this.Controls.Add(this.labelCharName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditDndBasicCharInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Basic Character Info";
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelCharName;
        private System.Windows.Forms.ComboBox comboSex;
        private System.Windows.Forms.Label labelLevel1;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelAlignment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox comboRace;
        private System.Windows.Forms.ComboBox comboClass;
        private System.Windows.Forms.ComboBox comboAlignment;
        private System.Windows.Forms.NumericUpDown nudLevel1;
        private System.Windows.Forms.Label labelLevel2;
        private System.Windows.Forms.Label labelLevel3;
        private System.Windows.Forms.NumericUpDown nudLevel2;
        private System.Windows.Forms.NumericUpDown nudLevel3;
        private System.Windows.Forms.Label labelClass1;
        private System.Windows.Forms.Label labelClass2;
        private System.Windows.Forms.Label labelClass3;
    }
}