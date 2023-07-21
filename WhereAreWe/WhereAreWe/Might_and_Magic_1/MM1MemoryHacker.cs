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
    public static class MM1Memory
    {
        // Search for "NO SPELL POINTS\0SPELL FAILED\0"
        public static byte[] MainSearch = new byte[] { 0x4E, 0x4F, 0x20, 0x53, 0x50, 0x45, 0x4C, 0x4C, 0x20, 0x50, 0x4F, 0x49,
            0x4E, 0x54, 0x53, 0x00, 0x53, 0x50, 0x45, 0x4C, 0x4C, 0x20, 0x46, 0x41, 0x49, 0x4C, 0x45, 0x44, 0x00 };

        public const int MainBlockSVN = -89513;
        public const int MainBlockNonSVN = -89481;

        public const int Location = 159;
        public const int Facing = 164;
        public const int NumChars = 39;
        public const int DisplayState = 59;               // 2 bytes
        public const int PartyInfo = 2659;
        public const int Map = 353;                       // 256 bytes of appearance followed by 256 bytes of behavior
        public const int MapIndex = -14944;               // The index of the currently loaded map
        public const int ViewChar = -5395;
        public const int SelectedParty = -6148;           // 6 bytes of roster indices
        public const int EncounterList = -3353;
        public const int CombatRound = 66;
        public const int NumberOfMonsters = 132;
        public const int SpellBeingCast = -530;           // MM1Item.GetSpellName
        public const int MonsterCombatStatus = -800;      // MM1MonsterCombatStatus
        public const int PartyMeleeStatus = 111;          // 6 bytes, 1 if in melee, 0 if not
        public const int PartyHasMovedThisRound = -836;   // 6 bytes, 1 if character has moved this round [next 15 bytes are monsters]
        public const int PartyPositions = 271;            // 12 bytes, two per character, pointing to the character record
        public const int Handicap = 71;                   // first byte: 1=party, 2=monster; second byte = value
        public const int NumberofMeleeMonsters = 120;     // This many monsters are in melee range
        public const int SearchResults = 224;             // 6 bytes, Container type, item1, item2, item3, gold (2 bytes)
        public const int PreEncounterList = 191;          // 15 bytes - monster indices for upcoming encounter
        public const int EncounterFriendliness = 246;     // 1 byte; affects chance to run
        public const int EncounterState = 117;            // 1 byte; might be encounter state 0x29 = exploring, 0x20 = encounter
        public const int ScreenState = 42;                // 2 bytes; something to do with what screen is being displayed
        public const int MonsterHP = -785;                // 15 bytes; current monster HP
        public const int CastState = 67;                  // 2 bytes.  "1b 14" = entering s.level "1b 15" = entering s.number "27 17" enter to cast or cast on a-e "10 18" and "10 17" cast on 1-6
        public const int CastingSpellNoncombat = 193;     // 1 byte - spell about to be cast
        public const int CastingSpellCombat = -530;       // 1 byte - spell about to be cast
        public const int ActingCharacterOffset = 61;      // 2 bytes; offset into party bytes
        public const int FirstCharacterInternal = 0x45FC; // Offset of first character record
        public const int FirstMonsterInternal = 0x2E80;   // Offset of first monster record
        public const int InsideOutside = 81;              // 1 = inside, 2 = outside
        public const int MonsterCombatOrder = 191;        // 15 two-byte pointers
        public const int CreationStatsModified = -6142;   // 7 bytes of stats
        public const int CreationStatsOriginal = -6135;   // 7 bytes of stats
        public const int RollState = -15033;              // 1 byte
        public const int RollState2 = 59;                 // 2 bytes
        public const int LastKeypress = -15161;           // 1 byte
        public const int MonsterData = 28105;             // 8192 bytes
        public const int MapAppearance = 353;             // 256 bytes
        public const int MapAttributes = 609;             // 256 bytes
        public const int StartedArenkoQuest = 36327;      // 1 byte
        public const int MagicSquareNumbers = 36736;      // 9 bytes, correct = B3 B2 B5 B8 B9 B6 B7 B4 B1
        public const int Encounter13 = 36592;             // 1 byte, number of special encounters on crazed wizard cave map
        public const int GongTones = 36513;               // 3 bytes, Loud/Sharp/Mellow
        public const int TriviaChance = 36679;            // 1 byte
        public const int ForbiddenSpells = 36309;         // 1 byte
        public const int NumSpecialSquares = 36313;       // 1 byte length, then that many bytes
        public const int SpecialSquareCoord = 36314;      // <NumSpecialSquares> bytes * 2
        public const int SurfaceCoordinates = 36305;      // 2 bytes
        public const int MapGlobalAttributes = 36263;     // 88 bytes
        public const int ActiveEffects = 252;
        public const int CombatFlags = 60;                // 1 byte
        public const int MapScripts = -20154;
        public const int CharPtrOffset = 0x12210;         // MainBlock + this + InternalPtr = memory offset
        public const int ScriptPtrOffset = 0x1a60;        // MainBlock + this + InternalPtr = script location
        public const int GlobalScriptCode = -82761;
        public const int ShopItems = -9795;               // 90 bytes (18 items in each of 5 towns)
        public const int Char1RestCounter = 2697;
        public const int Char1Age = 2696;
        public const int RosterTowns = 2639;              // 18 bytes

        public static MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.MightAndMagic1]; } }

        public static byte[] MM1MapPtrBytes(MM1Map map)
        {
            int iPtr = MM1MapPointer(map);
            byte[] bytes = BitConverter.GetBytes(iPtr);
            return new byte[] { bytes[0], bytes[1], bytes[2] };
        }

        public static int MM1MapPointer(MM1Map map)
        {
            switch (map)
            {
                case MM1Map.A1CastleDoom: return 0x030706;
                case MM1Map.A1Surface: return 0x020F01;
                case MM1Map.A2Surface: return 0x020502;
                case MM1Map.A3Surface: return 0x020B02;
                case MM1Map.A4Surface: return 0x020103;
                case MM1Map.AstralPlane: return 0x030B1A;
                case MM1Map.B1AncientWizardLairLevel1: return 0x030F03;
                case MM1Map.B1AncientWizardLairLevel2: return 0x030703;
                case MM1Map.B1CastleBlackridgeNorth: return 0x030F08;
                case MM1Map.B1CastleBlackridgeSouth: return 0x030508;
                case MM1Map.B1Erliquin: return 0x010B1A;
                case MM1Map.B1ErliquinDungeon: return 0x010202;
                case MM1Map.B1Surface: return 0x020A00;
                case MM1Map.B2MedusaLair: return 0x010A00;
                case MM1Map.B2Surface: return 0x020703;
                case MM1Map.B2WarriorsStrongholdLevel1: return 0x030F02;
                case MM1Map.B2WarriorsStrongholdLevel2: return 0x030702;
                case MM1Map.B3CastleWhiteWolf: return 0x030A11;
                case MM1Map.B3CavesattheKorinBluffs: return 0x01051B;
                case MM1Map.B3Portsmith: return 0x010C03;
                case MM1Map.B3PortsmithDungeon: return 0x010C01;
                case MM1Map.B3StrongholdintheEnchantedForestLevel1: return 0x030F04;
                case MM1Map.B3StrongholdintheEnchantedForestLevel2: return 0x030704;
                case MM1Map.B3Surface: return 0x020101;
                case MM1Map.B4Surface: return 0x020D03;
                case MM1Map.C1Surface: return 0x020304;
                case MM1Map.C2CrazedWizardCave: return 0x010001;
                case MM1Map.C2Sorpigal: return 0x010604;
                case MM1Map.C2SorpigalDungeon: return 0x010A11;
                case MM1Map.C2Surface: return 0x020A11;
                case MM1Map.C3Surface: return 0x020904;
                case MM1Map.C4Surface: return 0x020F04;
                case MM1Map.C4Volcano: return 0x010212;
                case MM1Map.D1Surface: return 0x020505;
                case MM1Map.D2Surface: return 0x020B05;
                case MM1Map.D3CaveOfSquareMagic: return 0x010601;
                case MM1Map.D3Surface: return 0x020106;
                case MM1Map.D4Algary: return 0x010203;
                case MM1Map.D4Surface: return 0x020801;
                case MM1Map.E1CastleDragaduneLevel1: return 0x030107;
                case MM1Map.E1CastleDragaduneLevel2: return 0x030F05;
                case MM1Map.E1CastleDragaduneLevel3: return 0x030A00;
                case MM1Map.E1CastleDragaduneLevel4: return 0x030705;
                case MM1Map.E1Dusk: return 0x010802;
                case MM1Map.E1DuskDungeon: return 0x010005;
                case MM1Map.E1Surface: return 0x020112;
                case MM1Map.E2Surface: return 0x020706;
                case MM1Map.E3CastleAlamar: return 0x030B07;
                case MM1Map.E3Surface: return 0x020B1A;
                case MM1Map.E4FabledBuildingofGoldLevel1: return 0x030F01;
                case MM1Map.E4FabledBuildingofGoldLevel2: return 0x030701;
                case MM1Map.E4FabledBuildingofGoldLevel3: return 0x030E00;
                case MM1Map.E4FabledBuildingofGoldLevel4: return 0x030201;
                case MM1Map.E4Surface: return 0x02011B;
                case MM1Map.SoulMaze: return 0x030412;
                default: return 0x010604;   // Sorpigal
            }
        }

        public static MM1Map MM1PointerToMap(byte[] bytes, int index = 0)
        {
            int iPtr = (bytes[index + 2] << 16) | (bytes[index + 1] << 8) | bytes[index];
            return MM1PointerToMap(iPtr);
        }

        public static MM1Map MM1PointerToMap(int ptr)
        {
            switch (ptr)
            {
                case 0x010001: return MM1Map.C2CrazedWizardCave;
                case 0x010005: return MM1Map.E1DuskDungeon;
                case 0x010202: return MM1Map.B1ErliquinDungeon;
                case 0x010203: return MM1Map.D4Algary;
                case 0x010212: return MM1Map.C4Volcano;
                case 0x01051B: return MM1Map.B3CavesattheKorinBluffs;
                case 0x010601: return MM1Map.D3CaveOfSquareMagic;
                case 0x010604: return MM1Map.C2Sorpigal;
                case 0x010802: return MM1Map.E1Dusk;
                case 0x010A00: return MM1Map.B2MedusaLair;
                case 0x010A11: return MM1Map.C2SorpigalDungeon;
                case 0x010B1A: return MM1Map.B1Erliquin;
                case 0x010C01: return MM1Map.B3PortsmithDungeon;
                case 0x010C03: return MM1Map.B3Portsmith;
                case 0x020101: return MM1Map.B3Surface;
                case 0x020103: return MM1Map.A4Surface;
                case 0x020106: return MM1Map.D3Surface;
                case 0x020112: return MM1Map.E1Surface;
                case 0x02011B: return MM1Map.E4Surface;
                case 0x020304: return MM1Map.C1Surface;
                case 0x020502: return MM1Map.A2Surface;
                case 0x020505: return MM1Map.D1Surface;
                case 0x020703: return MM1Map.B2Surface;
                case 0x020706: return MM1Map.E2Surface;
                case 0x020801: return MM1Map.D4Surface;
                case 0x020904: return MM1Map.C3Surface;
                case 0x020A00: return MM1Map.B1Surface;
                case 0x020A11: return MM1Map.C2Surface;
                case 0x020B02: return MM1Map.A3Surface;
                case 0x020B05: return MM1Map.D2Surface;
                case 0x020B1A: return MM1Map.E3Surface;
                case 0x020D03: return MM1Map.B4Surface;
                case 0x020F01: return MM1Map.A1Surface;
                case 0x020F04: return MM1Map.C4Surface;
                case 0x030107: return MM1Map.E1CastleDragaduneLevel1;
                case 0x03010F: return MM1Map.E4FabledBuildingofGoldLevel1;              // This is not a valid value but is what is in the MM1 game itself
                case 0x030201: return MM1Map.E4FabledBuildingofGoldLevel4;
                case 0x030412: return MM1Map.SoulMaze;
                case 0x030508: return MM1Map.B1CastleBlackridgeSouth;
                case 0x030701: return MM1Map.E4FabledBuildingofGoldLevel2;
                case 0x030702: return MM1Map.B2WarriorsStrongholdLevel2;
                case 0x030703: return MM1Map.B1AncientWizardLairLevel2;
                case 0x030704: return MM1Map.B3StrongholdintheEnchantedForestLevel2;
                case 0x030705: return MM1Map.E1CastleDragaduneLevel4;
                case 0x030706: return MM1Map.A1CastleDoom;
                case 0x030A00: return MM1Map.E1CastleDragaduneLevel3;
                case 0x030A11: return MM1Map.B3CastleWhiteWolf;
                case 0x030B07: return MM1Map.E3CastleAlamar;
                case 0x030B1A: return MM1Map.AstralPlane;
                case 0x030E00: return MM1Map.E4FabledBuildingofGoldLevel3;
                case 0x030F01: return MM1Map.E4FabledBuildingofGoldLevel1;              // This is the value that will actually take you to this map in MM1
                case 0x030F02: return MM1Map.B2WarriorsStrongholdLevel1;
                case 0x030F03: return MM1Map.B1AncientWizardLairLevel1;
                case 0x030F04: return MM1Map.B3StrongholdintheEnchantedForestLevel1;
                case 0x030F05: return MM1Map.E1CastleDragaduneLevel2;
                case 0x030F08: return MM1Map.B1CastleBlackridgeNorth;
                default: return MM1Map.C2Sorpigal;
            }
        }
    }

    public enum MM1ScreenState
    {
        Unknown = 0,
        Combat1 = 20,
        Combat2 = 23,
        WallBonk = 21,
        Combat4 = 31,
        Combat3 = 33,
        InStore = 34,
        BuyingItem = 35,
        FoodStore = 29,
        InTemple = 36,
        InTraining1 = 37,
        InTraining2 = 38,
        Locked = 50
    }

    [Flags]
    public enum MM1MapAppearance
    {
        North = 0xC0,
        East = 0x30,
        South = 0x0C,
        West = 0x03
    }

    [Flags]
    public enum MM1MapBehavior
    {
        Encounter = 0x80,
        NorthSolid = 0x40,
        Dark = 0x20,
        EastSolid = 0x10,
        Dangerous = 0x08,
        SouthSolid = 0x04,
        AntiMagic = 0x02,
        WestSolid = 0x01,
    }

    public enum MM1Map
    {
        None = -1,
        C2Sorpigal = 0,
        B3Portsmith = 2,
        D4Algary = 4,
        E1Dusk = 6,
        B1Erliquin = 8,
        C2SorpigalDungeon = 10,
        C2CrazedWizardCave = 12,
        B3PortsmithDungeon = 14,
        B1ErliquinDungeon = 16,
        E1DuskDungeon = 18,
        B3CavesattheKorinBluffs = 20,
        C4Volcano = 22,
        D3CaveOfSquareMagic = 24,
        B2MedusaLair = 26,
        A1Surface = 28,
        A2Surface = 30,
        A3Surface = 32,
        A4Surface = 34,
        B1Surface = 36,
        B2Surface = 38,
        B3Surface = 40,
        B4Surface = 42,
        C1Surface = 44,
        C2Surface = 46,
        C3Surface = 48,
        C4Surface = 50,
        D1Surface = 52,
        D2Surface = 54,
        D3Surface = 56,
        D4Surface = 58,
        E1Surface = 60,
        E2Surface = 62,
        E3Surface = 64,
        E4Surface = 66,
        A1CastleDoom = 68,
        B1CastleBlackridgeNorth = 70,
        B1CastleBlackridgeSouth = 72,
        B1AncientWizardLairLevel1 = 74,
        B1AncientWizardLairLevel2 = 76,
        B2WarriorsStrongholdLevel1 = 78,
        B2WarriorsStrongholdLevel2 = 80,
        B3StrongholdintheEnchantedForestLevel1 = 82,
        B3StrongholdintheEnchantedForestLevel2 = 84,
        B3CastleWhiteWolf = 86,
        E1CastleDragaduneLevel1 = 88,
        E1CastleDragaduneLevel2 = 90,
        E1CastleDragaduneLevel3 = 92,
        E1CastleDragaduneLevel4 = 94,
        SoulMaze = 96,
        E3CastleAlamar = 98,
        E4FabledBuildingofGoldLevel1 = 100,
        E4FabledBuildingofGoldLevel2 = 102,
        E4FabledBuildingofGoldLevel3 = 104,
        E4FabledBuildingofGoldLevel4 = 106,
        AstralPlane = 108,
        Last = 109,

        Unknown = -1
    }

    public class MM1String : MMString
    {
        public int Offset;
        public string Value;

        public MM1String(int offset, string str)
        {
            Offset = offset;
            Value = str;
            Basic = str;

        }

        public override bool Valid { get { return true; } }
    }

    public class MM1MapInfo
    {
        public MM1MapAppearance[,] Appearance;   // 16x16 bytes
        public MM1MapBehavior[,] Behavior;       // 16x16 bytes

        public MM1MapInfo()
        {
            Appearance = new MM1MapAppearance[16, 16];
            Behavior = new MM1MapBehavior[16, 16];
        }

        public MM1MapInfo(byte[] bytes, int index = 0) : this() { SetBytes(bytes, index); }

        public void SetBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 512)
                return;

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Appearance[x, y] = (MM1MapAppearance)bytes[index + y * 16 + x];
                    Behavior[x, y] = (MM1MapBehavior)bytes[index + 256 + y * 16 + x];
                }
            }
        }
    }

    public class MM1GameState : GameState
    {
        public override GameNames Game { get { return GameNames.MightAndMagic1; } }

        public override bool Casting
        {
            get
            {
                return (Main == MainState.CastLevel || Main == MainState.CastNumber);
            }
        }

        public override bool ActingIsCaster { get { return true; } }
    }

    public class MM1GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return MM1MemoryHacker.GetMapTitlePair(iMap); }

        public MM1GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public MM1GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public MM1GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class MM1GameInfo : GameInfo
    {
        public MM1MapAttributes MapAttributes;
        public MMActiveEffects ActiveEffects;

        public override GameNames Game { get { return GameNames.MightAndMagic1; } }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int effects = MM1Memory.ActiveEffects;

            items.Add(new MM1GameInfoItem("Prot. Fear", (byte)ActiveEffects.ProtFear, effects + MM1EffectsOffsets.ProtFear));
            items.Add(new MM1GameInfoItem("Prot. Cold", (byte)ActiveEffects.ProtCold, effects + MM1EffectsOffsets.ProtCold));
            items.Add(new MM1GameInfoItem("Prot. Fire", (byte)ActiveEffects.ProtFire, effects + MM1EffectsOffsets.ProtFire));
            items.Add(new MM1GameInfoItem("Prot. Poison", (byte)ActiveEffects.ProtPoison, effects + MM1EffectsOffsets.ProtPoison));
            items.Add(new MM1GameInfoItem("Prot. Acid", (byte)ActiveEffects.ProtAcid, effects + MM1EffectsOffsets.ProtAcid));
            items.Add(new MM1GameInfoItem("Prot. Elec", (byte)ActiveEffects.ProtElectric, effects + MM1EffectsOffsets.ProtElectric));
            items.Add(new MM1GameInfoItem("Prot. Magic", (byte)ActiveEffects.ProtMagic, effects + MM1EffectsOffsets.ProtMagic));
            items.Add(new MM1GameInfoItem("Light", (byte)ActiveEffects.LightFactors, effects + MM1EffectsOffsets.LightFactors));
            items.Add(new MM1GameInfoItem("Leather Skin", ActiveEffects.LeatherSkin, effects + MM1EffectsOffsets.LeatherSkin));
            items.Add(new MM1GameInfoItem("Levitate", ActiveEffects.Levitate, effects + MM1EffectsOffsets.Levitate));
            items.Add(new MM1GameInfoItem("Water Walk", ActiveEffects.WaterWalk, effects + MM1EffectsOffsets.WaterWalk));
            items.Add(new MM1GameInfoItem("Guard Dog", ActiveEffects.GuardDog, effects + MM1EffectsOffsets.GuardDog));
            items.Add(new MM1GameInfoItem("Psychic Prot.", ActiveEffects.PsychicProt, effects + MM1EffectsOffsets.PsychicProt));
            items.Add(new MM1GameInfoItem("Bless", ActiveEffects.Bless, effects + MM1EffectsOffsets.Bless));
            items.Add(new MM1GameInfoItem("Invisibility", ActiveEffects.Invisibility, effects + MM1EffectsOffsets.Invisibility));
            items.Add(new MM1GameInfoItem("Shield", ActiveEffects.Shield, effects + MM1EffectsOffsets.Shield));
            items.Add(new MM1GameInfoItem("PowerShield", ActiveEffects.PowerShield, effects + MM1EffectsOffsets.PowerShield));
            items.Add(new MM1GameInfoItem("Cursed", (byte)ActiveEffects.Cursed, effects + MM1EffectsOffsets.Cursed));
            return items;
        }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int mapOffset = MM1Memory.MapGlobalAttributes;

            if (MapAttributes.Exits != null && MapAttributes.Exits.Count > 5)
            {
                items.Add(new MM1GameInfoItem("Surface Map", MapAttributes.Exits[4].MM1MapAndPoint, 
                    new OffsetList(mapOffset + MM1MapAttributeOffsets.Surface, mapOffset + MM1MapAttributeOffsets.Surface + 3), DataType.MM1MapAndPoint16));
                items.Add(new MM1GameInfoItem("North Map", (byte)MapAttributes.Exits[0].Map, mapOffset + MM1MapAttributeOffsets.ExitNorth, DataType.Map8));
                items.Add(new MM1GameInfoItem("East Map", (byte)MapAttributes.Exits[1].Map, mapOffset + MM1MapAttributeOffsets.ExitEast, DataType.Map8));
                items.Add(new MM1GameInfoItem("South Map", (byte)MapAttributes.Exits[2].Map, mapOffset + MM1MapAttributeOffsets.ExitSouth, DataType.Map8));
                items.Add(new MM1GameInfoItem("West Map", (byte)MapAttributes.Exits[3].Map, mapOffset + MM1MapAttributeOffsets.ExitWest, DataType.Map8));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int mapOffset = MM1Memory.MapGlobalAttributes;

            items.Add(new MM1GameInfoItem("Ban Teleport", MapAttributes.ForbiddenSpells, mapOffset + MM1MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x02));
            items.Add(new MM1GameInfoItem("Ban Surface", MapAttributes.ForbiddenSpells, mapOffset + MM1MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x04));
            items.Add(new MM1GameInfoItem("Ban Town Portal", MapAttributes.ForbiddenSpells, mapOffset + MM1MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x08));
            items.Add(new MM1GameInfoItem("Allow Flying", MapAttributes.FlyFlags, mapOffset + MM1MapAttributeOffsets.FlyFlags, DataType.Boolean, 0x80));
            items.Add(new MM1GameInfoItem("Dark", MapAttributes.ForbiddenSpells, mapOffset + MM1MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x01));
            items.Add(new MM1GameInfoItem("Map Index", MapAttributes.Index, MM1Memory.MapIndex, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
            items.Add(new MM1GameInfoItem("Run", SquareBytes(MapAttributes.SafestSquare), mapOffset + MM1MapAttributeOffsets.SafestSquare, DataType.Point16));
            items.Add(new MM1GameInfoItem("Surrender", SquareBytes(MapAttributes.SurrenderSquare), mapOffset + MM1MapAttributeOffsets.SurrenderSquare, DataType.Point16));
            items.Add(new MM1GameInfoItem("Enc. Size", (byte)MapAttributes.EncounterSize, mapOffset + MM1MapAttributeOffsets.EncounterSize));
            items.Add(new MM1GameInfoItem("Monster Group", (byte)MapAttributes.MonsterGroup, mapOffset + MM1MapAttributeOffsets.MonsterGroup));
            items.Add(new MM1GameInfoItem("Depth", (byte)MapAttributes.UndergroundLevel, mapOffset + MM1MapAttributeOffsets.UndergroundLevel, DataType.Depth));
            return items;
        }

        private byte[] SquareBytes(Point pt) { return new byte[] { (byte)pt.X, (byte)pt.Y }; }
    }


    public class MM1TrainingInfo : TrainingInfo
    {
        public MM1GameState State;
        public MM1Map Map;

        public override bool InTraining
        {
            get
            {
                return (
                    State.Main == MainState.Training ||
                    State.Main == MainState.TrainSuccess ||
                    State.Main == MainState.TrainingNoGold ||
                    State.Main == MainState.TrainingNoExp
                    );
            }
        }
    }

    public class MM1MapData : MMMapData
    {
        public byte StartedArenkoQuest;
        public byte[] MagicSquare;
        public byte[] GongTones;
        public byte StartedTrivia;

        public byte[] QuestBytes
        {
            get
            {
                MemoryStream stream = new MemoryStream();
                stream.WriteByte(StartedTrivia);
                stream.WriteByte(StartedArenkoQuest);
                stream.Write(GongTones, 0, GongTones.Length );
                stream.Write(MagicSquare, 0, MagicSquare.Length);
                return stream.ToArray();
            }
        }
    }

    public class MM1CharCreationInfo : CharCreationInfo
    {
        public override bool ValidValues
        {
            get
            {
                // Unmodified stat range: 3-18
                // Modified stat range: 1-19
                foreach (byte b in AttributesOriginal)
                    if (b < 3 || b > 18)
                        return false;

                foreach (byte b in AttributesModified)
                    if (b < 1 || b > 19)
                        return false;

                return true;
            }
        }
    }

    public class MM1BackpackBytes
    {
        public byte[] Items;
        public byte[] Charges;

        public MM1BackpackBytes()
        {
            Items = new byte[6];
            Charges = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                Items[i] = 0;
                Charges[i] = 0;
            }
        }
    }

    public class MM1PartyInfo : PartyInfo
    {
        public byte ScreenState;

        public MM1PartyInfo()
        {
        }

        public static MM1PartyInfo Create(byte[] bytes, byte numchars, byte acting, byte[] positions, byte state)
        {
            MM1PartyInfo info = new MM1PartyInfo();
            info.SetFromBytes(bytes, numchars, acting, positions, state);
            return info;
        }

        public void SetFromBytes(byte[] bytes, byte numchars, byte acting, byte[] positions, byte state)
        {
            Bytes = bytes;
            NumChars = numchars;
            ActingChar = (byte) (acting < 6 ? acting : 0);
            ScreenState = state;
            Positions = new byte[numchars];
            Addresses = new int[numchars];
            for (int i = 0; i < numchars; i++)
            {
                Addresses[i] = Positions[i] = (byte)((BitConverter.ToUInt16(positions, i * 2) - MM1Memory.FirstCharacterInternal) / CharacterSize);
                if (Positions[i] > numchars)
                    Addresses[i] = Positions[i] = 255;
            }
        }

        public override int CharacterSize { get { return MM1Character.SizeInBytes; } }

        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null)
                    return null;
                MemoryStream stream = new MemoryStream(6);
                for (int i = 0; i < NumChars; i++)
                {
                    stream.Write(Bytes, i * CharacterSize + MM1.Offsets.Awards, MM1.Offsets.AwardsLength);
                    stream.Write(Bytes, i * CharacterSize + MM1.Offsets.Inventory, 12);
                    stream.Write(Bytes, i * CharacterSize + MM1.Offsets.Alignment, 2);
                    stream.Write(Bytes, i * CharacterSize + MM1.Offsets.Age, 1);
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public bool FirstCharHasItem(MM1ItemIndex item)
        {
            if (Addresses.Length < 1)
                return false;

            return CharacterHasItem(Addresses[0], item);
        }

        public bool CharacterHasItem(int iCharAddress, MM1ItemIndex item)
        {
            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            MM1Inventory inventory = new MM1Inventory(Bytes, iCharAddress * CharacterSize + MM1.Offsets.Inventory);
            if (inventory.HasItem(item))
                return true;

            return false;
        }

        public bool CurrentPartyHasItem(MM1ItemIndex item)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(Addresses[i], item))
                    return true;
            }
            return false;
        }

        public override bool InCombatOrStore
        {
            get
            {
                return (ScreenState == (byte)MM1ScreenState.Combat1 ||
                ScreenState == (byte)MM1ScreenState.Combat2 ||
                ScreenState == (byte)MM1ScreenState.InStore ||
                ScreenState == (byte)MM1ScreenState.BuyingItem ||
                ScreenState == (byte)MM1ScreenState.InTemple ||
                ScreenState == (byte)MM1ScreenState.InTraining1 ||
                ScreenState == (byte)MM1ScreenState.InTraining2);
            }
        }
    }

    public class MM1SpecialSquares
    {
        public Dictionary<Point, List<MM1SpecialSquare>> Squares;

        public MM1SpecialSquares(byte[] squares, byte[] directions, ushort[] pointers, byte[] map)
        {
            Squares = new Dictionary<Point, List<MM1SpecialSquare>>(squares.Length);
            for (int i = 0; i < squares.Length; i++)
            {
                if (i >= directions.Length || i >= pointers.Length)
                    break;

                MM1SpecialSquare square = new MM1SpecialSquare(squares[i], directions[i], pointers[i]);

                if (!Squares.ContainsKey(square.Location))
                    Squares.Add(square.Location, new List<MM1SpecialSquare>(1));
                Squares[square.Location].Add(square);
            }

            for (int iMap = 0; iMap < map.Length; iMap++)
            {
                if ((map[iMap] & 0x80) == 0)
                    continue;
                Point pt = new Point(iMap % 16, iMap / 16);
                if (Squares.ContainsKey(pt))
                    continue;   // Square already accounted for; not a random encounter
                List<MM1SpecialSquare> list = new List<MM1SpecialSquare>(1);
                list.Add(new MM1SpecialSquare(pt));
                Squares.Add(pt, list);
            }
        }
    }

    public class MM1SpecialSquare
    {
        public Point Location;
        public DirectionFlags Facing;
        public bool Custom;
        public ushort ScriptPointer;

        public MM1SpecialSquare(Point pt)
        {
            Location = pt;
            Facing = DirectionFlags.All;
            Custom = true;
            ScriptPointer = 0;
        }

        public MM1SpecialSquare(byte pos, byte dir, ushort ptr)
        {
            Location = new Point(pos % 16, pos / 16);
            Facing = DirectionFlags.None;
            if ((dir & (int)MM1MapAppearance.North) != 0)
                Facing |= DirectionFlags.North;
            if ((dir & (int)MM1MapAppearance.East) != 0)
                Facing |= DirectionFlags.East;
            if ((dir & (int)MM1MapAppearance.South) != 0)
                Facing |= DirectionFlags.South;
            if ((dir & (int)MM1MapAppearance.West) != 0)
                Facing |= DirectionFlags.West;
            Custom = false;
            ScriptPointer = ptr;
        }
    }

    public class MM1SpellInfo : SpellInfo
    {
        public MM1Spell Spell;
        public MM1PartyInfo Party;
        public override bool UsesSpellLevel { get { return true; } }

        public MM1SpellInfo()
        {
            Spell = null;
            Party = null;
            Game = new MM1GameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingChar = -1;
        }
    }

    public enum MM1TreasureContainer
    {
        ClothSack = 0,
        LeatherSack = 1,
        WoodenBox = 2,
        WoodenChest = 3,
        IronBox = 4,
        IronChest = 5,
        SilverBox = 6,
        SilverChest = 7,
        GoldBox = 8,
        GoldChest = 9,
        BlackBox = 10
    }

    public class MM1CureAllInfo : CureAllInfo
    {
        public MM1Condition[] Conditions;   // 6 bytes; one per character
        public MM1Condition CasterCondition;
        public UInt16[] HitPoints;          // 12 bytes; two per character
        public UInt16[] HitPointsMax;       // 12 bytes; two per character
        public byte CasterSpellLevel;
        public UInt16 CasterSpellPoints;
        public UInt16 CasterGems;
        public MM1Class CasterClass;
        public bool InCombat;
        public bool AntiMagicZone;

        public MM1CureAllInfo()
        {
        }

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsHealer { get { return CasterClass == MM1Class.Cleric || CasterClass == MM1Class.Paladin; } }
        public override bool IsIncapacitated { get { return ((((byte)CasterCondition) & (byte)MM1Condition.UnableToCast) > 0); } }
        public override bool MagicPermitted { get { return !AntiMagicZone; } }
        public override bool Combat { get { return InCombat; } }
    }

    public class MM1SearchResults : SearchResults
    {
        MM1TreasureContainer Container;

        public MM1SearchResults(byte[] bytes, int index)
        {
            Container = (MM1TreasureContainer)bytes[index];
            Items = new List<Item>(3);

            for (int iItem = 1; iItem < 4; iItem++)
            {
                if (bytes[index + iItem] > 0)
                    Items.Add(MM1.Items[bytes[index + iItem]].Clone());
            }
            Gold = BitConverter.ToUInt16(bytes, index + 4);
            Gems = bytes[6];
        }

        public override int CompareTo(SearchResults results)
        {
            int iResult = base.CompareTo(results);
            if (iResult != 0)
                return iResult;

            if (!(results is MM1SearchResults))
                return 0;

            MM1SearchResults mm1Search = (MM1SearchResults)results;
            if (mm1Search.Container != Container)
                return 1;

            return 0;
        }

        public override string ContainerString
        {
            get
            {
                switch (Container)
                {
                    case MM1TreasureContainer.ClothSack: return "Cloth Sack";
                    case MM1TreasureContainer.LeatherSack: return "Leather Sack";
                    case MM1TreasureContainer.WoodenBox: return "Wooden Box";
                    case MM1TreasureContainer.WoodenChest: return "Wooden Chest";
                    case MM1TreasureContainer.IronBox: return "Iron Box";
                    case MM1TreasureContainer.IronChest: return "Iron Chest";
                    case MM1TreasureContainer.SilverBox: return "Silver Box";
                    case MM1TreasureContainer.SilverChest: return "Silver Chest";
                    case MM1TreasureContainer.GoldBox: return "Gold Box";
                    case MM1TreasureContainer.GoldChest: return "Gold Chest";
                    case MM1TreasureContainer.BlackBox: return "Black Box";
                    default: return "Unknown box";
                }
            }
        }
    }

    public class MM1EncounterInfo : EncounterInfo
    {
        public byte SpellCasting;
        public List<UInt16> PartyPosition;
        public byte[] PartyMelee;  // 6 bytes
        public byte[] Moved;  // 21 bytes
        public byte Friendliness;
        private Dictionary<int, Monster> m_monsters;

        public MM1EncounterInfo()
        {
            m_monsters = null;
        }

        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } set { m_monsters = value; } }
        public override bool HasTreasure { get { return SearchResults != null && !SearchResults.IsEmpty; } }
        public override bool InCombat { get { return NumTotalMonsters > 0; } }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            TurnOrderCalculator toc = new TurnOrderCalculator(HandicapTarget, HandicapValue);

            UInt16 iMin = PartyPosition.Min();

            int iNameMax = 0;

            MM1Character[] characters = new MM1Character[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = MM1Character.Create(Party.Bytes, PartyPosition[iIndex] - iMin);
                labelChars[iIndex].Melee = PartyMelee[iIndex] > 0;
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex+1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Current.ToString();
                labelChars[iIndex].SP = characters[iIndex].SpellPoints.Current.ToString();
                if (Moved[iIndex] != 1 || PreEncounter)
                {
                    toc.AddPlayerCharacter(characters[iIndex].CharName, MM1Character.GetStatModifier(characters[iIndex].Speed.Temporary, PrimaryStat.Speed).Value, iIndex);
                }

                iNameMax = Math.Max(iNameMax, labelChars[iIndex].NameLength);
            }
            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
                labelChars[iIndex].SetHPOffset(iNameMax + 2);

            List<int> monsterIndices = new List<int>(Monsters.Count);
            foreach(int i in Monsters.Keys)
                monsterIndices.Add(i);
            monsterIndices.Sort();

            foreach(int iIndex in monsterIndices)
            {
                if (!Monsters[iIndex].HasMoved || PreEncounter)
                    toc.AddMonster(Monsters[iIndex].ProperName, MM1Character.GetStatModifier(Monsters[iIndex].Speed, PrimaryStat.Speed).Value, iIndex + 6);
            }

            return toc;
        }

        public override string ExtraText { get { return String.Format("Encounter friendliness: {0}", Friendliness); } }
        public override string ExtraTitleText { get { return String.Format("Friendliness: {0}", Friendliness); } }
    }

    public static class MM1MapAttributeOffsets
    {
        public const int SafestSquare = 23;
        public const int SurrenderSquare = 26;
        public const int ExitNorth = 8;
        public const int ExitEast = 11;
        public const int ExitSouth = 14;
        public const int ExitWest = 17;
        public const int Surface = 39;
        public const int MonsterGroup = 33;
        public const int EncounterSize = 34;
        public const int UndergroundLevel = 37;
        public const int FlyFlags = 0;
        public const int ForbiddenSpells = 46;
        public const int NumSpecialSquares = 50;
        public const int SolidString1 = 30;
        public const int SolidString2 = 31;
        public const int SolidString3 = 32;
    }

    public static class MM1EffectsOffsets
    {
        public const int ProtFear = 0;
        public const int ProtCold = 1;
        public const int ProtFire = 2;
        public const int ProtPoison = 3;
        public const int ProtAcid = 4;
        public const int ProtElectric = 5;
        public const int ProtMagic = 6;
        public const int LightFactors = 7;
        public const int LeatherSkin = 8;
        public const int Levitate = 9;
        public const int WaterWalk = 10;
        public const int GuardDog = 11;
        public const int PsychicProt = 12;
        public const int Bless = 13;
        public const int Invisibility = 14;
        public const int Shield = 15;
        public const int PowerShield = 16;
        public const int Cursed = 17;
    }

    public class MM1MapAttributes : MapAttributes
    {
        public MM1MapAttributes()
        {
        }

        public override bool IsOutside(Point pt)
        {
            return (UndergroundLevel == 0);
        }
    }

    public class MM1ActiveSquares : ActiveSquares
    {
        public MM1ActiveSquares(MainForm main, int mapIndex, byte[] mapAttributes)
        {
            Main = main;
            m_iMapIndex = mapIndex;
            RawBytes = mapAttributes;
            m_bInitialized = false;
        }
    }

    public class MM1MemoryHacker : MMMemoryHacker
    {
        private MM1RosterFile m_mm1Roster = null;

        public MM1MemoryHacker()
        {
            m_game = GameNames.MightAndMagic1;
        }

        public override int GetLightDistance()
        {
            if (ReadByte(MM1Memory.ActiveEffects + MM1EffectsOffsets.LightFactors) > 0)
                return 4;
            if ((ReadByte(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ForbiddenSpells) & 0x01) == 0)
                return 4;
            return 0;
        }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return MM1Character.GetStatModifier(value, stat);
        }

        public override byte[] MainSearch { get { return MM1Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return MM1Memory.Guesses; } }
        public override PrimaryStat[] StatOrder { get { return StatOrderIMPESAL; } }

        public override void RefreshRollScreen()
        {
            // Pick "ROBBER" (6) and retry (Escape)
            SendKeysToDOSBox(new Keys[] { Keys.D6, Keys.Escape });
        }


        public static MainState GetMM1MenuState(byte[] castState, byte rollState, byte[] rollState2, UInt16 displayState)
        {
            MainState state = MainState.Unknown;
            int rs2 = rollState2[0] << 8 | rollState2[1];
            switch (castState[0] << 8 | castState[1])
            {
                case 0x220a:
                    switch (rollState)
                    {
                        case 58:
                            if (rs2 == 0xf821)
                                state = MainState.Main;
                            break;
                        case 68:
                            if (rs2 == 0x6e22)
                                state = MainState.CreateSelectClass;
                            break;
                        default:
                            break;
                    }
                    break;
                case 0x250D:
                    switch (rollState)
                    {
                        case 56:
                            if (rs2 == 0x8423)
                                state = MainState.CreateSelectRace;
                            break;
                        case 74:
                            if (rs2 == 0x3a23)
                                state = MainState.CreateSelectAlignment;
                            break;
                        case 60:
                            if (rs2 == 0xB81E)
                                state = MainState.CreateSelectSex;
                            break;
                        default:
                            break;
                    }
                    break;
                case 0x2511:
                    if (rollState == 74 && rs2 == 0x6B23)
                        state = MainState.SaveCharacter;
                    break;
                case 0x1C14:
                    if (rollState == 76 && rs2 == 0x8c16)
                        state = MainState.Training;
                    break;
                case 0x1316:
                case 0x1416:
                    if (rollState == 64)
                    {
                        if (rs2 == 0xAE13)
                            state = MainState.TrainSuccess;
                        else if (rs2 == 0x7514)
                            state = MainState.TrainingNoGold;
                    }
                    break;
                case 0x1E14:
                case 0x1D14:
                case 0x1F14:
                    if (rs2 == 0xAC16 || rs2 == 0x8C16)
                        state = MainState.TrainingNoExp;
                    break;
                case 0x1C18:
                    if (rollState == 56 && rs2 == 0x0c22)
                        state = MainState.Inn;
                    else if (rollState == 56 && rs2 == 0x8423)
                        state = MainState.CreateSelectRace;
                    else
                        state = MainState.Adventuring;
                    break;
                case 0x1B14:
                    switch (rollState2[1])
                    {
                        case 0x2C: state = MainState.SelectTownPortal; break;
                        case 0xCA:
                        case 0xCB:
                        case 0xCC:
                        case 0x48: state = MainState.AfterTownPortal; break;
                        case 0x21:
                        case 0x03: state = MainState.Unknown; break;
                        case 0x39:
                        case 0x2B: state = MainState.CastLevel; break;
                        default: state = MainState.Unknown; break;
                    }
                    break;
                case 0x1B15:
                    switch (rs2)
                    {
                        case 0x922A: state = MainState.Unknown; break;// remove cursed item
                        default: state = MainState.CastNumber; break;
                    }
                    break;
                   
                case 0x1017:
                case 0x1018: state = MainState.QueryTarget; break;
                case 0x1814: state = MainState.EnterFlyLetter; break;
                case 0x1815: state = MainState.EnterFlyNumber; break;
                case 0x2717:
                    switch (displayState)
                    {
                        case 0x1805: state = MainState.PreCombat; break;
                        default: state = MainState.PressEnter; break;
                    }
                    break;
                case 0x0000: state = MainState.Opening; break;
                case 0x1C0D: state = MainState.Opening2; break;
                case 0x1D18:
                    switch (displayState)
                    {
                        case 0x220C: state = MainState.SignIn; break;
                        default: state = MainState.MainMenu; break;
                    }
                    break;
                default:
                    switch (displayState)
                    {
                        case 0x1805: state = MainState.PreCombat; break;
                        case 0x2889: state = MainState.CharacterScreen; break;
                        case 0x27fd: state = MainState.QuickRef; break;
                        case 0x2116: state = MainState.Adventuring; break;
                        case 0x487c: state = MainState.Adventuring; break;
                        default:
                            if ((displayState & 0xf000) == 0xc000 ||
                                (displayState & 0xfc00) == 0x1000
                               )
                                state = MainState.Adventuring;
                            else
                                state = MainState.Unknown;
                            break;
                    }
                    break;
            }
            if (state == MainState.Unknown && castState[1] == 0x0d && castState[0] >= 0x16 && castState[0] <= 0x24 && rollState >= 46 && rollState <= 74)
                state = MainState.CreateSelectName;

            return state;
        }

        private MM1GameState ReadMM1GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as MM1GameState;     // Don't spam the game state from different windows

            byte[] temp = new byte[2];
            MM1GameState state = new MM1GameState();
            byte[] castState = new byte[2];
            byte[] rollState = new byte[1];
            byte[] rollState2 = new byte[2];
            UInt16 displayState = 0;
            if (m_block != null)
            {
                ReadOffset(MM1Memory.CastState, castState);
                ReadOffset(MM1Memory.RollState, rollState);
                ReadOffset(MM1Memory.RollState2, rollState2);
                displayState = ReadUInt16(MM1Memory.DisplayState);
            }
            state.Main = GetMM1MenuState(castState, rollState[0], rollState2, displayState);

            //NotInCombat    48  01001000
            //NotInCombat    21  00100001
            //CharScreen     28  00101000
            //QuickRef       27  00100111 (combat or noncombat)
            //AnswerYN       CA  11001010
            //Shopping       16  00010110
            //Selling        3C  00111100
            //Buying         16  00010110
            //CastNoncombat  2B  00101011
            //FoundTreasure  19  00011001
            //PreEncounter   18  00011000
            //InCombat       36  00110110
            //ViewCharCombat 37  00110111
            //CastInCombat   39  00111001
            //MonsterTurn    30  00110000

            byte bFlags = ReadByte(MM1Memory.CombatFlags);
            state.InCombat = (bFlags == 0x18 || bFlags == 0x30 || bFlags == 0x36 || bFlags == 0x37 || bFlags == 0x39);
            if (state.InCombat)
            {
                byte bNumMonsters = ReadByte(MM1Memory.NumberOfMonsters);
                if (bNumMonsters == 0 || bNumMonsters > 15)
                    state.InCombat = false;
            }

            state.Location = GetLocationForce();

            m_gsCurrent = state;
            return state;
        }

        private MM1PartyInfo ReadMM1PartyInfo()
        {
            byte numChars = ReadByte(MM1Memory.NumChars);
            if (m_block == null)
                return null;
            if (numChars == 0)
                return MM1PartyInfo.Create(new byte[0], 0, 0, new byte[0], 0);
            if (numChars < 7)
            {
                MemoryBytes bytes = ReadOffset(MM1Memory.PartyInfo, MM1Character.SizeInBytes * numChars);
                if (bytes == null)
                    return null;
                byte state = ReadByte(MM1Memory.ScreenState + 1);
                MemoryBytes positions = ReadOffset(MM1Memory.PartyPositions, 12);
                if (positions == null)
                    return null;
                int iActingChar = (ReadUInt16(MM1Memory.ActingCharacterOffset) - MM1Memory.FirstCharacterInternal) / MM1Character.SizeInBytes;
                return MM1PartyInfo.Create(bytes, numChars, (byte)iActingChar, positions, state);
            }
            return null;
        }

        private MM1MapInfo ReadMM1MapInfo()
        {
            MemoryBytes bytes = ReadOffset(MM1Memory.Map, 512);
            if (bytes == null)
                return null;
            return new MM1MapInfo(bytes);
        }

        public override CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterIndex >= partyAddresses.Length)
                return null;

            MM1CureAllInfo info = new MM1CureAllInfo();
            MM1PartyInfo party = ReadMM1PartyInfo();

            byte[] temp = new byte[2];
            ReadOffset(MM1Memory.ScreenState, temp);
            info.InCombat = (temp[1] == 20 || temp[1] == 23 || temp[1] == 33);
            ReadOffset(MM1Memory.Location, temp);
            MM1MapInfo map = ReadMM1MapInfo();
            info.AntiMagicZone = map.Behavior[temp[0], temp[1]].HasFlag(MM1MapBehavior.AntiMagic);

            info.Conditions = new MM1Condition[party.NumChars];
            info.HitPoints = new UInt16[party.NumChars];
            info.HitPointsMax = new UInt16[party.NumChars];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                info.Conditions[i] = (MM1Condition)party.Bytes[partyAddresses[i] * party.CharacterSize + MM1.Offsets.Condition];
                info.HitPoints[i] = BitConverter.ToUInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + MM1.Offsets.CurrentHP);
                info.HitPointsMax[i] = BitConverter.ToUInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + MM1.Offsets.MaxHPMod);
            }

            int iCasterAddress = partyAddresses[iCasterIndex];
            info.CasterGems = BitConverter.ToUInt16(party.Bytes, iCasterAddress * party.CharacterSize + MM1.Offsets.Gems);
            info.CasterSpellPoints = BitConverter.ToUInt16(party.Bytes, iCasterAddress * party.CharacterSize + MM1.Offsets.CurrentSP);
            info.CasterSpellLevel = party.Bytes[iCasterAddress * party.CharacterSize + MM1.Offsets.SpellLevelMod];
            info.CasterClass = (MM1Class)party.Bytes[iCasterAddress * party.CharacterSize + MM1.Offsets.Class];
            info.CasterCondition = info.Conditions[iCasterIndex];
            return info;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterIndex >= partyAddresses.Length)
                return;

            MM1CureAllInfo info = cureAll as MM1CureAllInfo;
            long pWritten;
            byte[] temp = BitConverter.GetBytes(info.CasterGems);
            int iCasterAddress = partyAddresses[iCasterIndex];
            WriteOffset(MM1Memory.PartyInfo + (iCasterAddress * MM1Character.SizeInBytes + MM1.Offsets.Gems), temp);
            temp = BitConverter.GetBytes(info.CasterSpellPoints);
            WriteOffset(MM1Memory.PartyInfo + (iCasterAddress * MM1Character.SizeInBytes + MM1.Offsets.CurrentSP), temp);
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                temp[0] = (byte)info.Conditions[i];
                WriteOffset(MM1Memory.PartyInfo + (partyAddresses[i] * MM1Character.SizeInBytes + MM1.Offsets.Condition), temp, 1, out pWritten);
                temp = BitConverter.GetBytes(info.HitPoints[i]);
                WriteOffset(MM1Memory.PartyInfo + (partyAddresses[i] * MM1Character.SizeInBytes + MM1.Offsets.CurrentHP), temp);
            }
        }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            MM1SpellInfo info = new MM1SpellInfo();
            info.Game = ReadMM1GameState();
            byte bSpell = ReadByte(MM1Memory.CastingSpellNoncombat);
            if (bSpell < MM1.Spells.Count)
                info.Spell = MM1.Spells[bSpell];
            bSpell = ReadByte(MM1Memory.CastingSpellCombat);
            if (bSpell < MM1.Spells.Count)
                info.Spell = MM1.Spells[bSpell];
            if (info.Game.Main != MainState.CastLevel && info.Game.Main != MainState.CastNumber)
                return info;
                    
            info.Party = ReadMM1PartyInfo();
            info.Game.ActingChar = info.Party.ActingChar;
            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadMM1PartyInfo();
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            MM1CharCreationInfo info = new MM1CharCreationInfo();
            info.AttributesModified = ReadOffset(MM1Memory.CreationStatsModified, 7);
            info.AttributesOriginal = ReadOffset(MM1Memory.CreationStatsOriginal, 7);
            info.State = ReadMM1GameState();
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            WriteOffset(MM1Memory.CreationStatsOriginal, info.AttributesOriginal);
            WriteOffset(MM1Memory.CreationStatsModified, info.AttributesModified);
            return true;
        }

        public override TrainingInfo GetTrainingInfo()
        {
            if (!IsValid)
                return null;

            MM1TrainingInfo info = new MM1TrainingInfo();
            info.State = ReadMM1GameState();
            info.Party = ReadMM1PartyInfo();

            byte[] bytes = new byte[1];
            info.MapIndex = GetCurrentMapIndex();
            info.Map = (MM1Map)info.MapIndex;
            return info;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            if (info is MM1TrainingInfo)
            {
                MM1TrainingInfo mm1Info = info as MM1TrainingInfo;
                long pWritten;
                byte[] hp = new byte[6];
                Buffer.BlockCopy(mm1Info.Party.Bytes, mm1Info.Party.ActingChar * mm1Info.Party.CharacterSize + 51, hp, 0, 6);
                return WriteOffset(MM1Memory.PartyInfo + (mm1Info.Party.ActingChar * mm1Info.Party.CharacterSize + 51), hp, 6, out pWritten);
            }

            return false;
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            byte[] temp = new byte[7];
            MM1EncounterInfo info = new MM1EncounterInfo();
            info.NumTotalMonsters = 0;
            long pRead;

            ReadOffset(MM1Memory.SearchResults, temp);
            info.SearchResults = new MM1SearchResults(temp, 0);

            ReadOffset(MM1Memory.NumberOfMonsters, temp, 1, out pRead);
            if ((uint)pRead != 1 || temp[0] == 0 || temp[0] > 15)
                return info;

            info.NumTotalMonsters = temp[0];

            MemoryStream all = new MemoryStream(32 * 15 + 15 + 21 + 18);
            byte screenState;

            ReadOffset(MM1Memory.EncounterState, temp, 1, out pRead);
            if (temp[0] == 0x29 || temp[0] == 0x32 || temp[0] == 0x3F)
            {
                info.NumTotalMonsters = 0;
                return info;
            }

            all.WriteByte((byte) info.NumTotalMonsters);
            all.WriteByte(temp[0]);

            info.Friendliness = ReadByte(MM1Memory.EncounterFriendliness);
            all.WriteByte(info.Friendliness);

            screenState = ReadByte(MM1Memory.ScreenState);
            info.PreEncounter = (screenState == 22);

            MemoryBytes statusArray = ReadOffset(MM1Memory.MonsterCombatStatus, 15);
            if (statusArray == null)
                return null;
            all.Write(statusArray, 0, statusArray.Length);

            MemoryBytes monsters = ReadOffset(MM1Memory.EncounterList, 32 * 15);
            if (monsters == null)
                return null;
            all.Write(monsters, 0, monsters.Length);
            info.Moved = ReadOffset(MM1Memory.PartyHasMovedThisRound, 21);
            all.Write(info.Moved, 0, info.Moved.Length);
            MemoryBytes monsterHP = ReadOffset(MM1Memory.MonsterHP, 15);
            if (monsterHP == null)
                return null;
            all.Write(monsterHP, 0, monsterHP.Length);

            MemoryBytes monsterpos = ReadOffset(MM1Memory.MonsterCombatOrder, 30);
            if (monsterpos == null)
                return null;

            info.Monsters = new Dictionary<int, Monster>(info.NumTotalMonsters);
            int iAlive = 0;
            for (int i = 0; i < 15; i++)
            {
                MM1MonsterCombatStatus status = (MM1MonsterCombatStatus)statusArray[i];

                if (screenState != 22)  // 22 seems to mean "on the pre-encounter screen"
                {
                    // Don't add dead monsters during normal combat
                    if (status == MM1MonsterCombatStatus.Dead)
                        continue;
                    int iAddress = BitConverter.ToUInt16(monsterpos, iAlive * 2) - MM1Memory.FirstMonsterInternal;
                    if (iAddress < 0 || iAddress > (14 * 32) || iAddress % 32 != 0)
                    {
                        // What's in the monsterpos array isn't what we're expecting; bail!
                        info.NumTotalMonsters = 0;
                        return info;
                    }
                    MM1Monster mm1Monster = new MM1Monster(monsters, iAddress, monsterHP[iAddress / 32], status, info.Moved[i + 6] > 0);
                    mm1Monster.EncounterIndex = i;
                    info.Monsters.Add(i, mm1Monster);
                    iAlive++;
                }
                else
                {
                    MM1Monster mm1Monster = new MM1Monster(monsters, i * 32, status, info.Moved[i + 6] > 0);
                    mm1Monster.EncounterIndex = i;
                    info.Monsters.Add(i, mm1Monster);
                }

                if (info.Monsters.Count >= info.NumTotalMonsters)
                    break;
            }

            byte bRound = ReadByte(MM1Memory.CombatRound);
            info.Round = info.PreEncounter ? 0 : bRound;
            all.WriteByte(bRound);
            info.HandicapTarget = ReadByte(MM1Memory.Handicap);
            info.HandicapValue = ReadByte(MM1Memory.Handicap+1);
            all.WriteByte((byte)info.HandicapTarget);
            all.WriteByte((byte)info.HandicapValue);
            byte bSpell = ReadByte(MM1Memory.SpellBeingCast);
            info.SpellCasting = bSpell;
            all.WriteByte(bSpell);
            byte bMelee = (byte) (screenState == 22 ? 0 : ReadByte(MM1Memory.NumberofMeleeMonsters));
            info.NumMeleeMonsters = bMelee;
            all.WriteByte((byte)info.NumMeleeMonsters);
            info.PartyMelee = new byte[6] {0,0,0,0,0,0};
            if (screenState != 22)
                ReadOffset(MM1Memory.PartyMeleeStatus, info.PartyMelee);
            all.Write(info.PartyMelee, 0, info.PartyMelee.Length);

            info.PartyPosition = new List<UInt16>(6);
            MemoryBytes positions = ReadOffset(MM1Memory.PartyPositions, 12);
            if (positions == null)
                return null;

            for (int i = 0; i < 6; i++)
                info.PartyPosition.Add(BitConverter.ToUInt16(positions, i * 2));
            all.Write(positions, 0, positions.Length);

            info.PartyLocation = GetPartyPosition();
            all.WriteByte((byte)info.PartyLocation.X);
            all.WriteByte((byte)info.PartyLocation.Y);

            info.Party = ReadMM1PartyInfo();
            info.AllBytes = all.ToArray();
            all.Close();
            return info;
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes pos = ReadOffset(MM1Memory.Location, 2);
            if (pos == null)
                return Global.NullPoint;
            return new Point(pos.Bytes[0], pos.Bytes[1]);
        }

        private LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(GetPartyPosition());
            switch(ReadByte(MM1Memory.Facing))
            {
                case 0xc0:
                    info.Facing = Direction.Up;
                    break;
                case 0x30:
                    info.Facing = Direction.Right;
                    break;
                case 0x0c:
                    info.Facing = Direction.Down;
                    break;
                case 0x03:
                    info.Facing = Direction.Left;
                    break;
                default:
                    break;
            }
            info.MapIndex = GetCurrentMapIndex();

            info.InInn = InSpots(new MapXY((MM1Map) info.MapIndex, info.PrimaryCoordinates.X, info.PrimaryCoordinates.Y), MM1.Spots.FullInns);

            info.CanUseBag = info.InInn || Global.Cheats;

            byte key = ReadByte(MM1Memory.LastKeypress);
            switch (key)
            {
                case 0xC8: info.LastKeypress = Keys.Up; break;
                case 0xD0: info.LastKeypress = Keys.Down; break;
                case 62: info.LastKeypress = Keys.B; break;
                case 13: info.LastKeypress = Keys.Enter; break;
                default: info.LastKeypress = (Keys)key; break;
            }
            switch ((MM1SpellIndex)ReadByte(MM1Memory.CastingSpellNoncombat))
            {
                case MM1SpellIndex.Teleport:info.LastSpellNonCombat = MMGenericSpell.Teleport; break;
                case MM1SpellIndex.Etherealize: info.LastSpellNonCombat = MMGenericSpell.Etherealize; break;
                case MM1SpellIndex.Fly: info.LastSpellNonCombat = MMGenericSpell.Fly; break;
                case MM1SpellIndex.Jump: info.LastSpellNonCombat = MMGenericSpell.Jump; break;
                case MM1SpellIndex.Shelter: info.LastSpellNonCombat = MMGenericSpell.Shelter; break;
                case MM1SpellIndex.Surface: info.LastSpellNonCombat = MMGenericSpell.Surface; break;
                case MM1SpellIndex.TownPortal: info.LastSpellNonCombat = MMGenericSpell.TownPortal; break;
                default: info.LastSpellNonCombat = MMGenericSpell.None; break;
            }

            info.Outside = (ReadByte(MM1Memory.InsideOutside) == 2);

            info.NumChars = ReadByte(MM1Memory.NumChars);
            info.LightDistance = GetLightDistance();
            return info;
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[2] { (byte) ptLocation.X, (byte) ptLocation.Y };
            return WriteOffset(MM1Memory.Location, bytes);
        }

        public override string StatToolTip(int iIndex, int iValue)
        {
            switch (iIndex)
            {
                case 0: return String.Format("Intellect gives a bonus ({0}: {1}) per level to the number of spell points for Sorcerers and Archers.  The base value is 3 SP/level.", iValue, GetStatModString(iValue, PrimaryStat.Intellect));
                case 1: return String.Format("Might gives a bonus ({0}: {1}) to damage inflicted with melee weapons in combat.  The base value is 0.", iValue, GetStatModString(iValue, PrimaryStat.Might));
                case 2: return String.Format("Personality gives a bonus ({0}: {1}) per level to the number of spell points for Clerics and Paladins.  The base value is 3 SP/level.", iValue, GetStatModString(iValue, PrimaryStat.Personality));
                case 3: return String.Format("Endurance gives a bonus ({0}: {1}) to the number of hit points gained per level.  The base values are Sorcerer:6, Cleric:8, Robber:8, Paladin:10, Archer:10, Knight:12", iValue, GetStatModString(iValue, PrimaryStat.Endurance));
                case 4: return String.Format("Speed gives a bonus ({0}: {1}) to armor class.  The base value is 0.  Speed (along with the per-round combat handicap) also determines the order of actions in combat.", iValue, GetStatModString(iValue, PrimaryStat.Speed));
                case 5: return String.Format("Accuracy gives a bonus ({0}: {1}) to hit in combat.  The base value is 0.", iValue, GetStatModString(iValue, PrimaryStat.Accuracy));
                case 6: return String.Format("Luck gives a bonus ({0}: {1}) to the thievery skill and may have other miscellaneous effects in-game.  The base thievery value is 50 + 2/level for robbers and 1 + 2/level for other classes.", iValue, GetStatModString(iValue, PrimaryStat.Luck));
            }
            return "";
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bInsufficientLevel = false;
            bool bInsufficientSP = false;
            bool bInsufficientGems = false;

            if (!(cureAllInfo is MM1CureAllInfo))
                return CureAllResult.Error;

            MM1CureAllInfo info = cureAllInfo as MM1CureAllInfo;

            // Okay, let's start curing!  Fix the conditions that rest won't fix first
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i].HasFlag(MM1Condition.Eradicated))
                    continue;   // Don't deal with eradication; make the player do that manually

                if (info.Conditions[i].HasFlag(MM1Condition.Dead))
                {
                    if (info.CasterGems >= 4)
                    {
                        if (info.CasterSpellPoints >= 6)
                        {
                            if (info.CasterSpellLevel >= 6)
                            {
                                info.CasterGems -= 4;
                                info.CasterSpellPoints -= 6;
                                info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Unconscious;
                                if (!info.Conditions[i].HasFlag(MM1Condition.Stone))
                                {
                                    info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Dead;
                                    info.HitPoints[i] = 1;
                                }
                            }
                            else
                                bInsufficientLevel = true;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bInsufficientGems = true;
                }
                if (info.Conditions[i].HasFlag(MM1Condition.Stone))
                {
                    if (info.CasterGems >= 4)
                    {
                        if (info.CasterSpellPoints >= 6)
                        {
                            if (info.CasterSpellLevel >= 6)
                            {
                                info.CasterGems -= 4;
                                info.CasterSpellPoints -= 6;
                                info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Paralyzed;
                                if (!info.Conditions[i].HasFlag(MM1Condition.Dead))
                                {
                                    info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Stone;
                                    info.HitPoints[i] = 1;
                                }
                            }
                            else
                                bInsufficientLevel = true;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bInsufficientGems = true;
                }
                if (info.Conditions[i].HasFlag(MM1Condition.Poisoned))
                {
                    if (info.CasterSpellPoints >= 4)
                    {
                        if (info.CasterSpellLevel >= 4)
                        {
                            info.CasterSpellPoints -= 4;
                            info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Poisoned;
                        }
                        else
                            bInsufficientLevel = true;
                    }
                    else
                        bInsufficientSP = true;
                }
                if (info.Conditions[i].HasFlag(MM1Condition.Diseased))
                {
                    if (info.CasterSpellPoints >= 4)
                    {
                        if (info.CasterSpellLevel >= 4)
                        {
                            info.CasterSpellPoints -= 4;
                            info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Diseased;
                        }
                        else
                            bInsufficientLevel = true;
                    }
                    else
                        bInsufficientSP = true;
                }
            }
            // Now we fix blind, paralyzed, and asleep, if there are leftover spell points
            bool bAnySleep = false;
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i].HasFlag(MM1Condition.Blinded))
                {
                    if (info.CasterSpellPoints >= 3)
                    {
                        if (info.CasterSpellLevel >= 3)
                        {
                            info.CasterSpellPoints -= 3;
                            info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Blinded;
                        }
                        else
                            bInsufficientLevel = true;
                    }
                    else
                        bInsufficientSP = true;
                }
                if (info.Conditions[i].HasFlag(MM1Condition.Paralyzed) && !info.Conditions[i].HasFlag(MM1Condition.Stone))
                {
                    if (info.CasterSpellPoints >= 3)
                    {
                        if (info.CasterSpellLevel >= 3)
                        {
                            info.CasterSpellPoints -= 3;
                            info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Paralyzed;
                        }
                        else
                            bInsufficientLevel = true;
                    }
                    else
                        bInsufficientSP = true;
                }
                if (info.Conditions[i].HasFlag(MM1Condition.Asleep))
                {
                    if (info.CasterSpellPoints >= 1)
                    {
                        if (info.CasterSpellLevel >= 1)
                        {
                            bAnySleep = true;
                            info.Conditions[i] = info.Conditions[i] & ~MM1Condition.Asleep;
                        }
                        else
                            bInsufficientLevel = true;
                    }
                    else
                        bInsufficientSP = true;
                }
            }
            if (bAnySleep && info.CasterSpellPoints > 0)
                info.CasterSpellPoints -= 1;

            // Restore HP with any remaining spell points
            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.CasterSpellLevel >= 1) // First Aid, 1 SP to heal 8 HP
                    {
                        while (info.HitPoints[i] < info.HitPointsMax[i] - 7)
                        {
                            if (info.CasterSpellPoints >= 1)
                            {
                                info.CasterSpellPoints--;
                                info.HitPoints[i] += 8;
                                if (info.HitPoints[i] > 0)
                                    info.Conditions[i] = info.Conditions[i] & (~MM1Condition.Unconscious);
                            }
                            else
                            {
                                bInsufficientSP = true;
                                break;
                            }
                        }
                    }
                    else
                        bInsufficientLevel = true;
                }
            }

            if (bInsufficientLevel)
                return CureAllResult.NoSpellLevel;
            if (bInsufficientSP)
                return CureAllResult.NoSpellPoints;
            if (bInsufficientGems)
                return CureAllResult.NoGems;
            return CureAllResult.Success;
        }

        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "Fear Resist: 70, Sleep Resist: 25";
                case GenericRace.Elf: return "INT+1, ACY+1, MGT-1, END-1, Fear Res: 70";
                case GenericRace.Dwarf: return "END+1, LUC+1, INT-1, SPD-1, Poison Res: 25";
                case GenericRace.Gnome: return "LUC+2, SPD-1, ACY-1, Magic Res: 20";
                case GenericRace.HalfOrc: return "MGT+1, END+1, INT-1, PER-1, LUC-1, Sleep Res: 50";
                default: return "Unknown";
            }
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(MM1Memory.PartyInfo + (iAddress * MM1Character.SizeInBytes), bytes);
        }

        public override bool SetMonsterInfo(int iIndex, MonsterBasicInfo info)
        {
            if (!IsValid)
                return false;

            if (info.Game != GameNames.MightAndMagic1)
                return false;

            long pWritten;
            byte[] bytes = info.GetBytes();
            WriteOffset(MM1Memory.EncounterList + (iIndex * 32), bytes);

            bytes[0] = (byte) info.CurrentHP;
            WriteOffset(MM1Memory.MonsterHP + iIndex, bytes, 1, out pWritten);

            bytes[0] = (byte) MM1Monster.GetCondition(info.Condition);
            WriteOffset(MM1Memory.MonsterCombatStatus + iIndex, bytes, 1, out pWritten);

            return ((int)pWritten == 1);
        }

        public override MonsterBasicInfo GetMonsterInfo(int iIndex)
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM1Memory.MonsterData + (32 * iIndex), 32);
            if (bytes == null)
                return null;
            return new MonsterBasicInfo(m_game, new MM1Monster(bytes, iIndex, MM1MonsterCombatStatus.Good, false), null);
        }

        public override bool TradeBackpacks(int iCharAddress1, int iCharAddress2)
        {
            // Equipped: bytes 64-69
            // Backpack: bytes 70-75
            // Charges:  bytes 76-81, 82-87

            byte[] backpack1 = new byte[6];
            byte[] charges1 = new byte[6];
            byte[] backpack2 = new byte[6];
            byte[] charges2 = new byte[6];

            long iAddress1 = MM1Memory.PartyInfo + (iCharAddress1 * MM1Character.SizeInBytes);
            long iAddress2 = MM1Memory.PartyInfo + (iCharAddress2 * MM1Character.SizeInBytes);

            bool bResult = ReadOffset(iAddress1 + 70, backpack1);
            bResult = bResult && ReadOffset(iAddress2 + 70, backpack2);
            bResult = bResult && ReadOffset(iAddress1 + 82, charges1);
            bResult = bResult && ReadOffset(iAddress2 + 82, charges2);

            if (!bResult)
                return bResult;

            bResult = bResult && WriteOffset(iAddress2 + 70, backpack1);
            bResult = bResult && WriteOffset(iAddress1 + 70, backpack2);
            bResult = bResult && WriteOffset(iAddress2 + 82, charges1);
            bResult = bResult && WriteOffset(iAddress1 + 82, charges2);

            return bResult;
        }

        public override GameState GetGameState()
        {
            return ReadMM1GameState();
        }

        public override int MaxInventoryChar { get { return 18; } }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new MM1RosterFile(Global.CombineRoster(Game), bSilent);
        }

        public override bool ValidateRosterFile()
        {
            // Always reload the roster file, in case the player deleted characters or otherwise putzed with the file
            m_mm1Roster = CreateRoster(true) as MM1RosterFile;
            if (!m_mm1Roster.Valid)
                BrowseRosterFile();

            return m_mm1Roster.Valid;
        }

        public override bool BrowseRosterFile(string strTitle = null)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = Global.CombineRoster(Game);
            ofd.InitialDirectory = Games.RosterPath(Game);
            ofd.Filter = "Roster Files|*.DTA|All Files|*.*";
            if (String.IsNullOrWhiteSpace(strTitle))
                ofd.Title = "You must select your ROSTER.DTA file in order to use the Bag of Holding";
            else
                ofd.Title = strTitle;
            while (true)
            {
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return false;

                m_mm1Roster = new MM1RosterFile(ofd.FileName, false);
                if (m_mm1Roster.Valid)
                {
                    Games.SetRosterFile(GameNames.MightAndMagic1, Path.GetFileName(m_mm1Roster.FileName));
                    Games.SetRosterPath(GameNames.MightAndMagic1, Path.GetDirectoryName(m_mm1Roster.FileName));
                    break;
                }
            }
            return true;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!ValidateRosterFile())
                return -1;

            byte[] bytes = new byte[MM1Character.SizeInBytes];
            while (iStart >= 0)
            {
                MM1Character mm1Char = null;
                if (iStart < m_mm1Roster.Chars.Count)
                    mm1Char = MM1Character.Create(m_mm1Roster.Chars[iStart].Bytes, 0, true);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (mm1Char != null && mm1Char.Name == "INVENTORY")
                            return iStart;
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (mm1Char != null && mm1Char.Name == "INVENTORY")
                            return iStart;
                        if (mm1Char == null || String.IsNullOrWhiteSpace(mm1Char.Name))
                        {
                            // No character in the roster at this position; make a new one;
                            //while (m_mm1Roster.Chars.Count <= iStart)
                            //{
                            //    m_mm1Roster.Chars.Add(new MM1CharAndBytes(MM1Character.GetInventoryCharBytes(), 0, 1, false));
                            //    m_mm1Roster.SaveCharBytes(m_mm1Roster.Chars.Count-1);
                            //}
                            m_mm1Roster.Chars[iStart].Bytes = MM1Character.GetInventoryCharBytes();
                            m_mm1Roster.Chars[iStart].Town = 1;
                            m_mm1Roster.SaveCharBytes(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (mm1Char == null || mm1Char.Name == "" || mm1Char.Name == "INVENTORY")
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            return -1;
        }

        public override SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items)
        {
            if (iRosterPosition < 0 || iRosterPosition > 17)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= m_mm1Roster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            MM1BackpackBytes bytes = GetBackpackBytes(items);

            byte[] bytesChar = m_mm1Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            Buffer.BlockCopy(bytes.Items, 0, bytesChar, 70, 6);
            Buffer.BlockCopy(bytes.Charges, 0, bytesChar, 82, 6);
            m_mm1Roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        private MM1BackpackBytes GetBackpackBytes(List<Item> items)
        {
            MM1BackpackBytes bytes = new MM1BackpackBytes();

            for (int i = 0; i < items.Count; i++)
            {
                if (!(items[i] is MM1Item))
                    continue;
                MM1Item mm1Item = items[i] as MM1Item;

                bytes.Items[i] = (byte)mm1Item.Index;
                bytes.Charges[i] = mm1Item.m_iChargesCurrent;
            }
            return bytes;
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            MM1BackpackBytes bytes = GetBackpackBytes(items);

            WriteOffset(MM1Memory.PartyInfo + (iCharAddress * MM1Character.SizeInBytes) + 70, bytes.Items);
            WriteOffset(MM1Memory.PartyInfo + (iCharAddress * MM1Character.SizeInBytes) + 82, bytes.Charges);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 17)
                return null;

            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition >= m_mm1Roster.Chars.Count)
                return null;

            byte[] bytesChar = m_mm1Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            List<Item> backpack = new List<Item>(6);
            for (int i = 0; i < 6; i++)
                backpack.Add(MM1Item.FromBagBytes(new byte[] { bytesChar[i+70], bytesChar[i + 82] }));

            return backpack;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            if (!IsValid)
                return null;

            List<Item> backpack = new List<Item>(6);

            MM1BackpackBytes bytes = new MM1BackpackBytes();
            ReadOffset(MM1Memory.PartyInfo + (iCharAddress * MM1Character.SizeInBytes) + 70, bytes.Items);
            ReadOffset(MM1Memory.PartyInfo + (iCharAddress * MM1Character.SizeInBytes) + 82, bytes.Charges);

            for (int i = 0; i < 6; i++)
                backpack.Add(MM1Item.FromBagBytes(new byte[] { bytes.Items[i], bytes.Charges[i] }));

            return backpack;
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            MM1MapData data = new MM1MapData();

            data.Appearance = ReadOffset(MM1Memory.MapAppearance, 256);
            data.Attributes = ReadOffset(MM1Memory.MapAttributes, 256);
            data.Outside = ReadByte(MM1Memory.InsideOutside) == 2;
            data.StartedArenkoQuest = ReadByte(MM1Memory.StartedArenkoQuest);
            data.MagicSquare = ReadOffset(MM1Memory.MagicSquareNumbers, 9);
            data.GongTones = ReadOffset(MM1Memory.GongTones, 3);
            data.StartedTrivia = ReadByte(MM1Memory.TriviaChance);

            if (bIncludeStrings)
                data.ScriptInfo = GetScriptInfo() as MMScriptInfo;

            return data;
        }

        public override List<ScriptString> GetScriptStrings()
        {
            List<ScriptString> strings = new List<ScriptString>(0);

            if (!IsValid)
                return strings;

            MemoryBytes bytes = ReadOffset(MM1Memory.NumSpecialSquares + 1 + (ReadByte(MM1Memory.NumSpecialSquares) * 4), 2300);
            if (bytes == null)
                return strings;

            int iCurrent = 0;
            int iStart = 0;

            StringBuilder sb = new StringBuilder();

            while (iCurrent < bytes.Length)
            {
                if (bytes[iCurrent] >= 0x20 && bytes[iCurrent] <= 126)
                    sb.Append((char)bytes[iCurrent]);
                else switch (bytes[iCurrent])
                    {
                        case 10:
                        case 13:
                            sb.Append(" ");
                            break;
                        default:
                            if (sb.Length > 0)
                                strings.Add(new MM1String(iStart, sb.ToString()));
                            sb.Clear();
                            iStart = iCurrent + 1;
                            break;
                    }
                iCurrent++;

                if (iCurrent > 1 && bytes[iCurrent] == 0 && bytes[iCurrent - 1] == 0)
                    break;
            }

            return strings;
        }

        public override string GetMapStrings(bool bRaw = false)
        {
            List<ScriptString> strings = GetScriptStrings();

            StringBuilder sb = new StringBuilder();
            foreach (MM1String str in strings)
                sb.AppendFormat("[{0:X4}] {1}\r\n", str.Offset, str.Value);

            return sb.ToString();
        }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            MM1QuestInfo info = new MM1QuestInfo();

            MM1PartyInfo party = ReadMM1PartyInfo();
            if (party == null)
                return null;

            MM1MapData map = GetMapData(false, 0) as MM1MapData;

            MemoryStream stream = new MemoryStream();
            byte[] questBytes = party.QuestBytes;
            if (questBytes == null)
                return null;
            stream.Write(questBytes, 0, questBytes.Length);
            if (map.Attributes == null)
                return null;
            stream.Write(map.Attributes, 0, map.Attributes.Length);

            questBytes = map.QuestBytes;
            stream.Write(questBytes, 0, questBytes.Length);
            stream.WriteByte((byte)iOverrideCharAddress);
            info.MapIndex = GetCurrentMapIndex();
            stream.WriteByte((byte)info.MapIndex);

            byte[] newBytes = stream.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(new MM1QuestData(party, GetLocation(), map), iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override bool SetQuestBits(int iAddress, QuestBits bits, bool bSet)
        {
            if (iAddress < 0)
                return false;

            if (!IsValid)
                return false;

            MM1PartyInfo party = ReadMM1PartyInfo();
            if (party == null)
                return false;

            LocationInformation location = GetLocation();

            MM1MapData mapData = GetMapData(false, 0) as MM1MapData;

            byte[] questBytes = new byte[17];
            Buffer.BlockCopy(party.Bytes, iAddress * party.CharacterSize + 109, questBytes, 0, 17);

            foreach (object bit in bits.Bits)
            {
                if (bit is MM1QuestIndex)
                {
                    if (!bSet)
                        questBytes[0] = (int)MM1QuestIndex.None;
                }
                else if (bit is MM1MainQuestFlags)
                {
                    MM1MainQuestFlags flags = (MM1MainQuestFlags)questBytes[3];
                    if (bSet)
                        flags |= (MM1MainQuestFlags)bit;
                    else
                        flags &= ~(MM1MainQuestFlags)bit;
                    questBytes[3] = (byte)flags;
                }
                else if (bit is MM1PrisonersFlags)
                {
                    MM1PrisonersFlags prisoners = (MM1PrisonersFlags)questBytes[4];
                    if (bSet)
                    {
                        prisoners |= (MM1PrisonersFlags)bit;
                    }
                    else
                        prisoners &= ~(MM1PrisonersFlags)bit;
                    questBytes[4] = (byte)prisoners;
                }
                else if (bit is MM1GreatBeastsFlags)
                {
                    MM1GreatBeastsFlags beasts = (MM1GreatBeastsFlags)questBytes[5];
                    if (bSet)
                    {
                        beasts |= (MM1GreatBeastsFlags)bit;
                    }
                    else
                        beasts &= ~(MM1GreatBeastsFlags)bit;
                    questBytes[5] = (byte)beasts;
                }
                else if (bit is MM1Sign)
                {
                    if (bSet)
                        questBytes[7] = (byte) MM1Sign.BlackDresidion;
                    else
                        questBytes[7] = 0;
                }
                else if (bit is MM1QuestStates.Inspectron)
                {
                    if (bSet)
                    {
                        questBytes[8] = 0xff;
                        questBytes[11] = 0xff;
                        if (questBytes[0] >= (int)MM1QuestIndex.Inspectron1 || questBytes[0] <= (int) MM1QuestIndex.Inspectron7)
                            questBytes[0] = (int)MM1QuestIndex.None;
                    }
                    else
                    {
                        questBytes[8] = (byte)MM1CastleQuestFlags.None;
                        questBytes[11] = (byte)MM1CastleQuestFlags.None;
                        questBytes[0] = (int)MM1QuestIndex.None;
                    }
                }
                else if (bit is MM1QuestStates.RepeatInspectron)
                {
                    if (bSet)
                    {
                        questBytes[8] = 0xff;
                        questBytes[11] = 0xbf;
                        if (questBytes[0] >= (int)MM1QuestIndex.Inspectron1 || questBytes[0] <= (int)MM1QuestIndex.Inspectron7)
                            questBytes[0] = (int)MM1QuestIndex.None;
                    }
                    else
                    {
                        questBytes[8] = (byte)MM1CastleQuestFlags.All;
                        questBytes[11] = (byte)MM1CastleQuestFlags.All;
                        questBytes[0] = (int)MM1QuestIndex.None;
                    }
                }
                else if (bit is MM1QuestStates.Hacker)
                {
                    if (bSet)
                    {
                        questBytes[9] = 0xff;
                        questBytes[12] = 0xff;
                        questBytes[0] = (int)MM1QuestIndex.None;
                    }
                    else
                    {
                        questBytes[9] = (byte)MM1CastleQuestFlags.None;
                        questBytes[12] = (byte)MM1CastleQuestFlags.None;
                        if (questBytes[0] >= (int)MM1QuestIndex.Hacker1 || questBytes[0] <= (int)MM1QuestIndex.Hacker1)
                            questBytes[0] = (int)MM1QuestIndex.None;
                    }
                }
                else if (bit is MM1QuestStates.Ironfist)
                {
                    if (bSet)
                    {
                        questBytes[10] = 0xff;
                        questBytes[13] = 0xff;
                        questBytes[0] = (int)MM1QuestIndex.None;
                    }
                    else
                    {
                        questBytes[10] = (byte)MM1CastleQuestFlags.None;
                        questBytes[13] = (byte)MM1CastleQuestFlags.None;
                        if (questBytes[0] >= (int)MM1QuestIndex.Ironfist1 || questBytes[0] <= (int)MM1QuestIndex.Ironfist1)
                            questBytes[0] = (int)MM1QuestIndex.None;
                    }
                }
                else if (bit is MM1QuestStates.RepeatIronfist)
                {
                    if (bSet)
                    {
                        questBytes[10] = 0xff;
                        questBytes[13] = 0xbf;
                        questBytes[0] = (int)MM1QuestIndex.None;
                    }
                    else
                    {
                        questBytes[10] = (byte)MM1CastleQuestFlags.All;
                        questBytes[13] = (byte)MM1CastleQuestFlags.All;
                        if (questBytes[0] >= (int)MM1QuestIndex.Ironfist1 || questBytes[0] <= (int)MM1QuestIndex.Ironfist1)
                            questBytes[0] = (int)MM1QuestIndex.None;
                    }
                }
                else if (bit is MM1StatIncreaserFlags)
                {
                    MM1StatIncreaserFlags stats = (MM1StatIncreaserFlags)questBytes[14];
                    if (bSet)
                    {
                        stats |= (MM1StatIncreaserFlags)bit;
                    }
                    else
                        stats &= ~(MM1StatIncreaserFlags)bit;
                    questBytes[14] = (byte)stats;
                }
                else if (bit is MM1AstralQuestFlags)
                {
                    MM1AstralQuestFlags astral = (MM1AstralQuestFlags)questBytes[16];
                    if (bSet)
                    {
                        astral |= (MM1AstralQuestFlags)bit;
                    }
                    else
                        astral &= ~(MM1AstralQuestFlags)bit;
                    questBytes[16] = (byte)astral;
                }
                else if (bit is MM1QuestStates.ClimbTrees)
                {
                    if (location.MapIndex == (int)MM1Map.D3Surface)
                    {
                        mapData.SetHighBits(MM1.Spots.Trees, bSet);
                        WriteByte(MM1Memory.StartedArenkoQuest, (byte)(bSet ? 1 : 0));
                        WriteOffset(MM1Memory.MapAttributes, mapData.Attributes);
                    }
                }
                else if (bit is MM1QuestStates.ErliquinTreasure)
                {
                    if (location.MapIndex == (int)MM1Map.B1Erliquin)
                    {
                        mapData.SetHighBits(MM1.Spots.ErliquinTreasures, bSet);
                        WriteOffset(MM1Memory.MapAttributes, mapData.Attributes);
                    }
                }
                else if (bit is MM1QuestStates.CrazedEncounters)
                {
                    if (location.MapIndex == (int)MM1Map.C2CrazedWizardCave)
                    {
                        mapData.SetHighBits(MM1.Spots.CWEncounters, bSet);
                        mapData.SetHighBit(MM1.Spots.CrazedWizard, bSet);
                        WriteByte(MM1Memory.Encounter13, (byte)(bSet ? 13 : 0));
                        WriteOffset(MM1Memory.MapAttributes, mapData.Attributes);
                    }
                }
                else if (bit is MM1QuestStates.MagicSquare)
                {
                    if (location.MapIndex == (int)MM1Map.D3CaveOfSquareMagic)
                    {
                        byte[] bytes;
                        if (bSet)
                            bytes = new byte[9] { 0xB3, 0xB2, 0xB5, 0xB8, 0xB9, 0xB6, 0xB7, 0xB4, 0xB1 };
                        else
                            bytes = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                        WriteOffset(MM1Memory.MagicSquareNumbers, bytes);
                    }
                }
            }

            return WriteOffset(MM1Memory.PartyInfo + (iAddress * MM1Character.SizeInBytes + 109), questBytes);
        }

        public override MapAttributes GetMapAttributes()
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM1Memory.MapGlobalAttributes, 88);
            if (bytes == null)
                return null;

            MM1MapAttributes map = new MM1MapAttributes();
            map.Bytes = bytes;

            map.Index = GetCurrentMapIndex();
            map.FlyFlags = bytes[MM1MapAttributeOffsets.FlyFlags];
            map.SafestSquare = new Point(bytes[MM1MapAttributeOffsets.SafestSquare], bytes[MM1MapAttributeOffsets.SafestSquare+1]);
            map.SurrenderSquare = new Point(bytes[MM1MapAttributeOffsets.SurrenderSquare], bytes[MM1MapAttributeOffsets.SurrenderSquare+1]);
            map.Exits.Add(new MMExit(MMExitDirection.North, (int)MM1Memory.MM1PointerToMap(bytes, MM1MapAttributeOffsets.ExitNorth)));
            map.Exits.Add(new MMExit(MMExitDirection.East, (int)MM1Memory.MM1PointerToMap(bytes, MM1MapAttributeOffsets.ExitEast)));
            map.Exits.Add(new MMExit(MMExitDirection.South, (int)MM1Memory.MM1PointerToMap(bytes, MM1MapAttributeOffsets.ExitSouth)));
            map.Exits.Add(new MMExit(MMExitDirection.West, (int)MM1Memory.MM1PointerToMap(bytes, MM1MapAttributeOffsets.ExitWest)));
            map.Exits.Add(new MMExit(MMExitDirection.Surface, (int)MM1Memory.MM1PointerToMap(bytes, MM1MapAttributeOffsets.Surface),
                new Point(bytes[MM1MapAttributeOffsets.Surface + 3], bytes[MM1MapAttributeOffsets.Surface + 4])));
            map.Exits.Add(new MMExit(MMExitDirection.Run, -1, map.SafestSquare));
            map.Exits.Add(new MMExit(MMExitDirection.Surrender, -1, map.SurrenderSquare));

            map.EncounterSize = bytes[MM1MapAttributeOffsets.EncounterSize];
            map.MonsterGroup = bytes[MM1MapAttributeOffsets.MonsterGroup];
