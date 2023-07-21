using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class MM1TrainingAssistantControl : TrainingAssistantControl
    {
        private byte[] m_bytesPrevious = null;
        private byte m_prevActing = 255;
        private int m_iNumTrained = 0;
        private int m_iLastActingChar = -1;

        public MM1TrainingAssistantControl()
        {
            InitializeComponent();
            Init();
        }

        public MM1TrainingAssistantControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
            cbGiveMax.Checked = Properties.Settings.Default.MaxHPOnLevelUp;
        }

        public override void TimerTick(bool bForce)
        {
            if (Hacker == null || !Hacker.IsValid)
                return;

            TrainingInfo info = Hacker.GetTrainingInfo();

            if (info == null)
                return;

            if (String.IsNullOrWhiteSpace(info.MapName))
                info.MapName = m_main.GetMapName(info.MapIndex);

            UpdateUI(info, bForce);
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnMainSet()
        {
            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.Endurance);
        }

        private void UpdateUI(TrainingInfo info, bool bForce)
        {
            if (info is MM1TrainingInfo)
                UpdateUI((MM1TrainingInfo)info, bForce);
            else if (info is MM2TrainingInfo)
                UpdateUI((MM2TrainingInfo)info, bForce);
        }

        private void UpdateUI(MM1TrainingInfo info, bool bForce)
        {
            if (info.Party.Bytes.Length < (info.Party.ActingChar + 1) * info.Party.CharacterSize)
                return; // invalid data

            if (!bForce && Global.Compare(m_bytesPrevious, info.Party.Bytes) && info.Party.ActingChar == m_prevActing)
                return; // Nothing changed

            labelDone.Text = "** done **";
            labelDone.Visible = false;
            if (info.Party.ActingChar != m_iLastActingChar)
                m_iNumTrained = 0;

            m_iLastActingChar = info.Party.ActingChar;

            cbGiveMax.Enabled = true;

            MM1Character charPrev = null;
            if (m_bytesPrevious != null && m_bytesPrevious.Length >= (m_prevActing + 1 * info.Party.CharacterSize))
                charPrev = MM1Character.Create(m_bytesPrevious, m_prevActing * info.Party.CharacterSize, false);

            m_bytesPrevious = info.Party.Bytes;
            m_prevActing = info.Party.ActingChar;

            MM1Character character = MM1Character.Create(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize, false);

            if (charPrev != null)
            {
                if (charPrev.Level.Permanent == character.Level.Permanent - 1 && charPrev.Experience == character.Experience && charPrev.CharName == character.CharName)
                {
                    if (cbGiveMax.Checked)
                    {
                        ushort iMax = (ushort) (MM1Character.BaseHPForClass(character.Class) + MM1Character.GetStatModifier(character.Endurance.Permanent, PrimaryStat.Endurance).Value);
                        iMax += (ushort) charPrev.HitPoints.Maximum;
                        byte[] bytes = BitConverter.GetBytes(iMax);
                        Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + 51, 2);    // HP Current
                        Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + 53, 2);    // HP Max
                        Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + 55, 2);    // HP Temp Max

                        character.HitPoints = new MMHitPoints(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + 51);

                        Hacker.SetTrainingInfo(info);

                        labelDone.Text = String.Format("** HP Maximized for {0}! **", Global.Plural(++m_iNumTrained, "level"));
                        labelDone.Visible = true;
                    }
                }
            }

            labelMapName.Text = info.MapName;
            labelCharName.Text = character.CharName;
            labelEndurance.Text = character.Endurance.Permanent.ToString();
            labelExperience.Text = character.ExperienceString;
            labelHitPoints.Text = character.HitPoints.Maximum.ToString();
            labelHPLevel.Text = character.HPLevelString;
            labelLevel.Text = character.Level.Permanent.ToString();
        }

        private void SetUnknown()
        {
            labelMapName.Text = "None";
            labelCharName.Text = "None";
            labelEndurance.Text = "N/A";
            labelExperience.Text = "N/A";
            labelHitPoints.Text = "N/A";
            labelHPLevel.Text = "N/A";
            labelLevel.Text = "N/A";
        }

        private void UpdateUI(MM2TrainingInfo info, bool bForce)
        {
            labelDone.Text = "(this game always gives max HP)";
            labelDone.Visible = true;

            cbGiveMax.Enabled = false;

            if (info.Party.Bytes.Length < (info.Party.ActingChar + 1) * info.Party.CharacterSize)
            {
                SetUnknown();
                return; // invalid data
            }

            if (!bForce && Global.Compare(m_bytesPrevious, info.Party.Bytes) && info.Party.ActingChar == m_prevActing)
                return; // Nothing changed

            MM2Character character = MM2Character.Create(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize, false);

            labelMapName.Text = info.MapName;
            labelCharName.Text = character.CharName;
            labelEndurance.Text = character.Endurance.Permanent.ToString();
            labelExperience.Text = character.ExperienceString;
            labelHitPoints.Text = character.HitPoints.Maximum.ToString();
            labelHPLevel.Text = character.HPLevelString(info.Map);
            labelLevel.Text = character.Level.Permanent.ToString();
        }

        public override void Closing()
        {
            Properties.Settings.Default.MaxHPOnLevelUp = cbGiveMax.Checked;
        }
    }
}
