namespace WhereAreWe
{
    partial class TrainingAssistantForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingAssistantForm));
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateMemory = new System.Windows.Forms.Timer(this.components);
            this.panelAssistant = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelAssistant.SuspendLayout();
            this.SuspendLayout();
            // 
            // miEditPath
            // 
            this.miEditPath.Name = "miEditPath";
            this.miEditPath.Size = new System.Drawing.Size(32, 19);
            // 
            // timerUpdateMemory
            // 
            this.timerUpdateMemory.Interval = 50;
            this.timerUpdateMemory.Tick += new System.EventHandler(this.timerUpdateMemory_Tick);
            // 
            // panelAssistant
            // 
            this.panelAssistant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAssistant.Controls.Add(this.btnClose);
            this.panelAssistant.Location = new System.Drawing.Point(3, 3);
            this.panelAssistant.Name = "panelAssistant";
            this.panelAssistant.Size = new System.Drawing.Size(377, 204);
            this.panelAssistant.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TrainingAssistantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(382, 208);
            this.Controls.Add(this.panelAssistant);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "TrainingAssistantForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Training Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainingAssistantForm_FormClosing);
            this.Load += new System.EventHandler(this.TrainingAssistantForm_Load);
            this.panelAssistant.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.Timer timerUpdateMemory;
        private System.Windows.Forms.Panel panelAssistant;
        private System.Windows.Forms.Button btnClose;
    }
}