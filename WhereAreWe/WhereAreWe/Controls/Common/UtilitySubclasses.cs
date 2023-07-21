using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WhereAreWe
{
    public class PanelBlockMessages : Panel
    {
        public bool ShowContext = true;
        public bool ControlWheelToParent = true;
        public bool BlockNextContextMenu = false;

        public Point LastManualScroll { get; set; }

        protected override void OnScroll(ScrollEventArgs se)
        {
            LastManualScroll = new Point(-AutoScrollPosition.X, -AutoScrollPosition.Y);
            base.OnScroll(se);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_MOUSEWHEEL:
                    if (!ControlWheelToParent)
                        break;
                    if (this.Parent != null)
                        NativeMethods.PostMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
                    return;
                case NativeMethods.WM_CONTEXTMENU:
                    if (!ShowContext)
                        return;
                    if (BlockNextContextMenu)
                    {
                        BlockNextContextMenu = false;
                        return;
                    }
                    break;
                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }

    public class PictureBoxBlockMessages : PictureBox
    {
        public bool ShowContext = true;
        public bool BlockNextContextMenu = false;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_CONTEXTMENU:
                    if (!ShowContext)
                        return;
                    if (BlockNextContextMenu)
                    {
                        BlockNextContextMenu = false;
                        return;
                    }
                    break;
                default:
                    break;
            }

            base.WndProc(ref m);
        }
    }

    public class ComboBoxIgnoreWheel : ComboBox
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL && this.Parent != null && NativeMethods.IsModifierDown())
            {
                NativeMethods.PostMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
            }
            else base.WndProc(ref m);
        }
    }

    public class NumericUpDownIgnoreWheel : NumericUpDown
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL && this.Parent != null && NativeMethods.IsModifierDown())
            {
                NativeMethods.PostMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
            }
            else base.WndProc(ref m);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            int iVal = 1;
            Int32.TryParse(Text, out iVal);
            if (iVal >= Minimum && iVal <= Maximum)
                Value = iVal;
        }

    }

    public class ToolStripIgnoreFocus : ToolStrip
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEACTIVATE && this.CanFocus && !this.Focused)
                this.Focus();

            base.WndProc(ref m);
        }
    }

    public class MenuStripIgnoreFocus : MenuStrip
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEACTIVATE && this.CanFocus && !this.Focused)
            {
                this.Focus();
            }

            base.WndProc(ref m);
        }
    }

    public class TextBoxIgnoreWheel : TextBox
    {
        protected override void WndProc(ref Message m)
        {
            if (ScrollBars != System.Windows.Forms.ScrollBars.None)
            {
                // Wheel is used for scroll bars if present
                base.WndProc(ref m);
                return;
            }
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL && this.Parent != null)
            {
                NativeMethods.PostMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
            }
            else base.WndProc(ref m);
        }
    }

    public class TipListView : DBListView
    {
        private ToolTip m_tip = new ToolTip();
        private Point m_ptLastTip = Point.Empty;
        private Timer m_tipTimer = new Timer();
        private String m_tipText = String.Empty;
        private ListViewItem m_lastItem = null;
        private ListViewItem m_currentItem = null;
        public int TipDuration { get; set; }
        public int TipDelay { get; set; }
        public bool EnableTips = true;

        public TipListView()
        {
            TipDuration = 30000;
            TipDelay = 700;
            m_tipTimer.Interval = 10;
            m_tipTimer.Tick += m_tipTimer_Tick;
            m_tip.SetToolTip(this, "");
        }

        void m_tipTimer_Tick(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;
            m_tipTimer.Stop();
            if (!EnableTips)
                return;
            Point pt = PointToClient(Cursor.Position);
            ListViewHitTestInfo info = HitTest(pt.X, pt.Y);
            if (info.Item != m_currentItem)
                return;
            int iHeight = Cursor.Current == null ? 24 : Cursor.Current.Size.Height * 3 / 4;
            pt.Y += iHeight;
            m_lastItem = m_currentItem;
            m_tip.Show(m_tipText, this, pt, TipDuration);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (IsDisposed || !EnableTips)
                return;
            if (m_ptLastTip != Cursor.Position)
            {
                if (m_lastItem == null || m_currentItem != m_lastItem || !m_tip.Active)
                {
                    m_ptLastTip = Cursor.Position;
                    if (String.IsNullOrWhiteSpace(m_tipText))
                    {
                        m_tip.RemoveAll();
                        m_lastItem = m_currentItem;
                    }
                    else
                    {
                        m_tipTimer.Interval = Math.Max(TipDelay - SystemInformation.MouseHoverTime, 100);
                        m_tipTimer.Start();
                    }
                }
            }
            base.OnMouseHover(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsDisposed || !EnableTips)
                return;
            ListViewHitTestInfo info = HitTest(e.X, e.Y);
            if (info.Item != null)
                m_tipText = info.Item.ToolTipText;
            else
                m_tipText = String.Empty;

            m_currentItem = info.Item;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (IsDisposed || !EnableTips)
                return;
            m_tip.RemoveAll();
            m_lastItem = null;
            base.OnMouseLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (m_tip != null)
            {
                m_tip.RemoveAll();
                m_tip.Dispose();
            }
            if (m_tipTimer != null)
            {
                m_tipTimer.Stop();
                m_tipTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class CreationAssistantControl : HackerBasedUserControl
    {
        public virtual void OnLoad() { }
        public virtual bool UsedAllPoints { get { return true; } }
        public virtual void TimerTick() { }
        public virtual bool TimerEnabled { get { return true; } }
    }

    public class TrainingAssistantControl : HackerBasedUserControl
    {
        public virtual void OnLoad() { }
        public virtual void TimerTick(bool bForce) { }
        public virtual void Closing() { }
    }

    public class TrapsControl : HackerBasedUserControl
    {
        public virtual void OnLoad() { }
        public virtual void SetSearchResults(SearchResults results) { }
        public virtual bool DisarmTrap() { return false; }
    }

    public class TabInputTextBox : TextBox
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
                return true;
            return base.IsInputKey(keyData);
        }
    }
}
