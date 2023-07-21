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
    public partial class MM45ItemEditForm : CommonKeyForm, IEditableItemForm
    {
        private MM45Item m_item;
        private GameNames m_game;

        private bool m_bAllowWeapons = true;
        private bool m_bAllowArmor = true;
        private bool m_bAllowAccessories = true;
        private bool m_bAllowMisc = true;

        private bool m_bCreatingComboBoxes = false;

        protected override bool OnCommonKeyPrevious()
        {
            FindPrevious();
            return true;
        }

        protected override bool OnCommonKeyNext(bool bIncludeCurrent)
        {
            FindNext();
            return true;
        }

        public bool AllowWeapons
        {
            get { return m_bAllowWeapons; }
            set { m_bAllowWeapons = value; CheckButtons();
            }
        }

        public bool AllowArmor
        {
            get { return m_bAllowArmor; }
            set { m_bAllowArmor = value; CheckButtons(); }
        }

        public bool AllowAccessories
        {
            get { return m_bAllowAccessories; }
            set { m_bAllowAccessories = value; CheckButtons(); }
        }

        public bool AllowMisc
        {
            get { return m_bAllowMisc; }
            set { m_bAllowMisc = value; CheckButtons(); }
        }

        public void AllowOnly(ItemType type)
        {
            m_bAllowWeapons = (type == ItemType.Weapon);
            m_bAllowArmor = (type == ItemType.Armor);
            m_bAllowAccessories = (type == ItemType.Accessory);
            m_bAllowMisc = (type == ItemType.Miscellaneous);
            CheckButtons();
        }

        public bool AnyAllowed { get { return m_bAllowAccessories || m_bAllowArmor || m_bAllowMisc || m_bAllowWeapons; } }

        public MM45ItemEditForm(GameNames game)
        {
            m_game = game;
            InitializeComponent();
        }

        private void CheckButtons()
        {
            rbWeapon.Enabled = m_bAllowWeapons;
            rbArmor.Enabled = m_bAllowArmor;
            rbAccessory.Enabled = m_bAllowAccessories;
            rbMisc.Enabled = m_bAllowMisc;

            if (rbWeapon.Checked && !m_bAllowWeapons)
                SelectAvailableButton();
            if (rbArmor.Checked && !m_bAllowArmor)
                SelectAvailableButton();
            if (rbAccessory.Checked && !m_bAllowAccessories)
                SelectAvailableButton();
            if (rbMisc.Checked && !m_bAllowMisc)
                SelectAvailableButton();

            int iCount = 0;
            foreach(bool b in new bool[] { m_bAllowWeapons, m_bAllowMisc, m_bAllowArmor, m_bAllowAccessories })
                if (b)
                    iCount++;

            rbWeapon.Visible = (iCount != 1);
            rbArmor.Visible = (iCount != 1);
            rbAccessory.Visible = (iCount != 1);
            rbMisc.Visible = (iCount != 1);
            labelTypePrefix.Visible = (iCount == 1);
            labelType.Visible = (iCount == 1);
            labelType.Text = MM45ItemBase.TypeString(GetSelectedType());
        }

        private void SelectAvailableButton()
        {
            if (m_bAllowWeapons)
                rbWeapon.Checked = true;
            else if (m_bAllowArmor)
                rbArmor.Checked = true;
            else if (m_bAllowAccessories)
                rbAccessory.Checked = true;
            else
                rbMisc.Checked = true;
        }

        public Item Item
        {
            get
            {
                UpdateFromUI();
                return m_item;
            }

            set
            {
                if (value is MM45Item)
                    m_item = (MM45Item)value;
                UpdateUI();
            }
        }

        public GameNames Game { get { return m_game; } set { m_game = value; } }

        private void AddItem(ComboBox combo, MM45ItemBase item)
        {
            combo.Items.Add(String.Format("{0}: {1} ({2}{3} gold)",
                item.Index,
                Global.Title(MM45Item.GetItemName(item)),
                Global.AddCommaSpace(MM45Item.DamageOrAC(item, false)),
                MM45Item.BaseItemValue(item.Type, item.Index)
                ));
        }

        public void SetSelectedType(ItemType type)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    rbWeapon.Checked = true;
                    break;
                case ItemType.Armor:
                    rbArmor.Checked = true;
                    break;
                case ItemType.Accessory:
                    rbAccessory.Checked = true;
                    break;
                case ItemType.Miscellaneous:
                    rbMisc.Checked = true;
                    break;
                default:
                    break;
            }
        }

        public ItemType GetSelectedType()
        {
            if (rbWeapon.Checked)
                return ItemType.Weapon;
            else if (rbArmor.Checked)
                return ItemType.Armor;
            else if (rbAccessory.Checked)
                return ItemType.Accessory;
            return ItemType.Miscellaneous;
        }

        public void UpdateUI()
        {
            m_bCreatingComboBoxes = true;

            comboItem.Items.Clear();
            comboEquipped.Items.Clear();
            comboPrefix.Items.Clear();
            comboSuffix.Items.Clear();

            comboItem.Items.Add("0: <empty>");
            comboEquipped.Items.Add("0: <not equipped>");
            comboPrefix.Items.Add("0: <none>");
            comboSuffix.Items.Add("0: <none>");

            ItemType type = (m_item == null ? GetSelectedType() : m_item.Base.Type);

            nudCharges.Enabled = false;

            switch (type)
            {
                case ItemType.Weapon:
                    for (int i = 1; i < (int)MM45WeaponIndex.Invalid; i++)
                        AddItem(comboItem, new MM45ItemBase(ItemType.Weapon, i));
                    for (int i = 1; i < (int)MM45WeaponSuffixIndex.Invalid; i++)
                        comboSuffix.Items.Add(String.Format("{0}: {1}{2}", i,
                            MM45Item.GetItemSuffix(ItemType.Weapon, i),
                            Global.SpaceParen(MM45Item.WeaponSuffixEffect((MM45WeaponSuffixIndex)i))
                            ));
                    comboSuffix.Enabled = true;
                    comboPrefix.Enabled = true;
                    break;
                case ItemType.Armor:
                    for (int i = 1; i < (int)MM45ArmorIndex.Invalid; i++)
                        AddItem(comboItem, new MM45ItemBase(ItemType.Armor, i));
                    comboSuffix.Enabled = false;
                    comboPrefix.Enabled = true;
                    break;
                case ItemType.Accessory:
                    for (int i = 1; i < (int)MM45AccessoryIndex.Invalid; i++)
                        AddItem(comboItem, new MM45ItemBase(ItemType.Accessory, i));
                    comboSuffix.Enabled = false;
                    comboPrefix.Enabled = true;
                    break;
                case ItemType.Miscellaneous:
                    nudCharges.Enabled = true;
                    for (int i = 1; i < (int)MM45MiscItemIndex.Invalid; i++)
                        AddItem(comboItem, new MM45ItemBase(ItemType.Miscellaneous, i));
                    comboSuffix.Enabled = true;
                    comboPrefix.Enabled = false;
                    break;
                default:
                    break;
            }

            if (type != ItemType.Weapon)
            {
                for (int i = 1; i < (int)MM45ItemSuffixIndex.Invalid; i++)
                    comboSuffix.Items.Add(String.Format("{0}: {1}{2}", i,
                        MM45Item.GetItemSuffix(ItemType.Miscellaneous, i),
                        Global.SpaceParen(MM45Item.MiscItemSuffixEffect((MM45ItemSuffixIndex) i))
                        ));
            }

            for (int i = 1; i < (int)MM45ItemPrefixIndex.Invalid; i++)
                comboPrefix.Items.Add(String.Format("{0}: {1}{2}", i,
                    MM45Item.GetItemPrefix(type, (MM45ItemPrefixIndex) i),
                    Global.SpaceParen(MM45Item.ItemPrefixEffect(type, (MM45ItemPrefixIndex)i))
                    ));

            for (int i = 1; i < (int)EquipLocation.Invalid; i++)
                comboEquipped.Items.Add(String.Format("{0}: {1}",
                    i,
                    Global.Title(MM45Item.GetEquipLocationString((EquipLocation)i))
                    ));

            if (m_item != null)
            {
                comboItem.SelectedIndex = m_item.Index;
                nudCharges.Value = m_item.m_iChargesCurrent;
                cbBroken.Checked = m_item.Broken;
                cbCursed.Checked = m_item.Cursed;
                if (comboPrefix.Enabled)
                    comboPrefix.SelectedIndex = (int)m_item.Prefix;
                else
                    comboPrefix.SelectedIndex = 0;
                if (comboSuffix.Enabled)
                    comboSuffix.SelectedIndex = m_item.Suffix;
                else
                    comboSuffix.SelectedIndex = 0;
                comboEquipped.SelectedIndex = (int)m_item.WhereEquipped;
            }
            else
            {
                comboItem.SelectedIndex = 0;
                comboEquipped.SelectedIndex = 0;
                comboPrefix.SelectedIndex = 0;
                comboSuffix.SelectedIndex = 0;
                nudCharges.Value = 0;
                cbBroken.Checked = false;
                cbCursed.Checked = false;
            }

            m_bCreatingComboBoxes = false;

            UpdateDescription();
        }

        public void UpdateFromUI()
        {
            m_item = GetItemFromUI();
        }

        public MM45Item GetItemFromUI()
        {
            MM45Item item = MM45Item.Create();
            item.Base.Type = GetSelectedType();


            item.Index = comboItem.SelectedIndex;
            item.Prefix = (MM45ItemPrefixIndex) comboPrefix.SelectedIndex;
            item.Suffix = comboSuffix.SelectedIndex;
            item.m_iChargesCurrent = (byte) nudCharges.Value;
            item.Broken = cbBroken.Checked;
            item.Cursed = cbCursed.Checked;
            item.WhereEquipped = (EquipLocation) comboEquipped.SelectedIndex;

            return item;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (comboPrefix.Enabled)
                comboPrefix.SelectedIndex = Global.Rand.Next(0, comboPrefix.Items.Count);
            if (comboSuffix.Enabled)
                comboSuffix.SelectedIndex = Global.Rand.Next(0, comboSuffix.Items.Count);
            comboItem.SelectedIndex = Global.Rand.Next(1, comboItem.Items.Count);
            if (nudCharges.Enabled)
                nudCharges.Value = Global.Rand.Next(0, (int) nudCharges.Maximum+1);
            cbBroken.Checked = (Global.Rand.Next(5) == 0);
            cbCursed.Checked = (Global.Rand.Next(5) == 0);
        }

        private void tbFindItem_TextChanged(object sender, EventArgs e)
        {
            int iIndex = 0;
            while(iIndex < comboItem.Items.Count)
            {
                if (comboItem.Items[iIndex].ToString().ToLower().Contains(tbFindItem.Text.ToLower()))
                {
                    comboItem.SelectedIndex = iIndex;
                    return;
                }
                iIndex++;
            }
        }

        private void FindNext()
        {
            Global.FindNext(comboItem, tbFindItem.Text);
        }

        private void FindPrevious()
        {
            Global.FindNext(comboItem, tbFindItem.Text, false);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            tbFindItem.Focus();
            tbFindItem.Select();
        }

        private void ControlChanged(object sender, EventArgs e)
        {
            if (m_bCreatingComboBoxes)
                return;

            UpdateDescription();
        }

        private void UpdateDescription()
        {
            MM45Item item = GetItemFromUI();

            if (item.Base.Index == 0)
                labelDescription.Text = "(no item type selected)";
            else
                labelDescription.Text = item.DescriptionString;
        }

        private void rbItemType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
