using System;
using System.Collections;
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
    public class EOB1Memory : EOBMemory
    {
        // Search for "can't rest"
        // public override byte[] MainSearch { get { return new byte[] { 0x63, 0x61, 0x6E, 0x27, 0x74, 0x20, 0x72, 0x65, 0x73, 0x74 }; } }
        // Search for "3 4 1 homie"
        // public override byte[] MainSearch { get { return new byte[] { 0x33, 0x20, 0x34, 0x20, 0x31, 0x20, 0x68, 0x6F, 0x6D, 0x69, 0x65 }; } }
        // public override byte[] MainSearch { get { return new byte[] { 0x20, 0x34, 0x20, 0x31, 0x20, 0x68, 0x6F, 0x6D, 0x69, 0x65 }; } }
        // Search for "C:\EOB.EXE"
        public override byte[] MainSearch { get { return new byte[] { 0x01, 0x00, 0x43, 0x3A, 0x5C, 0x45, 0x4F, 0x42, 0x2E, 0x45, 0x58, 0x45, 0x00 }; } }
        const int MainSearchOffset = 242;
        public int MainSearchAdjust = 0;

        //public override int MainBlockSVN { get { return -6626; } }
        //public override int MainBlockOldSVN { get { return -6994; } }
        //public override int MainBlockNonSVN { get { return -6626; } }
        public override int MainBlockSVN { get { return -6626; } }
        public override int MainBlockOldSVN { get { return -6994; } }
        public override int MainBlockNonSVN { get { return 6362; } }
        public override int Map { get { return MainSearchOffset + MainSearchAdjust + 183282; } }
        public override int LocationXY { get { return MainSearchOffset + MainSearchAdjust + 200488; } }
        public override int Facing { get { return MainSearchOffset + MainSearchAdjust + 165364; } }
        public override int ItemList { get { return MainSearchOffset + MainSearchAdjust + 172334; } }
        public override int ItemBasicList { get { return MainSearchOffset + MainSearchAdjust + 179534; } }
        public override int PartyInfo { get { return MainSearchOffset + MainSearchAdjust + 198878; } }
        public override int GameTimeSeconds { get { return MainSearchOffset + MainSearchAdjust + 159518; } }
        public override int MapSpecials { get { return MainSearchOffset + MainSearchAdjust + 381294; } }
        public override int MainMapIndex { get { return MainSearchOffset + MainSearchAdjust + 200486; } }
        public override int MonsterHP { get { return MainSearchOffset + MainSearchAdjust + 171488; } }
        public override int ScriptBits { get { return MainSearchOffset + MainSearchAdjust + 200502; } }
        public override int ScriptBits2 { get { return MainSearchOffset + MainSearchAdjust + 200546; } }
        public override int InspectingChar { get { return MainSearchOffset + MainSearchAdjust + 179448; } }
        public override int MonsterList { get { return MainSearchOffset + MainSearchAdjust + 142745; } }
        public override int AskCastSpell { get { return MainSearchOffset + MainSearchAdjust + 198423; } }     // 0 if not casting, 1 if casting
        public override int ScreenText { get { return MainSearchOffset + MainSearchAdjust + 160124; } }       // 1 if viewing text, 4 if not
        public override int AskWhichSpell { get { return MainSearchOffset + MainSearchAdjust + 192832; } }    // 0-4 depending on spell level selected
        public override int CampInspectingChar { get { return MainSearchOffset + MainSearchAdjust + 200354; } }   // 0 = adventuring, 1 = Inventory, 2 = stats

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.EyeOfTheBeholder1]; } }

        public override int CreationStats => 59257;
        public override int MapSquareStrings => 33323;
        public override int CreationRace => -1405;
        public override int PartyNames => -2287;
        public override int MarchingOrder => -12845;

        public EOB1Memory()
        {
        }
    }

    public enum EOB1Map
    {
        Unknown = -1,
        None = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10,
        Level11 = 11,
        Level12 = 12
    }

    public class EOB1Effects
    {
        public int RationsCounter;
        public int NestCounter;

        public EOB1Effects(EOB1MemoryHacker hacker)
        {
            RationsCounter = -1;
            NestCounter = -1;
            // Don't bother getting the item counts (a nontrivial process) if the party isn't even on the map they're used for
            if (hacker.GetCurrentMapIndex() == (int)EOB1Map.Level5)
            {
                EOB1Item[] items = hacker.GetItems(EOB1.Spots.RationsQuest_Alcove_L5_1014, EOBItemLocation.Alcove);
                RationsCounter = items.Count(i => i.ItemIndex == EOBItemIndex.Rations);
            }
            if (hacker.GetCurrentMapIndex() == (int)EOB1Map.Level6)
            {
                EOB1Item[] items = hacker.GetItems(EOB1.Spots.KenkuNest_L6_1317, EOBItemLocation.Any);
                NestCounter = items.Count(i => i.ItemIndex == EOBItemIndex.KenkuEgg);
            }
        }

        public byte[] Bytes
        {
            get
            {
                return Global.Combine(BitConverter.GetBytes(RationsCounter), BitConverter.GetBytes(NestCounter));
            }
        }
    }

    public class EOB1GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return EOB1MemoryHacker.GetMapTitlePair(iMap); }

        public EOB1GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn) { }

        public EOB1GameInfoItem(string desc, object val, int offset, BitDescriptionDelegate fn)
            : base(desc, val, offset, DataType.Bits, 0, ShowStyle.Editable, fn) { }
    }

    public class EOB1GameInfo : EOBGameInfo
    {
        public override GameNames Game { get { return GameNames.EyeOfTheBeholder1; } }

        public EOB1GameInfo()
        {
        }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new EOB1GameInfoItem("Level 1 Bits", LevelBits[0].Bytes, EOB1.Memory.ScriptBits + (4 * 0), EOB1Bits.Level1Description));
            items.Add(new EOB1GameInfoItem("Level 2 Bits", LevelBits[1].Bytes, EOB1.Memory.ScriptBits + (4 * 1), EOB1Bits.Level2Description));
            items.Add(new EOB1GameInfoItem("Level 3 Bits", LevelBits[2].Bytes, EOB1.Memory.ScriptBits + (4 * 2), EOB1Bits.Level3Description));
            items.Add(new EOB1GameInfoItem("Level 4 Bits", LevelBits[3].Bytes, EOB1.Memory.ScriptBits + (4 * 3), EOB1Bits.Level4Description));
            items.Add(new EOB1GameInfoItem("Level 5 Bits", LevelBits[4].Bytes, EOB1.Memory.ScriptBits + (4 * 4), EOB1Bits.Level5Description));
            items.Add(new EOB1GameInfoItem("Level 6 Bits", LevelBits[5].Bytes, EOB1.Memory.ScriptBits + (4 * 5), EOB1Bits.Level6Description));
            items.Add(new EOB1GameInfoItem("Level 7 Bits", LevelBits[6].Bytes, EOB1.Memory.ScriptBits + (4 * 6), EOB1Bits.Level7Description));
            items.Add(new EOB1GameInfoItem("Level 8 Bits", LevelBits[7].Bytes, EOB1.Memory.ScriptBits + (4 * 7), EOB1Bits.Level8Description));
            items.Add(new EOB1GameInfoItem("Level 9 Bits", LevelBits[8].Bytes, EOB1.Memory.ScriptBits + (4 * 8), EOB1Bits.Level9Description));
            items.Add(new EOB1GameInfoItem("Level 10 Bits", LevelBits[9].Bytes, EOB1.Memory.ScriptBits + (4 * 9), EOB1Bits.Level10Description));
            items.Add(new EOB1GameInfoItem("Level 11 Bits", LevelBits[10].Bytes, EOB1.Memory.ScriptBits + (4 * 10), EOB1Bits.Level11Description));
            items.Add(new EOB1GameInfoItem("Global Bits", GlobalBits.Bytes, EOB1.Memory.ScriptBits2, EOB1Bits.GlobalDescription));
            items.Add(new EOB1GameInfoItem("Map", (byte)MainMap, new OffsetList(EOB1.Memory.MainMapIndex)));
            return items;
        }

        public bool GlobalFlag(EOB1Bits.Game bit)
        {
            return Global.GetBit(GlobalBits.Bytes, (int)bit, true) == 1;
        }

        public bool LevelFlag(int iLevel, EOB1Bits.Level bit)
        {
            if (iLevel >= LevelBits.Length)
                return false;
            return Global.GetBit(LevelBits[iLevel].Bytes, (int)bit, true) == 1;
        }
    }

    public class EOB1MapData : EOBMapData
    {
        public string Name;
        public const int BytesPerSquare = 9;
        public Size InternalSize;
        public int SquaresOffset;
        public int ScriptsOffset;
        public int MapLevel;
        public byte[] MapFlags;
        private EOB1Scripts m_scripts;
        private EOB1ScriptInfo m_scriptInfo;

        public EOB1MapData(EOB1MemoryHacker hacker, MapBytes mb, int iMapIndex, byte[] flags)
        {
            Index = iMapIndex;
            MapFlags = flags;
            Name = EOB1MemoryHacker.GetMapTitlePair(iMapIndex).Title;
            InternalSize = new Size(32, 32);

            SquaresOffset = 0;
            Squares = mb.Bytes;
            ScriptBytes = hacker.ReadOffset(EOB1.Memory.MapSpecials, 8192);
            Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
        }

        public override ScriptInfo Scripts
        {
            get
            {
                if (m_scripts == null)
                    m_scripts = new EOB1Scripts(new MapBytes(Squares, InternalSize.Width, InternalSize.Height), ScriptBytes);
                if (m_scriptInfo == null)
                    m_scriptInfo = new EOB1ScriptInfo(EOB1MemoryHacker.GetMapTitlePair(Index), m_scripts);
                return m_scriptInfo;
            }
        }

        public static int DirOffset(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return 0;
                case Direction.Right: return 1;
                case Direction.Down: return 2;
                case Direction.Left: return 3;
                default: return 0;
            }
        }

        public static short GetItemPointer(byte[] squares, Size sz, int squarelen, int x, int y)
        {
            int iSquareOffset = (y * (sz.Width) * squarelen) + (x * squarelen);
            if (squares == null || iSquareOffset < 0 || iSquareOffset - EOB1MapData.BytesPerSquare > squares.Length)
                return -1;
            // Map square is 9 bytes:  N, E, S, W, #monsters, (Item List), (Script offset)
            return BitConverter.ToInt16(squares, iSquareOffset + 5);
        }

        public static BasicWall GetWall(byte[] squares, Size sz, int squarelen, int mapLevel, int x, int y, Direction dir)
        {
            int iIndex = GetWallIndex(squares, sz, squarelen, x, y, dir);
            if (iIndex == -1)
                return BasicWall.Solid;
            return EOB1.WallsEOB1.Value.GetWall(mapLevel, iIndex);
        }

        public static int GetWallIndex(byte[] squares, Size sz, int squarelen, int x, int y, Direction dir)
        {
            int iSquareOffset = (y * (sz.Width) * squarelen) + (x * squarelen);
            if (squares == null || iSquareOffset < 0 || iSquareOffset - EOB1MapData.BytesPerSquare > squares.Length)
                return -1;
            // Map square is 9 bytes:  N, E, S, W, #monsters, (Item List), (Script offset)
            return squares[iSquareOffset + DirOffset(dir)];
        }

        public int GetWallIndex(int x, int y, Direction dir) => GetWallIndex(Squares, InternalSize, BytesPerSquare, x, y, dir);
        public BasicWall GetWall(int x, int y, Direction dir) => GetWall(Squares, InternalSize, BytesPerSquare, MapLevel, x, y, dir);

        public bool HasItem(int x, int y)
        {
            int iSquareOffset = (y * (InternalSize.Width) * BytesPerSquare) + (x * BytesPerSquare);
            if (Squares == null || iSquareOffset - EOB1MapData.BytesPerSquare > Squares.Length)
                return false;
            return BitConverter.ToInt16(Squares, iSquareOffset + 5) > 0;
        }

        public bool HasItem(int x, int y, int iItemTableIndex)
        {
            int itemPointer = GetItemPointer(Squares, InternalSize, BytesPerSquare, x, y);
            return GetItems(ItemList, itemPointer).Any(i => i.ItemListIndex == iItemTableIndex);
        }

        public static EOB1Item[] GetItems(byte[] bytesTable, int itemPointer, EOBItemLocation location = EOBItemLocation.Any)
        {
            if (itemPointer < 1 || bytesTable == null)
                return new EOB1Item[0];

            int firstItem = itemPointer;
            List<EOB1Item> itemList = new List<EOB1Item>();
            while (itemPointer < bytesTable.Length / 14 && itemPointer > -1)
            {
                EOB1Item item = EOB1Item.FromItemListBytes(GameNames.EyeOfTheBeholder1, bytesTable, itemPointer * 14) as EOB1Item;
                if (item == null)
                    break;
                if (location != EOBItemLocation.Any && item.Floor != location)
                    break;
                itemList.Add(item);
                if (item.NextItem == itemPointer || item.NextItem == firstItem)
                    break;
                itemPointer = item.NextItem;
            }

            return itemList.ToArray();
        }

        public bool HasItemType(EOBItemIndex itemType, int x, int y)
        {
            int itemPointer = GetItemPointer(Squares, InternalSize, BytesPerSquare, x, y);
            return GetItems(ItemList, itemPointer).Any(i => i.ItemIndex == itemType);
        }

        public bool HasItemTypes(int x, int y, params EOBItemIndex[] itemType)
        {
            int itemPointer = GetItemPointer(Squares, InternalSize, BytesPerSquare, x, y);
            return GetItems(ItemList, itemPointer).Any(i => itemType.Contains(i.ItemIndex));
        }

        // Returns true if an item of the specified type is present in the item table (that is, has an X and Y coordinate that aren't both 0xff)
        public bool ItemInTable(EOB1ItemTableDefault item, EOBItemIndex type)
        {
            if ((int)item >= ItemList.Length / 14)
                return false;
            return ItemList[(int)item*14 + 4] == (byte) type && (ItemList[(int)item * 14 + 6] != 0xff || ItemList[(int)item * 14 + 7] != 0xff);
        }

    }

    public class EOB1EncounterInfo : EOBEncounterInfo
    {
        public EOB1EncounterInfo(MonsterLocations locations)
        {
            if (locations != null)
            {
                AllBytes = locations.RawBytes;
                m_monsters = locations.Monsters;
                NumTotalMonsters = m_monsters.Count;
            }
        }
    }

    public class EOB1MemoryHacker : EOBMemoryHacker
    {
        public override EOBMemory Memory { get { return EOB1.Memory; } }
        public override List<EOBItem> EOBItems { get { return EOB1.Items; } }
        protected override EOBGameInfo CreateGameInfo() { return new EOB1GameInfo(); }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new EOB1GameInformationControl(main); }
        public override string GetMapEnum(int index) { return String.Format("EOB1Map.{0}", Enum.GetName(typeof(EOB1Map), (EOB1Map)(index))); }
        protected override QuestInfoBase CreateQuestInfo() { return new EOB1QuestInfo(); }
        public override int ScriptCommandOffset { get { return 0; } }
        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }
        public override int DelayBetweenSpellKeys { get { return 10; } }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return EOB1RosterFile.Create(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "EOB.EXE"; } }
        public static byte[] BatchBytes = new byte[] { 0x45, 0x79, 0x65, 0x20, 0x6F, 0x66, 0x20, 0x74, 0x68, 0x65, 0x20, 0x42, 0x65, 0x68, 0x6F, 0x6C, 0x64, 0x65, 0x72 };  // "Eye of the Beholder"
        private MonsterLocations m_lastMonsterLocations = null;
        private ItemLocations m_lastItemLocations = null;
        public override string ConfirmSuperCharMessage => "This will maximize all of this character's stats and add a +30 modifier to currently equipped weapons and armor.  Proceed?";
        protected override int ItemListLength => 599;
        public override int MonsterListEntrySize => EOB1MonsterList.MonsterLengthBytes;

        protected override void OnReinitialized(EventArgs e)
        {
            EOB1.MonsterList.Value.Reinitialize(this, false);
            EOB1.ItemList.Value.InitExternalList(this);
            if (FoundBlockOffset == Memory.MainBlockNonSVN)
                ((EOB1Memory)Memory).MainSearchAdjust = -10;
            else
                ((EOB1Memory)Memory).MainSearchAdjust = 0;
            base.OnReinitialized(e);
        }

        public EOB1MemoryHacker()
        {
            m_game = GameNames.EyeOfTheBeholder1;
        }

        protected override EOBGameState GetMainState()
        {
            EOBGameState state = new EOBGameState();

            state.Main = MainState.Adventuring;
            int iActingChar = ReadByte(Memory.InspectingChar);

            if (IsBeforeDetection)
            {
                state.Main = MainState.DosIntro1;
                return state;
            }

            byte bCasting = ReadByte(Memory.AskCastSpell);     // 0 if not casting, 1 if casting
            byte bSpellLevel = ReadByte(Memory.AskWhichSpell);
            byte bInspecting = ReadByte(Memory.CampInspectingChar);

            if (bCasting == 1)
                state.Main = MainState.SelectSpell;
            else if (bInspecting == 2)
                state.Main = MainState.CampInspecting;
            else if (bInspecting == 1)
                state.Main = MainState.Inventory;

            Global.FixRange(ref iActingChar, 0, 6);
            state.ActingCharAddress = iActingChar;
            state.ActingCaster = iActingChar;
            state.ActingCombatChar = iActingChar;

            return state;
        }

        protected override List<Item> GetSuperItems(GenericClass gc)
        {
            List<Item> items = new List<Item>();
            return items;
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int iSize = CharacterSize;
            int iMemorySize = CharacterMemorySize;

            int offset = iAddress * iSize;
            CharacterOffsets offsets = EOB1.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + CharacterSize > info.Bytes.Length + 1)
                return false;

            EOBClass multiClass = (EOBClass)info.Bytes[offset + offsets.Class];
            GenericClass gc = EOB1Character.GetBasicClass(multiClass);
            EOBClass[] classes = EOB1Character.SeparateClasses(multiClass);

            byte[] bytes = new byte[] { 25, 25, 0, 0, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 }; // Stats (Str, /100, Int, Wis, Dex, Con, Cha)
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            info.Bytes[offset + offsets.Food] = 100;
            info.Bytes[offset + offsets.Condition] = (byte)EOBCondition.Good;
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 100);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 100);
            for (int i = 0; i < classes.Length; i++)
            {
                byte iMax = (byte)(classes[i] == EOBClass.Cleric ? 10 : 11);
                info.Bytes[offset + offsets.Level + i] = iMax;
                Global.SetInt32(info.Bytes, offset + offsets.Experience + (i * 4), EOBCharacter.XPForLevel(Game, EOBCharacter.GetBasicClass(classes[i]), iMax));
            }
            // One of each spell
            bytes = new byte[] { 1,  2,  3,  4,  6,  7,
                                 8, 10, 11, 12, 13, 14,
                                15, 16, 17, 18, 19, 20,
                                21, 22, 23, 24, 25,  0,
                                 0,  0,  0,  0,  0,  0,
                                 1,  2,  3,  4,  5,  6,
                                 7,  8,  9, 10, 11, 12,
                                13, 14, 15, 16, 17, 18,
                                19, 20, 21, 22, 23, 24,
                                 0,  0,  0,  0,  0,  0 };
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Spells, bytes.Length);

            byte[] bytesNew = new byte[iMemorySize * 6];
            for (int i = 0; i < 6; i++)
                Buffer.BlockCopy(info.Bytes, i * iSize, bytesNew, i * iMemorySize, iMemorySize);

            WriteOffset(Memory.PartyInfo, bytesNew);

            byte[] bytesInv = GetBackpackBytes(iAddress);
            byte[] bytesTable = GetItemTable();
            EOBInventory inv = EOBInventory.Create(Game, bytesInv, bytesTable);

            foreach (EOBItem item in inv.SelectEquippedItems)
            {
                switch (item.WhereEquipped)
                {
                    case EquipLocation.LeftHand:
                    case EquipLocation.RightHand:
                    case EquipLocation.Torso:
                        if (item.IsWeapon || item.IsArmor)
                        {
                            bytesTable[14 * item.ItemListIndex + 13] = 30;      // Modifier
                            bytesTable[14 * item.ItemListIndex + 2] |= 0x40;    // Identified
                        }
                        break;
                    default:
                        break;
                }
            }

            SetMasterItemTable(bytesTable);

            return true;
        }

        public override IEnumerable<Monster> GetMonsterList() { return EOB1.Monsters; }

        public bool GetGameTime(out int seconds, out int minutes, out int hours)
        {
            uint iTime = ReadUInt32(Memory.GameTimeSeconds);
            hours = (int)(iTime / 32768) % 24;
            minutes = (int)(iTime % 32768) / 547;
            seconds = (int)(iTime % 3594) / 60;
            return true;
        }

        public override String GetGameTimeString(bool bFull)
        {
            int seconds, minutes, hours;
            if (!GetGameTime(out seconds, out minutes, out hours))
                return String.Empty;

            int hoursDisp = hours % 12;
            if (hoursDisp == 0)
                hoursDisp = 12;
            return String.Format("{0}:{1:D2}:{2:D2} {3}", hoursDisp, minutes, seconds, hours >= 12 ? "PM" : "AM");
            //return String.Format("{0}:{1:D2} {2}", hours == 0 ? 12 : hours % 12, minutes, hours >= 12 ? "PM" : "AM");
        }

        protected override EOBQuestData CreateQuestData()
        {
            EOBPartyInfo party = GetPartyInfo() as EOBPartyInfo;
            if (party == null)
                return null;
            EOB1MapData map = new EOB1MapData(this, GetCurrentMapBytes(), GetCurrentMapIndex(), null);
            map.ItemList = GetItemTable();
            return new EOB1QuestData(party, GetLocation(), GetGameState() as EOBGameState, null, map, new EOB1Effects(this), GetGameInfo() as EOB1GameInfo);
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new EOB1RosterFile(Global.CombineRoster(Game), bSilent);
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (iRosterPosition < 0)
                return null;

            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition >= m_roster.Chars.Count)
                return null;

            byte[] bytesChar = m_roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            List<Item> backpack = new List<Item>(12);
            for (int i = 0; i < 12; i++)
            {
                if (bytesChar[EOB1.Offsets.Inventory + (i * 3) + 1] != 0)
                    backpack.Add(EOB1Item.FromEOB1InventoryBytes(bytesChar, EOB1.Offsets.Inventory + (i * 3)));
            }

            return backpack;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action, bool bForceRosterLoad = true)
        {
            if (!ValidateRosterFile(bForceRosterLoad))
                return -1;

            // Any un-created file is a potential inventory character (used to determine maximum bag size)
            if (action == InventoryCharAction.FindPotential && iStart >= m_roster.Chars.Count)
                return iStart;

            // Bard's Tale 3 doesn't have a fixed-size roster, so we need to search the entire roster
            // before using a new character
            byte[] bytes = new byte[CharacterSize];
            if (iStart >= m_roster.Chars.Count)
                iStart = m_roster.Chars.Count - 1;

            while (iStart >= 0)
            {
                BTCharacter EOB1Char = null;
                if (iStart < m_roster.Chars.Count)
                    EOB1Char = BTCharacter.Create(Game, 0, m_roster.Chars[iStart].Bytes, 0);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                    case InventoryCharAction.FindOrCreate:
                        if (EOB1Char != null && EOB1Char.Name.ToUpper().StartsWith("INVENTORY"))
                            return iStart;
                        break;
                    case InventoryCharAction.FindPotential:
                        if (EOB1Char == null || EOB1Char.Name.ToUpper().StartsWith("INVENTORY"))
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            // Didn't find any inventory characters in the list of existing files; create one if necessary
            if (action == InventoryCharAction.FindOrCreate)
            {
                // No character in the roster at this position; make a new one;
                m_roster.Chars.Add(new CharAndBytes(GetInventoryCharBytes()));
                int iNewCharIndex = m_roster.SaveCharBytes(m_roster.Chars.Count - 1);
                return iNewCharIndex;
            }

            return -1;
        }

        public override bool PrepareToSaveBag(int iMaxItems)
        {
            // Delete any current inventory characters and create enough to handle the full bag of items
            if (!ValidateRosterFile())
                return false;

            return true;
        }

        public override SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items)
        {
            if (iRosterPosition < 0)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= m_roster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            BTBackpackBytes bytes = new BTBackpackBytes(GetBackpackBytes(items));

            byte[] bytesChar = m_roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            Buffer.BlockCopy(bytes.Items, 0, bytesChar, EOB1.Offsets.Inventory, 36);
            m_roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        public override byte[] GetInventoryCharBytes() { return GetInventoryChar(); }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            EOB1MapData EOB1Data = new EOB1MapData(this, mb, GetCurrentMapIndex(), null);
            EOB1Data.LiveOnly = true;
            return EOB1Data;
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            EOB1MapData data = new EOB1MapData(this, GetCurrentMapBytes(), GetCurrentMapIndex(), null);

            data.Title = GetMapTitlePair(data.Index);
            data.Bounds = new Rectangle(0, 0, data.Width, data.Height);
            data.ItemList = GetItemTable();

            return data;
        }

        private MapBytes GetMapBytes(MemoryBytes bytes, int iIndex)
        {
            if (bytes == null || bytes.Length < (32 * 32 * EOB1MapData.BytesPerSquare))
                return null;

            return new MapBytes(bytes.Bytes, 32, 32);
        }

        public override MapBytes GetCurrentMapBytes()
        {
            int iIndex = GetCurrentMapIndex();
            MemoryBytes bytes = ReadOffset(EOB1.Memory.Map, 32 * 32 * EOB1MapData.BytesPerSquare);

            return GetMapBytes(bytes, iIndex);
        }

        public override MapBytes GetCurrentMapBytesLive()
        {
            // Return map bytes without monster/item/script information
            int iIndex = GetCurrentMapIndex();
            MemoryBytes bytes = ReadOffset(EOB1.Memory.Map, 32 * 32 * EOB1MapData.BytesPerSquare);
            for (int i = 0; i < bytes.Length; i += EOB1MapData.BytesPerSquare)
            {
                for (int j = 4; j < EOB1MapData.BytesPerSquare; j++)
                    bytes.Bytes[i + j] = 0;
            }

            return GetMapBytes(bytes, iIndex);
        }

        private Size GetMapSize()
        {
            return new Size(32, 32);
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            return PointFromPackedFive(ReadUInt16(Memory.LocationXY));
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            return WriteUInt16(Memory.LocationXY, PackedFiveFromPoint(ptLocation));
        }

        public override int GetCurrentMapIndex()
        {
            return ReadInt16(Memory.MainMapIndex);
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((EOB1Map)index)
            {
                case EOB1Map.None: return new MapTitleInfo(index, "Legend", "");
                case EOB1Map.Level1: return new MapTitleInfo(index, "Level 1: Upper Sewer Level");
                case EOB1Map.Level2: return new MapTitleInfo(index, "Level 2: Middle Sewer Level");
                case EOB1Map.Level3: return new MapTitleInfo(index, "Level 3: Lower Sewer Level");
                case EOB1Map.Level4: return new MapTitleInfo(index, "Level 4: Upper Level Dwarven Ruins");
                case EOB1Map.Level5: return new MapTitleInfo(index, "Level 5: Dwarven Ruins and Camp");
                case EOB1Map.Level6: return new MapTitleInfo(index, "Level 6: Bottom Level of Dwarven Ruins");
                case EOB1Map.Level7: return new MapTitleInfo(index, "Level 7: Upper Reaches of the Drow");
                case EOB1Map.Level8: return new MapTitleInfo(index, "Level 8: Drow Outcasts");
                case EOB1Map.Level9: return new MapTitleInfo(index, "Level 9: Lower Reaches of the Drow");
                case EOB1Map.Level10: return new MapTitleInfo(index, "Level 10: Xanthar's Outer Sanctum, Mantis Hive");
                case EOB1Map.Level11: return new MapTitleInfo(index, "Level 11: Xanthar's Outer Sanctum, Lower Reaches");
                case EOB1Map.Level12: return new MapTitleInfo(index, "Level 12: Xanthar's Inner Sanctum");
                default: return new MapTitleInfo(index, String.Format("Unknown({0})", index));
            }
        }

        public static string GetTownSpecial(int iSpecial)
        {
            switch (iSpecial)
            {
                default: return $"Special #{iSpecial}";
            }
        }

        protected override int GetNumChars()
        {
            return 6;
        }

        private EOBPartyInfo ReadEOB1PartyInfo()
        {
            if (m_block == null)
                return null;

            EOBGameState state = GetGameState() as EOBGameState;
            if (state == null)
                return null;

            MemoryBytes bytesStats = ReadOffset(Memory.PartyInfo, EOB1Character.SizeInMemory * 6);
            if (bytesStats == null)
                return null;
            byte numChars = 0;
            for (int i = 0; i < 6; i++)
                if ((bytesStats.Bytes[i * EOB1Character.SizeInMemory + 1] & 0x01) == 1)
                    numChars++;

            if (numChars == 0)
                return new EOBPartyInfo(new byte[0], new byte[0], numChars, null);

            int iInspecting = 0;
            if (state.Casting)
                iInspecting = state.ActingCaster;
            else if (state.InCombat)
                iInspecting = state.ActingCombatChar;
            else
                iInspecting = state.ActingCharAddress;

            Global.FixRange(ref iInspecting, 0, 6);
            EOBPartyInfo info = new EOBPartyInfo(ReadOffset(Memory.PartyInfo, EOB1Character.SizeInMemory * 6), GetMarchingOrder(), numChars, GetItemTable());

            info.State = state;

            info.ActingChar = (byte)iInspecting;
            info.ActingCaster = (byte)iInspecting;
            info.ActingCombatChar = (byte)iInspecting;
            info.InspectingCombatChar = (byte)iInspecting;

            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadEOB1PartyInfo();
        }

        public override byte[] GetMarchingOrder()
        {
            byte[] order = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            MemoryBytes mb = ReadOffset(Memory.MarchingOrder, 7);
            if (mb == null)
                return order;

            for (int i = 0; i < mb.Length; i++)
            {
                if (mb.Bytes[i] < 0 || mb.Bytes[i] > 6)
                    return order;
                mb.Bytes[i]++;      // BTMemoryHacker expects "1 to n", not "0 to n-1"
            }

            return mb.Bytes;
        }

        public override byte[] GetBackpackBytes(List<Item> items)
        {
            byte[] bytes = Global.NullBytes(36);
            for (int i = 0; i < 12; i++)
            {
                if (i >= items.Count)
                    continue;
                EOB1Item EOB1Item = items[i] as EOB1Item;
                if (EOB1Item == null)
                    continue;
                if (i >= items.Count)
                    break;
                byte[] bytesItem = EOB1Item.Serialize();
                Buffer.BlockCopy(bytesItem, 0, bytes, i * 3, bytesItem.Length);
            }
            return bytes;
        }

        public static MonsterName ExtractMonsterNames(byte[] monsters, int iOffset)
        {
            byte[] monster = Global.Subset(monsters, iOffset, 16);
            string strMonster = Global.GetLowAsciiString(monster);
            return ExtractMonsterNames(strMonster);
        }

        public static MonsterName ExtractMonsterNames(string strMonster)
        {
            int iSlash = strMonster.IndexOf('/');
            int iBack1 = strMonster.IndexOf('\\');
            int iBack2 = strMonster.IndexOf('\\', iBack1 + 1);
            if (iBack2 == -1)
                iBack2 = strMonster.Length;

            string strSingular = "";
            string strPlural = "";

            if (iSlash != -1 && iBack1 == -1)
            {
                strSingular = strMonster.Substring(0, iSlash);
                strPlural = strMonster.Substring(0, iSlash);
            }
            else if (iSlash == -1 && iBack1 != -1)
            {
                strSingular = strMonster.Substring(0, iBack1);
                strPlural = strMonster.Substring(0, iBack1) + strMonster.Substring(iBack1 + 1, iBack2 - iBack1 - 1);
            }
            else if (iSlash == -1 && iBack1 == -1)
            {
                strSingular = strMonster;
                strPlural = strMonster;
            }
            else
            {
                if (iBack1 == 0)
                    iBack1++;
                if (iSlash > iBack1) // Not valid, but can happen if the user is editing the monster name manually
                    iSlash = iBack1 - 1;
                strSingular = strMonster.Substring(0, iSlash) + strMonster.Substring(iSlash + 1, iBack1 - iSlash - 1);
                strPlural = strMonster.Substring(0, iSlash) + strMonster.Substring(iBack1 + 1, iBack2 - iBack1 - 1);
            }

            return new MonsterName(strSingular, strPlural);
        }

        public static string MonsterCount(byte[] monsters, int iIndex, int iCount)
        {
            if (monsters == null || iIndex >= monsters.Length / 32)
                return String.Format("Unknown#{0} ({1})", iIndex, iCount);

            MonsterName name = ExtractMonsterNames(monsters, iIndex * 32);

            return iCount == 1 ? name.Singular : String.Format("{0} {1}", iCount, name.Plural);
        }

        public override Size GetCurrentMapDimensions() { return new Size(32, 32); }

        public override MapBasicInfo GetCurrentMapInfo()
        {
            int iIndex = GetCurrentMapIndex();

            return new MapBasicInfo(iIndex, GetCurrentMapDimensions());
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            m_lastEncounterInfo = new EOB1EncounterInfo(GetMonsterLocations());
            m_lastEncounterInfo.Party = GetPartyInfo();
            m_lastEncounterInfo.PartyLocation = GetPartyPosition();

            return m_lastEncounterInfo;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is EOB1EncounterInfo) || !IsValid)
                return false;

            return true;
        }

        public override string[] GetCharacterNames()
        {
            MemoryBytes mb = ReadOffset(Memory.PartyInfo, EOB1Character.SizeInMemory * 7);
            if (mb == null)
                return new string[0];
            List<string> names = new List<string>(7);
            for (int i = 0; i < 7; i++)
            {
                if (mb.Bytes[i * EOB1Character.SizeInMemory] == 0x00)
                    break;
                names.Add(Global.GetNullTerminatedString(mb.Bytes, i * EOB1Character.SizeInMemory, 15));
            }

            return names.ToArray();
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            return null;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;
            return true;
        }

        public static byte[] GetInventoryChar() { return Properties.Resources.EOB1InventoryChar; }

        public override ScriptInfo GetScriptInfo(MemoryBytes scriptBytes)
        {
            int iIndex = GetCurrentMapIndex();
            return new EOB1ScriptInfo(GetMapTitle(iIndex), new EOB1Scripts(GetCurrentMapBytes(), scriptBytes.Bytes));
        }

        public override MemoryBytes GetScriptBytes()
        {
            return ReadOffset(Memory.MapSpecials, 12288);
        }

        public override bool SetScriptLine(ScriptLine line)
        {
            return WriteOffset(Memory.MapSpecials + line.Address, line.CommandBytes);
        }

        public override byte[] GetSpecialBytes()
        {
            return null;
        }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat, int valueSecondary = 0, PrimaryStat statSecondary = PrimaryStat.None, GenericClass gc = GenericClass.None)
        {
            return EOB1Character.GetStatModifier(value, stat, valueSecondary, statSecondary, gc);
        }

        public override string GetClassDescription(GenericClass gc)
        {
            switch (gc)
            {
                default: return "Unknown";
            }
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            return base.SetCharacterBytes(iAddress, bytes);
        }

        public HashSet<EOBItemIndex> GetAllInventoryItems()
        {
            HashSet<EOBItemIndex> list = new HashSet<EOBItemIndex>();
            int iChars = GetNumChars();

            MemoryBytes mb = ReadOffset(Memory.PartyInfo, iChars * CharacterMemorySize);
            if (mb == null)
                return list;

            for (int i = 0; i < iChars; i++)
            {
                for (int j = 0; j < MaxBackpackSize; j++)
                {
                    EOBItemIndex item = (EOBItemIndex)mb.Bytes[i * CharacterMemorySize + EOB1.Offsets.Inventory + (j * 3 + 1)];
                    if (!list.Contains(item))
                        list.Add(item);
                }
            }
            return list;
        }

        public override GenericClass GetCharacterClass(int iCharAddress)
        {
            return EOB1Character.GetBasicClass((EOBClass)ReadByte(Memory.PartyInfo + (CharacterMemorySize * iCharAddress) + EOB1.Offsets.Class));
        }

        public override bool VisitFacingSquare(BasicLocation location, MapSheet sheet)
        {
            // Stairs and ladders need to be visited automatically because the party is never technically in those squares
            if (sheet == null || location == null)
                return false;

            Point ptFacing = Global.OffsetPoint(location.PrimaryCoordinates, location.Facing);
            MapSquare squareFacing = sheet.GetSquareAtGridPoint(ptFacing);
            if (squareFacing != null && (squareFacing.HasIcon(IconName.StairsDown) || squareFacing.HasIcon(IconName.StairsUp)))
                return true;

            return false;
        }

        public EOB1Item[] GetItems(MapXY square, EOBItemLocation location)
        {
            MapBytes map = GetCurrentMapBytes();   // For alcove positioning
            byte[] bytesTable = GetItemTable();

            int itemPointer = EOB1MapData.GetItemPointer(map.Bytes, map.Size, EOB1MapData.BytesPerSquare, square.Location.X, square.Location.Y);

            return EOB1MapData.GetItems(bytesTable, itemPointer, location);
        }

        public override ItemLocations GetItemLocations(int iMapIndex, bool bForceNew = false)
        {
            if (!IsValid || iMapIndex == -1)
                return null;

            MapBytes map = GetCurrentMapBytes();   // For alcove positioning
            if (map == null)
                return null;

            byte[] bytesTable = GetItemTable(bForceNew);
            if (bytesTable == null)
                return null;

            ItemLocations locations = new ItemLocations();
            MemoryStream msRaw = new MemoryStream();
            // Add processed items to the list
            for (int i = 0; i <= (bytesTable.Length - 14); i += 14)
            {
                if (bytesTable[i + 12] == iMapIndex)
                {
                    EOB1Item item = EOB1Item.FromItemListBytes(GameNames.EyeOfTheBeholder1, bytesTable, i) as EOB1Item;
                    if (item.Image == 0)
                        continue;   // There are some "NULL" items hanging around; they're always in solid walls and showing them on the map is just confusing
                    // If the item is in an alcove in an adjacent square, "move" it to the square it looks like it's in from the player's perspective
                    // Unfortunately an item in "alcove" position will appear in any of the four adjacent walls if they are of type "alcove"
                    // however, practically this is a non-issue as there are no multi-alcove map squares in the game, so just find the one that is
                    // and change the location to that adjacent square instead.
                    if (item.Floor == EOBItemLocation.Alcove)
                    {
                        if (EOB1MapData.GetWall(map.Bytes, map.Size, EOB1MapData.BytesPerSquare, iMapIndex, item.Location.X, item.Location.Y, Direction.Down).Specials.HasFlag(WallSpecials.Alcove))
                            item.Location.Y++;
                        else if (EOB1MapData.GetWall(map.Bytes, map.Size, EOB1MapData.BytesPerSquare, iMapIndex, item.Location.X, item.Location.Y, Direction.Up).Specials.HasFlag(WallSpecials.Alcove))
                            item.Location.Y--;
                        else if (EOB1MapData.GetWall(map.Bytes, map.Size, EOB1MapData.BytesPerSquare, iMapIndex, item.Location.X, item.Location.Y, Direction.Right).Specials.HasFlag(WallSpecials.Alcove))
                            item.Location.X++;
                        else if (EOB1MapData.GetWall(map.Bytes, map.Size, EOB1MapData.BytesPerSquare, iMapIndex, item.Location.X, item.Location.Y, Direction.Left).Specials.HasFlag(WallSpecials.Alcove))
                            item.Location.X--;
                    }
                    Global.WriteInt16(msRaw, (short) item.Index);
                    msRaw.WriteByte((byte)item.Location.X);
                    msRaw.WriteByte((byte)item.Location.Y);
                    locations.AddItemPosition(item, item.Location);
                }
            }

            byte[] bytesProcessed = msRaw.ToArray();
            if (!bForceNew &&
                m_lastItemLocations != null &&
                Global.Compare(bytesProcessed, m_lastItemLocations.RawBytes))
                return m_lastItemLocations;

            locations.RawBytes = bytesProcessed;
            m_lastItemLocations = locations;
            return locations;
        }

        public override MonsterLocations GetMonsterLocations(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            MemoryBytes mbMonsters = ReadOffset(Memory.MonsterHP, 30 * 28);
            if (mbMonsters == null)
                return null;
            for (int i = 0; i < 30 * 28; i += 28)
            {
                // Null out bytes that change frequently but provide no useful information (e.g. animations)
                mbMonsters.Bytes[i + 1] = 0;
                mbMonsters.Bytes[i + 4] = 0;
                mbMonsters.Bytes[i + 5] = 0;
                mbMonsters.Bytes[i + 6] = 0;
                mbMonsters.Bytes[i + 7] = 0;
                mbMonsters.Bytes[i + 9] = 0;
                mbMonsters.Bytes[i + 10] = 0;
                mbMonsters.Bytes[i + 11] = 0;
                for (int j = 22; j < 28; j++)
                    mbMonsters.Bytes[i + j] = 0;
            }

            Point ptParty = GetPartyPosition();

            if (!bForceNew &&
                m_lastMonsterLocations != null &&
                Global.Compare(mbMonsters.Bytes, m_lastMonsterLocations.RawBytes) &&
                ptParty == m_lastMonsterLocations.PartyLocation)
                return m_lastMonsterLocations;

            m_lastMonsterLocations = new MonsterLocations();
            m_lastMonsterLocations.RawBytes = mbMonsters.Bytes;
            m_lastMonsterLocations.PartyLocation = ptParty;

            byte[] bytesItemTable = GetItemTable(false);
            for (int i = 0; i < 30; i++)
                m_lastMonsterLocations.AddEOBMonster(GameNames.EyeOfTheBeholder1, i, mbMonsters.Bytes, bytesItemTable, i * 28);
            return m_lastMonsterLocations;
        }

        public override bool KillAllMonsters()
        {
            // Killing monsters in Eye Of the Beholder involves setting their HP to zero
            // and moving their position to (0,0)

            MemoryBytes mbMap = ReadOffset(Memory.Map, 32 * 32 * EOB1MapData.BytesPerSquare);
            if (mbMap == null)
                return false;

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    byte bMonsters = mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 4];
                    mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 4] = (byte)(bMonsters & 0xF8);    // Number of monsters in the map square
                }
            }

            WriteOffset(mbMap);

            MemoryBytes mbMonsters = ReadOffset(Memory.MonsterHP, 30 * 28);
            if (mbMonsters == null)
                return false;

            for (int i = 0; i < 30 * 28; i += 28)
            {
                mbMonsters.Bytes[i + 2] = 0;    // x
                mbMonsters.Bytes[i + 3] = 0;    // y
                mbMonsters.Bytes[i + 14] = 0;   // hp
                mbMonsters.Bytes[i + 15] = 0;   // hp
            }

            return WriteOffset(mbMonsters);
        }

        public override bool SetMonster(Monster monster)
        {
            MemoryBytes mbSpecific = ReadOffset(Memory.MonsterHP + (monster.EncounterIndex * 28), 28);
            MemoryBytes mbType = ReadOffset(Memory.MonsterList + (monster.Index * 27), 27);

            EOBMonster eobMonster = monster as EOBMonster;
            if (mbSpecific == null || mbType == null || eobMonster == null)
                return false;

            byte[] bytesOrigType = Global.Clone(mbType.Bytes);

            mbSpecific.Bytes[8] = (byte)eobMonster.Speed;
            Global.SetInt16(mbSpecific.Bytes, 12, eobMonster.HP);
            Global.SetInt16(mbSpecific.Bytes, 14, eobMonster.CurrentHP);
            Global.SetInt16(mbSpecific.Bytes, 18, eobMonster.Items[0]);
            Global.SetInt16(mbSpecific.Bytes, 20, eobMonster.Items[1]);

            Point ptOld = PointFromPackedFive(BitConverter.ToUInt16(mbSpecific.Bytes, 2));
            if ((ptOld.X != monster.Position.X || ptOld.Y != monster.Position.Y) && Global.PointInRects(monster.Position, 0, 0, 32, 32))
            {
                MemoryBytes mbMap = ReadOffset(Memory.Map, 32 * 32 * EOB1MapData.BytesPerSquare);
                if (mbMap != null)
                {
                    int iOldPos = (ptOld.Y * 32 * EOB1MapData.BytesPerSquare) + (ptOld.X * EOB1MapData.BytesPerSquare) + 4;
                    int iNewPos = (monster.Position.Y * 32 * EOB1MapData.BytesPerSquare) + (monster.Position.X * EOB1MapData.BytesPerSquare) + 4;
                    // Moving the monster involves updating the map as well
                    mbMap.Bytes[iOldPos] = (byte)(mbMap.Bytes[iOldPos] == 0 ? 0 : mbMap.Bytes[iOldPos] - 1);
                    mbMap.Bytes[iNewPos]++;
                    Global.SetUInt16(mbSpecific.Bytes, 2, PackedFiveFromPoint(monster.Position));
                    WriteOffset(mbMap);
                }
            }

            WriteOffset(mbSpecific);

            eobMonster.SetBytes(mbType.Bytes);
            if (!Global.Compare(bytesOrigType, mbType.Bytes))
            {
                WriteOffset(mbType);
                EOB1.MonsterList.Value.Reinitialize(this, true);
            }
            return true;
        }

        public override Item CloneItem(Item item)
        {
            // Find an empty slot in the master item table and copy this item to it
            EOB1Item eobItem = item as EOB1Item;
            if (eobItem == null)
                return null;

            byte[] bytesTable = GetItemTable();
            // Start from the end of the table, since some empty spaces seem to be reserved by the game

            int iIndex = bytesTable.Length / 14 - 1;
            while (iIndex > 350)
            {
                if (bytesTable[iIndex * 14] == 0)
                {
                    byte[] bytesClone = eobItem.GetBytes();
                    // Zero out the location bytes for the clone
                    Global.SetBytes(bytesClone, 5, 8, 0);
                    Buffer.BlockCopy(bytesClone, 0, bytesTable, iIndex * 14, 14);
                    SetMasterItemTable(bytesTable);
                    return new EOB1Item(bytesClone, 0, iIndex);
                }
                iIndex--;
            }

            return null;
        }

        public override bool RemoveAllScripts()
        {
            MemoryBytes mbMap = ReadOffset(Memory.Map, 32 * 32 * EOB1MapData.BytesPerSquare);
            if (mbMap == null)
                return false;

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    byte bMonsters = mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 4];
                    mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 4] = (byte)(bMonsters & 0x07);
                    mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 7] = 0;
                    mbMap.Bytes[(j * 32 * EOB1MapData.BytesPerSquare) + (i * EOB1MapData.BytesPerSquare) + 8] = 0;
                }
            }

            WriteOffset(mbMap);
            return true;
        }
    }
}