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
    public class Wiz3Memory : WizMemory
    {
        // Search for "WIZ3.DSK is missing"
        public override byte[] MainSearch { get { return new byte[] { 0x57, 0x49, 0x5A, 0x33, 0x2E, 0x44, 0x53, 0x4B, 0x20, 0x69, 0x73, 0x20, 0x6D, 0x69, 0x73, 0x73, 0x69, 0x6E, 0x67 }; } }

        public override int MainBlockSVN { get { return -7514; } }
        public override int MainBlockOldSVN { get { return -7146; } }
        public override int MainBlockNonSVN { get { return -7514; } }

        public override int Map { get { return 39274; } }
        public override int Facing { get { return 48626; } }         // Int16
        public override int LocationDown { get { return 48628; } }   // Int16
        public override int LocationNorth { get { return 48630; } }  // Int16
        public override int LocationEast { get { return 48632; } }   // Int16
        public override int PartyInfo { get { return 49362; } }
        public override int NumChars { get { return 48624; } }       // Int16
        public override int AllMaps { get { return 204134; } }
        public override int MonsterListDisk { get { return 210278; } }
        public override int ItemList { get { return 180070; } }
        public override int TreasureList { get { return 239974; } }
        public override int FightMap { get { return 49282; } }
        public override int CombatCharInfo { get { return 46562; } }
        public override int EncounterInfo { get { return 46562; } }
        public override int State1 { get { return 2938; } }
        public override int State2 { get { return 42136; } }
        public override int State3 { get { return 38868; } }
        public override int State4 { get { return 38412; } }
        public override int State5 { get { return 64866; } }
        public override int TimeDelay { get { return 48672; } }
        public override int InspectingChar2 { get { return 42134; } }
        public override int TrainingChar { get { return 43306; } }
        public override int ShoppingChar { get { return 42200; } }
        public override int Light { get { return 48682; } }
        public override int ACBonus { get { return 48684; } }
        public override int EncounterRewardModifier { get { return 48674; } }
        public override int TrapType { get { return 43058; } }
        public override int TrapType2 { get { return 43280; } }
        public override int RewardIndex { get { return 43278; } }
        public override int CombatOptionActiveChar { get { return 39000; } }
        public override int Identify { get { return 48680; } }  // Latumapic

        public int LocationDownAlt { get { return 18984; } }   // Int16
        public int InCastle { get { return 48642; } }   // 0:InMaze

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry3]; } }
    }

    public enum Wiz3Map
    {
        None = -1,
        Castle = 0,
        TowerLevel1 = 1,
        TowerLevel2 = 2,
        TowerLevel3 = 3,
        TowerLevel4 = 4,
        TowerLevel5 = 5,
        TowerLevel6 = 6,
        Last = 7,
        AltCastle = 255,

        Unknown = -1
    }

    public class Wiz3GameInfo : Wiz1234GameInfo
    {
        public override GameNames Game { get { return GameNames.Wizardry3; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            return items;
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new Wiz3GameInfoItem("Light", (Int16)Light, Wiz3.Memory.Light));
            items.Add(new Wiz3GameInfoItem("AC Bonus", (Int16)ACBonus, Wiz3.Memory.ACBonus));
            items.Add(new Wiz3GameInfoItem("Delay", (Int16)TimeDelay, Wiz3.Memory.TimeDelay));
            items.Add(new Wiz3GameInfoItem("Identify", (Int16)Identify, Wiz3.Memory.Identify));
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int mapOffset = Wiz3.Memory.Map;
            List<GameInfoItem> items = new List<GameInfoItem>();

            WizMapData map = new WizMapData(Game, Location.MapIndex, Bytes, 0, true);
            if (map == null)
                return items;

            items.Add(new Wiz3GameInfoItem("Tower Level", Location.MapIndex, -1));
            if (Global.Debug)
            {
                items.Add(new Wiz3GameInfoItem("Trap Type 1", TrapType, Wiz3.Memory.TrapType));
                items.Add(new Wiz3GameInfoItem("Trap Type 2", TrapType, Wiz3.Memory.TrapType2));
                if (!InCombat)
                {
                    items.Add(new Wiz3GameInfoItem("Group 1 Min", map.Enemies[0].MinEnemy, mapOffset + WizMapData.Offsets.Enemies1));
                    items.Add(new Wiz3GameInfoItem("Group 1 Mult", map.Enemies[0].Multiplier, mapOffset + WizMapData.Offsets.Enemies1 + 2));
                    items.Add(new Wiz3GameInfoItem("Group 1 Max", map.Enemies[0].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies1 + 4));
                    items.Add(new Wiz3GameInfoItem("Group 1 Range", map.Enemies[0].Range, mapOffset + WizMapData.Offsets.Enemies1 + 6));
                    items.Add(new Wiz3GameInfoItem("Group 1 Worse%", map.Enemies[0].WorseChance, mapOffset + WizMapData.Offsets.Enemies1 + 8));
                    items.Add(new Wiz3GameInfoItem("Group 2 Min", map.Enemies[1].MinEnemy, mapOffset + WizMapData.Offsets.Enemies2));
                    items.Add(new Wiz3GameInfoItem("Group 2 Mult", map.Enemies[1].Multiplier, mapOffset + WizMapData.Offsets.Enemies2 + 2));
                    items.Add(new Wiz3GameInfoItem("Group 2 Max", map.Enemies[1].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies2 + 4));
                    items.Add(new Wiz3GameInfoItem("Group 2 Range", map.Enemies[1].Range, mapOffset + WizMapData.Offsets.Enemies2 + 6));
                    items.Add(new Wiz3GameInfoItem("Group 2 Worse%", map.Enemies[1].WorseChance, mapOffset + WizMapData.Offsets.Enemies2 + 8));
                    items.Add(new Wiz3GameInfoItem("Group 3 Min", map.Enemies[2].MinEnemy, mapOffset + WizMapData.Offsets.Enemies3));
                    items.Add(new Wiz3GameInfoItem("Group 3 Mult", map.Enemies[2].Multiplier, mapOffset + WizMapData.Offsets.Enemies3 + 2));
                    items.Add(new Wiz3GameInfoItem("Group 3 Max", map.Enemies[2].MaxEnemy, mapOffset + WizMapData.Offsets.Enemies3 + 4));
                    items.Add(new Wiz3GameInfoItem("Group 3 Range", map.Enemies[2].Range, mapOffset + WizMapData.Offsets.Enemies3 + 6));
                    items.Add(new Wiz3GameInfoItem("Group 3 Worse%", map.Enemies[2].WorseChance, mapOffset + WizMapData.Offsets.Enemies3 + 8));
                }
            }
            return items;
        }
    }

    public class Wiz3QuestData : QuestData
    {
        public byte[] CurrentEncounterMonsters;

        public static Wiz3QuestData Create(byte[] currentEncounterMonsters)
        {
            Wiz3QuestData data = new Wiz3QuestData();
            data.SetFromBytes(currentEncounterMonsters);
            return data;
        }

        public void SetFromBytes(byte[] currentEncounterMonsters)
        {
            CurrentEncounterMonsters = currentEncounterMonsters;
        }

        public bool InCombat { get { return CurrentEncounterMonsters[0] != 0; } }
        public int[] Monsters
        {
            get
            {
                int[] monsters = new int[4];
                monsters[0] = BitConverter.ToInt16(CurrentEncounterMonsters, 1);
                monsters[1] = BitConverter.ToInt16(CurrentEncounterMonsters, 3);
                monsters[2] = BitConverter.ToInt16(CurrentEncounterMonsters, 5);
                monsters[3] = BitConverter.ToInt16(CurrentEncounterMonsters, 7);
                return monsters;
            }
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            Global.WriteBytes(stream, CurrentEncounterMonsters);
        }
    }

    public class Wiz3GameInfoItem : WizGameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return Wiz3MemoryHacker.GetMapTitlePair(iMap); }

        public Wiz3GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public Wiz3GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public Wiz3GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class Wiz3MemoryHacker : Wiz123MemoryHacker
    {
        protected override WizMemory Memory { get { return Wiz3.Memory; } }
        public override List<WizItem> WizItems { get { return Wiz3.Items; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new Wiz3GameInformationControl(main); }
        protected override QuestInfoBase CreateQuestInfo() { return new Wiz3QuestInfo(); }
        public override bool InitExternalMonsterList() { return Wiz3.MonsterList.Value.InitExternalList(this); }
        protected override Wiz1234GameInfo CreateGameInfo() { return new Wiz3GameInfo(); }
        public override List<bool[,]> GetFights() { return Wiz3.Encounters.Fights; }
        public override string GetMapEnum(int index) { return String.Format("Wiz3Map.{0}", Enum.GetName(typeof(Wiz3Map), (Wiz3Map)(index))); }
        public static MapTitleInfo GetMapTitlePair(int index) { return IsCastle(index) ? new MapTitleInfo(index, "Castle") : new MapTitleInfo(index, String.Format("Tower Level {0}", index)); }
        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return Wiz3RosterFile.CreateWiz3(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "SAVE3.DSK"; } }

        protected override WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter, Point ptPartyPosition, int iRewardModifier, int offset = 0)
        {
            return new Wiz3EncounterInfo(state, bytesEncounter, ptPartyPosition, iRewardModifier, offset); 
        }

        public Wiz3MemoryHacker()
        {
            m_game = GameNames.Wizardry3;
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Wiz3.MonsterList.Value.Reinitialize(this, false);
            if (Wiz3.MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
            Wiz3.ItemList.Value.InitExternalList(this);
            Wiz3.TreasureList.Value.InitExternalList(this);
            base.OnReinitialized(e);
        }

        protected override MainState GetMainState(int state1, int state2, int state3, int state4, int state5)
        {
            switch (state1)
            {
                case 0x548E: return MainState.EdgeOfTown;
                case 0x5506: return MainState.Castle;
                case 0x552E: return MainState.Tavern;
                case 0x575A: return MainState.Inn;
                case 0x58B4: return MainState.Temple;
                case 0x57A6: return MainState.Shop;
                case 0x5594: return MainState.TavernRemoveChar;
                case 0x58CA: return MainState.TavernAddChar;
                case 0x5930: return MainState.TavernInspect;
                case 0x5D0C: return MainState.TavernInspectRead;
                case 0x5928: return MainState.TavernInspectTrade;
                case 0x52C4: return MainState.Utilities;
                case 0x55CA: return MainState.ChangeName;
                case 0x55D2: return MainState.MoveSelectChars;
                case 0x5284: return MainState.Training;
                case 0x558E: return MainState.TrainingInspectSelectChar;
                case 0x531A: return MainState.TrainingInspectCharSelected;
                case 0x571A: return MainState.TrainingInspecting;
                //case 0x531A: return MainState.TrainingInspectChangeClass;
                case 0x5AF6: return MainState.TrainingInspectRead;
                case 0x5210: return MainState.Roster;
                case 0x545A: return MainState.Camp;
                case 0x5924: return MainState.CampInspecting;
                case 0x5D00: return MainState.CampInspectingRead;
                case 0x556A: return MainState.CampReorder;
                case 0x5456: return MainState.CampEquip;
                case 0x54E8:
                case 0x548A: return MainState.Adventuring;
                case 0x2846: return MainState.Opening;
                case 0x54B0: return MainState.PreCombat;
                case 0x591C:
                    switch (state2)
                    {
                        case 7: return MainState.UseSelectItem;
                        case 3: return MainState.DropSelectItem;
                        case 6: return MainState.SelectSpell;
                        default: return MainState.CampInspectingCastDropUse;
                    }

                /////////////////////////////////////////////////////////////////
                case 0x5524:
                    switch (state5)
                    {
                        case 0xA256: return MainState.TreasureWhoWillOpen;
                        case 0xA254: return MainState.TreasureWhoWillCalfo;
                        case 0xA250: return MainState.TreasureWhoWillInspect;
                        case 0xA1AC: return MainState.TreasureWhoWillDisarm;
                        case 0xA1B8: return MainState.TreasureEnterTrapType;
                        case 0xA1DA: return MainState.TreasureCouldNotDisarm;
                        case 0xA018: return MainState.Treasure;
                        default: 
                            if ((state5 & 0xF000) == 0xA000)
                                return MainState.Treasure;
                            return MainState.Question;
                    }
                case 0x5642:
                    switch (state3)
                    {
                        case 0x959B:
                        case 0x955E: return MainState.CombatConfirmRound;
                        case 0xF7FE:
                            switch (state4)
                            {
                                case 0x90AA: return MainState.CombatConfirmRound;
                                default: return MainState.CombatOptions;
                            }
                        case 0x9630: return MainState.CombatSelectFightTarget;
                        case 0x0000: return MainState.CombatSelectSpell;
                        case 0xF7F6: return MainState.CombatFriendly;
                        default: return MainState.Combat;
                    }
                case 0x5440: return MainState.ReceiveExp;
                default: return MainState.Unknown;
            }
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz3.Monsters; }

        protected override List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment)
        {
            bool bEvil = alignment == WizAlignment.Evil;
            bool bGood = alignment == WizAlignment.Good;

            List<Item> items = new List<Item>(8);
            WizItem plate = Wiz3.Items[(int)Wiz3ItemIndex.PlateArmorPlus2 % 1000];
            WizItem breast = Wiz3.Items[(int)Wiz3ItemIndex.BreastplatePlus2 % 1000];
            WizItem mace = Wiz3.Items[(int)Wiz3ItemIndex.MacePlus2 % 1000];
            WizItem shield = Wiz3.Items[(int)Wiz3ItemIndex.HeaterPlus2 % 1000];
            WizItem round = Wiz3.Items[(int)Wiz3ItemIndex.RoundShield % 1000];
            WizItem gloves = Wiz3.Items[(int)Wiz3ItemIndex.MantisGloves % 1000];
            WizItem cuisinart = Wiz3.Items[(int)Wiz3ItemIndex.BladeCuisinart % 1000];
            WizItem blade = Wiz3.Items[(bGood ? (int)Wiz3ItemIndex.IvoryBlade : bEvil ? (int)Wiz3ItemIndex.EbonyBlade : (int)Wiz3ItemIndex.AmberBlade) % 1000];
            WizItem armet = Wiz3.Items[(int)Wiz3ItemIndex.Armet % 1000];

            switch (wizClass)
            {
                case WizClass.Fighter:
                case WizClass.Samurai:
                case WizClass.Lord:
                    items.Add(cuisinart);
                    items.Add(shield);
                    items.Add(plate);
                    items.Add(gloves);
                    items.Add(armet);
                    break;
                case WizClass.Mage:
                    items.Add(blade);
                    items.Add(Wiz3.Items[(int)Wiz3ItemIndex.DisplacerRobes % 1000]);
                    break;
                case WizClass.Priest:
                    items.Add(mace);
                    items.Add(shield);
                    items.Add(breast);
                    items.Add(gloves);
                    break;
                case WizClass.Thief:
                    items.Add(blade);
                    items.Add(round);
                    items.Add(Wiz3.Items[(int)Wiz3ItemIndex.CuirassPlus2 % 1000]);
                    items.Add(Wiz3.Items[(int)Wiz3ItemIndex.GlovesOfMithril % 1000]);
                    break;
                case WizClass.Bishop:
                    items.Add(mace);
                    items.Add(round);
                    items.Add(breast);
                    break;
                case WizClass.Ninja:
                    items.Add(cuisinart);
                    items.Add(shield);
                    items.Add(plate);
                    break;
                default:
                    break;
            }

            if (!items.Any(i => i.CanEquipLocation == EquipLocation.Head))
                items.Add(Wiz3.Items[(int)Wiz3ItemIndex.GoldTiara % 1000]);
            items.Add(Wiz3.Items[(int)Wiz3ItemIndex.DragonsTooth % 1000]);

            return items;
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return Wiz3RosterFile.CreateWiz3(Global.CombineRoster(Game), bSilent);
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(10);
            for (Wiz3Map map = Wiz3Map.TowerLevel1; map < Wiz3Map.Last; map++)
                maps.Add(GetMapTitlePair((int)map));
            return maps;
        }

        public override int GetCurrentMapIndex()
        {
            Int16 inCastle = ReadInt16(Wiz3.Memory.InCastle);
            if (inCastle != 0)
                return 0;

            return base.GetCurrentMapIndex();
        }
    }
}
