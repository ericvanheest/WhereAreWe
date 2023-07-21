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
    public partial class Wiz123TrainingAssistantControl : TrainingAssistantControl
    {
        private byte[] m_bytesPrevious = null;
        private byte m_prevActing = 255;
        private int m_iNumTrained = 0;
        private int m_iLastActingChar = -1;
        private int m_iDiffCount = 0;

        public Wiz123TrainingAssistantControl()
        {
            InitializeComponent();
            Init();
        }

        public Wiz123TrainingAssistantControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
            NativeMethods.SetTooltipDelay(lvEffects, 30000);
        }

        public override void OnLoad()
        {
            base.OnLoad();
            labelDone.Text = "** done **";
            labelDone.Visible = false;
            cbGiveMax.Checked = Properties.Settings.Default.TrainMaximizeHP;
            cbPreventStatLoss.Checked = Properties.Settings.Default.TrainPreventStatLoss;
        }

        protected override void OnMainSet()
        {
            Global.UpdateBonusTable(lvBonusTable, Hacker, PrimaryStat.Vitality);
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

            UpdateUI(info as Wiz123TrainingInfo, bForce);
        }

        private void UpdateUI(Wiz123TrainingInfo info, bool bForce)
        {
            if (info == null)
                return;

            if (info.Party.Bytes.Length < (info.Party.ActingChar + 1) * info.Party.CharacterSize)
            {
                panelInvalid.Visible = true;
                panelInvalid.BringToFront();
                panelInvalid.Location = labelCharacter.Location;
                return; // invalid data
            }

            if (panelInvalid.Visible)
            {
                panelInvalid.Visible = false;
                panelInvalid.SendToBack();
            }

            if (m_iDiffCount == 0 && !bForce && Global.Compare(m_bytesPrevious, info.Party.Bytes) && info.Party.ActingChar == m_prevActing)
                return; // Nothing changed

            if (m_iDiffCount++ < 2)
                return; // Don't intercept changes in the middle of multiple adjustments

            m_iDiffCount = 0;

            if (info.Party.ActingChar != m_iLastActingChar)
            {
                m_iNumTrained = 0;
                labelDone.Visible = false;
            }

            m_iLastActingChar = info.Party.ActingChar;

            cbGiveMax.Enabled = true;

            WizCharacter charPrev = null;
            if (m_bytesPrevious != null && m_bytesPrevious.Length >= (m_prevActing + 1 * info.Party.CharacterSize))
                charPrev = WizCharacter.Create(info.State.Game, 0, m_bytesPrevious, m_prevActing * info.Party.CharacterSize, null);

            m_bytesPrevious = info.Party.Bytes;
            m_prevActing = info.Party.ActingChar;

            WizCharacter character = WizCharacter.Create(info.State.Game, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize, null);

            if (charPrev != null)
            {
                if (charPrev.Level == character.Level - 1 && charPrev.Experience == character.Experience && charPrev.CharName == character.CharName)
                {
                    if (cbPreventStatLoss.Checked || cbGiveMax.Checked)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (cbPreventStatLoss.Checked)
                        {
                            if (charPrev.Strength > character.Strength)
                                character.Strength = charPrev.Strength;
                            if (charPrev.IQ > character.IQ)
                                character.IQ = charPrev.IQ;
                            if (charPrev.Piety > character.Piety)
                                character.Piety = charPrev.Piety;
                            if (charPrev.Vitality > character.Vitality)
                                character.Vitality = charPrev.Vitality;
                            if (charPrev.Agility > character.Agility)
                                character.Agility = charPrev.Agility;
                            if (charPrev.Luck > character.Luck)
                                character.Luck = charPrev.Luck;
                            byte[] bytes = PackedFiveBitValues.GetBytes(character.Strength, character.IQ, character.Piety, character.Vitality, character.Agility, character.Luck);
                            Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + character.Offsets.Stats, 4);
                            sb.Append("** Stats");
                        }
                        if (cbGiveMax.Checked)
                        {
                            ushort iMax = (ushort)(WizCharacter.MaxHPPerLevel(character.Class) + WizCharacter.GetStatModifier(character.Vitality, PrimaryStat.Vitality).Value);
                            iMax *= (ushort) character.Level;
                            iMax = (ushort) Math.Max(iMax, charPrev.HitPoints.Maximum + 1);
                            byte[] bytes = BitConverter.GetBytes(iMax);
                            Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + character.Offsets.CurrentHP, 2);
                            Buffer.BlockCopy(bytes, 0, info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + character.Offsets.MaxHP, 2);

                            character.HitPoints = new MMHitPoints(BitConverter.ToInt16(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + character.Offsets.CurrentHP),
                                BitConverter.ToInt16(info.Party.Bytes, info.Party.ActingChar * info.Party.CharacterSize + character.Offsets.MaxHP));

                            if (sb.Length > 0)
                                sb.Append(", ");
                            else
                                sb.Append("** ");
                            sb.Append("HP");
                        }

                        labelDone.Text = String.Format("{0} adjusted for {1}! **", sb.ToString(), Global.Plural(++m_iNumTrained, "level"));
                        labelDone.Visible = true;
                        Thread.Sleep(200);  // Otherwise the game may set the values after the hacker does
                        Hacker.SetTrainingInfo(info);
                    }
                }
            }

            labelCharName.Text = character.CharName;
            labelEndurance.Text = character.Vitality.ToString();
            labelExperience.Text = character.ExperienceString;
            labelHitPoints.Text = character.HitPoints.Maximum.ToString();
            labelHPLevel.Text = character.HPLevelString;
            labelLevel.Text = character.Level.ToString();
            labelAge.Text = character.BasicAge.Years.ToString();

            SetChances(character);
        }

        private void AddChance(int chance, string strType, int iMinLevel, int iMaxLevel = -1)
        {
            if (iMaxLevel == -1)
            iMaxLevel = iMinLevel;
            if (iMaxLevel < 1 || iMaxLevel > 7)
                return;

            ListViewItem lvi = new ListViewItem(String.Format("{0}%", chance));
            lvi.SubItems.Add(String.Format("Learn level {0}{1} {2} spells",
                iMinLevel, iMaxLevel == iMinLevel ? "" : String.Format("-{0}", iMaxLevel), strType));
            lvi.ToolTipText = String.Format("The chance to learn {0} spells is 3.33% per point of {1}\r\nMore than one spell may be learned during a single level-up", strType, strType.ToLower() == "mage" ? "I.Q." : "Piety");
            lvEffects.Items.Add(lvi);
        }

        private void AddChances(PrimaryStat stat, int statValue, double chanceUp, double chanceDown)
        {
            Global.FixRange(ref chanceUp, 0.0, 100.0);
            Global.FixRange(ref chanceDown, 0.0, 100.0);

            if (statValue < 18 && chanceUp > 0)
            {
                ListViewItem lvi = new ListViewItem(String.Format("{0:F1}%", chanceUp));
                lvi.SubItems.Add(String.Format("+1 {0}", Global.StatString(stat)));
                lvi.ToolTipText = Hacker.RaiseStatChance;
                lvEffects.Items.Add(lvi);
            }
            if (chanceDown > 0)
            {
                ListViewItem lvi = null;
                if (cbPreventStatLoss.Checked)
                {
                    lvi = new ListViewItem("0%");
                    lvi.SubItems.Add(String.Format("-1 {0}", Global.StatString(stat)));
                    lvi.ToolTipText = "The chance to lower a stat has been removed via the checkbox below\r\n(the game UI may still show a loss, but it will be retroactively adjusted).";
                }
                else
                {
                    lvi = new ListViewItem(String.Format("{0:F1}%", statValue == 18 ? chanceDown / 6 : chanceDown));
                    lvi.SubItems.Add(String.Format("-1 {0}", Global.StatString(stat)));
                    if (statValue == 18)
                        lvi.ToolTipText = Hacker.LowerMaxStatChance;
                    else
                        lvi.ToolTipText = Hacker.LowerStatChance;
                }
                lvEffects.Items.Add(lvi);
            }
        }

        private void SetChances(WizCharacter wizChar)
        {
            lvEffects.BeginUpdate();
            lvEffects.Items.Clear();

            int iMaxMageLevel = wizChar.MaxMageSpell(wizChar.Level+1);
            int iMaxPriestLevel = wizChar.MaxPriestSpell(wizChar.Level+1);

            bool bAlwaysMage = false;
            bool bAlwaysPriest = false;
            int nextMage = wizChar.SpellBook.NextMage;
            int nextPriest = wizChar.SpellBook.NextPriest;

            WizardrySpell spellNextMage = null;
            WizardrySpell spellNextPriest = null;

            if (wizChar is Wiz5Character)
            {
                bAlwaysMage = nextMage > (int) Wiz5SpellIndex.Halito && Wiz5.Spells[nextMage].Level == Wiz5.Spells[nextMage - 1].Level;
                bAlwaysPriest = nextPriest >(int) Wiz5SpellIndex.Kalki && Wiz5.Spells[nextPriest].Level == Wiz5.Spells[nextPriest - 1].Level;
                if (nextMage > 0)
                    spellNextMage = Wiz5.Spells[(int)nextMage - 1];
                if (nextPriest > 0)
                spellNextPriest = Wiz5.Spells[(int)nextPriest - 1];
            }
            else
            {
                bAlwaysMage = nextMage > (int) Wiz1234SpellIndex.Halito && Wiz123.Spells[nextMage].Level == Wiz123.Spells[nextMage - 1].Level;
                bAlwaysPriest = nextPriest > (int) Wiz1234SpellIndex.Kalki && Wiz123.Spells[nextPriest].Level == Wiz123.Spells[nextPriest - 1].Level;
                if (nextMage > 0)
                    spellNextMage = Wiz123.Spells[(int)nextMage - 1];
                if (nextPriest > 0)
                    spellNextPriest = Wiz123.Spells[(int)nextPriest - 1];
            }


            if (iMaxMageLevel == 0 && bAlwaysMage)
                AddChance(WizCharacter.GetStatModifier(wizChar.IQ, PrimaryStat.IQ).Value, "Mage", spellNextMage.Level);
            else if (iMaxMageLevel > 0)
                AddChance(WizCharacter.GetStatModifier(wizChar.IQ, PrimaryStat.IQ).Value, "Mage", 1, iMaxMageLevel);

            if (iMaxPriestLevel == 0 && bAlwaysPriest)
                AddChance(WizCharacter.GetStatModifier(wizChar.IQ, PrimaryStat.IQ).Value, "Priest", spellNextPriest.Level);
            else if (iMaxPriestLevel > 0)
                AddChance(WizCharacter.GetStatModifier(wizChar.IQ, PrimaryStat.IQ).Value, "Priest", 1, iMaxPriestLevel);

            int years = wizChar.BasicAge.Years;

            double ageUp = 0.0;
            double ageDown = 0.0;

            if (Hacker.Game == GameNames.Wizardry5)
            {
                ageUp = 50 - (years * 0.3333);
                ageDown = 0.0;
                if (wizChar.Level > 2 && wizChar.Level < 10)
                    ageDown = 0.01 * wizChar.Level * years;
                else if (wizChar.Level > 9)
                    ageDown = 0.12 * years;
                if (ageDown > 20.0)
                    ageDown = 20.0;
            }
            else
            {
                ageUp = years < 21 ? (36 + (1.9 * (21 - years))) : years > 67 ? (36 - (1.5 * (years - 67))) : 36;
                ageDown = 75 - ageUp;
            }
            AddChances(PrimaryStat.Strength, wizChar.Strength, ageUp, ageDown);
            AddChances(PrimaryStat.IQ, wizChar.IQ, ageUp, ageDown);
            AddChances(PrimaryStat.Piety, wizChar.Piety, ageUp, ageDown);
            AddChances(PrimaryStat.Vitality, wizChar.Vitality, ageUp, ageDown);
            AddChances(PrimaryStat.Agility, wizChar.Agility, ageUp, ageDown);
            AddChances(PrimaryStat.Luck, wizChar.Luck, ageUp, ageDown);
            lvEffects.EndUpdate();
        }

        public override void Closing()
        {
            Properties.Settings.Default.TrainMaximizeHP = cbGiveMax.Checked;
            Properties.Settings.Default.TrainPreventStatLoss = cbPreventStatLoss.Checked;
        }

        private void llNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewInfoForm.ShowCentered(this, "When a character gains a level in Wizardry, the entire range of hit points is re-rolled.  For example, a Fighter becoming level 14 now has 14d10 HP, which could be as few as 14 or as many as 140.  If that new value is less than the character's current HP, the new value is modified to be the current value plus one (if the Fighter had 100 HP at level 13, any value in the 14d10 range that is 100 or lower will be changed to be 101).  One consequence of this is that the better your random HP roll is for the current level, the more likely you are to have single-HP gains for the next few levels.\r\n\r\n"
                + "As the character becomes higher and higher level, the chances of having a maximum HP value substantially different than half of the theoretical maximum are very small.  The \"Give maximum HP on level-up\" checkbox will completely short-circuit this intrinsic limitation and always give you the maximum values regardless of the progressively small probabilities of that happening naturally, but be aware that this will make the character twice as beefy as the original game design intended.",
            "Wizardry Hit Point Calculations");
        }

        private void cbPreventStatLoss_CheckedChanged(object sender, EventArgs e)
        {
            m_bytesPrevious = null;
        }
    }
}
