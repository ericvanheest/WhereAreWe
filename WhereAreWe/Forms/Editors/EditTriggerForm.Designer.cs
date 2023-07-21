namespace WhereAreWe
{
    partial class EditTriggerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboWho = new System.Windows.Forms.ComboBox();
            this.tbWhoValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboWhat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboWhen = new System.Windows.Forms.ComboBox();
            this.nudDifference = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.comboWhich = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboDo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboTo = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.llColor = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbWhichValue = new System.Windows.Forms.TextBox();
            this.labelColorSample = new System.Windows.Forms.Label();
            this.tbWhatValue = new System.Windows.Forms.TextBox();
            this.tbToValue = new System.Windows.Forms.TextBox();
            this.comboDifference = new System.Windows.Forms.ComboBox();
            this.labelAnd = new System.Windows.Forms.Label();
            this.comboWhichBetween = new System.Windows.Forms.ComboBox();
            this.tbWhichValueBetween = new System.Windows.Forms.TextBox();
            this.nudDifferenceBetween = new System.Windows.Forms.NumericUpDown();
            this.comboDifferenceBetween = new System.Windows.Forms.ComboBox();
            this.labelColorToSample = new System.Windows.Forms.Label();
            this.llColorTo = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudDifference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDifferenceBetween)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Who:";
            // 
            // comboWho
            // 
            this.comboWho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWho.FormattingEnabled = true;
            this.comboWho.Location = new System.Drawing.Point(82, 6);
            this.comboWho.Name = "comboWho";
            this.comboWho.Size = new System.Drawing.Size(152, 21);
            this.comboWho.TabIndex = 1;
            this.comboWho.SelectedIndexChanged += new System.EventHandler(this.comboWho_SelectedIndexChanged);
            // 
            // tbWhoValue
            // 
            this.tbWhoValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhoValue.Enabled = false;
            this.tbWhoValue.Location = new System.Drawing.Point(242, 6);
            this.tbWhoValue.Name = "tbWhoValue";
            this.tbWhoValue.Size = new System.Drawing.Size(315, 20);
            this.tbWhoValue.TabIndex = 2;
            this.tbWhoValue.TextChanged += new System.EventHandler(this.tbWhoValue_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "What:";
            // 
            // comboWhat
            // 
            this.comboWhat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWhat.FormattingEnabled = true;
            this.comboWhat.Location = new System.Drawing.Point(82, 34);
            this.comboWhat.Name = "comboWhat";
            this.comboWhat.Size = new System.Drawing.Size(152, 21);
            this.comboWhat.TabIndex = 4;
            this.comboWhat.SelectedIndexChanged += new System.EventHandler(this.comboWhat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "When:";
            // 
            // comboWhen
            // 
            this.comboWhen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWhen.FormattingEnabled = true;
            this.comboWhen.Location = new System.Drawing.Point(82, 63);
            this.comboWhen.Name = "comboWhen";
            this.comboWhen.Size = new System.Drawing.Size(152, 21);
            this.comboWhen.TabIndex = 7;
            this.comboWhen.SelectedIndexChanged += new System.EventHandler(this.comboWhen_SelectedIndexChanged);
            // 
            // nudDifference
            // 
            this.nudDifference.Enabled = false;
            this.nudDifference.Location = new System.Drawing.Point(242, 63);
            this.nudDifference.Name = "nudDifference";
            this.nudDifference.Size = new System.Drawing.Size(69, 20);
            this.nudDifference.TabIndex = 8;
            this.nudDifference.ValueChanged += new System.EventHandler(this.nudPercent_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Which:";
            // 
            // comboWhich
            // 
            this.comboWhich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWhich.FormattingEnabled = true;
            this.comboWhich.Location = new System.Drawing.Point(82, 91);
            this.comboWhich.Name = "comboWhich";
            this.comboWhich.Size = new System.Drawing.Size(152, 21);
            this.comboWhich.TabIndex = 11;
            this.comboWhich.SelectedIndexChanged += new System.EventHandler(this.comboWhich_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Do:";
            // 
            // comboDo
            // 
            this.comboDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDo.FormattingEnabled = true;
            this.comboDo.Location = new System.Drawing.Point(82, 173);
            this.comboDo.Name = "comboDo";
            this.comboDo.Size = new System.Drawing.Size(152, 21);
            this.comboDo.TabIndex = 14;
            this.comboDo.SelectedIndexChanged += new System.EventHandler(this.comboDo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "To what:";
            // 
            // comboTo
            // 
            this.comboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTo.FormattingEnabled = true;
            this.comboTo.Location = new System.Drawing.Point(82, 220);
            this.comboTo.Name = "comboTo";
            this.comboTo.Size = new System.Drawing.Size(152, 21);
            this.comboTo.TabIndex = 18;
            this.comboTo.SelectedIndexChanged += new System.EventHandler(this.comboTo_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(401, 304);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(482, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // llColor
            // 
            this.llColor.AutoSize = true;
            this.llColor.Location = new System.Drawing.Point(240, 176);
            this.llColor.Name = "llColor";
            this.llColor.Size = new System.Drawing.Size(31, 13);
            this.llColor.TabIndex = 15;
            this.llColor.TabStop = true;
            this.llColor.Text = "Color";
            this.llColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llColor_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Description:";
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(82, 251);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(475, 47);
            this.tbDescription.TabIndex = 21;
            // 
            // tbWhichValue
            // 
            this.tbWhichValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhichValue.Enabled = false;
            this.tbWhichValue.Location = new System.Drawing.Point(242, 91);
            this.tbWhichValue.Name = "tbWhichValue";
            this.tbWhichValue.Size = new System.Drawing.Size(315, 20);
            this.tbWhichValue.TabIndex = 12;
            this.tbWhichValue.TextChanged += new System.EventHandler(this.tbWhichValue_TextChanged);
            // 
            // labelColorSample
            // 
            this.labelColorSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColorSample.Location = new System.Drawing.Point(297, 173);
            this.labelColorSample.Name = "labelColorSample";
            this.labelColorSample.Size = new System.Drawing.Size(260, 18);
            this.labelColorSample.TabIndex = 16;
            this.labelColorSample.Text = "FORE on BACK";
            this.labelColorSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorSample.Click += new System.EventHandler(this.labelColorSample_Click);
            // 
            // tbWhatValue
            // 
            this.tbWhatValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhatValue.Enabled = false;
            this.tbWhatValue.Location = new System.Drawing.Point(242, 34);
            this.tbWhatValue.Name = "tbWhatValue";
            this.tbWhatValue.Size = new System.Drawing.Size(315, 20);
            this.tbWhatValue.TabIndex = 5;
            this.tbWhatValue.TextChanged += new System.EventHandler(this.tbWhatValue_TextChanged);
            // 
            // tbToValue
            // 
            this.tbToValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbToValue.Enabled = false;
            this.tbToValue.Location = new System.Drawing.Point(243, 220);
            this.tbToValue.Name = "tbToValue";
            this.tbToValue.Size = new System.Drawing.Size(315, 20);
            this.tbToValue.TabIndex = 19;
            this.tbToValue.TextChanged += new System.EventHandler(this.tbToValue_TextChanged);
            // 
            // comboDifference
            // 
            this.comboDifference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDifference.FormattingEnabled = true;
            this.comboDifference.Location = new System.Drawing.Point(317, 62);
            this.comboDifference.Name = "comboDifference";
            this.comboDifference.Size = new System.Drawing.Size(121, 21);
            this.comboDifference.TabIndex = 9;
            this.comboDifference.SelectedIndexChanged += new System.EventHandler(this.comboDifference_SelectedIndexChanged);
            // 
            // labelAnd
            // 
            this.labelAnd.AutoSize = true;
            this.labelAnd.Location = new System.Drawing.Point(156, 120);
            this.labelAnd.Name = "labelAnd";
            this.labelAnd.Size = new System.Drawing.Size(29, 13);
            this.labelAnd.TabIndex = 10;
            this.labelAnd.Text = "And:";
            // 
            // comboWhichBetween
            // 
            this.comboWhichBetween.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWhichBetween.FormattingEnabled = true;
            this.comboWhichBetween.Location = new System.Drawing.Point(159, 144);
            this.comboWhichBetween.Name = "comboWhichBetween";
            this.comboWhichBetween.Size = new System.Drawing.Size(152, 21);
            this.comboWhichBetween.TabIndex = 11;
            this.comboWhichBetween.SelectedIndexChanged += new System.EventHandler(this.comboWhichBetween_SelectedIndexChanged);
            // 
            // tbWhichValueBetween
            // 
            this.tbWhichValueBetween.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhichValueBetween.Enabled = false;
            this.tbWhichValueBetween.Location = new System.Drawing.Point(317, 146);
            this.tbWhichValueBetween.Name = "tbWhichValueBetween";
            this.tbWhichValueBetween.Size = new System.Drawing.Size(240, 20);
            this.tbWhichValueBetween.TabIndex = 12;
            this.tbWhichValueBetween.TextChanged += new System.EventHandler(this.tbWhichValueBetween_TextChanged);
            // 
            // nudDifferenceBetween
            // 
            this.nudDifferenceBetween.Enabled = false;
            this.nudDifferenceBetween.Location = new System.Drawing.Point(242, 118);
            this.nudDifferenceBetween.Name = "nudDifferenceBetween";
            this.nudDifferenceBetween.Size = new System.Drawing.Size(69, 20);
            this.nudDifferenceBetween.TabIndex = 8;
            this.nudDifferenceBetween.ValueChanged += new System.EventHandler(this.nudDifferenceBetween_ValueChanged);
            // 
            // comboDifferenceBetween
            // 
            this.comboDifferenceBetween.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDifferenceBetween.FormattingEnabled = true;
            this.comboDifferenceBetween.Location = new System.Drawing.Point(317, 117);
            this.comboDifferenceBetween.Name = "comboDifferenceBetween";
            this.comboDifferenceBetween.Size = new System.Drawing.Size(121, 21);
            this.comboDifferenceBetween.TabIndex = 9;
            this.comboDifferenceBetween.SelectedIndexChanged += new System.EventHandler(this.comboDifferenceBetween_SelectedIndexChanged_1);
            // 
            // labelColorToSample
            // 
            this.labelColorToSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColorToSample.Location = new System.Drawing.Point(297, 196);
            this.labelColorToSample.Name = "labelColorToSample";
            this.labelColorToSample.Size = new System.Drawing.Size(260, 18);
            this.labelColorToSample.TabIndex = 16;
            this.labelColorToSample.Text = "FORE on BACK";
            this.labelColorToSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorToSample.Click += new System.EventHandler(this.labelColorToSample_Click);
            // 
            // llColorTo
            // 
            this.llColorTo.AutoSize = true;
            this.llColorTo.Location = new System.Drawing.Point(240, 199);
            this.llColorTo.Name = "llColorTo";
            this.llColorTo.Size = new System.Drawing.Size(47, 13);
            this.llColorTo.TabIndex = 15;
            this.llColorTo.TabStop = true;
            this.llColorTo.Text = "To Color";
            this.llColorTo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llColorTo_LinkClicked);
            // 
            // EditTriggerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 339);
            this.Controls.Add(this.comboDifferenceBetween);
            this.Controls.Add(this.comboDifference);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.llColorTo);
            this.Controls.Add(this.llColor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudDifferenceBetween);
            this.Controls.Add(this.nudDifference);
            this.Controls.Add(this.tbWhichValueBetween);
            this.Controls.Add(this.tbWhichValue);
            this.Controls.Add(this.tbToValue);
            this.Controls.Add(this.tbWhatValue);
            this.Controls.Add(this.tbWhoValue);
            this.Controls.Add(this.comboTo);
            this.Controls.Add(this.comboWhichBetween);
            this.Controls.Add(this.comboDo);
            this.Controls.Add(this.comboWhich);
            this.Controls.Add(this.comboWhen);
            this.Controls.Add(this.comboWhat);
            this.Controls.Add(this.comboWho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelColorToSample);
            this.Controls.Add(this.labelColorSample);
            this.Controls.Add(this.labelAnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(400, 267);
            this.Name = "EditTriggerForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Trigger";
            ((System.ComponentModel.ISupportInitialize)(this.nudDifference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDifferenceBetween)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboWho;
        private System.Windows.Forms.TextBox tbWhoValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboWhat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboWhen;
        private System.Windows.Forms.NumericUpDown nudDifference;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboWhich;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboDo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel llColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbWhichValue;
        private System.Windows.Forms.Label labelColorSample;
        private System.Windows.Forms.TextBox tbWhatValue;
        private System.Windows.Forms.TextBox tbToValue;
        private System.Windows.Forms.ComboBox comboDifference;
        private System.Windows.Forms.Label labelAnd;
        private System.Windows.Forms.ComboBox comboWhichBetween;
        private System.Windows.Forms.TextBox tbWhichValueBetween;
        private System.Windows.Forms.NumericUpDown nudDifferenceBetween;
        private System.Windows.Forms.ComboBox comboDifferenceBetween;
        private System.Windows.Forms.Label labelColorToSample;
        private System.Windows.Forms.LinkLabel llColorTo;
    }
}