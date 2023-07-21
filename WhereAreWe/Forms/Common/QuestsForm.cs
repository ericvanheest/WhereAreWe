using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class QuestsForm : HackerBasedForm
    {
        private enum AddGoalType { Primary, Secondary, Quest, NoChange };

        private int m_iHiddenInvalid = 0;
        private bool m_bExpanding = false;
        private bool m_bUnknown = false;
        private FindBox m_findBox = null;
        private FindBox m_findBoxGoal = null;

        private bool m_bShowGiver = true;
        private bool m_bShowRewards = true;
        private bool m_bBoldNearby = true;
        private bool m_bUseFilesLink = true;

        private QuestInfoBase m_lastInfo = null;

        private TreeNode tnMain = null;
        private TreeNode tnAvailable = null;
        private TreeNode tnAccepted = null;
        private TreeNode tnCompleted = null;
        private TreeNode tnNearby = null;
        private TreeNode tnFlagged = null;
        private TreeNode tnUnachievable = null;

        private TreeNode[] QuestNodes = null;
        private TreeNode[] QuestNodesWithUnachievable = null;
        private Regex m_reNodePath = new Regex(@" \(\d+\)[^\\]*");

        private Font m_fontNormal;
        private string m_strLastSelectedQuestName = null;
        private int m_iLastSelectedQuestSubItem = -1;
        private int m_iLastListPos = -1;
        private bool m_bAutoAskFiles = true;
        private bool m_bOptionsChanged = true;

        private HashSet<MapXY> m_nearbyLocations = new HashSet<MapXY>();
        private HashSet<string> m_flaggedQuests = new HashSet<string>();
        private HashSet<string> m_manualCompletedTasks = new HashSet<string>();
        private HashSet<string> m_manualCompletedQuests = new HashSet<string>();

        public string[] GetFlaggedQuests() { return m_flaggedQuests.ToArray(); }
        public string[] GetManualCompletedTasks() { return m_manualCompletedTasks.ToArray(); }
        public string[] GetManualCompletedQuests() { return m_manualCompletedQuests.ToArray(); }

        protected override bool OnCommonKeyClearText()
        {
            if (tbFindGoal.Focused)
                tbFindGoal.Text = "";
            else
                tbFind.Text = "";
            return true;
        }

        public void SetFlaggedQuests(string[] quests, bool bRefresh = true)
        {
            m_flaggedQuests.Clear();
            if (quests != null)
                foreach (string str in quests)
                    m_flaggedQuests.Add(str);
            if (bRefresh)
                ForceRefresh();
        }

        private void SetHashSet(HashSet<string> hashSet, string[] strings)
        {
            hashSet.Clear();
            if (strings == null)
                return;
            foreach (string str in strings)
                hashSet.Add(str);
        }

        public void SetManualCompletedItems(string[] quests, string[] tasks, bool bRefresh = true)
        {
            SetHashSet(m_manualCompletedQuests, quests);
            SetHashSet(m_manualCompletedTasks, tasks);
            if (bRefresh)
                ForceRefresh();
        }

        public override void SetParameter(object param)
        {
            QuestWindowParams qwp = param as QuestWindowParams;
            if (qwp == null)
                return;

            SetFlaggedQuests(qwp.Flagged, false);
        }

        private bool GoalsFocused
        {
            get
            {
                return lvInfo.Focused || splitContainer2.Focused || splitContainer2.Panel1.Focused || splitContainer2.Panel2.Focused || tbFindGoal.Focused;
            }
        }

        public void Find(object sender, EventArgs e)
        {
            if (GoalsFocused)
            {
                m_findBox.HideFindBox();
                m_findBoxGoal.Find(sender, e);
            }
            else
            {
                m_findBoxGoal.HideFindBox();
                m_findBox.Find(sender, e);
            }
        }

        public void Next(object sender, BoolHandlerEventArgs e)
        {
            if (GoalsFocused)
                m_findBoxGoal.Next(sender, e);
            else
                m_findBox.Next(sender, e);
        }

        public void Previous(object sender, EventArgs e)
        {
            if (GoalsFocused)
                m_findBoxGoal.Previous(sender, e);
            else
                m_findBox.Previous(sender, e);
        }

        public QuestsForm()
        {
            InitializeComponent();
            SetNodeObjects();
            NativeMethods.SetTooltipDelay(lvInfo, 32000);

            m_findBox = new FindBox(splitContainer3, tbFind, FindBox.TreeViewFindFunction, tvQuests);
            CommonKeyFind += Find;
            CommonKeyNext += Next;
            CommonKeyPrevious += Previous;
            m_findBox.HideFindBox();

            m_findBoxGoal = new FindBox(splitContainer2, tbFindGoal, FindBox.ListViewFindFunction, lvInfo);
            m_findBoxGoal.HideFindBox();

            miQuestsHideInvalid.Checked = Properties.Settings.Default.HideInvalidQuestGoals;
        }

        private TreeNode CreateNamedNode(string str)
        {
            TreeNode tn = new TreeNode(str);
            tn.Name = str;
            return tn;
        }

        private void SetNodeObjects()
        {
            m_fontNormal = new System.Drawing.Font(tvQuests.Font, FontStyle.Regular);
            tvQuests.Font = new System.Drawing.Font(tvQuests.Font, FontStyle.Bold);
            tnFlagged = CreateNamedNode("Flagged");
            tnNearby = CreateNamedNode("Nearby");
            tnMain = CreateNamedNode("Main Story");
            tnAccepted = CreateNamedNode("Accepted Side Quests");
            tnAvailable = CreateNamedNode("Available Side Quests");
            tnCompleted = CreateNamedNode("Completed");
            tnUnachievable = CreateNamedNode("Unachievable");
            QuestNodes = new TreeNode[] { tnFlagged, tnNearby, tnMain, tnAccepted, tnAvailable, tnCompleted };
            QuestNodesWithUnachievable = new TreeNode[] { tnFlagged, tnNearby, tnMain, tnAccepted, tnAvailable, tnCompleted, tnUnachievable };
            SetUnknown();
        }

        protected override void OnMainSet()
        {
            m_main.OptionsChanged += OnMainOptionsChanged;
            OnMainSetAgain();
        }

        protected override void OnMainSetAgain()
        {
            ForceRefresh();
            timerUpdateMemory.Interval = Properties.Settings.Default.PollQuests;
            Global.RestartTimer(timerUpdateMemory);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!splitContainer1.Panel2Collapsed)
                m_findBox.Next(sender, new BoolHandlerEventArgs(false));
            else
                Close();
        }

        public override int[] Splitters
        {
            get
            {
                return new int[] { splitContainer1.Panel2Collapsed ? 0 : splitContainer1.SplitterDistance };
            }

            set
            {
                if (value == null || value.Length == 0 || value[0] == -1)
                    return;

                if (value[0] == 0)
                    ShowGoals(false);
                else
                {
                    ShowGoals(true);
                    Global.SetSplitterDistance(splitContainer1, value[0]);
                }
            }
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (Hacker == null || !Hacker.Running)
            {
                if (!m_bUnknown)
                {
                    Text = "Quests (no running game detected!)";
                    SetUnknown();
                    m_bUnknown = true;
                }
                return;
            }

            QuestInfoBase info = Hacker.GetQuestInfo(m_lastInfo, m_main.GetSelectedCharacterAddress(), m_bAutoAskFiles);
            m_bAutoAskFiles = false;    // Don't ask this question every tick if the answer was no!

            if (info == null || String.IsNullOrWhiteSpace(info.CharName))
            {
                Text = "Quests (no characters in party)";
                return;
            }

            if (m_bUnknown)
                m_bUnknown = false;

            UpdateUI(info);
        }

        private TreeNode AddTreeQuest(TreeNode root, List<TreeNode> parents, BasicQuest quest, bool bIgnorePath = false)
        {
            StringBuilder sb = new StringBuilder();
            if (m_bShowGiver && !String.IsNullOrWhiteSpace(quest.Giver))
                sb.AppendFormat("{0}: ", quest.Giver);
            sb.Append(quest.Name);
            if (m_bShowRewards && !String.IsNullOrWhiteSpace(quest.Reward))
                sb.AppendFormat(" ({0})", quest.Reward);
            TreeNode nodeNew = new TreeNode(sb.ToString());
            nodeNew.Tag = quest;
            TreeNode leaf = root;
            if (!String.IsNullOrEmpty(quest.Path) && !bIgnorePath)
            {
                foreach (string sub in quest.Path.Split('/'))
                {
                    if (leaf.Nodes.ContainsKey(sub))
                        leaf = root.Nodes[sub];
                    else
                    {
                        leaf = leaf.Nodes.Add(sub);
                        parents.Add(leaf);
                        leaf.NodeFont = new Font(tvQuests.Font, FontStyle.Bold);
                        leaf.Name = sub;
                    }
                }
            }
            nodeNew.NodeFont = m_fontNormal;
            leaf.Nodes.Add(nodeNew);

            QuestNodeTag tag = root.Tag as QuestNodeTag;
            if (tag != null)
                tag.Count++;

            return nodeNew;
        }

        private void OnMainOptionsChanged(object sender, EventArgs e)
        {
            m_bShowGiver = Properties.Settings.Default.QuestShowGiver;
            m_bShowRewards = Properties.Settings.Default.QuestShowReward;
            m_bBoldNearby = Properties.Settings.Default.QuestBoldNearby;
            timerUpdateMemory.Stop();
            timerUpdateMemory.Interval = Properties.Settings.Default.PollQuests;
            timerUpdateMemory.Start();
            m_bOptionsChanged = true;
        }

        private string NodePath(TreeNode node)
        {
            if (node == null)
                return String.Empty;
            StringBuilder sb = new StringBuilder(m_reNodePath.Replace(node.Text, ""));
            while(node.Parent != null)
            {
                node = node.Parent;
                sb.Insert(0, '\\');
                sb.Insert(0, m_reNodePath.Replace(node.Text, ""));
            }
            return sb.ToString();
        }

        private void AddExpandedNodes(TreeNodeCollection nodes, HashSet<string> paths)
        {
            if (nodes == null || nodes.Count < 1)
                return;

            foreach (TreeNode node in nodes)
            {
                if (node.IsExpanded)
                    paths.Add(NodePath(node));
                AddExpandedNodes(node.Nodes, paths);
            }
        }

        private void UpdateUI(QuestInfoBase info)
        {
            if (info == null || IsDisposed)
                return;

            if (m_lastInfo != null && Global.Compare(m_lastInfo.Bytes, info.Bytes) && !m_bOptionsChanged)
                return; // Nothing changed

            if (info.QuestsEqual(m_lastInfo) && info.MapIndex == m_lastInfo.MapIndex)
            {
                // Nothing changed that actually impacted something in the UI (typically characters simply moving around the map)
                m_lastInfo = info;  // Don't need to check this again
                return;
            }

            m_bOptionsChanged = false;

            m_bUseFilesLink = info.NeedsFiles;

            m_bShowGiver = Properties.Settings.Default.QuestShowGiver;
            m_bShowRewards = Properties.Settings.Default.QuestShowReward;
            m_bBoldNearby = Properties.Settings.Default.QuestBoldNearby;

            string strQuestSelected = null;
            string strPathSelected = null;
            if (tvQuests.SelectedNode != null)
            {
                strQuestSelected = tvQuests.SelectedNode.Text;
                strPathSelected = m_reNodePath.Replace(tvQuests.SelectedNode.FullPath, "");
            }

            m_lastInfo = info;

            if (lvInfo.TopItem != null)
                m_iLastListPos = lvInfo.TopItem.Index;
            if (lvInfo.SelectedIndices.Count > 0)
                m_iLastSelectedQuestSubItem = lvInfo.SelectedIndices[0];

            Point ptScroll = tvQuests.ScrollPosition;
            Dictionary<string, bool> oldParents = CreateParentsDict();
            List<TreeNode> parents = new List<TreeNode>(QuestNodesWithUnachievable);

            lvInfo.Items.Clear();

            TreeNode nodePriorSelected = tvQuests.SelectedNode;
            bool bPriorSelNearby = (nodePriorSelected != null && nodePriorSelected.Parent == tnNearby);
            bool bPriorSelFlagged = (nodePriorSelected != null && nodePriorSelected.Parent == tnFlagged);

            HashSet<string> priorExpanded = new HashSet<string>();
            AddExpandedNodes(tvQuests.Nodes, priorExpanded);

            NativeMethods.SuspendDrawing(splitContainer3.Panel1);
            tvQuests.BeginUpdate();

            tvQuests.Nodes.Clear();
            foreach (TreeNode node in QuestNodesWithUnachievable)
            {
                node.Nodes.Clear();
                node.Text = node.Name;
                node.Tag = new QuestNodeTag(0);
            }

            if (tvQuests.SelectedNode != null && tvQuests.SelectedNode.Tag is BasicQuest)
                m_strLastSelectedQuestName = (tvQuests.SelectedNode.Tag as BasicQuest).Name;

            tvQuests.SelectedNode = null;

            int iHidden = 0;
            int iRepeatable = 0;
            int iInvalid = 0;

            BasicQuest[] quests = info.GetQuests();
            TreeNode tnSelected = null;

            foreach (BasicQuest quest in quests)
            {
                if (m_manualCompletedQuests.Contains(quest.Name))
                    quest.Status.Main = QuestStatus.Basic.ManualCompleted;

                if (quest.Status.InvalidClass)
                {
                    iInvalid++;
                    continue;
                }

                quest.CharAddress = info.CharAddress;

                TreeNode tnQuest = null;
                bool bMain = (quest.QuestType == BasicQuestType.Primary);

                switch (quest.Status.Main)
                {
                    case QuestStatus.Basic.Completed:
                    case QuestStatus.Basic.ManualCompleted:
                        tnQuest = AddTreeQuest(tnCompleted, parents, quest);
                        break;
                    case QuestStatus.Basic.Unachievable:
                    case QuestStatus.Basic.NoFile:
                        tnQuest = AddTreeQuest(tnUnachievable, parents, quest);
                        break;
                    case QuestStatus.Basic.Accepted:
                        tnQuest = AddTreeQuest(bMain ? tnMain : tnAccepted, parents, quest);
                        if (quest.Path == Global.RepeatableQuest)
                            iRepeatable++;
                        break;
                    case QuestStatus.Basic.NotStarted:
                        tnQuest = AddTreeQuest(bMain ? tnMain : tnAvailable, parents, quest);
                        if (quest.Path == Global.RepeatableQuest)
                            iRepeatable++;
                        break;
                    default:
                        break;
                }

                if (Properties.Settings.Default.UIElementOptions != null)
                {
                    if (quest.Status.Main == QuestStatus.Basic.ManualCompleted)
                        Properties.Settings.Default.UIElementOptions.SetElement(tnQuest, ColoredUIElements.ManuallyCompletedQuest);
                    else
                        Properties.Settings.Default.UIElementOptions.SetElement(tnQuest, ColoredUIElements.ActiveQuest);
                }

                TreeNode tnFlaggedQuest = null;
                if (m_flaggedQuests.Contains(quest.Name))
                {
                    TreeNode tnClone = tnQuest.Clone() as TreeNode;
                    if ((tnQuest.Tag as BasicQuest).IsCompleted)
                    {
                        tnClone.NodeFont = new Font(tnClone.NodeFont, FontStyle.Strikeout);
                        tnClone.ForeColor = SystemColors.GrayText;
                    }
                    tnClone.Tag = tnQuest.Tag;
                    tnFlagged.Nodes.Add(tnClone);
                    (tnFlagged.Tag as QuestNodeTag).Count++;
                    tnFlaggedQuest = tnClone;
                }

                TreeNode tnNearbyQuest = null;
                if (quest.IsNearby(m_lastInfo.MapIndex))
                    tnNearbyQuest = AddTreeQuest(tnNearby, parents, quest, true);

                if (tnSelected == null && tnQuest != null && NodePath(tnQuest.Parent) == strPathSelected)
                    tnSelected = tnQuest.Parent;
                else if (tnSelected == null && tnQuest != null && tnQuest.Text == strQuestSelected)
                {
                    if (bPriorSelFlagged)
                        tnSelected = tnFlaggedQuest;
                    else if (bPriorSelNearby)
                        tnSelected = tnNearbyQuest;
                    else
                        tnSelected = tnQuest;
                }
            }

            AddCounts();

            tnMain.ExpandAll();
            tnAccepted.Expand();
            tnNearby.ExpandAll();
            tnFlagged.ExpandAll();

            foreach(TreeNode node in QuestNodesWithUnachievable)
                ExpandSmallNodes(node.Nodes, priorExpanded);

            foreach (TreeNode node in parents)
            {
                if (node.Parent == null)
                    continue;

                if (node.TreeView != null && oldParents.ContainsKey(node.FullPath))
                {
                    if (oldParents[node.FullPath])
                        node.Expand();
                    else
                        node.Collapse();
                }
            }

            if (tnSelected == null && !String.IsNullOrEmpty(strPathSelected))
            {
                foreach (TreeNode root in parents)
                {
                    if (root.Parent == null)
                        continue;

                    if (root.TreeView !=  null && m_reNodePath.Replace(root.FullPath, "") == strPathSelected)
                    {
                        tnSelected = root;
                        break;
                    }
                }
            }

            if (tnUnachievable.Nodes.Count < 1)
                tvQuests.Nodes.AddRange(QuestNodes);
            else
                tvQuests.Nodes.AddRange(QuestNodesWithUnachievable);

            if (QuestNodesWithUnachievable.Contains(nodePriorSelected) && nodePriorSelected.TreeView != null)
                tvQuests.SelectedNode = nodePriorSelected;
            else
                tvQuests.SelectedNode = tnSelected;

            tvQuests.EndUpdate();
            NativeMethods.ResumeDrawing(splitContainer3.Panel1);

            tvQuests.ScrollPosition = ptScroll;

            string strHidden = (iHidden == 0 ? "" : String.Format(", {0} hidden", iHidden));
            string strRepeatable = (iRepeatable == 0 ? "" : String.Format(", {0} repeatable", iRepeatable));
            string strTitle = String.Format("Quests for {0} ({1}/{2} completed{3}{4})",
                info.CharName, info.CompletedQuests, quests.Length - iInvalid - iRepeatable, strRepeatable, strHidden);
            if (strTitle != Text)
                Text = strTitle;
        }

        private void AddCounts()
        {
            foreach (TreeNode tn in QuestNodesWithUnachievable)
                AddCounts(tn);
        }

        private void AddCounts(TreeNode tn)
        {
            QuestNodeTag tag = tn.Tag as QuestNodeTag;

            if (tag == null)
                return;

            tn.Text = String.Format("{0} ({1})", tn.Name, tag.Count);
        }

        private void SetUnknown()
        {
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        private void QuestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdateMemory.Stop();

            m_main.SetFlaggedQuests(GetFlaggedQuests());
        }

        private ListViewItem GetInfo(AddGoalType goal, QuestLocation loc, int iIcon = -1, bool bHighlightNearby = false)
        {
            if (loc.Active.State == QuestStatus.Basic.InvalidClass)
                return null;     // Don't even show it in the list if we're not the right class

            ListViewItem lvi = new ListViewItem(loc.Description);
            if (loc.Game == GameNames.None)
                lvi.SubItems.Add("<invalid location>");
            else if (loc.HasLocation || loc.HasMap)
                lvi.SubItems.Add(LocationString(loc));
            else if (goal == AddGoalType.Quest)
                lvi.SubItems.Add("(see quest)");
            lvi.ImageIndex = iIcon;

            if (!UpdateInfo(lvi, loc))
                return null;

            if (bHighlightNearby)
            {
                if (m_lastInfo.MapIndex == loc.MapIndex && loc.Active.State == QuestStatus.Basic.Accepted)
                {
                    // Only bold the first entry for a particular location (prevents piles of "return to the king" style bolded locations)
                    MapXY map = new MapXY(Hacker.Game, loc.MapIndex, loc.Location.X, loc.Location.Y);
                    if (!m_nearbyLocations.Contains(map))
                    {
                        m_nearbyLocations.Add(map);
                        lvi.Font = new Font(lvi.Font, FontStyle.Bold);
                    }
                }
            }

            return lvi;
        }

        private bool UpdateInfo(ListViewItem lvi, QuestLocation loc)
        {
            lvi.Tag = loc;
            if (loc.Active.IsManual && m_manualCompletedTasks.Contains(loc.Path))
                loc.Active.State = QuestStatus.Basic.ManualCompleted;

            UIElementOptions colors = Properties.Settings.Default.UIElementOptions;

            switch (loc.Active.State)
            {
                case QuestStatus.Basic.Completed:
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Strikeout);
                    colors.SetElement(lvi, ColoredUIElements.CompletedQuestTask);
                    break;
                case QuestStatus.Basic.Unachievable:
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Italic);
                    colors.SetElement(lvi, ColoredUIElements.UnachievableQuestTask);
                    if (!String.IsNullOrWhiteSpace(loc.Active.Reason))
                        lvi.ToolTipText = String.Format("Unachievable: {0}", loc.Active.Reason);
                    break;
                case QuestStatus.Basic.Invalid:
                    if (Properties.Settings.Default.HideInvalidQuestGoals)
                    {
                        m_iHiddenInvalid++;
                        return false;
                    }
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Italic);
                    colors.SetElement(lvi, ColoredUIElements.InvalidQuestTask);
                    if (!String.IsNullOrWhiteSpace(loc.Active.Reason))
                        lvi.ToolTipText = String.Format("Invalid: {0}", loc.Active.Reason);
                    break;
                case QuestStatus.Basic.NoFile:
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Italic);
                    colors.SetElement(lvi, ColoredUIElements.UnachievableQuestTask);
                    if (!String.IsNullOrWhiteSpace(loc.Active.Reason))
                        lvi.ToolTipText = String.Format("No File: {0}", loc.Active.Reason);
                    break;
                case QuestStatus.Basic.ManualCompleted:
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Strikeout);
                    colors.SetElement(lvi, ColoredUIElements.ManuallyCompletedQuestTask);
                    if (!String.IsNullOrWhiteSpace(loc.Active.Reason))
                        lvi.ToolTipText = loc.Active.Reason;
                    break;
                case QuestStatus.Basic.ManualNotCompleted:
                    colors.SetElement(lvi, ColoredUIElements.ActiveManualQuestTask);
                    lvi.Font = new Font(lvInfo.Font, FontStyle.Regular);
                    if (!String.IsNullOrWhiteSpace(loc.Active.Reason))
                        lvi.ToolTipText = loc.Active.Reason;
                    break;
                default:
                    break;
            }

            return true;
        }

        private void UpdateInfo(int iIndex, QuestLocation loc)
        {
            if (iIndex >= lvInfo.Items.Count)
                return;
            if (!UpdateInfo(lvInfo.Items[iIndex], loc))
                lvInfo.Items.RemoveAt(iIndex);
        }

        private void AddInfo(AddGoalType goal, QuestLocation loc, int iIcon = -1, bool bHighlightNearby = false)
        {
            ListViewItem lvi = GetInfo(goal, loc, iIcon, bHighlightNearby);
            if (lvi != null)
                lvInfo.Items.Add(lvi);
        }

        private void AddInfo(AddGoalType goal, BasicQuest quest, bool bHighlightNearby = false)
        {
            ListViewItem lvi = new ListViewItem(quest.Name);
            lvi.SubItems.Add("(see quest)");
            lvi.Tag = quest;
            if (quest.Status.Completed)
            {
                lvi.Font = new Font(lvInfo.Font, FontStyle.Strikeout);
                Properties.Settings.Default.UIElementOptions.SetElement(lvi, ColoredUIElements.CompletedQuestTask);
            }
            lvInfo.Items.Add(lvi);
        }

        private string LocationString(QuestLocation location)
        {
            if (location.Game == GameNames.None)
                return "<invalid location>";

            if (location.MapIndex == -1)
                return "";

            if (location.Location.X >= location.UnsummonedRange)
                return location.Active.IsComplete ? "dead" : "not summoned";

            string strEra = location.EraString;
            if (!String.IsNullOrWhiteSpace(strEra))
                strEra = ", " + strEra;

            if (location.HasLocation)
                return String.Format("{0} ({1},{2}){3}", m_main.GetMapName(location.MapIndex), location.Location.X, location.Location.Y, strEra);
            return String.Format("{0}{1}", m_main.GetMapName(location.MapIndex), strEra);
        }

        private void UpdateSelectedQuest(BasicQuest quest, bool bHighlightNearby = false)
        {
            m_nearbyLocations.Clear();

            m_iHiddenInvalid = 0;

            lvInfo.BeginUpdate();
            lvInfo.Items.Clear();

            // Don't highlight every single item if they are all on this map
            if (bHighlightNearby && quest.Secondary.All(q => q.MapIndex == m_lastInfo.MapIndex))
                bHighlightNearby = false;

            if (quest.Primary.HasLocation)
                AddInfo(AddGoalType.Primary, quest.Primary, -1, bHighlightNearby);

            if (quest.Status.PreQuest != null)
            {
                foreach (BasicQuest questPre in quest.Status.PreQuest)
                    AddInfo(AddGoalType.Quest, questPre, bHighlightNearby);
            }

            if (quest.Secondary != null)
            {
                for (int i = 0; i < quest.Secondary.Count; i++)
                {
                    QuestLocation loc = quest.Secondary[i];
                    AddInfo(AddGoalType.Secondary, loc, -1, bHighlightNearby);
                }
            }

            Global.SizeHeadersAndContent(lvInfo);

            if (quest.Name == m_strLastSelectedQuestName &&
                m_iLastSelectedQuestSubItem < lvInfo.Items.Count &&
                m_iLastSelectedQuestSubItem >= 0)
            {
                lvInfo.Items[m_iLastSelectedQuestSubItem].Selected = true;
                lvInfo.FocusedItem = lvInfo.Items[m_iLastSelectedQuestSubItem];
                lvInfo.EnsureVisible(m_iLastSelectedQuestSubItem);
            }

            m_strLastSelectedQuestName = quest.Name;

            lvInfo.EndUpdate();

            if (m_iHiddenInvalid > 0)
                chDescription.Text = String.Format("Description ({0} hidden)", m_iHiddenInvalid);
            else
                chDescription.Text = "Description";

            if (m_iLastListPos >= 0 && m_iLastListPos < lvInfo.Items.Count)
                lvInfo.TopItem = lvInfo.Items[m_iLastListPos];
        }

        public void ForceRefresh()
        {
            if (Hacker == null)
                return;

            m_lastInfo = null;
            UpdateUI(Hacker.GetQuestInfo(m_lastInfo, m_main.GetSelectedCharacterAddress(), m_bAutoAskFiles));
            m_bAutoAskFiles = false;    // Don't ask this question repeatedly
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                    if (tbFind.Focused || tbFindGoal.Focused)
                        return false;
                    m_main.SelectCharacter(keyData - Keys.D1);
                    Activate(); // for some reason selecting a character on the party window activates that window
                    return true;
                case (Keys.Q | Keys.Control):
                    FlagQuest(tvQuests.SelectedNode);
                    break;
                case Keys.F5:
                    ForceRefresh();
                    return true;
                default:
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cmQuests_Opening(object sender, CancelEventArgs e)
        {
            if (Hacker == null)
                return;

            bool bHasMap = true;
            if (lvInfo.SelectedItems.Count < 1)
                bHasMap = false;

            miQuestsGoToMap.Text = "&Go to this map";

            bool bAnySelected = lvInfo.SelectedItems.Count > 0;

            QuestLocation loc = bAnySelected ? lvInfo.SelectedItems[0].Tag as QuestLocation : null;
            if (loc == null)
                bHasMap = false;
            else if (loc.MapIndex == -1)
                bHasMap = false;
            else
            {
                DirectionsTo directions = Hacker.GetDirections(loc.MapIndex, loc.Location,
                    m_main.CurrentBook.Location.IncreaseY == AxisIncreaseY.BottomToTop,
                    m_main.CurrentBook.Location.IncreaseX == AxisIncreaseX.LeftToRight);
                if (directions.Possible)
                {
                    string strDir = directions.Total.ToString();
                    if (!String.IsNullOrWhiteSpace(strDir))
                        miQuestsGoToMap.Text += String.Format(" ({0})", strDir);
                }
            }

            miSelectedSelectGameFiles.Visible = loc != null && loc.Active.State == QuestStatus.Basic.NoFile;

            miQuestsToggleCompleted.Visible = (loc != null && loc.Active.IsManual);

            miQuestsGoToMap.Enabled = bHasMap;
            miQuestsSetBeacon.Enabled = Global.Cheats && bHasMap;
            miQuestsSetBeacon.Visible = Global.Cheats && Hacker.HasBeacon;
            miQuestsTeleport.Enabled = Global.Cheats && bHasMap && (m_lastInfo.MapIndex == loc.MapIndex);
            miQuestsTeleport.Visible = Global.Cheats;
            miQuestsSetSurface.Enabled = Global.Cheats && bHasMap;
            miQuestsSetSurface.Visible = Global.Cheats && Hacker.HasSurfaceLocation;
        }

        private void miQuestsGoToMap_Click(object sender, EventArgs e)
        {
            GoToSelectedItem();
        }

        private void GoToSelectedItem()
        {
            if (lvInfo.SelectedItems.Count < 1)
                return;

            if (lvInfo.SelectedItems[0].Tag is QuestLocation)
            {
                QuestLocation location = lvInfo.SelectedItems[0].Tag as QuestLocation;
                if (location == null)
                    return;

                if (location.MapIndex == -1)
                    return;

                m_main.GotoSheet(location.MapIndex);
                m_main.SetCursor(m_main.TranslateToInternalMap(location.Location, null));
            }
            else if (lvInfo.SelectedItems[0].Tag is BasicQuest)
            {
                BasicQuest quest = lvInfo.SelectedItems[0].Tag as BasicQuest;
                // Find this quest in the list and select it
                SelectNode(tvQuests.Nodes, quest);
            }
        }

        private void ToggleSelectedItem()
        {
            if (lvInfo.SelectedItems.Count < 1)
                return;

            QuestLocation loc = lvInfo.SelectedItems[0].Tag as QuestLocation;
            if (loc == null)
                return;

            if (!loc.Active.IsManual)
                return;

            bool bWasComplete = loc.Active.IsComplete;
            loc.Active.State = bWasComplete ? QuestStatus.Basic.ManualNotCompleted : QuestStatus.Basic.ManualCompleted;

            if (bWasComplete && m_manualCompletedTasks.Contains(loc.Path))
                m_manualCompletedTasks.Remove(loc.Path);
            else
                m_manualCompletedTasks.Add(loc.Path);

            UpdateInfo(lvInfo.SelectedItems[0].Index, loc);

            m_main.UpdateBookExtraData();
        }

        private bool SelectNode(IEnumerable nodeCollection, BasicQuest quest)
        {
            if (nodeCollection == null)
                return false;

            foreach (TreeNode tn in nodeCollection)
            {
                BasicQuest test = tn.Tag as BasicQuest;
                if (test != null && test.Name == quest.Name)
                {
                    tvQuests.SelectedNode = tn;
                    return true;
                }
                if (SelectNode(tn.Nodes, quest))
                    return true;
            }

            return false;
        }

        private void miQuestsSetBeacon_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvInfo.FocusedItem == null)
                return;

            QuestLocation location = lvInfo.FocusedItem.Tag as QuestLocation;
            if (location == null || location.MapIndex == -1)
                return;

            Hacker.SetBeacon(location.Location, location.MapIndex);
        }

        private void miQuestsRefresh_Click(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void lvInfo_DoubleClick(object sender, EventArgs e)
        {
            GoToSelectedItem();
        }

        private void cbHideCompleted_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideUnstarted_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cbHideAccepted_CheckedChanged(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private void cmAllQuestsRefresh_Click(object sender, EventArgs e)
        {
            ForceRefresh();
        }

        private TreeNode TopParent(TreeNode tn)
        {
            if (tn == null)
                return null;
            while (tn.Parent != null)
                tn = tn.Parent;
            return tn;
        }

        private void cmAllQuests_Opening(object sender, CancelEventArgs e)
        {
            bool bSelected = tvQuests.SelectedNode != null;
            bool bEnableBits = (tvQuests.SelectedNode.Tag is BasicQuest &&
                ((BasicQuest)tvQuests.SelectedNode.Tag).Bits != null &&
                !((BasicQuest)tvQuests.SelectedNode.Tag).Bits.IsEmpty);
            if (Hacker.QuestsNeedFiles)
                bEnableBits = false;    // setting and clearing the quest bits without the corresponding script bytes is a recipe for disaster
            if (bSelected && tvQuests.SelectedNode.Nodes != null && tvQuests.SelectedNode.Nodes.Count > 0)
                bEnableBits = false;    // Don't allow set and clear for entire quest trees

            miAllQuestsClear.Enabled = Global.Cheats && bSelected && bEnableBits;
            miAllQuestsSet.Enabled = Global.Cheats && bSelected && bEnableBits;
            miAllQuestsClear.Visible = Global.Cheats && bEnableBits;
            miAllQuestsSet.Visible = Global.Cheats && bEnableBits;
            miAllQuestsFlag.Visible = bSelected && (tvQuests.SelectedNode.Tag != null);
            miAllQuestsFlag.Enabled = bSelected && (tvQuests.SelectedNode.Tag != null);
            miAllQuestsExpand.Visible = bSelected && tvQuests.SelectedNode.Nodes != null && tvQuests.SelectedNode.Nodes.Count > 0;
            miAllQuestsCollapse.Visible = bSelected && tvQuests.SelectedNode.Nodes != null && tvQuests.SelectedNode.Nodes.Count > 0;
            miAllQuestsShowGoals.Checked = !splitContainer1.Panel2Collapsed;
            miAllQuestsSelectFiles.Visible = m_bUseFilesLink;
            miAllQuestsMarkComplete.Enabled = bSelected && (tvQuests.SelectedNode.Tag is BasicQuest);
            miAllQuestsMarkComplete.Visible = bSelected && (tvQuests.SelectedNode.Tag is BasicQuest);

            if (bSelected)
            {
                if (tvQuests.SelectedNode == tnFlagged)
                    miAllQuestsFlag.Text = "Un&flag all quests";
                else if (tvQuests.SelectedNode.Tag is QuestNodeTag)
                    miAllQuestsFlag.Visible = false;
                else if (TopParent(tvQuests.SelectedNode) == tnFlagged)
                    miAllQuestsFlag.Text = "Un&flag this quest";
                else
                    miAllQuestsFlag.Text = "&Flag this quest";

                BasicQuest tag = tvQuests.SelectedNode.Tag as BasicQuest;
                if (tag != null)
                {
                    if (tag.Status.Main == QuestStatus.Basic.ManualCompleted)
                        miAllQuestsMarkComplete.Text = "Remove manual &completion status";
                    else
                        miAllQuestsMarkComplete.Text = "&Complete quest manually";
                }
            }
        }

        private void miAllQuestsClear_Click(object sender, EventArgs e)
        {
            // Set all of the bits associated with this quest to 0
            if (tvQuests.SelectedNode == null)
                return;

            BasicQuest quest = tvQuests.SelectedNode.Tag as BasicQuest;
            if (quest == null || quest.Bits == null)
                return;

            Hacker.SetQuestBits(m_lastInfo.CharAddress, quest.Bits, false);
        }

        private void miAllQuestsSet_Click(object sender, EventArgs e)
        {
            // Set all of the bits associated with this quest to 1 (does not provide quest items)
            if (tvQuests.SelectedNode == null)
                return;

            BasicQuest quest = tvQuests.SelectedNode.Tag as BasicQuest;
            if (quest == null || quest.Bits == null)
                return;

            Hacker.SetQuestBits(m_lastInfo.CharAddress, quest.Bits, true);
        }

        private void miQuestsTeleport_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvInfo.FocusedItem == null)
                return;

            QuestLocation location = lvInfo.FocusedItem.Tag as QuestLocation;
            if (location == null || location.MapIndex == -1)
                return;

            Hacker.SetLocation(location.Location);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (tbFindGoal.Focused)
            {
                m_findBoxGoal.HideFindBox();
                lvInfo.Focus();
            }
            else if (tbFind.Focused)
            {
                m_findBox.HideFindBox();
                tvQuests.Focus();
            }
            else
                Close();
        }

        private void miQuestsSetSurface_Click(object sender, EventArgs e)
        {
            if (Hacker == null)
                return;

            if (!Global.Cheats)
                return;

            if (lvInfo.FocusedItem == null)
                return;

            QuestLocation location = lvInfo.FocusedItem.Tag as QuestLocation;
            if (location == null || location.MapIndex == -1)
                return;

            MMExit exit = new MMExit(MMExitDirection.Surface, location.MapIndex, location.Location);
            Hacker.SetExit(exit);
        }

        private void tvQuests_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BasicQuest quest = e.Node.Tag as BasicQuest;
            if (quest == null)
                return; // probably a root node

            TreeNode tnTop = TopParent(e.Node);
            UpdateSelectedQuest(quest, m_bBoldNearby && (tnTop == tnNearby || tnTop == tnFlagged));
        }

        private void tvQuests_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvQuests.SelectedNode = e.Node;
        }

        private void lvInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInfo.SelectedIndices.Count > 0)
                m_iLastSelectedQuestSubItem = lvInfo.SelectedIndices[0];
            else
                m_iLastSelectedQuestSubItem = -1;
        }

        private void SelectFiles()
        {
            Hacker.SelectGameFiles();
            ForceRefresh();
        }

        private void miAllQuestsFlag_Click(object sender, EventArgs e)
        {
            FlagQuest(tvQuests.SelectedNode);
        }

        private void FlagQuest(TreeNode tn)
        {
            if (tn == null)
                return;

            if (tn == tnFlagged)
            {
                if (tnFlagged.Nodes.Count == 0)
                    return;
                if (MessageBox.Show(String.Format("Do you want to remove the {0} from the list?", Global.Plural(tnFlagged.Nodes.Count, "flagged quest")), "Remove flagged quests?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    m_flaggedQuests.Clear();
                    m_main.UpdateBookExtraData();
                    ForceRefresh();
                }
                return;
            }

            if (!(tvQuests.SelectedNode.Tag is BasicQuest))
                return;

            string strName = (tvQuests.SelectedNode.Tag as BasicQuest).Name;

            if (TopParent(tvQuests.SelectedNode) == tnFlagged)
            {
                m_flaggedQuests.Remove(strName);
                m_main.UpdateBookExtraData();
                ForceRefresh();
            }
            else if (!m_flaggedQuests.Contains(strName))
            {
                m_flaggedQuests.Add(strName);
                m_main.UpdateBookExtraData();
                ForceRefresh();
            }
        }

        private void AddParentsDict(Dictionary<string, bool> dict, TreeNodeCollection tnc)
        {
            if (tnc == null || tnc.Count == 0)
                return;

            foreach (TreeNode node in tnc)
            {
                if (node.Nodes == null || node.Nodes.Count == 0)
                    continue;

                dict.Add(node.FullPath, node.IsExpanded);
                AddParentsDict(dict, node.Nodes);
            }
        }

        private Dictionary<string, bool> CreateParentsDict()
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool>();
            AddParentsDict(dict, tvQuests.Nodes);
            return dict;
        }

        private void tvQuests_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Nodes == null || m_bExpanding)
                return;

            m_bExpanding = true;
            ExpandSmallNodes(e.Node.Nodes, null);
            m_bExpanding = false;
        }

        private void ExpandSmallNodes(TreeNodeCollection tnc, HashSet<string> paths)
        {
            if (tnc == null)
                return;

            if (tnc.Count == 1)
            {
                tnc[0].Expand();
                return;
            }

            foreach (TreeNode node in tnc)
            {
                if (!node.IsExpanded && node.Nodes != null && 
                    (node.Nodes.Count < 3 || (paths != null && paths.Contains(NodePath(node)))) )
                    node.Expand();
                else
                    ExpandSmallNodes(node.Nodes, paths);
            }
        }

        private void miAllQuestsExpand_Click(object sender, EventArgs e)
        {
            if (tvQuests.SelectedNode != null)
                tvQuests.SelectedNode.ExpandAll();
        }

        private void miQuestsHideInvalid_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.HideInvalidQuestGoals = !Properties.Settings.Default.HideInvalidQuestGoals;
            miQuestsHideInvalid.Checked = Properties.Settings.Default.HideInvalidQuestGoals;
            ForceRefresh();
        }

        private void miAllQuestsCollapse_Click(object sender, EventArgs e)
        {
            if (tvQuests.SelectedNode != null)
                CollapseAll(tvQuests.SelectedNode);
        }

        private void CollapseAll(TreeNode node)
        {
            if (node == null)
                return;

            node.Collapse();

            if (node.Nodes == null || node.Nodes.Count < 1)
                return;

            foreach (TreeNode nodeSub in node.Nodes)
                CollapseAll(nodeSub);
        }

        private void ShowGoals(bool bShow)
        {
            splitContainer1.Panel2Collapsed = !bShow;
        }

        private void miAllQuestsHideGoals_Click(object sender, EventArgs e)
        {
            ShowGoals(splitContainer1.Panel2Collapsed);
        }

        private void miAllQuestsSelectFiles_Click(object sender, EventArgs e)
        {
            SelectFiles();
        }

        private void lvInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GoToSelectedItem();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_fontNormal != null)
                    m_fontNormal.Dispose();
            }
            base.Dispose(disposing);
        }

        private void miQuestsToggleCompleted_Click(object sender, EventArgs e)
        {
            ToggleSelectedItem();
        }

        private void miAllQuestsMarkComplete_Click(object sender, EventArgs e)
        {
            BasicQuest tag = tvQuests.SelectedNode.Tag as BasicQuest;
            if (tag == null)
                return;

            if (m_manualCompletedQuests.Contains(tag.Name))
                m_manualCompletedQuests.Remove(tag.Name);
            else
                m_manualCompletedQuests.Add(tag.Name);

            ForceRefresh();

            m_main.UpdateBookExtraData();
        }
    }

    class QuestComparer : BasicListViewComparer
    {
        public QuestComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            BasicQuest quest1 = ((ListViewItem)x).Tag as BasicQuest;
            BasicQuest quest2 = ((ListViewItem)y).Tag as BasicQuest;

            switch (m_column)
            {
                case 0: return Order(String.Compare(quest1.Name, quest2.Name));
                case 1: return Order(String.Compare(quest1.QuestTypeString, quest2.QuestTypeString));
                case 2: return Order(String.Compare(quest1.StateString, quest2.StateString));
                case 3: return Order(String.Compare(quest1.Giver, quest2.Giver));
                case 4: return Order(String.Compare(quest1.Reward, quest2.Reward));
                default: return base.Compare(x, y);
            }
        }
    }

    public class QuestNodeTag
    {
        public int Count;

        public QuestNodeTag(int count)
        {
            Count = count;
        }

        public QuestNodeTag(BasicQuest quest, string path)
        {
            Count = 0;
        }
    }

    public class QuestWindowParams
    {
        public string[] Flagged;

        public QuestWindowParams(string[] flagged)
        {
            Flagged = flagged;
        }
    }

    public class DBTreeView : System.Windows.Forms.TreeView
    {
        public DBTreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, (IntPtr)NativeMethods.TVS_EX_DOUBLEBUFFER, (IntPtr)NativeMethods.TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
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
                NativeMethods.SetScrollPos(Handle, NativeMethods.SB_HORZ, value.X, true);
                NativeMethods.SetScrollPos(Handle, NativeMethods.SB_VERT, value.Y, true);
            }
        }
    }
}

