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
    public partial class SheetPathEditForm : Form
    {
        private string m_strPath;

        public SheetPathEditForm()
        {
            InitializeComponent();
        }

        public string Path
        {
            get { return m_strPath; }
            set { m_strPath = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_strPath = tbPath.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SheetPathEditForm_Load(object sender, EventArgs e)
        {
            tbPath.Text = m_strPath;
        }
    }
}
