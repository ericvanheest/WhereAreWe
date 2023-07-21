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
    public partial class BT3MonsterEditForm : Form
    {
        private BT3Monster m_monster;
        private bool m_bLoaded = false;

        public BT3Monster Monster
        {
            get
            {
                UpdateFromUI();
                return m_monster;
            }

            set
            {
                m_monster = value;
                UpdateUI();
            }
        }

        public BT3MonsterEditForm()
        {
            InitializeComponent();
            BT3AttackControl.InitFaces(comboHPFaces);
        }

        private void UpdateFromUI()
        {
            if (!m_bLoaded)
                return;
            m_monster.SetEncodedName(tbName.Text);
            UpdateNameLabel();
            m_monster.HPDice = new DamageDice(BT3AttackControl.FacesFromIndex(comboHPFaces.SelectedIndex), (int)nudHPRange.Value, (int)nudHPMinimum.Value);
            m_monster.GroupSize = (int)nudGroupSize.Value;
            m_monster.InitialDistance = (int)nudDistance.Value;
            m_monster.AdvancePerRound = (int)nudAdvance.Value;
            m_monster.AC = (int)nudArmorClass.Value;
            m_monster.InitiativeMin = (byte)nudInitiativeLow.Value;
            m_monster.InitiativeMax = (byte)nudInitiativeHigh.Value;
            m_monster.Speed = (byte)nudSpeed.Value;
            m_monster.MagicResistHalf = (byte)nudMagicHalf.Value;
            m_monster.MagicResistFull = (byte)nudMagicFull.Value;
            m_monster.Experience = (long) nudExperience.Value;
            m_monster.ImageIndex = (byte)nudImageIndex.Value;
            m_monster.Attacks = new BT3MonsterAttack[] { attack1.Attack, attack2.Attack, attack3.Attack, attack4.Attack };
            m_monster.Evade = (byte)nudEvade.Value;
            m_monster.Unknown1E = (byte)nudUnknown2.Value;
            m_monster.Unknown1F = (byte)nudUnknown3.Value;
            m_monster.Unknown26 = (byte)nudUnknown4.Value;
            m_monster.ACIgnoreLow = (int)nudACIgnoreLow.Value;
            m_monster.ACIgnoreHigh = (int)nudACIgnoreHigh.Value;
            m_monster.Illusion = cbIllusion.Checked;
            m_monster.NoRandom = cbNoRandomEncounter.Checked;
            m_monster.AttackHigh = (cbUnknown26.Checked ? 0x40 : 0) | (cbUnknown27.Checked ? 0x80 : 0);
            SetCategory(m_monster, BT3MonsterCategories.Dragon, cbDragon.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Mechanical, cbMechanical.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Warrior, cbWarrior.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Fiend, cbFiend.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Death, cbDeath.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Spellcaster, cbSpellcaster.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Fast, cbFast.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Supernatural, cbSupernatural.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown08, cbUnknown8.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Stalker, cbUnknown9.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown10, cbUnknown10.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Insane, cbUnknown11.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Poison, cbUnknown12.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Dark, cbUnknown13.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Bone, cbUnknown14.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Ice, cbUnknown15.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Colored, cbUnknown16.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown17, cbUnknown17.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Magical, cbUnknown18.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown19, cbUnknown19.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown20, cbUnknown20.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown21, cbUnknown21.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown22, cbUnknown22.Checked);
            SetCategory(m_monster, BT3MonsterCategories.Unknown23, cbUnknown23.Checked);

            tbBytes.Text = Global.ByteString(m_monster.GetBytes());
        }

        private void UpdateNameLabel()
        {
            labelName.Text = String.Format("{0}, {1}", m_monster.FullName.Singular, m_monster.FullName.Plural);
        }

        private void UpdateUI()
        {
            UpdateNameLabel();
            tbName.Text = m_monster.GetEncodedName();
            Global.SetNud(nudHPMinimum, m_monster.HPDice.Bonus);
            Global.SetNud(nudHPRange, m_monster.HPDice.Quantity);
            comboHPFaces.SelectedIndex = BT3AttackControl.IndexFromFaces(m_monster.HPDice.Faces);
            Global.SetNud(nudGroupSize, m_monster.GroupSize);
            Global.SetNud(nudDistance, m_monster.InitialDistance);
            Global.SetNud(nudAdvance, m_monster.AdvancePerRound);
            Global.SetNud(nudArmorClass, m_monster.AC);
            Global.SetNud(nudInitiativeLow, m_monster.InitiativeMin);
            Global.SetNud(nudInitiativeHigh, m_monster.InitiativeMax);
            Global.SetNud(nudSpeed, m_monster.Speed);
            Global.SetNud(nudMagicHalf, m_monster.MagicResistHalf);
            Global.SetNud(nudMagicFull, m_monster.MagicResistFull);
            Global.SetNud(nudExperience, m_monster.Experience);
            Global.SetNud(nudImageIndex, m_monster.ImageIndex);
            SetAttack(attack1, m_monster, 0);
            SetAttack(attack2, m_monster, 1);
            SetAttack(attack3, m_monster, 2);
            SetAttack(attack4, m_monster, 3);
            Global.SetNud(nudEvade, m_monster.Evade);
            Global.SetNud(nudUnknown2, m_monster.Unknown1E);
            Global.SetNud(nudUnknown3, m_monster.Unknown1F);
            Global.SetNud(nudUnknown4, m_monster.Unknown26);
            Global.SetNud(nudACIgnoreLow, m_monster.ACIgnoreLow);
            Global.SetNud(nudACIgnoreHigh, m_monster.ACIgnoreHigh);
            cbIllusion.Checked = m_monster.Illusion;
            cbNoRandomEncounter.Checked = m_monster.NoRandom;
            cbUnknown26.Checked = (m_monster.AttackHigh & 0x40) > 0;
            cbUnknown27.Checked = (m_monster.AttackHigh & 0x80) > 0;
            cbDragon.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Dragon);
            cbMechanical.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Mechanical);
            cbWarrior.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Warrior);
            cbFiend.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Fiend);
            cbDeath.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Death);
            cbSpellcaster.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Spellcaster);
            cbFast.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Fast);
            cbSupernatural.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Supernatural);
            cbUnknown8.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown08);
            cbUnknown9.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Stalker);
            cbUnknown10.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown10);
            cbUnknown11.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Insane);
            cbUnknown12.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Poison);
            cbUnknown13.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Dark);
            cbUnknown14.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Bone);
            cbUnknown15.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Ice);
            cbUnknown16.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Colored);
            cbUnknown17.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown17);
            cbUnknown18.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Magical);
            cbUnknown19.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown19);
            cbUnknown20.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown20);
            cbUnknown21.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown21);
            cbUnknown22.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown22);
            cbUnknown23.Checked = m_monster.Categories.HasFlag(BT3MonsterCategories.Unknown23);

            tbBytes.Text = Global.ByteString(m_monster.GetBytes());
        }

        private void SetCategory(BT3Monster monster, BT3MonsterCategories category, bool bSet)
        {
            if (bSet)
                monster.Categories |= category;
            else
                monster.Categories &= ~(category);
        }

        private void SetAttack(BT3AttackControl attack, BT3Monster monster, int index)
        {
            if (attack == null || monster == null || monster.Attacks == null || index < 0 || index >= monster.Attacks.Length)
                return;
            attack.Attack = monster.Attacks[index];
        }

        private void BT3MonsterEditForm_Load(object sender, EventArgs e)
        {
            m_bLoaded = true;
        }

        private void onValueChanged(object sender, EventArgs e)
        {
            UpdateFromUI();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            UpdateFromUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
