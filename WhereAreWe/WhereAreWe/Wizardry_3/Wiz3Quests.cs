using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Wiz3Quest : BasicQuest
    {
        public Wiz3Quest()
        {
        }
    }

    public class Wiz3QuestInfo : QuestInfo
    {
        public QuestStatus OrbOfEarithin = new QuestStatus(QuestStatus.Basic.NotStarted, "Find and return the Orb of Earithin");
        public QuestStatus CrystalOfGood = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the Crystal of Good");
        public QuestStatus CrystalOfEvil = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the Crystal of Evil");
        public QuestStatus ShipInBottle = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Ship-in-a-Bottle");
        public QuestStatus StaffOfEarth = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Staff of Earth");
        public QuestStatus AmuletOfAir = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain an Amulet of Air");
        public QuestStatus RodOfFire = new QuestStatus(QuestStatus.Basic.NotStarted, "Purchase a Rod of Fire");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { OrbOfEarithin, CrystalOfGood, CrystalOfEvil, ShipInBottle, StaffOfEarth, AmuletOfAir, RodOfFire };
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<Wiz3Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<Wiz3Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<Wiz3Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<Wiz3Quest> quests, string strReward = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<Wiz3Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            Wiz3Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as Wiz3Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<Wiz3Quest> quests = new List<Wiz3Quest>();

            CrystalOfEvil.Prerequisites.Add(new QuestLocation("Have a party of at least as many Good as Evil", Wiz3.Spots.None));
            CrystalOfEvil.Prerequisites.Add(new QuestLocation("Go to the second floor of the Tower", Wiz3.Spots.StairsTo2));
            CrystalOfEvil.Prerequisites.Add(new QuestLocation("Go to the fourth floor of the Tower", Wiz3.Spots.StairsTo4));
            CrystalOfEvil.AddLocations(new QuestLocation("Defeat Delf and his minions", Wiz3.Spots.CrystalOfEvil));

            CrystalOfGood.Prerequisites.Add(new QuestLocation("Have a party containing no Good characters", Wiz3.Spots.None));
            CrystalOfGood.Prerequisites.Add(new QuestLocation("Go to the third floor of the Tower", Wiz3.Spots.StairsTo3));
            CrystalOfGood.AddLocations(new QuestLocation("Obtain a Broadsword", Wiz3.Spots.Castle),
                new QuestLocation("Trade a Broadsword for a Gold Medallion", Wiz3.Spots.Medallion),
                new QuestLocation("Trade a Gold Medallion for a vial of Holy Water", Wiz3.Spots.HolyWater),
                new QuestLocation("Go to the fifth floor of the Tower", Wiz3.Spots.StairsTo5),
                new QuestLocation("Defeat the Soul Trapper and his Crusader Lords", Wiz3.Spots.CrystalOfGood));
            OrbOfEarithin.PreQuest.Add(AddMainQuest(1, CrystalOfEvil, quests, "Crystal of Evil"));
            OrbOfEarithin.PreQuest.Add(AddMainQuest(2, CrystalOfGood, quests, "Crystal of Good"));
            OrbOfEarithin.AddLocations(new QuestLocation("Have both the Crystal of Good and Crystal of Evil", Wiz3.Spots.None), 
                new QuestLocation("Invoke the Crystal of Evil with an Evil character", Wiz3.Spots.None),
                new QuestLocation("Invoke the Crystal of Good with a Good character", Wiz3.Spots.None),
                new QuestLocation("Talk to L'KBreth", Wiz3.Spots.LKBreth),
                new QuestLocation("Trade the Neutral Crystal for the Orb of Earithin", Wiz3.Spots.OrbEarithin),
                new QuestLocation("Surrender the Orb of Earithin to the Sages of Llylgamyn", Wiz3.Spots.Castle));
            AddMainQuest(3, OrbOfEarithin, quests);

            StaffOfEarth.Prerequisites.Add(new QuestLocation("Have a party of at least as many Good as Evil", Wiz3.Spots.None));
            StaffOfEarth.Prerequisites.Add(new QuestLocation("Go to the second floor of the Tower", Wiz3.Spots.StairsTo2));
            StaffOfEarth.AddLocations(new QuestLocation("Get past the Fiend", Wiz3.Spots.Fiend),
                new QuestLocation("Search the desk", Wiz3.Spots.StaffOfEarth));
            AddSideQuest(StaffOfEarth, quests, "Staff of Earth");

            AmuletOfAir.Prerequisites.Add(new QuestLocation("Have a party of at least as many Good as Evil", Wiz3.Spots.None));
            AmuletOfAir.Prerequisites.Add(new QuestLocation("Go to the second floor of the Tower", Wiz3.Spots.StairsTo2));
            AmuletOfAir.AddLocations(new QuestLocation("Defeat Po'Le", Wiz3.Spots.AmuletOfAir));
            AddSideQuest(AmuletOfAir, quests, "Amulet of Air");

            RodOfFire.Prerequisites.Add(new QuestLocation("Have a party containing no Good characters", Wiz3.Spots.None));
            RodOfFire.Prerequisites.Add(new QuestLocation("Go to the third floor of the Tower", Wiz3.Spots.StairsTo3));
            RodOfFire.Prerequisites.Add(new QuestLocation("Go to the fifth floor of the Tower", Wiz3.Spots.StairsTo5));
            RodOfFire.AddLocations(new QuestLocation("Pay Abdul 25,000 Gold", Wiz3.Spots.PayRod),
                new QuestLocation("Receive the Rod of Fire from Abdul", Wiz3.Spots.RodOfFire));
            AddSideQuest(RodOfFire, quests, "Rod of Fire");

            ShipInBottle.Prerequisites.Add(new QuestLocation("Go to one of the upper floors (4-6) of the Tower", Wiz3.Spots.None));
            ShipInBottle.AddLocations(new QuestLocation("Fight a Goblin Prince", Wiz3.Spots.None),
                new QuestLocation("Fight a Goblin Shaman", Wiz3.Spots.None),
                new QuestLocation("Fight a Dark Rider", Wiz3.Spots.None),
                new QuestLocation("Fight a Stone Fly", Wiz3.Spots.None),
                new QuestLocation("Fight a Master Ninja", Wiz3.Spots.None),
                new QuestLocation("Fight a Crusader Lord", Wiz3.Spots.None),
                new QuestLocation("Fight a Faerie", Wiz3.Spots.None),
                new QuestLocation("Fight a Seraph", Wiz3.Spots.None),
                new QuestLocation("Fight a T'ien Lung", Wiz3.Spots.None),
                new QuestLocation("Fight a Roc", Wiz3.Spots.None));
            AddSideQuest(ShipInBottle, quests, "Ship in Bottle");

            quests.Sort(CompareWiz3Quests);
            return quests.ToArray();
        }

        public int CompareWiz3Quests(Wiz3Quest quest1, Wiz3Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            WizQuestData wizData = data as WizQuestData;
            if (wizData == null)
                return;

            WizPartyInfo party = wizData.Party as WizPartyInfo;
            LocationInformation location = wizData.Location;
            byte[] fights = wizData.Fights;
            WizGameState state = wizData.State as WizGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            WizCharacter wiz3Char = WizCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize, null, false);
            //Wiz3QuestData questData = data as Wiz3QuestData;
            //bool bCombat = questData == null ? false : questData.InCombat;
            //int[] monsters = questData == null ? null : questData.Monsters;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = wiz3Char.Name;
            CharAddress = iOverrideCharAddress;

            int iGood = party.AlignmentCount(WizAlignment.Good);
            int iEvil = party.AlignmentCount(WizAlignment.Evil);
            bool bHolyWater = party.CurrentPartyHasItem(Wiz3ItemIndex.HolyWater);
            bool bMedallion = party.CurrentPartyHasItem(Wiz3ItemIndex.GoldMedallion);
            bool bGoodCrystal = party.CurrentPartyHasItem(Wiz3ItemIndex.CrystalOfGood);
            bool bEvilCrystal = party.CurrentPartyHasItem(Wiz3ItemIndex.CrystalOfEvil);
            bool bNeut = party.CurrentPartyHasItem(Wiz3ItemIndex.NeutralCrystal);
            bool bOrb = party.CurrentPartyHasItem(Wiz3ItemIndex.OrbOfEarithin);
            bool bBottle = party.CurrentPartyHasItem(Wiz3ItemIndex.ShipInBottle);
            bool bFire = party.CurrentPartyHasItem(Wiz3ItemIndex.RodOfFire);
            bool bStaff = party.CurrentPartyHasItem(Wiz3ItemIndex.StaffOfEarth);
            bool bWon = (wiz3Char.Honors & 0x0020) > 0;
            bool bGNW = bGoodCrystal || bNeut || bOrb || bWon;

            CrystalOfEvil.MarkAllWhenComplete = true;
            CrystalOfEvil.AddPre(iGood >= iEvil);
            CrystalOfEvil.AddPre(location.MapIndex == 2 || location.MapIndex == 4);
            CrystalOfEvil.AddPre(location.MapIndex == 4);
            CrystalOfEvil.AddObj(bEvilCrystal || bNeut || bOrb || bWon);
            AddQuest(totals, CrystalOfEvil);

            CrystalOfGood.MarkAllWhenComplete = true;
            CrystalOfGood.AddPre(iGood == 0);
            CrystalOfGood.AddPre(location.MapIndex == 3 || location.MapIndex == 5);
            CrystalOfGood.AddObj(bGNW || bHolyWater || bMedallion || party.CurrentPartyHasItem(Wiz3ItemIndex.Broadsword),
                bGNW || bHolyWater || party.CurrentPartyHasItem(Wiz3ItemIndex.GoldMedallion),
                bGNW || bHolyWater,
                bGNW || location.MapIndex == 5,
                bGNW);
            AddQuest(totals, CrystalOfGood);

            bool bLKBreth = location.MapIndex != 6 || Global.PointInRects(location.PrimaryCoordinates, new Rectangle(5, 0, 10, 1), new Rectangle(6, 0, 8, 2), new Rectangle(9, 2, 2, 2));

            OrbOfEarithin.MarkAllWhenComplete = true;
            OrbOfEarithin.AddObj(bNeut || bOrb || bWon || (bGoodCrystal && bEvilCrystal),
                bNeut || bOrb || bWon, bNeut || bOrb || bWon,
                !bLKBreth || bOrb || bWon,
                bOrb || bWon,
                bWon);
            AddQuest(totals, OrbOfEarithin);

            bool bFiend = Global.PointInRects(location.PrimaryCoordinates, new Rectangle(3, 0, 7, 2), new Rectangle(3, 2, 6, 1));

            StaffOfEarth.MarkAllWhenComplete = true;
            StaffOfEarth.AddPre(iGood >= iEvil);
            StaffOfEarth.AddPre(location.MapIndex == 2);
            StaffOfEarth.AddObj(bFiend || bStaff,
                bStaff);
            AddQuest(totals, StaffOfEarth);

            AmuletOfAir.MarkAllWhenComplete = true;
            AmuletOfAir.AddPre(iGood >= iEvil);
            AmuletOfAir.AddPre(location.MapIndex == 2);
            AmuletOfAir.AddObj(party.CurrentPartyHasItem(Wiz3ItemIndex.AmuletOfAir));
            AddQuest(totals, AmuletOfAir);

            RodOfFire.MarkAllWhenComplete = true;
            RodOfFire.AddPre(iGood == 0);
            RodOfFire.AddPre(location.MapIndex == 3 || location.MapIndex == 5);
            RodOfFire.AddPre(location.MapIndex == 5);
            RodOfFire.AddObj(bFire || (location.MapIndex == 5 &&
                (location.PrimaryCoordinates == Wiz3.Spots.PayRod.Location || location.PrimaryCoordinates == Wiz3.Spots.RodOfFire.Location)),
                bFire);
            AddQuest(totals, RodOfFire);

            ShipInBottle.MarkAllWhenComplete = true;
            ShipInBottle.AddPre(location.MapIndex > 3 || bBottle);
            ShipInBottle.AddObj(10, bBottle ? QuestGoal.Complete : QuestGoal.Incomplete);
            AddQuest(totals, ShipInBottle);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}