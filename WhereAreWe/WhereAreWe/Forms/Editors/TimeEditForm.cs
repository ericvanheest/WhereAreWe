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
    public partial class TimeEditForm : Form
    {
        public enum InitialFocusControl
        {
            Year,
            Day,
            Minutes
        }

        private DateTime m_dtZero = new DateTime(2000, 1, 1, 0, 0, 0);
        public InitialFocusControl InitialFocus = InitialFocusControl.Year;

        public TimeEditForm()
        {
            InitializeComponent();
        }

        public UInt16 Year
        {
            get { return (UInt16)nudYear.Value; }
            set { nudYear.Value = value; UpdateUI(); }
        }

        public Byte Day
        {
            get { return (Byte)nudDay.Value; }
            set { nudDay.Value = value; UpdateUI(); }
        }

        public UInt16 Minutes
        {
            get { return (UInt16) TimeOnly(dtpTime.Value).TotalMinutes; }
            set { dtpTime.Value = m_dtZero.AddMinutes(value); UpdateUI(); }
        }

        private TimeSpan TimeOnly(DateTime dt)
        {
            return new TimeSpan(dt.Hour, dt.Minute, 0);
        }

        private void UpdateUI()
        {
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

        private void TimeEditForm_Load(object sender, EventArgs e)
        {
            switch (InitialFocus)
            {
                case InitialFocusControl.Year:
                    nudYear.Focus();
                    nudYear.Select();
                    break;
                case InitialFocusControl.Day:
                    nudDay.Focus();
                    nudDay.Select();
                    break;
                case InitialFocusControl.Minutes:
                    dtpTime.Focus();
                    dtpTime.Select();
                    break;
                default:
                    break;
            }
        }
    }
}
