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

namespace WhereAreWe
{
    public partial class InventoryManipulatorForm : HackerBasedForm
    {
        public List<Item>[] BackpackItems;
        private ItemBag m_bag;
        private UnfilteredBag m_bagUnfiltered = new UnfilteredBag();
        private int m_iMaxBagItems = 0;
        private List<BaseCharacter> m_characters;
        private int m_iCurrentChar = -1;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bUpdating = false;
        private bool m_bInitialized = false;
        private string m_strRosterFile = "";
        private FindBox m_findBox = null;

        public InventoryManipulatorForm()
        {
            InitializeComponent();
            NativeMethods.SetTooltipDelay(lvBag, 32000);
            NativeMethods.SetTooltipDelay(lvBackpack, 32000);
            BackpackItems = new List<Item>[8];
            for (int i = 0; i < 8; i++)
                BackpackItems[i] = new List<Item>(18);
        }

        public string RosterFile
        {
            get { return m_strRosterFile; }
            set
            {
                m_strRosterFile = value;
            }
        }

        public string LVIDescriptionText(Item item)
        {
            return item.FormatDescription(Properties.Settings.Default.BagItemFormat);
        }

        private void AddBackpackItem(Item item)
        {
            if (item == null || item.Index == 0)
                return;

            ListViewItem lvi = new ListViewItem(LVIDescriptionText(item));
            lvi.Tag = item;

            if (!item.IsIdentified && !item.RevealUnidentified && Properties.Settings.Default.HideUnidentifiedItems)
                lvi.ToolTipText = Global.UnidentifiedItemTip;
            else
                lvi.ToolTipText = item.MultiLineDescription;
            lvBackpack.Items.Add(lvi);
        }

        private void AddBagItem(Item item, bool bUpdateBagUI = true)
        {
            if (item == null || item.Index == 0)
                return;

            m_bagUnfiltered.Add(item);

            if (bUpdateBagUI)
                UpdateBagUI();
        }

        public int MaxBagItems
        {
            get { return m_iMaxBagItems; }
            set
            {
                m_iMaxBagItems = value;
                SetBagHeaderText();
            }
        }

        public ItemBag Bag
        {
            get { return m_bag; }
            set
            {
                m_bag = value;
                if (m_bag != null)
                {
                    m_bagUnfiltered = new UnfilteredBag(Bag.Items.Count);
                    foreach (Item item in Bag.Items)
                        m_bagUnfiltered.Add(item);
                    SetDefaultFilters();
                }
            }
        }

        private void SetBagHeaderText(bool bIncludeStatus = true)
        {
            if (Hacker.SeparateInventoryTypes)
            {
                int iMaxEach = MaxBagItems / 4;
                labelBagHeader.Text = String.Format("Bag: {0} weap, {1} armor, {2} acc, {3} misc, {4} filtered",
                    m_bagUnfiltered.Counts.Weapons,
                    m_bagUnfiltered.Counts.Armor,
                    m_bagUnfiltered.Counts.Accessories,
                    m_bagUnfiltered.Counts.Miscellaneous,
                    m_bagUnfiltered.Count - lvBag.Items.Count);
                if (bIncludeStatus)
                    SetStatus(String.Format("{0}/{1} (maximum {2} of each type) in the bag", m_bagUnfiltered.Count, Global.Plural(MaxBagItems, "item"), MaxBagItems / 4));
            }
            else
            {
                labelBagHeader.Text = String.Format("Bag of Holding ({0}/{1}, {2} filtered)", m_bagUnfiltered.Count, m_iMaxBagItems, m_bagUnfiltered.Count - lvBag.Items.Count);
                if (bIncludeStatus)
                    SetStatus(String.Format("{0}/{1} in the bag", m_bagUnfiltered.Count, Global.Plural(MaxBagItems, "item")));
            }
        }

        public void SetCharacters(List<BaseCharacter> characters, int iCurrentChar)
        {
            m_characters = characters;

            if (m_characters == null)
                return;
            if (iCurrentChar < 0 || iCurrentChar >= m_characters.Count)
                return;

            comboCharacter.Items.Clear();
            for (int i = 0; i < m_characters.Count; i++)
            {
                comboCharacter.Items.Add(String.Format("{0}: {1}", i + 1, m_characters[i].Name));
                BackpackItems[i] = m_characters[i].BackpackItems;
            }

            comboCharacter.SelectedIndex = iCurrentChar;

            SetCurrentCharacter(iCurrentChar);
        }

