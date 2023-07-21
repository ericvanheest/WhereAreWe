namespace WhereAreWe
{
    partial class MM3ItemEditForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.comboElemental = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboMaterial = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboAttribute = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboProperty = new System.Windows.Forms.ComboBox();
            this.cbCursed = new System.Windows.Forms.CheckBox();
            this.cbBroken = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboEquipped = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharges)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(323, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(404, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "&Item:";
            // 
            // comboItem
            // 
            this.comboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Location = new System.Drawing.Point(105, 107);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(374, 21);
            this.comboItem.TabIndex = 10;
            this.comboItem.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "C&harges:";
            // 
            // nudCharges
            // 
            this.nudCharges.Location = new System.Drawing.Point(105, 158);
            this.nudCharges.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.nudCharges.Name = "nudCharges";
            this.nudCharges.Size = new System.Drawing.Size(60, 20);
            this.nudCharges.TabIndex = 14;
            this.nudCharges.ValueChanged += new System.EventHandler(this.ControlChanged);
            // 
            // btnRandom
            // 
            this.btnRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRandom.Location = new System.Drawing.Point(12, 257);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 21;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "&Elemental:";
            // 
            // comboElemental
            // 
            this.comboElemental.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboElemental.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboElemental.FormattingEnabled = true;
            this.comboElemental.Location = new System.Drawing.Point(105, 32);
            this.comboElemental.Name = "comboElemental";
            this.comboElemental.Size = new System.Drawing.Size(374, 21);
            this.comboElemental.TabIndex = 4;
            this.comboElemental.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "&Material:";
            // 
            // comboMaterial
            // 
            this.comboMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterial.FormattingEnabled = true;
            this.comboMaterial.Location = new System.Drawing.Point(105, 57);
            this.comboMaterial.Name = "comboMaterial";
            this.comboMaterial.Size = new System.Drawing.Size(374, 21);
            this.comboMaterial.TabIndex = 6;
            this.comboMaterial.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "&Attribute:";
            // 
            // comboAttribute
            // 
            this.comboAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttribute.FormattingEnabled = true;
            this.comboAttribute.Location = new System.Drawing.Point(105, 82);
            this.comboAttribute.Name = "comboAttribute";
            this.comboAttribute.Size = new System.Drawing.Size(374, 21);
            this.comboAttribute.TabIndex = 8;
            this.comboAttribute.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "&Property:";
            // 
            // comboProperty
            // 
            this.comboProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProperty.FormattingEnabled = true;
            this.comboProperty.Location = new System.Drawing.Point(105, 132);
            this.comboProperty.Name = "comboProperty";
            this.comboProperty.Size = new System.Drawing.Size(374, 21);
            this.comboProperty.TabIndex = 12;
            this.comboProperty.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // cbCursed
            // 
            this.cbCursed.AutoSize = true;
            this.cbCursed.Location = new System.Drawing.Point(171, 161);
            this.cbCursed.Name = "cbCursed";
            this.cbCursed.Size = new System.Drawing.Size(59, 17);
            this.cbCursed.TabIndex = 15;
            this.cbCursed.Text = "&Cursed";
            this.cbCursed.UseVisualStyleBackColor = true;
            this.cbCursed.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // cbBroken
            // 
            this.cbBroken.AutoSize = true;
            this.cbBroken.Location = new System.Drawing.Point(236, 161);
            this.cbBroken.Name = "cbBroken";
            this.cbBroken.Size = new System.Drawing.Size(60, 17);
            this.cbBroken.TabIndex = 16;
            this.cbBroken.Text = "&Broken";
            this.cbBroken.UseVisualStyleBackColor = true;
            this.cbBroken.CheckedChanged += new System.EventHandler(this.ControlChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Description:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(102, 222);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(43, 13);
            this.labelDescription.TabIndex = 20;
            this.labelDescription.Text = "<none>";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "E&quipped:";
            // 
            // comboEquipped
            // 
            this.comboEquipped.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEquipped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEquipped.FormattingEnabled = true;
            this.comboEquipped.Location = new System.Drawing.Point(105, 183);
            this.comboEquipped.Name = "comboEquipped";
            this.comboEquipped.Size = new System.Drawing.Size(374, 21);
            this.comboEquipped.TabIndex = 18;
            this.comboEquipped.SelectedIndexChanged += new System.EventHandler(this.ControlChanged);
            // 
            // MM3ItemEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 292);
            this.Controls.Add(this.cbBroken);
            this.Controls.Add(this.cbCursed);
            this.Controls.Add(this.tbFindItem);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.nudCharges);
            this.Controls.Add(this.comboEquipped);
            this.Controls.Add(this.comboProperty);
            this.Controls.Add(this.comboAttribute);
            this.Controls.Add(this.comboMaterial);
            this.Controls.Add(this.comboElemental);
            this.Controls.Add(this.comboItem);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(281, 155);
            this.Name = "MM3ItemEditForm";
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboElemental;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboMaterial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboAttribute;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboProperty;
        private System.Windows.Forms.CheckBox cbCursed;
        private System.Windows.Forms.CheckBox cbBroken;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboEquipped;
    }
}