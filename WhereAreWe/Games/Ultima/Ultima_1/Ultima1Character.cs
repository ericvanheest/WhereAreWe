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
    public class Ultima1CharacterOffsets : UltimaCharacterOffsets
    {
    }

    public class Ultima1Stats : UltimaStats
    {
        public Ultima1Stats(byte[] bytes, int offset = 0)
        {
            SetBytes(bytes, offset);
        }
    }

    public enum Ultima1CastleQuests
    {
        FindGrave = 0,
        KillCube = 1,
        FindPost = 2,
        KillCreeper = 3,
        FindPillar = 4,
        KillLich = 5,
        FindTower = 6,
        KillBalron = 7,
        Last
    }

    public enum Ultima1LastSignUsed
    {
        None = -1,
        PillarsOfProtection = 0,
        TowerOfKnowledge = 1,
        PillarsOfTheArgonauts = 2,
        PillarOfOzymandias = 3,
        SignPost = 4,
        SouthernSignPost = 5,
        EasternSignPost = 6,
        GraveOfTheLostSoul = 7,
    }

    public class Ultima1Character : UltimaCharacter
    {
        public const int SizeInBytes = 170;
        public const int SizeInMemory = 170;

        public Ultima1Character()
        {
            m_game = GameNames.Ultima1;
        }

        public override CharacterOffsets Offsets { get { return Ultima1.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override UltimaStats CreateStats(byte[] bytes, int offset) { return new Ultima1Stats(RawBytes, Offsets.Stats); }
        public override int MaxLevel => 33;
        public override int BasicHP => Hits;
        public override int BasicMaxHP => BasicHP;
        public Ultima1LastSignUsed LastSignUsed;
        public int[] m_quests;

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
            Race = (UltimaRace)RawBytes[Offsets.Race];
            Sex = (UltimaSex)RawBytes[Offsets.Sex];
            Class = (UltimaClass)RawBytes[Offsets.Class];
            Stats = CreateStats(RawBytes, Offsets.Stats);
            Food = BitConverter.ToInt16(RawBytes, Offsets.Food);
            Hits = BitConverter.ToInt16(RawBytes, Offsets.CurrentHP);
            Coin = BitConverter.ToInt16(RawBytes, Offsets.Gold);
            LastSignUsed = (Ultima1LastSignUsed) BitConverter.ToInt16(RawBytes, Offsets.LastUsed);
            m_quests = new int[(int) Ultima1CastleQuests.Last];
            for (int i = 0; i < m_quests.Length; i++)
                m_quests[i] = BitConverter.ToInt16(RawBytes, Offsets.Quests + (2 * i));
            Inventory = new Ultima1Inventory(RawBytes, Offsets.Inventory, RawBytes[Offsets.ReadyWeapon], RawBytes[Offsets.ReadySpell], RawBytes[Offsets.ReadyArmor]);
            
            Experience = BitConverter.ToInt16(RawBytes, Offsets.Experience);
            Modifiers = Inventory.GetModifiers();
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = Global.NullBytes(CharacterSize);
            bytes[Offsets.Race] = (byte)Race;
            bytes[Offsets.Class] = (byte)Class;
            bytes[Offsets.Sex] = (byte)Sex;
            Stats.SetBytes(bytes, Offsets.Stats);
            Global.SetInt16(bytes, Offsets.CurrentHP, Hits);
            Inventory.SetBytes(bytes, Offsets.Inventory, Offsets.ReadyWeapon, Offsets.ReadySpell, Offsets.ReadyArmor);
            for(int i = 0; i < m_quests.Length; i += 2)
                Global.SetInt16(bytes, Offsets.Quests, m_quests[i]);
            Global.SetInt16(bytes, Offsets.Food, Food);
            Global.SetInt16(bytes, Offsets.Gold, Coin);
            Global.SetInt16(bytes, Offsets.Experience, Experience);
            Global.SetBytes(bytes, 0, Games.GetNameBytes(Game, CharName));
            stream.Write(bytes, 0, CharacterSize);
        }

        public static string LastUsed(Ultima1LastSignUsed last)
        {
            switch (last)
            {
                case Ultima1LastSignUsed.None: return "None";
                case Ultima1LastSignUsed.PillarsOfProtection: return "Pillars of Protection";  // +5 Agility  
                case Ultima1LastSignUsed.TowerOfKnowledge: return "Tower of Knowledge";  // +2 Intelligence
                case Ultima1LastSignUsed.PillarsOfTheArgonauts: return "Pillars of the Argonauts";  // +Weapon  
                case Ultima1LastSignUsed.PillarOfOzymandias: return "Pillar of Ozymandias";  // +1 Wisdom    
                case Ultima1LastSignUsed.SignPost: return "Sign Post";  // +1 Stamina              
                case Ultima1LastSignUsed.SouthernSignPost: return "Southern Sign Post";  // +1 Charisma    
                case Ultima1LastSignUsed.EasternSignPost: return "Eastern Sign Post";
                case Ultima1LastSignUsed.GraveOfTheLostSoul: return "Grave of the Lost Soul";  // +1 Stamina
                default: return String.Format("Unknown({0})", last);
            }
        }

        public int CastleQuest(Ultima1CastleQuests quest) => m_quests == null || m_quests.Length <= (int)quest ? 0 : m_quests[(int)quest];
    }

    public class Ultima1Inventory : UltimaInventory
    {
        public Ultima1Inventory()
        {
            m_items = new List<Item>();
        }

        public Ultima1Inventory(byte[] bytes, int offset, int readyWeapon, int readySpell, int readyArmor)
        {
            // An Ultima 1 inventory is 50 2-byte values (47 backpack and 3 ready)

            m_items = new List<Item>(47);

            for (int i = 0; i < 47; i++)
            {
                if (i * 2 + offset > bytes.Length)
                    break;  // Not enough bytes for a proper inventory

                int iCount = BitConverter.ToInt16(bytes, offset + (i * 2));
                if (iCount < 1)
                    continue;
                UltimaItem item = new UltimaItem((UltimaItemIndex)i, iCount);
                if (item != null)
                {
                    item.MemoryIndex = i;
                    m_items.Add(item);
                }
            }

            if (readyWeapon > 0)
                m_items.Add(new UltimaItem(readyWeapon + UltimaItemIndex.Hands, 1, true));
            if (readySpell > 0)
                m_items.Add(new UltimaItem(readySpell + UltimaItemIndex.Prayer, 1, true));
            if (readyArmor > 0)
                m_items.Add(new UltimaItem(readyArmor + UltimaItemIndex.Skin, 1, true));
        }

        public Ultima1Inventory(List<Item> items)
        {
            m_items = items;
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[94];
            SetBytes(bytes, 0, -1, -1, -1);
            return bytes;
        }

        public override byte[] GetReadyBytes()
        {
            byte[] bytes = new byte[6];
            foreach (UltimaItem item in Items)
            {
                if (!item.Ready)
                    continue;
                switch (item.Type)
                {
                    case ItemType.Weapon:
                        Global.SetInt16(bytes, 0, item.ItemIndex - UltimaItemIndex.Hands);
                        break;
                    case ItemType.Armor:
                        Global.SetInt16(bytes, 4, item.ItemIndex - UltimaItemIndex.Skin);
                        break;
                    case ItemType.Spell:
                        Global.SetInt16(bytes, 2, item.ItemIndex - UltimaItemIndex.Prayer);
                        break;
                    default:
                        break;
                }
            }
            return bytes;
        }

        public override void SetBytes(byte[] bytes, int offset, int readyWeapon, int readySpell, int readyArmor)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[offset + i] = 0;
            Global.SetInt16(bytes, offset + 2 * (int)UltimaItemIndex.Hands, -1);
            Global.SetInt16(bytes, offset + 2 * (int)UltimaItemIndex.Skin, -1);
            Global.SetInt16(bytes, offset + 2 * (int)UltimaItemIndex.Foot, -1);
            Global.SetInt16(bytes, offset + 2 * (int)UltimaItemIndex.Prayer, -1);
            Global.SetInt16(bytes, offset + 2 * (int)UltimaItemIndex.EndVehicles, -1);

            foreach (Item item in Items)
            {
                UltimaItem ultimaItem = item as UltimaItem;
                if (ultimaItem == null)
                    continue;

                if (ultimaItem.Ready)
                {
                    switch (ultimaItem.Type)
                    {
                        case ItemType.Weapon:
                            if (readyWeapon > 0)
                                Global.SetInt16(bytes, readyWeapon, ultimaItem.ItemIndex - UltimaItemIndex.Hands);
                            break;
                        case ItemType.Armor:
                            if (readyArmor > 0)
                                Global.SetInt16(bytes, readyArmor, ultimaItem.ItemIndex - UltimaItemIndex.Skin);
                            break;
                        case ItemType.Spell:
                            if (readySpell > 0)
                                Global.SetInt16(bytes, readySpell, ultimaItem.ItemIndex - UltimaItemIndex.Prayer);
                            break;
                        default:
                            break;
                    }
                } else
                    Global.SetInt16(bytes, ultimaItem.Index * 2, ultimaItem.Count);
            }
        }
    }
}