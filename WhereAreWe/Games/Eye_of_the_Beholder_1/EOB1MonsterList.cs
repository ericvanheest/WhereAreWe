using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOB1MonsterIndex
    {
        None = -1,
        Kobold = 0,
        GiantLeech = 1,
        Skeleton = 2,
        Zombie = 3,
        KuoToa = 4,
        Flind = 5,
        GiantSpider = 6,
        Dwarf = 7,
        SkeletonWarrior = 8,
        DisplacerBeast = 9,
        MantisWarrior = 10,
        Shindia = 11,
        Drow = 12,
        Golem = 13,
        DarkRobedFigure = 14,
        Drider = 15,
        Kenku = 16,
        MindFlayer = 17,
        RustMonster = 18,
        Xorn = 19,
        HellHound = 20,
        Beholder = 21,

        Last
    }

    public class EOB1Monster : EOBMonster
    {
        public EOB1Monster()
        {
        }

        public override Monster Clone()
        {
            EOB1Monster monster = CreateBase(Index, RawBytes);
            SetSpecific(monster, null, SpecificBytes);
            monster.PocketItem = PocketItem == null ? null : PocketItem.Clone() as EOBItem;
            monster.Weapon = Weapon == null ? null : Weapon.Clone() as EOBItem;
            return monster;
        }

        public static EOB1Monster CreateSpecific(byte[] bytes, byte[] bytesItemTable, int offset = 0)
        {
            EOB1Monster monster = EOB1.CloneMonster(bytes[offset]);
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

        public static void SetSpecific(EOB1Monster monster, byte[] bytesItemTable, byte[] bytes, int offset = 0)
        {
            if (monster == null || bytes == null)
                return;
            monster.SpecificBytes = Global.Subset(bytes, offset, 28);
            monster.Position = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, offset + 2));
            monster.HP = BitConverter.ToInt16(bytes, offset + 12);
            monster.CurrentHP = BitConverter.ToInt16(bytes, offset + 14);
            monster.PocketItem = null;
            monster.Weapon = null;
            monster.Items = new int[2];
            monster.Items[0] = BitConverter.ToInt16(bytes, offset + 18);
            monster.Items[1] = BitConverter.ToInt16(bytes, offset + 20);
            if (bytesItemTable != null && monster.Items[0] != 0)
                monster.Weapon = EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder1, BitConverter.GetBytes((short)monster.Items[0]), bytesItemTable, 0);
            if (bytesItemTable != null && monster.Items[1] != 0)
                monster.PocketItem = EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder1, BitConverter.GetBytes((short)monster.Items[1]), bytesItemTable, 0);
            monster.Speed = bytes[offset + 8];
        }

        public static EOB1Monster CreateBase(int iIndex, byte[] bytes, int offset = 0)
        {
            EOB1Monster monster = new EOB1Monster();
            byte[] raw = Global.Subset(bytes, offset, 27);
            monster.Index = iIndex;
            monster.RawBytes = raw;
            monster.Name = GetName((EOB1MonsterIndex)iIndex);
            monster.AC = (sbyte)raw[0];
            monster.HitDice = raw[2];
            monster.NumAttacks = raw[3];
            monster.Attack1 = new DamageDice(raw[5], raw[4], raw[6]);
            monster.Attack2 = new DamageDice(raw[8], raw[7], raw[9]);
            monster.Attack3 = new DamageDice(raw[11], raw[10], raw[12]);
            monster.Special = (EOBMonsterSpecial)raw[14];
            monster.Experience = BitConverter.ToUInt16(raw, 19);
            monster.MonsterSize = (EOBMonsterSize)raw[21];
            monster.UndeadPower = (sbyte)raw[25];
            monster.Undead = monster.UndeadPower > 0;
            return monster;
        }

        public override void SetBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 27)
                return;
            bytes[0] = (byte) AC;
            bytes[2] = (byte) HitDice;
            bytes[3] = (byte) NumAttacks;
            bytes[4] = (byte) Attack1.Quantity;
            bytes[5] = (byte) Attack1.Faces;
            bytes[6] = (byte) Attack1.Bonus;
            bytes[7] = (byte) Attack2.Quantity;
            bytes[8] = (byte) Attack2.Faces;
            bytes[9] = (byte) Attack2.Bonus;
            bytes[10] = (byte) Attack3.Quantity;
            bytes[11] = (byte) Attack3.Faces;
            bytes[12] = (byte) Attack3.Bonus;
            bytes[14] = (byte) Special;
            Global.SetInt16(bytes, 19, (Int16) Experience);
            bytes[21] = (byte) GroupSize;
            bytes[25] = (byte) UndeadPower;

            foreach (int i in new int[] { 1, 13, 15, 22, 23, 24, 26 })
                bytes[i] = RawBytes[i];
        }

        public static string GetName(EOB1MonsterIndex index)
        {
            switch (index)
            {
                case EOB1MonsterIndex.Kobold: return "Kobold";
                case EOB1MonsterIndex.GiantLeech: return "Giant Leech";
                case EOB1MonsterIndex.Zombie: return "Zombie";
                case EOB1MonsterIndex.Skeleton: return "Skeleton";
                case EOB1MonsterIndex.KuoToa: return "Kuo-Toa";
                case EOB1MonsterIndex.Flind: return "Flind";
                case EOB1MonsterIndex.GiantSpider: return "Giant Spider";
                case EOB1MonsterIndex.Dwarf: return "Dwarf";
                case EOB1MonsterIndex.SkeletonWarrior: return "Skeleton Warrior";
                case EOB1MonsterIndex.DisplacerBeast: return "Displacer Beast";
                case EOB1MonsterIndex.MantisWarrior: return "Mantis Warrior";
                case EOB1MonsterIndex.Drow: return "Drow";
                case EOB1MonsterIndex.Golem: return "Golem";
                case EOB1MonsterIndex.DarkRobedFigure: return "Dark-Robed Figure";
                case EOB1MonsterIndex.Kenku: return "Kenku";
                case EOB1MonsterIndex.Drider: return "Drider";
                case EOB1MonsterIndex.MindFlayer: return "Mind Flayer";
                case EOB1MonsterIndex.RustMonster: return "Rust Monster";
                case EOB1MonsterIndex.Xorn: return "Xorn";
                case EOB1MonsterIndex.HellHound: return "HellHound";
                case EOB1MonsterIndex.Shindia: return "Shindia";
                case EOB1MonsterIndex.Beholder: return "Beholder";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class EOB1MonsterList : EOB123MonsterList
    {
        public const int MonsterLengthBytes = 27;

        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.EOB1_Monster_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker) { return hacker.ReadOffset(EOB1.Memory.MonsterList, MonsterLength * 22).Bytes; }
        public override bool InitInternalList()
        {
            m_monsters = SetFromBytes(GetInternalBytes());
            return true;
        }

        public EOB1MonsterList()
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
                    EOBMonster monster = EOB1Monster.CreateBase(iCount, bytes, iIndex);
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
