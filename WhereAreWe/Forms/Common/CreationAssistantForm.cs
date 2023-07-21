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
    public partial class CreationAssistantForm : HackerBasedForm
    {
        private CreationAssistantControl m_assistant = null;
        private bool m_bAssistantLoaded = false;

        public CreationAssistantForm()
        {
            InitializeComponent();
        }

        public override void Destroy()
        {
            timerUpdateMemory.Stop();
            base.Destroy();
        }

        public void SetAssistant(CreationAssistantControl ctrl)
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
        }

        protected override void OnMainSet()
        {
            LoadAssistant();

            if (Hacker == null)
            {
                Text = "Character Creation Assistant (no game running)";
                return;
            }
            Text = "Character Creation Assistant";

            Global.RestartTimer(timerUpdateMemory);
        }

        private void CharCreationForm_Load(object sender, EventArgs e)
        {
            LoadAssistant();
            timerUpdateMemory.Start();
        }

        private void LoadAssistant()
        {
            if (m_assistant != null && !m_bAssistantLoaded)
            {
                m_assistant.OnLoad();
                m_bAssistantLoaded = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timerUpdateMemory_Tick(object sender, EventArgs e)
        {
            if (Hacker == null || !Hacker.Running)
            {
                Text = "Character Creation Assistant (no running game detected!)";
                return;
            }

            Text = "Character Creation Assistant";

            if (m_assistant != null && m_assistant.TimerEnabled)
                m_assistant.TimerTick();
        }

        private void CharCreationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_assistant != null && !m_assistant.UsedAllPoints && !m_bDestroy)
            {
                switch (MessageBox.Show("You still have points remaining; these will be lost!  Exit this helper window?", "Exit with points remaining?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                    default:
                        break;
                }
            }
            timerUpdateMemory.Stop();
        }

        private void CreationAssistantForm_SizeChanged(object sender, EventArgs e)
        {
            CheckSize();
        }
    }

    public enum PrimaryStat
    {
        Intellect = 0,
        Might = 1,
        Personality = 2,
        Endurance = 3,
        Speed = 4,
        Accuracy = 5,
        Luck = 6,
        Strength = 7,
        IQ = 8,
        Piety = 9,
        Vitality = 10,
        Agility = 11,
        Dexterity = 12,
        Constitution = 13,
        HitPoints = 14,
        Charisma = 15,
        Strength18 = 16,
        Intelligence = 17,
        Wisdom = 18,
        None = 255
    }
}
