using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class BTQuestData : QuestData
    {
        public byte[] MapSpecials;
        public byte[] TownMap;

        public BTQuestData(BTPartyInfo party, LocationInformation location, BTGameState state, byte[] mapSpecials, byte[] townMap)
        {
            Party = party;
            Location = location;
            State = state;
            MapSpecials = mapSpecials;
            TownMap = townMap;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            if (MapSpecials != null)
                Global.WriteBytes(stream, MapSpecials);
            if (TownMap != null)
                Global.WriteBytes(stream, TownMap);
        }

        public byte MapSpecial(MapXY spot) { return MapSpecial(spot.Location); }

        public byte TownMapByte(MapXY spot)
        {
            if (TownMap == null)
                return 0;
            int iOffset = (29 - spot.Y) * 30 + spot.X;
            if (TownMap.Length < iOffset || iOffset < 0)
                return 0;
            return TownMap[iOffset];
        }

        public byte MapSpecial(Point pt)
        {
            if (MapSpecials == null)
                return 0;
            int iOffset = pt.Y * 22 + pt.X;
            if (MapSpecials.Length < iOffset)
                return 0;
            return MapSpecials[iOffset];
        }
    }

    [Flags]
    public enum BTGameStateFlags
    {
        AskCastSpell =          0x0001,
        AskWhichSpell =         0x0002,
        AskWhichSpellCombat =   0x0004,
        AskWhichSong =          0x0008,
    }

    public class BTGameState : GameState
    {
        public GameNames BTGame = GameNames.BardsTale1;

        public override GameNames Game { get { return BTGame; } }

        public bool CastingState = false;
        public override bool Casting { get { return CastingState; } }
        public override bool ActingIsCaster { get { return true; } }
        public override Subscreen Subscreen { get { return Subscreen.InventoryMain; } set { } }
        public bool UseMarchingOrder = false;

        public int MarchingChar(MemoryHacker hacker, int iAddress)
        {
            if (!UseMarchingOrder)
                return iAddress;

            byte[] order = hacker.GetMarchingOrder();
            if (order == null || order.Length <= iAddress)
                return iAddress;

            return order[iAddress];
        }

        public override bool NoActingChar
        {
            get
            {
                if (InCombat)
                {
                    switch (Main)
                    {
                        case MainState.Combat:  // "Combat" = Resolution of battle commands -> no active character
                        case MainState.PreCombat:
                        case MainState.CombatConfirmRound:
                            return true;
                        default:
                            return false;
                    }
                }

                switch (Main)
                {
                    case MainState.Adventuring:
                    case MainState.Pause:
                    case MainState.QuickRef:
                    case MainState.Question:
                    case MainState.PreCombat:
                    case MainState.Combat:                  
                    case MainState.Treasure:
                    case MainState.TreasureEnterTrapType:
                    case MainState.TreasureWhoWillDisarm:
                    case MainState.SelectBard:
                    case MainState.SelectSpellCaster:
                    case MainState.UseSelectCharacter:
                    case MainState.CantPerformAction:
                    case MainState.ItemSelectAction:
                    case MainState.Training:
                    case MainState.ReviewMain:
                    case MainState.ReviewWhoAdvance:
                    case MainState.ReviewWhoClass:
                    case MainState.ReviewWhoSpell:
                    case MainState.ReviewWhoTalk:
                    //case MainState.Shop:
                    case MainState.Unknown:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }

    public class BTBackpackBytes
    {
        public byte[] Items;  // 8 Int16s

        public BTBackpackBytes()
        {
            Items = Global.NullBytes(16);
        }

        public BTBackpackBytes(byte[] bytes)
        {
            Items = bytes;
        }
    }

    public class BTPartyInfo : PartyInfo
    {
        private Dictionary<int, BTInventory> m_invCache = null;
        public BTInventory InventoryForChar(int iCharAddress)
        {
            if (iCharAddress < 0 || Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return null;
            if (m_invCache == null)
                m_invCache = new Dictionary<int, BTInventory>();
            else if (m_invCache.ContainsKey(iCharAddress))
                return m_invCache[iCharAddress];
            BTInventory inventory = BTInventory.Create(Game, Bytes, iCharAddress * CharacterSize + Offsets.Inventory);
            m_invCache.Add(iCharAddress, inventory);
            return inventory;
        }

        public BTPartyInfo(byte[] bytes, byte[] order, byte numChars)
        {
            Bytes = bytes;
            State = new GameState();
            NumChars = numChars;
            Positions = new byte[NumChars];
            Addresses = new int[NumChars];
            m_bytesMarchingOrder = order;
            for (int i = 0; i < NumChars; i++)
            {
                Positions[i] = (byte)i;
                Addresses[i] = order[i] - 1;
            }
        }

        public override int MarchingIndex(int index) { return m_bytesMarchingOrder == null || m_bytesMarchingOrder.Length <= index ? index : m_bytesMarchingOrder[index] - 1; }

        public override byte[] QuestBytes
        {
            get
            {
                CharacterOffsets offsets = Offsets;
                if (Bytes == null)
                    return null;
                MemoryStream stream = new MemoryStream();
                for (int i = 0; i < NumChars; i++)
                {
                    stream.WriteByte(Bytes[i * CharacterSize + offsets.Level]);
                    stream.Write(Bytes, i * CharacterSize + offsets.Inventory, offsets.InventoryLength);
                    if (offsets.Awards != -1)
                        stream.Write(Bytes, i * CharacterSize + offsets.Awards, offsets.AwardsLength);
                    if (offsets.Spells != -1)
                        stream.Write(Bytes, i * CharacterSize + offsets.Spells, offsets.SpellsLength);
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public bool CharacterHasItem(GameNames game, int iCharAddress, int item, bool bEquippedOnly = false)
        {
            BTInventory inventory = InventoryForChar(iCharAddress);
            return inventory == null ? false : inventory.HasItem(game, item, bEquippedOnly);
        }

        public bool CharacterHasItems(GameNames game, int iCharAddress, params int[] items)
        {
            BTInventory inventory = InventoryForChar(iCharAddress);

            foreach (int item in items)
            {
                if (!inventory.HasItem(game, item))
                    return false;
            }
            return true;
        }

        public bool AllCharactersHaveItem(GameNames game, int item, bool bEquippedOnly = false)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (!CharacterHasItem(game, Addresses[i], item, bEquippedOnly))
                    return false;
            return true;
        }

        public string CharacterName(int iCharAddress)
        {
            if (iCharAddress < 0 || CharacterSize < 1)
                return String.Empty;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return String.Empty;

            return Global.GetNullTerminatedString(Bytes, iCharAddress * CharacterSize + Offsets.Name, Offsets.NameLength);
        }

        public bool CharacterIsClass(int iCharAddress, GenericClass gc)
        {
            if (Offsets == null || iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress * CharacterSize) + Offsets.Class)
                return false;

            return Bytes[iCharAddress * CharacterSize + Offsets.Class] == Games.ClassValue(State.Game, gc);
        }

        public bool CharacterHasCondition(int iCharAddress, BTCondition conditionTest)
        {
            if (iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            BTCondition condition = (BTCondition) BitConverter.ToUInt16(Bytes, iCharAddress * CharacterSize + Offsets.Condition);
            return condition.HasFlag(conditionTest);
        }

        public bool AnyCharacterHasCondition(BTCondition conditionTest)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (CharacterHasCondition(Addresses[i], conditionTest))
                    return true;
            return false;
        }

        public bool CharacterHasItem(int iCharAddress, BT1ItemIndex item) { return CharacterHasItem(GameNames.BardsTale1, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, BT2ItemIndex item) { return CharacterHasItem(GameNames.BardsTale2, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, BT3ItemIndex item) { return CharacterHasItem(GameNames.BardsTale3, iCharAddress, (int)item); }

        public bool CurrentPartyHasItem(GameNames game, int item, bool bEquippedOnly = false)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(game, GetAddress(i), item, bEquippedOnly))
                    return true;
            }
            return false;
        }

        public bool AllCharactersHaveBackpackSpace()
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (!CharacterHasBackpackSpace(GetAddress(i)))
                    return false;
            return true;
        }

        public bool CharacterHasBackpackSpace(int iCharAddress)
        {
            if (iCharAddress < 0)
                return false;

            switch (State.Game)
            {
                case GameNames.BardsTale1:
                case GameNames.BardsTale2: return AnyInt16Zeros(Bytes, iCharAddress * CharacterSize + Offsets.Inventory, 16);
                default: return false;
            }
        }

        public bool AnyInt16Zeros(byte[] bytes, int offset, int length)
        {
            if (bytes.Length <= offset + length + 1)
                return false;
            for (int i = offset; i < offset + length; i += 2)
                if (bytes[i] == 0 && bytes[i + 1] == 0)
                    return true;
            return false;
        }

        public bool CurrentPartyHasClass(GenericClass gc) { return CurrentPartyClassIndex(gc) != -1; }

        public int CurrentPartyClassIndex(GenericClass gc)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterIsClass(GetAddress(i), gc))
                    return i;
            }
            return -1;
        }

        public bool CurrentPartyHasCharacter(string strName, GenericClass gc = GenericClass.None)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterName(GetAddress(i)) != strName)
                    continue;

                if (gc == GenericClass.None || CharacterIsClass(GetAddress(i), gc))
                    return true;
            }
            return false;
        }

        public bool CurrentPartyHasCondition(BTCondition condition, bool bAll = false)
        {
            bool[] list = new bool[Addresses.Length];
            for (int i = 0; i < Addresses.Length; i++)
                list[i] = CharacterHasCondition(GetAddress(i), condition);
            if (bAll)
                return list.All(b => b);
            return list.Any(b => b);
        }

        public bool CharacterIsCaster(int iCharAddress)
        {
            if (Offsets == null || iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            BT3Class bt3Class = (BT3Class)Bytes[iCharAddress * CharacterSize + Offsets.Class];
            switch (bt3Class)
            {
                case BT3Class.Archmage:
                case BT3Class.Chronomancer:
                case BT3Class.Conjurer:
                case BT3Class.Geomancer:
                case BT3Class.Magician:
                case BT3Class.Sorcerer:
                case BT3Class.Wizard:
                    return true;
                default:
                    return false;
            }
        }

        public bool CharacterKnowsSpell(int iCharAddress, BT3SpellIndex spell)
        {
            if (iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            if (CharacterIsClass(iCharAddress, GenericClass.Bard))
            {
                if (spell < BT3SpellIndex.SirRobinsTune || spell > BT3SpellIndex.MinstrelShield)
                    return false;   // Bards only "know" the songs (even if the spell bits happen to be set via cheats or whatnot)
                return Global.GetBit(Bytes, (iCharAddress * CharacterSize + Offsets.Identify) * 8 + (spell - BT3SpellIndex.SirRobinsTune)) != 0;
            }

            if (!CharacterIsCaster(iCharAddress))
                return false;

            return Global.GetBit(Bytes, (iCharAddress * CharacterSize + Offsets.Spells) * 8 + (int) spell - 1) != 0;
        }

        public bool CurrentPartyKnowsSpell(BT3SpellIndex spell)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (CharacterKnowsSpell(GetAddress(i), spell))
                    return true;
            return false;
        }

        public bool CharacterHasItem(int iCharAddress, BTItemType type, BT3ItemFlags contains)
        {
            BTInventory inventory = InventoryForChar(iCharAddress);
            if (inventory == null)
                return false;

            return inventory.Items.Any(i => i is BT3Item && ((BT3Item)i).BTType == type && ((BT3Item)i).Contains == contains);
        }

        public bool CharacterHasItem(int iCharAddress, BTItemType type)
        {
            BTInventory inventory = InventoryForChar(iCharAddress);
            if (inventory == null)
                return false;

            return inventory.Items.Any(i => i is BT3Item && ((BT3Item)i).BTType == type);
        }

        public bool CurrentPartyHasItem(BTItemType type, BT3ItemFlags contains)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(i, type, contains))
                    return true;
            }
            return false;
        }

        public bool CurrentPartyHasItem(BTItemType type)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(i, type))
                    return true;
            }
            return false;
        }

        public bool CurrentPartyHasItem(BT1ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale1, (int)item); }
        public bool CurrentPartyHasItem(BT2ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale2, (int)item); }
        public bool CurrentPartyHasItem(BT3ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale3, (int)item); }

        public bool CurrentPartyHasEquipped(BT1ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale1, (int)item, true); }
        public bool CurrentPartyHasEquipped(BT2ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale2, (int)item, true); }
        public bool CurrentPartyHasEquipped(BT3ItemIndex item) { return CurrentPartyHasItem(GameNames.BardsTale3, (int)item, true); }

        public override int CharacterSize { get { return Games.CharacterSize(State.Game); } }
    }

    public class BTSearchResults : SearchResults
    {
        public override bool HasTraps { get { return true; } }
        public int Trap;
        public int KillCount;
        public int GoldMax;
        public int ItemMin;
        public int ItemRange;

        public override string HeaderString { get { return "The party has found a chest!"; } }
        public override bool IsEmpty { get { return false; } }

        public override string ContentsString
        {
            get
            {
                if (KillCount < 1)
                    return "Nothing";
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("0-{0} Gold\r\n", GoldMax * KillCount);
                sb.AppendFormat("Chance of item index #{0}-#{1}\r\n", ItemMin, ItemMin + ItemRange);
                return sb.ToString();
            }
        }

        public BTSearchResults()
        {
            Trap = 0;
            KillCount = 0;
            GoldMax = 0;
            ItemMin = 0;
            ItemRange = 0;
        }

        public BTSearchResults(BTTrapInfo.BT12Trap trap, int iRiskReward, byte[] killedCount, byte[] goldMaximums, byte[] itemMinimums, byte[] itemRanges)
        {
            Trap = (int) trap;
            if (itemMinimums == null || itemRanges == null || goldMaximums == null || 
                itemMinimums.Length <= iRiskReward || itemRanges.Length <= iRiskReward || goldMaximums.Length <= iRiskReward)
                return;

            KillCount = killedCount.Sum(i => i);
            GoldMax = goldMaximums[iRiskReward];
            ItemMin = itemMinimums[iRiskReward];
            ItemRange = itemRanges[iRiskReward];
        }

        public override int CompareTo(SearchResults results)
        {
            BTSearchResults btResults = results as BTSearchResults;
            if (btResults == null)
                return 1;

            if (btResults.Trap != Trap)
                return 1;

            return 0;
        }
    }

    public class BTSpellInfo : SpellInfo
    {
        public BTPartyInfo Party;
        public override bool ClassLimited { get { return false; } }

        public BTSpellInfo()
        {
            Party = null;
            Game = new BTGameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingCharAddress = -1;
        }
    }

    public class BTGameInfo : GameInfo
    {
        public bool InCombat = false;
        public int GameTimeHours = 0;
        public int RiskReward = 0;
        public int SpellIcon1 = 0;
        public int SpellIcon2 = 0;
        public int SpellIcon3 = 0;
        public int SpellIcon4 = 0;
        public int SpellIcon5 = 0;
        public int LightDistance = 0;
        public int LevitationDuration = 0;
        public int ShieldDuration = 0;
        public int LightDuration = 0;
        public int DetectionDuration = 0;
        public int CompassDuration = 0;
        public int MainMap = 0;
        public int SubMap = 0;
        public int SurfaceMap = 0;
        public int SongDuration = 0;
        public int AdventuringSong = 0;
        public int CombatSong = 0;
        public int AdvACBonus = 0;
        public int Trap = 0;
        public int PartyPerish = 0;
        public int MaxCounters = 1;
        public byte CombatActiveSpells = 0;
        public int[] Counters = null;
        public BT1Summon Summon = null;
        public MemoryBytes ScriptBits = null;
    }

    public class BTMapData : MapData
    {
        public override int DefaultZoom { get { return 150; } }
        public byte SwapWallsDoors;

        public byte[] Squares;
        public byte[] Specials;
        public byte[] CustomSquares;
        public byte[] FixedEncounters;
        public byte[] Teleport;
        public byte[] Monsters;

        public int GetIndex(byte[] bytes, int x, int y)
        {
            if (bytes == null)
                return -1;
            for (int i = 0; i < bytes.Length - 1; i++)
            {
                if (i >= 16)
                    return -1;
                if (bytes[i] == y && bytes[i + 1] == x)
                    return i / 2;
            }
            return -1;
        }

        public int GetCustomIndex(int x, int y) { return GetIndex(CustomSquares, x, y); }
        public int GetFixedIndex(int x, int y) { return GetIndex(FixedEncounters, x, y); }
        public int GetTeleportIndex(int x, int y) { return GetIndex(Teleport, x, y); }
    }

    public class BTCharCreationInfo : CharCreationInfo
    {
        public int MemoryOffset = 0;

        public override bool ValidValues
        {
            get
            {
                for(int i = 0; i < 5; i++)
                    if (AttributesOriginal[i] < 3 || AttributesOriginal[i] > 19 || AttributesModified[i] < 3 || AttributesModified[i] > 19)
                        return false;

                return true;
            }
        }
    }

    public class BTActiveSquares : ActiveSquares
    {
        byte[] ForcedEncounters = null;

        public BTActiveSquares(MainForm main, int mapIndex, Size szMap, byte[] mapSpecials, byte[] forcedEncounters)
        {
            Main = main;
            MapSize = szMap;
            m_iMapIndex = mapIndex;
            ForcedEncounters = forcedEncounters;
            RawBytes = mapSpecials;
            m_bInitialized = false;
        }

        private bool IsEncounter(int x, int y, byte b)
        {
            if (ForcedEncounters == null)
            {
                // This is the town map
                if (MapSize.Height == 30)
                    return b == 0x60;
                return false;
            }

            if ((b & 0x80) > 0)
                return true;    // random encounter
            if ((b & 0x04) == 0)
                return false;

            for (int i = 0; i < ForcedEncounters.Length - 1; i += 2)
            {
                if (ForcedEncounters[i] == y && ForcedEncounters[i + 1] == x)
                    return true;    // fixed encounter
            }

            return false;
        }

        public override bool IsActive(int x, int y, bool bEncountersOnly)
        {
            if (AllInactive)
                return false;

            bool bActive = false;
            if (x < 0 || x >= MapSize.Width || y < 0 || y >= MapSize.Height)
                return false;
            if (MapSize.Height != 22)
                y = MapSize.Height - 1 - y;
            int offset = y * MapSize.Width + x;
            if (offset < 0 || offset >= RawBytes.Length)
                return false;
            byte bSquare = RawBytes[offset];

            bActive = IsActiveByte(bSquare);

            if (!bActive)
                return false;
            if (!bEncountersOnly)
                return true;

            return IsEncounter(x, y, bSquare);
        }

        private bool IsActiveByte(byte b)
        {
            if (MapSize.Height != 22)
                return b > 4;
            return (b & ~0x08) != 0;
        }

        protected override void Initialize()
        {
            m_activeSquares = new Dictionary<Point, ActiveSquareInfo>();

            if (RawBytes == null || RawBytes.Length < (MapSize.Width * MapSize.Height) || AllInactive)
                return;

            bool bTown = (MapSize.Height != 22);

            for (int y = 0; y < MapSize.Height; y++)
            {
                for (int x = 0; x < MapSize.Width; x++)
                {
                    Point pt = new Point(x, y);
                    m_activeSquares.Add(pt, new ActiveSquareInfo(pt, IsActiveByte(RawBytes[(bTown ? MapSize.Height - 1 - y : y) * MapSize.Width + x])));
                }
            }

            m_bInitialized = true;
        }
    }

    public abstract class BTMemoryHacker : MemoryHacker
    {
        public override PrimaryStat[] StatOrder { get { return StatOrderSIDCL; } }
        public override bool SpellsUseLevelOnly { get { return true; } }
        public abstract BTMemory Memory { get; }

        public override byte[] MainSearch { get { return Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return Memory.Guesses; } }
        protected int m_iCurrentMapAdventuringCount = 0;
        public override int MaxBackpackSize { get { return 8; } }
        protected virtual List<Item> GetSuperItems(GenericClass btClass) { return new List<Item>(0); }
        public override CreationAssistantControl CreateCreationAssistantControl(IMain main) { return new BTCreationAssistantControl(main); }
        public override TrainingAssistantControl CreateTrainingAssistantControl(IMain main) { return new BT123TrainingAssistantControl(main); }
        protected BTEncounterInfo m_lastEncounterInfo = null;
        public override bool SpellsHaveDuration { get { return true; } }
        protected virtual QuestInfo CreateQuestInfo() { return null; }
        protected virtual BTQuestData CreateQuestData() { return null; }
        public override bool UsesAlignment { get { return false; } }
        public virtual byte[] GetSpecialBytes() { return null; }
        private DateTime m_dtLastAutoCombat = DateTime.Now;
        private DateTime m_dtLastAutoPreCombat = DateTime.Now;
        private DateTime m_dtLastAutoConfirmRound = DateTime.Now;
        private int m_iLastAutoCombatChar = -1;

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return BTCharacter.GetStatModifier(value, stat);
        }

        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "May be any class";
                case GenericRace.Elf: return "May not be a hunter";
                case GenericRace.Dwarf: return "May not be a conjurer or magician";
                case GenericRace.Hobbit: return "May not be a paladin or hunter";
                case GenericRace.HalfElf: return "May not be a paladin or hunter";
                case GenericRace.HalfOrc: return "May not be a paladin, bard or monk";
                case GenericRace.Gnome: return "May not be a paladin or bard";
                default: return "Unknown";
            }
        }

        public override string GetClassDescription(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Paladin: return "+2 to-hit, 1-16 HP/level, +Level/2 priority";
                case GenericClass.Warrior: return "+2 to-hit, 1-16 HP/level, +Level/2 priority";
                case GenericClass.Hunter: return "+2 to-hit, 1-16 HP/level, +Level/2 priority";
                case GenericClass.Bard: return "+1 to-hit, 1-16 HP/level, +Level/4 priority";
                case GenericClass.Rogue: return "+1 to-hit, 1-8 HP/level, +Level/4 priority";
                case GenericClass.Monk: return "+3 to-hit, 1-8 HP/level, +Level priority";
                case GenericClass.Conjurer: return "+0 to-hit, 1-4 HP/level, +Level/8 priority";
                case GenericClass.Magician: return "+0 to-hit, 1-4 HP/level, +Level/8 priority";
                case GenericClass.Sorcerer: return "+0 to-hit, 1-8 HP/level, +Level/8 priority";
                case GenericClass.Wizard: return "+0 to-hit, 1-8 HP/level, +Level/8 priority";
                case GenericClass.Archmage: return "+? to-hit, 1-16 HP/level, +? priority";
                case GenericClass.Chronomancer: return "(Available only in Bard's Tale 3)";
                case GenericClass.Geomancer: return "(Available only in Bard's Tale 3)";
                default: return "Unknown";
            }
        }

        public override int CharacterSize { get { return Games.CharacterSize(Game); } }
        public override int CharacterMemorySize { get { return Games.CharacterMemorySize(Game); } }

        public override BaseCharacter CreateCharFromBytes(byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (bytes.Length <= CharacterSize)
                return BTCharacter.Create(Game, 0, bytes, 0);

            return BTCharacter.Create(Game, 0, bytes, 0);
        }

        protected virtual int InventoryOffset { get { return Games.GetCharacterOffsets(Game).Inventory; } }
        protected virtual int InventoryLength { get { return Games.GetCharacterOffsets(Game).InventoryLength; } }
        protected virtual BTInventory CreateInventory(List<Item> items) { return BTInventory.Create(Game, items); }
        protected virtual BTInventory CreateInventory(byte[] bytesChar) { return BTInventory.Create(Game, bytesChar, InventoryOffset); }
        protected virtual BTGameState GetMainState(byte[] stack, byte[] states = null) { return null; }

        public override GameState GetGameState() { return ReadBT123GameState(); }

        private BTGameState ReadBT123GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as BTGameState;     // Don't spam the game state from different windows

            byte[] bytesStack = ReadOffset(Memory.Stack - Memory.StackSize, Memory.StackSize);
            byte[] bytesStateArray = null;
            if (Memory.StateArray != 0)
                bytesStateArray = ReadOffset(Memory.StateArray, 256);

            BTGameState state = GetMainState(bytesStack, bytesStateArray);
            if (state == null)
                return null;

            state.UseMarchingOrder = Game != GameNames.BardsTale3;

            state.BTGame = Game;
            state.Location = GetLocationForce();
            state.InShop = false;

            byte[] order = GetMarchingOrder();

            switch (state.Main)
            {
                case MainState.GuildDiskOptions:
                case MainState.GuildMain:
                case MainState.GuildRemove:
                case MainState.LoadingGuild:
                    state.Location.MapIndex = (int)BT3Map.Wilderness;
                    break;
                case MainState.Unknown:
                    if (state.Location.MapIndex >= 128 || state.InCombat)
                        break;
                    switch (ReadUInt16(Memory.State4))
                    {
                        case 0xFE02:
                        case 0x8F03:
                        case 0x1E01:
                        case 0x6400:
                            state.Location.MapIndex = (int)BT3Map.Wilderness;
                            break;
                    }
                    break;
                case MainState.Shop:
                case MainState.ShopBuyItem:
                case MainState.ShopIdentifyItem:
                case MainState.ShopSellItem:
                case MainState.ShopInspecting:
                    state.InShop = true;
                    break;
                case MainState.ShopInspectChar:
                    state.InShop = true;
                    state.Inspecting = true;
                    break;
                case MainState.CampInspecting:
                    state.Inspecting = true;
                    break;
                case MainState.PreCombat:
                case MainState.CombatConfirmRound:
                case MainState.Combat:
                case MainState.CombatOptions:
                case MainState.CombatSelectSpell:
                case MainState.CombatSelectBardSong:
                case MainState.Treasure:
                case MainState.TreasureWhoWillCalfo:
                case MainState.TreasureWhoWillDisarm:
                case MainState.TreasureWhoWillInspect:
                case MainState.TreasureWhoWillOpen:
                case MainState.TreasureCouldNotDisarm:
                    state.InCombat = true;
                    break;
            }

            switch (state.Main)
            {
                case MainState.SelectSpell:
                case MainState.SelectBardSong:
                case MainState.CombatSelectBardSong:
                case MainState.CombatSelectSpell:
                    state.CastingState = true;
                    break;
            }

            state.ActingPosition = state.ActingCharAddress;

            if (state.UseMarchingOrder)
            {
                if (state.ActingCaster >= 0 && state.ActingCaster < order.Length)
                    state.ActingCaster = order[state.ActingCaster] - 1;
                if (state.ActingCombatChar >= 0 && state.ActingCombatChar < order.Length)
                    state.ActingCombatChar = order[state.ActingCombatChar] - 1;
                if (state.ActingCharAddress >= 0 && state.ActingCharAddress < order.Length)
                    state.ActingCharAddress = order[state.ActingCharAddress] - 1;
            }

            m_gsCurrent = state;
            return state;
        }

        protected virtual int GetNumChars() { return 0; }
        protected virtual byte GetFacingByte(int iMapIndex) { return 0; }
        protected virtual bool CanUseBag(int iMapIndex) { return false; }
        protected virtual Point CheckInvalidCoordinates(int iMapIndex, Point pt) { return pt; }
        protected virtual bool IsOutside(int iMap) { return false; }

        protected LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(GetPartyPosition());
            info.MapIndex = GetCurrentMapIndex();
            info.LightDistance = GetLightDistance(info.PrimaryCoordinates);
            byte bFacing = GetFacingByte(info.MapIndex);
            switch (bFacing)
            {
                case 0:
                    info.Facing = Direction.Up;
                    break;
                case 1:
                    info.Facing = Direction.Right;
                    break;
                case 2:
                    info.Facing = Direction.Down;
                    break;
                case 3:
                    info.Facing = Direction.Left;
                    break;
                default:
                    break;
            }
            info.Outside = IsOutside(info.MapIndex);
            info.CanUseBag = CanUseBag(info.MapIndex) || Global.Cheats;
            info.NumChars = (byte)GetNumChars();
            info.PrimaryCoordinates = CheckInvalidCoordinates(info.MapIndex, info.PrimaryCoordinates);

            return info;
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            int iSize = CharacterMemorySize;
            if (bytes.Length > iSize)
            {
                // The 17-character name/roster at the beginning does not belong at this memory address
                byte[] bytesNoName = Global.Subset(bytes, 17, bytes.Length - 17);
                return WriteOffset(Memory.PartyInfo + (iAddress * iSize), bytesNoName, Math.Min(iSize, bytesNoName.Length));
            }
            return WriteOffset(Memory.PartyInfo + (iAddress * iSize), bytes, Math.Min(iSize, bytes.Length));
        }

        public virtual byte[] GetBackpackBytes(int iCharAddress)
        {
            CharacterOffsets offsets = Games.CharacterOffsets(Game);
            return ReadOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory - 17, offsets.InventoryLength).Bytes;
        }

        public virtual byte[] GetBackpackBytes(List<Item> items)
        {
            byte[] bytes = Global.NullBytes(16);
            for (int i = 0; i < 8; i++)
            {
                if (i >= items.Count)
                    break;
                Global.SetInt16(bytes, i * 2, items[i].Index);
            }
            return bytes;
        }

        public virtual bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            CharacterOffsets offsets = Games.CharacterOffsets(Game);
            return WriteOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory - 17, bytes);
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            if (items == null || (items.Count > 0 && !(items[0] is BTItem)))
                return SetBackpackResult.InvalidItems;

            BTInventory inv = null;
            inv = BTInventory.Create(Game, GetBackpackBytes(iCharAddress));

            List<Item> listNew = bRemoveEquipped ? new List<Item>() : inv.SelectEquippedItems;
            foreach (Item item in items)
            {
                if (listNew.Count < MaxBackpackSize)
                    listNew.Add(item);
                else
                    return SetBackpackResult.InsufficientSpace;
            }

            for (int i = 0; i < listNew.Count; i++)
                listNew[i].MemoryIndex = i;

            byte[] bytes = null;
            bytes = BTInventory.Create(Game, listNew).GetBytes(GetCharacterClass(iCharAddress));
            
            SetBackpackBytes(iCharAddress, bytes);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            List<Item> list = new List<Item>();

            if (!IsValid || iCharAddress < 0)
                return list;

            byte[] bytes = GetBackpackBytes(iCharAddress);
            BTInventory inv = BTInventory.Create(Game, bytes);

            return inv.SelectUnequippedItems;
        }

        public abstract List<BTItem> BTItems { get; }

        protected virtual BTGameInfo CreateGameInfo() { return null; }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            BTGameInfo info = CreateGameInfo();

            MemoryStream stream = new MemoryStream();

            info.GameTimeHours = ReadByte(Memory.GameTimeHours);
            info.RiskReward = ReadByte(Memory.MapTreasureIndex);
            info.Trap = ReadByte(Memory.TrapType);
            info.PartyPerish = ReadInt16(Memory.PartyPerishSeconds);
            info.Counters = Global.UInt16Array(ReadOffset(Memory.Counter1, info.MaxCounters * 2));
            info.SpellIcon1 = ReadByte(Memory.SpellIcon1);
            info.SpellIcon2 = ReadByte(Memory.SpellIcon2);
            info.SpellIcon3 = ReadByte(Memory.SpellIcon3);
            info.SpellIcon4 = ReadByte(Memory.SpellIcon4);
            info.SpellIcon5 = ReadByte(Memory.SpellIcon5);
            info.LightDistance = ReadByte(Memory.LightDistance);
            info.LevitationDuration = ReadByte(Memory.LevitationDuration);
            info.ShieldDuration = ReadByte(Memory.ShieldDuration);
            info.LightDuration = ReadByte(Memory.LightDuration);
            info.DetectionDuration = ReadByte(Memory.DetectionDuration);
            info.SongDuration = ReadByte(Memory.SongDuration);
            info.AdventuringSong = ReadByte(Memory.AdventuringSong);
            info.CombatSong = ReadByte(Memory.CombatSong);
            info.CompassDuration = ReadByte(Memory.CompassDuration);
            info.MainMap = ReadByte(Memory.MainMapIndex);
            info.SubMap = ReadByte(Memory.SubMapIndex);
            info.SurfaceMap = ReadByte(Memory.SurfaceMapIndex);
            info.AdvACBonus = ReadByte(Memory.AdvPartyACBonus);
            info.CombatActiveSpells = ReadByte(Memory.CombatActiveSpells);
            MemoryBytes mb = ReadOffset(Memory.SummonedCreature, 48);
            if (mb != null)
                info.Summon = new BT1Summon(mb.Bytes);
            else
                info.Summon = new BT1Summon();

            if (info is BT3GameInfo)
            {
                info.ScriptBits = ReadOffset(BT3.Memory.ScriptBits, 16);
                stream.Write(info.ScriptBits, 0, 16);
            }

            stream.WriteByte((byte)info.GameTimeHours);
            stream.WriteByte((byte)info.RiskReward);
            stream.WriteByte((byte)info.SpellIcon1);
            stream.WriteByte((byte)info.SpellIcon2);
            stream.WriteByte((byte)info.SpellIcon3);
            stream.WriteByte((byte)info.SpellIcon4);
            stream.WriteByte((byte)info.SpellIcon5);
            stream.WriteByte((byte)info.LightDistance);
            stream.WriteByte((byte)info.LevitationDuration);
            stream.WriteByte((byte)info.ShieldDuration);
            stream.WriteByte((byte)info.LightDuration);
            stream.WriteByte((byte)info.DetectionDuration);
            stream.WriteByte((byte)info.CompassDuration);
            stream.WriteByte((byte)info.MainMap);
            stream.WriteByte((byte)info.SubMap);
            stream.WriteByte((byte)info.SurfaceMap);
            stream.WriteByte((byte)info.SongDuration);
            stream.WriteByte((byte)info.AdventuringSong);
            stream.WriteByte((byte)info.CombatSong);
            stream.WriteByte((byte)info.AdvACBonus);
            stream.WriteByte((byte)info.Trap);
            Global.WriteInt16(stream, info.PartyPerish);
            foreach (int i in info.Counters)
                Global.WriteInt16(stream, i);
            stream.WriteByte(info.CombatActiveSpells);
            stream.Write(info.Summon.RawBytes, 0, info.Summon.RawBytes.Length);

            info.Bytes = stream.ToArray();

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is BTGameInfo))
                return GetGameInfo();

            BTGameInfo BTOld = infoOld as BTGameInfo;
            BTGameInfo BTNew = GetGameInfo() as BTGameInfo;

            if (BTNew == null)
                return null;

            if (Global.Compare(BTOld.Bytes, BTNew.Bytes))
                return infoOld; // All the bytes are the same; return the old object

            return BTNew;
        }

        private string[] m_lastCharNames = null;
        private byte[] m_lastCharBytes = null;
        private byte[] m_lastMarchingOrder = null;

        protected string[] GetCharacterNames(int iLength, int iCount)
        {
            byte[] order = GetMarchingOrder();
            MemoryBytes bytesNames = ReadOffset(Memory.PartyNames, iLength * iCount);
            if (order == null || bytesNames == null)
                return new string[0];
            if (Global.Compare(order, m_lastMarchingOrder) && Global.Compare(bytesNames.Bytes, m_lastCharBytes))
                return m_lastCharNames;

            ASCIIEncoding ascii = new ASCIIEncoding();
            List<string> names = new List<string>(iCount);
            for (int i = 0; i < iCount; i++)
            {
                if (bytesNames.Bytes[iLength * (order[i] - 1)] > (byte)' ')
                {
                    if (order[i] < iCount + 1)
                        names.Add(ascii.GetString(bytesNames, iLength * (order[i] - 1), 15).Trim());
                    else
                        names.Add("Invalid");
                }
            }
            m_lastCharNames = names.ToArray();
            m_lastCharBytes = bytesNames.Bytes;
            m_lastMarchingOrder = order;
            return m_lastCharNames;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            BTPartyInfo pi = GetPartyInfo() as BTPartyInfo;
            if (pi == null)
                return null;

            byte[] order = GetMarchingOrder();

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
            {
                if (i >= order.Length)
                    break;
                int iChar = order[i] - 1;
                BTCharacter BTChar = BTCharacter.Create(Game, iChar, pi.Bytes, iChar * CharacterSize);
                BTChar.Address = iChar;
                chars.Add(BTChar);
            }

            return chars;
        }

        public override string PleaseFormPartyString { get { return "Please form a party and exit the adventurer's guild."; } }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            BTCharacter btChar = baseChar as BTCharacter;
            if (!IsValid || btChar == null)
                return;

            // Bard's Tale stores equipped/unequipped in the same list, so we can't just completely randomize the inventory
            List<Item> equipped = btChar.Inventory.SelectEquippedItems;

            // Add random items to fill up or replace the unequipped spaces in the backpack
            List<Item> items = new List<Item>(MaxBackpackSize - equipped.Count);

            int iMemIndex = 0;
            while (items.Count + equipped.Count < MaxBackpackSize)
            {
                while (equipped.Any(i => i.MemoryIndex == iMemIndex))
                    iMemIndex++;    // Skip the equipped items
                BTItem item = BTItem.CreateRandom(BTItems, type, bUsableOnly ? btChar : null);
                items.Add(item);
            }

            SetBackpack(baseChar.BasicAddress, items);
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[] { info.AttributesOriginal[0], 0, info.AttributesOriginal[1], 0, 
                 info.AttributesOriginal[2], 0, info.AttributesOriginal[3], 0, info.AttributesOriginal[4], 0, 
                  info.AttributesModified[0], 0, info.AttributesModified[1], 0, 
                 info.AttributesModified[2], 0, info.AttributesModified[3], 0, info.AttributesModified[4], 0, 
                 0x9e, 0x03, info.AttributesOriginal[5], 0 };
            WriteOffset(Memory.CreationStats, bytes);
            return true;
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            BTCharCreationInfo info = new BTCharCreationInfo();
            info.Race = BTCharacter.GetBasicRace((BTRace) ReadByte(Memory.CreationRace));
            MemoryBytes bytes = ReadOffset(Memory.CreationStats, 24);
            info.AttributesModified = new byte[6];
            info.AttributesOriginal = new byte[6];
            info.AttributesModified[0] = bytes.Bytes[0];
            info.AttributesModified[1] = bytes.Bytes[2];
            info.AttributesModified[2] = bytes.Bytes[4];
            info.AttributesModified[3] = bytes.Bytes[6];
            info.AttributesModified[4] = bytes.Bytes[8];
            info.AttributesModified[5] = bytes.Bytes[22];
            Buffer.BlockCopy(info.AttributesModified, 0, info.AttributesOriginal, 0, 6);
            info.State = ReadBT123GameState();
            return info;
        }

        public override string GetMapStrings(bool bRaw = false)
        {
            if (!IsValid)
                return String.Empty;

            //MemoryBytes mb = ReadOffset(Memory.MapStrings, 256);
            MemoryBytes mb = ReadOffset(Memory.MapSquareStrings, 256);
            if (mb == null)
                return String.Empty;

            return Global.UnixToDos(Encoding.ASCII.GetString(mb.Bytes));
        }

        public override TrapsControl GetTrapsControl(IMain main) { return new BT123TrapsControl(main); }

        public virtual BTSpellInfo CreateSpellInfo() { return new BTSpellInfo(); }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            BTSpellInfo info = CreateSpellInfo();
            IntPtr pRead = IntPtr.Zero;
            info.Game = GetGameState() as BTGameState;

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = GetPartyInfo() as BTPartyInfo;

            if (info.Game.ActingCharAddress == -1)
                info.Game.ActingCharAddress = info.Party.ActingChar;

            if (info.Game.ActingCaster == -1)
            {
                if (info.Game.InCombat)
                    info.Game.ActingCaster = info.Party.ActingCombatChar;
                else
                    info.Game.ActingCaster = info.Party.ActingCaster;
            }

            return info;
        }

        protected virtual MonsterName[] GetMonsterNames() { return new MonsterName[] {
            new MonsterName("Unknown"), new MonsterName("Unknown"), new MonsterName("Unknown"), new MonsterName("Unknown"), }; }

        public override string ReplaceNoteStrings(string str)
        {
            if (!IsValid)
                return str;

            StringBuilder sbResult = new StringBuilder(str);

            if (str.Contains("EncounterMonsters"))
            {
                MonsterName[] monsters = GetMonsterNames();
                MemoryBytes monsterCounts = ReadOffset(Memory.MonsterNumAlive, 4);

                StringBuilder sbUnique = new StringBuilder();
                StringBuilder sbAll = new StringBuilder();

                for (int i = 0; i < 4; i++)
                {
                    if (monsterCounts.Bytes[i] < 1)
                        continue;
                    string strName = monsters[i].Singular;
                    sbUnique.AppendFormat("{0}, ", strName);
                    if (monsterCounts.Bytes[i] > 1)
                        sbAll.AppendFormat("{0} ({1}), ", strName, monsterCounts.Bytes[i]);
                    else
                        sbAll.AppendFormat("{0}, ", strName);
                }

                Global.Trim(sbUnique);
                Global.Trim(sbAll);

                sbResult.Replace("$uniqueEncounterMonsters", sbUnique.ToString());
                sbResult.Replace("$allEncounterMonsters", sbAll.ToString());
            }
            return sbResult.ToString();
        }

        protected virtual BTTrainingInfo CreateTrainingInfo() { return new BT1TrainingInfo(); }

        public override TrainingInfo GetTrainingInfo()
        {
            BTTrainingInfo info = CreateTrainingInfo();
            info.Party = GetPartyInfo();
            info.MapIndex = GetCurrentMapIndex();
            info.State = ReadBT123GameState();
            return info;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            if (info is BTTrainingInfo)
            {
                BTTrainingInfo btInfo = info as BTTrainingInfo;
                for (int i = 0; i < btInfo.Party.NumChars; i++)
                {
                    if (btInfo.Party.Addresses == null || btInfo.Party.Addresses.Length <= i)
                        continue;
                    int iAddress = btInfo.Party.Addresses[i];
                    WriteTrainingInfo(btInfo, iAddress);
                }
            }

            return false;
        }

        protected virtual void WriteTrainingInfo(BTTrainingInfo info, int iAddress) { }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            QuestInfo info = CreateQuestInfo();
            QuestData data = CreateQuestData();

            if (info == null || data == null)
                return null;

            info.MapIndex = data.Location.MapIndex;
            MemoryStream ms = new MemoryStream();
            data.AddBytes(ms);
            ms.WriteByte((byte)iOverrideCharAddress);
            byte[] newBytes = ms.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(data, iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override bool SkipIntroductions(int iTimeout = 27000)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                BTGameState state = ReadBT123GameState();
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Opening:          // Black screen; need to wait
                        case MainState.LoadingGuild:     // Loading character list; need to wait
                            break;
                        case MainState.Opening2:    // Title screen
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Space }, true);
                            TweakSleep(100);
                            break;
                        case MainState.MainMenu:
                            TweakSleep(100);
                            // Select the first six characters in the list
                            SendKeysToDOSBox(50, new Keys[] { Keys.A, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.E }, true);
                            TweakSleep(100);
                            break;
                        case MainState.Adventuring:
                            return true;
                        default:
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            return false;
        }

        public List<Item> GetEquippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.EquippedOnly);
        }

        public List<Item> GetUnequippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.UnequippedOnly);
        }

        public List<Item> GetBackpack(int iCharAddress, BackpackType type)
        {
            if (!IsValid)
                return null;

            BTInventory inv = BTInventory.Create(Game, GetBackpackBytes(iCharAddress));
            if (inv == null)
                return null;

            switch (type)
            {
                case BackpackType.All: return inv.Items;
                case BackpackType.EquippedOnly: return inv.SelectEquippedItems;
                case BackpackType.UnequippedOnly: return inv.SelectUnequippedItems;
                default: return new List<Item>();
            }
        }

        public override bool TradeBackpacks(int iCharAddress1, int iCharAddress2)
        {
            // Backpacks can only be traded if both characters have enough capacity to make the trade

            List<Item> equipped1 = GetEquippedItems(iCharAddress1);
            List<Item> equipped2 = GetEquippedItems(iCharAddress2);
            List<Item> backpack1 = GetBackpack(iCharAddress1, BackpackType.UnequippedOnly);
            List<Item> backpack2 = GetBackpack(iCharAddress2, BackpackType.UnequippedOnly);

            if (backpack1.Count > MaxBackpackSize - equipped2.Count ||
                backpack2.Count > MaxBackpackSize - equipped1.Count)
                return false;

            SetBackpackResult result1 = SetBackpack(iCharAddress1, backpack2);
            SetBackpackResult result2 = SetBackpack(iCharAddress2, backpack1);

            return (result1 == SetBackpackResult.Success && result2 == SetBackpackResult.Success);
        }

        public override bool AutoCombat()
        {
            BTGameState state = ReadBT123GameState();

            bool bBT3 = state.Game == GameNames.BardsTale3;

            // 100-ms commands:
            if ((DateTime.Now - m_dtLastAutoCombat).TotalMilliseconds < 100)
                return true;    // Don't send commands so fast that the game can't keep up

            switch (state.Main)
            {
                case MainState.PreCombat:
                case MainState.Combat:
                case MainState.CombatFriendly:
                    if ((DateTime.Now - m_dtLastAutoPreCombat).TotalMilliseconds < 1000)
                        return true;    // Don't send this one too often; need to wait an entire round
                    if (state.Main == MainState.CombatFriendly)
                        SendKeysToDOSBox(new Keys[] { Keys.F }, true);      // Fight
                    else
                        SendKeysToDOSBox(new Keys[] { Keys.A, Keys.F, Keys.Space }, true);      // Advance/Fight/Continue
                    m_dtLastAutoPreCombat = DateTime.Now;
                    return true;
                case MainState.CombatConfirmRound:
                    if ((DateTime.Now - m_dtLastAutoConfirmRound).TotalMilliseconds < 1000)
                    {
                        SendKeysToDOSBox(new Keys[] { Keys.Space }, true);  // Speeds up combat
                        return true;
                    }
                    SendKeysToDOSBox(new Keys[] { Keys.Y }, true);
                    m_dtLastAutoConfirmRound = DateTime.Now;
                    return true;
                case MainState.Unknown:
                    if (state.InCombat)
                    {
                        // Probably "resolving combat commands"
                        m_dtLastAutoCombat = DateTime.Now;
                        SendKeysToDOSBox(new Keys[] { Keys.Space }, true);  // Speeds up combat
                        return true;
                    }
                    return false;
                default:
                    break;
            }

            // 300-ms commands
            if ((DateTime.Now - m_dtLastAutoCombat).TotalMilliseconds < 300 && state.ActingPosition == m_iLastAutoCombatChar)
                return true;    // Don't send commands so fast that the game can't keep up

            m_dtLastAutoCombat = DateTime.Now;
            m_iLastAutoCombatChar = state.ActingPosition;

            switch (state.Main)
            {
                case MainState.Treasure:
                    return false;
                case MainState.CombatOptions:
                    bool bCanAttack = state.ActingPosition < (bBT3 ? 4 : 3);

                    if (!bCanAttack)
                        SendKeysToDOSBox(new Keys[] { Keys.D }, true);  // Defend
                    else
                    {
                        if (bBT3)
                        {
                            BT3EncounterInfo info = GetEncounterInfo() as BT3EncounterInfo;
                            for (int i = 0; i < info.Distances.Length; i++)
                            {
                                if (info.Distances[i] < 2)
                                {
                                    SendKeysToDOSBox(new Keys[] { Keys.A }, true);  // Attack
                                    if (info.Living.Count(b => b > 0) > 1)
                                        SendKeysToDOSBox(new Keys[] { Keys.A }, true);  // Group A
                                }
                            }
                        }
                        else
                        {
                            if (state.ActingPosition == 0)
                                SendKeysToDOSBox(new Keys[] { Keys.A, Keys.B, Keys.A }, true);  // Mix it up a bit so that everyone doesn't attack a size-1 group A
                            else
                                SendKeysToDOSBox(new Keys[] { Keys.A, Keys.A }, true);
                        }
                    }
                    return true;
                default:
                    return false;
            }
        }

        public override Modifiers GetExternalModifiers(BaseCharacter baseChar)
        {
            Modifiers mod = new Modifiers();
            mod.Adjust(ModAttr.ArmorClass, - ReadByte(Memory.AdvPartyACBonus), "Spell");
            return mod;
        }

        public override bool IsDungeon(int iMap) { return iMap != 0; }
        public override Size GetCurrentMapDimensions() { return GetMapDimensions(GetCurrentMapIndex()); }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            if (!IsValid)
                return null;

            MapBasicInfo map = GetCurrentMapInfo();
            MemoryBytes specials;
            if (Game == GameNames.BardsTale3)
                return new BT3ActiveSquares(form, map.Index, map.Dimensions, GetSpecialBytes());

            if (!IsDungeon(map.Index))
            {
                MapBytes mb = GetCurrentMapBytes();
                if (mb == null && mb.Bytes == null)
                    return null;
                return new BTActiveSquares(form, map.Index, map.Dimensions, mb.Bytes, null);
            }
            else
            {
                specials = ReadOffset(Memory.MapSpecials, map.ByteCount + 16);
                if (specials == null)
                    return null;
                return new BTActiveSquares(form, map.Index, map.Dimensions, specials, ReadOffset(Memory.ForcedEncounters, 16));
            }
        }

        public override StatsPerLevel GetStatsPerLevel(GenericClass gc)
        {
            int iLowHP = 1;
            int iHighHP = 16;
            int iLowSP = 1;
            int iHighSP = 4;
            switch (gc)
            {
                case GenericClass.Rogue:
                case GenericClass.Monk:
                case GenericClass.Sorcerer:
                case GenericClass.Wizard:
                    iHighHP = 8;
                    break;
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                    iHighHP = 4;
                    break;
            }

            return new StatsPerLevel(iLowHP, iHighHP, iLowSP, iHighSP);
        }

        public override TrapInfo CreateTrapInfo(int iTrap) { return new BTTrapInfo(iTrap); }
    }
}
