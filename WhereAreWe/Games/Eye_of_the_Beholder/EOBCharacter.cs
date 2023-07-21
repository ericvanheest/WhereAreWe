using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WhereAreWe
{
    public class EOBCharacterOffsets : CharacterOffsets
    {
        public override int Condition => 1;
        public override int ConditionLength => 1;
        public override int Race => 31;
        public override int Class => 32;
        public override int Alignment => 33;
        public override int Stats => 13;
        public override int ArmorClass => 29;
        public override int CurrentHP => 27;
        public override int MaxHP => 28;
        public override int Inventory => 119;
        public override int InventoryLength => 54;
        public override int Experience => 39;
        public override int ExperienceLength => 12;
        public override int Level => 36;
        public override int Spells => 55;
        public override int SpellsLength => 64;
        public override int LevelLength => 1;
        public override int Food => 35;
        public override int Portrait => 34;
        public override int ItemUse => 30;
        public override int SpellTimeouts => 173;
        public override int ActiveSpells => 213;

        public override int Name => 2;
        public override int NameLength => 11;
    }

    [Flags]
    public enum EOBCondition
    {
        None =          0x00,
        Good =          0x01,
        InParty =       0x01,
        Poisoned =      0x02,
        Paralyzed =     0x04
    }

    public enum EOBRace
    {
        None = -1,
        HumanMale = 0,
        HumanFemale = 1,
        ElfMale = 2,
        ElfFemale = 3,
        HalfElfMale = 4,
        HalfElfFemale = 5,
        DwarfMale = 6,
        DwarfFemale = 7,
        GnomeMale = 8,
        GnomeFemale = 9,
        HalflingMale = 10,
        HalflingFemale = 11,
        Last
    }

    public enum EOBClass
    {
        None = -1,
        Fighter = 0,
        Ranger = 1,
        Paladin = 2,
        Mage = 3,
        Cleric = 4,
        Thief = 5,
        FighterCleric = 6,
        FighterThief = 7,
        FighterMage = 8,
        FighterMageThief = 9,
        ThiefMage = 10,
        ClericThief = 11,
        FighterClericMage = 12,
        RangerCleric = 13,
        ClericMage = 14,
        Last
    }

    public enum EOBAlignment
    {
        None = -1,
        LawfulGood = 0,
        NeutralGood = 1,
        ChaoticGood = 2,
        LawfulNeutral = 3,
        TrueNeutral = 4,
        ChaoticNeutral = 5,
        LawfulEvil = 6,
        NeutralEvil = 7,
        ChaoticEvil = 8,
        Last
    }

    public abstract class EOBStats
    {
        public OneByteStat Strength;
        public OneByteStat Strength18;
        public OneByteStat Intelligence;
        public OneByteStat Wisdom;
        public OneByteStat Dexterity;
        public OneByteStat Constitution;
        public OneByteStat Charisma;

        public string StrengthString { get { return new StrengthWith18(Strength, Strength18).ToString(); } }

        public int StrengthOffsetPerm { get { return 0; } }
        public int StrengthOffsetTemp { get { return StrengthOffsetPerm + 1; } }
        public int Strength18OffsetPerm { get { return 2; } }
        public int Strength18OffsetTemp { get { return Strength18OffsetPerm + 1; } }
        public int IntelligenceOffsetPerm { get { return 4; } }
        public int IntelligenceOffsetTemp { get { return IntelligenceOffsetPerm + 1; } }
        public int WisdomOffsetPerm { get { return 6; } }
        public int WisdomOffsetTemp { get { return WisdomOffsetPerm + 1; } }
        public int DexterityOffsetPerm { get { return 8; } }
        public int DexterityOffsetTemp { get { return DexterityOffsetPerm + 1; } }
        public int ConstitutionOffsetPerm { get { return 10; } }
        public int ConstitutionOffsetTemp { get { return ConstitutionOffsetPerm + 1; } }
        public int CharismaOffsetPerm { get { return 12; } }
        public int CharismaOffsetTemp { get { return CharismaOffsetPerm + 1; } }

        public void SetBytes(byte[] bytes, int offset)
        {
            Strength.SetBytes(bytes, offset + StrengthOffsetPerm);
            Strength18.SetBytes(bytes, offset + Strength18OffsetPerm);
            Intelligence.SetBytes(bytes, offset + IntelligenceOffsetPerm);
            Wisdom.SetBytes(bytes, offset + WisdomOffsetPerm);
            Dexterity.SetBytes(bytes, offset + DexterityOffsetPerm);
            Constitution.SetBytes(bytes, offset + ConstitutionOffsetPerm);
            Charisma.SetBytes(bytes, offset + CharismaOffsetPerm);
        }
    }

    public abstract class EOBInventory : Inventory
    {
        protected List<Item> m_items;

        public override List<Item> Items { get { return m_items; } set { m_items = value; } }

        public static EOBInventory Create(GameNames game, byte[] bytes, byte[] bytesItemTable, int offset = 0)
        {
            return new EOB1Inventory(bytes, bytesItemTable, offset);
        }

        public static EOBInventory Create(GameNames game, List<Item> items)
        {
            return new EOB1Inventory(items);
        }

        public EOBItem BestMeleeItem()
        {
            EOBItem itemBest = null;
            foreach (EOBItem item in Items)
            {
                if (item.WhereEquipped != EquipLocation.LeftHand && item.WhereEquipped != EquipLocation.RightHand)
                    continue;   // Don't include items in belt, quiver, etc.
                if (!item.IsMelee)
                    continue;
                if (itemBest == null)
                {
                    itemBest = item;
                    continue;
                }
                if (itemBest.BaseDamage.Average > item.BaseDamage.Average)
                    continue;
                itemBest = item;
            }
            return itemBest;
        }

        public override BasicDamage MeleeWeaponDamage
        {
            get
            {
                // Return the damage for whichever equipped melee weapon does the most average damage
                EOBItem itemBest = BestMeleeItem();
                return itemBest == null ? BasicDamage.Zero : itemBest.BaseDamage;
            }
        }

        public abstract byte[] GetBytes(GenericClass gc = GenericClass.None);
        public abstract void SetBytes(byte[] bytes, int offset, GenericClass gc = GenericClass.None);
        public abstract bool HasItem(GameNames game, int index, bool bEquippedOnly);
    }

    public class EOBSpellPoints : SpellPoints
    {
        private int NumericCount;
        private string FullString;

        public EOBSpellPoints(EOBCharacter eobChar)
        {
            FullString = eobChar.GetAvailableSpellString(out int iCount);
            NumericCount = iCount;
        }

        public override string Current => FullString;
        public override bool HasAnyCurrent => NumericCount > 0;
        public override string Maximum => "";

        public override string ToString() { return FullString; }
    }

    public abstract class EOBCharacter : EOBBaseCharacter
    {
        public string CharName;
        public EOBRace Race;
        public MMSex Sex;
        public EOBStats Stats;
        public EOBClass Class;
        public int[] Level;
        public long[] Experience;
        public int ArmorClass;
        public EOBCondition Condition;
        public EOBInventory Inventory;
        public byte ItemUseBits;
        public EOBAlignment Alignment;
        public byte Portrait;
        public byte Food;
        public byte[] Unknown33;
        public byte[] Unknown223;
        public EOBKnownSpells Spells;
        public int[] SpellExpirationTimes;
        public byte[] ActiveSpells;

        public virtual PermAndTemp HitPoints => null;

        public override StatAndModifier BasicStrength { get { return new StatAndModifier(Stats.Strength); } }
        public override StatAndModifier BasicStrength18 { get { return new StatAndModifier(Stats.Strength18); } }
        public override StatAndModifier BasicIntelligence { get { return new StatAndModifier(Stats.Intelligence); } }
        public override StatAndModifier BasicWisdom { get { return new StatAndModifier(Stats.Wisdom); } }
        public override StatAndModifier BasicDexterity { get { return new StatAndModifier(Stats.Dexterity); } }
        public override StatAndModifier BasicConstitution { get { return new StatAndModifier(Stats.Constitution); } }
        public override StatAndModifier BasicCharisma { get { return new StatAndModifier(Stats.Charisma); } }
        public override GenericClass BasicClass => GetBasicClass(Class);
        public override GenericAlignment BasicAlignment => new GenericAlignment(GetAlignment(Alignment));
        public override int BasicFood => Food;
        public override int BasicMaxFood => 100;

        protected GameNames m_game = GameNames.EyeOfTheBeholder1;

        public int Address = -1;

        public EOBCharacter()
        {
            Address = -1;
        }

        public override GameNames Game { get { return m_game; } }

        public string GetExtraConditionDesc()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full: Character may not hold any more items\r\n");
            if (Food < 1)
                sb.AppendFormat("Hungry: Character will not regain HP or spells when resting\r\n");
            if (HitPoints.Temporary < -9)
                sb.AppendFormat("Dead: Character must cannot perform actions\r\n");
            else if (HitPoints.Temporary < 1)
                sb.AppendFormat("Unconscious: Character must cannot perform actions\r\n");
            return sb.ToString().Trim();
        }

        public string GetExtraConditions()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full, ");
            if (Food < 1)
                sb.AppendFormat("Hungry, ");
            if (HitPoints.Temporary < -9)
                sb.AppendFormat("Dead, ");
            else if (HitPoints.Temporary < 1)
                sb.AppendFormat("Unconscious, ");
            return Global.Trim(sb).ToString();
        }

        public override int BasicAddress { get { return Address; } }
        public abstract CheatOffsets GetInventoryCheatOffsets(int iIndex);

        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public override MMSex BasicSex
        {
            get
            {
                switch (Race)
                {
                    case EOBRace.DwarfFemale:
                    case EOBRace.ElfFemale:
                    case EOBRace.GnomeFemale:
                    case EOBRace.HalfElfFemale:
                    case EOBRace.HalflingFemale:
                    case EOBRace.HumanFemale:
                        return MMSex.Female;
                    default:
                        return MMSex.Male;
                }
            }
        }

        public static bool IsThief(GenericClass cl)
        {
            switch (cl)
            {
                case GenericClass.Thief:
                case GenericClass.FighterMageThief:
                case GenericClass.FighterThief:
                case GenericClass.ThiefMage:
                    return true;
                default:
                    return false;
            }
        }

        public static EOBCharacter Create(GameNames game, byte[] itemTable, int iCharIndex = -1, byte[] bytes = null, int iIndex = -1)
        {
            EOBCharacter eobChar = null;
            switch (game)
            {
                case GameNames.EyeOfTheBeholder1:
                    eobChar = new EOB1Character();
                    break;
                case GameNames.EyeOfTheBeholder2:
                    eobChar = new EOB2Character();
                    break;
            }

            if (bytes != null)
                eobChar.SetFromBytes(game, itemTable, iCharIndex, bytes, iIndex);
            return eobChar;
        }

        public void SetFromBytes(GameNames game, byte[] itemTable, int iCharIndex, byte[] bytes, int iIndex)
        {
            m_game = game;
            Address = -1;
            if (bytes == null || bytes.Length < iIndex + CharacterSize - 1)
                return;
            SetCharFromStream(iCharIndex, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null, null, false, itemTable);
        }

        public abstract EOBStats CreateStats(byte[] bytes, int offset);
        public override int MaxBackpackSize { get { return 14; } }

        public override string Name { get { return CharName; } }
        public override List<Item> BackpackItems { get { return Inventory.SelectUnequippedItems; } }

        public static string RaceString(EOBRace race)
        {
            switch (race)
            {
                case EOBRace.None: return "None";
                case EOBRace.HumanMale: return "Human Male";
                case EOBRace.HumanFemale: return "Human Female";
                case EOBRace.ElfMale: return "Elf Male";
                case EOBRace.ElfFemale: return "Elf Female";
                case EOBRace.HalfElfMale: return "Half-Elf Male";
                case EOBRace.HalfElfFemale: return "Half-Elf Female";
                case EOBRace.DwarfMale: return "Dwarf Male";
                case EOBRace.DwarfFemale: return "Dwarf Female";
                case EOBRace.GnomeMale: return "Gnome Male";
                case EOBRace.GnomeFemale: return "Gnome Female";
                case EOBRace.HalflingMale: return "Halfling Male";
                case EOBRace.HalflingFemale: return "Halfling Female";
                default: return String.Format("Unknown({0})", (int)race);
            }
        }

        public static byte GetRaceByte(GenericRace race, MMSex sex)
        {
            switch (race)
            {
                case GenericRace.Human: return sex == MMSex.Male ? (byte) EOBRace.HumanMale : (byte) EOBRace.HumanFemale;
                case GenericRace.Elf: return sex == MMSex.Male ? (byte)EOBRace.ElfMale : (byte)EOBRace.ElfFemale;
                case GenericRace.HalfElf: return sex == MMSex.Male ? (byte)EOBRace.HalfElfMale : (byte)EOBRace.HalfElfFemale;
                case GenericRace.Dwarf: return sex == MMSex.Male ? (byte)EOBRace.DwarfMale : (byte)EOBRace.DwarfFemale;
                case GenericRace.Gnome: return sex == MMSex.Male ? (byte)EOBRace.GnomeMale : (byte)EOBRace.GnomeFemale;
                case GenericRace.Halfling: return sex == MMSex.Male ? (byte)EOBRace.HalflingMale : (byte)EOBRace.HalflingFemale;
                default: return 0;
            }
        }

        public static string ClassString(EOBClass classenum)
        {
            switch (classenum)
            {
                case EOBClass.Fighter: return "Fighter";
                case EOBClass.Paladin: return "Paladin";
                case EOBClass.Ranger: return "Ranger";
                case EOBClass.Mage: return "Mage";
                case EOBClass.Cleric: return "Cleric";
                case EOBClass.Thief: return "Thief";
                case EOBClass.FighterCleric: return "Fighter/Cleric";
                case EOBClass.FighterThief: return "Fighter/Thief";
                case EOBClass.FighterMage: return "Fighter/Mage";
                case EOBClass.FighterMageThief: return "Fighter/Mage/Thief";
                case EOBClass.ThiefMage: return "Thief/Mage";
                case EOBClass.ClericThief: return "Cleric/Thief";
                case EOBClass.FighterClericMage: return "Fighter/Cleric/Mage";
                case EOBClass.RangerCleric: return "Ranger/Cleric";
                case EOBClass.ClericMage: return "Cleric/Mage";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.LawfulGood: return (byte)EOBAlignment.LawfulGood;
                case GenericAlignmentValue.NeutralGood: return (byte)EOBAlignment.NeutralGood;
                case GenericAlignmentValue.ChaoticGood: return (byte)EOBAlignment.ChaoticGood;
                case GenericAlignmentValue.LawfulNeutral: return (byte)EOBAlignment.LawfulNeutral;
                case GenericAlignmentValue.TrueNeutral: return (byte)EOBAlignment.TrueNeutral;
                case GenericAlignmentValue.ChaoticNeutral: return (byte)EOBAlignment.ChaoticNeutral;
                case GenericAlignmentValue.LawfulEvil: return (byte)EOBAlignment.LawfulEvil;
                case GenericAlignmentValue.NeutralEvil: return (byte)EOBAlignment.NeutralEvil;
                case GenericAlignmentValue.ChaoticEvil: return (byte)EOBAlignment.ChaoticEvil;
                default: return 0;
            }
        }

        public static GenericAlignmentValue GetAlignment(EOBAlignment align)
        {
            switch (align)
            {
                case EOBAlignment.LawfulGood: return GenericAlignmentValue.LawfulGood;
                case EOBAlignment.NeutralGood: return GenericAlignmentValue.NeutralGood;
                case EOBAlignment.ChaoticGood: return GenericAlignmentValue.ChaoticGood;
                case EOBAlignment.LawfulNeutral: return GenericAlignmentValue.LawfulNeutral;
                case EOBAlignment.TrueNeutral: return GenericAlignmentValue.TrueNeutral;
                case EOBAlignment.ChaoticNeutral: return GenericAlignmentValue.ChaoticNeutral;
                case EOBAlignment.LawfulEvil: return GenericAlignmentValue.LawfulEvil;
                case EOBAlignment.NeutralEvil: return GenericAlignmentValue.NeutralEvil;
                case EOBAlignment.ChaoticEvil: return GenericAlignmentValue.ChaoticEvil;
                default: return GenericAlignmentValue.None;
            }
        }

        public static string AlignmentString(EOBAlignment align) { return AlignmentString(GetAlignment(align)); }

        public static string ConditionString(EOBCondition cond) { return ConditionString(cond, String.Empty, true); }

        public static string ConditionString(EOBCondition cond, string strExtra, bool bIncludeGood)
        {
            if (cond  == EOBCondition.Good)
            {
                if (!String.IsNullOrWhiteSpace(strExtra))
                    return strExtra;
                return bIncludeGood ? "Good" : "";
            }

            if (!String.IsNullOrWhiteSpace(strExtra))
                strExtra = ", " + strExtra;

            StringBuilder sb = new StringBuilder();

            if (cond.HasFlag(EOBCondition.Paralyzed))
                sb.Append("Paralyzed, ");
            if (cond.HasFlag(EOBCondition.Poisoned))
                sb.Append("Poison, ");
            Global.Trim(Global.Trim(sb).Append(strExtra));

            if (sb.Length > 2 && sb[0] == ',')
                sb.Remove(0, 2);
            return sb.ToString();
        }

        public static string ConditionDescription(EOBCondition cond, EOBCharacter eobChar = null)
        {
            string strExtra = eobChar == null ? String.Empty : eobChar.GetExtraConditionDesc();
            if (String.IsNullOrWhiteSpace(strExtra))
                return "Good: Character is healthy";

            StringBuilder sb = new StringBuilder();
            if (cond.HasFlag(EOBCondition.Paralyzed))
                sb.AppendLine("Paralyzed: Cannot perform actions");
            if (cond.HasFlag(EOBCondition.Poisoned))
                sb.AppendLine("Poisoned: -5 HP every 10 seconds");
            if (!String.IsNullOrWhiteSpace(strExtra))
                sb.Append(strExtra);

            return sb.ToString();
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Str:{0}, Int:{1}, Wis: {2}, Dex:{3}, Con:{4}, Cha:{5}",
                    Stats.Strength.ToString(),
                    Stats.Intelligence.ToString(),
                    Stats.Wisdom.ToString(),
                    Stats.Dexterity.ToString(),
                    Stats.Constitution.ToString(),
                    Stats.Charisma.ToString());
            }
        }

        protected string LevelString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (int i in Level)
                {
                    if (i > 0)
                        sb.AppendFormat("{0}/", i);
                }
                if (sb.Length > 1)
                    sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3}",
                 LevelString,
                 EOBCharacter.AlignmentString(Alignment),
                 EOBCharacter.RaceString(Race),
                 EOBCharacter.ClassString(Class));
            }
        }

        static int[][] ClericSpells = new int[][] {
            new int[] { 0 },
            new int[] { 0, 1, 2, 2, 3, 3, 3, 3, 3, 4, 4, 5, 6, 6, 6, 6, 7, 7, 8, 9, 9 },
            new int[] { 0, 0, 0, 1, 2, 3, 3, 3, 3, 4, 4, 4, 5, 6, 6, 6, 7, 7, 8, 9, 9 },
            new int[] { 0, 0, 0, 0, 0, 1, 2, 2, 3, 3, 3, 4, 5, 6, 6, 6, 7, 7, 8, 9, 9 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 3, 3, 3, 4, 5, 6, 6, 7, 8, 8, 8 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 3, 4, 4, 5, 6, 6, 7 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 3, 3, 4, 4, 5 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 2 }
        };

        static int[][] ClericBonusSpells = new int[][] {
            // Starts at Wisdom=13
            new int[] { 0 },
            new int[] { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 },
            new int[] { 0, 0, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 1, 2, 3, 3, 4, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 4, 4, 4 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        };

        static int[][] PaladinSpells = new int[][] {
            new int[] { 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 3 },
            new int[] { 0 },
            new int[] { 0 },
            new int[] { 0 }
        };

        static int[][] MageSpells = new int[][] {
            new int[] { 0 },
            new int[] { 0, 1, 2, 2, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5 },
            new int[] { 0, 0, 0, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5 },
            new int[] { 0, 0, 0, 0, 0, 1, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 3, 4, 4, 4, 5, 5, 5, 5, 5, 5 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 4, 4, 5, 5, 5, 5, 5, 5 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 3, 3, 3, 3, 4 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 3, 3, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 3, 3 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2 },
        };

        public static int GetClericSpellPoints(int iLevel, int iWisdom) { return GetClericSpells(iLevel, iWisdom).Sum(); }
        public static string GetClericSpellString(int iLevel, int iWisdom) { return GetClericSpellString(iLevel, iWisdom, out int iCount); }
        public static string GetClericSpellString(int iLevel, int iWisdom, out int iCount) { return SpellString(GetClericSpells(iLevel, iWisdom), out iCount); }

        public static int GetMageSpellPoints(int iLevel, int iWisdom) { return GetMageSpells(iLevel, iWisdom).Sum(); }
        public static string GetMageSpellString(int iLevel, int iWisdom) { return GetMageSpellString(iLevel, iWisdom, out int iCount); }
        public static string GetMageSpellString(int iLevel, int iWisdom, out int iCount) { return SpellString(GetMageSpells(iLevel, iWisdom), out iCount); }

        public static int GetPaladinSpellPoints(int iLevel) { return GetPaladinSpells(iLevel).Sum(); }
        public static string GetPaladinSpellString(int iLevel) { return GetPaladinSpellString(iLevel, out int iCount); }
        public static string GetPaladinSpellString(int iLevel, out int iCount) { return SpellString(GetPaladinSpells(iLevel), out iCount); }

        public static string SpellString(int[] spells, out int iCount)
        {
            iCount = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < spells.Length; i++)
            {
                if (spells[i] == 0)
                    break;
                sb.AppendFormat("{0}/", spells[i]);
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            else
                return "0";
            return sb.ToString();
        }

        public static int[] GetClericSpells(int iLevel, int iWisdom)
        {
            int[] spells = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (iLevel < 0) return spells;

            for (int i = 1; i < 8; i++)
            {
                int iSpells = 0;
                if (iLevel >= ClericSpells[i].Length)
                    iSpells = ClericSpells[i][ClericSpells[i].Length - 1];
                else
                    iSpells = ClericSpells[i][iLevel];
                if (iSpells > 0 && iWisdom >= 13)
                {
                    if ((iWisdom - 13) >= ClericBonusSpells[i].Length)
                        iSpells += ClericBonusSpells[i][ClericBonusSpells[i].Length - 1];
                    else
                        iSpells += ClericBonusSpells[i][iWisdom - 13];
                }
                spells[i] = iSpells;
            }
            return spells;
        }

        public static int[] GetClericBonusSpells(int iWisdom)
        {
            int[] bonus = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (iWisdom < 13)
                return bonus;
            iWisdom -= 13;
            if (iWisdom >= ClericBonusSpells[1].Length)
                iWisdom = ClericBonusSpells[1].Length - 1;
            for (int i = 1; i < 8; i++)
                bonus[i] = ClericBonusSpells[i][iWisdom];
            return bonus;
        }

        public static int[] GetPaladinSpells(int iLevel)
        {
            int[] spells = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (iLevel < 9) return spells;
            for (int i = 1; i < 8; i++)
            {
                int iSpells = 0;
                if (iLevel >= ClericSpells[i].Length)
                    iSpells = ClericSpells[i][ClericSpells[i].Length - 1];
                else
                    iSpells = ClericSpells[i][iLevel];
                spells[i] = iSpells;
            }
            return spells;
        }

        public static int[] GetMageSpells(int iLevel, int iIntelligence)
        {
            int[] spells = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (iLevel < 0) return spells;
            for (int i = 1; i < 10; i++)
            {
                int iSpells = 0;
                if (iLevel >= MageSpells[i].Length)
                    iSpells = MageSpells[i][MageSpells[i].Length - 1];
                else
                    iSpells = MageSpells[i][iLevel];
                spells[i] = iSpells;
            }
            return spells;
        }

        public string GetAvailableSpellString()
        {
            return GetAvailableSpellString(out int iCount);
        }

        public override int[] GetSpells(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Cleric:
                    switch (Class)
                    {
                        case EOBClass.Cleric:
                        case EOBClass.ClericMage:
                        case EOBClass.ClericThief:
                            return GetClericSpells(Level[0], Stats.Wisdom.Permanent);
                        case EOBClass.FighterCleric:
                        case EOBClass.FighterClericMage:
                        case EOBClass.RangerCleric:
                            return GetClericSpells(Level[1], Stats.Wisdom.Permanent);
                        case EOBClass.Paladin:  // Can't multi-class a paladin
                            return GetPaladinSpells(Level[0]);
                        default:
                            return new int[0];
                    }
                case GenericClass.Mage:
                    switch (Class)
                    {
                        case EOBClass.Mage:
                            return GetMageSpells(Level[0], Stats.Intelligence.Permanent);
                        case EOBClass.ClericMage:
                        case EOBClass.FighterMage:
                        case EOBClass.FighterMageThief:
                        case EOBClass.ThiefMage:
                            return GetMageSpells(Level[1], Stats.Intelligence.Permanent);
                        case EOBClass.FighterClericMage:
                            return GetMageSpells(Level[2], Stats.Intelligence.Permanent);
                        default: return new int[0];
                    }
                default: return new int[0];
            }
        }

        public string GetAvailableSpellString(out int iCount)
        {
            iCount = 0;
            int iTemp = 0;
            StringBuilder sb = new StringBuilder();
            int[] cleric = GetSpells(GenericClass.Cleric);
            int[] mage = GetSpells(GenericClass.Mage);

            if (cleric.Any(s => s > 0))
                sb.AppendFormat("{0}, ", SpellString(cleric, out iTemp));
            iCount += iTemp;
            if (mage.Any(s => s > 0))
                sb.AppendFormat("{0}, ", SpellString(mage, out iTemp));
            iCount += iTemp;

            if (sb.Length == 0)
                return "0";
            return Global.Trim(sb).ToString();
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(CharName);
                sb.AppendLine(BasicInfoString);
                sb.AppendLine(AttributesString);
                sb.AppendFormat("Experience: {0}\r\n", ExperienceString);
                sb.AppendFormat("Condition: {0}\r\n", EOBCharacter.ConditionString(Condition, GetExtraConditions(), true));
                sb.AppendFormat("HP: {0}\r\n", HitPoints.ToString());
                sb.AppendFormat("AC: {0}\r\n", ArmorClass);
                sb.AppendFormat("Melee: {0}\r\n", MeleeDamageString);
                sb.AppendFormat("Equipped: {0}\r\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\r\n", BackpackString);
                return sb.ToString();
            }
        }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level.Max(), 0); } }
        public override string BasicLevelString { get { return LevelString; } }
        public override StatAndModifier BasicAC { get { return new StatAndModifier(ArmorClass, 0); } }
        public override OneByteStat QuickRefSpellLevel { get { return new OneByteStat(99, 99); } }
        public override SpellPoints QuickRefSpellPoints { get { return new EOBSpellPoints(this); } }

        public static GenericRace GetBasicRace(EOBRace race)
        {
            switch (race)
            {
                case EOBRace.HumanMale:
                case EOBRace.HumanFemale: return GenericRace.Human;
                case EOBRace.ElfMale:
                case EOBRace.ElfFemale: return GenericRace.Elf;
                case EOBRace.HalfElfMale:
                case EOBRace.HalfElfFemale: return GenericRace.HalfElf;
                case EOBRace.DwarfMale:
                case EOBRace.DwarfFemale: return GenericRace.Dwarf;
                case EOBRace.GnomeMale:
                case EOBRace.GnomeFemale: return GenericRace.Gnome;
                case EOBRace.HalflingMale:
                case EOBRace.HalflingFemale: return GenericRace.Halfling;
                default: return GenericRace.None;
            }
        }

        public override GenericRace BasicRace { get { return GetBasicRace(Race); } }
        public override bool IsCaster
        {
            get
            {
                switch (Class)
                {
                    case EOBClass.Fighter:
                    case EOBClass.Thief:
                    case EOBClass.FighterThief:
                    case EOBClass.Ranger:
                    case EOBClass.None:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public override long QuickRefExperience { get { return Experience.Max(); } }
        public override MMHitPoints QuickRefHitPoints { get { return new MMHitPoints(HitPoints.Temporary, HitPoints.Permanent); } }
        public override string QuickRefCondition { get { return EOBCharacter.ConditionString(Condition, GetExtraConditions(), false); } }

        public override BasicConditionFlags BasicCondition { get { return GetBasicCondition(Condition); } }

        public static BasicConditionFlags GetBasicCondition(EOBCondition eobCondition)
        {
            BasicConditionFlags cond = BasicConditionFlags.Good;
            if (eobCondition.HasFlag(EOBCondition.Poisoned))
                cond |= BasicConditionFlags.Poisoned;
            if (eobCondition.HasFlag(EOBCondition.Paralyzed))
                cond |= BasicConditionFlags.Paralyzed;
            return cond;
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            byte b = 1;
            if (condition.HasFlag(BasicConditionFlags.Poisoned))
                b |= (byte)EOBCondition.Poisoned;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                b |= (byte)EOBCondition.Paralyzed;
            return b;
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)EOBRace.HumanMale;
                case GenericRace.Elf: return (byte)EOBRace.ElfMale;
                case GenericRace.Dwarf: return (byte)EOBRace.DwarfMale;
                case GenericRace.Gnome: return (byte)EOBRace.GnomeMale;
                case GenericRace.HalfElf: return (byte)EOBRace.HalfElfMale;
                case GenericRace.Halfling: return (byte)EOBRace.HalflingMale;
                default: return 0;
            }
        }

        public static EOBClass ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Fighter: return EOBClass.Fighter;
                case GenericClass.Ranger: return EOBClass.Ranger;
                case GenericClass.Paladin: return EOBClass.Paladin;
                case GenericClass.Mage: return EOBClass.Mage;
                case GenericClass.Cleric: return EOBClass.Cleric;
                case GenericClass.Thief: return EOBClass.Thief;
                case GenericClass.FighterCleric: return EOBClass.FighterCleric;
                case GenericClass.FighterThief: return EOBClass.FighterThief;
                case GenericClass.FighterMage: return EOBClass.FighterMage;
                case GenericClass.FighterMageThief: return EOBClass.FighterMageThief;
                case GenericClass.ThiefMage: return EOBClass.ThiefMage;
                case GenericClass.FighterClericMage: return EOBClass.FighterClericMage;
                case GenericClass.RangerCleric: return EOBClass.RangerCleric;
                case GenericClass.ClericMage: return EOBClass.ClericMage;
                default: return EOBClass.None;
            }
        }

        public static GenericClass GetBasicClass(EOBClass eobClass)
        {
            switch (eobClass)
            {
                case EOBClass.Fighter: return GenericClass.Fighter;
                case EOBClass.Paladin: return GenericClass.Paladin;
                case EOBClass.Ranger: return GenericClass.Ranger;
                case EOBClass.Mage: return GenericClass.Mage;
                case EOBClass.Cleric: return GenericClass.Cleric;
                case EOBClass.Thief: return GenericClass.Thief;
                case EOBClass.FighterCleric: return GenericClass.FighterCleric;
                case EOBClass.FighterThief: return GenericClass.FighterThief;
                case EOBClass.FighterMage: return GenericClass.FighterMage;
                case EOBClass.FighterMageThief: return GenericClass.FighterMageThief;
                case EOBClass.ThiefMage: return GenericClass.ThiefMage;
                case EOBClass.FighterClericMage: return GenericClass.FighterClericMage;
                case EOBClass.RangerCleric: return GenericClass.RangerCleric;
                case EOBClass.ClericMage: return GenericClass.ClericMage;
                default: return GenericClass.None;
            }
        }

        public static EOBClass[] SeparateClasses(EOBClass multiClass)
        {
            switch (multiClass)
            {
                case EOBClass.Fighter: return new EOBClass[] { EOBClass.Fighter };
                case EOBClass.Ranger: return new EOBClass[] { EOBClass.Ranger };
                case EOBClass.Mage: return new EOBClass[] { EOBClass.Mage };
                case EOBClass.Cleric: return new EOBClass[] { EOBClass.Cleric };
                case EOBClass.Paladin: return new EOBClass[] { EOBClass.Paladin };
                case EOBClass.Thief: return new EOBClass[] { EOBClass.Thief };
                case EOBClass.FighterCleric: return new EOBClass[] { EOBClass.Fighter, EOBClass.Cleric };
                case EOBClass.FighterThief: return new EOBClass[] { EOBClass.Fighter, EOBClass.Thief };
                case EOBClass.FighterMage: return new EOBClass[] { EOBClass.Fighter, EOBClass.Mage };
                case EOBClass.FighterMageThief: return new EOBClass[] { EOBClass.Fighter, EOBClass.Mage, EOBClass.Thief };
                case EOBClass.ThiefMage: return new EOBClass[] { EOBClass.Thief, EOBClass.Mage };
                case EOBClass.ClericThief: return new EOBClass[] { EOBClass.Cleric, EOBClass.Thief };
                case EOBClass.FighterClericMage: return new EOBClass[] { EOBClass.Fighter, EOBClass.Cleric, EOBClass.Mage };
                case EOBClass.RangerCleric: return new EOBClass[] { EOBClass.Ranger, EOBClass.Cleric };
                case EOBClass.ClericMage: return new EOBClass[] { EOBClass.Cleric, EOBClass.Mage };
                default: return new EOBClass[] { EOBClass.None };
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                bool[] items = new bool[14];
                for (int i = 0; i < items.Length; i++)
                    items[i] = false;
                foreach(Item item in Inventory.Items)
                    if (item.MemoryIndex > 1 && item.MemoryIndex < 16) // 0 and 1 are right hand and left hand
                        items[item.MemoryIndex - 2] = true;
                for (int i = 0; i < items.Length; i++)
                    if (!items[i])
                        return i;
                return -1;
            }
        }

        public override bool BackpackFull { get { return (FirstEmptyBackpackIndex == -1); } }

        public virtual int GetMaxLevel(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Cleric: return 10;
                default: return MaxLevel;
            }
        }

        public override string ExperienceString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                EOBClass[] classes = SeparateClasses(Class);
                for (int i = 0; i < classes.Length; i++)
                {
                    sb.AppendFormat("{0}/{1}, ", Experience[i], Level[i] >= GetMaxLevel(GetBasicClass(classes[i])) ? "Max" : GetXPForNextLevel(Experience[i], classes[i]).ToString());
                }
                return Global.Trim(sb).ToString();
            }
        }

        public override long NeedsXP
        {
            get
            {
                // Return the XP necessary for the class nearest a level-up
                EOBClass[] classes = SeparateClasses(Class);
                long[] needsExp = new long[classes.Length];
                for (int i = 0; i < classes.Length; i++)
                {
                    needsExp[i] = XPForLevel(GetBasicClass(classes[i]), Level[i] + 1) - Experience[i];
                }
                return needsExp.Min();
            }
        }

        public override long XPForNextLevel
        {
            get
            {
                // Return the XP necessary for the class nearest a level-up
                EOBClass[] classes = SeparateClasses(Class);
                long[] nextExp = new long[classes.Length];
                for (int i = 0; i < classes.Length; i++)
                {
                    nextExp[i] = XPForLevel(GetBasicClass(classes[i]), Level[i] + 1);
                }
                return nextExp.Min();
            }
        }

        public long GetXPForNextLevel(long iCurrentExp, EOBClass singleClass)
        {
            EOBClass[] classes = SeparateClasses(Class);
            for (int i = 0; i < classes.Length; i++)
            {
                if (classes[i] == singleClass)
                    return XPForLevel(GetBasicClass(singleClass), Level[i] + 1);
            }
            return -1;
        }

        public override long BasicExperience { get { return Experience.Max(); } }

        public override long XPForLevel(GenericClass gc, int iLevel)
        {
            return XPForLevel(Game, gc, iLevel);
        }

        public static int[] XPEOB1Cleric = new int[] { 1501, 3001, 6001, 13001, 27501, 55001, 110001, 225001, 450001 };
        public static int[] XPEOB1Fighter = new int[] { 2001, 4001, 8001, 16001, 32001, 64001, 125001, 250001, 500001, 750001, 1000001 };
        public static int[] XPEOB1Mage = new int[] { 2501, 5001, 10001, 20001, 40001, 60001, 90001, 135001, 250001, 375001 };
        public static int[] XPEOB1Paladin = new int[] { 2251, 4501, 9001, 18001, 36001, 75001, 150001, 300001, 600001, 900001 };
        public static int[] XPEOB1Thief = new int[] { 1251, 2501, 5001, 10001, 20001, 40001, 70001, 110001, 160001, 220001, 440001 };
        public static int[] XPEOB1Ranger = new int[] { 2251, 4501, 9001, 18001, 36001, 75001, 150001, 300001, 600001, 900001, 1200001 };

        public static long XPForLevel(GameNames game, GenericClass gClass, int iLevel)
        {
            switch (gClass)
            {
                case GenericClass.Fighter: return XPForLevel(XPEOB1Fighter, iLevel);
                case GenericClass.Cleric: return XPForLevel(XPEOB1Cleric, iLevel);
                case GenericClass.Mage: return XPForLevel(XPEOB1Mage, iLevel);
                case GenericClass.Paladin: return XPForLevel(XPEOB1Paladin, iLevel);
                case GenericClass.Ranger: return XPForLevel(XPEOB1Ranger, iLevel);
                case GenericClass.Thief: return XPForLevel(XPEOB1Thief, iLevel);
                default: return 0;
            }
        }

        public static long XPForLevel(int[] expLevels, int iLevel)
        {
            if (iLevel - 2 < 0)
                return 0;
            int iLen = expLevels.Length;
            if (iLevel - 2 < iLen)
                return expLevels[iLevel - 2];
            return expLevels[iLen - 1] * (iLevel - iLen - 1);
        }

        public static StatModifier GetMeleeHitModifier(StrengthWith18 s18)
        {
            StatModifier sm = StatModifier.FromSubStat(s18.Strength, s18.Strength18, -3, 4);
            if (s18.Strength < 6) sm.SetValueNext(-2, 6);
            else if (s18.Strength < 8) sm.SetValueNext(-1, 8);
            else if (s18.Strength < 17) sm.SetValueNext(0, 17);
            else if (s18.Strength < 18) sm.SetValueNext(1, 19);
            else if (s18.Strength == 18)
            {
                if (s18.Strength18 < 51) sm.SetValueNext(1, 19);
                else if (s18.Strength18 < 100) sm.SetValueNext(2, 19);
                else sm.SetValueNext(3, 21);
            }
            else if (s18.Strength < 21) sm.SetValueNext(3, 21);
            else sm.SetValueNext(4, -1);
            return sm;
        }

        public static StatModifier GetMissileHitModifier(int iDexterity)
        {
            return StatModifier.FromTable(iDexterity, PrimaryStat.Dexterity, 4, -3, 5, -2, 6, -1, 16, 0, 17, 1, 19, 2, 3);
        }

        public static StatModifier GetACModifier(int iDexterity)
        {
            return StatModifier.FromTable(iDexterity, PrimaryStat.Dexterity, 4, 4, 5, 3, 6, 2, 7, 1, 15, 0, 16, -1, 17, -2, 18, -3, 21, -4, 24, -6, -7);
        }

        public static StatModifier GetMeleeDamageModifier(StrengthWith18 s18)
        {
            StatModifier sm = StatModifier.FromSubStat(s18.Strength, s18.Strength18, -1, 6);
            if (s18.Strength < 16) sm.SetValueNext(0, 16);
            else if (s18.Strength < 18) sm.SetValueNext(1, 18);
            else if (s18.Strength < 17) sm.SetValueNext(0, 17);
            else if (s18.Strength == 18)
            {
                if (s18.Strength18 == 0) sm.SetValueNext(2, 19);
                else if (s18.Strength18 < 76) sm.SetValueNext(3, 19);
                else if (s18.Strength18 < 91) sm.SetValueNext(4, 19);
                else if (s18.Strength18 < 100) sm.SetValueNext(5, 19);
                else sm.SetValueNext(6, 19);
            }
            else if (s18.Strength < 20) sm.SetValueNext(7, 21);
            else if (s18.Strength < 21) sm.SetValueNext(8, 21);
            else if (s18.Strength < 22) sm.SetValueNext(9, 21);
            else sm.SetValueNext(10, -1);
            return sm;
        }

        public static StatModifier GetConstitutionModifier(int iCon, GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Fighter:
                case GenericClass.Paladin:
                case GenericClass.Ranger:
                    return StatModifier.FromTable(iCon, PrimaryStat.Constitution, 4, -2, 7, -1, 15, 0, 16, 1, 17, 2, 18, 3, 19, 4, 21, 5, 24, 6, 7);
                default:
                    return StatModifier.FromTable(iCon, PrimaryStat.Constitution, 4, -2, 7, -1, 15, 0, 16, 1, 2);
            }
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat, int valueSecondary, PrimaryStat statSecondary, GenericClass gc)
        {
            switch (stat)
            {
                case PrimaryStat.Strength: return GetMeleeDamageModifier(new StrengthWith18(value, valueSecondary));
                case PrimaryStat.Dexterity: return GetACModifier(value);
                case PrimaryStat.Constitution: return GetConstitutionModifier(value, gc);
                default: return StatModifier.Zero;
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (spell == null)
                return false;
            return false;
        }

        public override ResistanceValue SaveVsParalyzePoisonDeath { get { return GetResistances()[0]; } }
        public override ResistanceValue SaveVsWand { get { return GetResistances()[1]; } }
        public override ResistanceValue SaveVsPetfifyPolymorph { get { return GetResistances()[2]; } }
        public override ResistanceValue SaveVsBreath { get { return GetResistances()[3]; } }
        public override ResistanceValue SaveVsSpell { get { return GetResistances()[4]; } }

        public override ResistanceValue[] GetResistances()
        {
            // Resistances in Eye of the Beholder are Saving Throws
            // For a multiclass character, the saving throws used are for whatever class/level combination is best
            string[] reasons = new string[5];
            EOBClass[] classes = SeparateClasses(Class);
            int[] best = new int[] { 20, 20, 20, 20, 20 };
            for (int i = 0; i < classes.Length; i++)
            {
                int[] throws = GetSavingThrows(GetBasicClass(classes[i]), Level[i]);
                for (int j = 0; j < best.Length; j++)
                {
                    if (throws[j] < best[j])
                    {
                        best[j] = throws[j];
                        reasons[j] = String.Format("Level {0} {1}", Level[i], ClassString(classes[i]));
                    }
                }
            }
            return new ResistanceValue[]
            {
                new ResistanceValue(GenericResistanceFlags.SaveVsPoison, best[0], reasons[0]),
                new ResistanceValue(GenericResistanceFlags.SaveVsWand, best[1], reasons[1]),
                new ResistanceValue(GenericResistanceFlags.SaveVsPetrify, best[2], reasons[2]),
                new ResistanceValue(GenericResistanceFlags.SaveVsBreath, best[3], reasons[3]),
                new ResistanceValue(GenericResistanceFlags.SaveVsSpell, best[4], reasons[4])
            };

        }

        public static int[] GetSavingThrows(GenericClass gc, int iLevel)
        {
            switch (gc)
            {
                case GenericClass.Cleric:
                    if (iLevel < 3) return new int[] { 10, 14, 13, 16, 15 };
                    if (iLevel < 7) return new int[] { 9, 13, 12, 15, 14 };
                    if (iLevel < 10) return new int[] { 7, 11, 10, 13, 12 };
                    if (iLevel < 13) return new int[] { 6, 10, 9, 12, 11 };
                    if (iLevel < 16) return new int[] { 5, 9, 8, 11, 10 };
                    if (iLevel < 19) return new int[] { 4, 8, 7, 10, 9 };
                    return new int[] { 2, 6, 5, 8, 7 };
                case GenericClass.Thief:
                    if (iLevel < 5) return new int[] { 13, 14, 12, 16, 5 };
                    if (iLevel < 9) return new int[] { 12, 12, 11, 15, 13 };
                    if (iLevel < 13) return new int[] { 11, 10, 10, 14, 11 };
                    if (iLevel < 17) return new int[] { 10, 8, 9, 13, 9 };
                    if (iLevel < 21) return new int[] { 9, 6, 8, 12, 7 };
                    return new int[] { 8, 4, 7, 11, 5 };
                case GenericClass.Mage:
                    if (iLevel < 6) return new int[] { 14, 11, 13, 15, 12 };
                    if (iLevel < 11) return new int[] { 13, 9, 11, 13, 10 };
                    if (iLevel < 16) return new int[] { 11, 7, 9, 11, 8 };
                    if (iLevel < 21) return new int[] { 10, 5, 7, 9, 6 };
                    return new int[] { 8, 3, 5, 7, 4 };
                default:    // Fighters, Paladins, etc.
                    if (iLevel < 3) return new int[] { 14, 16, 15, 17, 17 };
                    if (iLevel < 5) return new int[] { 13, 15, 14, 16, 16 };
                    if (iLevel < 7) return new int[] { 11, 13, 12, 13, 14 };
                    if (iLevel < 9) return new int[] { 10, 12, 11, 12, 13 };
                    if (iLevel < 11) return new int[] { 8, 10, 9, 9, 11 };
                    if (iLevel < 13) return new int[] { 7, 9, 8, 8, 10 };
                    if (iLevel < 15) return new int[] { 5, 7, 6, 5, 8 };
                    if (iLevel < 17) return new int[] { 4, 6, 5, 4, 7 };
                    return new int[] { 3, 5, 4, 4, 6 };
            }
        }

        public int THAC0 { get { return GetClassTHAC0(out GenericClass bestClass, out int iLevel); } }

        public int GetClassTHAC0(out GenericClass bestClass, out int iBestLevel)
        {
            GenericClass[] classes = Global.SeparateClasses(GetBasicClass(Class));
            bestClass = classes[0];
            iBestLevel = Level[0];
            int iBestTHAC0 = 20;
            for (int i = 0; i < classes.Length; i++)
            {
                int iTHAC0 = GetTHAC0(classes[i], Level[i], BasicStrength.Temporary, UsesStrength18(classes[i]) ? BasicStrength18.Temporary : 0);
                if (iTHAC0 < iBestTHAC0)
                {
                    iBestTHAC0 = iTHAC0;
                    bestClass = classes[i];
                    iBestLevel = Level[i];
                }
            }
            return iBestTHAC0;
        }

        public static int GetTHAC0(GenericClass gc, int iLevel, int iStrength, int iStrength18)
        {
            return GetBaseTHAC0(gc, iLevel) + GetTHAC0Mod(iStrength, iStrength18);
        }

        public static int GetTHAC0Mod(int iStrength, int iStrength18)
        {
            if (iStrength < 4) return 3;
            if (iStrength < 6) return 2;
            if (iStrength < 8) return 1;
            if (iStrength < 17) return 0;
            if (iStrength < 18) return -1;
            if (iStrength > 24) return -7;
            if (iStrength > 23) return -6;
            if (iStrength > 22) return -5;
            if (iStrength > 20) return -4;
            if (iStrength > 18) return -3;
            if (iStrength18 < 51) return -1;
            if (iStrength18 < 100) return -2;
            return -3;  // 18/00
        }

        public static int GetBaseTHAC0(GenericClass gc, int iLevel)
        {
            switch (gc)
            {
                case GenericClass.Mage: return 20 - ((iLevel - 1) / 3);
                case GenericClass.Cleric: return 20 - (2 * ((iLevel - 1) / 3));
                case GenericClass.Thief: return 20 - ((iLevel - 1) / 2);
                default: return 21 - iLevel;   // Fighter, Paladin, Ranger
            }
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (EOBItem item in Inventory.SelectEquippedItems)
                    sb.AppendFormat("{0}, ", item.Name);
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(nothing)";
                return sb.ToString();
            }
        }

        public override string BackpackString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (EOBItem item in Inventory.SelectUnequippedItems)
                    sb.AppendFormat("{0}, ", item.Name);
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(empty)";
                return sb.ToString();
            }
        }

        public override string MeleeDamageString
        {
            get
            {
                EOBItem item = Inventory.BestMeleeItem();
                if (item == null)
                    return "0";
                return item.DamageStringFull;
            }
        }

        public override string RangedDamageString
        {
            get
            {
                return Inventory.RangedWeaponDamage.ToString();
            }
        }

        public override string GetACFormula(int iBless = 0)
        {
            return String.Format("10\tBase Armor Class\r\n{0}\tDexterity modifier", GetACModifier(BasicDexterity.Temporary).PlusValue);
        }

        public override Modifiers InternalModifiers
        {
            get
            {
                Modifiers mod = new Modifiers();

                foreach (EOBItem item in Inventory.SelectEquippedItems)
                {
                    if (item.ArmorClass != 0 || item.ModAffectsAC)
                        mod.Adjust(ModAttr.ArmorClass, item.ArmorClassFull, item.DescriptionString);
                }
                return mod;
            }
        }

        public virtual string GetCurrentQuest(MemoryHacker hacker)
        {
            return "None";
        }
    }
}