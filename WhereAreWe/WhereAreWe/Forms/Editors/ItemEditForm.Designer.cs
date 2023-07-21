namespace WhereAreWe
{
    partial class ItemEditForm
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
            this.comboItem = new System.Windows.Forms.ComboBox();
            this.labelCharges = new System.Windows.Forms.Label();
            this.nudCharges = new System.Windows.Forms.NumericUpDown();
            this.labelBonus = new System.Windows.Forms.Label();
            this.nudBonus = new System.Windows.Forms.NumericUpDown();
            this.labelAlignment = new System.Windows.Forms.Label();
            this.comboAlignment = new System.Windows.Forms.ComboBox();
            this.labelNoteCursed = new System.Windows.Forms.Label();
            this.btnRandom = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFindItem = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.llItemDisplayFormat = new System.Windows.Forms.LinkLabel();
            this.cbCursed = new System.Windows.Forms.CheckBox();
            this.cbIdentified = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(323, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(404, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Item";
            // 
            // comboItem
            // 
            this.comboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Location = new System.Drawing.Point(70, 39);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(409, 21);
            this.comboItem.TabIndex = 4;
            this.comboItem.SelectedIndexChanged += new System.EventHandler(this.comboItem_SelectedIndexChanged);
            // 
            // labelCharges
            // 
            this.labelCharges.AutoSize = true;
            this.labelCharges.Location = new System.Drawing.Point(8, 70);
            this.labelCharges.Name = "labelCharges";
            this.labelCharges.Size = new System.Drawing.Size(46, 13);
            this.labelCharges.TabIndex = 5;
            this.labelCharges.Text = "Charges";
            // 
            // nudCharges
            // 
            this.nudCharges.Location = new System.Drawing.Point(70, 66);
            this.nudCharges.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCharges.Name = "nudCharges";
            this.nudCharges.Size = new System.Drawing.Size(60, 20);
            this.nudCharges.TabIndex = 6;
            this.nudCharges.ValueChanged += new System.EventHandler(this.nudCharges_ValueChanged);
            // 
            // labelBonus
            // 
            this.labelBonus.AutoSize = true;
            this.labelBonus.Location = new System.Drawing.Point(141, 70);
            this.labelBonus.Name = "labelBonus";
            this.labelBonus.Size = new System.Drawing.Size(37, 13);
            this.labelBonus.TabIndex = 7;
            this.labelBonus.Text = "Bonus";
            // 
            // nudBonus
            // 
            this.nudBonus.Location = new System.Drawing.Point(184, 66);
            this.nudBonus.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.nudBonus.Name = "nudBonus";
            this.nudBonus.Size = new System.Drawing.Size(60, 20);
            this.nudBonus.TabIndex = 8;
            this.nudBonus.ValueChanged += new System.EventHandler(this.nudBonus_ValueChanged);
            // 
            // labelAlignment
            // 
            this.labelAlignment.AutoSize = true;
            this.labelAlignment.Location = new System.Drawing.Point(8, 95);
            this.labelAlignment.Name = "labelAlignment";
            this.labelAlignment.Size = new System.Drawing.Size(53, 13);
            this.labelAlignment.TabIndex = 12;
            this.labelAlignment.Text = "Alignment";
            // 
            // comboAlignment
            // 
            this.comboAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAlignment.FormattingEnabled = true;
            this.comboAlignment.Location = new System.Drawing.Point(70, 92);
            this.comboAlignment.Name = "comboAlignment";
            this.comboAlignment.Size = new System.Drawing.Size(108, 21);
            this.comboAlignment.TabIndex = 13;
            this.comboAlignment.SelectedIndexChanged += new System.EventHandler(this.comboAlignment_SelectedIndexChanged);
            // 
            // labelNoteCursed
            // 
            this.labelNoteCursed.AutoSize = true;
            this.labelNoteCursed.Location = new System.Drawing.Point(179, 95);
            this.labelNoteCursed.Name = "labelNoteCursed";
            this.labelNoteCursed.Size = new System.Drawing.Size(159, 13);
            this.labelNoteCursed.TabIndex = 14;
            this.labelNoteCursed.Text = "(63 bonus and Neutral = cursed)";
            // 
            // btnRandom
            // 
            this.btnRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRandom.Location = new System.Drawing.Point(10, 243);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 17;
            this.btnRandom.Text = "&Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Find item:";
            // 
            // tbFindItem
            // 
            this.tbFindItem.Location = new System.Drawing.Point(70, 6);
            this.tbFindItem.Name = "tbFindItem";
            this.tbFindItem.Size = new System.Drawing.Size(328, 20);
            this.tbFindItem.TabIndex = 1;
            this.tbFindItem.TextChanged += new System.EventHandler(this.tbFindItem_TextChanged);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(404, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 20);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(70, 119);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescription.Size = new System.Drawing.Size(409, 118);
            this.tbDescription.TabIndex = 16;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(7, 122);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 15;
            this.labelDescription.Text = "Description";
            // 
            // llItemDisplayFormat
            // 
            this.llItemDisplayFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llItemDisplayFormat.AutoSize = true;
            this.llItemDisplayFormat.Location = new System.Drawing.Point(406, 70);
            this.llItemDisplayFormat.Name = "llItemDisplayFormat";
            this.llItemDisplayFormat.Size = new System.Drawing.Size(76, 13);
            this.llItemDisplayFormat.TabIndex = 11;
            this.llItemDisplayFormat.TabStop = true;
            this.llItemDisplayFormat.Text = "Display Format";
            this.llItemDisplayFormat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llItemDisplayFormat_LinkClicked);
            // 
            // cbCursed
            // 
            this.cbCursed.AutoSize = true;
            this.cbCursed.Location = new System.Drawing.Point(261, 68);
            this.cbCursed.Name = "cbCursed";
            this.cbCursed.Size = new System.Drawing.Size(59, 17);
            this.cbCursed.TabIndex = 9;
            this.cbCursed.Text = "&Cursed";
            this.cbCursed.UseVisualStyleBackColor = true;
            // 
            // cbIdentified
            // 
            this.cbIdentified.AutoSize = true;
            this.cbIdentified.Location = new System.Drawing.Point(326, 68);
            this.cbIdentified.Name = "cbIdentified";
            this.cbIdentified.Size = new System.Drawing.Size(69, 17);
            this.cbIdentified.TabIndex = 10;
            this.cbIdentified.Text = "&Identified";
            this.cbIdentified.UseVisualStyleBackColor = true;
            // 
            // ItemEditForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(491, 278);
            this.Controls.Add(this.cbIdentified);
            this.Controls.Add(this.cbCursed);
            this.Controls.Add(this.llItemDisplayFormat);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbFindItem);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.nudBonus);
            this.Controls.Add(this.nudCharges);
            this.Controls.Add(this.comboAlignment);
            this.Controls.Add(this.comboItem);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelAlignment);
            this.Controls.Add(this.labelNoteCursed);
            this.Controls.Add(this.labelBonus);
            this.Controls.Add(this.labelCharges);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(281, 155);
            this.Name = "ItemEditForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Item";
            this.Load += new System.EventHandler(this.ItemEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboItem;
        private System.Windows.Forms.Label labelCharges;
        private System.Windows.Forms.NumericUpDown nudCharges;
        private System.Windows.Forms.Label labelBonus;
        private System.Windows.Forms.NumericUpDown nudBonus;
        private System.Windows.Forms.Label labelAlignment;
        private System.Windows.Forms.ComboBox comboAlignment;
        private System.Windows.Forms.Label labelNoteCursed;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFindItem;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.LinkLabel llItemDisplayFormat;
        private System.Windows.Forms.CheckBox cbCursed;
        private System.Windows.Forms.CheckBox cbIdentified;
    }
}