        private void SetCurrentCharacter(int iPosition)
        {
            m_iCurrentChar = iPosition;

            UpdateListViewFromBackpack();
        }

        private void UpdateBackpackHeader()
        {
            if (Hacker.SeparateInventoryTypes)
                chBackpackItem.Text = new InventoryItemCounts(lvBackpack.Items).GetShortHeaderString();
            else
                chBackpackItem.Text = String.Format("Backpack ({0})", m_characters[m_iCurrentChar].Name);
        }

        public void UpdateListViewFromBackpack()
        {
            lvBackpack.BeginUpdate();
            lvBackpack.Items.Clear();
            foreach (Item item in BackpackItems[m_iCurrentChar])
                AddBackpackItem(item);
            UpdateBackpackHeader();
            lvBackpack.EndUpdate();
        }

        public void UpdateUI()
        {
            btnBrowseRoster.Visible = m_main.Hacker.Game != GameNames.MightAndMagic2;   // MM2's entire roster is always in memory
            scUsableBy.Panel1Collapsed = Games.IsBardsTale(m_main.Hacker.Game);

            UpdateBagUI();
            SetTitle();

            if (m_iCurrentChar == -1)
                return;

            UpdateListViewFromBackpack();
        }

        private void SetTitle()
        {
            if (Hacker.CurrentRoster != null)
                Text = "Bag of Holding: " + Hacker.CurrentRoster.FileName;
            else
                Text = "Bag of Holding";
        }

        private void AddFilterItem(ListView lv, string str, object filter)
        {
            ListViewItem lvi = new ListViewItem(str);
            lvi.Tag = filter;
            lvi.Checked = true;
            lv.Items.Add(lvi);
        }

        private void AddFilterItem(ListView lv, GenericClass gc)
        {
            AddFilterItem(lv, BaseCharacter.ClassString(gc), gc);
        }

        private void AddFilterItem(ListView lv, GenericAlignmentValue ga)
        {
            AddFilterItem(lv, BaseCharacter.AlignmentString(ga), ga);
        }

        public void SetDefaultFilters()
        {
            if (Bag != null)
            {
                m_bUpdating = true;
                lvFilterClass.Items.Clear();
                foreach (GenericClass gc in Games.Classes(m_bag.Game))
                    AddFilterItem(lvFilterClass, gc);

                lvFilterAlign.Items.Clear();
                foreach(GenericAlignmentValue ga in Games.Alignments(m_bag.Game))
                    AddFilterItem(lvFilterAlign, ga);

                m_bUpdating = false;

                UpdateBagUI();
            }
        }

        private bool ItemFiltered(Item item)
        {
            if (item.NotUsable)
                return false;

            bool bUsable = false;
            foreach (ListViewItem lvi in lvFilterClass.CheckedItems)
            {
                if (item.IsUsableByAny(lvi.Tag))
                    bUsable = true;
            }
            if (!bUsable)
                return true;

            if (Hacker.UsesAlignment)
            {
                bUsable = false;
                foreach (ListViewItem lvi in lvFilterAlign.CheckedItems)
                {
                    if (item.IsUsableByAny(lvi.Tag))
                        bUsable = true;
                }
            }
            return !bUsable;
        }

        public void UpdateBagUI(bool bIncludeStatus = true)
        {
            if (!m_bInitialized)
                return;

            if (m_bUpdating)
                return;

            if (m_bagUnfiltered == null)
                return;

            lvBag.BeginUpdate();

            lvBag.Items.Clear();

            List<ListViewItem> list = new List<ListViewItem>();

            foreach (Item item in m_bagUnfiltered)
            {
                if (ItemFiltered(item))
                    continue;

                ListViewItem lvi = new ListViewItem(LVIDescriptionText(item));
                if (!item.IsIdentified && !item.RevealUnidentified && Properties.Settings.Default.HideUnidentifiedItems)
                {
                    lvi.ToolTipText = Global.UnidentifiedItemTip;
                    lvi.SubItems.Add("?");
                    lvi.SubItems.Add("?");
                    lvi.SubItems.Add("?");
                    lvi.SubItems.Add("?");
                    lvi.SubItems.Add("?");
                }
                else
                {
                    lvi.SubItems.Add(item.TypeString);
                    lvi.SubItems.Add(item.UsableString);
                    lvi.SubItems.Add(item.UsableByAlignment);
                    lvi.SubItems.Add(item.LargestBonusEffect);
                    lvi.SubItems.Add(Global.AddPlus(item.LargestBonus, false));
                    lvi.ToolTipText = item.MultiLineDescription;
                }
                lvi.Tag = item;
                list.Add(lvi);
            }

            lvBag.Items.AddRange(list.ToArray());

            lvBag.EndUpdate();

            SetBagHeaderText(bIncludeStatus);
        }

