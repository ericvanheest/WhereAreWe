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
    public class BT1CharacterOffsets : CharacterOffsets
    {
        public override int Condition { get { return 17; } }
        public override int ConditionLength { get { return 1; } }
        public override int Race { get { return 19; } }
        public override int Class { get { return 21; } }
        public override int Stats { get { return 23; } }
        public override int ArmorClass { get { return 43; } }
        public override int MaxHP { get { return 45; } }
        public override int CurrentHP { get { return 47; } }
        public override int CurrentSP { get { return 49; } }
        public override int MaxSP { get { return 51; } }
        public override int Inventory { get { return 53; } }
        public override int InventoryLength { get { return 16; } }
        public override int Experience { get { return 69; } }
        public override int ExperienceLength { get { return 4; } }
        public override int Gold { get { return 73; } }
        public override int GoldLength { get { return 4; } }
        public override int LevelMod { get { return 77; } }
        public override int Level { get { return 79; } }
        public override int SpellLevel { get { return 81; } }
        public override int SpellLevelLength { get { return 4; } }
        public override int HideChance { get { return 85; } }
        public override int Critical { get { return 91; } }
        public override int BardSongs { get { return 93; } }
        public override int Swings { get { return 101; } }
        public override int BattlesWon { get { return 105; } }
        public override int Name { get { return 0; } }
        public override int NameLength { get { return 15; } }
    }

    public class BT1Stats : BTStats
    {
        public override int StatSize { get { return 2; } }

        public BT1Stats(byte[] bytes, int offset = 0)
        {
            StrengthOffsetPerm = 10;
            StrengthOffsetTemp = 0;
            IQOffsetPerm = 12;
            IQOffsetTemp = 2;
            DexOffsetPerm = 14;
            DexOffsetTemp = 4;
            ConOffsetPerm = 16;
            ConOffsetTemp = 6;
            LuckOffsetPerm = 18;
            LuckOffsetTemp = 8;

            SetFromTwoBytes(bytes, offset);
        }

        public override void SetBytes(byte[] bytes, int offset)
        {
            Global.SetInt16(bytes, offset + StrengthOffsetTemp, Strength.Temporary);
            Global.SetInt16(bytes, offset + IQOffsetTemp, IQ.Temporary);
            Global.SetInt16(bytes, offset + DexOffsetTemp, Dexterity.Temporary);
            Global.SetInt16(bytes, offset + ConOffsetTemp, Constitution.Temporary);
            Global.SetInt16(bytes, offset + LuckOffsetTemp, Luck.Temporary);
            Global.SetInt16(bytes, offset + StrengthOffsetPerm, Strength.Permanent);
            Global.SetInt16(bytes, offset + IQOffsetPerm, IQ.Permanent);
            Global.SetInt16(bytes, offset + DexOffsetPerm, Dexterity.Permanent);
            Global.SetInt16(bytes, offset + ConOffsetPerm, Constitution.Permanent);
            Global.SetInt16(bytes, offset + LuckOffsetPerm, Luck.Permanent);
        }
    }

    public class BT1Character : BTCharacter
    {
        public const int SizeInBytes = 109;
        public const int SizeInMemory = 92;
        public byte[] Unknown69;
        public byte[] Unknown79;
        public BT12Class Class;

        public BT1Character()
        {
        }

        public override CharacterOffsets Offsets { get { return BT1.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override BTStats CreateStats(byte[] bytes, int offset) { return new BT1Stats(RawBytes, Offsets.Stats); }

        public override CheatOffsets GetInventoryCheatOffsets(int iIndex)
        {
            return new CheatOffsets(new int[] { Offsets.Inventory + (iIndex * 2), Offsets.Inventory + (iIndex * 2) + 1 });
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false)
        {
            if (stream.Length < CharacterSize)
                return;

            if (info != null)
                m_game = info.Game;

            RawBytes = new byte[CharacterSize + BT1.Offsets.NameLength];
            stream.Read(RawBytes, 0, CharacterSize);

            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, 15).TrimEnd(new char[] { ' ', '\0' });
            Condition = (BTCondition)BitConverter.ToInt16(RawBytes, Offsets.Condition);
            Race = (BTRace)BitConverter.ToInt16(RawBytes, Offsets.Race);
            Class = (BT12Class)BitConverter.ToInt16(RawBytes, Offsets.Class);
            Stats = CreateStats(RawBytes, Offsets.Stats);
            ArmorClass = BitConverter.ToInt16(RawBytes, Offsets.ArmorClass);
            HitPoints = new TwoByteStat(RawBytes, Offsets.CurrentHP, Offsets.MaxHP);
            SpellPoints = new TwoByteStat(RawBytes, Offsets.CurrentSP, Offsets.MaxSP);
            Inventory = new BT1Inventory(RawBytes, Offsets.Inventory);
            Experience = BitConverter.ToInt32(RawBytes, Offsets.Experience);
            Gold = BitConverter.ToInt32(RawBytes, Offsets.Gold);
            Level = BitConverter.ToInt16(RawBytes, Offsets.Level);
            LevelMod = BitConverter.ToInt16(RawBytes, Offsets.LevelMod);
            SpellLevel = new BTSpellLevel(RawBytes, Offsets.SpellLevel, 4);
            Unknown69 = Global.Subset(RawBytes, 69, 8);
            m_iSongs = BitConverter.ToInt16(RawBytes, Offsets.BardSongs);
            Unknown79 = Global.Subset(RawBytes, 79, 14);
            BattlesWon = BitConverter.ToInt16(RawBytes, Offsets.BattlesWon);
            HideChance = BitConverter.ToInt16(RawBytes, Offsets.HideChance);
            CriticalChance = BitConverter.ToInt16(RawBytes, Offsets.Critical);
            NumAttacks = BitConverter.ToInt16(RawBytes, Offsets.Swings);

            Modifiers = Inventory.GetModifiers();
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            Global.SetInt16(bytes, Offsets.Condition, (int)Condition);
            Global.SetInt16(bytes, Offsets.Race, (int)Race);
            Global.SetInt16(bytes, Offsets.Class, (int)Class);
            Stats.SetBytes(bytes, Offsets.Stats);
            Global.SetInt16(bytes, Offsets.ArmorClass, ArmorClass);
            HitPoints.SetBytes(bytes, Offsets.MaxHP, Offsets.CurrentHP);
            SpellPoints.SetBytes(bytes, Offsets.MaxSP, Offsets.CurrentSP);
            Inventory.SetBytes(bytes, Offsets.Inventory);
            Global.SetInt32(bytes, Offsets.Experience, Experience);
            Global.SetInt32(bytes, Offsets.Gold, Gold);
            Global.SetInt16(bytes, Offsets.Level, Level);
            Global.SetInt16(bytes, Offsets.LevelMod, LevelMod);
            SpellLevel.SetBytes(bytes, Offsets.SpellLevel, 4);
            Buffer.BlockCopy(Unknown69, 0, bytes, 69, Unknown69.Length);
            Global.SetInt16(bytes, Offsets.BardSongs, Songs);
            Buffer.BlockCopy(Unknown79, 0, bytes, 79, Unknown79.Length);
            Global.SetInt16(bytes, Offsets.BattlesWon, BattlesWon);
            bytes[Offsets.HideChance] = (byte)HideChance;
            bytes[Offsets.Critical] = (byte)CriticalChance;
            Global.SetInt16(bytes, Offsets.Swings, NumAttacks);

            // Bard's tale doesn't store the name with the rest of the character information
            //for (int i = Offsets.Name; i <= 15; i++)
            //    bytes[i] = 0x00;
            //Buffer.BlockCopy(Encoding.ASCII.GetBytes(CharName), 0, bytes, Offsets.Name, CharName.Length);

            stream.Write(bytes, 0, CharacterSize);
        }

        public static GenericClass GetBasicClass(BT12Class btClass)
        {
            switch (btClass)
            {
                case BT12Class.Bard: return GenericClass.Bard;
                case BT12Class.Conjurer: return GenericClass.Conjurer;
                case BT12Class.Hunter: return GenericClass.Hunter;
                case BT12Class.Magician: return GenericClass.Magician;
                case BT12Class.Monk: return GenericClass.Monk;
                case BT12Class.Paladin: return GenericClass.Paladin;
                case BT12Class.Rogue: return GenericClass.Rogue;
                case BT12Class.Sorcerer: return GenericClass.Sorcerer;
                case BT12Class.Warrior: return GenericClass.Warrior;
                case BT12Class.Wizard: return GenericClass.Wizard;
                case BT12Class.Archmage: return GenericClass.Archmage;
                case BT12Class.Monster: return GenericClass.Monster;
                default: return GenericClass.None;
            }
        }

        public override GenericClass BasicClass { get { return GetBasicClass(Class); }  }
    }

    public class BT1Inventory : BTInventory
    {
        public BT1Inventory(byte[] bytes, int offset = 0)
        {
            // A Bard's Tale inventory is just 8 Int16s

            m_items = new List<Item>(8);

            for (int i = 0; i < 8; i++)
            {
                if (i * 2 + offset > bytes.Length)
                    break;  // Not enough bytes for a proper inventory

                BTItem item = BTItem.FromInventoryBytes(GameNames.BardsTale1, bytes, offset + (i * 2));
                if (item != null)
                {
                    if (item.Index > 0)
                    {
                        item.MemoryIndex = i;
                        item.DisplayIndex = String.Format("{0}", i + 1);
                        m_items.Add(item);
                    }
                }
            }
        }

        public BT1Inventory(List<Item> items)
        {
            m_items = items;
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[24];
            SetBytes(bytes, 0);
            return bytes;
        }
    }
}