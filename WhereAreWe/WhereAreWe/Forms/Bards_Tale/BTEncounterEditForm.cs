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
    public partial class BTEncounterEditForm : CommonKeyForm
    {
        private GameNames m_game = GameNames.None;
        private BTEncounterInfo m_info;
        private List<BTMonster> m_monstersSelected = null;
        private Timer m_timerCancelMonsters = new Timer();
        private Timer m_timerCancelGroups = new Timer();

        public BTEncounterEditForm()
        {
            InitializeComponent();
        }

        public BTEncounterEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;

            comboCondition.Items.Clear();
            comboCondition.Items.Add("Good");

            comboMonster.Items.Clear();
            comboMonster.Items.Add("<none>");

            List<BTMonster> monsters = Games.GetBTGlobals(game).GetMonsters();

            foreach(BTMonster monster in monsters)
                if (monster.Index != 0)
                    comboMonster.Items.Add(String.Format("#{0}, {1}, {2} HP, {3} AC, {4} Exp", monster.Index, monster.ProperName, monster.HPString(true), monster.AC, monster.Experience));

            m_timerCancelGroups.Interval = 100;
            m_timerCancelMonsters.Interval = 100;
            m_timerCancelGroups.Tick += m_timerCancelGroups_Tick;
            m_timerCancelMonsters.Tick += m_timerCancelMonsters_Tick;

            CommonKeyFind += BT1EncounterEditForm_CommonKeyFind;
            CommonKeyNext += BT1EncounterEditForm_CommonKeyNext;
            CommonKeyPrevious += BT1EncounterEditForm_CommonKeyPrevious;
            CommonKeySelectAll += BTEncounterEditForm_CommonKeySelectAll;

            switch (game)
            {
                case GameNames.BardsTale1:
                    nudHitPoints.Maximum = 255;
                    break;
                default:
                    break;
            }
        }

        void BTEncounterEditForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(ActiveControl);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboMonster.Text.ToLower().Contains(tbSearch.Text.ToLower()))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void BT1EncounterEditForm_CommonKeyPrevious(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, false);
        }

        void BT1EncounterEditForm_CommonKeyNext(object sender, BoolHandlerEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbSearch.Text))
                return;
            Global.FindNext(comboMonster, tbSearch.Text, true);
        }

        void BT1EncounterEditForm_CommonKeyFind(object sender, EventArgs e)
        {
            tbSearch.Focus();
            tbSearch.Select();
        }

        public BTEncounterInfo Info
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

        public void SetMonsters(List<BTMonster> monsters)
        {
            m_monstersSelected = monsters;
            if (m_monstersSelected != null && m_monstersSelected.Count > 0)
            {
                if (m_monstersSelected[0] != null && lvGroups.Items.Count > m_monstersSelected[0].MonsterGroup)
                    lvGroups.Items[m_monstersSelected[0].MonsterGroup].Selected = true;
            }
        }

        private void AddGroup(BTEncounterInfo info, int iGroup)
        {
            int index = info.MonsterIndices[iGroup];
            ListViewItem lvi = new ListViewItem((index).ToString());
            int iMonsterIndex = (info == null ? -1 : index);

            lvi.SubItems.Add(MonsterName(iMonsterIndex));

            if (info != null)
                lvi.SubItems.Add(info.Living[iGroup].ToString());
            lvi.Tag = new BTEncounterGroupTag(index, Global.Subset(info.MonsterHP, iGroup * 100, 100), info.Living[iGroup]);
            lvGroups.Items.Add(lvi);
        }

        private void AddMonster(int index, byte[] monsterHP, int iGroup, int iMonster)
        {
            int iHP = monsterHP[iMonster];
            if (iHP < 1)
            {
                iHP = 1;
                monsterHP[iMonster] = 1;
            }
            ListViewItem lvi = new ListViewItem(index.ToString());
            lvi.SubItems.Add(iHP.ToString());
            lvi.SubItems.Add("0");
            lvi.SubItems.Add("Good");
            lvi.Tag = new BTEncounterMonsterTag(iGroup, iMonster, iHP);
            SetMonsterColor(lvi, iHP);
            lvMonsters.Items.Add(lvi);
        }

        public void UpdateUI()
        {
            lvGroups.BeginUpdate();
            lvGroups.Items.Clear();
            for (int i = 0; i < m_info.MonsterIndices.Length; i++)
                AddGroup(m_info, i);
            lvGroups.EndUpdate();
        }

        public void UpdateFromUI()
        {
            m_info.MonsterIndices = Global.NullBytes(4);
            m_info.MonsterHP = Global.NullBytes(400);

            for (int i = 0; i < lvGroups.Items.Count; i++)
            {
                BTEncounterGroupTag tag = lvGroups.Items[i].Tag as BTEncounterGroupTag;
                m_info.MonsterIndices[i] = (byte) tag.Index;
                for (int j = 0; j < tag.Living; j++)
                    m_info.MonsterHP[i * 100 + j] = tag.MonsterHP[j];
            }
            m_info.UpdateLivingMonstersBytes();
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
            {
                nudLiving.Value = (lvGroups.SelectedItems[0].Tag as BTEncounterGroupTag).Living;
                UpdateMonsters(lvGroups.SelectedItems[0].Index);
            }
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
            nudLiving.Enabled = bEnable;
            comboMonster.Enabled = bEnable;
            if (!bEnable)
                EnableMonsterControls(bEnable);
        }

        private void EnableMonsterControls(bool bEnable)
        {
            nudHitPoints.Enabled = bEnable;
            nudACModifier.Enabled = bEnable;
            comboCondition.Enabled = bEnable;
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

            BTEncounterGroupTag group = lvGroups.SelectedItems[0].Tag as BTEncounterGroupTag;
            if (group == null)
                return;

            Global.SetIndex(comboMonster, group.Index);
        }

        private void UpdateMonsters(int iGroup = -1)
        {
            if (iGroup == -1 && lvGroups.SelectedItems.Count > 0)
                iGroup = lvGroups.SelectedItems[0].Index;

            BTEncounterGroupTag group = lvGroups.SelectedItems[0].Tag as BTEncounterGroupTag;

            if (iGroup < 0 || iGroup > lvGroups.Items.Count)
                return;

            int iIndex = group.Index;

            if (group == null)
                return;

            lvMonsters.BeginUpdate();
            lvMonsters.Items.Clear();

            for (int i = 0; i < group.Living; i++)
                AddMonster(iIndex, group.MonsterHP, iGroup, i);

            if (m_monstersSelected != null)
            {
                foreach (BTMonster monster in m_monstersSelected)
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

            BTEncounterMonsterTag record = lvMonsters.SelectedItems[0].Tag as BTEncounterMonsterTag;
            if (record == null)
                return;

            Global.SetNud(nudHitPoints, record.HP);
        }

        private void ChangeMonster(int index, int val, string strVal = null)
        {
            foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                ChangeMonster(lvi, index, val, strVal);
        }

        private void ChangeMonster(ListViewItem lvi, int index, int val, string strVal = null)
        {
            BTEncounterMonsterTag record = lvi.Tag as BTEncounterMonsterTag;
            if (record != null)
            {
                // Only one thing to change at the moment - HP
                record.HP = val;
                BTEncounterGroupTag tag = lvGroups.Items[record.Group].Tag as BTEncounterGroupTag;
                tag.MonsterHP[record.Monster] = (byte)val;
                tag.UpdateLiving();
                lvGroups.Items[record.Group].SubItems[2].Text = tag.Living.ToString();
                lvi.SubItems[index + 1].Text = strVal == null ? val.ToString() : strVal;
                SetMonsterColor(lvi, val);
            }
        }

        private void SetMonsterColor(ListViewItem lvi, int hp)
        {
            if (hp == 0)
            {
                lvi.ForeColor = SystemColors.GrayText;
                if (lvi.Font.Style != FontStyle.Italic)
                    lvi.Font = new Font(lvi.Font, FontStyle.Italic);
            }
            else
            {
                lvi.ForeColor = SystemColors.ControlText;
                if (lvi.Font.Style != FontStyle.Regular)
                    lvi.Font = new Font(lvi.Font, FontStyle.Regular);
            }
        }

        private BTMonster CloneMonster(int index)
        {
            List<BTMonster> monsters = Games.GetBTGlobals(m_game).GetMonsters();
            if (index >= 0 && index < monsters.Count)
                return monsters[index].Clone() as BTMonster;
            return null;
        }

        private string MonsterName(int index)
        {
            List<BTMonster> monsters = Games.GetBTGlobals(m_game).GetMonsters();
            if (index >= 0 && index < monsters.Count)
                return monsters[index].ProperName;
            return "<none>";
        }

        private void comboMonster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count < 1)
                return;
            int index = comboMonster.SelectedIndex;
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                lvi.Text = index.ToString();
                lvi.SubItems[1].Text = MonsterName(index);
                (lvi.Tag as BTEncounterGroupTag).Index = index;
            }
        }

        private void comboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only one condition at the moment
        }

        private void ChangeHitPoints()
        {
            ChangeMonster(0, (int) nudHitPoints.Value);
        }

        private void ChangeACMod()
        {
            // Monsters may not have a changable AAC
        }

        private void ChangeLiving()
        {
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                BTEncounterGroupTag tag = lvi.Tag as BTEncounterGroupTag;
                tag.Living = (int) nudLiving.Value;
                lvi.SubItems[2].Text = tag.Living.ToString();
                UpdateMonsters(lvi.Index);
            }
        }

        private void nudHitPoints_ValueChanged(object sender, EventArgs e) { ChangeHitPoints(); }
        private void nudHitPoints_KeyDown(object sender, KeyEventArgs e) { ChangeHitPoints(); }
        private void nudACModifier_ValueChanged(object sender, EventArgs e) { ChangeACMod(); }
        private void nudACModifier_KeyDown(object sender, KeyEventArgs e) { ChangeACMod(); }
        private void nudLiving_KeyDown(object sender, KeyEventArgs e) { ChangeLiving(); }
        private void nudLiving_ValueChanged(object sender, EventArgs e) { ChangeLiving(); }

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
            }
            base.Dispose(disposing);
        }

        private void llKillAllMonsters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_info.MonsterHP = Global.NullBytes(400);
            m_info.Living = Global.NullBytes(4);
            lvMonsters.Items.Clear();
            lvGroups.SelectedItems.Clear();
            UpdateUI();
        }
    }

    public class BTEncounterMonsterTag
    {
        public int Group;
        public int Monster;
        public int HP;

        public BTEncounterMonsterTag(int iGroup, int iMonster, int iHP)
        {
            Group = iGroup;
            Monster = iMonster;
            HP = iHP;
        }
    }

    public class BTEncounterGroupTag
    {
        public int Index;
        public byte[] MonsterHP;
        public int Living;

        public BTEncounterGroupTag(int index, byte[] monsterHP, int living)
        {
            Index = index;
            MonsterHP = monsterHP;
            Living = living;
        }

        public void UpdateLiving()
        {
            Living = 0;
            if (MonsterHP == null)
                return;
            for (int i = 0; i < MonsterHP.Length; i++)
            {
                if (MonsterHP[i] > 0)
                    Living++;
                else
                    break;
            }
        }
    }
}
