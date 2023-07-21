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
    public class BT2CharacterOffsets : CharacterOffsets
    {
        public override int Condition { get { return 17; } }
        public override int ConditionLength { get { return 2; } }
        public override int Race { get { return 19; } }
        public override int Class { get { return 20; } }
        public override int Stats { get { return 21; } }
        public override int ArmorClass { get { return 33; } }
        public override int CurrentHP { get { return 37; } }
        public override int MaxHP { get { return 35; } }
        public override int CurrentSP { get { return 39; } }
        public override int MaxSP { get { return 41; } }
        public override int Inventory { get { return 43; } }
        public override int InventoryLength { get { return 24; } }
        public override int Experience { get { return 67; } }
        public override int ExperienceLength { get { return 4; } }
        public override int Gold { get { return 71; } }
        public override int GoldLength { get { return 4; } }
        public override int LevelMod { get { return 75; } }
        public override int Level { get { return 76; } }
        public override int SpellLevel { get { return 77; } }
        public override int SpellLevelLength { get { return 5; } }
        public override int LevelLength { get { return 1; } }

        public override int BardSongs { get { return 86; } }
        public override int Name { get { return 0; } }
        public override int NameLength { get { return 15; } }
        public override int Swings { get { return 90; } }
        public override int Critical { get { return 85; } }
        public override int BattlesWon { get { return 93; } }
        public override int HideChance { get { return 82; } }
    }

    public class BT2Stats : BTStats
    {
        public override int StatSize { get { return 1; } }

        public BT2Stats(byte[] bytes, int offset = 0)
        {
            StrengthOffsetPerm = 5;
            StrengthOffsetTemp = 0;
            IQOffsetPerm = 6;
            IQOffsetTemp = 1;
            DexOffsetPerm = 7;
            DexOffsetTemp = 2;
            ConOffsetPerm = 8;
            ConOffsetTemp = 3;
            LuckOffsetPerm = 9;
            LuckOffsetTemp = 4;

            SetFromOneByte(bytes, offset);
        }
    }

    public class BT2Character : BTCharacter
    {
        public const int SizeInBytes = 103;
        public const int SizeInMemory = 86;
        public byte[] Unknown14;
        public byte[] Unknown42;
        public byte[] Unknown65;
        public BT12Class Class;

        public BT2Character()
        {
            m_game = GameNames.BardsTale2;
        }

        public override CheatOffsets GetInventoryCheatOffsets(int iIndex)
        {
            return new CheatOffsets(new int[] { Offsets.Inventory + (iIndex * 2), Offsets.Inventory + (iIndex * 2) + 1, Offsets.Inventory + iIndex + 16 });
        }

        public override CharacterOffsets Offsets { get { return BT2.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override BTStats CreateStats(byte[] bytes, int offset) { return new BT2Stats(RawBytes, Offsets.Stats); }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemList = null)
        {
            if (stream.Length < CharacterSize)
                return;

            if (info != null)
                m_game = info.Game;

            RawBytes = new byte[CharacterSize + BT2.Offsets.NameLength];
            stream.Read(RawBytes, 0, CharacterSize);

            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, 15).TrimEnd(new char[] { ' ', '\0' });
            Condition = (BTCondition)BitConverter.ToInt16(RawBytes, Offsets.Condition);
            Race = (BTRace)RawBytes[Offsets.Race];
            Class = (BT12Class)RawBytes[Offsets.Class];
            Stats = CreateStats(RawBytes, Offsets.Stats);
            ArmorClass = BitConverter.ToInt16(RawBytes, Offsets.ArmorClass);
            HitPoints = new TwoByteStat(RawBytes, Offsets.CurrentHP, Offsets.MaxHP);
            SpellPoints = new TwoByteStat(RawBytes, Offsets.CurrentSP, Offsets.MaxSP);
            Inventory = new BT2Inventory(RawBytes, Offsets.Inventory);
            Experience = BitConverter.ToInt32(RawBytes, Offsets.Experience);
            Gold = BitConverter.ToInt32(RawBytes, Offsets.Gold);
            Level = RawBytes[Offsets.Level];
            LevelMod = RawBytes[Offsets.LevelMod];
            SpellLevel = new BTSpellLevel(RawBytes, Offsets.SpellLevel, 5);
            Unknown14 = Global.Subset(RawBytes, 14, 2);
            m_iSongs = RawBytes[Offsets.BardSongs];
            Unknown42 = Global.Subset(RawBytes, 42, 8);
            Unknown65 = Global.Subset(RawBytes, 65, 4);
            BattlesWon = BitConverter.ToInt16(RawBytes, Offsets.BattlesWon);
            HideChance = RawBytes[Offsets.HideChance];
            CriticalChance = RawBytes[Offsets.Critical];
            NumAttacks = RawBytes[Offsets.Swings];

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
            bytes[Offsets.Level] = (byte)Level;
            bytes[Offsets.LevelMod] = (byte)LevelMod;
            SpellLevel.SetBytes(bytes, Offsets.SpellLevel, 5);
            Global.SetInt16(bytes, Offsets.BardSongs, Songs);
            Buffer.BlockCopy(Unknown14, 0, bytes, 14, Unknown14.Length);
            Buffer.BlockCopy(Unknown42, 0, bytes, 42, Unknown42.Length);
            Buffer.BlockCopy(Unknown65, 0, bytes, 65, Unknown65.Length);
            Global.SetInt16(bytes, Offsets.BattlesWon, BattlesWon);
            bytes[Offsets.HideChance] = (byte)HideChance;
            bytes[Offsets.Critical] = (byte)CriticalChance;
            Global.SetInt16(bytes, Offsets.Swings, NumAttacks);

            stream.Write(bytes, 0, CharacterSize);
        }

        public override GenericClass BasicClass { get { return BT1Character.GetBasicClass(Class); } }

        public override string GetCurrentQuest(MemoryHacker hacker)
        {
            BT2MemoryHacker bt2Hacker = hacker as BT2MemoryHacker;
            if (bt2Hacker == null)
                return base.GetCurrentQuest(hacker);

            HashSet<BT2ItemIndex> items = bt2Hacker.GetAllInventoryItems();
            if (items.Contains(BT2ItemIndex.TheScepter))
                return "Defeat Lagoth Zanta";
            if (!items.Contains(BT2ItemIndex.WandSegment1))
                return "Go to Ephesus and retrieve Wand Segment 1";
            if (!items.Contains(BT2ItemIndex.MasterKey))
                return "Go to Ephesus and purchase a Master Key";
            if (!items.Contains(BT2ItemIndex.WandSegment2))
                return "Go to Fanskar's Castle and retrieve Wand Segment 2";
            if (!items.Contains(BT2ItemIndex.WandSegment3))
                return "Go to Philippi and retrieve Wand Segment 3";
            if (!items.Contains(BT2ItemIndex.WandSegment4))
                return "Go to Thessalonica and retrieve Wand Segment 4";
            if (!items.Contains(BT2ItemIndex.WandSegment5))
                return "Go to Corinth and retrieve Wand Segment 5";
            if (!items.Contains(BT2ItemIndex.WandSegment6))
                return "Go to the Grey Crypt and retrieve Wand Segment 6";
            if (!items.Contains(BT2ItemIndex.WandSegment7))
                return "Go to Colosse and retrieve Wand Segment 7";
            return "Defeat Lagoth Zanta";
        }
    }

    public class BT2Inventory : BTInventory
    {
        public BT2Inventory(byte[] bytes, int offset = 0)
        {
            // A Bard's Tale 2 inventory is 8 Int16s followed by 8 bytes (charges)

            m_items = new List<Item>(8);

            for (int i = 0; i < 8; i++)
            {
                if (i * 3 + offset > bytes.Length)
                    break;  // Not enough bytes for a proper inventory

                BTItem item = BTItem.FromInventoryBytes(GameNames.BardsTale2, bytes, offset + (i * 2));
                if (item != null)
                {
                    if (item.Index > 0)
                    {
                        item.MemoryIndex = i;
                        item.DisplayIndex = String.Format("{0}.", i + 1);
                        m_items.Add(item);
                    }
                    item.ChargesCurrent = bytes[offset + i + 16];
                }
            }
        }

        public BT2Inventory(List<Item> items)
        {
            m_items = items;
        }

        public override byte[] GetBytes(GenericClass gc = GenericClass.None)
        {
            byte[] bytes = new byte[24];
            SetBytes(bytes, 0, gc);
            return bytes;
        }
    }
}