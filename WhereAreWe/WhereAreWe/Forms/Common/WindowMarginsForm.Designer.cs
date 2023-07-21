namespace WhereAreWe
{
    partial class WindowMarginsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowMarginsForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudBottom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAllMarginsSame = new System.Windows.Forms.CheckBox();
            this.cbUseExtendedWindowRect = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(135, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Top:";
            // 
            // nudTop
            // 
            this.nudTop.Location = new System.Drawing.Point(126, 23);
            this.nudTop.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(51, 20);
            this.nudTop.TabIndex = 1;
            this.nudTop.ValueChanged += new System.EventHandler(this.nudTop_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Bottom:";
            // 
            // nudBottom
            // 
            this.nudBottom.Location = new System.Drawing.Point(126, 84);
            this.nudBottom.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudBottom.Name = "nudBottom";
            this.nudBottom.Size = new System.Drawing.Size(51, 20);
            this.nudBottom.TabIndex = 7;
            this.nudBottom.ValueChanged += new System.EventHandler(this.nudBottom_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Left:";
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(50, 53);
            this.nudLeft.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(51, 20);
            this.nudLeft.TabIndex = 3;
            this.nudLeft.ValueChanged += new System.EventHandler(this.nudLeft_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(161, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Right:";
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(202, 53);
            this.nudRight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(51, 20);
            this.nudRight.TabIndex = 5;
            this.nudRight.ValueChanged += new System.EventHandler(this.nudRight_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(12, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(279, 71);
            this.label9.TabIndex = 0;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAllMarginsSame);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nudBottom);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nudRight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudLeft);
            this.groupBox1.Controls.Add(this.nudTop);
            this.groupBox1.Location = new System.Drawing.Point(15, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window Snapping Margins (in pixels)";
            // 
            // cbAllMarginsSame
            // 
            this.cbAllMarginsSame.AutoSize = true;
            this.cbAllMarginsSame.Checked = true;
            this.cbAllMarginsSame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllMarginsSame.Location = new System.Drawing.Point(8, 119);
            this.cbAllMarginsSame.Name = "cbAllMarginsSame";
            this.cbAllMarginsSame.Size = new System.Drawing.Size(192, 17);
            this.cbAllMarginsSame.TabIndex = 8;
            this.cbAllMarginsSame.Text = "&Force all margins to the same value";
            this.cbAllMarginsSame.UseVisualStyleBackColor = true;
            this.cbAllMarginsSame.CheckedChanged += new System.EventHandler(this.cbAllMarginsSame_CheckedChanged);
            // 
            // cbUseExtendedWindowRect
            // 
            this.cbUseExtendedWindowRect.AutoSize = true;
            this.cbUseExtendedWindowRect.Checked = true;
            this.cbUseExtendedWindowRect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseExtendedWindowRect.Location = new System.Drawing.Point(15, 228);
            this.cbUseExtendedWindowRect.Name = "cbUseExtendedWindowRect";
            this.cbUseExtendedWindowRect.Size = new System.Drawing.Size(183, 17);
            this.cbUseExtendedWindowRect.TabIndex = 8;
            this.cbUseExtendedWindowRect.Text = "Use &extended windows rectangle";
            this.cbUseExtendedWindowRect.UseVisualStyleBackColor = true;
            this.cbUseExtendedWindowRect.CheckedChanged += new System.EventHandler(this.cbAllMarginsSame_CheckedChanged);
            // 
            // WindowMarginsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(303, 285);
            this.Controls.Add(this.cbUseExtendedWindowRect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(228, 226);
            this.Name = "WindowMarginsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Window Margins";
            this.Load += new System.EventHandler(this.WindowMarginsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudBottom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbAllMarginsSame;
        private System.Windows.Forms.CheckBox cbUseExtendedWindowRect;
    }
}