using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class NoteTemplatesForm : CommonKeyForm
    {
        private List<string> m_notes;

        public NoteTemplatesForm()
        {
            InitializeComponent();
        }

        public NoteTemplatesForm(StringCollection notes)
        {
            InitializeComponent();

            m_notes = new List<string>(notes.Count);
            foreach (string str in notes)
                m_notes.Add(str);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateNote();
            DialogResult = DialogResult.OK;
            UpdateFromUI();
            Close();
        }

        private void StringsViewForm_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateFromUI()
        {
            m_notes = new List<string>(lvNotes.Items.Count);
            foreach (ListViewItem lvi in lvNotes.Items)
            {
                m_notes.Add(String.Format("{0}\t{1}", lvi.Text, lvi.SubItems[1].Text));
            }
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(ActiveControl);
            return true;
        }

        private void UpdateUI()
        {
            lvNotes.BeginUpdate();
            lvNotes.Items.Clear();

            foreach (string str in m_notes)
            {
                string strSymbol, strNote;
                int iSplit = str.IndexOf('\t');
                if (iSplit == -1)
                {
                    strSymbol = "?";
                    strNote = str;
                }
                else
                {
                    strSymbol = str.Substring(0, iSplit);
                    strNote = str.Substring(iSplit + 1);
                }
                ListViewItem lvi = new ListViewItem(strSymbol);
                lvi.SubItems.Add(Global.EnsureCR(strNote));
                lvNotes.Items.Add(lvi);
            }

            Global.SizeHeadersAndContent(lvNotes);

            lvNotes.EndUpdate();
        }

        public StringCollection GetNoteTemplates()
        {
            StringCollection collection = new StringCollection();
            foreach (string str in m_notes)
                collection.Add(str);
            return collection;
        }

        private void lvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedItem();
        }

        private void ShowSelectedItem()
        {
            if (lvNotes.SelectedItems.Count < 1)
            {
                tbNoteText.Clear();
                tbSymbol.Clear();
                return;
            }

            lvNotes.SelectedItems[0].EnsureVisible();

            tbNoteText.Text = Global.EnsureCR(lvNotes.SelectedItems[0].SubItems[1].Text);
            tbSymbol.Text = lvNotes.SelectedItems[0].Text;
        }

        private void tbNoteText_Leave(object sender, EventArgs e)
        {
            UpdateNote();
        }

        private void UpdateNote()
        {
            if (lvNotes.SelectedItems.Count < 1)
                return;

            lvNotes.SelectedItems[0].SubItems[1].Text = tbNoteText.Text;
        }

        private void tbSymbol_Leave(object sender, EventArgs e)
        {
            if (lvNotes.SelectedItems.Count < 1)
                return;

            lvNotes.SelectedItems[0].Text = tbSymbol.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void InsertVariable()
        {
            string strVariable = comboVariables.SelectedItem.ToString();
            string strText = tbNoteText.Text;
            int iSelection = tbNoteText.SelectionStart;
            if (tbNoteText.SelectionStart > -1)
            {
                strText = strText.Substring(0, tbNoteText.SelectionStart) + strVariable + strText.Substring(tbNoteText.SelectionStart + tbNoteText.SelectionLength);
                iSelection += strVariable.Length;
            }
            else
            {
                strText = strText + strVariable;
                iSelection += strVariable.Length;
            }
            tbNoteText.Text = strText;
            tbNoteText.SelectionStart = iSelection;
            tbNoteText.SelectionLength = 0;
        }

        private void cmNotes_Opening(object sender, CancelEventArgs e)
        {
            miNotesDelete.Enabled = lvNotes.SelectedItems.Count > 0;
        }

        private void DeleteSelectedItems()
        {
            if (lvNotes.SelectedItems.Count < 1)
                return;

            if (MessageBox.Show(String.Format("Delete {0} note{1}?", lvNotes.SelectedItems.Count, lvNotes.SelectedItems.Count == 1 ? "" : "s"),
                "Delete Note Templates", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                return;

            lvNotes.BeginUpdate();
            while (lvNotes.SelectedItems.Count > 0)
                lvNotes.Items.RemoveAt(lvNotes.SelectedItems[0].Index);
            lvNotes.EndUpdate();
        }

        private void AddNewNote()
        {
            ListViewItem lvi = new ListViewItem("N");
            lvi.SubItems.Add("Note");
            lvNotes.Items.Add(lvi);
            lvNotes.SelectedItems.Clear();
            lvi.Selected = true;
            tbNoteText.Focus();
            tbNoteText.Select();
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            AddNewNote();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void miNotesAdd_Click(object sender, EventArgs e)
        {
            AddNewNote();
        }

        private void miNotesDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void btnInsertVariable_Click(object sender, EventArgs e)
        {
            InsertVariable();
        }

        private void miNotesResetToDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete all note templates and load the default set?", "Delete Note Templates", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;

            lvNotes.Items.Clear();

            m_notes.Clear();
            m_notes.Add("?\tPossible Encounter");
            m_notes.Add("E\tForced Encounter");
            m_notes.Add("E\tForced Encounter with $uniqueEncounterMonsters");
            m_notes.Add("E\tForced Encounter with $allEncounterMonsters");

            UpdateUI();

        }
    }
}
