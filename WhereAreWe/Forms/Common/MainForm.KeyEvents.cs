using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WhereAreWe
{
    public partial class MainForm
    {
        bool m_bPreviewControlDown = false;
        bool m_bPreviewShiftDown = false;
        bool m_bPreviewAltDown = false;
        public Action m_lastActionRun = Action.None;

        private bool ProcessTextBoxKey(AnyTextBox tb, Keys keyData)
        {
            if (tb == null)
                return false;

            switch (keyData)
            {
                case (Keys.Enter):
                    if (tb == tbNote || tb == tbSymbol)
                    {
                        FinishNote();
                        return true;
                    }
                    break;
                case (Keys.A | Keys.Control):
                    tb.SelectionStart = 0;
                    tb.SelectionLength = tb.Text.Length;
                    return true;
                case (Keys.C | Keys.Control):
                case (Keys.Insert | Keys.Control):
                    tb.Copy();
                    return true;
                case (Keys.V | Keys.Control):
                case (Keys.Insert | Keys.Shift):
                    tb.Paste();
                    return true;
                case (Keys.Enter | Keys.Control):
                    if (tbNote.Focused)
                    {
                        Global.InsertText(tbNote, "\r\n");
                        return true;
                    }
                    break;
                case (Keys.Escape):
                    if (tb == tbNote || tb == tbSymbol)
                    {
                        CancelNote();
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }

        public AnyTextBox FocusedTextBox
        {
            get
            {
                foreach (AnyTextBox tb in AnyTextBox.FromControls(tseTitle.TextBox, tbSymbol, tbNote))
                    if (tb.Focused && !tb.ReadOnly)
                        return tb;
                return null;
            }
        }

        public bool InputControlFocused { get { return FocusedTextBox != null; } }

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
                        if (Global.FormVisible(m_formEditLabels) &&
                            CurrentSheet != null &&
                            m_formEditLabels.OnMainCmdKey(keyData, CurrentSheet.SquareSize))
                            return true; // do not let the dialog process the key
                        break;
                    default:
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void CheckModifierKeys()
        {
            // If focus is taken away from the program while a key such as Control is down, the WM_KEYUP event may not be processed
            // This will clear the preview keys if they keys are not actually down.

            m_bPreviewControlDown = NativeMethods.IsControlDown();
            m_bPreviewShiftDown = NativeMethods.IsShiftDown();
            m_bPreviewAltDown = NativeMethods.IsAltDown();
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_KEYDOWN)
                m_lastActionRun = Action.None;

            switch ((Keys) m.WParam)
            {
                case Keys.ControlKey:
                    m_bPreviewControlDown = (m.Msg == NativeMethods.WM_KEYDOWN);
                    return base.ProcessKeyPreview(ref m);   // We don't need to check everything when modifier keys are pressed or released
                case Keys.ShiftKey:
                    m_bPreviewShiftDown = (m.Msg == NativeMethods.WM_KEYDOWN);
                    return base.ProcessKeyPreview(ref m);   // We don't need to check everything when modifier keys are pressed or released
                case Keys.Menu:
                    m_bPreviewAltDown = (m.Msg == NativeMethods.WM_KEYDOWN);
                    return base.ProcessKeyPreview(ref m);   // We don't need to check everything when modifier keys are pressed or released
                default:
                    break;
            }

            if (m.Msg == NativeMethods.WM_KEYDOWN || m.Msg == NativeMethods.WM_SYSKEYDOWN)
            {
                Keys keyData = (Keys)m.WParam;
                Keys keyOnly = keyData;
                if (m_bPreviewControlDown)
                    keyData |= Keys.Control;
                if (m_bPreviewShiftDown)
                    keyData |= Keys.Shift;
                if (m_bPreviewAltDown)
                    keyData |= Keys.Alt;

                if (InputControlFocused && !m_bPreviewAltDown)
                {
                    // Don't process certain shortcuts when an input control is focused
                    if (!m_bPreviewControlDown)
                        return base.ProcessKeyPreview(ref m);   // Key or Shift+Key in an input field -> no shortcut
                    switch(keyOnly)
                    {
                        case Keys.Home:
                        case Keys.End:
                        case Keys.Insert:
                        case Keys.Delete:
                        case Keys.Up:
                        case Keys.Down:
                        case Keys.Left:
                        case Keys.Right:
                            return base.ProcessKeyPreview(ref m);   // These key combinations (plus ctrl/shift/both) do things in textboxes -> no shortcut
                        default:
                            break;  // A key combination that's okay to use as a shortcut while in a textbox
                    }
                    switch(keyData)
                    {
                        case Keys.Control | Keys.Z:
                        case Keys.Control | Keys.X:
                        case Keys.Control | Keys.C:
                        case Keys.Control | Keys.V:
                        case Keys.Control | Keys.A:
                            return base.ProcessKeyPreview(ref m);   // These key combinations do things in textboxes -> no shortcut
                        default:
                            break;  // A key combination that's okay to use as a shortcut while in a textbox
                    }
                }

                switch (keyData)
                {
                    case Keys.Escape:
                        OnEscapePressed();
                        break;
                    case Keys.A | Keys.Control:
                        OnSelectAll();
                        break;
                    default:
                        break;
                }

                if (!KeyboardHook.IsActive())
                {
                    // Process the list of user-specified shortcuts
                    InputOption input = Properties.Settings.Default.Shortcuts.ActionForDialogKey(keyData);
                    if (input != null && input.Action != Action.None)
                    {
                        // Tell the normal menu processing not to run this action (in order to avoid activating it twice)
                        m_lastActionRun = input.Action;
                        return PerformAction(input.Action);
                    }
                }

                m_lastActionRun = Action.None;
            }

            return base.ProcessKeyPreview(ref m);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (EditingNote || FocusedTextBox != null)
            {
                return ProcessTextBoxKey(FocusedTextBox, keyData);
            }

            if (tseTitle.Focused && (keyData & Keys.Control) == 0)
                return false;

            switch (keyData)
            {
                case (Keys.Enter):
                    if (Mode == BlockMode.Keyboard)
                    {
                        BeginEditNote(CurrentSheet.Cursor);
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        private bool ProcessEditKey(Keys keyData)
        {
            UpdateEditMenu();
            switch (keyData)
            {
                case (Keys.Z | Keys.Control):
                    if (miEditUndo.Enabled)
                        MenuEditUndo();
                    return true;
                case (Keys.Y | Keys.Control):
                    if (miEditRedo.Enabled)
                        MenuEditRedo();
                    return true;
                case (Keys.X | Keys.Control):
                    if (miEditCut.Enabled)
                        MenuEditCut();
                    return true;
                case (Keys.C | Keys.Control):
                    if (miEditCopy.Enabled)
                        MenuEditCopy();
                    return true;
                case (Keys.V | Keys.Control):
                    if (miEditPaste.Enabled)
                        MenuEditPaste();
                    return true;
                default:
                    break;
            }
            return false;
        }

        private bool ProcessSheetKey(Keys keyData)
        {
            UpdateSheetMenu();
            switch (keyData)
            {
                case (Keys.PageUp | Keys.Control):
                    if (miSheetPrevious.Enabled)
                        ProcessSheetPrevious();
                    return true;
                case (Keys.PageDown | Keys.Control):
                    if (miSheetNext.Enabled)
                        ProcessSheetNext();
                    return true;
                default:
                    break;
            }
            return false;
        }

        private HashSet<Keys> m_keysNeedKeyup = new HashSet<Keys>();

        public bool OnLLKeyUp(Keys key, bool[] keysDown)
        {
            if (m_keysNeedKeyup.Contains(key))
            {
                m_keysNeedKeyup.Remove(key);
                return true;
            }
            return false;
        }

        public bool OnLLKeyDown(Keys key, bool[] keysDown)
        {
            if (Global.Debug)
            {
                if (key == Keys.Scroll)
                {
                    ReadAutosizeMap(null, null, MapReplaceMode.WallIcons);
                    //miDebugReadMapAuto_Click(null, null);
                    miDebugDumpScripts_Click(null, null);
                }
            }

            if (!m_hacker.IsGameFocused && !NativeMethods.ApplicationIsActivated())
                return false;

            InputOption input = m_shortcuts.InputOptionForKeys(key, keysDown);
            if (input == null)
                return false;
            if (input.Action == Action.None)
                return false;

            bool bAppFocused = NativeMethods.ApplicationIsActivated();
            if ((EditingNote || FocusedTextBox != null) && bAppFocused)
                return false;

            if (!input.Global)
            {
                if (NativeMethods.GetForegroundWindow() != Handle)  // Only matches the main map window, not other tool windows
                    return false;
            }

            if (input.Action < Action.LastMenuItem && bAppFocused)
                return false;   // Let the normal Windows menus handle it
            bool bNeedKeyUp = PerformAction(input.Action);
            if (bNeedKeyUp && !m_keysNeedKeyup.Contains(key))
                m_keysNeedKeyup.Add(key);
            return bNeedKeyUp;
        }
    }
}
