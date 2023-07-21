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
    public partial class TrainingAssistantForm : HackerBasedForm
    {
        private TrainingAssistantControl m_assistant = null;
        private bool m_bAssistantLoaded = false;
        private bool m_bForce = false;

        public TrainingAssistantForm()
        {
            InitializeComponent();
            CommonKeyRefresh += TrainingAssistantForm_CommonKeyRefresh;
        }

        void TrainingAssistantForm_CommonKeyRefresh(object sender, EventArgs e)
        {
            m_bForce = true;
        }

        protected override void OnMainSet()
        {
            if (m_assistant != null)
                m_assistant.SetMain(m_main);
            Global.RestartTimer(timerUpdateMemory);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        public void SetAssistant(TrainingAssistantControl ctrl)
        {
            if (ctrl == null)
                return;
            m_assistant = ctrl;
            if (m_main != null)
                m_assistant.SetMain(m_main);
            m_assistant.Dock = DockStyle.Fill;
            panelAssistant.Controls.Add(m_assistant);
            m_assistant.BringToFront();

            CheckSize();

            LoadAssistant();
        }

        private void CheckSize()
        {
            if (m_assistant == null)
                return;

            int iDeltaWidth = m_assistant.MinimumSize.Width - panelAssistant.Width;
            int iDeltaHeight = m_assistant.MinimumSize.Height - panelAssistant.Height;

            if (iDeltaHeight > 0)
            {
                Height += iDeltaHeight;
                MinimumSize = new Size(MinimumSize.Width, Height);
            }
            if (iDeltaWidth > 0)
            {
                Width += iDeltaWidth;
                MinimumSize = new Size(Width, MinimumSize.Height);
            }
            if (iDeltaHeight <= 0 && iDeltaWidth <= 0)
                MinimumSize = new Size(Width + iDeltaWidth, Height + iDeltaHeight);
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (Hacker == null || !Hacker.Running)
            {
                Text = "Training Assistant (no running game detected!)";
                return;
            }

            Text = "Training Assistant";

            if (m_assistant != null)
            {
                m_assistant.TimerTick(m_bForce);
                m_bForce = false;
            }
        }


        private void TrainingAssistantForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdateMemory.Stop();

            if (m_assistant != null)
                m_assistant.Closing();
        }

        protected override void WndProc(ref Message m)
        {
            if (m_main != null)
                m_main.ProcessMessage(ref m, this);
            base.WndProc(ref m);
        }

        private void LoadAssistant()
        {
            if (m_assistant != null && !m_bAssistantLoaded)
            {
                m_assistant.OnLoad();
                m_bAssistantLoaded = true;
            }
        }

        private void CharCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerUpdateMemory.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TrainingAssistantForm_Load(object sender, EventArgs e)
        {
            LoadAssistant();
            timerUpdateMemory.Start();
        }
    }
}

