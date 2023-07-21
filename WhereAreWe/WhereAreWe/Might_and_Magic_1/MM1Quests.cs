using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class MM1QuestData : QuestData
    {
        public MM1QuestData(MM1PartyInfo party, LocationInformation location, MM1MapData map)
        {
            Party = party;
            Location = location;
            Map = map;
        }
    }

    namespace MM1QuestStates
    {
        [Flags]
        public enum Main
        {
            NotStarted = 0x000000,
            AcceptedCourierQuest = 0x000001,
            DeliveredToAgar = 0x000002,
            DeliveredToTelgoran = 0x000004,
            FoundZom = 0x000008,
            FoundZam = 0x000010,
            FoundZomAndZam = 0x000018,
            FoundRubyWhistle = 0x000020,
            FoundStronghold = 0x000040,
            FoundCanine = 0x000080,
            HaveVellumScroll = 0x000100,
            HaveRubyWhistle = 0x000200,
            HaveEyeOfGoros = 0x000400,
            HaveKeyCard = 0x000800,
            FoundSign = 0x001000,
            HaveCoralKey = 0x002000,
            HaveGoldKey = 0x004000,
            InSoulMaze = 0x008000,
            Projector1Visited = 0x010000,
            Projector2Visited = 0x020000,
            Projector3Visited = 0x040000,
            Projector4Visited = 0x080000,
            Projector5Visited = 0x100000,
            AllProjectorsVisited = 0x1f0000,
            EnteredSheltem = 0x400000,
            FinishedGame = 0x800000,
        }

        public enum RepeatIronfist
        {
            Set,
            Reset
        }

        public enum RepeatInspectron
        {
            Set,
            Reset
        }

        public enum Ironfist
        {
            NotStarted,
            AlreadyQuested,
            Q1Accepted,             // Find the Stronghold in Raven`s Wood.
            Q1FoundStronghold,
            Q1TurnedIn,
            Q2Accepted,             // Find Lord Kilburn.
            Q2FoundLordKilburn,
            Q2TurnedIn,
            Q3Accepted,             // Discover the Secret of Portsmith.
            Q3FoundSuccubus,
            Q3TurnedIn,
            Q4Accepted,             // Find the Pirates Secret Cove.
            Q4HavePirateMap,
            Q4FoundCove,
            Q4TurnedIn,
            Q5Accepted,             // Find the shipwreck of the Jolly Raven.
            Q5FoundShipwreck,
            Q5TurnedIn,
            Q6Accepted,             // Defeat the Pirate Ghost Ship.
            Q6DefeatedGhostShip,
            Q6TurnedIn,
            Q7Accepted,             // Defeat the Stronghold in Ravens Wood.
            Q7DefeatedStronghold,
            Q7TurnedIn,
            Completed
        }

        public enum Inspectron
        {
            NotStarted,
            AlreadyQuested,
            Q1Accepted,             // Find the Ancient Ruins in the Quivering Forest.
            Q1FoundRuins,
            Q1TurnedIn,
            Q2Accepted,             // Visit Blithes Peak and report.
            Q2VisitedPeak,
            Q2TurnedIn,
            Q3Accepted,             // Get Cactus Nactar.
            Q3FoundNectar,
            Q3TurnedIn,
            Q4Accepted,             // Find the Shrine of Okzar.
            Q4FoundShrine,
            Q4TurnedIn,
            Q5Accepted,             // Find the Fabled Fountain of Dragadune.
            Q5FoundFountain,
            Q5TurnedIn,
            Q6Accepted,             // Solve the Riddle of the Ruby.
            Q6SolvedRiddle,
            Q6TurnedIn,
            Q7Accepted,             // Defeat the Stronghold in the Enchanted Forest.
            Q7DefeatedStronghold,
            Q7TurnedIn,
            Completed
        }

        public enum Hacker
        {
            NotStarted,
            AlreadyQuested,
            Q1Accepted,             // Bring Lord Hacker Garlic.
            Q1FoundGarlic,
            Q1TurnedIn,
            Q2Accepted,             // Bring Lord Hacker Wolfsbane.
            Q2FoundWolfsbane,
            Q2TurnedIn,
            Q3Accepted,             // Bring Lord Hacker Belladonna.
            Q3FoundBelladonna,
            Q3TurnedIn,
            Q4Accepted,             // Bring Lord Hacker the Head of a Medusa.
            Q4FoundMedusaHead,
            Q4TurnedIn,
            Q5Accepted,             // Bring Lord Hacker the Eye of a Wyvern.
            Q5FoundWyvernEye,
            Q5TurnedIn,
            Q6Accepted,             // Bring Lord Hacker a Dragon's Tooth.
            Q6FoundDragonTooth,
            Q6TurnedIn,
            Q7Accepted,             // Bring Lord Hacker the Ring of Okrim.
            Q7FoundRing,
            Q7TurnedIn,
            Completed
        }

        public enum Alamar
        {
            NotStarted,
            FindCrypt,
        }

        [Flags]
        public enum ClimbTrees
        {
            NotStarted = 0,
            Accepted = 1,
            Climbed0F = 0x000002,
            Climbed0D = 0x000004,
            Climbed0B = 0x000008,
            Climbed09 = 0x000010,
            Climbed07 = 0x000020,
            Climbed05 = 0x000040,
            Climbed2F = 0x000080,
            Climbed2D = 0x000100,
            Climbed2B = 0x000200,
            Climbed29 = 0x000400,
            Climbed27 = 0x000800,
            Climbed25 = 0x001000,
            Climbed4F = 0x002000,
            Climbed4D = 0x004000,
            Climbed4B = 0x008000,
            Climbed49 = 0x010000,
            Climbed47 = 0x020000,
            Climbed45 = 0x040000,
            Climbed6F = 0x080000,
            ClimbedAll = 0x0ffffe,
            Completed = 0x100000
        }

        [Flags]
        public enum MagicSquare
        {
            NotStarted = 0,
            Accepted = 1,
            Set1 = 0x0002,
            Set2 = 0x0004,
            Set3 = 0x0008,
            Set4 = 0x0010,
            Set5 = 0x0020,
            Set6 = 0x0040,
            Set7 = 0x0080,
            Set8 = 0x0100,
            Set9 = 0x0200,
            SetAll = 0x03fe,
            Completed = 0x8000
        }

        [Flags]
        public enum Og
        {
            NotStarted = 0,
            Accepted = 1,
            FoundBlackIdol = 0x02,
            FoundWhiteIdol = 0x04,
            FoundIdols = FoundWhiteIdol | FoundBlackIdol,
            Completed = 0x80
        }

        [Flags]
        public enum Worthy
        {
            NotStarted = 0,
            Accepted = 1,
            UsedIntellect = 0x0002,
            UsedMight = 0x0004,
            UsedPersonality = 0x0008,
            UsedEndurance = 0x0010,
            UsedSpeed = 0x0020,
            UsedAccuracy = 0x0040,
            UsedLuck = 0x0080,
            FoundClerics = 0x0100,
            HeardSharp = 0x0200,
            HeardLoud = 0x0400,
            HeardMellow = 0x0800,
            Completed = 0x1000,
            HeardAll = HeardLoud | HeardMellow | HeardSharp,
            AllStats = UsedIntellect | UsedMight | UsedPersonality | UsedEndurance | UsedSpeed | UsedAccuracy | UsedLuck,
            All = 0x0ffe,
        }

        [Flags]
        public enum Trivia
        {
            NotStarted = 0,
            Accepted = 0x01,
            PaidFee = 0x02,
            LordIronfist = 0x04,
            IBeMe = 0x08,
            Lara = 0x10,
            Og = 0x20,
            CurrentTrends = 0x40,
            AllTrivia = LordIronfist | IBeMe | Lara | Og | CurrentTrends,
        }

        [Flags]
        public enum Beasts
        {
            NotStarted = 0,
            Accepted = 1,
            DefeatedGreatSeaBeast = 0x02,
            DefeatedDarkRider = 0x04,
            DefeatedGargantuanScorpion = 0x08,
            DefeatedGreatWingedBeast = 0x10,
            DefeatedAllBeasts = DefeatedDarkRider | DefeatedGargantuanScorpion | DefeatedGreatSeaBeast | DefeatedGreatWingedBeast,
            Completed = 0x80
        }

        [Flags]
        public enum ErliquinTreasure
        {
            NotStarted = 0,
            Accepted = 1,
            Stole7F = 0x00000002,
            StoleFF = 0x00000004,
            Stole2E = 0x00000008,
            Stole3E = 0x00000010,
            StoleCE = 0x00000020,
            StoleDE = 0x00000040,
            Stole5D = 0x00000080,
            Stole8D = 0x00000100,
            Stole9D = 0x00000200,
            StoleBC = 0x00000400,
            Stole37 = 0x00000800,
            Stole57 = 0x00001000,
            Stole77 = 0x00002000,
            Stole97 = 0x00004000,
            StoleB7 = 0x00008000,
            StoleD7 = 0x00010000,
            StoleF6 = 0x00020000,
            Stole12 = 0x00040000,
            Stole22 = 0x00080000,
            Stole72 = 0x00100000,
            Stole82 = 0x00200000,
            Stole11 = 0x00400000,
            Stole21 = 0x00800000,
            Stole71 = 0x01000000,
            Stole81 = 0x02000000,
            StoleE1 = 0x04000000,
            StoleAll = 0x07fffffe,
        }

        [Flags]
        public enum CrazedEncounters
        {
            NotStarted = 0,
            Accepted = 1,
            Encountered0012 = 0x00000002,
            Encountered1011 = 0x00000004,
            Encountered0710 = 0x00000008,
            Encountered0609 = 0x00000010,
            Encountered0408 = 0x00000020,
            Encountered0607 = 0x00000040,
            Encountered1006 = 0x00000080,
            Encountered0905 = 0x00000100,
            Encountered1504 = 0x00000200,
            Encountered1403 = 0x00000400,
            Encountered0302 = 0x00000800,
            Encountered1101 = 0x00001000,
            Encountered1400 = 0x00002000,
            EncounteredAll =  0x00003ffe,
            Completed =       0x00008000
        }

        public enum IcePrincess
        {
            NotStarted,
            FoundDiamondKey,
            FoundBronzeKey,
            Completed
        }
    }

    public class MM1Quest : BasicQuest
    {
        public MM1Quest()
        {
        }

        public MM1Quest(BasicQuestType type, string name, string giver, string reward)
        {
            QuestType = type;
            Name = name;
            Status.Set(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM1Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = giver;
            Reward = reward;
        }

        public MM1Quest(BasicQuestType type, string name, string giver)
        {
            QuestType = type;
            Name = name;
            Status.Set(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM1Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = giver;
        }

        public MM1Quest(BasicQuestType type, string name)
        {
            QuestType = type;
            Name = name;
            Status.Set(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM1Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = String.Empty;
        }
    }

    public class MM1QuestInfo : QuestInfo
    {
        public MM1QuestStates.Main Main;
        public MM1QuestStates.Ironfist Ironfist;
        public MM1QuestStates.Ironfist RepeatIronfist;
        public MM1QuestStates.Inspectron Inspectron;
        public MM1QuestStates.Inspectron RepeatInspectron;
        public MM1QuestStates.Hacker Hacker;
        public MM1QuestStates.Alamar Alamar;
        public MM1QuestStates.ClimbTrees ClimbTrees;
        public MM1QuestStates.MagicSquare MagicSquare;
        public MM1QuestStates.Og Og;
        public MM1QuestIndex RemovableQuest;
        public MM1QuestStates.Worthy Worthy;
        public MM1QuestStates.Trivia Trivia;
        public MM1QuestStates.Beasts Beasts;
        public MM1PrisonersFlags Prisoners;
        public int PrisonersTreatedAppropriately;
        public int PrisonersTreatedInappropriately;
        public GenericAlignmentValue Alignment;
        public MM1QuestStates.ErliquinTreasure ErliquinTreasure;
        public MM1QuestStates.CrazedEncounters CrazedEncounters;
        public MM1QuestStates.IcePrincess IcePrincess;
        public QuestStatus Canine = new QuestStatus(QuestStatus.Basic.NotStarted, "Discover the secret of the canine");
        public QuestStatus InnerSanctum = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain entry to the Inner Sanctum");
        public QuestStatus RepeatCanine = new QuestStatus(QuestStatus.Basic.NotStarted, "Re-discover the secret of the canine");
        public QuestStatus WhiteWolf = new QuestStatus(QuestStatus.Basic.NotStarted, "Perform services for Castle White Wolf");
        public QuestStatus RepeatWhiteWolf = new QuestStatus(QuestStatus.Basic.NotStarted, "Re-perform services for Castle White Wolf");
        public QuestStatus BlackridgeNorth = new QuestStatus(QuestStatus.Basic.NotStarted, "Perform services for Castle Blackridge North");
        public QuestStatus RepeatBlackridgeNorth = new QuestStatus(QuestStatus.Basic.NotStarted, "Re-perform services for Castle Blackridge North");
        public QuestStatus BlackridgeSouth = new QuestStatus(QuestStatus.Basic.NotStarted, "Perform services for Castle Blackridge South");
        public QuestStatus Grove = new QuestStatus(QuestStatus.Basic.NotStarted, "Climb all of the trees in Guire's grove");
        public QuestStatus TownTreasure = new QuestStatus(QuestStatus.Basic.NotStarted, "Steal all of the town treasure in Erliquin");
        public QuestStatus SquareMagic = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the Cave of Square Magic");
        public QuestStatus OgsPlight = new QuestStatus(QuestStatus.Basic.NotStarted, "End Og's Plight");
        public QuestStatus BecomeWorthy = new QuestStatus(QuestStatus.Basic.NotStarted, "Become worthy for stat improvements");
        public QuestStatus IncreaseStats = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your permanent stats");
        public QuestStatus TriviaIsland = new QuestStatus(QuestStatus.Basic.NotStarted, "Answer the questions on Trivia Island");
        public QuestStatus LuckWheel = new QuestStatus(QuestStatus.Basic.NotStarted, "Spin the Wheel of Luck");
        public QuestStatus Judgement = new QuestStatus(QuestStatus.Basic.NotStarted, "Face the Scale of Judgement");
        public QuestStatus RepeatJudgement = new QuestStatus(QuestStatus.Basic.NotStarted, "Face the Scale of Judgement again");
        public QuestStatus Keys = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain various keys and passes");
        public QuestStatus FakeQuest = new QuestStatus(QuestStatus.Basic.NotStarted, "Refrain from being quested by the false King Alamar");
        public QuestStatus Rejuvenate = new QuestStatus(QuestStatus.Basic.NotStarted, "Rejuvenate in the Land that Time Forgot");
        public QuestStatus CrazedWizard = new QuestStatus(QuestStatus.Basic.NotStarted, "Encounter the 13, win the prize");
        public QuestStatus MiscItems = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain various specific items");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { Canine, InnerSanctum, RepeatCanine, WhiteWolf, RepeatWhiteWolf, BlackridgeNorth, RepeatBlackridgeNorth, BlackridgeSouth,
                Grove, TownTreasure, SquareMagic, OgsPlight, BecomeWorthy, IncreaseStats, TriviaIsland, LuckWheel, Judgement, RepeatJudgement, Keys, FakeQuest,
                Rejuvenate, CrazedWizard, MiscItems };
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<MM1Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM1Quest> quests, object bits, string strGiver, string strReward = "", string strPath = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, bits, strGiver, strReward, strPath);
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<MM1Quest> quests, object bits, string strGiver = "", string strReward = "", string strPath = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, bits, strGiver, strReward, strPath);
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<MM1Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            MM1Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as MM1Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<MM1Quest> quests = new List<MM1Quest>();

            IncreaseStats.AddLocations(new QuestLocation("(+4 Int) Use the strange alien device", MM1.Spots.PermIntellect),
                new QuestLocation("(+4 Might) Step in the glowing pool", MM1.Spots.PermMight),
                new QuestLocation("(+4 Personality) Use the pool", MM1.Spots.PermPersonality),
                new QuestLocation("(+4 Endurance) Use the Pool of Health", MM1.Spots.PermEndurance),
                new QuestLocation("(+4 Speed) Use the Flame of Agility", MM1.Spots.PermSpeed),
                new QuestLocation("(+4 Accuracy) Use the Prism of Precision", MM1.Spots.PermAccuracy),
                new QuestLocation("(+4 Luck) Use the Green Clover", MM1.Spots.PermLuck));

            BecomeWorthy.Prerequisites.Add(new QuestLocation("Talk to the Clerics of the South", MM1.Spots.ClericsSouth));
            BecomeWorthy.AddLocations(new QuestLocation("Hear the Loud tone", MM1Map.E1CastleDragaduneLevel4, -1, -1),
                new QuestLocation("Hear the Mellow tone", MM1Map.E1CastleDragaduneLevel4, -1, -1),
                new QuestLocation("Hear the Sharp tone", MM1Map.E1CastleDragaduneLevel4, -1, -1),
                new QuestLocation("Ring the gong", MM1.Spots.Gong0006),
                new QuestLocation("Ring the gong", MM1.Spots.Gong0008),
                new QuestLocation("Ring the gong", MM1.Spots.Gong0600),
                new QuestLocation("Ring the gong", MM1.Spots.Gong0800),
                new QuestLocation("Ring the gong", MM1.Spots.Gong1506),
                new QuestLocation("Ring the gong", MM1.Spots.Gong1508));
            BecomeWorthy.Postrequisites.Add(new QuestLocation("Return to the Clerics of the South", MM1.Spots.ClericsSouth));
            IncreaseStats.PreQuest.Add(AddSideQuest(BecomeWorthy, quests, new QuestBits(MM1StatIncreaserFlags.Worthy, MM1QuestStates.Worthy.All), "Clerics", String.Empty, Global.RepeatableQuest));
            AddSideQuest(IncreaseStats, quests, new QuestBits(MM1StatIncreaserFlags.AllStats), "Clerics", "+4 to primary stats", Global.RepeatableQuest);

            foreach (QuestStatus qs in new QuestStatus[] { Canine, RepeatCanine })
            {
                qs.AddLocations(new QuestLocation("Find a man in need of courier service", MM1.Spots.Courier),
                    new QuestLocation("Deliver the Vellum Scroll to Agar", MM1.Spots.Agar),
                    new QuestLocation("Deliver the Vellum Scroll to Telgoran", MM1.Spots.Telgoran),
                    new QuestLocation("Find Zom", MM1.Spots.Zom),
                    new QuestLocation("Find Zam", MM1.Spots.Zam),
                    new QuestLocation("Find the Ruby Whistle", MM1.Spots.RubyWhistle),
                    new QuestLocation("Enter the Stronghold", MM1.Spots.Stronghold),
                    new QuestLocation("Activate the canine statue", MM1.Spots.Canine));
            }
            Canine.Postrequisites.Add(new QuestLocation("Obtain the Gold Key", MM1.Spots.GoldKey));
            BasicQuest canine = AddMainQuest(Canine, quests, new QuestBits(MM1MainQuestFlags.All), String.Empty, "13500 Exp, 3500 Gold, Gold Key");
            RepeatCanine.PreQuest.Add(canine);
            if (Canine.Completed)
                AddSideQuest(RepeatCanine, quests, new QuestBits(MM1MainQuestFlags.All), String.Empty, "13500 Exp, 3500 Gold, Gold Key", Global.RepeatableQuest);

            InnerSanctum.PreQuest.Add(canine);
            InnerSanctum.AddLocations(new QuestLocation("Obtain the Eye of Goros", MM1.Spots.EyeOfGoros),
                new QuestLocation("Expose the impostor in Castle Alamar", MM1.Spots.Alamar),
                new QuestLocation("Escape the Soul Maze by answering \"Sheltem\"", MM1.Spots.SoulMaze),
                new QuestLocation("Learn your Sign", MM1.Spots.Gypsy),
                new QuestLocation("Obtain the Coral Key", MM1.Spots.CoralKey),
                new QuestLocation("Obtain the Key Card", MM1.Spots.KeyCard),
                new QuestLocation("Visit Astral Projector #1", MM1.Spots.Projector1),
                new QuestLocation("Visit Astral Projector #2", MM1.Spots.Projector2),
                new QuestLocation("Visit Astral Projector #3", MM1.Spots.Projector3),
                new QuestLocation("Visit Astral Projector #4", MM1.Spots.Projector4),
                new QuestLocation("Visit Astral Projector #5", MM1.Spots.Projector5));
            InnerSanctum.Postrequisites.Add(new QuestLocation("Enter the Inner Sanctum", MM1.Spots.InnerSanctum));
            AddMainQuest(InnerSanctum, quests, new QuestBits(MM1AstralQuestFlags.All));

            foreach (QuestStatus qs in new QuestStatus[] { WhiteWolf, RepeatWhiteWolf })
            {
                qs.Prerequisites.Add(new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist));
                qs.AddLocations(new QuestLocation("Enter (and leave) the Stronghold in the Raven's Wood", MM1.Spots.RavenStronghold),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Obtain a Map of Desert from Lord Kilburn (in first character's backpack)", MM1.Spots.Kilburn),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Defeat, surrender to, or run from the Succubus Queen", MM1.Spots.Succubus),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Trade for a Pirate Map A", MM1.Spots.Trade),
                    new QuestLocation("Trade for a Pirate Map B", MM1.Spots.Trade),
                    new QuestLocation("Find Pirate Cove A", MM1.Spots.PirateA),
                    new QuestLocation("Find Pirate Cove B", MM1.Spots.PirateB),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Find the shipwreck of the Jolly Raven", MM1.Spots.JollyRaven),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Defeat the Pirate Ghost Ship", MM1.Spots.PirateGhost),
                    new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Speak to Lord Ironfist", MM1.Spots.Ironfist),
                    new QuestLocation("Defeat, surrender to, or run from Lord Archer", MM1.Spots.LordArcher));
                qs.Postrequisites.Add(new QuestLocation("Report back to Lord Ironfist", MM1.Spots.Ironfist));
            }
            RepeatWhiteWolf.PreQuest.Add(AddSideQuest(WhiteWolf, quests, new QuestBits(Ironfist), "Lord Ironfist", "34000 Exp"));
            if (WhiteWolf.Completed)
                AddSideQuest(RepeatWhiteWolf, quests, new QuestBits(MM1QuestStates.RepeatIronfist.Set), "Lord Ironfist", "34000 Exp", Global.RepeatableQuest);

            foreach (QuestStatus qs in new QuestStatus[] { BlackridgeNorth, RepeatBlackridgeNorth })
            {
                qs.Prerequisites.Add(new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron));
                qs.AddLocations(new QuestLocation("Enter (and leave) the Ancient Wizard Lair", MM1.Spots.AncientWizardLair),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Visit Blithe's Peak", MM1.Spots.BlithesPeak),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Trade for Cactus Nectar", MM1.Spots.CactusNectar),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Find the Shrine of Okzar", MM1.Spots.Okzar),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Find the Fabled Fountain of Dragadune", MM1.Spots.DragaduneFountain),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Solve the Riddle of the Ruby", MM1.Spots.RubyRiddle),
                    new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Speak to Lord Inspectron", MM1.Spots.Inspectron),
                    new QuestLocation("Defeat or surrender to Gray Minotaur", MM1.Spots.GrayMinotaur));
                qs.Postrequisites.Add(new QuestLocation("Report back to Lord Inspectron", MM1.Spots.Inspectron));
            }
            RepeatBlackridgeNorth.PreQuest.Add(AddSideQuest(BlackridgeNorth, quests, new QuestBits(Inspectron), "Lord Inspectron", "34000 Exp"));
            if (BlackridgeNorth.Completed)
                AddSideQuest(RepeatBlackridgeNorth, quests, new QuestBits(MM1QuestStates.RepeatInspectron.Set), "Lord Inspectron", "34000 Exp", Global.RepeatableQuest);

            BlackridgeSouth.Prerequisites.Add(new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker));
            BlackridgeSouth.AddLocations(new QuestLocation("Obtain Garlic (in first character's backpack)", MM1.Spots.Garlic),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Wolfsbane (in first character's backpack)", MM1.Spots.Wolfsbane),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Belladonna (in first character's backpack)", MM1.Spots.Belladonna),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Medusa Head (in first character's backpack)", MM1.Spots.MedusaHead),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Wyvern Eye (in first character's backpack)", MM1.Spots.WyvernEye),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Dragons Tooth (in first character's backpack)", MM1.Spots.DragonsTooth),
                new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Speak to Lord Hacker", MM1.Spots.Hacker),
                new QuestLocation("Obtain Ring of Okrim (in first character's backpack)", MM1.Spots.Okrim));
            BlackridgeSouth.Postrequisites.Add(new QuestLocation("Report back to Lord Hacker", MM1.Spots.Hacker));
            AddSideQuest(BlackridgeSouth, quests, new QuestBits(Hacker), "Lord Hacker", "34000 Exp");

            FakeQuest.AddLocations(new QuestLocation("Cast C3-7, Remove Quest, to remove your quest", MM1Map.Unknown, -1, -1));
            AddSideQuest(FakeQuest, quests, new QuestBits(RemovableQuest), String.Empty);

            Grove.Prerequisites.Add(new QuestLocation("Talk to Arenko Guire", MM1.Spots.Arenko));
            foreach(MapXY map in MM1.Spots.Trees)
                Grove.MainObjectives.Add(new QuestLocation("Climb the tree", map));
            Grove.Postrequisites.Add(new QuestLocation("Return to Arenko Guire", MM1.Spots.Arenko));
            AddSideQuest(Grove, quests, new QuestBits(ClimbTrees), "Arenko Guire", "3000 Gold, 200 Gems, or random item", Global.RepeatableQuest);

            TownTreasure.Prerequisites.Add(new QuestLocation("Go to the town of Erliquin", MM1.Spots.Erliquin));
            foreach (MapXY map in MM1.Spots.ErliquinTreasures)
                TownTreasure.MainObjectives.Add(new QuestLocation("Steal the town treasure", map));
            AddSideQuest(TownTreasure, quests, new QuestBits(ErliquinTreasure), String.Empty, "15600 Gold, 260 Gems", Global.RepeatableQuest);

            SquareMagic.Prerequisites.Add(new QuestLocation("Go to the Cave of Square Magic", MM1.Spots.SquareCave));
            SquareMagic.AddLocations(new QuestLocation("Set Polyhedron 1", MM1.Spots.Square1402),
                new QuestLocation("Set Polyhedron 2", MM1.Spots.Square1014),
                new QuestLocation("Set Polyhedron 3", MM1.Spots.Square0614),
                new QuestLocation("Set Polyhedron 4", MM1.Spots.Square0202),
                new QuestLocation("Set Polyhedron 5", MM1.Spots.Square0210),
                new QuestLocation("Set Polyhedron 6", MM1.Spots.Square0606),
                new QuestLocation("Set Polyhedron 7", MM1.Spots.Square1006),
                new QuestLocation("Set Polyhedron 8", MM1.Spots.Square1410),
                new QuestLocation("Set Polyhedron 9", MM1.Spots.Square0206));
            SquareMagic.Postrequisites.Add(new QuestLocation("Pull the platinum lever", MM1.Spots.SquareLever));
            AddSideQuest(SquareMagic, quests, new QuestBits(MagicSquare), String.Empty, "2 Int, 20 Gems, 200 Gold, 2000 Exp", Global.RepeatableQuest);

            OgsPlight.AddLocations(new QuestLocation("Find the Black Queen Idol", MM1.Spots.BlackQueen),
                new QuestLocation("Find the White Queen Idol", MM1.Spots.WhiteQueen));
            OgsPlight.Postrequisites.Add(new QuestLocation("Answer \"Queen to Kings Level 1\"", MM1.Spots.Og));
            AddSideQuest(OgsPlight, quests, new QuestBits(Og), String.Empty, "25000 Exp", Global.RepeatableQuest);

            TriviaIsland.Prerequisites.Add(new QuestLocation("Pay the entrance fee (500 Gold)", MM1.Spots.TriviaEntrance));
            TriviaIsland.Prerequisites.Add(new QuestLocation("Pull the branch for a free chance", MM1.Spots.TriviaBranch));
            TriviaIsland.AddLocations(new QuestLocation("Answer \"Where's the very latest?\" (Current Trends)", MM1.Spots.TriviaCurrentTrends),
                new QuestLocation("Answer \"Who be ye?\" (I Be Me)", MM1.Spots.TriviaIBeMe),
                new QuestLocation("Answer \"Who is the voluptuous one? (Lara)", MM1.Spots.TriviaLara),
                new QuestLocation("Answer \"Who rules Castle W.W.?\" (Lord Ironfist)", MM1.Spots.TriviaIronfist),
                new QuestLocation("Answer \"Who's lost sight?\" (Og)", MM1.Spots.TriviaOg));
            AddSideQuest(TriviaIsland, quests, new QuestBits(MM1QuestStates.Trivia.AllTrivia), String.Empty, "250 Gems", Global.RepeatableQuest);

            LuckWheel.AddLocations(new QuestLocation("Defeat the Dark Rider", MM1.Spots.DarkRider),
                new QuestLocation("Defeat the Gargantuan Scorpion", MM1.Spots.Scorpion),
                new QuestLocation("Defeat the Great Sea Beast", MM1.Spots.SeaBeast),
                new QuestLocation("Defeat the Winged Beast", MM1.Spots.WingedBeast));
            LuckWheel.Postrequisites.Add(new QuestLocation("Spin the Wheel", MM1.Spots.LuckWheel));
            AddSideQuest(LuckWheel, quests, new QuestBits(MM1GreatBeastsFlags.All), String.Empty, "Random", Global.RepeatableQuest);

            foreach (QuestStatus qs in new QuestStatus[] { Judgement, RepeatJudgement })
            {
                qs.Information.Add(new QuestLocation(String.Format("Prisoners treated appropriately: {0}/6", PrisonersTreatedAppropriately), MM1Map.None, -1, -1));
                qs.Information.Add(new QuestLocation(String.Format("Prisoners treated inappropriately: {0}/6", PrisonersTreatedInappropriately), MM1Map.None, -1, -1));
                string strAction = (Alignment == GenericAlignmentValue.Good ? "Torment" : (Alignment == GenericAlignmentValue.Evil ? "Free" : "Leave"));
                qs.AddLocations(new QuestLocation("Speak to the Wizard Ranalou", MM1.Spots.Ranalou),
                    new QuestLocation(String.Format("{0} the man in shackles", strAction), MM1.Spots.PrisonerMan),
                    new QuestLocation(String.Format("{0} the mysterious cloaked figure", strAction), MM1.Spots.PrisonerCloak),
                    new QuestLocation(String.Format("{0} the vicious demon", strAction), MM1.Spots.PrisonerDemon));
                strAction = (Alignment == GenericAlignmentValue.Evil ? "Torment" : (Alignment == GenericAlignmentValue.Good ? "Free" : "Leave"));
                qs.AddLocations(new QuestLocation(String.Format("{0} the small child", strAction), MM1.Spots.PrisonerChild),
                    new QuestLocation(String.Format("{0} the mutated creature", strAction), MM1.Spots.PrisonerCreature),
                    new QuestLocation(String.Format("{0} the fair maiden", strAction), MM1.Spots.PrisonerMaiden));
                qs.Postrequisites.Add(new QuestLocation("Go to the Scale of Judgement", MM1.Spots.Scale));
            }
            BasicQuest judgement = AddSideQuest(Judgement, quests, new QuestBits(MM1PrisonersFlags.All), String.Empty, "24576 Exp, +3 to random stat");
            RepeatJudgement.PreQuest.Add(judgement);
            if (Judgement.Completed)
                AddSideQuest(RepeatJudgement, quests, new QuestBits(MM1PrisonersFlags.All), String.Empty, "24576 Exp, +3 to random stat", Global.RepeatableQuest);

            Keys.AddLocations(new QuestLocation("(Diamond Key) Answer \"Love\" once", MM1.Spots.IcePrincess),
                new QuestLocation("(Bronze Key) Answer \"Love\" twice", MM1.Spots.IcePrincess),
                new QuestLocation("(Silver Key) Search the Ancient Glacier", MM1.Spots.SilverKey),
                new QuestLocation("(Crystal Key) Answer \"Crystal\" to the ruby's riddle", MM1.Spots.RubyRiddle),
                new QuestLocation("(Gold Key) Search after completing the canine quest", MM1.Spots.Canine),
                new QuestLocation("(Coral Key) Tell your signs to the hooded figure", MM1.Spots.RubyRiddle),
                new QuestLocation("(King's Pass) Promise to help Percella the Druid", MM1.Spots.Percella),
                new QuestLocation("(Merchant's Pass) Search the deserted merchant wagons", MM1.Spots.MerchantsPass),
                new QuestLocation("(Map of Desert) Accept Lord Kilburn's request", MM1.Spots.Kilburn));
            AddSideQuest(Keys, quests, null, String.Empty, String.Empty, Global.RepeatableQuest);

            MiscItems.AddLocations(new QuestLocation("(Laser Blaster) Defeat the ALIEN", MM1.Spots.LaserBlaster),
                new QuestLocation("(Thundranium) Defeat the INVISIBLE THING", MM1.Spots.Thundranium1),
                new QuestLocation("Collect the THUNDRANIUM", MM1.Spots.Thundranium2),
                new QuestLocation("Collect the THUNDRANIUM", MM1.Spots.Thundranium3),
                new QuestLocation("Collect the THUNDRANIUM", MM1.Spots.Thundranium4),
                new QuestLocation("Rope & Hooks, Smelling Salts, Bag of Garbage", MM1.Spots.RopeSaltGarbage),
                new QuestLocation("Collect the Undead Amulet", MM1.Spots.UndeadAmulet));
            AddSideQuest(MiscItems, quests, null, String.Empty, String.Empty, Global.RepeatableQuest);

            Rejuvenate.AddLocations(new QuestLocation("Turn the hourglass", MM1.Spots.Hourglass));
            AddSideQuest(Rejuvenate, quests, null, String.Empty, "-20 Age", Global.RepeatableQuest);

            CrazedWizard.Prerequisites.Add(new QuestLocation("Go to the Crazed Wizard cave", MM1.Spots.CrazedWizardCave));
            foreach (MapXY map in MM1.Spots.CWEncounters)
                CrazedWizard.MainObjectives.Add(new QuestLocation("Encounter the enemies", map));
            CrazedWizard.Postrequisites.Add(new QuestLocation("Return to the Crazed Wizard", MM1.Spots.CrazedWizard));
            AddSideQuest(CrazedWizard, quests, new QuestBits(CrazedEncounters), String.Empty, "12000 Gold, Bronze Key, Misc Item", Global.RepeatableQuest);

            quests.Sort(CompareMM1Quests);
            return quests.ToArray();
        }

        public int CompareMM1Quests(MM1Quest quest1, MM1Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        private QuestLocation GetAlreadyQuestedQuestLocation()
        {
            switch (RemovableQuest)
            {
                case MM1QuestIndex.Ironfist1:
                case MM1QuestIndex.Ironfist2:
                case MM1QuestIndex.Ironfist3:
                case MM1QuestIndex.Ironfist4:
                case MM1QuestIndex.Ironfist5:
                case MM1QuestIndex.Ironfist6:
                case MM1QuestIndex.Ironfist7:
                    return new QuestLocation("Finish your quest for Lord Ironfist (or cast C3-7, Remove Quest)", MM1Map.B3CastleWhiteWolf, 1, 8);
                case MM1QuestIndex.Inspectron1:
                case MM1QuestIndex.Inspectron2:
                case MM1QuestIndex.Inspectron3:
                case MM1QuestIndex.Inspectron4:
                case MM1QuestIndex.Inspectron5:
                case MM1QuestIndex.Inspectron6:
                case MM1QuestIndex.Inspectron7:
                    return new QuestLocation("Finish your quest for Lord Inspectron (or cast C3-7, Remove Quest)", MM1Map.B1CastleBlackridgeNorth, 7, 4);
                case MM1QuestIndex.Hacker1:
                case MM1QuestIndex.Hacker2:
                case MM1QuestIndex.Hacker3:
                case MM1QuestIndex.Hacker4:
                case MM1QuestIndex.Hacker5:
                case MM1QuestIndex.Hacker6:
                case MM1QuestIndex.Hacker7:
                    return new QuestLocation("Finish your quest for Lord Hacker (or cast C3-7, Remove Quest)", MM1Map.B1CastleBlackridgeSouth, 11, 7);
                case MM1QuestIndex.FakeAlamar:
                    return new QuestLocation("Cast C3-7, Remove Quest, to remove your quest for the fake King Alamar", MM1Map.Unknown, -1, -1);
                default:
                    return new QuestLocation("Cast C3-7, Remove Quest", MM1Map.Unknown, -1, -1);
            }
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress = -1)
        {
            MM1PartyInfo party = data.Party as MM1PartyInfo;
            LocationInformation location = data.Location;
            MM1MapData map = data.Map as MM1MapData;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            MM1Character mm1Char = MM1Character.Create(party.Bytes, iOverrideCharAddress * party.CharacterSize, false);
            if (mm1Char == null)
                return;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = mm1Char.CharName;
            CharAddress = iOverrideCharAddress;

            Main = MM1QuestStates.Main.NotStarted;
            if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.FinishedGame))
            {
                Main = MM1QuestStates.Main.FinishedGame;
            }
            else
            {
                if (party.CurrentPartyHasItem(MM1ItemIndex.VellumScroll))
                    Main |= MM1QuestStates.Main.HaveVellumScroll;
                if (party.CurrentPartyHasItem(MM1ItemIndex.RubyWhistle))
                    Main |= MM1QuestStates.Main.HaveRubyWhistle;
                if (party.CurrentPartyHasItem(MM1ItemIndex.KeyCard))
                    Main |= MM1QuestStates.Main.HaveKeyCard;
                if (party.CurrentPartyHasItem(MM1ItemIndex.GoldKey))
                    Main |= MM1QuestStates.Main.HaveGoldKey;
                if (party.CurrentPartyHasItem(MM1ItemIndex.EyeOfGoros))
                    Main |= MM1QuestStates.Main.HaveEyeOfGoros;
                if (party.CurrentPartyHasItem(MM1ItemIndex.CoralKey) || location.MapIndex == (int)MM1Map.C4Volcano)
                    Main |= MM1QuestStates.Main.HaveCoralKey;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.EnteredSheltem))
                    Main |= MM1QuestStates.Main.EnteredSheltem;
                if (location.MapIndex == (int)MM1Map.SoulMaze)
                    Main |= MM1QuestStates.Main.InSoulMaze;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.Projector1Visited))
                    Main |= MM1QuestStates.Main.Projector1Visited;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.Projector2Visited))
                    Main |= MM1QuestStates.Main.Projector2Visited;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.Projector3Visited))
                    Main |= MM1QuestStates.Main.Projector3Visited;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.Projector4Visited))
                    Main |= MM1QuestStates.Main.Projector4Visited;
                if (mm1Char.AstralQuest.HasFlag(MM1AstralQuestFlags.Projector5Visited))
                    Main |= MM1QuestStates.Main.Projector5Visited;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.DeliveredToAgar))
                    Main |= MM1QuestStates.Main.DeliveredToAgar;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.DeliveredToTelgoran))
                    Main |= MM1QuestStates.Main.DeliveredToTelgoran;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.FoundCanine))
                    Main |= MM1QuestStates.Main.FoundCanine;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.FoundRubyWhistle))
                    Main |= MM1QuestStates.Main.FoundRubyWhistle;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.FoundStronghold))
                    Main |= MM1QuestStates.Main.FoundStronghold;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.FoundZam))
                    Main |= MM1QuestStates.Main.FoundZam;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.FoundZom))
                    Main |= MM1QuestStates.Main.FoundZom;
                if (mm1Char.MainQuest.HasFlag(MM1MainQuestFlags.ReceivedScroll))
                    Main |= MM1QuestStates.Main.AcceptedCourierQuest;
                if (mm1Char.Sign != (MM1Sign)0)
                    Main |= MM1QuestStates.Main.FoundSign;
            }

            bool bCompletedCanine = Main.HasFlag(MM1QuestStates.Main.HaveGoldKey) ||
                Main.HasFlag(MM1QuestStates.Main.HaveEyeOfGoros) ||
                Main.HasFlag(MM1QuestStates.Main.EnteredSheltem) ||
                Main.HasFlag(MM1QuestStates.Main.FinishedGame);
            bool bScroll = Main.HasFlag(MM1QuestStates.Main.HaveVellumScroll);
            bool bTelgoran = Main.HasFlag(MM1QuestStates.Main.DeliveredToTelgoran);
            bool bWhistle = Main.HasFlag(MM1QuestStates.Main.FoundRubyWhistle);
            bool bStatue = Main.HasFlag(MM1QuestStates.Main.FoundCanine);
            bool bFinished = Main.HasFlag(MM1QuestStates.Main.FinishedGame);
            bool bSheltem = Main.HasFlag(MM1QuestStates.Main.EnteredSheltem);
            bool bGoros = Main.HasFlag(MM1QuestStates.Main.HaveEyeOfGoros);

            Canine.AddObj(bScroll || bTelgoran || bStatue || bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.DeliveredToAgar) || bTelgoran || bStatue || bCompletedCanine,
                bTelgoran || bWhistle || bStatue || bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.FoundZom) || bWhistle || bStatue || bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.FoundZam) || bWhistle || bStatue || bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.HaveRubyWhistle) || bStatue || bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.FoundStronghold) || bStatue || bCompletedCanine,
                bStatue || bCompletedCanine);
            Canine.AddPost(Main.HasFlag(MM1QuestStates.Main.HaveGoldKey) || bGoros || bFinished);
            AddQuest(totals, Canine);

            InnerSanctum.AddObj(bGoros || bFinished,
                Main.HasFlag(MM1QuestStates.Main.InSoulMaze) || bSheltem || bFinished,
                bSheltem || bFinished,
                Main.HasFlag(MM1QuestStates.Main.FoundSign) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.HaveCoralKey) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.HaveKeyCard) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.Projector1Visited) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.Projector2Visited) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.Projector3Visited) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.Projector4Visited) || bFinished,
                Main.HasFlag(MM1QuestStates.Main.Projector5Visited) || bFinished);
            InnerSanctum.AddPost(bFinished);
            AddQuest(totals, InnerSanctum);

            RepeatCanine.AddObj((bScroll || bWhistle || bTelgoran) && bCompletedCanine,
                (Main.HasFlag(MM1QuestStates.Main.DeliveredToAgar) || bTelgoran) && bCompletedCanine,
                (bTelgoran || bWhistle) && bCompletedCanine,
                (Main.HasFlag(MM1QuestStates.Main.FoundZom) || bWhistle) && bCompletedCanine,
                (Main.HasFlag(MM1QuestStates.Main.FoundZam) || bWhistle) && bCompletedCanine,
                bWhistle && bCompletedCanine,
                Main.HasFlag(MM1QuestStates.Main.FoundStronghold) && bCompletedCanine,
                false);   // Can't really complete the last step; the quest just repeats
            if (Canine.Completed)
                AddQuest(totals, RepeatCanine);

            RemovableQuest = mm1Char.Quest;

            if (mm1Char.CastleQuestStatus != null)
            {
                switch (RemovableQuest)
                {
                    case MM1QuestIndex.Ironfist1:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest1))
                            Ironfist = MM1QuestStates.Ironfist.Q1FoundStronghold;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q1Accepted;
                        break;
                    case MM1QuestIndex.Ironfist2:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest2))
                            Ironfist = MM1QuestStates.Ironfist.Q2FoundLordKilburn;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q2Accepted;
                        break;
                    case MM1QuestIndex.Ironfist3:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest3))
                            Ironfist = MM1QuestStates.Ironfist.Q3FoundSuccubus;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q3Accepted;
                        break;
                    case MM1QuestIndex.Ironfist4:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest4))
                            Ironfist = MM1QuestStates.Ironfist.Q4FoundCove;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q4Accepted;
                        break;
                    case MM1QuestIndex.Ironfist5:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest5))
                            Ironfist = MM1QuestStates.Ironfist.Q5FoundShipwreck;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q5Accepted;
                        break;
                    case MM1QuestIndex.Ironfist6:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest6))
                            Ironfist = MM1QuestStates.Ironfist.Q6DefeatedGhostShip;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q6Accepted;
                        break;
                    case MM1QuestIndex.Ironfist7:
                        if (mm1Char.CastleQuestStatus.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest7))
                            Ironfist = MM1QuestStates.Ironfist.Q7DefeatedStronghold;
                        else
                            Ironfist = MM1QuestStates.Ironfist.Q7Accepted;
                        break;
                    default:
                        if (RemovableQuest != MM1QuestIndex.None)
                            RepeatIronfist = MM1QuestStates.Ironfist.AlreadyQuested;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest7))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q7TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest6))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q6TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest5))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q5TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest4))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q4TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest3))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q3TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest2))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q2TurnedIn;
                        else if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest1))
                            RepeatIronfist = MM1QuestStates.Ironfist.Q1TurnedIn;
                        else
                            RepeatIronfist = MM1QuestStates.Ironfist.NotStarted;

                        if (mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.All))
                        {
                            Ironfist = MM1QuestStates.Ironfist.Completed;
                            CompletedQuests++;
                        }
                        else
                            Ironfist = RepeatIronfist;

                        break;
                }
                if (Ironfist != MM1QuestStates.Ironfist.Completed)
                    RepeatIronfist = Ironfist;

                switch (RemovableQuest)
                {
                    case MM1QuestIndex.Inspectron1:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest1))
                            Inspectron = MM1QuestStates.Inspectron.Q1FoundRuins;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q1Accepted;
                        break;
                    case MM1QuestIndex.Inspectron2:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest2))
                            Inspectron = MM1QuestStates.Inspectron.Q2VisitedPeak;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q2Accepted;
                        break;
                    case MM1QuestIndex.Inspectron3:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest3) || party.CurrentPartyHasItem(MM1ItemIndex.CactusNectar))
                            Inspectron = MM1QuestStates.Inspectron.Q3FoundNectar;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q3Accepted;
                        break;
                    case MM1QuestIndex.Inspectron4:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest4))
                            Inspectron = MM1QuestStates.Inspectron.Q4FoundShrine;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q4Accepted;
                        break;
                    case MM1QuestIndex.Inspectron5:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest5))
                            Inspectron = MM1QuestStates.Inspectron.Q5FoundFountain;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q5Accepted;
                        break;
                    case MM1QuestIndex.Inspectron6:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest6))
                            Inspectron = MM1QuestStates.Inspectron.Q6SolvedRiddle;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q6Accepted;
                        break;
                    case MM1QuestIndex.Inspectron7:
                        if (mm1Char.CastleQuestStatus.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest7))
                            Inspectron = MM1QuestStates.Inspectron.Q7DefeatedStronghold;
                        else
                            Inspectron = MM1QuestStates.Inspectron.Q7Accepted;
                        break;
                    default:
                        if (RemovableQuest != MM1QuestIndex.None)
                            RepeatInspectron = MM1QuestStates.Inspectron.AlreadyQuested;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest7))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q7TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest6))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q6TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest5))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q5TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest4))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q4TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest3))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q3TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest2))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q2TurnedIn;
                        else if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest1))
                            RepeatInspectron = MM1QuestStates.Inspectron.Q1TurnedIn;
                        else
                            RepeatInspectron = MM1QuestStates.Inspectron.NotStarted;

                        if (mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.All))
                        {
                            Inspectron = MM1QuestStates.Inspectron.Completed;
                            CompletedQuests++;
                        }
                        else
                            Inspectron = RepeatInspectron;

                        break;
                }
                if (Inspectron != MM1QuestStates.Inspectron.Completed)
                    RepeatInspectron = Inspectron;

                switch (RemovableQuest)
                {
                    case MM1QuestIndex.Hacker1:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest1) || party.FirstCharHasItem(MM1ItemIndex.Garlic))
                            Hacker = MM1QuestStates.Hacker.Q1FoundGarlic;
                        else
                            Hacker = MM1QuestStates.Hacker.Q1Accepted;
                        break;
                    case MM1QuestIndex.Hacker2:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest2) || party.FirstCharHasItem(MM1ItemIndex.Wolfsbane))
                            Hacker = MM1QuestStates.Hacker.Q2FoundWolfsbane;
                        else
                            Hacker = MM1QuestStates.Hacker.Q2Accepted;
                        break;
                    case MM1QuestIndex.Hacker3:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest3) || party.FirstCharHasItem(MM1ItemIndex.Belladonna))
                            Hacker = MM1QuestStates.Hacker.Q3FoundBelladonna;
                        else
                            Hacker = MM1QuestStates.Hacker.Q3Accepted;
                        break;
                    case MM1QuestIndex.Hacker4:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest4) || party.FirstCharHasItem(MM1ItemIndex.MedusaHead))
                            Hacker = MM1QuestStates.Hacker.Q4FoundMedusaHead;
                        else
                            Hacker = MM1QuestStates.Hacker.Q4Accepted;
                        break;
                    case MM1QuestIndex.Hacker5:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest5) || party.FirstCharHasItem(MM1ItemIndex.WyvernEye))
                            Hacker = MM1QuestStates.Hacker.Q5FoundWyvernEye;
                        else
                            Hacker = MM1QuestStates.Hacker.Q5Accepted;
                        break;
                    case MM1QuestIndex.Hacker6:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest6) || party.FirstCharHasItem(MM1ItemIndex.DragonsTooth))
                            Hacker = MM1QuestStates.Hacker.Q6FoundDragonTooth;
                        else
                            Hacker = MM1QuestStates.Hacker.Q6Accepted;
                        break;
                    case MM1QuestIndex.Hacker7:
                        if (mm1Char.CastleQuestStatus.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest7) || party.FirstCharHasItem(MM1ItemIndex.RingOfOkrim))
                            Hacker = MM1QuestStates.Hacker.Q7FoundRing;
                        else
                            Hacker = MM1QuestStates.Hacker.Q7Accepted;
                        break;
                    default:
                        if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.All))
                        {
                            Hacker = MM1QuestStates.Hacker.Completed;
                            CompletedQuests++;
                        }
                        else if (RemovableQuest != MM1QuestIndex.None)
                            Hacker = MM1QuestStates.Hacker.AlreadyQuested;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest7))
                            Hacker = MM1QuestStates.Hacker.Q7TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest6))
                            Hacker = MM1QuestStates.Hacker.Q6TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest5))
                            Hacker = MM1QuestStates.Hacker.Q5TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest4))
                            Hacker = MM1QuestStates.Hacker.Q4TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest3))
                            Hacker = MM1QuestStates.Hacker.Q3TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest2))
                            Hacker = MM1QuestStates.Hacker.Q2TurnedIn;
                        else if (mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest1))
                            Hacker = MM1QuestStates.Hacker.Q1TurnedIn;
                        else
                            Hacker = MM1QuestStates.Hacker.NotStarted;
                        break;
                }

                bool bIronfistCompleted = mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.Quest7) || 
                    mm1Char.CastleQuestStatus.IronfistRewarded.HasFlag(MM1CastleQuestFlags.All);

                WhiteWolf.AddPre(Ironfist >= MM1QuestStates.Ironfist.Q1Accepted || bIronfistCompleted);
                WhiteWolf.AddObj(Ironfist >= MM1QuestStates.Ironfist.Q1FoundStronghold || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q1TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q2Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q2FoundLordKilburn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q2TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q3Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q3FoundSuccubus || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q3TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4FoundCove || party.CurrentPartyHasItem(MM1ItemIndex.PiratesMapA) || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4FoundCove || party.CurrentPartyHasItem(MM1ItemIndex.PiratesMapB) || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4FoundCove || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4FoundCove || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q4TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q5Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q5FoundShipwreck || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q5TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q6Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q6DefeatedGhostShip || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q6TurnedIn || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q7Accepted || bIronfistCompleted,
                    Ironfist >= MM1QuestStates.Ironfist.Q7DefeatedStronghold || bIronfistCompleted);
                WhiteWolf.AddPost(bIronfistCompleted);
                AddQuest(totals, WhiteWolf);

                if (RepeatIronfist >= MM1QuestStates.Ironfist.Q7TurnedIn)
                {
                    RepeatWhiteWolf.AddPre(false);
                    RepeatWhiteWolf.AddObj(22, Goal(false));
                    RepeatWhiteWolf.AddPost(false);
                }
                else
                {
                    RepeatWhiteWolf.AddPre(RepeatIronfist >= MM1QuestStates.Ironfist.Q1Accepted && bIronfistCompleted);
                    RepeatWhiteWolf.AddObj(RepeatIronfist >= MM1QuestStates.Ironfist.Q1FoundStronghold && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q1TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q2Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q2FoundLordKilburn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q2TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q3Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q3FoundSuccubus && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q3TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4FoundCove || party.CurrentPartyHasItem(MM1ItemIndex.PiratesMapA) && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4FoundCove || party.CurrentPartyHasItem(MM1ItemIndex.PiratesMapB) && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4FoundCove && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4FoundCove && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q4TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q5Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q5FoundShipwreck && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q5TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q6Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q6DefeatedGhostShip && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q6TurnedIn && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q7Accepted && bIronfistCompleted,
                        RepeatIronfist >= MM1QuestStates.Ironfist.Q7DefeatedStronghold && bIronfistCompleted);
                    RepeatWhiteWolf.AddPost(RepeatIronfist >= MM1QuestStates.Ironfist.Q7TurnedIn);
                }
                if (WhiteWolf.Completed)
                    AddQuest(totals, RepeatWhiteWolf);

                bool bInspectronCompleted = mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.Quest7) ||
                    mm1Char.CastleQuestStatus.InspectronRewarded.HasFlag(MM1CastleQuestFlags.All);

                BlackridgeNorth.AddPre(Inspectron >= MM1QuestStates.Inspectron.Q1Accepted || bInspectronCompleted);
                BlackridgeNorth.AddObj(Inspectron >= MM1QuestStates.Inspectron.Q1FoundRuins || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q1TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q2Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q2VisitedPeak || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q2TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q3Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q3FoundNectar || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q3TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q4Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q4FoundShrine || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q4TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q5Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q5FoundFountain || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q5TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q6Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q6SolvedRiddle || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q6TurnedIn || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q7Accepted || bInspectronCompleted,
                    Inspectron >= MM1QuestStates.Inspectron.Q7DefeatedStronghold || bInspectronCompleted);
                BlackridgeNorth.AddPost(bInspectronCompleted);
                AddQuest(totals, BlackridgeNorth);

                if (RepeatInspectron >= MM1QuestStates.Inspectron.Q7TurnedIn)
                {
                    RepeatBlackridgeNorth.AddPre(false);
                    RepeatBlackridgeNorth.AddObj(19, Goal(false));
                    RepeatBlackridgeNorth.AddPost(false);
                }
                else
                {
                    RepeatBlackridgeNorth.AddPre(RepeatInspectron >= MM1QuestStates.Inspectron.Q1Accepted && bInspectronCompleted);
                    RepeatBlackridgeNorth.AddObj(RepeatInspectron >= MM1QuestStates.Inspectron.Q1FoundRuins && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q1TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q2Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q2VisitedPeak && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q2TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q3Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q3FoundNectar && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q3TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q4Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q4FoundShrine && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q4TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q5Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q5FoundFountain && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q5TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q6Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q6SolvedRiddle && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q6TurnedIn && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q7Accepted && bInspectronCompleted,
                        RepeatInspectron >= MM1QuestStates.Inspectron.Q7DefeatedStronghold && bInspectronCompleted);
                    RepeatBlackridgeNorth.AddPost(RepeatInspectron >= MM1QuestStates.Inspectron.Q7TurnedIn);
                }
                if (BlackridgeNorth.Completed)
                    AddQuest(totals, RepeatBlackridgeNorth);

                bool bHackerCompleted = mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.Quest7) ||
                    mm1Char.CastleQuestStatus.HackerRewarded.HasFlag(MM1CastleQuestFlags.All);

                BlackridgeSouth.AddPre(Hacker >= MM1QuestStates.Hacker.Q1Accepted || bHackerCompleted);
                BlackridgeSouth.AddObj(Hacker >= MM1QuestStates.Hacker.Q1FoundGarlic || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q1TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q2Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q2FoundWolfsbane || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q2TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q3Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q3FoundBelladonna || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q3TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q4Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q4FoundMedusaHead || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q4TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q5Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q5FoundWyvernEye || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q5TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q6Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q6FoundDragonTooth || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q6TurnedIn || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q7Accepted || bHackerCompleted,
                    Hacker >= MM1QuestStates.Hacker.Q7FoundRing || bHackerCompleted);
                BlackridgeSouth.AddPost(bHackerCompleted);
                AddQuest(totals, BlackridgeSouth);

                if (RemovableQuest != MM1QuestIndex.FakeAlamar)
                    Alamar = MM1QuestStates.Alamar.NotStarted;
                else
                    Alamar = MM1QuestStates.Alamar.FindCrypt;

                FakeQuest.AddObj(Alamar != MM1QuestStates.Alamar.FindCrypt);
                AddQuest(totals, FakeQuest);
            }

            if (map.StartedArenkoQuest == 0 || location.MapIndex != (int)MM1Map.D3Surface)
                ClimbTrees = MM1QuestStates.ClimbTrees.NotStarted;
            else if ((map.AttributeByteAt(0, 2) & 0x80) == 0)
                ClimbTrees = MM1QuestStates.ClimbTrees.Completed;
            else
            {
                ClimbTrees = MM1QuestStates.ClimbTrees.Accepted;
                if ((map.AttributeByteAt(0, 5) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed05;
                if ((map.AttributeByteAt(0, 7) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed07;
                if ((map.AttributeByteAt(0, 9) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed09;
                if ((map.AttributeByteAt(0, 11) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed0B;
                if ((map.AttributeByteAt(0, 13) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed0D;
                if ((map.AttributeByteAt(0, 15) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed0F;
                if ((map.AttributeByteAt(2, 5) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed25;
                if ((map.AttributeByteAt(2, 7) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed27;
                if ((map.AttributeByteAt(2, 9) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed29;
                if ((map.AttributeByteAt(2, 11) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed2B;
                if ((map.AttributeByteAt(2, 13) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed2D;
                if ((map.AttributeByteAt(2, 15) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed2F;
                if ((map.AttributeByteAt(4, 5) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed45;
                if ((map.AttributeByteAt(4, 7) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed47;
                if ((map.AttributeByteAt(4, 9) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed49;
                if ((map.AttributeByteAt(4, 11) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed4B;
                if ((map.AttributeByteAt(4, 13) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed4D;
                if ((map.AttributeByteAt(4, 15) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed4F;
                if ((map.AttributeByteAt(6, 15) & 0x80) == 0)
                    ClimbTrees |= MM1QuestStates.ClimbTrees.Climbed6F;
            }

            bool bClimbedAll = (ClimbTrees == MM1QuestStates.ClimbTrees.Completed);
            Grove.AddPre(ClimbTrees != MM1QuestStates.ClimbTrees.NotStarted);
            Grove.AddObj(ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed0F) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed2F) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed4F) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed6F) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed0D) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed2D) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed4D) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed0B) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed2B) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed4B) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed09) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed29) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed49) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed07) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed27) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed47) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed05) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed25) || bClimbedAll,
                ClimbTrees.HasFlag(MM1QuestStates.ClimbTrees.Climbed45) || bClimbedAll);
            Grove.AddPost(bClimbedAll);
            AddQuest(totals, Grove);

            if (location.MapIndex != (int)MM1Map.B1Erliquin)
                ErliquinTreasure = MM1QuestStates.ErliquinTreasure.NotStarted;
            else
            {
                ErliquinTreasure = MM1QuestStates.ErliquinTreasure.Accepted;
                if ((map.AttributeByteAt(7, 15) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole7F;
                if ((map.AttributeByteAt(15, 15) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleFF;
                if ((map.AttributeByteAt(2, 14) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole2E;
                if ((map.AttributeByteAt(3, 14) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole3E;
                if ((map.AttributeByteAt(12, 14) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleCE;
                if ((map.AttributeByteAt(13, 14) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleDE;
                if ((map.AttributeByteAt(5, 13) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole5D;
                if ((map.AttributeByteAt(8, 13) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole8D;
                if ((map.AttributeByteAt(9, 13) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole9D;
                if ((map.AttributeByteAt(11, 12) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleBC;
                if ((map.AttributeByteAt(3, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole37;
                if ((map.AttributeByteAt(5, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole57;
                if ((map.AttributeByteAt(7, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole77;
                if ((map.AttributeByteAt(9, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole97;
                if ((map.AttributeByteAt(11, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleB7;
                if ((map.AttributeByteAt(13, 7) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleD7;
                if ((map.AttributeByteAt(15, 6) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleF6;
                if ((map.AttributeByteAt(1, 2) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole12;
                if ((map.AttributeByteAt(2, 2) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole22;
                if ((map.AttributeByteAt(7, 2) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole72;
                if ((map.AttributeByteAt(8, 2) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole82;
                if ((map.AttributeByteAt(1, 1) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole11;
                if ((map.AttributeByteAt(2, 1) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole21;
                if ((map.AttributeByteAt(7, 1) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole71;
                if ((map.AttributeByteAt(8, 1) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.Stole81;
                if ((map.AttributeByteAt(14, 1) & 0x80) == 0)
                    ErliquinTreasure |= MM1QuestStates.ErliquinTreasure.StoleE1;
            }

            TownTreasure.AddPre(ErliquinTreasure != MM1QuestStates.ErliquinTreasure.NotStarted);
            TownTreasure.AddObj(ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole7F),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleFF),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole2E),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole3E),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleCE),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleDE),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole5D),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole8D),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole9D),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleBC),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole37),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole57),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole77),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole97),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleB7),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleD7),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleF6),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole12),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole22),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole72),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole82),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole11),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole21),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole71),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.Stole81),
                ErliquinTreasure.HasFlag(MM1QuestStates.ErliquinTreasure.StoleE1));
            AddQuest(totals, TownTreasure);

            bool bInCrazedCave = location.MapIndex == (int)MM1Map.C2CrazedWizardCave;
            CrazedWizard.AddPre(bInCrazedCave);
            foreach(MapXY spot in MM1.Spots.CWEncounters)
                CrazedWizard.AddObj(bInCrazedCave && (map.AttributeByteAt(spot.X, spot.Y) & 0x80) == 0);
            CrazedWizard.AddPost(bInCrazedCave && (map.AttributeByteAt(MM1.Spots.CrazedWizard.X, MM1.Spots.CrazedWizard.Y) & 0x80) == 0);
            AddQuest(totals, CrazedWizard);

            if (location.MapIndex != (int)MM1Map.D3CaveOfSquareMagic)
                MagicSquare = MM1QuestStates.MagicSquare.NotStarted;
            else
            {
                MagicSquare = MM1QuestStates.MagicSquare.Accepted;
                if (map.MagicSquare[0] == 0xB3)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set3;
                if (map.MagicSquare[1] == 0xB2)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set2;
                if (map.MagicSquare[2] == 0xB5)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set5;
                if (map.MagicSquare[3] == 0xB8)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set8;
                if (map.MagicSquare[4] == 0xB9)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set9;
                if (map.MagicSquare[5] == 0xB6)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set6;
                if (map.MagicSquare[6] == 0xB7)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set7;
                if (map.MagicSquare[7] == 0xB4)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set4;
                if (map.MagicSquare[8] == 0xB1)
                    MagicSquare |= MM1QuestStates.MagicSquare.Set1;
            }

            SquareMagic.AddPre(MagicSquare != MM1QuestStates.MagicSquare.NotStarted);
            SquareMagic.AddObj(MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set1),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set2),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set3),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set4),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set5),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set6),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set7),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set8),
                MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Set9));
            SquareMagic.AddPost(Goal(MagicSquare.HasFlag(MM1QuestStates.MagicSquare.Completed)));   // The completed bit is never really set; the quest can be completed again immediately
            AddQuest(totals, SquareMagic);

            Og = MM1QuestStates.Og.NotStarted;
            bool bBlackIdol = party.CurrentPartyHasItem(MM1ItemIndex.BlackQueenIdol);
            bool bWhiteIdol = party.CurrentPartyHasItem(MM1ItemIndex.WhiteQueenIdol);
            if (bBlackIdol || bWhiteIdol)
                Og = MM1QuestStates.Og.Accepted;
            if (bBlackIdol)
                Og |= MM1QuestStates.Og.FoundBlackIdol;
            if (bWhiteIdol)
                Og |= MM1QuestStates.Og.FoundWhiteIdol;

            OgsPlight.AddObj(bBlackIdol, bWhiteIdol);
            OgsPlight.AddPost(false);     // This quest is repeatable so the last step is never really completed
            AddQuest(totals, OgsPlight);

            Worthy = MM1QuestStates.Worthy.NotStarted;
            if (mm1Char.IncreasersUsed != (MM1StatIncreaserFlags)0)
                Worthy = MM1QuestStates.Worthy.Accepted;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Intellect))
                Worthy |= MM1QuestStates.Worthy.UsedIntellect;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Might))
                Worthy |= MM1QuestStates.Worthy.UsedMight;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Personality))
                Worthy |= MM1QuestStates.Worthy.UsedPersonality;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Endurance))
                Worthy |= MM1QuestStates.Worthy.UsedEndurance;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Speed))
                Worthy |= MM1QuestStates.Worthy.UsedSpeed;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Accuracy))
                Worthy |= MM1QuestStates.Worthy.UsedAccuracy;
            if (mm1Char.IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Luck))
                Worthy |= MM1QuestStates.Worthy.UsedLuck;
            if (location.MapIndex == (int)MM1Map.E1CastleDragaduneLevel4)
            {
                Worthy |= MM1QuestStates.Worthy.Accepted;
                Worthy |= MM1QuestStates.Worthy.FoundClerics;
                if (map.GongTones[0] > 0)
                    Worthy |= MM1QuestStates.Worthy.HeardLoud;
                if (map.GongTones[1] > 0)
                    Worthy |= MM1QuestStates.Worthy.HeardSharp;
                if (map.GongTones[2] > 0)
                    Worthy |= MM1QuestStates.Worthy.HeardMellow;
            }

            bool bWorthy = (Worthy & MM1QuestStates.Worthy.AllStats) == (MM1QuestStates.Worthy)0;
            BecomeWorthy.AddPre(Worthy.HasFlag(MM1QuestStates.Worthy.FoundClerics));
            BecomeWorthy.AddObj(Worthy.HasFlag(MM1QuestStates.Worthy.HeardLoud),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardMellow),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardSharp),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll),
                Worthy.HasFlag(MM1QuestStates.Worthy.HeardAll));
            BecomeWorthy.AddPost(bWorthy);
            BecomeWorthy.MarkAllWhenComplete = true;
            AddQuest(totals, BecomeWorthy);

            IncreaseStats.AddObj(Worthy.HasFlag(MM1QuestStates.Worthy.UsedIntellect),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedMight),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedPersonality),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedEndurance),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedSpeed),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedAccuracy),
                Worthy.HasFlag(MM1QuestStates.Worthy.UsedLuck));
            AddQuest(totals, IncreaseStats);

            bool bPaidTrivia = false;
            Trivia = MM1QuestStates.Trivia.NotStarted;
            if (location.MapIndex == (int)MM1Map.B4Surface)
            {
                if (map.StartedTrivia == 1)
                {
                    Trivia = MM1QuestStates.Trivia.Accepted;
                    Trivia |= MM1QuestStates.Trivia.PaidFee;
                    bPaidTrivia = true;
                }
                if ((map.AttributeByteAt(6, 0) & 0x80) == 0)
                    Trivia |= MM1QuestStates.Trivia.Lara;
                if ((map.AttributeByteAt(7, 0) & 0x80) == 0)
                    Trivia |= MM1QuestStates.Trivia.Og;
                if ((map.AttributeByteAt(8, 0) & 0x80) == 0)
                    Trivia |= MM1QuestStates.Trivia.CurrentTrends;
                if ((map.AttributeByteAt(9, 0) & 0x80) == 0)
                    Trivia |= MM1QuestStates.Trivia.IBeMe;
                if ((map.AttributeByteAt(8, 1) & 0x80) == 0)
                    Trivia |= MM1QuestStates.Trivia.LordIronfist;
            }

            TriviaIsland.AddPre(bPaidTrivia);
            TriviaIsland.AddPre(bPaidTrivia);
            TriviaIsland.AddObj(Trivia.HasFlag(MM1QuestStates.Trivia.CurrentTrends),
                Trivia.HasFlag(MM1QuestStates.Trivia.IBeMe),
                Trivia.HasFlag(MM1QuestStates.Trivia.Lara),
                Trivia.HasFlag(MM1QuestStates.Trivia.LordIronfist),
                Trivia.HasFlag(MM1QuestStates.Trivia.Og));
            AddQuest(totals, TriviaIsland);

            if (mm1Char.Beasts != (MM1GreatBeastsFlags) 0)
                Beasts = MM1QuestStates.Beasts.Accepted;
            else
                Beasts = MM1QuestStates.Beasts.NotStarted;

            if (mm1Char.Beasts.HasFlag(MM1GreatBeastsFlags.DarkRider))
                Beasts |= MM1QuestStates.Beasts.DefeatedDarkRider;
            if (mm1Char.Beasts.HasFlag(MM1GreatBeastsFlags.GreatSeaBeast))
                Beasts |= MM1QuestStates.Beasts.DefeatedGreatSeaBeast;
            if (mm1Char.Beasts.HasFlag(MM1GreatBeastsFlags.Scorpion))
                Beasts |= MM1QuestStates.Beasts.DefeatedGargantuanScorpion;
            if (mm1Char.Beasts.HasFlag(MM1GreatBeastsFlags.WingedBeast))
                Beasts |= MM1QuestStates.Beasts.DefeatedGreatWingedBeast;

            LuckWheel.AddObj(Beasts.HasFlag(MM1QuestStates.Beasts.DefeatedDarkRider),
                Beasts.HasFlag(MM1QuestStates.Beasts.DefeatedGargantuanScorpion),
                Beasts.HasFlag(MM1QuestStates.Beasts.DefeatedGreatSeaBeast),
                Beasts.HasFlag(MM1QuestStates.Beasts.DefeatedGreatWingedBeast));
            LuckWheel.AddPost(false); // repeatable quest

            Prisoners = mm1Char.Prisoners;
            if (Prisoners.HasFlag(MM1PrisonersFlags.QuestCompleted))
                CompletedQuests++;
            Alignment = mm1Char.BasicAlignmentValue(true);
            PrisonersTreatedAppropriately = mm1Char.PrisonersXP / 32;

            bool bJudgedOnce = Prisoners.HasFlag(MM1PrisonersFlags.QuestCompleted);

            Judgement.AddObj(Prisoners.HasFlag(MM1PrisonersFlags.QuestStarted),
                Prisoners.HasFlag(MM1PrisonersFlags.BlackridgeNorth),
                Prisoners.HasFlag(MM1PrisonersFlags.BlackridgeSouth),
                Prisoners.HasFlag(MM1PrisonersFlags.WhiteWolf),
                Prisoners.HasFlag(MM1PrisonersFlags.Doom),
                Prisoners.HasFlag(MM1PrisonersFlags.Dragadune),
                Prisoners.HasFlag(MM1PrisonersFlags.Alamar));
            Judgement.AddPost(bJudgedOnce);
            Judgement.MarkAllWhenComplete = true;
            AddQuest(totals, Judgement);

            RepeatJudgement.AddObj(Prisoners.HasFlag(MM1PrisonersFlags.QuestStarted) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.BlackridgeNorth) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.BlackridgeSouth) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.WhiteWolf) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.Doom) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.Dragadune) && bJudgedOnce,
                Prisoners.HasFlag(MM1PrisonersFlags.Alamar) && bJudgedOnce);
            RepeatJudgement.AddPost(false);   // The repeatable version of this quest can't really be completed
            RepeatJudgement.MarkAllWhenComplete = true;
            AddQuest(totals, RepeatJudgement);

            int iPrisoners = 0;
            foreach(MM1PrisonersFlags flag in new MM1PrisonersFlags[] { MM1PrisonersFlags.BlackridgeNorth, MM1PrisonersFlags.BlackridgeSouth,
                MM1PrisonersFlags.WhiteWolf, MM1PrisonersFlags.Doom, MM1PrisonersFlags.Dragadune, MM1PrisonersFlags.Alamar })
                if (Prisoners.HasFlag(flag))
                    iPrisoners++;

            PrisonersTreatedInappropriately = iPrisoners - PrisonersTreatedAppropriately;

            Keys.AddObj(party.CurrentPartyHasItem(MM1ItemIndex.DiamondKey),
                party.CurrentPartyHasItem(MM1ItemIndex.BronzeKey),
                party.CurrentPartyHasItem(MM1ItemIndex.SilverKey),
                party.CurrentPartyHasItem(MM1ItemIndex.CrystalKey),
                party.CurrentPartyHasItem(MM1ItemIndex.GoldKey),
                party.CurrentPartyHasItem(MM1ItemIndex.CoralKey),
                party.CurrentPartyHasItem(MM1ItemIndex.KingsPass),
                party.CurrentPartyHasItem(MM1ItemIndex.MerchantsPass),
                party.CurrentPartyHasItem(MM1ItemIndex.MapOfDesert));
            AddQuest(totals, Keys);

            MiscItems.AddObj(party.CurrentPartyHasItem(MM1ItemIndex.LaserBlaster));
            MiscItems.AddObj(4, Goal(party.CurrentPartyHasItem(MM1ItemIndex.Thundranium)));
            MiscItems.AddObj(party.CurrentPartyHasItem(MM1ItemIndex.RopeAndHooks) &&
                party.CurrentPartyHasItem(MM1ItemIndex.SmellingSalt) &&
                party.CurrentPartyHasItem(MM1ItemIndex.BagOfGarbage));
            MiscItems.AddObj(party.CurrentPartyHasItem(MM1ItemIndex.UndeadAmulet));
            AddQuest(totals, MiscItems);

            Rejuvenate.AddObj(mm1Char.Age < 19);
            AddQuest(totals, Rejuvenate);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}