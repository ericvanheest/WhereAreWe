namespace WhereAreWe
{
    partial class MM45ItemEditForm
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
            this.comboItem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCharges = new System.Windows.Forms.NumericUpDown();
            this.btnRandom = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFindItem = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboPrefix = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboSuffix = new System.Windows.Forms.ComboBox();
            this.cbCursed = new System.Windows.Forms.CheckBox();
            this.cbBroken = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboEquipped = new System.Windows.Forms.ComboBox();
            this.rbWeapon = new System.Windows.Forms.RadioButton();
            this.rbArmor = new System.Windows.Forms.RadioButton();
            this.rbAccessory = new System.Windows.Forms.RadioButton();
            this.rbMisc = new System.Windows.Forms.RadioButton();
            this.labelTypePrefix = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharges)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(323, 216);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 23;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(404, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "&Item:";
            // 
            // comboItem
            // 
            this.comboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Location = new System.Drawing.Point(105, 82);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(374, 21);
            this.comboItem.TabIndex = 11;
            this.comboItem.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "C&harges:";
            // 
            // nudCharges
            // 
            this.nudCharges.Location = new System.Drawing.Point(105, 132);
            this.nudCharges.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.nudCharges.Name = "nudCharges";
            this.nudCharges.Size = new System.Drawing.Size(60, 20);
            this.nudCharges.TabIndex = 15;
            this.nudCharges.ValueChanged += new System.EventHandler(this.ControlChanged);
            // 
            // btnRandom
            // 
            this.btnRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRandom.Location = new System.Drawing.Point(12, 216);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 22;
            this.btnRandom.Text = "&Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Find item:";
            // 
            // tbFindItem
            // 
            this.tbFindItem.Location = new System.Drawing.Point(105, 7);
            this.tbFindItem.Name = "tbFindItem";
            this.tbFindItem.Size = new System.Drawing.Size(293, 20);
            this.tbFindItem.TabIndex = 1;
            this.tbFindItem.TextChanged += new System.EventHandler(this.tbFindItem_TextChanged);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(404, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 20);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "&Prefix:";
            // 
            // comboPrefix
            // 
            this.comboPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPrefix.FormattingEnabled = true;
            this.comboPrefix.Location = new System.Drawing.Point(105, 57);
            this.comboPrefix.Name = "comboPrefix";
            this.comboPrefix.Size = new System.Drawing.Size(374, 21);
            this.comboPrefix.TabIndex = 9;
            this.comboPrefix.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "&Suffix:";
            // 
            // comboSuffix
            // 
            this.comboSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSuffix.FormattingEnabled = true;
            this.comboSuffix.Location = new System.Drawing.Point(105, 107);
            this.comboSuffix.Name = "comboSuffix";
            this.comboSuffix.Size = new System.Drawing.Size(374, 21);
            this.comboSuffix.TabIndex = 13;
            this.comboSuffix.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // cbCursed
            // 
            this.cbCursed.AutoSize = true;
            this.cbCursed.Location = new System.Drawing.Point(171, 135);
            this.cbCursed.Name = "cbCursed";
            this.cbCursed.Size = new System.Drawing.Size(59, 17);
            this.cbCursed.TabIndex = 16;
            this.cbCursed.Text = "&Cursed";
            this.cbCursed.UseVisualStyleBackColor = true;
            this.cbCursed.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // cbBroken
            // 
            this.cbBroken.AutoSize = true;
            this.cbBroken.Location = new System.Drawing.Point(236, 135);
            this.cbBroken.Name = "cbBroken";
            this.cbBroken.Size = new System.Drawing.Size(60, 17);
            this.cbBroken.TabIndex = 17;
            this.cbBroken.Text = "&Broken";
            this.cbBroken.UseVisualStyleBackColor = true;
            this.cbBroken.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Description:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(101, 185);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(43, 13);
            this.labelDescription.TabIndex = 21;
            this.labelDescription.Text = "<none>";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "E&quipped:";
            // 
            // comboEquipped
            // 
            this.comboEquipped.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEquipped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipped.FormattingEnabled = true;
            this.comboEquipped.Location = new System.Drawing.Point(105, 157);
            this.comboEquipped.Name = "comboEquipped";
            this.comboEquipped.Size = new System.Drawing.Size(374, 21);
            this.comboEquipped.TabIndex = 19;
            this.comboEquipped.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // rbWeapon
            // 
            this.rbWeapon.AutoSize = true;
            this.rbWeapon.Checked = true;
            this.rbWeapon.Location = new System.Drawing.Point(12, 34);
            this.rbWeapon.Name = "rbWeapon";
            this.rbWeapon.Size = new System.Drawing.Size(66, 17);
            this.rbWeapon.TabIndex = 3;
            this.rbWeapon.TabStop = true;
            this.rbWeapon.Text = "&Weapon";
            this.rbWeapon.UseVisualStyleBackColor = true;
            this.rbWeapon.CheckedChanged += new System.EventHandler(this.rbItemType_CheckedChanged);
            // 
            // rbArmor
            // 
            this.rbArmor.AutoSize = true;
            this.rbArmor.Location = new System.Drawing.Point(84, 34);
            this.rbArmor.Name = "rbArmor";
            this.rbArmor.Size = new System.Drawing.Size(52, 17);
            this.rbArmor.TabIndex = 5;
            this.rbArmor.TabStop = true;
            this.rbArmor.Text = "&Armor";
            this.rbArmor.UseVisualStyleBackColor = true;
            this.rbArmor.CheckedChanged += new System.EventHandler(this.rbItemType_CheckedChanged);
            // 
            // rbAccessory
            // 
            this.rbAccessory.AutoSize = true;
            this.rbAccessory.Location = new System.Drawing.Point(142, 34);
            this.rbAccessory.Name = "rbAccessory";
            this.rbAccessory.Size = new System.Drawing.Size(74, 17);
            this.rbAccessory.TabIndex = 6;
            this.rbAccessory.TabStop = true;
            this.rbAccessory.Text = "A&ccessory";
            this.rbAccessory.UseVisualStyleBackColor = true;
            this.rbAccessory.CheckedChanged += new System.EventHandler(this.rbItemType_CheckedChanged);
            // 
            // rbMisc
            // 
            this.rbMisc.AutoSize = true;
            this.rbMisc.Location = new System.Drawing.Point(222, 34);
            this.rbMisc.Name = "rbMisc";
            this.rbMisc.Size = new System.Drawing.Size(92, 17);
            this.rbMisc.TabIndex = 7;
            this.rbMisc.TabStop = true;
            this.rbMisc.Text = "&Miscellaneous";
            this.rbMisc.UseVisualStyleBackColor = true;
            this.rbMisc.CheckedChanged += new System.EventHandler(this.rbItemType_CheckedChanged);
            // 
            // labelTypePrefix
            // 
            this.labelTypePrefix.AutoSize = true;
            this.labelTypePrefix.Location = new System.Drawing.Point(10, 35);
            this.labelTypePrefix.Name = "labelTypePrefix";
            this.labelTypePrefix.Size = new System.Drawing.Size(34, 13);
            this.labelTypePrefix.TabIndex = 0;
            this.labelTypePrefix.Text = "Type:";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(102, 35);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(34, 13);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "Type:";
            // 
            // MM45ItemEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 251);
            this.Controls.Add(this.rbMisc);
            this.Controls.Add(this.rbAccessory);
            this.Controls.Add(this.rbArmor);
            this.Controls.Add(this.rbWeapon);
            this.Controls.Add(this.cbBroken);
            this.Controls.Add(this.cbCursed);
            this.Controls.Add(this.tbFindItem);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.nudCharges);
            this.Controls.Add(this.comboEquipped);
            this.Controls.Add(this.comboSuffix);
            this.Controls.Add(this.comboPrefix);
            this.Controls.Add(this.comboItem);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelTypePrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(281, 155);
            this.Name = "MM45ItemEditForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Item";
            this.Load += new System.EventHandler(this.ItemEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCharges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCharges;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFindItem;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboPrefix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboSuffix;
        private System.Windows.Forms.CheckBox cbCursed;
        private System.Windows.Forms.CheckBox cbBroken;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboEquipped;
        private System.Windows.Forms.RadioButton rbWeapon;
        private System.Windows.Forms.RadioButton rbArmor;
        private System.Windows.Forms.RadioButton rbAccessory;
        private System.Windows.Forms.RadioButton rbMisc;
        private System.Windows.Forms.Label labelTypePrefix;
        private System.Windows.Forms.Label labelType;
    }
}