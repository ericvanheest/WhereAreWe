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
    public partial class MM345MonsterEditForm : HackerBasedForm
    {
        private MM345Monster m_monster;
        private bool m_bUpdatingUI = false;
        private bool m_bUpdatingIndex = false;

        public MM345MonsterEditForm()
        {
            InitializeComponent();
        }

        protected override bool OnCommonKeyPrevious()
        {
            FindPrevious();
            return true;
        }

        protected override bool OnCommonKeyNext(bool bIncludeCurrent)
        {
            FindNext();
            return true;
        }

        public void SetMonsterInfo(MM345Monster info)
        {
            if (m_monster != null)
                TransferMetadata(m_monster, info);
            m_monster = info;
            UpdateUI();
        }

        private void TransferMetadata(MMMonster source, MMMonster target)
        {
            target.CurrentHP = source.CurrentHP;
            target.Position = source.Position;
            target.EncounterIndex = source.EncounterIndex;
        }

        public MM345Monster GetMonsterInfo()
        {
            return m_monster;
        }

        public List<MMMonster> Monsters
        {
            get
            {
                if (m_monster is MM3Monster)
                    return MM3.Monsters;
                if (m_monster is MM45Monster)
                {
                    if (Hacker is MM45MemoryHacker && ((MM45MemoryHacker)Hacker).GetMonsterSide() == MM45Side.Clouds)
                        return MM45.MM4Monsters;
                    else
                        return MM45.MM5Monsters;
                }
                return null;
            }
        }

        public void UpdateUI()
        {
            int iIndex;

            m_bUpdatingUI = true;

            comboTouch.Items.Clear();
            comboTarget.Items.Clear();
            comboDamageType.Items.Clear();

            if (!m_bUpdatingIndex)
            {
                comboIndex.Items.Clear();
                for (int i = 0; i < Monsters.Count; i++)
                    comboIndex.Items.Add(String.Format("{0}: {1}", i, Monsters[i].OneLineDescription));
                if (m_monster.Index < comboIndex.Items.Count)
                    comboIndex.SelectedIndex = m_monster.Index;
            }

            if (m_monster is MM3Monster)
            {
                MM3Monster mm3Monster = m_monster as MM3Monster;
                for (MM3Touch touch = MM3Touch.First; touch < MM3Touch.Last; touch++)
                {
                    iIndex = comboTouch.Items.Add(new MM345TouchComboItem(touch));
                    if (mm3Monster.Touch == touch)
                        comboTouch.SelectedIndex = iIndex;
                }

                for (MM3Target target = MM3Target.First; target < MM3Target.Last; target++)
                {
                    iIndex = comboTarget.Items.Add(new MM345TargetComboItem(target));
                    if (mm3Monster.Target == target)
                        comboTarget.SelectedIndex = iIndex;
                }
                nudGems.Maximum = 65535;
                nudDropItem.Maximum = 6;
            }
            else if (m_monster is MM45Monster)
            {
                MM45Monster mm45Monster = m_monster as MM45Monster;
                for (MM45SpecialAttack touch = MM45SpecialAttack.First; touch < MM45SpecialAttack.Last; touch++)
                {
                    iIndex = comboTouch.Items.Add(new MM345TouchComboItem(touch));
                    if (mm45Monster.SpecialAttack == touch)
                        comboTouch.SelectedIndex = iIndex;
                }

                for (MM45RaceClass target = MM45RaceClass.First; target < MM45RaceClass.Last; target++)
                {
                    iIndex = comboTarget.Items.Add(new MM345TargetComboItem(target));
                    if (mm45Monster.HatesClass == target)
                        comboTarget.SelectedIndex = iIndex;
                }
                nudGems.Maximum = 255;
                nudDropItem.Maximum = 7;
            }

            for (DamageType damageType = DamageType.First; damageType < DamageType.Last; damageType++)
            {
                iIndex = comboDamageType.Items.Add(new MM345DamageTypeComboItem(damageType));
                if (m_monster.DamageType == damageType)
                    comboDamageType.SelectedIndex = iIndex;
            }

            tbName.Text = m_monster.Name;
            Global.SetNud(nudCurrentHP, m_monster.CurrentHP);
            Global.SetNud(nudMaxHP, m_monster.HP);
            Global.SetNud(nudArmorClass, m_monster.AC);
            Global.SetNud(nudSpeed, m_monster.Speed);
            Global.SetNud(nudNumAttacks, m_monster.NumAttacks);
            Global.SetNud(nudNumDice, m_monster.DamageNumDice);
            Global.SetNud(nudDieMax, m_monster.DamageDieMax);
            Global.SetNud(nudGems, m_monster.Gems);
            Global.SetNud(nudExperience, m_monster.Experience);
            Global.SetNud(nudDropItem, m_monster.Items[0]);
            Global.SetNud(nudDropGold, m_monster.Gold);
            Global.SetNud(nudAccuracy, m_monster.Accuracy); 
            cbUndead.Checked = m_monster.Undead;
            cbMissile.Checked = m_monster.Missile;
            nudFireResist.Value = m_monster.Resistances.Fire;
            nudColdResist.Value = m_monster.Resistances.Cold;
            nudElectricResist.Value = m_monster.Resistances.Electricity;
            nudEnergyResist.Value = m_monster.Resistances.Energy;
            nudMagicResist.Value = m_monster.Resistances.Magic;
            nudPoisonResist.Value = m_monster.Resistances.Poison;
            nudPhysicalResist.Value = m_monster.Resistances.Physical;
            nudLocationX.Value = m_monster.Position.X;
            nudLocationY.Value = m_monster.Position.Y;
            cbActive.Checked = m_monster.Active;
            cbKilled.Checked = m_monster.Position.X == 0xff80 && m_monster.Position.Y == 0xff80;

            rbAsleep.Checked = m_monster.Condition == BasicConditionFlags.Asleep;
            rbImmobilized.Checked = m_monster.Condition == BasicConditionFlags.Paralyzed;
            rbSilenced.Checked = m_monster.Condition == BasicConditionFlags.Silenced;
            rbFeebleMind.Checked = m_monster.Condition == BasicConditionFlags.Mindless;
            rbHypnotized.Checked = m_monster.Condition == BasicConditionFlags.Hypnotized;
            rbGood.Checked = m_monster.Condition == BasicConditionFlags.Good;

            m_bUpdatingUI = false;
        }

        public void UpdateFromUI()
        {
            m_monster = GetMonsterFromUI(m_monster);
        }

        public MM345Monster GetMonsterFromUI(MM345Monster source = null)
        {
            MM345Monster monster = null;
            if (source == null)
                monster = m_monster.Clone() as MM345Monster;
            else
                monster = source;

            // Changing the name is complicated and not really worthwhile
            monster.CurrentHP = (int) nudCurrentHP.Value;
            monster.HP = (int) nudMaxHP.Value;
            monster.AC = (int) nudArmorClass.Value;
            monster.Speed = (int) nudSpeed.Value;
            monster.NumAttacks = (int) nudNumAttacks.Value;
            monster.Damage = (int) nudNumDice.Value;
            if (nudDieMax.Value > 255 && monster is MM3Monster)
                monster.DamageDieMax = 238;       // For some reason the game interprets this as "1000"
            else
                monster.DamageDieMax = (int)nudDieMax.Value;
            monster.DamageNumDice = (int)nudNumDice.Value;
            monster.Gems = (int) nudGems.Value;
            monster.Experience = (int) nudExperience.Value;
            monster.Items = new int[] { (int)nudDropItem.Value };
            monster.Gold = (int) nudDropGold.Value;
            monster.Accuracy = (int) nudAccuracy.Value;
            monster.Undead = cbUndead.Checked;
            monster.Missile = cbMissile.Checked;
            monster.Resistances.Fire = (int)nudFireResist.Value;
            monster.Resistances.Cold = (int)nudColdResist.Value;
            monster.Resistances.Electricity = (int)nudElectricResist.Value;
            monster.Resistances.Energy = (int)nudEnergyResist.Value;
            monster.Resistances.Magic = (int)nudMagicResist.Value;
            monster.Resistances.Poison = (int)nudPoisonResist.Value;
            monster.Resistances.Physical = (int)nudPhysicalResist.Value;
            monster.DamageType = ((MM345DamageTypeComboItem)comboDamageType.SelectedItem).DamageType;

            if (monster.DamageType == DamageType.Physical && monster.Accuracy == 0)
                monster.Accuracy = 1;   // This combo crashes MM3/4/5 otherwise

            if (monster is MM345Monster)
            {
                MM345Monster mm345Monster = monster as MM345Monster;

                if (rbAsleep.Checked)
                    mm345Monster.MM345Condition = MM345MonsterCondition.Asleep;
                else if (rbImmobilized.Checked)
                    mm345Monster.MM345Condition = MM345MonsterCondition.Immobilized;
                else if (rbSilenced.Checked)
                    mm345Monster.MM345Condition = MM345MonsterCondition.Silenced;
                else if (rbFeebleMind.Checked)
                    mm345Monster.MM345Condition = MM345MonsterCondition.FeebleMind;
                else if (rbHypnotized.Checked)
                    mm345Monster.MM345Condition = MM345MonsterCondition.Hypnotized;
                else
                    mm345Monster.MM345Condition = MM345MonsterCondition.Good;
            }
            if (monster is MM3Monster)
            {
                MM3Monster mm3Monster = monster as MM3Monster;
                if (comboTouch.SelectedIndex != -1)
                    mm3Monster.Touch = (comboTouch.SelectedItem as MM345TouchComboItem).TouchMM3;
                if (comboTarget.SelectedIndex != -1)
                    mm3Monster.Target = (comboTarget.SelectedItem as MM345TargetComboItem).TargetMM3;

            }
            else if (monster is MM45Monster)
            {
                MM45Monster mm45Monster = monster as MM45Monster;
                if (comboTouch.SelectedIndex != -1)
                    mm45Monster.SpecialAttack = (comboTouch.SelectedItem as MM345TouchComboItem).TouchMM45;
                if (comboTarget.SelectedIndex != -1)
                    mm45Monster.HatesClass = (comboTarget.SelectedItem as MM345TargetComboItem).TargetMM45;
            }

            if (cbKilled.Checked)
            {
                monster.Position.X = 0xff80;
                monster.Position.Y = 0xff80;
            }
            else
            {
                monster.Position.X = (int)nudLocationX.Value;
                monster.Position.Y = (int)nudLocationY.Value;
            }
            monster.Active = cbActive.Checked;

            return monster;
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

        private void comboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingUI)
                return;

            MM345Monster monster = Monsters[comboIndex.SelectedIndex] as MM345Monster;
            if (monster != null)
            {
                m_bUpdatingIndex = true;
                SetMonsterInfo(monster);
                m_bUpdatingIndex = false;
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            int iIndex = 0;
            while (iIndex < comboIndex.Items.Count)
            {
                if (comboIndex.Items[iIndex].ToString().ToLower().Contains(tbSearch.Text.ToLower()))
                {
                    comboIndex.SelectedIndex = iIndex;
                    return;
                }
                iIndex++;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        private void FindNext()
        {
            Global.FindNext(comboIndex, tbSearch.Text);
        }

        private void FindPrevious()
        {
            Global.FindNext(comboIndex, tbSearch.Text, false);
        }

        private void cbKilled_CheckedChanged(object sender, EventArgs e)
        {
            nudLocationX.Enabled = !cbKilled.Checked;
            nudLocationY.Enabled = !cbKilled.Checked;

            if (!cbKilled.Checked)
            {
                if (nudLocationX.Value == 0xff80)
                    nudLocationX.Value = 0;
                if (nudLocationY.Value == 0xff80)
                    nudLocationY.Value = 0;
            }
        }

        private void llResetDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Global.Debug && NativeMethods.IsControlDown())
            {
                MM345Monster monsterTemp = GetMonsterFromUI();
                StringsViewForm viewForm = new StringsViewForm();
                viewForm.Strings = Global.ByteString(monsterTemp.GetBytes());
                viewForm.ShowDialog();
                return;
            }

            if (MessageBox.Show(String.Format("Reset all of the monster index #{0} statistics to the \"{1}\" defaults?", m_monster.Index,
                Games.Name(Hacker.Game)), "Reset to default?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            MM345Monster monster = Monsters[comboIndex.SelectedIndex] as MM345Monster;
            if (monster == null)
            {
                Global.InternalError("The default monster data could not be loaded.");
                return;
            }

            MM345Monster monsterNew = Hacker.GetDefaultMonster(monster.Index, monster is MM45Monster ? (int)((MM45Monster)monster).Side : 0) as MM345Monster;
            if (monsterNew == null)
            {
                Global.InternalError("The default monster data could not be loaded.");
                return;
            }

            TransferMetadata(m_monster, monsterNew);

            SetMonsterInfo(monsterNew);
        }
    }

    public class MM345TouchComboItem : GameMultiEnum
    {
        public MM3Touch TouchMM3 { get { return (MM3Touch) Index; }  }
        public MM345TouchComboItem(MM3Touch touch) { SetIndex(touch, GameNames.MightAndMagic3); }

        public MM45SpecialAttack TouchMM45 { get { return (MM45SpecialAttack) Index; } }
        public MM345TouchComboItem(MM45SpecialAttack special) { SetIndex(special, GameNames.MightAndMagic45); }

        public override string ToString()
        {
            switch (Game)
            {
                case GameNames.MightAndMagic3: return MM3Monster.GetTouchString(TouchMM3);
                case GameNames.MightAndMagic45: return MM45Monster.GetSpecialAttackString(TouchMM45, true);
                default: return "Unknown";
            }
        }
    }

    public class MM345TargetComboItem : GameMultiEnum
    {
        public MM3Target TargetMM3 { get { return (MM3Target)Index; } }
        public MM45RaceClass TargetMM45 { get { return (MM45RaceClass)Index; } }

        public MM345TargetComboItem(MM3Target target) { SetIndex(target, GameNames.MightAndMagic3); }
        public MM345TargetComboItem(MM45RaceClass target) { SetIndex(target, GameNames.MightAndMagic45); }

        public override string ToString()
        {
            switch (Game)
            {
                case GameNames.MightAndMagic3: return MM3Monster.GetTargetString(TargetMM3);
                case GameNames.MightAndMagic45: return MM45Monster.GetHatesString(TargetMM45);
                default: return "Unknown";
            }
        }
    }

    public class MM345DamageTypeComboItem
    {
        public DamageType DamageType;

        public MM345DamageTypeComboItem(DamageType damageType)
        {
            DamageType = damageType;
        }

        public override string ToString()
        {
            return MMMonster.GetDamageTypeString(DamageType);
        }
    }

}
