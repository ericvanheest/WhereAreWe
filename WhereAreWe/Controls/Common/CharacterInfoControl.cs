using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using WhereAreWe.MM1QuestStates;

namespace WhereAreWe
{
    public partial class CharacterInfoControl : HackerBasedUserControl
    {
        protected CheatOffsets m_cheatOffsets = null;
        protected byte[] m_bytes = null;
        protected int m_iCharacterPosition = -1;
        protected int m_iCharacterIndex = -1;
        protected int m_iCharacterAddress = -1;
        protected ViewPartyForm m_formParty = null;
        protected BaseCharacter m_char = null;
        protected CharCommonControls m_commonCtrls;
        protected AttributeType m_cheatType = AttributeType.Unknown;
        protected Control[] m_editableAttributes = null;
        protected Size m_szOrigEquipped;
        protected PrimaryStat[] m_lastStatOrder = null;
        protected ResistanceValue[] m_lastResistances = null;
        protected Subscreen m_oldScreen = Subscreen.Unknown;

        protected ToolTip m_tipCondition = null;
        protected ToolTip m_tipResistances = null;
        protected ToolTip m_tipSpells = null;
        protected ToolTip m_tipSkills = null;
        protected ToolTip m_tipStats = null;
        protected Control m_ctrlEdit = null;
        protected bool m_bDebugMonitorBackpack = false;
        protected List<Item> m_lastBackpack = null;

        private bool m_bUpdate = false;

        public CharacterInfoControl()
        {
            m_bUpdate = true;
            InitializeComponent();

            SetCommonControls();
            m_bUpdate = false;
        }

        public SplitterPanel QuickRefPanel { get { return scCharQuickref.Panel2; } }
        public ListView BackpackLV => lvBackpack;
        public ListView EquippedLV => lvEquipped;

        public int QuickRefSplitPosition
        {
            get { return scCharQuickref.SplitterDistance; }
            set
            {
                if (value <= 0)
                    return;
                BeginUpdate();
                Global.SetSplitterDistance(scCharQuickref, value);
                EndUpdate();
            }
        }

        public int ResistancesSplitPosition
        {
            get { return scStatsResistances.SplitterDistance; }
            set
            {
                if (value <= 0)
                    return;
                BeginUpdate();
                Global.SetSplitterDistance(scStatsResistances, value);
                EndUpdate();
            }
        }

        public void BeginUpdate() { m_bUpdate = true; }
        public void EndUpdate() { m_bUpdate = false; }

