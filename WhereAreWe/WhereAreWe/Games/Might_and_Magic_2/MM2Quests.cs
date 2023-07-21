using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class MM2QuestData : QuestData
    {
        public MM2QuestData(MM2PartyInfo party, LocationInformation location, MM2GameInfo info)
        {
            Party = party;
            Info = info;
            Location = location;
        }
    }

    namespace MM2QuestStates
    {
        [Flags]
        public enum Main
        {
            NotStarted = 0x00,
            MetCoraksSoul = 0x01,
            CompletedPromotionQuest = 0x02,
            WonBlackTripleCrown = 0x04,
            CompletedLamandasQuest = 0x08,
            CompletedKalohnsQuest = 0x10
        }

        [Flags]
        public enum Promotion
        {
            NotStartedKnight = 0,
            NotStartedPaladin = 1,
            NotStartedArcher = 2,
            NotStartedCleric = 3,
            NotStartedSorcerer = 4,
            NotStartedRobber = 5,
            NotStartedNinja = 6,
            NotStartedBarbarian = 7,
            SorcererCodeEvilRight = 0x0008,
            SorcererCodeEvilLeft = 0x0010,
            SorcererCodeGoodLeft = 0x0020,
            SorcererCodeGoodRight = 0x0040,
            SorcererFreedEvil = 0x0080,
            SorcererFreedGood = 0x0100,
            ClericFoundAdmit8Pass = 0x0200,
            ClericFoundCoraksSoul = 0x0400,
            ClericMetGwyndon = 0x0800,
            AccomplishedMainGoal = 0x4000,
            Completed = 0x8000
        }

        public enum Hoardall
        {
            NotStarted,
            NotCrusader,
            Accepted,
            FoundItem,
            Completed
        }

        public enum Slayer
        {
            NotStarted,
            NotCrusader,
            Accepted,
            DefeatedEnemy,
            Completed
        }

        [Flags]
        public enum HoardallLords
        {
            NotCrusader = 0x00,
            NotStarted = 0x01,
            Accepted = 0x02,
            FoundValor = 0x04,
            FoundNoble = 0x08,
            FoundHonor = 0x10,
            FoundAllSwords = FoundValor | FoundNoble | FoundHonor,
            Completed = 0x80,
        }

        [Flags]
        public enum SlayerLords
        {
            NotCrusader = 0x00,
            NotStarted = 0x01,
            Accepted = 0x02,
            DefeatedWinged = 0x04,
            DefeatedCrawling = 0x08,
            DefeatedSlithering = 0x10,
            DefeatedAllEnvoys = DefeatedCrawling | DefeatedSlithering | DefeatedWinged,
            Completed = 0x80,
        }

        public enum Pegasus
        {
            NotStarted,
            MetPegasus,
            NamedPegasus
        }

        public enum MurraysCruise
        {
            NotStarted,
            MadeReservation,
            StartedCruise,
            FinishedCruise
        }

        [Flags]
        public enum TripleCrown
        {
            NotStarted = 0x00,
            BoughtTicket = 0x01,
            WonArena = 0x02,
            WonMonsterBowl = 0x04,
            WonColosseum = 0x08,
            FoundKey = 0x10,
            WonAllBattles = WonArena | WonMonsterBowl | WonColosseum,
            FreedBishop = 0x20,
        }

        public enum ElderDruid
        {
            NotStarted,
            Accepted,
            DefeatedHorvath,
            Completed
        }

        [Flags]
        public enum Haart
        {
            NotStarted = 0x00,
            Accepted = 0x01,
            DefeatedSpazTwit = 0x02,
            FoundPhaser = 0x04,
            FoundLoincloth = 0x08,
            AccomplishedTasks = DefeatedSpazTwit | FoundPhaser | FoundLoincloth,
            Completed = 0x10
        }

        public enum Peabody
        {
            NotCrusader,
            NotStarted,
            Accepted,
            RescuedSherman,
            ShermanInParty,
            Completed
        }

        public enum Murray
        {
            NotStarted,
            Accepted,
            DefeatedDawn,
            Completed
        }

        public enum Circus
        {
            NotStarted,
            FoundCupieDoll,
            GaveDollToMadMan,
            BathedInPool,
            Completed
        }

        [Flags]
        public enum Lamanda
        {
            NotCrusader = 0x000000,
            NotStarted = 0x000001,
            Accepted = 0x000002,
            FoundJ26Fluxer = 0x000004,
            FoundM27Radicon = 0x000008,
            FoundA1Todilor = 0x000010,
            FoundN19Capitor = 0x000020,
            FoundAllComponents = FoundJ26Fluxer | FoundM27Radicon | FoundA1Todilor | FoundN19Capitor,
            FoundWaterDisc = 0x000040,
            FoundAirDisc = 0x000080,
            FoundFireDisc = 0x000100,
            FoundEarthDisc = 0x000200,
            FoundAllDiscs = FoundWaterDisc | FoundAirDisc | FoundFireDisc | FoundEarthDisc,
            FoundWaterTalon = 0x000400,
            FoundAirTalon = 0x000800,
            FoundFireTalon = 0x001000,
            FoundEarthTalon = 0x002000,
            FoundAllTalons = FoundWaterTalon | FoundAirTalon | FoundFireTalon | FoundEarthTalon,
            FoundElementOrb = 0x004000,
            FoundOrbAndTalons = FoundAllTalons | FoundElementOrb,
            DefeatedMegaDragon = 0x008000,
            Completed = 0x010000
        }

        public enum Kalohn
        {
            NotCrusader,
            NotStarted,
            LamandaIncomplete,
            Accepted,
            Completed
        }

        public enum HirelingsDE
        {
            NotStarted,
            AteLiver,
            Completed,
        }

        public enum HirelingsTU
        {
            NotStarted,
            AtePudding,
            Completed,
        }

        public enum HirelingC
        {
            NotStarted,
            AteChopSuey,
            Completed,
        }

        public enum MarksKeys
        {
            NotStarted,
            FoundKeys,
            Completed
        }

        public enum SpellS52
        {
            NotStarted,
            AteBrownie,
            Completed,
            InvalidClass
        }

        public enum SpellC23
        {
            NotStarted,
            AteChips,
            Completed,
            InvalidClass
        }

        [Flags]
        public enum CastleKey
        {
            NotStarted = 0x00,
            DonatedMiddlegate = 0x01,
            DonatedAtlantium = 0x02,
            DonatedTundara = 0x04,
            DonatedVulcania = 0x08,
            DonatedSandsobar = 0x10,
            DonatedAll = DonatedAtlantium | DonatedMiddlegate | DonatedSandsobar | DonatedTundara | DonatedVulcania,
            FoundFarthing = 0x20,
            Completed = 0x40
        }
    }

    public class MM2Quest : BasicQuest
    {
        public MM2Quest()
        {
        }

        public MM2Quest(BasicQuestType type, string name, string giver, string reward)
        {
            QuestType = type;
            Name = name;
            Status = new QuestStatus(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM2Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = giver;
            Reward = reward;
        }

        public MM2Quest(BasicQuestType type, string name, string giver)
        {
            QuestType = type;
            Name = name;
            Status = new QuestStatus(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM2Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = giver;
        }

        public MM2Quest(BasicQuestType type, string name)
        {
            QuestType = type;
            Name = name;
            Status = new QuestStatus(QuestStatus.Basic.NotStarted);
            Primary = new QuestLocation(String.Empty, MM2Map.Unknown, -1, -1);
            Secondary = new List<QuestLocation>();
            Giver = String.Empty;
        }
    }

    public class MM2QuestInfo : QuestInfo
    {
        public byte QuestObject;
        public MM2QuestStates.TripleCrown GreenTripleCrown;
        public MM2QuestStates.TripleCrown YellowTripleCrown;
        public MM2QuestStates.TripleCrown RedTripleCrown;
        public MM2QuestStates.TripleCrown BlackTripleCrown;
        public QuestStatus HirelingC = new QuestStatus(QuestStatus.Basic.NotStarted, "Recruit H. K. Phooey");
        public QuestStatus HirelingsDE = new QuestStatus(QuestStatus.Basic.NotStarted, "Recruit Thund R. and Aeriel");
        public QuestStatus HirelingsTU = new QuestStatus(QuestStatus.Basic.NotStarted, "Recruit Sir Kill and Jed I");
        public QuestStatus HirelingsMisc = new QuestStatus(QuestStatus.Basic.NotStarted, "Recruit various hirelings");
        public QuestStatus SpellFingersOfDeath = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn S5-2, Fingers of Death");
        public QuestStatus SpellNaturesGate = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn C2-3, Nature's Gate");
        public int ShermanTown = 0;

        public QuestStatus Promotion = new QuestStatus(QuestStatus.Basic.NotStarted, "Earn Your Class Promotion");
        public QuestStatus BlackTriple = new QuestStatus(QuestStatus.Basic.NotStarted, "Win the Black Triple Crown");
        public QuestStatus RepeatBlackTriple = new QuestStatus(QuestStatus.Basic.NotStarted, "Win the Black Triple Crown again");
        public QuestStatus HelpKalohnMega = new QuestStatus(QuestStatus.Basic.NotStarted, "Help King Kalohn defeat the Mega Dragon");
        public QuestStatus SquareLake = new QuestStatus(QuestStatus.Basic.NotStarted, "Discover the Secret of the Square Lake Cave");
        public QuestStatus Nordon = new QuestStatus(QuestStatus.Basic.NotStarted, "Find Nordon's lost goblet");
        public QuestStatus RepeatNordon = new QuestStatus(QuestStatus.Basic.NotStarted, "Find Nordon's lost goblet again");
        public QuestStatus Nordonna = new QuestStatus(QuestStatus.Basic.NotStarted, "Rescue Nordonna's Children");
        public QuestStatus Epicurean = new QuestStatus(QuestStatus.Basic.NotStarted, "Become an Epicurean");
        public QuestStatus ChivalrySwords = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Swords of Chivalry");
        public QuestStatus RoyalEnvoys = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Royal Envoys of Evil");
        public QuestStatus PegasusName = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn Your Guardian Pegasus' Name");
        public QuestStatus MageGuilds = new QuestStatus(QuestStatus.Basic.NotStarted, "Join the Mage Guilds");
        public QuestStatus RejuvenateMurrays = new QuestStatus(QuestStatus.Basic.NotStarted, "Rejuvenate at Murray's Resort");
        public QuestStatus GreenTriple = new QuestStatus(QuestStatus.Basic.NotStarted, "Win the Green Triple Crown");
        public QuestStatus YellowTriple = new QuestStatus(QuestStatus.Basic.NotStarted, "Win the Yellow Triple Crown");
        public QuestStatus RedTriple = new QuestStatus(QuestStatus.Basic.NotStarted, "Win the Red Triple Crown");
        public QuestStatus FindItem = new QuestStatus(QuestStatus.Basic.NotStarted, "Find an Item");
        public QuestStatus DefeatEnemy = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat an Enemy");
        public QuestStatus DefeatHorvath = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat The Horvath");
        public QuestStatus RescueSherman = new QuestStatus(QuestStatus.Basic.NotStarted, "Rescue Lord Peabody's Son");
        public QuestStatus PhaserLoincloth = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the Phaser and Loincloth");
        public QuestStatus DefeatDawn = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Dawn");
        public QuestStatus WinCircus = new QuestStatus(QuestStatus.Basic.NotStarted, "Win a Game at the Circus (days 140-170)");
        public QuestStatus MarksKeys = new QuestStatus(QuestStatus.Basic.NotStarted, "Return Mark's Lost Keys");
        public QuestStatus CastleKey = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Castle Key");
        public QuestStatus Spells = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn various spells");
        public QuestStatus PermStats = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your stats permanently");
        public QuestStatus TempStats = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your stats temporarily");
        public QuestStatus Skills = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn secondary skills");
        public QuestStatus MiscItems = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain various specific items");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { Promotion, BlackTriple, RepeatBlackTriple, HelpKalohnMega, SquareLake, Nordon, RepeatNordon, Nordonna, Epicurean,
                ChivalrySwords, RoyalEnvoys, PegasusName, MageGuilds, RejuvenateMurrays, GreenTriple, YellowTriple, RedTriple, FindItem, DefeatEnemy,
                DefeatHorvath, RescueSherman, PhaserLoincloth, DefeatDawn, WinCircus, MarksKeys, CastleKey, Spells, PermStats, TempStats, Skills, MiscItems };
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<MM2Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM2Quest> quests, object bits, string strGiver, string strReward = "", string strPath = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, bits, strGiver, strReward, strPath);
        }

        public BasicQuest AddSideQuest(int iOrder, QuestStatus status, List<MM2Quest> quests, object bits, string strGiver, string strReward = "", string strPath = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Side, status, quests, bits, strGiver, strReward, strPath);
            quest.SortOrder = iOrder;
            return quest;
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<MM2Quest> quests, object bits, string strGiver = "", string strReward = "", string strPath = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, bits, strGiver, strReward, strPath);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<MM2Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            MM2Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as MM2Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        private QuestStatus AddHirelingQuest(ref int TotalQuests, ref int CompletedQuests, MM2HirelingFlags all, MM2HirelingFlags current)
        {
            TotalQuests++;
            if (all.HasFlag(current))
            {
                CompletedQuests++;
                return new QuestStatus(QuestStatus.Basic.Completed);
            }
            return new QuestStatus(QuestStatus.Basic.Accepted);
        }


        private QuestStatus AddSpellQuest(ref int TotalQuests, ref int CompletedQuests, MM2Character mm2Char, SpellType spellType, int spellNumber, int spellLevel)
        {
            if (spellType == SpellType.Sorcerer && mm2Char.Class != MM2Class.Sorcerer && mm2Char.Class != MM2Class.Archer)
                return new QuestStatus(QuestStatus.Basic.InvalidClass);

            if (spellType == SpellType.Sorcerer && spellNumber > 7 && mm2Char.Class != MM2Class.Sorcerer)
                return new QuestStatus(QuestStatus.Basic.InvalidClass);

            if (spellType == SpellType.Cleric && mm2Char.Class != MM2Class.Cleric && mm2Char.Class != MM2Class.Paladin)
                return new QuestStatus(QuestStatus.Basic.InvalidClass);

            if (spellType == SpellType.Cleric && spellNumber > 7 && mm2Char.Class != MM2Class.Cleric)
                return new QuestStatus(QuestStatus.Basic.InvalidClass);

            TotalQuests++;
            if (mm2Char.KnowsSpell(spellType, spellNumber, spellLevel))
            {
                CompletedQuests++;
                return new QuestStatus(QuestStatus.Basic.Completed);
            }
            return new QuestStatus(QuestStatus.Basic.Accepted);
        }

        public override BasicQuest[] GetQuests()
        {
            List<MM2Quest> quests = new List<MM2Quest>(90);

            const string hirelings = "Hirelings";
            const string spells = "Spells";

            Promotion.AddLocations(new QuestLocation("Defeat the Dread Knight", MM2.Spots.DreadKnight),
                new QuestLocation("Defeat the Frost Dragon", MM2.Spots.FrostDragon),
                new QuestLocation("Defeat Baron Wilfrey", MM2.Spots.BaronWilfrey),
                new QuestLocation("Meet Corak's apprentice Gwyndon", MM2.Spots.Gwyndon),
                new QuestLocation("Obtain Corak's Soul", MM2.Spots.CorakSoul),
                new QuestLocation("Obtain an Admit 8 Pass", MM2.Spots.Admit8Pass),
                new QuestLocation("Return Corak's Soul to his body", MM2.Spots.CorakCoffin),
                new QuestLocation("Set the control to Ybmug's left to 23", MM2.Spots.YbmugLeft),
                new QuestLocation("Set the control to Ybmug's right to 46", MM2.Spots.YbmugRight),
                new QuestLocation("Set the control to Yekop's left to 64", MM2.Spots.YekopLeft),
                new QuestLocation("Set the control to Yekop's right to 32", MM2.Spots.YekopRight),
                new QuestLocation("Release Yekop", MM2.Spots.Yekop),
                new QuestLocation("Release Ybmug", MM2.Spots.Ybmug),
                new QuestLocation("Accompany a different class on this quest", MM2Map.Unknown, -1, -1),
                new QuestLocation("Assassinate Dawn", MM2.Spots.Dawn),
                new QuestLocation("Defeat Brutal Bruno", MM2.Spots.BrutalBruno));
            Promotion.Postrequisites.Add(new QuestLocation("Return to Mt. Farview", MM2.Spots.Jurors));
            HelpKalohnMega.PreQuest.Add(AddMainQuest(1, Promotion, quests, new QuestBits(MM2GuildFlags.Advancement, MM2AdvancementFlags.AccomplishedMainGoal), "Jurors of Mt. Farview", "5 Million XP"));

            foreach (QuestStatus qs in new QuestStatus[] { BlackTriple, RepeatBlackTriple })
            {
                qs.AddLocations(new QuestLocation("Purchase a Black Ticket (1000 Gold)", MM2.Spots.BlackTicket),
                    new QuestLocation("Obtain a Black Ticket", MM2.Spots.BlackTicket2),
                    new QuestLocation("Win a Black Ticket battle at the Arena", MM2.Spots.Arena),
                    new QuestLocation("Win a Black Ticket battle at the Colosseum", MM2.Spots.Colosseum),
                    new QuestLocation("Win a Black Ticket battle at the Monster Bowl", MM2.Spots.MonsterBowl),
                    new QuestLocation("Purchase a Black Key (50000 Gold)", MM2.Spots.BlackKey));
                qs.Postrequisites.Add(new QuestLocation("Free the Bishop of Black Battle", MM2.Spots.BlackBishop));
            }
            HelpKalohnMega.PreQuest.Add(AddMainQuest(2, BlackTriple, quests, new QuestBits(MM2QuestFlags2.FreedBlackBishop, MM2ArenaFlags.BlackAll), "Queen Lamanda", "100000 Gold, 210000 Exp"));
            if (BlackTriple.Completed)
                AddSideQuest(4, RepeatBlackTriple, quests, new QuestBits(MM2ArenaFlags.BlackAll), String.Empty, "100000 Gold, 210000 Exp", Global.RepeatableQuest);

            RescueSherman.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            RescueSherman.Prerequisites.Add(new QuestLocation("See Lord Peabody", MM2.Spots.Peabody));
            RescueSherman.AddLocations(new QuestLocation("Defeat the Amazons", MM2.Spots.Amazons),
                new QuestLocation("Recruit Sherman into your party", ShermanTown >= 1 && ShermanTown <= 5 ? MM2.Spots.Inns[ShermanTown - 1] : MM2.Spots.Inn2));
            RescueSherman.Postrequisites.Add(new QuestLocation("Return to Lord Peabody", MM2.Spots.Peabody));
            HelpKalohnMega.PreQuest.Add(AddMainQuest(3, RescueSherman, quests, new QuestBits(MM2QuestFlags2.AcceptedPeabodysQuest, MM2QuestFlags2.FinishedPeabodysQuest, MM2HirelingFlags.Sherman | MM2HirelingFlags.Nakazawa), "Lord Peabody", "Use of Wayback Machine"));

            HelpKalohnMega.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            HelpKalohnMega.Prerequisites.Add(new QuestLocation("Speak to Queen Lamanda", MM2.Spots.KingKalohn));
            HelpKalohnMega.AddLocations(new QuestLocation("Obtain a J-26 Fluxer", MM2.Spots.Fluxer),
                new QuestLocation("Obtain an M-27 Radicon", MM2.Spots.Radicon),
                new QuestLocation("Obtain an A-1 Todilor", MM2.Spots.Todilor),
                new QuestLocation("Obtain an N-19 Capitor", MM2.Spots.Capitor),
                new QuestLocation("Obtain an Element Orb", MM2.Spots.ElementOrb),
                new QuestLocation("Obtain a Water Disc", MM2.Spots.WaterDisc),
                new QuestLocation("Obtain a Water Talon", MM2.Spots.WaterTalon),
                new QuestLocation("Obtain an Air Disc", MM2.Spots.AirDisc),
                new QuestLocation("Obtain an Air Talon", MM2.Spots.AirTalon),
                new QuestLocation("Obtain a Fire Disc", MM2.Spots.FireDisc),
                new QuestLocation("Obtain a Fire Talon", MM2.Spots.FireTalon),
                new QuestLocation("Obtain an Earth Disc", MM2.Spots.EarthDisc),
                new QuestLocation("Obtain an Earth Talon", MM2.Spots.EarthTalon),
                new QuestLocation("Help King Kalohn in the 9th century", MM2.Spots.MegaDragon));
            HelpKalohnMega.Postrequisites.Add(new QuestLocation("Return to Queen Lamanda", MM2.Spots.KingKalohn));
            SquareLake.PreQuest.Add(AddMainQuest(4, HelpKalohnMega, quests, new QuestBits(MM2QuestFlags2.AcceptedLamandasQuests | MM2QuestFlags2.HelpedKalohn | MM2QuestFlags2.FinishedLamandasQuest), "Queen Lamanda"));

            SquareLake.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            SquareLake.AddLocations(new QuestLocation("See King Kalohn", MM2.Spots.KingKalohn),
                new QuestLocation("Decode the riddle", MM2.Spots.SquareRiddle));
            AddMainQuest(5, SquareLake, quests, new QuestBits(MM2QuestFlags2.FinishedKalohnsQuest), "King Kalohn", "50 Million Exp");

            foreach (QuestStatus qs in new QuestStatus[] { Nordon, RepeatNordon })
            {
                qs.Prerequisites.Add(new QuestLocation("Talk to Nordon", MM2.Spots.Nordon));
                qs.AddLocations(new QuestLocation("Obtain a Gold Goblet", MM2.Spots.GoldGoblet));
            }
            Nordon.AddLocations(new QuestLocation("Return to Nordon", MM2.Spots.Nordon));
            Nordon.Postrequisites.Add(new QuestLocation("Help Nordon's sister Nordonna", MM2.Spots.Nordonna));
            RepeatNordon.Postrequisites.Add(new QuestLocation("Return to Nordon", MM2.Spots.Nordon));

            Nordonna.Prerequisites.Add(new QuestLocation("Talk to Nordonna", MM2.Spots.Nordonna));
            Nordonna.AddLocations(new QuestLocation("Rescue Sir Hyron and Drog", MM2.Spots.SirHyronDrog));
            Nordonna.Postrequisites.Add(new QuestLocation("Return to Nordonna", MM2.Spots.Nordonna));
            Nordonna.PreQuest.Add(AddSideQuest(Nordon, quests, new QuestBits(MM2QuestFlags1.AcceptedNordonsQuest | MM2QuestFlags1.FinishedNordonsQuest),
                "Nordon", "S2-1 (Eagle Eye), 2000 Exp, 1000 Gold"));
            AddSideQuest(Nordonna, quests, new QuestBits(MM2QuestFlags1.AcceptedNordonnasQuest | MM2QuestFlags1.FinishedNordonnasQuest |
                MM2QuestFlags1.RescuedDrogAndSirHyron, MM2HirelingFlags.SirHyron, MM2HirelingFlags.Drog), "Nordonna", "Sir Hyron, Drog", hirelings);

            if (Nordonna.Completed)
                AddSideQuest(RepeatNordon, quests, new QuestBits(MM2QuestFlags1.AcceptedNordonsQuest | MM2QuestFlags1.FinishedNordonsQuest),
                    "Nordon", "S2-1 (Eagle Eye), 2000 Exp, 1000 Gold", Global.RepeatableQuest);

            Epicurean.AddLocations(new QuestLocation("Eat Horrors D'oeuvres (10 Gold)", MM2.Spots.Tavern1),
                new QuestLocation("Eat Soup De Ghoul w/Garlic Toast (50 Gold)", MM2.Spots.Tavern1),
                new QuestLocation("Eat Dragon Steak Tartar (100 Gold)", MM2.Spots.Tavern1),
                new QuestLocation("Eat Lightly Salted Tongue of Toad (1000 Gold)", MM2.Spots.Tavern2),
                new QuestLocation("Eat Puree of Gnome (2000 Gold)", MM2.Spots.Tavern2),
                new QuestLocation("Eat Devil's Food Brownie (3000 Gold)", MM2.Spots.Tavern2),
                new QuestLocation("Eat Sizzling Swine Soup (200 Gold)", MM2.Spots.Tavern3),
                new QuestLocation("Eat Red Hot Wolf Nipple Chips (100)", MM2.Spots.Tavern3),
                new QuestLocation("Eat Roast Leg of Wyvern (1000)", MM2.Spots.Tavern3),
                new QuestLocation("Eat Pickled Pixie Brains (5000)", MM2.Spots.Tavern4),
                new QuestLocation("Eat Deep Fried Troll Liver (500)", MM2.Spots.Tavern4),
                new QuestLocation("Eat Cream of Kobold Soup (1000)", MM2.Spots.Tavern4),
                new QuestLocation("Eat Gourmet Dinner B - Wyrm Chop Suey (20 Gold)", MM2.Spots.Tavern5),
                new QuestLocation("Eat Roast Peasant Under Glass (50 Gold)", MM2.Spots.Tavern5),
                new QuestLocation("Eat Phantom Pudding ( Very Low-cal ) (250 Gold)", MM2.Spots.Tavern5));
            Epicurean.Postrequisites.Add(new QuestLocation("Talk to The Gourmet", MM2.Spots.Gourmet));
            AddSideQuest(Epicurean, quests, new QuestBits(MM2MealsEaten.All), "The Gourmet", "100000 Exp", Global.RepeatableQuest);

            ChivalrySwords.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            ChivalrySwords.Prerequisites.Add(new QuestLocation("Ask Lord Hoardall about his Lord's Quest", MM2.Spots.Hoardall));
            ChivalrySwords.AddLocations(new QuestLocation("Obtain the Honor Sword", MM2.Spots.HonorSword),
                new QuestLocation("Obtain the Noble Sword", MM2.Spots.NobleSword),
                new QuestLocation("Obtain the Valor Sword", MM2.Spots.ValorSword));
            ChivalrySwords.Postrequisites.Add(new QuestLocation("Return the swords to Lord Hoardall", MM2.Spots.Hoardall));
            AddSideQuest(ChivalrySwords, quests, new QuestBits(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest | MM2QuestFlags1.FinishedHoardallLordsQuest), "Lord Hoardall", "100000 Exp");

            RoyalEnvoys.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            RoyalEnvoys.Prerequisites.Add(new QuestLocation("Ask Lord Slayer about his Lord's Quest", MM2.Spots.Slayer));
            RoyalEnvoys.AddLocations(new QuestLocation("Defeat the Crawling Envoy of Evil", MM2.Spots.CrawlingEnvoy),
                new QuestLocation("Defeat the Slithering Envoy of Evil", MM2.Spots.SlitheringEnvoy),
                new QuestLocation("Defeat the Winged Envoy of Evil", MM2.Spots.WingedEnvoy));
            RoyalEnvoys.Postrequisites.Add(new QuestLocation("Return to Lord Slayer", MM2.Spots.Slayer));
            AddSideQuest(RoyalEnvoys, quests, new QuestBits(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest | MM2QuestFlags1.FinishedSlayerLordsQuest | MM2QuestFlags1.DefeatedDragonLord | MM2QuestFlags1.DefeatedQueenBeetle | MM2QuestFlags1.DefeatedSerpentKing), "Lord Slayer", "1 Million Exp");

            PegasusName.Prerequisites.Add(new QuestLocation("Meet your Guardian Pegasus", MM2.Spots.MeetPegasus));
            PegasusName.Postrequisites.Add(new QuestLocation("Name your Guardian Pegasus once per year (Meenu)", MM2.Spots.Pegasus));
            AddSideQuest(PegasusName, quests, new QuestBits(MM2GuildFlags.VisitedPegasus, MM2QuestStates.Pegasus.NamedPegasus), "Guardian Pegasus", "100000 Gold", Global.RepeatableQuest);

            MageGuilds.AddLocations(new QuestLocation("Join the Middlegate Mage Guild", MM2.Spots.JoinGuild1),
                new QuestLocation("Join the Atlantium Mage Guild", MM2.Spots.JoinGuild2),
                new QuestLocation("Join the Tundaran Mage Guild", MM2.Spots.JoinGuild3),
                new QuestLocation("Join the Vulcanian Mage Guild", MM2.Spots.JoinGuild4),
                new QuestLocation("Join the Sandsobar Mage Guild", MM2.Spots.JoinGuild5));
            AddSideQuest(MageGuilds, quests, new QuestBits(MM2GuildFlags.JoinedAllGuilds), String.Empty);

            RejuvenateMurrays.Prerequisites.Add(new QuestLocation("Book a ticket on the ferry", MM2.Spots.FerryTicket));
            RejuvenateMurrays.AddLocations(new QuestLocation("Catch the ferry", MM2.Spots.Ferry),
                new QuestLocation("Ride the ferry to Murray's Resort Isle", MM2.Spots.ResortIsle));
            RejuvenateMurrays.Postrequisites.Add(new QuestLocation("Rejuvenate at the Hot Springs", MM2.Spots.HotSprings));
            AddSideQuest(RejuvenateMurrays, quests, new QuestBits(MM2QuestFlags1.UsedMurraysFerry | MM2QuestFlags1.OnMurraysFerry | MM2QuestFlags1.BoughtMurraysTicket), String.Empty, "-5 Age", Global.RepeatableQuest);

            GreenTriple.AddLocations(new QuestLocation("Purchase a Green Ticket (10 Gold)", MM2.Spots.GreenTicket),
                new QuestLocation("Win a Green Ticket battle at the Arena", MM2.Spots.Arena),
                new QuestLocation("Win a Green Ticket battle at the Colosseum", MM2.Spots.Colosseum),
                new QuestLocation("Win a Green Ticket battle at the Monster Bowl", MM2.Spots.MonsterBowl),
                new QuestLocation("Purchase a Green Key (500 Gold)", MM2.Spots.GreenKey));
            GreenTriple.Postrequisites.Add(new QuestLocation("Free the Bishop of Green Battle", MM2.Spots.GreenBishop));
            AddSideQuest(1, GreenTriple, quests, new QuestBits(MM2ArenaFlags.GreenAll), String.Empty, "2200 Gold, 13000 Exp", Global.RepeatableQuest);

            YellowTriple.AddLocations(new QuestLocation("Purchase a Yellow Ticket (50 Gold)", MM2.Spots.YellowTicket),
                new QuestLocation("Win a Yellow Ticket battle at the Arena", MM2.Spots.Arena),
                new QuestLocation("Win a Yellow Ticket battle at the Colosseum", MM2.Spots.Colosseum),
                new QuestLocation("Win a Yellow Ticket battle at the Monster Bowl", MM2.Spots.MonsterBowl),
                new QuestLocation("Purchase a Yellow Key (1000 Gold)", MM2.Spots.YellowKey));
            YellowTriple.Postrequisites.Add(new QuestLocation("Free the Bishop of Yellow Battle", MM2.Spots.YellowBishop));
            AddSideQuest(2, YellowTriple, quests, new QuestBits(MM2ArenaFlags.YellowAll), String.Empty, "10000 Gold, 55000 Exp", Global.RepeatableQuest);

            RedTriple.AddLocations(new QuestLocation("Purchase a Red Ticket (250 Gold)", MM2.Spots.RedTicket),
                new QuestLocation("Win a Red Ticket battle at the Arena", MM2.Spots.Arena),
                new QuestLocation("Win a Red Ticket battle at the Colosseum", MM2.Spots.Colosseum),
                new QuestLocation("Win a Red Ticket battle at the Monster Bowl", MM2.Spots.MonsterBowl),
                new QuestLocation("Purchase a Red Key (2500 Gold)", MM2.Spots.RedKey));
            RedTriple.Postrequisites.Add(new QuestLocation("Free the Bishop of Red Battle", MM2.Spots.RedBishop));
            AddSideQuest(3, RedTriple, quests, new QuestBits(MM2ArenaFlags.RedAll), String.Empty, "32000 Gold, 108000 Exp", Global.RepeatableQuest);

            FindItem.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            FindItem.Prerequisites.Add(new QuestLocation("Ask Lord Hoardall about his Page, Squire or Knight quest", MM2.Spots.Hoardall));
            FindItem.AddLocations(new QuestLocation("Find the item" + 
                (QuestObject == 0 ? "" : ": " + MM2.Items[QuestObject].Name), MM2Map.Unknown, -1, -1));
            FindItem.Postrequisites.Add(new QuestLocation("Return to Lord Hoardall" + 
                (QuestObject == 0 ? "" : String.Format(" ({0} Exp)", MM2.Items[QuestObject].BaseValue * 8)), MM2.Spots.Hoardall));
            AddSideQuest(FindItem, quests, new QuestBits(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest, MM2QuestStates.Hoardall.NotStarted),
                "Lord Hoardall", "Exp based on item", Global.RepeatableQuest);

            DefeatEnemy.Prerequisites.Add(new QuestLocation("Put a Crusader in your party", MM2.Spots.Crusader));
            DefeatEnemy.Prerequisites.Add(new QuestLocation("Ask Lord Slayer about his Page, Squire or Knight quest", MM2.Spots.Slayer));
            DefeatEnemy.AddLocations(new QuestLocation("Defeat the enemy" + 
                (QuestObject == 0 ? "" : ": " + MM2.Monsters[QuestObject].ProperName), MM2Map.Unknown, -1, -1));
            DefeatEnemy.Postrequisites.Add(new QuestLocation("Return to Lord Slayer" +
                (QuestObject == 0 ? "" : String.Format(" ({0} Exp)", Global.MM2SlayerExp(QuestObject))), MM2.Spots.Slayer));
            AddSideQuest(DefeatEnemy, quests, new QuestBits(MM2QuestFlags1.AcceptedSlayerQuest, MM2QuestFlags1.DefeatedSlayersMonster, MM2QuestStates.Slayer.NotStarted),
                "Lord Slayer", "Exp based on enemy", Global.RepeatableQuest);

            DefeatHorvath.Prerequisites.Add(new QuestLocation("Speak to the Elder Druid", MM2.Spots.ElderDruid));
            DefeatHorvath.AddLocations(new QuestLocation("Defeat The Horvath", MM2.Spots.HotSprings));
            DefeatHorvath.Postrequisites.Add(new QuestLocation("Return to the Elder Druid", MM2.Spots.ElderDruid));
            AddSideQuest(DefeatHorvath, quests, new QuestBits(MM2QuestFlags2.AcceptedElderDruidsQuest | MM2QuestFlags2.DefeatedHorvath, MM2SpellIndex.DivineIntervention), "Elder Druid", "C9-1 (Divine Intervention)", spells);

            PhaserLoincloth.Prerequisites.Add(new QuestLocation("Speak to Lord Haart", MM2.Spots.Haart));
            PhaserLoincloth.AddLocations(new QuestLocation("Defeat Spaz Twit", MM2.Spots.SpazTwit),
                new QuestLocation("Defeat The Long One", MM2.Spots.LongOne),
                new QuestLocation("Recover the Phaser", MM2.Spots.SpazTwit));
            PhaserLoincloth.Postrequisites.Add(new QuestLocation("Return the items to Lord Haart", MM2.Spots.Haart));
            AddSideQuest(PhaserLoincloth, quests, new QuestBits(MM2QuestFlags2.AcceptedHaartsQuest, MM2QuestFlags1.DefeatedSpazTwit), "Lord Haart", "250000 Exp, 50000 Gold, 500 Gems", Global.RepeatableQuest);

            WinCircus.Prerequisites.Add(new QuestLocation("Win a Cupie Doll", MM2.Spots.Circus));
            WinCircus.AddLocations(new QuestLocation("Give a Cupie Doll to the mad man", MM2.Spots.MadMan),
                new QuestLocation("Bathe in the pool in the Inner Limits", MM2.Spots.InnerLimits));
            WinCircus.Postrequisites.Add(new QuestLocation("Win a game", MM2.Spots.Circus));
            AddSideQuest(WinCircus, quests, new QuestBits(MM2QuestFlags1.UsedCupieDoll | MM2QuestFlags1.UsedInnerLimitsPool), String.Empty, "+10 to any Statistic", Global.RepeatableQuest);

            MarksKeys.Prerequisites.Add(new QuestLocation("Obtain Mark's Keys", MM2.Spots.MarksKeys));
            MarksKeys.Postrequisites.Add(new QuestLocation("Talk to Mark", MM2.Spots.Mark));
            AddSideQuest(MarksKeys, quests, null, "Mark", "10000 Exp", Global.RepeatableQuest);

            CastleKey.AddLocations(new QuestLocation("Donate at the Gateway Temple (100 Gold)", MM2.Spots.Temple1),
                new QuestLocation("Donate at the Eleusinian Temple (500 Gold)", MM2.Spots.Temple2),
                new QuestLocation("Donate at the White Dove Temple (200 Gold)", MM2.Spots.Temple3),
                new QuestLocation("Donate at the Vulcan Temple (300 Gold)", MM2.Spots.Temple4),
                new QuestLocation("Donate at Temple Benedictus (200 Gold)", MM2.Spots.Temple5),
                new QuestLocation("Obtain the Fe Farthing", MM2Map.Unknown, -1, -1));
            CastleKey.Postrequisites.Add(new QuestLocation("Flick the Fe Farthing into the Feldecarb Fountain", MM2.Spots.FeldecarbFountain));
            AddSideQuest(CastleKey, quests, new QuestBits(MM2DonationFlags.AllTowns), String.Empty, "Castle Key", Global.RepeatableQuest);

            HirelingC.Prerequisites.Add(new QuestLocation("Eat Gourmet Dinner B Wyrm Chop Suey", MM2.Spots.Tavern5));
            HirelingC.Postrequisites.Add(new QuestLocation("Talk to H. K. Phooey", MM2.Spots.Phooey));
            AddSideQuest(HirelingC, quests, new QuestBits(MM2MealsEaten.GourmetDinnerBWyrmChopSuey, MM2HirelingFlags.HKPhooey), String.Empty, String.Empty, hirelings);

            HirelingsDE.Prerequisites.Add(new QuestLocation("Eat Deep Fried Troll Liver", MM2.Spots.Tavern4));
            HirelingsDE.Postrequisites.Add(new QuestLocation("Defeat Old Misers and Cripples", MM2.Spots.Cripples));
            AddSideQuest(HirelingsDE, quests, new QuestBits(MM2MealsEaten.DeepFriedTrollLiver, MM2HirelingFlags.ThundR | MM2HirelingFlags.Aeriel), String.Empty, String.Empty, hirelings);

            HirelingsTU.Prerequisites.Add(new QuestLocation("Eat Phantom Pudding ( Very Low-cal )", MM2.Spots.Tavern5));
            HirelingsTU.Postrequisites.Add(new QuestLocation("Talk to Sir Kill and Jed I", MM2.Spots.SirKillJedI));
            AddSideQuest(HirelingsTU, quests, new QuestBits(MM2MealsEaten.PhantomPuddingVeryLowCal, (MM2HirelingFlags.SirKill | MM2HirelingFlags.JedI)), String.Empty, String.Empty, hirelings);

            HirelingsMisc.AddLocations(new QuestLocation("Free the prisoners (Big Bootay, Cleogotcha)", MM2.Spots.Prison),
                new QuestLocation("Rescue Harry Kari and No Name", MM2.Spots.Hirelings1),
                new QuestLocation("Rescue Gertrude and Rat Fink", MM2.Spots.Hirelings2),
                new QuestLocation("Rescue Friar Fly and Dark Mage", MM2.Spots.Hirelings3),
                new QuestLocation("Defeat Bozorc (Red Duke, Dead Eye)", MM2.Spots.Bozorc),
                new QuestLocation("Defeat Amazons (Nakazawa, Sherman)", MM2.Spots.Amazons),
                new QuestLocation("Free the prisoners (Flailer, Fumbler)", MM2.Spots.Prisoners),
                new QuestLocation("Rescue Holy Moley and Slick Pick", MM2.Spots.Hirelings4),
                new QuestLocation("Defeat the Lich Lord (Mr. Wizard)", MM2.Spots.LichLord));
            AddSideQuest(HirelingsMisc, quests, new QuestBits(MM2HirelingFlags.BigBootay | MM2HirelingFlags.Cleogotcha | MM2HirelingFlags.HarryKari |
                MM2HirelingFlags.NoName | MM2HirelingFlags.Gertrude | MM2HirelingFlags.RatFink | MM2HirelingFlags.FriarFly | MM2HirelingFlags.DarkMage |
                MM2HirelingFlags.RedDuke | MM2HirelingFlags.DeadEye | MM2HirelingFlags.Nakazawa | MM2HirelingFlags.Sherman | MM2HirelingFlags.Flailer |
                MM2HirelingFlags.Fumbler | MM2HirelingFlags.HolyMoley | MM2HirelingFlags.SlickPick | MM2HirelingFlags.MrWizard), String.Empty, String.Empty, hirelings);

            SpellFingersOfDeath.Prerequisites.Add(new QuestLocation("Eat Devil's Food Brownie", MM2.Spots.Tavern2));
            SpellFingersOfDeath.Postrequisites.Add(new QuestLocation("Defeat the Grim Reapers", MM2.Spots.GrimReapers));
            AddSideQuest(SpellFingersOfDeath, quests, new QuestBits(MM2MealsEaten.DevilsFoodBrownie, MM2SpellIndex.FingersofDeath), String.Empty, String.Empty, spells);

            SpellNaturesGate.Prerequisites.Add(new QuestLocation("Eat Red Hot Wolf Nipple Chips", MM2.Spots.Tavern3));
            SpellNaturesGate.Postrequisites.Add(new QuestLocation("Visit the old druid", MM2.Spots.OldDruid));
            AddSideQuest(SpellNaturesGate, quests, new QuestBits(MM2MealsEaten.RedHotWolfNippleChips, MM2SpellIndex.NaturesGate), String.Empty, String.Empty, spells);

            if (CharClass == GenericClass.Sorcerer || CharClass == GenericClass.Archer)
            {
                AddArcaneLocations(Spells);
                AddClericLocations(Spells);
            }
            else
            {
                AddClericLocations(Spells);
                AddArcaneLocations(Spells);
            }
            Spells.AddLocations(new QuestLocation("Purchase complete spellbook (2000000 Gold)", MM2.Spots.AllSpells));
            AddSideQuest(Spells, quests, new QuestBits(
                MM2SpellIndex.AwakenSorcerer, MM2SpellIndex.EnergyBlast, MM2SpellIndex.Sleep, MM2SpellIndex.IdentifyMonster, MM2SpellIndex.LloydsBeacon,
               MM2SpellIndex.ProtectionfromMagic, MM2SpellIndex.AcidStream, MM2SpellIndex.LightningBolt, MM2SpellIndex.WizardEye, MM2SpellIndex.ColdBeam,
               MM2SpellIndex.FeebleMind, MM2SpellIndex.FireBall, MM2SpellIndex.Disrupt, MM2SpellIndex.SandStorm, MM2SpellIndex.Disintegration,
               MM2SpellIndex.FantasticFreeze, MM2SpellIndex.SuperShock, MM2SpellIndex.DancingSword, MM2SpellIndex.Duplication, MM2SpellIndex.MegaVolts,
               MM2SpellIndex.MeteorShower, MM2SpellIndex.Implosion, MM2SpellIndex.Inferno, MM2SpellIndex.StarBurst, MM2SpellIndex.EnchantItem,
               MM2SpellIndex.Apparition, MM2SpellIndex.AwakenCleric, MM2SpellIndex.PowerCure, MM2SpellIndex.Heroism, MM2SpellIndex.ProtectionFromElements,
               MM2SpellIndex.Weaken, MM2SpellIndex.ColdRay, MM2SpellIndex.LastingLight, MM2SpellIndex.WalkonWater, MM2SpellIndex.AirTransmutation,
               MM2SpellIndex.RestoreAlignment, MM2SpellIndex.HolyBonus, MM2SpellIndex.AirEncasement, MM2SpellIndex.Frenzy, MM2SpellIndex.RemoveCondition,
               MM2SpellIndex.EarthTransmutatuion, MM2SpellIndex.WaterEncasement, MM2SpellIndex.WaterTransmutation, MM2SpellIndex.EarthEncasement,
               MM2SpellIndex.FieryFlail, MM2SpellIndex.FireEncasement, MM2SpellIndex.FireTransmutation, MM2SpellIndex.MassDistortion, MM2SpellIndex.HolyWord,
               MM2SpellIndex.Resurrection, MM2SpellIndex.UncurseItem), String.Empty, String.Empty, spells);

            Skills.AddLocations(new QuestLocation("Learn Cartography (10 Gold)", MM2.Spots.Cartography),
                new QuestLocation("Learn Mountaineering (2000 Gold)", MM2.Spots.Mountaineering),
                new QuestLocation("Become a Pathfinder (2000 Gold)", MM2.Spots.Pathfinder),
                new QuestLocation("Become a Linguist (500 Gold)", MM2.Spots.Linguist),
                new QuestLocation("Become a Athlete (500 Gold)", MM2.Spots.Athlete),
                new QuestLocation("Become a Hero/Heroine (1000 Gold)", MM2.Spots.Hero),
                new QuestLocation("Become a Crusader (250 Gold)", MM2.Spots.Crusader),
                new QuestLocation("Become a Merchant (1000 Gold)", MM2.Spots.Merchant),
                new QuestLocation("Become a Navigator (750 Gold)", MM2.Spots.Navigator),
                new QuestLocation("Become a Gladiator (500 Gold)", MM2.Spots.Gladiator),
                new QuestLocation("Become a Soldier (500 Gold)", MM2.Spots.Soldier),
                new QuestLocation("Become an Arms Master (500 Gold)", MM2.Spots.ArmsMaster),
                new QuestLocation("Become a PickPocket (300 Gold)", MM2.Spots.PickPocket),
                new QuestLocation("Learn Diplomacy (500 Gold)", MM2.Spots.Diplomacy),
                new QuestLocation("Become a Gambler (200 Gold)", MM2.Spots.Gambler),
                new QuestLocation("Remove your skills (100 Gold)", MM2.Spots.Detox));
            AddSideQuest(Skills, quests, new QuestBits(MM2SecondarySkill.None), String.Empty, String.Empty, Global.RepeatableQuest);

            PermStats.AddLocations(new QuestLocation("(+1000 MaxHP) Break glass on box", MM2.Spots.BreakGlass),
                new QuestLocation("(+3 Mgt, -5 Per) Drink the ale", MM2.Spots.TradeForMight2),
                new QuestLocation("(+3 Mgt, -5 Int) Trade your intellect", MM2.Spots.TradeForMight2),
                new QuestLocation("(+3 Int, -5 End) Touch the stone", MM2.Spots.TradeForInt),
                new QuestLocation("(+3 Per, -5 Lck) Swap your luck", MM2.Spots.TradeForPers),
                new QuestLocation("(+3 End, -5 Spd) Sample the water", MM2.Spots.TradeForEnd),
                new QuestLocation("(+3 Spd, -5 Mgt) Enter the chamber", MM2.Spots.TradeForSpeed),
                new QuestLocation("(+3 Acy, -5 Spd) Pull the lever", MM2.Spots.TradeForAcy),
                new QuestLocation("(+3 Lck, -5 Per) Sample the syrup", MM2.Spots.TradeForLuck),
                new QuestLocation("(+10 Mgt) Eat the spinach", MM2.Spots.PermMight),
                new QuestLocation("(+10 Int) Read the book", MM2.Spots.PermInt),
                new QuestLocation("(+10 Per) Step into the pillar", MM2.Spots.PermPersFemale),
                new QuestLocation("(+10 Per) Step into the pillar", MM2.Spots.PermPersMale),
                new QuestLocation("(+10 End) Listen to the giant", MM2.Spots.PermEnd),
                new QuestLocation("(+10 Spd) Step onto the treadmill", MM2.Spots.PermSpeed),
                new QuestLocation("(+10 Acy) Jump in the pool", MM2.Spots.PermAcy),
                new QuestLocation("(+10 Lck) Roll in the lucky charms", MM2.Spots.PermLuck),
                new QuestLocation("(+10 Thievery) Pay Rinaldo Jr. (700 Gold)", MM2.Spots.PermThievery),
                new QuestLocation("Pay for correct MaxHP (1000000 Gold)", MM2.Spots.FullHP),
                new QuestLocation("(+1-12 MaxHP) Enter the location", MM2.Spots.NoHumansHP1),
                new QuestLocation("(+1-5 MaxHP) Enter the location", MM2.Spots.NoElvesHP),
                new QuestLocation("(+1-12 MaxHP) Enter the location", MM2.Spots.NoHumansHP2),
                new QuestLocation("(+1-12 MaxHP) Enter the location", MM2.Spots.NoDwarvesHP1),
                new QuestLocation("(+1-12 MaxHP) Enter the location", MM2.Spots.NoDwarvesHP2),
                new QuestLocation("(+1-5 MaxHP) Enter the location", MM2.Spots.NoHalfOrcHP1),
                new QuestLocation("(+1-12 MaxHP) Enter the location", MM2.Spots.NoHalfOrcHP2),
                new QuestLocation("(+25 MaxHP) Drink from the fountain", MM2.Spots.MaxHP25),
                new QuestLocation("(+10 MaxHP) Pitch a coin into the pool", MM2.Spots.MaxHP10));
            AddSideQuest(PermStats, quests, new QuestBits(MM2QuestFlags1.BrokeDragonsDominionGlass), String.Empty, String.Empty, Global.RepeatableQuest);

            TempStats.AddLocations(new QuestLocation("(Mgt: 40) Break the ice", MM2.Spots.TempMight40),
                new QuestLocation("(Mgt: 200) Eat the magic fruit", MM2.Spots.TempMight200),
                new QuestLocation("(Mgt: 1-200) Drink from the fountain", MM2.Spots.TempMightRandom),
                new QuestLocation("(Int: 65) Eat the mango", MM2.Spots.TempInt65),
                new QuestLocation("(Per: 75) Get a rub down", MM2.Spots.TempPer75),
                new QuestLocation("(+25 Spd) Drink from the fountain", MM2.Spots.TempSpeed25),
                new QuestLocation("(Spd: 200) Eat the magic fruit", MM2.Spots.TempSpeed200),
                new QuestLocation("(Acy: 40) Drink from the fountain", MM2.Spots.TempAcy40),
                new QuestLocation("(Lvl: 1-15) Drink from the fountain", MM2.Spots.TempLevRandom),
                new QuestLocation("(Stats: 100) Sip the sewage", MM2.Spots.TempStats100),
                new QuestLocation("(Stats: 200, Lvl: 50) Drink from the fountain", MM2.Spots.GreatestFountain),
                new QuestLocation("(Lvl: 25) Break the ice", MM2.Spots.TempLvl25),
                new QuestLocation("(SpellLev: 9) Drink from the fountain", MM2.Spots.TempSL9),
                new QuestLocation("(HP/MaxHP: 150) Swim in the pond", MM2.Spots.TempHP150),
                new QuestLocation("(SP: 20) Drink from the fountain", MM2.Spots.TempSP20),
                new QuestLocation("(SP: 200) Drink from the fountain", MM2.Spots.TempSP200),
                new QuestLocation("(SP: 200, SpellLev: 9) Eat the bark", MM2.Spots.AncientBark),
                new QuestLocation("(HP/MaxHP: 200) Drink the juice", MM2.Spots.JuiceBar),
                new QuestLocation("(Spd: 65) Jump in the pool", MM2.Spots.TempSpd65),
                new QuestLocation("(+1-20 Mgt) Work out", MM2.Spots.Gym),
                new QuestLocation("(+500 MaxHP) Rub on the oil", MM2.Spots.TempHP500),
                new QuestLocation("(Lev: 200, HP/MaxHP: 1) Drink the juice", MM2.Spots.GoofyJuice));

            MiscItems.AddLocations(new QuestLocation("Ivory Cameo (Mgt+15, Enter with no Knights)", MM2.Spots.IvoryCameo),
                new QuestLocation("Agate Grail (Per+15, Enter with no Paladins)", MM2.Spots.AgateGrail),
                new QuestLocation("Ruby Tiara (Acy+15, Enter with no Knights)", MM2.Spots.RubyTiara),
                new QuestLocation("Opal Pendant (Mgt+15, Enter with no Paladins)", MM2.Spots.OpalPendant),
                new QuestLocation("Onxy Effigy (Per+15, Enter with no Clerics)", MM2.Spots.OnxyEffigy),
                new QuestLocation("Sapphire Pin (Lck+15, Enter with no Robbers)", MM2.Spots.SapphirePin),
                new QuestLocation("Amethyst Box (Lck+15, Enter with no Robbers)", MM2.Spots.AmethystBox),
                new QuestLocation("Pearl Choker (Per+15, Enter with no Clerics)", MM2.Spots.PearlChoker),
                new QuestLocation("Topaz Shard (Acy+15, Enter with no Archers)", MM2.Spots.TopazShard),
                new QuestLocation("Amber Skull (Int+15, Enter with no Sorcerers)", MM2.Spots.AmberSkull),
                new QuestLocation("Quartz Skull (Int+15, Enter with no Sorcerers)", MM2.Spots.QuartzSkull),
                new QuestLocation("Sun Crown (Int+15, Enter with no Archers)", MM2.Spots.SunCrown),
                new QuestLocation("Coral Broach (Mgt+15, Enter with no Barbarians)", MM2.Spots.CoralBroach),
                new QuestLocation("Crystal Vial (Spd+15, Enter with no Ninjas)", MM2.Spots.CrystalVial),
                new QuestLocation("Lapis Scarab (Mgt+15, Enter with no Barbarians)", MM2.Spots.LapisScarab),
                new QuestLocation("Ruby Amulet (Lck+15, Enter with no Ninjas)", MM2.Spots.RubyAmulet),
                new QuestLocation("Cold Shield +5", MM2.Spots.ColdShield5),
                new QuestLocation("Giant Sling +3", MM2.Spots.GiantSling3),
                new QuestLocation("Speed Boots +3", MM2.Spots.SpeedBoots3),
                new QuestLocation("Looter Knife +5", MM2.Spots.LooterKnife5),
                new QuestLocation("Ice Sickle +3, Cold Blade +3", MM2.Spots.IceSickle3ColdBlade3),
                new QuestLocation("Dagger +15", MM2.Spots.Dagger15),
                new QuestLocation("Web Caster", MM2.Spots.WebCaster),
                new QuestLocation("2 Magic Meal", MM2.Spots.MagicMeals),
                new QuestLocation("Silver Chain Mail +2", MM2.Spots.SilverChain2),
                new QuestLocation("Trident +2", MM2.Spots.Trident2),
                new QuestLocation("Fiery Spear +2", MM2.Spots.FierySpear2),
                new QuestLocation("Great Bow +2", MM2.Spots.GreatBow2),
                new QuestLocation("Monster Tome", MM2.Spots.MonsterTome),
                new QuestLocation("Great Shield +5, Helm +5, Plate Mail +5", MM2.Spots.GreatShield5Helm5PlateMail5),
                new QuestLocation("Gold Shield +7, Gold Plate Mail +7, Gold Helm +7", MM2.Spots.GoldShieldPlateHelm7),
                new QuestLocation("Broad Sword +5, Great Bow +5, Flamberge +5", MM2.Spots.Broadsword5GreatBow5Flamberge5),
                new QuestLocation("Titan's Pike +7, Photon Blade +7, Ancient Bow +7", MM2.Spots.Titan7Photon7Ancient7),
                new QuestLocation("Fire Shield +4", MM2.Spots.FireShield4),
                new QuestLocation("Flaming Sword +10", MM2.Spots.FlamingSword10),
                new QuestLocation("Fiery Spear +4", MM2.Spots.FierySpear4),
                new QuestLocation("Fire Glaive +4", MM2.Spots.FireGlaive4),
                new QuestLocation("Instant Keep", MM2.Spots.InstantKeep),
                new QuestLocation("Ice Sickle +2", MM2.Spots.IceSickle2),
                new QuestLocation("Emerald Ring", MM2.Spots.EmeraldRing),
                new QuestLocation("Magic Herb", MM2.Spots.MagicHerb),
                new QuestLocation("Mace", MM2.Spots.Mace),
                new QuestLocation("Helm", MM2.Spots.Helm),
                new QuestLocation("Torch", MM2.Spots.Torch),
                new QuestLocation("Large Shield", MM2.Spots.LargeShield),
                new QuestLocation("Lava Grenade", MM2.Spots.LavaGrenade),
                new QuestLocation("Dog Whistle", MM2.Spots.DogWhistle));
            AddSideQuest(MiscItems, quests, null, String.Empty, String.Empty, Global.RepeatableQuest);

            DefeatDawn.Prerequisites.Add(new QuestLocation("Dismiss any hirelings", MM2.Spots.MurrayNoHirelings));
            DefeatDawn.Prerequisites.Add(new QuestLocation("Raise character 1's Might to at least 40", MM2.Spots.PermMight));
            DefeatDawn.Prerequisites.Add(new QuestLocation("Give a character the Hero/Heroine secondary skill", MM2.Spots.Hero));
            DefeatDawn.AddLocations(new QuestLocation("See Murray", MM2.Spots.Murray),
                new QuestLocation("Defeat Dawn", MM2.Spots.Dawn));
            DefeatDawn.Postrequisites.Add(new QuestLocation("Return to Murray", MM2.Spots.Murray));
            TempStats.PreQuest.Add(AddSideQuest(DefeatDawn, quests, 
                new QuestBits(MM2QuestFlags2.AcceptedMurraysQuest, MM2QuestFlags1.DefeatedDawn, MM2QuestFlags2.FinishedMurraysQuest), "Murray", "100000 Gold"));
            AddSideQuest(TempStats, quests, null, String.Empty, String.Empty, Global.RepeatableQuest);

            quests.Sort(CompareMM2Quests);
            return quests.ToArray();
        }

        public int CompareMM2Quests(MM2Quest quest1, MM2Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        private void AddTripleCrown(QuestTotals totals, QuestStatus qs, MM2QuestStates.TripleCrown state, bool bBlack = false, bool bRepeat = false)
        {
            bool bWonAll = state.HasFlag(MM2QuestStates.TripleCrown.WonAllBattles);
            bool bTicket = bWonAll || state.HasFlag(MM2QuestStates.TripleCrown.BoughtTicket);

            if (bBlack)
                qs.AddObj(bTicket); // There are two places to get a black ticket
            qs.AddObj(bTicket,
                state.HasFlag(MM2QuestStates.TripleCrown.WonArena),
                state.HasFlag(MM2QuestStates.TripleCrown.WonColosseum),
                state.HasFlag(MM2QuestStates.TripleCrown.WonMonsterBowl),
                state.HasFlag(MM2QuestStates.TripleCrown.FoundKey));
            qs.AddPost(bRepeat ? false : state.HasFlag(MM2QuestStates.TripleCrown.FreedBishop));
            AddQuest(totals, qs);
        }

        private MM2QuestStates.TripleCrown GetTripleCrown(MM2PartyInfo party, MM2Character mm2Char, MM2ItemIndex ticket, MM2ItemIndex key, 
            MM2ArenaFlags arena, MM2ArenaFlags colosseum, MM2ArenaFlags bowl, bool bBishop = false)
        {
            MM2QuestStates.TripleCrown state = MM2QuestStates.TripleCrown.NotStarted;
            if (bBishop)
                state = MM2QuestStates.TripleCrown.FreedBishop;
            if (party.CurrentPartyHasItem(ticket))
                state |= MM2QuestStates.TripleCrown.BoughtTicket;
            if (party.CurrentPartyHasItem(key))
                state |= MM2QuestStates.TripleCrown.FoundKey;
            if (mm2Char.Arena.HasFlag(arena))
                state |= MM2QuestStates.TripleCrown.WonArena;
            if (mm2Char.Arena.HasFlag(colosseum))
                state |= MM2QuestStates.TripleCrown.WonColosseum;
            if (mm2Char.Arena.HasFlag(bowl))
                state |= MM2QuestStates.TripleCrown.WonMonsterBowl;
            return state;
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress = -1)
        {
            MM2PartyInfo party = data.Party as MM2PartyInfo;
            MM2GameInfo info = data.Info as MM2GameInfo;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            MM2Character mm2Char = MM2Character.Create(party.Bytes, iOverrideCharAddress * MM2Character.SizeInBytes, false);
            bool[,] spells = mm2Char.KnownSpells.Spells;

            CharClass = mm2Char.BasicClass;
            bool bCleric = mm2Char.Class == MM2Class.Cleric;
            bool bSorcerer = mm2Char.Class == MM2Class.Sorcerer;
            bool bArcher = mm2Char.Class == MM2Class.Archer;
            bool bPaladin = mm2Char.Class == MM2Class.Paladin;

            CharName = mm2Char.CharName;
            CharAddress = iOverrideCharAddress;

            QuestTotals totals = new QuestTotals(0, 0);

            BlackTripleCrown = GetTripleCrown(party, mm2Char, MM2ItemIndex.BlackTicket, MM2ItemIndex.BlackKey,
                MM2ArenaFlags.BlackArena, MM2ArenaFlags.BlackColosseum, MM2ArenaFlags.BlackMonsterBowl,
                mm2Char.Quests2.HasFlag(MM2QuestFlags2.FreedBlackBishop));

            GreenTripleCrown = GetTripleCrown(party, mm2Char, MM2ItemIndex.GreenTicket, MM2ItemIndex.GreenKey,
                MM2ArenaFlags.GreenArena, MM2ArenaFlags.GreenColosseum, MM2ArenaFlags.GreenMonsterBowl);

            YellowTripleCrown = GetTripleCrown(party, mm2Char, MM2ItemIndex.YellowTicket, MM2ItemIndex.YellowKey,
                MM2ArenaFlags.YellowArena, MM2ArenaFlags.YellowColosseum, MM2ArenaFlags.YellowMonsterBowl);

            RedTripleCrown = GetTripleCrown(party, mm2Char, MM2ItemIndex.RedTicket, MM2ItemIndex.RedKey,
                MM2ArenaFlags.RedArena, MM2ArenaFlags.RedColosseum, MM2ArenaFlags.RedMonsterBowl);

            AddTripleCrown(totals, BlackTriple, BlackTripleCrown, true);
            BlackTriple.MarkAllWhenComplete = true;
            if (BlackTriple.Completed)
                AddTripleCrown(totals, RepeatBlackTriple, BlackTripleCrown, true, true);
            AddTripleCrown(totals, GreenTriple, GreenTripleCrown);
            AddTripleCrown(totals, YellowTriple, YellowTripleCrown);
            AddTripleCrown(totals, RedTriple, RedTripleCrown);

            bool bFinishedMain = mm2Char.Advancement.HasFlag(MM2AdvancementFlags.AccomplishedMainGoal);
            bool bYekop = bFinishedMain || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererFreedEvil);
            bool bYbmug = bFinishedMain || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererFreedGood);

            Promotion.Obj.Add(GetSingle(true, bFinishedMain, mm2Char.Class == MM2Class.Knight, "You are not a Knight"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, mm2Char.Class == MM2Class.Paladin, "You are not a Paladin"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, bArcher, "You are not an Archer"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain || info.Gwyndon, bCleric, "You are not a Cleric"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain || party.CurrentPartyHasItem(MM2ItemIndex.CoraksSoul), bCleric, "You are not a Cleric"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain || party.CurrentPartyHasItem(MM2ItemIndex.Admit8Pass), bCleric, "You are not a Cleric"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, bCleric, "You are not a Cleric"));
            Promotion.Obj.Add(GetSingle(true, bYekop || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererCodeEvilLeft), bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bYekop || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererCodeEvilRight), bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bYbmug || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererCodeGoodLeft), bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bYbmug || mm2Char.Advancement.HasFlag(MM2AdvancementFlags.SorcererCodeGoodRight), bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bYekop, bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bYbmug, bSorcerer, "You are not a Sorcerer"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, mm2Char.Class == MM2Class.Robber, "You are not a Robber"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, mm2Char.Class == MM2Class.Ninja, "You are not a Ninja"));
            Promotion.Obj.Add(GetSingle(true, bFinishedMain, mm2Char.Class == MM2Class.Barbarian, "You are not a Barbarian"));
            Promotion.AddPost(mm2Char.Guilds.HasFlag(MM2GuildFlags.Advancement));
            Promotion.MarkAllWhenComplete = true;
            AddQuest(totals, Promotion);

            bool bFinishNordon = mm2Char.Quests1.HasFlag(MM2QuestFlags1.FinishedNordonsQuest);
            foreach (QuestStatus qs in new QuestStatus[] { Nordon, RepeatNordon })
            {
                qs.AddPre(bFinishNordon || mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedNordonsQuest));
                qs.AddObj(bFinishNordon || party.CurrentPartyHasItem(MM2ItemIndex.GoldGoblet));
            }
            Nordon.AddObj(bFinishNordon);
            Nordon.AddPost(mm2Char.Quests1.HasFlag(MM2QuestFlags1.FinishedNordonnasQuest));
            Nordon.MarkAllWhenComplete = true;
            RepeatNordon.AddPost(bFinishNordon);
            AddQuest(totals, Nordon);

            bool bDrog = mm2Char.Quests1.HasFlag(MM2QuestFlags1.RescuedDrogAndSirHyron);
            Nordonna.AddPre(bDrog || mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedNordonnasQuest));
            Nordonna.AddObj(bDrog);
            Nordonna.AddPost(mm2Char.Quests1.HasFlag(MM2QuestFlags1.FinishedNordonnasQuest));
            Nordonna.MarkAllWhenComplete = true;
            AddQuest(totals, Nordonna);

            if (Nordonna.Completed)
                AddQuest(totals, RepeatNordon);

            Epicurean.AddObj(mm2Char.Meals.HasFlag(MM2MealsEaten.HorrorsDoeuvres),
                mm2Char.Meals.HasFlag(MM2MealsEaten.SoupDeGhoulWGarlicToast),
                mm2Char.Meals.HasFlag(MM2MealsEaten.DragonSteakTartar),
                mm2Char.Meals.HasFlag(MM2MealsEaten.LightlySaltedTongueOfToad),
                mm2Char.Meals.HasFlag(MM2MealsEaten.PureeOfGnome),
                mm2Char.Meals.HasFlag(MM2MealsEaten.DevilsFoodBrownie),
                mm2Char.Meals.HasFlag(MM2MealsEaten.SizzlingSwineSoup),
                mm2Char.Meals.HasFlag(MM2MealsEaten.RedHotWolfNippleChips),
                mm2Char.Meals.HasFlag(MM2MealsEaten.RoastLegOfWyvern),
                mm2Char.Meals.HasFlag(MM2MealsEaten.PickledPixieBrains),
                mm2Char.Meals.HasFlag(MM2MealsEaten.DeepFriedTrollLiver),
                mm2Char.Meals.HasFlag(MM2MealsEaten.CreamOfKoboldSoup),
                mm2Char.Meals.HasFlag(MM2MealsEaten.GourmetDinnerBWyrmChopSuey),
                mm2Char.Meals.HasFlag(MM2MealsEaten.RoastPeasantUnderGlass),
                mm2Char.Meals.HasFlag(MM2MealsEaten.PhantomPuddingVeryLowCal));
            Epicurean.AddPost(false);     // Repeatable; can't really finish
            AddQuest(totals, Epicurean);

            bool bHoardallLords = (mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest)
                    && mm2Char.QuestObject == 0
                    && !mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerQuest));

            ChivalrySwords.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            ChivalrySwords.AddPre(bHoardallLords);
            ChivalrySwords.AddObj(party.CurrentPartyHasItem(MM2ItemIndex.HonorSword),
                party.CurrentPartyHasItem(MM2ItemIndex.NobleSword),
                party.CurrentPartyHasItem(MM2ItemIndex.ValorSword));
            ChivalrySwords.AddPost(mm2Char.Quests1.HasFlag(MM2QuestFlags1.FinishedHoardallLordsQuest));
            ChivalrySwords.MarkAllWhenComplete = true;
            AddQuest(totals, ChivalrySwords);

            bool bSlayer = (mm2Char.QuestObject != 0 && mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerQuest));
            bool bSlayerDefeatedEnemy = (mm2Char.QuestObject != 0 && mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedSlayersMonster));

            DefeatEnemy.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            DefeatEnemy.AddPre(bSlayer);
            DefeatEnemy.AddObj(bSlayerDefeatedEnemy);
            DefeatEnemy.AddPost(false);  // repeatable; never really completed
            AddQuest(totals, DefeatEnemy);

            bool bHoardall = (!mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest) && mm2Char.QuestObject != 0 && !(mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerQuest)));
            bool bFoundObject = bHoardall && party.CurrentPartyHasItem((MM2ItemIndex)QuestObject);

            FindItem.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            FindItem.AddPre(bHoardall);
            FindItem.AddObj(bFoundObject);
            FindItem.AddPost(false);  // repeatable; never really completed
            AddQuest(totals, FindItem);

            bool bSlayerLords = (mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerOrHoardallQuest)
                    && mm2Char.QuestObject == 0
                    && mm2Char.Quests1.HasFlag(MM2QuestFlags1.AcceptedSlayerQuest));

            RoyalEnvoys.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            RoyalEnvoys.AddPre(bSlayerLords);
            RoyalEnvoys.AddObj(mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedQueenBeetle),
                mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedSerpentKing),
                mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedDragonLord));
            RoyalEnvoys.AddPost(mm2Char.Quests1.HasFlag(MM2QuestFlags1.FinishedSlayerLordsQuest));
            RoyalEnvoys.MarkAllWhenComplete = true;
            AddQuest(totals, RoyalEnvoys);

            PegasusName.AddPre(mm2Char.Guilds.HasFlag(MM2GuildFlags.VisitedPegasus));
            PegasusName.AddPost(party.VisitedPegasus);
            AddQuest(totals, PegasusName);

            MM2GuildFlags guilds = mm2Char.Guilds & MM2GuildFlags.JoinedAllGuilds;

            MageGuilds.AddObj(guilds.HasFlag(MM2GuildFlags.MiddlegateMageGuild),
                guilds.HasFlag(MM2GuildFlags.AtlantiumMageGuild),
                guilds.HasFlag(MM2GuildFlags.TundaranMageGuild),
                guilds.HasFlag(MM2GuildFlags.VulcanianMageGuild),
                guilds.HasFlag(MM2GuildFlags.SandsobarMageGuild));
            AddQuest(totals, MageGuilds);

            bool bUsedFerry = mm2Char.Quests1.HasFlag(MM2QuestFlags1.UsedMurraysFerry);
            bool bOnFerry = mm2Char.Quests1.HasFlag(MM2QuestFlags1.OnMurraysFerry);
            bool bFerryTicket = mm2Char.Quests1.HasFlag(MM2QuestFlags1.BoughtMurraysTicket);
            RejuvenateMurrays.AddPre(bFerryTicket || bOnFerry || bUsedFerry);
            RejuvenateMurrays.AddObj(bOnFerry || bUsedFerry, bUsedFerry);
            RejuvenateMurrays.AddPost(false); // Repeatable, never really completed
            AddQuest(totals, RejuvenateMurrays);

            PermStats.AddObj(mm2Char.Quests1.HasFlag(MM2QuestFlags1.BrokeDragonsDominionGlass),
                mm2Char.Personality.Permanent < 5 || mm2Char.Might.Permanent >= 255,
                mm2Char.Intellect.Permanent < 5 || mm2Char.Might.Permanent >= 255,
                mm2Char.Endurance.Permanent < 5 || mm2Char.Intellect.Permanent >= 255,
                mm2Char.Luck.Permanent < 5 || mm2Char.Personality.Permanent >= 255,
                mm2Char.Speed.Permanent < 5 || mm2Char.Endurance.Permanent >= 255,
                mm2Char.Might.Permanent < 5 || mm2Char.Speed.Permanent >= 255,
                mm2Char.Speed.Permanent < 5 || mm2Char.Accuracy.Permanent >= 255,
                mm2Char.Personality.Permanent < 5 || mm2Char.Luck.Permanent >= 255,
                mm2Char.Might.Permanent >= 50,
                mm2Char.Intellect.Permanent >= 50);
            PermStats.Obj.Add(GetSingle(true, mm2Char.Personality.Permanent >= 50, mm2Char.Sex == MM2Sex.Female, "You are not female"));
            PermStats.Obj.Add(GetSingle(true, mm2Char.Personality.Permanent >= 50, mm2Char.Sex == MM2Sex.Male, "You are not male"));
            PermStats.AddObj(mm2Char.Endurance.Permanent >= 50,
                mm2Char.Speed.Permanent >= 50,
                mm2Char.Accuracy.Permanent >= 50,
                mm2Char.Luck.Permanent >= 50,
                mm2Char.Thievery > 60,
                false,
                info.BenefitsUsed2,
                info.BenefitsUsed1,
                info.BenefitsUsed2,
                info.BenefitsUsed1,
                info.BenefitsUsed2,
                info.BenefitsUsed1,
                info.BenefitsUsed2,
                false,
                mm2Char.HitPoints.Maximum >= 200);
            AddQuest(totals, PermStats);

            TempStats.AddObj(mm2Char.Might.Temporary == 40,
                mm2Char.Might.Temporary == 200,
                false,
                mm2Char.Intellect.Temporary == 65,
                mm2Char.Personality.Temporary == 75,
                false,
                mm2Char.Speed.Temporary == 200,
                mm2Char.Accuracy.Temporary == 40,
                false,
                mm2Char.AllStatsTemp(100),
                mm2Char.AllStatsTemp(200) && mm2Char.Level.Temporary == 50,
                mm2Char.Level.Temporary == 25,
                mm2Char.SpellLevel.Temporary == 9,
                mm2Char.HitPoints.TemporaryMaximum == 150 && mm2Char.HitPoints.Current == 150,
                mm2Char.SpellPoints.CurrentSP == 20,
                mm2Char.SpellPoints.CurrentSP == 200,
                mm2Char.SpellPoints.CurrentSP == 200 && mm2Char.SpellLevel.Temporary == 9,
                mm2Char.HitPoints.TemporaryMaximum == 200 && mm2Char.HitPoints.Current == 200,
                mm2Char.Speed.Temporary == 65,
                false,
                false,
                mm2Char.Level.Temporary == 200 && mm2Char.HitPoints.TemporaryMaximum == 1 && mm2Char.HitPoints.Current == 1);
            AddQuest(totals, TempStats);

            bool bDefeatedMegaDragon = mm2Char.Quests2.HasFlag(MM2QuestFlags2.HelpedKalohn);
            bool bHasOrb = bDefeatedMegaDragon || party.CurrentPartyHasItem(MM2ItemIndex.ElementOrb);
            bool bHasWaterTalon = bDefeatedMegaDragon || party.CurrentPartyHasItem(MM2ItemIndex.WaterTalon);
            bool bHasAirTalon = bDefeatedMegaDragon || party.CurrentPartyHasItem(MM2ItemIndex.AirTalon);
            bool bHasFireTalon = bDefeatedMegaDragon || party.CurrentPartyHasItem(MM2ItemIndex.FireTalon);
            bool bHasEarthTalon = bDefeatedMegaDragon || party.CurrentPartyHasItem(MM2ItemIndex.EarthTalon);

            HelpKalohnMega.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            HelpKalohnMega.AddPre(mm2Char.Quests2.HasFlag(MM2QuestFlags2.AcceptedLamandasQuest1));
            HelpKalohnMega.AddObj(bHasOrb || party.CurrentPartyHasItem(MM2ItemIndex.J26Fluxer),
                bHasOrb || party.CurrentPartyHasItem(MM2ItemIndex.M27Radicon),
                bHasOrb || party.CurrentPartyHasItem(MM2ItemIndex.A1Todilor),
                bHasOrb || party.CurrentPartyHasItem(MM2ItemIndex.N19Capitor),
                bHasOrb,
                bHasWaterTalon || party.CurrentPartyHasItem(MM2ItemIndex.WaterDisc),
                bHasWaterTalon,
                bHasAirTalon || party.CurrentPartyHasItem(MM2ItemIndex.AirDisc),
                bHasAirTalon,
                bHasFireTalon || party.CurrentPartyHasItem(MM2ItemIndex.FireDisc),
                bHasFireTalon,
                bHasEarthTalon || party.CurrentPartyHasItem(MM2ItemIndex.EarthDisc),
                bHasEarthTalon,
                bDefeatedMegaDragon);
            HelpKalohnMega.AddPost(mm2Char.Quests2.HasFlag(MM2QuestFlags2.FinishedLamandasQuest));
            HelpKalohnMega.MarkAllWhenComplete = true;
            AddQuest(totals, HelpKalohnMega);

            bool bSolvedRiddle = mm2Char.Quests2.HasFlag(MM2QuestFlags2.FinishedKalohnsQuest);
            SquareLake.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            SquareLake.AddObj(bSolvedRiddle || mm2Char.Quests2.HasFlag(MM2QuestFlags2.FinishedLamandasQuest),
                bSolvedRiddle);
            SquareLake.MarkAllWhenComplete = true;
            AddQuest(totals, SquareLake);

            DefeatHorvath.AddPre(mm2Char.Quests2.HasFlag(MM2QuestFlags2.AcceptedElderDruidsQuest));
            DefeatHorvath.AddObj(mm2Char.Quests2.HasFlag(MM2QuestFlags2.DefeatedHorvath));
            DefeatHorvath.AddPost(mm2Char.KnowsSpell(MM2.Spells[(int)MM2SpellIndex.DivineIntervention]));
            if (!bCleric)
                DefeatHorvath.Main = QuestStatus.Basic.InvalidClass;
            else
                AddQuest(totals, DefeatHorvath);

            bool bShermanInParty = party.Addresses.Any(b => b == 40);
            ShermanTown = info.HirelingTowns[16];

            bool bRescuedSherman = party.Hirelings.HasFlag(MM2HirelingFlags.Sherman) || bShermanInParty;

            RescueSherman.AddPre(bRescuedSherman || party.CurrentPartyHasSkill(MM2SecondarySkill.Crusader));
            RescueSherman.AddPre(bRescuedSherman || mm2Char.Quests2.HasFlag(MM2QuestFlags2.AcceptedPeabodysQuest));
            RescueSherman.AddObj(bRescuedSherman, bShermanInParty);
            RescueSherman.AddPost(mm2Char.Quests2.HasFlag(MM2QuestFlags2.FinishedPeabodysQuest));
            RescueSherman.MarkAllWhenComplete = true;
            AddQuest(totals, RescueSherman);

            PhaserLoincloth.AddPre(mm2Char.Quests2.HasFlag(MM2QuestFlags2.AcceptedHaartsQuest));
            PhaserLoincloth.AddObj(mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedSpazTwit),
                party.CurrentPartyHasItem(MM2ItemIndex.Loincloth),
                party.CurrentPartyHasItem(MM2ItemIndex.Phaser));
            PhaserLoincloth.AddPost(false);  // Repeatable, not really completed
            AddQuest(totals, PhaserLoincloth);

            bool bDefeatedDawn = mm2Char.Quests1.HasFlag(MM2QuestFlags1.DefeatedDawn);
            DefeatDawn.AddPre(!party.AnyCharIsHireling);
            DefeatDawn.AddPre(party.StatAtLeast(party.Addresses[0], PrimaryStat.Might, 40));
            DefeatDawn.AddPre(party.CurrentPartyHasSkill(MM2SecondarySkill.HeroHeroine));
            DefeatDawn.AddObj(mm2Char.Quests2.HasFlag(MM2QuestFlags2.AcceptedMurraysQuest));
            DefeatDawn.AddObj(bDefeatedDawn);
            DefeatDawn.AddPost(mm2Char.Quests2.HasFlag(MM2QuestFlags2.FinishedMurraysQuest));
            AddQuest(totals, DefeatDawn);

            MiscItems.AddObj(47, Goal(false));
            AddQuest(totals, MiscItems);

            bool bMadMan = mm2Char.Quests1.HasFlag(MM2QuestFlags1.UsedCupieDoll);
            WinCircus.AddPre(bMadMan || party.CurrentPartyHasItem(MM2ItemIndex.CupieDoll));
            WinCircus.AddObj(bMadMan,
                mm2Char.Quests1.HasFlag(MM2QuestFlags1.UsedInnerLimitsPool));
            WinCircus.AddPost(false); // Repeatable, not really completed
            AddQuest(totals, WinCircus);

            MarksKeys.AddPre(party.CurrentPartyHasItem(MM2ItemIndex.MarksKeys));
            MarksKeys.AddPost(false);
            AddQuest(totals, MarksKeys);

            bool bHasFarthing = party.CurrentPartyHasItem(MM2ItemIndex.FeFarthing);
            CastleKey.AddObj(bHasFarthing || party.Donations.HasFlag(MM2DonationFlags.Middlegate),
                bHasFarthing || party.Donations.HasFlag(MM2DonationFlags.Atlantium),
                bHasFarthing || party.Donations.HasFlag(MM2DonationFlags.Tundara),
                bHasFarthing || party.Donations.HasFlag(MM2DonationFlags.Vulcania),
                bHasFarthing || party.Donations.HasFlag(MM2DonationFlags.Sandsobar),
                bHasFarthing);
            CastleKey.AddPost(party.CurrentPartyHasItem(MM2ItemIndex.CastleKey));
            CastleKey.MarkAllWhenComplete = true;
            AddQuest(totals, CastleKey);

            HirelingC.AddPre(mm2Char.Meals.HasFlag(MM2MealsEaten.GourmetDinnerBWyrmChopSuey));
            HirelingC.AddPost(party.Hirelings.HasFlag(MM2HirelingFlags.HKPhooey));
            HirelingC.MarkAllWhenComplete = true;
            AddQuest(totals, HirelingC);

            HirelingsDE.AddPre(mm2Char.Meals.HasFlag(MM2MealsEaten.DeepFriedTrollLiver));
            HirelingsDE.AddPost(party.Hirelings.HasFlag(MM2HirelingFlags.ThundR | MM2HirelingFlags.Aeriel));
            HirelingsDE.MarkAllWhenComplete = true;
            AddQuest(totals, HirelingsDE);

            HirelingsTU.AddPre(mm2Char.Meals.HasFlag(MM2MealsEaten.PhantomPuddingVeryLowCal));
            HirelingsTU.AddPost(party.Hirelings.HasFlag(MM2HirelingFlags.SirKill | MM2HirelingFlags.JedI));
            HirelingsTU.MarkAllWhenComplete = true;
            AddQuest(totals, HirelingsTU);

            HirelingsMisc.AddObj(party.Hirelings.HasFlag(MM2HirelingFlags.BigBootay | MM2HirelingFlags.Cleogotcha),
                party.Hirelings.HasFlag(MM2HirelingFlags.HarryKari | MM2HirelingFlags.NoName),
                party.Hirelings.HasFlag(MM2HirelingFlags.Gertrude | MM2HirelingFlags.RatFink),
                party.Hirelings.HasFlag(MM2HirelingFlags.FriarFly | MM2HirelingFlags.DarkMage),
                party.Hirelings.HasFlag(MM2HirelingFlags.RedDuke | MM2HirelingFlags.DeadEye),
                party.Hirelings.HasFlag(MM2HirelingFlags.Nakazawa | MM2HirelingFlags.Sherman),
                party.Hirelings.HasFlag(MM2HirelingFlags.Flailer | MM2HirelingFlags.Fumbler),
                party.Hirelings.HasFlag(MM2HirelingFlags.HolyMoley | MM2HirelingFlags.SlickPick),
                party.Hirelings.HasFlag(MM2HirelingFlags.MrWizard));
            AddQuest(totals, HirelingsMisc);

            SpellFingersOfDeath.AddPre(mm2Char.Meals.HasFlag(MM2MealsEaten.DevilsFoodBrownie));
            SpellFingersOfDeath.AddPost(mm2Char.KnowsSpell(SpellType.Sorcerer, 5, 2));
            SpellFingersOfDeath.MarkAllWhenComplete = true;
            if (bSorcerer || bArcher)
                AddQuest(totals, SpellFingersOfDeath);
            else
                SpellFingersOfDeath.Main = QuestStatus.Basic.InvalidClass;

            SpellNaturesGate.AddPre(mm2Char.Meals.HasFlag(MM2MealsEaten.RedHotWolfNippleChips));
            SpellNaturesGate.AddPost(mm2Char.KnowsSpell(SpellType.Cleric, 2, 3));
            SpellNaturesGate.MarkAllWhenComplete = true;
            if (bCleric || bPaladin)
                AddQuest(totals, SpellNaturesGate);
            else
                SpellNaturesGate.Main = QuestStatus.Basic.InvalidClass;

            bool bKnowsAllSpells = mm2Char.KnownSpells.Total >= 48;

            if (bArcher || bSorcerer)
            {
                AddArcane(Spells, spells, bSorcerer, bArcher);
                AddCleric(Spells, spells, bCleric, bPaladin);
            }
            else
            {
                AddCleric(Spells, spells, bCleric, bPaladin);
                AddArcane(Spells, spells, bSorcerer, bArcher);
            }

            Spells.Obj.Add(GetSingle(true, bKnowsAllSpells, mm2Char.IsCaster, "You are not a spellcaster"));
            if (mm2Char.IsCaster)
                AddQuest(totals, Spells);
            else
                Spells.Main = QuestStatus.Basic.InvalidClass;

            Skills.AddObj(mm2Char.HasSkill(MM2SecondarySkill.Cartographer),
                mm2Char.HasSkill(MM2SecondarySkill.Mountaineer),
                mm2Char.HasSkill(MM2SecondarySkill.Pathfinder),
                mm2Char.HasSkill(MM2SecondarySkill.Linguist),
                mm2Char.HasSkill(MM2SecondarySkill.Athlete),
                mm2Char.HasSkill(MM2SecondarySkill.HeroHeroine),
                mm2Char.HasSkill(MM2SecondarySkill.Crusader),
                mm2Char.HasSkill(MM2SecondarySkill.Merchant),
                mm2Char.HasSkill(MM2SecondarySkill.Navigator),
                mm2Char.HasSkill(MM2SecondarySkill.Gladiator),
                mm2Char.HasSkill(MM2SecondarySkill.Soldier),
                mm2Char.HasSkill(MM2SecondarySkill.ArmsMaster),
                mm2Char.HasSkill(MM2SecondarySkill.PickPocket),
                mm2Char.HasSkill(MM2SecondarySkill.Diplomat),
                mm2Char.HasSkill(MM2SecondarySkill.Gambler),
                mm2Char.Skill1 == MM2SecondarySkill.None && mm2Char.Skill2 == MM2SecondarySkill.None);
            AddQuest(totals, Skills);

            CompletedQuests = totals.Completed;
            TotalQuests = totals.All;
        }

        private void AddArcane(QuestStatus qsSpells, bool[,] spells, bool bSorcerer, bool bArcher)
        {
            Spells.Obj.Add(GetSingle(true, spells[1, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[1, 3], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[1, 7], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[2, 3], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[2, 6], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[2, 7], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[3, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[3, 4], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[3, 6], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[4, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[4, 2], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[4, 3], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[5, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[5, 3], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[6, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[6, 3], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[6, 5], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[7, 1], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[7, 2], bSorcerer || bArcher, "You are not a Sorcerer or Archer"));
            Spells.Obj.Add(GetSingle(true, spells[8, 2], bSorcerer, "You are not a Sorcerer"));
            Spells.Obj.Add(GetSingle(true, spells[8, 3], bSorcerer, "You are not a Sorcerer"));
            Spells.Obj.Add(GetSingle(true, spells[9, 1], bSorcerer, "You are not a Sorcerer"));
            Spells.Obj.Add(GetSingle(true, spells[9, 2], bSorcerer, "You are not a Sorcerer"));
            Spells.Obj.Add(GetSingle(true, spells[9, 3], bSorcerer, "You are not a Sorcerer"));
            Spells.Obj.Add(GetSingle(true, spells[9, 4], bSorcerer, "You are not a Sorcerer"));
        }

        private void AddCleric(QuestStatus qsSpells, bool[,] spells, bool bCleric, bool bPaladin)
        {
            Spells.Obj.Add(GetSingle(true, spells[1, 1], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[1, 2], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[1, 6], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[2, 2], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[2, 5], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[2, 7], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[3, 1], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[3, 5], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[3, 6], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[4, 2], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[4, 4], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[4, 6], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[5, 1], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[5, 3], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[5, 5], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[6, 1], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[6, 4], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[6, 5], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[7, 1], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[7, 2], bCleric || bPaladin, "You are not a Cleric or Paladin"));
            Spells.Obj.Add(GetSingle(true, spells[8, 1], bCleric, "You are not a Cleric"));
            Spells.Obj.Add(GetSingle(true, spells[8, 2], bCleric, "You are not a Cleric"));
            Spells.Obj.Add(GetSingle(true, spells[8, 3], bCleric, "You are not a Cleric"));
            Spells.Obj.Add(GetSingle(true, spells[9, 2], bCleric, "You are not a Cleric"));
            Spells.Obj.Add(GetSingle(true, spells[9, 3], bCleric, "You are not a Cleric"));
            Spells.Obj.Add(GetSingle(true, spells[9, 4], bCleric, "You are not a Cleric"));
        }

        private void AddArcaneLocations(QuestStatus qsSpells)
        {
            qsSpells.AddLocations(new QuestLocation("Purchase S1-1, Awaken (10 Gold)", MM2.Spots.Guild1),
                new QuestLocation("Purchase S1-3, Energy Blast (1000 Gold)", MM2.Spots.Guild1),
                new QuestLocation("Purchase S1-7, Sleep (50 Gold)", MM2.Spots.Guild1),
                new QuestLocation("Purchase S2-3, Identify Monster (100 Gold)", MM2.Spots.Guild1),
                new QuestLocation("Learn S2-6, Lloyd's Beacon", MM2Map.C2CoraksCave, 7, 11),
                new QuestLocation("Purchase S2-7, Protection from Magic (400 Gold)", MM2.Spots.Guild5),
                new QuestLocation("Purchase S3-1, Acid Stream (200 Gold)", MM2.Spots.Guild5),
                new QuestLocation("Purchase S3-4, Lightning Bolt (1000 Gold)", MM2.Spots.Guild5),
                new QuestLocation("Purchase S3-6, Wizard Eye (100 Gold)", MM2.Spots.BeggarsGift),
                new QuestLocation("Purchase S4-1, Cold Beam (500 Gold)", MM2.Spots.Guild5),
                new QuestLocation("Purchase S4-2, Feeble Mind (600 Gold)", MM2.Spots.Guild3),
                new QuestLocation("Purchase S4-3, Fire Ball (2000 Gold)", MM2.Spots.Guild3),
                new QuestLocation("Purchase S5-1, Disrupt (3000 Gold)", MM2.Spots.Guild3),
                new QuestLocation("Purchase S5-3, Sand Storm (3000 Gold)", MM2.Spots.Guild3),
                new QuestLocation("Purchase S6-1, Disintegration (5000 Gold)", MM2.Spots.Guild4),
                new QuestLocation("Purchase S6-3, Fantastic Freeze (5000 Gold)", MM2.Spots.Guild4),
                new QuestLocation("Purchase S6-5, Super Shock (5000 Gold)", MM2.Spots.Guild4),
                new QuestLocation("Defeat the Mist Warrior (S7-1, Dancing Sword)", MM2.Spots.MistWarrior),
                new QuestLocation("Purchase S7-2, Duplication (25000 Gold)", MM2.Spots.Guild4),
                new QuestLocation("Purchase S8-2, Mega Volts (50000 Gold)", MM2.Spots.Guild2),
                new QuestLocation("Purchase S8-3, Meteor Shower (50000 Gold)", MM2.Spots.Guild2),
                new QuestLocation("Purchase S9-1, Implosion (100000 Gold)", MM2.Spots.Guild2),
                new QuestLocation("Purchase S9-2, Inferno (100000 Gold)", MM2.Spots.Guild2),
                new QuestLocation("Cast Nature's Gate on day 93 (S9-3, Star Burst)", MM2.Spots.Starburst),
                new QuestLocation("Purchase S9-4, Enchant Item (10 Years)", MM2.Spots.Gemmaker));
        }

        private void AddClericLocations(QuestStatus qsSpells)
        {
            qsSpells.AddLocations(new QuestLocation("Purchase C1-1, Apparition (10 Gold)", MM2.Spots.Temple1),
                new QuestLocation("Purchase C1-2, Awaken (10 Gold)", MM2.Spots.Temple1),
                new QuestLocation("Purchase C1-6, Power Cure (1000 Gold)", MM2.Spots.Temple1),
                new QuestLocation("Purchase C2-2, Heroism (250 Gold)", MM2.Spots.Temple5),
                new QuestLocation("Purchase C2-5, Protection From Elements (300 Gold)", MM2.Spots.Temple5),
                new QuestLocation("Purchase C2-7, Weaken (200 Gold)", MM2.Spots.Temple5),
                new QuestLocation("Purchase C3-1, Cold Ray (400 Gold)", MM2.Spots.Temple3),
                new QuestLocation("Purchase C3-5, Lasting Light (100 Gold)", MM2.Spots.Temple3),
                new QuestLocation("Purchase C3-6, Walk on Water (50 Gold)", MM2.Spots.CuriousFellow),
                new QuestLocation("Learn C4-2, Air Transmutation", MM2.Spots.AirTransmutation),
                new QuestLocation("Purchase C4-4, Restore Alignment (500 Gold)", MM2.Spots.Temple3),
                new QuestLocation("Purchase C4-6, Holy Bonus (2000 Gold)", MM2.Spots.Temple4),
                new QuestLocation("Learn C5-1, Air Encasement", MM2.Spots.AirEncasement),
                new QuestLocation("Defeat the Crazed Natives (C5-3, Frenzy)", MM2.Spots.CrazedNatives),
                new QuestLocation("Purchase C5-5, Remove Condition (3000 Gold)", MM2.Spots.Temple4),
                new QuestLocation("Learn C6-1, Earth Transmutatuion", MM2.Spots.EarthTransmutation),
                new QuestLocation("Learn C6-4, Water Encasement", MM2.Spots.WaterEncasement),
                new QuestLocation("Learn C6-5, Water Transmutation", MM2.Spots.WaterTransmutation),
                new QuestLocation("Learn C7-1, Earth Encasement", MM2.Spots.EarthEncasement),
                new QuestLocation("Purchase C7-2, Fiery Flail (10000 Gold)", MM2.Spots.Temple4),
                new QuestLocation("Learn C8-1, Fire Encasement", MM2.Spots.FireEncasement),
                new QuestLocation("Learn C8-2, Fire Transmutation", MM2.Spots.FireTransmutation),
                new QuestLocation("Purchase C8-3, Mass Distortion (20000 Gold)", MM2.Spots.Temple2),
                new QuestLocation("Face south in Lost Soul's Woods (C9-2, Holy Word)", MM2.Spots.HolyWord),
                new QuestLocation("Purchase C9-3, Resurrection (50000 Gold)", MM2.Spots.Temple2),
                new QuestLocation("Purchase C9-4, Uncurse Item (100000 Gold)", MM2.Spots.Temple2));
        }
    }
}