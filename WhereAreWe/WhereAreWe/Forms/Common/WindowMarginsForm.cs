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
    public partial class WindowMarginsForm : Form
    {
        private bool m_bUpdating = false;

        public WindowMarginsForm()
        {
            InitializeComponent();
        }

        public ExpandSizes Margins
        {
            get
            {
                return new ExpandSizes((int)nudTop.Value, (int)nudBottom.Value, (int)nudLeft.Value, (int)nudRight.Value);
            }

            set
            {
                m_bUpdating = true;
                Global.SetNud(nudTop, value.Top);
                Global.SetNud(nudBottom, value.Bottom);
                Global.SetNud(nudLeft, value.Left);
                Global.SetNud(nudRight, value.Right);
                cbAllMarginsSame.Checked = value.AllEqual;
                m_bUpdating = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseExtendedWindowRect = cbUseExtendedWindowRect.Checked;
            Close();
        }

        private void cbAllMarginsSame_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            CheckForce();
        }

        private void CheckForce()
        {
            if (cbAllMarginsSame.Checked)
            {
                nudLeft.Value = nudTop.Value;
                nudRight.Value = nudTop.Value;
                nudBottom.Value = nudTop.Value;
            }
        }

        private void nudTop_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (cbAllMarginsSame.Checked)
                SetAllValues(nudTop.Value);
        }

        private void nudLeft_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (cbAllMarginsSame.Checked)
                SetAllValues(nudLeft.Value);
        }

        private void nudRight_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (cbAllMarginsSame.Checked)
                SetAllValues(nudRight.Value);
        }

        private void nudBottom_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (cbAllMarginsSame.Checked)
                SetAllValues(nudBottom.Value);
        }

        private void SetAllValues(decimal val)
        {
            m_bUpdating = true;
            Global.SetNud(nudTop, (int)val);
            Global.SetNud(nudLeft, (int)val);
            Global.SetNud(nudRight, (int)val);
            Global.SetNud(nudBottom, (int)val);
            m_bUpdating = false;
        }

        private void WindowMarginsForm_Load(object sender, EventArgs e)
        {
            cbUseExtendedWindowRect.Checked = Properties.Settings.Default.UseExtendedWindowRect;
        }
    }
}