        private void AddControls(List<Control> labels, ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.Controls != null)
                    AddControls(labels, ctrl.Controls);
                if (ctrl is EditableAttributeLabel)
                    ctrl.MouseUp += new MouseEventHandler(EditableAttributeLabel_MouseUp);
                else if (ctrl is MMItemLabel)
                    ((MMItemLabel)ctrl).AddMouseUpEvent(new MouseEventHandler(EditableAttributeLabel_MouseUp));
                labels.Add(ctrl);
            }
        }

        private InventoryItemCounts GetItemCounts(ListView lv)
        {
            InventoryItemCounts counts = new InventoryItemCounts();

            foreach (ListViewItem lvi in lv.Items)
            {
                InventoryItemTag tag = lvi.Tag as InventoryItemTag;
                if (tag == null)
                    continue;

                switch (tag.Item.Type)
                {
                    case ItemType.Weapon:
                    case ItemType.OneHandMelee:
                    case ItemType.TwoHandMelee:
                    case ItemType.Missile:
                        counts.Weapons++;
                        break;
                    case ItemType.Accessory:
                        counts.Accessories++;
                        break;
                    case ItemType.Armor:
                        counts.Armor++;
                        break;
                    default:
                        counts.Miscellaneous++;
                        break;
                }
            }

            return counts;
        }

        public void UpdateHeaders()
        {
            if (m_char is MM1Character || m_char is MM2Character)
            {
                lvBackpack.Columns[0].Text = "Backpack";
                lvEquipped.Columns[0].Text = "Equipped";
            }
            else
            {
                int iBox = 0;
                if (m_char is Wiz4Character)
                    iBox = ((Wiz4Character)m_char).Inventory.Items.Count(i => i.MemoryIndex > 7);
                lvBackpack.Columns[0].Text = GetItemCounts(lvBackpack).GetHeaderString("Backpack", iBox, "in Black Box");
                lvEquipped.Columns[0].Text = GetItemCounts(lvEquipped).GetHeaderString("Equipped");
            }
        }

        public void ClearInventoryLists()
        {
            lvBackpack.Items.Clear();
            lvEquipped.Items.Clear();
        }

        public void SetBackpackLVI(int index, Item item, BaseCharacter character)
        {
            SetInventoryLVI(lvBackpack, index, item, character);
        }

        public void SetEquippedLVI(int index, Item item, BaseCharacter character)
        {
            SetInventoryLVI(lvEquipped, index, item, character);
        }

        public void SetBackpackLVI(Item item, BaseCharacter character)
        {
            SetInventoryLVI(lvBackpack, -1, item, character);
        }

        public void SetEquippedLVI(Item item, BaseCharacter character)
        {
            SetInventoryLVI(lvEquipped, -1, item, character);
        }

        public void CheckMonitorBackpack()
        {
            if (m_bDebugMonitorBackpack)
            {
                Inventory inv = m_char.BasicInventory;
                if (m_lastBackpack == null)
                    m_lastBackpack = new List<Item>();
                for (int i = 0; i < Math.Max(m_lastBackpack.Count, inv.Items.Count); i++)
                {
                    if (i >= m_lastBackpack.Count)
                        Global.Log("Char{0} +Item: {1}", m_char.BasicAddress, inv.Items[i].DebugString);
                    else if (i >= inv.Items.Count)
                        Global.Log("Char{0} -Item: {1}", m_char.BasicAddress, m_lastBackpack[i].DebugString);
                    else if (m_lastBackpack[i].DebugString != inv.Items[i].DebugString)
                        Global.Log("Char{0} xItem: {1} <=> {2}", m_char.BasicAddress, m_lastBackpack[i].DebugString, inv.Items[i].DebugString);
                }
                m_lastBackpack = inv.Items;
            }
        }

        public void SetResistances(ResistanceValue[] resist)
        {
            if (Global.Compare(m_lastResistances, resist))
                return;

            m_lastResistances = resist;
            if (lvResistances.Items.Count > resist.Length)
                lvResistances.Items.Clear();

            int iIndex = 0;
            foreach (ResistanceValue rv in resist)
            {
                ListViewItem lvi;
                string strResist = Global.SingleResistance(rv.Resistance);
                if (iIndex >= lvResistances.Items.Count)
                {
                    lvi = new ListViewItem(strResist);
                    lvi.SubItems.Add(rv.ToString());
                }
                else
                {
                    lvi = lvResistances.Items[iIndex];
                    if (lvi.Text != strResist)
                        lvi.Text = strResist;  // prevent flashing
                    strResist = rv.ToString();
                    if (lvi.SubItems[1].Text != strResist)
                        lvi.SubItems[1].Text = strResist;
                }

                lvi.Tag = rv;
                lvi.ToolTipText = Global.Flatten(m_char.Modifiers.Reasons(resist[iIndex].Resistance));

                if (iIndex >= lvResistances.Items.Count)
                    lvResistances.Items.Add(lvi);

                iIndex++;
            }
            lvResistances.BackColor = Color.FromArgb(255, BackColor);
        }

        public string LVIDescriptionString(BaseCharacter mmChar, Item item)
        {
            return item.FormatDescription(Properties.Settings.Default.ItemFormat, m_char.BasicAlignment.Temporary, m_char.BasicClass);
        }

        public void SetInventoryLVI(ListView lv, int index, Item item, BaseCharacter character)
        {
            if (index > -1)
            {
                while (lv.Items.Count <= index)
                    lv.Items.Add(new ListViewItem());
            }
            else
                index = lv.Items.Add(new ListViewItem()).Index;

            if (item == null || (item.Index == item.EmptyIndex))
            {
                lv.Items[index].Text = String.Empty;
                lv.Items[index].ToolTipText = String.Empty;
                lv.Items[index].Tag = null;
            }
            else
            {
                string strDesc;
                if (item is WizItem && item.Index == 0)
                    strDesc = "<empty>";
                else
                    strDesc = LVIDescriptionString(m_char, item);
                lv.Items[index].Text = strDesc;

                if (!item.IsIdentified && !item.RevealUnidentified && Properties.Settings.Default.HideUnidentifiedItems)
                    lv.Items[index].ToolTipText = Global.UnidentifiedItemNoTradeTip;
                else
                    lv.Items[index].ToolTipText = item.MultiLineDescription;
                lv.Items[index].Tag = new InventoryItemTag(item, item.MemoryIndex, item.DisplayIndex, strDesc);
            }
        }

        public CharacterInfoControl(IMain main)
        {
            InitializeComponent();

            SetCommonControls();

            SetMain(main);
        }

        protected void FindEditableAttributes()
        {
            List<Control> labels = new List<Control>();

            AddControls(labels, Controls);

            m_editableAttributes = labels.ToArray();
        }

        protected virtual void SetCommonControls()
        {
            m_commonCtrls = new CharCommonControls();
            m_tipCondition = new ToolTip();
            m_tipSpells = new ToolTip();
            m_tipSkills = new ToolTip();
            m_tipStats = new ToolTip();
            m_tipResistances = new ToolTip();
            m_tipStats.ShowAlways = true;
            m_tipResistances.ShowAlways = true;
            m_tipStats.AutoPopDelay = 32000;

            NativeMethods.SetTooltipDelay(lvBackpack, 32000);
            NativeMethods.SetTooltipDelay(lvEquipped, 32000);
            NativeMethods.SetTooltipDelay(lvResistances, 32000);
            m_szOrigEquipped = lvEquipped.Size;

            m_commonCtrls.llCureAll = llCureAll;
            m_commonCtrls.lvBackpack = lvBackpack;
            m_commonCtrls.lvEquipped = lvEquipped;
            m_commonCtrls.lvResistances = lvResistances;
            m_commonCtrls.labelMelee = labelMelee;
            m_commonCtrls.labelMeleeHeader = labelMeleeHeader;
            m_commonCtrls.labelExp = labelExp;
            m_commonCtrls.labelCondition = labelCondition;
            m_commonCtrls.labelConditionHeader = labelConditionHeader;
            m_commonCtrls.labelAC = labelAC;
            m_commonCtrls.labelHP = labelHP;
            m_commonCtrls.labelSP = labelSP;
            m_commonCtrls.labelACHeader = labelACHeader;
            m_commonCtrls.labelHPHeader = labelHPHeader;
            m_commonCtrls.labelSPHeader = labelSPHeader;
            m_commonCtrls.labelLevel = labelLevel;
            m_commonCtrls.labelExpHeader = labelExpHeader;

            SetInventorySize(true);
        }

        public void MoveControls(Control ctrlTo, params Control[] controls)
        {
            MoveControls(ctrlTo, 0, controls);
        }

        public void MoveControls(Control ctrlTo, int iSecondX, params Control[] controls)
        {
            if (controls == null || controls.Length < 1)
                return;

            if (controls[0].Location == ctrlTo.Location && (controls.Length < 2 || controls[1].Location.X == iSecondX || iSecondX == 0))
                return;     // Already moved these; don't cause flicker

            Point ptOriginal = controls[0].Location;

            for(int i = 0; i < controls.Length; i++)
            {
                if (i == 1 && iSecondX != 0)
                    controls[i].Location = new Point(iSecondX, ctrlTo.Location.Y);
                else
                    controls[i].Location = new Point(ctrlTo.Location.X + (controls[i].Location.X - ptOriginal.X), ctrlTo.Location.Y);
            }
        }

        public virtual BaseCharacter Character
        {
            get { return m_char; }
        }

        public int CharacterIndex
        {
            get { return m_iCharacterIndex; }
        }

        public int CharacterAddress
        {
            get { return m_iCharacterAddress; }
        }

        public int CharacterPosition
        {
            get { return m_iCharacterPosition; }
        }

        public void SetPartyWindow(ViewPartyForm form)
        {
            m_formParty = form;
        }

        public virtual void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null) { }
        public virtual void UpdateUI() { }
        public virtual int CharacterSize { get { return 128; } }

        public virtual void CheckTriggers(TriggerList triggers, bool bRevertAll = false)
        {
            if (triggers == null)
                return; 

            if (bRevertAll)
            {
                foreach (TriggerControls tc in m_revertTriggers.Keys)
                {
                    if (tc.Ctrl.Main is TabPage)
                        m_main.PartyForm?.ClearTabStyle(tc.Ctrl.Main as TabPage);
                    else
                        m_revertTriggers[tc].Revert();
                }
                m_revertTriggers.Clear();
            }

            HashSet<TriggerControl> ctrlsChanged = new HashSet<TriggerControl>();
            if (triggers.Enabled)
            {
                foreach (CharacterTrigger trigger in triggers.Items)
                {
                    if (!trigger.Enabled)
                        continue;
                    TriggerControl[] ctrls = CheckTrigger(trigger);
                    if (ctrls == null)
                        continue;
                    foreach (TriggerControl ctrl in ctrls)
                    {
                        if (ctrl != null && !ctrlsChanged.Contains(ctrl))
                            ctrlsChanged.Add(ctrl);
                    }
                }
            }

            TriggerControls[] toRevert = m_revertTriggers.Keys.ToArray(); // So that we can remove entries from the dictionary in the loop
            foreach (TriggerControls tc in toRevert)
            {
                if (!ctrlsChanged.Contains(tc.Ctrl))
                {
                    if (tc.Ctrl.Main is TabPage)
                        m_main.PartyForm?.ClearTabStyle(tc.Ctrl.Main as TabPage);
                    else
                        m_revertTriggers[tc].Revert();
                    m_revertTriggers.Remove(tc);
                }
            }
        }

        public Dictionary<TriggerControls, TriggerReverter> m_revertTriggers = new Dictionary<TriggerControls, TriggerReverter>();

        public static bool CheckTriggerWho(CharacterTrigger trigger, BaseCharacter baseChar, int iCharIndex)
        {
            int iTest = 0;
            switch (trigger.Who)
            {
                case TriggerWho.AnyCharacter:
                case TriggerWho.EntireParty:
                    break;
                case TriggerWho.ClassEquals:
                    if (String.Compare(BaseCharacter.ClassString(baseChar.BasicClass), trigger.WhoValue, true) == 0)
                        break;
                    return false; // no class match
                case TriggerWho.IndexEquals:
                    if (!Int32.TryParse(trigger.WhoValue, out iTest))
                        return false; // invalid number; skip
                    if (iCharIndex != iTest)
                        return false;
                    break;
                case TriggerWho.NameEquals:
                    if (String.Compare(baseChar.Name, trigger.WhoValue, true) == 0)
                        break;
                    return false; // no name match
                case TriggerWho.NonSpellcaster:
                    if (baseChar.IsCaster)
                        return false;
                    break;
                case TriggerWho.Spellcaster:
                    if (!baseChar.IsCaster)
                        return false;
                    break;
                default: return false;    // unknown; skip it
            }
            return true;
        }

        public static TriggerTarget[] CheckTriggerConditions(MemoryHacker hacker, List<BaseCharacter> allChars, BaseCharacter baseChar, CharacterTrigger trigger)
        {
            List<TriggerTarget> listTested = new List<TriggerTarget>();

            foreach (TriggerCondition cond in trigger.Conditions)
            {
                foreach (TriggerCondition tc in cond.Expand(baseChar))
                {
                    if (CheckTriggerCondition(hacker, allChars, baseChar, trigger, tc, out TriggerTarget target))
                        listTested.Add(target);
                    // TODO: pay attention to tc.Previous somehow
                }
            }
            return listTested.ToArray();
        }

        public void DoSetColorTo(TriggerControl ctrl, CharacterTrigger trigger, Color fore, Color back)
        {
            if (ctrl.Main is ListView)
            {
                ListView lv = ctrl.Main as ListView;
                Global.SetListViewSubItemColor(lv, ctrl.Index, ctrl.SubIndex, fore, back);
                if (ctrl.Index >= 0 && ctrl.Index < lv.Items.Count)
                {
                    InventoryItemTag iit = lv.Items[ctrl.Index].Tag as InventoryItemTag;
                    if (iit != null)
                        iit.Triggered = true;
                }
            }
            else if (ctrl.Main is TabPage)
                m_main.PartyForm.SetTabStyle(ctrl.Main as TabPage, trigger);
            else
            {
                ctrl.Main.ForeColor = fore;
                ctrl.Main.BackColor = back;
            }
        }

        public virtual TriggerControl[] CheckTrigger(CharacterTrigger trigger)
        {
            if (!CheckTriggerWho(trigger, m_char, CharacterIndex))
                return null;

            TriggerTarget[] testedEntities = CheckTriggerConditions(m_main.Hacker, m_formParty.GetCharacters(), m_char, trigger);
            if (testedEntities == null || testedEntities.Length < 1)
                return null;

            // Condition was met; perform requested action

            TriggerTarget[] targets = trigger.To == TriggerEntity.TestedItem ? testedEntities : new TriggerTarget[] { new TriggerTarget(trigger.To, trigger.ToValue, testedEntities) };

            List<TriggerControl> targetControls = new List<TriggerControl>(targets.Length);

            foreach (TriggerTarget target in targets)
            {
                TriggerControl ctrl = GetTriggerControl(target.Entity, target.Value);
                TriggerSubItem[] items = m_formParty.GetQuickRefItems(target.Entity, CharacterIndex);

                TriggerControls tc = new TriggerControls(ctrl, items);
                if (tc.Empty)
                    continue;

                CharacterTrigger triggerAction = trigger;

                if (!m_revertTriggers.ContainsKey(tc))
                {
                    TriggerReverter tr = null;
                    if (tc.Ctrl.Main != lvBackpack && tc.Ctrl.Main != lvEquipped)
                        tr = new TriggerReverter(tc, trigger, ColoredUIElements.PartyFormItem);
                    else
                        tr = new TriggerReverter(tc, trigger, SystemColors.ControlText, SystemColors.Window);
                    m_revertTriggers.Add(tc, tr);
                }

                switch (trigger.Do)
                {
                    case TriggerDo.SetBoldFont:
                        if (ctrl.Main is ListView)
                            Global.SetListViewSubItemFont(ctrl.Main as ListView, ctrl.Index, ctrl.SubIndex, FontStyle.Bold);
                        else if (ctrl.Main is TabPage)
                            m_main.PartyForm.SetTabStyle(ctrl.Main as TabPage, trigger);
                        else
                            ctrl.Main.Font = new Font(ctrl.Main.Font, FontStyle.Bold);
                        break;
                    case TriggerDo.SetItalicFont:
                        if (ctrl.Main is ListView)
                            Global.SetListViewSubItemFont(ctrl.Main as ListView, ctrl.Index, ctrl.SubIndex, FontStyle.Italic);
                        else if (ctrl.Main is TabPage)
                            m_main.PartyForm.SetTabStyle(ctrl.Main as TabPage, trigger);
                        else
                            ctrl.Main.Font = new Font(ctrl.Main.Font, FontStyle.Italic);
                        break;
                    case TriggerDo.SetColorTo:
                        DoSetColorTo(ctrl, trigger, trigger.DoColorFore, trigger.DoColorBack);
                        break;
                    case TriggerDo.SetGradient:
                        Color colorBetweenFore = Global.GetGradientValue(trigger.DoColorFore, trigger.DoColorFore2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                        Color colorBetweenBack = Global.GetGradientValue(trigger.DoColorBack, trigger.DoColorBack2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                        DoSetColorTo(ctrl, trigger, colorBetweenFore, colorBetweenBack);
                        break;
                    default:
                        break;
                }

                // Change ListViewItem text every time, because the list items are redrawn when necessary
                foreach (TriggerSubItem si in items)
                {
                    switch (triggerAction.Do)
                    {
                        case TriggerDo.SetBoldFont:
                            si?.SetBold();
                            break;
                        case TriggerDo.SetItalicFont:
                            si?.SetItalic();
                            break;
                        case TriggerDo.SetColorTo:
                            si?.SetColors(triggerAction.DoColorFore, triggerAction.DoColorBack);
                            break;
                        case TriggerDo.SetGradient:
                            Color colorBetweenFore = Global.GetGradientValue(triggerAction.DoColorFore, triggerAction.DoColorFore2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                            Color colorBetweenBack = Global.GetGradientValue(triggerAction.DoColorBack, triggerAction.DoColorBack2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                            si?.SetColors(colorBetweenFore, colorBetweenBack);
                            break;
                    }
                }

                targetControls.Add(ctrl);
            }

            return targetControls.ToArray();
        }

        private int GetTriggerLVI(ListView lv, string strSearch)
        {
            if (Int32.TryParse(strSearch, out int iIndex))
                return iIndex;
            foreach (ListViewItem lvi in lv.Items)
                if (String.Compare(lvi.Text, strSearch, true) == 0)
                    return lvi.Index;
            return -1;
        }

        public virtual TriggerControl GetTriggerControl(TriggerEntity entity, string strVal)
        {
            switch (entity)
            {
                case TriggerEntity.SpellPoints:
                case TriggerEntity.MaxSpellPoints: return new TriggerControl(labelSP);    // SP: X/Y is one control
                case TriggerEntity.HitPoints:
                case TriggerEntity.MaxHitPoints: return new TriggerControl(labelHP);      // HP: X/Y is one control
                case TriggerEntity.ArmorClass: return new TriggerControl(labelAC);
                case TriggerEntity.MeleeDamageAverage: return new TriggerControl(labelMelee);
                case TriggerEntity.Condition: return new TriggerControl(labelCondition);
                case TriggerEntity.Experience:
                case TriggerEntity.ExperienceRangeCurrentLevel:
                case TriggerEntity.ExperienceToNext: return new TriggerControl(labelExp);
                case TriggerEntity.CurrentLevel:
                case TriggerEntity.ProperLevel:
                case TriggerEntity.CurrentAge:
                case TriggerEntity.ProperAge:
                case TriggerEntity.CurrentAlignment:
                case TriggerEntity.ProperAlignment: return new TriggerControl(labelLevel);
                case TriggerEntity.BackpackItemCount:
                case TriggerEntity.BackpackItemMax: return null;
                case TriggerEntity.BackpackItemNames: return new TriggerControl(lvBackpack);
                case TriggerEntity.AnyBackpackItemAge: return new TriggerControl(lvBackpack);
                case TriggerEntity.Name:
                case TriggerEntity.TabTitle: return Parent is TabPage ? new TriggerControl(Parent) : null;
                case TriggerEntity.ResistanceIndex:
                case TriggerEntity.ResistanceNamed: return new TriggerControl(lvResistances, GetTriggerLVI(lvResistances, strVal), 1);
                case TriggerEntity.BackpackItemAge:
                case TriggerEntity.BackpackItemName: return new TriggerControl(lvBackpack, GetTriggerLVI(lvBackpack, strVal), 0);
                default: return null;
            }
        }

        public static long LongValue(BaseCharacter baseChar, TriggerEntity entity, string strVal)
        {
            if (baseChar == null)
                return -1;

            Int64.TryParse(strVal, out long iVal);
            ResistanceValue[] res = null;

            switch (entity)
            {
                case TriggerEntity.CharIndex: return baseChar.BasicAddress;
                case TriggerEntity.SpellPoints: return baseChar.BasicSP;
                case TriggerEntity.MaxSpellPoints: return baseChar.BasicMaxSP;
                case TriggerEntity.SpecificValue: return iVal;
                case TriggerEntity.StatIndex: return baseChar.Stat((int) iVal).Temporary;
                case TriggerEntity.StatNamed: return baseChar.Stat(strVal).Temporary;
                case TriggerEntity.ResistanceIndex:
                    res = baseChar.GetResistances();
                    if (iVal < 0 || iVal >= res.Length)
                        return 0;
                    return res[iVal].Total;
                case TriggerEntity.ResistanceNamed:
                    res = baseChar.GetResistances();
                    foreach (ResistanceValue r in res)
                    {
                        if (String.Compare(Global.SingleResistance(r.Resistance), strVal, true) == 0)
                            return r.Total;
                    }
                    return 0;
                case TriggerEntity.BackpackItemAge:
                    if (iVal < 0 || iVal >= baseChar.BackpackItems.Count)
                        return -1;
                    return baseChar.BackpackItems[(int) iVal].Age;
                case TriggerEntity.HitPoints: return baseChar.BasicHP;
                case TriggerEntity.MaxHitPoints: return baseChar.BasicMaxHP;
                case TriggerEntity.Experience: return baseChar.BasicExperience;
                case TriggerEntity.ExperienceToNext: return baseChar.BasicLevel.Permanent == baseChar.MaxLevel ? Int64.MaxValue : baseChar.NeedsXP;
                case TriggerEntity.ExperienceRangeCurrentLevel: return baseChar.BasicLevel.Permanent == baseChar.MaxLevel ? Int64.MaxValue :
                        baseChar.XPForLevel(baseChar.BasicClass, baseChar.BasicLevel.Permanent + 1) - baseChar.XPForLevel(baseChar.BasicClass, baseChar.BasicLevel.Permanent);
                case TriggerEntity.Gems: return baseChar.QuickRefGems;
                case TriggerEntity.Gold: return baseChar.BasicMoney;
                case TriggerEntity.Food: return baseChar.BasicFood;
                case TriggerEntity.MaxFood: return baseChar.BasicMaxFood;
                case TriggerEntity.Thievery: return baseChar.BasicThievery;
                case TriggerEntity.MeleeDamageAverage: return (long) baseChar.BasicMeleeDamage.Average;
                case TriggerEntity.RangedDamageAverage: return (long) baseChar.BasicRangedDamage.Average;
                case TriggerEntity.ArmorClass: return baseChar.BasicAC.Temporary;
                case TriggerEntity.SpellLevel: return baseChar.QuickRefSpellLevel.Temporary;
                case TriggerEntity.BackpackItemCount: return baseChar.BackpackItems.Count;
                case TriggerEntity.BackpackItemMax: return baseChar.MaxBackpackSize;
                case TriggerEntity.CurrentLevel: return baseChar.BasicLevel.Temporary;
                case TriggerEntity.ProperLevel: return baseChar.BasicLevel.Permanent;
                case TriggerEntity.CurrentAge: return baseChar.BasicAge.Years + baseChar.BasicAge.Modifier;
                case TriggerEntity.ProperAge: return baseChar.BasicAge.Years;
                case TriggerEntity.Deaths: return baseChar.BasicDeaths;
                case TriggerEntity.Swimming: return baseChar.BasicSwimming;
                case TriggerEntity.Disarm: return baseChar.BasicDisarm;
                case TriggerEntity.Regen: return baseChar.BasicRegen;
                case TriggerEntity.Poison: return baseChar.BasicPoison;
                case TriggerEntity.IDTrap: return baseChar.BasicIDTrap;
                case TriggerEntity.Dispel: return baseChar.BasicDispel;
                case TriggerEntity.Critical: return baseChar.BasicCritical;
                case TriggerEntity.Hide: return baseChar.BasicHide;
                case TriggerEntity.Attacks: return baseChar.BasicAttacks;
                case TriggerEntity.Won: return baseChar.BasicWon;
                case TriggerEntity.Songs: return baseChar.BasicSongs;
                default: return 0;
            }
        }

        public static string GameValue(MemoryHacker hacker, TriggerEntity entity)
        {
            GameTime gt = hacker.GetGameTime();

            switch (entity)
            {
                case TriggerEntity.TimeDayOfMonth: return "0";
                case TriggerEntity.TimeDayOfWeek: return gt.DayOfWeek.ToString();
                case TriggerEntity.TimeDayOfYear: return gt.Day.ToString();
                case TriggerEntity.TimeHour: return gt.Hour.ToString();
                case TriggerEntity.TimeMinute: return gt.Minute.ToString();
                case TriggerEntity.TimeYear: return gt.Year.ToString();
                default: return "";
            }
        }

        public static string StringValue(MemoryHacker hacker, BaseCharacter baseChar, TriggerEntity entity, string strVal)
        {
            Int64.TryParse(strVal, out long iVal);
            switch (entity)
            {
                case TriggerEntity.Class: return BaseCharacter.ClassString(baseChar.BasicClass);
                case TriggerEntity.Name: return baseChar.Name;
                case TriggerEntity.SpecificValue: return strVal;
                case TriggerEntity.Condition:
                    string strCondition = baseChar.QuickRefCondition;
                    if (String.IsNullOrEmpty(strCondition))
                        return "Good";
                    return strCondition;
                case TriggerEntity.BackpackItemNames:
                    StringBuilder sb = new StringBuilder();
                    foreach (Item item in baseChar.BackpackItems)
                        sb.AppendFormat("{0}\r\n", item.Name);
                    return sb.ToString();
                case TriggerEntity.BackpackItemName:
                    if (iVal < 0 || iVal >= baseChar.BackpackItems.Count)
                        return "";
                    return baseChar.BackpackItems[(int)iVal].FormatDescription(Properties.Settings.Default.ItemFormat, baseChar.BasicAlignment.Temporary, baseChar.BasicClass);
                case TriggerEntity.TabTitle: return baseChar.Name;
                case TriggerEntity.TestedItem: return "";
                case TriggerEntity.CurrentAlignment: return BaseCharacter.AlignmentString(baseChar.BasicAlignment.Temporary);
                case TriggerEntity.ProperAlignment: return BaseCharacter.AlignmentString(baseChar.BasicAlignment.Permanent);

                case TriggerEntity.TimeDayOfMonth:
                case TriggerEntity.TimeDayOfWeek:
                case TriggerEntity.TimeDayOfYear:
                case TriggerEntity.TimeHour:
                case TriggerEntity.TimeMinute:
                case TriggerEntity.TimeYear:
                    return GameValue(hacker, entity);

                default: return LongValue(baseChar, entity, strVal).ToString();
            }
        }

        public static bool CheckTriggerCondition(MemoryHacker hacker, 
            List<BaseCharacter> allChars,
            BaseCharacter baseChar,
            CharacterTrigger trigger,
            TriggerCondition cond,
            out TriggerTarget triggerTarget)
        {
            triggerTarget = new TriggerTarget(cond.What, cond.WhatValue);

            if (baseChar is Wiz4Character && cond.What == TriggerEntity.ExperienceToNext)
                return false;   // Werdna's "experience" value is unrelated to his level

            long iSource = 0;
            long iTarget = 0;
            long iTargetBetween = 0;
            bool bValidSource = false;
            bool bValidTarget = false;
            bool bValidTargetBetween = false;

            bool bBetween = cond.When == TriggerWhen.IsBetween || cond.When == TriggerWhen.IsNotBetween;

            string strSource, strTarget, strTargetBetween;

            // If the trigger is for "entire party" but some of the entities are for per-character values
            // (e.g. the backpack item count, or hit points), then we add up all of the values of that entity
            // for the collection of characters.  This allows triggers to do things like activate if "the total
            // inventory free spaces" is at some level, regardless of which characters are actually carrying the
            // items.
            if (trigger.Who == TriggerWho.EntireParty && !cond.AllPartyValues(baseChar.Game))
            {
                if (trigger.Tested)
                    return false;     // No need to test a truly party-only trigger for every single character in the outer loop
                List<string> source = new List<string>(allChars.Count);
                List<string> target = new List<string>(allChars.Count);
                List<string> targetBetween = new List<string>(allChars.Count);
                foreach (BaseCharacter bc in allChars)
                {
                    source.Add(StringValue(hacker, bc, cond.What, cond.WhatValue));
                    target.Add(StringValue(hacker, bc, cond.Which, cond.WhichValue));
                    targetBetween.Add(StringValue(hacker, bc, cond.WhichBetween, cond.WhichValueBetween));
                }
                bValidSource = Global.SumInt64(source, out iSource);
                bValidTarget = Global.SumInt64(target, out iTarget);
                bValidTargetBetween = Global.SumInt64(targetBetween, out iTargetBetween);
                strSource = String.Join(",", source);
                strTarget = String.Join(",", target);
                strTargetBetween = String.Join(",", targetBetween);
            }
            else
            {
                strSource = StringValue(hacker, baseChar, cond.What, cond.WhatValue);
                strTarget = StringValue(hacker, baseChar, cond.Which, cond.WhichValue);
                strTargetBetween = StringValue(hacker, baseChar, cond.WhichBetween, cond.WhichValueBetween);
                bValidSource = Int64.TryParse(strSource, out iSource);
                bValidTarget = Int64.TryParse(strTarget, out iTarget);
                bValidTargetBetween = Int64.TryParse(strTargetBetween, out iTargetBetween);
            }

            trigger.Tested = true;

            bool bNumbers = bValidSource && bValidTarget && (!bBetween || bValidTargetBetween);

            switch (cond.When)
            {
                case TriggerWhen.Contains: return strSource.Contains(strTarget);
                case TriggerWhen.DoesNotContain: return !strSource.Contains(strTarget);
                case TriggerWhen.MatchesRegex: return Global.SafeRegexMatch(strSource, strTarget);
                case TriggerWhen.DoesNotMatchRegex: return !Global.SafeRegexMatch(strSource, strTarget);
                default:
                    break;
            }

            long iDiff = TriggerCondition.GetDifference(iTarget, cond.WhenDiff, cond.WhenValue);
            long iDiffBetween = bBetween ? TriggerCondition.GetDifference(iTargetBetween, cond.WhenDiffBetween, cond.WhenValueBetween) : 0;

            triggerTarget.BetweenMin = Math.Min(iDiff, iDiffBetween);
            triggerTarget.BetweenMax = Math.Max(iDiff, iDiffBetween);
            triggerTarget.TestedValue = iSource;

            return bNumbers ? TriggerCondition.Compare(cond.When, iSource, iDiff, iDiffBetween) : TriggerCondition.Compare(cond.When, strSource, strTarget, strTargetBetween);
        }

        private void CharacterInfoControl_Resize(object sender, EventArgs e)
        {
            SetInventorySize();
        }

        public void SetInventorySize(bool bInitial = false)
        {
            try
            {
                switch (Properties.Settings.Default.InventoryOrientation)
                {
                    case Orient.Horiz:
                        scInventory.Orientation = Orientation.Vertical;
                        Global.SetSplitterDistance(scInventory, scInventory.Width / 2);
                        break;
                    case Orient.Vert:
                        scInventory.Orientation = Orientation.Horizontal;
                        Global.SetSplitterDistance(scInventory, scInventory.Height / 2);
                        break;
                    default:    // Auto
                        if (Width > (2 * lvResistances.Right - 50) && scInventory.Orientation == Orientation.Horizontal)
                        {
                            scInventory.Orientation = Orientation.Vertical;
                            Global.SetSplitterDistance(scInventory, scInventory.Width / 2);
                        }
                        else if (Width <= (2 * lvResistances.Right - 50) && scInventory.Orientation == Orientation.Vertical)
                        {
                            scInventory.Orientation = Orientation.Horizontal;
                            Global.SetSplitterDistance(scInventory, scInventory.Height / 2);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                // If the window is too small, trying to set the splitter distances can cause pointless exceptions
            }

            Global.FitSingleColumn(lvEquipped);
            Global.FitSingleColumn(lvBackpack);
        }

        protected virtual CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None) { return CheatMenuFlags.None; }

        private void EditableAttributeLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Global.Cheats)
                return;

            if (e.Button != MouseButtons.Right)
                return;

            Point ptClient = PointToClient(e.Location);

            CheatMenuFlags menuFlags = CheatMenuFlags.None;

            if (sender is ListView)
            {
                menuFlags = PrepareCheatMenu(sender as ListView);
            }
            else
            {
                if (!(sender is EditableAttributeLabel || (sender is Label && ((Label)sender).Parent != null && ((Label)sender).Parent is MMItemLabel)))
                    return;
                menuFlags = PrepareCheatMenu(sender is EditableAttributeLabel ? sender as Control : ((Label)sender).Parent);
            }

            miCheatAdd1.Visible = menuFlags.HasFlag(CheatMenuFlags.Add1);
            miCheatSubtract1.Visible = menuFlags.HasFlag(CheatMenuFlags.Subtract1);
            miCheatMaximum.Visible = menuFlags.HasFlag(CheatMenuFlags.Maximum);
            miCheatMinimum.Visible = menuFlags.HasFlag(CheatMenuFlags.Minimum);
            miCheatNextLevel.Visible = menuFlags.HasFlag(CheatMenuFlags.NextLevel);
            miCheatEdit.Visible = menuFlags.HasFlag(CheatMenuFlags.Edit);
            miCheatCreateSupercharacter.Visible = menuFlags.HasFlag(CheatMenuFlags.SuperChar);

            cmCheat.Show(Cursor.Position);
        }

        private void ModifySelectedValue(CheatMenuFlags flags)
        {
            if (m_cheatOffsets == null)
                return;

            bool bSkipCharWrite = false;
            byte[] bytes = new byte[0];
            UInt16[] ushorts = new UInt16[0];
            Int16[] shorts = new Int16[0];
            UInt24[] int24s = new UInt24[0];
            Int32[] ints = new Int32[0];
            UInt32[] uints = new UInt32[0];
            long[] longs = new long[0];
            string[] strings = new string[0];
            MM2Item[] items = new MM2Item[0];
            long min = 0;
            long max = 0;
            CharBasicInfo basicInfo = null;
            DnDCharBasicInfo basicDnDInfo = null;

            // Downgrade certain attribute types if the offset arrays contain -1 values
            switch (m_cheatType)
            {
                case AttributeType.TwoUInt8:
                    if (m_cheatOffsets[1] < 0 || m_cheatOffsets[1] >= m_bytes.Length)
                        m_cheatType = AttributeType.UInt8;
                    break;
                case AttributeType.TwoUInt16:
                    if (m_cheatOffsets[1] < 0 || m_cheatOffsets[1] >= m_bytes.Length)
                        m_cheatType = AttributeType.UInt16;
                    break;
                case AttributeType.ThreeUInt16:
                    if (m_cheatOffsets[1] < 0 || m_cheatOffsets[1] >= m_bytes.Length)
                        m_cheatType = AttributeType.UInt16;
                    else if (m_cheatOffsets[2] < 0 || m_cheatOffsets[2] >= m_bytes.Length)
                        m_cheatType = AttributeType.TwoUInt16;
                    break;
                case AttributeType.ThreeInt32:
                    if (m_cheatOffsets[1] < 0 || m_cheatOffsets[1] >= m_bytes.Length)
                        m_cheatType = AttributeType.Int32;
                    else if (m_cheatOffsets[2] < 0 || m_cheatOffsets[2] >= m_bytes.Length)
                        m_cheatType = AttributeType.TwoInt32;
                    break;
                default:
                    break;
            }

            switch (m_cheatType)
            {
                case AttributeType.Strength18:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]], m_bytes[m_cheatOffsets[1]], m_bytes[m_cheatOffsets[2]], m_bytes[m_cheatOffsets[3]] };
                    break;
                case AttributeType.WizCondition:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    max = 7;
                    break;
                case AttributeType.WizSpellPoints:
                    shorts = new short[m_cheatOffsets.Length];
                    for (int i = 0; i < m_cheatOffsets.Length; i++)
                        shorts[i] = BitConverter.ToInt16(m_bytes, m_cheatOffsets[i]);
                    max = 9;
                    break;
                case AttributeType.Wiz5SpellPoints:
                    bytes = new byte[m_cheatOffsets.Length];
                    for (int i = 0; i < m_cheatOffsets.Length; i++)
                        bytes[i] = m_bytes[m_cheatOffsets[i]];
                    max = 9;
                    break;
                case AttributeType.StatMax18:
                    bytes = new byte[] { (byte)new PackedFiveBitValues(m_bytes, m_cheatOffsets[0]).Values[((WizardryCheatTag)m_cheatOffsets.Tag).StatOffset] };
                    max = 18;
                    break;
                case AttributeType.StatMax31:
                    bytes = new byte[] { (byte)new PackedFiveBitValues(m_bytes, m_cheatOffsets[0]).Values[((WizardryCheatTag)m_cheatOffsets.Tag).StatOffset] };
                    max = 31;
                    break;
                case AttributeType.SixByteLong:
                    longs = new long[] { new WizardryLong(m_bytes, m_cheatOffsets[0]).Number };
                    max = 999999999999;
                    break;
                case AttributeType.UInt8:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    break;
                case AttributeType.TwoUInt8:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]], m_bytes[m_cheatOffsets[1]] };
                    break;
                case AttributeType.UInt16:
                    ushorts = new UInt16[] { BitConverter.ToUInt16(m_bytes, m_cheatOffsets[0]) };
                    min = Int16.MinValue;
                    max = Int16.MaxValue;
                    break;
                case AttributeType.Int16:
                    shorts = new Int16[] { BitConverter.ToInt16(m_bytes, m_cheatOffsets[0]) };
                    min = Int16.MinValue;
                    max = Int16.MaxValue;
                    break;
                case AttributeType.Int8:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    min = sbyte.MinValue;
                    max = sbyte.MaxValue;
                    break;
                case AttributeType.TwoInt8:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]], m_bytes[m_cheatOffsets[1]] };
                    min = sbyte.MinValue;
                    max = sbyte.MaxValue;
                    break;
                case AttributeType.UInt24:
                    int24s = new UInt24[] { new UInt24(m_bytes[m_cheatOffsets[0]] | (m_bytes[m_cheatOffsets[0] + 1] << 8) | (m_bytes[m_cheatOffsets[0] + 2] << 16)) };
                    break;
                case AttributeType.Int32:
                    ints = new Int32[] { BitConverter.ToInt32(m_bytes, m_cheatOffsets[0]) };
                    break;
                case AttributeType.UInt32:
                    uints = new UInt32[] { BitConverter.ToUInt32(m_bytes, m_cheatOffsets[0]) };
                    break;
                case AttributeType.Int64:
                    longs = new long[] { BitConverter.ToInt64(m_bytes, m_cheatOffsets[0]) };
                    break;
                case AttributeType.TwoUInt16:
                    ushorts = new UInt16[] { BitConverter.ToUInt16(m_bytes, m_cheatOffsets[0]), BitConverter.ToUInt16(m_bytes, m_cheatOffsets[1]) };
                    break;
                case AttributeType.TwoInt32:
                    ints= new Int32[] { BitConverter.ToInt32(m_bytes, m_cheatOffsets[0]), BitConverter.ToInt32(m_bytes, m_cheatOffsets[1]) };
                    break;
                case AttributeType.TwoInt16:
                    shorts = new Int16[] { BitConverter.ToInt16(m_bytes, m_cheatOffsets[0]), BitConverter.ToInt16(m_bytes, m_cheatOffsets[1]) };
                    break;
                case AttributeType.ThreeUInt16:
                    ushorts = new UInt16[] { 
                        BitConverter.ToUInt16(m_bytes, m_cheatOffsets[0]),
                        BitConverter.ToUInt16(m_bytes, m_cheatOffsets[1]),
                        BitConverter.ToUInt16(m_bytes, m_cheatOffsets[2])
                    };
                    break;
                case AttributeType.ThreeInt32:
                    ints = new Int32[] {
                        BitConverter.ToInt32(m_bytes, m_cheatOffsets[0]),
                        BitConverter.ToInt32(m_bytes, m_cheatOffsets[1]),
                        BitConverter.ToInt32(m_bytes, m_cheatOffsets[2])
                    };
                    break;
                case AttributeType.Item:
                    if (m_cheatOffsets.Tag is WizardryCheatTag && ((WizardryCheatTag)m_cheatOffsets.Tag).Stat == PackedStat.BlackBox)
                    {
                        List<Item> box = ((Wiz4MemoryHacker)Hacker).GetBlackBox();
                        bytes = ((Wiz4Item) box[m_cheatOffsets[0]]).Serialize();
                    }
                    else
                    {
                        bytes = new byte[m_cheatOffsets.Length];
                        for (int i = 0; i < m_cheatOffsets.Length; i++)
                            bytes[i] = m_bytes[m_cheatOffsets[i]];
                    }
                    break;
                case AttributeType.LevSexAlignRaceClass:
                    // If we picked "add 1" or similar, only affect the level
                    if (flags != CheatMenuFlags.Edit)
                    {
                        if (m_char.Offsets.LevelLength == 2)
                        {
                            shorts = new short[] { BitConverter.ToInt16(m_bytes, m_cheatOffsets[0]),
                                BitConverter.ToInt16(m_bytes, m_cheatOffsets[1])};
                        }
                        else if (m_cheatOffsets[1] != -1)
                        {
                            bytes = new byte[] { m_bytes[m_cheatOffsets[0]], m_bytes[m_cheatOffsets[1]] };
                        }
                        else
                        {
                            bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                        }
                    }
                    else if (Games.IsEyeOfTheBeholder(m_char.Game))
                    {
                        basicDnDInfo = new DnDCharBasicInfo(
                            Properties.Settings.Default.Game,
                            m_char.Name,
                            ((EOBCharacter) m_char).Level,
                            m_char.BasicSex,
                            m_char.BasicAlignment.Permanent,
                            m_char.BasicRace,
                            m_char.BasicClass);
                    }
                    else
                        basicInfo = new CharBasicInfo(
                            Properties.Settings.Default.Game,
                            m_char.Name,
                            m_char.BasicLevel,
                            m_char.BasicSex,
                            m_char.BasicAlignment,
                            m_char.BasicRace,
                            m_char.BasicClass,
                            m_char.BasicAge);
                    break;
                case AttributeType.KnownSpells:
                    if (m_char is MM345BaseCharacter)
                    {
                        bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    }
                    else
                    {
                        bytes = new byte[m_cheatOffsets.Length];
                        for(int i = 0; i < m_cheatOffsets.Values.Length; i++)
                            bytes[i] = m_bytes[m_cheatOffsets[i]];
                    }
                    break;
                case AttributeType.MapAndPosition:
                    if (m_char is MM3Character)
                    {
                        bytes = new byte[] {
                            m_bytes[m_cheatOffsets[0]], 
                            m_bytes[m_cheatOffsets[1]], 
                            m_bytes[m_cheatOffsets[2]] 
                        };
                    }
                    else if (m_char is MM45Character)
                    {
                        bytes = new byte[] {
                            m_bytes[m_cheatOffsets[0]], 
                            m_bytes[m_cheatOffsets[1]], 
                            m_bytes[m_cheatOffsets[2]],
                            m_bytes[m_cheatOffsets[3]] 
                        };
                    }
                    break;
                case AttributeType.Condition:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    break;
                case AttributeType.MM1Worthy:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    break;
                case AttributeType.MM1Castle:
                    bytes = new byte[6];
                    for (int iCastle = 0; iCastle < 6; iCastle++)
                        bytes[iCastle] = m_bytes[m_cheatOffsets[iCastle]];
                    break;
                case AttributeType.MM1Main:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]], m_bytes[m_cheatOffsets[1]]};
                    break;
                case AttributeType.SecondarySkills:
                    bytes = new byte[] { m_bytes[80] };
                    break;
                case AttributeType.ReadySpell:
                    bytes = new byte[] { m_bytes[m_cheatOffsets[0]] };
                    break;
                default:
                    break;
            }

            CheatTag tag = m_cheatOffsets.Tag as CheatTag;
            if (min == 0 && max == 0 && tag != null)
            {
                min = tag.Minimum;
                max = tag.Maximum;
            }

            // Only one flag at a time is acceptable here
            switch (flags)
            {
                case CheatMenuFlags.Add1:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < bytes.Length && (tag == null || bytes[i] < tag.ByteMax))
                            bytes[i]++;
                        if (i < shorts.Length && (tag == null || shorts[i] < tag.Int16Max))
                            shorts[i]++;
                        if (i < ushorts.Length && (tag == null || ushorts[i] < tag.UInt16Max))
                            ushorts[i]++;
                        if (i < int24s.Length && (tag == null || int24s[i] < tag.Int32Max))
                            int24s[i].Value++;
                        if (i < ints.Length && (tag == null || ints[i] < tag.Int32Max))
                            ints[i]++;
                        if (i < uints.Length && (tag == null || uints[i] < tag.UInt32Max))
                            uints[i]++;
                        if (i < longs.Length && (tag == null || longs[i] < tag.Maximum))
                            longs[i]++;
                        if (m_char is MM345BaseCharacter)
                            break;  // MM3/4/5 character records behave better with "add 1" if only the first value is changed
                    }
                    break;
                case CheatMenuFlags.Subtract1:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < bytes.Length && (tag == null || bytes[i] > tag.ByteMin))
                            bytes[i]--;
                        if (i < shorts.Length && (tag == null || shorts[i] > tag.Int16Min))
                            shorts[i]--;
                        if (i < ushorts.Length && (tag == null || ushorts[i] > tag.UInt16Min))
                            ushorts[i]--;
                        if (i < int24s.Length && (tag == null || int24s[i] > tag.Int32Min))
                            int24s[i].Value--;
                        if (i < ints.Length && (tag == null || ints[i] > tag.Int32Min))
                            ints[i]--;
                        if (i < uints.Length && (tag == null || uints[i] > tag.UInt32Min))
                            uints[i]--;
                        if (i < longs.Length && (tag == null || longs[i] > tag.Minimum))
                            longs[i]--;
                        if (m_char is MM345BaseCharacter)
                            break;  // MM3/4/5 character records behave better with "subtract 1" if only the first value is changed
                    }
                    break;
                case CheatMenuFlags.SuperChar:
                    // This cheat is very specific and doesn't use the general formula of value arrays
                    if (MessageBox.Show(Hacker.ConfirmSuperCharMessage, 
                        "Create Super-Character?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        m_cheatOffsets = null;
                        return;
                    }
                    Hacker.CreateSuperCharacter(CharacterAddress);
                    return;
                case CheatMenuFlags.Maximum:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < bytes.Length)
                            bytes[i] = tag == null ? (byte) 0xff : tag.ByteMax;
                        if (i < shorts.Length)
                            shorts[i] = tag == null ? Int16.MaxValue : tag.Int16Max;
                        if (i < ushorts.Length)
                            ushorts[i] = tag == null ? UInt16.MaxValue : tag.UInt16Max;
                        if (i < int24s.Length)
                            int24s[i].Value = tag == null ? 0xffffff : tag.Int16Max;
                        if (i < ints.Length)
                            ints[i] = tag == null ? Int32.MaxValue : tag.Int32Max;
                        if (i < uints.Length)
                            uints[i] = tag == null ? UInt32.MaxValue : tag.UInt32Max;
                        if (i < longs.Length)
                            longs[i] = tag == null ? Int64.MaxValue : tag.Maximum;
                        if (m_char is MM345BaseCharacter)
                            break;  // MM3/4/5 character records behave better with "maximum" if only the first value is changed
                    }
                    break;
                case CheatMenuFlags.Minimum:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < bytes.Length)
                            bytes[i] = tag == null ? (byte) 0 : tag.ByteMin;
                        if (i < shorts.Length)
                            shorts[i] = tag == null ? (byte)0 : tag.Int16Min;
                        if (i < ushorts.Length)
                            ushorts[i] = tag == null ? (byte)0 : tag.UInt16Min;
                        if (i < int24s.Length)
                            int24s[i].Value = tag == null ? (byte)0 : tag.Int16Min;
                        if (i < ints.Length)
                            ints[i] = tag == null ? (byte)0 : tag.Int32Min;
                        if (i < uints.Length)
                            uints[i] = tag == null ? (byte)0 : tag.UInt32Min;
                        if (i < longs.Length)
                            longs[i] = tag == null ? (byte)0 : tag.Minimum;
                        if (m_char is MM345BaseCharacter)
                            break;  // MM3/4/5 character records behave better with "minimum" if only the first value is changed
                    }
                    break;
                case CheatMenuFlags.NextLevel:
                    // Only valid for Experience (i.e. Int32)
                    if (m_char is MM345BaseCharacter)
                    {
                        // In MM3, the experience bytes are the amount the character has in addition
                        // to the amount they are required to have for the level they are, not the absolute total.
                        uints[0] = (UInt32)(m_char.XPForNextLevel - m_char.XPForCurrentLevel);
                    }
                    else if (m_char is WizardryBaseCharacter)
                    {
                        // Wizardry holds the Experience as a 6-byte triple-short
                        longs[0] = m_char.XPForNextLevel;
                    }
                    else if (m_char is EOBCharacter)
                    {
                        // Eye of the Beholder uses multi-classes and may need multiple next XP values
                        EOBCharacter eobChar = m_char as EOBCharacter;
                        EOBClass[] classes = EOBCharacter.SeparateClasses(eobChar.Class);
                        for(int i = 0; i < classes.Length; i++)
                            ints[i] = (int) eobChar.GetXPForNextLevel(eobChar.Experience[i], classes[i]);
                    }
                    else
                        uints[0] = (UInt32)m_char.XPForNextLevel;
                    break;
                case CheatMenuFlags.Edit:
                    MM345BaseCharacter mm345Char = m_char as MM345BaseCharacter;
                    switch (m_cheatType)
                    {
                        case AttributeType.Strength18:
                            EditStrength18Form formStrength18 = new EditStrength18Form();
                            formStrength18.Attributes = new StrengthWith18[] { new StrengthWith18(bytes[0], bytes[2]), new StrengthWith18(bytes[1], bytes[3]) };
                            if (formStrength18.ShowDialog() == DialogResult.OK)
                            {
                                bytes = new byte[] {
                                    (byte) formStrength18.Attributes[0].Strength,
                                    (byte) formStrength18.Attributes[1].Strength,
                                    (byte) formStrength18.Attributes[0].Strength18,
                                    (byte) formStrength18.Attributes[1].Strength18
                                };
                            }
                            break;
                        case AttributeType.MapAndPosition:
                            if (mm345Char != null)
                            {
                                MapSelectionForm form = new MapSelectionForm();
                                form.SetMain(m_main, WindowType.MapSelection);
                                form.Exit = new MMExit(MMExitDirection.Beacon, (int)mm345Char.Beacon.Map, mm345Char.Beacon.Position);
                                if (form.ShowDialog() == DialogResult.OK)
                                    bytes = new byte[] { (byte)form.Exit.Map, (byte)form.Exit.Point.X, (byte)form.Exit.Point.Y, (byte)(form.Exit.Map / 256) };
                            }
                            break;
                        case AttributeType.KnownSpells:
                            if (mm345Char != null)
                            {
                                KnownSpellsEditForm formSpells = new KnownSpellsEditForm();
                                formSpells.SetSpells(mm345Char.Spells, mm345Char.BasicClass);
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSpells.Spells.GetBytes();
                            }
                            else if (m_char is MM2Character)
                            {
                                MM2KnownSpellsEditForm formSpells = new MM2KnownSpellsEditForm();
                                formSpells.Attribute = new EditableAttribute(bytes, ushorts, shorts, int24s, uints, ints, longs, strings, items);
                                formSpells.Sorcerer = !m_char.IsHealer;
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                            }
                            else if (m_char is WizCharacter)
                            {
                                KnownSpellsEditForm formSpells = new KnownSpellsEditForm();
                                formSpells.SetSpells(((WizCharacter)m_char).SpellBook, m_char.BasicClass);
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSpells.Spells.GetBytes();
                            }
                            else if (m_char is BT3Character)
                            {
                                KnownSpellsEditForm formSpells = new KnownSpellsEditForm();
                                formSpells.SetSpells(((BT3Character)m_char).Spells, m_char.BasicClass);
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSpells.Spells.GetBytes();
                            }
                            else if (m_char is BTCharacter)
                            {
                                EditBTSpellLevels formSpells = new EditBTSpellLevels(((BTCharacter)m_char).SpellLevel);
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSpells.SpellLevel.GetBytes();
                            }
                            else if (m_char is EOBCharacter)
                            {
                                EditDnDSpellsForm formSpells = new EditDnDSpellsForm();
                                formSpells.SetSpells(((EOBCharacter)m_char).Spells, m_char);
                                if (formSpells.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSpells.Spells.GetBytes();
                            }
                            break;
                        case AttributeType.SecondarySkills:
                            if (mm345Char != null)
                            {
                                MMSkillEditForm formSkill = new MMSkillEditForm(Properties.Settings.Default.Game);
                                formSkill.Skills = mm345Char.Skills;
                                if (formSkill.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = formSkill.Skills.GetBytes();
                            }
                            else
                            {
                                MM2SecondarySkillForm formSkills = new MM2SecondarySkillForm();
                                formSkills.SkillByte = bytes[0];
                                if (formSkills.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes[0] = formSkills.SkillByte;
                            }
                            break;
                        case AttributeType.ReadySpell:
                            if (mm345Char != null)
                            {
                                MMSpellSelectForm formSpellSelect = new MMSpellSelectForm(m_char, Global.Cheats);
                                if (mm345Char is MM45Character)
                                    formSpellSelect.SpellIndex = new MMInternalSpellIndex(mm345Char.ReadySpell, mm345Char.CasterType);
                                else
                                    formSpellSelect.SpellIndex = new MMInternalSpellIndex((MM3InternalSpellIndex)mm345Char.ReadySpell);
                                if (formSpellSelect.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes[0] = (byte)formSpellSelect.SpellIndex.CorrectedIndex;
                            }
                            break;
                        case AttributeType.Condition:
                            if (mm345Char != null)
                            {
                                MM3ConditionEditForm formCondition = new MM3ConditionEditForm(Properties.Settings.Default.Game);
                                formCondition.Condition = mm345Char.Condition;
                                formCondition.Protection = mm345Char.Protection;
                                if (formCondition.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes = MM3Character.GetConditionProtectionBytes(formCondition.Condition, formCondition.Protection);
                            }
                            else
                            {
                                ConditionEditForm formCondition = new ConditionEditForm(Properties.Settings.Default.Game);
                                formCondition.BasicCondition = m_char.BasicCondition;
                                if (formCondition.ShowDialog() == DialogResult.Cancel)
                                    return;
                                bytes[0] = m_char.ConditionValue(formCondition.BasicCondition);
                            }
                            break;
                        case AttributeType.MM1Worthy:
                            EditBitsForm formWorthy = new EditBitsForm(MM1Bits.WorthyDescription, this);
                            formWorthy.SetMain(m_main, WindowType.EditBits);
                            formWorthy.Bytes = bytes;
                            if (formWorthy.ShowDialog() == DialogResult.Cancel)
                                return;
                            bytes = formWorthy.Bytes;
                            break;
                        case AttributeType.MM1Castle:
                            EditBitsForm formCastle = new EditBitsForm(MM1Bits.CastleDescription, this);
                            formCastle.SetMain(m_main, WindowType.EditBits);
                            formCastle.Bytes = bytes;
                            if (formCastle.ShowDialog() == DialogResult.Cancel)
                                return;
                            bytes = formCastle.Bytes;
                            break;
                        case AttributeType.MM1Main:
                            EditBitsForm formMain = new EditBitsForm(MM1Bits.MainDescription, this);
                            formMain.SetMain(m_main, WindowType.EditBits);
                            formMain.Bytes = bytes;
                            if (formMain.ShowDialog() == DialogResult.Cancel)
                                return;
                            bytes = formMain.Bytes;
                            break;
                        case AttributeType.Item:
                            if (m_char is MM45Character)
                            {
                                // MM4/5 items are special, since the inventory is divided into four categories that can't be overlapped
                                // We are editing a single item, so we can't change the item type (it would be a delete and add, which
                                // is possible but not implemented)
                                InventoryItemCounts info = m_cheatOffsets.Tag as InventoryItemCounts;
                                MM45Item item = MM45Item.Create(bytes, info.ItemType, 0);
                                MM45ItemEditForm formItem = new MM45ItemEditForm(m_char.Game);
                                formItem.AllowWeapons = (item.Base.Type == ItemType.Weapon);
                                formItem.AllowArmor = (item.Base.Type == ItemType.Armor);
                                formItem.AllowAccessories = (item.Base.Type == ItemType.Accessory);
                                formItem.AllowMisc = (item.Base.Type == ItemType.Miscellaneous);
                                formItem.Item = item;
                                if (formItem.ShowDialog() == DialogResult.Cancel)
                                    return;
                                item = formItem.Item as MM45Item;
                                bytes = item.GetMemoryBytes();
                            }
                            else if (m_char is EOBCharacter)
                            {
                                bytes = EditEOBItems(bytes);
                            }
                            else if (m_char is UltimaCharacter)
                            {
                                bytes = EditUltimaItems(bytes);
                                bSkipCharWrite = true;
                            }
                            else
                            {
                                Item item = m_char.GetItem(bytes, m_char is Wiz5Character ? bytes.Length - 4 : m_char is WizCharacter ? bytes.Length - 8 : 0);
                                IEditableItemForm formItem = (item is MM3Item ? (IEditableItemForm)new MM3ItemEditForm(m_char.Game) : (IEditableItemForm)new ItemEditForm(m_char.Game));
                                formItem.Item = item;
                                if (formItem.ShowDialog() == DialogResult.Cancel)
                                    return;
                                item = formItem.Item;
                                if (m_char is WizCharacter && !(m_cheatOffsets.Tag is WizardryCheatTag && ((WizardryCheatTag)m_cheatOffsets.Tag).Stat == PackedStat.BlackBox))
                                {
                                    byte[] bytesNew = item.Serialize();
                                    Buffer.BlockCopy(bytesNew, 0, bytes, 1, bytesNew.Length);
                                }
                                else
                                    bytes = item.Serialize();
                            }
                            break;
                        case AttributeType.LevSexAlignRaceClass:
                            if (Games.IsEyeOfTheBeholder(m_char.Game) && basicDnDInfo != null)
                            {
                                EditDndBasicCharInfoForm formBasic = new EditDndBasicCharInfoForm();
                                formBasic.BasicInfo = basicDnDInfo;
                                if (formBasic.ShowDialog() == DialogResult.Cancel)
                                    return;
                                basicDnDInfo = formBasic.BasicInfo;
                            }

                            if (basicInfo != null)
                            {
                                BasicCharInfoEditForm formBasic = new BasicCharInfoEditForm();
                                formBasic.BasicInfo = basicInfo;
                                if (formBasic.ShowDialog() == DialogResult.Cancel)
                                    return;
                                basicInfo = formBasic.BasicInfo;
                            }
                            break;
                        case AttributeType.WizCondition:
                            EnumSelectionForm formEnum = new EnumSelectionForm();
                            formEnum.SetValue(bytes[0], Enum.GetNames(typeof(WizCondition)), "Change Condition");
                            if (formEnum.ShowDialog() == DialogResult.Cancel)
                                return;
                            bytes[0] = (byte) formEnum.GetValue();
                            break;
                        case AttributeType.WizSpellPoints:
                            EditWizSpellPointsForm formSP = new EditWizSpellPointsForm();
                            formSP.SetSP((m_char as WizCharacter).SpellPoints);
                            if (formSP.ShowDialog() == DialogResult.Cancel)
                                return;
                            shorts = formSP.GetSP();
                            break;
                        case AttributeType.Wiz5SpellPoints:
                            EditWizSpellPointsForm formWiz5SP = new EditWizSpellPointsForm();
                            formWiz5SP.SetSP((m_char as Wiz5Character).SpellPoints);
                            if (formWiz5SP.ShowDialog() == DialogResult.Cancel)
                                return;
                            bytes = formWiz5SP.GetWiz5SP();
                            break;
                        default:
                            AttributeEditForm formAttr = new AttributeEditForm();
                            formAttr.Attribute = new EditableAttribute(bytes, ushorts, shorts, int24s, uints, ints, longs, strings, items, min, max);
                            if (formAttr.ShowDialog() == DialogResult.Cancel)
                                return;
                            break;
                    }
                    break;
                case CheatMenuFlags.AddNew:
                    switch (m_cheatType)
                    {
                        case AttributeType.Item:
                            if (m_char is MM45Character)
                            {
                                // MM4/5 items are special, since the inventory is divided into four categories that can't be overlapped
                                // We are adding an item, so we can allow changing the item type if there is space in the backpack for it
                                MM45Item item = null;
                                InventoryItemCounts info = m_cheatOffsets.Tag as InventoryItemCounts;
                                MM45ItemEditForm formItem = new MM45ItemEditForm(m_char.Game);
                                formItem.AllowWeapons = info.Weapons < 9;
                                formItem.AllowArmor = info.Armor < 9;
                                formItem.AllowAccessories = info.Accessories < 9;
                                formItem.AllowMisc = info.Miscellaneous < 9;
                                formItem.Text = "Create Item";
                                if (!formItem.AnyAllowed)
                                {
                                    MessageBox.Show("There is no space in this backpack for any more items!", "Insufficient Space", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                formItem.Item = null;
                                if (formItem.ShowDialog() == DialogResult.Cancel)
                                    return;
                                item = formItem.Item as MM45Item;
                                switch (((MM45Item)item).Base.Type)
                                {
                                    case ItemType.Weapon:
                                        m_cheatOffsets = m_cheatOffsets.Subset(0, 4);
                                        break;
                                    case ItemType.Armor:
                                        m_cheatOffsets = m_cheatOffsets.Subset(4, 4);
                                        break;
                                    case ItemType.Accessory:
                                        m_cheatOffsets = m_cheatOffsets.Subset(8, 4);
                                        break;
                                    case ItemType.Miscellaneous:
                                        m_cheatOffsets = m_cheatOffsets.Subset(12, 4);
                                        break;
                                }
                                bytes = item.GetMemoryBytes();
                            }
                            else if (m_char is EOBCharacter)
                            {
                                bytes = EditEOBItems(null);
                            }
                            else if (m_char is UltimaCharacter)
                            {
                                bytes = EditUltimaItems(null);
                                bSkipCharWrite = true;
                            }
                            else
                            {
                                Item item = null;
                                IEditableItemForm formItem = (m_char.Game == GameNames.MightAndMagic3 ? (IEditableItemForm)new MM3ItemEditForm(m_char.Game) : (IEditableItemForm)new ItemEditForm(m_char.Game));
                                formItem.Item = item;
                                if (formItem.ShowDialog() == DialogResult.Cancel)
                                    return;
                                item = formItem.Item;
                                if (item is WizItem)
                                {
                                    if (bytes[0] >= Hacker.MaxBackpackSize)
                                    {
                                        MessageBox.Show("There is no space in this backpack for any more items!", "Insufficient Space", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    // Have to change the inventory count, which is the first byte
                                    byte[] bytesNew = item.Serialize();
                                    bytes[0]++;
                                    Buffer.BlockCopy(bytesNew, 0, bytes, 1, bytesNew.Length);
                                }
                                else
                                {
                                    bytes = item.Serialize();
                                    if (m_char.Game == GameNames.BardsTale3)
                                        BT3Item.SetUsableBit(bytes, m_char.BasicClass);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
            }

            if (basicInfo != null)
            {
                byte[] nameBytes = basicInfo.GetNameBytes();
                if (m_cheatOffsets[2] != -1)
                    Buffer.BlockCopy(nameBytes, 0, m_bytes, m_cheatOffsets[2], nameBytes.Length);
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[3], m_char.SexValue(basicInfo.CharSex));
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[5], m_char.AlignmentValue(basicInfo.CharAlignment.Permanent));
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[6], m_char.RaceValue(basicInfo.CharRace));
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[7], m_char.ClassValue(basicInfo.CharClass));

                switch (m_char.InfoStyle)
                {
                    case BasicInfoStyle.PermanentWithTemp:  // MM1/2
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[0], (byte)basicInfo.Level.Permanent);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[1], (byte)basicInfo.Level.Temporary);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[4], m_char.AlignmentValue(basicInfo.CharAlignment.Temporary));
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[9], (byte)basicInfo.Age.Days);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[11], (byte)basicInfo.Age.Years);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[12], (byte)basicInfo.Age.Days);
                        break;
                    case BasicInfoStyle.PermanentWithMod:  // MM3
                    case BasicInfoStyle.PermanentWithModNoAlign:  // MM4-5
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[0], (byte)basicInfo.Level.Permanent);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[1], (byte)basicInfo.Level.Modifier);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[9], (byte)basicInfo.Age.Days);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[10], (byte)basicInfo.Age.Modifier);
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[13], (byte)(MMBaseCharacter.IsCasterClass(basicInfo.CharClass) ? 1 : 0));
                        Buffer.BlockCopy(BitConverter.GetBytes(basicInfo.Age.Years), 0, m_bytes, m_cheatOffsets[8], 2);
                        break;
                    case BasicInfoStyle.AgeInWeeks:  // Wiz1-5
                        Global.SetInt16(m_bytes, m_cheatOffsets[0], basicInfo.Level.Permanent);
                        Global.SetInt16(m_bytes, m_cheatOffsets[1], basicInfo.Level.Temporary);
                        Global.SetInt16(m_bytes, m_cheatOffsets[11], (basicInfo.Age.Years * 52) + (basicInfo.Age.Days / 7));
                        break;
                    case BasicInfoStyle.NoAgeOrAlign:  // BT1-2
                    case BasicInfoStyle.SexNoAgeAlign:  // BT3
                        if (!(m_char is BT2Character))
                        {
                            Global.SetInt16(m_bytes, m_cheatOffsets[0], basicInfo.Level.Permanent);
                            Global.SetInt16(m_bytes, m_cheatOffsets[1], basicInfo.Level.Temporary);
                        }
                        else
                        {
                            m_bytes[m_cheatOffsets[0]] = (byte)basicInfo.Level.Permanent;
                            m_bytes[m_cheatOffsets[1]] = (byte)basicInfo.Level.Temporary;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (basicDnDInfo != null)
            {
                byte[] nameBytes = basicDnDInfo.GetNameBytes();
                if (m_cheatOffsets[2] != -1)
                    Buffer.BlockCopy(nameBytes, 0, m_bytes, m_cheatOffsets[2], nameBytes.Length);
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[5], m_char.AlignmentValue(basicDnDInfo.CharAlignment));
                if (Games.IsEyeOfTheBeholder(m_char.Game))
                {
                    Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[6], EOBCharacter.GetRaceByte(basicDnDInfo.CharRace, basicDnDInfo.CharSex));
                }
                Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[7], m_char.ClassValue(basicDnDInfo.CharClass));
                if (m_cheatOffsets[0] != -1)
                {
                    m_bytes[m_cheatOffsets[0]] = (byte)basicDnDInfo.Levels[0];
                    m_bytes[m_cheatOffsets[0]+1] = (byte)basicDnDInfo.Levels[1];
                    m_bytes[m_cheatOffsets[0]+2] = (byte)basicDnDInfo.Levels[2];
                }
            }
            else if (m_cheatOffsets.Tag is WizardryCheatTag && ((WizardryCheatTag)m_cheatOffsets.Tag).Stat == PackedStat.BlackBox)
            {
                ((Wiz4MemoryHacker)Hacker).SetBlackBox(m_cheatOffsets[0], BitConverter.ToInt16(bytes, 6));
            }
            else
            {
                // Set the game bytes to the modified values
                for (int i = 0; i < m_cheatOffsets.Length; i++)
                {
                    if (i < bytes.Length)
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[i], bytes[i], m_cheatOffsets.Tag);
                    if (i < shorts.Length)
                        Buffer.BlockCopy(BitConverter.GetBytes(shorts[i]), 0, m_bytes, m_cheatOffsets[i], 2);
                    if (i < ushorts.Length)
                        Buffer.BlockCopy(BitConverter.GetBytes(ushorts[i]), 0, m_bytes, m_cheatOffsets[i], 2);
                    if (i < int24s.Length)
                        Buffer.BlockCopy(BitConverter.GetBytes(int24s[i].Value), 0, m_bytes, m_cheatOffsets[i], 3);
                    if (i < ints.Length)
                        Buffer.BlockCopy(BitConverter.GetBytes(ints[i]), 0, m_bytes, m_cheatOffsets[i], 4);
                    if (i < uints.Length)
                        Buffer.BlockCopy(BitConverter.GetBytes(uints[i]), 0, m_bytes, m_cheatOffsets[i], 4);
                    if (i < longs.Length)
                    {
                        if (m_cheatOffsets.Tag is WizardryCheatTag && ((WizardryCheatTag)m_cheatOffsets.Tag).IsWizLong)
                        {
                            byte[] bytesWizLong = new WizardryLong(longs[i]).Bytes;
                            Buffer.BlockCopy(bytesWizLong, 0, m_bytes, m_cheatOffsets[i], bytesWizLong.Length);
                        }
                        else
                            Buffer.BlockCopy(BitConverter.GetBytes(longs[i]), 0, m_bytes, m_cheatOffsets[i], 8);
                    }
                    if (i < strings.Length)
                        Buffer.BlockCopy(Encoding.ASCII.GetBytes(strings[i]), 0, m_bytes, m_cheatOffsets[i], strings[i].Length);
                    if (i < items.Length)
                    {
                        Global.CheckRangeAndSet(m_bytes, m_cheatOffsets[i], (byte)items[i].Index);
                        m_bytes[m_cheatOffsets[i] + 6] = (byte)items[i].BonusCurrent;
                        m_bytes[m_cheatOffsets[i] + 12] = items[i].m_iChargesCurrent;
                    }
                }
            }

            if (!bSkipCharWrite)
                Hacker.SetCharacterBytes(CharacterAddress, m_bytes);

            m_cheatOffsets = null;
        }

        private byte[] EditEOBItems(byte[] bytesItem)
        {
            // Eye of the Beholder stores its inventory items as pointers into a master item table, which 
            // needs a completely different editing mechanism (more like the bag of holding) to change items around.
            EditInventoryTableForm form = new EditInventoryTableForm();
            form.Character = m_char;
            form.MasterItemTable = (Hacker as EOBMemoryHacker).GetItemTable();
            if (bytesItem != null)
                form.SelectedItem = BitConverter.ToInt16(bytesItem, 0);
            else
                form.SelectedItem = -1;
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Save new inventory and master item table
                EOBMemoryHacker eobHacker = Hacker as EOBMemoryHacker;
                if (eobHacker != null)
                {
                    eobHacker.SetMasterItemTable(form.MasterItemTable);
                    byte[] bytesBackpack = form.GetBackpackBytes();
                    Buffer.BlockCopy(bytesBackpack, 0, m_bytes, EOB.Offsets.Inventory, bytesBackpack.Length);
                    m_formParty.ForceUpdate();  // Because the master item list may have changed
                }
            }
            return new byte[0]; // We set the entire backpack directly in this function; no need to update the cheat values later
        }

        private byte[] EditUltimaItems(byte[] bytesItem)
        {
            UltimaItemEditForm form = new UltimaItemEditForm();
            UltimaCharacter ultimaChar = m_char as UltimaCharacter;
            form.Inventory = ultimaChar.Inventory;

            if (form.ShowDialog() == DialogResult.OK)
            {
                // Save new inventory and master item table
                UltimaMemoryHacker ultimaHacker = Hacker as UltimaMemoryHacker;
                if (ultimaHacker != null)
                {
                    UltimaInventory inv = form.Inventory;
                    ultimaHacker.SetInventory(inv);
                }
            }
            return new byte[0]; // We set the entire backpack directly in this function; no need to update the cheat values later
        }

        private void miCheatSubtract1_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.Subtract1);
        }

        private void miCheatAdd1_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.Add1);
        }

        private void miCheatMinimum_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.Minimum);
        }

        private void miCheatMaximum_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.Maximum);
        }

        private void miCheatNextLevel_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.NextLevel);
        }

        private void miCheatEdit_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.Edit);
        }

        private void btnCureAll_Click(object sender, EventArgs e)
        {
            CureAll_Clicked();
        }

        protected virtual void CureAll_Clicked()
        {
            if (!m_char.IsHealer)
            {
                MessageBox.Show(String.Format("The selected character is a {0} and only healers can cure conditions",
                    BaseCharacter.ClassString(m_char.BasicClass)), "Wrong Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_main.CureAll(m_iCharacterAddress, false);
        }

        private void cmBackpack_Opening(object sender, CancelEventArgs e)
        {
            bool bWrite = Properties.Settings.Default.EnableMemoryWrite;
            bool bCheat = Global.Cheats;
            bool bBackpack = lvBackpack.Focused;
            Item itemSelectedBackpack = lvBackpack.SelectedItems.Count > 0 ? (lvBackpack.SelectedItems[0].Tag as InventoryItemTag)?.Item : null;
            Item itemSelectedEquip = lvEquipped.SelectedItems.Count > 0 ? (lvEquipped.SelectedItems[0].Tag as InventoryItemTag)?.Item : null;

            bool bSelectedBackpack = (bBackpack && itemSelectedBackpack != null);
            bool bSelectedEquip = (lvEquipped.Focused && lvEquipped.FocusedItem != null && lvEquipped.SelectedItems.Count > 0);

            m_formParty.FillBackpackTradeMenu(miBackpackTrade, this);

            miBackpackDebugMonitor.Visible = Global.Debug && bBackpack;
            miBackpackDebugClearAll.Visible = Global.Debug && bBackpack;
            miBackpackDropTrash.Visible = bBackpack && bWrite;
            miBackpackBagofHolding.Visible = bBackpack && bWrite;
            miBackpackTrade.Visible = bBackpack && bWrite;
            miBackpackFillRandom.Visible = bCheat && bBackpack;
            miBackpackAdd.Visible = bCheat && bBackpack;
            miBackpackEdit.Visible = bCheat && (bSelectedBackpack || bSelectedEquip);
            miBackpackDuplicate.Visible = bCheat && (bSelectedBackpack || bSelectedEquip);
            miBackpackClearAll.Visible = bCheat && bBackpack;
            miBackpackDeleteItem.Visible = bCheat && bSelectedBackpack;
            miBackpackStackCharges.Visible = bCheat && bSelectedBackpack && itemSelectedBackpack.ChargeBased;
        }

        private void miBackpackBagofHolding_Click(object sender, EventArgs e)
        {
            LocationInformation info = Hacker.GetLocation();

            if (Hacker is Wiz4MemoryHacker || Hacker is EOBMemoryHacker)
            {
                string strExtra = String.Empty;
                if (!Global.Cheats)
                    strExtra = "However, if you enable cheating in the options, you may add and remove items as you wish via the backpack context menu.";
                else
                    strExtra = "However, you may still add and remove items as you wish via the backpack context menu.";
                MessageBox.Show(String.Format("{0} does not have a roster of characters, so there is no Bag of Holding.\r\n\r\n{1}", Games.ShortName(Hacker.Game), strExtra),
                    "No roster in this game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // MM3/4/5 are special
            if (Hacker is MM3MemoryHacker || Hacker is MM45MemoryHacker)
            {
                GameState gameState = Hacker.GetGameState();
                if (gameState.Main == MainState.Inn || info.InInn)
                {
                    MessageBox.Show("Due to technical annoyances, it is not safe for your items to use the Bag of Holding while in (or in front of) the Inn.  Please exit the Inn and try again from elsewhere.",
                        "Can't use bag from the sign-in screen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!Global.Cheats && Hacker.ActiveMonstersNearby)
                {
                    MessageBox.Show("You may not use the Bag of Holding if there are active monsters nearby",
                        "Can't use bag with monsters nearby", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (!info.CanUseBag && !Global.Cheats)
            {
                MessageBox.Show(String.Format("The party must be {0} to use the Bag of Holding!", Hacker.BagOfHoldingRequirement),
                    String.Format("Not {0}", Hacker.BagOfHoldingRequirement)
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!info.CanUseBag)
            {
                MessageBox.Show(String.Format("For technical reasons, the bag of holding only works while {0} in {1} (even if cheats are enabled).", Hacker.BagOfHoldingRequirement, Games.Name(Hacker.Game)),
                    String.Format("Not {0}", Hacker.BagOfHoldingRequirement)
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (m_main.PartyForm.GetCharacters().Any(b => b.Name.ToLower() == "inventory"))
            {
                MessageBox.Show("You may not use the Bag of Holding if you have \"Inventory\" characters in your party.\r\n\r\n" +
                    "They are the characters that hold the items in the bag, and should really never be in the party.",
                    "Shouldn't be using Inventory characters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Hacker.BagNeedsRosterFile && !Hacker.ValidateRosterFile())
                return;

            InventoryManipulatorForm form = new InventoryManipulatorForm();
            form.SetMain(m_main, WindowType.InventoryManipulator);
            form.MaxBagItems = Hacker.GetMaxBagItems();
            List<BaseCharacter> chars = m_formParty.GetCharacters();
            List<int> hashBefore = GetInventoryHashes(chars);
            form.SetCharacters(chars, CharacterPosition);
            form.Bag = Hacker.ReadBagFromRoster();

            List<Item>[] origBackpacks = new List<Item>[chars.Count];
            for(int i = 0; i < chars.Count; i++)
                origBackpacks[i] = chars[i].BackpackItems;

            if (form.ShowDialog() == DialogResult.OK)
            {
                List<int> hashAfter = GetInventoryHashes(m_formParty.GetCharacters());
                if (!Global.Compare(hashBefore, hashAfter))
                {
                    if (!Global.Cheats)
                    {
                        MessageBox.Show("The in-game party inventory was altered while the Bag of Holding dialog was open.\r\n\r\n" + 
                            "No changes will be made (you may enable Cheating in the Options to override this if you wish).", "In-Game Modification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (MessageBox.Show("The in-game party inventory was altered while the Bag of Holding dialog was open.\r\n\r\n" + 
                        "Would you like to overwrite the party inventory with what you did in the Bag of Holding?  Choosing \"yes\" may cause duplicate or missing items!",
                        "In-Game Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (!Hacker.SetBackpacks(chars, form.BackpackItems, true))
                {
                    MessageBox.Show("There was an error while setting the character backpacks.  Restoring original character inventory.", "Backpack Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Put the unstored items back in the bag
                    foreach (List<Item> list in form.BackpackItems)
                        foreach (Item item in list)
                            form.Bag.Items.Add(item);
                }
                if (Hacker.StoreBagInRoster(form.Bag) == -1)
                {
                    MessageBox.Show("There was an error while setting the bag items.  Restoring original character inventory.", "Bag of Holding Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Restore the original inventory
                    Hacker.SetBackpacks(chars, origBackpacks, true);
                }
            }
        }

        private List<int> GetInventoryHashes(List<BaseCharacter> chars)
        {
            List<int> list = new List<int>();
            foreach (BaseCharacter baseChar in chars)
            {
                list.Add(baseChar.Name.GetHashCode());
                if (baseChar.BasicInventory == null)
                    continue;

                foreach (Item item in baseChar.BasicInventory.Items)
                {
                    list.Add(item.IsEquipped ? 1 : 0);
                    list.Add(item.GetHashCode());
                }
            }
            return list;
        }

        private int GetNumBackpackItems()
        {
            List<Item> items = Hacker.GetBackpack(m_iCharacterAddress);
            int iRealItems = 0;
            foreach (Item item in items)
            {
                if (item.Index != 0)
                    iRealItems++;
            }
            return iRealItems;
        }

        private void miBackpackFillRandom_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            if (GetNumBackpackItems() > 0)
            {
                if (MessageBox.Show("Replace the contents of the backpack with completely random items?", "Randomize backpack?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }

            ItemType type = ItemType.None;
            if (NativeMethods.IsControlDown())
            {
                type = NativeMethods.IsKeyDown(Keys.D1) ? ItemType.Weapon :
                    NativeMethods.IsKeyDown(Keys.D2) ? ItemType.Armor :
                    NativeMethods.IsKeyDown(Keys.D3) ? ItemType.Accessory :
                    NativeMethods.IsKeyDown(Keys.D4) ? ItemType.Miscellaneous : ItemType.None;
            }
            Hacker.RandomizeBackpack(m_char, type, type == ItemType.Weapon || type == ItemType.Armor || type == ItemType.Accessory, NativeMethods.IsShiftDown());
        }

        private void btnQuests_Click(object sender, EventArgs e)
        {
            Quests_Clicked();
        }

        protected virtual void Quests_Clicked()
        {
            m_main.ShowQuests(m_char);
        }

        private void miBackpackEdit_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            PrepareCheatMenu(lvBackpack.Focused ? lvBackpack : lvEquipped);
            ModifySelectedValue(CheatMenuFlags.Edit);
        }

        private void miBackpackAdd_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            if (!lvBackpack.Focused)
                return;

            if (m_char.BackpackFull)
            {
                MessageBox.Show("Cannot add item; this character's backpack is full.", "Backpack full!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PrepareCheatMenu(lvBackpack, CheatMenuFlags.AddNew);
            ModifySelectedValue(CheatMenuFlags.AddNew);
        }

        private void miBackpackDuplicate_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            if (m_char.BackpackFull)
            {
                MessageBox.Show("Cannot duplicate item; this character's backpack is full.", "Backpack full!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Item item = null;

            if (lvBackpack.Focused && lvBackpack.SelectedItems.Count > 0)
            {
                if (!(lvBackpack.SelectedItems[0].Tag is InventoryItemTag))
                    return;
                item = (lvBackpack.SelectedItems[0].Tag as InventoryItemTag).Item;
            }
            else if (lvEquipped.Focused && lvEquipped.SelectedItems.Count > 0)
            {
                if (!(lvEquipped.SelectedItems[0].Tag is InventoryItemTag))
                    return;
                item = (lvEquipped.SelectedItems[0].Tag as InventoryItemTag).Item;
            }

            if (item == null)
                return;

            List<Item> items = m_char.BackpackItems;
            Item itemClone = Hacker.CloneItem(item);
            items.Add(itemClone);

            switch(Hacker.SetBackpack(m_char.BasicAddress, items))
            {
                case SetBackpackResult.InsufficientSpace:
                    MessageBox.Show(String.Format("Cannot duplicate item; there are too many items of type '{0}' in this character's backpack.",
                        item.BaseTypeString),"Too many items!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case SetBackpackResult.Success:
                    break;
                default:
                    MessageBox.Show("Cannot duplicate item; could not write character bytes to memory.",
                        "Error setting backpack data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void miBackpackClearAll_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            if (GetNumBackpackItems() > 0)
            {
                if (MessageBox.Show("Delete every item in this character's backpack?", "Discard all items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }

            Hacker.SetBackpack(m_char.BasicAddress, new List<Item>(0));
        }

        private void miBackpackDeleteItem_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            Item item = null;

            if (lvBackpack.Focused && lvBackpack.SelectedItems.Count > 0 && lvBackpack.SelectedItems[0].Tag is InventoryItemTag)
                item = (lvBackpack.SelectedItems[0].Tag as InventoryItemTag).Item;

            if (item == null)
                return;

            if (Hacker is Wiz4MemoryHacker && item is Wiz4Item && item.MemoryIndex > 7)
            {
                if (MessageBox.Show("Delete item from the Black Box?", "Discard item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                Wiz4MemoryHacker wiz4Hacker = Hacker as Wiz4MemoryHacker;
                List<Item> box = wiz4Hacker.GetBlackBox();
                box.Remove(box.First(i => i.Index == item.Index));
                wiz4Hacker.SetBlackBox(box);
                return;
            }

            if (MessageBox.Show("Delete item from this character's backpack?", "Discard item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            List<Item> items = m_char.BackpackItems;
            Item first = items.FirstOrDefault(i => i.Equals(item));
            if (first != null)
                items.Remove(first);

            Hacker.SetBackpack(m_char.BasicAddress, items);
        }

        public virtual void SetStatOrder(PrimaryStat[] stats)
        {
            if (Global.Compare(stats, m_lastStatOrder))
                return; // no changes

            m_lastStatOrder = stats;
        }

        private void lvResistances_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvResistances.SelectedItems)
                lvi.Selected = false;
        }

        private void cmCheat_Opening(object sender, CancelEventArgs e)
        {
            if (!Global.Cheats)
                e.Cancel = true;
        }

        private void miCheatCreateSupercharacter_Click(object sender, EventArgs e)
        {
            ModifySelectedValue(CheatMenuFlags.SuperChar);
        }

        public virtual bool UpdateSubscreen(Subscreen screen, bool bForce = false)
        {
            if (screen == m_oldScreen && !bForce)
                return false;

            m_oldScreen = screen;

            return HighlightInventory(screen);
        }

        protected bool HighlightInventory(Subscreen screen, bool bUpdate = true)
        {
            switch (screen)
            {
                case Subscreen.Weapons: return HighlightInventory(ItemType.Weapon, bUpdate);
                case Subscreen.Armor: return HighlightInventory(ItemType.Armor, bUpdate);
                case Subscreen.Accessories: return HighlightInventory(ItemType.Accessory, bUpdate);
                case Subscreen.Miscellaneous: return HighlightInventory(ItemType.Miscellaneous, bUpdate);
                case Subscreen.Inventory1: return HighlightInventory(ItemType.FirstScreen, bUpdate);
                case Subscreen.Inventory2: return HighlightInventory(ItemType.SecondScreen, bUpdate);
                case Subscreen.InventoryMain: return HighlightInventory(ItemType.Any, bUpdate);
                default: return HighlightInventory(ItemType.None, bUpdate);
            }
        }

        protected bool HighlightInventory(ItemType type, bool bUpdate = true)
        {
            if (bUpdate)
            {
                m_commonCtrls.lvEquipped.BeginUpdate();
                m_commonCtrls.lvBackpack.BeginUpdate();
            }

            Color colorHighlight = type == ItemType.Any ? m_commonCtrls.lvEquipped.BackColor : Global.Highlight(m_commonCtrls.lvEquipped.BackColor, 30);
            foreach (ListViewItem lvi in m_commonCtrls.lvEquipped.Items)
            {
                InventoryItemTag iit = lvi.Tag as InventoryItemTag;
                if (iit == null)
                    continue;   // MM1/2 don't have tags on null item entries in the fixed-size backpack
                if (iit.Triggered)
                    continue;   // Don't change the color of an item that was altered due to a trigger effect
                if (iit.Item.Matches(type))
                {
                    lvi.BackColor = colorHighlight;
                    lvi.Text = iit.FormatForDisplay();
                }
                else
                {
                    lvi.BackColor = m_commonCtrls.lvEquipped.BackColor;
                    lvi.Text = iit == null ? String.Empty : iit.ListViewString;
                }
            }
            colorHighlight = type == ItemType.Any ? m_commonCtrls.lvBackpack.BackColor : Global.Highlight(m_commonCtrls.lvBackpack.BackColor, 30);
            foreach (ListViewItem lvi in m_commonCtrls.lvBackpack.Items)
            {
                InventoryItemTag iit = lvi.Tag as InventoryItemTag;
                if (iit == null)
                    continue;   // MM1/2 don't have tags on null item entries in the fixed-size backpack
                if (iit.Triggered)
                    continue;   // Don't change the color of an item that was altered due to a trigger effect
                if (iit.Item.Matches(type))
                {
                    lvi.BackColor = colorHighlight;
                    lvi.Text = iit.FormatForDisplay();
                }
                else
                {
                    lvi.BackColor = m_commonCtrls.lvBackpack.BackColor;
                    lvi.Text = iit == null ? String.Empty : iit.ListViewString;
                }
            }
            if (bUpdate)
            {
                m_commonCtrls.lvEquipped.EndUpdate();
                m_commonCtrls.lvBackpack.EndUpdate();
            }

            return true;
        }

        private void miBackpackDropTrash_Click(object sender, EventArgs e)
        {
            List<BaseCharacter> chars = m_formParty.GetCharacters();
            DropTrashForm form = new DropTrashForm(chars, m_char);
            if (form.ShowDialog() == DialogResult.OK)
            {
                HashSet<Item> drop = form.ItemsToDrop;

                foreach (BaseCharacter mmChar in chars)
                {
                    List<Item> pack = mmChar.BackpackItems;
                    List<Item> packNew = new List<Item>(pack.Count);
                    foreach (Item bpItem in pack)
                    {
                        if (!drop.Contains(bpItem))
                            packNew.Add(bpItem);
                    }

                    Hacker.SetBackpack(mmChar.BasicAddress, packNew);
                }
            }
        }

        private string AddModString(Modifiers mod, ModAttr attrib, string strTip)
        {
            if (mod == null)
                return strTip.Trim();

            string strMod = Global.Flatten(mod.Reasons(attrib));
            if (String.IsNullOrWhiteSpace(strTip))
                return strMod;

            if (String.IsNullOrWhiteSpace(strMod))
                return strTip.Trim();

            return strMod + "\r\n" + strTip;
        }

        private string GetTipString(ModAttr attrib)
        {
            if (attrib == ModAttr.Invalid)
                return String.Empty;
            string strAttribTip = m_char.AttributeTip(attrib, Hacker);
            string strTip = Global.Flatten(m_char.Modifiers.Reasons(attrib));
            Modifiers modInternal = m_char.InternalModifiers;
            strTip = AddModString(modInternal, attrib, strTip);
            Modifiers modExternal = Hacker.GetExternalModifiers(m_char);
            strTip = AddModString(modExternal, attrib, strTip);

            if (String.IsNullOrWhiteSpace(strTip) && String.IsNullOrWhiteSpace(strAttribTip))
                return String.Empty;
            return String.IsNullOrWhiteSpace(strAttribTip) ? strTip : strAttribTip + (strTip.StartsWith("\r\n") ? "" : "\r\n") + strTip;
        }

        protected void SetTip(ModAttr attrib, Label label)
        {
            string strTip = GetTipString(attrib);
            if (String.IsNullOrWhiteSpace(strTip))
                m_tipStats.RemoveAll();
            m_tipStats.SetToolTip(label, strTip);
        }

        protected virtual void Stat_MouseLeave(object sender, EventArgs e)
        {
            m_tipStats.RemoveAll();
        }

        private void labelAC_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.ArmorClass, m_commonCtrls.labelAC); }
        private void labelMelee_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.MeleeDamage, m_commonCtrls.labelMelee); }
        private void labelHP_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.HitPoints, labelHP); }
        private void labelSP_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.SpellPoints, labelSP); }
        private void labelExp_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Experience, labelExp); }

        private void labelACHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.ArmorClass, labelACHeader); }
        private void labelMeleeHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.MeleeDamage, labelMeleeHeader); }
        private void labelHPHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.HitPoints, labelHPHeader); }
        private void labelSPHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.SpellPoints, labelSPHeader); }
        private void labelExpHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Experience, labelExpHeader); }

        private void lvResistances_MouseEnter(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvResistances.Items)
            {
                ResistanceValue rv = lvi.Tag as ResistanceValue;
                if (rv == null)
                    continue;

                lvi.ToolTipText = GetTipString(Modifiers.GetAttrib(rv.Resistance));
            }
        }

        private void miBackpackDebugMonitor_Click(object sender, EventArgs e)
        {
            cmBackpack.ShowCheckMargin = true;
            miBackpackDebugMonitor.Checked = !miBackpackDebugMonitor.Checked;
            m_bDebugMonitorBackpack = miBackpackDebugMonitor.Checked;
        }

        private void miBackpackDebugClearAll_Click(object sender, EventArgs e)
        {
            if (!Global.Debug)
                return;

            foreach(BaseCharacter bc in m_formParty.GetCharacters())
                Hacker.SetBackpack(bc.BasicAddress, new List<Item>(0));
        }

        private void miBackpackItemDisplayFormat_Click(object sender, EventArgs e)
        {
            EditItemDisplayFormatForm form = new EditItemDisplayFormatForm();
            form.DisplayFormat = Properties.Settings.Default.ItemFormat;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ItemFormat = form.DisplayFormat;
                m_formParty.ForceUpdate();
            }
        }

        private void lvBackpack_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowSelectedBackpackItemInfo();
        }

        private void ShowSelectedBackpackItemInfo()
        {
            if (m_char.Game == GameNames.Wizardry4)
                return; // Can't move items between characters in Wizardry 4 because the other "characters" are really enemies
            if (lvBackpack.SelectedItems.Count < 1)
                return;
            if (!(lvBackpack.SelectedItems[0].Tag is InventoryItemTag))
                return;
            ShowItemInfo(((InventoryItemTag)lvBackpack.SelectedItems[0].Tag).Item);
        }

        private void lvEquipped_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowSelectedEquippedItemInfo();
        }

        private void ShowSelectedEquippedItemInfo()
        {
            if (m_char.Game == GameNames.Wizardry4)
                return; // Can't move items between characters in Wizardry 4 because the other "characters" are really enemies
            if (lvEquipped.SelectedItems.Count < 1)
                return;
            if (!(lvEquipped.SelectedItems[0].Tag is InventoryItemTag))
                return;
            ShowItemInfo(((InventoryItemTag)lvEquipped.SelectedItems[0].Tag).Item);
        }

        private void ShowItemInfo(Item item)
        {
            ItemCompareForm formCompare = new ItemCompareForm();
            formCompare.CenteringForm = m_formParty;
            formCompare.Item = item;
            List<BaseCharacter> chars = m_formParty.GetCharacters();
            if (chars == null)
                return;
            formCompare.ItemOwner = CharacterIndex;

            formCompare.Characters = chars;
            if (formCompare.ShowDialog() == DialogResult.OK)
            {
                int iMoveTo = formCompare.MoveToCharacter;
                if (iMoveTo != -1 && Hacker != null && iMoveTo < chars.Count && iMoveTo != CharacterIndex)
                {
                    if (item.IsEquipped)
                    {
                        MessageBox.Show(String.Format("You must unequip the \"{0}\" from \"{1}\" in order to move it to \"{2}\"",
                            item.DescriptionString, Character.Name, chars[iMoveTo].Name), "Error moving item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!Hacker.MoveItem(item, m_char, chars[iMoveTo]))
                    {
                        MessageBox.Show(String.Format("Could not move \"{0}\" from \"{1}\" to \"{2}\" - backpack may be full.",
                            item.DescriptionString, Character.Name, chars[iMoveTo].Name), "Error moving item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lvEquipped_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ShowSelectedEquippedItemInfo();
        }

        private void lvBackpack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ShowSelectedBackpackItemInfo();
        }

        private void scCharQuickref_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!m_bUpdate)
                Properties.Settings.Default.PartyQuickrefSplitPosition = scCharQuickref.SplitterDistance;
        }

        private void scStatsResistances_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!m_bUpdate)
                Properties.Settings.Default.PartyResistSplitPosition = scStatsResistances.SplitterDistance;
        }

        private void miBackpackStackCharges_Click(object sender, EventArgs e)
        {
            if (!Global.Cheats)
                return;

            Item itemToStack = lvBackpack.SelectedItems.Count > 0 ? (lvBackpack.SelectedItems[0].Tag as InventoryItemTag)?.Item : null;

            if (itemToStack == null || !itemToStack.ChargeBased)
                return;

            List<Item> listSame = new List<Item>();

            foreach (ListViewItem lvi in lvBackpack.Items)
            {
                Item item = (lvi.Tag as InventoryItemTag)?.Item;

                if (item == null || item == itemToStack || item.Index != itemToStack.Index)
                    continue;

                if (!item.IsIdentified || item.Broken || item.Cursed || !item.ChargeBased)
                    continue;

                listSame.Add(item);
            }

            if (listSame.Count < 1)
            {
                MessageBox.Show("Could not find any matching items to stack.", "No similar items", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int iAdd = listSame.Sum(i => i.ChargesCurrent);
            int iNew = iAdd + itemToStack.ChargesCurrent;
            int iNeededItems = ((iNew - 1) / itemToStack.MaxCharges) + 1;

            if (MessageBox.Show(String.Format("Combine {0} into {1}?", Global.Plural(listSame.Count + 1, "item"), Global.Plural(iNeededItems, "item")),
                "Stack items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            List<Item> listPack = m_char.BackpackItems;
            for(int i = 0; i < listPack.Count; i++)
            {
                Item item = listPack[i];
                if (item != itemToStack && !listSame.Contains(item))
                    continue;

                if (iNew > 0)
                {
                    item.ChargesCurrent = (iNew <= item.MaxCharges ? iNew : item.MaxCharges);
                    iNew -= item.ChargesCurrent;
                }
                else
                {
                    listPack.Remove(item);
                    i--;
                }
            }

            Hacker.SetBackpack(m_char.BasicAddress, listPack);
        }
    }

    public enum BasicInfoStyle
    {
        Unknown,
        PermanentWithTemp,
        PermanentWithMod,
        PermanentWithModNoAlign,
        AgeInWeeks,
        NoAgeOrAlign,
        SexNoAgeAlign,
        NoLevel,
    }

    public abstract class BaseCharacter
    {
        public byte[] RawBytes;

        [NonSerialized]
        public Modifiers Modifiers = new Modifiers();

        public virtual Modifiers InternalModifiers { get { return null; } }

        public override string ToString()
        {
            return BasicInfoString;
        }

        public BaseCharacter()
        {
        }

        public void SetFromBytes(byte[] bytes, int iIndex = 0, bool bRosterFile = false)
        {
            if (bytes == null || bytes.Length < iIndex + CharacterSize - 1)
                return;
            SetCharFromStream(0, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null, null, bRosterFile);
        }

        public void SetFromBytes(int iCharIndex, byte[] bytes, GameInfo info, EncounterInfo encounterInfo = null, byte[] itemTable = null)
        {
            SetCharFromStream(iCharIndex, new MemoryStream(bytes), info, encounterInfo, false, itemTable);
        }

        public static string AgeString(byte year, byte day)
        {
            return String.Format("{0}y {1}d", year, day);
        }

        public abstract CharacterOffsets Offsets { get; }

        public static string ClassString(GenericClass classenum)
        {
            switch (classenum)
            {
                case GenericClass.Archer: return "Archer";
                case GenericClass.Cleric: return "Cleric";
                case GenericClass.Knight: return "Knight";
                case GenericClass.Paladin: return "Paladin";
                case GenericClass.Robber: return "Robber";
                case GenericClass.Sorcerer: return "Sorcerer";
                case GenericClass.Ninja: return "Ninja";
                case GenericClass.Barbarian: return "Barbarian";
                case GenericClass.Druid: return "Druid";
                case GenericClass.Ranger: return "Ranger";
                case GenericClass.Fighter: return "Fighter";
                case GenericClass.Mage: return "Mage";
                case GenericClass.Priest: return "Priest";
                case GenericClass.Thief: return "Thief";
                case GenericClass.Bishop: return "Bishop";
                case GenericClass.Samurai: return "Samurai";
                case GenericClass.Lord: return "Lord";
                case GenericClass.Bard: return "Bard";
                case GenericClass.Conjurer: return "Conjurer";
                case GenericClass.Hunter: return "Hunter";
                case GenericClass.Magician: return "Magician";
                case GenericClass.Monk: return "Monk";
                case GenericClass.Rogue: return "Rogue";
                case GenericClass.Warrior: return "Warrior";
                case GenericClass.Wizard: return "Wizard";
                case GenericClass.Archmage: return "Archmage";
                case GenericClass.Chronomancer: return "Chronomancer";
                case GenericClass.Geomancer: return "Geomancer";
                case GenericClass.FighterCleric: return "Fighter/Cleric";
                case GenericClass.FighterThief: return "Fighter/Thief";
                case GenericClass.FighterMage: return "Fighter/Mage";
                case GenericClass.FighterMageThief: return "Fighter/Mage/Thief";
                case GenericClass.ThiefMage: return "Thief/Mage";
                case GenericClass.FighterClericMage: return "Fighter/Cleric/Mage";
                case GenericClass.RangerCleric: return "Ranger/Cleric";
                case GenericClass.ClericMage: return "Cleric/Mage";
                default: return "None";
            }
        }

        public static string AlignmentString(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Good: return "Good";
                case GenericAlignmentValue.Neutral: return "Neutral";
                case GenericAlignmentValue.Evil: return "Evil";
                case GenericAlignmentValue.LawfulGood: return "Lawful Good";
                case GenericAlignmentValue.NeutralGood: return "Neutral Good";
                case GenericAlignmentValue.ChaoticGood: return "Chaotic Good";
                case GenericAlignmentValue.LawfulNeutral: return "Lawful Neutral";
                case GenericAlignmentValue.TrueNeutral: return "True Neutral";
                case GenericAlignmentValue.ChaoticNeutral: return "Chaotic Neutral";
                case GenericAlignmentValue.LawfulEvil: return "Lawful Evil";
                case GenericAlignmentValue.NeutralEvil: return "Neutral Evil";
                case GenericAlignmentValue.ChaoticEvil: return "Chaotic Evil";
                default: return "None";
            }
        }

        public virtual string GetACString(int iBless = 0) { return BasicAC.Temporary.ToString(); }

        public static string WorstConditionString(BasicConditionFlags cond)
        {
            if (cond.HasFlag(BasicConditionFlags.Eradicated))
                return "erad";
            if (cond.HasFlag(BasicConditionFlags.Dead))
                return "dead";
            if (cond.HasFlag(BasicConditionFlags.Stone))
                return "stone";
            if (cond.HasFlag(BasicConditionFlags.Unconscious))
                return "unc";
            if (cond.HasFlag(BasicConditionFlags.Paralyzed))
                return "held";
            if (cond.HasFlag(BasicConditionFlags.Silenced))
                return "silent";
            if (cond.HasFlag(BasicConditionFlags.Asleep))
                return "asleep";
            if (cond.HasFlag(BasicConditionFlags.Afraid))
                return "afraid";
            if (cond.HasFlag(BasicConditionFlags.Blinded))
                return "blind";
            if (cond.HasFlag(BasicConditionFlags.Poisoned))
                return "poison";
            if (cond.HasFlag(BasicConditionFlags.Diseased))
                return "disease";
            return "";
        }

        public virtual string ExperienceString { get { return "N/A"; } }
        public virtual ResistanceValue[] GetResistances() { return new ResistanceValue[0]; }
        public virtual string Name { get { return "N/A"; } }
        public virtual StatAndModifier BasicLevel { get { return null; } }
        public virtual string BasicLevelString { get { return BasicLevel.Temporary.ToString(); } }
        public virtual StatAndModifier BasicAC { get { return new StatAndModifier(0, 0); } }
        public virtual BasicDamage BasicMeleeDamage { get { return BasicDamage.Zero; } }
        public virtual BasicDamage BasicRangedDamage { get { return BasicDamage.Zero; } }
        public virtual void Serialize(Stream stream) { }
        public virtual void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemTable = null) { }
        public virtual string CombatInfo { get { return "N/A"; } }
        public virtual long NeedsXP { get { return -1; } }
        public virtual long XPForNextLevel { get { return -1; } }
        public virtual long XPForCurrentLevel { get { return -1; } }
        public virtual string MeleeDamageString { get { return "N/A"; } }
        public virtual string RangedDamageString { get { return "N/A"; } }
        public virtual string ResistancesString { get { return "N/A"; } }
        public virtual string AttributesString { get { return "N/A"; } }
        public virtual bool ReadyToTrain { get { return false; } }
        public virtual string BasicInfoString { get { return "N/A"; } }
        public virtual string MultiLineDescription { get { return "N/A"; } }
        public virtual long QuickRefExperience { get { return 0; } }
        public virtual MMHitPoints QuickRefHitPoints { get { return null; } }
        public virtual SpellPoints QuickRefSpellPoints { get { return null; } }
        public virtual OneByteStat QuickRefSpeed { get { return null; } }
        public virtual OneByteStat QuickRefSpellLevel { get { return null; } }
        public virtual int QuickRefGems { get { return 0; } }
        public virtual string QuickRefCondition { get { return "N/A"; } }
        public virtual bool IsHealer { get { return false; } }
        public virtual bool PartyGems { get { return false; } }
        public virtual bool HasSpellLevel { get { return true; } }
        public virtual GenericClass BasicClass { get { return GenericClass.None; } }
        public virtual GenericRace BasicRace { get { return GenericRace.None; } }
        public virtual MMSex BasicSex { get { return MMSex.None; } }
        public virtual GenericAlignment BasicAlignment { get { return new GenericAlignment(GenericAlignmentValue.None, GenericAlignmentValue.None); } }
        public virtual GenericAge BasicAge { get { return null; } }
        public virtual bool KnowsSpell(Spell spell) { return true; }
        public virtual bool KnowsSpell(SpellType type, int level, int number) { return true; }
        public virtual int NumKnownSpells { get { return 0; } }
        public virtual BasicConditionFlags BasicCondition { get { return BasicConditionFlags.Good; } }
        public virtual byte SexValue(MMSex sex) { return 0; }
        public virtual byte AlignmentValue(GenericAlignmentValue align) { return 0; }
        public virtual byte RaceValue(GenericRace race) { return 0; }
        public virtual byte ClassValue(GenericClass classVal) { return 0; }
        public virtual byte ConditionValue(BasicConditionFlags condition) { return 0; }
        public virtual Item GetItem(byte[] bytes, int offset = 0) { return null; }
        public virtual List<Item> BackpackItems { get { return null; } }
        public virtual int MaxBackpackSize { get { return 6; } }
        public virtual int MaxBackpackWeapons { get { return 0; } }
        public virtual int MaxBackpackArmor { get { return 0; } }
        public virtual int MaxBackpackAccessories { get { return 0; } }
        public virtual int MaxBackpackMisc { get { return 0; } }
        public virtual int BasicAddress { get { return -1; } }
        public virtual GameNames Game { get { return GameNames.None; } }
        public virtual int FirstEmptyBackpackIndex { get { return -1; } }
        public virtual int CharacterSize { get { return 0; } }
        public virtual bool BackpackFull { get { return false; } }
        public virtual int MaxLevel { get { return 255; } }
        public virtual long BasicExperience { get { return 0; } }
        public virtual long XPForLevel(GenericClass mmClass, int iLevel) { return 0; }
        public virtual Inventory BasicInventory { get { return null; } }
        public virtual string GetMaxHPFormula() { return String.Empty; }
        public virtual string GetMaxSPFormula() { return String.Empty; }
        public virtual string GetACFormula(int iBless = 0) { return String.Empty; }
        public virtual string GetThieveryFormula() { return String.Empty; }
        public virtual string GetCriticalFormula() { return String.Empty; }
        public virtual BasicInfoStyle InfoStyle { get { return Games.GetInfoType(Game); } }
        public virtual string ReadySpellString { get { return "None"; } }
        public virtual string EquippedString { get { return "<Unknown>"; } }
        public virtual string BackpackString { get { return "<Unknown>"; } }
        public virtual bool UsesSpellLevel { get { return false; } }
        public virtual int Songs { get { return 0; } }
        public virtual int BasicSP { get { return 0; } }
        public virtual int BasicMaxSP { get { return 0; } }
        public virtual int BasicHP { get { return 0; } }
        public virtual int BasicMaxHP { get { return 0; } }
        public virtual long BasicMoney { get { return 0; } }
        public virtual int BasicFood { get { return 0; } }
        public virtual int BasicMaxFood { get { return 0; } }
        public virtual int BasicThievery { get { return 0; } }
        public virtual int BasicDeaths { get { return 0; } }
        public virtual int BasicSwimming { get { return 0; } }
        public virtual int BasicDisarm { get { return 0; } }
        public virtual int BasicRegen { get { return 0; } }
        public virtual int BasicPoison { get { return 0; } }
        public virtual int BasicIDTrap { get { return 0; } }
        public virtual int BasicIDItem { get { return 0; } }
        public virtual int BasicDispel { get { return 0; } }
        public virtual int BasicCritical { get { return 0; } }
        public virtual int BasicHide { get { return 0; } }
        public virtual int BasicAttacks { get { return 0; } }
        public virtual int BasicWon { get { return 0; } }
        public virtual int BasicSongs { get { return 0; } }

        public virtual bool IsCaster { get { return true; } }

        public abstract StatAndModifier[] PrimaryStats { get; }
        public virtual StatAndModifier Stat(int i) { return (PrimaryStats == null || PrimaryStats.Length <= i) ? StatAndModifier.Zero : PrimaryStats[i]; }
        public virtual StatAndModifier Stat(string str) { return StatAndModifier.Zero; }

        public virtual int TrainableLevel
        {
            get
            {
                int iLevel = BasicLevel.Permanent;
                while (XPForLevel(BasicClass, iLevel + 1) <= BasicExperience && iLevel < MaxLevel)
                    iLevel++;
                return iLevel;
            }
        }

        public virtual string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            switch (attrib)
            {
                case ModAttr.HitPoints: return GetMaxHPFormula();
                case ModAttr.SpellPoints: return GetMaxSPFormula();
                case ModAttr.ArmorClass: return GetACFormula(hacker.GetBlessValue());
                case ModAttr.Thievery: return GetThieveryFormula();
                case ModAttr.Critical: return GetCriticalFormula();
                default: return String.Empty;
            }
        }

        public StatModifier GetModifier(int iValue, PrimaryStat stat, int iValueSecondary = 0, PrimaryStat statSecondary = PrimaryStat.None)
        {
            if (this is MM1Character)
                return MM1Character.GetStatModifier(iValue, stat);
            if (this is MM2Character)
                return MM2Character.GetStatModifier(iValue, stat);
            if (this is MM3Character)
                return MM3Character.GetStatModifier(iValue, stat);
            if (this is MM45Character)
                return MM45Character.GetStatModifier(iValue, stat);
            if (this is WizCharacter)
                return WizCharacter.GetStatModifier(iValue, stat);
            if (this is BT3Character)
                return BT3Character.GetBT3StatModifier(iValue, stat);
            if (this is BTCharacter)
                return BTCharacter.GetStatModifier(iValue, stat);
            if (this is EOBCharacter)
                return EOBCharacter.GetStatModifier(iValue, stat, iValueSecondary, statSecondary, BasicClass);
            return StatModifier.Zero;
        }

        public virtual bool HasItem(int itemIndex)
        {
            if (BasicInventory == null || BasicInventory.Items == null)
                return false;

            return BasicInventory.Items.Any(i => i.Index == itemIndex);
        }

        public virtual bool HasEquipped(int itemIndex)
        {
            if (BasicInventory == null || BasicInventory.Items == null)
                return false;

            Item item = BasicInventory.Items.FirstOrDefault(i => i.Index == itemIndex && i.IsEquipped);
            return (item != null);
        }
    }

    public class CharCommonControls
    {
        public LinkLabel llCureAll;
        public ListView lvBackpack;
        public ListView lvEquipped;
        public ListView lvResistances;
        public Label labelMelee;
        public Label labelMeleeHeader;
        public Label labelExp;
        public Label labelCondition;
        public Label labelConditionHeader;
        public Label labelAC;
        public Label labelHP;
        public Label labelSP;
        public Label labelACHeader;
        public Label labelHPHeader;
        public Label labelSPHeader;
        public Label labelLevel;
        public Label labelExpHeader;
        public Label labelPoison;
        public Label labelPoisonHeader;

        public virtual void CopyFrom(CharCommonControls cc)
        {
            llCureAll = cc.llCureAll;
            lvBackpack = cc.lvBackpack;
            lvEquipped = cc.lvEquipped;
            lvResistances = cc.lvResistances;
            labelMelee = cc.labelMelee;
            labelMeleeHeader = cc.labelMeleeHeader;
            labelExp = cc.labelExp;
            labelCondition = cc.labelCondition;
            labelConditionHeader = cc.labelConditionHeader;
            labelAC = cc.labelAC;
            labelHP = cc.labelHP;
            labelSP = cc.labelSP;
            labelACHeader = cc.labelACHeader;
            labelHPHeader = cc.labelHPHeader;
            labelSPHeader = cc.labelSPHeader;
            labelLevel = cc.labelLevel;
            labelExpHeader = cc.labelExpHeader;
            labelPoison = cc.labelPoison;
            labelPoisonHeader = cc.labelPoisonHeader;
        }
    }

    public class MMArmorClass
    {
        public byte ArmorOnly;
        public byte Total;

        public MMArmorClass(byte[] bytes, int index)
        {
            ArmorOnly = bytes[index];
            Total = bytes[index + 1];
        }

        public MMArmorClass(byte armor, byte total)
        {
            ArmorOnly = armor;
            Total = total;
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = ArmorOnly;
            bytes[index + 1] = Total;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}{2}", ArmorOnly, Total < ArmorOnly ? "-" : "+", Math.Abs(Total - ArmorOnly));
        }
    }

    public class StatAndModifier
    {
        public int Permanent;
        public int Modifier;

        public int Temporary { get { return Permanent + Modifier; } }
        public static StatAndModifier Zero { get { return new StatAndModifier(0, 0); } }

        public StatAndModifier(int perm, int mod)
        {
            Permanent = perm;
            Modifier = mod;
        }

        public StatAndModifier(PermAndTemp pat)
        {
            Permanent = pat.Permanent;
            Modifier = pat.Temporary - pat.Permanent;
        }

        public StatAndModifier(OneByteStatModifier obsm)
        {
            Permanent = obsm.Permanent;
            Modifier = obsm.Modifier;
        }

        public override string ToString()
        {
            return String.Format("{0}+{1} ({2})", Permanent, Modifier, Temporary);
        }

        public override int GetHashCode()
        {
            return Permanent << 16 | Modifier;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StatAndModifier))
                return false;
            StatAndModifier sm = obj as StatAndModifier;
            return (sm.Permanent == Permanent && sm.Modifier == Modifier);
        }
    }

    public class PermAndTemp
    {
        public int Permanent;
        public int Temporary;

        public int Bonus { get { return Temporary - Permanent; } }

        public override string ToString()
        {
            if (Temporary == Permanent)
                return String.Format("{0}", Temporary);
            return String.Format("{0}{1}{2}", Permanent, Temporary < Permanent ? "-" : "+", Math.Abs(Temporary - Permanent));
        }

        public virtual string SlashString { get { return String.Format("{0}/{1}", Temporary, Permanent); } }

        public PermAndTemp(int temp, int perm)
        {
            Temporary = temp;
            Permanent = perm;
        }

        public virtual void SetBytes(byte[] bytes, int permOffset, int tempOffset = -1)
        {
            if (tempOffset == -1)
                tempOffset = permOffset + 1;
            bytes[permOffset] = (byte)Permanent;
            bytes[permOffset + 1] = (byte)Temporary;
        }
    }

    public class OneByteStat : PermAndTemp
    {
        public bool Signed;
        public OneByteStat(byte[] bytes, int index) : base(bytes[index + 1], bytes[index]) { }
        public OneByteStat(int temp, int perm) : base(temp, perm) { }
        public OneByteStat(int both) : base(both, both) { }
        public static OneByteStat Zero => new OneByteStat(0, 0);

        public OneByteStat(byte[] bytes, int indexTemp, int indexPerm, bool bSigned = false) : base(bytes[indexTemp], bytes[indexPerm])
        {
            Signed = bSigned;
            if (Signed)
            {
                Temporary = (sbyte)Temporary;
                Permanent = (sbyte)Permanent;
            }
        }

        public override string SlashString
        {
            get
            {
                if (!Signed)
                    return base.SlashString;
                return String.Format("{0}/{1}", (sbyte)Temporary, (sbyte)Permanent);
            }
        }
    }

    public class TwoByteStat : PermAndTemp
    {
        public TwoByteStat(byte[] bytes, int tempOffset, int permOffset) : base(BitConverter.ToInt16(bytes, tempOffset), BitConverter.ToInt16(bytes, permOffset)) { }
        public TwoByteStat(OneByteStat stat) : base(stat.Temporary, stat.Permanent) { }

        public override void SetBytes(byte[] bytes, int perm, int temp = -1)
        {
            Global.SetInt16(bytes, temp, Temporary);
            Global.SetInt16(bytes, perm, Permanent);
        }
    }

    public class OneByteStatModifier
    {
        // Permanent value is the first byte
        public byte Permanent;
        public byte Modifier;

        public UInt16 Temporary
        {
            get { return (UInt16)(Permanent + Modifier); }
        }

        public OneByteStatModifier(byte[] bytes, int index)
        {
            Permanent = bytes[index];
            Modifier = bytes[index + 1];
        }

        public OneByteStatModifier(byte mod, byte perm)
        {
            Modifier = mod;
            Permanent = perm;
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = Permanent;
            bytes[index + 1] = Modifier;
        }

        public override string ToString()
        {
            if (Modifier == 0)
                return String.Format("{0}", Permanent);
            return String.Format("{0}{1}", Permanent, Global.AddPlus(Modifier));
        }

        public string AdjustedString(int iAmount)
        {
            if (Modifier == 0)
                return String.Format("{0}{1}", Permanent, iAmount == 0 ? "" : Global.AddPlus(iAmount));
            return String.Format("{0}{1}{2}", Permanent, iAmount == 0 ? "" : Global.AddPlus(iAmount), Global.AddPlus(Modifier));
        }
    }

    public abstract class SpellPoints
    {
        public abstract string Current { get; }
        public virtual string Maximum { get { return String.Empty; } }
        public virtual bool HasAnyCurrent { get { return false; } }

        public static int CompareCurrent(SpellPoints sp1, SpellPoints sp2)
        {
            if (sp1 is MMSpellPoints && sp2 is MMSpellPoints)
                return Math.Sign(((MMSpellPoints)sp1).CurrentSP - ((MMSpellPoints)sp2).CurrentSP);
            return String.Compare(sp1.Current, sp2.Current);
        }

        public static int CompareMaximum(SpellPoints sp1, SpellPoints sp2)
        {
            if (sp1 is MMSpellPoints && sp2 is MMSpellPoints)
                return Math.Sign(((MMSpellPoints)sp1).MaximumSP - ((MMSpellPoints)sp2).MaximumSP);
            return 0;
        }
    }

    public class MMSpellPoints : SpellPoints
    {
        public int CurrentSP;
        public int MaximumSP;

        public override string Current { get { return CurrentSP.ToString(); } }
        public override string Maximum { get { return MaximumSP.ToString(); } }
        public override bool HasAnyCurrent { get { return CurrentSP > 0; } }

        public MMSpellPoints(byte[] bytes, int index)
        {
            CurrentSP = BitConverter.ToUInt16(bytes, index);
            MaximumSP = BitConverter.ToUInt16(bytes, index + 2);
        }

        public void SetBytes(byte[] bytes, int index)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(CurrentSP), 0, bytes, index, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(MaximumSP), 0, bytes, index + 2, 2);
        }

        public MMSpellPoints(int iCurrent, int iMax)
        {
            CurrentSP = iCurrent;
            MaximumSP = iMax;
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}", CurrentSP, MaximumSP);
        }
    }

    public class MMHitPoints
    {
        public int Current;
        public int Maximum;
        public int TemporaryMaximum;

        public MMHitPoints(byte[] bytes, int index)
        {
            Current = BitConverter.ToUInt16(bytes, index);
            Maximum = BitConverter.ToUInt16(bytes, index + 2);
            TemporaryMaximum = BitConverter.ToUInt16(bytes, index + 4);
        }

        public MMHitPoints(byte[] bytes, int iCurrent, int iMax, int iTempMax)
        {
            Current = BitConverter.ToUInt16(bytes, iCurrent);
            Maximum = BitConverter.ToUInt16(bytes, iMax);
            TemporaryMaximum = BitConverter.ToUInt16(bytes, iTempMax);
        }

        public MMHitPoints(int iCurrent, int iMax, int iTempMax)
        {
            Current = iCurrent;
            Maximum = iMax;
            TemporaryMaximum = iTempMax;
        }

        public MMHitPoints(int iCurrent, int iMax)
        {
            Current = iCurrent;
            Maximum = iMax;
            TemporaryMaximum = iMax;
        }

        public void SetBytes(byte[] bytes, int index)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(Current), 0, bytes, index, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(Maximum), 0, bytes, index + 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(TemporaryMaximum), 0, bytes, index + 4, 2);
        }

        public override string ToString()
        {
            if (Maximum == TemporaryMaximum)
                return String.Format("{0}/{1}", Current, Maximum);
            return String.Format("{0}/{1} ({2})", Current, TemporaryMaximum, Maximum);
        }
    }

    public class MMDamage
    {
        public byte Ordinary;
        public byte Magical;

        public MMDamage(byte[] bytes, int index)
        {
            Ordinary = bytes[index];
            Magical = bytes[index + 1];
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = Ordinary;
            bytes[index + 1] = Magical;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}", Ordinary > 0 ? "1d" : "", Ordinary, Magical > 0 ? "+" : "", Magical > 0 ? Magical.ToString() : "");
        }
    }

    public class TriggerControl
    {
        public Control Main;
        public int Index;
        public int SubIndex;

        public TriggerControl(Control ctrl)
        {
            Main = ctrl;
            Index = -1;
            SubIndex = -1;
        }

        public TriggerControl(Control ctrl, int index, int subindex)
        {
            Main = ctrl;
            Index = index;
            SubIndex = subindex;
        }

        public override int GetHashCode()
        {
            return Main == null ? 0 : Main.GetHashCode() ^ (Index << 8) ^ (SubIndex);
        }

        public override bool Equals(object obj)
        {
            TriggerControl tc = obj as TriggerControl;
            if (tc == null)
                return false;

            return tc.Main == Main && Index == tc.Index && SubIndex == tc.SubIndex;
        }
    }
}