        private void UpdateBackpackFromListView(int iChar)
        {
            BackpackItems[m_iCurrentChar].Clear();
            foreach (ListViewItem lvi in lvBackpack.Items)
                BackpackItems[m_iCurrentChar].Add(lvi.Tag as Item);
        }

        private void UpdateUnfilteredListFromUI()
        {
            m_bagUnfiltered.Clear();
            foreach (ListViewItem lvi in lvBag.Items)
            {
                m_bagUnfiltered.Add(lvi.Tag as Item);
            }
        }

        public void UpdateFromUI()
        {
            if (m_bagUnfiltered.Count - lvBag.Items.Count == 0)
                UpdateUnfilteredListFromUI();

            UpdateBackpackFromListView(m_iCurrentChar);

            Bag.Items.Clear();
            foreach (Item item in m_bagUnfiltered)
                Bag.Items.Add(item);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!scBagFind.Panel2Collapsed && tbFind.Focused)
                m_findBox.Next(sender, new BoolHandlerEventArgs(false));
            else
            {
                UpdateFromUI();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void InventoryManipulatorForm_Load(object sender, EventArgs e)
        {
            m_bInitialized = true;
            m_findBox = new FindBox(scBagFind, tbFind, FindBox.ListViewFindFunction, lvBag);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;

            scBagFind.Panel2Collapsed = true;

            WindowInfo info = Global.Windows.Get(WindowType.InventoryManipulator);
            if (info.NormalSize == Rectangle.Empty)
                Global.CenterForm(this, m_main.PartyForm.RectangleToScreen(m_main.PartyForm.DisplayRectangle));
            else
                Global.SetWindowInfo(this, info);
            UpdateUI();
        }

        private void lvBag_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvBag_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvBag_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                return;

            MoveToBag(e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection);
        }

        private void lvBag_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Capture = true;
            DoDragDrop(lvBag.SelectedItems, DragDropEffects.Move);
        }

        private void lvBackpack_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                return;

            ListView.SelectedListViewItemCollection items = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;

            SetBackpackResult result = SetBackpackResult.Success;
            lvBag.BeginUpdate();
            lvBackpack.BeginUpdate();
            foreach (ListViewItem lvi in items)
            {
                if (lvi.ListView == lvBackpack)
                    continue;

                SetBackpackResult res = MoveToBackpack(lvi);
                if (res != SetBackpackResult.Success)
                    result = res;
            }
            SetBagHeaderText();
            UpdateBackpackHeader();
            lvBag.EndUpdate();
            lvBackpack.EndUpdate();

            if (result == SetBackpackResult.InsufficientSpace)
                MessageBox.Show("There is not enough free space in the backpack to hold all of the items.", "Insufficient Space", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lvBackpack_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvBackpack_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvBackpack_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Capture = true;
            DoDragDrop(lvBackpack.SelectedItems, DragDropEffects.Move);
        }

