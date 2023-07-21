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
    public partial class TreasurePanel : HackerBasedUserControl
    {
        public TrapsControl m_traps = null;
        public Size m_margin = new Size(0,0);

        public TreasurePanel()
        {
            InitializeComponent();
            m_margin = new Size(gbTreasure.Width - labelContents.Width, gbTreasure.Height - labelContents.Height);
        }

        public void SetTreasure(SearchResults treasure)
        {
            gbTreasure.Text = treasure.ContainerString;
            labelContents.Text = treasure.ContentsString;
            labelTreasureHeader.Text = treasure.HeaderString;

            if (Hacker != null)
            {
                if (m_traps == null)
                    m_traps = Hacker.GetTrapsControl(m_main);
                if (treasure.HasTraps && m_traps != null)
                {
                    splitContainer1.Panel2.Controls.Add(m_traps);
                    m_traps.Dock = DockStyle.Fill;
                    splitContainer1.Panel2Collapsed = false;
                    m_traps.SetSearchResults(treasure);
                }
                else
                {
                    splitContainer1.Panel2Collapsed = true;
                }
                timerUpdateSplitter.Start();
            }
        }

        public bool DisarmTrap()
        {
            if (m_traps == null)
                return false;

            return m_traps.DisarmTrap();
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            SetSplitterDistance();
        }

        private void SetSplitterDistance()
        {
            if (!splitContainer1.Panel2Collapsed)
                splitContainer1.SplitterDistance = gbTreasure.Bottom + 5 + (splitContainer1.Panel1.HorizontalScroll.Visible ? SystemInformation.HorizontalScrollBarHeight : 0);
        }

        private void labelContents_SizeChanged(object sender, EventArgs e)
        {
            gbTreasure.Width = Math.Max(labelContents.Width + m_margin.Width, 150);
            gbTreasure.Height = labelContents.Height + m_margin.Height;
        }

        private void timerUpdateSplitter_Tick(object sender, EventArgs e)
        {
            timerUpdateSplitter.Stop();
            SetSplitterDistance();
        }
    }
}
