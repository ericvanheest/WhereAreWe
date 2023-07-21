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
    public class BT1Memory : BTMemory
    {
        // Search for "Not enough gold"
        public override byte[] MainSearch { get { return new byte[] { 0x4E, 0x6F, 0x74, 0x20, 0x65, 0x6E, 0x6F, 0x75, 0x67, 0x68, 0x20, 0x67, 0x6F, 0x6C, 0x64 }; } }

        public override int MainBlockSVN { get { return -154334; } }
        public override int MainBlockOldSVN { get { return -153966; } }
        public override int MainBlockNonSVN { get { return -154334; } }

        public override int FacingTown { get { return 52760; } }
        public override int Facing { get { return 50652; } }
        public override int LocationNorthTown { get { return 47832; } }
        public override int LocationEastTown { get { return 53744; } }
        public override int LocationNorth { get { return 47964; } }
        public override int LocationEast { get { return 52748; } }
        public override int PartyInfo { get { return 51502; } }
        public override int PartyNames { get { return -884; } }
        public override int CampInspectingChar { get { return 54694; } }
        public override int MarchingOrder { get { return -639; } }
        public override int InspectingChar { get { return 54724; } }
        public override int ShopInspectingChar { get { return 54700; } }
        public override int CastingChar { get { return 52753; } }
        public override int ShoppingChar { get { return 54702; } }
        public override int ShopInventory { get { return 54564; } }
        public override int CreationStats { get { return 54632; } }
        public override int CreationRace { get { return 54628; } }
        public override int CombatActingChar1 { get { return 54628; } }
        public override int CombatActingChar2 { get { return 54650; } }
        public override int CombatActingChar3 { get { return 54680; } }
        public override int Map { get { return -36366; } }
        public override int MapSpecials { get { return -35854; } }
        public override int MainMapIndex { get { return 53747; } }
        public override int SubMapIndex { get { return 52762; } }
        public override int MapStrings { get { return 52808; } }
        public override int MapSquareStrings { get { return 50740; } }
        public override int MonsterHP { get { return 50996; } }
        public override int MonsterIndices { get { return 52276; } }
        public override int MonsterNumAlive { get { return 49398; } }
        public override int AskCastSpell { get { return -74; } }
        public override int AskWhichSpell { get { return 54681; } }
        public override int AskWhichSpellCombat { get { return 54567; } }
        public override int AskWhichSpellCombat2 { get { return 54589; } }
        public override int AskWhichSong { get { return 54679; } }
        public override int GameTimeHours { get { return -73; } }
        public override int GameTimeSeconds { get { return 10747; } }
        public override int SpellIcon1 { get { return 12608; } }
        public override int SpellIcon2 { get { return 12609; } }
        public override int SpellIcon3 { get { return 12610; } }
        public override int SpellIcon4 { get { return 12611; } }
        public override int SpellIcon5 { get { return 12612; } }
        public override int LightDistance { get { return -112; } }
        public override int LevitationDuration { get { return 30445; } }
        public override int ShieldDuration { get { return 51408; } }
        public override int LightDuration { get { return 46918; } }
        public override int DetectionDuration { get { return 51409; } }
        public override int CompassDuration { get { return 47837; } }
        public override int AdventuringSong { get { return 52280; } }
        public override int CombatSong { get { return 52695; } }
        public override int SongDuration { get { return 21; } }
        public override int CharCombatACBonus { get { return 53709; } }
        public override int CharCombatDamageBonus { get { return 46912; } }
        public override int EnemyDamageBonus { get { return 50648; } }
        public override int EnemyACBonus { get { return 47844; } }
        public override int EnemyLoseTurn { get { return 47838; } }
        public override int PartyCombatACBonus { get { return 53715; } }
        public override int PartyCombatMagicResist { get { return 30457; } }
        public override int PartyCombatOptions { get { return 46867; } }
        public override int PartyCombatSubOptions1 { get { return 46925; } }
        public override int PartyCombatSubOptions2 { get { return 47851; } }
        public override int PartyCombatSelectedSpells { get { return 52063; } }
        public override int SummonedCreature { get { return 51410; } }
        public override int TreasureState { get { return 28432; } }
        public override int TrapType { get { return 46909; } }
        public override int TrapExamined { get { return 46924; } }
        public override int ItemACBonus { get { return 3930; } }
        public override int ItemTypes { get { return 4058; } }
        public override int ItemUsableBy { get { return 4186; } }
        public override int ItemEffects { get { return 4314; } }
        public override int ItemDamage { get { return 7324; } }
        public override int ItemValues { get { return 14778; } }
        public override int ItemCharges { get { return 0; } }
        public override int ItemEquipEffect { get { return 0; } }
        public override int MonsterGroup { get { return 4692; } }
        public override int MonsterAC { get { return 4820; } }
        public override int MonsterDamage { get { return 4948; } }
        public override int MonsterExp { get { return 5076; } }
        public override int ScreenText { get { return 50740; } }
        public override int TreasureRanges { get { return 6048; } }
        public override int TreasureMinimums { get { return 6064; } }
        public override int MapTreasureIndex { get { return 30458; } }
        public override int EncounterMonstersKilled { get { return 47860; } }
        public override int MapGoldMax { get { return 6178; } }
        public override int SwapWallsDoors { get { return 20921; } }
        public override int ForcedEncounters { get { return -35102; } }
        public override int TownMap { get { return 46932; } }
        public override int ImageCaption { get { return -1012; } }
        public override int NumItemsInShop { get { return 54558; } }
        public override int AdvPartyACBonus { get { return 52778; } }
        public override int MonsterDistances { get { return 33305; } }
        public override int CharCombatDamageBonus2 { get { return 0; } }

        // Unfinished
        public override int NumChars { get { return 0; } }
        public override int ItemList { get { return 0; } }
        public override int EncounterInfo { get { return 0; } }

        public override int State1 { get { return 54672; } }
        public override int State2 { get { return 54670; } }
        public override int State3 { get { return 54658; } }
        public override int State4 { get { return -12891; } }
        public override int Stack { get { return 54874; } }
        public override int StackAddressIndicator { get { return 54758; } }
        public override int MapCustomSquares { get { return -80341; } }
        public override int MapFixedSquares { get { return -80341; } }
        public override int MapTeleport { get { return -80341; } }
        public override int MapSpecials2 { get { return -80341; } }
        public override int PartyPerishSeconds { get { return 0; } }
        public override int Counter1 { get { return 0; } }
        public override int PartyBonusAttacks { get { return 0; } }
        public override int SurfaceMapIndex { get { return 0; } }
        public override int StateArray { get { return 0; } }
        public override int CombatActiveSpells { get { return 0; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.BardsTale1]; } }

        // Bard's Tale 1 specific
        public BTStackInfo[] StackLocations;

        public BT1Memory()
        {
            List<BTStackInfo> list = new List<BTStackInfo>();
            list.Add(BTStackInfo.NewAddress(152, MainState.Adventuring, 0x0079));
            list.Add(BTStackInfo.NewAddress(200, MainState.Adventuring, 0x0079));
            list.Add(new BTStackInfo(194, MainState.ShopInspecting, 0x000B));
            list.Add(new BTStackInfo(188, MainState.ShopBuyItem, 0x1FA3));
            list.Add(new BTStackInfo(166, MainState.Shop, 0x0512));
            list.Add(new BTStackInfo(164, MainState.Shop, 0x0512));
            list.Add(BTStackInfo.NewAddress(184, MainState.Pause, 0x01A4));
            list.Add(new BTStackInfo(184, MainState.PreCombat, 0x0B67));

            BTStackInfo.AddSet(list, 214, MainState.CombatSelectBardSong, 0x14A8);
            list.Add(new BTStackInfo(180, MainState.SelectBardSong, 0x14A8));
            list.Add(new BTStackInfo(210, MainState.SelectBardSong, 0x14A8));

            BTStackInfo.AddSet(list, 236, MainState.CombatSelectSpell, 0x1163);
            list.Add(new BTStackInfo(200, MainState.SelectSpell, 0x1163));
            list.Add(new BTStackInfo(230, MainState.SelectSpell, 0x1163));
            BTStackInfo.AddSet(list, 206, MainState.PreCombat, 0x3982);
            BTStackInfo.AddSet(list, 202, MainState.CombatConfirmRound, 0x3E7D);
            BTStackInfo.AddSet(list, 170, MainState.CombatOptions, 0x232D);
            BTStackInfo.AddSet(list, 170, MainState.PreCombat, 0x0707, 0x252F, 0x2328);
            BTStackInfo.AddSet(list, 170, MainState.Combat, 0x23C4);
            list.Add(new BTStackInfo(230, MainState.PreCombat, 0x0707, 0x252F, 0x2328));
            list.Add(new BTStackInfo(224, MainState.Combat, 0x2424));
            list.Add(new BTStackInfo(202, MainState.PreCombat, 0x2424));
            list.Add(new BTStackInfo(176, MainState.PreCombat, 0x042C));
            list.Add(new BTStackInfo(184, MainState.PreCombat, 0x23C4));
            BTStackInfo.AddSet(list, 170, MainState.Treasure, 0x2340);
            list.Add(new BTStackInfo(166, MainState.CampInspecting, 0x000B));
            list.Add(new BTStackInfo(170, MainState.CampInspecting, 0x000B));
            list.Add(new BTStackInfo(200, MainState.CampInspecting, 0x000B));

            list.Add(new BTStackInfo(274, MainState.CreateSelectName, 0x089E));
            list.Add(new BTStackInfo(278, MainState.CreateSelectClass, 0x0799));
            list.Add(new BTStackInfo(278, MainState.CreateSelectRace, 0x0822));
            list.Add(new BTStackInfo(134, MainState.Opening, 0x2492));
            list.Add(new BTStackInfo(138, MainState.Opening2, 0x1BC2));
            list.Add(new BTStackInfo(130, MainState.LoadingGuild, 0x1CCB));
            list.Add(new BTStackInfo(160, MainState.MainMenu, 0x1F9B));

            StackLocations = list.ToArray();
        }
    }

    public enum BT1Map
    {
        None =      -1,
        Unknown =   -1,
        SkaraBrae =  0x0000,
        Cellars =    0x0101,
        Sewers1 =    0x0102,
        Sewers2 =    0x0103,
        Sewers3 =    0x0104,
        Catacombs1 = 0x0301,
        Catacombs2 = 0x0302,
        Catacombs3 = 0x0303,
        Harkyn1 =    0x0401,
        Harkyn2 =    0x0402,
        Harkyn3 =    0x0403,
        Kylearan =   0x0501,
        Mangar1 =    0x0601,
        Mangar2 =    0x0602,
        Mangar3 =    0x0603,
        Mangar4 =    0x0604,
        Mangar5 =    0x0605,
    }

    public class BT1TrainingInfo : BTTrainingInfo
    {
        public BT1Map Map { get { return (BT1Map)MapIndex; } }
    }

    public class BT1GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return BT1MemoryHacker.GetMapTitlePair(iMap); }

        public BT1GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn) { }
    }

    public class BT1GameInfo : BTGameInfo
    {
        public override GameNames Game { get { return GameNames.BardsTale1; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new BT1GameInfoItem("View Dist.", (byte)LightDistance, new OffsetList(BT1.Memory.LightDistance)));
            items.Add(new BT1GameInfoItem("Light", (byte)LightDuration, new OffsetList(BT1.Memory.LightDuration)));
            items.Add(new BT1GameInfoItem("Levitate", (byte)LevitationDuration, new OffsetList(BT1.Memory.LevitationDuration)));
            items.Add(new BT1GameInfoItem("Compass", (byte)CompassDuration, new OffsetList(BT1.Memory.CompassDuration)));
            items.Add(new BT1GameInfoItem("Detect", (byte)DetectionDuration, new OffsetList(BT1.Memory.DetectionDuration)));
            items.Add(new BT1GameInfoItem("Shield", (byte)ShieldDuration, new OffsetList(BT1.Memory.ShieldDuration)));
            items.Add(new BT1GameInfoItem("Adv. Song", (byte)AdventuringSong, new OffsetList(BT1.Memory.AdventuringSong)));
            items.Add(new BT1GameInfoItem("Song Time", (byte)SongDuration, new OffsetList(BT1.Memory.SongDuration)));
            items.Add(new BT1GameInfoItem("Combat Song", (byte)CombatSong, new OffsetList(BT1.Memory.CombatSong)));
            items.Add(new BT1GameInfoItem("AC Bonus", (byte)AdvACBonus, new OffsetList(BT1.Memory.AdvPartyACBonus)));
            if (Global.Debug)
            {
                items.Add(new BT1GameInfoItem("Spell 1", (byte)SpellIcon1, new OffsetList(BT1.Memory.SpellIcon1)));
                items.Add(new BT1GameInfoItem("Spell 2", (byte)SpellIcon2, new OffsetList(BT1.Memory.SpellIcon2)));
                items.Add(new BT1GameInfoItem("Spell 3", (byte)SpellIcon3, new OffsetList(BT1.Memory.SpellIcon3)));
                items.Add(new BT1GameInfoItem("Spell 4", (byte)SpellIcon4, new OffsetList(BT1.Memory.SpellIcon4)));
                items.Add(new BT1GameInfoItem("Spell 5", (byte)SpellIcon5, new OffsetList(BT1.Memory.SpellIcon5)));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new BT1GameInfoItem("Time (hours)", (byte) GameTimeHours, new OffsetList(BT1.Memory.GameTimeHours)));
            items.Add(new BT1GameInfoItem("Main Map", (byte)MainMap, new OffsetList(BT1.Memory.MainMapIndex)));
            items.Add(new BT1GameInfoItem("Sub Map", (byte)SubMap, new OffsetList(BT1.Memory.SubMapIndex)));
            items.Add(new BT1GameInfoItem("Sum. Index", Summon.Index, new OffsetList(BT1.Memory.SummonedCreature + BT1Summon.Offsets.Index)));
            items.Add(new BT1GameInfoItem("Sum. AC", Summon.AC, new OffsetList(BT1.Memory.SummonedCreature + BT1Summon.Offsets.AC)));
            items.Add(new BT1GameInfoItem("Sum. HP", Summon.MaxHP, new OffsetList(BT1.Memory.SummonedCreature + BT1Summon.Offsets.MaxHP)));
            items.Add(new BT1GameInfoItem("Sum. MaxHP", Summon.CurrentHP, new OffsetList(BT1.Memory.SummonedCreature + BT1Summon.Offsets.CurrentHP)));
            items.Add(new BT1GameInfoItem("Risk/Reward", (byte) RiskReward, new OffsetList(BT1.Memory.MapTreasureIndex)));
            items.Add(new BT1GameInfoItem("Trap", (byte)Trap, new OffsetList(BT1.Memory.TrapType)));

            return items;
        }
    }

    public class BT1Summon
    {
        public class Offsets
        {
            public const int Index = 2;
            public const int AC = 26;
            public const int MaxHP = 28;
            public const int CurrentHP = 30;
        }

        public Int16 Index;
        public Int16 AC;
        public Int16 MaxHP;
        public Int16 CurrentHP;
        public byte[] RawBytes;

        public BT1Summon()
        {
            Index = 0;
            AC = 10;
            MaxHP = 0;
            CurrentHP = 0;
            RawBytes = Global.NullBytes(48);
        }

        public BT1Summon(byte[] bytes, int offset = 0)
        {
            RawBytes = Global.Subset(bytes, offset, 48);
            Index = BitConverter.ToInt16(bytes, Offsets.Index + offset);
            AC = BitConverter.ToInt16(bytes, Offsets.AC + offset);
            MaxHP = BitConverter.ToInt16(bytes, Offsets.MaxHP + offset);
            CurrentHP = BitConverter.ToInt16(bytes, Offsets.CurrentHP + offset);
        }
    }

    public class BT1MapData : BTMapData
    {
        public BT1MapData(MapBytes mb)
        {
            Squares = mb.Bytes;
            Specials = null;
            Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            SwapWallsDoors = mb.Bytes[484];
        }
    }

    public class BT1CombatEffects : BTCombatEffects
    {
        public BT1CombatEffects(BTMemoryHacker hacker)
        {
            Init(hacker, 6, 4);
        }

        public override void InitSpecific(BTMemoryHacker hacker)
        {
            CharDamage2 = Global.NullBytes(6);
            CharAC = hacker.ReadOffset(hacker.Memory.CharCombatACBonus, 6);
            PartySubOptions2 = hacker.ReadOffset(hacker.Memory.PartyCombatSubOptions2, 6);
        }

        public override string SpellName(byte b) { return BT1.SpellName(b); }
        public override bool HasTarget(byte b) { return BT1.SpellList.Value.HasTarget(b); }

        public override string GetTarget(int iTarget)
        {
            if (iTarget >= 128 && iTarget <= 131)
                return String.Format("Group {0}", (char)('A' + iTarget - 128));
            if (iTarget >= 1 && iTarget <= 6)
                return Names[iTarget - 1];
            if (iTarget == 0)
                return "Summoned";
            return "<Unknown>";
        }

        public override string GetItem(int iChar)
        {
            int iEffect = PartySubOptions1[iChar];
            int iTarget = PartySubOptions2[iChar];

            if (iEffect< 79)
                return String.Format("Item ({0}{1})", BT1.SpellName(iEffect), 
                    BT1.SpellList.Value.HasTarget(iEffect) ? String.Format(" on {0}", GetTarget(iTarget)) : "");

            switch (iEffect)
            {
                case 79: return String.Format("Item on {0}", GetTarget(iTarget));
                case 87: return String.Format("Firehorn on {0}", GetTarget(iTarget));
                case 89: return "Use Dragonwand";
                case 92: return "Use Dragon Fgn";
                case 93: return "Use Giant Fgn";
                case 94: return "Use Ogre Fgn";
                case 95: return "Use Mongo Fgn";
                case 97: return "Use Old Man Fgn";
                case 99: return "Use Thor Fgn";
                case 100: return "Use Mage Fgn";
                case 101: return "Use Lich Fgn";
                case 102: return "Use Samurai Fgn";
                case 103: return "Use Titan Fgn";
                case 104: return "Use Golem Fgn";
                default: return String.Format("Use Item #{0}", iTarget);
            }
        }
    }

    public class BT1MemoryHacker : BTMemoryHacker
    {
        public override BTMemory Memory { get { return BT1.Memory; } }
        public override List<BTItem> BTItems { get { return BT1.Items; } }
        protected override BTGameInfo CreateGameInfo() { return new BT1GameInfo(); }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new BT1GameInformationControl(main); }
        public override string GetMapEnum(int index) { return String.Format("BT1Map.{0}", Enum.GetName(typeof(BT1Map), (BT1Map)(index))); }
        protected override QuestInfoBase CreateQuestInfo() { return new BT1QuestInfo(); }
        public override int MaxInventoryChar { get { return 99; } }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return BT1RosterFile.Create(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "BARD.EXE"; } }

        protected override void OnReinitialized(EventArgs e)
        {
            base.OnReinitialized(e);
        }

        public BT1MemoryHacker()
        {
            m_game = GameNames.BardsTale1;
        }

        public string DumpStack()
        {
            byte[] stack = ReadOffset(BT1.Memory.Stack - 512, 512);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 512; i += 2)
            {
                sb.AppendFormat("{0,-3}:{1:X4}\r\n", i, BitConverter.ToUInt16(stack, 510 - i));
            }
            return sb.ToString();
        }

        protected override BTGameState GetMainState(byte[] stack, byte[] states = null)
        {
            int iIndicator = ReadUInt16(BT1.Memory.StackAddressIndicator);
            int iAddressOffset = 0x050A - iIndicator;

            BTGameState state = new BTGameState();
            state.Main = MainState.Unknown;
            int offset = -1;

            if (stack == null)
                return state;

            foreach (BTStackInfo info in BT1.Memory.StackLocations)
            {
                if (info == null)
                    return state;
                offset = stack.Length - info.Offset;
                int iValue = BitConverter.ToInt16(stack, offset);
                if (info.Address)
                    iValue += iAddressOffset;

                if (info.Values.Contains(iValue))
                {
                    state.Main = info.State;
                    break;
                }
            }

            int iActingChar = 0;

            switch (state.Main)
            {
                case MainState.Treasure:
                    switch(BitConverter.ToUInt16(stack, offset - 0x32))
                    {
                        case 0x0080: state.Main = MainState.TreasureWhoWillInspect; break;
                        case 0x00AA: state.Main = MainState.TreasureWhoWillOpen; break;
                        case 0x00D4: state.Main = MainState.TreasureWhoWillDisarm; break;
                        case 0x00FF: state.Main = MainState.TreasureWhoWillCalfo; break;
                        case 0x0D06: state.Main = MainState.TreasureCouldNotDisarm; break;
                    }
                    break;
                case MainState.CampInspecting:
                case MainState.ShopInspecting:
                    iActingChar = BitConverter.ToUInt16(stack, offset + 0x14);
                    break;
                case MainState.CombatSelectBardSong:
                    iActingChar = BitConverter.ToUInt16(stack, offset + 0x14);
                    break;
                case MainState.SelectBardSong:
                case MainState.SelectSpell:
                    iActingChar = ReadByte(BT1.Memory.CastingChar);
                    break;
                case MainState.CombatOptions:
                    iActingChar = BitConverter.ToUInt16(stack, offset - 0x18);
                    break;
                case MainState.CombatSelectSpell:
                    iActingChar = BitConverter.ToUInt16(stack, offset + 0x2A);
                    break;
                case MainState.Shop:
                    iActingChar = BitConverter.ToUInt16(stack, offset - 0x06);
                    if (iActingChar > 6)
                        state.Main = MainState.Adventuring;
                    break;
                case MainState.ShopBuyItem:
                    iActingChar = BitConverter.ToUInt16(stack, offset + 0x10);
                    if (iActingChar > 6)
                        state.Main = MainState.Adventuring;
                    break;
            }

            iActingChar--;
            Global.FixRange(ref iActingChar, 0, 5);
            state.ActingCharAddress = iActingChar;
            state.ActingCaster = iActingChar;
            state.ActingCombatChar = iActingChar;
            return state;
        }

        protected override List<Item> GetSuperItems(GenericClass btClass)
        {
            List<Item> items = new List<Item>(8);
            BTItem snare = BT1.Item(BT1ItemIndex.SpectreSnare);
            BTItem staff = BT1.Item(BT1ItemIndex.PowerStaff);
            BTItem bracers = BT1.Item(BT1ItemIndex.Bracers6);
            BTItem shield = BT1.Item(BT1ItemIndex.AdamantShield);
            BTItem arc = BT1.Item(BT1ItemIndex.ArcShield);
            BTItem helm = BT1.Item(BT1ItemIndex.DiamondHelm);
            BTItem wargloves = BT1.Item(BT1ItemIndex.Wargloves);
            BTItem dragon = BT1.Item(BT1ItemIndex.DragonWand);
            BTItem carpet = BT1.Item(BT1ItemIndex.AlisCarpet);
            BTItem cloak = BT1.Item(BT1ItemIndex.ElfCloak);
            BTItem boots = BT1.Item(BT1ItemIndex.SpeedBoots);

            switch (btClass)
            {
                case GenericClass.Warrior:
                    items.Add(snare);
                    items.Add(BT1.Item(BT1ItemIndex.DiamondPlate));
                    items.Add(BT1.Item(BT1ItemIndex.DiamondShield));
                    items.Add(helm);
                    items.Add(wargloves);
                    items.Add(cloak);
                    break;
                case GenericClass.Paladin:
                    items.Add(BT1.Item(BT1ItemIndex.ArcsHammer));
                    items.Add(BT1.Item(BT1ItemIndex.MithrilPlate));
                    items.Add(BT1.Item(BT1ItemIndex.PureShield));
                    items.Add(BT1.Item(BT1ItemIndex.AdamantGloves));
                    items.Add(helm);
                    items.Add(cloak);
                    break;
                case GenericClass.Bard:
                    items.Add(snare);
                    items.Add(bracers);
                    items.Add(shield);
                    items.Add(BT1.Item(BT1ItemIndex.TravelHelm));
                    items.Add(BT1.Item(BT1ItemIndex.PipesofPan));
                    items.Add(wargloves);
                    items.Add(boots);
                    break;
                case GenericClass.Hunter:
                    items.Add(snare);
                    items.Add(bracers);
                    items.Add(shield);
                    items.Add(BT1.Item(BT1ItemIndex.SpiritHelm));
                    break;
                case GenericClass.Monk:
                    items.Add(BT1.Item(BT1ItemIndex.DeathDagger));
                    items.Add(bracers);
                    items.Add(arc);
                    items.Add(BT1.Item(BT1ItemIndex.LeatherGloves));
                    items.Add(carpet);
                    break;
                case GenericClass.Rogue:
                    items.Add(BT1.Item(BT1ItemIndex.TrollStaff));
                    items.Add(bracers);
                    items.Add(arc);
                    items.Add(BT1.Item(BT1ItemIndex.MithrilGloves));
                    items.Add(carpet);
                    break;
                case GenericClass.Conjurer:
                case GenericClass.Magician:
                case GenericClass.Sorcerer:
                    items.Add(staff);
                    items.Add(bracers);
                    items.Add(dragon);
                    items.Add(carpet);
                    break;
                case GenericClass.Wizard:
                    items.Add(snare);
                    items.Add(bracers);
                    items.Add(BT1.Item(BT1ItemIndex.LoreHelm));
                    items.Add(carpet);
                    items.Add(BT1.Item(BT1ItemIndex.WizWand));
                    break;
                default:
                    break;
            }

            if (items.Count < 8 && !items.Any(i => i != null && i.ItemBasicType == Item.ItemNounType.Ring))
                items.Add(BT1.Item(BT1ItemIndex.TrollRing));

            return items;
        }

        public override int GetLightDistance(Point ptLocation)
        {
            if (GetCurrentMapIndex() == 0)
                return 3;
            return ReadByte(BT1.Memory.LightDistance);
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int iSize = CharacterSize;
            int iMemorySize = CharacterMemorySize;

            int offset = iAddress * iSize;
            CharacterOffsets offsets = BT1.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + iSize > info.Bytes.Length + 1)
                return false;

            GenericClass btClass = BT1Character.GetBasicClass((BT12Class)info.Bytes[offset + offsets.Class]);

            byte[] bytes = new byte[] { 99, 0, 99, 0, 99, 0, 99, 0, 99, 0, 99, 0, 99, 0, 99, 0, 99, 0, 99, 0 }; // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)BTCondition.Good;
            Global.SetInt16(info.Bytes, offset + offsets.Level, 99);
            Global.SetInt16(info.Bytes, offset + offsets.LevelMod, 99);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentSP, 999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxSP, 999);
            Global.SetInt32(info.Bytes, offset + offsets.Gold, 300000000);  // Low enough so that "pool gold" doesn't overflow the value
            Global.SetInt32(info.Bytes, offset + offsets.Experience, BTCharacter.XPForLevel(Game, btClass, 99));
            bytes = new byte[] { 7, 7, 7, 7 };  // Spell levels
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.SpellLevel, bytes.Length);

            byte[] bytesNew = new byte[iMemorySize * 6];
            for(int i = 0; i < 6; i++)
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

            BT1ShopInventory inv = new BT1ShopInventory(bytesShops.Bytes);

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

            WriteByte(BT1.Memory.ShopInventory + item.Offset, (byte)item.Item.Index);
            return true; 
        }

        public override IEnumerable<Monster> GetMonsterList() { return BT1.Monsters; }

        public bool GetGameTime(out int seconds, out int minutes, out int hours)
        {
            hours = ReadByte(Memory.GameTimeHours);
            seconds = ReadUInt16(Memory.GameTimeSeconds) % 2048;
            // There are 2048 seconds in a Bard's Tale hour
            minutes = seconds * 100 / 3413;
            seconds = (seconds % 34) * 17647 / 10000;
            return true;
        }

        public override String GetGameTimeString(bool bFull)
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
            return new BT1QuestData(party, GetLocation(), GetGameState() as BTGameState, mapSpecials, townMap);
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return new BT1RosterFile(Global.CombineRoster(Game), bSilent);
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

            List<Item> backpack = new List<Item>(8);
            for (int i = 0; i < 8; i++)
            {
                int iItem = BitConverter.ToInt16(bytesChar, i * 2 + BT1.Offsets.Inventory);
                if (iItem != 0)
                    backpack.Add(Global.BT1.GetClonedItem(iItem));
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

            // Bard's Tale 1 doesn't have a fixed-size roster, so we need to search the entire roster
            // before using a new file
            byte[] bytes = new byte[CharacterSize];
            if (iStart >= m_roster.Chars.Count)
                iStart = m_roster.Chars.Count - 1;

            while (iStart >= 0)
            {
                BTCharacter bt1Char = null;
                if (iStart < m_roster.Chars.Count)
                    bt1Char = BTCharacter.Create(Game, 0, m_roster.Chars[iStart].Bytes, 0);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                    case InventoryCharAction.FindOrCreate:
                        if (bt1Char != null && bt1Char.Name == "INVENTORY")
                            return iStart;
                        break;
                    case InventoryCharAction.FindPotential:
                        if (bt1Char == null || bt1Char.Name == "INVENTORY")
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
                ((BTRosterFile) m_roster).DeleteInventoryChars();
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

            Buffer.BlockCopy(bytes.Items, 0, bytesChar, BT1.Offsets.Inventory, 16);
            m_roster.SaveCharBytes(iRosterPosition, 255, bytesChar);

            return SetBackpackResult.Success;
        }

        public static byte[] GetInventoryChar() { return Properties.Resources.BT1InventoryChar; }
        public override byte[] GetInventoryCharBytes() { return GetInventoryChar(); }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            BT1MapData bt1Data = new BT1MapData(mb);
            bt1Data.LiveOnly = true;
            return bt1Data;
        }

        public override bool VisitFacingSquare(BasicLocation location, MapSheet sheet)
        {
            return sheet != null && location != null && location.MapIndex == 0 && sheet.GameMapIndex == 0;
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            BTMapData data = new BTMapData();

            MemoryBytes mbMap = ReadOffset(Memory.Map, 484);
            if (mbMap == null)
                return null;

            MemoryBytes mbSpecials = ReadOffset(Memory.MapSpecials, 484);
            if (mbSpecials == null)
                return null;

            data.Squares = mbMap.Bytes;
            data.Specials = mbSpecials.Bytes;
            data.Bounds = new Rectangle(0, 0, 22, 22);
            data.SwapWallsDoors = ReadByte(Memory.SwapWallsDoors);

            return data;
        }

        public override MapBytes GetCurrentMapBytes()
        {
            MemoryBytes bytes = ReadOffset(Memory.Map, 484 + 1);
            if (bytes == null)
                return null;

            bytes.Bytes[484] = ReadByte(Memory.SwapWallsDoors);

            return new MapBytes(bytes, 22, 22);
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            int iMap = GetCurrentMapIndex();
            if (iMap == (int)BT1Map.SkaraBrae)
                return new Point(ReadByte(Memory.LocationEastTown), 29 - ReadByte(Memory.LocationNorthTown));

            return new Point(ReadByte(Memory.LocationEast), ReadByte(Memory.LocationNorth));
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            if (GetCurrentMapIndex() == (int)BT1Map.SkaraBrae)
            {
                WriteByte(Memory.LocationEastTown, (byte)ptLocation.X);
                return WriteByte(Memory.LocationNorthTown, (byte)(29 - ptLocation.Y));
            }
            else
            {
                WriteByte(Memory.LocationEast, (byte)ptLocation.X);
                return WriteByte(Memory.LocationNorth, (byte)ptLocation.Y);
            }
        }

        public override Size GetMapDimensions(int iIndex)
        {
            return IsDungeon(iIndex) ? new Size(22, 22) : new Size(30, 30);
        }

        public override MapBasicInfo GetCurrentMapInfo()
        {
            int iIndex = GetCurrentMapIndex();
            return new MapBasicInfo(iIndex, GetMapDimensions(iIndex));
        }

        public override int GetCurrentMapIndex()
        {
            byte bMain = ReadByte(Memory.MainMapIndex);
            byte bSub = ReadByte(Memory.SubMapIndex);
            if (bMain == 0)
                bSub = 0;
            return (bMain << 8) | bSub;
        }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((BT1Map) index)
            {
                case BT1Map.SkaraBrae: return new MapTitleInfo(index, "Skara Brae");
                case BT1Map.Cellars: return new MapTitleInfo(index, "Cellars");
                case BT1Map.Sewers1: return new MapTitleInfo(index, "Sewers, Level 1");
                case BT1Map.Sewers2: return new MapTitleInfo(index, "Sewers, Level 2");
                case BT1Map.Sewers3: return new MapTitleInfo(index, "Sewers, Level 3");
                case BT1Map.Catacombs1: return new MapTitleInfo(index, "Catacombs, Level 1");
                case BT1Map.Catacombs2: return new MapTitleInfo(index, "Catacombs, Level 2");
                case BT1Map.Catacombs3: return new MapTitleInfo(index, "Catacombs, Level 3");
                case BT1Map.Harkyn1: return new MapTitleInfo(index, "Harkyn's Castle, Level 1");
                case BT1Map.Harkyn2: return new MapTitleInfo(index, "Harkyn's Castle, Level 2");
                case BT1Map.Harkyn3: return new MapTitleInfo(index, "Harkyn's Castle, Level 3");
                case BT1Map.Kylearan: return new MapTitleInfo(index, "Kylearan's Tower");
                case BT1Map.Mangar1: return new MapTitleInfo(index, "Mangar's Tower, Level 1");
                case BT1Map.Mangar2: return new MapTitleInfo(index, "Mangar's Tower, Level 2");
                case BT1Map.Mangar3: return new MapTitleInfo(index, "Mangar's Tower, Level 3");
                case BT1Map.Mangar4: return new MapTitleInfo(index, "Mangar's Tower, Level 4");
                case BT1Map.Mangar5: return new MapTitleInfo(index, "Mangar's Tower, Level 5");
                default: return new MapTitleInfo(index, String.Format("Unknown {0}", index));
            }
        }

        private BTPartyInfo ReadBT1PartyInfo()
        {
            byte numChars = (byte)GetNumChars();
            if (numChars > 6)
                numChars = 6;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new BTPartyInfo(new byte[0], new byte[0], numChars);

            BTGameState state = GetGameState() as BTGameState;

            MemoryBytes bytesNames = ReadOffset(Memory.PartyNames, 37 * 6);
            MemoryBytes bytesStats = ReadOffset(Memory.PartyInfo, BT1Character.SizeInMemory * 6);
            int iInspecting = 0;
            if (state.Casting)
                iInspecting = state.ActingCaster;
            else if (state.InCombat)
                iInspecting = state.ActingCombatChar;
            else
                iInspecting = state.ActingCharAddress;

            Global.FixRange(ref iInspecting, 0, 5);

            MemoryStream ms = new MemoryStream(6 * CharacterSize);
            for (int i = 0; i < 6; i++)
            {
                ms.Write(bytesNames, i * 37, 16);
                ms.WriteByte(1);    // This byte is in the roster file but not in memory
                ms.Write(bytesStats, i * BT1Character.SizeInMemory, BT1Character.SizeInMemory);
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

            return ReadBT1PartyInfo();
        }

        protected override int GetNumChars()
        {
            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, BT1Character.SizeInMemory * 6);
            if (bytes == null)
                return 0;

            int iNumChars = 0;
            for (int i = 0; i < 6; i++)
                if ((bytes.Bytes[i * BT1Character.SizeInMemory] & 0x01) == 0)
                    iNumChars++;

            return iNumChars;
        }

        public override byte[] GetMarchingOrder()
        {
            byte[] order = new byte[] { 1, 2, 3, 4, 5, 6 };

            MemoryBytes mb = ReadOffset(Memory.MarchingOrder, 6);
            if (mb == null)
                return order;

            foreach (byte b in mb.Bytes)
                if (b < 1 || b > 6)
                    return order;

            return mb.Bytes;
        }

        public override string SpellType1 { get { return "Conjurer"; } }
        public override string SpellType2 { get { return "Magician"; } }
        public override string SpellType3 { get { return "Sorcerer"; } }
        public override string SpellType4 { get { return "Wizard"; } }
        public override string SpellType5 { get { return "Bard"; } }
        public override bool IsDungeon(int iMap) { return iMap != (int) BT1Map.SkaraBrae; }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            byte[] bytesNull = Global.NullBytes(4);

            MemoryBytes mbMonsters = ReadOffset(Memory.MonsterIndices, 4);
            if (mbMonsters == null || Global.Compare(mbMonsters.Bytes, bytesNull))
                return null;

            MemoryBytes mbNumAlive = ReadOffset(Memory.MonsterNumAlive, 4);
            if (mbNumAlive == null)
                return null;

            MemoryBytes mbHP = ReadOffset(Memory.MonsterHP, 400);

            if (!Global.AllNull(mbNumAlive.Bytes))
            {
                // Sometimes if a monster joins the party, the "number alive" stays at 1, which leaves
                // the encounter window up.  This will set the number alive to 0 if the first monster in
                // each group has no HP.
                if (mbHP.Bytes[0] == 0 && mbHP.Bytes[100] == 0 && mbHP.Bytes[200] == 0 && mbHP.Bytes[300] == 0)
                    Global.SetBytes(mbNumAlive.Bytes, 0, mbNumAlive.Length, 0);
            }

            Point ptParty = GetLocation().PrimaryCoordinates;

            BTCombatEffects effects = new BT1CombatEffects(this);

            if (!bForceNew &&
                m_lastEncounterInfo != null &&
                Global.Compare(m_lastEncounterInfo.MonsterIndices, mbMonsters.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Living, mbNumAlive.Bytes) &&
                Global.Compare(m_lastEncounterInfo.MonsterHP, mbHP.Bytes) &&
                Global.Compare(m_lastEncounterInfo.Effects.Bytes, effects.Bytes))
                return m_lastEncounterInfo;

            BTEncounterInfo info = new BTEncounterInfo(mbMonsters.Bytes, mbHP.Bytes, mbNumAlive.Bytes, effects);
            if (effects.TreasureState == 2)
                info.SearchResults = new BTSearchResults((BTTrapInfo.BT12Trap)effects.Trap,
                    ReadByte(Memory.MapTreasureIndex),
                    ReadOffset(Memory.EncounterMonstersKilled, 4),
                    ReadOffset(Memory.MapGoldMax, 8),
                    ReadOffset(Memory.TreasureMinimums, 8),
                    ReadOffset(Memory.TreasureRanges, 8));
            info.Party = GetPartyInfo();

            if (!effects.GameState.InCombat)
                return null;

            int iTotal = 0;
            Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
            for (int i = 0; i < info.MonsterIndices.Length; i++)
            {
                if (info.MonsterIndices[i] < 1)
                    continue;
                for (int j = 0; j < mbNumAlive.Bytes[i]; j++)
                {
                    BTMonster monster = Games.GetBTGlobals(Game).GetClonedMonster(info.MonsterIndices[i]);
                    if (monster == null)
                        monster = Games.GetBTGlobals(Game).GetClonedMonster(0);
                    monster.CurrentHP = info.MonsterHP[i * 100 + j];
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

            BTEncounterInfo bi = info as BTEncounterInfo;

            WriteOffset(Memory.MonsterIndices, bi.MonsterIndices);
            WriteOffset(Memory.MonsterNumAlive, bi.Living);
            return WriteOffset(Memory.MonsterHP, bi.MonsterHP);
        }

        public override string[] GetCharacterNames() { return GetCharacterNames(37, 6); }

        protected override void WriteTrainingInfo(BTTrainingInfo btInfo, int iAddress)
        {
            byte[] HPSP = Global.Subset(btInfo.Party.Bytes, btInfo.Party.CharacterSize * iAddress + BT1.Offsets.MaxHP, 8);
            WriteOffset(BT1.Memory.PartyInfo + CharacterMemorySize * iAddress + BT1.Offsets.MaxHP - 17, HPSP);
        }

        protected override byte GetFacingByte(int iMapIndex) { return ReadByte(iMapIndex == (int)BT1Map.SkaraBrae ? Memory.FacingTown : Memory.Facing); }
        protected override bool CanUseBag(int iMapIndex) { return iMapIndex == (int)BT1Map.SkaraBrae; }
        protected override Point CheckInvalidCoordinates(int iMapIndex, Point pt)
        {
            // Sometimes the party shows at (0,29) while the game is starting up; that's not a valid position in Skara Brae
            if (pt == new Point(0, 29) && iMapIndex == (int)BT1Map.SkaraBrae)
                return BT1.Spots.AdventurersGuild.Location;
            return pt;
        }

        protected override MonsterName[] GetMonsterNames()
        {
            MemoryBytes monsters = ReadOffset(Memory.MonsterIndices, 4);
            if (monsters == null)
                return base.GetMonsterNames();

            MonsterName[] names = new MonsterName[4];
            for (int i = 0; i < 4; i++)
            {
                if (monsters[i] > BT1.Monsters.Count || monsters[i] == 0)
                    names[i] = new MonsterName("Unknown");
                else
                    names[i] = new MonsterName(BT1.Monsters[i - 1].ProperName);
            }
            return names;
        }
    }
}
