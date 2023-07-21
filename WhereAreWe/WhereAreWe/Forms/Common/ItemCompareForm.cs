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
    public partial class ItemCompareForm : CommonKeyForm
    {
        private Timer m_timerHideSelection = new Timer();
        private string m_strItemText;
        private bool m_bInitialized = false;
        private Item m_item = null;

        public ItemCompareForm()
        {
            InitializeComponent();
            m_timerHideSelection.Tick += m_timerHideSelection_Tick;
            CommonKeySelectAll += ViewInfoForm_CommonKeySelectAll;
            Characters = null;
            MoveToCharacter = -1;
            ItemOwner = -1;
        }

        void ViewInfoForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            tbInfo.SelectionStart = 0;
            tbInfo.SelectionLength = tbInfo.Text.Length;
        }

        void m_timerHideSelection_Tick(object sender, EventArgs e)
        {
            m_timerHideSelection.Stop();
            tbInfo.SelectionStart = 0;
            tbInfo.SelectionLength = 0;
            lvCompare.Focus();
            lvCompare.Select();
            m_bInitialized = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public Form CenteringForm { get; set; }
        public int ItemOwner { get; set; }
        public int MoveToCharacter { get; set; }
        public List<BaseCharacter> Characters { get; set; }
        public Item Item
        {
            get { return m_item; }

            set
            {
                m_item = value;
                if (!m_item.IsIdentified && !m_item.RevealUnidentified && Properties.Settings.Default.HideUnidentifiedItems)
                    m_strItemText = Global.UnidentifiedItemTip;
                else
                    m_strItemText = m_item.MultiLineDescription.Trim();
                tbInfo.Text = WordWrap(m_strItemText);
                SizeToText();
            }
        }

        private string WordWrap(string str)
        {
            int iMax = m_bInitialized ? tbInfo.Width - 4 : 800;
            if (TextRenderer.MeasureText(str, tbInfo.Font).Width < iMax)
                return str;

            StringBuilder sb = new StringBuilder();
            int iLastWhitespace = -1;
            int iLastLine = 0;
            for(int iCurrent = 0; iCurrent < str.Length; iCurrent++)
            {
                if (!Char.IsWhiteSpace(str[iCurrent]))
                {
                    sb.Append(str[iCurrent]);
                    continue;
                }

                if (TextRenderer.MeasureText(str.Substring(iLastLine, iCurrent - iLastLine), tbInfo.Font).Width > iMax)
                {
                    if (iLastWhitespace > 0)
                    {
                        sb.Insert(iLastWhitespace, "\r\n");
                        while (sb.Length > iLastWhitespace + 2 && Char.IsWhiteSpace(sb[iLastWhitespace + 2]))
                            sb.Remove(iLastWhitespace + 2, 1);
                    }
                    else
                        sb.Append("\r\n");
                    iLastLine = iCurrent + 1;
                }

                sb.Append(str[iCurrent]);
                iLastWhitespace = sb.Length;
            }

            return sb.ToString();
        }

        private void SizeToText()
        {
            Size sz = TextRenderer.MeasureText(tbInfo.Text, tbInfo.Font);

            int iMarginHoriz = Width - tbInfo.Width;
            int iMarginSplit = splitContainer1.Panel1.Bottom - tbInfo.Height;
            int iMarginVert = Height - tbInfo.Height;

            int iNewWidth = iMarginHoriz + sz.Width + 10;
            int iNewSplit = sz.Height + 10;
            int iNewHeight = sz.Height + iMarginVert + splitContainer1.Top;

            if (Width < iNewWidth)
                Width = iNewWidth;

            Height = iNewHeight;

            if (!splitContainer1.Panel2Collapsed)
            {
                if (splitContainer1.SplitterDistance < iNewSplit)
                {
                    iNewHeight += (iNewSplit - splitContainer1.SplitterDistance);
                    Height = iNewHeight;
                    splitContainer1.SplitterDistance = iNewSplit;
                }
            }
        }

        private void ViewInfoForm_Load(object sender, EventArgs e)
        {
            m_timerHideSelection.Interval = 10;
            m_timerHideSelection.Start();
            bool bUnidentified = false;

            if (Item != null)
            {
                if (!Item.IsIdentified && Properties.Settings.Default.HideUnidentifiedItems)
                {
                    bUnidentified = true;
                    Text = String.Format("Unidentified {0}", Item.ItemNoun);
                }
                else
                    Text = Item.DescriptionString;
            }

            if (Characters == null || Item == null || Item.CanEquipLocation == EquipLocation.None || bUnidentified)
                HideCompareView();
            else
            {
                UpdateCharacterUI();
                if (lvCompare.Items.Count < 1)
                    HideCompareView();
            }

            if (CenteringForm != null)
                Global.CenterForm(this, CenteringForm.RectangleToScreen(CenteringForm.DisplayRectangle));
        }

        private void HideCompareView()
        {
            int iHeight = splitContainer1.Panel2.Height;
            splitContainer1.Panel2Collapsed = true;
            SizeToText();
        }

        private bool UsesSameSlot(EquipLocation loc1, EquipLocation loc2)
        {
            if (loc1 == loc2)
                return true;
            if ((loc1 == EquipLocation.RightHand || loc1 == EquipLocation.LeftHand) && loc2 == EquipLocation.BothHands)
                return true;
            if ((loc2 == EquipLocation.RightHand || loc2 == EquipLocation.LeftHand) && loc1 == EquipLocation.BothHands)
                return true;
            return false;
        }

        private ListViewItem AddCompareItem(string strPlusMinus, int iChar, string strDamAC, string strEquip, string strDescription, string strName)
        {
            ListViewItem lvi = new ListViewItem(strPlusMinus);
            lvi.SubItems.Add(iChar.ToString());
            lvi.SubItems.Add(strDamAC);
            lvi.SubItems.Add(strEquip);
            lvi.SubItems.Add(strDescription);
            lvi.SubItems.Add(strName);
            lvCompare.Items.Add(lvi);
            return lvi;
        }

        private void UpdateCharacterUI()
        {
            bool bAnyDamAC = false;
            bool bAnyEquip = false;
            bool bHasDamage = false;
            bool bHasAC = false;
            for(int iChar = 0; iChar < Characters.Count; iChar++)
            {
                BaseCharacter baseChar = Characters[iChar];
                if (baseChar == null || baseChar.BasicInventory == null)
                    continue;

                ListViewItem lvi = null;
                if (m_item.IsUsableByAny(baseChar.BasicClass) && m_item.IsUsableByAny(baseChar.BasicAlignment.Temporary))
                {
                    // Show the item(s) currently in the equipped slot for this character
                    bool bAnyItems = false;
                    foreach (Item itemCompare in baseChar.BasicInventory.Items)
                    {
                        if (itemCompare.IsEquipped && UsesSameSlot(m_item.CanEquipLocation, itemCompare.CanEquipLocation))
                        {
                            if (itemCompare.BaseDamage.Quantity > 0)
                                bHasDamage = true;
                            if (itemCompare.BaseDamage.Quantity == 0 && itemCompare.ArmorClassFull != 0)
                                bHasAC = true;
                            string strDamAC = itemCompare.BaseDamage.Quantity > 0 ? itemCompare.DamageStringFull : itemCompare.ArmorClassFull.ToString();
                            if (!String.IsNullOrWhiteSpace(strDamAC) && strDamAC != "0")
                                bAnyDamAC = true;
                            string strEquip = itemCompare.EquipEffects;
                            if (!String.IsNullOrWhiteSpace(strEquip))
                                bAnyEquip = true;
                            MMItem.CompareResult compare = m_item.CompareTo(itemCompare);
                            string strCompare = " ";
                            if (compare == MMItem.CompareResult.Better)
                                strCompare = "+";
                            else if (compare == MMItem.CompareResult.Worse)
                                strCompare = "-";
                            lvi = AddCompareItem(strCompare, iChar+1, strDamAC, strEquip, itemCompare.DescriptionString, baseChar.Name);
                            lvi.Tag = new CompareItemTag(iChar, baseChar, itemCompare, compare);
                            bAnyItems = true;
                        }
                    }
                    if (!bAnyItems)
                    {
                        string strCompare = m_item.Cursed ? " " : "+";  // Cursed items aren't necessarily better than nothing
                        lvi = AddCompareItem(strCompare, iChar+1, String.Empty, String.Empty, "<none>", baseChar.Name);
                        lvi.Tag = new CompareItemTag(iChar, baseChar, null, MMItem.CompareResult.Better);
                    }

                }
                else
                {
                    lvi = AddCompareItem("X", iChar+1, String.Empty, String.Empty, "<can't use>", baseChar.Name);
                    lvi.Tag = new CompareItemTag(iChar, baseChar, null);
                    lvi.Tag = new CompareItemTag(iChar, baseChar, null);
                }
            }

            if (bHasDamage && bHasAC)
                chDamAC.Text = "Dmg/AC";
            else if (bHasAC)
                chDamAC.Text = "AC";
            else
                chDamAC.Text = "Damage";

            Global.SizeHeadersAndContent(lvCompare, 0, false);

            if (!bAnyDamAC)
                chDamAC.Width = 0;
            if (!bAnyEquip)
                chEquip.Width = 0;

            labelWhichSlot.Text = String.Format("Items in other characters' \"{0}\" slot", MMItem.GetEquipLocationString(m_item.CanEquipLocation));

            SizeToColumns();

            foreach (ListViewItem lvi in lvCompare.Items)
            {
                CompareItemTag tag = lvi.Tag as CompareItemTag;
                if (tag.Compare == MMItem.CompareResult.Better && tag.CharacterIndex != ItemOwner)
                {
                    lvi.Selected = true;
                    break;
                }
            }
        }

        private void SizeToColumns()
        {
            int iWidth = 0;
            foreach (ColumnHeader hdr in lvCompare.Columns)
                iWidth += hdr.Width;

            int iHeight = Global.HeaderRect(lvCompare).Height;
            for (int i = 0; i < lvCompare.Items.Count; i++)
                iHeight += lvCompare.GetItemRect(i).Height;

            int iMarginHoriz = Width - lvCompare.Width;
            int iMarginVert = Height - lvCompare.Height;

            int iNewWidth = iMarginHoriz + iWidth + 10;
            int iNewHeight = iMarginVert + iHeight + 10;

            if (Width < iNewWidth)
                Width = iNewWidth;
            if (Height < iNewHeight)
                Height = iNewHeight;
        }

        public static void Show(string strText, string strCaption)
        {
            ViewInfoForm form = new ViewInfoForm();
            form.ItemText = strText;
            form.Text = strCaption;
            form.ShowDialog();
        }

        private void ViewInfoForm_SizeChanged(object sender, EventArgs e)
        {
            tbInfo.Text = WordWrap(m_strItemText);
        }

        private void miCtxMoveToCharacter_Click(object sender, EventArgs e)
        {
            SetMoveToCharacter();
        }

        private void SetMoveToCharacter(int iChar = -1)
        {
            DialogResult = DialogResult.OK;

            if (iChar >= 0 && iChar < Characters.Count)
                MoveToCharacter = iChar;
            else
            {
                if (lvCompare.SelectedItems.Count < 1)
                    return;

                CompareItemTag compare = (CompareItemTag)lvCompare.SelectedItems[0].Tag;
                iChar = compare.CharacterIndex;
            }
            if (iChar != ItemOwner)
                MoveToCharacter = iChar;
            else
                DialogResult = DialogResult.Cancel;

            Close();
        }

        private void lvCompare_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetMoveToCharacter();
        }

        private void lvCompare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetMoveToCharacter();
        }

        protected override void OnKeyDown(KeyEventArgs e)
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
                    int iMoveTo = e.KeyCode - Keys.D1;
                    if (iMoveTo < Characters.Count)
                    {
                        SetMoveToCharacter(iMoveTo);
                        return;
                    }
                    break;
            }
            base.OnKeyDown(e);
        }
    }

    public class CompareItemTag
    {
        public int CharacterIndex;
        public BaseCharacter Character;
        public Item Item;
        public Item.CompareResult Compare;

        public CompareItemTag(int index, BaseCharacter character, Item item, Item.CompareResult compare = Item.CompareResult.Uncomparable)
        {
            CharacterIndex = index;
            Character = character;
            Item = item;
            Compare = compare;
        }
    }
}
