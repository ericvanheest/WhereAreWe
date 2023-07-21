using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOB2MonsterIndex
    {
        None = -1,
        Wolf1 = 0,
        Wolf2 = 1,
        DireWolf = 2,
        Monster3 = 3,
        Monster4 = 4,
        Monster6 = 6,
        Monster7 = 7,
        Monster8 = 8,
        Monster9 = 9,
        Monster10 = 10,
        Monster11 = 11,
        Monster12 = 12,
        Monster13 = 13,
        Monster14 = 14,
        Monster15 = 15,
        Monster16 = 16,
        Monster17 = 17,
        Monster18 = 18,
        Monster19 = 19,
        Monster20 = 20,
        Monster21 = 21,
        Last
    }

    public class EOB2Monster : EOBMonster
    {
        public EOB2Monster()
        {
        }

        public override Monster Clone()
        {
            EOB2Monster monster = CreateBase(Index, RawBytes);
            SetSpecific(monster, null, SpecificBytes);
            monster.PocketItem = PocketItem == null ? null : PocketItem.Clone() as EOBItem;
            monster.Weapon = Weapon == null ? null : Weapon.Clone() as EOBItem;
            return monster;
        }

        public static EOB2Monster CreateSpecific(byte[] bytes, byte[] bytesItemTable, int offset = 0)
        {
            EOB2Monster monster = EOB2.CloneMonster(bytes[offset]);
            SetSpecific(monster, bytesItemTable, bytes, offset);
            return monster;
        }

        public override string DamageString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Attack1 != null && Attack1.Quantity > 0)
                    sb.AppendFormat("{0}, ", Attack1.ToString());
                if (Attack2 != null && Attack2.Quantity > 0)
                    sb.AppendFormat("{0}, ", Attack2.ToString());
                if (Attack3 != null && Attack3.Quantity > 0)
                    sb.AppendFormat("{0}, ", Attack3.ToString());
                if (sb.Length < 1)
                    return "N/A";
                return Global.Trim(sb).ToString();
            }
        }

        public override string TreasureStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Items != null && Weapon != null && Items[0] != 0)
                    sb.AppendFormat("{0}, ", Weapon.Name);
                if (Items != null && PocketItem != null && Items[1] != 0)
                    sb.AppendFormat("{0}, ", PocketItem.Name);
                return Global.Trim(sb).ToString();
            }
        }

        public static void SetSpecific(EOB2Monster monster, byte[] bytesItemTable, byte[] bytes, int offset = 0)
        {
            if (monster == null || bytes == null)
                return;
            monster.SpecificBytes = Global.Subset(bytes, offset, 30);
            monster.Position = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, offset + 2));
            monster.HP = BitConverter.ToInt16(bytes, offset + 12);
            monster.CurrentHP = BitConverter.ToInt16(bytes, offset + 14);
            monster.PocketItem = null;
            monster.Weapon = null;
            monster.Items = new int[2];
            monster.Items[0] = BitConverter.ToInt16(bytes, offset + 18);
            monster.Items[1] = BitConverter.ToInt16(bytes, offset + 20);
            if (bytesItemTable != null && monster.Items[0] != 0)
                monster.Weapon = EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder2, BitConverter.GetBytes((short)monster.Items[0]), bytesItemTable, 0);
            if (bytesItemTable != null && monster.Items[1] != 0)
                monster.PocketItem = EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder2, BitConverter.GetBytes((short)monster.Items[1]), bytesItemTable, 0);
            monster.Speed = bytes[offset + 8];
        }

        public static EOB2Monster CreateBase(int iIndex, byte[] bytes, int offset = 0)
        {
            EOB2Monster monster = new EOB2Monster();
            byte[] raw = Global.Subset(bytes, offset, EOB2MonsterList.MonsterLengthBytes);
            monster.Index = iIndex;
            monster.RawBytes = raw;
            monster.Name = GetName((EOB2MonsterIndex)iIndex);
            monster.AC = (sbyte)raw[4];
            monster.HitDice = raw[6];
            monster.NumAttacks = raw[7];
            monster.Attack1 = new DamageDice(raw[12], raw[11], raw[13]);
            monster.Attack2 = new DamageDice(raw[15], raw[14], raw[16]);
            monster.Attack3 = new DamageDice(raw[18], raw[17], raw[19]);
            monster.Special = (EOBMonsterSpecial)raw[17];
            monster.Experience = BitConverter.ToUInt16(raw, 30);
            monster.MonsterSize = (EOBMonsterSize)raw[34];
            monster.UndeadPower = (sbyte)raw[45];
            monster.Undead = monster.UndeadPower > 0;
            return monster;
        }

        public override void SetBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length < EOB2MonsterList.MonsterLengthBytes)
                return;
            bytes[4] = (byte) AC;
            bytes[6] = (byte) HitDice;
            bytes[7] = (byte) NumAttacks;
            bytes[11] = (byte) Attack1.Quantity;
            bytes[12] = (byte) Attack1.Faces;
            bytes[13] = (byte) Attack1.Bonus;
            bytes[14] = (byte) Attack2.Quantity;
            bytes[15] = (byte) Attack2.Faces;
            bytes[16] = (byte) Attack2.Bonus;
            bytes[17] = (byte) Attack3.Quantity;
            bytes[18] = (byte) Attack3.Faces;
            bytes[19] = (byte) Attack3.Bonus;
            bytes[29] = (byte) Special;
            Global.SetInt16(bytes, 30, (Int16) Experience);
            bytes[34] = (byte) GroupSize;
            bytes[45] = (byte) UndeadPower;

            foreach (int i in new int[] { 0, 1, 2, 3, 5, 8, 9, 10, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 32, 33, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 })
                bytes[i] = RawBytes[i];
        }

        public static string GetName(EOB2MonsterIndex index)
        {
            switch (index)
            {
                case EOB2MonsterIndex.Wolf1: return "Wolf-1";
                case EOB2MonsterIndex.Wolf2: return "Wolf-2";
                case EOB2MonsterIndex.DireWolf: return "Dire Wolf";

                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class EOB2MonsterList : EOB123MonsterList
    {
        public const int MonsterLengthBytes = 46;

        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.EOB2_Monster_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker) { return hacker.ReadOffset(EOB2.Memory.MonsterList, MonsterLength * 22).Bytes; }
        public override bool InitInternalList()
        {
            m_monsters = SetFromBytes(GetInternalBytes());
            return true;
        }

        public EOB2MonsterList()
        {
            MonsterLength = MonsterLengthBytes;
        }

        public override List<EOBMonster> SetFromBytes(byte[] bytes)
        {
            List<EOBMonster> monsters = new List<EOBMonster>();

            try
            {
                int iIndex = 0;
                int iCount = 0;
                while (iIndex <= bytes.Length - MonsterLength)
                {
                    EOBMonster monster = EOB2Monster.CreateBase(iCount, bytes, iIndex);
                    monsters.Add(monster);
                    iCount++;
                    iIndex += MonsterLength;
                }

                m_bValid = true;
            }
            catch (Exception)
            {
                m_bValid = false;
            }

            return monsters;
        }
    }
}
