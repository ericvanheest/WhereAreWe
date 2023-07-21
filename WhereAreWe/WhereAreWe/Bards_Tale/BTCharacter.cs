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
    public enum BTRace
    {
        None = -1,
        Human = 0,
        Elf = 1,
        Dwarf = 2,
        Hobbit = 3,
        HalfElf = 4,
        HalfOrc = 5,
        Gnome = 6,
        Last
    }

    public enum BT12Class
    {
        None = -1,
        Warrior = 0,
        Paladin = 1,
        Rogue = 2,
        Bard = 3,
        Hunter = 4,
        Monk = 5,
        Conjurer = 6,
        Magician = 7,
        Sorcerer = 8,
        Wizard = 9, 
        Archmage = 10,
        Monster = 11,
        Last
    }

    public enum BT3Class
    {
        None = -1,
        Warrior = 0,
        Wizard = 1,
        Sorcerer = 2,
        Conjurer = 3,
        Magician = 4,
        Rogue = 5,
        Bard = 6,
        Paladin = 7,
        Hunter = 8,
        Monk = 9,
        Archmage = 10,
        Chronomancer = 11,
        Geomancer = 12,
        Monster = 13,
        Illusion = 14,
        Last
    }

    [Flags]
    public enum BTCondition
    {
        Good =       0x0000,
        Out =        0x0001,
        Dead =       0x0002,
        Old =        0x0004,
        Poison =     0x0008,
        Stone =      0x0010,
        Paralyzed =  0x0020,
        Possessed =  0x0040,
        Nuts =       0x0080,
        FinishGame = 0x2000,
        DisplayConditions = Dead | Old | Poison | Stone | Paralyzed | Possessed | Nuts
    }

    public abstract class BTStats
    {
        public TwoByteStat Strength;
        public TwoByteStat IQ;
        public TwoByteStat Dexterity;
        public TwoByteStat Constitution;
        public TwoByteStat Luck;

        public int StrengthOffsetPerm;
        public int StrengthOffsetTemp;
        public int IQOffsetPerm;
        public int IQOffsetTemp;
        public int DexOffsetPerm;
        public int DexOffsetTemp;
        public int ConOffsetPerm;
        public int ConOffsetTemp;
        public int LuckOffsetPerm;
        public int LuckOffsetTemp;

        public abstract int StatSize { get; }

        protected void SetFromTwoBytes(byte[] bytes, int offset = 0)
        {
            Strength = new TwoByteStat(bytes, offset + StrengthOffsetTemp, offset + StrengthOffsetPerm);
            IQ = new TwoByteStat(bytes, offset + IQOffsetTemp, offset + IQOffsetPerm);
            Dexterity = new TwoByteStat(bytes, offset + DexOffsetTemp, offset + DexOffsetPerm);
            Constitution = new TwoByteStat(bytes, offset + ConOffsetTemp, offset + ConOffsetPerm);
            Luck = new TwoByteStat(bytes, offset + LuckOffsetTemp, offset + LuckOffsetPerm);
        }

        protected void SetFromOneByte(byte[] bytes, int offset = 0)
        {
            Strength = new TwoByteStat(new OneByteStat(bytes, offset + StrengthOffsetTemp, offset + StrengthOffsetPerm));
            IQ = new TwoByteStat(new OneByteStat(bytes, offset + IQOffsetTemp, offset + IQOffsetPerm));
            Dexterity = new TwoByteStat(new OneByteStat(bytes, offset + DexOffsetTemp, offset + DexOffsetPerm));
            Constitution = new TwoByteStat(new OneByteStat(bytes, offset + ConOffsetTemp, offset + ConOffsetPerm));
            Luck = new TwoByteStat(new OneByteStat(bytes, offset + LuckOffsetTemp, offset + LuckOffsetPerm));
        }

        public virtual void SetBytes(byte[] bytes, int offset)
        {
            bytes[offset + StrengthOffsetTemp] = (byte)Strength.Temporary;
            bytes[offset + IQOffsetTemp] = (byte)IQ.Temporary;
            bytes[offset + DexOffsetTemp] = (byte)Dexterity.Temporary;
            bytes[offset + ConOffsetTemp] = (byte)Constitution.Temporary;
            bytes[offset + LuckOffsetTemp] = (byte)Luck.Temporary;
            bytes[offset + StrengthOffsetPerm] = (byte)Strength.Permanent;
            bytes[offset + IQOffsetPerm] = (byte)IQ.Permanent;
            bytes[offset + DexOffsetPerm] = (byte)Dexterity.Permanent;
            bytes[offset + ConOffsetPerm] = (byte)Constitution.Permanent;
            bytes[offset + LuckOffsetPerm] = (byte)Luck.Permanent;
        }
    }

    public class BTSpellLevel
    {
        public int Sorcerer;
        public int Conjurer;
        public int Magician;
        public int Wizard;
        public int Archmage;

        public BTSpellLevel(int sorc, int conj, int magi, int wizd, int arch = -1)
        {
            Sorcerer = sorc;
            Conjurer = conj;
            Magician = magi;
            Wizard = wizd;
            Archmage = arch;
        }

        public BTSpellLevel(byte[] bytes, int offset = 0, int count = 4) : this(bytes[offset], bytes[offset+1], bytes[offset+2], bytes[offset+3], count < 5 ? -1 : bytes[offset + 4]) { }

        public bool KnowsAnySpells { get { return Sorcerer > 0 || Conjurer > 0 || Magician > 0 || Wizard > 0 || Archmage > 0; } }

        public bool IsKnown(Spell spell)
        {
            BTSpell btSpell = spell as BTSpell;
            if (btSpell == null)
                return false;

            switch(btSpell.Type)
            {
                case SpellType.Sorcerer: return btSpell.Level <= Sorcerer;
                case SpellType.Conjurer: return btSpell.Level <= Conjurer;
                case SpellType.Magician: return btSpell.Level <= Magician;
                case SpellType.Wizard: return btSpell.Level <= Wizard;
                case SpellType.Archmage: return btSpell.Level <= Archmage;
                default: return false;
            }
        }

        public void SetBytes(byte[] bytes, int offset, int count = 4)
        {
            if (bytes.Length < offset + count)
                return;

            Buffer.BlockCopy(GetBytes(), 0, bytes, offset, count);
        }

        public byte[] GetBytes()
        {
            if (Archmage < 0)
                return new byte[] { (byte)Sorcerer, (byte)Conjurer, (byte)Magician, (byte)Wizard };
            return new byte[] { (byte)Sorcerer, (byte)Conjurer, (byte)Magician, (byte)Wizard, (byte)Archmage };
        }

        public bool KnowsAnyHealing
        {
            // Bard's Tale 1/2 healing spells: Word of Healing, Flesh Restore, Flesh Anew, Restoration, Dispossess, Beyond Death, HealAll
            get { return Conjurer >= 2 || Magician >= 7 || Wizard >= 3 || Archmage >= 5 ; }
        }

        public override string ToString()
        {
            if (Archmage < 0)
                return String.Format("{0}/{1}/{2}/{3}", Conjurer, Magician, Sorcerer, Wizard);
            return String.Format("{0}/{1}/{2}/{3}/{4}", Conjurer, Magician, Sorcerer, Wizard, Archmage);
        }

        public string ToFullString()
        {
            if (Archmage < 0)
                return String.Format("Conjurer {0}, Magician {1}, Sorcerer {2}, Wizard {3}", Conjurer, Magician, Sorcerer, Wizard);
            return String.Format("Conjurer {0}, Magician {1}, Sorcerer {2}, Wizard {3}, Archmage {4}", Conjurer, Magician, Sorcerer, Wizard, Archmage);
        }
    }

    public class BTInventory : Inventory
    {
        protected List<Item> m_items;

        public override List<Item> Items { get { return m_items; } set { m_items = value; } }

        public static BTInventory Create(GameNames game, byte[] bytes, int offset = 0)
        {
            switch (game)
            {
                case GameNames.BardsTale1: return new BT1Inventory(bytes, offset);
                case GameNames.BardsTale2: return new BT2Inventory(bytes, offset);
                case GameNames.BardsTale3: return new BT3Inventory(bytes, offset);
                default:
                    return null;
            }
        }

        public static BTInventory Create(GameNames game, List<Item> items)
        {
            switch (game)
            {
                case GameNames.BardsTale1: return new BT1Inventory(items);
                case GameNames.BardsTale2: return new BT2Inventory(items);
                case GameNames.BardsTale3: return new BT3Inventory(items);
                default:
                    return null;
            }
        }

        public bool HasItem(GameNames game, int index, bool bEquippedOnly = false)
        {
            Item itemFound = bEquippedOnly ? Items.FirstOrDefault(i => i.Index == index && i.IsEquipped) : Items.FirstOrDefault(i => i.Index == index);
            switch (game)
            {
                case GameNames.BardsTale1: return itemFound is BT1Item;
                case GameNames.BardsTale2: return itemFound is BT2Item;
                case GameNames.BardsTale3: return itemFound is BT3Item;
                default: return false;
            }
        }

        public bool HasItem(BT1ItemIndex itemWanted) { return HasItem(GameNames.BardsTale1, (int)itemWanted); }
        public bool HasItem(BT2ItemIndex itemWanted) { return HasItem(GameNames.BardsTale2, (int)itemWanted); }
        public bool HasItem(BT3ItemIndex itemWanted) { return HasItem(GameNames.BardsTale3, (int)itemWanted); }

        public virtual void SetBytes(byte[] bytes, int offset)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[offset + i] = 0;

            int iIndexItem = 0;
            foreach (BTItem item in m_items)
            {
                int equipped = item.IsEquipped ? 0x8000 : 0;
                int identified = item.IsIdentified ? 0 : 0x4000;

                Global.SetUInt16(bytes, offset + (iIndexItem * 2), item.Index | equipped | identified);
                if (bytes.Length > 16)
                    bytes[iIndexItem + 16] = (byte) item.ChargesCurrent;
                iIndexItem++;
            }
        }

        public virtual byte[] GetBytes()
        {
            byte[] bytes = new byte[16];
            SetBytes(bytes, 0);
            return bytes;
        }

        public override Modifiers GetModifiers()
        {
            Modifiers mod = new Modifiers();

            foreach (BTItem item in Items)
            {
                if (item.WhereEquipped == EquipLocation.None)
                    continue;

                if (item.Damage.Max > 0)
                    mod.Adjust(ModAttr.MeleeDamage, item.Damage.Bonus, item.Name);
            }

            return mod;
        }

        public override BasicDamage MeleeWeaponDamage
        {
            get
            {
                BasicDamage dmg = new BasicDamage(1, DamageDice.Zero);
                foreach (BTItem item in SelectEquippedItems)
                {
                    if (item.Damage.Max > 0)
                        dmg.Add(item.Damage);
                }
                return dmg;
            }
        }

        public override string MeleeWeaponName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (BTItem item in SelectEquippedItems)
                {
                    if (item.Damage.Max > 0)
                        sb.AppendFormat("{0}, ", item.Name);
                }
                return Global.Trim(sb).ToString();
            }
        }
    }

    public abstract class BTCharacter : BTBaseCharacter
    {
        public string CharName;
        public BTRace Race;
        public BTStats Stats;
        public int Level;
        public int LevelMod;
        public long Experience;
        public TwoByteStat SpellPoints;
        public TwoByteStat HitPoints;
        public long Gold;
        public int ArmorClass;
        public BTCondition Condition;
        public BTInventory Inventory;
        public BTSpellLevel SpellLevel;
        protected int m_iSongs;
        public int BattlesWon;
        public int HideChance;
        public int CriticalChance;
        public int NumAttacks;
        public int Identify;
        public int Disarm;
        public BT3KnownSpells Spells = null;

        protected GameNames m_game = GameNames.BardsTale1;
        public override int Songs { get { return m_iSongs; } }

        public int Address = -1;

        public BTCharacter()
        {
            Address = -1;
        }

        public override GameNames Game { get { return m_game; } }

        public string GetExtraConditionDesc()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full: Character may not hold any more items\r\n");
            return sb.ToString().Trim();
        }

        public string GetExtraConditions()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full, ");
            return Global.Trim(sb).ToString();
        }

        public override int BasicAddress { get { return Address; } }
        public abstract CheatOffsets GetInventoryCheatOffsets(int iIndex);

        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public static BTCharacter Create(GameNames game, int iCharIndex = -1, byte[] bytes = null, int iIndex = -1)
        {
            BTCharacter btChar = null;
            switch (game)
            {
                case GameNames.BardsTale1:
                    btChar = new BT1Character();
                    break;
                case GameNames.BardsTale2:
                    btChar = new BT2Character();
                    break;
                case GameNames.BardsTale3:
                    btChar = new BT3Character();
                    break;
            }

            if (bytes != null)
                btChar.SetFromBytes(game, iCharIndex, bytes, iIndex);
            return btChar;
        }

        public void SetFromBytes(GameNames game, int iCharIndex, byte[] bytes, int iIndex)
        {
            m_game = game;
            Address = -1;
            if (bytes == null || bytes.Length < iIndex + CharacterSize - 1)
                return;
            if (bytes[16] == 2)
                return;  // This is a "team" not a single character
            SetCharFromStream(iCharIndex, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null);
        }

        public abstract BTStats CreateStats(byte[] bytes, int offset);
        public override int MaxBackpackSize { get { return 8 - Inventory.SelectEquippedItems.Count; } }

        public override StatAndModifier BasicStrength { get { return new StatAndModifier(Stats.Strength); } }
        public override StatAndModifier BasicIQ { get { return new StatAndModifier(Stats.IQ); } }
        public override StatAndModifier BasicDexterity { get { return new StatAndModifier(Stats.Dexterity); } }
        public override StatAndModifier BasicConstitution { get { return new StatAndModifier(Stats.Constitution); } }
        public override StatAndModifier BasicLuck { get { return new StatAndModifier(Stats.Luck); } }

        public override string Name { get { return CharName; } }
        public override SpellPoints QuickRefSpellPoints { get { return new MMSpellPoints(SpellPoints.Temporary, SpellPoints.Permanent); } }
        public override List<Item> BackpackItems { get { return Inventory.SelectUnequippedItems; } }

        public static string RaceString(BTRace race)
        {
            switch (race)
            {
                case BTRace.None: return "None";
                case BTRace.Dwarf: return "Dwarf";
                case BTRace.Elf: return "Elf";
                case BTRace.Gnome: return "Gnome";
                case BTRace.Hobbit: return "Hobbit";
                case BTRace.Human: return "Human";
                case BTRace.HalfElf: return "Half-Elf";
                case BTRace.HalfOrc: return "Half-Orc";
                default: return String.Format("Unknown({0})", (int)race);
            }
        }

        public static string ClassString(BT12Class classenum)
        {
            switch (classenum)
            {
                case BT12Class.Bard: return "Bard";
                case BT12Class.Conjurer: return "Conjurer";
                case BT12Class.Hunter: return "Hunter";
                case BT12Class.Magician: return "Magician";
                case BT12Class.Monk: return "Monk";
                case BT12Class.Paladin: return "Paladin";
                case BT12Class.Rogue: return "Rogue";
                case BT12Class.Sorcerer: return "Sorcerer";
                case BT12Class.Warrior: return "Warrior";
                case BT12Class.Wizard: return "Wizard";
                case BT12Class.Archmage: return "Archmage";
                case BT12Class.Monster: return "Monster";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public static string ClassString(BT3Class classenum)
        {
            switch (classenum)
            {
                case BT3Class.Bard: return "Bard";
                case BT3Class.Conjurer: return "Conjurer";
                case BT3Class.Hunter: return "Hunter";
                case BT3Class.Magician: return "Magician";
                case BT3Class.Monk: return "Monk";
                case BT3Class.Paladin: return "Paladin";
                case BT3Class.Rogue: return "Rogue";
                case BT3Class.Sorcerer: return "Sorcerer";
                case BT3Class.Warrior: return "Warrior";
                case BT3Class.Wizard: return "Wizard";
                case BT3Class.Archmage: return "Archmage";
                case BT3Class.Chronomancer: return "Chronomancer";
                case BT3Class.Geomancer: return "Geomancer";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public static string ConditionString(BTCondition cond) { return ConditionString(cond, String.Empty, true); }

        public static string ConditionString(BTCondition cond, string strExtra, bool bIncludeGood)
        {
            if ((cond & BTCondition.DisplayConditions) == BTCondition.Good)
            {
                if (!String.IsNullOrWhiteSpace(strExtra))
                    return strExtra;
                return bIncludeGood ? "Good" : "";
            }

            if (!String.IsNullOrWhiteSpace(strExtra))
                strExtra = ", " + strExtra;

            StringBuilder sb = new StringBuilder();

            if (cond.HasFlag(BTCondition.Dead))
                sb.Append("Dead, ");
            if (cond.HasFlag(BTCondition.Nuts))
                sb.Append("Nuts, ");
            if (cond.HasFlag(BTCondition.Old))
                sb.Append("Old, ");
            if (cond.HasFlag(BTCondition.Paralyzed))
                sb.Append("Paralyzed, ");
            if (cond.HasFlag(BTCondition.Poison))
                sb.Append("Poison, ");
            if (cond.HasFlag(BTCondition.Possessed))
                sb.Append("Possessed, ");
            if (cond.HasFlag(BTCondition.Stone))
                sb.Append("Stone, ");
            Global.Trim(Global.Trim(sb).Append(strExtra));

            if (sb.Length > 2 && sb[0] == ',')
                sb.Remove(0, 2);
            return sb.ToString();
        }

        public static string ConditionDescription(BTCondition cond, BTCharacter btChar = null)
        {
            string strExtra = btChar == null ? String.Empty : btChar.GetExtraConditionDesc();
            if (cond == BTCondition.Good)
            {
                if (String.IsNullOrWhiteSpace(strExtra))
                    return "Good: Character is healthy";
                return strExtra;
            }

            strExtra = "\r\n" + strExtra;
            StringBuilder sb = new StringBuilder();
            if (cond.HasFlag(BTCondition.Dead))
                sb.AppendLine("Dead:  Cannot perform actions and gains no combat XP");
            if (cond.HasFlag(BTCondition.Nuts))
                sb.AppendLine("Nuts (Insane): Character attacks random targets"); 
            if (cond.HasFlag(BTCondition.Old))
                sb.AppendLine("Old: All attributes reduced to 1"); 
            if (cond.HasFlag(BTCondition.Out))
                sb.AppendLine("Out: Character is not in the active party"); 
            if (cond.HasFlag(BTCondition.Paralyzed))
                sb.AppendLine("Paralyzed: Cannot perform actions");
            if (cond.HasFlag(BTCondition.Poison))
                sb.AppendLine("Poisoned: -1 HP every 8 minutes of game time (or combat round)");
            if (cond.HasFlag(BTCondition.Possessed))
                sb.AppendLine("Possessed: Character attacks party members");
            if (cond.HasFlag(BTCondition.Stone))
                sb.AppendLine("Stone: Cannot perform actions");

            return sb.ToString();
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Str:{0}, IQ:{1}, Dex:{2}, Con:{3}, Lck:{4}",
                    Stats.Strength.ToString(),
                    Stats.IQ.ToString(),
                    Stats.Dexterity.ToString(),
                    Stats.Constitution.ToString(),
                    Stats.Luck.ToString());
            }
        }



        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2}",
                 new PermAndTemp(LevelMod, Level).ToString(),
                 BTCharacter.RaceString(Race),
                 BTCharacter.ClassString(BasicClass));
            }
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
                sb.AppendFormat("Condition: {0}\r\n", BTCharacter.ConditionString(Condition, GetExtraConditions(), true));
                sb.AppendFormat("HP: {0}\r\n", HitPoints.ToString());
                sb.AppendFormat("SP: {0}\r\n", SpellPoints.ToString());
                sb.AppendFormat("AC: {0}\r\n", ArmorClass);
                sb.AppendFormat("Melee: {0}\r\n", MeleeDamageString);
                sb.AppendFormat("Equipped: {0}\r\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\r\n", BackpackString);
                sb.AppendFormat("Gold: {0}\r\n", Gold);
                return sb.ToString();
            }
        }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level, LevelMod - Level); } }
        public override StatAndModifier BasicAC { get { return new StatAndModifier(ArmorClass, 0); } }
        public override OneByteStat QuickRefSpellLevel { get { return new OneByteStat(99, 99); } }

        public static GenericRace GetBasicRace(BTRace race)
        {
            switch (race)
            {
                case BTRace.Human: return GenericRace.Human;
                case BTRace.Elf: return GenericRace.Elf;
                case BTRace.Gnome: return GenericRace.Gnome;
                case BTRace.Dwarf: return GenericRace.Dwarf;
                case BTRace.Hobbit: return GenericRace.Hobbit;
                case BTRace.HalfElf: return GenericRace.HalfElf;
                case BTRace.HalfOrc: return GenericRace.HalfOrc;
                default: return GenericRace.None;
            }
        }

        public override GenericRace BasicRace { get { return GetBasicRace(Race); } }
        public override bool IsCaster { get { return SpellLevel != null && SpellLevel.KnowsAnySpells; } }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return new MMHitPoints(HitPoints.Temporary, HitPoints.Permanent); } }
        public override string QuickRefCondition { get { return BTCharacter.ConditionString(Condition, GetExtraConditions(), false); } }

        public override BasicConditionFlags BasicCondition { get { return GetBasicCondition(Condition); } }

        public static BasicConditionFlags GetBasicCondition(BTCondition BT1Condition)
        {
            BasicConditionFlags cond = BasicConditionFlags.Good;

            if (BT1Condition.HasFlag(BTCondition.Dead))
                cond |= BasicConditionFlags.Dead;
            if (BT1Condition.HasFlag(BTCondition.Nuts))
                cond |= BasicConditionFlags.Insane;
            if (BT1Condition.HasFlag(BTCondition.Old))
                cond |= BasicConditionFlags.Old;
            if (BT1Condition.HasFlag(BTCondition.Paralyzed))
                cond |= BasicConditionFlags.Paralyzed;
            if (BT1Condition.HasFlag(BTCondition.Poison))
                cond |= BasicConditionFlags.Poisoned;
            if (BT1Condition.HasFlag(BTCondition.Possessed))
                cond |= BasicConditionFlags.Confused;
            if (BT1Condition.HasFlag(BTCondition.Stone))
                cond |= BasicConditionFlags.Stone;

            return cond;
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            byte b = 0;
            if (condition.HasFlag(BasicConditionFlags.Dead))
                b |= (byte)BTCondition.Dead;
            if (condition.HasFlag(BasicConditionFlags.Insane))
                b |= (byte)BTCondition.Nuts;
            if (condition.HasFlag(BasicConditionFlags.Old))
                b |= (byte)BTCondition.Old;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                b |= (byte)BTCondition.Paralyzed;
            if (condition.HasFlag(BasicConditionFlags.Poisoned))
                b |= (byte)BTCondition.Poison;
            if (condition.HasFlag(BasicConditionFlags.Confused))
                b |= (byte)BTCondition.Possessed;
            if (condition.HasFlag(BasicConditionFlags.Stone))
                b |= (byte)BTCondition.Stone;

            return b;
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)BTRace.Human;
                case GenericRace.Elf: return (byte)BTRace.Elf;
                case GenericRace.Dwarf: return (byte)BTRace.Dwarf;
                case GenericRace.Gnome: return (byte)BTRace.Gnome;
                case GenericRace.Hobbit: return (byte)BTRace.Hobbit;
                case GenericRace.HalfElf: return (byte)BTRace.HalfElf;
                case GenericRace.HalfOrc: return (byte)BTRace.HalfOrc;
                default: return 0;
            }
        }

        public static BT12Class ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Bard: return BT12Class.Bard;
                case GenericClass.Conjurer: return BT12Class.Conjurer;
                case GenericClass.Hunter: return BT12Class.Hunter;
                case GenericClass.Magician: return BT12Class.Magician;
                case GenericClass.Monk: return BT12Class.Monk;
                case GenericClass.Paladin: return BT12Class.Paladin;
                case GenericClass.Rogue: return BT12Class.Rogue;
                case GenericClass.Sorcerer: return BT12Class.Sorcerer;
                case GenericClass.Warrior: return BT12Class.Warrior;
                case GenericClass.Wizard: return BT12Class.Wizard;
                case GenericClass.Archmage: return BT12Class.Archmage;
                default: return BT12Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 2)
                return null;

            return BTItem.FromInventoryBytes(Game, bytes, offset);
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                bool[] items = new bool[8] { false, false, false, false, false, false, false, false };
                foreach(Item item in Inventory.Items)
                    if (item.MemoryIndex >= 0 && item.MemoryIndex < 8)
                        items[item.MemoryIndex] = true;
                for (int i = 0; i < items.Length; i++)
                    if (!items[i])
                        return i;
                return -1;
            }
        }

        public override bool BackpackFull
        {
            get
            {
                return (FirstEmptyBackpackIndex == -1);
            }
        }

        public override string ExperienceString
        {
            get
            {
                if (Level >= MaxLevel)
                    return String.Format("{0} (Max Level)", Experience);
                return String.Format("{0}{1}", Experience, ReadyToTrain ? " (Train!)" : ("/" + XPForNextLevel.ToString()));
            }
        }

        public override int TrainableLevel
        {
            get
            {
                int iLevel = Level;
                while (XPForLevel(Game, BasicClass, iLevel + 1) <= Experience && iLevel < 255)
                    iLevel++;
                return iLevel;
            }
        }

        public override long NeedsXP { get { return XPForNextLevel - Experience; } }
        public override long XPForNextLevel { get { return XPForLevel(Game, BasicClass, Level + 1); } }
        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass gc, int iLevel)
        {
            return XPForLevel(Game, gc, iLevel);
        }

        public static int[] XPBT1MonkConjurerMagician = new int[] { 1800, 4000, 6000, 10000, 14000, 19000, 29000, 50000, 90000, 120000, 170000, 230000, 460000 };
        public static int[] XPBT1Sorcerer = new int[] { 7000, 15000, 25000, 40000, 60000, 80000, 100000, 130000, 170000, 220000, 300000, 400000, 800000 };
        public static int[] XPBT1Wizard = new int[] { 20000, 50000, 80000, 120000, 160000, 200000, 250000, 300000, 400000, 600000, 900000, 1300000, 2600000 };

        public static int[] XPBT2MonkConjurerMagician = new int[] { 2000, 4000, 6000, 10000, 14000, 19000, 29000, 50000, 90000, 120000, 170000, 230000, 430000 };
        public static int[] XPBT2Sorcerer = new int[] { 7000, 14000, 25000, 40000, 60000, 80000, 100000, 130000, 170000, 220000, 300000, 400000, 600000 };
        public static int[] XPBT2Wizard = new int[] { 20000, 50000, 80000, 120000, 160000, 200000, 250000, 300000, 400000, 600000, 900000, 1300000, 1500000 };

        public static int[] XPArchmage = new int[] { 100000, 220000, 400000, 600000, 800000, 1100000, 1400000, 1800000, 2200000, 2600000, 3000000, 4000000, 4200000};
        public static int[] XPOther = new int[] { 2000, 4000, 7000, 10000, 15000, 20000, 30000, 50000, 80000, 110000, 150000, 200000, 400000 };

        public static long XPForLevel(GameNames game, GenericClass btClass, int iLevel)
        {
            int[] increase = XPOther;
            bool bBT1 = (game == GameNames.BardsTale1);

            int iStart = 2000;
            switch (btClass)
            {
                case GenericClass.Monk:
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                    iStart = 1800;
                    increase = bBT1 ? XPBT1MonkConjurerMagician : XPBT2MonkConjurerMagician;
                    break;
                case GenericClass.Sorcerer:
                    iStart = 7000;
                    increase = bBT1 ? XPBT1Sorcerer : XPBT2Sorcerer;
                    break;
                case GenericClass.Wizard:
                    iStart = 20000;
                    increase = bBT1 ? XPBT1Wizard : XPBT2Wizard;
                    break;
                case GenericClass.Archmage:
                    iStart = 100000;
                    increase = XPArchmage;
                    break;
                default:
                    break;
            }

            long iExpForNext = 0;
            if (iLevel < 2)
                return iStart;
            if (iLevel < 15)
                iExpForNext = increase[iLevel - 2];
            else
                iExpForNext = increase[12] + ((iLevel - 14) * (increase[12] - increase[11]));

            if (iExpForNext < 0)
                return 0;

            return iExpForNext;
        }

        public override bool ReadyToTrain
        {
            get
            {
                return NeedsXP < 1;
            }
        }

        public static int BaseHPForClass(BT12Class btClass)
        {
            switch (btClass)
            {
                case BT12Class.Paladin:
                case BT12Class.Warrior:
                case BT12Class.Hunter:
                case BT12Class.Bard: return 16;
                case BT12Class.Rogue: 
                case BT12Class.Monk:
                case BT12Class.Sorcerer:
                case BT12Class.Wizard: return 8;
                case BT12Class.Conjurer:
                case BT12Class.Magician: return 4;
                default:
                    return 0;
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (spell == null)
                return false;
            if (BasicClass == GenericClass.Bard && spell.Type == SpellType.Bard)
                return true;
            if (SpellLevel == null)
                return false;
            return SpellLevel.IsKnown(spell);
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Strength: return StatModifier.FromTablePlus(value, stat, 1, 1, 17, 0);
                case PrimaryStat.Dexterity: return StatModifier.FromTablePlus(value, stat, 1, 1, 16, 0);
                default: return StatModifier.FromTablePlus(value, stat, 1, 1, 15, 0);
            }
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (BTItem item in Inventory.SelectEquippedItems)
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
                foreach (BTItem item in Inventory.SelectUnequippedItems)
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
                BasicDamage dmg = Inventory.MeleeWeaponDamage;
                if (dmg.Max == 0 && BasicClass == GenericClass.Monk)
                {
                    if (Level < 3) return "4";
                    if (Level < 5) return "8";
                    if (Level < 9) return "16";
                    if (Level < 13) return "32";
                    if (Level < 17) return "40";
                    if (Level < 25) return "48";
                    if (Level < 31) return "56";
                    if (Level < 40) return "80";
                    if (Level < 49) return "96";
                    if (Level < 56) return "128";
                    if (Level < 62) return "160";
                    if (Level < 64) return "192";
                    return "234";
                }

                return Inventory.MeleeWeaponDamage.ToString(); 
            }
        }

        public override string GetACFormula(int iBless = 0)
        {
            return String.Format("Note: Lower AC improves to-hit chance (-10 min)\r\n{0}\tDexterity modifier", Global.AddPlus(-GetStatModifier(BasicDexterity.Temporary, PrimaryStat.Dexterity).Value));
        }

        public override Modifiers InternalModifiers
        {
            get
            {
                Modifiers mod = new Modifiers();

                foreach (BTItem item in Inventory.SelectEquippedItems)
                {
                    if (item.ArmorClass != 0)
                        mod.Adjust(ModAttr.ArmorClass, -item.ArmorClass, item.DescriptionString);
                }
                return mod;
            }
        }

    }
}