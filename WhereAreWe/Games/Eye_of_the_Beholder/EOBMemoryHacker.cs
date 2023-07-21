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
    public class EOBQuestData : QuestData
    {
        public byte[] MapSpecials;
        public byte[] TownMap;

        public EOBQuestData(EOBPartyInfo party, LocationInformation location, EOBGameState state, byte[] mapSpecials, byte[] townMap, GameInfo gameInfo)
        {
            Info = gameInfo;
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
            EOBGameInfo eobInfo = Info as EOBGameInfo;
            if (eobInfo != null && eobInfo.GlobalBits != null)
                Global.WriteBytes(stream, eobInfo.GlobalBits.Bytes);
            if (eobInfo != null && eobInfo.LevelBits != null)
            {
                foreach(MemoryBytes mb in eobInfo.LevelBits)
                    Global.WriteBytes(stream, mb.Bytes);
            }
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
    public enum EOBGameStateFlags
    {
        AskCastSpell =          0x0001,
        AskWhichSpell =         0x0002,
        AskWhichSpellCombat =   0x0004,
        AskWhichSong =          0x0008,
    }

    public class EOBKnownSpells : DnDKnownSpells
    {
        // EOB uses 4 bytes to store the Mage spellbook and 60 bytes to store the memorized spells
        public EOBKnownSpells(byte[] bytes, int offset = 0) { RawBytes = Global.Subset(bytes, offset, 64); }
        public override int NumKnown { get { return Global.NumBitsSet(Global.Subset(RawBytes, 60, 4)); } }
        public override bool IsKnown(int index, SpellType type) { return IsKnown(index, GenericClass.None); }
        public override string KnownString(GenericClass charClass) { return String.Format("{0}", NumKnown); }
        public bool KnowsAny { get { return !Global.AllNull(RawBytes); } }
        const int FirstPriest = (int)EOBSpellIndex.Bless;

        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null)
        {
            return new EOBKnownSpells(Global.NullBytes(64));
        }

        public override bool IsKnown(int internalIndex, GenericClass mmClass) { return InBook(internalIndex); }

        public bool IsMemorized(int internalIndex, GenericClass mmClass)
        {
            if (internalIndex < FirstPriest)
            {
                for (int i = 0; i < 30; i++)
                    if (RawBytes[i] == internalIndex)
                        return true;
            }
            else
            {
                internalIndex = internalIndex - FirstPriest + 1;
                for (int i = 31; i < 60; i++)
                    if (RawBytes[i] == internalIndex)
                        return true;
            }
            return false;
        }

        private void AddSpells(List<Spell> spells, int iFirst, int iCount, bool bMage, bool bMemorized)
        {
            for (int i = iFirst; i < iFirst + iCount; i++)
            {
                if (i < RawBytes.Length && RawBytes[i] != 0)
                {
                    int iSpell = (sbyte)RawBytes[i];
                    if ((bMemorized && iSpell < 0) || (!bMemorized && iSpell > 0))
                        continue;

                    iSpell = Math.Abs(iSpell);
                    if (!bMage)
                        iSpell += (FirstPriest - 1);
                    if (iSpell < EOB1.Spells.Count)
                        spells.Add(EOB1.Spells[iSpell]);
                }
            }
        }

        public override List<Spell> Memorized
        {
            get
            {
                List<Spell> spells = new List<Spell>();
                AddSpells(spells, 0, 30, true, true);
                AddSpells(spells, 30, 30, false, true);
                return spells;
            }
        }

        public override List<Spell> Selected
        {
            get
            {
                List<Spell> spells = new List<Spell>();
                AddSpells(spells, 0, 30, true, false);
                AddSpells(spells, 30, 30, false, false);
                return spells;
            }
        }

        private int CountOf(byte value, int iStart, int iLength)
        {
            int iCount = 0;
            for (int i = iStart; i < iStart + iLength; i++)
            {
                if (RawBytes[i] == value)
                    iCount++;
            }
            return iCount;
        }

        public override bool InBook(int index)
        {
            if (index >= FirstPriest)
                return true;    // Priests know all spells
            return Global.IsBitSet(RawBytes[60 + (index - 1) / 8], (index - 1) % 8);
        }

        public override void SetBook(int index, bool bLearned = true)
        {
            if (index >= FirstPriest)
                return;    // Priests know all spells
            Global.SetBit(RawBytes, 60 * 8 + index - 1, bLearned ? 1 : 0, true);
        }

        public override int Available(SpellType type)
        {
            switch (type)
            {
                case SpellType.Mage: return CountOf(0, 0, 30);
                case SpellType.Cleric: return CountOf(0, 30, 30);
                default: return 0;
            }
        }

        public override int NumSelected(int index)
        {
            if (index < FirstPriest)
                return CountOf((byte)-index, 0, 30);
            return CountOf((byte)-(index - FirstPriest + 1), 30, 30);
        }

        public override int NumMemorized(int index)
        {
            if (index < FirstPriest)
                return CountOf((byte)index, 0, 30);
            return CountOf((byte)(index - FirstPriest + 1), 30, 30);
        }

        public int Count(bool bPositive, int iStart, int iLength)
        {
            int iCount = 0;
            for (int i = iStart; i < iStart + iLength; i++)
            {
                if (i >= RawBytes.Length)
                    break;
                if (bPositive && (sbyte)RawBytes[i] > 0)
                    iCount++;
                else if (!bPositive && (sbyte)RawBytes[i] < 0)
                    iCount++;
            }
            return iCount;
        }

        public override int NumSelected(SpellType type)
        {
            switch (type)
            {
                case SpellType.Mage: return Count(false, 0, 30);
                case SpellType.Cleric: return Count(false, 30, 30);
                case SpellType.Any: return Count(false, 0, 60);
                default: return 0;
            }
        }

        public override int NumMemorized(SpellType type)
        {
            switch (type)
            {
                case SpellType.Mage: return Count(true, 0, 30);
                case SpellType.Cleric: return Count(true, 30, 30);
                case SpellType.Any: return Count(true, 0, 60);
                default: return 0;
            }
        }

        private int FirstEmpty(int iStart, int iCount, bool bOrNegative)
        {
            int i = iStart;
            while (i < iStart + iCount)
            {
                if (RawBytes[i] == 0)
                    return i;
                if ((sbyte)RawBytes[i] < 0 && bOrNegative)
                    return i;
                i++;
            }
            return -1;
        }

        private int AddMageSpell(int iIndex, bool bMemorized)
        {
            int iEmpty = FirstEmpty(0, 30, bMemorized);
            if (iEmpty == -1)
                return -1;
            RawBytes[iEmpty] = bMemorized ? (byte)iIndex : (byte)-iIndex;
            return iEmpty;
        }

        private int AddClericSpell(int iIndex, bool bMemorized)
        {
            int iEmpty = FirstEmpty(30, 30, bMemorized);
            if (iEmpty == -1)
                return -1;
            RawBytes[iEmpty] = bMemorized ? (byte)iIndex : (byte)-iIndex;
            return iEmpty;
        }

        public override int[] Add(SpellTag spell)
        {
            List<int> indices = new List<int>();
            int iAddedAt = -1;
            if (spell.Spell.BasicIndex < FirstPriest)
                SetBook(spell.Spell.BasicIndex, spell.Castable);
            for (int i = 0; i < spell.Memorized; i++)
            {
                if (spell.Spell.Type == SpellType.Mage)
                    iAddedAt = AddMageSpell(spell.Spell.BasicIndex, true);
                else
                    iAddedAt = AddClericSpell(spell.Spell.BasicIndex - FirstPriest + 1, true);
                if (iAddedAt == -1)
                    break;
                indices.Add(iAddedAt);
            }
            for (int i = 0; i < spell.Selected; i++)
            {
                if (spell.Spell.Type == SpellType.Mage)
                    iAddedAt = AddMageSpell(spell.Spell.BasicIndex, false);
                else
                    iAddedAt = AddClericSpell(spell.Spell.BasicIndex - FirstPriest + 1, false);
                if (iAddedAt == -1)
                    break;
                indices.Add(iAddedAt);
            }
            return indices.ToArray();
        }
    }

    public class EOBGameState : GameState
    {
        public GameNames EOBGame = GameNames.EyeOfTheBeholder1;

        public override GameNames Game { get { return EOBGame; } }

        public bool CastingState = false;
        public override bool Casting { get { return Main == MainState.SelectSpell; } }
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

        public override bool NoActingChar => false;
    }

    public class EOBBackpackBytes
    {
        public byte[] Items;  // 8 Int16s

        public EOBBackpackBytes()
        {
            Items = Global.NullBytes(16);
        }

        public EOBBackpackBytes(byte[] bytes)
        {
            Items = bytes;
        }
    }

    public class EOBPartyInfo : PartyInfo
    {
        private Dictionary<int, EOBInventory> m_invCache = null;
        public byte[] ItemTable = null;

        public EOBInventory InventoryForChar(int iCharAddress, byte[] bytesItemTable)
        {
            if (iCharAddress < 0 || Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return null;
            if (m_invCache == null)
                m_invCache = new Dictionary<int, EOBInventory>();
            else if (m_invCache.ContainsKey(iCharAddress))
                return m_invCache[iCharAddress];
            EOBInventory inventory = EOBInventory.Create(Game, Bytes, bytesItemTable, iCharAddress * CharacterSize + Offsets.Inventory);
            m_invCache.Add(iCharAddress, inventory);
            return inventory;
        }

        public bool AnyCharHasItemType(int iItemTypeIndex, byte[] bytesItemTable, int iModifier = -0x10000)
        {
            // The item type is offset 4 in the master item table (14 bytes per item)
            for (int i = 0; i < NumChars; i++)
            {
                if (CharHasItemType(i, iItemTypeIndex, bytesItemTable, iModifier))
                    return true;
            }
            return false;
        }

        public bool AnyCharHasItem(EOB1ItemTableDefault iItemTableIndex, byte[] bytesItemTable)
        {
            for (int i = 0; i < NumChars; i++)
            {
                if (CharHasItem(i, (int) iItemTableIndex, bytesItemTable))
                    return true;
            }
            return false;
        }

        // Returns true if a particular character is carrying a very specific item from the master item table
        // (e.g. no two Kenku eggs are the same using this method)
        public bool CharHasItem(int iCharAddress, int iItemTableIndex, byte[] bytesItemTable)
        {
            if (bytesItemTable == null || iCharAddress < 0 || Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            for (int i = 0; i < 27; i++)
            {
                int iItemIndex = BitConverter.ToInt16(Bytes, iCharAddress * CharacterSize + EOB1.Offsets.Inventory + (2 * i));
                if (iItemIndex == iItemTableIndex)
                    return true;
            }
            return false;
        }

        public bool CharHasItemType(int iCharAddress, int iItemTypeIndex, byte[] bytesItemTable, int iModifier = -0x10000)
        {
            if (bytesItemTable == null || iCharAddress < 0 || Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            for (int i = 0; i < 27; i++)
            {
                int iItemIndex = BitConverter.ToInt16(Bytes, iCharAddress * CharacterSize + EOB1.Offsets.Inventory + (2 * i));
                if (iItemIndex >= 0 && iItemIndex + 4 < Bytes.Length && bytesItemTable[iItemIndex * 14 + 4] == iItemTypeIndex)
                {
                    if (iModifier == -0x10000)
                        return true;
                    return (sbyte)bytesItemTable[iItemIndex * 14 + 13] == iModifier;
                }
            }
            return false;
        }

        public EOBPartyInfo(byte[] bytes, byte[] order, byte numChars, byte[] itemTable)
        {
            Bytes = bytes;
            ItemTable = itemTable;
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

        public bool CharacterHasItem(GameNames game, int iCharAddress, int item, byte[] itemTable, bool bEquippedOnly = false)
        {
            EOBInventory inventory = InventoryForChar(iCharAddress, itemTable);
            return inventory == null ? false : inventory.HasItem(game, item, bEquippedOnly);
        }

        public bool CharacterHasItems(GameNames game, int iCharAddress, byte[] itemTable, params int[] items)
        {
            EOBInventory inventory = InventoryForChar(iCharAddress, itemTable);

            foreach (int item in items)
            {
                if (!inventory.HasItem(game, item, false))
                    return false;
            }
            return true;
        }

        public bool AllCharactersHaveItem(GameNames game, int item, byte[] itemTable, bool bEquippedOnly = false)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (!CharacterHasItem(game, Addresses[i], item, itemTable, bEquippedOnly))
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

        public bool CharacterHasCondition(int iCharAddress, EOBCondition conditionTest)
        {
            if (iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            EOBCondition condition = (EOBCondition) Bytes[iCharAddress * CharacterSize + Offsets.Condition];
            return condition.HasFlag(conditionTest);
        }

        public bool AnyCharacterHasCondition(EOBCondition conditionTest)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (CharacterHasCondition(Addresses[i], conditionTest))
                    return true;
            return false;
        }

        public bool CurrentPartyHasItem(GameNames game, byte[] itemTable, int item, bool bEquippedOnly = false)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(game, GetAddress(i), item, itemTable, bEquippedOnly))
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

        public bool CurrentPartyHasCondition(EOBCondition condition, bool bAll = false)
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

            EOBClass eobClass = (EOBClass)Bytes[iCharAddress * CharacterSize + Offsets.Class];
            switch (eobClass)
            {
                case EOBClass.Fighter:
                case EOBClass.FighterThief:
                case EOBClass.Thief:
                case EOBClass.Ranger:
                    return false;
                default:
                    return true;
            }
        }

        public bool CharacterKnowsSpell(int iCharAddress, EOBSpellIndex spell)
        {
            if (iCharAddress < 0)
                return false;

            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            if (!CharacterIsCaster(iCharAddress))
                return false;

            return Global.GetBit(Bytes, (iCharAddress * CharacterSize + Offsets.Spells) * 8 + (int) spell - 1) != 0;
        }

        public bool CurrentPartyKnowsSpell(EOBSpellIndex spell)
        {
            for (int i = 0; i < Addresses.Length; i++)
                if (CharacterKnowsSpell(GetAddress(i), spell))
                    return true;
            return false;
        }

        public bool CharacterHasItem(int iCharAddress, byte[] itemTable, EOBItemIndex type)
        {
            EOBInventory inventory = InventoryForChar(iCharAddress, itemTable);
            if (inventory == null)
                return false;

            return inventory.Items.Any(i => i is EOB1Item && ((EOB1Item)i).ItemIndex == type);
        }

        public bool CurrentPartyHasItem(EOBItemIndex item, byte[] itemTable) { return CurrentPartyHasItem(GameNames.EyeOfTheBeholder1, itemTable, (int)item); }

        public bool CurrentPartyHasEquipped(EOBItemIndex item, byte[] itemTable) { return CurrentPartyHasItem(GameNames.EyeOfTheBeholder1, itemTable, (int)item, true); }

        public override int CharacterSize { get { return Games.CharacterSize(State.Game); } }
    }

    public class EOBSpellInfo : SpellInfo
    {
        public EOBPartyInfo Party;
        public override bool ClassLimited { get { return false; } }

        public EOBSpellInfo()
        {
            Party = null;
            Game = new EOBGameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingCharAddress = -1;
        }
    }

    public class EOBGameInfo : GameInfo
    {
        public bool InCombat = false;
        public int MainMap = 0;
        public MemoryBytes[] LevelBits = new MemoryBytes[12];
        public MemoryBytes GlobalBits = null;
    }

    public class EOBMapData : MapData
    {
        public override int DefaultZoom { get { return 100; } }

        public byte[] Squares;
        public byte[] ItemList;

        public bool ItemInSpot(EOB1ItemTableDefault item, MapXY spot)
        {
            int iOffset = ((int) item) * 14;
            if (ItemList == null || ((int) item) > ItemList.Length - 14)
                return false;
            Point pt = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(ItemList, iOffset + 6));
            return spot.Map == ItemList[iOffset + 12] && pt.X == spot.X && pt.Y == spot.Y;
        }
    }

    public class EOBCharCreationInfo : CharCreationInfo
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

    public class EOBActiveSquares : ActiveSquares
    {
        byte[] ForcedEncounters = null;

        public EOBActiveSquares(MainForm main, int mapIndex, Size szMap, byte[] mapSpecials, byte[] forcedEncounters)
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

    public abstract class EOBMemoryHacker : MemoryHacker
    {
        public override PrimaryStat[] StatOrder { get { return StatOrderSIDCL; } }
        public override bool SpellsUseLevelOnly { get { return true; } }
        public abstract EOBMemory Memory { get; }

        public override byte[] MainSearch { get { return Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return Memory.Guesses; } }
        protected int m_iCurrentMapAdventuringCount = 0;
        public override int MaxInventoryChar { get { return 0; } }
        public override int MaxBackpackSize { get { return 14; } }
        protected virtual List<Item> GetSuperItems(GenericClass btClass) { return new List<Item>(0); }
        public override CreationAssistantControl CreateCreationAssistantControl(IMain main) { return new EOBCreationAssistantControl(main); }
        protected EOBEncounterInfo m_lastEncounterInfo = null;
        public override bool SpellsHaveDuration { get { return true; } }
        protected virtual QuestInfoBase CreateQuestInfo() { return null; }
        protected virtual EOBQuestData CreateQuestData() { return null; }
        public override bool UsesAlignment { get { return false; } }
        public virtual byte[] GetSpecialBytes() { return null; }
        private DateTime m_dtLastAutoCombat = DateTime.Now;
        private DateTime m_dtLastAutoPreCombat = DateTime.Now;
        private DateTime m_dtLastAutoConfirmRound = DateTime.Now;
        public override bool HasScripts { get { return true; } }
        public override bool HasRoamingMonsters => true;
        public override string SpellType1 => "Cleric";
        public override string SpellType3 => "Mage";
        public override BitIndex DefaultBitIndex => BitIndex.SevenToZero;

        public override StatModifier GetStatModifier(int value, PrimaryStat stat, int valueSecondary = 0, PrimaryStat statSecondary = PrimaryStat.None, GenericClass gc = GenericClass.None)
        {
            return EOBCharacter.GetStatModifier(value, stat, valueSecondary, statSecondary, gc);
        }

        public override GameScripts GetScripts(MemoryBytes bytes) { return GetScriptInfo(bytes).Scripts; }

        public static string FacingString(int i, bool bAbbrev = false)
        {
            switch (i)
            {
                case 0: return bAbbrev ? "N" : "North";
                case 1: return bAbbrev ? "E" : "East";
                case 2: return bAbbrev ? "S" : "South";
                case 3: return bAbbrev ? "W" : "West";
                case 255: return bAbbrev ? "x" : "None";
                default: return bAbbrev ? "?" : "Unknown";
            }
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

        public override BaseCharacter CreateCharFromBytes(byte[] bytes, byte[] bytesItemTable = null)
        {
            if (bytes == null)
                return null;

            return EOBCharacter.Create(Game, bytesItemTable, 0, bytes, 0);
        }

        protected virtual int InventoryOffset { get { return Games.GetCharacterOffsets(Game).Inventory; } }
        protected virtual int InventoryLength { get { return Games.GetCharacterOffsets(Game).InventoryLength; } }
        protected virtual EOBInventory CreateInventory(List<Item> items) { return EOBInventory.Create(Game, items); }
        protected virtual EOBInventory CreateInventory(byte[] bytesChar) { return EOBInventory.Create(Game, bytesChar, GetItemTable(), InventoryOffset); }
        protected virtual EOBGameState GetMainState() { return null; }
        protected virtual int ItemListLength => 499;

        private DateTime m_dtLastItemTable = DateTime.MinValue;
        private byte[] m_bytesLastItemTable = null;

        public byte[] GetItemTable(bool bForceNew = true)
        {
            if (bForceNew || m_bytesLastItemTable == null || (DateTime.UtcNow - m_dtLastItemTable).TotalMilliseconds > 1000)
            {
                m_dtLastItemTable = DateTime.UtcNow;
                m_bytesLastItemTable = ReadOffset(Memory.ItemList, 14 * ItemListLength)?.Bytes;
            }
            return m_bytesLastItemTable;
        }

        public override GameState GetGameState() { return ReadEOB123GameState(); }

        private EOBGameState ReadEOB123GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as EOBGameState;     // Don't spam the game state from different windows

            EOBGameState state = GetMainState();
            if (state == null)
                return null;

            state.EOBGame = Game;
            state.Location = GetLocationForce();
            state.InShop = false;

            m_gsCurrent = state;
            return state;
        }

        protected virtual int GetNumChars() { return 0; }
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
            byte bFacing = ReadByte(Memory.Facing);
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
            info.CanUseBag = false; // No roster of characters -> no place to hold items
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
            return ReadOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory, offsets.InventoryLength).Bytes;
        }

        public virtual byte[] GetBackpackBytes(List<Item> items)
        {
            byte[] bytes = Global.NullBytes(28);
            for (int i = 0; i < 14; i++)
            {
                if (i >= items.Count)
                    break;
                Global.SetInt16(bytes, i * 2, ((EOBItem)items[i]).ItemListIndex);

            }
            return bytes;
        }

        public virtual bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            CharacterOffsets offsets = Games.CharacterOffsets(Game);
            return WriteOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory, bytes);
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            if (items == null || (items.Count > 0 && !(items[0] is EOBItem)))
                return SetBackpackResult.InvalidItems;

            EOBInventory inv = null;
            inv = EOBInventory.Create(Game, GetBackpackBytes(iCharAddress), GetItemTable());

            List<Item> listNew = bRemoveEquipped ? new List<Item>() : inv.SelectEquippedItems;
            int iNextPackIndex = 0;
            foreach (Item item in items)
            {
                if (!bRemoveEquipped && item is EOBItem)
                {
                    item.MemoryIndex = iNextPackIndex + 2;
                    ((EOBItem)item).EOBInvLocation = EOBEquipPosition.Backpack1 + iNextPackIndex++;
                }
                listNew.Add(item);
            }

            if (items.Count(i => !i.IsEquipped) > MaxBackpackSize)
                return SetBackpackResult.InsufficientSpace;

            byte[] bytes = EOBInventory.Create(Game, listNew).GetBytes(GetCharacterClass(iCharAddress));
            SetBackpackBytes(iCharAddress, bytes);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            List<Item> list = new List<Item>();

            if (!IsValid || iCharAddress < 0)
                return list;

            byte[] bytes = GetBackpackBytes(iCharAddress);
            EOBInventory inv = EOBInventory.Create(Game, bytes, GetItemTable());

            return inv.SelectUnequippedItems;
        }

        public abstract List<EOBItem> EOBItems { get; }

        protected virtual EOBGameInfo CreateGameInfo() { return null; }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            EOBGameInfo info = CreateGameInfo();

            MemoryStream stream = new MemoryStream();

            info.MainMap = ReadByte(Memory.MainMapIndex);
            MemoryBytes mb = ReadOffset(Memory.ScriptBits, 4 * 12);
            if (mb == null)
                return null;
            for (int i = 0; i < 12; i++)
                info.LevelBits[i] = mb.GetRange(i * 4, 4);
            info.GlobalBits = ReadOffset(Memory.ScriptBits2, 4);

            stream.WriteByte((byte)info.MainMap);
            stream.Write(mb.Bytes, 0, mb.Length);
            stream.Write(info.GlobalBits.Bytes, 0, info.GlobalBits.Length);

            info.Bytes = stream.ToArray();

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is EOBGameInfo))
                return GetGameInfo();

            EOBGameInfo EOBOld = infoOld as EOBGameInfo;
            EOBGameInfo EOBNew = GetGameInfo() as EOBGameInfo;

            if (EOBNew == null)
                return null;

            if (Global.Compare(EOBOld.Bytes, EOBNew.Bytes))
                return infoOld; // All the bytes are the same; return the old object

            return EOBNew;
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
            EOBPartyInfo pi = GetPartyInfo() as EOBPartyInfo;
            if (pi == null)
                return null;

            byte[] order = GetMarchingOrder();

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            byte[] bytesItemTable = GetItemTable();
            for (int i = 0; i < pi.NumChars; i++)
            {
                if (i >= order.Length)
                    break;
                int iChar = order[i] - 1;
                EOBCharacter EOBChar = EOBCharacter.Create(Game, bytesItemTable, iChar, pi.Bytes, iChar * CharacterSize);
                EOBChar.Address = iChar;
                chars.Add(EOBChar);
            }

            return chars;
        }

        public override string PleaseFormPartyString { get { return "Please form a party and enter the dungeon."; } }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            EOBCharacter eobChar = baseChar as EOBCharacter;
            if (!IsValid || eobChar == null)
                return;

            // Eye of the beholder's items are pointers to items in the main list, so we need to verify that those
            // items actually exist before using them.
            List<int> validItems = new List<int>(500);
            byte[] bytesTable = GetItemTable();
            int iLength = bytesTable.Length / 14;
            for (int i = 0; i < iLength; i++)
            {
                // An item is valid if the x/y bytes are not FF FF
                int iOffset = i * 14;
                if (!(bytesTable[iOffset + 6] == 0xFF && bytesTable[iOffset + 7] == 0xFF))
                    validItems.Add(i);
            }

            byte[] newItems = Global.NullBytes(28);
            // Pick 14 random valid items from the list (no duplicates)
            for (int i = 0; i < 14; i++)
            {
                if (validItems.Count < 2)   // Item #0 isn't valid
                    break;
                int iItem = Global.Rand.Next(validItems.Count - 1) + 1;
                Global.SetInt16(newItems, i * 2, validItems[iItem]);
                validItems.RemoveAt(iItem);
            }

            WriteOffset(Memory.PartyInfo + (eobChar.Address * CharacterMemorySize) + eobChar.Offsets.Inventory + 4, newItems);
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

            EOBCharCreationInfo info = new EOBCharCreationInfo();
            info.Race = EOBCharacter.GetBasicRace((EOBRace) ReadByte(Memory.CreationRace));
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
            info.State = ReadEOB123GameState();
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

        public virtual EOBSpellInfo CreateSpellInfo() { return new EOBSpellInfo(); }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            EOBSpellInfo info = CreateSpellInfo();
            IntPtr pRead = IntPtr.Zero;
            info.Game = GetGameState() as EOBGameState;

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = GetPartyInfo() as EOBPartyInfo;

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

        public override QuestInfoBase GetQuestInfo(QuestInfoBase lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            QuestInfoBase info = CreateQuestInfo();
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

        public override GameReadyState GameReady
        {
            // "Ready" for EOB games is slightly different as we want to be able to send keys to the
            // pre-game DOS window as well as the game itself
            get
            {
                EOBGameState state = ReadEOB123GameState();

                switch (state.Main)
                {
                    case MainState.DosIntro1:
                        return GameReadyState.Ready;
                    default:
                        return base.GameReady;
                }
            }
        }

        public override bool SkipIntroductions(int iTimeout = 27000, bool bPreLaunch = false)
        {
            if (bPreLaunch)
            {
                TweakSleep(500);
                SendKeysToDOSBox(new Keys[] { Keys.D4 }, true);
                TweakSleep(10);
                SendKeysToDOSBox(new Keys[] { Keys.D1 }, true);
                TweakSleep(10);
                SendKeysToDOSBox(new Keys[] { Keys.Y }, true);
                TweakSleep(200);
                SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                return true;
            }

            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                EOBGameState state = ReadEOB123GameState();
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Adventuring:
                            TweakSleep(1000);
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
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

            EOBInventory inv = EOBInventory.Create(Game, GetBackpackBytes(iCharAddress), GetItemTable());
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
            // Backpacks in Eye of the Beholder can always be traded as they are always the same size
            long iPack1 = Memory.PartyInfo + (iCharAddress1 * CharacterSize) + EOB.Offsets.Inventory + 4;
            long iPack2 = Memory.PartyInfo + (iCharAddress2 * CharacterSize) + EOB.Offsets.Inventory + 4;
            MemoryBytes mbPack1 = ReadOffset(iPack1, 28);
            MemoryBytes mbPack2 = ReadOffset(iPack2, 28);

            WriteOffset(iPack1, mbPack2.Bytes);
            return WriteOffset(iPack2, mbPack1.Bytes);
        }

        public override bool AutoCombat()
        {
            return false;
        }

        public override Modifiers GetExternalModifiers(BaseCharacter baseChar)
        {
            Modifiers mod = new Modifiers();
            return mod;
        }

        public override bool IsDungeon(int iMap) { return iMap != 0; }
        public override Size GetCurrentMapDimensions() { return GetMapDimensions(GetCurrentMapIndex()); }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            return null;
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

        public static Point PointFromPackedFive(ushort position)
        {
            return new Point(position & 0x001F, (position & 0x03E0) >> 5);
        }

        public static ushort PackedFiveFromPoint(Point ptLocation)
        {
            return (ushort)(ptLocation.X | (ptLocation.Y << 5));
        }

        public bool SetMasterItemTable(byte[] bytes)
        {
            if (bytes == null)
                return false;

            return WriteOffset(Memory.ItemList, bytes);
        }

        public MemoryBytes GetMapSquare(int x, int y)
        {
            if (x < 0 || y < 0 || x > 31 || y > 31)
                return null;

            int offset = (y * 32 * 9) + (x * 9);
            return ReadOffset(Memory.Map + offset, 9);
        }

        public bool SetMapSquare(int x, int y, int dir, int wall)
        {
            if (x < 0 || y < 0 || x > 31 || y > 31 || dir < -1 || dir > 3 || wall < 0 || wall > 255)
                return false;

            int offset = (y * 32 * 9) + (x * 9);
            MemoryBytes mb = ReadOffset(Memory.Map + offset, 4);
            if (mb == null)
                return false;
            switch (dir)
            {
                case -1:
                    for (int i = 0; i < 4; i++)
                        mb.Bytes[i] = (byte)wall;
                    break;
                default:
                    mb.Bytes[dir] = (byte)wall;
                    break;
            }
            return WriteOffset(mb);
        }
    }
}
