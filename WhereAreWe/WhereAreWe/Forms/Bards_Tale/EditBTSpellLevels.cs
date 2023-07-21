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
    public partial class EditBTSpellLevels : Form
    {
        private BTSpellLevel m_spellLevels;

        public BTSpellLevel SpellLevel { get { return m_spellLevels; } set { m_spellLevels = value; UpdateUI(); } }

        public EditBTSpellLevels()
        {
            InitializeComponent();
        }

        public EditBTSpellLevels(BTSpellLevel level)
        {
            InitializeComponent();
            SpellLevel = level;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            UpdateFromUI();
            Close();
        }

        private void UpdateUI()
        {
            Global.SetNud(nudSorcerer, m_spellLevels.Sorcerer);
            Global.SetNud(nudConjurer, m_spellLevels.Conjurer);
            Global.SetNud(nudMagician, m_spellLevels.Magician);
            Global.SetNud(nudWizard, m_spellLevels.Wizard);
            Global.SetNud(nudArchmage, m_spellLevels.Archmage);
        }

        private void UpdateFromUI()
        {
            SpellLevel = new BTSpellLevel((int)nudSorcerer.Value, (int)nudConjurer.Value, (int)nudMagician.Value, (int)nudWizard.Value, (int)nudArchmage.Value);
        }
    }
}
