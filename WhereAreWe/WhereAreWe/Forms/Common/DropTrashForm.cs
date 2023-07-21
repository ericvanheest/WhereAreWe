using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WhereAreWe
{
    public partial class DropTrashForm : CommonKeyForm
    {
        private bool m_bInitialized = false;
        private List<BaseCharacter> m_chars = null;
        private BaseCharacter m_selectedChar = null;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bUpdatingCombo = false;
        private DropTrashOptions m_options = null;

        public enum MaterialRank
        {
            Wooden,
            Leather,
            Brass,
            Bronze,
            None,
            Glass,
            Coral,
            Crystal,
            Iron,
            Lapis,
            Pearl,
            Silver,
            Amber,
            Steel,
            Ebony,
            Gold,
            Quartz,
            Platinum,
            Ruby,
            Emerald,
            Sapphire,
            Diamond,
            Obsidian, 
            Unknown,
            Immaterial
        }

        public DropTrashForm(List<BaseCharacter> chars, BaseCharacter selected)
        {
            InitializeComponent();
            m_chars = chars;
            m_selectedChar = selected;
            comboClass.Location = nudGold.Location;
            comboItemType.Location = nudGold.Location;
            comboMaterial.Location = nudGold.Location;
            nudBonus.Location = nudGold.Location;
            NativeMethods.SetTooltipDelay(lvDiscarded, 32000);
            CommonKeySelectAll += DropTrashForm_CommonKeySelectAll;
            m_options = Properties.Settings.Default.DropTrash;
            if (m_options == null)
                m_options = new DropTrashOptions();
            m_bInitialized = true;
        }

        private void DropTrashForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(lvDiscarded);
        }

        public TrashCriteria.DropIf DropType
        {
            get { return (comboDropChoice.SelectedItem as TrashCriteria).Criteria; }

            set
            {
                for(int i = 0; i < comboDropChoice.Items.Count; i++)
                {
                    if ((comboDropChoice.Items[i] as TrashCriteria).Criteria == value)
                    {
                        comboDropChoice.SelectedIndex = i;
                        return;
                    }
                }
                comboDropChoice.SelectedIndex = 0;
            }
        }

        public int Gold
        {
            get { return (int)nudGold.Value; }
            set { nudGold.Value = value; UpdateUI(); }
        }

        public bool AllCharacters
        {
            get { return cbAllCharacters.Checked; }
            set { cbAllCharacters.Checked = value; UpdateUI(); }
        }

        public HashSet<Item> ItemsToDrop
        {
            get
            {
                HashSet<Item> list = new HashSet<Item>();
                foreach (ListViewItem lvi in lvDiscarded.Items)
                {
                    if (DropType != TrashCriteria.DropIf.Custom || lvi.Checked)
                        list.Add(lvi.Tag as Item);
                }
                return list;
            }
        }

        private void AddItem(BaseCharacter baseChar, Item item)
        {
            ListViewItem lvi = new ListViewItem(item.FullDescriptionString);
            lvi.SubItems.Add(baseChar.Name);
            lvi.SubItems.Add(item.Value.ToString());
            lvi.Tag = item;
            lvi.ToolTipText = item.MultiLineDescription;
            lvi.Checked = m_options.IsCustomTrash(baseChar.Game, item);
            lvDiscarded.Items.Add(lvi);
        }

        private void AddItems(BaseCharacter baseChar)
        {
            foreach(Item item in baseChar.BasicInventory.Items)
            {
                if (item.IsEquipped)
                    continue;
                if (!item.Trashable)
                    continue;
                if (!item.IsIdentified)
                    continue;
                switch (DropType)
                {
                    case TrashCriteria.DropIf.LessThanGold:
                        if (item.Value < Gold)
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.GreaterThanGold:
                        if (item.Value > Gold)
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.NotUsableClass:
                        if (!item.IsUsableByAny(((ClassComboItem) comboClass.SelectedItem).Class))
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.OfType:
                        if (ItemIsType(item, comboItemType.SelectedIndex))
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.NotOfType:
                        if (!ItemIsType(item, comboItemType.SelectedIndex))
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.LessQualityMaterial:
                        if (ItemQualityWorse(item, comboMaterial.SelectedIndex))
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.LessThanBonus:
                        if (ItemBonusWorse(item, (int) nudBonus.Value))
                            AddItem(baseChar, item);
                        break;
                    case TrashCriteria.DropIf.Custom:
                        AddItem(baseChar, item);
                        break;
                    default:
                        break;
                }
            }

            gbDiscards.Text = String.Format("Items that will be discarded ({0})", lvDiscarded.Items.Count);
        }

        private MaterialRank GetMaterialRank(MM45Item item)
        {
            if (item.Base.Type == ItemType.Miscellaneous)
                return MaterialRank.Immaterial;

            switch (item.Prefix)
            {
                case MM45ItemPrefixIndex.Wooden: return MaterialRank.Wooden;
                case MM45ItemPrefixIndex.Leather: return MaterialRank.Leather;
                case MM45ItemPrefixIndex.Brass: return MaterialRank.Brass;
                case MM45ItemPrefixIndex.Bronze: return MaterialRank.Bronze;
                case MM45ItemPrefixIndex.Iron: return MaterialRank.Iron;
                case MM45ItemPrefixIndex.Silver: return MaterialRank.Silver;
                case MM45ItemPrefixIndex.Steel: return MaterialRank.Steel;
                case MM45ItemPrefixIndex.Gold: return MaterialRank.Gold;
                case MM45ItemPrefixIndex.Platinum: return MaterialRank.Platinum;
                case MM45ItemPrefixIndex.Glass: return MaterialRank.Glass;
                case MM45ItemPrefixIndex.Coral: return MaterialRank.Coral;
                case MM45ItemPrefixIndex.Crystal: return MaterialRank.Crystal;
                case MM45ItemPrefixIndex.Lapis: return MaterialRank.Lapis;
                case MM45ItemPrefixIndex.Pearl: return MaterialRank.Pearl;
                case MM45ItemPrefixIndex.Amber: return MaterialRank.Amber;
                case MM45ItemPrefixIndex.Ebony: return MaterialRank.Ebony;
                case MM45ItemPrefixIndex.Quartz: return MaterialRank.Quartz;
                case MM45ItemPrefixIndex.Ruby: return MaterialRank.Ruby;
                case MM45ItemPrefixIndex.Emerald: return MaterialRank.Emerald;
                case MM45ItemPrefixIndex.Sapphire: return MaterialRank.Sapphire;
                case MM45ItemPrefixIndex.Diamond: return MaterialRank.Diamond;
                case MM45ItemPrefixIndex.Obsidian: return MaterialRank.Obsidian;
                case MM45ItemPrefixIndex.None: return MaterialRank.None;
                default: return MaterialRank.Unknown;
            }
        }

        private MaterialRank GetMaterialRank(MM3Item item)
        {
            switch (item.Material)
            {
                case MM3ItemMaterialIndex.Wooden: return MaterialRank.Wooden;
                case MM3ItemMaterialIndex.Leather: return MaterialRank.Leather;
                case MM3ItemMaterialIndex.Brass: return MaterialRank.Brass;
                case MM3ItemMaterialIndex.Bronze: return MaterialRank.Bronze;
                case MM3ItemMaterialIndex.Iron: return MaterialRank.Iron;
                case MM3ItemMaterialIndex.Silver: return MaterialRank.Silver;
                case MM3ItemMaterialIndex.Steel: return MaterialRank.Steel;
                case MM3ItemMaterialIndex.Gold: return MaterialRank.Gold;
                case MM3ItemMaterialIndex.Platinum: return MaterialRank.Platinum;
                case MM3ItemMaterialIndex.Glass: return MaterialRank.Glass;
                case MM3ItemMaterialIndex.Coral: return MaterialRank.Coral;
                case MM3ItemMaterialIndex.Crystal: return MaterialRank.Crystal;
                case MM3ItemMaterialIndex.Lapis: return MaterialRank.Lapis;
                case MM3ItemMaterialIndex.Pearl: return MaterialRank.Pearl;
                case MM3ItemMaterialIndex.Amber: return MaterialRank.Amber;
                case MM3ItemMaterialIndex.Ebony: return MaterialRank.Ebony;
                case MM3ItemMaterialIndex.Quartz: return MaterialRank.Quartz;
                case MM3ItemMaterialIndex.Ruby: return MaterialRank.Ruby;
                case MM3ItemMaterialIndex.Emerald: return MaterialRank.Emerald;
                case MM3ItemMaterialIndex.Sapphire: return MaterialRank.Sapphire;
                case MM3ItemMaterialIndex.Diamond: return MaterialRank.Diamond;
                case MM3ItemMaterialIndex.Obsidian: return MaterialRank.Obsidian;
                case MM3ItemMaterialIndex.None: return MaterialRank.None;
                default: return MaterialRank.Unknown;
            }
        }

        private bool ItemQualityWorse(Item item, int selected)
        {
            MaterialRank material = MaterialRank.Unknown;

            if (item is MM45Item)
                material = GetMaterialRank(item as MM45Item);
            else if (item is MM3Item)
                material = GetMaterialRank(item as MM3Item);
            else
                return false;

            return (material < (MaterialRank)selected);
        }

        private bool ItemBonusWorse(Item item, int val)
        {
            return (item.Bonus > 0 && item.Bonus < val);
        }

        private bool ItemIsType(Item item, int selected)
        {
            switch (selected)
            {
                case 0: return (item.Type == ItemType.Weapon ||
                                item.Type == ItemType.Missile ||
                                item.Type == ItemType.OneHandMelee ||
                                item.Type == ItemType.TwoHandMelee);
                case 1: return (item.Type == ItemType.Armor);
                case 2: return (item.Type == ItemType.Accessory);
                case 3: return (item.Type == ItemType.Miscellaneous ||
                                 item.Type == ItemType.None);
                case 4: return (item.ChargeBased);
                default: return false;
            }
        }

        private void UpdateCriteria()
        {
            m_bUpdatingCombo = true;
            comboDropChoice.Items.Clear();
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.LessThanGold));
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.GreaterThanGold));
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.NotUsableClass));
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.OfType));
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.NotOfType));
            if (m_chars != null && m_chars.Count > 0)
            {
                if (m_chars[0] is MM345BaseCharacter)
                {
                    comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.LessQualityMaterial));
                    comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.LessThanBonus));
                }
            }
            comboDropChoice.Items.Add(new TrashCriteria(TrashCriteria.DropIf.Custom));
            comboDropChoice.SelectedIndex = 0;
            m_bUpdatingCombo = false;

            UpdateFromDropChoice();
        }

        private void UpdateClass()
        {
            m_bUpdatingCombo = true;
            comboClass.Items.Clear();
            foreach(GenericClass gc in Games.Classes(m_chars == null || m_chars.Count < 1 ? GameNames.MightAndMagic1 : m_chars[0].Game))
                comboClass.Items.Add(new ClassComboItem(gc));
            comboClass.SelectedIndex = 0;
            m_bUpdatingCombo = false;
        }

        private void UpdateUI()
        {
            lvDiscarded.BeginUpdate();
            lvDiscarded.Items.Clear();

            if (AllCharacters)
            {
                foreach (BaseCharacter mmChar in m_chars)
                    AddItems(mmChar);
            }
            else if (m_selectedChar != null)
                AddItems(m_selectedChar);

            lvDiscarded.EndUpdate();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.DropTrash = new DropTrashOptions(DropType, (int) nudGold.Value, (int) nudBonus.Value,
                comboClass.SelectedIndex, comboItemType.SelectedIndex, comboMaterial.SelectedIndex, cbAllCharacters.Checked, GetAllCustomTrash());
            Close();
        }

        private Dictionary<GameNames, HashSet<string>> GetAllCustomTrash()
        {
            HashSet<string> custom = GetCustomTrash();
            GameNames game = m_chars != null && m_chars.Count > 0 ? m_chars[0].Game : GameNames.None;
            if (m_options.CustomTrash.ContainsKey(game))
                m_options.CustomTrash[game] = custom;
            else
                m_options.CustomTrash.Add(game, custom);
            return m_options.CustomTrash;
        }

        private HashSet<string> GetCustomTrash()
        {
            HashSet<string> items = new HashSet<string>();
            foreach (ListViewItem lvi in lvDiscarded.CheckedItems)
            {
                string strIndex = (lvi.Tag as Item).TrashIndex;
                if (!items.Contains(strIndex))
                    items.Add(strIndex);
            }
            return items;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DropTrashForm_Load(object sender, EventArgs e)
        {
            UpdateClass();
            UpdateCriteria();
            try
            {
                comboItemType.SelectedIndex = m_options.ItemType;
                comboMaterial.SelectedIndex = m_options.Material;
                comboClass.SelectedIndex = m_options.Class;
                Global.SetNud(nudGold, m_options.Gold);
                Global.SetNud(nudBonus, m_options.Bonus);
            }
            catch (Exception)
            {
                // Use defaults if there are errors in the saved values
            }
            cbAllCharacters.Checked = m_options.AllCharacters;
            DropType = m_options.Criteria;
        }

        private void comboDropChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bInitialized || m_bUpdatingCombo)
                return;

            UpdateFromDropChoice();
        }

        private void UpdateFromDropChoice()
        {
            labelAttribute.Visible = false;
            nudGold.Visible = false;
            nudBonus.Visible = false;
            comboClass.Visible = false;
            comboItemType.Visible = false;
            comboMaterial.Visible = false;
            lvDiscarded.CheckBoxes = false;

            switch ((comboDropChoice.SelectedItem as TrashCriteria).Criteria)
            {
                case TrashCriteria.DropIf.GreaterThanGold:
                case TrashCriteria.DropIf.LessThanGold:
                    labelAttribute.Visible = true;
                    nudGold.Visible = true;
                    break;
                case TrashCriteria.DropIf.NotUsableClass:
                    comboClass.Visible = true;
                    break;
                case TrashCriteria.DropIf.OfType:
                case TrashCriteria.DropIf.NotOfType:
                    comboItemType.Visible = true;
                    break;
                case TrashCriteria.DropIf.LessQualityMaterial:
                    comboMaterial.Visible = true;
                    break;
                case TrashCriteria.DropIf.LessThanBonus:
                    nudBonus.Visible = true;
                    labelAttribute.Visible = false;
                    break;
                case TrashCriteria.DropIf.Custom:
                    lvDiscarded.CheckBoxes = true;
                    break;
            }
            UpdateUI();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bInitialized || m_bUpdatingCombo)
                return;

            UpdateUI();
        }

        private void lvDiscarded_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvDiscarded.ListViewItemSorter = new DropItemComparer(e.Column, m_bAscendingSort);
            lvDiscarded.Sort();
        }

        private void lvDiscarded_DoubleClick(object sender, EventArgs e)
        {
            if (lvDiscarded.SelectedItems.Count < 1)
                return;

            Item item = lvDiscarded.SelectedItems[0].Tag as Item;
            ViewInfoForm.Show(item.MultiLineDescription.Trim(), String.Format("#{0} ({1}): {2}", item.Index, item.TypeString, item.Name));
        }
    }

    class DropItemComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public DropItemComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }

        public DropItemComparer(int column, bool bAscending)
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

            int returnVal = -1;
            switch (m_column)
            {
                case 0:
                    returnVal = String.Compare(lvi1.Text, lvi2.Text);
                    break;
                case 1:
                    returnVal = String.Compare(lvi1.SubItems[1].Text, lvi2.SubItems[1].Text);
                    break;
                case 2:
                    returnVal = Math.Sign((lvi1.Tag as Item).Value - (lvi2.Tag as Item).Value);
                    break;
                default:
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }

    class ClassComboItem
    {
        private GenericClass m_class;

        public GenericClass Class { get { return m_class; } set { m_class = value; } }

        public ClassComboItem(GenericClass gc)
        {
            m_class = gc;
        }

        public override string ToString() { return BaseCharacter.ClassString(m_class); }
    }

    public class TrashCriteria
    {
        public enum DropIf
        {
            LessThanGold,
            GreaterThanGold,
            NotUsableClass,
            OfType,
            NotOfType,
            LessQualityMaterial,
            LessThanBonus,
            Custom
        }

        public DropIf Criteria;

        public static string GetCriteriaString(DropIf criteria)
        {
            switch (criteria)
            {
                case DropIf.LessThanGold: return "are worth less than";
                case DropIf.GreaterThanGold: return "are worth more than";
                case DropIf.NotUsableClass: return "are not usable by class";
                case DropIf.OfType: return "are of type";
                case DropIf.NotOfType: return "are not of type";
                case DropIf.LessQualityMaterial: return "are of lesser quality than";
                case DropIf.LessThanBonus: return "have an attrib. bonus below";
                case DropIf.Custom: return "are explicitly selected";
                default: return String.Format("Unknown criteria {0}", (int)criteria);
            }
        }

        public TrashCriteria(DropIf criteria)
        {
            Criteria = criteria;
        }

        public override string ToString() { return GetCriteriaString(Criteria); }
    }
}
