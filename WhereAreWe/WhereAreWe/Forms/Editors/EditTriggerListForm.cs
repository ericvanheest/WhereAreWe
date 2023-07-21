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
    public partial class EditTriggerListForm : CommonKeyForm
    {
        private ListViewItem[] m_undoListView = null;

        public EditTriggerListForm()
        {
            InitializeComponent();
            CommonKeySelectAll += EditTriggerListForm_CommonKeySelectAll;
            CommonKeyUndo += EditTriggerListForm_CommonKeyUndo;
        }

        private void EditTriggerListForm_CommonKeyUndo(object sender, EventArgs e)
        {
            Undo();
        }

        private void EditTriggerListForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(lvTriggers);
        }

        private void cmTriggers_Opening(object sender, CancelEventArgs e)
        {
            if (cmCtxAddExample.DropDownItems.Count < 2)
                AddExamples();

            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            int iSelected = lvTriggers.SelectedItems.Count;
            bool bAnySelected = iSelected > 0;
            miCtxDuplicate.Enabled = bAnySelected;
            miCtxRemove.Enabled = bAnySelected;
            llRemove.Enabled = bAnySelected;
            llEdit.Enabled = iSelected == 1;
            miCtxEdit.Enabled = iSelected == 1;
        }

        private ToolStripMenuItem GetExampleMenuItem(string strMenu, CharacterTrigger trigger)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem(strMenu, null, OnAddExample);
            tsmi.Tag = trigger;
            return tsmi;
        }

        private CharacterTrigger GetRedTrigger(TriggerEntity what, TriggerWhen when, TriggerEntity which, int percent = 0)
        {
            CharacterTrigger trigger = new CharacterTrigger();
            trigger.Who = TriggerWho.AnyCharacter;
            trigger.Conditions = new List<TriggerCondition>();
            TriggerCondition condition = new TriggerCondition(when);
            condition.What = what;
            condition.WhenDiff = percent != 0 ? TriggerDifference.PercentOf : TriggerDifference.None;
            condition.WhenValue = percent;
            condition.Which = which;
            trigger.Conditions.Add(condition);
            trigger.Do = TriggerDo.SetColorTo;
            trigger.DoColorFore = SystemColors.ControlText;
            trigger.DoColorBack = Color.Red;
            trigger.To = TriggerEntity.TestedItem;
            return trigger;
        }

        private void AddExamples()
        {
            cmCtxAddExample.DropDownItems.Clear();
            CharacterTrigger example;

            example = GetRedTrigger(TriggerEntity.SpellPoints, TriggerWhen.LessThan, TriggerEntity.MaxSpellPoints, 30);
            example.Who = TriggerWho.Spellcaster;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Red if SP < 30%", example));

            example = GetRedTrigger(TriggerEntity.HitPoints, TriggerWhen.LessThan, TriggerEntity.MaxHitPoints, 30);
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Red if HP < 30%", example));

            example = GetRedTrigger(TriggerEntity.SpellPoints, TriggerWhen.LessThan, TriggerEntity.MaxSpellPoints, 50);
            example.Who = TriggerWho.Spellcaster;
            example.DoColorBack = Color.Yellow;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Yellow if SP < 50%", example));

            example = GetRedTrigger(TriggerEntity.HitPoints, TriggerWhen.LessThan, TriggerEntity.MaxHitPoints, 50);
            example.DoColorBack = Color.Yellow;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Yellow if HP < 50%", example));

            example = GetRedTrigger(TriggerEntity.Condition, TriggerWhen.DoesNotEqual, TriggerEntity.SpecificValue);
            example.Conditions[0].WhichValue = "Good";
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Red if condition is not good", example));

            example = GetRedTrigger(TriggerEntity.Food, TriggerWhen.LessThan, TriggerEntity.SpecificValue);
            example.Conditions[0].WhichValue = "5";
            example.DoColorBack = Color.Yellow;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Yellow if food < 5", example));

            example = GetRedTrigger(TriggerEntity.AnyStat, TriggerWhen.LessThanOrEqual, TriggerEntity.SpecificValue);
            example.Conditions[0].WhichValue = "3";
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Red if any primary stat <= 3", example));

            example = GetRedTrigger(TriggerEntity.MeleeDamageAverage, TriggerWhen.Equals, TriggerEntity.SpecificValue);
            example.Conditions[0].WhichValue = "0";
            example.Do = TriggerDo.SetBoldFont;
            example.To = TriggerEntity.TabTitle;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Bold character tab if melee damage is zero", example));

            example = GetRedTrigger(TriggerEntity.ExperienceToNext, TriggerWhen.LessThanOrEqual, TriggerEntity.SpecificValue);
            example.Conditions[0].WhichValue = "0";
            example.Do = TriggerDo.SetColorTo;
            example.To = TriggerEntity.TabTitle;
            example.DoColorBack = Color.LimeGreen;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Set character tab to green if ready to train", example));

            example = GetRedTrigger(TriggerEntity.BackpackItemCount, TriggerWhen.GreaterThanOrEqual, TriggerEntity.BackpackItemMax);
            example.Conditions[0].WhichValue = "1";
            example.Conditions[0].WhenDiff = TriggerDifference.LessThan;
            example.Conditions[0].WhenValue = 1;
            example.DoColorBack = Color.PaleGoldenrod;
            example.DoColorFore = SystemColors.ControlText;
            example.To = TriggerEntity.BackpackItemNames;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Change backpack background to pale goldenrod if nearly full", example));

            example = GetRedTrigger(TriggerEntity.MeleeDamageAverage, TriggerWhen.LessThan, TriggerEntity.SpecificValue);
            example.Who = TriggerWho.NonSpellcaster;
            example.Conditions[0].WhichValue = "2";
            example.Do = TriggerDo.SetItalicFont;
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Italic if non-caster melee damage < 2", example));

            example = GetRedTrigger(TriggerEntity.CurrentAlignment, TriggerWhen.DoesNotEqual, TriggerEntity.ProperAlignment);
            cmCtxAddExample.DropDownItems.Add(GetExampleMenuItem("Red if alignment has slipped", example));
        }

        private void OnAddExample(object sender, EventArgs e)
        {
            m_undoListView = null;
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi == null)
                return;
            CharacterTrigger trigger = tsmi.Tag as CharacterTrigger;
            if (trigger == null)
                return;

            AddTrigger(trigger);
        }

        private void AddTrigger(CharacterTrigger trigger)
        {
            m_undoListView = null;
            ListViewItem lvi = new ListViewItem(Global.Title(TriggerCondition.EntityString(trigger.ConditionEntity)));
            lvi.SubItems.Add(trigger.ToString());
            lvi.Checked = trigger.Enabled;
            lvi.Tag = trigger;
            lvTriggers.Items.Add(lvi);
        }

        private void AddEditTrigger()
        {
            EditTriggerForm form = new EditTriggerForm(new CharacterTrigger());
            if (form.ShowDialog() == DialogResult.OK)
                AddTrigger(form.Trigger);
        }

        private void UpdateLVI(ListViewItem lvi)
        {
            CharacterTrigger trigger = lvi.Tag as CharacterTrigger;
            lvi.Text = Global.Title(TriggerCondition.EntityString(trigger.ConditionEntity));
            lvi.SubItems[1].Text = trigger.ToString();
        }

        private void miCtxAdd_Click(object sender, EventArgs e)
        {
            AddEditTrigger();
        }

        private void miCtxRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        private void RemoveSelectedItems()
        {
            m_undoListView = new ListViewItem[lvTriggers.Items.Count];
            for(int i = 0; i < lvTriggers.Items.Count; i++)
                m_undoListView[i] = lvTriggers.Items[i];

            lvTriggers.BeginUpdate();
            for (int i = lvTriggers.SelectedItems.Count - 1; i >= 0; i--)
                lvTriggers.Items.RemoveAt(lvTriggers.SelectedItems[i].Index);
            lvTriggers.EndUpdate();
            UpdateEnabledState();
        }

        private void miCtxDuplicate_Click(object sender, EventArgs e)
        {
            m_undoListView = null;
            lvTriggers.BeginUpdate();
            foreach (ListViewItem lvi in lvTriggers.SelectedItems)
                AddTrigger((lvi.Tag as CharacterTrigger).Clone());
            lvTriggers.EndUpdate();
        }

        private void miCtxEdit_Click(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        private void Undo()
        {
            if (m_undoListView == null)
                return;

            ListViewItem[] m_current = new ListViewItem[lvTriggers.Items.Count];
            for (int i = 0; i < lvTriggers.Items.Count; i++)
                m_current[i] = lvTriggers.Items[i];

            lvTriggers.BeginUpdate();
            lvTriggers.Items.Clear();
            lvTriggers.Items.AddRange(m_undoListView);
            lvTriggers.EndUpdate();

            m_undoListView = m_current;
        }

        private void EditSelectedItem()
        {
            m_undoListView = null;
            if (lvTriggers.SelectedItems.Count < 1)
                return;

            CharacterTrigger trigger = lvTriggers.SelectedItems[0].Tag as CharacterTrigger;
            if (trigger == null)
                return;

            EditTriggerForm form = new EditTriggerForm(trigger);
            if (form.ShowDialog() == DialogResult.OK)
            {
                lvTriggers.SelectedItems[0].Tag = form.Trigger;
                UpdateLVI(lvTriggers.SelectedItems[0]);
            }
        }

        private void llAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddEditTrigger();
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveSelectedItems();
        }

        private void lvTriggers_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        private void lvTriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void llEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditSelectedItem();
        }

        public TriggerList GetTriggerList()
        {
            TriggerList list = new TriggerList();
            foreach (ListViewItem lvi in lvTriggers.Items)
                list.Items.Add(lvi.Tag as CharacterTrigger);
            list.Enabled = !cbDisableAllTriggers.Checked;
            return list;
        }

        public void SetTriggerList(TriggerList list)
        {
            if (list == null)
                list = new TriggerList();
            lvTriggers.BeginUpdate();
            lvTriggers.Items.Clear();
            foreach (CharacterTrigger trigger in list.Items)
                AddTrigger(trigger);
            cbDisableAllTriggers.Checked = !list.Enabled;
            Global.SizeHeadersAndContent(lvTriggers);
            lvTriggers.EndUpdate();
        }

        private void cbDisableAllTriggers_CheckedChanged(object sender, EventArgs e)
        {
            lvTriggers.Enabled = !cbDisableAllTriggers.Checked;
        }

        private void lvTriggers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            (e.Item.Tag as CharacterTrigger).Enabled = e.Item.Checked;
        }

        private void EditTriggerListForm_Load(object sender, EventArgs e)
        {
            if (!Global.Windows.IsEmpty(WindowType.EditTriggerList))
                Global.SetWindowInfo(this, Global.Windows.Get(WindowType.EditTriggerList));
        }

        private void EditTriggerListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Windows.Set(WindowType.EditTriggerList, this);
        }
    }

    [TypeConverter(typeof(TriggerListTypeConverter))]
    public class TriggerList
    {
        public List<CharacterTrigger> Items;
        public bool Enabled = true;

        public TriggerList(List<CharacterTrigger> list)
        {
            Items = new List<CharacterTrigger>(list.Count);
            foreach (CharacterTrigger trigger in list)
                Items.Add(trigger);
        }

        public TriggerList()
        {
            Items = new List<CharacterTrigger>();
        }

        public TriggerList(string strXml)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                XmlElement eTriggers = xml.SelectSingleNode("/Triggers") as XmlElement;
                Enabled = Global.GetIntAttrib(eTriggers, "enabled") == 1;
                XmlNodeList triggers = eTriggers.SelectNodes("trigger");
                Items = new List<CharacterTrigger>(triggers.Count);
                foreach (XmlElement eTrigger in triggers)
                    Items.Add(new CharacterTrigger(eTrigger));
            }
            catch (Exception ex)
            {
                Global.LogError("Unable to load TriggerList from XML: {0}", ex.Message);
            }
        }

        public string GetXml()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartDocument();
                WriteXml(writer);
                writer.WriteEndDocument();
            }
            return sb.ToString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Triggers");
            writer.WriteAttributeString("enabled", Enabled ? "1" : "0");
            foreach(CharacterTrigger trigger in Items)
                trigger.WriteXml(writer);
            writer.WriteEndElement();
        }
    }

    public class TriggerSubItem
    {
        public Color Fore;
        public Color Back;
        public Font Font;
        public ListViewItem Parent;
        public int Index;
        public ListViewItem.ListViewSubItem SubItem { get { return Index < 0 || Parent.SubItems.Count < Index ? null : Parent.SubItems[Index]; } }

        public void SetBold()
        {
            Parent.UseItemStyleForSubItems = false;
            if (SubItem != null)
                SubItem.Font = new Font(SubItem.Font, FontStyle.Bold);
        }

        public void SetItalic()
        {
            Parent.UseItemStyleForSubItems = false;
            if (SubItem != null)
                SubItem.Font = new Font(SubItem.Font, FontStyle.Italic);
        }

        public void SetColors(Color fore, Color back)
        {
            Parent.UseItemStyleForSubItems = false;
            if (SubItem != null)
            {
                SubItem.ForeColor = fore;
                if (back.Name != "Control")
                    SubItem.BackColor = back;
            }
        }

        public TriggerSubItem(ListViewItem parent, int index)
        {
            Parent = parent;
            Index = index;
            ListViewItem.ListViewSubItem si = SubItem;
            if (si != null)
            {
                Fore = si.ForeColor;
                Back = si.BackColor;
                Font = si.Font;
            }
        }

        public void Revert()
        {
            ListViewItem.ListViewSubItem si = SubItem;
            if (si != null)
            {
                si.ForeColor = Fore;
                si.BackColor = Back;
                si.Font = Font;
            }
        }

        public override int GetHashCode()
        {
            return Fore.ToArgb() ^ 
                Back.ToArgb() ^ 
                (Font.Bold ? 0x10000 : 0) ^ 
                (Font.Italic ? 0x20000 : 0) ^ 
                Index;
        }

        public override bool Equals(object obj)
        {
            TriggerSubItem tsi = obj as TriggerSubItem;
            if (obj == null)
                return false;
            return Fore == tsi.Fore &&
                Back == tsi.Back &&
                Font == tsi.Font &&
                Index == tsi.Index;
        }
    }

    public class TriggerReverter
    {
        public TriggerControls Controls;
        public Color Fore;
        public Color Back;
        public Font Font;
        public Color[] SubFore;
        public Color[] SubBack;
        public Font[] SubFont;
        public CharacterTrigger Action;
        public ColoredUIElements RevertTo;

        public TriggerReverter(TriggerControls tc, CharacterTrigger action, ColoredUIElements revert)
        {
            Controls = tc;
            Fore = tc.Ctrl.Main.ForeColor;
            Back = tc.Ctrl.Main.BackColor;
            Font = tc.Ctrl.Main.Font;
            Action = action;
            RevertTo = revert;
        }

        public TriggerReverter(TriggerControls tc, CharacterTrigger action, Color fore, Color back)
        {
            Controls = tc;
            Fore = fore;
            Back = back;
            Font = null;
            Action = action;
            RevertTo = ColoredUIElements.None;
        }

        public void Revert()
        {
            if (Controls.Ctrl.Main is ListView)
            {
                if (RevertTo == ColoredUIElements.None)
                {
                    Global.SetListViewSubItemColor(Controls.Ctrl.Main as ListView, Controls.Ctrl.Index, Controls.Ctrl.SubIndex, Fore, Back);
                    Global.SetListViewSubItemFont(Controls.Ctrl.Main as ListView, Controls.Ctrl.Index, Controls.Ctrl.SubIndex, FontStyle.Regular);
                }
                else
                    Global.SetListViewSubItem(Controls.Ctrl.Main as ListView, Controls.Ctrl.Index, Controls.Ctrl.SubIndex, RevertTo);
            }
            else if (Controls.Ctrl != null)
                Properties.Settings.Default.UIElementOptions.SetElement(Controls.Ctrl.Main, RevertTo);
        }
    }

    public class TriggerControls
    {
        public class TCSub
        {
            public int Item;
            public int SubItem;

            public TCSub(int item, int subItem)
            {
                Item = item;
                SubItem = subItem;
            }

            public override int GetHashCode()
            {
                return Item ^ SubItem;
            }

            public override bool Equals(object obj)
            {
                TCSub tcs = obj as TCSub;
                if (obj == null)
                    return false;
                return Item == tcs.Item && SubItem == tcs.SubItem;
            }
        }
        
        public TriggerControl Ctrl;
        public TCSub[] SubItems;

        public TriggerControls(TriggerControl ctrl, TriggerSubItem[] subItems)
        {
            Ctrl = ctrl;
            SubItems = new TCSub[subItems.Length];
            for (int i = 0; i < subItems.Length; i++)
                SubItems[i] = new TCSub(subItems[i] == null ? -1 : subItems[i].Parent.Index, subItems[i] ==  null ? -1 : subItems[i].Index);
        }
        
        public bool Empty { get { return Ctrl == null || (SubItems == null || SubItems.Length < 1); } }

        public override bool Equals(object obj)
        {
            TriggerControls tc = obj as TriggerControls;
            if (tc == null)
                return false;
            if (!tc.Ctrl.Equals(Ctrl))
                return false;
            if (SubItems == null)
                return tc.SubItems == null;
            if (tc.SubItems == null)
                return false;
            if (tc.SubItems.Length != SubItems.Length)
                return false;
            return SubItems.SequenceEqual(tc.SubItems);
        }

        public override int GetHashCode()
        {
            int iHash = (Ctrl == null ? 0 : Ctrl.GetHashCode());
            if (SubItems == null || SubItems.Length < 1)
                return iHash;
            foreach (TCSub tcs in SubItems)
                iHash ^= tcs.GetHashCode();
            return iHash;
        }
    }
}
