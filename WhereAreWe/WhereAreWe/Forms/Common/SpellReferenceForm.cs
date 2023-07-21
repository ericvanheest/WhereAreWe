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
    public partial class SpellReferenceForm : HackerBasedForm
    {
        private SpellType m_lastType = SpellType.Unknown;
        private byte m_lastLevel = 0;
        private int m_lastChar = 0;
        private int m_lastInOut = 0;
        private bool m_bAutoClose = false;
        private MainState m_lastMainState = MainState.Unknown;
        private int m_lastNumKnownSpells = 0;
        private FindBox m_findBox = null;
        private SpellRefPanel[] m_spellLists = null;
        private BaseCharacter m_character = null;
        private FavoriteSpells m_favorites = null;
        private int m_iLastFavSortColumn = -1;
        private bool m_bAscendingFavSort = true;
        private SpellInfo m_lastSpellinfo;
        private int m_iLastSelectedTab = -1;
        private bool m_bUpdatingTabs = false;
        private static bool m_bFilterUncastable = true;
        public bool m_bShowingFullList = false;

        private bool ManualClose 
        {
            get
            {
                if (m_main != null)
                    return m_main.ManualSpellWindowClose;
                return false;
            }
            set
            {
                if (m_main != null)
                    m_main.ManualSpellWindowClose = value;
            }
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        public SpellReferenceForm()
        {
            InitializeComponent();

            m_spellLists = new SpellRefPanel[] { spellListFavorites, spellList1, spellList2, spellList3, spellList4, spellList5, spellList6, spellList7, spellList8, spellList9 };
            foreach (SpellRefPanel srp in m_spellLists)
            {
                srp.ParentTabControl = tcSpells;
                srp.SpellListView.ContextMenuStrip = cmSpell;
            }

            spellListFavorites.ParentTab = tpFavorites;
            spellList1.ParentTab = tabPage1;
            spellList2.ParentTab = tabPage2;
            spellList3.ParentTab = tabPage3;
            spellList4.ParentTab = tabPage4;
            spellList5.ParentTab = tabPage5;
            spellList6.ParentTab = tabPage6;
            spellList7.ParentTab = tabPage7;
            spellList8.ParentTab = tabPage8;
            spellList9.ParentTab = tabPage9;

            foreach (SpellRefPanel srp in m_spellLists)
                srp.SpellActivated += spellList_SpellActivated;

            ManualClose = false;
            m_favorites = Properties.Settings.Default.FavoriteSpells;
            if (m_favorites == null)
                m_favorites = new FavoriteSpells();

            spellListFavorites.ShowSpellType = true;
            spellListFavorites.SpellListView.ItemsRearranged += SpellListView_ItemsRearranged;
        }

        protected override bool OnCommonKeyRefresh()
        {
            UpdateUI();
            return true;
        }

        private void SpellListView_ItemsRearranged(object sender, EventArgs e)
        {
            List<int> list = new List<int>(spellListFavorites.SpellListView.Items.Count);
            foreach (ListViewItem lvi in spellListFavorites.SpellListView.Items)
                list.Add((lvi.Tag as SpellTag).Spell.BasicIndex);
            m_favorites.SetFavorites(Hacker.Game, m_character?.Name, list);
        }

        private void SpellListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastFavSortColumn)
                m_bAscendingFavSort = !m_bAscendingFavSort;
            else
                m_bAscendingFavSort = true;

            m_iLastFavSortColumn = e.Column;

            spellListFavorites.SpellListView.ListViewItemSorter = new SpellReferenceComparer(e.Column, m_bAscendingFavSort);
            spellListFavorites.SpellListView.Sort();
        }

        void spellList_SpellActivated(object sender, SpellActivatedEventArgs e)
        {
            ActivateSpell(e.SelectedSpell);
        }

        private void SpellReferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefreshSpellInfo.Stop();

            if (m_bDestroy)
                return;

            ManualClose = true;
            m_bAutoClose = true;
        }

        public bool OpenedManually
        {
            get
            {
                return !m_bAutoClose;
            }
        }

        public bool SetSpellInfo(SpellInfo spellInfo, bool bFavoritesOnly = false)
        {
            if (m_spellLists == null)
                return false;

            if (IsDisposed || m_spellLists.Any(sl => sl.IsDisposed))
                return false;

            if (spellInfo == null)
            {
                m_bAutoClose = false;
                m_lastInOut = 0;
                m_lastChar = 0;
                m_lastLevel = 7;
                m_lastMainState = MainState.Unknown;
                m_lastType = SpellType.Unknown;

                if (!m_bShowingFullList)
                    ShowSpells(null, null, bFavoritesOnly);
                m_bShowingFullList = true;

                return true;
            }

            m_bShowingFullList = false;

            BaseCharacter character = null;
            int iActingChar = -1;

            if (spellInfo is MM1SpellInfo)
            {
                MM1SpellInfo info = spellInfo as MM1SpellInfo;

                if (info.Party != null && info.Party.Bytes.Length >= info.Party.ActingChar * info.Party.CharacterSize)
                {
                    character = new MM1Character();
                    character.SetFromBytes(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize);
                    iActingChar = info.Party.ActingChar;
                }
            }
            else if (spellInfo is MM2SpellInfo)
            {
                MM2SpellInfo info = spellInfo as MM2SpellInfo;

                if (info.Party != null && info.Party.Bytes.Length >= info.Party.ActingChar * info.Party.CharacterSize)
                {
                    character = MM2Character.Create(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize);
                    iActingChar = info.Party.ActingChar;
                }
            }
            else if (spellInfo is MM3SpellInfo)
            {
                MM3SpellInfo info = spellInfo as MM3SpellInfo;

                iActingChar = info.Game.ActingCaster;

                if (info.Party != null && info.Party.Bytes.Length >= iActingChar * info.Party.CharacterSize)
                    character = MM3Character.Create(info.Party.Bytes, iActingChar * info.Party.CharacterSize, null);
            }

            else if (spellInfo is MM45SpellInfo)
            {
                MM45SpellInfo info = spellInfo as MM45SpellInfo;

                iActingChar = info.Game.ActingCaster;

                if (info.Party != null && info.Party.Bytes.Length >= iActingChar * info.Party.CharacterSize)
                    character = MM45Character.Create(info.Party.Bytes, iActingChar * info.Party.CharacterSize, null);
            }
            else if (spellInfo is Wiz5SpellInfo)
            {
                Wiz5SpellInfo info = spellInfo as Wiz5SpellInfo;

                iActingChar = info.Game.ActingCaster;

                if (info.Party != null && info.Party.Bytes.Length >= iActingChar * info.Party.CharacterSize)
                    character = Wiz5Character.Create(0, info.Party.Bytes, iActingChar * info.Party.CharacterSize, null, null);
            }
            else if (spellInfo is Wiz1234SpellInfo)
            {
                Wiz1234SpellInfo info = spellInfo as Wiz1234SpellInfo;

                iActingChar = info.Game.ActingCaster;

                if (info.Party != null && info.Party.Bytes.Length >= iActingChar * info.Party.CharacterSize)
                    character = WizCharacter.Create(info.Game.Game, 0, info.Party.Bytes, iActingChar * info.Party.CharacterSize, null);
            }
            else if (spellInfo is BTSpellInfo)
            {
                BTSpellInfo info = spellInfo as BTSpellInfo;

                iActingChar = info.Game.ActingCaster;

                if (info.Party != null && info.Party.Bytes.Length >= iActingChar * info.Party.CharacterSize)
                    character = BTCharacter.Create(info.Game.Game, 0, info.Party.Bytes, iActingChar * info.Party.CharacterSize);
            }

            if (character == null || character.Name == null)
            {
                if (m_bAutoClose)
                    Close();
                return false;
            }

            SpellType type = Global.GetSpellType(character.BasicClass);
            if (character.QuickRefSpellLevel == null)
            {
                Close();
                return false;
            }

            if (type == m_lastType &&
                character.QuickRefSpellLevel.Temporary == m_lastLevel &&
                iActingChar == m_lastChar &&
                spellInfo.Game.InCombat == (m_lastMainState == MainState.Combat) &&
                spellInfo.Game.Location.Outside == (m_lastInOut == 2) &&
                character.NumKnownSpells == m_lastNumKnownSpells &&
                !bFavoritesOnly)
                return false;

            m_lastType = type;
            m_lastLevel = (byte) character.QuickRefSpellLevel.Temporary;
            m_lastChar = iActingChar;
            m_lastMainState = spellInfo.Game.InCombat ? MainState.Combat : MainState.Unknown;
            m_lastInOut = spellInfo.Game.Location.Outside ? 2 : 1;
            m_lastNumKnownSpells = character.NumKnownSpells;
            m_bAutoClose = true;

            ShowSpells(spellInfo, character, bFavoritesOnly);

            m_character = character;
            return true;
        }

        private string SpellNoun(string str)
        {
            return (str == "Bard" ? "Bard Songs" : (str + " Spells"));
        }

        private void UpdateUI()
        {
            ShowSpells(m_lastSpellinfo, m_character, false);
            if (m_iLastSelectedTab != -1 && tcSpells.TabPages.Count > m_iLastSelectedTab)
                tcSpells.SelectedIndex = m_iLastSelectedTab;
        }

        public void ShowSpells(SpellInfo info, BaseCharacter character, bool bFavoritesOnly)
        {
            m_lastSpellinfo = info;

            SpellType type = SpellType.Unknown;
            string strCharDesc = "";

            m_bUpdatingTabs = true;
            int level = 99;
            if (character != null)
            {
                type = Global.GetSpellType(character.BasicClass);
                level = character.QuickRefSpellLevel.Temporary;
                if (info.UsesSpellLevel)
                {
                    strCharDesc = String.Format(" for {0}, a{1}{2} with spell level {3}, {4} SP, {5} gems",
                        character.Name,
                        character.BasicClass == GenericClass.Archer ? "n " : " ",
                        BaseCharacter.ClassString(character.BasicClass),
                        character.QuickRefSpellLevel.Temporary,
                        character.QuickRefSpellPoints.Current,
                        character.QuickRefGems);
                }
                else if (character.BasicClass == GenericClass.Bard)
                {
                    strCharDesc = String.Format(" for {0}, a Bard with {1}",
                        character.Name,
                        Global.Plural(character.Songs, "Song"));
                }
                else
                {
                    strCharDesc = String.Format(" for {0}, a{1}{2} with {3} SP",
                        character.Name,
                        character.BasicClass == GenericClass.Archer ? "n " : " ",
                        BaseCharacter.ClassString(character.BasicClass),
                        character.QuickRefSpellPoints.Current);
                }
            }

            if (character == null || !m_favorites.ShowFavorites)
            {
                if (tcSpells.TabPages[0] == tpFavorites)
                    tcSpells.TabPages.Remove(tpFavorites);
            }
            else
            {
                if (tcSpells.TabPages[0] != tpFavorites)
                    tcSpells.TabPages.Insert(0, tpFavorites);
            }

            foreach (SpellRefPanel srp in m_spellLists)
            {
                if (srp != spellListFavorites && bFavoritesOnly)
                    continue;
                srp.SetCharacter(character);
                srp.SpellsUseLevelOnly = Hacker.SpellsUseLevelOnly;
                srp.ShowDuration = Hacker.SpellsHaveDuration;
                srp.SpellListView.BeginUpdate();
                srp.SpellListView.Items.Clear();
            }

            bool bFilterCombat = (character != null && !(info is MM3SpellInfo || info is MM45SpellInfo) && m_bFilterUncastable);
            bool bFilterOutside = (info != null && m_bFilterUncastable);
            bool bFilterType = (character != null);
            string strLocation = "";
            if (bFilterCombat)
            {
                strLocation = info.Game.InCombat ? "Combat " : "Non-Combat ";
            }

            if (!bFavoritesOnly)
            {
                tabPage1.Text = Hacker.SpellType1;
                tabPage2.Text = Hacker.SpellType2;
                tabPage3.Text = Hacker.SpellType3;
                tabPage4.Text = Hacker.SpellType4;
                tabPage5.Text = Hacker.SpellType5;
                tabPage6.Text = Hacker.SpellType6;
                tabPage7.Text = Hacker.SpellType7;
                tabPage8.Text = Hacker.SpellType8;
                tabPage9.Text = Hacker.SpellType9;

                spellList1.Title = strLocation + SpellNoun(Hacker.SpellType1) + strCharDesc;
                spellList2.Title = strLocation + SpellNoun(Hacker.SpellType2) + strCharDesc;
                spellList3.Title = strLocation + SpellNoun(Hacker.SpellType3) + strCharDesc;
                spellList4.Title = strLocation + SpellNoun(Hacker.SpellType4) + strCharDesc;
                spellList5.Title = strLocation + SpellNoun(Hacker.SpellType5) + strCharDesc;
                spellList6.Title = strLocation + SpellNoun(Hacker.SpellType6) + strCharDesc;
                spellList7.Title = strLocation + SpellNoun(Hacker.SpellType7) + strCharDesc;
                spellList8.Title = strLocation + SpellNoun(Hacker.SpellType8) + strCharDesc;
                spellList9.Title = strLocation + SpellNoun(Hacker.SpellType9) + strCharDesc;
            }
            spellListFavorites.Title = "Favorite Spells" + strCharDesc;

            int iNumSpells = Games.GetSpellCount(Hacker.Game);

            Dictionary<int, ListViewItem> lviFavorites = new Dictionary<int, ListViewItem>();

            for (int i = 0; i < iNumSpells; i++)
            {
                Spell spell = Games.GetSpell(Hacker.Game, i);
                bool bFilter = FilterSpell(bFilterCombat, bFilterOutside, bFilterType, level, character == null ? GenericClass.Knight : character.BasicClass, spell, info);
                ListViewItem lvi = spellList1.GetLVIForSpell(spell);
                if (character != null && m_favorites.IsFavorite(Hacker.Game, character.Name, spell.BasicIndex) && !lviFavorites.ContainsKey(spell.BasicIndex))
                {
                    ListViewItem lviFavorite = spellList1.GetLVIForSpell(spell);
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem(lviFavorite, Spell.TypeString(spell));
                    sub.ForeColor = lviFavorite.ForeColor;
                    sub.BackColor = lviFavorite.BackColor;
                    lviFavorite.SubItems.Insert(0, sub);
                    lviFavorites.Add(spell.BasicIndex, lviFavorite);
                }

                if (bFavoritesOnly || bFilter)
                    continue;

                switch (spell.Type)
                {
                    case SpellType.Cleric:
                    case SpellType.Priest:
                    case SpellType.Conjurer:
                        spellList1.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Druid:
                    case SpellType.Magician:
                        spellList2.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Sorcerer:
                    case SpellType.Mage:
                        spellList3.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Wizard:
                        spellList4.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Archmage:
                        spellList5.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Chronomancer:
                        spellList7.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Geomancer:
                        spellList8.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Miscellaneous:
                        spellList9.SpellListView.Items.Add(lvi);
                        break;
                    case SpellType.Bard:
                        if (Hacker.Game == GameNames.BardsTale1)
                            spellList5.SpellListView.Items.Add(lvi);
                        else
                            spellList6.SpellListView.Items.Add(lvi);
                        break;
                    default:
                        break;
                }
            }

            foreach (int i in m_favorites.GetFavorites(Hacker.Game, character?.Name))
            {
                if (lviFavorites.ContainsKey(i))
                    spellListFavorites.SpellListView.Items.Add(lviFavorites[i]);
            }

            if (!bFavoritesOnly)
            {
                foreach (SpellRefPanel srp in m_spellLists)
                {
                    srp.SizeHeadersAndContent();
                    srp.SpellListView.EndUpdate();
                    if (srp == spellListFavorites)
                        continue;   // show/hide for favorites is based on user option, not number of items
                    if (srp.SpellListView.Items.Count < 1)
                    {
                        if (tcSpells.TabPages.Contains(srp.ParentTab))
                            tcSpells.TabPages.Remove(srp.ParentTab);
                    }
                    else if (!tcSpells.TabPages.Contains(srp.ParentTab))
                        tcSpells.TabPages.Add(srp.ParentTab);
                }

                if (character != null && character.BasicClass == GenericClass.Bard)
                {
                    foreach (TabPage page in tcSpells.TabPages)
                    {
                        if (page.Text.ToLower() == "bard")
                        {
                            tcSpells.SelectedTab = page;
                            break;
                        }
                    }
                }
                else if (character != null && tcSpells.TabPages.Contains(tpFavorites) && spellListFavorites.SpellListView.Items.Count > 0)
                    tcSpells.SelectedTab = tpFavorites;
                else
                {
                    // Select the first tab that has any non-gray spells
                    foreach (SpellRefPanel srp in m_spellLists)
                    {
                        if (Global.AnyEnabled(srp.SpellListView))
                        {
                            tcSpells.SelectedTab = srp.ParentTab;
                            break;
                        }
                    }
                }
            }
            else
            {
                spellListFavorites.SizeHeadersAndContent();
                spellListFavorites.SpellListView.EndUpdate();
            }

            Properties.Settings.Default.FavoriteSpells = m_favorites;
            m_bUpdatingTabs = false;
        }

        private bool FilterSpell(bool bCombat, bool bOutside, bool bType, int level, GenericClass charClass, Spell spell, SpellInfo info)
        {
            if (spell.Level > level)
                return true;
            if (bCombat)
            {
                if (!spell.When.HasFlag(SpellWhen.Combat) && info.Game.InCombat)
                    return true;
                if (!spell.When.HasFlag(SpellWhen.NonCombat) && !info.Game.InCombat)
                    return true;
            }
            if (bOutside)
            {
                bool bOutdoors = (info.Game.Location.Outside || info.Game.Location.Town);
                if (!spell.When.HasFlag(SpellWhen.Indoors) && !info.Game.Location.Outside)
                    return true;
                if (!spell.When.HasFlag(SpellWhen.Outdoors) && info.Game.Location.Outside)
                    return true;
            }
            if (bType && info.ClassLimited)
            {
                switch (charClass)
                {
                    case GenericClass.Archer:
                    case GenericClass.Sorcerer:
                        if (spell.Type != SpellType.Sorcerer)
                            return true;
                        break;
                    case GenericClass.Paladin:
                    case GenericClass.Cleric:
                        if (spell.Type != SpellType.Cleric)
                            return true;
                        break;
                    case GenericClass.Ranger:
                    case GenericClass.Druid:
                        if (spell.Type != SpellType.Druid)
                            return true;
                        break;
                    default:
                        return true;    // Other classes can cast no spells at all
                }
            }
            return false;
        }

        private void timerRefreshSpellInfo_Tick(object sender, EventArgs e)
        {
            if (Hacker != null && Hacker.Running)
                RefreshUI();
        }

        private void RefreshUI(bool bFavoritesOnly = false)
        {
            SetSpellInfo(Hacker.GetSpellInfo(), bFavoritesOnly);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!scMainFind.Panel2Collapsed)
                m_findBox.Next(sender, new BoolHandlerEventArgs(false));
            else
                Close();
        }

        protected override void WndProc(ref Message m)
        {
            if (m_main != null)
                m_main.ProcessMessage(ref m, this);
            base.WndProc(ref m);
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

        private void OnDoubleClickSpell(object sender, EventArgs e)
        {
            SpellTag spellTag = GetSelectedSpell();
            ActivateSpell(spellTag);
        }

        private void ActivateSpell(SpellTag spellTag)
        {
            if (spellTag == null)
                return;
            if (Properties.Settings.Default.DoubleClickSpellSendsKeys && Hacker != null && Hacker.GetGameState().Casting && spellTag.Castable)
            {
                if (SendSpellKeys(spellTag))
                    return;
            }
            ShowSpellInfo(spellTag);
        }

        private void ShowSpellInfo(SpellTag spellTag)
        {
            if (spellTag == null || spellTag.Spell == null)
                return;
            Spell spell = spellTag.Spell;
            string strCaption = spell.Name;
            string strCast = spellTag.Castable ? "" : ", not castable by this character";
            if (spell.Number == 0)
                strCaption = String.Format("{0} ({1}){2}", spell.Name, Spell.TypeString(spell.Type), strCast);
            else
                strCaption = String.Format("{0}{1}-{2} ({3}){4}", Spell.TypeString(spell.Type)[0], spell.Level, spell.Number, spell.Name, strCast);
            ViewInfoForm.ShowCentered(tcSpells, spell.MultiLineDescription, strCaption);
        }

        private void lvDruidSpells_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClickSpell(sender, e);
        }

        private void miCtxViewSpellInfo_Click(object sender, EventArgs e)
        {
            ShowSpellInfo(GetSelectedSpell());
        }

        private SpellRefPanel GetSelectedSpellPanel()
        {
            foreach (SpellRefPanel srp in m_spellLists)
                if (tcSpells.SelectedTab == srp.ParentTab)
                    return srp;
            return null;
        }

        private SpellTag GetSelectedSpell()
        {
            ListViewItem lvi = GetSelectedSpellLVI();
            if (lvi == null)
                return null;

            return lvi.Tag as SpellTag;
        }

        private ListViewItem GetSelectedSpellLVI()
        {
            SpellRefPanel srp = GetSelectedSpellPanel();
            if (srp == null || srp.SpellListView.SelectedItems.Count < 1)
                return null;

            return srp.SpellListView.SelectedItems[0];
        }

        private void miCtxSendSpellKeys_Click(object sender, EventArgs e)
        {
            SendSpellKeys(GetSelectedSpell());
        }

        private bool SendSpellKeys(SpellTag spellTag)
        {
            if (spellTag == null || spellTag.Spell == null || Hacker == null || !Hacker.IsValid)
                return false;

            Keys[] keys = spellTag.Spell.GetKeys(m_character);
            if (keys == null)
                return false;

            return Hacker.SendKeysToDOSBox(Hacker.DelayBetweenSpellKeys, keys);
        }

        private void SpellReferenceForm_Load(object sender, EventArgs e)
        {
            spellListFavorites.SpellListView.EnableDragging = true;
            spellListFavorites.SpellListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            spellListFavorites.SpellListView.ColumnClick += SpellListView_ColumnClick;

            m_findBox = new FindBox(scMainFind, tbFind, FindBox.MultiSpellRefFindFunction, m_spellLists);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;

            scMainFind.Panel2Collapsed = true;

            miCtxFilterUncastable.Checked = m_bFilterUncastable;

            timerRefreshSpellInfo.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (scMainFind.Panel2Collapsed)
                Close();
            m_findBox.HideFindBox();
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
        }

        private ListView GetFocusedListView()
        {
            SpellRefPanel panel = GetSelectedSpellPanel();
            if (panel == null)
                return null;
            return panel.SpellListView;
        }

        private void miCtxCopyAllInfo_Click(object sender, EventArgs e)
        {
            ListView lv = GetFocusedListView();
            if (lv == null)
                return;

            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in lv.Items)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                if (tag == null)
                    continue;
                if (lv == spellListFavorites.SpellListView)
                    sb.AppendFormat("{0}\t", Spell.TypeString(tag.Spell.Type));
                // level name cost when target description
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                    tag.Spell.Level, tag.Spell.Name, tag.Spell.Cost.LongString,
                    tag.Spell.WhenString, tag.Spell.TargetStringFull, tag.Spell.ShortDescription, tag.Spell.Description);
                sb.AppendLine();
            }
            Clipboard.SetText(sb.ToString());
        }

        private void SpellReferenceForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:
                    if (m_findBox.Focused)
                        return;
                    int iIndex = e.KeyCode - Keys.D1;
                    if (e.KeyCode == Keys.D0)
                        iIndex += 10;
                    if (iIndex >= tcSpells.TabPages.Count)
                        return;
                    tcSpells.SelectedIndex = iIndex;
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Enter:
                    OnDoubleClickSpell(sender, e);
                    break;
            }
        }

        private void miCtxFavorites_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetSelectedSpellLVI();
            if (lvi == null)
                return;

            if (GetSelectedSpellPanel() == spellListFavorites)
            {
                m_favorites.RemoveFavorite(Hacker.Game, m_character.Name, (lvi.Tag as SpellTag).Spell.BasicIndex);
            }
            else
            {
                m_favorites.AddFavorite(Hacker.Game, m_character.Name, (lvi.Tag as SpellTag).Spell.BasicIndex);
                m_favorites.ShowFavorites = true;
            }

            RefreshUI(true);
        }

        private void cmSpell_Opening(object sender, CancelEventArgs e)
        {
            if (GetSelectedSpellPanel() == spellListFavorites)
            {
                miCtxFavorites.Text = "Remove from &Favorites";
                miCtxRemoveAllFavorites.Visible = spellListFavorites.SpellListView.Items.Count > 0;
            }
            else
            {
                miCtxFavorites.Text = "Add to &Favorites";
                miCtxRemoveAllFavorites.Visible = false;
            }

            bool bSelected = GetSelectedSpellLVI() != null;
            miCtxFavorites.Visible = bSelected;
            miCtxSendSpellKeys.Visible = bSelected;
            miCtxViewSpellInfo.Visible = bSelected;
            miCtxFilterUncastable.Checked = m_bFilterUncastable;
        }

        private void miCtxRemoveAllFavorites_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Remove {0}?", Global.Plural(spellListFavorites.SpellListView.Items.Count, "favorite spell")),
                    "Remove all favorites?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                m_favorites.RemoveFavorites(Hacker.Game, m_character.Name);
                RefreshUI(true);
            }
        }

        private void miCtxFilterUncastable_Click(object sender, EventArgs e)
        {
            miCtxFilterUncastable.Checked = !miCtxFilterUncastable.Checked;
            m_bFilterUncastable = miCtxFilterUncastable.Checked;
            UpdateUI();
        }

        private void tcSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingTabs)
                return;
            m_iLastSelectedTab = tcSpells.SelectedIndex;
        }
    }

    class SpellReferenceComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public SpellReferenceComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }

        public SpellReferenceComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }

        public int Compare(object x, object y)
        {
            ListViewItem lvi1 = x as ListViewItem;
            ListViewItem lvi2 = y as ListViewItem;
            if (lvi1 == null || lvi2 == null)
                return 0;
            SpellTag spell1 = lvi1.Tag as SpellTag;
            SpellTag spell2 = lvi2.Tag as SpellTag;
            if (spell1 == null || spell2 == null)
                return 0;

            int returnVal = -1;

            // Columns: type, level, name, cost, when, duration, target, description

            switch (m_column)
            {
                case 1:
                    returnVal = Math.Sign(spell1.Spell.Level - spell2.Spell.Level);
                    break;
                case 3:
                    returnVal = Math.Sign(spell1.Spell.Cost.SpellPoints - spell2.Spell.Cost.SpellPoints);
                    break;
                default:
                    returnVal = String.Compare(lvi1.SubItems[m_column].Text, lvi2.SubItems[m_column].Text);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }

    public class SpellCost
    {
        public int SpellPoints;
        public bool PerLevel;
        public int Gems = 0;

        public SpellCost(int sp)
        {
            SpellPoints = sp;
            PerLevel = false;
            Gems = 0;
        }

        public override string ToString()
        {
            return String.Format("{0} SP", SpellPoints);
        }

        public virtual string ShortString { get { return ToString(); } }
        public virtual string LongString { get { return ToString(); } }

    }

    public class WizardrySpellCost : SpellCost
    {
        public int SpellLevel;

        public WizardrySpellCost(int level) : base(1)
        {
            SpellLevel = level;
            SpellPoints = 1;
        }

        public override string ShortString { get { return String.Format("L{0}", SpellLevel); } }

        public override string ToString()
        {
            return String.Format("1 Lev{0}", SpellLevel);
        }
    }

    public class MMSpellCost : SpellCost
    {
        public MMSpellCost() : base(0)
        {
            SpellPoints = 0;
            PerLevel = false;
            Gems = 0;
        }

        public MMSpellCost(int sp, bool perlevel, int gems) : base(sp)
        {
            PerLevel = perlevel;
            Gems = gems;
        }

        public override string ShortString { get { return ToString(); } }

        public override string LongString
        { 
            get
            {
                return String.Format("{0} SP{1}{2}", SpellPoints, PerLevel ? " per level" : "", Gems > 0 ? String.Format(" +{0}", Global.Plural(Gems, "Gem")) : "");
            }
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}", SpellPoints, PerLevel ? "/lv" : "", Gems > 0 ? String.Format("+{0}G", Gems) : "");
        }
    }

    public class SpellTag
    {
        public Spell Spell;
        public bool Castable;

        public SpellTag(Spell spell, bool bCastable)
        {
            Spell = spell;
            Castable = bCastable;
        }
    }

    [Flags]
    public enum SpellWhen
    {
        None =       0x0000,
        Combat =     0x0001,
        NonCombat =  0x0002,
        Indoors =    0x0004,
        Outdoors =   0x0008,
        Ranged =     0x0010,
        Land =       0x0020,
        Water =      0x0040,
        Camp =       0x0080,
        Looting =    0x0100,
        NonCombatLooting = Looting | NonCombat | Anywhere,
        NonCombatCamp = Camp | NonCombat | Anywhere,
        RangedCombat = Combat | Ranged,
        RangedCombatAnywhere = Combat | Ranged | Anywhere,
        Anytime = Combat | NonCombat,
        Anywhere = Indoors | Outdoors | Land | Water,
        AnywhereAnytime = Anytime | Anywhere,
        OutdoorCombat = Combat | Outdoors,
        OutdoorNonCombat = NonCombat | Outdoors,
        CombatAnywhere = Combat | Anywhere,
        NonCombatAnywhere = NonCombat | Anywhere,
        IndoorNonCombat = NonCombat | Indoors,
        NonCombatLand = NonCombat | Land | Indoors | Outdoors,
    }

    public enum SpellDuration
    {
        Instant = 0,
        OneMove = 1,
        OneRound = 2,
        Combat = 3,
        Short = 4,
        Medium = 5,
        Long = 6,
        Indefinite = 7
    }

    public enum SpellTarget
    {
        Caster = 0,
        Monster = 1,
        Character = 2,
        CharacterSameAlign = 3,
        Party = 4,
        AllUndeadMonsters = 5,
        NonUndeadMonster = 6,
        ThreeMonsters = 7,
        FiveMonsters = 8,
        AllMeleeMonsters = 9,
        AllRangedMonsters = 10,
        AllMonsters = 11,
        AllPlayersAndMonsters = 12,
        FiveRangedMonsters = 13,
        ThreeRangedMonsters = 14,
        TenMonsters = 15,
        FourMonstersPlusOnePerLevel = 16,
        FourMonsters = 17,
        FiveStepsPerLevel = 18,
        CharacterOnce = 19,
        TwoMonsters = 20,
        ThreeNonUndeadMonsters = 21,
        SixMonsters = 22,
        OneRangedMonster = 23,
        FourRangedMonstersPlusOnePerLevel = 24,
        SixRangedMonsters = 25,
        MonsterGroup = 26,
        UndeadMonsterGroup = 27,
        AllVisibleMonsters = 28,
        AnimalGroup = 29,
        Dragon = 30,
        Item = 31,
        Golem = 32,
        PartyAndVisibleMonsters = 33,
        Variable = 34,
        Special = 35,
        ThirtyFeet = 36,
        OneWall = 37,
        Monster10Feet = 38,
        MonsterGroup20Feet = 39,
        MonsterGroup30Feet = 40,
        MonsterGroup40FeetDiminish = 41,
        Monster70Feet = 42,
        Monster40FeetDiminish = 44,
        AllMonsters30FeetDiminish = 45,
        MonsterGroup10Feet = 46,
        Monster30Feet = 47,
        MonsterGroup50Feet = 48,
        MonsterGroup60Feet = 49,
        AllMonsters40Feet = 50,
        Level = 51,
        MonsterGroup80Feet = 52,
        MonsterGroup40Feet = 53,
        Monster60Feet = 54,
        Monster90Feet = 55,
        Unknown = 255
    }

    public class Spell
    {
        public SpellType Type = SpellType.Unknown;
        public string Name;
        public int Level = 0;
        public int Number = 0;
        public SpellWhen When = SpellWhen.None;
        public SpellTarget Target = SpellTarget.Unknown;
        public SpellCost Cost;
        public string ShortDescription = null;
        public string Abbreviation = null;
        public string Description = null;
        public string Learned = null;

        public virtual int BasicIndex { get { return 0; } }
        public virtual bool UsesLevelOnly { get { return false; } }
        public virtual string ExtendedName { get { return Name; } }
        public virtual Keys[] GetKeys(BaseCharacter character = null) { return null; }
        public virtual bool MayHaveDuplicateNames { get { return false; } }

        public static string TypeString(SpellType type)
        {
            switch (type)
            {
                case SpellType.Druid: return "Druid";
                case SpellType.Sorcerer: return "Sorcerer";
                case SpellType.Cleric: return "Cleric";
                case SpellType.Mage: return "Mage";
                case SpellType.Priest: return "Priest";
                case SpellType.Conjurer: return "Conjurer";
                case SpellType.Magician: return "Magician";
                case SpellType.Wizard: return "Wizard";
                case SpellType.Archmage: return "Archmage";
                case SpellType.Bard: return "Bard";
                case SpellType.Chronomancer: return "Chronomancer";
                case SpellType.Geomancer: return "Geomancer";
                case SpellType.Miscellaneous: return "Misc";
                default: return "Unknown";
            }
        }

        public static string TypeString(Spell spell)
        {
            if (spell is MM345Spell && spell.Type == SpellType.Sorcerer)
                return "Arcane";
            return TypeString(spell.Type);
        }

        public virtual string MultiLineDescription
        {
            get
            {
                return String.Format("Name: {0}\r\nType: {1}\r\nLevel: {2}\r\nWhen: {3}\r\nTarget: {4}\r\nLearned: {5}\r\nShort: {6}\r\n{7}",
                    Name, TypeString(Type), Level, WhenString, TargetStringFull, Learned, ShortDescription, Description);
            }
        }

        public static string GetDurationString(SpellDuration duration)
        {
            switch (duration)
            {
                case SpellDuration.Combat: return "Combat";
                case SpellDuration.Indefinite: return "Indefinite";
                case SpellDuration.Instant: return "Instant";
                case SpellDuration.Long: return "Long";
                case SpellDuration.Medium: return "Medium";
                case SpellDuration.OneMove: return "One Move";
                case SpellDuration.OneRound: return "One Round";
                case SpellDuration.Short: return "Short";
                default: return String.Format("Unknown ({0})", (int)duration);
            }
        }

        public virtual string DurationString { get { return String.Empty; } }

        public string WhenString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (When.HasFlag(SpellWhen.Anytime))
                    sb.Append("Anytime, ");
                else if (When.HasFlag(SpellWhen.Combat))
                {
                    if (When.HasFlag(SpellWhen.Ranged))
                        sb.Append("Ranged ");
                    sb.Append("Combat, ");
                }
                else if (When.HasFlag(SpellWhen.Looting))
                    sb.Append("Looting, ");
                else if (When.HasFlag(SpellWhen.Camp))
                    sb.Append("Camp, ");
                else
                    sb.Append("Non-Combat, ");

                if (!When.HasFlag(SpellWhen.Anywhere))
                {
                    if (!When.HasFlag(SpellWhen.Indoors))
                        sb.Append("Outdoor");
                    else if (!When.HasFlag(SpellWhen.Outdoors))
                        sb.Append("Indoor");
                    else if (!When.HasFlag(SpellWhen.Water))
                        sb.Append("Land");
                    else if (!When.HasFlag(SpellWhen.Land))
                        sb.Append("Water");
                }

                return Global.Trim(sb).ToString();
            }
        }

        public string TargetString
        {
            get
            {
                switch (Target)
                {
                    case SpellTarget.Caster: return "Caster";
                    case SpellTarget.Monster: return "Monster";
                    case SpellTarget.Character: return "Character";
                    case SpellTarget.CharacterSameAlign: return "Character Same Align";
                    case SpellTarget.Party: return "Party";
                    case SpellTarget.AllUndeadMonsters: return "All Undead Monsters";
                    case SpellTarget.NonUndeadMonster: return "Non Undead Monster";
                    case SpellTarget.ThreeMonsters: return "3 Monsters";
                    case SpellTarget.FiveMonsters: return "5 Monsters";
                    case SpellTarget.AllMeleeMonsters: return "All Melee Monsters";
                    case SpellTarget.AllRangedMonsters: return "All Ranged Monsters";
                    case SpellTarget.AllMonsters: return "All Monsters";
                    case SpellTarget.AllPlayersAndMonsters: return "All";
                    case SpellTarget.FiveRangedMonsters: return "5 Ranged Monsters";
                    case SpellTarget.ThreeRangedMonsters: return "3 Ranged Monsters";
                    case SpellTarget.TenMonsters: return "10 Monsters";
                    case SpellTarget.FourMonstersPlusOnePerLevel: return "4 Monsters +1/Lv";
                    case SpellTarget.FourMonsters: return "4 Monsters";
                    case SpellTarget.FiveStepsPerLevel: return "5 Steps/Lv";
                    case SpellTarget.CharacterOnce: return "1 Character Once";
                    case SpellTarget.TwoMonsters: return "2 Monsters";
                    case SpellTarget.ThreeNonUndeadMonsters: return "3 Non Undead Monsters";
                    case SpellTarget.SixMonsters: return "6 Monsters";
                    case SpellTarget.SixRangedMonsters: return "6 Ranged Monsters";
                    case SpellTarget.FourRangedMonstersPlusOnePerLevel: return "4 Ranged +1/Lv";
                    case SpellTarget.OneRangedMonster: return "1 Ranged Monster";
                    case SpellTarget.MonsterGroup: return "1 Monster Group";
                    case SpellTarget.UndeadMonsterGroup: return "1 Undead Group";
                    case SpellTarget.AllVisibleMonsters: return "Visible Monsters";
                    case SpellTarget.AnimalGroup: return "1 Animal Group";
                    case SpellTarget.Dragon: return "1 Dragon";
                    case SpellTarget.Item: return "1 Item";
                    case SpellTarget.Golem: return "1 Golem";
                    case SpellTarget.PartyAndVisibleMonsters: return "Visible and Party";
                    case SpellTarget.Variable: return "Variable";
                    case SpellTarget.ThirtyFeet: return "30 Feet";
                    case SpellTarget.AllMonsters30FeetDiminish: return "All Monsters (30' +)";
                    case SpellTarget.AllMonsters40Feet: return "All Monsters (40')";
                    case SpellTarget.Monster40FeetDiminish: return "Monster (40' +)";
                    case SpellTarget.MonsterGroup10Feet: return "Monster Group (10')";
                    case SpellTarget.MonsterGroup20Feet: return "Monster Group (20')";
                    case SpellTarget.MonsterGroup30Feet: return "Monster Group (30')";
                    case SpellTarget.MonsterGroup40Feet: return "Monster Group (40')";
                    case SpellTarget.MonsterGroup40FeetDiminish: return "Monster Group (30' +)";
                    case SpellTarget.MonsterGroup50Feet: return "Monster Group (50')";
                    case SpellTarget.MonsterGroup60Feet: return "Monster Group (60')";
                    case SpellTarget.MonsterGroup80Feet: return "Monster Group (80')";
                    case SpellTarget.Monster10Feet: return "Monster (10')";
                    case SpellTarget.Monster30Feet: return "Monster (30')";
                    case SpellTarget.Monster60Feet: return "Monster (60')";
                    case SpellTarget.Monster90Feet: return "Monster (90')";
                    case SpellTarget.Monster70Feet: return "Monster (70')";
                    case SpellTarget.OneWall: return "One Wall";
                    case SpellTarget.Level: return "Map Level";
                    default: return "Unknown";
                }
            }
        }

        public string TargetStringFull
        {
            get
            {
                switch (Target)
                {
                    case SpellTarget.CharacterSameAlign: return "One character, same alignment as caster";
                    case SpellTarget.Party: return "Entire party";
                    case SpellTarget.AllUndeadMonsters: return "All undead monsters";
                    case SpellTarget.NonUndeadMonster: return "One non-undead monster";
                    case SpellTarget.ThreeMonsters: return "3 monsters";
                    case SpellTarget.FiveMonsters: return "5 monsters";
                    case SpellTarget.AllMeleeMonsters: return "All monsters in melee range";
                    case SpellTarget.AllRangedMonsters: return "All monsters not in melee range";
                    case SpellTarget.AllMonsters: return "All monsters";
                    case SpellTarget.AllPlayersAndMonsters: return "All monsters and characters";
                    case SpellTarget.FiveRangedMonsters: return "5 monsters not in melee range";
                    case SpellTarget.ThreeRangedMonsters: return "3 monsters not in melee range";
                    case SpellTarget.TenMonsters: return "10 monsters";
                    case SpellTarget.FourMonstersPlusOnePerLevel: return "4 monsters + 1 per level of the caster";
                    case SpellTarget.FourMonsters: return "4 monsters";
                    case SpellTarget.FiveStepsPerLevel: return "5 steps per level of the caster";
                    case SpellTarget.CharacterOnce: return "1 character, once per combat";
                    case SpellTarget.TwoMonsters: return "2 monsters";
                    case SpellTarget.ThreeNonUndeadMonsters: return "3 non-undead monsters";
                    case SpellTarget.SixMonsters: return "6 monsters";
                    case SpellTarget.SixRangedMonsters: return "6 monsters not in melee range";
                    case SpellTarget.FourRangedMonstersPlusOnePerLevel: return "4 monsters not in melee range + 1 per level of the caster";
                    case SpellTarget.OneRangedMonster: return "1 ranged monster";
                    case SpellTarget.MonsterGroup: return "One monster group";
                    case SpellTarget.UndeadMonsterGroup: return "One undead monster group";
                    case SpellTarget.AllVisibleMonsters: return "All visible monsters";
                    case SpellTarget.AnimalGroup: return "1 group of animal monsters";
                    case SpellTarget.Dragon: return "One dragon";
                    case SpellTarget.Item: return "One item";
                    case SpellTarget.Golem: return "One golem";
                    case SpellTarget.PartyAndVisibleMonsters: return "All visible monsters and party";
                    case SpellTarget.Variable: return "Variable";
                    default: return TargetString;
                }
            }
        }
    }

    public class FavoriteSpell : Spell
    {
        private int m_index;

        public FavoriteSpell(int index)
        {
            m_index = -999 + index;
            Name = String.Format("Favorite Spell #{0}", m_index + 1000);
        }

        public override int BasicIndex { get { return m_index; } }
        public override string MultiLineDescription { get { return String.Format("The pseudo-spell represents spell #{0} in the character's \"Favorite Spells\" list, if any", m_index + 1000); } }
    }

    public class MMSpell : Spell
    {
        public virtual MMSpell None { get { return new MMSpell(); } }

        public override string MultiLineDescription
        {
            get
            {
                if (Number == 0)
                    return String.Format("Name: {0}\r\nType: {1}\r\nLevel: {2}\r\nCost: {3}\r\nWhen: {4}\r\nTarget: {5}\r\nLearned: {6}\r\nShort: {7}\r\n{8}",
                        Name, TypeString(Type), Level, Cost.LongString, WhenString, TargetStringFull, Learned, ShortDescription, Description);
                return String.Format("Name: {0}\r\nType: {1}\r\nLevel: {2}\r\nNumber: {3}\r\nCost: {4}\r\nWhen: {5}\r\nTarget: {6}\r\nLearned: {7}\r\nShort: {8}\r\n{9}",
                    Name, TypeString(Type), Level, Number, Cost.LongString, WhenString, TargetStringFull, Learned, ShortDescription, Description);
            }
        }
    }

    public enum SpellType
    {
        Cleric = 0,
        Sorcerer = 1,
        Druid = 2,
        Priest = 3,
        Mage = 4,
        Conjurer = 5,
        Magician = 6,
        Wizard = 7,
        Bard = 8,
        Archmage = 9,
        Chronomancer = 10,
        Geomancer = 11,
        Miscellaneous = 12,
        Unknown = -1
    }

    public enum MMGenericSpell
    {
        None = 0,
        Jump = 1,
        Fly = 2,
        Shelter = 3,
        Teleport = 4,
        Etherealize = 5,
        Surface = 6,
        TownPortal = 7
    }
}
