using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditItemDisplayFormatForm : Form
    {
        private int m_iVarHeight = 0;
        private int m_iMinHeight = 0;
        private int m_iSplitPos = 0;
        private MMItem m_itemTest = MM3Item.Create(0, 200, (byte)MM3ItemElementalIndex.Ectoplasmic, (byte)MM3ItemMaterialIndex.Emerald,
            (byte)MM3ItemAttributeIndex.Divine, (byte)MM3ItemIndex.Flamberge, (byte)MM3ItemPropertyIndex.Implosions);

        public EditItemDisplayFormatForm()
        {
            InitializeComponent();
            m_iSplitPos = splitContainer1.SplitterDistance;
            m_iMinHeight = MinimumSize.Height;
            ShowVariables = false;
            foreach (string str in Global.ItemFormats)
                comboFormat.Items.Add(str);
            FillVariablesList();
        }

        public String DisplayFormat
        {
            get { return comboFormat.Text; }
            set { comboFormat.Text = value; }
        }

        public bool ShowVariables
        {
            get { return !splitContainer1.Panel2Collapsed; }
            set
            {
                if (splitContainer1.Panel2Collapsed)
                {
                    Height = m_iVarHeight;
                    splitContainer1.Panel2Collapsed = false;
                    Global.SetSplitterDistance(splitContainer1, m_iSplitPos);
                    llVariables.Text = "Hide &Variables";
                }
                else
                {
                    m_iVarHeight = Height;
                    Height = m_iMinHeight;
                    splitContainer1.Panel2Collapsed = true;
                    llVariables.Text = "Show &Variables";
                }
            }
        }

        public void UpdateUI()
        {
            labelExample.Text = m_itemTest.FormatDescription(comboFormat.Text, GenericAlignmentValue.Good, GenericClass.Cleric);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void llVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowVariables = !ShowVariables;
        }

        private void AddVariable(string strVar, string strDescription)
        {
            ListViewItem lvi = new ListViewItem(strVar);
            lvi.SubItems.Add(strDescription);
            lvi.Tag = strVar;
            lvVariables.Items.Add(lvi);
        }

        private void FillVariablesList()
        {
            lvVariables.BeginUpdate();
            lvVariables.Items.Clear();
            AddVariable("$[AC]", "The total armor class for a piece of armor, including bonuses");
            AddVariable("$[Align]", "The item's alignment (or \"Any\" if unaligned)");
            AddVariable("$[AlignShort]", "The item's abbreviated alignment (G/N/E/A)");
            AddVariable("$[Attribute]", "The item attribute (e.g. \"Genius\" or \"Velocity\")");
            AddVariable("$[BasicName]", "The unadorned item name (e.g. \"sword\" or \"cloak\")");
            AddVariable("$[Broken]", "\"BROKEN\" if the item is broken");
            AddVariable("$[Charges]", "The number of charges remaining");
            AddVariable("$[Cursed]", "\"CURSED\" if the item is cursed");
            AddVariable("$[DamAC]", "Either the damage or armor class, depending on the item type");
            AddVariable("$[Damage]", "The total damage of a weapon, including bonuses");
            AddVariable("$[AvgDamage]", "The average damage of a weapon per-swing");
            AddVariable("$[Duplicatable]", "\"Yes\" or \"No\" if the \"Duplicate Item\" spell can copy this item");
            AddVariable("$[Element]", "The item element (e.g. \"Acidic\" or \"Blazing\")");
            AddVariable("$[Equip]", "The effects, if any, of equipping this item");
            AddVariable("$[Index]", "The internal index of this item");
            AddVariable("$[LongDescription]", "The default long description for items");
            AddVariable("$[Material]", "The item material (e.g. \"Steel\" or \"Diamond\")");
            AddVariable("$[Name]", "The item's full in-game name");
            AddVariable("$[Property]", "The item property (e.g. \"of Distortion\" or \"of the GODS!\")");
            AddVariable("$[ToHit]", "The to-hit bonus for a weapon");
            AddVariable("$[Type]", "The abbreviated item type (e.g. \"1H\" or \"Armor\")");
            AddVariable("$[TypeLong]", "The item type (e.g. \"One-Handed Weapon\")");
            AddVariable("$[Usable]", "\"Yes\" or \"No\" if the current character may equip the item or not");
            AddVariable("$[UsableBy]", "The abbreviated list of classes which can use this item (e.g. \"KPAR\")");
            AddVariable("$[Use]", "The effects, if any, of using this item");
            AddVariable("$[Value]", "The value of the item in gold pieces");
            AddVariable("$[Why]", "Short explanation of why the item can't be used by this character"); 
            lvVariables.EndUpdate();
        }

        private void lvVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lvVariables.SelectedItems.Count < 0)
                return;

            string strVar = (string)lvVariables.SelectedItems[0].Tag;

            if (comboFormat.SelectionStart < 0 || comboFormat.SelectionStart >= comboFormat.Text.Length)
            {
                comboFormat.Text += strVar;
                comboFormat.SelectionStart = comboFormat.Text.Length;
            }
            else
            {
                int iStart = comboFormat.SelectionStart;
                comboFormat.Text = String.Format("{0}{1}{2}",
                    comboFormat.Text.Substring(0, comboFormat.SelectionStart),
                    strVar,
                    comboFormat.Text.Substring(comboFormat.SelectionStart + comboFormat.SelectionLength));
                comboFormat.SelectionStart = iStart + strVar.Length;
            }
        }

        private void comboFormat_TextChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void EditItemDisplayFormatForm_Load(object sender, EventArgs e)
        {
            comboFormat.Focus();
            comboFormat.Select();
        }
    }
}
