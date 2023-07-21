using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Globalization;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace WhereAreWe
{
    public class DraggableListView : TipListView
    {
        public delegate void ItemsRearrangedEventHandler(object sender, EventArgs e);
        public bool m_bDragging = false;
        private Timer m_timerAutoScroll = new Timer();
        public bool EnableDragging = true;

        public event ItemsRearrangedEventHandler ItemsRearranged;

        public DraggableListView()
        {
            EnableTips = false;
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            AllowDrop = true;
            m_timerAutoScroll.Tick += m_timerAutoScroll_Tick;
            m_timerAutoScroll.Interval = 200;
        }

        void m_timerAutoScroll_Tick(object sender, EventArgs e)
        {
            if (IsDisposed || !m_bDragging)
            {
                m_timerAutoScroll.Stop();
                return;
            }

            if (!EnableDragging)
                return;

            Point ptClientCursor = PointToClient(Cursor.Position);
            if (!ClientRectangle.Contains(ptClientCursor))
            {
                int iCount = 1;
                if (ptClientCursor.Y > ClientRectangle.Bottom)
                {
                    if (ptClientCursor.Y - ClientRectangle.Bottom > 80)
                        iCount = 20;
                    else  if (ptClientCursor.Y - ClientRectangle.Bottom > 20)
                        iCount = 5;

                    ListViewItem lviBottom = BottomItem;
                    if (lviBottom != null && lviBottom.Index < Items.Count - 1)
                        EnsureVisible(Math.Min(Items.Count - 1, lviBottom.Index + iCount));
                }
                else if (ptClientCursor.Y < ClientRectangle.Top && TopItem.Index > 0)
                {
                    if (ClientRectangle.Top - ptClientCursor.Y > 80)
                        iCount = 20;
                    else if (ClientRectangle.Top - ptClientCursor.Y > 20)
                        iCount = 5;
                    EnsureVisible(Math.Max(0, TopItem.Index - iCount));
                }
                return;
            }
        }

        protected virtual void OnRearranged(EventArgs e)
        {
            if (ItemsRearranged != null)
                ItemsRearranged(this, e);
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14 || !EnableDragging) // WM_ERASEBKGND
                base.OnNotifyMessage(m);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (SelectedItems.Count == 0)
                return;
            base.OnDragEnter(e);
            if (!EnableDragging)
                return;
            e.Effect = DragDropEffects.Move;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (!EnableDragging)
                return;
            Point ptDrop = PointToClient(new Point(e.X, e.Y));
            ListViewItem lvi = GetItemAt(ptDrop.X, ptDrop.Y);
            m_bDragging = false;
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (!EnableDragging)
                return;
            if (SelectedItems.Count == 0)
                return;
            ListViewItemSorter = null;
            Capture = true;
            m_bDragging = true;
            m_timerAutoScroll.Start();
            DoDragDrop(FocusedItem, DragDropEffects.Move);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!EnableDragging)
                return;
            if (SelectedItems.Count == 0)
                return;
            Point ptDrop = PointToClient(new Point(e.X, e.Y));
            ListViewItem lvi = GetItemAt(ptDrop.X, ptDrop.Y);
            e.Effect = DragDropEffects.Move;
            MoveItems(e, lvi == null ? -1 : lvi.Index);
        }

        public ListViewItem BottomItem
        {
            get
            {
                ListViewItem lvi = TopItem;
                for (int i = TopItem.Index + 1; i < Items.Count; i++)
                {
                    if (ClientRectangle.Contains(Items[i].Bounds))
                        lvi = Items[i];
                    else
                        break;
                }
                return lvi;
            }
        }

        private void MoveItems(DragEventArgs e, int iNewIndex)
        {
            if (SelectedItems.Count < 1)
                return;

            if (!e.Data.GetDataPresent(typeof(ListViewItem)))
                return;

            ListViewItem lviFocused = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;

            if (TopItem == null)
                return;

            if (iNewIndex == -1)
            {
                if (SelectedItems[0].Index != TopItem.Index)
                    iNewIndex = Items.Count;
            }

            int iDelta = lviFocused.Index - (iNewIndex == -1 ? 0 : iNewIndex);

            if (iDelta > 0 && SelectedItems[0].Index < iDelta)
                iDelta = SelectedItems[0].Index;
            else if (iDelta < 0 && SelectedItems[SelectedItems.Count - 1].Index >= Items.Count + iDelta)
                iDelta = SelectedItems[SelectedItems.Count - 1].Index - (Items.Count - 1);

            if (iDelta == 0)
                return;

            int iCurrent = 0;
            if (iDelta < 0)
                iCurrent = SelectedItems.Count - 1;

            BeginUpdate();
            do
            {
                ListViewItem lvi = SelectedItems[iCurrent];
                int iNewPosition = lvi.Index - iDelta;

                lvi.Remove();
                if (iNewPosition < 0)
                    iNewPosition = 0;
                if (iNewPosition >= Items.Count)
                    Items.Add(lvi);
                else
                    Items.Insert(iNewPosition, lvi);

                iCurrent += Math.Sign(iDelta);
            } while (iCurrent >= 0 && iCurrent < SelectedItems.Count);

            lviFocused.Focused = true;
            EndUpdate();

            OnRearranged(new EventArgs());
        }
    }

    public class DBListView : System.Windows.Forms.ListView
    {
        public DBListView()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public Point ScrollPosition
        {
            get
            {
                if (IsDisposed)
                    return Point.Empty;
                return new Point(
                    NativeMethods.GetScrollPos(Handle, NativeMethods.SB_HORZ),
                    NativeMethods.GetScrollPos(Handle, NativeMethods.SB_VERT));
            }

            set
            {
                if (IsDisposed)
                    return;
                if (Items.Count > 0)
                {
                    int iItemHeight = GetItemRect(0).Height;
                    NativeMethods.SendMessage(Handle, NativeMethods.LVM_SCROLL, (IntPtr)value.X, (IntPtr)(value.Y * iItemHeight));
                }
            }
        }

        public void RemoveSmallImageList()
        {
            SmallImageList = null;
            RecreateHandle();
        }
    }
}
