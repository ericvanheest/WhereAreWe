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
    public class UltimaCharacterOffsets : CharacterOffsets
    {
        public override int Name => 0;
        public override int NameLength => 15;
        public override int Race => 16;
        public override int Class => 18;
        public override int Sex => 20;
        public override int CurrentHP => 22;
        public override int Stats => 24;
        public override int Gold => 36;  // Copper/Silver/Gold is calculated (gold=100, silver=10, copper=1)
        public override int Experience => 38;  // Level is just (Experience / 1000) + 1  (max level 33; negative values cause trouble)
        public override int ExperienceLength => 2;
        public override int Food => 40;
        public override int Inventory => 76;
        public override int InventoryLength => 90;
        public override int LocationX => 162;
        public override int LocationY => 164;
        public override int ReadyWeapon => 42;
        public override int ReadySpell => 44;
        public override int ReadyArmor => 46;
        public override int Quests => 60;
        public override int QuestsLength => 16;
        public override int LastUsed => 168;

    }

    public enum UltimaRace
    {
        None = -1,
        Human = 0,
        Elf = 1,
        Dwarf = 2,
        Bobbit = 3,
        Last
    }

    public enum UltimaSex
    {
        None = -1,
        Male = 0,
        Female = 1,
        Last
    }

    public enum UltimaClass
    {
        None = -1,
        Fighter = 0,
        Cleric = 1,
        Wizard = 2,
        Thief = 3,
        Last
    }

    public abstract class UltimaStats
    {
        public int Strength;
        public int Agility;
        public int Stamina;
        public int Charisma;
        public int Intelligence;
        public int Wisdom;

        public int StrengthOffset => 0;
        public int AgilityOffset => 2;
        public int StaminaOffset => 4;
        public int CharismaOffset => 6;
        public int WisdomOffset => 8;
        public int IntelligenceOffset => 10;

        public void SetBytes(byte[] bytes, int offset)
        {
            Strength = BitConverter.ToInt16(bytes, offset + StrengthOffset);
            Agility = BitConverter.ToInt16(bytes, offset + AgilityOffset);
            Stamina = BitConverter.ToInt16(bytes, offset + StaminaOffset);
            Charisma = BitConverter.ToInt16(bytes, offset + CharismaOffset);
            Wisdom = BitConverter.ToInt16(bytes, offset + WisdomOffset);
            Intelligence = BitConverter.ToInt16(bytes, offset + IntelligenceOffset);
        }
    }

    public abstract class UltimaInventory : Inventory
    {
        protected List<Item> m_items;

        public override List<Item> Items { get { return m_items; } set { m_items = value; } }

        public static UltimaInventory Create(GameNames game, byte[] bytes, byte[] bytesItemTable, int offset = 0)
        {
            return new Ultima1Inventory(bytes, offset, -1, -1, -1);
        }

        public static UltimaInventory Create(GameNames game, List<Item> items)
        {
            return new Ultima1Inventory(items);
        }

        public abstract byte[] GetBytes();
        public abstract byte[] GetReadyBytes();
        public abstract void SetBytes(byte[] bytes, int offset, int readyWeapon, int readySpell, int readyArmor);
    }

    public abstract class UltimaCharacter : UltimaBaseCharacter
    {
        public string CharName;
        public UltimaRace Race;
        public UltimaSex Sex;
        public UltimaStats Stats;
        public UltimaClass Class;
        public int Hits;
        public int Experience;
        public UltimaInventory Inventory;
        public int Food;
        public int Coin;

        public override StatAndModifier BasicStrength { get { return new StatAndModifier(Stats.Strength, 0); } }
        public override StatAndModifier BasicIntelligence { get { return new StatAndModifier(Stats.Intelligence, 0); } }
        public override StatAndModifier BasicWisdom { get { return new StatAndModifier(Stats.Wisdom, 0); } }
        public override StatAndModifier BasicAgility { get { return new StatAndModifier(Stats.Agility, 0); } }
        public override StatAndModifier BasicStamina { get { return new StatAndModifier(Stats.Stamina, 0); } }
        public override StatAndModifier BasicCharisma { get { return new StatAndModifier(Stats.Charisma, 0); } }
        public override GenericClass BasicClass => GetBasicClass(Class);
        public override int BasicFood => Food;

        protected GameNames m_game = GameNames.Ultima1;

        public int Address = 0;

        public UltimaCharacter()
        {
            Address = 0;
        }

        public override GameNames Game => m_game;
        public override int BasicAddress => Address;
        public abstract CheatOffsets GetInventoryCheatOffsets(int iIndex);
        public override Inventory BasicInventory => Inventory;
        public override MMSex BasicSex => Sex == UltimaSex.Male ? MMSex.Male : MMSex.Female;
        public static string SexString(UltimaSex sex) => sex == UltimaSex.Male ? "Male" : "Female";
        public override byte SexValue(MMSex sex) => sex == MMSex.Female ? (byte) 1 : (byte) 0;

        public static UltimaCharacter Create(GameNames game, byte[] bytes = null, int iIndex = -1)
        {
            UltimaCharacter ultimaChar = null;
            ultimaChar = new Ultima1Character();

            if (bytes != null)
                ultimaChar.SetFromBytes(game, bytes, iIndex);
            return ultimaChar;
        }

        public void SetFromBytes(GameNames game, byte[] bytes, int iIndex)
        {
            m_game = game;
            Address = 0;
            if (bytes == null || bytes.Length < iIndex + CharacterSize - 1)
                return;
            SetCharFromStream(0, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null, null, false, null);
        }

        public abstract UltimaStats CreateStats(byte[] bytes, int offset);
        public override int MaxBackpackSize => 48;
        public override string Name => CharName;
        public override List<Item> BackpackItems => Inventory.SelectUnequippedItems;

        public int ItemCount(UltimaItemIndex index)
        {
            if (Inventory == null)
                return 0;
            foreach (UltimaItem item in Inventory.Items)
            {
                if (item.ItemIndex == index)
                    return item.Count;
            }
            return 0;
        }

        public static string RaceString(UltimaRace race)
        {
            switch (race)
            {
                case UltimaRace.None: return "None";
                case UltimaRace.Human: return "Human";
                case UltimaRace.Elf: return "Elf";
                case UltimaRace.Dwarf: return "Dwarf";
                case UltimaRace.Bobbit: return "Bobbit";
                default: return String.Format("Unknown({0})", (int)race);
            }
        }

        public static string ClassString(UltimaClass classenum)
        {
            switch (classenum)
            {
                case UltimaClass.Fighter: return "Fighter";
                case UltimaClass.Cleric: return "Cleric";
                case UltimaClass.Wizard: return "Wizard";
                case UltimaClass.Thief: return "Thief";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Str:{0}, Agi:{1}, Sta: {2}, Cha:{3}, Wis:{4}, Int:{5}",
                    Stats.Strength.ToString(),
                    Stats.Agility.ToString(),
                    Stats.Stamina.ToString(),
                    Stats.Charisma.ToString(),
                    Stats.Wisdom.ToString(),
                    Stats.Intelligence.ToString());
            }
        }

        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3}",
                 BasicLevelString,
                 Ultima1Character.SexString(Sex),
                 RaceString(Race),
                 ClassString(Class));
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
                sb.AppendFormat("HP: {0}\r\n", Hits.ToString());
                sb.AppendFormat("Coin: {0}\r\n", Coin.ToString());
                sb.AppendFormat("Equipped: {0}\r\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\r\n", BackpackString);
                return sb.ToString();
            }
        }

        public override StatAndModifier BasicLevel => new StatAndModifier(Experience / 1000 + 1, 0);
        public override string BasicLevelString => BasicLevel.Permanent.ToString();

        public static GenericRace GetBasicRace(UltimaRace race)
        {
            switch (race)
            {
                case UltimaRace.Human: return GenericRace.Human;
                case UltimaRace.Elf: return GenericRace.Elf;
                case UltimaRace.Dwarf: return GenericRace.Dwarf;
                case UltimaRace.Bobbit: return GenericRace.Bobbit;
                default: return GenericRace.None;
            }
        }

        public override GenericRace BasicRace { get { return GetBasicRace(Race); } }
        public override bool IsCaster => Class == UltimaClass.Wizard || Class == UltimaClass.Cleric;

        public override long QuickRefExperience => Experience;
        public override MMHitPoints QuickRefHitPoints => new MMHitPoints(Hits, Hits);

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)UltimaRace.Human;
                case GenericRace.Elf: return (byte)UltimaRace.Elf;
                case GenericRace.Dwarf: return (byte)UltimaRace.Dwarf;
                case GenericRace.Bobbit: return (byte)UltimaRace.Bobbit;
                default: return 0;
            }
        }

        public static UltimaClass ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Fighter: return UltimaClass.Fighter;
                case GenericClass.Wizard: return UltimaClass.Wizard;
                case GenericClass.Cleric: return UltimaClass.Cleric;
                case GenericClass.Thief: return UltimaClass.Thief;
                default: return UltimaClass.None;
            }
        }

        public static GenericClass GetBasicClass(UltimaClass ultimaClass)
        {
            switch (ultimaClass)
            {
                case UltimaClass.Fighter: return GenericClass.Fighter;
                case UltimaClass.Cleric: return GenericClass.Cleric;
                case UltimaClass.Thief: return GenericClass.Thief;
                case UltimaClass.Wizard: return GenericClass.Wizard;
                default: return GenericClass.None;
            }
        }

        public override byte ClassValue(GenericClass classVal) => (byte)ClassForGeneric(classVal);
        public override bool BackpackFull => false;
        public override string ExperienceString => String.Format("{0}/{1}", Experience, BasicLevel.Permanent >= MaxLevel ? "Max" : XPForNextLevel.ToString());
        public override long XPForNextLevel => Experience >= 32000 ? -1 : ((Experience / 1000) + 1) * 1000;
        public override long BasicExperience => Experience;
        public override long XPForLevel(GenericClass gc, int iLevel) => (iLevel - 1) * 1000;

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (UltimaItem item in Inventory.SelectEquippedItems)
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
                foreach (UltimaItem item in Inventory.SelectUnequippedItems)
                    sb.AppendFormat("{0}, ", item.Name);
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(empty)";
                return sb.ToString();
            }
        }
    }
}