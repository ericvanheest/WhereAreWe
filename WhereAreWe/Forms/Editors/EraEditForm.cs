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
    public partial class EraEditForm : Form
    {
        MM2TimeInfo m_timeInfo = null;

        public EraEditForm()
        {
            InitializeComponent();
        }

        public MM2TimeInfo Time
        {
            get { return m_timeInfo; }
            set { m_timeInfo = value; UpdateUI(); }
        }

        public void UpdateUI()
        {
            if (m_timeInfo.CurrentEra < 1 || m_timeInfo.CurrentEra > 9)
                m_timeInfo.CurrentEra = 9;
            comboCurrentEra.SelectedIndex = m_timeInfo.CurrentEra - 1;

            nudEra1Day.Value = m_timeInfo.EraDays[0];
            nudEra2Day.Value = m_timeInfo.EraDays[1];
            nudEra3Day.Value = m_timeInfo.EraDays[2];
            nudEra4Day.Value = m_timeInfo.EraDays[3];
            nudEra5Day.Value = m_timeInfo.EraDays[4];
            nudEra6Day.Value = m_timeInfo.EraDays[0];
            nudEra7Day.Value = m_timeInfo.EraDays[6];
            nudEra8Day.Value = m_timeInfo.EraDays[7];
            nudEra9Day.Value = m_timeInfo.EraDays[8];

            nudEra1Year.Value = m_timeInfo.EraYears[0];
            nudEra2Year.Value = m_timeInfo.EraYears[1];
            nudEra3Year.Value = m_timeInfo.EraYears[2];
            nudEra4Year.Value = m_timeInfo.EraYears[3];
            nudEra5Year.Value = m_timeInfo.EraYears[4];
            nudEra6Year.Value = m_timeInfo.EraYears[0];
            nudEra7Year.Value = m_timeInfo.EraYears[6];
            nudEra8Year.Value = m_timeInfo.EraYears[7];
            nudEra9Year.Value = m_timeInfo.EraYears[8];

            nudSteps.Value = m_timeInfo.Steps;
        }

        public void UpdateFromUI()
        {
            m_timeInfo.CurrentEra = (byte) (comboCurrentEra.SelectedIndex + 1);

            m_timeInfo.EraDays[0] = (ushort) nudEra1Day.Value;
            m_timeInfo.EraDays[1] = (ushort) nudEra2Day.Value;
            m_timeInfo.EraDays[2] = (ushort) nudEra3Day.Value;
            m_timeInfo.EraDays[3] = (ushort) nudEra4Day.Value;
            m_timeInfo.EraDays[4] = (ushort) nudEra5Day.Value;
            m_timeInfo.EraDays[0] = (ushort) nudEra6Day.Value;
            m_timeInfo.EraDays[6] = (ushort) nudEra7Day.Value;
            m_timeInfo.EraDays[7] = (ushort) nudEra8Day.Value;
            m_timeInfo.EraDays[8] = (ushort) nudEra9Day.Value;

            m_timeInfo.EraYears[0] = (ushort) nudEra1Year.Value;
            m_timeInfo.EraYears[1] = (ushort) nudEra2Year.Value;
            m_timeInfo.EraYears[2] = (ushort) nudEra3Year.Value;
            m_timeInfo.EraYears[3] = (ushort) nudEra4Year.Value;
            m_timeInfo.EraYears[4] = (ushort) nudEra5Year.Value;
            m_timeInfo.EraYears[0] = (ushort) nudEra6Year.Value;
            m_timeInfo.EraYears[6] = (ushort) nudEra7Year.Value;
            m_timeInfo.EraYears[7] = (ushort) nudEra8Year.Value;
            m_timeInfo.EraYears[8] = (ushort) nudEra9Year.Value;

            m_timeInfo.Steps = (byte) nudSteps.Value;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            comboCurrentEra.SelectedIndex = 8;

            nudEra1Day.Value = 1;
            nudEra2Day.Value = 1;
            nudEra3Day.Value = 1;
            nudEra4Day.Value = 1;
            nudEra5Day.Value = 1;
            nudEra6Day.Value = 1;
            nudEra7Day.Value = 1;
            nudEra8Day.Value = 1;
            nudEra9Day.Value = 1;

            nudEra1Year.Value = 100;
            nudEra2Year.Value = 200;
            nudEra3Year.Value = 300;
            nudEra4Year.Value = 400;
            nudEra5Year.Value = 500;
            nudEra6Year.Value = 600;
            nudEra7Year.Value = 700;
            nudEra8Year.Value = 800;
            nudEra9Year.Value = 900;

            nudSteps.Value = 0;
        }
    }
}
