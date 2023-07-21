using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditWizSpellPointsForm : Form
    {
        WizSpellPoints m_sp = WizSpellPoints.Zero;
        public NumericUpDown[] nudMages;
        public NumericUpDown[] nudPriests;

        public int Minimums { get; set; }
        public int Maximums { get; set; }

        public EditWizSpellPointsForm()
        {
            InitializeComponent();
            nudMages = new NumericUpDown[] { nudMage1, nudMage2, nudMage3, nudMage4, nudMage5, nudMage6, nudMage7 };
            nudPriests = new NumericUpDown[] { nudPriest1, nudPriest2, nudPriest3, nudPriest4, nudPriest5, nudPriest6, nudPriest7 };
            Minimums = 0;
            Maximums = 9;
        }

        public void SetSP(WizSpellPoints sp)
        {
            m_sp = sp;
        }

        public short[] GetSP()
        {
            UpdateFromUI(); 
            short[] sp = new short[14];
            for (int i = 0; i < 7; i++)
            {
                sp[i] = (short) m_sp.Mage[i];
                sp[i + 7] = (short) m_sp.Priest[i];
            }
            return sp;
        }

        public byte[] GetWiz5SP()
        {
            UpdateFromUI();
            byte[] sp = new byte[16];
            sp[7] = 0;
            sp[15] = 0;
            for (int i = 0; i < 7; i++)
            {
                sp[i] = (byte)m_sp.Mage[i];
                sp[i + 8] = (byte)m_sp.Priest[i];
            }
            return sp;
        }

        private void EditWizSpellPointsForm_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateFromUI()
        {
            for (int i = 0; i < m_sp.Mage.Length; i++)
            {
                m_sp.Mage[i] = (int) nudMages[i].Value;
                m_sp.Priest[i] = (int)nudPriests[i].Value;
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < m_sp.Mage.Length; i++)
            {
                nudMages[i].Maximum = Maximums;
                nudPriests[i].Maximum = Maximums;
                nudMages[i].Minimum = Minimums;
                nudPriests[i].Minimum = Minimums;
                Global.SetNud(nudMages[i], m_sp.Mage[i]);
                Global.SetNud(nudPriests[i], m_sp.Priest[i]);
            }
        }

        private void llMinimum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (NumericUpDown nud in nudMages)
                nud.Value = Minimums;
            foreach (NumericUpDown nud in nudPriests)
                nud.Value = Minimums;
        }

        private void llMaximum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (NumericUpDown nud in nudMages)
                nud.Value = Maximums;
            foreach (NumericUpDown nud in nudPriests)
                nud.Value = Maximums;
        }
    }
}
