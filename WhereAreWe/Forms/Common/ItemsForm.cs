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
    public partial class ItemsForm : HackerBasedForm
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private FindBox m_findBox = null;
        private int m_iFiltered = 0;
        private HashSet<GenericClass> FilterClass = new HashSet<GenericClass>();
        private HashSet<string> FilterString = new HashSet<string>();

        public ItemsForm()
        {
            InitializeComponent();

            NativeMethods.SetTooltipDelay(lvItems, 32000);
        }

        protected override void OnMainSet()
        {
            UpdateUI();
        }

        protected override bool OnCommonKeySelectAll()
        {
            lvItems.BeginUpdate();
            foreach (ListViewItem lvi in lvItems.Items)
                lvi.Selected = true;
            lvItems.EndUpdate();
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel2Collapsed)
                m_findBox.Next(sender, new BoolHandlerEventArgs(false));
            else
                Close();
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
        }

        private string EquipBonus(Item item)
        {
            if (item.EquipBonusValue != 0)
                return Global.AddPlus(item.EquipBonusValue, false);
            return item.EquipEffects;
        }

        private string GetColumnText(Item item, int iColumn)
        {
            switch (iColumn)
            {
                case 0: return item.TypeString;
                case 1: return item.Index.ToString();
                case 2: return item.Name;
                case 3: return item.UsableString;
                case 4: return item.UsableByAlignment.Replace("Any", "");
                case 5: return item.ArmorClassString;
                case 6: return item.BaseDamage.Max == 0 ? String.Empty : item.BaseDamage.ToString();
                case 7: return item.AttributeString;
                case 8: return EquipBonus(item);
                case 9: return item.UseEffectString;
                case 10: return item.MaterialString; 
                case 11: return item.ElementString; 
                case 12: return item.ResistString; 
                case 13: return item.ValueString;
                case 14: return item.RangeString;
                default: return String.Empty;
            }
        }

        private void AddItem(Item item)
        {
            if (FilterClass.Count > 0)
            {
                if (!FilterClass.Any(gc => item.IsUsableByAny(gc)))
                {
                    m_iFiltered++;
                    return;
                }
            }

            ListViewItem lvi = new ListViewItem(item.TypeString);
            for (int i = 1; i < lvItems.Columns.Count; i++)
                lvi.SubItems.Add(GetColumnText(item, i));

            if (FilterString.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListViewItem.ListViewSubItem si in lvi.SubItems)
                    sb.AppendFormat("{0} ", si.Text.ToLower());
                string strLine = sb.ToString();

                bool bShow = false;
                foreach (String strFilter in FilterString)
                {
                    if (strLine.Contains(strFilter.ToLower()))
                    {
                        bShow = true;
                        break;
                    }
                }
                if (!bShow)
                {
                    m_iFiltered++;
                    return;
                }
            }

            lvi.Tag = item;
            lvi.ToolTipText = item.MultiLineDescription;
            lvItems.Items.Add(lvi);
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            m_findBox = new FindBox(splitContainer1, tbFind, FindBox.ListViewFindFunction, lvItems);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;

            splitContainer1.Panel2Collapsed = true;
        }

        private void UpdateUI()
        {
            lvItems.BeginUpdate();
            lvItems.Items.Clear();

            m_iFiltered = 0;
            if (Hacker is MM1MemoryHacker)
            {
                foreach (MM1Item item in MM1.Items)
                    AddItem(item);
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chMaterial.Width = 0;
                chElement.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
            }
            else if (Hacker is MM2MemoryHacker)
            {
                foreach (MM2Item item in MM2.Items)
                    AddItem(item);
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chUsableAlignments.Width = 0;
                chMaterial.Width = 0;
                chElement.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
            }
            else if (Hacker is MM3MemoryHacker)
            {
                for(MM3ItemIndex index = MM3ItemIndex.First; index < MM3ItemIndex.Last; index++)
                    AddItem(MM3Item.Create(0, 0, 0, 0, 0, (byte) index, 0));
                for (MM3ItemElementalIndex index = MM3ItemElementalIndex.Burning; index < MM3ItemElementalIndex.Invalid; index++)
                    AddItem(new MM3ElementalItem(index));
                for (MM3ItemMaterialIndex index = MM3ItemMaterialIndex.Wooden; index < MM3ItemMaterialIndex.Invalid; index++)
                    AddItem(new MM3MaterialItem(index));
                for (MM3ItemAttributeIndex index = MM3ItemAttributeIndex.Might; index < MM3ItemAttributeIndex.Invalid; index++)
                    AddItem(new MM3AttributeItem(index));
                for (MM3ItemPropertyIndex index = MM3ItemPropertyIndex.Light; index < MM3ItemPropertyIndex.Invalid; index++)
                    AddItem(new MM3PropertyItem(index));
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chUsableAlignments.Width = 0;
                chMaterial.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
            }
            else if (Hacker is MM45MemoryHacker)
            {
                for (MM45WeaponIndex index = MM45WeaponIndex.First; index < MM45WeaponIndex.Last; index++)
                    AddItem(MM45Item.Create(ItemType.Weapon, (int) index));
                for (MM45ArmorIndex index = MM45ArmorIndex.First; index < MM45ArmorIndex.Last; index++)
                    AddItem(MM45Item.Create(ItemType.Armor, (int)index));
                for (MM45AccessoryIndex index = MM45AccessoryIndex.First; index < MM45AccessoryIndex.Last; index++)
                    AddItem(MM45Item.Create(ItemType.Accessory, (int)index));
                for (MM45MiscItemIndex index = MM45MiscItemIndex.First; index < MM45MiscItemIndex.Last; index++)
                    AddItem(MM45Item.Create(ItemType.Miscellaneous, (int)index));
                for (MM45ItemPrefixIndex index = MM45ItemPrefixIndex.Burning; index < MM45ItemPrefixIndex.Invalid; index++)
                    AddItem(new MM45PrefixItem(index));
                for (MM45ItemSuffixIndex index = MM45ItemSuffixIndex.Light; index < MM45ItemSuffixIndex.Invalid; index++)
                    AddItem(new MM45SuffixItem(index));
                for (MM45WeaponSuffixIndex index = MM45WeaponSuffixIndex.DragonSlayer; index < MM45WeaponSuffixIndex.Invalid; index++)
                    AddItem(new MM45WeaponSuffixItem(index));
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chUsableAlignments.Width = 0;
                chMaterial.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
            }
            else if (Games.IsWizardry(Hacker.Game))
            {
                IEnumerable<WizItem> items = null;
                WizGameGlobals globals = Games.GetWizGlobals(Hacker.Game);
                globals.InitItemList(Hacker);
                items = globals.GetItems();
                foreach (WizItem item in items)
                {
                    if (Hacker.Game == GameNames.Wizardry4 && (item.Index == (int) Wiz4ItemIndex.Invalid95 || item.Index == (int) Wiz4ItemIndex.Invalid96))
                        continue;
                    AddItem(item);
                }
                chEquipAttribute.Text = "Equip";
                chEquipBonus.Text = "Hit";
                chMaterial.Text = "50% damage from";
                chElement.Text = "2x damage vs.";
                Global.SizeHeadersAndContent(lvItems, 0, false);
            }
            else if (Games.IsBardsTale(Hacker.Game))
            {
                IEnumerable<BTItem> items = null;
                BTGameGlobals globals = Games.GetBTGlobals(Hacker.Game);
                globals.InitItemList(Hacker);
                items = globals.GetItems();
                foreach (BTItem item in items)
                    if (item.Index != 0)
                        AddItem(item);
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chMaterial.Width = 0;
                chElement.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
                chEquipAttribute.Width = 0;
                chUsableAlignments.Width = 0;
            }
            else if (Games.IsEyeOfTheBeholder(Hacker.Game))
            {
                IEnumerable<EOBItem> items = null;
                EOBGameGlobals globals = Games.GetEOBGlobals(Hacker.Game);
                globals.InitItemList(Hacker);
                items = globals.GetItems();
                foreach (EOBItem item in items)
                    AddItem(item);
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chMaterial.Width = 0;
                chElement.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
                chEquipAttribute.Width = 0;
                chUsableAlignments.Width = 0;
                chEquipBonus.Width = 0;
                chUse.Width = 0;
                chValue.Width = 0;
            }
            else if (Games.IsUltima(Hacker.Game))
            {
                foreach (UltimaItem item in Ultima1.Items)
                    AddItem(item);
                Global.SizeHeadersAndContent(lvItems, 0, false);
                chMaterial.Width = 0;
                chElement.Width = 0;
                chResistance.Width = 0;
                chRange.Width = 0;
            }

            string strFilter = "";
            if (AnyFiltersActive)
                strFilter = String.Format(" ({0} filtered)", m_iFiltered);

            if (Hacker != null)
                Text = String.Format("Item List: {0}", Games.Name(Hacker.Game)) + strFilter;
            else
                Text = "Item List (no game running)" + strFilter;

            lvItems.EndUpdate();
        }

        private bool AnyFiltersActive { get { return FilterString.Count > 0 || FilterClass.Count > 0; } }

        private void lvItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvItems.ListViewItemSorter = new ItemComparer(lvItems, e.Column, m_bAscendingSort);
            lvItems.Sort();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
                Close();
            m_findBox.HideFindBox();
        }

        private void lvItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowItemInfo();
        }

        private void ShowItemInfo()
        {
            if (lvItems.FocusedItem == null)
                return;

            Item item = lvItems.FocusedItem.Tag as Item;
            if (item == null)
                return;

            ViewInfoForm.Show(item.MultiLineDescription.Trim(), String.Format("#{0} ({1}): {2}", item.Index, item.TypeString, item.Name));
        }

        private void lvItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ShowItemInfo();
        }

        private string GetItemString(ListViewItem lvi)
        {
            Item item = lvi.Tag as Item;
            if (item == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lvItems.Columns.Count; i++)
            {
                if (lvItems.Columns[i].Width > 0)
                    sb.AppendFormat("{0}\t", GetColumnText(item, i));
            }
            return Global.Trim(sb).ToString();
        }

        private void miCtxCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (lvItems.SelectedItems.Count == 0)
            {
                foreach (ListViewItem lvi in lvItems.Items)
                    sb.AppendFormat("{0}\r\n", GetItemString(lvi));
            }
            else
            {
                foreach (ListViewItem lvi in lvItems.SelectedItems)
                    sb.AppendFormat("{0}\r\n", GetItemString(lvi));
            }
            Clipboard.SetText(sb.ToString());
        }

        private void cmItems_Opening(object sender, CancelEventArgs e)
        {
            if (lvItems.SelectedItems.Count == 0)
                miCtxCopy.Text = "&Copy all items";
            else
                miCtxCopy.Text = "&Copy selected items";
            miCtxRemoveAllFilters.Visible = AnyFiltersActive;
            if (m_findBox.Visible && !String.IsNullOrWhiteSpace(tbFind.Text))
            {
                miCtxFilterText.Text = String.Format("Show only matching \"{0}\"", tbFind.Text);
                miCtxFilterText.Visible = true;
                miCtxFilterText.Checked = FilterString.Contains(tbFind.Text);
            }
            else
                miCtxFilterText.Visible = false;
        }

        private void FilterClass_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            if (mi != null)
            {
                GenericClass gc = (GenericClass)mi.Tag;
                if (FilterClass.Contains(gc))
                    FilterClass.Remove(gc);
                else
                    FilterClass.Add(gc);
            }
            UpdateUI();
        }

        private void miCtxRemoveAllFilters_Click(object sender, EventArgs e)
        {
            FilterClass.Clear();
            FilterString.Clear();
            UpdateUI();
        }

        private void miCtxFilterUsableBy_DropDownOpening(object sender, EventArgs e)
        {
            miCtxFilterUsableBy.DropDown.Items.Clear();
            if (Hacker != null)
            {
                foreach (GenericClass gc in Games.Classes(Hacker.Game))
                {
                    ToolStripMenuItem tsi = new ToolStripMenuItem(BaseCharacter.ClassString(gc));
                    tsi.Tag = gc;
                    if (FilterClass.Contains(gc))
                        tsi.Checked = true;
                    tsi.Click += FilterClass_Click;
                    miCtxFilterUsableBy.DropDown.Items.Add(tsi);
                }
            }
        }

        private void miCtxFilterText_Click(object sender, EventArgs e)
        {
            FilterString.Clear();
            if (!miCtxFilterText.Checked)
                FilterString.Add(tbFind.Text);
            UpdateUI();
        }
    }

    class ItemComparer : BasicListViewComparer
    {
        public ItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            Item item1 = ((ListViewItem)x).Tag as Item;
            Item item2 = ((ListViewItem)y).Tag as Item;

            switch (m_column)
            {
                case 0:
                    int returnVal = String.Compare(item1.TypeString, item2.TypeString);
                    if (returnVal == 0)
                        return Order(Math.Sign(item1.Index - item2.Index));
                    return Order(returnVal);
                case 1: return Order(Math.Sign(item1.Index - item2.Index));
                case 2: return Order(String.Compare(item1.Name, item2.Name));
                case 3: return Order(String.Compare(item1.UsableString, item2.UsableString));
                case 4: return Order(String.Compare(item1.UsableByAlignment, item2.UsableByAlignment));
                case 5: return Order(Math.Sign(item1.ArmorClass - item2.ArmorClass));
                case 6: return Order(Math.Sign(item1.BaseDamage.Average - item2.BaseDamage.Average));
                case 7: return Order(String.Compare(item1.AttributeString, item2.AttributeString));
                case 8:
                    if (item1.EquipBonusValue != item2.EquipBonusValue)
                        return Order(Math.Sign(item1.EquipBonusValue - item2.EquipBonusValue));
                    else
                        return Order(String.Compare(item1.EquipEffects, item2.EquipEffects));
                case 9: return Order(String.Compare(item1.UseEffectString, item2.UseEffectString));
                case 10: return Order(String.Compare(item1.MaterialString, item2.MaterialString));
                case 11: return Order(String.Compare(item1.ElementString, item2.ElementString));
                case 12: return Order(String.Compare(item1.ResistString, item2.ResistString));
                case 13: return Order(Math.Sign(item1.Value - item2.Value));
                case 14: return Order(String.Compare(item1.RangeString, item2.RangeString));
                default: return base.Compare(x, y);
            }
        }
    }
}
