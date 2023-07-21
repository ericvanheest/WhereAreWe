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
    public partial class MM2MonsterEditForm : HackerBasedForm
    {
        private MonsterBasicInfo m_monsterBasicInfo;
        private bool m_bUpdatingIndex = false;
        private bool m_bUpdatingUI = false;

        public MM2MonsterEditForm()
        {
            InitializeComponent();
        }

        public MM2MonsterEditForm(IMain main)
        {
            InitializeComponent();
            m_main = main;
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

        public void SetMonsterInfo(MonsterBasicInfo info)
        {
            m_monsterBasicInfo = info;
            UpdateUI();
        }

        public MonsterBasicInfo GetMonsterInfo()
        {
            return m_monsterBasicInfo;
        }

        public void AddCondition(MM1MonsterCombatStatus status, bool bChecked)
        {
            ListViewItem lvi = new ListViewItem(MM1Monster.GetMonsterConditionSingle(status));
            lvi.Tag = status;
            lvi.Checked = bChecked;
            lvCondition.Items.Add(lvi);
        }

        public void AddCondition(MM2MonsterCombatStatus status, bool bChecked)
        {
            ListViewItem lvi = new ListViewItem(MM2Monster.GetMonsterConditionSingle(status));
            lvi.Tag = status;
            lvi.Checked = bChecked;
            lvCondition.Items.Add(lvi);
        }

        public void AddResistance(MM1Resistances resist, bool bChecked)
        {
            ListViewItem lvi = new ListViewItem(MM1Monster.GetMonsterResistanceSingle(resist));
            lvi.Tag = resist;
            lvi.Checked = bChecked;
            lvResistances.Items.Add(lvi);
        }

        public void AddResistance(MM2Resistances resist, bool bChecked)
        {
            ListViewItem lvi = new ListViewItem(MM2Monster.GetMonsterResistanceSingle(resist));
            lvi.Tag = resist;
            lvi.Checked = bChecked;
            lvResistances.Items.Add(lvi);
        }

        public void UpdateUI()
        {
            int i;
            int iTouchIndex, iPowerIndex;

            m_bUpdatingUI = true;

            lvCondition.Items.Clear();
            lvResistances.Items.Clear();
            comboTouch.Items.Clear();
            comboPower.Items.Clear();

            switch (m_monsterBasicInfo.Game)
            {
                case GameNames.MightAndMagic1:
                    nudExperience.Maximum = 65535;
                    nudDropItem.Maximum = 31;
                    nudDropGold.Maximum = 3;
                    nudLevel.Enabled = false;
                    nudMagicResist.Maximum = 127;
                    nudBravery.Maximum = 63;
                    nudCurrentHP.Maximum = 255;
                    nudMaxHP.Maximum = 255;
                    cbSummonFriends.Enabled = false;
                    cbBribeFood.Enabled = false;
                    cbBribeGems.Enabled = false;
                    cbBribeGold.Enabled = false;
                    MM1MonsterCombatStatus mm1Status = MM1Monster.GetCondition(m_monsterBasicInfo.Condition);
                    MM1Resistances mm1Res = MM1Monster.GetResistances(m_monsterBasicInfo.Resistances);
                    for (i = 1; i < 0x100; i <<= 1)
                    {
                        AddCondition((MM1MonsterCombatStatus)i, ((int) mm1Status & i) > 0);
                        AddResistance((MM1Resistances)i, ((int) mm1Res & i) > 0);
                    }
                    for (i = 0; i < 26; i++)
                    {
                        comboTouch.Items.Add(MM1Monster.GetMonsterTouchString((MM1MonsterTouch)i, false, false));
                    }
                    for (i = 0; i < 33; i++)
                    {
                        comboPower.Items.Add(MM1Monster.GetMonsterPowerString((MM1MonsterPower)i, 255));
                    }
                    iTouchIndex = (int) (m_monsterBasicInfo.MM1Touch & MM1MonsterTouch.AllEffects);
                    comboTouch.SelectedIndex = iTouchIndex < comboTouch.Items.Count ? iTouchIndex : 0;
                    iPowerIndex = (int) (m_monsterBasicInfo.MM1Power & MM1MonsterPower.AllPowers);
                    comboPower.SelectedIndex = ((int)m_monsterBasicInfo.MM1Power < comboPower.Items.Count ? (int)m_monsterBasicInfo.MM1Power : 0);

                    labelMM2Note.Visible = false;
                    labelIndex.Visible = false;
                    comboIndex.Visible = false;
                    cbTouchDisabled.Text = "Disabled";
                    splitContainer1.Panel1Collapsed = true;
                    break;
                case GameNames.MightAndMagic2:
                    nudBravery.Maximum = 3;
                    nudDropGold.Maximum = 3;
                    nudDropItem.Maximum = 3;
                    nudGroupSize.Maximum = 32;
                    nudMagicResist.Maximum = 7;
                    nudNumAttacks.Maximum = 16;
                    nudNumAttacks.Minimum = 0;
                    nudPowerChance.Maximum = 7;
                    nudFriendliness.Enabled = false;
                    nudCurrentHP.Maximum = 64000;
                    nudMaxHP.Maximum = 64000;
                    MM2MonsterCombatStatus mm2Status = MM2Monster.GetCondition(m_monsterBasicInfo.Condition);
                    MM2Resistances mm2Res = MM2Monster.GetResistances(m_monsterBasicInfo.Resistances);
                    for (i = 1; i < 0x100; i <<= 1)
                    {
                        AddCondition((MM2MonsterCombatStatus)i, ((int) mm2Status & i) > 0);
                        AddResistance((MM2Resistances)i, ((int) mm2Res & i) > 0);
                    }
                    AddResistance((MM2Resistances)0x100, ((int) mm2Res & 0x100) > 0);
                    for (i = 0; i < 32; i++)
                    {
                        comboTouch.Items.Add(MM2Monster.GetMonsterTouchString((MM2MonsterTouch)i));
                        comboPower.Items.Add(MM2Monster.GetMonsterPowerString((MM2MonsterPower)i, true));
                    }
                    iTouchIndex = (int) (m_monsterBasicInfo.MM2Touch & MM2MonsterTouch.AllTouches);
                    comboTouch.SelectedIndex = iTouchIndex < comboTouch.Items.Count ? iTouchIndex : 0;
                    iPowerIndex = (int)(m_monsterBasicInfo.MM2Power & MM2MonsterPower.AllPowers);
                    comboPower.SelectedIndex = iPowerIndex < comboPower.Items.Count ? iPowerIndex : 0;
                    labelMM2Note.Visible = true;
                    labelIndex.Visible = true;
                    comboIndex.Visible = true;

                    if (!m_bUpdatingIndex)
                    {
                        comboIndex.Items.Clear();
                        for (i = 0; i < MM2.Monsters.Count; i++)
                            comboIndex.Items.Add(String.Format("{0}: {1}", i, MM2.Monsters[i].GetLongDescription()));
                        if (m_monsterBasicInfo.Index < comboIndex.Items.Count)
                            comboIndex.SelectedIndex = m_monsterBasicInfo.Index;
                    }
                    cbTouchDisabled.Text = "Rare";
                    splitContainer1.Panel1Collapsed = false;
                    break;
                default:
                    break;
            }
            tbName.Text = m_monsterBasicInfo.Name;
            Global.SetNud(nudCurrentHP, m_monsterBasicInfo.CurrentHP);
            Global.SetNud(nudMaxHP, m_monsterBasicInfo.MaxHP);
            Global.SetNud(nudArmorClass, m_monsterBasicInfo.AC);
            Global.SetNud(nudSpeed, m_monsterBasicInfo.Speed);
            Global.SetNud(nudNumAttacks, m_monsterBasicInfo.NumAttacks);
            Global.SetNud(nudDamage, m_monsterBasicInfo.Damage);
            Global.SetNud(nudMagicResist, m_monsterBasicInfo.MagicResistance);
            Global.SetNud(nudGroupSize, m_monsterBasicInfo.GroupSize);
            Global.SetNud(nudBravery, m_monsterBasicInfo.Bravery);
            Global.SetNud(nudExperience, m_monsterBasicInfo.Experience);
            Global.SetNud(nudDropItem, m_monsterBasicInfo.DropItem);
            Global.SetNud(nudDropGold, m_monsterBasicInfo.DropGold);
            Global.SetNud(nudImageIndex, m_monsterBasicInfo.ImageIndex);
            Global.SetNud(nudLevel, m_monsterBasicInfo.Level);
            Global.SetNud(nudFriendliness, m_monsterBasicInfo.Friendliness); 
            cbUndead.Checked = m_monsterBasicInfo.Undead;
            cbAdvance.Checked = m_monsterBasicInfo.Advance;
            cbRegen.Checked = m_monsterBasicInfo.Regenerate;
            cbMissile.Checked = m_monsterBasicInfo.Missile;
            cbSummonFriends.Checked = m_monsterBasicInfo.SummonFriends;
            cbBribeFood.Checked = m_monsterBasicInfo.BribeFood;
            cbBribeGems.Checked = m_monsterBasicInfo.BribeGems;
            cbBribeGold.Checked = m_monsterBasicInfo.BribeGold;
            nudPowerChance.Value = m_monsterBasicInfo.PowerChance;
            cbDropGems.Checked = m_monsterBasicInfo.DropGems;
            cbTouchDisabled.Checked = m_monsterBasicInfo.TouchDisabled || m_monsterBasicInfo.RareTouch;

            m_bUpdatingUI = false;
        }

        public void UpdateFromUI()
        {
            int i;
            int iTemp;

            switch (m_monsterBasicInfo.Game)
            {
                case GameNames.MightAndMagic1:
                    m_monsterBasicInfo.MM1Touch = (MM1MonsterTouch)comboTouch.SelectedIndex;
                    m_monsterBasicInfo.MM1Power = (MM1MonsterPower)comboPower.SelectedIndex;

                    iTemp = 0;
                    for(i = 0; i < lvCondition.Items.Count; i++)
                        if (lvCondition.Items[i].Checked)
                            iTemp |= (1 << i);
                    m_monsterBasicInfo.Condition = MM1Monster.GetCondition((MM1MonsterCombatStatus)iTemp);

                    iTemp = 0;
                    for(i = 0; i < lvResistances.Items.Count; i++)
                        if (lvResistances.Items[i].Checked)
                            iTemp |= (1 << i);
                    m_monsterBasicInfo.Resistances = MM1Monster.GetResistances((MM1Resistances)iTemp);
                    m_monsterBasicInfo.Index = 0;
                    break;
                case GameNames.MightAndMagic2:
                    m_monsterBasicInfo.MM2Touch = (MM2MonsterTouch)comboTouch.SelectedIndex;
                    m_monsterBasicInfo.MM2Power = (MM2MonsterPower)comboPower.SelectedIndex;

                    iTemp = 0;
                    for (i = 0; i < lvCondition.Items.Count; i++)
                        if (lvCondition.Items[i].Checked)
                            iTemp |= (1 << i);
                    m_monsterBasicInfo.Condition = MM2Monster.GetCondition((MM2MonsterCombatStatus)iTemp);

                    iTemp = 0;
                    for (i = 0; i < lvResistances.Items.Count; i++)
                        if (lvResistances.Items[i].Checked)
                            iTemp |= (1 << i);
                    m_monsterBasicInfo.Resistances = MM2Monster.GetResistances((MM2Resistances)iTemp);
                    m_monsterBasicInfo.Index = comboIndex.SelectedIndex;
                    break;
                default:
                    break;
            }
            m_monsterBasicInfo.Name = tbName.Text;
            m_monsterBasicInfo.CurrentHP = (int) nudCurrentHP.Value;
            m_monsterBasicInfo.MaxHP = (int) nudMaxHP.Value;
            m_monsterBasicInfo.AC = (int) nudArmorClass.Value;
            m_monsterBasicInfo.Speed = (int) nudSpeed.Value;
            m_monsterBasicInfo.NumAttacks = (int) nudNumAttacks.Value;
            m_monsterBasicInfo.Damage = (int) nudDamage.Value;
            m_monsterBasicInfo.MagicResistance = (int) nudMagicResist.Value;
            m_monsterBasicInfo.GroupSize = (int) nudGroupSize.Value;
            m_monsterBasicInfo.Bravery = (int) nudBravery.Value;
            m_monsterBasicInfo.Experience = (int) nudExperience.Value;
            m_monsterBasicInfo.DropItem = (int) nudDropItem.Value;
            m_monsterBasicInfo.DropGold = (int) nudDropGold.Value;
            m_monsterBasicInfo.ImageIndex = (int) nudImageIndex.Value;
            m_monsterBasicInfo.Level = (int) nudLevel.Value;
            m_monsterBasicInfo.Friendliness = (int) nudFriendliness.Value;
            m_monsterBasicInfo.Undead = cbUndead.Checked;
            m_monsterBasicInfo.Advance = cbAdvance.Checked;
            m_monsterBasicInfo.Regenerate = cbRegen.Checked;
            m_monsterBasicInfo.Missile = cbMissile.Checked;
            m_monsterBasicInfo.SummonFriends = cbSummonFriends.Checked;
            m_monsterBasicInfo.BribeFood = cbBribeFood.Checked;
            m_monsterBasicInfo.BribeGems = cbBribeGems.Checked;
            m_monsterBasicInfo.BribeGold = cbBribeGold.Checked;
            m_monsterBasicInfo.PowerChance = (int) nudPowerChance.Value;
            m_monsterBasicInfo.DropGems = cbDropGems.Checked;
            m_monsterBasicInfo.TouchDisabled = cbTouchDisabled.Checked;
            m_monsterBasicInfo.RareTouch = cbTouchDisabled.Checked;
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

        private void lvCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvCondition.SelectedItems.Clear();
        }

        private void lvResistances_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvResistances.SelectedItems.Clear();
        }

        private void comboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingUI)
                return;

            if (m_main.Hacker != null)
            {
                MonsterBasicInfo monster = m_main.Hacker.GetMonsterInfo(comboIndex.SelectedIndex);
                if (monster != null)
                {
                    m_bUpdatingIndex = true;
                    m_monsterBasicInfo = monster;
                    UpdateUI();
                    m_bUpdatingIndex = false;
                }
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
    }

    public class MonsterBasicInfo
    {
        public GameNames Game;
        public int Index;
        public string Name;
        public int AC;
        public int MaxHP;
        public int CurrentHP;
        public BasicConditionFlags Condition;
        public int Speed;
        public int NumAttacks;
        public int Damage;
        public MM1MonsterTouch MM1Touch;
        public MM2MonsterTouch MM2Touch;
        public MM1MonsterPower MM1Power;
        public MM2MonsterPower MM2Power;
        public int PowerChance;
        public bool RareTouch;
        public GenericResistanceFlags Resistances;
        public int MagicResistance;
        public int GroupSize;
        public int Bravery;
        public bool TouchDisabled;
        public bool Undead;
        public bool Regenerate;
        public bool Missile;
        public bool Advance;
        public bool SummonFriends;
        public bool BribeFood;
        public bool BribeGold;
        public bool BribeGems;
        public long Experience;
        public int DropItem;
        public int DropGold;
        public bool DropGems;
        public int ImageIndex;
        public int Level;
        public int Friendliness;

        public MonsterBasicInfo(GameNames game, Monster monster, EncounterInfo info)
        {
            Game = game;

            Index = monster.Index;
            Name = monster.Name;
            AC = monster.AC;
            MaxHP = monster.HP;
            CurrentHP = monster.CurrentHP;
            Condition = monster.Condition;
            Speed = monster.Speed;
            NumAttacks = monster.NumAttacks;
            Damage = monster.Damage;
            Resistances = monster.GenericResistances;
            MagicResistance = monster.MagicResist;
            GroupSize = monster.GroupSize;
            Bravery = monster.Bravery;
            Undead = monster.Undead;
            Regenerate = monster.Regenerate;
            Missile = monster.Missile;
            Advance = monster.Advance;
            SummonFriends = false;
            MM1Touch = MM1MonsterTouch.None;
            MM2Touch = MM2MonsterTouch.None;
            MM1Power = MM1MonsterPower.None;
            MM2Power = (MM2MonsterPower) 0;
            TouchDisabled = false;
            PowerChance = 0;
            RareTouch = true;
            BribeFood = false;
            BribeGold = false;
            BribeGems = false;
            DropGold = 0;
            DropItem = 0;
            DropGems = false;
            Experience = monster.Experience;
            ImageIndex = monster.ImageIndex;
            Level = 0;
            Friendliness = 0;

            if (info == null || info.PreEncounter)
                Condition = BasicConditionFlags.Good;

            if (monster is MM1Monster)
            {
                MM1Monster mm1Monster = monster as MM1Monster;
                MM1Touch = mm1Monster.Touch;
                RareTouch = (MM1Touch == MM1MonsterTouch.CausesParalysis1 ||
                    MM1Touch == MM1MonsterTouch.DrainsLifeforce1 ||
                    MM1Touch == MM1MonsterTouch.InducesPoison1 ||
                    MM1Touch == MM1MonsterTouch.InflictsDisease1);
                MM1Power = mm1Monster.SpecialPower;
                PowerChance = mm1Monster.PowerChance;
                DropGold = (int) (mm1Monster.Treasure & MM1TreasureFlags.AnyGold) >> 1;
                DropItem = (int) (mm1Monster.Treasure & MM1TreasureFlags.AnyItem) >> 3;
                DropGems = (int)(mm1Monster.Treasure & MM1TreasureFlags.Gems) > 0;
                Friendliness = mm1Monster.Friendliness;
                TouchDisabled = mm1Monster.TouchDisabled;
            }
            else if (monster is MM2Monster)
            {
                MM2Monster mm2Monster = monster as MM2Monster;
                MM2Touch = mm2Monster.Touch;
                RareTouch = MM2Touch.HasFlag(MM2MonsterTouch.RareEffect);
                MM2Power = mm2Monster.SpecialPower;
                PowerChance = mm2Monster.PowerChance;
                SummonFriends = mm2Monster.SummonFriends;
                BribeFood = mm2Monster.BribeFood;
                BribeGold = mm2Monster.BribeGold;
                BribeGems = mm2Monster.BribeGems;
                DropGold = mm2Monster.DropGold;
                DropItem = mm2Monster.DropItem;
                DropGems = mm2Monster.DropGems;
                Level = mm2Monster.Level;
            }
        }

        public byte[] GetBytes()
        {
            byte[] bytes = null;
            byte[] bytesName;
            ASCIIEncoding enc = new ASCIIEncoding();

            switch(Game)
            {
                case GameNames.MightAndMagic1:
                    bytes = new byte[32];
                    bytesName = enc.GetBytes(String.Format("{0,-15}", Name));
                    Buffer.BlockCopy(bytesName, 0, bytes, 0, 15);
                    bytes[15] = (byte)GroupSize;
                    bytes[16] = (byte)Friendliness;
                    bytes[17] = (byte)MaxHP;
                    bytes[18] = (byte)AC;
                    bytes[19] = (byte)Damage;
                    bytes[20] = (byte)NumAttacks;
                    bytes[21] = (byte)Speed;
                    Buffer.BlockCopy(BitConverter.GetBytes((UInt16)Experience), 0, bytes, 22, 2);
                    byte treasure = DropGems ? (byte) MM1TreasureFlags.Gems : (byte) 0;
                    treasure |= (byte)(DropItem << 3);
                    treasure |= (byte)(DropGold << 1);
                    bytes[24] = treasure;
                    bytes[25] = (byte)MagicResistance;
                    bytes[25] |= Undead ? (byte) 0x80 : (byte) 0x00;
                    bytes[26] = (byte) MM1Monster.GetResistances(Resistances);
                    bytes[27] = (byte) ((int) MM1Touch | (TouchDisabled ? (int) MM1MonsterTouch.Disabled : 0));
                    bytes[28] = (byte)MM1Power;
                    bytes[29] = (byte)PowerChance;
                    bytes[30] = (byte)Bravery;
                    bytes[30] |= (Advance ? (byte)MM1BraveryFlags.Advance : (byte)0);
                    bytes[30] |= (Regenerate ? (byte)MM1BraveryFlags.Regenerate : (byte)0);
                    bytes[31] = (byte)ImageIndex;
                    break;
                case GameNames.MightAndMagic2:
                    bytes = new byte[26];
                    bytesName = enc.GetBytes(String.Format("{0,-14}", Name));
                    for (int i = 0; i < 14; i++)
                        bytesName[i] |= 0x80;   // no idea why
                    Buffer.BlockCopy(bytesName, 0, bytes, 0, 14);
                    bytes[14] = MM2Monster.HPToByte(MaxHP);
                    bytes[15] = MM2Monster.ExpToByte(Experience);
                    bytes[16] = (byte)DropItem;
                    bytes[16] |= DropGems ? (byte)MM2TreasureByte.Gems : (byte)0;
                    bytes[16] |= (byte)(DropGold << 3);
                    bytes[16] |= BribeFood ? (byte)MM2TreasureByte.BribeFood : (byte)0;
                    bytes[16] |= BribeGold ? (byte)MM2TreasureByte.BribeMoney : (byte)0;
                    bytes[16] |= BribeGems ? (byte)MM2TreasureByte.BribeGems : (byte)0;
                    bytes[17] = (byte) MM2Power;
                    bytes[18] = (byte) MM2Touch;
                    bytes[18] |= Missile ? (byte)MM2MonsterTouch.Missile : (byte)0;
                    bytes[18] |= Undead ? (byte)MM2MonsterTouch.Undead : (byte)0;
                    bytes[19] = (byte)(GroupSize - 1);
                    bytes[19] |= (byte)(Bravery << 5);
                    bytes[19] |= SummonFriends ? (byte)MM2BraveryFlags.AddFriends : (byte)0;
                    bytes[20] = (byte)(NumAttacks - 1);
                    bytes[20] |= (byte)((Level - 1) << 4);
                    bytes[21] = (byte) ImageIndex;
                    bytes[22] = (byte) MM2Monster.ByteToSixBit(AC);
                    bytes[22] |= Advance ? (byte)MM2ACByte.Advance : (byte)0;
                    bytes[22] |= Regenerate ? (byte)MM2ACByte.Regenerate : (byte)0;
                    bytes[23] = (byte) MM2Monster.ByteToSixBit(Damage);
                    bytes[23] |= Resistances.HasFlag(GenericResistanceFlags.Electricity) ? (byte)MM2DamageByte.ElecResist : (byte)0;
                    bytes[23] |= Resistances.HasFlag(GenericResistanceFlags.Fire) ? (byte)MM2DamageByte.FireResist : (byte)0;
                    bytes[24] = (byte) MM2Monster.ByteToSixBit(Speed);
                    bytes[24] |= Resistances.HasFlag(GenericResistanceFlags.Acid) ? (byte)MM2SpeedByte.AcidResist : (byte)0;
                    bytes[24] |= Resistances.HasFlag(GenericResistanceFlags.Cold) ? (byte)MM2SpeedByte.ColdResist : (byte)0;
                    bytes[25] = (byte) (MagicResistance << 5);
                    bytes[25] |= Resistances.HasFlag(GenericResistanceFlags.Sleep) ? (byte)MM2MagicResistByte.SleepResist: (byte)0;
                    bytes[25] |= Resistances.HasFlag(GenericResistanceFlags.Paralyze) ? (byte)MM2MagicResistByte.ParalyzeResist: (byte)0;
                    bytes[25] |= Resistances.HasFlag(GenericResistanceFlags.Male) ? (byte)MM2MagicResistByte.MaleResist: (byte)0;
                    bytes[25] |= Resistances.HasFlag(GenericResistanceFlags.Female) ? (byte)MM2MagicResistByte.FemaleResist: (byte)0;
                    bytes[25] |= Resistances.HasFlag(GenericResistanceFlags.Weapons) ? (byte)MM2MagicResistByte.WeaponResist: (byte)0;
                    break;
                default:
                    break;
            }
            return bytes;
        }
    }

    [Flags]
    public enum GenericResistanceFlags
    {
        None = 0x00000000,
        Fire = 0x00000001,
        Electricity = 0x00000002,
        Acid = 0x00000004,
        Cold = 0x00000008,
        Paralyze = 0x00000010,
        Sleep = 0x00000020,
        Male = 0x00000040,
        Female = 0x00000080,
        Weapons = 0x00000100,
        Poison = 0x00000200,
        Fear = 0x00000400,
        Energy = 0x00000800,
        Mental = 0x00001000,
        Magic = 0x00002000,
        Blessed = 0x00010000,
        PowerShield = 0x00020000,
        HolyBonus = 0x00040000,
        Heroism = 0x00080000,
        SaveVsPoison = 0x00100000,
        SaveVsPetrify = 0x00200000,
        SaveVsWand = 0x00400000,
        SaveVsBreath = 0x00800000,
        SaveVsSpell= 0x01000000,
        SaveVsSleep = 0x02000000,
        SaveVsParalyze= 0x04000000
    }
}
