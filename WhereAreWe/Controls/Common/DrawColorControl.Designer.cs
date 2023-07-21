namespace WhereAreWe
{
    partial class DrawColorControl
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
            this.labelKey = new System.Windows.Forms.Label();
            this.pbSelect = new System.Windows.Forms.PictureBox();
            this.cbStyle = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(3, 4);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(14, 13);
            this.labelKey.TabIndex = 0;
            this.labelKey.Text = "#";
            // 
            // pbSelect
            // 
            this.pbSelect.BackColor = System.Drawing.Color.Red;
            this.pbSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSelect.Location = new System.Drawing.Point(33, 2);
            this.pbSelect.Name = "pbSelect";
            this.pbSelect.Size = new System.Drawing.Size(19, 19);
            this.pbSelect.TabIndex = 1;
            this.pbSelect.TabStop = false;
            this.pbSelect.Click += new System.EventHandler(this.pbSelect_Click);
            // 
            // cbStyle
            // 
            this.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStyle.FormattingEnabled = true;
            this.cbStyle.Items.AddRange(new object[] {
            "Solid",
            "Dash / 75%",
            "Dot / 50%",
            "None / 25%"});
            this.cbStyle.Location = new System.Drawing.Point(70, 1);
            this.cbStyle.Name = "cbStyle";
            this.cbStyle.Size = new System.Drawing.Size(70, 21);
            this.cbStyle.TabIndex = 1;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(145, 1);
            this.nudWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(39, 20);
            this.nudWidth.TabIndex = 2;
            this.nudWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DrawColorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.cbStyle);
            this.Controls.Add(this.pbSelect);
            this.Controls.Add(this.labelKey);
            this.Name = "DrawColorControl";
            this.Size = new System.Drawing.Size(187, 24);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.PictureBox pbSelect;
        private System.Windows.Forms.ComboBox cbStyle;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown nudWidth;
    }
}
