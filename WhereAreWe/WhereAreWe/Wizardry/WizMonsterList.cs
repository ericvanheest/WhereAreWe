using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum WizTarget
    {
        // Not used in the game: Beast, Priest, Fighter, Construct, Giant, Rogue, Midget
        None = 0x0000,
        Fighter = 0x0001,
        Mage = 0x0002,
        Priest = 0x0004,
        Rogue = 0x0008,
        Midget = 0x0010,
        Giant = 0x0020,
        Mythical = 0x0040,
        Dragon = 0x0080,
        Beast = 0x0100,
        Were = 0x0200,
        Undead = 0x0400,
        Demon = 0x0800,
        Insect = 0x1000,
        Construct = 0x2000,
        Werdna = 0x1FFF,
        All = 0x3FFF
    }

    [Flags]
    public enum WizResist
    {
        None = 0x0000,
        Physical = 0x0001,
        Fire = 0x0002,
        Cold = 0x0004,
        Poison = 0x0008,
        LevelDrain = 0x0010,
        Stone = 0x0020,
        Magic = 0x0040,
        All = 0x007F
    }

    public enum WizBreath
    {
        None = 0,
        Fire = 1,
        Cold = 2,
        Poison = 3,
        LevelDrain = 4,
        Stone = 5,
        Death = 6
    }

    [Flags]
    public enum WizMonsterProperty
    {
        None =      0x0000,
        Stone =     0x0001,     // bit 0
        Poison =    0x0002,     // bit 1
        Paralyze =  0x0004,     // bit 2
        Autokill  = 0x0008,     // bit 3
        Sleep =     0x0010,     // bit 4
        RunAway =   0x0020,     // bit 5
        CallHelp =  0x0040      // bit 6
    }

    public enum WizMonsterFamily
    {
        First = 0,
        Fighter = 0,
        Mage = 1,
        Priest = 2,
        Rogue = 3,
        Midget = 4,
        Giant = 5,
        Mythical = 6,
        Dragon = 7,
        Beast = 8,
        Were = 9,
        Undead = 10,
        Demon = 11,
        Insect = 12,
        Construct = 13,
        Last = 14
    }

    public class WizMonster : Monster
    {
        public const int Size = 94;

        public WizCondition WizCondition;
        public DamageDice NumAppearing;
        public int Image;
        public int Silenced;
        public int ACModifier;
        public DamageDice HitPoints;
        public WizMonsterFamily Family;
        public List<DamageDice> Attacks;
        public int Drain;
        public int Regeneration;
        public int Reward1;
        public int Reward2;
        public int GroupHelpChance;
        public int MageSpellLevel;
        public int PriestSpellLevel;
        public WizBreath BreathWeapon;
        public WizResist Resistances;
        public int Unique;
        public int Target;
        public int Help;
        public WizMonsterProperty Properties;
        public GenericAttribute DrainStat = GenericAttribute.None;
        public int Help2;
        public int Help3;
        public int BreathChance;
        public int MagicChance;
        public int StealChance;
        public int NPC1;
        public int NPC2;

        public int[] Unknowns = null;

        public virtual string GetName(int index) { return "Unknown"; }

        public void SetFromBytes(int iIndex, string strName, byte[] bytes, int offset)
        {
            Index = iIndex;
            Silenced = 0;
            ACModifier = 0;
            Image = BitConverter.ToInt16(bytes, offset + Offsets.Image);
            NumAppearing = DiceFromOffset(bytes, offset + Offsets.NumAppearing);
            HitPoints = DiceFromOffset(bytes, offset + Offsets.HitPoints);
            Family = (WizMonsterFamily)BitConverter.ToInt16(bytes, offset + Offsets.Family);
            AC = BitConverter.ToInt16(bytes, offset + Offsets.ArmorClass);
            NumAttacks = BitConverter.ToInt16(bytes, offset + Offsets.NumAttacks);
            Attacks = new List<DamageDice>();
            for (int i = Offsets.Attack1; i <= Offsets.Attack7; i += 6)
            {
                DamageDice dice = DiceFromOffset(bytes, offset + i);
                if (dice.Quantity > 0)
                    Attacks.Add(dice);
            }
            Experience = new WizardryLong(bytes, offset + Offsets.Experience).Number;
            Drain = BitConverter.ToInt16(bytes, offset + Offsets.Drain);
            Unique = BitConverter.ToInt16(bytes, offset + Offsets.Unique);
            Regeneration = BitConverter.ToInt16(bytes, offset + Offsets.Regeneration);
            Reward1 = BitConverter.ToInt16(bytes, offset + Offsets.Reward1);
            Reward2 = BitConverter.ToInt16(bytes, offset + Offsets.Reward2);
            Help = BitConverter.ToInt16(bytes, offset + Offsets.GroupHelp);
            GroupHelpChance = BitConverter.ToInt16(bytes, offset + Offsets.GroupHelpChance);
            MageSpellLevel = BitConverter.ToInt16(bytes, offset + Offsets.MageSpellLevel);
            PriestSpellLevel = BitConverter.ToInt16(bytes, offset + Offsets.ClericSpellLevel);
            BreathWeapon = (WizBreath)BitConverter.ToInt16(bytes, offset + Offsets.BreathWeapon);
            MagicResist = BitConverter.ToInt16(bytes, offset + Offsets.MagicResist);
            Resistances = (WizResist)BitConverter.ToInt16(bytes, offset + Offsets.Resistances);
            Properties = (WizMonsterProperty)BitConverter.ToInt16(bytes, offset + Offsets.Special);
            RewardModifier = -1;
            Name = strName;
            if (Experience == 0)
                Experience = CalculateExp();
        }



        public override string OneLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ", ProperName);
                sb.AppendFormat("({0}), ", GetFamilyString(Family));
                sb.AppendFormat("{0} HP, ", HitPoints.ToString());
                sb.AppendFormat("{0} AC, ", AC);
                sb.AppendFormat("{0:F1} dmg, ", AverageDamage);

                string strProperties = GetPropertyString(Properties);
                if (!String.IsNullOrWhiteSpace(strProperties))
                    sb.AppendFormat("{0}, ", strProperties);
                if (BreathWeapon != WizBreath.None)
                    sb.AppendFormat("{0}, ", GetBreathString(BreathWeapon));
                if (PriestSpellLevel > 0)
                    sb.AppendFormat("C{0}, ", PriestSpellLevel);
                if (MageSpellLevel > 0)
                    sb.AppendFormat("M{0}, ", MageSpellLevel);
                if (Drain > 0)
                    sb.AppendFormat("Drain {0}, ", Drain);
                if (Regeneration > 0)
                    sb.AppendFormat("Regen {0}, ", Regeneration);
                if (GroupHelpChance > 0)
                    sb.Append("Call Help, ");
                if (MagicResist > 0)
                    sb.AppendFormat("MagRes {0}%, ", MagicResist);
                sb.AppendFormat("Resist: {0}", WizItem.GetResistString(Resistances, true));

                return Global.Trim(sb).ToString();
            }
        }

        public static string GetBreathString(WizBreath index)
        {
            switch (index)
            {
                case WizBreath.None: return "None";
                case WizBreath.Fire: return "Fire";
                case WizBreath.Cold: return "Cold";
                case WizBreath.Poison: return "Poison";
                case WizBreath.LevelDrain: return "LevelDrain";
                case WizBreath.Stone: return "Stone";
                case WizBreath.Death: return "Death";
                default: return String.Format("Unknown({0})", (int)index);

            }
        }

        public static string GetFamilyString(WizMonsterFamily index)
        {
            switch (index)
            {
                case WizMonsterFamily.Fighter: return "Fighter";
                case WizMonsterFamily.Mage: return "Mage";
                case WizMonsterFamily.Priest: return "Priest";
                case WizMonsterFamily.Rogue: return "Rogue";
                case WizMonsterFamily.Beast: return "Beast";
                case WizMonsterFamily.Midget: return "Midget";
                case WizMonsterFamily.Mythical: return "Mythical";
                case WizMonsterFamily.Demon: return "Demon";
                case WizMonsterFamily.Giant: return "Giant";
                case WizMonsterFamily.Were: return "Were";
                case WizMonsterFamily.Dragon: return "Dragon";
                case WizMonsterFamily.Undead: return "Undead";
                case WizMonsterFamily.Insect: return "Insect";
                case WizMonsterFamily.Construct: return "Construct";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSinglePropertyString(WizMonsterProperty index)
        {
            switch (index)
            {
                case WizMonsterProperty.None: return "None";
                case WizMonsterProperty.Autokill: return "Autokill";
                case WizMonsterProperty.CallHelp: return "Summon";
                case WizMonsterProperty.Paralyze: return "Paralyze";
                case WizMonsterProperty.Poison: return "Poison";
                case WizMonsterProperty.RunAway: return "Escape";
                case WizMonsterProperty.Sleep: return "Sleep";
                case WizMonsterProperty.Stone: return "Stone";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public string FullPropertyString
        {
            get
            {
                bool bWiz4 = (this is Wiz4Monster);
                if (Properties == WizMonsterProperty.None)
                    return String.Empty;

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 7; i++)
                {
                    WizMonsterProperty single = (WizMonsterProperty)(1 << i);
                    if (Properties.HasFlag(single))
                    {
                        if (single == WizMonsterProperty.CallHelp && Help == 0 && GroupHelpChance == 0)
                            sb.AppendFormat("Summon {0}, ", Name);
                        else if (bWiz4 && single == WizMonsterProperty.CallHelp)
                            sb.AppendFormat("Summon{0}, ", GroupHelpChance > 0 ? String.Format(" ({0}%)", GroupHelpChance) : "");
                        else if (single == WizMonsterProperty.CallHelp && GroupHelpChance > 0)
                            sb.AppendFormat("Summon {0} ({1}%), ", GetName(Help), GroupHelpChance);
                        else
                            sb.AppendFormat("{0}, ", GetSinglePropertyString(single));
                    }
                }
                return Global.Trim(sb).ToString();
            }
        }

        public static string GetPropertyString(WizMonsterProperty index, bool bAbbreviated = false)
        {
            if (index == WizMonsterProperty.None)
                return bAbbreviated ? "" : "None";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                WizMonsterProperty single = (WizMonsterProperty)(1 << i);
                if (index.HasFlag(single))
                {
                    sb.AppendFormat("{0},", bAbbreviated ? GetSinglePropertyString(single).Substring(0, 2) : GetSinglePropertyString(single));
                    if (!bAbbreviated)
                        sb.Append(" ");
                }
            }
            return Global.Trim(sb).ToString();
        }

        public override BasicConditionFlags Condition
        {
            get
            {
                BasicConditionFlags flags = WizCharacter.GetBasicCondition(WizCondition);
                if (Silenced > 0)
                    flags |= BasicConditionFlags.Silenced;
                return flags;
            }
        }

        private long ShiftMul(long i, int shift) { return shift < 1 ? 0 : i * (1L << (shift - 1)); }

        public long CalculateExp()
        {
            long iExp = HitPoints.Quantity * HitPoints.Faces;
            iExp *= 20;
            if (BreathWeapon != WizBreath.None)
                iExp *= 2;
            iExp += ShiftMul(35, MageSpellLevel);
            iExp += ShiftMul(35, PriestSpellLevel);
            iExp += ShiftMul(200, Drain);
            iExp += ShiftMul(90, Regeneration);
            iExp += (40 * (11 - AC));
            if (NumAttacks > 1)
                iExp += ShiftMul(30, NumAttacks);
            if (MagicResist > 0)
                iExp += ShiftMul(40, MagicResist / 10 + 1);
            if (MagicResist >= 80)
                iExp += (10000 * (MagicResist / 10 - 7));
            iExp += ShiftMul(35, Global.NumBitsSet((int)(Resistances & ~WizResist.Physical), 7));
            iExp += ShiftMul(40, Global.NumBitsSet((int)Properties, 8));
            return iExp;
        }

        protected static DamageDice DiceFromOffset(byte[] bytes, int offset)
        {
            return new DamageDice(
                BitConverter.ToInt16(bytes, offset + 2),
                BitConverter.ToInt16(bytes, offset),
                BitConverter.ToInt16(bytes, offset + 4));
        }

        public static class Offsets
        {
            public static int Image = 0;
            public static int NumAppearing = 2;
            public static int HitPoints = 8;
            public static int Family = 14;
            public static int ArmorClass = 16;
            public static int NumAttacks = 18;
            public static int Attack1 = 20;
            public static int Attack2 = 26;
            public static int Attack3 = 32;
            public static int Attack4 = 38;
            public static int Attack5 = 44;
            public static int Attack6 = 50;
            public static int Attack7 = 56;
            public static int Experience = 62;
            public static int Drain = 68;
            public static int Regeneration = 70;
            public static int Reward1 = 72;
            public static int Reward2 = 74;
            public static int GroupHelp = 76;
            public static int GroupHelpChance = 78;
            public static int MageSpellLevel = 80;
            public static int ClericSpellLevel = 82;
            public static int Unique = 84;
            public static int BreathWeapon = 86;
            public static int MagicResist = 88;
            public static int Resistances = 90;
            public static int Special = 92;
        }

        protected void WriteDice(MemoryStream ms, DamageDice dice)
        {
            Global.WriteInt16(ms, dice.Quantity);
            Global.WriteInt16(ms, dice.Faces);
            Global.WriteInt16(ms, dice.Bonus);
        }

        public virtual byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Global.WriteInt16(ms, Image);
            WriteDice(ms, NumAppearing);
            WriteDice(ms, HitPoints);
            Global.WriteInt16(ms, (int)Family);
            Global.WriteInt16(ms, AC);
            Global.WriteInt16(ms, NumAttacks);
            for (int i = 0; i < 7; i++)
            {
                if (Attacks.Count > i)
                    WriteDice(ms, Attacks[i]);
                else
                    WriteDice(ms, DamageDice.Zero);
            }
            byte[] bytesExp = WizardryLong.GetBytes(Experience);
            ms.Write(bytesExp, 0, bytesExp.Length);
            Global.WriteInt16(ms, Drain);
            Global.WriteInt16(ms, Regeneration);
            Global.WriteInt16(ms, Reward1);
            Global.WriteInt16(ms, Reward2);
            Global.WriteInt16(ms, Help);
            Global.WriteInt16(ms, GroupHelpChance);
            Global.WriteInt16(ms, MageSpellLevel);
            Global.WriteInt16(ms, PriestSpellLevel);
            Global.WriteInt16(ms, Unique);
            Global.WriteInt16(ms, (int)BreathWeapon);
            Global.WriteInt16(ms, MagicResist);
            Global.WriteInt16(ms, (int)Resistances);
            Global.WriteInt16(ms, (int)Properties);
            return ms.ToArray();
        }

        public override int InternalIndex { get { return Index; } }
        public override string ProperName { get { return Name; } }
        public override string HPString(bool bPreEncounter) { return bPreEncounter ? HitPoints.ToString() : CurrentHP.ToString(); }
        public override double AverageHP { get { return HitPoints.Average; } }

        public override double AverageDamage
        {
            get
            {
                double total = 0.0;
                foreach (DamageDice dd in Attacks)
                    total += dd.Average;
                return total;
            }
        }

        public override string SpeedString { get { return NumAppearing.ToString(); } }

        public override string DamageString
        {
            get
            {
                if (Attacks == null || Attacks.Count < 1)
                    return String.Empty;
                Dictionary<DamageDice, int> dict = new Dictionary<DamageDice, int>();
                foreach (DamageDice attack in Attacks)
                {
                    if (!dict.ContainsKey(attack))
                        dict.Add(attack, 0);
                    dict[attack]++;
                }

                StringBuilder sb = new StringBuilder();
                foreach (DamageDice dd in dict.Keys)
                {
                    int iCount = dict[dd];
                    if (iCount == 1)
                        sb.AppendFormat("{0}, ", dd.ToString());
                    else
                        sb.AppendFormat("{0}x{1}, ", iCount, dd.ToString());
                }

                return Global.Trim(sb).ToString();
            }
        }

        public override string GetMultiLineDescription(bool bActive) { return MultiLineDescription; }
        public override string ResistancesStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MagicResist > 0)
                    sb.AppendFormat("Mag {0}%, ", MagicResist);
                sb.Append(WizItem.GetResistString(Resistances, true));
                return Global.Trim(sb).ToString();
            }
        }

        public override string AllPowersString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string strProperties = FullPropertyString;
                if (!String.IsNullOrWhiteSpace(strProperties))
                    sb.AppendFormat("{0}, ", strProperties);
                if (BreathWeapon != WizBreath.None)
                    sb.AppendFormat("{0}Breath, ", GetBreathString(BreathWeapon));
                if (PriestSpellLevel > 0)
                    sb.AppendFormat("Cleric {0}, ", PriestSpellLevel);
                if (MageSpellLevel > 0)
                    sb.AppendFormat("Mage {0}, ", MageSpellLevel);
                if (Drain > 0)
                    sb.AppendFormat("Drain {0}, ", Drain);
                if (Regeneration > 0)
                    sb.AppendFormat("Regen {0}, ", Regeneration);
                if (DrainStat != GenericAttribute.None)
                    sb.AppendFormat("Drain {0}, ", Global.GenericAttributeString(DrainStat));
                if (StealChance > 0)
                    sb.AppendFormat("Steal {0}%, ", StealChance);
                if (MagicChance > 0)
                    sb.AppendFormat("Cast {0}%, ", MagicChance);
                if (Unknowns != null)
                {
                    if (Unknowns[0] != 0)
                        sb.AppendFormat("ResistUnknown {0}%, ", Unknowns[0]);

                    StringBuilder sbUnk = new StringBuilder();
                    for (int i = 1; i < Unknowns.Length; i++)
                    {
                        if (Unknowns[i] != 0)
                            sbUnk.AppendFormat("{0}:{1}, ", i, Unknowns[i]);
                    }
                    if (Global.Trim(sbUnk).Length > 0)
                        sb.AppendFormat("[Unk {0}]", sbUnk.ToString());
                }

                return Global.Trim(sb).ToString();
            }
        }

        public virtual int TreasureCount { get { return 0; } }
        public virtual WizTreasure Treasure(int index) { return null; }

        public override string TreasureStringShort
        {
            get
            {
                // return String.Format("<{0},{1}>", Reward1, Reward2);
                if (Reward1 < 0 || Reward2 < 0 || Reward1 >= TreasureCount || Reward2 >= TreasureCount)
                    return String.Format("<Unknown:{0},{1}>", Reward1, Reward2);

                string str1 = Treasure(Reward1).ShortDescription;
                string str2 = Treasure(Reward2).ShortDescription;

                switch (RewardModifier)
                {
                    case 0: return str1;
                    case 1: return String.Format("2x {0}", str1);
                    case 2: return str2;
                    default: return str1 == str2 ? str1 : String.Format("{0} / {1}", str1, str2);
                }
            }
        }

        public override int TreasureStrength { get { return Reward2; } }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0}, {1} ({2})\r\n", Index, Name, GetFamilyString(Family));
                if (WizCondition != WizCondition.Good || Silenced > 0)
                {
                    sb.Append("Condition: ");
                    if (WizCondition != WizCondition.Good)
                        sb.AppendFormat("{0}, ", Global.Title(StatusSuffix));
                    if (Silenced > 0)
                        sb.AppendFormat("Silenced for {0} rounds", Silenced);
                    Global.Trim(sb);
                    sb.AppendLine();
                }
                //sb.AppendFormat("Image: {0}\r\n", Image);
                if (HitPoints == null)
                {
                    sb.AppendLine("(Invalid Monster Record)");
                    return sb.ToString();
                }
                sb.AppendFormat("HP: {0}, Group Size: {1}\r\n", HitPoints.ToString(), NumAppearing.ToString());
                if (ACModifier != 0)
                    sb.AppendFormat("AC: {0} ({1} from a spell effect)\r\n", AC, Global.AddPlus(ACModifier));
                else
                    sb.AppendFormat("AC: {0}\r\n", AC);
                sb.AppendFormat("Attacks: {0}\r\n", DamageString);
                string strProperties = GetPropertyString(Properties);
                if (!String.IsNullOrWhiteSpace(strProperties))
                    sb.AppendFormat("Special: {0}\r\n", strProperties);
                if (StealChance > 0)
                    sb.AppendFormat("Chance to Steal: {0}%\r\n", StealChance);
                if (MagicChance > 0)
                    sb.AppendFormat("Chance to Cast Spell: {0}%\r\n", MagicChance);
                if (BreathWeapon != WizBreath.None)
                {
                    if (BreathChance > 0)
                        sb.AppendFormat("Breath Weapon: {0} ({1}% chance)\r\n", GetBreathString(BreathWeapon), BreathChance);
                    else
                        sb.AppendFormat("Breath Weapon: {0}\r\n", GetBreathString(BreathWeapon));
                }
                if (PriestSpellLevel > 0 || MageSpellLevel > 0)
                {
                    sb.Append("Spell Level: ");
                    if (PriestSpellLevel > 0)
                        sb.AppendFormat("Cleric {0}, ", PriestSpellLevel);
                    if (MageSpellLevel > 0)
                        sb.AppendFormat("Mage {0}, ", MageSpellLevel);
                    Global.Trim(sb);
                    sb.AppendLine();
                }
                if (Drain > 0)
                    sb.AppendFormat("Drain: {0}\r\n", Drain);
                if (DrainStat != GenericAttribute.None)
                    sb.AppendFormat("Drain {0}\r\n", Global.GenericAttributeString(DrainStat));
                if (Regeneration > 0)
                    sb.AppendFormat("Regeneration: {0}\r\n", Regeneration);
                if (Reward1 >= 0 && Reward2 >= 0 && Reward1 < TreasureCount && Reward2 < TreasureCount)
                {
                    sb.AppendFormat("Random Treasure (#{0}): {1}\r\n", Reward1, Treasure(Reward1).LongDescription);
                    sb.AppendFormat("Fixed Treasure (#{0}): {1}\r\n", Reward2, Treasure(Reward2).LongDescription);
                }
                if (GroupHelpChance > 0)
                {
                    if (Help2 == 0)
                        sb.AppendFormat("Group Help ({0}%): #{1}, {2}\r\n", GroupHelpChance, Help, GetName(Help));
                    else if (Help3 == 0)
                        sb.AppendFormat("Group Help ({0}%): #{1},{2} ({3}, {4})\r\n", GroupHelpChance, Help, Help2, GetName(Help), GetName(Help2));
                    else
                        sb.AppendFormat("Group Help ({0}%): #{1},{2},{3} ({4}, {5}, {6})\r\n", GroupHelpChance, Help, Help2, Help3, GetName(Help), GetName(Help2), GetName(Help3));
                }
                if (MagicResist > 0)
                    sb.AppendFormat("Magic Resist: {0}%\r\n", MagicResist);
                sb.AppendFormat("Resists: {0}\r\n", WizItem.GetResistString(Resistances));
                if (Experience > 0)
                    sb.AppendFormat("Experience: {0}\r\n", Experience);
                if (NPC1 > 0 || NPC2 > 0)
                {
                    sb.AppendFormat("NPC values: {0}, {1}\r\n", NPC1, NPC2);
                }
                if (Unknowns != null)
                {
                    if (Unknowns[0] != 0)
                        sb.AppendFormat("Unknown Resistance: {0}%\r\n", Unknowns[0]);

                    for (int i = 1; i < Unknowns.Length; i++)
                    {
                        if (Unknowns[i] != 0)
                            sb.AppendFormat("Unknown {0}: {1}\r\n", i, Unknowns[i]);
                    }
                }

                if (Reward1 >= 0 && Reward2 >= 0 && Reward1 < TreasureCount && Reward2 < TreasureCount)
                {
                    sb.AppendLine();
                    sb.Append("Random Encounter Treasure Formula:\r\n");
                    sb.Append(Treasure(Reward1).MultilineDescription);
                    sb.AppendLine();
                    sb.Append("Fixed Encounter Treasure Formula:\r\n");
                    sb.Append(Treasure(Reward2).MultilineDescription);
                }
                return sb.ToString();
            }
        }
    }

    public abstract class Wiz123MonsterList
    {
        public List<WizMonster> Monsters;
        public List<WizMonster> InternalMonsters;
        public bool UsingInternalList = false;
        public bool Valid = false;

        public virtual WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return null; }
        public virtual byte[] GetInternalBytes() { return null; }
        public virtual byte[] GetExternalBytes(MemoryHacker hacker) { return null; }

        public bool Reinitialize(Wiz123MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
                return true;

            InitInternalList();
            return false;
        }

        public virtual List<WizMonster> SetFromBytes(byte[] bytes)
        {
            List<WizMonster> monsters = new List<WizMonster>(bytes.Length / WizMonster.Size);

            try
            {
                // The wizardry monsters are stored in 10-item sections, padded out to 1024 bytes each
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - WizMonster.Size)
                {
                    monsters.Add(CreateMonster(iItemCount, bytes, iPos));
                    iPos += WizMonster.Size;
                    iItemCount++;
                    if (iItemCount % 10 == 0)
                        iPos += 84;
                }
                Valid = true;
            }
            catch (Exception)
            {
                Valid = false;
            }

            return monsters;
        }

        public bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            Monsters = SetFromBytes(bytes);

            if (!bOverrideSanityCheck)
            {
                if (Monsters == null || Monsters.Count < 1 || Monsters[0].HitPoints.Quantity == 0 || Monsters[0].NumAppearing.Quantity == 0)
                {
                    InitInternalList();
                    return false;
                }
            }

            UsingInternalList = false;
            return true;
        }

        public bool InitInternalList()
        {
            InternalMonsters = SetFromBytes(GetInternalBytes());
            Monsters = InternalMonsters;
            UsingInternalList = true;
            return true;
        }
    }

    public class WizTreasure
    {
        public const int Size = 6 + (9 * Reward.Size);

        public enum Trap
        {
            TraplessChest = 0,
            PoisonNeedle = 1,
            GasBomb = 2,
            MiscTrap = 3,
            Teleporter = 4,
            MageBlaster = 5,
            PriestBlaster = 6,
            Alarm = 7,
            CrossbowBolt = 8,
            ExplodingBox = 9,
            Splinters = 10,
            Blades = 11,
            Stunner = 12,

            Last
        }

        [Flags]
        public enum TrapFlags
        {
            None = 0,
            TraplessChest =  0x01,
            PoisonNeedle =   0x02,
            GasBomb =        0x04,
            MiscTrap =       0x08,
            Teleporter =     0x10,
            MageBlaster =    0x20,
            PriestBlaster =  0x40,
            Alarm =          0x80,
            All = 0xff
        }

        public static string GetTrapName(Trap trap)
        {
            switch (trap)
            {
                case Trap.Alarm: return "Alarm";
                case Trap.MageBlaster: return "Mage Blaster";
                case Trap.PriestBlaster: return "Priest Blaster";
                case Trap.Blades: return "Blades";
                case Trap.CrossbowBolt: return "Crossbow Bolt";
                case Trap.ExplodingBox: return "Exploding Box";
                case Trap.GasBomb: return "Gas Bomb";
                case Trap.PoisonNeedle: return "Poison Needle";
                case Trap.Splinters: return "Splinters";
                case Trap.Stunner: return "Stunner";
                case Trap.Teleporter: return "Teleporter";
                case Trap.TraplessChest: return "Untrapped";
                default: return "Unknown";
            }
        }

        public virtual string TrapsString
        {
            get
            {
                if (Traps == TrapFlags.All)
                    return "Any";
                if (Traps == TrapFlags.None)
                    return "None";
                StringBuilder sb = new StringBuilder();
                if (Traps.HasFlag(TrapFlags.TraplessChest))
                    sb.Append("Untrapped, ");
                if (Traps.HasFlag(TrapFlags.PoisonNeedle))
                    sb.Append("Poison Needle, ");
                if (Traps.HasFlag(TrapFlags.GasBomb))
                    sb.Append("Gas Bomb, ");
                if (Traps.HasFlag(TrapFlags.MiscTrap))
                    sb.Append("Crossbow Bolt, Exploding Box, Stunner, ");
                if (Traps.HasFlag(TrapFlags.Teleporter))
                    sb.Append("Teleporter, ");
                if (Traps.HasFlag(TrapFlags.MageBlaster))
                    sb.Append("Mage Blaster, ");
                if (Traps.HasFlag(TrapFlags.PriestBlaster))
                    sb.Append("Priest Blaster, ");
                if (Traps.HasFlag(TrapFlags.Alarm))
                    sb.Append("Alarm, ");
                return Global.Trim(sb).ToString();
                // Splinters and Blades don't appear to actually be in the DOS version of Wizardry 1
                //return Global.Trim(sb).ToString() + (Traps.HasFlag(TrapFlags.MiscTrap) ? ",\r\n     Crossbow Bolt, Exploding Box, Splinters, Blades, Stunner" : "");
            }
        }

        public class Reward
        {
            public const int Size = 18;

            public class Gold
            {
                public BasicDamage First;
                public DamageDice Second;

                public int Minimum { get { return First.Min * Second.Min; } }
                public int Maximum { get { return First.Max * Second.Max; } }
                public double Average { get { return First.Average * Second.Average; } }

                public string Formula
                {
                    get
                    {
                        if (Second.Faces == 1 && Second.Bonus == 0 && Second.Quantity == 1)
                            return First.ToString();
                        return String.Format("{0}, {1} times", First.ToString(), Second.ToString());
                    }
                }

                public Gold(byte[] bytes, int offset = 0)
                {
                    int quantity1 = BitConverter.ToInt16(bytes, offset);
                    int faces1 = BitConverter.ToInt16(bytes, offset + 2);
                    int bonus1 = BitConverter.ToInt16(bytes, offset + 4);
                    int multiply = BitConverter.ToInt16(bytes, offset + 6);
                    int quantity2 = BitConverter.ToInt16(bytes, offset + 8);
                    int faces2 = BitConverter.ToInt16(bytes, offset + 10);
                    int bonus2 = BitConverter.ToInt16(bytes, offset + 12);

                    First = new BasicDamage(multiply, new DamageDice(faces1, quantity1, bonus1));
                    Second = new DamageDice(faces2, quantity2, bonus2);
                }
            }

            public class Artifact
            {
                public int MinIndex;
                public int MFactor;
                public int MaxTimes;
                public int Range;
                public int ChanceBigger;
                public int Unused1;
                public int Unused2;

                public int Maximum { get { return MinIndex + Range + (MFactor * MaxTimes); } }
                public int Minimum { get { return MinIndex; } }

                public Artifact(byte[] bytes, int offset = 0)
                {
                    MinIndex = BitConverter.ToInt16(bytes, offset);
                    MFactor = BitConverter.ToInt16(bytes, offset + 2);
                    MaxTimes = BitConverter.ToInt16(bytes, offset + 4);
                    Range = BitConverter.ToInt16(bytes, offset + 6);
                    ChanceBigger = BitConverter.ToInt16(bytes, offset + 8);
                    Unused1 = BitConverter.ToInt16(bytes, offset + 10);
                    Unused2 = BitConverter.ToInt16(bytes, offset + 12);
                }
            }

            public int Chance;
            public bool IsItem;

            public Gold Money;
            public Artifact Item;

            public Reward(byte[] bytes, int offset = 0)
            {
                Chance = BitConverter.ToInt16(bytes, offset);
                IsItem = BitConverter.ToInt16(bytes, offset + 2) != 0;
                Money = new Gold(bytes, offset + 4);
                Item = new Artifact(bytes, offset + 4);
            }
        }

        public bool Chest;
        public TrapFlags Traps { get { return (TrapFlags)m_iTrapsFlag; } set { m_iTrapsFlag = (int)value; } }
        public int NumRewards;
        public List<Reward> Rewards;
        public byte[] Bytes;
        public GameNames Game;
        protected int m_iTrapsFlag;

        public WizTreasure(GameNames game, byte[] bytes, int offset = 0)
        {
            Game = game;
            Bytes = Global.Subset(bytes, offset, Size);
            Chest = BitConverter.ToInt16(bytes, offset) != 0;
            Traps = (TrapFlags) BitConverter.ToUInt16(bytes, offset + 2);
            NumRewards = BitConverter.ToInt16(bytes, offset + 4);
            Rewards = new List<Reward>(9);
            for (int i = 0; i < 9; i++)
                Rewards.Add(new Reward(bytes, offset + 6 + (i * Reward.Size)));
        }

        public WizTreasure()
        {
            Game = GameNames.None;
            NumRewards = 0;
        }

        public static Trap[] AllTraps
        {
            get
            {
                Trap[] traps = new Trap[(int)Trap.Last];
                for (Trap trap = Trap.TraplessChest; trap < Trap.Last; trap++)
                    traps[(int)trap] = trap;
                return traps;
            }
        }

        public string MultilineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Possible traps: {0}\r\n", TrapsString);
                for (int iReward = 0; iReward < Rewards.Count; iReward++)
                {
                    if (iReward >= NumRewards)
                        continue;
                    WizTreasure.Reward reward = Rewards[iReward];
                    if (reward.Chance == 0)
                        continue;
                    if (reward.Chance < 100)
                        sb.AppendFormat("({0}%) ", reward.Chance);
                    sb.AppendFormat("{0}: ", reward.IsItem ? "Item" : "Gold");
                    if (reward.IsItem)
                    {
                        WizTreasure.Reward.Artifact item = reward.Item;
                        if (item.Range == 0 || item.Range == 1)
                            sb.AppendFormat("{0}", Games.GetItemName(Game, item.MinIndex));
                        else if (item.MinIndex == 1 && item.Range == 15)
                            sb.AppendFormat("Level 1 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 17 && item.Range == 15)
                            sb.AppendFormat("Level 2 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 33 && item.Range == 18)
                            sb.AppendFormat("Level 3 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 52 && item.Range == 27)
                            sb.AppendFormat("Level 4 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 78 && item.Range == 13)
                            sb.AppendFormat("Level 5 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 79)
                            sb.AppendFormat("Level 6 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else if (item.MinIndex == 110)
                            sb.AppendFormat("Level 7 (#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex - 1);
                        else
                            sb.AppendFormat("#{0} to #{1}", item.MinIndex, item.Range + item.MinIndex - 1);
                        if (item.ChanceBigger == 0 || item.MaxTimes == 0 || item.MFactor == 0)
                            sb.AppendLine();
                        else
                            sb.AppendFormat(", with {0} {1}% chance{2} to increase by {3}\r\n",
                                item.MaxTimes, item.ChanceBigger + 1, item.MaxTimes == 1 ? "" : "s", item.MFactor);
                    }
                    else
                    {
                        WizTreasure.Reward.Gold gold = reward.Money;
                        sb.AppendFormat("{0}-{1} ({2})\r\n",
                            gold.Minimum, gold.Maximum, gold.Formula);
                    }
                }
                return sb.ToString();
            }
        }

        public string GetDescription(bool bLong)
        {
            StringBuilder sb = new StringBuilder();
            double averageGold = 0;
            int iMinGold = 0;
            int iMaxGold = 0;
            int iHighestItem = -1;
            int iLowestItem = -1;
            int iLowestChance = 0;
            int iHighestChance = 0;
            bool bUsesMFactor = false;
            for (int iReward = 0; iReward < Rewards.Count; iReward++)
            {
                if (iReward >= NumRewards)
                    continue;
                Reward reward = Rewards[iReward];
                if (reward.Chance == 0)
                    continue;
                if (reward.IsItem)
                {
                    int iMax = reward.Item.Maximum;
                    if (reward.Item.MaxTimes > 0 && reward.Item.MFactor > 0)
                        bUsesMFactor = true;
                    if (iHighestItem < iMax)
                    {
                        iHighestItem = Math.Max(iHighestItem, iMax);
                        iHighestChance = reward.Chance;
                    }
                    if (iLowestItem == -1 || iLowestItem > reward.Item.Minimum)
                    {
                        iLowestItem = reward.Item.Minimum;
                        iLowestChance = reward.Chance;
                    }
                }
                else
                {
                    averageGold += reward.Money.Average;
                    iMinGold += reward.Money.Minimum;
                    iMaxGold += reward.Money.Maximum;
                }
            }
            if (averageGold > 0)
            {
                if (bLong)
                    sb.AppendFormat("{0}-{1} Gold, ", iMinGold, iMaxGold);
                else
                    sb.AppendFormat("{0}g, ", (int)averageGold);
            }
            if (iHighestItem != -1)
            {
                if (iLowestItem == iHighestItem || iLowestItem == iHighestItem - 1)
                    sb.AppendFormat("{0}", Games.GetItemName(Game, iLowestItem));
                else
                {
                    if (bLong)
                    {
                        sb.AppendFormat("Items #{0} ({1}%) to #{2} ({3}%)", iLowestItem, iLowestChance, iHighestItem, iHighestChance);
                    }
                    else
                    {
                        // arbitrary
                        int iLevel1 = 1;
                        int iLevel2 = 1;
                        if (bUsesMFactor)
                        {
                            iLevel1 = LevelForItemMFactor(iLowestItem);
                            iLevel2 = LevelForItemMFactor(iHighestItem);
                        }
                        else
                        {
                            iLevel1 = LevelForItem(iLowestItem);
                            iLevel2 = LevelForItem(iHighestItem);
                        }
                        if (iLevel1 == iLevel2)
                            sb.AppendFormat("L{0} Item", iLevel1);
                        else
                            sb.AppendFormat("L{0}-{1} Item", iLevel1, iLevel2);
                    }
                }
            }

            return Global.Trim(sb).ToString();
        }

        public virtual int LevelForItemMFactor(int index)
        {
            if (index == 57)
                return 3;
            int iLevel = index / 10 - 3;
            if (iLevel < 1)
                iLevel = 1;
            return iLevel;
        }

        public virtual int LevelForItem(int index)
        {
            if (index < 17)
                return 1;
            if (index < 33)
                return 2;
            if (index < 52)
                return 3;
            if (index < 79)
                return 4;
            if (index < 80)
                return 5;
            if (index < 110)
                return 6;
            return 7;
        }

        public string ShortDescription { get { return GetDescription(false); } }
        public string LongDescription { get { return GetDescription(true); } }
    }

    public abstract class Wiz123TreasureList
    {
        public List<WizTreasure> Treasures;
        public bool Internal = false;
        public bool Valid = false;
        public GameNames Game;

        public abstract byte[] GetExternalBytes(MemoryHacker hacker);
        public abstract byte[] GetInternalBytes();
        public virtual int TreasureSize { get { return WizTreasure.Size; } }
        public virtual int Padding { get { return 1024; } }
        public virtual WizTreasure CreateTreasure(GameNames game, byte[] bytes, int offset) { return new WizTreasure(Game, bytes, offset); }

        public virtual List<WizTreasure> SetFromBytes(GameNames game, byte[] bytes)
        {
            Game = game;
            int iSize = TreasureSize;
            List<WizTreasure> Treasures = new List<WizTreasure>(bytes.Length / iSize);

            try
            {
                // The wizardry treasures are stored in 6-item sections, padded out to 1024 bytes each
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - iSize)
                {
                    Treasures.Add(CreateTreasure(Game, bytes, iPos));
                    iPos += iSize;
                    iItemCount++;
                    if (Padding > 0 && (iItemCount % (Padding / iItemCount) == 0))
                        iPos += (Padding - (iPos % Padding));
                }
                Valid = true;
            }
            catch (Exception)
            {
                Valid = false;
            }

            return Treasures;
        }

        public bool InitExternalList(MemoryHacker hacker)
        {
            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            Treasures = SetFromBytes(hacker.Game,bytes);
            Internal = false;
            return true;
        }

        public bool InitInternalList(GameNames game)
        {
            byte[] bytes = GetInternalBytes();
            if (bytes == null)
                return false;

            Treasures = SetFromBytes(game, bytes);
            Internal = true;
            return true;
        }

        public virtual string ItemName(int index) { return "Unknown"; }
        public virtual int TreasureCount { get { return 0; } }
        public virtual WizTreasure Treasure(int index) { return null; }

        public string GetFullDescription(int index)
        {
            StringBuilder sb = new StringBuilder();
            WizTreasure treasure = Treasure(index);
            sb.AppendFormat("#{0}{1}, ", index, treasure.Chest ? " (Chest)" : "");
            for (int iReward = 0; iReward < treasure.Rewards.Count; iReward++)
            {
                if (iReward >= treasure.NumRewards)
                    continue;
                WizTreasure.Reward reward = treasure.Rewards[iReward];
                if (reward.Chance == 0)
                    continue;
                sb.AppendFormat("{0}{1}", reward.Chance == 100 ? "" : String.Format("{0}%: ", reward.Chance), reward.IsItem ? "Item (" : "");
                if (reward.IsItem)
                {
                    WizTreasure.Reward.Artifact item = reward.Item;
                    if (item.Range == 0 || item.Range == 1)
                        sb.AppendFormat("{0})", ItemName(item.MinIndex));
                    else
                        sb.AppendFormat("#{0} to #{1})", item.MinIndex, item.Range + item.MinIndex);
                    if (item.ChanceBigger == 0 || item.MaxTimes == 0 || item.MFactor == 0)
                        sb.AppendLine();
                    else
                        sb.AppendFormat(" with {0} to add {1}\r\n", Global.Plural(item.MaxTimes, String.Format("{0}% chance", item.ChanceBigger + 1)), item.MFactor);
                }
                else
                    sb.AppendFormat("{0}-{1} Gold\r\n", reward.Money.Minimum, reward.Money.Maximum);
            }
            return Global.Trim(sb).ToString();
        }

        public string GetFullDescriptions()
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < TreasureCount; index++)
            {
                sb.Append(GetFullDescription(index));
                Global.Trim(sb).AppendLine();
            }
            return sb.ToString();
        }
    }
}
