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
    public static class MM2Memory
    {
        // Search for "P-Prot Q-Quick"
        public static byte[] MainSearch = new byte[] { 0x50, 0x2D, 0x50, 0x72, 0x6F, 0x74, 0x20, 0x51, 0x2D, 0x51, 0x75, 0x69, 0x63, 0x6B, 0x00 };
        public static byte[] MonsterSearch = new byte[] {0xC3, 0xF2, 0xE5, 0xE5, 0xF0, 0xF9, 0xA0, 0xC3, 0xF2, 0xE1, 0xF7, 0xEC, 0xE5, 0xF2,
        0x04, 0x2E, 0x00, 0x00, 0x23, 0x65, 0x01, 0x01, 0x03, 0x05, 0x13, 0x03, 0xC7, 0xE9, 0xE1, 0xEE, 0xF4, 0xA0};
        public static byte[] CastSpell = new byte[] {0x43, 0x61, 0x73, 0x74, 0x20, 0x53, 0x70, 0x65, 0x6C, 0x6C};   // "Cast Spell"
        public static byte[] SpellNumCombat = new byte[] { 0xA9, 0x20, 0xA0, 0xA0 };
        public static byte[] SpellNumNonCombat = new byte[] { 0x55, 0x31, 0x00, 0x37 };

        public const int MainBlockStart = -72528;
        public const int CreationStats = 40384;           // 7 bytes of stats
        public const int UnmodifiedStats = 40350;         // 7 bytes of stats
        public const int MainState = -33082;              // 2 bytes
        public const int PartyInfo = 27232;               // Each character is 130 bytes
        public const int NumChars1 = -3994;               // 1 byte
        public const int NumChars2 = 16894;               // 1 byte
        public const int CharacterOrder = -4010;          // 16 bytes
        public const int HirelingChars = 30352;           // 3120 bytes (24 chars, 130 bytes each)
        public const int CharacterScreenOffset = 40368;   // 2 bytes
        public const int FirstCharacterInternal = 0x7E20;
        public const int QuickRefInternal = 0x3271;
        public const int Location = -4141;                // 2 bytes (x,y)
        public const int IndoorFacing = 998;              // 1 byte N/W/S/E = 0/2/4/6
        public const int LastFPKeypress = 40408;          // 1 byte; last key pressed in first-person mode
        public const int MapAppearance = 17942;           // 256 bytes, nneessww, empty/wall/door/torch (town)
        public const int MapAttributes = 18198;           // 256 bytes, SnDeRsAw, Special/North/Dark/East/Rest/South/Antimagic/West
        public const int OutdoorFacing = 17927;           // 1 byte N/W/S/E = 0/2/4/6
        public const int CurrentDay = -4108;              // 1 byte
        public const int CurrentYear = -4088;             // 2 bytes
        public const int IsOutside = -4131;               // 1 byte; 1 = outside, 0 = inside
        public const int StepCounter = -4084;             // bit 7 set = night, resting adds 85 to it, wraparound adds 1 to character age in days
        public const int MapCode = 19602;                 // Max length 2304
        public const int MapCodeLength = 2304;
        public const int CurrentMapIndex = -4142;
        public const int SearchItems = 21904;             // 3 bytes
        public const int SearchBonus = 21907;             // 3 bytes
        public const int SearchCharges = 21910;           // 3 bytes
        public const int SearchGems = 21914;              // 2 bytes
        public const int SearchGold = 21916;              // 4 bytes
        public const int NumMonsters = -3768;             // 1 byte
        public const int MonsterHP = 35818;               // 20 bytes
        public const int EncounterList = 33472;           // 10 bytes of active and 1 byte of reserve
        public const int MonsterCombatStatus = 35782;     // 10 bytes
        public const int Entrapment = 35844;              // 1 byte, 1=Entrapped, 0=Normal
        public const int CombatWaiting = 13732;           // 1 byte, 1=Waiting, 0=Casting
        public const int CastingSpell = 40287;            // 1 byte, 1=Casting
        public const int CastingState = 40279;            // 1 byte, 2/123=Select Level, 0=Select Number
        public const int CastingState2 = 40277;           // 1 byte
        public const int CombatCastingChar1 = 40270;      // 2 bytes
        public const int CombatCastingChar2 = 40280;      // 2 bytes
        public const int CombatCastingChar3 = 35858;      // 1 byte
        public const int PreEncounter = -4082;            // 2 = combat, 1 = precombat (if state is combat)
        public const int NumPartyInMelee = 16605;         // 1 byte
        public const int ActingCharTavernTrain = 40344;   // 1 byte
        public const int ActingCharShop = 17432;          // 1 byte
        public const int ActingCharTemple = 17672;        // 1 byte
        public const int CombatMonstersMoved = 16576;     // 10 bytes
        public const int CombatPartyMoved = 16588;        // 8 bytes; 1 = currently moving or has moved already this round
        public const int MonstersInMelee = 35845;         // 1 byte
        public const int MonsterData1 = 263408;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData2 = 300288;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData3 = 379680;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData4 = 386160;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData5 = 393520;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData6 = 423040;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData7 = 423248;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData8 = 422272;           // 0x1A00 bytes (26 per monster)
        public const int MonsterData9 = 308736;           // 0x1A00 bytes (26 per monster)
        public const int CombatActingCharPos = 35858;     // 1 byte
        public const int MonsterDataLocator = 13724;      // 2 bytes
        public const int FacingChar = -4081;              // 1 byte, 'N', 'S', 'E', 'W'
        public const int ExtraStrings = 36014;            // 2400 bytes
        public const int ExtraStringsLen = 2400;          //
        public const int ExtraStrings2 = 16656;           // 422 bytes
        public const int ExtraStrings2Len = 422;          //
        public const int CastSpellCombatString = 40170;   // "Cast Spell" if casting in combat
        public const int CastSpellNCString = 40204;       // "Cast Spell" if casting out of combat
        public const int CombatSpellNumString = 40178;    // 4 bytes; A9 20 A0 A0 if entering spell number in combat
        //public const int NCSpellState = 40266;            // 159: enter level, 231: enter number
        //public const int ComSpellState = 13933;           // 0f 0f: enter level, 10 0f: enter number
        public const int NonCombatSpellNumString = 40267; // 4 bytes; 55 31 00 37 if entering spell number outside combat
        public const int MonsterDataPointer = -3770;      // 2 bytes; offset from MainBlockStart (minus 2)
        public const int State2 = -33076;                 // 2 bytes
        public const int ActingCharSpecialties = 40312;   // 1 byte
        public const int DatesAndTimes = -4124;           // 40 bytes; 10 UInt16 days and 10 UInt16 years
        public const int BeaconLocation = -4056;          // 2 bytes; Map and YX coordinates
        public const int BattleStats = -4016;             // 4 bytes; Won/Lost UInt16
        public const int CurrentEra = -4086;              // 1 byte
        public const int BenefitsUsed1 = -4044;           // 1 byte
        public const int BenefitsUsed2 = -4043;           // 1 byte
        public const int Gwyndon = -4046;                 // 1 byte (0/1) Party has talked to Gwyndon
        public const int MapGlobalAttributes = 17862;     // 64 bytes
        public const int HirelingsRescued = -4042;        // 24 bytes
        public const int VisitedPegasus = -4054;          // 1 byte
        public const int Donations = -4062;               // 1 bytes, bits 0-4 are towns 1-5
        public const int ActiveEffects = -4075;           // 84 bytes
        public const int CartographyData = 33484;         // 1920 bytes (60 maps * 32 bytes/map)
        public const int ShopItems = 12296;               // 210 bytes (5 shops * 42 bytes/shop)
        public const int DailySpecials = 12506;           // 36 bytes
        public const int CurrentShopItems = 17402;        // 6 bytes
        public const int CurrentShopBonuses = 17486;      // 6 bytes
        public const int CurrentShopCharges = 17536;      // 6 bytes
        public const int Scripts = 19602;                 // Up to 2304 bytes
        public const int Command = -28236;                // 1 byte
        public const int Command2 = -60642;               // 1 byte

        public static MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.MightAndMagic2]; } }
    }

    public class MM2MapData : MMMapData
    {
        public MM2MapData()
        {
            BytesPerSquare = 1;
            Bounds = new Rectangle(0, 0, 16, 16);
        }
    }


    [Flags]
    public enum MM2DungeonMapAppearance
    {
        North = 0xC0,
        East = 0x30,
        South = 0xC0,
        West = 0x03
    }

    [Flags]
    public enum MM2MapBehavior
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

    [Flags]
    public enum MM2MapFlags1
    {
        None = 0x00,
        TeleportForbidden = 0x10,
        EtherealizeForbidden = 0x20,
        BasicTransportForbidden = 0x40,
        DefaultDark = 0x80
    }

    public class MM2MapInfo
    {
        public MM2DungeonMapAppearance[,] Appearance;   // 16x16 bytes
        public MM2MapBehavior[,] Behavior;       // 16x16 bytes

        public MM2MapInfo()
        {
            Appearance = new MM2DungeonMapAppearance[16, 16];
            Behavior = new MM2MapBehavior[16, 16];
        }

        public MM2MapInfo(byte[] bytes, int index = 0) : this() { SetBytes(bytes, index); }

        public void SetBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 512)
                return;

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Appearance[x, y] = (MM2DungeonMapAppearance)bytes[index + y * 16 + x];
                    Behavior[x, y] = (MM2MapBehavior)bytes[index + 256 + y * 16 + x];
                }
            }
        }
    }

    public class MM2CharCreationInfo : CharCreationInfo
    {
        public override bool ValidValues
        {
            get
            {
                if (AttributesModified == null)
                    return false;
                // For MM2 the original stats are copied from the modified location when
                // race is chosen.  When race is unchosen the AttributesOriginal array
                // is just random data.
                foreach (byte b in AttributesModified)
                    if (b < 1 || b > 23)
                        return false;

                return true;
            }
        }
    }

    public class MM2SpellInfo : SpellInfo
    {
        public MM2Spell Spell;
        public MM2PartyInfo Party;
        public override bool UsesSpellLevel { get { return true; } }

        public MM2SpellInfo()
        {
            Spell = null;
            Party = null;
            Game = new MM2GameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingCharAddress = -1;
        }
    }

    public class MM2SearchResults : SearchResults
    {
        private byte[] m_bytes = null;

        public MM2SearchResults(byte[] bytes, int index)
        {
            if (bytes.Length + index < 15)
            {
                Items = null;
                Gems = 0;
                Gold = 0;
                return;
            }

            m_bytes = new byte[15];
            Buffer.BlockCopy(bytes, index, m_bytes, 0, 15);
            Items = new List<Item>(3);

            for (int iItem = 0; iItem < 3; iItem++)
            {
                if (bytes[index + iItem] > 0)
                {
                    MM2Item item = MM2.Items[bytes[index + iItem]].Clone() as MM2Item;
                    item.BonusCurrent = (MM2BonusFlags) bytes[index + iItem + 3];
                    item.m_iChargesCurrent = bytes[index + iItem + 6];
                    Items.Add(item);
                }
            }

            Gems = BitConverter.ToUInt16(bytes, 10);
            Gold = BitConverter.ToInt32(bytes, 12);
        }

        public override byte[] Bytes { get { return m_bytes; } }
    }

    public class MM2TimeInfo
    {
        public byte CurrentEra;
        public ushort[] EraYears;
        public ushort[] EraDays;
        public byte Steps;
        public MemoryBytes Bytes;
    }

    public class MM2GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return MM2MemoryHacker.GetMapTitlePair(iMap); }

        public MM2GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public MM2GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public MM2GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class MM2GameInfo : GameInfo
    {
        public UInt16 BeaconAndPoint;
        public MM2Map BeaconMap;
        public Point BeaconPoint;
        public int BattlesWon;
        public int BattlesLost;
        public MM2Spell[] ForbiddenSpells;
        public bool BenefitsUsed1;
        public bool BenefitsUsed2;
        public bool Gwyndon;
        public MM2MapAttributes MapAttributes;
        public MMActiveEffects ActiveEffects;
        public byte[] HirelingTowns;
        public MM2TimeInfo Time;

        public MM2GameInfo()
        {
            Time = new MM2TimeInfo();
        }

        public override GameNames Game { get { return GameNames.MightAndMagic2; } }

        private byte ByteFromMapLocation(Point pt)
        {
            return (byte)((pt.Y << 4) | pt.X);
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int mapOffset = MM2Memory.MapGlobalAttributes;
            const int effects = MM2Memory.ActiveEffects;

            items.Add(new MM2GameInfoItem("Ban Teleport", (byte)MapAttributes.ForbiddenSpells, mapOffset + MM2MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x10));
            items.Add(new MM2GameInfoItem("Ban Etherealize", (byte)MapAttributes.ForbiddenSpells, mapOffset + MM2MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x20));
            items.Add(new MM2GameInfoItem("Ban Transport", (byte)MapAttributes.ForbiddenSpells, mapOffset + MM2MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x40));
            items.Add(new MM2GameInfoItem("Prot. Forces", (byte)ActiveEffects.ProtForces, effects + MM2EffectsOffsets.ProtForces));
            items.Add(new MM2GameInfoItem("Prot. Magic", (byte)ActiveEffects.ProtMagic, effects + MM2EffectsOffsets.ProtMagic));
            items.Add(new MM2GameInfoItem("Light", (byte)ActiveEffects.LightFactors, effects + MM2EffectsOffsets.LightFactors));
            items.Add(new MM2GameInfoItem("Holy Bonus", (byte)ActiveEffects.HolyBonus, effects + MM2EffectsOffsets.HolyBonus));
            items.Add(new MM2GameInfoItem("Water Trans.", ActiveEffects.WaterTransmutation, effects + MM2EffectsOffsets.WaterTransmutation));
            items.Add(new MM2GameInfoItem("Air Trans.", ActiveEffects.AirTransmutation, effects + MM2EffectsOffsets.AirTransmutation));
            items.Add(new MM2GameInfoItem("Fire Trans.", ActiveEffects.FireTransmutation, effects + MM2EffectsOffsets.FireTransmutation));
            items.Add(new MM2GameInfoItem("Earth Trans.", ActiveEffects.EarthTransmutation, effects + MM2EffectsOffsets.EarthTransmutation));
            items.Add(new MM2GameInfoItem("Levitate", ActiveEffects.Levitate, effects + MM2EffectsOffsets.Levitate));
            items.Add(new MM2GameInfoItem("Water Walk", ActiveEffects.WaterWalk, effects + MM2EffectsOffsets.WaterWalk));
            items.Add(new MM2GameInfoItem("Guard Dog", ActiveEffects.GuardDog, effects + MM2EffectsOffsets.GuardDog));
            items.Add(new MM2GameInfoItem("Entrapment", ActiveEffects.Entrapment, effects + MM2EffectsOffsets.Entrapment));
            items.Add(new MM2GameInfoItem("Bless", ActiveEffects.Bless, effects + MM2EffectsOffsets.Bless));
            items.Add(new MM2GameInfoItem("Invisibility", ActiveEffects.Invisibility, effects + MM2EffectsOffsets.Invisibility));
            items.Add(new MM2GameInfoItem("Shield", ActiveEffects.Shield, effects + MM2EffectsOffsets.Shield));
            items.Add(new MM2GameInfoItem("PowerShield", ActiveEffects.PowerShield, effects + MM2EffectsOffsets.PowerShield));
            items.Add(new MM2GameInfoItem("Cursed", (byte)ActiveEffects.Cursed, effects + MM2EffectsOffsets.Cursed));
            items.Add(new MM2GameInfoItem("Eagle Eye", (byte)ActiveEffects.EagleEye, effects + MM2EffectsOffsets.EagleEye));
            items.Add(new MM2GameInfoItem("Wizard Eye", (byte)ActiveEffects.WizardEye, effects + MM2EffectsOffsets.WizardEye));
            return items;
        }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int mapOffset = MM2Memory.MapGlobalAttributes;

            items.Add(new MM2GameInfoItem("Lloyd's Beacon", BeaconAndPoint, MM2Memory.BeaconLocation, DataType.MapAndPoint8));
            items.Add(new MM2GameInfoItem("World Explored", Exploration, 0, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
            items.Add(new MM2GameInfoItem("Used Benefits 1", BenefitsUsed1, MM2Memory.BenefitsUsed1));
            items.Add(new MM2GameInfoItem("Used Benefits 2", BenefitsUsed2, MM2Memory.BenefitsUsed2));
            if (MapAttributes.Exits != null && MapAttributes.Exits.Count > 5)
            {
                items.Add(new MM2GameInfoItem("Surface Map", MapAttributes.Exits[4].MM2MapAndPoint,
                    new OffsetList(mapOffset + MM2MapAttributeOffsets.SurfaceMap, mapOffset + MM2MapAttributeOffsets.SurfaceCoord), DataType.MM2MapAndPoint8));
                items.Add(new MM2GameInfoItem("North Map", (byte)MapAttributes.Exits[0].Map, mapOffset + MM2MapAttributeOffsets.ExitNorth, DataType.Map8));
                items.Add(new MM2GameInfoItem("East Map", (byte)MapAttributes.Exits[1].Map, mapOffset + MM2MapAttributeOffsets.ExitEast, DataType.Map8));
                items.Add(new MM2GameInfoItem("South Map", (byte)MapAttributes.Exits[2].Map, mapOffset + MM2MapAttributeOffsets.ExitSouth, DataType.Map8));
                items.Add(new MM2GameInfoItem("West Map", (byte)MapAttributes.Exits[3].Map, mapOffset + MM2MapAttributeOffsets.ExitWest, DataType.Map8));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            const int mapOffset = MM2Memory.MapGlobalAttributes;

            for (int i = 0; i < 9; i++)
                items.Add(new MM2GameInfoItem(String.Format("Era {0}", i + 1), new ushort[] { Time.EraYears[i], Time.EraDays[i] },
                    new OffsetList(MM2Memory.DatesAndTimes + 20 + i * 2, MM2Memory.DatesAndTimes + i * 2)));
            items.Add(new MM2GameInfoItem("Current Era", Time.CurrentEra, MM2Memory.CurrentEra));
            items.Add(new MM2GameInfoItem("Default Era", (byte)MapAttributes.DefaultEra, mapOffset + MM2MapAttributeOffsets.DefaultEra));
            items.Add(new MM2GameInfoItem("Steps", Time.Steps, MM2Memory.StepCounter));
            items.Add(new MM2GameInfoItem("Battles Won", BattlesWon, MM2Memory.BattleStats));
            items.Add(new MM2GameInfoItem("Battles Lost", BattlesLost, MM2Memory.BattleStats + 2));
            items.Add(new MM2GameInfoItem("Outside", Location.Outside, MM2Memory.IsOutside, DataType.Boolean, 0, GameInfoItem.ShowStyle.Visible));
            items.Add(new MM2GameInfoItem("Dark", (byte)MapAttributes.ForbiddenSpells, mapOffset + MM2MapAttributeOffsets.ForbiddenSpells, DataType.Boolean, 0x80));
            items.Add(new MM2GameInfoItem("Map Index", MapAttributes.Index, MM2Memory.CurrentMapIndex, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
            items.Add(new MM2GameInfoItem("Run Square", (byte)ByteFromMapLocation(MapAttributes.SafestSquare), mapOffset + MM2MapAttributeOffsets.SafestSquare, DataType.Point8));
            items.Add(new MM2GameInfoItem("Enc. Size", (byte)MapAttributes.EncounterSize, mapOffset + MM2MapAttributeOffsets.EncounterSize));
            items.Add(new MM2GameInfoItem("Monster Group", (byte)MapAttributes.MonsterGroup, mapOffset + MM2MapAttributeOffsets.MonsterGroup));
            items.Add(new MM2GameInfoItem("Depth", (byte)MapAttributes.UndergroundLevel, mapOffset + MM2MapAttributeOffsets.UndergroundLevel, DataType.Depth));
            items.Add(new MM2GameInfoItem("Met Gwyndon", Gwyndon, MM2Memory.Gwyndon, DataType.Boolean));
            return items;
        }

    }

    public class MM2MapCartography : MapCartography
    {
        public override bool IsVisited(int x, int y)
        {
            return (Global.GetBit(Bytes, y * 16 + (x ^ 0x08) ) > 0);
        }
    }

    public class MM2EncounterInfo : EncounterInfo
    {
        public byte PartyMelee;     // Number of characters in melee range
        public byte[] MonsterMoved; // 10 bytes
        public byte[] PartyMoved;   // 8 bytes
        public byte[] MonsterHP;    // 22 bytes
        public bool Entrapped;
        public byte[] MonsterStatus;    // 11 bytes
        public byte[] EncounterList;    // 11 bytes
        public byte[] RawMonsterData;   // 0x1a00 bytes
        public Dictionary<int, Monster> m_monsters;
        public MMMonster MonsterReserves;

        public MM2GameState GameState;

        public MM2EncounterInfo()
            : base()
        {
            m_monsters = null;
            MonsterReserves = null;
            RawMonsterData = null;
        }

        public void CreateMonsterList()
        {
            m_monsters = new Dictionary<int, Monster>(NumTotalMonsters);
            for (int i = 0; i < Math.Min(NumTotalMonsters, 10); i++)
            {
                MM2Monster monster = null;
                if (GameState.Main == MainState.PreCombat)
                    monster = MM2Monster.Create(EncounterList[i], RawMonsterData);
                else
                    monster = MM2Monster.Create(EncounterList[i], BitConverter.ToUInt16(MonsterHP, i * 2), (MM2MonsterCombatStatus)MonsterStatus[i], RawMonsterData);
                monster.EncounterIndex = i;
                m_monsters.Add(i, monster);
            }
            if (NumTotalMonsters > 10)
            {
                MonsterReserves = MM2Monster.Create(EncounterList[10], BitConverter.ToUInt16(MonsterHP, 20), MM2MonsterCombatStatus.Good, RawMonsterData);
                MonsterReserves.EncounterIndex = 10;
            }
            else
                MonsterReserves = null;
        }

        public override bool HasTreasure { get { return SearchResults != null && !SearchResults.IsEmpty; } }
        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } }
        public override bool InCombat { get { return GameState.InCombat; } }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);

            MM2Character[] characters = new MM2Character[Party.NumChars];

            int iNameMax = 0;

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = MM2Character.Create(Party.Bytes, Party.Addresses[iIndex] * Party.CharacterSize);
                labelChars[iIndex].Melee = (PartyMelee > iIndex);
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Current.ToString();
                labelChars[iIndex].SP = characters[iIndex].SpellPoints.Current.ToString();
                if (PartyMoved[iIndex] != 1 || PreEncounter || Party.PositionOfAddress(Party.ActingChar) == iIndex)
                    toc.AddPlayerCharacter(characters[iIndex].CharName, characters[iIndex].Speed.Temporary, iIndex);

                iNameMax = Math.Max(iNameMax, labelChars[iIndex].NameLength);
            }

            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
                labelChars[iIndex].SetHPOffset(iNameMax + 2);

            for (byte iIndex = 0; iIndex < Monsters.Count; iIndex++)
            {
                if (MonsterMoved[iIndex] != 1 || PreEncounter)
                    toc.AddMonster(Monsters[iIndex].ProperName, Monsters[iIndex].Speed, iIndex + 6);
            }

            return toc;
        }

        public override string ExtraText
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Entrapped)
                    sb.AppendLine("  Entrapped (monsters cannot flee combat)");

                if (sb.Length > 0)
                    return ("Active Effects:\n" + sb.ToString());

                return "Active Effects: None";
            }
        }
    }

    public class MM2PartyInfo : PartyInfo
    {
        public MM2GameState GameState;
        public MM2HirelingFlags Hirelings;
        public bool VisitedPegasus;
        public MM2DonationFlags Donations;

        public override int CharacterSize { get { return MM2Character.SizeInBytes; } }

        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null || Bytes.Length < (CharacterSize * 48))
                    return null;
                MemoryStream stream = new MemoryStream(6);
                Global.WriteInt32(stream, (int)Hirelings);
                stream.WriteByte((byte) (VisitedPegasus ? 1 : 0));
                stream.WriteByte((byte)Donations);
                for(int i = 0; i < 48; i++)
                {
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Might]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Intellect]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Personality]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Endurance]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Speed]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Accuracy]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Luck]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.MightMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.IntellectMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.PersonalityMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.EnduranceMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.SpeedMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.AccuracyMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.LuckMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.LevelMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.SpellLevelMod]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM2.Offsets.Thievery]);
                    Global.Write(stream, Addresses);
                    stream.Write(Bytes, i * CharacterSize + MM2.Offsets.MaxHP, 2);
                    stream.Write(Bytes, i * CharacterSize + MM2.Offsets.Skills, 7);    // Secondary skills (for Crusader) and Spell Book
                    stream.Write(Bytes, i * CharacterSize + MM2.Offsets.Awards, MM2.Offsets.AwardsLength);  // Quest bytes
                    stream.Write(Bytes, i * CharacterSize + MM2.Offsets.EquippedBases, 6);  // Equipped items
                    stream.Write(Bytes, i * CharacterSize + MM2.Offsets.BackpackBases, 6);  // Backpack items
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public MM2PartyInfo(byte[] bytes, byte numchars, int actingAddress, int[] addresses, byte[] positions, MM2GameState state)
        {
            Bytes = bytes;
            NumChars = numchars;
            GameState = state;
            List<int> pos = new List<int>(numchars);

            if (positions == null)
                positions = new byte[0];
            if (addresses == null)
                addresses = new int[0];

            Positions = positions;
            Addresses = addresses;

            ActingChar = (byte)(actingAddress < 49 ? actingAddress : 0);
        }

        public override bool InCombatOrStore
        {
            get
            {
                return (GameState.InCombat ||
                    GameState.Main == MainState.Shop ||
                    GameState.Main == MainState.Tavern ||
                    GameState.Main == MainState.Temple ||
                    GameState.Main == MainState.Training ||
                    GameState.Main == MainState.TrainingNoExp ||
                    GameState.Main == MainState.TrainingNoGold ||
                    GameState.Main == MainState.TrainSuccess
                    );
            }
        }

        public bool CurrentPartyHasItem(MM2ItemIndex item)
        {
            for(int i = 0; i < Addresses.Length; i++)
            {
                if (Bytes.Length < (Addresses[i] + 1) * CharacterSize)
                    continue;

                MM2Inventory inventory = new MM2Inventory(Bytes, Addresses[i] * CharacterSize + 40);
                if (inventory.HasItem(item))
                    return true;
            }
            return false;
        }

        public bool AnyCharIsHireling { get { return Addresses.Any(b => b > 23); } }

        public bool StatAtLeast(int iCharAddress, PrimaryStat stat, int iMinimum)
        {
            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            switch (stat)
            {
                case PrimaryStat.Might: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.MightMod] >= iMinimum;
                case PrimaryStat.Intellect: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.IntellectMod] >= iMinimum;
                case PrimaryStat.Personality: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.PersonalityMod] >= iMinimum;
                case PrimaryStat.Endurance: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.EnduranceMod] >= iMinimum;
                case PrimaryStat.Speed: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.SpeedMod] >= iMinimum;
                case PrimaryStat.Accuracy: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.AccuracyMod] >= iMinimum;
                case PrimaryStat.Luck: return Bytes[iCharAddress * CharacterSize + MM2.Offsets.LuckMod] >= iMinimum;
                default: return false;
            }
        }

        public bool CurrentPartyHasSkill(MM2SecondarySkill skill)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (Bytes.Length < (Addresses[i] + 1) * CharacterSize)
                    continue;

                if (skill == (MM2SecondarySkill)(Bytes[Addresses[i] * CharacterSize + 80] & 0x0f))
                    return true;
                if (skill == (MM2SecondarySkill)((Bytes[Addresses[i] * CharacterSize + 80] & 0xf0) >> 4))
                    return true;
            }
            return false;
        }
    }

    public class MM2RawStateData
    {
        public byte[] MainState;
        public byte CombatWaiting;
        public byte CastingSpell;
        public byte CastingState;
        public byte CastingState2;
        public byte Command;
    }

    public class MM2GameState : GameState
    {
        public override GameNames Game { get { return GameNames.MightAndMagic2; } }
        public byte[] CharPositions;
        public int[] CharAddresses;

        public MM2RawStateData Raw;

        public override bool Casting
        {
            get
            {
                return (Main == MainState.CastLevel || Main == MainState.CastNumber);
            }
        }

        public override bool ActingIsCaster { get { return true; } }
    }

    public class MM2CureAllInfo : CureAllInfo
    {
        public MM2Condition[] Conditions;   // 8 bytes; one per character
        public MM2Condition CasterCondition;
        public MM2GameState GameState;
        public UInt16[] HitPoints;          // 16 bytes; two per character
        public UInt16[] HitPointsMax;       // 16 bytes; two per character
        public byte CasterSpellLevel;
        public UInt16 CasterSpellPoints;
        public UInt16 CasterGems;
        public MM2KnownSpells CasterKnownSpells;
        public MM2Class CasterClass;
        public bool InCombat;
        public bool AntiMagicZone;

        public MM2CureAllInfo()
        {
        }

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsHealer { get { return CasterClass == MM2Class.Cleric || CasterClass == MM2Class.Paladin; } }
        public override bool IsIncapacitated { get { return ((((byte)CasterCondition) & (byte)MM2Condition.UnableToCast) > 0); } }
        public override bool MagicPermitted { get { return !AntiMagicZone; } }
        public override bool Combat { get { return InCombat; } }
    }

    public class MM2TrainingInfo : TrainingInfo
    {
        public MM2GameState State;
        public MM2Map Map;

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

    public enum MM2Map
    {
        C2Middlegate = 0,
        A4Atlantium = 1,
        A1Tundara = 2,
        E1Vulcania = 3,
        E4Sandsobar = 4,
        A1Surface = 5,
        B1Surface = 6,
        C1Surface = 7,
        D1Surface = 8,
        A2Surface = 9,
        B2Surface = 10,
        C2Surface = 11,
        A3Surface = 12,
        B3Surface = 13,
        C3Surface = 14,
        A4Surface = 15,
        B4Surface = 16,
        C2MiddlegateDungeon = 17,
        A4AtlantiumDungeon = 18,
        A1TundaraDungeon = 19,
        E1VulcaniaDungeon = 20,
        E4SandsobarDungeon = 21,
        C2CoraksCave = 22,
        C2SquareLakeCave = 23,
        B1IceCavern = 24,
        A2SarakinsMine = 25,
        B4MurraysCave = 26,
        C3DruidsCave = 27,
        C3ForbiddenForestCavern = 28,
        E1DragonsDominion = 29,
        D4DawnsCavern = 30,
        E1GemmakersCave = 31,
        E3NomadicRiftCave = 32,
        E1Surface = 33,
        D2Surface = 34,
        E2Surface = 35,
        D3Surface = 36,
        E3Surface = 37,
        C4Surface = 38,
        D4Surface = 39,
        E4Surface = 40,
        PlaneOfAir = 41,
        PlaneOfWater = 44,
        PlaneOfEarth = 43,
        PlaneOfFire = 42,
        D4CastleHillstoneB1 = 45,
        D4CastleHillstoneB2 = 46,
        C1CastleWoodhavenB1 = 47,
        C1CastleWoodhavenB2 = 48,
        A2CastlePinehurstB1 = 49,
        A2CastlePinehurstB2 = 50,
        D2GreatLuxusPalaceRoyaleB1 = 51,
        D2GreatLuxusPalaceRoyaleB2 = 52,
        B3CastleOfEvil = 53,
        B4CastleOfGood = 54,
        D4CastleHillstone = 55,
        C1CastleWoodhaven = 56,
        A2CastlePinehurst = 57,
        D2GreatLuxusPalaceRoyale = 58,
        C2CastleXabran = 59,

        Unknown = -1
    }

    public class MM2MapAttributes : MapAttributes
    {
        public MM2MapAttributes()
        {
        }

        public override bool IsOutside(Point pt)
        {
            if (pt.X > 15 || pt.Y > 15)
                return false;

            // Bytes 32-63 are a bitmap of whether a square is outside
            // Bit 0 = (0,0), Bit 1 = (1,0) ...
            int iByte = (pt.Y * 2) + (pt.X / 8);
            if (32 + iByte >= Bytes.Length || 32 + iByte < 0)
                return false;

            byte bTest = Bytes[32 + iByte];
            int iBit = pt.X % 8;
            return (((bTest >> iBit) & 1) == 0);
        }

        public override bool SetOutside(Point pt, bool bOutside)
        {
            if (pt.X > 15 || pt.Y > 15)
                return false;

            int iByte = (pt.Y * 2) + (pt.X / 8);
            byte bTest = Bytes[32 + iByte];
            int iBit = pt.X % 8;
            if (!bOutside)
                bTest |= (byte) (1 << iBit);
            else
                bTest &= (byte) ~(1 << iBit);
            Bytes[32 + iByte] = bTest;
            return true;
        }
    }

    [Flags]
    public enum MM2DonationFlags
    {
        Middlegate = 0x01,
        Atlantium = 0x02,
        Tundara = 0x04,
        Vulcania = 0x08,
        Sandsobar = 0x10,
        AllTowns = Middlegate | Atlantium | Tundara | Vulcania | Sandsobar,
    }

    [Flags]
    public enum MM2HirelingFlags
    {
        SirHyron =   0x000001,
        Drog =       0x000002,
        HKPhooey =   0x000004,
        ThundR =     0x000008,
        Aeriel =     0x000010,
        BigBootay =  0x000020,
        Cleogotcha = 0x000040,
        HarryKari =  0x000080,
        NoName =     0x000100,
        Gertrude =   0x000200,
        RatFink =    0x000400,
        FriarFly =   0x000800,
        DarkMage =   0x001000,
        RedDuke =    0x002000,
        DeadEye =    0x004000,
        Nakazawa =   0x008000,
        Sherman =    0x010000,
        Flailer =    0x020000,
        Fumbler =    0x040000,
        SirKill =    0x080000,
        JedI =       0x100000,
        HolyMoley =  0x200000,
        SlickPick =  0x400000,
        MrWizard =   0x800000,    
    }

    public static class MM2EffectsOffsets
    {
        public const int LightFactors = 0;
        public const int ProtMagic = 1;
        public const int ProtForces = 2;
        public const int Levitate = 3;
        public const int WaterWalk = 4;
        public const int GuardDog = 5;
        public const int Cursed = 6;
        public const int WaterTransmutation = 7;
        public const int AirTransmutation = 8;
        public const int FireTransmutation = 9;
        public const int EarthTransmutation = 10;
        public const int EagleEye = 11;
        public const int WizardEye = 12;
        public const int Bless = 14;
        public const int Invisibility = 15;
        public const int Shield = 16;
        public const int PowerShield = 17;
        public const int HolyBonus = 18;
        public const int Entrapment = 83;
    }

    public static class MM2MapAttributeOffsets
    {
        public const int ExitNorth = 5;
        public const int ExitEast = 6;
        public const int ExitSouth = 7;
        public const int ExitWest = 8;
        public const int EncounterSize = 10;
        public const int MonsterGroup = 11;
        public const int SafestSquare = 14;
        public const int DefaultEra = 15;
        public const int SurfaceCoord = 22;
        public const int UndergroundLevel = 23;
        public const int SurfaceMap = 24;
        public const int ForbiddenSpells = 26;
        public const int OutdoorFlags = 32;
    }

    public class MM2ActiveSquares : ActiveSquares
    {
        public MM2ActiveSquares(MainForm main, int mapIndex, byte[] mapAttributes)
        {
            Main = main;
            m_iMapIndex = mapIndex;
            RawBytes = mapAttributes;
            m_bInitialized = false;
        }
    }

    public class MM2String : MMString
    {
        public override void SetFromBytes(byte[] bytes, ref int iNext)
        {
            // MM2 strings are 0xff-terminated, and '@' is used as a newline character
            Init();
            StringBuilder sb = new StringBuilder();

            int iStart = iNext;

            if (iStart >= bytes.Length)
                return;

            if (bytes[iStart] == 0xff)
            {
                iNext++;
                return;
            }

            while (iNext < bytes.Length - 1 && bytes[iNext] != 0xff)
            {
                switch (bytes[iNext])
                {
                    case (byte) '@':
                        iNext++;
                        sb.Append(" ");
                        break;
                    default:
                        sb.Append((char)bytes[iNext]);
                        iNext++;
                        break;
                }
            }

            iNext++;
            RawBytes = new byte[iNext - iStart];
            Buffer.BlockCopy(bytes, iStart, RawBytes, 0, iNext - iStart);
            Basic = sb.ToString();
        }
    }



    public class MM2MemoryHacker : MMMemoryHacker
    {
        public MM2MemoryHacker()
        {
            m_game = GameNames.MightAndMagic2;
        }

        public override byte[] MainSearch { get { return MM2Memory.MainSearch; } }
        public override string SpellType3 { get { return "Sorcerer"; } }
        public override MemoryGuess[] Guesses { get { return MM2Memory.Guesses; } }

        public override int[] StatMinimums(GenericClass charClass)
        {
            switch (charClass)
            {
                case GenericClass.Knight: return new int[] { 0, 15, 0, 0, 0, 0, 0 };
                case GenericClass.Paladin: return new int[] { 0, 13, 13, 13, 0, 0, 0 };
                case GenericClass.Archer: return new int[] { 13, 0, 0, 0, 0, 13, 0 };   
                case GenericClass.Sorcerer: return new int[] { 13, 0, 0, 0, 0, 0, 0 };
                case GenericClass.Cleric: return new int[] { 0, 0, 13, 0, 0, 0, 0 };
                case GenericClass.Robber: return new int[] { 0, 0, 0, 0, 0, 0, 13 };
                case GenericClass.Ninja: return new int[] { 0, 0, 0, 0, 13, 13, 0 };
                case GenericClass.Barbarian: return new int[] { 0, 0, 0, 15, 0, 0, 0 };
                default: return null;
            }
        }

        public override int GetLightDistance(Point ptLocation)
        {
            if (ReadByte(MM2Memory.ActiveEffects + MM2EffectsOffsets.LightFactors) > 0)
                return 4;
            if ((ReadByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ForbiddenSpells) & 0x80) == 0)
                return 4;
            return 0;
        }

        private MM2MapInfo ReadMM2MapInfo()
        {
            MemoryBytes bytes = ReadOffset(MM2Memory.MapAppearance, 512);
            if (bytes == null)
                return null;
            return new MM2MapInfo(bytes);
        }

        private int GetCharIndexFromAddresses(int iAddress, int[] partyAddresses)
        {
            for (int i = 0; i < partyAddresses.Length; i++)
                if (partyAddresses[i] == iAddress)
                    return i;
            return -1;
        }

        public override CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            // In Might and Magic 2, the caster index and the caster address are the same
            if (iCasterIndex >= 24)   // Roster index, so 0-23
                return null;
            int iCasterAddress = iCasterIndex;
            iCasterIndex = GetCharIndexFromAddresses(iCasterAddress, partyAddresses);
            if (iCasterIndex == -1)
                return null;

            MM2CureAllInfo info = new MM2CureAllInfo();
            MM2PartyInfo party = ReadMM2PartyInfo();

            byte[] temp = new byte[2];
            IntPtr pRead = IntPtr.Zero;

            info.GameState = ReadMM2GameState();
            info.InCombat = info.GameState.InCombat;

            ReadOffset(MM2Memory.Location, temp);
            MM2MapInfo map = ReadMM2MapInfo();
            info.AntiMagicZone = map.Behavior[temp[0], temp[1]].HasFlag(MM2MapBehavior.AntiMagic);

            info.Conditions = new MM2Condition[partyAddresses.Length];
            info.HitPoints = new UInt16[partyAddresses.Length];
            info.HitPointsMax = new UInt16[partyAddresses.Length];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                info.Conditions[i] = (MM2Condition)party.Bytes[partyAddresses[i] * party.CharacterSize + MM2.Offsets.Condition];
                info.HitPoints[i] = BitConverter.ToUInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + MM2.Offsets.CurrentHP);
                info.HitPointsMax[i] = BitConverter.ToUInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + MM2.Offsets.MaxHPMod);
            }

            info.CasterGems = BitConverter.ToUInt16(party.Bytes, iCasterAddress * party.CharacterSize + MM2.Offsets.Gems);
            info.CasterSpellPoints = BitConverter.ToUInt16(party.Bytes, iCasterAddress * party.CharacterSize + MM2.Offsets.CurrentSP);
            info.CasterSpellLevel = party.Bytes[iCasterAddress * party.CharacterSize + MM2.Offsets.SpellLevelMod];
            info.CasterKnownSpells = new MM2KnownSpells(party.Bytes, iCasterAddress * party.CharacterSize + MM2.Offsets.Spells);
            info.CasterClass = (MM2Class)party.Bytes[iCasterAddress * party.CharacterSize + MM2.Offsets.Class];
            info.CasterCondition = info.Conditions[iCasterIndex];
            return info;
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bInsufficientLevel = false;
            bool bInsufficientSP = false;
            bool bInsufficientGems = false;
            bool bUnknownSpell = false;

            if (!(cureAllInfo is MM2CureAllInfo))
                return CureAllResult.Error;

            MM2CureAllInfo info = cureAllInfo as MM2CureAllInfo;

            // Might and Magic 2 doesn't have specific spells for curing anything except
            // Poison, Disease, Death, Stone, and Eradication.  Curing Paralysis, Curse, and Silence
            // requires casting C5-5 (Remove Condition) for 5+5G, and resting fixes those conditions
            // as well, so we won't bother with those here.

            // Okay, let's start curing!
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                // First skip Dead/Stone/Eradicated (these conditions are more complicated in MM2 than
                // "Cure All" should fix)
                if (info.Conditions[i].HasFlag(MM2Condition.SevereFlag))
                    continue;

                if (info.Conditions[i].HasFlag(MM2Condition.Poisoned))
                {
                    if (info.CasterKnownSpells.Spells[3, 3])
                    {
                        if (info.CasterSpellPoints >= 3)
                        {
                            if (info.CasterSpellLevel >= 3)
                            {
                                info.CasterSpellPoints -= 3;
                                info.Conditions[i] = info.Conditions[i] & ~MM2Condition.Poisoned;
                            }
                            else
                                bInsufficientLevel = true;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bUnknownSpell = true;
                }
                if (info.Conditions[i].HasFlag(MM2Condition.Diseased))
                {
                    if (info.CasterKnownSpells.Spells[4, 3])
                    {
                        if (info.CasterSpellPoints >= 4)
                        {
                            if (info.CasterSpellLevel >= 4)
                            {
                                info.CasterSpellPoints -= 4;
                                info.Conditions[i] = info.Conditions[i] & ~MM2Condition.Diseased;
                            }
                            else
                                bInsufficientLevel = true;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bUnknownSpell = true;
                }
            }
            bool bAnySleep = false;
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i].HasFlag(MM2Condition.Asleep))
                {
                    if (info.CasterKnownSpells.Spells[1, 2])
                    {
                        if (info.CasterSpellPoints >= 1)
                        {
                            if (info.CasterSpellLevel >= 1)
                            {
                                bAnySleep = true;
                                info.Conditions[i] = info.Conditions[i] & ~MM2Condition.Asleep;
                            }
                            else
                                bInsufficientLevel = true;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bUnknownSpell = true;
                }
            }
            if (bAnySleep && info.CasterSpellPoints > 0)
                info.CasterSpellPoints -= 1;

            // Restore HP with any remaining spell points
            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.CasterKnownSpells.Spells[1, 4])
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
                                        info.Conditions[i] = info.Conditions[i] & (~MM2Condition.Unconscious);
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
                    else
                        bUnknownSpell = true;
                }
            }

            if (bUnknownSpell)
                return CureAllResult.SpellNotKnown;
            if (bInsufficientLevel)
                return CureAllResult.NoSpellLevel;
            if (bInsufficientSP)
                return CureAllResult.NoSpellPoints;
            if (bInsufficientGems)
                return CureAllResult.NoGems;
            return CureAllResult.Success;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterIndex >= 24)   // Roster index, so 0-23
                return;
            int iCasterAddress = iCasterIndex;
            iCasterIndex = GetCharIndexFromAddresses(iCasterAddress, partyAddresses);
            if (iCasterIndex == -1)
                return;

            MM2CureAllInfo info = cureAll as MM2CureAllInfo;
            long pWritten;
            byte[] temp = BitConverter.GetBytes(info.CasterGems);
            WriteOffset(MM2Memory.PartyInfo + (iCasterAddress * MM2Character.SizeInBytes + 92), temp);
            temp = BitConverter.GetBytes(info.CasterSpellPoints);
            WriteOffset(MM2Memory.PartyInfo + (iCasterAddress * MM2Character.SizeInBytes + 88), temp);
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                temp[0] = (byte)info.Conditions[i];
                WriteOffset(MM2Memory.PartyInfo + (partyAddresses[i] * MM2Character.SizeInBytes + 38), temp, 1, out pWritten);
                temp = BitConverter.GetBytes(info.HitPoints[i]);
                WriteOffset(MM2Memory.PartyInfo + (partyAddresses[i] * MM2Character.SizeInBytes + 94), temp, 2, out pWritten);
            }
        }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            MM2SpellInfo info = new MM2SpellInfo();
            byte[] temp = new byte[2];
            IntPtr pRead = IntPtr.Zero;
            info.Game = ReadMM2GameState();
            if (info.Game.Main == MainState.SignIn)
                return info;

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = ReadMM2PartyInfo();

            if (info.Party == null)
                return null;

            info.Game.ActingCharAddress = info.Party.ActingChar;
            return info;
        }

        private int GetActingAddress(MM2GameState state)
        {
            byte[] actingChar = new byte[2];
            int actingAddress = 0;
            if (state.Main == MainState.CharacterScreen || state.Main == MainState.QuickRef || state.Casting)
            {
                if (state.Casting && state.InCombat)
                {
                    ReadOffset(MM2Memory.CombatCastingChar3, actingChar);
                    if (actingChar[0] >= 0 && actingChar[0] < state.CharAddresses.Length)
                        return state.CharAddresses[actingChar[0]];
                    return actingChar[0];
                }
                else
                {
                    ReadOffset(MM2Memory.CharacterScreenOffset, actingChar);
                    if (((actingChar[1] << 8) | actingChar[0]) == MM2Memory.QuickRefInternal)
                        return 0;
                    else
                        actingAddress = (byte)((BitConverter.ToUInt16(actingChar, 0) - MM2Memory.FirstCharacterInternal) / MM2Character.SizeInBytes);
                    if (actingAddress > 47)
                        return 0;
                }
            }
            else
            {
                byte bActing = 0;
                switch (state.Main)
                {
                    case MainState.Shop:
                        bActing = ReadByte(MM2Memory.ActingCharShop);
                        break;
                    case MainState.Temple:
                        bActing = ReadByte(MM2Memory.ActingCharTemple);
                        break;
                    case MainState.Training:
                    case MainState.TrainingNoExp:
                    case MainState.TrainingNoGold:
                    case MainState.TrainSuccess:
                    case MainState.Tavern:
                    case MainState.TavernPurchased:
                    case MainState.TavernRumors:
                    case MainState.MageGuild:
                        bActing = ReadByte(MM2Memory.ActingCharTavernTrain);
                        break;
                    case MainState.TavernHaveADrink:
                    case MainState.TavernSpecialties:
                        bActing = ReadByte(MM2Memory.ActingCharSpecialties);
                        break;
                    default:
                        bActing = state.InCombat ? (byte)state.ActingCharAddress : (byte)0;
                        break;
                }
                return bActing;
            }
            return actingAddress;
        }

        private void GetCharAddressesAndPositions(out int[] addresses, out byte[] positions)
        {
            int iNumChars = ReadByte(MM2Memory.NumChars1);
            if (iNumChars > 8)
                iNumChars = 8;
            addresses = new int[iNumChars];
            positions = Global.NullBytes(48);   // All characters and hirelings
            MemoryBytes mbPos = ReadOffset(MM2Memory.CharacterOrder, iNumChars * 2);
            for (byte i = 0; i < iNumChars; i++)
            {
                addresses[i] = mbPos.Bytes[i * 2];
                if (addresses[i] >= 0 && addresses[i] < positions.Length)
                    positions[addresses[i]] = i;
            }
        }

        private MM2PartyInfo ReadMM2PartyInfo()
        {
            byte actingAddress = 0;
            if (m_block == null)
                return null;

            // Might and Magic 2 stores the entire roster in memory and accesses it via the position information

            MM2GameState state = ReadMM2GameState();
            if (state.Main == MainState.Opening || state.Main == MainState.MainMenu || state.Main == MainState.Options)
                return new MM2PartyInfo(new byte[0], 0, 0, null, null, state);

            byte numChars = ReadByte(MM2Memory.NumChars1);
            if (numChars == 0)
                return new MM2PartyInfo(new byte[0], 0, 0, null, null, state);
            if (numChars < 9)
            {
                MemoryBytes bytes = ReadOffset(MM2Memory.PartyInfo, MM2Character.SizeInBytes * 24 * 2); // 24 chars, 24 hirelings
                if (bytes == null)
                    return null;
                actingAddress = (byte) GetActingAddress(state);

                MM2PartyInfo info = new MM2PartyInfo(bytes, numChars, actingAddress, state.CharAddresses, state.CharPositions, state);
                MemoryBytes hirelings = ReadOffset(MM2Memory.HirelingsRescued, 24);
                if (hirelings == null)
                    return null;

                int iHirelings = 0;
                for (int i = 0; i < 24; i++)
                {
                    if (hirelings[i] == 1)
                        iHirelings |= (1 << i);
                }

                info.Hirelings = (MM2HirelingFlags) iHirelings;
                info.VisitedPegasus = ReadByte(MM2Memory.VisitedPegasus) == 1;
                info.Donations = (MM2DonationFlags)ReadByte(MM2Memory.Donations);

                return info;
            }
            return null;
        }

        public override String GetGameTime(bool bFull)
        {
            if (!IsValid)
                return String.Empty;

            byte bSteps = ReadByte(MM2Memory.StepCounter);
            // Each step is 5.625 minutes, but we round it to more-or-less the nearest 5, making sure not to skip :15, :30, and :45

            int iMinutes = 0;
            int iDay, iYear;

            switch(bSteps & 0x0f)
            {
                case 0: iMinutes = 0; break;
                case 1: iMinutes = 5; break;
                case 2: iMinutes = 10; break;
                case 3: iMinutes = 15; break;
                case 4: iMinutes = 25; break;
                case 5: iMinutes = 30; break;
                case 6: iMinutes = 35; break;
                case 7: iMinutes = 40; break;
                case 8: iMinutes = 45; break;
                case 9: iMinutes = 50; break;
                case 10: iMinutes = 55; break;
                case 11: iMinutes = 60; break;
                case 12: iMinutes = 65; break;
                case 13: iMinutes = 70; break;
                case 14: iMinutes = 75; break;
                case 15: iMinutes = 85; break;
                default: iMinutes = 0; break;
            }

            iMinutes += ((bSteps >> 4) * 90);
            bool bPM = false;

            MM2TimeInfo time = ReadMM2TimeInfo();
            if (time != null && time.CurrentEra < 10 && time.CurrentEra > 0)
            {
                iDay = time.EraDays[time.CurrentEra - 1];
                iYear = time.EraYears[time.CurrentEra - 1];
            }
            else
            {
                iDay = ReadByte(MM2Memory.CurrentDay);
                iYear = ReadUInt16(MM2Memory.CurrentYear);
            }

            int iHours = iMinutes / 60;
            iMinutes = iMinutes % 60;

            // minute "0" changes from night to day, and minute 720 vice-versa, so we'll
            // set those to 6:00 AM and 6:00 PM

            iHours += 6;
            if (iHours > 23)
                iHours -= 24;

            if (iHours >= 12)
            {
                iHours -= 12;
                bPM = true;
            }

            if (iHours == 0)
                iHours = 12;   // For display; 0:00 -> 12:00

            if (bFull)
                return String.Format("{0}:{1:D2} {2}, day {3} of year {4}", iHours, iMinutes, bPM ? "PM" : "AM", iDay, iYear);
            else
                return String.Format("{0}:{1:D2} {2}", iHours, iMinutes, bPM ? "PM" : "AM");
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadMM2PartyInfo();
        }

        private MainState GetTavernState(UInt16 state2)
        {
            switch (state2)
            {
                case 0xca42: return MainState.TavernPurchased;
                case 0xcc0c: return MainState.TavernHaveADrink;
                case 0xce4f: return MainState.TavernSpecialties;
                case 0xd0b4: return MainState.TavernRumors;
                default: return MainState.Tavern;
            }
        }

        private MainState GetMainState(UInt16 state1, UInt16 state2)
        {
            switch(state1)
            {
                case 0xcc04: return MainState.Options;
                case 0xc2ea: return MainState.Opening2;
                case 0xC265: return MainState.Opening;
                case 0xC903: return MainState.About;
                case 0xC849: return MainState.CopyDisk;
                case 0x9868: return MainState.Transfer;
                case 0x97f3: return MainState.EndGame;
                case 0x995f:
                case 0x7f8d:
                case 0x978d:
                case 0x9901: return MainState.Combat;
                case 0x802a: return MainState.ViewAll;
                case 0x8158: return MainState.ViewChar;
                case 0x7e51: return MainState.RenameChar;
                case 0x7f24: return MainState.DeleteChar;
                case 0x7f28: return MainState.DeleteChar;
                case 0x8f5f:
                case 0x8f63:
                case 0x855f:
                case 0x8563: return MainState.CreateSelectClass;
                case 0x8c16: return MainState.CreateSelectRace;
                case 0x8d14: return MainState.CreateSelectAlignment;
                case 0x8dd3: return MainState.CreateSelectSex;
                case 0x8e91: return MainState.CreateSelectName;
                case 0x8493: return MainState.GoToTown;
                case 0x82aa:
                case 0x82f0:
                case 0x7e41: return MainState.Adventuring;
                case 0x7e30:
                case 0x9809:
                case 0x9701:
                case 0x9763: return MainState.SignIn;
                case 0x821f: return MainState.CharacterScreen;
                //case 0x821f: return MainState.QuickRef;  If character screen is accessed first, 81d1 isn't used
                case 0x81d1: return MainState.QuickRef;
                case 0x8172: return MainState.Controls;
                case 0x8179: return MainState.Dismiss;
                case 0x8182: return MainState.Exchange;
                case 0x81d7: return MainState.Rest;
                case 0x9702:
                case 0x979f:
                case 0xa9d0: return MainState.Combat;
                case 0x9781: return MainState.Shop;
                case 0x976f: return GetTavernState(state2);
                case 0xbdf1: return MainState.Map;
                case 0x9775: return MainState.Temple;
                case 0x9769: return MainState.Training;
                case 0x977b: return MainState.MageGuild;
                case 0x88d3:
                case 0x88ea:
                case 0x8915:
                case 0x8933:
                case 0x893f:
                case 0x905d:
                case 0x9070:
                case 0x9079:
                case 0x90aa:
                case 0x90c9:
                case 0x90e7:
                case 0x9101:
                case 0x910b:
                case 0x8a26: return MainState.Rolling;
                case 0x882e:
                case 0x8836:
                case 0x885c:
                case 0x8866:
                case 0x886e:
                case 0x8892:
                case 0x8a9f:
                case 0x8ab4: return MainState.CreateExchangeStat;
                case 0x0000: return MainState.Unknown;
                case 0x9787: return MainState.WaitingNumericInput;
                default:
                    if ((state1 & 0xff00) == 0xc900)
                        return MainState.Options;
                    if ((state1 & 0xff00) == 0x8300)
                        return MainState.MainMenu;
                    return MainState.Unknown;
            }
        }

        public override void RefreshRollScreen()
        {
            // Exchange stat A with stat B twice
            SendKeysToDOSBox(new Keys[] { Keys.A, Keys.B, Keys.A, Keys.B });
        }

        private MM2GameState ReadMM2GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as MM2GameState;     // Don't spam the game state from different windows

            byte[] bytes = new byte[4];
            byte[] bytes2 = new byte[2];
            long pRead;
            ReadOffset(MM2Memory.MainState, bytes, 2, out pRead);
            ReadOffset(MM2Memory.State2, bytes2, 2, out pRead);
            MM2GameState state = new MM2GameState();

            state.Raw = new MM2RawStateData();
            state.Raw.MainState = bytes;

            state.Main = GetMainState((UInt16)((bytes[1] << 8) | bytes[0]), (UInt16)((bytes2[1] << 8) | bytes2[0]));
            if (state.Main == MainState.EndGame)
            {
                byte[] numMonsters = new byte[1];
                ReadOffset(MM2Memory.NumMonsters, numMonsters);
                if (numMonsters[0] > 0)
                    state.Main = MainState.Combat;
            }

            ReadOffset(MM2Memory.PreEncounter, bytes, 1, out pRead);

            state.InCombat = (state.Main == MainState.Combat || state.Main == MainState.PreCombat || (state.Main == MainState.Rest && bytes[0] != 1));

            int actingAddress = GetActingAddress(state);
            GetCharAddressesAndPositions(out state.CharAddresses, out state.CharPositions);

            if (state.Main != MainState.SignIn)
            {
                if (state.InCombat)
                {
                    state.ActingCharAddress = ReadByte(MM2Memory.CombatActingCharPos);
                    if (state.ActingCharAddress >= 0 && state.ActingCharAddress < state.CharAddresses.Length)
                        state.ActingCharAddress = state.CharAddresses[state.ActingCharAddress];
                }
                else
                    state.ActingCharAddress = actingAddress < 48 ? actingAddress : 0;
                state.ActingPosition = state.CharPositions[state.ActingCharAddress];

                byte[] bytesNumString = new byte[4];

                state.Raw.CombatWaiting = ReadByte(MM2Memory.CombatWaiting);
                state.Raw.CastingSpell = ReadByte(MM2Memory.CastingSpell);
                state.Raw.CastingState = ReadByte(MM2Memory.CastingState);
                state.Raw.CastingState2 = ReadByte(MM2Memory.CastingState2);
                state.Raw.Command = ReadByte(MM2Memory.Command2);

                ReadOffset(MM2Memory.NonCombatSpellNumString, bytesNumString);
                if (Global.Compare(bytesNumString, MM2Memory.SpellNumNonCombat))
                    state.Main = MainState.CastNumber;

                if (state.Main != MainState.WaitingNumericInput && state.Main != MainState.EndGame)
                {
                    if (state.Raw.CombatWaiting == 0 &&
                        (state.Raw.CastingState == 123 || state.Raw.CastingState2 == 0 || state.Raw.CastingState == 177) &&
                        state.Raw.Command != 34 && state.Raw.Command != 28)
                    {
                        state.Main = MainState.CastLevel;
                    }
                    else if (state.Raw.CastingSpell == 1)
                    {
                        if (state.Raw.CastingState == 2)
                            state.Main = MainState.CastLevel;
                        else if (state.Raw.CastingState == 1 && state.Raw.CastingState2 == 1)
                            state.Main = MainState.CastLevel;
                        else if (state.Raw.CastingState == 0 && state.Raw.CastingState2 != 0x85 && state.Raw.CastingState2 != 0x86 && state.Raw.CastingState2 != 23)
                            state.Main = MainState.CastNumber;
                    }
                }
            }
            state.Location = GetLocationForce();

            //if (m_gsCurrent == null || state.Main != m_gsCurrent.Main)
            //    Global.Log("State: {0}", state.Main);

            m_gsCurrent = state;

            return state;
        }

        public override int[] CreationStatWeights { get { return new int[22] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 6, 10, 16, 22, 30 }; } }
        public override int DieMax { get { return 7; } }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return MM2Character.GetStatModifier(value, stat);
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            MM2CharCreationInfo info = new MM2CharCreationInfo();
            info.State = ReadMM2GameState();
            info.AttributesModified = ReadOffset(MM2Memory.CreationStats, 7);
            if (info.State.Main == MainState.CreateSelectClass)
                info.AttributesOriginal = ReadOffset(MM2Memory.CreationStats, 7);
            else if (info.OnCharCreation)
                info.AttributesOriginal = ReadOffset(MM2Memory.UnmodifiedStats, 7);
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            WriteOffset(MM2Memory.CreationStats, info.AttributesOriginal);
            return true;
        }

        public override TrainingInfo GetTrainingInfo()
        {
            if (!IsValid)
                return null;

            MM2TrainingInfo info = new MM2TrainingInfo();
            info.State = ReadMM2GameState();
            info.Party = ReadMM2PartyInfo();
            info.MapIndex = ReadByte(MM2Memory.CurrentMapIndex);
            info.Map = (MM2Map)info.MapIndex;
            return info;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            return false;
        }

        public MM2SearchResults GetSearchResults()
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM2Memory.SearchItems, 16);
            if (bytes == null)
                return null;
            return new MM2SearchResults(bytes, 0);
        }

        private MM2TimeInfo ReadMM2TimeInfo()
        {
            MM2TimeInfo info = new MM2TimeInfo();
            info.EraYears = new ushort[9];
            info.EraDays = new ushort[9];
            info.Bytes = ReadOffset(MM2Memory.DatesAndTimes, 40);
            if (info.Bytes == null)
                return info;

            for (int i = 0; i < 9; i++)
            {
                info.EraDays[i] = BitConverter.ToUInt16(info.Bytes, i * 2);
                info.EraYears[i] = BitConverter.ToUInt16(info.Bytes, 20 + i * 2);
            }

            info.CurrentEra = ReadByte(MM2Memory.CurrentEra);
            info.Steps = ReadByte(MM2Memory.StepCounter);

            return info;
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            MM2GameInfo info = new MM2GameInfo();

            MemoryStream stream = new MemoryStream();

            byte beaconMap = ReadByte(MM2Memory.BeaconLocation);
            byte beaconPoint = ReadByte(MM2Memory.BeaconLocation + 1);
            info.BeaconAndPoint = (UInt16)((beaconMap << 8) | beaconPoint);
            info.BeaconMap = (MM2Map)beaconMap;
            info.BeaconPoint = Global.PointFromByte(beaconPoint);
            Global.Write(stream, info.BeaconAndPoint);

            info.Time = ReadMM2TimeInfo();
            if (info == null || info.Time.Bytes == null)
                return null;
            stream.Write(info.Time.Bytes, 0, 40);

            stream.WriteByte(info.Time.CurrentEra);
            stream.WriteByte(info.Time.Steps);

            info.BattlesWon = ReadUInt16(MM2Memory.BattleStats);
            info.BattlesLost = ReadUInt16(MM2Memory.BattleStats+2);
            Global.Write(stream, (UInt16) info.BattlesWon, (UInt16) info.BattlesLost);

            info.BenefitsUsed1 = ReadByte(MM2Memory.BenefitsUsed1) == 1;
            Global.Write(stream, info.BenefitsUsed1);

            info.Gwyndon = ReadByte(MM2Memory.Gwyndon) == 1;
            Global.Write(stream, info.Gwyndon);

            info.BenefitsUsed2 = ReadByte(MM2Memory.BenefitsUsed2) == 1;
            Global.Write(stream, info.BenefitsUsed2);

            info.MapAttributes = (MM2MapAttributes)GetMapAttributes();
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

            MemoryBytes bytesCartography = ReadOffset(MM2Memory.CartographyData, 32 * 60);
            if (bytesCartography == null)
                return null;

            stream.Write(bytesCartography, 0, bytesCartography.Length);

            info.HirelingTowns = new byte[24];
            for(int i = 0; i < 24; i++)
                info.HirelingTowns[i] = ReadByte(MM2Memory.HirelingChars + (i * MM2Character.SizeInBytes) + MM2.Offsets.Town);
            stream.Write(info.HirelingTowns, 0, info.HirelingTowns.Length);

            info.Bytes = stream.ToArray();

            info.Exploration = new AmountExplored(Global.NumBitsSet(bytesCartography), 60 * 256);

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is MM2GameInfo))
                return GetGameInfo();

            MM2GameInfo mm2Old = infoOld as MM2GameInfo;
            MM2GameInfo mm2New = GetGameInfo() as MM2GameInfo;

            if (mm2New == null)
                return null;

            if (Global.Compare(mm2Old.Bytes, mm2New.Bytes))
                return infoOld; // All the bytes are the same; return the old object

            return mm2New;
        }

        public override bool HasBeacon { get { return true; } }

        public override bool SetBeacon(Point ptLocation, int iMap)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[2];
            bytes[0] = (byte) iMap;
            bytes[1] = (byte) (ptLocation.X | (ptLocation.Y << 4));
            return WriteOffset(MM2Memory.BeaconLocation, bytes);
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            MM2EncounterInfo info = new MM2EncounterInfo();

            info.GameState = ReadMM2GameState();

            info.NumTotalMonsters = 0;
            info.HandicapTarget = 0;
            info.HandicapValue = 0;
            info.Round = 0;

            info.NumTotalMonsters = ReadByte(MM2Memory.NumMonsters);
            info.SearchResults = GetSearchResults();

            MemoryStream allBytes = new MemoryStream(6320);

            allBytes.WriteByte((byte)info.NumTotalMonsters);
            if (info.SearchResults == null)
                return null;

            allBytes.Write(info.SearchResults.Bytes, 0, info.SearchResults.Bytes.Length);

            if (info.GameState.InCombat)
            {
                info.MonsterHP = ReadOffset(MM2Memory.MonsterHP, 22);
                info.MonsterStatus = ReadOffset(MM2Memory.MonsterCombatStatus, 11);
                info.EncounterList = ReadOffset(MM2Memory.EncounterList, 11);
                info.Entrapped = (ReadByte(MM2Memory.Entrapment) & 1) > 0;
                Global.Write(allBytes, info.Entrapped);
                info.PreEncounter = ReadByte(MM2Memory.PreEncounter) == 1;
                Global.Write(allBytes, info.PreEncounter);
                info.PartyMelee = ReadByte(MM2Memory.NumPartyInMelee);
                allBytes.WriteByte(info.PartyMelee);
                info.NumMeleeMonsters = ReadByte(MM2Memory.MonstersInMelee);
                allBytes.WriteByte((byte)info.NumMeleeMonsters);
                info.MonsterMoved = ReadOffset(MM2Memory.CombatMonstersMoved, 10);
                info.PartyMoved = ReadOffset(MM2Memory.CombatPartyMoved, 8);

                uint iMonsterData = GetMonsterDataLocation();
                info.RawMonsterData = ReadOffset(iMonsterData, 0x1a00);

                info.CreateMonsterList();

                info.Party = ReadMM2PartyInfo();

                allBytes.Write(info.MonsterHP, 0, info.MonsterHP.Length);
                allBytes.Write(info.MonsterStatus, 0, info.MonsterStatus.Length);
                allBytes.Write(info.EncounterList, 0, info.EncounterList.Length);
                allBytes.Write(info.Party.Bytes, 0, info.Party.Bytes.Length);
                allBytes.Write(info.MonsterMoved, 0, info.MonsterMoved.Length);
                allBytes.Write(info.PartyMoved, 0, info.PartyMoved.Length);
                allBytes.Write(info.RawMonsterData, 0, info.RawMonsterData.Length);

                info.PartyLocation = GetPartyPosition();
                allBytes.WriteByte((byte)info.PartyLocation.X);
                allBytes.WriteByte((byte)info.PartyLocation.Y);
            }

            info.AllBytes = allBytes.ToArray();
            allBytes.Close();

            return info;
        }

        private uint GetMonsterDataLocation()
        {
            uint iResult = ReadUInt16(MM2Memory.MonsterDataPointer);
            iResult += 2;
            iResult <<= 4;
            iResult -= m_offsetFoundBlock;

            // Check the icon indices for a few of the monsters to verify that this is the correct location
            uint[] offsets = new uint[] { iResult, iResult + 0x20, iResult + 0x40 };
            foreach (uint iTest in offsets)
            {
                byte icon1 = ReadByte(iTest + 0x15);
                byte icon2 = ReadByte(iTest + 0x2f);
                byte icon3 = ReadByte(iTest + 0x49);
                byte icon4 = ReadByte(iTest + 0x63);

                if (icon1 == 1 && icon2 == 2 && icon3 == 4 && icon4 == 5)
                    return iTest;
            }
            
            return iResult;
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes pos = ReadOffset(MM2Memory.Location, 2);
            if (pos == null)
                return Global.NullPoint;

            return new Point(pos.Bytes[0], pos.Bytes[1]);
        }

        public LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(GetPartyPosition());
            switch (ReadByte(MM2Memory.FacingChar))
            {
                case (byte) 'W':
                    info.Facing = Direction.Left;
                    break;
                case (byte) 'N':
                    info.Facing = Direction.Up;
                    break;
                case (byte) 'E':
                    info.Facing = Direction.Right;
                    break;
                case (byte) 'S':
                    info.Facing = Direction.Down;
                    break;
                default:
                    break;
            }
            info.NumChars = ReadByte(MM2Memory.NumChars1);

            byte bKey = ReadByte(MM2Memory.LastFPKeypress);
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
            info.LastSpellNonCombat = MMGenericSpell.None;

            info.MapIndex = ReadByte(MM2Memory.CurrentMapIndex);

            info.InInn = info.InInn = InSpots(new MapXY((MM2Map)info.MapIndex, info.PrimaryCoordinates.X, info.PrimaryCoordinates.Y), MM2.Spots.FullInns);

            info.CanUseBag = info.InInn || Global.Cheats;

            MM2MapAttributes mapAttrib = (MM2MapAttributes) GetMapAttributes();
            if (mapAttrib != null)
                info.Outside = mapAttrib.IsOutside(info.PrimaryCoordinates);

            info.LightDistance = GetLightDistance(info.PrimaryCoordinates);
            return info;
        }

        public override bool BagNeedsRosterFile { get { return false; } }       // MM2 always has the entire roster loaded into memory

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[2] { (byte) ptLocation.X, (byte) ptLocation.Y };
            WriteOffset(MM2Memory.Location, bytes);

            return true;
        }

        public override string StatToolTip(int iIndex, int iValue)
        {
            PrimaryStat[] order = StatOrder;
            if (order[iIndex] == PrimaryStat.Might)
                return String.Format("Might gives a bonus ({0}: {1}) to damage inflicted with melee weapons in combat.  The base value is 0.", iValue, GetStatModString(iValue, PrimaryStat.Might));
            if (order[iIndex] == PrimaryStat.Intellect)
                return String.Format("Intellect gives a bonus ({0}: {1}) per level to the number of spell points for Sorcerers and Archers.  The base value is 3 SP/level.", iValue, GetStatModString(iValue, PrimaryStat.Intellect));
            if (order[iIndex] == PrimaryStat.Personality)
                return String.Format("Personality gives a bonus ({0}: {1}) per level to the number of spell points for Clerics and Paladins.  The base value is 3 SP/level.", iValue, GetStatModString(iValue, PrimaryStat.Personality));
            if (order[iIndex] == PrimaryStat.Endurance)
                return String.Format("Endurance gives a bonus ({0}: {1}) to the number of hit points gained per level.  The base values are Sorcerer:6, Cleric, Robber:8, Paladin, Archer, Ninja:10, Archer:10, Knight:12, Barbarian:15", iValue, GetStatModString(iValue, PrimaryStat.Endurance));
            if (order[iIndex] == PrimaryStat.Speed)
                return String.Format("Speed gives a bonus ({0}: {1}) to armor class.  The base value is 0.  Speed also determines the order of actions in combat.", iValue, GetStatModString(iValue, PrimaryStat.Speed));
            if (order[iIndex] == PrimaryStat.Accuracy)
                return String.Format("Accuracy gives a bonus ({0}: {1}) to hit in combat.  The base value is 0.", iValue, GetStatModString(iValue, PrimaryStat.Accuracy));
            if (order[iIndex] == PrimaryStat.Luck)
                return String.Format("Luck gives a bonus ({0}: {1}) to the thievery skill and may have other miscellaneous effects in-game.  The base thievery value is 30+1/lv for robbers, 10+1/lv for ninjas, and 0 for other classes.", iValue, GetStatModString(iValue, PrimaryStat.Luck));
            return "";
        }

        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "Fire/Elec/Cold: 5, Sleep/Poison: 60";
                case GenericRace.Elf: return "INT/ACY+1, MGT/END-1, Sleep: 30, Poison: 5";
                case GenericRace.Dwarf: return "END/LUC+1, INT/SPD-1, Fire/Elec/Cold: 10, Poison: 60";
                case GenericRace.Gnome: return "LUC+2, SPD/ACY-1, Magic Res: 35";
                case GenericRace.HalfOrc: return "MGT/END+1, INT/PER/LUC-1, Fire/Elec/Cold: 5, Sleep/Poison: 30";
                default: return "Unknown";
            }
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            MMMapData data = new MM2MapData();
            data.Appearance = ReadOffset(MM2Memory.MapAppearance, 256);
            data.Attributes = ReadOffset(MM2Memory.MapAttributes, 256);
            data.Outside = ReadByte(MM2Memory.IsOutside) == 1;
            data.Title = GetMapTitlePair(iMapIndex);

            if (bIncludeStrings)
                data.ScriptInfo = GetScriptInfo() as MMScriptInfo;

            return data;
        }

        public override string GetMapStrings(bool bRaw = false)
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM2Memory.MapCode, MM2Memory.MapCodeLength);
            MemoryBytes bytes2 = ReadOffset(MM2Memory.ExtraStrings, MM2Memory.ExtraStringsLen);
            MemoryBytes bytes3 = ReadOffset(MM2Memory.ExtraStrings2, MM2Memory.ExtraStrings2Len);

            if (bytes == null || bytes2 == null || bytes3 == null)
                return null;

            StringBuilder sb = new StringBuilder();
            foreach (MMString mm2String in GetScriptStrings(bytes))
                sb.AppendFormat("{0}\r\n", mm2String.ToString().Replace('\n', ' '));

            string s1 = sb.ToString().Replace('@', ' ');
            string s2 = Global.GetStrings(4, bytes2, true).Replace('@', ' ');
            string s3 = Global.GetStrings(4, bytes3, true).Replace('@', ' ');

            return (s1 + s2 + s3);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(MM2Memory.PartyInfo + (iAddress * MM2Character.SizeInBytes), bytes);
        }

        public override bool SetMonsterInfo(int iIndex, MonsterBasicInfo info)
        {
            if (!IsValid)
                return false;

            if (info.Game != GameNames.MightAndMagic2)
                return false;

            long pWritten;
            byte[] bytesTemp = new byte[2];

            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)info.CurrentHP), 0, bytesTemp, 0, 2);
            WriteOffset(MM2Memory.MonsterHP + (2*iIndex), bytesTemp, 2, out pWritten);

            bytesTemp[0] = (byte)MM2Monster.GetCondition(info.Condition);
            WriteOffset(MM2Memory.MonsterCombatStatus + iIndex, bytesTemp, 1, out pWritten);

            bytesTemp[0] = (byte)info.Index;
            WriteOffset(MM2Memory.EncounterList + iIndex, bytesTemp, 1, out pWritten);

            uint iMonsterData = GetMonsterDataLocation();
            byte[] bytes = info.GetBytes();
            WriteOffset(iMonsterData + (26 * info.Index), bytes);

            return ((int)pWritten == bytes.Length);
        }

        public override MonsterBasicInfo GetMonsterInfo(int iIndex)
        {
            if (!IsValid)
                return null;

            uint iMonsterData = GetMonsterDataLocation();
            MemoryBytes bytes = ReadOffset(iMonsterData + (26 * iIndex), 26);
            if (bytes == null)
                return null;
            return new MonsterBasicInfo(m_game, new MM2Monster(bytes, 0, iIndex, MM2MonsterCombatStatus.Good, false), null);
        }

        public override bool TradeBackpacks(int iCharAddress1, int iCharAddress2)
        {
            // Equipped: bytes 40-57
            // Backpack: bytes 58-75

            byte[] bytes1 = new byte[18];
            byte[] bytes2 = new byte[18];

            long iAddress1 = MM2Memory.PartyInfo + (iCharAddress1 * MM2Character.SizeInBytes);
            long iAddress2 = MM2Memory.PartyInfo + (iCharAddress2 * MM2Character.SizeInBytes);

            bool bResult = ReadOffset(iAddress1 + MM2.Offsets.BackpackBases, bytes1);
            bResult = bResult && ReadOffset(iAddress2 + MM2.Offsets.BackpackBases, bytes2);

            if (!bResult)
                return bResult;

            bResult = WriteOffset(iAddress2 + MM2.Offsets.BackpackBases, bytes1);
            bResult = bResult && WriteOffset(iAddress1 + MM2.Offsets.BackpackBases, bytes2);

            return bResult;
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            if (items.Count > 6)
                return SetBackpackResult.InsufficientSpace;

            // Backpack: bytes 58-75

            byte[] bytes = new byte[18];

            for (int i = 0; i < 18; i++)
                bytes[i] = 0;

            for(int i = 0; i < items.Count; i++)
            {
                MM2Item mm2Item = items[i] as MM2Item;
                if (mm2Item == null)
                    continue;

                bytes[i] = (byte)mm2Item.Index;
                bytes[i + 6] = mm2Item.m_iChargesCurrent;
                bytes[i + 12] = (byte)mm2Item.BonusCurrent;
            }

            WriteOffset(MM2Memory.PartyInfo + (iCharAddress * MM2Character.SizeInBytes) + MM2.Offsets.BackpackBases, bytes);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            if (!IsValid)
                return null;

            List<Item> backpack = new List<Item>(6);

            // Backpack: bytes 58-75

            MemoryBytes bytes = ReadOffset(MM2Memory.PartyInfo + (iCharAddress * MM2Character.SizeInBytes) + MM2.Offsets.BackpackBases, 18);
            if (bytes == null)
                return null;

            for (int i = 0; i < 6; i++)
                backpack.Add(MM2Item.FromBagBytes(new byte[] { bytes[i], bytes[i+6], bytes[i+12] }));

            return backpack;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!IsValid)
                return -1;

            byte[] bytes = new byte[MM2Character.SizeInBytes];
            while (iStart >= 0)
            {
                ReadOffset(MM2Memory.PartyInfo + (iStart * MM2Character.SizeInBytes), bytes);
                MM2Character mm2Char = MM2Character.Create(bytes);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (mm2Char.Name == "Inventory" && mm2Char.Town != 0)   // Town of 0 means "deleted"
                            return iStart;
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (mm2Char.Name == "Inventory" && mm2Char.Town != 0)
                            return iStart;
                        if (mm2Char.Name == "" || mm2Char.Town == 0)
                        {
                            // No character in the roster at this position; make a new one;
                            bytes = MM2Character.GetInventoryCharBytes();
                            WriteOffset(MM2Memory.PartyInfo + (iStart * MM2Character.SizeInBytes), bytes);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (mm2Char.Name == "Inventory" && mm2Char.Town != 0)
                            return iStart;
                        if (mm2Char.Town == 0)
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            return -1;
        }

        public override int MaxInventoryChar { get { return 24; } }

        public override int MinimumBlockOffset { get { return 33082; } }

        public override string GetDebugMemoryInfo()
        {
            if (!IsValid)
                return "<no info available; game program may not be running>";

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Current Map Index: {0}\r\n", ReadByte(MM2Memory.CurrentMapIndex));

            return sb.ToString();
        }

        public override string GetEncounterNoteText(string strPrefix, byte[] bytesCommand)
        {
            if (Global.AllNull(bytesCommand))
                return "Forced Encounter";

            Dictionary<string, MonsterCount> dict = new Dictionary<string, MonsterCount>();
            for (int i = 1; i < bytesCommand.Length; i++)   // skip the first byte, which is the "encounter" command
            {
                if (bytesCommand[i] == 0)   // Fixed encounters don't use monster #0
                    break;

                if (bytesCommand[i] >= MM2.Monsters.Count)
                    continue;       // Something is wrong

                string strMonster = MM2.Monsters[bytesCommand[i]].ProperName;
                if (dict.ContainsKey(strMonster))
                    dict[strMonster].Count++;
                else
                    dict.Add(strMonster, new MonsterCount(strMonster, 1));

                if (i == 11 && bytesCommand.Length > 12)
                {
                    // The last byte specifies the number of extra monsters that byte[10] indicates
                    dict[strMonster].Count += (bytesCommand[12] - 1);
                    break;
                }
            }

            return strPrefix + MonsterCount.MonsterList(dict);
        }

        public override string ReplaceNoteStrings(string str)
        {
            if (!IsValid)
                return str;

            StringBuilder sbResult = new StringBuilder(str);

            if (str.Contains("EncounterMonsters"))
            {
                int iNumMonsters = ReadByte(MM2Memory.NumMonsters);
                byte[] byteMonsters = ReadOffset(MM2Memory.EncounterList, 11);

                if (str.Contains("$uniqueEncounterMonsters"))
                {
                    Dictionary<byte, MMMonster> uniqueMonsters = new Dictionary<byte, MMMonster>();
                    for (int i = 0; i < iNumMonsters && i < byteMonsters.Length; i++)
                    {
                        if (!uniqueMonsters.ContainsKey(byteMonsters[i]))
                            uniqueMonsters.Add(byteMonsters[i], MM2.Monsters[byteMonsters[i]]);
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (MMMonster monster in uniqueMonsters.Values)
                    {
                        sb.AppendFormat("{0}, ", monster.ProperName);
                    }
                    if (Global.Trim(sb).Length == 0)
                        sb = new StringBuilder("Unknown");

                    sbResult.Replace("$uniqueEncounterMonsters", sb.ToString());
                }
                if (str.Contains("$allEncounterMonsters"))
                {
                    Dictionary<string, MonsterCount> dict = new Dictionary<string, MonsterCount>();
                    for (int i = 0; i < 11; i++)
                    {
                        if (i >= iNumMonsters)
                            break;
                        int iMonsterNumber = (i < 10 ? 1 : iNumMonsters - 10);

                        string strName = MM2.Monsters[byteMonsters[i]].ProperName;
                        if (!dict.ContainsKey(strName))
                            dict.Add(strName, new MonsterCount(strName, iMonsterNumber));
                        else
                            dict[strName].Count += iMonsterNumber;
                    }

                    sbResult.Replace("$allEncounterMonsters", MonsterCount.MonsterList(dict));
                }
            }
            return sbResult.ToString();
        }

        public override MapAttributes GetMapAttributes()
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(MM2Memory.MapGlobalAttributes, 64);
            if (bytes == null)
                return null;

            MM2MapAttributes map = new MM2MapAttributes();
            map.Bytes = bytes;

            map.Index = ReadByte(MM2Memory.CurrentMapIndex);
            map.SafestSquare = Global.PointFromByte(bytes[MM2MapAttributeOffsets.SafestSquare]);
            map.Exits.Add(new MMExit(MMExitDirection.North, bytes[MM2MapAttributeOffsets.ExitNorth]));
            map.Exits.Add(new MMExit(MMExitDirection.East, bytes[MM2MapAttributeOffsets.ExitEast]));
            map.Exits.Add(new MMExit(MMExitDirection.South, bytes[MM2MapAttributeOffsets.ExitSouth]));
            map.Exits.Add(new MMExit(MMExitDirection.West, bytes[MM2MapAttributeOffsets.ExitWest]));
            map.Exits.Add(new MMExit(MMExitDirection.Surface, bytes[MM2MapAttributeOffsets.SurfaceMap], Global.PointFromByte(bytes[MM2MapAttributeOffsets.SurfaceCoord])));
            map.Exits.Add(new MMExit(MMExitDirection.Run, -1, map.SafestSquare));
            map.EncounterSize = bytes[MM2MapAttributeOffsets.EncounterSize];
            map.MonsterGroup = bytes[MM2MapAttributeOffsets.MonsterGroup];
            map.DefaultEra = bytes[MM2MapAttributeOffsets.DefaultEra];
            map.ForbiddenSpells = bytes[MM2MapAttributeOffsets.ForbiddenSpells];
            map.UndergroundLevel = bytes[MM2MapAttributeOffsets.UndergroundLevel];

            map.Flags = MapAttributeFlags.AllowFly;
            if ((bytes[MM2MapAttributeOffsets.ForbiddenSpells] & 0x10) == 0)
                map.Flags |= MapAttributeFlags.AllowTeleport;
            if ((bytes[MM2MapAttributeOffsets.ForbiddenSpells] & 0x20) == 0)
                map.Flags |= MapAttributeFlags.AllowEtherealize;
            if ((bytes[MM2MapAttributeOffsets.ForbiddenSpells] & 0x40) == 0)
                map.Flags |= MapAttributeFlags.AllowBasicTransport;
            if ((bytes[MM2MapAttributeOffsets.ForbiddenSpells] & 0x80) == 0x80)
                map.Flags |= MapAttributeFlags.Darkness;

            return map;
        }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            MM2QuestInfo info = new MM2QuestInfo();

            MM2PartyInfo party = ReadMM2PartyInfo();
            if (party == null)
                return null;

            MemoryStream stream = new MemoryStream();
            byte[] questBytes = party.QuestBytes;
            if (questBytes == null)
                return null;
            stream.Write(questBytes, 0, questBytes.Length);
            stream.WriteByte((byte)iOverrideCharAddress);
            info.MapIndex = GetCurrentMapIndex();
            stream.WriteByte((byte)info.MapIndex);
            stream.WriteByte(ReadByte(MM2Memory.Gwyndon));
            stream.WriteByte(ReadByte(MM2Memory.BenefitsUsed1));
            stream.WriteByte(ReadByte(MM2Memory.BenefitsUsed2));
            stream.WriteByte(ReadByte(MM2Memory.CurrentDay));

            byte[] newBytes = stream.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(new MM2QuestData(party, GetLocation(), GetGameInfo() as MM2GameInfo), iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override bool SetQuestBits(int iAddress, QuestBits bits, bool bSet)
        {
            if (iAddress < 0)
                return false;

            if (!IsValid)
                return false;

            MM2PartyInfo party = ReadMM2PartyInfo();
            if (party == null)
                return false;

            byte[] knownSpells = new byte[6];
            Buffer.BlockCopy(party.Bytes, iAddress * MM2Character.SizeInBytes + MM2.Offsets.Spells, knownSpells, 0, MM2.Offsets.SpellsLength);

            byte[] questBytes = new byte[12];
            Buffer.BlockCopy(party.Bytes, iAddress * MM2Character.SizeInBytes + MM2.Offsets.Awards, questBytes, 0, MM2.Offsets.AwardsLength);

            byte skills = party.Bytes[iAddress * MM2Character.SizeInBytes + MM2.Offsets.Skills];

            MM2KnownSpells spells = new MM2KnownSpells(knownSpells, 0);

            foreach (object bit in bits.Bits)
            {
                if (bit is MM2MealsEaten)
                {
                    MM2MealsEaten meals = (MM2MealsEaten) ((questBytes[0] << 8) | questBytes[1]);
                    if (bSet)
                        meals |= (MM2MealsEaten)bit;
                    else
                        meals &= ~(MM2MealsEaten)bit;
                    questBytes[0] = (byte) ((int) meals >> 8);
                    questBytes[1] = (byte) ((int) meals & 0xff);
                }
                else if (bit is MM2GuildFlags)
                {
                    MM2GuildFlags guilds = (MM2GuildFlags)questBytes[3];
                    if (bSet)
                        guilds |= (MM2GuildFlags)bit;
                    else
                        guilds &= ~(MM2GuildFlags)bit;
                    questBytes[3] = (byte) guilds;
                }
                else if (bit is MM2AdvancementFlags)
                {
                    // Advancement is a little bit special thanks to the sorcerers
                    questBytes[4] = (byte) (bSet ? 0xff : 0);
                    WriteByte(MM2Memory.Gwyndon, (byte)(bSet ? 1 : 0));
                    //MM2AdvancementFlags adv = (MM2AdvancementFlags)questBytes[4];
                    //if (bSet)
                    //    adv |= (MM2AdvancementFlags)bit;
                    //else
                    //    adv &= ~(MM2AdvancementFlags)bit;
                    //questBytes[4] = (byte) adv;
                }
                else if (bit is MM2QuestFlags1)
                {
                    MM2QuestFlags1 questFlags1 = (MM2QuestFlags1) ((questBytes[5] << 16) | (questBytes[6] << 8) | questBytes[7]);
                    if (bSet)
                        questFlags1 |= (MM2QuestFlags1)bit;
                    else
                        questFlags1 &= ~(MM2QuestFlags1)bit;
                    questBytes[5] = (byte) ((int) questFlags1 >> 16);
                    questBytes[6] = (byte) (((int) questFlags1 >> 8) & 0xff);
                    questBytes[7] = (byte) ((int) questFlags1 & 0xff);
                }
                else if (bit is MM2ArenaFlags)
                {
                    MM2ArenaFlags arena = (MM2ArenaFlags)((questBytes[8] << 8) | questBytes[9]);
                    if (bSet)
                        arena |= (MM2ArenaFlags)bit;
                    else
                        arena &= ~(MM2ArenaFlags)bit;
                    questBytes[8] = (byte) ((int) arena >> 8);
                    questBytes[9] = (byte) ((int) arena & 0xff);
                }
                else if (bit is MM2QuestFlags2)
                {
                    MM2QuestFlags2 questFlags2 = (MM2QuestFlags2)((questBytes[10] << 8) | questBytes[11]);
                    if (bSet)
                        questFlags2 |= (MM2QuestFlags2)bit;
                    else
                        questFlags2 &= ~(MM2QuestFlags2)bit;
                    questBytes[10] = (byte) ((int) questFlags2 >> 8);
                    questBytes[11] = (byte) ((int) questFlags2 & 0xff);
                }
                else if (bit is MM2SpellIndex)
                {
                    MM2Spell spell = MM2.Spells[(int) bit];
                    spells.Spells[spell.Level, spell.Number] = bSet;
                }
                else if (bit is MM2HirelingFlags)
                {
                    if (bSet)
                        party.Hirelings |= (MM2HirelingFlags)bit;
                    else
                        party.Hirelings &= ~(MM2HirelingFlags)bit;
                }
                else if (bit is MM2QuestStates.Pegasus)
                {
                    if (bSet)
                        party.VisitedPegasus = true;
                    else
                        party.VisitedPegasus = false;
                }
                else if (bit is MM2DonationFlags)
                {
                    if (bSet)
                        party.Donations |= (MM2DonationFlags)bit;
                    else
                        party.Donations &= ~(MM2DonationFlags)bit;
                }
                else if (bit is MM2QuestStates.Hoardall || bit is MM2QuestStates.Slayer)
                {
                    // Can't really "set" this value, since it's generated randomly when the quest is accepted
                    if (!bSet)
                        questBytes[2] = 0;
                }
                else if (bit is MM2SecondarySkill)
                {
                    // Can't set this value, since only two secondary skills can be learned at a time
                    if (!bSet)
                        skills = 0;
                }
            }

            knownSpells = spells.GetBytes();
            byte[] hirelingBytes = new byte[24];
            for (int i = 0; i < 24; i++)
            {
                if (((((int) party.Hirelings) >> i) & 1) == 1)
                    hirelingBytes[i] = 1;
                else
                    hirelingBytes[i] = 0;
            }

            WriteOffset(MM2Memory.PartyInfo + (iAddress * MM2Character.SizeInBytes + MM2.Offsets.Spells), knownSpells);
            WriteOffset(MM2Memory.PartyInfo + (iAddress * MM2Character.SizeInBytes + MM2.Offsets.Awards), questBytes);
            WriteByte(MM2Memory.PartyInfo + (iAddress * MM2Character.SizeInBytes + MM2.Offsets.Skills), skills);
            WriteOffset(MM2Memory.HirelingsRescued, hirelingBytes);
            byte[] bytes = new byte[1];
            bytes[0] = (byte) (party.VisitedPegasus ? 1 : 0);
            WriteOffset(MM2Memory.VisitedPegasus, bytes);
            return WriteByte(MM2Memory.Donations, (byte)party.Donations);
        }

        public override GameState GetGameState()
        {
            return ReadMM2GameState();
        }

        public MMActiveEffects GetActiveEffects()
        {
            if (!IsValid)
                return null;


            MemoryBytes bytes = ReadOffset(MM2Memory.ActiveEffects, 84);
            if (bytes == null)
                return null;

            MMActiveEffects effects = new MMActiveEffects();
            effects.ProtForces = bytes[MM2EffectsOffsets.ProtForces];
            effects.WaterTransmutation = (bytes[MM2EffectsOffsets.WaterTransmutation] != 0);
            effects.AirTransmutation = (bytes[MM2EffectsOffsets.AirTransmutation] != 0);
            effects.FireTransmutation = (bytes[MM2EffectsOffsets.FireTransmutation] != 0);
            effects.EarthTransmutation = (bytes[MM2EffectsOffsets.EarthTransmutation] != 0);
            effects.EagleEye = bytes[MM2EffectsOffsets.EagleEye];
            effects.WizardEye = bytes[MM2EffectsOffsets.WizardEye];
            effects.ProtMagic = bytes[MM2EffectsOffsets.ProtMagic];
            effects.LightFactors = bytes[MM2EffectsOffsets.LightFactors];
            effects.HolyBonus = bytes[MM2EffectsOffsets.HolyBonus];
            effects.Levitate = (bytes[MM2EffectsOffsets.Levitate] != 0);
            effects.WaterWalk = (bytes[MM2EffectsOffsets.WaterWalk] != 0);
            effects.GuardDog = (bytes[MM2EffectsOffsets.GuardDog] != 0);
            effects.Entrapment = (bytes[MM2EffectsOffsets.Entrapment] != 0);
            effects.Bless = (bytes[MM2EffectsOffsets.Bless] != 0);
            effects.Invisibility = (bytes[MM2EffectsOffsets.Invisibility] != 0);
            effects.Shield = (bytes[MM2EffectsOffsets.Shield] != 0);
            effects.PowerShield = (bytes[MM2EffectsOffsets.PowerShield] != 0);
            effects.Cursed = bytes[MM2EffectsOffsets.Cursed];

            return effects;
        }

        public override bool SetActiveEffect(MMEffectTag effect)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[1];
            switch (effect.Effect)
            {
                case MMEffects.ProtForces:
                case MMEffects.ProtMagic:
                case MMEffects.LightFactors:
                case MMEffects.Cursed:
                case MMEffects.EagleEye:
                case MMEffects.WizardEye:
                case MMEffects.HolyBonus:
                    bytes[0] = (byte)(effect.Enabled ? effect.Value : 0);
                    break;
                case MMEffects.Levitate:
                case MMEffects.GuardDog:
                case MMEffects.WaterWalk:
                case MMEffects.Bless:
                case MMEffects.Invisibility:
                case MMEffects.Shield:
                case MMEffects.PowerShield:
                case MMEffects.WaterTransmutation:
                case MMEffects.AirTransmutation:
                case MMEffects.FireTransmutation:
                case MMEffects.EarthTransmutation:
                case MMEffects.Entrapment:
                    bytes[0] = (byte)(effect.Enabled ? 1 : 0);
                    break;
                default:
                    return false;
            }

            int iOffset = effect.MM2Offset;
            if (iOffset != -1)
                return WriteOffset(MM2Memory.ActiveEffects + iOffset, bytes);

            return false;
        }

        public static string GetMapName(MM2Map map)
        {
            return GetMapTitlePair((int)map).Title;
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch((MM2Map) index)
            {
                case MM2Map.C2Middlegate: return new MapTitleInfo(index, "C-2, Middlegate");
                case MM2Map.A4Atlantium: return new MapTitleInfo(index, "A-4, Atlantium");
                case MM2Map.A1Tundara: return new MapTitleInfo(index, "A-1, Tundara");
                case MM2Map.E1Vulcania: return new MapTitleInfo(index, "E-1, Vulcania");
                case MM2Map.E4Sandsobar: return new MapTitleInfo(index, "E-4, Sandsobar");
                case MM2Map.A1Surface: return new MapTitleInfo(index, "A-1, Surface");
                case MM2Map.B1Surface: return new MapTitleInfo(index, "B-1, Surface");
                case MM2Map.C1Surface: return new MapTitleInfo(index, "C-1, Surface");
                case MM2Map.D1Surface: return new MapTitleInfo(index, "D-1, Surface");
                case MM2Map.A2Surface: return new MapTitleInfo(index, "A-2, Surface");
                case MM2Map.B2Surface: return new MapTitleInfo(index, "B-2, Surface");
                case MM2Map.C2Surface: return new MapTitleInfo(index, "C-2, Surface");
                case MM2Map.A3Surface: return new MapTitleInfo(index, "A-3, Surface");
                case MM2Map.B3Surface: return new MapTitleInfo(index, "B-3, Surface");
                case MM2Map.C3Surface: return new MapTitleInfo(index, "C-3, Surface");
                case MM2Map.A4Surface: return new MapTitleInfo(index, "A-4, Surface");
                case MM2Map.B4Surface: return new MapTitleInfo(index, "B-4, Surface");
                case MM2Map.C2MiddlegateDungeon: return new MapTitleInfo(index, "C-2, Cavern Under Middlegate");
                case MM2Map.A4AtlantiumDungeon: return new MapTitleInfo(index, "A-4, Cavern Under Atlantium");
                case MM2Map.A1TundaraDungeon: return new MapTitleInfo(index, "A-1, Cavern Under Tundara");
                case MM2Map.E1VulcaniaDungeon: return new MapTitleInfo(index, "E-1, Cavern Under Vulcania");
                case MM2Map.E4SandsobarDungeon: return new MapTitleInfo(index, "E-4, Cavern Under Sandsobar");
                case MM2Map.C2CoraksCave: return new MapTitleInfo(index, "C-2, Corak's Cave");
                case MM2Map.C2SquareLakeCave: return new MapTitleInfo(index, "C-2, Square Lake Cave");
                case MM2Map.B1IceCavern: return new MapTitleInfo(index, "B-1, Ice Cavern");
                case MM2Map.A2SarakinsMine: return new MapTitleInfo(index, "A-2, Sarakin's Mine");
                case MM2Map.B4MurraysCave: return new MapTitleInfo(index, "B-4, Murray's Cave");
                case MM2Map.C3DruidsCave: return new MapTitleInfo(index, "C-3, Druid's Cave");
                case MM2Map.C3ForbiddenForestCavern: return new MapTitleInfo(index, "C-3, Forbidden Forest Cavern");
                case MM2Map.E1DragonsDominion: return new MapTitleInfo(index, "E-1, Dragon's Dominion");
                case MM2Map.D4DawnsCavern: return new MapTitleInfo(index, "D-4, Dawn's Cavern");
                case MM2Map.E1GemmakersCave: return new MapTitleInfo(index, "E-1, Gemmaker's Cave");
                case MM2Map.E3NomadicRiftCave: return new MapTitleInfo(index, "E-3, Nomadic Rift Cave");
                case MM2Map.E1Surface: return new MapTitleInfo(index, "E-1, Surface");
                case MM2Map.D2Surface: return new MapTitleInfo(index, "D-2, Surface");
                case MM2Map.E2Surface: return new MapTitleInfo(index, "E-2, Surface");
                case MM2Map.D3Surface: return new MapTitleInfo(index, "D-3, Surface");
                case MM2Map.E3Surface: return new MapTitleInfo(index, "E-3, Surface");
                case MM2Map.C4Surface: return new MapTitleInfo(index, "C-4, Surface");
                case MM2Map.D4Surface: return new MapTitleInfo(index, "D-4, Surface");
                case MM2Map.E4Surface: return new MapTitleInfo(index, "E-4, Surface");
                case MM2Map.PlaneOfAir: return new MapTitleInfo(index, "Plane of Air");
                case MM2Map.PlaneOfFire: return new MapTitleInfo(index, "Plane of Fire");
                case MM2Map.PlaneOfEarth: return new MapTitleInfo(index, "Plane of Earth");
                case MM2Map.PlaneOfWater: return new MapTitleInfo(index, "Plane of Water");
                case MM2Map.D4CastleHillstoneB1: return new MapTitleInfo(index, "D-4, Castle Hillstone, B1");
                case MM2Map.D4CastleHillstoneB2: return new MapTitleInfo(index, "D-4, Castle Hillstone, B2");
                case MM2Map.C1CastleWoodhavenB1: return new MapTitleInfo(index, "C-1, Castle Woodhaven, B1");
                case MM2Map.C1CastleWoodhavenB2: return new MapTitleInfo(index, "C-1, Castle Woodhaven, B2");
                case MM2Map.A2CastlePinehurstB1: return new MapTitleInfo(index, "A-2, Castle Pinehurst, B1");
                case MM2Map.A2CastlePinehurstB2: return new MapTitleInfo(index, "A-2, Castle Pinehurst, B2");
                case MM2Map.D2GreatLuxusPalaceRoyaleB1: return new MapTitleInfo(index, "D-2, Great Luxus Palace Royale, B1");
                case MM2Map.D2GreatLuxusPalaceRoyaleB2: return new MapTitleInfo(index, "D-2, Great Luxus Palace Royale, B2");
                case MM2Map.B3CastleOfEvil: return new MapTitleInfo(index, "B-3, Castle of Evil");
                case MM2Map.B4CastleOfGood: return new MapTitleInfo(index, "B-4, Castle of Good");
                case MM2Map.D4CastleHillstone: return new MapTitleInfo(index, "D-4, Castle Hillstone");
                case MM2Map.C1CastleWoodhaven: return new MapTitleInfo(index, "C-1, Castle Woodhaven");
                case MM2Map.A2CastlePinehurst: return new MapTitleInfo(index, "A-2, Castle Pinehurst");
                case MM2Map.D2GreatLuxusPalaceRoyale: return new MapTitleInfo(index, "D-2, Great Luxus Palace Royale");
                case MM2Map.C2CastleXabran: return new MapTitleInfo(index, "C-2, Castle Xabran");
                default: return new MapTitleInfo(index, String.Format("UnknownMap({0})", index));
            }
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(60);
            foreach (MM2Map map in Enum.GetValues(typeof(MM2Map)))
            {
                if (map != MM2Map.Unknown)
                    maps.Add(new MapTitleInfo((int)map, GetMapName(map)));
            }
            return maps;
        }

        public override MapTitleInfo GetMapTitle(int index)
        {
            return MM2MemoryHacker.GetMapTitlePair(index);
        }

        public override bool SetExit(MMExit exit)
        {
            if (!IsValid)
                return false;

            if (exit == null)
                return false;

            byte[] map = new byte[1];
            byte[] point = new byte[1];
            map[0] = (byte) exit.Map;
            point[0] = (byte) (exit.Point.X | (exit.Point.Y << 4));

            switch (exit.Direction)
            {
                case MMExitDirection.North:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ExitNorth, map);
                    break;
                case MMExitDirection.East:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ExitEast, map);
                    break;
                case MMExitDirection.South:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ExitSouth, map);
                    break;
                case MMExitDirection.West:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ExitWest, map);
                    break;
                case MMExitDirection.Surface:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.SurfaceMap, map);
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.SurfaceCoord, point);
                    break;
                case MMExitDirection.Run:
                    WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.SafestSquare, point);
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

            byte spells = ReadByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ForbiddenSpells);

            spells = Global.UpdateFlag(spells, 0x80, flags.HasFlag(MapAttributeFlags.Darkness));
            spells = Global.UpdateFlag(spells, 0x10, !flags.HasFlag(MapAttributeFlags.AllowTeleport));
            spells = Global.UpdateFlag(spells, 0x20, !flags.HasFlag(MapAttributeFlags.AllowEtherealize));
            spells = Global.UpdateFlag(spells, 0x40, !flags.HasFlag(MapAttributeFlags.AllowSurface));

            return WriteByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.ForbiddenSpells, spells);
        }

        public override bool SetOutside(Point pt, bool bOutside)
        {
            MM2MapAttributes attrib = GetMapAttributes() as MM2MapAttributes;
            return attrib.SetOutside(pt, bOutside);
        }

        public override bool ToggleOutside()
        {
            if (!IsValid)
                return false;

            LocationInformation location = GetLocation();
            MM2MapAttributes attrib = GetMapAttributes() as MM2MapAttributes;
            attrib.SetOutside(location.PrimaryCoordinates, !attrib.IsOutside(location.PrimaryCoordinates));

            byte[] outdoor = new byte[32];
            Buffer.BlockCopy(attrib.Bytes, MM2MapAttributeOffsets.OutdoorFlags, outdoor, 0, 32);
            return WriteOffset(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.OutdoorFlags, outdoor);
        }

        public bool SetBenefitsUsed1(bool bUsed)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM2Memory.BenefitsUsed1, (byte)(bUsed ? 1 : 0));
        }

        public bool SetBenefitsUsed2(bool bUsed)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM2Memory.BenefitsUsed2, (byte)(bUsed ? 1 : 0));
        }

        public bool SetBattlesLost(UInt16 iLost)
        {
            if (!IsValid)
                return false;

            return WriteUInt16(MM2Memory.BattleStats + 2, iLost);
        }

        public bool SetBattlesWon(UInt16 iWon)
        {
            if (!IsValid)
                return false;

            return WriteUInt16(MM2Memory.BattleStats, iWon);
        }

        public override bool SetMonsterGroup(byte bGroup)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.MonsterGroup, bGroup);
        }

        public override bool SetEncounterSize(byte bSize)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.EncounterSize, bSize);
        }

        public override bool SetDepth(byte bDepth)
        {
            if (!IsValid)
                return false;

            return WriteByte(MM2Memory.MapGlobalAttributes + MM2MapAttributeOffsets.UndergroundLevel, bDepth);
        }


        public bool SetTimeInfo(MM2TimeInfo info)
        {
            if (!IsValid)
                return false;

            long pWritten;

            MemoryStream stream = new MemoryStream(40);
            for (int i = 0; i < info.EraDays.Length; i++)
            {
                byte[] days = BitConverter.GetBytes(info.EraDays[i]);
                stream.Write(days, 0, 2);
            }
            while (stream.Length < 20)
                stream.WriteByte(0);
            for (int i = 0; i < info.EraYears.Length; i++)
            {
                byte[] years = BitConverter.GetBytes(info.EraYears[i]);
                stream.Write(years, 0, 2);
            }
            while (stream.Length < 40)
                stream.WriteByte(0);

            byte[] bytes = stream.ToArray();
            WriteOffset(MM2Memory.DatesAndTimes, bytes, 40, out pWritten);

            bytes[0] = (byte)info.CurrentEra;
            WriteOffset(MM2Memory.CurrentEra, bytes, 1, out pWritten);

            bytes[0] = (byte)info.Steps;
            WriteOffset(MM2Memory.StepCounter, bytes, 1, out pWritten);

            return ((int)pWritten == 1);
        }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            IntDeck deck = new IntDeck(1, 255);

            List<Item> items = new List<Item>(6);
            byte[] bytes = new byte[3];
            for (int i = 0; i < 6; i++)
            {
                deck.Shuffle();
                Global.Rand.NextBytes(bytes);
                MMItem itemRand = null;

                foreach (int iIndex in deck.Cards)
                {
                    bytes[0] = (byte)iIndex;
                    if (bUsableOnly)
                        bytes[2] &= (byte) MM2BonusFlags.PlusFlags;
                    itemRand = MM2Item.FromBagBytes(bytes);
                    if (itemRand.MatchTypeAndChar(type, bUsableOnly ? baseChar : null))
                        break;
                }

                items.Add(itemRand);
            }
            SetBackpack(baseChar.BasicAddress, items);
        }

        public override int GetCurrentMapIndex()
        {
            if (!IsValid)
                return -1;

            return ReadByte(MM2Memory.CurrentMapIndex);
        }

        public override MapCartography GetCartography()
        {
            if (!IsValid)
                return null;

            MapCartography cart = new MM2MapCartography();

            cart.MapIndex = GetCurrentMapIndex();

            MemoryBytes bytes = ReadOffset(MM2Memory.CartographyData + (cart.MapIndex * 32), 32);
            if (bytes == null)
                return null;

            cart.SetBytes(bytes, new Size(16, 16));
            return cart;
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
                    for (int i = 0; i < bytes.Length; i++)
                        bytes[i] = 0xff;
                    break;
                default:
                    break;
            }

            switch (action)
            {
                case MapCartography.EditAction.FillAll:
                case MapCartography.EditAction.ClearAll:
                    for (int i = 0; i < 60; i++)
                        WriteOffset(MM2Memory.CartographyData + (32 * i), bytes);
                    return true;
                case MapCartography.EditAction.FillSingle:
                case MapCartography.EditAction.ClearSingle:
                    int iMap = GetCurrentMapIndex();
                    WriteOffset(MM2Memory.CartographyData + (32 * iMap), bytes);
                    return true;
            }
            return false;
        }

        public override Shops GetShopInfo()
        {
            Shops shops = new Shops();

            LocationInformation info = GetLocation();
            if (info == null)
                return shops;

            // Shops in MM2 are in fixed locations, so they are hard-coded
            // (because the "InShop" state applies to temples and whatnot as well)

            shops.Inventories = new List<ShopInventory>(5);

            MemoryBytes bytesShops = ReadOffset(MM2Memory.ShopItems, 246);
            if (bytesShops == null)
                return shops;

            MemoryStream ms = new MemoryStream(264);
            ms.Write(bytesShops, 0, bytesShops.Length);

            byte day = ReadByte(MM2Memory.CurrentDay);
            ms.WriteByte(day);

            MM2ShopInventory middlegate = new MM2ShopInventory(bytesShops, MM2Map.C2Middlegate, day);
            MM2ShopInventory atlantium = new MM2ShopInventory(bytesShops, MM2Map.A4Atlantium, day);
            MM2ShopInventory tundara = new MM2ShopInventory(bytesShops, MM2Map.A1Tundara, day);
            MM2ShopInventory vulcania = new MM2ShopInventory(bytesShops, MM2Map.E1Vulcania, day);
            MM2ShopInventory sandsobar = new MM2ShopInventory(bytesShops, MM2Map.E4Sandsobar, day);

            middlegate.Town = "Middlegate";
            atlantium.Town = "Atlantium";
            tundara.Town = "Tundara";
            vulcania.Town = "Vulcania";
            sandsobar.Town = "Sandsobar";

            shops.Inventories.Add(middlegate);
            shops.Inventories.Add(atlantium);
            shops.Inventories.Add(tundara);
            shops.Inventories.Add(vulcania);
            shops.Inventories.Add(sandsobar);

            MemoryBytes bytesCurrentItems = ReadOffset(MM2Memory.CurrentShopItems, 6);
            MemoryBytes bytesCurrentBonuses = ReadOffset(MM2Memory.CurrentShopBonuses, 6);
            MemoryBytes bytesCurrentCharges = ReadOffset(MM2Memory.CurrentShopCharges, 6);

            if (bytesCurrentItems == null || bytesCurrentBonuses == null || bytesCurrentCharges == null)
                return null;

            ms.Write(bytesCurrentItems, 0, bytesCurrentItems.Length);
            ms.Write(bytesCurrentBonuses, 0, bytesCurrentBonuses.Length);
            ms.Write(bytesCurrentCharges, 0, bytesCurrentCharges.Length);

            List<ShopItem> current = new List<ShopItem>(6);
            for(int i = 0; i < bytesCurrentItems.Length; i++)
            {
                if (bytesCurrentItems[i] == (int)MM2ItemIndex.Blank)
                    continue;

                MM2Item item = MM2.Items[bytesCurrentItems[i]].Clone() as MM2Item;
                item.m_iChargesCurrent = bytesCurrentCharges[i];
                item.BonusCurrent = (MM2BonusFlags) bytesCurrentBonuses[i];
                current.Add(new ShopItem(item, i, (int) MM2ShopInventory.ItemCategory.Current));
            }
            shops.CurrentDisplay = current;

            shops.InShop = false;

            switch ((MM2Map)info.MapIndex)
            {
                case MM2Map.C2Middlegate:
                    shops.InShop = (info.PrimaryCoordinates.X == 4 && info.PrimaryCoordinates.Y == 4);
                    break;
                case MM2Map.A4Atlantium:
                    shops.InShop = (info.PrimaryCoordinates.X == 6 && info.PrimaryCoordinates.Y == 13);
                    break;
                case MM2Map.A1Tundara:
                    shops.InShop = (info.PrimaryCoordinates.X == 11 && info.PrimaryCoordinates.Y == 10);
                    break;
                case MM2Map.E1Vulcania:
                    shops.InShop = (info.PrimaryCoordinates.X == 15 && info.PrimaryCoordinates.Y == 8);
                    break;
                case MM2Map.E4Sandsobar:
                    shops.InShop = (info.PrimaryCoordinates.X == 7 && info.PrimaryCoordinates.Y == 15);
                    break;
                default:
                    break;
            }

            shops.RawBytes = ms.ToArray();

            if (!shops.InShop)
                return shops;

            return shops;
        }

        public override bool SetShopItem(ShopItem item)
        {
            if (!(item.Item is MM2Item))
                return false;

            MM2Item mm2Item = item.Item as MM2Item;

            byte[] index = new byte[] { (byte)mm2Item.Index };
            byte[] charges = new byte[] { (byte)mm2Item.m_iChargesCurrent };
            byte[] bonus = new byte[] { (byte)mm2Item.BonusCurrent };

            switch ((MM2ShopInventory.ItemCategory) item.Multiplier)
            {
                case MM2ShopInventory.ItemCategory.Current:
                    WriteOffset(MM2Memory.CurrentShopItems + item.Offset, index);
                    WriteOffset(MM2Memory.CurrentShopCharges + item.Offset, charges);
                    WriteOffset(MM2Memory.CurrentShopBonuses + item.Offset, bonus);
                    break;
                case MM2ShopInventory.ItemCategory.Armor:
                case MM2ShopInventory.ItemCategory.Weapons:
                    WriteOffset(item.Offset, index);
                    WriteOffset(item.Offset + 30, bonus);
                    break;
                case MM2ShopInventory.ItemCategory.Misc:
                    WriteOffset(item.Offset, index);
                    WriteOffset(item.Offset + 30, charges);
                    break;
                default: // Bare item
                    WriteOffset(item.Offset, index);
                    break;
            }

            return true;
        }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            if (!IsValid)
                return null;

            MemoryBytes attributes = ReadOffset(MM2Memory.MapAttributes, 256);
            if (attributes == null)
                return null;

            return new MM2ActiveSquares(form, ReadByte(MM2Memory.CurrentMapIndex), attributes);
        }

        public override MemoryBytes GetScriptBytes()
        {
            return ReadOffset(MM2Memory.Scripts, 2304);
        }

        public List<ScriptString> GetScriptStrings(byte[] bytes)
        {
            if (bytes == null)
                return new List<ScriptString>();

            // Script bytes contain 3 segments if map-based, or two segments otherwise:

            // 1 (map based) - Script Headers, 3 bytes per header, terminated with [00 00 00]
            // 2 - Scripts, variables bytes per script, each script terminated with [ff], list of scripts terminated with [ff ff]
            // 3 - Strings, terminated with [ff], newlines encoded as '@', list of strings terminated with [ff ff]

            byte[] bytesEndHeaders = new byte[] { 0, 0, 0 };
            byte[] bytesEndLists = new byte[] { 0xff, 0xff };

            int i = 0;
            if (bytes.Length > 3 && bytes[2] != 0xff)
            {
                while (i < bytes.Length - 3)
                {
                    if (Global.CompareBytes(bytes, bytesEndHeaders, i, 0, bytesEndHeaders.Length))
                    {
                        i += 3;
                        break;
                    }
                    i++;
                }
            }
            while (i < bytes.Length - 2)
            {
                if (Global.CompareBytes(bytes, bytesEndLists, i, 0, bytesEndLists.Length))
                {
                    i += 2;
                    break;
                }
                i++;
            }

            List<ScriptString> strings = new List<ScriptString>();

            while (i < bytes.Length - 2)
            {
                MM2String mm2String = new MM2String();
                mm2String.SetFromBytes(bytes, ref i);
                if (mm2String.Valid)
                    strings.Add(mm2String);
                else
                    break;
            }

            return strings;
        }

        public override GameScripts GetScripts(MemoryBytes bytes)
        {
            return new MM2Scripts(bytes);
        }

        public override bool SetScriptLine(ScriptLine line)
        {
            if (!line.Bytes.ValidOffset)
                return false;

            return WriteOffset(line.Bytes);
        }

        public override List<ScriptString> GetScriptStrings()
        {
            return GetScriptStrings(GetScriptBytes());
        }

        public override List<ScriptString> GetScriptStrings(MemoryBytes mb)
        {
            return GetScriptStrings(mb);
        }

        public byte[] GetEncounterBytes()
        {
            if (!IsValid)
                return new byte[0];

            byte[] encounters = Properties.Resources.MM2MonsterDefaults;
            int iIndex = 0;

            byte bMap = ReadByte(MM2Memory.CurrentMapIndex);

            while (iIndex < encounters.Length - 1)
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

            MemoryBytes bytesAttrib = ReadOffset(MM2Memory.MapAttributes, 256);
            if (bytesAttrib == null)
                return false;

            for (int i = 0; i < encounters.Length; i++)
                bytesAttrib[encounters[i]] &= 0x7f;

            return WriteOffset(MM2Memory.MapAttributes, bytesAttrib);
        }

        public override bool ResetMonsters()
        {
            if (!IsValid)
                return false;

            // Set all upper bits from encounter squares for this map
            byte[] encounters = GetEncounterBytes();

            MemoryBytes bytesAttrib = ReadOffset(MM2Memory.MapAttributes, 256);
            if (bytesAttrib == null)
                return false;

            for (int i = 0; i < encounters.Length; i++)
                bytesAttrib[encounters[i]] |= 0x80;

            return WriteOffset(MM2Memory.MapAttributes, bytesAttrib);
        }

        public override AmountExplored GetExplored()
        {
            MemoryBytes bytes = ReadOffset(MM2Memory.CartographyData, 32 * 60);
            if (bytes == null)
                return new AmountExplored(0, 1);

            return new AmountExplored(Global.NumBitsSet(bytes), 256*60);
        }

        public override bool HasScripts { get { return true; } }
        public override RosterFile CurrentRoster { get { return null; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new MM2GameInformationControl(main); }

        public override string GetMapEnum(int index)
        {
            return String.Format("MM2Map.{0}", Enum.GetName(typeof(MM2Map), (MM2Map)(index)));
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * MM2Character.SizeInBytes;
            CharacterOffsets offsets = MM2.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + MM2Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            // Single-byte 255 values
            foreach (int lOffset in new int[] { 
                offsets.Might, offsets.MightMod,
                offsets.Intellect, offsets.IntellectMod,
                offsets.Personality, offsets.PersonalityMod, 
                offsets.Endurance, offsets.EnduranceMod,
                offsets.Speed, offsets.SpeedMod,
                offsets.Accuracy, offsets.AccuracyMod, 
                offsets.Luck, offsets.LuckMod, 
                offsets.FireResist, 
                offsets.ColdResist, 
                offsets.ElecResist, 
                offsets.AcidResist, 
                offsets.PoisonResist, 
                offsets.MagicResist, 
                offsets.EnergyResist, 
                offsets.SleepResist, 
                offsets.Level, offsets.LevelMod,
                offsets.ArmorClassMod, offsets.Food, offsets.Thievery})
                info.Bytes[offset + lOffset] = 255;

            // Two-byte 60000 values (not quite maximum, to avoid overflow issues):
            byte[] pbMax2 = BitConverter.GetBytes((UInt16)60000);
            foreach (int lOffset in new int[] { 
                offsets.CurrentHP, offsets.MaxHP, offsets.MaxHPMod,
                offsets.CurrentSP, offsets.MaxSP, offsets.Gems})
                Buffer.BlockCopy(pbMax2, 0, info.Bytes, offset + lOffset, pbMax2.Length);

            byte[] pbMaxGold = BitConverter.GetBytes((UInt32)900000000);
            Buffer.BlockCopy(pbMaxGold, 0, info.Bytes, offset + offsets.Gold, offsets.GoldLength);
            byte[] pbMaxXP = BitConverter.GetBytes((UInt32)1229376000);
            Buffer.BlockCopy(pbMaxXP, 0, info.Bytes, offset + offsets.Experience, pbMaxXP.Length);

            MM2Class mmClass = (MM2Class)info.Bytes[offset + offsets.Class];
            MM2AlignmentValue mmAlign = (MM2AlignmentValue)info.Bytes[offset + offsets.Alignment];

            byte[] allSpells = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            byte[] noSpells = new byte[] { 0, 0, 0, 0, 0, 0 };

            switch (mmClass)
            {
                case MM2Class.Knight:
                case MM2Class.Robber:
                case MM2Class.Barbarian:
                case MM2Class.Ninja:
                    info.Bytes[offset + offsets.SpellLevel] = 0;
                    info.Bytes[offset + offsets.SpellLevelMod] = 0;
                    Buffer.BlockCopy(noSpells, 0, info.Bytes, offset + offsets.Spells, offsets.SpellsLength);
                    break;
                default:
                    info.Bytes[offset + offsets.SpellLevel] = 9;
                    info.Bytes[offset + offsets.SpellLevelMod] = 9;
                    Buffer.BlockCopy(allSpells, 0, info.Bytes, offset + offsets.Spells, offsets.SpellsLength);
                    break;
            }

            Global.SetBytes(info.Bytes, offset + offsets.Condition, offsets.ConditionLength, 0);
            info.Bytes[offset + offsets.Age] = 18;

            info.Bytes[offset + offsets.Skills] = 0xBD;  // Mountaineer and Pathfinder

            WriteOffset(MM2Memory.PartyInfo, info.Bytes);

            List<Item> items = new List<Item>(6);

            MM2Item pike = MM2.Items[(int)MM2ItemIndex.TitansPike].Clone() as MM2Item;
            MM2Item naginata = MM2.Items[(int)MM2ItemIndex.SunNaginata].Clone() as MM2Item;
            MM2Item plate = MM2.Items[(int)MM2ItemIndex.GoldPlateMail].Clone() as MM2Item;
            MM2Item chain = MM2.Items[(int)MM2ItemIndex.GoldChainMail].Clone() as MM2Item;
            MM2Item ring = MM2.Items[(int)MM2ItemIndex.GoldRingMail].Clone() as MM2Item;
            MM2Item scale = MM2.Items[(int)MM2ItemIndex.GoldScaleMail].Clone() as MM2Item;
            MM2Item bow = MM2.Items[(int)MM2ItemIndex.AncientBow].Clone() as MM2Item;
            MM2Item sling = MM2.Items[(int)MM2ItemIndex.GiantSling].Clone() as MM2Item;
            MM2Item pipe = MM2.Items[(int)MM2ItemIndex.ShamanPipe].Clone() as MM2Item;
            MM2Item cloak = MM2.Items[(int)MM2ItemIndex.Invisocloak].Clone() as MM2Item;
            MM2Item blade = MM2.Items[(int)MM2ItemIndex.EnergyBlade].Clone() as MM2Item;
            MM2Item shield = MM2.Items[(int)MM2ItemIndex.GoldShield].Clone() as MM2Item;
            MM2Item helm = MM2.Items[(int)MM2ItemIndex.GoldHelm].Clone() as MM2Item;
            MM2Item staff = MM2.Items[(int)MM2ItemIndex.WizardStaff].Clone() as MM2Item;
            MM2Item padded = MM2.Items[(int)MM2ItemIndex.PaddedArmor].Clone() as MM2Item;
            MM2Item hammer = MM2.Items[(int)MM2ItemIndex.StoneHammer].Clone() as MM2Item;

            foreach (MM2Item item in new MM2Item[] { pike, plate, chain, scale, naginata, ring, bow, sling, pipe, cloak, blade, shield, helm, staff, padded, hammer })
            {
                item.m_iChargesCurrent = 255;
                item.BonusCurrent = (MM2BonusFlags) 63;
            }

            switch (mmClass)
            {
                case MM2Class.Knight:
                    items.Add(pike);
                    items.Add(plate);
                    items.Add(bow);
                    items.Add(helm);
                    break;
                case MM2Class.Paladin:
                    items.Add(pike);
                    items.Add(plate);
                    items.Add(bow);
                    items.Add(helm);
                    break;
                case MM2Class.Archer:
                    items.Add(pike);
                    items.Add(chain);
                    items.Add(bow);
                    break;
                case MM2Class.Barbarian:
                    items.Add(pike);
                    items.Add(scale);
                    items.Add(sling);
                    items.Add(helm);
                    break;
                case MM2Class.Ninja:
                    items.Add(naginata);
                    items.Add(ring);
                    items.Add(sling);
                    break;
                case MM2Class.Robber:
                    items.Add(blade);
                    items.Add(shield);
                    items.Add(sling);
                    break;
                case MM2Class.Cleric:
                    items.Add(hammer);
                    items.Add(chain);
                    items.Add(helm);
                    break;
                case MM2Class.Sorcerer:
                    items.Add(staff);
                    items.Add(padded);
                    items.Add(pipe);
                    break;
                default:
                    break;
            }

            items.Add(cloak);
            items.Add(cloak);

            SetBackpack(iAddress, items);

            return true;
        }

        public override bool HasCartography { get { return true; } }

        public override bool IsExplored(int x, int y)
        {
            // Should be faster than GetCartography().IsBitSet() for a single square

            if (!IsValid)
                return true;

            byte bCartography = ReadByte(MM2Memory.CartographyData + (GetCurrentMapIndex() * 32) + (y * 2) + (1-(x / 8)));

            return (bCartography >> (7 - ((x % 8))) & 1) == 1;
        }

        public override bool ToggleCartography(Point pt, Toggle toggle)
        {
            if (!IsValid)
                return false;

            int offset = MM2Memory.CartographyData + (GetCurrentMapIndex() * 32);
            int iDelta = (pt.Y * 2) + (1-(pt.X / 8));

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
            return ((long) ReadUInt16(MM2Memory.DatesAndTimes + 36) << 24) + ((long) ReadUInt16(MM2Memory.DatesAndTimes + 16) << 8) + ReadByte(MM2Memory.StepCounter);
        }

        public override List<BaseCharacter> GetCharacters()
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
                chars.Add(MM2Character.Create(pi.Bytes, MM2Character.SizeInBytes * i));

            return chars;
        }

        public override bool IsSurface(int iMap) { return IsSurfaceMap(iMap); }

        public static bool IsSurfaceMap(int iMap)
        {
            switch ((MM2Map)iMap)
            {
                case MM2Map.A1Surface:
                case MM2Map.A2Surface:
                case MM2Map.A3Surface:
                case MM2Map.A4Surface:
                case MM2Map.B1Surface:
                case MM2Map.B2Surface:
                case MM2Map.B3Surface:
                case MM2Map.B4Surface:
                case MM2Map.C1Surface:
                case MM2Map.C2Surface:
                case MM2Map.C3Surface:
                case MM2Map.C4Surface:
                case MM2Map.D1Surface:
                case MM2Map.D2Surface:
                case MM2Map.D3Surface:
                case MM2Map.D4Surface:
                case MM2Map.E1Surface:
                case MM2Map.E2Surface:
                case MM2Map.E3Surface:
                case MM2Map.E4Surface: 
                    return true;
                default:
                    return false;
            }
        }

        public override Point GetSurfaceSector(int iMap)
        {
            switch ((MM2Map)iMap)
            {
                case MM2Map.A1Surface: return new Point(0, 3);
                case MM2Map.A2Surface: return new Point(0, 2);
                case MM2Map.A3Surface: return new Point(0, 1);
                case MM2Map.A4Surface: return new Point(0, 0);
                case MM2Map.B1Surface: return new Point(1, 3);
                case MM2Map.B2Surface: return new Point(1, 2);
                case MM2Map.B3Surface: return new Point(1, 1);
                case MM2Map.B4Surface: return new Point(1, 0);
                case MM2Map.C1Surface: return new Point(2, 3);
                case MM2Map.C2Surface: return new Point(2, 2);
                case MM2Map.C3Surface: return new Point(2, 1);
                case MM2Map.C4Surface: return new Point(2, 0);
                case MM2Map.D1Surface: return new Point(3, 3);
                case MM2Map.D2Surface: return new Point(3, 2);
                case MM2Map.D3Surface: return new Point(3, 1);
                case MM2Map.D4Surface: return new Point(3, 0);
                case MM2Map.E1Surface: return new Point(4, 3);
                case MM2Map.E2Surface: return new Point(4, 2);
                case MM2Map.E3Surface: return new Point(4, 1);
                case MM2Map.E4Surface: return new Point(4, 0);
                default: return new Point(-1, -1);
            }
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                MM2GameState state = ReadMM2GameState();
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Opening:
                        case MainState.Opening2:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Space }, true);
                            TweakSleep(100);
                            break;
                        case MainState.Options:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.S }, true);
                            TweakSleep(100);
                            break;
                        case MainState.MainMenu:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.G }, true);
                            TweakSleep(200);
                            break;
                        case MainState.GoToTown:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Z }, true);
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
            return MM2.Monsters;
        }

        public override MapBytes GetCurrentMapBytes()
        {
            GameState state = GetGameState();
            switch (state.Main)
            {
                case MainState.Adventuring:
                    return new MapBytes(ReadOffset(MM2Memory.MapAppearance, 512), 16, 16);
                default:
                    return null;
            }
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null || mb.Bytes.Length < 512)
                return null;
            MMMapData data = new MM2MapData();
            data.LiveOnly = true;
            data.Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            data.Appearance = mb.Bytes;
            data.Attributes = Global.Subset(mb.Bytes, 256, 256);
            return data;
        }
    }
}
