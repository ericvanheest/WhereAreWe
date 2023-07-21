namespace WhereAreWe
{
    partial class DropTrashForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudGold = new System.Windows.Forms.NumericUpDown();
            this.comboDropChoice = new System.Windows.Forms.ComboBox();
            this.labelAttribute = new System.Windows.Forms.Label();
            this.cbAllCharacters = new System.Windows.Forms.CheckBox();
            this.lvDiscarded = new WhereAreWe.TipListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCharacter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbDiscards = new System.Windows.Forms.GroupBox();
            this.comboClass = new System.Windows.Forms.ComboBox();
            this.comboItemType = new System.Windows.Forms.ComboBox();
            this.comboMaterial = new System.Windows.Forms.ComboBox();
            this.nudBonus = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudGold)).BeginInit();
            this.gbDiscards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(428, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(347, 342);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Drop all items that";
            // 
            // nudGold
            // 
            this.nudGold.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudGold.Location = new System.Drawing.Point(295, 5);
            this.nudGold.Maximum = new decimal(new int[] {
            -294967296,
            0,
            0,
            0});
            this.nudGold.Name = "nudGold";
            this.nudGold.Size = new System.Drawing.Size(65, 20);
            this.nudGold.TabIndex = 2;
            this.nudGold.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudGold.ValueChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // comboDropChoice
            // 
            this.comboDropChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDropChoice.FormattingEnabled = true;
            this.comboDropChoice.Location = new System.Drawing.Point(109, 4);
            this.comboDropChoice.Name = "comboDropChoice";
            this.comboDropChoice.Size = new System.Drawing.Size(180, 21);
            this.comboDropChoice.TabIndex = 1;
            this.comboDropChoice.SelectedIndexChanged += new System.EventHandler(this.comboDropChoice_SelectedIndexChanged);
            // 
            // labelAttribute
            // 
            this.labelAttribute.AutoSize = true;
            this.labelAttribute.Location = new System.Drawing.Point(366, 9);
            this.labelAttribute.Name = "labelAttribute";
            this.labelAttribute.Size = new System.Drawing.Size(29, 13);
            this.labelAttribute.TabIndex = 3;
            this.labelAttribute.Text = "Gold";
            // 
            // cbAllCharacters
            // 
            this.cbAllCharacters.AutoSize = true;
            this.cbAllCharacters.Checked = true;
            this.cbAllCharacters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllCharacters.Location = new System.Drawing.Point(407, 8);
            this.cbAllCharacters.Name = "cbAllCharacters";
            this.cbAllCharacters.Size = new System.Drawing.Size(90, 17);
            this.cbAllCharacters.TabIndex = 4;
            this.cbAllCharacters.Text = "&All characters";
            this.cbAllCharacters.UseVisualStyleBackColor = true;
            this.cbAllCharacters.CheckedChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // lvDiscarded
            // 
            this.lvDiscarded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chCharacter,
            this.chValue});
            this.lvDiscarded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDiscarded.FullRowSelect = true;
            this.lvDiscarded.Location = new System.Drawing.Point(3, 16);
            this.lvDiscarded.Name = "lvDiscarded";
            this.lvDiscarded.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvDiscarded.Size = new System.Drawing.Size(485, 286);
            this.lvDiscarded.TabIndex = 0;
            this.lvDiscarded.TipDelay = 1000;
            this.lvDiscarded.TipDuration = 30000;
            this.lvDiscarded.UseCompatibleStateImageBehavior = false;
            this.lvDiscarded.View = System.Windows.Forms.View.Details;
            this.lvDiscarded.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvDiscarded_ColumnClick);
            this.lvDiscarded.DoubleClick += new System.EventHandler(this.lvDiscarded_DoubleClick);
            // 
            // chItem
            // 
            this.chItem.Text = "Item";
            this.chItem.Width = 323;
            // 
            // chCharacter
            // 
            this.chCharacter.Text = "Character";
            this.chCharacter.Width = 93;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 49;
            // 
            // gbDiscards
            // 
            this.gbDiscards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDiscards.Controls.Add(this.lvDiscarded);
            this.gbDiscards.Location = new System.Drawing.Point(12, 31);
            this.gbDiscards.Name = "gbDiscards";
            this.gbDiscards.Size = new System.Drawing.Size(491, 305);
            this.gbDiscards.TabIndex = 5;
            this.gbDiscards.TabStop = false;
            this.gbDiscards.Text = "Items that will be discarded";
            // 
            // comboClass
            // 
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(507, 31);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(106, 21);
            this.comboClass.TabIndex = 8;
            this.comboClass.Visible = false;
            this.comboClass.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // comboItemType
            // 
            this.comboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboItemType.FormattingEnabled = true;
            this.comboItemType.Items.AddRange(new object[] {
            "Weapon",
            "Armor",
            "Accessory",
            "Miscellaneous",
            "Charge-based"});
            this.comboItemType.Location = new System.Drawing.Point(507, 58);
            this.comboItemType.Name = "comboItemType";
            this.comboItemType.Size = new System.Drawing.Size(106, 21);
            this.comboItemType.TabIndex = 8;
            this.comboItemType.Visible = false;
            this.comboItemType.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // comboMaterial
            // 
            this.comboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterial.FormattingEnabled = true;
            this.comboMaterial.Items.AddRange(new object[] {
            "Wooden",
            "Leather",
            "Brass",
            "Bronze",
            "None",
            "Glass",
            "Coral",
            "Crystal",
            "Iron",
            "Lapis",
            "Pearl",
            "Silver",
            "Amber",
            "Steel",
            "Ebony",
            "Gold",
            "Quartz",
            "Platinum",
            "Ruby",
            "Emerald",
            "Sapphire",
            "Diamond",
            "Obsidian"});
            this.comboMaterial.Location = new System.Drawing.Point(507, 85);
            this.comboMaterial.Name = "comboMaterial";
            this.comboMaterial.Size = new System.Drawing.Size(106, 21);
            this.comboMaterial.TabIndex = 8;
            this.comboMaterial.Visible = false;
            this.comboMaterial.SelectedIndexChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // nudBonus
            // 
            this.nudBonus.Location = new System.Drawing.Point(506, 112);
            this.nudBonus.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBonus.Name = "nudBonus";
            this.nudBonus.Size = new System.Drawing.Size(65, 20);
            this.nudBonus.TabIndex = 2;
            this.nudBonus.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBonus.Visible = false;
            this.nudBonus.ValueChanged += new System.EventHandler(this.Control_ValueChanged);
            // 
            // DropTrashForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(515, 377);
            this.Controls.Add(this.comboMaterial);
            this.Controls.Add(this.nudBonus);
            this.Controls.Add(this.comboItemType);
            this.Controls.Add(this.comboClass);
            this.Controls.Add(this.gbDiscards);
            this.Controls.Add(this.cbAllCharacters);
            this.Controls.Add(this.comboDropChoice);
            this.Controls.Add(this.nudGold);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelAttribute);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(490, 261);
            this.Name = "DropTrashForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Drop Trash";
            this.Load += new System.EventHandler(this.DropTrashForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudGold)).EndInit();
            this.gbDiscards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudGold;
        private System.Windows.Forms.ComboBox comboDropChoice;
        private System.Windows.Forms.Label labelAttribute;
        private System.Windows.Forms.CheckBox cbAllCharacters;
        private WhereAreWe.TipListView lvDiscarded;
        private System.Windows.Forms.ColumnHeader chCharacter;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.GroupBox gbDiscards;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.ComboBox comboClass;
        private System.Windows.Forms.ComboBox comboItemType;
        private System.Windows.Forms.ComboBox comboMaterial;
        private System.Windows.Forms.NumericUpDown nudBonus;
    }
}