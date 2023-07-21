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
    public partial class ExitsControl
        : UserControl
    {
        private MainForm m_mainForm = null;
        private MemoryHacker m_hacker = null;
        private byte[] m_lastAttributes = null;
        private bool m_bUpdatingSize = false;

        public ExitsControl()
        {
            InitializeComponent();
        }

        public ExitsControl(MainForm form, MemoryHacker hacker)
        {
            InitializeComponent();
            SetHacker(form, hacker);
        }

        public void SetHacker(MainForm form, MemoryHacker hacker)
        {
            m_mainForm = form;
            m_hacker = hacker;
        }

        public void UpdateUI(MapAttributes attributes)
        {
            if (!Global.Compare(attributes.Bytes, m_lastAttributes))
            {
                m_lastAttributes = attributes.Bytes;
                StringBuilder sb = new StringBuilder();

                lvLocations.BeginUpdate();

                lvLocations.Items.Clear();
                foreach (MMExit exit in attributes.Exits)
                    AddLocation(exit);

                Global.SizeHeadersAndContent(lvLocations);

                lvLocations.EndUpdate();
            }
        }

        private void AddLocation(MMExit exit)
        {
            ListViewItem lvi = new ListViewItem(exit.DirectionString);
            if (exit.Point != Global.NullPoint && exit.Map != -1)
                lvi.SubItems.Add(String.Format("{0} ({1},{2})", m_mainForm.GetMapName(exit.Map), exit.Point.X, exit.Point.Y));
            else if (exit.Map != -1)
                lvi.SubItems.Add(m_mainForm.GetMapName(exit.Map));
            else
                lvi.SubItems.Add(String.Format("{0},{1}", exit.Point.X, exit.Point.Y));
            lvi.Tag = exit;
            lvLocations.Items.Add(lvi);
        }

        private void miExitEdit_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.EnableCheats)
                return;

            if (lvLocations.FocusedItem == null)
                return;

            MapSelectionForm form = new MapSelectionForm();
            form.SetMain(m_mainForm, WindowType.MapSelection);
            form.Exit = lvLocations.FocusedItem.Tag as MMExit;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_hacker.SetExit(form.Exit);
            }
        }

        private void cmExit_Opening(object sender, CancelEventArgs e)
        {
            if (!Properties.Settings.Default.EnableCheats)
            {
                e.Cancel = true;
                return;
            }
        }

        private void lvLocations_SizeChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingSize)
                return;

            m_bUpdatingSize = true;
            lvLocations.BeginUpdate();
            Global.SizeHeadersAndContent(lvLocations);
            lvLocations.EndUpdate();
            m_bUpdatingSize = false;
        }
    }
}
