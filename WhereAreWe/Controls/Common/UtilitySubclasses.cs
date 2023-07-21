using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.CodeDom;

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

    public class AnyTextBox
    {
        public Control Box;
        public RichTextBox AsRich => Box as RichTextBox;
        public TextBox AsText => Box as TextBox;
        public bool IsRich => Box is RichTextBox;

        public static AnyTextBox[] FromControls(params Control[] controls)
        {
            AnyTextBox[] array = new AnyTextBox[controls.Length];
            for (int i = 0; i < controls.Length; i++)
                array[i] = new AnyTextBox(controls[i]);
            return array;
        }

        public AnyTextBox(Control ctrl)
        {
            if (!(ctrl is RichTextBox || ctrl is TextBox))
                throw new InvalidCastException("Only TextBox and RichTextBox controls are accepted as AnyTextBox values");
            Box = ctrl;
        }

        public bool Focused => Box.Focused;

        public bool ReadOnly
        {
            get => ((TextBoxBase)Box).ReadOnly;
            set { ((TextBoxBase)Box).ReadOnly = value; }
        }

        public int SelectionStart
        {
            get => ((TextBoxBase)Box).SelectionStart;
            set { ((TextBoxBase)Box).SelectionStart = value; }
        }

        public int SelectionLength
        {
            get => ((TextBoxBase)Box).SelectionLength;
            set { ((TextBoxBase)Box).SelectionLength = value; }
        }

        public string Text
        {
            get => ((TextBoxBase)Box).Text;
            set { ((TextBoxBase)Box).Text = value; }
        }

        public void Copy() => ((TextBoxBase)Box).Copy();
        public void Paste() => ((TextBoxBase)Box).Paste();
        public void ScrollToCaret() => ((TextBoxBase)Box).ScrollToCaret();

        public static bool operator==(AnyTextBox obj1, Control obj2)
        {
            return (null == obj1 && null == obj2) || (obj1 != null && obj1.Box == obj2);
        }

        public static bool operator!=(AnyTextBox obj1, Control obj2)
        {
            return (null != obj1 && obj1.Box != obj2);
        }

        public override bool Equals(object obj)
        {
            return Box != null && Box.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Box == null ? 0 : Box.GetHashCode();
        }
    }

    public class TextBoxIgnoreWheel : RichTextBox
    {
        protected override void WndProc(ref Message m)
        {
            if (ScrollBars != RichTextBoxScrollBars.None)
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

    public class RichTextBoxContextMenu : ContextMenuStrip
    {
        private RichTextBox m_rtb;
        private ToolStripMenuItem miNotesSpoiler;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem miNotesCut;
        private ToolStripMenuItem miNotesCopy;
        private ToolStripMenuItem miNotesPaste;
        private ToolStripMenuItem miNotesDelete;
        private ToolStripMenuItem miNotesUndo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem miNotesSelectAll;
        private ToolStripMenuItem miNotesRemoveAllSpoilerTags;

        public RichTextBoxContextMenu(RichTextBox rtb)
        {
            miNotesSpoiler = new ToolStripMenuItem();
            miNotesRemoveAllSpoilerTags = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            miNotesUndo = new ToolStripMenuItem();
            miNotesCut = new ToolStripMenuItem();
            miNotesCopy = new ToolStripMenuItem();
            miNotesPaste = new ToolStripMenuItem();
            miNotesDelete = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            miNotesSelectAll = new ToolStripMenuItem();
            miNotesSpoiler = new ToolStripMenuItem();
            miNotesRemoveAllSpoilerTags = new ToolStripMenuItem();

            m_rtb = rtb;
            // 
            // miNotesSpoiler
            // 
            miNotesSpoiler.Name = "miNotesSpoiler";
            miNotesSpoiler.Size = new Size(170, 22);
            miNotesSpoiler.Text = "&Add or remove spoiler";
            miNotesSpoiler.Click += new EventHandler(this.miNotesSpoiler_Click);
            // 
            // miNotesRemoveAllSpoilerTags
            // 
            miNotesRemoveAllSpoilerTags.Name = "miNotesRemoveAllSpoilerTags";
            miNotesRemoveAllSpoilerTags.Size = new Size(170, 22);
            miNotesRemoveAllSpoilerTags.Text = "&Remove all spoiler tags";
            miNotesRemoveAllSpoilerTags.Click += new EventHandler(this.miNotesRemoveAllSpoilerTags_Click);
            //
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(167, 6);
            // 
            // miNotesUndo
            // 
            miNotesUndo.Name = "miNotesUndo";
            miNotesUndo.Size = new Size(170, 22);
            miNotesUndo.Text = "Un&do";
            miNotesUndo.Click += new EventHandler(miNotesUndo_Click);
            // 
            // miNotesCut
            // 
            miNotesCut.Name = "miNotesCut";
            miNotesCut.Size = new Size(170, 22);
            miNotesCut.Text = "C&ut";
            miNotesCut.Click += new EventHandler(miNotesCut_Click);
            // 
            // miNotesCopy
            // 
            miNotesCopy.Name = "miNotesCopy";
            miNotesCopy.Size = new Size(170, 22);
            miNotesCopy.Text = "&Copy";
            miNotesCopy.Click += new EventHandler(miNotesCopy_Click);
            // 
            // miNotesPaste
            // 
            miNotesPaste.Name = "miNotesPaste";
            miNotesPaste.Size = new Size(170, 22);
            miNotesPaste.Text = "&Paste";
            miNotesPaste.Click += new EventHandler(miNotesPaste_Click);
            // 
            // miNotesDelete
            // 
            miNotesDelete.Name = "miNotesDelete";
            miNotesDelete.Size = new Size(170, 22);
            miNotesDelete.Text = "&Delete";
            miNotesDelete.Click += new EventHandler(miNotesDelete_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(167, 6);
            // 
            // miNotesSelectAll
            // 
            miNotesSelectAll.Name = "miNotesSelectAll";
            miNotesSelectAll.Size = new Size(170, 22);
            miNotesSelectAll.Text = "&Select all";
            miNotesSelectAll.Click += new EventHandler(miNotesSelectAll_Click);

            Items.AddRange(new ToolStripItem[] {
                miNotesSpoiler,
                miNotesRemoveAllSpoilerTags,
                toolStripSeparator1,
                miNotesUndo,
                miNotesCut,
                miNotesCopy,
                miNotesPaste,
                miNotesDelete,
                toolStripSeparator2,
                miNotesSelectAll});
            Name = "cmSelection";
            ShowImageMargin = false;
            Size = new Size(171, 214);
            Opening += new CancelEventHandler(this.cmNotes_Opening);
        }

        private void miNotesSpoiler_Click(object sender, EventArgs e)
        {
            if (m_rtb.SelectionLength == 0)
                return; // Nothing to spoiler/unspoiler

            int iStart = m_rtb.SelectionStart;
            int iEnd = m_rtb.SelectionLength + iStart;

            string strText = m_rtb.Text;
            while (iStart >= 0 && iStart < m_rtb.SelectionLength + m_rtb.SelectionStart && Char.IsWhiteSpace(strText[iStart]))
                iStart++;
            while (iEnd > iStart && Char.IsWhiteSpace(strText[iEnd-1]))
                iEnd--;
            if (iStart > 0 && strText[iStart - 1] == Global.SpoilerStart)
                iStart--;
            if (iEnd < strText.Length - 1 && strText[iEnd] == Global.SpoilerEnd)
                iEnd++;
            if (iStart >= iEnd)
                return; // Nothing but whitespace

            m_rtb.SelectionStart = iStart;
            m_rtb.SelectionLength = iEnd - iStart;
            if (strText[iStart] == Global.SpoilerStart && strText[iEnd - 1] == Global.SpoilerEnd)
            {
                m_rtb.SelectedText = m_rtb.SelectedText.Substring(1, m_rtb.SelectionLength - 2);
            }
            else
            {
                string strSel = m_rtb.SelectedText;
                int iEndSel = strSel.Length;
                while (iEndSel > 0 && Char.IsWhiteSpace(strSel[iEndSel - 1]))
                    iEndSel--;
                if (iEndSel != strSel.Length)
                    m_rtb.SelectedText = String.Format("{0}{1}{2}{3}", Global.SpoilerStart, strSel.Substring(0, iEndSel), Global.SpoilerEnd, strSel.Substring(iEndSel));
                else
                    m_rtb.SelectedText = String.Format("{0}{1}{2}", Global.SpoilerStart, strSel, Global.SpoilerEnd);
            }
        }

        private void miNotesCut_Click(object sender, EventArgs e)
        {
            m_rtb.Cut();
        }

        private void miNotesCopy_Click(object sender, EventArgs e)
        {
            m_rtb.Copy();
        }

        private void miNotesPaste_Click(object sender, EventArgs e)
        {
            m_rtb.Paste();
        }

        private void miNotesDelete_Click(object sender, EventArgs e)
        {
            m_rtb.SelectedText = "";
        }

        private void cmNotes_Opening(object sender, CancelEventArgs e)
        {
            miNotesCut.Enabled = !m_rtb.ReadOnly && m_rtb.SelectionLength > 0;
            miNotesCopy.Enabled = m_rtb.SelectionLength > 0;
            miNotesDelete.Enabled = !m_rtb.ReadOnly && m_rtb.SelectionLength > 0;
            miNotesPaste.Enabled = !m_rtb.ReadOnly && Clipboard.ContainsText();
            miNotesUndo.Enabled = !m_rtb.ReadOnly && m_rtb.CanUndo;
            miNotesSpoiler.Enabled = !m_rtb.ReadOnly && m_rtb.SelectionLength > 0;
            miNotesRemoveAllSpoilerTags.Enabled = !m_rtb.ReadOnly && m_rtb.Text.IndexOfAny(Global.SpoilerChars) != -1;
        }

        private void miNotesUndo_Click(object sender, EventArgs e)
        {
            m_rtb.Undo();
        }

        private void miNotesSelectAll_Click(object sender, EventArgs e)
        {
            m_rtb.SelectAll();
        }

        private void miNotesRemoveAllSpoilerTags_Click(object sender, EventArgs e)
        {
            m_rtb.Text = m_rtb.Text.Replace(Global.SpoilerStart.ToString(), "").Replace(Global.SpoilerEnd.ToString(), "");
        }
    }
}
