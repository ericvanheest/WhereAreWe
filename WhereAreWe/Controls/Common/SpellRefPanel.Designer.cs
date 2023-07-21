namespace WhereAreWe
{
    partial class SpellRefPanel
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
            this.lvSpells = new WhereAreWe.DraggableListView();
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelSpellType = new System.Windows.Forms.Label();
            this.chDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvSpells
            // 
            this.lvSpells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSpells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNumber,
            this.chName,
            this.chCost,
            this.chWhen,
            this.chTarget,
            this.chDuration,
            this.chDescription});
            this.lvSpells.FullRowSelect = true;
            this.lvSpells.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSpells.HideSelection = false;
            this.lvSpells.Location = new System.Drawing.Point(0, 16);
            this.lvSpells.MultiSelect = false;
            this.lvSpells.Name = "lvSpells";
            this.lvSpells.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvSpells.Size = new System.Drawing.Size(851, 264);
            this.lvSpells.TabIndex = 2;
            this.lvSpells.TipDelay = 700;
            this.lvSpells.TipDuration = 30000;
            this.lvSpells.UseCompatibleStateImageBehavior = false;
            this.lvSpells.View = System.Windows.Forms.View.Details;
            // 
            // chNumber
            // 
            this.chNumber.Text = "Num";
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 92;
            // 
            // chCost
            // 
            this.chCost.Text = "Cost";
            // 
            // chWhen
            // 
            this.chWhen.Text = "When";
            this.chWhen.Width = 71;
            // 
            // chTarget
            // 
            this.chTarget.Text = "Target";
            this.chTarget.Width = 84;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 457;
            // 
            // labelSpellType
            // 
            this.labelSpellType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpellType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSpellType.Location = new System.Drawing.Point(0, 0);
            this.labelSpellType.Name = "labelSpellType";
            this.labelSpellType.Size = new System.Drawing.Size(850, 17);
            this.labelSpellType.TabIndex = 3;
            this.labelSpellType.Text = "Spells";
            this.labelSpellType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chDuration
            // 
            this.chDuration.Text = "Duration";
            // 
            // SpellRefPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSpells);
            this.Controls.Add(this.labelSpellType);
            this.Name = "SpellRefPanel";
            this.Size = new System.Drawing.Size(851, 280);
            this.ResumeLayout(false);

        }

        #endregion

        private DraggableListView lvSpells;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chCost;
        private System.Windows.Forms.ColumnHeader chWhen;
        private System.Windows.Forms.ColumnHeader chTarget;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.Label labelSpellType;
        private System.Windows.Forms.ColumnHeader chDuration;
    }
}
