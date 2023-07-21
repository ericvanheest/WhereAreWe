using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditNotificationForm : Form
    {
        private Notification m_notification;
        private int m_iVarHeight = 0;
        private int m_iMinHeight = 0;
        private int m_iSplitPos = 0;

        public EditNotificationForm()
        {
            InitializeComponent();
            m_iSplitPos = splitContainer1.SplitterDistance;
            m_iMinHeight = MinimumSize.Height;
            ShowVariables = false;
            FillVariablesList();
        }

        public Action Action { get; set; }

        public Notification Notification
        {
            get { m_notification = GetNotificationFromUI(); return m_notification; }
            set { m_notification = value; UpdateUI();  }
        }

        public bool ShowVariables
        {
            get { return !splitContainer1.Panel2Collapsed; }
            set
            {
                if (splitContainer1.Panel2Collapsed)
                {
                    Height = m_iVarHeight;
                    splitContainer1.Panel2Collapsed = false;
                    Global.SetSplitterDistance(splitContainer1, m_iSplitPos);
                    llVariables.Text = "Hide &Variables";
                }
                else
                {
                    m_iVarHeight = Height;
                    Height = m_iMinHeight;
                    splitContainer1.Panel2Collapsed = true;
                    llVariables.Text = "Show &Variables";
                }
            }
        }

        public Notification GetNotificationFromUI()
        {
            Notification.AlertType type = Notification.AlertType.None;
            if (cbMessage.Checked && cbAudioFile.Checked)
                type = Notification.AlertType.Both;
            else if (cbMessage.Checked)
                type = Notification.AlertType.Text;
            else if (cbAudioFile.Checked)
                type = Notification.AlertType.Audio;

            return new Notification(type, tbMessage.Text, tbAudioFile.Text);
        }

        public void UpdateUI()
        {
            labelAction.Text = Global.ActionString(Action);
            cbAudioFile.Checked = (m_notification.Type == Notification.AlertType.Audio || m_notification.Type == Notification.AlertType.Both);
            cbMessage.Checked = (m_notification.Type == Notification.AlertType.Text || m_notification.Type == Notification.AlertType.Both);
            tbMessage.Text = m_notification.Message;
            tbAudioFile.Text = m_notification.AudioFile;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdAudioFile.ShowDialog() == DialogResult.OK)
            {
                tbAudioFile.Text = ofdAudioFile.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void llVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowVariables = !ShowVariables;
        }

        private void AddVariable(string strVar, string strDescription)
        {
            ListViewItem lvi = new ListViewItem(strVar);
            lvi.SubItems.Add(strDescription);
            lvi.Tag = strVar;
            lvVariables.Items.Add(lvi);
        }

        private void FillVariablesList()
        {
            lvVariables.BeginUpdate();
            lvVariables.Items.Clear();
            AddVariable("$curChar", "Currently-selected character's name");
            AddVariable("$actionChar", "Identity of the character(s) referenced by the action");
            AddVariable("$readySpell", "Currently-selected character's ready spell");
            AddVariable("$spellName", "Spell name referenced by the action");
            AddVariable("$spellAction", "The action referenced by the spell hotkey");
            AddVariable("$curGame", "Name of the currently-playing game");
            AddVariable("$curMap", "Name of the map in which the party is located");
            AddVariable("$curCoord", "Coordinates of the square in which the party is located");
            AddVariable("$curLocation", "Map and coordinates of the party's current location");
            AddVariable("$curMouse", "Coordinates of the square under the mouse cursor");
            AddVariable("$curCursor", "Coordinates of the current keyboard mode cursor");
            AddVariable("$curZoom", "Current zoom level");
            AddVariable("$curSquareSize", "Size of the map squares at the current zoom level");
            AddVariable("$curSheetSize", "Size of the current map sheet");
            AddVariable("$curSheets", "Number of sheets in the current map book");
            AddVariable("$bookTitle", "Title of the current map book");
            AddVariable("$enabledState", "\"enabled\" or \"disabled\" depending on the action");
            AddVariable("$successState", "\"succeeded\" or \"failed\" depending on the action");
            AddVariable("$mode", "The name of the current editing mode");
            AddVariable("$fileName", "The name of the current .WAW file");
            AddVariable("$curSelection", "The width and height of the current edit mode selection");
            AddVariable("$character1", "Name of the the first character in the party");
            AddVariable("$character2", "Name of the the second character in the party");
            AddVariable("$character3", "Name of the the third character in the party");
            AddVariable("$character4", "Name of the the fourth character in the party");
            AddVariable("$character5", "Name of the the fifth character in the party");
            AddVariable("$character6", "Name of the the sixth character in the party");
            AddVariable("$character7", "Name of the the seventh character in the party");
            AddVariable("$character8", "Name of the the eighth character in the party");
            AddVariable("$ready1", "Name of the the first character's ready spell");
            AddVariable("$ready2", "Name of the the second character's ready spell");
            AddVariable("$ready3", "Name of the the third character's ready spell");
            AddVariable("$ready4", "Name of the the fourth character's ready spell");
            AddVariable("$ready5", "Name of the the fifth character's ready spell");
            AddVariable("$ready6", "Name of the the sixth character's ready spell");
            AddVariable("$ready7", "Name of the the seventh character's ready spell");
            AddVariable("$ready8", "Name of the the eighth character's ready spell");
            lvVariables.EndUpdate();
        }

        private void cbMessage_CheckedChanged(object sender, EventArgs e)
        {
            tbMessage.Enabled = cbMessage.Checked;
        }

        private void cbAudioFile_CheckedChanged(object sender, EventArgs e)
        {
            tbAudioFile.Enabled = cbAudioFile.Checked;
            btnBrowse.Enabled = cbAudioFile.Checked;
        }

        private void lvVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lvVariables.SelectedItems.Count < 0)
                return;

            if (!tbMessage.Enabled)
                return;

            if (tbMessage.SelectionStart < 0 || tbMessage.SelectionStart >= tbMessage.Text.Length)
                tbMessage.Text += (string)lvVariables.SelectedItems[0].Tag;
            else
                tbMessage.Text = String.Format("{0}{1}{2}",
                    tbMessage.Text.Substring(0, tbMessage.SelectionStart),
                    (string)lvVariables.SelectedItems[0].Tag,
                    tbMessage.Text.Substring(tbMessage.SelectionStart + tbMessage.SelectionLength));
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Notification alert = GetNotificationFromUI();
            // Special case for $successState, $enabledState
            if (alert.AudioFile.Contains("$successState"))
                alert.PlayAudio(true, alert.AudioFile.Replace("$successState", "succeeded"));
            else if (alert.AudioFile.Contains("$enabledState"))
                alert.PlayAudio(true, alert.AudioFile.Replace("$enabledState", "enabled"));
            else
                alert.PlayAudio(true);
        }
    }
}
