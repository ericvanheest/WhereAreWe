using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditInventoryTableForm : CommonKeyForm
    {
        private BaseCharacter m_char;
        private byte[] m_bytesMasterTable;
        private bool m_bInvAscendingSort = true;
        private int m_iLastInvSortColumn = -1;
        private bool m_bItemAscendingSort = true;
        private int m_iLastItemSortColumn = -1;
        private bool m_bAnyChanges = false;

        private FindBox m_findBox = null;

        public EditInventoryTableForm()
        {
            InitializeComponent();
            NativeMethods.SetTooltipDelay(lvCharInventory, 32000);
            NativeMethods.SetTooltipDelay(lvMasterItems, 32000);
        }

        public BaseCharacter Character
        {
            get { return m_char; }
            set
            {
                m_char = value;
                if (m_char == null)
                {
                    labelCharName.Text = "NULL";
                    return;
                }
                labelCharName.Text = m_char.Name;
                Inventory inv = m_char.BasicInventory;
                UpdateCharInventory(inv);
            }
        }

        public GameNames Game { get { return m_char == null ? GameNames.None : m_char.Game; } }

        public int SelectedItem
        {
            get
            {
                if (lvCharInventory.SelectedItems.Count < 1)
                    return -1;
                return (lvCharInventory.SelectedItems[0].Tag as EOBItem).ItemListIndex;
            }
            set
            {
                foreach (ListViewItem lvi in lvCharInventory.Items)
                {
                    EOBItem item = lvi.Tag as EOBItem;
                    if (item == null)
                        continue;
                    if (value == -1 && item.Available && item.EOBInvLocation >= EOBEquipPosition.Backpack1 && item.EOBInvLocation <= EOBEquipPosition.Backpack14)
                    {
                        Global.SelectOnly(lvCharInventory, lvi, false);
                        return;
                    }
                    if (((EOBItem)lvi.Tag).ItemListIndex != value)
                        continue;
                    Global.SelectOnly(lvCharInventory, lvi, false);
                    ShowSelectedItemInMasterList(false);
                    return;
                }
            }
        }

        public List<Item> Inventory
        {
            get
            {
                List<Item> list = new List<Item>();
                foreach (ListViewItem lvi in lvCharInventory.Items)
                    list.Add(lvi.Tag as Item);
                return list;
            }

            set
            {
                UpdateCharInventory(value);
            }

        }

        public byte[] GetBackpackBytes()
        {
            byte[] bytes = Global.NullBytes(EOB.Offsets.InventoryLength);
            foreach (ListViewItem lvi in lvCharInventory.Items)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null || item.ItemListIndex < 0)
                    continue;

                int iPos = (int)item.EOBInvLocation * 2;
                if (iPos < 0 || iPos >= bytes.Length - 1)
                    continue;

                byte[] bytesShort = BitConverter.GetBytes((short)item.ItemListIndex);
                Buffer.BlockCopy(bytesShort, 0, bytes, iPos, bytesShort.Length);
            }

            return bytes;
        }

        public byte[] MasterItemTable
        {
            get
            {
                byte[] bytes = new byte[(lvMasterItems.Items.Count + 1) * 14];
                foreach (ListViewItem lvi in lvMasterItems.Items)
                {
                    EOBItem item = lvi.Tag as EOBItem;
                    if (item == null)
                        continue;
                    byte[] bytesSingleItem = item.GetItemListBytes();
                    if (item.ItemListIndex * bytesSingleItem.Length > bytes.Length - bytesSingleItem.Length)
                        continue;
                    Buffer.BlockCopy(bytesSingleItem, 0, bytes, item.ItemListIndex * bytesSingleItem.Length, bytesSingleItem.Length);
                }
                return bytes;
            }
            set
            {
                m_bytesMasterTable = new byte[value.Length];
                Buffer.BlockCopy(value, 0, m_bytesMasterTable, 0, value.Length);
                UpdateItemTable(m_bytesMasterTable);
            }
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(ActiveControl);
            return true;
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
        }

        private void UpdateCharInventory(Inventory inv)
        {
            if (inv == null)
                return;
            UpdateCharInventory(inv.Items);
        }

        private void UpdateCharInventory(ListViewItem lvi)
        {
            EOBItem item = lvi.Tag as EOBItem;
            if (item == null)
                return;
            lvi.Text = Global.Title(EOBItem.EOBInvPositionString(item.EOBInvLocation));
            if (item.Available)
            {
                lvi.SubItems[1].Text = "";
                lvi.SubItems[2].Text = "";
            }
            else
            {
                lvi.SubItems[1].Text = item.ItemListIndex.ToString();
                lvi.SubItems[2].Text = item.Name;
            }
            lvi.ToolTipText = item.Available ? null : item.MultiLineDescription;
        }

        private void UpdateCharInventory(List<Item> items)
        {
            lvCharInventory.Items.Clear();
            if (items == null)
                return;

            Dictionary<EOBEquipPosition, EOBItem> dict = new Dictionary<EOBEquipPosition, EOBItem>();
            foreach (EOBItem item in items)
            {
                if (!dict.ContainsKey(item.EOBInvLocation))
                    dict.Add(item.EOBInvLocation, item);
            }

            lvCharInventory.BeginUpdate();
            for (EOBEquipPosition pos = EOBEquipPosition.RightHand; pos < EOBEquipPosition.Last; pos++)
            {
                ListViewItem lvi = null;
                if (dict.ContainsKey(pos))
                    lvi = GetCharInvLVI(dict[pos]);
                else
                    lvi = GetCharInvLVI(pos);
                if (lvi == null)
                    continue;
                lvCharInventory.Items.Add(lvi);
            }
            lvCharInventory.EndUpdate();
        }

        private ListViewItem GetCharInvLVI(EOBEquipPosition pos)
        {
            ListViewItem lvi = new ListViewItem(Global.Title(EOBItem.EOBInvPositionString(pos)));
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.Tag = EOBItem.CreateEmpty(Game, pos);
            lvi.ToolTipText = null;
            return lvi;
        }

        private ListViewItem GetCharInvLVI(EOBItem item)
        {
            if (item == null)
                return null;

            ListViewItem lvi = new ListViewItem(Global.Title(EOBItem.EOBInvPositionString(item.EOBInvLocation)));
            lvi.SubItems.Add(item.ItemListIndex.ToString());
            lvi.SubItems.Add(item.DescriptionString);
            lvi.Tag = item;
            lvi.ToolTipText = item.Available ? null : item.MultiLineDescription;
            return lvi;
        }

        private void UpdateItemTable(byte[] bytes)
        {
            lvMasterItems.Items.Clear();
            if (bytes == null)
                return;

            lvMasterItems.BeginUpdate();
            for (int i = 0; i <= (bytes.Length - 14); i += 14)
            {
                ListViewItem lvi = GetMasterItemLVI(bytes, i);
                if (lvi == null)
                    continue;
                if ((lvi.Tag as EOBItem).ItemListIndex == 0)
                    continue;
                lvMasterItems.Items.Add(lvi);
            }
            lvMasterItems.EndUpdate();
        }

        private void UpdateMasterListLVI(ListViewItem lvi)
        {
            EOBItem item = lvi.Tag as EOBItem;
            if (item == null)
                return;

            Global.UpdateSubItem(lvi, 0, item.ItemListIndex.ToString());
            Global.UpdateSubItem(lvi, 1, item.Available ? "" : item.Identified ? "Yes" : "");
            Global.UpdateSubItem(lvi, 2, item.Available ? "" : item.Magical ? "Yes" : "");
            Global.UpdateSubItem(lvi, 3, item.Available ? "" : item.ChargeBased ? item.Charges.ToString() : "");
            Global.UpdateSubItem(lvi, 4, item.Available ? "<empty>" : EOBItem.GetName(item.ItemIndex));
            Global.UpdateSubItem(lvi, 5, item.Available ? "" : item.DescriptionString);
            Global.UpdateSubItem(lvi, 6, item.Available ? "" : item.Damage.Quantity == 0 ? "" : item.DamageStringFull);
            Global.UpdateSubItem(lvi, 7, item.Available ? "" : item.AC == 0 ? "" : (item.AC - item.Modifier).ToString());
            Global.UpdateSubItem(lvi, 8, item.Available ? "" : EOBItem.LocationString(item.Floor, item.Location, item.MapIndex, true));
            Global.UpdateSubItem(lvi, 9, item.Available ? "" : EOBItem.ModifierString(item.ItemIndex, item.Modifier));
            Global.UpdateSubItem(lvi, 10, item.Available ? "" : item.PrevItem < 1 || item.ItemListIndex == item.PrevItem ? "" : item.PrevItem.ToString());
            Global.UpdateSubItem(lvi, 11, item.Available ? "" : item.NextItem < 1 || item.ItemListIndex == item.NextItem ? "" : item.NextItem.ToString());

            lvi.ToolTipText = (item.Available ? null : item.MultiLineDescription);
        }

        private ListViewItem GetMasterItemLVI(byte[] bytes, int offset)
        {
            if (bytes == null || offset > bytes.Length - 14)
                return null;

            EOBItem item = EOBItem.FromItemListBytes(Game, bytes, offset);

            ListViewItem lvi = new ListViewItem(item.ItemListIndex.ToString());
            lvi.Tag = item;
            UpdateMasterListLVI(lvi);
            return lvi;
        }

        private void lvMasterItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastItemSortColumn)
                m_bItemAscendingSort = !m_bItemAscendingSort;
            else
                m_bItemAscendingSort = true;

            m_iLastItemSortColumn = e.Column;

            lvMasterItems.ListViewItemSorter = new EOBMasterItemComparer(lvMasterItems, e.Column, m_bItemAscendingSort);
            lvMasterItems.Sort();
        }

        private void lvCharInventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastInvSortColumn)
                m_bInvAscendingSort = !m_bInvAscendingSort;
            else
                m_bInvAscendingSort = true;

            m_iLastInvSortColumn = e.Column;

            lvCharInventory.ListViewItemSorter = new EOBInventoryItemComparer(lvCharInventory, e.Column, m_bInvAscendingSort);
            lvCharInventory.Sort();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckDuplicates())
                return;
            DialogResult = DialogResult.OK;
            CloseForm();
        }

        private bool CheckDuplicates()
        {
            HashSet<int> items = new HashSet<int>();
            foreach (ListViewItem lvi in lvCharInventory.Items)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null || item.ItemListIndex == -2)
                    continue;
                if (items.Contains(item.ItemListIndex))
                {
                    if (MessageBox.Show(String.Format("The player inventory contains at least one duplicate ID ({0}).\r\n\r\n" +
                        "Duplicate IDs may cause unwanted in-game effects.  Allow the duplicate item?", item.ItemListIndex),
                        "Duplicate ID", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                    {
                        Global.DeselectAll(lvCharInventory);
                        foreach (ListViewItem lviTest in lvCharInventory.Items)
                        {
                            EOBItem itemTest = lviTest.Tag as EOBItem;
                            if (itemTest.ItemListIndex == item.ItemListIndex)
                                lviTest.Selected = true;
                        }
                        lvCharInventory.Focus();
                        return false;
                    }
                    return true;
                }
                items.Add(item.ItemListIndex);
            }
            return true;
        }

        private void CloseForm()
        {
            m_findBox.HideFindBox();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelForm();
        }

        private void CancelForm()
        {
            if (!CheckChanges())
                return;
            DialogResult = DialogResult.Cancel;
            CloseForm();
        }

        private void EditInventoryTableForm_Load(object sender, EventArgs e)
        {
            WindowInfo info = Global.Windows.Get(WindowType.EditMasterInventoryTable);
            Global.SetWindowInfo(this, info);
            Global.SetSplitterDistance(scInventoryTable, info.SplitPositions, 0);
            m_findBox = new FindBox(scMasterFind, tbFind, FindBox.ListViewFindFunction, lvMasterItems);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;
            scMasterFind.Panel2Collapsed = true;
        }

        private void EditInventoryTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Windows.Set(WindowType.EditMasterInventoryTable, new WindowInfo(this, false, new int[] { scInventoryTable.SplitterDistance }));
        }

        private void EditInventoryTableForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    if (m_findBox.Focused)
                        return;
                    CancelForm();
                    break;
            }
        }

        private bool CheckChanges()
        {
            if (m_bAnyChanges)
            {
                if (MessageBox.Show("Close this form without saving changes?", "Changes have been made", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return false;
            }
            return true;
        }

        private void miInvRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedInvItems();
        }

        private void RemoveSelectedInvItems()
        {
            foreach (ListViewItem lvi in lvCharInventory.SelectedItems)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null || item.Available)
                    continue;   // Already removed
                ReplaceCharInvItem(lvi, EOBItem.CreateEmpty(Game, item.EOBInvLocation));
            }
        }

        private void miInvChange_Click(object sender, EventArgs e)
        {
            ChangeInvItem();
        }

        private void miMasterRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format("Are you sure you want to permanently erase {0} from the master item list?\r\n\r\n" +
                    "This may cause any references to it in the game to behave in an unknown manner!",
                    Global.Plural(lvMasterItems.SelectedItems.Count, "item")), "Erase item in master list?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            RemoveSelectedMasterListItems();
        }

        private void miMasterDuplicate_Click(object sender, EventArgs e)
        {
            DuplicateSelectedMasterListItems();
        }

        private void RemoveSelectedMasterListItems()
        {
            lvMasterItems.BeginUpdate();
            foreach (ListViewItem lvi in lvMasterItems.SelectedItems)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null)
                    continue;
                EOBItem itemNew = EOBItem.CreateEmpty(Game, EOBEquipPosition.Invalid);
                itemNew.ItemListIndex = item.ItemListIndex;
                lvi.Tag = itemNew;
                UpdateMasterListLVI(lvi);
            }
            FinishUpdateMasterList();
        }

        private void FinishUpdateMasterList()
        {
            lvMasterItems.EndUpdate();
            m_bAnyChanges = true;
        }

        private void DuplicateSelectedMasterListItems()
        {
            lvMasterItems.BeginUpdate();
            List<int> newItems = new List<int>();
            foreach (ListViewItem lvi in lvMasterItems.SelectedItems)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null)
                    continue;
                for (int i = lvMasterItems.Items.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        MessageBox.Show("Unable to create duplicate; master list is full!", "Master List Full!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FinishUpdateMasterList();
                        return;
                    }
                    EOBItem itemExisting = lvMasterItems.Items[i].Tag as EOBItem;
                    if (itemExisting.Index != 0)
                        continue;

                    EOBItem itemNew = EOBItem.CreateClone(Game, item, itemExisting.ItemListIndex);
                    lvMasterItems.Items[i].Tag = itemNew;
                    UpdateMasterListLVI(lvMasterItems.Items[i]);
                    newItems.Add(i);
                    break;
                }
            }
            if (newItems.Count > 0)
            {
                int[] indices = new int[lvMasterItems.SelectedIndices.Count];
                lvMasterItems.SelectedIndices.CopyTo(indices, 0);
                foreach (int i in indices)
                    lvMasterItems.Items[i].Selected = false;
                foreach(int i in newItems)
                    lvMasterItems.Items[i].Selected = true;
                lvMasterItems.EnsureVisible(newItems[newItems.Count - 1]);
            }
            FinishUpdateMasterList();
        }

        private void miMasterPlaceInBackpack_Click(object sender, EventArgs e)
        {
            PlaceSelectedMasterItemsInBackpack();
        }

        private void ChangeInvItem()
        {
            if (lvCharInventory.SelectedItems.Count < 1)
                return;

            ListViewItem lvi = lvCharInventory.SelectedItems[0];
            EOBItem item = lvi.Tag as EOBItem;
            AttributeEditForm form = new AttributeEditForm();
            form.Attribute = new EditableAttribute((short)(item == null ? -1 : item.ItemListIndex));
            form.Text = "Change item index";
            if (form.ShowDialog() == DialogResult.OK)
            {
                int iIndex = form.Attribute.Shorts[0];
                foreach (ListViewItem lviMaster in lvMasterItems.Items)
                {
                    EOBItem itemMaster = lviMaster.Tag as EOBItem;
                    if (itemMaster == null || itemMaster.ItemListIndex != iIndex)
                        continue;

                    ReplaceCharInvItem(lvi, itemMaster);
                    return;
                }
                MessageBox.Show(String.Format("There is no item with index {0} in the master item table.", iIndex), "Item not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvCharInventory_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectedItemInMasterList(true);
        }

        private void ShowSelectedItemInMasterList(bool bEditIfNotFound = false)
        {
            if (lvCharInventory.SelectedItems.Count < 1)
                return;
            EOBItem item = lvCharInventory.SelectedItems[0].Tag as EOBItem;
            if (item == null || item.Available)
            {
                if (bEditIfNotFound)
                    ChangeInvItem();
                return;
            }
            SelectMasterItem(item.ItemListIndex);
        }

        private void SelectMasterItem(int iListIndex)
        {
            foreach (ListViewItem lvi in lvMasterItems.Items)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null)
                    continue;
                if (item.ItemListIndex == iListIndex)
                {
                    Global.DeselectAll(lvMasterItems);
                    lvi.Selected = true;
                    lvi.Focused = true;
                    lvMasterItems.EnsureVisible(lvi.Index);
                    return;
                }
            }
        }

        private void lvMasterItems_DoubleClick(object sender, EventArgs e)
        {
            PlaceSelectedMasterItemsInBackpack();
        }

        private void PlaceSelectedMasterItemsInBackpack()
        {
            if (lvMasterItems.SelectedItems.Count < 1)
                return;
            foreach (ListViewItem lvi in lvMasterItems.SelectedItems)
            {
                if (!AddItemToBackpack(lvi.Tag as EOBItem))
                    break;  // Failed; don't keep trying
            }
        }

        private EOBEquipPosition PositionOfItem(int iListIndex)
        {
            foreach (ListViewItem lvi in lvCharInventory.Items)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null || item.Available || item.ItemListIndex != iListIndex)
                    continue;
                return item.EOBInvLocation;
            }
            return EOBEquipPosition.Invalid;
        }

        private bool AddItemToBackpack(EOBItem item)
        {
            if (item == null)
                return true;
            if (PositionOfItem(item.ItemListIndex) != EOBEquipPosition.Invalid)
            {
                MessageBox.Show(String.Format("Inventory already contains item #{0}", item.ItemListIndex), "Item already present", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (ListViewItem lvi in lvCharInventory.Items)
            {
                EOBItem itemChar = lvi.Tag as EOBItem;
                if (itemChar == null)
                    continue;
                if (itemChar.EOBInvLocation < EOBEquipPosition.Backpack1 || itemChar.EOBInvLocation > EOBEquipPosition.Backpack14)
                    continue;
                if (!itemChar.Available)
                    continue;

                ReplaceCharInvItem(lvi, item);
                return true;
            }

            MessageBox.Show("There are no free backpack slots to hold the item!", "Backpack Full", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private void ReplaceCharInvItem(ListViewItem lviTarget, EOBItem itemSource)
        {
            if (itemSource == null || lviTarget == null)
                return;
            EOBItem itemNew = itemSource.Clone() as EOBItem;
            EOBItem itemOld = lviTarget.Tag as EOBItem;
            if (itemOld != null)
                itemNew.EOBInvLocation = itemOld.EOBInvLocation;
            itemNew.Available = itemSource.Available;
            lviTarget.Tag = itemNew;
            UpdateCharInventory(lviTarget);
            m_bAnyChanges = true;
        }

        private void lvCharInventory_DragDrop(object sender, DragEventArgs e)
        {
            object o = e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
            ListView.SelectedListViewItemCollection items = o as ListView.SelectedListViewItemCollection;
            if (items == null || items.Count < 1)
                return;
            if (lvCharInventory.SelectedItems.Count < 1)
                return;
            int iTarget = lvCharInventory.SelectedItems[0].Index;
            int iSource = 0;
            while (iSource < items.Count && iTarget < lvCharInventory.Items.Count)
                ReplaceCharInvItem(lvCharInventory.Items[iTarget++], items[iSource++].Tag as EOBItem);
        }

        private void lvMasterItems_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Capture = true;
            DoDragDrop(lvMasterItems.SelectedItems, DragDropEffects.Move);
        }

        private void lvCharInventory_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetType() != typeof(ListView.SelectedListViewItemCollection))
                return;
            e.Effect = DragDropEffects.Move;
        }

        private void lvCharInventory_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            SelectCharInvItemAt(e.X, e.Y);
        }

        private void SelectCharInvItemAt(int x, int y)
        {
            Point pt = lvCharInventory.PointToClient(new Point(x, y));
            ListViewItem lvi = lvCharInventory.GetItemAt(pt.X, pt.Y);
            Global.SelectOnly(lvCharInventory, lvi, false);
        }

        private void lvMasterItems_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvMasterItems_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lvCharInventory_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    RemoveSelectedInvItems();
                    break;
                case Keys.Insert:
                    ChangeInvItem();
                    break;
            }
        }

        private void miInvShowInMaster_Click(object sender, EventArgs e)
        {
            ShowSelectedItemInMasterList(false);
        }

        private bool IsAvailable(EOBItem item)
        {
            if (item == null)
                return true;
            return item.Available;
        }

        private void cmMasterTable_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool bSelected = lvMasterItems.SelectedItems.Count > 0;
            EOBItem item = bSelected ? lvMasterItems.SelectedItems[0].Tag as EOBItem : null;
            miMasterPlaceInBackpack.Enabled = bSelected && !IsAvailable(item);
            miMasterRemove.Enabled = bSelected;
            miMasterDuplicate.Enabled = bSelected;
            miMasterNext.Enabled = bSelected;
            miMasterPrevious.Enabled = bSelected;
            miMasterEdit.Enabled = bSelected;
            miMasterIdentified.Enabled = bSelected;
            miMasterIdentified.Checked = (item != null && item.Identified);
        }

        private void miMasterNext_Click(object sender, EventArgs e)
        {
            SelectNextMaster(true);
        }

        private void SelectNextMaster(bool bNext)
        {
            if (lvMasterItems.SelectedItems.Count < 1)
                return;
            EOBItem item = lvMasterItems.SelectedItems[0].Tag as EOBItem;
            if (item == null)
                return;
            if (bNext && item.NextItem > 0)
                SelectMasterItem(item.NextItem);
            else if (!bNext && item.PrevItem > 0)
                SelectMasterItem(item.PrevItem);
        }

        private void miMasterPrevious_Click(object sender, EventArgs e)
        {
            SelectNextMaster(false);
        }

        private void miMasterIdentified_Click(object sender, EventArgs e)
        {
            if (lvMasterItems.SelectedItems.Count < 1)
                return;
            miMasterIdentified.Checked = !miMasterIdentified.Checked;
            foreach (ListViewItem lvi in lvMasterItems.SelectedItems)
            {
                EOBItem item = lvi.Tag as EOBItem;
                if (item == null)
                    continue;
                item.Identified = miMasterIdentified.Checked;
                UpdateMasterListLVI(lvi);
            }
        }

        private void miMasterEdit_Click(object sender, EventArgs e)
        {
            if (lvMasterItems.SelectedItems.Count < 1)
                return;
            ListViewItem lvi = lvMasterItems.SelectedItems[0];
            EOBItem item = lvi.Tag as EOBItem;
            if (item == null)
                return;
            EditEOBMasterItemForm form = new EditEOBMasterItemForm();
            form.Item = item;
            if (form.ShowDialog() == DialogResult.OK)
            {
                lvi.Tag = form.Item;
                UpdateMasterListLVI(lvi);
                foreach (ListViewItem lviUpdate in lvCharInventory.Items)
                {
                    EOBItem itemUpdate = lviUpdate.Tag as EOBItem;
                    if (itemUpdate != null && itemUpdate.ItemListIndex == form.Item.ItemListIndex)
                        ReplaceCharInvItem(lviUpdate, form.Item);
                }
            }
        }

        private void miMasterCopy_Click(object sender, EventArgs e)
        {
            Global.CopyListViewItems(lvMasterItems, true);
        }
    }

    class EOBMasterItemComparer : BasicListViewComparer
    {
        public EOBMasterItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            ListViewItem lvi1 = x as ListViewItem;
            ListViewItem lvi2 = y as ListViewItem;
            if (x == null || y == null)
                return 0;

            EOBItem item1 = lvi1.Tag as EOBItem;
            EOBItem item2 = lvi2.Tag as EOBItem;

            if (item1.Available && item2.Available)
                return 0;
            if (item1.Available && !item2.Available)
                return 1;
            if (!item1.Available && item2.Available)
                return -1;

            int iIndex1, iIndex2;

            switch (m_column)
            {
                case 0: return Order(Math.Sign(item1.ItemListIndex - item2.ItemListIndex));
                case 3: return Order(Math.Sign(item1.Charges - item2.Charges));
                case 6: return Order(Math.Sign((item1.Damage.Average - item2.Damage.Average) + (item1.DamageLarge.Average - item2.DamageLarge.Average)));
                case 7:
                    if (item1.IsArmor && item2.IsArmor)
                        return Order(Math.Sign((item1.AC - item1.Modifier) - (item2.AC - item2.Modifier)));
                    return Order(Math.Sign(item1.AC - item2.AC));
                case 8: return Order(Math.Sign(item1.MapIndex - item2.MapIndex));
                case 10:
                    iIndex1 = item1.PrevItem != item1.ItemListIndex ? item1.PrevItem : 0;
                    iIndex2 = item2.PrevItem != item2.ItemListIndex ? item2.PrevItem : 0;
                    return Order(Math.Sign(iIndex1 - iIndex2));
                case 11:
                    iIndex1 = item1.NextItem != item1.ItemListIndex ? item1.NextItem : 0;
                    iIndex2 = item2.NextItem != item2.ItemListIndex ? item2.NextItem : 0;
                    return Order(Math.Sign(iIndex1 - iIndex2));
                default: return base.Compare(x, y);
            }
        }
    }

    class EOBInventoryItemComparer : BasicListViewComparer
    {
        public EOBInventoryItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            ListViewItem lvi1 = x as ListViewItem;
            ListViewItem lvi2 = y as ListViewItem;
            if (x == null || y == null)
                return 0;

            EOBItem item1 = lvi1.Tag as EOBItem;
            EOBItem item2 = lvi2.Tag as EOBItem;

            if (item2 == null)
                return -1;
            if (item1 == null)
                return 1;

            switch (m_column)
            {
                case 1: return Order(Math.Sign(item1.ItemListIndex - item2.ItemListIndex));
                default: return base.Compare(x, y);
            }
        }
    }
}
