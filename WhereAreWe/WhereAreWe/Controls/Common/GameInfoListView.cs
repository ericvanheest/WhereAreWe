using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class GameInfoListView : ListView
    {
        private IMain m_main = null;
        private bool m_bUpdatingSize = false;
        private bool m_bShowValue = true;
        private ColumnHeader m_chInfo = new ColumnHeader();
        private ColumnHeader m_chValue = new ColumnHeader();
        private bool m_bFirstUpdate = false;

        public GameInfoListView()
        {
            InitializeComponent();
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ContextMenuStrip = cmGameInfo;
            m_bFirstUpdate = true;
        }

        public bool ValueEnabled
        {
            get { return m_bShowValue; }
            set 
            {
                m_bShowValue = value;
                ResizeColumns();
            }
        }

        public void SetMain(IMain main)
        {
            m_main = main;
            m_bFirstUpdate = true;
        }

        public void CreateColumns()
        {
            if (Columns.Count < 2)
            {
                FullRowSelect = true;
                View = System.Windows.Forms.View.Details;
                m_chInfo.Width = 60;
                m_chValue.Width = 60;
                m_chInfo.Text = "Info";
                m_chValue.Text = "Value";
                Columns.Add(m_chInfo);
                Columns.Add(m_chValue);
                m_bFirstUpdate = true;
            }
        }

        public void UpdateUI(List<GameInfoItem> items)
        {
            if (items == null)
            {
                Items.Clear();
                return;
            }

            CreateColumns();


            if (items.Count != Items.Count)
            {
                BeginUpdate();
                Items.Clear();
                foreach (GameInfoItem tag in items)
                    AddItem(tag);
                EndUpdate();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Global.UpdateText(Items[i], items[i].Description);
                    Global.UpdateText(Items[i].SubItems[1], items[i].ValueString);
                    Items[i].Tag = items[i];
                }
            }

            if (m_bFirstUpdate)
            {
                m_bFirstUpdate = false;
                BeginUpdate();
                ResizeColumns();
                EndUpdate();
            }
        }

        private void ResizeColumns()
        {
            Global.SizeHeadersAndContent(this);
            if (!m_bShowValue)
                Columns[1].Width = 0;
        }

        private void AddItem(GameInfoItem item)
        {
            ListViewItem lvi = new ListViewItem(item.Description);
            lvi.SubItems.Add(String.Format("{0}", item.ValueString));
            lvi.Tag = item;
            Items.Add(lvi);
        }

        private void miActiveEdit_Click(object sender, EventArgs e)
        {
            if (m_main == null || m_main.Hacker == null)
                return;

            if (FocusedItem == null)
                return;

            GameInfoItem tag = FocusedItem.Tag as GameInfoItem;

            if (tag.Style != GameInfoItem.ShowStyle.Editable)
                return;

            AttributeEditForm formAttr = new AttributeEditForm();
            MapSelectionForm formMap = new MapSelectionForm();
            formMap.SetMain(m_main, WindowType.MapSelection);
            
            switch (tag.Type)
            {
                case DataType.UInt32:
                    formAttr.Attribute = new EditableAttribute((UInt32)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.UInts[0];
                    break;
                case DataType.Int32:
                    formAttr.Attribute = new EditableAttribute((Int32)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.Ints[0];
                    break;
                case DataType.Time:
                case DataType.UInt16:
                    formAttr.Attribute = new EditableAttribute((UInt16)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.UShorts[0];
                    break;
                case DataType.Int16:
                case DataType.CopyInt16:
                    formAttr.Attribute = new EditableAttribute((Int16)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.Shorts[0];
                    break;
                case DataType.Boolean: // Toggle boolean values without invoking a UI
                    if (tag.Value is bool)
                        tag.Value = !((bool)tag.Value);
                    else
                        tag.Value = (UInt32)(~Convert.ToUInt32(tag.Value) & tag.Mask);
                    break;
                case DataType.Byte:
                case DataType.Depth:
                case DataType.Facing:
                    formAttr.Attribute = new EditableAttribute((byte)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.Bytes[0];
                    break;
                case DataType.Bits:
                    EditBitsForm formEditBits = new EditBitsForm(null, TopLevelControl);
                    formEditBits.ReadOnly = !Global.Cheats;
                    formEditBits.SetMain(m_main, WindowType.EditBits);
                    if (tag.Value is UInt32)
                        formEditBits.Bytes = BitConverter.GetBytes((UInt32)tag.Value);
                    else
                        formEditBits.Bytes = (byte[])tag.Value;
                    formEditBits.DescriptionFunction = tag.BitDescFunction;
                    if (tag.BitDescFunction == MM45Bits.WorldDescription || tag.BitDescFunction == MM45Bits.QuestDescription)
                        formEditBits.AutosizeColumns = new int[] { 2, 3 };
                    else if (tag.BitDescFunction == MM45Bits.CharDescription)
                        formEditBits.AutosizeColumns = new int[] { 2, 3, 4, 5 };
                    else if (tag.BitDescFunction == MM45Bits.MapFlagsDescription)
                        formEditBits.AutosizeColumns = new int[] { 2 };
                    if (formEditBits.ShowDialog() == DialogResult.Cancel || !Global.Cheats)
                        return;
                    tag.Value = formEditBits.Bytes;
                    break;
                case DataType.Bytes:
                    EditBytesForm formEditBytes = new EditBytesForm(TopLevelControl);
                    formEditBytes.ReadOnly = !Global.Cheats;
                    if (formEditBytes.ReadOnly)
                        formEditBytes.Title = "View Bytes";
                    formEditBytes.ForceLength = true;
                    formEditBytes.SetDescriptionFunction(tag.ByteDescFunction);
                    formEditBytes.Bytes = (byte[])tag.Value;
                    if (formEditBytes.ShowDialog() == DialogResult.Cancel || !Global.Cheats)
                        return;
                    tag.Value = formEditBytes.Bytes;
                    break;
                case DataType.Map16:
                case DataType.Map8:
                    formMap.Exit = new MMExit(MMExitDirection.None, Convert.ToUInt16(tag.Value));
                    if (formMap.ShowDialog() == DialogResult.OK)
                    {
                        if (tag.Type == DataType.Map8)
                            tag.Value = (byte)formMap.Exit.Map;
                        else
                            tag.Value = (UInt16)formMap.Exit.Map;
                    }
                    break;
                case DataType.MapAndPoint8:
                    UInt16 map = Convert.ToUInt16(tag.Value);
                    formMap.Exit = new MMExit(MMExitDirection.None, map >> 8, Global.PointFromByte((byte) map));
                    if (formMap.ShowDialog() == DialogResult.OK)
                        tag.Value = (UInt16)(formMap.Exit.Map | (formMap.Exit.Point.X << 8) | (formMap.Exit.Point.Y << 12));
                    break;
                case DataType.MM1MapAndPoint16:
                    byte[][] bytesMP16 = (byte[][])tag.Value;
                    formMap.Exit = MMExit.FromMM1MapAndPoint(bytesMP16);
                    if (formMap.ShowDialog() == DialogResult.OK)
                        tag.Value = formMap.Exit.MM1MapAndPoint;
                    break;
                case DataType.MM2MapAndPoint8:
                    byte[][] bytesMP8 = (byte[][])tag.Value;
                    formMap.Exit = new MMExit(MMExitDirection.None, bytesMP8[0][0], Global.PointFromByte(bytesMP8[1][0]));
                    if (formMap.ShowDialog() == DialogResult.OK)
                        tag.Value = formMap.Exit.MM2MapAndPoint;
                    break;
                case DataType.Point8:
                    byte bP8 = (byte) tag.Value;
                    formMap.Exit = new MMExit(MMExitDirection.None, -1, Global.PointFromByte(bP8));
                    if (formMap.ShowDialog() == DialogResult.OK)
                        tag.Value = (byte)((formMap.Exit.Point.X & 0xf) | (formMap.Exit.Point.Y << 4));
                    break;
                case DataType.Point16:
                    byte[] bP16 = (byte[]) tag.Value;
                    formMap.Exit = new MMExit(MMExitDirection.None, -1, new Point(bP16[0], bP16[1]));
                    if (formMap.ShowDialog() == DialogResult.OK)
                        tag.Value = new byte[] { (byte) formMap.Exit.Point.X, (byte) formMap.Exit.Point.Y };
                    break;
                case DataType.UShorts:
                    formAttr.Attribute = new EditableAttribute(null, (ushort[])tag.Value, null, null, null, null, null, null, null);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.UShorts;
                    break;
                case DataType.Exploration:
                    // Can't edit exploration values directly
                    break;
                default:
                    Global.InternalError(String.Format("Unknown data type {0}", (int)tag.Type));
                    return;
            }

            m_main.Hacker.SetGameInfoItem(tag);
        }

        private void cmEdit_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedItems.Count > 0 && ((GameInfoItem)SelectedItems[0].Tag).Style != GameInfoItem.ShowStyle.Editable)
            {
                e.Cancel = true;
                return;
            }

            if (SelectedItems.Count > 0)
            {
                miInfoEdit.Enabled = true;
                DataType type = ((GameInfoItem)SelectedItems[0].Tag).Type;

                if (!Global.Cheats)
                {
                    if (type == DataType.Bits || type == DataType.Bytes)
                        miInfoEdit.Text = "Vi&ew";
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                    miInfoEdit.Text = type == DataType.Boolean ? "Toggl&e" : "&Edit";
            }
            else
                miInfoEdit.Enabled = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (m_bUpdatingSize)
                return;

            m_bUpdatingSize = true;
            BeginUpdate();
            ResizeColumns();
            EndUpdate();
            Refresh();
            m_bUpdatingSize = false;
        }

    }
}
