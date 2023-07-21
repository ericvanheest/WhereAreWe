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
    public static class MM3Memory
    {
        // Search for ?
        public static byte[] MainSearch = new byte[] { 0x30, 0x30, 0x37, 0x25, 0x73, 0x20, 0x69, 0x73, 0x20, 0x6E, 0x6F, 0x74 };  // "007%s is not"
        public static byte[] FileHeaderTest = new byte[] { 0x4d, 0x4d, 0x33, 0, 0, 0, 0, 0 };   // "MM3....."

        // Offsets based on the base rather than the main search
        public const int MainSearchSVN = 131401;              // Address of Main Block + Main Search
        public const int MainSearchNonSVN1 = 131337;
        public const int MonsterBase = 547312;
        public const int LoadingMap = 637380;           // 2 bytes (0=loaded, 2=loading)

        public const int MonMemPhysicalResist = 0;      // 90 bytes
        public const int MonMemEnergyResist = 128;      // 90 bytes
        public const int MonMemAcidResist = 256;        // 90 bytes
        public const int MonMemColdResist = 384;        // 90 bytes
        public const int MonMemElecResist = 512;        // 90 bytes
        public const int MonMemFireResist = 640;        // 90 bytes
        public const int MonMemMagicResist = 768;       // 90 words
        public const int MonMemGems = 896;              // 90 dwords
        public const int MonMemGold = 1120;             // 90 bytes
        public const int MonMemItems = 1520;            // 90 bytes
        public const int MonMemAccuracy = 1648;         // 90 bytes
        public const int MonMemRanged = 1776;           // 90 bytes
        public const int MonMemSpecialPower = 1904;     // 90 bytes
        public const int MonMemTarget = 2032;           // 90 bytes
        public const int MonMemDamageType = 2160;       // 90 dwords
        public const int MonMemExperience = 2288;       // 90 bytes
        public const int MonMemNumAttacks = 2688;       // 90 bytes
        public const int MonMemDamageDieMax = 2816;     // 90 bytes
        public const int MonMemDamageNumDice = 2944;    // 90 bytes
        public const int MonMemSpeed = 3072;            // 90 bytes
        public const int MonMemAC = 3200;               // 90 words
        public const int MonMemHPMax = 3328;            // 90 words
        public const int MonMemEnd = MonMemHPMax + 256;
 
        // Offsets based on the main search delta;
        public const int LoadingMapLocal = 19517;           // 1 byte (0=loading)
        public const int LoadingTownPortal = 41806;         // 1 byte (254=loading)
        public const int LastKeypress = 42091;              // 1 byte
        public const int FacingAndLocation = 36315;         // 3 bytes (f,x,y)
        public const int NumChars = 36305;                  // 1 byte
        public const int MapBytes = 27156;                  // ? bytes
        public const int PartyBytes = 36305;                // ? bytes
        public const int PartyInfo = 24253;                 // 303 bytes * NumChars
        public const int ActingCharacter = 42125;           // 1 byte
        public const int Time = 37148;                      // 9 bytes (d, y, y, ?, ?, ?, ?, m, m)
        public const int Year = 37149;                      // 2 bytes
        public const int Day = 37148;                       // 1 bytes
        public const int Minutes = 37161;                   // 2 bytes
        public const int CreationStats = 42061;             // 7 bytes of stats (M/I/P/E/S/A/L)
        public const int MainState1 = -123611;              // 2 bytes
        public const int MainState2 = -48091;               // 2 bytes
        public const int CreateCharState = 41321;           // 2 bytes
        public const int MapAppearance1 = 27195;            // 512 bytes (2 bytes per square)
        public const int MapCartography1 = 27995;           // 32 bytes
        public const int MapAppearance2 = 28027;            // 512 bytes (2 bytes per square)
        public const int MapCartography2 = 28827;           // 32 bytes
        public const int MapAppearance3 = 28859;            // 512 bytes (2 bytes per square)
        public const int MapCartography3 = 29659;           // 32 bytes
        public const int MapAppearance4 = 29691;            // 512 bytes (2 bytes per square)
        public const int MapCartography4 = 30491;           // 32 bytes
        public const int MonsterLocationY = 21079;          // 74 bytes (at least) of words
        public const int MonsterLocationX = 21419;          // 74 bytes (at least) of words
        public const int NumberOfMonsters = 37465;          // 1 byte
        public const int MonsterHP = 22779;                 // 74 bytes (at least) of words
        public const int MonsterIndices = 23459;            // 74 bytes (at least) of words
        public const int MonsterActive = 22439;             // 74 bytes (at least) of words
        public const int VisibleObjects = 19573;            // 960 bytes (max), 12 bytes per object
        public const int SpecialSquares = 14303;            // 5053 bytes (max), 10 bytes per square
        public const int ActingCaster = -23144;             // 1 byte (0-7)
        public const int ActingCombatChar = -22986;         // 1 byte (0-7)
        public const int InspectingCombatChar = -22971;     // 1 byte (0-7)
        public const int MonsterConditions = 23799;         // array of words
        public const int PartyFood = 37163;                 // 2 bytes
        public const int PartyGold = 37179;                 // 4 bytes
        public const int PartyGems = 37183;                 // 4 bytes
        public const int BankGold = 37171;                  // 4 bytes
        public const int BankGems = 37175;                  // 4 bytes
        public const int SavePermitted = 27975;             // 1 byte
        public const int MovedThisRound = 24240;            // 11 bytes (8 char, 3 melee monster)
        public const int ScriptPointer = 19539;             // 2 bytes, points to [value]*16, with a 32-byte header
        public const int ScriptLength = 20625;              // 2 bytes
        public const int MazeFilePointer = 19571;           // 2 bytes, points to [value]*16, with a 32-byte header
        public const int MazeFileLength = 20643;            // 2 bytes
        public const int LevitateActive = 36323;            // 1 byte
        public const int WizardEyeActive = 36325;           // 1 byte
        public const int LightActive = 37151;               // 1 byte
        public const int WalkOnWaterActive = 36326;         // 1 byte
        public const int CurrentMapQuadIndex = 27173;       // 1 byte (0-3)
        public const int MapAttributes1 = 27970;            // 57 bytes
        public const int MapAttributes2 = 28802;            // 57 bytes
        public const int MapAttributes3 = 29634;            // 57 bytes
        public const int MapAttributes4 = 30466;            // 57 bytes
        public const int LockStrengthOffset = 10;           // 1 byte
        public const int ForbiddenSpells = 27984;           // 7 bytes
        public const int AreaLight = 27976;                 // 1 byte (0/1)
        public const int ShopInventory = 36328;             // 810 bytes (6 properties, 9 items, 3 types, 5 shops)
        public const int CurrentShop = 26821;               // 114 bytes (6 properties, 18 items + 1 padding)
        public const int ProtectElements = 37153;           // 8 bytes (Fire/Elec/Cold/Acid shorts)
        public const int PartyStaticBits = 37191;           // 32 bytes (256 bits settable via script object #20)
        public const int InventoryDisplayOffset = 41447;    // 1 byte (0, 9 or 255)
        public const int InvInShopDisplayOffset = 41231;    // 1 byte (0, 9 or 255)
        public const int ShopSubScreen = 41867;             // 1 byte (0, 1, 2 or 255)
        
        public const int MonsterNames = 4032;              // 924 bytes

        public const int MonFilePhysicalResist = 0;      // 90 bytes
        public const int MonFileEnergyResist = 90;       // 90 bytes
        public const int MonFileAcidResist = 180;        // 90 bytes
        public const int MonFileColdResist = 270;        // 90 bytes
        public const int MonFileElecResist = 360;        // 90 bytes
        public const int MonFileFireResist = 450;        // 90 bytes
        public const int MonFileMagicResist = 540;       // 90 bytes
        public const int MonFileGems = 630;              // 90 words
        public const int MonFileGold = 810;              // 90 dwords
        public const int MonFileItems = 1170;            // 90 bytes
        public const int MonFileAccuracy = 1260;         // 90 bytes
        public const int MonFileRanged = 1350;           // 90 bytes
        public const int MonFileSpecialPower = 1440;     // 90 bytes
        public const int MonFileTarget = 1530;           // 90 bytes
        public const int MonFileDamageType = 1620;       // 90 bytes
        public const int MonFileExperience = 1710;       // 90 dwords
        public const int MonFileNumAttacks = 2070;       // 90 bytes
        public const int MonFileDamageDieMax = 2160;     // 90 bytes
        public const int MonFileDamageNumDice = 2250;    // 90 bytes
        public const int MonFileSpeed = 2340;            // 90 bytes
        public const int MonFileAC = 2430;               // 90 bytes
        public const int MonFileHPMax = 2520;            // 90 words
        public const int MonFileEnd = MonFileHPMax + 180;

        public const int CurrentMapIndex = 27156;           // 1 byte

        public static MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.MightAndMagic3]; } }
    }


    public class MM3VisibleObject : MM345VisibleObject
    {
        public UInt16 X;
        public UInt16 Y;
        public UInt16 ImageIndex;
        public UInt16 AnimationIndex;
        public UInt16 Unknown1;
        public UInt16 Unknown2;

        public override Point Location { get { return new Point(X, Y); } }
        public override int Image { get { return ImageIndex; } }
        public override uint Unknown { get { return (UInt32) ((Unknown2 << 16) | Unknown1); } }

        public MM3VisibleObject(byte[] bytes, int iOffset)
        {
            if (bytes.Length - iOffset < 12)
                return;

            Y = BitConverter.ToUInt16(bytes, 0 + iOffset);
            X = BitConverter.ToUInt16(bytes, 2 + iOffset);
            ImageIndex = BitConverter.ToUInt16(bytes, 4 + iOffset);
            AnimationIndex = BitConverter.ToUInt16(bytes, 6 + iOffset);
            Unknown1 = BitConverter.ToUInt16(bytes, 8 + iOffset);
            Unknown2 = BitConverter.ToUInt16(bytes, 10 + iOffset);
        }
    }

    public class MM3CureAllInfo : CureAllInfo
    {
        public MMCondition[] Conditions;   // 8 bytes; one per character
        public MMCondition CasterCondition;
        public MM3GameState GameState;
        public Int16[] HitPoints;          // 16 bytes; one per character
        public Int16[] HitPointsMax;       // 16 bytes; one per character
        public UInt16 CasterSpellPoints;
        public UInt32 CasterGems;
        public GenericClass CasterClass;
        public MM345KnownSpells CasterSpells;
        public bool InCombat;
        public bool AntiMagicZone;

        public MM3CureAllInfo()
        {
        }

        public override bool IsHealer
        {
            get
            {
                switch(CasterClass)
                {
                    case GenericClass.Cleric:
                    case GenericClass.Paladin:
                    case GenericClass.Ranger:
                    case GenericClass.Druid:
                        return true;
                    default:
                        return false;

                }
            }
        }

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsIncapacitated { get { return CasterCondition.UnableToCast; } }
        public override bool MagicPermitted { get { return !AntiMagicZone; } }
        public override bool Combat { get { return InCombat; } }
    }

    public class MM3SpellInfo : SpellInfo
    {
        public MM3Spell Spell;
        public MM3PartyInfo Party;

        public MM3SpellInfo()
        {
            Spell = null;
            Party = null;
            Game = new MM3GameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingChar = -1;
            Game.ActingCaster = -1;
            Game.ActingCombatChar = -1;
        }
    }

    public class MM3MapCartography : MM345MapCartography { }

    public class MM3SpecialSquare : MM345SpecialSquare
    {
        public MM3SpecialSquare(byte[] bytes, int iOffset, byte[] bytesActions)
        {
            SetData(bytes, iOffset, bytesActions);
        }
    }

    public class MM3GameState : GameState
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public bool[] IsCaster = null;
        private Subscreen m_subScreen = Subscreen.Unknown;
        public MainState OriginalMain = MainState.Unknown;

        public override bool Casting
        {
            get
            {
                switch (Main)
                {
                    case MainState.CastQuickspell:
                    case MainState.SelectSpell:
                    case MainState.SelectSpellTarget:
                        return true;
                    default:
                        return false;
                }

            }
        }

        public override bool ActingIsCaster
        {
            get
            {
                if (IsCaster == null || ActingCaster < 0 || ActingCaster >= IsCaster.Length)
                    return true;    // Something is wrong; show the spell list by default
                return IsCaster[ActingCaster];
            }
        }

        public override Subscreen Subscreen { get { return m_subScreen; } set { m_subScreen = value; } }
    }

    public enum MM3Map
    {
        None = 0,
        A1FountainHead = 1,
        A2Baywatch = 2,
        B4Wildabar = 3,
        E2SwampTown = 4,
        D3BlisteringHeights = 5,
        A1FountainHeadCavern = 6,
        A2BaywatchCavern = 7,
        B4WildabarCavern = 8,
        E2SwampTownCavern = 9,
        D3BlisteringHeightsCavern = 10,
        B1CyclopsCavern = 11,
        B4ArachnoidCavern = 12,
        D1CursedColdCavern = 13,
        F1DragonCavern = 14,
        E4TheMagicCavern = 15,
        A1AncientTempleOfMoo = 16,
        B1SlithercultStronghold = 17,
        B2FortressOfFear = 18,
        A3HallsOfInsanity = 19,
        B3DarkWarriorKeep = 20,
        B3CathedralOfCarnage = 21,
        F2TombOfTerror = 22,
        F3TheMazeFromHell = 23,
        A2CastleWhiteshield = 24,
        B4CastleBloodReign = 25,
        E1CastleDragontooth = 26,
        C4CastleGreywind = 27,
        D4CastleBlackwind = 28,
        A2WhiteshieldDungeon = 29,
        B4BloodreignDungeon = 30,
        E1DragontoothDungeon = 31,
        C4GreywindDungeon = 32,
        D4BlackwindDungeon = 33,
        F4AlphaEngineSector = 34,
        F2MainEngineSector = 35,
        F1BetaEngineSector = 36,
        A2AftStorageSector = 37,
        C2CentralControlSector = 38,
        A2ForwardStorageSector = 39,
        C2MainControlSector = 40,
        A1Surface = 41,
        A2Surface = 42,
        A3Surface = 43,
        A4Surface = 44,
        B1Surface = 45,
        B2Surface = 46,
        B3Surface = 47,
        B4Surface = 48,
        C1Surface = 49,
        C2Surface = 50,
        C3Surface = 51,
        C4Surface = 52,
        D1Surface = 53,
        D2Surface = 54,
        D3Surface = 55,
        D4Surface = 56,
        E1Surface = 57,
        E2Surface = 58,
        E3Surface = 59,
        E4Surface = 60,
        F1Surface = 61,
        F2Surface = 62,
        F3Surface = 63,
        F4Surface = 64,
        LastMain = 65,
        ItsASecret = 105,
        TheArena = 106,
        Last = 107,
        Unknown = -1
    }

    public class MM3MapData : MM345MapData
    {
        public MM3MapData()
        {
            BytesPerSquare = 2;
            Bounds = new Rectangle(0, 0, 16, 16);
        }
    }

    public class MM3EncounterInfo : MM345EncounterInfo
    {
        public MM3EncounterInfo()
            : base()
        {
            m_monsters = null;
        }

        public override string ExtraText { get { return GetExtraText(MMEffectTag.GetEffectsMM3(ActiveEffects)); } }
    }

    public class MM3PartyInfo : PartyInfo
    {
        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                    return null;
                MemoryStream stream = new MemoryStream(80);
                for (int i = 0; i < NumChars; i++)
                {
                    stream.Write(Bytes, i * CharacterSize + MM3.Offsets.Might, 18);  // Basic stats and modifiers
                    stream.Write(Bytes, i * CharacterSize + MM3.Offsets.FireResist, 12);  // Basic stats and modifiers
                    stream.Write(Bytes, i * CharacterSize + MM3.Offsets.Skills, 80);  // Secondary skills, Awards, Spells
                    stream.Write(Bytes, i * CharacterSize + MM3.Offsets.InventoryBases, 18);  // Backpack items (base only)
                    stream.Write(Bytes, i * CharacterSize + MM3.Offsets.Donations, 1);
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public MM3PartyInfo(byte[] bytes, byte numchars, byte actingChar)
        {
            Bytes = bytes;
            NumChars = numchars;
            ActingChar = actingChar;
            Positions = new byte[numchars];
            Addresses = new int[numchars];
            for (byte i = 0; i < numchars; i++)
            {
                Positions[i] = i;
                Addresses[i] = i;
            }
        }

        public bool HasItem(MM3ItemIndex index)
        {
            return ItemCount(index) > 0;
        }

        public int ItemCount(MM3ItemIndex index)
        {
            int iCount = 0;
            if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                return iCount;
            for (int iChar = 0; iChar < NumChars; iChar++)
            {
                for (int iOffset = 0; iOffset < 18; iOffset++)
                {
                    if (Bytes[iChar * CharacterSize + MM3.Offsets.InventoryBases + iOffset] == (byte)index)
                        iCount++;
                }
            }
            return iCount;
        }

        public int CharsWithStatus(BasicConditionFlags cond)
        {
            int iCount = 0;
            if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                return iCount;
            for (int iChar = 0; iChar < NumChars; iChar++)
            {
                if (Bytes[iChar * CharacterSize + MM3Character.ConditionOffset(cond)] > 0)
                    iCount++;
            }
            return iCount;
        }

        public bool HasAward(MM3AwardIndex index)
        {
            if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                return false;
            for (int iChar = 0; iChar < NumChars; iChar++)
            {
                if (Bytes[iChar * CharacterSize + MM3.Offsets.Awards + (int) index] > 0)
                    return true;
            }
            return false;
        }

        public override int CharacterSize { get { return MM3Character.SizeInBytes; } }
    }

    public class MM3BackpackBytes : MMBackpackBytes
    {
        public byte[] Equipped;
        public byte[] Charges;
        public byte[] Element;
        public byte[] Material;
        public byte[] Attribute;
        public byte[] Base;
        public byte[] Property;

        public byte[][] All { get { return new byte[][] { Equipped, Charges, Element, Material, Attribute, Base, Property }; } }

        public MM3BackpackBytes()
        {
            Equipped = new byte[18];
            Charges = new byte[18];
            Element = new byte[18];
            Material = new byte[18];
            Attribute = new byte[18];
            Base = new byte[18];
            Property = new byte[18];

            foreach (byte[] bytes in All)
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = 0;
        }

        public void InitNull()
        {
            Equipped = Global.NullBytes(18);
            Charges = Global.NullBytes(18);
            Element = Global.NullBytes(18);
            Material = Global.NullBytes(18);
            Attribute = Global.NullBytes(18);
            Base = Global.NullBytes(18);
            Property = Global.NullBytes(18);
        }

        public static MM3BackpackBytes Create(List<Item> items)
        {
            MM3BackpackBytes backpack = new MM3BackpackBytes();
            backpack.SetFromList(items);
            return backpack;
        }

        public void SetFromList(List<Item> items)
        {
            InitNull();

            foreach (Item item in items)
                Add(item);
        }

        public bool Replace(MM3Item item, int iPosition)
        {
            if (iPosition >= 18)
                return false;

            Base[iPosition] = (byte)item.Base;
            Equipped[iPosition] = (byte)item.WhereEquipped;
            Charges[iPosition] = item.ChargesByte;
            Element[iPosition] = (byte)item.Element;
            Material[iPosition] = (byte)item.Material;
            Attribute[iPosition] = (byte)item.Attribute;
            Property[iPosition] = (byte)item.Property;
            return true;
        }

        public override bool Add(Item item)
        {
            MM3Item mmItem = item as MM3Item;
            if (mmItem == null)
                return false;

            if (mmItem.MemoryIndex < Base.Length && Base[mmItem.MemoryIndex] == 0)
                return Replace(mmItem, mmItem.MemoryIndex);

            // Place this item in the first empty slot
            for (int i = 0; i < Base.Length; i++)
            {
                if (Base[i] == 0)
                    return Replace(mmItem, i);
            }

            return false;
        }

        public override bool Add(List<Item> items)
        {
            bool bAllSucceeded = true;
            foreach (Item item in items)
            {
                if (!Add(item))
                    bAllSucceeded = false;
            }
            return bAllSucceeded;
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(19 * 7);
            foreach (byte[] bytes in All)
            {
                ms.Write(bytes, 0, 18);
                ms.WriteByte(0);
            }

            return ms.ToArray();
        }
    }

    public class MM3String : MM345String
    {
    }

    public class MM3MapBytes
    {

        public const int OffsetCurrentMap = 0x000;
        public const int OffsetCurrentQuad = 0x011;
        public const int OffsetQuad1 = 0x027;
        public const int OffsetQuad2 = OffsetQuad1 + 0x340;
        public const int OffsetQuad3 = OffsetQuad2 + 0x340;
        public const int OffsetQuad4 = OffsetQuad3 + 0x340;
        public const int Size = OffsetQuad4 + 0x340;

        public byte[] RawBytes;

        public byte CurrentMapIndex;
        public byte CurrentQuadIndex;

        public class Quad
        {
            public const int OffsetAppearance = 0x000;
            public const int OffsetAttributes = 0x200;
            public const int OffsetUnkVal1 = 0x300;
            public const int OffsetUnkVal2 = 0x301;
            public const int OffsetUnkVal3 = 0x302;
            public const int OffsetUnkVal4 = 0x303;
            public const int OffsetUnkVal5 = 0x304;
            public const int OffsetUnkVal6 = 0x305;
            public const int OffsetUnkVal7 = 0x306;
            public const int OffsetUnkVal8 = 0x307;
            public const int OffsetMapWest = 0x308;
            public const int OffsetMapSouth = 0x309;
            public const int OffsetMapEast = 0x30A;
            public const int OffsetMapNorth = 0x30B;
            public const int OffsetAllowSave = 0x30C;
            public const int OffsetHasLight = 0x30D;
            public const int OffsetAllowRest = 0x30E;
            public const int OffsetUnkVal9 = 0x30F;
            public const int OffsetUnkVal10 = 0x310;
            public const int OffsetLockStrength = 0x311;
            public const int OffsetWallStrength = 0x312;
            public const int OffsetRunSquare = 0x313;
            public const int OffsetAllowTeleport = 0x314;
            public const int OffsetAllowBeacon = 0x315;
            public const int OffsetAllowDistortion = 0x316;
            public const int OffsetAllowShelter = 0x317;
            public const int OffsetAllowPortal = 0x318;
            public const int OffsetAllowGate = 0x319;
            public const int OffsetAllowEtherealize = 0x31A;
            public const int OffsetUnkVal12 = 0x31B;
            public const int OffsetGrateStrength = 0x31C;
            public const int OffsetUnkVal14 = 0x31D;
            public const int OffsetTrapStrength = 0x31E;
            public const int OffsetUnkVal15 = 0x31F;
            public const int OffsetCartography = 0x320;

            public byte[] Appearance;   // 512 bytes
            public byte[] Attributes;   // 256 bytes
            public byte MapWest;
            public byte MapSouth;
            public byte MapEast;
            public byte MapNorth;
            public byte AllowSave;
            public byte HasLight;
            public byte AllowRest;
            public byte LockStrength;
            public byte WallStrength;
            public byte AllowTeleport;
            public byte AllowLloydsBeacon;
            public byte AllowTimeDistortion;
            public byte AllowSuperShelter;
            public byte AllowTownPortal;
            public byte AllowNaturesGate;
            public byte AllowEtherealize;
            public byte GrateStrength;
            public byte TrapStrength;
            public byte[] Cartography;     // 32 bytes

            public byte UnknownByte01;
            public byte UnknownByte02;
            public byte UnknownByte03;
            public byte UnknownByte04;
            public byte UnknownByte05;
            public byte UnknownByte06;
            public byte UnknownByte07;
            public byte UnknownByte08;
            public byte UnknownByte09;
            public byte UnknownByte10;
            public byte RunSquareByte;
            public byte UnknownByte12;
            public byte UnknownByte14;
            public byte UnknownByte15;

            public Point RunSquare { get { return new Point(RunSquareByte & 0xf, RunSquareByte >> 4); } }

            public Quad(byte[] bytes, int offset)
            {
                Appearance = Global.Subset(bytes, offset + OffsetAppearance, 512);
                Attributes = Global.Subset(bytes, offset + OffsetAttributes, 256);
                MapWest = bytes[offset + OffsetMapWest];
                MapSouth = bytes[offset + OffsetMapSouth];
                MapEast = bytes[offset + OffsetMapEast];
                MapNorth = bytes[offset + OffsetMapNorth];
                AllowSave = bytes[offset + OffsetAllowSave];
                HasLight = bytes[offset + OffsetHasLight];
                AllowRest = bytes[offset + OffsetAllowRest];
                LockStrength = bytes[offset + OffsetLockStrength];
                WallStrength = bytes[offset + OffsetWallStrength];
                AllowTeleport = bytes[offset + OffsetAllowTeleport];
                AllowLloydsBeacon = bytes[offset + OffsetAllowBeacon];
                AllowTimeDistortion = bytes[offset + OffsetAllowDistortion];
                AllowSuperShelter = bytes[offset + OffsetAllowShelter];
                AllowTownPortal = bytes[offset + OffsetAllowPortal];
                AllowNaturesGate = bytes[offset + OffsetAllowGate];
                AllowEtherealize = bytes[offset + OffsetAllowEtherealize];
                TrapStrength = bytes[offset + OffsetTrapStrength];
                GrateStrength = bytes[offset + OffsetGrateStrength];
                RunSquareByte = bytes[offset + OffsetRunSquare];
                Cartography = Global.Subset(bytes, offset + OffsetCartography, 32);

                UnknownByte01 = bytes[offset + OffsetUnkVal1];
                UnknownByte02 = bytes[offset + OffsetUnkVal2];
                UnknownByte03 = bytes[offset + OffsetUnkVal3];
                UnknownByte04 = bytes[offset + OffsetUnkVal4];
                UnknownByte05 = bytes[offset + OffsetUnkVal5];
                UnknownByte06 = bytes[offset + OffsetUnkVal6];
                UnknownByte07 = bytes[offset + OffsetUnkVal7];
                UnknownByte08 = bytes[offset + OffsetUnkVal8];
                UnknownByte09 = bytes[offset + OffsetUnkVal9];
                UnknownByte10 = bytes[offset + OffsetUnkVal10];
                UnknownByte12 = bytes[offset + OffsetUnkVal12];
                UnknownByte14 = bytes[offset + OffsetUnkVal14];
                UnknownByte15 = bytes[offset + OffsetUnkVal15];
            }
        }

        public Quad Quad1;
        public Quad Quad2;
        public Quad Quad3;
        public Quad Quad4;

        public Quad CurrentQuad
        {
            get
            {
                switch (CurrentQuadIndex)
                {
                    case 0: return Quad1;
                    case 1: return Quad2;
                    case 2: return Quad3;
                    case 3: return Quad4;
                    default: return null;
                }
            }
        }

        public int CurrentQuadOffset
        {
            get
            {
                switch (CurrentQuadIndex)
                {
                    case 0: return OffsetQuad1;
                    case 1: return OffsetQuad2;
                    case 2: return OffsetQuad3;
                    case 3: return OffsetQuad4;
                    default: return OffsetQuad1;
                }
            }
        }

        public MM3MapBytes(byte[] bytes)
        {
            RawBytes = bytes;

            CurrentMapIndex = bytes[OffsetCurrentMap];
            CurrentQuadIndex = bytes[OffsetCurrentQuad];

            Quad1 = new Quad(bytes, OffsetQuad1);
            Quad2 = new Quad(bytes, OffsetQuad2);
            Quad3 = new Quad(bytes, OffsetQuad3);
            Quad4 = new Quad(bytes, OffsetQuad4);
        }
    }

    public class MM3PartyBytes
    {
        public const int OffsetPartyCount = 0x00;
        public const int OffsetRealPartyCount = 0x01;
        public const int OffsetPartyMembers = 0x02;
        public const int OffsetFacing = 0x0A;
        public const int OffsetLocationX = 0x0B;
        public const int OffsetLocationY = 0x0C;
        public const int OffsetMapIndex = 0x0D;
        public const int OffsetSoundFlag = 0x0E;
        public const int OffsetMusicFlag = 0x0F;
        public const int OffsetDelay = 0x10;
        public const int OffsetLastMap = 0x11;
        public const int OffsetLevitate = 0x12;
        public const int OffsetAutomap = 0x13;
        public const int OffsetWizardEye = 0x14;
        public const int OffsetWalkOnWater = 0x15;
        public const int OffsetUnkVal1 = 0x16;
        public const int OffsetShopInventories = 0x17;  // 810 bytes
        public const int OffsetUnkVal2 = 0x341;
        public const int OffsetUnkVal3 = 0x342;
        public const int OffsetUnkVal4 = 0x343;
        public const int OffsetUnkVal5 = 0x344;
        public const int OffsetUnkVal6 = 0x345;
        public const int OffsetUnkVal7 = 0x346;
        public const int OffsetUnkVal8 = 0x347;
        public const int OffsetUnkVal9 = 0x348;
        public const int OffsetUnkVal10 = 0x349;
        public const int OffsetUnkVal11 = 0x34a;
        public const int OffsetDay = 0x34B;
        public const int OffsetYear = 0x34C;
        public const int OffsetLight = 0x34E;
        public const int OffsetResistFire = 0x350;
        public const int OffsetResistElec = 0x352;
        public const int OffsetResistCold = 0x354;
        public const int OffsetResistPoison = 0x356;
        public const int OffsetMinutes = 0x358;
        public const int OffsetFood = 0x35A;
        public const int OffsetUnkVal12 = 0x35c;
        public const int OffsetUnkVal13 = 0x35d;
        public const int OffsetUnkVal14 = 0x35e;
        public const int OffsetUnkVal15 = 0x35f;
        public const int OffsetUnkVal16 = 0x360;
        public const int OffsetUnkVal17 = 0x361;
        public const int OffsetBankGold = 0x362;
        public const int OffsetBankGems = 0x366;
        public const int OffsetGold = 0x36A;
        public const int OffsetGems = 0x36E;
        public const int OffsetUnkVal18 = 0x372;
        public const int OffsetUnkVal19 = 0x373;
        public const int OffsetUnkVal20 = 0x374;
        public const int OffsetUnkVal21 = 0x375;
        //public const int OffsetSecondsPlayed = 0x28E;
        //public const int OffsetRested = 0x292;
        public const int OffsetPartyBits = 0x376;

        public const int Size = OffsetPartyBits + 32;

        public byte[] RawBytes;

        public byte PartyCount;
        public byte RealPartyCount;
        public byte[] arrayPartyMembers; // 8 bytes
        public byte Facing;
        public byte LocationX;
        public byte LocationY;
        public byte MapIndex;
        public byte SoundFlag;
        public byte MusicFlag;
        public byte Delay;
        public byte LastMap;
        public byte Levitate;
        public byte Automap;
        public byte WizardEye;
        public byte WalkOnWater;
        public byte[] ShopInventory;     // 810 bytes
        public byte Day;
        public UInt16 Year;
        public UInt16 Minutes;
        public UInt16 Food;
        public UInt16 Light;
        public UInt16 ResistFire;
        public UInt16 ResistElec;
        public UInt16 ResistCold;
        public UInt16 ResistPoison;
        public UInt32 Gold;
        public UInt32 Gems;
        public UInt32 BankGold;
        public UInt32 BankGems;
        public byte[] PartyBits;        // 32 bytes

        public byte UnknownByte1;
        public byte UnknownByte2;
        public byte UnknownByte3;
        public byte UnknownByte4;
        public byte UnknownByte5;
        public byte UnknownByte6;
        public byte UnknownByte7;
        public byte UnknownByte8;
        public byte UnknownByte9;
        public byte UnknownByte10;
        public byte UnknownByte11;
        public byte UnknownByte12;
        public byte UnknownByte13;
        public byte UnknownByte14;
        public byte UnknownByte15;
        public byte UnknownByte16;
        public byte UnknownByte17;
        public byte UnknownByte18;
        public byte UnknownByte19;
        public byte UnknownByte20;
        public byte UnknownByte21;

        public MM3PartyBytes(byte[] bytes)
        {
            RawBytes = bytes;

            PartyCount = bytes[OffsetPartyCount];
            RealPartyCount = bytes[OffsetRealPartyCount];
            arrayPartyMembers = Global.Subset(bytes, OffsetPartyMembers, 8);
            Facing = bytes[OffsetFacing];
            LocationX = bytes[OffsetLocationX];
            LocationY = bytes[OffsetLocationY];
            MapIndex = bytes[OffsetMapIndex];
            SoundFlag = bytes[OffsetSoundFlag];
            MusicFlag = bytes[OffsetMusicFlag];
            Delay = bytes[OffsetDelay];
            LastMap = bytes[OffsetLastMap];
            Levitate = bytes[OffsetLevitate];
            Automap = bytes[OffsetAutomap];
            WizardEye = bytes[OffsetWizardEye];
            WalkOnWater = bytes[OffsetWalkOnWater];
            ShopInventory = Global.Subset(bytes, OffsetShopInventories, 810);
            Day = bytes[OffsetDay];
            Year = BitConverter.ToUInt16(bytes, OffsetYear);
            Minutes = BitConverter.ToUInt16(bytes, OffsetMinutes);
            Food = BitConverter.ToUInt16(bytes, OffsetFood);
            Light = BitConverter.ToUInt16(bytes, OffsetLight);
            ResistFire = BitConverter.ToUInt16(bytes, OffsetResistFire);
            ResistElec = BitConverter.ToUInt16(bytes, OffsetResistElec);
            ResistCold = BitConverter.ToUInt16(bytes, OffsetResistCold);
            ResistPoison = BitConverter.ToUInt16(bytes, OffsetResistPoison);
            Gold = BitConverter.ToUInt32(bytes, OffsetGold);
            Gems = BitConverter.ToUInt32(bytes, OffsetGems);
            BankGold = BitConverter.ToUInt32(bytes, OffsetBankGold);
            BankGems = BitConverter.ToUInt32(bytes, OffsetBankGems);
            PartyBits = Global.Subset(bytes, OffsetPartyBits, 32);

            UnknownByte1 = bytes[OffsetUnkVal1];
            UnknownByte2 = bytes[OffsetUnkVal2];
            UnknownByte3 = bytes[OffsetUnkVal3];
            UnknownByte4 = bytes[OffsetUnkVal4];
            UnknownByte5 = bytes[OffsetUnkVal5];
            UnknownByte6 = bytes[OffsetUnkVal6];
            UnknownByte7 = bytes[OffsetUnkVal7];
            UnknownByte8 = bytes[OffsetUnkVal8];
            UnknownByte9 = bytes[OffsetUnkVal9];
            UnknownByte10 = bytes[OffsetUnkVal10];
            UnknownByte11 = bytes[OffsetUnkVal11];
            UnknownByte12 = bytes[OffsetUnkVal12];
            UnknownByte13 = bytes[OffsetUnkVal13];
            UnknownByte14 = bytes[OffsetUnkVal14];
            UnknownByte15 = bytes[OffsetUnkVal15];
            UnknownByte16 = bytes[OffsetUnkVal16];
            UnknownByte17 = bytes[OffsetUnkVal17];
            UnknownByte18 = bytes[OffsetUnkVal18];
            UnknownByte19 = bytes[OffsetUnkVal19];
            UnknownByte20 = bytes[OffsetUnkVal20];
            UnknownByte21 = bytes[OffsetUnkVal21];
        }
    }

    public class MM3GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return MM3MemoryHacker.GetMapTitlePair(iMap); }

        public MM3GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offset, type, mask, style, fn)
        {
        }

        public MM3GameInfoItem(string desc, object val, int offset, BitDescriptionDelegate fn)
            : base(desc, val, offset, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }

        public override string FacingString(int i, bool bAbbrev = false) { return MM3MemoryHacker.FacingString(i, bAbbrev); }
    }

    public class MM3GameInfo : MM345GameInfo
    {
        public MM3MapBytes Map;
        public MM3PartyBytes Party;
        public int CurrentQuad;

        public override GameNames Game { get { return GameNames.MightAndMagic3; } }

        public MM3GameInfo(byte[] map, byte[] party)
        {
            SetMapBytes(map);
            SetPartyBytes(party);
            Bytes = Global.Combine(RawMap, RawParty);
        }

        public void SetMapBytes(byte[] bytes)
        {
            RawMap = bytes;
            Map = new MM3MapBytes(bytes);
        }

        public void SetPartyBytes(byte[] bytes)
        {
            RawParty = bytes;
            Party = new MM3PartyBytes(bytes);
        }

        public override string GameTime(bool bFull)
        {
            return GetGameTimeString(Party.Year, Party.Day, Party.Minutes, bFull);
        }

        public override List<GameInfoItem> GetPartyItems()
        {
            int offset = MM3Memory.PartyBytes;

            List<GameInfoItem> items = new List<GameInfoItem>();

            {
                items.Add(new MM3GameInfoItem("Gold", Party.Gold, offset + MM3PartyBytes.OffsetGold));
                items.Add(new MM3GameInfoItem("Gems", Party.Gems, offset + MM3PartyBytes.OffsetGems));
                items.Add(new MM3GameInfoItem("Food", Party.Food, offset + MM3PartyBytes.OffsetFood));
                items.Add(new MM3GameInfoItem("Bank Gold", Party.BankGold, offset + MM3PartyBytes.OffsetBankGold));
                items.Add(new MM3GameInfoItem("Bank Gems", Party.BankGems, offset + MM3PartyBytes.OffsetBankGems));
                items.Add(new MM3GameInfoItem("Year", Party.Year, offset + MM3PartyBytes.OffsetYear));
                items.Add(new MM3GameInfoItem("Day", Party.Day, offset + MM3PartyBytes.OffsetDay));
                items.Add(new MM3GameInfoItem("Time", Party.Minutes, offset + MM3PartyBytes.OffsetMinutes, DataType.Time));
                items.Add(new MM3GameInfoItem("Levitate", Party.Levitate, offset + MM3PartyBytes.OffsetLevitate, DataType.Boolean));
                items.Add(new MM3GameInfoItem("Water Walk", Party.WalkOnWater, offset + MM3PartyBytes.OffsetWalkOnWater, DataType.Boolean));
                items.Add(new MM3GameInfoItem("Light", Party.Light, offset + MM3PartyBytes.OffsetLight));
                items.Add(new MM3GameInfoItem("Fire Res", Party.ResistFire, offset + MM3PartyBytes.OffsetResistFire));
                items.Add(new MM3GameInfoItem("Elec Res", Party.ResistElec, offset + MM3PartyBytes.OffsetResistElec));
                items.Add(new MM3GameInfoItem("Cold Res", Party.ResistCold, offset + MM3PartyBytes.OffsetResistCold));
                items.Add(new MM3GameInfoItem("Poison Res", Party.ResistPoison, offset + MM3PartyBytes.OffsetResistPoison));
                items.Add(new MM3GameInfoItem("Party Bits", Party.PartyBits, offset + MM3PartyBytes.OffsetPartyBits, MM3Bits.PartyDescription));

                if (Global.Debug)
                {
                    items.Add(new MM3GameInfoItem("Automap", Party.Automap, offset + MM3PartyBytes.OffsetAutomap, DataType.Boolean));
                    items.Add(new MM3GameInfoItem("Wizard Eye", Party.WizardEye, offset + MM3PartyBytes.OffsetWizardEye, DataType.Boolean));
                    items.Add(new MM3GameInfoItem("Party Size", Party.PartyCount, offset + MM3PartyBytes.OffsetPartyCount, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                    items.Add(new MM3GameInfoItem("Player Chars", Party.RealPartyCount, offset + MM3PartyBytes.OffsetRealPartyCount, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                    items.Add(new MM3GameInfoItem("Char Indices", Party.arrayPartyMembers, offset + MM3PartyBytes.OffsetPartyMembers, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                    items.Add(new MM3GameInfoItem("Facing", Party.Facing, offset + MM3PartyBytes.OffsetFacing, DataType.Facing));
                    items.Add(new MM3GameInfoItem("Location:X", Party.LocationX, offset + MM3PartyBytes.OffsetLocationX));
                    items.Add(new MM3GameInfoItem("Location:Y", Party.LocationY, offset + MM3PartyBytes.OffsetLocationY));
                    items.Add(new MM3GameInfoItem("Last Map", Party.LastMap, offset + MM3PartyBytes.OffsetLastMap));
                    items.Add(new MM3GameInfoItem("Sound Flag", Party.SoundFlag, offset + MM3PartyBytes.OffsetSoundFlag, DataType.Boolean));
                    items.Add(new MM3GameInfoItem("Music Flag", Party.MusicFlag, offset + MM3PartyBytes.OffsetMusicFlag, DataType.Boolean));
                    items.Add(new MM3GameInfoItem("Delay", Party.Delay, offset + MM3PartyBytes.OffsetDelay));

                    items.Add(new MM3GameInfoItem("Unk1", Party.UnknownByte1, offset + MM3PartyBytes.OffsetUnkVal1));
                    items.Add(new MM3GameInfoItem("Unk2", Party.UnknownByte2, offset + MM3PartyBytes.OffsetUnkVal2));
                    items.Add(new MM3GameInfoItem("Unk3", Party.UnknownByte3, offset + MM3PartyBytes.OffsetUnkVal3));
                    items.Add(new MM3GameInfoItem("Unk4", Party.UnknownByte4, offset + MM3PartyBytes.OffsetUnkVal4));
                    items.Add(new MM3GameInfoItem("Unk5", Party.UnknownByte5, offset + MM3PartyBytes.OffsetUnkVal5));
                    items.Add(new MM3GameInfoItem("Unk6", Party.UnknownByte6, offset + MM3PartyBytes.OffsetUnkVal6));
                    items.Add(new MM3GameInfoItem("Unk7", Party.UnknownByte7, offset + MM3PartyBytes.OffsetUnkVal7));
                    items.Add(new MM3GameInfoItem("Unk8", Party.UnknownByte8, offset + MM3PartyBytes.OffsetUnkVal8));
                    items.Add(new MM3GameInfoItem("Unk9", Party.UnknownByte9, offset + MM3PartyBytes.OffsetUnkVal9));
                    items.Add(new MM3GameInfoItem("Unk10", Party.UnknownByte10, offset + MM3PartyBytes.OffsetUnkVal10));
                    items.Add(new MM3GameInfoItem("Unk11", Party.UnknownByte11, offset + MM3PartyBytes.OffsetUnkVal11));
                    items.Add(new MM3GameInfoItem("Unk12", Party.UnknownByte12, offset + MM3PartyBytes.OffsetUnkVal12));
                    items.Add(new MM3GameInfoItem("Unk13", Party.UnknownByte13, offset + MM3PartyBytes.OffsetUnkVal13));
                    items.Add(new MM3GameInfoItem("Unk14", Party.UnknownByte14, offset + MM3PartyBytes.OffsetUnkVal14));
                    items.Add(new MM3GameInfoItem("Unk15", Party.UnknownByte15, offset + MM3PartyBytes.OffsetUnkVal15));
                    items.Add(new MM3GameInfoItem("Unk16", Party.UnknownByte16, offset + MM3PartyBytes.OffsetUnkVal16));
                    items.Add(new MM3GameInfoItem("Unk17", Party.UnknownByte17, offset + MM3PartyBytes.OffsetUnkVal17));
                    items.Add(new MM3GameInfoItem("Unk18", Party.UnknownByte18, offset + MM3PartyBytes.OffsetUnkVal18));
                    items.Add(new MM3GameInfoItem("Unk19", Party.UnknownByte19, offset + MM3PartyBytes.OffsetUnkVal19));
                    items.Add(new MM3GameInfoItem("Unk20", Party.UnknownByte20, offset + MM3PartyBytes.OffsetUnkVal20));
                    items.Add(new MM3GameInfoItem("Unk21", Party.UnknownByte21, offset + MM3PartyBytes.OffsetUnkVal21));
                }
            }
            return items;
        }

        private byte ByteFromMapLocation(Point pt)
        {
            return (byte)((pt.Y << 4) | pt.X);
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int offset = MM3Memory.MapBytes;

            List<GameInfoItem> items = new List<GameInfoItem>();
            MM3MapBytes.Quad quad = Map.CurrentQuad;
            if (quad == null)
                return items;

            offset += Map.CurrentQuadOffset;

            items.Add(new MM3GameInfoItem("Can Portal", quad.AllowTownPortal, offset + MM3MapBytes.Quad.OffsetAllowPortal, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Etherealize", quad.AllowEtherealize, offset + MM3MapBytes.Quad.OffsetAllowEtherealize, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Shelter", quad.AllowSuperShelter, offset + MM3MapBytes.Quad.OffsetAllowShelter, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can TimeDistort", quad.AllowTimeDistortion, offset + MM3MapBytes.Quad.OffsetAllowDistortion, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Beacon", quad.AllowLloydsBeacon, offset + MM3MapBytes.Quad.OffsetAllowBeacon, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Teleport", quad.AllowTeleport, offset + MM3MapBytes.Quad.OffsetAllowTeleport, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Rest", quad.AllowRest, offset + MM3MapBytes.Quad.OffsetAllowRest, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Can Save", quad.AllowSave, offset + MM3MapBytes.Quad.OffsetAllowSave, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Has Light", quad.HasLight, offset + MM3MapBytes.Quad.OffsetHasLight, DataType.Boolean));
            items.Add(new MM3GameInfoItem("Lock Strength", quad.LockStrength, offset + MM3MapBytes.Quad.OffsetLockStrength));
            items.Add(new MM3GameInfoItem("Wall Strength", quad.WallStrength, offset + MM3MapBytes.Quad.OffsetWallStrength));
            items.Add(new MM3GameInfoItem("Grate Strength", quad.GrateStrength, offset + MM3MapBytes.Quad.OffsetGrateStrength));
            items.Add(new MM3GameInfoItem("Trap Strength", quad.TrapStrength, offset + MM3MapBytes.Quad.OffsetTrapStrength));
                items.Add(new MM3GameInfoItem("Run", (byte)ByteFromMapLocation(quad.RunSquare), offset + MM3MapBytes.Quad.OffsetRunSquare, DataType.Point8));

            if (Global.Debug)
            {
                items.Add(new MM3GameInfoItem("Unk01", quad.UnknownByte01, offset + MM3MapBytes.Quad.OffsetUnkVal1));
                items.Add(new MM3GameInfoItem("Unk02", quad.UnknownByte02, offset + MM3MapBytes.Quad.OffsetUnkVal2));
                items.Add(new MM3GameInfoItem("Unk03", quad.UnknownByte03, offset + MM3MapBytes.Quad.OffsetUnkVal3));
                items.Add(new MM3GameInfoItem("Unk04", quad.UnknownByte04, offset + MM3MapBytes.Quad.OffsetUnkVal4));
                items.Add(new MM3GameInfoItem("Unk05", quad.UnknownByte05, offset + MM3MapBytes.Quad.OffsetUnkVal5));
                items.Add(new MM3GameInfoItem("Unk06", quad.UnknownByte06, offset + MM3MapBytes.Quad.OffsetUnkVal6));
                items.Add(new MM3GameInfoItem("Unk07", quad.UnknownByte07, offset + MM3MapBytes.Quad.OffsetUnkVal7));
                items.Add(new MM3GameInfoItem("Unk08", quad.UnknownByte08, offset + MM3MapBytes.Quad.OffsetUnkVal8));
                items.Add(new MM3GameInfoItem("Unk09", quad.UnknownByte09, offset + MM3MapBytes.Quad.OffsetUnkVal9));
                items.Add(new MM3GameInfoItem("Unk10", quad.UnknownByte10, offset + MM3MapBytes.Quad.OffsetUnkVal10));
                items.Add(new MM3GameInfoItem("Unk12", quad.UnknownByte12, offset + MM3MapBytes.Quad.OffsetUnkVal12));
                items.Add(new MM3GameInfoItem("Unk14", quad.UnknownByte14, offset + MM3MapBytes.Quad.OffsetUnkVal14));
                items.Add(new MM3GameInfoItem("Unk15", quad.UnknownByte15, offset + MM3MapBytes.Quad.OffsetUnkVal15));
            }

            return items;
        }

        public override List<GameInfoItem> GetMapItems()
        {
            int offset = MM3Memory.MapBytes;

            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new MM3GameInfoItem("Map Index", Map.CurrentMapIndex, offset + MM3MapBytes.OffsetCurrentMap, DataType.Map8, 0, GameInfoItem.ShowStyle.Visible));
            if (Global.Debug)
            {
                items.Add(new MM3GameInfoItem("Map Quad", Map.CurrentQuadIndex, offset + MM3MapBytes.OffsetCurrentQuad, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
            }

            MM3MapBytes.Quad quad = Map.CurrentQuad;
            if (quad == null)
                return items;

            offset += Map.CurrentQuadOffset;

            if (Global.Debug)
            {
                items.Add(new MM3GameInfoItem("Map North", quad.MapNorth, offset + MM3MapBytes.Quad.OffsetMapNorth, DataType.Map8));
                items.Add(new MM3GameInfoItem("Map East", quad.MapEast, offset + MM3MapBytes.Quad.OffsetMapEast, DataType.Map8));
                items.Add(new MM3GameInfoItem("Map South", quad.MapSouth, offset + MM3MapBytes.Quad.OffsetMapSouth, DataType.Map8));
                items.Add(new MM3GameInfoItem("Map West", quad.MapWest, offset + MM3MapBytes.Quad.OffsetMapWest, DataType.Map8));
                items.Add(new MM3GameInfoItem("Cartography", quad.Cartography, offset + MM3MapBytes.Quad.OffsetCartography));
            }

            return items;
        }

    }

    public class MM3MemoryHacker : MM345MemoryHacker
    {
        public MM3MemoryHacker()
        {
            m_game = GameNames.MightAndMagic3;
        }

        protected override void OnReinitialized(EventArgs e)
        {
            MM3.MonsterList.Value.Reinitialize(this, false);
            if (MM3.MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
            base.OnReinitialized(e);
        }

        private byte[] m_lastMapAppearance;
        private MM3RosterFile m_mm3Roster = null;
        private WatchedFile m_fileCurrentData = null;
        private int m_lastQuestMap = -1;
        private MM3FileQuestInfo m_fileQuestInfo = new MM3FileQuestInfo();
        private MonsterLocations m_lastMonsterLocations = null;
        private byte[] m_lastMonsterBytesY = null;
        private byte[] m_lastMonsterBytesX = null;
        private byte[] m_lastMonsterBytesHP = null;
        private byte[] m_lastMonsterBytesActive = null;
        private byte[] m_lastMonsterBytesConditions = null;
        private byte[] m_lastMonsterBytesIndices = null;
        private Point m_lastMonsterParty = Global.NullPoint;

        public override byte[] MainSearch { get { return MM3Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return MM3Memory.Guesses; } }
        public override int IntroTimeout { get { return 8000; } }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            MM3SpellInfo info = new MM3SpellInfo();
            IntPtr pRead = IntPtr.Zero;
            info.Game = ReadMM3GameState();

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = ReadMM3PartyInfo();

            info.Game.ActingChar = info.Party.ActingChar;
            if (info.Game.InCombat)
                info.Game.ActingCaster = info.Party.ActingCombatChar;
            else
                info.Game.ActingCaster = info.Party.ActingCaster;
            return info;
        }

        public override int GetLightDistance()
        {
            if (ReadByte(MM3Memory.LightActive) > 0 || ReadByte(MM3Memory.AreaLight) > 0)
                return 4;
            return 0;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadMM3PartyInfo();
        }

        public override TrainingInfo GetTrainingInfo()
        {
            if (!IsValid)
                return null;

            return null;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            return false;
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            MM3EncounterInfo info = new MM3EncounterInfo();
            if (info == null)
                return null;

            MonsterLocations locations = GetMonsterLocations(bForceNew);
            if (locations == null)
                return null;

            info.Party = ReadMM3PartyInfo();
            if (info.Party == null || info.Party.Bytes == null)
                return null;

            info.CharsMovedThisRound = ReadMM3MovedThisRound();
            info.MonstersMovedThisRound = info.CharsMovedThisRound.Skip(8).ToArray();
            info.ActiveEffects = GetActiveEffects();
            info.MaxEncounterIndex = locations.MaxEncounterIndex;
            info.SetMonsters(locations.Monsters);

            MemoryStream stream = new MemoryStream();
            stream.Write(locations.RawBytes, 0, locations.RawBytes.Length);
            stream.Write(info.Party.Bytes, 0, info.Party.Bytes.Length);
            stream.Write(info.CharsMovedThisRound, 0, info.CharsMovedThisRound.Length);
            byte[] bytes = info.ActiveEffects.MM3Bytes;
            stream.Write(bytes, 0, bytes.Length);

            info.PartyLocation = GetPartyPosition();
            stream.WriteByte((byte) info.PartyLocation.X);
            stream.WriteByte((byte) info.PartyLocation.Y);

            info.AllBytes = stream.ToArray();

            stream.Close();

            return info;
        }

        public override GameState GetGameState()
        {
            return ReadMM3GameState();
        }

        private MainState GetMainState(UInt16 main1, UInt16 main2, UInt16 create)
        {
            switch(main1)
            {
                case 0xfee2:
                case 0xfef2:
                case 0xff06:
                    switch (main2)
                    {
                        case 0xe73c:
                        case 0xece8: return MainState.Adventuring;
                        default: return MainState.SelectSpell;
                    }
                case 0xfeec:
                case 0xff10:
                case 0xff2e:
                case 0xff46:
                case 0xff52:
                    switch (main2)
                    {
                        case 0x3e56:
                        case 0xece8:
                        case 0x5240: return MainState.CastQuickspell;
                        case 0x3aed: return MainState.MainMenu;
                        default: return MainState.Unknown;
                    }
                case 0xfe80:
                case 0xfe86:
                case 0xff48:
                case 0xfe6c: return MainState.Question;
                case 0xfe68: return MainState.Temple;
                case 0xfe70: return MainState.SignIn;
                case 0xff3c: return MainState.Inn;
                case 0xfd1a: return MainState.CreateExchangeStat;
                case 0xff5c: return MainState.Teleport;
                case 0xfd36:
                    return MainState.Inventory;
                case 0xfd2a:
                case 0xfd4a:
                case 0xfd4d:
                    switch(create)
                    {
                        case 0x7217:
                        case 0xff66: return MainState.CreateSelectClass;
                        case 0x1a61: return MainState.CreateSelectAlignment;
                        default: return MainState.CreateSelectClass;
                    }
                case 0xfe06: return MainState.InnAcceptHireling;
                case 0xff78: return MainState.Opening;
                case 0xff84: return MainState.Opening2;
                case 0xfc30: return MainState.LoadGame;
                case 0xff5e: return MainState.Adventuring;
                default:
                    return MainState.Unknown;
            }
        }

        private bool[] ReadIsCaster()
        {
            int iNumChars = ReadByte(MM3Memory.NumChars);
            bool[] result = new bool[iNumChars];
            for (int i = 0; i < iNumChars; i++)
            {
                byte bClass = ReadByte(MM3Memory.PartyInfo + (i * MM3Character.SizeInBytes) + MM3.Offsets.Class);
                result[i] = MM3Character.IsSpellcaster(MM3Character.ClassFromByte(bClass));
            }

            return result;
        }

        private MM3GameState ReadMM3GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as MM3GameState;     // Don't spam the game state from different windows

            MM3GameState state = new MM3GameState();
            state.Location = GetLocationForce();

            UInt16 iMain1 = ReadUInt16(MM3Memory.MainState1);
            UInt16 iMain2 = ReadUInt16(MM3Memory.MainState2);
            UInt16 iCreate = ReadUInt16(MM3Memory.CreateCharState);

            state.IsCaster = ReadIsCaster();

            state.Main = GetMainState(iMain1, iMain2, iCreate);
            state.OriginalMain = state.Main;

            state.Inspecting = false;
            state.InCombat = false;
            state.InShop = false;

            bool bCreatingChar = false;

            switch (state.Main)
            {
                case MainState.CreateExchangeStat:
                case MainState.CreateSelectAlignment:
                case MainState.CreateSelectClass:
                case MainState.CreateSelectName:
                case MainState.CreateSelectRace:
                case MainState.CreateSelectSex:
                    bCreatingChar = true;
                    break;
            }


            switch (iMain1)
            {
                case 0xfee2:
                case 0xfeec:
                case 0xff2c:
                case 0xff38:
                case 0xff40:
                case 0xff46:
                case 0xff2e:
                    switch (iMain2)
                    {
                        case 0x3136:
                            break;
                        default:
                            state.InCombat = true;
                            break;
                    }
                    break;
                case 0xfb32:
                case 0xfb42:
                case 0xfcb0:
                case 0xfca4:
                case 0xfcac:
                case 0xfc7a:
                    state.InShop = true;
                    state.Inspecting = true;
                    break;
                case 0xfd26:
                case 0xfd36:
                case 0xfd62:
                    state.Inspecting = true;
                    state.InCombat = true;
                    break;
                case 0xfc28:
                    state.Inspecting = true;
                    state.Main = MainState.QuickRef;
                    break;
                case 0xfe34:
                    state.Main = MainState.Training;
                    break;
                case 0xfc3e:
                    state.Main = MainState.QuickRef;
                    break;
                case 0xfc84:
                case 0xfef2:
                case 0xfc08:
                case 0xfc18:
                case 0xfc74:
                case 0xfcd4:
                case 0xfcd6:
                case 0xfcc0:
                case 0xfd0c:
                case 0xfd4a:
                case 0xfd50:
                case 0xfd5a:
                case 0xfd86:
                case 0xfd8c:
                case 0xfe42:
                case 0xfe52:
                case 0xfe8a:
                case 0xff06:
                case 0xff10:
                case 0xff30:
                case 0xff52:
                case 0xff54:
                case 0xff48:
                case 0xff4a:
                case 0xfd7a:
                    state.Inspecting = true;
                    if (state.Main != MainState.CastQuickspell && state.Main != MainState.SelectSpell && !bCreatingChar)
                        state.Main = MainState.CharacterScreen;
                    switch (iMain2)
                    {
                        case 0x4e88:
                            state.InShop = true;
                            state.Main = MainState.Inventory;
                            break;
                    }
                    break;
                default:
                    break;
            }

            byte bShopSub = ReadByte(MM3Memory.ShopSubScreen);
            if (bShopSub == 255 || bShopSub == 50)
                state.InShop = false;

            if (state.Location.NumChars == 0 && !bCreatingChar)
            {
                // Don't show various windows if there is no party
                state.Main = MainState.Unknown;
                state.InShop = false;
                state.Inspecting = false;
                state.InCombat = false;
            }

            state.Subscreen = Subscreen.Unknown;

            if (state.InShop)
            {
                switch (bShopSub)
                {
                    case 0:
                        state.Subscreen = Subscreen.Weapons;
                        break;
                    case 1:
                        state.Subscreen = Subscreen.Armor;
                        break;
                    case 2:
                        state.Subscreen = Subscreen.Miscellaneous;
                        break;
                    case 105:
                        switch (ReadByte(MM3Memory.InvInShopDisplayOffset))
                        {
                            case 0:
                            case 97:
                                state.Subscreen = Subscreen.Inventory1;
                                break;
                            case 9:
                                state.Subscreen = Subscreen.Inventory2;
                                break;
                        }
                        break;
                }
            }
            else if (state.Inspecting)
            {
                byte bInspectSub = ReadByte(MM3Memory.InventoryDisplayOffset);
                switch (bInspectSub)
                {
                    case 0:
                    case 68:
                    case 105:
                    case 108:
                        state.Subscreen = Subscreen.Inventory1;
                        break;
                    case 9:
                    case 109:
                    case 32:
                        state.Subscreen = Subscreen.Inventory2;
                        break;
                }
            }

            if (LoadingMap)
            {
                switch (state.Main)
                {
                    case MainState.Training:
                    case MainState.Temple:
                    case MainState.Question:
                    case MainState.CastQuickspell:
                    case MainState.Inn:
                        break;
                    default:
                        state.Main = MainState.LoadingMap;
                        break;
                }
            }

            m_gsCurrent = state;

            return state;
        }

        public override bool LoadingMap 
        {
            get 
            {
                return ReadUInt16(MM3Memory.LoadingMap - m_offsetFoundBlock) == 2 ||
                    ReadByte(MM3Memory.LoadingMapLocal) == 0;
                //ReadByte(MM3Memory.LoadingTownPortal) == 254);
            }
        }

        public override String GetGameTime(bool bFull)
        {
            if (!IsValid)
                return String.Empty;

            MemoryBytes temp = ReadOffset(MM3Memory.Time, 15);
            if (temp == null)
                return String.Empty;

            int iDay = temp[0];
            int iYear = BitConverter.ToUInt16(temp, 1);
            int iMinutes = BitConverter.ToUInt16(temp, 13);

            return MM345GameInfo.GetGameTimeString(iYear, iDay, iMinutes, bFull);
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes pos = ReadOffset(MM3Memory.FacingAndLocation, 3);
            if (pos == null)
                return Global.NullPoint;
            return new Point(pos.Bytes[1], pos.Bytes[2]);
        }

        public LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            MemoryBytes bytes = ReadOffset(MM3Memory.FacingAndLocation, 3);
            if (bytes == null)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(new Point(bytes[1], bytes[2]));
            switch (bytes[0])
            {
                case 0:
                    info.Facing = Direction.Up;
                    break;
                case 1:
                    info.Facing = Direction.Down;
                    break;
                case 2:
                    info.Facing = Direction.Right;
                    break;
                case 3:
                    info.Facing = Direction.Left;
                    break;
                default:
                    info.Facing = Direction.Up;
                    break;
            }

            byte bKey = ReadByte(MM3Memory.LastKeypress);
            switch (bKey)
            {
                case 0xF0: info.LastKeypress = Keys.Left; break;
                case 0xF1: info.LastKeypress = Keys.Right; break;
                case 0xF2: info.LastKeypress = Keys.Up; break;
                case 0xF3: info.LastKeypress = Keys.Down; break;
                case 42: info.LastKeypress = Keys.B; break;
                case 13: info.LastKeypress = Keys.Enter; break;
                default: info.LastKeypress = (Keys)bKey; break;
            }

            info.NumChars = ReadByte(MM3Memory.NumChars);

            info.MapIndex = ReadByte(MM3Memory.CurrentMapIndex);
            info.Town = IsTown((MM3Map)info.MapIndex);

            info.InInn = false;
            switch ((MM3Map)info.MapIndex)
            {
                case MM3Map.A1FountainHead:
                    info.InInn = ((info.PrimaryCoordinates.X == 1 || info.PrimaryCoordinates.X == 2) && info.PrimaryCoordinates.Y == 5);
                    break;
                case MM3Map.A2Baywatch:
                    info.InInn = (info.PrimaryCoordinates.X == 3 && (info.PrimaryCoordinates.Y == 1 || info.PrimaryCoordinates.Y == 2));
                    break;
                case MM3Map.B4Wildabar:
                    info.InInn = (info.PrimaryCoordinates.X == 7 && (info.PrimaryCoordinates.Y == 13 || info.PrimaryCoordinates.Y == 14));
                    break;
                case MM3Map.E2SwampTown:
                    info.InInn = (info.PrimaryCoordinates.X == 3 && (info.PrimaryCoordinates.Y == 9 || info.PrimaryCoordinates.Y == 10));
                    break;
                case MM3Map.D3BlisteringHeights:
                    info.InInn = (info.PrimaryCoordinates.X == 14 && (info.PrimaryCoordinates.Y == 5 || info.PrimaryCoordinates.Y == 6));
                    break;
                default:
                    break;
            }

            info.CanUseBag = (info.Town || Global.Cheats) && !info.InInn;  // Using the bag while in an Inn will result in deleted items!
            info.LightDistance = GetLightDistance();

            return info;
        }

        private byte[] ReadMM3MovedThisRound()
        {
            byte[] bytes = new byte[11] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (!IsValid)
                return bytes;

            ReadOffset(MM3Memory.MovedThisRound, bytes);
            return bytes;
        }

        private MM3PartyInfo ReadMM3PartyInfo()
        {
            if (!IsValid)
                return null;

            if (m_block == null)
                return null;

            // Might and Magic 3 stores the party in marching order and rearranges the entire party if the order is changed

            MM3GameState state = ReadMM3GameState();

            byte numChars = ReadByte(MM3Memory.NumChars);
            if (numChars > 8)  // invalid
                return null;

            MemoryBytes bytes = null;
            if (numChars > 0)
                bytes = ReadOffset(MM3Memory.PartyInfo, MM3Character.SizeInBytes * numChars);

            MM3PartyInfo info = new MM3PartyInfo(bytes, numChars, ReadByte(MM3Memory.ActingCharacter));

            info.ActingCaster = ReadByte(MM3Memory.ActingCaster);
            info.ActingCombatChar = ReadByte(MM3Memory.ActingCombatChar);
            info.InspectingCombatChar = ReadByte(MM3Memory.InspectingCombatChar);

            if (state.Casting)
                info.ActingChar = info.ActingCaster;
            else if (state.InCombat)
                info.ActingChar = info.ActingCombatChar;
            else if (state.Inspecting)
                info.ActingChar = info.InspectingCombatChar;

            return info;
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[2];
            bytes[0] = (byte) ptLocation.X;
            bytes[1] = (byte) ptLocation.Y;
            return WriteOffset(MM3Memory.FacingAndLocation + 1, bytes);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(MM3Memory.PartyInfo + (iAddress * MM3Character.SizeInBytes), bytes);
        }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            if (!(baseChar is MM3Character))
                return;

            // MM3 stores equipped/unequipped in the same list, so we can't just completely randomize the inventory
            MM3Character mm3Char = baseChar as MM3Character;

            int iEquipped = GetEquippedItems(mm3Char.Address).Count;

            // Add random items to fill up or replace the unequipped spaces in the backpack
            List<Item> items = new List<Item>(MaxBackpackSize - iEquipped);

            while(items.Count + iEquipped < MaxBackpackSize)
            {
                MM3Item item = MM3Item.CreateRandom(type, bUsableOnly ? mm3Char : null, bSingleModifierOnly);
                items.Add(item);
            }

            SetBackpack(baseChar.BasicAddress, items);
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            MM345CharCreationInfo info = new MM345CharCreationInfo();
            info.AttributesModified = new byte[7] {0,0,0,0,0,0,0};
            info.AttributesOriginal = new byte[7] {0,0,0,0,0,0,0};
            info.State = ReadMM3GameState();

            switch (info.State.Main)
            {
                case MainState.CreateExchangeStat:
                case MainState.CreateSelectAlignment:
                case MainState.CreateSelectName:
                case MainState.CreateSelectRace:
                case MainState.CreateSelectSex:
                case MainState.CreateSelectClass:
                    ReadOffset(MM3Memory.CreationStats, info.AttributesModified);
                    Buffer.BlockCopy(info.AttributesModified, 0, info.AttributesOriginal, 0, 7);
                    break;
                default:
                    break;
            }
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            if (info is MM345CharCreationInfo)
            {
                if (((MM345CharCreationInfo)info).SwapMightIntellect)
                {
                    byte b = info.AttributesOriginal[0];
                    info.AttributesOriginal[0] = info.AttributesOriginal[1];
                    info.AttributesOriginal[1] = b;
                }

                WriteOffset(MM3Memory.CreationStats, info.AttributesOriginal);
                return true;
            }

            return false;
        }

        public override void RefreshRollScreen()
        {
            // Exchange Might with Intellect.  This takes a while, so swap the values in memory first to save a swap
            byte[] bytes = new byte[2];
            ReadOffset(MM3Memory.CreationStats, bytes);
            byte byte1 = bytes[1];
            bytes[1] = bytes[0];
            bytes[0] = byte1;
            WriteOffset(MM3Memory.CreationStats, bytes);
            SendKeysToDOSBox(new Keys[] { Keys.M, Keys.I });
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((MM3Map)index)
            {
                case MM3Map.A1FountainHead: return new MapTitleInfo(index, "A-1, Fountain Head");
                case MM3Map.A2Baywatch: return new MapTitleInfo(index, "A-2, Baywatch");
                case MM3Map.B4Wildabar: return new MapTitleInfo(index, "B-4, Wildabar");
                case MM3Map.E2SwampTown: return new MapTitleInfo(index, "E-2, Swamp Town");
                case MM3Map.D3BlisteringHeights: return new MapTitleInfo(index, "D-3, Blistering Heights");
                case MM3Map.A1FountainHeadCavern: return new MapTitleInfo(index, "A-1, Fountain Head Cavern");
                case MM3Map.A2BaywatchCavern: return new MapTitleInfo(index, "A-2, Baywatch Cavern");
                case MM3Map.B4WildabarCavern: return new MapTitleInfo(index, "B-4, Wildabar Cavern");
                case MM3Map.E2SwampTownCavern: return new MapTitleInfo(index, "E-2, Swamp Town Cavern");
                case MM3Map.D3BlisteringHeightsCavern: return new MapTitleInfo(index, "D-3, Blistering Heights Cavern");
                case MM3Map.B1CyclopsCavern: return new MapTitleInfo(index, "B-1, Cyclops Cavern");
                case MM3Map.B4ArachnoidCavern: return new MapTitleInfo(index, "B-4, Arachnoid Cavern");
                case MM3Map.D1CursedColdCavern: return new MapTitleInfo(index, "D-1, Cursed Cold Cavern");
                case MM3Map.F1DragonCavern: return new MapTitleInfo(index, "F-1, Dragon Cavern");
                case MM3Map.E4TheMagicCavern: return new MapTitleInfo(index, "E-4, The Magic Cavern");
                case MM3Map.A1AncientTempleOfMoo: return new MapTitleInfo(index, "A-1, Ancient Temple of Moo");
                case MM3Map.B1SlithercultStronghold: return new MapTitleInfo(index, "B-1, Slithercult Stronghold");
                case MM3Map.B2FortressOfFear: return new MapTitleInfo(index, "B-2, Fortress of Fear");
                case MM3Map.A3HallsOfInsanity: return new MapTitleInfo(index, "A-3, Halls of Insanity");
                case MM3Map.B3DarkWarriorKeep: return new MapTitleInfo(index, "B-3, Dark Warrior Keep");
                case MM3Map.B3CathedralOfCarnage: return new MapTitleInfo(index, "B-3, Cathedral of Carnage");
                case MM3Map.F2TombOfTerror: return new MapTitleInfo(index, "F-2, Tomb of Terror");
                case MM3Map.F3TheMazeFromHell: return new MapTitleInfo(index, "F-3, The Maze From Hell");
                case MM3Map.A2CastleWhiteshield: return new MapTitleInfo(index, "A-2, Castle Whiteshield");
                case MM3Map.B4CastleBloodReign: return new MapTitleInfo(index, "B-4, Castle Blood Reign");
                case MM3Map.E1CastleDragontooth: return new MapTitleInfo(index, "E-1, Castle Dragontooth");
                case MM3Map.C4CastleGreywind: return new MapTitleInfo(index, "C-4, Castle Greywind");
                case MM3Map.D4CastleBlackwind: return new MapTitleInfo(index, "D-4, Castle Blackwind");
                case MM3Map.A2WhiteshieldDungeon: return new MapTitleInfo(index, "A-2, Whiteshield Dungeon");
                case MM3Map.B4BloodreignDungeon: return new MapTitleInfo(index, "B-4, Blood Reign Dungeon");
                case MM3Map.E1DragontoothDungeon: return new MapTitleInfo(index, "E-1, Dragontooth Dungeon");
                case MM3Map.C4GreywindDungeon: return new MapTitleInfo(index, "C-4, Greywind Dungeon");
                case MM3Map.D4BlackwindDungeon: return new MapTitleInfo(index, "D-4, Blackwind Dungeon");
                case MM3Map.F4AlphaEngineSector: return new MapTitleInfo(index, "F-4, Alpha Engine Sector");
                case MM3Map.F2MainEngineSector: return new MapTitleInfo(index, "F-2, Main Engine Sector");
                case MM3Map.F1BetaEngineSector: return new MapTitleInfo(index, "F-1, Beta Engine Sector");
                case MM3Map.A2AftStorageSector: return new MapTitleInfo(index, "A-2, Aft Storage Sector");
                case MM3Map.C2CentralControlSector: return new MapTitleInfo(index, "C-2, Central Control Sector");
                case MM3Map.A2ForwardStorageSector: return new MapTitleInfo(index, "A-2, Forward Storage Sector");
                case MM3Map.C2MainControlSector: return new MapTitleInfo(index, "C-2, Main Control Sector");
                case MM3Map.A1Surface: return new MapTitleInfo(index, "A-1, Surface");
                case MM3Map.A2Surface: return new MapTitleInfo(index, "A-2, Surface");
                case MM3Map.A3Surface: return new MapTitleInfo(index, "A-3, Surface");
                case MM3Map.A4Surface: return new MapTitleInfo(index, "A-4, Surface");
                case MM3Map.B1Surface: return new MapTitleInfo(index, "B-1, Surface");
                case MM3Map.B2Surface: return new MapTitleInfo(index, "B-2, Surface");
                case MM3Map.B3Surface: return new MapTitleInfo(index, "B-3, Surface");
                case MM3Map.B4Surface: return new MapTitleInfo(index, "B-4, Surface");
                case MM3Map.C1Surface: return new MapTitleInfo(index, "C-1, Surface");
                case MM3Map.C2Surface: return new MapTitleInfo(index, "C-2, Surface");
                case MM3Map.C3Surface: return new MapTitleInfo(index, "C-3, Surface");
                case MM3Map.C4Surface: return new MapTitleInfo(index, "C-4, Surface");
                case MM3Map.D1Surface: return new MapTitleInfo(index, "D-1, Surface");
                case MM3Map.D2Surface: return new MapTitleInfo(index, "D-2, Surface");
                case MM3Map.D3Surface: return new MapTitleInfo(index, "D-3, Surface");
                case MM3Map.D4Surface: return new MapTitleInfo(index, "D-4, Surface");
                case MM3Map.E1Surface: return new MapTitleInfo(index, "E-1, Surface");
                case MM3Map.E2Surface: return new MapTitleInfo(index, "E-2, Surface");
                case MM3Map.E3Surface: return new MapTitleInfo(index, "E-3, Surface");
                case MM3Map.E4Surface: return new MapTitleInfo(index, "E-4, Surface");
                case MM3Map.F1Surface: return new MapTitleInfo(index, "F-1, Surface");
                case MM3Map.F2Surface: return new MapTitleInfo(index, "F-2, Surface");
                case MM3Map.F3Surface: return new MapTitleInfo(index, "F-3, Surface");
                case MM3Map.F4Surface: return new MapTitleInfo(index, "F-4, Surface");
                case MM3Map.ItsASecret: return new MapTitleInfo(index, "It's a Secret");
                case MM3Map.TheArena: return new MapTitleInfo(index, "The Arena");
                default: return new MapTitleInfo(index, String.Format("UnknownMap({0})", index));
            }
        }

        public override MapTitleInfo GetMapTitle(int index)
        {
            return GetMapTitlePair(index);
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>((int) MM3Map.LastMain + 2);
            for (int i = (int)MM3Map.A1FountainHead; i < (int)MM3Map.Last; i++)
            {
                if (i >= (int)MM3Map.LastMain && i < (int)MM3Map.ItsASecret)
                    continue;
                MapTitleInfo pair = GetMapTitle(i);
                if (pair.Map != -1)
                    maps.Add(pair);
            }

            return maps;
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            if (iMapIndex == -1)
                iMapIndex = ReadByte(MM3Memory.CurrentMapQuadIndex);

            LocationInformation location = GetLocation();

            MM3MapData data = new MM3MapData();
            data.Attributes = new byte[512];
            data.VisibleObjects = GetVisibleObjects();
            data.SpecialSquares = GetSpecialSquares();
            data.ScriptInfo = GetScriptInfo() as MMScriptInfo;
            data.Index = location.MapIndex;
            data.Title = GetMapTitle(location.MapIndex);

            int iOffsetAppearance = 0;
            int iOffsetCartography = 0;
            switch (iMapIndex)
            {
                case 0:
                    iOffsetAppearance = MM3Memory.MapAppearance1;
                    iOffsetCartography = MM3Memory.MapCartography1;
                    break;
                case 1:
                    iOffsetAppearance = MM3Memory.MapAppearance2;
                    iOffsetCartography = MM3Memory.MapCartography2;
                    break;
                case 2:
                    iOffsetAppearance = MM3Memory.MapAppearance3;
                    iOffsetCartography = MM3Memory.MapCartography3;
                    break;
                case 3:
                    iOffsetAppearance = MM3Memory.MapAppearance4;
                    iOffsetCartography = MM3Memory.MapCartography4;
                    break;
                default:
                    return null;
            }

            data.Cartography = new MapCartography();
            MemoryBytes bytesMap = ReadOffset(iOffsetCartography, 32);
            if (bytesMap == null)
                return null;

            data.Cartography.SetBytes(bytesMap, new Size(16, 16));

            data.Appearance = ReadOffset(iOffsetAppearance, 512);
            for (int i = 0; i < data.Attributes.Length; i++)
                data.Attributes[i] = 0;

            m_lastMapAppearance = new byte[512];
            Buffer.BlockCopy(data.Appearance, 0, m_lastMapAppearance, 0, 512);

            data.Outside = IsOutside((MM3Map)data.Index);
            data.Town = IsTown((MM3Map)data.Index);

            if (bIncludeStrings)
                data.ScriptInfo = GetScriptInfo() as MMScriptInfo;

            return data;
        }

        private bool IsOutside(MM3Map map)
        {
            return (map >= MM3Map.A1Surface && map <= MM3Map.F4Surface);
        }

        public override bool IsSurface(int iMap) { return IsOutside((MM3Map) iMap); }

        private bool IsTown(MM3Map map)
        {
            switch (map)
            {
                case MM3Map.A1FountainHead:
                case MM3Map.A2Baywatch:
                case MM3Map.B4Wildabar:
                case MM3Map.E2SwampTown:
                case MM3Map.D3BlisteringHeights:
                    return true;
                default:
                    return false;
            }
        }

        public override string GetMapStrings(bool bRaw = false)
        {
            if (!IsValid)
                return null;
            
            byte[] bytes = GetMM3LoadedFileBytes(MM3Memory.MazeFilePointer, MM3Memory.MazeFileLength);

            List<ScriptString> strings = MM3MazeStrings(bytes);

            StringBuilder sb = new StringBuilder();
            foreach(MMString mm3String in strings)
                sb.AppendLine(mm3String.ToString());

            return sb.ToString();
        }

        public override List<int> GetMonsterIndices()
        {
            int iNumMonsters = ReadByte(MM3Memory.NumberOfMonsters);

            MemoryBytes bytesIndices = ReadOffset(MM3Memory.MonsterIndices, iNumMonsters * 2);
            if (bytesIndices == null)
                return null;

            List<int> list = new List<int>(bytesIndices.Length / 2);

            for (int i = 0; i < iNumMonsters * 2; i += 2)
                list.Add(BitConverter.ToUInt16(bytesIndices, i));

            return list;
        }

        public override List<ScriptString> GetScriptStrings()
        {
            if (!IsValid)
                return null;

            byte[] bytes = GetMM3LoadedFileBytes(MM3Memory.MazeFilePointer, MM3Memory.MazeFileLength);

            return MM3MazeStrings(bytes);
        }

        public override List<ScriptString> GetScriptStrings(MemoryBytes mb)
        {
            // MM3 stores the strings in a completely different location than the scripts, so they must be re-read each time
            return GetScriptStrings();
        }

        public List<ScriptString> MM3MazeStrings(byte[] bytes)
        {
            List<ScriptString> list = new List<ScriptString>();

            MM3String mm3String = null;

            int iNext = 0;
            do
            {
                mm3String = new MM3String();
                mm3String.SetFromBytes(bytes, ref iNext);
                if (mm3String.Valid)
                    list.Add(mm3String);
            } while (mm3String.Valid);

            return list;
        }

        public override string GetDebugMemoryInfo()
        {
            if (!IsValid)
                return "<no info available; game program may not be running>";

            StringBuilder sb = new StringBuilder();

            LocationInformation location = GetLocation();

            sb.AppendFormat("Current Map Index: {0}\r\n", ReadByte(MM3Memory.CurrentMapIndex));
            GetMapData(false, -1);
            if (m_lastMapAppearance != null)
            {
                sb.AppendFormat("Current Location Bytes: {0:X2} {1:X2}\r\n",
                    m_lastMapAppearance[location.PrimaryCoordinates.Y * 32 + location.PrimaryCoordinates.X * 2],
                    m_lastMapAppearance[location.PrimaryCoordinates.Y * 32 + location.PrimaryCoordinates.X * 2 + 1]
                    );
            }

            //sb.AppendFormat("Last Map Appearance:\r\n{0}", Global.ByteString(m_lastMapAppearance));

            //MonsterLocations monsters = GetMonsterLocations();
            //sb.AppendFormat("Monsters [{0}]: ", monsters.RawBytes.Length / 4);
            //foreach (MonsterPosition pos in monsters.MonsterPositions.Values)
            //{
            //    sb.AppendFormat("{0},{1}:{2} ", pos.Position.X, pos.Position.Y, pos.Monsters.Count);
            //}
            //sb.Remove(sb.Length - 1, 1);
            //sb.AppendLine();

            //sb.AppendLine("Visible Objects:");
            //Dictionary<Point, MM3VisibleObject> objects = GetVisibleObjects();
            //int iIndex = 0;
            //foreach (MM3VisibleObject vo in objects.Values)
            //{
            //    sb.AppendFormat("{0}: {1},{2} {3} {4} {5} {6}",
            //        iIndex, vo.X, vo.Y, vo.ImageIndex, vo.AnimationIndex, vo.Unknown1, vo.Unknown2
            //        );
            //    sb.AppendLine();
            //}

            sb.AppendLine("Special Squares:");
            MM3Scripts scripts = GetScripts() as MM3Scripts;

            byte[] bytesMaze = GetMM3LoadedFileBytes(MM3Memory.MazeFilePointer, MM3Memory.MazeFileLength);
            MMScriptInfo info = GetScriptInfo() as MMScriptInfo;

            int iIndex = 0;
            int iSubIndex = 0;
            foreach (List<GameScript> scriptList in scripts.Scripts.Values)
            {
                foreach (MM3Script script in scriptList)
                {
                    foreach (MM3ScriptLine line in script.m_lines)
                    {
                        sb.AppendFormat("{0},{1},{2} {3}: {4} [{5}]",
                            line.Location.X, line.Location.Y, line.Number, Global.DirectionString(line.Facing), line.Description(info), Global.ByteString(line.Bytes)
                            );
                        sb.AppendLine();
                        iSubIndex++;
                    }
                    iIndex++;
                    iSubIndex = 0;
                }
            }

            return sb.ToString();
        }

        public override MonsterLocations GetMonsterLocations(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            byte numMonsters = ReadByte(MM3Memory.NumberOfMonsters);

            MemoryBytes bytesX = ReadOffset(MM3Memory.MonsterLocationX, numMonsters * 2);
            MemoryBytes bytesY = ReadOffset(MM3Memory.MonsterLocationY, numMonsters * 2);
            MemoryBytes bytesHP = ReadOffset(MM3Memory.MonsterHP, numMonsters * 2);
            MemoryBytes bytesActive = ReadOffset(MM3Memory.MonsterActive, numMonsters * 2);
            MemoryBytes bytesIndices = ReadOffset(MM3Memory.MonsterIndices, numMonsters * 2);
            MemoryBytes bytesConditions = ReadOffset(MM3Memory.MonsterConditions, numMonsters * 2);

            if (bytesX == null || bytesY == null || bytesHP == null || bytesActive == null || bytesIndices == null || bytesConditions == null)
                return null;

            Point ptParty = GetPartyPosition();

            if (!bForceNew &&
                m_lastMonsterLocations != null &&
                Global.Compare(bytesY, m_lastMonsterBytesY) && 
                Global.Compare(bytesX, m_lastMonsterBytesX) && 
                Global.Compare(bytesHP, m_lastMonsterBytesHP) &&
                Global.Compare(bytesActive, m_lastMonsterBytesActive) &&
                Global.Compare(bytesConditions, m_lastMonsterBytesConditions) && 
                Global.Compare(bytesIndices, m_lastMonsterBytesIndices) && 
                ptParty == m_lastMonsterParty)
                return m_lastMonsterLocations;

            m_lastMonsterBytesX = bytesX;
            m_lastMonsterBytesY = bytesY;
            m_lastMonsterBytesHP = bytesHP;
            m_lastMonsterBytesActive = bytesActive;
            m_lastMonsterBytesConditions = bytesConditions;
            m_lastMonsterBytesIndices = bytesIndices;
            m_lastMonsterParty = ptParty;

            m_lastMonsterLocations = new MonsterLocations(bytesX, bytesY, bytesHP, bytesActive, bytesIndices, bytesConditions, GetPartyPosition());
            return m_lastMonsterLocations;
        }

        public Dictionary<Point, List<MM345VisibleObject>> GetVisibleObjects()
        {
            if (!IsValid)
                return null;

            byte[] bytesNull = new byte[12] { 0,0,0,0,0,0,0,0,0,0,0,0 };

            Dictionary<Point, List<MM345VisibleObject>> objects = new Dictionary<Point, List<MM345VisibleObject>>();

            MemoryBytes bytes = ReadOffset(MM3Memory.VisibleObjects, 12 * 80);
            if (bytes == null)
                return objects;

            for (int i = 0; i < 80 * 12; i += 12)
            {
                if (Global.CompareBytes(bytes, bytesNull, i, 0, 12))
                    continue;
                MM3VisibleObject vo = new MM3VisibleObject(bytes, i);
                if (objects.ContainsKey(vo.Location))
                    objects[vo.Location].Add(vo);
                else
                {
                    objects.Add(vo.Location, new List<MM345VisibleObject>(1));
                    objects[vo.Location].Add(vo);
                }
            }

            return objects;
        }

        public Dictionary<Point, List<MM345SpecialSquare>> GetSpecialSquares()
        {
            return GetSpecialSquares(null);
        }

        public Dictionary<Point, List<MM345SpecialSquare>> GetSpecialSquares(byte[] bytesActions)
        {
            if (!IsValid)
                return null;

            byte[] bytesEmpty = new byte[10] { 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 };

            Dictionary<Point, List<MM345SpecialSquare>> squares = new Dictionary<Point, List<MM345SpecialSquare>>();

            MemoryBytes bytes = ReadOffset(MM3Memory.SpecialSquares, 10 * 500 + 4);
            if (bytes == null)
                return squares;

            for (int i = 4; i < 500 * 10 + 4; i += 10)
            {
                if (Global.CompareBytes(bytes, bytesEmpty, i, 0, bytesEmpty.Length))
                    continue;
                MM3SpecialSquare ss = new MM3SpecialSquare(bytes, i, bytesActions);
                ss.AutoExecute = true;  // MM3 seems to always execute scripts
                if (squares.ContainsKey(ss.Location))
                    squares[ss.Location].Add(ss);
                else
                {
                    List<MM345SpecialSquare> list = new List<MM345SpecialSquare>(1);
                    list.Add(ss);
                    squares.Add(ss.Location, list);
                }

            }

            return squares;
        }

        public override bool SetMonster(Monster monster) { return SetMonsterInfo(monster as MM3Monster); }

        public bool SetMonsterInfo(MM3Monster monster)
        {
            if (monster == null)
                return false;

            if (!IsValid)
                return false;

            uint offset = MM3Memory.MonsterBase - m_offsetFoundBlock;
            MemoryBytes testBytes = ReadOffset(offset + 24, 8);
            if (Global.Compare(testBytes.Bytes, MM3Memory.FileHeaderTest))
                offset += 32;

            WriteByte(offset + MM3Memory.MonMemPhysicalResist + monster.Index, (byte) monster.Resistances.Physical);
            WriteByte(offset + MM3Memory.MonMemEnergyResist + monster.Index, (byte)monster.Resistances.Energy);
            WriteByte(offset + MM3Memory.MonMemAcidResist + monster.Index, (byte)monster.Resistances.Poison);
            WriteByte(offset + MM3Memory.MonMemColdResist + monster.Index, (byte)monster.Resistances.Cold);
            WriteByte(offset + MM3Memory.MonMemElecResist + monster.Index, (byte)monster.Resistances.Electricity);
            WriteByte(offset + MM3Memory.MonMemFireResist + monster.Index, (byte)monster.Resistances.Fire);
            WriteByte(offset + MM3Memory.MonMemMagicResist + monster.Index, (byte)monster.Resistances.Magic);
            WriteUInt16(offset + MM3Memory.MonMemGems + 2 * monster.Index, (UInt16)monster.Gems);
            WriteOffset(offset + MM3Memory.MonMemGold + 4 * monster.Index, BitConverter.GetBytes(monster.Gold));
            WriteByte(offset + MM3Memory.MonMemItems + monster.Index, (byte)monster.Items);
            WriteByte(offset + MM3Memory.MonMemAccuracy + monster.Index, (byte)monster.Accuracy);
            WriteByte(offset + MM3Memory.MonMemRanged + monster.Index, (byte) (monster.Missile ? 1 : 0));
            WriteByte(offset + MM3Memory.MonMemSpecialPower + monster.Index, (byte)monster.Touch);
            WriteByte(offset + MM3Memory.MonMemTarget + monster.Index, (byte)monster.Target);
            WriteByte(offset + MM3Memory.MonMemDamageType + monster.Index, (byte)monster.DamageType);
            WriteOffset(offset + MM3Memory.MonMemExperience + 4 * monster.Index, BitConverter.GetBytes(monster.Experience));
            WriteByte(offset + MM3Memory.MonMemNumAttacks + monster.Index, (byte)monster.NumAttacks);
            WriteByte(offset + MM3Memory.MonMemDamageDieMax + monster.Index, (byte)monster.DamageDieMax);
            WriteByte(offset + MM3Memory.MonMemDamageNumDice + monster.Index, (byte)monster.DamageNumDice);
            WriteByte(offset + MM3Memory.MonMemSpeed + monster.Index, (byte)monster.Speed);
            WriteByte(offset + MM3Memory.MonMemAC + monster.Index, (byte)monster.AC);
            WriteUInt16(MM3Memory.MonMemHPMax + 2 * monster.Index, (UInt16)monster.HP);
            WriteUInt16(MM3Memory.MonsterHP + 2 * monster.EncounterIndex, (UInt16)monster.CurrentHP);
            WriteUInt16(MM3Memory.MonsterIndices + 2 * monster.EncounterIndex, (UInt16)monster.Index);
            WriteUInt16(MM3Memory.MonsterConditions + 2 * monster.EncounterIndex, (UInt16)monster.MM345Condition);
            WriteUInt16(MM3Memory.MonsterLocationX + 2 * monster.EncounterIndex, (UInt16)monster.Position.X);
            WriteUInt16(MM3Memory.MonsterLocationY + 2 * monster.EncounterIndex, (UInt16)monster.Position.Y);
            WriteUInt16(MM3Memory.MonsterActive + 2 * monster.EncounterIndex, (UInt16) (monster.Active ? 1 : 0));

            // Reload the values into the main monster list
            MM3.MonsterList.Value.Reinitialize(this, true);
            return true;
        }

        private bool GetGameInfoBytes(out MemoryBytes bytesMapInfo, out MemoryBytes bytesPartyInfo)
        {
            if (!IsValid)
            {
                bytesMapInfo = null;
                bytesPartyInfo = null;
                return false;
            }

            bytesMapInfo = ReadOffset(MM3Memory.MapBytes, MM3MapBytes.Size);

            if (bytesMapInfo == null)
            {
                bytesPartyInfo = null;
                return false;
            }

            // There are some bytes that change frequently that aren't relevant to the GameInfo display
            // so we set those to zero to avoid being detected as changed that require a UI update
            for (int i = 0; i < MM3MapBytes.OffsetQuad1; i++)
            {
                switch (i)
                {
                    case MM3MapBytes.OffsetCurrentMap:
                    case MM3MapBytes.OffsetCurrentQuad:
                        break;
                    default:
                        bytesMapInfo[i] = 0;
                        break;
                }
            }

            bytesPartyInfo = ReadOffset(MM3Memory.PartyBytes, MM3PartyBytes.Size);

            return (bytesMapInfo != null && bytesPartyInfo != null);
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            MemoryBytes bytesMapInfo, bytesPartyInfo;

            if (!GetGameInfoBytes(out bytesMapInfo, out bytesPartyInfo))
                return null;

            MM3GameInfo info = new MM3GameInfo(bytesMapInfo, bytesPartyInfo);

            info.Bytes = Global.Combine(bytesMapInfo, bytesPartyInfo);

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is MM3GameInfo))
                return GetGameInfo();

            MemoryBytes bytesMapInfo, bytesPartyInfo;

            if (!GetGameInfoBytes(out bytesMapInfo, out bytesPartyInfo))
                return GetGameInfo();

            MM3GameInfo MM3Info = (MM3GameInfo)infoOld;

            if (Global.Compare(MM3Info.RawMap, bytesMapInfo) && Global.Compare(MM3Info.RawParty, bytesPartyInfo))
                return infoOld; // All the bytes are the same; don't bother creating a new object

            return new MM3GameInfo(bytesMapInfo, bytesPartyInfo);
        }

        public override int GetCurrentMapQuad()
        {
            if (!IsValid)
                return -1;

            return ReadByte(MM3Memory.CurrentMapQuadIndex);
        }

        public override MapAttributes GetMapAttributes()
        {
            if (!IsValid)
                return null;

            return GetMapAttributes(GetCurrentMapQuad());
        }

        private MapAttributes GetMapAttributes(int iQuad)
        {
            MemoryBytes bytesAttributes = null;
            switch (iQuad)
            {
                case 1:
                    bytesAttributes = ReadOffset(MM3Memory.MapAttributes2, 57);
                    break;
                case 2:
                    bytesAttributes = ReadOffset(MM3Memory.MapAttributes3, 57);
                    break;
                case 3:
                    bytesAttributes = ReadOffset(MM3Memory.MapAttributes4, 57);
                    break;
                default:
                    bytesAttributes = ReadOffset(MM3Memory.MapAttributes1, 57);
                    break;
            }

            if (bytesAttributes == null)
                return null;
            return new MM345MapAttributes(bytesAttributes);
        }

        public MMActiveEffects GetActiveEffects()
        {
            MMActiveEffects effects = new MMActiveEffects();

            if (!IsValid)
                return effects;

            effects.Levitate = ReadByte(MM3Memory.LevitateActive) != 0;
            effects.WizardEyeBool = ReadByte(MM3Memory.WizardEyeActive) != 0;
            effects.LightBool = ReadByte(MM3Memory.LightActive) != 0;
            effects.WaterWalk = ReadByte(MM3Memory.WalkOnWaterActive) != 0;
            effects.ProtFire = ReadUInt16(MM3Memory.ProtectElements);
            effects.ProtElectric = ReadUInt16(MM3Memory.ProtectElements + 2);
            effects.ProtCold = ReadUInt16(MM3Memory.ProtectElements + 4);
            effects.ProtAcid = ReadUInt16(MM3Memory.ProtectElements + 6);
            return effects;
        }

        public override bool SetActiveEffect(MMEffectTag effect)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[1];
            bytes[0] = (byte)(effect.Enabled ? 1 : 0);
            int iValue = (effect.Enabled ? effect.Value : 0);

            switch (effect.Effect)
            {
                case MMEffects.Levitate:
                    WriteOffset(MM3Memory.LevitateActive, bytes);
                    break;
                case MMEffects.WaterWalk:
                    WriteOffset(MM3Memory.WalkOnWaterActive, bytes);
                    break;
                case MMEffects.WizardEyeBool:
                    WriteOffset(MM3Memory.WizardEyeActive, bytes);
                    break;
                case MMEffects.LightBool:
                    WriteOffset(MM3Memory.LightActive, bytes);
                    break;
                case MMEffects.ProtFire:
                    WriteOffset(MM3Memory.ProtectElements, BitConverter.GetBytes((ushort) iValue));
                    break;
                case MMEffects.ProtElectric:
                    WriteOffset(MM3Memory.ProtectElements+2, BitConverter.GetBytes((ushort) iValue));
                    break;
                case MMEffects.ProtCold:
                    WriteOffset(MM3Memory.ProtectElements+4, BitConverter.GetBytes((ushort) iValue));
                    break;
                case MMEffects.ProtAcid:
                    WriteOffset(MM3Memory.ProtectElements+6, BitConverter.GetBytes((ushort) iValue));
                    break;
                default:
                    return false;
            }

            return true;
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new MM3RosterFile(Global.CombineRoster(Game), bSilent);
        }

        public override bool ValidateRosterFile()
        {
            // Always reload the roster file, in case the player deleted characters or otherwise putzed with the file
            m_mm3Roster = CreateRoster(true) as MM3RosterFile;
            if (!m_mm3Roster.Valid)
                BrowseRosterFile();

            if (m_mm3Roster.Valid)
            {
                Games.SetRosterFile(GameNames.MightAndMagic3, Path.GetFileName(m_mm3Roster.FileName));
                Games.SetRosterPath(GameNames.MightAndMagic3, Path.GetDirectoryName(m_mm3Roster.FileName));
            }

            return m_mm3Roster.Valid;
        }

        public static string MM3CurrentDataFile
        {
            get
            {
                return Global.CombineRoster(GameNames.MightAndMagic3);
            }
            set
            {
                if (value == null)
                {
                    Games.SetRosterFile(GameNames.MightAndMagic3, "MM3.CUR");
                    Games.SetRosterPath(GameNames.MightAndMagic3, String.Empty);
                    return;
                }
                Games.SetRosterFile(GameNames.MightAndMagic3, Path.GetFileName(value));
                Games.SetRosterPath(GameNames.MightAndMagic3, Path.GetDirectoryName(value));
            }
        }

        public override int MaxInventoryChar { get { return 20; } }
        public override int MaxBackpackSize { get { return 18; } }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!ValidateRosterFile())
                return -1;

            byte[] bytes = new byte[MM3Character.SizeInBytes];
            while (iStart >= 0)
            {
                MM3Character mm3Char = null;
                if (iStart < m_mm3Roster.Chars.Count)
                    mm3Char = MM3Character.Create(m_mm3Roster.Chars[iStart].Bytes, 0, null);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (mm3Char != null && mm3Char.Name == "Inventory")
                            return iStart;
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (mm3Char != null && mm3Char.Name == "Inventory")
                            return iStart;
                        if (mm3Char == null || String.IsNullOrWhiteSpace(mm3Char.Name))
                        {
                            // No character in the roster at this position; make a new one;
                            m_mm3Roster.Chars[iStart].Bytes = Properties.Resources.MM3InventoryChar;
                            m_mm3Roster.Chars[iStart].Town = 1;
                            m_mm3Roster.SaveCharBytes(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (mm3Char == null || mm3Char.Name == "" || mm3Char.Name == "Inventory")
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
            // Completely overwrites a backpack, including equipped items
            // Also removes the "equipped" status of all items

            if (iRosterPosition < 0 || iRosterPosition > 19)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= m_mm3Roster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            MM3BackpackBytes bytes = GetBackpackBytes(items);

            byte[] bytesChar = m_mm3Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            byte[] bytesNull = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Buffer.BlockCopy(bytesNull, 0, bytesChar, MM3.Offsets.InvEquipLoc, 18);
            Buffer.BlockCopy(bytes.Charges, 0, bytesChar, MM3.Offsets.InvCharges, 18);
            Buffer.BlockCopy(bytes.Element, 0, bytesChar, MM3.Offsets.InvElements, 18);
            Buffer.BlockCopy(bytes.Material, 0, bytesChar, MM3.Offsets.InvMaterials, 18);
            Buffer.BlockCopy(bytes.Attribute, 0, bytesChar, MM3.Offsets.InvAttributes, 18);
            Buffer.BlockCopy(bytes.Base, 0, bytesChar, MM3.Offsets.InvBases, 18);
            Buffer.BlockCopy(bytes.Property, 0, bytesChar, MM3.Offsets.InvProperties, 18);

            m_mm3Roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        private MM3BackpackBytes GetBackpackBytes(List<Item> items)
        {
            MM3BackpackBytes bytes = new MM3BackpackBytes();

            for (int i = 0; i < items.Count; i++)
            {
                if (!(items[i] is MM3Item))
                    continue;
                MM3Item mm3Item = items[i] as MM3Item;

                bytes.Base[i] = (byte)mm3Item.Base;
                bytes.Charges[i] = (byte)mm3Item.ChargesByte;
                bytes.Equipped[i] = (byte)mm3Item.WhereEquipped;
                bytes.Element[i] = (byte)mm3Item.Element;
                bytes.Material[i] = (byte)mm3Item.Material;
                bytes.Attribute[i] = (byte)mm3Item.Attribute;
                bytes.Property[i] = (byte)mm3Item.Property;
            }
            return bytes;
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            List<Item> equipped = bRemoveEquipped ? new List<Item>() : GetEquippedItems(iCharAddress);
            if (equipped.Count + items.Count > MaxBackpackSize)
                return SetBackpackResult.InsufficientSpace;   // Too many items

            equipped.AddRange(items);

            MM3BackpackBytes bytes = GetBackpackBytes(equipped);

            long iAddress = MM3Memory.PartyInfo + (iCharAddress * MM3Character.SizeInBytes);

            WriteOffset(iAddress + MM3.Offsets.InvEquipLoc, bytes.Equipped);
            WriteOffset(iAddress + MM3.Offsets.InvCharges, bytes.Charges);
            WriteOffset(iAddress + MM3.Offsets.InvElements, bytes.Element);
            WriteOffset(iAddress + MM3.Offsets.InvMaterials, bytes.Material);
            WriteOffset(iAddress + MM3.Offsets.InvAttributes, bytes.Attribute);
            WriteOffset(iAddress + MM3.Offsets.InvBases, bytes.Base);
            WriteOffset(iAddress + MM3.Offsets.InvProperties, bytes.Property);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 19)
                return null;

            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition >= m_mm3Roster.Chars.Count)
                return null;

            byte[] bytesChar = m_mm3Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            List<Item> backpack = new List<Item>(18);
            for (int i = 0; i < 18; i++)
            {
                if (bytesChar[i + MM3.Offsets.InvBases] != 0)
                {
                    backpack.Add(MM3Item.FromBagBytes(new byte[] {
                        bytesChar[i + MM3.Offsets.InvEquipLoc],
                        bytesChar[i + MM3.Offsets.InvCharges],
                        bytesChar[i + MM3.Offsets.InvElements],
                        bytesChar[i + MM3.Offsets.InvMaterials],
                        bytesChar[i + MM3.Offsets.InvAttributes],
                        bytesChar[i + MM3.Offsets.InvBases],
                        bytesChar[i + MM3.Offsets.InvProperties]
                    }));
                }
            }

            return backpack;
        }

        public List<Item> GetEquippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.EquippedOnly);
        }

        public List<Item> GetUnequippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.UnequippedOnly);
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.All);
        }

        public List<Item> GetBackpack(int iCharAddress, BackpackType type)
        {
            if (!IsValid)
                return null;

            List<Item> backpack = new List<Item>(18);

            long iAddress = MM3Memory.PartyInfo + (iCharAddress * MM3Character.SizeInBytes);

            MM3BackpackBytes bytes = new MM3BackpackBytes();
            ReadOffset(iAddress + MM3.Offsets.InvEquipLoc, bytes.Equipped);
            ReadOffset(iAddress + MM3.Offsets.InvCharges, bytes.Charges);
            ReadOffset(iAddress + MM3.Offsets.InvElements, bytes.Element);
            ReadOffset(iAddress + MM3.Offsets.InvMaterials, bytes.Material);
            ReadOffset(iAddress + MM3.Offsets.InvAttributes, bytes.Attribute);
            ReadOffset(iAddress + MM3.Offsets.InvBases, bytes.Base);
            ReadOffset(iAddress + MM3.Offsets.InvProperties, bytes.Property);

            for (int i = 0; i < 18; i++)
            {
                if (bytes.Base[i] != 0)
                {
                    if (bytes.Equipped[i] == 0 && (type == BackpackType.None || type == BackpackType.EquippedOnly))
                        continue;

                    if (bytes.Equipped[i] != 0 && (type == BackpackType.None || type == BackpackType.UnequippedOnly))
                        continue;

                    backpack.Add(MM3Item.FromBagBytes(new byte[] {
                        bytes.Equipped[i],
                        bytes.Charges[i],
                        bytes.Element[i],
                        bytes.Material[i],
                        bytes.Attribute[i],
                        bytes.Base[i],
                        bytes.Property[i]
                    }));
                }
            }

            return backpack;
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

        public override bool HasBeacon { get { return true; } }

        public override bool SetBeacon(Point ptLocation, int iMap)
        {
            if (!IsValid)
                return false;

            // Set the first sorcerer/archer beacon

            MM3PartyInfo party = ReadMM3PartyInfo();
            for (int i = 0; i < party.NumChars; i++)
            {
                MM3Character mm3Char = MM3Character.Create(party.Bytes, i * MM3Character.SizeInBytes, GetGameInfo());
                if (mm3Char == null)
                    continue;

                if (mm3Char.Class != MM345Class.Sorcerer && mm3Char.Class != MM345Class.Archer)
                    continue;

                MMBeacon beacon = new MMBeacon(iMap, ptLocation);

                byte[] bytes = beacon.GetBytes();
                return WriteOffset(MM3Memory.PartyInfo + i * MM3Character.SizeInBytes + MM3.Offsets.Beacon, bytes);
            }

            return false;
        }

        public override bool SetReadySpell(int iChar, int iSpell)
        {
            if (iSpell >= 76)
                iSpell = (int)MM3InternalSpellIndex.None;
            else if (!Global.Cheats)
            {
                // Make sure this character can cast this spell
                MM3PartyInfo party = ReadMM3PartyInfo();
                MM3Character mm3Char = MM3Character.Create(party.Bytes, iChar * party.CharacterSize, GetGameInfo());
                if (!mm3Char.Spells.IsKnown(iSpell, mm3Char.BasicClass))
                    return false;
            }

            byte[] bytes = new byte[1] { (byte)iSpell };

            return WriteOffset(MM3Memory.PartyInfo + iChar * MM3Character.SizeInBytes + MM3.Offsets.ReadySpell, bytes);
        }

        public override bool SetReadySpells(int iSpell, SpellType type)
        {
            if (iSpell >= 76)
                iSpell = (int)MM3InternalSpellIndex.None;

            // Make sure this character can cast this spell
            MM3PartyInfo party = ReadMM3PartyInfo();

            for (int iChar = 0; iChar < party.NumChars; iChar++)
            {
                MM3Character mm3Char = MM3Character.Create(party.Bytes, iChar * party.CharacterSize, GetGameInfo());
                if (!Global.Cheats)
                {
                    if (!mm3Char.Spells.IsKnown(iSpell, mm3Char.BasicClass))
                        continue;
                }
                if (type != SpellType.Unknown)
                {
                    SpellType typeChar = Global.GetSpellType(mm3Char.BasicClass);
                    if (typeChar != type)
                    {
                        if (typeChar != SpellType.Druid)
                            continue;

                        // Only set the quick spell for a druid to one of the sorcerer or cleric spells if Druids can normally cast that spell
                        if (!MM3SpellList.IsDruidSpell((MM3InternalSpellIndex)iSpell))
                            continue;
                    }
                }
                WriteByte(MM3Memory.PartyInfo + iChar * MM3Character.SizeInBytes + MM3.Offsets.ReadySpell, (byte)iSpell);
            }

            return true;
        }

        public override bool SetMapAttributeFlags(MapAttributeFlags flags)
        {
            if (!IsValid)
                return false;

            return SetMapAttributeFlags(ReadByte(MM3Memory.CurrentMapQuadIndex), flags);
        }

        public int GetMapAttributesAddress()
        {
            if (!IsValid)
                return -1;

            int iQuad = GetCurrentMapQuad();
            if (iQuad == -1)
                return -1;

            switch (iQuad)
            {
                case 0: return MM3Memory.MapAttributes1;
                case 1: return MM3Memory.MapAttributes2;
                case 2: return MM3Memory.MapAttributes3;
                case 3: return MM3Memory.MapAttributes4;
                default: return -1;
            }
        }

        private bool SetMapAttributeFlags(int iQuad, MapAttributeFlags flags)
        {
            byte[] bytesAttributes = new byte[57];

            MM345MapAttributes attributes = GetMapAttributes(iQuad) as MM345MapAttributes;
            attributes.SetFlags(flags);

            switch (iQuad)
            {
                case 0: return WriteOffset(MM3Memory.MapAttributes1, attributes.GetBytes());
                case 1: return WriteOffset(MM3Memory.MapAttributes2, attributes.GetBytes());
                case 2: return WriteOffset(MM3Memory.MapAttributes3, attributes.GetBytes());
                case 3: return WriteOffset(MM3Memory.MapAttributes4, attributes.GetBytes());
                default: return false;
            }
        }

        public override bool SetPartyGold(UInt32 gold)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gold);
            return WriteOffset(MM3Memory.PartyGold, bytes);
        }

        public override bool SetPartyFood(UInt32 food)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes((UInt16) food);
            return WriteOffset(MM3Memory.PartyFood, bytes);
        }

        public override bool SetPartyGems(UInt32 gems)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gems);
            return WriteOffset(MM3Memory.PartyGems, bytes);
        }

        public override bool SetBankGold(UInt32 gold)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gold);
            return WriteOffset(MM3Memory.BankGold, bytes);
        }

        public override bool SetBankGems(UInt32 gems)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gems);
            return WriteOffset(MM3Memory.BankGems, bytes);
        }

        public override bool SetLockStrength(byte strength)
        {
            if (!IsValid)
                return false;

            int iAttributesAddress = GetMapAttributesAddress();
            byte[] bytes = new byte[] { strength };
            return WriteOffset(iAttributesAddress + MM3Memory.LockStrengthOffset, bytes);
        }

        public override bool SetYear(UInt16 year)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(year);
            return WriteOffset(MM3Memory.Year, bytes);
        }

        public override bool SetDay(Byte day)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[] { day };
            return WriteOffset(MM3Memory.Day, bytes);
        }

        public override bool SetTime(UInt16 minutes)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(minutes);
            return WriteOffset(MM3Memory.Minutes, bytes);
        }

        public override bool SetSavePermitted(bool bPermitted)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[] { (byte) (bPermitted ? 1 : 0) };
            return WriteOffset(MM3Memory.SavePermitted, bytes);
        }

        public override bool SetTime(UInt16 year, byte day, UInt16 minutes)
        {
            if (!IsValid)
                return false;

            byte[] bytesMinutes = BitConverter.GetBytes(minutes);
            byte[] bytesDay = new byte[] { day };
            byte[] bytesYear = BitConverter.GetBytes(year);
            WriteOffset(MM3Memory.Minutes, bytesMinutes);
            WriteOffset(MM3Memory.Year, bytesYear);
            return WriteOffset(MM3Memory.Day, bytesDay);
        }

        private void CureCondition(MM3CureAllInfo info, int iAddress, ref byte condition, MM3InternalSpellIndex spell, bool bOneHP, ref bool bNoGems, ref bool bNoSP, ref bool bNotKnown)
        {
            if (condition > 0)
            {
                if (info.CasterSpells.IsKnown((int)spell, info.CasterClass))
                {
                    int iSP = MM3.SpellList.Value.GetSpell(spell).Cost.SpellPoints;
                    if (info.CasterSpellPoints >= iSP)
                    {
                        int iGems = MM3.SpellList.Value.GetSpell(spell).Cost.Gems;
                        if (info.CasterGems >= iGems)
                        {
                            info.CasterSpellPoints -= (ushort) iSP;
                            info.CasterGems -= (uint) iGems;
                            condition = 0;
                            if (bOneHP)
                                info.HitPoints[iAddress] = 1;
                        }
                        else
                            bNoGems = true;
                    }
                    else
                        bNoSP = true;
                }
                else
                    bNotKnown = true;
            }
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bSpellNotKnown = false;
            bool bInsufficientSP = false;
            bool bInsufficientGems = false;

            if (!(cureAllInfo is MM3CureAllInfo))
                return CureAllResult.Error;

            if (cureAllInfo.MonstersNearby)
                return CureAllResult.MonstersNearby;

            MM3CureAllInfo info = cureAllInfo as MM3CureAllInfo;

            // Might and Magic 3 doesn't have specific spells for curing anything except
            // Sleep, Poison, Disease, Weakness, Paralysis, Death, Stone, and Eradication.

            // Okay, let's start curing!
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                // First skip Dead/Eradicated (these conditions are more complicated in MM3 than
                // "Cure All" should fix)
                if (info.Conditions[i].Dead > 0 || info.Conditions[i].Eradicated > 0)
                    continue;

                CureCondition(info, i, ref info.Conditions[i].Stone, MM3InternalSpellIndex.StoneToFlesh, true, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Paralyzed, MM3InternalSpellIndex.CureParalysis, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Weak, MM3InternalSpellIndex.Revitalize, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Poisoned, MM3InternalSpellIndex.CurePoison, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Diseased, MM3InternalSpellIndex.CureDisease, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
            }

            bool bAnySleep = false;
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i].Asleep > 0)
                {
                    if (info.CasterSpells.IsKnown((int) MM3InternalSpellIndex.Awaken, info.CasterClass))
                    {
                        if (info.CasterSpellPoints >= 1)
                        {
                            bAnySleep = true;
                            info.Conditions[i].Asleep = 0;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bSpellNotKnown = true;
                }
            }
            if (bAnySleep && info.CasterSpellPoints > 0)
                info.CasterSpellPoints -= 1;

            // Restore HP with any remaining spell points
            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.CasterSpells.IsKnown((int)MM3InternalSpellIndex.FirstAid, info.CasterClass))
                    {
                        while (info.HitPoints[i] < info.HitPointsMax[i] - 5)
                        {
                            if (info.CasterSpellPoints >= 6)
                            {
                                info.CasterSpellPoints--;
                                info.HitPoints[i] += 6;
                                if (info.HitPoints[i] > 0)
                                    info.Conditions[i].Unconscious = 0;
                            }
                            else
                            {
                                bInsufficientSP = true;
                                break;
                            }
                        }
                    }
                    else
                        bSpellNotKnown = true;
                }
            }

            if (bSpellNotKnown)
                return CureAllResult.SpellNotKnown;
            if (bInsufficientSP)
                return CureAllResult.NoSpellPoints;
            if (bInsufficientGems)
                return CureAllResult.NoGems;
            return CureAllResult.Success;
        }

        public override CureAllInfo GetCureAllInfo(int iCasterAddress, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterAddress >= partyAddresses.Length)
                return null;

            MM3CureAllInfo info = new MM3CureAllInfo();
            MM3PartyInfo party = ReadMM3PartyInfo();

            byte[] temp = new byte[2];
            IntPtr pRead = IntPtr.Zero;

            info.GameState = ReadMM3GameState();
            info.InCombat = info.GameState.InCombat;

            MM3Character caster = null;

            info.Conditions = new MM3Condition[partyAddresses.Length];
            info.HitPoints = new Int16[partyAddresses.Length];
            info.HitPointsMax = new Int16[partyAddresses.Length];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                MM3Character mm3Char = MM3Character.Create(party.Bytes, party.CharacterSize * i, GetGameInfo());
                if (i == partyAddresses[iCasterAddress])
                    caster = mm3Char;
                info.Conditions[i] = mm3Char.Condition;
                info.HitPoints[i] = mm3Char.CurrentHP;
                info.HitPointsMax[i] = (short) mm3Char.MaxHP;
            }

            MM3GameInfo gameInfo = GetGameInfo() as MM3GameInfo;
            info.CasterGems = gameInfo.Party.Gems;
            info.CasterSpellPoints = caster.CurrentSP;
            info.CasterSpells = caster.Spells;
            info.CasterClass = caster.BasicClass;
            info.CasterCondition = caster.Condition;
            info.MonstersNearby = ActiveMonstersNearby;

            return info;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterAddress, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterAddress >= partyAddresses.Length)
                return;

            MM3CureAllInfo info = cureAll as MM3CureAllInfo;

            for (int i = 0; i < info.Conditions.Length; i++)
            {
                WriteOffset(MM3Memory.PartyInfo + i * MM3Character.SizeInBytes + MM3.Offsets.Condition, info.Conditions[i].GetBytes());
                WriteOffset(MM3Memory.PartyInfo + i * MM3Character.SizeInBytes + MM3.Offsets.CurrentHP, BitConverter.GetBytes((Int16)info.HitPoints[i]));
            }

            WriteOffset(MM3Memory.PartyGems, BitConverter.GetBytes(info.CasterGems));
            WriteOffset(MM3Memory.PartyInfo + partyAddresses[iCasterAddress] * MM3Character.SizeInBytes + MM3.Offsets.CurrentSP, BitConverter.GetBytes((UInt16)info.CasterSpellPoints));
        }

        public override string StatToolTip(int iIndex, int iValue)
        {
            PrimaryStat[] order = StatOrder;
            if (order[iIndex] == PrimaryStat.Might)
                return String.Format("Might gives a bonus ({0}: {1}) to damage inflicted with melee weapons in combat.  Penalties cannot drop damage per attack below 1.", iValue, GetStatModString(iValue, PrimaryStat.Might));
            if (order[iIndex] == PrimaryStat.Intellect)
                return String.Format("Intellect gives a bonus ({0}: {1}) to maximum spell points.  The formulas are Sorcerer/Druid:Lev*(3+Bonus), Archer/Ranger:(Lev*(3+Bonus))/2.  For Druids and Rangers the Bonus is the average of the Int and Per bonuses.", iValue, GetStatModString(iValue, PrimaryStat.Intellect));
            if (order[iIndex] == PrimaryStat.Personality)
                return String.Format("Personality gives a bonus ({0}: {1})  to maximum spell points.  The formulas are Cleric/Druid:Lev*(3+Bonus), Paladin/Ranger:(Lev*(3+Bonus))/2.  For Druids and Rangers the Bonus is the average of the Int and Per bonuses.", iValue, GetStatModString(iValue, PrimaryStat.Personality));
            if (order[iIndex] == PrimaryStat.Endurance)
                return String.Format("Endurance gives a bonus ({0}: {1}) to maximum hit points.  The formula is Level*(Base*Bonus).  The base values are Barbarian:12, Knight:10, Ranger:9, Paladin/Robber:8, Archer/Ninja:7, Druid:6, Cleric:5, Sorcerer:4", iValue, GetStatModString(iValue, PrimaryStat.Endurance));
            if (order[iIndex] == PrimaryStat.Speed)
                return String.Format("Speed gives a bonus ({0}: {1}) to armor class.  Speed also determines the order of actions in combat.", iValue, GetStatModString(iValue, PrimaryStat.Speed));
            if (order[iIndex] == PrimaryStat.Accuracy)
                return String.Format("Accuracy gives a bonus ({0}: {1}) to hit in combat.", iValue, GetStatModString(iValue, PrimaryStat.Accuracy));
            if (order[iIndex] == PrimaryStat.Luck)
                return String.Format("Luck gives a bonus ({0}: {1}) to resistance rolls, both damage resistance and status condition resistance.  You must have at least 1 resistance point for the Luck bonus to apply at all.", iValue, GetStatModString(iValue, PrimaryStat.Luck));
            return "";
        }

        public override Shops GetShopInfo()
        {
            Shops shops = GetShopItems() as Shops;
            if (shops == null)
                return shops;

            MM3GameState state = ReadMM3GameState();
            shops.InShop = state.InShop;
            return shops;
        }

        public MM3Shops GetShopItems()
        {
            if (!IsValid)
                return null;

            // Each item has 6 1-byte properties (charges, element, material, attribute, base, property)
            // Each shop has 3 categories (weapons, armor, misc)
            // Each category has 9 items
            // Each of 5 towns has 1 shop
            MemoryBytes bytes = ReadOffset(MM3Memory.ShopInventory, 6*3*9*5);
            if (bytes == null)
                return null;

            MemoryBytes bytesCurrent = ReadOffset(MM3Memory.CurrentShop, 6 * 19);
            if (bytesCurrent == null)
                return null;

            return new MM3Shops(MM3Memory.ShopInventory, bytes, MM3Memory.CurrentShop, bytesCurrent);
        }

        public override bool SetShopItem(ShopItem item)
        {
            if (!(item.Item is MM3Item))
                return false;

            MM3Item mm3Item = item.Item as MM3Item;

            byte[] charges = new byte[] { mm3Item.ChargesByte };
            byte[] element = new byte[] { (byte) mm3Item.Element };
            byte[] material = new byte[] { (byte)mm3Item.Material };
            byte[] attribute = new byte[] { (byte)mm3Item.Attribute };
            byte[] index = new byte[] { (byte)mm3Item.Base };
            byte[] property = new byte[] { (byte)mm3Item.Property };

            WriteOffset(item.Offset + 0 * item.Multiplier, charges);
            WriteOffset(item.Offset + 1 * item.Multiplier, element);
            WriteOffset(item.Offset + 2 * item.Multiplier, material);
            WriteOffset(item.Offset + 3 * item.Multiplier, attribute);
            WriteOffset(item.Offset + 4 * item.Multiplier, index);
            WriteOffset(item.Offset + 5 * item.Multiplier, property);

            return true;
        }

        public override bool BrowseRosterFile(string strTitle = null)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = Games.RosterFile(Game);
            ofd.InitialDirectory = Games.RosterPath(Game);
            ofd.Filter = "Current Files|*.CUR|Saved Files|*.MM3|All Files|*.*";
            if (String.IsNullOrWhiteSpace(strTitle))
                ofd.Title = "You must select your MM3.CUR file in order to use the Bag of Holding";
            else
                ofd.Title = strTitle;
            while (true)
            {
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return false;

                m_mm3Roster = new MM3RosterFile(ofd.FileName, false);
                if (m_mm3Roster.Valid)
                {
                    Games.SetRosterFile(GameNames.MightAndMagic3, Path.GetFileName(m_mm3Roster.FileName));
                    break;
                }
            }
            return true;
        }

        public override bool KillAllMonsters()
        {
            if (!IsValid)
                return false;

            int iNumMonsters = ReadByte(MM3Memory.NumberOfMonsters);

            byte[] bytesY = new byte[iNumMonsters * 2];
            byte[] bytesX = new byte[iNumMonsters * 2];
            byte[] bytesHP = new byte[iNumMonsters * 2];
            byte[] bytesConditions = new byte[iNumMonsters * 2];

            for (int i = 0; i < iNumMonsters * 2; i += 2)
            {
                bytesY[i] = 0x80;
                bytesY[i + 1] = 0xff;
                bytesX[i] = 0x80;
                bytesX[i + 1] = 0xff;
                bytesHP[i] = 0;
                bytesHP[i + 1] = 0;
                bytesConditions[i] = 0;
                bytesConditions[i + 1] = 0;
            }

            WriteOffset(MM3Memory.MonsterLocationX, bytesX);
            WriteOffset(MM3Memory.MonsterLocationY, bytesY);
            WriteOffset(MM3Memory.MonsterHP, bytesHP);
            WriteOffset(MM3Memory.MonsterConditions, bytesConditions);
            WriteByte(MM3Memory.NumberOfMonsters, 0);

            return true;
        }

        public override bool ResetMonsters()
        {
            if (!IsValid)
                return false;

            byte[] bytesMonsterDefaults = Global.Uncompress(Properties.Resources.MM3MonsterDefaults);
            if (bytesMonsterDefaults.Length < 256)
                return false; // corrupt data?

            UInt16 offset = BitConverter.ToUInt16(bytesMonsterDefaults, ReadByte(MM3Memory.CurrentMapIndex) * 2);
            byte numMonsters = bytesMonsterDefaults[offset];
            byte[] bytes = new byte[numMonsters * 2];

            Buffer.BlockCopy(bytesMonsterDefaults, offset + 1, bytes, 0, numMonsters * 2);
            WriteOffset(MM3Memory.MonsterLocationX, bytes);
            Buffer.BlockCopy(bytesMonsterDefaults, offset + 1 + numMonsters * 2, bytes, 0, numMonsters * 2);
            WriteOffset(MM3Memory.MonsterLocationY, bytes);
            Buffer.BlockCopy(bytesMonsterDefaults, offset + 1 + numMonsters * 4, bytes, 0, numMonsters * 2);
            WriteOffset(MM3Memory.MonsterHP, bytes);
            Buffer.BlockCopy(bytesMonsterDefaults, offset + 1 + numMonsters * 6, bytes, 0, numMonsters * 2);
            WriteOffset(MM3Memory.MonsterConditions, bytes);

            bytes = new byte[1] { numMonsters };
            WriteOffset(MM3Memory.NumberOfMonsters, bytes);

            return false;
        }

        public override int GetCurrentMapIndex()
        {
            if (!IsValid)
                return -1;

            return ReadByte(MM3Memory.CurrentMapIndex);
        }

        public override void SelectGameFiles()
        {
            SelectGameFilesForm form = new SelectGameFilesForm(Game);
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_fileCurrentData = new WatchedFile(MM3CurrentDataFile, "Please select your MM3.CUR file for more complete quest information", false);
                if (m_fileCurrentData.IsValid)
                {
                    Games.SetRosterPath(GameNames.MightAndMagic3, Path.GetDirectoryName(m_fileCurrentData.FileName));
                    Games.SetRosterFile(GameNames.MightAndMagic3, Path.GetFileName(m_fileCurrentData.FileName));
                }
            }

        }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            MM3QuestInfo info = new MM3QuestInfo();

            MM3PartyInfo party = ReadMM3PartyInfo();
            if (party == null)
                return null;

            MM3GameInfo gameInfo = GetGameInfo() as MM3GameInfo;
            if (gameInfo == null)
                return null;

            MemoryStream stream = new MemoryStream();
            byte[] questBytes = party.QuestBytes;
            if (questBytes == null)
                return null;
            stream.Write(questBytes, 0, questBytes.Length);
            stream.WriteByte((byte)iOverrideCharAddress);
            stream.WriteByte((byte)gameInfo.Party.Day);
            int iMap = GetCurrentMapIndex();
            info.MapIndex = iMap;
            stream.WriteByte((byte)iMap);

            MemoryBytes partyBits = GetPartyStaticBits();
            if (partyBits != null)
                stream.Write(partyBits, 0, partyBits.Length);

            if (m_fileCurrentData == null)
                m_fileCurrentData = new WatchedFile(MM3CurrentDataFile, false);

            if (bAllowSelectionDialog && m_fileCurrentData == null)
            {
                m_fileCurrentData = new WatchedFile(MM3CurrentDataFile, false);
                if (!m_fileCurrentData.IsValid)
                    SelectGameFiles();
            }

            if (iMap != m_lastQuestMap && m_fileCurrentData != null)
            {
                m_fileCurrentData.ForceRead();
                m_lastQuestMap = iMap;
            }

            if (m_fileCurrentData != null && !m_fileCurrentData.UserCanceled)
                m_fileQuestInfo.SetInfo(m_fileCurrentData.GetBytes(), GetScriptBytes(), GetMonsterBytes(), GetMapBytes(), iMap);
            else
                m_fileQuestInfo.Valid = false;

            if (m_fileQuestInfo != null)
            {
                byte[] bytesFQI = m_fileQuestInfo.GetBytes();
                stream.Write(bytesFQI, 0, bytesFQI.Length);
            }

            byte[] newBytes = stream.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(new MM3QuestData(party, gameInfo, partyBits, m_fileQuestInfo), iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override bool SetQuestBits(int iAddress, QuestBits bits, bool bSet)
        {
            if (iAddress < 0)
                return false;

            if (!IsValid)
                return false;

            MM3PartyInfo party = ReadMM3PartyInfo();
            if (party == null)
                return false;

            byte[] oneByte = new byte[1] { (byte)(bSet ? 1 : 0) };

            foreach (object bit in bits.Bits)
            {
                if (bit is MM3QuestStates.Skills)
                {
                    MM3SecondarySkills skills = new MM3SecondarySkills((byte) (bSet ? 1 : 0));
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Skills, skills.GetBytes());
                }
                else if (bit is MM3QuestStates.Guilds)
                {
                    byte[] bytes = new byte[5];
                    for (int i = 0; i < 5; i++)
                        bytes[i] = (byte)(bSet ? 1 : 0);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards, bytes);
                }
                else if (bit is MM3AwardIndex)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int) bit, oneByte);
                }
                else if (bit is MM3QuestStates.Blessed)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int) MM3AwardIndex.BlessedByTheForces, oneByte);
                }
                else if (bit is MM3QuestStates.Main)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.UltimateAdventurer, oneByte);
                }
                else if (bit is MM3QuestStates.Orbs)
                {
                    byte[] bytesTen = new byte[] { 10 };
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.OrbsGivenToMalefactor, bSet ? bytesTen : oneByte);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.OrbsGivenToTumult, bSet ? bytesTen : oneByte);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.OrbsGivenToZealot, bSet ? bytesTen : oneByte);
                }
                else if (bit is MM3QuestStates.FortressKeys)
                {
                    // Don't add or remove items from inventory
                }
                else if (bit is MM3QuestStates.SequencingCards)
                {
                    // Don't add or remove items from inventory
                }
                else if (bit is MM3QuestStates.Pearls)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.PearlsToPirateQueen, oneByte);
                }
                else if (bit is MM3QuestStates.Shells)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.ShellsGivenToAthea, oneByte);
                }
                else if (bit is MM3QuestStates.Skulls)
                {
                    byte[] bytesFive = new byte[] { 5 };
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.SkullsGivenToKranion, bSet ? bytesFive : oneByte);
                }
                else if (bit is MM3QuestStates.Arena)
                {
                    byte[] bytes76 = new byte[] { 76 };
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.ArenaWins, bSet ? bytes76 : oneByte);
                }
                else if (bit is MM3QuestStates.Trueberry)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.FreedPrincessTrueberry, oneByte);
                }
                else if (bit is MM3QuestStates.Alacorn)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.FreedPrincessTrueberry, oneByte);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.IcarusResurrected, oneByte);
                }
                else if (bit is MM3QuestStates.Blackwind)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.BlackwindReleased231, oneByte);
                }
                else if (bit is MM3QuestStates.Greywind)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.GreywindReleased645, oneByte);
                }
                else if (bit is MM3QuestStates.Artifacts)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.EvilArtifactsRecovered, oneByte);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.GoodArtifactsRecovered, oneByte);
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.NeutralArtifactsRecovered, oneByte);
                }
                else if (bit is MM3QuestStates.GreekBrothers)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Awards + (int)MM3AwardIndex.GreekBrothersVisited, oneByte);
                }
                else if (bit is MM3SpellIndex)
                {
                    WriteOffset(MM3Memory.PartyInfo + MM3Character.SizeInBytes * iAddress + MM3.Offsets.Spells + MM3KnownSpells.RawByteIndex((MM3SpellIndex)bit), oneByte);
                }
            }

            return true;
        }

        private byte[] GetMM3LoadedFileBytes(int iPointerOffset, int iLengthOffset)
        {
            if (!IsValid)
                return null;

            UInt16 length = ReadUInt16(iLengthOffset);
            int ptr = ReadUInt16(iPointerOffset) * 16 + 32;

            MemoryBytes bytesFile = ReadOffset(-m_offsetFoundBlock + ptr, length);
            if (Global.CompareBytes(bytesFile, MM3Memory.FileHeaderTest, 24, 0, 8))
                bytesFile = ReadOffset(-m_offsetFoundBlock + ptr + 32, length);
            if (bytesFile == null)
                return null;

            return bytesFile;
        }

        public override MemoryBytes GetScriptBytes()
        {
            return new MemoryBytes(GetMM3LoadedFileBytes(MM3Memory.ScriptPointer, MM3Memory.ScriptLength), MM3Memory.ScriptPointer);
        }

        public int AppearanceOffsetForQuad(int iQuad)
        {
            switch (iQuad)
            {
                case 0: return MM3Memory.MapAppearance1;
                case 1: return MM3Memory.MapAppearance2;
                case 2: return MM3Memory.MapAppearance3;
                case 3: return MM3Memory.MapAppearance4;
                default: return -1;
            }
        }

        public int CartographyOffsetForQuad(int iQuad)
        {
            switch (iQuad)
            {
                case 0: return MM3Memory.MapCartography1;
                case 1: return MM3Memory.MapCartography2;
                case 2: return MM3Memory.MapCartography3;
                case 3: return MM3Memory.MapCartography4;
                default: return -1;
            }
        }

        public List<MemoryBytes> GetMapBytes()
        {
            if (!IsValid)
                return null;

            // For maps 11-23, the data is always all four quads combined
            // For other maps, the data is a single quad, located at CurrentMapQuadIndex

            List<MemoryBytes> list = new List<MemoryBytes>(1);

            int iMap = GetCurrentMapIndex();
            if (iMap < 11 || iMap > 23)
            {
                MemoryBytes bytesQuad0 = ReadOffset(MM3Memory.MapAppearance1, 0x300);
                list.Add(bytesQuad0);
                return list;
            }

            for(int i = 0; i < 3; i++)
            {
                MemoryBytes bytesQuad = ReadOffset(AppearanceOffsetForQuad(i), 0x300);
                list.Add(bytesQuad);
            }

            return list;
        }

        public override GameScripts GetScripts()
        {
            return GetScripts(GetScriptBytes());
        }

        public override GameScripts GetScripts(MemoryBytes bytes)
        {
            return new MM3Scripts(GetSpecialSquares(bytes));
        }

        public bool Is32x32Map(MM3Map map)
        {
            switch (map)
            {
                case MM3Map.B1CyclopsCavern:
                case MM3Map.B4ArachnoidCavern:
                case MM3Map.D1CursedColdCavern:
                case MM3Map.F1DragonCavern:
                case MM3Map.E4TheMagicCavern:
                case MM3Map.A1AncientTempleOfMoo:
                case MM3Map.B1SlithercultStronghold:
                case MM3Map.B2FortressOfFear:
                case MM3Map.A3HallsOfInsanity:
                case MM3Map.B3DarkWarriorKeep:
                case MM3Map.B3CathedralOfCarnage:
                case MM3Map.F2TombOfTerror:
                case MM3Map.F3TheMazeFromHell:
                    return true;
                default:
                    return false;
            }
        }

        public override bool SetScriptLine(ScriptLine line)
        {
            if (!IsValid)
                return false;

            return WriteLoadedFileOffset(MM3Memory.ScriptPointer, line.Bytes.Offset + 1, line.Bytes);
        }

        public override MemoryBytes GetPartyStaticBits()
        {
            if (!IsValid)
                return null;

            return ReadOffset(MM3Memory.PartyStaticBits, 32);
        }

        public override bool SetPartyStaticBits(byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(MM3Memory.PartyStaticBits, bytes);
        }

        public override string BagOfHoldingRequirement { get { return "in a town"; } }

        public override MapCartography GetCartography()
        {
            if (!IsValid)
                return null;

            MapCartography cart = new MM3MapCartography();
            cart.MapIndex = GetCurrentMapIndex();

            MemoryBytes bytes = null;

            if (Is32x32Map((MM3Map)cart.MapIndex))
            {
                bytes = ReadOffset(MM3Memory.MapCartography1, 32);
                MemoryBytes bytes2 = ReadOffset(MM3Memory.MapCartography2, 32);
                MemoryBytes bytes3 = ReadOffset(MM3Memory.MapCartography3, 32);
                MemoryBytes bytes4 = ReadOffset(MM3Memory.MapCartography4, 32);

                if (bytes == null || bytes2 == null || bytes3 == null || bytes4 == null)
                    return null;
                cart.SetFromQuad(bytes, bytes2, bytes3, bytes4);
                return cart;
            }

            int iQuad = GetCurrentMapQuad();
            switch (iQuad)
            {
                case 1:
                    bytes = ReadOffset(MM3Memory.MapCartography2, 32);
                    break;
                case 2:
                    bytes = ReadOffset(MM3Memory.MapCartography3, 32);
                    break;
                case 3:
                    bytes = ReadOffset(MM3Memory.MapCartography4, 32);
                    break;
                default:
                    bytes = ReadOffset(MM3Memory.MapCartography1, 332);
                    break;
            }

            if (bytes == null)
                return null;
            cart.SetBytes(bytes, new Size(16, 16));
            return cart;
        }

        public override string[] CurrentDataFiles
        {
            get
            {
                return new string[] { MM3CurrentDataFile };
            }
            set
            {
                MM3CurrentDataFile = (value == null || value.Length < 1 ? String.Empty : value[0]);
            }
        }

        public override bool EditMapCartography(MapCartography.EditAction action)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            switch (action)
            {
                case MapCartography.EditAction.FillSingle:
                case MapCartography.EditAction.FillAll:
                    for(int i = 0; i < bytes.Length; i++)
                        bytes[i] = 0xff;
                    break;
                default:
                    break;
            }

            switch (action)
            {
                case MapCartography.EditAction.FillAll:
                case MapCartography.EditAction.ClearAll:
                    if (m_fileCurrentData == null || !m_fileCurrentData.CanWrite)
                    {
                        m_fileCurrentData = new WatchedFile(MM3CurrentDataFile, "Please select your MM3.CUR file to edit the cartography data", false, true);
                        if (m_fileCurrentData.IsValid)
                            MM3CurrentDataFile = m_fileCurrentData.FileName;
                    }
                    if (m_fileCurrentData.IsValid)
                    {
                        foreach(uint offset in MM3FileOffsets.StaticMaps)
                        {
                            if (offset == 0)
                                continue;
                            m_fileCurrentData.WriteBytes(offset + MM3FileOffsets.CartographyOffset, bytes);
                        }
                    }
                    goto case MapCartography.EditAction.ClearSingle;
                case MapCartography.EditAction.FillSingle:
                case MapCartography.EditAction.ClearSingle:
                    int iMap = GetCurrentMapIndex();
                    if (Is32x32Map((MM3Map)iMap))
                    {
                        WriteOffset(MM3Memory.MapCartography1, bytes);
                        WriteOffset(MM3Memory.MapCartography2, bytes);
                        WriteOffset(MM3Memory.MapCartography3, bytes);
                        WriteOffset(MM3Memory.MapCartography4, bytes);
                    }
                    else
                    {
                        int iQuad = GetCurrentMapQuad();
                        WriteOffset(CartographyOffsetForQuad(iQuad), bytes);
                    }
                    return true;
            }
            return false;
        }

        public static string FacingString(int i, bool bAbbrev = false)
        {
            switch (i)
            {
                case 0: return "North";
                case 1: return "South";
                case 2: return "East";
                case 3: return "West";
                case 4: return "All";
                default: return bAbbrev ? "?" : "Unknown";
            }
        }

        public static DirectionFlags FacingDirection(int i)
        {
            switch (i)
            {
                case 0: return DirectionFlags.North;
                case 1: return DirectionFlags.South;
                case 2: return DirectionFlags.East;
                case 3: return DirectionFlags.West;
                case 4: return DirectionFlags.All;
                default: return DirectionFlags.None;
            }
        }

        public override bool HasScripts { get { return true; } }
        public override bool SpellsUseLevelOnly { get { return true; } }
        public override RosterFile CurrentRoster { get { return m_mm3Roster; } }

        public int GetNumChars()
        {
            if (!IsValid)
                return 0;

            return ReadByte(MM3Memory.NumChars);
        }

        public override bool HasParty { get { return GetNumChars() > 0; } }

        public override Size GetCurrentMapDimensions()
        {
            int iMap = GetCurrentMapIndex();
            if (Is32x32Map((MM3Map) iMap))
                return new Size(32, 32);
            return new Size(16, 16);
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * MM3Character.SizeInBytes;
            CharacterOffsets offsets = MM3.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + MM3Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            // Single-byte 255 values:
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
                offsets.EnergyResist, offsets.EnergyResistMod,
                offsets.PoisonResist, offsets.PoisonResistMod,
                offsets.MagicResist, offsets.MagicResistMod,
                offsets.HolyBonus, offsets.PowerShield, offsets.Blessed, offsets.Heroism,
                offsets.ArmorClassMod, offsets.Level, offsets.LevelMod})
                info.Bytes[offset + lOffset] = 255;

            // A Spell Points value of 0xffff shows in the UI as 65535, but causes "not enough spell points to cast", so limit to 32767
            Buffer.BlockCopy(Global.MaxInt16, 0, info.Bytes, offset + offsets.CurrentSP, Global.MaxInt16.Length);
            Buffer.BlockCopy(Global.MaxInt16, 0, info.Bytes, offset + offsets.CurrentHP, Global.MaxInt16.Length);
            Buffer.BlockCopy(Global.MaxUInt32, 0, info.Bytes, offset + offsets.Experience, Global.MaxUInt32.Length);

            MM345Class mmClass = (MM345Class)info.Bytes[offset + offsets.Class];

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Barbarian:
                case MM345Class.Robber:
                case MM345Class.Ninja:
                    Global.SetBytes(info.Bytes, offset + offsets.Spells, offsets.SpellsLength, 0);
                    break;
                default:
                    Global.SetBytes(info.Bytes, offset + offsets.Spells, offsets.SpellsLength, 1);
                    break;
            }

            Global.SetBytes(info.Bytes, offset + offsets.Skills, offsets.SkillsLength, 1);
            Global.SetBytes(info.Bytes, offset + offsets.Condition, offsets.ConditionLength, 0);
            info.Bytes[offset + offsets.AgeModifier] = 0;

            WriteOffset(MM3Memory.PartyInfo, info.Bytes);

            byte obsidian = (byte)MM3ItemMaterialIndex.Obsidian;
            byte divine = (byte)MM3ItemAttributeIndex.Divine;
            byte kinetic = (byte)MM3ItemElementalIndex.Kinetic;

            List<Item> items = new List<Item>(23);

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                case MM345Class.Archer:
                case MM345Class.Ranger:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Flamberge, (byte)MM3ItemPropertyIndex.Teleportation));
                    break;
                case MM345Class.Robber:
                case MM345Class.Barbarian:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.GreatAxe, (byte)MM3ItemPropertyIndex.Teleportation));
                    break;
                case MM345Class.Cleric:
                case MM345Class.Druid:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Hammer, (byte)MM3ItemPropertyIndex.Teleportation));
                    break;
                case MM345Class.Sorcerer:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Staff, (byte)MM3ItemPropertyIndex.Teleportation));
                    break;
                case MM345Class.Ninja:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Halberd, (byte)MM3ItemPropertyIndex.Teleportation));
                    break;
                default:
                    break;
            }

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                case MM345Class.Archer:
                case MM345Class.Robber:
                case MM345Class.Ranger:
                case MM345Class.Barbarian:
                case MM345Class.Ninja:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.LongBow, (byte)MM3ItemPropertyIndex.Beacons));
                    break;
                default:
                    break;
            }

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.PlateArmor, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                case MM345Class.Archer:
                case MM345Class.Robber:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.ChainMail, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                case MM345Class.Cleric:
                case MM345Class.Ranger:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.SplintMail, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                case MM345Class.Sorcerer:
                case MM345Class.Druid:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.PaddedArmor, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                case MM345Class.Barbarian:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.ScaleArmor, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                case MM345Class.Ninja:
                    items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.RingMail, (byte)MM3ItemPropertyIndex.TheGods));
                    break;
                default:
                    break;
            }

            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Cloak, (byte)MM3ItemPropertyIndex.StarBursts));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Boots, (byte)MM3ItemPropertyIndex.Recharging));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Helm, (byte)MM3ItemPropertyIndex.Resurrection));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Gauntlets, (byte)MM3ItemPropertyIndex.Portals));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Belt, (byte)MM3ItemPropertyIndex.Levitation));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Amulet, (byte)MM3ItemPropertyIndex.Implosions));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Broach, (byte)MM3ItemPropertyIndex.HolyWords));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Cameo, (byte)MM3ItemPropertyIndex.DragonBreath));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Ring, (byte)MM3ItemPropertyIndex.Gating));
            items.Add(MM3Item.Create(0, 63, kinetic, obsidian, divine, (byte)MM3ItemIndex.Ring, (byte)MM3ItemPropertyIndex.Etherealization));

            AddItemsToBackpack(iAddress, items);

            return true;
        }

        public void AddItemsToBackpack(int iAddress, List<Item> items)
        {
            if (items == null)
                return;

            List<Item> equipped = GetEquippedItems(iAddress);

            MM3BackpackBytes bpBytes = MM3BackpackBytes.Create(equipped);
            bpBytes.Add(items);

            WriteOffset(MM3Memory.PartyInfo + (iAddress * MM3Character.SizeInBytes) + MM3.Offsets.Inventory, bpBytes.GetBytes());
        }

        public override Subscreen GetSubscreen()
        {
            return ReadMM3GameState().Subscreen;
        }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new MM3GameInformationControl(main); }

        public override string GetMapEnum(int index)
        {
            return String.Format("MM3Map.{0}", Enum.GetName(typeof(MM3Map), (MM3Map)(index)));
        }

        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "All Resistances 7, Swimming";
                case GenericRace.Elf: return "Energy/Magic 5, Thievery 10, -2HP,+2MP/Lv  (Sorc/Arc)";
                case GenericRace.Dwarf: return "Fire/Elec/Cold/Energy 5, Poison 20, Thievery 10, +1HP,-1SP/Lv, Spot Secret Doors";
                case GenericRace.Gnome: return "Fire/Elec/Cold/Poison/Energy 2, Magic 20, Thievery 5, -1HP,+1SP/Lv, Danger Sense";
                case GenericRace.HalfOrc: return "Fire/Elec/Cold 10, Thievery -10, +2HP,-2SP/Lv";
                default: return "Unknown";
            }
        }

        public override MMMonster GetDefaultMonster(int iIndex, int iSide = 0)
        {
            MM3MonsterList list = new MM3MonsterList();
            if (iIndex >= list.Monsters.Count)
                return null;
            return list.Monsters[iIndex];
        }

        public override bool HasCartography { get { return true; } }

        public override bool ActiveMonstersNearby
        {
            get
            {
                Point ptParty = GetPartyPosition();
                MonsterLocations monsters = GetMonsterLocations();
                foreach (MonsterPosition pos in monsters.MonsterPositions.Values)
                {
                    Proximity prox = new Proximity(pos.Position, ptParty);
                    if (prox.Simple < 4 && pos.Monsters.Any(m => m.Active))
                        return true;
                }
                return false;
            }
        }

        public byte[] GetMonsterListBytes()
        {
            const int iCount = 90;
            byte[] bytesStats = new byte[MM3Memory.MonFileEnd];

            uint offset = MM3Memory.MonsterBase - m_offsetFoundBlock;
            MemoryBytes testBytes = ReadOffset(offset + 24, 8);
            if (Global.Compare(testBytes.Bytes, MM3Memory.FileHeaderTest))
                offset += 32;

            ReadOffset(offset + MM3Memory.MonMemPhysicalResist, iCount).CopyTo(bytesStats, 0 * iCount);
            ReadOffset(offset + MM3Memory.MonMemEnergyResist, iCount).CopyTo(bytesStats, 1 * iCount);
            ReadOffset(offset + MM3Memory.MonMemAcidResist, iCount).CopyTo(bytesStats, 2 * iCount);
            ReadOffset(offset + MM3Memory.MonMemColdResist, iCount).CopyTo(bytesStats, 3 * iCount);
            ReadOffset(offset + MM3Memory.MonMemElecResist, iCount).CopyTo(bytesStats, 4 * iCount);
            ReadOffset(offset + MM3Memory.MonMemFireResist, iCount).CopyTo(bytesStats, 5 * iCount);
            ReadOffset(offset + MM3Memory.MonMemMagicResist, iCount).CopyTo(bytesStats, 6 * iCount);
            ReadOffset(offset + MM3Memory.MonMemGems, 2 * iCount).CopyTo(bytesStats, 7 * iCount);
            ReadOffset(offset + MM3Memory.MonMemGold, 4 * iCount).CopyTo(bytesStats, 9 * iCount);
            ReadOffset(offset + MM3Memory.MonMemItems, iCount).CopyTo(bytesStats, 13 * iCount);
            ReadOffset(offset + MM3Memory.MonMemAccuracy, iCount).CopyTo(bytesStats, 14 * iCount);
            ReadOffset(offset + MM3Memory.MonMemRanged, iCount).CopyTo(bytesStats, 15 * iCount);
            ReadOffset(offset + MM3Memory.MonMemSpecialPower, iCount).CopyTo(bytesStats, 16 * iCount);
            ReadOffset(offset + MM3Memory.MonMemTarget, iCount).CopyTo(bytesStats, 17 * iCount);
            ReadOffset(offset + MM3Memory.MonMemDamageType, iCount).CopyTo(bytesStats, 18 * iCount);
            ReadOffset(offset + MM3Memory.MonMemExperience, 4 * iCount).CopyTo(bytesStats, 19 * iCount);
            ReadOffset(offset + MM3Memory.MonMemNumAttacks, iCount).CopyTo(bytesStats, 23 * iCount);
            ReadOffset(offset + MM3Memory.MonMemDamageDieMax, iCount).CopyTo(bytesStats, 24 * iCount);
            ReadOffset(offset + MM3Memory.MonMemDamageNumDice, iCount).CopyTo(bytesStats, 25 * iCount);
            ReadOffset(offset + MM3Memory.MonMemSpeed, iCount).CopyTo(bytesStats, 26 * iCount);
            ReadOffset(offset + MM3Memory.MonMemAC, iCount).CopyTo(bytesStats, 27 * iCount);
            ReadOffset(offset + MM3Memory.MonMemHPMax, 2 * iCount).CopyTo(bytesStats, 28 * iCount);

            return bytesStats;
        }

        private int GetCartographyAddress(int x, int y)
        {
            if (x > 15)
                return y > 15 ? MM3Memory.MapCartography4 : MM3Memory.MapCartography2;
            return y > 15 ? MM3Memory.MapCartography3 : MM3Memory.MapCartography1;
        }

        public override bool IsExplored(int x, int y)
        {
            // Should be faster than GetCartography().IsBitSet() for a single square

            if (!IsValid)
                return true;

            if (!PointInMap(new Point(x,y)))
                return true;

            int offset = GetCartographyAddress(x,y);
            byte bCartography = ReadByte(offset + ((y % 16) * 2) + ((x % 16) / 8));
            return (bCartography >> (7 - ((x % 8))) & 1) == 1;
        }

        public override bool ToggleCartography(Point pt, Toggle toggle)
        {
            if (!IsValid)
                return false;

            if (!PointInMap(pt))
                return false;

            int offset = GetCartographyAddress(pt.X, pt.Y);
            int iDelta = ((pt.Y % 16) * 2) + ((pt.X % 16) / 8);
            byte bCartography = ReadByte(offset + iDelta);
            byte bit = (byte)(1 << (7 - (pt.X % 8)));
            switch (toggle)
            {
                case Toggle.Toggle:
                    bCartography ^= bit;
                    break;
                case Toggle.Reset:
                    bCartography &= (byte)~bit;
                    break;
                case Toggle.Set:
                    bCartography |= bit;
                    break;
            }
            return WriteByte(offset + iDelta, bCartography);
        }

        public override long GetGameTimeLong()
        {
            long year = ReadUInt16(MM3Memory.Year);
            long day = ReadByte(MM3Memory.Day);
            long minutes = ReadUInt16(MM3Memory.Minutes);
            return minutes | (day << 16) | (year << 24);
        }

        public bool SetAwards(int iCharIndex, byte[] awards)
        {
            if (!IsValid)
                return false;

            if (awards == null || awards.Length < MM3.Offsets.AwardsLength)
                return false;

            if (iCharIndex < 0 || iCharIndex > GetNumChars())
                return false;

            return WriteOffset(MM3Memory.PartyInfo + (iCharIndex * MM3Character.SizeInBytes) + MM3.Offsets.Awards, awards);
        }

        public override bool RefreshConditions()
        {
            MM3GameState state = ReadMM3GameState();
            switch (state.Main)
            {
                case MainState.Adventuring:
                    SendKeysToDOSBox(new Keys[] { Keys.D, Keys.Escape }, true);
                    break;
                case MainState.Inventory:
                    SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                    break;
                case MainState.CharacterScreen:
                    SendKeysToDOSBox(new Keys[] { Keys.E, Keys.Escape }, true);
                    break;
                case MainState.QuickRef:
                    if (state.Inspecting)
                        SendKeysToDOSBox(new Keys[] { Keys.Escape, Keys.E, Keys.Escape }, true);
                    else
                        SendKeysToDOSBox(new Keys[] { Keys.Escape, Keys.D, Keys.Escape }, true);
                    break;
                default:
                    SendKeysToDOSBox(new Keys[] { Keys.D, Keys.Escape }, true);
                    break;
            }
            return true;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            MM3GameInfo gi = GetGameInfo() as MM3GameInfo;
            for (int i = 0; i < pi.NumChars; i++)
                chars.Add(MM3Character.Create(pi.Bytes, MM3Character.SizeInBytes * i, gi));

            return chars;
        }

        public override Point GetSurfaceSector(int iMap)
        {
            switch ((MM3Map)iMap)
            {
                case MM3Map.A1Surface: return new Point(0, 3);
                case MM3Map.A2Surface: return new Point(0, 2);
                case MM3Map.A3Surface: return new Point(0, 1);
                case MM3Map.A4Surface: return new Point(0, 0);
                case MM3Map.B1Surface: return new Point(1, 3);
                case MM3Map.B2Surface: return new Point(1, 2);
                case MM3Map.B3Surface: return new Point(1, 1);
                case MM3Map.B4Surface: return new Point(1, 0);
                case MM3Map.C1Surface: return new Point(2, 3);
                case MM3Map.C2Surface: return new Point(2, 2);
                case MM3Map.C3Surface: return new Point(2, 1);
                case MM3Map.C4Surface: return new Point(2, 0);
                case MM3Map.D1Surface: return new Point(3, 3);
                case MM3Map.D2Surface: return new Point(3, 2);
                case MM3Map.D3Surface: return new Point(3, 1);
                case MM3Map.D4Surface: return new Point(3, 0);
                case MM3Map.E1Surface: return new Point(4, 3);
                case MM3Map.E2Surface: return new Point(4, 2);
                case MM3Map.E3Surface: return new Point(4, 1);
                case MM3Map.E4Surface: return new Point(4, 0);
                case MM3Map.F1Surface: return new Point(5, 3);
                case MM3Map.F2Surface: return new Point(5, 2);
                case MM3Map.F3Surface: return new Point(5, 1);
                case MM3Map.F4Surface: return new Point(5, 0);
                default: return new Point(-1, -1);
            }
        }

        public override DirectionsTo GetDirections(int iMap, Point ptLocation, bool bNorthIncreases = true, bool bEastIncreases = true)
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

            // If we aren't on surface maps on the same side of Xeen, the directions are "impossible"
            // (i.e. it requires more than just n/s/e/w travel to get there)

            if (!(IsOutside((MM3Map)iMap) && IsOutside((MM3Map)li.MapIndex)))
                return DirectionsTo.Impossible; // Not both surface maps

            Point ptSurfaceTarget = GetSurfaceSector(iMap);
            Point ptSurfaceParty = GetSurfaceSector(li.MapIndex);

            return new DirectionsTo((ptSurfaceTarget.Y - ptSurfaceParty.Y) * 16 * (bNorthIncreases ? 1 : -1) + iNorth,
                (ptSurfaceTarget.X - ptSurfaceParty.X) * 16 * (bEastIncreases ? 1 : -1) + iEast);
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                MM3GameState state = ReadMM3GameState();
                if (state != null)
                {
                    switch (state.OriginalMain)
                    {
                        case MainState.Opening:
                        case MainState.Opening2:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                            TweakSleep(50);
                            break;
                        case MainState.MainMenu:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Escape, Keys.L }, true);
                            TweakSleep(200);
                            break;
                        case MainState.LoadGame:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.D1, Keys.Enter }, true);
                            TweakSleep(500);
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
            return MM3.Monsters;
        }

        public int GetCurrentMapQuadMemoryOffset()
        {
            switch (ReadByte(MM3Memory.CurrentMapQuadIndex))
            {
                case 1: return MM3Memory.MapAppearance2;
                case 2: return MM3Memory.MapAppearance3;
                case 3: return MM3Memory.MapAppearance4;
                default: return MM3Memory.MapAppearance1;
            }
        }

        public override MapBytes GetCurrentMapBytes()
        {
            Size sz = GetCurrentMapDimensions();
            if (sz.Width == 32 && sz.Height == 32)
            {
                MemoryBytes mb1 = ReadOffset(MM3Memory.MapAppearance1, 512);
                MemoryBytes mb2 = ReadOffset(MM3Memory.MapAppearance2, 512);
                MemoryBytes mb3 = ReadOffset(MM3Memory.MapAppearance3, 512);
                MemoryBytes mb4 = ReadOffset(MM3Memory.MapAppearance4, 512);
                if (mb1 == null || mb2 == null || mb3 == null || mb4 == null)
                    return null;

                return new MapBytes(MMMapData.CreateQuadBytes(32, mb1.Bytes, mb2.Bytes, mb3.Bytes, mb4.Bytes), 32, 32);
            }

            return new MapBytes(ReadOffset(GetCurrentMapQuadMemoryOffset(), 512), 16, 16);
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            MM3MapData data = new MM3MapData();
            data.LiveOnly = true;
            data.Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            data.Appearance = mb.Bytes;
            return data;
        }

        public override SpellHotkeyList GetSpellHotkeys() { return Properties.Settings.Default.MM3SpellKeys; }
    }
}
