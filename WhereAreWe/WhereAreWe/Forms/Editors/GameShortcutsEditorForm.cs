using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class GameShortcutsEditorForm : Form
    {
        private GameStrings m_paths;

        public GameShortcutsEditorForm()
        {
            InitializeComponent();
            m_paths = null;
        }

        public void SetPaths(GameStrings paths)
        {
            m_paths = paths;
        }

        public GameStrings GetPaths()
        {
            return m_paths;
        }

        private void cmListView_Opening(object sender, CancelEventArgs e)
        {
            miBrowseShortcut.Enabled = (lvPaths.FocusedItem != null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void UpdatePathsFromUI()
        {
            foreach (ListViewItem lvi in lvPaths.Items)
            {
                MapSheetPathInfo tag = (MapSheetPathInfo)lvi.Tag;
                tag.Path = lvi.Text;
                tag.Index = lvi.Index;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvPaths_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
        }

        private void lvPaths_DoubleClick(object sender, EventArgs e)
        {
            if (lvPaths.FocusedItem != null)
                BrowsePath();
        }

        private void lvPaths_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    if (lvPaths.FocusedItem != null)
                        lvPaths.FocusedItem.BeginEdit();
                    break;
                default:
                    break;
            }
        }

        private void GameShortcutsEditorForm_Load(object sender, EventArgs e)
        {
            foreach(KeyValuePair<GameNames, string> pair in m_paths.Strings)
            {
                ListViewItem lvi = new ListViewItem(pair.Value);
                lvi.SubItems.Add(Games.Name(pair.Key));
                lvi.Tag = pair.Key;
                lvPaths.Items.Add(lvi);
            }
        }

        private void miBrowseShortcut_Click(object sender, EventArgs e)
        {
            BrowsePath();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            BrowsePath();
        }

        private void BrowsePath()
        {
            if (lvPaths.SelectedItems.Count < 1)
                return;

            GameNames game = (GameNames)lvPaths.SelectedItems[0].Tag;
            string strPath = m_paths.Get(game);
            if (strPath != "<Not Found>")
            {
                try
                {
                    ofdBrowseShortcut.FileName = Path.GetFileName(strPath);
                    ofdBrowseShortcut.InitialDirectory = Path.GetDirectoryName(strPath);
                }
                catch (Exception)
                {
                    // No initial directory/filename, not a problem
                }
            }
            if (ofdBrowseShortcut.ShowDialog() == DialogResult.OK)
            {
                m_paths.Set(game, ofdBrowseShortcut.FileName);
                lvPaths.SelectedItems[0].Text = m_paths.Get(game);
            }
        }

        private void lvPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBrowse.Enabled = (lvPaths.SelectedItems.Count > 0);
        }
    }
}
