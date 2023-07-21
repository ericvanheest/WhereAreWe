namespace WhereAreWe
{
    partial class MM2SecondarySkillForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboSkill1 = new System.Windows.Forms.ComboBox();
            this.comboSkill2 = new System.Windows.Forms.ComboBox();
            this.labelSkills1 = new System.Windows.Forms.Label();
            this.labelSkills2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(310, 245);
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
            this.btnCancel.Location = new System.Drawing.Point(391, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First skill:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Second skill:";
            // 
            // comboSkill1
            // 
            this.comboSkill1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSkill1.FormattingEnabled = true;
            this.comboSkill1.Location = new System.Drawing.Point(67, 7);
            this.comboSkill1.Name = "comboSkill1";
            this.comboSkill1.Size = new System.Drawing.Size(121, 21);
            this.comboSkill1.TabIndex = 1;
            // 
            // comboSkill2
            // 
            this.comboSkill2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSkill2.FormattingEnabled = true;
            this.comboSkill2.Location = new System.Drawing.Point(289, 7);
            this.comboSkill2.Name = "comboSkill2";
            this.comboSkill2.Size = new System.Drawing.Size(121, 21);
            this.comboSkill2.TabIndex = 3;
            // 
            // labelSkills1
            // 
            this.labelSkills1.Location = new System.Drawing.Point(12, 39);
            this.labelSkills1.Name = "labelSkills1";
            this.labelSkills1.Size = new System.Drawing.Size(103, 204);
            this.labelSkills1.TabIndex = 6;
            this.labelSkills1.Text = "SECONDARY SKILLS";
            // 
            // labelSkills2
            // 
            this.labelSkills2.Location = new System.Drawing.Point(121, 39);
            this.labelSkills2.Name = "labelSkills2";
            this.labelSkills2.Size = new System.Drawing.Size(341, 204);
            this.labelSkills2.TabIndex = 6;
            this.labelSkills2.Text = "SECONDARY SKILLS";
            // 
            // MM2SecondarySkillForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(478, 280);
            this.Controls.Add(this.labelSkills2);
            this.Controls.Add(this.labelSkills1);
            this.Controls.Add(this.comboSkill2);
            this.Controls.Add(this.comboSkill1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "MM2SecondarySkillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Secondary Skills";
            this.Load += new System.EventHandler(this.MM2SecondarySkillForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboSkill1;
        private System.Windows.Forms.ComboBox comboSkill2;
        private System.Windows.Forms.Label labelSkills1;
        private System.Windows.Forms.Label labelSkills2;
    }
}