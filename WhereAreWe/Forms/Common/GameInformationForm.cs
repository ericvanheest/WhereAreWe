using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class GameInformationForm : HackerBasedForm
    {
        private GameInfo m_infoPrevious = null;
        private GameInformationControl m_ctrlGameInfo = null;
        private int[] m_splitters = null;
        private NoScanReason m_noScan = NoScanReason.None;

        protected override bool ShowWithoutActivation { get { return true; } }

        public GameInformationForm()
        {
            InitializeComponent();
        }

        protected override void OnMainSet()
        {
            CreateUI();

            Global.RestartTimer(timerUpdateMemory);
        }

        private void CreateUI()
        {
            if (Hacker == null)
                return;

            if (m_ctrlGameInfo != null)
            {
                Controls.Remove(m_ctrlGameInfo);
                m_ctrlGameInfo.Dispose();
                m_ctrlGameInfo = null;
                m_infoPrevious = null;
            }

            m_ctrlGameInfo = Hacker.CreateGameInfoControl(m_main);

            if (m_ctrlGameInfo != null)
            {
                Controls.Add(m_ctrlGameInfo);
                m_ctrlGameInfo.Dock = DockStyle.Fill;

                if (m_splitters != null && m_splitters.Length > 0)
                    m_ctrlGameInfo.Splitters = m_splitters;
            }

            labelDisabled.SendToBack();
        }

        protected override bool OnCommonKeyRefresh()
        {
            m_infoPrevious = null;
            return true;
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        protected override void OnMainSetAgain()
        {
            Global.RestartTimer(timerUpdateMemory);
        }

        public override int[] Splitters
        {
            get { return m_ctrlGameInfo == null ? null : m_ctrlGameInfo.Splitters; }
            set { m_splitters = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (Hacker == null || !Hacker.Running)
            {
                SetUnknown();
                labelDisabled.BringToFront();
                if (m_main.InOptions && m_noScan != NoScanReason.OptionsOpen)
                {
                    m_noScan = NoScanReason.OptionsOpen;
                    labelDisabled.Text = Global.DisableWhileOptions("game information");
                }
                else if (!m_main.InOptions && m_noScan != NoScanReason.NotRunning)
                {
                    m_noScan = NoScanReason.NotRunning;
                    labelDisabled.Text = Global.DisableWhileNoScanner();
                }
                return;
            }

            if (m_noScan != NoScanReason.None)
            {
                labelDisabled.SendToBack();
                m_noScan = NoScanReason.None;
                Text = "Game Information";
            }

            GameInfo info = Hacker.GetGameInfo(m_infoPrevious);

            if (m_ctrlGameInfo == null)
                return;

            if (m_ctrlGameInfo.Game != Hacker.Game)
            {
                Controls.Remove(m_ctrlGameInfo);
                m_ctrlGameInfo.Dispose();
                m_ctrlGameInfo = null;
                CreateUI();
            }

            if (info != m_infoPrevious)
                UpdateUI(info);
        }

        private void UpdateUI(GameInfo info)
        {
            bool bForce = (m_infoPrevious == null);

            m_infoPrevious = info;

            ((GameInformationControl)m_ctrlGameInfo).UpdateUI(info, bForce);
        }

        private void SetUnknown()
        {
            m_infoPrevious = null;
        }

        private void GameInformationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdateMemory.Stop();
        }
    }

    public class GameInformationControl : UserControl, ISplitters
    {
        public virtual GameNames Game { get { return GameNames.None; } }
        public virtual void UpdateUI(GameInfo info, bool bForce) { }
        public virtual int[] Splitters { get { return new int[0]; } set {} }
    }
}

