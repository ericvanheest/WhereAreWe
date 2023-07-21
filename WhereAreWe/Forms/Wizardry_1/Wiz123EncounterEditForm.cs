using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class Wiz123EncounterEditForm : CommonKeyForm
    {
        private GameNames m_game = GameNames.None;
        private WizEncounterInfo m_info;
        private List<WizMonster> m_monstersSelected = null;
        private Timer m_timerCancelMonsters = new Timer();
        private Timer m_timerCancelGroups = new Timer();
        private Font m_fontAlive;
        private Font m_fontDead;
        private bool m_bInitialized = false;
        private bool m_bUpdating = false;

        public Wiz123EncounterEditForm()
        {
            InitializeComponent();
        }

        public Wiz123EncounterEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;

            comboCondition.Items.Clear();
            for (WizCondition cond = WizCondition.Good; cond <= WizCondition.Lost; cond++)
                comboCondition.Items.Add(WizCharacter.ConditionString(cond));

            comboMonster.Items.Clear();
            comboMonster.Items.Add("<none>");

            List<WizMonster> monsters = Games.GetWizGlobals(game).GetMonsters();

            int index = 0;
            foreach(WizMonster monster in monsters)
            {
                comboMonster.Items.Add(String.Format("#{0}, {1}, {2} HP, {3} AC, {4} Exp", index, monster.ProperName, monster.HitPoints.ToString(), monster.AC, monster.Experience));
                index++;
            }

            m_timerCancelGroups.Interval = 100;
            m_timerCancelMonsters.Interval = 100;
            m_timerCancelGroups.Tick += m_timerCancelGroups_Tick;
            m_timerCancelMonsters.Tick += m_timerCancelMonsters_Tick;

            m_fontAlive = new Font(lvMonsters.Font, FontStyle.Regular);
            m_fontDead = new Font(lvMonsters.Font, FontStyle.Italic);

            CommonKeyFind += Wiz1EncounterEditForm_CommonKeyFind;
            CommonKeyNext += Wiz1EncounterEditForm_CommonKeyNext;
            CommonKeyPrevious += Wiz1EncounterEditForm_CommonKeyPrevious;

            bool bWiz5 = game == GameNames.Wizardry5;
            nudFizzleField.Visible = bWiz5;
            nudMagicScreen.Visible = bWiz5;
            labelFizzleField.Visible = bWiz5;
            labelMagicScreen.Visible = bWiz5;
            if (!bWiz5)
            {
                lvGroups.Columns[5].Width = 0;
                lvGroups.Columns[6].Width = 0;
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboMonster.Text.ToLower().Contains(tbSearch.Text.ToLower()))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void Wiz1EncounterEditForm_CommonKeyPrevious(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, false);
        }

        void Wiz1EncounterEditForm_CommonKeyNext(object sender, BoolHandlerEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void Wiz1EncounterEditForm_CommonKeyFind(object sender, EventArgs e)
        {
            tbSearch.Focus();
            tbSearch.Select();
        }

        public WizEncounterInfo Info
        {
            get
            {
                return m_info;
            }
            set
            {
                m_info = value;
                UpdateUI();
            }
        }

        public void SetMonsters(List<WizMonster> monsters)
        {
            m_monstersSelected = monsters;
            if (m_monstersSelected != null && m_monstersSelected.Count > 0)
            {
                if (m_monstersSelected[0] != null && lvGroups.Items.Count > m_monstersSelected[0].MonsterGroup)
                    lvGroups.Items[m_monstersSelected[0].MonsterGroup].Selected = true;
            }
        }

        private void AddGroup(int index, WizEncounterGroup info)
        {
            ListViewItem lvi = new ListViewItem((index).ToString());
            int iMonsterIndex = (info == null ? -1 : info.Index);

            if (index == 0)
                lvi.SubItems.Add("Player Characters");
            else if (iMonsterIndex < 0)
                lvi.SubItems.Add("<none>");
            else if (Games.IsWizardry(m_game))
                lvi.SubItems.Add(Games.GetWizGlobals(m_game).GetMonsterName(iMonsterIndex));

            if (info != null)
            {
                lvi.SubItems.Add(info.Identified ? "Yes" : "No");
                lvi.SubItems.Add(info.NumAlive.ToString());
                lvi.SubItems.Add(info.NumEnemies.ToString());
                lvi.SubItems.Add(info.MagicScreen.ToString());
                lvi.SubItems.Add(info.FizzleField.ToString());
            }
            lvi.Tag = info;
            lvGroups.Items.Add(lvi);
        }

        private void CheckInvalidValues(int index, WizEncounterRecord record)
        {
            if (record == null || index >= lvMonsters.Items.Count)
                return;

            ListViewItem lvi = lvMonsters.Items[index];

            if (index >= nudAlive.Value)
            {
                if (record.CurrentHP != 0)
                    ChangeMonster(lvi, (int)WizEncounterRecord.Property.HP, 0);
            }
            else
            {
                if (record.CurrentHP < 1)
                    ChangeMonster(lvi, (int)WizEncounterRecord.Property.HP, 1);
            }

            if (record.ArmorClass > 99)
                ChangeMonster(lvi, (int)WizEncounterRecord.Property.AC, 99);
            if (record.ArmorClass < -99)
                ChangeMonster(lvi, (int)WizEncounterRecord.Property.AC, -99);
            if (record.Initiative < -1 || record.Initiative > 10)
                ChangeMonster(lvi, (int)WizEncounterRecord.Property.Initiative, 0);

            if (record.Condition < WizCondition.Good || record.Condition > WizCondition.Lost)
                ChangeMonster(lvi, (int)WizEncounterRecord.Property.Condition, (int)WizCondition.Good, WizCharacter.ConditionDescription(WizCondition.Good));
        }

        private void AddMonster(int index, WizEncounterRecord info, bool bEnabled)
        {
            CheckInvalidValues(index, info);

            ListViewItem lvi = new ListViewItem((index+1).ToString());
            lvi.SubItems.Add(info.CurrentHP.ToString());
            lvi.SubItems.Add(info.ArmorClass.ToString());
            lvi.SubItems.Add(WizCharacter.ConditionString(info.Condition));
            lvi.SubItems.Add(info.Initiative.ToString());
            lvi.SubItems.Add(info.Victim.ToString());
            lvi.SubItems.Add(info.Silenced.ToString());
            lvi.SubItems.Add(info.Unknown1.ToString());
            lvi.SubItems.Add(info.SpellHash.ToString());
            lvi.Tag = info;
            if (!bEnabled)
                Disable(lvi);
            lvMonsters.Items.Add(lvi);
        }

        private void llEditMonster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lvGroups.SelectedItems.Count < 1)
                return;

            ListViewItem lvi = lvGroups.SelectedItems[0];
            WizEncounterGroup group = lvi.Tag as WizEncounterGroup;
            Wiz123MonsterEditForm form = new Wiz123MonsterEditForm(m_game);
            form.Monster = group.Monster;
            if (form.ShowDialog() == DialogResult.OK)
                group.MonsterChanged = true;
        }

        public void UpdateUI()
        {
            lvGroups.BeginUpdate();
            lvGroups.Items.Clear();
            for (int i = 0; i < m_info.Groups.Count; i++)
                AddGroup(i, m_info.Groups[i]);
            lvGroups.EndUpdate();
        }

        public void UpdateFromUI()
        {
            for (int i = 0; i < lvGroups.Items.Count; i++)
            {
                if (m_info.Groups.Count > i)
                    m_info.Groups[i] = lvGroups.Items[i].Tag as WizEncounterGroup;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            UpdateFromUI();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bAny = lvGroups.SelectedItems.Count > 0;
            if (bAny)
                UpdateMonsters(lvGroups.SelectedItems[0].Index);
            else
                lvMonsters.Items.Clear();
            UpdateGroupControls();
            EnableMonsterControls(false);
        }

        void m_timerCancelMonsters_Tick(object sender, EventArgs e)
        {
            EnableMonsterControls(false);
            m_timerCancelMonsters.Stop();
        }

        private void EnableGroupControls(bool bEnable)
        {
            comboMonster.Enabled = bEnable;
            nudAlive.Enabled = bEnable;
            nudTotal.Enabled = bEnable;
            cbIdentified.Enabled = bEnable;
            llEditMonster.Enabled = bEnable;
            if (!bEnable)
                EnableMonsterControls(bEnable);
        }

        private void EnableMonsterControls(bool bEnable)
        {
            nudHitPoints.Enabled = bEnable;
            nudACModifier.Enabled = bEnable;
            comboCondition.Enabled = bEnable;
            nudInitiative.Enabled = bEnable;
            nudVictim.Enabled = bEnable;
            nudInAudCnt.Enabled = bEnable;
            nudUnknown.Enabled = bEnable;
            nudSpellHash.Enabled = bEnable;
        }

        void m_timerCancelGroups_Tick(object sender, EventArgs e)
        {
            EnableGroupControls(false);
            m_timerCancelGroups.Stop();
        }

        private void UpdateGroupControls()
        {
            bool bAny = lvGroups.SelectedItems.Count > 0;
            if (!bAny)
            {
                m_timerCancelGroups.Start();
                return;
            }

            m_timerCancelGroups.Stop();
            EnableGroupControls(lvGroups.SelectedItems[0].Index != 0);

            WizEncounterGroup group = lvGroups.SelectedItems[0].Tag as WizEncounterGroup;
            if (group == null)
                return;

            Global.SetIndex(comboMonster, (int) group.Index + 1);
            m_bUpdating = true;
            Global.SetNud(nudAlive, group.NumAlive);
            Global.SetNud(nudTotal, group.NumEnemies);
            Global.SetNud(nudMagicScreen, group.MagicScreen);
            Global.SetNud(nudFizzleField, group.FizzleField);
            m_bUpdating = false;
            cbIdentified.Checked = group.Identified;
        }

        private void UpdateMonsters(int iGroup = -1)
        {
            if (iGroup == -1 && lvGroups.SelectedItems.Count > 0)
                iGroup = lvGroups.SelectedItems[0].Index;

            if (iGroup < 0 || iGroup > m_info.Groups.Count)
                return;

            WizEncounterGroup group = m_info.Groups[iGroup];

            if (group == null)
                return;

            lvMonsters.BeginUpdate();
            lvMonsters.Items.Clear();

            for (int i = 0; i < group.NumEnemies; i++)
            {
                if (i < group.Records.Count)
                {
                    WizEncounterRecord record = group.Records[i];
                    if (record != null)
                        AddMonster(i, record, i < group.NumAlive);
                }
            }

            if (m_monstersSelected != null)
            {
                foreach (WizMonster monster in m_monstersSelected)
                {
                    if (lvMonsters.Items.Count > monster.MonsterSubGroup)
                        lvMonsters.Items[monster.MonsterSubGroup].Selected = true;
                }
                m_monstersSelected = null;
                lvMonsters.Focus();
                lvMonsters.Select();
            }

            lvMonsters.EndUpdate();
        }

        private void lvMonsters_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bAny = lvMonsters.SelectedItems.Count > 0;
            if (!bAny)
            {
                m_timerCancelMonsters.Start();
                return;
            }

            m_timerCancelMonsters.Stop();
            EnableMonsterControls(true);

            WizEncounterRecord record = lvMonsters.SelectedItems[0].Tag as WizEncounterRecord;
            if (record == null)
                return;

            Global.SetIndex(comboCondition, (int)record.Condition);
            Global.SetNud(nudHitPoints, record.CurrentHP);
            Global.SetNud(nudACModifier, record.ArmorClass);
            Global.SetNud(nudInitiative, record.Initiative);
            Global.SetNud(nudVictim, record.Victim);
            Global.SetNud(nudInAudCnt, record.Silenced);
            Global.SetNud(nudUnknown, record.Unknown1);
            Global.SetNud(nudSpellHash, record.SpellHash);
        }

        private void ChangeGroup(int index, int val, string strVal = null)
        {
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                WizEncounterGroup group = lvi.Tag as WizEncounterGroup;
                if (group != null)
                {
                    if (group.ChangeValue(index, val))
                    {
                        WizMonster monster = CloneMonster(val);
                        if (monster != null)
                            group.Monster = monster;
                    }
                    lvi.SubItems[index + 1].Text = strVal == null ? val.ToString() : strVal;
                }
            }
        }

        private void ChangeMonster(int index, int val, string strVal = null)
        {
            foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                ChangeMonster(lvi, index, val, strVal);
        }

        private void ChangeMonster(ListViewItem lvi, int index, int val, string strVal = null)
        {
            WizEncounterRecord record = lvi.Tag as WizEncounterRecord;
            if (record != null)
            {
                record.ChangeValue(index, val);
                lvi.SubItems[index + 1].Text = strVal == null ? val.ToString() : strVal;
            }
        }

        private WizMonster CloneMonster(int index)
        {
            List<WizMonster> monsters = Games.GetWizGlobals(m_game).GetMonsters();
            if (index >= 0 && index < monsters.Count)
                return monsters[index].Clone() as WizMonster;
            return null;
        }

        private string MonsterName(int index)
        {
            List<WizMonster> monsters = Games.GetWizGlobals(m_game).GetMonsters();
            if (index >= 0 && index < monsters.Count)
                return monsters[index].ProperName;
            return "<none>";
        }

        private void comboMonster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count < 1 || lvGroups.SelectedItems[0].Index == 0)
                return;
            int index = comboMonster.SelectedIndex - 1;
            ChangeGroup((int)WizEncounterGroup.Property.Index, index, MonsterName(index));
        }

        private void cbIdentified_CheckedChanged(object sender, EventArgs e)
        {
            ChangeGroup((int)WizEncounterGroup.Property.Identified, cbIdentified.Checked ? 1 : 0, cbIdentified.Checked ? "Yes" : "No");
        }

        private void comboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCond = comboCondition.SelectedIndex;
            ChangeMonster((int)WizEncounterRecord.Property.Condition, iCond, WizCharacter.ConditionString((WizCondition) iCond));
        }

        private void UpdateAliveMonsters()
        {
            lvMonsters.BeginUpdate();
            for(int index = 0; index < lvMonsters.Items.Count; index++)
            {
                ListViewItem lvi = lvMonsters.Items[index];
                CheckInvalidValues(index, lvi.Tag as WizEncounterRecord);
                if (lvi.Index < nudAlive.Value)
                    Enable(lvi);
                else
                    Disable(lvi);
            }
            lvMonsters.EndUpdate();
        }

        private void Enable(ListViewItem lvi)
        {
            lvi.Font = m_fontAlive;
            lvi.ForeColor = SystemColors.ControlText;
        }

        private void Disable(ListViewItem lvi)
        {
            lvi.Font = m_fontDead;
            lvi.ForeColor = SystemColors.GrayText;
        }

        private void ChangeMagicScreen()
        {
            if (m_bUpdating)
                return;
            if (!m_bInitialized)
                return;
            ChangeGroup((int)WizEncounterGroup.Property.MagicScreen, (int)nudMagicScreen.Value);
        }

        private void ChangeFizzleField()
        {
            if (m_bUpdating)
                return;
            if (!m_bInitialized)
                return;
            ChangeGroup((int)WizEncounterGroup.Property.FizzleField, (int)nudFizzleField.Value);
        }

        private void ChangeAlive()
        {
            if (m_bUpdating)
                return;
            if (!m_bInitialized)
                return;
            if (nudAlive.Value > nudTotal.Value)
                nudTotal.Value = nudAlive.Value;
            ChangeGroup((int)WizEncounterGroup.Property.Alive, (int) nudAlive.Value);

            UpdateAliveMonsters();
        }

        private void ChangeTotal()
        {
            if (m_bUpdating)
                return;
            if (!m_bInitialized)
                return;
            if (nudTotal.Value < nudAlive.Value)
                nudAlive.Value = nudTotal.Value;
            ChangeGroup((int)WizEncounterGroup.Property.Total, (int)nudTotal.Value);
            UpdateMonsters();
            UpdateAliveMonsters();
        }
        
        private void ChangeHitPoints()
        {
            ChangeMonster((int)WizEncounterRecord.Property.HP, (int) nudHitPoints.Value);
        }

        private void ChangeACMod()
        {
            ChangeMonster((int)WizEncounterRecord.Property.AC, (int)nudACModifier.Value);
        }

        private void ChangeInitiative()
        {
            ChangeMonster((int)WizEncounterRecord.Property.Initiative, (int)nudInitiative.Value);
        }

        private void ChangeVictim()
        {
            ChangeMonster((int)WizEncounterRecord.Property.Victim, (int)nudVictim.Value);
        }

        private void ChangeInAudCnt()
        {
            ChangeMonster((int)WizEncounterRecord.Property.Silenced, (int)nudInAudCnt.Value);
        }

        private void ChangeUnknown()
        {
            ChangeMonster((int)WizEncounterRecord.Property.Unknown, (int)nudUnknown.Value);
        }

        private void ChangeSpellHash()
        {
            ChangeMonster((int)WizEncounterRecord.Property.SpellHash, (int)nudSpellHash.Value);
        }

        private void nudAlive_ValueChanged(object sender, EventArgs e) { ChangeAlive(); }
        private void nudAlive_KeyDown(object sender, KeyEventArgs e) { ChangeAlive(); }
        private void nudTotal_ValueChanged(object sender, EventArgs e) { ChangeTotal(); }
        private void nudTotal_KeyDown(object sender, KeyEventArgs e) { ChangeTotal(); }
        private void nudHitPoints_ValueChanged(object sender, EventArgs e) { ChangeHitPoints(); }
        private void nudHitPoints_KeyDown(object sender, KeyEventArgs e) { ChangeHitPoints(); }
        private void nudACModifier_ValueChanged(object sender, EventArgs e) { ChangeACMod(); }
        private void nudACModifier_KeyDown(object sender, KeyEventArgs e) { ChangeACMod(); }
        private void nudInitiative_ValueChanged(object sender, EventArgs e) { ChangeInitiative(); }
        private void nudInitiative_KeyDown(object sender, KeyEventArgs e) { ChangeInitiative(); }
        private void nudVictim_ValueChanged(object sender, EventArgs e) { ChangeVictim(); }
        private void nudVictim_KeyDown(object sender, KeyEventArgs e) { ChangeVictim(); }
        private void nudInAudCnt_ValueChanged(object sender, EventArgs e) { ChangeInAudCnt(); }
        private void nudInAudCnt_KeyDown(object sender, KeyEventArgs e) { ChangeInAudCnt(); }
        private void nudUnknown_ValueChanged(object sender, EventArgs e) { ChangeUnknown(); }
        private void nudUnknown_KeyDown(object sender, KeyEventArgs e) { ChangeUnknown(); }
        private void nudSpellHash_ValueChanged(object sender, EventArgs e) { ChangeSpellHash(); }
        private void nudSpellHash_KeyDown(object sender, KeyEventArgs e) { ChangeSpellHash(); }
        private void nudMagicScreen_ValueChanged(object sender, EventArgs e) { ChangeMagicScreen(); }
        private void nudMagicScreen_KeyDown(object sender, KeyEventArgs e) { ChangeMagicScreen(); }
        private void nudFizzleField_ValueChanged(object sender, EventArgs e) { ChangeFizzleField(); }
        private void nudFizzleField_KeyDown(object sender, KeyEventArgs e) { ChangeFizzleField(); }

        private void Wiz1EncounterEditForm_Load(object sender, EventArgs e)
        {
            m_bInitialized = true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_fontDead != null)
                    m_fontDead.Dispose();
                if (m_fontAlive != null)
                    m_fontAlive.Dispose();
            }
            base.Dispose(disposing);
        }

        private void llKillAllMonsters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem lvi in lvGroups.Items)
            {
                if (lvi.Index == 0)
                    continue;  // Group 0 is the players

                WizEncounterGroup group = lvi.Tag as WizEncounterGroup;
                if (group != null)
                    group.NumAlive = 0;

                lvi.SubItems[(int)WizEncounterGroup.Property.Alive + 1].Text = "0";
            }

            UpdateAliveMonsters();
        }
    }
}
