using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Wiz1Quest : BasicQuest
    {
        public Wiz1Quest()
        {
        }
    }

    public class Wiz1QuestInfo : QuestInfo
    {
        public bool HasBronzeKey = false;
        public bool HasSilverKey = false;
        public bool HasGoldKey = false;
        public bool HasBlueRibbon = false;
        public bool HasFrogStatue = false;
        public bool HasBearStatue = false;
        public bool HasAmuletOfWerdna = false;
        public MapXY Location;

        public QuestStatus BronzeKey = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Key of Bronze");
        public QuestStatus SilverKey = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Key of Silver");
        public QuestStatus GoldKey = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Key of Gold");
        public QuestStatus BlueRibbon = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Blue Ribbon");
        public QuestStatus FrogStatue = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Statue of Frog");
        public QuestStatus BearStatue = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Statue of Bear");
        public QuestStatus Werdna = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Amulet of Werdna");
        public QuestStatus MurphysGhost = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Murphy's Ghost");
        public QuestStatus FireDragons = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Fire Dragons");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { BronzeKey, SilverKey, GoldKey, BlueRibbon, FrogStatue, BearStatue, Werdna, MurphysGhost, FireDragons };
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<Wiz1Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<Wiz1Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<Wiz1Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<Wiz1Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            Wiz1Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as Wiz1Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<Wiz1Quest> quests = new List<Wiz1Quest>();

            BronzeKey.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.BronzeKey));
            SilverKey.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.SilverKey));
            FrogStatue.PreQuest.Add(AddSideQuest(BronzeKey, quests, "Key of Bronze"));
            FrogStatue.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.FrogStatue));
            BearStatue.PreQuest.Add(AddSideQuest(SilverKey, quests, "Key of Silver"));
            BearStatue.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.BearStatue));
            GoldKey.PreQuest.Add(AddSideQuest(FrogStatue, quests, "Statue of Frog"));
            GoldKey.PreQuest.Add(AddSideQuest(BearStatue, quests, "Statue of Bear"));
            GoldKey.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.GoldKey));
            AddSideQuest(GoldKey, quests, "Key of Gold");
            BlueRibbon.AddLocations(new QuestLocation("Enter the room", Wiz1.Spots.BlueRibbon));
            MurphysGhost.AddLocations(new QuestLocation("Search the statue", Wiz1.Spots.MurphysGhost));
            AddSideQuest(MurphysGhost, quests);
            FireDragons.AddLocations(new QuestLocation("Enter the room", Wiz1.Spots.FireDragon));
            AddSideQuest(FireDragons, quests);
            Werdna.PreQuest.Add(AddMainQuest(BlueRibbon, quests, "Blue Ribbon"));
            Werdna.Prerequisites.Add(new QuestLocation("Gain entrance to level 10 of the Maze", Wiz1.Spots.Chute));
            Werdna.AddLocations(
                new QuestLocation("Defeat the first set of guardians", Wiz1.Spots.Guardian1),
                new QuestLocation("Use the first guardians' teleporter", Wiz1.Spots.GuardianTeleport1),
                new QuestLocation("Defeat the second set of guardians", Wiz1.Spots.Guardian2),
                new QuestLocation("Use the second guardians' teleporter", Wiz1.Spots.GuardianTeleport2),
                new QuestLocation("Defeat the third set of guardians", Wiz1.Spots.Guardian3),
                new QuestLocation("Use the third guardians' teleporter", Wiz1.Spots.GuardianTeleport3),
                new QuestLocation("Defeat the fourth set of guardians", Wiz1.Spots.Guardian4),
                new QuestLocation("Use the fourth guardians' teleporter", Wiz1.Spots.GuardianTeleport4),
                new QuestLocation("Defeat the fifth set of guardians", Wiz1.Spots.Guardian5),
                new QuestLocation("Use the fifth guardians' teleporter", Wiz1.Spots.GuardianTeleport5),
                new QuestLocation("Defeat the sixth set of guardians", Wiz1.Spots.Guardian6),
                new QuestLocation("Use the sixth guardians' teleporter", Wiz1.Spots.GuardianTeleport6),
                new QuestLocation("Defeat Werdna", Wiz1.Spots.Werdna));
            Werdna.Postrequisites.Add(new QuestLocation("Use the amulet (or other means) to teleport back to the Castle", Wiz1.Spots.Werdna));
            AddMainQuest(Werdna, quests, "50000 Gold, 50000 Exp");

            quests.Sort(CompareWiz1Quests);
            return quests.ToArray();
        }

        public int CompareWiz1Quests(Wiz1Quest quest1, Wiz1Quest quest2)
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

            WizCharacter wiz1Char = WizCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize, null, false);
            QuestTotals totals = new QuestTotals(0, 0);

            CharName = wiz1Char.Name;
            CharAddress = iOverrideCharAddress;

            Location = new MapXY((Wiz1Map)location.MapIndex, location.PrimaryCoordinates.X, location.PrimaryCoordinates.Y);

            HasBronzeKey = party.CurrentPartyHasItem(Wiz1ItemIndex.KeyOfBronze);
            HasSilverKey = party.CurrentPartyHasItem(Wiz1ItemIndex.KeyOfSilver);
            HasGoldKey = party.CurrentPartyHasItem(Wiz1ItemIndex.KeyOfGold);
            HasBlueRibbon = party.CurrentPartyHasItem(Wiz1ItemIndex.BlueRibbon);
            HasFrogStatue = party.CurrentPartyHasItem(Wiz1ItemIndex.StatuetteOfFrog);
            HasBearStatue = party.CurrentPartyHasItem(Wiz1ItemIndex.StatuetteOfBear);
            HasAmuletOfWerdna = party.CurrentPartyHasItem(Wiz1ItemIndex.AmuletOfWerdna);

            BronzeKey.AddObj(HasBronzeKey);
            AddQuest(totals, BronzeKey);

            SilverKey.AddObj(HasSilverKey);
            AddQuest(totals, SilverKey);

            GoldKey.AddObj(HasGoldKey);
            AddQuest(totals, GoldKey);

            BlueRibbon.AddObj(HasBlueRibbon);
            AddQuest(totals, BlueRibbon);

            FrogStatue.AddObj(HasFrogStatue);
            AddQuest(totals, FrogStatue);

            BearStatue.AddObj(HasBearStatue);
            AddQuest(totals, BearStatue);

            MurphysGhost.AddObj(Location.Equals(Wiz1.Spots.MurphysGhost));
            AddQuest(totals, MurphysGhost);

            FireDragons.AddObj(Location.Equals(Wiz1.Spots.FireDragon));
            AddQuest(totals, MurphysGhost);

            bool b10 = Location.Map == (int)Wiz1Map.MazeLevel10;
            bool bCombat = state.InCombat;
            Point pt = location.PrimaryCoordinates;

            Rectangle rcGuardian1 = new Rectangle(0, 0, 5, 11);
            Rectangle rcGuardian2a = new Rectangle(0, 0, 4, 20);
            Rectangle rcGuardian2b = new Rectangle(0, 0, 5, 15);
            Rectangle rcGuardian2c = new Rectangle(0, 12, 6, 3);
            Rectangle rcGuardian3a = new Rectangle(0, 0, 5, 20);
            Rectangle rcGuardian3b = new Rectangle(5, 12, 3, 8);
            Rectangle rcGuardian3c = new Rectangle(8, 16, 4, 4);
            Rectangle rcGuardian4a = new Rectangle(0, 0, 5, 20);
            Rectangle rcGuardian4b = new Rectangle(5, 11, 8, 9);
            Rectangle rcGuardian4c = new Rectangle(9, 10, 3, 1);
            Rectangle rcGuardian4d = new Rectangle(13, 14, 5, 6);
            Rectangle rcGuardian5a = new Rectangle(0, 0, 7, 20);
            Rectangle rcGuardian5b = new Rectangle(7, 6, 7, 14);
            Rectangle rcGuardian5c = new Rectangle(14, 12, 6, 8);
            Rectangle rcGuardian6a = new Rectangle(0, 0, 7, 20);
            Rectangle rcGuardian6b = new Rectangle(7, 6, 13, 14);

            Werdna.StartedWhenAnyComplete = true;
            Werdna.MarkAllWhenComplete = true;

            Werdna.AddPre(b10);
            if (bCombat)
            {
                QuestStatus.Single qs = QuestStatus.Single.Invalid("The status of this goal is unavailable while in combat.");
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !rcGuardian1.Contains(pt));
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !Global.PointInRects(pt, rcGuardian2a, rcGuardian2b, rcGuardian2c));
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !Global.PointInRects(pt, rcGuardian3a, rcGuardian3b, rcGuardian3c));
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !Global.PointInRects(pt, rcGuardian4a, rcGuardian4b, rcGuardian4c, rcGuardian4d));
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !Global.PointInRects(pt, rcGuardian5a, rcGuardian5b, rcGuardian5c));
                Werdna.Obj.Add(qs);
                Werdna.AddObj(b10 && !Global.PointInRects(pt, rcGuardian6a, rcGuardian6b));
            }
            else if (b10)
            {
                bool[,] fightMap = WizardryMapData.GetFights(fights);
                Werdna.AddObj(!fightMap[Wiz1.Spots.Guardian1.X, Wiz1.Spots.Guardian1.Y],
                    !rcGuardian1.Contains(pt),
                    !fightMap[Wiz1.Spots.Guardian2.X, Wiz1.Spots.Guardian2.Y],
                    !Global.PointInRects(pt, rcGuardian2a, rcGuardian2b, rcGuardian2c),
                    !fightMap[Wiz1.Spots.Guardian3.X, Wiz1.Spots.Guardian3.Y],
                    !Global.PointInRects(pt, rcGuardian3a, rcGuardian3b, rcGuardian3c),
                    !fightMap[Wiz1.Spots.Guardian4.X, Wiz1.Spots.Guardian4.Y],
                    !Global.PointInRects(pt, rcGuardian4a, rcGuardian4b, rcGuardian4c, rcGuardian4d),
                    !fightMap[Wiz1.Spots.Guardian5.X, Wiz1.Spots.Guardian5.Y],
                    !Global.PointInRects(pt, rcGuardian5a, rcGuardian5b, rcGuardian5c),
                    !fightMap[Wiz1.Spots.Guardian6.X, Wiz1.Spots.Guardian6.Y],
                    !Global.PointInRects(pt, rcGuardian6a, rcGuardian6b),
                    HasAmuletOfWerdna);
            }
            else
            {
                Werdna.AddObj(12, QuestGoal.NotStarted);
                Werdna.AddObj(HasAmuletOfWerdna);
            }
            Werdna.AddPost(wiz1Char.Honors != 0);

            AddQuest(totals, Werdna);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}