using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WhereAreWe
{
    public partial class PanelIgnoreWheel : Panel
    {
        public PanelIgnoreWheel()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL && this.Parent != null && NativeMethods.IsControlDown())
            {
                NativeMethods.PostMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
            }
            else base.WndProc(ref m);
        }

    }
}
