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
    public partial class MM345KnownSpellsEditForm : Form
    {
        private MM345KnownSpells m_spells;
        private GenericClass m_class;
        private bool m_bReadOnly = false;

        public MM345KnownSpellsEditForm()
        {
            InitializeComponent();
            Win32.SetTooltipDelay(lvSpells, 32000);
            SetSpellInfo(null);
        }
        
        public MM345KnownSpells Spells
        {
            get { return m_spells; }
            set
            {
                m_spells = value;
                if (Global.GetSpellType(m_class) != SpellType.Unknown)
                    UpdateUI();
            }
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

        public GenericClass CharacterClass
        {
            get { return m_class; }
            set
            {
                m_class = value;
                if (Spells != null)
                    UpdateUI();
            }
        }

        public void SetSpells(MM345KnownSpells spells, GenericClass charClass)
        {
            m_spells = spells;
            m_class = charClass;
            UpdateUI();
        }

        public void UpdateUI()
        {
            InitSpellList();

            for (int i = 0; i < m_spells.RawBytes.Length; i++)
            {
                if (i >= lvSpells.Items.Count)
                    break;
                lvSpells.Items[i].Checked = m_spells.RawBytes[i] != 0;
            }
        }

        private IEnumerable<MM345Spell> SpellList
        {
            get
            {
                if (m_spells is MM3KnownSpells)
                    return MM3.Spells.Values;
                if (m_spells is MM45KnownSpells)
                    return MM45.Spells.Values;
                return null;
            }
        }

        private void InitSpellList()
        {
            lvSpells.BeginUpdate();
            lvSpells.Items.Clear();
            SpellType type = Global.GetSpellType(m_class);
            foreach (MM345Spell spell in SpellList)
            {
                if (spell.Type != type)
                    continue;

                ListViewItem lvi = new ListViewItem(spell.Name);
                lvi.SubItems.Add(spell.Level.ToString());
                lvi.Tag = spell;
                lvSpells.Items.Add(lvi);
            }
            Global.SizeHeadersAndContent(lvSpells);
            lvSpells.EndUpdate();
        }

        public void UpdateFromUI()
        {
            if (m_bReadOnly)
                return;

            m_spells = m_spells.CreateNew(m_class);

            foreach (ListViewItem lvi in lvSpells.Items)
                m_spells.SetKnown(lvi.Tag as MM345Spell, lvi.Checked);
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
            foreach (ListViewItem lvi in lvSpells.Items)
                lvi.Checked = true;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            if (m_bReadOnly)
                return;
            foreach (ListViewItem lvi in lvSpells.Items)
                lvi.Checked = false;
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

        private void lvSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSpells.SelectedItems.Count < 1)
                SetSpellInfo(null);
            else
                SetSpellInfo(lvSpells.SelectedItems[0].Tag as MM345Spell);
        }

        private void SetSpellInfo(MM345Spell spell)
        {
            if (spell == null)
            {
                string strNone = "No spell selected";
                labelIndex.Text = String.Empty;
                labelType.Text = String.Empty;
                labelLevel.Text = String.Empty;
                labelCost.Text = strNone;
                labelWhen.Text = strNone;
                labelTarget.Text = strNone;
                labelLearned.Text = strNone;
                labelShort.Text = strNone;
                labelLong.Text = strNone;
                return;
            }

            labelIndex.Text = String.Format("{0}", spell.InternalIndex345);
            labelType.Text = MMSpell.TypeString(spell.Type);
            labelLevel.Text = String.Format("{0}", spell.Level);
            labelCost.Text = spell.Cost.LongString;
            labelWhen.Text = spell.WhenString;
            labelTarget.Text = spell.TargetStringFull;
            labelLearned.Text = spell.Learned;
            labelShort.Text = spell.ShortDescription;
            labelLong.Text = spell.Description;
        }
    }
}
