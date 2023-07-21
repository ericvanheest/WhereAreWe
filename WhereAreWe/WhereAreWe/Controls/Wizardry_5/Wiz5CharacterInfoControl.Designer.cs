namespace WhereAreWe
{
    partial class Wiz5CharacterInfoControl
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
            this.labelSwimmingHeader = new System.Windows.Forms.Label();
            this.labelSwimming = new WhereAreWe.EditableAttributeLabel();
            this.labelDeathsHeader = new System.Windows.Forms.Label();
            this.labelDeaths = new WhereAreWe.EditableAttributeLabel();
            this.SuspendLayout();
            // 
            // labelSwimmingHeader
            // 
            this.labelSwimmingHeader.AutoSize = true;
            this.labelSwimmingHeader.Location = new System.Drawing.Point(242, 94);
            this.labelSwimmingHeader.Name = "labelSwimmingHeader";
            this.labelSwimmingHeader.Size = new System.Drawing.Size(54, 13);
            this.labelSwimmingHeader.TabIndex = 49;
            this.labelSwimmingHeader.Text = "Swimming";
            this.labelSwimmingHeader.MouseEnter += new System.EventHandler(this.labelSwimmingHeader_MouseEnter);
            this.labelSwimmingHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelSwimming
            // 
            this.labelSwimming.AutoSize = true;
            this.labelSwimming.Location = new System.Drawing.Point(300, 94);
            this.labelSwimming.Name = "labelSwimming";
            this.labelSwimming.Size = new System.Drawing.Size(37, 13);
            this.labelSwimming.TabIndex = 49;
            this.labelSwimming.Text = "SWIM";
            this.labelSwimming.MouseEnter += new System.EventHandler(this.labelSwimming_MouseEnter);
            this.labelSwimming.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelDeathsHeader
            // 
            this.labelDeathsHeader.AutoSize = true;
            this.labelDeathsHeader.Location = new System.Drawing.Point(6, 131);
            this.labelDeathsHeader.Name = "labelDeathsHeader";
            this.labelDeathsHeader.Size = new System.Drawing.Size(44, 13);
            this.labelDeathsHeader.TabIndex = 49;
            this.labelDeathsHeader.Text = "Deaths:";
            this.labelDeathsHeader.MouseEnter += new System.EventHandler(this.labelSwimmingHeader_MouseEnter);
            this.labelDeathsHeader.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // labelDeaths
            // 
            this.labelDeaths.AutoSize = true;
            this.labelDeaths.Location = new System.Drawing.Point(59, 131);
            this.labelDeaths.Name = "labelDeaths";
            this.labelDeaths.Size = new System.Drawing.Size(25, 13);
            this.labelDeaths.TabIndex = 49;
            this.labelDeaths.Text = "RIP";
            this.labelDeaths.MouseEnter += new System.EventHandler(this.labelSwimming_MouseEnter);
            this.labelDeaths.MouseLeave += new System.EventHandler(this.Stat_MouseLeave);
            // 
            // Wiz5CharacterInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDeaths);
            this.Controls.Add(this.labelSwimming);
            this.Controls.Add(this.labelDeathsHeader);
            this.Controls.Add(this.labelSwimmingHeader);
            this.Name = "Wiz5CharacterInfoControl";
            this.Controls.SetChildIndex(this.labelSwimmingHeader, 0);
            this.Controls.SetChildIndex(this.labelDeathsHeader, 0);
            this.Controls.SetChildIndex(this.labelSwimming, 0);
            this.Controls.SetChildIndex(this.labelDeaths, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSwimmingHeader;
        private EditableAttributeLabel labelSwimming;
        private System.Windows.Forms.Label labelDeathsHeader;
        private EditableAttributeLabel labelDeaths;
    }
}
