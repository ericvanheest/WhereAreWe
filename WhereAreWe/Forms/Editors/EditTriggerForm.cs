using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WhereAreWe
{
    public partial class EditTriggerForm : Form
    {
        private bool m_bUpdating = false;
        private CharacterTrigger m_trigger = null;

        public EditTriggerForm()
        {
            InitializeComponent();
            InitComboBoxes();
        }

        public EditTriggerForm(CharacterTrigger trigger)
        {
            m_trigger = trigger;
            InitializeComponent();
            InitComboBoxes();
        }

        public CharacterTrigger Trigger
        {
            get { return GetTriggerFromUI(); }
            set { m_trigger = value; UpdateUI();  }
        }

        private void InitComboBoxes()
        {
            m_bUpdating = true;
            for (TriggerEntity what = TriggerEntity.Nothing; what < TriggerEntity.Last; what++)
            {
                bool bWhat = true;
                bool bWhich = true;
                bool bTo = true;
                switch (what)
                {
                    case TriggerEntity.AnyStat:
                        bWhich = false;
                        bTo = false;
                        break;
                    case TriggerEntity.SpecificValue:
                        bTo = false;
                        break;
                    case TriggerEntity.TabTitle:
                    case TriggerEntity.TestedItem:
                        bWhich = false;
                        bWhat = false;
                        break;
                }
                if (bWhat)
                    comboWhat.Items.Add(new TriggerEntityTag(what));
                if (bWhich)
                {
                    comboWhich.Items.Add(new TriggerEntityTag(what));
                    comboWhichBetween.Items.Add(new TriggerEntityTag(what));
                }
                if (bTo)
                    comboTo.Items.Add(new TriggerEntityTag(what));
            }
            comboWhat.Sorted = true;
            comboWhich.Sorted = true;
            comboTo.Sorted = true;
            comboWhichBetween.Sorted = true;

            for (TriggerWho who = TriggerWho.None; who < TriggerWho.Last; who++)
                comboWho.Items.Add(Global.Title(CharacterTrigger.WhoString(who)));
            for (TriggerWhen when = TriggerWhen.Equals; when < TriggerWhen.Last; when++)
                comboWhen.Items.Add(TriggerCondition.WhenString(when));
            for (TriggerDo tdo = TriggerDo.Nothing; tdo < TriggerDo.Last; tdo++)
                comboDo.Items.Add(Global.Title(CharacterTrigger.DoFull(tdo)));
            for (TriggerDifference diff = TriggerDifference.None; diff < TriggerDifference.Last; diff++)
            {
                comboDifference.Items.Add(TriggerCondition.DifferenceString(diff));
                comboDifferenceBetween.Items.Add(TriggerCondition.DifferenceString(diff));
            }

            UpdateUI();

            m_bUpdating = false;
        }

        private void UpdateUI()
        {
            if (m_trigger == null)
                m_trigger = new CharacterTrigger();

            if (m_trigger.Conditions != null && m_trigger.Conditions.Count > 0)
            {
                SetEntity(comboWhat, m_trigger.Conditions[0].What);
                SetEntity(comboWhich, m_trigger.Conditions[0].Which);
                SetEntity(comboWhichBetween, m_trigger.Conditions[0].WhichBetween);
                comboWhen.SelectedIndex = (int)m_trigger.Conditions[0].When;
                tbWhatValue.Text = m_trigger.Conditions[0].WhatValue;
                tbWhichValue.Text = m_trigger.Conditions[0].WhichValue;
                tbWhichValueBetween.Text = m_trigger.Conditions[0].WhichValueBetween;
                comboDifference.SelectedIndex = (int)m_trigger.Conditions[0].WhenDiff;
                comboDifferenceBetween.SelectedIndex = (int)m_trigger.Conditions[0].WhenDiffBetween;
                nudDifference.Value = m_trigger.Conditions[0].WhenValue;
                nudDifferenceBetween.Value = m_trigger.Conditions[0].WhenValueBetween;
            }
            SetEntity(comboTo, m_trigger.To);
            comboWho.SelectedIndex = (int) m_trigger.Who;
            comboDo.SelectedIndex = (int) m_trigger.Do;
            tbToValue.Text = m_trigger.ToValue;
            tbWhoValue.Text = m_trigger.WhoValue;
            UpdateColor();
        }

        private void SetEntity(ComboBox combo, TriggerEntity entity)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (((TriggerEntityTag)combo.Items[i]).Entity == entity)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }

        private void UpdateDescription()
        {
            CharacterTrigger trigger = GetTriggerFromUI();
            tbDescription.Text = trigger == null ? "None" : trigger.ToString();
        }

        private void comboWho_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((TriggerWho)comboWho.SelectedIndex)
            {
                case TriggerWho.ClassEquals:
                case TriggerWho.NameEquals:
                case TriggerWho.IndexEquals:
                    tbWhoValue.Enabled = true;
                    break;
                default:
                    tbWhoValue.Enabled = false;
                    break;
            }
            UpdateDescription();
        }

        private void comboWhat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbWhatValue.Enabled = ((TriggerEntityTag)comboWhat.SelectedItem).NeedsValue;
            UpdateDescription();
        }

        private void comboWhen_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bAnd = false;
            switch ((TriggerWhen)comboWhen.SelectedIndex)
            {
                case TriggerWhen.Contains:
                case TriggerWhen.DoesNotContain:
                case TriggerWhen.MatchesRegex:
                case TriggerWhen.DoesNotMatchRegex:
                    nudDifference.Enabled = false;
                    comboDifference.Enabled = false;
                    break;
                case TriggerWhen.IsBetween:
                case TriggerWhen.IsNotBetween:
                    bAnd = true;
                    break;
                default:
                    comboDifference.Enabled = true;
                    nudDifference.Enabled = comboDifference.SelectedIndex != (int) TriggerDifference.None;
                    break;
            }
            tbWhichValueBetween.Enabled = bAnd && ((TriggerEntityTag)comboWhichBetween.SelectedItem).NeedsValue;
            comboWhichBetween.Enabled = bAnd;
            comboDifferenceBetween.Enabled = bAnd;
            nudDifferenceBetween.Enabled = bAnd && comboDifferenceBetween.SelectedIndex != (int)TriggerDifference.None;
            labelAnd.Enabled = bAnd;
            UpdateDescription();
        }

        private void comboWhich_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbWhichValue.Enabled = ((TriggerEntityTag)comboWhich.SelectedItem).NeedsValue;
            bool bBetween = ((TriggerWhen)comboWhen.SelectedIndex) == TriggerWhen.IsBetween || ((TriggerWhen)comboWhen.SelectedIndex) == TriggerWhen.IsNotBetween;
            tbWhichValueBetween.Enabled = bBetween && ((TriggerEntityTag)comboWhichBetween.SelectedItem).NeedsValue;
            UpdateDescription();
        }

        private void comboDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((TriggerDo)comboDo.SelectedIndex)
            {
                case TriggerDo.SetColorTo:
                    llColor.Enabled = true;
                    llColor.Text = "Color";
                    labelColorSample.Visible = true;
                    llColorTo.Enabled = false;
                    labelColorToSample.Visible = false;
                    break;
                case TriggerDo.SetGradient:
                    llColor.Text = "From Color";
                    llColor.Enabled = true;
                    labelColorSample.Visible = true;
                    llColorTo.Enabled = true;
                    labelColorToSample.Visible = true;
                    break;
                default:
                    llColor.Text = "Color";
                    llColor.Enabled = false;
                    labelColorSample.Visible = false;
                    llColorTo.Enabled = false;
                    labelColorToSample.Visible = false;
                    break;
            }
            UpdateDescription();
        }

        private void comboTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbToValue.Enabled = ((TriggerEntityTag)comboTo.SelectedItem).NeedsValue;
            UpdateDescription();
        }

        private void tbWhoValue_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void tbWhatValue_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void tbToValue_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void nudPercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void tbWhichValue_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void nudDifferenceBetween_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void comboDifferenceBetween_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            OnDifferenceChanged(comboDifferenceBetween, nudDifferenceBetween);
        }

        private void comboWhichBetween_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbWhichValueBetween.Enabled = ((TriggerEntityTag)comboWhichBetween.SelectedItem).NeedsValue;
            UpdateDescription();
        }

        private void tbWhichValueBetween_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void pbColor_MouseClick(object sender, MouseEventArgs e)
        {
            SelectColor();
        }

        private void llColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectColor();
        }

        private void labelColorToSample_Click(object sender, EventArgs e)
        {
            SelectToColor();
        }

        private void llColorTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectToColor();
        }

        private void SelectColor()
        {
            NamedColorSelectorForm form = new NamedColorSelectorForm(m_trigger.ColorElement);
            if (form.ShowDialog() == DialogResult.OK)
                m_trigger.ColorElement = form.Element;
            UpdateColor();
            UpdateDescription();
        }

        private void SelectToColor()
        {
            NamedColorSelectorForm form = new NamedColorSelectorForm(m_trigger.ColorToElement);
            if (form.ShowDialog() == DialogResult.OK)
                m_trigger.ColorToElement = form.Element;
            UpdateColor();
            UpdateDescription();
        }

        private void UpdateColor()
        {
            labelColorSample.BackColor = m_trigger.DoColorBack;
            labelColorSample.ForeColor = m_trigger.DoColorFore;
            labelColorSample.Text = UIElementOptions.ColorString(m_trigger.DoColorFore, m_trigger.DoColorBack);
            labelColorToSample.BackColor = m_trigger.DoColorBack2;
            labelColorToSample.ForeColor = m_trigger.DoColorFore2;
            labelColorToSample.Text = UIElementOptions.ColorString(m_trigger.DoColorFore2, m_trigger.DoColorBack2);
        }

        public CharacterTrigger GetTriggerFromUI()
        {
            if (m_bUpdating)
                return m_trigger;

            CharacterTrigger trigger = new CharacterTrigger();
            trigger.Who = (TriggerWho)comboWho.SelectedIndex;
            trigger.WhoValue = tbWhoValue.Text;
            trigger.Do = (TriggerDo)comboDo.SelectedIndex;
            trigger.DoColorBack = m_trigger.DoColorBack;
            trigger.DoColorFore = m_trigger.DoColorFore;
            trigger.DoColorBack2 = m_trigger.DoColorBack2;
            trigger.DoColorFore2 = m_trigger.DoColorFore2;
            trigger.To = ((TriggerEntityTag)comboTo.SelectedItem).Entity;
            trigger.ToValue = tbToValue.Text;
            trigger.Conditions = new List<TriggerCondition>(1);
            TriggerCondition condition = new TriggerCondition((TriggerWhen)comboWhen.SelectedIndex);
            condition.What = ((TriggerEntityTag)comboWhat.SelectedItem).Entity;
            condition.WhatValue = tbWhatValue.Text;
            condition.When = (TriggerWhen)comboWhen.SelectedIndex;
            condition.WhenDiff = (TriggerDifference)comboDifference.SelectedIndex;
            condition.WhenValue = (int)nudDifference.Value;
            condition.Which = ((TriggerEntityTag)comboWhich.SelectedItem).Entity;
            condition.WhichValue = tbWhichValue.Text;
            condition.WhenValueBetween = (int)nudDifferenceBetween.Value;
            condition.WhenDiffBetween = (TriggerDifference)comboDifferenceBetween.SelectedIndex;
            condition.WhichBetween = ((TriggerEntityTag)comboWhichBetween.SelectedItem).Entity;
            condition.WhichValueBetween = tbWhichValueBetween.Text;
            trigger.Conditions.Add(condition);
            return trigger;
        }

        private void labelColorSample_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        private void comboDifference_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnDifferenceChanged(comboDifference, nudDifference);
        }

        private void OnDifferenceChanged(ComboBox combo, NumericUpDown nud)
        {
            switch ((TriggerDifference)combo.SelectedIndex)
            {
                case TriggerDifference.PercentOf:
                case TriggerDifference.LessThan:
                case TriggerDifference.MoreThan:
                    nud.Enabled = true;
                    nud.Minimum = -99999;
                    nud.Maximum = 99999;
                    break;
                default:
                    nud.Enabled = false;
                    break;
            }
            nud.Enabled = combo.SelectedIndex != (int)TriggerDifference.None;
            UpdateDescription();
        }

    }

    public enum TriggerWho
    {
        None,
        AnyCharacter,
        NameEquals,
        IndexEquals,
        ClassEquals,
        Spellcaster,
        NonSpellcaster,
        EntireParty,

        Last
    }

    public enum TriggerDifference
    {
        None,
        PercentOf,
        MoreThan,
        LessThan,

        Last
    }

    public enum TriggerEntity
    {
        Nothing,
        SpecificValue,
        AnyStat,
        StatIndex,  // based on MemoryHacker.StatOrder
        StatNamed,
        HitPoints,
        MaxHitPoints,
        SpellPoints,
        MaxSpellPoints,
        Experience,
        Gems,
        Gold,
        Food,
        Thievery,
        MeleeDamageAverage,
        RangedDamageAverage,
        ArmorClass,
        SpellLevel,
        ResistanceIndex,
        ResistanceNamed,
        BackpackItemCount,
        BackpackItemMax,
        BackpackItemNames,
        Condition,
        TabTitle,
        TestedItem,
        CurrentAlignment,
        ProperAlignment,
        CurrentLevel,
        ProperLevel,
        CurrentAge,
        ProperAge,
        ExperienceToNext,
        Name,
        CharIndex,
        Class,
        Regen,
        Poison,
        IDTrap,
        IDItem,
        Disarm,
        Dispel,
        Swimming,
        Critical,
        Deaths,
        Hide,
        Attacks,
        Won,
        Songs,
        MaxFood,
        TimeHour,
        TimeMinute,
        TimeDayOfYear,
        TimeDayOfMonth,
        TimeDayOfWeek,
        TimeYear,
        AnyBackpackItemAge,    // 0 = most recently added
        BackpackItemAge,
        AnyBackpackItemName,
        BackpackItemName,
        ExperienceRangeCurrentLevel,

        Last
    }

    public enum TriggerWhen
    {
        Equals,
        DoesNotEqual,
        Contains,
        DoesNotContain,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        IsBetween,
        IsNotBetween,
        MatchesRegex,
        DoesNotMatchRegex,

        Last
    }

    public enum TriggerDo
    {
        Nothing,
        SetColorTo,
        SetBoldFont,
        SetItalicFont,
        SetGradient,

        Last
    }

    public enum TriggerBool
    {
        And,
        Or
    }

    public class TriggerTarget
    {
        public TriggerEntity Entity;
        public string Value;
        public long BetweenMin;
        public long BetweenMax;
        public long TestedValue;

        public TriggerTarget(TriggerEntity entity, string val)
        {
            Entity = entity;
            Value = val;
        }

        public TriggerTarget(TriggerEntity entity, string val, TriggerTarget[] source)
        {
            Entity = entity;
            Value = val;
            if (source != null && source.Length > 0)
            {
                BetweenMin = source[0].BetweenMin;
                BetweenMax = source[0].BetweenMax;
                TestedValue = source[0].TestedValue;
            }
        }
    }

    public class TriggerCondition
    {
        public TriggerEntity What;
        public string WhatValue;
        public TriggerWhen When;
        public int WhenValue;
        public int WhenValueBetween;
        public TriggerDifference WhenDiff;
        public TriggerDifference WhenDiffBetween;
        public TriggerEntity Which;
        public TriggerEntity WhichBetween;
        public string WhichValue;
        public string WhichValueBetween;
        public TriggerBool Previous;

        public TriggerCondition(TriggerWhen when, TriggerBool tb = TriggerBool.And)
        {
            When = when;
            Previous = tb;
        }

        public TriggerCondition Clone()
        {
            TriggerCondition condition = new TriggerCondition(When, Previous);
            condition.What = What;
            condition.WhatValue = WhatValue;
            condition.When = When;
            condition.WhenValue = WhenValue;
            condition.WhenDiff = WhenDiff;
            condition.Which = Which;
            condition.WhichValue = WhichValue;
            condition.WhenValueBetween = WhenValueBetween;
            condition.WhenDiffBetween = WhenDiffBetween;
            condition.WhichBetween = WhichBetween;
            condition.WhichValueBetween = WhichValueBetween;
            return condition;
        }

        public TriggerCondition[] Expand(BaseCharacter baseChar)
        {
            TriggerCondition[] expanded;
            switch (What)
            {
                case TriggerEntity.AnyStat:
                    expanded = new TriggerCondition[baseChar.PrimaryStats.Length];
                    for (int i = 0; i < expanded.Length; i++)
                    {
                        expanded[i] = Clone();
                        expanded[i].What = TriggerEntity.StatIndex;
                        expanded[i].WhatValue = i.ToString();
                    }
                    return expanded;
                case TriggerEntity.AnyBackpackItemAge:
                    expanded = new TriggerCondition[baseChar.BackpackItems.Count];
                    for (int i = 0; i < expanded.Length; i++)
                    {
                        expanded[i] = Clone();
                        expanded[i].What = TriggerEntity.BackpackItemAge;
                        expanded[i].WhatValue = i.ToString();
                    }
                    return expanded;
                case TriggerEntity.AnyBackpackItemName:
                    expanded = new TriggerCondition[baseChar.BackpackItems.Count];
                    for (int i = 0; i < expanded.Length; i++)
                    {
                        expanded[i] = Clone();
                        expanded[i].What = TriggerEntity.BackpackItemName;
                        expanded[i].WhatValue = i.ToString();
                    }
                    return expanded;
                default:
                    return new TriggerCondition[] { this };
            }
        }

        public static bool IsPartyValue(TriggerEntity entity, GameNames name)
        {
            switch (entity)
            {
                case TriggerEntity.SpecificValue:
                case TriggerEntity.Deaths:
                case TriggerEntity.Won:
                case TriggerEntity.TimeHour:
                case TriggerEntity.TimeMinute:
                case TriggerEntity.TimeDayOfYear:
                case TriggerEntity.TimeDayOfMonth:
                case TriggerEntity.TimeDayOfWeek:
                case TriggerEntity.TimeYear:
                case TriggerEntity.Nothing:
                    return true;    // These values are never per-character


                case TriggerEntity.Gems:
                case TriggerEntity.Gold:
                case TriggerEntity.Food:
                case TriggerEntity.MaxFood:
                    return name == GameNames.MightAndMagic3 ||
                        name == GameNames.MightAndMagic4 ||
                        name == GameNames.MightAndMagic45;

                default:
                    return false;
            }
        }

        public bool AllPartyValues(GameNames game)
        {
            return IsPartyValue(What, game) && IsPartyValue(Which, game) && IsPartyValue(WhichBetween, game);
        }

        private static bool isBetween(long iTest, long iVal1, long iVal2)
        {
            if (iVal1 < iVal2)
                return iTest >= iVal1 && iTest <= iVal2;
            return iTest >= iVal2 && iTest <= iVal1;
        }

        private static bool isBetween(string sTest, string sVal1, string sVal2)
        {
            if (sVal1.CompareTo(sVal2) < 0)
                return sTest.CompareTo(sVal1) >= 0 && sTest.CompareTo(sVal2) <= 0;
            return sTest.CompareTo(sVal2) >= 0 && sTest.CompareTo(sVal1) <= 0;
        }

        public static bool Compare(TriggerWhen when, long iSource, long iDiff, long iDiffBetween = 0)
        {
            switch (when)
            {
                case TriggerWhen.Equals: return iSource == iDiff;
                case TriggerWhen.DoesNotEqual: return iSource != iDiff;
                case TriggerWhen.LessThan: return iSource < iDiff;
                case TriggerWhen.LessThanOrEqual: return iSource <= iDiff;
                case TriggerWhen.GreaterThan: return iSource > iDiff;
                case TriggerWhen.GreaterThanOrEqual: return iSource >= iDiff;
                case TriggerWhen.IsBetween: return isBetween(iSource, iDiff, iDiffBetween);
                case TriggerWhen.IsNotBetween: return !isBetween(iSource, iDiff, iDiffBetween);
                default: return false; // Unknown comparison fails
            }
        }

        public static bool Compare(TriggerWhen when, string strSource, string strTarget, string strTargetBetween = "")
        {
            switch (when)
            {
                case TriggerWhen.Equals: return strSource == strTarget;
                case TriggerWhen.DoesNotEqual: return strSource != strTarget;
                case TriggerWhen.LessThan: return strSource.CompareTo(strTarget) == -1;
                case TriggerWhen.LessThanOrEqual: return strSource.CompareTo(strTarget) != 1;
                case TriggerWhen.GreaterThan: return strSource.CompareTo(strTarget) == 1;
                case TriggerWhen.GreaterThanOrEqual: return strSource.CompareTo(strTarget) != -1;
                case TriggerWhen.IsBetween: return isBetween(strSource, strTarget, strTargetBetween);
                case TriggerWhen.IsNotBetween: return !isBetween(strSource, strTarget, strTargetBetween);
                default: return false; // Unknown comparison fails
            }
        }

        public static long GetDifference(long val, TriggerDifference diff, int when)
        {
            switch (diff)
            {
                case TriggerDifference.LessThan:
                    val -= when;
                    break;
                case TriggerDifference.MoreThan:
                    val += when;
                    break;
                case TriggerDifference.PercentOf:
                    val = when * val / 100;
                    break;
                default:
                    break;
            }
            return val;
        }

        public static string WhenString(TriggerWhen when, bool bShort = false)
        {
            switch (when)
            {
                case TriggerWhen.Equals: return bShort ? "=" : "equals";
                case TriggerWhen.DoesNotEqual: return bShort ? "!=" : "does not equal";
                case TriggerWhen.Contains: return "contains";
                case TriggerWhen.DoesNotContain: return bShort ? "!contains" : "does not contain";
                case TriggerWhen.MatchesRegex: return bShort ? "matches" : "matches regex";
                case TriggerWhen.DoesNotMatchRegex: return bShort ? "!matches" : "does not match regex";
                case TriggerWhen.LessThan: return bShort ? "<" : "is less than";
                case TriggerWhen.LessThanOrEqual: return bShort ? "<=" : "is less than or equal to";
                case TriggerWhen.GreaterThan: return bShort ? ">" : "is greater than";
                case TriggerWhen.GreaterThanOrEqual: return bShort ? ">=" : "is greater than or equal to";
                case TriggerWhen.IsBetween: return "is between";
                case TriggerWhen.IsNotBetween: return "is not between";
                default: return "(unknown)";
            }
        }

        public static string EntityString(TriggerEntity entity, bool bShort = false)
        {
            switch (entity)
            {
                case TriggerEntity.Nothing: return "nothing";
                case TriggerEntity.SpecificValue: return "specific value";
                case TriggerEntity.AnyStat: return bShort ? "any Stat" : "any primary stat";
                case TriggerEntity.StatIndex: return bShort ? "stat Index" : "primary stat index";
                case TriggerEntity.StatNamed: return bShort ? "stat" : "primary stat named";
                case TriggerEntity.HitPoints: return bShort ? "HP" : "hit points";
                case TriggerEntity.MaxHitPoints: return bShort ? "MaxHP" : "max hit points";
                case TriggerEntity.SpellPoints: return bShort ? "SP" : "spell points";
                case TriggerEntity.MaxSpellPoints: return bShort ? "MaxSP" : "max spell points";
                case TriggerEntity.Experience: return bShort ? "exp." : "experience";
                case TriggerEntity.Gems: return "gems";
                case TriggerEntity.Gold: return "gold";
                case TriggerEntity.Food: return "food";
                case TriggerEntity.MaxFood: return "max food";
                case TriggerEntity.Thievery: return "thievery";
                case TriggerEntity.MeleeDamageAverage: return bShort ? "avg. melee" : "average melee damage";
                case TriggerEntity.RangedDamageAverage: return bShort ? "avg. ranged" : "average ranged damage";
                case TriggerEntity.ArmorClass: return bShort ? "AC" : "armor class";
                case TriggerEntity.SpellLevel: return "spell level";
                case TriggerEntity.ResistanceIndex: return "resistance index";
                case TriggerEntity.ResistanceNamed: return "resistance named";
                case TriggerEntity.BackpackItemCount: return "backpack count";
                case TriggerEntity.BackpackItemMax: return "backpack max size";
                case TriggerEntity.BackpackItemNames: return "all backpack items";
                case TriggerEntity.Condition: return "condition";
                case TriggerEntity.TabTitle: return "tab title";
                case TriggerEntity.TestedItem: return "tested item";
                case TriggerEntity.CurrentAge: return bShort ? "age" : "current age";
                case TriggerEntity.ProperAge: return "proper age";
                case TriggerEntity.CurrentLevel: return bShort ? "level" : "current level";
                case TriggerEntity.ProperLevel: return "proper level";
                case TriggerEntity.CurrentAlignment: return bShort ? "alignment" : "current alignment";
                case TriggerEntity.ProperAlignment: return "proper alignment";
                case TriggerEntity.ExperienceToNext: return "exp. to next level";
                case TriggerEntity.ExperienceRangeCurrentLevel: return "exp. range this level";
                case TriggerEntity.Name: return "name";
                case TriggerEntity.CharIndex: return "character index";
                case TriggerEntity.Class: return "class";
                case TriggerEntity.Regen: return "regen";
                case TriggerEntity.Poison: return "poison";
                case TriggerEntity.IDTrap: return bShort ? "ID Trap" : "id trap";
                case TriggerEntity.IDItem: return bShort ? "ID Item" : "id item";
                case TriggerEntity.Disarm: return "disarm";
                case TriggerEntity.Dispel: return "dispel";
                case TriggerEntity.Swimming: return "swimming";
                case TriggerEntity.Critical: return "critical";
                case TriggerEntity.Deaths: return "deaths";
                case TriggerEntity.Hide: return "hide";
                case TriggerEntity.Attacks: return "attacks";
                case TriggerEntity.Won: return "won";
                case TriggerEntity.Songs: return "songs";
                case TriggerEntity.TimeHour: return "hour";
                case TriggerEntity.TimeMinute: return "minute";
                case TriggerEntity.TimeDayOfYear: return "day of year";
                case TriggerEntity.TimeDayOfMonth: return "day of month";
                case TriggerEntity.TimeDayOfWeek: return "day of week";
                case TriggerEntity.TimeYear: return "year";
                case TriggerEntity.AnyBackpackItemAge: return "any backpack item age";
                case TriggerEntity.BackpackItemAge: return "backpack item age index";
                case TriggerEntity.AnyBackpackItemName: return "any backpack item";
                case TriggerEntity.BackpackItemName: return "backpack item index";
                default: return "unknown";
            }
        }

        public static string EntityString(TriggerEntity entity, string strValue, bool bShort = false)
        {
            if (strValue != null && TriggerEntityTag.NeedsArguments(entity))
            {
                switch (entity)
                {
                    case TriggerEntity.SpecificValue: return strValue;
                    case TriggerEntity.StatNamed:
                    case TriggerEntity.ResistanceNamed: return String.Format("{0} \"{1}\"", EntityString(entity, bShort), strValue);
                    default: return String.Format("{0} {1}", EntityString(entity, bShort), strValue);
                }
            }
            return EntityString(entity, bShort);
        }

        public static string DifferenceString(TriggerDifference diff)
        {
            switch (diff)
            {
                case TriggerDifference.None: return "exactly";
                case TriggerDifference.PercentOf: return "percent of";
                case TriggerDifference.LessThan: return "less than";
                case TriggerDifference.MoreThan: return "more than";
                default: return String.Empty;
            }
        }

        public static string DiffBefore(TriggerDifference diff, int iVal, bool bShort)
        {
            switch (diff)
            {
                case TriggerDifference.PercentOf: return String.Format("{0}{1} of ", iVal, bShort ? "%" : " percent");
                case TriggerDifference.LessThan: return bShort ? "(" : String.Format("{0} less than ", iVal);
                case TriggerDifference.MoreThan: return bShort ? "(" : String.Format("{0} more than ", iVal);
                default: return String.Empty;
            }
        }

        public static string DiffAfter(TriggerDifference diff, int iVal, bool bShort)
        {
            switch (diff)
            {
                case TriggerDifference.LessThan: return bShort ? String.Format(" - {0})", iVal) : "";
                case TriggerDifference.MoreThan: return bShort ? String.Format(" + {0})", iVal) : "";
                default: return String.Empty;
            }
        }

        public override string ToString()
        {
            if (What == TriggerEntity.Nothing)
                return "(no condition)";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", EntityString(What, WhatValue));
            sb.AppendFormat(" {0} ", WhenString(When, true));
            sb.AppendFormat("{0}{1}{2}", DiffBefore(WhenDiff, WhenValue, true), EntityString(Which, WhichValue), DiffAfter(WhenDiff, WhenValue, true));
            if (When == TriggerWhen.IsBetween || When == TriggerWhen.IsNotBetween)
                sb.AppendFormat(" and {0}{1}{2}", DiffBefore(WhenDiffBetween, WhenValueBetween, true), EntityString(WhichBetween, WhichValueBetween), DiffAfter(WhenDiffBetween, WhenValueBetween, true));
            return sb.ToString();
        }

        public string GetXmlFragment()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                WriteXml(writer);
            }
            return sb.ToString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("condition");
            writer.WriteAttributeString("what", ((int)What).ToString());
            writer.WriteAttributeString("whatval", WhatValue);
            writer.WriteAttributeString("when", ((int)When).ToString());
            writer.WriteAttributeString("whenval", WhenValue.ToString());
            writer.WriteAttributeString("which", ((int)Which).ToString());
            writer.WriteAttributeString("whichval", WhichValue);

            writer.WriteAttributeString("whenvalbet", WhenValueBetween.ToString());
            writer.WriteAttributeString("whichbet", ((int)WhichBetween).ToString());
            writer.WriteAttributeString("whichvalbet", WhichValueBetween);
            writer.WriteAttributeString("whendiffbet", ((int)WhenDiffBetween).ToString());

            writer.WriteAttributeString("prev", ((int)Previous).ToString());
            writer.WriteAttributeString("whendiff", ((int)WhenDiff).ToString());
            writer.WriteEndElement();
        }

        public TriggerCondition(XmlElement e)
        {
            What = (TriggerEntity)Global.GetIntAttrib(e, "what", (int)TriggerEntity.Nothing);
            WhatValue = Global.GetStrAttrib(e, "whatval");
            When = (TriggerWhen)Global.GetIntAttrib(e, "when", (int)TriggerWhen.Equals);
            WhenValue = Global.GetIntAttrib(e, "whenval");
            WhenDiff = (TriggerDifference)Global.GetIntAttrib(e, "whendiff");
            Which = (TriggerEntity)Global.GetIntAttrib(e, "which", (int)TriggerEntity.Nothing);
            WhichValue = Global.GetStrAttrib(e, "whichval");

            WhenValueBetween = Global.GetIntAttrib(e, "whenvalbet");
            WhenDiffBetween = (TriggerDifference)Global.GetIntAttrib(e, "whendiffbet");
            WhichBetween = (TriggerEntity)Global.GetIntAttrib(e, "whichbet", (int)TriggerEntity.Nothing);
            WhichValueBetween = Global.GetStrAttrib(e, "whichvalbet");

            Previous = (TriggerBool)Global.GetIntAttrib(e, "prev", (int)TriggerBool.And);
        }
    }

    public class CharacterTrigger
    {
        public TriggerWho Who;
        public string WhoValue;
        public List<TriggerCondition> Conditions;
        public TriggerDo Do;
        public Color DoColorFore;
        public Color DoColorBack;
        public Color DoColorFore2;
        public Color DoColorBack2;
        public TriggerEntity To;
        public string ToValue;
        public bool Enabled;
        public bool Tested;

        public CharacterTrigger()
        {
            Who = TriggerWho.None;
            Conditions = new List<TriggerCondition>();
            Conditions.Add(new TriggerCondition(TriggerWhen.Equals));
            Do = TriggerDo.Nothing;
            DoColorFore = Color.White;
            DoColorBack = Color.Black;
            DoColorFore2 = Color.White;
            DoColorBack2 = Color.Black;
            To = TriggerEntity.Nothing;
            WhoValue = String.Empty;
            ToValue = String.Empty;
            Enabled = true;
            Tested = false;
        }

        public CharacterTrigger Clone()
        {
            CharacterTrigger ct = new CharacterTrigger();
            ct.Who = Who;
            ct.WhoValue = WhoValue;
            ct.Do = Do;
            ct.DoColorBack = DoColorBack;
            ct.DoColorFore = DoColorFore;
            ct.DoColorBack2 = DoColorBack2;
            ct.DoColorFore2 = DoColorFore2;
            ct.To = To;
            ct.ToValue = ToValue;
            ct.Enabled = Enabled;
            ct.Tested = Tested;
            if (Conditions != null)
                ct.Conditions = new List<TriggerCondition>(Conditions.Count);
            foreach (TriggerCondition condition in Conditions)
                ct.Conditions.Add(condition.Clone());
            return ct;
        }

        public UIElementOption ColorElement
        {
            get { return new UIElementOption(ColoredUIElements.TriggerItem, DoColorFore, DoColorBack); }
            set { DoColorFore = value.ForeColor; DoColorBack = value.BackColor; }
        }

        public UIElementOption ColorToElement
        {
            get { return new UIElementOption(ColoredUIElements.TriggerItem, DoColorFore2, DoColorBack2); }
            set { DoColorFore2 = value.ForeColor; DoColorBack2 = value.BackColor; }
        }

        public TriggerEntity ConditionEntity
        {
            get
            {
                if (Conditions == null || Conditions.Count < 1)
                    return TriggerEntity.Nothing;
                return Conditions[0].What;
            }
        }

        public static string WhoString(TriggerWho who, string str = null)
        {
            switch (who)
            {
                case TriggerWho.None: return "nobody";
                case TriggerWho.AnyCharacter: return "any character";
                case TriggerWho.NameEquals: return str == null ? "character named" : String.Format("character named \"{0}\"", str);
                case TriggerWho.IndexEquals:
                    if (str == null)
                        return "character number";
                    int iIndex = 0;
                    if (!Int32.TryParse(str, out iIndex))
                        return String.Format("character #ERROR");
                    return String.Format("character #{0}", iIndex);
                case TriggerWho.ClassEquals: return str == null ? "character class" : String.Format("character of class \"{0}\"", str);
                case TriggerWho.Spellcaster: return "any spellcaster";
                case TriggerWho.NonSpellcaster: return "any non-spellcaster";
                case TriggerWho.EntireParty: return "the entire party";
                default: return "unknown";
            }
        }

        public static string TriggerBoolString(TriggerBool tb)
        {
            switch (tb)
            {
                case TriggerBool.And: return "AND";
                case TriggerBool.Or: return "OR";
                default: return "<unknown>";
            }
        }

        public static string DoFull(TriggerDo tdo)
        {
            switch (tdo)
            {
                case TriggerDo.Nothing: return "Nothing";
                case TriggerDo.SetItalicFont: return "Set Italic Style";
                case TriggerDo.SetColorTo: return "Set to Color";
                case TriggerDo.SetBoldFont: return "Set Bold Style";
                case TriggerDo.SetGradient: return "Set Gradient";
                default: return "Unknown";
            }
        }

        public static string DoBefore(TriggerDo td)
        {
            switch (td)
            {
                case TriggerDo.Nothing: return "do nothing to";
                case TriggerDo.SetItalicFont:
                case TriggerDo.SetColorTo:
                case TriggerDo.SetGradient:
                case TriggerDo.SetBoldFont: return "set";
                default: return "Unknown";
            }
        }

        public static string DoAfter(TriggerDo td) { return DoAfter(td, Color.Empty, Color.Empty, Color.Empty, Color.Empty); }
        public static string DoAfter(TriggerDo td, Color fore, Color back, Color fore2, Color back2)
        {
            switch (td)
            {
                case TriggerDo.Nothing: return "";
                case TriggerDo.SetItalicFont: return " to italic";
                case TriggerDo.SetColorTo: return String.Format(" to \"{0}\"", UIElementOptions.ColorString(fore, back));
                case TriggerDo.SetGradient: return String.Format(" to gradient between \"{0}\" and \"{1}\"", UIElementOptions.ColorString(fore, back), UIElementOptions.ColorString(fore2, back2));
                case TriggerDo.SetBoldFont: return " to bold";
                default: return " unknown";
            }
        }

        public override string ToString()
        {
            if (Who == TriggerWho.None)
                return "None";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("For {0}, if ", WhoString(Who, WhoValue));
            for(int i = 0; i < Conditions.Count; i++)
            {
                if (i > 0)
                    sb.AppendFormat(" {0} ", TriggerBoolString(Conditions[i].Previous));
                sb.AppendFormat("{0}", Conditions[i].ToString());
            }
            sb.AppendFormat(", then {0} {1}{2}", DoBefore(Do), TriggerCondition.EntityString(To, ToValue), DoAfter(Do, DoColorFore, DoColorBack, DoColorFore2, DoColorBack2));
            return sb.ToString();
        }

        public string GetXmlFragment()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                WriteXml(writer);
            }
            return sb.ToString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("trigger");
            writer.WriteAttributeString("enabled", Enabled ? "1" : "0");
            writer.WriteAttributeString("who", ((int)Who).ToString());
            writer.WriteAttributeString("whoval", WhoValue);
            writer.WriteAttributeString("do", ((int)Do).ToString());
            writer.WriteAttributeString("fore", UIElementOptions.ColorString(DoColorFore));
            writer.WriteAttributeString("back", UIElementOptions.ColorString(DoColorBack));
            writer.WriteAttributeString("fore2", UIElementOptions.ColorString(DoColorFore2));
            writer.WriteAttributeString("back2", UIElementOptions.ColorString(DoColorBack2));
            writer.WriteAttributeString("to", ((int)To).ToString());
            writer.WriteAttributeString("toval", ToValue);
            foreach (TriggerCondition condition in Conditions)
                condition.WriteXml(writer);
            // "Tested" is temporary and not part of the serialized information
            writer.WriteEndElement();
        }

        public CharacterTrigger(XmlElement e)
        {
            Enabled = Global.GetIntAttrib(e, "enabled") == 1;
            Who = (TriggerWho)Global.GetIntAttrib(e, "who", (int)TriggerWho.None);
            WhoValue = Global.GetStrAttrib(e, "whoval");
            Do = (TriggerDo)Global.GetIntAttrib(e, "do", (int)TriggerDo.Nothing);
            DoColorFore = UIElementOptions.ColorFromString(Global.GetStrAttrib(e, "fore", "Black"));
            DoColorBack = UIElementOptions.ColorFromString(Global.GetStrAttrib(e, "back", "Control"));
            DoColorFore2 = UIElementOptions.ColorFromString(Global.GetStrAttrib(e, "fore2", "Black"));
            DoColorBack2 = UIElementOptions.ColorFromString(Global.GetStrAttrib(e, "back2", "Control"));
            To = (TriggerEntity)Global.GetIntAttrib(e, "to", (int)TriggerEntity.Nothing);
            ToValue = Global.GetStrAttrib(e, "toval");
            XmlNodeList conditions = e.SelectNodes("condition");
            Conditions = new List<TriggerCondition>(conditions.Count);
            foreach (XmlElement eCondition in conditions)
                Conditions.Add(new TriggerCondition(eCondition));
        }
    }

    public class TriggerEntityTag
    {
        public TriggerEntity Entity;

        public TriggerEntityTag(TriggerEntity entity)
        {
            Entity = entity;
        }

        public override string ToString() { return Global.Title(TriggerCondition.EntityString(Entity)); }

        public static bool NeedsArguments(TriggerEntity entity)
        {
            switch (entity)
            {
                case TriggerEntity.ResistanceIndex:
                case TriggerEntity.ResistanceNamed:
                case TriggerEntity.SpecificValue:
                case TriggerEntity.StatIndex:
                case TriggerEntity.StatNamed:
                    return true;
                default:
                    return false;
            }
        }

        public bool NeedsValue { get { return NeedsArguments(Entity); } }
    }
}
