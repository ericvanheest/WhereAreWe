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
    public interface IEditableItemForm
    {
        GameNames Game { get; set; }
        Item Item { get; set; }
        DialogResult ShowDialog();
    }

    public partial class ItemEditForm : CommonKeyForm, IEditableItemForm
    {
        private Item m_item;
        private GameNames m_game;
        private bool m_bUpdating = false;

        public ItemEditForm(GameNames game)
        {
            m_game = game;
            InitializeComponent();
        }

        public GameNames Game { get { return m_game; } set { m_game = value; } }

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
                m_item = value;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            int i;
            comboItem.Items.Clear();
            comboAlignment.Items.Clear();

            int iDefaultCharges = Games.IsBardsTale(Game) ? 255 : 0;
            nudCharges.Value = (m_item == null ? iDefaultCharges : m_item.ChargesCurrent == -1 ? 0 : m_item.ChargesCurrent);

            comboAlignment.Items.Add("Any");
            comboAlignment.Items.Add("Evil");
            comboAlignment.Items.Add("Good");
            comboAlignment.Items.Add("Neutral");

            switch (Game)
            {
                case GameNames.MightAndMagic1:
                    if (m_item == null)
                        m_item = MM1.Items[0].Clone();
                    for (i = 0; i < 256; i++)
                        comboItem.Items.Add(String.Format("{0}: {1}", i, MM1.Items[i].FormatDescription(Properties.Settings.Default.EditItemFormat)));
                    labelBonus.Visible = false;
                    labelAlignment.Visible = false;
                    nudBonus.Visible = false;
                    comboAlignment.Visible = false;
                    labelNoteCursed.Visible = false;
                    labelDescription.Location = labelAlignment.Location;
                    tbDescription.Height += tbDescription.Top - comboAlignment.Top;
                    tbDescription.Top = comboAlignment.Top;
                    cbCursed.Visible = false;
                    cbIdentified.Visible = false;
                    break;
                case GameNames.MightAndMagic2:
                    if (m_item == null)
                        m_item = MM2.Items[0].Clone();
                    for (i = 0; i < 256; i++)
                        comboItem.Items.Add(String.Format("{0}: {1}", i, MM2.Items[i].GetLongDescription()));
                    nudBonus.Value = (int) (((MM2Item)m_item).BonusCurrent & MM2BonusFlags.PlusFlags);
                    int iAlignment = (int) (((MM2Item)m_item).BonusCurrent & MM2BonusFlags.AlignmentFlags) >> 6;
                    comboAlignment.SelectedIndex = iAlignment;
                    cbCursed.Visible = false;
                    cbIdentified.Visible = false;
                    break;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5:
                    WizGameGlobals globals = Games.GetWizGlobals(Game);
                    if (m_item == null)
                    {
                        m_item = globals.GetClonedItem(0);
                        ((WizItem)m_item).Identified = true;
                    }
                    List<WizItem> itemList = globals.GetItems();
                    for (i = 0; i < itemList.Count; i++)
                        comboItem.Items.Add(String.Format("{0}: {1}", i, globals.GetDirectItem(i).FormatDescription(Properties.Settings.Default.EditItemFormat)));
                    labelBonus.Visible = false;
                    labelAlignment.Visible = false;
                    nudBonus.Visible = false;
                    comboAlignment.Visible = false;
                    labelNoteCursed.Visible = false;
                    labelCharges.Visible = false;
                    nudCharges.Visible = false;
                    labelDescription.Location = labelAlignment.Location;
                    tbDescription.Height += tbDescription.Top - comboAlignment.Top;
                    tbDescription.Top = comboAlignment.Top;
                    cbCursed.Left = nudCharges.Left;
                    cbCursed.Checked = ((WizItem)m_item).Cursed;
                    cbIdentified.Left = nudBonus.Left;
                    cbIdentified.Checked = ((WizItem)m_item).Identified;
                    break;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    BTGameGlobals btGlobals = Games.GetBTGlobals(Game);
                    if (m_item == null)
                    {
                        m_item = btGlobals.GetClonedItem(0);
                        ((BTItem)m_item).Identified = true;
                    }
                    List<BTItem> btItemList = btGlobals.GetItems();
                    for (i = 0; i < btItemList.Count; i++)
                        comboItem.Items.Add(String.Format("{0}: {1}", i, btGlobals.GetDirectItem(i).FormatDescription(Properties.Settings.Default.EditItemFormat)));
                    labelBonus.Visible = false;
                    cbCursed.Visible = false;
                    nudBonus.Visible = false;
                    labelNoteCursed.Visible = false;
                    if (Game == GameNames.BardsTale3)
                    {
                        labelAlignment.Visible = true;
                        comboAlignment.Visible = true;
                        labelAlignment.Text = "Contains";
                        SetContains(comboAlignment, m_item as BT3Item);
                    }
                    else
                    {
                        labelAlignment.Visible = false;
                        comboAlignment.Visible = false;
                        labelDescription.Location = labelAlignment.Location;
                        tbDescription.Height += tbDescription.Top - comboAlignment.Top;
                        tbDescription.Top = comboAlignment.Top;
                    }
                    if (Game == GameNames.BardsTale1)
                    {
                        labelCharges.Visible = false;
                        nudCharges.Visible = false;
                        cbIdentified.Left = nudCharges.Left;
                    }
                    else
                    {
                        labelCharges.Visible = true;
                        nudCharges.Visible = true;
                        cbIdentified.Left = labelBonus.Left;
                    }
                    cbIdentified.Checked = ((BTItem)m_item).Identified;
                    break;
                default:
                    break;
            }

            if (m_item == null)
                return;

            comboItem.SelectedIndex = (m_item.Index < 1000 ? m_item.Index : m_item.Index - 1000);
        }

        private void SetContains(ComboBox combo, BT3Item item)
        {
            combo.Items.Clear();
            combo.Items.Add(new BT3ItemContents(BT3ItemFlags.FilledWater, BT3Item.ContentsString(BT3ItemFlags.FilledWater)));
            combo.Items.Add(new BT3ItemContents(BT3ItemFlags.FilledSpirits, BT3Item.ContentsString(BT3ItemFlags.FilledSpirits)));
            combo.Items.Add(new BT3ItemContents(BT3ItemFlags.FilledWaterOfLife, BT3Item.ContentsString(BT3ItemFlags.FilledWaterOfLife)));
            combo.Items.Add(new BT3ItemContents(BT3ItemFlags.FilledDragonBlood, BT3Item.ContentsString(BT3ItemFlags.FilledDragonBlood)));
            combo.Items.Add(new BT3ItemContents(BT3ItemFlags.FilledMoltenTar, BT3Item.ContentsString(BT3ItemFlags.FilledMoltenTar)));
            if (item == null)
                combo.SelectedIndex = 0;
            else
            {
                foreach (BT3ItemContents contents in combo.Items)
                {
                    if (contents.Flags == item.Contains)
                    {
                        combo.SelectedItem = contents;
                        break;
                    }
                }
            }
        }

        public void UpdateFromUI()
        {
            if (m_item == null)
                return;

            if (m_item is MM2Item)
            {
                m_item = MM2.Items[comboItem.SelectedIndex].Clone();
                MM2Item mm2Item = m_item as MM2Item;
                mm2Item.BonusCurrent = (MM2BonusFlags)((int)nudBonus.Value | (comboAlignment.SelectedIndex << 6));
            }
            else if (m_item is MM1Item)
            {
                m_item = MM1.Items[comboItem.SelectedIndex].Clone();
            }
            else if (m_item is WizItem)
            {
                m_item = Games.GetWizGlobals(m_item.Game).GetClonedItem(comboItem.SelectedIndex);
                m_item.Cursed = cbCursed.Checked;
                ((WizItem)m_item).Identified = cbIdentified.Checked;
            }
            else if (m_item is BTItem)
            {
                m_item = Games.GetBTGlobals(m_item.Game).GetClonedItem(comboItem.SelectedIndex);
                if (m_item is BT3Item)
                {
                    BT3Item bt3Item = m_item as BT3Item;
                    bt3Item.Contains = (bt3Item.BTType == BTItemType.Container ? ((BT3ItemContents)comboAlignment.SelectedItem).Flags : BT3ItemFlags.FilledWater);
                }
                ((BTItem)m_item).Identified = cbIdentified.Checked;
            }
            else
                m_item.Index = comboItem.SelectedIndex;

            m_item.ChargesCurrent = (byte)nudCharges.Value;
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
            m_bUpdating = true;
            comboItem.SelectedIndex = Global.Rand.Next(comboItem.Items.Count);
            nudBonus.Value = Global.Rand.Next(64);
            nudCharges.Value = Global.Rand.Next(256);
            comboAlignment.SelectedIndex = Global.Rand.Next(comboAlignment.Items.Count);
            m_bUpdating = false;
            UpdateDescription();
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindNext()
        {
            Global.FindNext(comboItem, tbFindItem.Text);
        }

        private void FindPrevious()
        {
            Global.FindNext(comboItem, tbFindItem.Text, false);
        }

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            tbFindItem.Focus();
            tbFindItem.Select();
        }

        private void comboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescription();
            if (Game == GameNames.BardsTale3)
                comboAlignment.Enabled = (m_item as BT3Item).BTType == BTItemType.Container;
        }

        private void UpdateDescription()
        {
            if (comboItem.SelectedIndex == -1 || m_bUpdating)
                return;

            m_bUpdating = true;
            UpdateFromUI();
            tbDescription.Text = m_item.MultiLineDescription.Trim();
            m_bUpdating = false;
        }

        private void llItemDisplayFormat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditItemDisplayFormatForm form = new EditItemDisplayFormatForm();
            form.DisplayFormat = Properties.Settings.Default.EditItemFormat;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.EditItemFormat = form.DisplayFormat;
                UpdateUI();
            }
        }

        private void nudCharges_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void nudBonus_ValueChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }

        private void comboAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }
    }

    public class BT3ItemContents
    {
        public string Text;
        public BT3ItemFlags Flags;

        public BT3ItemContents(BT3ItemFlags flags, string text)
        {
            Text = text;
            Flags = flags;
        }

        public override string ToString() { return Text; }
    }
}
