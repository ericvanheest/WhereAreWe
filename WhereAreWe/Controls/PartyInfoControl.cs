using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class PartyInfoControl : UserControl
    {
        private MemoryHacker m_hacker = null;

        public PartyInfoControl()
        {
            InitializeComponent();
        }

        public void SetHacker(MemoryHacker hacker)
        {
            m_hacker = hacker;
        }

        public virtual void SetInfo(PartyInfo info)
        {
        }
    }
}
