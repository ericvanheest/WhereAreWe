using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class ShopInventoryForm : HackerBasedForm
    {
        private Subscreen m_oldScreen = Subscreen.Unknown;
        private Shops m_shopsPrevious = null;
        private bool m_bLastInShop = false;
        private int m_lastCharAddr = -1;
        ListView[] m_townsViews;
        TabPage[] m_pages;

        public ShopInventoryForm()
        {
            InitializeComponent();

            m_townsViews = new ListView[] { lvTown1, lvTown2, lvTown3, lvTown4, lvTown5, lvTown6, lvTown7, lvTown8 };
            m_pages = new TabPage[] { tpTown1, tpTown2, tpTown3, tpTown4, tpTown5, tpTown6, tpTown7, tpTown8 };
            NativeMethods.SetTooltipDelay(lvCurrent, 32000);
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        public bool SetInventories(Shops shops, string strTitle = null)
        {
            if (strTitle != null)
                Text = strTitle;

            if (shops == null)
                return false;
            bool bUpdated = UpdateUI(shops);
            UpdateSubscreen(shops.Screen);
            return bUpdated;
        }

        private void AddItem(ListView lv, ShopItem item, Dictionary<ItemType, int> indices)
        {
            string strItem = item.Item.FormatDescription(Properties.Settings.Default.ShopItemFormat);
            ListViewItem lvi = new ListViewItem(strItem);
            BaseCharacter charSelected = m_main.GetSelectedCharacter();
            string strPrefix = "";
            bool bEquip = item.Item.CanEquip;
            if (charSelected != null && bEquip)
            {
                UIElementOption uiDisabled = Properties.Settings.Default.UIElementOptions.Elements[ColoredUIElements.DisabledShopItem];
                UIElementOption uiEnabled = Properties.Settings.Default.UIElementOptions.Elements[ColoredUIElements.EnabledShopItem];

                bool bUsable = item.Item.IsUsableByAny(charSelected.BasicClass) && item.Item.IsUsableByAny(charSelected.BasicAlignment.Temporary);
                strPrefix = bUsable ? "Yes: " : "No: ";
                Global.SetColor(lvi, bUsable ? ColoredUIElements.EnabledShopItem : ColoredUIElements.DisabledShopItem);
            }
            lvi.SubItems.Add(bEquip ? strPrefix + item.Item.UsableString : "");
            lvi.Tag = new InventoryItemTag(item, 0, String.Format("{0}", indices[item.Item.ItemBaseType]++), strItem);
            lvi.ToolTipText = item.Item.MultiLineDescription;
            lv.Items.Add(lvi);
        }

        protected override void OnMainSet()
        {
            Global.RestartTimer(timerRefreshInfo);
        }

        private void UpdateHeader(ListView lv)
        {
            switch (Hacker.Game)
            {
                default:
                    lv.Columns[1].Text = "Usable By";
                    break;
            }
        }

        private Dictionary<ItemType, int> NewIndices()
        {
            Dictionary<ItemType, int> indices = new Dictionary<ItemType, int>();
            indices.Add(ItemType.Weapon, 1);
            indices.Add(ItemType.Armor, 1);
            indices.Add(ItemType.Accessory, 1);
            indices.Add(ItemType.Miscellaneous, 1);
            return indices;
        }

        private void UpdateListView(ListView lv, ShopInventory inv)
        {
            Dictionary<ItemType, int> indices = NewIndices();

            lv.BeginUpdate();
            UpdateHeader(lv);
            lv.Items.Clear();
            if (inv != null && inv.AllItems != null)
            {
                foreach (ShopItem item in inv.AllItems)
                    AddItem(lv, item, indices);
            }
            Global.SizeHeadersAndContent(lv);
            lv.EndUpdate();
        }

        private bool UpdateUI(Shops shops, bool bForce = false)
        {
            if (Hacker == null || !Hacker.IsValid || shops == null || (!shops.InShop && !m_main.ShowShopInventories))
            {
                Hide();
                m_shopsPrevious = null;
                return false;
            }

            if (!Visible)
                Show();

            if (!bForce && 
                m_shopsPrevious != null &&
                m_bLastInShop == shops.InShop &&
                Global.Compare(shops.RawBytes, m_shopsPrevious.RawBytes) && 
                m_lastCharAddr == m_main.GetSelectedCharacterAddress())
                return false;   // no changes; don't update the UI

            m_lastCharAddr = m_main.GetSelectedCharacterAddress();
            m_shopsPrevious = shops;
            m_bLastInShop = shops.InShop;

            if (shops.CurrentDisplay != null)
            {
                Dictionary<ItemType, int> indices = NewIndices();

                lvCurrent.BeginUpdate();
                UpdateHeader(lvCurrent);
                lvCurrent.Items.Clear();
                foreach (ShopItem item in shops.CurrentDisplay)
                    AddItem(lvCurrent, item, indices);
                Global.SizeHeadersAndContent(lvCurrent);
                lvCurrent.EndUpdate();
            }

            for (int iTown = 0; iTown < shops.Inventories.Count; iTown++)
            {
                if (!tcInventories.TabPages.Contains(m_pages[iTown]))
                    tcInventories.TabPages.Insert(iTown, m_pages[iTown]);

                if (!String.IsNullOrWhiteSpace(shops.Inventories[iTown].Town))
                    m_pages[iTown].Text = shops.Inventories[iTown].Town;
                UpdateListView(m_townsViews[iTown], shops.Inventories[iTown]);
            }
            for (int iExtra = shops.Inventories.Count; iExtra < m_pages.Length; iExtra++)
            {
                if (tcInventories.TabPages.Contains(m_pages[iExtra]))
                    tcInventories.TabPages.Remove(m_pages[iExtra]);
            }

            if (!shops.InShop || shops.CurrentDisplay == null)
            {
                lvCurrent.Items.Clear();
                if (tcInventories.TabPages.Contains(tpCurrent))
                    tcInventories.TabPages.Remove(tpCurrent);
            }
            else if (!tcInventories.TabPages.Contains(tpCurrent))
            {
                tcInventories.TabPages.Insert(0, tpCurrent);
                tcInventories.SelectedTab = tpCurrent;
            }

            return true;
        }

        private void ShopInventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_shopsPrevious = null;
            timerRefreshInfo.Stop();
            if (!m_main.ShuttingDown)
                m_main.ShowShopInventories = false;
        }

        public override void Destroy()
        {
            timerRefreshInfo.Stop();
            base.Destroy();
        }

        private void timerRefreshInfo_Tick(object sender, EventArgs e)
        {
            if (Hacker != null && Hacker.Running)
            {
                Shops shops = Hacker.GetShopInfo();

                if (shops == null)
                    Close();

                SetInventories(shops);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectNextTab(int iNext)
        {
            int iTab = tcInventories.SelectedIndex + iNext;
            if (iTab < 0)
                iTab = tcInventories.TabPages.Count - 1;
            if (iTab >= tcInventories.TabPages.Count)
                iTab = 0;
            tcInventories.SelectedIndex = iTab;
        }

        private void ShopInventoryForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
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
                    int iIndex = e.KeyCode - Keys.D1;
                    if (iIndex >= 0 && iIndex < tcInventories.TabPages.Count)
                        tcInventories.SelectedIndex = iIndex;
                    e.Handled = true;
                    break;
                case Keys.Left:
                    SelectNextTab(-1);
                    e.Handled = true;
                    break;
                case Keys.Right:
                    SelectNextTab(1);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        public bool UpdateSubscreen(Subscreen screen, bool bForce = false)
        {
            if (screen == m_oldScreen && !bForce)
                return false;

            m_oldScreen = screen;

            return HighlightInventory(screen);
        }

        private bool HighlightInventory(Subscreen screen)
        {
            switch (screen)
            {
                case Subscreen.Weapons: return HighlightInventory(ItemType.Weapon);
                case Subscreen.Armor: return HighlightInventory(ItemType.Armor);
                case Subscreen.Accessories: return HighlightInventory(ItemType.Accessory);
                case Subscreen.Miscellaneous: return HighlightInventory(ItemType.Miscellaneous);
                default: return HighlightInventory(ItemType.None);
            }
        }

        private bool HighlightInventory(ItemType type)
        {
            lvCurrent.BeginUpdate();

            Color colorHighlight = Global.Highlight(lvCurrent.BackColor, 30);
            foreach (ListViewItem lvi in lvCurrent.Items)
            {
                InventoryItemTag iit = lvi.Tag as InventoryItemTag;
                if (iit.Item.ItemBaseType == type)
                {
                    lvi.BackColor = colorHighlight;
                    lvi.Text = String.Format("{0}. {1}", iit.DisplayIndex, iit.ListViewString);
                }
                else
                {
                    lvi.BackColor = lvCurrent.BackColor;
                    lvi.Text = iit.ListViewString;
                }
            }
            lvCurrent.EndUpdate();

            return true;
        }

        private ListView SelectedListView
        {
            get
            {
                foreach (Control ctrl in tcInventories.SelectedTab.Controls)
                    if (ctrl is ListView)
                        return ctrl as ListView;

                return null;
            }
        }

        private InventoryItemTag SelectedItemTag
        {
            get
            {
                ListView lv = SelectedListView;

                if (lv == null)
                    return null;

                if (lv.FocusedItem == null)
                    return null;

                return lv.FocusedItem.Tag as InventoryItemTag;
            }
        }

        private void cmEdit_Opening(object sender, CancelEventArgs e)
        {
            InventoryItemTag tag = SelectedItemTag;

            miCtxEdit.Visible = Global.Cheats && tag != null && tag.ShopItem.Offset != -1;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryItemTag tag = SelectedItemTag;

            if (tag == null)
                return;

            if (tag.Item is MM3Item)
            {
                MM3ItemEditForm form = new MM3ItemEditForm(Hacker.Game);
                form.Item = tag.Item;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tag.ShopItem.Item = form.Item;
                    Hacker.SetShopItem(tag.ShopItem);
                }
            }
            else if (tag.Item is MM45Item)
            {
                MM45ItemEditForm form = new MM45ItemEditForm(Hacker.Game);
                form.Item = tag.Item;
                form.AllowOnly(((MM45Item)tag.Item).Base.Type);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tag.ShopItem.Item = form.Item;
                    Hacker.SetShopItem(tag.ShopItem);
                }
            }
            else
            {
                ItemEditForm form = new ItemEditForm(Hacker.Game);
                form.Item = tag.Item;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tag.ShopItem.Item = form.Item;
                    Hacker.SetShopItem(tag.ShopItem);
                }
            }
        }

        private void miCtxItemDisplayFormat_Click(object sender, EventArgs e)
        {
            EditItemDisplayFormatForm form = new EditItemDisplayFormatForm();
            form.DisplayFormat = Properties.Settings.Default.ShopItemFormat;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ShopItemFormat = form.DisplayFormat;
                UpdateUI(m_shopsPrevious, true);
            }
        }

        private void miCtxCopyAll_Click(object sender, EventArgs e)
        {
            ListView lv = SelectedListView;
            if (lv == null)
                return;
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in lv.Items)
            {
                InventoryItemTag tag = lvi.Tag as InventoryItemTag;
                if (tag == null)
                    continue;
                sb.AppendFormat("{0}\t{1}\r\n", tag.Item.FullDescriptionString, tag.Item.UsableString);
            }
            Clipboard.SetText(sb.ToString());
        }

        private void lvAny_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowSelectedItemInfo();
        }

        private void ShowSelectedItemInfo()
        {
            ListView lv = SelectedListView;
            if (lv == null)
                return;
            if (lv.SelectedItems.Count < 1)
                return;
            if (!(lv.SelectedItems[0].Tag is InventoryItemTag))
                return;
            ShowItemInfo(((InventoryItemTag)lv.SelectedItems[0].Tag).Item);
        }

        private void ShowItemInfo(Item item)
        {
            ItemCompareForm formCompare = new ItemCompareForm();
            formCompare.CenteringForm = this;
            formCompare.Item = item;
            List<BaseCharacter> chars = m_main.PartyForm.GetCharacters();
            if (chars == null)
                return;
            formCompare.ItemOwner = -1;

            formCompare.Characters = chars;
            formCompare.ShowDialog();
        }
    }
}
