using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class SearchForm : HackerBasedForm
    {
        private int m_iSearchStart = 0;
        private MapBook m_book;
        private NoteSearchItem m_currentNote = null;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bEditNote = false;
        private bool m_bCancelNote = false;

        protected override bool ShowWithoutActivation { get { return true; } }

        public SearchForm()
        {
            InitializeComponent();
            CommonKeyBeacon += SearchForm_CommonKeyBeacon;
            CommonKeyFind += SearchForm_CommonKeyFind;
            CommonKeyNext += SearchForm_CommonKeyNext;
            CommonKeySelectAll += SearchForm_CommonKeySelectAll;
        }

        void SearchForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(ActiveControl);
        }

        protected override bool OnCommonKeyClearText()
        {
            tbSearch.Text = "";
            return true;
        }

        void SearchForm_CommonKeyNext(object sender, BoolHandlerEventArgs e)
        {
            FindNext();
        }

        void SearchForm_CommonKeyFind(object sender, EventArgs e)
        {
            Find();
        }

        void SearchForm_CommonKeyBeacon(object sender, EventArgs e)
        {
            SetBeacon();
        }

        public void SetMapBook(MapBook book, MapNote noteInitial, bool bEdit)
        {
            m_book = book;
            UpdateUI(noteInitial, bEdit);
        }

        public void ClearSearch()
        {
            tbSearch.Text = "";
            if (m_bEditNote)
            {
                tbNoteText.Focus();
                tbNoteText.Select();
            }
            else
            {
                tbSearch.Select();
                tbSearch.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StringsViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_bCancelNote)
                UpdateNote();
        }

        private void Find()
        {
            tbSearch.Text = "";
            tbSearch.Focus();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.A | Keys.Control):
                    if (tbNoteText.Focused)
                        tbNoteText.SelectAll();
                    else if (tbSearch.Focused)
                        tbSearch.SelectAll();
                    return true;
                case (Keys.F | Keys.Control):
                    Find();
                    return true;
                case (Keys.G | Keys.Control):
                case (Keys.F3):
                    FindNext();
                    return true;
                case (Keys.G | Keys.Control | Keys.Shift):
                case (Keys.F3 | Keys.Shift):
                    FindPrevious();
                    return true;
                case (Keys.M | Keys.Control):
                    GoToSelectedMap();
                    return true;
                case (Keys.Enter | Keys.Control):
                    if (tbNoteText.Focused)
                        UpdateNote();
                    return true;
                case Keys.Escape:
                    m_bCancelNote = true;
                    Close();
                    return true;
                default:
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private int FindText(string str, int iStart, bool bReverse)
        {
            if (iStart < -1)
                iStart = -1;

            if (lvNotes.Items.Count < 1)
                return -1;

            if (lvNotes.Items.Count < 2)
                return ((NoteSearchItem)lvNotes.Items[0].Tag).Note.Text.IndexOf(str, 0, StringComparison.InvariantCultureIgnoreCase) == -1 ? -1 : 0;

            int iTest = lvNotes.Items.Count;
            int iDelta = bReverse ? -1 : 1;

            if (iStart == -1)
                iStart = bReverse ? lvNotes.Items.Count - 1 : 0;
            int iSearch = iStart;

            if (iSearch >= lvNotes.Items.Count)
                iSearch = 0;

            do
            {
                NoteSearchItem item = lvNotes.Items[iSearch].Tag as NoteSearchItem;
                if (item.Note.Text.IndexOf(str, 0, StringComparison.InvariantCultureIgnoreCase) != -1)
                    return iSearch;
                iSearch += iDelta;
                if (iSearch < 0)
                    iSearch = lvNotes.Items.Count - 1;
                if (iSearch >= lvNotes.Items.Count)
                    iSearch = 0;
            } while (iSearch != iStart);
            return -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            m_iSearchStart = FindText(tbSearch.Text, m_iSearchStart, false);
            ShowSearchItem();
        }

        private void StringsViewForm_Load(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void UpdateUI(MapNote noteInitial, bool bEdit)
        {
            lvNotes.ListViewItemSorter = null;

            if (m_book == null)
                return;

            if (m_book.Sheets == null)
                return;

            m_bEditNote = bEdit;

            bool bSkipVisited = Properties.Settings.Default.HideUnvisitedSquares && miNotesHideUnvisited.Checked;

            lvNotes.BeginUpdate();
            lvNotes.Items.Clear();

            int iIndexInitial = -1;

            List<ListViewItem> list = new List<ListViewItem>();

            foreach (MapSheet sheet in m_book.Sheets)
            {
                MapNote[] notes = sheet.GetAllNotes();
                foreach (MapNote note in notes)
                {
                    if (bSkipVisited)
                    {
                        MapSquare square = sheet.SquareForNote(note);
                        if (square != null && !(square.Visited))
                            continue;
                    }
                    ListViewItem lvi = new ListViewItem(sheet.GameMapIndex.ToString());
                    lvi.SubItems.Add(sheet.Title);
                    Point pt = m_main.TranslateToGameMap(note.Location, sheet);
                    lvi.SubItems.Add(String.Format("{0},{1}", pt.X, pt.Y));
                    lvi.SubItems.Add(note.Symbol);
                    lvi.SubItems.Add(note.Text);
                    lvi.Tag = new NoteSearchItem(sheet, note);
                    list.Add(lvi);
                    if (note == noteInitial)
                        iIndexInitial = lvi.Index;
                }
            }

            lvNotes.Items.AddRange(list.ToArray());

            // Global.SizeHeadersAndContent(lvNotes);   // This call is kind of slow for no particular benefit

            lvNotes.EndUpdate();

            if (iIndexInitial != -1)
            {
                lvNotes.Items[iIndexInitial].EnsureVisible();
                lvNotes.Items[iIndexInitial].Selected = true;
            }

            tbNoteText.ReadOnly = Properties.Settings.Default.ReadOnlyNotes;
        }

        public void UpdateLVI(ListViewItem lvi)
        {
            NoteSearchItem item = lvi.Tag as NoteSearchItem;
            MapNote note = item.Note;
            Point pt = m_main.TranslateToGameMap(note.Location, item.Sheet);
            lvi.SubItems[2].Text = String.Format("{0},{1}", pt.X, pt.Y);
            lvi.SubItems[3].Text = note.Symbol;
            lvi.SubItems[4].Text = note.Text;
        }

        public void RefreshMapData(MapNote noteInitial, bool bEdit)
        {
            UpdateUI(noteInitial, bEdit);
        }

        private void lvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedItem();
        }

        private void ShowSelectedItem()
        {
            if (lvNotes.SelectedItems.Count < 1)
                return;

            lvNotes.SelectedItems[0].EnsureVisible();

            m_currentNote = lvNotes.SelectedItems[0].Tag as NoteSearchItem;
            m_iSearchStart = lvNotes.SelectedItems[0].Index;
            tbNoteText.Text = m_currentNote.Note.Text;
        }

        private void lvNotes_DoubleClick(object sender, EventArgs e)
        {
            GoToSelectedMap();
        }

        private void lvNotes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvNotes.ListViewItemSorter = new NoteSearchItemComparer(e.Column, m_bAscendingSort);
            lvNotes.Sort();
        }

        private void miNotesGoToMap_Click(object sender, EventArgs e)
        {
            GoToSelectedMap();
        }

        private void miNotesFindNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void ShowSearchItem()
        {
            if (m_iSearchStart < 0 || m_iSearchStart >= lvNotes.Items.Count)
                return;

            lvNotes.Items[m_iSearchStart].Selected = true;
        }

        private void miNotesRefresh_Click(object sender, EventArgs e)
        {
            RefreshMapData(null, false);
        }

        private void GoToSelectedMap()
        {
            if (lvNotes.SelectedItems.Count < 1)
                return;

            NoteSearchItem item = lvNotes.SelectedItems[0].Tag as NoteSearchItem;
            m_main.SetCursor(item.Note.Location);
            m_main.GotoSheet(item.Sheet);
        }

        private void tbNoteText_Leave(object sender, EventArgs e)
        {
            UpdateNote();
        }

        private void UpdateNote()
        {
            if (m_currentNote == null)
                return;

            if (m_currentNote.Note.Text == tbNoteText.Text)
                return;

            m_currentNote.Note.Text = tbNoteText.Text;

            if (lvNotes.SelectedItems.Count < 1)
                return;

            UpdateLVI(lvNotes.SelectedItems[0]);

            m_main.SetUnsaved(true, false);
        }

        private void miNotesFindPrevious_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }

        private void FindPrevious()
        {
            UpdateNote();
            m_iSearchStart = FindText(tbSearch.Text, m_iSearchStart - 1, true);
            ShowSearchItem();
        }

        private void btnFindPrevious_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }

        private void FindNext()
        {
            UpdateNote();
            m_iSearchStart = FindText(tbSearch.Text, m_iSearchStart + 1, false);
            ShowSearchItem();
        }

        private void cmNotes_Opening(object sender, CancelEventArgs e)
        {
            miNotesSetBeacon.Visible = Global.Cheats && Hacker.HasBeacon;
            miNotesSetBeacon.Enabled = Global.Cheats && Hacker.HasBeacon;
            miNotesSetSurface.Visible = Global.Cheats && Hacker.HasSurfaceLocation;
            miNotesSetSurface.Enabled = Global.Cheats && Hacker.HasSurfaceLocation;
        }

        private void miNotesSetBeacon_Click(object sender, EventArgs e)
        {
            SetBeacon();
        }

        private void SetBeacon()
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvNotes.SelectedItems.Count < 1)
                return;

            NoteSearchItem item = lvNotes.SelectedItems[0].Tag as NoteSearchItem;
            Point pt = m_main.TranslateToGameMap(item.Note.Location, item.Sheet);
            Hacker.SetBeacon(pt, item.Sheet.GameMapIndex);
        }

        private void miNotesCopy_Click(object sender, EventArgs e)
        {
            NoteSearchItem item = lvNotes.FocusedItem.Tag as NoteSearchItem;
            Point pt = m_main.TranslateToGameMap(item.Note.Location, item.Sheet);

            StringBuilder sbCopy = new StringBuilder();
            sbCopy.AppendFormat("{0}\t{1}\t{2},{3}\t{4}\t{5}",
                item.Sheet.GameMapIndex,
                item.Sheet.Title,
                pt.X,
                pt.Y,
                item.Note.Symbol,
                item.Note.Text);
            Clipboard.SetText(sbCopy.ToString());
        }

        private void miNotesHideUnvisited_Click(object sender, EventArgs e)
        {
            miNotesHideUnvisited.Checked = !miNotesHideUnvisited.Checked;
            RefreshMapData(null, false);
        }

        private void miNotesSetSurface_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvNotes.FocusedItem == null)
                return;

            NoteSearchItem item = lvNotes.FocusedItem.Tag as NoteSearchItem;
            Point pt = m_main.TranslateToGameMap(item.Note.Location, item.Sheet);
            Hacker.SetExit(new MMExit(MMExitDirection.Surface, item.Sheet.GameMapIndex, pt));
        }

        private void miNotesCopyLocation_Click(object sender, EventArgs e)
        {
            NoteSearchItem item = lvNotes.FocusedItem.Tag as NoteSearchItem;
            Point pt = m_main.TranslateToGameMap(item.Note.Location, item.Sheet);
            m_main.CopyLocationText(pt, item.Sheet);
        }

        private void miNotesCopyAllLocations_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string str = tbSearch.Text;

            foreach (ListViewItem lvi in lvNotes.Items)
            {
                NoteSearchItem item = lvi.Tag as NoteSearchItem;
                if (item.Note.Text.IndexOf(str, 0, StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    Point pt = m_main.TranslateToGameMap(item.Note.Location, item.Sheet);
                    sb.AppendFormat("{0}\t{1}\r\n", m_main.GetLocationText(pt, item.Sheet.GameMapIndex, item.Sheet.Title), item.Note.Text.Replace("\r","").Replace("\n", ";"));
                }
            }

            Clipboard.SetText(sb.ToString());
        }
    }

    class NoteSearchItemComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public NoteSearchItemComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }
        public NoteSearchItemComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            NoteSearchItem item1 = ((ListViewItem)x).Tag as NoteSearchItem;
            NoteSearchItem item2 = ((ListViewItem)y).Tag as NoteSearchItem;

            switch (m_column)
            {
                case 0:
                    returnVal = Math.Sign(item1.Sheet.GameMapIndex - item2.Sheet.GameMapIndex);
                    break;
                case 1:
                    returnVal = String.Compare(item1.Sheet.Title, item2.Sheet.Title);
                    break;
                case 2:
                    returnVal = String.Compare(Global.PointString(item1.Note.Location), Global.PointString(item2.Note.Location));
                    break;
                case 3:
                    returnVal = String.Compare(item1.Note.Symbol, item2.Note.Symbol);
                    break;
                case 4:
                    returnVal = String.Compare(item1.Note.Text, item2.Note.Text);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }
}
