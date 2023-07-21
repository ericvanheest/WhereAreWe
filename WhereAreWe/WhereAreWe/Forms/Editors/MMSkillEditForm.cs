using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class MMSkillEditForm : Form
    {
        private MMSecondarySkills m_skills;
        private bool m_bReadOnly = false;
        private bool m_bReadyForInput = false;
        private Timer m_timerInput = new Timer();

        private GameNames m_game;

        public MMSkillEditForm()
        {
            InitializeComponent();
        }

        public MMSkillEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;
        }

        public bool ReadOnly
        {
            get { return m_bReadOnly; }
            set 
            {
                m_bReadOnly = value;
                Text = String.Format("{0} Skills", m_bReadOnly ? "View" : "Change");
            }
        }

        public MMSecondarySkills Skills
        {
            get
            {
                UpdateFromUI();
                return m_skills;
            }

            set
            {
                m_skills = value;
                InitList();
                UpdateUI();
            }
        }

        private void InitList()
        {
            if (lvSkills.Items.Count > 0)
                return;

            lvSkills.BeginUpdate();
            lvSkills.Items.Clear();
            for(MMSecondarySkillIndex index = MMSecondarySkillIndex.Thievery; index < MMSecondarySkillIndex.Last; index++)
            {
                ListViewItem lvi = new ListViewItem(MMSecondarySkills.Name(index));
                lvi.Checked = HasSkill(index);
                lvi.Tag = index;
                lvSkills.Items.Add(lvi);
            }
            lvSkills.EndUpdate();

            m_timerInput.Interval = 250;
            m_timerInput.Tick += m_timerInput_Tick;
            m_timerInput.Start();
        }

        void m_timerInput_Tick(object sender, EventArgs e)
        {
            m_timerInput.Stop();
            m_bReadyForInput = true;
        }

        private bool HasSkill(MMSecondarySkillIndex index)
        {
            switch (index)
            {
                case MMSecondarySkillIndex.Thievery:
                    return (m_skills.Thievery > 0);
                case MMSecondarySkillIndex.ArmsMaster:
                    return (m_skills.ArmsMaster > 0);
                case MMSecondarySkillIndex.Astrologer:
                    return (m_skills.Astrologer > 0);
                case MMSecondarySkillIndex.BodyBuilder:
                    return (m_skills.BodyBuilder > 0);
                case MMSecondarySkillIndex.Cartographer:
                    return (m_skills.Cartographer > 0);
                case MMSecondarySkillIndex.Crusader:
                    return (m_skills.Crusader > 0);
                case MMSecondarySkillIndex.DirectionSense:
                    return (m_skills.DirectionSense > 0);
                case MMSecondarySkillIndex.Linguist:
                    return (m_skills.Linguist > 0);
                case MMSecondarySkillIndex.Merchant:
                    return (m_skills.Merchant > 0);
                case MMSecondarySkillIndex.Mountaineer:
                    return (m_skills.Mountaineer > 0);
                case MMSecondarySkillIndex.Navigator:
                    return (m_skills.Navigator > 0);
                case MMSecondarySkillIndex.PathFinder:
                    return (m_skills.PathFinder > 0);
                case MMSecondarySkillIndex.PrayerMaster:
                    return (m_skills.PrayerMaster > 0);
                case MMSecondarySkillIndex.Prestidigitator:
                    return (m_skills.Prestidigitator > 0);
                case MMSecondarySkillIndex.Swimmer:
                    return (m_skills.Swimmer > 0);
                case MMSecondarySkillIndex.Tracker:
                    return (m_skills.Tracker > 0);
                case MMSecondarySkillIndex.SpotSecretDoors:
                    return (m_skills.SpotSecretDoors > 0);
                case MMSecondarySkillIndex.DangerSense:
                    return (m_skills.DangerSense > 0);
                default:
                    return false;
            }
        }

        public void UpdateUI()
        {
            if (m_skills != null)
            {
                foreach (ListViewItem lvi in lvSkills.Items)
                    lvi.Checked = HasSkill((MMSecondarySkillIndex)lvi.Tag);
            }

            btnNone.Enabled = !m_bReadOnly;
            btnSetAll.Enabled = !m_bReadOnly;
        }

        public void UpdateFromUI()
        {
            if (m_bReadOnly)
                return;

            m_skills = new MMSecondarySkills();

            foreach (ListViewItem lvi in lvSkills.Items)
            {
                switch ((MMSecondarySkillIndex)lvi.Tag)
                {
                    case MMSecondarySkillIndex.Thievery:
                        m_skills.Thievery = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.ArmsMaster:
                        m_skills.ArmsMaster = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Astrologer:
                        m_skills.Astrologer = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.BodyBuilder:
                        m_skills.BodyBuilder = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Cartographer:
                        m_skills.Cartographer = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Crusader:
                        m_skills.Crusader = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.DirectionSense:
                        m_skills.DirectionSense = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Linguist:
                        m_skills.Linguist = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Merchant:
                        m_skills.Merchant = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Mountaineer:
                        m_skills.Mountaineer = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Navigator:
                        m_skills.Navigator = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.PathFinder:
                        m_skills.PathFinder = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.PrayerMaster:
                        m_skills.PrayerMaster = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Prestidigitator:
                        m_skills.Prestidigitator = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Swimmer:
                        m_skills.Swimmer = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.Tracker:
                        m_skills.Tracker = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.SpotSecretDoors:
                        m_skills.SpotSecretDoors = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    case MMSecondarySkillIndex.DangerSense:
                        m_skills.DangerSense = (byte)(lvi.Checked ? 1 : 0);
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvSkills.Items)
                lvi.Checked = false;
        }

        private void btnSetAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvSkills.Items)
                lvi.Checked = true;
        }

        private void lvSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSkills.SelectedItems.Count < 1)
            {
                labelDescription.Text = "No skill selected";
                labelLearned.Text = "No skill selected";
                return;
            }

            MMSecondarySkillIndex index = (MMSecondarySkillIndex)lvSkills.SelectedItems[0].Tag;

            labelDescription.Text = MMSecondarySkills.Description(index);
            labelLearned.Text = m_skills.WhereLearned(index);
        }

        private void lvSkills_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_bReadOnly && m_bReadyForInput)
                e.NewValue = e.CurrentValue;
        }
    }
}
