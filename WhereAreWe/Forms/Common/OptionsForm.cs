using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace WhereAreWe
{
    public partial class OptionsForm : HackerBasedForm, IKeyboardHookCallback
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private ListViewItem m_lviEditing = null;
        private bool[] m_keysCurrent = new bool[256];
        private List<Keys> m_keysShortcut = new List<Keys>();
        private ShortcutKeys m_keysOriginal = null;
        private int m_iShortcutEditing = -1;
        private bool m_bSkipOK = false;
        private bool m_bWaitForKeysUp = false;
        private bool m_bNoMoreShortcutKeys = true;
        private bool m_bResetAllVisited = false;
        private bool m_bResetWindows = false;
        private Point m_ptContextOpened = Global.NullPoint;
        private bool m_bLoadNewMap = false;
        private Timer timerAutoload = new Timer();
        private GameStrings m_autoLoadFiles = null;
        private bool m_bUpdatingSpellGame = false;
        private SpellHotkeyCollection m_shkc;
        private bool m_bSetKeys = false;
        private SquareStyleList m_styles = new SquareStyleList();
        public bool SquareStylesChanged = false;
        private FindBox m_findBox = null;
        private Notifications m_notifications = null;
        ComboBox[] m_comboChars;
        ComboBox[] m_comboSpells;
        private static GameNames m_lastSpellHotkeyGame = GameNames.None;

        public int StartPage { get; set; }

        public OptionsForm()
        {
            StartPage = 3;
            InitializeComponent();
            UpdateUI();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_findBox.HideFindBox();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override bool OnCommonKeyEscape()
        {
            if (!scKeyboard.Panel2Collapsed && tcOptions.SelectedTab == tpKeyboard)
            {
                scKeyboard.Panel2Collapsed = true;
                return true;
            }
            Close();
            return false;
        }

        private GameNames SelectedGame { get { return ((CurrentGameName)comboCurrentGame.SelectedItem).Game; } }

        private void SwitchMapIfNecessary(string strNewMap)
        {
            if (m_autoLoadFiles.Get(SelectedGame) != strNewMap)
            {
                m_autoLoadFiles.Set(SelectedGame, strNewMap);
                m_bLoadNewMap = true;
            }
            else
                m_bLoadNewMap = false;
        }

        private bool CheckNoDuplicateKeys()
        {
            Dictionary<ShortcutKeys, InputOption> dict = new Dictionary<ShortcutKeys, InputOption>();
            foreach (ListViewItem lvi in lvInput.Items)
            {
                InputOption input = (InputOption)lvi.Tag;
                foreach(ShortcutKeys keys in input.Input)
                {
                    if (keys == null || keys.Keys == null || keys.Keys.Length == 0)
                        continue;

                    if (dict.ContainsKey(keys))
                    {
                        MessageBox.Show(String.Format("The actions \"{0}\" and \"{1}\" are assigned to the same key combination ({2})\r\n\r\n" +
                            "You must change one or both of these before saving the keyboard shortcuts (press F8 to cycle between conflicting entries).",
                            Global.ActionString(dict[keys].Action), Global.ActionString(input.Action), Global.KeyString(keys.Keys)),
                            "Duplicate keyboard shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Global.DeselectAll(lvInput);
                        lvi.Selected = true;
                        tcOptions.SelectedTab = tpKeyboard;
                        lvInput.EnsureVisible(lvi.Index);
                        lvInput.Focus();
                        lvInput.Select();
                        return false;
                    }
                    dict.Add(keys, input);
                }
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_bSkipOK)
            {
                m_bSkipOK = false;
                return;
            }

            if (UpdateFromUI())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool UpdateFromUI()
        {
            if (m_lviEditing != null)
                FinishEditKeys();
            else
                CancelKey();

            if (!CheckNoDuplicateKeys())
            {
                tcOptions.SelectedTab = tpKeyboard;
                return false;
            }

            Properties.Settings.Default.Shortcuts = GetShortcuts();
            Properties.Settings.Default.DefaultZoom = (int) nudDefaultZoom.Value;
            Properties.Settings.Default.DefaultMapSize = new System.Drawing.Size((int) nudDefaultMapWidth.Value, (int) nudDefaultMapHeight.Value);
            Properties.Settings.Default.AutoSwitchSheets = cbAutoSwitchSheets.Checked;
            Properties.Settings.Default.Game = ((CurrentGameName)comboCurrentGame.SelectedItem).Game;
            Properties.Settings.Default.YouAreHereOpacity = (int)nudYouAreHereOpacity.Value;
            Properties.Settings.Default.TreasureOpacity = (int)nudOpacityTreasure.Value;
            Properties.Settings.Default.EnableMemoryWrite = cbEnableMemoryWrite.Checked;
            Properties.Settings.Default.HideUnvisitedSquares = cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.AlwaysRevealEdges = cbAlwaysRevealEdges.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.ShowUnvisitedNotes = cbShowUnvisitedNotes.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.RevealAdjacentInaccessible = cbRevealAdjacentInaccessible.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.UseInGameCartography = cbUseInGameCartography.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.ReadOnlyMaps = cbReadOnlyMaps.Checked;
            Properties.Settings.Default.ReadOnlyNotes = cbReadOnlyNotes.Checked;
            Properties.Settings.Default.FollowCharWindows = cbFollowPartyChar.Checked;
            Global.Windows.SetAutoShow(WindowType.Party, cbShowPartyByDefault.Checked);
            Properties.Settings.Default.ShowSpells = cbShowSpellsWhenCasting.Checked;
            Properties.Settings.Default.EnableGlobalShortcuts = cbEnableKeyboardHook.Checked;
            Properties.Settings.Default.SaveDOSBoxPosition = cbSaveDOSBox.Checked;
            Properties.Settings.Default.ShowEncounters = cbShowEncounters.Checked;
            Properties.Settings.Default.ShowTreasureWindow = cbShowTreasureWindow.Checked && cbShowEncounters.Checked;
            Properties.Settings.Default.SnapWindowsToDOSBox = cbSnapToDOSBox.Checked;
            Properties.Settings.Default.AutoLaunchGame = cbAutoLaunchGame.Checked;
            Properties.Settings.Default.EnableCheats = cbEnableCheats.Checked && cbEnableMemoryWrite.Checked;
            Properties.Settings.Default.PollTime = (int) nudPollingSpeed.Value;
            Properties.Settings.Default.UnvisitedSquareOpacity = (int)nudUnvisitedSquareOpacity.Value;
            Properties.Settings.Default.AutoShowNoteAtLocation = cbAutoShowNoteAtLocation.Checked; 
            Global.Windows.SetAutoShow(WindowType.GameInfo, cbAutoShowInfo.Checked);
            Properties.Settings.Default.MonsterOpacity = (int)nudMonsterOpacity.Value;
            Properties.Settings.Default.ShowMonstersOnMaps = cbShowMonstersOnMaps.Checked;
            Properties.Settings.Default.SingleTaskbarWindow = cbSingleTaskbarWindow.Checked;
            Properties.Settings.Default.PreventOffscreenWindows = cbPreventOffscreenWindows.Checked;
            Properties.Settings.Default.FocusDOSBoxWhenSelected = cbBringDOSBoxToFront.Checked;
            Properties.Settings.Default.AutoSave = cbAutoSave.Checked;
            Properties.Settings.Default.AutosaveSeconds = (int) nudAutosaveMinutes.Value * 60;
            Properties.Settings.Default.AutoSaveFile = tbAutoSave.Text;
            Properties.Settings.Default.ShowActiveSquares = cbShowActiveSquares.Checked;
            Properties.Settings.Default.ShowActiveEncountersOnly = cbShowActiveEncountersOnly.Checked && cbShowActiveSquares.Checked;
            Properties.Settings.Default.ForceDosBoxLocation = cbForceDOSBoxLocation.Checked;
            Properties.Settings.Default.ForceDosBoxSize = cbForceDOSBoxSize.Checked;
            Properties.Settings.Default.DosBoxWindowRect = new Rectangle((int)nudDOSBoxLocationX.Value, (int)nudDOSBoxLocationY.Value,
                (int) nudDOSBoxWidth.Value, (int) nudDOSBoxHeight.Value);
            Properties.Settings.Default.ShowDeadMonsters = cbShowDeadMonsters.Checked;
            Properties.Settings.Default.CureAllHPWithConditions = cbCureAllHPWithConditions.Checked && cbEnableMemoryWrite.Checked;
            Properties.Settings.Default.CopyLocationFormat = comboCopyLocationFormat.Text;
            Properties.Settings.Default.ScriptsDumpFile = tbScriptsDumpFile.Text;
            Properties.Settings.Default.QuestShowGiver = cbQuestPrefixGiver.Checked;
            Properties.Settings.Default.QuestShowReward = cbQuestShowRewards.Checked;
            Properties.Settings.Default.QuestBoldNearby = cbQuestBoldNearbyGoals.Checked;
            Properties.Settings.Default.OfferToSwitchBooks = cbOfferSwitch.Checked;
            SaveGameTitles(lvGameTitles);
            Properties.Settings.Default.QuickScanDOSBox = cbQuickScanDOSBox.Checked;
            Properties.Settings.Default.HideInvalidQuestGoals = cbHideInvalidQuestGoals.Checked;
            Properties.Settings.Default.WarnMultipleReacquire = cbWarnOnMultipleReacquire.Checked;
            Properties.Settings.Default.ShowOnlyDetectableMonsters = cbShowOnlyDetectableMonsters.Checked;
            Properties.Settings.Default.ShowCPU = cbShowCPU.Checked;
            Properties.Settings.Default.ShowActivatedMonstersIcon = cbShowActivatedMonstersIcon.Checked && cbShowMonstersOnMaps.Checked;
            Properties.Settings.Default.ShowListMonstersUnexplored = cbShowListMonstersUnexplored.Checked;
            Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed = cbUpdateCartWhenInaccessibleRevealed.Checked && cbEnableMemoryWrite.Checked;
            Properties.Settings.Default.HideScriptMonsters = cbHideScriptMonsters.Checked;
            Properties.Settings.Default.RefreshDOSBox = cbRefreshDOSBox.Checked;
            Properties.Settings.Default.GameStartDelay = (int) nudLaunchDelay.Value;
            Properties.Settings.Default.WatchdogTimer = (int) nudWatchdog.Value;
            Properties.Settings.Default.SquareStyles = m_styles;
            Properties.Settings.Default.ActionLeftMouse = GetMouseAction(comboMouseLeft);
            Properties.Settings.Default.ActionRightMouse = GetMouseAction(comboMouseRight);
            Properties.Settings.Default.ActionMiddleMouse = GetMouseAction(comboMouseMiddle);
            Properties.Settings.Default.ActionX1Mouse = GetMouseAction(comboMouseX1);
            Properties.Settings.Default.ActionX2Mouse = GetMouseAction(comboMouseX2);
            Properties.Settings.Default.CenterPartyInMap = cbCenterPartyInMap.Checked;
            Properties.Settings.Default.HideScrollbarsInPlayMode = cbHideScrollbarsInPlayMode.Checked;
            Properties.Settings.Default.ShowGridLinesAboveZoom = (int) nudShowGridLinesAboveZoom.Value;
            Properties.Settings.Default.ShowGridLines = cbShowGridLines.Checked;
            Properties.Settings.Default.EditCopyBackgrounds = cbEditCopyBackgrounds.Checked;
            Properties.Settings.Default.EditCopyInnerLines = cbEditCopyInnerLines.Checked;
            Properties.Settings.Default.EditCopyOuterLines = cbEditCopyOuterLines.Checked;
            Properties.Settings.Default.EditCopyIcons = cbEditCopyIcons.Checked;
            Properties.Settings.Default.EditCopyNotes = cbEditCopyNotes.Checked;
            Properties.Settings.Default.MaxUndoActions = (int)nudMaxUndoActions.Value;
            Properties.Settings.Default.AutoSwitchToPlayMode = cbAutoSwitchToPlayMode.Checked;
            Properties.Settings.Default.ActionWheelUp = WheelUpAction(comboWheel);
            Properties.Settings.Default.ActionWheelDown = WheelDownAction(comboWheel);
            Properties.Settings.Default.ActionShiftWheelUp = WheelUpAction(comboShiftWheel);
            Properties.Settings.Default.ActionShiftWheelDown = WheelDownAction(comboShiftWheel);
            Properties.Settings.Default.ActionCtrlWheelUp = WheelUpAction(comboControlWheel);
            Properties.Settings.Default.ActionCtrlWheelDown = WheelDownAction(comboControlWheel);
            Properties.Settings.Default.ActionAltWheelUp = WheelUpAction(comboAltWheel);
            Properties.Settings.Default.ActionAltWheelDown = WheelDownAction(comboAltWheel);
            Properties.Settings.Default.ActionCtrlShiftWheelUp = WheelUpAction(comboControlShiftWheel);
            Properties.Settings.Default.ActionCtrlShiftWheelDown = WheelDownAction(comboControlShiftWheel);
            Properties.Settings.Default.ActionCtrlAltWheelUp = WheelUpAction(comboControlAltWheel);
            Properties.Settings.Default.ActionCtrlAltWheelDown = WheelDownAction(comboControlAltWheel);
            Properties.Settings.Default.ActionShiftAltWheelUp = WheelUpAction(comboShiftAltWheel);
            Properties.Settings.Default.ActionShiftAltWheelDown = WheelDownAction(comboShiftAltWheel);
            Properties.Settings.Default.RevealTeleports = cbRevealTeleports.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.HideUnvisitedDottedLines = cbHideUnvisitedDottedLines.Checked && cbHideUnvisitedSquares.Checked;
            Properties.Settings.Default.ShowMapLabels = cbShowMapLabels.Checked;
            Properties.Settings.Default.DebugShowDirtySquares = cbDebugShowDirtySquares.Checked;
            Properties.Settings.Default.BitmapCacheCrop = (int) nudBitmapCache.Value * 1000000;
            Properties.Settings.Default.ShowShopInventories = cbShowShopInventories.Checked;
            Properties.Settings.Default.WarnIfMissingCustomBackground = cbWarnIfMissingCustomBackground.Checked;
            Properties.Settings.Default.PollQuests = (int)nudPollQuests.Value;
            Properties.Settings.Default.PollEncounters = (int)nudPollEncounters.Value;
            m_notifications.Enabled = cbEnableNotifications.Checked;
            Properties.Settings.Default.Notifications = m_notifications;
            Properties.Settings.Default.NotificationDelay = (int)nudNotificationDelay.Value;
            Properties.Settings.Default.RevealSeenSquares = cbRevealSeenSquares.Checked;
            Properties.Settings.Default.SeenSquareOpacity = (int)nudSeenSquareOpacity.Value;
            Properties.Settings.Default.SkipIntroductions = cbSkipIntroductions.Checked;
            Properties.Settings.Default.LinkedMinimizeAndRestore = cbLinkMinimizeAndRestore.Checked;
            Properties.Settings.Default.SkipIntroTimingTweak = (int) nudSkipIntroTimingTweak.Value;
            Properties.Settings.Default.HideUnidentifiedItems = cbHideUnidentifiedItems.Checked;
            Properties.Settings.Default.DoubleClickSpellSendsKeys = cbDoubleClickSpellSendsKeys.Checked;
            Properties.Settings.Default.IndicateNewInventoryItems = cbIndicateNewInventoryItems.Checked;
            Properties.Settings.Default.FavoriteSpells.ShowFavorites = cbShowFavoriteSpells.Checked;
            Properties.Settings.Default.ShowItemIcons = cbShowItemIcons.Checked;
            Properties.Settings.Default.HideSpoilers = cbHideSpoilers.Checked;
            Properties.Settings.Default.SpoilerColor = pbSpoilerColor.BackColor;

            Global.SquareStyles = m_styles;

            switch (comboInventoryOrientation.SelectedIndex)
            {
                case 1:
                    Properties.Settings.Default.InventoryOrientation = Orient.Horiz;
                    break;
                case 2:
                    Properties.Settings.Default.InventoryOrientation = Orient.Vert;
                    break;
                default:
                    Properties.Settings.Default.InventoryOrientation = Orient.None;
                    break;
            }

            SaveSpellHotkeys();

            if (comboAutoloadFile.Text != Global.InternalMapString)
                m_bLoadNewMap = false;

            Properties.Settings.Default.AutoLoadFiles.Set(SelectedGame, comboAutoloadFile.Text);

            Properties.Settings.Default.AutoLoadFiles = GameStrings.Clone(m_autoLoadFiles);

            switch (comboDefaultMode.SelectedIndex)
            {
                case 0: Properties.Settings.Default.DefaultMode = BlockMode.Play; break;
                case 1: Properties.Settings.Default.DefaultMode = BlockMode.Block; break;
                case 2: Properties.Settings.Default.DefaultMode = BlockMode.Hybrid; break;
                case 3: Properties.Settings.Default.DefaultMode = BlockMode.Line; break;
                case 4: Properties.Settings.Default.DefaultMode = BlockMode.Notes; break;
                case 5: Properties.Settings.Default.DefaultMode = BlockMode.Keyboard; break;
                case 6: Properties.Settings.Default.DefaultMode = BlockMode.Edit; break;
                case 7: Properties.Settings.Default.DefaultMode = BlockMode.Fill; break;
                default: Properties.Settings.Default.DefaultMode = BlockMode.None; break;
            }

            Properties.Settings.Default.Windows = Global.Windows;
            Properties.Settings.Default.Save();

            return true;
        }

        private void SaveGameTitles(ListView lv)
        {
            foreach(ListViewItem lvi in lv.Items)
            {
                if (!(lvi.Tag is GameNames))
                    continue;
                GameNames game = (GameNames)lvi.Tag;
                Properties.Settings.Default.GameTitles.Set(game, lvi.Text);
                // Special case for DOSBox
                if (game == GameNames.DOSBox)
                    Properties.Settings.Default.DOSBoxCaption = lvi.Text;
            }
        }

        public bool LoadNewMap { get { return m_bLoadNewMap; } }
        public bool SetKeys { get { return m_bSetKeys; } }

        private SpellHotkeyList GetSpellKeys(GameNames game)
        {
            SpellHotkeyList list = new SpellHotkeyList();
            list.SelectedGame = game;

            list.Hotkeys.Add(0, new SpellHotkey(0, comboSpellKey1Type, comboSpellKey1Spell));
            list.Hotkeys.Add(1, new SpellHotkey(1, comboSpellKey2Type, comboSpellKey2Spell));
            list.Hotkeys.Add(2, new SpellHotkey(2, comboSpellKey3Type, comboSpellKey3Spell));
            list.Hotkeys.Add(3, new SpellHotkey(3, comboSpellKey4Type, comboSpellKey4Spell));
            list.Hotkeys.Add(4, new SpellHotkey(4, comboSpellKey5Type, comboSpellKey5Spell));
            list.Hotkeys.Add(5, new SpellHotkey(5, comboSpellKey6Type, comboSpellKey6Spell));
            list.Hotkeys.Add(6, new SpellHotkey(6, comboSpellKey7Type, comboSpellKey7Spell));
            list.Hotkeys.Add(7, new SpellHotkey(7, comboSpellKey8Type, comboSpellKey8Spell));
            list.Hotkeys.Add(8, new SpellHotkey(8, comboSpellKey9Type, comboSpellKey9Spell));
            list.Hotkeys.Add(9, new SpellHotkey(9, comboSpellKey10Type, comboSpellKey10Spell));

            return list;
        }

        private void SetSpellKeys(SpellHotkeyList list)
        {
            if (list == null)
                return;
            SetSpellKey(list, 0, comboSpellKey1Type, comboSpellKey1Spell);
            SetSpellKey(list, 1, comboSpellKey2Type, comboSpellKey2Spell);
            SetSpellKey(list, 2, comboSpellKey3Type, comboSpellKey3Spell);
            SetSpellKey(list, 3, comboSpellKey4Type, comboSpellKey4Spell);
            SetSpellKey(list, 4, comboSpellKey5Type, comboSpellKey5Spell);
            SetSpellKey(list, 5, comboSpellKey6Type, comboSpellKey6Spell);
            SetSpellKey(list, 6, comboSpellKey7Type, comboSpellKey7Spell);
            SetSpellKey(list, 7, comboSpellKey8Type, comboSpellKey8Spell);
            SetSpellKey(list, 8, comboSpellKey9Type, comboSpellKey9Spell);
            SetSpellKey(list, 9, comboSpellKey10Type, comboSpellKey10Spell);
        }

        private void SetSpellKey(SpellHotkeyList list, int index, ComboBox comboCharacter, ComboBox comboSpell)
        {
            if (!list.Hotkeys.ContainsKey(index))
                return;

            SpellHotkey hk = list.Hotkeys[index];

            for(int i = 0; i < comboCharacter.Items.Count; i++)
            {
                HKCharTag tagChar = comboCharacter.Items[i] as HKCharTag;
                if (tagChar != null && tagChar.HKChar == hk.Character)
                {
                    comboCharacter.SelectedIndex = i;
                    break;
                }
            }
            if (comboCharacter.SelectedIndex == -1)
                comboCharacter.SelectedIndex = 0;

            for (int i = 0; i < comboSpell.Items.Count; i++)
            {
                HKSpellTag tagSpell = comboSpell.Items[i] as HKSpellTag;
                if (tagSpell != null && tagSpell.HKSpell.BasicIndex == hk.SpellIndex)
                {
                    comboSpell.SelectedIndex = i;
                    break;
                }
            }
        }

        private Shortcuts GetShortcuts()
        {
            Shortcuts shortcuts = new Shortcuts();
            foreach (ListViewItem lvi in lvInput.Items)
            {
                InputOption input = (InputOption)lvi.Tag;
                input.Global = lvi.Checked;
                foreach (ShortcutKeys keys in input.Input)
                {
                    if (keys != null && keys.Length > 0)
                        shortcuts.ShortcutDict.Add(keys, input);
                }
            }
            return shortcuts;
        }

        private void InitMouseActions(params ComboBox[] combos)
        {
            foreach (ComboBox cb in combos)
            {
                cb.Items.Clear();
                cb.Items.Add(new MouseActionItem(Action.None));
                cb.Items.Add(new MouseActionItem(Action.Draw));
                cb.Items.Add(new MouseActionItem(Action.ScrollSheet));
                cb.Items.Add(new MouseActionItem(Action.DrawScroll));
                cb.Items.Add(new MouseActionItem(Action.MoveLabels));
                cb.Items.Add(new MouseActionItem(Action.DrawBlocks));
                cb.Items.Add(new MouseActionItem(Action.DrawLines));
                cb.Items.Add(new MouseActionItem(Action.DrawHybrid));
                cb.Items.Add(new MouseActionItem(Action.DrawFill));
            }
        }

        private void UpdateUI()
        {
            foreach(ComboBox combo in new ComboBox[] { comboWheel, comboShiftWheel, comboControlWheel, comboAltWheel, comboShiftAltWheel, comboControlAltWheel, comboControlShiftWheel })
                InitWheelCombo(combo);
            InitMouseActions(comboMouseLeft, comboMouseRight, comboMouseMiddle, comboMouseX1, comboMouseX2);

            m_shkc = Properties.Settings.Default.SpellHotkeys;
            if (m_shkc == null)
                m_shkc = new SpellHotkeyCollection();

            m_autoLoadFiles = GameStrings.Clone(Properties.Settings.Default.AutoLoadFiles);

            if (comboCurrentGame.Items.Count < Games.ImplementedGames.Length)
            {
                comboCurrentGame.Items.Clear();
                foreach (GameNames game in Games.ImplementedGames)
                    comboCurrentGame.Items.Add(new CurrentGameName(game));
                comboCurrentGame.SelectedIndex = 0;
            }

            nudDefaultZoom.Value = Properties.Settings.Default.DefaultZoom;
            nudDefaultMapWidth.Value = Properties.Settings.Default.DefaultMapSize.Width;
            nudDefaultMapHeight.Value = Properties.Settings.Default.DefaultMapSize.Height;
            SetGameIndex(comboCurrentGame, Properties.Settings.Default.Game);
            cbAutoSwitchSheets.Checked = Properties.Settings.Default.AutoSwitchSheets;
            nudYouAreHereOpacity.Value = Properties.Settings.Default.YouAreHereOpacity;
            cbEnableMemoryWrite.Checked = Properties.Settings.Default.EnableMemoryWrite;
            cbHideUnvisitedSquares.Checked = Properties.Settings.Default.HideUnvisitedSquares;
            cbAlwaysRevealEdges.Checked = Properties.Settings.Default.AlwaysRevealEdges;
            cbShowUnvisitedNotes.Checked = Properties.Settings.Default.ShowUnvisitedNotes;
            cbRevealAdjacentInaccessible.Checked = Properties.Settings.Default.RevealAdjacentInaccessible;
            cbUseInGameCartography.Checked = Properties.Settings.Default.UseInGameCartography;
            cbReadOnlyMaps.Checked = Properties.Settings.Default.ReadOnlyMaps;
            cbReadOnlyNotes.Checked = Properties.Settings.Default.ReadOnlyNotes;
            cbFollowPartyChar.Checked = Properties.Settings.Default.FollowCharWindows;
            comboAutoloadFile.Text = m_autoLoadFiles.Get(SelectedGame, Global.InternalMapString);
            cbShowPartyByDefault.Checked = Global.Windows.AutoShow(WindowType.Party);
            cbShowSpellsWhenCasting.Checked = Properties.Settings.Default.ShowSpells;
            cbEnableKeyboardHook.Checked = Properties.Settings.Default.EnableGlobalShortcuts;
            cbSaveDOSBox.Checked = Properties.Settings.Default.SaveDOSBoxPosition;
            cbShowEncounters.Checked = Properties.Settings.Default.ShowEncounters;
            cbShowTreasureWindow.Checked = Properties.Settings.Default.ShowTreasureWindow;
            cbSnapToDOSBox.Checked = Properties.Settings.Default.SnapWindowsToDOSBox;
            cbAutoLaunchGame.Checked = Properties.Settings.Default.AutoLaunchGame;
            nudOpacityTreasure.Value = Properties.Settings.Default.TreasureOpacity;
            cbEnableCheats.Checked = Properties.Settings.Default.EnableCheats;
            nudPollingSpeed.Value = Properties.Settings.Default.PollTime;
            nudUnvisitedSquareOpacity.Value = Properties.Settings.Default.UnvisitedSquareOpacity;
            cbAutoShowNoteAtLocation.Checked = Properties.Settings.Default.AutoShowNoteAtLocation;
            cbAutoShowInfo.Checked = Global.Windows.AutoShow(WindowType.GameInfo);
            nudMonsterOpacity.Value = Properties.Settings.Default.MonsterOpacity;
            cbShowMonstersOnMaps.Checked = Properties.Settings.Default.ShowMonstersOnMaps;
            cbSingleTaskbarWindow.Checked = Properties.Settings.Default.SingleTaskbarWindow;
            cbPreventOffscreenWindows.Checked = Properties.Settings.Default.PreventOffscreenWindows;
            cbBringDOSBoxToFront.Checked = Properties.Settings.Default.FocusDOSBoxWhenSelected;
            tbAutoSave.Text = Properties.Settings.Default.AutoSaveFile;
            cbAutoSave.Checked = Properties.Settings.Default.AutoSave;
            Global.SetNud(nudAutosaveMinutes, Math.Max(Properties.Settings.Default.AutosaveSeconds / 60, 1));
            cbShowActiveSquares.Checked = Properties.Settings.Default.ShowActiveSquares;
            cbShowActiveEncountersOnly.Checked = Properties.Settings.Default.ShowActiveEncountersOnly;
            cbForceDOSBoxLocation.Checked = Properties.Settings.Default.ForceDosBoxLocation;
            cbForceDOSBoxSize.Checked = Properties.Settings.Default.ForceDosBoxSize;
            nudDOSBoxLocationX.Value = Properties.Settings.Default.DosBoxWindowRect.X;
            nudDOSBoxLocationY.Value = Properties.Settings.Default.DosBoxWindowRect.Y;
            nudDOSBoxWidth.Value = Properties.Settings.Default.DosBoxWindowRect.Width;
            nudDOSBoxHeight.Value = Properties.Settings.Default.DosBoxWindowRect.Height;
            cbShowDeadMonsters.Checked = Properties.Settings.Default.ShowDeadMonsters;
            cbCureAllHPWithConditions.Checked = Properties.Settings.Default.CureAllHPWithConditions;
            comboCopyLocationFormat.Text = Properties.Settings.Default.CopyLocationFormat;
            tbScriptsDumpFile.Text = Properties.Settings.Default.ScriptsDumpFile;
            cbQuestPrefixGiver.Checked = Properties.Settings.Default.QuestShowGiver;
            cbQuestShowRewards.Checked = Properties.Settings.Default.QuestShowReward;
            cbQuestBoldNearbyGoals.Checked = Properties.Settings.Default.QuestBoldNearby;
            cbOfferSwitch.Checked = Properties.Settings.Default.OfferToSwitchBooks;
            lvGameTitles.Items.Clear();
            foreach(GameNames game in Games.ImplementedGames)
                AddGameTitle(game, Games.CurrentTitle(game));
            cbQuickScanDOSBox.Checked = Properties.Settings.Default.QuickScanDOSBox;
            cbHideInvalidQuestGoals.Checked = Properties.Settings.Default.HideInvalidQuestGoals;
            cbWarnOnMultipleReacquire.Checked = Properties.Settings.Default.WarnMultipleReacquire;
            cbShowOnlyDetectableMonsters.Checked = Properties.Settings.Default.ShowOnlyDetectableMonsters;
            cbShowCPU.Checked = Properties.Settings.Default.ShowCPU;
            cbShowActivatedMonstersIcon.Checked = Properties.Settings.Default.ShowActivatedMonstersIcon;
            cbShowListMonstersUnexplored.Checked = Properties.Settings.Default.ShowListMonstersUnexplored;
            cbUpdateCartWhenInaccessibleRevealed.Checked = Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed;
            cbHideScriptMonsters.Checked = Properties.Settings.Default.HideScriptMonsters;
            cbRefreshDOSBox.Checked = Properties.Settings.Default.RefreshDOSBox;
            Global.SetNud(nudLaunchDelay, Properties.Settings.Default.GameStartDelay);
            Global.SetNud(nudWatchdog, Properties.Settings.Default.WatchdogTimer);
            m_styles = Properties.Settings.Default.SquareStyles;
            SetMouseAction(comboMouseLeft, Properties.Settings.Default.ActionLeftMouse);
            SetMouseAction(comboMouseRight, Properties.Settings.Default.ActionRightMouse);
            SetMouseAction(comboMouseMiddle, Properties.Settings.Default.ActionMiddleMouse);
            SetMouseAction(comboMouseX1, Properties.Settings.Default.ActionX1Mouse);
            SetMouseAction(comboMouseX2, Properties.Settings.Default.ActionX2Mouse);
            cbCenterPartyInMap.Checked = Properties.Settings.Default.CenterPartyInMap;
            cbHideScrollbarsInPlayMode.Checked = Properties.Settings.Default.HideScrollbarsInPlayMode;
            Global.SetNud(nudShowGridLinesAboveZoom, Properties.Settings.Default.ShowGridLinesAboveZoom);
            cbShowGridLines.Checked = Properties.Settings.Default.ShowGridLines;
            cbEditCopyBackgrounds.Checked = Properties.Settings.Default.EditCopyBackgrounds;
            cbEditCopyInnerLines.Checked = Properties.Settings.Default.EditCopyInnerLines;
            cbEditCopyOuterLines.Checked = Properties.Settings.Default.EditCopyOuterLines;
            cbEditCopyIcons.Checked = Properties.Settings.Default.EditCopyIcons;
            cbEditCopyNotes.Checked = Properties.Settings.Default.EditCopyNotes;
            Global.SetNud(nudMaxUndoActions, Properties.Settings.Default.MaxUndoActions);
            cbAutoSwitchToPlayMode.Checked = Properties.Settings.Default.AutoSwitchToPlayMode;
            SetWheelCombo(comboWheel, GetWheelAction(Properties.Settings.Default.ActionWheelUp));
            SetWheelCombo(comboShiftWheel, GetWheelAction(Properties.Settings.Default.ActionShiftWheelUp));
            SetWheelCombo(comboControlWheel, GetWheelAction(Properties.Settings.Default.ActionCtrlWheelUp));
            SetWheelCombo(comboAltWheel, GetWheelAction(Properties.Settings.Default.ActionAltWheelUp));
            SetWheelCombo(comboControlShiftWheel, GetWheelAction(Properties.Settings.Default.ActionCtrlShiftWheelUp));
            SetWheelCombo(comboControlAltWheel, GetWheelAction(Properties.Settings.Default.ActionCtrlAltWheelUp));
            SetWheelCombo(comboShiftAltWheel, GetWheelAction(Properties.Settings.Default.ActionShiftAltWheelUp));
            cbRevealTeleports.Checked = Properties.Settings.Default.RevealTeleports;
            cbHideUnvisitedDottedLines.Checked = Properties.Settings.Default.HideUnvisitedDottedLines;
            cbShowMapLabels.Checked = Properties.Settings.Default.ShowMapLabels;
            cbDebugShowDirtySquares.Checked = Properties.Settings.Default.DebugShowDirtySquares;
            Global.SetNud(nudBitmapCache, Properties.Settings.Default.BitmapCacheCrop / 1000000);
            cbShowShopInventories.Checked = Properties.Settings.Default.ShowShopInventories;
            cbWarnIfMissingCustomBackground.Checked = Properties.Settings.Default.WarnIfMissingCustomBackground;
            Global.SetNud(nudPollQuests, Properties.Settings.Default.PollQuests);
            Global.SetNud(nudPollEncounters, Properties.Settings.Default.PollEncounters);
            m_notifications = Properties.Settings.Default.Notifications.Clone();
            cbEnableNotifications.Checked = m_notifications.Enabled;
            Global.SetNud(nudNotificationDelay, Properties.Settings.Default.NotificationDelay);
            cbRevealSeenSquares.Checked = Properties.Settings.Default.RevealSeenSquares;
            Global.SetNud(nudSeenSquareOpacity, Properties.Settings.Default.SeenSquareOpacity);
            cbSkipIntroductions.Checked = Properties.Settings.Default.SkipIntroductions;
            cbLinkMinimizeAndRestore.Checked = Properties.Settings.Default.LinkedMinimizeAndRestore;
            Global.SetNud(nudSkipIntroTimingTweak, Properties.Settings.Default.SkipIntroTimingTweak);
            cbHideUnidentifiedItems.Checked = Properties.Settings.Default.HideUnidentifiedItems;
            cbDoubleClickSpellSendsKeys.Checked = Properties.Settings.Default.DoubleClickSpellSendsKeys;
            cbIndicateNewInventoryItems.Checked = Properties.Settings.Default.IndicateNewInventoryItems;
            if (Properties.Settings.Default.FavoriteSpells == null)
                Properties.Settings.Default.FavoriteSpells = new FavoriteSpells();
            cbShowFavoriteSpells.Checked = Properties.Settings.Default.FavoriteSpells.ShowFavorites;
            cbShowItemIcons.Checked = Properties.Settings.Default.ShowItemIcons;
            cbHideSpoilers.Checked = Properties.Settings.Default.HideSpoilers;
            pbSpoilerColor.BackColor = Properties.Settings.Default.SpoilerColor;


            SetBoxColors(m_styles);

            if (!Global.Debug && tcOptions.TabPages.Contains(tpDebug))
                tcOptions.TabPages.Remove(tpDebug);

            switch(Properties.Settings.Default.InventoryOrientation)
            {
                case Orient.Horiz:
                    comboInventoryOrientation.SelectedIndex = 1;
                    break;
                case Orient.Vert:
                    comboInventoryOrientation.SelectedIndex = 2;
                    break;
                default:
                    comboInventoryOrientation.SelectedIndex = 0;
                    break;
            }

            switch (Properties.Settings.Default.DefaultMode)
            {
                case BlockMode.Play: comboDefaultMode.SelectedIndex = 0; break;
                case BlockMode.Block: comboDefaultMode.SelectedIndex = 1; break;
                case BlockMode.Hybrid: comboDefaultMode.SelectedIndex = 2; break;
                case BlockMode.Line: comboDefaultMode.SelectedIndex = 3; break;
                case BlockMode.Notes: comboDefaultMode.SelectedIndex = 4; break;
                case BlockMode.Keyboard: comboDefaultMode.SelectedIndex = 5; break;
                case BlockMode.Edit: comboDefaultMode.SelectedIndex = 6; break;
                case BlockMode.Fill: comboDefaultMode.SelectedIndex = 7; break;
                default: comboDefaultMode.SelectedIndex = 0; break;
            }

            comboAutoloadFile.Items.Clear();
            comboAutoloadFile.Items.Add(Global.InternalMapString);

            UpdateShortcutsView(Properties.Settings.Default.Shortcuts);

            SHKSelectedGame = ((CurrentGameName)comboCurrentGame.SelectedItem).Game;
        }

        private void SetMouseAction(ComboBox cb, Action action)
        {
            foreach (MouseActionItem item in cb.Items)
            {
                if (item.Action == action)
                {
                    cb.SelectedItem = item;
                    return;
                }
            }
            cb.SelectedIndex = -1;
        }

        private Action GetMouseAction(ComboBox cb)
        {
            if (cb.SelectedIndex == -1)
                return Action.None;
            return ((MouseActionItem) cb.SelectedItem).Action;
        }

        private void UpdateShortcutsView(Shortcuts sc)
        {
            lvInput.BeginUpdate();
            lvInput.Items.Clear();
            foreach (InputOption input in sc.Commands)
            {
                switch (input.Action)
                {
                    case Action.Draw:
                    case Action.DrawScroll:
                    case Action.ScrollSheet:
                    case Action.MoveLabels:
                    case Action.DrawBlocks:
                    case Action.DrawLines:
                    case Action.DrawHybrid:
                    case Action.DrawFill:
                    case Action.DrawEdit:
                        // These actions are only for mouse buttons
                        break;
                    default:
                        ListViewItem item = new ListViewItem(Global.ActionString(input.Action));
                        item.Checked = input.Global;
                        foreach (ShortcutKeys keys in input.Input)
                            if (keys != null)
                                item.SubItems.Add(keys.KeyString);
                        item.Tag = input;
                        lvInput.Items.Add(item);
                        break;
                }
            }
            Global.SizeHeadersAndContent(lvInput);
            lvInput.EndUpdate();
        }

        private void AddGameTitle(GameNames game, string strTitle)
        {
            ListViewItem lvi = new ListViewItem(strTitle);
            lvi.SubItems.Add(Games.Name(game));
            lvi.Tag = game;
            lvGameTitles.Items.Add(lvi);
        }

        private void UpdateItem(ListViewItem lvi)
        {
            InputOption input = (InputOption)lvi.Tag;
            int iColumn = 1;
            foreach(ShortcutKeys keys in input.Input)
            {
                if (iColumn >= lvi.SubItems.Count || keys == null)
                    break;
                lvi.SubItems[iColumn].Text = keys.KeyString;
                lvi.Checked = input.Global;
                iColumn++;
            }
        }

        private void ExpandColumn(int iColumn, bool bSuspendUpdate)
        {
            if (bSuspendUpdate)
                lvInput.BeginUpdate();
            int iWidth = lvInput.Columns[iColumn].Width;
            lvInput.Columns[iColumn].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            if (lvInput.Columns[iColumn].Width < iWidth)
                lvInput.Columns[iColumn].Width = iWidth;
            iWidth = lvInput.Columns[iColumn].Width;
            //lvInput.Columns[iColumn].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            //if (lvInput.Columns[iColumn].Width < iWidth)
            //    lvInput.Columns[iColumn].Width = iWidth;
            if (bSuspendUpdate)
                lvInput.EndUpdate();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            m_findBox = new FindBox(scKeyboard, tbFindKeyboard, FindBox.ListViewFindFunction, lvInput, tcOptions, tpKeyboard);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;
            scKeyboard.Panel2Collapsed = true;
            if (StartPage < tcOptions.TabPages.Count)
                tcOptions.SelectedIndex = StartPage;
            else
                tcOptions.SelectedTab = tpPlay;
            DrawIcon(Properties.Resources.iconYouAreHere, pbYouAreHere, (int)nudYouAreHereOpacity.Value);
            DrawIcon(Properties.Resources.iconYouAreHereDirectionless, pbYouAreHereDirectionless, (int)nudYouAreHereOpacity.Value);
            DrawMonsters();
            if (m_lastSpellHotkeyGame == GameNames.None)
                m_lastSpellHotkeyGame = SelectedGame;
            InitSpellPage();
            UpdateEnabledState();
        }

        private void lvInput_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvInput.ListViewItemSorter = new InputOptionItemComparer(lvInput, e.Column, m_bAscendingSort);
            lvInput.Sort();
        }

        public bool ResetAllVisited
        {
            get { return m_bResetAllVisited; }
        }

        public bool ResetWindows
        {
            get { return m_bResetWindows; }
        }

        private void FinishEditKeys()
        {
            KeyboardHook.Stop();
            UpdateKey(m_lviEditing, m_iShortcutEditing);
            m_lviEditing = null;
        }

        private void CancelKey()
        {
            KeyboardHook.Stop();
            if (m_lviEditing != null)
            {
                m_keysShortcut = new List<Keys>(m_keysOriginal.Keys);
                UpdateKey(m_lviEditing, m_iShortcutEditing);
            }
            m_lviEditing = null;
        }

        private void UpdateKey(ListViewItem lvi, int iShortcut)
        {
            InputOption input = (InputOption)lvi.Tag;
            string strOrig = lvi.SubItems[iShortcut + 1].Text;
            input.Input[iShortcut] = new ShortcutKeys(m_keysShortcut.ToArray());
            input.Global = lvi.Checked;
            string strNew = input.Input[iShortcut].KeyString;
            if (strNew != strOrig)
            {
                lvi.SubItems[iShortcut + 1].Text = strNew;
                ExpandColumn(iShortcut+1, true);
            }
            m_keyConflicts = null;  // Created as necessary if user presses F8
        }

        private void lvInput_MouseUp(object sender, MouseEventArgs e)
        {
            m_bSetKeys = true;

            if (m_lviEditing != null)
                FinishEditKeys();
            else
                CancelKey();

            m_ptContextOpened = lvInput.PointToClient(Cursor.Position);
            ListViewItem lvi;
            int iColumn = Global.SubItemAtPoint(lvInput, m_ptContextOpened, out lvi);

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cmKeyboard.Show(Cursor.Position);
                return;
            }

            BeginEdit(lvi, iColumn);
        }

        private void BeginEdit(ListViewItem lvi, int iColumn)
        {
            if (lvi == null)
                return;
            if (iColumn < 1 || iColumn > 2)
                return;

            InputOption input = (InputOption)lvi.Tag;
            if (input.Input == null || input.Input.Length == 0)
                input.Input = new ShortcutKeys[2];
            if (input.Input.Length == 1)
                input.Input = new ShortcutKeys[2] { input.Input[0], null };

            m_iShortcutEditing = iColumn - 1;
            m_keysOriginal = input.Input[m_iShortcutEditing];
            lvInput.SelectedItems.Clear();
            lvi.Selected = true;
            while (lvi.SubItems.Count < 3)
                lvi.SubItems.Add(String.Empty);
            lvi.SubItems[m_iShortcutEditing+1].Text = "<press keys>";
            ExpandColumn(m_iShortcutEditing + 1, true);
            m_lviEditing = lvi;
            for (int i = 0; i < 256; i++)
                m_keysCurrent[i] = false;
            m_keysShortcut.Clear();
            m_bNoMoreShortcutKeys = false;
            KeyboardHook.Start(this, Global.AllKeys);
        }

        public bool OnLLKeyUp(Keys key, bool[] keysDown)
        {
            //Global.Log("KeyUp: {0}", Global.KeyNames[(int)key]);
            m_keysCurrent[(int)key] = false;
            bool bAnyDown = false;
            foreach (bool b in m_keysCurrent)
                bAnyDown = bAnyDown || b;

            if (!bAnyDown && m_bWaitForKeysUp)
            {
                m_bWaitForKeysUp = false;
                return false;
            }

            if (!bAnyDown)
                FinishEditKeys();
            else
            {
                UpdateKey(m_lviEditing, m_iShortcutEditing);
                m_bNoMoreShortcutKeys = true;    // Don't allow adding any more keys once one has been released
            }

            return true;
        }

        public bool OnLLKeyDown(Keys key, bool[] keysDown)
        {
            if (m_bNoMoreShortcutKeys)
                return false;

            if (key == Keys.Escape)
            {
                CancelKey();
                return true;
            }

            m_keysCurrent[(int)key] = true;
            if (!m_keysShortcut.Contains(key))
                m_keysShortcut.Add(key);
            UpdateKey(m_lviEditing, m_iShortcutEditing);
            return true;
        }

        private void tcOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lviEditing != null)
                FinishEditKeys();
            else
                CancelKey();
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelKey();
        }

        private void lvInput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (lvInput.FocusedItem != null)
                    {
                        m_bSkipOK = true;
                        m_bWaitForKeysUp = true;
                        BeginEdit(lvInput.FocusedItem, 1);
                    }
                    break;
                case Keys.Delete:
                    if (lvInput.FocusedItem != null)
                    {
                        ((InputOption)lvInput.FocusedItem.Tag).ClearKey(m_iShortcutEditing);
                        UpdateItem(lvInput.FocusedItem);
                    }
                    break;
                case Keys.Insert:
                    if (lvInput.SelectedItems.Count > 0)
                        EditNotification();
                    break;
                case Keys.F8:
                    SelectNextKeyConflict();
                    break;
            }
        }

        private void SelectNextKeyConflict()
        {
            if (lvInput.Items.Count < 1)
                return;
            Dictionary<int, ShortcutKeys> conflicts = GetAllKeyConflicts();
            if (conflicts.Count < 1)
                return;

            int iStart = lvInput.SelectedItems.Count > 0 ? lvInput.SelectedItems[0].Index + 1 : 0;
            if (iStart >= lvInput.Items.Count)
                iStart = 0;
            bool bWrapped = false;
            while (iStart < lvInput.Items.Count)
            {
                if (conflicts.ContainsKey(iStart))
                {
                    lvInput.SelectedItems.Clear();
                    lvInput.Items[iStart].Selected = true;
                    lvInput.EnsureVisible(iStart);
                    return;
                }
                iStart++;
                if (iStart >= lvInput.Items.Count)
                {
                    if (bWrapped)
                        break;
                    iStart = 0;
                    bWrapped = true;
                }
            }
        }

        private Dictionary<int, ShortcutKeys> m_keyConflicts = null;

        private Dictionary<int, ShortcutKeys> GetAllKeyConflicts()
        {
            if (m_keyConflicts != null)
                return m_keyConflicts;

            Dictionary<ShortcutKeys, int> allKeys = new Dictionary<ShortcutKeys, int>();
            m_keyConflicts = new Dictionary<int, ShortcutKeys>();
            foreach (ListViewItem lvi in lvInput.Items)
            {
                InputOption input = (InputOption)lvi.Tag;
                foreach (ShortcutKeys keys in input.Input)
                {
                    if (keys == null || keys.Length == 0)
                        continue;   // "no shortcut" is not a conflict
                    if (!allKeys.ContainsKey(keys))
                        allKeys.Add(keys, lvi.Index);
                    else
                    {
                        if (!m_keyConflicts.ContainsKey(allKeys[keys]))
                            m_keyConflicts.Add(allKeys[keys], keys);
                        m_keyConflicts.Add(lvi.Index, keys);
                        break;
                    }
                }
            }
            return m_keyConflicts;
        }

        private void cmKeyboard_Opening(object sender, CancelEventArgs e)
        {
            ListViewItem lvi;
            int iColumn = Global.SubItemAtPoint(lvInput, m_ptContextOpened, out lvi);
            cmKeyboardClear.Enabled = (iColumn > 0 && iColumn < lvInput.Columns.Count);
        }

        private void cmKeyboardClear_Click(object sender, EventArgs e)
        {
            if (lvInput.SelectedItems.Count > 1)
            {
                lvInput.BeginUpdate();
                foreach (ListViewItem lviClear in lvInput.SelectedItems)
                {
                    ((InputOption)lviClear.Tag).ClearKeys();
                    UpdateItem(lviClear);
                }
                lvInput.EndUpdate();
                return;
            }

            ListViewItem lvi;
            int iColumn = Global.SubItemAtPoint(lvInput, m_ptContextOpened, out lvi);
            ((InputOption)lvi.Tag).ClearKeys(iColumn-1);
            UpdateItem(lvi);
        }

        private void nudYouAreHereOpacity_ValueChanged(object sender, EventArgs e)
        {
            DrawIcon(Properties.Resources.iconYouAreHere, pbYouAreHere, (int)nudYouAreHereOpacity.Value);
            DrawIcon(Properties.Resources.iconYouAreHereDirectionless, pbYouAreHereDirectionless, (int)nudYouAreHereOpacity.Value);
        }

        private void btnResetVisited_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset the \"visited\" status for ALL squares in this map book and cannot be undone.  Are you sure you wish to do this?",
                "Reset all?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                m_bResetAllVisited = true;
        }

        private void cbHideUnvisited_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void btnResetWindows_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset the saved window sizes and positions?",
                "Reset Positions?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                m_bResetWindows = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = comboAutoloadFile.Text;
            ofd.Filter = "WhereAreWe Files|*.WAW|All Files|*.*";
            if (ofd.FileName == Global.InternalMapString)
                ofd.FileName = String.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                comboAutoloadFile.Text = ofd.FileName;
                m_autoLoadFiles.Set(SelectedGame, ofd.FileName);
            }
        }

        private void btnEditShortcuts_Click(object sender, EventArgs e)
        {
            GameShortcutsEditorForm form = new GameShortcutsEditorForm();
            form.SetPaths(Properties.Settings.Default.AutoLaunchShortcuts);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.AutoLaunchShortcuts = form.GetPaths();
                foreach (GameNames game in Games.ImplementedGames)
                    Games.SetRosterPath(game, String.Empty);
            }
        }

        private void cbReadLocationFromGame_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            cbAlwaysRevealEdges.Enabled = cbHideUnvisitedSquares.Checked;
            cbRevealAdjacentInaccessible.Enabled = cbHideUnvisitedSquares.Checked;
            cbUseInGameCartography.Enabled = cbHideUnvisitedSquares.Checked;
            cbRevealTeleports.Enabled = cbHideUnvisitedSquares.Checked;
            cbHideUnvisitedDottedLines.Enabled = cbHideUnvisitedSquares.Checked;
            cbShowUnvisitedNotes.Enabled = cbHideUnvisitedSquares.Checked;
            cbShowTreasureWindow.Enabled = cbShowEncounters.Checked;
            cbEnableCheats.Enabled = cbEnableMemoryWrite.Checked;
            cbCureAllHPWithConditions.Enabled = cbEnableMemoryWrite.Checked;
            cbUpdateCartWhenInaccessibleRevealed.Enabled = cbEnableMemoryWrite.Checked;
            cbShowActivatedMonstersIcon.Enabled = cbShowMonstersOnMaps.Checked;
            cbRevealSeenSquares.Enabled = cbHideUnvisitedSquares.Checked;
            nudSeenSquareOpacity.Enabled = cbShowGridLines.Checked;
            nudShowGridLinesAboveZoom.Enabled = cbShowGridLines.Checked;
            nudSkipIntroTimingTweak.Enabled = cbSkipIntroductions.Checked;
        }

        private void cbEnableMemoryWrite_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void comboCurrentGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboAutoloadFile.Text = m_autoLoadFiles.Get(SelectedGame);
        }

        private void comboAutoloadFile_Leave(object sender, EventArgs e)
        {
            m_autoLoadFiles.Set(SelectedGame, comboAutoloadFile.Text);
        }

        private void nudMonsterOpacity_ValueChanged(object sender, EventArgs e)
        {
            DrawMonsters();
        }

        private void DrawMonsters()
        {
            DrawIcon(Properties.Resources.iconOneMonster, pbMonsters1, (int)nudMonsterOpacity.Value);
            DrawIcon(Properties.Resources.iconTwoMonsters, pbMonsters2, (int)nudMonsterOpacity.Value);
            DrawIcon(Properties.Resources.iconThreeMonsters, pbMonsters3, (int)nudMonsterOpacity.Value);
            DrawIcon(Properties.Resources.iconOneMonsterHighlighted, pbMonsters4, (int)nudMonsterOpacity.Value);
            DrawIcon(Properties.Resources.iconTwoMonstersHighlighted, pbMonsters5, (int)nudMonsterOpacity.Value);
            DrawIcon(Properties.Resources.iconThreeMonstersHighlighted, pbMonsters6, (int)nudMonsterOpacity.Value);
        }

        private void DrawIcon(Icon icon, PictureBox pb, int opacity)
        {
            Bitmap bmpIcon = new Icon(icon, 32, 32).ToBitmap();
            Bitmap bmpBackground = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmpBackground))
            {
                g.DrawImage(bmpIcon, new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height), 0.0f, 0.0f, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel,
                    Global.GetOpacityAttributes(opacity));
            }
            pb.Image = bmpBackground;
        }

        private SpellHotkeyList GetSelectedHotkeyList(GameNames game)
        {
            if (m_shkc == null || !m_shkc.Hotkeys.ContainsKey(game))
                return new SpellHotkeyList();
            return m_shkc.Hotkeys[game];
        }

        private HKSpellTag[] m_favoriteSpells;

        private void InitSpellPage()
        {
            m_favoriteSpells = new HKSpellTag[10];
            for (int i = 0; i < 10; i++)
                m_favoriteSpells[i] = new HKSpellTag(new FavoriteSpell(i));

            comboSpellGame.Items.Clear();
            foreach (GameNames game in Games.ImplementedGames)
                comboSpellGame.Items.Add(new SpellGameTag(game));

            m_comboChars = new ComboBox[] {
                comboSpellKey1Type, comboSpellKey2Type, comboSpellKey3Type, comboSpellKey4Type, comboSpellKey5Type,
                comboSpellKey6Type, comboSpellKey7Type, comboSpellKey8Type, comboSpellKey9Type, comboSpellKey10Type
            };

            m_comboSpells = new ComboBox[] {
                comboSpellKey1Spell, comboSpellKey2Spell, comboSpellKey3Spell, comboSpellKey4Spell, comboSpellKey5Spell,
                comboSpellKey6Spell, comboSpellKey7Spell, comboSpellKey8Spell, comboSpellKey9Spell, comboSpellKey10Spell
            };

            if (m_lastSpellHotkeyGame != GameNames.None)
            {
                for (int i = 0; i < comboSpellGame.Items.Count; i++)
                {
                    SpellGameTag tag = comboSpellGame.Items[i] as SpellGameTag;
                    if (tag != null && tag.Game == m_lastSpellHotkeyGame)
                    {
                        comboSpellGame.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private Dictionary<GameNames, List<HKSpellTag>> m_hkSpellTags = new Dictionary<GameNames, List<WhereAreWe.HKSpellTag>>();

        private List<HKSpellTag> GetHKSpellTags(GameNames game)
        {
            if (m_hkSpellTags.ContainsKey(game))
                return m_hkSpellTags[game];

            Dictionary<int, Spell> spells = Games.GetSpellList(game);
            List<HKSpellTag> tags = new List<HKSpellTag>(spells.Count);
            foreach (Spell spell in spells.Values)
                tags.Add(new HKSpellTag(spell));
            m_hkSpellTags.Add(game, tags);
            return tags;
        }

        private void UpdateSpellHotkeyCollection()
        {
            if (m_bUpdatingSpellGame)
                return;

            GameNames game = SHKSelectedGame;
            if (m_shkc == null)
                m_shkc = new SpellHotkeyCollection();
            SpellHotkeyList list = GetSpellKeys(game);
            if (!m_shkc.Hotkeys.ContainsKey(game))
                m_shkc.Hotkeys.Add(game, list);
            else
                m_shkc.Hotkeys[game] = list;
        }

        private void comboSpellGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingSpellGame)
                return;

            GameNames game = SHKSelectedGame;

            m_bUpdatingSpellGame = true;

            List<HKCharTag> listChars = new List<HKCharTag>();
            listChars.Add(new HKCharTag(SpellHotkey.HKCharacter.None));
            List<HKSpellTag> listSpells = GetHKSpellTags(game);
            switch (game)
            {
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45:
                    for (SpellHotkey.HKCharacter hk = SpellHotkey.HKCharacter.Character1; hk <= SpellHotkey.HKCharacter.AllCharacters; hk++)
                        listChars.Add(new HKCharTag(hk));
                    break;
                default:
                    listChars.Add(new HKCharTag(SpellHotkey.HKCharacter.CurrentCharacter));
                    break;

            }

            foreach (ComboBox comboCharacter in m_comboChars)
            {
                comboCharacter.BeginUpdate();
                comboCharacter.Items.Clear();
                comboCharacter.Items.AddRange(listChars.ToArray());
                comboCharacter.EndUpdate();
            }

            foreach (ComboBox comboSpell in m_comboSpells)
            {
                comboSpell.BeginUpdate();
                comboSpell.Items.Clear();
                comboSpell.Items.AddRange(m_favoriteSpells);
                comboSpell.Items.AddRange(listSpells.ToArray());
                comboSpell.EndUpdate();
            }

            SetSpellKeys(GetSelectedHotkeyList(game));

            m_bUpdatingSpellGame = false;

            UpdateSpellControls();

            m_lastSpellHotkeyGame = game;
        }

        private GameNames SHKSelectedGame
        {
            get
            {
                SpellGameTag tag = comboSpellGame.SelectedItem as SpellGameTag;
                if (tag == null)
                    return GameNames.None;
                return tag.Game;
            }

            set
            {
                foreach (SpellGameTag tag in comboSpellGame.Items)
                {
                    if (tag.Game == value)
                    {
                        comboSpellGame.SelectedItem = tag;
                        return;
                    }
                }

                comboSpellGame.SelectedIndex = -1;
            }
        }

        private void SaveSpellHotkeys()
        {
            Properties.Settings.Default.SpellHotkeys = m_shkc;
        }

        private void comboSpellTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingSpellGame)
                return;

            UpdateSpellControls();
        }

        private void UpdateSpellControl(Label label, ComboBox comboType, ComboBox comboSpell)
        {
            HKCharTag tagChar = comboType.SelectedItem as HKCharTag;
            if (tagChar == null)
                return;
            label.Visible = tagChar.HKChar != SpellHotkey.HKCharacter.None;
            label.Text = tagChar.HKChar == SpellHotkey.HKCharacter.CurrentCharacter ? ":" : "to";
            comboSpell.Visible = tagChar.HKChar != SpellHotkey.HKCharacter.None;
        }

        private void UpdateSpellControls()
        {
            UpdateSpellControl(labelTo1, comboSpellKey1Type, comboSpellKey1Spell);
            UpdateSpellControl(labelTo2, comboSpellKey2Type, comboSpellKey2Spell);
            UpdateSpellControl(labelTo3, comboSpellKey3Type, comboSpellKey3Spell);
            UpdateSpellControl(labelTo4, comboSpellKey4Type, comboSpellKey4Spell);
            UpdateSpellControl(labelTo5, comboSpellKey5Type, comboSpellKey5Spell);
            UpdateSpellControl(labelTo6, comboSpellKey6Type, comboSpellKey6Spell);
            UpdateSpellControl(labelTo7, comboSpellKey7Type, comboSpellKey7Spell);
            UpdateSpellControl(labelTo8, comboSpellKey8Type, comboSpellKey8Spell);
            UpdateSpellControl(labelTo9, comboSpellKey9Type, comboSpellKey9Spell);
            UpdateSpellControl(labelTo10, comboSpellKey10Type, comboSpellKey10Spell);
            UpdateSpellHotkeyCollection();
        }

        private void cbFocusAllWhenAnySelected_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void cbAutosave_CheckedChanged(object sender, EventArgs e)
        {
            tbAutoSave.Enabled = cbAutoSave.Checked;
            btnBrowseAutosave.Enabled = cbAutoSave.Checked;
            nudAutosaveMinutes.Enabled = cbAutoSave.Checked;
        }

        private void btnBrowseAutosave_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = tbAutoSave.Text;
            if (ofd.ShowDialog() == DialogResult.OK)
                tbAutoSave.Text = ofd.FileName;
        }

        private void cbIndicateActiveSquares_CheckedChanged(object sender, EventArgs e)
        {
            cbShowActiveEncountersOnly.Enabled = cbShowActiveSquares.Checked;
        }

        private void llClearHotkeys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdatingSpellGame = true;

            foreach (ComboBox comboCharacter in new ComboBox[] {
                comboSpellKey1Type, comboSpellKey2Type, comboSpellKey3Type, comboSpellKey4Type, comboSpellKey5Type,
                comboSpellKey6Type, comboSpellKey7Type, comboSpellKey8Type, comboSpellKey9Type, comboSpellKey10Type
            })
                comboCharacter.SelectedIndex = 0;

            m_bUpdatingSpellGame = false;

            UpdateSpellControls();
        }

        private void cbEnableKeyboardHook_CheckedChanged(object sender, EventArgs e)
        {
            m_bSetKeys = true;
        }

        private void lvGameTitles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvGameTitles.SelectedItems.Count < 1)
                return;

            lvGameTitles.SelectedItems[0].BeginEdit();
        }

        private void cmTitlesResetDefault_Click(object sender, EventArgs e)
        {
            if (lvGameTitles.SelectedItems.Count < 1)
            {
                foreach(ListViewItem lvi in lvGameTitles.Items)
                    lvi.Text = Games.DefaultTitle((GameNames)lvi.Tag);
            }
            else
                lvGameTitles.SelectedItems[0].Text = Games.DefaultTitle((GameNames) lvGameTitles.SelectedItems[0].Tag);
        }

        private void cmTitles_Opening(object sender, CancelEventArgs e)
        {
            if (lvGameTitles.SelectedItems.Count < 1)
                cmTitlesResetDefault.Text = "&Reset all values to default";
            else
                cmTitlesResetDefault.Text = String.Format("&Reset to default: {0}", Games.DefaultTitle((GameNames) lvGameTitles.SelectedItems[0].Tag));
        }

        private void lvGameTitles_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    if (e.Modifiers == Keys.None && lvGameTitles.SelectedItems.Count > 0)
                        lvGameTitles.SelectedItems[0].BeginEdit();
                    break;
                default:
                    break;
            }
        }

        private void cbShowEncounters_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void cbShowMonsters_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void llDOSBoxLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Rectangle rc = MemoryHacker.GetDOSBoxRectStatic();
            Global.SetNud(nudDOSBoxLocationX, rc.X);
            Global.SetNud(nudDOSBoxLocationY, rc.Y);
        }

        private void llDOSBoxSize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Rectangle rc = MemoryHacker.GetDOSBoxRectStatic();
            Global.SetNud(nudDOSBoxWidth, rc.Width);
            Global.SetNud(nudDOSBoxHeight, rc.Height);
        }

        private void SetBoxColor(PictureBox pb, SquareStyleList list, SquareStyleList.Name name)
        {
            if (!list.List.ContainsKey(name))
                return;

            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorPattern info = list.List[name];
                Brush brush = new SolidBrush(info.Color);
                if (info.Pattern != HatchStyle.Percent90)
                    brush = new HatchBrush(info.Pattern, info.Color, Color.White);
                g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
            }
            if (pb.Image != null)
                pb.Image.Dispose();
            pb.Image = bmp;
        }

        private void llResetSpecialSquares_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_styles = SquareStyleList.Default;
            SetBoxColors(m_styles);
        }

        private void SetBoxColors(SquareStyleList list)
        {
            SetBoxColor(pbSolid, list, SquareStyleList.Name.Solid);
            SetBoxColor(pbSky, list, SquareStyleList.Name.Sky);
            SetBoxColor(pbBorder, list, SquareStyleList.Name.Border);
            SetBoxColor(pbSpace, list, SquareStyleList.Name.Space);
        }

        private void pbSolid_Click(object sender, EventArgs e)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm();
            form.Color = m_styles.SolidColor;
            form.Pattern = m_styles.SolidPattern;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_styles.List[SquareStyleList.Name.Solid] = new ColorPattern(form.Color, form.Pattern);
                SetBoxColor(pbSolid, m_styles, SquareStyleList.Name.Solid);
                SquareStylesChanged = true;
            }
        }

        private void pbSky_Click(object sender, EventArgs e)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm();
            form.Color = m_styles.SkyColor;
            form.Pattern = m_styles.SkyPattern;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_styles.List[SquareStyleList.Name.Sky] = new ColorPattern(form.Color, form.Pattern);
                SetBoxColor(pbSky, m_styles, SquareStyleList.Name.Sky);
                SquareStylesChanged = true;
            }
        }

        private void pbBorder_Click(object sender, EventArgs e)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm();
            form.Color = m_styles.BorderColor;
            form.Pattern = m_styles.BorderPattern;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_styles.List[SquareStyleList.Name.Border] = new ColorPattern(form.Color, form.Pattern);
                SetBoxColor(pbBorder, m_styles, SquareStyleList.Name.Border);
                SquareStylesChanged = true;
            }
        }

        private void pbSpace_Click(object sender, EventArgs e)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm();
            form.Color = m_styles.SpaceColor;
            form.Pattern = m_styles.SpacePattern;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_styles.List[SquareStyleList.Name.Space] = new ColorPattern(form.Color, form.Pattern);
                SetBoxColor(pbSpace, m_styles, SquareStyleList.Name.Space);
                SquareStylesChanged = true;
            }
        }

        private void cbShowGridLines_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void llSelectAllEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cbEditCopyBackgrounds.Checked = true;
            cbEditCopyIcons.Checked = true;
            cbEditCopyInnerLines.Checked = true;
            cbEditCopyNotes.Checked = true;
            cbEditCopyOuterLines.Checked = true;
        }

        private void cbBringDOSBoxToFront_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void InitWheelCombo(ComboBox combo)
        {
            combo.Items.Clear();
            for (WheelAction action = WheelAction.None; action < WheelAction.Last; action++)
                combo.Items.Add(new WheelActionItem(action));
            combo.SelectedIndex = 0;
        }

        private Action WheelDownAction(ComboBox combo)
        {
            switch (((WheelActionItem)combo.SelectedItem).Action)
            {
                case WheelAction.Zoom: return Action.DecreaseSquareSize;
                case WheelAction.FixedZoom: return Action.DecreaseSquareSizeFixed;
                case WheelAction.Icon: return Action.NextIcon;
                case WheelAction.IconOrientation: return Action.RotateIconCW;
                case WheelAction.BlockStyle: return Action.NextBlockStyle;
                case WheelAction.LineStyle: return Action.NextLineStyle;
                case WheelAction.BlockIndex: return Action.NextBlockIndex;
                case WheelAction.LineIndex: return Action.NextLineIndex;
                case WheelAction.Sheet: return Action.SheetNext;
                case WheelAction.Character: return Action.NextCharacter;
                default: return Action.None;
            }
        }

        private Action WheelUpAction(ComboBox combo)
        {
            switch (((WheelActionItem)combo.SelectedItem).Action)
            {
                case WheelAction.Zoom: return Action.IncreaseSquareSize;
                case WheelAction.FixedZoom: return Action.IncreaseSquareSizeFixed;
                case WheelAction.Icon: return Action.PrevIcon;
                case WheelAction.IconOrientation: return Action.RotateIconCCW;
                case WheelAction.BlockStyle: return Action.PrevBlockStyle;
                case WheelAction.LineStyle: return Action.PrevLineStyle;
                case WheelAction.BlockIndex: return Action.PrevBlockIndex;
                case WheelAction.LineIndex: return Action.PrevLineIndex;
                case WheelAction.Sheet: return Action.SheetPrevious;
                case WheelAction.Character: return Action.PrevCharacter;
                default: return Action.None;
            }
        }

        private void SetWheelCombo(ComboBox combo, WheelAction action)
        {
            foreach (WheelActionItem item in combo.Items)
            {
                if (item.Action == action)
                {
                    combo.SelectedItem = item;
                    break;
                }
            }
        }

        private WheelAction GetWheelAction(Action action)
        {
            switch (action)
            {
                case Action.IncreaseSquareSize:
                case Action.DecreaseSquareSize:
                    return WheelAction.Zoom;
                case Action.IncreaseSquareSizeFixed:
                case Action.DecreaseSquareSizeFixed:
                    return WheelAction.FixedZoom;
                case Action.NextIcon:
                case Action.PrevIcon:
                    return WheelAction.Icon;
                case Action.RotateIconCW:
                case Action.RotateIconCCW:
                    return WheelAction.IconOrientation;
                case Action.NextBlockStyle:
                case Action.PrevBlockStyle:
                    return WheelAction.BlockStyle;
                case Action.NextLineStyle:
                case Action.PrevLineStyle:
                    return WheelAction.LineStyle;
                case Action.NextBlockIndex:
                case Action.PrevBlockIndex:
                    return WheelAction.BlockIndex;
                case Action.NextLineIndex:
                case Action.PrevLineIndex:
                    return WheelAction.LineIndex;
                case Action.SheetNext:
                case Action.SheetPrevious:
                    return WheelAction.Sheet;
                case Action.NextCharacter:
                case Action.PrevCharacter:
                    return WheelAction.Character;
                default: return WheelAction.None;
            }
        }

        private void llMouseDefaults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Reset all mouse shortcuts to their defaults?", "Reset Mouse Defaults", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;

            SetMouseDefaults();
        }

        private void SetMouseDefaults()
        {
            SetMouseAction(comboMouseLeft, Action.Draw);
            SetMouseAction(comboMouseRight, Action.ScrollSheet);
            SetMouseAction(comboMouseMiddle, Action.None);
            SetMouseAction(comboMouseX1, Action.None);
            SetMouseAction(comboMouseX2, Action.None);
            SetWheelCombo(comboWheel, WheelAction.IconOrientation);
            SetWheelCombo(comboControlWheel, WheelAction.Zoom);
            SetWheelCombo(comboControlShiftWheel, WheelAction.FixedZoom);
            SetWheelCombo(comboShiftWheel, WheelAction.Icon);
            SetWheelCombo(comboAltWheel, WheelAction.None);
            SetWheelCombo(comboControlAltWheel, WheelAction.None);
            SetWheelCombo(comboShiftAltWheel, WheelAction.None);
        }

        private void llResetKeyboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (NativeMethods.IsControlDown())
            {
                if (MessageBox.Show("Reset all keyboard shortcuts to the debug defaults?", "Reset Keyboard Defaults", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                UpdateShortcutsView(Global.DebugShortcuts);
                m_notifications = Notifications.Defaults;

                m_bSetKeys = true;
                return;
            }

            if (MessageBox.Show("Reset all keyboard shortcuts to their defaults?", "Reset Keyboard Defaults", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                return;

            UpdateShortcutsView(Global.DefaultShortcuts);
            m_notifications = Notifications.Defaults;

            m_bSetKeys = true;
        }

        private void llImportKeys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files|*.xml|All Files|*.*";
            ofd.FileName = "WhereAreWe_Keyboard_Shortcuts.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(File.ReadAllText(ofd.FileName));
                        UpdateShortcutsView((Shortcuts) new ShortcutsTypeConverter().ConvertFrom(doc.SelectSingleNode("/Actions/shortcuts").OuterXml));
                        m_notifications = Notifications.FromString(doc.SelectSingleNode("/Actions/Notifications").OuterXml);
                        bRetry = false;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + ofd.FileName + "\" could not be converted to a list of shortcuts.\r\n\r\nException:  " + ex.Message, "Error importing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            return;
                    }
                }
            }
        }

        private void llExportKeys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files|*.xml|All Files|*.*";
            sfd.FileName = "WhereAreWe_Keyboard_Shortcuts.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string strShortcuts = GetShortcuts().ToString(false);
                string strNotifications = m_notifications.ToString(false);
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        using (XmlWriter writer = XmlWriter.Create(sb))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("Actions");
                            writer.WriteRaw(strShortcuts);
                            writer.WriteRaw(strNotifications);
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        bRetry = false;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + sfd.FileName + "\" could not be written.\r\n\r\nException:  " + ex.Message, "Error exporting to file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            return;
                    }
                }
            }
        }

        public static void ImportSettingsFile(string strFile)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            File.Copy(strFile, config.FilePath, true);
            Properties.Settings.Default.Reload();
        }

        private void llImportAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files|*.xml|All Files|*.*";
            ofd.FileName = "WhereAreWe_Settings.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        ImportSettingsFile(ofd.FileName);
                        UpdateUI();
                        bRetry = false;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + ofd.FileName + "\" could not be copied to the user.config.\r\n\r\nException:  " + ex.Message, "Error importing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            return;
                    }
                }
            }
        }

        private void llExportAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files|*.xml|All Files|*.*";
            sfd.FileName = "WhereAreWe_Settings.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                UpdateFromUI();
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                        File.Copy(config.FilePath, sfd.FileName, true);
                        bRetry = false;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + sfd.FileName + "\" could not be written.\r\n\r\nException:  " + ex.Message, "Error exporting to file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            return;
                    }
                }
            }
        }

        public static void ResetSettings()
        {
            Properties.Settings.Default.Reset();
        }

        private void llResetAllSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset all of the user settings to their default values?\r\n" +
                "This includes window positions, shortcuts, and game paths as well as the more obvious options!", "Reset all settings?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ResetSettings();
                Close();
            }
        }

        private void cmKeyboardNotification_Click(object sender, EventArgs e)
        {
            EditNotification();
        }

        private void EditNotification()
        {
            if (lvInput.SelectedItems.Count < 1)
                return;

            InputOption option = lvInput.SelectedItems[0].Tag as InputOption;
            if (option == null)
                return;
            Notification alert = m_notifications.GetAlert(option.Action);
            if (alert == null)
                alert = new Notification();
            EditNotificationForm form = new EditNotificationForm();
            form.Action = option.Action;
            form.Notification = alert;
            if (form.ShowDialog() == DialogResult.OK)
                m_notifications.SetAlert(form.Action, form.Notification);
        }

        private void SetGameIndex(ComboBox combo, GameNames game)
        {
            for(int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i] is CurrentGameName && ((CurrentGameName) combo.Items[i]).Game == game)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }

        private void cbSkipIntroductions_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void llSnapMargins_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WindowMarginsForm form = new WindowMarginsForm();
            form.Margins = Properties.Settings.Default.WindowSnapMargins;
            if (form.ShowDialog() == DialogResult.OK)
                Properties.Settings.Default.WindowSnapMargins = form.Margins;
        }

        private void comboSpellKeySpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSpellHotkeyCollection();
        }

        private void pbSpoilerColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorForm = new ColorDialog();
            colorForm.Color = pbSpoilerColor.BackColor;
            if (colorForm.ShowDialog() == DialogResult.OK)
                pbSpoilerColor.BackColor = colorForm.Color;
        }
    }

    class WheelActionItem
    {
        public WheelAction Action;

        public WheelActionItem(WheelAction action)
        {
            Action = action;
        }

        public override string ToString() { return Global.WheelActionString(Action); }
    }

    class InputOptionItemComparer : BasicListViewComparer
    {
        public InputOptionItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }
    }

    public class MouseActionItem
    {
        public Action Action;

        public MouseActionItem(Action action)
        {
            Action = action;
        }

        public override string ToString()
        {
            switch (Action)
            {
                case Action.ScrollSheet: return "Drag to scroll the map window in any mode";
                case Action.Draw: return "Draw, move or select items, depending on the mode";
                case Action.DrawScroll: return "Drag to scroll in Play Mode, draw/move/select items otherwise";
                case Action.MoveLabels: return "Move/edit notes and labels as if in Notes mode (except in Edit mode)";
                case Action.DrawBlocks: return "Draw blocks as if in Blocks mode (except in Edit mode)";
                case Action.DrawLines: return "Draw lines as if in Lines mode (except in Edit mode)";
                case Action.DrawHybrid: return "Draw hybrid blocks as if in Hybrid mode (except in Edit mode)";
                case Action.DrawFill: return "Fill as if in Fill mode (except in Edit mode)";
                case Action.None: return "No action";
                default: return String.Empty;
            }
        }
    }

    public class CurrentGameName
    {
        public GameNames Game;
        public string Name;

        public CurrentGameName(GameNames game)
        {
            Game = game;
            Name = Games.Name(game);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SpellGameTag
    {
        public GameNames Game;

        public SpellGameTag(GameNames game)
        {
            Game = game;
        }

        public override string ToString()
        {
            return Games.Name(Game);
        }
    }

    public class HKCharTag
    {
        public SpellHotkey.HKCharacter HKChar;

        public HKCharTag(SpellHotkey.HKCharacter hkc)
        {
            HKChar = hkc;
        }

        public override string ToString()
        {
            return SpellHotkey.CharacterString(HKChar);
        }
    }

    public class HKSpellTag
    {
        public Spell HKSpell;

        public HKSpellTag(Spell spell)
        {
            HKSpell = spell;
        }

        public override string ToString()
        {
            if (HKSpell == null)
                return "None";
            if (HKSpell.MayHaveDuplicateNames)
                return String.Format("{0}: {1}", Spell.TypeString(HKSpell.Type), HKSpell.Name);
            return HKSpell.Name;
        }
    }
}
