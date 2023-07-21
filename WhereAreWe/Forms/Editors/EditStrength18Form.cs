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
    public partial class EditStrength18Form : Form
    {
        private StrengthWith18[] m_attr;
        private long m_maxValue = 255;
        private long m_minValue = 0;

        public EditStrength18Form()
        {
            InitializeComponent();
        }

        public StrengthWith18[] Attributes
        {
            get { return m_attr; }
            set { m_attr = value; UpdateUI(); }
        }

        public void UpdateFromUI()
        {
            StrengthWith18 perm = StrengthWith18.FromString(tbPermanent.Text);
            StrengthWith18 temp = StrengthWith18.FromString(tbTemporary.Text);
            if (temp == null)
                m_attr = new StrengthWith18[] { perm };
            else
                m_attr = new StrengthWith18[] { perm, temp };
        }

        public void UpdateUI()
        {
            if (m_attr == null)
                m_attr = new StrengthWith18[] { new StrengthWith18(new OneByteStat(18), OneByteStat.Zero) };
            labelCurrent.Text = m_attr[0].ToString();
            tbTemporary.Visible = m_attr.Length > 1;
            labelTemporary.Visible = m_attr.Length > 1;
            labelInvalid1.Visible = false;
            labelInvalid2.Visible = false;
            tbPermanent.Text = m_attr[0].ToString();
            if (m_attr.Length > 1)
                tbTemporary.Text = m_attr[1].ToString();
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

        private void tbTemporary_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbTemporary, labelInvalid1);
        }

        private void CheckValid(TextBox tb, Label label)
        {
            StrengthWith18 s18 = StrengthWith18.FromString(tb.Text);
            if (s18 == null)
            {
                label.Text = "Not a valid number";
                label.Visible = true;
            }
            else
            {
                if (s18.Strength != 18 && s18.Strength18 != 0)
                {
                    label.Text = "/xx only valid if Strength is 18";
                    label.Visible = true;
                }
                else if (s18.Strength < m_minValue || s18.Strength > m_maxValue)
                {
                    label.Text = String.Format("Strength must be between {0} and {1}", m_minValue, m_maxValue);
                    label.Visible = true;
                }
                else if (s18.Strength == 18 && (s18.Strength18 < 0 || s18.Strength18 > 100))
                {
                    label.Text = String.Format("/xx must be between 00 and 99");
                    label.Visible = true;
                }
                else
                    label.Visible = false;
            }

            btnOK.Enabled = !label.Visible;
        }

        private void tbPermanent_TextChanged(object sender, EventArgs e)
        {
            CheckValid(tbPermanent, labelInvalid2);
        }
    }

    public class StrengthWith18
    {
        public int Strength;
        public int Strength18;
        public int StrengthPerm;
        public int Strength18Perm;

        public StrengthWith18(OneByteStat str, OneByteStat str18)
        {
            Strength = str.Temporary;
            StrengthPerm = str.Permanent;
            Strength18 = Strength == 18 ? str18.Temporary : 0;
            Strength18Perm = StrengthPerm == 18 ? str18.Permanent : 0;
        }

        public StrengthWith18(int iStrength, int iStrength18)
        {
            Strength = iStrength;
            StrengthPerm = iStrength;
            Strength18 = Strength == 18 ? iStrength18 : 0;
            Strength18Perm = Strength18;
        }

        public override string ToString()
        {
            if (Strength != 18 || Strength18 == 0)
            {
                if (StrengthPerm == Strength)
                    return Strength.ToString();
                return String.Format("{0}{1}", StrengthPerm, Global.AddPlus(Strength - StrengthPerm));
            }
            return String.Format("{0}/{1:D2}", Strength, Strength18 > 99 ? 0 : Strength18);
        }

        public static StrengthWith18 FromString(string str)
        {
            int iSlash = str.IndexOf('/');
            int iStrength = 0;
            if (iSlash == -1)
            {
                if (Int32.TryParse(str, out iStrength))
                    return new StrengthWith18(new OneByteStat(iStrength), OneByteStat.Zero);
                return null;
            }
            if (!Int32.TryParse(str.Substring(0, iSlash), out iStrength))
                return null;
            if (iSlash + 1 >= str.Length)
                return null;
            if (!Int32.TryParse(str.Substring(iSlash + 1), out int iStrength18))
                return null;
            return new StrengthWith18(new OneByteStat(iStrength), new OneByteStat(iStrength18 == 0 ? 100 : iStrength18));
        }
    }
}
