namespace WhereAreWe
{
    partial class EditBTSpellLevels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBTSpellLevels));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nudConjurer = new System.Windows.Forms.NumericUpDown();
            this.nudMagician = new System.Windows.Forms.NumericUpDown();
            this.nudSorcerer = new System.Windows.Forms.NumericUpDown();
            this.nudWizard = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudArchmage = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudConjurer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagician)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSorcerer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWizard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArchmage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conjurer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Magician";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sorcerer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wizard";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(86, 151);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(167, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // nudConjurer
            // 
            this.nudConjurer.Location = new System.Drawing.Point(134, 16);
            this.nudConjurer.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudConjurer.Name = "nudConjurer";
            this.nudConjurer.Size = new System.Drawing.Size(49, 20);
            this.nudConjurer.TabIndex = 1;
            // 
            // nudMagician
            // 
            this.nudMagician.Location = new System.Drawing.Point(134, 41);
            this.nudMagician.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudMagician.Name = "nudMagician";
            this.nudMagician.Size = new System.Drawing.Size(49, 20);
            this.nudMagician.TabIndex = 3;
            // 
            // nudSorcerer
            // 
            this.nudSorcerer.Location = new System.Drawing.Point(134, 66);
            this.nudSorcerer.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudSorcerer.Name = "nudSorcerer";
            this.nudSorcerer.Size = new System.Drawing.Size(49, 20);
            this.nudSorcerer.TabIndex = 5;
            // 
            // nudWizard
            // 
            this.nudWizard.Location = new System.Drawing.Point(134, 91);
            this.nudWizard.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudWizard.Name = "nudWizard";
            this.nudWizard.Size = new System.Drawing.Size(49, 20);
            this.nudWizard.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Archmage";
            // 
            // nudArchmage
            // 
            this.nudArchmage.Location = new System.Drawing.Point(134, 116);
            this.nudArchmage.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudArchmage.Name = "nudArchmage";
            this.nudArchmage.Size = new System.Drawing.Size(49, 20);
            this.nudArchmage.TabIndex = 9;
            // 
            // EditBTSpellLevels
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(254, 186);
            this.Controls.Add(this.nudArchmage);
            this.Controls.Add(this.nudWizard);
            this.Controls.Add(this.nudSorcerer);
            this.Controls.Add(this.nudMagician);
            this.Controls.Add(this.nudConjurer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditBTSpellLevels";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bard\'s Tale Spell Levels";
            ((System.ComponentModel.ISupportInitialize)(this.nudConjurer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagician)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSorcerer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWizard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArchmage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudConjurer;
        private System.Windows.Forms.NumericUpDown nudMagician;
        private System.Windows.Forms.NumericUpDown nudSorcerer;
        private System.Windows.Forms.NumericUpDown nudWizard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudArchmage;
    }
}