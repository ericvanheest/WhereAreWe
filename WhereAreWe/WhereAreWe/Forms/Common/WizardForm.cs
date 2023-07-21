using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WhereAreWe
{
    public partial class WizardForm : Form
    {
        public enum Styles
        {
            Unknown,
            Minimal,
            FaqStyle,
            FullVisibility
        }

        public const string DefaultPath = @"C:\Games";

        // Keys are all in HKEY_LOCAL_MACHINE
        public const string GogRoot = @"SOFTWARE\Gog.com";
        public const string GogDefaultPackPath = "DefaultPackPath";
        public const string GogGamePath = "PATH";
        public const string NotFound = "<Not Found>";

        public GameNames LaunchGame = GameNames.None;

        private bool m_bFinish = false;
        private bool m_bUpdatePaths = false;
        private bool m_bWarnOnCancel = true;

        public WizardForm()
        {
            InitializeComponent();
            rbMinimalHelp.Checked = true;
            btnBack.Enabled = false;
        }

        public WizardForm(int iStartPage)
        {
            InitializeComponent();
            btnBack.Enabled = false;
            tcWizard.SelectedIndex = iStartPage;
            if (iStartPage == 1)
            {
                m_bWarnOnCancel = false;
                UpdateStyleFromSettings();
            }
            else
                SelectFocusCheck(rbMinimalHelp);
            UpdateButtons();
        }

        private void miPathsBrowse_Click(object sender, EventArgs e)
        {
            BrowseShortcut();
        }

        private void BrowseShortcut()
        {
            if (lvGamePaths.SelectedItems.Count < 1)
                return;

            ListViewItem lvi = lvGamePaths.SelectedItems[0];
            GamePathTag tag = lvi.Tag as GamePathTag;
            if (tag.Valid)
            {
                ofdBrowseShortcut.FileName = tag.Path;
                ofdBrowseShortcut.InitialDirectory = Path.GetDirectoryName(tag.Path);
            }
            if (ofdBrowseShortcut.ShowDialog() == DialogResult.OK)
            {
                tag.Path = ofdBrowseShortcut.FileName;
                tag.Valid = true;
                lvi.Text = tag.Path;
                UpdateLaunchBox();
            }
        }

        private void cmPaths_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvGamePaths.SelectedItems.Count > 0;
            miPathsBrowse.Enabled = bAnySelected;
            miPathsLaunch.Enabled = bAnySelected;
        }

        private void lvGamePaths_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BrowseShortcut();
        }

        private void WizardForm_Load(object sender, EventArgs e)
        {
            if (tcWizard.SelectedIndex == 0)
                FindGamePaths();
        }

        private void FindGamePaths()
        {
            lvGamePaths.BeginUpdate();
            lvGamePaths.Items.Clear();
            foreach(GameNames game in Games.ImplementedGames)
                AddFoundGamePath(FindGamePath(game, Games.GetRegistry(game), Games.GetDefaultLink(game)));
            lvGamePaths.EndUpdate();
            m_bUpdatePaths = true;
            UpdateLaunchBox();
        }

        private void AddFoundGamePath(GamePathTag tag)
        {
            ListViewItem lvi = new ListViewItem(tag.Path);
            lvi.SubItems.Add(Games.Name(tag.Game));
            lvi.Tag = tag;
            lvGamePaths.Items.Add(lvi);
        }

        private GamePathTag FindGamePath(GameNames game, string strReg, string strLink)
        {
            if (String.IsNullOrWhiteSpace(strReg))
                return new GamePathTag(Path.Combine(DefaultPath, strLink), game);
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                RegistryKey keySub = key.OpenSubKey(GogRoot + "\\" + strReg, false);
                if (keySub == null)
                {
                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    keySub = key.OpenSubKey(GogRoot + "\\" + strReg, false);
                }
                if (keySub != null)
                {
                    string strPath = keySub.GetValue(GogGamePath) as string;
                    keySub.Close();
                    key.Close();

                    string strFullPath = Path.Combine(strPath, strLink);
                    if (File.Exists(strFullPath))
                        return new GamePathTag(strFullPath, game);

                    foreach (string str in Directory.GetFiles(strPath, "*Might and Magic*.lnk"))
                    {
                        if (File.Exists(str))
                            return new GamePathTag(Path.Combine(strPath, str), game);
                    }
                }
            }
            catch (Exception)
            {
            }
            return new GamePathTag(NotFound, game, false);
        }

        private void miPathsLaunch_Click(object sender, EventArgs e)
        {
            if (lvGamePaths.SelectedItems.Count < 1)
                return;
            ListViewItem lvi = lvGamePaths.SelectedItems[0];
            GamePathTag tag = lvi.Tag as GamePathTag;
            if (!tag.Valid || !File.Exists(tag.Path))
            {
                MessageBox.Show("The selected item is not valid.", "Invalid shortcut path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process proc = new Process();
            proc.StartInfo.FileName = tag.Path;
            if (!proc.Start())
            {
                MessageBox.Show("The system could not start the process", "Unable to launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("The system reports that the process started successfully", "Launch successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (tcWizard.SelectedIndex > 0)
                tcWizard.SelectedIndex--;

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            btnBack.Enabled = tcWizard.SelectedIndex > 0;
            if (tcWizard.SelectedIndex == tcWizard.TabCount - 1)
                btnNext.Text = "Fi&nish";
            else
                btnNext.Text = "&Next >>";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tcWizard.SelectedIndex == tcWizard.TabCount - 1)
                Finish();

            if (tcWizard.SelectedIndex < tcWizard.TabCount)
                tcWizard.SelectedIndex++;

            UpdateButtons();
        }

        private void SelectFocusCheck(RadioButton rb)
        {
            rb.Checked = true;
            rb.Focus();
            rb.Select();
        }

        private void UpdateStyleFromSettings()
        {
            cbTrainer.Checked = Properties.Settings.Default.EnableCheats;
            Styles style = GetStyleFromSettings();

            switch (style)
            {
                case Styles.Minimal:
                    SelectFocusCheck(rbMinimalHelp);
                    break;
                case Styles.FaqStyle:
                    SelectFocusCheck(rbFaqStyle);
                    break;
                case Styles.FullVisibility:
                    SelectFocusCheck(rbFullVisiblity);
                    break;
            }
        }

        private static Styles GetStyleFromSettings()
        {
            if (!Properties.Settings.Default.ShowUnvisitedNotes && Properties.Settings.Default.HideUnvisitedSquares)
                return Styles.Minimal;
            else if (Properties.Settings.Default.HideUnvisitedSquares)
                return Styles.FaqStyle;
            return Styles.FullVisibility;
        }

        private static void SetCommon()
        {
            Properties.Settings.Default.RevealAdjacentInaccessible = true;
            Properties.Settings.Default.AlwaysRevealEdges = true;
            Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed = true;
            Properties.Settings.Default.ShowActiveSquares = true;
            Properties.Settings.Default.ShowActiveEncountersOnly = true;
            Properties.Settings.Default.CureAllHPWithConditions = true;
            Properties.Settings.Default.ShowEncounters = true;
            Properties.Settings.Default.ShowTreasureWindow = true;
            Properties.Settings.Default.ShowDeadMonsters = false;
            Properties.Settings.Default.EnableMemoryWrite = true;
            Properties.Settings.Default.ShowActivatedMonstersIcon = true;
            Properties.Settings.Default.HideScriptMonsters = true;
            Properties.Settings.Default.UseInGameCartography = true;
            Properties.Settings.Default.ShowMonstersOnMaps = true;
            Properties.Settings.Default.ShowMapLabels = true;
            Properties.Settings.Default.SeenSquareOpacity = 50;
        }

        public static void SetMinimal()
        {
            SetCommon();

            Properties.Settings.Default.ShowUnvisitedNotes = false;
            Properties.Settings.Default.UnvisitedSquareOpacity = 100;
            Properties.Settings.Default.RevealTeleports = false;
            Properties.Settings.Default.HideUnvisitedDottedLines = true;

            Properties.Settings.Default.HideUnvisitedSquares = true;
            Properties.Settings.Default.ShowListMonstersUnexplored = false;
            Properties.Settings.Default.ShowOnlyDetectableMonsters = true;
            Properties.Settings.Default.RevealSeenSquares = true;
        }

        public static void SetFaqStyle()
        {
            SetCommon();

            Properties.Settings.Default.ShowUnvisitedNotes = true;
            Properties.Settings.Default.UnvisitedSquareOpacity = 60;
            Properties.Settings.Default.RevealTeleports = true;
            Properties.Settings.Default.HideUnvisitedDottedLines = false;

            Properties.Settings.Default.HideUnvisitedSquares = true;
            Properties.Settings.Default.ShowListMonstersUnexplored = false;
            Properties.Settings.Default.ShowOnlyDetectableMonsters = true;
            Properties.Settings.Default.RevealSeenSquares = false;
        }

        public static void SetFullInformation()
        {
            SetCommon();

            Properties.Settings.Default.ShowUnvisitedNotes = true;
            Properties.Settings.Default.UnvisitedSquareOpacity = 100;
            Properties.Settings.Default.RevealTeleports = false;    // no need in this mode
            Properties.Settings.Default.HideUnvisitedDottedLines = false;

            Properties.Settings.Default.HideUnvisitedSquares = false;
            Properties.Settings.Default.ShowListMonstersUnexplored = true;
            Properties.Settings.Default.ShowOnlyDetectableMonsters = false;
            Properties.Settings.Default.RevealSeenSquares = false;
        }

        public static void CycleStyles()
        {
            Styles style = GetStyleFromSettings();
            switch (style)
            {
                case Styles.FaqStyle:
                    SetFullInformation();
                    break;
                case Styles.FullVisibility:
                    SetMinimal();
                    break;
                default:
                    SetFaqStyle();
                    break;
            }
        }

        private void Finish()
        {
            DialogResult = DialogResult.OK;
            m_bFinish = true;

            if (rbMinimalHelp.Checked)
                SetMinimal();
            else if (rbFaqStyle.Checked)
                SetFaqStyle();
            else
                SetFullInformation();

            Properties.Settings.Default.EnableCheats = cbTrainer.Checked;

            Close();
        }

        private void miPathsRescan_Click(object sender, EventArgs e)
        {
            FindGamePaths();
        }

        private void tcWizard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcWizard.SelectedIndex == 0 && !m_bUpdatePaths)
                FindGamePaths();
            UpdateButtons();
        }

        private void WizardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_bFinish)
            {
                if (m_bWarnOnCancel && MessageBox.Show("Are you sure you want to cancel the setup wizard?  " +
                    "You may run it again at any time by selecting \"Run setup wizard\" from the \"Help\" menu.",
                    "Cancel setup wizard?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }

            if (m_bUpdatePaths)
            {
                bool bShowErrors = true;
                foreach (ListViewItem lvi in lvGamePaths.Items)
                {
                    GamePathTag tag = lvi.Tag as GamePathTag;
                    if (tag == null)
                        continue;

                    Properties.Settings.Default.AutoLaunchShortcuts.Set(tag.Game, tag.Path);
                    try
                    {
                        if (cbForceWindowed.Checked && tag.Path != NotFound)
                            ForceWindowed(tag.Path);
                    }
                    catch (Exception ex)
                    {
                        if (bShowErrors)
                            MessageBox.Show("The DOSBox config files could not be automatically modified.  You may wish to manually set them to windowed mode\r\n\r\n" +
                                "Exception: " + ex.Message, "Error modifying DOSBox config files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bShowErrors = false;
                    }
                }

                if (comboLaunch.SelectedItem != null)
                {
                    LaunchGame = (comboLaunch.SelectedItem as GamePathTag).Game;
                }

                foreach (GameNames game in Games.WizardryGames)
                {
                    if (Properties.Settings.Default.AutoLaunchShortcuts.ContainsKey(game))
                    {
                        string strPath = Properties.Settings.Default.AutoLaunchShortcuts.Get(game);
                        if (strPath != NotFound)
                        {
                            string strDir = Path.GetDirectoryName(strPath);
                            if (!String.IsNullOrWhiteSpace(strDir))
                                Games.SetRosterPath(game, strDir);
                        }
                    }
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void UpdateLaunchBox()
        {
            GamePathTag tagSelected = comboLaunch.SelectedItem as GamePathTag;

            comboLaunch.Items.Clear();

            comboLaunch.Items.Add(new GamePathTag(String.Empty, GameNames.None, true));
            foreach (ListViewItem lvi in lvGamePaths.Items)
            {
                GamePathTag tag = lvi.Tag as GamePathTag;
                if (tag == null || String.IsNullOrWhiteSpace(tag.Path) || tag.Game == GameNames.None || !tag.Valid)
                    continue;

                comboLaunch.Items.Add(tag);
            }

            try
            {
                if (tagSelected == null)
                    comboLaunch.SelectedIndex = comboLaunch.Items.Count < 2 ? 0 : 1;
                else if (tagSelected.Game == GameNames.None)
                    comboLaunch.SelectedIndex = 0;
                else
                    comboLaunch.SelectedItem = tagSelected;

                if (comboLaunch.SelectedItem == null)
                    comboLaunch.SelectedIndex = comboLaunch.Items.Count < 2 ? 0 : 1;
            }
            catch (Exception)
            {
                comboLaunch.SelectedIndex = comboLaunch.Items.Count < 2 ? 0 : 1;
            }
        }

        private void ForceWindowed(string strPath)
        {
            // Changes "fullscreen=true" to "fullscreen=false" in any dosbox*.conf files found in the target directory
            string strDir = Path.GetDirectoryName(strPath);
            foreach (string strFile in Directory.GetFiles(strDir, "dosbox*.conf"))
            {
                if (new FileInfo(strFile).Length > 1000000)
                    continue;   // This is way too large to be the correct file

                string strConfigFile = File.ReadAllText(strFile);
                strConfigFile = strConfigFile.Replace(@"fullscreen=true", @"fullscreen=false");
                File.WriteAllText(strFile, strConfigFile);
            }
        }

        private void labelMinimal_Click(object sender, EventArgs e)
        {
            SelectFocusCheck(rbMinimalHelp);
        }

        private void labelFAQ_Click(object sender, EventArgs e)
        {
            SelectFocusCheck(rbFaqStyle);
        }

        private void labelFullVisibility_Click(object sender, EventArgs e)
        {
            SelectFocusCheck(rbFullVisiblity);
        }

        private void labelTrainer_Click(object sender, EventArgs e)
        {
            cbTrainer.Checked = !cbTrainer.Checked;
        }
    }

    public class GamePathTag
    {
        public bool Valid;
        public string Path;
        public GameNames Game;

        public GamePathTag(string path, GameNames game, bool valid = true)
        {
            Path = path;
            Game = game;
            Valid = valid;
        }

        public override string ToString()
        {
            return Games.Name(Game);
        }
    }
}
