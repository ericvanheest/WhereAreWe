namespace WhereAreWe
{
    partial class AttributeEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVal1 = new System.Windows.Forms.TextBox();
            this.tbVal2 = new System.Windows.Forms.TextBox();
            this.tbVal3 = new System.Windows.Forms.TextBox();
            this.labelNewValue2 = new System.Windows.Forms.Label();
            this.labelNewValue3 = new System.Windows.Forms.Label();
            this.labelInvalid1 = new System.Windows.Forms.Label();
            this.labelInvalid2 = new System.Windows.Forms.Label();
            this.labelInvalid3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Value:";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(92, 10);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(95, 13);
            this.labelCurrent.TabIndex = 1;
            this.labelCurrent.Text = "VAL1/VAL2/VAL3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "New Value:";
            // 
            // tbVal1
            // 
            this.tbVal1.Location = new System.Drawing.Point(95, 33);
            this.tbVal1.Name = "tbVal1";
            this.tbVal1.Size = new System.Drawing.Size(88, 20);
            this.tbVal1.TabIndex = 3;
            this.tbVal1.TextChanged += new System.EventHandler(this.tbVal1_TextChanged);
            // 
            // tbVal2
            // 
            this.tbVal2.Location = new System.Drawing.Point(95, 59);
            this.tbVal2.Name = "tbVal2";
            this.tbVal2.Size = new System.Drawing.Size(88, 20);
            this.tbVal2.TabIndex = 6;
            this.tbVal2.TextChanged += new System.EventHandler(this.tbVal2_TextChanged);
            // 
            // tbVal3
            // 
            this.tbVal3.Location = new System.Drawing.Point(95, 85);
            this.tbVal3.Name = "tbVal3";
            this.tbVal3.Size = new System.Drawing.Size(88, 20);
            this.tbVal3.TabIndex = 9;
            this.tbVal3.TextChanged += new System.EventHandler(this.tbVal3_TextChanged);
            // 
            // labelNewValue2
            // 
            this.labelNewValue2.AutoSize = true;
            this.labelNewValue2.Location = new System.Drawing.Point(12, 62);
            this.labelNewValue2.Name = "labelNewValue2";
            this.labelNewValue2.Size = new System.Drawing.Size(71, 13);
            this.labelNewValue2.TabIndex = 5;
            this.labelNewValue2.Text = "New Value 2:";
            // 
            // labelNewValue3
            // 
            this.labelNewValue3.AutoSize = true;
            this.labelNewValue3.Location = new System.Drawing.Point(12, 88);
            this.labelNewValue3.Name = "labelNewValue3";
            this.labelNewValue3.Size = new System.Drawing.Size(71, 13);
            this.labelNewValue3.TabIndex = 8;
            this.labelNewValue3.Text = "New Value 3:";
            // 
            // labelInvalid1
            // 
            this.labelInvalid1.AutoSize = true;
            this.labelInvalid1.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid1.Location = new System.Drawing.Point(189, 36);
            this.labelInvalid1.Name = "labelInvalid1";
            this.labelInvalid1.Size = new System.Drawing.Size(68, 13);
            this.labelInvalid1.TabIndex = 4;
            this.labelInvalid1.Text = "Invalid Value";
            this.labelInvalid1.Visible = false;
            // 
            // labelInvalid2
            // 
            this.labelInvalid2.AutoSize = true;
            this.labelInvalid2.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid2.Location = new System.Drawing.Point(189, 62);
            this.labelInvalid2.Name = "labelInvalid2";
            this.labelInvalid2.Size = new System.Drawing.Size(68, 13);
            this.labelInvalid2.TabIndex = 7;
            this.labelInvalid2.Text = "Invalid Value";
            this.labelInvalid2.Visible = false;
            // 
            // labelInvalid3
            // 
            this.labelInvalid3.AutoSize = true;
            this.labelInvalid3.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid3.Location = new System.Drawing.Point(189, 88);
            this.labelInvalid3.Name = "labelInvalid3";
            this.labelInvalid3.Size = new System.Drawing.Size(68, 13);
            this.labelInvalid3.TabIndex = 10;
            this.labelInvalid3.Text = "Invalid Value";
            this.labelInvalid3.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(214, 119);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(295, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AttributeEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(382, 154);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbVal3);
            this.Controls.Add(this.tbVal2);
            this.Controls.Add(this.tbVal1);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelNewValue3);
            this.Controls.Add(this.labelNewValue2);
            this.Controls.Add(this.labelInvalid3);
            this.Controls.Add(this.labelInvalid2);
            this.Controls.Add(this.labelInvalid1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "AttributeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Attribute";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVal1;
        private System.Windows.Forms.TextBox tbVal2;
        private System.Windows.Forms.TextBox tbVal3;
        private System.Windows.Forms.Label labelNewValue2;
        private System.Windows.Forms.Label labelNewValue3;
        private System.Windows.Forms.Label labelInvalid1;
        private System.Windows.Forms.Label labelInvalid2;
        private System.Windows.Forms.Label labelInvalid3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}