namespace WhereAreWe
{
    partial class GameInformationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameInformationForm));
            this.miEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateMemory = new System.Windows.Forms.Timer(this.components);
            this.labelDisabled = new System.Windows.Forms.Label();
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
            // labelDisabled
            // 
            this.labelDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDisabled.Location = new System.Drawing.Point(0, 0);
            this.labelDisabled.Name = "labelDisabled";
            this.labelDisabled.Size = new System.Drawing.Size(577, 236);
            this.labelDisabled.TabIndex = 0;
            this.labelDisabled.Text = "Please wait...";
            this.labelDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(577, 236);
            this.Controls.Add(this.labelDisabled);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "GameInformationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameInformationForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miEditPath;
        private System.Windows.Forms.Timer timerUpdateMemory;
        private System.Windows.Forms.Label labelDisabled;
    }
}