using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public delegate BitDesc BitDescriptionDelegate(object val);

    public enum BitIndex
    {
        DefaultForGame = 0,
        ZeroToSeven = 1,
        SevenToZero = 2
    }

    public partial class EditBitsForm : HackerBasedForm
    {
        private bool m_bReadOnly = false;
        private byte[] m_bytes;
        private bool m_bUpdating = false;
        public BitDescriptionDelegate DescriptionFunction { get; set; }
        private Control m_parent = null;
        private FindBox m_findBox = null;

        public int[] AutosizeColumns { get; set; }

        public EditBitsForm()
        {
            InitializeComponent();
        }

        public EditBitsForm(BitDescriptionDelegate fn, Control parent)
        {
            InitializeComponent();
            DescriptionFunction = fn;
            m_parent = parent;
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
                Text = m_bReadOnly ? "View Bits" : "Change Bits";
                llAll.Visible = !m_bReadOnly;
                llNone.Visible = !m_bReadOnly;
            }
        }

        public byte[] Bytes
        {
            get
            {
                return BytesFromUI();
            }

            set
            {
                m_bytes = value;
            }
        }

        public int GetBitIndex(int iBit)
        {
            bool bReverse = Properties.Settings.Default.BitIndex == (int)BitIndex.SevenToZero ||
                (Properties.Settings.Default.BitIndex == (int)BitIndex.DefaultForGame && Hacker.DefaultBitIndex == BitIndex.SevenToZero);
            if (bReverse)
                return (iBit / 8) * 8 + (7 - (iBit % 8));
            return iBit;
        }

        public void SetFromBytes(byte[] bytes)
        {
            m_bUpdating = true;

            int iBit = 0;
            int iByte = 0;

            lvBits.BeginUpdate();
            lvBits.Items.Clear();

            StringBuilder sb = new StringBuilder();

            if (bytes != null)
            {
                while (iByte < bytes.Length)
                {
                    ListViewItem lvi = new ListViewItem(new string[] { String.Format("{0}", GetBitIndex(iBit)), String.Format("{0}", iByte) });
                    BitDesc desc = DescriptionFunction == null ? null : DescriptionFunction(iBit);

                    lvi.SubItems.Add(desc.SetWhen);
                    lvi.SubItems.Add(desc.SetWhere);
                    lvi.SubItems.Add(desc.CheckedWhen);
                    lvi.SubItems.Add(desc.CheckedWhere);
                    lvi.SubItems.Add(desc.ClearedWhen);
                    lvi.SubItems.Add(desc.ClearedWhere);
                    lvi.Checked = (Global.GetBit(bytes, iBit) != 0);
                    lvi.Tag = new BitsItemTag(iBit, desc);
                    lvBits.Items.Add(lvi);
                    iBit++;
                    if (iBit % 8 == 0)
                    {
                        sb.AppendFormat("{0:X2} ", bytes[iByte]);
                        iByte++;
                    }
                }

                if ((iBit % 8) != 0)
                    sb.AppendFormat("{0:X2} ", bytes[iByte]);

                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);

                labelBytes.Text = sb.ToString();
            }

            //Global.SizeHeadersAndContent(lvBits);
            lvBits.EndUpdate();

            m_bUpdating = false;
        }

        private void UpdateByteText()
        {
            if (m_bUpdating)
                return;

            StringBuilder sb = new StringBuilder();

            byte b = 0;
            for (int i = 0; i < lvBits.Items.Count; i++)
            {
                b |= (byte) ((lvBits.Items[i].Checked ? 1 : 0) << (7 - (i % 8)));
                if ((i+1) % 8 == 0)
                {
                    sb.AppendFormat("{0:X2} ", b);
                    b = 0;
                }
            }

            if ((lvBits.Items.Count % 8) != 0)
                sb.AppendFormat("{0:X2} ", b);

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            labelBytes.Text = sb.ToString();
        }

        public byte[] BytesFromUI()
        {
            MemoryStream ms = new MemoryStream((lvBits.Items.Count + 7) / 8);

            byte b = 0;
            for(int iBit = 0; iBit < lvBits.Items.Count; iBit++)
            {
                b |= (byte) ((lvBits.Items[iBit].Checked ? 1 : 0) << (7 - (iBit % 8)));
                if (((iBit+1) % 8) == 0)
                {
                    ms.WriteByte(b);
                    b = 0;
                }
            }

            return ms.ToArray();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            if (splitContainer1.Panel2Collapsed)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            m_findBox.HideFindBox();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_bytes = BytesFromUI();
            DialogResult = DialogResult.OK;
            Global.Windows.Set(WindowType.EditBits, this);
            Close();
        }

        private void labelBytes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                cmBytes.Show(Cursor.Position);
        }

        private void miBytesEdit_Click(object sender, EventArgs e)
        {
            EditBytesForm form = new EditBytesForm(TopLevelControl);
            form.ForceLength = true;
            form.Bytes = BytesFromUI();
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_bytes = form.Bytes;
                SetFromBytes(m_bytes);
            }
        }

        private void miBytesCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(labelBytes.Text);
        }

        private void lvBits_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateByteText();
        }

        private void EditBitsForm_Load(object sender, EventArgs e)
        {
            OnBitIndexUpdated();

            m_findBox = new FindBox(splitContainer1, tbFind, FindBox.ListViewFindFunction, lvBits);
            CommonKeyFind += m_findBox.Find;
            CommonKeyNext += m_findBox.Next;
            CommonKeyPrevious += m_findBox.Previous;
            splitContainer1.Panel2Collapsed = true;

            if (Global.Windows.IsEmpty(WindowType.EditBits) && m_parent != null)
                Global.Windows.SetNormalSize(WindowType.EditBits, Global.GetCenterRect(this, m_parent.Bounds));
            Global.SetWindowInfo(this, Global.Windows.Get(WindowType.EditBits));

            SetFromBytes(m_bytes);

            if (AutosizeColumns != null)
            {
                foreach (int i in AutosizeColumns)
                {
                    lvBits.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
        }

        private void llAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_bReadOnly)
                return;
            m_bUpdating = true;
            for (int i = 0; i < lvBits.Items.Count; i++)
                lvBits.Items[i].Checked = true;
            m_bUpdating = false;
            UpdateByteText();
        }

        private void llNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_bReadOnly)
                return;
            m_bUpdating = true;
            for (int i = 0; i < lvBits.Items.Count; i++)
                lvBits.Items[i].Checked = false;
            m_bUpdating = false;
            UpdateByteText();
        }

        private void cmBits_Opening(object sender, CancelEventArgs e)
        {
            if (lvBits.SelectedItems.Count < 1)
            {
                e.Cancel = true;
                return;
            }

            if (!Global.Cheats || !Hacker.HasBeacon)
            {
                miBitsSetBeacon.Enabled = false;
                return;
            }

            miBitsSetBeacon.Enabled = true;
            miBitsSetBeacon.DropDownItems.Clear();
            miBitsSetBeacon.Tag = null;

            miBitsGotoMap.Enabled = true;
            miBitsGotoMap.DropDownItems.Clear();
            miBitsGotoMap.Tag = null;

            if (lvBits.FocusedItem == null)
                return;

            BitsItemTag tag = lvBits.FocusedItem.Tag as BitsItemTag;
            List<BitDesc.WWW> locations = tag.Description.UniqueLocations;
            if (locations.Count == 1)
            {
                miBitsSetBeacon.Tag = locations[0];
                miBitsGotoMap.Tag = locations[0];
                return;
            }
            else if (locations.Count > 1)
            {
                miBitsSetBeacon.DropDownItems.Add("<location>");
                miBitsGotoMap.DropDownItems.Add("<location>");
            }
            else
            {
                miBitsSetBeacon.Enabled = false;
                miBitsGotoMap.Enabled = false;
            }
        }

        private void miBitsCopy_Click(object sender, EventArgs e)
        {
            CopySelected();
        }

        private void CopySelected()
        {
            if (lvBits.SelectedItems.Count < 1)
                return;

            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in lvBits.SelectedItems)
            {
                BitsItemTag tag = lvi.Tag as BitsItemTag;
                if (tag == null)
                    return;
                sb.AppendFormat("{0}\t{1}\t{2}", GetBitIndex(tag.Bit), tag.Bit / 8, tag.Description.AllText);
                sb.AppendLine();
            }

            Clipboard.SetText(sb.ToString());
        }

        private void miBitsSetBeacon_Click(object sender, EventArgs e)
        {
            if (miBitsSetBeacon.HasDropDownItems)
                return;

            BitDesc.WWW loc = miBitsSetBeacon.Tag as BitDesc.WWW;
            if (loc == null || loc.Where == null)
                return;

            SetBeacon(loc.Where);
        }

        private void miBitsSetBeacon_DropDownOpening(object sender, EventArgs e)
        {
            miBitsSetBeacon.DropDownItems.Clear();
            miBitsSetBeacon.Tag = null;

            if (lvBits.FocusedItem == null)
                return;

            BitsItemTag tag = lvBits.FocusedItem.Tag as BitsItemTag;
            List<BitDesc.WWW> locations = tag.Description.UniqueLocations;
            if (locations.Count == 1)
            {
                miBitsSetBeacon.Tag = locations[0];
                return;
            }

            foreach (BitDesc.WWW location in locations)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(location.ToString(), null, new EventHandler(miBitsBeaconItem));
                mi.Tag = location;
                miBitsSetBeacon.DropDownItems.Add(mi);
            }
        }

        private void miBitsBeaconItem(object sender, EventArgs e)
        {
            BitDesc.WWW loc = (sender as ToolStripMenuItem).Tag as BitDesc.WWW;
            if (loc == null || loc.Where == null)
                return;

            SetBeacon(loc.Where);
        }

        private void SetBeacon(MapXY loc)
        {
            Hacker.SetBeacon(loc.Location, loc.Map);
        }

        private void GotoMap(MapXY loc)
        {
            m_main.GotoSheet(loc.Map);
            m_main.SetCursor(m_main.TranslateToInternalMap(loc.Location, m_main.CurrentSheet));
        }

        private void SelectAll()
        {
            lvBits.BeginUpdate();
            foreach (ListViewItem lvi in lvBits.Items)
                lvi.Selected = true;
            lvBits.EndUpdate();
        }

        private void lvBits_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    if (e.Modifiers == Keys.Control)
                    {
                        CopySelected();
                        e.Handled = true;
                    }
                    break;
                case Keys.A:
                    if (e.Modifiers == Keys.Control)
                    {
                        SelectAll();
                        e.Handled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void lvBits_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_bUpdating)
                return;

            if (m_bReadOnly)
                e.NewValue = e.CurrentValue;
        }

        private void miBitsGotoMap_Click(object sender, EventArgs e)
        {
            if (miBitsGotoMap.HasDropDownItems)
                return;

            BitDesc.WWW loc = miBitsGotoMap.Tag as BitDesc.WWW;
            if (loc == null || loc.Where == null)
                return;

            GotoMap(loc.Where);
        }

        private void miBitsGotoMap_DropDownOpening(object sender, EventArgs e)
        {
            miBitsGotoMap.DropDownItems.Clear();
            miBitsGotoMap.Tag = null;

            if (lvBits.FocusedItem == null)
                return;

            BitsItemTag tag = lvBits.FocusedItem.Tag as BitsItemTag;
            List<BitDesc.WWW> locations = tag.Description.UniqueLocations;
            if (locations.Count == 1)
            {
                miBitsGotoMap.Tag = locations[0];
                return;
            }

            foreach (BitDesc.WWW location in locations)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(location.ToString(), null, new EventHandler(miBitsGotoMapItem));
                mi.Tag = location;
                miBitsGotoMap.DropDownItems.Add(mi);
            }
        }

        private void miBitsGotoMapItem(object sender, EventArgs e)
        {
            BitDesc.WWW loc = (sender as ToolStripMenuItem).Tag as BitDesc.WWW;
            if (loc == null || loc.Where == null)
                return;

            GotoMap(loc.Where);
        }

        private void EditBitsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseForm();
        }

        private void UpdateBitIndices()
        {
            lvBits.BeginUpdate();
            foreach (ListViewItem lvi in lvBits.Items)
            {
                BitsItemTag tag = lvi.Tag as BitsItemTag;
                lvi.Text = String.Format("{0}", GetBitIndex(tag.Bit));
            }
            lvBits.EndUpdate();
        }

        private void miBitIndex0to7_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BitIndex = (int)BitIndex.ZeroToSeven;
            OnBitIndexUpdated();
        }

        private void miBitIndex7to0_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BitIndex = (int)BitIndex.SevenToZero;
            OnBitIndexUpdated();
        }

        private void miBitIndexDefaultForGame_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BitIndex = (int)BitIndex.DefaultForGame;
            OnBitIndexUpdated();
        }

        private void OnBitIndexUpdated()
        {
            miBitIndexDefaultForGame.Checked = Properties.Settings.Default.BitIndex == (int)BitIndex.DefaultForGame;
            miBitIndex7to0.Checked = Properties.Settings.Default.BitIndex == (int)BitIndex.SevenToZero;
            miBitIndex0to7.Checked = Properties.Settings.Default.BitIndex == (int)BitIndex.ZeroToSeven;
            UpdateBitIndices();
        }
    }

    public class BitsItemTag
    {
        public int Bit;
        public BitDesc Description;

        public BitsItemTag(int bit, BitDesc desc)
        {
            Bit = bit;
            Description = desc;
        }
    }

    public class BitDesc
    {
        public class WWW
        {
            public enum Action
            {
                None,
                Set,
                Check,
                Clear
            }

            public Action What;
            public string When;
            public MapXY Where;

            public WWW(Action what, string when, MapXY where)
            {
                What = what;
                When = when;
                Where = where;
            }

            public WWW Clone()
            {
                return new WWW(What, When, Where.Clone());
            }

            public WWW Clone(WWW.Action what)
            {
                return new WWW(what, When, Where.Clone());
            }

            public override int GetHashCode()
            {
                if (Where == null)
                    return What.GetHashCode() ^ When.GetHashCode();
                return What.GetHashCode() ^ When.GetHashCode() ^ Where.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                WWW compare = obj as WWW;
                if (compare == null)
                    return false;
                return (What == compare.What && When == compare.When && Where.Equals(compare));
            }

            public override string ToString()
            {
                return String.Format("{0} ({1} when \"{2}\")", Where.ToString(), ActionString(What), When);
            }

            public static string ActionString(Action what)
            {
                switch (what)
                {
                    case Action.Check: return "Check";
                    case Action.Set: return "Set";
                    case Action.Clear: return "Clear";
                    default: return "No Action";
                }
            }
        }

        public List<WWW> Set;
        public List<WWW> Checked;
        public List<WWW> Cleared;

        public List<WWW> Locations
        {
            get
            {
                List<WWW> list = new List<WWW>();
                foreach(List<WWW> lww in new List<WWW>[] { Set, Checked, Cleared })
                    foreach (WWW ww in lww)
                        if (ww != null && ww.Where != null)
                            list.Add(ww);
                return list;
            }
        }

        public List<WWW> UniqueLocations
        {
            get
            {
                HashSet<WWW> set = new HashSet<WWW>();
                foreach (List<WWW> lww in new List<WWW>[] { Set, Checked, Cleared })
                    foreach (WWW ww in lww)
                        if (ww != null && ww.Where != null && !set.Contains(ww))
                            set.Add(ww);
                return set.ToList<WWW>();
            }
        }

        public string SetWhen { get { return CombinedString(Set, true); } }
        public string SetWhere { get { return CombinedString(Set, false); } }
        public string CheckedWhen { get { return CombinedString(Checked, true); } }
        public string CheckedWhere { get { return CombinedString(Checked, false); } }
        public string ClearedWhen { get { return CombinedString(Cleared, true); } }
        public string ClearedWhere { get { return CombinedString(Cleared, false); } }

        public string AllText
        {
            get
            {
                return String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                    SetWhen, SetWhere, CheckedWhen, CheckedWhere, ClearedWhen, ClearedWhere);
            }
        }

        public string CombinedString(List<WWW> list, bool bWhen)
        {
            StringBuilder sb = new StringBuilder();
            int iMapLast = -1;
            string strLastWhen = String.Empty;
            foreach (WWW ww in list)
            {
                if (bWhen)
                {
                    if (!String.IsNullOrWhiteSpace(ww.When) && ww.When != strLastWhen)
                    {
                        sb.AppendFormat("{0}; ", ww.When);
                        strLastWhen = ww.When;
                    }
                }
                else if (ww.Where != null && !ww.Where.IsEmpty)
                {
                    if (ww.Where.Map == iMapLast)
                        sb.AppendFormat("{0},{1}; ", ww.Where.Location.X, ww.Where.Location.Y);
                    else
                        sb.AppendFormat("{0}; ", ww.Where.ToString());
                    iMapLast = ww.Where.Map;
                }
            }

            if (sb.Length > 1)
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public static BitDesc Empty { get { return new BitDesc(String.Empty); } }

        public bool IsEmpty
        {
            get
            {
                return (Set == null || Set.Count == 0 || String.IsNullOrWhiteSpace(Set[0].When)) &&
                       (Checked == null || Checked.Count == 0 || String.IsNullOrWhiteSpace(Checked[0].When)) &&
                       (Cleared == null || Cleared.Count == 0 || String.IsNullOrWhiteSpace(Cleared[0].When));
            }
        }

        public BitDesc()
        {
            InitLists();
        }

        private void InitLists()
        {
            Set = new List<WWW>();
            Checked = new List<WWW>();
            Cleared = new List<WWW>();
        }

        public BitDesc(string setWhen)
        {
            InitLists();
            Set.Add(new WWW(WWW.Action.Set, setWhen, null));
        }

        // Overloads to create a single "set" location
        public BitDesc(string setWhen, MM1Map map, int x, int y) : this(setWhen, new MapXY(map, x, y)) { }
        public BitDesc(string setWhen, MM2Map map, int x, int y) : this(setWhen, new MapXY(map, x, y)) { }
        public BitDesc(string setWhen, MM3Map map, int x, int y) : this(setWhen, new MapXY(map, x, y)) { }
        public BitDesc(string setWhen, MM4Map map, int x, int y) : this(setWhen, new MapXY(map, x, y)) { }
        public BitDesc(string setWhen, MM5Map map, int x, int y) : this(setWhen, new MapXY(map, x, y)) { }

        public BitDesc(string setWhen, params MapXY[] setWhere)
        {
            InitLists();
            foreach(MapXY map in setWhere)
                Set.Add(new WWW(WWW.Action.Set, setWhen, map));
        }

        public BitDesc(string setWhen, MapXY setWhere, string checkedWhen, MapXY checkedWhere)
        {
            InitLists();
            Set.Add(new WWW(WWW.Action.Set, setWhen, setWhere));
            Checked.Add(new WWW(WWW.Action.Check, checkedWhen, checkedWhere));
        }

        // For a bit that is checked and cleared in the same location
        public BitDesc(string setWhen, MapXY setWhere, string checkedWhen, MapXY checkedWhere, string clearedWhen)
        {
            InitLists();
            Set.Add(new WWW(WWW.Action.Set, setWhen, setWhere));
            Checked.Add(new WWW(WWW.Action.Check, checkedWhen, checkedWhere));
            Cleared.Add(new WWW(WWW.Action.Clear, clearedWhen, checkedWhere));
        }

        public BitDesc(string setWhen, MapXY setWhere, string checkedWhen, MapXY checkedWhere, string clearedWhen, MapXY clearedWhere)
        {
            InitLists();
            Set.Add(new WWW(WWW.Action.Set, setWhen, setWhere));
            Checked.Add(new WWW(WWW.Action.Check, checkedWhen, checkedWhere));
            Cleared.Add(new WWW(WWW.Action.Clear, clearedWhen, clearedWhere));
        }

        public static BitDesc ClearOnly(string setWhen, string clearedWhen, MapXY clearedWhere)
        {
            BitDesc bd = new BitDesc(setWhen, MapXY.Empty);
            bd.Cleared.Add(new WWW(WWW.Action.Clear, clearedWhen, clearedWhere));
            return bd;
        }

        public static BitDesc CheckOnly(string checkedWhen, MapXY checkedWhere)
        {
            BitDesc bd = new BitDesc();
            bd.Checked.Add(new WWW(WWW.Action.Check, checkedWhen, checkedWhere));
            return bd;
        }

        public static BitDesc SetClear(string setWhen, MapXY setWhere, string clearedWhen, MapXY clearedWhere = null)
        {
            BitDesc bd = new BitDesc(setWhen, setWhere);
            bd.Cleared.Add(new WWW(WWW.Action.Clear, clearedWhen, clearedWhere == null ? MapXY.Empty : clearedWhere));
            return bd;
        }

        public BitDesc AddChecked(string when, MapXY where)
        {
            Checked.Add(new WWW(WWW.Action.Check, when, where));
            return this;
        }

        public BitDesc AddChecked()
        {
            foreach(WWW ww in Set)
                Checked.Add(ww.Clone(WWW.Action.Check));
            return this;
        }

        public BitDesc AddZero()
        {
            foreach (WWW ww in Set)
                Cleared.Add(ww.Clone(WWW.Action.Clear));
            return this;
        }

        public BitDesc AddCZ()
        {
            foreach (WWW ww in Set)
            {
                Checked.Add(ww.Clone(WWW.Action.Check));
                Cleared.Add(ww.Clone(WWW.Action.Clear));
            }
            return this;
        }

        private WWW.Action ActionFromList(List<WWW> list)
        {
            if (list == Set)
                return WWW.Action.Set;
            if (list == Checked)
                return WWW.Action.Check;
            if (list == Cleared)
                return WWW.Action.Clear;
            return WWW.Action.None;
        }

        private BitDesc Add(WWW.Action what, List<WWW> list, params int[] coordinates)
        {
            string strWhen = Set[0].When;

            for (int i = 0; i < coordinates.Length; i += 2)
            {
                MapXY loc = Set[0].Where.Clone();
                loc.Location.X = coordinates[i];
                loc.Location.Y = coordinates[i + 1];
                list.Add(new WWW(what, strWhen, loc));
            }
            return this;
        }

        public BitDesc AddSet(string when, params MapXY[] where)
        {
            foreach (MapXY map in where)
            {
                Set.Add(new WWW(WWW.Action.Set, when, map));
            }
            return this;
        }

        public BitDesc AddClear(string when, MapXY where)
        {
            Cleared.Add(new WWW(WWW.Action.Clear, when, where));
            return this;
        }

        public BitDesc AddClear(MapXY where)
        {
            if (Cleared.Count < 1)
                Cleared.Add(new WWW(WWW.Action.Clear, String.Empty, where));
            else
                Cleared.Add(new WWW(WWW.Action.Clear, Cleared[Cleared.Count - 1].When, where));
            return this;
        }

        public BitDesc AddChecked(params int[] coordinates)
        {
            return Add(WWW.Action.Check, Checked, coordinates);
        }

        public BitDesc AddCZ(params int[] coordinates)
        {
            Add(WWW.Action.Check, Checked, coordinates);
            Add(WWW.Action.Clear, Cleared, coordinates);
            return this;
        }

        public BitDesc AddZero(params int[] coordinates)
        {
            return Add(WWW.Action.Clear, Cleared, coordinates);
        }

        public BitDesc AddChecked(string when, params MapXY[] where)
        {
            foreach(MapXY map in where)
                Checked.Add(new WWW(WWW.Action.Check, when, map));
            return this;
        }

        public BitDesc AddZero(string when, params MapXY[] where)
        {
            foreach(MapXY map in where)
                Cleared.Add(new WWW(WWW.Action.Clear, when, map));
            return this;
        }

        public BitDesc AddZero(string when)
        {
            Cleared.Add(new WWW(WWW.Action.Clear, when, Set[0].Where));
            return this;
        }

        public BitDesc AddCZ(string when, params MapXY[] where)
        {
            foreach(MapXY map in where)
            {
                Checked.Add(new WWW(WWW.Action.Check, String.Empty, map));
                Cleared.Add(new WWW(WWW.Action.Clear, String.Empty, map.Clone()));
            }
            return this;
        }

        // Overloads to add a single "checked" location
        public BitDesc AddChecked(string when, MM1Map map, int x, int y) { return AddChecked(when, new MapXY(GameNames.MightAndMagic1, (int)map, x, y)); }
        public BitDesc AddChecked(string when, MM2Map map, int x, int y) { return AddChecked(when, new MapXY(GameNames.MightAndMagic2, (int)map, x, y)); }
        public BitDesc AddChecked(string when, MM3Map map, int x, int y) { return AddChecked(when, new MapXY(GameNames.MightAndMagic3, (int)map, x, y)); }
        public BitDesc AddChecked(string when, MM4Map map, int x, int y) { return AddChecked(when, new MapXY(GameNames.MightAndMagic45, (int)map, x, y)); }
        public BitDesc AddChecked(string when, MM5Map map, int x, int y) { return AddChecked(when, new MapXY(GameNames.MightAndMagic45, (int)map + 256, x, y)); }

        // Overloads to add a single "zero" location
        public BitDesc AddZero(string when, MM1Map map, int x, int y) { return AddZero(when, new MapXY(GameNames.MightAndMagic1, (int)map, x, y)); }
        public BitDesc AddZero(string when, MM2Map map, int x, int y) { return AddZero(when, new MapXY(GameNames.MightAndMagic2, (int)map, x, y)); }
        public BitDesc AddZero(string when, MM3Map map, int x, int y) { return AddZero(when, new MapXY(GameNames.MightAndMagic3, (int)map, x, y)); }
        public BitDesc AddZero(string when, MM4Map map, int x, int y) { return AddZero(when, new MapXY(GameNames.MightAndMagic45, (int)map, x, y)); }
        public BitDesc AddZero(string when, MM5Map map, int x, int y) { return AddZero(when, new MapXY(GameNames.MightAndMagic45, (int)map + 256, x, y)); }

        // Overloads to accept a single "set" location and a single "checked" location
        public BitDesc(string setWhen, MM1Map mapSet, int xSet, int ySet, string checkedWhen, MM1Map mapChecked, int xChecked, int yChecked) 
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapChecked, xChecked, yChecked)) { }
        public BitDesc(string setWhen, MM2Map mapSet, int xSet, int ySet, string checkedWhen, MM2Map mapChecked, int xChecked, int yChecked)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapChecked, xChecked, yChecked)) { }
        public BitDesc(string setWhen, MM3Map mapSet, int xSet, int ySet, string checkedWhen, MM3Map mapChecked, int xChecked, int yChecked)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapChecked, xChecked, yChecked)) { }
        public BitDesc(string setWhen, MM4Map mapSet, int xSet, int ySet, string checkedWhen, MM4Map mapChecked, int xChecked, int yChecked)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapChecked, xChecked, yChecked)) { }
        public BitDesc(string setWhen, MM5Map mapSet, int xSet, int ySet, string checkedWhen, MM5Map mapChecked, int xChecked, int yChecked)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapChecked, xChecked, yChecked)) { }

        // Overloads to accept "set" and "checked" as the same location
        public BitDesc(string setWhen, MapXY map, string checkedWhen)
            : this(setWhen, map, checkedWhen, map.Clone()) { }
        public BitDesc(string setWhen, MM1Map mapSet, int xSet, int ySet, string checkedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { }
        public BitDesc(string setWhen, MM2Map mapSet, int xSet, int ySet, string checkedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { }
        public BitDesc(string setWhen, MM3Map mapSet, int xSet, int ySet, string checkedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { }
        public BitDesc(string setWhen, MM4Map mapSet, int xSet, int ySet, string checkedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { }
        public BitDesc(string setWhen, MM5Map mapSet, int xSet, int ySet, string checkedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { }

        // Overloads to accept "set" and "checked" and "cleared" as the same location
        public BitDesc(string setWhen, MM1Map mapSet, int xSet, int ySet, string checkedWhen, string clearedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { AddZero(clearedWhen); }
        public BitDesc(string setWhen, MM2Map mapSet, int xSet, int ySet, string checkedWhen, string clearedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { AddZero(clearedWhen); }
        public BitDesc(string setWhen, MM3Map mapSet, int xSet, int ySet, string checkedWhen, string clearedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { AddZero(clearedWhen); }
        public BitDesc(string setWhen, MM4Map mapSet, int xSet, int ySet, string checkedWhen, string clearedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { AddZero(clearedWhen); }
        public BitDesc(string setWhen, MM5Map mapSet, int xSet, int ySet, string checkedWhen, string clearedWhen)
            : this(setWhen, new MapXY(mapSet, xSet, ySet), checkedWhen, new MapXY(mapSet, xSet, ySet)) { AddZero(clearedWhen); }
    }
}
