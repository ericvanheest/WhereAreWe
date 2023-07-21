using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum GameReadyState
    {
        NotReady,
        NeedDelay,
        Ready
    }

    public enum MainState
    {
        Unknown,
        Main,
        CreateSelectClass,
        CreateSelectRace,
        CreateSelectAlignment,
        CreateSelectSex,
        CreateSelectName,
        SaveCharacter,
        Inn,
        Training,
        TrainingNoExp,
        TrainingNoGold,
        TrainSuccess,
        CastLevel,
        CastNumber,
        QueryTarget,
        PressEnter,
        EnterFlyLetter,
        EnterFlyNumber,
        SelectTownPortal,
        AfterTownPortal,
        Opening,
        Options,
        About,
        CopyDisk,
        MainMenu,
        Transfer,
        ViewAll,
        ViewChar,
        RenameChar,
        DeleteChar,
        GoToTown,
        Adventuring,
        SignIn,
        CharacterScreen,
        QuickRef,
        Controls,
        Dismiss,
        Exchange,
        Rest,
        PreCombat,
        Combat,
        Shop,
        Tavern,
        Map,
        Temple,
        Rolling,
        CreateExchangeStat,
        WaitingNumericInput,
        TavernPurchased,
        TavernHaveADrink,
        TavernSpecialties,
        TavernRumors,
        MageGuild,
        InnAcceptHireling,
        CastQuickspell,
        SelectSpell,
        SelectSpellTarget,
        Inventory,
        QuestItems,
        EndGame,
        DiscardItem,
        Bank,
        EnchantItem,
        Question,
        LoadingMap,
        Teleport,
        EdgeOfTown,
        Castle,
        TavernRemoveChar,
        TavernAddChar,
        TavernInspect,
        TavernInspectRead,
        TavernInspectTrade,
        Utilities,
        ChangeName,
        MoveInsertDisc,
        MoveSelectChars,
        TrainingInspectSelectChar,
        TrainingInspectCharSelected,
        TrainingInspecting,
        TrainingInspectChangeClass,
        TrainingInspectRead,
        Roster,
        Camp,
        CampInspecting,
        CampInspectingRead,
        CampInspectingCastDropUse,
        CampReorder,
        CampEquip,
        UseSelectItem,
        DropSelectItem,
        CombatOptions,
        CombatSelectFightTarget,
        CombatSelectSpell,
        Treasure,
        CreateSelectPassword,
        CreateKeepChar,
        Opening2,
        LoadGame,
        InsertDisk,
        TreasureWhoWillDisarm,
        TreasureEnterTrapType,
        TreasureWhoWillOpen,
        TreasureWhoWillInspect,
        TreasureWhoWillCalfo,
        TreasureCouldNotDisarm,
        CombatConfirmRound,
        CombatFriendly,
        ReceiveExp,
        SelectSave,
        PentagramText,
        PentagramSelect,
        Transitional,
        CombatUseItem,
        Barter,
        ReceiveGold,
        Pause,
        SelectSpellCaster,
        SelectBard,
        SelectBardSong,
        CantPerformAction,
        MoveSelectPosition,
        UseSelectCharacter,
        ItemSelectAction,
        ItemSelectTarget,
        ShopBuyItem,
        ShopSellItem,
        ShopIdentifyItem,
        ShopInspectChar,
        TreasureQuestion,
        SaveParty,
        LeaveGame,
        RemoveChar,
        AddChar,
        CombatPartyAttack,
        CombatSelectBardSong,
        LoadingGuild,
        ShopInspecting,
        BankOpenAccount,
        BankCloseAccount,
        BankListAccounts,
        BankChooseOption,
        ReviewMain,
        ReviewWhoClass,
        ReviewWhoSpell,
        ReviewWhoAdvance,
        TempleNoHealing,
        ShopChooseOption,
        TempleWhoWillPay,
        EnergyMain,
        EnergyWhoWillPay,
        GuildRemove,
        GuildDiskOptions,
        GuildMain,
        ReviewWhoTalk,
        TradeItemTo
    }

    public enum GenericRace
    {
        None = 0,
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Gnome = 4,
        HalfOrc = 5,
        Hobbit = 6,
        HalfElf = 7,
    }

    public enum GenericClass
    {
        None = 0,
        Knight = 1,
        Paladin = 2,
        Archer = 3,
        Cleric = 4,
        Sorcerer = 5,
        Robber = 6,
        Ninja = 7,
        Barbarian = 8,
        Druid = 9,
        Ranger = 10,
        Fighter = 11,
        Mage = 12,
        Priest = 13,
        Thief = 14,
        Bishop = 15,
        Samurai = 16,
        Lord = 17,
        Bard = 18,
        Conjurer = 19,
        Hunter = 20,
        Magician = 21,
        Monk = 22,
        Rogue = 23,
        Warrior = 24,
        Wizard = 25,
        Archmage = 26,
        Monster = 27,
        Chronomancer = 28,
        Geomancer = 29
    }

    public enum GenericAlignmentValue
    {
        None = 0,
        Good = 1,
        Neutral = 2,
        Evil = 3
    }

    public class GenericAlignment
    {
        public GenericAlignmentValue Temporary;
        public GenericAlignmentValue Permanent;

        public GenericAlignment(GenericAlignmentValue temp, GenericAlignmentValue perm)
        {
            Temporary = temp;
            Permanent = perm;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", BaseCharacter.AlignmentString(Permanent), BaseCharacter.AlignmentString(Temporary));
        }
    }

    public enum CureAllResult
    {
        Success,
        NoSpellPoints,
        NoGems,
        NoSpellLevel,
        AntiMagic,
        InCombat,
        Incapacitated,
        NotHealer,
        SpellNotKnown,
        Error,
        MonstersNearby
    }

    public class GameState
    {
        public MainState Main;
        public bool InCombat;
        public bool InShop;
        public bool Inspecting;
        public LocationInformation Location;
        public int ActingPosition;
        public int ActingCharAddress;
        public int ActingCaster;
        public int ActingCombatChar;
        public DateTime ReadTime;
        public int[] States;
        public virtual Subscreen Subscreen { get { return Subscreen.Unknown; } set { } }
        public virtual bool Casting { get { return false; } }
        public virtual bool ActingIsCaster { get { return false; } }
        public virtual GameNames Game { get { return GameNames.None; } }

        public string StateString { get { return Global.States(States); } }

        public virtual bool IsTreasure
        {
            get
            {
                switch (Main)
                {
                    case MainState.Treasure:
                    case MainState.TreasureEnterTrapType:
                    case MainState.TreasureWhoWillDisarm:
                    case MainState.TreasureWhoWillOpen:
                    case MainState.TreasureWhoWillInspect:
                    case MainState.TreasureWhoWillCalfo:
                    case MainState.TreasureCouldNotDisarm:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public GameState()
        {
            ReadTime = DateTime.Now;
        }

        public virtual bool NotPlaying
        {
            get
            {
                switch (Main)
                {
                    case MainState.CreateSelectClass:
                    case MainState.CreateSelectRace:
                    case MainState.CreateSelectAlignment:
                    case MainState.CreateSelectSex:
                    case MainState.CreateSelectName:
                    case MainState.SaveCharacter:
                    case MainState.Opening:
                    case MainState.Options:
                    case MainState.About:
                    case MainState.CopyDisk:
                    case MainState.MainMenu:
                    case MainState.Transfer:
                    case MainState.ViewAll:
                    case MainState.ViewChar:
                    case MainState.RenameChar:
                    case MainState.DeleteChar:
                    case MainState.GoToTown:
                    case MainState.SignIn:
                    case MainState.Rolling:
                    case MainState.CreateExchangeStat:
                    case MainState.EndGame:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public virtual bool NoActingChar
        { 
            get 
            {
                if (InCombat && Main != MainState.PreCombat)
                    return false;   // Acting character is always important in combat

                switch (Main)
                {
                    case MainState.Adventuring:
                    case MainState.QuickRef:
                    case MainState.Question:
                    case MainState.PreCombat:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public virtual bool CharCreation
        {
            get
            {
                switch (Main)
                {
                    case MainState.CreateExchangeStat:
                    case MainState.CreateSelectAlignment:
                    case MainState.CreateSelectName:
                    case MainState.CreateSelectRace:
                    case MainState.CreateSelectSex:
                    case MainState.CreateSelectClass:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }

    public class TrainingInfo
    {
        public PartyInfo Party;
        public string MapName;
        public int MapIndex;

        public virtual bool InTraining { get { return false; } }
    }

    public enum DataType
    {
        Auto,
        Boolean,
        Byte,
        UInt16,
        Int16,
        UInt24,
        UInt32,
        Int32,
        Time,
        Bits,
        Bytes,
        Facing,
        Map8,
        Map16,
        MapAndPoint8,
        MapAndPoint16,
        MM1MapAndPoint16,
        MM2MapAndPoint8,
        Point8,
        Point16,
        UShorts,
        Exploration,
        Depth,
        CopyInt16,
        Unknown
    }

    public class OffsetList
    {
        public long[] Offsets;

        public int Length { get { return Offsets == null ? 0 : Offsets.Length; } }
        public long First { get { return Offsets == null ? 0 : Offsets[0]; } }

        public OffsetList(params long[] offsets)
        {
            Offsets = offsets;
        }
    }

    public class GameInfoItem
    {
        public enum ShowStyle
        {
            Hidden,
            Visible,
            Editable
        }

        public object Value;
        public string Description;
        public OffsetList Offsets;
        protected DataType m_type;       // number of bits
        protected ShowStyle m_style;
        public UInt32 Mask;

        public DataType Type { get { return m_type; } }
        public ShowStyle Style { get { return m_style; } }
        public BitDescriptionDelegate BitDescFunction;
        public ByteDescriptionDelegate ByteDescFunction;

        public virtual MapTitleInfo GetMapTitlePair(int iMap) { return new MapTitleInfo(iMap, String.Format("Map #{0}", iMap)); }

        public GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 bitMask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : this(desc, val, new OffsetList(offset), type, bitMask, style, fn)
        {
        }

        public GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 bitMask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
        {
            BitDescFunction = fn;
            Description = desc;
            m_type = type == DataType.Auto ? AutoType(val) : type;
            Offsets = offsets;
            m_style = style;
            Mask = bitMask;

            if (m_type == DataType.Boolean)
            {
                if (Mask == 0)
                    Value = Convert.ToInt32(val) != 0;
                else
                    Value = Convert.ToUInt32(val) & Mask;
            }
            else
                Value = val;
        }

        public GameInfoItem(string desc, ByteDescriptionDelegate fn, byte[] val, int offset)
        {
            ByteDescFunction = fn;
            Description = desc;
            m_type = DataType.Bytes;
            Offsets = new OffsetList(offset);
            m_style = ShowStyle.Editable;
            Mask = 0;
            Value = val;
        }

        private DataType AutoType(object obj)
        {
            if (obj is Byte) return DataType.Byte;
            if (obj is bool) return DataType.Boolean;
            if (obj is Int16) return DataType.Int16;
            if (obj is UInt16) return DataType.UInt16;
            if (obj is UInt32) return DataType.UInt32;
            if (obj is Int32) return DataType.Int32;
            if (obj is byte[]) return DataType.Bytes;
            if (obj is ushort[]) return DataType.UShorts;
            if (obj is AmountExplored) return DataType.Exploration;
            return DataType.Unknown;
        }

        private string MapString(int iMap)
        {
            if (iMap < 256)
                return String.Format("{0}", iMap);
            if (iMap == 256)
                return "0";
            return String.Format("{0} ({1})", iMap % 256, iMap);
        }

        private string GetValueString(object obj, DataType dt)
        {
            switch (dt)
            {
                case DataType.Boolean:
                    if (Mask == 0)
                        return (bool)obj ? "True" : "False";
                    return Convert.ToUInt32(Value) == 0 ? "False" : "True";
                case DataType.Byte: return String.Format("{0}", (byte)obj);
                case DataType.Int16:
                case DataType.CopyInt16:
                    return String.Format("{0}", (Int16)obj);
                case DataType.UInt16: return String.Format("{0}", (UInt16)obj);
                case DataType.UInt24: return String.Format("{0}", (Int32)obj & 0xffffff);
                case DataType.UInt32: return String.Format("{0}", (UInt32)obj);
                case DataType.Int32: return String.Format("{0}", (Int32)obj);
                case DataType.Bits: return String.Format("{0} set", Global.NumBitsSet(obj));
                case DataType.Bytes: return String.Format("{0} bytes", ((byte[])obj).Length);
                case DataType.Time: return Global.GetTimeString((UInt16)obj);
                case DataType.Facing: return FacingString((byte)obj);
                case DataType.Map16: return GetMapTitlePair(Convert.ToUInt16(obj)).ToString();
                case DataType.Map8: return GetMapTitlePair(Convert.ToByte(obj)).ToString();
                case DataType.MapAndPoint8: 
                    UInt16 iMAP = (UInt16) obj;
                    return String.Format("{0},{1}  {2}", iMAP & 0xf, (iMAP & 0xf0) >> 4, GetMapTitlePair(iMAP >> 8).Title);
                case DataType.MapAndPoint16:
                    byte[] pbMAP16 = obj as byte[];
                    return String.Format("{0},{1}  {2}", pbMAP16[1], pbMAP16[2], GetMapTitlePair(pbMAP16[0]).Title);
                case DataType.MM1MapAndPoint16:
                    byte[][] bytesMP16 = (byte[][])obj;
                    return String.Format("{0},{1}  {2}", bytesMP16[1][0], bytesMP16[1][1], GetMapTitlePair((int) MM1Memory.MM1PointerToMap(bytesMP16[0])).Title);
                case DataType.MM2MapAndPoint8:
                    byte[][] bytesMP8 = (byte[][])obj;
                    return String.Format("{0},{1}  {2}", bytesMP8[1][0] & 0xf, bytesMP8[1][0] >> 4, GetMapTitlePair(bytesMP8[0][0]).Title);
                case DataType.Point8:
                    byte bP8 = (byte)obj;
                    return String.Format("{0},{1}", bP8 & 0xf, bP8 >> 4);
                case DataType.Point16:
                    byte[] bP16 = (byte[])obj;
                    return String.Format("{0},{1}", bP16[0], bP16[1]);
                case DataType.UShorts:
                    StringBuilder sb = new StringBuilder();
                    foreach(ushort us in (obj as ushort[]))
                        sb.AppendFormat("{0},", us);
                    return Global.Trim(sb).ToString();
                case DataType.Depth:
                    switch ((byte) obj)
                    {
                        case 253: return "In Castle";
                        case 254: return "In Town";
                        case 255: return "Unknown";
                        case 0: return "Surface";
                        default: return String.Format("{0}' Under", ((byte) obj) * 10);
                    }
                case DataType.Exploration:
                    AmountExplored exp = (AmountExplored)obj;
                    return String.Format("{0}/{1}, {2:0.0}%", exp.Explored, exp.Total, exp.Percent);
                //case DataType.Map16: return GetMapTitlePair(Convert.ToUInt16(obj)).Title;
                //case DataType.Map8: return GetMapTitlePair(Convert.ToByte(obj)).Title;
                default: return String.Format("{0}", obj);
            }
        }

        public string ValueString { get { return GetValueString(Value, m_type); } }

        public MemoryBytes[] GetBytes()
        {
            switch (m_type)
            {
                case DataType.Boolean:
                    return MemoryBytes.Array(new byte[] { (byte) ((bool) Value ? 1 : 0) }, Offsets.First);
                case DataType.Byte:
                case DataType.Depth:
                case DataType.Map8:
                case DataType.Point8:
                case DataType.Facing:
                    return MemoryBytes.Array(new byte[] { (byte)Value }, Offsets.First);
                case DataType.Point16:
                    return MemoryBytes.Array((byte[])Value, Offsets.First);
                case DataType.Int16:
                    return MemoryBytes.Array(BitConverter.GetBytes((Int16)Value), Offsets.First);
                case DataType.CopyInt16:
                    byte[] bytesCopy = BitConverter.GetBytes((Int16)Value);
                    return MemoryBytes.Array(new byte[][] { bytesCopy, bytesCopy }, Offsets);
                case DataType.UInt16:
                case DataType.Time:
                case DataType.Map16:
                case DataType.MapAndPoint8:
                    return MemoryBytes.Array(BitConverter.GetBytes((UInt16)Value), Offsets.First);
                case DataType.MapAndPoint16:
                    return MemoryBytes.Array(Value as byte[], Offsets.First);
                case DataType.MM2MapAndPoint8:
                case DataType.MM1MapAndPoint16:
                    return MemoryBytes.Array((byte[][])Value, Offsets);
                case DataType.UInt24:
                    return MemoryBytes.Array(BitConverter.GetBytes((Int32)Value), Offsets.First);
                case DataType.UInt32:
                    return MemoryBytes.Array(BitConverter.GetBytes((UInt32)Value), Offsets.First);
                case DataType.Int32:
                    return MemoryBytes.Array(BitConverter.GetBytes((Int32)Value), Offsets.First);
                case DataType.Bits:
                case DataType.Bytes:
                    return MemoryBytes.Array((byte[])Value, Offsets.First);
                case DataType.UShorts:
                    return MemoryBytes.Array((ushort[]) Value, Offsets);
                default: return null;
            }
        }

        public virtual string FacingString(int i, bool bAbbrev = false) { return MM3MemoryHacker.FacingString(i, bAbbrev); }
    }

    public class GameInfo : IComparable<GameInfo>
    {
        public byte[] Bytes;
        public LocationInformation Location;
        public List<MMExit> Exits;
        public AmountExplored Exploration;
        public virtual List<GameInfoItem> GetPartyItems() { return null; }
        public virtual List<GameInfoItem> GetMapItems() { return null; }
        public virtual List<GameInfoItem> GetTimeItems() { return null; }
        public virtual List<GameInfoItem> GetEffectItems() { return null; }
        public virtual List<GameInfoItem> GetMiscItems() { return null; }

        public virtual byte[] QuestBytes { get { return Bytes; } }

        public GameInfo()
        {
            Bytes = null;
            Exits = null;
            Exploration = new AmountExplored(0, 0);
        }

        public virtual GameNames Game { get { return GameNames.None; } }

        public int CompareTo(GameInfo info)
        {
            if (Global.Compare(info.Bytes, Bytes))
                return 0;
            return 1;
        }
    }

    public class CharCreationInfo
    {
        public byte[] AttributesOriginal;
        public byte[] AttributesModified;
        public GameState State;
        public GenericRace Race;

        public virtual bool ValidValues { get { return false; } }
        public virtual bool OnCharCreation
        {
            get
            {
                return (
                    State.Main == MainState.CreateSelectClass ||
                    State.Main == MainState.CreateSelectAlignment ||
                    State.Main == MainState.CreateSelectRace ||
                    State.Main == MainState.CreateSelectSex ||
                    State.Main == MainState.CreateSelectName ||
                    State.Main == MainState.Rolling ||
                    State.Main == MainState.CreateExchangeStat ||
                    State.Main == MainState.SaveCharacter);
            }
        }
    }

    public class PartyInfo
    {
        public byte[] Bytes;
        public byte ActingChar;
        public byte ActingCaster;
        public byte ActingCombatChar;
        public byte InspectingCombatChar;
        public byte NumChars;
        public byte[] Positions;
        public int[] Addresses;
        protected byte[] m_bytesMarchingOrder;
        public GameState State;

        public virtual int MarchingIndex(int index) { return m_bytesMarchingOrder == null || m_bytesMarchingOrder.Length <= index ? index : m_bytesMarchingOrder[index]; }
        public virtual bool InCombatOrStore { get { return false; } }
        public virtual int CharacterSize { get { return 0; } }
        public virtual CharacterOffsets Offsets { get { return State == null ? null : Games.GetCharacterOffsets(State.Game); } }
        public virtual BaseCharacter CharFromAddress(int iAddress) { return null; }
        public virtual byte[] QuestBytes { get { return new byte[0]; } }
        public virtual int GetAddress(int i) { return Addresses == null || Addresses.Length <= i ? 0 : Addresses[i]; }
        public virtual GameNames Game { get { return State == null ? GameNames.None : State.Game; } }

        public byte PositionOfAddress(byte address)
        {
            for (byte i = 0; i < Addresses.Length; i++)
            {
                if (address == Addresses[i])
                    return i;
            }
            return 0;
        }
    }

    public class SpellInfo
    {
        public GameState Game;
        public virtual bool UsesSpellLevel { get { return false; }  }
        public virtual bool ClassLimited { get { return true; } }
        public virtual bool ShowFullList { get { return false; } }
    }

    public class CureAllInfo
    {
        public virtual bool IsHealer { get { return false; } }
        public virtual bool IsIncapacitated { get { return false; } }
        public virtual bool MagicPermitted { get { return false; } }
        public virtual bool Combat { get { return false; } }
        public virtual string CombatString { get { return "combat"; } }
        public virtual bool Valid { get { return false; } }
        public bool MonstersNearby = false;
    }

    public class SearchResults : IComparable<SearchResults>
    {
        public List<Item> Items;
        public int Gold;
        public int Gems;
        public virtual bool IsEmpty
        {
            get
            {
                return ((Items == null || Items.Count == 0) && Gold == 0 && Gems == 0);
            }
        }

        public virtual bool HasTraps { get { return false; } }
        public virtual string ContainerString { get { return "Treasure Chest"; } }
        public virtual byte[] Bytes { get { return null; } }

        public virtual string HeaderString { get { return "Treasure in the area!  Search!"; } }

        public virtual string ContentsString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Gold > 0)
                    sb.AppendFormat("{0} Gold\n", Gold);
                if (Gems > 0)
                    sb.AppendFormat("{0} Gems\n", Gems);
                if (Items != null)
                {
                    if (Items.Count > 0)
                        sb.AppendLine(Items[0].DescriptionString);
                    if (Items.Count > 1)
                        sb.AppendLine(Items[1].DescriptionString);
                    if (Items.Count > 2)
                        sb.AppendLine(Items[2].DescriptionString);
                }
                return sb.ToString();
            }
        }

        public virtual int CompareTo(SearchResults results)
        {
            if (results.Gold != Gold ||
                results.Gems != Gems)
                return 1;

            if (Items == null && results.Items == null)
                return 0;
            if (Items.Count != results.Items.Count)
                return 1;

            for (int i = 0; i < Items.Count; i++)
                if (Items[i].Index != results.Items[i].Index)
                    return 1;

            return 0;
        }
    }

    public class EncounterInfo : IComparable<EncounterInfo>
    {
        public int NumTotalMonsters;
        public int Round;
        public int NumMeleeMonsters;
        public PartyInfo Party;
        public SearchResults SearchResults;
        public byte[] AllBytes;
        public bool PreEncounter;
        public int HandicapTarget;
        public int HandicapValue;
        public Point PartyLocation = Global.NullPoint;
        public virtual bool MonstersOnMap { get { return false; } }

        public virtual Dictionary<int, Monster> Monsters { get { return null; } set { } }
        public virtual bool HasTreasure { get { return false; } }
        public virtual TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo) { return null; }
        public virtual string ExtraText { get { return ""; } }
        public virtual string ExtraTitleText { get { return ""; } }
        public virtual bool InCombat { get { return false; } }
        public virtual int NumLivingMonsters { get { return NumTotalMonsters; } }
        public virtual byte[] QuestBytes { get { return AllBytes; } }
        public virtual MeleeType GetMeleeType(int iChar, out string strWeapon) { strWeapon = String.Empty; return MeleeType.None; }

        public virtual string GetMeleeTip(MeleeType type, string strWeapon)
        {
            switch (type)
            {
                case MeleeType.FrontRowClose:
                    return String.Format("This front-row character is using a close-range weapon ({0}),\r\n which permits attacking only monster groups 1 and 2", strWeapon);
                case MeleeType.FrontRowShort:
                    return String.Format("This front-row character is using a short-range weapon ({0}),\r\n which permits attacking any monster group", strWeapon);
                case MeleeType.FrontRowMedium:
                    return String.Format("This front-row character is using a medium-range weapon ({0}),\r\n which permits attacking any monster group", strWeapon);
                case MeleeType.FrontRowLong:
                    return String.Format("This front-row character is using a long-range weapon ({0}),\r\n which permits attacking any monster group", strWeapon);
                case MeleeType.BackRowClose:
                    return String.Format("This back-row character is using a close-range weapon ({0}),\r\n which does not permit attacking any monster groups", strWeapon);
                case MeleeType.BackRowShort:
                    return String.Format("This back-row character is using a short-range weapon ({0}),\r\n which permits attacking only monster groups 1 and 2", strWeapon);
                case MeleeType.BackRowMedium:
                    return String.Format("This back-row character is using a medium-range weapon ({0}),\r\n which permits attacking monster groups 1-3", strWeapon);
                case MeleeType.BackRowLong:
                    return String.Format("This back-row character is using a long-range weapon ({0}),\r\n which permits attacking any monster group", strWeapon);
            }

            return String.Empty;
        }

        public virtual int CompareTo(EncounterInfo info)
        {
            if (info == null)
                return 1;

            if (info.AllBytes == null)
                return (AllBytes == null ? 0 : 1);

            return Global.Compare(info.AllBytes, AllBytes) ? 0 : 1;
        }
    }

    public enum MMExitDirection
    {
        None,
        North,
        East,
        South,
        West,
        Surface,
        Run,
        Surrender,
        Beacon
    }

    public class MMExit
    {
        public MMExitDirection Direction;
        public int Map;
        public Point Point;

        public byte[][] MM1MapAndPoint
        {
            get
            {
                return new byte[][] { MM1Memory.MM1MapPtrBytes((MM1Map) Map), new byte[] { (byte)Point.X, (byte)Point.Y } };
            }
        }

        public byte[][] MM2MapAndPoint { get { return new byte[][] { new byte[] { (byte)Map }, new byte[] { (byte)(Point.X | (Point.Y << 4)) } }; } }
        
        public MMExit()
        {
            Direction = MMExitDirection.None;
            Map = -1;
            Point = Global.NullPoint;
        }

        public static MMExit FromMM1MapAndPoint(byte[][] bytes)
        {
            return new MMExit(MMExitDirection.None, 
                (int) MM1Memory.MM1PointerToMap(bytes[0]),
                new Point(bytes[1][0], bytes[1][1]));
        }

        public MMExit(MMExitDirection dir, int map, Point pt)
        {
            Direction = dir;
            Map = map;
            Point = pt;
        }

        public MMExit(MMExitDirection dir, int map)
        {
            Direction = dir;
            Map = map;
            Point = Global.NullPoint;
        }

        public MMExit(MMExitDirection dir, Point pt)
        {
            Direction = dir;
            Map = -1;
            Point = pt;
        }

        public string DirectionString
        {
            get
            {
                switch (Direction)
                {
                    case MMExitDirection.North: return "North";
                    case MMExitDirection.East: return "East";
                    case MMExitDirection.South: return "South";
                    case MMExitDirection.West: return "West";
                    case MMExitDirection.Surface: return "Surface";
                    case MMExitDirection.Run: return "Run";
                    case MMExitDirection.Surrender: return "Surrender";
                    default: return "None";
                }
            }
        }
    }

    public enum MMEffects
    {
        None,
        ProtFear,
        ProtCold,
        ProtFire,
        ProtPoison,
        ProtAcid,
        ProtElectric,
        ProtMagic,
        LightFactors,
        LeatherSkin,
        Levitate,
        WaterWalk,
        GuardDog,
        PsychicProt,
        Bless,
        Invisibility,
        Shield,
        PowerShield,
        Cursed,
        ProtForces,
        WaterTransmutation,
        AirTransmutation,
        FireTransmutation,
        EarthTransmutation,
        EagleEye,
        WizardEye,
        HolyBonus,
        Entrapment,
        WizardEyeBool,
        LightBool
    }

    public class MMActiveEffects
    {
        public int ProtFear;
        public int ProtCold;
        public int ProtFire;
        public int ProtPoison;
        public int ProtAcid;
        public int ProtElectric;
        public int ProtMagic;
        public int LightFactors;
        public bool LeatherSkin;
        public bool Levitate;
        public bool WaterWalk;
        public bool GuardDog;
        public bool PsychicProt;
        public bool Bless;
        public bool Invisibility;
        public bool Shield;
        public bool PowerShield;
        public int Cursed;
        public int ProtForces;
        public bool WaterTransmutation;
        public bool AirTransmutation;
        public bool FireTransmutation;
        public bool EarthTransmutation;
        public int EagleEye;
        public int WizardEye;
        public int HolyBonus;
        public bool Entrapment;
        public bool WizardEyeBool;
        public bool LightBool;

        public byte[] Bytes
        {
            get
            {
                return new byte[]
                {
                    (byte) ProtFear,
                    (byte) ProtCold,
                    (byte) ProtFire,
                    (byte) ProtPoison,
                    (byte) ProtAcid,
                    (byte) ProtElectric,
                    (byte) ProtMagic,
                    (byte) LightFactors,
                    (byte) (LeatherSkin ? 1 : 0),
                    (byte) (Levitate ? 1 : 0),
                    (byte) (WaterWalk ? 1 : 0),
                    (byte) (GuardDog ? 1 : 0),
                    (byte) (PsychicProt ? 1 : 0),
                    (byte) (Bless ? 1 : 0),
                    (byte) (Invisibility ? 1 : 0),
                    (byte) (Shield ? 1 : 0),
                    (byte) (PowerShield ? 1 : 0),
                    (byte) Cursed,
                    (byte) ProtForces,
                    (byte) (WaterTransmutation ? 1 : 0),
                    (byte) (AirTransmutation ? 1 : 0),
                    (byte) (FireTransmutation ? 1 : 0),
                    (byte) (EarthTransmutation ? 1 : 0),
                    (byte) EagleEye,
                    (byte) WizardEye,
                    (byte) HolyBonus,
                    (byte) (Entrapment ? 1 : 0),
                    (byte) (WizardEyeBool ? 1 : 0),
                    (byte) (LightBool ? 1 : 0)
                };
            }
        }

        public byte[] MM3Bytes
        {
            get
            {
                MemoryStream ms = new MemoryStream(12);
                ms.WriteByte((byte) (Levitate ? 1 : 0));
                ms.WriteByte((byte) (WaterWalk ? 1 : 0));
                ms.WriteByte((byte) (WizardEyeBool ? 1 : 0));
                ms.WriteByte((byte) (LightBool ? 1 : 0));
                ms.Write(BitConverter.GetBytes((ushort)ProtFire), 0, 2);
                ms.Write(BitConverter.GetBytes((ushort)ProtElectric), 0, 2);
                ms.Write(BitConverter.GetBytes((ushort)ProtCold), 0, 2);
                ms.Write(BitConverter.GetBytes((ushort)ProtAcid), 0, 2);
                return ms.ToArray();
            }
        }

        public byte[] MM45Bytes
        {
            get
            {
                MemoryStream ms = new MemoryStream(12);
                //ms.WriteByte((byte)(Levitate ? 1 : 0));
                //ms.WriteByte((byte)(WaterWalk ? 1 : 0));
                //ms.WriteByte((byte)(WizardEyeBool ? 1 : 0));
                //ms.WriteByte((byte)(LightBool ? 1 : 0));
                //ms.Write(BitConverter.GetBytes((ushort)ProtFire), 0, 2);
                //ms.Write(BitConverter.GetBytes((ushort)ProtElectric), 0, 2);
                //ms.Write(BitConverter.GetBytes((ushort)ProtCold), 0, 2);
                //ms.Write(BitConverter.GetBytes((ushort)ProtAcid), 0, 2);
                return ms.ToArray();
            }
        }
    }

    public class MMEffectTag
    {
        public MMEffects Effect;
        public string EffectText;
        public bool Enabled;
        public int Value;
        public int Index;
        public int BytesPerValue;

        public MMEffectTag(MMEffects effect, string text, bool enabled, int value)
        {
            Effect = effect;
            EffectText = text;
            Enabled = enabled;
            Value = value;
            BytesPerValue = 1;
            Index = -1;
        }

        public MMEffectTag(MMEffects effect, string text, bool enabled, int value, int valSize)
        {
            Effect = effect;
            EffectText = text;
            Enabled = enabled;
            Value = value;
            BytesPerValue = valSize;
            Index = -1;
        }

        public MMEffectTag()
        {
            Effect = MMEffects.None;
            EffectText = String.Empty;
            Enabled = false;
            Value = 0;
            BytesPerValue = 1;
            Index = -1;
        }

        public static void UpdateEffects(Dictionary<MMEffects, MMEffectTag> effects)
        {
            Dictionary<MMEffects, MMEffectTag> tags = GetEffects();
            foreach (MMEffectTag tag in tags.Values)
            {
                if (!effects.ContainsKey(tag.Effect))
                    effects.Add(tag.Effect, tag);
            }
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffects(MMActiveEffects active, GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return GetEffectsMM1(active);
                case GameNames.MightAndMagic2: return GetEffectsMM2(active);
                case GameNames.MightAndMagic3: return GetEffectsMM3(active);
                default: return null;
            }
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffects(MMActiveEffects active = null)
        {
            if (active == null)
                active = new MMActiveEffects();

            Dictionary<MMEffects, MMEffectTag> effects = new Dictionary<MMEffects, MMEffectTag>(18);
            effects.Add(MMEffects.ProtFear, new MMEffectTag(MMEffects.ProtFear, "Protection: Fear", active.ProtFear != 0, active.ProtFear));
            effects.Add(MMEffects.ProtCold, new MMEffectTag(MMEffects.ProtCold, "Protection: Cold", active.ProtCold != 0, active.ProtCold));
            effects.Add(MMEffects.ProtFire, new MMEffectTag(MMEffects.ProtFire, "Protection: Fire", active.ProtFire != 0, active.ProtFire));
            effects.Add(MMEffects.ProtPoison, new MMEffectTag(MMEffects.ProtPoison, "Protection: Poison", active.ProtPoison != 0, active.ProtPoison));
            effects.Add(MMEffects.ProtAcid, new MMEffectTag(MMEffects.ProtAcid, "Protection: Acid", active.ProtAcid != 0, active.ProtAcid));
            effects.Add(MMEffects.ProtElectric, new MMEffectTag(MMEffects.ProtElectric, "Protection: Electricity", active.ProtElectric != 0, active.ProtElectric));
            effects.Add(MMEffects.ProtMagic, new MMEffectTag(MMEffects.ProtMagic, "Protection: Magic", active.ProtMagic != 0, active.ProtMagic));
            effects.Add(MMEffects.LightFactors, new MMEffectTag(MMEffects.LightFactors, "Light Factors", active.LightFactors != 0, active.LightFactors));
            effects.Add(MMEffects.LeatherSkin, new MMEffectTag(MMEffects.LeatherSkin, "Leather Skin", active.LeatherSkin, 0));
            effects.Add(MMEffects.Levitate, new MMEffectTag(MMEffects.Levitate, "Levitate", active.Levitate, 0));
            effects.Add(MMEffects.WaterWalk, new MMEffectTag(MMEffects.WaterWalk, "Walk on Water", active.WaterWalk, 0));
            effects.Add(MMEffects.GuardDog, new MMEffectTag(MMEffects.GuardDog, "Guard Dog", active.GuardDog, 0));
            effects.Add(MMEffects.PsychicProt, new MMEffectTag(MMEffects.PsychicProt, "Psychic Protection", active.PsychicProt, 0));
            effects.Add(MMEffects.Bless, new MMEffectTag(MMEffects.Bless, "Bless", active.Bless, 0));
            effects.Add(MMEffects.Invisibility, new MMEffectTag(MMEffects.Invisibility, "Invisibility", active.Invisibility, 0));
            effects.Add(MMEffects.Shield, new MMEffectTag(MMEffects.Shield, "Shield", active.Shield, 0));
            effects.Add(MMEffects.PowerShield, new MMEffectTag(MMEffects.PowerShield, "Power Shield", active.PowerShield, 0));
            effects.Add(MMEffects.Cursed, new MMEffectTag(MMEffects.Cursed, "Cursed", active.Cursed != 0, active.Cursed));
            effects.Add(MMEffects.ProtForces, new MMEffectTag(MMEffects.ProtForces, "Protection: Forces", active.ProtForces != 0, active.ProtForces));
            effects.Add(MMEffects.WaterTransmutation, new MMEffectTag(MMEffects.WaterTransmutation, "Water Transmutation", active.WaterTransmutation, 0));
            effects.Add(MMEffects.AirTransmutation, new MMEffectTag(MMEffects.AirTransmutation, "Air Transmutation", active.AirTransmutation, 0));
            effects.Add(MMEffects.FireTransmutation, new MMEffectTag(MMEffects.FireTransmutation, "Fire Transmutation", active.FireTransmutation, 0));
            effects.Add(MMEffects.EarthTransmutation, new MMEffectTag(MMEffects.EarthTransmutation, "Earth Transmutation", active.EarthTransmutation, 0));
            effects.Add(MMEffects.Entrapment, new MMEffectTag(MMEffects.Entrapment, "Entrapment", active.Entrapment, 0));
            effects.Add(MMEffects.EagleEye, new MMEffectTag(MMEffects.EagleEye, "Eagle Eye", active.EagleEye != 0, active.EagleEye));
            effects.Add(MMEffects.WizardEye, new MMEffectTag(MMEffects.WizardEye, "Wizard Eye", active.WizardEye != 0, active.WizardEye));
            effects.Add(MMEffects.HolyBonus, new MMEffectTag(MMEffects.HolyBonus, "Holy Bonus", active.HolyBonus != 0, active.HolyBonus));
            effects.Add(MMEffects.WizardEyeBool, new MMEffectTag(MMEffects.WizardEyeBool, "Wizard Eye", active.WizardEyeBool, 0));
            effects.Add(MMEffects.LightBool, new MMEffectTag(MMEffects.LightBool, "Light", active.LightBool, 0));
            return effects;
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffectsMM1(MMActiveEffects active = null)
        {
            if (active == null)
                active = new MMActiveEffects();

            Dictionary<MMEffects, MMEffectTag> effects = new Dictionary<MMEffects, MMEffectTag>(18);
            effects.Add(MMEffects.ProtFear, new MMEffectTag(MMEffects.ProtFear, "Protection: Fear", active.ProtFear != 0, active.ProtFear));
            effects.Add(MMEffects.ProtCold, new MMEffectTag(MMEffects.ProtCold, "Protection: Cold", active.ProtCold != 0, active.ProtCold));
            effects.Add(MMEffects.ProtFire, new MMEffectTag(MMEffects.ProtFire, "Protection: Fire", active.ProtFire != 0, active.ProtFire));
            effects.Add(MMEffects.ProtPoison, new MMEffectTag(MMEffects.ProtPoison, "Protection: Poison", active.ProtPoison != 0, active.ProtPoison));
            effects.Add(MMEffects.ProtAcid, new MMEffectTag(MMEffects.ProtAcid, "Protection: Acid", active.ProtAcid != 0, active.ProtAcid));
            effects.Add(MMEffects.ProtElectric, new MMEffectTag(MMEffects.ProtElectric, "Protection: Electricity", active.ProtElectric != 0, active.ProtElectric));
            effects.Add(MMEffects.ProtMagic, new MMEffectTag(MMEffects.ProtMagic, "Protection: Magic", active.ProtMagic != 0, active.ProtMagic));
            effects.Add(MMEffects.LightFactors, new MMEffectTag(MMEffects.LightFactors, "Light Factors", active.LightFactors != 0, active.LightFactors));
            effects.Add(MMEffects.LeatherSkin, new MMEffectTag(MMEffects.LeatherSkin, "Leather Skin", active.LeatherSkin, 0));
            effects.Add(MMEffects.Levitate, new MMEffectTag(MMEffects.Levitate, "Levitate", active.Levitate, 0));
            effects.Add(MMEffects.WaterWalk, new MMEffectTag(MMEffects.WaterWalk, "Walk on Water", active.WaterWalk, 0));
            effects.Add(MMEffects.GuardDog, new MMEffectTag(MMEffects.GuardDog, "Guard Dog", active.GuardDog, 0));
            effects.Add(MMEffects.PsychicProt, new MMEffectTag(MMEffects.PsychicProt, "Psychic Protection", active.PsychicProt, 0));
            effects.Add(MMEffects.Bless, new MMEffectTag(MMEffects.Bless, "Bless", active.Bless, 0));
            effects.Add(MMEffects.Invisibility, new MMEffectTag(MMEffects.Invisibility, "Invisibility", active.Invisibility, 0));
            effects.Add(MMEffects.Shield, new MMEffectTag(MMEffects.Shield, "Shield", active.Shield, 0));
            effects.Add(MMEffects.PowerShield, new MMEffectTag(MMEffects.PowerShield, "Power Shield", active.PowerShield, 0));
            effects.Add(MMEffects.Cursed, new MMEffectTag(MMEffects.Cursed, "Cursed", active.Cursed != 0, active.Cursed));
            return effects;
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffectsMM2(MMActiveEffects active = null)
        {
            if (active == null)
                active = new MMActiveEffects();

            Dictionary<MMEffects, MMEffectTag> effects = new Dictionary<MMEffects, MMEffectTag>(19);
            effects.Add(MMEffects.LightFactors, new MMEffectTag(MMEffects.LightFactors, "Light Factors", active.LightFactors != 0, active.LightFactors));
            effects.Add(MMEffects.Levitate, new MMEffectTag(MMEffects.Levitate, "Levitate", active.Levitate, 0));
            effects.Add(MMEffects.WaterWalk, new MMEffectTag(MMEffects.WaterWalk, "Walk on Water", active.WaterWalk, 0));
            effects.Add(MMEffects.GuardDog, new MMEffectTag(MMEffects.GuardDog, "Guard Dog", active.GuardDog, 0));
            effects.Add(MMEffects.Bless, new MMEffectTag(MMEffects.Bless, "Bless", active.Bless, 0));
            effects.Add(MMEffects.Invisibility, new MMEffectTag(MMEffects.Invisibility, "Invisibility", active.Invisibility, 0));
            effects.Add(MMEffects.Shield, new MMEffectTag(MMEffects.Shield, "Shield", active.Shield, 0));
            effects.Add(MMEffects.PowerShield, new MMEffectTag(MMEffects.PowerShield, "Power Shield", active.PowerShield, 0));
            effects.Add(MMEffects.Cursed, new MMEffectTag(MMEffects.Cursed, "Cursed", active.Cursed != 0, active.Cursed));
            effects.Add(MMEffects.ProtForces, new MMEffectTag(MMEffects.ProtForces, "Protection: Forces", active.ProtForces != 0, active.ProtForces));
            effects.Add(MMEffects.WaterTransmutation, new MMEffectTag(MMEffects.WaterTransmutation, "Water Transmutation", active.WaterTransmutation, 0));
            effects.Add(MMEffects.AirTransmutation, new MMEffectTag(MMEffects.AirTransmutation, "Air Transmutation", active.AirTransmutation, 0));
            effects.Add(MMEffects.FireTransmutation, new MMEffectTag(MMEffects.FireTransmutation, "Fire Transmutation", active.FireTransmutation, 0));
            effects.Add(MMEffects.EarthTransmutation, new MMEffectTag(MMEffects.EarthTransmutation, "Earth Transmutation", active.EarthTransmutation, 0));
            effects.Add(MMEffects.Entrapment, new MMEffectTag(MMEffects.Entrapment, "Entrapment", active.Entrapment, 0));
            effects.Add(MMEffects.EagleEye, new MMEffectTag(MMEffects.EagleEye, "Eagle Eye", active.EagleEye != 0, active.EagleEye));
            effects.Add(MMEffects.WizardEye, new MMEffectTag(MMEffects.WizardEye, "Wizard Eye", active.WizardEye != 0, active.WizardEye));
            effects.Add(MMEffects.HolyBonus, new MMEffectTag(MMEffects.HolyBonus, "Holy Bonus", active.HolyBonus != 0, active.HolyBonus));
            return effects;
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffectsMM345(MMActiveEffects active = null)
        {
            if (active == null)
                active = new MMActiveEffects();

            Dictionary<MMEffects, MMEffectTag> effects = new Dictionary<MMEffects, MMEffectTag>(4);
            effects.Add(MMEffects.Levitate, new MMEffectTag(MMEffects.Levitate, "Levitate", active.Levitate, 0));
            effects.Add(MMEffects.WaterWalk, new MMEffectTag(MMEffects.WaterWalk, "Walk on Water", active.WaterWalk, 0));
            effects.Add(MMEffects.WizardEyeBool, new MMEffectTag(MMEffects.WizardEyeBool, "Wizard Eye", active.WizardEyeBool, 0));
            effects.Add(MMEffects.LightBool, new MMEffectTag(MMEffects.LightBool, "Light", active.LightBool, 0));
            effects.Add(MMEffects.ProtFire, new MMEffectTag(MMEffects.ProtFire, "Prot: Fire", active.ProtFire != 0, active.ProtFire, 2));
            effects.Add(MMEffects.ProtElectric, new MMEffectTag(MMEffects.ProtElectric, "Prot: Electric", active.ProtElectric != 0, active.ProtElectric, 2));
            effects.Add(MMEffects.ProtCold, new MMEffectTag(MMEffects.ProtCold, "Prot: Cold", active.ProtCold != 0, active.ProtCold, 2));
            effects.Add(MMEffects.ProtAcid, new MMEffectTag(MMEffects.ProtAcid, "Prot: Acid", active.ProtAcid != 0, active.ProtAcid, 2));
            return effects;
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffectsMM3(MMActiveEffects active = null)
        {
            return GetEffectsMM345(active);
        }

        public static Dictionary<MMEffects, MMEffectTag> GetEffectsMM45(MMActiveEffects active = null)
        {
            return GetEffectsMM345(active);
        }

        public int MM1Offset
        {
            get
            {
                switch (Effect)
                {
                    case MMEffects.ProtFear: return MM1EffectsOffsets.ProtFear;
                    case MMEffects.ProtCold: return MM1EffectsOffsets.ProtCold;
                    case MMEffects.ProtFire: return MM1EffectsOffsets.ProtFire;
                    case MMEffects.ProtPoison: return MM1EffectsOffsets.ProtPoison;
                    case MMEffects.ProtAcid: return MM1EffectsOffsets.ProtAcid;
                    case MMEffects.ProtElectric: return MM1EffectsOffsets.ProtElectric;
                    case MMEffects.ProtMagic: return MM1EffectsOffsets.ProtMagic;
                    case MMEffects.LightFactors: return MM1EffectsOffsets.LightFactors;
                    case MMEffects.Cursed: return MM1EffectsOffsets.Cursed;
                    case MMEffects.LeatherSkin: return MM1EffectsOffsets.LeatherSkin;
                    case MMEffects.Levitate: return MM1EffectsOffsets.Levitate;
                    case MMEffects.GuardDog: return MM1EffectsOffsets.GuardDog;
                    case MMEffects.PsychicProt: return MM1EffectsOffsets.PsychicProt;
                    case MMEffects.WaterWalk: return MM1EffectsOffsets.WaterWalk;
                    case MMEffects.Bless: return MM1EffectsOffsets.Bless;
                    case MMEffects.Invisibility: return MM1EffectsOffsets.Invisibility;
                    case MMEffects.Shield: return MM1EffectsOffsets.Shield;
                    case MMEffects.PowerShield: return MM1EffectsOffsets.PowerShield;
                    default:
                        return -1;
                }
            }
        }

        public int MM2Offset
        {
            get
            {
                switch (Effect)
                {
                    case MMEffects.LightFactors: return MM2EffectsOffsets.LightFactors;
                    case MMEffects.ProtMagic: return MM2EffectsOffsets.ProtMagic;
                    case MMEffects.ProtForces: return MM2EffectsOffsets.ProtForces;
                    case MMEffects.Levitate: return MM2EffectsOffsets.Levitate;
                    case MMEffects.GuardDog: return MM2EffectsOffsets.GuardDog;
                    case MMEffects.WaterTransmutation: return MM2EffectsOffsets.WaterTransmutation;
                    case MMEffects.AirTransmutation: return MM2EffectsOffsets.AirTransmutation;
                    case MMEffects.FireTransmutation: return MM2EffectsOffsets.FireTransmutation;
                    case MMEffects.EarthTransmutation: return MM2EffectsOffsets.EarthTransmutation;
                    case MMEffects.EagleEye: return MM2EffectsOffsets.EagleEye;
                    case MMEffects.WizardEye: return MM2EffectsOffsets.WizardEye;
                    case MMEffects.Bless: return MM2EffectsOffsets.Bless;
                    case MMEffects.Invisibility: return MM2EffectsOffsets.Invisibility;
                    case MMEffects.Shield: return MM2EffectsOffsets.Shield;
                    case MMEffects.PowerShield: return MM2EffectsOffsets.PowerShield;
                    case MMEffects.HolyBonus: return MM2EffectsOffsets.HolyBonus;
                    case MMEffects.Entrapment: return MM2EffectsOffsets.Entrapment;
                    case MMEffects.Cursed: return MM2EffectsOffsets.Cursed;
                    case MMEffects.WaterWalk: return MM2EffectsOffsets.WaterWalk;
                    default:
                        return -1;
                }
            }
        }
    }

    [Flags]
    public enum MapAttributeFlags
    {
        None = 0x0000,
        AllowFly = 0x0001,
        AllowSurface = 0x0002,
        AllowTeleport = 0x0004,
        AllowEtherealize = 0x0008,
        AllowLloydsBeacon = 0x0010,
        AllowTownPortal = 0x0020,
        AllowSuperShelter = 0x0100,
        AllowTimeDistortion = 0x0200,
        AllowNaturesGate = 0x0400,
        Darkness = 0x0040,

        AllowBasicTransport = AllowSurface | AllowLloydsBeacon | AllowTownPortal
    }

    public class MapAttributes
    {
        public int Index;
        public MapAttributeFlags Flags;
        public List<MMExit> Exits;
        public Point SafestSquare;
        public Point SurrenderSquare;
        public int UndergroundLevel;
        public byte ForbiddenSpells;
        public byte FlyFlags;
        public int DefaultEra;
        public int EncounterSize;
        public int MonsterGroup;

        public byte[] Bytes;

        public MapAttributes()
        {
            Exits = new List<MMExit>();
        }

        public virtual bool IsOutside(Point pt) { return false; }
        public virtual bool SetOutside(Point pt, bool bOutside) { return false; }
    }

    public class BasicLocation
    {
        public Point PrimaryCoordinates;
        public Direction Facing;
        public bool Drawn;
        public int MapIndex;
        public int LightDistance;

        public BasicLocation()
        {
            PrimaryCoordinates = Global.NullPoint;
            Facing = Direction.Up;
            MapIndex = -1;
            Drawn = false;
            LightDistance = 4;
        }

        public BasicLocation(LocationInformation copy)
        {
            PrimaryCoordinates = copy.PrimaryCoordinates;
            Facing = copy.Facing;
            Drawn = copy.Drawn;
            MapIndex = copy.MapIndex;
            LightDistance = copy.LightDistance;
        }
    }

    public class LocationInformation
    {
        private Point m_ptPrimary;
        public Point SecondaryCoordinates;
        public Direction Facing;
        public byte NumChars;
        public Keys LastKeypress;
        public MMGenericSpell LastSpellNonCombat;
        public bool InInn;
        public bool CanUseBag;
        public bool Drawn;
        public int MapIndex;
        public bool Outside;
        public bool Town;
        public int LightDistance;

        public LocationInformation()
        {
            SetDefaults();
        }

        public Point PrimaryCoordinates
        {
            get { return m_ptPrimary; }
            set { m_ptPrimary = value; }
        }

        public bool IsAt(MapXY map)
        {
            return MapIndex == map.Map && map.X == PrimaryCoordinates.X && map.Y == PrimaryCoordinates.Y;
        }

        public static LocationInformation Empty
        {
            get
            {
                return new LocationInformation(Global.NullPoint, Global.NullPoint, null, Direction.None, Keys.None, MMGenericSpell.None, 0);
            }
        }

        public LocationInformation(Point primary)
        {
            m_ptPrimary = primary;
            SetDefaults();
        }

        public void SetDefaults()
        {
            SecondaryCoordinates = Global.NullPoint;
            MapIndex = -1;
            Facing = Direction.None;
            LastKeypress = 0;
            LastSpellNonCombat = MMGenericSpell.None;
            NumChars = 0;
            InInn = false;
            Drawn = false;
            Outside = false;
            Town = false;
            CanUseBag = false;
            LightDistance = 4;
        }

        public LocationInformation(Point primary, Point secondary, string name, Direction facing, Keys key, MMGenericSpell spell, byte numChars)
        {
            SetDefaults();
            m_ptPrimary = primary;
            SecondaryCoordinates = secondary;
            Facing = facing;
            LastKeypress = key;
            LastSpellNonCombat = spell;
            NumChars = numChars;
        }

        public byte[] QuestBytes
        {
            get
            {
                byte[] bytes = new byte[6];
                Global.SetInt16(bytes, 0, MapIndex);
                Global.SetInt16(bytes, 2, PrimaryCoordinates.X);
                Global.SetInt16(bytes, 4, PrimaryCoordinates.Y);
                return bytes;
            }
        }

        public byte[] GetBytes()
        {
            MemoryStream stream = new MemoryStream();
            byte[] bytes = BitConverter.GetBytes(PrimaryCoordinates.X);
            stream.Write(bytes, 0, bytes.Length);
            bytes = BitConverter.GetBytes(PrimaryCoordinates.Y);
            stream.Write(bytes, 0, bytes.Length);
            bytes = BitConverter.GetBytes(SecondaryCoordinates.Y);
            stream.Write(bytes, 0, bytes.Length);
            bytes = BitConverter.GetBytes(SecondaryCoordinates.Y);
            stream.Write(bytes, 0, bytes.Length);
            stream.WriteByte((byte)Facing);
            stream.WriteByte(NumChars);
            stream.WriteByte((byte)LastKeypress);
            stream.WriteByte((byte)LastSpellNonCombat);
            stream.WriteByte((byte)(InInn ? 1 : 0));
            stream.WriteByte((byte)(Drawn ? 1 : 0));
            stream.WriteByte((byte)MapIndex);
            stream.WriteByte((byte)(Outside ? 1 : 0));
            stream.WriteByte((byte)(Town ? 1 : 0));
            return stream.ToArray();
        }
    }

    public class MemoryBlock
    {
        public ulong Start;
        public ulong Length;
        public ulong Found;
        public bool IsFound = false;

        public MemoryBlock(ulong start, ulong length)
        {
            Start = start;
            Length = length;
            Found = 0;
            IsFound = false;
        }

        public MemoryBlock(ulong start, ulong length, ulong found)
        {
            Start = start;
            Length = length;
            Found = found;
            IsFound = true;
        }

        public ulong Offset(ulong offset)
        {
            if (offset > Length)
                return Start;
            return (Start + offset);
        }

        public override string ToString()
        {
            return String.Format("Start=0x{0:X}, Length=0x{1:X}", Start, Length);
        }
    }

    public class MapCartography
    {
        public int MapIndex;

        public enum EditAction
        {
            None,
            ClearSingle,
            FillSingle,
            ClearAll,
            FillAll
        }

        protected byte[] Bytes;
        public Size MapSize;

        public MapCartography()
        {
            Bytes = null;
            MapIndex = -1;
        }

        public byte[] GetBytes()
        {
            return Bytes;
        }

        public void SetBytes(byte[] bytes, Size size)
        {
            Bytes = bytes;
            MapSize = size;
        }

        public void SetFromQuad(MapCartography cart1, MapCartography cart2, MapCartography cart3, MapCartography cart4)
        {
            if (cart1 == null || cart2 == null || cart3 == null || cart4 == null ||
                cart1.Bytes == null || cart2.Bytes == null || cart3.Bytes == null || cart4.Bytes == null)
            {
                Bytes = null;
                return;
            }

            SetFromQuad(cart1.Bytes, cart2.Bytes, cart3.Bytes, cart4.Bytes);
        }

        public void SetFromPair(MapCartography cart1, MapCartography cart2, Orientation orient)
        {
            if (cart1 == null || cart2 == null || cart1.Bytes == null || cart2.Bytes == null)
            {
                Bytes = null;
                return;
            }

            SetFromPair(cart1.Bytes, cart2.Bytes, orient);
        }

        public void SetFromQuad(byte[] bytes1, byte[] bytes2, byte[] bytes3, byte[] bytes4)
        {
            MemoryStream ms = new MemoryStream(bytes1.Length * 4);
            ms.Write(bytes1, 0, bytes1.Length);
            ms.Write(bytes2, 0, bytes2.Length);
            ms.Write(bytes3, 0, bytes3.Length);
            ms.Write(bytes4, 0, bytes4.Length);
            Bytes = ms.ToArray();
            MapSize = new Size(32, 32);
        }

        public void SetFromPair(byte[] bytes1, byte[] bytes2, Orientation orient)
        {
            MemoryStream ms = new MemoryStream(bytes1.Length * 4);
            ms.Write(bytes1, 0, bytes1.Length);
            ms.Write(bytes2, 0, bytes2.Length);
            Bytes = ms.ToArray();

            if (orient == Orientation.Horizontal)
                MapSize = new Size(32, 16);
            else
                MapSize = new Size(16, 32);
        }

        public virtual bool SupportsSeen { get { return false; } }
        public virtual bool IsVisited(int x, int y) { return false; }
        public virtual bool IsSeen(int x, int y) { return IsVisited(x, y); }
    }

    public class MapData
    {
        public int Index;
        public MapTitleInfo Title;
        public bool LiveOnly = false;
        public MapSection[] Sections = null;
        public Rectangle Bounds = Rectangle.Empty;
        public Point LocationOffset = Point.Empty;
        public virtual int DefaultZoom { get { return 200; } }

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public MapData()
        {
            Index = -1;
            Bounds = new Rectangle(0, 0, 16, 16);
            Title = null;
        }

        public virtual void CopyMetadataFrom(MapData dataCopy)
        {
            Index = dataCopy.Index;
            Title = new MapTitleInfo(dataCopy.Title);
        }
    }

    public class MMMapData : MapData
    {
        public byte[] Appearance;
        public byte[] Attributes;
        public bool Outside;
        public bool Town;
        public bool Castle;
        public bool Mixed;
        public int BytesPerSquare;
        public MapCartography Cartography;
        public MMScriptInfo ScriptInfo;

        public MMMapData()
        {
            BytesPerSquare = 1;
            Bounds = new Rectangle(0, 0, 16, 16);
            Index = -1;
            Title = null;
            Town = false;
            Castle = false;
            Mixed = false;
            Cartography = new MapCartography();
        }

        public static byte[] CreateQuadBytes(int iRow, byte[] b1, byte[] b2, byte[] b3, byte[] b4)
        {
            byte[] bytes = new byte[iRow * 16 * 4];
            int iRow2 = 2 * iRow;

            for (int j = 0; j < 16; j++)
            {
                Buffer.BlockCopy(b1, j * iRow, bytes, j * iRow2, iRow);
                Buffer.BlockCopy(b2, j * iRow, bytes, j * iRow2 + iRow, iRow);
                Buffer.BlockCopy(b3, j * iRow, bytes, (bytes.Length / 2) + (j * iRow2), iRow);
                Buffer.BlockCopy(b4, j * iRow, bytes, (bytes.Length / 2) + (j * iRow2) + iRow, iRow);
            }

            return bytes;
        }

        public static byte[] CreatePairBytes(int iRow, byte[] b1, byte[] b2, Orientation dir)
        {
            byte[] bytes = new byte[iRow * 17 * 2];
            int iRow2 = 2 * iRow;

            for (int j = 0; j < 16; j++)
            {
                if (dir == Orientation.Horizontal)
                {
                    Buffer.BlockCopy(b1, j * iRow, bytes, j * iRow2, iRow);
                    Buffer.BlockCopy(b2, j * iRow, bytes, j * iRow2 + iRow, iRow);
                }
                else
                {
                    Buffer.BlockCopy(b1, j * iRow, bytes, j * iRow, iRow);
                    Buffer.BlockCopy(b2, j * iRow, bytes, (bytes.Length / 2) + j * iRow + iRow, iRow);
                }
            }

            return bytes;
        }

        public static MMMapData CreateFromQuad<T>(MMMapData data1, MMMapData data2, MMMapData data3, MMMapData data4) where T : MMMapData, new()
        {
            T data = new T();
            int iWidth = Math.Max(data1.Width, data3.Width) + Math.Max(data2.Width, data4.Width);
            int iHeight = Math.Max(data1.Height, data2.Height) + Math.Max(data3.Height, data4.Height);
            data.Appearance = new byte[iWidth * iHeight * data1.BytesPerSquare];

            data.Attributes = new byte[iWidth * iHeight];

            for (int j = 0; j < data1.Height; j++)
            {
                Buffer.BlockCopy(data1.Appearance, j * data1.Width * data1.BytesPerSquare, data.Appearance, j * iWidth * data1.BytesPerSquare, data1.Width * data1.BytesPerSquare);
                Buffer.BlockCopy(data1.Attributes, j * data1.Width, data.Attributes, j * iWidth, data1.Width);
            }

            for (int j = 0; j < data2.Height; j++)
            {
                Buffer.BlockCopy(data2.Appearance, j * data2.Width * data2.BytesPerSquare, data.Appearance, (j * iWidth + data1.Width) * data1.BytesPerSquare, data2.Width * data2.BytesPerSquare);
                Buffer.BlockCopy(data2.Attributes, j * data2.Width, data.Attributes, (j * iWidth + data1.Width), data2.Width);
            }

            int iHalfHeight = Math.Max(data1.Height, data2.Height);

            for (int j = 0; j < data3.Height; j++)
            {
                Buffer.BlockCopy(data3.Appearance, j * data3.Width * data3.BytesPerSquare, data.Appearance, iHalfHeight * iWidth * data1.BytesPerSquare + j * iWidth * data3.BytesPerSquare, data3.Width * data3.BytesPerSquare);
                Buffer.BlockCopy(data3.Attributes, j * data3.Width, data.Attributes, iHalfHeight * iWidth + j * iWidth, data3.Width);
            }

            for (int j = 0; j < data4.Height; j++)
            {
                Buffer.BlockCopy(data4.Appearance, j * data4.Width * data4.BytesPerSquare, data.Appearance, iHalfHeight * iWidth * data1.BytesPerSquare + (j * iWidth + data1.Width) * data1.BytesPerSquare, data4.Width * data4.BytesPerSquare);
                Buffer.BlockCopy(data4.Attributes, j * data4.Width, data.Attributes, iHalfHeight * iWidth + (j * iWidth + data1.Width), data4.Width);
            }

            data.Bounds = new Rectangle(0, 0, iWidth, iHeight);

            data.CopyMetadataFrom(data1);

            data.Cartography.SetFromQuad(data1.Cartography, data2.Cartography, data3.Cartography, data4.Cartography);

            return data;
        }

        public static MMMapData CreateFromPair<T>(MMMapData data1, MMMapData data2, Orientation orient) where T : MMMapData, new()
        {
            T data = new T();
            int iWidth = (orient == Orientation.Horizontal ? data1.Width + data2.Width : Math.Max(data1.Width, data2.Width));
            int iHeight = (orient == Orientation.Horizontal ? Math.Max(data1.Height, data2.Height) : data1.Height + data2.Height);
            data.Appearance = new byte[iWidth * iHeight * data1.BytesPerSquare];
            data.Attributes = new byte[iWidth * iHeight];

            for (int j = 0; j < data1.Height; j++)
            {
                Buffer.BlockCopy(data1.Appearance, j * data1.Width * data1.BytesPerSquare, data.Appearance, j * iWidth * data1.BytesPerSquare, data1.Width * data1.BytesPerSquare);
                Buffer.BlockCopy(data1.Attributes, j * data1.Width, data.Attributes, j * iWidth, data1.Width);
            }

            if (orient == Orientation.Horizontal)
            {
                for (int j = 0; j < data2.Height; j++)
                {
                    Buffer.BlockCopy(data2.Appearance, j * data2.Width * data2.BytesPerSquare, data.Appearance, (j * iWidth + data1.Width) * data1.BytesPerSquare, data2.Width * data2.BytesPerSquare);
                    Buffer.BlockCopy(data2.Attributes, j * data2.Width, data.Attributes, (j * iWidth + data1.Width), data2.Width);
                }
            }
            else
            {
                int iHalfHeight = Math.Max(data1.Height, data2.Height);
                for (int j = 0; j < data2.Height; j++)
                {
                    Buffer.BlockCopy(data2.Appearance, j * data2.Width * data2.BytesPerSquare, data.Appearance, iHalfHeight * iWidth * data1.BytesPerSquare + j * iWidth * data2.BytesPerSquare, data2.Width * data2.BytesPerSquare);
                    Buffer.BlockCopy(data2.Attributes, j * data2.Width, data.Attributes, iHalfHeight * iWidth + j * iWidth, data2.Width);
                }
            }

            data.Bounds = new Rectangle(0, 0, iWidth, iHeight);
            data.CopyMetadataFrom(data1);

            data.Cartography.SetFromPair(data1.Cartography, data2.Cartography, orient);

            return data;
        }

        public byte[] AppearanceAt(Point pt)
        {
            return AppearanceAt(pt.X, pt.Y);
        }

        public byte[] AppearanceAt(int x, int y)
        {
            int iOffset = y * Width * BytesPerSquare + x * BytesPerSquare;
            if (iOffset + BytesPerSquare > Appearance.Length)
                return null;
            byte[] bytes = new byte[BytesPerSquare];
            Buffer.BlockCopy(Appearance, iOffset, bytes, 0, BytesPerSquare);
            return bytes;
        }

        public byte[] AttributesAt(Point pt)
        {
            return AttributesAt(pt.X, pt.Y);
        }

        public byte AttributeByteAt(int x, int y)
        {
            return AttributesAt(x, y)[0];
        }

        public byte[] AttributesAt(int x, int y)
        {
            int iOffset = y * Width * BytesPerSquare + x * BytesPerSquare;
            if (iOffset + BytesPerSquare > Attributes.Length)
                return null;
            byte[] bytes = new byte[BytesPerSquare];
            Buffer.BlockCopy(Attributes, iOffset, bytes, 0, BytesPerSquare);
            return bytes;
        }

        public void SetAttributesAt(Point pt, byte[] b)
        {
            SetAttributesAt(pt.X, pt.Y, b);
        }

        public void SetAttributesAt(int x, int y, byte[] b)
        {
            int iOffset = y * Width * BytesPerSquare + x * BytesPerSquare;
            if (iOffset + BytesPerSquare > Attributes.Length)
                return;
            Buffer.BlockCopy(b, 0, Attributes, iOffset, BytesPerSquare);
        }

        public void SetAttributesAt(int x, int y, int i)
        {
            int iOffset = y * Width * BytesPerSquare + x * BytesPerSquare;
            if (iOffset + BytesPerSquare > Attributes.Length)
                return;
            Buffer.BlockCopy(BitConverter.GetBytes(i), 0, Attributes, iOffset, BytesPerSquare);
        }

        public void SetHighBits(MapXY[] spots, bool bSet)
        {
            foreach (MapXY spot in spots)
                SetHighBit(spot, bSet);
        }

        public void SetHighBit(MapXY spot, bool bSet)
        {
            if (bSet)
                SetAttributesAt(spot.X, spot.Y, AttributeByteAt(spot.X, spot.Y) & 0x7f);
            else
                SetAttributesAt(spot.X, spot.Y, AttributeByteAt(spot.X, spot.Y) | 0x80);
        }

        public override void CopyMetadataFrom(MapData dataCopy)
        {
            base.CopyMetadataFrom(dataCopy);
            if (dataCopy is MMMapData)
            {
                MMMapData data = dataCopy as MMMapData;
                BytesPerSquare = data.BytesPerSquare;
                ScriptInfo = data.ScriptInfo;
                Outside = data.Outside;
            }
        }
    }

    public abstract class FileOffsets
    {
        public abstract uint[] Maps { get; }
        public abstract uint[] Scripts { get; }
        public abstract uint[] Monsters { get; }
        public virtual uint AlternateGameMapOffset(int iMap) { return 0; }
        public virtual uint AlternateGameScriptStart(int iMap) { return 0; }
        public virtual uint AlternateGameMonsterStart(int iMap) { return 0; }
    }

    public abstract class FileQuestInfo
    {
        public bool Valid { get; set; }
        public bool AlternateGameVersion = false;

        protected class FileAndMemoryInfo
        {
            public byte[] File;
            public byte[] MemoryScript;
            public List<MemoryBytes> MemoryMap;
            public byte[] MemoryMonsters;
            public int Map;

            public FileAndMemoryInfo(byte[] file, byte[] bytesScript, byte[] bytesMonsters, List<MemoryBytes> bytesMap, int map)
            {
                File = file;
                MemoryScript = bytesScript;
                MemoryMonsters = bytesMonsters;
                MemoryMap = bytesMap;
                Map = map;
            }
        }

        protected Point GetMonsterLocation(FileOffsets fileOffsets, int map, uint offset, FileAndMemoryInfo info)
        {
            if ((int)map == -1)
                return Global.NullPoint;

            if (map == info.Map)
            {
                if (info.MemoryMonsters != null && offset < info.MemoryMonsters.Length - 1)
                    return new Point(info.MemoryMonsters[offset], info.MemoryMonsters[offset + 1]);
                return Global.NullPoint;
            }

            map %= 256;     // The FileOffsets object will have only the low byte

            uint iMonsters = fileOffsets.Monsters[map];
            if (AlternateGameVersion)
                iMonsters = fileOffsets.AlternateGameMonsterStart(map);
            if (offset < info.File.Length - iMonsters)
                return new Point(info.File[iMonsters + offset], info.File[iMonsters + offset + 1]);

            return Global.NullPoint;
        }

        protected byte MapScriptByte(FileOffsets fileOffsets, int map, uint offset, FileAndMemoryInfo info, bool bScript = true)
        {
            if (map == -1)
                return 0;

            if (map == info.Map)
            {
                if (bScript)
                {
                    if (offset < info.MemoryScript.Length)
                        return info.MemoryScript[offset];
                }
                else
                {
                    foreach (byte[] bytes in info.MemoryMap)
                    {
                        if (offset < bytes.Length)
                            return bytes[offset];
                        offset -= (uint)bytes.Length;
                    }
                }
                return 0;
            }

            map %= 256;     // The FileOffsets object will have only the low byte

            if (bScript)
            {
                uint iScriptOffset = fileOffsets.Scripts[map];
                if (AlternateGameVersion)
                    iScriptOffset = fileOffsets.AlternateGameScriptStart(map);
                if (offset < info.File.Length - iScriptOffset)
                    return info.File[iScriptOffset + offset];
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    uint iMapOffset = fileOffsets.Maps[map * 4 + i];
                    if (AlternateGameVersion)
                        iMapOffset += fileOffsets.AlternateGameMapOffset(map);
                    if (offset < info.File.Length - iMapOffset)
                        return info.File[iMapOffset + offset];
                    offset -= 0x300;
                }
            }

            return 0;
        }

        public QuestGoal Goal(bool b)
        {
            if (!Valid)
                return QuestGoal.BadFile;
            return b ? QuestGoal.Complete : QuestGoal.Incomplete;
        }

        public QuestGoal Goal(byte b, byte compare = 0)
        {
            return Goal(b == compare);
        }

        public void NullPoints(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = Global.NullPoint;
        }

        public FileQuestInfo()
        {
            Valid = false;
        }

        public abstract byte[] GetBytes();
    }

    [Flags]
    public enum BasicQuestType
    {
        Side =       0x00000000,
        Primary =    0x00000001,
        Unofficial = 0x00000002
    }

    public class DestroyQuestStatus : QuestStatus
    {
        public DestroyQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Destroy";
        }
    }

    public class DefeatQuestStatus : QuestStatus
    {
        public DefeatQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Defeat";
        }
    }

    public class LocateQuestStatus : QuestStatus
    {
        public LocateQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Find";
        }
    }

    public class LiberateQuestStatus : QuestStatus
    {
        public LiberateQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Free";
        }
    }

    public class RetrieveQuestStatus : QuestStatus
    {
        public RetrieveQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Retrieve";
        }
    }

    public class TalkQuestStatus : QuestStatus
    {
        public TalkQuestStatus(string strPrimaryObjective)
            : base(Basic.NotStarted, strPrimaryObjective)
        {
            m_strVerb = "Talk to";
        }
    }

    public class QuestStatus
    {
        public enum Basic
        {
            NotStarted,
            Accepted,
            Completed,
            Unachievable,
            InvalidClass,
            NoFile,
            Unrepeatable,
            Invalid,
            Information,
            ManualCompleted,
            ManualNotCompleted
        }

        public const string strNoTrack = "The game does not track this goal internally; it may be manually marked as completed.";

        public class Single
        {
            public Basic State;
            public string Reason;

            public static Single Information { get { return new Single(Basic.Information); } }
            public static Single Complete { get { return new Single(Basic.Completed); } }
            public static Single NotStarted { get { return new Single(Basic.NotStarted); } }
            public static Single Incomplete { get { return new Single(Basic.Accepted); } }
            public static Single Unachievable(string reason = "") { return new Single(Basic.Unachievable, reason); }
            public static Single NoFile { get { return new Single(Basic.NoFile, "The game data file is inaccessible; please re-select it"); } }
            public static Single Failed(string reason) { return new Single(Basic.Unachievable, reason); }
            public static Single Invalid(string reason) { return new Single(Basic.Invalid, reason); }
            public static Single ManualCompleted { get { return new Single(Basic.ManualCompleted, strNoTrack); } }
            public static Single ManualNotCompleted { get { return new Single(Basic.ManualNotCompleted, strNoTrack); } }

            public bool ShowNearby { get { return State == Basic.Accepted || State == Basic.NotStarted; } }

            public static Single FromGoal(QuestGoal goal)
            {
                switch (goal)
                {
                    case QuestGoal.Complete: return Complete;
                    case QuestGoal.Incomplete: return Incomplete;
                    case QuestGoal.NotStarted: return NotStarted;
                    default: return NoFile;
                }
            }

            public Single(Basic state, string reason = "")
            {
                State = state;
                Reason = reason;
            }

            public bool IsComplete { get { return IsCompleted(State); } }
            public bool IsManual { get { return IsManualCompletion(State); } }

            public static bool IsCompleted(Basic basic)
            {
                switch (basic)
                {
                    case Basic.ManualCompleted:
                    case Basic.Completed:
                        return true;
                    default:
                        return false;
                }
            }

            public static bool IsManualCompletion(Basic basic)
            {
                switch (basic)
                {
                    case Basic.ManualCompleted:
                    case Basic.ManualNotCompleted:
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool SinglesEqual(List<Single> list1, List<Single> list2)
        {
            if (list1 == null && list2 == null)
                return true;
            if (list1 == null || list2 == null)
                return false;
            if (list1.Count != list2.Count)
                return false;

            for(int i = 0; i < list1.Count; i++)
                if (list1[i].State != list2[i].State || list1[i].Reason != list2[i].Reason)
                    return false;

            return true;
        }

        public virtual bool QuestEqual(QuestStatus qs)
        {
            if (qs == null)
                return false;

            if (!SinglesEqual(Pre, qs.Pre))
                return false;
            if (!SinglesEqual(Obj, qs.Obj))
                return false;
            if (!SinglesEqual(Post, qs.Post))
                return false;

            // Prequest changes will be caught by one of the prior loops; no need to check those again
            return true;
        }

        public static QuestGoal Or(params QuestGoal[] goals)
        {
            foreach (QuestGoal goal in goals)
            {
                if (goal == QuestGoal.BadFile)
                    return QuestGoal.BadFile;
                if (goal == QuestGoal.Complete)
                    return QuestGoal.Complete;
            }
            return QuestGoal.Incomplete;
        }

        public static QuestGoal And(params QuestGoal[] goals)
        {
            foreach (QuestGoal goal in goals)
            {
                if (goal == QuestGoal.BadFile)
                    return QuestGoal.BadFile;
                if (goal == QuestGoal.Incomplete)
                    return QuestGoal.Incomplete;
            }
            return QuestGoal.Complete;
        }

        public static QuestGoal Not(QuestGoal q)
        {
            switch (q)
            {
                case QuestGoal.Complete: return QuestGoal.Incomplete;
                case QuestGoal.Incomplete: return QuestGoal.Complete;
                default: return QuestGoal.BadFile;
            }
        }

        public static QuestGoal AndNot(QuestGoal q1, QuestGoal q2)
        {
            return And(q1, Not(q2));
        }

        public static QuestGoal OrAndNot(QuestGoal q1, QuestGoal q2, QuestGoal q3)
        {
            return Or(q1, And(q2, Not(q3)));
        }

        public List<Single> Pre = new List<Single>();
        public List<Single> Obj = new List<Single>();
        public List<Single> Post = new List<Single>();
        public List<BasicQuest> PreQuest = new List<BasicQuest>();

        public void AddPre(QuestGoal goal) { Pre.Add(Single.FromGoal(goal)); }
        public void AddPre(bool b) { Pre.Add(b ? Single.Complete : Single.Incomplete); }

        public void AddObj(params QuestGoal[] complete)
        {
            foreach (QuestGoal goal in complete)
            {
                Obj.Add(Single.FromGoal(goal));
            }
            CheckStrictProgression();
        }

        public void AddObj(params bool[] complete)
        {
            foreach (bool b in complete) 
            {
                Obj.Add(b ? Single.Complete : Single.Incomplete);
            }
            CheckStrictProgression();
        }

        public void AddObj(int num, QuestGoal complete)
        { 
            for (int i = 0; i < num; i++) 
            {
                Obj.Add(Single.FromGoal(complete));
            }
            CheckStrictProgression();
        }

        public void CheckStrictProgression()
        {
            if (!StrictProgression)
                return;

            for (int i = Obj.Count - 1; i >= 0; i--)
            {
                if (Obj[i].IsComplete)
                {
                    for (int j = i - 1; j >= 0; j--)
                        Obj[j].State = Basic.Completed;
                    return;
                }
            }
        }

        public void AddPost(bool b) { Post.Add(b ? Single.Complete : Single.Incomplete); }
        public void AddPost(QuestGoal goal) { Post.Add(Single.FromGoal(goal)); }
        public void AddPost(int num, QuestGoal complete) { for (int i = 0; i < num; i++) { Post.Add(Single.FromGoal(complete)); } }

        public void Add(QuestGoal pre, QuestGoal obj, QuestGoal post) { AddPre(pre); AddObj(obj); AddPost(post); }

        public void AddQuest(QuestGoal quest, QuestGoal pre, QuestGoal obj, QuestGoal post) {
            AddPre(Or(quest, post)); AddObj(Or(obj, AndNot(post, quest))); AddPost(AndNot(post, quest)); }
        public void AddQuest(QuestGoal quest, QuestGoal pre, QuestGoal obj) { AddPre(Or(quest, pre)); AddObj(OrAndNot(obj, pre, quest)); AddPost(AndNot(pre, quest)); }
        public void AddQuest(QuestGoal quest, QuestGoal obj) { AddPre(Or(quest, obj)); AddObj(obj); AddPost(AndNot(obj, quest)); }

        public QuestLocation FirstIncompleteObjective
        {
            get
            {
                foreach(List<QuestLocation> list in new List<QuestLocation>[] { Prerequisites, MainObjectives, Postrequisites })
                    foreach (QuestLocation loc in list)
                        if (!loc.Active.IsComplete)
                            return loc;
                return null;
            }
        }

        public string FirstIncompleteObjectiveString
        {
            get
            {
                QuestLocation loc = FirstIncompleteObjective;
                if (loc == null)
                    return String.Empty;
                return loc.Description;
            }
        }

        public void AddMonsterObj(params Point[] monsters)
        {
            foreach (Point pt in monsters)
            {
                Obj.Add(pt == Global.NullPoint ? Single.NoFile : (pt.X > 31 ? Single.Complete : Single.Incomplete));
            }
        }

        public void AddMonsterObj(QuestGoal bGate, params Point[] monsters)
        {
            foreach (Point pt in monsters)
            {
                if (bGate == QuestGoal.BadFile)
                    Obj.Add(Single.NoFile);
                else
                {
                    if (pt == Global.NullPoint)
                        Obj.Add(Single.NoFile);
                    else if (pt.X < 32)
                        Obj.Add(Single.Incomplete);
                    else if (bGate == QuestGoal.NotStarted)
                        Obj.Add(Single.NotStarted);
                    else
                        Obj.Add(Single.Complete);
                }
            }
        }

        public void AddStdQuest(QuestGoal quest, QuestGoal post, params QuestGoal[] obj)
        {
            QuestGoal finished = And(post, Not(quest));
            AddPre(Or(quest, finished));
            foreach (QuestGoal b in obj)
                AddObj(Or(b, finished));
            AddPost(finished);
        }

        public void AddMonsterQuest(QuestGoal quest, QuestGoal post, params MapXY[] monsters)
        {
            AddPre(Or(quest, post));
            foreach(MapXY map in monsters)
                AddMonsterObj(new Point(map.X, map.Y));
            LocationsOverride = monsters;
            AddPost(post);
        }

        protected string m_strVerb;
        public string PrimaryObjective;

        protected Basic m_mainState;
        public virtual Basic Main 
        { 
            get { return m_mainState; }
            set 
            { 
                m_mainState = value;
                if (QuestStatus.Single.IsCompleted(m_mainState) && MarkAllWhenComplete)
                {
                    for (int i = 0; i < Pre.Count; i++)
                        Pre[i] = Single.Complete;
                    for (int i = 0; i < Obj.Count; i++)
                        Obj[i] = Single.Complete;
                    for (int i = 0; i < Post.Count; i++)
                        Post[i] = Single.Complete;
                    for (int i = 0; i < Prerequisites.Count; i++)
                        Prerequisites[i].Active = Single.Complete;
                    for (int i = 0; i < MainObjectives.Count; i++)
                        MainObjectives[i].Active = Single.Complete;
                    for (int i = 0; i < Postrequisites.Count; i++)
                        Postrequisites[i].Active = Single.Complete;
                }

                if ( Pre.Count == 0 || StartedWhenAnyComplete)
                {
                    if (m_mainState == Basic.NotStarted && Obj.Any(o => Single.IsCompleted(o.State)))
                        m_mainState = Basic.Accepted;
                    else if (m_mainState == Basic.Accepted && !Obj.Any(o => Single.IsCompleted(o.State)))
                        m_mainState = Basic.NotStarted;
                }
            } 
        
        }
        public virtual string Verb { get { return m_strVerb; } }

        public virtual bool NotStarted { get { return m_mainState == Basic.NotStarted; } }
        public virtual bool Completed { get { return QuestStatus.Single.IsCompleted(m_mainState); } }
        public virtual bool Accepted { get { return m_mainState == Basic.Accepted; } }
        public virtual bool Unachievable { get { return m_mainState == Basic.Unachievable; } }
        public virtual bool InvalidClass { get { return m_mainState == Basic.InvalidClass; } }
        public virtual bool ValidClass { get { return m_mainState != Basic.InvalidClass; } }

        public virtual Single StatePre(int i) { return i < Pre.Count ? Pre[i] : Single.Unachievable("The internal quest state does not include any way to complete this task"); }
        public virtual Single StateMain(int i) { return i < Obj.Count ? Obj[i] : Single.Unachievable("The internal quest state does not include any way to complete this task"); }
        public virtual Single StatePost(int i) { return i < Post.Count ? Post[i] : Single.Unachievable("The internal quest state does not include any way to complete this task"); }

        public virtual bool CompletedPre() { return Pre.All(q => q.IsComplete || q.State == Basic.InvalidClass || q.State == Basic.Invalid); }
        public virtual bool CompletedMain() { return Obj.All(q => q.IsComplete || q.State == Basic.InvalidClass || q.State == Basic.Invalid); }

        public virtual bool CompletedPost()
        {
            if (Post.Count > 0)
                return !Post.Any(q => !q.IsComplete && q.State != Basic.InvalidClass);
            return CompletedMain();
        }

        public virtual bool AnyUnachievable { get { return Any(Basic.Unachievable) || All(Basic.Invalid); } }
        public virtual bool AnyNoFile { get { return Any(Basic.NoFile); } }

        public bool Any(Basic state)
        {
            foreach(List<Single> list in new List<Single>[] { Pre, Obj, Post })
                if (list.Any(q => q.State == state))
                    return true;
            return false;
        }

        public bool All(Basic state)
        {
            foreach (List<Single> list in new List<Single>[] { Pre, Obj, Post })
                if (list.Any(q => q.State != state))
                    return false;
            return true;
        }

        public string[] ItemsOverride = null;

        public MapXY[] LocationsOverride = null;

        public MapXY Location(int i)
        {
            if (LocationsOverride == null || i >= LocationsOverride.Length)
                return MapXY.Empty;
            return LocationsOverride[i];
        }

        public string Item(int i)
        {
            if (ItemsOverride == null || i >= ItemsOverride.Length)
                return "<unknown>";
            return ItemsOverride[i];
        }

        public bool ReturnToGiver = true;
        public bool MarkAllWhenComplete = false;
        public bool StartedWhenAnyComplete = false;
        public bool StrictProgression = false;      // If this is true, any completed objective also completed any prior objectives

        public List<QuestLocation> Information = new List<QuestLocation>();
        public List<QuestLocation> Prerequisites = new List<QuestLocation>();
        public List<QuestLocation> MainObjectives = new List<QuestLocation>();
        public List<QuestLocation> Postrequisites = new List<QuestLocation>();

        public virtual bool HasPrerequisiteQuests { get { return PreQuest != null && PreQuest.Count > 0; } }

        public void AddLocations(string strPre, MapXY mapPre, string strPost, MapXY mapPost, params QuestLocation[] main)
        {
            Prerequisites.Add(new QuestLocation(strPre, mapPre));
            AddLocations(main);
            Postrequisites.Add(new QuestLocation(strPost, mapPost));
        }

        public void AddLocations(string strPre, MapXY mapPre, params QuestLocation[] main)
        {
            Prerequisites.Add(new QuestLocation(strPre, mapPre));
            AddLocations(main);
        }

        public void AddLocations(params QuestLocation[] main)
        {
            foreach (QuestLocation loc in main)
                MainObjectives.Add(loc);
        }

        public void AddInformation(params QuestLocation[] info)
        {
            foreach (QuestLocation loc in info)
                Information.Add(loc);
        }

        public void AddLocations(params QuestLocation[][] main)
        {
            foreach(QuestLocation[] list in main)
                foreach (QuestLocation loc in list)
                    MainObjectives.Add(loc);
        }

        public QuestStatus(Basic state = Basic.NotStarted, string strPrimaryObjective = "Complete the Quest")
        {
            m_mainState = state;
            m_strVerb = "Complete";
            PrimaryObjective = strPrimaryObjective;
        }

        public void SetItems(params string[] items)
        {
            ItemsOverride = items;
        }

        public void SetItems(string item, int count, params string[] items)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
                list.Add(item);
            foreach (string str in items)
                list.Add(str);
            ItemsOverride = list.ToArray();
        }

        public virtual void Set(Basic state)
        {
            m_mainState = state;
        }
    }


    public class MapTitleInfo
    {
        public int Map;
        public string Title;
        public string Path;

        public MapTitleInfo(int map, string title, string path = "\\")
        {
            Map = map;
            Title = title;
            Path = path;
        }

        public MapTitleInfo(MapTitleInfo copy)
        {
            Map = copy.Map;
            Title = copy.Title;
            Path = copy.Path;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}{2}", Map, Path == "\\" ? "" : Path, Title);
        }
    }

    public class QuestLocation
    {
        public GameNames Game;
        public string Description;
        public int MapIndex;
        public Point Location;
        public int Era;
        public QuestStatus.Single Active;
        public int Priority;

        public QuestLocation(string desc, MM5Map map, int x, int y) : this(GameNames.MightAndMagic45, desc, (int)map + 256, x, y, -1, 1) { }
        public QuestLocation(string desc, MM4Map map, int x, int y) : this(GameNames.MightAndMagic45, desc, (int)map, x, y, -1, 1) { }
        public QuestLocation(string desc, MM3Map map, int x, int y) : this(GameNames.MightAndMagic3, desc, (int)map, x, y, -1, 1) { }
        public QuestLocation(string desc, MM2Map map, int x, int y) : this(GameNames.MightAndMagic2, desc, (int)map, x, y, 9, 1) { }
        public QuestLocation(string desc, MM1Map map, int x, int y) : this(GameNames.MightAndMagic1, desc, (int)map, x, y, -1, 1) { }
        public QuestLocation(string desc, MM2Map map, int x, int y, int era) : this(GameNames.MightAndMagic2, desc, (int)map, x, y, era, 1) { }
        public QuestLocation(string desc, MapXY map, int priority = 1) : this(map.Game, desc, map.Map, map.X, map.Y, map.Era, priority) { }
        public QuestLocation(string desc) : this(GameNames.None, desc, -1, -1, -1, -1, 1) { }

        public QuestLocation(GameNames game, string desc, int map, int x, int y, int era, int priority)
        {
            Game = game;
            Description = desc;
            MapIndex = map;
            Location = new Point(x, y);
            Era = era;
            Active = QuestStatus.Single.Incomplete;
            Priority = priority;
        }

        public string Path
        {
            get
            {
                return String.Format("{0}:{1},{2}:{3}", MapIndex, Location.X, Location.Y, Description);
            }
        }

        public static QuestLocation[] Multi(int iNum, MapXY map, string strBase = "Mine the vein")
        {
            QuestLocation[] locations = new QuestLocation[iNum];
            for (int i = 0; i < iNum; i++)
            {
                string strMulti = "";
                switch(i)
                {
                    case 1: strMulti = " again"; break;
                    case 2: strMulti = " a third time"; break;
                    case 3: strMulti = " a fourth time"; break;
                    case 4: strMulti = " a fifth time"; break;
                    default: break;
                }
                locations[i] = new QuestLocation(strBase + strMulti, map);
            }
            return locations;
        }

        public void SetManual(string strReason = null)
        {
            Active.State = QuestStatus.Basic.ManualNotCompleted;
            Active.Reason = String.IsNullOrWhiteSpace(strReason) ? "This goal is not tracked by the game and will not be automatically marked as completed" : strReason;
        }

        public bool HasLocation { get { return Location.X != -1 || Location.Y != -1; } }
        public bool HasMap { get { return MapIndex != -1; } }
        public string EraString { get { return Global.EraString(Era); } }
    }

    public abstract class BasicQuest
    {
        public BasicQuestType QuestType;
        public string Name;
        public string Giver;
        public string Reward;
        public QuestStatus Status = new QuestStatus();
        public QuestLocation Primary = null;
        public List<QuestLocation> Secondary = null;
        public QuestBits Bits = null;
        public int CharAddress = -1;
        public string Path = "Main";
        public int SortOrder = 0;

        protected void Init(GameNames game, BasicQuestType type, string name, string giver, string reward)
        {
            QuestType = type;
            Name = name;
            Status = new QuestStatus(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(game, String.Empty, -1, -1, -1, -1, 1);
            Secondary = new List<QuestLocation>();
            Giver = giver;
            Reward = reward;
            Path = QuestInfo.GetPath(type);
        }

        public void Init(BasicQuestType type, string name, string giver, string reward)
        {
            Init(Games.WhichGame(this), type, name, giver, reward);
        }

        public virtual string StateString
        {
            get
            {
                switch (Status.Main)
                {
                    case QuestStatus.Basic.Accepted: return "Accepted";
                    case QuestStatus.Basic.Completed:
                    case QuestStatus.Basic.ManualCompleted:
                        return "Completed";
                    case QuestStatus.Basic.InvalidClass: return "Invalid";
                    default: return "Not Started";
                }
            }
        }

        public bool IsCompleted { get { return QuestStatus.Single.IsCompleted(Status.Main); } }

        public virtual string QuestTypeString
        {
            get
            {
                return (QuestType == BasicQuestType.Primary ? "Primary" : "Side");
            }
        }

        public virtual bool IsNearby(int iMapIndex)
        {
            if (Status.Completed || Status.Unachievable || iMapIndex == -1)
                return false;

            if (Primary != null && Primary.MapIndex == iMapIndex)
                return true;

            if (Secondary == null)
                return false;

            return Secondary.Any(q => q.MapIndex == iMapIndex && q.Active.ShowNearby);
        }
    }

    public class QuestInfo
    {
        public string CharName;
        public int MapIndex;
        public int CharAddress = -1;
        public GenericClass CharClass = GenericClass.None;
        public int TotalQuests;
        public int CompletedQuests;
        public byte[] Bytes;

        public virtual bool NeedsFiles { get { return false; } }
        public virtual BasicQuest[] GetQuests() { return new BasicQuest[0]; }
        protected QuestGoal Goal(bool b) { return b ? QuestGoal.Complete : QuestGoal.Incomplete; }
        protected Point PartyLocation;
        protected bool PartyIn(params int[] rectangles) { return Global.PointInRects(PartyLocation, rectangles); }

        public virtual void SetQuests(QuestData data, int iOverrideCharAddress = -1) { }

        public virtual bool QuestsEqual(QuestInfo info)
        {
            if (info == null)
                return false;

            if (CharName != info.CharName)
                return false;

            QuestStatus[] list1 = info.GetAllQuests();
            QuestStatus[] list2 = GetAllQuests();

            if (list1.Length != list2.Length)
                return false;   // Not even the same collection of quests

            for(int i = 0; i < list1.Length; i++)
            {
                if (!list1[i].QuestEqual(list2[i]))
                    return false;
            }

            return true;
        }

        public virtual QuestStatus[] GetAllQuests() { return new QuestStatus[0]; }

        protected QuestStatus.Basic GetQuestBasic(bool bStarted, bool bCompleted, bool bCorrectClass = true)
        {
            if (!bCorrectClass)
                return QuestStatus.Basic.InvalidClass;
            if (!bStarted)
                return QuestStatus.Basic.NotStarted;
            if (bCompleted)
                return QuestStatus.Basic.Completed;
            return QuestStatus.Basic.Accepted;
        }

        protected QuestStatus.Single GetSingle(bool bStarted, bool bCompleted, bool bValid, string strReason = "")
        {
            if (!bValid)
                return QuestStatus.Single.Invalid(strReason);
            if (!bStarted)
                return QuestStatus.Single.NotStarted;
            if (bCompleted)
                return QuestStatus.Single.Complete;
            return QuestStatus.Single.Incomplete;
        }

        protected QuestStatus GetQuestState(bool bStarted, bool bCompleted, bool bCorrectClass = true)
        {
            return new QuestStatus(GetQuestBasic(bStarted, bCompleted, bCorrectClass));
        }

        protected QuestStatus AddQuest(QuestTotals totals, bool bCompleted, bool bStarted = true)
        {
            totals.All++;
            if (bCompleted)
                totals.Completed++;
            return GetQuestState(bStarted, bCompleted);
        }

        protected void AddQuest(QuestTotals totals, QuestStatus qs)
        {
            totals.All++;
            if (qs.CompletedPost())
                totals.Completed++;
            qs.Main = GetQuestBasic(qs.CompletedPre(), qs.CompletedPost());
        }

        public static string GetPath(BasicQuestType type)
        {
            return "";
        }

        protected virtual BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations) { return null;  }

        public static BasicQuest GetQuest<T>(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, string path, params QuestLocation[] locations)
            where T : BasicQuest, new()
        {
            if (locations == null || locations.Length < 1)
                return null;

            T quest = new T();
            quest.Init(type, name, giver, reward);

            if (bits is QuestBits)
                quest.Bits = bits as QuestBits;
            else
                quest.Bits = new QuestBits(bits);
            quest.Status = status;

            bool bPrimarySet = false;
            foreach (QuestLocation location in locations)
            {
                if (!bPrimarySet)
                {
                    quest.Primary = location;
                    bPrimarySet = true;
                    continue;
                }
                quest.Secondary.Add(location);
            }

            if (String.IsNullOrWhiteSpace(path))
                quest.Path = GetPath(type);
            else
                quest.Path = path;

            return quest;
        }

        public BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string strTarget, string strReward)
        {
            status.Main = QuestStatus.Basic.NotStarted;
            int iPriority = 1;
            string strGiver = strTarget;
            string strName = status.PrimaryObjective;
            status.ReturnToGiver = false;

            int iPreCompleted = 0;
            List<QuestLocation> locations = new List<QuestLocation>();
            locations.Add(new QuestLocation(status.PrimaryObjective));

            foreach (QuestLocation info in status.Information)
            {
                info.Priority = 0;
                info.Active = QuestStatus.Single.Information;
                locations.Add(info);
            }

            foreach (QuestLocation pre in status.Prerequisites)
            {
                pre.Priority = iPriority++;
                pre.Active = status.StatePre(iPreCompleted++);
                locations.Add(pre);
            }

            for (int i = 0; i < status.MainObjectives.Count; i++)
            {
                QuestLocation loc = status.MainObjectives[i];
                loc.Active = status.StateMain(i);
                loc.Priority = iPriority;
                locations.Add(loc);
            }

            int iPostCompleted = 0;

            foreach (QuestLocation post in status.Postrequisites)
            {
                post.Priority = iPriority++;
                post.Active = status.StatePost(iPostCompleted++);
                locations.Add(post);
            }

            if (status.AnyUnachievable)
                status.Main = QuestStatus.Basic.Unachievable;
            else if (status.AnyNoFile)
                status.Main = QuestStatus.Basic.NoFile;
            else if (status.CompletedPost())
                status.Main = QuestStatus.Basic.Completed;
            else if (status.CompletedPre())
                status.Main = QuestStatus.Basic.Accepted;

            return GetQuest(status, type, bits, strName, strGiver, strReward, locations.ToArray());
        }
    }

    public class MonsterPosition
    {
        public Point Position;
        public bool Highlighted;
        public List<Monster> Monsters;

        public MonsterPosition()
        {
        }

        public MonsterPosition(MonsterPosition copy)
        {
            Monsters = copy.Monsters;
            Position = copy.Position;
            Highlighted = copy.Highlighted;
        }

        public MonsterPosition(Point pt, Monster monster, bool highlight)
        {
            Position = pt;
            Highlighted = highlight;
            Monsters = new List<Monster>(1);
            Monsters.Add(monster);
        }

        public string TipString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Monster monster in Monsters)
                    sb.AppendLine(monster.OneLineDescription);
                return sb.ToString();
            }
        }
    }

    public class MonsterLocations
    {
        public byte[] RawBytes;
        public byte[] HighlightedBytes;
        public Dictionary<Point, MonsterPosition> MonsterPositions;
        public Dictionary<int, Monster> Monsters;
        public int MaxEncounterIndex = 0;
        public bool Drawn;
        public bool AlwaysShow;

        public MonsterLocations()
        {
            InitLists();
        }

        private void InitLists()
        {
            MonsterPositions = new Dictionary<Point, MonsterPosition>();
            Monsters = new Dictionary<int, Monster>();
            Drawn = false;
            AlwaysShow = false;
        }

        private MMMonster AddMM3Monster(Point pt, UInt16 index, UInt16 hp, UInt16 active, UInt16 condition, int iEncounterIndex, Point ptParty)
        {
            if (index >= MM3.Monsters.Count)
                return null;

            MM3Monster monster = MM3.Monsters[index].Clone() as MM3Monster;

            return AddMonster(monster, pt, index, hp, active, condition, iEncounterIndex, ptParty);
        }

        private MMMonster AddMM4Monster(Point pt, UInt16 index, UInt16 hp, UInt16 active, UInt16 condition, int iEncounterIndex, Point ptParty)
        {
            if (index >= MM45.MM4Monsters.Count)
                return null;

            MM45Monster monster = MM45.MM4Monsters[index].Clone() as MM45Monster;

            return AddMonster(monster, pt, index, hp, active, condition, iEncounterIndex, ptParty) as MM45Monster;
        }

        private MMMonster AddMM5Monster(Point pt, UInt16 index, UInt16 hp, UInt16 active, UInt16 condition, int iEncounterIndex, Point ptParty)
        {
            if (index >= MM45.MM5Monsters.Count)
                return null;

            MM45Monster monster = MM45.MM5Monsters[index].Clone() as MM45Monster;

            return AddMonster(monster, pt, index, hp, active, condition, iEncounterIndex, ptParty) as MM45Monster;
        }

        private MMMonster AddMonster(MM345Monster monster, Point pt, UInt16 index, UInt16 hp, UInt16 active, UInt16 condition, int iEncounterIndex, Point ptParty)
        {
            monster.CurrentHP = hp;
            monster.Position = pt;
            monster.Killed = ((pt.X & 0x00FF) > 31 || (pt.Y & 0x00FF) > 31);
            monster.EncounterIndex = iEncounterIndex;
            monster.Melee = (monster.Position == ptParty);
            monster.Active = (active != 0);
            monster.MM345Condition = (MM345MonsterCondition)condition;

            Monsters.Add(iEncounterIndex, monster);

            foreach (MonsterPosition monPos in MonsterPositions.Values)
            {
                if (monPos.Position == pt)
                {
                    monPos.Monsters.Add(monster);
                    return monster;
                }
            }
            MonsterPositions.Add(pt, new MonsterPosition(pt, monster, false));
            Drawn = false;

            return monster;
        }

        public void AddMonster(FixedMonster monster)
        {
            if (!MonsterPositions.ContainsKey(monster.Position))
                MonsterPositions.Add(monster.Position, new MonsterPosition(monster.Position, monster, false));
            else
                MonsterPositions[monster.Position].Monsters.Add(monster);

            if (!Monsters.ContainsKey(monster.EncounterIndex))
                Monsters.Add(monster.EncounterIndex, monster);
        }

        public MonsterLocations(byte[] bytesX, byte[] bytesY, byte[] bytesHP, byte[] bytesActive, byte[] bytesIndices, byte[] bytesConditions, Point ptParty)
        {
            // For MM3 monster list

            InitLists();

            MemoryStream ms = new MemoryStream(bytesX.Length + bytesY.Length + bytesHP.Length + bytesActive.Length + bytesX.Length / 2);

            ms.Write(bytesX, 0, bytesX.Length);
            ms.Write(bytesY, 0, bytesX.Length);
            ms.Write(bytesHP, 0, bytesX.Length);
            ms.Write(bytesActive, 0, bytesActive.Length);
            ms.Write(bytesConditions, 0, bytesConditions.Length);

            for (int i = 0; i < bytesX.Length; i += 2)
            {
                UInt16 x = BitConverter.ToUInt16(bytesX, i);
                UInt16 y = BitConverter.ToUInt16(bytesY, i);
                UInt16 hp = BitConverter.ToUInt16(bytesHP, i);
                UInt16 active = BitConverter.ToUInt16(bytesActive, i);
                UInt16 index = BitConverter.ToUInt16(bytesIndices, i);
                UInt16 condition = BitConverter.ToUInt16(bytesConditions, i);

                MMMonster monster = AddMM3Monster(new Point(x, y), index, hp, active, condition, i / 2, ptParty);
                if (monster != null)
                    ms.WriteByte((byte)(monster.Melee ? 1 : 0));
            }
            Drawn = false;

            MaxEncounterIndex = bytesX.Length;
            RawBytes = ms.ToArray();
        }

        public MonsterLocations(byte[] bytes, Point ptParty, MM45Side side)
        {
            // For MM4/5 monster list

            InitLists();

            MemoryStream ms = new MemoryStream(bytes.Length / 5);

            for (int i = 0; i < bytes.Length; i += 20)
            {
                if (Global.AllNull(bytes, i, 20))
                    continue;

                UInt16 x = bytes[i + MM45Memory.MonsterCurrentX];
                UInt16 y = bytes[i + MM45Memory.MonsterCurrentY];
                UInt16 hp = BitConverter.ToUInt16(bytes, i + MM45Memory.MonsterCurrentHP);
                UInt16 index = bytes[i + MM45Memory.MonsterCurrentIndex];
                UInt16 active = bytes[i + MM45Memory.MonsterActivated];
                UInt16 condition = bytes[i + MM45Memory.MonsterCondition];

                // Don't use all of the bytes for the RawBytes, because the animation indices change constantly
                ms.WriteByte(bytes[i + MM45Memory.MonsterCurrentX]);
                ms.WriteByte(bytes[i + MM45Memory.MonsterCurrentY]);
                ms.WriteByte(bytes[i + MM45Memory.MonsterActivated]);
                ms.WriteByte(bytes[i + MM45Memory.MonsterCurrentIndex]);
                ms.WriteByte(bytes[i + MM45Memory.MonsterCondition]);
                ms.Write(bytes, i + MM45Memory.MonsterCurrentHP, 2);

                MMMonster monster;
                if (side == MM45Side.Clouds)
                    monster = AddMM4Monster(new Point(x, y), index, hp, active, condition, i / 20, ptParty);
                else
                    monster = AddMM5Monster(new Point(x, y), index, hp, active, condition, i / 20, ptParty);

                if (monster != null)
                    ms.WriteByte((byte)(monster.Melee ? 1 : 0));
            }
            Drawn = false;

            MaxEncounterIndex = bytes.Length / 20;
            RawBytes = ms.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MonsterPosition pos in MonsterPositions.Values)
                sb.AppendFormat("{0},{1}:{2} ", pos.Position.X, pos.Position.Y, pos.Monsters.Count);
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }

    public class ActiveSquareInfo : IComparable<ActiveSquareInfo>
    {
        public Point Location;
        public bool Active;

        public ActiveSquareInfo(Point pt, bool active)
        {
            Location = pt;
            Active = active;
        }

        public int CompareTo(ActiveSquareInfo info)
        {
            if (info.Location != Location)
            {
                if (Location.Y == info.Location.Y)
                    return Math.Sign(Location.X - info.Location.X);
                return Math.Sign(Location.Y - info.Location.Y);
            }

            if (info.Active != Active)
                return Active ? 1 : -1;

            return 0;
        }

        public override string ToString()
        {
            return String.Format("{0},{1}:{2}", Location.X, Location.Y, Active ? "Active" : "Inactive");
        }
    }

    public class ActiveSquares
    {
        private bool m_bDrawn;

        public bool AllInactive = false;

        public MainForm Main { get; set; }

        public bool Drawn 
        {
            get { return m_bDrawn; }
            set { m_bDrawn = value; }
        }

        public int MapIndex { get { return m_iMapIndex; } }

        public bool Changed { get; set; }
        public byte[] RawBytes;
        public Size MapSize;
        protected bool m_bInitialized = false;
        protected int m_iMapIndex = -1;
        protected Dictionary<Point, ActiveSquareInfo> m_activeSquares;
        public int NumActive { get { return m_activeSquares.Count(s => s.Value.Active); } }

        public virtual bool IsActiveInternal(int x, int y, bool bEncountersOnly)
        {
            if (AllInactive)
                return false;

            Point pt = new Point(x, y);
            if (Main != null)
                pt = Main.TranslateToGameMap(pt);

            return IsActive(pt.X, pt.Y, bEncountersOnly);
        }

        public static ActiveSquares CreateAllInactive
        {
            get
            {
                ActiveSquares inactive = new ActiveSquares();
                inactive.AllInactive = true;
                return inactive;
            }
        }

        protected EncounterData GlobalEncounterData
        {
            get
            {
                if (Main == null)
                    return null;

                switch (Main.Game)
                {
                    case GameNames.MightAndMagic1: return MM1.Encounters;
                    case GameNames.MightAndMagic2: return MM2.Encounters;
                    case GameNames.Wizardry1:
                    case GameNames.Wizardry2:
                    case GameNames.Wizardry3:
                    case GameNames.Wizardry4:
                    case GameNames.Wizardry5:
                        return Games.GetWizGlobals(Main.Game).GetEncounters();
                    default: return null;
                }
            }
        }

        public virtual bool IsActive(int x, int y, bool bEncountersOnly)
        {
            if (AllInactive)
                return false;

            if (x < 0 || x > 15 || y < 0 || y > 15)
                return false;
            int offset = y * 16 + x;
            if (offset < 0 || offset >= RawBytes.Length)
                return false;
            bool bActive = ((RawBytes[offset] & 0x80) > 0);
            if (!bActive)
                return false;
            if (!bEncountersOnly)
                return true;

            EncounterData ed = GlobalEncounterData;
            if (ed == null)
                return true;

            return ed.IsMonsterEncounter(m_iMapIndex, new Point(x, y));
        }

        public virtual Dictionary<Point, ActiveSquareInfo> GetActiveSquares()
        {
            if (m_bInitialized && m_activeSquares != null)
                return m_activeSquares;

            Initialize();

            return m_activeSquares;
        }

        protected virtual void Initialize()
        {
            m_activeSquares = new Dictionary<Point, ActiveSquareInfo>();

            if (RawBytes == null || RawBytes.Length < 256 || AllInactive)
                return;

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Point pt = new Point(x, y);
                    m_activeSquares.Add(pt, new ActiveSquareInfo(pt, (RawBytes[y * 16 + x] & 0x80) > 0));
                }
            }

            m_bInitialized = true;
        }

        public virtual Dictionary<Point, ActiveSquareInfo> GetInternalMapDelta(ActiveSquares compare)
        {
            if (compare != null && compare.BytesEqual(this))
                return null;

            if (!m_bInitialized)
                Initialize();

            if (m_activeSquares == null)
                return null;

            Dictionary<Point, ActiveSquareInfo> delta = new Dictionary<Point, ActiveSquareInfo>();

            if (compare == null)
            {
                foreach (ActiveSquareInfo square in m_activeSquares.Values)
                {
                    Point pt = Main.TranslateToInternalMap(square.Location);
                    if (!delta.ContainsKey(pt))
                        delta.Add(pt, square);
                }
                return delta;
            }

            Dictionary<Point, ActiveSquareInfo> dict = compare.GetActiveSquares();
            if (dict == null)
                return null;

            foreach (KeyValuePair<Point, ActiveSquareInfo> pair in dict)
            {
                if (m_activeSquares.ContainsKey(pair.Key) && m_activeSquares[pair.Key].CompareTo(pair.Value) != 0)
                    delta.Add(Main.TranslateToInternalMap(pair.Key), pair.Value);
            }
            foreach (KeyValuePair<Point, ActiveSquareInfo> pair in m_activeSquares)
            {
                if (!dict.ContainsKey(pair.Key))
                    delta.Add(Main.TranslateToInternalMap(pair.Key), pair.Value);
            }

            return delta;
        }

        public virtual bool BytesEqual(byte[] bytes)
        {
            return Global.Compare(bytes, RawBytes);
        }

        public virtual bool BytesEqual(ActiveSquares active)
        {
            if (active == null)
                return RawBytes == null;
            return Global.Compare(RawBytes, active.RawBytes);
        }

        public ActiveSquares()
        {
            Main = null;
            Drawn = false;
            Changed = false;
            AllInactive = false;
            RawBytes = null;
        }
    }

    public class AmountExplored
    {
        public int Explored;
        public int Total;

        public AmountExplored(int explored, int total)
        {
            Explored = explored;
            Total = total;
        }

        public int Unexplored { get { return Total - Explored; } }
        public double Percent { get { return Total == 0 ? 100.0 : Explored * 100 / (double)Total; } }
    }

    public class QuickRefInfo
    {
        public List<Character> Characters;

        public class Character
        {
            public string Name;
            public GenericClass Class;
            public int Level;
            public int HP;
            public int MaxHP;
            public int SP;
            public int MaxSP;
            public int AC;
            public int Might;
            public int Intellect;
            public int Personality;
            public int Speed;
            public int Accuracy;
            public int Luck;
            public BasicDamage Melee;
            public BasicDamage Ranged;
            public string Condition;

            public Character()
            {
            }

            public void AddBytes(Stream stream)
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                stream.Write(ascii.GetBytes(Name), 0, Name.Length);
                stream.WriteByte((byte)Class);
                foreach (int i in new int[] { Level, HP, MaxHP, SP, MaxSP, AC, Might, Intellect, Personality, Speed, Accuracy, Luck })
                    Global.WriteInt32(stream, i);
                Melee.AddBytes(stream);
                Ranged.AddBytes(stream);
                stream.Write(ascii.GetBytes(Condition), 0, Condition.Length);
            }
        }

        public QuickRefInfo()
        {
            Characters = new List<Character>();
        }

        public byte[] GetBytes()
        {
            MemoryStream stream = new MemoryStream();

            foreach (Character c in Characters)
                c.AddBytes(stream);

            return stream.ToArray();
        }
    }

    public abstract class QuestData
    {
        public PartyInfo Party;
        public LocationInformation Location;
        public MapData Map;
        public GameInfo Info;
        public GameState State;
        public EncounterInfo Encounter;

        public virtual void AddBytes(Stream stream)
        {
            if (Party != null)
                Global.WriteBytes(stream, Party.QuestBytes);

            if (Location != null)
                Global.WriteBytes(stream, Location.QuestBytes);

            if (Map != null)
                Global.WriteInt32(stream, Map.Index);

            if (Info != null)
                Global.WriteBytes(stream, Info.QuestBytes);

            // Changes in GameState typically do not translate to changes that need to be tracked here

            if (Encounter != null)
                Global.WriteBytes(stream, Encounter.QuestBytes);
        }

        public virtual byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            AddBytes(ms);
            return ms.ToArray();
        }
    }

    public class MapBasicInfo
    {
        public int Index;
        public Size Dimensions;

        public MapBasicInfo(int index, Size sz)
        {
            Index = index;
            Dimensions = sz;
        }

        public virtual int ByteCount { get { return Dimensions.Height * Dimensions.Width; } }
    }

    public abstract class TrapInfo
    {
        public int Trap = 0;

        public TrapInfo()
        {
        }

        public virtual int TrapCount { get { return 0; } }
        public virtual string GetTrapName(int trap) { return "Unknown"; }

        public string[] GetAllTrapNames()
        {
            string[] names = new string[TrapCount];
            for (int i = 0; i < TrapCount; i++)
                names[i] = GetTrapName(i);
            return names;
        }
    }

    public class TrapTag
    {
        public TrapInfo Treasure;

        public TrapTag(TrapInfo treasure)
        {
            Treasure = treasure;
        }

        public int Trap { get { return Treasure.Trap; } }

        public override string ToString() { return Treasure.GetTrapName(Trap); }
    }

    public delegate MapTitleInfo MapTitlePairDelegate(int index);

    public abstract class MemoryHacker : IDisposable
    {
        public enum BackpackType
        {
            None,
            UnequippedOnly,
            EquippedOnly,
            All
        }

        protected RosterFile m_roster;
        private IntPtr m_handleGame = IntPtr.Zero;
        protected Process m_processGame = null;
        protected MemoryBlock m_block = null;
        protected GameNames m_game = GameNames.None;
        protected const uint m_bufferSize = 65536;
        protected uint m_offsetFoundBlock = 0;
        protected System.Timers.Timer m_timerProcessWatchdog = new System.Timers.Timer(Properties.Settings.Default.WatchdogTimer);
        protected bool m_bInitSuccess = false;
        private IntPtr m_hwndDOSBox = IntPtr.Zero;
        private List<MemoryBlock> m_blocks;
        private byte[] m_bufferBytes;
        public bool NeedsReinitialize = false;
        private int m_iReacquireCount = 0;
        private DateTime m_dtLastReacquire = DateTime.MinValue;
        private GameNames m_gameFound = GameNames.None;
        protected GameState m_gsCurrent;
        protected MainState m_lastMainState = MainState.Unknown;
        protected int m_lastActingChar = -1;
        protected byte[] m_bytesLastLiveMap = null;
        public virtual bool UsesAlignment { get { return true; } }
        public virtual int GetLightDistance(Point ptLocation) { return 4; }
        public virtual bool CartographySupportsSeen { get { return false; } }

        public virtual string[] GetCharacterNames()
        {
            List<BaseCharacter> chars = GetCharacters();
            if (chars == null)
                return new string[0];
            string[] result = new string[chars.Count];
            for (int i = 0; i < chars.Count; i++)
                result[i] = chars[i].Name;
            return result;
        }

        public GameNames GameFound { get { return m_gameFound; } }
        public bool HasReinitializedHandler { get { return Reinitialized != null; } }

        public delegate void ReinitializedEventHandler(object sender, EventArgs e);
        public event ReinitializedEventHandler Reinitialized;

        protected virtual void OnReinitialized(EventArgs e)
        {
            if (Reinitialized != null)
                Reinitialized(this, e);

            GameGlobals globals = Games.GetGlobals(Game);
            if (globals != null)
            {
                globals.InitItemList(this);
                globals.InitMonsterList(this);
            }
        }

        public MemoryBytes ReadDirect(long address, int count)
        {
            byte[] bytes = new byte[count];
            ReadDirect(address, bytes);
            return new MemoryBytes(bytes, address);
        }

        public bool ReadDirect(long address, byte[] bytes)
        {
            if (m_block == null || bytes == null)
                return false;
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)address), bytes, (ulong)bytes.Length, out pRead);
            return (pRead == bytes.Length);
        }

        public MemoryBytes ReadOffset(long offset, int length)
        {
            byte[] bytes = new byte[length];
            if (!ReadOffset(offset, bytes))
                return null;

            return new MemoryBytes(bytes, offset);
        }

        public bool ReadOffset(long offset, byte[] bytes)
        {
            if (m_block == null)
                return false;
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong) (m_offsetFoundBlock + offset)), bytes, (ulong) bytes.Length, out pRead);
            return (pRead == bytes.Length);
        }

        public string ReadString(long offset, int iMaxLength = 16)
        {
            if (m_block == null)
                return String.Empty;

            byte[] bytes = new byte[iMaxLength + 1];
            bytes[iMaxLength] = 0;
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length - 1, out pRead);
            if (pRead != bytes.Length - 1)
                return String.Empty;

            string strResult = Encoding.ASCII.GetString(bytes, 0, iMaxLength);
            int iNull = strResult.IndexOf('\0');
            if (iNull == -1)
                return strResult;
            return strResult.Substring(0, iNull);
        }

        public UInt16 ReadUInt16(long offset)
        {
            if (m_block == null)
                return 0;

            byte[] bytes = new byte[2];
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pRead);
            if (pRead != bytes.Length)
                return 0;

            return BitConverter.ToUInt16(bytes, 0);
        }

        public Int16 ReadInt16(long offset)
        {
            if (m_block == null)
                return 0;

            byte[] bytes = new byte[2];
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pRead);
            if (pRead != bytes.Length)
                return 0;

            return BitConverter.ToInt16(bytes, 0);
        }

        public UInt32 ReadUInt32(long offset)
        {
            if (m_block == null)
                return 0;

            byte[] bytes = new byte[4];
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pRead);
            if (pRead != bytes.Length)
                return 0;

            return BitConverter.ToUInt32(bytes, 0);
        }

        public byte ReadByte(long offset)
        {
            if (m_block == null)
                return 0;

            byte[] bytes = new byte[1];
            long pRead;
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pRead);
            if (pRead != bytes.Length)
                return 0;

            return bytes[0];
        }

        public bool ReadOffset(long offset, byte[] bytes, uint length, out long iRead)
        {
            if (m_block == null)
            {
                iRead = 0;
                return false;
            }
            NativeMethods.ReadProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, length, out iRead);
            return (iRead == bytes.Length);
        }

        public bool WriteOffset(MemoryBytes mb)
        {
            return WriteOffset(mb.Offset, mb.Bytes);
        }

        public bool WriteDirect(MemoryBytes mb)
        {
            return WriteDirect(mb.Offset - m_offsetFoundBlock, mb.Bytes);
        }

        public bool WriteDirect(long address, byte[] bytes)
        {
            if (m_block == null || bytes == null)
                return false;
            long pWritten;
            NativeMethods.WriteProcessMemory(m_handleGame, m_block.Offset((ulong)address), bytes, (ulong)bytes.Length, out pWritten);
            return (pWritten == bytes.Length);
        }

        public bool WriteByte(long offset, byte b)
        {
            if (m_block == null)
                return false;
            byte[] bytes = new byte[] { b };
            long pWritten;
            NativeMethods.WriteProcessMemory(m_handleGame, m_block.Offset((ulong) (m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pWritten);
            return (pWritten == bytes.Length);
        }

        public bool WriteOffset(long offset, byte[] bytes)
        {
            if (m_block == null || bytes == null)
                return false;
            long pWritten;
            NativeMethods.WriteProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)bytes.Length, out pWritten);
            return (pWritten == bytes.Length);
        }

        public bool WriteOffset(long offset, byte[] bytes, int length)
        {
            if (m_block == null || bytes == null)
                return false;
            long pWritten;
            NativeMethods.WriteProcessMemory(m_handleGame, m_block.Offset((ulong)(m_offsetFoundBlock + offset)), bytes, (ulong)length, out pWritten);
            return (pWritten == bytes.Length);
        }

        public bool WriteUInt16(long offset, UInt16 i)
        {
            if (m_block == null)
                return false;
            return WriteOffset(offset, BitConverter.GetBytes(i));
        }

        public bool WriteInt16(long offset, Int16 i)
        {
            if (m_block == null)
                return false;
            return WriteOffset(offset, BitConverter.GetBytes(i));
        }

        public bool WriteOffset(long offset, byte[] bytes, long length, out long pWritten)
        {
            if (m_block == null || bytes == null)
            {
                pWritten = 0;
                return false;
            }
            NativeMethods.WriteProcessMemory(m_handleGame, m_block.Offset((ulong) (m_offsetFoundBlock + offset)), bytes, (ulong) length, out pWritten);
            return (pWritten == length);
        }

        public bool WriteLoadedFileOffset(long pointer, long offset, byte[] bytes)
        {
            int ptr = ReadUInt16(pointer) * 16 + 32;
            return WriteOffset(-FoundBlockOffset + ptr + offset, bytes);
        }

        public string GetStatModString(int value, PrimaryStat stat)
        {
            int iMod = GetStatModifier(value, stat).Value;
            if (iMod < 0)
                return iMod.ToString();
            return "+" + iMod.ToString();
        }

        // For orders other than MIPESAL (e.g. MM1 has IMPESAL)
        public virtual PrimaryStat[] StatOrder { get { return StatOrderMIPESAL; } }

        public virtual int[] StatMinimums(GenericClass charClass)
        {
            switch (charClass)
            {
                case GenericClass.Knight: return new int[] {0, 12, 0, 0, 0, 0, 0};
                case GenericClass.Paladin: return new int[] { 0, 12, 12, 12, 0, 0, 0 };
                case GenericClass.Archer: return new int[] { 12, 0, 0, 0, 0, 12, 0 };
                case GenericClass.Sorcerer: return new int[] { 12, 0, 0, 0, 0, 0, 0 };
                case GenericClass.Cleric: return new int[] { 0, 0, 12, 0, 0, 0, 0 };
                case GenericClass.Robber: return new int[] { 0, 0, 0, 0, 0, 0, 0 };
                default: return null;
            }
        }

        public long GetBaseAddress()
        {
            if (m_block == null)
                return 0;
            return (long) m_block.Start;
        }

        public bool IsValid
        {
            get
            {
                try
                {
                    if (m_processGame == null)
                        return false;

                    if (m_processGame.HasExited)
                        return false;

                    if (m_handleGame == IntPtr.Zero)
                        return false;

                    if (m_block == null)
                        return false;

                    if ((long)m_block.Start < MinimumBlockOffset)
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        private DateTime m_dtLastDOSBoxCheck = DateTime.MinValue;

        public IntPtr DOSBoxWindow
        {
            set { m_hwndDOSBox = value; }
            get
            {
                if (m_hwndDOSBox != IntPtr.Zero)
                    if (NativeMethods.IsWindow(m_hwndDOSBox))
                        return m_hwndDOSBox;

                if ((DateTime.Now - m_dtLastDOSBoxCheck).TotalMilliseconds < 1000)
                    return IntPtr.Zero;     // Don't spam WM_GETTEXT messages all over the system with the WindowFinder

                m_dtLastDOSBoxCheck = DateTime.Now;

                m_hwndDOSBox = FindDOSBoxWindow();
                return m_hwndDOSBox;
            }
        }

        public void FocusDOSBox()
        {
            if (DOSBoxWindow != IntPtr.Zero)
            {
                NativeMethods.SetForegroundWindow(DOSBoxWindow);
            }
        }

        public bool IsGameFocused
        {
            get { return (NativeMethods.GetForegroundWindow() == DOSBoxWindow); }
        }

        public Rectangle GetDOSBoxRect()
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return Rectangle.Empty;

            return NativeMethods.GetWindowRect(DOSBoxWindow);
        }

        public static Rectangle GetDOSBoxRectStatic()
        {
            IntPtr hwnd = FindDOSBoxWindow();
            if (hwnd == IntPtr.Zero)
                return Rectangle.Empty;

            return NativeMethods.GetExtendedWindowRect(hwnd);
        }

        public static IntPtr FindDOSBoxWindow()
        {
            WindowFinder finder = new WindowFinder();
            IntPtr[] hWnds = finder.FindWindowByRexeg(Properties.Settings.Default.DOSBoxClass, Properties.Settings.Default.DOSBoxCaption);
            if (hWnds.Length == 0)
                return IntPtr.Zero;

            return hWnds[0];
        }

        public void SetDOSBoxPosition(Point pt)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            Rectangle rcScreen = SystemInformation.VirtualScreen;
            rcScreen.Inflate(16, 16);   // To include possible hidden window borders
            if (!rcScreen.Contains(pt))
                return;
            NativeMethods.SetExtendedWindowPosition(DOSBoxWindow, pt);
        }

        public void SetDOSBoxPosition(Rectangle rc)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            if (!SystemInformation.VirtualScreen.Contains(rc))
                return;
            NativeMethods.SetExtendedWindowPosition(DOSBoxWindow, rc);
        }

        public bool SendKeysToDOSBox(Keys[] keys, bool bSendModifiersUp = false) { return SendKeysToDOSBox(0, keys, bSendModifiersUp); }

        public bool SendKeysToDOSBox(int iSleepBetween, Keys[] keys, bool bSendModifiersUp = false, Keys keySurround = Keys.None)
        {
            if (DOSBoxWindow == IntPtr.Zero || keys == null)
                return false;

            DOSBoxVersion version = GetDOSBoxVersion();

            if (version == DOSBoxVersion.Classic074)
            {
                // Classic DOSBox does not accept keys unless it has the focus, and requires SendInput rather than PostMessage
                FocusDOSBox();

                List<NativeMethods.INPUT> list = new List<NativeMethods.INPUT>(keys.Length * 2);

                System.Threading.Thread.Sleep(50);  // If the SendInput is too soon, DOSBox doesn't register the keys

                if (bSendModifiersUp)
                    NativeMethods.SendInput((uint)Global.ModifierKeysUp.Length, Global.ModifierKeysUp, NativeMethods.INPUT.Size);

                bool bSleepBetween = true;

                int iKey = 0;
                while (bSleepBetween)
                {
                    if (iSleepBetween == 0)
                    {
                        // Send all of the keys at once;
                        if (keySurround != Keys.None)
                            list.Add(NativeMethods.CreateKeyInput(keySurround, true));

                        for (int i = 0; i < keys.Length; i++)
                        {
                            list.Add(NativeMethods.CreateKeyInput(keys[i], true));
                            list.Add(NativeMethods.CreateKeyInput(keys[i], false));
                        }
                        bSleepBetween = false;
                    }
                    else
                    {
                        list.Clear();

                        if (keySurround != Keys.None)
                            list.Add(NativeMethods.CreateKeyInput(keySurround, true));

                        list.Add(NativeMethods.CreateKeyInput(keys[iKey], true));
                        list.Add(NativeMethods.CreateKeyInput(keys[iKey], false));
                        iKey++;
                        if (iKey >= keys.Length)
                            bSleepBetween = false;
                    }

                    if (keySurround != Keys.None)
                        list.Add(NativeMethods.CreateKeyInput(keySurround, false));

                    //Global.Log("Sending: {0}", Global.InputString(list.ToArray()));
                    NativeMethods.SendInput((uint)list.Count, list.ToArray(), NativeMethods.INPUT.Size);

                    System.Threading.Thread.Sleep(iSleepBetween);
                }
            }
            else
            {
                if (bSendModifiersUp)
                {
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)Keys.ControlKey), NativeMethods.MakeLParam(1, 0));
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)Keys.ShiftKey), NativeMethods.MakeLParam(1, 0));
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)Keys.Menu), NativeMethods.MakeLParam(1, 0));
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)Keys.LWin), NativeMethods.MakeLParam(1, 0));
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)Keys.RWin), NativeMethods.MakeLParam(1, 0));
                }

                if (keySurround != Keys.None)
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYDOWN, new IntPtr((int) keySurround), NativeMethods.MakeLParam(1, 0));

                foreach (Keys key in keys)
                {
                    if (key == Keys.None)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {
                        IntPtr ipKey = new IntPtr((int)key);
                        NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYDOWN, ipKey, NativeMethods.MakeLParam(1, 0));
                        NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, ipKey, NativeMethods.MakeLParam(1, (ushort)0xc000));
                    }
                    System.Threading.Thread.Sleep(iSleepBetween);
                }

                if (keySurround != Keys.None)
                    NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int)keySurround), NativeMethods.MakeLParam(1, (ushort)0xc000));
            }

            return true;
        }

        public void SendInputsToDOSBox(List<NativeMethods.INPUT> list, bool bSendModifiersUp = false)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            DOSBoxVersion version = GetDOSBoxVersion();

            if (version == DOSBoxVersion.Classic074)
            {
                // Classic DOSBox does not accept keys unless it has the focus, and requires SendInput rather than PostMessage
                FocusDOSBox();

                System.Threading.Thread.Sleep(50);  // If the SendInput is too soon, DOSBox doesn't register the keys

                if (bSendModifiersUp)
                    NativeMethods.SendInput((uint)Global.ModifierKeysUp.Length, Global.ModifierKeysUp, NativeMethods.INPUT.Size);

                NativeMethods.SendInput((uint)list.Count, list.ToArray(), NativeMethods.INPUT.Size);
            }
            else
            {
                foreach (NativeMethods.INPUT input in list)
                {
                    IntPtr ipKey = new IntPtr((int)input.U.ki.wVk);
                    if (input.U.ki.dwFlags == NativeMethods.KEYEVENTF_KEYUP)
                        NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, ipKey, NativeMethods.MakeLParam(1, (ushort)0xc000));
                    else
                        NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYDOWN, ipKey, NativeMethods.MakeLParam(1, 0));
                    System.Threading.Thread.Sleep(5);
                }
            }
        }

        public void SendStringToDOSBox(string str, bool bSendModifiersUp = false)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            List<NativeMethods.INPUT> list = new List<NativeMethods.INPUT>(str.Length * 2);

            for (int i = 0; i < str.Length; i++)
                NativeMethods.AddKeysForChar(list, str[i]);

            SendInputsToDOSBox(list, bSendModifiersUp);
        }

        public void SendKeyUpToDOSBox(Keys[] keys)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            foreach (Keys key in keys)
            {
                NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYUP, new IntPtr((int) key), NativeMethods.MakeLParam(1, (ushort)0xc000));
            }
        }

        public void SendKeyDownToDOSBox(Keys[] keys)
        {
            if (DOSBoxWindow == IntPtr.Zero)
                return;

            foreach (Keys key in keys)
            {
                NativeMethods.PostMessage(DOSBoxWindow, NativeMethods.WM_KEYDOWN, new IntPtr((int)key), NativeMethods.MakeLParam(1, 0));
            }
        }

        public MemoryHacker()
        {
            m_game = GameNames.None;
            m_bInitSuccess = false;
            m_timerProcessWatchdog.Elapsed += m_timerProcessWatchdog_Elapsed;
            m_timerProcessWatchdog.Start();
        }

        public void CheckKnownGames()
        {
            m_gameFound = MemoryHacker.FindKnownGame();
        }

        public static GameNames FindKnownGame()
        {
            Process[] aProcesses = GetDOSBoxProcesses();
            if (aProcesses == null)
                return GameNames.None;

            foreach (Process proc in aProcesses)
            {
                foreach (GameNames game in Games.ImplementedGames)
                {
                    if (game == GameNames.DOSBox)
                        continue;   // Not really a game
                    if (Regex.IsMatch(proc.MainWindowTitle, Games.CurrentTitle(game)))
                        return game;
                }
            }

            return GameNames.None;
        }

        void m_timerProcessWatchdog_Elapsed(object sender, ElapsedEventArgs e)
        {
            if ((m_handleGame != IntPtr.Zero && m_processGame.HasExited) || m_handleGame == IntPtr.Zero || NeedsReinitialize)
            {
                NeedsReinitialize = false;
                if (!Init())
                    CheckKnownGames();

                if (!IsValid)
                    return;
                Global.LogWarning("Game process re-acquired");
                m_gameFound = Game;

                if ((DateTime.Now - m_dtLastReacquire).TotalSeconds > 30)
                    m_iReacquireCount = 0;
                m_dtLastReacquire = DateTime.Now;
                m_iReacquireCount++;
                if (m_iReacquireCount == 20 && Properties.Settings.Default.WarnMultipleReacquire)
                {
                    Global.InternalError("The game process has been re-acquired 20 times in the past 60 seconds\r\n\r\n" +
                        "this is an indication that something in the game memory does not match what is expected (e.g. the internal monster list or other ostensibly static data).  " + 
                        "You may disable this warning in the Options dialog but be aware that this the program may not read DOSBox memory as expected.");
                }
                OnReinitialized(new EventArgs());
                if (Running && Properties.Settings.Default.DOSBoxPosition != Global.NullPoint && Properties.Settings.Default.DOSBoxPosition != Global.NullPoint32000)
                    SetDOSBoxPosition(Properties.Settings.Default.DOSBoxPosition);
            }
        }

        public int FindBytes(byte[] source, int length, byte[] search, int start)
        {
            if (length > source.Length)
                length = source.Length;

            int iSearchRange = length - search.Length;
            int iSearchLength = search.Length;

            for (int i = start; i < iSearchRange; i++)
            {
                for (int j = 0; j < search.Length; j++)
                {
                    if (source[i + j] != search[j])
                        break;

                    if (j == search.Length - 1)
                        return i;
                }
            }
            return -1;
        }

        public bool Running
        {
            get
            {
                return (m_handleGame != IntPtr.Zero && !m_processGame.HasExited && m_bInitSuccess);
            }
        }

        public void Stop()
        {
            if (m_handleGame != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(m_handleGame);
                m_handleGame = IntPtr.Zero;
            }
            m_timerProcessWatchdog.Stop();
        }

        public void Start()
        {
            m_timerProcessWatchdog.Start();
        }

        public bool Init()
        {
            m_bInitSuccess = false;

            Process proc = GetGameProcess();

            if (proc == null)
                return false;

            m_processGame = proc;

            NativeMethods.ProcessAccessType access =
                  NativeMethods.ProcessAccessType.PROCESS_VM_READ
                | NativeMethods.ProcessAccessType.PROCESS_QUERY_INFORMATION
                | NativeMethods.ProcessAccessType.PROCESS_VM_WRITE
                | NativeMethods.ProcessAccessType.PROCESS_VM_OPERATION;

            m_handleGame = NativeMethods.OpenProcess((uint)access, 1, (uint)proc.Id);

            long MaxAddress = Int64.MaxValue;
            long address = 0;
            m_blocks = new List<MemoryBlock>();
            do
            {
                NativeMethods.MEMORY_BASIC_INFORMATION64 m;
                int result = NativeMethods.QueryMemory(m_handleGame, (ulong) address, out m, (uint)Marshal.SizeOf(typeof(NativeMethods.MEMORY_BASIC_INFORMATION64)));
                if (result == 0)
                    break;  // VirtualQueryEx error
                if (m.State == NativeMethods.MemoryState.MEM_COMMIT && (uint) m.RegionSize > 640 * 1024)   // Not looking for blocks under 640KB
                    m_blocks.Add(new MemoryBlock((ulong)m.BaseAddress, (ulong)m.RegionSize));

                if (address >= m.BaseAddress + m.RegionSize)
                    break;
                address = m.BaseAddress + m.RegionSize;
            } while (address <= MaxAddress);

            byte[] byteSearch = MainSearch;
            MemoryGuess[] baseGuesses = Guesses;

            // Check +/- 32 bytes and +/- 368 of each guess too; some of the SVN builds seem to be arbitrarily off by that many bytes
            List<MemoryGuess> guesses = new List<MemoryGuess>(baseGuesses.Length * 7);
            for (int i = 0; i < baseGuesses.Length; i++)
            {
                guesses.Add(baseGuesses[i]);
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index - 32));
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index + 32));
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index - 96));
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index + 96));
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index - 368));
                guesses.Add(new MemoryGuess(baseGuesses[i].BlockLength, baseGuesses[i].Index + 368));
            }

            m_bufferBytes = new byte[m_bufferSize];
            byte[] bytesGuess = new byte[byteSearch.Length];
            long pRead;

            foreach (MemoryBlock block in m_blocks.ToArray())   // Make a copy of the list in case it is modified during the loop
            {
                if (block == null)
                    continue;
                foreach (MemoryGuess guess in guesses)
                {
                    if (guess.BlockLength <= block.Length)
                    {
                        NativeMethods.ReadProcessMemory(m_handleGame, block.Offset(guess.Index), bytesGuess, (ulong) byteSearch.Length, out pRead);
                        if ((int)pRead == byteSearch.Length)
                        {
                            if (Global.Compare(bytesGuess, byteSearch))
                            {
                                m_block = block;
                                m_offsetFoundBlock = guess.Index;
                                OnReinitialized(new EventArgs());
                                m_bInitSuccess = true;
                                return true;
                            }
                        }
                    }
                }
            }

            if (!Properties.Settings.Default.QuickScanDOSBox)
            {
                Global.LogWarning("Could not locate search string in DOSBox process quickly; trying full scan...");

                MemoryBlock[] foundValues = ScanAllBlocks(byteSearch, 1);

                if (foundValues.Length > 0)
                {
                    m_block = foundValues[0];
                    m_offsetFoundBlock = (uint)foundValues[0].Found;
                    OnReinitialized(new EventArgs());
                    m_bInitSuccess = true;
                    return true;
                }
            }
            else
                Global.LogWarning("Could not locate search string in DOSBox process quickly and full scan is disabled in the options; giving up!");

            return false;
        }

        public MemoryBlock[] ScanAllBlocks(byte[] byteSearch, int iMaxResults)
        {
            int iResults = 0;
            List<MemoryBlock> blocksFound = new List<MemoryBlock>();
            long pRead;
            // If we couldn't find the block with the guesses, then scan all of process memory
            foreach (MemoryBlock block in m_blocks)
            {
                int iSearchOffset = 0;
                while (iSearchOffset < (long) block.Length)
                {
                    NativeMethods.ReadProcessMemory(m_handleGame, block.Offset((ulong) iSearchOffset), m_bufferBytes, block.Length - (ulong) iSearchOffset > m_bufferSize ? m_bufferSize : block.Length - (ulong) iSearchOffset, out pRead);

                    int iFind = 0;
                    while (iFind != -1)
                    {
                        iFind = FindBytes(m_bufferBytes, (int)pRead, byteSearch, iFind);
                        if (iFind != -1)
                        {
                            blocksFound.Add(new MemoryBlock(block.Start, block.Length, (ulong) (iSearchOffset + iFind)));
                            iFind += byteSearch.Length;
                            iResults++;
                            if (iResults >= iMaxResults)
                                return blocksFound.ToArray();
                        }
                    }

                    if (iSearchOffset >= (long) block.Length)
                        break;

                    iSearchOffset += ((int) m_bufferSize - byteSearch.Length);
                }
            }

            return blocksFound.ToArray();
        }

        public ItemBag ReadBagFromRoster()
        {
            ItemBag bag = new ItemBag();
            int iCharacter = MaxInventoryChar;    // last character in the roster

            do
            {
                iCharacter = FindNextInventoryChar(iCharacter - 1, InventoryCharAction.FindExisting);

                if (iCharacter < 0)
                    break;

                List<Item> backpack = GetBackpackFromRoster(iCharacter);
                if (backpack == null)
                    break;
                foreach (Item item in backpack)
                {
                    if (item.Index != 0)
                        bag.Items.Add(item);
                }
            } while (iCharacter >= 0);

            bag.Game = m_game;

            return bag;
        }

        public int GetMaxBagItems()
        {
            int iCharacter = MaxInventoryChar - 1;    // last character in the roster
            int iCharactersAvailable = 0;

            while (iCharacter >= 0)
            {
                iCharacter = FindNextInventoryChar(iCharacter, InventoryCharAction.FindPotential);
                if (iCharacter >= 0)
                    iCharactersAvailable++;
                iCharacter--;
            }

            return iCharactersAvailable * MaxBackpackSize;
        }

        public virtual bool PrepareToSaveBag(int iMaxItems) { return true; }

        public virtual int StoreBagInRoster(ItemBag bag)
        {
            if (!PrepareToSaveBag(bag.Items.Count))
            {
                MessageBox.Show("The roster data could not be initialized for this game.  Please ensure that all character data in the game directory can be read and written properly.",
                    "Error preparing roster for Bag of Holding", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return -1;
            }

            int iCharacter = MaxInventoryChar - 1;    // last character in the roster
            int iItemsStored = 0;

            // Create an inventory character if one doesn't exist
            iCharacter = FindNextInventoryChar(iCharacter, InventoryCharAction.FindOrCreate);

            if (iCharacter == -1)
                return iItemsStored;   // Couldn't save everything; not enough empty character slots

            List<Item> backpack = new List<Item>(12);

            SetBackpackInRoster(iCharacter, backpack);

            foreach (Item item in bag.Items)
            {
                if (backpack.Count >= MaxBackpackSize)
                {
                    // This character's inventory is full; write it out
                    if (SetBackpackInRoster(iCharacter, backpack) == SetBackpackResult.Success)
                        iItemsStored += backpack.Count;
                    else
                        return iItemsStored;    // Error saving one of the backpacks

                    backpack.Clear();

                    // Create an inventory character if one doesn't exist
                    iCharacter = FindNextInventoryChar(iCharacter - 1, InventoryCharAction.FindOrCreate);

                    if (iCharacter == -1)
                        return iItemsStored;   // Couldn't save everything; not enough empty character slots
                }

                backpack.Add(item);
            }

            if ((backpack.Count > 0) && (SetBackpackInRoster(iCharacter, backpack) == SetBackpackResult.Success))
                iItemsStored += backpack.Count;

            // Any remaining Inventory characters' backpacks must be cleared
            backpack.Clear();
            iCharacter--;
            while (iCharacter >= 0)
            {
                iCharacter = FindNextInventoryChar(iCharacter, InventoryCharAction.FindExisting);
                SetBackpackInRoster(iCharacter, backpack);
                iCharacter--;
            }

            return iItemsStored;
        }

        public virtual GameReadyState GameReady
        {
            get
            {
                if (!IsValid)
                    return GameReadyState.NotReady;

                GameState info = GetGameState() as GameState;
                if (info == null)
                    return GameReadyState.NotReady;

                switch (info.Main)
                {
                    case MainState.LoadingMap:
                        return GameReadyState.NeedDelay;
                    case MainState.Unknown:
                        return GameReadyState.NotReady;
                    default:
                        return GameReadyState.Ready;
                }
            }
        }

        public virtual DirectionsTo GetDirections(int iMap, Point ptLocation, bool bNorthIncreases = true, bool bEastIncreases = true)
        {
            LocationInformation li = GetLocation();
            int iEast = ptLocation.X - li.PrimaryCoordinates.X;
            int iNorth = ptLocation.Y - li.PrimaryCoordinates.Y;

            if (!bNorthIncreases)
                iNorth = -iNorth;
            if (!bEastIncreases)
                iEast = -iEast;

            if (li.MapIndex == iMap)
            {
                // Same map -> simple calculation
                return new DirectionsTo(iNorth, iEast);
            }

            // If we aren't on surface maps, the directions are "impossible"
            // (i.e. it requires more than just n/s/e/w travel to get there)

            if (!SameSurfaceMaps(iMap, li.MapIndex))
                return DirectionsTo.Impossible; // For the moment this means that the maps are on opposite sides of Xeen

            if (!(IsSurface(iMap) && IsSurface(li.MapIndex)))
                return DirectionsTo.Impossible; // Not both surface maps

            Point ptSurfaceTarget = GetSurfaceSector(iMap);
            Point ptSurfaceParty = GetSurfaceSector(li.MapIndex);

            return new DirectionsTo((ptSurfaceTarget.Y - ptSurfaceParty.Y) * 16 * (bNorthIncreases ? 1 : -1) + iNorth,
                (ptSurfaceTarget.X - ptSurfaceParty.X) * 16 * (bEastIncreases ? 1 : -1) + iEast);
        }

        public DOSBoxVersion GetDOSBoxVersion() 
        {
            if (m_processGame == null)
                return DOSBoxVersion.Unknown;

            if (NativeMethods.Is64Bit(m_processGame.Handle) && !Environment.Is64BitProcess)
            {
                // if there is a 32/64 bit discrepancy; as of the moment only the SVN-Daum has a 64-bit build
                return DOSBoxVersion.SVNDaum;
            }

            try
            {
                if (m_processGame == null || m_processGame.HasExited)
                    return DOSBoxVersion.NotRunning;

                FileVersionInfo info = m_processGame.MainModule.FileVersionInfo;
                if (info == null)
                    return DOSBoxVersion.Unknown;

                if (info.Comments[0] == '©')
                    return DOSBoxVersion.Classic074;
            }
            catch (Exception)
            {
                return DOSBoxVersion.Unknown;
            }

            return DOSBoxVersion.SVNDaum;
        }

        public virtual bool SetGameInfoItem(GameInfoItem item)
        {
            if (!IsValid)
                return false;

            if (item == null || item.Offsets == null || item.Offsets.Length == 0)
                return false;

            if (item.Mask == 0)
            {
                MemoryBytes[] mb = item.GetBytes();
                if (mb == null)
                    return false;
                bool bSuccess = true;
                for (int i = 0; i < mb.Length; i++)
                    bSuccess = bSuccess && WriteOffset(item.Offsets.Offsets[i], mb[i].Bytes);
                return bSuccess;
            }

            byte[] bytes = new byte[4];
            if (!ReadOffset(item.Offsets.First, bytes))
                return false;

            UInt32 newValue = 0;
            if (item.Value is bool)
                newValue = ((bool)item.Value) ? 0xFFFFFFFF : 0;
            else
                newValue = Convert.ToUInt32(item.Value);

            UInt32 flags = BitConverter.ToUInt32(bytes, 0);
            UInt32 newFlags = (UInt32)((flags | (newValue & item.Mask)) & (newValue | ~item.Mask));
            bytes = BitConverter.GetBytes(newFlags);
            return WriteOffset(item.Offsets.First, bytes);
        }

        public virtual ScriptInfo GetScriptInfo() { return GetScriptInfo(GetScriptBytes()); }
        public virtual ScriptInfo GetScriptInfo(MemoryBytes scriptBytes) { return null; }

        public virtual bool SetBackpacks(List<BaseCharacter> characters, List<Item>[] items, bool bRemoveMovedItems)
        {
            SetBackpackResult result = SetBackpackResult.Success;

            for (int i = 0; i < items.Length; i++)
            {
                if (i < characters.Count)
                {
                    SetBackpackResult res = SetBackpack(characters[i].BasicAddress, items[i]);
                    if (res == SetBackpackResult.Success && bRemoveMovedItems)
                        items[i].Clear();
                    else if (result == SetBackpackResult.Success && res != SetBackpackResult.Success)
                        result = res;
                }
            }

            return (result == SetBackpackResult.Success);
        }

        public virtual bool RefreshConditions()
        {
            SendKeysToDOSBox(new Keys[] { Keys.D1, Keys.Escape }, true);
            return true;
        }

        public GameNames Game { get { return m_game; } }

        public static Process[] GetDOSBoxProcesses(string strProcessName = null)
        {
            if (strProcessName == null)
                strProcessName = Properties.Settings.Default.DosBoxProcessName;

            string[] names = new string[] { strProcessName, strProcessName + "_debug", strProcessName + "_x64" };

            Process[] aProcesses = null;
            foreach (string strName in names)
            {
                aProcesses = Process.GetProcessesByName(strName);
                if (aProcesses.Length > 0)
                    break;
            }
            if (aProcesses.Length < 1)
                aProcesses = Process.GetProcessesByName(Properties.Settings.Default.DosBoxProcessName2);

            if (aProcesses.Length < 1)
                return null;

            return aProcesses;
        }

        public static Process GetProcess(string strTitleMatch, string strProcessName = null)
        {
            Process[] aProcesses = GetDOSBoxProcesses(strProcessName);
            if (aProcesses == null)
                return null;

            foreach (Process proc in aProcesses)
            {
                if (Regex.IsMatch(proc.MainWindowTitle, strTitleMatch))
                    return proc;
            }
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
                if (m_timerProcessWatchdog != null)
                    m_timerProcessWatchdog.Dispose();
            }
        }

        ~MemoryHacker()
        {
            Dispose(false);
        }

        public static PrimaryStat[] StatOrderMIPESAL = new PrimaryStat[]
        { 
            PrimaryStat.Might,
            PrimaryStat.Intellect,
            PrimaryStat.Personality,
            PrimaryStat.Endurance,
            PrimaryStat.Speed,
            PrimaryStat.Accuracy,
            PrimaryStat.Luck
        };

        public static PrimaryStat[] StatOrderIMPESAL = new PrimaryStat[]
        { 
            PrimaryStat.Intellect,
            PrimaryStat.Might,
            PrimaryStat.Personality,
            PrimaryStat.Endurance,
            PrimaryStat.Speed,
            PrimaryStat.Accuracy,
            PrimaryStat.Luck
        };

        public static PrimaryStat[] StatOrderSIPVAL = new PrimaryStat[]
        { 
            PrimaryStat.Strength,
            PrimaryStat.IQ,
            PrimaryStat.Piety,
            PrimaryStat.Vitality,
            PrimaryStat.Agility,
            PrimaryStat.Luck
        };

        public static PrimaryStat[] StatOrderSIDCL = new PrimaryStat[]
        { 
            PrimaryStat.Strength,
            PrimaryStat.IQ,
            PrimaryStat.Dexterity,
            PrimaryStat.Constitution,
            PrimaryStat.Luck
        };

        public bool InSpots(MapXY location, MapXY[] spots)
        {
            if (location == null || spots == null)
                return false;
            foreach (MapXY spot in spots)
                if (location.Equals(spot))
                    return true;
            return false;
        }

        public virtual bool MoveItem(Item item, BaseCharacter charFrom, BaseCharacter charTo)
        {
            List<Item> itemsFrom = charFrom.BasicInventory.SelectUnequippedItems;

            if (!itemsFrom.Contains(item))
            {
                // If the backpack has been re-read (e.g. after a combat), the object itself may be
                // a different refernence, but if the backpack has exactly the same item in it, that's
                // almost undoubtedly what was intended.
                foreach (Item itemFrom in itemsFrom)
                {
                    if (Global.Compare(itemFrom.Serialize(), item.Serialize()))
                    {
                        item = itemFrom;
                        break;
                    }
                }
                if (!itemsFrom.Contains(item))
                    return false;
            }

            itemsFrom.Remove(item);
            List<Item> itemsTo = charTo.BasicInventory.SelectUnequippedItems;
            itemsTo.Add(item);

            if (SetBackpack(charTo.BasicAddress, itemsTo) != SetBackpackResult.Success)
                return false;

            return SetBackpack(charFrom.BasicAddress, itemsFrom) == SetBackpackResult.Success;
        }

        public void TweakSleep(int iMilliseconds)
        {
            Thread.Sleep(iMilliseconds * 100 / Properties.Settings.Default.SkipIntroTimingTweak);
        }

        public virtual bool UpdateLiveSquares(MapBook book, MapSheet sheet, List<Point> coordinates)
        {
            MapBytes mb = GetCurrentMapBytes();
            if (mb == null || mb.Bytes == null)
                return false;

            if (Global.Compare(mb.Bytes, m_bytesLastLiveMap))
                return false; // No updates since the last check

            m_bytesLastLiveMap = mb.Bytes;

            LocationInformation location = GetLocation();

            MapData data = CreateLiveMapData(mb);
            if (data == null)
                return false;

            data.Index = location.MapIndex;

            foreach (Point pt in coordinates)
            {
                MapSquare square = sheet.GetSquareAtGridPoint(pt);
                if (!square.Flags.HasFlag(MapSquareFlags.Live))
                    continue;

                Point ptGame = book.TranslateLocationFromMap(pt, sheet);

                // "Live" in this case just applies to the walls, not the rest of the square
                sheet.ReinterpretSquare(pt, ptGame, data, book);
            }

            // This check is in a separate look because the nearby squares need to be set first
            foreach (Point pt in coordinates)
                sheet.CheckUnimportantWalls(book, pt);

            return true;
        }

        public virtual bool ValidateRosterFile()
        {
            // Always reload the roster file, in case the player deleted characters or otherwise putzed with the file
            m_roster = CreateRoster(true);
            if (m_roster == null || !m_roster.Valid)
                BrowseRosterFile();

            return m_roster != null && m_roster.Valid;
        }

        public virtual bool BrowseRosterFile(string strTitle = null)
        {
            if (String.IsNullOrWhiteSpace(DefaultRosterFileName))
                return false;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = Global.CombineRoster(Game);
            ofd.InitialDirectory = Games.RosterPath(Game);
            ofd.Filter = RosterFilter + "All Files|*.*";
            if (String.IsNullOrWhiteSpace(strTitle))
                ofd.Title = "You must select your " + DefaultRosterFileName + " file in order to use the Bag of Holding";
            else
                ofd.Title = strTitle;
            while (true)
            {
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return false;

                m_roster = CreateRoster(ofd.FileName, false);
                if (m_roster != null && m_roster.Valid)
                {
                    Games.SetRosterFile(Game, Path.GetFileName(m_roster.FileName));
                    Games.SetRosterPath(Game, Path.GetDirectoryName(m_roster.FileName));
                    break;
                }
            }
            return true;
        }

        public virtual RosterFile CreateRoster(string strFile, bool bSilent) { return null; }
        public virtual string RosterFilter { get { return String.Format("{0}|{1}|", Games.ShortName(Game), DefaultRosterFileName); } }
        public virtual string DefaultRosterFileName { get { return ""; } }
        public virtual uint FoundBlockOffset { get { return m_offsetFoundBlock; } }
        public virtual Process GetGameProcess() { return GetProcess(Properties.Settings.Default.GameTitles.Get(Game, String.Empty)); }
        public virtual TrainingInfo GetTrainingInfo() { return new TrainingInfo(); }
        public virtual bool SetCharCreationInfo(CharCreationInfo info) { return false; }
        public virtual bool SetTrainingInfo(TrainingInfo info) { return false; }
        public virtual CharCreationInfo GetCharCreationInfo() { return null; }
        public virtual PartyInfo GetPartyInfo() { return null; }
        public virtual void SetCureAllInfo(CureAllInfo info, int iCasterAddress, int[] partyAddresses) { }
        public virtual CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses) { return null; }
        public virtual EncounterInfo GetEncounterInfo(bool bForceNew = false) { return null; }
        public virtual SpellInfo GetSpellInfo() { return null; }
        public virtual LocationInformation GetLocation() { return GetGameState().Location; }
        public virtual bool SetLocation(Point ptLocation) { return false; }
        public virtual bool SetBeacon(Point ptLocation, int iMap) { return false; }
        public virtual byte[] MainSearch { get { return null; } }
        public virtual MemoryGuess[] Guesses { get { return new MemoryGuess[0]; } }
        public virtual StatModifier GetStatModifier(int value, PrimaryStat stat) { return StatModifier.Zero; }
        public virtual void RefreshRollScreen() { }
        public virtual string StatToolTip(int iIndex, int iValue) { return String.Empty; }
        public virtual CureAllResult CureAll(CureAllInfo info) { return CureAllResult.Error; }
        public virtual string GetRaceDescription(GenericRace race) { return "N/A"; }
        public virtual string GetClassDescription(GenericClass gc) { return "N/A"; }
        public virtual MapData GetMapData(bool bIncludeStrings, int iMapIndex) { return null; }
        public virtual String GetGameTime(bool bFull) { return String.Empty; }
        public virtual long GetGameTimeLong() { return -1; }
        public virtual bool SetCharacterBytes(int iAddress, byte[] bytes) { return false; }
        public virtual bool SetMonsterInfo(int iIndex, MonsterBasicInfo info) { return false; }
        public virtual MonsterBasicInfo GetMonsterInfo(int iIndex) { return null; }
        public virtual string GetMapStrings(bool bRaw = false) { return "<none>"; }
        public virtual bool TradeBackpacks(int iCharAddress1, int iCharAddress2) { return false; }
        public virtual SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false) { return SetBackpackResult.NotImplemented; }
        public virtual List<Item> GetBackpack(int iCharAddress) { return null; }
        public virtual int MinimumBlockOffset { get { return 0; } }
        public virtual string GetDebugMemoryInfo() { return "N/A"; }
        public virtual string ReplaceNoteStrings(string str) { return str; }
        public virtual GameInfo GetGameInfo() { return null; }
        public virtual GameInfo GetGameInfo(GameInfo infoOld) { return GetGameInfo(); }
        public virtual bool HasBeacon { get { return false; } }
        public virtual bool HasSurfaceLocation { get { return false; } }
        public virtual MapAttributes GetMapAttributes() { return null; }
        public virtual QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false) { return null; }
        public virtual bool SetQuestBits(int iAddress, QuestBits bits, bool bSet) { return false; }
        public virtual GameState GetGameState() { return null; }
        public virtual int MaxInventoryChar { get { return 0; } }
        protected virtual int FindNextInventoryChar(int iStart, InventoryCharAction action) { return -1; }
        public virtual List<Item> GetBackpackFromRoster(int iRosterPosition) { return GetBackpack(iRosterPosition); }
        public virtual SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items) { return SetBackpack(iRosterPosition, items); }
        public virtual List<MapTitleInfo> GetMapTitles() { return null; }
        public virtual MapTitleInfo GetMapTitle(int index) { return null; }
        public virtual bool SetExit(MMExit exit) { return false; }
        public virtual bool SetMapAttributeFlags(MapAttributeFlags flags) { return false; }
        public virtual bool SetDepth(byte bDepth) { return false; }
        public virtual bool SetMapAttributes(int index, byte[] bytes) { return false; }
        public virtual bool SetActiveEffect(MMEffectTag effect) { return false; }
        public virtual bool SetOutside(Point pt, bool bOutside) { return false; }
        public virtual bool ToggleOutside() { return false; }
        public virtual bool SetMonsterGroup(byte bGroup) { return false; }
        public virtual bool SetEncounterSize(byte bSize) { return false; }
        public virtual void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly) { }
        public virtual MonsterLocations GetMonsterLocations(bool bForceNew = false) { return null; }
        public virtual int MaxBackpackSize { get { return 6; } }
        public virtual bool SetReadySpell(SpellHotkey hk) { return false; }
        public virtual bool SetReadySpell(int iChar, int iSpell) { return false; }
        public virtual bool SetReadySpells(int iSpell, SpellType type) { return false; }
        public virtual Shops GetShopInfo() { return null; }
        public virtual bool SetShopItem(ShopItem item) { return false; }
        public virtual bool KillAllMonsters() { return false; }
        public virtual bool ResetMonsters() { return false; }
        public virtual RosterFile CreateRoster(bool bSilent) { return null; }
        public virtual bool PointInMap(Point pt) { return new Rectangle(new Point(0, 0), GetCurrentMapDimensions()).Contains(pt); }
        public virtual string BagOfHoldingRequirement { get { return "at an Inn"; } }
        public virtual MapCartography GetCartography() { return null; }
        public virtual string[] CurrentDataFiles { get { return null; } set { } }
        public virtual bool EditMapCartography(MapCartography.EditAction action) { return false; }
        public virtual ActiveSquares GetActiveSquares(MainForm form, bool bForce = false) { return null; }
        public virtual byte[] GetMonsterBytes() { return null; }
        public virtual List<ScriptString> GetScriptStrings() { return new List<ScriptString>(0); }
        public virtual List<ScriptString> GetScriptStrings(MemoryBytes mb) { return new List<ScriptString>(0); }
        public virtual List<int> GetMonsterIndices() { return new List<int>(0); }
        public virtual bool SetScriptLine(ScriptLine line) { return false; }
        public virtual int ScriptCommandOffset { get { return 1; } }
        public virtual string GetEncounterNoteText(string strPrefix, byte[] bytesCommand) { return strPrefix + Global.ByteString(bytesCommand); }
        public virtual AmountExplored GetExplored() { return new AmountExplored(0, 0); }
        public virtual bool HasScripts { get { return false; } }
        public virtual bool BagNeedsRosterFile { get { return true; } }
        public virtual bool SpellsUseLevelOnly { get { return false; } }
        public virtual bool SpellsHaveDuration { get { return false; } }
        public virtual bool SetMonster(Monster monster) { return false; }
        public virtual int GetCurrentMapIndex() { return -1; }
        public virtual int GetCurrentMapQuad() { return 0; }
        public virtual MemoryBytes GetScriptBytes() { return null; }
        public virtual GameScripts GetScripts(MemoryBytes bytes) { return null; }
        public virtual GameScripts GetScripts() { return GetScripts(GetScriptBytes()); }
        public virtual bool CreateSuperCharacter(int iAddress) { return false; }
        public virtual Size GetCurrentMapDimensions() { return new Size(16, 16); }
        public virtual Size GetMapDimensions(int iIndex) { return GetCurrentMapDimensions(); }
        public virtual MapBasicInfo GetCurrentMapInfo() { int iIndex = GetCurrentMapIndex(); return new MapBasicInfo(iIndex, GetMapDimensions(iIndex)); }
        public virtual RosterFile CurrentRoster { get { return null; } }
        public virtual bool HasParty { get { return true; } }
        public virtual Subscreen GetSubscreen() { return Subscreen.Unknown; }
        public virtual string CurrentMapEnum { get { return GetMapEnum(GetCurrentMapIndex()); } }
        public virtual string GetMapEnum(int index) { return "Unknown"; }
        public virtual List<BaseCharacter> GetCharacters() { return null; }
        public virtual byte[] OffsetSearchBytes { get { return null; } }
        public virtual void SelectGameFiles() { }
        public virtual int CorrectMapIndex(int iMap) { return iMap; }
        public virtual int GetMonsterListIndex() { return 0; }
        public virtual GameInformationControl CreateGameInfoControl(IMain main) { return null; }
        public virtual Point GetPartyPosition() { return Global.NullPoint; }
        public virtual string ModifierText(PrimaryStat stat) { return String.Empty; }
        public virtual int GetBlessValue() { return 0; }
        public virtual MMMonster GetDefaultMonster(int iIndex, int iSide = 0) { return null; }
        public virtual bool IsExplored(int x, int y) { return true; }
        public virtual bool ToggleCartography(Point pt, Toggle toggle) { return false; }
        public virtual bool HasCartography { get { return false; } }
        public virtual bool HasCartographyOnMap(int iMap = -1) { return HasCartography; }
        public virtual string RosterFile { get { return Games.RosterFile(Game); } }
        public virtual string RosterPath { get { return Games.RosterPath(Game); } }
        public virtual string GamePath { get { return Properties.Settings.Default.AutoLaunchShortcuts.Get(Game); } }
        public virtual bool ActiveMonstersNearby { get { return GetGameState().InCombat; } }
        public virtual bool IsScriptBitMonster(int iMonster, int iMapIndex = -1) { return false; }
        public virtual bool LoadingMap { get { return false; } }
        public virtual bool NeedsMovementDelay { get { return false; } }
        public virtual Point GetSurfaceSector(int iMap) { return new Point(-1, -1); }
        public virtual bool IsSurface(int iMap) { return false; }
        public virtual bool IsDungeon(int iMap) { return !IsSurface(iMap); }
        public virtual bool SameSurfaceMaps(int iMap1, int iMap2) { return true; }
        public virtual string SpellType1 { get { return "Cleric"; } }
        public virtual string SpellType2 { get { return "Mage"; } }
        public virtual string SpellType3 { get { return "Druid"; } }
        public virtual string SpellType4 { get { return "Wizard"; } }
        public virtual string SpellType5 { get { return "Bard"; } }
        public virtual string SpellType6 { get { return "Archmage"; } }
        public virtual string SpellType7 { get { return "Chronomancer"; } }
        public virtual string SpellType8 { get { return "Geomancer"; } }
        public virtual string SpellType9 { get { return "Misc"; } }
        public virtual bool SetEncounterInfo(EncounterInfo info) { return false; }
        public virtual CreationAssistantControl CreateCreationAssistantControl(IMain main) { return new MMCreationAssistantControl(main); }
        public virtual TrainingAssistantControl CreateTrainingAssistantControl(IMain main) { return new MM1TrainingAssistantControl(main); }
        public virtual bool SkipIntroductions(int iTimeout = 5000) { return false; }
        public virtual string PleaseFormPartyString { get { return "Please form a party and exit the inn."; } }
        public virtual TrapsControl GetTrapsControl(IMain main) { return null; }
        public virtual IEnumerable<Monster> GetMonsterList() { return null; }
        public virtual bool AutoCombat() { return false; }
        public virtual MapBytes GetCurrentMapBytes() { return null; }
        public virtual MapData CreateLiveMapData(MapBytes bytes) { return null; }
        public void ClearLiveData() { m_bytesLastLiveMap = null; }
        public virtual int[] CreationStatWeights { get { return new int[19] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 6, 10, 15, 21 }; } }
        public virtual int DieMax { get { return 6; } }
        public virtual int IntroTimeout { get { return 5000; } }
        public virtual bool HasRoamingMonsters { get { return false; } }
        public virtual bool QuestsNeedFiles { get { return false; } }
        public virtual bool SeparateInventoryTypes { get { return false; } }
        public virtual bool UsesTrainingAssistant { get { return true; } }
        public static MapTitleInfo DefaultMapTitlePairFunction(int map) { return new MapTitleInfo(map, String.Format("Map {0}", map)); }
        public virtual string ShopWindowTitle { get { return null; } }
        public virtual int CharacterSize { get { return -1; } }
        public virtual int CharacterMemorySize { get { return CharacterSize; } }
        public virtual BaseCharacter CreateCharFromBytes(byte[] bytes) { return null; }
        public virtual byte[] GetInventoryCharBytes() { return null; }
        public virtual bool PartyInfoChanged(GameInfo info1, GameInfo info2) { return false; }
        public virtual string RaiseStatChance { get { return "The chance to raise a sub-18 stat is approximately 36%\r\n+1.9%\tEach year under 21\r\n-1.5%\tEach year over 67"; } }
        public virtual string LowerStatChance { get { return "The chance to lower a stat that is at the maximum value (18) is approximately 6.6%\r\n-0.3%\tEach year under 21\r\n+0.3%\tEach year over 67"; } }
        public virtual string LowerMaxStatChance { get { return "The chance to lower a stat that is not the maximum value (18) is approximately 39.4%\r\n-1.9%\tEach year under 21\r\n+1.5%\tEach year over 67"; } }
        public virtual bool VisitFacingSquare(BasicLocation location, MapSheet sheet) { return false; }
        public virtual Modifiers GetExternalModifiers(BaseCharacter baseChar) {  return null; }
        public virtual byte[] GetMarchingOrder() { return new byte[] { 0, 1, 2, 3, 4, 5 }; }
        public virtual int DelayBetweenSpellKeys { get { return 0; } }
        public virtual StatsPerLevel GetStatsPerLevel(GenericClass gc) { return null; }
        public virtual TrapInfo CreateTrapInfo(int iTrap) { return null; }
        public virtual GenericClass GetCharacterClass(int iCharAddress) { return GenericClass.None; }
        public virtual bool CartographyCanUnvisitSquares { get { return true; } }
        public virtual bool CartographyEditableGlobally { get { return HasCartography; } }
        public virtual int GetActingCharacterAddress() { GameState gs = GetGameState(); return gs == null ? 0 : gs.ActingCharAddress; }
        public virtual bool AlternateGameVersion { get { return false; } }
    }
}
