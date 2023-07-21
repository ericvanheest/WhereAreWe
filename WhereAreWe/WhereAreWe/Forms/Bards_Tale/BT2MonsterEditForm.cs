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
    public partial class BT2MonsterEditForm : Form
    {
        private BT2Monster m_monster;
        private bool m_bLoaded = false;

        public BT2Monster Monster
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

        public BT2MonsterEditForm()
        {
            InitializeComponent();
            InitCombos();
        }

        public static string AttackString(int iAttack, bool bAbbrev = false)
        {
            if (iAttack < 1)
                return bAbbrev ? "melee" : "Melee Attack";
            if (iAttack < (int)BT2SpellIndex.MangarsMallet)
                return bAbbrev ? BT2.Spells[iAttack].Abbreviation : String.Format("{0}: {1}", BT2.Spells[iAttack].Abbreviation, BT2.Spells[iAttack].Name);
            if (bAbbrev)
            {
                switch (iAttack)
                {
                    case 79: return "invalid";
                    case 80: return "breath 20'";
                    case 81: return "breath 20'";
                    case 82: return "breath 30'";
                    case 83: return "breath 40'";
                    case 84: return "breath 40'";
                    case 85: return "breath 50'";
                    case 86: return "breath 50'";
                    case 87: return "breath 60'";
                    case 88: return "breath 60'";
                    case 89: return "breath 70'";
                    case 90: return "breath 70'";
                    case 91: return "breath 80'";
                    case 92: return "breath 80'";
                    case 93: return "breath 80'";
                    case 94: return "breath 90'";
                    case 95: return "breath 90'";
                    case 96: return "light";
                    case 97: return "light";
                    case 98: return "arrow 30'";
                    case 99: return "spear 20'";
                    case 100: return "axe 20'";
                    case 101: return "shuriken 30'";
                    case 102: return "spear 40'";
                    case 103: return "arrow 50'";
                    case 104: return "boomerang 40'";
                    case 105: return "shuriken 50'";
                    case 106: return "arrow 90'";
                    case 107: return "hammer 70'";
                    case 108: return "arrow 70'";
                    case 109: return "axe 80'";
                    case 110: return "blade 80'";
                    case 111: return "blade 90'";
                    case 112: return "giant";
                    case 113: return "m. man";
                    case 114: return "bulldozer";
                    case 115: return "slayer";
                    case 116: return "vanquisher";
                    case 117: return "blast dr.";
                    case 118: return "mage";
                    case 119: return "herb";
                    case 120: return "wolf";
                    case 121: return "ogre";
                    default: return "wolf";
                }
            }

            switch (iAttack)
            {
                case 79: return "Invalid";
                case 80: return "Breath (20')";
                case 81: return "Breath (20')";
                case 82: return "Breath (30')";
                case 83: return "Breath (40')";
                case 84: return "Breath (40')";
                case 85: return "Breath (50')";
                case 86: return "Breath (50')";
                case 87: return "Breath (60')";
                case 88: return "Breath (60')";
                case 89: return "Breath (70')";
                case 90: return "Breath (70')";
                case 91: return "Breath (80')";
                case 92: return "Breath (80')";
                case 93: return "Breath (80')";
                case 94: return "Breath (90')";
                case 95: return "Breath (90')";
                case 96: return "Make a light";
                case 97: return "Make a light";
                case 98: return "Shoot an arrow (30')";
                case 99: return "Throw a spear (20')";
                case 100: return "Throw an axe (20')";
                case 101: return "Hurl a shuriken (30')";
                case 102: return "Throw a spear (40')";
                case 103: return "Shoot an arrow (50')";
                case 104: return "Throw a boomerang (40')";
                case 105: return "Hurl a shuriken (50')";
                case 106: return "Shoot an arrow (90')";
                case 107: return "Hurl a hammer (70')";
                case 108: return "Shoot an arrow (70')";
                case 109: return "Throw an axe (80')";
                case 110: return "Throw a blade (80')";
                case 111: return "Throw a blade (90')";
                case 112: return "Summon Storm Giant";
                case 113: return "Summon Molten Man";
                case 114: return "Summon Bulldozer";
                case 115: return "Summon Slayer";
                case 116: return "Summon Vanquisher";
                case 117: return "Summon Blast Dragon";
                case 118: return "Summon Master Mage";
                case 119: return "Summon Herb";
                case 120: return "Summon Wolf";
                case 121: return "Summon Ogre";
                default: return "Summon Wolf";
            }
        }

        private void InitCombos()
        {
            foreach (ComboBox cb in new ComboBox[] { comboAttack1, comboAttack2, comboAttack3, comboAttack4 })
            {
                for (int attack = 0; attack < 122; attack++)
                    cb.Items.Add(AttackString(attack));
            }
            comboTouch.Items.Add("None");
            for (int i = 1; i < 8; i++)
                comboTouch.Items.Add(BTMonster.GetMonsterSpecial((BTMonsterSpecial)i));
        }

        private void UpdateFromUI()
        {
            if (!m_bLoaded)
                return;
            m_monster.HPDice = new DamageDice((int)nudHPRange.Value, 1, (int)nudHPMinimum.Value);
            m_monster.GroupSize = (int)nudGroupSize.Value;
            m_monster.InitialDistance = (int)nudDistance.Value;
            m_monster.Speed = (int)nudSpeed.Value;
            m_monster.AC = (int)nudArmorClass.Value;
            m_monster.NumAttacks = (int)nudNumAttacks.Value;
            m_monster.Attacks[0] = (byte)comboAttack1.SelectedIndex;
            m_monster.Attacks[1] = (byte)comboAttack2.SelectedIndex;
            m_monster.Attacks[2] = (byte)comboAttack3.SelectedIndex;
            m_monster.Attacks[3] = (byte)comboAttack4.SelectedIndex;
            m_monster.Special = (BTMonsterSpecial)comboTouch.SelectedIndex;
            m_monster.DamDice.Quantity = (int)nudDamage.Value;

            tbBytes.Text = Global.ByteString(m_monster.GetBytes());
        }

        private void UpdateUI()
        {
            labelName.Text = m_monster.Name;
            Global.SetNud(nudHPMinimum, m_monster.HPDice.Bonus);
            Global.SetNud(nudHPRange, m_monster.HPDice.Faces);
            Global.SetNud(nudGroupSize, m_monster.GroupSize);
            Global.SetNud(nudDistance, m_monster.InitialDistance);
            Global.SetNud(nudSpeed, m_monster.Speed);
            Global.SetNud(nudArmorClass, m_monster.AC);
            Global.SetNud(nudNumAttacks, m_monster.NumAttacks);
            Global.SetNud(nudDamage, m_monster.DamDice.Quantity);
            Global.SetIndex(comboAttack1, m_monster.Attacks[0]);
            Global.SetIndex(comboAttack2, m_monster.Attacks[1]);
            Global.SetIndex(comboAttack3, m_monster.Attacks[2]);
            Global.SetIndex(comboAttack4, m_monster.Attacks[3]);
            Global.SetIndex(comboTouch, (int) m_monster.Special);
            tbBytes.Text = Global.ByteString(m_monster.GetBytes());
        }

        private void BT2MonsterEditForm_Load(object sender, EventArgs e)
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
