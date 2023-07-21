using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class Wiz1CreationAssistantForm : HackerBasedForm
    {
        private int m_iPointsRemaining = 0;
        private byte[] m_oldStatsOrig = null;
        private byte[] m_oldStatsMod = null;
        private bool m_bAcceptNewValues = false;
        private MainState m_state = MainState.Unknown;
        private List<DiceRolls> m_prevRolls;
        private int m_iSkipUpdates = 0;
        private int m_iErrorGraceTimer = 0;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bIgnoreNextRoll = false;
        public int[] WeightTable;
        private int m_highestWeight = 1;
        private PrimaryStat[] m_statOrder = null;

        public Wiz1CreationAssistantForm()
        {
            InitializeComponent();

            m_prevRolls = new List<DiceRolls>();
            Win32.SetTooltipDelay(lvStats, 32000);
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        protected override void OnMainSet()
        {
            if (Hacker == null)
            {
                Text = "Character Creation Assistant (no game running)";
                return;
            }
            Text = "Character Creation Assistant";

            m_statOrder = Hacker.StatOrder;

            int[] Weights = Hacker.CreationStatWeights;
            WeightTable = new int[Weights.Length];
            WeightTable[0] = Weights[0];
            for (int i = 1; i < Weights.Length; i++)
                WeightTable[i] = WeightTable[i - 1] + Weights[i];

            m_highestWeight = Weights[Weights.Length - 1];

            lvPrevious.SuspendLayout();
            for (int i = 0; i < m_statOrder.Length; i++)
            {
                if (lvPrevious.Columns.Count > i)
                    lvPrevious.Columns[i + 2].DisplayIndex = (int)m_statOrder[i] + 2;  // skip the weighted and unweighted columns
            }
            lvPrevious.ResumeLayout();

            StringBuilder sbStats = new StringBuilder("Up to ");
            StringBuilder sbValues = new StringBuilder();
            for (int i = 4; i < Weights.Length; i++)
            {
                if (Weights[i] == Weights[i - 1])
                    continue;
                sbStats.AppendFormat("{0}\n{0} to ", i - 1);
                sbValues.AppendFormat("{0}\n", Weights[i - 1]);
            }
            sbStats.AppendFormat("{0}\n{0} to ", Weights.Length - 1);
            sbValues.AppendFormat("{0}\n", Weights[Weights.Length - 1]);

            while (sbStats.Length > 0 && sbStats[sbStats.Length - 1] != '\n')
                sbStats.Remove(sbStats.Length - 1, 1);

            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.None);

            labelHuman.Text = Hacker.GetRaceDescription(GenericRace.Human);
            labelElf.Text = Hacker.GetRaceDescription(GenericRace.Elf);
            labelDwarf.Text = Hacker.GetRaceDescription(GenericRace.Dwarf);
            labelGnome.Text = Hacker.GetRaceDescription(GenericRace.Gnome);
            labelHobbit.Text = Hacker.GetRaceDescription(GenericRace.Hobbit);

            labelFighter.Text = Hacker.GetClassDescription(GenericClass.Fighter);
            labelMage.Text = Hacker.GetClassDescription(GenericClass.Mage);
            labelPriest.Text = Hacker.GetClassDescription(GenericClass.Priest);
            labelThief.Text = Hacker.GetClassDescription(GenericClass.Thief);
            labelBishop.Text = Hacker.GetClassDescription(GenericClass.Bishop);
            labelSamurai.Text = Hacker.GetClassDescription(GenericClass.Samurai);
            labelLord.Text = Hacker.GetClassDescription(GenericClass.Lord);
            labelNinja.Text = Hacker.GetClassDescription(GenericClass.Ninja);

            Global.RestartTimer(timerUpdateMemory);
        }

        private int GetWeightedTotal(byte[] rolls)
        {
            int iTotal = 0;
            for (int i = 0; i < rolls.Length; i++)
                iTotal += GetWeightedValue(rolls[i]);
            return iTotal;
        }

        private void CharCreationForm_Load(object sender, EventArgs e)
        {
            timerUpdateMemory.Start();
            comboAttrib.SelectedIndex = 3;  // Ed
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvSheets_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Add:
                    ModifySelectedAttribute(1, true);
                    break;
                case Keys.Subtract:
                    ModifySelectedAttribute(-1, true);
                    break;
                default:
                    break;
            }
        }

        private bool ModifySelectedAttribute(int i, bool bWriteNew)
        {
            if (lvStats.FocusedItem == null)
                return false;

            DiceRoll roll = (DiceRoll)lvStats.FocusedItem.Tag;
            if (roll.Original + i < 3)
                return false;
            if (roll.Original + i > (Hacker.DieMax * 3))
                return false;

            int iWeightedOrig = GetWeightedValue(roll.Original);
            int iWeightedNew = GetWeightedValue(roll.Original + i);
            if (iWeightedNew - iWeightedOrig > m_iPointsRemaining)
                return false;

            m_iPointsRemaining -= (iWeightedNew - iWeightedOrig);
            roll.Original += i;
            roll.Modified += i;

            if (bWriteNew)
                WriteNewValues();

            return true;
        }

        private void WriteNewValues()
        {
            CharCreationInfo info = CreateInfo();
            info.AttributesModified = new byte[7];
            info.AttributesOriginal = new byte[7];
            for (int i = 0; i < 7; i++)
            {
                DiceRoll roll = (DiceRoll)lvStats.Items[i].Tag;
                info.AttributesOriginal[i] = (byte)roll.Original;
                info.AttributesModified[i] = (byte)roll.Modified;
            }
            m_bAcceptNewValues = true;
            m_iSkipUpdates = 1;

            Hacker.SetCharCreationInfo(info);

            if (Properties.Settings.Default.RefreshDOSBox && !(info is MM345CharCreationInfo))
                Hacker.RefreshRollScreen();
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (m_iSkipUpdates > 0)
            {
                m_iSkipUpdates--;
                return;
            }

            if (Hacker == null || !Hacker.Running)
            {
                Text = "Character Creation Assistant (no running game detected!)";
                return;
            }

            Text = "Character Creation Assistant";

            CharCreationInfo info = Hacker.GetCharCreationInfo();
            if (info == null)
                return;

            if (info.State.Main == MainState.Rolling || info.State.Main == MainState.CreateExchangeStat)
                return; // Don't try to read values while they are being generated

            if (!info.ValidValues || !info.OnCharCreation)
            {
                if (m_iErrorGraceTimer == -1)
                {
                    m_iErrorGraceTimer = 8;
                    return;
                }
                else if (m_iErrorGraceTimer > 0)
                {
                    m_iErrorGraceTimer--;
                    return;
                }

                m_iErrorGraceTimer = -1;

                panelInvalidRolls.Visible = true;
                panelInvalidRolls.BringToFront();
                return;
            }
            else
            {
                panelInvalidRolls.Visible = false;
            }

            if (!m_bAcceptNewValues)
            {
                bool bStatsSame = Global.CompareBytes(info.AttributesModified, m_oldStatsMod) && Global.CompareBytes(info.AttributesOriginal, m_oldStatsOrig);
                if (bStatsSame && m_state == info.State.Main)
                    return;

                if (!bStatsSame)
                {
                    if (m_bIgnoreNextRoll)
                        m_bIgnoreNextRoll = false;
                    else
                        AddPreviousRollToList(new DiceRolls(info.AttributesOriginal, m_statOrder, GetWeightedTotal(info.AttributesOriginal)));
                    m_iPointsRemaining = 0;
                }
            }

            m_state = info.State.Main;

            m_bAcceptNewValues = false;
            m_bIgnoreNextRoll = false;

            m_oldStatsOrig = info.AttributesOriginal;
            m_oldStatsMod = info.AttributesModified;

            UpdateUI(info);
        }

        private void AddPreviousRollToList(DiceRolls rolls)
        {
            // Don't add this roll if it's exactly the same as the previous one in the list
            if (lvPrevious.Items.Count > 0)
            {
                DiceRolls lastRolls = (DiceRolls)lvPrevious.Items[lvPrevious.Items.Count - 1].Tag;
                if (lastRolls.Equals(rolls))
                    return;
            }

            m_prevRolls.Add(rolls);

            ListViewItem lvi = new ListViewItem(rolls.RawTotal.ToString());
            lvi.SubItems.Add(rolls.WeightedTotal.ToString());
            lvi.SubItems.Add(rolls.Intellect.ToString());
            lvi.SubItems.Add(rolls.Might.ToString());
            lvi.SubItems.Add(rolls.Personality.ToString());
            lvi.SubItems.Add(rolls.Endurance.ToString());
            lvi.SubItems.Add(rolls.Speed.ToString());
            lvi.SubItems.Add(rolls.Accuracy.ToString());
            lvi.SubItems.Add(rolls.Luck.ToString());
            lvi.Tag = rolls;

            lvPrevious.ListViewItemSorter = null;

            lvPrevious.Items.Add(lvi);

            lvPrevious.EnsureVisible(lvi.Index);
        }

        private void UpdateUI(CharCreationInfo info)
        {
            if (info.AttributesOriginal == null || info.AttributesModified == null)
                return;

            if (info.AttributesModified.Length < 7 || info.AttributesOriginal.Length < 7)
                return;

            int iTotal = 0;
            int iTotalWeighted = 0;

            int iSelected = lvStats.FocusedItem != null ? lvStats.FocusedItem.Index : -1;

            lvStats.BeginUpdate();
            lvStats.Items.Clear();
            for (int i = 0; i < m_statOrder.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(GetAttributeName(m_statOrder[i]));
                lvi.SubItems.Add(GetStatString(info.AttributesOriginal[i], info.AttributesModified[i]));
                lvi.Tag = new DiceRoll(m_statOrder[i], info.AttributesOriginal[i], info.AttributesModified[i]);
                iTotal += info.AttributesOriginal[i];
                iTotalWeighted += GetWeightedValue(info.AttributesOriginal[i]);
                lvi.ToolTipText = GetStatTooltip(i, info.AttributesModified[i]);
                lvStats.Items.Add(lvi);
            }
            lvStats.EndUpdate();

            if (iSelected > -1)
            {
                lvStats.Items[iSelected].Focused = true;
                lvStats.Items[iSelected].Selected = true;
            }

            labelExtraPoints.Text = String.Format("{0}", m_iPointsRemaining);
        }

        private string GetStatTooltip(int i, int stat)
        {
            return Hacker.StatToolTip(i, stat);
        }

        public int GetWeightedValue(int iValue)
        {
            if (iValue > WeightTable.Length - 1)
                return WeightTable[WeightTable.Length - 1] + (iValue - WeightTable.Length - 1) * m_highestWeight;

            return WeightTable[iValue];
        }

        private string GetAttributeName(PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Strength: return "Strength";
                case PrimaryStat.Might: return "Might";
                case PrimaryStat.Personality: return "Personality";
                case PrimaryStat.Endurance: return "Endurance";
                case PrimaryStat.Speed: return "Speed";
                case PrimaryStat.Accuracy: return "Accuracy";
                case PrimaryStat.Luck: return "Luck";
                default: return "Unknown";
            }
        }

        private string GetStatString(byte original, byte modified)
        {
            if (original == modified)
                return String.Format("{0}", original);
            if (original > modified)
                return String.Format("{0}{1}", original, modified - original);
            return String.Format("{0}+{1}", original, modified - original);
        }

        private void CharCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_iPointsRemaining > 0 && !m_bDestroy)
            {
                switch (MessageBox.Show("You still have points remaining; these will be lost!  Exit this helper window?", "Exit with points remaining?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                    default:
                        break;
                }
            }
            timerUpdateMemory.Stop();
        }

        CharCreationInfo CreateInfo()
        {
            if (Hacker is MM1MemoryHacker)
                return new MM1CharCreationInfo();
            if (Hacker is MM2MemoryHacker)
                return new MM2CharCreationInfo();

            return new MM345CharCreationInfo();
        }

        private void LoadRolls(DiceRolls rolls)
        {
            if (Hacker == null || !Hacker.IsValid)
                return;

            CharCreationInfo info = Hacker.GetCharCreationInfo();
            if (info == null)
                return;

            if (Global.CompareBytes(info.AttributesOriginal, rolls.GetBytes(Hacker.StatOrder)))
                return;

            info = CreateInfo();

            info.AttributesModified = rolls.GetBytes(Hacker.StatOrder);
            info.AttributesOriginal = rolls.GetBytes(Hacker.StatOrder);

            m_iSkipUpdates = 5;
            m_bIgnoreNextRoll = true;
            m_iPointsRemaining = 0;

            Hacker.SetCharCreationInfo(info);

            if (Properties.Settings.Default.RefreshDOSBox)
                Hacker.RefreshRollScreen();
        }

        private void lvPrevious_DoubleClick(object sender, EventArgs e)
        {
            if (lvPrevious.FocusedItem == null)
                return;

            LoadRolls((DiceRolls)lvPrevious.FocusedItem.Tag);
        }

        private void lvPrevious_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvPrevious.ListViewItemSorter = new DiceRollsComparer(e.Column, m_bAscendingSort);
            lvPrevious.Sort();
        }

        private void cmPrevious_Opening(object sender, CancelEventArgs e)
        {
            miDelete.Enabled = (lvPrevious.SelectedItems.Count > 0);
            miDeleteExcept.Enabled = (lvPrevious.SelectedItems.Count > 0);
            miDeleteLower.Enabled = (lvPrevious.SelectedItems.Count == 1);
            miDeleteLowerUnweighted.Enabled = (lvPrevious.SelectedItems.Count == 1);
        }

        private void DeleteSelected()
        {
            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            lvPrevious.BeginUpdate();
            foreach (ListViewItem lvi in lvPrevious.SelectedItems)
            {
                lvi.Remove();
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
        }

        private void DeleteUnselected()
        {
            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            lvPrevious.BeginUpdate();
            foreach (ListViewItem lvi in lvPrevious.Items)
            {
                if (!lvi.Selected)
                    lvi.Remove();
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
        }

        private void DeleteLowerWeights()
        {
            if (lvPrevious.FocusedItem == null)
                return;

            DiceRolls rolls = (DiceRolls)lvPrevious.FocusedItem.Tag;

            RemoveByMinimumStat(rolls.WeightedTotal, 0, null);
        }

        private void DeleteLowerRaw()
        {
            if (lvPrevious.FocusedItem == null)
                return;

            DiceRolls rolls = (DiceRolls)lvPrevious.FocusedItem.Tag;

            RemoveByMinimumStat(0, rolls.RawTotal, null);
        }

        private void StartWorking()
        {
            lvPrevious.Enabled = false;
            panelWorking.BringToFront();
            panelWorking.Visible = true;
            Application.DoEvents();
        }

        private void EndWorking()
        {
            lvPrevious.Enabled = true;
            panelWorking.SendToBack();
            panelWorking.Visible = false;
            Application.DoEvents();
        }

        private void lvPrevious_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control)
                    {
                        lvPrevious.BeginUpdate();
                        foreach (ListViewItem lvi in lvPrevious.Items)
                            lvi.Selected = true;
                        lvPrevious.EndUpdate();
                    }
                    break;
                default:
                    break;
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (lvPrevious.SelectedItems.Count > 0)
                DeleteSelected();
        }

        private void miDeleteExcept_Click(object sender, EventArgs e)
        {
            if (lvPrevious.SelectedItems.Count > 0)
                DeleteUnselected();
        }

        private void miDeleteLower_Click(object sender, EventArgs e)
        {
            if (lvPrevious.SelectedItems.Count == 1)
                DeleteLowerWeights();
        }

        private void miGenerate100_Click(object sender, EventArgs e)
        {
            GenerateRolls(100);
        }

        private void GenerateRolls(int iNum)
        {
            int iMin = 1;
            int iMax = Hacker.DieMax + 1;

            lvPrevious.BeginUpdate();
            for (int i = 0; i < iNum; i++)
            {
                byte[] bytes = new byte[7];
                for (int j = 0; j < 7; j++)
                    bytes[j] = (byte)(Global.Rand.Next(iMin, iMax) + Global.Rand.Next(iMin, iMax) + Global.Rand.Next(iMin, iMax));
                DiceRolls rolls = new DiceRolls(bytes, m_statOrder, GetWeightedTotal(bytes));
                AddPreviousRollToList(rolls);
            }
            lvPrevious.EndUpdate();
        }

        private void useTheseRollsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvPrevious.FocusedItem != null)
                LoadRolls((DiceRolls)lvPrevious.FocusedItem.Tag);
        }

        private void RemoveByMinimumStat(int iMinWeight, int iMinRaw, int[] minStats)
        {
            if (lvPrevious.Items.Count > 500)
                StartWorking();

            if (minStats == null)
                minStats = new int[] { 0, 0, 0, 0, 0, 0, 0 };

            lvPrevious.BeginUpdate();
            for (int i = 0; i < lvPrevious.Items.Count; i++)
            {
                DiceRolls rolls = (DiceRolls)lvPrevious.Items[i].Tag;
                if (rolls.WeightedTotal < iMinWeight ||
                    rolls.RawTotal < iMinRaw ||
                    rolls.Intellect < minStats[(int)PrimaryStat.Intellect] ||
                    rolls.Might < minStats[(int)PrimaryStat.Might] ||
                    rolls.Personality < minStats[(int)PrimaryStat.Personality] ||
                    rolls.Endurance < minStats[(int)PrimaryStat.Endurance] ||
                    rolls.Speed < minStats[(int)PrimaryStat.Speed] ||
                    rolls.Accuracy < minStats[(int)PrimaryStat.Accuracy] ||
                    rolls.Luck < minStats[(int)PrimaryStat.Luck])
                {
                    rolls = null;
                    lvPrevious.Items.RemoveAt(i);
                    i--;
                }
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
        }

        private void UpdateWorkingMessage()
        {
            if (lvPrevious.Items.Count % 100 == 0)
            {
                labelWorking.Text = String.Format("Working... {0} items in list", lvPrevious.Items.Count);
                Application.DoEvents();
            }
        }

        private void miRemoveUnusableKnight_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Knight));
        }

        private void miRemoveUnusablePaladin_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Paladin));
        }

        private void miRemoveUnusableArcher_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Archer));
        }

        private void miRemoveUnusableCleric_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Cleric));
        }

        private void miRemoveUnusableSorcerer_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Sorcerer));
        }

        private void miGenerate1000_Click(object sender, EventArgs e)
        {
            GenerateRolls(1000);
        }

        private void miDeleteLowerUnweighted_Click(object sender, EventArgs e)
        {
            if (lvPrevious.SelectedItems.Count == 1)
                DeleteLowerRaw();
        }

        private void miMaximum_Click(object sender, EventArgs e)
        {
            while (ModifySelectedAttribute(1, false))
            {
            }

            WriteNewValues();
        }

        private void miMinimum_Click(object sender, EventArgs e)
        {
            while (ModifySelectedAttribute(-1, false))
            {
            }

            WriteNewValues();
        }

        private void miRemoveUnusable_DropDownOpening(object sender, EventArgs e)
        {
            miRemoveUnusableBarbarian.Visible = (Hacker.StatMinimums(GenericClass.Barbarian) != null);
            miRemoveUnusableNinja.Visible = (Hacker.StatMinimums(GenericClass.Ninja) != null);
            miRemoveUnusableRobber.Visible = (Hacker.StatMinimums(GenericClass.Robber) != null);
        }

        private void miRemoveUnusableRobber_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Robber));
        }

        private void miRemoveUnusableNinja_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Ninja));
        }

        private void miRemoveUnusableBarbarian_Click(object sender, EventArgs e)
        {
            RemoveByMinimumStat(0, 0, Hacker.StatMinimums(GenericClass.Barbarian));
        }

        private void btnUpdateGameUI_Click(object sender, EventArgs e)
        {
            m_bAcceptNewValues = false;
            m_iSkipUpdates = 5;
            if (Properties.Settings.Default.RefreshDOSBox)
                Hacker.RefreshRollScreen();
        }

        private void miBonusCopyTable_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in lvBonusTable.Items)
                sb.AppendFormat("{0}\t{1}\r\n", lvi.Text, lvi.SubItems[1].Text);
            Clipboard.SetText(sb.ToString());
        }

        private void comboAttrib_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrimaryStat statUpdate = PrimaryStat.None;
            switch (comboAttrib.SelectedIndex)
            {
                case 0: statUpdate = PrimaryStat.Might; break;
                case 1: statUpdate = PrimaryStat.Intellect; break;
                case 2: statUpdate = PrimaryStat.Personality; break;
                case 3: statUpdate = PrimaryStat.Endurance; break;
                case 4: statUpdate = PrimaryStat.Speed; break;
                case 5: statUpdate = PrimaryStat.Accuracy; break;
                case 6: statUpdate = PrimaryStat.Luck; break;
            }

            Global.UpdateBonusTable(lvBonusTable, Hacker, statUpdate);
            if (statUpdate != PrimaryStat.None)
                gbBonusTable.Text = String.Format("Bonus Table: {0}", Global.StatString(statUpdate));
            else
                gbBonusTable.Text = "Bonus Table";
        }
    }
}