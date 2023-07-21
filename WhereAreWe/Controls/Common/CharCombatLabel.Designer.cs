namespace WhereAreWe
{
    partial class CharCombatLabel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharCombatLabel));
            this.labelName = new System.Windows.Forms.Label();
            this.labelHP = new System.Windows.Forms.Label();
            this.labelSP = new System.Windows.Forms.Label();
            this.labelCondition = new System.Windows.Forms.Label();
            this.pbMelee = new System.Windows.Forms.PictureBox();
            this.tipCombatInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbMelee)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(45, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(117, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "MAX_NAME_LENGTH";
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.Location = new System.Drawing.Point(157, 0);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(31, 13);
            this.labelHP.TabIndex = 0;
            this.labelHP.Text = "9999";
            // 
            // labelSP
            // 
            this.labelSP.AutoSize = true;
            this.labelSP.Location = new System.Drawing.Point(194, 0);
            this.labelSP.Name = "labelSP";
            this.labelSP.Size = new System.Drawing.Size(31, 13);
            this.labelSP.TabIndex = 0;
            this.labelSP.Text = "9999";
            // 
            // labelCondition
            // 
            this.labelCondition.Location = new System.Drawing.Point(-8, 0);
            this.labelCondition.Margin = new System.Windows.Forms.Padding(0);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(47, 13);
            this.labelCondition.TabIndex = 0;
            this.labelCondition.Text = "disease";
            this.labelCondition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbMelee
            // 
            this.pbMelee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbMelee.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbMelee.InitialImage")));
            this.pbMelee.Location = new System.Drawing.Point(38, 3);
            this.pbMelee.Name = "pbMelee";
            this.pbMelee.Size = new System.Drawing.Size(8, 8);
            this.pbMelee.TabIndex = 1;
            this.pbMelee.TabStop = false;
            // 
            // CharCombatLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMelee);
            this.Controls.Add(this.labelSP);
            this.Controls.Add(this.labelHP);
            this.Controls.Add(this.labelCondition);
            this.Controls.Add(this.labelName);
            this.Name = "CharCombatLabel";
            this.Size = new System.Drawing.Size(230, 14);
            ((System.ComponentModel.ISupportInitialize)(this.pbMelee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.Label labelSP;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.PictureBox pbMelee;
        private System.Windows.Forms.ToolTip tipCombatInfo;
    }
}
