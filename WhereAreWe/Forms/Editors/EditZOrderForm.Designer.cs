namespace WhereAreWe
{
    partial class EditZOrderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditZOrderForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lvZOrder = new WhereAreWe.DraggableListView();
            this.chForm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.llResetToDefault = new System.Windows.Forms.LinkLabel();
            this.cbIgnoreZOrderList = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(161, 396);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(242, 396);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // lvZOrder
            // 
            this.lvZOrder.AllowDrop = true;
            this.lvZOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvZOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chForm});
            this.lvZOrder.FullRowSelect = true;
            this.lvZOrder.LabelEdit = true;
            this.lvZOrder.Location = new System.Drawing.Point(1, 72);
            this.lvZOrder.Name = "lvZOrder";
            this.lvZOrder.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvZOrder.Size = new System.Drawing.Size(325, 295);
            this.lvZOrder.TabIndex = 1;
            this.lvZOrder.UseCompatibleStateImageBehavior = false;
            this.lvZOrder.View = System.Windows.Forms.View.Details;
            this.lvZOrder.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvZOrder_ColumnClick);
            this.lvZOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvZOrder_KeyDown);
            // 
            // chForm
            // 
            this.chForm.Text = "Form Name";
            this.chForm.Width = 302;
            // 
            // llResetToDefault
            // 
            this.llResetToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llResetToDefault.AutoSize = true;
            this.llResetToDefault.Location = new System.Drawing.Point(12, 401);
            this.llResetToDefault.Name = "llResetToDefault";
            this.llResetToDefault.Size = new System.Drawing.Size(82, 13);
            this.llResetToDefault.TabIndex = 3;
            this.llResetToDefault.TabStop = true;
            this.llResetToDefault.Text = "&Reset to default";
            this.llResetToDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llResetToDefault_LinkClicked);
            // 
            // cbIgnoreZOrderList
            // 
            this.cbIgnoreZOrderList.AutoSize = true;
            this.cbIgnoreZOrderList.Location = new System.Drawing.Point(5, 373);
            this.cbIgnoreZOrderList.Name = "cbIgnoreZOrderList";
            this.cbIgnoreZOrderList.Size = new System.Drawing.Size(275, 17);
            this.cbIgnoreZOrderList.TabIndex = 2;
            this.cbIgnoreZOrderList.Text = "&Always place new windows at the top (ignore this list)";
            this.cbIgnoreZOrderList.UseVisualStyleBackColor = true;
            this.cbIgnoreZOrderList.CheckedChanged += new System.EventHandler(this.cbIgnoreZOrderList_CheckedChanged);
            // 
            // EditZOrderForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(327, 430);
            this.Controls.Add(this.cbIgnoreZOrderList);
            this.Controls.Add(this.llResetToDefault);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvZOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(335, 300);
            this.Name = "EditZOrderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Organize Form Z-Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraggableListView lvZOrder;
        private System.Windows.Forms.ColumnHeader chForm;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel llResetToDefault;
        private System.Windows.Forms.CheckBox cbIgnoreZOrderList;
    }
}