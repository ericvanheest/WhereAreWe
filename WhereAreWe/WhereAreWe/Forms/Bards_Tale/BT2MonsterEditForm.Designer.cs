namespace WhereAreWe
{
    partial class BT2MonsterEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BT2MonsterEditForm));
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudHPMinimum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudHPRange = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudGroupSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudArmorClass = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudNumAttacks = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboAttack1 = new System.Windows.Forms.ComboBox();
            this.comboAttack2 = new System.Windows.Forms.ComboBox();
            this.comboAttack3 = new System.Windows.Forms.ComboBox();
            this.comboAttack4 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboTouch = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.nudDamage = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.nudDistance = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.tbBytes = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPMinimum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGroupSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumAttacks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(86, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "NAME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Minimum HP:";
            // 
            // nudHPMinimum
            // 
            this.nudHPMinimum.Location = new System.Drawing.Point(88, 25);
            this.nudHPMinimum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudHPMinimum.Name = "nudHPMinimum";
            this.nudHPMinimum.Size = new System.Drawing.Size(67, 20);
            this.nudHPMinimum.TabIndex = 3;
            this.nudHPMinimum.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudHPMinimum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "HP Range:";
            // 
            // nudHPRange
            // 
            this.nudHPRange.Location = new System.Drawing.Point(88, 48);
            this.nudHPRange.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudHPRange.Name = "nudHPRange";
            this.nudHPRange.Size = new System.Drawing.Size(67, 20);
            this.nudHPRange.TabIndex = 5;
            this.nudHPRange.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudHPRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(311, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "The value applied is a random selection of the 1-bits in the range";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Group Size:";
            // 
            // nudGroupSize
            // 
            this.nudGroupSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudGroupSize.Location = new System.Drawing.Point(88, 71);
            this.nudGroupSize.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.nudGroupSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGroupSize.Name = "nudGroupSize";
            this.nudGroupSize.Size = new System.Drawing.Size(67, 20);
            this.nudGroupSize.TabIndex = 8;
            this.nudGroupSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGroupSize.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudGroupSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(162, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Must be odd";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Speed:";
            // 
            // nudSpeed
            // 
            this.nudSpeed.Location = new System.Drawing.Point(88, 117);
            this.nudSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(67, 20);
            this.nudSpeed.TabIndex = 14;
            this.nudSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSpeed.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(162, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(331, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "The distance (in tens of feet) that a monster can advance in a round)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Armor Class:";
            // 
            // nudArmorClass
            // 
            this.nudArmorClass.Location = new System.Drawing.Point(88, 140);
            this.nudArmorClass.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudArmorClass.Minimum = new decimal(new int[] {
            244,
            0,
            0,
            -2147483648});
            this.nudArmorClass.Name = "nudArmorClass";
            this.nudArmorClass.Size = new System.Drawing.Size(67, 20);
            this.nudArmorClass.TabIndex = 17;
            this.nudArmorClass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudArmorClass.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudArmorClass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 167);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Num Attacks:";
            // 
            // nudNumAttacks
            // 
            this.nudNumAttacks.Location = new System.Drawing.Point(88, 163);
            this.nudNumAttacks.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudNumAttacks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumAttacks.Name = "nudNumAttacks";
            this.nudNumAttacks.Size = new System.Drawing.Size(67, 20);
            this.nudNumAttacks.TabIndex = 19;
            this.nudNumAttacks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumAttacks.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudNumAttacks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 214);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Attack #1:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 237);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Attack #2:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 260);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Attack #3:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 283);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Attack #4:";
            // 
            // comboAttack1
            // 
            this.comboAttack1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttack1.FormattingEnabled = true;
            this.comboAttack1.Location = new System.Drawing.Point(88, 209);
            this.comboAttack1.Name = "comboAttack1";
            this.comboAttack1.Size = new System.Drawing.Size(267, 21);
            this.comboAttack1.TabIndex = 24;
            this.comboAttack1.SelectedIndexChanged += new System.EventHandler(this.onValueChanged);
            // 
            // comboAttack2
            // 
            this.comboAttack2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttack2.FormattingEnabled = true;
            this.comboAttack2.Location = new System.Drawing.Point(88, 232);
            this.comboAttack2.Name = "comboAttack2";
            this.comboAttack2.Size = new System.Drawing.Size(267, 21);
            this.comboAttack2.TabIndex = 26;
            this.comboAttack2.SelectedIndexChanged += new System.EventHandler(this.onValueChanged);
            // 
            // comboAttack3
            // 
            this.comboAttack3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttack3.FormattingEnabled = true;
            this.comboAttack3.Location = new System.Drawing.Point(88, 255);
            this.comboAttack3.Name = "comboAttack3";
            this.comboAttack3.Size = new System.Drawing.Size(267, 21);
            this.comboAttack3.TabIndex = 28;
            this.comboAttack3.SelectedIndexChanged += new System.EventHandler(this.onValueChanged);
            // 
            // comboAttack4
            // 
            this.comboAttack4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttack4.FormattingEnabled = true;
            this.comboAttack4.Location = new System.Drawing.Point(88, 278);
            this.comboAttack4.Name = "comboAttack4";
            this.comboAttack4.Size = new System.Drawing.Size(267, 21);
            this.comboAttack4.TabIndex = 30;
            this.comboAttack4.SelectedIndexChanged += new System.EventHandler(this.onValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 306);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Touch:";
            // 
            // comboTouch
            // 
            this.comboTouch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTouch.FormattingEnabled = true;
            this.comboTouch.Location = new System.Drawing.Point(88, 301);
            this.comboTouch.Name = "comboTouch";
            this.comboTouch.Size = new System.Drawing.Size(112, 21);
            this.comboTouch.TabIndex = 32;
            this.comboTouch.SelectedIndexChanged += new System.EventHandler(this.onValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 190);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Damage:";
            // 
            // nudDamage
            // 
            this.nudDamage.Location = new System.Drawing.Point(88, 186);
            this.nudDamage.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDamage.Name = "nudDamage";
            this.nudDamage.Size = new System.Drawing.Size(67, 20);
            this.nudDamage.TabIndex = 21;
            this.nudDamage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDamage.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudDamage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(161, 190);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(194, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Number of d4 thrown for a melee attack";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(162, 98);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(342, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "The distance (in tens of feet) that a monster groups starts from the party";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 98);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Distance:";
            // 
            // nudDistance
            // 
            this.nudDistance.Location = new System.Drawing.Point(88, 94);
            this.nudDistance.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudDistance.Name = "nudDistance";
            this.nudDistance.Size = new System.Drawing.Size(67, 20);
            this.nudDistance.TabIndex = 11;
            this.nudDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDistance.ValueChanged += new System.EventHandler(this.onValueChanged);
            this.nudDistance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 346);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(36, 13);
            this.label21.TabIndex = 33;
            this.label21.Text = "Bytes:";
            // 
            // tbBytes
            // 
            this.tbBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBytes.Location = new System.Drawing.Point(88, 343);
            this.tbBytes.Name = "tbBytes";
            this.tbBytes.ReadOnly = true;
            this.tbBytes.Size = new System.Drawing.Size(469, 20);
            this.tbBytes.TabIndex = 34;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(401, 369);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(482, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // BT2MonsterEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 404);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbBytes);
            this.Controls.Add(this.comboTouch);
            this.Controls.Add(this.comboAttack4);
            this.Controls.Add(this.comboAttack3);
            this.Controls.Add(this.comboAttack2);
            this.Controls.Add(this.comboAttack1);
            this.Controls.Add(this.nudDamage);
            this.Controls.Add(this.nudNumAttacks);
            this.Controls.Add(this.nudArmorClass);
            this.Controls.Add(this.nudDistance);
            this.Controls.Add(this.nudSpeed);
            this.Controls.Add(this.nudGroupSize);
            this.Controls.Add(this.nudHPRange);
            this.Controls.Add(this.nudHPMinimum);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(528, 399);
            this.Name = "BT2MonsterEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Monster";
            this.Load += new System.EventHandler(this.BT2MonsterEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudHPMinimum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGroupSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumAttacks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudHPMinimum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudHPRange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudGroupSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudArmorClass;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudNumAttacks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboAttack1;
        private System.Windows.Forms.ComboBox comboAttack2;
        private System.Windows.Forms.ComboBox comboAttack3;
        private System.Windows.Forms.ComboBox comboAttack4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboTouch;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudDamage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown nudDistance;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbBytes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}