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
    public class Wiz1Memory : WizMemory
    {
        // Search for "WIZ1.DSK is missing"
        public override byte[] MainSearch { get { return new byte[] { 0x57, 0x49, 0x5A, 0x31, 0x2E, 0x44, 0x53, 0x4B, 0x20, 0x69, 0x73, 0x20, 0x6D, 0x69, 0x73, 0x73, 0x69, 0x6E, 0x67 }; } }

        public override int MainBlockSVN { get { return -6586; } }
        public override int MainBlockOldSVN { get { return -6218; } }
        public override int MainBlockNonSVN { get { return -6586; } }

        public override int Facing { get { return 48294; } }         // Int16
        public override int LocationDown { get { return 48296; } }   // Int16
        public override int LocationNorth { get { return 48298; } }  // Int16
        public override int LocationEast { get { return 48300; } }   // Int16
        public override int NumChars { get { return 48292; } }       // Int16
        public override int Map { get { return 39116; } }
        public override int MonsterListDisk { get { return 211462; } }
        public override int ItemList { get { return 178182; } }
        public override int PartyInfo { get { return 49010; } }
        public override int State1 { get { return 2586; } }
        public override int State2 { get { return 41864; } }
        public override int State3 { get { return 38418; } }
        public override int State4 { get { return 16268; } }
        public override int InspectingChar { get { return 48302; } }
        public override int InspectingChar2 { get { return 41862; } }
        public override int CombatOptionActiveChar { get { return 38794; } }
        public override int TrainingChar { get { return 43072; } }
        public override int ShoppingChar { get { return 42294; } }
        public override int EncounterInfo { get { return 46230; } }
        public override int TreasureList { get { return 239110; } }
        public override int FightMap { get { return 48930; } }
        public override int EncounterRewardModifier { get { return 48342; } }
        public override int AllMaps { get { return 201222; } }
        public override int CreateName { get { return 42044; } }
        public override int CreateBonus { get { return 42484; } }
        public override int CreateAttributes { get { return 42968; } }  // 6 Int16s
        public override int CreationSelectedStat { get { return 42540; } }
        public override int CreationSelectedRace { get { return 42682; } }
        public override int CreateGold { get { return 42700; } }
        public override int TimeDelay { get { return 48340; } }
        public override int Light { get { return 48350; } }
        public override int ACBonus { get { return 48352; } }
        public override int Identify { get { return 48348; } }
        public override int TrapType { get { return 42412; } }
        public override int TrapType2 { get { return 42634; } }
        public override int RewardIndex { get { return 42632; } }

        public override int CombatCharInfo { get { return 46230; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry1]; } }
    }

    public class Wiz1MemoryOld : WizMemory
    {
        // Search for "original Wizardry"
        public override byte[] MainSearch { get { return new byte[] { 0x6F, 0x72, 0x69, 0x67, 0x69, 0x6E, 0x61, 0x6C, 0x20, 0x57, 0x69, 0x7A, 0x61, 0x72, 0x64, 0x72, 0x79 }; } }

        public override int MainBlockSVN { get { return -8066; } }
        public override int MainBlockOldSVN { get { return -7698; } }
        public override int MainBlockNonSVN { get { return -8066; } }

        public override int Facing { get { return 49774; } }         // Int16
        public override int LocationDown { get { return 49776; } }   // Int16
        public override int LocationNorth { get { return 49778; } }  // Int16
        public override int LocationEast { get { return 49780; } }   // Int16
        public override int NumChars { get { return 49772; } }       // Int16
        public override int Map { get { return 40596; } }
        public override int MonsterListDisk { get { return 212942; } }
        public override int ItemList { get { return 179662; } }
        public override int PartyInfo { get { return 50490; } }
        public override int State1 { get { return 4066; } }
        public override int State2 { get { return 43344; } }
        public override int State3 { get { return 39898; } }
        public override int State4 { get { return 17748; } }
        public override int InspectingChar { get { return 49782; } }
        public override int InspectingChar2 { get { return 43342; } }
        public override int CombatOptionActiveChar { get { return 40274; } }
        public override int TrainingChar { get { return 44552; } }
        public override int ShoppingChar { get { return 43774; } }
        public override int EncounterInfo { get { return 47718; } }
        public override int TreasureList { get { return 223182; } }
        public override int FightMap { get { return 50410; } }
        public override int EncounterRewardModifier { get { return 49822; } }
        public override int AllMaps { get { return 202702; } }
        public override int CreateName { get { return 43524; } }
        public override int CreateBonus { get { return 43964; } }
        public override int CreateAttributes { get { return 44448; } }  // 6 Int16s
        public override int CreationSelectedStat { get { return 44020; } }
        public override int CreationSelectedRace { get { return 44162; } }
        public override int CreateGold { get { return 44180; } }
        public override int TimeDelay { get { return 49820; } }
        public override int Light { get { return 49830; } }
        public override int ACBonus { get { return 49832; } }
        public override int Identify { get { return 49828; } }
        public override int TrapType { get { return 43892; } }
        public override int TrapType2 { get { return 44114; } }
        public override int RewardIndex { get { return 44112; } }

        public override int CombatCharInfo { get { return 47718; } }

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry1]; } }
    }

    public enum Wiz1Map
    {
        None = -1,
        Castle = 0,
        MazeLevel1 = 1,
        MazeLevel2 = 2,
        MazeLevel3 = 3,
        MazeLevel4 = 4,
        MazeLevel5 = 5,
        MazeLevel6 = 6,
        MazeLevel7 = 7,
        MazeLevel8 = 8,
        MazeLevel9 = 9,
        MazeLevel10 = 10,
        Last = 11,
        AltCastle = 255,

        Unknown = -1
    }

    public class Wiz1234GameInfo : GameInfo
    {
        public bool InCombat = false;
        public int Light;
        public int ACBonus;
        public int TimeDelay;
        public int Identify;
        public int TrapType;
        public int TrapType2;
    }

    public class Wiz1GameInfo : Wiz1234GameInfo
    {
        public override GameNames Game { get { return GameNames.Wizardry1; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new WizGameInfoItem("Light", (Int16) Light, Wiz1.Memory.Light));
            items.Add(new WizGameInfoItem("AC Bonus", (Int16)ACBonus, Wiz1.Memory.ACBonus));
            items.Add(new WizGameInfoItem("Delay", (Int16)TimeDelay, Wiz1.Memory.TimeDelay));
            items.Add(new WizGameInfoItem("Identify", (Int16)Identify, Wiz1.Memory.Identify));
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int mapOffset = Wiz1.Memory.Map;
            List<GameInfoItem> items = new List<GameInfoItem>();

            WizMapData map = new WizMapData(Game, Location.MapIndex, Bytes, 0, true);
            if (map == null)
                return items;

            items.Add(new WizGameInfoItem("Maze Level", Location.MapIndex, -1));
            if (Global.Debug)
            {
                items.Add(new WizGameInfoItem("Trap Type 1", TrapType, Wiz1.Memory.TrapType));
                items.Add(new WizGameInfoItem("Trap Type 2", TrapType, Wiz1.Memory.TrapType2));
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

    public class Wiz1MemoryHacker : Wiz123MemoryHacker
    {
        protected override WizMemory Memory { get { return Wiz1.Memory; } }
        public override List<WizItem> WizItems { get { return Wiz1.Items; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new Wiz1GameInformationControl(main); }
        protected override QuestInfo CreateQuestInfo() { return new Wiz1QuestInfo(); }
        public override bool InitExternalMonsterList() { return Wiz1.MonsterList.Value.InitExternalList(this); }
        protected override Wiz1234GameInfo CreateGameInfo() { return new Wiz1GameInfo(); }
        public override List<bool[,]> GetFights() { return Wiz1.Encounters.Fights; }
        public override string GetMapEnum(int index) { return String.Format("Wiz1Map.{0}", Enum.GetName(typeof(Wiz1Map), (Wiz1Map)(index))); }
        public static MapTitleInfo GetMapTitlePair(int index) { return IsCastle(index) ? new MapTitleInfo(index, "Castle") : new MapTitleInfo(index, String.Format("Maze Level {0}", index)); }
        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return Wiz1RosterFile.CreateWiz1(Global.CombineRoster(Game), bSilent);
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Wiz1.MonsterList.Value.Reinitialize(this, false);
            if (Wiz1.MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
            Wiz1.ItemList.Value.InitExternalList(this);
            Wiz1.TreasureList.Value.InitExternalList(this);
            base.OnReinitialized(e);
        }

        protected override WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter,
            Point ptPartyPosition, int iRewardModifier, int offset = 0)
        {
            return new Wiz1EncounterInfo(state, bytesEncounter, ptPartyPosition, iRewardModifier, offset); 
        }

        public Wiz1MemoryHacker()
        {
            m_game = GameNames.Wizardry1;
        }

        protected override MainState GetMainState(int state1, int state2, int state3, int state4, int state5)
        {
            switch (state1)
            {
                case 0x540E: return MainState.EdgeOfTown;
                case 0x5486: return MainState.Castle;
                case 0x54AE: return MainState.Tavern;
                case 0x56DA: return MainState.Inn;
                case 0x5834: return MainState.Temple;
                case 0x5726: return MainState.Shop;
                case 0x5514: return MainState.TavernRemoveChar;
                case 0x584A: return MainState.TavernAddChar;
                case 0x58B0: return MainState.TavernInspect;
                case 0x5C8C: return MainState.TavernInspectRead;
                case 0x58A8: return MainState.TavernInspectTrade;
                case 0x524C:
                case 0x527A: return MainState.Utilities;
                case 0x5552: return MainState.ChangeName;
                case 0x5254: return MainState.MoveInsertDisc;
                case 0x555A: return MainState.MoveSelectChars;
                case 0x5238: return MainState.Training;
                case 0x5516: return MainState.TrainingInspectSelectChar;
                case 0x5282: return MainState.TrainingInspectCharSelected;
                case 0x56A2: return MainState.TrainingInspecting;
                case 0x52A2: return MainState.TrainingInspectChangeClass;
                case 0x5A7E: return MainState.TrainingInspectRead;
                case 0x525A:
                    switch (state2)
                    {
                        case 0xD1E5: return MainState.CreateSelectPassword;
                        default: return MainState.CreateSelectName;
                    }
                case 0x5286: return MainState.CreateSelectRace;
                case 0x5256: return MainState.CreateSelectAlignment;
                case 0x5198: return MainState.Roster;
                case 0x53E2: return MainState.Camp;
                case 0x58AC:
                case 0x5806: return MainState.CampInspecting;
                case 0x5C88: return MainState.CampInspectingRead;
                case 0x54AC:
                    switch (state4)
                    {
                        case 0x4502: return MainState.TreasureWhoWillOpen;
                        case 0x1EC2: return MainState.TreasureWhoWillCalfo;
                        case 0x3446: return MainState.TreasureWhoWillInspect;
                        case 0x2F7E:
                            switch (state2)
                            {
                                case 0xDCB2: return MainState.TreasureCouldNotDisarm;
                                case 0xB8E6: return MainState.TreasureEnterTrapType;
                                default: return MainState.TreasureWhoWillDisarm;
                            }
                        default: return MainState.Treasure;
                    }
                case 0x58A4:
                    switch (state2)
                    {
                        case 7: return MainState.UseSelectItem;
                        case 3: return MainState.DropSelectItem;
                        case 6: return MainState.SelectSpell;
                        default: return MainState.CampInspectingCastDropUse;
                    }
                case 0x54F2: return MainState.CampReorder;
                case 0x53DE: return MainState.CampEquip;
                case 0x5550:
                case 0x5470: return MainState.Adventuring;
                case 0x55CA:
                case 0x5718:
                    switch (state3)
                    {
                        case 0xF7FE: return MainState.CombatOptions;
                        case 0x00C1:
                        case 0x0D11: return MainState.CombatSelectFightTarget;
                        case 0xB87C: return MainState.CombatSelectSpell;
                        case 0x9218: return MainState.CombatConfirmRound;
                        case 0x3904: return MainState.CombatFriendly;
                        default: return MainState.Combat;
                    }
                case 0x5438: return MainState.PreCombat;
                case 0x53C8:
                    if (state3 == 0x9218)
                        return MainState.ReceiveExp;
                    switch (state2)
                    {
                        case 0x9F86: return MainState.CreateKeepChar;
                        default: return MainState.CreateExchangeStat;
                    }
                case 0x27CE: return MainState.Opening;
                case 0x514C: return MainState.InsertDisk;
                default: return MainState.Unknown;
            }
        }

        protected override List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment)
        {
            bool bEvil = alignment == WizAlignment.Evil;

            List<Item> items = new List<Item>(8);
            WizItem plate = Wiz1.Items[bEvil ? (int)Wiz1ItemIndex.EvilPlatePlus3 : (int)Wiz1ItemIndex.BreastPlatePlus3];
            WizItem leather = Wiz1.Items[(int)Wiz1ItemIndex.LeatherPlus2];
            WizItem mace = Wiz1.Items[(int)Wiz1ItemIndex.MacePlus2];
            WizItem shield = Wiz1.Items[(int)Wiz1ItemIndex.ShieldPlus3];
            WizItem gloves = Wiz1.Items[(int)Wiz1ItemIndex.GlovesOfSilver];
            WizItem blade = Wiz1.Items[(int)Wiz1ItemIndex.BladeCusinart];
            WizItem helm = Wiz1.Items[bEvil ? (int)Wiz1ItemIndex.HelmPlus2 : (int)Wiz1ItemIndex.HelmOfMalor];
            WizItem eshort = Wiz1.Items[(int)Wiz1ItemIndex.EvilShortSwordPlus3];

            switch (wizClass)
            {
                case WizClass.Fighter:
                    items.Add(blade);
                    items.Add(shield);
                    items.Add(plate);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                case WizClass.Mage:
                    items.Add(Wiz1.Items[(int)Wiz1ItemIndex.Robes]);
                    items.Add(Wiz1.Items[(int)Wiz1ItemIndex.DaggerPlus2]);
                    break;
                case WizClass.Priest:
                    items.Add(mace);
                    items.Add(shield);
                    items.Add(plate);
                    break;
                case WizClass.Thief:
                    items.Add(eshort);
                    items.Add(shield);
                    items.Add(leather);
                    break;
                case WizClass.Bishop:
                    items.Add(mace);
                    items.Add(leather);
                    break;
                case WizClass.Samurai:
                    items.Add(Wiz1.Items[(int)Wiz1ItemIndex.MurasamaBlade]);
                    items.Add(shield);
                    items.Add(plate);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                case WizClass.Lord:
                    items.Add(blade);
                    items.Add(shield);
                    items.Add(Wiz1.Items[(int)Wiz1ItemIndex.LordsGarb]);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                case WizClass.Ninja:
                    items.Add(bEvil ? Wiz1.Items[(int)Wiz1ItemIndex.Shurikens] : blade);
                    items.Add(shield);
                    items.Add(plate);
                    items.Add(gloves);
                    items.Add(helm);
                    break;
                default:
                    break;
            }

            if (!items.Any(i => i.CanEquipLocation == EquipLocation.Head))
                items.Add(Wiz1.Items[(int)Wiz1ItemIndex.HelmOfMalor]);
            items.Add(Wiz1.Items[(int)Wiz1ItemIndex.RingOfHealing]);

            return items;
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(10);
            for (Wiz1Map map = Wiz1Map.MazeLevel1; map < Wiz1Map.Last; map++)
                maps.Add(GetMapTitlePair((int)map));
            return maps;
        }
    }
}
