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
    public partial class EncounterForm : HackerBasedForm
    {
        private int m_iLastSortColumn = -1;
        private int m_iMonitorIndex = -1;
        private int m_iMonitorLastHP = -1;

        private bool m_bAscendingSort = true;
        private bool m_bNeedNewColumnData = false;
        private bool m_bNextUpdateForced = false;

        private EncounterInfo m_infoPrevious = null;
        private CharCombatLabel[] labelChars = new CharCombatLabel[8];
        private SearchResults m_treasurePrevious = null;
        GameInfo m_lastGameInfo = null;
        private DateTime m_dtLastUpdate = DateTime.MinValue;

        private ColumnHeaderList m_colWidths;

        protected override bool ShowWithoutActivation { get { return true; } }

        public EncounterForm()
        {
            InitializeComponent();
            labelChars[0] = ccl1;
            labelChars[1] = ccl2;
            labelChars[2] = ccl3;
            labelChars[3] = ccl4;
            labelChars[4] = ccl5;
            labelChars[5] = ccl6;
            labelChars[6] = ccl7;
            labelChars[7] = ccl8;

            NativeMethods.SetTooltipDelay(lvMonsters, 32000);

            m_colWidths = Properties.Settings.Default.EncounterListColumns;
        }

        public void UpdateUI()
        {
            UpdateUI(m_infoPrevious, true);
        }

        protected override bool OnCommonKeyRefresh()
        {
            m_infoPrevious = null;
            return true;
        }

        protected override bool OnCommonKeySelectAll()
        {
            lvMonsters.BeginUpdate();
            foreach (ListViewItem lvi in lvMonsters.Items)
                lvi.Selected = true;
            lvMonsters.EndUpdate();
            return true;
        }

        public void ForceNextUpdate() { m_bNextUpdateForced = true; }

        public bool UpdateUI(EncounterInfo info, bool bForce = false)
        {
            // Too many updates too quickly spikes the CPU
            if (Hacker == null || (Hacker.RealtimeEncounters && (DateTime.Now - m_dtLastUpdate).TotalMilliseconds < 400))
            {
                if (bForce)
                    m_bNextUpdateForced = true;
                return false;
            }

            if (IsDisposed)
                return false;

            if (m_bNextUpdateForced)
                bForce = true;

            m_bNextUpdateForced = false;

            m_dtLastUpdate = DateTime.Now;

            if (info == null)
            {
                Hide();
                return false;
            }

            if (Properties.Settings.Default.ShowDeadMonsters != miMonsterShowOffMap.Checked)
            {
                miMonsterShowOffMap.Checked = Properties.Settings.Default.ShowDeadMonsters;
                m_infoPrevious = null;
            }

            if (info.NumTotalMonsters == 0 || info.Monsters == null || (!miMonsterShowOffMap.Checked && info.NumLivingMonsters == 0))
            {
                if (info.SearchResults != null && !info.SearchResults.IsEmpty)
                {
                    if (m_treasurePrevious != null && m_treasurePrevious.CompareTo(info.SearchResults) == 0)
                    {
                        if (!ctlTreasure.Visible && !Properties.Settings.Default.ShowEncounters)
                            return false;   // User hid the treasure window manually
                        if (ctlTreasure.Visible)
                            return true;    // Nothing changed and the treasure window is already visible; don't flash the labels
                    }
                    lvMonsters.Hide();
                    splitContainer1.Panel2.Hide();
                    if (WindowState != FormWindowState.Minimized)
                        Global.SetSplitterDistance(splitContainer1, ctlTreasure.Bottom);
                    ctlTreasure.SetMain(m_main);
                    ctlTreasure.SetTreasure(info.SearchResults);
                    Opacity = Properties.Settings.Default.TreasureOpacity / 100.0;
                    m_treasurePrevious = info.SearchResults;
                    Text = "Treasure Available!";
                    if (Properties.Settings.Default.ShowTreasureWindow)
                    {
                        ctlTreasure.BringToFront();
                        ctlTreasure.Show();
                        Show();
                    }
                    else
                    {
                        ctlTreasure.SendToBack();
                        ctlTreasure.Hide();
                        Hide();
                    }
                    return true;
                }
                else
                {
                    m_infoPrevious = null;
                    Hide();
                    return false;
                }
            }

            ctlTreasure.SendToBack();

            if (!bForce && m_infoPrevious != null && m_infoPrevious.CompareTo(info) == 0 && lvMonsters.Visible)
                return false;

            Opacity = 1.0;
            bool bCreateNewList = !(info is MM345EncounterInfo);

            lvMonsters.Show();
            splitContainer1.Panel2.Show();
            ctlTreasure.Hide();

            HashSet<int> m_prevSelected = new HashSet<int>();
            foreach(ListViewItem lvi in lvMonsters.SelectedItems)
                m_prevSelected.Add(((EncounterItemTag)lvi.Tag).Monster.EncounterIndex);
            Point ptScroll = lvMonsters.ScrollPosition;

            int iMelee = 0;
            int iActive = 0;
            int iDead = 0;
            int iAlive = 0;

            bool bUseProximity = (info is MM345EncounterInfo || info is EOBEncounterInfo || info is UltimaEncounterInfo);
            bool bHidAnyDistant = false;

            List<ListViewItem> lviList = new List<ListViewItem>();

            foreach (Monster monster in info.Monsters.Values)
            {
                if (monster.EncounterIndex == m_iMonitorIndex && m_iMonitorLastHP != monster.CurrentHP)
                {
                    if (m_iMonitorLastHP == -1)
                        Global.Log("{0}: {1}", monster.ProperName, monster.CurrentHP);
                    else
                        Global.Log("{0}: {1} ({2})", monster.ProperName, monster.CurrentHP, Global.AddPlus(monster.CurrentHP - m_iMonitorLastHP));
                    m_iMonitorLastHP = monster.CurrentHP;
                }

                string strPrefix = "";
                if (monster is MM1Monster || monster is MM2Monster)
                    strPrefix = String.Format("{0}) ", (char)(lviList.Count + 'A'));
                else if (monster is WizMonster)
                {
                    int iGroupOffset = (monster is Wiz1Monster || monster is Wiz2Monster ? 0 : 1);
                    strPrefix = String.Format("{0}) ", monster.MonsterGroup + iGroupOffset);
                }
                bool bRanged = lviList.Count >= info.NumMeleeMonsters;
                if (info is MM345EncounterInfo)
                    bRanged = !monster.Melee;

                if (!bRanged)
                    iMelee++;

                if (!bUseProximity && monster.Position.X == 0 && monster.Position.Y == 0)
                    monster.Position = info.PartyLocation;

                bool bHideBit = Properties.Settings.Default.HideScriptMonsters && Hacker.IsScriptBitMonster(monster.EncounterIndex);

                if (monster.Killed && !bHideBit)
                    iDead++;

                if ((!monster.Killed || miMonsterShowOffMap.Checked) && !bHideBit)
                {
                    Proximity prox = bUseProximity ? new Proximity(info.PartyLocation, monster.Position) : new Proximity(monster.Distance > 0 ? monster.Distance : lviList.Count);
                    bool bShow = true;
                    if (Properties.Settings.Default.ShowOnlyDetectableMonsters && bUseProximity && prox.Simple >= 4 && !monster.Killed)
                    {
                        bHidAnyDistant = true;
                        bShow = false;
                    }

                    if (info.MonstersOnMap && Properties.Settings.Default.HideUnvisitedSquares &&
                        !Properties.Settings.Default.ShowListMonstersUnexplored &&
                        (!m_main.ShowingCurrentMap || !m_main.SquareIsVisible(m_main.TranslateToInternalMap(monster.Position, null),
                                                                              m_main.TranslateToInternalMap(info.PartyLocation, null), 3)))
                        bShow = false;
                    if (bShow)
                    {
                        if (monster.Active)
                            iActive++;
                        if (!monster.Killed)
                            iAlive++;
                        ListViewItem lvi = NewMonsterLVI(bRanged, info.PreEncounter, monster, strPrefix, prox);
                        lviList.Add(lvi);
                    }
                }
            }

            if (iAlive == 0 && iActive == 0 && iMelee == 0 && (iDead == 0 || !miMonsterShowOffMap.Checked))
            {
                Hide();
                return false;
            }

            if (info is MM345EncounterInfo)
                Text = String.Format("Monsters - {0}{1}, {2} active, {3} in melee{4}", iAlive, bHidAnyDistant ? " in range" : " on map", iActive, iMelee,
                    miMonsterShowOffMap.Checked ? String.Format(", {0} dead", iDead) : "");
            else if (info is Wiz4EncounterInfo)
            {
                Text = "Werdna's Summoned Creatures";
            }
            else
                Text = info.Round > 0 ? String.Format("Combat!  Round {0}", info.Round) : String.Format("Combat!  {0}", info.ExtraTitleText);

            if (info is MM2EncounterInfo)
            {
                MM2EncounterInfo mm2Info = info as MM2EncounterInfo;
                if (mm2Info.MonsterReserves != null && info.NumTotalMonsters > 10)
                {
                    ListViewItem lvi = NewMonsterLVI(true, info.PreEncounter, mm2Info.MonsterReserves, String.Format("+{0} ", info.NumTotalMonsters - 10), new Proximity(10));
                    lviList.Add(lvi);
                }
            }

            bool bClearList = lviList.Count != lvMonsters.Items.Count;

            lvMonsters.BeginUpdate();
            if (!bClearList)
            {
                for (int i = 0; i < lviList.Count; i++)
                {
                    for (int iSub = 0; iSub < lviList[i].SubItems.Count; iSub++)
                    {
                        if (lvMonsters.Items[i].SubItems[iSub].Text != lviList[i].SubItems[iSub].Text)
                            lvMonsters.Items[i].SubItems[iSub].Text = lviList[i].SubItems[iSub].Text;
                    }
                    lvMonsters.Items[i].Tag = lviList[i].Tag;
                    lvMonsters.Items[i].ToolTipText = lviList[i].ToolTipText;
                    lvMonsters.Items[i].Selected = m_prevSelected.Contains(((EncounterItemTag)lvMonsters.Items[i].Tag).Monster.EncounterIndex);
                }
            }
            else
            {
                lvMonsters.Items.Clear();
                lvMonsters.Items.AddRange(lviList.ToArray());
                foreach(ListViewItem lvi in lvMonsters.Items)
                    lvi.Selected = m_prevSelected.Contains(((EncounterItemTag)lvi.Tag).Monster.EncounterIndex);
            }

            int iSplitDistance2 = -1;

            if (info is MM345EncounterInfo || info is UltimaEncounterInfo)
            {
                if (m_iLastSortColumn == -1)
                {
                    m_iLastSortColumn = 1;
                    m_bAscendingSort = true;
                }
                splitContainer2.Panel1Collapsed = true;
                splitContainer1.Panel2Collapsed = (info.NumMeleeMonsters == 0);
            }
            else if (info is Wiz4EncounterInfo)
            {
                splitContainer1.Panel2Collapsed = true;
            }

            GameInfo gameInfo = Hacker.GetGameInfo(m_lastGameInfo);
            TurnOrderCalculator toc = info.GetTurnOrder(labelChars, gameInfo);
            m_lastGameInfo = gameInfo;

            if (!splitContainer2.Panel1Collapsed)
            {
                if (ccl7.Empty && ccl8.Empty)
                {
                    iSplitDistance2 = ccl6.Bottom;
                    if (ccl5.Empty && ccl6.Empty)
                    {
                        iSplitDistance2 = ccl4.Bottom;
                        if (ccl3.Empty && ccl4.Empty)
                        {
                            iSplitDistance2 = ccl2.Bottom;
                            if (ccl1.Empty && ccl2.Empty)
                            {
                                iSplitDistance2 = 0;
                            }
                        }
                    }
                }
            }

            StringBuilder sbExtraText = new StringBuilder();

            if (toc != null)
            {
                string strTurns = toc.GetTurnOrder();
                if (!String.IsNullOrWhiteSpace(strTurns))
                    sbExtraText.AppendFormat("Turn: {0}\r\n", toc.GetTurnOrder());
            }
            sbExtraText.Append(info.ExtraText);
            tbExtraText.Text = sbExtraText.ToString().Trim();

            Global.RestoreColumnOrder(lvMonsters, m_colWidths, cmColumns);
            if (m_colWidths.SortColumn != -1)
            {
                m_iLastSortColumn = m_colWidths.SortColumn;
                m_bAscendingSort = m_colWidths.SortAscending;
                m_colWidths.SortColumn = -1;
                lvMonsters.ListViewItemSorter = new EncounterItemComparer(lvMonsters, m_iLastSortColumn, m_bAscendingSort);
                lvMonsters.Sort();
            }

            if (info is WizEncounterInfo)
            {
                if (chSpeed.Text != "Init")
                {
                    lvMonsters.RemoveSmallImageList();
                    chSpeed.Text = "Init";
                    chAccuracy.Text = "AC+";
                    miColumnsSpeed.Text = "Initiative";
                    miColumnsAccuracy.Text = "AC Modifier";
                }
            }
            else if (info is EOBEncounterInfo)
            {
                if (chAccuracy.Text != "Size")
                {
                    lvMonsters.RemoveSmallImageList();
                    chSpeed.Text = "Spd";
                    chAccuracy.Text = "Size";
                    miColumnsSpeed.Text = "Speed";
                    miColumnsAccuracy.Text = "Size";
                }
            }
            else if (chSpeed.Text != "Spd")
            {
                lvMonsters.SmallImageList = imageListMonsters;
                chSpeed.Text = "Spd";
                chAccuracy.Text = "Acc";
                miColumnsSpeed.Text = "Speed";
                miColumnsAccuracy.Text = "Accuracy";
            }

            if (info is Wiz4EncounterInfo)
            {
                chExperience.Width = 0;
                chTreasure.Width = 0;
                chTarget.Width = 0;
            }
            else if (info is BTEncounterInfo)
            {
                if (!(info is BT3EncounterInfo))
                {
                    chSpeed.Width = 0;
                    chResistances.Width = 0;
                }
                chAccuracy.Width = 0;
                chTarget.Width = 0;
                chTreasure.Width = 0;
            }
            else if (info is EOBEncounterInfo)
            {
                chTarget.Width = 0;
            }

            if (bClearList)
                Global.SizeHeadersAndContent(lvMonsters);

            m_infoPrevious = info;

            if (m_iLastSortColumn != -1)
            {
                lvMonsters.ListViewItemSorter = new EncounterItemComparer(lvMonsters, m_iLastSortColumn, m_bAscendingSort);
                lvMonsters.Sort();
            }

            lvMonsters.EndUpdate();

            lvMonsters.ScrollPosition = ptScroll;

            CheckSplitPositions(-1, iSplitDistance2);

            if (!Visible)
                Show();

            return true;
        }

        public bool DisarmTrap()
        {
            if (!ctlTreasure.Visible)
                return false;

            return ctlTreasure.DisarmTrap();
        }

        public void SelectMonsters(MonsterPosition pos, bool bAdd)
        {
            lvMonsters.BeginUpdate();

            if (!bAdd)
                lvMonsters.SelectedItems.Clear();

            HashSet<int> processed = new HashSet<int>();

            foreach (ListViewItem lvi in lvMonsters.Items)
            {
                foreach (Monster monster in pos.Monsters)
                {
                    if (!processed.Contains(lvi.Index) && ((EncounterItemTag)lvi.Tag).Monster.Position == pos.Position)
                    {
                        lvi.Selected = bAdd ? !lvi.Selected : true;
                        lvMonsters.EnsureVisible(lvi.Index);
                        processed.Add(lvi.Index);
                    }
                }
            }
            lvMonsters.EndUpdate();
        }

        public void SetSelectedMonsters(MonsterLocations monsters)
        {
            List<byte> bytes = new List<byte>();

            foreach(MonsterPosition position in monsters.MonsterPositions.Values)
            {
                position.Highlighted = false;
                foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                {
                    if (((EncounterItemTag)lvi.Tag).Monster.Position == position.Position)
                    {
                        position.Highlighted = true;
                        bytes.Add((byte) position.Position.X);
                        bytes.Add((byte) position.Position.Y);
                    }
                }
            }
            monsters.HighlightedBytes = bytes.ToArray();
        }

        public byte[] GetSelectedBytes()
        {
            List<Point> points = new List<Point>();

            foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                points.Add(((EncounterItemTag)lvi.Tag).Monster.Position);
            points.Sort(Global.PointComparer);

            byte[] bytes = new byte[points.Count * 2];
            for(int i = 0; i < points.Count; i++)
            {
                bytes[i * 2] = (byte)points[i].X;
                bytes[i * 2 + 1] = (byte)points[i].Y;
            }

            return bytes;
        }

        private void UpdateMonsterLVI(ListViewItem lvi, bool bRanged, bool bPreEncounter, Monster monster, string strPrefix, Proximity prox)
        {
            bool bHighProxDead = (Hacker.Game == GameNames.MightAndMagic3 || Hacker.Game == GameNames.MightAndMagic45);

            if (monster.Killed && !miMonsterShowOffMap.Checked)
            {
                lvi.Remove();
                return;
            }

            string strSuffix = monster.StatusSuffix;
            if (monster.Summoned)
                strSuffix = String.Format("{0}{1}{2}", strSuffix, String.IsNullOrWhiteSpace(strSuffix) ? "" : ", ", "summoned");
            if (strSuffix != "")
                strSuffix = " (" + strSuffix + ")";
            //if (Global.Debug)
            //    lvi.Text = String.Format("{0:D3}: {1}{2}{3}", monster.EncounterIndex, strPrefix, monster.ProperName, bPreEncounter ? "" : strSuffix);
            //else
                lvi.Text = String.Format("{0}{1}{2}", strPrefix, monster.ProperName, bPreEncounter ? "" : strSuffix);
            lvi.ImageIndex = bRanged ? (monster.Active ? 1 : -1) : 0;
            while (lvi.SubItems.Count < 13)
                lvi.SubItems.Add("");
            int iSubItem = 1;
            if (prox.Simple > 32 && bHighProxDead)
                lvi.SubItems[iSubItem++].Text = "dead";
            else
                lvi.SubItems[iSubItem++].Text = prox.UseSimple ? prox.Simple.ToString() : String.Format("{0:F1}", prox.Full);
            lvi.SubItems[iSubItem++].Text = (prox.Simple >= 32 && bHighProxDead) ? "off-map" : String.Format("{0},{1}", monster.Position.X, monster.Position.Y);
            lvi.SubItems[iSubItem++].Text = monster.HPString(bPreEncounter);
            lvi.SubItems[iSubItem++].Text = monster.AC.ToString();
            lvi.SubItems[iSubItem++].Text = monster.DamageString;
            lvi.SubItems[iSubItem++].Text = monster.Speed.ToString();
            lvi.SubItems[iSubItem++].Text = monster.AccuracyString;
            lvi.SubItems[iSubItem++].Text = monster.ResistancesStringShort;
            lvi.SubItems[iSubItem++].Text = monster.AllPowersString;
            lvi.SubItems[iSubItem++].Text = monster.Experience.ToString();
            lvi.SubItems[iSubItem++].Text = monster.TreasureStringShort;
            lvi.SubItems[iSubItem++].Text = monster.TargetString;
            lvi.Tag = new EncounterItemTag(monster, prox);
            lvi.ToolTipText = monster.MultiLineDescription;
            if (lvi.ToolTipText.Length > 1022)
                lvi.ToolTipText = lvi.ToolTipText.Substring(0, 1018) + "...";
        }

        private ListViewItem NewMonsterLVI(bool bRanged, bool bPreEncounter, Monster monster, string strPrefix, Proximity prox)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.AddRange(new string[13]);

            UpdateMonsterLVI(lvi, bRanged, bPreEncounter, monster, strPrefix, prox);
            return lvi;
        }

        public bool SetEncounterInfo(EncounterInfo info)
        {
            return UpdateUI(info);
        }

        protected override void OnMainSet()
        {
            m_main.OptionsChanged += OnMainOptionsChanged;
            OnMainSetAgain();
        }

        protected override void OnMainSetAgain()
        {
            Global.RestartTimer(timerRefreshInfo);
        }

        void OnMainOptionsChanged(object sender, EventArgs e)
        {
            timerRefreshInfo.Stop();
            timerRefreshInfo.Interval = Properties.Settings.Default.PollEncounters;
            timerRefreshInfo.Start();

            UpdateUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        public override void Destroy()
        {
            timerRefreshInfo.Stop();
            base.Destroy();
        }

        private void EncounterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.EncounterListColumns = new ColumnHeaderList(lvMonsters, m_iLastSortColumn, m_bAscendingSort);
            timerRefreshInfo.Stop();
            if (!m_main.ShuttingDown && Visible)
            {
                Properties.Settings.Default.ShowEncounters = false;
                m_main.UpdateEncounterMenu();
            }
        }

        private void timerRefreshInfo_Tick(object sender, EventArgs e)
        {
            if (Hacker == null || !Hacker.HasParty)
                Hide();
            else if (Hacker.Running)
            {
                if (m_bNeedNewColumnData)
                {
                    m_colWidths = new ColumnHeaderList(lvMonsters, m_iLastSortColumn, m_bAscendingSort);
                    Properties.Settings.Default.EncounterListColumns = m_colWidths;
                    m_bNeedNewColumnData = false;
                }

                EncounterInfo info = Hacker.GetEncounterInfo(m_infoPrevious == null);
                SetEncounterInfo(info);
                //m_infoPrevious = info;
            }
        }

        public void RefocusGame()
        {
            timerRefocus.Start();
        }

        private void timerRefocus_Tick(object sender, EventArgs e)
        {
            timerRefocus.Stop();
            Hacker.FocusDOSBox();
        }

        private void cmMonsters_Opening(object sender, CancelEventArgs e)
        {
            if (lvMonsters.FocusedItem != null)
                miMonsterMonitorHP.Checked = (lvMonsters.FocusedItem.Tag as EncounterItemTag).Monster.EncounterIndex == m_iMonitorIndex;

            if (!Global.Debug)
                miMonsterMonitorHP.Visible = false;

            if (Global.HeaderRect(lvMonsters).Contains(lvMonsters.PointToClient(Cursor.Position)))
            {
                e.Cancel = true;
                cmColumns.Show(Cursor.Position);
                return;
            }

            bool bCheat = Global.Cheats;

            if (Hacker.HasRoamingMonsters)
            {
                miMonsterRemoveAll.Available = bCheat;
                miMonsterShowOffMap.Available = true;
                miMonsterTeleportParty.Available = bCheat;
                miMonsterMoveMonster.Available = bCheat;
            }
            else
            {
                miMonsterRemoveAll.Available = false;
                miMonsterShowOffMap.Available = false;
                miMonsterTeleportParty.Available = false;
                miMonsterMoveMonster.Available = false;
            }

            miMonsterEdit.Available = bCheat;

            bool bAnyVisible = false;
            foreach (ToolStripMenuItem mi in cmMonsters.Items)
            {
                if (mi.Available)
                {
                    bAnyVisible = true;
                    break;
                }
            }

            if (!bAnyVisible)
                e.Cancel = true;

            miMonsterEdit.Enabled = lvMonsters.SelectedItems.Count > 0;
        }

        private void miMonsterEdit_Click(object sender, EventArgs e)
        {
            if (lvMonsters.FocusedItem == null)
                return;

            if (Hacker is MM3MemoryHacker || Hacker is MM45MemoryHacker)
            {
                MM345MonsterEditForm form = new MM345MonsterEditForm();
                form.SetMain(m_main, WindowType.MM345MonsterEdit);
                form.SetMonsterInfo(((EncounterItemTag)lvMonsters.FocusedItem.Tag).Monster as MM345Monster);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MM345Monster monster = form.GetMonsterInfo();
                    int iSaveIndex = monster.EncounterIndex;
                    Point ptSave = monster.Position;
                    bool bActive = monster.Active;
                    foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                    {
                        if (lvMonsters.SelectedItems.Count < 2)
                        {
                            // Only set the position if there is only one monster selected
                            monster.EncounterIndex = iSaveIndex;
                            monster.Position = ptSave;
                            Hacker.SetMonster(monster);
                            m_main.SetDirty(monster.Position);
                        }
                        else
                        {
                            Monster monsterSet = ((EncounterItemTag)lvi.Tag).Monster;

                            int iHoldIndex = monster.EncounterIndex;
                            monster.EncounterIndex = monsterSet.EncounterIndex;
                            // Don't set the position of multiple selected monsters unless it's to set them to "killed"
                            if (ptSave.X > 64)
                                monster.Position = ptSave;
                            else
                                monster.Position = monsterSet.Position;
                            monster.Active = bActive;
                            Hacker.SetMonster(monster);
                            m_main.SetDirty(monster.Position);
                            monster.EncounterIndex = iHoldIndex;  // To keep the "previous selected items" working
                        }
                    }
                    m_infoPrevious = null;
                }
            }
            else if (Games.IsWizardry(Hacker.Game) && m_infoPrevious is WizEncounterInfo)
            {
                Wiz123EncounterEditForm form = new Wiz123EncounterEditForm(Hacker.Game);
                form.Info = m_infoPrevious as WizEncounterInfo;
                List<WizMonster> monsters = new List<WizMonster>(lvMonsters.SelectedItems.Count);
                foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                    monsters.Add(((EncounterItemTag)lvi.Tag).Monster as WizMonster);
                form.SetMonsters(monsters);
                if (form.ShowDialog() == DialogResult.OK)
                    Hacker.SetEncounterInfo(form.Info);
                m_infoPrevious = null;  // Force reading of the new values
            }
            else if (Games.IsBardsTale(Hacker.Game) && m_infoPrevious is BTEncounterInfo)
            {
                if (Hacker.Game == GameNames.BardsTale1)
                {
                    BTEncounterEditForm form = new BTEncounterEditForm(Hacker.Game);
                    form.Info = m_infoPrevious as BTEncounterInfo;
                    List<BTMonster> monsters = new List<BTMonster>(lvMonsters.SelectedItems.Count);
                    foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                        monsters.Add(((EncounterItemTag)lvi.Tag).Monster as BTMonster);
                    form.SetMonsters(monsters);
                    if (form.ShowDialog() == DialogResult.OK)
                        Hacker.SetEncounterInfo(form.Info);
                }
                else
                {
                    BT2EncounterEditForm form = new BT2EncounterEditForm(Hacker.Game);
                    form.Info = m_infoPrevious as BT23EncounterInfo;
                    List<BTMonster> monsters = new List<BTMonster>(lvMonsters.SelectedItems.Count);
                    foreach (ListViewItem lvi in lvMonsters.SelectedItems)
                        monsters.Add(((EncounterItemTag)lvi.Tag).Monster as BTMonster);
                    form.SetMonsters(monsters);
                    if (form.ShowDialog() == DialogResult.OK)
                        Hacker.SetEncounterInfo(form.Info);
                }
                m_infoPrevious = null;  // Force reading of the new values
            }
            else if (Games.IsEyeOfTheBeholder(Hacker.Game) && m_infoPrevious is EOBEncounterInfo)
            {
                EOBMonsterEditForm form = new EOBMonsterEditForm();
                form.ItemTable = ((EOBMemoryHacker)Hacker).GetItemTable();
                form.Monster = (lvMonsters.SelectedItems[0].Tag as EncounterItemTag).Monster as EOBMonster;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Hacker.SetMonster(form.Monster);
                    m_infoPrevious = null;
                }
            }
            else if (Games.IsUltima(Hacker.Game) && m_infoPrevious is UltimaEncounterInfo)
            {
                UltimaMonsterEditForm form = new UltimaMonsterEditForm();
                UltimaMonster monster = (lvMonsters.SelectedItems[0].Tag as EncounterItemTag).Monster as UltimaMonster;
                Point ptPrev = monster.Position;
                form.Monster = monster;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    UltimaMonster newMonster = form.Monster;
                    newMonster.PreviousPosition = ptPrev;
                    Hacker.SetMonster(newMonster);
                    m_infoPrevious = null;
                }
            }
            else
            {
                MM2MonsterEditForm form = new MM2MonsterEditForm(m_main);
                Monster monster = ((EncounterItemTag)lvMonsters.FocusedItem.Tag).Monster;
                form.SetMonsterInfo(new MonsterBasicInfo(Hacker.Game, monster, m_infoPrevious));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MonsterBasicInfo info = form.GetMonsterInfo();
                    Hacker.SetMonsterInfo(monster.EncounterIndex, info);
                    m_infoPrevious = null;
                }
            }
        }

        class EncounterItemComparer : BasicListViewComparer
        {
            public EncounterItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

            public override int Compare(object x, object y)
            {
                if (!(x is ListViewItem) || !(y is ListViewItem))
                    return 0;

                ListViewItem lvi1 = x as ListViewItem;
                ListViewItem lvi2 = y as ListViewItem;

                EncounterItemTag item1 = (EncounterItemTag)lvi1.Tag;
                EncounterItemTag item2 = (EncounterItemTag)lvi2.Tag;

                switch (m_column)
                {
                    case 0:
                        if (!Global.Debug)
                        {
                            int returnVal = String.Compare(item1.Monster.ProperName, item2.Monster.ProperName);
                            if (returnVal == 0)
                                return Order(Math.Sign(item1.Monster.EncounterIndex - item2.Monster.EncounterIndex));
                            return Order(returnVal);
                        }
                        else
                            return Order(Math.Sign(item1.Monster.EncounterIndex - item2.Monster.EncounterIndex));
                    case 1: return Order(Math.Sign(item1.Prox.Full - item2.Prox.Full));
                    case 2: return Order(String.Compare(Global.PointString(item1.Monster.Position), Global.PointString(item2.Monster.Position)));
                    case 3: return Order(Math.Sign(item1.Monster.CurrentHP - item2.Monster.CurrentHP));
                    case 4: return Order(Math.Sign(item1.Monster.AC - item2.Monster.AC));
                    case 5: return Order(Math.Sign(item1.Monster.AverageDamage - item2.Monster.AverageDamage));
                    case 6: return Order(Math.Sign(item1.Monster.Speed - item2.Monster.Speed));
                    case 7: return Order(Math.Sign(item1.Monster.Accuracy - item2.Monster.Accuracy));
                    case 8: return Order(Math.Sign(item1.Monster.AverageResistance - item2.Monster.AverageResistance));
                    case 9: return Order(String.Compare(item1.Monster.AllPowersString, item2.Monster.AllPowersString));
                    case 10: return Order(Math.Sign(item1.Monster.Experience - item2.Monster.Experience));
                    case 11: return Order(Math.Sign(item1.Monster.TreasureStrength - item2.Monster.TreasureStrength));
                    case 12: return Order(String.Compare(item1.Monster.TargetString, item2.Monster.TargetString));
                    default: return base.Compare(x, y);
                }
            }
        }

        private void lvMonsters_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvMonsters.ListViewItemSorter = new EncounterItemComparer(lvMonsters, e.Column, m_bAscendingSort);
            lvMonsters.Sort();
        }

        private void miColumnsName_Click(object sender, EventArgs e)
        {
            miColumnsName.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chName.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsPosition_Click(object sender, EventArgs e)
        {
            miColumnsPosition.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chPosition.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsHP_Click(object sender, EventArgs e)
        {
            miColumnsHP.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chHitPoints.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsAC_Click(object sender, EventArgs e)
        {
            miColumnsAC.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chArmorClass.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsDamage_Click(object sender, EventArgs e)
        {
            miColumnsDamage.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chDamage.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsSpeed_Click(object sender, EventArgs e)
        {
            miColumnsSpeed.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chSpeed.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsAccuracy_Click(object sender, EventArgs e)
        {
            miColumnsAccuracy.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chAccuracy.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsResistances_Click(object sender, EventArgs e)
        {
            miColumnsResistances.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chResistances.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsSpecial_Click(object sender, EventArgs e)
        {
            miColumnsSpecial.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chSpecialPowers.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsExperience_Click(object sender, EventArgs e)
        {
            miColumnsExperience.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chExperience.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsTreasure_Click(object sender, EventArgs e)
        {
            miColumnsTreasure.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chTreasure.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsTarget_Click(object sender, EventArgs e)
        {
            miColumnsTarget.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chTarget.Index);
            m_bNeedNewColumnData = true;
        }

        private void miColumnsShowAll_Click(object sender, EventArgs e)
        {
            Global.ShowAllColumns(lvMonsters, m_colWidths, cmColumns);
            m_bNeedNewColumnData = true;
        }

        private void lvMonsters_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_main.InvalidateMonsters();
            m_main.SetDirty();
        }

        private void miMonsterShowDead_Click(object sender, EventArgs e)
        {
            miMonsterShowOffMap.Checked = !miMonsterShowOffMap.Checked;
            Properties.Settings.Default.ShowDeadMonsters = miMonsterShowOffMap.Checked;
            UpdateUI(m_infoPrevious, true);
        }

        private void miMonsterRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all of the monsters on this map?  This is different than killing the monsters normally and may have some unexpected effects.", "Remove All Monsters?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            Hacker.KillAllMonsters();
        }

        private void CheckSplitPositions(int iSplit1 = -1, int iSplit2 = -1)
        {
            if (iSplit2 > 0)
                Global.SetSplitterDistance(splitContainer2, iSplit2);

            if (!splitContainer1.Panel2Collapsed)
            {
                int iTextHeight = (int)Graphics.FromHwnd(tbExtraText.Handle).MeasureString(tbExtraText.Text, tbExtraText.Font, tbExtraText.Width).Height;
                int iMinSplit = splitContainer1.Height - iTextHeight - splitContainer2.Panel1.Height - splitContainer1.SplitterWidth - splitContainer2.SplitterWidth - 4;
                if (iSplit1 < 0)
                    iSplit1 = iMinSplit;
                else
                    iSplit1 = Math.Min(iSplit1, iMinSplit);

                Global.SetSplitterDistance(splitContainer1, iSplit1);
            }
        }

        private void EncounterForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            CheckSplitPositions();
        }

        private void miMonsterTeleportParty_Click(object sender, EventArgs e)
        {
            if (lvMonsters.SelectedItems.Count < 1)
                return;

            MMMonster monster = (lvMonsters.SelectedItems[0].Tag as EncounterItemTag).Monster as MMMonster;
            if (monster == null)
                return;

            Hacker.SetLocation(monster.Position);
        }

        private void miMonsterMoveMonster_Click(object sender, EventArgs e)
        {
            if (lvMonsters.FocusedItem == null)
                return;

            MMMonster monster = (lvMonsters.FocusedItem.Tag as EncounterItemTag).Monster as MMMonster;
            if (monster == null)
                return;

            monster.Position = Hacker.GetLocation().PrimaryCoordinates;
            Hacker.SetMonster(monster);
        }

        private void miColumnsProximity_Click(object sender, EventArgs e)
        {
            miColumnsProximity.Checked = Global.ToggleColumnVisible(lvMonsters, m_colWidths, chProximity.Index);
        }

        private void miMonsterMonitorHP_Click(object sender, EventArgs e)
        {
            if (lvMonsters.FocusedItem == null)
                return;

            Monster monster = (lvMonsters.FocusedItem.Tag as EncounterItemTag).Monster;
            int iIndex = monster.EncounterIndex;
            m_iMonitorLastHP = -1;

            if (m_iMonitorIndex == iIndex)
                m_iMonitorIndex = -1;
            else
                m_iMonitorIndex = iIndex;
        }

        private void lvMonsters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMonsterInfo();
        }

        private void lvMonsters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ShowMonsterInfo();
        }

        private void ShowMonsterInfo()
        {
            if (lvMonsters.SelectedItems.Count < 1)
                return;
            if (!(lvMonsters.SelectedItems[0].Tag is EncounterItemTag))
                return;
            Monster monster = ((EncounterItemTag) lvMonsters.SelectedItems[0].Tag).Monster; 
            ViewInfoForm.ShowCentered(splitContainer1, monster.MultiLineDescription, monster.ProperName);
        }

        private void lvMonsters_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            m_bNeedNewColumnData = true;
        }

        private void miColumnsRemoveSorting_Click(object sender, EventArgs e)
        {
            m_bAscendingSort = true;
            m_iLastSortColumn = -1;
            lvMonsters.ListViewItemSorter = null;
            lvMonsters.Sort();
        }
    }

    public class EncounterItemTag
    {
        public Monster Monster;
        public Proximity Prox;

        public EncounterItemTag(Monster monster, Proximity prox = null)
        {
            Monster = monster;
            Prox = prox;
        }
    }
}
