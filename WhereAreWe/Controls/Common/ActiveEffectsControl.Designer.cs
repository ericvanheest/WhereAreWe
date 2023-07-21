namespace WhereAreWe
{
    partial class ActiveEffectsControl
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
            this.lvActiveEffects = new System.Windows.Forms.ListView();
            this.chActiveEffect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmActiveEffects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miActiveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmActiveEffects.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvActiveEffects
            // 
            this.lvActiveEffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActiveEffect,
            this.chValue});
            this.lvActiveEffects.ContextMenuStrip = this.cmActiveEffects;
            this.lvActiveEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvActiveEffects.FullRowSelect = true;
            this.lvActiveEffects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvActiveEffects.Location = new System.Drawing.Point(0, 0);
            this.lvActiveEffects.MultiSelect = false;
            this.lvActiveEffects.Name = "lvActiveEffects";
            this.lvActiveEffects.Size = new System.Drawing.Size(196, 158);
            this.lvActiveEffects.TabIndex = 24;
            this.lvActiveEffects.UseCompatibleStateImageBehavior = false;
            this.lvActiveEffects.View = System.Windows.Forms.View.Details;
            this.lvActiveEffects.SizeChanged += new System.EventHandler(this.lvActiveEffects_SizeChanged);
            // 
            // chActiveEffect
            // 
            this.chActiveEffect.Text = "Active Effect";
            this.chActiveEffect.Width = 132;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 39;
            // 
            // cmActiveEffects
            // 
            this.cmActiveEffects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miActiveEdit,
            this.miActiveAdd,
            this.miActiveRemove});
            this.cmActiveEffects.Name = "cmForbiddenSpells";
            this.cmActiveEffects.ShowImageMargin = false;
            this.cmActiveEffects.Size = new System.Drawing.Size(112, 70);
            this.cmActiveEffects.Opening += new System.ComponentModel.CancelEventHandler(this.cmActiveEffects_Opening);
            // 
            // miActiveEdit
            // 
            this.miActiveEdit.Name = "miActiveEdit";
            this.miActiveEdit.Size = new System.Drawing.Size(111, 22);
            this.miActiveEdit.Text = "&Edit Value";
            this.miActiveEdit.Click += new System.EventHandler(this.miActiveEdit_Click);
            // 
            // miActiveAdd
            // 
            this.miActiveAdd.Name = "miActiveAdd";
            this.miActiveAdd.Size = new System.Drawing.Size(111, 22);
            this.miActiveAdd.Text = "&Add/Remove";
            this.miActiveAdd.Click += new System.EventHandler(this.miActiveAdd_Click);
            // 
            // miActiveRemove
            // 
            this.miActiveRemove.Name = "miActiveRemove";
            this.miActiveRemove.Size = new System.Drawing.Size(111, 22);
            this.miActiveRemove.Text = "&Delete";
            this.miActiveRemove.Click += new System.EventHandler(this.miActiveRemove_Click);
            // 
            // ActiveEffectsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.lvActiveEffects);
            this.Name = "ActiveEffectsControl";
            this.Size = new System.Drawing.Size(196, 158);
            this.cmActiveEffects.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvActiveEffects;
        private System.Windows.Forms.ColumnHeader chActiveEffect;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ContextMenuStrip cmActiveEffects;
        private System.Windows.Forms.ToolStripMenuItem miActiveEdit;
        private System.Windows.Forms.ToolStripMenuItem miActiveAdd;
        private System.Windows.Forms.ToolStripMenuItem miActiveRemove;

    }
}
