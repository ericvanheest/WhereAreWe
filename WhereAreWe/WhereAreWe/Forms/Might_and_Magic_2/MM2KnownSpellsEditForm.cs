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
    public partial class MM2KnownSpellsEditForm : Form
    {
        private EditableAttribute m_attr;
        private MM2KnownSpells m_spells;
        private bool m_bReadOnly = false;
        private ToolTip m_tipSpell = null;
        private bool m_bSorcerer = false;
        private bool m_bUpdatingUI = false;

        public MM2KnownSpellsEditForm()
        {
            InitializeComponent();

            m_tipSpell = new ToolTip();

            SetTags();
        }

        public EditableAttribute Attribute
        {
            get { return m_attr; }
            set { m_attr = value; UpdateUI(); }
        }

        public bool Sorcerer
        {
            get { return m_bSorcerer; }
            set
            {
                m_bSorcerer = value;
                SetTags();
            }
        }

        private void SetTags()
        {
            int iOffset = m_bSorcerer ? 0 : 48;
            cb11.Tag = MM2.Spells[iOffset + 0];
            cb12.Tag = MM2.Spells[iOffset + 1];
            cb13.Tag = MM2.Spells[iOffset + 2];
            cb14.Tag = MM2.Spells[iOffset + 3];
            cb15.Tag = MM2.Spells[iOffset + 4];
            cb16.Tag = MM2.Spells[iOffset + 5];
            cb17.Tag = MM2.Spells[iOffset + 6];
            cb21.Tag = MM2.Spells[iOffset + 7];
            cb22.Tag = MM2.Spells[iOffset + 8];
            cb23.Tag = MM2.Spells[iOffset + 9];
            cb24.Tag = MM2.Spells[iOffset + 10];
            cb25.Tag = MM2.Spells[iOffset + 11];
            cb26.Tag = MM2.Spells[iOffset + 12];
            cb27.Tag = MM2.Spells[iOffset + 13];
            cb31.Tag = MM2.Spells[iOffset + 14];
            cb32.Tag = MM2.Spells[iOffset + 15];
            cb33.Tag = MM2.Spells[iOffset + 16];
            cb34.Tag = MM2.Spells[iOffset + 17];
            cb35.Tag = MM2.Spells[iOffset + 18];
            cb36.Tag = MM2.Spells[iOffset + 19];
            cb41.Tag = MM2.Spells[iOffset + 20];
            cb42.Tag = MM2.Spells[iOffset + 21];
            cb43.Tag = MM2.Spells[iOffset + 22];
            cb44.Tag = MM2.Spells[iOffset + 23];
            cb45.Tag = MM2.Spells[iOffset + 24];
            cb46.Tag = MM2.Spells[iOffset + 25];
            cb51.Tag = MM2.Spells[iOffset + 26];
            cb52.Tag = MM2.Spells[iOffset + 27];
            cb53.Tag = MM2.Spells[iOffset + 28];
            cb54.Tag = MM2.Spells[iOffset + 29];
            cb55.Tag = MM2.Spells[iOffset + 30];
            cb61.Tag = MM2.Spells[iOffset + 31];
            cb62.Tag = MM2.Spells[iOffset + 32];
            cb63.Tag = MM2.Spells[iOffset + 33];
            cb64.Tag = MM2.Spells[iOffset + 34];
            cb65.Tag = MM2.Spells[iOffset + 35];
            cb71.Tag = MM2.Spells[iOffset + 36];
            cb72.Tag = MM2.Spells[iOffset + 37];
            cb73.Tag = MM2.Spells[iOffset + 38];
            cb74.Tag = MM2.Spells[iOffset + 39];
            cb81.Tag = MM2.Spells[iOffset + 40];
            cb82.Tag = MM2.Spells[iOffset + 41];
            cb83.Tag = MM2.Spells[iOffset + 42];
            cb84.Tag = MM2.Spells[iOffset + 43];
            cb91.Tag = MM2.Spells[iOffset + 44];
            cb92.Tag = MM2.Spells[iOffset + 45];
            cb93.Tag = MM2.Spells[iOffset + 46];
            cb94.Tag = MM2.Spells[iOffset + 47];
        }

        public bool ReadOnly
        {
            get { return m_bReadOnly; }
            set
            {
                m_bReadOnly = value;
                SetEnabledState();
            }
        }

        public void UpdateUI()
        {
            if (m_attr.Bytes == null || m_attr.Bytes.Length < 6)
                return;

            m_bUpdatingUI = true;

            m_spells = new MM2KnownSpells(m_attr.Bytes, 0);

            cb11.Checked = m_spells.Spells[1, 1];
            cb12.Checked = m_spells.Spells[1, 2];
            cb13.Checked = m_spells.Spells[1, 3];
            cb14.Checked = m_spells.Spells[1, 4];
            cb15.Checked = m_spells.Spells[1, 5];
            cb16.Checked = m_spells.Spells[1, 6];
            cb17.Checked = m_spells.Spells[1, 7];
            cb21.Checked = m_spells.Spells[2, 1];
            cb22.Checked = m_spells.Spells[2, 2];
            cb23.Checked = m_spells.Spells[2, 3];
            cb24.Checked = m_spells.Spells[2, 4];
            cb25.Checked = m_spells.Spells[2, 5];
            cb26.Checked = m_spells.Spells[2, 6];
            cb27.Checked = m_spells.Spells[2, 7];
            cb31.Checked = m_spells.Spells[3, 1];
            cb32.Checked = m_spells.Spells[3, 2];
            cb33.Checked = m_spells.Spells[3, 3];
            cb34.Checked = m_spells.Spells[3, 4];
            cb35.Checked = m_spells.Spells[3, 5];
            cb36.Checked = m_spells.Spells[3, 6];
            cb41.Checked = m_spells.Spells[4, 1];
            cb42.Checked = m_spells.Spells[4, 2];
            cb43.Checked = m_spells.Spells[4, 3];
            cb44.Checked = m_spells.Spells[4, 4];
            cb45.Checked = m_spells.Spells[4, 5];
            cb46.Checked = m_spells.Spells[4, 6];
            cb51.Checked = m_spells.Spells[5, 1];
            cb52.Checked = m_spells.Spells[5, 2];
            cb53.Checked = m_spells.Spells[5, 3];
            cb54.Checked = m_spells.Spells[5, 4];
            cb55.Checked = m_spells.Spells[5, 5];
            cb61.Checked = m_spells.Spells[6, 1];
            cb62.Checked = m_spells.Spells[6, 2];
            cb63.Checked = m_spells.Spells[6, 3];
            cb64.Checked = m_spells.Spells[6, 4];
            cb65.Checked = m_spells.Spells[6, 5];
            cb71.Checked = m_spells.Spells[7, 1];
            cb72.Checked = m_spells.Spells[7, 2];
            cb73.Checked = m_spells.Spells[7, 3];
            cb74.Checked = m_spells.Spells[7, 4];
            cb81.Checked = m_spells.Spells[8, 1];
            cb82.Checked = m_spells.Spells[8, 2];
            cb83.Checked = m_spells.Spells[8, 3];
            cb84.Checked = m_spells.Spells[8, 4];
            cb91.Checked = m_spells.Spells[9, 1];
            cb92.Checked = m_spells.Spells[9, 2];
            cb93.Checked = m_spells.Spells[9, 3];
            cb94.Checked = m_spells.Spells[9, 4];

            m_bUpdatingUI = false;
        }

        public void UpdateFromUI()
        {
            if (m_bReadOnly)
                return;

            m_spells.Spells[1, 1] = cb11.Checked;
            m_spells.Spells[1, 2] = cb12.Checked;
            m_spells.Spells[1, 3] = cb13.Checked;
            m_spells.Spells[1, 4] = cb14.Checked;
            m_spells.Spells[1, 5] = cb15.Checked;
            m_spells.Spells[1, 6] = cb16.Checked;
            m_spells.Spells[1, 7] = cb17.Checked;
            m_spells.Spells[2, 1] = cb21.Checked;
            m_spells.Spells[2, 2] = cb22.Checked;
            m_spells.Spells[2, 3] = cb23.Checked;
            m_spells.Spells[2, 4] = cb24.Checked;
            m_spells.Spells[2, 5] = cb25.Checked;
            m_spells.Spells[2, 6] = cb26.Checked;
            m_spells.Spells[2, 7] = cb27.Checked;
            m_spells.Spells[3, 1] = cb31.Checked;
            m_spells.Spells[3, 2] = cb32.Checked;
            m_spells.Spells[3, 3] = cb33.Checked;
            m_spells.Spells[3, 4] = cb34.Checked;
            m_spells.Spells[3, 5] = cb35.Checked;
            m_spells.Spells[3, 6] = cb36.Checked;
            m_spells.Spells[4, 1] = cb41.Checked;
            m_spells.Spells[4, 2] = cb42.Checked;
            m_spells.Spells[4, 3] = cb43.Checked;
            m_spells.Spells[4, 4] = cb44.Checked;
            m_spells.Spells[4, 5] = cb45.Checked;
            m_spells.Spells[4, 6] = cb46.Checked;
            m_spells.Spells[5, 1] = cb51.Checked;
            m_spells.Spells[5, 2] = cb52.Checked;
            m_spells.Spells[5, 3] = cb53.Checked;
            m_spells.Spells[5, 4] = cb54.Checked;
            m_spells.Spells[5, 5] = cb55.Checked;
            m_spells.Spells[6, 1] = cb61.Checked;
            m_spells.Spells[6, 2] = cb62.Checked;
            m_spells.Spells[6, 3] = cb63.Checked;
            m_spells.Spells[6, 4] = cb64.Checked;
            m_spells.Spells[6, 5] = cb65.Checked;
            m_spells.Spells[7, 1] = cb71.Checked;
            m_spells.Spells[7, 2] = cb72.Checked;
            m_spells.Spells[7, 3] = cb73.Checked;
            m_spells.Spells[7, 4] = cb74.Checked;
            m_spells.Spells[8, 1] = cb81.Checked;
            m_spells.Spells[8, 2] = cb82.Checked;
            m_spells.Spells[8, 3] = cb83.Checked;
            m_spells.Spells[8, 4] = cb84.Checked;
            m_spells.Spells[9, 1] = cb91.Checked;
            m_spells.Spells[9, 2] = cb92.Checked;
            m_spells.Spells[9, 3] = cb93.Checked;
            m_spells.Spells[9, 4] = cb94.Checked;

            byte[] bytes = m_spells.GetBytes();
            Buffer.BlockCopy(bytes, 0, m_attr.Bytes, 0, 6);
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

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (m_bReadOnly)
                return;
            foreach (Control ctrl in Controls)
            {
                if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            if (m_bReadOnly)
                return;
            foreach (Control ctrl in Controls)
            {
                if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = false;
            }
        }

        private void SetEnabledState()
        {
            if (m_bReadOnly)
                Text = "View Spellbook";
            else
                Text = "Change Known Spells";

            btnAll.Visible = !m_bReadOnly;
            btnNone.Visible = !m_bReadOnly;
            btnOK.Visible = !m_bReadOnly;
        }

        private void cbSpell_MouseEnter(object sender, EventArgs e)
        {
            if (!(sender is CheckBox))
                return;

            MM2Spell spell = (sender as CheckBox).Tag as MM2Spell;
            m_tipSpell.SetToolTip(sender as Control, spell.Name);
        }

        private void cbSpell_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingUI)
                return;

            if (!(sender is CheckBox))
                return;

            if (m_bReadOnly)
            {
                m_bUpdatingUI = true;
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
                m_bUpdatingUI = false;
            }
        }
    }
}
