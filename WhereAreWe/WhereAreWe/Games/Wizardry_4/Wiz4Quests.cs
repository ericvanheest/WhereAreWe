using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Wiz4QuestData : WizQuestData
    {
        public byte[] SummonedMonsters;
        public List<Item> BlackBox;

        public Wiz4QuestData(WizPartyInfo party, LocationInformation location, byte[] fights, WizGameState state, Wiz4GameInfo info, byte[] summoned, List<Item> box)
            : base(party, location, fights, state)
        {
            Info = info;
            BlackBox = box;
            SummonedMonsters = summoned;
        }

        public int[] Monsters
        {
            get
            {
                int[] monsters = new int[3];
                monsters[0] = BitConverter.ToInt16(SummonedMonsters, 6);
                monsters[1] = BitConverter.ToInt16(SummonedMonsters, 8);
                monsters[2] = BitConverter.ToInt16(SummonedMonsters, 10);
                return monsters;
            }
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            if (BlackBox != null)
            {
                foreach(Item item in BlackBox)
                    Global.WriteInt16(stream, item.Index);
            }
            stream.Write(SummonedMonsters, 6, 6);
        }
    }

    public enum Wiz4QuestBits
    {
        None,
        Temple,
        Trebor
    }

    public class Wiz4Quest : BasicQuest
    {
        public Wiz4Quest()
        {
        }
    }

    public class Wiz4QuestInfo : QuestInfo
    {
        public QuestStatus MronHints = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn all of Mron's hints");
        public QuestStatus DefeatL10 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 10");
        public QuestStatus DefeatL9 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 9");
        public QuestStatus DefeatL8 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 8");
        public QuestStatus DefeatL7 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 7");
        public QuestStatus DefeatL6 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 6");
        public QuestStatus DefeatL5 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 5");
        public QuestStatus DefeatL4 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 4");
        public QuestStatus DefeatL3 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 3");
        public QuestStatus DefeatL2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 2");
        public QuestStatus DefeatL1 = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Do-Gooders on Level 1");
        public QuestStatus QuestItems1 = new QuestStatus(QuestStatus.Basic.NotStarted, "Quest items carried by Do-Gooders");
        public QuestStatus QuestItems2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Other quest items");
        public QuestStatus UsefulItems1 = new QuestStatus(QuestStatus.Basic.NotStarted, "Useful items carried by Do-Gooders");
        public QuestStatus UsefulItems2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Other useful items");
        public QuestStatus L10Escape = new QuestStatus(QuestStatus.Basic.NotStarted, "Escape from your Cell");
        public QuestStatus L10Guardians = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Pyramid Guardians");
        public QuestStatus L9HHG = new QuestStatus(QuestStatus.Basic.NotStarted, "Travel to Hell and back");
        public QuestStatus L8BlackBox = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Black Box");
        public QuestStatus L7Temple = new QuestStatus(QuestStatus.Basic.NotStarted, "Restore the Temple of the Dreampainter");
        public QuestStatus L7Dreampainter = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Dreampainter");
        public QuestStatus L6Dervish = new QuestStatus(QuestStatus.Basic.NotStarted, "Escape the Realm of the Whirling Dervish");
        public QuestStatus L5Creatures = new QuestStatus(QuestStatus.Basic.NotStarted, "Escape the Creatures of Light and Darkness");
        public QuestStatus Cube = new QuestStatus(QuestStatus.Basic.NotStarted, "Escape the Cosmic Cube");
        public QuestStatus L1Trebor = new QuestStatus(QuestStatus.Basic.NotStarted, "Remove Trebor's Curse");
        public QuestStatus Puce = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Dab of Puce");
        public QuestStatus Level = new QuestStatus(QuestStatus.Basic.NotStarted, "Raise your level");
        public QuestStatus GoodEnding = new QuestStatus(QuestStatus.Basic.NotStarted, "Finish the game (\"Good\" ending)");
        public QuestStatus EvilEnding = new QuestStatus(QuestStatus.Basic.NotStarted, "Finish the game (\"Evil\" ending)");
        public QuestStatus GrandmasterEnding = new QuestStatus(QuestStatus.Basic.NotStarted, "Finish the game (\"Grandmaster\" ending)");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { MronHints, DefeatL10, DefeatL9, DefeatL8, DefeatL7, DefeatL6, DefeatL5, DefeatL4, DefeatL3, DefeatL2, 
                DefeatL1, QuestItems1, QuestItems2, UsefulItems1, UsefulItems2, L10Escape, L10Guardians, L9HHG, L8BlackBox, L7Temple, L7Dreampainter, 
                L6Dervish, L5Creatures, Cube, L1Trebor, Puce, Level, GoodEnding, EvilEnding, GrandmasterEnding };
        }

        private QuestStatus[] qsDoGooders { get { return new QuestStatus[] { DefeatL1, DefeatL2, DefeatL3, DefeatL4, DefeatL5, DefeatL6, DefeatL7, DefeatL8, DefeatL9, DefeatL10 }; } }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<Wiz4Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<Wiz4Quest> quests, string strReward = "", string strPath = "")
        {
            Wiz4Quest quest = GetQuest(status, BasicQuestType.Side, null, null, strReward) as Wiz4Quest;
            if (!String.IsNullOrWhiteSpace(strPath))
                quest.Path = strPath;
            quests.Add(quest);
            return quest;
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<Wiz4Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<Wiz4Quest> quests, string strReward = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<Wiz4Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            Wiz4Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as Wiz4Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            const string obtainItems = "Obtain Items";

            List<Wiz4Quest> quests = new List<Wiz4Quest>();

            for (int i = 0; i < 42; i++)
                MronHints.AddLocations(new QuestLocation(String.Format("Pay Mron for hint #{0}", i + 1), Wiz4.Spots.None));
            AddSideQuest(MronHints, quests);

            for (int level = 1; level <= 10; level++)
            {
                foreach(int iChar in Wiz4GameInfo.DoGoodersOnLevel(level))
                {
                    string strDoGooder = Wiz4GameInfo.DoGooder(iChar);
                    if (!String.IsNullOrWhiteSpace(strDoGooder))
                        qsDoGooders[level - 1].AddLocations(new QuestLocation(String.Format("Defeat \"{0}\"", strDoGooder), Wiz4.Spots.Level(level)));
                }
            }

            for(int i = 0; i < qsDoGooders.Length; i++)
                AddSideQuest(qsDoGooders[i], quests, "", "Defeat Do-Gooders (repeatable)").SortOrder = qsDoGooders.Length - i;

            QuestItems1.AddLocations(new QuestLocation("Winged Boots (Pair of L-5 Pioneers)", Wiz4.Spots.WingedBoots1),
                new QuestLocation("Dreampainter Ka (Dreampainter)", Wiz4.Spots.Dreampainter),
                new QuestLocation("Pennonceaux (Joachim's Jihad)", Wiz4.Spots.Level2),
                new QuestLocation("Blade Cusinart' (Joachim's Jihad)", Wiz4.Spots.Level2),
                new QuestLocation("Magician's Hat (Horin's Holy Rollers)", Wiz4.Spots.Level5),
                new QuestLocation("Arabic Diary (Jesse the Smith)", Wiz4.Spots.GoldenPyrite),
                new QuestLocation("Black Candle (Pyramid Guard)", Wiz4.Spots.PyramidGuard),
                new QuestLocation("Inn Key (Innkeyper)", Wiz4.Spots.Innkeyper),
                new QuestLocation("Holy Limp Wrist (Loktar's Lucky Ladies)", Wiz4.Spots.Level1));
            AddSideQuest(QuestItems1, quests, "", obtainItems).SortOrder = 1;

            QuestItems2.AddLocations(new QuestLocation("Bloodstone", Wiz4.Spots.Bloodstone),
                new QuestLocation("Lander's Turquoise", Wiz4.Spots.LandersTurquoise),
                new QuestLocation("Amber Dragon", Wiz4.Spots.AmberDragon),
                new QuestLocation("Holy Hand Grenade of Aunty Ock", Wiz4.Spots.GatesOfHell),
                new QuestLocation("Hopalong Carrot", Wiz4.Spots.HopalongCarrot),
                new QuestLocation("Cleansing Oil (50000 Gold)", Wiz4.Spots.CleansingOil),
                new QuestLocation("Witching Rod", Wiz4.Spots.WitchingRod),
                new QuestLocation("Aromatic Ball", Wiz4.Spots.AromaticBall),
                new QuestLocation("Void Transducer", Wiz4.Spots.PackratNest),
                new QuestLocation("Kris of Truth", Wiz4.Spots.KrisOfTruth),
                new QuestLocation("Crystal Rose", Wiz4.Spots.LadiesOfTheRose),
                new QuestLocation("Dab of Puce", Wiz4.Spots.Witch),
                new QuestLocation("Maintenance Cap", Wiz4.Spots.OrderOfThePelican),
                new QuestLocation("Golden Pyrite", Wiz4.Spots.GoldenPyrite),
                new QuestLocation("Demonic Chimes", Wiz4.Spots.Hellhound),
                new QuestLocation("St. Trebor Rump", Wiz4.Spots.TreborRump),
                new QuestLocation("Arrow of Truth", Wiz4.Spots.OrderOfTheLaurel),
                new QuestLocation("Orb of Dreams", Wiz4.Spots.TygersCubs),
                new QuestLocation("Rallying Horn", Wiz4.Spots.CaptainsCouncil),
                new QuestLocation("Signet Ring", Wiz4.Spots.CouncilOfBarons),
                new QuestLocation("Mythril Glove", Wiz4.Spots.MythrilGauntlets),
                new QuestLocation("Sacred Sword", Wiz4.Spots.Altar));
            AddSideQuest(QuestItems2, quests, "", obtainItems).SortOrder = 2;

            UsefulItems1.AddLocations(new QuestLocation("St. Ka's Foot (Softalk All-Stars Minus One)", Wiz4.Spots.SoftalkAllStars),
                new QuestLocation("Dagger of Speed (Raist)", Wiz4.Spots.Level2),
                new QuestLocation("Initiate Turban (Jesse the Smith)", Wiz4.Spots.JesseTheSmith),
                new QuestLocation("Novice's Cap (Walking Wounded)", Wiz4.Spots.WalkingWounded),
                new QuestLocation("Black Box (Glum)", Wiz4.Spots.BlackBox),
                new QuestLocation("Wizard Skullcap (The Company)", Wiz4.Spots.Level3),
                new QuestLocation("Mordorcharge (Thorin's Tramplers)", Wiz4.Spots.Level7),
                new QuestLocation("Cape of Jackal (Sorriman's Sorcerers)", Wiz4.Spots.Level7),
                new QuestLocation("Get Out of Jail Free (Khan's Kosmic Killers)", Wiz4.Spots.Level4),
                new QuestLocation("Ring of Healing (Bilbous Baggins)", Wiz4.Spots.Level3),
                new QuestLocation("Ring of Dispelling (Frodough)", Wiz4.Spots.Level3),
                new QuestLocation("Adept Baldness (Applet's Angels)", Wiz4.Spots.Level1),
                new QuestLocation("St. Rimbo Digit (Gomez's Gorillas)", Wiz4.Spots.Level6),
                new QuestLocation("Twilight Cloak (Officer's Mess)", Wiz4.Spots.Level9),
                new QuestLocation("Shadow Cloak (Killer)", Wiz4.Spots.Level7),
                new QuestLocation("Cone of Silence (Talon's Tigers)", Wiz4.Spots.Level9),
                new QuestLocation("Darkness Cloak (Boz)", Wiz4.Spots.Level6),
                new QuestLocation("Night Cloak (Stalker)", Wiz4.Spots.Level4),
                new QuestLocation("Entropy Cloak (Elindull's Evil Elites)", Wiz4.Spots.Level2));
            AddSideQuest(UsefulItems1, quests, "", obtainItems).SortOrder = 3;

            UsefulItems2.AddLocations(new QuestLocation("Good Hope Cape", Wiz4.Spots.GoodHopeCape),
                new QuestLocation("Oxygen Mask", Wiz4.Spots.OxygenMask));
            AddSideQuest(UsefulItems2, quests, "", obtainItems).SortOrder = 4;

            L10Escape.AddLocations(new QuestLocation("Summon Level 1 Priests", Wiz4.Spots.Pentagram10A),
                new QuestLocation("Fight Do-Gooders until a priest casts Milwa", Wiz4.Spots.Level10),
                new QuestLocation("Exit through the secret door", Wiz4.Spots.CellDoor));
            AddMainQuest(L10Escape, quests).SortOrder = 1;

            L10Guardians.AddLocations(new QuestLocation("Defeat the Inner Guardian", Wiz4.Spots.InnerGuardian),
                new QuestLocation("Defeat the Middle Guardian", Wiz4.Spots.MiddleGuardian),
                new QuestLocation("Defeat the Outer Guardian", Wiz4.Spots.OuterGuardian),
                new QuestLocation("Defeat the Pyramid Guardian", Wiz4.Spots.PyramidGuard));
            AddMainQuest(L10Guardians, quests).SortOrder = 2;

            L7Temple.AddLocations(new QuestLocation("Obtain the Bloodstone", Wiz4.Spots.Bloodstone),
                new QuestLocation("Obtain the Lander's Turquoise", Wiz4.Spots.LandersTurquoise),
                new QuestLocation("Obtain the Amber Dragon", Wiz4.Spots.AmberDragon),
                new QuestLocation("Invoke the Bloodstone", Wiz4.Spots.Altar),
                new QuestLocation("Invoke the Lander's Turquoise", Wiz4.Spots.Altar),
                new QuestLocation("Invoke the Amber Dragon", Wiz4.Spots.Altar),
                new QuestLocation("Offer the invoked Bloodstone", Wiz4.Spots.Altar),
                new QuestLocation("Offer the invoked Lander's Turquoise", Wiz4.Spots.Altar),
                new QuestLocation("Offer the invoked Amber Dragon", Wiz4.Spots.Altar),
                new QuestLocation("Choose one of the Sacred Swords", Wiz4.Spots.Altar));
            BasicQuest questTemple = AddMainQuest(L7Temple, quests, "Sacred Sword");
            questTemple.Bits = new QuestBits(Wiz4QuestBits.Temple);
            questTemple.SortOrder = 3;

            L6Dervish.AddLocations(new QuestLocation("Defeat Jesse the Smith", Wiz4.Spots.JesseTheSmith),
                new QuestLocation("Climb the stairs", Wiz4.Spots.Level6StairsUp));
            AddMainQuest(L6Dervish, quests).SortOrder = 4;

            L5Creatures.AddLocations(new QuestLocation("Use the teleporter to go outside the walls", Wiz4.Spots.Level5TeleporterOut),
                new QuestLocation("Climb the stairs", Wiz4.Spots.Level5StairsUp));
            AddMainQuest(L5Creatures, quests).SortOrder = 5;

            L7Dreampainter.AddLocations(new QuestLocation("Defeat the Pair of L-5 Pioneers (Winged Boots)", Wiz4.Spots.WingedBoots1),
                new QuestLocation("Invoke the Winged Boots", Wiz4.Spots.Level7),
                new QuestLocation("Obtain the Hopalong Carrot", Wiz4.Spots.HopalongCarrot),
                new QuestLocation("Use the Hopalong Carrot (while facing north)", Wiz4.Spots.HopOverWall),
                new QuestLocation("Defeat the Dreampainter (Dreampainter Ka)", Wiz4.Spots.Dreampainter));
            BasicQuest questDreampainter = AddMainQuest(L7Dreampainter, quests, "Dreampainter Ka");
            questDreampainter.SortOrder = 6;

            L9HHG.PreQuest.Add(questDreampainter);
            L9HHG.AddLocations(new QuestLocation("Defeat the Pair of L-5 Pioneers (Winged Boots)", Wiz4.Spots.WingedBoots1),
                new QuestLocation("Invoke the Winged Boots", Wiz4.Spots.Level7), 
                new QuestLocation("Defeat the Hellhound (Demonic Chimes)", Wiz4.Spots.Hellhound),
                new QuestLocation("Defeat Jesse the Smith (Arabic Diary)", Wiz4.Spots.GoldenPyrite),
                new QuestLocation("Defeat the Pyramid Guard (Black Candle)", Wiz4.Spots.PyramidGuard),
                new QuestLocation("Use the Demonic Chimes", Wiz4.Spots.GatesOfHell),
                new QuestLocation("Use the Arabic Diary", Wiz4.Spots.GatesOfHell),
                new QuestLocation("Use the Black Candle", Wiz4.Spots.GatesOfHell));
            BasicQuest questHHG = AddMainQuest(L9HHG, quests, "Holy Hand Grenade");
            questHHG.SortOrder = 7;

            Cube.PreQuest.Add(questHHG);
            Cube.AddLocations(new QuestLocation("Enter the Cosmic Cube", Wiz4.Spots.Cube01Start),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube02Teleport),
                new QuestLocation("Climb up the stairs", Wiz4.Spots.Cube03StairsUp),
                new QuestLocation("Go down the chute", Wiz4.Spots.Cube04ChuteDown),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube05ChuteUp),
                new QuestLocation("Go down the chute", Wiz4.Spots.Cube06ChuteDown),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube07ChuteUp),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube08Teleport),
                new QuestLocation("Climb the stairs", Wiz4.Spots.Cube09Stairs),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube10ChuteUp),
                new QuestLocation("Go down the chute", Wiz4.Spots.Cube11ChuteDown),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube12ChuteUp),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube13ChuteUp),
                new QuestLocation("Climb down the stairs", Wiz4.Spots.Cube14StairsDown),
                new QuestLocation("Go up the chute", Wiz4.Spots.Cube15ChuteUp),
                new QuestLocation("Use the chute", Wiz4.Spots.Cube16Chute),
                new QuestLocation("Buy the Cleansing Oil (50,000 Gold)", Wiz4.Spots.CleansingOil),
                new QuestLocation("Climb down the stairs", Wiz4.Spots.Cube17StairsDown),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube18Teleport),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube19Teleport),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube20Teleport),
                new QuestLocation("Climb up the stairs", Wiz4.Spots.Cube21StairsUp),
                new QuestLocation("Buy St. Trebor's Rump (100 Gold)", Wiz4.Spots.TreborRump),
                new QuestLocation("Go down the chute", Wiz4.Spots.Cube22ChuteDown),
                new QuestLocation("Climb up the stairs", Wiz4.Spots.Cube23StairsUp),
                new QuestLocation("Climb down the stairs", Wiz4.Spots.Cube24StairsDown),
                new QuestLocation("Use the teleporter", Wiz4.Spots.Cube25Teleport),
                new QuestLocation("Climb down the stairs", Wiz4.Spots.Cube26StairsDown),
                new QuestLocation("Use the teleporter to Level 2", Wiz4.Spots.Cube27TeleportUp),
                new QuestLocation("Use the teleporter to Level 1", Wiz4.Spots.Cube28TeleportUp),
                new QuestLocation("Invoke the Holy Hand Grenade", Wiz4.Spots.Cube29UseHHG),
                new QuestLocation("Invoke the Cleansing Oil", Wiz4.Spots.Cube29UseHHG),
                new QuestLocation("Drop the Holy Hand Grenade", Wiz4.Spots.Cube29UseHHG),
                new QuestLocation("Escape the explosion", Wiz4.Spots.Cube30EscapeHHG),
                new QuestLocation("Climb the stairs", Wiz4.Spots.Cube31StairsUp));
            AddMainQuest(Cube, quests, "Permits Malor to 0,0 on Castle First Floor").SortOrder = 8;

            Puce.AddLocations(new QuestLocation("Buy Cleansing Oil (50000 Gold)", Wiz4.Spots.CleansingOil),
                new QuestLocation("Wade into the pool (Witching Rod)", Wiz4.Spots.WitchingRod),
                new QuestLocation("Pick up a marble (Aromatic Ball)", Wiz4.Spots.AromaticBall),
                new QuestLocation("Grab a nugget (Golden Pyrite)", Wiz4.Spots.GoldenPyrite),
                new QuestLocation("Defeat Joachim's Jihad (Blade Cusinart')", Wiz4.Spots.Level2),
                new QuestLocation("Defeat Horin's Holy Rollers (Magician's Hat)", Wiz4.Spots.Level5));
            Puce.Postrequisites.Add(new QuestLocation("Give the items to the Witch", Wiz4.Spots.Witch));
            BasicQuest questPuce = AddMainQuest(Puce, quests, "Dab of Puce");
            questPuce.SortOrder = 9;

            L1Trebor.AddLocations(new QuestLocation("Buy St. Trebor's Rump (100 Gold)", Wiz4.Spots.TreborRump));
            L1Trebor.Postrequisites.Add(new QuestLocation("Invoke the St. Trebor's Rump", Wiz4.Spots.Level1));
            BasicQuest questTrebor = AddMainQuest(L1Trebor, quests);
            questTrebor.Bits = new QuestBits(Wiz4QuestBits.Trebor);
            questTrebor.SortOrder = 10;

            GoodEnding.PreQuest.Add(questDreampainter);
            GoodEnding.PreQuest.Add(questTemple);
            GoodEnding.PreQuest.Add(questPuce);
            GoodEnding.PreQuest.Add(questTrebor);
            GoodEnding.AddLocations(new QuestLocation("Use the Ron Wartow wading pool", Wiz4.Spots.NeutralPool),
                new QuestLocation("Defeat the Innkeyper (Inn Key)", Wiz4.Spots.Innkeyper),
                new QuestLocation("Give the Puce to the Order of the Laurel (Arrow of Truth)", Wiz4.Spots.OrderOfTheLaurel),
                new QuestLocation("Defeat Joachim's Jihad (Pennonceaux)", Wiz4.Spots.Level2),
                new QuestLocation("Give the Pennonceaux to the Tyger's Cubs (Orb of Dreams)", Wiz4.Spots.TygersCubs),
                new QuestLocation("Talk to the Order of the Pelican (Maintenance Cap)", Wiz4.Spots.OrderOfThePelican),
                new QuestLocation("Equip the Maintenance Cap", Wiz4.Spots.None),
                new QuestLocation("Repair the fountain", Wiz4.Spots.GoodFountain),
                new QuestLocation("Bathe in the fountain", Wiz4.Spots.GoodFountain),
                new QuestLocation("Abandon your summoned monsters", Wiz4.Spots.GoodFountain),
                new QuestLocation("Talk to the Ladies of the Rose (Crystal Rose)", Wiz4.Spots.LadiesOfTheRose),
                new QuestLocation("Equip the Crystal Rose", Wiz4.Spots.None),
                new QuestLocation("Invoke the Crystal Rose", Wiz4.Spots.None),
                new QuestLocation("Pay the Council of Barons 1000000 Gold (Rallying Horn)", Wiz4.Spots.CouncilOfBarons),
                new QuestLocation("Sign the parchment (Signet Ring)", Wiz4.Spots.CaptainsCouncil),
                new QuestLocation("Agree to duel the Dukes", Wiz4.Spots.DukesCouncil));
            GoodEnding.Postrequisites.Add(new QuestLocation("Agree to the Dukes' terms", Wiz4.Spots.DukesCouncil));
            AddMainQuest(GoodEnding, quests).SortOrder = 11;

            EvilEnding.PreQuest.Add(questTemple);
            EvilEnding.AddLocations(new QuestLocation("Maintain an Evil alignment", Wiz4.Spots.EvilFountain),
                new QuestLocation("Defeat Loktar's Lucky Ladies (Holy Limp Wrist)", Wiz4.Spots.Level1),
                new QuestLocation("Answer the riddle (Mythril Glove)", Wiz4.Spots.MythrilGauntlets),
                new QuestLocation("Defeat the Innkeyper (Inn Key)", Wiz4.Spots.Innkeyper),
                new QuestLocation("Summon a Dink", Wiz4.Spots.Pentagram01B),
                new QuestLocation("Defeat the Von Halstern Chivalry (or have the Crystal Rose)", Wiz4.Spots.VonHalsternChivalry),
                new QuestLocation("Defeat the Softalk All-Stars Minus One", Wiz4.Spots.SoftalkAllStars),
                new QuestLocation("Defeat the Temple Priests", Wiz4.Spots.TemplePriests),
                new QuestLocation("Defeat Hawkwind", Wiz4.Spots.Hawkwind),
                new QuestLocation("Equip the Mythril Glove", Wiz4.Spots.None),
                new QuestLocation("Equip one of the Sacred Swords", Wiz4.Spots.None));
            EvilEnding.Postrequisites.Add(new QuestLocation("Engage Kadorto", Wiz4.Spots.Kadorto));
            AddMainQuest(EvilEnding, quests).SortOrder = 12;

            GrandmasterEnding.PreQuest.Add(questTemple);
            GrandmasterEnding.AddLocations(new QuestLocation("Defeat Loktar's Lucky Ladies (Holy Limp Wrist)", Wiz4.Spots.Level1),
                new QuestLocation("Answer the riddle (Mythril Glove)", Wiz4.Spots.MythrilGauntlets),
                new QuestLocation("Defeat the Innkeyper (Inn Key)", Wiz4.Spots.Innkeyper),
                new QuestLocation("Defeat the Von Halstern Chivalry", Wiz4.Spots.VonHalsternChivalry),
                new QuestLocation("Defeat the Softalk All-Stars Minus One", Wiz4.Spots.SoftalkAllStars),
                new QuestLocation("Reach into the Black Hole (Void Transducer)", Wiz4.Spots.PackratNest),
                new QuestLocation("Make your way back to the cell", Wiz4.Spots.StartingPosition),
                new QuestLocation("Cast Malor and teleport one floor down", Wiz4.Spots.StartingPosition),
                new QuestLocation("Equip the Void Transducer", Wiz4.Spots.Level11),
                new QuestLocation("Answer the riddles and talk to the being (Kris of Truth)", Wiz4.Spots.KrisOfTruth),
                new QuestLocation("Summon a Dink", Wiz4.Spots.Pentagram01B),
                new QuestLocation("Defeat (or avoid) the Von Halstern Chivalry again", Wiz4.Spots.VonHalsternChivalry),
                new QuestLocation("Defeat the Softalk All-Stars Minus One again", Wiz4.Spots.SoftalkAllStars),
                new QuestLocation("Defeat the Temple Priests", Wiz4.Spots.TemplePriests),
                new QuestLocation("Defeat Hawkwind", Wiz4.Spots.Hawkwind),
                new QuestLocation("Equip the Mythril Glove", Wiz4.Spots.None),
                new QuestLocation("Equip the Kris of Truth", Wiz4.Spots.None));
            GrandmasterEnding.Postrequisites.Add(new QuestLocation("Engage Kadorto", Wiz4.Spots.Kadorto));
            AddMainQuest(GrandmasterEnding, quests).SortOrder = 13;

            Level.AddLocations(new QuestLocation("Use a pentragram on level 10", Wiz4.Spots.Pentagram10A),
                new QuestLocation("Use a pentragram on level 10", Wiz4.Spots.Pentagram10B),
                new QuestLocation("Use a pentragram on level 9", Wiz4.Spots.Pentagram09),
                new QuestLocation("Use a pentragram on level 8", Wiz4.Spots.Pentagram08),
                new QuestLocation("Use a pentragram on level 7", Wiz4.Spots.Pentagram07),
                new QuestLocation("Use a pentragram on level 6", Wiz4.Spots.Pentagram06A),
                new QuestLocation("Use a pentragram on level 6", Wiz4.Spots.Pentagram06B),
                new QuestLocation("Use a pentragram on level 6", Wiz4.Spots.Pentagram06C),
                new QuestLocation("Use a pentragram on level 5", Wiz4.Spots.Pentagram05),
                new QuestLocation("Use a pentragram on level 4", Wiz4.Spots.Pentagram04A),
                new QuestLocation("Use a pentragram on level 4", Wiz4.Spots.Pentagram04B),
                new QuestLocation("Use a pentragram on level 4", Wiz4.Spots.Pentagram04C),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03A),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03B),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03C),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03D),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03E),
                new QuestLocation("Use a pentragram on level 3", Wiz4.Spots.Pentagram03F),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02A),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02B),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02C),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02D),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02E),
                new QuestLocation("Use a pentragram on level 2", Wiz4.Spots.Pentagram02F),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01A),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01B),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01C),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01D),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01E),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01F),
                new QuestLocation("Use a pentragram on level 1", Wiz4.Spots.Pentagram01G));
            AddSideQuest(Level, quests, "10 HP and 9 SP per level");

            quests.Sort(CompareWiz4Quests);
            return quests.ToArray();
        }

        public int CompareWiz4Quests(Wiz4Quest quest1, Wiz4Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public bool AboveMap(int iIndex, int iLevel)
        {
            switch ((Wiz4Map) iIndex)
            {
                case Wiz4Map.CastleFirstFloor:
                case Wiz4Map.CastleSecondFloor:
                case Wiz4Map.CastleThirdFloor:
                case Wiz4Map.L11Grandmaster:
                    // The castle maps are above any of the other levels,
                    // and the Grandmaster level, although technically below all, is gameplay-wise "above" everything for quest purposes
                    return true;
                default:
                    // WizMap level 10 is index 11, and up from there
                    return (iIndex <= iLevel);
            }
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            WizPartyInfo party = data.Party as WizPartyInfo;
            LocationInformation location = data.Location;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            if (!(data is Wiz4QuestData))
                return;

            Wiz4QuestData questData = data as Wiz4QuestData;

            byte[] fights = questData.Fights;
            WizGameState state = questData.State as WizGameState;
            Wiz4GameInfo info = questData.Info as Wiz4GameInfo;
            Wiz4EncounterInfo encounter = questData.Encounter as Wiz4EncounterInfo;
            List<Item> box = questData.BlackBox;
            int[] monsters = questData.Monsters;
            Point pt = location.PrimaryCoordinates;

            if (info == null || state == null)
                return;

            Wiz4Character werdna = Wiz4Character.Create(0, party.Bytes, 0, info, encounter, questData.BlackBox);
            int iLevel = werdna.Level;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = werdna.Name;
            CharAddress = iOverrideCharAddress;

            for (int i = 0; i < 42; i++)
                MronHints.AddObj(info.MronHint > i);

            for (int level = 1; level <= 10; level++)
                foreach (int iChar in Wiz4GameInfo.DoGoodersOnLevel(level))
                    qsDoGooders[level - 1].AddObj(Global.GetBit(info.DoGooders, iChar, false) != 0);

            foreach (Wiz4ItemIndex index in new Wiz4ItemIndex[] { Wiz4ItemIndex.WingedBoots, Wiz4ItemIndex.DreampainterKa, Wiz4ItemIndex.Pennonceaux,
                Wiz4ItemIndex.BladeCusinart, Wiz4ItemIndex.MagiciansHat, Wiz4ItemIndex.ArabicDiary, Wiz4ItemIndex.BlackCandle, Wiz4ItemIndex.InnKey, Wiz4ItemIndex.HolyLimpWrist })
                QuestItems1.AddObj(werdna.HasItem((int)index));

            foreach (Wiz4ItemIndex index in new Wiz4ItemIndex[] { Wiz4ItemIndex.Bloodstone, Wiz4ItemIndex.LandersTurq, Wiz4ItemIndex.AmberDragon, Wiz4ItemIndex.HHGOfAuntyOck,
                Wiz4ItemIndex.HopalongCarrot, Wiz4ItemIndex.CleansingOil, Wiz4ItemIndex.WitchingRod, Wiz4ItemIndex.AromaticBall, Wiz4ItemIndex.VoidTransducer, Wiz4ItemIndex.KrisOfTruth,
                Wiz4ItemIndex.CrystalRose, Wiz4ItemIndex.DabOfPuce, Wiz4ItemIndex.MaintenanceCap, Wiz4ItemIndex.GoldenPyrite, Wiz4ItemIndex.DemonicChimes,
                Wiz4ItemIndex.StTreborRump, Wiz4ItemIndex.ArrowOfTruth, Wiz4ItemIndex.OrbOfDreams, Wiz4ItemIndex.RallyingHorn, Wiz4ItemIndex.SignetRing, Wiz4ItemIndex.MythrilGlove })
                QuestItems2.AddObj(werdna.HasItem((int)index));
            QuestItems2.AddObj(werdna.HasItem((int)Wiz4ItemIndex.DragonsClaw) || werdna.HasItem((int)Wiz4ItemIndex.EastWindSword) || werdna.HasItem((int)Wiz4ItemIndex.WestWindSword));

            foreach (Wiz4ItemIndex index in new Wiz4ItemIndex[] { Wiz4ItemIndex.StKasFoot, Wiz4ItemIndex.DaggerOfSpeed, Wiz4ItemIndex.InitiateTurban, Wiz4ItemIndex.NovicesCap,
                Wiz4ItemIndex.BlackBox, Wiz4ItemIndex.WizardSkullcap, Wiz4ItemIndex.Mordorcharge, Wiz4ItemIndex.CapeOfJackal, Wiz4ItemIndex.GetOutOfJailFree, Wiz4ItemIndex.RingOfHealing,
                Wiz4ItemIndex.RingOfDispelling, Wiz4ItemIndex.AdeptBaldness, Wiz4ItemIndex.StRimboDigit, Wiz4ItemIndex.TwilightCloak, Wiz4ItemIndex.ShadowCloak, Wiz4ItemIndex.ConeOfSilence,
                Wiz4ItemIndex.DarknessCloak, Wiz4ItemIndex.NightCloak, Wiz4ItemIndex.EntropyCloak })
                UsefulItems1.AddObj(werdna.HasItem((int)index));

            foreach (Wiz4ItemIndex index in new Wiz4ItemIndex[] { Wiz4ItemIndex.GoodHopeCape, Wiz4ItemIndex.OxygenMask })
                UsefulItems2.AddObj(werdna.HasItem((int)index));

            bool bLev10 = location.MapIndex == (int)Wiz4Map.L10PyramidOfEntrapment;
            bool bInCell = bLev10 && Global.PointInRects(pt, new Rectangle(9, 9, 2, 2));
            L10Escape.StrictProgression = true;
            L10Escape.AddObj(monsters.Contains((int)Wiz4MonsterIndex.Level1Priest), info.Light > 0, !bInCell);

            L10Guardians.AddObj(!bLev10 || !WizMapData.Fight(fights, Wiz4.Spots.InnerGuardian),
                !bLev10 || !WizMapData.Fight(fights, Wiz4.Spots.MiddleGuardian),
                !bLev10 || !WizMapData.Fight(fights, Wiz4.Spots.OuterGuardian),
                !bLev10 || !WizMapData.Fight(fights, Wiz4.Spots.PyramidGuard));

            bool bTemple = info.BloodstoneLoc == 2 && info.TurquoiseLoc == 2 && info.AmberDragonLoc == 2;
            bool bBlood = werdna.HasItem((int)Wiz4ItemIndex.Bloodstone);
            bool bTurq = werdna.HasItem((int)Wiz4ItemIndex.LandersTurq);
            bool bAmber = werdna.HasItem((int)Wiz4ItemIndex.AmberDragon);
            bool bBloodIn = info.BloodstoneLoc == 2;
            bool bTurqIn = info.TurquoiseLoc == 2;
            bool bAmberIn = info.AmberDragonLoc == 2;
            L7Temple.AddObj(bTemple || bBlood || bBloodIn, bTemple || bTurq || bTurqIn, bTemple || bAmber || bAmberIn,
                info.Bloodstone || bBloodIn, info.Turquoise || bTurqIn, info.AmberDragon || bAmberIn,
                bBloodIn, bTurqIn, bAmberIn, bTemple);

            bool bAbove6 = AboveMap(location.MapIndex, 6);
            L6Dervish.StrictProgression = true;
            L6Dervish.AddObj(!WizMapData.Fight(fights, Wiz4.Spots.JesseTheSmith), bAbove6); 

            bool bAbove5 = AboveMap(location.MapIndex, 5);
            bool bOutsideWall = location.MapIndex == (int)Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness && Global.PointInRects(pt,
                new Rectangle(0, 14, 20, 6), new Rectangle(16, 0, 4, 20), new Rectangle(0, 12, 2, 2), new Rectangle(4, 12, 2, 2), new Rectangle(8, 12, 2, 2),
                new Rectangle(12, 12, 2, 2), new Rectangle(16, 12, 2, 2), new Rectangle(16, 8, 2, 2), new Rectangle(16, 4, 2, 2), new Rectangle(16, 0, 2, 2));
            L5Creatures.AddObj(bAbove5 || bOutsideWall, bAbove5);

            bool bInDreamRoom = location.MapIndex == (int) Wiz4Map.L7TempleOfTheDreampainter && Global.PointInRects(pt, new Rectangle(6, 8, 2, 2));
            bool bHaveKa = werdna.HasItem((int) Wiz4ItemIndex.DreampainterKa);
            bool bHaveCarrot = werdna.HasItem((int) Wiz4ItemIndex.HopalongCarrot);
            bool bHaveBoots = werdna.HasItem((int)Wiz4ItemIndex.WingedBoots);
            L7Dreampainter.AddObj(bHaveKa || bHaveCarrot || bHaveBoots,
                bHaveKa || bHaveCarrot || info.UsedBoots,
                bHaveKa || werdna.HasItem((int)Wiz4ItemIndex.HopalongCarrot),
                bHaveKa || bInDreamRoom,
                bHaveKa);

            bool bChimes = werdna.HasItem((int)Wiz4ItemIndex.DemonicChimes);
            bool bDiary = werdna.HasItem((int)Wiz4ItemIndex.ArabicDiary);
            bool bCandle = werdna.HasItem((int)Wiz4ItemIndex.BlackCandle);
            bool bHHG = werdna.HasItem((int)Wiz4ItemIndex.HHGOfAuntyOck) || info.UsedHHG || info.PinPulled != 0;
            L9HHG.AddObj(bHHG || bHaveBoots,
                bHHG || info.UsedBoots,
                bHHG || bChimes,
                bHHG || bDiary,
                bHHG || bCandle,
                bHHG || info.HellItems > 0,
                bHHG || info.HellItems > 1,
                bHHG);

            bool bLev1 = location.MapIndex == (int)Wiz4Map.L1CosmicCube;
            bool bLev2 = location.MapIndex == (int)Wiz4Map.L2CosmicCube;
            bool bLev3 = location.MapIndex == (int)Wiz4Map.L3CosmicCube;
            bool bInCube = (bLev1 || bLev2 || bLev3);
            bool bOil = werdna.HasItem((int) Wiz4ItemIndex.CleansingOil);
            bool bRump = werdna.HasItem((int) Wiz4ItemIndex.StTreborRump);

            if (!bInCube)
            {
                if (AboveMap(location.MapIndex, 1))
                    Cube.AddObj(35, QuestGoal.Complete);
                else
                    Cube.AddObj(35, QuestGoal.Incomplete);

                Cube.Obj[16] = bOil ? QuestStatus.Single.Complete : QuestStatus.Single.Incomplete;
                Cube.Obj[22] = (info.CurseLifted || bRump) ? QuestStatus.Single.Complete : QuestStatus.Single.Incomplete;
            }
            else
            {
                bool[] clusters = new bool[29];
				clusters[28] = bLev1 && Global.PointInRects(pt, 15, 16, 1, 1);
				clusters[27] = clusters[28] || (bLev1 && Global.PointInRects(pt, 15, 15, 5, 1, 16, 14, 1, 1, 18, 14, 1, 1));
				clusters[26] = clusters[27] || (bLev2 && Global.PointInRects(pt, 2, 0, 13, 1, 3, 1, 12, 1, 4, 2, 11, 1, 5, 3, 10, 1, 6, 4, 9, 1, 7, 5, 8, 1, 8, 6, 4, 1, 9, 7, 4, 1));
				clusters[25] = clusters[26] || (bLev3 && Global.PointInRects(pt, 0, 0, 4, 9));
				clusters[24] = clusters[25] || (bLev1 && Global.PointInRects(pt, 0, 0, 1, 4, 1, 0, 1, 3, 2, 0, 1, 2, 3, 0, 2, 1, 4, 1, 2, 1, 5, 2, 2, 1, 6, 3, 2, 1, 7, 4, 2, 1, 8, 5, 2, 1));
				clusters[23] = clusters[24] || (bLev3 && Global.PointInRects(pt, 4, 0, 7, 9, 11, 0, 1, 6, 12, 0, 1, 5, 13, 0, 1, 4));
				clusters[22] = clusters[23] || (bLev1 && Global.PointInRects(pt, 0, 7, 1, 6, 1, 8, 1, 5, 2, 9, 1, 4, 3, 10, 1, 2, 4, 9, 1, 4, 5, 8, 1, 4, 6, 7, 1, 6, 7, 6, 4, 6, 9, 11, 5, 2));
				clusters[21] = clusters[22] || (bLev2 && Global.PointInRects(pt, 15, 0, 5, 20, 13, 11, 2, 9, 14, 7, 1, 1));
				clusters[20] = clusters[21] || (bLev1 && Global.PointInRects(pt, 5, 0, 15, 1, 6, 1, 14, 1, 7, 2, 13, 1, 8, 3, 12, 1, 9, 4, 11, 1, 10, 5, 10, 1, 14, 6, 6, 5, 14, 11, 1, 1, 16, 11, 4, 1, 17, 11, 3, 1, 18, 13, 1, 1));
				clusters[19] = clusters[20] || (bLev3 && Global.PointInRects(pt, 12, 16, 6, 4, 18, 18, 2, 2, 19, 16, 1, 2));
				clusters[18] = clusters[19] || (bLev3 && Global.PointInRects(pt, 7, 10, 3, 3));
				clusters[17] = clusters[18] || (bLev3 && Global.PointInRects(pt, 1, 16, 3, 3));
				clusters[16] = clusters[17] || (bLev3 && Global.PointInRects(pt, 0, 19, 12, 1, 0, 9, 1, 10, 4, 16, 8, 3, 1, 9, 6, 7, 7, 13, 4, 3, 10, 12, 3, 1, 10, 11, 2, 1, 7, 9, 4, 1, 10, 10, 1, 1));
				clusters[15] = clusters[16] || (bLev2 && Global.PointInRects(pt, 0, 5, 1, 4, 1, 6, 1, 8, 2, 7, 1, 10, 3, 8, 1, 9, 4, 9, 1, 8, 5, 10, 1, 6, 6, 11, 1, 5, 7, 12, 3, 6, 9, 11, 4, 3, 10, 10, 2, 1, 11, 8, 2, 2, 10, 8, 1, 1));
				clusters[14] = clusters[15] || (bLev2 && Global.PointInRects(pt, 0, 2, 1, 3, 1, 3, 1, 3, 2, 4, 1, 3, 3, 5, 1, 3, 4, 6, 1, 3, 5, 7, 1, 3, 6, 8, 1, 3, 7, 9, 1, 3, 8, 10, 1, 2));
				clusters[13] = clusters[14] || (bLev3 && Global.PointInRects(pt, 16, 6, 2, 5, 18, 6, 1, 1, 14, 0, 3, 6, 17, 0, 1, 4, 18, 3, 1, 1));
				clusters[12] = clusters[13] || (bLev1 && Global.PointInRects(pt, 12, 7, 2, 3));
				clusters[11] = clusters[12] || (bLev2 && Global.PointInRects(pt, 14, 8, 1, 2));
				clusters[10] = clusters[11] || (bLev3 && Global.PointInRects(pt, 12, 11, 2, 1));
                clusters[ 9] = clusters[10] || (bLev1 && Global.PointInRects(pt, 9, 13, 5, 7, 14, 14, 1, 6));
				clusters[ 8] = clusters[ 9] || (bLev2 && Global.PointInRects(pt, 5, 16, 2, 4));
				clusters[ 7] = clusters[ 8] || (bLev2 && Global.PointInRects(pt, 0, 0, 1, 2, 1, 0, 1, 3, 2, 1, 1, 3, 3, 2, 1, 3, 4, 3, 1, 3, 5, 4, 1, 3, 6, 5, 1, 3, 7, 6, 1, 3, 8, 7, 1, 3, 9, 8, 1, 3, 10, 9, 1, 1));
				clusters[ 6] = clusters[ 7] || (bLev2 && Global.PointInRects(pt, 0, 9, 1, 11, 1, 18, 4, 2));
				clusters[ 5] = clusters[ 6] || (bLev3 && Global.PointInRects(pt, 11, 14, 1, 2, 12, 13, 6, 2, 13, 12, 6, 1, 18, 10, 1, 3, 14, 11, 3, 1, 12, 10, 3, 1, 13, 9, 2, 1, 14, 8, 1, 1));
				clusters[ 4] = clusters[ 5] || (bLev1 && Global.PointInRects(pt, 0, 19, 6, 1, 0, 16, 7, 3, 0, 13, 3, 3, 3, 12, 1, 2, 4, 13, 1, 2, 5, 12, 1, 2, 6, 13, 1, 2, 7, 12, 1, 2, 8, 12, 1, 3));
				clusters[ 3] = clusters[ 4] || (bLev2 && Global.PointInRects(pt, 7, 18, 6, 2, 10, 14, 3, 6));
				clusters[ 2] = clusters[ 3] || (bLev1 && Global.PointInRects(pt, 15, 17, 5, 3, 16, 16, 4, 1));
				clusters[ 1] = clusters[ 2] || (bLev3 && Global.PointInRects(pt, 18, 13, 1, 5, 16, 15, 4, 1));
				clusters[ 0] = clusters[ 1] || (bLev1 && Global.PointInRects(pt, 14, 2, 3, 1, 15, 11, 1, 4));
                for (int i = 0; i < 16; i++)
                    Cube.AddObj(clusters[i]);
                Cube.AddObj(bOil);
                for (int i = 17; i < 22; i++)
                    Cube.AddObj(clusters[i - 1]);
                Cube.AddObj(bRump);
                for (int i = 23; i < 30; i++)
                    Cube.AddObj(clusters[i - 2]);
                Cube.AddObj(info.UsedHHG || info.PinPulled != 0);
                Wiz4Item itemHHG = werdna.Inventory.GetItem((int)Wiz4ItemIndex.HHGOfAuntyOck) as Wiz4Item;
                Cube.AddObj(info.UsedHHG || (info.PinPulled != 0 && (itemHHG == null || !itemHHG.Equipped)));
                Cube.AddObj(info.UsedHHG || (info.PinPulled != 0 && itemHHG == null));
                Cube.AddObj(info.UsedHHG || (info.PinPulled != 0 && itemHHG == null && pt != Wiz4.Spots.Cube29UseHHG.Location));
                Cube.AddObj(AboveMap(location.MapIndex, 1));
            }

            Puce.MarkAllWhenComplete = true;
            Puce.AddObj(bOil, werdna.HasItem((int)Wiz4ItemIndex.WitchingRod), werdna.HasItem((int)Wiz4ItemIndex.AromaticBall),
                werdna.HasItem((int)Wiz4ItemIndex.GoldenPyrite), werdna.HasItem((int)Wiz4ItemIndex.BladeCusinart),
                werdna.HasItem((int)Wiz4ItemIndex.MagiciansHat));
            Puce.AddPost(werdna.HasItem((int)Wiz4ItemIndex.DabOfPuce) || werdna.HasItem((int)Wiz4ItemIndex.ArrowOfTruth));

            L1Trebor.MarkAllWhenComplete = true;
            L1Trebor.AddObj(bRump);
            L1Trebor.AddPost(info.CurseLifted);

            bool bEndGame = state.Main == MainState.EndGame;
            bool bKey = werdna.HasItem((int)Wiz4ItemIndex.InnKey);
            bool bOrb = werdna.HasItem((int)Wiz4ItemIndex.OrbOfDreams);
            bool bGood = werdna.Alignment == WizAlignment.Good;

            GoodEnding.MarkAllWhenComplete = true;
            GoodEnding.AddObj(werdna.Alignment != WizAlignment.Evil,
                bKey,
                werdna.HasItem((int)Wiz4ItemIndex.ArrowOfTruth),
                bOrb || werdna.HasItem((int)Wiz4ItemIndex.Pennonceaux),
                bOrb,
                bGood || werdna.HasItem((int)Wiz4ItemIndex.MaintenanceCap),
                bGood || werdna.HasEquipped((int)Wiz4ItemIndex.MaintenanceCap),
                bGood || info.FountainFixed,
                bGood,
                monsters.All(m => m == -1),
                werdna.HasItem((int)Wiz4ItemIndex.CrystalRose),
                werdna.HasEquipped((int)Wiz4ItemIndex.CrystalRose),
                info.CrystalRose,
                werdna.HasItem((int)Wiz4ItemIndex.RallyingHorn),
                werdna.HasItem((int)Wiz4ItemIndex.SignetRing),
                false);  // not sure about this one yet
            GoodEnding.AddPost(bEndGame);
            
            bool bCastle1 = location.MapIndex == (int) Wiz4Map.CastleFirstFloor;
            bool bCastle2 = location.MapIndex == (int) Wiz4Map.CastleSecondFloor;
            bool bCastle3 = location.MapIndex == (int) Wiz4Map.CastleThirdFloor;

            bool bWrist = werdna.HasItem((int)Wiz4ItemIndex.HolyLimpWrist);
            bool bGlove = werdna.HasItem((int)Wiz4ItemIndex.MythrilGlove);
            bool bDink = monsters.Any(m => m == (int) Wiz4MonsterIndex.Dink);
            bool bVonHalstern = (bCastle1 && Global.PointInRects(pt, 12, 9, 2, 1)) ||
                (bCastle2 && Global.PointInRects(pt, 7, 9, 6, 5)) ||
                (bCastle3 && Global.PointInRects(pt, 3, 9, 9, 5));
            bool bSoftalk = (bCastle2 && Global.PointInRects(pt, 7, 9, 2, 1)) ||
                (bCastle3 && Global.PointInRects(pt, 3, 9, 9, 5));
            bool bPriests = bCastle3 && (Global.PointInRects(pt, 3, 9, 3, 5, 6, 12, 1, 2));
            bool bHawkwind = bCastle3 && (Global.PointInRects(pt, 5, 12, 2, 2));
            bool bGloveEquipped = werdna.HasEquipped((int) Wiz4ItemIndex.MythrilGlove);
            bool bKrisEquipped = werdna.HasEquipped((int)Wiz4ItemIndex.KrisOfTruth);
            bool bSwordEquipped = werdna.HasEquipped((int) Wiz4ItemIndex.DragonsClaw) ||
                werdna.HasEquipped((int) Wiz4ItemIndex.EastWindSword) ||
                werdna.HasEquipped((int) Wiz4ItemIndex.WestWindSword) ||
                bKrisEquipped;

            EvilEnding.MarkAllWhenComplete = true;
            EvilEnding.AddObj(werdna.Alignment == WizAlignment.Evil,
                bWrist, bGlove, bKey, bDink,
                bVonHalstern, bSoftalk, bPriests, bHawkwind, bGloveEquipped, bSwordEquipped);
            EvilEnding.AddPost(bEndGame);

            bool bVoid = werdna.HasItem((int)Wiz4ItemIndex.VoidTransducer);
            bool bKris = werdna.HasItem((int)Wiz4ItemIndex.KrisOfTruth);
            bool bStart = location.IsAt(Wiz4.Spots.StartingPosition);
            bool bLev11 = location.MapIndex == (int)Wiz4Map.L11Grandmaster;
            bool bVoidEquipped = werdna.HasEquipped((int)Wiz4ItemIndex.VoidTransducer);

            GrandmasterEnding.MarkAllWhenComplete = true;
            GrandmasterEnding.AddObj(bWrist, bGlove, bKey, bVonHalstern || bVoid || bKris, bSoftalk || bVoid || bKris, bVoid || bKris,
                (bVoid && bStart) || bKris || bLev11, bLev11 || bKris, bVoidEquipped || bKris, bKris, bKris && bDink, bKris & bVonHalstern, bKris && bSoftalk, bPriests && bKris,
                bHawkwind && bKris, bGloveEquipped && bKris, bKrisEquipped);
            GrandmasterEnding.AddPost(bEndGame);

            Level.AddObj(iLevel > 0, iLevel > 0, iLevel > 1, iLevel > 2, iLevel > 3, iLevel > 4, iLevel > 4, iLevel > 4, iLevel > 5, iLevel > 6, iLevel > 6, iLevel > 6,
                iLevel > 7, iLevel > 7, iLevel > 7, iLevel > 7, iLevel > 7, iLevel > 7, iLevel > 8, iLevel > 8, iLevel > 8, iLevel > 8, iLevel > 8, iLevel > 8,
                iLevel > 9, iLevel > 9, iLevel > 9, iLevel > 9, iLevel > 9, iLevel > 9, iLevel > 9);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}