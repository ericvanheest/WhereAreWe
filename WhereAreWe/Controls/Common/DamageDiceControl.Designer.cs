namespace WhereAreWe
{
    partial class DamageDiceControl
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
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelAttack = new System.Windows.Forms.Label();
            this.nudFaces = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudBonus = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).BeginInit();
            this.SuspendLayout();
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(0, 0);
            this.nudQuantity.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(54, 20);
            this.nudQuantity.TabIndex = 2;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.uiItem_ValueChanged);
            // 
            // labelAttack
            // 
            this.labelAttack.AutoSize = true;
            this.labelAttack.Location = new System.Drawing.Point(54, 2);
            this.labelAttack.Name = "labelAttack";
            this.labelAttack.Size = new System.Drawing.Size(13, 13);
            this.labelAttack.TabIndex = 1;
            this.labelAttack.Text = "d";
            // 
            // nudFaces
            // 
            this.nudFaces.Location = new System.Drawing.Point(68, 0);
            this.nudFaces.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudFaces.Name = "nudFaces";
            this.nudFaces.Size = new System.Drawing.Size(54, 20);
            this.nudFaces.TabIndex = 2;
            this.nudFaces.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudFaces.ValueChanged += new System.EventHandler(this.uiItem_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "+";
            // 
            // nudBonus
            // 
            this.nudBonus.Location = new System.Drawing.Point(136, 0);
            this.nudBonus.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudBonus.Name = "nudBonus";
            this.nudBonus.Size = new System.Drawing.Size(54, 20);
            this.nudBonus.TabIndex = 2;
            this.nudBonus.ValueChanged += new System.EventHandler(this.uiItem_ValueChanged);
            // 
            // DamageDiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudBonus);
            this.Controls.Add(this.nudFaces);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAttack);
            this.Name = "DamageDiceControl";
            this.Size = new System.Drawing.Size(196, 23);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label labelAttack;
        private System.Windows.Forms.NumericUpDown nudFaces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBonus;
    }
}
