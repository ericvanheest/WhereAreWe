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
    public partial class SelectGameFilesForm : Form
    {
        private GameNames m_game;
        private string m_strFileSpec1 = "All Files|*.*";
        private string m_strFileSpec2 = "All Files|*.*";

        public string File1 { get { return tbFile1.Text; } set { tbFile1.Text = value; } }
        public string File2 { get { return tbFile2.Text; } set { tbFile2.Text = value; } }

        private long[] m_iExpectedSizes1 = null;
        private long[] m_iExpectedSizes2 = null;

        public SelectGameFilesForm()
        {
            InitializeComponent();
        }

        public SelectGameFilesForm(GameNames game)
        {
            m_game = game;
            InitializeComponent();

            UpdateUI();
        }

        private void UpdateUI()
        {
            switch(m_game)
            {
                case GameNames.MightAndMagic3:
                    tbFile1.Text = MM3MemoryHacker.MM3CurrentDataFile;
                    tbFile2.Enabled = false;
                    tbFile2.Visible = false;
                    labelFile2.Visible = false;
                    btnBrowse2.Visible = false;
                    btnBrowse2.Enabled = false;
                    labelFile1.Text = "MM3.CUR";
                    m_strFileSpec1 = "MM3 Data File|MM3.CUR|All Files|*.*";
                    m_iExpectedSizes1 = MM3RosterFile.AcceptableLengths;
                    break;
                case GameNames.MightAndMagic45:
                    labelFile1.Text = "XEEN.CUR";
                    labelFile2.Text = "DARK.CUR";
                    tbFile1.Text = MM45MemoryHacker.MM4CurrentDataFile;
                    tbFile2.Enabled = true;
                    btnBrowse2.Enabled = true;
                    labelFile2.Visible = true;
                    btnBrowse2.Visible = true;
                    btnBrowse2.Enabled = true;
                    tbFile2.Text = MM45MemoryHacker.MM5CurrentDataFile;
                    m_strFileSpec1 = "MM4 Data File|XEEN.CUR|All Files|*.*";
                    m_strFileSpec2 = "MM5 Data File|DARK.CUR|All Files|*.*";
                    m_iExpectedSizes1 = MM4RosterFile.AcceptableLengths;
                    m_iExpectedSizes2 = MM5RosterFile.AcceptableLengths;
                    break;
                default:
                    break;
            }

            CheckFiles();
        }

        private void CheckFiles()
        {
            CheckFile(tbFile1, labelWarning1, m_iExpectedSizes1);
            CheckFile(tbFile2, labelWarning2, m_iExpectedSizes2);

            btnOK.Enabled = !(labelWarning1.Visible || labelWarning2.Visible);
        }

        private void CheckFile(TextBox tb, Label warning, long[] expectedSizes)
        {
            warning.Visible = false;
            if (tb.Visible)
            {
                if (!File.Exists(tb.Text))
                {
                    warning.Text = "Warning: File does not exist";
                    warning.Visible = true;
                }
                else
                {
                    long iSize = new FileInfo(tb.Text).Length;
                    if (!expectedSizes.Contains(iSize))
                    {
                        warning.Text = String.Format("Warning: File is {0} bytes, expected size is one of: {1}", iSize, Global.ArrayString(expectedSizes));
                        warning.Visible = true;
                    }
                }
            }
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (tbFile1.Text.Contains('\\'))
                ofd.InitialDirectory = Path.GetDirectoryName(tbFile1.Text);
            ofd.FileName = tbFile1.Text;
            ofd.Filter = m_strFileSpec1;
            if (ofd.ShowDialog() == DialogResult.OK)
                tbFile1.Text = ofd.FileName;
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (tbFile2.Text.Contains('\\'))
                ofd.InitialDirectory = Path.GetDirectoryName(tbFile2.Text);
            ofd.FileName = tbFile2.Text;
            ofd.Filter = m_strFileSpec2;
            if (ofd.ShowDialog() == DialogResult.OK)
                tbFile2.Text = ofd.FileName;
        }

        private void tbFile1_TextChanged(object sender, EventArgs e)
        {
            CheckFiles();
        }

        private void tbFile2_TextChanged(object sender, EventArgs e)
        {
            CheckFiles();
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
    }
}
