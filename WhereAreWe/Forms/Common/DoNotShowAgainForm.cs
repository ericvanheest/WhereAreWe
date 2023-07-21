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
    public partial class DoNotShowAgainForm : Form
    {
        public DoNotShowAgainForm()
        {
            InitializeComponent();
        }

        public DoNotShowAgainForm(string strTitle, string strMessage, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            InitializeComponent();
            Text = strTitle;
            labelText.Text = strMessage;
            if (buttons == MessageBoxButtons.OK)
                btnOK.Location = btnCancel.Location;

            Size sz = TextRenderer.MeasureText(strMessage, labelText.Font, labelText.Size);
            Height = Height - labelText.Height + sz.Height;
        }

        public bool DoNotShowAgain 
        { 
            get { return cbDoNotShowAgain.Checked; } 
            set { cbDoNotShowAgain.Checked = value; } 
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
    }
}
