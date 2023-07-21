using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Wiz5QuestData : WizQuestData
    {
        public byte[] Bits;
        public byte[] Aux3;
        public int SummonedMonster;

        public bool IsSet(int iLevel, int iBit) { return iLevel < 1 ? false : Global.GetBit(Bits, (iLevel - 1) * 64 + iBit, true) == 1; }
        public bool HasFlag3(int iScript, int iFlag) { return (BitConverter.ToInt16(Aux3, iScript * 2) & iFlag) > 0; }

        public Wiz5QuestData(WizPartyInfo party, LocationInformation location, WizGameState state, byte[] bits, byte[] aux3, int summonedMonster)
            : base(party, location, null, state)
        {
            Bits = bits;
            Aux3 = aux3;
            SummonedMonster = summonedMonster;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            Global.WriteBytes(stream, Bits);
            Global.WriteBytes(stream, Aux3);
            stream.WriteByte((byte) SummonedMonster);
            stream.WriteByte((byte)(State.Main == MainState.Question ? 1 : 0));
        }
    }

    public class Wiz5Quest : BasicQuest
    {
        public Wiz5Quest()
        {
        }
    }

    public class Wiz5QuestInfo : QuestInfo
    {
        public QuestStatus Orb = new QuestStatus(QuestStatus.Basic.NotStarted, "Recover the Orb of Llylgamyn");
        public QuestStatus AccessLevel2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to the level 2 stairway");
        public QuestStatus RubyWarlock = new QuestStatus(QuestStatus.Basic.NotStarted, "Deal with the Ruby Warlock");
        public QuestStatus AccessLevel3 = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to the level 3 stairway");
        public QuestStatus JeweledScepter = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Jeweled Scepter");
        public QuestStatus MunkeWand = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Munke Wand");
        public QuestStatus AccessLevel4 = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to the level 4 stairway");
        public QuestStatus LarkInCage = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Lark in a Cage");
        public QuestStatus JackOfSpades = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Jack of Spades");
        public QuestStatus AccessLevel6 = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to the level 6 stairway");
        public QuestStatus MightyYog = new QuestStatus(QuestStatus.Basic.NotStarted, "Free The Mighty Yog");
        public QuestStatus QueenOfHearts = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Queen of Hearts");
        public QuestStatus KingOfDiamonds = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the King of Diamonds");
        public QuestStatus StaffOfFire = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Fire");
        public QuestStatus StaffOfEarth = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Earth");
        public QuestStatus StaffOfAir = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Air");
        public QuestStatus StaffOfWater = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Water");
        public QuestStatus LordOfHearts = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the puzzle of the Lord of Hearts");
        public QuestStatus LordOfSpades = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the puzzle of the Lord of Spades");
        public QuestStatus LordOfDiamonds = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the puzzle of the Lord of Diamonds");
        public QuestStatus LordOfClubs = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the puzzle of the Lord of Clubs");
        public QuestStatus HeartOfAbriel = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the SORN and return the Heart of Abriel");
        public QuestStatus EncounterEnemies = new QuestStatus(QuestStatus.Basic.NotStarted, "Encounter particular enemies");
        public QuestStatus EncounterNPCs = new QuestStatus(QuestStatus.Basic.NotStarted, "Encounter particular NPCs");
        public QuestStatus ObtainItems = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain particular items");
        public QuestStatus RestoreHP = new QuestStatus(QuestStatus.Basic.NotStarted, "Restore hit points");
        public QuestStatus Conditions = new QuestStatus(QuestStatus.Basic.NotStarted, "Remove adverse conditions");
        public QuestStatus RestoreSP = new QuestStatus(QuestStatus.Basic.NotStarted, "Restore spell points");
        public QuestStatus MaxHP = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your maximum hit points");
        public QuestStatus Aging = new QuestStatus(QuestStatus.Basic.NotStarted, "Reverse the effects of aging");
        public QuestStatus Statistics = new QuestStatus(QuestStatus.Basic.NotStarted, "Improve your primary statistics");

        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { Orb, AccessLevel2, RubyWarlock, AccessLevel3, JeweledScepter, MunkeWand, 
            AccessLevel4, LarkInCage, JackOfSpades, AccessLevel6, MightyYog, QueenOfHearts, KingOfDiamonds, StaffOfFire, StaffOfEarth, StaffOfAir, StaffOfWater, 
            LordOfHearts, LordOfSpades, LordOfDiamonds, LordOfClubs, HeartOfAbriel, EncounterEnemies, EncounterNPCs, ObtainItems, RestoreHP, Conditions, RestoreSP,
            MaxHP, Aging, Statistics }; }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<Wiz5Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<Wiz5Quest> quests, string strReward = "", string strPath = "")
        {
            Wiz5Quest quest = GetQuest(status, BasicQuestType.Side, null, null, strReward) as Wiz5Quest;
            if (!String.IsNullOrWhiteSpace(strPath))
                quest.Path = strPath;
            quests.Add(quest);
            return quest;
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<Wiz5Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<Wiz5Quest> quests, string strReward = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<Wiz5Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            Wiz5Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as Wiz5Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<Wiz5Quest> quests = new List<Wiz5Quest>();

            int iMainIndex = 1;

            Orb.AddLocations(new QuestLocation("Unlock the door", Wiz5.Spots.L1_968C_Locked),
                new QuestLocation("Search the room to obtain the Orb of Llylgamyn", Wiz5.Spots.L1_938A_Orb));
            BasicQuest OrbQuest = AddMainQuest(iMainIndex++, Orb, quests);

            AccessLevel2.AddLocations(new QuestLocation("Unlock the door", Wiz5.Spots.L1_9D8E_Locked),
                new QuestLocation("Inspect hidden items to obtain the Silver Key", Wiz5.Spots.L1_9C8F_SilverKey),
                new QuestLocation("Use the Silver Key to unlock the Silver Door", Wiz5.Spots.L1_869C_Silver));
            AccessLevel2.Postrequisites.Add(new QuestLocation("Access the room containing the stairs to level 2", Wiz5.Spots.L1_889D_Down));
            AddMainQuest(iMainIndex++, AccessLevel2, quests);

            RubyWarlock.AddLocations(new QuestLocation("Answer the statue's riddle (\"Vampire\")", Wiz5.Spots.L1_959C_Vampire),
                new QuestLocation("Purchase the Brass Key from Ironose (300 Gold)", Wiz5.Spots.L1_969C_Ironose),
                new QuestLocation("Use the Brass Key to unlock the door", Wiz5.Spots.L1_8784_MaintDoor),
                new QuestLocation("Press D, B, C, A to stop the conveyor belt", Wiz5.Spots.L1_8782_Conveyor),
                new QuestLocation("Unlock the door", Wiz5.Spots.L1_8384_Locked),
                new QuestLocation("Defeat the Zombies to obtain the Bag of Tokens", Wiz5.Spots.L1_8585_Zombie),
                new QuestLocation("Use the Bag of Tokens to activate the Castle Transport", Wiz5.Spots.L1_8D85_Portal),
                new QuestLocation("Search the crate to obtain the Bottle of Rum", Wiz5.Spots.L2_7E73_Rum));
            RubyWarlock.Postrequisites.Add(new QuestLocation("Give the Bottle of Rum to the Ruby Warlock", Wiz5.Spots.L2_7A84_Ruby));
            AddMainQuest(iMainIndex++, RubyWarlock, quests);

            AccessLevel3.AddLocations(new QuestLocation("Unlock the door", Wiz5.Spots.L2_8187_Locked),
                new QuestLocation("Inspect hidden items to obtain the Hacksaw", Wiz5.Spots.L2_8584_Hacksaw),
                new QuestLocation("Use the Hacksaw to cut through the chains", Wiz5.Spots.L2_8372_Chains));
            AccessLevel3.Postrequisites.Add(new QuestLocation("Access the area containing the stairs to level 3", Wiz5.Spots.L2_8D73_Down));
            AddMainQuest(iMainIndex++, AccessLevel3, quests);

            JeweledScepter.AddLocations(new QuestLocation("Defeat the Guardian", Wiz5.Spots.L2_8988_Guardian),
                new QuestLocation("Unlock the door", Wiz5.Spots.L2_8983_Locked),
                new QuestLocation("Press A, C, F to purchase a potion of Spirit-Away (250 Gold)", Wiz5.Spots.L2_8988_Guardian));
            JeweledScepter.Postrequisites.Add(new QuestLocation("Use the Potion of Spirit-Away to obtain the Jeweled Scepter", Wiz5.Spots.L2_8581_Scepter));
            AccessLevel4.PreQuest.Add(AddMainQuest(iMainIndex++, JeweledScepter, quests));

            MunkeWand.AddLocations(new QuestLocation("Purchase a Rubber Duck from the Mad Stomper (6000 Gold)", Wiz5.Spots.L3_8772_Stomper));
            MunkeWand.Postrequisites.Add(new QuestLocation("Give the Rubber Duck to the Duck of Sparks to obtain the Munke Wand", Wiz5.Spots.L2_8A8B_Duck));
            StaffOfEarth.PreQuest.Add(AddMainQuest(iMainIndex++, MunkeWand, quests));

            AccessLevel4.AddLocations(new QuestLocation("Equip and use the Jeweled Scepter to open the temple door", Wiz5.Spots.L3_8D86_Hienmitey),
                new QuestLocation("Defeat the Dejin Wind King to obtain the Blue Candle", Wiz5.Spots.L3_8D89_Dejin),
                new QuestLocation("Use the Blue Candle to reveal the secret door", Wiz5.Spots.L3_8D68_Candle));
            AccessLevel4.Postrequisites.Add(new QuestLocation("Access the room containing the stairs to level 4", Wiz5.Spots.L3_8D65_Down));
            AddMainQuest(iMainIndex++, AccessLevel4, quests);

            LarkInCage.AddLocations(new QuestLocation("Inspect hidden items to obtain the Battery", Wiz5.Spots.L4_8C7A_Battery),
                new QuestLocation("Unlock the door", Wiz5.Spots.L3_9873_Locked),
                new QuestLocation("Dive to level H to obtain the Gold Key", Wiz5.Spots.L3_9B73_Pool),
                new QuestLocation("Use the Battery and press C, D, E, G to obtain the Pocketwatch", Wiz5.Spots.L3_878A_Timeless),
                new QuestLocation("Search the disc to activate the first access gate", Wiz5.Spots.L4_7660_Access),
                new QuestLocation("Search the disc to activate the second access gate", Wiz5.Spots.L4_7862_Access),
                new QuestLocation("Search the disc to activate the third access gate", Wiz5.Spots.L4_705F_Access),
                new QuestLocation("Search the disc to activate the fourth access gate", Wiz5.Spots.L4_7065_Access),
                new QuestLocation("Search the disc to activate the fifth access gate", Wiz5.Spots.L4_7565_Access),
                new QuestLocation("Search the disc to activate the sixth access gate", Wiz5.Spots.L4_7968_Access),
                new QuestLocation("Search the disc to activate the seventh access gate", Wiz5.Spots.L4_7163_Access),
                new QuestLocation("Use the Gold Key to enter Ye Gold Vault", Wiz5.Spots.L4_726C_Gold),
                new QuestLocation("Defeat the Gold Statues to use the mysterious tunnel", Wiz5.Spots.L4_796C_Statues),
                new QuestLocation("Dive to level J to obtain the Skeleton Key", Wiz5.Spots.L4_8A78_Pool),
                new QuestLocation("Answer the totem's riddle (\"Time\")", Wiz5.Spots.L4_8069_Time),
                new QuestLocation("Use the Skeleton Key to unlock The Loon's door", Wiz5.Spots.L4_8569_Skeleton),
                new QuestLocation("Use the Pocketwatch to resurrect The Loon", Wiz5.Spots.L4_8369_Loon));
            LarkInCage.Postrequisites.Add(new QuestLocation("Purchase the Lark in a Cage from The Loon (10000 Gold)", Wiz5.Spots.L4_8369_Loon));
            AddMainQuest(iMainIndex++, LarkInCage, quests);

            JackOfSpades.AddLocations(new QuestLocation("Unlock the door", Wiz5.Spots.L3_8273_Locked),
                new QuestLocation("Dive to level H and defeat Makara to obtain the Petrified Demon", Wiz5.Spots.L3_7F73_Fountain),
                new QuestLocation("Equip and use the Petrified Demon to reveal a secret door", Wiz5.Spots.L4_8770_Demon));
            JackOfSpades.Postrequisites.Add(new QuestLocation("Defeat the Copper Demon and Sly Nymph to obtain the Jack of Spades", Wiz5.Spots.L4_8C71_Jack));
            LordOfSpades.PreQuest.Add(AddMainQuest(iMainIndex++, JackOfSpades, quests));

            AccessLevel6.AddLocations(new QuestLocation("Purchase or steal Tickets from Big Max (4375 Gold)", Wiz5.Spots.L5_8880_BigMax));
            AddMainQuest(iMainIndex++, AccessLevel6, quests);

            MightyYog.AddLocations(new QuestLocation("Steal the Gold Medallion from Evil Eyes", Wiz5.Spots.L6_797F_EvilEyes));
            MightyYog.Postrequisites.Add(new QuestLocation("Use the Gold Medallion to revive The Mighty Yog", Wiz5.Spots.L6_8F7C_MightyYog));
            AddSideQuest(MightyYog, quests);

            QueenOfHearts.AddLocations(new QuestLocation("Dive to level N and defeat Lady Neptune to obtain the Queen of Hearts", Wiz5.Spots.None));
            LordOfHearts.PreQuest.Add(AddMainQuest(iMainIndex++, QueenOfHearts, quests));

            KingOfDiamonds.AddLocations(new QuestLocation("Inspect hidden items and search the sarcophagus to obtain the Ice Key", Wiz5.Spots.L6_817F_IceKey),
                new QuestLocation("Press G, D, A, F, E, B, C to repair Yog's Ice Capades", Wiz5.Spots.L6_886F_IceCapades),
                new QuestLocation("Use Yog's Ice Capades", Wiz5.Spots.L6_886F_IceCapades),
                new QuestLocation("Use the Ice Key to open the cavern", Wiz5.Spots.L6_8668_Cavern));
            KingOfDiamonds.Postrequisites.Add(new QuestLocation("Defeat the Robuna Ice King to obtain the King of Diamonds", Wiz5.Spots.L6_8968_King));
            LordOfDiamonds.PreQuest.Add(AddMainQuest(iMainIndex++, KingOfDiamonds, quests));

            StaffOfFire.AddLocations(new QuestLocation("Defeat the Kanzi Fire King to obtain the Lightning Rod", Wiz5.Spots.L7_8E89_Kanzi),
                new QuestLocation("Disarm the lightning trap by holding the Lightning Rod", Wiz5.Spots.L7_8E7D_Lightning));
            StaffOfFire.Postrequisites.Add(new QuestLocation("Defeat the Zana Fire Queen to obtain the Staff of Fire", Wiz5.Spots.L7_8E78_FireStaff));
            AddMainQuest(iMainIndex++, StaffOfFire, quests);

            StaffOfEarth.AddLocations(new QuestLocation("Stop the swinging monkeys by holding the Munke Wand", Wiz5.Spots.L7_8074_Monkey));
            StaffOfEarth.Postrequisites.Add(new QuestLocation("Defeat Kong and Fay to obtain the Staff of Earth", Wiz5.Spots.L7_7C74_EarthStaff));
            AddMainQuest(iMainIndex++, StaffOfEarth, quests);

            StaffOfAir.AddLocations(new QuestLocation("Stop howling wind by holding the Lark in a Cage", Wiz5.Spots.L7_7480_Wind));
            StaffOfAir.Postrequisites.Add(new QuestLocation("Answer the Phoenix's riddle (\"Life\")", Wiz5.Spots.L7_757A_AirStaff));
            AddMainQuest(iMainIndex++, StaffOfAir, quests);

            StaffOfWater.AddLocations(new QuestLocation("Dive to level H and defeat Dragonfinn to obtain the Staff of Water", Wiz5.Spots.L7_8B8D_Pool));
            AddMainQuest(iMainIndex++, StaffOfWater, quests);

            LordOfSpades.PreQuest.Add(OrbQuest);
            LordOfSpades.AddLocations(new QuestLocation("Use the Jack of Spades to get past the Lord of Spades", Wiz5.Spots.L7_7B86_LordSpades),
                new QuestLocation("Use the Orb of Llylgamyn to open the red portal", Wiz5.Spots.L7_8180_Red),
                new QuestLocation("Defeat the clones of the first four party members", Wiz5.Spots.L8_7B81_Clones),
                new QuestLocation("Equip and use the Staff of Earth to open the red sphere", Wiz5.Spots.L8_7D81_Red),
                new QuestLocation("Press A, D, I to light the candles of the aspect of earth", Wiz5.Spots.L8_7D81_Red));
            LordOfSpades.Postrequisites.Add(new QuestLocation("Answer the red face's riddle (\"Nature\")", Wiz5.Spots.L8_7D81_Red));
            LordOfHearts.PreQuest.Add(AddMainQuest(iMainIndex++, LordOfSpades, quests));

            LordOfHearts.AddLocations(new QuestLocation("Use the Queen of Hearts to get past the Lord of Hearts", Wiz5.Spots.L7_8686_LordHearts),
                new QuestLocation("Use the Orb of Llylgamyn to open the blue portal", Wiz5.Spots.L7_8080_Blue),
                new QuestLocation("Defeat the clones of the first four party members", Wiz5.Spots.L8_817B_Clones),
                new QuestLocation("Equip and use the Staff of Water to open the blue sphere", Wiz5.Spots.L8_817D_Blue),
                new QuestLocation("Press B, E, H to light the candles of the aspect of water", Wiz5.Spots.L8_817D_Blue));
            LordOfHearts.Postrequisites.Add(new QuestLocation("Answer the blue face's riddle (\"Growth\")", Wiz5.Spots.L8_817D_Blue));
            LordOfDiamonds.PreQuest.Add(AddMainQuest(iMainIndex++, LordOfHearts, quests));

            LordOfDiamonds.AddLocations(new QuestLocation("Use the King of Diamonds to get past the Lord of Diamonds", Wiz5.Spots.L7_7B7B_LordDiamonds),
                new QuestLocation("Use the Orb of Llylgamyn to open the yellow portal", Wiz5.Spots.L7_8181_Yellow),
                new QuestLocation("Defeat the clones of the first four party members to obtain the Ace of Clubs", Wiz5.Spots.L8_8781_Clones),
                new QuestLocation("Equip and use the Staff of Fire to open the yellow sphere", Wiz5.Spots.L8_8581_Yellow),
                new QuestLocation("Press C, F, G to light the candles of the aspect of fire", Wiz5.Spots.L8_8581_Yellow));
            LordOfDiamonds.Postrequisites.Add(new QuestLocation("Answer the yellow face's riddle (\"Change\")", Wiz5.Spots.L8_8581_Yellow));
            LordOfClubs.PreQuest.Add(AddMainQuest(iMainIndex++, LordOfDiamonds, quests));

            LordOfClubs.AddLocations(new QuestLocation("Use the Ace of Clubs to get past the Lord of Clubs", Wiz5.Spots.L7_867B_LordClubs),
                new QuestLocation("Use the Orb of Llylgamyn to open the white portal", Wiz5.Spots.L7_8081_White),
                new QuestLocation("Defeat the clones of the first four party members", Wiz5.Spots.L8_8187_Clones),
                new QuestLocation("Equip and use the Staff of Air to open the white sphere", Wiz5.Spots.L8_8185_White),
                new QuestLocation("Press A, B, C, D, E, F, G, H, I to light the candles of the aspect of air", Wiz5.Spots.L8_8185_White));
            LordOfClubs.Postrequisites.Add(new QuestLocation("Answer the white face's riddle (\"Man\")", Wiz5.Spots.L8_8185_White));
            AddMainQuest(iMainIndex++, LordOfClubs, quests);

            HeartOfAbriel.AddLocations(new QuestLocation("Cast Socordi or Bamordi to summon The Gatekeeper", Wiz5.Spots.L8_8181_Sorn),
                new QuestLocation("Defeat The S*O*R*N to obtain the Heart of Abriel", Wiz5.Spots.L8_8181_Sorn));
            HeartOfAbriel.Postrequisites.Add(new QuestLocation("Return to the Castle with the Heart of Abriel", Wiz5.Spots.Castle));
            AddMainQuest(iMainIndex++, HeartOfAbriel, quests);

            EncounterEnemies.AddLocations(new QuestLocation("Golem (6552 Exp)", Wiz5.Spots.L1_9098_Golem),
                new QuestLocation("Werebat (976 Exp), Black Bat (184 Exp)", Wiz5.Spots.L1_959C_Vampire),
                new QuestLocation("Zombie (1020 Exp)", Wiz5.Spots.L1_8585_Zombie),
                new QuestLocation("LaLa Moo-Moo (790401 Exp)", Wiz5.Spots.L1_1818_Lala),
                new QuestLocation("The Guardian (7268 Exp)", Wiz5.Spots.L2_8988_Guardian),
                new QuestLocation("Hurkle Beast (2844 Exp)", Wiz5.Spots.L2_8279_Portal),
                new QuestLocation("Makara (14960 Exp), Sea Cobra (720 Exp)", Wiz5.Spots.L3_7F73_Fountain),
                new QuestLocation("The Dejin Wind King (22358 Exp)", Wiz5.Spots.L3_8D89_Dejin),
                new QuestLocation("Nessie (612 Exp), Loch Babies (612 Exp)", Wiz5.Spots.L4_8A78_Pool),
                new QuestLocation("Copper Demon (36348 Exp), Sly Nymph (1358 Exp)", Wiz5.Spots.L4_8C71_Jack),
                new QuestLocation("Gold Statue (9424 Exp)", Wiz5.Spots.L4_796C_Statues),
                new QuestLocation("Royal Guard (1232 Exp), Royal Lady (1184 Exp)", Wiz5.Spots.L5_8D8F_Lady),
                new QuestLocation("Royal Lord (2128 Exp)", Wiz5.Spots.L5_8382_Lord),
                new QuestLocation("Werewolf (2788 Exp)", Wiz5.Spots.L5_8275_Werewolf),
                new QuestLocation("Horbule (5520 Exp)", Wiz5.Spots.L6_817F_IceKey),
                new QuestLocation("Lady Neptune (66675 Exp), Triton (2332 Exp)", Wiz5.Spots.L6_7978_Well),
                new QuestLocation("The Robuna Ice King (44782 Exp)", Wiz5.Spots.L6_8968_King),
                new QuestLocation("Dragonfinn (15055 Exp), Water Elemental (17150 Exp)", Wiz5.Spots.L7_8B8D_Pool),
                new QuestLocation("The Kanzi Fire King (32738 Exp)", Wiz5.Spots.L7_8E89_Kanzi),
                new QuestLocation("The Zana Fire Queen (38930 Exp)", Wiz5.Spots.L7_8E78_FireStaff),
                new QuestLocation("Phoenix (122299 Exp)", Wiz5.Spots.L7_757A_AirStaff),
                new QuestLocation("Kong (37827 Exp), Fay (18762 Exp)", Wiz5.Spots.L7_7C74_EarthStaff));
            AddSideQuest(EncounterEnemies, quests);

            EncounterNPCs.AddLocations(new QuestLocation("G'bli Gedook (19104 Exp)", Wiz5.Spots.L1_8999_Gbli),
                new QuestLocation("Ironose (640 Exp)", Wiz5.Spots.L1_969C_Ironose),
                new QuestLocation("The Laughing Kettle (528158 Exp)", Wiz5.Spots.L1_979F_Kettle),
                new QuestLocation("The Ruby Warlock (1792 Exp)", Wiz5.Spots.L2_7A84_Ruby),
                new QuestLocation("The Duck of Sparks (1668 Exp)", Wiz5.Spots.L2_8A8B_Duck),
                new QuestLocation("The Mad Stomper (8690 Exp)", Wiz5.Spots.L3_8772_Stomper),
                new QuestLocation("Lord Hienmitey (1352 Exp)", Wiz5.Spots.L3_8D86_Hienmitey),
                new QuestLocation("The Loon (117880 Exp)", Wiz5.Spots.L4_8369_Loon),
                new QuestLocation("Big Max (16656 Exp)", Wiz5.Spots.L5_8880_BigMax),
                new QuestLocation("The Snatch (3868 Exp)", Wiz5.Spots.L5_8675_Snatch),
                new QuestLocation("Evil Eyes (15086 Exp)", Wiz5.Spots.L6_797F_EvilEyes),
                new QuestLocation("The Mighty Yog (9792 Exp)", Wiz5.Spots.L6_8F7C_MightyYog),
                new QuestLocation("The Lord of Spades (132667 Exp)", Wiz5.Spots.L7_7B86_LordSpades),
                new QuestLocation("The Lord of Hearts (123451 Exp)", Wiz5.Spots.L7_8686_LordHearts),
                new QuestLocation("The Lord of Diamonds (123451 Exp)", Wiz5.Spots.L7_7B7B_LordDiamonds),
                new QuestLocation("The Lord of Clubs (141768 Exp)", Wiz5.Spots.L7_867B_LordClubs));
            AddSideQuest(EncounterNPCs, quests);

            ObtainItems.AddLocations(new QuestLocation("Holy Talisman (25000 Gold)", Wiz5.Spots.L1_8999_Gbli),
                new QuestLocation("Iron Gloves (search)", Wiz5.Spots.L4_7463_IronGloves),
                new QuestLocation("Obtain Ring of Frozz (search)", Wiz5.Spots.L4_7469_Frozz),
                new QuestLocation("Obtain Crested Shield (search)", Wiz5.Spots.L4_7965_Crested),
                new QuestLocation("Purchase Scarlet Robes from Evil Eyes (18000 Gold)", Wiz5.Spots.L6_797F_EvilEyes),
                new QuestLocation("Purchase Emerald Robes from Evil Eyes (18000 Gold)", Wiz5.Spots.L6_797F_EvilEyes),
                new QuestLocation("Purchase Ring of Skulls from Evil Eyes (20000 Gold)", Wiz5.Spots.L6_797F_EvilEyes),
                new QuestLocation("Obtain Potion of Demon-Out (1500 Gold, press E)", Wiz5.Spots.L6_876A_DemonOut));
            AddSideQuest(ObtainItems, quests);

            RestoreHP.AddLocations(new QuestLocation("Gain/Lose 2d8 HP (dive to level A)", Wiz5.Spots.L3_846A_Moat),
                new QuestLocation("Restore all HP (dive to level A)", Wiz5.Spots.L5_6D80_Fountain),
                new QuestLocation("Gain 4d2 HP (dive to level A)", Wiz5.Spots.L7_8B8D_Pool),
                new QuestLocation("Gain/Lose 12d2 HP (dive to level C)", Wiz5.Spots.L7_8B8D_Pool),
                new QuestLocation("Gain 3d8 HP (dive to level C)", Wiz5.Spots.L6_7978_Well),
                new QuestLocation("Restore all HP (dive to level K)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(RestoreHP, quests);

            Conditions.AddLocations(new QuestLocation("Heal minor conditions (dive to level F)", Wiz5.Spots.L3_7F73_Fountain),
                new QuestLocation("Restore all HP, remove minor conditions (dive to level B)", Wiz5.Spots.L5_6D80_Fountain),
                new QuestLocation("Restore all HP, remove minor conditions, resurrect (dive to level C)", Wiz5.Spots.L5_6D80_Fountain),
                new QuestLocation("Remove minor conditions (dive to level L)", Wiz5.Spots.L7_8B8D_Pool),
                new QuestLocation("Remove any adverse conditions (dive to level O)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(Conditions, quests);

            RestoreSP.AddLocations(new QuestLocation("Gain 4/3/2/1 SP (dive to level E)", Wiz5.Spots.L2_7175_Pool),
                new QuestLocation("Gain/Lose 5/4/3/2/1 SP (dive to level C)", Wiz5.Spots.L3_846A_Moat),
                new QuestLocation("Gain 6/10/15/21/28 SP (dive to level B/C/D/E/F)", Wiz5.Spots.L3_9B73_Pool),
                new QuestLocation("Gain/Lose 5/4/3/2/1 SP (dive to level E)", Wiz5.Spots.L6_9880_Bog),
                new QuestLocation("Gain up to 9/8/7/6/5/4/3 SP (dive to level J)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(RestoreSP, quests);

            MaxHP.AddLocations(new QuestLocation("+1d4 MaxHP, -1 Vitality (dive to level G)", Wiz5.Spots.L3_7F73_Fountain),
                new QuestLocation("+1d4 MaxHP, -1 Vitality (dive to level G)", Wiz5.Spots.L3_9B73_Pool),
                new QuestLocation("Gain/Lose 3d2 MaxHP (dive to level I)", Wiz5.Spots.L6_7978_Well),
                new QuestLocation("+3d2 MaxHP, character becomes Dead (dive to level J)", Wiz5.Spots.L6_9880_Bog),
                new QuestLocation("Gain/Lose 1d3 MaxHP (dive to level N)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(MaxHP, quests);

            Aging.AddLocations(new QuestLocation("Gain/Lose 1 year of age (dive to level D)", Wiz5.Spots.L3_846A_Moat),
                new QuestLocation("Gain/Lose 1 year of age (dive to level I)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(Aging, quests);

            Statistics.AddLocations(new QuestLocation("Gain/Lose 1 I.Q. (dive to level E)", Wiz5.Spots.L3_846A_Moat),
                new QuestLocation("+1 Piety, +2 years of age (dive to level H)", Wiz5.Spots.L4_8A78_Pool),
                new QuestLocation("+1 Strength, +1d2 years of age (dive to level L)", Wiz5.Spots.L6_7978_Well),
                new QuestLocation("+Age 1d3+1 years, +1 Agility (dive to level L)", Wiz5.Spots.L6_9880_Bog),
                new QuestLocation("+1/-1 to random statistic (dive to level M)", Wiz5.Spots.L7_8B8D_Pool));
            AddSideQuest(Statistics, quests);

            quests.Sort(CompareWiz5Quests);
            return quests.ToArray();
        }

        public int CompareWiz5Quests(Wiz5Quest quest1, Wiz5Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        private void AddQuestion(QuestStatus qs, bool bOverrideComplete, bool bIncomplete, bool bInQuestion, QuestStatus.Single qsUnavailable)
        {
            if (bOverrideComplete)
                qs.AddObj(true);
            else if (bInQuestion)
                qs.Obj.Add(qsUnavailable);
            else if (bIncomplete)
                qs.Obj.Add(QuestStatus.Single.Incomplete);
            else
                qs.Obj.Add(QuestStatus.Single.Complete);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            Wiz5QuestData wizData = data as Wiz5QuestData;
            if (wizData == null)
                return;

            Wiz5PartyInfo party = data.Party as Wiz5PartyInfo;
            LocationInformation location = data.Location;
            WizGameState state = wizData.State as WizGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            Wiz5Character wiz5Char = Wiz5Character.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize, null, false) as Wiz5Character;

            if (!(data is Wiz5QuestData))
                return;

            Wiz5QuestData questData = data as Wiz5QuestData;

            QuestStatus.Single qsQuestion = QuestStatus.Single.Invalid("The status of this goal is unavailable while answering questions or in menus.");
            bool bQuestion = questData.State.Main == MainState.Question;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = wiz5Char.Name;
            CharAddress = iOverrideCharAddress;

            int iMap = location.MapIndex;
            bool bUnlockedSilverDoor = wizData.IsSet(1, 17);
            bool bOrb = party.CurrentPartyHasItem(Wiz5ItemIndex.OrbOfLlylgamyn);
            bool bSilverKey = party.CurrentPartyHasItem(Wiz5ItemIndex.SilverKey);
            bool bGoldKey = party.CurrentPartyHasItem(Wiz5ItemIndex.GoldKey);
            bool bTokens = party.CurrentPartyHasItem(Wiz5ItemIndex.BagOfTokens);
            bool bWarlock = wizData.IsSet(2, 3);
            bool bHacksaw = party.CurrentPartyHasItem(Wiz5ItemIndex.Hacksaw);
            bool bCutChains = wizData.IsSet(2, 4);
            bool bSpiritPotion = party.CurrentPartyHasItem(Wiz5ItemIndex.PotionOfSpiritAway);
            bool bScepter = party.CurrentPartyHasItem(Wiz5ItemIndex.JeweledScepter);
            bool bUsedCandle = wizData.IsSet(3, 11);
            bool bPocketwatch = party.CurrentPartyHasItem(Wiz5ItemIndex.Pocketwatch);
            bool bLark = party.CurrentPartyHasItem(Wiz5ItemIndex.LarkInACage);
            bool bJack = party.CurrentPartyHasItem(Wiz5ItemIndex.JackOfSpades);
            bool bQueen = party.CurrentPartyHasItem(Wiz5ItemIndex.QueenOfHearts);
            bool bKing = party.CurrentPartyHasItem(Wiz5ItemIndex.KingOfDiamonds);
            bool bAce = party.CurrentPartyHasItem(Wiz5ItemIndex.AceOfClubs);
            bool bDemon = party.CurrentPartyHasItem(Wiz5ItemIndex.PetrifiedDemon);
            bool bUsedDemon = wizData.IsSet(4, 15);
            bool bFireStaff = party.CurrentPartyHasItem(Wiz5ItemIndex.StaffOfFire);
            bool bWaterStaff = party.CurrentPartyHasItem(Wiz5ItemIndex.StaffOfWater);
            bool bEarthStaff = party.CurrentPartyHasItem(Wiz5ItemIndex.StaffOfEarth);
            bool bAirStaff = party.CurrentPartyHasItem(Wiz5ItemIndex.StaffOfAir);
            bool bAbrielHeart = party.CurrentPartyHasItem(Wiz5ItemIndex.HeartOfAbriel);

            AddQuestion(Orb, bOrb, wizData.HasFlag3(67, 0x0010), bQuestion, qsQuestion);
            Orb.AddObj(bOrb);
            AddQuest(totals, Orb);

            AccessLevel2.MarkAllWhenComplete = true;
            AddQuestion(AccessLevel2, bSilverKey || iMap > 1, wizData.HasFlag3(71, 0x0001), bQuestion, qsQuestion);
            AccessLevel2.AddObj(bSilverKey,
                bUnlockedSilverDoor);
            AccessLevel2.AddPost(iMap > 1 || bUnlockedSilverDoor);
            AddQuest(totals, AccessLevel2);

            RubyWarlock.MarkAllWhenComplete = true;
            RubyWarlock.AddObj(wizData.IsSet(1, 9),
                party.CurrentPartyHasItem(Wiz5ItemIndex.BrassKey),
                wizData.IsSet(1, 16),
                wizData.IsSet(1, 2));
            AddQuestion(RubyWarlock, bTokens || bWarlock || iMap > 2, wizData.HasFlag3(36, 0x0004), bQuestion, qsQuestion);
            RubyWarlock.AddObj(bTokens,
                !wizData.IsSet(1, 1),
                party.CurrentPartyHasItem(Wiz5ItemIndex.BottleOfRum));
            RubyWarlock.AddPost(bWarlock);
            AddQuest(totals, RubyWarlock);

            AccessLevel3.MarkAllWhenComplete = true;
            AddQuestion(AccessLevel3, bHacksaw || iMap > 2, wizData.HasFlag3(70, 0x0004), bQuestion, qsQuestion);
            AccessLevel3.AddObj(bHacksaw,
                bCutChains);
            AccessLevel3.AddPost(iMap > 2 || bCutChains);
            AddQuest(totals, AccessLevel3);

            JeweledScepter.MarkAllWhenComplete = true;
            JeweledScepter.AddObj(wizData.IsSet(2, 9));
            AddQuestion(JeweledScepter, bSpiritPotion || bScepter || iMap > 2, wizData.HasFlag3(86, 0x0010), bQuestion, qsQuestion);
            JeweledScepter.AddObj(bSpiritPotion);
            JeweledScepter.AddPost(bScepter);
            AddQuest(totals, JeweledScepter);

            MunkeWand.MarkAllWhenComplete = true;
            MunkeWand.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.RubberDuck));
            MunkeWand.AddPost(party.CurrentPartyHasItem(Wiz5ItemIndex.MunkeWand));
            AddQuest(totals, MunkeWand);

            AccessLevel4.MarkAllWhenComplete = true;
            AccessLevel4.AddObj(wizData.IsSet(3, 1),
                party.CurrentPartyHasItem(Wiz5ItemIndex.BlueCandle),
                bUsedCandle);
            AccessLevel4.AddPost(iMap > 3 || bUsedCandle);
            AddQuest(totals, AccessLevel4);

            LarkInCage.MarkAllWhenComplete = true;
            LarkInCage.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.Battery) || bPocketwatch || bLark);
            AddQuestion(LarkInCage, bGoldKey, iMap == 3 && wizData.HasFlag3(57, 0x0004), bQuestion, qsQuestion);
            LarkInCage.AddObj(bGoldKey,
                bPocketwatch,
                !wizData.IsSet(4, 36),
                !wizData.IsSet(4, 37),
                !wizData.IsSet(4, 38),
                !wizData.IsSet(4, 39),
                !wizData.IsSet(4, 40),
                !wizData.IsSet(4, 41),
                !wizData.IsSet(4, 42),
                wizData.IsSet(4, 7),
                wizData.IsSet(4, 1),
                party.CurrentPartyHasItem(Wiz5ItemIndex.SkeletonKey),
                wizData.IsSet(4, 9),
                wizData.IsSet(4, 11),
                !wizData.IsSet(4, 14));
            LarkInCage.AddPost(bLark);
            AddQuest(totals, LarkInCage);

            JackOfSpades.MarkAllWhenComplete = true;
            AddQuestion(JackOfSpades, bJack, iMap == 3 && wizData.HasFlag3(51, 0x0040), bQuestion, qsQuestion);
            JackOfSpades.AddObj(bDemon,
                bUsedDemon);
            JackOfSpades.AddPost(bJack);
            AddQuest(totals, JackOfSpades);

            AccessLevel6.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.Tickets) || party.CurrentPartyHasItem(Wiz5ItemIndex.TicketStubs));
            AddQuest(totals, AccessLevel6);

            MightyYog.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.GoldMedallion));
            MightyYog.AddPost(!wizData.IsSet(6, 6));
            AddQuest(totals, MightyYog);

            QueenOfHearts.AddObj(bQueen);
            AddQuest(totals, QueenOfHearts);

            KingOfDiamonds.MarkAllWhenComplete = true;
            KingOfDiamonds.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.IceKey),
                !wizData.IsSet(6, 2),
                (iMap == 6 && location.PrimaryCoordinates == Wiz5.Spots.L6_8668_Cavern.Location),
                (iMap == 6 && Global.PointInRects(location.PrimaryCoordinates, new Rectangle(6, -23, 6, 6))));
            KingOfDiamonds.AddPost(bKing);
            AddQuest(totals, KingOfDiamonds);

            StaffOfFire.MarkAllWhenComplete = true;
            StaffOfFire.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.LightningRod),
                (iMap == 7 && Global.PointInRects(location.PrimaryCoordinates, new Rectangle(12, -5, 3, 9))));
            StaffOfFire.AddPost(bFireStaff);
            AddQuest(totals, StaffOfFire);

            StaffOfEarth.MarkAllWhenComplete = true;
            StaffOfEarth.AddObj(iMap == 7 && Global.PointInRects(location.PrimaryCoordinates, new Rectangle(-13, -14, 26, 2), new Rectangle(-11, -11, 9, 3), new Rectangle(2, -11, 9, 3)));
            StaffOfEarth.AddPost(bEarthStaff);
            AddQuest(totals, StaffOfEarth);

            StaffOfAir.MarkAllWhenComplete = true;
            StaffOfAir.AddObj(iMap == 7 && Global.PointInRects(location.PrimaryCoordinates, new Rectangle(-15, 12, 2, 26), new Rectangle(-13, 10, 3, 9), new Rectangle(-13, -3, 3, 9)));
            StaffOfAir.AddPost(bAirStaff);
            AddQuest(totals, StaffOfAir);

            StaffOfWater.AddObj(bWaterStaff);
            AddQuest(totals, StaffOfWater);

            LordOfSpades.MarkAllWhenComplete = true;
            LordOfSpades.AddObj(wizData.IsSet(7, 20),
                !wizData.IsSet(7, 16),
                wizData.IsSet(8, 1),
                wizData.IsSet(8, 2),
                !wizData.IsSet(8, 4));
            LordOfSpades.AddPost(!wizData.IsSet(8, 20));
            AddQuest(totals, LordOfSpades);

            LordOfHearts.MarkAllWhenComplete = true;
            LordOfHearts.AddObj(wizData.IsSet(7, 21),
                !wizData.IsSet(7, 17),
                wizData.IsSet(8, 5),
                wizData.IsSet(8, 6),
                !wizData.IsSet(8, 8));
            LordOfHearts.AddPost(!wizData.IsSet(8, 21));
            AddQuest(totals, LordOfHearts);

            LordOfDiamonds.MarkAllWhenComplete = true;
            LordOfDiamonds.AddObj(wizData.IsSet(7, 22),
                !wizData.IsSet(7, 18),
                wizData.IsSet(8, 9),
                wizData.IsSet(8, 10),
                !wizData.IsSet(8, 12));
            LordOfDiamonds.AddPost(!wizData.IsSet(8, 22));
            AddQuest(totals, LordOfDiamonds);

            LordOfClubs.MarkAllWhenComplete = true;
            LordOfClubs.AddObj(wizData.IsSet(7, 23),
                !wizData.IsSet(7, 19),
                wizData.IsSet(8, 13),
                wizData.IsSet(8, 14),
                !wizData.IsSet(8, 16));
            LordOfClubs.AddPost(!wizData.IsSet(8, 23));
            AddQuest(totals, LordOfClubs);

            HeartOfAbriel.MarkAllWhenComplete = true;
            HeartOfAbriel.AddObj(bAbrielHeart || (iMap == 8 && wizData.State.InCombat && wizData.SummonedMonster == 126),
                bAbrielHeart);
            HeartOfAbriel.AddPost((wiz5Char.Honors & 0x8000) > 0);
            AddQuest(totals, HeartOfAbriel);

            EncounterEnemies.AddObj(22, QuestGoal.Incomplete);
            AddQuest(totals, EncounterEnemies);

            EncounterNPCs.AddObj(16, QuestGoal.Incomplete);
            AddQuest(totals, EncounterNPCs);

            ObtainItems.AddObj(party.CurrentPartyHasItem(Wiz5ItemIndex.HolyTalisman),
                party.CurrentPartyHasItem(Wiz5ItemIndex.IronGloves),
                party.CurrentPartyHasItem(Wiz5ItemIndex.RingOfFrozz),
                party.CurrentPartyHasItem(Wiz5ItemIndex.CrestedShield),
                party.CurrentPartyHasItem(Wiz5ItemIndex.ScarletRobes),
                party.CurrentPartyHasItem(Wiz5ItemIndex.EmeraldRobes),
                party.CurrentPartyHasItem(Wiz5ItemIndex.RingOfSkulls),
                party.CurrentPartyHasItem(Wiz5ItemIndex.PotionOfDemonOut));
            AddQuest(totals, ObtainItems);

            RestoreHP.AddObj(6, QuestGoal.Incomplete);
            AddQuest(totals, RestoreHP);

            Conditions.AddObj(5, QuestGoal.Incomplete);
            AddQuest(totals, Conditions);

            RestoreSP.AddObj(5, QuestGoal.Incomplete);
            AddQuest(totals, RestoreSP);

            MaxHP.AddObj(5, QuestGoal.Incomplete);
            AddQuest(totals, MaxHP);

            Aging.AddObj(2, QuestGoal.Incomplete);
            AddQuest(totals, Aging);

            Statistics.AddObj(5, QuestGoal.Incomplete);
            AddQuest(totals, Statistics);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}