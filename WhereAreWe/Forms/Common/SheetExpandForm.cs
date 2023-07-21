using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class SheetExpandForm : Form
    {
        public SheetExpandForm()
        {
            InitializeComponent();
        }

        public ExpandSizes Sizes
        {
            get
            {
                return new ExpandSizes((int)nudTop.Value, (int)nudBottom.Value, (int)nudLeft.Value, (int)nudRight.Value);
            }

            set
            {
                nudTop.Value = value.Top;
                nudBottom.Value = value.Bottom;
                nudLeft.Value = value.Left;
                nudRight.Value = value.Right;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
