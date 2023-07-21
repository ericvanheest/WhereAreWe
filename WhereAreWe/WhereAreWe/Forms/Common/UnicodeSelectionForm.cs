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
    public partial class UnicodeSelectionForm : Form
    {
        private string m_strSelection;

        public UnicodeSelectionForm()
        {
            InitializeComponent();
        }

        public string SelectedSymbol
        {
            get { return m_strSelection; }
        }

        private void tbUnicode_Click(object sender, EventArgs e)
        {
            Point pt = tbUnicode.PointToClient(Cursor.Position);
            int iIndex = tbUnicode.GetCharIndexFromPosition(pt);
            if (iIndex >= 0 && iIndex < tbUnicode.Text.Length)
            {
                if (tbUnicode.Text[iIndex] == ' ')
                    iIndex--;
                if (iIndex < 0)
                    iIndex = 0;
                m_strSelection = new String(tbUnicode.Text[iIndex], 1);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UnicodeSelectionForm_Load(object sender, EventArgs e)
        {
            tbUnicode.SelectionStart = 0;
            tbUnicode.SelectionLength = 0;
        }

        private void tbUnicode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
                default:
                    break;
            }
        }
    }
}
