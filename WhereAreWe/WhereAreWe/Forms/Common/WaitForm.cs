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
    public partial class WaitForm : Form
    {
        public event EventHandler OnAbort;

        public bool ShowAbort
        {
            get { return btnAbort.Visible; }

            set { btnAbort.Visible = value; }
        }

        public WaitForm()
        {
            InitializeComponent();
        }

        public void SetWaitText(string str)
        {
            labelWaitText.Text = str;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (OnAbort != null)
                OnAbort(this, new EventArgs());
        }

        public void SetAbort(EventHandler abortFunction)
        {
            if (OnAbort != null)
                foreach (Delegate d in OnAbort.GetInvocationList())
                    OnAbort -= (EventHandler)d;

            if (abortFunction == null)
            {
                ShowAbort = false;
                return;
            }
            OnAbort += abortFunction;
            ShowAbort = true;
        }
    }
}
