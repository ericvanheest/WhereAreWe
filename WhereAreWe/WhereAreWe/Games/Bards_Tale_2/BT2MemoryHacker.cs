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
    public class BT2Memory : BTMemory
    {
        // Search for "on the disk."
        public override byte[] MainSearch { get { return new byte[] { 0x6F, 0x6E, 0x20, 0x74, 0x68, 0x65, 0x20, 0x64, 0x69, 0x73, 0x6B }; } }

        public override int MainBlockSVN { get { return -244055; } }
        public override int MainBlockOldSVN { get { return -243687; } }
        public override int MainBlockNonSVN { get { return -244055; } }

        public override int Map { get { return -82005; } }
        public override int TownMap { get { return 33323; } }
        public override int LocationNorthTown { get { return 33835; } }
        public override int LocationEastTown { get { return 37529; } }
        public override int FacingTown { get { return 37391; } }
        public override int MainMapIndex { get { return 33226; } }
        public override int LocationNorth { get { return 34127; } }
        public override int LocationEast { get { return 37223; } }
        public override int Facing { get { return 35289; } }
        public override int PartyNames { get { return -13140; } }
        public override int PartyInfo { get { return 36163; } }
        public override int MarchingOrder { get { return -12845; } }
        public override int ItemList { get { return -1215; } }
        public override int ItemACBonus { get { return -1215; } }
        public override int ItemTypes { get { return -3821; } }
        public override int ItemUsableBy { get { return -3693; } }
        public override int ItemEffects { get { return -3437; } }
        public override int ItemDamage { get { return -1077; } }
        public override int ItemValues { get { return 515; } }
        public override int ItemCharges { get { return -3309; } }
        public override int ItemEquipEffect { get { return -3565; } }
        public override int SubMapIndex { get { return 35576; } }
        public override int MapSpecials { get { return -81493; } }
        public override int MapCustomSquares { get { return -80341; } }
        public override int MapFixedSquares { get { return -80373; } }
        public override int MapTeleport { get { return -80405; } }
        public override int MapSpecials2 { get { return -80981; } }
        public override int Stack { get { return 39633; } }
        public override int MonsterHP { get { return 32931; } }
        public override int MonsterNumAlive { get { return 32925; } }
        public override int MonsterIndices { get { return 37233; } }
        public override int MonsterDistances { get { return 33305; } }

        public override int CharCombatDamageBonus { get { return 33297; } }
        public override int EnemyDamageBonus { get { return 35285; } }
        public override int EnemyACBonus { get { return 34003; } }
        public override int EnemyLoseTurn { get { return 33189; } }
        public override int PartyCombatACBonus { get { return 37446; } }
        public override int PartyCombatMagicResist { get { return 33192; } }
        public override int PartyCombatOptions { get { return 33227; } }
        public override int PartyCombatSubOptions1 { get { return 33315; } }
        public override int PartyCombatSelectedSpells { get { return 36774; } }
        public override int CharCombatDamageBonus2 { get { return 36785; } }
        public override int LightDistance { get { return -12499; } }
        public override int LevitationDuration { get { return 32929; } }
        public override int ShieldDuration { get { return 36161; } }
        public override int LightDuration { get { return 33304; } }
        public override int DetectionDuration { get { return 36162; } }
        public override int CompassDuration { get { return 33973; } }
        public override int AdventuringSong { get { return 37003; } }
        public override int CombatSong { get { return 37128; } }
        public override int SongDuration { get { return -12366; } }
        public override int SpellIcon1 { get { return 1759; } }
        public override int SpellIcon2 { get { return 1760; } }
        public override int SpellIcon3 { get { return 1761; } }
        public override int SpellIcon4 { get { return 1762; } }
        public override int SpellIcon5 { get { return 1763; } }
        public override int GameTimeHours { get { return -12460; } }
        public override int GameTimeSeconds { get { return 2182; } }
        public override int MapStrings { get { return 33323; } }
        public override int MapSquareStrings { get { return 33323; } }
        public override int CastingChar { get { return 37230; } }
        public override int PartyPerishSeconds { get { return 33975; } }
        public override int Counter1 { get { return 31347; } }
        public override int CreationStats { get { return 39397; } }
        public override int CreationRace { get { return 39395; } }
        public override int NumItemsInShop { get { return 39317; } }
        public override int ShopInventory { get { return 39323; } }
        public override int TreasureRanges { get { return -2725; } }
        public override int TreasureMinimums { get { return -2709; } }
        public override int MapTreasureIndex { get { return 33193; } }
        public override int EncounterMonstersKilled { get { return 34023; } }
        public override int MapGoldMax { get { return -2595; } }
        public override int ForcedEncounters { get { return -80373; } }
        public override int CampInspectingChar { get { return 39487; } }
        public override int TrapType { get { return 33271; } }
        public override int PartyBonusAttacks { get { return 36772; } }

        // Unfinished
        public override int CharCombatACBonus { get { return 0; } }
        public override int PartyCombatSubOptions2 { get { return 0; } }
        public override int InspectingChar { get { return 54724; } }
        public override int ShopInspectingChar { get { return 54700; } }
        public override int ShoppingChar { get { return 54702; } }
        public override int CombatActingChar1 { get { return 54628; } }
        public override int CombatActingChar2 { get { return 54650; } }
        public override int CombatActingChar3 { get { return 54680; } }
        public override int AskCastSpell { get { return -74; } }
        public override int AskWhichSpell { get { return 54681; } }
        public override int AskWhichSpellCombat { get { return 54567; } }
        public override int AskWhichSpellCombat2 { get { return 54589; } }
        public override int AskWhichSong { get { return 54679; } }
        public override int SummonedCreature { get { return 51410; } }
        public override int TreasureState { get { return 28432; } }
        public override int TrapExamined { get { return 46924; } }
        public override int MonsterGroup { get { return 4692; } }
        public override int MonsterAC { get { return 4820; } }
        public override int MonsterDamage { get { return 4948; } }
        public override int MonsterExp { get { return 5076; } }
        public override int ScreenText { get { return 50740; } }
        public override int SwapWallsDoors { get { return 20921; } }
        public override int ImageCaption { get { return -1012; } }
        public override int AdvPartyACBonus { get { return 52778; } }
        public override int SurfaceMapIndex { get { return 0; } }
        public override int CombatActiveSpells { get { return 0; } }

        public override int NumChars { get { return 0; } }
        public override int EncounterInfo { get { return 0; } }

        public override int State1 { get { return 54672; } }
        public override int State2 { get { return 54670; } }
        public override int State3 { get { return 54658; } }
        public override int State4 { get { return -12891; } }
        public override int StackAddressIndicator { get { return 54758; } }
        public override int StateArray { get { return 0; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.BardsTale2]; } }

        // Bard's Tale 2 specific
        public int Wilderness { get { return 31381; } }
        public int CurrentMonsters { get { return 35291; } }
        public int SubMapIndex2 { get { return 35672; } }

        public BT2StackInfo[] StackLocations;
        public StackGuess[] StackGuesses;

        public BT2Memory()
        {
            // Treasure:  0064 0206 0521 0A74 0DF7 0ECA 13C1 16A1 3A75 3AD7 3F0A 4C0A 4F0A 5A20 6173 6565 6570 6576 6920 6944 6D72 7061 7254 7845 8FB0
            List<BT2StackInfo> list = new List<BT2StackInfo>();
            list.Add(new BT2StackInfo(0x9B, 0x43, MainState.CombatSelectSpell, 0x08));
            list.Add(new BT2StackInfo(0x31, 0x12, MainState.SelectSpell, 0));
            list.Add(new BT2StackInfo(0xB2, 0x44, MainState.CombatSelectBardSong, 0x08));
            list.Add(new BT2StackInfo(0xDA, 0x16, MainState.SelectBardSong, 0));
            list.Add(new BT2StackInfo(0x57, 0x09, MainState.Adventuring, 0));
            list.Add(new BT2StackInfo(0xFB, 0x44, MainState.CombatConfirmRound, 0));
            list.Add(new BT2StackInfo(0x6E, 0x3F, MainState.PreCombat, 0));
            list.Add(new BT2StackInfo(0xFD, 0x26, MainState.CombatOptions, -0x1A));
            list.Add(new BT2StackInfo(0xD7, 0x3A, MainState.Treasure, 0));
            list.Add(new BT2StackInfo(0xAA, 0x0F, MainState.CreateSelectClass, 0));
            list.Add(new BT2StackInfo(0x80, 0xFF, MainState.CreateSelectName, 0));
            list.Add(new BT2StackInfo(0x69, 0x08, MainState.Combat, -0x1C));
//            list.Add(new BT2StackInfo(0x4D, 0x22, MainState.ShopBuyItem, 0x10));
//            list.Add(new BT2StackInfo(0x80, 0xD3, MainState.Shop, 0x0C));
            list.Add(new BT2StackInfo(0x06, 0xBF, MainState.ShopBuyItem, 0x14));
            list.Add(new BT2StackInfo(0xB7, 0x10, MainState.Shop, 0x06));
            list.Add(new BT2StackInfo(0x96, 0x04, MainState.CampInspecting, 0x1A));
            StackLocations = list.ToArray();

            List<StackGuess> guesses = new List<StackGuess>();
            guesses.Add(new StackGuess(39269, 0x3D, 0x22, 0xC6, 0xD2, MainState.RenameChar, +0x00));
            guesses.Add(new StackGuess(39483, 0xA2, 0xD3, 0x65, 0x00, MainState.Adventuring, +0x00));
            guesses.Add(new StackGuess(39483, 0xA2, 0xD3, 0x12, 0x01, MainState.Adventuring, +0x00));
            guesses.Add(new StackGuess(39483, 0xA2, 0xD3, 0xB5, 0x01, MainState.Adventuring, +0x00));
            guesses.Add(new StackGuess(39271, 0x3D, 0x22, 0xC8, 0xD2, MainState.ShopBuyItem, +0xBE));
            guesses.Add(new StackGuess(39271, 0x90, 0x00, 0x03, 0x00, MainState.ShopBuyItem, +0xBE));
            guesses.Add(new StackGuess(39271, 0x90, 0x00, 0x04, 0x00, MainState.ShopBuyItem, +0xBE));
            guesses.Add(new StackGuess(39277, 0x00, 0x00, 0xD8, 0xD2, MainState.ShopBuyItem, +0xB8));
            guesses.Add(new StackGuess(39443, 0x72, 0xD3, 0x4D, 0x22, MainState.ShopInspectChar, +0x10));
            guesses.Add(new StackGuess(39353, 0x1A, 0xD3, 0x1A, 0xD3, MainState.CreateSelectName, +0x00));
            guesses.Add(new StackGuess(39357, 0x00, 0x08, 0x94, 0xD3, MainState.CreateSelectClass, +0x00));
            guesses.Add(new StackGuess(39357, 0x7E, 0x21, 0x94, 0xD3, MainState.CreateSelectRace, +0x00));
            guesses.Add(new StackGuess(39365, 0x4F, 0x00, 0x20, 0x00, MainState.DeleteChar, +0x00));
            guesses.Add(new StackGuess(39379, 0x10, 0x00, 0x42, 0x04, MainState.BankOpenAccount, +0x52));
            guesses.Add(new StackGuess(39385, 0x10, 0x00, 0x42, 0x04, MainState.BankCloseAccount, +0x4C));
            guesses.Add(new StackGuess(39399, 0x10, 0x00, 0xC8, 0x3A, MainState.BankListAccounts, +0x3E));
            guesses.Add(new StackGuess(39411, 0x5E, 0xD3, 0x5E, 0xD3, MainState.ShopSellItem, +0x32));
            guesses.Add(new StackGuess(39415, 0x62, 0xD3, 0x62, 0xD3, MainState.ShopIdentifyItem, +0x2E));
            guesses.Add(new StackGuess(39421, 0x6D, 0x1A, 0x60, 0xD3, MainState.ReviewWhoClass, +0x00));
            guesses.Add(new StackGuess(39421, 0x5E, 0xD3, 0x67, 0x1E, MainState.TempleNoHealing, +0x00));
            guesses.Add(new StackGuess(39423, 0x6D, 0x1A, 0x62, 0xD3, MainState.ReviewWhoAdvance, +0x00));
            guesses.Add(new StackGuess(39427, 0x10, 0x00, 0xA4, 0x01, MainState.BankChooseOption, +0x22));
            guesses.Add(new StackGuess(39427, 0x10, 0x00, 0x74, 0x25, MainState.Bank, +0x00));
            guesses.Add(new StackGuess(39427, 0x3D, 0x22, 0x64, 0xD3, MainState.Temple, +0x00));
            guesses.Add(new StackGuess(39453, 0x6A, 0x01, 0xB7, 0x10, MainState.ShopChooseOption, +0x08));
            guesses.Add(new StackGuess(39427, 0xB0, 0x00, 0x74, 0x25, MainState.ReviewMain, +0x00));
            guesses.Add(new StackGuess(39429, 0x6A, 0x00, 0x74, 0xD3, MainState.ReviewMain, +0x00));
            guesses.Add(new StackGuess(39427, 0x70, 0x00, 0x86, 0x0C, MainState.Adventuring, +0x00));
            guesses.Add(new StackGuess(39459, 0x08, 0x00, 0x8A, 0xD3, MainState.SelectBardSong, +0x00));
            guesses.Add(new StackGuess(39429, 0x74, 0x25, 0x25, 0x00, MainState.Shop, +0x00));
            guesses.Add(new StackGuess(39429, 0x6A, 0x00, 0x74, 0xD3, MainState.Shop, +0x00));
            guesses.Add(new StackGuess(39427, 0x80, 0x00, 0x86, 0x0C, MainState.Adventuring, +0x00));
            guesses.Add(new StackGuess(39441, 0x3D, 0x22, 0x72, 0xD3, MainState.SelectBardSong, +0x00));
            guesses.Add(new StackGuess(39415, 0x9A, 0xC6, 0x59, 0xD3, MainState.TempleWhoWillPay, +0x22));
            guesses.Add(new StackGuess(39415, 0x30, 0x00, 0x25, 0x00, MainState.ReviewWhoSpell, +0x00));
            guesses.Add(new StackGuess(39435, 0x3D, 0x22, 0x6C, 0xD3, MainState.EnergyMain, +0x00));
            guesses.Add(new StackGuess(39437, 0x3D, 0x22, 0x6E, 0xD3, MainState.AddChar, +0x00));
            guesses.Add(new StackGuess(39437, 0x6D, 0x1A, 0x70, 0xD3, MainState.EnergyWhoWillPay, +0x14));
            // guesses.Add(new StackGuess(39427, 0x30, 0x00, 0x6A, 0x00, MainState.ReviewMain, +0x00));     // too similar to "shop"
            guesses.Add(new StackGuess(39447, 0x3D, 0x22, 0x78, 0xD3, MainState.Pause, +0x00));
            guesses.Add(new StackGuess(39461, 0x3D, 0x22, 0x86, 0xD3, MainState.GuildRemove, +0x00));
            guesses.Add(new StackGuess(39463, 0x3D, 0x22, 0x88, 0xD3, MainState.GuildDiskOptions, +0x00));
            guesses.Add(new StackGuess(39471, 0x3D, 0x22, 0x90, 0xD3, MainState.GuildMain, +0x00));
            guesses.Add(new StackGuess(39481, 0x42, 0x04, 0xA2, 0xD3, MainState.Adventuring, +0x00));
            StackGuesses = guesses.ToArray();
        }
    }

    public enum BT2Map
    {
        None =             -1,
        Unknown =          -1,
        Wilderness =       0x0000,
        GreyCrypt1 =       0x0001,
        GreyCrypt2 =       0x0002,
        FanskarsCastle =   0x0004,
        Tangramayne =      0x0100,
        DarkDomain1 =      0x0101,
        DarkDomain2 =      0x0102,
        DarkDomain3 =      0x0103,
        DarkDomain4 =      0x0104,
        Ephesus =          0x0200,
        Tombs1 =           0x0201,
        Tombs2 =           0x0202,
        Tombs3 =           0x0203,
        Philippi =         0x0300,
        DargothsTower1 =   0x0301,
        DargothsTower2 =   0x0302,
        DargothsTower3 =   0x0303,
        DargothsTower4 =   0x0304,
        DargothsTower5 =   0x0305,
        Colosse =          0x0400,
        DestinyStone1 =    0x0401,
        DestinyStone2 =    0x0402,
        DestinyStone3 =    0x0403,
        Corinth =          0x0500,
        OsconsFortress1 =  0x0501,
        OsconsFortress2 =  0x0502,
        OsconsFortress3 =  0x0503,
        OsconsFortress4 =  0x0504,
        Thessalonica =     0x0600,
        MazeOfDread1 =     0x0601,
        MazeOfDread2 =     0x0602,
        MazeOfDread3 =     0x0603
    }

    public class BT2TrainingInfo : BTTrainingInfo
    {
        public BT2Map Map { get { return (BT2Map)MapIndex; } }
    }

    public class BT2Effects
    {
        public byte AdventuringSong;
        public byte Counter1;

        public BT2Effects(BT2MemoryHacker hacker)
        {
            AdventuringSong = hacker.ReadByte(BT2.Memory.AdventuringSong);
            Counter1 = hacker.ReadByte(BT2.Memory.Counter1);
        }

        public byte[] Bytes
        {
            get
            {
                return new byte[] { AdventuringSong, Counter1 };
            }
        }
    }

    public class BT2GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return BT2MemoryHacker.GetMapTitlePair(iMap); }

        public BT2GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn) { }
    }

    public class BT2GameInfo : BTGameInfo
    {
        public override GameNames Game { get { return GameNames.BardsTale2; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new BT2GameInfoItem("View Dist.", (byte)LightDistance, new OffsetList(BT2.Memory.LightDistance)));
            items.Add(new BT2GameInfoItem("Light", (byte)LightDuration, new OffsetList(BT2.Memory.LightDuration)));
            items.Add(new BT2GameInfoItem("Levitate", (byte)LevitationDuration, new OffsetList(BT2.Memory.LevitationDuration)));
            items.Add(new BT2GameInfoItem("Compass", (byte)CompassDuration, new OffsetList(BT2.Memory.CompassDuration)));
            items.Add(new BT2GameInfoItem("Detect", (byte)DetectionDuration, new OffsetList(BT2.Memory.DetectionDuration)));
            items.Add(new BT2GameInfoItem("Shield", (byte)ShieldDuration, new OffsetList(BT2.Memory.ShieldDuration)));
            items.Add(new BT2GameInfoItem("Adv. Song", (byte)AdventuringSong, new OffsetList(BT2.Memory.AdventuringSong)));
            items.Add(new BT2GameInfoItem("Song Time", (byte)SongDuration, new OffsetList(BT2.Memory.SongDuration)));
            items.Add(new BT2GameInfoItem("Combat Song", (byte)CombatSong, new OffsetList(BT2.Memory.CombatSong)));
            if (Global.Debug)
            {
                items.Add(new BT2GameInfoItem("Spell 1", (byte)SpellIcon1, new OffsetList(BT2.Memory.SpellIcon1)));
                items.Add(new BT2GameInfoItem("Spell 2", (byte)SpellIcon2, new OffsetList(BT2.Memory.SpellIcon2)));
                items.Add(new BT2GameInfoItem("Spell 3", (byte)SpellIcon3, new OffsetList(BT2.Memory.SpellIcon3)));
                items.Add(new BT2GameInfoItem("Spell 4", (byte)SpellIcon4, new OffsetList(BT2.Memory.SpellIcon4)));
                items.Add(new BT2GameInfoItem("Spell 5", (byte)SpellIcon5, new OffsetList(BT2.Memory.SpellIcon5)));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new BT2GameInfoItem("Time (hours)", (byte)GameTimeHours, new OffsetList(BT2.Memory.GameTimeHours)));
            items.Add(new BT2GameInfoItem("Main Map", (byte)MainMap, new OffsetList(BT2.Memory.MainMapIndex)));
            items.Add(new BT2GameInfoItem("Sub Map", (byte)SubMap, new OffsetList(BT2.Memory.SubMapIndex)));
            items.Add(new BT2GameInfoItem("Trap", (byte)Trap, new OffsetList(BT2.Memory.TrapType)));
            items.Add(new BT2GameInfoItem("Death in (sec)", (Int16) PartyPerish, new OffsetList(BT2.Memory.PartyPerishSeconds)));
            items.Add(new BT2GameInfoItem("Snare Counter", (byte)Counters[0], new OffsetList(BT2.Memory.Counter1)));
            items.Add(new BT2GameInfoItem("Risk/Reward", (byte)RiskReward, new OffsetList(BT2.Memory.MapTreasureIndex)));

            return items;
        }
    }

    public class BT2MapData : BTMapData
    {
        public BT2MapData(MapBytes mb)
        {
            Squares = mb.Bytes;
            Specials = null;
            Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
        }
    }

    public class BT23EncounterInfo : BTEncounterInfo
    {
        public byte[] Distances;
        public BTMonster[] Groups;
    }

    public class BT2EncounterInfo : BT23EncounterInfo
    {
        public BT2EncounterInfo(byte[] monsters, byte[] hp, byte[] living, byte[] distances, BTCombatEffects effects)
        {
            Groups = new BTMonster[living.Length];
            for (int i = 0; i < living.Length; i++)
            {
                Groups[i] = new BT2Monster(i, monsters, i * 32);
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

    public class BT2CombatEffects : BTCombatEffects
    {
        public BT2CombatEffects(BT2MemoryHacker hacker)
        {
            Init(hacker, 7, 4);
        }

        public override void InitSpecific(BTMemoryHacker hacker)
        {
            CharAC = Global.NullBytes(7);
            PartySubOptions2 = Global.NullBytes(7);
            CharDamage2 = hacker.ReadOffset(hacker.Memory.CharCombatDamageBonus2, 7);
        }

        public override string SpellName(byte b) { return BT2.SpellName(b); }
        public override bool HasTarget(byte b) { return BT2.SpellList.Value.HasTarget(b); }

        public override string GetItem(int iChar)
        {
            return String.Format("Unknown({0})", iChar);
        }

        public override string GetTarget(int iTarget)
        {
            if (iTarget >= 128 && iTarget <= 131)
                return String.Format("Group {0}", (char)('A' + iTarget - 128));
            if (iTarget >= 0 && iTarget <= 6 && iTarget < Names.Length)
                return Names[iTarget];
            return "<Unknown>";
        }
    }

    public class BT2StackInfo
    {
        public byte Byte1;
        public byte Byte2;
        public MainState State;
        public int ActingChar;

        public BT2StackInfo(byte b1, byte b2, MainState state, int acting)
        {
            Byte1 = b1;
            Byte2 = b2;
            State = state;
            ActingChar = acting;
        }

        public string UInt16 { get { return String.Format("{0:X2}{1:X2}", Byte2, Byte1); } }

        public override string ToString()
        {
            return String.Format("{0:X2} {1:X2}: {2}", Byte1, Byte2, State.ToString());
        }
    }

    public class BT2MemoryHacker : BTMemoryHacker
    {
        public override BTMemory Memory { get { return BT2.Memory; } }
        public override List<BTItem> BTItems { get { return BT2.Items; } }
        protected override BTGameInfo CreateGameInfo() { return new BT2GameInfo(); }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new BT2GameInformationControl(main); }
        public override string GetMapEnum(int index) { return String.Format("BT2Map.{0}", Enum.GetName(typeof(BT2Map), (BT2Map)(index))); }
        protected override QuestInfo CreateQuestInfo() { return new BT2QuestInfo(); }
        public override int MaxInventoryChar { get { return 99; } }
        protected override BTTrainingInfo CreateTrainingInfo() { return new BT2TrainingInfo(); }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return BT2RosterFile.Create(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "DK.EXE"; } }

        protected override void OnReinitialized(EventArgs e)
        {
            base.OnReinitialized(e);
        }

        public BT2MemoryHacker()
        {
            m_game = GameNames.BardsTale2;
        }

        public string DumpStack()
        {
            byte[] stack = ReadOffset(BT2.Memory.Stack - 512, 512);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 512; i += 2)
            {
                sb.AppendFormat("{0,-3}:{1:X4}\r\n", i, BitConverter.ToUInt16(stack, 510 - i));
            }
            return sb.ToString();
        }

        //private MainState testState = MainState.Unknown;

        protected override BTGameState GetMainState(byte[] stack, byte[] states = null)
        {
            BTGameState state = new BTGameState();
            const int iStart = 160;
            const int iEnd = iStart + 256;

            state.Main = MainState.Unknown;
            int iOffset = -1;
            int iActingChar = 0;

            foreach (StackGuess guess in BT2.Memory.StackGuesses)
            {
                if (state.Main != MainState.Unknown)
                    break;

                int iStackOffset = guess.Location - Memory.Stack + stack.Length;
                if (Global.CompareBytes(stack, guess.Value, iStackOffset, 0, guess.Value.Length))
                {
                    state.Main = guess.State;
                    if (guess.CharacterOffset != 0)
                        iActingChar = stack[iStackOffset + guess.CharacterOffset];
                }
            }

            foreach (BT2StackInfo info in BT2.Memory.StackLocations)
            {
                if (state.Main != MainState.Unknown)
                    break;
                iOffset = Global.AlignedFirstIndexOf(stack, info.Byte1, info.Byte2, iStart, iEnd);
                if (iOffset >= 0)
                {
                    state.Main = info.State;
                    if (info.ActingChar != 0)
                        iActingChar = stack[iOffset + info.ActingChar];
                }
            }

            //if (state.Main != testState) { Global.Log("New State: {0}", state.Main.ToString()); testState = state.Main; }

            if (state.Main == MainState.Treasure)
            {
                switch (stack[iOffset - 0x18])
                {
                    case 0x83:
                        state.Main = MainState.TreasureWhoWillInspect;
                        break;
                    case 0xD7:
                        state.Main = MainState.TreasureWhoWillDisarm;
                        break;
                    case 0xAD:
                        state.Main = MainState.TreasureWhoWillOpen;
                        break;
                    case 0x02:
                        state.Main = MainState.TreasureWhoWillCalfo;
                        break;
                }
            }

            switch (state.Main)
            {
                case MainState.CombatOptions:
                case MainState.Combat:
                case MainState.CombatSelectSpell:
                case MainState.Treasure:
                case MainState.TreasureWhoWillCalfo:
                case MainState.TreasureWhoWillDisarm:
                case MainState.TreasureWhoWillInspect:
                case MainState.TreasureWhoWillOpen:
                    state.InCombat = true;
                    break;
                case MainState.SelectSpell:
                case MainState.SelectBardSong:
                    iActingChar = ReadByte(Memory.CastingChar);
                    break;
                default:
                    break;
            }

            Global.FixRange(ref iActingChar, 0, 6);
            state.ActingCharAddress = iActingChar;
            state.ActingCaster = iActingChar;
            state.ActingCombatChar = iActingChar;
            return state;
        }

        protected override List<Item> GetSuperItems(GenericClass btClass)
        {
            List<Item> items = new List<Item>(8);
            BTItem snare = BT2.Item(BT2ItemIndex.SpectreSnare);
            BTItem staff = BT2.Item(BT2ItemIndex.WarStaff);
            BTItem bracers = BT2.Item(BT2ItemIndex.Bracers4);
            BTItem shield = BT2.Item(BT2ItemIndex.DiamondShield);
            BTItem dragshield = BT2.Item(BT2ItemIndex.DragonShield);
            BTItem helm = BT2.Item(BT2ItemIndex.DiamondHelm);
            BTItem wargloves = BT2.Item(BT2ItemIndex.Wargloves);
            BTItem gloves = BT2.Item(BT2ItemIndex.LeatherGloves);
            BTItem cloak = BT2.Item(BT2ItemIndex.ElfCloak);

            switch (btClass)
            {
                case GenericClass.Warrior:
                    items.Add(snare);
                    items.Add(BT2.Item(BT2ItemIndex.DiamondPlate));
                    items.Add(shield);
                    items.Add(helm);
                    items.Add(wargloves);
                    break;
                case GenericClass.Paladin:
                    items.Add(snare);
                    items.Add(BT2.Item(BT2ItemIndex.DiamondPlate));
                    items.Add(BT2.Item(BT2ItemIndex.PureShield));
                    items.Add(wargloves);
                    items.Add(helm);
                    break;
                case GenericClass.Bard:
                    items.Add(snare);
                    items.Add(bracers);
                    items.Add(shield);
                    items.Add(BT2.Item(BT2ItemIndex.AdamantHelm));
                    items.Add(BT2.Item(BT2ItemIndex.PipesOfPan));
                    items.Add(BT2.Item(BT2ItemIndex.MithrilGloves));
                    break;
                case GenericClass.Hunter:
                    items.Add(snare);
                    items.Add(bracers);
                    items.Add(dragshield);
                    items.Add(helm);
                    items.Add(BT2.Item(BT2ItemIndex.Gauntlets));
                    break;
                case GenericClass.Monk:
                    items.Add(staff);
                    items.Add(bracers);
                    items.Add(dragshield);
                    items.Add(gloves);
                    items.Add(BT2.Item(BT2ItemIndex.MithrilHelm));
                    break;
                case GenericClass.Rogue:
                    items.Add(staff);
                    items.Add(bracers);
                    items.Add(dragshield);
                    items.Add(gloves);
                    break;
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                case GenericClass.Sorcerer:
                    items.Add(staff);
                    items.Add(bracers);
                    items.Add(dragshield);
                    items.Add(gloves);
                    break;
                case GenericClass.Wizard:
                case GenericClass.Archmage:
                    items.Add(staff);
                    items.Add(bracers);
                    items.Add(BT2.Item(BT2ItemIndex.WizHelm));
                    items.Add(dragshield);
                    break;
                default:
                    break;
            }

            if (items.Count < 8)
                items.Add(cloak);
            if (items.Count < 8 && !items.Any(i => i.ItemBasicType == Item.ItemNounType.Ring))
                items.Add(BT2.Item(BT2ItemIndex.TroyP));

            foreach (BTItem item in items)
                item.ChargesCurrent = 255;

            return items;
        }

        public override int GetLightDistance(Point ptLocation)
        {
            if (!IsDungeon(GetCurrentMapIndex()))
                return 3;
            return ReadByte(BT2.Memory.LightDistance);
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int iSize = CharacterSize;
            int iMemorySize = CharacterMemorySize;

            int offset = iAddress * iSize;
            CharacterOffsets offsets = BT2.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + CharacterSize > info.Bytes.Length + 1)
                return false;

            GenericClass btClass = BT1Character.GetBasicClass((BT12Class)info.Bytes[offset + offsets.Class]);

            byte[] bytes = new byte[] { 99, 99, 99, 99, 99, 99, 99, 99, 99, 99 }; // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)BTCondition.Good;
            info.Bytes[offset + offsets.Level] = 99;
            info.Bytes[offset + offsets.LevelMod] = 99;
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentSP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxSP, 9999);
            Global.SetInt32(info.Bytes, offset + offsets.Gold, 200000000);  // Low enough so that "pool gold" doesn't overflow the value
            Global.SetInt32(info.Bytes, offset + offsets.Experience, BTCharacter.XPForLevel(Game, btClass, 99));
            bytes = new byte[] { 7, 7, 7, 7, 7 };  // Spell levels
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.SpellLevel, bytes.Length);

            byte[] bytesNew = new byte[iMemorySize * 7];
            for(int i = 0; i < 7; i++)
                Buffer.BlockCopy(info.Bytes, i * iSize + 17, bytesNew, i * iMemorySize, iMemorySize);

            WriteOffset(Memory.PartyInfo, bytesNew);

            List<Item> items = GetSuperItems(btClass);

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override Shops GetShopInfo()
        {
            Shops shops = new Shops();

            GameState state = GetGameState();
            shops.InShop = (state.Main == MainState.ShopBuyItem || state.Main == MainState.ShopInspectChar);

            if (!shops.InShop)
                return null;

            LocationInformation info = GetLocation();

            int iShopSize = ReadByte(Memory.NumItemsInShop);

            shops.Inventories = new List<ShopInventory>(1);
            MemoryBytes bytesShops = ReadOffset(Memory.ShopInventory, iShopSize);
            if (bytesShops == null)
                return null;

            BT2ShopInventory inv = new BT2ShopInventory(bytesShops.Bytes);

            inv.Town = "Garth's";

            if (inv.AllItems == null)
                return null;
            shops.RawBytes = bytesShops;
            shops.Inventories.Add(inv);

            return shops;
        }

        public override bool SetShopItem(ShopItem item)
        {
            if (item.Offset < 0 || item.Offset > 46)
                return false;

            WriteByte(BT2.Memory.ShopInventory + item.Offset, (byte)item.Item.Index);
            return true; 
        }

        public override IEnumerable<Monster> GetMonsterList() { return BT2.Monsters; }

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
            MemoryBytes mapSpecials = ReadOffset(Memory.MapSpecials, 484);
            if (party == null)
                return null;
            MemoryBytes townMap = ReadOffset(Memory.TownMap, 30 * 30);
            if (townMap == null)
                return null;
            return new BT2QuestData(party, GetLocation(), GetGameState() as BTGameState, mapSpecials, townMap, GetCurrentMapBytes(), new BT2Effects(this));
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new BT2RosterFile(Global.CombineRoster(Game), bSilent);
        }

        public override string BagOfHoldingRequirement { get { return "in a Town"; } }

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

            List<Item> backpack = new List<Item>(8);
            for (int i = 0; i < 8; i++)
            {
                int iItem = BitConverter.ToInt16(bytesChar, i * 2 + BT2.Offsets.Inventory);
                if (iItem != 0)
                    backpack.Add(Global.BT2.GetClonedItem(iItem));
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

            // Bard's Tale 1 doesn't have a fixed-size roster, so we need to search the entire roster
            // before using a new file
            byte[] bytes = new byte[CharacterSize];
            if (iStart >= m_roster.Chars.Count)
                iStart = m_roster.Chars.Count - 1;

            while (iStart >= 0)
            {
                BTCharacter BT2Char = null;
                if (iStart < m_roster.Chars.Count)
                    BT2Char = BTCharacter.Create(Game, 0, m_roster.Chars[iStart].Bytes, 0);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                    case InventoryCharAction.FindOrCreate:
                        if (BT2Char != null && BT2Char.Name == "INVENTORY")
                            return iStart;
                        break;
                    case InventoryCharAction.FindPotential:
                        if (BT2Char == null || BT2Char.Name == "INVENTORY")
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
            // Remove any existing inventory characters so that they can be saved starting from character 99
            if (!ValidateRosterFile())
                return false;

            try
            {
                ((BTRosterFile)m_roster).DeleteInventoryChars();
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

            Buffer.BlockCopy(bytes.Items, 0, bytesChar, BT2.Offsets.Inventory, 16);
            m_roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        public static byte[] GetInventoryChar() { return Properties.Resources.BT2InventoryChar; }
        public override byte[] GetInventoryCharBytes() { return GetInventoryChar(); }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            BT2MapData bt2Data = new BT2MapData(mb);
            bt2Data.LiveOnly = true;
            return bt2Data;
        }

        public override bool VisitFacingSquare(BasicLocation location, MapSheet sheet)
        {
            return sheet != null && location != null && ((location.MapIndex & 0x00ff) == 0) && ((sheet.GameMapIndex & 0x00ff) == 0);
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            BT2MapData data = new BT2MapData(GetCurrentMapBytes());

            data.Index = GetCurrentMapIndex();
            data.Title = GetMapTitlePair(data.Index);

            if (IsDungeon(data.Index))
            {
                data.Specials = Global.Combine(ReadOffset(Memory.MapSpecials, 22 * 22), ReadOffset(Memory.MapSpecials2, 22 * 22));
                data.CustomSquares = ReadOffset(Memory.MapCustomSquares, 32);
                data.FixedEncounters = ReadOffset(Memory.MapFixedSquares, 32);
                data.Teleport = ReadOffset(Memory.MapTeleport, 32);
                data.Monsters = ReadOffset(BT2.Memory.CurrentMonsters, 32 * 23);
            }
            data.Bounds = new Rectangle(0, 0, data.Width, data.Height);

            return data;
        }

        public override bool IsDungeon(int iMap) { return IsDungeon((BT2Map)iMap); }

        private bool IsDungeon(BT2Map map)
        {
            switch (map)
            {
                case BT2Map.Wilderness:
                case BT2Map.Tangramayne:
                case BT2Map.Colosse:
                case BT2Map.Corinth:
                case BT2Map.Ephesus:
                case BT2Map.Philippi:
                case BT2Map.Thessalonica:
                    return false;
                default:
                    return true;
            }
        }

        public override MapBytes GetCurrentMapBytes()
        {
            int iIndex = GetCurrentMapIndex();

            if (iIndex == (int)BT2Map.Wilderness)
                return new MapBytes(ReadOffset(BT2.Memory.Wilderness, 32 * 48), 32, 48);

            MapBytes mb = null;
            if (!IsDungeon(iIndex))
            {
                mb = new MapBytes(ReadOffset(BT2.Memory.TownMap, 16 * 16 + 1), 16, 16);
                mb.Bytes[256] = 0;
                return mb;
            }

            mb = new MapBytes(ReadOffset(BT2.Memory.Map, 22 * 22 + 1), 22, 22);
            mb.Bytes[22*22] = 0;
            return mb;
        }

        private Size GetMapSize(BT2Map map)
        {
            if (IsDungeon(map))
                return new Size(22, 22);
            if (map == BT2Map.Wilderness)
                return new Size(32, 48);
            return new Size(16, 16);
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            int iIndex = GetCurrentMapIndex();
            if (!IsDungeon(iIndex))
            {
                Size sz = GetMapSize((BT2Map)iIndex);
                return new Point(ReadByte(Memory.LocationEastTown), sz.Height - 1 - ReadByte(Memory.LocationNorthTown));
            }
            return new Point(ReadByte(Memory.LocationEast), ReadByte(Memory.LocationNorth));
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            int iIndex = GetCurrentMapIndex();
            if (!IsDungeon(iIndex))
            {
                Size sz = GetMapSize((BT2Map)iIndex);
                WriteByte(Memory.LocationEastTown, (byte)ptLocation.X);
                return WriteByte(Memory.LocationNorthTown, (byte)(sz.Height - 1 - ptLocation.Y));
            }
            WriteByte(Memory.LocationEast, (byte)ptLocation.X);
            return WriteByte(Memory.LocationNorth, (byte)ptLocation.Y);
        }

        public override int GetCurrentMapIndex()
        {
            byte bMain = ReadByte(Memory.MainMapIndex);
            byte bSub1 = ReadByte(Memory.SubMapIndex);
            byte bSub2 = ReadByte(BT2.Memory.SubMapIndex2);
            int index = (bMain << 16) | (bSub1 << 8) | bSub2;

            switch (index)
            {
                case 0x000202: return (int)BT2Map.Wilderness;
                case 0x000622: return (int)BT2Map.GreyCrypt1;
                case 0x000C44: return (int)BT2Map.GreyCrypt2;
                case 0x000C13: return (int)BT2Map.FanskarsCastle;
                case 0x010202: return (int)BT2Map.Tangramayne;
                case 0x010101: return (int)BT2Map.DarkDomain1;
                case 0x010303: return (int)BT2Map.DarkDomain2;
                case 0x010806: return (int)BT2Map.DarkDomain3;
                case 0x010506: return (int)BT2Map.DarkDomain4;
                case 0x020202: return (int)BT2Map.Ephesus;
                case 0x02300C: return (int)BT2Map.Tombs1;
                case 0x02120F: return (int)BT2Map.Tombs2;
                case 0x021308: return (int)BT2Map.Tombs3;
                case 0x030202: return (int)BT2Map.Philippi;
                case 0x031E20: return (int)BT2Map.DargothsTower1;
                case 0x032015: return (int)BT2Map.DargothsTower2;
                case 0x031210: return (int)BT2Map.DargothsTower3;
                case 0x031619: return (int)BT2Map.DargothsTower4;
                case 0x031027: return (int)BT2Map.DargothsTower5;
                case 0x040202: return (int)BT2Map.Colosse;
                case 0x041442: return (int)BT2Map.DestinyStone1;
                case 0x04501E: return (int)BT2Map.DestinyStone2;
                case 0x041720: return (int)BT2Map.DestinyStone3;
                case 0x050202: return (int)BT2Map.Corinth;
                case 0x052613: return (int)BT2Map.OsconsFortress1;
                case 0x052618: return (int)BT2Map.OsconsFortress2;
                case 0x053031: return (int)BT2Map.OsconsFortress3;
                case 0x053A1C: return (int)BT2Map.OsconsFortress4;
                case 0x060202: return (int)BT2Map.Thessalonica;
                case 0x062114: return (int)BT2Map.MazeOfDread1;
                case 0x062422: return (int)BT2Map.MazeOfDread2;
                case 0x062810: return (int)BT2Map.MazeOfDread3;
                default: return -1;
            }
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((BT2Map)index)
            {
                case BT2Map.Wilderness: return new MapTitleInfo(index, "Wilderness");
                case BT2Map.Tangramayne: return new MapTitleInfo(index, "Tangramayne");
                case BT2Map.Ephesus: return new MapTitleInfo(index, "Ephesus");
                case BT2Map.Philippi: return new MapTitleInfo(index, "Philippi");
                case BT2Map.Colosse: return new MapTitleInfo(index, "Colosse");
                case BT2Map.Corinth: return new MapTitleInfo(index, "Corinth");
                case BT2Map.Thessalonica: return new MapTitleInfo(index, "Thessalonica");
                case BT2Map.DarkDomain1: return new MapTitleInfo(index, "Dark Domain, Level 1");
                case BT2Map.DarkDomain2: return new MapTitleInfo(index, "Dark Domain, Level 2");
                case BT2Map.DarkDomain3: return new MapTitleInfo(index, "Dark Domain, Level 3");
                case BT2Map.DarkDomain4: return new MapTitleInfo(index, "Dark Domain, Level 4");
                case BT2Map.DargothsTower1: return new MapTitleInfo(index, "Dargoth's Tower, Level 1");
                case BT2Map.DargothsTower2: return new MapTitleInfo(index, "Dargoth's Tower, Level 2");
                case BT2Map.DargothsTower3: return new MapTitleInfo(index, "Dargoth's Tower, Level 3");
                case BT2Map.DargothsTower4: return new MapTitleInfo(index, "Dargoth's Tower, Level 4");
                case BT2Map.DargothsTower5: return new MapTitleInfo(index, "Dargoth's Tower, Level 5");
                case BT2Map.Tombs1: return new MapTitleInfo(index, "The Tombs, Level 1");
                case BT2Map.Tombs2: return new MapTitleInfo(index, "The Tombs, Level 2");
                case BT2Map.Tombs3: return new MapTitleInfo(index, "The Tombs, Level 3");
                case BT2Map.FanskarsCastle: return new MapTitleInfo(index, "Fanskar's Castle");
                case BT2Map.GreyCrypt1: return new MapTitleInfo(index, "Grey Crypt, Level 1");
                case BT2Map.GreyCrypt2: return new MapTitleInfo(index, "Grey Crypt, Level 2");
                case BT2Map.DestinyStone1: return new MapTitleInfo(index, "Destiny Stone, Level 1");
                case BT2Map.DestinyStone2: return new MapTitleInfo(index, "Destiny Stone, Level 2");
                case BT2Map.DestinyStone3: return new MapTitleInfo(index, "Destiny Stone, Level 3");
                case BT2Map.MazeOfDread1: return new MapTitleInfo(index, "Maze of Dread, Level 1");
                case BT2Map.MazeOfDread2: return new MapTitleInfo(index, "Maze of Dread, Level 2");
                case BT2Map.MazeOfDread3: return new MapTitleInfo(index, "Maze of Dread, Level 3");
                case BT2Map.OsconsFortress1: return new MapTitleInfo(index, "Oscon's Fortress, Level 1");
                case BT2Map.OsconsFortress2: return new MapTitleInfo(index, "Oscon's Fortress, Level 2");
                case BT2Map.OsconsFortress3: return new MapTitleInfo(index, "Oscon's Fortress, Level 3");
                case BT2Map.OsconsFortress4: return new MapTitleInfo(index, "Oscon's Fortress, Level 4");
                default: return new MapTitleInfo(index, String.Format("Unknown {0}", index));
            }
        }

        public static string GetTownSpecial(int iSpecial)
        {
            switch (iSpecial)
            {
                case 21: return "Inn";
                case 38: return "Temple";
                case 55: return "Adventurer's Guild";
                case 77: return "Review Board";
                case 89: return "Roscoe's Energy Emporium";
                case 136: return "Garth's Equipment Shoppe";
                case 154: return "Casino";
                case 171: return "Bedder's Bank for the Bold";
                case 241: return "Maze of Dread";
                default: return $"Special #{iSpecial}";
            }
        }

        protected override int GetNumChars()
        {
            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, BT2Character.SizeInMemory * 7);
            if (bytes == null)
                return 0;

            int iNumChars = 0;
            for (int i = 0; i < 7; i++)
                if ((bytes.Bytes[i * BT2Character.SizeInMemory] & 0x01) == 0)
                    iNumChars++;

            return iNumChars;
        }

        private BTPartyInfo ReadBT2PartyInfo()
        {
            byte numChars = (byte)GetNumChars();
            if (numChars > 7)
                numChars = 7;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new BTPartyInfo(new byte[0], new byte[0], numChars);

            BTGameState state = GetGameState() as BTGameState;

            MemoryBytes bytesNames = ReadOffset(Memory.PartyNames, 39 * 7);
            MemoryBytes bytesStats = ReadOffset(Memory.PartyInfo, BT2Character.SizeInMemory * 7);
            int iInspecting = 0;
            if (state.Casting)
                iInspecting = state.ActingCaster;
            else if (state.InCombat)
                iInspecting = state.ActingCombatChar;
            else
                iInspecting = state.ActingCharAddress;

            Global.FixRange(ref iInspecting, 0, 6);

            MemoryStream ms = new MemoryStream(7 * CharacterSize);
            for (int i = 0; i < 7; i++)
            {
                ms.Write(bytesNames, i * 39, 16);
                ms.WriteByte(1);    // This byte is in the roster file but not in memory
                ms.Write(bytesStats, i * BT2Character.SizeInMemory, BT2Character.SizeInMemory);
            }

            BTPartyInfo info = new BTPartyInfo(ms.ToArray(), GetMarchingOrder(), numChars);

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

            return ReadBT2PartyInfo();
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
        protected override bool CanUseBag(int iMapIndex) { return Global.Cheats || ((iMapIndex >= 0x0100) && ((iMapIndex & 0xff) == 0)); }

        public override string SpellType1 { get { return "Conjurer"; } }
        public override string SpellType2 { get { return "Magician"; } }
        public override string SpellType3 { get { return "Sorcerer"; } }
        public override string SpellType4 { get { return "Wizard"; } }
        public override string SpellType5 { get { return "Archmage"; } }
        public override string SpellType6 { get { return "Bard"; } }

        public override byte[] GetBackpackBytes(List<Item> items)
        {
            byte[] bytes = Global.NullBytes(24);
            for (int i = 0; i < 8; i++)
            {
                if (i >= items.Count)
                    break;
                Global.SetInt16(bytes, i * 2, items[i].Index);
                bytes[i + 16] = (byte)items[i].ChargesCurrent;
            }
            return bytes;
        }

        public static MonsterName ExtractMonsterNames(byte[] monsters, int iOffset)
        {
            byte[] monster = Global.Subset(monsters, iOffset, 16);
            string strMonster = Global.GetLowAsciiString(monster);

            int iSlash = strMonster.IndexOf('/');
            int iBack1 = strMonster.IndexOf('\\');
            int iBack2 = strMonster.IndexOf('\\', iBack1 + 1);
            if (iBack2 == -1)
                iBack2 = strMonster.Length;

            string strSingular = "";
            string strPlural = "";

            if (iSlash == -1 && iBack1 != -1)
            {
                strSingular = strMonster.Substring(0, iBack1);
                strPlural = strMonster.Substring(0, iBack1);
            }
            else if (iSlash == -1 || iBack1 == -1 || iBack2 == -1)
            {
                strSingular = strMonster;
                strPlural = strMonster;
            }
            else
            {
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

        public override Size GetMapDimensions(int iIndex)
        {
            if (iIndex == (int)BT2Map.Wilderness)
                return new Size(32, 48);
            else return IsDungeon(iIndex) ? new Size(22, 22) : new Size(16, 16);
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            byte[] bytesNull = Global.NullBytes(4);

            MemoryBytes mbDistances = ReadOffset(Memory.MonsterDistances, 4);
            if (mbDistances == null)
                return null;

            MemoryBytes mbMonsters = ReadOffset(Memory.MonsterIndices, 128);
            if (mbMonsters == null || Global.Compare(mbMonsters.Bytes, bytesNull))
                return null;

            MemoryBytes mbNumAlive = ReadOffset(Memory.MonsterNumAlive, 4);
            if (mbNumAlive == null)
                return null;

            MemoryBytes mbHP = ReadOffset(Memory.MonsterHP, 256);
            Point ptParty = GetLocation().PrimaryCoordinates;

            BTCombatEffects effects = new BT2CombatEffects(this);

            if (!bForceNew &&
                m_lastEncounterInfo != null &&
                Global.Compare(m_lastEncounterInfo.MonsterIndices, mbMonsters.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Living, mbNumAlive.Bytes) &&
                Global.Compare(m_lastEncounterInfo.MonsterHP, mbHP.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Effects.Bytes, effects.Bytes) &&
                m_lastEncounterInfo.InCombat == effects.GameState.InCombat &&
                m_lastEncounterInfo.Effects.GameState.IsTreasure == effects.GameState.InCombat)
                return m_lastEncounterInfo;

            BT2EncounterInfo info = new BT2EncounterInfo(mbMonsters.Bytes, mbHP.Bytes, mbNumAlive.Bytes, mbDistances, effects);
            if (effects.GameState.IsTreasure)
            {
                info.SearchResults = new BTSearchResults((BTTrapInfo.BT12Trap)effects.Trap,
                    ReadByte(Memory.MapTreasureIndex),
                    ReadOffset(Memory.EncounterMonstersKilled, 4),
                    ReadOffset(Memory.MapGoldMax, 8),
                    ReadOffset(Memory.TreasureMinimums, 8),
                    ReadOffset(Memory.TreasureRanges, 8));
            }
            info.Party = GetPartyInfo();

            if (!effects.GameState.InCombat)
                return null;

            int iTotal = 0;
            Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
            for (int i = 0; i < 4; i++)
            {
                if (info.MonsterIndices[i] < 1)
                    continue;
                for (int j = 0; j < mbNumAlive.Bytes[i]; j++)
                {
                    BT2Monster monster = new BT2Monster(0, mbMonsters.Bytes, i * 32);
                    monster.CurrentHP = BitConverter.ToInt16(info.MonsterHP, i * 64 + (j * 2));
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

            BT2EncounterInfo bi = info as BT2EncounterInfo;

            WriteOffset(Memory.MonsterIndices, bi.MonsterIndices);
            WriteOffset(Memory.MonsterNumAlive, bi.Living);
            WriteOffset(Memory.MonsterDistances, bi.Distances);
            return WriteOffset(Memory.MonsterHP, bi.MonsterHP);
        }

        public override string[] GetCharacterNames() { return GetCharacterNames(39, 7); }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            BTCharCreationInfo info = new BTCharCreationInfo();
            info.Race = BTCharacter.GetBasicRace((BTRace)ReadByte(Memory.CreationRace));
            MemoryBytes bytes = ReadOffset(Memory.CreationStats, 16);
            info.AttributesModified = new byte[6];
            info.AttributesOriginal = new byte[6];
            info.AttributesModified[0] = bytes.Bytes[0];
            info.AttributesModified[1] = bytes.Bytes[1];
            info.AttributesModified[2] = bytes.Bytes[2];
            info.AttributesModified[3] = bytes.Bytes[3];
            info.AttributesModified[4] = bytes.Bytes[4];
            info.AttributesModified[5] = bytes.Bytes[14];
            Buffer.BlockCopy(info.AttributesModified, 0, info.AttributesOriginal, 0, 6);
            info.State = GetGameState();
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[] { info.AttributesOriginal[0], info.AttributesOriginal[1],
                info.AttributesOriginal[2], info.AttributesOriginal[3], info.AttributesOriginal[4],
                  info.AttributesModified[0], info.AttributesModified[1],
                 info.AttributesModified[2], info.AttributesModified[3], info.AttributesModified[4],
                 0, 0, 0, 0, info.AttributesOriginal[5], 0};
            WriteOffset(Memory.CreationStats, bytes);
            return true;
        }

        protected override void WriteTrainingInfo(BTTrainingInfo btInfo, int iAddress)
        {
            byte[] HPSP = Global.Subset(btInfo.Party.Bytes, btInfo.Party.CharacterSize * iAddress + BT2.Offsets.MaxHP, 8);
            WriteOffset(BT2.Memory.PartyInfo + CharacterMemorySize * iAddress + BT2.Offsets.MaxHP - 17, HPSP);
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

        protected override Point CheckInvalidCoordinates(int iMapIndex, Point pt)
        {
            // Sometimes the party shows at (0,15) while the in the guild; that's not a valid location in any town
            if (pt == new Point(0, 15))
            {
                switch ((BT2Map)iMapIndex)
                {
                    case BT2Map.Colosse: return BT2.Spots.ColosseGuild.Location;
                    case BT2Map.Corinth: return BT2.Spots.CorinthGuild.Location;
                    case BT2Map.Ephesus: return BT2.Spots.EphesusGuild.Location;
                    case BT2Map.Philippi: return BT2.Spots.PhilippiGuild.Location;
                    case BT2Map.Tangramayne: return BT2.Spots.TangramayneGuild.Location;
                    case BT2Map.Thessalonica: return BT2.Spots.ThessalonicaGuild.Location;
                    default: return pt;
                }
            }
            return pt;
        }

        protected override bool IsOutside(int iMap) { return iMap == (int)BT2Map.Wilderness; }

        public HashSet<BT2ItemIndex> GetAllInventoryItems()
        {
            HashSet<BT2ItemIndex> list = new HashSet<BT2ItemIndex>();
            int iChars = GetNumChars();

            MemoryBytes mb = ReadOffset(Memory.PartyInfo, iChars * CharacterMemorySize);
            if (mb == null)
                return list;

            for (int i = 0; i < iChars; i++)
            {
                for (int j = 0; j < MaxBackpackSize; j++)
                {
                    BT2ItemIndex item = (BT2ItemIndex) BitConverter.ToInt16(mb.Bytes, i * CharacterMemorySize + BT2.Offsets.Inventory - 17 + (j * 2));
                    if (item != BT2ItemIndex.None && !list.Contains(item))
                        list.Add(item);
                }
            }
            return list;
        }
    }
}
