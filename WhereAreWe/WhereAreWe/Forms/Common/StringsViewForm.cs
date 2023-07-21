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
    public partial class StringsViewForm : CommonKeyForm
    {
        private int m_iSearchStart = 0;
        private string m_text = String.Empty;

        public StringsViewForm()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
            Close();
        }

        public string Strings
        {
            get 
            {
                return tbStrings.Text;
            }

            set
            {
                m_text = value;
                ShowText();
            }
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(ActiveControl);
            return true;
        }

        protected override bool OnCommonKeyClearText()
        {
            ClearSearch();
            return true;
        }

        public void ClearSearch()
        {
            tbSearch.Text = "";
            tbSearch.Select();
            tbSearch.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.F | Keys.Control):
                    tbSearch.Text = "";
                    tbSearch.Focus();
                    return true;
                default:
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private int FindText(string str, int iStart, bool bForward)
        {
            int iFind = -1;
            if (bForward)
                iFind = tbStrings.Text.IndexOf(str, iStart, StringComparison.CurrentCultureIgnoreCase);
            else
                iFind = tbStrings.Text.LastIndexOf(str, iStart, StringComparison.CurrentCultureIgnoreCase);

            if (iFind == -1 && m_iSearchStart > 0)
            {
                if (bForward)
                    iFind = tbStrings.Text.IndexOf(str, 0, StringComparison.CurrentCultureIgnoreCase);
                else
                    iFind = tbStrings.Text.LastIndexOf(str, StringComparison.CurrentCultureIgnoreCase);
            }

            if (iFind > -1)
            {
                tbStrings.SelectionStart = iFind;
                tbStrings.SelectionLength = str.Length;
                tbStrings.ScrollToCaret();
            }
            return iFind;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindNext()
        {
            m_iSearchStart = FindText(tbSearch.Text, m_iSearchStart+1, true);
        }

        private void FindPrev()
        {
            m_iSearchStart = FindText(tbSearch.Text, m_iSearchStart - 1, false);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            m_iSearchStart = FindText(tbSearch.Text, 0, true);
        }

        private void StringsViewForm_Load(object sender, EventArgs e)
        {
            tbSearch.Select();
            tbSearch.Focus();
        }

        private void tbStrings_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.A | Keys.Control):
                    tbStrings.SelectionStart = 0;
                    tbStrings.SelectionLength = tbStrings.Text.Length;
                    break;
                case (Keys.F | Keys.Control):
                case (Keys.OemQuestion):
                    tbSearch.Focus();
                    break;
                case (Keys.F3):
                    FindNext();
                    break;
                case (Keys.F3 | Keys.Shift):
                    FindPrev();
                    break;
                default:
                    break;
            }
        }

        private void tbSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.F3):
                    FindNext();
                    break;
                case (Keys.F3 | Keys.Shift):
                    FindPrev();
                    break;
                default:
                    break;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            FindPrev();
        }

        private void ShowText()
        {
            if (cbLineNumbers.Checked)
            {
                StringBuilder sb = new StringBuilder(m_text.Length);
                int iChar = 0;
                int iLine = 0;
                sb.AppendFormat("{0}: ", iLine++);
                while (iChar < m_text.Length)
                {
                    if (iChar < m_text.Length-2 && m_text.Substring(iChar, 2) == "\r\n")
                    {
                        sb.AppendFormat("\r\n{0}: ", iLine++);
                        iChar++;
                    }
                    else
                        sb.Append(m_text[iChar]);
                    iChar++;
                }
                tbStrings.Text = sb.ToString();
            }
            else
            {
                tbStrings.Text = m_text;
            }

            tbStrings.SelectionStart = 0;
            tbStrings.SelectionLength = 0;
        }

        private void cbLineNumbers_CheckedChanged(object sender, EventArgs e)
        {
            ShowText();
        }
    }
}
