using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class BTCreationAssistantControl : CreationAssistantControl
    {
        private int m_iPointsRemaining = 0;
        private byte[] m_oldStatsOrig = null;
        private byte[] m_oldStatsMod = null;
        private bool m_bAcceptNewValues = false;
        private MainState m_state = MainState.Unknown;
        private List<BTDiceRolls> m_prevRolls;
        private int m_iSkipUpdates = 0;
        private int m_iErrorGraceTimer = 0;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bIgnoreNextRoll = false;
        public int[] WeightTable;
        private int m_highestWeight = 1;
        private PrimaryStat[] m_statOrder = null;
        private CharCreationInfo m_lastInfo = null;
        private bool m_bTimerEnabled = true;
        private int m_iOrigClassHeight = 0;

        public BTCreationAssistantControl()
        {
            InitializeComponent();
            m_iOrigClassHeight = gbClasses.Height;

            Init();
        }

        private void Init()
        {
            m_prevRolls = new List<BTDiceRolls>();
            NativeMethods.SetTooltipDelay(lvStats, 32000);
        }

        public BTCreationAssistantControl(IMain main)
        {
            InitializeComponent();
            m_iOrigClassHeight = gbClasses.Height;

            SetMain(main);
            Init();
        }

        public override bool TimerEnabled { get { return m_bTimerEnabled; } }
        private string StatRange(int iMin) { return String.Format("{0}-{1}", iMin, Math.Min(iMin + (IsBT3 ? 7 : 6), (IsBT3 ? 19 : 18))); }

        private string StatRanges(GenericRace race)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("St {0}, ", StatRange(MinStat(race, PrimaryStat.Strength)));
            sb.AppendFormat("IQ {0}, ", StatRange(MinStat(race, PrimaryStat.IQ)));
            sb.AppendFormat("Dx {0}, ", StatRange(MinStat(race, PrimaryStat.Dexterity)));
            sb.AppendFormat("Cn {0}, ", StatRange(MinStat(race, PrimaryStat.Constitution)));
            sb.AppendFormat("Lk {0}", StatRange(MinStat(race, PrimaryStat.Luck)));
            return sb.ToString();
        }

        protected override void OnMainSet()
        {
            m_statOrder = Hacker.StatOrder;
            int[] Weights = Hacker.CreationStatWeights;
            WeightTable = new int[Weights.Length];
            WeightTable[0] = Weights[0];
            for (int i = 1; i < Weights.Length; i++)
                WeightTable[i] = WeightTable[i - 1] + Weights[i];

            m_highestWeight = Weights[Weights.Length - 1];

            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.None, Hacker.Game == GameNames.BardsTale3 ? 30 : 18);

            labelHuman.Text = StatRanges(GenericRace.Human) + "; " + Hacker.GetRaceDescription(GenericRace.Human);
            labelElf.Text = StatRanges(GenericRace.Elf) + "; " + Hacker.GetRaceDescription(GenericRace.Elf);
            labelDwarf.Text = StatRanges(GenericRace.Dwarf) + "; " + Hacker.GetRaceDescription(GenericRace.Dwarf);
            labelHobbit.Text = StatRanges(GenericRace.Hobbit) + "; " + Hacker.GetRaceDescription(GenericRace.Hobbit);
            labelHalfElf.Text = StatRanges(GenericRace.HalfElf) + "; " + Hacker.GetRaceDescription(GenericRace.HalfElf);
            labelHalfOrc.Text = StatRanges(GenericRace.HalfOrc) + "; " + Hacker.GetRaceDescription(GenericRace.HalfOrc);
            labelGnome.Text = StatRanges(GenericRace.Gnome) + "; " + Hacker.GetRaceDescription(GenericRace.Gnome);

            labelWarrior.Text = Hacker.GetClassDescription(GenericClass.Warrior);
            labelPaladin.Text = Hacker.GetClassDescription(GenericClass.Paladin);
            labelRogue.Text = Hacker.GetClassDescription(GenericClass.Rogue);
            labelBard.Text = Hacker.GetClassDescription(GenericClass.Bard);
            labelHunter.Text = Hacker.GetClassDescription(GenericClass.Hunter);
            labelMonk.Text = Hacker.GetClassDescription(GenericClass.Monk);
            labelConjurer.Text = Hacker.GetClassDescription(GenericClass.Conjurer);
            labelMagician.Text = Hacker.GetClassDescription(GenericClass.Magician);
            labelSorcerer.Text = Hacker.GetClassDescription(GenericClass.Sorcerer);
            labelWizard.Text = Hacker.GetClassDescription(GenericClass.Wizard);
            labelArchmage.Text = Hacker.GetClassDescription(GenericClass.Archmage);
            labelChronomancer.Text = Hacker.GetClassDescription(GenericClass.Chronomancer);
            labelGeomancer.Text = Hacker.GetClassDescription(GenericClass.Geomancer);

            if (IsBT3)
                labelWeightBasedOn.Text = "Values are obtained by rolling 1d8 and adding the race minimum for that stat.";

            UpdateListTitle();
        }

        private int GetWeightedTotal(byte[] rolls)
        {
            int iTotal = 0;
            for (int i = 0; i < rolls.Length; i++)
                iTotal += GetWeightedValue(rolls[i]);
            return iTotal;
        }

        public override void OnLoad()
        {
            comboAttrib.BeginUpdate();
            comboAttrib.Items.Clear();
            comboAttrib.Items.Add("Strength");
            comboAttrib.Items.Add("IQ");
            comboAttrib.Items.Add("Dexterity");
            comboAttrib.Items.Add("Constitution");
            comboAttrib.Items.Add("Luck");
            comboAttrib.SelectedIndex = 3;  // Constitution
            comboAttrib.EndUpdate();
        }

        private void lvStats_KeyDown(object sender, KeyEventArgs e)
        {
            // There isn't really a good weighting system for Bard's Tale stats,
            // so that functionality isn't enabled here.
            //switch (e.KeyCode)
            //{
            //    case Keys.Add:
            //        ModifySelectedAttribute(1, true);
            //        break;
            //    case Keys.Subtract:
            //        ModifySelectedAttribute(-1, true);
            //        break;
            //    default:
            //        break;
            //}
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

        CharCreationInfo CreateInfo()
        {
            return new BTCharCreationInfo();
        }

        private void WriteNewValues()
        {
            CharCreationInfo info = CreateInfo();
            info.AttributesModified = Global.NullBytes(6);
            info.AttributesOriginal = Global.NullBytes(6);
            for (int i = 0; i < 6; i++)
            {
                DiceRoll roll = (DiceRoll)lvStats.Items[i].Tag;
                info.AttributesOriginal[i] = (byte)roll.Original;
                info.AttributesModified[i] = (byte)roll.Modified;
            }
            m_bAcceptNewValues = true;
            m_iSkipUpdates = 1;

            Hacker.SetCharCreationInfo(info);

            if (Properties.Settings.Default.RefreshDOSBox)
                Hacker.RefreshRollScreen();
        }

        public override void TimerTick()
        {
            if (m_iSkipUpdates > 0)
            {
                m_iSkipUpdates--;
                return;
            }

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

                panelInvalidRolls.Width = gbUsingRolls.Right - panelInvalidRolls.Left;
                panelInvalidRolls.Visible = true;
                panelInvalidRolls.BringToFront();
                return;
            }
            else
            {
                panelInvalidRolls.Visible = false;
            }

            m_lastInfo = info;

            if (!m_bAcceptNewValues)
            {
                bool bStatsSame = Global.Compare(info.AttributesModified, m_oldStatsMod) && Global.Compare(info.AttributesOriginal, m_oldStatsOrig);
                if (bStatsSame && m_state == info.State.Main)
                    return;

                if (!bStatsSame)
                {
                    if (m_bIgnoreNextRoll)
                        m_bIgnoreNextRoll = false;
                    else
                        AddPreviousRollToList(new BTDiceRolls(info.AttributesOriginal, info.Race));
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

        ListViewItem GetRollsLVI(BTDiceRolls rolls)
        {
            ListViewItem lvi = new ListViewItem(rolls.RawTotal.ToString());
            lvi.SubItems.Add(rolls.Strength.ToString());
            lvi.SubItems.Add(rolls.IQ.ToString());
            lvi.SubItems.Add(rolls.Dexterity.ToString());
            lvi.SubItems.Add(rolls.Constitution.ToString());
            lvi.SubItems.Add(rolls.Luck.ToString());
            lvi.SubItems.Add(rolls.HP.ToString());
            lvi.SubItems.Add(Race.RaceString(rolls.Race));
            lvi.Tag = rolls;
            return lvi;
        }

        private void AddPreviousRollToList(BTDiceRolls rolls)
        {
            // Don't add this roll if it's exactly the same as the previous one in the list
            if (lvPrevious.Items.Count > 0)
            {
                BTDiceRolls lastRolls = (BTDiceRolls)lvPrevious.Items[lvPrevious.Items.Count - 1].Tag;
                if (lastRolls.Equals(rolls))
                    return;
            }

            m_prevRolls.Add(rolls);

            ListViewItem lvi = GetRollsLVI(rolls);

            lvPrevious.ListViewItemSorter = null;

            lvPrevious.Items.Add(lvi);

            lvPrevious.EnsureVisible(lvi.Index);

            UpdateListTitle();
        }
        
        private void AddPreviousRollToList(BTDiceRolls[] rolls)
        {
            // Add a pre-curated list of rolls to the list (no comparisons to prior rolls, etc.)
            m_prevRolls.AddRange(rolls);

            ListViewItem[] lvis = new ListViewItem[rolls.Length];
            for (int i = 0; i < rolls.Length; i++)
                lvis[i] = GetRollsLVI(rolls[i]);

            lvPrevious.ListViewItemSorter = null;

            if (rolls.Length > 10000)
                StartWorking();

            lvPrevious.Items.AddRange(lvis);

            EndWorking();

            lvPrevious.EnsureVisible(lvPrevious.Items.Count - 1);

            UpdateListTitle();
        }

        private void UpdateItem(ListViewItem lvi, BTDiceRolls rolls)
        {
            lvi.Text = rolls.RawTotal.ToString();
            lvi.SubItems[1].Text = rolls.Strength.ToString();
            lvi.SubItems[2].Text = rolls.IQ.ToString();
            lvi.SubItems[3].Text = rolls.Dexterity.ToString();
            lvi.SubItems[4].Text = rolls.Constitution.ToString();
            lvi.SubItems[5].Text = rolls.Luck.ToString();
            lvi.SubItems[6].Text = rolls.HP.ToString();
            lvi.SubItems[7].Text = Race.RaceString(rolls.Race);
            lvi.Tag = rolls;
        }

        private void UpdateListTitle()
        {
            gbPreviousRolls.Text = String.Format("Previous Rolls ({0})", lvPrevious.Items.Count);
        }

        private void UpdateUI(CharCreationInfo info)
        {
            if (info.AttributesOriginal == null || info.AttributesModified == null)
                return;

            if (info.AttributesModified.Length < 6 || info.AttributesOriginal.Length < 6)
                return;

            int iTotal = 0;
            int iTotalWeighted = 0;

            int iSelected = lvStats.FocusedItem != null ? lvStats.FocusedItem.Index : -1;

            lvStats.BeginUpdate();
            lvStats.Items.Clear();
            for(int i = 0; i < m_statOrder.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(Global.StatString(m_statOrder[i]));
                lvi.SubItems.Add(GetStatString(info.AttributesOriginal[i], info.AttributesModified[i]));
                lvi.Tag = new DiceRoll(m_statOrder[i], info.AttributesOriginal[i], info.AttributesModified[i]);
                iTotal += info.AttributesOriginal[i];
                iTotalWeighted += GetWeightedValue(info.AttributesOriginal[i]);
                lvi.ToolTipText = GetStatTooltip(i, info.AttributesModified[i]);
                lvStats.Items.Add(lvi);
            }
            ListViewItem lviHP = new ListViewItem("HP");
            lviHP.SubItems.Add(String.Format("{0}", info.AttributesOriginal[5]));
            lviHP.Tag = new DiceRoll(PrimaryStat.HitPoints, info.AttributesOriginal[5], info.AttributesModified[5]);
            lviHP.ToolTipText = "The character's starting HP";
            lvStats.Items.Add(lviHP);
            lvStats.EndUpdate();

            if (iSelected > -1)
            {
                lvStats.Items[iSelected].Focused = true;
                lvStats.Items[iSelected].Selected = true;
            }

            UpdateButtons();
        }

        private string GetStatTooltip(int i, int stat)
        {
            return Hacker.StatToolTip(i, stat);
        }

        private void UpdateButtons()
        {
        }

        public int GetWeightedValue(int iValue)
        {
            if (iValue > WeightTable.Length - 1)
                return WeightTable[WeightTable.Length - 1] + (iValue - WeightTable.Length - 1) * m_highestWeight;

            return WeightTable[iValue];
        }

        private string GetStatString(byte original, byte modified)
        {
            if (original == modified)
                return String.Format("{0}", original);
            if (original > modified)
                return String.Format("{0}{1}", original, modified - original);
            return String.Format("{0}+{1}", original, modified - original);
        }

        private bool IsBT3 { get { return Hacker != null && Hacker.Game == GameNames.BardsTale3; } }

        private void AdjustRolls(GenericRace raceFrom, GenericRace raceTo)
        {
            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            lvPrevious.BeginUpdate();
            for(int i = lvPrevious.Items.Count - 1; i >= 0; i--)
            {
                BTDiceRolls rolls = lvPrevious.Items[i].Tag as BTDiceRolls;
                if (rolls == null)
                    lvPrevious.Items.RemoveAt(i);

                rolls.Strength = (byte) (rolls.Strength + (MinStat(raceTo, PrimaryStat.Strength) - MinStat(raceFrom, PrimaryStat.Strength)));
                Global.FixRange(ref rolls.Strength, 3, (byte) (IsBT3 ? 19 : 18));
                rolls.Strength = (byte)(rolls.IQ + (MinStat(raceTo, PrimaryStat.IQ) - MinStat(raceFrom, PrimaryStat.IQ)));
                Global.FixRange(ref rolls.IQ, 3, 18);
                rolls.Strength = (byte)(rolls.Dexterity + (MinStat(raceTo, PrimaryStat.Dexterity) - MinStat(raceFrom, PrimaryStat.Dexterity)));
                Global.FixRange(ref rolls.Dexterity, 3, 18);
                rolls.Strength = (byte)(rolls.Constitution + (MinStat(raceTo, PrimaryStat.Constitution) - MinStat(raceFrom, PrimaryStat.Constitution)));
                Global.FixRange(ref rolls.Constitution, 3, 18);
                rolls.Strength = (byte)(rolls.Luck + (MinStat(raceTo, PrimaryStat.Luck) - MinStat(raceFrom, PrimaryStat.Luck)));
                Global.FixRange(ref rolls.Luck, 3, 18);

                UpdateItem(lvPrevious.Items[i], rolls);
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();

            UpdateListTitle();

        }

        public override bool UsedAllPoints { get { return m_iPointsRemaining == 0; } }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            ModifySelectedAttribute(1, true);
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            ModifySelectedAttribute(-1, true);
        }

        private void lvStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void LoadRolls(BTDiceRolls rolls)
        {
            if (Hacker == null || !Hacker.IsValid)
                return;

            BTCharCreationInfo info = Hacker.GetCharCreationInfo() as BTCharCreationInfo;
            if (info == null)
                return;

            if (Global.Compare(info.AttributesOriginal, rolls.GetBytes(Hacker.StatOrder)))
                return;

            if (info.Race == GenericRace.None || !info.ValidValues)
                return;

            if (info.Race != rolls.Race)
            {
                MessageBox.Show(String.Format("The rolls you selected were rolled for the \"{0}\" race.  " + 
                    "The game is currently expecting values for the \"{1}\" race.\r\n\r\n" +
                    "Please change your selected race in the game if you wish to use these rolls.",
                    Race.RaceString(rolls.Race), Race.RaceString(info.Race)),
                    "Race Changed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            BTCharCreationInfo infoNew = CreateInfo() as BTCharCreationInfo;

            infoNew.AttributesModified = rolls.GetBytes(Hacker.StatOrder);
            infoNew.AttributesOriginal = rolls.GetBytes(Hacker.StatOrder);
            infoNew.MemoryOffset = info.MemoryOffset;

            m_iSkipUpdates = 5;
            m_bIgnoreNextRoll = true;
            m_iPointsRemaining = 0;

            Hacker.SetCharCreationInfo(infoNew);

            if (Properties.Settings.Default.RefreshDOSBox)
                Hacker.RefreshRollScreen();
        }

        private void lvPrevious_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvPrevious.ListViewItemSorter = new BTDiceRollsComparer(e.Column, m_bAscendingSort);
            lvPrevious.Sort();
        }

        private void cmPrevious_Opening(object sender, CancelEventArgs e)
        {
            miDelete.Enabled = (lvPrevious.SelectedItems.Count > 0);
            miDeleteExcept.Enabled = (lvPrevious.SelectedItems.Count > 0);
            miDeleteLowerUnweighted.Enabled = (lvPrevious.SelectedItems.Count == 1);
        }

        private void DeleteSelected()
        {
            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            Global.DeleteSelectedItems(lvPrevious, UpdateWorkingMessage);
            EndWorking();
            UpdateListTitle();
        }

        private void DeleteUnselected()
        {
            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            Global.DeleteUnselectedItems(lvPrevious, UpdateWorkingMessage);
            EndWorking();
            UpdateListTitle();
        }

        private void DeleteLowerRaw()
        {
            if (lvPrevious.FocusedItem == null)
                return;

            BTDiceRolls rolls = (BTDiceRolls)lvPrevious.FocusedItem.Tag;

            RemoveByMinimumStat(0, rolls.RawTotal, null);

            UpdateListTitle();
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
                case Keys.R:
                    if (e.Shift && e.Control)
                    {
                        GenerateRolls(100000);
                        e.Handled = true;
                    }
                    break;
                case Keys.A:
                    if (e.Control)
                    {
                        lvPrevious.BeginUpdate();
                        foreach (ListViewItem lvi in lvPrevious.Items)
                            lvi.Selected = true;
                        lvPrevious.EndUpdate();
                    }
                    break;
                case Keys.S:
                    if (e.Control)
                        ShowStats();
                    break;
                default:
                    break;
            }
        }

        private string Statistics(string strHeader, int[] stats)
        {
            StringBuilder sb = new StringBuilder(strHeader + ":");
            for (int i = 3; i < stats.Length; i++)
                sb.AppendFormat("{0,5}", stats[i]);

            return sb.ToString();
        }

        private void ShowStats()
        {
            int iCount = 0;
            int[] strength = new int[19];
            int[] iq = new int[19];
            int[] dexterity = new int[19];
            int[] constitution = new int[19];
            int[] luck = new int[19];
            int[] hp = new int[40];
            int totalhp = 0;

            foreach (ListViewItem lvi in lvPrevious.Items)
            {
                BTDiceRolls rolls = lvi.Tag as BTDiceRolls;
                if (rolls == null)
                    continue;

                iCount++;
                if (rolls.Strength < 19)
                    strength[rolls.Strength]++;
                if (rolls.IQ < 19)
                    iq[rolls.IQ]++;
                if (rolls.Dexterity < 19)
                    dexterity[rolls.Dexterity]++;
                if (rolls.Constitution < 19)
                    constitution[rolls.Constitution]++;
                if (rolls.Luck < 19)
                    luck[rolls.Luck]++;
                if (rolls.HP < 40)
                    hp[rolls.HP]++;
                totalhp += rolls.HP;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Statistics for {0} rolls:\r\n\r\n", iCount);
            sb.Append(Statistics("Val", Global.IntRange(0, 19)));
            sb.AppendLine();
            sb.Append(Statistics("Str", strength));
            sb.AppendLine();
            sb.Append(Statistics("IQ ", iq));
            sb.AppendLine();
            sb.Append(Statistics("Dex", dexterity));
            sb.AppendLine();
            sb.Append(Statistics("Con", constitution));
            sb.AppendLine();
            sb.Append(Statistics("Lck", luck));
            sb.AppendLine();

            sb.Append(Statistics("HP", Global.IntRange(0, 40)));
            sb.AppendLine();
            sb.Append(Statistics("HP", hp));
            sb.AppendLine();


            ViewInfoForm.Show(sb.ToString(), "Statistics");
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

        private void miGenerate100_Click(object sender, EventArgs e)
        {
            GenerateRolls(100);
        }

        private int MinStat(GenericRace race, PrimaryStat stat)
        {
            return MinStat_BT2(race, stat) - (IsBT3 ? 1 : 0);
        }

        private int MinStat_BT2(GenericRace race, PrimaryStat stat)
        {
            switch (race)
            {
                case GenericRace.Dwarf:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 13;
                        case PrimaryStat.IQ: return 7;
                        case PrimaryStat.Dexterity: return 8;
                        case PrimaryStat.Constitution: return 11;
                        case PrimaryStat.Luck: return 4;
                    }
                    break;
                case GenericRace.Elf:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 9;
                        case PrimaryStat.IQ: return 10;
                        case PrimaryStat.Dexterity: return 10;
                        case PrimaryStat.Constitution: return 7;
                        case PrimaryStat.Luck: return 7;
                    }
                    break;
                case GenericRace.Gnome:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 10;
                        case PrimaryStat.IQ: return 11;
                        case PrimaryStat.Dexterity: return 8;
                        case PrimaryStat.Constitution: return 4;
                        case PrimaryStat.Luck: return 5;
                    }
                    break;
                case GenericRace.HalfElf:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 10;
                        case PrimaryStat.IQ: return 9;
                        case PrimaryStat.Dexterity: return 10;
                        case PrimaryStat.Constitution: return 8;
                        case PrimaryStat.Luck: return 7;
                    }
                    break;
                case GenericRace.HalfOrc:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 12;
                        case PrimaryStat.IQ: return 4;
                        case PrimaryStat.Dexterity: return 9;
                        case PrimaryStat.Constitution: return 12;
                        case PrimaryStat.Luck: return 5;
                    }
                    break;
                case GenericRace.Hobbit:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 5;
                        case PrimaryStat.IQ: return 7;
                        case PrimaryStat.Dexterity: return 13;
                        case PrimaryStat.Constitution: return 6;
                        case PrimaryStat.Luck: return 11;
                    }
                    break;
                case GenericRace.Human:
                    switch (stat)
                    {
                        case PrimaryStat.Strength: return 11;
                        case PrimaryStat.IQ: return 7;
                        case PrimaryStat.Dexterity: return 9;
                        case PrimaryStat.Constitution: return 9;
                        case PrimaryStat.Luck: return 6;
                    }
                    break;
            }
            return 4;
        }

        private int BTRandomStat(int iMin)
        {
            if (IsBT3)
                return Math.Min(19, iMin + Global.Rand.Next(8));
            else
                return Math.Min(18, iMin + Math.Max(Global.Rand.Next(7), Global.Rand.Next(7)));
        }

        private int BTRandomHitPoints()
        {
            if (IsBT3)
                return 13 + Math.Max(Global.Rand.Next(16), Global.Rand.Next(16));
            else
                return 15 + Math.Max(Global.Rand.Next(15), Global.Rand.Next(15));
        }

        private void GenerateRolls(int iNum)
        {
            if (m_lastInfo == null)
            {
                MessageBox.Show("Rolls cannot be generated until a race has been selected in-game.", "No Race Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            BTDiceRolls[] allRolls = new BTDiceRolls[iNum];

            for (int i = 0; i < iNum; i++)
            {
                byte[] bytes = new byte[6];
                bytes[0] = (byte)BTRandomStat(MinStat(m_lastInfo.Race, PrimaryStat.Strength));
                bytes[1] = (byte)BTRandomStat(MinStat(m_lastInfo.Race, PrimaryStat.IQ));
                bytes[2] = (byte)BTRandomStat(MinStat(m_lastInfo.Race, PrimaryStat.Dexterity));
                bytes[3] = (byte)BTRandomStat(MinStat(m_lastInfo.Race, PrimaryStat.Constitution));
                bytes[4] = (byte)BTRandomStat(MinStat(m_lastInfo.Race, PrimaryStat.Luck));
                bytes[5] = (byte) BTRandomHitPoints();
                allRolls[i] = new BTDiceRolls(bytes, m_lastInfo.Race);
            }

            lvPrevious.BeginUpdate();
            AddPreviousRollToList(allRolls);
            lvPrevious.EndUpdate();
        }

        private void useTheseRollsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvPrevious.FocusedItem != null)
                LoadRolls((BTDiceRolls)lvPrevious.FocusedItem.Tag);
        }

        private void RemoveByMinimumStat(int iMinWeight, int iMinRaw, int[] minStats)
        {
            if (lvPrevious.Items.Count > 500)
                StartWorking();

            if (minStats == null)
                minStats = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            List<ListViewItem> itemsKeep = new List<ListViewItem>();

            for(int i = 0; i < lvPrevious.Items.Count; i++)
            {
                BTDiceRolls rolls = (BTDiceRolls)lvPrevious.Items[i].Tag;
                if (!(rolls.RawTotal < iMinRaw ||
                    rolls.Strength < minStats[(int) PrimaryStat.Strength] ||
                    rolls.IQ < minStats[(int)PrimaryStat.IQ] ||
                    rolls.Dexterity < minStats[(int)PrimaryStat.Dexterity] ||
                    rolls.Constitution < minStats[(int)PrimaryStat.Constitution] ||
                    rolls.Luck < minStats[(int)PrimaryStat.Luck]))
                {
                    itemsKeep.Add(lvPrevious.Items[i]);
                }
            }

            lvPrevious.BeginUpdate();
            lvPrevious.Items.Clear();
            lvPrevious.Items.AddRange(itemsKeep.ToArray());
            lvPrevious.EndUpdate();
            EndWorking();
            UpdateListTitle();
        }

        private void UpdateWorkingMessage()
        {
            if (lvPrevious.Items.Count % 100 == 0)
            {
                labelWorking.Text = String.Format("Working... {0} items in list", lvPrevious.Items.Count);
                Application.DoEvents();
            }
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
            switch(comboAttrib.SelectedIndex)
            {
                case 0: statUpdate = PrimaryStat.Strength; break;
                case 1: statUpdate = PrimaryStat.IQ; break;
                case 2: statUpdate = PrimaryStat.Dexterity; break;
                case 3: statUpdate = PrimaryStat.Constitution; break;
                case 4: statUpdate = PrimaryStat.Luck; break;
            }

            Global.UpdateBonusTable(lvBonusTable, Hacker, statUpdate, Hacker.Game == GameNames.BardsTale3 ? 30 : 19);
            if (statUpdate != PrimaryStat.None)
                gbBonusTable.Text = String.Format("Bonus for: {0}", Global.StatString(statUpdate));
            else
                gbBonusTable.Text = "Bonus Table";
        }

        private void lvPrevious_DoubleClick(object sender, EventArgs e)
        {
            if (lvPrevious.SelectedItems.Count < 1)
                return;

            LoadRolls(lvPrevious.SelectedItems[0].Tag as BTDiceRolls);
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            string strLines = Global.GetText(lvPrevious, lvPrevious.SelectedItems.Count > 0);
            if (String.IsNullOrWhiteSpace(strLines))
                return;
            Clipboard.SetText(strLines);
        }

        private void cmStats_Opening(object sender, CancelEventArgs e)
        {
            // Not really a good way to allow "fair" stat adjustment for Bard's Tale.
            e.Cancel = true;
        }
    }

    public class BTDiceRolls
    {
        public byte IQ;
        public byte Strength;
        public byte Dexterity;
        public byte Constitution;
        public byte Luck;
        public byte HP;
        public GenericRace Race;

        public BTDiceRolls(byte[] attributes, GenericRace race)
        {
            Strength = attributes[0];
            IQ = attributes[1];
            Dexterity = attributes[2];
            Constitution = attributes[3];
            Luck = attributes[4];
            HP = attributes[5];
            Race = race;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BTDiceRolls))
                return false;

            BTDiceRolls rolls = obj as BTDiceRolls;

            return (IQ == rolls.IQ &&
                Strength == rolls.Strength &&
                Dexterity == rolls.Dexterity &&
                Constitution == rolls.Constitution &&
                Luck == rolls.Luck &&
                HP == rolls.HP &&
                Race == rolls.Race);
        }

        public override int GetHashCode()
        {
            return (IQ << 24) |
                (Strength << 16) |
                (Dexterity << 8) |
                (Constitution) |
                (HP << 16) |
                (Luck << 8) |
                (int) Race;
        }

        public byte[] GetBytes(PrimaryStat[] order)
        {
            return new byte[] { (byte) Strength, (byte) IQ, (byte) Dexterity, (byte) Constitution, (byte) Luck, (byte) HP };
        }

        public override string ToString()
        {
            return String.Format("Str:{0} IQ:{1} Dex:{2} Con:{3} Luck:{4} HP:{5}",
                Strength, IQ, Dexterity, Constitution, Luck, HP);
        }

        public int RawTotal
        {
            get
            {
                return Strength + IQ  + Dexterity + Constitution + Luck;
            }
        }
    }
    class BTDiceRollsComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public BTDiceRollsComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }

        public BTDiceRollsComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            BTDiceRolls rolls1 = ((ListViewItem)x).Tag as BTDiceRolls;
            BTDiceRolls rolls2 = ((ListViewItem)y).Tag as BTDiceRolls;

            switch (m_column)
            {
                case 0:
                    returnVal = Math.Sign(rolls1.RawTotal - rolls2.RawTotal);
                    break;
                case 1:
                    returnVal = Math.Sign(rolls1.Strength - rolls2.Strength);
                    break;
                case 2:
                    returnVal = Math.Sign(rolls1.IQ - rolls2.IQ);
                    break;
                case 3:
                    returnVal = Math.Sign(rolls1.Dexterity - rolls2.Dexterity);
                    break;
                case 4:
                    returnVal = Math.Sign(rolls1.Constitution - rolls2.Constitution);
                    break;
                case 5:
                    returnVal = Math.Sign(rolls1.Luck - rolls2.Luck);
                    break;
                case 6:
                    returnVal = Math.Sign(rolls1.HP - rolls2.HP);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }
}
