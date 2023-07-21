using System;
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
    public partial class EditCartographyForm : HackerBasedForm
    {
        public int m_iHeightZeroFiles;
        public int m_iHeightOneFile;
        public int m_iHeightTwoFiles;

        public MapCartography.EditAction Action
        {
            get
            {
                if (rbClearCurrent.Checked)
                    return MapCartography.EditAction.ClearSingle;
                if (rbFillCurrent.Checked)
                    return MapCartography.EditAction.FillSingle;
                if (rbClearAll.Checked)
                    return MapCartography.EditAction.ClearAll;
                if (rbFillAll.Checked)
                    return MapCartography.EditAction.FillAll;
                return MapCartography.EditAction.None;
            }
        }

        public EditCartographyForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (tbFile1.Enabled && File.Exists(tbFile1.Text))
                Hacker.CurrentDataFiles = new string[] { tbFile1.Text, tbFile2.Text };

            switch (Action)
            {
                case MapCartography.EditAction.ClearAll:
                case MapCartography.EditAction.FillAll:
                    if (MessageBox.Show("This will change the in-game cartography data for every map in the entire game.  Are you sure you want to do this?",
                        "Change all maps?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;
                    break;
                default:
                    break;
            }
            Hacker.EditMapCartography(Action);

            Close();
        }

        private void rbClearCurrent_CheckedChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void rbFillCurrent_CheckedChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void rbClearAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void rbFillAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void CheckWarning(Label label, string strFile, int iExpectedSize)
        {
            if (!File.Exists(strFile))
                label.Text = "Warning: File does not exist";
            else if (new FileInfo(strFile).Length != iExpectedSize)
                label.Text = String.Format("Warning: File is not the correct size ({0})", iExpectedSize);
            else
            {
                label.Visible = false;
                return;
            }
            label.Visible = true;
        }

        private void CheckEnabled()
        {
            if (rbClearAll.Checked || rbFillAll.Checked)
            {
                btnBrowse1.Enabled = !splitContainer1.Panel2Collapsed;
                tbFile1.Enabled = !splitContainer1.Panel2Collapsed;
                if (tbFile1.Enabled && !splitContainer1.Panel2Collapsed)
                    CheckWarning(labelWarning1, tbFile1.Text, Hacker.Game == GameNames.MightAndMagic3 ? MM3RosterFile.ExpectedLength : MM4RosterFile.ExpectedLength);
                else
                    labelWarning1.Visible = false;
                btnBrowse2.Enabled = SecondFileVisible;
                tbFile2.Enabled = SecondFileVisible;
                if (tbFile2.Enabled && SecondFileVisible)
                    CheckWarning(labelWarning2, tbFile2.Text, MM5RosterFile.ExpectedLength);
                else
                    labelWarning2.Visible = false;
            }
            else
            {
                btnBrowse1.Enabled = false;
                tbFile1.Enabled = false;
                labelWarning1.Visible = false;
                btnBrowse2.Enabled = false;
                tbFile2.Enabled = false;
                labelWarning2.Visible = false;
            }
        }

        private void ShowSecondFile(bool bShow)
        {
            btnBrowse2.Visible = bShow;
            tbFile2.Visible = bShow;
            labelFile2.Visible = bShow;
            labelWarning2.Visible = bShow;
        }

        private bool SecondFileVisible { get { return tbFile2.Visible; } }

        private void tbCurrentFile_TextChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofdDataFile.FileName = String.IsNullOrEmpty(tbFile1.Text) ? Hacker.CurrentDataFiles[0] : tbFile1.Text;
            if (ofdDataFile.ShowDialog() == DialogResult.OK)
                tbFile1.Text = ofdDataFile.FileName;
        }

        protected override void OnMainSet()
        {
            switch (Hacker.Game)
            {
                case GameNames.MightAndMagic3:
                    ofdDataFile.FileName = Hacker.CurrentDataFiles[0];
                    tbFile1.Text = Hacker.CurrentDataFiles[0];
                    tbFile1.Enabled = true;
                    tbFile2.Enabled = false;
                    btnBrowse1.Enabled = true;
                    btnBrowse2.Enabled = false;
                    labelFile1.Text = "MM3.CUR";
                    splitContainer1.Panel2Collapsed = false;
                    ShowSecondFile(false);
                    break;
                case GameNames.MightAndMagic45:
                    tbFile1.Text = Hacker.CurrentDataFiles[0];
                    tbFile2.Text = Hacker.CurrentDataFiles[1];
                    tbFile1.Enabled = true;
                    tbFile2.Enabled = true;
                    btnBrowse1.Enabled = true;
                    btnBrowse2.Enabled = true;
                    labelFile1.Text = "XEEN.CUR";
                    labelFile2.Text = "DARK.CUR";
                    ShowSecondFile(true);
                    break;
                default:
                    Height = DefaultSize.Height - splitContainer1.Panel2.Height;
                    splitContainer1.Panel2Collapsed = true;
                    break;
            }
        }

        private void EditCartographyForm_Load(object sender, EventArgs e)
        {
            m_iHeightTwoFiles = Height;
            m_iHeightOneFile = Height - (splitContainer1.Panel2.Height - labelWarning1.Bottom);
            m_iHeightZeroFiles = Height - splitContainer1.Panel2.Height;

            switch (Hacker.Game)
            {
                case GameNames.MightAndMagic3:
                    Height = m_iHeightOneFile;
                    break;
                case GameNames.MightAndMagic45:
                    Height = m_iHeightTwoFiles;
                    break;
                default:
                    Height = m_iHeightZeroFiles;
                    break;
            }

            if (!Hacker.CartographyEditableGlobally)
            {
                rbClearAll.Visible = false;
                rbFillAll.Visible = false;
            }

            CheckEnabled();
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            ofdDataFile.FileName = String.IsNullOrEmpty(tbFile2.Text) ? Hacker.CurrentDataFiles[1] : tbFile2.Text;
            if (ofdDataFile.ShowDialog() == DialogResult.OK)
                tbFile2.Text = ofdDataFile.FileName;
        }

    }
}
