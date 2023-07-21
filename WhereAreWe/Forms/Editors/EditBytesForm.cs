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
    public delegate string ByteDescriptionDelegate(int index);

    public partial class EditBytesForm : CommonKeyForm
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private FindBox m_findBox = null;

        private bool m_bReadOnly = false;
        private bool m_bInitialized = false;
        private byte[] m_bytesOriginal;
        private string m_strTitle = "Change Bytes";
        private bool m_bUpdating = false;
        private Control m_parent = null;
        private ByteDescriptionDelegate m_fnDescription = null;

        public EditBytesForm()
        {
            InitializeComponent();
            CommonKeySelectAll += EditBytesForm_CommonKeySelectAll;
        }

        public EditBytesForm(Control parent)
        {
            InitializeComponent();
            CommonKeySelectAll += EditBytesForm_CommonKeySelectAll;
            m_parent = parent;
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
        }

        void EditBytesForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            if (lvBytes.Focused)
                Global.SelectAll(lvBytes);
        }

        protected override bool OnCommonKeyEscape()
        {
            CloseForm();
            return true;
        }

        public string Title
        {
            get { return Text; }
            set { m_strTitle = value; Text = m_strTitle; }
        }

        public void SetDescriptionFunction(ByteDescriptionDelegate fn)
        {
            m_fnDescription = fn;
        }

        public bool ReadOnly
        {
            get { return m_bReadOnly; }
            set
            {
                m_bReadOnly = value;
                tbNew.ReadOnly = m_bReadOnly;
            }
        }

        public byte[] Bytes
        {
            get
            {
                return Global.BytesFromRelaxedString(tbNew.Text, ForceLength ? m_bytesOriginal.Length : -1);
            }

            set
            {
                m_bytesOriginal = value;
                labelCurrent.Text = Global.ByteString(m_bytesOriginal);
                tbNew.Text = labelCurrent.Text;
                UpdateList(m_bytesOriginal);
            }
        }

        public void UpdateList(byte[] bytes)
        {
            if (!m_bInitialized)
                return;

            if (m_bUpdating)
                return;

            if (splitContainer1.Panel1Collapsed)
                return;

            lvBytes.BeginUpdate();
            if (bytes.Length != lvBytes.Items.Count)
            {
                lvBytes.Items.Clear();
                for (int i = 0; i < bytes.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    SetLVI(lvi, i, bytes[i]);
                    lvBytes.Items.Add(lvi);
                }
            }
            else
            {
                for (int i = 0; i < bytes.Length; i++)
                    SetLVI(lvBytes.Items[i], i, bytes[i]);
            }

            Global.SizeHeadersAndContent(lvBytes);
            lvBytes.EndUpdate();
        }

        private void SetLVI(ListViewItem lvi, int i, byte b)
        {
            if (i != -1)
                lvi.Text = String.Format("{0}", i);

            string strByte = String.Format(miBytesHex.Checked ? "{0:X2}" : "{0}", b);

            if (lvi.SubItems.Count < 2)
                lvi.SubItems.Add(strByte);
            else
                lvi.SubItems[1].Text = strByte;

            // The description shouldn't change while the dialog is open, so don't set it repeatedly
            if (lvi.SubItems.Count < 3)
            {
                string strDesc = m_fnDescription(i);
                lvi.SubItems.Add(m_fnDescription == null ? String.Empty : strDesc);
                lvi.Tag = new ByteItemTag(i, b, strDesc);
            }
            else
                (lvi.Tag as ByteItemTag).Byte = b;      // The byte is the only thing that should change

        }

        public bool ForceLength { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            if (scBytesFind.Panel2Collapsed)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            m_findBox.HideFindBox();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OnOK();
        }

        private void OnOK()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbNew_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbNew, labelInvalid1);
        }

        private bool IsValidChar(char c)
        {
            return Global.IsHexChar(c) || Char.IsWhiteSpace(c) || c == '.';
        }

        private void CheckValid(TextBox tb, Label label)
        {
            bool bValid = true;

            foreach (char c in tb.Text)
            {
                if (!IsValidChar(c))
                {
                    bValid = false;
                    break;
                }
            }

            if (!bValid)
            {
                label.Text = "Not a valid string of bytes";
                label.Visible = true;
                label.ForeColor = Color.Red;
                labelCountText.Text = "?";
                btnOK.Enabled = false;
            }
            else
            {
                byte[] bytes = Global.BytesFromRelaxedString(tbNew.Text, ForceLength ? m_bytesOriginal.Length : -1);
                label.Text = Global.ByteString(bytes);
                label.Visible = true;
                label.ForeColor = Color.Green;
                if (bytes.Length > 9)
                    labelCountText.Text = String.Format("{0} (0x{1:X2})", bytes.Length, bytes.Length);
                else
                    labelCountText.Text = String.Format("{0}", bytes.Length);
                btnOK.Enabled = true;
                UpdateList(bytes);
            }

        }

        private void EditBytesForm_Load(object sender, EventArgs e)
        {
            m_findBox = new FindBox(scBytesFind, tbFind, FindBox.ListViewFindFunction, lvBytes);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;
            scBytesFind.Panel2Collapsed = true;

            if (m_fnDescription == null)
            {
                if (Global.Windows.IsEmpty(WindowType.EditBytesSmall) && m_parent != null)
                {
                    Height = splitContainer1.Panel2.Height + SystemInformation.CaptionHeight + panelSpacing.Height;
                    Global.Windows.SetNormalSize(WindowType.EditBytesSmall, Global.GetCenterRect(this, m_parent.Bounds));
                }
                Global.SetWindowInfo(this, Global.Windows.Get(WindowType.EditBytesSmall));
                splitContainer1.Panel1Collapsed = true;
                tbNew.Focus();
                tbNew.Select();
            }
            else
            {
                if (Global.Windows.IsEmpty(WindowType.EditBytes) && m_parent != null)
                    Global.Windows.SetNormalSize(WindowType.EditBytes, Global.GetCenterRect(this, m_parent.Bounds));

                Global.SetWindowInfo(this, Global.Windows.Get(WindowType.EditBytes));
                splitContainer1.Panel1Collapsed = false;
                lvBytes.Focus();
                lvBytes.Select();
                string strItems = m_fnDescription(-1);
                if (!String.IsNullOrWhiteSpace(strItems))
                {
                    Title = (m_bReadOnly ? "View " : "Edit ") + strItems;
                    if (m_bReadOnly)
                    {
                        splitContainer1.Panel2Collapsed = true;
                        btnOK.Visible = false;
                    }
                }
            }

            Text = m_strTitle;
            tbNew.ReadOnly = m_bReadOnly;
            m_bInitialized = true;

            UpdateList(m_bytesOriginal);
        }

        private void miBytesHex_Click(object sender, EventArgs e)
        {
            miBytesHex.Checked = !miBytesHex.Checked;
            UpdateList(Bytes);
        }

        private void miBytesEdit_Click(object sender, EventArgs e)
        {
            EditSelectedItems();
        }

        private void EditSelectedItems()
        {
            if (m_bReadOnly)
                return;

            if (lvBytes.SelectedItems.Count < 1)
                return;

            AttributeEditForm form = new AttributeEditForm();
            form.Attribute = new EditableAttribute((lvBytes.SelectedItems[0].Tag as ByteItemTag).Byte);
            if (form.ShowDialog() == DialogResult.OK)
            {
                foreach (ListViewItem lvi in lvBytes.SelectedItems)
                    SetLVI(lvi, -1, form.Attribute.Bytes[0]);
                SetBytesFromList();
            }
        }

        private void SetBytesFromList()
        {
            byte[] bytes = new byte[lvBytes.Items.Count];
            for(int i = 0; i < lvBytes.Items.Count; i++)
            {
                ByteItemTag tag = lvBytes.Items[i].Tag as ByteItemTag;
                bytes[tag.Index] = tag.Byte;
            }
            m_bUpdating = true;
            Bytes = bytes;
            m_bUpdating = false;
        }

        private void cmBytes_Opening(object sender, CancelEventArgs e)
        {
            bool bSel = lvBytes.SelectedItems.Count > 0;
            miBytesEdit.Enabled = !m_bReadOnly && bSel;
            miBytesSet0.Enabled = !m_bReadOnly && bSel;
            miBytesSet1.Enabled = !m_bReadOnly && bSel;
            miBytesSet255.Enabled = !m_bReadOnly && bSel;
            miBytesEdit.Visible = !m_bReadOnly;
            miBytesSet0.Visible = !m_bReadOnly;
            miBytesSet1.Visible = !m_bReadOnly;
            miBytesSet255.Visible = !m_bReadOnly;
        }

        private void lvBytes_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedItems();
        }

        private void SetSelected(byte b)
        {
            foreach (ListViewItem lvi in lvBytes.SelectedItems)
                SetLVI(lvi, -1, b);
            SetBytesFromList();
        }

        private void miBytesSet0_Click(object sender, EventArgs e)
        {
            SetSelected(0);
        }

        private void miBytesSet1_Click(object sender, EventArgs e)
        {
            SetSelected(1);
        }

        private void miBytesSet255_Click(object sender, EventArgs e)
        {
            SetSelected(255);
        }

        private void EditBytesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
                Global.Windows.Set(WindowType.EditBytesSmall, this);
            else
                Global.Windows.Set(WindowType.EditBytes, this);
        }

        private void lvBytes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvBytes.ListViewItemSorter = new EditByteItemComparer(lvBytes, e.Column, m_bAscendingSort);
            lvBytes.Sort();
        }
    }

    class ByteItemTag
    {
        public int Index;
        public byte Byte;
        public string Description;

        public ByteItemTag(int index, byte b, string desc)
        {
            Index = index;
            Byte = b;
            Description = desc;
        }
    }

    class EditByteItemComparer : BasicListViewComparer
    {
        public EditByteItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            if (!(x is ListViewItem) || !(y is ListViewItem))
                return 0;

            ByteItemTag item1 = (ByteItemTag)((ListViewItem) x).Tag;
            ByteItemTag item2 = (ByteItemTag)((ListViewItem) y).Tag;

            switch (m_column)
            {
                case 0: return Order(Math.Sign(item1.Index - item2.Index));
                case 1: return Order(Math.Sign(item1.Byte - item2.Byte));
                case 2: return Order(String.Compare(item1.Description, item2.Description, true));
                default: return base.Compare(x, y);
            }
        }
    }


}
