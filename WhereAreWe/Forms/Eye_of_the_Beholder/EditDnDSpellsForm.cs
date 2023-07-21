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
    public partial class EditDnDSpellsForm : CommonKeyForm
    {
        private DnDKnownSpells m_spells;
        private BaseCharacter m_char;
        private bool m_bReadOnly = false;
        private bool m_bUpdating = false;
        private bool m_bAscendingSort = true;
        private int m_iLastSortColumn = -1;

        private FindBox m_findBox = null;

        public EditDnDSpellsForm()
        {
            InitializeComponent();
            NativeMethods.SetTooltipDelay(lvSpells, 32000);
            SetSpellInfo(null);
        }

        public DnDKnownSpells Spells
        {
            get { return m_spells; }
            set
            {
                m_spells = value;
                if (Global.GetSpellType(CharacterClass) != SpellType.Unknown)
                    UpdateUI();
            }
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(lvSpells);
            return true;
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
            get { return m_char == null ? GenericClass.None : m_char.BasicClass; ; }
        }

        public void SetSpells(DnDKnownSpells spells, BaseCharacter bc)
        {
            m_spells = spells;
            m_char = bc;
            UpdateUI();
        }

        public void UpdateUI()
        {
            InitSpellList();
        }

        private void AddSpell(Spell spell, bool bKnown, int iSelected, int iMemorized)
        {
            ListViewItem lvi = new ListViewItem(Spell.TypeString(spell.Type));
            lvi.Tag = new SpellTag(spell, bKnown, iSelected, iMemorized);
            UpdateListItem(lvi);
            lvSpells.Items.Add(lvi);
        }

        private void AddUpdateSubItem(ListViewItem lvi, int iSubItem, string strText)
        {
            if (lvi.SubItems.Count <= iSubItem)
                lvi.SubItems.Add(strText);
            else
                lvi.SubItems[iSubItem].Text = strText;
        }

        private void UpdateListItem(ListViewItem lvi)
        {
            SpellTag tag = lvi.Tag as SpellTag;
            AddUpdateSubItem(lvi, 1, tag.Spell.Level.ToString());
            AddUpdateSubItem(lvi, 2, tag.Spell.Name);
            AddUpdateSubItem(lvi, 3, tag.Castable ? "Yes" : "");
            AddUpdateSubItem(lvi, 4, String.Format("{0}", tag.Selected));
            AddUpdateSubItem(lvi, 5, String.Format("{0}", tag.Memorized));
        }

        private IEnumerable<Spell> SpellList
        {
            get
            {
                if (m_spells is EOBKnownSpells)
                    return EOB1.Spells;
                return null;
            }
        }

        private void InitSpellList()
        {
            lvSpells.BeginUpdate();
            lvSpells.Items.Clear();
            foreach (Spell spell in SpellList)
            {
                if (spell.BasicIndex == 0)
                    continue;
                AddSpell(spell, m_spells.InBook(spell.BasicIndex), m_spells.NumSelected(spell.BasicIndex), m_spells.NumMemorized(spell.BasicIndex));
            }
            Global.SizeHeadersAndContent(lvSpells);
            lvSpells.EndUpdate();
        }

        public void UpdateFromUI()
        {
            if (m_bReadOnly)
                return;

            m_spells = m_spells.CreateNew(CharacterClass, null) as DnDKnownSpells;

            foreach (ListViewItem lvi in lvSpells.Items)
                m_spells.Add(lvi.Tag as SpellTag);
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

        private void SetEnabledState()
        {
            if (m_bReadOnly)
            {
                Text = "View Spellbook";
                btnCancel.Text = "&Close";
            }
            else
            {
                Text = "Change Memorized Spells";
                btnCancel.Text = "&Cancel";
            }

            btnOK.Visible = !m_bReadOnly;
        }

        private void lvSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSpells.SelectedItems.Count < 1)
                SetSpellInfo(null);
            else
                SetSpellInfo(lvSpells.SelectedItems[0].Tag as SpellTag);
        }

        private void SetSpellInfo(SpellTag tag)
        {
            if (tag == null || tag.Spell == null)
            {
                string strNone = "No spell selected";
                labelIndex.Text = String.Empty;
                labelType.Text = String.Empty;
                labelLevel.Text = String.Empty;
                labelTarget.Text = strNone;
                labelLearned.Text = strNone;
                labelShort.Text = strNone;
                labelLong.Text = strNone;
                return;
            }

            labelIndex.Text = String.Format("{0}", tag.Spell.BasicIndex);
            labelType.Text = MMSpell.TypeString(tag.Spell.Type);
            labelLevel.Text = String.Format("{0}", tag.Spell.Level);
            labelTarget.Text = tag.Spell.TargetStringFull;
            labelLearned.Text = tag.Spell.Learned;
            labelShort.Text = tag.Spell.ShortDescription;
            labelLong.Text = tag.Spell.Description;
            cbInSpellbook.Checked = tag.Castable;
            nudMemorized.Maximum = m_spells.Maximum(tag.Spell.Type);
            nudSelected.Maximum = m_spells.Maximum(tag.Spell.Type);
            Global.SetNud(nudSelected, tag.Selected);
            Global.SetNud(nudMemorized, tag.Memorized);

            nudSelected.Enabled = !m_bReadOnly;
            nudMemorized.Enabled = !m_bReadOnly;
            cbInSpellbook.Enabled = !m_bReadOnly;
            UpdateCounts();
        }

        private void UpdateCounts()
        {
            string strFormat = "Total: {0} Mage, {1} Cleric";

            int iCountMageSelected = 0;
            int iCountMageMemorized = 0;
            int iCountClericSelected = 0;
            int iCountClericMemorized = 0;
            foreach (ListViewItem lvi in lvSpells.Items)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                switch (tag.Spell.Type)
                {
                    case SpellType.Mage:
                        iCountMageSelected += tag.Selected;
                        iCountMageMemorized += tag.Memorized;
                        break;
                    case SpellType.Cleric:
                        iCountClericSelected += tag.Selected;
                        iCountClericMemorized += tag.Memorized;
                        break;
                    default:
                        break;
                }
            }
            labelTotalSelected.Text = String.Format(strFormat, iCountMageSelected, iCountClericSelected);
            labelTotalMemorized.Text = String.Format(strFormat, iCountMageMemorized, iCountClericMemorized);
            if (lvSpells.SelectedItems.Count > 0)
            {
                SpellTag tag = lvSpells.SelectedItems[0].Tag as SpellTag;
                m_bUpdating = true;
                Global.SetNud(nudMemorized, tag.Memorized);
                Global.SetNud(nudSelected, tag.Selected);
                m_bUpdating = false;
            }
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
            WindowInfo info = Global.Windows.Get(WindowType.EditDnDSpells);
            Global.SetWindowInfo(this, info);
            Global.SetSplitterDistance(scSpellsInfo, info.SplitPositions, 0);
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

        private int AvailableToMemorize(SpellType type)
        {
            int iAvailable = m_spells.Maximum(type);
            foreach (ListViewItem lvi in lvSpells.Items)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                if (tag.Spell.Type != type)
                    continue;
                iAvailable -= tag.Memorized;
                iAvailable -= tag.Selected;
            }
            return Math.Max(0, iAvailable);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (m_bReadOnly)
            {
                e.Cancel = true;
                return;
            }
            miUnmemorize.Enabled = lvSpells.SelectedItems.Count > 0;
            miMemorizeMaximum.Enabled = lvSpells.SelectedItems.Count > 0;
        }

        private void miMemorizeMaximum_Click(object sender, EventArgs e)
        {
            if (lvSpells.SelectedItems.Count < 1)
                return;
            foreach (ListViewItem lvi in lvSpells.SelectedItems)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                tag.Memorized = 0;
                tag.Selected = 0;
            }
            bool bAnyChanged = true;
            while (bAnyChanged)
            {
                bAnyChanged = false;
                foreach (ListViewItem lvi in lvSpells.SelectedItems)
                {
                    SpellTag tag = lvi.Tag as SpellTag;
                    if (AvailableToMemorize(tag.Spell.Type) > 0)
                    {
                        tag.Memorized++;
                        bAnyChanged = true;
                    }
                }
            }
            lvSpells.BeginUpdate();
            foreach (ListViewItem lvi in lvSpells.SelectedItems)
                UpdateListItem(lvi);
            lvSpells.EndUpdate();
            UpdateCounts();
        }

        private void miUnmemorize_Click(object sender, EventArgs e)
        {
            if (lvSpells.SelectedItems.Count < 1)
                return;
            foreach (ListViewItem lvi in lvSpells.SelectedItems)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                tag.Memorized = 0;
                tag.Selected = 0;
                UpdateListItem(lvi);
                ConstrainSpells(tag);
            }
            UpdateCounts();
        }

        private void nudSelected_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (lvSpells.SelectedItems.Count < 1)
                return;

            SpellTag tag = lvSpells.SelectedItems[0].Tag as SpellTag;
            tag.Selected = (int)nudSelected.Value;
            if (tag.Selected + tag.Memorized > m_spells.Maximum(tag.Spell.Type))
            {
                tag.Memorized = m_spells.Maximum(tag.Spell.Type) - tag.Selected;
                m_bUpdating = true;
                Global.SetNud(nudMemorized, tag.Memorized);
                m_bUpdating = false;
            }
            UpdateListItem(lvSpells.SelectedItems[0]);
            ConstrainSpells(tag);
            UpdateCounts();
        }

        private void nudMemorized_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            if (lvSpells.SelectedItems.Count < 1)
                return;

            SpellTag tag = lvSpells.SelectedItems[0].Tag as SpellTag;
            tag.Memorized = (int)nudMemorized.Value;
            if (tag.Selected + tag.Memorized > m_spells.Maximum(tag.Spell.Type))
            {
                tag.Selected = m_spells.Maximum(tag.Spell.Type) - tag.Memorized;
                m_bUpdating = true;
                Global.SetNud(nudSelected, tag.Selected);
                m_bUpdating = false;
            }
            UpdateListItem(lvSpells.SelectedItems[0]);
            ConstrainSpells(tag);
            UpdateCounts();
        }

        private void EditDnDSpellsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowInfo info = Global.Windows.Get(WindowType.EditDnDSpells);
            info.SplitPositions = new int[] { scSpellsInfo.SplitterDistance };
            Global.Windows.Set(WindowType.EditDnDSpells, info);
        }

        private void ConstrainSpells(SpellTag taken)
        {
            // Remove other selected or memorized spells in order to allow the selected item to have the number desired
            int iCount = 0;
            foreach (ListViewItem lvi in lvSpells.Items)
            {
                if (lvi.Selected)
                    continue;

                bool bAnyChanged = false;
                SpellTag tag = lvi.Tag as SpellTag;
                if (tag.Spell.Type != taken.Spell.Type)
                    continue;
                iCount += tag.Memorized;
                iCount += tag.Selected;
                while (iCount + taken.Memorized + taken.Selected > m_spells.Maximum(tag.Spell.Type) && tag.Selected > 0)
                {
                    tag.Selected--;
                    iCount--;
                    bAnyChanged = true;
                }
                while (iCount + taken.Memorized + taken.Selected > m_spells.Maximum(tag.Spell.Type) && tag.Memorized > 0)
                {
                    tag.Memorized--;
                    iCount--;
                    bAnyChanged = true;
                }
                if (bAnyChanged)
                    UpdateListItem(lvi);
            }
        }

        private void cbInSpellbook_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bReadOnly)
                return;
            lvSpells.BeginUpdate();
            foreach (ListViewItem lvi in lvSpells.SelectedItems)
            {
                SpellTag tag = lvi.Tag as SpellTag;
                tag.Castable = cbInSpellbook.Checked;
                UpdateListItem(lvi);
            }
            lvSpells.EndUpdate();
        }

        private void lvSpells_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvSpells.ListViewItemSorter = new DnDSpellComparer(lvSpells, e.Column, m_bAscendingSort);
            lvSpells.Sort();
        }
    }

    class DnDSpellComparer : BasicListViewComparer
    {
        public DnDSpellComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            SpellTag spell1 = ((ListViewItem)x).Tag as SpellTag;
            SpellTag spell2 = ((ListViewItem)y).Tag as SpellTag;

            switch (m_column)
            {
                case 1: return Order(Math.Sign(spell1.Spell.Level - spell2.Spell.Level));
                case 4: return Order(Math.Sign(spell1.Selected - spell2.Selected));
                case 5: return Order(Math.Sign(spell1.Memorized - spell2.Memorized));
                default: return base.Compare(x, y);
            }
        }
    }
}
