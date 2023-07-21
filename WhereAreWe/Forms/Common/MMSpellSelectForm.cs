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
    public partial class MMSpellSelectForm : CommonKeyForm
    {
        public MMSpellSelectForm()
        {
            InitializeComponent();

            InitSpellList();
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

        protected override bool OnCommonKeyClearText()
        {
            tbFindItem.Text = "";
            return true;
        }

        public MMSpellSelectForm(BaseCharacter mmChar, bool bShowAll = false)
        {
            InitializeComponent();

            InitSpellList(mmChar, bShowAll);
        }

        private MMInternalSpellIndex m_spell = MMInternalSpellIndex.None;

        public MMInternalSpellIndex SpellIndex
        {
            get { return m_spell; }
            set
            {
                m_spell = value;
                UpdateUI();
            }
        }

        private void InitSpellList(BaseCharacter mmChar = null, bool bShowAll = false)
        {
            comboSpell.BeginUpdate();
            comboSpell.Items.Clear();

            if (mmChar is MM3Character)
            {
                // Might and Magic 3 uses a global spell index (i.e. any character can have any spell ready,
                // even if they are the wrong class for it)
                for (MM3InternalSpellIndex index = MM3InternalSpellIndex.First; index <= MM3InternalSpellIndex.Last; index++)
                {
                    if (mmChar == null || ((MM3Character) mmChar).Spells.IsKnown((int) index, mmChar.BasicClass) || bShowAll)
                        comboSpell.Items.Add(new SpellSelectItem(index));
                }
                comboSpell.Items.Add(new SpellSelectItem(MM3InternalSpellIndex.None));
            }
            else if (mmChar is MM45Character)
            {
                // Might and Magic 4/5 uses an index for the ready spell that depends on the character class (i.e. you can only
                // have a ready spell that is appropriate for your class)
                MM45SpellRange range = ((MM45Character)mmChar).SpellRange;
                for (MM45SpellIndex index = range.First; index <= range.Last; index++)
                {
                    if (mmChar == null || ((MM45Character)mmChar).Spells.IsKnown((int) index, range.Type) || bShowAll)
                        comboSpell.Items.Add(new SpellSelectItem(index));
                }
            }
            comboSpell.EndUpdate();
        }

        public void UpdateUI()
        {
            foreach (SpellSelectItem item in comboSpell.Items)
            {
                if (item.Index == m_spell.RawIndex)
                {
                    comboSpell.SelectedItem = item;
                    break;
                }
            }

            if (comboSpell.SelectedIndex == -1)
                comboSpell.SelectedIndex = comboSpell.Items.Count - 1;
        }

        public void UpdateFromUI()
        {
            if (comboSpell.SelectedItem == null)
                m_spell = MMInternalSpellIndex.None;
            else
                m_spell.Set((SpellSelectItem) comboSpell.SelectedItem);
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
            while(iIndex < comboSpell.Items.Count)
            {
                if (comboSpell.Items[iIndex].ToString().ToLower().Contains(tbFindItem.Text.ToLower()))
                {
                    comboSpell.SelectedIndex = iIndex;
                    return;
                }
                iIndex++;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindNext()
        {
            Global.FindNext(comboSpell, tbFindItem.Text);
        }

        private void FindPrevious()
        {
            Global.FindNext(comboSpell, tbFindItem.Text, false);
        }

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            if (comboSpell.Items.Count < 1)
            {
                MessageBox.Show("This character does not know any spells!", "No spells known", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Cancel;
                Close();
            }

            tbFindItem.Focus();
            tbFindItem.Select();
        }

        private void comboSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInformation(comboSpell.SelectedItem as SpellSelectItem);
        }

        private void UpdateInformation(SpellSelectItem item)
        {
            labelIndex.Text = String.Format("{0}", (int) item.Index);
            labelType.Text = MMSpell.TypeString(item.Spell.Type);
            labelLevel.Text = String.Format("{0}", item.Spell.Level);
            labelCost.Text = item.Spell.Cost.ShortString;
            labelWhen.Text = item.Spell.WhenString;
            labelTarget.Text = item.Spell.TargetStringFull;
            labelLearned.Text = item.Spell.Learned;
            labelShort.Text = item.Spell.ShortDescription;
            labelLong.Text = item.Spell.Description;
        }
    }
}
