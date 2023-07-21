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
                    comboWhich.Items.Add(new TriggerEntityTag(what));
                if (bTo)
                    comboTo.Items.Add(new TriggerEntityTag(what));
            }
            comboWhat.Sorted = true;
            comboWhich.Sorted = true;
            comboTo.Sorted = true;

            for (TriggerWho who = TriggerWho.None; who < TriggerWho.Last; who++)
                comboWho.Items.Add(Global.Title(CharacterTrigger.WhoString(who)));
            for (TriggerWhen when = TriggerWhen.Equals; when < TriggerWhen.Last; when++)
                comboWhen.Items.Add(TriggerCondition.WhenString(when));
            for (TriggerDo tdo = TriggerDo.Nothing; tdo < TriggerDo.Last; tdo++)
                comboDo.Items.Add(Global.Title(CharacterTrigger.DoFull(tdo)));
            for (TriggerDifference diff = TriggerDifference.None; diff < TriggerDifference.Last; diff++)
                comboDifference.Items.Add(TriggerCondition.DifferenceString(diff));

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
                comboWhen.SelectedIndex = (int)m_trigger.Conditions[0].When;
                tbWhatValue.Text = m_trigger.Conditions[0].WhatValue;
                tbWhichValue.Text = m_trigger.Conditions[0].WhichValue;
                comboDifference.SelectedIndex = (int)m_trigger.Conditions[0].WhenDiff;
                nudDifference.Value = m_trigger.Conditions[0].WhenValue;
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
            switch ((TriggerWhen)comboWhen.SelectedIndex)
            {
                case TriggerWhen.Contains:
                case TriggerWhen.DoesNotContain:
                    nudDifference.Enabled = false;
                    comboDifference.Enabled = false;
                    break;
                default:
                    comboDifference.Enabled = true;
                    nudDifference.Enabled = comboDifference.SelectedIndex != (int) TriggerDifference.None;
                    break;
            }
            UpdateDescription();
        }

        private void comboWhich_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbWhichValue.Enabled = ((TriggerEntityTag)comboWhich.SelectedItem).NeedsValue;
            UpdateDescription();
        }

        private void comboDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((TriggerDo)comboDo.SelectedIndex)
            {
                case TriggerDo.SetColorTo:
                    llColor.Enabled = true;
                    labelColorSample.Visible = true;
                    break;
                default:
                    llColor.Enabled = false;
                    labelColorSample.Visible = false;
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

        private void pbColor_MouseClick(object sender, MouseEventArgs e)
        {
            SelectColor();
            UpdateDescription();
        }

        private void llColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectColor();
            UpdateDescription();
        }

        private void SelectColor()
        {
            NamedColorSelectorForm form = new NamedColorSelectorForm(m_trigger.ColorElement);
            if (form.ShowDialog() == DialogResult.OK)
                m_trigger.ColorElement = form.Element;
            UpdateColor();
            UpdateDescription();
        }

        private void UpdateColor()
        {
            labelColorSample.BackColor = m_trigger.DoColorBack;
            labelColorSample.ForeColor = m_trigger.DoColorFore;
            labelColorSample.Text = UIElementOptions.ColorString(m_trigger.DoColorFore, m_trigger.DoColorBack);
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
            trigger.Conditions.Add(condition);
            return trigger;
        }

        private void labelColorSample_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        private void comboDifference_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((TriggerDifference)comboDifference.SelectedIndex)
            {
                case TriggerDifference.PercentOf:
                    nudDifference.Enabled = true;
                    if (nudDifference.Value < 0)
                        nudDifference.Value = 0;
                    if (nudDifference.Value > 100)
                        nudDifference.Value = 100;
                    nudDifference.Minimum = 0;
                    nudDifference.Maximum = 100;
                    break;
                case TriggerDifference.LessThan:
                case TriggerDifference.MoreThan:
                    nudDifference.Enabled = true;
                    nudDifference.Minimum = -99999;
                    nudDifference.Maximum = 99999;
                    break;
                default:
                    nudDifference.Enabled = false;
                    break;
            }
            nudDifference.Enabled = comboDifference.SelectedIndex != (int)TriggerDifference.None;
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

        Last
    }

    public enum TriggerDo
    {
        Nothing,
        SetColorTo,
        SetBoldFont,
        SetItalicFont,

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

        public TriggerTarget(TriggerEntity entity, string val)
        {
            Entity = entity;
            Value = val;
        }
    }

    public class TriggerCondition
    {
        public TriggerEntity What;
        public string WhatValue;
        public TriggerWhen When;
        public int WhenValue;
        public TriggerDifference WhenDiff;
        public TriggerEntity Which;
        public string WhichValue;
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
            return condition;
        }

        public TriggerCondition[] Expand(BaseCharacter baseChar)
        {
            switch (What)
            {
                case TriggerEntity.AnyStat:
                    TriggerCondition[] expanded = new TriggerCondition[baseChar.PrimaryStats.Length];
                    for (int i = 0; i < expanded.Length; i++)
                    {
                        expanded[i] = Clone();
                        expanded[i].What = TriggerEntity.StatIndex;
                        expanded[i].WhatValue = i.ToString();
                    }
                    return expanded;
                default:
                    return new TriggerCondition[] { this };
            }
        }

        public static string WhenString(TriggerWhen when, bool bShort = false)
        {
            switch (when)
            {
                case TriggerWhen.Equals: return bShort ? "=" : "equals";
                case TriggerWhen.DoesNotEqual: return bShort ? "!=" : "does not equal";
                case TriggerWhen.Contains: return "contains";
                case TriggerWhen.DoesNotContain: return bShort ? "!contains" : "does not contain";
                case TriggerWhen.LessThan: return bShort ? "<" : "is less than";
                case TriggerWhen.LessThanOrEqual: return bShort ? "<=" : "is less than or equal to";
                case TriggerWhen.GreaterThan: return bShort ? ">" : "is greater than";
                case TriggerWhen.GreaterThanOrEqual: return bShort ? ">=" : "is greater than or equal to";
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
                case TriggerEntity.Thievery: return "thievery";
                case TriggerEntity.MeleeDamageAverage: return bShort ? "avg. melee" : "average melee damage";
                case TriggerEntity.RangedDamageAverage: return bShort ? "avg. ranged" : "average ranged damage";
                case TriggerEntity.ArmorClass: return bShort ? "AC" : "armor class";
                case TriggerEntity.SpellLevel: return "spell level";
                case TriggerEntity.ResistanceIndex: return "resistance index";
                case TriggerEntity.ResistanceNamed: return "resistance named";
                case TriggerEntity.BackpackItemCount: return "backpack count";
                case TriggerEntity.BackpackItemMax: return "backpack max size";
                case TriggerEntity.BackpackItemNames: return "backpack items";
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
            // For Any Character, Hit Points 15 % of , then set Tab Title to bold
            if (What == TriggerEntity.Nothing)
                return "(no condition)";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", EntityString(What, WhatValue));
            sb.AppendFormat(" {0} ", WhenString(When, true));
            sb.AppendFormat("{0}{1}{2}", DiffBefore(WhenDiff, WhenValue, true), EntityString(Which, WhichValue), DiffAfter(WhenDiff, WhenValue, true));
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
        public TriggerEntity To;
        public string ToValue;
        public bool Enabled;

        public CharacterTrigger()
        {
            Who = TriggerWho.None;
            Conditions = new List<TriggerCondition>();
            Conditions.Add(new TriggerCondition(TriggerWhen.Equals));
            Do = TriggerDo.Nothing;
            DoColorFore = Color.White;
            DoColorBack = Color.Black;
            To = TriggerEntity.Nothing;
            WhoValue = String.Empty;
            ToValue = String.Empty;
            Enabled = true;
        }

        public CharacterTrigger Clone()
        {
            CharacterTrigger ct = new CharacterTrigger();
            ct.Who = Who;
            ct.WhoValue = WhoValue;
            ct.Do = Do;
            ct.DoColorBack = DoColorBack;
            ct.DoColorFore = DoColorFore;
            ct.To = To;
            ct.ToValue = ToValue;
            ct.Enabled = Enabled;
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
                case TriggerDo.SetBoldFont: return "set";
                default: return "Unknown";
            }
        }

        public static string DoAfter(TriggerDo td) { return DoAfter(td, Color.Empty, Color.Empty); }
        public static string DoAfter(TriggerDo td, Color fore, Color back)
        {
            switch (td)
            {
                case TriggerDo.Nothing: return "";
                case TriggerDo.SetItalicFont: return " to italic";
                case TriggerDo.SetColorTo: return String.Format(" to \"{0}\"", UIElementOptions.ColorString(fore, back));
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
            sb.AppendFormat(", then {0} {1}{2}", DoBefore(Do), TriggerCondition.EntityString(To, ToValue), DoAfter(Do, DoColorFore, DoColorBack));
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
            writer.WriteAttributeString("to", ((int)To).ToString());
            writer.WriteAttributeString("toval", ToValue);
            foreach (TriggerCondition condition in Conditions)
                condition.WriteXml(writer);
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