//            map.DefaultEra = -1;
            map.UndergroundLevel = bytes[MM1MapAttributeOffsets.UndergroundLevel];
            map.ForbiddenSpells = bytes[MM1MapAttributeOffsets.ForbiddenSpells];

            map.Flags = MapAttributeFlags.AllowEtherealize;
            if ((bytes[MM1MapAttributeOffsets.ForbiddenSpells] & 0x02) == 0)
                map.Flags |= MapAttributeFlags.AllowTeleport;
            if ((bytes[MM1MapAttributeOffsets.ForbiddenSpells] & 0x04) == 0)
                map.Flags |= MapAttributeFlags.AllowSurface;
            if ((bytes[MM1MapAttributeOffsets.ForbiddenSpells] & 0x08) == 0)
                map.Flags |= MapAttributeFlags.AllowTownPortal;
            if ((bytes[MM1MapAttributeOffsets.ForbiddenSpells] & 0x01) == 0x01)
                map.Flags |= MapAttributeFlags.Darkness;
            if ((bytes[MM1MapAttributeOffsets.FlyFlags] & 0x80) == 0x80)
                map.Flags |= MapAttributeFlags.AllowFly;

            return map;
        }

        public override bool SetMapAttributes(int index, byte[] bytes)
        {
            if (!IsValid)
                return false;

            WriteOffset(MM1Memory.MapGlobalAttributes + index, bytes);
            return true;
        }

        public MMActiveEffects GetActiveEffects()
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM1Memory.ActiveEffects, 18);
            if (bytes == null)
                return null;

            MMActiveEffects effects = new MMActiveEffects();
            effects.ProtFear = bytes[MM1EffectsOffsets.ProtFear];
            effects.ProtCold = bytes[MM1EffectsOffsets.ProtCold];
            effects.ProtFire = bytes[MM1EffectsOffsets.ProtFire];
            effects.ProtPoison = bytes[MM1EffectsOffsets.ProtPoison];
            effects.ProtAcid = bytes[MM1EffectsOffsets.ProtAcid];
            effects.ProtElectric = bytes[MM1EffectsOffsets.ProtElectric];
            effects.ProtMagic = bytes[MM1EffectsOffsets.ProtMagic];
            effects.LightFactors = bytes[MM1EffectsOffsets.LightFactors];
            effects.LeatherSkin = (bytes[MM1EffectsOffsets.LeatherSkin] != 0);
            effects.Levitate = (bytes[MM1EffectsOffsets.Levitate] != 0);
            effects.WaterWalk = (bytes[MM1EffectsOffsets.WaterWalk] != 0);
            effects.GuardDog = (bytes[MM1EffectsOffsets.GuardDog] != 0);
            effects.PsychicProt = (bytes[MM1EffectsOffsets.PsychicProt] != 0);
            effects.Bless = (bytes[MM1EffectsOffsets.Bless] != 0);
            effects.Invisibility = (bytes[MM1EffectsOffsets.Invisibility] != 0);
            effects.Shield = (bytes[MM1EffectsOffsets.Shield] != 0);
            effects.PowerShield = (bytes[MM1EffectsOffsets.PowerShield] != 0);
            effects.Cursed = bytes[MM1EffectsOffsets.Cursed];

            return effects;
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            MM1GameInfo info = new MM1GameInfo();

            MemoryStream stream = new MemoryStream();

            info.MapAttributes = (MM1MapAttributes)GetMapAttributes();
            if (info.MapAttributes == null)
                return null;
            stream.Write(info.MapAttributes.Bytes, 0, info.MapAttributes.Bytes.Length);

            info.ActiveEffects = (MMActiveEffects)GetActiveEffects();
            if (info.ActiveEffects == null)
                return null;
            stream.Write(info.ActiveEffects.Bytes, 0, info.ActiveEffects.Bytes.Length);

            info.Location = GetLocation();
            if (info.Location == null)
                return null;
            byte[] locationBytes = info.Location.GetBytes();
            stream.Write(locationBytes, 0, locationBytes.Length);

            info.Bytes = stream.ToArray();

            return info;
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(55);
            for (MM1Map map = MM1Map.C2Sorpigal; map < MM1Map.Last; map += 2)
                maps.Add(GetMapTitlePair((int) map));
            return maps;
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch((MM1Map) index)
            {
                case MM1Map.C2Sorpigal: return new MapTitleInfo(index, "C-2, Sorpigal");
                case MM1Map.B3Portsmith: return new MapTitleInfo(index, "B-3, Portsmith");
                case MM1Map.D4Algary: return new MapTitleInfo(index, "D-4, Algary");
                case MM1Map.E1Dusk: return new MapTitleInfo(index, "E-1, Dusk");
                case MM1Map.B1Erliquin: return new MapTitleInfo(index, "B-1, Erliquin");
                case MM1Map.C2SorpigalDungeon: return new MapTitleInfo(index, "C-2, Sorpigal Dungeon");
                case MM1Map.C2CrazedWizardCave: return new MapTitleInfo(index, "C-2, Crazed Wizard Cave");
                case MM1Map.B3PortsmithDungeon: return new MapTitleInfo(index, "B-3, Portsmith Dungeon");
                case MM1Map.B1ErliquinDungeon: return new MapTitleInfo(index, "B-1, Erliquin Dungeon");
                case MM1Map.E1DuskDungeon: return new MapTitleInfo(index, "E-1, Dusk Dungeon");
                case MM1Map.B3CavesattheKorinBluffs: return new MapTitleInfo(index, "B-3, Caves at the Korin Bluffs");
                case MM1Map.C4Volcano: return new MapTitleInfo(index, "C-4, Volcano");
                case MM1Map.D3CaveOfSquareMagic: return new MapTitleInfo(index, "D-3, Cave of Square Magic");
                case MM1Map.B2MedusaLair: return new MapTitleInfo(index, "B-2, Medusa Lair");
                case MM1Map.A1Surface: return new MapTitleInfo(index, "A-1, Surface");
                case MM1Map.A2Surface: return new MapTitleInfo(index, "A-2, Surface");
                case MM1Map.A3Surface: return new MapTitleInfo(index, "A-3, Surface");
                case MM1Map.A4Surface: return new MapTitleInfo(index, "A-4, Surface");
                case MM1Map.B1Surface: return new MapTitleInfo(index, "B-1, Surface");
                case MM1Map.B2Surface: return new MapTitleInfo(index, "B-2, Surface");
                case MM1Map.B3Surface: return new MapTitleInfo(index, "B-3, Surface");
                case MM1Map.B4Surface: return new MapTitleInfo(index, "B-4, Surface");
                case MM1Map.C1Surface: return new MapTitleInfo(index, "C-1, Surface");
                case MM1Map.C2Surface: return new MapTitleInfo(index, "C-2, Surface");
                case MM1Map.C3Surface: return new MapTitleInfo(index, "C-3, Surface");
                case MM1Map.C4Surface: return new MapTitleInfo(index, "C-4, Surface");
                case MM1Map.D1Surface: return new MapTitleInfo(index, "D-1, Surface");
                case MM1Map.D2Surface: return new MapTitleInfo(index, "D-2, Surface");
                case MM1Map.D3Surface: return new MapTitleInfo(index, "D-3, Surface");
                case MM1Map.D4Surface: return new MapTitleInfo(index, "D-4, Surface");
                case MM1Map.E1Surface: return new MapTitleInfo(index, "E-1, Surface");
                case MM1Map.E2Surface: return new MapTitleInfo(index, "E-2, Surface");
                case MM1Map.E3Surface: return new MapTitleInfo(index, "E-3, Surface");
                case MM1Map.E4Surface: return new MapTitleInfo(index, "E-4, Surface");
                case MM1Map.A1CastleDoom: return new MapTitleInfo(index, "A-1, Castle Doom");
                case MM1Map.B1CastleBlackridgeNorth: return new MapTitleInfo(index, "B-1, Castle Blackridge North");
                case MM1Map.B1CastleBlackridgeSouth: return new MapTitleInfo(index, "B-1, Castle Blackridge South");
                case MM1Map.B1AncientWizardLairLevel1: return new MapTitleInfo(index, "B-1, Ancient Wizard Lair, Level 1");
                case MM1Map.B1AncientWizardLairLevel2: return new MapTitleInfo(index, "B-1, Ancient Wizard Lair, Level 2");
                case MM1Map.B2WarriorsStrongholdLevel1: return new MapTitleInfo(index, "B-2, Warrior's Stronghold, Level 1");
                case MM1Map.B2WarriorsStrongholdLevel2: return new MapTitleInfo(index, "B-2, Warrior's Stronghold, Level 2");
                case MM1Map.B3StrongholdintheEnchantedForestLevel1: return new MapTitleInfo(index, "B-3, Stronghold in the Enchanted Forest, Level 1");
                case MM1Map.B3StrongholdintheEnchantedForestLevel2: return new MapTitleInfo(index, "B-3, Stronghold in the Enchanted Forest, Level 2");
                case MM1Map.B3CastleWhiteWolf: return new MapTitleInfo(index, "B-3, Castle White Wolf");
                case MM1Map.E1CastleDragaduneLevel1: return new MapTitleInfo(index, "E-1, Castle Dragadune, Level 1");
                case MM1Map.E1CastleDragaduneLevel2: return new MapTitleInfo(index, "E-1, Castle Dragadune, Level 2");
                case MM1Map.E1CastleDragaduneLevel3: return new MapTitleInfo(index, "E-1, Castle Dragadune, Level 3");
                case MM1Map.E1CastleDragaduneLevel4: return new MapTitleInfo(index, "E-1, Castle Dragadune, Level 4");
                case MM1Map.SoulMaze: return new MapTitleInfo(index, "Soul Maze");
                case MM1Map.E3CastleAlamar: return new MapTitleInfo(index, "E-3, Castle Alamar");
                case MM1Map.E4FabledBuildingofGoldLevel1: return new MapTitleInfo(index, "E-4, Fabled Building of Gold, Level 1");
                case MM1Map.E4FabledBuildingofGoldLevel2: return new MapTitleInfo(index, "E-4, Fabled Building of Gold, Level 2");
                case MM1Map.E4FabledBuildingofGoldLevel3: return new MapTitleInfo(index, "E-4, Fabled Building of Gold, Level 3");
                case MM1Map.E4FabledBuildingofGoldLevel4: return new MapTitleInfo(index, "E-4, Fabled Building of Gold, Level 4");
                case MM1Map.AstralPlane: return new MapTitleInfo(index, "Astral Plane");
                default: return new MapTitleInfo(index, String.Format("UnknownMap({0})", index));
            }
        }

        public override bool SetExit(MMExit exit)
        {
            if (!IsValid)
                return false;

            if (exit == null)
                return false;

            long pWritten;

            byte[] bytes = new byte[5];
            int iPointer = MM1Memory.MM1MapPointer((MM1Map)exit.Map);
            bytes[0] = (byte) (iPointer & 0xff);
            bytes[1] = (byte) ((iPointer >> 8) & 0xff);
            bytes[2] = (byte) ((iPointer >> 16) & 0xff);
            bytes[3] = (byte) exit.Point.X;
            bytes[4] = (byte) exit.Point.Y;

            if (bytes[3] > 15)
                bytes[3] = 15;
            if (bytes[4] > 15)
                bytes[4] = 15;

            switch (exit.Direction)
            {
                case MMExitDirection.North:
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ExitNorth, bytes, 3, out pWritten);
                    break;
                case MMExitDirection.East:
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ExitEast, bytes, 3, out pWritten);
                    break;
                case MMExitDirection.South:
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ExitSouth, bytes, 3, out pWritten);
                    break;
                case MMExitDirection.West:
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ExitWest, bytes, 3, out pWritten);
                    break;
                case MMExitDirection.Surface:
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.Surface, bytes, 5, out pWritten);
                    break;
                case MMExitDirection.Run:
                    bytes[0] = bytes[3];
                    bytes[1] = bytes[4];
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.SafestSquare, bytes, 2, out pWritten);
                    break;
                case MMExitDirection.Surrender:
                    bytes[0] = bytes[3];
                    bytes[1] = bytes[4];
                    WriteOffset(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.SurrenderSquare, bytes, 2, out pWritten);
                    break;
                default:
                    break;
            }

            return true;
        }

        public override bool SetMapAttributeFlags(MapAttributeFlags flags)
        {
            if (!IsValid)
                return false;

            byte flyFlags;
            byte spells;

            flyFlags = ReadByte(MM1Memory.MapGlobalAttributes);
            spells = ReadByte(MM1Memory.MapGlobalAttributes);

            spells = Global.UpdateFlag(spells, 0x01, flags.HasFlag(MapAttributeFlags.Darkness));
            spells = Global.UpdateFlag(spells, 0x02, !flags.HasFlag(MapAttributeFlags.AllowTeleport));
            spells = Global.UpdateFlag(spells, 0x04, !flags.HasFlag(MapAttributeFlags.AllowSurface));
            spells = Global.UpdateFlag(spells, 0x08, !flags.HasFlag(MapAttributeFlags.AllowTownPortal));
            flyFlags = Global.UpdateFlag(flyFlags, 0x80, flags.HasFlag(MapAttributeFlags.AllowFly));

            WriteByte(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.FlyFlags, flyFlags);
            return WriteByte(MM1Memory.MapGlobalAttributes + MM1MapAttributeOffsets.ForbiddenSpells, spells);
        }

        public override bool SetDepth(byte bDepth)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM1Memory.MapGlobalAttributes + 37, bDepth);
        }

        public override bool SetActiveEffect(MMEffectTag effect)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[1];
            switch (effect.Effect)
            {
                case MMEffects.ProtFear:
                case MMEffects.ProtCold:
                case MMEffects.ProtFire:
                case MMEffects.ProtPoison:
                case MMEffects.ProtAcid:
                case MMEffects.ProtElectric:
                case MMEffects.ProtMagic:
                case MMEffects.LightFactors:
                case MMEffects.Cursed:
                    bytes[0] = (byte)(effect.Enabled ? effect.Value : 0);
                    break;
                case MMEffects.LeatherSkin:
                case MMEffects.Levitate:
                case MMEffects.GuardDog:
                case MMEffects.PsychicProt:
                    bytes[0] = (byte)(effect.Enabled ? 0xff : 0);
                    break;
                case MMEffects.WaterWalk:
                case MMEffects.Bless:
                case MMEffects.Invisibility:
                case MMEffects.Shield:
                case MMEffects.PowerShield:
                    bytes[0] = (byte)(effect.Enabled ? 1 : 0);
                    break;
                default:
                    return false;
            }

            int iOffset = effect.MM1Offset;
            if (iOffset != -1)
                return WriteOffset(MM1Memory.ActiveEffects + iOffset, bytes);

            return false;
        }

        public override bool HasSurfaceLocation { get { return true; } }


        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            IntDeck deck = new IntDeck(1, 255);

            List<Item> items = new List<Item>(6);
            byte[] bytes = new byte[2];
            for (int i = 0; i < 6; i++)
            {
                deck.Shuffle();
                Global.Rand.NextBytes(bytes);
                Item itemRand = null;

                foreach (int iIndex in deck.Cards)
                {
                    bytes[0] = (byte) iIndex;
                    itemRand = MM1Item.FromBagBytes(bytes);
                    if (itemRand.MatchTypeAndChar(type, bUsableOnly ? baseChar : null))
                        break;
                }

                items.Add(itemRand);
            }
            SetBackpack(baseChar.BasicAddress, items);
        }

        public byte[] GetEncounterBytes()
        {
            if (!IsValid)
                return new byte[0];

            byte[] encounters = Properties.Resources.MM1MonsterDefaults;
            int iIndex = 0;

            byte bMap = (byte) GetCurrentMapIndex();

            while (iIndex < encounters.Length-1)
            {
                if (encounters[iIndex] == bMap)
                {
                    byte[] bytes = new byte[encounters[iIndex + 1]];
                    Buffer.BlockCopy(encounters, iIndex + 2, bytes, 0, encounters[iIndex + 1]);
                    return bytes;
                }

                iIndex += (encounters[iIndex + 1] + 2);
            }

            return new byte[0];
        }

        public override bool KillAllMonsters()
        {
            if (!IsValid)
                return false;

            // Remove all upper bits from encounter squares for this map
            byte[] encounters = GetEncounterBytes();

            MemoryBytes bytesAttrib = ReadOffset(MM1Memory.MapAttributes, 256);
            if (bytesAttrib == null)
                return false;

            for (int i = 0; i < encounters.Length; i++)
                bytesAttrib[encounters[i]] &= 0x7f;

            return WriteOffset(MM1Memory.MapAttributes, bytesAttrib);
        }

        public override bool ResetMonsters()
        {
            if (!IsValid)
                return false;

            // Set all upper bits from encounter squares for this map
            byte[] encounters = GetEncounterBytes();

            MemoryBytes bytesAttrib = ReadOffset(MM1Memory.MapAttributes, 256);
            if (bytesAttrib == null)
                return false;

            for (int i = 0; i < encounters.Length; i++)
                bytesAttrib[encounters[i]] |= 0x80;

            return WriteOffset(MM1Memory.MapAttributes, bytesAttrib);
        }

        public override string GetDebugMemoryInfo()
        {
            if (!IsValid)
                return "[Hacker not valid]";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Special Squares: ");
            MM1SpecialSquares squares = GetSpecialSquares();
            foreach (List<MM1SpecialSquare> list in squares.Squares.Values)
            {
                foreach (MM1SpecialSquare square in list)
                {
                    sb.AppendFormat("{0},{1},{2},{3:X4}\r\n", square.Location.X, square.Location.Y, Global.DirectionString(square.Facing, true), square.ScriptPointer);
                }
            }

            //byte[] bytesMap = new byte[1];
            //ReadOffset(MM1Memory.MapIndex, bytesMap);

            //ByteSaver.AddMap(bytesMap[0], listEncounters.ToArray());

            return sb.ToString();
        }

        public MM1SpecialSquares GetSpecialSquares()
        {
            if (!IsValid)
                return null;

            int iNumSpecial = ReadByte(MM1Memory.NumSpecialSquares);
            MemoryBytes bytesSquares = ReadOffset(MM1Memory.SpecialSquareCoord, iNumSpecial);
            MemoryBytes bytesDirections = ReadOffset(MM1Memory.SpecialSquareCoord + bytesSquares.Length, iNumSpecial);
            MemoryBytes bytesAttrib = ReadOffset(MM1Memory.MapAttributes, 256);
            MemoryBytes bytesPointers = ReadOffset(MM1Memory.SpecialSquareCoord + bytesSquares.Length + bytesDirections.Length, iNumSpecial * 2);

            if (bytesSquares == null || bytesDirections == null || bytesAttrib == null || bytesPointers == null)
                return null;

            ushort[] pointers = new ushort[iNumSpecial];
            for (int i = 0; i < bytesPointers.Length; i += 2)
                pointers[i / 2] = BitConverter.ToUInt16(bytesPointers, i);

            MM1SpecialSquares squares = new MM1SpecialSquares(bytesSquares, bytesDirections, pointers, bytesAttrib);

            return squares;
        }

        public override string ReplaceNoteStrings(string str)
        {
            if (!IsValid)
                return str;
            
            StringBuilder sbResult = new StringBuilder(str);

            if (str.Contains("EncounterMonsters"))
            {
                int iNumMonsters = ReadByte(MM1Memory.NumberOfMonsters);
                if (iNumMonsters > 15)
                    iNumMonsters = 15;
                MemoryBytes monsters = ReadOffset(MM1Memory.EncounterList, 32 * 15);
                if (monsters == null)
                    return str;
                ASCIIEncoding encoding = new ASCIIEncoding();

                if (str.Contains("$uniqueEncounterMonsters"))
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < iNumMonsters; i++)
                    {
                        string strName = encoding.GetString(monsters, i * 32, 15).Trim();
                        if (!list.Contains(strName))
                            list.Add(strName);
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (string strMonster in list)
                    {
                        sb.AppendFormat("{0}, ", strMonster);
                    }
                    if (Global.Trim(sb).Length == 0)
                        sb = new StringBuilder("Unknown");

                    sbResult.Replace("$uniqueEncounterMonsters", sb.ToString());
                }
                if (str.Contains("$allEncounterMonsters"))
                {
                    Dictionary<string, MonsterCount> dict = new Dictionary<string, MonsterCount>();
                    for (int i = 0; i < iNumMonsters; i++)
                    {
                        string strName = encoding.GetString(monsters, i * 32, 15).Trim();
                        if (!dict.ContainsKey(strName))
                            dict.Add(strName, new MonsterCount(strName, 1));
                        else
                            dict[strName].Count++;
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (MonsterCount mc in dict.Values)
                    {
                        sb.AppendFormat("{0}{1}, ", mc.Name, mc.Count != 1 ? String.Format(" ({0})", mc.Count) : "");
                    }
                    if (Global.Trim(sb).Length == 0)
                        sb = new StringBuilder("Unknown");

                    sbResult.Replace("$allEncounterMonsters", sb.ToString());
                }
            }
            return sbResult.ToString();
        }

        public override Shops GetShopInfo()
        {
            Shops shops = new Shops();

            LocationInformation info = GetLocation();

            // Shops in MM1 are in fixed locations, so they are hard-coded
            // (because the "InShop" state applies to temples and whatnot as well)

            shops.Inventories = new List<ShopInventory>(5);

            MemoryBytes bytesShops = ReadOffset(MM1Memory.ShopItems, 90);
            if (bytesShops == null)
                return shops;

            shops.RawBytes = bytesShops;

            MM1ShopInventory sorpigal = new MM1ShopInventory(bytesShops, MM1Map.C2Sorpigal);
            MM1ShopInventory portsmith = new MM1ShopInventory(bytesShops, MM1Map.B3Portsmith);
            MM1ShopInventory algary = new MM1ShopInventory(bytesShops, MM1Map.D4Algary);
            MM1ShopInventory dusk = new MM1ShopInventory(bytesShops, MM1Map.E1Dusk);
            MM1ShopInventory erliquin = new MM1ShopInventory(bytesShops, MM1Map.B1Erliquin);

            sorpigal.Town = "Sorpigal";
            portsmith.Town = "Portsmith";
            algary.Town = "Algary";
            dusk.Town = "Dusk";
            erliquin.Town = "Erliquin";

            shops.Inventories.Add(sorpigal);
            shops.Inventories.Add(portsmith);
            shops.Inventories.Add(algary);
            shops.Inventories.Add(dusk);
            shops.Inventories.Add(erliquin);

            shops.InShop = false;

            switch ((MM1Map) info.MapIndex)
            {
                case MM1Map.C2Sorpigal:
                    shops.InShop = (info.PrimaryCoordinates.X == 6 && info.PrimaryCoordinates.Y == 5);
                    if (shops.InShop)
                        shops.CurrentDisplay = sorpigal.Items;
                    break;
                case MM1Map.B3Portsmith:
                    shops.InShop = (info.PrimaryCoordinates.X == 12 && info.PrimaryCoordinates.Y == 13);
                    if (shops.InShop)
                        shops.CurrentDisplay = portsmith.Items;
                    break;
                case MM1Map.D4Algary:
                    shops.InShop = (info.PrimaryCoordinates.X == 12 && info.PrimaryCoordinates.Y == 12);
                    if (shops.InShop)
                        shops.CurrentDisplay = algary.Items;
                    break;
                case MM1Map.E1Dusk:
                    shops.InShop = (info.PrimaryCoordinates.X == 11 && info.PrimaryCoordinates.Y == 4);
                    if (shops.InShop)
                        shops.CurrentDisplay = dusk.Items;
                    break;
                case MM1Map.B1Erliquin:
                    shops.InShop = (info.PrimaryCoordinates.X == 3 && info.PrimaryCoordinates.Y == 9);
                    if (shops.InShop)
                        shops.CurrentDisplay = erliquin.Items;
                    break;
                default:
                    break;
            }

            if (!shops.InShop)
                return shops;

            return shops;
        }

        public override bool SetShopItem(ShopItem item)
        {
            if (!(item.Item is MM1Item))
                return false;

            MM1Item mm1Item = item.Item as MM1Item;

            byte[] index = new byte[] { (byte)mm1Item.Index };

            WriteOffset(item.Offset + 0 * item.Multiplier, index);

            return true;
        }

        public override int GetCurrentMapIndex()
        {
            return ReadByte(MM1Memory.MapIndex);
        }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            if (!IsValid)
                return null;

            MemoryBytes attributes = ReadOffset(MM1Memory.MapAttributes, 256);
            if (attributes == null)
                return null;

            return new MM1ActiveSquares(form, GetCurrentMapIndex(), attributes);
        }

        public override RosterFile CurrentRoster { get { return m_mm1Roster; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new MM1GameInformationControl(main); }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * MM1Character.SizeInBytes;
            CharacterOffsets offsets = MM1.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + MM1Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            // Single-byte 180 values (180 to ward off in-game overflow errors; some items are +60 to a resistance)
            foreach (int lOffset in new int[] { 
                offsets.Might, offsets.MightMod,
                offsets.Intellect, offsets.IntellectMod,
                offsets.Personality, offsets.PersonalityMod, 
                offsets.Endurance, offsets.EnduranceMod,
                offsets.Speed, offsets.SpeedMod,
                offsets.Accuracy, offsets.AccuracyMod, 
                offsets.Luck, offsets.LuckMod, 
                offsets.FireResist, offsets.FireResistMod, 
                offsets.ColdResist, offsets.ColdResistMod, 
                offsets.ElecResist, offsets.ElecResistMod,
                offsets.AcidResist, offsets.AcidResistMod,
                offsets.PoisonResist, offsets.PoisonResistMod,
                offsets.MagicResist, offsets.MagicResistMod,
                offsets.FearResist, offsets.FearResistMod,
                offsets.SleepResist, offsets.SleepResistMod,
                offsets.ArmorClassMod, offsets.Food, offsets.Thievery})
                info.Bytes[offset + lOffset] = 180;

            info.Bytes[offset + offsets.Level] = 200;
            info.Bytes[offset + offsets.LevelMod] = 200;

            // Two-byte 60000 values (not quite maximum, to avoid overflow issues):
            byte[] pbMax2 = BitConverter.GetBytes((UInt16)60000);
            foreach (int lOffset in new int[] { 
                offsets.CurrentHP, offsets.MaxHP, offsets.MaxHPMod,
                offsets.CurrentSP, offsets.MaxSP, offsets.Gems})
                Buffer.BlockCopy(pbMax2, 0, info.Bytes, offset + lOffset, pbMax2.Length);

            byte[] pbMaxGold = BitConverter.GetBytes((UInt32)12000000);
            Buffer.BlockCopy(pbMaxGold, 0, info.Bytes, offset + offsets.Gold, 3);
            byte[] pbMaxXP = BitConverter.GetBytes((UInt32)28842001);
            Buffer.BlockCopy(pbMaxXP, 0, info.Bytes, offset + offsets.Experience, pbMaxXP.Length);

            MM1Class mmClass = (MM1Class)info.Bytes[offset + offsets.Class];
            MM1AlignmentValue mmAlign = (MM1AlignmentValue)info.Bytes[offset + offsets.Alignment];

            switch (mmClass)
            {
                case MM1Class.Knight:
                case MM1Class.Robber:
                    info.Bytes[offset + offsets.SpellLevel] = 0;
                    info.Bytes[offset + offsets.SpellLevelMod] = 0;
                    break;
                default:
                    info.Bytes[offset + offsets.SpellLevel] = 7;
                    info.Bytes[offset + offsets.SpellLevelMod] = 7;
                    break;
            }

            Global.SetBytes(info.Bytes, offset + offsets.Condition, offsets.ConditionLength, 0);
            info.Bytes[offset + offsets.Age] = 18;

            WriteOffset(MM1Memory.PartyInfo, info.Bytes);

            List<Item> items = new List<Item>(6);

            bool bGood = mmAlign == MM1AlignmentValue.Good;
            bool bEvil = mmAlign == MM1AlignmentValue.Evil;
            bool bNeutral = mmAlign == MM1AlignmentValue.Neutral;

            MM1ItemIndex bow = bGood ? MM1ItemIndex.TheMagicBow : bEvil ? MM1ItemIndex.BowOfPower : MM1ItemIndex.GiantsBow;

            switch (mmClass)
            {
                case MM1Class.Knight:
                    items.Add(MM1.Items[(int)MM1ItemIndex.TheFlamberge]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.UltimatePlate]);
                    items.Add(MM1.Items[(int)bow]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BootsOfSpeed]);
                    break;
                case MM1Class.Paladin:
                    items.Add(MM1.Items[(int)MM1ItemIndex.TheFlamberge]);
                    items.Add(MM1.Items[bGood ? (int)MM1ItemIndex.HolyPlate : bEvil ? (int)MM1ItemIndex.UnHolyPlate : (int) MM1ItemIndex.XXXXsPlate] );
                    items.Add(MM1.Items[(int)bow]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BootsOfSpeed]);
                    break;
                case MM1Class.Archer:
                    items.Add(MM1.Items[(int)MM1ItemIndex.TheFlamberge]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.UltimatePlate]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.ArchersBow]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BootsOfSpeed]);
                    break;
                case MM1Class.Robber:
                    items.Add(MM1.Items[(int)MM1ItemIndex.UltimateSword]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.DragonShield]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BlueRingMail]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.RobbersXBow]);
                    break;
                case MM1Class.Cleric:
                    items.Add(MM1.Items[(int)MM1ItemIndex.ThunderHammer]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.RedChainMail]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BootsOfSpeed]);
                    break;
                case MM1Class.Sorcerer:
                    items.Add(MM1.Items[(int)MM1ItemIndex.DiamondDagger]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.UltimatePlate]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.FlyingCarpet]);
                    items.Add(MM1.Items[(int)MM1ItemIndex.BootsOfSpeed]);
                    break;
                default:
                    break;
            }

            items.Add(MM1.Items[(int)MM1ItemIndex.DefenseCloak]);
            items.Add(MM1.Items[(int)MM1ItemIndex.DefenseRing]);
            SetBackpack(iAddress, items);

            return true;
        }

        public override string GetMapEnum(int index)
        {
            return String.Format("MM1Map.{0}", Enum.GetName(typeof(MM1Map), (MM1Map)(index)));
        }

        public override long GetGameTimeLong()
        {
            return (ReadByte(MM1Memory.Char1Age) << 8) + ReadByte(MM1Memory.Char1RestCounter);
        }


        public override List<BaseCharacter> GetCharacters()
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
                chars.Add(MM1Character.Create(pi.Bytes, MM1Character.SizeInBytes * i));

            return chars;
        }

        public override bool IsSurface(int iMap) { return IsSurfaceMap(iMap); }

        public static bool IsSurfaceMap(int iMap)
        {
            switch ((MM1Map)iMap)
            {
                case MM1Map.A1Surface:
                case MM1Map.A2Surface:
                case MM1Map.A3Surface:
                case MM1Map.A4Surface:
                case MM1Map.B1Surface:
                case MM1Map.B2Surface:
                case MM1Map.B3Surface:
                case MM1Map.B4Surface:
                case MM1Map.C1Surface:
                case MM1Map.C2Surface:
                case MM1Map.C3Surface:
                case MM1Map.C4Surface:
                case MM1Map.D1Surface:
                case MM1Map.D2Surface:
                case MM1Map.D3Surface:
                case MM1Map.D4Surface:
                case MM1Map.E1Surface:
                case MM1Map.E2Surface:
                case MM1Map.E3Surface:
                case MM1Map.E4Surface: 
                    return true;
                default:
                    return false;
            }
        }

        public override Point GetSurfaceSector(int iMap)
        {
            switch ((MM1Map)iMap)
            {
                case MM1Map.A1Surface: return new Point(0, 3);
                case MM1Map.A2Surface: return new Point(0, 2);
                case MM1Map.A3Surface: return new Point(0, 1);
                case MM1Map.A4Surface: return new Point(0, 0);
                case MM1Map.B1Surface: return new Point(1, 3);
                case MM1Map.B2Surface: return new Point(1, 2);
                case MM1Map.B3Surface: return new Point(1, 1);
                case MM1Map.B4Surface: return new Point(1, 0);
                case MM1Map.C1Surface: return new Point(2, 3);
                case MM1Map.C2Surface: return new Point(2, 2);
                case MM1Map.C3Surface: return new Point(2, 1);
                case MM1Map.C4Surface: return new Point(2, 0);
                case MM1Map.D1Surface: return new Point(3, 3);
                case MM1Map.D2Surface: return new Point(3, 2);
                case MM1Map.D3Surface: return new Point(3, 1);
                case MM1Map.D4Surface: return new Point(3, 0);
                case MM1Map.E1Surface: return new Point(4, 3);
                case MM1Map.E2Surface: return new Point(4, 2);
                case MM1Map.E3Surface: return new Point(4, 1);
                case MM1Map.E4Surface: return new Point(4, 0);
                default: return new Point(-1, -1);
            }
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                MM1GameState state = ReadMM1GameState();
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Opening:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                            TweakSleep(200);
                            break;
                        case MainState.Opening2:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                            TweakSleep(100);
                            break;
                        case MainState.MainMenu:
                            TweakSleep(10);
                            MemoryBytes bytes = ReadOffset(MM1Memory.RosterTowns, 18);
                            // Select the first six characters in the first town that has any characters in it
                            int iTown = 1;      // Default to Sorpigal
                            for(int i = 0; i < bytes.Length; i++)
                            {
                                if (bytes.Bytes[i] != 0)
                                {
                                    iTown = bytes.Bytes[i];
                                    break;
                                }
                            }
                            SendKeysToDOSBox(new Keys[] { Keys.D0 + iTown }, true);
                            TweakSleep(100);
                            break;
                        case MainState.Inn:
                        case MainState.SignIn:
                            TweakSleep(10);
                            for (int i = 0; i < 6; i++)
                            {
                                SendKeysToDOSBox(0, new Keys[] { Keys.A + i }, false, Keys.LControlKey);
                                TweakSleep(50);
                            }
                            SendKeysToDOSBox(new Keys[] { Keys.X });
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

        public override IEnumerable<Monster> GetMonsterList()
        {
            return MM1.Monsters;
        }

        public override MapBytes GetCurrentMapBytes()
        {
            GameState state = GetGameState();
            switch (state.Main)
            {
                case MainState.Adventuring:
                case MainState.PressEnter:
                    return new MapBytes(ReadOffset(MM1Memory.MapAppearance, 512), 16, 16);
                default:
                    return null;
            }
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null || mb.Bytes.Length < 512)
                return null;
            MMMapData data = new MMMapData();
            data.LiveOnly = true;
            data.Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            data.Appearance = mb.Bytes;
            data.Attributes = Global.Subset(mb.Bytes, 256, 256);
            return data;
        }
    }
}
