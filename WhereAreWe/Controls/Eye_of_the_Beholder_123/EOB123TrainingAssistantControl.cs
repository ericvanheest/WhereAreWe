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
    public partial class EOB123TrainingAssistantControl : TrainingAssistantControl
    {
        private byte[] m_bytesPrevious = null;
        private int m_iNumTrainedHP = 0;
        private int m_iNumTrainedSP = 0;
        private int m_iLastCharTrained = -1;

        public EOB123TrainingAssistantControl()
        {
            InitializeComponent();
            Init();
        }

        public EOB123TrainingAssistantControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
            cbGiveMaxHP.Checked = Properties.Settings.Default.MaxHPOnLevelUp;
            cbGiveMaxSP.Checked = Properties.Settings.Default.MaxSPOnLevelUp;
        }

        public GameNames Game { get { return m_main == null ? GameNames.None : m_main.Game; } }

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
            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.Constitution, Hacker.Game == GameNames.BardsTale3 ? 30 : 18);
            Global.UpdateBonusTable(lvIQBonus, Hacker, PrimaryStat.IQ, Hacker.Game == GameNames.BardsTale3 ? 30 : 18);
        }

        private void UpdateUI(TrainingInfo info, bool bForce)
        {
            if (info is EOBTrainingInfo)
                UpdateUI((EOBTrainingInfo)info, bForce);
        }

        private void AddCharacter(EOBCharacter btChar, int index, int iLowHP, int iHighHP, int iLowSP, int iHighSP)
        {
            ListViewItem lvi = new ListViewItem(index.ToString());
            lvi.SubItems.Add(btChar.Name);
            lvi.SubItems.Add(String.Format("{0}", btChar.Level));
            lvi.SubItems.Add(String.Format("{0}", btChar.Experience));
            lvi.SubItems.Add(String.Format("{0}", btChar.Stats.Constitution.Permanent));
            lvi.SubItems.Add(String.Format("{0}", btChar.Stats.Intelligence.Permanent));
            lvi.SubItems.Add(String.Format("{0}", btChar.HitPoints.Permanent));
            lvi.SubItems.Add(String.Format("{0}-{1}", iLowHP, iHighHP));
            lvi.SubItems.Add(String.Format("{0}", btChar.SpellPoints.Permanent));
            if (btChar.IsCaster)
                lvi.SubItems.Add(String.Format("{0}-{1}", iLowSP, iHighSP));
            else
                lvi.SubItems.Add("0");
            lvi.Tag = btChar;
            lvParty.Items.Add(lvi);
        }

        private void UpdateUI(EOBTrainingInfo info, bool bForce)
        {
            if (info.Party.Bytes.Length < (info.Party.ActingChar + 1) * info.Party.CharacterSize)
                return; // invalid data

            if (!bForce && Global.Compare(m_bytesPrevious, info.Party.Bytes))
                return; // Nothing changed

            if (m_iNumTrainedHP < 1)
            {
                labelDoneHP.Visible = false;
                cbGiveMaxHP.Enabled = true;
            }

            if (m_iNumTrainedSP < 1)
            {
                labelDoneSP.Visible = false;
                cbGiveMaxSP.Enabled = true;
            }

            EOBPartyInfo btInfo = info.Party as EOBPartyInfo;
            if (btInfo == null)
                return;

            byte[] bytesOld = null;

            lvParty.BeginUpdate();
            lvParty.Items.Clear();
            for (int iChar = 0; iChar < info.Party.NumChars; iChar++)
            {
                if (btInfo.Addresses == null || btInfo.Addresses.Length <= iChar)
                    break;
                int iAddress = btInfo.Addresses[iChar];
                EOBCharacter btChar = EOBCharacter.Create(Game, iChar, btInfo.Bytes, Games.CharacterSize(Game) * iAddress);

                int iBonusHP = Hacker.GetStatModifier(btChar.Stats.Constitution.Permanent, PrimaryStat.Constitution).Value;
                int iBonusSP = Hacker.GetStatModifier(btChar.Stats.Intelligence.Permanent, PrimaryStat.Intellect).Value;

                StatsPerLevel spl = Hacker.GetStatsPerLevel(btChar.BasicClass);

                AddCharacter(btChar, iChar + 1, spl.HPMin + iBonusHP, spl.HPMax + iBonusHP, spl.SPMin + iBonusSP, spl.SPMax + iBonusSP);

                if ((cbGiveMaxHP.Checked || cbGiveMaxSP.Checked) && m_bytesPrevious != null)
                {
                    EOBCharacter btCharPrev = EOBCharacter.Create(Game, iChar, m_bytesPrevious, Games.CharacterSize(Game) * iAddress);
                    if (btCharPrev.Level == btChar.Level)
                        continue;
                    if (btCharPrev.Level > btChar.Level)
                    {
                        // Probably a reloaded savegame
                        m_iNumTrainedHP = 0;
                        m_iNumTrainedSP = 0;
                        m_iLastCharTrained = -1;
                        continue;
                    }
                    int iLevels = btChar.Level - btCharPrev.Level;
                    int iMaximizedHP = btCharPrev.HitPoints.Permanent + (iLevels * spl.HPMax + iBonusHP);
                    int iMaximizedSP = btCharPrev.SpellPoints.Permanent + (iLevels * spl.SPMax + iBonusSP);
                    int iSPDiff = btChar.SpellPoints.Permanent - btCharPrev.SpellPoints.Permanent;

                    if (m_iLastCharTrained != iChar)
                    {
                        m_iNumTrainedHP = 0;
                        m_iNumTrainedSP = 0;
                    }
                    m_iLastCharTrained = iChar;

                    if (btChar.HitPoints.Permanent < iMaximizedHP || (iSPDiff > 0 && btChar.SpellPoints.Permanent < iMaximizedSP))
                    {
                        bytesOld = new byte[info.Party.Bytes.Length];
                        Buffer.BlockCopy(info.Party.Bytes, 0, bytesOld, 0, bytesOld.Length);

                        CharacterOffsets offsets = Games.GetCharacterOffsets(Game);
                        if (cbGiveMaxHP.Checked)
                        {
                            Global.SetInt16(info.Party.Bytes, info.Party.CharacterSize * iAddress + offsets.MaxHP, iMaximizedHP);
                            Global.SetInt16(info.Party.Bytes, info.Party.CharacterSize * iAddress + offsets.CurrentHP, iMaximizedHP);
                            labelDoneHP.Text = String.Format("** HP Maximized for {0}! **", Global.Plural(++m_iNumTrainedHP, "level"));
                            labelDoneHP.Visible = true;
                        }
                        if (cbGiveMaxSP.Checked && iSPDiff > 0)
                        {
                            Global.SetInt16(info.Party.Bytes, info.Party.CharacterSize * iAddress + offsets.MaxSP, iMaximizedSP);
                            Global.SetInt16(info.Party.Bytes, info.Party.CharacterSize * iAddress + offsets.CurrentSP, iMaximizedSP);
                            labelDoneSP.Text = String.Format("** SP Maximized for {0}! **", Global.Plural(++m_iNumTrainedSP, "level"));
                            labelDoneSP.Visible = true;
                        }
                        Hacker.SetTrainingInfo(info);
                    }
                }
            }
            lvParty.EndUpdate();

            m_bytesPrevious = bytesOld == null ? info.Party.Bytes : bytesOld;
        }

        private void SetUnknown()
        {
            lvParty.Items.Clear();
        }

        public override void Closing()
        {
            Properties.Settings.Default.MaxHPOnLevelUp = cbGiveMaxHP.Checked;
            Properties.Settings.Default.MaxSPOnLevelUp = cbGiveMaxSP.Checked;
        }

        private void cbGiveMaxHP_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbGiveMaxHP.Checked)
                m_iNumTrainedHP = 0;
        }

        private void cbGiveMaxSP_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbGiveMaxSP.Checked)
                m_iNumTrainedSP = 0;
        }
    }
}
