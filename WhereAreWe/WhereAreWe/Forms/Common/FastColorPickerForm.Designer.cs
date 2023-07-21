namespace WhereAreWe
{
    partial class FastColorPickerForm
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
            if (m_bmp != null)
                m_bmp.Dispose();
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
            this.pbColors = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).BeginInit();
            this.SuspendLayout();
            // 
            // pbColors
            // 
            this.pbColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbColors.Location = new System.Drawing.Point(0, 0);
            this.pbColors.Name = "pbColors";
            this.pbColors.Size = new System.Drawing.Size(104, 324);
            this.pbColors.TabIndex = 0;
            this.pbColors.TabStop = false;
            this.pbColors.Click += new System.EventHandler(this.pbColors_Click);
            this.pbColors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbColors_MouseMove);
            // 
            // FastColorPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(104, 324);
            this.ControlBox = false;
            this.Controls.Add(this.pbColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(106, 183);
            this.Name = "FastColorPickerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Color Selection";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FastColorPickerForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbColors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbColors;

    }
}