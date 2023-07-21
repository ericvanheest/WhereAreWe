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
    public class Wiz2Memory : WizMemory
    {
        // Search for "WIZ2.DSK is missing"
        public override byte[] MainSearch { get { return new byte[] { 0x57, 0x49, 0x5A, 0x32, 0x2E, 0x44, 0x53, 0x4B, 0x20, 0x69, 0x73, 0x20, 0x6D, 0x69, 0x73, 0x73, 0x69, 0x6E, 0x67 }; } }

        public override int MainBlockSVN { get { return -7514; } }
        public override int MainBlockOldSVN { get { return -7146; } }
        public override int MainBlockNonSVN { get { return -7514; } }

        public override int Facing { get { return 48460; } }         // Int16
        public override int LocationDown { get { return 48462; } }   // Int16
        public override int LocationNorth { get { return 48464; } }  // Int16
        public override int LocationEast { get { return 48466; } }   // Int16
        public override int NumChars { get { return 48458; } }       // Int16
        public override int Map { get { return 39250; } }
        public override int ItemList { get { return 178870; } }
        public override int PartyInfo { get { return 49176; } }
        public override int AllMaps { get { return 202934; } }
        public override int TrainingChar { get { return 43238; } }
        public override int InspectingChar2 { get { return 42028; } }
        public override int ShoppingChar { get { return 42094; } }
        public override int TimeDelay { get { return 48506; } }
        public override int FightMap { get { return 49096; } }
        public override int EncounterInfo { get { return 46396; } }
        public override int State1 { get { return 2762; } }
        public override int State2 { get { return 42030; } }
        public override int State3 { get { return 38898; } }
        public override int State4 { get { return 58802; } }
        public override int CombatOptionActiveChar { get { return 38960; } }
        public override int CombatCharInfo { get { return 46396; } }
        public override int MonsterListDisk { get { return 209078; } }
        public override int TreasureList { get { return 238774; } }
        public override int TrapType { get { return 42874; } }
        public override int TrapType2 { get { return 43096; } }
        public override int RewardIndex { get { return 43094; } }
        public override int ACBonus { get { return 48518; } }
        public override int Light { get { return 48516; } }
        public override int EncounterRewardModifier { get { return 48508; } }
        public override int Identify { get { return 38954; } }  // Latumapic

        public int DefeatedKod { get { return 51952; } }  // 5 Int16s: Sword, Helm, Shield, Gauntlets, Armor

        // Unfinished
        public override int InspectingChar { get { return 49782; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry2]; } }
    }

    public enum Wiz2Map
    {
        None = -1,
        Castle = 0,
        MazeLevel1 = 1,
        MazeLevel2 = 2,
        MazeLevel3 = 3,
        MazeLevel4 = 4,
        MazeLevel5 = 5,
        MazeLevel6 = 6,
        Last = 7,
        AltCastle = 255,

        Unknown = -1
    }

    public class Wiz2GameInfo : Wiz1234GameInfo
    {
        public override GameNames Game { get { return GameNames.Wizardry2; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new WizGameInfoItem("Light", (Int16)Light, Wiz2.Memory.Light));
            items.Add(new WizGameInfoItem("AC Bonus", (Int16)ACBonus, Wiz2.Memory.ACBonus));
            items.Add(new WizGameInfoItem("Delay", (Int16)TimeDelay, Wiz2.Memory.TimeDelay));
            items.Add(new WizGameInfoItem("Identify", (Int16)Identify, Wiz2.Memory.Identify));
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int mapOffset = Wiz2.Memory.Map;
            List<GameInfoItem> items = new List<GameInfoItem>();

            WizMapData map = new WizMapData(Game, Location.MapIndex, Bytes, 0, true);
            if (map == null)
                return items;

            items.Add(new WizGameInfoItem("Maze Level", Location.MapIndex, -1));
            if (Global.Debug)
            {
                items.Add(new WizGameInfoItem("Trap Type 1", TrapType, Wiz2.Memory.TrapType));
                items.Add(new WizGameInfoItem("Trap Type 2", TrapType, Wiz2.Memory.TrapType2));
                if (!InCombat)
                {
                    items.Add(new WizGameInfoItem("Group 1 Min", map.Enemies[0].MinEnemy, mapOffset + WizMapData.Offsets.Enemies1));
                    items.Add(new WizGameInfoItem("Group 1 Mult", map.Enemies[0].Multiplier, mapOffset + WizMapData.Offsets.Enemies1 + 2));
                    items.Add(new WizGameInfoItem("Group 1 Max", map.Enemies[0].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies1 + 4));
                    items.Add(new WizGameInfoItem("Group 1 Range", map.Enemies[0].Range, mapOffset + WizMapData.Offsets.Enemies1 + 6));
                    items.Add(new WizGameInfoItem("Group 1 Worse%", map.Enemies[0].WorseChance, mapOffset + WizMapData.Offsets.Enemies1 + 8));
                    items.Add(new WizGameInfoItem("Group 2 Min", map.Enemies[1].MinEnemy, mapOffset + WizMapData.Offsets.Enemies2));
                    items.Add(new WizGameInfoItem("Group 2 Mult", map.Enemies[1].Multiplier, mapOffset + WizMapData.Offsets.Enemies2 + 2));
                    items.Add(new WizGameInfoItem("Group 2 Max", map.Enemies[1].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies2 + 4));
                    items.Add(new WizGameInfoItem("Group 2 Range", map.Enemies[1].Range, mapOffset + WizMapData.Offsets.Enemies2 + 6));
                    items.Add(new WizGameInfoItem("Group 2 Worse%", map.Enemies[1].WorseChance, mapOffset + WizMapData.Offsets.Enemies2 + 8));
                    items.Add(new WizGameInfoItem("Group 3 Min", map.Enemies[2].MinEnemy, mapOffset + WizMapData.Offsets.Enemies3));
                    items.Add(new WizGameInfoItem("Group 3 Mult", map.Enemies[2].Multiplier, mapOffset + WizMapData.Offsets.Enemies3 + 2));
                    items.Add(new WizGameInfoItem("Group 3 Max", map.Enemies[2].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies3 + 4));
                    items.Add(new WizGameInfoItem("Group 3 Range", map.Enemies[2].Range, mapOffset + WizMapData.Offsets.Enemies3 + 6));
                    items.Add(new WizGameInfoItem("Group 3 Worse%", map.Enemies[2].WorseChance, mapOffset + WizMapData.Offsets.Enemies3 + 8));
                }
            }
            return items;
        }
    }

    public class Wiz2QuestData : QuestData
    {
        public byte[] KodBytes;

        public static Wiz2QuestData Create(byte[] bytesKod)
        {
            Wiz2QuestData data = new Wiz2QuestData();
            data.SetFromBytes(bytesKod);
            return data;
        }

        public void SetFromBytes(byte[] bytesKod)
        {
            KodBytes = bytesKod;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            Global.WriteBytes(stream, KodBytes);
        }
    }

    public class Wiz2MemoryHacker : Wiz123MemoryHacker
    {
        protected override WizMemory Memory { get { return Wiz2.Memory; } }
        public override List<WizItem> WizItems { get { return Wiz2.Items; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new Wiz2GameInformationControl(main); }
        protected override QuestInfo CreateQuestInfo() { return new Wiz2QuestInfo(); }
        public override bool InitExternalMonsterList() { return Wiz2.MonsterList.Value.InitExternalList(this); }
        protected override Wiz1234GameInfo CreateGameInfo() { return new Wiz2GameInfo(); }
        public override List<bool[,]> GetFights() { return Wiz2.Encounters.Fights; }
        public override string GetMapEnum(int index) { return String.Format("Wiz2Map.{0}", Enum.GetName(typeof(Wiz2Map), (Wiz2Map)(index))); }
        public static MapTitleInfo GetMapTitlePair(int index) { return IsCastle(index) ? new MapTitleInfo(index, "Castle") : new MapTitleInfo(index, String.Format("Maze Level {0}", index)); }
        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }

        protected override WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter, Point ptPartyPosition, int iRewardModifier, int offset = 0)
        {
            return new Wiz2EncounterInfo(state, bytesEncounter, ptPartyPosition, iRewardModifier, offset); 
        }

        public Wiz2MemoryHacker()
        {
            m_game = GameNames.Wizardry2;
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Wiz2.MonsterList.Value.Reinitialize(this, false);
            if (Wiz2.MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
            Wiz2.ItemList.Value.InitExternalList(this);
            Wiz2.TreasureList.Value.InitExternalList(this);
            base.OnReinitialized(e);
        }

        protected override MainState GetMainState(int state1, int state2, int state3, int state4, int state5)
        {
            switch (state1)
            {
                case 0x53EC: return MainState.EdgeOfTown;
                case 0x5464: return MainState.Castle;
                case 0x548C: return MainState.Tavern;
                case 0x56B8: return MainState.Inn;
                case 0x5812: return MainState.Temple;
                case 0x5704: return MainState.Shop;
                case 0x54F2: return MainState.TavernRemoveChar;
                case 0x5828: return MainState.TavernAddChar;
                case 0x588E: return MainState.TavernInspect;
                case 0x5C6A: return MainState.TavernInspectRead;
                case 0x5886: return MainState.TavernInspectTrade;
                case 0x5222: return MainState.Utilities;
                case 0x5528: return MainState.ChangeName;
                case 0x5530: return MainState.MoveSelectChars;
                case 0x51E2: return MainState.Training;
                case 0x54EC: return MainState.TrainingInspectSelectChar;
                case 0x5258: return MainState.TrainingInspectCharSelected;
                case 0x5678: return MainState.TrainingInspecting;
                case 0x5278: return MainState.TrainingInspectChangeClass;
                case 0x5A54: return MainState.TrainingInspectRead;
                case 0x516E: return MainState.Roster;
                case 0x53B8: return MainState.Camp;
                case 0x5882: return MainState.CampInspecting;
                case 0x5C5E: return MainState.CampInspectingRead;
                case 0x54C8: return MainState.CampReorder;
                case 0x55FE: return MainState.CampEquip;
                case 0x5446: return MainState.Adventuring;
                case 0x27A4: return MainState.Opening;
                case 0x540E: return MainState.PreCombat;
                case 0x587A:
                    switch (state2)
                    {
                        case 7: return MainState.UseSelectItem;
                        case 3: return MainState.DropSelectItem;
                        case 6: return MainState.SelectSpell;
                        default: return MainState.CampInspectingCastDropUse;
                    }

                /////////////////////////////////////////////////////////////////
                case 0x5482:
                    switch (state4)
                    {
                        case 0xA23E: return MainState.TreasureWhoWillOpen;
                        case 0xA23C: return MainState.TreasureWhoWillCalfo;
                        case 0xA238:
                        case 0xA24C: return MainState.TreasureWhoWillInspect;
                        case 0xA1A0:
                        case 0xA1B4: return MainState.TreasureEnterTrapType;
                        case 0xA194:
                        case 0xA1A8: return MainState.TreasureWhoWillDisarm;
                        case 0xA1C6: return MainState.TreasureCouldNotDisarm;
                        case 0x9F44:
                        case 0x9F58: return MainState.Question;
                        default: return MainState.Treasure;
                    }
                case 0x55A0:
                    switch (state3)
                    {
                        case 0x0054: return MainState.CombatOptions;
                        case 0xF7FE:
                            switch (state4)
                            {
                                case 0x9348: return MainState.CombatConfirmRound;
                                default: return MainState.CombatOptions;
                            }
                        case 0x9630: return MainState.CombatSelectFightTarget;
                        case 0x0000: return MainState.CombatSelectSpell;
                        case 0xB872: return MainState.CombatFriendly;
                        default: return MainState.Combat;
                    }
                case 0x539E: return MainState.ReceiveExp;
                default: return MainState.Unknown;
            }
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz2.Monsters; }

        protected override List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment)
        {
            bool bEvil = alignment == WizAlignment.Evil;

            List<Item> items = new List<Item>(8);
            WizItem plate = Wiz2.Items[(int)Wiz2ItemIndex.PlateMailPlus5];
            WizItem cursedplate = Wiz2.Items[(int)Wiz2ItemIndex.CursedPlatePlus1];
            WizItem leather = Wiz2.Items[(int)Wiz2ItemIndex.LeatherPlus2];
            WizItem mace = Wiz2.Items[(int)Wiz2ItemIndex.PriestsMace];
            WizItem shield = Wiz2.Items[(int)Wiz2ItemIndex.ShieldPlus3];
            WizItem gloves = Wiz2.Items[(int)Wiz2ItemIndex.WinterMittens];
            WizItem sword = Wiz2.Items[(int)Wiz2ItemIndex.LongSwordPlus5];   // The "best" weapons is a matter of taste; this is the most well-rounded
            WizItem blade = Wiz2.Items[(int)Wiz2ItemIndex.BladeCusinart];
            WizItem swings = Wiz2.Items[(int)Wiz2ItemIndex.ShortSwordOfSwings];  // Usable by mages for some odd reason
            WizItem helm = Wiz2.Items[bEvil ? (int)Wiz2ItemIndex.HelmPlus2 : (int)Wiz2ItemIndex.HelmOfMalor];

            switch (wizClass)
            {
                case WizClass.Fighter:
                case WizClass.Samurai:
                case WizClass.Lord:
                    // Although the "Kod's" equipment is definitely the best, they are quest items and so
                    // we use the second-best here.
                    //items.Add(Wiz2.Items[(int)Wiz2ItemIndex.Hrathnir]);
                    //items.Add(Wiz2.Items[(int)Wiz2ItemIndex.KodsShield]);
                    //items.Add(Wiz2.Items[(int)Wiz2ItemIndex.KodsArmor]);
                    //items.Add(Wiz2.Items[(int)Wiz2ItemIndex.KodsGauntlets]);
                    //items.Add(Wiz2.Items[(int)Wiz2ItemIndex.KodsHelm]);
                    items.Add(sword);
                    items.Add(shield);
                    items.Add(wizClass == WizClass.Lord ? Wiz2.Items[(int)Wiz2ItemIndex.LordsGarb] : plate);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                case WizClass.Mage:
                    items.Add(swings);
                    // Though the "Cursed Plate +1" is usable by mages, is is a nuisance
                    //items.Add(cursedplate);
                    items.Add(Wiz2.Items[(int)Wiz2ItemIndex.RobePlus3]);
                    break;
                case WizClass.Priest:
                    items.Add(mace);
                    items.Add(shield);
                    items.Add(plate);
                    break;
                case WizClass.Thief:
                    items.Add(swings);
                    items.Add(shield);
                    items.Add(leather);
                    break;
                case WizClass.Bishop:
                    items.Add(mace);
                    items.Add(leather);
                    break;
                case WizClass.Ninja:
                    items.Add(bEvil ? Wiz2.Items[(int)Wiz2ItemIndex.Shurikens] : blade);    // Ninjas are required to be evil, but alignments can shift
                    items.Add(shield);
                    items.Add(plate);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                default:
                    break;
            }

            if (!items.Any(i => i.CanEquipLocation == EquipLocation.Head))
                items.Add(Wiz2.Items[(int)Wiz2ItemIndex.HelmOfMalor]);
            items.Add(Wiz2.Items[(int)Wiz2ItemIndex.AmuletOfCover]);

            return items;
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return Wiz2RosterFile.CreateWiz2(Global.CombineRoster(Game), bSilent);
        }

        public override QuestData GetQuestData()
        {
            if (!IsValid)
                return null;

            return Wiz2QuestData.Create(ReadOffset(Wiz2.Memory.DefeatedKod, 10).Bytes);
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(10);
            for (Wiz2Map map = Wiz2Map.MazeLevel1; map < Wiz2Map.Last; map++)
                maps.Add(GetMapTitlePair((int)map));
            return maps;
        }

    }
}
