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
    public partial class EOBMonsterEditForm : Form
    {
        private EOBMonster m_monster;
        private bool m_bLoaded = false;
        private CheckBox[] cbSpecials;

        public EOBMonster Monster
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

        public EOBMonsterEditForm()
        {
            InitializeComponent();
            cbSpecials = new CheckBox[] { cbUnknown01, cbUnknown02, cbUnknown04, cbUnknown08, cbPoison, cbParalyze, cbUnknown40, cbRust };
        }

        public byte[] ItemTable { get; set; }

        private void UpdateFromUI()
        {
            if (!m_bLoaded)
                return;
            m_monster.CurrentHP = (int)nudHPCurrent.Value;
            m_monster.HP = (int)nudHPMax.Value;
            m_monster.Items = new int[] { (int)nudWeapon.Value, (int)nudPocket.Value };
            m_monster.Weapon = ItemFromTable((int)nudWeapon.Value);
            m_monster.PocketItem = ItemFromTable((int)nudPocket.Value);
            m_monster.UndeadPower = (int)nudUndead.Value;
            m_monster.NumAttacks = (int)nudNumAttacks.Value;
            m_monster.MonsterSize = (EOBMonsterSize)nudSize.Value;
            m_monster.Special = EOBMonsterSpecial.None;
            for (int i = 0; i < 8; i++)
                if (cbSpecials[i].Checked)
                    m_monster.Special |= (EOBMonsterSpecial)(1 << i);

            m_monster.AC = (int)nudArmorClass.Value;
            m_monster.Speed = (byte)nudSpeed.Value;
            m_monster.Experience = (long) nudExperience.Value;
            m_monster.Attack1 = attack1.Dice;
            m_monster.Attack2 = attack2.Dice;
            m_monster.Attack3 = attack3.Dice;
            m_monster.RawBytes[1] = unchecked((byte)(sbyte)nudUnknown1.Value);
            m_monster.HitDice = (int) nudHitDice.Value;
            m_monster.RawBytes[13] = unchecked((byte)(sbyte)nudUnknown3.Value);
            m_monster.RawBytes[15] = unchecked((byte)(sbyte)nudUnknown4.Value);
            m_monster.RawBytes[22] = unchecked((byte)(sbyte)nudUnknown5.Value);
            m_monster.RawBytes[23] = unchecked((byte)(sbyte)nudUnknown6.Value);
            m_monster.RawBytes[24] = unchecked((byte)(sbyte)nudUnknown7.Value);
            m_monster.RawBytes[26] = unchecked((byte)(sbyte)nudUnknown8.Value);
        }

        private void UpdateUI()
        {
            labelName.Text = m_monster.Name;
            Global.SetNud(nudHPCurrent, m_monster.CurrentHP);
            Global.SetNud(nudHPMax, m_monster.HP);
            Global.SetNud(nudWeapon, m_monster.Items[0]);
            Global.SetNud(nudPocket, m_monster.Items[1]);
            Global.SetNud(nudUndead, m_monster.UndeadPower);
            Global.SetNud(nudNumAttacks, m_monster.NumAttacks);
            Global.SetNud(nudSize, (int) m_monster.MonsterSize);
            for (int i = 0; i < 8; i++)
                cbSpecials[i].Checked = m_monster.Special.HasFlag((EOBMonsterSpecial)(1 << i));

            cbParalyze.Checked = m_monster.Special.HasFlag(EOBMonsterSpecial.Paralyze);
            cbPoison.Checked = m_monster.Special.HasFlag(EOBMonsterSpecial.Poison);
            cbRust.Checked = m_monster.Special.HasFlag(EOBMonsterSpecial.Rust);
            attack1.Dice = m_monster.Attack1;
            attack2.Dice = m_monster.Attack2;
            attack3.Dice = m_monster.Attack3;
            Global.SetNud(nudArmorClass, m_monster.AC);
            Global.SetNud(nudSpeed, m_monster.Speed);
            Global.SetNud(nudExperience, m_monster.Experience);
            Global.SetNud(nudUnknown1, (sbyte) m_monster.RawBytes[1]);
            Global.SetNud(nudHitDice, m_monster.HitDice);
            Global.SetNud(nudUnknown3, (sbyte) m_monster.RawBytes[13]);
            Global.SetNud(nudUnknown4, (sbyte) m_monster.RawBytes[15]);
            Global.SetNud(nudUnknown5, (sbyte) m_monster.RawBytes[22]);
            Global.SetNud(nudUnknown6, (sbyte) m_monster.RawBytes[23]);
            Global.SetNud(nudUnknown7, (sbyte) m_monster.RawBytes[24]);
            Global.SetNud(nudUnknown8, (sbyte) m_monster.RawBytes[26]);
        }

        private void EOBMonsterEditForm_Load(object sender, EventArgs e)
        {
            m_bLoaded = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }

        private EOBItem ItemFromTable(int iIndex)
        {
            if (ItemTable != null && iIndex != 0)
                return EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder1, BitConverter.GetBytes((short)iIndex), ItemTable, 0);
            return null;
        }

        private void nudItem_ValueChanged(object sender, EventArgs e)
        {
            EOBItem item = ItemFromTable((int)nudWeapon.Value);
            labelWeapon.Text = item == null ? "(None)" : item.Name;
        }

        private void nudPocket_ValueChanged(object sender, EventArgs e)
        {
            EOBItem item = ItemFromTable((int)nudPocket.Value);
            labelPocket.Text = item == null ? "(None)" : item.Name;
        }
    }
}
