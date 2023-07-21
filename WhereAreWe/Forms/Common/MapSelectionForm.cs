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
    public partial class MapSelectionForm : HackerBasedForm
    {
        private MMExit m_exit;

        public MapSelectionForm()
        {
            InitializeComponent();
        }

        protected override bool OnCommonKeyPrevious()
        {
            FindPrevious();
            return true;
        }

        protected override bool OnCommonKeyNext(bool bIncludeCurrent)
        {
            FindNext();
            return true;
        }

        public MMExit Exit
        {
            get
            {
                UpdateFromUI();
                return m_exit;
            }

            set
            {
                m_exit = value;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            comboMap.Items.Clear();

            if (m_exit.Point == Global.NullPoint)
            {
                nudX.Enabled = false;
                nudY.Enabled = false;
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                nudX.Enabled = true;
                nudY.Enabled = true;
                nudX.Value = m_exit.Point.X;
                nudY.Value = m_exit.Point.Y;
                splitContainer1.Panel2Collapsed = false;
            }

            List<MapTitleInfo> maps = Hacker.GetMapTitles();
            foreach (MapTitleInfo pair in maps)
                comboMap.Items.Add(pair);

            if (m_exit.Map != -1)
            {
                comboMap.Enabled = true;
                for (int i = 0; i < comboMap.Items.Count; i++)
                {
                    if (((MapTitleInfo)comboMap.Items[i]).Map == m_exit.Map)
                    {
                        comboMap.SelectedIndex = i;
                        break;
                    }
                }
                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                comboMap.Enabled = false;
                splitContainer1.Panel1Collapsed = true;
            }
        }

        public void UpdateFromUI()
        {
            if (comboMap.Enabled && comboMap.SelectedItem != null)
                m_exit.Map = (comboMap.SelectedItem as MapTitleInfo).Map;
            if (nudX.Enabled)
                m_exit.Point = new Point((int)nudX.Value, (int)nudY.Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbFindItem_TextChanged(object sender, EventArgs e)
        {
            int iIndex = 0;
            while(iIndex < comboMap.Items.Count)
            {
                if (comboMap.Items[iIndex].ToString().ToLower().Contains(tbFindMap.Text.ToLower()))
                {
                    comboMap.SelectedIndex = iIndex;
                    return;
                }
                iIndex++;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindPrevious()
        {
            Global.FindNext(comboMap, tbFindMap.Text, false);
        }

        private void FindNext()
        {
            Global.FindNext(comboMap, tbFindMap.Text);
        }

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel1Collapsed)
            {
                tbFindMap.Focus();
                tbFindMap.Select();
            }
            else
            {
                nudX.Select();
                nudX.Focus();
            }
        }
    }
}
