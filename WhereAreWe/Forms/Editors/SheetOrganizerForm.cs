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
    public partial class SheetOrganizerForm : HackerBasedForm
    {
        private MapSheetPathInfo[] m_paths;
        private bool m_bAscendingSort = true;
        private int m_iLastSortColumn = -1;
        private bool m_bPathsChanged = false;

        public SheetOrganizerForm()
        {
            InitializeComponent();
        }

        public void SetPaths(MapSheetPathInfo[] paths)
        {
            m_paths = paths;
            m_bPathsChanged = false;
        }

        public MapSheetPathInfo[] GetPaths()
        {
            return m_paths;
        }

        private void miEditPath_Click(object sender, EventArgs e)
        {
            EditSelectedItems();
        }

        private void cmListView_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvSheets.SelectedItems.Count > 0;
            miEditPath.Enabled = bAnySelected;
            miGotoMap.Enabled = bAnySelected;
        }

        private void SheetOrganizerForm_Load(object sender, EventArgs e)
        {
            foreach (MapSheetPathInfo pair in m_paths)
            {
                ListViewItem lvi = new ListViewItem(pair.Path);
                lvi.SubItems.Add(pair.Sheet.Title);
                lvi.SubItems.Add(String.Format("{0}", pair.Sheet.DefaultZoom));
                lvi.Tag = pair;
                lvSheets.Items.Add(lvi);
            }

            comboZoom.SelectedIndex = 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdatePathsFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void UpdatePathsFromUI()
        {
            foreach (ListViewItem lvi in lvSheets.Items)
            {
                MapSheetPathInfo tag = (MapSheetPathInfo)lvi.Tag;
                tag.Path = lvi.Text;
                tag.Index = lvi.Index;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvSheets_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvSheets.ListViewItemSorter = new MapSheetPairComparer(lvSheets, e.Column, m_bAscendingSort);
            lvSheets.Sort();
            m_bPathsChanged = true;
        }

        public bool PathsChanged
        {
            get { return m_bPathsChanged; }
        }

        private void lvSheets_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != lvSheets.Items[e.Item].Text)
                m_bPathsChanged = true;
        }

        private void lvSheets_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedItems();
        }

        private void EditSelectedItems()
        {
            if (lvSheets.SelectedItems.Count < 1)
                return;

            SheetPathEditForm form = new SheetPathEditForm();
            MapSheetPathInfo info = lvSheets.FocusedItem.Tag as MapSheetPathInfo;
            form.Path = info.Path;
            if (form.ShowDialog() == DialogResult.OK)
            {
                bool bAnyChanged = false;
                foreach (ListViewItem lvi in lvSheets.SelectedItems)
                {
                    info = lvi.Tag as MapSheetPathInfo;
                    if (info.Path != form.Path)
                    {
                        bAnyChanged = true;
                        info.Path = form.Path;
                        lvi.Text = info.Path;
                    }
                }
                if (bAnyChanged)
                    m_bPathsChanged = true;
            }
        }

        private void lvSheets_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control)
                    {
                        lvSheets.BeginUpdate();
                        foreach (ListViewItem lvi in lvSheets.Items)
                            lvi.Selected = true;
                        lvSheets.EndUpdate();
                    }
                    break;
                case Keys.F2:
                    if (lvSheets.FocusedItem != null)
                        lvSheets.FocusedItem.BeginEdit();
                    break;
                default:
                    break;
            }
        }

        private void lvSheets_ItemsRearranged(object sender, EventArgs e)
        {
            m_bPathsChanged = true;
        }

        private int AskForValue(string strValue)
        {
            AskValueForm form = new AskValueForm(strValue, 100, 10, 400);
            if (form.ShowDialog() == DialogResult.OK)
                return form.Value;
            return -1;
        }

        private void llSetZoom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (comboZoom.SelectedIndex)
            {
                case 0:  // Set minimum to ...
                    SetMinimumZoom(AskForValue("Minimum zoom level"));
                    break;
                case 1:  // Set minimum to 100%
                    SetMinimumZoom(100);
                    break;
                case 2:  // Set minimum to 150%
                    SetMinimumZoom(150);
                    break;
                case 3:  // Set minimum to 200%
                    SetMinimumZoom(200);
                    break;
                case 4:  // Set all to ...
                    SetMinimumZoom(AskForValue("Zoom level"));
                    break;
                case 5:  // Set all to 100%
                    SetAllZoom(100);
                    break;
                case 6:  // Set all to 150%
                    SetAllZoom(150);
                    break;
                case 7:  // Set all to 200%
                    SetAllZoom(200);
                    break;
                case 8:  // Set all to 300%
                    SetAllZoom(300);
                    break;
                default:
                    break;
            }
        }

        private void SetMinimumZoom(int iMin)
        {
            if (iMin < 18)
                return;
            int iCurrentMinimum = -1;
            IEnumerable items = lvSheets.Items;
            if (lvSheets.SelectedItems.Count > 0)
                items = lvSheets.SelectedItems;

            foreach (ListViewItem lvi in items)
            {
                MapSheetPathInfo tag = lvi.Tag as MapSheetPathInfo;
                if (tag == null)
                    continue;
                if (iCurrentMinimum == -1 || iCurrentMinimum > tag.Zoom)
                    iCurrentMinimum = tag.Zoom;
            }

            if (iCurrentMinimum == iMin)
                return; // Nothing to change

            foreach (ListViewItem lvi in items)
            {
                MapSheetPathInfo tag = lvi.Tag as MapSheetPathInfo;
                if (tag == null)
                    continue;

                tag.Zoom = tag.Zoom * iMin / iCurrentMinimum;
                lvi.SubItems[2].Text = String.Format("{0}", tag.Zoom);
            }
            m_bPathsChanged = true;
        }

        private void SetAllZoom(int iZoom)
        {
            if (iZoom < 18)
                return;
            IEnumerable items = lvSheets.Items;
            if (lvSheets.SelectedItems.Count > 0)
                items = lvSheets.SelectedItems;
            foreach (ListViewItem lvi in items)
            {
                MapSheetPathInfo tag = lvi.Tag as MapSheetPathInfo;
                if (tag.Zoom != iZoom)
                {
                    m_bPathsChanged = true;
                    tag.Zoom = iZoom;
                    lvi.SubItems[2].Text = String.Format("{0}", tag.Zoom);
                }
            }
        }

        private void miGotoMap_Click(object sender, EventArgs e)
        {
            if (lvSheets.SelectedItems.Count < 1)
                return;

            MapSheetPathInfo info = lvSheets.SelectedItems[0].Tag as MapSheetPathInfo;
            if (info == null)
                return;

            m_main.GotoSheet(info.Sheet.GameMapIndex);
        }

        private void lvSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            llSetZoom.Text = String.Format("Change zoom levels for {0} maps", lvSheets.SelectedItems.Count > 0 ? "selected" : "all");
        }
    }

    class MapSheetPairComparer : BasicListViewComparer
    {
        public MapSheetPairComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            try
            {
                MapSheetPathInfo info1 = ((ListViewItem) x).Tag as MapSheetPathInfo;
                MapSheetPathInfo info2 = ((ListViewItem) y).Tag as MapSheetPathInfo;

                switch (m_column)
                {
                    case 0: return Order(String.Compare(info1.Path, info2.Path));
                    case 1: return Order(String.Compare(info1.Sheet.Title, info2.Sheet.Title));
                    case 2: return Order(Math.Sign(info1.Zoom - info2.Zoom));
                    default: return base.Compare(x, y);
                }
            }
            catch(Exception)
            {
                return 0;
            }
        }
    }
}
