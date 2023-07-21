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
    public class EOB1CharacterOffsets : EOBCharacterOffsets
    {
    }

    public class EOB1Stats : EOBStats
    {
        public EOB1Stats(byte[] bytes, int offset = 0)
        {
            Strength = new OneByteStat(bytes[offset], bytes[offset+1]);
            Strength18 = new OneByteStat(bytes[offset + 2], bytes[offset + 3]);
            Intelligence = new OneByteStat(bytes[offset + 4], bytes[offset + 5]);
            Wisdom = new OneByteStat(bytes[offset + 6], bytes[offset + 7]);
            Dexterity = new OneByteStat(bytes[offset + 8], bytes[offset + 9]);
            Constitution = new OneByteStat(bytes[offset + 10], bytes[offset + 11]);
            Charisma = new OneByteStat(bytes[offset + 12], bytes[offset + 13]);
        }
    }

    public class EOB1Character : EOBCharacter
    {
        public const int SizeInBytes = 243;
        public const int SizeInMemory = 243;

        public EOB1Character()
        {
            m_game = GameNames.EyeOfTheBeholder1;
        }

        public OneByteStat m_hitPoints;
        public override CharacterOffsets Offsets { get { return EOB1.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override EOBStats CreateStats(byte[] bytes, int offset) { return new EOB1Stats(RawBytes, Offsets.Stats); }
        public override int MaxLevel { get { return Class == EOBClass.Cleric ? 10 : 11; } }
        public override int BasicHP { get { return HitPoints.Temporary; } }
        public override int BasicMaxHP { get { return HitPoints.Permanent; } }
        public override PermAndTemp HitPoints => m_hitPoints;

        public override CheatOffsets GetInventoryCheatOffsets(int iIndex)
        {
            return new CheatOffsets(new int[] { Offsets.Inventory + (iIndex * 2), Offsets.Inventory + (iIndex * 2) + 1 });
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemTable = null)
        {
            if (stream.Length < CharacterSize)
                return;

            if (info != null)
                m_game = info.Game;

            RawBytes = new byte[CharacterSize];
            stream.Read(RawBytes, 0, CharacterSize);

            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, Offsets.NameLength).TrimEnd(new char[] { ' ', '\0' });
            Condition = (EOBCondition) RawBytes[Offsets.Condition];
            Race = (EOBRace)RawBytes[Offsets.Race];
            Class = (EOBClass)RawBytes[Offsets.Class];
            Stats = CreateStats(RawBytes, Offsets.Stats);
            ArmorClass = (sbyte) RawBytes[Offsets.ArmorClass];
            Alignment = (EOBAlignment)RawBytes[Offsets.Alignment];
            ItemUseBits = RawBytes[Offsets.ItemUse];
            Portrait = RawBytes[Offsets.Portrait];
            Food = RawBytes[Offsets.Food];
            m_hitPoints = new OneByteStat(RawBytes, Offsets.CurrentHP, Offsets.MaxHP, true);
            Inventory = new EOB1Inventory(RawBytes, itemTable, Offsets.Inventory);
            Experience = new long[3];
            Experience[0] = BitConverter.ToInt32(RawBytes, Offsets.Experience);
            Experience[1] = BitConverter.ToInt32(RawBytes, Offsets.Experience + 4);
            Experience[2] = BitConverter.ToInt32(RawBytes, Offsets.Experience + 8);
            Level = new int[3];
            Level[0] = RawBytes[Offsets.Level];
            Level[1] = RawBytes[Offsets.Level + 1];
            Level[2] = RawBytes[Offsets.Level + 2];
            Spells = new EOBKnownSpells(RawBytes, Offsets.Spells);
            SpellExpirationTimes = new int[10];
            for (int i = 0; i < SpellExpirationTimes.Length; i++)
                SpellExpirationTimes[i] = BitConverter.ToInt32(RawBytes, Offsets.SpellTimeouts + (i * 4));
            ActiveSpells = new byte[10];
            for (int i = 0; i < ActiveSpells.Length; i++)
                ActiveSpells[i] = RawBytes[Offsets.ActiveSpells + i];
            Unknown33 = Global.Subset(RawBytes, 33, 4);
            Unknown223 = Global.Subset(RawBytes, 223, CharacterSize - 223);
            Modifiers = Inventory.GetModifiers();
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            bytes[Offsets.Condition] = (byte)Condition;
            bytes[Offsets.Race] = (byte)Race;
            bytes[Offsets.Class] = (byte)Class;
            Stats.SetBytes(bytes, Offsets.Stats);
            bytes[Offsets.ArmorClass] = (byte) ArmorClass;
            HitPoints.SetBytes(bytes, Offsets.MaxHP, Offsets.CurrentHP);
            bytes[Offsets.Alignment] = (byte)Alignment;
            Inventory.SetBytes(bytes, Offsets.Inventory, BasicClass);
            bytes[Offsets.ItemUse] = ItemUseBits;
            bytes[Offsets.Portrait] = Portrait;
            bytes[Offsets.Food] = Food;
            for (int i = 0; i < Experience.Length; i++)
            {
                byte[] bytesExp = BitConverter.GetBytes((int)Experience[i]);
                Buffer.BlockCopy(bytesExp, 0, bytes, Offsets.Experience + (i * 4), bytesExp.Length);
            }
            for (int i = 0; i < Level.Length; i++)
                bytes[Offsets.Level + i] = (byte)Level[i];
            byte[] bytesSpells = Spells.GetBytes();
            Buffer.BlockCopy(bytesSpells, 0, bytes, Offsets.Spells, bytesSpells.Length);
            for (int i = 0; i < SpellExpirationTimes.Length; i++)
            {
                byte[] bytesExpire = BitConverter.GetBytes((int)SpellExpirationTimes[i]);
                Buffer.BlockCopy(bytesExpire, 0, bytes, Offsets.SpellTimeouts + (i * 4), bytesExpire.Length);
            }
            ActiveSpells = new byte[10];
            for (int i = 0; i < ActiveSpells.Length; i++)
                RawBytes[Offsets.ActiveSpells + i] = ActiveSpells[i];

            Global.SetBytes(bytes, 33, Unknown33);
            Global.SetBytes(bytes, 223, Unknown223);
            Global.SetBytes(bytes, 0, Games.GetNameBytes(Game, CharName));
            stream.Write(bytes, 0, CharacterSize);
        }
    }

    public class EOB1Inventory : EOBInventory
    {
        public EOB1Inventory(byte[] bytes, byte[] bytesItemTable, int offset = 0)
        {
            // An Eye of the Beholder inventory is 27 2-byte values (2 equipped, 14 backpack, then 11 equipped)

            m_items = new List<Item>(27);

            for (int i = 0; i < 27; i++)
            {
                if (i * 2 + offset > bytes.Length)
                    break;  // Not enough bytes for a proper inventory

                EOBItem item = EOBItem.FromInventoryBytes(GameNames.EyeOfTheBeholder1, bytes, bytesItemTable, i, offset);
                if (item != null)
                {
                    if (item.ItemListIndex > 0)
                    {
                        item.MemoryIndex = i;
                        EquipLocation el = GetEquipLocation(i);
                        item.DisplayIndex = null;
                        m_items.Add(item);
                    }
                }
            }
        }

        public static EquipLocation GetEquipLocation(int iPosition)
        {
            switch (iPosition)
            {
                case 0: return EquipLocation.RightHand;
                case 1: return EquipLocation.LeftHand;
                case 16: return EquipLocation.Quiver;
                case 17: return EquipLocation.Torso;
                case 18: return EquipLocation.Gauntlet;
                case 19: return EquipLocation.Head;
                case 20: return EquipLocation.Neck;
                case 21: return EquipLocation.Feet;
                case 22: return EquipLocation.Slot1;
                case 23: return EquipLocation.Slot2;
                case 24: return EquipLocation.Slot3;
                case 25: return EquipLocation.Finger;
                case 26: return EquipLocation.Finger2;

                default: return EquipLocation.None; // 2-15 are the backpack
            }
        }

        public EOB1Inventory(List<Item> items)
        {
            m_items = items;
        }

        public override bool HasItem(GameNames game, int index, bool bEquippedOnly)
        {
            return false;
        }

        public override byte[] GetBytes(GenericClass gc = GenericClass.None)
        {
            byte[] bytes = new byte[54];
            SetBytes(bytes, 0, gc);
            return bytes;
        }

        public override void SetBytes(byte[] bytes, int offset, GenericClass gc = GenericClass.None)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[offset + i] = 0;

            foreach (Item item in Items)
            {
                EOB1Item eobItem = item as EOB1Item;
                if (eobItem == null)
                    continue;

                Global.SetInt16(bytes, eobItem.MemoryIndex * 2, eobItem.ItemListIndex);
            }
        }

        public override BasicDamage RangedWeaponDamage
        {
            get
            {
                EOB1Item itemRanged = null;
                foreach (EOB1Item item in SelectEquippedItems)
                {
                    if (itemRanged == null || item.MissileDamage.Average > itemRanged.MissileDamage.Average)
                        itemRanged = item;
                }

                return itemRanged == null ? BasicDamage.Zero : new BasicDamage(1, itemRanged.MissileDamage);
            }
        }
    }
}