        private void lvBackpack_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control)
                    {
                        lvBackpack.BeginUpdate();
                        foreach (ListViewItem lvi in lvBackpack.Items)
                            lvi.Selected = true;
                        lvBackpack.EndUpdate();
                    }
                    break;
                default:
                    break;
            }
        }

        private void lvBag_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control)
                    {
                        lvBag.BeginUpdate();
                        foreach (ListViewItem lvi in lvBag.Items)
                            lvi.Selected = true;
                        lvBag.EndUpdate();
                    }
                    break;
                default:
                    break;
            }
        }

        private void DeleteSelectedBagItems()
        {
            if (MessageBox.Show(String.Format("Destroy the {0} selected item{1} in the Bag of Holding?", lvBag.SelectedItems.Count, lvBag.SelectedItems.Count == 1 ? "" : "s"),
                "Destroy Items?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
                return;

            foreach (ListViewItem lvi in lvBag.SelectedItems)
                m_bagUnfiltered.Remove(lvi.Tag as Item);

            UpdateBagUI();
        }

        private void DeleteSelectedBackpackItems()
        {
            if (MessageBox.Show(String.Format("Destroy the {0} selected item{1} in the inventory of character \"{2}\"?",
                lvBackpack.SelectedItems.Count, lvBackpack.SelectedItems.Count == 1 ? "" : "s", m_characters[m_iCurrentChar].Name),
                "Destroy Items?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
                return;

            lvBackpack.BeginUpdate();
            foreach (ListViewItem lvi in lvBackpack.SelectedItems)
                lvi.Remove();
            lvBackpack.EndUpdate();
        }

        private void lvBackpack_DoubleClick(object sender, EventArgs e)
        {
            MoveToBag(lvBackpack.SelectedItems);
        }

        private void lvBag_DoubleClick(object sender, EventArgs e)
        {
            SetBackpackResult result = SetBackpackResult.Success;

            foreach (ListViewItem lvi in lvBag.SelectedItems)
            {
                SetBackpackResult res = MoveToBackpack(lvi);
                if (res != SetBackpackResult.Success)
                    result = res;
            }

            if (result == SetBackpackResult.InsufficientSpace)
                MessageBox.Show("There is not enough free space in the backpack to hold all of the items.", "Insufficient Space", MessageBoxButtons.OK, MessageBoxIcon.Error);

            SetBagHeaderText();
            UpdateBackpackHeader();
        }

        private bool CanMoveToBag(ListViewItem lvi)
        {
            if (Hacker.SeparateInventoryTypes)
            {
                // MM4/5 has different inventory slots for different item types, so it needs more attention
                // Four base item types (weapon, armor, accessory, misc)
                ItemType type = (lvi.Tag as MM45Item).Base.Type;
                if (m_bagUnfiltered.Counts.CountType(type) >= MaxBagItems / 4)
                {
                    SetStatus(String.Format("There are too many items of type \"{0}\" ({1}) in the bag!", MM45ItemBase.TypeString(type), MaxBagItems / 4));
                    return false;
                }
            }

            if (m_bagUnfiltered.Count >= MaxBagItems)
            {
                SetStatus(String.Format("There are too many items ({0}) in the bag!", MaxBagItems));
                return false;      // Too many items in the bag
            }

            return true;
        }

        private void SetStatus(string str)
        {
            labelStatus.Text = str;
        }

        private void MoveToBag(IEnumerable items)
        {
            if (m_characters[m_iCurrentChar].Name.ToLower() == "inventory")
            {
                MessageBox.Show("You may not move items from an \"Inventory\" character to the bag of holding.  That would cause trouble.", "Invalid move", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lvBackpack.BeginUpdate();

            bool bError = false;

            int iCount = 0;
            foreach (ListViewItem lvi in items)
            {
                if (lvi.ListView == lvBag)
                    continue;       // Already in the bag

                if (!CanMoveToBag(lvi))
                {
                    bError = true;
                    continue;
                }

                iCount++;

                lvi.Remove();
                AddBagItem(lvi.Tag as Item, false);
            }

            lvBackpack.EndUpdate();

            UpdateBagUI(!bError);
            UpdateBackpackHeader();
        }

        private SetBackpackResult MoveToBackpack(ListViewItem lvi)
        {
            if (m_characters[m_iCurrentChar].Name.ToLower() == "inventory")
            {
                MessageBox.Show("You may not move items to an \"Inventory\" character from the bag of holding.  That would cause trouble.", "Invalid move", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SetBackpackResult.NotImplemented;
            }

            if (lvBackpack.Items.Count >= m_characters[m_iCurrentChar].MaxBackpackSize)
                return SetBackpackResult.InsufficientSpace;

            Item itemAdd = lvi.Tag as Item;

            if (Hacker.SeparateInventoryTypes)
            {
                // Might and Magic 4/5 have separate categories of inventory that must be checked independently
                InventoryItemCounts counts = new InventoryItemCounts();
                foreach (ListViewItem item in lvBackpack.Items)
                    counts.Add(item.Tag as Item);

                counts.Add(itemAdd);

                if (counts.Weapons > m_characters[m_iCurrentChar].MaxBackpackWeapons)
                    return SetBackpackResult.InsufficientSpace;
                if (counts.Armor > m_characters[m_iCurrentChar].MaxBackpackArmor)
                    return SetBackpackResult.InsufficientSpace;
                if (counts.Accessories > m_characters[m_iCurrentChar].MaxBackpackAccessories)
                    return SetBackpackResult.InsufficientSpace;
                if (counts.Miscellaneous > m_characters[m_iCurrentChar].MaxBackpackMisc)
                    return SetBackpackResult.InsufficientSpace;
            }

            lvi.Remove();
            m_bagUnfiltered.Remove(itemAdd);
            AddBackpackItem(itemAdd);

            return SetBackpackResult.Success;
        }

        private void comboCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_iCurrentChar > -1)
                UpdateBackpackFromListView(m_iCurrentChar);
            SetCurrentCharacter(comboCharacter.SelectedIndex);
        }

        private void lvBag_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvBag.ListViewItemSorter = new BagItemComparer(e.Column, m_bAscendingSort);
            lvBag.Sort();
        }

        private void cmBagDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedBagItems();
        }

        private void miBackpackDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedBackpackItems();
        }

        private void lvFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvFilterClass.SelectedItems.Clear();
        }

        private void lvFilter_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!m_bUpdating)
                UpdateBagUI();
        }

        private void lvFilterAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvFilterAlign.SelectedItems.Clear();
        }

        private void lvFilterAlign_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!m_bUpdating)
                UpdateBagUI();
        }

        private void btnBrowseRoster_Click(object sender, EventArgs e)
        {
            Hacker.BrowseRosterFile();
            Bag = Hacker.ReadBagFromRoster();
            UpdateUI();
        }

        private void llAllAlign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdating = true;
            foreach (ListViewItem lvi in lvFilterAlign.Items)
                lvi.Checked = true;
            m_bUpdating = false;
            UpdateBagUI();
        }

        private void llNoneAlign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdating = true;
            foreach (ListViewItem lvi in lvFilterAlign.Items)
                lvi.Checked = false;
            m_bUpdating = false;
            UpdateBagUI();
        }

        private void llAllClass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdating = true;
            foreach (ListViewItem lvi in lvFilterClass.Items)
                lvi.Checked = true;
            m_bUpdating = false;
            UpdateBagUI();
        }

        private void llNoneClass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdating = true;
            foreach (ListViewItem lvi in lvFilterClass.Items)
                lvi.Checked = false;
            m_bUpdating = false;
            UpdateBagUI();
        }

        private void llAddAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoveToBag(lvBackpack.Items);
        }

        private void InventoryManipulatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                    int iIndex = e.KeyData - Keys.D1;
                    if (iIndex < comboCharacter.Items.Count)
                        comboCharacter.SelectedIndex = iIndex;
                    break;
                case Keys.Escape:
                    if (!scBagFind.Panel2Collapsed)
                        m_findBox.HideFindBox();
                    else
                        btnCancel_Click(this, new EventArgs());
                    break;
                default:
                    break;
            }
        }

        private void EditDisplayFormat()
        {
            EditItemDisplayFormatForm form = new EditItemDisplayFormatForm();
            form.DisplayFormat = Properties.Settings.Default.BagItemFormat;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BagItemFormat = form.DisplayFormat;
                UpdateUI();
            }
        }

        private void miBackpackItemDisplayFormat_Click(object sender, EventArgs e)
        {
            EditDisplayFormat();
        }

        private void miBagItemDisplayFormat_Click(object sender, EventArgs e)
        {
            EditDisplayFormat();
        }
    }

    class BagItemComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public BagItemComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }

        public BagItemComparer(int column, bool bAscending)
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
                default:
                    returnVal = Compare(lvi1.Tag as Item, lvi2.Tag as Item);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }

        public int Compare(Item item1, Item item2)
        {
            switch (m_column)
            {
                case 0: return String.Compare(item1.DescriptionString, item2.DescriptionString);
                case 1: return String.Compare(item1.TypeString, item2.TypeString);
                case 2: return String.Compare(item1.UsableString, item2.UsableString);
                case 3: return String.Compare(item1.UsableByAlignment, item2.UsableByAlignment);
                case 4:
                    int iResult = String.Compare(item1.LargestBonusEffect, item2.LargestBonusEffect);
                    if (iResult == 0)
                        iResult = Math.Sign(item1.LargestBonus - item2.LargestBonus);
                    return iResult;
                case 5: return Math.Sign(item1.LargestBonus - item2.LargestBonus);
                default: return -1;
            }

        }
    }

    public class UnfilteredBag : IEnumerable<Item>
    {
        private List<Item> Items;
        public InventoryItemCounts Counts;

        public UnfilteredBag()
        {
            Items = new List<Item>();
            Counts = new InventoryItemCounts();
        }

        public UnfilteredBag(int iCapacity)
        {
            Items = new List<Item>(iCapacity);
            Counts = new InventoryItemCounts();
        }

        public void Add(Item item)
        {
            Items.Add(item);
            Counts.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
            Counts.Clear();
        }

        public void Remove(Item item)
        {
            if (!Items.Contains(item))
                return;

            Counts.Remove(item);
            Items.Remove(item);
        }

        public IEnumerator<Item> GetEnumerator() { return Items.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count { get { return Items.Count; } }
    }
}
