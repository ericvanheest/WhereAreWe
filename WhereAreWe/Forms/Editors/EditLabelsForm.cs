using System;
using System.Collections;
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
    public partial class EditLabelsForm : HackerBasedForm
    {
        [Flags]
        enum Item
        {
            None =           0x0000,
            Name =           0x0001,
            Fore =           0x0002,
            Back =           0x0004,
            Border =         0x0008,
            ForeAlpha =      0x0010,
            BackAlpha =      0x0020,
            BorderAlpha =    0x0040,
            X =              0x0080,
            Y =              0x0100,
            Anchor =         0x0200,
            Size =           0x0400,
            Text =           0x0800,
            All = Name | Fore | Back | Border | ForeAlpha | BackAlpha | BorderAlpha | X | Y | Anchor | Size | Text
        }

        private List<MapLabel> m_undo = new List<MapLabel>();
        private bool m_bChangingValues = false;
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private bool m_bDuplicateCoordinates = false;
        private Timer m_timerUpdate = new Timer();
        private MapLabel m_labelSetSelected = null;
        private int m_iSetSelected = -1;
        private bool m_bChangingSelection = false;
        private Item m_itemsChanged = Item.None;
        private Timer m_timerSelected = new Timer();
        private bool m_bNudNeedsUpdate = false;

        public EditLabelsForm()
        {
            InitializeComponent();
            m_timerUpdate.Interval = 200;
            m_timerUpdate.Tick += m_timerUpdate_Tick;
            m_timerSelected.Interval = 50;
            m_timerSelected.Tick += m_timerSelected_Tick;
            UpdateTitle();
        }

        protected override void OnMainSet()
        {
            m_main.OptionsChanged += OnMainOptionsChanged;
            OnMainSetAgain();
        }

        protected override void OnMainSetAgain()
        {
        }

        private void UpdateTitle()
        {
            Text = String.Format("Map Labels{0}", Properties.Settings.Default.ShowMapLabels ? "" : " (not shown on map)");
        }

        void OnMainOptionsChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        void m_timerUpdate_Tick(object sender, EventArgs e)
        {
            m_timerUpdate.Stop();
            SetAllSelectedItems(LabelFromUI());
        }

        private void SetUndo()
        {
            m_undo.Clear();
            foreach (ListViewItem lvi in lvLabels.Items)
                m_undo.Add(((MapLabel)lvi.Tag).Clone());
        }

        private void miCtxCopy_Click(object sender, EventArgs e)
        {
            MapLabels labels = new MapLabels();
            foreach (ListViewItem lvi in lvLabels.SelectedItems)
            {
                MapLabel label = lvi.Tag as MapLabel;
                labels.Add(label.Clone());
            }

            MapSquareArray array = new MapSquareArray(labels);

            DataFormats.Format format = DataFormats.GetFormat(typeof(MapSquareArray).FullName);
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, array);
            Clipboard.SetDataObject(dataObj, false);
        }

        public void SetNextSelectedLabel(MapLabel label)
        {
            m_labelSetSelected = label;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == NativeMethods.WM_KEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Up | Keys.Control:
                    case Keys.Down | Keys.Control:
                    case Keys.Left | Keys.Control:
                    case Keys.Right | Keys.Control:
                    case Keys.Up | Keys.Control | Keys.Shift:
                    case Keys.Down | Keys.Control | Keys.Shift:
                    case Keys.Left | Keys.Control | Keys.Shift:
                    case Keys.Right | Keys.Control | Keys.Shift:
                        return OnMainCmdKey(keyData, m_main.CurrentSheet.SquareSize);
                    default:
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public bool OnMainCmdKey(Keys keyData, Size szSquares)
        {
            if (lvLabels.SelectedItems.Count < 1)
                return false;

            float fX = 0.0f;
            float fY = 0.0f;

            switch(keyData & ~(Keys.Control))
            {
                case Keys.Up:
                    fY = -1.0f / szSquares.Height;
                    break;
                case Keys.Down:
                    fY = 1.0f / szSquares.Height;
                    break;
                case Keys.Left:
                    fX = -1.0f / szSquares.Width;
                    break;
                case Keys.Right:
                    fX = 1.0f / szSquares.Width;
                    break;
                case Keys.Up | Keys.Shift:
                    fY = -1.0f;
                    break;
                case Keys.Down | Keys.Shift:
                    fY = 1.0f;
                    break;
                case Keys.Left | Keys.Shift:
                    fX = -1.0f;
                    break;
                case Keys.Right | Keys.Shift:
                    fX = 1.0f;
                    break;
                default: return false;
            }

            if (!m_bChangingValues)
            {
                m_bChangingValues = true;
                SetUndo();
            }

            lvLabels.BeginUpdate();

            foreach (ListViewItem lvi in lvLabels.SelectedItems)
            {
                MapLabel label = lvi.Tag as MapLabel;

                MapLabel labelNew = new MapLabel(new PointF(label.Location.X + fX, label.Location.Y + fY), label.Anchors, label.Text);
                labelNew.CopyFrom(label);
                SetLVI(lvi, labelNew);
            }
            lvLabels.EndUpdate();
            LabelsChanged();

            return true;
        }

        public void SetCurrentlySelectedLabel(MapLabel label)
        {
            foreach (ListViewItem lvi in lvLabels.Items)
            {
                if (((MapLabel)lvi.Tag).Location == label.Location)
                {
                    lvLabels.BeginUpdate();
                    lvLabels.SelectedItems.Clear();
                    lvi.Selected = true;
                    lvLabels.EndUpdate();
                    break;
                }
            }
        }

        private void CopySelectedItems()
        {
            SetUndo();
            ListViewItem[] items = new ListViewItem[lvLabels.SelectedItems.Count];
            for(int i = 0; i < lvLabels.SelectedItems.Count; i++)
                items[i] = NewLVI((lvLabels.SelectedItems[i].Tag as MapLabel).Clone());
            lvLabels.Items.AddRange(items);
            LabelsChanged();
        }

        private ListViewItem NewLVI(MapLabel label)
        {
            ListViewItem lvi = new ListViewItem(label.Text);
            SetLVI(lvi, label);
            return lvi;
        }

        private void SetLVI(ListViewItem lvi, MapLabel label)
        {
            lvi.SubItems.Clear();
            lvi.Text = label.Text;
            PointF pt = m_main.TranslateToGameMap(label.Location, null);
            lvi.SubItems.Add(String.Format("{0:F2}", pt.X));
            lvi.SubItems.Add(String.Format("{0:F2}", pt.Y));
            lvi.SubItems.Add(String.Format("{0}", label.Size));
            lvi.SubItems.Add(String.Format("{0}", label.NumAnchorPoints));
            lvi.Tag = label;
        }

        private void miCtxDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        public void DeleteSelectedItems()
        {
            SetUndo();
            lvLabels.BeginUpdate();
            List<int> selected = new List<int>(lvLabels.SelectedItems.Count);
            foreach (int i in lvLabels.SelectedIndices)
                selected.Add(i);
            for (int i = selected.Count - 1; i >= 0; i--)
                lvLabels.Items.RemoveAt(selected[i]);
            lvLabels.EndUpdate();
            LabelsChanged();
        }

        private void cmLabels_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvLabels.SelectedItems.Count > 0;
            miCtxCopy.Enabled = bAnySelected;
            miCtxDuplicate.Enabled = bAnySelected;
            miCtxDelete.Enabled = bAnySelected;
            miCtxUndo.Enabled = m_undo.Count > 0;
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(ActiveControl);
            return true;
        }

        private void miCtxAdd_Click(object sender, EventArgs e)
        {
            InsertItem();
        }

        private bool LabelExists(PointF pt)
        {
            foreach (ListViewItem lvi in lvLabels.Items)
            {
                if ((lvi.Tag as MapLabel).Location == pt)
                    return true;
            }
            return false;
        }

        private void InsertItem()
        {
            SetUndo();
            Global.DeselectAll(lvLabels);
            PointF ptLocation = m_main.TranslateToInternalMap(new PointF(0, 0), null);
            while (LabelExists(ptLocation))
                ptLocation = new PointF(ptLocation.X + 0.25f, ptLocation.Y + 0.25f);
            lvLabels.Items.Add(NewLVI(new MapLabel(ptLocation, "New Label"))).Selected = true;
            LabelsChanged();
        }

        protected override bool OnCommonKeyEscape()
        {
            Close();
            return true;
        }

        private void lvLabels_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteSelectedItems();
                    break;
                case Keys.Insert:
                    InsertItem();
                    break;
                case Keys.Enter:
                    EditFocusedItem();
                    break;
                case Keys.Escape:
                    Close();
                    break;
                case Keys.C:
                    if (e.Modifiers == Keys.Control)
                        CopySelectedItems();
                    break;
                case Keys.Z:
                    if (e.Modifiers == Keys.Control)
                        Undo();
                    break;
                case Keys.F2:
                    EditFocusedItem();
                    break;
                default:
                    break;
            }
        }

        private void EditFocusedItem()
        {
            if (lvLabels.FocusedItem != null)
                lvLabels.FocusedItem.BeginEdit();
        }

        private void Undo()
        {
            List<MapLabel> redo = new List<MapLabel>(lvLabels.Items.Count);
            foreach (ListViewItem lvi in lvLabels.Items)
                redo.Add(((MapLabel)lvi.Tag).Clone());

            lvLabels.BeginUpdate();
            lvLabels.Items.Clear();
            foreach (MapLabel label in m_undo)
                lvLabels.Items.Add(NewLVI(label));
            lvLabels.EndUpdate();
            m_undo = redo;
            LabelsChanged();
        }

        private void miCtxUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void EditLabelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetAnchor(true);
            LabelsChanged();
            SetSelectedLabels(true);
        }

        protected override void BeforeSetSize()
        {
            HideRectangles();
        }

        void m_timerSelected_Tick(object sender, EventArgs e)
        {
            m_timerSelected.Stop();
            ProcessSelectionChange();
        }

        private void lvLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_timerSelected.Enabled || m_bChangingSelection)
                return;

            m_timerSelected.Start();
        }

        private void AddAnchorLVI(Rectangle rc)
        {
            ListViewItem lvi = new ListViewItem(rc.X.ToString());
            lvi.SubItems.Add(rc.Y.ToString());
            lvi.SubItems.Add(rc.Width.ToString());
            lvi.SubItems.Add(rc.Height.ToString());
            lvi.Tag = rc;
            lvAnchors.Items.Add(lvi);
        }

        private void UpdateAnchorsUI(MapLabel label)
        {
            DisableRectControls();
            lvAnchors.BeginUpdate();
            lvAnchors.Items.Clear();
            if (label.Anchors != null)
            {
                foreach (Rectangle rc in label.Anchors)
                {
                    Rectangle rcGame = m_main.TranslateToGameMap(rc, null);
                    AddAnchorLVI(rcGame);
                }
            }
            lvAnchors.EndUpdate();
        }

        private void ProcessSelectionChange()
        {
            m_bChangingValues = false;

            m_bChangingSelection = true;

            m_main.CancelSelection();

            labelWarning.Visible = m_bDuplicateCoordinates;

            if (m_iSetSelected != -1 && m_iSetSelected < lvLabels.Items.Count)
            {
                lvLabels.SelectedItems.Clear();
                lvLabels.Items[m_iSetSelected].Selected = true;
            }

            if (lvLabels.SelectedItems.Count < 1)
            {
                tbText.Text = String.Empty;
                tbText.Enabled = false;
                nudX.Enabled = false;
                nudY.Enabled = false;
                nudSize.Enabled = false;
                nudForegroundOpacity.Enabled = false;
                nudBackgroundOpacity.Enabled = false;
                nudBorderOpacity.Enabled = false;
                pbBackgroundColor.Enabled = false;
                pbBorderColor.Enabled = false;
                pbTextColor.Enabled = false;
                SetSelectedLabels(true);
                m_bChangingSelection = false;
                lvAnchors.Items.Clear();
                DisableRectControls();
                CheckShowAnchors();
                return;
            }

            MapLabel label = lvLabels.SelectedItems[0].Tag as MapLabel;
            tbText.Text = label.Text;
            tbText.Enabled = true;
            nudX.Enabled = true;
            nudY.Enabled = true;
            nudSize.Enabled = true;
            nudForegroundOpacity.Enabled = true;
            nudBackgroundOpacity.Enabled = true;
            nudBorderOpacity.Enabled = true;
            pbBackgroundColor.Enabled = true;
            pbBorderColor.Enabled = true;
            pbTextColor.Enabled = true;
            PointF pt = m_main.TranslateToGameMap(label.Location, null);
            Global.SetNud(nudX, pt.X);
            Global.SetNud(nudY, pt.Y);
            Global.SetNud(nudSize, label.Size);
            Global.SetNud(nudForegroundOpacity, label.ForeColor.A);
            Global.SetNud(nudBackgroundOpacity, label.BackColor.A);
            Global.SetNud(nudBorderOpacity, label.BorderColor.A);
            UpdateAnchorsUI(label);
            pbBackgroundColor.BackColor = Color.FromArgb(255, label.BackColor);
            pbBorderColor.BackColor = Color.FromArgb(255, label.BorderColor);
            pbTextColor.BackColor = Color.FromArgb(255, label.ForeColor);

            m_bChangingSelection = false;

            SetSelectedLabels();

            if (m_iSetSelected != -1)
            {
                m_iSetSelected = -1;
                tbText.Focus();
                tbText.Select();
                tbText.SelectAll();
            }

            CheckShowAnchors();
        }

        private void SetSelectedLabels(bool bClearAll = false)
        {
            if (bClearAll)
                m_main.SetCurrentSheetSelectedLabels(new HashSet<PointF>());
            else
            {
                HashSet<PointF> selected = new HashSet<PointF>();
                foreach (ListViewItem lvi in lvLabels.SelectedItems)
                    selected.Add(((MapLabel)lvi.Tag).Location);
                m_main.SetCurrentSheetSelectedLabels(selected);
            }
        }

        private MapLabel LabelFromUI()
        {
            PointF pt = m_main.TranslateToInternalMap(new PointF((float)nudX.Value, (float)nudY.Value), null);
            MapLabel label = new MapLabel(pt, GetAllAnchorRects(), tbText.Text);
            label.BackColor = Color.FromArgb((int) nudBackgroundOpacity.Value, pbBackgroundColor.BackColor);
            label.BorderColor = Color.FromArgb((int) nudBorderOpacity.Value, pbBorderColor.BackColor);
            label.ForeColor = Color.FromArgb((int) nudForegroundOpacity.Value, pbTextColor.BackColor);
            label.Size = (int) nudSize.Value;
            return label;
        }

        private void SetAllSelectedItems(MapLabel label)
        {
            if (!m_bChangingValues)
            {
                m_bChangingValues = true;
                SetUndo();
            }

            lvLabels.BeginUpdate();
            foreach (ListViewItem lvi in lvLabels.SelectedItems)
            {
                if (m_itemsChanged == Item.All || m_itemsChanged == Item.None)
                    SetLVI(lvi, label);
                else
                {
                    MapLabel labelOld = lvi.Tag as MapLabel;
                    PointF ptNew = labelOld.Location;
                    Rectangle[] anchorsNew = labelOld.Anchors;
                    if (m_itemsChanged.HasFlag(Item.X))
                        ptNew.X = label.Location.X;
                    if (m_itemsChanged.HasFlag(Item.Y))
                        ptNew.Y = label.Location.Y;
                    if (m_itemsChanged.HasFlag(Item.Anchor))
                        anchorsNew = label.Anchors;
                    MapLabel labelNew = new MapLabel(ptNew, anchorsNew, m_itemsChanged.HasFlag(Item.Text) ? label.Text : labelOld.Text);
                    if (m_itemsChanged.HasFlag(Item.Fore))
                        labelNew.ForeColor = Color.FromArgb(m_itemsChanged.HasFlag(Item.ForeAlpha) ? label.ForeColor.A : labelOld.ForeColor.A, label.ForeColor);
                    else if (m_itemsChanged.HasFlag(Item.ForeAlpha))
                        labelNew.ForeColor = Color.FromArgb(label.ForeColor.A, labelOld.ForeColor);
                    else
                        labelNew.ForeColor = labelOld.ForeColor;
                    if (m_itemsChanged.HasFlag(Item.Back))
                        labelNew.BackColor = Color.FromArgb(m_itemsChanged.HasFlag(Item.BackAlpha) ? label.BackColor.A : labelOld.BackColor.A, label.BackColor);
                    else if (m_itemsChanged.HasFlag(Item.BackAlpha))
                        labelNew.BackColor = Color.FromArgb(label.BackColor.A, labelOld.BackColor);
                    else
                        labelNew.BackColor = labelOld.BackColor;
                    if (m_itemsChanged.HasFlag(Item.Border))
                        labelNew.BorderColor = Color.FromArgb(m_itemsChanged.HasFlag(Item.BorderAlpha) ? label.BorderColor.A : labelOld.BorderColor.A, label.BorderColor);
                    else if (m_itemsChanged.HasFlag(Item.BorderAlpha))
                        labelNew.BorderColor = Color.FromArgb(label.BorderColor.A, labelOld.BorderColor);
                    else
                        labelNew.BorderColor = labelOld.BorderColor;
                    labelNew.Size = m_itemsChanged.HasFlag(Item.Size) ? label.Size : labelOld.Size;
                    SetLVI(lvi, labelNew);
                }
            }
            lvLabels.EndUpdate();
            LabelsChanged();
            m_itemsChanged = Item.None;
            CheckShowAnchors();
        }

        private void LabelsChanged()
        {
            m_main.SetCurrentSheetLabels(LabelsFromUI());
            SetSelectedLabels();
            labelWarning.Visible = m_bDuplicateCoordinates;
        }

        public void SetLabels(MapLabels labels)
        {
            m_bDuplicateCoordinates = false;
            m_undo.Clear();
            lvLabels.BeginUpdate();
            lvLabels.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>(labels.Count);
            foreach (MapLabel label in labels.Values)
            {
                ListViewItem lvi = NewLVI(label);
                if (label == m_labelSetSelected)
                {
                    m_labelSetSelected = null;
                    m_iSetSelected = items.Count;
                }
                items.Add(lvi);
            }
            lvLabels.Items.AddRange(items.ToArray());
            lvLabels.EndUpdate();
            m_timerSelected.Start();
        }

        private MapLabels LabelsFromUI()
        {
            m_bDuplicateCoordinates = false;
            MapLabels labels = new MapLabels();
            foreach(ListViewItem lvi in lvLabels.Items)
            {
                MapLabel label = lvi.Tag as MapLabel;
                if (labels.ContainsKey(label.Location))
                {
                    m_bDuplicateCoordinates = true;
                    MapLabel labelOffset = label.Clone();
                    do
                    {
                        labelOffset = new MapLabel(new PointF(labelOffset.Location.X + 0.01f, labelOffset.Location.Y + 0.01f), label.Anchors, label.Text);
                    } while (labels.ContainsKey(labelOffset.Location));
                    label = labelOffset;
                    lvi.Tag = label;
                }
                labels.Add(label.Clone());
            }
            return labels;
        }

        private void UIItemChanged(object sender, EventArgs e)
        {
            if (m_bChangingSelection)
                return;

            ScheduleItemUpdate();
        }

        private void ScheduleItemUpdate()
        {
            if (m_timerUpdate.Enabled)
                return; // Already scheduled
            m_timerUpdate.Start();
        }

        private void lvLabels_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvLabels.ListViewItemSorter = new MapLabelsItemComparer(lvLabels, e.Column, m_bAscendingSort);
            lvLabels.Sort();
        }

        private void lvLabels_DoubleClick(object sender, EventArgs e)
        {
            tbText.Focus();
        }

        private void pbTextColor_Click(object sender, EventArgs e)
        {
            TitledColorDialog form = new TitledColorDialog("Select the text color");
            form.Color = pbTextColor.BackColor;
            if (form.ShowDialog() == DialogResult.OK)
            {
                pbTextColor.BackColor = form.Color;
                m_itemsChanged |= Item.Fore;
                UIItemChanged(sender, e);
            }
        }

        private void pbBackgroundColor_Click(object sender, EventArgs e)
        {
            TitledColorDialog form = new TitledColorDialog("Select the background color");
            form.Color = pbBackgroundColor.BackColor;
            if (form.ShowDialog() == DialogResult.OK)
            {
                pbBackgroundColor.BackColor = form.Color;
                m_itemsChanged |= Item.Back;
                UIItemChanged(sender, e);
            }
        }

        private void pbBorderColor_Click(object sender, EventArgs e)
        {
            TitledColorDialog form = new TitledColorDialog("Select the border color");
            form.Color = pbBorderColor.BackColor;
            if (form.ShowDialog() == DialogResult.OK)
            {
                pbBorderColor.BackColor = form.Color;
                m_itemsChanged |= Item.Border;
                UIItemChanged(sender, e);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UIItemChanged(sender, e);
        }

        private void ChangedItem(object sender, EventArgs e, Item item)
        {
            if (m_bChangingSelection)
                return;

            m_bNudNeedsUpdate = false;
            m_itemsChanged |= item;
            UIItemChanged(sender, e);
        }

        private void KeyDownItem(object sender, KeyEventArgs e, Item item)
        {
            if (m_bChangingSelection)
                return;

            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.OemPeriod:
                case Keys.OemMinus:
                case Keys.Add:
                case Keys.Back:
                    m_itemsChanged |= item;
                    m_bNudNeedsUpdate = true;
                    return;
                default:
                    return;
            }
        }

        private void nudForegroundOpacity_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.ForeAlpha); }
        private void nudBackgroundOpacity_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.BackAlpha); }
        private void nudBorderOpacity_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.BorderAlpha); }
        private void nudX_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.X); }
        private void nudY_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.Y); }
        private void nudSize_ValueChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.Size); }
        private void tbText_TextChanged(object sender, EventArgs e) { ChangedItem(sender, e, Item.Text); }

        private void EditLabelsForm_Deactivate(object sender, EventArgs e)
        {
            SetAnchor(true);
            SetSelectedLabels(true);
        }

        private void EditLabelsForm_Activated(object sender, EventArgs e)
        {
            m_main.CancelSelection();
            SetAnchor(true);
            SetSelectedLabels();
            CheckShowAnchors();
        }

        private void lvLabels_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_bNudNeedsUpdate)
            {
                m_bNudNeedsUpdate = false;
                UIItemChanged(sender, e);
            }
        }

        private void miCtxDuplicate_Click(object sender, EventArgs e)
        {
            CopySelectedItems();
        }

        private void SetAnchor(bool bClear = false)
        {
            Rectangle[] rects = bClear ? null : GetAllAnchorRects();
            if (m_main.CurrentSheet.SetAnchorSelection(rects))
                m_main.SetDirty();
        }

        private void nudForegroundOpacity_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.ForeAlpha); }
        private void nudBackgroundOpacity_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.BackAlpha); }
        private void nudBorderOpacity_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.BorderAlpha); }
        private void nudX_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.X); }
        private void nudY_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.Y); }
        private void nudSize_KeyDown(object sender, KeyEventArgs e) { KeyDownItem(sender, e, Item.Size); }

        public void HideRectangles()
        {
            if (splitContainer1.Panel2Collapsed)
                return;

            int iWidth = lvLabels.Width;
            splitContainer1.Panel2Collapsed = true;
            Width -= (lvLabels.Width - iWidth);
        }

        private void llAnchors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NativeMethods.SuspendDrawing(splitContainer1);
            int iWidth = lvLabels.Width;
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
                Width += (iWidth - lvLabels.Width);
                llAnchors.Text = "&Anchors <<";
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                Width -= (lvLabels.Width - iWidth);
                llAnchors.Text = "&Anchors >>";
            }
            NativeMethods.ResumeDrawing(splitContainer1);
        }

        private void EditLabelsForm_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
        }

        private void DisableRectControls()
        {
            nudRectX.Enabled = false;
            nudRectY.Enabled = false;
            nudRectWidth.Enabled = false;
            nudRectHeight.Enabled = false;
        }

        private void lvAnchors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAnchors.SelectedItems.Count < 1)
            {
                DisableRectControls();
                return;
            }

            Rectangle rc = (Rectangle)lvAnchors.SelectedItems[0].Tag;
            nudRectX.Enabled = true;
            nudRectY.Enabled = true;
            nudRectWidth.Enabled = true;
            nudRectHeight.Enabled = true;

            nudRectX.Value = rc.X;
            nudRectY.Value = rc.Y;
            nudRectWidth.Value = rc.Width;
            nudRectHeight.Value = rc.Height;
        }

        private void nudRectValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateAnchorRectsFromControls(sender, e);
        }

        private void nudRectValue_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateAnchorRectsFromControls(sender, e);
        }

        private void UpdateAnchorRectsFromControls(object sender, EventArgs e)
        {
            Rectangle rc = new Rectangle((int)nudRectX.Value, (int)nudRectY.Value, (int)nudRectWidth.Value, (int)nudRectHeight.Value);
            lvAnchors.BeginUpdate();
            foreach (ListViewItem lvi in lvAnchors.SelectedItems)
            {
                lvi.SubItems[0].Text = rc.X.ToString();
                lvi.SubItems[1].Text = rc.Y.ToString();
                lvi.SubItems[2].Text = rc.Width.ToString();
                lvi.SubItems[3].Text = rc.Height.ToString();
                lvi.Tag = rc;
            }
            lvAnchors.EndUpdate();

            ChangedItem(sender, e, Item.Anchor);
        }

        private Rectangle[] GetAllAnchorRects(bool bConvertFromGame = true)
        {
            Rectangle[] rects = new Rectangle[lvAnchors.Items.Count];
            for (int i = 0; i < rects.Length; i++)
            {
                if (bConvertFromGame)
                    rects[i] = m_main.TranslateToInternalMap((Rectangle)lvAnchors.Items[i].Tag, null);
                else
                    rects[i] = (Rectangle)lvAnchors.Items[i].Tag;
            }
            return rects;
        }

        private void miAnchorAdd_Click(object sender, EventArgs e)
        {
            AddAnchorLVI(Rectangle.Empty);
            ChangedItem(sender, e, Item.Anchor);
        }

        private void miAnchorDuplicate_Click(object sender, EventArgs e)
        {
            lvAnchors.BeginUpdate();
            foreach (ListViewItem lvi in lvAnchors.SelectedItems)
                AddAnchorLVI((Rectangle)lvi.Tag);
            lvAnchors.EndUpdate();
            ChangedItem(sender, e, Item.Anchor);
        }

        private void miAnchorDelete_Click(object sender, EventArgs e)
        {
            lvAnchors.BeginUpdate();
            foreach (ListViewItem lvi in lvAnchors.SelectedItems)
                lvi.Remove();
            lvAnchors.EndUpdate();
            ChangedItem(sender, e, Item.Anchor);
        }

        private void cmAnchors_Opening(object sender, CancelEventArgs e)
        {
            bool bSelected = lvAnchors.SelectedItems.Count > 0;

            miAnchorAdd.Enabled = true;
            miAnchorDuplicate.Enabled = bSelected;
            miAnchorDelete.Enabled = bSelected;
        }

        private void CheckShowAnchors()
        {
            if (cbShowAnchors.Checked)
                m_main.CurrentSheet.SetAnchorSelection(GetAllAnchorRects());
        }

        private void cbShowAnchors_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowAnchors.Checked)
                SetAnchor();
            else
                SetAnchor(true);
        }
    }

    class MapLabelsItemComparer : BasicListViewComparer
    {
        public MapLabelsItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            if (x == null || y == null)
                return 0;

            MapLabel label1 = (MapLabel)((ListViewItem)x).Tag;
            MapLabel label2 = (MapLabel)((ListViewItem)y).Tag;

            switch (m_column)
            {
                case 0: return Order(String.Compare(label1.Text, label2.Text));
                case 1: return Order(Math.Sign((int)(label1.Location.X - label2.Location.X)));
                case 2: return Order(Math.Sign((int)(label1.Location.Y - label2.Location.Y)));
                case 3: return Order(Math.Sign(label1.Size - label2.Size));
                case 4: return Order(String.Compare(Global.PointString(label1.Anchors[0].Location), Global.PointString(label2.Anchors[0].Location)));
                default: return base.Compare(x, y);
            }
        }
    }
}
