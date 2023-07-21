using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT3MonsterIndex
    {
        None = 0,
        Last
    }

    public enum BT3Pronoun
    {
        Him = 0,
        Her = 1,
        It = 2,
        Name = 3
    }

    [Flags]
    public enum BT3MonsterCategories
    {
        Dragon =        0x00000001,
        Mechanical =    0x00000002,  // No monster has only this bit set
        Warrior =       0x00000004,
        Fiend =         0x00000008,
        Death =         0x00000010,  // No monster has only this bit set
        Spellcaster =   0x00000020,  // Affects spells like "Mage Maelstrom"
        Fast =          0x00000040,
        Supernatural =  0x00000080,  // "Supernatural or evil" according to the Holy Water spell
        Unknown08 =     0x00000100,  // No monster has only this bit set
        Stalker =       0x00000200,
        Unknown10 =     0x00000400,
        Insane =        0x00000800,
        Poison =        0x00001000,  // No monster has only this bit set
        Dark =          0x00002000,
        Bone =          0x00004000,
        Ice =           0x00008000,
        Colored =       0x00010000,
        Unknown17 =     0x00020000,
        Magical =       0x00040000,
        Unknown19 =     0x00080000,
        Unknown20 =     0x00100000,  // No monster has only this bit set
        Unknown21 =     0x00200000,  // No monster has only this bit set
        Unknown22 =     0x00400000,  // No monster has only this bit set
        Unknown23 =     0x00800000
    }

    public class BT3MonsterAttack
    {
        public byte[] RawBytes;

        public BT3MonsterAttack(byte[] bytes)
        {
            RawBytes = bytes;
        }

        public BT3SpellIndex SpellIndex { get { return RawBytes[0] < 0x80 ? (BT3SpellIndex)RawBytes[0] + 1 : BT3SpellIndex.None; } }
        public bool Melee { get { return RawBytes[0] >= 0x80; } }
        public DamageDice Damage { get { return Melee ? BT3Monster.DamageFromByte(RawBytes[1]) : DamageDice.Zero; } }
        public int Level { get { return RawBytes[1]; } }

        public BT3Spell Spell
        {
            get
            {
                if (Melee)
                    return null;
                int iSpell = (int)SpellIndex;
                if (iSpell >= 0 && iSpell < BT3.Spells.Count)
                    return BT3.Spells[iSpell - 1];
                return null;
            }
        }

        public string SpellName { get { return Spell == null ? String.Format("<Unknown:{0}>", SpellIndex) : Spell.Name; } }
        public string SpellAbbr { get { return Spell == null ? String.Format("<U{0}>", SpellIndex) : Spell.Abbreviation; } }

        public string GetAttackString(bool bAbbrev)
        {
            if (!bAbbrev)
                return ToString();

            if (Melee)
                return Damage.ToString();

            return String.Format("{0}:{1}", SpellAbbr, Level);
        }

        public override string ToString()   
        {
            if (Melee)
                return String.Format("{0} physical", Damage.ToString());
            int iSpell = (int)SpellIndex;
            return String.Format("{0} (Level {1})", SpellName, Level);
        }

        public static BT3MonsterAttack[] GetAttacks(byte[] bytes, int offset, int length)
        {
            BT3MonsterAttack[] attacks = new BT3MonsterAttack[length / 2];
            for (int i = 0; i < length; i += 2)
                attacks[i / 2] = new BT3MonsterAttack(Global.Subset(bytes, offset + i, 2));
            return attacks;
        }
    }

    public class BT3TrapInfo : BTTrapInfo
    {
        public enum BT3Trap
        {
            PoisonNeedle  = 0,
            PoisonBlades  = 1,
            Blades        = 2,
            ShockWave     = 3,
            Crazycloud    = 4,
            Vortex        = 5,
            Shocks        = 6,
            PoisonDarts   = 7,
            AcidBurst     = 8,
            GasCloud      = 9,
            PoisonSpikes  = 10,
            MindBlast     = 11,
            MindJab       = 12,
            BasiliskSnare = 13,
            DeathBlades   = 14,
            CodgerBomb    = 15,
            Swindler      = 16,
            Hammer        = 17,
            Last
        }

        public static string GetBT3TrapName(BT3Trap trap)
        {
            switch (trap)
            {
                case BT3Trap.PoisonNeedle: return "Poison Needle";
                case BT3Trap.PoisonBlades: return "Poison Blades";
                case BT3Trap.Blades: return "Blades";
                case BT3Trap.ShockWave: return "Shock Wave";
                case BT3Trap.Crazycloud: return "Crazycloud";
                case BT3Trap.Vortex: return "Vortex";
                case BT3Trap.Shocks: return "Shocks";
                case BT3Trap.PoisonDarts: return "Poison Darts";
                case BT3Trap.AcidBurst: return "Acid Burst";
                case BT3Trap.GasCloud: return "Gas Cloud";
                case BT3Trap.PoisonSpikes: return "Poison Spikes";
                case BT3Trap.MindBlast: return "Mind Blast";
                case BT3Trap.MindJab: return "Mind Jab";
                case BT3Trap.BasiliskSnare: return "Basilisk Snare";
                case BT3Trap.DeathBlades: return "Death Blades";
                case BT3Trap.CodgerBomb: return "Codger Bomb";
                case BT3Trap.Swindler: return "Swindler";
                case BT3Trap.Hammer: return "Hammer";
                default: return "Unknown";
            }
        }

        public BT3TrapInfo(int trap)
        {
            Trap = trap;
        }

        public override string GetTrapName(int iTrap) { return GetBT3TrapName((BT3Trap)iTrap); }
        public override int TrapCount { get { return (int)BT3Trap.Last; } }
    }

    public class BT3Monster : BTMonster
    {
        public BT3MonsterAttack[] Attacks;
        public int InitialDistance;
        public int AdvancePerRound;
        public int TouchHigh;
        public int GroupHigh;
        public int AttackVerb;
        public int AttackHigh;
        public int MagicResistHalf;
        public int MagicResistFull;
        public int InitiativeMin;
        public int InitiativeMax;
        public bool Illusion;
        public bool NoRandom;
        public BT3MonsterCategories Categories;
        public BT3Pronoun Pronoun;
        public int Evade;
        public int Unknown1E;
        public int Unknown1F;
        public int Unknown26;
        public int ACIgnoreLow;
        public int ACIgnoreHigh;

        public override string GetName(int index) { return GetName((BT3MonsterIndex) index); }

        public override Monster Clone() 
        {
            return new BT3Monster(Index, GetBytes(), 0);
        }

        public static DamageDice DamageFromByte(byte b, int iBonus = 0)
        {
            return new DamageDice(2 << ((b & 0xE0) >> 5), (b & 0x1F) + 1, iBonus);
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT3Bytes(index, bytes, offset); }
        public override string GetAttackString(int attack, bool bAbbrev)
        {
            if (Attacks == null || Attacks.Length < attack)
                return base.GetAttackString(attack, bAbbrev);
            return Attacks[attack].GetAttackString(bAbbrev);
        }

        public void SetBT3Bytes(int index, byte[] bytes, int offset = 0)
        {
            if (bytes == null || bytes.Length < offset + 48)
                return;

            RawBytes = Global.Subset(bytes, offset, 48);
            FullName = BT3MemoryHacker.ExtractMonsterNames(bytes, offset);
            MonsterIndex = (BT3MonsterIndex)index;
            Name = FullName.Singular;
            NumAttacks = 1;

            HPDice = DamageFromByte(bytes[offset+16], BitConverter.ToUInt16(bytes, offset + 17));
            InitialDistance = bytes[offset + 19] & 0x0f;
            AdvancePerRound = (bytes[offset + 19] >> 4); 
            Pronoun = (BT3Pronoun)((bytes[offset + 20] & 0xC0) >> 6);
            GroupSize = 1 + (bytes[offset + 21] & 0x1F);
            Speed = (bytes[offset + 21] & 0xE0) >> 5;
            Attacks = BT3MonsterAttack.GetAttacks(bytes, offset + 22, 8);
            ImageIndex = bytes[offset + 32];
            Distance = InitialDistance;
            Experience = new UInt24(bytes, offset + 33);
            Gold = (int)(Experience / 5);
            AttackVerb = bytes[offset + 36] & 0x0f;
            Illusion = (bytes[offset + 36] & 0x10) > 0;
            NoRandom = (bytes[offset + 36] & 0x20) > 0;
            AttackHigh = bytes[offset + 36] & 0xC0;
            AC = 10 - bytes[offset + 37];
            InitiativeMin = bytes[offset + 39];
            InitiativeMax = bytes[offset + 40];
            Categories = (BT3MonsterCategories)((bytes[offset + 41] << 16) | (bytes[offset + 42] << 8) | bytes[offset + 43]);
            MagicResistFull = bytes[offset + 46];
            MagicResistHalf = bytes[offset + 47];
            MagicResist = (byte)((MagicResistHalf + MagicResistFull) / 2);
            Evade = bytes[offset + 0x14] & 0x3F;
            Unknown1E = bytes[offset + 0x1E];
            Unknown1F = bytes[offset + 0x1F];
            Unknown26 = bytes[offset + 0x26];
            ACIgnoreLow = 10 - bytes[offset + 0x2D];
            ACIgnoreHigh = 10 - bytes[offset + 0x2C];
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = Global.NullBytes(48);
            Buffer.BlockCopy(RawBytes, 0, bytes, 0, 48);

            bytes[16] = (byte) ((HPDice.Quantity - 1) | ((Global.NumRightZeros(HPDice.Faces) - 1) << 5));
            Global.SetUInt16(bytes, 17, HPDice.Bonus);
            bytes[19] = (byte)(((AdvancePerRound) << 4) | InitialDistance);
            bytes[20] = (byte)(((int)Pronoun << 6) | Evade);
            bytes[21] = (byte)((GroupSize - 1) | (Speed << 5));
            for (int i = 0; i < Attacks.Length; i++)
                Global.SetBytes(bytes, 22 + (i * 2), Attacks[i].RawBytes, 0);
            bytes[32] = (byte)ImageIndex;
            Global.SetUInt24(bytes, 33, (int)Experience);
            bytes[36] = (byte)(AttackVerb | (Illusion ? 0x10 : 0x00) | (NoRandom ? 0x20 : 0x00) | AttackHigh);
            bytes[37] = (byte)(10 - AC);
            bytes[39] = (byte)(InitiativeMin);
            bytes[40] = (byte)(InitiativeMax);
            bytes[41] = (byte)((int)Categories >> 16);
            bytes[42] = (byte)(((int)Categories >> 8) & 0xff);
            bytes[43] = (byte)((int)Categories & 0xff);
            bytes[46] = (byte)MagicResistFull;
            bytes[47] = (byte)MagicResistHalf;
            bytes[0x1E] = (byte)Unknown1E;
            bytes[0x1F] = (byte)Unknown1F;
            bytes[0x26] = (byte)Unknown26;
            bytes[0x2D] = (byte)(10 - ACIgnoreLow);
            bytes[0x2C] = (byte)(10 - ACIgnoreHigh);
            return bytes;
        }

        public void SetEncodedName(string strName)
        {
            byte[] bytes = Global.GetHighAsciiBytes(strName, 16, 0xFF);
            Global.SetBytes(RawBytes, 0, bytes, 0);
            FullName = BT3MemoryHacker.ExtractMonsterNames(bytes, 0);
            Name = FullName.Singular;
        }

        public string GetEncodedName()
        {
            return Global.GetLowAsciiString(RawBytes, 0, 15);
        }

        public BT3MonsterIndex MonsterIndex { get { return (BT3MonsterIndex)Index; } set { Index = (int)value; } }

        public static string GetName(BT3MonsterIndex index)
        {
            switch (index)
            {
                case BT3MonsterIndex.None: return "<None>";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public BT3MonsterIndex BTIndex { get { return (BT3MonsterIndex)Index; } set { Index = (int)value; } }

        public BT3Monster(int iIndex, byte[] bytes, int offset)
        {
            SetBT3Bytes(iIndex, bytes, offset);
        }

        public override string DamageString
        {
            get
            {
                if (Attacks == null || Attacks.Length < 1)
                    return "0";
                Dictionary<DamageDice, int> physical = new Dictionary<DamageDice, int>(Attacks.Length);
                Dictionary<string, int> spells = new Dictionary<string, int>(Attacks.Length);
                foreach (BT3MonsterAttack attack in Attacks)
                {
                    if (attack.Melee)
                    {
                        if (!physical.ContainsKey(attack.Damage))
                            physical.Add(attack.Damage, 0);
                        physical[attack.Damage]++;
                    }
                    else
                    {
                        string strSpell = String.Format("{0}:{1}", attack.SpellAbbr, attack.Level);
                        if (!spells.ContainsKey(strSpell))
                            spells.Add(strSpell, 0);
                        spells[strSpell]++;
                    }
                }
                StringBuilder sb = new StringBuilder();
                foreach (DamageDice dd in physical.Keys)
                    sb.AppendFormat("{0}, ", new BasicDamage(1, dd));
                foreach (string strSpell in spells.Keys)
                    sb.AppendFormat("{0}, ", strSpell);
                return Global.Trim(sb).ToString();
            }
        }

        public override string ResistancesStringShort
        {
            get
            {
                if (MagicResistFull == MagicResistHalf)
                    return String.Format("Magic:{0}", MagicResistFull);
                return String.Format("Magic:{0},{1}", MagicResistFull, MagicResistHalf);
            }
        }

        private int MagResLevelHalfChance(int iMagRes) { return Math.Max(1, 2 * iMagRes - 40); }

        public string MonsterTypeString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Illusion)
                    sb.Append("Illusion, ");
                if (Categories.HasFlag(BT3MonsterCategories.Dragon))
                    sb.Append("Dragon, ");
                if (Categories.HasFlag(BT3MonsterCategories.Mechanical))
                    sb.Append("Mech, ");
                if (Categories.HasFlag(BT3MonsterCategories.Warrior))
                    sb.Append("Warrior, ");
                if (Categories.HasFlag(BT3MonsterCategories.Fiend))
                    sb.Append("Fiend, ");
                if (Categories.HasFlag(BT3MonsterCategories.Death))
                    sb.Append("Death, ");
                if (Categories.HasFlag(BT3MonsterCategories.Spellcaster))
                    sb.Append("Spellcaster, ");
                if (Categories.HasFlag(BT3MonsterCategories.Fast))
                    sb.Append("Fast, ");
                if (Categories.HasFlag(BT3MonsterCategories.Supernatural))
                    sb.Append("Supernatural, ");

                Global.Trim(sb);
                if (sb.Length < 1)
                    return "None";
                return sb.ToString();
            }
        }

        public override string AllPowersString
        {
            get
            {
                if (NoRandom)
                    return "Fixed";
                return "";
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                // test: Bit   Location              Name              HP                 Exp  A0 A1 A2 A3 14 1e 1f 24    26  29        2a        2b      
                //string[] names = Name.Split(':');
                //if (names.Length < 2) names = new string[] { "?", names[0] };
                //sb.AppendFormat("{0:D4}  {1,-20}  {2,-16}  {3,-12}  {4,8}  ", Index, names[0], names[1].Trim(), HPDice.ToString(), Experience);
                //for (int i = 0; i < Attacks.Length; i++)
                //    sb.AppendFormat("{0:X2} ", Attacks[i].RawBytes[0] < 0x80 ? "  " : String.Format("{0:X2}", Attacks[i].RawBytes[0]));
                //sb.AppendFormat("{0,-2} ", RawBytes[0x14] & 0x3F);
                //sb.AppendFormat("{0,-2} ", RawBytes[0x1e]);
                //sb.AppendFormat("{0,-2} ", RawBytes[0x1f]);
                //sb.AppendFormat("{0} ", Global.GetBits(RawBytes[0x24], "11111111", "00000000").Substring(0, 3));
                //sb.AppendFormat("{0:D3}  ", RawBytes[0x26]);
                //sb.AppendFormat("{0}  ", Global.GetBits(RawBytes[0x29], "11111111", "00000000"));
                //sb.AppendFormat("{0}  ", Global.GetBits(RawBytes[0x2A], "11111111", "00000000"));
                //sb.AppendFormat("{0}  ", Global.GetBits(RawBytes[0x2B], "11111111", "00000000"));
                //return sb.ToString();

                sb.AppendFormat("#{0}, {1}\r\n", Index, Name);
                if (HPDice == null)
                {
                    sb.AppendLine("(Invalid Monster Record)");
                    return sb.ToString();
                }
                if (NoRandom)
                    sb.AppendLine("Appears in fixed encounters only");
                sb.AppendFormat("HP: {0}, Group Size: {1}\r\n", HPString(true), GroupSize);
                sb.AppendFormat("AC: {0}\r\n", AC);
                sb.AppendFormat("Ignores Target AC: {0}{1}\r\n", ACIgnoreLow, ACIgnoreLow == ACIgnoreHigh ? "" : String.Format(" to {0}", ACIgnoreHigh));
                sb.AppendFormat("Starting Distance: {0}0'\r\n", InitialDistance);
                sb.AppendFormat("Advance Per Round: {0}0'\r\n", AdvancePerRound);
                sb.AppendFormat("Speed: {0}\r\n", Speed);
                sb.AppendFormat("Evade: {0}\r\n", Evade);
                sb.AppendFormat("Initiative: {0}{1}\r\n", InitiativeMin, InitiativeMin == InitiativeMax ? "" : String.Format("-{0}", InitiativeMax));
                sb.AppendFormat("Magic Resistance (zero damage): {0} ({1} chance above level {2})\r\n",
                    MagicResistFull, MagicResistFull < 20 ? "low" : "under 50%", MagResLevelHalfChance(MagicResistFull));
                if (MagicResistHalf != MagicResistFull)
                {
                    sb.AppendFormat("Magic Resistance (half damage): {0} ({1} chance above level {2})\r\n",
                        MagicResistHalf, MagicResistHalf < 20 ? "low" : "under 50%", MagResLevelHalfChance(MagicResistHalf));
                }
                sb.AppendFormat("Type: {0}\r\n", MonsterTypeString);
                sb.AppendFormat("Experience: {0}\r\n", Experience);
                sb.AppendFormat("Gold: {0}\r\n", Gold);
                for (int i = 0; i < Attacks.Length; i++)
                    sb.AppendFormat("Attack #{0}: {1}\r\n", i + 1, Attacks[i].ToString());
                if (Special != BTMonsterSpecial.None)
                {
                    sb.AppendFormat("Special: {0}\r\n", AllPowersString);
                }
                return sb.ToString();
            }
        }

        public override double AverageDamage
        {
            get
            {
                int iMelee = 0;
                double avgDmg = 0.0;
                foreach (BT3MonsterAttack attack in Attacks)
                {
                    if (!attack.Melee)
                        continue;

                    iMelee++;
                    avgDmg += attack.Damage.Average;
                }

                if (iMelee < 1)
                    return 0.0;

                return avgDmg / iMelee;
            }
        }
    }

    public class BT3MonsterList : BT123MonsterList
    {
        public override BTMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new BT3Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT3_Monster_List); }

        // The external bytes for BT3 are not stored in memory directly, so always use the internal bytes
        public override byte[] GetExternalBytes(MemoryHacker hacker) { return GetInternalBytes(); }
        public override bool InitInternalList()
        {
            m_monsters = SetFromBytes(GetInternalBytes());
            return true;
        }

        public BT3MonsterList()
        {
            MonsterLength = 48;
        }

        public override List<BTMonster> SetFromBytes(byte[] bytes)
        {
            List<BTMonster> monsters = new List<BTMonster>();

            try
            {
                int iIndex = 0;
                int iCount = 0;
                while (iIndex < bytes.Length - MonsterLength)
                {
                    int iMap = BitConverter.ToInt16(bytes, iIndex);
                    int iSize = BitConverter.ToUInt16(bytes, iIndex + 2);
                    string strAbbrev = Global.GetNullTerminatedString(bytes, iIndex + 4, 44);
                    string strMap = "Unknown";
                    strMap = BT3MemoryHacker.GetMapTitlePair(iMap).Title.Replace(", Level ", " ");

                    for (int i = iIndex + 48; i <= iIndex + iSize; i += MonsterLength)
                    {
                        BTMonster monster = CreateMonster(iCount, bytes, i);
                        monster.Name = String.Format("{0}: {1}", strMap, monster.Name);
                        monsters.Add(monster);
                        iCount++;
                    }
                    iIndex += (iSize + 48);
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
