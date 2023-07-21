using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditEOBMasterItemForm : Form
    {
        public EditEOBMasterItemForm()
        {
            InitializeComponent();
            InitComboBoxes();
        }

        private EOBItem m_item;
        private bool m_bUpdating = false;

        public EOBItem Item
        {
            get
            {
                m_item = UpdateFromUI();
                return m_item;
            }
            set
            {
                m_item = value;
                UpdateUI(m_item);
            }
        }

        private void InitComboBoxes()
        {
            m_bUpdating = true;
            for (EOBItemIndex index = EOBItemIndex.Axe; index < EOBItemIndex.Last; index++)
                comboBasicItem.Items.Add(new EOBBasicIndexTag(index));
            int iLastItem = (int) EOB1ItemStringIndex.Last;
            switch (m_item.Game)
            {
                case GameNames.EyeOfTheBeholder2:
                    iLastItem = (int) EOB2ItemStringIndex.Last;
                    break;
            }
            for (int index = 0; index < iLastItem; index++)
                comboText.Items.Add(new EOBItemStringTag(m_item.Game, index));
            for (EOBItemGraphic index = EOBItemGraphic.MouseCursor; index < EOBItemGraphic.Last; index++)
                comboGraphic.Items.Add(new EOBItemGraphicTag(index));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.NorthWest, 0, 255, 255));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.NorthWest, 0));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.NorthWest, 255));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.NorthWest, 1));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.NorthEast, 1));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.SouthWest, 1));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.SouthEast, 1));
            comboLocation.Items.Add(new EOBLocationTag(EOBItemLocation.Alcove, 1));
            comboBasicItem.SelectedIndex = 0;
            comboText.SelectedIndex = 0;
            comboGraphic.SelectedIndex = 0;
            comboLocation.SelectedIndex = 0;
            m_bUpdating = false;
        }

        public void UpdateUI(EOBItem item, bool bUpdateBytes = true)
        {
            m_bUpdating = true;
            labelMasterIndex.Text = item.ItemListIndex.ToString();
            SetBasicItem(item.ItemIndex);
            Global.SetNud(nudUnknown, item.Unknown01);
            Global.SetNud(nudCharges, item.Charges);
            SetGraphic((EOBItemGraphic) item.Image);
            cbMagical.Checked = item.Magical;
            cbIdentified.Checked = item.Identified;
            cbRemovable.Checked = !item.Nonremovable;
            SetItemString(item.StringIndex);
            Global.SetNud(nudPrevItem, item.PrevItem);
            Global.SetNud(nudNextItem, item.NextItem);
            Global.SetNud(nudModifier, item.Modifier);
            SetLocation(item.Floor, item.MapIndex, item.Available ? 255 : item.Location.X, item.Available ? 255 : item.Location.Y);
            UpdateDescription(item, bUpdateBytes);
            m_bUpdating = false;
        }

        public void SetBasicItem(EOBItemIndex index)
        {
            for (int i = 0; i < comboBasicItem.Items.Count; i++)
            {
                EOBBasicIndexTag tag = comboBasicItem.Items[i] as EOBBasicIndexTag;
                if (tag == null)
                    continue;
                if (tag.Index == index)
                {
                    comboBasicItem.SelectedIndex = i;
                    return;
                }
            }
            comboBasicItem.SelectedIndex = -1;
        }

        public void SetGraphic(EOBItemGraphic index)
        {
            for (int i = 0; i < comboGraphic.Items.Count; i++)
            {
                EOBItemGraphicTag tag = comboGraphic.Items[i] as EOBItemGraphicTag;
                if (tag == null)
                    continue;
                if (tag.Index == index)
                {
                    comboGraphic.SelectedIndex = i;
                    return;
                }
            }
            comboGraphic.SelectedIndex = -1;
        }

        public void SetItemString(int index)
        {
            for (int i = 0; i < comboText.Items.Count; i++)
            {
                EOBItemStringTag tag = comboText.Items[i] as EOBItemStringTag;
                if (tag == null)
                    continue;
                if (tag.Index == index)
                {
                    comboText.SelectedIndex = i;
                    return;
                }
            }
            comboText.SelectedIndex = -1;
        }

        public void SetLocation(EOBItemLocation loc, int iMap, int x, int y)
        {
            Global.SetNud(nudMap, iMap);
            Global.SetNud(nudPositionX, x);
            Global.SetNud(nudPositionY, y);
            for (int i = 0; i < comboLocation.Items.Count; i++)
            {
                EOBLocationTag tag = comboLocation.Items[i] as EOBLocationTag;
                if (tag == null)
                    continue;
                if (tag.Empty && x == 255 && y == 255)
                {
                    comboLocation.SelectedIndex = i;
                    UpdateLocationEnabled(!tag.Quiver && !tag.Nowhere && !tag.Empty);
                    return;
                }
                if ((tag.Location == loc && tag.Map == 1) ||
                    ((iMap == 0 || iMap == 255) && tag.Map == iMap))
                {
                    if (tag.Empty)
                        continue;
                    comboLocation.SelectedIndex = i;
                    UpdateLocationEnabled(!tag.Quiver && !tag.Nowhere && !tag.Empty);
                    return;
                }
            }
            comboLocation.SelectedIndex = -1;
        }

        public EOBItem UpdateFromUI()
        {
            if (m_bUpdating)
                return null;
            EOBBasicIndexTag tag = comboBasicItem.SelectedItem as EOBBasicIndexTag;
            if (tag == null)
                return null;
            EOBItem itemBasic = tag.GetItem();
            EOBItem item = EOBItem.CreateBasic(itemBasic.Game, itemBasic.GetBasicBytes(), (int) tag.Index, 0);
            item.ItemListIndex = m_item.ItemListIndex;
            item.Unknown01 = (byte)nudUnknown.Value;
            item.Charges = (int)nudCharges.Value;
            item.Image = (int) (comboGraphic.SelectedItem as EOBItemGraphicTag).Index;
            item.Identified = cbIdentified.Checked;
            item.Magical = cbMagical.Checked;
            item.Nonremovable = !cbRemovable.Checked;
            item.StringIndex = (comboText.SelectedItem as EOBItemStringTag).Index;
            item.Name = item.StringName;
            item.PrevItem = (int)nudPrevItem.Value;
            item.NextItem = (int)nudNextItem.Value;
            item.Modifier = (int)nudModifier.Value;
            item.Location = new Point((int)nudPositionX.Value, (int)nudPositionY.Value);
            EOBLocationTag loc = comboLocation.SelectedItem as EOBLocationTag;
            if (loc != null)
            {
                item.Floor = loc.Location;
                if (loc.Empty)
                {
                    item.Available = true;
                    item.MapIndex = 0;
                }
                else if (loc.Map == 255 || loc.Map == 0)
                    item.MapIndex = loc.Map;
                else
                    item.MapIndex = (int)nudMap.Value;
            }
            else
            {
                item.Floor = EOBItemLocation.NorthWest;
                item.MapIndex = (int)nudMap.Value;
            }
            return item;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_item = UpdateFromUI();
        }

        private void ClearBasicItemInfo()
        {
            labelUsable.Text = "None";
            labelWeaponStyle.Text = "None";
            labelAC.Text = "0";
            labelDamageLarge.Text = "0";
            labelDamageSmall.Text = "0";
            labelType.Text = "None";
        }

        private void OnItemChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            EOBItem item = UpdateFromUI();
            UpdateDescription(item, true);
        }

        private void UpdateDescription(EOBItem item, bool bUpdateBytes)
        {
            labelInvalidBytes.Visible = false;
            byte[] bytes = Global.NullBytes(14);
            if (item == null)
            {
                labelDescription.Text = "<invalid item>";
            }
            else
            {
                labelDescription.Text = item.GetLongDescription();
                bytes = item.GetItemListBytes();
            }
            if (bUpdateBytes)
            {
                m_bUpdating = true;
                tbBytes.Text = Global.ByteString(bytes);
                m_bUpdating = false;
            }
        }

        public static string UsableString(EOBItem item)
        {
            if (item == null)
                return "None";

            if (item.Usable == EOBUseFlags.All)
                return "All";
            if (item.Usable == EOBUseFlags.None)
                return "None";

            StringBuilder sb = new StringBuilder();
            if (item.Usable.HasFlag(EOBUseFlags.Fighter))
                sb.Append("Fighter, ");
            if (item.Usable.HasFlag(EOBUseFlags.Cleric))
                sb.Append("Cleric, ");
            if (item.Usable.HasFlag(EOBUseFlags.Mage))
                sb.Append("Mage, ");
            if (item.Usable.HasFlag(EOBUseFlags.Thief))
                sb.Append("Thief, ");

            return Global.Trim(sb).ToString();
        }

        public static string WeaponStyleString(EOBItem item)
        {
            if (item == null || !item.IsWeapon)
                return "None";

            switch (item.Handed)
            {
                case EOBHanded.None: return "None";
                case EOBHanded.OneHanded: return "One-Handed";
                case EOBHanded.TwoHanded: return "Two-Handed";
                default: return String.Format("Unknown({0})", (int)item.Handed);
            }
        }

        public static string TypeString(ItemType type)
        {
            switch (type)
            {
                case ItemType.Armor: return "Armor";
                case ItemType.Weapon: return "Weapon";
                case ItemType.Accessory: return "Accessory";
                case ItemType.Miscellaneous: return "Miscellaneous";
                default: return "None";
            }
        }

        private void comboBasicItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            EOBBasicIndexTag tag = comboBasicItem.SelectedItem as EOBBasicIndexTag;
            if (tag == null)
                ClearBasicItemInfo();
            else
            {
                EOBItem item = tag.GetItem();
                if (item == null)
                {
                    ClearBasicItemInfo();
                    return;
                }
                labelUsable.Text = UsableString(item);
                labelWeaponStyle.Text = WeaponStyleString(item);
                labelAC.Text = item.AC.ToString();
                labelDamageSmall.Text = item.Damage.StringWithAverage;
                labelDamageLarge.Text = item.DamageLarge.StringWithAverage;
                labelType.Text = TypeString(item.ItemBaseType);
            }

            OnItemChanged(sender, e);
        }

        private void comboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            EOBLocationTag tag = comboLocation.SelectedItem as EOBLocationTag;
            if (tag == null)
                return;

            UpdateLocationEnabled(!tag.Quiver && !tag.Nowhere && !tag.Empty);
            OnItemChanged(sender, e);
        }

        private void UpdateLocationEnabled(bool bMap)
        {
            m_bUpdating = true;
            if (bMap && (nudMap.Value == 0 || nudMap.Value == 255))
                Global.SetNud(nudMap, 1);
            nudPositionX.Enabled = bMap;
            nudPositionY.Enabled = bMap;
            nudMap.Enabled = bMap;
            m_bUpdating = false;
        }

        private void tbBytes_TextChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            byte[] bytes = Global.BytesFromRelaxedString(tbBytes.Text, 14);
            bool bValid = bytes != null && bytes.Length > 13;
            labelInvalidBytes.Visible = !bValid;
            if (bValid)
            {
                try
                {
                    EOBItem item = EOBItem.FromItemListBytes(m_item.Game, bytes);
                    item.ItemListIndex = m_item.ItemListIndex;
                    UpdateUI(item, false);
                }
                catch (Exception)
                {
                    labelInvalidBytes.Visible = true;
                }
            }
        }

        private void EditEOBMasterItemForm_Load(object sender, EventArgs e)
        {
            ActiveControl = tbBytes;
            tbBytes.Focus();
            tbBytes.SelectAll();
        }
    }

    public class EOBBasicIndexTag
    {
        public EOBItemIndex Index { get; set; }

        public EOBBasicIndexTag(EOBItemIndex index)
        {
            Index = index;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", (int)Index, EOBItem.GetName(Index));
        }

        public EOBItem GetItem()
        {
            if ((int)Index < 0 || (int)Index >= EOB1.Items.Count)
                return null;
            return EOB1.Items[(int)Index];
        }
    }

    public class EOBItemStringTag
    {
        public GameNames Game;
        public int Index { get; set; }

        public EOBItemStringTag(GameNames game, int index)
        {
            Game = game;
            Index = index;
        }

        public override string ToString()
        {
            switch (Game)
            {
                case GameNames.EyeOfTheBeholder1:
                    return String.Format("{0}: {1}", Index, EOB1Item.GetName((EOB1ItemStringIndex) Index));
                case GameNames.EyeOfTheBeholder2:
                    return String.Format("{0}: {1}", Index, EOB2Item.GetName((EOB2ItemStringIndex) Index));
                default:
                    return String.Format("{0}: Unknown Game!", Index);
            }
        }
    }

    public class EOBItemGraphicTag
    {
        public EOBItemGraphic Index { get; set; }

        public EOBItemGraphicTag(EOBItemGraphic index)
        {
            Index = index;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", (int) Index, EOBItem.GraphicString(Index));
        }
    }

    public class EOBLocationTag
    {
        public EOBItemLocation Location { get; set; }
        public int Map { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Quiver { get { return Map == 255; } }
        public bool Nowhere { get { return Map == 0; } }
        public bool Empty { get { return X == 255 && Y == 255; } }

        public EOBLocationTag(EOBItemLocation location, int iMap = 0, int x = 0, int y = 0)
        {
            Location = location;
            Map = iMap;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            if (Empty) return "Empty";
            if (Quiver) return "Quiver";
            if (Nowhere) return "Nowhere";
            switch (Location)
            {
                case EOBItemLocation.NorthEast: return "Northeast";
                case EOBItemLocation.NorthWest: return "Northwest";
                case EOBItemLocation.SouthEast: return "Southeast";
                case EOBItemLocation.SouthWest: return "Southwest";
                case EOBItemLocation.Alcove: return "Alcove";
                default: return String.Format("Unknown({0})", (int)Location);
            }
        }
    }
}
