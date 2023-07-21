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
    public partial class Wiz1EncounterEditForm : CommonKeyForm
    {
        private Wiz1EncounterInfo m_info;
        private List<Wiz1Monster> m_monstersSelected = null;
        private Timer m_timerCancelMonsters = new Timer();
        private Timer m_timerCancelGroups = new Timer();
        private Font m_fontAlive;
        private Font m_fontDead;
        private bool m_bInitialized = false;

        public Wiz1EncounterEditForm()
        {
            InitializeComponent();

            comboCondition.Items.Clear();
            for (Wiz1Condition cond = Wiz1Condition.Good; cond <= Wiz1Condition.Lost; cond++)
                comboCondition.Items.Add(Wiz1Character.ConditionString(cond));

            comboMonster.Items.Clear();
            comboMonster.Items.Add("<none>");
            for (int index = 0; index < Wiz1.Monsters.Count; index++)
            {
                Wiz1Monster monster = Wiz1.Monsters[index];
                comboMonster.Items.Add(String.Format("#{0}, {1}, {2} HP, {3} AC, {4} Exp", index, monster.ProperName, monster.HitPoints.ToString(), monster.AC, monster.Experience));
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
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboMonster.Text.ToLower().Contains(tbSearch.Text.ToLower()))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void Wiz1EncounterEditForm_CommonKeyPrevious()
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, false);
        }

        void Wiz1EncounterEditForm_CommonKeyNext(bool bIncludeCurrent)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void Wiz1EncounterEditForm_CommonKeyFind()
        {
            tbSearch.Focus();
            tbSearch.Select();
        }

        public Wiz1EncounterInfo Info
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

        public void SetMonsters(List<Wiz1Monster> monsters)
        {
            m_monstersSelected = monsters;
            if (m_monstersSelected != null && m_monstersSelected.Count > 0)
            {
                if (lvGroups.Items.Count > m_monstersSelected[0].MonsterGroup)
                    lvGroups.Items[m_monstersSelected[0].MonsterGroup].Selected = true;
            }
        }

        private void AddGroup(int index, Wiz1EncounterInfo.Group info)
        {
            ListViewItem lvi = new ListViewItem((index+1).ToString());
            if ((int) info.Index < 0)
                lvi.SubItems.Add("<none>");
            else
                lvi.SubItems.Add((int)info.Index < Wiz1.Monsters.Count ? Wiz1.Monsters[(int)info.Index].ProperName : "<invalid>");
            lvi.SubItems.Add(info.Identified ? "Yes" : "No");
            lvi.SubItems.Add(info.NumAlive.ToString());
            lvi.SubItems.Add(info.NumEnemies.ToString());
            lvi.Tag = info;
            lvGroups.Items.Add(lvi);
        }

        private void CheckInvalidValues(int index, Wiz1EncounterInfo.Record record)
        {
            if (record == null || index >= lvMonsters.Items.Count)
                return;

            ListViewItem lvi = lvMonsters.Items[index];

            if (index >= nudAlive.Value)
            {
                if (record.CurrentHP != 0)
                    ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.HP, 0);
            }
            else
            {
                if (record.CurrentHP < 1)
                    ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.HP, 1);
            }

            if (record.ArmorClass > 99)
                ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.AC, 99);
            if (record.ArmorClass < -99)
                ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.AC, -99);
            if (record.Initiative < -1 || record.Initiative > 10)
                ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.Initiative, 0);

            if (record.Condition < Wiz1Condition.Good || record.Condition > Wiz1Condition.Lost)
                ChangeMonster(lvi, (int)Wiz1EncounterInfo.Record.Property.Condition, (int)Wiz1Condition.Good, Wiz1Character.ConditionString(Wiz1Condition.Good));
        }

        private void AddMonster(int index, Wiz1EncounterInfo.Record info, bool bEnabled)
        {
            CheckInvalidValues(index, info);

            ListViewItem lvi = new ListViewItem((index+1).ToString());
            lvi.SubItems.Add(info.CurrentHP.ToString());
            lvi.SubItems.Add(info.ArmorClass.ToString());
            lvi.SubItems.Add(Wiz1Character.ConditionString(info.Condition));
            lvi.SubItems.Add(info.Initiative.ToString());
            lvi.SubItems.Add(info.Victim.ToString());
            lvi.SubItems.Add(info.InAudCnt.ToString());
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
            Wiz1EncounterInfo.Group group = lvi.Tag as Wiz1EncounterInfo.Group;
            Wiz1MonsterEditForm form = new Wiz1MonsterEditForm();
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
            EnableGroupControls(true);

            Wiz1EncounterInfo.Group group = lvGroups.SelectedItems[0].Tag as Wiz1EncounterInfo.Group;
            if (group == null)
                return;

            Global.SetIndex(comboMonster, (int) group.Index + 1);
            Global.SetNud(nudTotal, group.NumEnemies);
            Global.SetNud(nudAlive, group.NumAlive);
            cbIdentified.Checked = group.Identified;
        }

        private void UpdateMonsters(int iGroup = -1)
        {
            if (iGroup == -1 && lvGroups.SelectedItems.Count > 0)
                iGroup = lvGroups.SelectedItems[0].Index;

            if (iGroup < 0 || iGroup >= m_info.Groups.Count)
                return;

            lvMonsters.BeginUpdate();
            lvMonsters.Items.Clear();

            Wiz1EncounterInfo.Group group = m_info.Groups[iGroup];

            for (int i = 0; i < group.NumEnemies; i++)
            {
                if (i < group.Records.Count)
                {
                    Wiz1EncounterInfo.Record record = group.Records[i];
                    if (record != null)
                        AddMonster(i, record, i < group.NumAlive);
                }
            }

            if (m_monstersSelected != null)
            {
                foreach (Wiz1Monster monster in m_monstersSelected)
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

            Wiz1EncounterInfo.Record record = lvMonsters.SelectedItems[0].Tag as Wiz1EncounterInfo.Record;
            if (record == null)
                return;

            Global.SetIndex(comboCondition, (int)record.Condition);
            Global.SetNud(nudHitPoints, record.CurrentHP);
            Global.SetNud(nudACModifier, record.ArmorClass);
            Global.SetNud(nudInitiative, record.Initiative);
            Global.SetNud(nudVictim, record.Victim);
            Global.SetNud(nudInAudCnt, record.InAudCnt);
            Global.SetNud(nudUnknown, record.Unknown1);
            Global.SetNud(nudSpellHash, record.SpellHash);
        }

        private void ChangeGroup(int index, int val, string strVal = null)
        {
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                Wiz1EncounterInfo.Group group = lvi.Tag as Wiz1EncounterInfo.Group;
                if (group != null)
                {
                    group.ChangeValue(index, val);
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
            Wiz1EncounterInfo.Record record = lvi.Tag as Wiz1EncounterInfo.Record;
            if (record != null)
            {
                record.ChangeValue(index, val);
                lvi.SubItems[index + 1].Text = strVal == null ? val.ToString() : strVal;
            }
        }

        private void comboMonster_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboMonster.SelectedIndex - 1;
            ChangeGroup((int)Wiz1EncounterInfo.Group.Property.Index, index, index >= 0 && index < Wiz1.Monsters.Count ? Wiz1.Monsters[index].ProperName : "<none>");
        }

        private void cbIdentified_CheckedChanged(object sender, EventArgs e)
        {
            ChangeGroup((int)Wiz1EncounterInfo.Group.Property.Identified, cbIdentified.Checked ? 1 : 0, cbIdentified.Checked ? "Yes" : "No");
        }

        private void comboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCond = comboCondition.SelectedIndex;
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.Condition, iCond, Wiz1Character.ConditionString((Wiz1Condition) iCond));
        }

        private void UpdateAliveMonsters()
        {
            lvMonsters.BeginUpdate();
            for(int index = 0; index < lvMonsters.Items.Count; index++)
            {
                ListViewItem lvi = lvMonsters.Items[index];
                CheckInvalidValues(index, lvi.Tag as Wiz1EncounterInfo.Record);
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

        private void ChangeAlive()
        {
            if (!m_bInitialized)
                return;
            if (nudAlive.Value > nudTotal.Value)
                nudTotal.Value = nudAlive.Value;
            ChangeGroup((int)Wiz1EncounterInfo.Group.Property.Alive, (int) nudAlive.Value);

            UpdateAliveMonsters();
        }

        private void ChangeTotal()
        {
            if (!m_bInitialized)
                return;
            if (nudTotal.Value < nudAlive.Value)
                nudAlive.Value = nudTotal.Value;
            ChangeGroup((int)Wiz1EncounterInfo.Group.Property.Total, (int)nudTotal.Value);
            UpdateMonsters();
            UpdateAliveMonsters();
        }
        
        private void ChangeHitPoints()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.HP, (int) nudHitPoints.Value);
        }

        private void ChangeACMod()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.AC, (int)nudACModifier.Value);
        }

        private void ChangeInitiative()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.Initiative, (int)nudInitiative.Value);
        }

        private void ChangeVictim()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.Victim, (int)nudVictim.Value);
        }

        private void ChangeInAudCnt()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.InAudCnt, (int)nudInAudCnt.Value);
        }

        private void ChangeUnknown()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.Unknown, (int)nudUnknown.Value);
        }

        private void ChangeSpellHash()
        {
            ChangeMonster((int)Wiz1EncounterInfo.Record.Property.SpellHash, (int)nudSpellHash.Value);
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

        private void Wiz1EncounterEditForm_Load(object sender, EventArgs e)
        {
            m_bInitialized = true;
        }
    }
}
