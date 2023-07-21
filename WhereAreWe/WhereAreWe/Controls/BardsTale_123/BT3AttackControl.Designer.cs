namespace WhereAreWe
{
    partial class BT3AttackControl
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
            this.comboFaces = new System.Windows.Forms.ComboBox();
            this.comboAttack = new System.Windows.Forms.ComboBox();
            this.nudLevel = new System.Windows.Forms.NumericUpDown();
            this.labelAttack = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // comboFaces
            // 
            this.comboFaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboFaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFaces.FormattingEnabled = true;
            this.comboFaces.Location = new System.Drawing.Point(462, 1);
            this.comboFaces.Name = "comboFaces";
            this.comboFaces.Size = new System.Drawing.Size(67, 21);
            this.comboFaces.TabIndex = 3;
            this.comboFaces.SelectedIndexChanged += new System.EventHandler(this.comboFaces_SelectedIndexChanged);
            // 
            // comboAttack
            // 
            this.comboAttack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAttack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAttack.FormattingEnabled = true;
            this.comboAttack.Location = new System.Drawing.Point(0, 0);
            this.comboAttack.Name = "comboAttack";
            this.comboAttack.Size = new System.Drawing.Size(351, 21);
            this.comboAttack.TabIndex = 0;
            this.comboAttack.SelectedIndexChanged += new System.EventHandler(this.comboAttack_SelectedIndexChanged);
            // 
            // nudLevel
            // 
            this.nudLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLevel.Location = new System.Drawing.Point(402, 2);
            this.nudLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(54, 20);
            this.nudLevel.TabIndex = 2;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.ValueChanged += new System.EventHandler(this.nudLevel_ValueChanged);
            // 
            // labelAttack
            // 
            this.labelAttack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAttack.AutoSize = true;
            this.labelAttack.Location = new System.Drawing.Point(357, 6);
            this.labelAttack.Name = "labelAttack";
            this.labelAttack.Size = new System.Drawing.Size(41, 13);
            this.labelAttack.TabIndex = 1;
            this.labelAttack.Text = "at level";
            // 
            // BT3AttackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboFaces);
            this.Controls.Add(this.comboAttack);
            this.Controls.Add(this.nudLevel);
            this.Controls.Add(this.labelAttack);
            this.Name = "BT3AttackControl";
            this.Size = new System.Drawing.Size(529, 23);
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboFaces;
        private System.Windows.Forms.ComboBox comboAttack;
        private System.Windows.Forms.NumericUpDown nudLevel;
        private System.Windows.Forms.Label labelAttack;
    }
}
