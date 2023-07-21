using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WhereAreWe
{
    public partial class Wiz1CreationAssistantControl : CreationAssistantControl
    {
        private Wiz1MemoryHacker WizHacker { get { return Hacker as Wiz1MemoryHacker; } }
        private Wiz1CharCreationInfo m_infoLast = null;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bResetBonus = true;
        private Thread m_threadReroll = null;

        public Wiz1CreationAssistantControl()
        {
            InitializeComponent();
            Init();
        }

        public Wiz1CreationAssistantControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
            comboAutoAlignment.Items.Clear();
            comboAutoRace.Items.Clear();
            for (Wiz1Alignment align = Wiz1Alignment.Good; align < Wiz1Alignment.Last; align++)
                comboAutoAlignment.Items.Add(Wiz1Character.AlignmentString(align));
            for (Wiz1Race race = Wiz1Race.Human; race < Wiz1Race.Last; race++)
                comboAutoRace.Items.Add(Wiz1Character.RaceString(race));
            comboAutoAlignment.SelectedIndex = 0;
            comboAutoRace.SelectedIndex = 0;
            Win32.SetTooltipDelay(lvStats, 30000);
        }

        protected override void OnMainSet()
        {
            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.Vitality);

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
        }

        private void InvalidRolls(bool bShow)
        {
            if (bShow)
            {
                panelInvalidRolls.Visible = true;
                panelInvalidRolls.BringToFront();
            }
            else
            {
                panelInvalidRolls.Visible = false;
                panelInvalidRolls.SendToBack();
            }
        }

        public override void OnLoad()
        {
            comboAttrib.SelectedIndex = 3;
        }

        public override void TimerTick()
        {
            if (WizHacker == null)
            {
                btnCreate.Enabled = false;
                InvalidRolls(true);
                return;
            }

            Wiz1CharCreationInfo info = WizHacker.GetCharCreationInfo() as Wiz1CharCreationInfo;
            if (info == null || !info.ValidValues)
            {
                InvalidRolls(true);
                btnCreate.Enabled = tbName.Text.Length > 0;
                m_bResetBonus = true;
                return;
            }

            switch (info.State.Main)
            {
                case MainState.CreateExchangeStat:
                case MainState.CreateSelectClass:
                case MainState.CreateKeepChar:
                case MainState.Training:
                    break;
                default:
                    InvalidRolls(true);
                    btnCreate.Enabled = false;
                    return;
            }
            
            InvalidRolls(false);
            btnCreate.Enabled = tbName.Text.Length > 0;

            if (m_infoLast != null && Global.CompareBytes(info.AttributesOriginal, m_infoLast.AttributesOriginal))
                return; // No changes

            m_infoLast = info;

            lvStats.BeginUpdate();
            lvStats.Items.Clear();
            for(int i = 0; i < 6; i++)
            {
                ListViewItem lvi = new ListViewItem(Global.StatString(StatForIndex(i)));
                lvi.SubItems.Add(info.AttributesOriginal[i].ToString());
                lvi.ToolTipText = GetStatTooltip(i, info.AttributesOriginal[i]);
                lvStats.Items.Add(lvi);
            }
            lvStats.EndUpdate();

            labelExtraPoints.Text = info.AttributesOriginal[6].ToString();

            if (m_bResetBonus && info.AttributesOriginal[6] > 4)
                AddBonus(info);
            m_bResetBonus = false;
        }

        private string GetStatTooltip(int i, int stat)
        {
            return Hacker.StatToolTip(i, stat);
        }

        private void AddBonus(Wiz1CharCreationInfo info)
        {
            ListViewItem lvi = CreateLVI(info);
            if (lvi != null)
                lvPrevious.Items.Add(lvi);
        }

        private ListViewItem CreateLVI(Wiz1CharCreationInfo info)
        {
            int iBonus = info.AttributesOriginal[6];
            // Only add "real" bonus values (ones that match the stats)

            int iSum = 0;
            for (int i = 0; i < 6; i++)
                iSum += info.AttributesOriginal[i];

            if (iSum + iBonus < 46 + 5 || iSum + iBonus > 50 + 29)
                return null;
            
            ListViewItem lvi = new ListViewItem(iBonus.ToString());
            lvi.SubItems.Add(String.Format("{0}", iBonus % 10));
            lvi.SubItems.Add(String.Format("{0}", iBonus > 10 ? 10 : 0));
            lvi.SubItems.Add(String.Format("{0}", iBonus > 20 ? 10 : 0));
            lvi.SubItems.Add(String.Format("{0}", info.Gold));
            lvi.Tag = new Wiz1PreviousRollInfo(iBonus, info.Gold);
            return lvi;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Global.Debug && Win32.IsControlDown())
            {
                if (m_threadReroll != null && m_threadReroll.IsAlive)
                {
                    m_threadReroll.Abort();
                    m_threadReroll.Join();
                    return;
                }
                ParameterizedThreadStart ts = new ParameterizedThreadStart(CreateCharThread);
                m_threadReroll = new Thread(ts);
                m_threadReroll.Start(new Tuple<int, int, int>(1000, comboAutoRace.SelectedIndex, comboAutoAlignment.SelectedIndex));
            }
            else
                CreateChar(comboAutoRace.SelectedIndex, comboAutoAlignment.SelectedIndex);
        }

        private void CreateCharThread(object o)
        {
            try
            {
                Tuple<int, int, int> t = (Tuple<int, int, int>)o;
                int iMax = t.Item1;
                for (int i = 0; i < iMax; i++)
                {
                    CreateChar(t.Item2, t.Item3);

                    do
                    {
                        System.Threading.Thread.Sleep(500);
                        Wiz1GameState state = Hacker.GetGameState() as Wiz1GameState;
                        System.Threading.Thread.Sleep(100);
                        if (state.Main == MainState.CreateExchangeStat)
                            break;
                    } while (true);
                }
            }
            catch (Exception)
            {
            }
        }

        private void CreateChar(int iRace, int iAlign)
        {
            if (tbName.Text.Length < 1)
                return;

            Wiz1GameState state = Hacker.GetGameState() as Wiz1GameState;

            switch (state.Main)
            {
                case MainState.CreateExchangeStat:
                    // Need to zero the bonus values and set strength to 11 first
                    WizHacker.SetCharCreationInfo(new Wiz1CharCreationInfo(11, 3, 3, 3, 3, 3, 0, 0, 0));
                    Hacker.SendKeysToDOSBox(new Keys[] { Keys.Down, Keys.Up, Keys.Escape, Keys.A, Keys.N }, true);
                    break;
                case MainState.Training:
                    break;
                default:
                    return;
            }

            Hacker.SendKeysToDOSBox(new Keys[] { Keys.C }, true);

            Hacker.SendStringToDOSBox(tbName.Text.ToLower());

            List<Keys> keys = new List<Keys>();
            keys.Add(Keys.Enter);  // Name
            keys.Add(Keys.Enter);  // Password
            keys.Add(Keys.A + iRace);
            keys.Add(Keys.A + iAlign);
            Hacker.SendKeysToDOSBox(keys.ToArray(), true);
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = tbName.Text.Length > 0;
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
                DeleteLower();
        }

        private void miGenerate100_Click(object sender, EventArgs e)
        {
            GenerateRolls(100);
        }

        private void miGenerate1000_Click(object sender, EventArgs e)
        {
            GenerateRolls(1000);
        }

        private void GenerateRolls(int iNum)
        {
            if (m_infoLast == null)
            {
                MessageBox.Show("Please generate at least one character via the in-game character creator before generating artificial rolls.", "Need a valid character", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem[] list = new ListViewItem[iNum];
            lvPrevious.BeginUpdate();
            for (int i = 0; i < iNum; i++)
            {
                int iBonus = Global.Rand.Next(5, 10);
                while (iBonus < 20 && Global.Rand.Next(11) == 10)
                    iBonus += 10;
                int gold = Global.Rand.Next(4) == 0 ? Global.Rand.Next(100, 116) : Global.Rand.Next(172, 200);
                list[i] = CreateLVI(new Wiz1CharCreationInfo((Wiz1Race)m_infoLast.SelectedRace, iBonus, m_infoLast.SelectedStat, gold));
            }
            lvPrevious.Items.AddRange(list);
            lvPrevious.EndUpdate();
        }

        private void UpdateWorkingMessage()
        {
            if (lvPrevious.Items.Count % 100 == 0)
            {
                labelWorking.Text = String.Format("Working... {0} items in list", lvPrevious.Items.Count);
                Application.DoEvents();
            }
        }

        private void DeleteSelected()
        {
            if (lvPrevious.Items.Count == lvPrevious.SelectedItems.Count)
            {
                lvPrevious.Items.Clear();
                return;
            }

            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            int[] indices = new int[lvPrevious.SelectedItems.Count];
            int iNext = indices.Length - 1;
            foreach (int index in lvPrevious.SelectedIndices)
                indices[iNext--] = index;

            lvPrevious.BeginUpdate();
            foreach (int i in indices)
            {
                lvPrevious.Items.RemoveAt(i);
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
        }

        private void DeleteUnselected()
        {
            if (lvPrevious.Items.Count == 0)
            {
                lvPrevious.Items.Clear();
                return;
            }

            if (lvPrevious.Items.Count > 1000)
                StartWorking();
            int[] indices = new int[lvPrevious.Items.Count - lvPrevious.SelectedItems.Count];
            int iNext = indices.Length - 1;
            foreach (ListViewItem lvi in lvPrevious.Items)
            {
                if (!lvi.Selected)
                    indices[iNext--] = lvi.Index;
            }

            lvPrevious.BeginUpdate();
            foreach (int i in indices)
            {
                lvPrevious.Items.RemoveAt(i);
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
        }

        private void DeleteLower()
        {
            if (lvPrevious.FocusedItem == null)
                return;

            Wiz1PreviousRollInfo rolls = (Wiz1PreviousRollInfo)lvPrevious.FocusedItem.Tag;

            if (lvPrevious.Items.Count > 500)
                StartWorking();

            List<int> toRemove = new List<int>();
            for (int i = lvPrevious.Items.Count - 1; i >= 0; i--)
            {
                Wiz1PreviousRollInfo checkRolls = (Wiz1PreviousRollInfo)lvPrevious.Items[i].Tag;
                if (checkRolls.Bonus < rolls.Bonus)
                {
                    lvPrevious.Items[i].Tag = null;
                    toRemove.Add(i);
                }
            }

            lvPrevious.BeginUpdate();
            foreach (int i in toRemove)
            {
                lvPrevious.Items.RemoveAt(i);
                UpdateWorkingMessage();
            }
            lvPrevious.EndUpdate();
            EndWorking();
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

        private void miUseTheseRolls_Click(object sender, EventArgs e)
        {
            UseSelectedRolls();
        }

        private void UseSelectedRolls()
        {
            CharCreationInfo info = Hacker.GetCharCreationInfo();
            if (!info.OnCharCreation)
            {
                MessageBox.Show("You must be on the character-selection screen to use the selected rolls.", "Can't Use Now", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lvPrevious.SelectedItems.Count < 1)
                return;
            Wiz1PreviousRollInfo rolls = lvPrevious.SelectedItems[0].Tag as Wiz1PreviousRollInfo;
            WizHacker.SetCharCreationInfo(new Wiz1CharCreationInfo((Wiz1Race) m_infoLast.SelectedRace, rolls.Bonus, m_infoLast.SelectedStat, rolls.Gold));
            WizHacker.SendKeysToDOSBox(5, new Keys[] { Keys.Up, Keys.Down, Keys.Enter, Keys.Up, Keys.Down, Keys.Enter, Keys.Up, Keys.Down, Keys.Enter, 
            Keys.Up, Keys.Down, Keys.Enter, Keys.Up, Keys.Down, Keys.Enter, Keys.Up, Keys.Down, Keys.Enter});
        }

        private void cmPrevious_Opening(object sender, CancelEventArgs e)
        {
            bool bAny = lvPrevious.SelectedItems.Count > 0;
            miDelete.Enabled = bAny;
            miDeleteExcept.Enabled = bAny;
            miDeleteLower.Enabled = bAny;
            miUseTheseRolls.Enabled = bAny;
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
                case Keys.Delete:
                    DeleteSelected();
                    break;
                case Keys.F5:
                    if (Global.Debug)
                        ShowStats();
                    break;
                default:
                    break;
            }
        }

        private void ShowStats()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (ListViewItem lvi in lvPrevious.Items)
            {
                Wiz1PreviousRollInfo info = (Wiz1PreviousRollInfo) lvi.Tag;
                if (!dict.ContainsKey(info.Bonus))
                    dict.Add(info.Bonus, 0);
                dict[info.Bonus]++;
            }
            for (int i = 0; i <= dict.Keys.Max(); i++)
            {
                if (dict.ContainsKey(i))
                {
                    sb.AppendFormat("{0,2}:{1,4} ({2,4:F1}%)\r\n", i, dict[i], dict[i] * 100.0 / (double) lvPrevious.Items.Count);
                }
            }
            ViewInfoForm.Show(sb.ToString(), "Statistics");
        }

        private void lvPrevious_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvPrevious.ListViewItemSorter = new Wiz1RollComparer(e.Column, m_bAscendingSort);
            lvPrevious.Sort();
        }

        private void lvPrevious_DoubleClick(object sender, EventArgs e)
        {
            UseSelectedRolls();
        }

        private PrimaryStat StatForIndex(int i)
        {
            switch (i)
            {
                case 0: return PrimaryStat.Strength;
                case 1: return PrimaryStat.IQ;
                case 2: return PrimaryStat.Piety;
                case 3: return PrimaryStat.Vitality;
                case 4: return PrimaryStat.Agility;
                case 5: return PrimaryStat.Luck;
                default: return PrimaryStat.None;
            }
        }

        private void comboAttrib_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.UpdateBonusTable(lvBonusTable, Hacker, StatForIndex(comboAttrib.SelectedIndex));
        }
    }

    public class Wiz1PreviousRollInfo
    {
        public int Bonus;
        public int Gold;

        public Wiz1PreviousRollInfo(int bonus, int gold)
        {
            Bonus = bonus;
            Gold = gold;
        }
    }

    class Wiz1RollComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public Wiz1RollComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }
        public Wiz1RollComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            Wiz1PreviousRollInfo rolls1 = ((ListViewItem)x).Tag as Wiz1PreviousRollInfo;
            Wiz1PreviousRollInfo rolls2 = ((ListViewItem)y).Tag as Wiz1PreviousRollInfo;

            switch (m_column)
            {
                case 0:
                    returnVal = Math.Sign(rolls1.Bonus - rolls2.Bonus);
                    break;
                case 1:
                    returnVal = Math.Sign(rolls1.Bonus % 10 - rolls2.Bonus % 10);
                    break;
                case 2:
                    returnVal = Math.Sign((rolls1.Bonus > 10 ? 10 : 0) - (rolls2.Bonus > 10 ? 10 : 0));
                    break;
                case 3:
                    returnVal = Math.Sign((rolls1.Bonus > 20 ? 10 : 0) - (rolls2.Bonus > 20 ? 10 : 0));
                    break;
                case 4:
                    returnVal = Math.Sign(rolls1.Gold - rolls2.Gold);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }
}
