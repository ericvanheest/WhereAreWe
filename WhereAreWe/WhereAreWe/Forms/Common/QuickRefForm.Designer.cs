namespace WhereAreWe
{
    partial class QuickRefForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lvChars = new WhereAreWe.DBListView();
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHPMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSPMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMelee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRanged = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIntellect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPersonality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEndurance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAccuracy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLuck = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCondition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmCharacter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMonitorHP = new System.Windows.Forms.ToolStripMenuItem();
            this.miCharCureAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miMonitorStats = new System.Windows.Forms.ToolStripMenuItem();
            this.cmColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miColumnsIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsName = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsClass = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsHP = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsMaxHP = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsSP = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsMaxSP = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsAC = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsMelee = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsRanged = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsMight = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsIntellect = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsPersonality = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsEndurance = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsSpeed = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsAccuracy = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsLuck = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsCondition = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsAge = new System.Windows.Forms.ToolStripMenuItem();
            this.miColumnsReset = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.chItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.miColumnsItems = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCharacter.SuspendLayout();
            this.cmColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1007, 139);
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
            this.btnOK.Location = new System.Drawing.Point(1088, 139);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvChars
            // 
            this.lvChars.AllowColumnReorder = true;
            this.lvChars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvChars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNumber,
            this.chName,
            this.chClass,
            this.chLevel,
            this.chHP,
            this.chHPMax,
            this.chSP,
            this.chSPMax,
            this.chAC,
            this.chMelee,
            this.chRanged,
            this.chMight,
            this.chIntellect,
            this.chPersonality,
            this.chEndurance,
            this.chSpeed,
            this.chAccuracy,
            this.chLuck,
            this.chAge,
            this.chItems,
            this.chCondition});
            this.lvChars.ContextMenuStrip = this.cmCharacter;
            this.lvChars.FullRowSelect = true;
            this.lvChars.HideSelection = false;
            this.lvChars.Location = new System.Drawing.Point(0, 2);
            this.lvChars.Name = "lvChars";
            this.lvChars.ScrollPosition = new System.Drawing.Point(0, 0);
            this.lvChars.Size = new System.Drawing.Size(1170, 196);
            this.lvChars.TabIndex = 0;
            this.lvChars.UseCompatibleStateImageBehavior = false;
            this.lvChars.View = System.Windows.Forms.View.Details;
            this.lvChars.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvChars_ColumnClick);
            this.lvChars.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvChars_ColumnReordered);
            // 
            // chNumber
            // 
            this.chNumber.Text = "#";
            this.chNumber.Width = 26;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 76;
            // 
            // chClass
            // 
            this.chClass.Text = "Class";
            this.chClass.Width = 71;
            // 
            // chLevel
            // 
            this.chLevel.Text = "Level";
            this.chLevel.Width = 42;
            // 
            // chHP
            // 
            this.chHP.Text = "HP";
            this.chHP.Width = 50;
            // 
            // chHPMax
            // 
            this.chHPMax.Text = "MaxHP";
            this.chHPMax.Width = 50;
            // 
            // chSP
            // 
            this.chSP.Text = "SP";
            this.chSP.Width = 50;
            // 
            // chSPMax
            // 
            this.chSPMax.Text = "MaxSP";
            this.chSPMax.Width = 50;
            // 
            // chAC
            // 
            this.chAC.Text = "AC";
            this.chAC.Width = 34;
            // 
            // chMelee
            // 
            this.chMelee.Text = "Melee";
            // 
            // chRanged
            // 
            this.chRanged.Text = "Ranged";
            // 
            // chMight
            // 
            this.chMight.Text = "Mgt";
            this.chMight.Width = 32;
            // 
            // chIntellect
            // 
            this.chIntellect.Text = "Int";
            this.chIntellect.Width = 32;
            // 
            // chPersonality
            // 
            this.chPersonality.Text = "Per";
            this.chPersonality.Width = 32;
            // 
            // chEndurance
            // 
            this.chEndurance.Text = "End";
            this.chEndurance.Width = 32;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Spd";
            this.chSpeed.Width = 32;
            // 
            // chAccuracy
            // 
            this.chAccuracy.Text = "Acy";
            this.chAccuracy.Width = 32;
            // 
            // chLuck
            // 
            this.chLuck.Text = "Lck";
            this.chLuck.Width = 32;
            // 
            // chAge
            // 
            this.chAge.Text = "Age";
            this.chAge.Width = 32;
            // 
            // chCondition
            // 
            this.chCondition.Text = "Condition";
            this.chCondition.Width = 268;
            // 
            // cmCharacter
            // 
            this.cmCharacter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMonitorHP,
            this.miCharCureAll,
            this.miMonitorStats});
            this.cmCharacter.Name = "cmDebug";
            this.cmCharacter.ShowImageMargin = false;
            this.cmCharacter.Size = new System.Drawing.Size(114, 70);
            this.cmCharacter.Opening += new System.ComponentModel.CancelEventHandler(this.cmCharacter_Opening);
            // 
            // miMonitorHP
            // 
            this.miMonitorHP.Name = "miMonitorHP";
            this.miMonitorHP.Size = new System.Drawing.Size(113, 22);
            this.miMonitorHP.Text = "&Monitor HP";
            this.miMonitorHP.Click += new System.EventHandler(this.miMonitorHP_Click);
            // 
            // miCharCureAll
            // 
            this.miCharCureAll.Name = "miCharCureAll";
            this.miCharCureAll.Size = new System.Drawing.Size(113, 22);
            this.miCharCureAll.Text = "&Cure All";
            this.miCharCureAll.Click += new System.EventHandler(this.miCharCureAll_Click);
            // 
            // miMonitorStats
            // 
            this.miMonitorStats.Name = "miMonitorStats";
            this.miMonitorStats.Size = new System.Drawing.Size(113, 22);
            this.miMonitorStats.Text = "Monitor &Stats";
            this.miMonitorStats.Click += new System.EventHandler(this.miMonitorStats_Click);
            // 
            // cmColumns
            // 
            this.cmColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miColumnsIndex,
            this.miColumnsName,
            this.miColumnsClass,
            this.miColumnsLevel,
            this.miColumnsHP,
            this.miColumnsMaxHP,
            this.miColumnsSP,
            this.miColumnsMaxSP,
            this.miColumnsAC,
            this.miColumnsMelee,
            this.miColumnsRanged,
            this.miColumnsMight,
            this.miColumnsIntellect,
            this.miColumnsPersonality,
            this.miColumnsEndurance,
            this.miColumnsSpeed,
            this.miColumnsAccuracy,
            this.miColumnsLuck,
            this.miColumnsAge,
            this.miColumnsItems,
            this.miColumnsCondition,
            this.miColumnsReset});
            this.cmColumns.Name = "cmHeaders";
            this.cmColumns.ShowCheckMargin = true;
            this.cmColumns.ShowImageMargin = false;
            this.cmColumns.Size = new System.Drawing.Size(176, 510);
            this.cmColumns.Opening += new System.ComponentModel.CancelEventHandler(this.cmColumns_Opening);
            // 
            // miColumnsIndex
            // 
            this.miColumnsIndex.Checked = true;
            this.miColumnsIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsIndex.Name = "miColumnsIndex";
            this.miColumnsIndex.Size = new System.Drawing.Size(175, 22);
            this.miColumnsIndex.Text = "Index";
            this.miColumnsIndex.Click += new System.EventHandler(this.miColumnsIndex_Click);
            // 
            // miColumnsName
            // 
            this.miColumnsName.Checked = true;
            this.miColumnsName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsName.Name = "miColumnsName";
            this.miColumnsName.Size = new System.Drawing.Size(175, 22);
            this.miColumnsName.Text = "&Name";
            this.miColumnsName.Click += new System.EventHandler(this.miColumnsName_Click);
            // 
            // miColumnsClass
            // 
            this.miColumnsClass.Checked = true;
            this.miColumnsClass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsClass.Name = "miColumnsClass";
            this.miColumnsClass.Size = new System.Drawing.Size(175, 22);
            this.miColumnsClass.Text = "&Class";
            this.miColumnsClass.Click += new System.EventHandler(this.miColumnsClass_Click);
            // 
            // miColumnsLevel
            // 
            this.miColumnsLevel.Checked = true;
            this.miColumnsLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsLevel.Name = "miColumnsLevel";
            this.miColumnsLevel.Size = new System.Drawing.Size(175, 22);
            this.miColumnsLevel.Text = "&Level";
            this.miColumnsLevel.Click += new System.EventHandler(this.miColumnsLevel_Click);
            // 
            // miColumnsHP
            // 
            this.miColumnsHP.Checked = true;
            this.miColumnsHP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsHP.Name = "miColumnsHP";
            this.miColumnsHP.Size = new System.Drawing.Size(175, 22);
            this.miColumnsHP.Text = "&Hit Points";
            this.miColumnsHP.Click += new System.EventHandler(this.miColumnsHP_Click);
            // 
            // miColumnsMaxHP
            // 
            this.miColumnsMaxHP.Checked = true;
            this.miColumnsMaxHP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsMaxHP.Name = "miColumnsMaxHP";
            this.miColumnsMaxHP.Size = new System.Drawing.Size(175, 22);
            this.miColumnsMaxHP.Text = "&Maximum Hit Points";
            this.miColumnsMaxHP.Click += new System.EventHandler(this.miColumnsMaxHP_Click);
            // 
            // miColumnsSP
            // 
            this.miColumnsSP.Checked = true;
            this.miColumnsSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsSP.Name = "miColumnsSP";
            this.miColumnsSP.Size = new System.Drawing.Size(175, 22);
            this.miColumnsSP.Text = "Spell Points";
            this.miColumnsSP.Click += new System.EventHandler(this.miColumnsSP_Click);
            // 
            // miColumnsMaxSP
            // 
            this.miColumnsMaxSP.Checked = true;
            this.miColumnsMaxSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsMaxSP.Name = "miColumnsMaxSP";
            this.miColumnsMaxSP.Size = new System.Drawing.Size(175, 22);
            this.miColumnsMaxSP.Text = "Ma&ximum Spell Points";
            this.miColumnsMaxSP.Click += new System.EventHandler(this.miColumnsMaxSP_Click);
            // 
            // miColumnsAC
            // 
            this.miColumnsAC.Checked = true;
            this.miColumnsAC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsAC.Name = "miColumnsAC";
            this.miColumnsAC.Size = new System.Drawing.Size(175, 22);
            this.miColumnsAC.Text = "&Armor Class";
            this.miColumnsAC.Click += new System.EventHandler(this.miColumnsAC_Click);
            // 
            // miColumnsMelee
            // 
            this.miColumnsMelee.Checked = true;
            this.miColumnsMelee.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsMelee.Name = "miColumnsMelee";
            this.miColumnsMelee.Size = new System.Drawing.Size(175, 22);
            this.miColumnsMelee.Text = "M&elee Damage";
            this.miColumnsMelee.Click += new System.EventHandler(this.miColumnsMelee_Click);
            // 
            // miColumnsRanged
            // 
            this.miColumnsRanged.Checked = true;
            this.miColumnsRanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsRanged.Name = "miColumnsRanged";
            this.miColumnsRanged.Size = new System.Drawing.Size(175, 22);
            this.miColumnsRanged.Text = "&Ranged Damage";
            this.miColumnsRanged.Click += new System.EventHandler(this.miColumnsRanged_Click);
            // 
            // miColumnsMight
            // 
            this.miColumnsMight.Checked = true;
            this.miColumnsMight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsMight.Name = "miColumnsMight";
            this.miColumnsMight.Size = new System.Drawing.Size(175, 22);
            this.miColumnsMight.Text = "Mi&ght";
            this.miColumnsMight.Click += new System.EventHandler(this.miColumnsMight_Click);
            // 
            // miColumnsIntellect
            // 
            this.miColumnsIntellect.Checked = true;
            this.miColumnsIntellect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsIntellect.Name = "miColumnsIntellect";
            this.miColumnsIntellect.Size = new System.Drawing.Size(175, 22);
            this.miColumnsIntellect.Text = "&Intellect";
            this.miColumnsIntellect.Click += new System.EventHandler(this.miColumnsIntellect_Click);
            // 
            // miColumnsPersonality
            // 
            this.miColumnsPersonality.Checked = true;
            this.miColumnsPersonality.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsPersonality.Name = "miColumnsPersonality";
            this.miColumnsPersonality.Size = new System.Drawing.Size(175, 22);
            this.miColumnsPersonality.Text = "&Personality";
            this.miColumnsPersonality.Click += new System.EventHandler(this.miColumnsPersonality_Click);
            // 
            // miColumnsEndurance
            // 
            this.miColumnsEndurance.Checked = true;
            this.miColumnsEndurance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsEndurance.Name = "miColumnsEndurance";
            this.miColumnsEndurance.Size = new System.Drawing.Size(175, 22);
            this.miColumnsEndurance.Text = "E&ndurance";
            this.miColumnsEndurance.Click += new System.EventHandler(this.miColumnsEndurance_Click);
            // 
            // miColumnsSpeed
            // 
            this.miColumnsSpeed.Checked = true;
            this.miColumnsSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsSpeed.Name = "miColumnsSpeed";
            this.miColumnsSpeed.Size = new System.Drawing.Size(175, 22);
            this.miColumnsSpeed.Text = "Spee&d";
            this.miColumnsSpeed.Click += new System.EventHandler(this.miColumnsSpeed_Click);
            // 
            // miColumnsAccuracy
            // 
            this.miColumnsAccuracy.Checked = true;
            this.miColumnsAccuracy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsAccuracy.Name = "miColumnsAccuracy";
            this.miColumnsAccuracy.Size = new System.Drawing.Size(175, 22);
            this.miColumnsAccuracy.Text = "Acc&uracy";
            this.miColumnsAccuracy.Click += new System.EventHandler(this.miColumnsAccuracy_Click);
            // 
            // miColumnsLuck
            // 
            this.miColumnsLuck.Checked = true;
            this.miColumnsLuck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsLuck.Name = "miColumnsLuck";
            this.miColumnsLuck.Size = new System.Drawing.Size(175, 22);
            this.miColumnsLuck.Text = "Luc&k";
            this.miColumnsLuck.Click += new System.EventHandler(this.miColumnsLuck_Click);
            // 
            // miColumnsCondition
            // 
            this.miColumnsCondition.Checked = true;
            this.miColumnsCondition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsCondition.Name = "miColumnsCondition";
            this.miColumnsCondition.Size = new System.Drawing.Size(175, 22);
            this.miColumnsCondition.Text = "C&ondition";
            this.miColumnsCondition.Click += new System.EventHandler(this.miColumnsCondition_Click);
            // 
            // miColumnsAge
            // 
            this.miColumnsAge.Checked = true;
            this.miColumnsAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsAge.Name = "miColumnsAge";
            this.miColumnsAge.Size = new System.Drawing.Size(175, 22);
            this.miColumnsAge.Text = "Magical Age Modifier";
            this.miColumnsAge.Click += new System.EventHandler(this.miColumnsAge_Click);
            // 
            // miColumnsReset
            // 
            this.miColumnsReset.Name = "miColumnsReset";
            this.miColumnsReset.Size = new System.Drawing.Size(175, 22);
            this.miColumnsReset.Text = "Reset Columns";
            this.miColumnsReset.Click += new System.EventHandler(this.miColumnsReset_Click);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 250;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // chItems
            // 
            this.chItems.Text = "Items";
            this.chItems.Width = 38;
            // 
            // miColumnsItems
            // 
            this.miColumnsItems.Checked = true;
            this.miColumnsItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miColumnsItems.Name = "miColumnsItems";
            this.miColumnsItems.Size = new System.Drawing.Size(175, 22);
            this.miColumnsItems.Text = "Items";
            this.miColumnsItems.Click += new System.EventHandler(this.miColumnsItems_Click);
            // 
            // QuickRefForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1170, 198);
            this.Controls.Add(this.lvChars);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 100);
            this.Name = "QuickRefForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Character Quick Reference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuickRefForm_FormClosing);
            this.cmCharacter.ResumeLayout(false);
            this.cmColumns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private WhereAreWe.DBListView lvChars;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chClass;
        private System.Windows.Forms.ColumnHeader chLevel;
        private System.Windows.Forms.ColumnHeader chHP;
        private System.Windows.Forms.ColumnHeader chHPMax;
        private System.Windows.Forms.ColumnHeader chSP;
        private System.Windows.Forms.ColumnHeader chSPMax;
        private System.Windows.Forms.ColumnHeader chAC;
        private System.Windows.Forms.ColumnHeader chMelee;
        private System.Windows.Forms.ColumnHeader chRanged;
        private System.Windows.Forms.ColumnHeader chCondition;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ColumnHeader chMight;
        private System.Windows.Forms.ColumnHeader chIntellect;
        private System.Windows.Forms.ColumnHeader chPersonality;
        private System.Windows.Forms.ColumnHeader chEndurance;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chAccuracy;
        private System.Windows.Forms.ColumnHeader chLuck;
        private System.Windows.Forms.ContextMenuStrip cmColumns;
        private System.Windows.Forms.ToolStripMenuItem miColumnsName;
        private System.Windows.Forms.ToolStripMenuItem miColumnsClass;
        private System.Windows.Forms.ToolStripMenuItem miColumnsLevel;
        private System.Windows.Forms.ToolStripMenuItem miColumnsHP;
        private System.Windows.Forms.ToolStripMenuItem miColumnsMaxHP;
        private System.Windows.Forms.ToolStripMenuItem miColumnsSP;
        private System.Windows.Forms.ToolStripMenuItem miColumnsMaxSP;
        private System.Windows.Forms.ToolStripMenuItem miColumnsAC;
        private System.Windows.Forms.ToolStripMenuItem miColumnsMelee;
        private System.Windows.Forms.ToolStripMenuItem miColumnsRanged;
        private System.Windows.Forms.ToolStripMenuItem miColumnsMight;
        private System.Windows.Forms.ToolStripMenuItem miColumnsIntellect;
        private System.Windows.Forms.ToolStripMenuItem miColumnsPersonality;
        private System.Windows.Forms.ToolStripMenuItem miColumnsEndurance;
        private System.Windows.Forms.ToolStripMenuItem miColumnsSpeed;
        private System.Windows.Forms.ToolStripMenuItem miColumnsAccuracy;
        private System.Windows.Forms.ToolStripMenuItem miColumnsLuck;
        private System.Windows.Forms.ToolStripMenuItem miColumnsCondition;
        private System.Windows.Forms.ToolStripMenuItem miColumnsReset;
        private System.Windows.Forms.ToolStripMenuItem miColumnsIndex;
        private System.Windows.Forms.ColumnHeader chAge;
        private System.Windows.Forms.ToolStripMenuItem miColumnsAge;
        private System.Windows.Forms.ContextMenuStrip cmCharacter;
        private System.Windows.Forms.ToolStripMenuItem miMonitorHP;
        private System.Windows.Forms.ToolStripMenuItem miCharCureAll;
        private System.Windows.Forms.ToolStripMenuItem miMonitorStats;
        private System.Windows.Forms.ColumnHeader chItems;
        private System.Windows.Forms.ToolStripMenuItem miColumnsItems;
    }
}