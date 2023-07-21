using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class Wiz123MonsterEditForm : CommonKeyForm
    {
        private GameNames m_game = GameNames.None;
        WizMonster m_monster;
        private bool m_bUpdatingAttacks = false;
        private bool m_bUpdatingUI = false;
        private ToolTip m_tipReward = new ToolTip();

        public Wiz123MonsterEditForm()
        {
            InitializeComponent();
        }

        public Wiz123MonsterEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;
            comboFamily.Items.Clear();
            for(WizMonsterFamily family = WizMonsterFamily.Fighter; family <= WizMonsterFamily.Construct; family++)
                comboFamily.Items.Add(WizMonster.GetFamilyString(family));

            comboGroupHelp.Items.Clear();

            IEnumerable<Monster> monsters = Games.GetMonsters(game);

            foreach (WizMonster monster in monsters)
                comboGroupHelp.Items.Add(monster.ProperName);

            comboBreathWeapon.Items.Clear();
            for (WizBreath breath = WizBreath.None; breath <= WizBreath.Stone; breath++)
                comboBreathWeapon.Items.Add(WizMonster.GetBreathString(breath));

            CommonKeySelectAll += Wiz1MonsterEditForm_CommonKeySelectAll;
        }

        void Wiz1MonsterEditForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvAttacks.Items)
                lvi.Selected = true;
        }

        public WizMonster Monster
        {
            get
            {
                return m_monster;
            }
            set
            {
                m_monster = value;
                UpdateUI();
            }
        }

        private void AddAttack(int index, DamageDice attack)
        {
            ListViewItem lvi = new ListViewItem((index+1).ToString());
            lvi.SubItems.Add(attack.Quantity.ToString());
            lvi.SubItems.Add(String.Format("d{0}", attack.Faces));
            lvi.SubItems.Add(Global.AddPlus(attack.Bonus));
            lvi.Tag = attack;
            lvAttacks.Items.Add(lvi);
        }

        public void UpdateUI()
        {
            UpdateUI(m_monster);
        }

        public void UpdateUI(WizMonster monster)
        {
            m_bUpdatingUI = true;
            Global.SetNud(nudGroupQuantity, monster.NumAppearing.Quantity);
            Global.SetNud(nudGroupFaces, monster.NumAppearing.Faces);
            Global.SetNud(nudGroupMod, monster.NumAppearing.Bonus);
            Global.SetNud(nudHPQuantity, monster.HitPoints.Quantity);
            Global.SetNud(nudHPFaces, monster.HitPoints.Faces);
            Global.SetNud(nudHPMod, monster.HitPoints.Bonus);
            Global.SetIndex(comboFamily, (int) monster.Family);
            Global.SetIndex(comboBreathWeapon, (int)monster.BreathWeapon);
            Global.SetIndex(comboGroupHelp, monster.Help);
            Global.SetNud(nudHelpChance, monster.GroupHelpChance);
            Global.SetNud(nudReward1, monster.Reward1);
            Global.SetNud(nudReward2, monster.Reward2);
            Global.SetNud(nudArmorClass, monster.AC);
            Global.SetNud(nudMagicRes, monster.MagicResist);
            Global.SetNud(nudDrain, monster.Drain);
            Global.SetNud(nudUnique, monster.Unique);
            Global.SetNud(nudRegeneration, monster.Regeneration);
            Global.SetNud(nudMageSpellLevel, monster.MageSpellLevel);
            Global.SetNud(nudPriestSpellLevel, monster.PriestSpellLevel);
            lvAttacks.BeginUpdate();
            lvAttacks.Items.Clear();
            for(int i = 0; i < monster.Attacks.Count; i++)
                AddAttack(i, monster.Attacks[i]);
            lvAttacks.EndUpdate();
            labelName.Text = monster.ProperName;
            cbPhysical.Checked = monster.Resistances.HasFlag(WizResist.Physical);
            cbFire.Checked = monster.Resistances.HasFlag(WizResist.Fire);
            cbCold.Checked = monster.Resistances.HasFlag(WizResist.Cold);
            cbPoison.Checked = monster.Resistances.HasFlag(WizResist.Poison);
            cbLevelDrain.Checked = monster.Resistances.HasFlag(WizResist.LevelDrain);
            cbStone.Checked = monster.Resistances.HasFlag(WizResist.Stone);
            cbMagic.Checked = monster.Resistances.HasFlag(WizResist.Magic);
            cbAutokill.Checked = monster.Properties.HasFlag(WizMonsterProperty.Autokill);
            cbCallHelp.Checked = monster.Properties.HasFlag(WizMonsterProperty.CallHelp);
            cbParalyze.Checked = monster.Properties.HasFlag(WizMonsterProperty.Paralyze);
            cbPoisonProperty.Checked = monster.Properties.HasFlag(WizMonsterProperty.Poison);
            cbRunAway.Checked = monster.Properties.HasFlag(WizMonsterProperty.RunAway);
            cbSleep.Checked = monster.Properties.HasFlag(WizMonsterProperty.Sleep);
            cbStoneProperty.Checked = monster.Properties.HasFlag(WizMonsterProperty.Stone);
            m_bUpdatingUI = false;
            UpdateExperience(monster);
        }

        private void UpdateExperience(WizMonster monster)
        {
            if (m_bUpdatingUI)
                return;
            labelExperience.Text = monster.Experience.ToString();
        }

        public void UpdateFromUI()
        {
            UpdateFromUI(m_monster);
        }

        public void UpdateFromUI(WizMonster monster)
        {
            monster.NumAppearing.Quantity = (int) nudGroupQuantity.Value;
            monster.NumAppearing.Faces = (int) nudGroupFaces.Value;
            monster.NumAppearing.Bonus = (int) nudGroupMod.Value;
            monster.HitPoints.Quantity = (int) nudHPQuantity.Value;
            monster.HitPoints.Faces = (int) nudHPFaces.Value;
            monster.HitPoints.Bonus = (int) nudHPMod.Value;
            monster.Family = (WizMonsterFamily)comboFamily.SelectedIndex;
            monster.BreathWeapon = (WizBreath)comboBreathWeapon.SelectedIndex;
            monster.Help = comboGroupHelp.SelectedIndex;
            monster.GroupHelpChance = (int) nudHelpChance.Value;
            monster.Reward1 = (int) nudReward1.Value;
            monster.Reward2 = (int) nudReward2.Value;
            monster.AC = (int) nudArmorClass.Value;
            monster.MagicResist = (int) nudMagicRes.Value;
            monster.Drain = (int) nudDrain.Value;
            monster.Unique = (int) nudUnique.Value;
            monster.Regeneration = (int) nudRegeneration.Value;
            monster.MageSpellLevel = (int) nudMageSpellLevel.Value;
            monster.PriestSpellLevel = (int) nudPriestSpellLevel.Value;
            monster.Attacks = new List<DamageDice>(lvAttacks.Items.Count);
            foreach (ListViewItem lvi in lvAttacks.Items)
            {
                DamageDice attack = lvi.Tag as DamageDice;
                if (attack != null)
                    monster.Attacks.Add(attack);
            }
            monster.NumAttacks = monster.Attacks.Count;
            WizResist resist = WizResist.None;
            resist |= cbPhysical.Checked ? WizResist.Physical : 0;
            resist |= cbFire.Checked ? WizResist.Fire : 0;
            resist |= cbCold.Checked ? WizResist.Cold : 0;
            resist |= cbPoison.Checked ? WizResist.Poison : 0;
            resist |= cbLevelDrain.Checked ? WizResist.LevelDrain : 0;
            resist |= cbStone.Checked ? WizResist.Stone : 0;
            resist |= cbMagic.Checked ? WizResist.Magic : 0;
            monster.Resistances = resist;
            WizMonsterProperty property = WizMonsterProperty.None;
            property |= cbAutokill.Checked ? WizMonsterProperty.Autokill : 0;
            property |= cbCallHelp.Checked ? WizMonsterProperty.CallHelp : 0;
            property |= cbParalyze.Checked ? WizMonsterProperty.Paralyze : 0;
            property |= cbPoisonProperty.Checked ? WizMonsterProperty.Poison : 0;
            property |= cbRunAway.Checked ? WizMonsterProperty.RunAway : 0;
            property |= cbSleep.Checked ? WizMonsterProperty.Sleep : 0;
            property |= cbStoneProperty.Checked ? WizMonsterProperty.Stone : 0;
            monster.Properties = property;
            monster.Experience = monster.CalculateExp();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            UpdateFromUI();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmAddRemove_Opening(object sender, CancelEventArgs e)
        {
            miCtxAdd.Enabled = (lvAttacks.Items.Count < 7);
            miCtxRemove.Enabled = (lvAttacks.SelectedItems.Count > 0);
            ExpSettingChanged(sender, e);
        }

        private void miCtxAdd_Click(object sender, EventArgs e)
        {
            if (lvAttacks.Items.Count > 6)
                return;

            AddAttack(lvAttacks.Items.Count + 1, DamageDice.Zero);
            RenumberAttacks();
            ExpSettingChanged(sender, e);
        }

        private void miCtxRemove_Click(object sender, EventArgs e)
        {
            DeleteSelectedAttacks();
            ExpSettingChanged(sender, e);
        }

        private void DeleteSelectedAttacks()
        {
            lvAttacks.BeginUpdate();
            foreach (ListViewItem lvi in lvAttacks.SelectedItems)
                lvi.Remove();
            lvAttacks.EndUpdate();
            RenumberAttacks();
        }

        private void RenumberAttacks()
        {
            lvAttacks.BeginUpdate();
            for (int i = 0; i < lvAttacks.Items.Count; i++)
                lvAttacks.Items[i].Text = (i + 1).ToString();
            lvAttacks.EndUpdate();
        }

        private void lvAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bAny = lvAttacks.SelectedItems.Count > 0;
            nudAttackQuantity.Enabled = bAny;
            nudAttackFaces.Enabled = bAny;
            nudAttackModifier.Enabled = bAny;

            if (bAny)
            {
                DamageDice attack = lvAttacks.SelectedItems[0].Tag as DamageDice;
                if (attack != null)
                {
                    m_bUpdatingAttacks = true;
                    Global.SetNud(nudAttackQuantity, attack.Quantity);
                    Global.SetNud(nudAttackFaces, attack.Faces);
                    Global.SetNud(nudAttackModifier, attack.Bonus);
                    m_bUpdatingAttacks = false;
                }
            }
        }

        private void nudAttackQuantity_ValueChanged(object sender, EventArgs e) { ChangeAttackQuantity(); }
        private void nudAttackFaces_ValueChanged(object sender, EventArgs e) { ChangeAttackFaces(); }
        private void nudAttackModifier_ValueChanged(object sender, EventArgs e) { ChangeAttackModifier(); }
        private void nudAttackQuantity_KeyDown(object sender, KeyEventArgs e) { ChangeAttackQuantity(); }
        private void nudAttackFaces_KeyDown(object sender, KeyEventArgs e) { ChangeAttackFaces(); }
        private void nudAttackModifier_KeyDown(object sender, KeyEventArgs e) { ChangeAttackModifier(); }

        private void ChangeAttackQuantity()
        {
            if (m_bUpdatingAttacks)
                return;
            foreach (ListViewItem lvi in lvAttacks.SelectedItems)
            {
                DamageDice attack = lvi.Tag as DamageDice;
                if (attack != null)
                {
                    attack.Quantity = (int)nudAttackQuantity.Value;
                    lvi.SubItems[1].Text = attack.Quantity.ToString();
                }
            }
        }

        private void ChangeAttackFaces()
        {
            if (m_bUpdatingAttacks)
                return;
            foreach (ListViewItem lvi in lvAttacks.SelectedItems)
            {
                DamageDice attack = lvi.Tag as DamageDice;
                if (attack != null)
                {
                    attack.Faces = (int)nudAttackFaces.Value;
                    lvi.SubItems[2].Text = String.Format("d{0}", attack.Faces);
                }
            }
        }

        private void ChangeAttackModifier()
        {
            if (m_bUpdatingAttacks)
                return;
            foreach (ListViewItem lvi in lvAttacks.SelectedItems)
            {
                DamageDice attack = lvi.Tag as DamageDice;
                if (attack != null)
                {
                    attack.Bonus = (int)nudAttackModifier.Value;
                    lvi.SubItems[3].Text = Global.AddPlus(attack.Bonus);
                }
            }
        }

        private void llResetAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show(String.Format("Reset all of the values for monster #{0} ({1}) to their defaults?", Monster.Index, Monster.ProperName),
                "Reset values?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            UpdateUI(Games.GetWizGlobals(m_game).GetInternalMonster(Monster.Index));
        }

        private void lvAttacks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedAttacks();
        }

        private void ExpSettingChanged(object sender, EventArgs e)
        {
            WizMonster monster = m_monster.Clone() as WizMonster;
            UpdateFromUI(monster);
            UpdateExperience(monster);
        }

        private void ExpSetting_KeyDown(object sender, KeyEventArgs e)
        {
            ExpSettingChanged(sender, e);
        }

        private void llRewardHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strDescriptions = Games.GetWizGlobals(m_game).GetTreasureDescriptions();
            ViewInfoForm.ShowCentered(this, strDescriptions, String.Format("Wizardry {0} Reward Levels", (int) (m_game - GameNames.Wizardry1 + 1)), 600);
        }
    }
}
