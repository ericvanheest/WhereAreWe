using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class ViewPartyForm : HackerBasedForm
    {
        public CharacterInfoControl[] m_charControls = new CharacterInfoControl[8];
        public TabPage[] m_pages = new TabPage[8];
        private byte[] m_bytes = null;
        private byte[] m_positions = null;
        private int[] m_addresses = null;
        private InventoryDelta m_inventoryDelta = new InventoryDelta(8);
        private byte m_numChars = 255;
        private byte[] m_lastEncounterInfo = null;
        private byte m_actingChar = 255;
        private String m_strTimeLast = null;
        private CharacterInfoControl m_charTradeSource = null;
        private GameInfo m_lastInfo = null;
        private GameTime m_lastGameTime = null;
        private NoScanReason m_noScan = NoScanReason.None;
        private bool m_bRevertTriggers = false;
        private Font m_defaultFont = null;
        private Color m_defaultForeColor;
        private Color m_defaultBackColor;

        public ViewPartyForm()
        {
            InitializeComponent();

            for(int i = 0; i < m_charControls.Length; i++)
                m_charControls[i] = null;

            m_pages[0] = tpChar1;
            m_pages[1] = tpChar2;
            m_pages[2] = tpChar3;
            m_pages[3] = tpChar4;
            m_pages[4] = tpChar5;
            m_pages[5] = tpChar6;
            m_pages[6] = tpChar7;
            m_pages[7] = tpChar8;

            tcCharacters.Dock = DockStyle.Fill;
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        public void UpdateSubscreen(Subscreen screen, bool bForce = false)
        {
            for (int i = 0; i < m_charControls.Length; i++)
                m_charControls[i].UpdateSubscreen(screen, bForce);
        }

        private void SetNoPartyDetected(bool bSet)
        {
            if (bSet)
            {
                bool bShow = false;
                if (m_main.InOptions && m_noScan != NoScanReason.OptionsOpen)
                {
                    m_noScan = NoScanReason.OptionsOpen;
                    labelNoParty.Text = Global.DisableWhileOptions("party window");
                    bShow = true;
                }
                else if (!m_main.InOptions && (Hacker == null || !Hacker.IsValid) && m_noScan != NoScanReason.NotRunning)
                {
                    m_noScan = NoScanReason.NotRunning;
                    labelNoParty.Text = Global.DisableWhileNoScanner();
                    bShow = true;
                }
                else if (Hacker != null && Hacker.IsValid && m_noScan != NoScanReason.NoParty)
                {
                    m_noScan = NoScanReason.NoParty;
                    labelNoParty.Text = String.Format("No party detected!  {0}", Hacker.PleaseFormPartyString);
                    bShow = true;
                }
                if (bShow)
                {
                    panelNoPartyDetected.Dock = DockStyle.Fill;
                    panelNoPartyDetected.Visible = true;
                    panelNoPartyDetected.BringToFront();
                }
            }
            else if (!bSet && m_noScan != NoScanReason.None)
            {
                panelNoPartyDetected.Dock = DockStyle.None;
                panelNoPartyDetected.Visible = false;
                panelNoPartyDetected.SendToBack();
                m_noScan = NoScanReason.None;
            }
        }

        public void UpdateTitle()
        {
            string strCheat = Global.Cheats ? " (cheats enabled)" : "";

            if (Hacker != null)
            {
                string strTime = Hacker.GetGameTimeString(true);
                if (!String.IsNullOrWhiteSpace(strTime) && strTime != m_strTimeLast)
                    Text = String.Format("Party Information - {0}{1}", strTime, strCheat);
                else
                    Text = "Party Information" + strCheat;
            }
            else
                Text = "Party Information (memory scanner inactive)";
        }

        public bool SetPartyInfo(PartyInfo info, GameInfo gameInfo, GameState gameState)
        {
            UpdateTitle();

            if (info == null)
                return false;

            if (info.Bytes == null)
                return false;

            bool bResetPages = false;

            if (m_charControls[0] == null || m_charControls[0].Character == null || gameInfo.Game != m_charControls[0].Character.Game)
            {
                // User probably closed one game and ran a different one
                bResetPages = true;

                for (int i = 0; i < m_charControls.Length; i++)
                    m_charControls[i] = Games.CreateCharacterInfoControl(gameInfo.Game, m_main);
            }

            if (bResetPages)
            {
                for (int i = 0; i < m_charControls.Length; i++)
                {
                    m_pages[i].Controls.Clear();
                    m_pages[i].Controls.Add(m_charControls[i]);
                    m_charControls[i].BeginUpdate();
                    m_charControls[i].Dock = DockStyle.Fill;
                    m_charControls[i].SetPartyWindow(this);
                    m_charControls[i].EndUpdate();
                }
                MoveQuickref(tcCharacters.SelectedIndex);
            }

            EncounterInfo encounterInfo = null;
            if (gameState.InCombat)
                encounterInfo = Hacker.GetEncounterInfo();

            bool bUpdated = false;

            if (!Global.Compare(info.Bytes, m_bytes) ||
                !Global.Compare(info.Positions, m_positions) ||
                !Global.Compare(info.Addresses, m_addresses) ||
                !Global.Compare(encounterInfo, m_lastEncounterInfo) ||
                m_numChars != info.NumChars ||
                Hacker.PartyInfoChanged(m_lastInfo, gameInfo))
            {
                m_bytes = info.Bytes;
                m_positions = info.Positions;
                m_addresses = info.Addresses;
                m_numChars = info.NumChars;
                m_lastInfo = gameInfo;
                m_lastGameTime = gameInfo.Time;
                m_lastEncounterInfo = (encounterInfo == null ? null : encounterInfo.AllBytes);

                NativeMethods.SuspendDrawing(this);

                for (int iTab = 0; iTab < info.Addresses.Length; iTab++)
                {
                    if (iTab < info.NumChars)
                    {
                        m_charControls[iTab].SetStatOrder(Hacker.StatOrder);
                        m_charControls[iTab].SetInfo(info, iTab, gameInfo, encounterInfo);
                        if (m_charControls[iTab].Character != null) 
                            SetCharacterName(iTab, m_charControls[iTab].Character.Name);
                        else
                            SetCharacterName(iTab, "INVALID");
                    }
                    else
                    {
                        m_charControls[iTab].SetInfo(null, iTab, gameInfo, encounterInfo);
                        SetCharacterName(iTab, String.Empty);
                    }

                    if (m_charControls[iTab] == null || m_charControls[iTab].Character == null || m_charControls[iTab].Character.BasicInventory == null)
                        continue;

                    m_inventoryDelta.Update(m_charControls[iTab].Character.Name, iTab, m_charControls[iTab].Character.BasicInventory.NumBackpackItems);
                }
                for (int iCtrl = info.Addresses.Length; iCtrl < m_charControls.Length; iCtrl++)
                    m_charControls[iCtrl].SetInfo(null, iCtrl, gameInfo, encounterInfo);   // So functions like "Cure-All" don't see a phantom character

                for (int i = info.Addresses.Length; i < m_charControls.Length; i++)
                    SetCharacterName(i, String.Empty);

                UpdateQuickRef();
                UpdateDelta();

                UpdateItemAges();

                ResetTested(Properties.Settings.Default.Triggers);
                for (int iTab = 0; iTab < info.NumChars; iTab++)
                {
                    SetLabelColors(m_charControls[iTab].Controls, Global.GetUIElement(ColoredUIElements.PartyFormItem));
                    m_charControls[iTab].CheckTriggers(Properties.Settings.Default.Triggers, m_bRevertTriggers);
                }

                bUpdated = true;
                NativeMethods.ResumeDrawing(this);
            }
            else if (gameInfo.Time != null && !gameInfo.Time.Equals(m_lastGameTime))
            {
                // Check the triggers without updating the form (for values like day/hour/minute, etc)

                Global.SetDefaultStyle(lvQuickRef, m_defaultFont, m_defaultForeColor, m_defaultBackColor);
                ResetTested(Properties.Settings.Default.Triggers);
                bool bNeedTabUpdate = false;
                for (int iTab = 0; iTab < info.NumChars; iTab++)
                {
                    if (iTab >= tcCharacters.TabPages.Count)
                        break;
                    CharTabTag tag = (CharTabTag)tcCharacters.TabPages[iTab].Tag;
                    if (tag != null)
                        tag = tag.Clone();
                    ClearTabStyle(tcCharacters.TabPages[iTab]);
                    SetLabelColors(m_charControls[iTab].Controls, Global.GetUIElement(ColoredUIElements.PartyFormItem));
                    m_charControls[iTab].CheckTriggers(Properties.Settings.Default.Triggers, m_bRevertTriggers);
                    CharTabTag newTag = (CharTabTag)tcCharacters.TabPages[iTab].Tag;
                    if (tag == null || newTag.Bold != tag.Bold || newTag.Italic != tag.Italic || newTag.Fore.ToArgb() != tag.Fore.ToArgb() || newTag.Back.ToArgb() != tag.Back.ToArgb())
                        bNeedTabUpdate = true;
                }
                if (bNeedTabUpdate)
                    tcCharacters.Refresh();
                m_lastGameTime = gameInfo.Time;
            }

            m_actingChar = info.ActingChar;

            if (Properties.Settings.Default.FollowCharWindows && Hacker.IsGameFocused && !gameState.NoActingChar)
            {
                SetCharacterByAddress(info.ActingChar);
            }

            m_bRevertTriggers = false;
            return bUpdated;
        }

        private void UpdateItemAges()
        {
            // Assume every item has been dropped; clear the flag during the loop if it is still being carried
            foreach (Item item in m_itemAgeList.Values)
                item.Dropped = true;

            List<Item> newItems = new List<Item>();
            int iTotalBackpackItemCount = 0;
            foreach (BaseCharacter bc in GetCharacters())
            {
                List<Item> backpack = bc.BackpackItems;
                iTotalBackpackItemCount += backpack.Count;
                foreach (Item item in backpack)
                {
                    string desc = item.FullDescriptionString;
                    if (!m_itemAgeList.ContainsKey(desc))
                        newItems.Add(item);
                    else
                    {
                        Item oldItem = m_itemAgeList[desc];
                        item.Age = oldItem.Age;
                        oldItem.Dropped = false;
                    }
                }
            }

            // Special case - if there is more than one new item and it's "every single item in the
            // inventory" then assume this is the start of the game or similar and mark all the ages as
            // "very old" so they don't get selected by triggers.
            if (newItems.Count > 1 && newItems.Count == iTotalBackpackItemCount)
            {
                m_itemAgeList.Clear();
                foreach (Item item in newItems)
                {
                    item.Age = 1000;
                    string desc = item.FullDescriptionString;
                    if (!m_itemAgeList.ContainsKey(desc))
                        m_itemAgeList.Add(desc, item);
                }
                return;
            }

            // Make everything currently in the list "older" (unless it's an invalid Age, -1)
            foreach (Item item in m_itemAgeList.Values)
                if (item.Age > -1)
                    item.Age += newItems.Count;
            for (int i = 0; i < newItems.Count; i++)
            {
                string desc = newItems[i].FullDescriptionString;
                // If we have literal duplicate items then one of them won't end up in the age list
                // but since the age list is based on the string as a key, it will still be found when
                // searched later (though if someone picks up a duplicate item it won't count as "new")
                newItems[i].Age = i;
                if (!m_itemAgeList.ContainsKey(desc))
                    m_itemAgeList.Add(desc, newItems[i]);
            }

            // Remove dropped/sold/used/etc items from the list to avoid it becoming a pointless history of all items ever touched
            m_itemAgeList.RemoveAll(i => i.Dropped);
        }

        private void ResetTested(TriggerList list)
        {
            if (list == null)
                return;
            foreach (CharacterTrigger ct in list.Items)
                ct.Tested = false;
        }

        public void SetLabelColors(Control.ControlCollection top, UIElementOption colors)
        {
            foreach (Control ctrl in top)
            {
                if (ctrl is EditableAttributeLabel)
                {
                    ctrl.ForeColor = colors.ForeColor;
                    ctrl.BackColor = colors.BackColor;
                }
                else if (ctrl.Controls != null)
                    SetLabelColors(ctrl.Controls, colors);
            }
        }

        public void RevertTriggeredItems()
        {
            m_bRevertTriggers = true;
        }

        public void UpdateDelta()
        {
            if (!Properties.Settings.Default.IndicateNewInventoryItems)
                return;
            for (int i = 0; i < m_inventoryDelta.Count; i++)
            {
                if (i >= tcCharacters.TabPages.Count)
                    continue;
                if (m_inventoryDelta.Increased[i] && !tcCharacters.TabPages[i].Text.EndsWith(" ∆"))
                    tcCharacters.TabPages[i].Text += " ∆";
            }
        }

        public BaseCharacter GetCharacterByAddress(int iAddress)
        {
            foreach(CharacterInfoControl ctrl in m_charControls)
            {
                if (ctrl != null && ctrl.CharacterAddress == iAddress)
                    return ctrl.Character;
            }
            return null;
        }

        public BaseCharacter GetCharacterByPosition(int iPosition)
        {
            if (iPosition >= m_charControls.Length)
                return null;
            return m_charControls[iPosition].Character;
        }

        public void UpdateUI()
        {
            foreach (CharacterInfoControl ctrl in m_charControls)
                if (ctrl != null)
                    ctrl.UpdateUI();
        }

        public bool SetCharacter(int iCharPosition)
        {
            if (iCharPosition < 0 || iCharPosition > tcCharacters.TabCount - 1)
                return false;

            if (tcCharacters.SelectedIndex == iCharPosition)
                return true;

            if (m_pages[iCharPosition].Text != "")
            {
                // I don't remember why we were doing this suspend/resume here but it causes programs in the background to bleed through
                //NativeMethods.SuspendDrawing(this);
                //tcCharacters.Enabled = false;
                tcCharacters.SelectedIndex = iCharPosition;
                //tcCharacters.Enabled = true;
                //NativeMethods.ResumeDrawing(this);
                return true;
            }

            return false;
        }

        public bool SelectNextCharacter(int iCount)
        {
            int iCharPosition = tcCharacters.SelectedIndex + iCount;
            if (iCharPosition < 0)
                iCharPosition = tcCharacters.TabPages.Count + iCount;
            if (iCharPosition >= tcCharacters.TabPages.Count)
                iCharPosition -= tcCharacters.TabPages.Count;
            if (String.IsNullOrWhiteSpace(tcCharacters.TabPages[iCharPosition].Name))
                iCharPosition = 0;

            return SetCharacter(iCharPosition);
        }

        public bool SetCharacterByIndex(int iCharIndex)
        {
            for (int i = 0; i < m_charControls.Length; i++)
                if (m_charControls[i].CharacterIndex == iCharIndex)
                    return SetCharacter(i);
            return false;
        }

        public bool SetCharacterByAddress(int iCharAddress)
        {
            for (int i = 0; i < m_charControls.Length; i++)
                if (iCharAddress < m_addresses.Length && m_charControls[i].CharacterAddress == m_addresses[iCharAddress])
                    return SetCharacter(i);
            return false;
        }

        public int SelectedCharacterIndex
        {
            get { return tcCharacters.SelectedIndex; }
            set { SetCharacterByIndex(value); }
        }

        public int GetSelectedCharacterAddress()
        {
            if (tcCharacters.SelectedTab == null)
                return -1;

            if (tcCharacters.SelectedIndex >= m_charControls.Length)
                return -1;

            if (m_charControls[tcCharacters.SelectedIndex] == null)
                return -1;

            return m_charControls[tcCharacters.SelectedIndex].CharacterAddress;
        }

        public int GetSelectedCharacterPosition()
        {
            return tcCharacters.SelectedIndex;
        }

        public void NextCharacter() { SelectNextCharacter(1); }
        public void PrevCharacter() { SelectNextCharacter(-1); }

        protected void SetCharacterName(int iPosition, string strName)
        {
            Global.UpdateText(m_pages[iPosition], strName);
            if (String.IsNullOrEmpty(strName))
            {
                if (tcCharacters.TabPages.Contains(m_pages[iPosition]))
                    tcCharacters.TabPages.Remove(m_pages[iPosition]);
                if (tcCharacters.TabPages.Count == 0)
                    tcCharacters.TabPages.Add(m_pages[0]);
            }
            else
            {
                for(int iTab = 0; iTab <= iPosition; iTab++)
                if (!tcCharacters.TabPages.Contains(m_pages[iTab]))
                    tcCharacters.TabPages.Insert(iTab, m_pages[iTab]);
                SetNoPartyDetected(false);
            }
        }

        public TriggerSubItem[] GetQuickRefItems(TriggerEntity entity, int iChar)
        {
            return new TriggerSubItem[] { GetQuickRefItem(entity, iChar) };
        }

        public TriggerSubItem GetQuickRefItem(TriggerEntity entity, int iChar)
        {
            if (iChar < 0 || iChar >= lvQuickRef.Items.Count)
                return null;

            switch (entity)
            {
                case TriggerEntity.CharIndex: return new TriggerSubItem(lvQuickRef.Items[iChar], 0);
                case TriggerEntity.Name: return new TriggerSubItem(lvQuickRef.Items[iChar], 1);
                case TriggerEntity.Condition: return new TriggerSubItem(lvQuickRef.Items[iChar], 6);
                case TriggerEntity.Gems: return new TriggerSubItem(lvQuickRef.Items[iChar], 5);
                case TriggerEntity.SpellPoints:
                case TriggerEntity.MaxSpellPoints: return new TriggerSubItem(lvQuickRef.Items[iChar], 4);
                case TriggerEntity.HitPoints:
                case TriggerEntity.MaxHitPoints: return new TriggerSubItem(lvQuickRef.Items[iChar], 3);
                case TriggerEntity.ExperienceToNext: return new TriggerSubItem(lvQuickRef.Items[iChar], 2);
                default: return null;
            }
        }

        public void SetTabStyle(TabPage page, CharacterTrigger trigger)
        {
            page.Tag = new CharTabTag(trigger, page.ForeColor, page.BackColor);
        }

        public void ClearTabStyle(TabPage page)
        {
            CharTabTag tag = page.Tag as CharTabTag;
            if (tag == null)
                return;
            tag.Revert();
        }

        public void UpdateQuickRef()
        {
            lvQuickRef.BeginUpdate();
            lvQuickRef.Items.Clear();

            for (int i = 0; i < m_numChars; i++)
            {
                BaseCharacter character = GetCharacterByPosition(i);
                if (character == null || character.Name == null)
                    continue;

                if (lvQuickRef.Columns.Contains(chGems) && character.PartyGems || !Hacker.UsesGems)
                    lvQuickRef.Columns.Remove(chGems);
            
                ListViewItem lvi = new ListViewItem((i+1).ToString());
                lvi.SubItems.Add(character.Name);
                if (character is Wiz4Character)
                    lvi.SubItems.Add(String.Empty);    // No training in Wiz4
                else
                    lvi.SubItems.Add(character.BasicLevel.Permanent >= character.MaxLevel ? "Max Level" : 
                        (character.ReadyToTrain ? Global.TrainString(character.BasicLevel.Permanent, character.TrainableLevel) : character.NeedsXP.ToString()));
                lvi.SubItems.Add(character.QuickRefHitPoints.ToString());

                if (character.QuickRefSpellPoints != null)
                {
                    if (character.HasSpellLevel && character.UsesSpellLevel && character.QuickRefSpellLevel.Temporary > 0)
                        lvi.SubItems.Add(String.Format("{0} ({1})", character.QuickRefSpellPoints.ToString(), character.QuickRefSpellLevel.Temporary));
                    else if (character.IsCaster)
                        lvi.SubItems.Add(character.QuickRefSpellPoints.ToString());
                    else if (character.QuickRefSpellPoints.HasAnyCurrent)
                        lvi.SubItems.Add(character.QuickRefSpellPoints.Current.ToString());
                    else
                        lvi.SubItems.Add("");
                }
                else
                    lvi.SubItems.Add("");

                if (!character.PartyGems)
                    lvi.SubItems.Add(character.QuickRefGems.ToString());

                lvi.SubItems.Add(character.QuickRefCondition);
                lvi.Tag = new QuickChar(i, character);
                lvQuickRef.Items.Add(lvi);
            }
            Global.SizeHeadersAndContent(lvQuickRef);
            int iHeight = 0;
            foreach(ListViewItem lvi in lvQuickRef.Items)
                iHeight += lvi.GetBounds(ItemBoundsPortion.Entire).Height;
            lvQuickRef.Height = iHeight + NativeMethods.GetListViewHeaderRect(lvQuickRef).Height + SystemInformation.HorizontalScrollBarHeight + 4;

            lvQuickRef.EndUpdate();

            if (lvQuickRef.Items.Count > 0)
            {
                m_defaultFont = lvQuickRef.Items[0].Font;
                m_defaultForeColor = lvQuickRef.Items[0].ForeColor;
                m_defaultBackColor = lvQuickRef.Items[0].BackColor;
            }

        }

        public bool CureAll(bool bSilent)
        {
            // Cast "Cure All" with currently selected character

            if (m_actingChar < m_positions.Length)
                if (m_positions[m_actingChar] != 255)
                    return m_main.CureAll(m_positions[m_actingChar], bSilent);
            return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        public override void Destroy()
        {
            timerRefreshPartyInfo.Stop();
            base.Destroy();
        }

        private void ViewPartyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefreshPartyInfo.Stop();
        }

        protected override void OnMainSet()
        {
            m_main.OptionsChanged += OnMainOptionsChanged;
            OnMainSetAgain();
        }

        protected override void OnMainSetAgain()
        {
            timerRefreshPartyInfo.Interval = Properties.Settings.Default.PollTime;
            Global.RestartTimer(timerRefreshPartyInfo);
        }

        void OnMainOptionsChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateTitle();
                CheckInventoryWindows();
            }
        }

        private void timerRefreshPartyInfo_Tick(object sender, EventArgs e)
        {
            if (Hacker != null && Hacker.Running && Hacker.IsValid)
            {
                GameState state = Hacker.GetGameState();
                GameInfo gameInfo = Hacker.GetGameInfo(m_lastInfo);
                PartyInfo partyInfo = Hacker.GetPartyInfo();
                if (partyInfo == null || partyInfo.NumChars == 0 || state.Main == MainState.Opening || state.Main == MainState.Opening2)
                {
                    SetNoPartyDetected(true);
                    return;
                }
                SetNoPartyDetected(false);
                bool bUpdated = SetPartyInfo(partyInfo, gameInfo, state);
                m_lastInfo = gameInfo;
                UpdateSubscreen(state.Subscreen, bUpdated);
            }
            else
                SetNoPartyDetected(true);

        }

        private void ViewPartyForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                    SetCharacter(e.KeyCode - Keys.D1);
                    break;
                case Keys.F5:
                    ForceUpdate();
                    break;
                case Keys.PageDown:
                    NextCharacter();
                    break;
                case Keys.PageUp:
                    PrevCharacter();
                    break;
                default:
                    break;
            }
        }

        public void ForceUpdate() { m_bytes = null; }

        private void miQuickCureAll_Click(object sender, EventArgs e)
        {
            if (lvQuickRef.FocusedItem == null)
                return;
            QuickChar qc = (QuickChar)lvQuickRef.FocusedItem.Tag;
            if (m_positions[qc.Index] != 255)
                m_main.CureAll(m_positions[qc.Index], false);
        }

        private void cmQuickRef_Opening(object sender, CancelEventArgs e)
        {
            bool bShowCureAll = false;
            miQuickAdvanced.Checked = Global.FormVisible(m_main.QuickRefForm);

            if (Properties.Settings.Default.EnableMemoryWrite &&
                lvQuickRef.FocusedItem != null)
            {
                QuickChar qc = (QuickChar)lvQuickRef.FocusedItem.Tag;
                if (qc.Character.IsHealer)
                    bShowCureAll = true;
            }

            miQuickCureAll.Available = bShowCureAll;
        }

        public void FillBackpackTradeMenu(ToolStripMenuItem menu, CharacterInfoControl charExcept)
        {
            m_charTradeSource = charExcept;

            menu.DropDownItems.Clear();
            foreach (CharacterInfoControl charInfo in m_charControls)
            {
                if (charInfo == charExcept)
                    continue;
                if (charInfo == null)
                    continue;
                if (String.IsNullOrWhiteSpace(charInfo.Character.Name))
                    continue;

                ToolStripMenuItem item = new ToolStripMenuItem(String.Format("&{0}: {1}", charInfo.CharacterIndex + 1, charInfo.Character.Name));
                item.Click += new EventHandler(menuBackpackTrade_clicked);
                item.Tag = charInfo;
                menu.DropDownItems.Add(item);
            }
            if (menu.DropDownItems.Count < 1)
            {
                menu.DropDownItems.Add("(none)");
                menu.DropDownItems[0].Enabled = false;
            }
        }

        public void menuBackpackTrade_clicked(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;

            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CharacterInfoControl charInfo = item.Tag as CharacterInfoControl;

            if (!Hacker.TradeBackpacks(m_charTradeSource.CharacterAddress, charInfo.CharacterAddress))
            {
                MessageBox.Show("There is not enough space to trade backpacks (equipped items count toward the backpack size)",
                    "Not enough backpack space", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool TradeBackpacks(int iCharPosition)
        {
            if (Hacker == null)
                return false;
            int iSource = m_actingChar;
            if (iSource == 255)
                iSource = SelectedCharacterIndex;
            if (iCharPosition < m_charControls.Length && m_charControls[iCharPosition] != null)
                return Hacker.TradeBackpacks(iSource, m_charControls[iCharPosition].CharacterAddress);
            return false;
        }

        public List<BaseCharacter> GetCharacters()
        {
            if (Hacker == null)
                return null;

            if (!Hacker.HasParty)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(m_numChars);
            foreach (CharacterInfoControl ctrl in m_charControls)
            {
                if (ctrl == null)
                    continue;
                if (chars.Count >= m_numChars)
                    break;
                if (ctrl.Character == null)
                    continue;
                if (String.IsNullOrWhiteSpace(ctrl.Character.Name))
                    continue;
                chars.Add(ctrl.Character);
            }
            return chars;
        }

        public void CheckInventoryWindows()
        {
            foreach (CharacterInfoControl ctrl in m_charControls)
            {
                if (ctrl != null)
                    ctrl.SetInventorySize();
            }
        }

        private void miQuickAdvanced_Click(object sender, EventArgs e)
        {
            miQuickAdvanced.Checked = !Global.FormVisible(m_main.QuickRefForm);
            m_main.ShowQuickRefNext();
        }

        private void miCtxShowIndicatorForItems_Click(object sender, EventArgs e)
        {
            miCtxShowIndicatorForItems.Checked = !miCtxShowIndicatorForItems.Checked;
            Properties.Settings.Default.IndicateNewInventoryItems = miCtxShowIndicatorForItems.Checked;
            m_inventoryDelta.ClearUpdates();
            ClearDeltas();
        }

        private void ClearDeltas()
        {
            m_inventoryDelta.ClearUpdates();
            for (int iTab = 0; iTab < tcCharacters.TabPages.Count; iTab++)
                if (m_charControls[iTab] != null && m_charControls[iTab].Character != null && !String.IsNullOrWhiteSpace(m_charControls[iTab].Character.Name))
                    SetCharacterName(iTab, m_charControls[iTab].Character.Name);
        }

        private void tcCharacters_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cmTab.Show(Cursor.Position);
                return;
            }
        }

        private void cmTab_Opening(object sender, CancelEventArgs e)
        {
            miCtxShowIndicatorForItems.Checked = Properties.Settings.Default.IndicateNewInventoryItems;
        }

        private void tcCharacters_DoubleClick(object sender, EventArgs e)
        {
            ClearDeltas();
        }

        private void tcCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            NativeMethods.ResumeDrawing(this);
        }

        private void MoveQuickref(int index)
        {
            if (index < 0 || index >= m_charControls.Length)
                return;
            m_charControls[index].QuickRefPanel.Controls.Add(lvQuickRef);
            m_charControls[index].QuickRefSplitPosition = Properties.Settings.Default.PartyQuickrefSplitPosition;
            lvQuickRef.Dock = DockStyle.Fill;
        }

        private void MoveResistances(int index)
        {
            if (index < 0 || index >= m_charControls.Length)
                return;
            m_charControls[index].ResistancesSplitPosition = Properties.Settings.Default.PartyResistSplitPosition;
        }

        private void tcCharacters_Selecting(object sender, TabControlCancelEventArgs e)
        {
            NativeMethods.SuspendDrawing(this);
            MoveQuickref(e.TabPageIndex);
            MoveResistances(e.TabPageIndex);
        }

        private void tcCharacters_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= tcCharacters.TabPages.Count)
                return;

            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(SystemColors.ControlText);
            Pen penSelect = new Pen(SystemBrushes.ButtonHighlight, 1.0f);

            TabPage page = tcCharacters.TabPages[e.Index];
            Rectangle rcBounds = tcCharacters.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                g.FillRectangle(SystemBrushes.Control, e.Bounds);
            }

            Font font = e.Font;
            CharTabTag tag = page.Tag as CharTabTag;
            if (tag != null)
            {
                if (tag.Bold)
                    font = new Font(font, FontStyle.Bold);
                if (tag.Italic)
                    font = new Font(font, FontStyle.Italic);
                if (tag.NewBackColor)
                    g.FillRectangle(new SolidBrush(tag.Back), e.Bounds);
                if (tag.NewForeColor)
                    brush = new SolidBrush(tag.Fore);
            }
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(tcCharacters.TabPages[e.Index].Text, font, brush, rcBounds, format);
        }

        private Dictionary<string, Item> m_itemAgeList = new Dictionary<string, Item>();

        public void OnCharacterInfoSet(BaseCharacter bc, int iIndex)
        {

        }
    }

    public class InventoryDelta
    {
        public string[] Names;
        public int[] Current;
        public int[] Previous;
        public bool[] Increased;
        public bool[] Decreased;

        public InventoryDelta(int iMax)
        {
            Names = new string[iMax];
            Current = new int[iMax];
            Previous = new int[iMax];
            Increased = new bool[iMax];
            Decreased = new bool[iMax];

            for (int i = 0; i < iMax; i++)
            {
                Names[i] = String.Empty;
                Current[i] = -1;
                Previous[i] = -1;
                Increased[i] = false;
                Decreased[i] = false;
            }
        }

        public void Set(int index)
        {
            if (index < 0 || index >= Count)
                return;
            Increased[index] = true;
        }

        public void Reset(int index)
        {
            if (index < 0 || index >= Count)
                return;
            Increased[index] = false;
        }

        public void Update(string strName, int index, int iCount)
        {
            if (index < 0 || index >= Count)
                return;

            Current[index] = iCount;
            if (Previous[index] == -1 || strName != Names[index])
                Previous[index] = iCount;

            if (Names[index] != strName)
            {
                Increased[index] = false;
                Decreased[index] = false;
            }

            Names[index] = strName;

            if (Current[index] > Previous[index])
            {
                Increased[index] = true;
                Decreased[index] = false;
            }
            else if (Current[index] < Previous[index])
            {
                Increased[index] = false;
                Decreased[index] = true;
            }

            Previous[index] = iCount;
        }

        public void ClearUpdates()
        {
            for (int i = 0; i < Increased.Length; i++)
            {
                Increased[i] = false;
                Decreased[i] = false;
            }
        }

        public int Count { get { return Increased.Length; } }
    }

    public class QuickChar
    {
        public int Index = -1;
        public BaseCharacter Character = null;

        public QuickChar(int index, BaseCharacter character)
        {
            Index = index;
            Character = character;
        }
    }

    public class CharTabTag
    {
        public bool Bold;
        public bool Italic;
        public bool NewForeColor;
        public bool NewBackColor;
        public Color Fore;
        public Color Back;

        public Color OriginalFore;
        public Color OriginalBack;

        public CharTabTag()
        {
        }

        public CharTabTag(CharacterTrigger trigger, Color origFore, Color origBack)
        {
            OriginalFore = origFore;
            OriginalBack = origBack;

            Bold = trigger.Do == TriggerDo.SetBoldFont;
            Italic = trigger.Do == TriggerDo.SetItalicFont;
            if (trigger.Do == TriggerDo.SetColorTo)
            {
                Fore = trigger.DoColorFore;
                Back = trigger.DoColorBack;
                NewForeColor = true;
                NewBackColor = true;
            }
            else
            {
                NewForeColor = false;
                NewBackColor = false;
            }
        }

        public void Revert()
        {
            Bold = false;
            Italic = false;
            Fore = OriginalFore;
            Back = OriginalBack;
        }

        public CharTabTag Clone()
        {
            CharTabTag tag = new CharTabTag();
            tag.Bold = Bold;
            tag.Italic = Italic;
            tag.Fore = Fore;
            tag.Back = Back;
            tag.NewBackColor = NewBackColor;
            tag.NewForeColor = NewForeColor;
            tag.OriginalBack = OriginalBack;
            tag.OriginalFore = OriginalFore;
            return tag;
        }
    }
}
