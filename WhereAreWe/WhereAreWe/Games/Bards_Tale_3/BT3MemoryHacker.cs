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
    public class BT3Memory : BTMemory
    {
        // Search for "refugee camp"
        public override byte[] MainSearch { get { return new byte[] { 0x72, 0x65, 0x66, 0x75, 0x67, 0x65, 0x65, 0x20, 0x63, 0x61, 0x6D, 0x70 }; } }

        public override int MainBlockSVN { get { return -213457; } }
        public override int MainBlockOldSVN { get { return -213089; } }
        public override int MainBlockNonSVN { get { return -213457; } }

        public override int Map { get { return -81073; } }
        public override int LocationNorth { get { return 50923; } }
        public override int LocationEast { get { return 50925; } }
        public override int LocationNorthTown { get { return 50923; } }
        public override int LocationEastTown { get { return 50925; } }
        public override int Facing { get { return 50927; } }
        public override int FacingTown { get { return 50927; } }
        public override int PartyNames { get { return -2287; } }
        public override int PartyInfo { get { return -2287; } }
        public override int Stack { get { return 59647; } }
        public override int NumChars { get { return 59403; } }
        public override int MainMapIndex { get { return 50919; } }
        public override int SubMapIndex { get { return 50921; } }
        public override int SurfaceMapIndex { get { return 50917; } }
        public override int MonsterHP { get { return -789; } }
        public override int MonsterIndices { get { return -1071; } }
        public override int GameTimeHours { get { return 51017; } }
        public override int GameTimeSeconds { get { return 51158; } }
        public override int SongDuration { get { return 51019; } }
        public override int LightDistance { get { return 51018; } }
        public override int SpellIcon1 { get { return 8103; } }
        public override int SpellIcon2 { get { return 8104; } }
        public override int SpellIcon3 { get { return 8105; } }
        public override int SpellIcon4 { get { return 8106; } }
        public override int SpellIcon5 { get { return 8107; } }
        public override int LevitationDuration { get { return 51024; } }
        public override int ShieldDuration { get { return 51023; } }
        public override int LightDuration { get { return 51020; } }
        public override int DetectionDuration { get { return 51022; } }
        public override int CompassDuration { get { return 51021; } }
        public override int CombatSong { get { return -849; } }
        public override int PartyCombatACBonus { get { return -803; } }
        public override int PartyCombatMagicResist { get { return -2321; } }
        public override int PartyBonusAttacks { get { return -8641; } }
        public override int CharCombatDamageBonus { get { return -2303; } }
        public override int CharCombatDamageBonus2 { get { return -2329; } }
        public override int EnemyACBonus { get { return -1281; } }
        public override int EnemyDamageBonus { get { return -1237; } }
        public override int State1 { get { return -857; } }
        public override int State3 { get { return -15781; } }
        public override int State4 { get { return 46067; } }
        public override int CreationStats { get { return 59257; } }
        public override int CreationRace { get { return -1405; } }
        public override int PartyCombatOptions { get { return -2317; } }
        public override int PartyCombatSubOptions1 { get { return -1325; } }
        public override int PartyCombatSubOptions2 { get { return -1085; } }
        public override int PartyCombatSelectedSpells { get { return -1275; } }
        public override int CombatActingChar1 { get { return 59071; } }
        public override int CombatActingChar2 { get { return 59083; } }
        public override int CombatActingChar3 { get { return 59045; } }
        public override int StateArray { get { return -15809; } }
        public override int AdvPartyACBonus { get { return 51034; } }
        public override int TrapType { get { return -2307; } }
        public override int EncounterMonstersKilled { get { return -2293; } }
        public override int TreasureState { get { return 91345; } }
        public override int MapTreasureIndex { get { return 51028; } }

        // Unfinished
        public override int TownMap { get { return 33323; } }
        public override int MarchingOrder { get { return -12845; } }
        public override int ItemList { get { return 15687; } }
        public override int ItemACBonus { get { return 16199; } }
        public override int ItemTypes { get { return 16455; } }
        public override int ItemUsableBy { get { return 16727; } }
        public override int ItemEffects { get { return 17239; } }
        public override int ItemDamage { get { return 15943; } }
        public override int ItemValues { get { return 515; } }
        public override int ItemCharges { get { return 15687; } }
        public override int ItemEquipEffect { get { return 16983; } }
        public override int MapSpecials { get { return -81493; } }
        public override int MapCustomSquares { get { return -80341; } }
        public override int MapFixedSquares { get { return -80373; } }
        public override int MapTeleport { get { return -80405; } }
        public override int MapSpecials2 { get { return -80981; } }
        public override int MonsterNumAlive { get { return 32925; } }
        public override int MonsterDistances { get { return 33305; } }
        public override int AdventuringSong { get { return -262; } }

        public override int EnemyLoseTurn { get { return 33189; } }
        public override int MapStrings { get { return 33323; } }
        public override int MapSquareStrings { get { return 33323; } }
        public override int CastingChar { get { return 37230; } }
        public override int PartyPerishSeconds { get { return 33975; } }
        public override int Counter1 { get { return 50953; } }
        public override int NumItemsInShop { get { return 39317; } }
        public override int ShopInventory { get { return 39323; } }
        public override int TreasureRanges { get { return -2725; } }
        public override int TreasureMinimums { get { return -2709; } }
        public override int MapGoldMax { get { return -2595; } }
        public override int ForcedEncounters { get { return -80373; } }
        public override int CampInspectingChar { get { return 39487; } }

        public override int CharCombatACBonus { get { return -803; } }
        public override int InspectingChar { get { return 54724; } }
        public override int ShopInspectingChar { get { return 54700; } }
        public override int ShoppingChar { get { return 54702; } }
        public override int AskCastSpell { get { return -74; } }
        public override int AskWhichSpell { get { return 54681; } }
        public override int AskWhichSpellCombat { get { return 54567; } }
        public override int AskWhichSpellCombat2 { get { return 54589; } }
        public override int AskWhichSong { get { return 54679; } }
        public override int SummonedCreature { get { return 51410; } }
        public override int TrapExamined { get { return 46924; } }
        public override int MonsterGroup { get { return 4692; } }
        public override int MonsterAC { get { return -1281; } }
        public override int MonsterDamage { get { return -1237; } }
        public override int MonsterExp { get { return 5076; } }
        public override int ScreenText { get { return 50740; } }
        public override int SwapWallsDoors { get { return 20921; } }
        public override int ImageCaption { get { return -1012; } }

        public override int EncounterInfo { get { return 0; } }

        public override int State2 { get { return 54670; } }
        public override int StackAddressIndicator { get { return 54758; } }
        public override int StackSize { get { return 0x600; } }
        public override int CombatActiveSpells { get { return -867; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.BardsTale3]; } }

        // Bard's Tale 3 specific
        public int Wilderness { get { return -81010; } }
        public int CurrentMonsters { get { return -29065; } }
        public int MapSize { get { return -81057; } }
        public int MapSizeDungeons { get { return -81043; } }
        public int ScriptBits { get { return 50937; } }
        public int MapFlags { get { return 51027; } }
        public int CreationStatsAlt { get { return 59349; } }
        public int CreationHP { get { return -1414; } }
        public int SelectedSpellSurface { get { return 58569; } }
        public int SelectedSpellSurfaceCombat { get { return 58247; } }
        public int SelectedSpellDungeon { get { return 58605; } }
        public int SelectedSpellDungeonCombat { get { return 58283; } }
        public int StackOffsetIndex { get { return -206915; } }
        public int CastingCharSurface { get { return 59393; } }
        public int CastingCharDungeon { get { return 59425; } }
        public int MapLoadingStatus { get { return -206932; } }

        public BT3StackStates StackStates;
        public static int[] CommonStackLocationsBC000000 = new int[] { 114, 140, 152, 188, 390, 438, 610, 636, 642, 648, 684, 698, 712, 730, 766, 846, 872, 884, 904, 918, 920, 930, 932,
            940, 942, 946, 958, 960, 964, 966, 968, 976, 978, 980, 982, 986, 988, 994, 1020, 1056, 1162, 1174, 1186, 1204, 1218, 1240, 1244, 1246, 1248, 1252, 1254, 1260,
            1262, 1264, 1266, 1268, 1270, 1276, 1278, 1284, 1292, 1296, 1298, 1300, 1304, 1328 };
        public static int[] CommonStackLocations19016C11 = new int[] { 908, 934, 946, 982, 1268, 1304 };
        public static byte[] UseItemDungeon = new byte[] { 0x34, 0xE8, 0x0B, 0x34 };
        public static byte[] UseItemSurface = new byte[] { 0x10, 0xE8, 0x0B, 0x34 };
        public static byte[] Inventory = new byte[] { 0x67, 0x07, 0x5C, 0x09 };

        public BT3Memory()
        {
            StackStates = new BT3StackStates();

            // Exact states
            StackStates.Add(new BT3StackGuess(MainState.Adventuring, +0x00, new byte[] { 0x0A, 0xE8, 0x22, 0x07 }));
            StackStates.Add(new BT3StackGuess(MainState.CampInspecting, +0x234, new byte[] { 0xE8, 0xE6, 0x22, 0x07 }));
            StackStates.Add(new BT3StackGuess(MainState.CombatPartyAttack, +0x144, new byte[] { 0xB4, 0xE7, 0x48, 0x2C }));
            StackStates.Add(new BT3StackGuess(MainState.CombatSelectBardSong, +0x58, new byte[] { 0xB6, 0xE7, 0xA6, 0x02 }));
            StackStates.Add(new BT3StackGuess(MainState.LeaveGame, +0x00, new byte[] { 0xA8, 0xE8, 0xDE, 0x0F }));
            StackStates.Add(new BT3StackGuess(MainState.UseSelectCharacter, +0x00, new byte[] { 0x2E, 0xE8, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.ReviewMain, +0x00, new byte[] { 0x08, 0xE9, 0xE8, 0x00 }));
            StackStates.Add(new BT3StackGuess(MainState.ReviewWhoAdvance, +0x00, new byte[] { 0xEA, 0xE7, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.ReviewWhoSpell, +0x00, new byte[] { 0xE4, 0xE7, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.ReviewWhoTalk, +0x00, new byte[] { 0xEE, 0xE8, 0x65, 0x00 }));
            StackStates.Add(new BT3StackGuess(MainState.SelectBard, +0x00, new byte[] { 0x7D, 0x06, 0x22, 0xE9 }));
            StackStates.Add(new BT3StackGuess(MainState.SelectBard, +0x00, new byte[] { 0xFE, 0xE8, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.TavernInspectTrade, +0x22, new byte[] { 0xE8, 0xE8, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.Transfer, +0x00, new byte[] { 0xA8, 0xE8, 0x25, 0x00 }));
            StackStates.Add(new BT3StackGuess(MainState.TradeItemTo, +0x00, new byte[] { 0xA4, 0xE7, 0xE5, 0x23, 0xE4, 0x03, 0x00, 0x20, 0xE4, 0x03, 0xF8, 0xE7 }));
            StackStates.Add(new BT3StackGuess(MainState.Treasure, +0x00, new byte[] { 0x0E, 0xE8, 0xD6, 0x12 }));
            StackStates.Add(new BT3StackGuess(MainState.TreasureWhoWillCalfo, +0x00, new byte[] { 0xCE, 0xE7, 0xE5, 0x23, 0xE4, 0x03, 0x00, 0x20, 0xE4, 0x03, 0xD6, 0xE7, 0xE8, 0x10 }));
            StackStates.Add(new BT3StackGuess(MainState.TreasureWhoWillDisarm, +0x00, new byte[] { 0xA4, 0xE7, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.TreasureWhoWillInspect, +0x00, new byte[] { 0xC8, 0xE6, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.TreasureWhoWillOpen, +0x00, new byte[] { 0xCE, 0xE7, 0xE5, 0x23, 0xE4, 0x03, 0x00, 0x20, 0xE4, 0x03, 0xD6, 0xE7, 0x5D, 0x0F }));
            StackStates.Add(new BT3StackGuess(MainState.CombatOptions, +0x330, new byte[] { 0x9A, 0xE4, 0x22, 0x07 }));     // "You don't know any spells"
            StackStates.Add(new BT3StackGuess(MainState.CombatFriendly, +0x00, new byte[] { 0x36, 0xE9, 0x34, 0x0D }));
            StackStates.Add(new BT3StackGuess(MainState.EndGame, +0x00, new byte[] { 0xD0, 0xDE, 0x22, 0x07 }));

            // Wildcard states
            StackStates.Add(new BT3StackGuess(MainState.CombatOptions, +0x0E, new int[] { -1, -1, 0x27, 0x28 }));
            StackStates.Add(new BT3StackGuess(MainState.PreCombat, +0x00, new int[] { -1, 0xE9, 0x90, 0x26 }));
            StackStates.Add(new BT3StackGuess(MainState.PreCombat, +0x00, new int[] { -1, 0xE8, 0x90, 0x26 }));
            StackStates.Add(new BT3StackGuess(MainState.CombatConfirmRound, +0x00, new int[] { -1, 0xE7, 0x65, 0x00 }));
            StackStates.Add(new BT3StackGuess(MainState.Adventuring, +0x00, new int[] { 0x64, 0xE9, -1, -1 }));
            StackStates.Add(new BT3StackGuess(MainState.CampInspecting, +0x1A, new int[] { -1, 0xE9, 0xD7, 0x06 }));
            StackStates.Add(new BT3StackGuess(MainState.CreateSelectClass, +0x00, new int[] { -1, -1, 0x42, 0x07 }));
            StackStates.Add(new BT3StackGuess(MainState.ReviewWhoClass, +0x00, new int[] { -1, 0xE8, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.SelectSpellTarget, +0x138, new int[] { -1, -1, 0x48, 0x2C }));
            StackStates.Add(new BT3StackGuess(MainState.GuildMain, +0x00, new int[] { -1, 0xE9, 0x25, 0x11 }));
            StackStates.Add(new BT3StackGuess(MainState.CreateSelectRace, +0x00, new int[] { -1, -1, 0x2F, 0x0A }));
            StackStates.Add(new BT3StackGuess(MainState.CreateSelectSex, +0x00, new int[] { -1, -1, 0xD5, 0x09 }));
            StackStates.Add(new BT3StackGuess(MainState.SelectBardSong, +0x48, new int[] { -1, -1, 0xA6, 0x02 }));
            StackStates.Add(new BT3StackGuess(MainState.SelectSpellCaster, +0x10, new int[] { -1, -1, 0xE5, 0x23 }));
            StackStates.Add(new BT3StackGuess(MainState.Pause, +0x00, new int[] { -1, -1, 0x22, 0x07 }));
            StackStates.Add(new BT3StackGuess(MainState.Question, +0x00, new int[] { -1, 0xE9, 0x65, 0x00 }));
        }
    }

    public enum BT3Map
    {
        None              = -1,
        Unknown           = -1,

        // Surface Maps
        Wilderness        = 0x80,
        SkaraBraeRuins    = 0x81,
        Arboria           = 0x82,
        CieraBrannia      = 0x83,
        Gelidia           = 0x84,
        Lucencia          = 0x85,
        CelariaBree       = 0x86,
        Nowhere           = 0x87,   // Tenebrosia
        DarkCopse         = 0x88,
        BlackScar         = 0x89,

        // Dungeon Maps
        FesteringPit1     = 0,
        FesteringPit2     = 1,
        CrystalPalace     = 2,
        ValariansTower1   = 3,
        ValariansTower2   = 4,
        ValariansTower3   = 5,
        ValariansTower4   = 6,
        SacredGrove       = 7,
        WhiteTower1       = 8,
        WhiteTower2       = 9,
        WhiteTower3       = 10,
        WhiteTower4       = 11,
        GreyTower1        = 12,
        GreyTower2        = 13,
        GreyTower3        = 14,
        GreyTower4        = 15,
        BlackTower1       = 16,
        BlackTower2       = 17,
        BlackTower3       = 18,
        BlackTower4       = 19,
        IceDungeon1       = 20,
        IceDungeon2       = 21,
        IceKeep1          = 22,
        IceKeep2          = 23,
        Mountain1         = 24,
        Mountain2         = 25,
        CyanisTower1      = 26,
        CyanisTower2      = 27,
        CyanisTower3      = 28,
        AlliriasTomb1     = 29,
        AlliriasTomb2     = 30,
        Wasteland         = 31,
        Tarmitia          = 32,
        Berlin            = 33,
        Stalingrad        = 34,
        Hiroshima         = 35,
        Troy              = 36,
        Rome              = 37,
        Nottingham        = 38,
        KunWang           = 39,
        Catacombs1        = 40,
        Catacombs2        = 41,
        Malefia1          = 42,
        Malefia2          = 43,
        Malefia3          = 44,
        Barracks          = 45,
        Ferofists         = 46,
        PrivateQuarter    = 47,
        Workshop          = 48,
        UrmechsParadise   = 49,
        ViscousPlane      = 50,
        Sanctum           = 51,
        Unterbrae1        = 52,
        Unterbrae2        = 53,
        Unterbrae3        = 54,
        Unterbrae4        = 55,
        TarQuarry         = 56,
        ShadowCanyon      = 57,
        SceadusDemesne1   = 58,
        SceadusDemesne2   = 59,
        Tarjan            = 60
    }

    public class BT3KnownSpells : KnownSpells
    {
        // BT3 uses a 16-byte bitfield to store the known spells
        public override bool UsesSpellType { get { return false; } }
        public BT3KnownSpells(byte[] bytes, int offset = 0) { RawBytes = Global.Subset(bytes, offset, 18); }
        public override int NumKnown { get { return Global.NumBitsSet(RawBytes) - Global.NumBitsSet(new byte[] { RawBytes[16] }); } }
        public override bool IsKnown(int index, SpellType type) { return IsKnown(index, GenericClass.None); }
        public override string KnownString(GenericClass charClass) { return String.Format("{0}/133", NumKnown); }
        public bool KnowsAny { get { return !Global.AllNull(RawBytes); } }

        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null)
        {
            byte[] bytes = Global.NullBytes(18);
            if (original != null)
                bytes[16] = original.GetBytes()[16];   // # of Bard Songs left isn't actually part of the spellbook; it's just in the way
            return new BT3KnownSpells(bytes);
        }

        public override void SetKnown(Spell spell, bool bKnown)
        {
            int iOffset = spell.BasicIndex >= (int)BT3SpellIndex.SirRobinsTune ? 10 : -1;
            Global.SetBit(RawBytes, spell.BasicIndex + iOffset, bKnown ? 1 : 0);
        }

        public override bool IsKnown(int internalIndex, GenericClass mmClass)
        {
            int iOffset = internalIndex >= (int)BT3SpellIndex.SirRobinsTune ? 10 : -1;
            return Global.GetBit(RawBytes, internalIndex + iOffset) == 1;
        }

    }

    public class BT3TrainingInfo : BTTrainingInfo
    {
        public BT3Map Map { get { return (BT3Map)MapIndex; } }
    }

    public class BT3Effects
    {
        public byte AdventuringSong;
        private byte[] RawCounters;

        public int Counter(int i)
        {
            if (RawCounters == null || i * 2 >= RawCounters.Length - 1)
                return 0;

            return BitConverter.ToUInt16(RawCounters, i * 2);
        }

        public BT3Effects(BT3MemoryHacker hacker)
        {
            AdventuringSong = hacker.ReadByte(BT3.Memory.AdventuringSong);
            // Technically the scripts could set up to 255 counters, but they use nowhere near that many
            const int MaxCounters = 8;
            RawCounters = new byte[MaxCounters * 2];
            hacker.ReadOffset(BT3.Memory.Counter1, RawCounters);
        }

        public byte[] Bytes
        {
            get
            {
                return Global.Combine(new byte[] { AdventuringSong }, RawCounters );
            }
        }
    }

    public class BT3GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return BT3MemoryHacker.GetMapTitlePair(iMap); }

        public BT3GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn) { }

        public BT3GameInfoItem(string desc, object val, int offset, BitDescriptionDelegate fn)
            : base(desc, val, offset, DataType.Bits, 0, ShowStyle.Editable, fn) { }
    }

    public class BT3GameInfo : BTGameInfo
    {
        public override GameNames Game { get { return GameNames.BardsTale3; } }

        public BT3GameInfo()
        {
            MaxCounters = 4;
        }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new BT3GameInfoItem("View Dist.", (byte)LightDistance, new OffsetList(BT3.Memory.LightDistance)));
            items.Add(new BT3GameInfoItem("Light", (byte)LightDuration, new OffsetList(BT3.Memory.LightDuration)));
            items.Add(new BT3GameInfoItem("Levitate", (byte)LevitationDuration, new OffsetList(BT3.Memory.LevitationDuration)));
            items.Add(new BT3GameInfoItem("Compass", (byte)CompassDuration, new OffsetList(BT3.Memory.CompassDuration)));
            items.Add(new BT3GameInfoItem("Detect", (byte)DetectionDuration, new OffsetList(BT3.Memory.DetectionDuration)));
            items.Add(new BT3GameInfoItem("Shield", (byte)ShieldDuration, new OffsetList(BT3.Memory.ShieldDuration)));
            items.Add(new BT3GameInfoItem("Adv. Song", (byte)AdventuringSong, new OffsetList(BT3.Memory.AdventuringSong)));
            items.Add(new BT3GameInfoItem("Song Time", (byte)SongDuration, new OffsetList(BT3.Memory.SongDuration)));
            items.Add(new BT3GameInfoItem("Combat Song", (byte)CombatSong, new OffsetList(BT3.Memory.CombatSong)));
            items.Add(new BT3GameInfoItem("Dispell", CombatActiveSpells, new OffsetList(BT3.Memory.CombatActiveSpells), DataType.Boolean, 0x80));
            items.Add(new BT3GameInfoItem("Preclusion", CombatActiveSpells, new OffsetList(BT3.Memory.CombatActiveSpells), DataType.Boolean, 0x40));
            items.Add(new BT3GameInfoItem("AC Bonus", (byte)AdvACBonus, new OffsetList(BT3.Memory.AdvPartyACBonus)));
            if (Global.Debug)
            {
                // These are completely unuseful even for debugging
                //items.Add(new BT3GameInfoItem("Spell 1", (byte)SpellIcon1, new OffsetList(BT3.Memory.SpellIcon1)));
                //items.Add(new BT3GameInfoItem("Spell 2", (byte)SpellIcon2, new OffsetList(BT3.Memory.SpellIcon2)));
                //items.Add(new BT3GameInfoItem("Spell 3", (byte)SpellIcon3, new OffsetList(BT3.Memory.SpellIcon3)));
                //items.Add(new BT3GameInfoItem("Spell 4", (byte)SpellIcon4, new OffsetList(BT3.Memory.SpellIcon4)));
                //items.Add(new BT3GameInfoItem("Spell 5", (byte)SpellIcon5, new OffsetList(BT3.Memory.SpellIcon5)));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new BT3GameInfoItem("Time (hours)", (byte)GameTimeHours, new OffsetList(BT3.Memory.GameTimeHours)));
            items.Add(new BT3GameInfoItem("Surface Map", (byte)SurfaceMap, new OffsetList(BT3.Memory.SurfaceMapIndex)));
            items.Add(new BT3GameInfoItem("Map", (byte)MainMap, new OffsetList(BT3.Memory.MainMapIndex)));
            items.Add(new BT3GameInfoItem("Level", (byte)SubMap, new OffsetList(BT3.Memory.SubMapIndex)));
            items.Add(new BT3GameInfoItem("Trap", (byte)Trap, new OffsetList(BT3.Memory.TrapType)));
            items.Add(new BT3GameInfoItem("Counter 0", (UInt16)Counters[0], new OffsetList(BT3.Memory.Counter1)));
            items.Add(new BT3GameInfoItem("Counter 1", (UInt16)Counters[1], new OffsetList(BT3.Memory.Counter1 + 2)));
            items.Add(new BT3GameInfoItem("Counter 2", (UInt16)Counters[2], new OffsetList(BT3.Memory.Counter1 + 4)));
            items.Add(new BT3GameInfoItem("Counter 3", (UInt16)Counters[3], new OffsetList(BT3.Memory.Counter1 + 6)));
            items.Add(new BT3GameInfoItem("Risk/Reward", (byte)RiskReward, new OffsetList(BT3.Memory.MapTreasureIndex)));
            items.Add(new BT3GameInfoItem("Quest Bits", ScriptBits.Bytes, BT3.Memory.ScriptBits, BT3Bits.ScriptsDescription));

            return items;
        }
    }

    public interface IBT3MapLevel
    {
        MapXY NextLevel(bool bDown, Point ptCurrent);
    }

    public class BT3MapData : BTMapData, IBT3MapLevel
    {
        public string Name;
        public int BytesPerSquare = 1;
        public Size InternalSize;
        public int SquaresOffset;
        public int ScriptsOffset;
        public int[] RowOffsets;
        public MemoryBytes Scripts;
        public int MapLevel;
        public byte[] DungeonLevels;
        public BT3Monster[] MapMonsters;
        public MapXY Surface;
        public byte[] MapFlags;

        private BT3Scripts m_scripts = null;

        public BT3ScriptInfo GetScriptInfo() { return new BT3ScriptInfo(this, BT3MemoryHacker.GetMapTitlePair(Index), GetScripts(), MapMonsters, MapFlags); }

        public BT3Scripts GetScripts()
        {
            if (m_scripts == null)
                m_scripts = new BT3Scripts(Scripts, 0, ScriptsOffset, (int) Scripts.Offset);

            return m_scripts;
        }

        public bool SquareHasScript(Point pt)
        {
            if (Scripts == null || Scripts.Length < 3)
                return false;
            for (int i = 0; i < Scripts.Bytes[0]; i++)
            {
                if (Scripts.Bytes[i * 2 + 2] == pt.X && Scripts.Bytes[i * 2 + 1] == pt.Y)
                    return true;
            }
            return false;
        }

        public bool SquareIsActive(Point pt)
        {
            if (BytesPerSquare == 1)
                return false;

            int iOffset = pt.Y * BytesPerSquare * Width + pt.X * BytesPerSquare;
            if (Squares == null || Squares.Length < iOffset + BytesPerSquare)
                return false;

            BT3MapSpecials flags = (BT3MapSpecials)((Squares[iOffset + 2] << 16) | (Squares[iOffset + 3] << 8) | Squares[iOffset + 4]);

            return (flags & BT3MapSpecials.Active) != BT3MapSpecials.None;
        }

        public BT3MapData(BT3MemoryHacker hacker, MapBytes mb, int iMapIndex, byte[] flags)
        {
            Index = iMapIndex;
            MapMonsters = hacker.GetCurrentMonsters();
            MapFlags = flags;
            Name = Global.GetLowAsciiString(mb.Bytes, 0, 16);
            BytesPerSquare = (mb.Bytes[16] >= 32 || mb.Bytes[17] < 5) ? 5 : 1;
            int iSizeOffset = BytesPerSquare == 1 ? 16 : 30;
            int iRowOffset = BytesPerSquare == 1 ? 3 : 2;
            InternalSize = new Size(mb.Bytes[iSizeOffset], mb.Bytes[iSizeOffset + 1]);
            RowOffsets = new int[InternalSize.Height + 1];
            for (int i = 0; i < RowOffsets.Length; i++)
                RowOffsets[i] = BitConverter.ToInt16(mb.Bytes, iSizeOffset + 2 + iRowOffset);
            
            SquaresOffset = iSizeOffset + 2 + iRowOffset + ((InternalSize.Height + 1) * 2);
            ScriptsOffset = SquaresOffset + (InternalSize.Width * InternalSize.Height * BytesPerSquare);
            Squares = Global.Subset(mb.Bytes, SquaresOffset, ScriptsOffset - SquaresOffset);
            Scripts = new MemoryBytes(Global.Subset(mb.Bytes, ScriptsOffset), hacker.FoundBlockOffset + BT3.Memory.Map);
            Specials = null;
            Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            if (BytesPerSquare == 5)
            {
                MapLevel = mb.Bytes[17];
                DungeonLevels = Global.Subset(mb.Bytes, 18, 8);
                Surface = new MapXY(GameNames.BardsTale3, mb.Bytes[28] + 128, mb.Bytes[27], mb.Bytes[26]);
            }
        }

        public MapXY NextLevel(bool bDown, Point ptCurrent)
        {
            if (BytesPerSquare == 1)
                return MapXY.Empty;     // Surface maps don't have a specific "next level" map

            if (bDown)
            {
                if (MapLevel < 0 || MapLevel >= DungeonLevels.Length - 1|| DungeonLevels[MapLevel + 1] == 0xFF)
                    return MapXY.Empty;
                return new MapXY(GameNames.BardsTale3, DungeonLevels[MapLevel + 1], ptCurrent.X, ptCurrent.Y);
            }
            else
            {
                if (MapLevel == 0)
                    return Surface;
                if (MapLevel < 1 || MapLevel >= DungeonLevels.Length || DungeonLevels[MapLevel - 1] == 0xFF)
                    return MapXY.Empty;
                return new MapXY(GameNames.BardsTale3, DungeonLevels[MapLevel - 1], ptCurrent.X, ptCurrent.Y);
            }
        }
    }

    public class BT3EncounterInfo : BT23EncounterInfo
    {
        public BT3EncounterInfo(byte[] monsters, byte[] hp, byte[] living, byte[] distances, BTCombatEffects effects)
        {
            Groups = new BT3Monster[living.Length];
            for (int i = 0; i < living.Length; i++)
            {
                Groups[i] = new BT3Monster(i, monsters, i * 48);
                Groups[i].Distance = distances[i];
            }

            Distances = distances;
            MonsterIndices = monsters;
            MonsterHP = hp;
            Living = living;
            Effects = effects;
            AllBytes = Global.Combine(monsters, hp, distances, living, effects.Bytes);
        }

        public override void UpdateLivingMonstersBytes()
        {
            if (MonsterHP == null)
                Living = Global.NullBytes(4);
            Living = new byte[] { 32, 32, 32, 32 };
            for (byte i = 0; i < 64; i += 2)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Living[j] == 32 && BitConverter.ToUInt16(MonsterHP, j * 64 + i) == 0)
                    {
                        Living[j] = (byte) (i / 2);
                        if (Living.All(b => b != 32))
                            return;
                    }
                }
            }
        }
    }

    public class BT3CombatEffects : BTCombatEffects
    {
        public BT3CombatEffects(BT3MemoryHacker hacker)
        {
            Init(hacker, 7, 4);
        }

        public override void InitSpecific(BTMemoryHacker hacker)
        {
            CharAC = Global.NullBytes(7);
            PartyAttacks = 0;
            PartySubOptions2 = hacker.ReadOffset(hacker.Memory.PartyCombatSubOptions2, 7);
            CharDamage2 = hacker.ReadOffset(hacker.Memory.CharCombatDamageBonus2, 7);
        }

        public override string SpellName(byte b) { return BT3.SpellName(b); }
        public override bool HasTarget(byte b) { return BT3.SpellList.Value.HasTarget(b); }

        public override string GetItem(int iChar)
        {
            if (iChar < 0 || iChar >= PartySubOptions2.Length || !(Hacker is BT3MemoryHacker))
                return "<Unknown>";

            byte[] backpack = ((BT3MemoryHacker) Hacker).GetBackpackBytes(iChar);
            int iIndex = PartySubOptions2[iChar];
            if (iIndex < 0 || iIndex > 11)
                return "<Unknown>";

            return BT3Item.GetName((BT3ItemIndex)backpack[iIndex * 3 + 1]);
        }

        public override string GetTarget(int iTarget)
        {
            if (iTarget >= 128 && iTarget <= 131)
                return String.Format("Group {0}", (char)('A' + iTarget - 128));
            if (iTarget >= 0 && iTarget <= 6 && iTarget < Names.Length)
                return Names[iTarget];
            return "<Unknown>";
        }

        public override string GetCharTarget(int iChar) { return GetTarget(PartySelectedSpells[iChar]); }

        public override string GetAction(int iChar)
        {
            if (iChar < 0 || iChar > PartyOptions.Length)
                return String.Empty;

            string strName = Names[iChar];

            switch (PartyOptions[iChar])
            {
                case 1: return String.Format("{0}: Attack {1}, ", strName, GetTarget(PartySubOptions1[iChar]));
                case 2: return String.Format("{0}: Defend, ", strName);
                case 3: return String.Format("{0}: Attack {1}, ", strName, GetTarget(PartySubOptions1[iChar]));
                case 4: return String.Format("{0}: Cast {1}{2}, ", strName, SpellName(PartySubOptions1[iChar]),
                    HasTarget(PartySubOptions1[iChar]) ? String.Format(" on {0}", GetCharTarget(iChar)) : "");
                case 5: return String.Format("{0}: Use {1}, ", strName, GetItem(iChar));
                case 6: return String.Format("{0}: Hide, ", strName);
                case 7: return String.Format("{0}: Sing #{1}, ", strName, PartySubOptions1[iChar] + 1);
            }

            return String.Empty;
        }
    }

    [Flags]
    public enum BT3MapSpecials
    {
        None =            0,
        Seen =            0x000001,
        HideCartography = 0x000002,     // Do not show this square on the in-game map, even if it has been visited
        Diamond =         0x000004,     // Shows a diamond on the map if "Sanctuary" or "Succor Song" has been cast
        Visited =         0x000008,
        Explosion =       0x000010,
        NoTeleport =      0x000020,
        HPRegen  =        0x000040,     // This doesn't actually seem to have a real in-game effect
        Stuck =           0x000080,
        Spinner =         0x000100,
        AntiMagic =       0x000200,
        DrainHP =         0x000400,
        Unknown000800 =   0x000800,
        Silence =         0x001000,
        RestoreSP =       0x002000,
        DrainSP =         0x004000,
        AlwaysEncounter = 0x008000,
        Unknown010000 =   0x010000,
        Unknown020000 =   0x020000,
        Unknown040000 =   0x040000,
        Dark =            0x080000,
        Trap =            0x100000,
        PortalDown =      0x200000,
        PortalUp =        0x400000,
        Encounter =       0x800000,
        Special =         0x774CD0,
        Unknown =         0x020000,
        UnknownFlags =    0x070800,
        Active = Explosion | DrainHP | DrainSP | AlwaysEncounter | Trap | Encounter
    }

    public class BT3StackGuess
    {
        public int CharIndex;
        public MainState State;
        public byte[] Match;
        public int[] IntMatch;
        public bool HasWildcard { get { return IntMatch != null; } }

        public const int MainSearch = 213457;

        public BT3StackGuess(MainState state, int iChar, byte[] match)
        {
            CharIndex = iChar;
            State = state;
            Match = match;
            IntMatch = null;
        }

        public BT3StackGuess(MainState state, int iChar, int[] match)
        {
            CharIndex = iChar;
            State = state;
            Match = null;
            IntMatch = match;
        }

        public string ByteString
        {
            get
            {
                if (HasWildcard)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (int i in IntMatch)
                    {
                        if (i == -1)
                            sb.Append("?? ");
                        else
                            sb.AppendFormat("{0:X2} ", i);
                    }
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                    return sb.ToString();
                }
                else
                    return Global.HexString(Match);
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", State.ToString(), ByteString);
        }
    }

    public class BT3StackStates
    {
        public List<BT3StackGuess> Guesses;

        public BT3StackStates()
        {
            Guesses = new List<BT3StackGuess>();
        }

        public void Add(BT3StackGuess guess)
        {
            Guesses.Add(guess);
        }

        public static bool WildcardMatch(byte[] bytes1, int[] bytes2, int iIndex)
        {
            for (int i = 0; i < bytes2.Length; i++)
            {
                if (bytes2[i] == -1)  // Match any byte
                    continue;
                if (bytes1[iIndex + i] != bytes2[i])
                    return false;
            }
            return true;
        }

        public BT3StackGuess Find(byte[] bytes, int iOffset, int iLength)
        {
            foreach (BT3StackGuess guess in Guesses)
            {
                if (guess.HasWildcard)
                {
                    if (WildcardMatch(bytes, guess.IntMatch, iOffset))
                        return guess;
                }
                else
                {
                    if (Global.CompareBytes(bytes, guess.Match, iOffset))
                        return guess;
                }
            }
            return null;
        }
    }

    public class BT3MapCartography : MapCartography
    {
        public override bool SupportsSeen { get { return true; } }

        private bool IsSet(int x, int y, byte bFlag)
        {
            int iIndex = ((MapSize.Height - y - 1) * MapSize.Width * 5) + (x * 5) + 4;
            if (iIndex >= Bytes.Length || iIndex < 0)
                return false;
            byte bSquare = Bytes[iIndex];
            return (bSquare & bFlag) == bFlag;
        }

        public override bool IsVisited(int x, int y) { return IsSet(x, y, 0x08); }
        public override bool IsSeen(int x, int y) { return IsSet(x, y, 0x01); }
    }

    public class BT3TextDecoder
    {
        private static char NormChar(int iChar)
        {
            switch (iChar)
            {
                case 0: return '\0';
                case 1: return ' ';
                case 2: return 'a';
                case 3: return 'b';
                case 4: return 'c';
                case 5: return 'd';
                case 6: return 'e';
                case 7: return 'f';
                case 8: return 'g';
                case 9: return 'h';
                case 10: return 'i';
                case 11: return 'k';
                case 12: return 'l';
                case 13: return 'm';
                case 14: return 'n';
                case 15: return 'o';
                case 16: return 'p';
                case 17: return 'r';
                case 18: return 's';
                case 19: return 't';
                case 20: return 'u';
                case 21: return 'v';
                case 22: return 'w';
                case 23: return 'y';
                case 24: return '.';
                case 25: return '"';
                case 26: return '\'';
                case 27: return ',';
                case 28: return '!';
                case 29: return '\n';
                case 30: return '\0';
                case 31: return '\0';
                default: return '\0';
            }
        }

        private static char AltChar(int iChar)
        {
            switch (iChar)
            {
                case 0: return 'j';
                case 1: return 'q';
                case 2: return 'x';
                case 3: return 'z';
                case 4: return '0';
                case 5: return '1';
                case 6: return '2';
                case 7: return '3';
                case 8: return '4';
                case 9: return '5';
                case 10: return '6';
                case 11: return '7';
                case 12: return '8';
                case 13: return '9';
                case 14: return '0';
                case 15: return '1';
                case 16: return '2';
                case 17: return '3';
                case 18: return '4';
                case 19: return '5';
                case 20: return '6';
                case 21: return '7';
                case 22: return '8';
                case 23: return '9';
                case 24: return 'A';
                case 25: return 'B';
                case 26: return 'C';
                case 27: return 'D';
                case 28: return 'E';
                case 29: return 'F';
                case 30: return 'G';
                case 31: return 'H';
                case 32: return 'I';
                case 33: return 'J';
                case 34: return 'K';
                case 35: return 'L';
                case 36: return 'M';
                case 37: return 'N';
                case 38: return 'O';
                case 39: return 'P';
                case 40: return 'Q';
                case 41: return 'R';
                case 42: return 'S';
                case 43: return 'T';
                case 44: return 'U';
                case 45: return 'V';
                case 46: return 'W';
                case 47: return 'X';
                case 48: return 'Y';
                case 49: return 'Z';
                case 50: return '(';
                case 51: return ')';
                case 52: return '/';
                case 53: return '\\';
                case 54: return '#';
                case 55: return '*';
                case 56: return '?';
                case 57: return '<';
                case 58: return '>';
                case 59: return ':';
                case 60: return ';';
                case 61: return '-';
                case 62: return '%';
                case 63: return '\0';
                default: return '\0';
            }
        }

        private byte[] RawBytes;
        private int m_offset;
        private BitStream m_bs;
        private string m_decoded = null;

        public string DecodedString { get { return m_decoded; } }

        public BT3TextDecoder(byte[] bytes, int offset = 0)
        {
            RawBytes = bytes;
            m_offset = offset;
            m_bs = new BitStream(bytes, offset);
            m_decoded = Decode(m_bs);
        }

        public int GetByteLength()
        {
            if (m_bs == null)
                return 0;
            if (m_bs.BitPosition == 0)
                return 0;

            return (int) ((m_bs.BitPosition - 1) / 8) + 1;
        }

        private string Decode(BitStream bs)
        {
            StringBuilder sb = new StringBuilder();
            bool bUppercaseNext = false;
            bool bSkip = false;
            while (!bs.EndOfFile)
            {
                char c = '?';
                int iChar = bs.GetNextBits(5);
                if (iChar == 30)
                {
                    bUppercaseNext = true;
                    bSkip = true;
                }
                else if (iChar == 31)
                {
                    iChar = bs.GetNextBits(6);
                    c = AltChar(iChar);
                }
                else
                    c = NormChar(iChar);

                if (!bSkip)
                {
                    if (c == '\0')
                        break;
                    if (bUppercaseNext)
                    {
                        c = Char.ToUpper(c);
                        bUppercaseNext = false;
                    }
                    sb.AppendFormat("{0}", c);
                }
                bSkip = false;
            }

            return sb.ToString();
        }
    }

    public enum BT3ActiveFlags
    {
        Script =        0x01,
        Trap   =        0x02,
        Other  =        0x04,
        Encounter =     0x80
    }

    public class BT3ActiveSquares : ActiveSquares
    {
        public BT3ActiveSquares(MainForm main, int mapIndex, Size szMap, byte[] bytesActive)
        {
            Main = main;
            MapSize = szMap;
            m_iMapIndex = mapIndex;
            RawBytes = bytesActive;
            m_bInitialized = false;
        }

        public override bool IsActive(int x, int y, bool bEncountersOnly)
        {
            if (RawBytes == null || AllInactive)
                return false;

            int iOffset = y * MapSize.Width + x;
            if (x < 0 || y < 0 || x >= MapSize.Width || y >= MapSize.Height || iOffset < 0 || iOffset >= RawBytes.Length)
                return false;

            byte b = RawBytes[iOffset];
            if (bEncountersOnly)
                return (b & (int) BT3ActiveFlags.Encounter) != 0;
            return b > 0;
        }

        protected override void Initialize()
        {
            m_activeSquares = new Dictionary<Point, ActiveSquareInfo>();

            if (RawBytes == null || AllInactive)
                return;

            for (int y = 0; y < MapSize.Height; y++)
            {
                for (int x = 0; x < MapSize.Width; x++)
                {
                    Point pt = new Point(x, y);
                    m_activeSquares.Add(pt, new ActiveSquareInfo(pt, IsActive(x, y, false)));
                }
            }

            m_bInitialized = true;
        }
    }

    public class BT3SearchResults : BTSearchResults
    {
        public uint TotalXP;
        public int RiskReward;

        public BT3SearchResults(int trap, int iRiskReward, uint totalXP, byte[] itemMinimums, byte[] itemRanges)
        {
            RiskReward = iRiskReward;
            Trap = trap;
            TotalXP = totalXP;
            GoldMax = (int) (TotalXP / 5);
            Gold = (int) (TotalXP / 5);
            ItemMin = 0;
            ItemRange = 255;
        }

        public static string GetRiskRewardString(int iRiskReward)
        {
            // Quest items: 112-117, 119-124, 135-137, 145-157, 160-164, 169-170
            // Invalid items: 178-180, 224-239, 249-255
            switch (iRiskReward)
            {
                case 0: return "18% (1-22, Containers)";
                case 1: return "38% (22-46, Containers, Shadowshiv, Harmonic Gem)";
                case 2: return "42% (47-72, Containers, Harmonic Gem)";
                case 3: return "37% (48-110, Containers, Flame Knife)";
                case 4: return "34% (73-111, 125-134, 138-144, 165-168, 174, 181-195, 223, Containers)";
                case 5: return "52% (158-159, Red's Stiletto, 195-212, 223, Containers)";
                case 6: return "55% (171-172, 213-222, Containers, Harmonic Gem)";
                case 7: return "83% (Anything except quest items)";
                default: return String.Format("(Unknown treasure level {0})", iRiskReward);
            }
        }

        public override int CompareTo(SearchResults results)
        {
            BT3SearchResults btResults = results as BT3SearchResults;
            if (btResults == null)
                return 1;

            if (btResults.RiskReward != RiskReward)
                return 1;

            return base.CompareTo(results);
        }

        public override string ContentsString
        {
            get
            {
                if (TotalXP < 1)
                    return "Nothing";
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} Gold\r\n", Gold);
                sb.AppendFormat("Chance of item: {0}\r\n", GetRiskRewardString(RiskReward));
                return sb.ToString();
            }
        }
    }

    public class BT3MemoryHacker : BTMemoryHacker
    {
        public override BTMemory Memory { get { return BT3.Memory; } }
        public override List<BTItem> BTItems { get { return BT3.Items; } }
        protected override BTGameInfo CreateGameInfo() { return new BT3GameInfo(); }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new BT3GameInformationControl(main); }
        public override string GetMapEnum(int index) { return String.Format("BT3Map.{0}", Enum.GetName(typeof(BT3Map), (BT3Map)(index))); }
        protected override QuestInfo CreateQuestInfo() { return new BT3QuestInfo(); }
        public override int MaxInventoryChar { get { return 999; } }
        public override int MaxBackpackSize { get { return 12; } }
        protected override BTTrainingInfo CreateTrainingInfo() { return new BT3TrainingInfo(); }
        protected override bool IsOutside(int iMap) { return iMap == (int)BT3Map.Wilderness; }
        public override bool HasCartography { get { return true; } }
        public override bool CartographySupportsSeen { get { return IsDungeon(GetCurrentMapIndex()); } }
        public override bool HasScripts { get { return true; } }
        public override int ScriptCommandOffset { get { return 0; } }
        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }
        public override int DelayBetweenSpellKeys { get { return 10; } }
        public override string BagOfHoldingRequirement { get { return "in the Wilderness"; } }
        public override bool CartographyCanUnvisitSquares { get { return false; } }
        public override bool CartographyEditableGlobally { get { return false; } }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return BT3RosterFile.Create(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "THIEVES.INF"; } }

        protected override void OnReinitialized(EventArgs e)
        {
            base.OnReinitialized(e);
        }

        public BT3MemoryHacker()
        {
            m_game = GameNames.BardsTale3;
        }

        public string DumpStack()
        {
            byte[] stack = ReadOffset(BT3.Memory.Stack - 512, 512);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 512; i += 2)
            {
                sb.AppendFormat("{0,-3}:{1:X4}\r\n", i, BitConverter.ToUInt16(stack, 510 - i));
            }
            return sb.ToString();
        }

        //public HashSet<int> listBC000000 = new HashSet<int>();
        //public HashSet<int> list19016C11 = new HashSet<int>();

        protected override BTGameState GetMainState(byte[] stack, byte[] states = null)
        {
            BTGameState state = new BTGameState();

            state.Main = MainState.Unknown;
            int iActingChar = 0;

            if (stack == null)
                return null;

            foreach (int i in BT3Memory.CommonStackLocationsBC000000)
            {
                if (stack[i] != 0xBC || stack[i + 1] != 0 || stack[i + 2] != 0 || stack[i + 3] != 0)
                    continue;

                BT3StackGuess guess = BT3.Memory.StackStates.Find(stack, i + 4, 8);
                if (guess == null)
                    continue;

                state.Main = guess.State;
                if (guess.CharIndex != 0)
                    iActingChar = stack[i + guess.CharIndex];
                break;
            }

            if (state.Main == MainState.Unknown)
            {
                // It's convenient to know who is using an item but not super-important, so just
                // check these two locations (surface and dungeon)
                if (Global.CompareBytes(stack, BT3Memory.UseItemDungeon, 1276))
                {
                    state.Main = MainState.UseSelectItem;
                    iActingChar = stack[1276 + 0x30];
                }
                else if (Global.CompareBytes(stack, BT3Memory.UseItemSurface, 1240))
                {
                    state.Main = MainState.UseSelectItem;
                    iActingChar = stack[1240 + 0x30];
                }

                // The same for inventory
                foreach (int iAddress in new int[] { 1308, 1272 })
                {
                    if (Global.CompareBytes(stack, BT3Memory.Inventory, iAddress))
                    {
                        state.Main = MainState.Inventory;
                        iActingChar = stack[iAddress + 4];
                        break;
                    }
                }
            }

            if (state.Main == MainState.Unknown)
            {
                foreach (int i in BT3Memory.CommonStackLocations19016C11)
                {
                    if (stack[i] != 0x19 || stack[i + 1] != 0x01 || stack[i + 2] != 0x6C || stack[i + 3] != 0x11)
                        continue;

                    state.Main = MainState.SelectSpell;
                    iActingChar = stack[i + 0x0A];
                    break;
                }
            }

            if (state.Main == MainState.Unknown)
            {
                // Look through the entire stack as a last resort (takes about 4 µs in release mode on a 3.3 GHz machine to fail to find anything)
                for (int i = stack.Length - 8; i > 0; i--)
                {
                    if (stack[i] != 0xBC && stack[i] != 0x19)
                        continue;

                    if (stack[i] == 0x19 && stack[i + 1] == 0x01 && stack[i + 2] == 0x6C && stack[i + 3] == 0x11)
                    {
                        //if (!list19016C11.Contains(i)) { list19016C11.Add(i); Global.Log("19016C11 in uncommon location: {0}", i); }
                        state.Main = MainState.SelectSpell;
                        iActingChar = stack[i + 0x0A];
                        // This is good to know but a "BC 00 00 00" state will take precedence if we find one later
                    }

                    if (stack[i] != 0xBC || stack[i + 1] != 0 || stack[i + 2] != 0 || stack[i + 3] != 0)
                        continue;

                    BT3StackGuess guess = BT3.Memory.StackStates.Find(stack, i + 4, 8);
                    if (guess == null)
                        continue;

                    //if (!listBC000000.Contains(i)) { listBC000000.Add(i); Global.Log("BC000000 in uncommon location: {0}", i); }
                    state.Main = guess.State;
                    if (guess.CharIndex != 0)
                        iActingChar = stack[i + guess.CharIndex];
                    break;
                }
            }

            state.InCombat = ReadByte(Memory.MonsterIndices) > 0x7F;

            if (!state.InCombat)
            {
                switch (state.Main)
                {
                    case MainState.PreCombat:
                    case MainState.CombatOptions:
                    case MainState.CombatFriendly:
                        state.Main = MainState.Adventuring;
                        break;
                    case MainState.CombatSelectSpell:
                        state.Main = MainState.SelectSpell;
                        int iStackOffset = ReadUInt16(BT3.Memory.StackOffsetIndex);
                        switch (iStackOffset)
                        {
                            case 0xE8C8:
                                iActingChar = ReadByte(BT3.Memory.CastingCharSurface);
                                break;
                            default:
                                iActingChar = ReadByte(BT3.Memory.CastingCharDungeon);
                                break;
                        }
                        break;
                    case MainState.CombatSelectBardSong:
                        state.Main = MainState.SelectBardSong;
                        break;
                    case MainState.CombatConfirmRound:
                        state.Main = MainState.Adventuring;
                        break;
                }
            }
            else
            {
                int iStackOffset = ReadUInt16(BT3.Memory.StackOffsetIndex);
                int iActingCharOffset = Memory.CombatActingChar3 + (iStackOffset - 0xE880) - Memory.Stack + Memory.StackSize;
                if (iActingCharOffset >= 0 && iActingCharOffset < stack.Length)
                    iActingChar = stack[iActingCharOffset];
            }

            switch (state.Main)
            {
                case MainState.CombatOptions:
                case MainState.Combat:
                case MainState.CombatFriendly:
                case MainState.CombatSelectSpell:
                case MainState.Treasure:
                case MainState.TreasureWhoWillCalfo:
                case MainState.TreasureWhoWillDisarm:
                case MainState.TreasureWhoWillInspect:
                case MainState.TreasureWhoWillOpen:
                    state.InCombat = true;
                    break;
                default:
                    break;
            }

            // If the state is "unknown" then default to the last known state
            // (prevents map swaps and other undesired effects during transitional states)
            if (state.Main != MainState.Unknown)
            {
                m_lastMainState = state.Main;
                m_lastActingChar = iActingChar;
            }
            else
            {
                state.Main = m_lastMainState;
                iActingChar = m_lastActingChar;
            }

            Global.FixRange(ref iActingChar, 0, 6);
            state.ActingCharAddress = iActingChar;
            state.ActingCaster = iActingChar;
            state.ActingCombatChar = iActingChar;

            return state;
        }

        protected override List<Item> GetSuperItems(GenericClass btClass)
        {
            List<Item> items = new List<Item>(12);

            BT3Item carpet = BT3.Item(BT3ItemIndex.AlisCarpet);
            BT3Item ring = BT3.Item(BT3ItemIndex.ShieldRing);
            BT3Item helm = BT3.Item(BT3ItemIndex.TitanHelm);
            BT3Item shield = BT3.Item(BT3ItemIndex.TungstenShield);
            BT3Item wand = BT3.Item(BT3ItemIndex.WandOfFury);
            BT3Item suit = BT3.Item(BT3ItemIndex.DiamondSuit);
            BT3Item cloak = BT3.Item(BT3ItemIndex.ElfCloak);
            BT3Item blade = BT3.Item(BT3ItemIndex.Stoneblade);
            BT3Item gloves = BT3.Item(BT3ItemIndex.Wargloves);

            switch (btClass)
            {
                case GenericClass.Warrior:
                    items.Add(blade);
                    items.Add(shield);
                    items.Add(BT3.Item(BT3ItemIndex.TitanSuit));
                    items.Add(helm);
                    items.Add(gloves);
                    items.Add(suit);
                    break;
                case GenericClass.Geomancer:
                    items.Add(shield);
                    items.Add(BT3.Item(BT3ItemIndex.TitanSuit));
                    items.Add(helm);
                    items.Add(gloves);
                    items.Add(wand);
                    items.Add(suit);
                    items.Add(BT3.Item(BT3ItemIndex.StaffOfGods));
                    break;
                case GenericClass.Paladin:
                    items.Add(blade);
                    items.Add(shield);
                    items.Add(BT3.Item(BT3ItemIndex.TungstenPlate));
                    items.Add(helm);
                    items.Add(gloves);
                    items.Add(suit);
                    break;
                case GenericClass.Bard:
                    items.Add(BT3.Item(BT3ItemIndex.Strifespear));
                    items.Add(shield);
                    items.Add(BT3.Item(BT3ItemIndex.DiamondPlate));
                    items.Add(BT3.Item(BT3ItemIndex.AdamantHelm));
                    items.Add(BT3.Item(BT3ItemIndex.MinstrelsGlove));
                    items.Add(BT3.Item(BT3ItemIndex.CliLyre));
                    items.Add(BT3.Item(BT3ItemIndex.BardBow));
                    break;
                case GenericClass.Hunter:
                    items.Add(BT3.Item(BT3ItemIndex.HunterBlade));
                    items.Add(BT3.Item(BT3ItemIndex.DiamondShield));
                    items.Add(BT3.Item(BT3ItemIndex.MithrilSuit));
                    items.Add(helm);
                    items.Add(BT3.Item(BT3ItemIndex.AdamantGloves));
                    items.Add(BT3.Item(BT3ItemIndex.EelskinTunic));
                    items.Add(BT3.Item(BT3ItemIndex.SteadyEye));
                    items.Add(BT3.Item(BT3ItemIndex.StealthArrows));
                    break;
                case GenericClass.Monk:
                    items.Add(BT3.Item(BT3ItemIndex.Buckler));
                    items.Add(BT3.Item(BT3ItemIndex.MithrilChain));
                    items.Add(BT3.Item(BT3ItemIndex.MithrilHelm));
                    items.Add(BT3.Item(BT3ItemIndex.LeatherGloves));
                    items.Add(BT3.Item(BT3ItemIndex.TaoRing));
                    items.Add(cloak);
                    break;
                case GenericClass.Rogue:
                    items.Add(BT3.Item(BT3ItemIndex.Heartseeker));
                    items.Add(BT3.Item(BT3ItemIndex.Dragonshield));
                    items.Add(BT3.Item(BT3ItemIndex.TitanBracers));
                    items.Add(BT3.Item(BT3ItemIndex.ThievesHood));
                    items.Add(BT3.Item(BT3ItemIndex.LeatherGloves));
                    items.Add(cloak);
                    break;
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                case GenericClass.Sorcerer:
                case GenericClass.Wizard:
                case GenericClass.Archmage:
                case GenericClass.Chronomancer:
                    items.Add(BT3.Item(BT3ItemIndex.StaffOfGods));
                    items.Add(BT3.Item(BT3ItemIndex.TitanBracers));
                    items.Add(BT3.Item(BT3ItemIndex.SorcerersHood));
                    items.Add(BT3.Item(BT3ItemIndex.MagesGlove));
                    items.Add(wand);
                    items.Add(cloak);
                    break;
                default:
                    break;
            }

            if (items.Count < 12 && btClass != GenericClass.Hunter)
                items.Add(carpet);
            if (items.Count < 12 && btClass != GenericClass.Monk)
                items.Add(ring);

            foreach (BTItem item in items)
                item.ChargesCurrent = 255;

            return items;
        }

        public override int GetLightDistance(Point ptLocation)
        {
            if (!IsDungeon(GetCurrentMapIndex()))
                return 3;
            return ReadByte(BT3.Memory.LightDistance);
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int iSize = CharacterSize;
            int iMemorySize = CharacterMemorySize;

            int offset = iAddress * iSize;
            CharacterOffsets offsets = BT3.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + CharacterSize > info.Bytes.Length + 1)
                return false;

            GenericClass btClass = BT3Character.GetBasicClass((BT3Class)info.Bytes[offset + offsets.Class]);

            byte[] bytes = new byte[] { 30, 30, 30, 30, 30 }; // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)BT3Condition.Good;
            info.Bytes[offset + offsets.Level] = 99;
            info.Bytes[offset + offsets.LevelMod] = 99;
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentSP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxSP, 9999);
            Global.SetInt32(info.Bytes, offset + offsets.Gold, 200000000);  // Low enough so that "pool gold" doesn't overflow the value
            Global.SetInt32(info.Bytes, offset + offsets.Experience, BTCharacter.XPForLevel(Game, btClass, 99));
            bytes = Global.ByteArray(16, 0xff); // Spells
            bytes[15] = 0xF8;   // Last three spellbook bits aren't used
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Spells, bytes.Length);

            byte[] bytesNew = new byte[iMemorySize * 7];
            for(int i = 0; i < 7; i++)
                Buffer.BlockCopy(info.Bytes, i * iSize, bytesNew, i * iMemorySize, iMemorySize);

            WriteOffset(Memory.PartyInfo, bytesNew);

            List<Item> items = GetSuperItems(btClass);

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override IEnumerable<Monster> GetMonsterList() { return BT3.Monsters; }

        public bool GetGameTime(out int seconds, out int minutes, out int hours)
        {
            hours = ReadByte(Memory.GameTimeHours);
            seconds = ReadUInt16(Memory.GameTimeSeconds) % 2048;
            // There are 2048 seconds in a Bard's Tale hour
            minutes = seconds * 100 / 3413;
            seconds = (seconds % 34) * 17647 / 10000;
            return true;
        }

        public override String GetGameTime(bool bFull)
        {
            int seconds, minutes, hours;
            if (!GetGameTime(out seconds, out minutes, out hours))
                return String.Empty;

//            return String.Format("{0}:{1:D2}:{2:D2} {3}", hours, minutes, seconds, hours >= 12 ? "PM" : "AM");
            return String.Format("{0}:{1:D2} {2}", hours == 0 ? 12 : hours % 12, minutes, hours >= 12 ? "PM" : "AM");
        }

        protected override BTQuestData CreateQuestData()
        {
            BTPartyInfo party = GetPartyInfo() as BTPartyInfo;
            if (party == null)
                return null;
            MemoryBytes questBits = ReadOffset(BT3.Memory.ScriptBits, 16);
            BT3MapData map = new BT3MapData(this, GetCurrentMapBytes(), GetCurrentMapIndex(), GetCurrentMapFlags());
            return new BT3QuestData(party, GetLocation(), GetGameState() as BTGameState, questBits.Bytes, map, new BT3Effects(this));
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new BT3RosterFile(Global.CombineRoster(Game), bSilent);
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
                if (bytesChar[BT3.Offsets.Inventory + (i * 3) + 1] != 0)
                    backpack.Add(BT3Item.FromBT3InventoryBytes(bytesChar, BT3.Offsets.Inventory + (i * 3)));
            }

            return backpack;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!ValidateRosterFile())
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
                BTCharacter BT3Char = null;
                if (iStart < m_roster.Chars.Count)
                    BT3Char = BTCharacter.Create(Game, 0, m_roster.Chars[iStart].Bytes, 0);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                    case InventoryCharAction.FindOrCreate:
                        if (BT3Char != null && BT3Char.Name.ToUpper().StartsWith("INVENTORY"))
                            return iStart;
                        break;
                    case InventoryCharAction.FindPotential:
                        if (BT3Char == null || BT3Char.Name.ToUpper().StartsWith("INVENTORY"))
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

            try
            {
                ((BT3RosterFile)m_roster).DeleteInventoryChars();
                ((BT3RosterFile)m_roster).CreateInventoryChars((iMaxItems - 1) / MaxBackpackSize + 1);
                ((BT3RosterFile)m_roster).SaveRoster();
            }
            catch (Exception)
            {
                return false;
            }
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

            Buffer.BlockCopy(bytes.Items, 0, bytesChar, BT3.Offsets.Inventory, 36);
            m_roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        public override byte[] GetInventoryCharBytes() { return GetInventoryChar(); }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            BT3MapData bt3Data = new BT3MapData(this, mb, GetCurrentMapIndex(), GetCurrentMapFlags());
            bt3Data.LiveOnly = true;
            return bt3Data;
        }

        public override bool VisitFacingSquare(BasicLocation location, MapSheet sheet)
        {
            if (sheet == null || location == null || IsDungeon(location.MapIndex))
                return false;

            return (BT3Map)location.MapIndex >= BT3Map.Wilderness;
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            BT3MapData data = new BT3MapData(this, GetCurrentMapBytes(), GetCurrentMapIndex(), GetCurrentMapFlags());

            data.Title = GetMapTitlePair(data.Index);

            if (IsDungeon(data.Index))
            {
                data.Specials = null;
                data.CustomSquares = null;
                data.FixedEncounters = null;
                data.Teleport = null;
                data.Monsters = null;
            }
            data.Bounds = new Rectangle(0, 0, data.Width, data.Height);

            return data;
        }

        public override bool IsDungeon(int iMap) { return IsDungeon((BT3Map)iMap); }

        public bool IsDungeon(BT3Map map)
        {
            return map < BT3Map.Wilderness;
        }

        private MapBytes GetMapBytes(MemoryBytes bytes, int iIndex)
        {
            if (bytes == null || bytes.Length < 32)
                return null;

            MapBytes mb = null;

            if (IsDungeon((BT3Map)iIndex))
                mb = new MapBytes(bytes.Bytes, bytes.Bytes[30], bytes.Bytes[31]);
            else
                mb = new MapBytes(bytes.Bytes, bytes.Bytes[16], bytes.Bytes[17]);

            return mb;
        }

        public MemoryBytes GetCurrentMapFlags() { return ReadOffset(BT3.Memory.MapFlags, 12); }

        public override MapBytes GetCurrentMapBytes()
        {
            int iIndex = GetCurrentMapIndex();
            MemoryBytes bytes = ReadOffset(BT3.Memory.Map, 4096);

            return GetMapBytes(bytes, iIndex);
        }

        private Size GetMapSize()
        {
            BT3Map map = (BT3Map) GetCurrentMapIndex();
            byte[] pbSize = new byte[2];
            ReadOffset(IsDungeon(map) ? BT3.Memory.MapSizeDungeons : BT3.Memory.MapSize, pbSize);
            return new Size(pbSize[0], pbSize[1]);
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            Size sz = GetMapSize();
            return new Point(ReadByte(Memory.LocationEast), ReadByte(Memory.LocationNorth));
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            Size sz = GetMapSize();
            WriteByte(Memory.LocationEast, (byte)ptLocation.X);
            return WriteByte(Memory.LocationNorth, (byte)ptLocation.Y);
        }

        public override int GetCurrentMapIndex()
        {
            byte bLoading = ReadByte(BT3.Memory.MapLoadingStatus);
            if (bLoading != 255)
                return -1;

            MemoryBytes bytesMaps = ReadOffset(Memory.SurfaceMapIndex, 6);
            if (bytesMaps == null)
                return (int) BT3Map.Wilderness;
            byte[] bytes = new byte[16];
            ReadOffset(Memory.Map, bytes);
            if (bytes[15] == 0)
                return (int)BT3Map.Wilderness;
            UInt32 hash = BitConverter.ToUInt32(bytes, 0);

            switch (hash)
            {
                case 0xE4ECE9D7: return (int)BT3Map.Wilderness;
                case 0xF2E1EBD3: return (int)BT3Map.SkaraBraeRuins;
                case 0xEFE2F2C1: return (int)BT3Map.Arboria;
                case 0xF2E5E9C3: return (int)BT3Map.CieraBrannia;
                case 0xE9ECE5C7: return (int)BT3Map.Gelidia;
                case 0xE5E3F5CC: return (int)BT3Map.Lucencia;
                case 0xE1ECE5C3: return (int)BT3Map.CelariaBree;
                case 0xE8F7EFCE: return (int)BT3Map.Nowhere;
                case 0xEBF2E1C4: return (int)BT3Map.DarkCopse;
                case 0xE3E1ECC2: return bytes[6] == 0xD3 ? (int)BT3Map.BlackScar : bytesMaps[2];    // "Black Tower" has the same first four characters
                default:
                    return bytesMaps[2];
            }
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((BT3Map)index)
            {
                case BT3Map.None: return new MapTitleInfo(index, "Legend", "");
                case BT3Map.Wilderness: return new MapTitleInfo(index, "Wilderness");
                case BT3Map.Tarjan: return new MapTitleInfo(index, "Tarjan", "\\Malefia\\");
                case BT3Map.Arboria: return new MapTitleInfo(index, "Arboria", "\\Arboria\\");
                case BT3Map.FesteringPit1: return new MapTitleInfo(index, "Festering Pit 1", "\\Arboria\\");
                case BT3Map.FesteringPit2: return new MapTitleInfo(index, "Festering Pit 2", "\\Arboria\\");
                case BT3Map.CrystalPalace: return new MapTitleInfo(index, "Crystal Palace", "\\Arboria\\");
                case BT3Map.ValariansTower1: return new MapTitleInfo(index, "Valarian's Tower 1", "\\Arboria\\");
                case BT3Map.ValariansTower2: return new MapTitleInfo(index, "Valarian's Tower 2", "\\Arboria\\");
                case BT3Map.ValariansTower3: return new MapTitleInfo(index, "Valarian's Tower 3", "\\Arboria\\");
                case BT3Map.ValariansTower4: return new MapTitleInfo(index, "Valarian's Tower 4", "\\Arboria\\");
                case BT3Map.SacredGrove: return new MapTitleInfo(index, "Sacred Grove", "\\Arboria\\");
                case BT3Map.CieraBrannia: return new MapTitleInfo(index, "Ciera Brannia", "\\Arboria\\");
                case BT3Map.CelariaBree: return new MapTitleInfo(index, "Celaria Bree", "\\Arboria\\");
                case BT3Map.Gelidia: return new MapTitleInfo(index, "Gelidia", "\\Gelidia\\");
                case BT3Map.WhiteTower1: return new MapTitleInfo(index, "White Tower 1", "\\Gelidia\\");
                case BT3Map.WhiteTower2: return new MapTitleInfo(index, "White Tower 2", "\\Gelidia\\");
                case BT3Map.WhiteTower3: return new MapTitleInfo(index, "White Tower 3", "\\Gelidia\\");
                case BT3Map.WhiteTower4: return new MapTitleInfo(index, "White Tower 4", "\\Gelidia\\");
                case BT3Map.GreyTower1: return new MapTitleInfo(index, "Grey Tower 1", "\\Gelidia\\");
                case BT3Map.GreyTower2: return new MapTitleInfo(index, "Grey Tower 2", "\\Gelidia\\");
                case BT3Map.GreyTower3: return new MapTitleInfo(index, "Grey Tower 3", "\\Gelidia\\");
                case BT3Map.GreyTower4: return new MapTitleInfo(index, "Grey Tower 4", "\\Gelidia\\");
                case BT3Map.BlackTower1: return new MapTitleInfo(index, "Black Tower 1", "\\Gelidia\\");
                case BT3Map.BlackTower2: return new MapTitleInfo(index, "Black Tower 2", "\\Gelidia\\");
                case BT3Map.BlackTower3: return new MapTitleInfo(index, "Black Tower 3", "\\Gelidia\\");
                case BT3Map.BlackTower4: return new MapTitleInfo(index, "Black Tower 4", "\\Gelidia\\");
                case BT3Map.IceDungeon1: return new MapTitleInfo(index, "Ice Dungeon 1", "\\Gelidia\\");
                case BT3Map.IceDungeon2: return new MapTitleInfo(index, "Ice Dungeon 2", "\\Gelidia\\");
                case BT3Map.IceKeep1: return new MapTitleInfo(index, "Ice Keep 1", "\\Gelidia\\");
                case BT3Map.IceKeep2: return new MapTitleInfo(index, "Ice Keep 2", "\\Gelidia\\");
                case BT3Map.Barracks: return new MapTitleInfo(index, "Barracks", "\\Kinestia\\");
                case BT3Map.Ferofists: return new MapTitleInfo(index, "Ferofist's", "\\Kinestia\\");
                case BT3Map.PrivateQuarter: return new MapTitleInfo(index, "Private Quarter", "\\Kinestia\\");
                case BT3Map.Workshop: return new MapTitleInfo(index, "Workshop", "\\Kinestia\\");
                case BT3Map.UrmechsParadise: return new MapTitleInfo(index, "Urmech's Paradise", "\\Kinestia\\");
                case BT3Map.ViscousPlane: return new MapTitleInfo(index, "Viscous Plane", "\\Kinestia\\");
                case BT3Map.Sanctum: return new MapTitleInfo(index, "Sanctum", "\\Kinestia\\");
                case BT3Map.Lucencia: return new MapTitleInfo(index, "Lucencia", "\\Lucencia\\");
                case BT3Map.Mountain1: return new MapTitleInfo(index, "Mountain 1", "\\Lucencia\\");
                case BT3Map.Mountain2: return new MapTitleInfo(index, "Mountain 2", "\\Lucencia\\");
                case BT3Map.CyanisTower1: return new MapTitleInfo(index, "Cyanis' Tower 1", "\\Lucencia\\");
                case BT3Map.CyanisTower2: return new MapTitleInfo(index, "Cyanis' Tower 2", "\\Lucencia\\");
                case BT3Map.CyanisTower3: return new MapTitleInfo(index, "Cyanis' Tower 3", "\\Lucencia\\");
                case BT3Map.AlliriasTomb1: return new MapTitleInfo(index, "Alliria's Tomb 1", "\\Lucencia\\");
                case BT3Map.AlliriasTomb2: return new MapTitleInfo(index, "Alliria's Tomb 2", "\\Lucencia\\");
                case BT3Map.Malefia1: return new MapTitleInfo(index, "Malefia 1", "\\Malefia\\");
                case BT3Map.Malefia2: return new MapTitleInfo(index, "Malefia 2", "\\Malefia\\");
                case BT3Map.Malefia3: return new MapTitleInfo(index, "Malefia 3", "\\Malefia\\");
                case BT3Map.SkaraBraeRuins: return new MapTitleInfo(index, "Skara Brae", "\\Skara Brae\\");
                case BT3Map.Catacombs1: return new MapTitleInfo(index, "Catacombs", "\\Skara Brae\\");
                case BT3Map.Catacombs2: return new MapTitleInfo(index, "Tunnels", "\\Skara Brae\\");
                case BT3Map.Unterbrae1: return new MapTitleInfo(index, "Unterbrae 1", "\\Skara Brae\\");
                case BT3Map.Unterbrae2: return new MapTitleInfo(index, "Unterbrae 2", "\\Skara Brae\\");
                case BT3Map.Unterbrae3: return new MapTitleInfo(index, "Unterbrae 3", "\\Skara Brae\\");
                case BT3Map.Unterbrae4: return new MapTitleInfo(index, "Unterbrae 4", "\\Skara Brae\\");
                case BT3Map.Tarmitia: return new MapTitleInfo(index, "Tarmitia", "\\Tarmitia\\");
                case BT3Map.Wasteland: return new MapTitleInfo(index, "Wasteland", "\\Tarmitia\\");
                case BT3Map.Berlin: return new MapTitleInfo(index, "Berlin", "\\Tarmitia\\");
                case BT3Map.Stalingrad: return new MapTitleInfo(index, "Stalingrad", "\\Tarmitia\\");
                case BT3Map.Hiroshima: return new MapTitleInfo(index, "Hiroshima", "\\Tarmitia\\");
                case BT3Map.Troy: return new MapTitleInfo(index, "Troy", "\\Tarmitia\\");
                case BT3Map.Rome: return new MapTitleInfo(index, "Rome", "\\Tarmitia\\");
                case BT3Map.Nottingham: return new MapTitleInfo(index, "Nottingham", "\\Tarmitia\\");
                case BT3Map.KunWang: return new MapTitleInfo(index, "K'un Wang", "\\Tarmitia\\");
                case BT3Map.TarQuarry: return new MapTitleInfo(index, "Tar Quarry", "\\Tenabrosia\\");
                case BT3Map.ShadowCanyon: return new MapTitleInfo(index, "Shadow Canyon", "\\Tenabrosia\\");
                case BT3Map.SceadusDemesne1: return new MapTitleInfo(index, "Sceadu's Demesne 1", "\\Tenabrosia\\");
                case BT3Map.SceadusDemesne2: return new MapTitleInfo(index, "Sceadu's Demesne 2", "\\Tenabrosia\\");
                case BT3Map.Nowhere: return new MapTitleInfo(index, "Nowhere", "\\Tenabrosia\\");
                case BT3Map.DarkCopse: return new MapTitleInfo(index, "Dark Copse", "\\Tenabrosia\\");
                case BT3Map.BlackScar: return new MapTitleInfo(index, "Black Scar", "\\Tenabrosia\\");
                default: return new MapTitleInfo(index, String.Format("Unknown {0}", index));
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
            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, BT3Character.SizeInMemory * 7);
            if (bytes == null)
                return 0;

            int iNumChars = 0;
            for (int i = 0; i < 7; i++)
                if (bytes.Bytes[i * BT3Character.SizeInMemory] != 0)
                    iNumChars++;

            return iNumChars;
        }

        private BTPartyInfo ReadBT3PartyInfo()
        {
            byte numChars = (byte)GetNumChars();
            if (numChars > 7)
                numChars = 7;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new BTPartyInfo(new byte[0], new byte[0], numChars);

            BTGameState state = GetGameState() as BTGameState;

            MemoryBytes bytesStats = ReadOffset(Memory.PartyInfo, BT3Character.SizeInMemory * 7);
            int iInspecting = 0;
            if (state.Casting)
                iInspecting = state.ActingCaster;
            else if (state.InCombat)
                iInspecting = state.ActingCombatChar;
            else
                iInspecting = state.ActingCharAddress;

            Global.FixRange(ref iInspecting, 0, 6);
            BTPartyInfo info = new BTPartyInfo(ReadOffset(Memory.PartyInfo, BT3Character.SizeInMemory * 7), GetMarchingOrder(), numChars);

            info.State = state;

            // Bard's Tale only has one "acting" character, so make sure they're all the same
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

            return ReadBT3PartyInfo();
        }

        public override byte[] GetMarchingOrder()
        {
            byte[] order = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            MemoryBytes mb = ReadOffset(Memory.MarchingOrder, 7);
            if (mb == null)
                return order;

            for(int i = 0; i < mb.Length; i++)
            {
                if (mb.Bytes[i] < 0 || mb.Bytes[i] > 6)
                    return order;
                mb.Bytes[i]++;      // BTMemoryHacker expects "1 to n", not "0 to n-1"
            }

            return mb.Bytes;
        }

        protected override byte GetFacingByte(int iMapIndex) { return ReadByte(IsDungeon(iMapIndex) ? Memory.Facing : Memory.FacingTown); }
        protected override bool CanUseBag(int iMapIndex) { return iMapIndex == (int) BT3Map.Wilderness; }

        public override string SpellType1 { get { return "Conjurer"; } }
        public override string SpellType2 { get { return "Magician"; } }
        public override string SpellType3 { get { return "Sorcerer"; } }
        public override string SpellType4 { get { return "Wizard"; } }
        public override string SpellType5 { get { return "Archmage"; } }
        public override string SpellType6 { get { return "Bard"; } }

        public override byte[] GetBackpackBytes(List<Item> items)
        {
            byte[] bytes = Global.NullBytes(36);
            for (int i = 0; i < 12; i++)
            {
                if (i >= items.Count)
                    continue;
                BT3Item bt3Item = items[i] as BT3Item;
                if (bt3Item == null)
                    continue;
                if (i >= items.Count)
                    break;
                byte[] bytesItem = bt3Item.Serialize();
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

        public override Size GetCurrentMapDimensions()
        {
            MemoryBytes mb = ReadOffset(Memory.Map, 32);
            int iBytesPerSquare = (mb.Bytes[16] >= 32 || mb.Bytes[17] < 5) ? 5 : 1;
            int iSizeOffset = iBytesPerSquare == 1 ? 16 : 30;
            return new Size(mb.Bytes[iSizeOffset], mb.Bytes[iSizeOffset + 1]);
        }

        public override MapBasicInfo GetCurrentMapInfo()
        {
            int iIndex = GetCurrentMapIndex();

            return new MapBasicInfo(iIndex, GetCurrentMapDimensions());
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            byte[] bytesNull = Global.NullBytes(4);

            MemoryBytes mbMonsters = ReadOffset(Memory.MonsterIndices, 48 * 4);
            if (mbMonsters == null || Global.Compare(mbMonsters.Bytes, bytesNull))
                return null;

            MemoryBytes mbDistances = new MemoryBytes(new byte[] {
                (byte) (mbMonsters.Bytes[48 * 0 + 19] & 0x0f),
                (byte) (mbMonsters.Bytes[48 * 1 + 19] & 0x0f),
                (byte) (mbMonsters.Bytes[48 * 2 + 19] & 0x0f),
                (byte) (mbMonsters.Bytes[48 * 3 + 19] & 0x0f)
            });

            MemoryBytes mbNumAlive = new MemoryBytes(new byte[] {
                (byte) (mbMonsters.Bytes[48 * 0 + 21] & 0x1f),
                (byte) (mbMonsters.Bytes[48 * 1 + 21] & 0x1f),
                (byte) (mbMonsters.Bytes[48 * 2 + 21] & 0x1f),
                (byte) (mbMonsters.Bytes[48 * 3 + 21] & 0x1f)
            });

            MemoryBytes mbHP = ReadOffset(Memory.MonsterHP, 256);
            Point ptParty = GetLocation().PrimaryCoordinates;

            BTCombatEffects effects = new BT3CombatEffects(this);

            if (!effects.GameState.InCombat)
                return null;

            if (m_lastEncounterInfo != null && m_lastEncounterInfo.HasTreasure && effects != null && effects.GameState.IsTreasure && m_lastEncounterInfo.SearchResults == null)
                bForceNew = true;

            if (!bForceNew &&
                m_lastEncounterInfo != null &&
                Global.Compare(m_lastEncounterInfo.MonsterIndices, mbMonsters.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Living, mbNumAlive.Bytes) &&
                Global.Compare(m_lastEncounterInfo.MonsterHP, mbHP.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Effects.Bytes, effects.Bytes) &&
                m_lastEncounterInfo.InCombat == effects.GameState.InCombat &&
                m_lastEncounterInfo.HasTreasure == effects.GameState.IsTreasure)
                return m_lastEncounterInfo;

            BT3EncounterInfo info = new BT3EncounterInfo(mbMonsters.Bytes, mbHP.Bytes, mbNumAlive.Bytes, mbDistances, effects);
            if (effects.GameState.IsTreasure)
            {
                info.SearchResults = new BT3SearchResults(effects.Trap,
                    ReadByte(Memory.MapTreasureIndex),
                    ReadUInt32(Memory.EncounterMonstersKilled),
                    ReadOffset(Memory.TreasureMinimums, 8),
                    ReadOffset(Memory.TreasureRanges, 8));
            }
            info.Party = GetPartyInfo();

            int iTotal = 0;
            Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
            for (int i = 0; i < 4; i++)
            {
                if (info.MonsterIndices[i] < 1)
                    continue;
                for (int j = 0; j < mbNumAlive.Bytes[i]; j++)
                {
                    int iHPIndex = i * 64 + (j * 2);
                    if (iHPIndex >= info.MonsterHP.Length - 1)
                        break;
                    BT3Monster monster = new BT3Monster(0, mbMonsters.Bytes, i * 48);
                    monster.CurrentHP = BitConverter.ToInt16(info.MonsterHP, iHPIndex);
                    monster.Distance = mbDistances[i] * 10;
                    monster.Position = ptParty;
                    monster.MonsterGroup = i;
                    monster.MonsterSubGroup = j;
                    monster.EncounterIndex = iTotal;
                    monsters.Add(iTotal, monster);
                    iTotal++;
                }
            }

            info.NumTotalMonsters = iTotal;
            info.Monsters = monsters;
            info.PartyLocation = ptParty;

            m_lastEncounterInfo = info;

            return info;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is BTEncounterInfo) || !IsValid)
                return false;

            BT3EncounterInfo bi = info as BT3EncounterInfo;

            byte[] monsters = Global.Subset(bi.MonsterIndices, 0);
            for (int i = 0; i < bi.Living.Length; i++)
            {
                monsters[i * 48 + 21] = (byte)((monsters[i * 48 + 21] & 0xE0) | bi.Living[i]);
                monsters[i * 48 + 19] = (byte)((monsters[i * 48 + 19] & 0xF0) | bi.Distances[i]);
            }
            WriteOffset(Memory.MonsterIndices, monsters);
            return WriteOffset(Memory.MonsterHP, bi.MonsterHP);
        }

        public override string[] GetCharacterNames()
        {
            MemoryBytes mb = ReadOffset(Memory.PartyInfo, BT3Character.SizeInMemory * 7);
            if (mb == null)
                return new string[0];
            List<string> names = new List<string>(7);
            for (int i = 0; i < 7; i++)
            {
                if (mb.Bytes[i * BT3Character.SizeInMemory] == 0x00)
                    break;
                names.Add(Global.GetNullTerminatedString(mb.Bytes, i * BT3Character.SizeInMemory, 15));
            }

            return names.ToArray();
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            BTCharCreationInfo info = new BTCharCreationInfo();
            info.Race = BTCharacter.GetBasicRace((BTRace)ReadByte(Memory.CreationRace));
            info.MemoryOffset = Memory.CreationStats;
            MemoryBytes bytes = ReadOffset(Memory.CreationStats, 16);
            if (bytes.Bytes[0] > 19)
            {
                bytes = ReadOffset(BT3.Memory.CreationStatsAlt, 16);
                info.MemoryOffset = BT3.Memory.CreationStatsAlt;
            }
            info.AttributesModified = new byte[6];
            info.AttributesOriginal = new byte[6];
            info.AttributesModified[0] = bytes.Bytes[0];
            info.AttributesModified[1] = bytes.Bytes[2];
            info.AttributesModified[2] = bytes.Bytes[4];
            info.AttributesModified[3] = bytes.Bytes[6];
            info.AttributesModified[4] = bytes.Bytes[8];
            info.AttributesModified[5] = bytes.Bytes[10];
            Buffer.BlockCopy(info.AttributesModified, 0, info.AttributesOriginal, 0, 6);
            info.State = GetGameState();
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            if (!(info is BTCharCreationInfo))
                return false;

            byte[] bytes = new byte[] { info.AttributesModified[0], 0, info.AttributesModified[1], 0, 
                 info.AttributesModified[2], 0, info.AttributesModified[3], 0, info.AttributesModified[4], 0, info.AttributesModified[5], 0};
            WriteOffset(((BTCharCreationInfo) info).MemoryOffset, bytes);
            WriteOffset(BT3.Memory.CreationHP, new byte[] { info.AttributesModified[5], 0, info.AttributesModified[5], 0 });
            return true;
        }

        protected override void WriteTrainingInfo(BTTrainingInfo btInfo, int iAddress)
        {
            byte[] HPSP = Global.Subset(btInfo.Party.Bytes, btInfo.Party.CharacterSize * iAddress + BT3.Offsets.CurrentHP, 8);
            WriteOffset(BT3.Memory.PartyInfo + CharacterMemorySize * iAddress + BT3.Offsets.CurrentHP, HPSP);
        }

        protected override MonsterName[] GetMonsterNames()
        {
            MemoryBytes monsters = ReadOffset(Memory.MonsterIndices, 128);
            if (monsters == null)
                return base.GetMonsterNames();

            MonsterName[] names = new MonsterName[4];
            for (int i = 0; i < 4; i++)
                names[i] = ExtractMonsterNames(monsters.Bytes, i * 32);
            return names;
        }

        public static byte[] GetInventoryChar() { return Properties.Resources.BT3InventoryChar; }

        public override byte[] GetBackpackBytes(int iCharAddress)
        {
            CharacterOffsets offsets = Games.CharacterOffsets(Game);
            return ReadOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory, offsets.InventoryLength).Bytes;
        }

        public override bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            CharacterOffsets offsets = Games.CharacterOffsets(Game);
            GenericClass gc = BT3Character.GetBasicClass((BT3Class) ReadByte(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Class));
            for (int i = 0; i < bytes.Length; i += 3)
                BT3Item.SetUsableBit(bytes, gc, i);
            return WriteOffset(Memory.PartyInfo + (iCharAddress * CharacterMemorySize) + offsets.Inventory, bytes);
        }

        public override bool IsSurface(int iMap) { return !IsDungeon((BT3Map) iMap); }

        public override bool HasCartographyOnMap(int iMap = -1) { return IsDungeon((BT3Map) (iMap == -1 ? GetCurrentMapIndex() : iMap)); }

        public override MapCartography GetCartography()
        {
            if (!IsValid)
                return null;

            int iIndex = GetCurrentMapIndex();
            if (IsSurface(iIndex))
                return null;    // BT3 surface maps don't keep track of cartography

            MapCartography cart = new BT3MapCartography();
            cart.MapIndex = iIndex;

            BT3MapData mapData = new BT3MapData(this, GetCurrentMapBytes(), cart.MapIndex, GetCurrentMapFlags());
            if (mapData == null || mapData.Squares == null)
                return null;

            cart.SetBytes(mapData.Squares, mapData.Bounds.Size);
            return cart;
        }

        public override bool EditMapCartography(MapCartography.EditAction action)
        {
            if (!IsValid)
                return false;

            BT3MapData mapData = new BT3MapData(this, GetCurrentMapBytes(), GetCurrentMapIndex(), GetCurrentMapFlags());
            int iSquares = mapData.Bounds.Width * mapData.Bounds.Height;

            for (int i = 0; i < iSquares; i++)
            {
                switch (action)
                {
                    case MapCartography.EditAction.FillSingle:
                    case MapCartography.EditAction.FillAll:
                        mapData.Squares[i * 5 + 4] |= 0x09;
                        break;
                    case MapCartography.EditAction.ClearSingle:
                    case MapCartography.EditAction.ClearAll:
                        mapData.Squares[i * 5 + 4] &= 0xF6;
                        break;
                    default:
                        break;
                }
            }

            WriteOffset(Memory.Map + mapData.SquaresOffset, mapData.Squares);
            return true;
        }

        public override ScriptInfo GetScriptInfo(MemoryBytes scriptBytes)
        {
            int iMapIndex = GetCurrentMapIndex();
            BT3MapData mapData = new BT3MapData(this, GetMapBytes(scriptBytes, iMapIndex), iMapIndex, GetCurrentMapFlags());
            BT3Scripts scripts = mapData.GetScripts();
            return new BT3ScriptInfo(mapData, GetMapTitlePair(GetCurrentMapIndex()), scripts, mapData.MapMonsters, mapData.MapFlags);
        }
        
        public override MemoryBytes GetScriptBytes()
        {
            return ReadOffset(BT3.Memory.Map, 4096);
        }

        public override GameScripts GetScripts(MemoryBytes bytes) { return GetScriptInfo().Scripts; }

        public override bool SetScriptLine(ScriptLine line)
        {
            return WriteOffset(Memory.Map + line.Address, line.CommandBytes);
        }

        public static string FacingString(int i, bool bAbbrev = false)
        {
            switch (i)
            {
                case 0: return bAbbrev ? "N" : "North";
                case 1: return bAbbrev ? "E" : "East";
                case 2: return bAbbrev ? "S" : "South";
                case 3: return bAbbrev ? "W" : "West";
                default: return bAbbrev ? "?" : "Unknown";
            }
        }

        private BT3Monster[] m_monstersLast = null;
        private MemoryBytes m_mbMonstersLast = null;

        public BT3Monster[] GetCurrentMonsters()
        {
            MemoryBytes mb = ReadOffset(BT3.Memory.CurrentMonsters, 48 * 32);
            if (m_mbMonstersLast != null && Global.Compare(mb.Bytes, m_mbMonstersLast.Bytes))
                return m_monstersLast;

            m_mbMonstersLast = mb;
            List<BT3Monster> list = new List<BT3Monster>();
            for (int i = 0; i < mb.Length; i += 48)
            {
                if (mb.Bytes[i] == 0)
                    break;
                BT3Monster monster = new BT3Monster(i / 48, mb.Bytes, i);
                list.Add(monster);
            }

            m_monstersLast = list.ToArray();
            return m_monstersLast;
        }

        private byte[] m_bytesSpecialRawLast = null;
        private byte[] m_bytesSpecialCookedLast = null;

        public override byte[] GetSpecialBytes()
        {
            MemoryBytes mb = ReadOffset(Memory.Map, 4096);

            if (Global.Compare(m_bytesSpecialRawLast, mb.Bytes))
                return m_bytesSpecialCookedLast;

            m_bytesSpecialRawLast = mb.Bytes;

            int iBytesPerSquare = (mb.Bytes[16] >= 32 || mb.Bytes[17] < 5) ? 5 : 1;
            int iSizeOffset = iBytesPerSquare == 1 ? 16 : 30;
            int iRowOffset = iBytesPerSquare == 1 ? 3 : 2;
            Size sz = new Size(mb.Bytes[iSizeOffset], mb.Bytes[iSizeOffset + 1]);
            if (sz.Width > 48 || sz.Height > 48)
                return null;
            int iSquaresOffset = iSizeOffset + 2 + iRowOffset + ((sz.Height + 1) * 2);
            int iScriptsOffset = iSquaresOffset + (sz.Width * sz.Height * iBytesPerSquare);

            byte[] bytes = new byte[sz.Width * sz.Height];
            if (iBytesPerSquare == 5)
            {
                for (int y = 0; y < sz.Height; y++)
                {
                    for (int x = 0; x < sz.Width; x++)
                    {
                        int iOffset = iSquaresOffset + y * iBytesPerSquare * sz.Width + x * iBytesPerSquare + 2;
                        if (iOffset > mb.Length - 3 || iOffset < 0)
                            return null;
                        int iByteOffset = (sz.Height - y - 1) * sz.Width + x;
                        BT3MapSpecials flags = (BT3MapSpecials)((mb.Bytes[iOffset] << 16) | (mb.Bytes[iOffset + 1] << 8) | mb.Bytes[iOffset + 2]);
                        bytes[iByteOffset] = (byte) (flags.HasFlag(BT3MapSpecials.Encounter) || flags.HasFlag(BT3MapSpecials.AlwaysEncounter) ? 0x80 : 0x00);
                        if ((flags & BT3MapSpecials.Active) != BT3MapSpecials.None)
                            bytes[iByteOffset] |= (byte) BT3ActiveFlags.Other;
                    }
                }
            }

            for (int iScript = 0; iScript < mb.Bytes[iScriptsOffset]; iScript++)
            {
                int iOffset = mb.Bytes[iScriptsOffset + iScript * 4 + 1] * sz.Width + mb.Bytes[iScriptsOffset + iScript * 4 + 2];
                if (bytes.Length > iOffset)
                    bytes[iOffset] |= (byte) BT3ActiveFlags.Script;
            }

            m_bytesSpecialCookedLast = bytes;
            return bytes;
        }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return BT3Character.GetBT3StatModifier(value, stat);
        }

        public override string GetClassDescription(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Paladin: return "+2 to-hit, 3-18 HP/level, +Level/2 priority";
                case GenericClass.Warrior: return "+2 to-hit, 3-18 HP/level, +Level/2 priority";
                case GenericClass.Hunter: return "+2 to-hit, 3-18 HP/level, +Level/2 priority";
                case GenericClass.Bard: return "+1 to-hit, 3-18 HP/level, +Level/4 priority";
                case GenericClass.Rogue: return "+1 to-hit, 3-10 HP/level, +Level/4 priority";
                case GenericClass.Monk: return "+3 to-hit, 3-10 HP/level, +Level priority";
                case GenericClass.Conjurer: return "+0 to-hit, 3-6 HP/level, +Level/8 priority";
                case GenericClass.Magician: return "+0 to-hit, 3-6 HP/level, +Level/8 priority";
                case GenericClass.Sorcerer: return "+0 to-hit, 3-10 HP/level, +Level/8 priority";
                case GenericClass.Wizard: return "+0 to-hit, 3-10 HP/level, +Level/8 priority";
                case GenericClass.Archmage: return "+? to-hit, 3-10 HP/level, +? priority";
                case GenericClass.Chronomancer: return "+? to-hit, 3-10 HP/level, +? priority";
                case GenericClass.Geomancer: return "+? to-hit, 3-10 HP/level, +? priority";
                default: return "Unknown";
            }
        }

        public void SetAwards(int iCharAddress, byte[] awards)
        {
            if (iCharAddress < 0 || iCharAddress > 6)
                return;
            WriteOffset(Memory.PartyInfo + (BT3Character.SizeInMemory) * iCharAddress + BT3.Offsets.Awards, awards);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            bool bMale = bytes[BT3.Offsets.Sex] == 0;
            switch ((BT3Class)bytes[BT3.Offsets.Class])
            {
                case BT3Class.Archmage:
                case BT3Class.Chronomancer:
                case BT3Class.Conjurer:
                case BT3Class.Magician:
                case BT3Class.Wizard:
                case BT3Class.Sorcerer:
                case BT3Class.Geomancer:
                    bytes[BT3CharacterOffsets.Animation] = (byte)(bMale ? 54 : 79);
                    break;
                default:
                    bytes[BT3CharacterOffsets.Animation] = (byte)(bMale ? 33 : 48);
                    break;
            }
            return base.SetCharacterBytes(iAddress, bytes);
        }

        public HashSet<BT3ItemIndex> GetAllInventoryItems()
        {
            HashSet<BT3ItemIndex> list = new HashSet<BT3ItemIndex>();
            int iChars = GetNumChars();

            MemoryBytes mb = ReadOffset(Memory.PartyInfo, iChars * CharacterMemorySize);
            if (mb == null)
                return list;

            for (int i = 0; i < iChars; i++)
            {
                for (int j = 0; j < MaxBackpackSize; j++)
                {
                    BT3ItemIndex item = (BT3ItemIndex)mb.Bytes[i * CharacterMemorySize + BT3.Offsets.Inventory + (j * 3 + 1)];
                    if (item != BT3ItemIndex.None && !list.Contains(item))
                        list.Add(item);
                }
            }
            return list;
        }

        public override StatsPerLevel GetStatsPerLevel(GenericClass gc)
        {
            int iLowHP = 3;
            int iHighHP = 10;
            int iLowSP = 4;
            int iHighSP = 7;
            switch (gc)
            {
                case GenericClass.Warrior:
                case GenericClass.Bard:
                case GenericClass.Paladin:
                case GenericClass.Hunter:
                    iHighHP = 18;
                    break;
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                    iHighHP = 6;
                    break;
            }

            return new StatsPerLevel(iLowHP, iHighHP, iLowSP, iHighSP);
        }
        public override TrapInfo CreateTrapInfo(int iTrap) { return new BT3TrapInfo(iTrap); }

        public override GenericClass GetCharacterClass(int iCharAddress)
        {
            return BT3Character.GetBasicClass((BT3Class) ReadByte(Memory.PartyInfo + (CharacterMemorySize * iCharAddress) + BT3.Offsets.Class));
        }
    }
}
