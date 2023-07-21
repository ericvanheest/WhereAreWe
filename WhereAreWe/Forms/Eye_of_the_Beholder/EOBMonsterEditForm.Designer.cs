namespace WhereAreWe
{
    partial class EOBMonsterEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EOBMonsterEditForm));
            this.labelName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudHPCurrent = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudArmorClass = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nudExperience = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudHPMax = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudWeapon = new System.Windows.Forms.NumericUpDown();
            this.labelWeapon = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.labelPocket = new System.Windows.Forms.Label();
            this.nudPocket = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbUnknown01 = new System.Windows.Forms.CheckBox();
            this.cbUnknown02 = new System.Windows.Forms.CheckBox();
            this.cbUnknown04 = new System.Windows.Forms.CheckBox();
            this.cbUnknown08 = new System.Windows.Forms.CheckBox();
            this.cbUnknown40 = new System.Windows.Forms.CheckBox();
            this.cbRust = new System.Windows.Forms.CheckBox();
            this.cbParalyze = new System.Windows.Forms.CheckBox();
            this.cbPoison = new System.Windows.Forms.CheckBox();
            this.nudUnknown8 = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown7 = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown6 = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown5 = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown4 = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown3 = new System.Windows.Forms.NumericUpDown();
            this.nudHitDice = new System.Windows.Forms.NumericUpDown();
            this.nudUnknown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudNumAttacks = new System.Windows.Forms.NumericUpDown();
            this.attack3 = new WhereAreWe.DamageDiceControl();
            this.attack2 = new WhereAreWe.DamageDiceControl();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.attack1 = new WhereAreWe.DamageDiceControl();
            this.nudUndead = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeapon)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPocket)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitDice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumAttacks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUndead)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(54, 10);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "NAME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Current HP:";
            // 
            // nudHPCurrent
            // 
            this.nudHPCurrent.Location = new System.Drawing.Point(71, 18);
            this.nudHPCurrent.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudHPCurrent.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.nudHPCurrent.Name = "nudHPCurrent";
            this.nudHPCurrent.Size = new System.Drawing.Size(67, 20);
            this.nudHPCurrent.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "AC:";
            // 
            // nudArmorClass
            // 
            this.nudArmorClass.Location = new System.Drawing.Point(63, 19);
            this.nudArmorClass.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudArmorClass.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudArmorClass.Name = "nudArmorClass";
            this.nudArmorClass.Size = new System.Drawing.Size(56, 20);
            this.nudArmorClass.TabIndex = 1;
            this.nudArmorClass.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Attack #2:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Attack #3:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(274, 392);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Attack #1:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(148, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Experience:";
            // 
            // nudExperience
            // 
            this.nudExperience.Location = new System.Drawing.Point(223, 19);
            this.nudExperience.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudExperience.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.nudExperience.Name = "nudExperience";
            this.nudExperience.Size = new System.Drawing.Size(66, 20);
            this.nudExperience.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(304, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 4;
            this.label18.Text = "Speed:";
            // 
            // nudSpeed
            // 
            this.nudSpeed.Location = new System.Drawing.Point(348, 18);
            this.nudSpeed.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudSpeed.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(56, 20);
            this.nudSpeed.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max HP:";
            // 
            // nudHPMax
            // 
            this.nudHPMax.Location = new System.Drawing.Point(209, 18);
            this.nudHPMax.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudHPMax.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.nudHPMax.Name = "nudHPMax";
            this.nudHPMax.Size = new System.Drawing.Size(67, 20);
            this.nudHPMax.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Weapon:";
            // 
            // nudWeapon
            // 
            this.nudWeapon.Location = new System.Drawing.Point(71, 44);
            this.nudWeapon.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudWeapon.Name = "nudWeapon";
            this.nudWeapon.Size = new System.Drawing.Size(67, 20);
            this.nudWeapon.TabIndex = 7;
            this.nudWeapon.ValueChanged += new System.EventHandler(this.nudItem_ValueChanged);
            // 
            // labelWeapon
            // 
            this.labelWeapon.AutoSize = true;
            this.labelWeapon.Location = new System.Drawing.Point(144, 46);
            this.labelWeapon.Name = "labelWeapon";
            this.labelWeapon.Size = new System.Drawing.Size(39, 13);
            this.labelWeapon.TabIndex = 8;
            this.labelWeapon.Text = "(None)";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.labelPocket);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelWeapon);
            this.groupBox1.Controls.Add(this.nudHPCurrent);
            this.groupBox1.Controls.Add(this.nudPocket);
            this.groupBox1.Controls.Add(this.nudHPMax);
            this.groupBox1.Controls.Add(this.nudWeapon);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.nudSpeed);
            this.groupBox1.Location = new System.Drawing.Point(15, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 97);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Affects this specific monster only";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Pocket:";
            // 
            // labelPocket
            // 
            this.labelPocket.AutoSize = true;
            this.labelPocket.Location = new System.Drawing.Point(144, 72);
            this.labelPocket.Name = "labelPocket";
            this.labelPocket.Size = new System.Drawing.Size(39, 13);
            this.labelPocket.TabIndex = 11;
            this.labelPocket.Text = "(None)";
            // 
            // nudPocket
            // 
            this.nudPocket.Location = new System.Drawing.Point(71, 70);
            this.nudPocket.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.nudPocket.Name = "nudPocket";
            this.nudPocket.Size = new System.Drawing.Size(67, 20);
            this.nudPocket.TabIndex = 10;
            this.nudPocket.ValueChanged += new System.EventHandler(this.nudPocket_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbUnknown01);
            this.groupBox2.Controls.Add(this.cbUnknown02);
            this.groupBox2.Controls.Add(this.cbUnknown04);
            this.groupBox2.Controls.Add(this.cbUnknown08);
            this.groupBox2.Controls.Add(this.cbUnknown40);
            this.groupBox2.Controls.Add(this.cbRust);
            this.groupBox2.Controls.Add(this.cbParalyze);
            this.groupBox2.Controls.Add(this.cbPoison);
            this.groupBox2.Controls.Add(this.nudUnknown8);
            this.groupBox2.Controls.Add(this.nudUnknown7);
            this.groupBox2.Controls.Add(this.nudUnknown6);
            this.groupBox2.Controls.Add(this.nudUnknown5);
            this.groupBox2.Controls.Add(this.nudUnknown4);
            this.groupBox2.Controls.Add(this.nudUnknown3);
            this.groupBox2.Controls.Add(this.nudHitDice);
            this.groupBox2.Controls.Add(this.nudUnknown1);
            this.groupBox2.Controls.Add(this.nudArmorClass);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nudNumAttacks);
            this.groupBox2.Controls.Add(this.attack3);
            this.groupBox2.Controls.Add(this.nudExperience);
            this.groupBox2.Controls.Add(this.attack2);
            this.groupBox2.Controls.Add(this.nudSize);
            this.groupBox2.Controls.Add(this.attack1);
            this.groupBox2.Controls.Add(this.nudUndead);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 250);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Affects all monsters of this type";
            // 
            // cbUnknown01
            // 
            this.cbUnknown01.AutoSize = true;
            this.cbUnknown01.Location = new System.Drawing.Point(319, 160);
            this.cbUnknown01.Name = "cbUnknown01";
            this.cbUnknown01.Size = new System.Drawing.Size(93, 17);
            this.cbUnknown01.TabIndex = 25;
            this.cbUnknown01.Text = "Unknown (01)";
            this.cbUnknown01.UseVisualStyleBackColor = true;
            // 
            // cbUnknown02
            // 
            this.cbUnknown02.AutoSize = true;
            this.cbUnknown02.Location = new System.Drawing.Point(319, 140);
            this.cbUnknown02.Name = "cbUnknown02";
            this.cbUnknown02.Size = new System.Drawing.Size(75, 17);
            this.cbUnknown02.TabIndex = 24;
            this.cbUnknown02.Text = "Resist Fire";
            this.cbUnknown02.UseVisualStyleBackColor = true;
            // 
            // cbUnknown04
            // 
            this.cbUnknown04.AutoSize = true;
            this.cbUnknown04.Location = new System.Drawing.Point(319, 120);
            this.cbUnknown04.Name = "cbUnknown04";
            this.cbUnknown04.Size = new System.Drawing.Size(84, 17);
            this.cbUnknown04.TabIndex = 23;
            this.cbUnknown04.Text = "Resist Slash";
            this.cbUnknown04.UseVisualStyleBackColor = true;
            // 
            // cbUnknown08
            // 
            this.cbUnknown08.AutoSize = true;
            this.cbUnknown08.Location = new System.Drawing.Point(319, 100);
            this.cbUnknown08.Name = "cbUnknown08";
            this.cbUnknown08.Size = new System.Drawing.Size(73, 17);
            this.cbUnknown08.TabIndex = 22;
            this.cbUnknown08.Text = "Resist Ice";
            this.cbUnknown08.UseVisualStyleBackColor = true;
            // 
            // cbUnknown40
            // 
            this.cbUnknown40.AutoSize = true;
            this.cbUnknown40.Location = new System.Drawing.Point(319, 40);
            this.cbUnknown40.Name = "cbUnknown40";
            this.cbUnknown40.Size = new System.Drawing.Size(101, 17);
            this.cbUnknown40.TabIndex = 19;
            this.cbUnknown40.Text = "Resist Lightning";
            this.cbUnknown40.UseVisualStyleBackColor = true;
            // 
            // cbRust
            // 
            this.cbRust.AutoSize = true;
            this.cbRust.Location = new System.Drawing.Point(319, 20);
            this.cbRust.Name = "cbRust";
            this.cbRust.Size = new System.Drawing.Size(48, 17);
            this.cbRust.TabIndex = 18;
            this.cbRust.Text = "Rust";
            this.cbRust.UseVisualStyleBackColor = true;
            // 
            // cbParalyze
            // 
            this.cbParalyze.AutoSize = true;
            this.cbParalyze.Location = new System.Drawing.Point(319, 60);
            this.cbParalyze.Name = "cbParalyze";
            this.cbParalyze.Size = new System.Drawing.Size(66, 17);
            this.cbParalyze.TabIndex = 20;
            this.cbParalyze.Text = "Paralyze";
            this.cbParalyze.UseVisualStyleBackColor = true;
            // 
            // cbPoison
            // 
            this.cbPoison.AutoSize = true;
            this.cbPoison.Location = new System.Drawing.Point(319, 80);
            this.cbPoison.Name = "cbPoison";
            this.cbPoison.Size = new System.Drawing.Size(58, 17);
            this.cbPoison.TabIndex = 21;
            this.cbPoison.Text = "Poison";
            this.cbPoison.UseVisualStyleBackColor = true;
            // 
            // nudUnknown8
            // 
            this.nudUnknown8.Location = new System.Drawing.Point(291, 216);
            this.nudUnknown8.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown8.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown8.Name = "nudUnknown8";
            this.nudUnknown8.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown8.TabIndex = 33;
            // 
            // nudUnknown7
            // 
            this.nudUnknown7.Location = new System.Drawing.Point(229, 216);
            this.nudUnknown7.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown7.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown7.Name = "nudUnknown7";
            this.nudUnknown7.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown7.TabIndex = 32;
            // 
            // nudUnknown6
            // 
            this.nudUnknown6.Location = new System.Drawing.Point(167, 216);
            this.nudUnknown6.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown6.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown6.Name = "nudUnknown6";
            this.nudUnknown6.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown6.TabIndex = 31;
            // 
            // nudUnknown5
            // 
            this.nudUnknown5.Location = new System.Drawing.Point(105, 216);
            this.nudUnknown5.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown5.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown5.Name = "nudUnknown5";
            this.nudUnknown5.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown5.TabIndex = 30;
            // 
            // nudUnknown4
            // 
            this.nudUnknown4.Location = new System.Drawing.Point(291, 190);
            this.nudUnknown4.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown4.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown4.Name = "nudUnknown4";
            this.nudUnknown4.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown4.TabIndex = 29;
            // 
            // nudUnknown3
            // 
            this.nudUnknown3.Location = new System.Drawing.Point(229, 190);
            this.nudUnknown3.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown3.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown3.Name = "nudUnknown3";
            this.nudUnknown3.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown3.TabIndex = 28;
            // 
            // nudHitDice
            // 
            this.nudHitDice.Location = new System.Drawing.Point(223, 71);
            this.nudHitDice.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudHitDice.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudHitDice.Name = "nudHitDice";
            this.nudHitDice.Size = new System.Drawing.Size(66, 20);
            this.nudHitDice.TabIndex = 11;
            // 
            // nudUnknown1
            // 
            this.nudUnknown1.Location = new System.Drawing.Point(105, 190);
            this.nudUnknown1.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUnknown1.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudUnknown1.Name = "nudUnknown1";
            this.nudUnknown1.Size = new System.Drawing.Size(56, 20);
            this.nudUnknown1.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Num. Attacks:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Hit Dice (d8):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Size:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Undead:";
            // 
            // nudNumAttacks
            // 
            this.nudNumAttacks.Location = new System.Drawing.Point(223, 45);
            this.nudNumAttacks.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudNumAttacks.Name = "nudNumAttacks";
            this.nudNumAttacks.Size = new System.Drawing.Size(66, 20);
            this.nudNumAttacks.TabIndex = 9;
            // 
            // attack3
            // 
            this.attack3.Bonus = 0;
            this.attack3.BonusMax = 127;
            this.attack3.Faces = 4;
            this.attack3.FacesMax = 127;
            this.attack3.Location = new System.Drawing.Point(87, 154);
            this.attack3.Name = "attack3";
            this.attack3.Quantity = 1;
            this.attack3.QuantityMax = 127;
            this.attack3.Size = new System.Drawing.Size(192, 22);
            this.attack3.TabIndex = 17;
            // 
            // attack2
            // 
            this.attack2.Bonus = 0;
            this.attack2.BonusMax = 127;
            this.attack2.Faces = 4;
            this.attack2.FacesMax = 127;
            this.attack2.Location = new System.Drawing.Point(87, 131);
            this.attack2.Name = "attack2";
            this.attack2.Quantity = 1;
            this.attack2.QuantityMax = 127;
            this.attack2.Size = new System.Drawing.Size(192, 22);
            this.attack2.TabIndex = 15;
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(63, 45);
            this.nudSize.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(56, 20);
            this.nudSize.TabIndex = 3;
            // 
            // attack1
            // 
            this.attack1.Bonus = 0;
            this.attack1.BonusMax = 127;
            this.attack1.Faces = 4;
            this.attack1.FacesMax = 127;
            this.attack1.Location = new System.Drawing.Point(87, 108);
            this.attack1.Name = "attack1";
            this.attack1.Quantity = 1;
            this.attack1.QuantityMax = 127;
            this.attack1.Size = new System.Drawing.Size(192, 22);
            this.attack1.TabIndex = 13;
            // 
            // nudUndead
            // 
            this.nudUndead.Location = new System.Drawing.Point(63, 71);
            this.nudUndead.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudUndead.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudUndead.Name = "nudUndead";
            this.nudUndead.Size = new System.Drawing.Size(56, 20);
            this.nudUndead.TabIndex = 5;
            this.nudUndead.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Unknown values:";
            // 
            // EOBMonsterEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(442, 427);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(458, 436);
            this.Name = "EOBMonsterEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Monster";
            this.Load += new System.EventHandler(this.EOBMonsterEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudHPCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeapon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPocket)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitDice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUnknown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumAttacks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUndead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudHPCurrent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudArmorClass;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label12;
        private DamageDiceControl attack1;
        private DamageDiceControl attack2;
        private DamageDiceControl attack3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudExperience;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudHPMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudWeapon;
        private System.Windows.Forms.Label labelWeapon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudUndead;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudNumAttacks;
        private System.Windows.Forms.CheckBox cbRust;
        private System.Windows.Forms.CheckBox cbParalyze;
        private System.Windows.Forms.CheckBox cbPoison;
        private System.Windows.Forms.NumericUpDown nudUnknown8;
        private System.Windows.Forms.NumericUpDown nudUnknown7;
        private System.Windows.Forms.NumericUpDown nudUnknown6;
        private System.Windows.Forms.NumericUpDown nudUnknown5;
        private System.Windows.Forms.NumericUpDown nudUnknown4;
        private System.Windows.Forms.NumericUpDown nudUnknown3;
        private System.Windows.Forms.NumericUpDown nudHitDice;
        private System.Windows.Forms.NumericUpDown nudUnknown1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbUnknown40;
        private System.Windows.Forms.CheckBox cbUnknown01;
        private System.Windows.Forms.CheckBox cbUnknown02;
        private System.Windows.Forms.CheckBox cbUnknown04;
        private System.Windows.Forms.CheckBox cbUnknown08;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelPocket;
        private System.Windows.Forms.NumericUpDown nudPocket;
    }
}