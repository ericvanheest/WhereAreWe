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
    public partial class BT2EncounterEditForm : CommonKeyForm
    {
        private GameNames m_game = GameNames.None;
        private BT23EncounterInfo m_info;
        private List<BTMonster> m_monstersSelected = null;
        private Timer m_timerCancelMonsters = new Timer();
        private Timer m_timerCancelGroups = new Timer();

        public BT2EncounterEditForm()
        {
            InitializeComponent();
        }

        public BT2EncounterEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;

            List<BTMonster> monsters = Games.GetBTGlobals(game).GetMonsters();

            m_timerCancelGroups.Interval = 100;
            m_timerCancelMonsters.Interval = 100;
            m_timerCancelGroups.Tick += m_timerCancelGroups_Tick;
            m_timerCancelMonsters.Tick += m_timerCancelMonsters_Tick;

            CommonKeySelectAll += BTEncounterEditForm_CommonKeySelectAll;
        }

        void BTEncounterEditForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(ActiveControl);
        }

        public BT23EncounterInfo Info
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

        private void ChangeGroup(BT2EncounterGroupTag tag)
        {
            if (tag.Index >= lvGroups.Items.Count)
                return;

            ListViewItem lvi = lvGroups.Items[tag.Index];
            UpdateGroupLVI(lvi, tag);
        }

        private void UpdateGroupLVI(ListViewItem lvi, BT2EncounterGroupTag tag)
        {
            BT3Monster bt3 = tag.Monster as BT3Monster;
            int iIndex = 1;
            lvi.SubItems[iIndex++].Text = tag.Monster.Name;
            lvi.SubItems[iIndex++].Text = tag.Living.ToString();
            lvi.SubItems[iIndex++].Text = tag.Monster.GroupSize.ToString();
            lvi.SubItems[iIndex++].Text = tag.Monster.AC.ToString();
            lvi.SubItems[iIndex++].Text = tag.Monster.NumAttacks.ToString();
            lvi.SubItems[iIndex++].Text = bt3 == null ? tag.Monster.DamageString : String.Format("{0}-{1}", bt3.InitiativeMin, bt3.InitiativeMax);
            lvi.SubItems[iIndex++].Text = tag.Monster.Distance > 0 ? String.Format("{0}0'", tag.Monster.Distance) : String.Empty;
            lvi.SubItems[iIndex++].Text = bt3 == null ? tag.Monster.TouchString : String.Format("{0}-{1}", bt3.MagicResistFull, bt3.MagicResistHalf);
            lvi.SubItems[iIndex++].Text = tag.Monster.Experience.ToString();
            lvi.SubItems[iIndex++].Text = tag.Monster.GetAttackString(0, true);
            lvi.SubItems[iIndex++].Text = tag.Monster.GetAttackString(1, true);
            lvi.SubItems[iIndex++].Text = tag.Monster.GetAttackString(2, true);
            lvi.SubItems[iIndex++].Text = tag.Monster.GetAttackString(3, true);
            lvi.SubItems[iIndex++].Text = tag.Monster.Speed.ToString();

            lvi.Tag = tag;
        }

        private void AddGroup(BT23EncounterInfo info, int iGroup)
        {
            ListViewItem lvi = new ListViewItem(String.Format("{0}", (char) ('a' + iGroup)));
            BTMonster monster = info.Groups[iGroup];
            BT2EncounterGroupTag tag = new BT2EncounterGroupTag(iGroup, monster, Global.Subset(info.MonsterHP, iGroup * 64, 64), info.Living[iGroup]);

            for (int i = 1; i < lvGroups.Columns.Count; i++)
                lvi.SubItems.Add(String.Empty);

            UpdateGroupLVI(lvi, tag);
            lvGroups.Items.Add(lvi);
        }

        private void AddMonster(int index, short[] monsterHP, int iGroup, int iMonster)
        {
            if (iMonster >= monsterHP.Length)
                return;

            int iHP = monsterHP[iMonster];
            if (iHP < 1)
            {
                iHP = 1;
                monsterHP[iMonster] = 1;
            }
            ListViewItem lvi = new ListViewItem(iMonster.ToString());
            lvi.SubItems.Add(iHP.ToString());
            lvi.Tag = new BTEncounterMonsterTag(iGroup, iMonster, iHP);
            SetMonsterColor(lvi, iHP);
            lvMonsters.Items.Add(lvi);
        }

        public void UpdateUI()
        {
            lvGroups.BeginUpdate();
            lvGroups.Items.Clear();
            for (int i = 0; i < m_info.Living.Length; i++)
                AddGroup(m_info, i);
            if (m_info is BT3EncounterInfo)
            {
                chDamage.Text = "Init.";
                chSpecial.Text = "MagRes";
            }
            lvGroups.EndUpdate();
        }

        public void UpdateFromUI()
        {
            m_info.MonsterHP = Global.NullBytes(256);

            for (int i = 0; i < lvGroups.Items.Count; i++)
            {
                BT2EncounterGroupTag tag = lvGroups.Items[i].Tag as BT2EncounterGroupTag;
                byte[] bytesMonster = tag.Monster.GetBytes();
                if (tag.Monster is BT3Monster)
                {
                    Buffer.BlockCopy(bytesMonster, 0, m_info.MonsterIndices, i * bytesMonster.Length, bytesMonster.Length);
                }
                else
                {
                    Buffer.BlockCopy(bytesMonster, 0, m_info.MonsterIndices, i * 32 + 16, bytesMonster.Length);
                }
                byte[] bytesHP = tag.GetHPBytes();
                Buffer.BlockCopy(bytesHP, 0, m_info.MonsterHP, i * 64, bytesHP.Length);
                m_info.Distances[i] = (byte)tag.Monster.Distance;
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
                Global.SetNud(nudLiving, (lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag).Living);
                Global.SetNud(nudDistance, 10 * (lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag).Monster.Distance);
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
            nudDistance.Enabled = bEnable;
            if (!bEnable)
                EnableMonsterControls(bEnable);
        }

        private void EnableMonsterControls(bool bEnable)
        {
            nudHitPoints.Enabled = bEnable;
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

            BT2EncounterGroupTag group = lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag;
            if (group == null)
                return;
        }

        private void UpdateMonsters(int iGroup = -1)
        {
            if (iGroup == -1 && lvGroups.SelectedItems.Count > 0)
                iGroup = lvGroups.SelectedItems[0].Index;

            BT2EncounterGroupTag group = lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag;

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
                BT2EncounterGroupTag tag = lvGroups.Items[record.Group].Tag as BT2EncounterGroupTag;
                tag.MonsterHP[record.Monster] = (short)val;
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

        private void ChangeHitPoints()
        {
            ChangeMonster(0, (int) nudHitPoints.Value);
        }

        private void ChangeLiving()
        {
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                BT2EncounterGroupTag tag = lvi.Tag as BT2EncounterGroupTag;
                tag.Living = (int) nudLiving.Value;
                if (tag.Living > 0 && tag.Monster.Distance < 1)
                    tag.Monster.Distance = 1;
                lvi.SubItems[2].Text = tag.Living.ToString();
                UpdateMonsters(lvi.Index);
            }
        }

        private void nudHitPoints_ValueChanged(object sender, EventArgs e) { ChangeHitPoints(); }
        private void nudHitPoints_KeyDown(object sender, KeyEventArgs e) { ChangeHitPoints(); }
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

        private void editMonsterGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count < 1)
                return;
            BT2EncounterGroupTag tag = lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag;
            if (tag.Monster is BT3Monster)
            {
                BT3MonsterEditForm form = new BT3MonsterEditForm();
                form.Monster = tag.Monster as BT3Monster;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tag.Monster = form.Monster;
                    ChangeGroup(tag);
                }
            }
            else
            {
                BT2MonsterEditForm form = new BT2MonsterEditForm();
                form.Monster = tag.Monster as BT2Monster;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tag.Monster = form.Monster;
                    ChangeGroup(tag);
                }
            }
        }

        private void cmGroup_Opening(object sender, CancelEventArgs e)
        {
            miEditMonsterGroup.Enabled = lvGroups.SelectedItems.Count > 0;
        }

        private void ChangeDistance()
        {
            foreach (ListViewItem lvi in lvGroups.SelectedItems)
            {
                BT2EncounterGroupTag tag = lvi.Tag as BT2EncounterGroupTag;
                tag.Monster.Distance = Math.Max(1, (int)nudDistance.Value / 10);
                lvi.SubItems[2].Text = tag.Living.ToString();
                lvi.SubItems[7].Text = String.Format("{0}0'", tag.Monster.Distance);
                UpdateMonsters(lvi.Index);
            }
        }

        private void nudDistance_ValueChanged(object sender, EventArgs e) { ChangeDistance(); }
        private void nudDistance_KeyDown(object sender, KeyEventArgs e) { ChangeDistance(); }

        private void lvGroups_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMonsterInfo();
        }

        private void ShowMonsterInfo()
        {
            if (lvGroups.SelectedItems.Count < 1)
                return;
            BT2EncounterGroupTag tag = lvGroups.SelectedItems[0].Tag as BT2EncounterGroupTag;
            if (tag == null)
                return;
            Monster monster = tag.Monster;
            ViewInfoForm.ShowCentered(this, monster.MultiLineDescription, monster.ProperName);
        }
    }

    public class BT2EncounterGroupTag
    {
        public int Index;
        public short[] MonsterHP;
        public int Living;
        public BTMonster Monster;

        public BT2EncounterGroupTag(int index, BTMonster monster, byte[] monsterHP, int living)
        {
            Index = index;
            MonsterHP = new short[monsterHP.Length / 2];
            for (int i = 0; i < monsterHP.Length - 1; i += 2)
                MonsterHP[i / 2] = BitConverter.ToInt16(monsterHP, i);

            Living = living;
            Monster = monster;
        }

        public byte[] GetHPBytes(bool bIgnoreLiving = false)
        {
            byte[] bytes = Global.NullBytes(MonsterHP.Length * 2);
            int iCount = bIgnoreLiving ? MonsterHP.Length : Math.Min(Living, MonsterHP.Length);
            for (int i = 0; i < iCount; i++)
                Global.SetInt16(bytes, i * 2, MonsterHP[i]);
            return bytes;
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
