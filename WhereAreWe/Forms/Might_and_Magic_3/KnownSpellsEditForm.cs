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
    public partial class KnownSpellsEditForm : CommonKeyForm
    {
        private KnownSpells m_spells;
        private GenericClass m_class;
        private bool m_bReadOnly = false;
        private FindBox m_findBox = null;

        public KnownSpellsEditForm()
        {
            InitializeComponent();
            NativeMethods.SetTooltipDelay(lvSpells, 32000);
            SetSpellInfo(null);
        }
        
        public KnownSpells Spells
        {
            get { return m_spells; }
            set
            {
                m_spells = value;
                if (Global.GetSpellType(m_class) != SpellType.Unknown)
                    UpdateUI();
            }
        }

        protected override bool OnCommonKeyClearText()
        {
            tbFind.Text = "";
            return true;
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

        public void SetSpells(KnownSpells spells, GenericClass charClass)
        {
            m_spells = spells;
            m_class = charClass;
            UpdateUI();
        }

        public void UpdateUI()
        {
            InitSpellList();

            if (m_spells is MM345KnownSpells)
            {
                byte[] bytes = m_spells.GetBytes();
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i >= lvSpells.Items.Count)
                        break;
                    lvSpells.Items[i].Checked = bytes[i] != 0;
                }
            }
            else if (m_spells is Wiz5KnownSpells)
            {
                for (Wiz5SpellIndex i = Wiz5SpellIndex.First; i < Wiz5SpellIndex.Last; i++)
                    lvSpells.Items[(int)i - 1].Checked = m_spells.IsKnown((int)i, GenericClass.None);
            }
            else if (m_spells is Wiz123KnownSpells)
            {
                for(Wiz1234SpellIndex i = Wiz1234SpellIndex.Halito; i < Wiz1234SpellIndex.Last; i++)
                    lvSpells.Items[(int) i-1].Checked = m_spells.IsKnown((int) i, GenericClass.None);
            }
            else if (m_spells is BT3KnownSpells)
            {
                for (BT3SpellIndex i = BT3SpellIndex.MageFlame; i < BT3SpellIndex.Last; i++)
                    lvSpells.Items[(int)i - 1].Checked = m_spells.IsKnown((int)i, GenericClass.None);
            }
        }

        private IEnumerable<Spell> SpellList
        {
            get
            {
                if (m_spells is MM3KnownSpells)
                    return MM3.Spells.Values;
                if (m_spells is MM45KnownSpells)
                    return MM45.Spells.Values;
                if (m_spells is Wiz5KnownSpells)
                    return Wiz5.Spells.Skip(1);
                if (m_spells is Wiz123KnownSpells)
                    return Wiz123.Spells.Skip(1);
                if (m_spells is BT3KnownSpells)
                    return BT3.Spells;
                return null;
            }
        }

        private void InitSpellList()
        {
            lvSpells.BeginUpdate();
            lvSpells.Items.Clear();
            SpellType type = Global.GetSpellType(m_class);
            foreach (Spell spell in SpellList)
            {
                if (spell.Type != type && m_spells.UsesSpellType)
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

            m_spells = m_spells.CreateNew(m_class, m_spells);

            foreach (ListViewItem lvi in lvSpells.Items)
                m_spells.SetKnown(lvi.Tag as Spell, lvi.Checked);
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
            {
                Text = "View Spellbook";
                btnCancel.Text = "&Close";
            }
            else
            {
                Text = "Change Known Spells";
                btnCancel.Text = "&Cancel";
            }

            btnAll.Visible = !m_bReadOnly;
            btnNone.Visible = !m_bReadOnly;
            btnOK.Visible = !m_bReadOnly;
        }

        private void lvSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSpells.SelectedItems.Count < 1)
                SetSpellInfo(null);
            else
                SetSpellInfo(lvSpells.SelectedItems[0].Tag as Spell);
        }

        private void SetSpellInfo(Spell spell)
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

            labelIndex.Text = String.Format("{0}", spell.BasicIndex);
            labelType.Text = MMSpell.TypeString(spell.Type);
            labelLevel.Text = String.Format("{0}", spell.Level);
            labelCost.Text = spell.Cost.LongString;
            labelWhen.Text = spell.WhenString;
            labelTarget.Text = spell.TargetStringFull;
            labelLearned.Text = spell.Learned;
            if (!String.IsNullOrWhiteSpace(spell.Abbreviation))
                labelShort.Text = String.Format("{0}: {1}", spell.Abbreviation, spell.ShortDescription);
            else
                labelShort.Text = spell.ShortDescription;
            labelLong.Text = spell.Description;
        }

        private void lvSpells_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_bReadOnly)
                e.NewValue = e.CurrentValue;
        }

        private void CloseForm()
        {
            m_findBox.HideFindBox();
            Close();
        }

        private void KnownSpellsEditForm_Load(object sender, EventArgs e)
        {
            m_findBox = new FindBox(scSpellList, tbFind, FindBox.ListViewFindFunction, lvSpells);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;
            scSpellList.Panel2Collapsed = true;
        }

        private void KnownSpellsEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
            if (m_findBox.Focused)
                return;
            CloseForm();
        }
    }
}
