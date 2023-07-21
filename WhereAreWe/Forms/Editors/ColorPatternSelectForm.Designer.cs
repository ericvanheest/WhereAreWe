namespace WhereAreWe
{
    partial class ColorPatternSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPatternSelectForm));
            this.pbSample = new System.Windows.Forms.PictureBox();
            this.llColor = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.comboPattern = new System.Windows.Forms.ComboBox();
            this.llBackground = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSample
            // 
            this.pbSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSample.Location = new System.Drawing.Point(12, 12);
            this.pbSample.Name = "pbSample";
            this.pbSample.Size = new System.Drawing.Size(220, 64);
            this.pbSample.TabIndex = 0;
            this.pbSample.TabStop = false;
            this.pbSample.SizeChanged += new System.EventHandler(this.pbSample_SizeChanged);
            this.pbSample.Click += new System.EventHandler(this.pbSample_Click);
            // 
            // llColor
            // 
            this.llColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llColor.AutoSize = true;
            this.llColor.Location = new System.Drawing.Point(9, 79);
            this.llColor.Name = "llColor";
            this.llColor.Size = new System.Drawing.Size(70, 13);
            this.llColor.TabIndex = 0;
            this.llColor.TabStop = true;
            this.llColor.Text = "Change color";
            this.llColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llColor_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(157, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(76, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // comboPattern
            // 
            this.comboPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPattern.FormattingEnabled = true;
            this.comboPattern.Location = new System.Drawing.Point(12, 99);
            this.comboPattern.Name = "comboPattern";
            this.comboPattern.Size = new System.Drawing.Size(220, 21);
            this.comboPattern.TabIndex = 2;
            this.comboPattern.SelectedIndexChanged += new System.EventHandler(this.comboPattern_SelectedIndexChanged);
            // 
            // llBackground
            // 
            this.llBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llBackground.AutoSize = true;
            this.llBackground.Location = new System.Drawing.Point(141, 79);
            this.llBackground.Name = "llBackground";
            this.llBackground.Size = new System.Drawing.Size(91, 13);
            this.llBackground.TabIndex = 1;
            this.llBackground.TabStop = true;
            this.llBackground.Text = "Background color";
            this.llBackground.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.llBackground.Visible = false;
            this.llBackground.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llBackground_LinkClicked);
            // 
            // ColorPatternSelectForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(244, 166);
            this.Controls.Add(this.comboPattern);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.llBackground);
            this.Controls.Add(this.llColor);
            this.Controls.Add(this.pbSample);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorPatternSelectForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a color and pattern";
            this.Load += new System.EventHandler(this.ColorPatternSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSample;
        private System.Windows.Forms.LinkLabel llColor;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox comboPattern;
        private System.Windows.Forms.LinkLabel llBackground;
    }
}