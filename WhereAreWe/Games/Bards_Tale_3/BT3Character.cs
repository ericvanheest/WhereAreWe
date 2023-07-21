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
    public class BT3CharacterOffsets : CharacterOffsets
    {
        public override int Condition { get { return 45; } }
        public override int ConditionLength { get { return 1; } }
        public override int Race { get { return 42; } }
        public override int Sex { get { return 43; } }
        public override int Class { get { return 41; } }
        public override int Stats { get { return 16; } }
        public override int ArmorClass { get { return 46; } }
        public override int CurrentHP { get { return 33; } }
        public override int MaxHP { get { return 35; } }
        public override int CurrentSP { get { return 37; } }
        public override int MaxSP { get { return 39; } }
        public override int Inventory { get { return 48; } }
        public override int InventoryLength { get { return 36; } }
        public override int Experience { get { return 21; } }
        public override int ExperienceLength { get { return 4; } }
        public override int Gold { get { return 25; } }
        public override int GoldLength { get { return 4; } }
        public override int LevelMod { get { return 29; } }
        public override int Level { get { return 31; } }
        public override int Spells { get { return 84; } }
        public override int SpellsLength { get { return 18; } }
        public override int Awards { get { return 114; } }
        public override int AwardsLength { get { return 2; } }
        public override int LevelLength { get { return 2; } }

        public override int BardSongs { get { return 100; } }
        public override int Thievery { get { return 100; } }
        public override int Critical { get { return 100; } }    // Bard songs, Thievery, and Hunter critical chance are overloaded
        public override int Name { get { return 0; } }
        public override int NameLength { get { return 15; } }
        public override int Swings { get { return 105; } }
        public override int HideChance { get { return 102; } }
        public override int Identify { get { return 101; } }

        // unverified
        public override int BattlesWon { get { return 100; } }
        public static int Animation { get { return 44; } }
    }

    [Flags]
    public enum BT3Condition
    {
        Good =       0x00,
        Poison =     0x01,
        Old =        0x02,
        Dead =       0x04,
        Stone =      0x08,
        Paralyzed =  0x10,
        Possessed =  0x20,
        Nuts =       0x40,
        Invalid =    0x80,
        DisplayConditions = Dead | Old | Poison | Stone | Paralyzed | Possessed | Nuts
    }

    public class BT3Stats : BTStats
    {
        public override int StatSize { get { return 1; } }

        public BT3Stats(byte[] bytes, int offset = 0)
        {
            StrengthOffsetPerm = 0;
            StrengthOffsetTemp = 0;
            IQOffsetPerm = 1;
            IQOffsetTemp = 1;
            DexOffsetPerm = 2;
            DexOffsetTemp = 2;
            ConOffsetPerm = 3;
            ConOffsetTemp = 3;
            LuckOffsetPerm = 4;
            LuckOffsetTemp = 4;

            SetFromOneByte(bytes, offset);
        }
    }

    public class BT3Character : BTCharacter
    {
        public const int SizeInBytes = 120;
        public const int SizeInMemory = 120;
        public byte[] Awards;
        public byte[] Unknown106;
        public byte[] Unknown116;
        public byte Animation;
        public MMSex Sex;
        public BT3Condition BT3Condition { get { return ToBT3Condition(Condition); } }
        public BT3Class Class;

        public BT3Character()
        {
            m_game = GameNames.BardsTale2;
        }

        public override CharacterOffsets Offsets { get { return BT3.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override BTStats CreateStats(byte[] bytes, int offset) { return new BT3Stats(RawBytes, Offsets.Stats); }
        public override MMSex BasicSex { get { return Sex; } }
        public override int MaxBackpackSize { get { return 12 - Inventory.SelectEquippedItems.Count; } }

        public override CheatOffsets GetInventoryCheatOffsets(int iIndex)
        {
            return new CheatOffsets(new int[] { Offsets.Inventory + (iIndex * 3), Offsets.Inventory + (iIndex * 3) + 1, Offsets.Inventory + (iIndex * 3) + 2 });
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemList = null)
        {
            if (stream.Length < CharacterSize)
                return;

            if (info != null)
                m_game = info.Game;

            RawBytes = new byte[CharacterSize];
            stream.Read(RawBytes, 0, CharacterSize);

            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, 15).TrimEnd(new char[] { ' ', '\0' });
            Condition = ToBTCondition((BT3Condition)RawBytes[Offsets.Condition]);
            Race = (BTRace)RawBytes[Offsets.Race];
            Class = (BT3Class)RawBytes[Offsets.Class];
            Sex = (MMSex)RawBytes[Offsets.Sex] + 1;
            Stats = CreateStats(RawBytes, Offsets.Stats);
            ArmorClass = 10 - BitConverter.ToInt16(RawBytes, Offsets.ArmorClass);
            HitPoints = new TwoByteStat(RawBytes, Offsets.CurrentHP, Offsets.MaxHP);
            SpellPoints = new TwoByteStat(RawBytes, Offsets.CurrentSP, Offsets.MaxSP);
            Inventory = new BT3Inventory(RawBytes, Offsets.Inventory);
            Experience = BitConverter.ToInt32(RawBytes, Offsets.Experience);
            Gold = BitConverter.ToInt32(RawBytes, Offsets.Gold);
            Level = BitConverter.ToInt16(RawBytes, Offsets.Level);
            LevelMod = BitConverter.ToInt16(RawBytes, Offsets.LevelMod);
            Spells = new BT3KnownSpells(RawBytes, Offsets.Spells);
            m_iSongs = RawBytes[Offsets.BardSongs];
            Animation = RawBytes[BT3CharacterOffsets.Animation];
            Awards = Global.Subset(RawBytes, Offsets.Awards, Offsets.AwardsLength);
            Unknown106 = Global.Subset(RawBytes, 106, 8);
            Unknown116 = Global.Subset(RawBytes, 116, 2);
            BattlesWon = BitConverter.ToInt16(RawBytes, Offsets.BattlesWon);
            HideChance = RawBytes[Offsets.HideChance];
            CriticalChance = RawBytes[Offsets.Critical];
            Identify = RawBytes[Offsets.Identify];
            Disarm = RawBytes[Offsets.Thievery];
            NumAttacks = RawBytes[Offsets.Swings];
            switch (Class)
            {
                case BT3Class.Bard:
                    CriticalChance = 0;
                    Disarm = 0;
                    break;
                case BT3Class.Hunter:
                    Disarm = 0;
                    m_iSongs = 0;
                    break;
                case BT3Class.Rogue:
                    m_iSongs = 0;
                    CriticalChance = HideChance;
                    break;
                default:
                    m_iSongs = 0;
                    CriticalChance = 0;
                    Disarm = 0;
                    break;
            }

            Modifiers = Inventory.GetModifiers();
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            bytes[Offsets.Condition] = (byte)BT3Condition;
            Global.SetInt16(bytes, Offsets.Condition, (int)Condition);
            bytes[Offsets.Race] = (byte)Race;
            bytes[Offsets.Class] = (byte)Class;
            bytes[Offsets.Sex] = (byte)(Sex - 1);

            Stats.SetBytes(bytes, Offsets.Stats);
            bytes[Offsets.ArmorClass] = (byte)(10 - ArmorClass);
            HitPoints.SetBytes(bytes, Offsets.MaxHP, Offsets.CurrentHP);
            SpellPoints.SetBytes(bytes, Offsets.MaxSP, Offsets.CurrentSP);
            Inventory.SetBytes(bytes, Offsets.Inventory, BasicClass);
            Global.SetInt32(bytes, Offsets.Experience, Experience);
            Global.SetInt32(bytes, Offsets.Gold, Gold);
            Global.SetInt16(bytes, Offsets.Level, Level);
            Global.SetInt16(bytes, Offsets.LevelMod, LevelMod);
            Global.SetBytes(bytes, Offsets.Spells, Spells.GetBytes());
            bytes[BT3CharacterOffsets.Animation] = Animation;
            Global.SetBytes(bytes, 106, Unknown106);
            Global.SetBytes(bytes, 116, Unknown116);
            Global.SetBytes(bytes, Offsets.Awards, Awards);
            Global.SetInt16(bytes, Offsets.BattlesWon, BattlesWon);
            bytes[Offsets.HideChance] = (byte)HideChance;
            bytes[Offsets.Identify] = (byte)Identify;
            switch (Class)
            {
                case BT3Class.Bard:
                    bytes[Offsets.BardSongs] = (byte)Songs;
                    break;
                case BT3Class.Hunter:
                    bytes[Offsets.Critical] = (byte)CriticalChance;
                    break;
                case BT3Class.Rogue:
                    bytes[Offsets.Thievery] = (byte)Disarm;
                    break;
                default:
                    bytes[Offsets.BardSongs] = 0;
                    break;
            }
            bytes[Offsets.Swings] = (byte) NumAttacks;
            Global.SetBytes(bytes, 0, Games.GetNameBytes(Game, CharName));
            stream.Write(bytes, 0, CharacterSize);
        }

        public static BTCondition ToBTCondition(BT3Condition cond)
        {
            BTCondition btCond = BTCondition.Good;
            if (cond.HasFlag(BT3Condition.Nuts))
                btCond |= BTCondition.Nuts;
            if (cond.HasFlag(BT3Condition.Old))
                btCond |= BTCondition.Old;
            if (cond.HasFlag(BT3Condition.Paralyzed))
                btCond |= BTCondition.Paralyzed;
            if (cond.HasFlag(BT3Condition.Poison))
                btCond |= BTCondition.Poison;
            if (cond.HasFlag(BT3Condition.Possessed))
                btCond |= BTCondition.Possessed;
            if (cond.HasFlag(BT3Condition.Stone))
                btCond |= BTCondition.Stone;
            if (cond.HasFlag(BT3Condition.Dead))
                btCond |= BTCondition.Dead;
            if (cond.HasFlag(BT3Condition.Invalid))
                btCond |= BTCondition.Out;
            return btCond;
        }

        public static BT3Condition ToBT3Condition(BTCondition cond)
        {
            BT3Condition bt3Cond = BT3Condition.Good;
            if (cond.HasFlag(BTCondition.Nuts))
                bt3Cond |= BT3Condition.Nuts;
            if (cond.HasFlag(BTCondition.Old))
                bt3Cond |= BT3Condition.Old;
            if (cond.HasFlag(BTCondition.Paralyzed))
                bt3Cond |= BT3Condition.Paralyzed;
            if (cond.HasFlag(BTCondition.Poison))
                bt3Cond |= BT3Condition.Poison;
            if (cond.HasFlag(BTCondition.Possessed))
                bt3Cond |= BT3Condition.Possessed;
            if (cond.HasFlag(BTCondition.Stone))
                bt3Cond |= BT3Condition.Stone;
            if (cond.HasFlag(BTCondition.Dead))
                bt3Cond |= BT3Condition.Dead;
            if (cond.HasFlag(BTCondition.Out))
                bt3Cond |= BT3Condition.Invalid;
            return bt3Cond;
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            return (byte) ToBT3Condition((BTCondition) base.ConditionValue(condition));
        }

        public static GenericClass GetBasicClass(BT3Class btClass)
        {
            switch (btClass)
            {
                case BT3Class.Bard: return GenericClass.Bard;
                case BT3Class.Conjurer: return GenericClass.Conjurer;
                case BT3Class.Hunter: return GenericClass.Hunter;
                case BT3Class.Magician: return GenericClass.Magician;
                case BT3Class.Monk: return GenericClass.Monk;
                case BT3Class.Paladin: return GenericClass.Paladin;
                case BT3Class.Rogue: return GenericClass.Rogue;
                case BT3Class.Sorcerer: return GenericClass.Sorcerer;
                case BT3Class.Warrior: return GenericClass.Warrior;
                case BT3Class.Wizard: return GenericClass.Wizard;
                case BT3Class.Archmage: return GenericClass.Archmage;
                case BT3Class.Chronomancer: return GenericClass.Chronomancer;
                case BT3Class.Geomancer: return GenericClass.Geomancer;
                case BT3Class.Monster: return GenericClass.Monster;
                default: return GenericClass.None;
            }
        }

        public override GenericClass BasicClass { get { return GetBasicClass(Class); } }

        public static GenericClass[] BT3Classes
        {
            get
            {
                return new GenericClass[] {
                    GenericClass.Warrior,
                    GenericClass.Wizard,
                    GenericClass.Sorcerer,
                    GenericClass.Conjurer,
                    GenericClass.Magician,
                    GenericClass.Rogue,
                    GenericClass.Bard,
                    GenericClass.Paladin,
                    GenericClass.Hunter,
                    GenericClass.Monk,
                    GenericClass.Archmage,
                    GenericClass.Chronomancer,
                    GenericClass.Geomancer,
                    GenericClass.Monster
                };
            }
        }

        public static BT3Class BT3ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Bard: return BT3Class.Bard;
                case GenericClass.Conjurer: return BT3Class.Conjurer;
                case GenericClass.Hunter: return BT3Class.Hunter;
                case GenericClass.Magician: return BT3Class.Magician;
                case GenericClass.Monk: return BT3Class.Monk;
                case GenericClass.Paladin: return BT3Class.Paladin;
                case GenericClass.Rogue: return BT3Class.Rogue;
                case GenericClass.Sorcerer: return BT3Class.Sorcerer;
                case GenericClass.Warrior: return BT3Class.Warrior;
                case GenericClass.Wizard: return BT3Class.Wizard;
                case GenericClass.Archmage: return BT3Class.Archmage;
                case GenericClass.Chronomancer: return BT3Class.Chronomancer;
                case GenericClass.Geomancer: return BT3Class.Geomancer;
                case GenericClass.Monster: return BT3Class.Monster;
                default: return BT3Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)BT3ClassForGeneric(classVal);
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                bool[] items = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };
                foreach (Item item in Inventory.Items)
                    if (item.MemoryIndex >= 0 && item.MemoryIndex < 12)
                        items[item.MemoryIndex] = true;
                for (int i = 0; i < items.Length; i++)
                    if (!items[i])
                        return i;
                return -1;
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (spell == null)
                return false;
            if (Spells == null)
                return false;
            return Spells.IsKnown(spell.BasicIndex, BasicClass);
        }

        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3}",
                 new PermAndTemp(LevelMod, Level).ToString(),
                 MM1Character.SexString(Sex),
                 BTCharacter.RaceString(Race),
                 BTCharacter.ClassString(BasicClass));
            }
        }

        public override byte SexValue(MMSex sex)
        {
            switch (sex)
            {
                case MMSex.Male: return 0;
                case MMSex.Female: return 1;
                default: return 0;
            }
        }

        public static StatModifier GetBT3StatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Strength: return StatModifier.Zero;
                case PrimaryStat.Dexterity: return StatModifier.FromTable(value, stat, 4, -3, 8, -2, 12, -1, 15, 0, 16, 1, 18, 2, 21, 3, 26, 4, 5);
                default: return StatModifier.FromTable(value, stat, 4, -3, 8, -2, 12, -1, 16, 0, 20, 1, 24, 2, 28, 3, 4);
            }
        }

        public override bool IsCaster
        {
            get
            {
                switch (Class)
                {
                    case BT3Class.Warrior:
                    case BT3Class.Bard:
                    case BT3Class.Hunter:
                    case BT3Class.Illusion:
                    case BT3Class.Monk:
                    case BT3Class.Monster:
                    case BT3Class.Paladin:
                    case BT3Class.Rogue:
                        return false;
                    default: return true;
                }
            }
        }

        public override string GetACFormula(int iBless = 0)
        {
            string strBase = String.Format("Note: Lower AC improves to-hit chance (-10 min)\r\n{0}\tDexterity modifier", Global.AddPlus(-GetBT3StatModifier(BasicDexterity.Temporary, PrimaryStat.Dexterity).Value));
            if (Class == BT3Class.Monk)
                strBase += String.Format("\r\n{0}\tMonk level bonus", -Level);
            return strBase;
        }

        public override string MeleeDamageString
        {
            get
            {
                BasicDamage dmg = Inventory.MeleeWeaponDamage;
                if (dmg.Max == 0 && BasicClass == GenericClass.Monk)
                    return new DamageDice(4, Level / 4 + 1, 0).ToString();
                return dmg.ToString();
            }
        }

        public static string AwardString(BT3AwardIndex index)
        {
            switch (index)
            {
                case BT3AwardIndex.BeginValariansBow: return "Begin quest \"Retrieve Valarian's Bow\"";
                case BT3AwardIndex.FinishValariansBow: return "Finish quest \"Retrieve Valarian's Bow\"";
                case BT3AwardIndex.BeginLanatirSphere: return "Begin quest \"Retrieve the Sphere of Lanatir\"";
                case BT3AwardIndex.FinishLanatirSphere: return "Finish quest \"Retrieve the Sphere of Lanatir\"";
                case BT3AwardIndex.BeginBeltOfAlliria: return "Begin quest \"Retrieve the Belt of Alliria\"";
                case BT3AwardIndex.FinishBeltOfAlliria: return "Finish quest \"Retrieve the Belt of Alliria\"";
                case BT3AwardIndex.BeginFerofistsHelm: return "Begin quest \"Retrieve Ferofist's Helm\"";
                case BT3AwardIndex.FinishFerofistsHelm: return "Finish quest \"Retrieve Ferofist's Helm\"";
                case BT3AwardIndex.BeginSceadusCloak: return "Begin quest \"Retrieve Sceadu's Cloak\"";
                case BT3AwardIndex.FinishSceadusCloak: return "Finish quest \"Retrieve Sceadu's Cloak\"";
                case BT3AwardIndex.BeginWerrasShield: return "Begin quest \"Retrieve Werra's Shield\"";
                case BT3AwardIndex.FinishWerrasShield: return "Finish quest \"Retrieve Werra's Shield\"";
                case BT3AwardIndex.BeginDefeatTarjan: return "Begin quest \"Defeat Tarjan\"";
                case BT3AwardIndex.FinishDefeatTarjan: return "Finish quest \"Defeat Tarjan\"";
                case BT3AwardIndex.TalkElder1: return "Talk to the guild elder the first time";
                case BT3AwardIndex.DefeatBrilhasti: return "Defeat Brilhasti ap Tarj";
                default: return null;
            }
        }

        public bool HasAward(BT3AwardIndex index)
        {
            return Global.GetBit(Awards, (int)index) != 0;
        }

        public override string GetCurrentQuest(MemoryHacker hacker)
        {
            BT3MemoryHacker bt3Hacker = hacker as BT3MemoryHacker;
            if (bt3Hacker == null)
                return base.GetCurrentQuest(hacker);

            HashSet<BT3ItemIndex> items = bt3Hacker.GetAllInventoryItems();

            if (HasAward(BT3AwardIndex.BeginDefeatTarjan))
                return "Travel to Malefia and Defeat Tarjan";
            if (HasAward(BT3AwardIndex.BeginWerrasShield))
            {
                if (items.Contains(BT3ItemIndex.WerrasShield))
                    return "Return Werra's Shield to the the guild elder";
                else
                    return "Travel to Tarmitia and retrieve Werra's Shield";
            }
            if (HasAward(BT3AwardIndex.BeginSceadusCloak))
            {
                if (items.Contains(BT3ItemIndex.SceadusCloak))
                    return "Return Sceadu's Cloak to the guild elder";
                else
                    return "Travel to Tenabrosia and retrieve Sceadu's Cloak";
            }
            if (HasAward(BT3AwardIndex.BeginFerofistsHelm))
            {
                if (items.Contains(BT3ItemIndex.FerofistsHelm))
                    return "Return Ferofist's Helm to the guild elder";
                else
                    return "Travel to Kinestia and retrieve Ferofist's Helm";
            }
            if (HasAward(BT3AwardIndex.BeginBeltOfAlliria))
            {
                if (items.Contains(BT3ItemIndex.BeltOfAlliria))
                    return "Return the Belt of Alliria to the guild elder";
                else
                    return "Travel to Lucencia and retrieve the Belt of Alliria";
            }
            if (HasAward(BT3AwardIndex.BeginLanatirSphere))
            {
                if (items.Contains(BT3ItemIndex.SphereOfLanatir))
                    return "Return the Sphere of Lanatir to the guild elder";
                else
                    return "Travel to Gelidia and retrieve the Sphere of Lanatir";
            }
            if (HasAward(BT3AwardIndex.BeginValariansBow))
            {
                if (items.Contains(BT3ItemIndex.ValariansBow))
                    return "Return Valarian's Bow to the guild elder";
                else
                    return "Travel to Arboria and retrieve Valarian's Bow";
            }
            if (HasAward(BT3AwardIndex.TalkElder1))
            {
                if (!HasAward(BT3AwardIndex.DefeatBrilhasti))
                    return "Travel to Unterbrae and defeat Brilhasti ap Tarj";
                else
                    return "Return to the elder with news of Brilhasti's defeat";
            }

            return "Go to Skara Brae and talk to the elder";
        }
    }

    public enum BT3AwardIndex
    {
        BeginValariansBow = 0,
        FinishValariansBow = 1,
        BeginLanatirSphere = 2,
        FinishLanatirSphere = 3,
        BeginBeltOfAlliria = 4,
        FinishBeltOfAlliria = 5,
        BeginFerofistsHelm = 6,
        FinishFerofistsHelm = 7,
        BeginSceadusCloak = 8,
        FinishSceadusCloak = 9,
        BeginWerrasShield = 10,
        FinishWerrasShield = 11,
        BeginDefeatTarjan = 12,
        FinishDefeatTarjan = 13,
        TalkElder1 = 14,
        DefeatBrilhasti = 15,
        None = -1,
    }

    public class BT3Inventory : BTInventory
    {
        public BT3Inventory(byte[] bytes, int offset = 0)
        {
            // A Bard's Tale 3 inventory is 12 3-byte values

            m_items = new List<Item>(8);

            for (int i = 0; i < 12; i++)
            {
                if (i * 3 + offset > bytes.Length)
                    break;  // Not enough bytes for a proper inventory

                BTItem item = BTItem.FromInventoryBytes(GameNames.BardsTale3, bytes, offset + (i * 3));
                if (item != null)
                {
                    if (item.Index > 0)
                    {
                        item.MemoryIndex = i;
                        item.DisplayIndex = String.Format("{0}.", i + 1);
                        m_items.Add(item);
                    }
                }
            }
        }

        public BT3Inventory(List<Item> items)
        {
            m_items = items;
        }

        public override byte[] GetBytes(GenericClass gc = GenericClass.None)
        {
            byte[] bytes = new byte[36];
            SetBytes(bytes, 0, gc);
            return bytes;
        }

        public override void SetBytes(byte[] bytes, int offset, GenericClass gc = GenericClass.None)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[offset + i] = 0;

            int iIndexItem = 0;
            foreach (BT3Item item in m_items)
            {
                BT3ItemFlags flags = item.Contains;
                if (item.IsEquipped)
                    flags |= BT3ItemFlags.Equipped;
                if (!item.IsUsable(gc) && gc != GenericClass.None)
                    flags |= BT3ItemFlags.Unequippable;
                if (!item.Identified)
                    flags |= BT3ItemFlags.Unidentified;
                if (item.FailedIdentify)
                    flags |= BT3ItemFlags.FailedIdentify;

                bytes[iIndexItem * 3] = (byte)flags;
                bytes[iIndexItem * 3 + 1] = (byte)item.Index;
                bytes[iIndexItem * 3 + 2] = (byte)item.ChargesCurrent;
                iIndexItem++;
            }
        }

        public override BasicDamage RangedWeaponDamage
        {
            get
            {
                BT3Item itemRanged = null;
                foreach (BT3Item item in SelectEquippedItems)
                {
                    if (item.BTType == BTItemType.Quiver)
                    {
                        // Quiver items are always "the" ranged item if they are equipped
                        itemRanged = item;
                        break;
                    }

                    if (itemRanged == null || item.MissileDamage.Average > itemRanged.MissileDamage.Average)
                        itemRanged = item;
                }

                return itemRanged == null ? BasicDamage.Zero : new BasicDamage(1, itemRanged.MissileDamage);
            }
        }
    }
}