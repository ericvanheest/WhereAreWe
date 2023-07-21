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
    public partial class SheetSelectorForm : HackerBasedForm
    {
        public SheetSelectorForm()
        {
            InitializeComponent();
            tbSearch.Focus();
        }

        public const int NormalHeight = 45;

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_main.SelectSheetByPartialName(tbSearch.Text, true, false);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            m_main.SelectSheetByPartialName(tbSearch.Text, false, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool OnCommonKeyPrevious()
        {
            m_main.SelectSheetByPartialName(tbSearch.Text, true, true);
            return true;
        }

        protected override bool OnCommonKeyNext(bool bIncludeCurrent)
        {
            m_main.SelectSheetByPartialName(tbSearch.Text, true, false);
            return true;
        }

        protected override bool OnCommonKeyClearText()
        {
            tbSearch.Text = "";
            return true;
        }
    }
}
