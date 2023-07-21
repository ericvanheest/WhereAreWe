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
    public partial class AttributeEditForm : Form
    {
        private EditableAttribute m_attr;
        private long m_maxValue = 0;
        private long m_minValue = 0;

        public AttributeEditForm()
        {
            InitializeComponent();
        }

        public EditableAttribute Attribute
        {
            get { return m_attr; }
            set { m_attr = value; UpdateUI(); }
        }

        public void SetBytesFromUI(byte[] values)
        {
            int i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
                values[0] = (byte) i;
            if (values.Length > 1 && Int32.TryParse(tbVal2.Text, out i))
                values[1] = (byte)i;
            if (values.Length > 2 && Int32.TryParse(tbVal3.Text, out i))
                values[2] = (byte)i;
        }

        public void SetUShortsFromUI(UInt16[] values)
        {
            int i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
                values[0] = (UInt16)i;
            if (values.Length > 1 && Int32.TryParse(tbVal2.Text, out i))
                values[1] = (UInt16)i;
            if (values.Length > 2 && Int32.TryParse(tbVal3.Text, out i))
                values[2] = (UInt16)i;
        }

        public void SetShortsFromUI(Int16[] values)
        {
            int i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
                values[0] = (Int16)i;
            if (values.Length > 1 && Int32.TryParse(tbVal2.Text, out i))
                values[1] = (Int16)i;
            if (values.Length > 2 && Int32.TryParse(tbVal3.Text, out i))
                values[2] = (Int16)i;
        }

        public void SetIntsFromUI(Int32[] values)
        {
            Int32 i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
                values[0] = i;
            if (values.Length > 1 && Int32.TryParse(tbVal2.Text, out i))
                values[1] = i;
            if (values.Length > 2 && Int32.TryParse(tbVal3.Text, out i))
                values[2] = i;
        }

        public void SetLongsFromUI(long[] values)
        {
            long i;
            if (values.Length > 0 && Int64.TryParse(tbVal1.Text, out i))
                values[0] = i;
            if (values.Length > 1 && Int64.TryParse(tbVal2.Text, out i))
                values[1] = i;
            if (values.Length > 2 && Int64.TryParse(tbVal3.Text, out i))
                values[2] = i;
        }

        public void SetUIntsFromUI(UInt32[] values)
        {
            UInt32 i;
            if (values.Length > 0 && UInt32.TryParse(tbVal1.Text, out i))
                values[0] = i;
            if (values.Length > 1 && UInt32.TryParse(tbVal2.Text, out i))
                values[1] = i;
            if (values.Length > 2 && UInt32.TryParse(tbVal3.Text, out i))
                values[2] = i;
        }

        public void SetInt24sFromUI(UInt24[] values)
        {
            int i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
                values[0].Value = i;
            if (values.Length > 1 && Int32.TryParse(tbVal2.Text, out i))
                values[1].Value = i;
            if (values.Length > 2 && Int32.TryParse(tbVal3.Text, out i))
                values[2].Value = i;
        }

        public void SetStringsFromUI(string[] values)
        {
            values[0] = tbVal1.Text;
            values[1] = tbVal2.Text;
            values[2] = tbVal3.Text;
        }

        public void SetItemsFromUI(Item[] values)
        {
            int i;
            if (values.Length > 0 && Int32.TryParse(tbVal1.Text, out i))
            {
                values[0].Index = i;
                if (Int32.TryParse(tbVal2.Text, out i))
                    values[0].ChargesCurrent = (byte) i;
                if (Int32.TryParse(tbVal2.Text, out i) && values[0] is MM2Item)
                    ((MM2Item) values[0]).BonusCurrent = (MM2BonusFlags) i;
            }
        }

        public void UpdateUI(Array values, long max)
        {
            UpdateUI(values, max, 0);
        }

        public void UpdateUI(Array values, long max, long min)
        {
            m_maxValue = max;
            m_minValue = min;
            StringBuilder sb = new StringBuilder();

            int iVisible = values.Length;
            long lVal1 = Convert.ToInt64(values.GetValue(0));
            long lVal2 = values.Length > 1 ? Convert.ToInt64(values.GetValue(1)) : 0;
            long lVal3 = values.Length > 2 ? Convert.ToInt64(values.GetValue(2)) : 0;
            if (min == sbyte.MinValue && max == sbyte.MaxValue && lVal1 > max)
                lVal1 = (sbyte)lVal1;
            if (min == sbyte.MinValue && max == sbyte.MaxValue && lVal2 > max)
                lVal2 = (sbyte)lVal3;
            if (min == sbyte.MinValue && max == sbyte.MaxValue && lVal3 > max)
                lVal3 = (sbyte)lVal3;
            sb.AppendFormat("{0}", lVal1);
            tbVal1.Text = lVal1.ToString();
            if (values.Length > 1)
            {
                sb.AppendFormat("/{0}", lVal2);
                tbVal2.Text = lVal2.ToString();
            }
            if (values.Length > 2)
            {
                sb.AppendFormat("/{0}", lVal3);
                tbVal3.Text = lVal3.ToString();
            }
            labelCurrent.Text = sb.ToString();
            labelNewValue2.Visible = (iVisible > 1);
            labelNewValue3.Visible = (iVisible > 2);
            tbVal2.Visible = (iVisible > 1);
            tbVal3.Visible = (iVisible > 2);
        }

        public void UpdateUI()
        {
            labelNewValue2.Visible = false;
            labelNewValue3.Visible = false;
            tbVal2.Visible = false;
            tbVal3.Visible = false;
            labelInvalid1.Visible = false;
            labelInvalid2.Visible = false;
            labelInvalid3.Visible = false;

            bool bDefault = (m_attr.Minimum == m_attr.Maximum);

            if (m_attr.Bytes != null && m_attr.Bytes.Length > 0)
                UpdateUI(m_attr.Bytes, bDefault ? Byte.MaxValue : m_attr.Maximum, bDefault ? 0 : m_attr.Minimum);
            else if (m_attr.Shorts != null && m_attr.Shorts.Length > 0)
                UpdateUI(m_attr.Shorts, bDefault ? Int16.MaxValue : m_attr.Maximum, bDefault ? Int16.MinValue : m_attr.Minimum);
            else if (m_attr.UShorts != null && m_attr.UShorts.Length > 0)
                UpdateUI(m_attr.UShorts, bDefault ? UInt16.MaxValue : m_attr.Maximum, bDefault ? UInt16.MinValue : m_attr.Minimum);
            else if (m_attr.Ints != null && m_attr.Ints.Length > 0)
                UpdateUI(m_attr.Ints, bDefault ? Int32.MaxValue : m_attr.Maximum, bDefault ? Int32.MinValue : m_attr.Minimum);
            else if (m_attr.Longs != null && m_attr.Longs.Length > 0)
                UpdateUI(m_attr.Longs, bDefault ? Int64.MaxValue : m_attr.Maximum, bDefault ? Int64.MinValue : m_attr.Minimum);
            else if (m_attr.UInts != null && m_attr.UInts.Length > 0)
                UpdateUI(m_attr.UInts, bDefault ? UInt32.MaxValue : m_attr.Maximum, bDefault ? UInt32.MinValue : m_attr.Minimum);
            else if (m_attr.Int24s != null && m_attr.Int24s.Length > 0)
                UpdateUI(m_attr.Int24s, bDefault ? 0xffffff : m_attr.Maximum, bDefault ? 0 : m_attr.Minimum);
            else if (m_attr.Strings != null && m_attr.Strings.Length > 0)
                UpdateUI(m_attr.Strings, 0);
            else if (m_attr.Items != null && m_attr.Items.Length > 0)
                UpdateUI(m_attr.Items, 0xff);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_attr.Bytes != null && m_attr.Bytes.Length > 0)
                SetBytesFromUI(m_attr.Bytes);
            else if (m_attr.UShorts != null && m_attr.UShorts.Length > 0)
                SetUShortsFromUI(m_attr.UShorts);
            else if (m_attr.Shorts != null && m_attr.Shorts.Length > 0)
                SetShortsFromUI(m_attr.Shorts);
            else if (m_attr.Ints != null && m_attr.Ints.Length > 0)
                SetIntsFromUI(m_attr.Ints);
            else if (m_attr.Longs != null && m_attr.Longs.Length > 0)
                SetLongsFromUI(m_attr.Longs);
            else if (m_attr.UInts != null && m_attr.UInts.Length > 0)
                SetUIntsFromUI(m_attr.UInts);
            else if (m_attr.UInts != null && m_attr.UInts.Length > 0)
                SetUIntsFromUI(m_attr.UInts);
            else if (m_attr.Int24s != null && m_attr.Int24s.Length > 0)
                SetInt24sFromUI(m_attr.Int24s);
            else if (m_attr.Strings != null && m_attr.Strings.Length > 0)
                SetStringsFromUI(m_attr.Strings);
            else if (m_attr.Items != null && m_attr.Items.Length > 0)
                SetItemsFromUI(m_attr.Items);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbVal1_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbVal1, labelInvalid1);
        }

        private void CheckValid(TextBox tb, Label label)
        {
            long i;
            if (long.TryParse(tb.Text, out i))
            {
                if (i < m_minValue || i > m_maxValue)
                {
                    label.Text = String.Format("Must be between {0} and {1}", m_minValue, m_maxValue);
                    label.Visible = true;
                }
                else
                    label.Visible = false;
            }
            else
            {
                label.Text = "Not a valid number";
                label.Visible = true;
            }

            btnOK.Enabled = !label.Visible;
        }

        private void tbVal3_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbVal3, labelInvalid3);
        }

        private void tbVal2_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbVal2, labelInvalid2);
        }
    }
}
