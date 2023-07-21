using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class MonstersForm : HackerBasedForm
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private FindBox m_findBox = null;

        public MonstersForm()
        {
            InitializeComponent();

            NativeMethods.SetTooltipDelay(lvMonsters, 32000);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel2Collapsed)
                m_findBox.Next(sender, new BoolHandlerEventArgs(false));
            else
                Close();
        }

        protected override bool OnCommonKeySelectAll()
        {
            lvMonsters.BeginUpdate();
            foreach (ListViewItem lvi in lvMonsters.Items)
                lvi.Selected = true;
            lvMonsters.EndUpdate();
            return true;
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
        }

        private void MonstersForm_Load(object sender, EventArgs e)
        {
            m_findBox = new FindBox(splitContainer1, tbFind, FindBox.ListViewFindFunction, lvMonsters);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;

            splitContainer1.Panel2Collapsed = true;
        }

        protected override void OnMainSet()
        {
            UpdateUI();
        }

        private string GetColumnText(Monster monster, int iColumn)
        {
            switch (iColumn)
            {
                case 0: return monster.ProperName;
                case 1: return monster.IndexString;
                case 2: return monster.HPString(true);
                case 3: return monster.AC.ToString();
                case 4: return monster.DamageString;
                case 5: return monster.SpeedString;
                case 6: return monster.ResistancesStringShort;
                case 7: return monster.AllPowersString;
                case 8: return monster.Experience.ToString();
                case 9: return monster.TreasureStringShort;
                default: return String.Empty;
            }
        }

        private void UpdateUI()
        {
            lvMonsters.BeginUpdate();
            lvMonsters.Items.Clear();

            IEnumerable<Monster> monsters = Hacker.GetMonsterList();

            foreach (Monster monster in monsters)
            {
                if (monster.Name == "<None>")
                    continue;
                ListViewItem lvi = new ListViewItem(GetColumnText(monster, 0));
                for(int i = 1; i < lvMonsters.Columns.Count; i++)
                    lvi.SubItems.Add(GetColumnText(monster, i));
                lvi.Tag = monster;
                lvi.ToolTipText = monster.GetMultiLineDescription(false);
                lvMonsters.Items.Add(lvi);
            }

            if (Games.IsWizardry(Hacker.Game))
            {
                chSpeed.Text = "Group";

                if (Hacker.Game == GameNames.Wizardry4)
                {
                    chTreasure.Width = 0;
                    chExperience.Width = 0;
                }
                else
                {
                    chTreasure.Width = 60;
                    chExperience.Width = 54;
                }
            }
            else if (Games.IsBardsTale(Hacker.Game))
            {
                chTreasure.Text = "Group";
                chDamage.Text = "Attacks";
                if (Hacker.Game == GameNames.BardsTale3)
                {
                    chSpecialPowers.Text = "Misc";
                }
                else
                {
                    chSpeed.Width = 0;
                    chResistances.Width = 0;
                }
            }

            Global.SizeHeadersAndContent(lvMonsters);
            lvMonsters.EndUpdate();

            if (Hacker != null)
                Text = String.Format("Monster List: {0}", Games.Name(Hacker.Game));
            else
                Text = "Monster List (no game running)";
        }

        private void lvMonsters_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvMonsters.ListViewItemSorter = new MonsterComparer(e.Column, m_bAscendingSort);
            lvMonsters.Sort();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
                Close();
            m_findBox.HideFindBox();
        }

        private void lvMonsters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMonsterInfo();
        }

        private void ShowMonsterInfo()
        {
            if (lvMonsters.FocusedItem == null)
                return;

            Monster monster = lvMonsters.FocusedItem.Tag as Monster;
            if (monster == null)
                return;

            ViewInfoForm.ShowCentered(this, monster.GetMultiLineDescription(false).Trim(), String.Format("#{0}: {1}", monster.Index, monster.ProperName));
        }

        private void lvMonsters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ShowMonsterInfo();
        }

        private void cmMonsters_Opening(object sender, CancelEventArgs e)
        {
            if (lvMonsters.SelectedItems.Count == 0)
                miCtxCopy.Text = "&Copy all monsters";
            else
                miCtxCopy.Text = "&Copy selected monsters";
        }

        private string GetMonsterString(ListViewItem lvi)
        {
            Monster monster = lvi.Tag as Monster;
            if (monster == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lvMonsters.Columns.Count; i++)
            {
                if (lvMonsters.Columns[i].Width > 0)
                    sb.AppendFormat("{0}\t", GetColumnText(monster, i));
            }
            return Global.Trim(sb).ToString();
        }

        private string GetMonsterLongDescription(ListViewItem lvi)
        {
            Monster monster = lvi.Tag as Monster;
            if (monster == null)
                return String.Empty;

            return String.Format("{0}\r\n", monster.MultiLineDescription);
        }

        private void miCtxCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (lvMonsters.SelectedItems.Count == 0)
            {
                foreach (ListViewItem lvi in lvMonsters.Items)
                    sb.AppendFormat("{0}\r\n", GetMonsterString(lvi));
            }
            else
            {
                foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                    sb.AppendFormat("{0}\r\n", GetMonsterString(lvi));
            }
            Clipboard.SetText(sb.ToString());
        }

        private void miCtxCopyFull_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (lvMonsters.SelectedItems.Count == 0)
            {
                foreach (ListViewItem lvi in lvMonsters.Items)
                    sb.AppendFormat("{0}\r\n", GetMonsterLongDescription(lvi));
            }
            else
            {
                foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                    sb.AppendFormat("{0}\r\n", GetMonsterLongDescription(lvi));
            }
            Clipboard.SetText(sb.ToString());
        }
    }

    class MonsterComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public MonsterComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }
        public MonsterComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }

        private double AverageDamage(int iAttackMax, int iAttacks, int iAttackMin = 1)
        {
            return (iAttackMax + iAttackMin) / 2.0 * iAttacks;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            Monster monster1 = ((ListViewItem)x).Tag as Monster;
            Monster monster2 = ((ListViewItem)y).Tag as Monster;

            switch (m_column)
            {
                case 0:
                    returnVal = String.Compare(monster1.ProperName, monster2.ProperName);
                    break;
                case 1:
                    if (monster1 is MM45Monster && monster2 is MM45Monster && ((MM45Monster)monster1).Side != ((MM45Monster)monster2).Side)
                        returnVal = ((MM45Monster)monster1).Side == MM45Side.Clouds ? -1 : 1;
                    else
                        returnVal = Math.Sign(monster1.Index - monster2.Index);
                    break;
                case 2:
                    returnVal = Math.Sign(monster1.AverageHP - monster2.AverageHP);
                    break;
                case 3:
                    returnVal = Math.Sign(monster1.AC - monster2.AC);
                    break;
                case 4:
                    returnVal = Math.Sign(monster1.AverageDamage - monster2.AverageDamage);
                    break;
                case 5:
                    if (monster1 is MMMonster)
                        returnVal = Math.Sign(((MMMonster)monster1).Speed - ((MMMonster)monster2).Speed);
                    else if (monster1 is WizMonster)
                        returnVal = Math.Sign(((WizMonster)monster1).NumAppearing.Average - ((WizMonster)monster2).NumAppearing.Average);
                    else if (monster1 is BT3Monster)
                        returnVal = Math.Sign(((BT3Monster)monster1).Speed - ((BT3Monster)monster2).Speed);
                    break;
                case 6:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    break;
                case 7:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    break;
                case 8:
                    returnVal = Math.Sign(monster1.Experience - monster2.Experience);
                    break;
                case 9:
                    if (monster1 is BTMonster)
                        returnVal = Math.Sign(((BTMonster)monster1).GroupSize - ((BTMonster)monster2).GroupSize);
                    else
                        returnVal = Math.Sign(monster1.TreasureStrength - monster2.TreasureStrength);
                    break;

            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }
}
