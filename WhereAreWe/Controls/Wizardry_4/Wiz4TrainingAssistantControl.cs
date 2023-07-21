using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WhereAreWe
{
    public partial class Wiz4TrainingAssistantControl : TrainingAssistantControl
    {
        private byte[] m_bytesLast = null;

        public Wiz4TrainingAssistantControl()
        {
            InitializeComponent();
            Init();
        }

        public Wiz4TrainingAssistantControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
            NativeMethods.SetTooltipDelay(lvGroups, 30000);
        }

        public override void OnLoad()
        {
            base.OnLoad();
            cbGiveMaxCreatures.Checked = Properties.Settings.Default.TrainMaximizeCreatures;
        }

        protected override void OnMainSet()
        {
        }

        public override void TimerTick(bool bForce)
        {
            if (Hacker == null || !Hacker.IsValid)
                return;

            Wiz4TrainingInfo info = Hacker.GetTrainingInfo() as Wiz4TrainingInfo;

            if (info == null)
                return;

            if (UpdateUI(info, bForce))
                CheckMonsterGroups();
            else if (info.State.Main != MainState.Adventuring)
                labelMaximized.Visible = false;
        }

        private void AddItem(int index, Wiz4MonsterIndex monster)
        {
            ListViewItem lvi = new ListViewItem(String.Format("{0}", index));
            Wiz4Monster wizMonster = null;
            if ((int)monster >= 0 && (int)monster < Wiz4.Monsters.Count)
            {
                wizMonster = Wiz4.Monsters[(int)monster] as Wiz4Monster;
                lvi.SubItems.Add(wizMonster.NumAppearing.ToString());
                lvi.SubItems.Add(wizMonster.OneLineDescription);
                lvi.ToolTipText = wizMonster.MultiLineDescription;
            }
            else
            {
                lvi.SubItems.Add("?");
                lvi.SubItems.Add(String.Format("Unknown monster index (#{0})", (int) monster));
            }
            lvGroups.Items.Add(lvi);
        }

        private void CheckMonsterGroups()
        {
            if (Properties.Settings.Default.TrainMaximizeCreatures)
            {
                Wiz4EncounterInfo encounter = Hacker.GetEncounterInfo() as Wiz4EncounterInfo;
                if (encounter != null)
                {
                    bool bNeedUpdate = false;
                    foreach (WizEncounterGroup group in encounter.Groups)
                    {
                        int iMax = Math.Max(1, Math.Min(9, group.Monster.NumAppearing.Max));
                        if (group.NumAlive < iMax)
                        {
                            group.NumAlive = iMax;
                            group.NumEnemies = iMax;
                            bNeedUpdate = true;
                        }
                    }

                    if (bNeedUpdate)
                    {
                        Hacker.SetEncounterInfo(encounter);
                        labelMaximized.Visible = true;
                    }
                }
            }
        }

        private bool UpdateUI(Wiz4TrainingInfo info, bool bForce)
        {
            if (info == null)
                return false;

            byte[] rawBytes = info.RawBytes;
            if (m_bytesLast != null && Global.Compare(rawBytes, m_bytesLast))
                return info.InTraining;

            m_bytesLast = rawBytes;

            if (info.InTraining)
            {
                labelNotAtPentagram.Text = "The checkbox above will change any group of summoned monsters to have the maximum permitted size.  " +
                    "For example, a group size of 1d6+2 will be set to 8 regardless of the actual roll.";
                lvGroups.Enabled = true;
                cbGiveMaxCreatures.Enabled = true;
                lvGroups.BeginUpdate();
                lvGroups.Items.Clear();
                int iIndex = 1;
                foreach (Wiz4MonsterIndex monster in info.GetSelectedMonsters())
                    AddItem(iIndex++, monster);
                lvGroups.EndUpdate();
                return true;
            }
            else
            {
                labelNotAtPentagram.Text = "You are not using a pentagram.  This assistant will only be active when Werdna is using a pentagram to summon creatures.";
                lvGroups.Enabled = false;
                cbGiveMaxCreatures.Enabled = false;
                lvGroups.Items.Clear();
            }
            return false;
        }

        public override void Closing()
        {
            Properties.Settings.Default.TrainMaximizeCreatures = cbGiveMaxCreatures.Checked;
        }

        private void cbGiveMaxCreatures_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TrainMaximizeCreatures = cbGiveMaxCreatures.Checked;
        }
    }
}
