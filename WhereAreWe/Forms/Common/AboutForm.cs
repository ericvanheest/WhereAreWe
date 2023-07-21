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
    public partial class AboutForm : CommonKeyForm
    {
        public AboutForm()
        {
            InitializeComponent();
            CommonKeySelectAll += AboutForm_CommonKeySelectAll;
        }

        void AboutForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            tbAddress.SelectAll();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            pbIcon.Image = new Icon(Properties.Resources.iconWhereAreWe, 48, 48).ToBitmap();
            labelVersion.Text = String.Format("Verson {0}", Global.Version);
        }
    }
}
