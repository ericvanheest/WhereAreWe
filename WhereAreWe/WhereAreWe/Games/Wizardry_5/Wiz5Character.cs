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
    public class Wiz5SpellPoints : WizSpellPoints
    {
        new public static Wiz5SpellPoints Zero { get { return new Wiz5SpellPoints(Global.NullBytes(16), 0); } }

        public Wiz5SpellPoints() : base()
        {
        }

        public Wiz5SpellPoints(byte[] bytes, int offset)
        {
            if (bytes.Length - offset < 16)
                return;

            Mage = new int[7];
            Priest = new int[7];

            for (int i = 0; i < 7; i++)
            {
                Mage[i] = bytes[offset + i];
                Priest[i] = bytes[offset + 8 + i];
            }
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = Global.NullBytes(16);
            for (int i = 0; i < 7; i++)
            {
                bytes[i] = (byte)Mage[i];
                bytes[i + 8] = (byte)Priest[i];
            }
            return bytes;
        }
    }

    public class Wiz5CharacterOffsets : CharacterOffsets
    {
        // "240" is "not implemented" at the moment
        public override int NameLength { get { return 0; } }
        public override int Name { get { return 1; } }
        public override int PasswordLength { get { return 16; } }
        public override int Password { get { return 17; } }
        public override int Out { get { return 240; } }
        public override int Race { get { return 32; } }
        public override int Class { get { return 34; } }
        public override int Age { get { return 42; } }
        public override int Condition { get { return 40; } }
        public override int Alignment { get { return 36; } }
        public override int ConditionLength { get { return 2; } }
        public override int Stats { get { return 44; } }
        public override int SavingThrows { get { return 48; } }
        public override int Gold { get { return 52; } }
        public override int GoldLength { get { return 6; } }
        public override int Inventory { get { return 58; } }
        public override int InventoryLength { get { return 32; } }
        public override int Experience { get { return 92; } }
        public override int ExperienceLength { get { return 6; } }
        public override int LevelMod { get { return 98; } }
        public override int Level { get { return 100; } }
        public override int CurrentHP { get { return 102; } }
        public override int MaxHP { get { return 104; } }
        public override int CurrentSP { get { return 114; } }
        public override int LastArmorClass { get { return 240; } }
        public override int ArmorClass { get { return 132; } }
        public override int Regeneration { get { return 240; } }
        public override int Critical { get { return 240; } }
        public override int Swings { get { return 182; } }
        public override int MeleeDamage { get { return 240; } }
        public override int WeaponEffects { get { return 230; } }
        public override int LocationX { get { return 240; } }
        public override int LocationY { get { return 240; } }
        public override int LocationZ { get { return 240; } }
        public override int Awards { get { return 172; } }
        public override int AwardsLength { get { return 2; } }
        public override int Spells { get { return 106; } }
        public override int SpellsLength { get { return 8; } }
        public override int Poison { get { return 138; } }
        public override int Marks { get { return 162; } }
        public override int RIP { get { return 168; } }
        public override int Swim { get { return 192; } }
    }

    public class Wiz5Character : WizCharacter
    {
        new public const int SizeInBytes = 246;

        public Wiz5Character()
        {
            Address = -1;
        }

        public override GameNames Game { get { return GameNames.Wizardry5; } }
        public override string PoisonString { get { return PoisonCounter == 0 ? "None" : string.Format("{0}", PoisonCounter); } }

        public static Wiz5Character Create(int iCharIndex, byte[] bytes, int iIndex, Wiz5GameInfo gameInfo, WizEncounterInfo encounterInfo)
        {
            Wiz5Character wizChar = new Wiz5Character();
            wizChar.SetFromBytes(iCharIndex, bytes, iIndex, gameInfo, encounterInfo);
            return wizChar;
        }

        public void SetFromBytes(int iCharIndex, byte[] bytes, int iIndex, Wiz5GameInfo gameInfo, WizEncounterInfo encounterInfo)
        {
            m_game = GameNames.Wizardry5;
            Address = iCharIndex;
            if (bytes == null || bytes.Length < iIndex + SizeInBytes - 1)
                return;
            SetCharFromStream(iCharIndex, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), gameInfo, encounterInfo);
        }

        public void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null)
        {
            if (stream.Length < CharacterSize)
                return;

            base.SetCharFromStream(iCharIndex, stream, info, encounterInfo, false);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 4)
                return null;

            return WizItem.FromWiz5InventoryBytes(bytes, offset);
        }

        public override CharacterOffsets Offsets { get { return Wiz5.Offsets; } }
        public override int BasicAddress { get { return Address; } }
        public override int CharacterSize { get { return SizeInBytes; } }

        public override long XPForNextLevel { get { return XPForLevel(Class, Level + 1); } }

        public override long XPForLevel(GenericClass gClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(gClass), iLevel);
        }

        public override long[] LevelArray(WizClass wizClass)
        {
            switch (wizClass)
            {
                case WizClass.Fighter: return new long[] { 0, 0, 800, 1666, 2776, 4626, 7710, 12850, 21416, 35693, 59488, 99146, 165243, 275405, 500810 };
                case WizClass.Mage: return new long[] { 0, 0, 1000, 2083, 3471, 5785, 9641, 16068, 26780, 44633, 74388, 123980, 206633, 344388, 638776 };
                case WizClass.Priest: return new long[] { 0, 0, 900, 1833, 3055, 5091, 8485, 14141, 23568, 39280, 65466, 109110, 181850, 303083, 556166 };
                case WizClass.Thief: return new long[] { 0, 0, 750, 1583, 2638, 4396, 7326, 12210, 20350, 33916, 56526, 94210, 157016, 261693, 473386 };
                case WizClass.Bishop: return new long[] { 0, 0, 1200, 2416, 4026, 6710, 11183, 18638, 31063, 51771, 86285, 143808, 239680, 399466, 748932 };
                case WizClass.Samurai: return new long[] { 0, 0, 1100, 2215, 3579, 6056, 10248, 17342, 29348, 49665, 84048, 142235, 240705, 407346, 764692 };
                case WizClass.Lord: return new long[] { 0, 0, 1100, 2215, 3579, 6056, 10248, 17342, 29348, 49665, 84048, 142235, 240705, 407346, 764692 };
                case WizClass.Ninja: return new long[] { 0, 0, 1100, 2284, 3865, 6540, 11067, 18728, 31693, 53634, 90765, 153602, 259941, 439900, 819800 };
                default: return new long[] { 0, 0, 800, 1666, 2776, 4626, 7710, 12850, 21416, 35693, 59488, 99146, 165243, 275405, 500810 };
            }
        }

        public override long XPForLevel(WizClass wizClass, int iLevel)
        {
            long[] levels = LevelArray(wizClass);

            if (iLevel < 2)
                return 0;
            if (iLevel < 14)
                return levels[iLevel];
            else
                return levels[13] + ((levels[14] - levels[13]) * (iLevel - 13));
        }
    }

    public class Wiz5Inventory : WizInventory
    {
        public Wiz5Inventory(List<Item> items) { m_items = items; }

        public Wiz5Inventory(GameNames game, byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < Wiz5.Offsets.InventoryLength)
                return;

            // A Wizardry 5 inventory is an Int16 followed by up to eight 4-byte items
            int iNumItems = BitConverter.ToInt16(bytes, offset);

            m_items = new List<Item>(iNumItems);
            for (int i = 0; i < iNumItems; i++)
            {
                if (i * 4 + 2 > bytes.Length - 4 - offset)
                    break;  // Not enough bytes for the item count

                WizItem item = WizItem.FromWiz5InventoryBytes(bytes, offset + 2 + (i * 4));
                if (item != null)
                {
                    item.MemoryIndex = i;
                    item.DisplayIndex = String.Format("{0}", i + 1);
                    m_items.Add(item);
                }
            }
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[34];

            for (int i = 0; i < 34; i++)
                bytes[i] = 0;

            Global.SetInt16(bytes, 0, m_items.Count);

            int iIndexItem = 0;
            foreach (Wiz5Item item in m_items)
            {
                Global.SetInt16(bytes, 2 + (iIndexItem * 4), item.DiskIndex);
                Global.SetInt16(bytes, 2 + (iIndexItem * 4 + 2), item.IdentifyFlagValue);
                iIndexItem++;
            }

            return bytes;
        }
    }
}