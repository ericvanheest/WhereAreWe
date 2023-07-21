namespace WhereAreWe
{
    partial class EOB123TrapsControl : TrapsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCharacter = new System.Windows.Forms.ComboBox();
            this.comboTrapType = new System.Windows.Forms.ComboBox();
            this.btnDisarm = new System.Windows.Forms.Button();
            this.gbDisarm = new System.Windows.Forms.GroupBox();
            this.gbDisarm.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Character:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trap Type:";
            // 
            // comboCharacter
            // 
            this.comboCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCharacter.FormattingEnabled = true;
            this.comboCharacter.Location = new System.Drawing.Point(70, 18);
            this.comboCharacter.Name = "comboCharacter";
            this.comboCharacter.Size = new System.Drawing.Size(121, 21);
            this.comboCharacter.TabIndex = 1;
            // 
            // comboTrapType
            // 
            this.comboTrapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTrapType.FormattingEnabled = true;
            this.comboTrapType.Location = new System.Drawing.Point(70, 44);
            this.comboTrapType.Name = "comboTrapType";
            this.comboTrapType.Size = new System.Drawing.Size(121, 21);
            this.comboTrapType.TabIndex = 3;
            // 
            // btnDisarm
            // 
            this.btnDisarm.Location = new System.Drawing.Point(197, 18);
            this.btnDisarm.Name = "btnDisarm";
            this.btnDisarm.Size = new System.Drawing.Size(75, 47);
            this.btnDisarm.TabIndex = 4;
            this.btnDisarm.Text = "Send &Disarm Command";
            this.btnDisarm.UseVisualStyleBackColor = true;
            this.btnDisarm.Click += new System.EventHandler(this.btnDisarm_Click);
            // 
            // gbDisarm
            // 
            this.gbDisarm.Controls.Add(this.label1);
            this.gbDisarm.Controls.Add(this.btnDisarm);
            this.gbDisarm.Controls.Add(this.label2);
            this.gbDisarm.Controls.Add(this.comboTrapType);
            this.gbDisarm.Controls.Add(this.comboCharacter);
            this.gbDisarm.Location = new System.Drawing.Point(3, 3);
            this.gbDisarm.Name = "gbDisarm";
            this.gbDisarm.Size = new System.Drawing.Size(280, 74);
            this.gbDisarm.TabIndex = 0;
            this.gbDisarm.TabStop = false;
            this.gbDisarm.Text = "Attempt to disarm a trap";
            // 
            // Wiz1TrapsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.gbDisarm);
            this.Name = "Wiz1TrapsControl";
            this.Size = new System.Drawing.Size(285, 79);
            this.gbDisarm.ResumeLayout(false);
            this.gbDisarm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCharacter;
        private System.Windows.Forms.ComboBox comboTrapType;
        private System.Windows.Forms.Button btnDisarm;
        private System.Windows.Forms.GroupBox gbDisarm;



    }
}
