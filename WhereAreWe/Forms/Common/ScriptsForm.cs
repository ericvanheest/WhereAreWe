using System;
using System.Collections;
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
    public partial class ScriptsForm : HackerBasedForm
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private ScriptInfo m_info = null;

        private MemoryBytes m_bytesLast = null;
        private int m_iLastSelectedLine = -1;
        private Point m_ptSelect = Global.NullPoint;
        private int m_idxSelect = -1;
        private GameScripts m_lastScripts = null;
        private string m_strLastFileLoaded = String.Empty;
        private int m_iLastMap = -1;
        private bool m_bNoUpdateSelLine = false;
        private bool m_bManuallySelected = false;
        private FindBox m_findBoxScript = null;
        private FindBox m_findBoxSelected = null;

        public ScriptsForm()
        {
            InitializeComponent();
        }

        protected override bool ShowWithoutActivation { get { return true; } }

        protected override void OnMainSet()
        {
            ForceRefresh();
            Global.RestartTimer(timerUpdateMemory);
        }

        protected override void OnMainSetAgain()
        {
            Global.RestartTimer(timerUpdateMemory);
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        public override int[] Splitters
        {
            get
            {
                return new int[] { splitContainer1.SplitterDistance };
            }

            set
            {
                if (value == null || value.Length == 0 || value[0] == -1)
                    return;

                Global.SetSplitterDistance(splitContainer1, value[0]);
            }
        }

        public override void SetParameter(object param)
        {
            if (param is Point)
                SelectPosition((Point)param);
        }

        public void SelectPosition(Point pt, int idx = -1)
        {
            m_ptSelect = pt;
            m_bManuallySelected = true;
            m_idxSelect = idx;
            lvScripts.SelectedItems.Clear();
            ForceRefresh();
        }

        private bool CheckSelected()
        {
            if (m_ptSelect != Global.NullPoint)
            {
                foreach (ListViewItem lvi in lvScripts.Items)
                {
                    GameScript script = lvi.Tag as GameScript;
                    if (script.Location == m_ptSelect && (m_idxSelect == -1 || script.Index == m_idxSelect))
                    {
                        lvScripts.EnsureVisible(lvi.Index);
                        m_bNoUpdateSelLine = true;
                        lvi.Selected = true;
                        m_bNoUpdateSelLine = false;
                        m_ptSelect = Global.NullPoint;
                        m_idxSelect = -1;
                        return true;
                    }
                }

                m_ptSelect = Global.NullPoint;
                m_idxSelect = -1;
            }
            return false;
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (m_bDestroy)
                return;

            if (!Visible)
                return;

            if (!miAllScriptsGame.Checked)
                return;     // File takes precedence until the menu item is unchecked

            if (Hacker == null || !Hacker.Running)
            {
                Text = "Scripts (no running game detected!)";
                return;
            }
            
            MemoryBytes bytes = Hacker.GetScriptBytes();
            if (bytes == null)
                return;

            if (m_bytesLast != null && Global.Compare(bytes.Bytes, m_bytesLast.Bytes))
            {
                CheckSelected();
                return;     // No change, no update
            }

            ForceRefresh(bytes);
        }

        private void UpdateUI(GameScripts scripts)
        {
            if (scripts == null)
                return;

            if (chHeaderBytes.Width == 0 && scripts.HasHeaderBytes)
                chHeaderBytes.Width = 70;
            else if (chHeaderBytes.Width > 0 && !scripts.HasHeaderBytes)
                chHeaderBytes.Width = 0;

            m_lastScripts = scripts;

            lvScripts.BeginUpdate();
            lvScripts.Items.Clear();

            int iHidden = 0;

            foreach (List<GameScript> scriptList in scripts.Scripts.Values)
            {
                foreach (GameScript script in scriptList)
                {
                    if (
                        (!Hacker.PointInMap(script.Location) && cbHideOffMapScripts.Checked) ||
                        (script.DoesNothing && cbHideEmptyScripts.Checked)
                       )
                    {
                        iHidden++;
                        continue;
                    }

                    ListViewItem lvi = new ListViewItem(String.Format("{0}", script.Index));
                    lvi.SubItems.Add(String.Format("{0},{1}", script.Location.X, script.Location.Y));
                    lvi.SubItems.Add(Global.DirectionString(script.Facing, true, true));
                    lvi.SubItems.Add(script.AutoExecute ? "Yes" : "");
                    lvi.SubItems.Add(String.Format("{0}", script.Lines.Count));
                    lvi.SubItems.Add(String.Format("{0}", script.NumBytes));
                    lvi.SubItems.Add(script.Summary(m_info, cbHideInlineSubscripts.Checked).Text);
                    lvi.Tag = script;
                    lvScripts.Items.Add(lvi);
                }
            }

            Global.SizeHeadersAndContent(lvScripts);

            CheckSelected();

            lvScripts.EndUpdate();

            int iMap = Hacker.GetCurrentMapIndex();

            if (m_iLastSelectedLine != -1)
            {
                int iLineTemp = m_iLastSelectedLine;
                UpdateSelectedScript();
                if (lvSelectedScript.Items.Count > iLineTemp)
                    lvSelectedScript.Items[iLineTemp].Selected = true;
            }
            else if (lvScripts.SelectedIndices.Count == 0 || (iMap != m_iLastMap && !m_bManuallySelected))
                lvSelectedScript.Items.Clear();

            m_iLastMap = iMap;
            m_bManuallySelected = false;
            m_iLastSelectedLine = -1;

            if (lvScripts.SelectedItems.Count > 0)
                lvScripts.EnsureVisible(lvScripts.SelectedItems[0].Index);

            string strHidden = (iHidden == 0 ? "" : String.Format(" ({0} hidden)", iHidden));
            string strMap = String.Format("{0}{1}", Hacker.GetMapTitle(Hacker.GetLocation().MapIndex), strHidden);
            string strTitle = String.Format("Scripts for map {0}", strMap);
            if (miAllScriptsFile.Checked)
                strTitle = "Scripts from file: " + m_strLastFileLoaded;
            else if (miAllScriptsInternal.Checked)
                strTitle = "Scripts from Internal Data";
            else if (!scripts.IsMainList)
                strTitle = String.Format("Currently loaded custom scripts for map {0}", strMap);

            if (strTitle != Text)
                Text = strTitle;
        }

        private void AddInfo(string info, string text, object tag = null)
        {
            ListViewItem lvi = new ListViewItem(info);
            lvi.SubItems.Add(text);
            lvi.Tag = tag;
            lvSelectedScript.Items.Add(lvi);
        }

        private void AddScript(GameScript script)
        {
            AddScript(0, script, 0, String.Empty);
        }

        private void AddScript(int iDepth, GameScript script, int iInsertLine, string strLinePrefix)
        {
            if (iDepth > 5)
            {
                lvSelectedScript.Items.Add(new ListViewItem(new string[] { (strLinePrefix + "*"), "", "", "[Recursion depth exceeded]" }));
                return;
            }

            int iMaxGoto = -1;

            foreach (ScriptLine line in script.Lines)
            {
                if (line.Number < iInsertLine)
                    continue;   // Note that line numbers aren't necessarily sequential

                if ((line.Bytes == null || line.Bytes.Length == 0) && cbHideEmptyLines.Checked)
                    continue;

                ListViewItem lvi = new ListViewItem(String.Format("{0}{1}", strLinePrefix, line.Number));
                if (line.Bytes == null)
                {
                    lvi.SubItems.Add("null");
                    lvi.SubItems.Add("null");
                }
                else
                {
                    if (cbHideAddresses.Checked)
                    {
                        lvi.SubItems.Add(Global.ByteStringCombined(line.HeaderBytes));
                        lvi.SubItems.Add(Global.ByteStringCombined(line.CommandBytes));
                    }
                    else
                    {
                        if (line.HeaderBytes != null)
                            lvi.SubItems.Add(String.Format("{0}: {1}", line.HeaderBytes.Offset+1, Global.ByteStringCombined(line.HeaderBytes)));
                        else
                            lvi.SubItems.Add("");
                        if (line.CommandBytes != null)
                            lvi.SubItems.Add(String.Format("{0}: {1}", line.CommandBytes.Offset + 1, Global.ByteStringCombined(line.CommandBytes)));
                        else
                            lvi.SubItems.Add("");
                    }
                }
                lvi.SubItems.Add(line.Description(m_info, strLinePrefix));
                lvi.Tag = line;

                if (!line.IsSubscriptCommand || cbHideInlineSubscripts.Checked)
                    lvSelectedScript.Items.Add(lvi);

                foreach(int iGoto in line.GotoLines)
                    iMaxGoto = Math.Max(iMaxGoto, iGoto);

                // Stop adding lines after an Exit or Return that can't be jumped past.
                // This prevents some infinite recursions in the list that can't actually happen in-game
                bool bUnreachable = (line.IsReturnCommand && iMaxGoto <= line.Number);

                if (bUnreachable && cbHideUnreachable.Checked)
                    break;

                if (line.IsSubscriptCommand && !cbHideInlineSubscripts.Checked && !bUnreachable)
                {
                    Point ptInsert = line.InsertScriptLocation;
                    if (m_lastScripts.Scripts.ContainsKey(ptInsert))
                        AddScript(iDepth + 1, m_lastScripts.Scripts[ptInsert][0], line.InsertScriptLine, String.Format("{0}{1}.", strLinePrefix, line.Number));
                    else
                        lvSelectedScript.Items.Add(new ListViewItem(new string[] { (strLinePrefix + "?"), "", "", String.Format("[No script found at {0},{1}]", ptInsert.X, ptInsert.Y) }));
                }
            }
        }

        private void lvScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedScript();
        }

        private void UpdateSelectedScript()
        {
            if (lvScripts.SelectedItems.Count < 1)
            {
                lvSelectedScript.Items.Clear();
                return;
            }

            if (!m_bNoUpdateSelLine)
                m_iLastSelectedLine = -1;

            GameScript script = lvScripts.SelectedItems[0].Tag as GameScript;

            lvSelectedScript.BeginUpdate();
            lvSelectedScript.Items.Clear();

            AddScript(script);

            Global.SizeHeadersAndContent(lvSelectedScript, 0, true, new int[] { -1, -1, 400, -1 });

            if (!script.HasHeaderBytes)
                chHeaderBytes.Width = 0;

            lvSelectedScript.EndUpdate();
        }

        private void lvScripts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvScripts.ListViewItemSorter = new ScriptComparer(lvScripts, e.Column, m_bAscendingSort);
            lvScripts.Sort();

            if (lvScripts.SelectedIndices.Count > 0)
                lvScripts.EnsureVisible(lvScripts.SelectedIndices[0]);
        }

        public void ForceRefresh(MemoryBytes bytes = null)
        {
            if (miAllScriptsInternal.Checked && bytes == null)
                bytes = new MemoryBytes(Global.Uncompress(Properties.Resources.MM2ExternalScripts));

            if (Hacker == null)
                return;

            if (lvScripts.SelectedItems.Count > 0 && m_ptSelect == Global.NullPoint)
            {
                GameScript script = lvScripts.SelectedItems[0].Tag as GameScript;
                m_ptSelect = script.Location;
                m_idxSelect = script.Index;
            }
            m_iLastSelectedLine = lvSelectedScript.SelectedIndices.Count > 0 ? lvSelectedScript.SelectedIndices[0] : -1;

            if (bytes != null)
                m_bytesLast = bytes;
            else
                m_bytesLast = Hacker.GetScriptBytes();

            m_info = Hacker.GetScriptInfo(m_bytesLast);

            if (m_info.Scripts == null)
            {
                Text = "Scripts (no data found)";
                return;
            }

            UpdateUI(m_info.Scripts);
        }

        private void RefreshView()
        {
            if (miAllScriptsFile.Checked)
                LoadFile(m_strLastFileLoaded);
            else
                ForceRefresh();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    RefreshView();
                    return true;
                default:
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cmSelectedScript_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvSelectedScript.SelectedItems.Count > 0;

            miScriptCopy.Enabled = bAnySelected;

            bool bHasMap = false;
            if (!bAnySelected)
                bHasMap = false;
            else if ((lvSelectedScript.SelectedItems[0].Tag as ScriptLine).IsTeleportCommand)
                bHasMap = true;

            miScriptsGoToMap.Enabled = bHasMap;
            miScriptsSetBeacon.Enabled = Global.Cheats && bHasMap;
            miScriptsSetBeacon.Visible = Global.Cheats && Hacker.HasBeacon;
            miSelectedScriptTeleport.Enabled = Global.Cheats && bHasMap;
            miSelectedScriptTeleport.Visible = Global.Cheats;
            miScriptEdit.Visible = Global.Cheats;
            miScriptEdit.Enabled = Global.Cheats && bAnySelected && ScriptsEditable;
            miScriptReinterpret.Visible = Global.Debug;
            miScriptCopyCommandOffset.Visible = Global.Debug;
        }

        private void miScriptsGoToMap_Click(object sender, EventArgs e)
        {
            GoToSelectedMap();
        }

        private void GoToSelectedMap()
        {
            if (lvSelectedScript.SelectedItems.Count < 1)
                return;

            ScriptLine line = lvSelectedScript.SelectedItems[0].Tag as ScriptLine;
            if (line == null)
                return;

            if (!line.IsTeleportCommand)
                return;

            m_main.SetCursor(line.TeleportLocation);
            m_main.GotoSheet(Hacker.CorrectMapIndex(line.TeleportMapIndex));
        }

        private void miScriptsSetBeacon_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvSelectedScript.FocusedItem == null)
                return;

            ScriptLine line = lvSelectedScript.SelectedItems[0].Tag as ScriptLine;
            if (line == null)
                return;

            if (!line.IsTeleportCommand)
                return;

            Hacker.SetBeacon(line.TeleportLocation, Hacker.CorrectMapIndex(line.TeleportMapIndex));
        }

        private void miScriptsRefresh_Click(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideOffMap_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideDoNothing_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cmAllScriptsRefresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void cmAllScripts_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvScripts.SelectedItems.Count > 0;

            miSelectedScriptTeleport.Enabled = Global.Cheats;
            miSelectedScriptTeleport.Visible = Global.Cheats;

            miAllScriptsCopy.Enabled = bAnySelected;

            switch (Hacker.Game)
            {
                case GameNames.MightAndMagic2:
                    toolStripSeparator3.Visible = true;
                    miAllScriptsFile.Visible = true;
                    miAllScriptsGame.Visible = true;
                    miAllScriptsInternal.Visible = true;
                    break;
                default:
                    toolStripSeparator3.Visible = false;
                    miAllScriptsFile.Visible = false;
                    miAllScriptsGame.Visible = false;
                    miAllScriptsInternal.Visible = false;
                    break;
            }
            miAllScriptsReinterpret.Visible = Global.Debug;
        }

        private void miSelectedScriptTeleport_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvSelectedScript.FocusedItem == null)
                return;

            ScriptLine line = lvSelectedScript.FocusedItem.Tag as ScriptLine;
            if (line == null)
                return;

            if (!line.IsTeleportCommand)
                return;

            Hacker.SetLocation(line.TeleportLocation);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!scScriptsFind.Panel2Collapsed)
                m_findBoxScript.Next(sender, new BoolHandlerEventArgs(false));
            else if (!scSelectedFind.Panel2Collapsed)
                m_findBoxSelected.Next(sender, new BoolHandlerEventArgs(false));
            else
                Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!scScriptsFind.Panel2Collapsed)
                m_findBoxScript.HideFindBox();
            else  if (!scSelectedFind.Panel2Collapsed)
                m_findBoxSelected.HideFindBox();
            else
                Close();
        }

        private void GetTabSeparatedString(StringBuilder sb, int iDepth, GameScript script, int iInsertLine, string strLinePrefix, bool bIgnoreSubscripts)
        {
            int iMaxGoto = -1;

            foreach (ScriptLine line in script.Lines)
            {
                if (line.Number < iInsertLine)
                    continue;
                
                if ((line.Bytes == null || line.Bytes.Length == 0) && cbHideEmptyLines.Checked)
                    continue;

                foreach (int iGoto in line.GotoLines)
                    iMaxGoto = Math.Max(iMaxGoto, iGoto);

                // Stop adding lines after an Exit or Return that can't be jumped past.
                // This prevents some infinite recursions in the list that can't actually happen in-game
                if (line.IsReturnCommand && iMaxGoto <= line.Number)
                    break;

                GetTabSeparatedString(sb, iDepth, line, strLinePrefix, bIgnoreSubscripts);
            }
        }

        private void GetTabSeparatedString(StringBuilder sb, int iDepth, ScriptLine line, string strLinePrefix, bool bIgnoreSubscripts)
        {
            if (iDepth > 5)
                sb.AppendLine("[subscript recursion depth exceeded]");

            if (bIgnoreSubscripts || !line.IsSubscriptCommand || !m_lastScripts.Scripts.ContainsKey(line.InsertScriptLocation))
            {
                sb.AppendLine(line.GetTabSeparatedString(m_info, strLinePrefix));
                return;
            }

            GetTabSeparatedString(sb, iDepth, m_lastScripts.Scripts[line.InsertScriptLocation][0], line.InsertScriptLine, String.Format("{0}{1}.", strLinePrefix, line.Number), bIgnoreSubscripts);
        }

        private void miAllScriptsCopy_Click(object sender, EventArgs e)
        {
            if (lvScripts.SelectedItems.Count < 1)
                return;

            StringBuilder sb = new StringBuilder();

            GameScript script = lvScripts.SelectedItems[0].Tag as GameScript;
            if (script == null)
                return;

            GetTabSeparatedString(sb, 0, script, 0, String.Empty, cbHideInlineSubscripts.Checked);

            Clipboard.SetText(sb.ToString());
        }

        private void miScriptCopy_Click(object sender, EventArgs e)
        {
            if (lvSelectedScript.SelectedItems.Count < 1)
                return;

            StringBuilder sb = new StringBuilder();

            ScriptLine line = lvSelectedScript.SelectedItems[0].Tag as ScriptLine;
            if (line == null)
                return;

            GetTabSeparatedString(sb, 0, line, String.Empty, cbHideInlineSubscripts.Checked);

            Clipboard.SetText(sb.ToString());
        }

        private void miScriptEdit_Click(object sender, EventArgs e)
        {
            EditScript();
        }

        private void EditScript()
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvSelectedScript.FocusedItem == null)
                return;

            if (!ScriptsEditable)
                return;

            ScriptLine line = lvSelectedScript.FocusedItem.Tag as ScriptLine;
            if (line == null)
                return;

            EditBytesForm form = new EditBytesForm(TopLevelControl);
            form.ForceLength = true;
            form.Bytes = line.Bytes;
            int iSCO = Hacker.ScriptCommandOffset;
            form.Title = String.Format("Change bytes at offset {0} (0x{1:X}), Command: {2}", line.Bytes.Offset+iSCO, line.Bytes.Offset+iSCO, line.CommandBytes.Offset+iSCO);
            if (form.ShowDialog() == DialogResult.OK)
            {
                line.Bytes = new MemoryBytes(form.Bytes, line.Bytes.Offset);
                Hacker.SetScriptLine(line);
                ForceRefresh();
            }
        }

        private void miAllScriptsTeleport_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvScripts.FocusedItem == null)
                return;

            GameScript script = lvScripts.FocusedItem.Tag as GameScript;
            if (script == null)
                return;

            Hacker.SetLocation(script.Location);
        }

        private void cbHideSubscriptLines_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideEmptyLines_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void lvSelectedScript_DoubleClick(object sender, EventArgs e)
        {
            EditScript();
        }

        private bool ScriptsEditable { get { return Hacker != null && (miAllScriptsGame.Checked || Hacker.Game != GameNames.MightAndMagic2); } }

        private void SetChecked(ToolStripMenuItem mi)
        {
            mi.Checked = true;
            if (mi == miAllScriptsGame)
            {
                miAllScriptsFile.Checked = false;
                miAllScriptsInternal.Checked = false;
            }
            else if (mi == miAllScriptsFile)
            {
                miAllScriptsGame.Checked = false;
                miAllScriptsInternal.Checked = false;
            }
            else if (mi == miAllScriptsInternal)
            {
                miAllScriptsGame.Checked = false;
                miAllScriptsFile.Checked = false;
            }
        }

        private void LoadFile(string strFile)
        {
            try
            {
                MemoryBytes mb = new MemoryBytes(File.ReadAllBytes(strFile));
                m_strLastFileLoaded = strFile;
                SetChecked(miAllScriptsFile);
                ForceRefresh(mb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Could not read from file \"{0}\":\r\n\r\n{1}", strFile, ex.Message), "Error loading file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void miAllScriptsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load a set of scripts from a file";
            ofd.DefaultExt = "dat";
            ofd.FileName = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFile(ofd.FileName);
            }
        }

        private void miAllScriptsGame_Click(object sender, EventArgs e)
        {
            SetChecked(miAllScriptsGame);
            ForceRefresh();
        }

        private void miAllScriptsInternal_Click(object sender, EventArgs e)
        {
            SetChecked(miAllScriptsInternal);
            m_strLastFileLoaded = "Internal Data";
            ForceRefresh();
        }

        private void miAllScriptsSetNote_Click(object sender, EventArgs e)
        {
            if (lvScripts.SelectedItems.Count < 1)
                return;

            GameScript script = lvScripts.SelectedItems[0].Tag as GameScript;
            string strPrefix = "Forced Encounter with ";
            string strSymbol = "E";

            foreach (ScriptLine line in script.Lines)
            {
                if (line is MM2ScriptLine)
                {
                    MM2ScriptLine mm2Line = line as MM2ScriptLine;
                    if (mm2Line.Command == MM2ScriptCommand.CheckEra)
                    {
                        strSymbol = String.Format("{0}c", mm2Line.CommandBytes[1]+1);
                        strPrefix = String.Format("[{0}th century only]\r\nForced Encounter with ", mm2Line.CommandBytes[1]+1);
                    }
                }

                if (line.IsEncounterCommand)
                {
                    m_main.AddNoteToMap(m_main.TranslateToInternalMap(script.Location, null), strSymbol, Hacker.GetEncounterNoteText(strPrefix, line.CommandBytes));
                    break;
                }
            }
        }

        private void miScriptReinterpret_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (lvSelectedScript.FocusedItem == null)
                return;

            ScriptLine line = lvSelectedScript.FocusedItem.Tag as ScriptLine;
            if (line == null)
                return;

            string strTest = line.Description(m_info, String.Empty);
        }

        private void miAllScriptsReinterpret_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (lvScripts.FocusedItem == null)
                return;

            GameScript script = lvScripts.FocusedItem.Tag as GameScript;
            if (script == null)
                return;

            string strScript = script.Summary(m_info, false, false).Text;
            NoteInfo noteInfo = script.Summary(m_info, false, true);
            MessageBox.Show(String.Format("{0}\r\n\r\n{1}\r\n{2}", strScript, noteInfo.Symbol, noteInfo.Text), "Script/Note");
        }

        private void cbHideUnreachable_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideAddresses_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void miScriptCopyCommandOffset_Click(object sender, EventArgs e)
        {
            if (lvSelectedScript.SelectedIndices.Count < 1)
                return;

            ScriptLine line = lvSelectedScript.SelectedItems[0].Tag as ScriptLine;
            if (line == null)
                return;

            GameScript script = lvScripts.SelectedItems[0].Tag as GameScript;
            if (script == null)
                return;

            Clipboard.SetText(m_main.GetLocationText(script.Location, m_main.CurrentSheet.GameMapIndex, 
                m_main.CurrentSheet.Title).Replace("{Command}", (line.CommandBytes.Offset+1).ToString()));
        }

        private void ScriptsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScriptViewerSettings settings = new ScriptViewerSettings();
            settings.HideAddresses = cbHideAddresses.Checked;
            settings.HideEmptyLines = cbHideEmptyLines.Checked;
            settings.HideEmptyScripts = cbHideEmptyScripts.Checked;
            settings.HideInlineSubscripts = cbHideInlineSubscripts.Checked;
            settings.HideOffMap = cbHideOffMapScripts.Checked;
            settings.HideUnreachableCode = cbHideUnreachable.Checked;
            Properties.Settings.Default.ScriptViewer = settings;
        }

        private void ScriptsForm_Load(object sender, EventArgs e)
        {
            ScriptViewerSettings settings = Properties.Settings.Default.ScriptViewer;
            cbHideAddresses.Checked = settings.HideAddresses;
            cbHideEmptyLines.Checked = settings.HideEmptyLines;
            cbHideEmptyScripts.Checked = settings.HideEmptyScripts;
            cbHideInlineSubscripts.Checked = settings.HideInlineSubscripts;
            cbHideOffMapScripts.Checked = settings.HideOffMap;
            cbHideUnreachable.Checked = settings.HideUnreachableCode;

            m_findBoxScript = new FindBox(scScriptsFind, tbFindScripts, FindBox.ListViewFindFunction, lvScripts);
            m_findBoxSelected = new FindBox(scSelectedFind, tbFindSelected, FindBox.ListViewFindFunction, lvSelectedScript);
            CommonKeyFind += Find;
            CommonKeyNext += Next;
            CommonKeyPrevious += Previous;

            scScriptsFind.Panel2Collapsed = true;
            scSelectedFind.Panel2Collapsed = true;
        }

        public void Previous(object sender, EventArgs e)
        {
            if (lvSelectedScript.Focused)
                m_findBoxSelected.Previous(sender, e);
            else if (lvScripts.Focused)
                m_findBoxScript.Previous(sender, e);
        }

        public void Next(object sender, BoolHandlerEventArgs e)
        {
            if (lvSelectedScript.Focused)
                m_findBoxSelected.Next(sender, e);
            else if (lvScripts.Focused)
                m_findBoxScript.Next(sender, e);
        }

        public void Find(object sender, EventArgs e)
        {
            if (lvSelectedScript.Focused)
                m_findBoxSelected.Find(sender, e);
            else if (lvScripts.Focused)
                m_findBoxScript.Find(sender, e);
        }
    }

    class ScriptComparer : BasicListViewComparer
    {
        public ScriptComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            if (x == null || y == null)
                return 0;

            GameScript script1 = ((ListViewItem) x).Tag as GameScript;
            GameScript script2 = ((ListViewItem) y).Tag as GameScript;

            switch (m_column)
            {
                case 0: return Order(Math.Sign((int)script1.Index - (int)script2.Index));
                case 1: return Order(String.Compare(Global.PointString(script1.Location), Global.PointString(script2.Location)));
                case 2: return Order(Math.Sign((int)script1.Facing - (int)script2.Facing));
                case 4: return Order(Math.Sign(script1.Lines.Count - script2.Lines.Count));
                case 5: return Order(Math.Sign(script1.NumBytes - script2.NumBytes));
                default: return base.Compare(x, y);
            }
        }
    }
}

