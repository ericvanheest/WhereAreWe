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
    public partial class MM3ItemEditForm : CommonKeyForm, IEditableItemForm
    {
        private MM3Item m_item;
        private GameNames m_game;

        public MM3ItemEditForm(GameNames game)
        {
            m_game = game;
            InitializeComponent();
        }

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

        public Item Item
        {
            get
            {
                UpdateFromUI();
                return m_item;
            }

            set
            {
                if (value is MM3Item)
                    m_item = (MM3Item)value;
                UpdateUI();
            }
        }

        public GameNames Game { get { return m_game; } set { m_game = value; } }

        public void UpdateUI()
        {
            int i;
            comboItem.Items.Clear();
            comboElemental.Items.Clear();
            comboMaterial.Items.Clear();
            comboAttribute.Items.Clear();
            comboProperty.Items.Clear();

            comboItem.Items.Add("0: <empty>");
            comboElemental.Items.Add("0: <none>");
            comboMaterial.Items.Add("0: <none>");
            comboAttribute.Items.Add("0: <none>");
            comboProperty.Items.Add("0: <none>");
            comboEquipped.Items.Add("0: <not equipped>");
            for (i = 1; i < (int)MM3ItemIndex.Invalid; i++)
                comboItem.Items.Add(String.Format("{0}: {1}{2}",
                    i,
                    Global.Title(MM3Item.GetItemName((MM3ItemIndex) i)),
                    Global.SpaceParen(MM3Item.DamageOrAC((MM3ItemIndex) i, false))
                    ));
            for (i = 1; i < (int)MM3ItemElementalIndex.Invalid; i++)
                comboElemental.Items.Add(String.Format("{0}: {1}{2}",
                    i,
                    Global.Title(MM3Item.GetItemElementalString((MM3ItemElementalIndex)i)),
                    Global.SpaceParen(MM3Item.ElementalEffect((MM3ItemElementalIndex)i).ToString() + ", " + MM3Item.LevelRange((MM3ItemElementalIndex)i).LevelString)
                    ));
            for (i = 1; i < (int)MM3ItemMaterialIndex.Invalid; i++)
                comboMaterial.Items.Add(String.Format("{0}: {1}{2}",
                    i,
                    Global.Title(MM3Item.GetItemMaterialString((MM3ItemMaterialIndex)i)),
                    Global.SpaceParen(MM3Item.MaterialEffect((MM3ItemMaterialIndex)i).ToString() + ", " + MM3Item.LevelRange((MM3ItemMaterialIndex)i).LevelString)
                    ));
            for (i = 1; i < (int)MM3ItemAttributeIndex.Invalid; i++)
                comboAttribute.Items.Add(String.Format("{0}: {1}{2}",
                    i,
                    Global.Title(MM3Item.GetItemAttributeString((MM3ItemAttributeIndex)i)),
                    Global.SpaceParen(MM3Item.AttributeEffect((MM3ItemAttributeIndex)i).ToString() + ", " + MM3Item.LevelRange((MM3ItemAttributeIndex)i).LevelString)
                    ));
            for (i = 1; i < (int)MM3ItemPropertyIndex.Invalid; i++)
                comboProperty.Items.Add(String.Format("{0}: of {1}{2}",
                    i,
                    Global.Title(MM3Item.GetItemPropertyString((MM3ItemPropertyIndex)i)),
                    Global.SpaceParen(MM3SpellList.GetSpellName(MM3Item.ItemPropertyEffect((MM3ItemPropertyIndex)i)) + ", " + MM3Item.LevelRange((MM3ItemPropertyIndex)i).LevelString)
                    ));
            for (i = 1; i < (int)EquipLocation.Invalid; i++)
                comboEquipped.Items.Add(String.Format("{0}: {1}",
                    i,
                    Global.Title(MM3Item.GetEquipLocationString((EquipLocation)i))
                    ));

            if (m_item != null)
            {
                comboItem.SelectedIndex = m_item.Index;
                comboElemental.SelectedIndex = (int)m_item.Element;
                comboMaterial.SelectedIndex = (int)m_item.Material;
                comboAttribute.SelectedIndex = (int)m_item.Attribute;
                comboProperty.SelectedIndex = (int)m_item.Property;
                nudCharges.Value = m_item.m_iChargesCurrent;
                cbBroken.Checked = m_item.Broken;
                cbCursed.Checked = m_item.Cursed;
                comboEquipped.SelectedIndex = (int)m_item.WhereEquipped;
            }
            else
            {
                comboItem.SelectedIndex = 0;
                comboElemental.SelectedIndex = 0;
                comboMaterial.SelectedIndex = 0;
                comboAttribute.SelectedIndex = 0;
                comboProperty.SelectedIndex = 0;
                comboEquipped.SelectedIndex = 0;
                nudCharges.Value = 0;
                cbBroken.Checked = false;
                cbCursed.Checked = false;
            }
        }

        public void UpdateFromUI()
        {
            m_item = GetItemFromUI();
        }

        public MM3Item GetItemFromUI()
        {
            MM3Item item = MM3Item.Create();

            if (comboItem.SelectedIndex == -1)
                comboItem.SelectedIndex = 0;
            if (comboElemental.SelectedIndex == -1)
                comboElemental.SelectedIndex = 0;
            if (comboMaterial.SelectedIndex == -1)
                comboMaterial.SelectedIndex = 0;
            if (comboAttribute.SelectedIndex == -1)
                comboAttribute.SelectedIndex = 0;
            if (comboProperty.SelectedIndex == -1)
                comboProperty.SelectedIndex = 0;
            if (comboEquipped.SelectedIndex == -1)
                comboEquipped.SelectedIndex = 0;

            item.Index = comboItem.SelectedIndex;
            item.Base = (MM3ItemIndex)item.Index;
            item.m_iChargesCurrent = (byte) nudCharges.Value;
            item.Cursed = cbCursed.Checked;
            item.Broken = cbBroken.Checked;
            item.Element = (MM3ItemElementalIndex)comboElemental.SelectedIndex;
            item.Material = (MM3ItemMaterialIndex)comboMaterial.SelectedIndex;
            item.Attribute = (MM3ItemAttributeIndex)comboAttribute.SelectedIndex;
            item.Property = (MM3ItemPropertyIndex)comboProperty.SelectedIndex;
            item.WhereEquipped = (EquipLocation)comboEquipped.SelectedIndex;

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
            comboItem.SelectedIndex = Global.Rand.Next(comboItem.Items.Count);
            comboElemental.SelectedIndex = Global.Rand.Next(comboElemental.Items.Count);
            comboMaterial.SelectedIndex = Global.Rand.Next(comboMaterial.Items.Count);
            comboAttribute.SelectedIndex = Global.Rand.Next(comboAttribute.Items.Count);
            comboProperty.SelectedIndex = Global.Rand.Next(comboProperty.Items.Count);
            nudCharges.Value = Global.Rand.Next((int) nudCharges.Maximum+1);
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
            UpdateDescription();
        }

        private void UpdateDescription()
        {
            MM3Item item = GetItemFromUI();

            labelDescription.Text = item.DescriptionString;
        }
    }
}
