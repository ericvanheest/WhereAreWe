namespace WhereAreWe
{
    partial class EditStrength18Form
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
            this.labelTemporary = new System.Windows.Forms.Label();
            this.tbTemporary = new System.Windows.Forms.TextBox();
            this.tbPermanent = new System.Windows.Forms.TextBox();
            this.labelPermanent = new System.Windows.Forms.Label();
            this.labelInvalid1 = new System.Windows.Forms.Label();
            this.labelInvalid2 = new System.Windows.Forms.Label();
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
            this.labelCurrent.Size = new System.Drawing.Size(36, 13);
            this.labelCurrent.TabIndex = 1;
            this.labelCurrent.Text = "18/00";
            // 
            // labelTemporary
            // 
            this.labelTemporary.AutoSize = true;
            this.labelTemporary.Location = new System.Drawing.Point(12, 60);
            this.labelTemporary.Name = "labelTemporary";
            this.labelTemporary.Size = new System.Drawing.Size(87, 13);
            this.labelTemporary.TabIndex = 5;
            this.labelTemporary.Text = "Temporary Value";
            // 
            // tbTemporary
            // 
            this.tbTemporary.Location = new System.Drawing.Point(111, 57);
            this.tbTemporary.Name = "tbTemporary";
            this.tbTemporary.Size = new System.Drawing.Size(88, 20);
            this.tbTemporary.TabIndex = 6;
            this.tbTemporary.TextChanged += new System.EventHandler(this.tbTemporary_TextChanged);
            // 
            // tbPermanent
            // 
            this.tbPermanent.Location = new System.Drawing.Point(111, 32);
            this.tbPermanent.Name = "tbPermanent";
            this.tbPermanent.Size = new System.Drawing.Size(88, 20);
            this.tbPermanent.TabIndex = 3;
            this.tbPermanent.TextChanged += new System.EventHandler(this.tbPermanent_TextChanged);
            // 
            // labelPermanent
            // 
            this.labelPermanent.AutoSize = true;
            this.labelPermanent.Location = new System.Drawing.Point(12, 35);
            this.labelPermanent.Name = "labelPermanent";
            this.labelPermanent.Size = new System.Drawing.Size(88, 13);
            this.labelPermanent.TabIndex = 2;
            this.labelPermanent.Text = "Permanent Value";
            // 
            // labelInvalid1
            // 
            this.labelInvalid1.AutoSize = true;
            this.labelInvalid1.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid1.Location = new System.Drawing.Point(204, 60);
            this.labelInvalid1.Name = "labelInvalid1";
            this.labelInvalid1.Size = new System.Drawing.Size(68, 13);
            this.labelInvalid1.TabIndex = 7;
            this.labelInvalid1.Text = "Invalid Value";
            this.labelInvalid1.Visible = false;
            // 
            // labelInvalid2
            // 
            this.labelInvalid2.AutoSize = true;
            this.labelInvalid2.ForeColor = System.Drawing.Color.Red;
            this.labelInvalid2.Location = new System.Drawing.Point(204, 35);
            this.labelInvalid2.Name = "labelInvalid2";
            this.labelInvalid2.Size = new System.Drawing.Size(68, 13);
            this.labelInvalid2.TabIndex = 4;
            this.labelInvalid2.Text = "Invalid Value";
            this.labelInvalid2.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(214, 119);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
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
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditStrength18Form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(382, 154);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbPermanent);
            this.Controls.Add(this.tbTemporary);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelPermanent);
            this.Controls.Add(this.labelInvalid2);
            this.Controls.Add(this.labelInvalid1);
            this.Controls.Add(this.labelTemporary);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "EditStrength18Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Strength";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label labelTemporary;
        private System.Windows.Forms.TextBox tbTemporary;
        private System.Windows.Forms.TextBox tbPermanent;
        private System.Windows.Forms.Label labelPermanent;
        private System.Windows.Forms.Label labelInvalid1;
        private System.Windows.Forms.Label labelInvalid2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}