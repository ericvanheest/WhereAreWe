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
    public class Ultima1Memory : UltimaMemory
    {
        // Search for "Attempting res"
        // public override byte[] MainSearch { get { return new byte[] { 0x41, 0x74, 0x74, 0x65, 0x6D, 0x70, 0x74, 0x69, 0x6E, 0x67, 0x20, 0x72, 0x65, 0x73 }; } }
        // Search for "OMPT=$P$G.BLAS"
        // public override byte[] MainSearch { get { return new byte[] { 0x4F, 0x4D, 0x50, 0x54, 0x3D, 0x24, 0x50, 0x24, 0x47, 0x00, 0x42, 0x4C, 0x41, 0x53 }; } }
        // Search for "C:\ULTIMA.EXE"
        public override byte[] MainSearch { get { return new byte[] { 0x43, 0x3A, 0x5C, 0x55, 0x4C, 0x54, 0x49, 0x4D, 0x41, 0x2E, 0x45, 0x58, 0x45 }; } }
        const int MainSearchOffset = 0;

        //public override int MainBlockSVN => -76106;  // "Attempting res"
        //public override int MainBlockSVN => -76194;  // "OMPT=$P$G.BLAS"
        public override int MainBlockSVN => -6386;  // "C:\ULTIMA.EXE"
        public override int MainBlockOldSVN => 0;
        public override int MainBlockNonSVN => 0;
        public override int PartyInfo => MainSearchOffset + 69830;

        public override int MapOverworld => MainSearchOffset + 70650;
        public override int Map => MainSearchOffset + 99236;
        public override int DungeonLevel => MainSearchOffset + 99230;
        public override int LocationXYOverworld => MainSearchOffset + 69882;
        public override int LocationXY => MainSearchOffset + 99212;
        public override int Facing => MainSearchOffset + 99232;
        public override int MainMapIndex => MainSearchOffset + 90600;
        public override int MonstersOverworld => MainSearchOffset + 70010;
        public override int TownNPCs => MainSearchOffset + 99092;

        public override int GemNames => MainSearchOffset - 73892;
        public override int WeaponNames => MainSearchOffset - 74596;
        public override int ArmorNames => MainSearchOffset - 74044;
        public override int SpellNames => MainSearchOffset - 74154;
        public override int VehicleNames => MainSearchOffset - 73966;

        public override int CityMap1 => MainSearchOffset + 91988;   // Eight towns 0x2AC bytes
        public override int CastleMap1 => MainSearchOffset + 90620;   // Two castles, 0x2AC bytes

        public override int MonsterCount => MainSearchOffset + 90612;

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Ultima1]; } }

        public Ultima1Memory()
        {
        }
    }

    public enum Ultima1Map
    {
        Legend = -1,
        Overworld = 0,
        Britian = 1,
        Moon = 2,
        Fawn = 3,
        Paws = 4,
        Montor = 5,
        Yew = 6,
        Tune = 7,
        Grey = 8,
        Arnold = 9,
        Linda = 10,
        Helen = 11,
        Owen = 12,
        John = 13,
        Gerry = 14,
        Wolf = 15,
        TheSnake = 16,
        Nassau = 17,
        ClearLagoon = 18,
        Stout = 19,
        Gauntlet = 20,
        Imagination = 21,
        Ponder = 22,
        Wealth = 23,
        Poor = 24,
        Gorlab = 25,
        Dextron = 26,
        Magic = 27,
        Wheeler = 28,
        Bulldozer = 29,
        TheBrother = 30,
        Turtle = 31,
        LostFriends = 32,
        TheCastleOfLordBritish = 33,
        TheCastleOfTheLostKing = 34,
        TheCastleBarataria = 35,
        TheCastleRondorin = 36,
        TheCastleOfOlympus = 37,
        TheBlackDragonsCastle = 38,
        TheWhiteDragonsCastle = 39,
        TheCastleOfShamino = 40,
        ThePillarsOfProtection = 41,
        TheTowerOfKnowledge = 42,
        ThePillarsOfTheArgonauts = 43,
        ThePillarOfOzymandias = 44,
        TheSignPost = 45,
        TheSouthernSignPost = 46,
        TheEasternSignPost = 47,
        TheGraveOfTheLostSoul = 48,
        TheUnholyHole = 49,
        TheDungeonOfPerinia = 50,
        TheDungeonOfMontor = 51,
        TheMinesOfMtDrash = 52,
        MondainsGateToHell = 53,
        TheLostCaverns = 54,
        TheDungeonOfDoubt = 55,
        TheMinesOfMtDrashII = 56,
        DeathsAwakening = 57,
        TheSavagePlace = 58,
        ScorpionHole = 59,
        AdvarisHole = 60,
        TheDeadWarriorsFight = 61,
        TheHorrorOfTheHarpies = 62,
        TheLabyrinth = 63,
        WhereHerculesDied = 64,
        TheHorrorOfTheHarpiesII = 65,
        TheGorgonHole = 66,
        TheTrampOfDoom = 67,
        TheVipersPit = 68,
        TheLongDeath = 69,
        TheEnd = 70,
        TheVipersPitII = 71,
        TheSlowDeath = 72,
        TheGuildOfDeath = 73,
        TheMetalTwister = 74,
        TheTrollsHole = 75,
        TheSkullSmasher = 76,
        TheSpineBreaker = 77,
        TheDungeonOfDoom = 78,
        TheDeadCatsLife = 79,
        TheMorbidAdventure = 80,
        FreeDeathHole = 81,
        DeadMansWalk = 82,
        TheDeadCatsLifeII = 83,
        TheHoleToHades = 84,
        Last
    }

    public class Ultima1GameInfo : UltimaGameInfo
    {
        public override GameNames Game => GameNames.Ultima1;
    }

    public class Ultima1MapData : UltimaMapData
    {
        public Ultima1Map MainMap
        {
            get
            {
                if (Index == 0)
                    return Ultima1Map.Overworld;
                if (Index > 255)
                    return (Ultima1Map)(Index & 0xff);
                return (Ultima1Map)Index;
            }
        }
        public Ultima1MapData(MapBytes mb, int iMapIndex)
        {
            RawBytes = mb;
            Index = iMapIndex;
            Title = Ultima1MemoryHacker.GetMapTitlePair(iMapIndex);
            Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
        }

        public override UltimaOutdoorTile GetOverworldValue(int horiz, int vert)
        {
            int iOffset = (vert * 84) + (horiz / 2);
            if (iOffset >= RawBytes.Bytes.Length || iOffset < 0 || horiz < 0 || vert < 0 || horiz >= Width || vert >= Height)
                return UltimaOutdoorTile.Border;
            byte bMap = RawBytes.Bytes[iOffset];
            if (horiz % 2 == 0)
                return (UltimaOutdoorTile)((bMap & 0xf0) >> 4);
            return (UltimaOutdoorTile)(bMap & 0xf);
        }

        public override UltimaIndoorTile GetUnderworldValue(int horiz, int vert)
        {
            int iOffset = (vert * 8) + (horiz * 88);
            if (iOffset < 0 || iOffset >= RawBytes.Bytes.Length || horiz < 0 || vert < 0 || horiz >= Width || vert >= Height)
                return UltimaIndoorTile.Solid;
            return (UltimaIndoorTile)RawBytes.Bytes[iOffset];
        }

        public override UltimaTownTile GetTownValue(int horiz, int vert)
        {
            int iOffset = vert + (horiz * 18);
            if (iOffset < 0 || iOffset >= RawBytes.Bytes.Length || horiz < 0 || vert < 0 || horiz >= Width || vert >= Height)
                return UltimaTownTile.Border;
            return (UltimaTownTile)RawBytes.Bytes[iOffset];
        }
    }

    public class Ultima1MonsterList : Ultima123MonsterList
    {
        public Ultima1MonsterList()
        {
            InitInternalList();
        }

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false) => false;
        public override List<UltimaMonster> SetFromBytes(byte[] bytes) => new List<UltimaMonster>();
    }

    public class Ultima1ItemList : Ultima123ItemList
    {
        public Ultima1ItemList()
        {
            InitInternalList();
        }

        public override bool InitInternalList()
        {
            Items = ItemList();
            return true;
        }
    }

    public class Ultima1MemoryHacker : UltimaMemoryHacker
    {
        private MonsterLocations m_lastMonsterLocations = null;
        private byte[] m_lastMonsterBytes = null;
        private Point m_lastMonsterParty;
        private ItemLocations m_lastItemLocations = null;
        public override UltimaMemory Memory => Ultima1.Memory;
        private const int DungeonSquareSize = 8;
        private const int DungeonWidth = 11;
        private const int DungeonHeight = 11;
        private const int OverworldWidth = 168;
        private const int OverworldHeight = 156;
        private const int CityWidth = 38;
        private const int CityHeight = 18;

        public Ultima1MemoryHacker()
        {
            m_game = GameNames.Ultima1;
        }

        protected override UltimaGameState GetMainState()
        {
            UltimaGameState state = new UltimaGameState();

            state.Main = MainState.Adventuring;
            return state;
        }

        public static string MapName(Ultima1Map map)
        {
            switch (map)
            {
                case Ultima1Map.Overworld: return "Sosaria";
                case Ultima1Map.Britian: return "Britian";
                case Ultima1Map.Moon: return "Moon";
                case Ultima1Map.Fawn: return "Fawn";
                case Ultima1Map.Paws: return "Paws";
                case Ultima1Map.Montor: return "Montor";
                case Ultima1Map.Yew: return "Yew";
                case Ultima1Map.Tune: return "Tune";
                case Ultima1Map.Grey: return "Grey";
                case Ultima1Map.Arnold: return "Arnold";
                case Ultima1Map.Linda: return "Linda";
                case Ultima1Map.Helen: return "Helen";
                case Ultima1Map.Owen: return "Owen";
                case Ultima1Map.John: return "John";
                case Ultima1Map.Gerry: return "Gerry";
                case Ultima1Map.Wolf: return "Wolf";
                case Ultima1Map.TheSnake: return "The Snake";
                case Ultima1Map.Nassau: return "Nassau";
                case Ultima1Map.ClearLagoon: return "Clear Lagoon";
                case Ultima1Map.Stout: return "Stout";
                case Ultima1Map.Gauntlet: return "Gauntlet";
                case Ultima1Map.Imagination: return "Imagination";
                case Ultima1Map.Ponder: return "Ponder";
                case Ultima1Map.Wealth: return "Wealth";
                case Ultima1Map.Poor: return "Poor";
                case Ultima1Map.Gorlab: return "Gorlab";
                case Ultima1Map.Dextron: return "Dextron";
                case Ultima1Map.Magic: return "Magic";
                case Ultima1Map.Wheeler: return "Wheeler";
                case Ultima1Map.Bulldozer: return "Bulldozer";
                case Ultima1Map.TheBrother: return "The Brother";
                case Ultima1Map.Turtle: return "Turtle";
                case Ultima1Map.LostFriends: return "Lost Friends";
                case Ultima1Map.TheCastleOfLordBritish: return "The Castle of Lord British";
                case Ultima1Map.TheCastleOfTheLostKing: return "The Castle of the Lost King";
                case Ultima1Map.TheCastleBarataria: return "The Castle Barataria";
                case Ultima1Map.TheCastleRondorin: return "The Castle Rondorin";
                case Ultima1Map.TheCastleOfOlympus: return "The Castle of Olympus";
                case Ultima1Map.TheBlackDragonsCastle: return "The Black Dragon's Castle";
                case Ultima1Map.TheWhiteDragonsCastle: return "The White Dragon's Castle";
                case Ultima1Map.TheCastleOfShamino: return "The Castle of Shamino";
                case Ultima1Map.ThePillarsOfProtection: return "The Pillars of Protection";
                case Ultima1Map.TheTowerOfKnowledge: return "The Tower of Knowledge";
                case Ultima1Map.ThePillarsOfTheArgonauts: return "The Pillars of the Argonauts";
                case Ultima1Map.ThePillarOfOzymandias: return "The Pillar of Ozymandias";
                case Ultima1Map.TheSignPost: return "The Sign Post";
                case Ultima1Map.TheSouthernSignPost: return "The Southern Sign Post";
                case Ultima1Map.TheEasternSignPost: return "The Eastern Sign Post";
                case Ultima1Map.TheGraveOfTheLostSoul: return "The Grave of the Lost Soul";
                case Ultima1Map.TheUnholyHole: return "The Unholy Hole";
                case Ultima1Map.TheDungeonOfPerinia: return "The Dungeon of Perinia";
                case Ultima1Map.TheDungeonOfMontor: return "The Dungeon of Montor";
                case Ultima1Map.TheMinesOfMtDrash: return "The Mines of Mt. Drash";
                case Ultima1Map.MondainsGateToHell: return "Mondain's Gate to Hell";
                case Ultima1Map.TheLostCaverns: return "The Lost Caverns";
                case Ultima1Map.TheDungeonOfDoubt: return "The Dungeon of Doubt";
                case Ultima1Map.TheMinesOfMtDrashII: return "The Mines of Mt. Drash II";
                case Ultima1Map.DeathsAwakening: return "Death's Awakening";
                case Ultima1Map.TheSavagePlace: return "The Savage Place";
                case Ultima1Map.ScorpionHole: return "Scorpion Hole";
                case Ultima1Map.AdvarisHole: return "Advari's Hole";
                case Ultima1Map.TheDeadWarriorsFight: return "The Dead Warrior's Fight";
                case Ultima1Map.TheHorrorOfTheHarpies: return "The Horror of the Harpies";
                case Ultima1Map.TheLabyrinth: return "The Labyrinth";
                case Ultima1Map.WhereHerculesDied: return "Where Hercules Died";
                case Ultima1Map.TheHorrorOfTheHarpiesII: return "The Horror of the Harpies II";
                case Ultima1Map.TheGorgonHole: return "The Gorgon Hole";
                case Ultima1Map.TheTrampOfDoom: return "The Tramp of Doom";
                case Ultima1Map.TheVipersPit: return "The Viper's Pit";
                case Ultima1Map.TheLongDeath: return "The Long Death";
                case Ultima1Map.TheEnd: return "The End...";
                case Ultima1Map.TheVipersPitII: return "The Viper's Pit II";
                case Ultima1Map.TheSlowDeath: return "The Slow Death";
                case Ultima1Map.TheGuildOfDeath: return "The Guild of Death";
                case Ultima1Map.TheMetalTwister: return "The Metal Twister";
                case Ultima1Map.TheTrollsHole: return "The Troll's Hole";
                case Ultima1Map.TheSkullSmasher: return "The Skull Smasher";
                case Ultima1Map.TheSpineBreaker: return "The Spine Breaker";
                case Ultima1Map.TheDungeonOfDoom: return "The Dungeon of Doom";
                case Ultima1Map.TheDeadCatsLife: return "The Dead Cat's Life";
                case Ultima1Map.TheMorbidAdventure: return "The Morbid Adventure";
                case Ultima1Map.FreeDeathHole: return "Free Death Hole";
                case Ultima1Map.DeadMansWalk: return "Dead Man's Walk";
                case Ultima1Map.TheDeadCatsLifeII: return "The Dead Cat's Life II";
                case Ultima1Map.TheHoleToHades: return "The Hole to Hades";
                default: return String.Format("Unknown({0})", (int)map);
            }
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            if (index == -1)
                return new MapTitleInfo(index, "Legend", "");

            int iCity = index & 0xff;
            Ultima1Map um = (Ultima1Map)index;
            if (index > 255)
                um = (Ultima1Map)iCity;
            switch (um)
            {
                case Ultima1Map.Overworld: return new MapTitleInfo(index, "Sosaria", "");
                default:
                    if (index < 1)
                        return new MapTitleInfo(index, MapName(um), "");
                    if (um >= Ultima1Map.Britian && um <= Ultima1Map.LostFriends)
                        return new MapTitleInfo(index, "City of " + MapName(um), "\\Cities");
                    if (um >= Ultima1Map.TheCastleOfLordBritish && um <= Ultima1Map.TheCastleOfShamino)
                        return new MapTitleInfo(index, MapName(um), "\\Castles");
                    return new MapTitleInfo(index, String.Format("{0}, Level {1}", MapName(um), index >> 8), "\\Dungeons");
            }
        }

        public override GameInfo GetGameInfo()
        {
            Ultima1GameInfo info = new Ultima1GameInfo();
            info.Location = GetLocation();
            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadUltima1PartyInfo();
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(Memory.PartyInfo + (iAddress * Ultima1Character.SizeInBytes), bytes);
        }

        private UltimaPartyInfo ReadUltima1PartyInfo()
        {
            if (m_block == null)
                return null;

            UltimaGameState state = GetGameState() as UltimaGameState;
            if (state == null)
                return null;

            MemoryBytes bytesParty = ReadOffset(Memory.PartyInfo, Ultima1Character.SizeInMemory);
            if (bytesParty == null)
                return null;
            if (BitConverter.ToUInt32(bytesParty.Bytes, 26) == 0x555c3a43)  // "C:\U"
                return null;  // Still on title screen
            if (BitConverter.ToUInt16(bytesParty.Bytes, 84) != 0xffff)
                return null;  // Inventory item "skin" should always be -1; if it's not, the game is probably in a non-playable state

            UltimaPartyInfo info = new UltimaPartyInfo(ReadOffset(Memory.PartyInfo, Ultima1Character.SizeInMemory));

            info.State = state;

            info.ActingCaster = 0;
            info.ActingCombatChar = 0;
            info.InspectingCombatChar = 0;

            return info;
        }

        private Direction FacingForByte(byte b)
        {
            switch (b)
            {
                case 1: return Direction.Left;
                case 2: return Direction.Right;
                case 3: return Direction.Up;
                case 4: return Direction.Down;
                default: return Direction.None;
            }
        }

        private Point GetOverworldLocation()
        {
            MemoryBytes mbOver = ReadOffset(Memory.LocationXYOverworld, 4);
            if (mbOver == null)
                return Global.NullPoint;
            return new Point(BitConverter.ToInt16(mbOver.Bytes, 0), BitConverter.ToInt16(mbOver.Bytes, 2));
        }

        public override LocationInformation GetLocation()
        {
            Point ptOverworld = GetOverworldLocation();
            int iMap = GetCurrentMapIndex();
            LocationInformation info = new LocationInformation(ptOverworld);
            info.MapIndex = iMap;
            info.NumChars = 1;
            if (iMap == 0)
                return info;

            MemoryBytes mbUnder = ReadOffset(Memory.LocationXY, 4);
            Point ptUnderworld = new Point(BitConverter.ToInt16(mbUnder.Bytes, 0), BitConverter.ToInt16(mbUnder.Bytes, 2));
            info.PrimaryCoordinates = ptUnderworld;
            info.SecondaryCoordinates = ptOverworld;
            info.Facing = FacingForByte(ReadByte(Memory.Facing));
            return info;
        }

        public override bool SetLocation(Point ptLocation)
        {
            int iMap = GetCurrentMapIndex();
            byte[] bytes = Global.NullBytes(4);
            Global.SetInt16(bytes, 0, ptLocation.X);
            Global.SetInt16(bytes, 2, ptLocation.Y);
            if (iMap == 0)
                return WriteOffset(Memory.LocationXYOverworld, bytes);
            return WriteOffset(Memory.LocationXY, bytes);
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            int iCurrentIndex = GetCurrentMapIndex();
            Ultima1MapData data = new Ultima1MapData(GetCurrentMapBytes(), iCurrentIndex);

            if (IsDungeon(iCurrentIndex))
                data.AlwaysLive = true; // The dungeons are randomized so they need a live update during gameplay
            else
                data.Directionless = true; // The overworld character is not "facing" any particular direction

            data.OverworldLocation = OverworldPointFromMap(data.MainMap);
            data.Title = GetMapTitlePair(data.Index);
            data.Bounds = new Rectangle(0, 0, data.Width, data.Height);

            return data;
        }

        public override int GetCurrentMapIndex()
        {
            byte bMain = ReadByte(Memory.MainMapIndex);
            if (bMain == 0)
                return bMain;
            Ultima1Map map = MapFromOverworldPoint(GetOverworldLocation());
            return ((int) map) | (ReadByte(Memory.DungeonLevel) << 8);
        }

        public override MapBytes GetCurrentMapBytes()
        {
            int iIndex = GetCurrentMapIndex();
            int iCity = iIndex & 0xff;
            switch (iIndex)
            {
                case 0: return new MapBytes(ReadOffset(Memory.MapOverworld, OverworldWidth / 2 * OverworldHeight).Bytes, OverworldWidth, OverworldHeight);
                default:
                    if (iCity < (int)Ultima1Map.TheCastleOfLordBritish)
                        return new MapBytes(ReadOffset(Memory.CityMap1 + (CityWidth * CityHeight * ((iCity - (int)Ultima1Map.Britian) % 8)), CityHeight * CityWidth).Bytes, CityWidth, CityHeight);
                    if (iCity < (int)Ultima1Map.ThePillarsOfProtection)
                        return new MapBytes(ReadOffset(Memory.CastleMap1 + (CityWidth * CityHeight * ((iCity-(int)Ultima1Map.TheCastleOfLordBritish)%2)), CityWidth * CityHeight).Bytes, CityWidth, CityHeight);
                    return new MapBytes(ReadOffset(Memory.Map, DungeonSquareSize * DungeonWidth * DungeonHeight).Bytes, DungeonWidth, DungeonHeight);
            }
        }

        public override Point GetPartyPosition()
        {
            Point ptOverworld = GetOverworldLocation();
            int iMap = GetCurrentMapIndex();
            if (iMap == 0)
                return ptOverworld;

            MemoryBytes mbUnder = ReadOffset(Memory.LocationXY, 4);
            return new Point(BitConverter.ToInt16(mbUnder.Bytes, 0), BitConverter.ToInt16(mbUnder.Bytes, 2));
        }

        public override MonsterLocations GetMonsterLocations(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            int iMap = GetCurrentMapIndex();
            UltimaMapType mapType = GetMapType(iMap);
            if (IsDungeon(iMap))
                return GetDungeonMonsterLocations(bForceNew);
            if (iMap == 0)
                return GetOverworldMonsterLocations(bForceNew);
            return GetTownMonsterLocations(mapType, bForceNew);
        }

        private MonsterLocations GetDungeonMonsterLocations(bool bForceNew)
        {
            if (!IsValid)
                return null;

            MemoryBytes bytes = ReadOffset(Memory.Map, 88 * 11);
            if (bytes == null)
                return null;

            Point ptParty = GetPartyPosition();

            if (!bForceNew &&
                m_lastMonsterLocations != null &&
                Global.Compare(bytes, m_lastMonsterBytes) &&
                ptParty == m_lastMonsterParty)
                return m_lastMonsterLocations;

            m_lastMonsterBytes = bytes;
            m_lastMonsterParty = ptParty;

            m_lastMonsterLocations = new MonsterLocations(ptParty);
            int iEncounterIndex = 0;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    int iOffset = (j * 88) + (i * 8);
                    int iMonster = BitConverter.ToInt16(bytes.Bytes, iOffset + 2);
                    if (iMonster != -1)
                        m_lastMonsterLocations.AddUltimaDungeonMonster(Game, iEncounterIndex++, new Point(j, i), bytes, iOffset);
                }
            }
            m_lastMonsterLocations.RawBytes = bytes.Bytes;
            return m_lastMonsterLocations;
        }

        private MonsterLocations GetOverworldMonsterLocations(bool bForceNew)
        {
            int iNumMonsters = ReadInt16(Memory.MonsterCount);
            if (iNumMonsters < 0 || iNumMonsters > 39)
                iNumMonsters = 39;
            iNumMonsters++; // Player is always present

            MemoryBytes bytes = ReadOffset(Memory.MonstersOverworld, iNumMonsters * 16);

            if (bytes == null)
                return null;

            Point ptParty = GetPartyPosition();

            if (!bForceNew &&
                m_lastMonsterLocations != null &&
                Global.Compare(bytes, m_lastMonsterBytes) &&
                ptParty == m_lastMonsterParty)
                return m_lastMonsterLocations;

            m_lastMonsterBytes = bytes;
            m_lastMonsterParty = ptParty;

            m_lastMonsterLocations= new MonsterLocations(ptParty);
            for (int i = 0; i < iNumMonsters; i++)
            {
                if (!IsFriendlySprite(bytes[i * 16]) && bytes[i * 16] != 0)
                    m_lastMonsterLocations.AddUltimaMonster(Game, i, bytes, i * 16);
            }
            m_lastMonsterLocations.RawBytes = bytes.Bytes;
            return m_lastMonsterLocations;
        }

        private MonsterLocations GetTownMonsterLocations(UltimaMapType mapType, bool bForceNew)
        {
            byte numMonsters = 18;

            MemoryBytes bytes = ReadOffset(Memory.TownNPCs, numMonsters * 8);

            if (bytes == null)
                return null;

            Point ptParty = GetPartyPosition();

            if (!bForceNew &&
                m_lastMonsterLocations != null &&
                Global.Compare(bytes, m_lastMonsterBytes) &&
                ptParty == m_lastMonsterParty)
                return m_lastMonsterLocations;

            m_lastMonsterBytes = bytes;
            m_lastMonsterParty = ptParty;

            m_lastMonsterLocations = new MonsterLocations(ptParty);
            for (int i = 0; i < numMonsters; i++)
                m_lastMonsterLocations.AddUltimaNPC(Game, mapType, i, bytes, i * 8);
            m_lastMonsterLocations.RawBytes = bytes.Bytes;
            return m_lastMonsterLocations;
        }

        private bool IsFriendlySprite(int i) => i < 19 || i > 47;

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            UltimaEncounterInfo info = new UltimaEncounterInfo();
            if (info == null)
                return null;

            MonsterLocations locations = GetMonsterLocations(bForceNew);
            if (locations == null)
                return null;

            info.Party = new PartyInfo();
            info.SetMonsters(locations.Monsters);

            MemoryStream stream = new MemoryStream();
            stream.Write(locations.RawBytes, 0, locations.RawBytes.Length);
            info.PartyLocation = GetPartyPosition();
            stream.WriteByte((byte)info.PartyLocation.X);
            stream.WriteByte((byte)info.PartyLocation.Y);
            info.AllBytes = stream.ToArray();
            stream.Close();

            return info;
        }

        public override MapBytes GetCurrentMapBytesLive()
        {
            MapBytes mb = GetCurrentMapBytes();

            // Zero out anything that's not "walls" (monsters, chests, coffins, etc)
            for (int iY = 0; iY < mb.Size.Height; iY++)
            {
                for (int iX = 0; iX < mb.Size.Width; iX++)
                {
                    for (int i = 1; i < 8; i++)
                        mb.Bytes[iX * 88 + iY * 8 + i] = 0;
                }
            }

            return mb;
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            Ultima1MapData data = new Ultima1MapData(mb, GetCurrentMapIndex());
            data.LiveOnly = true;
            return data;
        }

        public override string GetMapEnum(int index)
        {
            switch (GetMapType(index))
            {
                case UltimaMapType.Overworld:
                    return String.Format("Ultima1Map.{0}", Enum.GetName(typeof(Ultima1Map), (Ultima1Map)(index)));
                case UltimaMapType.Castle:
                case UltimaMapType.City:
                    return String.Format("Ultima1Map.{0}", Enum.GetName(typeof(Ultima1Map), (Ultima1Map)(index & 0xff)));
                default:
                    // Should probably have Level1 through Level10 indices for these maps but currently they serve no purpose (quests, etc.)
                    return String.Format("Ultima1Map.{0}", Enum.GetName(typeof(Ultima1Map), (Ultima1Map)(index & 0xff)));
            }
        }

        public override bool IsDungeon(int iMap) => GetMapType(iMap) == UltimaMapType.Dungeon;

        public UltimaMapType GetMapType(int iMap)
        {
            if (iMap < 1)
                return UltimaMapType.Overworld;
            switch ((Ultima1Map) (iMap & 0xff))
            {
                case Ultima1Map.AdvarisHole:
                case Ultima1Map.DeadMansWalk:
                case Ultima1Map.DeathsAwakening:
                case Ultima1Map.FreeDeathHole:
                case Ultima1Map.MondainsGateToHell:
                case Ultima1Map.ScorpionHole:
                case Ultima1Map.TheDeadCatsLife:
                case Ultima1Map.TheDeadCatsLifeII:
                case Ultima1Map.TheDeadWarriorsFight:
                case Ultima1Map.TheDungeonOfDoom:
                case Ultima1Map.TheDungeonOfDoubt:
                case Ultima1Map.TheDungeonOfMontor:
                case Ultima1Map.TheDungeonOfPerinia:
                case Ultima1Map.TheEnd:
                case Ultima1Map.TheGorgonHole:
                case Ultima1Map.TheGuildOfDeath:
                case Ultima1Map.TheHoleToHades:
                case Ultima1Map.TheHorrorOfTheHarpies:
                case Ultima1Map.TheHorrorOfTheHarpiesII:
                case Ultima1Map.TheLabyrinth:
                case Ultima1Map.TheLongDeath:
                case Ultima1Map.TheLostCaverns:
                case Ultima1Map.TheMetalTwister:
                case Ultima1Map.TheMinesOfMtDrash:
                case Ultima1Map.TheMinesOfMtDrashII:
                case Ultima1Map.TheMorbidAdventure:
                case Ultima1Map.TheSavagePlace:
                case Ultima1Map.TheSkullSmasher:
                case Ultima1Map.TheSlowDeath:
                case Ultima1Map.TheSpineBreaker:
                case Ultima1Map.TheTrampOfDoom:
                case Ultima1Map.TheTrollsHole:
                case Ultima1Map.TheUnholyHole:
                case Ultima1Map.TheVipersPit:
                case Ultima1Map.TheVipersPitII:
                case Ultima1Map.WhereHerculesDied:
                    return UltimaMapType.Dungeon;
                case Ultima1Map.TheCastleOfLordBritish:
                case Ultima1Map.TheCastleOfTheLostKing:
                case Ultima1Map.TheCastleBarataria:
                case Ultima1Map.TheCastleRondorin:
                case Ultima1Map.TheCastleOfOlympus:
                case Ultima1Map.TheBlackDragonsCastle:
                case Ultima1Map.TheWhiteDragonsCastle:
                case Ultima1Map.TheCastleOfShamino:
                    return UltimaMapType.Castle;
                default:
                    return UltimaMapType.City;
            }
        }

        public bool IsCastle(int iMap)
        {
            if (iMap < 1)
                return false;
            switch ((Ultima1Map)(iMap & 0xff))
            {
                case Ultima1Map.TheBlackDragonsCastle:
                case Ultima1Map.TheCastleBarataria:
                case Ultima1Map.TheCastleOfLordBritish:
                case Ultima1Map.TheCastleOfOlympus:
                case Ultima1Map.TheCastleOfShamino:
                case Ultima1Map.TheCastleOfTheLostKing:
                case Ultima1Map.TheCastleRondorin:
                case Ultima1Map.TheWhiteDragonsCastle:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsCity(int iMap)
        {
            if (iMap < 1)
                return false;
            switch ((Ultima1Map)(iMap & 0xff))
            {
                case Ultima1Map.Arnold:
                case Ultima1Map.Britian:
                case Ultima1Map.Bulldozer:
                case Ultima1Map.ClearLagoon:
                case Ultima1Map.Dextron:
                case Ultima1Map.Fawn:
                case Ultima1Map.Gauntlet:
                case Ultima1Map.Gerry:
                case Ultima1Map.Gorlab:
                case Ultima1Map.Grey:
                case Ultima1Map.Helen:
                case Ultima1Map.Imagination:
                case Ultima1Map.John:
                case Ultima1Map.Linda:
                case Ultima1Map.LostFriends:
                case Ultima1Map.Magic:
                case Ultima1Map.Montor:
                case Ultima1Map.Moon:
                case Ultima1Map.Nassau:
                case Ultima1Map.Owen:
                case Ultima1Map.Paws:
                case Ultima1Map.Ponder:
                case Ultima1Map.Poor:
                case Ultima1Map.Stout:
                case Ultima1Map.TheBrother:
                case Ultima1Map.TheSnake:
                case Ultima1Map.Tune:
                case Ultima1Map.Turtle:
                case Ultima1Map.Wealth:
                case Ultima1Map.Wheeler:
                case Ultima1Map.Wolf:
                case Ultima1Map.Yew:
                    return true;
                default:
                    return false;
            }
        }

        public static Ultima1Map MapFromOverworldPoint(Point pt)
        {
            int index = (pt.X << 8) | pt.Y;
            switch (index)
            {
                case (18 << 8) | 13: return Ultima1Map.TheDungeonOfPerinia;
                case (36 << 8) | 9: return Ultima1Map.ThePillarsOfProtection;
                case (48 << 8) | 11: return Ultima1Map.TheUnholyHole;
                case (69 << 8) | 10: return Ultima1Map.TheTowerOfKnowledge;
                case (53 << 8) | 22: return Ultima1Map.TheDungeonOfMontor;
                case (64 << 8) | 22: return Ultima1Map.Grey;
                case (32 << 8) | 27: return Ultima1Map.TheCastleOfTheLostKing;
                case (46 << 8) | 28: return Ultima1Map.Paws;
                case (59 << 8) | 29: return Ultima1Map.TheMinesOfMtDrash;
                case (18 << 8) | 34: return Ultima1Map.Yew;
                case (39 << 8) | 39: return Ultima1Map.Britian;
                case (40 << 8) | 38: return Ultima1Map.TheCastleOfLordBritish;
                case (66 << 8) | 41: return Ultima1Map.Moon;
                case (29 << 8) | 37: return Ultima1Map.MondainsGateToHell;
                case (13 << 8) | 43: return Ultima1Map.TheLostCaverns;
                case (62 << 8) | 49: return Ultima1Map.TheDungeonOfDoubt;
                case (25 << 8) | 61: return Ultima1Map.Fawn;
                case (39 << 8) | 60: return Ultima1Map.TheMinesOfMtDrashII;
                case (52 << 8) | 63: return Ultima1Map.Montor;
                case (70 << 8) | 63: return Ultima1Map.Tune;
                case (38 << 8) | 68: return Ultima1Map.DeathsAwakening;
                case (13 << 8) | 89: return Ultima1Map.TheSignPost;
                case (25 << 8) | 94: return Ultima1Map.Poor;
                case (44 << 8) | 92: return Ultima1Map.ClearLagoon;
                case (66 << 8) | 88: return Ultima1Map.Wealth;
                case (52 << 8) | 96: return Ultima1Map.TheTrampOfDoom;
                case (32 << 8) | 99: return Ultima1Map.TheVipersPit;
                case (25 << 8) | 105: return Ultima1Map.TheLongDeath;
                case (14 << 8) | 110: return Ultima1Map.TheEnd;
                case (31 << 8) | 112: return Ultima1Map.Gauntlet;
                case (41 << 8) | 118: return Ultima1Map.TheCastleOfOlympus;
                case (12 << 8) | 122: return Ultima1Map.TheSouthernSignPost;
                case (42 << 8) | 119: return Ultima1Map.Nassau;
                case (63 << 8) | 119: return Ultima1Map.TheVipersPitII;
                case (71 << 8) | 120: return Ultima1Map.TheSlowDeath;
                case (30 << 8) | 126: return Ultima1Map.TheBlackDragonsCastle;
                case (40 << 8) | 129: return Ultima1Map.TheGuildOfDeath;
                case (64 << 8) | 133: return Ultima1Map.Stout;
                case (16 << 8) | 140: return Ultima1Map.TheMetalTwister;
                case (37 << 8) | 140: return Ultima1Map.Ponder;
                case (46 << 8) | 145: return Ultima1Map.TheTrollsHole;
                case (130 << 8) | 10: return Ultima1Map.TheSavagePlace;
                case (100 << 8) | 15: return Ultima1Map.ScorpionHole;
                case (121 << 8) | 15: return Ultima1Map.Gerry;
                case (148 << 8) | 22: return Ultima1Map.Helen;
                case (124 << 8) | 26: return Ultima1Map.AdvarisHole;
                case (114 << 8) | 29: return Ultima1Map.TheCastleRondorin;
                case (96 << 8) | 33: return Ultima1Map.ThePillarsOfTheArgonauts;
                case (126 << 8) | 36: return Ultima1Map.Arnold;
                case (125 << 8) | 37: return Ultima1Map.TheCastleBarataria;
                case (147 << 8) | 36: return Ultima1Map.TheHorrorOfTheHarpies;
                case (155 << 8) | 35: return Ultima1Map.TheDeadWarriorsFight;
                case (98 << 8) | 45: return Ultima1Map.TheLabyrinth;
                case (115 << 8) | 43: return Ultima1Map.Owen;
                case (109 << 8) | 50: return Ultima1Map.WhereHerculesDied;
                case (150 << 8) | 49: return Ultima1Map.John;
                case (116 << 8) | 56: return Ultima1Map.TheHorrorOfTheHarpiesII;
                case (136 << 8) | 59: return Ultima1Map.TheGorgonHole;
                case (109 << 8) | 61: return Ultima1Map.TheSnake;
                case (128 << 8) | 63: return Ultima1Map.Linda;
                case (97 << 8) | 66: return Ultima1Map.ThePillarOfOzymandias;
                case (150 << 8) | 67: return Ultima1Map.Wolf;
                case (98 << 8) | 88: return Ultima1Map.TheGraveOfTheLostSoul;
                case (119 << 8) | 89: return Ultima1Map.TheSkullSmasher;
                case (131 << 8) | 87: return Ultima1Map.TheEasternSignPost;
                case (149 << 8) | 91: return Ultima1Map.TheSpineBreaker;
                case (103 << 8) | 100: return Ultima1Map.LostFriends;
                case (114 << 8) | 100: return Ultima1Map.TheDungeonOfDoom;
                case (108 << 8) | 107: return Ultima1Map.TheDeadCatsLife;
                case (121 << 8) | 106: return Ultima1Map.Wheeler;
                case (135 << 8) | 105: return Ultima1Map.TheCastleOfShamino;
                case (149 << 8) | 112: return Ultima1Map.TheBrother;
                case (138 << 8) | 115: return Ultima1Map.TheMorbidAdventure;
                case (127 << 8) | 116: return Ultima1Map.TheWhiteDragonsCastle;
                case (128 << 8) | 117: return Ultima1Map.Gorlab;
                case (101 << 8) | 119: return Ultima1Map.Dextron;
                case (154 << 8) | 121: return Ultima1Map.FreeDeathHole;
                case (105 << 8) | 127: return Ultima1Map.DeadMansWalk;
                case (128 << 8) | 138: return Ultima1Map.TheDeadCatsLifeII;
                case (142 << 8) | 139: return Ultima1Map.Magic;
                case (97 << 8) | 141: return Ultima1Map.Turtle;
                case (115 << 8) | 141: return Ultima1Map.Bulldozer;
                case (129 << 8) | 146: return Ultima1Map.TheHoleToHades;
                case (66 << 8) | 106: return Ultima1Map.Imagination;
                default: return Ultima1Map.Legend;
            }
        }

        public static Point OverworldPointFromMap(Ultima1Map map)
        {
            switch (map)
            {
                case Ultima1Map.TheDungeonOfPerinia: return Ultima1.Spots.SpotTheDungeonOfPerinia.Location;
                case Ultima1Map.ThePillarsOfProtection: return Ultima1.Spots.SpotPillarsOfProtection.Location;
                case Ultima1Map.TheUnholyHole: return Ultima1.Spots.SpotTheUnholyHole.Location;
                case Ultima1Map.TheTowerOfKnowledge: return Ultima1.Spots.SpotTowerOfKnowledge.Location;
                case Ultima1Map.TheDungeonOfMontor: return Ultima1.Spots.SpotTheDungeonOfMontor.Location;
                case Ultima1Map.Grey: return Ultima1.Spots.SpotCityOfGrey.Location;
                case Ultima1Map.TheCastleOfTheLostKing: return Ultima1.Spots.SpotCastleOfTheLostKing.Location;
                case Ultima1Map.Paws: return Ultima1.Spots.SpotCityOfPaws.Location;
                case Ultima1Map.TheMinesOfMtDrash: return Ultima1.Spots.SpotMinesOfMountDrash.Location;
                case Ultima1Map.Yew: return Ultima1.Spots.SpotCityOfYew.Location;
                case Ultima1Map.Britian: return Ultima1.Spots.SpotCityOfBritain.Location;
                case Ultima1Map.TheCastleOfLordBritish: return Ultima1.Spots.SpotCastleOfLordBritish.Location;
                case Ultima1Map.Moon: return Ultima1.Spots.SpotCityOfMoon.Location;
                case Ultima1Map.MondainsGateToHell: return Ultima1.Spots.SpotMondainsGateToHell.Location;
                case Ultima1Map.TheLostCaverns: return Ultima1.Spots.SpotTheLostCaverns.Location;
                case Ultima1Map.TheDungeonOfDoubt: return Ultima1.Spots.SpotTheDungeonOfDoubt.Location;
                case Ultima1Map.Fawn: return Ultima1.Spots.SpotCityOfFawn.Location;
                case Ultima1Map.TheMinesOfMtDrashII: return Ultima1.Spots.SpotMinesOfMountDrash2.Location;
                case Ultima1Map.Montor: return Ultima1.Spots.SpotCityOfMontor.Location;
                case Ultima1Map.Tune: return Ultima1.Spots.SpotCityOfTune.Location;
                case Ultima1Map.DeathsAwakening: return Ultima1.Spots.SpotDeathsAwakening.Location;
                case Ultima1Map.TheSignPost: return Ultima1.Spots.SpotSignPost.Location;
                case Ultima1Map.Poor: return Ultima1.Spots.SpotCityOfPoor.Location;
                case Ultima1Map.ClearLagoon: return Ultima1.Spots.SpotCityOfClearLagoon.Location;
                case Ultima1Map.Wealth: return Ultima1.Spots.SpotCityOfWealth.Location;
                case Ultima1Map.TheTrampOfDoom: return Ultima1.Spots.SpotTrampOfDoom.Location;
                case Ultima1Map.TheVipersPit: return Ultima1.Spots.SpotTheVipersPit.Location;
                case Ultima1Map.TheLongDeath: return Ultima1.Spots.SpotTheLongDeath.Location;
                case Ultima1Map.TheEnd: return Ultima1.Spots.SpotTheEnd.Location;
                case Ultima1Map.Gauntlet: return Ultima1.Spots.SpotCityOfGauntlet.Location;
                case Ultima1Map.TheCastleOfOlympus: return Ultima1.Spots.SpotCityOfOlympus.Location;
                case Ultima1Map.TheSouthernSignPost: return Ultima1.Spots.SpotSouthernSignPost.Location;
                case Ultima1Map.Nassau: return Ultima1.Spots.SpotCityOfNassau.Location;
                case Ultima1Map.TheVipersPitII: return Ultima1.Spots.SpotVipersPit2.Location;
                case Ultima1Map.TheSlowDeath: return Ultima1.Spots.SpotTheSlowDeath.Location;
                case Ultima1Map.TheBlackDragonsCastle: return Ultima1.Spots.SpotBlackDragonsCastle.Location;
                case Ultima1Map.TheGuildOfDeath: return Ultima1.Spots.SpotTheGuildOfDeath.Location;
                case Ultima1Map.Stout: return Ultima1.Spots.SpotCityOfStout.Location;
                case Ultima1Map.TheMetalTwister: return Ultima1.Spots.SpotTheMetalTwister.Location;
                case Ultima1Map.Ponder: return Ultima1.Spots.SpotCityOfPonder.Location;
                case Ultima1Map.TheTrollsHole: return Ultima1.Spots.SpotTrollsHole.Location;
                case Ultima1Map.TheSavagePlace: return Ultima1.Spots.SpotTheSavagePlace.Location;
                case Ultima1Map.ScorpionHole: return Ultima1.Spots.SpotScorpionHole.Location;
                case Ultima1Map.Gerry: return Ultima1.Spots.SpotCityOfGerry.Location;
                case Ultima1Map.Helen: return Ultima1.Spots.SpotCityOfHelen.Location;
                case Ultima1Map.AdvarisHole: return Ultima1.Spots.SpotAdvarisHole.Location;
                case Ultima1Map.TheCastleRondorin: return Ultima1.Spots.SpotCastleRondorin.Location;
                case Ultima1Map.ThePillarsOfTheArgonauts: return Ultima1.Spots.SpotPillarsOfTheArgonauts.Location;
                case Ultima1Map.Arnold: return Ultima1.Spots.SpotCityOfArnold.Location;
                case Ultima1Map.TheCastleBarataria: return Ultima1.Spots.SpotCastleBarataria.Location;
                case Ultima1Map.TheHorrorOfTheHarpies: return Ultima1.Spots.SpotTheHorrorOfTheHarpies.Location;
                case Ultima1Map.TheDeadWarriorsFight: return Ultima1.Spots.SpotTheDeadWarriorsFight.Location;
                case Ultima1Map.TheLabyrinth: return Ultima1.Spots.SpotTheLabyrinth.Location;
                case Ultima1Map.Owen: return Ultima1.Spots.SpotCityOfOwen.Location;
                case Ultima1Map.WhereHerculesDied: return Ultima1.Spots.SpotWhereHerculesDied.Location;
                case Ultima1Map.John: return Ultima1.Spots.SpotCityOfJohn.Location;
                case Ultima1Map.TheHorrorOfTheHarpiesII: return Ultima1.Spots.SpotTheHorrorOfTheHarpies2.Location;
                case Ultima1Map.TheGorgonHole: return Ultima1.Spots.SpotTheGorgonHole.Location;
                case Ultima1Map.TheSnake: return Ultima1.Spots.SpotTheCityOfTheSnake.Location;
                case Ultima1Map.Linda: return Ultima1.Spots.SpotCityOfLinda.Location;
                case Ultima1Map.ThePillarOfOzymandias: return Ultima1.Spots.SpotPillarOfOzymandias.Location;
                case Ultima1Map.Wolf: return Ultima1.Spots.SpotCityOfWolf.Location;
                case Ultima1Map.TheGraveOfTheLostSoul: return Ultima1.Spots.SpotGraveOfTheLostSoul.Location;
                case Ultima1Map.TheSkullSmasher: return Ultima1.Spots.SpotTheSkullSmasher.Location;
                case Ultima1Map.TheEasternSignPost: return Ultima1.Spots.SpotEasternSignPost.Location;
                case Ultima1Map.TheSpineBreaker: return Ultima1.Spots.SpotTheSpineBreaker.Location;
                case Ultima1Map.LostFriends: return Ultima1.Spots.SpotCityOfLostFriends.Location;
                case Ultima1Map.TheDungeonOfDoom: return Ultima1.Spots.SpotTheDungeonOfDoom.Location;
                case Ultima1Map.TheDeadCatsLife: return Ultima1.Spots.SpotTheDeadCatsLife.Location;
                case Ultima1Map.Wheeler: return Ultima1.Spots.SpotCityOfWheeler.Location;
                case Ultima1Map.TheCastleOfShamino: return Ultima1.Spots.SpotCastleOfShamino.Location;
                case Ultima1Map.TheBrother: return Ultima1.Spots.SpotCityOfTheBrother.Location;
                case Ultima1Map.TheMorbidAdventure: return Ultima1.Spots.SpotTheMorbidAdventure.Location;
                case Ultima1Map.TheWhiteDragonsCastle: return Ultima1.Spots.SpotWhiteDragonsCastle.Location;
                case Ultima1Map.Gorlab: return Ultima1.Spots.SpotCityOfGorlab.Location;
                case Ultima1Map.Dextron: return Ultima1.Spots.SpotCityOfDextron.Location;
                case Ultima1Map.FreeDeathHole: return Ultima1.Spots.SpotFreeDeathHole.Location;
                case Ultima1Map.DeadMansWalk: return Ultima1.Spots.SpotDeadMansWalk.Location;
                case Ultima1Map.TheDeadCatsLifeII: return Ultima1.Spots.SpotTheDeadCatsLife2.Location;
                case Ultima1Map.Magic: return Ultima1.Spots.SpotCityOfMagic.Location;
                case Ultima1Map.Turtle: return Ultima1.Spots.SpotCityOfTurtle.Location;
                case Ultima1Map.Bulldozer: return Ultima1.Spots.SpotCityOfBulldozer.Location;
                case Ultima1Map.TheHoleToHades: return Ultima1.Spots.SpotTheHoleToHades.Location;
                case Ultima1Map.Imagination: return Ultima1.Spots.SpotCityOfImagination.Location;
                default: return new Point(-1, -1);
            }
        }

        public override IEnumerable<Monster> GetMonsterList() => Ultima1.Monsters;


        public override QuestInfoBase GetQuestInfo(QuestInfoBase lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            QuestInfoBase info = new Ultima1QuestInfo();
            LocationInformation li = GetLocation();
            UltimaPartyInfo pi = ReadUltima1PartyInfo();
            QuestData data = new Ultima1QuestData(pi, li, GetGameState() as UltimaGameState, GetMapData(false, li.MapIndex) as Ultima1MapData, GetGameInfo() as Ultima1GameInfo);

            info.MapIndex = GetCurrentMapIndex();
            MemoryStream ms = new MemoryStream();
            data.AddBytes(ms);
            byte[] newBytes = ms.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            if (pi != null)
                info.CharName = pi.CharacterName;
            info.SetQuests(data, iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override void SetInventory(UltimaInventory inv)
        {
            byte[] bytesReady = inv.GetReadyBytes();
            byte[] bytesInv = inv.GetBytes();
            WriteOffset(Memory.PartyInfo + Ultima1.Offsets.Inventory, bytesInv);
            WriteOffset(Memory.PartyInfo + Ultima1.Offsets.ReadyWeapon, bytesReady);
        }

        public override bool SetMonster(Monster monster)
        {
            UltimaMonster uMon = monster as UltimaMonster;
            if (uMon == null)
                return false;

            switch(uMon.MapType)
            {
                case UltimaMapType.Dungeon:
                    int iNewOffset = (uMon.Position.X * 88) + (uMon.Position.Y * 8);

                    if (uMon.PreviousPosition != Global.NullPoint)
                    {
                        // Need to remove the monster from the previous location also
                        int iOldOffset = (uMon.PreviousPosition.X * 88) + (uMon.PreviousPosition.Y * 8);
                        WriteInt16(Memory.Map + iOldOffset + 2, -1);    // Index
                        WriteInt16(Memory.Map + iOldOffset + 6, 0);     // HP
                    }
                    WriteInt16(Memory.Map + iNewOffset + 2, (short) uMon.ExternalIndex);
                    WriteInt16(Memory.Map + iNewOffset + 6, (short) uMon.CurrentHP);
                    return true;
                case UltimaMapType.Overworld:
                    WriteOffset(Memory.MonstersOverworld + (16 * monster.EncounterIndex), uMon.GetBytes());
                    return true;
                case UltimaMapType.Castle:
                case UltimaMapType.City:
                    WriteOffset(Memory.TownNPCs + (8 * monster.EncounterIndex), uMon.GetBytes());
                    return true;
                default:
                    return false;
            }
        }

        public override ItemLocations GetItemLocations(int iMapIndex, bool bForceNew = false)
        {
            if (!IsValid || !IsDungeon(iMapIndex))
                return null;

            MemoryBytes mbMap = ReadOffset(Memory.Map, DungeonSquareSize * DungeonWidth * DungeonHeight);
            ItemLocations locations = new ItemLocations();
            MemoryStream msRaw = new MemoryStream();
            for (int i = 0; i < DungeonWidth; i++)
            {
                for (int j = 0; j < DungeonHeight; j++)
                {
                    byte bItem = mbMap.Bytes[DungeonSquareSize * (i * DungeonHeight + j) + 4];
                    msRaw.WriteByte(bItem);
                    switch (bItem)
                    {
                        case 4:
                            locations.AddItemPosition(new UltimaItem(UltimaItemIndex.Chest, 1), new Point(i, j));
                            break;
                        case 5:
                            locations.AddItemPosition(new UltimaItem(UltimaItemIndex.Coffin, 1), new Point(i, j));
                            break;
                        default:
                            break;
                    }
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
    }
}
