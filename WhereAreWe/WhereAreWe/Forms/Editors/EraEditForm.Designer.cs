namespace WhereAreWe
{
    partial class EraEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboCurrentEra = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudEra1Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra1Day = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudEra2Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra2Day = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudEra3Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra3Day = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudEra4Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra4Day = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudEra5Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra6Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra7Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra8Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra5Day = new System.Windows.Forms.NumericUpDown();
            this.nudEra6Day = new System.Windows.Forms.NumericUpDown();
            this.nudEra7Day = new System.Windows.Forms.NumericUpDown();
            this.nudEra8Day = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudEra9Year = new System.Windows.Forms.NumericUpDown();
            this.nudEra9Day = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudSteps = new System.Windows.Forms.NumericUpDown();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra1Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra1Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra2Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra2Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra3Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra3Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra4Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra4Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra5Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra6Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra7Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra8Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra5Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra6Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra7Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra8Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra9Year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra9Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(193, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 34;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Current Era:";
            // 
            // comboCurrentEra
            // 
            this.comboCurrentEra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCurrentEra.FormattingEnabled = true;
            this.comboCurrentEra.Items.AddRange(new object[] {
            "1: 2nd Century",
            "2: 3rd Century",
            "3: 4th Century",
            "4: 5th Century",
            "5: 6th Century",
            "6: 7th Century",
            "7: 8th Century",
            "8: 9th Century",
            "9: 10th Century"});
            this.comboCurrentEra.Location = new System.Drawing.Point(259, 23);
            this.comboCurrentEra.Name = "comboCurrentEra";
            this.comboCurrentEra.Size = new System.Drawing.Size(113, 21);
            this.comboCurrentEra.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Era 1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Year";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Day";
            // 
            // nudEra1Year
            // 
            this.nudEra1Year.Location = new System.Drawing.Point(53, 24);
            this.nudEra1Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra1Year.Name = "nudEra1Year";
            this.nudEra1Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra1Year.TabIndex = 3;
            this.nudEra1Year.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudEra1Day
            // 
            this.nudEra1Day.Location = new System.Drawing.Point(117, 24);
            this.nudEra1Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra1Day.Name = "nudEra1Day";
            this.nudEra1Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra1Day.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Era 2:";
            // 
            // nudEra2Year
            // 
            this.nudEra2Year.Location = new System.Drawing.Point(53, 48);
            this.nudEra2Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra2Year.Name = "nudEra2Year";
            this.nudEra2Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra2Year.TabIndex = 6;
            this.nudEra2Year.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // nudEra2Day
            // 
            this.nudEra2Day.Location = new System.Drawing.Point(117, 48);
            this.nudEra2Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra2Day.Name = "nudEra2Day";
            this.nudEra2Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra2Day.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Era 3:";
            // 
            // nudEra3Year
            // 
            this.nudEra3Year.Location = new System.Drawing.Point(53, 72);
            this.nudEra3Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra3Year.Name = "nudEra3Year";
            this.nudEra3Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra3Year.TabIndex = 9;
            this.nudEra3Year.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // nudEra3Day
            // 
            this.nudEra3Day.Location = new System.Drawing.Point(117, 72);
            this.nudEra3Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra3Day.Name = "nudEra3Day";
            this.nudEra3Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra3Day.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Era 4:";
            // 
            // nudEra4Year
            // 
            this.nudEra4Year.Location = new System.Drawing.Point(53, 96);
            this.nudEra4Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra4Year.Name = "nudEra4Year";
            this.nudEra4Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra4Year.TabIndex = 12;
            this.nudEra4Year.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            // 
            // nudEra4Day
            // 
            this.nudEra4Day.Location = new System.Drawing.Point(117, 96);
            this.nudEra4Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra4Day.Name = "nudEra4Day";
            this.nudEra4Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra4Day.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Era 5:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Era 6:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Era 7:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Era 8:";
            // 
            // nudEra5Year
            // 
            this.nudEra5Year.Location = new System.Drawing.Point(53, 120);
            this.nudEra5Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra5Year.Name = "nudEra5Year";
            this.nudEra5Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra5Year.TabIndex = 15;
            this.nudEra5Year.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudEra6Year
            // 
            this.nudEra6Year.Location = new System.Drawing.Point(53, 144);
            this.nudEra6Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra6Year.Name = "nudEra6Year";
            this.nudEra6Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra6Year.TabIndex = 18;
            this.nudEra6Year.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // nudEra7Year
            // 
            this.nudEra7Year.Location = new System.Drawing.Point(53, 168);
            this.nudEra7Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra7Year.Name = "nudEra7Year";
            this.nudEra7Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra7Year.TabIndex = 21;
            this.nudEra7Year.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            // 
            // nudEra8Year
            // 
            this.nudEra8Year.Location = new System.Drawing.Point(53, 192);
            this.nudEra8Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra8Year.Name = "nudEra8Year";
            this.nudEra8Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra8Year.TabIndex = 24;
            this.nudEra8Year.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // nudEra5Day
            // 
            this.nudEra5Day.Location = new System.Drawing.Point(117, 120);
            this.nudEra5Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra5Day.Name = "nudEra5Day";
            this.nudEra5Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra5Day.TabIndex = 16;
            // 
            // nudEra6Day
            // 
            this.nudEra6Day.Location = new System.Drawing.Point(117, 144);
            this.nudEra6Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra6Day.Name = "nudEra6Day";
            this.nudEra6Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra6Day.TabIndex = 19;
            // 
            // nudEra7Day
            // 
            this.nudEra7Day.Location = new System.Drawing.Point(117, 168);
            this.nudEra7Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra7Day.Name = "nudEra7Day";
            this.nudEra7Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra7Day.TabIndex = 22;
            // 
            // nudEra8Day
            // 
            this.nudEra8Day.Location = new System.Drawing.Point(117, 192);
            this.nudEra8Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra8Day.Name = "nudEra8Day";
            this.nudEra8Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra8Day.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Era 9:";
            // 
            // nudEra9Year
            // 
            this.nudEra9Year.Location = new System.Drawing.Point(53, 216);
            this.nudEra9Year.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudEra9Year.Name = "nudEra9Year";
            this.nudEra9Year.Size = new System.Drawing.Size(58, 20);
            this.nudEra9Year.TabIndex = 27;
            this.nudEra9Year.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // nudEra9Day
            // 
            this.nudEra9Day.Location = new System.Drawing.Point(117, 216);
            this.nudEra9Day.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEra9Day.Name = "nudEra9Day";
            this.nudEra9Day.Size = new System.Drawing.Size(58, 20);
            this.nudEra9Day.TabIndex = 28;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(190, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Steps";
            // 
            // nudSteps
            // 
            this.nudSteps.Location = new System.Drawing.Point(259, 48);
            this.nudSteps.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSteps.Name = "nudSteps";
            this.nudSteps.Size = new System.Drawing.Size(58, 20);
            this.nudSteps.TabIndex = 32;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(259, 76);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 33;
            this.btnReset.Text = "&Reset all";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // EraEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 243);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.nudEra9Day);
            this.Controls.Add(this.nudEra8Day);
            this.Controls.Add(this.nudEra4Day);
            this.Controls.Add(this.nudEra7Day);
            this.Controls.Add(this.nudEra3Day);
            this.Controls.Add(this.nudEra6Day);
            this.Controls.Add(this.nudSteps);
            this.Controls.Add(this.nudEra2Day);
            this.Controls.Add(this.nudEra5Day);
            this.Controls.Add(this.nudEra1Day);
            this.Controls.Add(this.nudEra9Year);
            this.Controls.Add(this.nudEra8Year);
            this.Controls.Add(this.nudEra4Year);
            this.Controls.Add(this.nudEra7Year);
            this.Controls.Add(this.nudEra3Year);
            this.Controls.Add(this.nudEra6Year);
            this.Controls.Add(this.nudEra2Year);
            this.Controls.Add(this.nudEra5Year);
            this.Controls.Add(this.nudEra1Year);
            this.Controls.Add(this.comboCurrentEra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 268);
            this.Name = "EraEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Date and Time";
            ((System.ComponentModel.ISupportInitialize)(this.nudEra1Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra1Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra2Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra2Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra3Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra3Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra4Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra4Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra5Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra6Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra7Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra8Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra5Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra6Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra7Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra8Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra9Year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEra9Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboCurrentEra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudEra1Year;
        private System.Windows.Forms.NumericUpDown nudEra1Day;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudEra2Year;
        private System.Windows.Forms.NumericUpDown nudEra2Day;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudEra3Year;
        private System.Windows.Forms.NumericUpDown nudEra3Day;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudEra4Year;
        private System.Windows.Forms.NumericUpDown nudEra4Day;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudEra5Year;
        private System.Windows.Forms.NumericUpDown nudEra6Year;
        private System.Windows.Forms.NumericUpDown nudEra7Year;
        private System.Windows.Forms.NumericUpDown nudEra8Year;
        private System.Windows.Forms.NumericUpDown nudEra5Day;
        private System.Windows.Forms.NumericUpDown nudEra6Day;
        private System.Windows.Forms.NumericUpDown nudEra7Day;
        private System.Windows.Forms.NumericUpDown nudEra8Day;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudEra9Year;
        private System.Windows.Forms.NumericUpDown nudEra9Day;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudSteps;
        private System.Windows.Forms.Button btnReset;
    }
}