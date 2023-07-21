using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public static class EOB1Bits
    {
        public enum Scripts
        {
            Test1 = 0,
            None = -1,
        }

        public enum Game
        {
            None = -1,
            SQ1Guinsoo = 1,
            SQ2Daggers = 2,
            SQ3Gems = 3,
            SQ4Chain = 4,
            SQ5Rations = 5,
            SQ6Nest = 6,
            SQ7Stone = 7,
            SQ8Darts = 8,
            SQ9Orbs = 9,
            SQ10Eggs = 10,
            SQ11Levers = 11,
            Global12 = 12,
            L4_TalkTaghor = 13,
            L4_HelpTaghor = 14,
            L5_TalkArmun = 15,
            L5_AgreeHelpArmun = 16,
            Global17 = 17,
            L5_AttackedDwarves = 18,
            Global19 = 19,
            L5_HelpedArmun = 20,
            Global21 = 21,
            L7_DealtWithDrow = 22,
            Global23 = 23,
            Global24 = 24,
            Global25 = 25,
            Global26 = 26,
            L7_AttackedDrow = 27,
        }

        public enum Level
        {
            None = -1,
            L1_1314Lever = 1,
            L1_MiscSewer = 3,
            L1_TodUphill = 4,
            L1_MiscButton = 5,
            L1_2521Button = 6,
            L1_MiscPlate = 7,
            L1_2220 = 8,
            L1_ExitDoor = 10,
            L2_1429Dagger = 4,
            L2_2130Dagger = 5,
            L2_2216Dagger = 6,
            L2_0930Dagger = 7,
            L4_AbandonTaghor = 17,
            L4_EncounterTaghor = 18,
            L11_WandOfFrost = 0
        }

        public static bool IsSet(byte[] bytes, Scripts bit) { return bit == Scripts.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }

        public static BitDesc Level1Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level2Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level3Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level4Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level5Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level6Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level7Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level8Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level9Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level10Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level11Description(object val) { return Description((Scripts)val); }
        public static BitDesc Level12Description(object val) { return Description((Scripts)val); }
        public static BitDesc GlobalDescription(object val) { return GlobalBitDescription((Game)val); }

        private const string enter = "Enter the square";
        private const string active = "Activate the square";

        public static BitDesc Description(Scripts bit)
        {
            switch (bit)
            {
                case Scripts.Test1: return new BitDesc("Do something", EOB1.Spots.None, enter);

                default: return BitDesc.Empty;
            }
        }

        public static BitDesc GlobalBitDescription(Game bit)
        {
            EOB1Locations es = EOB1.Spots;

            switch (bit)
            {
                case Game.SQ1Guinsoo: return new BitDesc("Complete Special Quest L1", es.GuinsooAlcoveL1_1225, enter, es.GuinsooAlcoveL1_1225);
                case Game.SQ2Daggers: return new BitDesc("Complete Special Quest L2", es.DaggerCarvingL2_1429Alcove, es.DaggerCarvingL2_0930Alcove, es.DaggerCarvingL2_2130Alcove, es.DaggerCarvingL2_2216Alcove).AddChecked(
                    active, es.DaggerCarvingL2_0930Alcove, es.DaggerCarvingL2_1429Alcove, es.DaggerCarvingL2_2130Alcove, es.DaggerCarvingL2_2216Alcove);
                case Game.SQ3Gems: return new BitDesc("Complete Special Quest L3", es.Socket_L3_0510, es.Socket_L3_1106, es.Socket_L3_0510, es.Socket_L3_1011).AddChecked(
                    active, es.Socket_L3_0510Alcove, es.Socket_L3_1106Alcove, es.Socket_L3_0510Alcove, es.Socket_L3_1011Alcove);
                case Game.SQ4Chain: return new BitDesc("Complete Special Quest L4", es.Chain_L4_1806).AddChecked(active, es.Chain_L4_1806Alcove);
                case Game.SQ5Rations: return new BitDesc("Complete Special Quest L5", es.RationsQuest_L5_1013).AddChecked(active, es.RationsQuest_Alcove_L5_1014);
                case Game.SQ6Nest: return new BitDesc("Complete Special Quest L6", es.KenkuNest_L6_1317).AddChecked(active, es.KenkuNest_L6_1317);
                case Game.SQ7Stone: return new BitDesc("Complete Special Quest L7", es.StoneAlcove_L7_1231, es.StoneAlcove_L7_1331, es.StoneAlcove_L7_1431).AddChecked(
                    active, es.StoneAlcove_L7_1231, es.StoneAlcove_L7_1331, es.StoneAlcove_L7_1431);
                case Game.SQ8Darts: return new BitDesc("Complete Special Quest L8", es.DartAlcove_L8_2402, es.DartAlcove_L8_2502, es.DartAlcove_L8_2602, es.DartAlcove_L8_2702,
                    es.DartAlcove_L8_2802, es.DartAlcove_L8_2903, es.DartAlcove_L8_2904, es.DartAlcove_L8_2405, es.DartAlcove_L8_2505, es.DartAlcove_L8_2605, es.DartAlcove_L8_2705, es.DartAlcove_L8_2805).AddChecked(
                    active, es.DartAlcove_L8_2402, es.DartAlcove_L8_2502, es.DartAlcove_L8_2602, es.DartAlcove_L8_2702, es.DartAlcove_L8_2802, es.DartAlcove_L8_2903,
                    es.DartAlcove_L8_2904, es.DartAlcove_L8_2405, es.DartAlcove_L8_2505, es.DartAlcove_L8_2605, es.DartAlcove_L8_2705, es.DartAlcove_L8_2805);
                case Game.SQ9Orbs: return new BitDesc("Complete Special Quest L9", es.MissileWall_L9_2218).AddChecked(active, es.Button_L9_2518);
                case Game.SQ10Eggs: return new BitDesc("Complete Special Quest L10", es.None);
                case Game.SQ11Levers: return new BitDesc("Complete Special Quest L11", es.None);
                case Game.Global12: return new BitDesc("?", es.None);
                case Game.L4_TalkTaghor: return new BitDesc("Talk to Taghor", es.None);
                case Game.L4_HelpTaghor: return new BitDesc("Choose to help Taghor", es.Taghor_L4_1613, enter, es.Taghor_L4_1613).AddChecked(enter, es.AbandonTaghor_L4_1621);
                case Game.L5_TalkArmun: return new BitDesc("Talk to Armun", es.None);
                case Game.L5_AgreeHelpArmun: return new BitDesc("Agree to help Armun", es.None);
                case Game.Global17: return new BitDesc("?", es.None);
                case Game.L5_AttackedDwarves: return new BitDesc("Attack any dwarves", es.None);
                case Game.Global19: return new BitDesc("?", es.None);
                case Game.L5_HelpedArmun: return new BitDesc("Help Armun", es.None);
                case Game.Global21: return new BitDesc("?", es.None);
                case Game.L7_DealtWithDrow: return new BitDesc("Deal with the Drow", es.None);
                case Game.Global23: return new BitDesc("?", es.None);
                case Game.Global24: return new BitDesc("?", es.None);
                case Game.Global25: return new BitDesc("?", es.None);
                case Game.Global26: return new BitDesc("?", es.None);
                case Game.L7_AttackedDrow: return new BitDesc("Attack the Drow", es.None);
                default: return BitDesc.Empty;
            }
        }
    }

    public class EOB1QuestData : EOBQuestData
    {
        public EOB1Effects Effects;
        public byte[] QuestBits;

        public EOB1QuestData(EOBPartyInfo party, LocationInformation location, EOBGameState state, byte[] questBits, EOB1MapData mapData, EOB1Effects effects, EOB1GameInfo gameInfo)
            : base(party, location, state, null, null, gameInfo)
        {
            Map = mapData;
            Effects = effects;
            QuestBits = questBits;
            Info = gameInfo;
        }

        public bool IsActive(Point pt)
        {
            return false;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            EOB1MapData data = Map as EOB1MapData;
            if (data != null)
            {
                // Record changes in the wall types and the item list head, but not
                // the number of monsters (changes frequently and isn't relevant to quests)
                // That means skipping byte index 4
                for (int iOffset = 0; iOffset < data.Squares.Length; iOffset += 9)
                {
                    stream.Write(data.Squares, iOffset, 4);
                    stream.Write(data.Squares, iOffset + 5, 2);
                }
            }
        }
    }

    public class EOB1Quest : BasicQuest
    {
        public EOB1Quest()
        {
        }
    }

    public class EOB1QuestInfo : QuestInfo<EOB1Quest>
    {
        public QuestStatus Descend2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 2");
        public QuestStatus Descend3 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 3");
        public QuestStatus Descend4 = new QuestStatus(QuestStatus.Basic.NotStarted, "Open the way to Level 4");
        public QuestStatus Descend5 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 5");
        public QuestStatus Descend6 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 6");
        public QuestStatus Descend7 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 7");
        public QuestStatus Descend10 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 10");
        public QuestStatus Descend11 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 11");
        public QuestStatus Descend12 = new QuestStatus(QuestStatus.Basic.NotStarted, "Descend to Level 12");
        public QuestStatus Beholder = new QuestStatus(QuestStatus.Basic.NotStarted, "Kill the Beholder Xanathar");

        public QuestStatus Special1 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 1: Guinsoo");
        public QuestStatus Special2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 2: Daggers");
        public QuestStatus Special3 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 3: Gems");
        public QuestStatus Special4 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 4: Chain");
        public QuestStatus Special5 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 5: Rations");
        public QuestStatus Special6 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 6: Nest");
        public QuestStatus Special7 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 7: Stone");
        public QuestStatus Special8 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 8: Darts");
        public QuestStatus Special9 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 9: Orbs");
        public QuestStatus Special10 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 10: Eggs");
        public QuestStatus Special11 = new QuestStatus(QuestStatus.Basic.NotStarted, "Special 11: Levers");

        public QuestStatus Taghor = new QuestStatus(QuestStatus.Basic.NotStarted, "Taghor's Fate");
        public QuestStatus Armun = new QuestStatus(QuestStatus.Basic.NotStarted, "Help Armun");
        public QuestStatus WandOfFrost = new QuestStatus(QuestStatus.Basic.NotStarted, "Dwarven Levers");
        public QuestStatus MiscXP = new QuestStatus(QuestStatus.Basic.NotStarted, "Miscellaneous XP");
        public QuestStatus Bones = new QuestStatus(QuestStatus.Basic.NotStarted, "Resurrect Bones");
        public QuestStatus Loot = new QuestStatus(QuestStatus.Basic.NotStarted, "Collect Loot");
        public int RationsCounter = -1;
        public int NestCounter = -1;

        // If all of the quests are not returned from the GetAllQuests() function, then the quest window will not
        // update any of the missing quests until a manual refresh is performed.
        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { Descend2, Descend3, Descend4, Descend5, Descend5, Descend6, Descend7, Descend10, Descend11, Descend12, Beholder,
            Special1, Special2, Special3, Special4, Special5, Special6, Special7, Special8, Special9, Special10, Special11,
            Armun, Taghor, WandOfFrost, MiscXP, Bones, Loot }; }

        public override bool QuestsEqual(QuestInfo<EOB1Quest> info)
        {
            if (!base.QuestsEqual(info))
                return false;
            return (RationsCounter == ((EOB1QuestInfo)info).RationsCounter &&
                NestCounter == ((EOB1QuestInfo)info).NestCounter);
        }

        public override BasicQuest[] GetQuests()
        {
            List<EOB1Quest> quests = new List<EOB1Quest>();
            int iMainOrder = 0;

            // Add locations to the QuestStatus objects, and add the objects themselves to the main/side quest lists
            Descend2.AddLocations(new QuestLocation("Pull the north lever", EOB1.Spots.LeverL1_1314),
                new QuestLocation("Open the door", EOB1.Spots.DoorL1_1611),
                new QuestLocation("Open the door", EOB1.Spots.DoorL1_1813),
                new QuestLocation("Push the south button", EOB1.Spots.ButtonL1_2312),
                new QuestLocation("Activate the plate", EOB1.Spots.PlateL1_2214),
                new QuestLocation("Push the west button", EOB1.Spots.ButtonL1_1519),
                new QuestLocation("Put an item on the plate", EOB1.Spots.PlateL1_2217),
                new QuestLocation("Pull the north lever", EOB1.Spots.LeverL1_1819),
                new QuestLocation("Open the door", EOB1.Spots.DoorL1_2224),
                new QuestLocation("Push the south button", EOB1.Spots.ButtonL1_0921),
                new QuestLocation("Put an item on the plate", EOB1.Spots.PlateL1_1623),
                new QuestLocation("Push the east button", EOB1.Spots.ButtonL1_1624));
            Descend2.Postrequisites.Add(new QuestLocation("Climb down the ladder", EOB1.Spots.LeverL1_1819));
            AddMainQuest(iMainOrder++, Descend2, quests);

            Descend3.AddLocations(new QuestLocation("Obtain a Silver Key", EOB1.Spots.SilverKey_87L2_1923),
                new QuestLocation("Unlock the north lock", EOB1.Spots.LockSilverKeyL2_2023),
                new QuestLocation("Pull the east lever", EOB1.Spots.LeverL2_2224),
                new QuestLocation("Throw an item onto the pressure plate", EOB1.Spots.PlateL2_2421),
                new QuestLocation("Take an object from the north alcove", EOB1.Spots.SilverKeyAlcove_84L2_2318),
                new QuestLocation("Obtain a Silver Key", EOB1.Spots.SilverKeyAlcove_84L2_2318),
                new QuestLocation("Unlock the west lock", EOB1.Spots.LockSilverKeyL2_1922),
                new QuestLocation("Push the east button", EOB1.Spots.DoorForceL2_2018),
                new QuestLocation("Force the door open", EOB1.Spots.DoorForceL2_2018),
                new QuestLocation("Place a dagger in the north carving", EOB1.Spots.DaggerCarvingL2_2216),
                new QuestLocation("Push the east button", EOB1.Spots.DoorDaggerL2_2316),
                new QuestLocation("Push the north button", EOB1.Spots.ButtonL2_2515),
                new QuestLocation("Force the door open", EOB1.Spots.DoorForceL2_2713),
                new QuestLocation("Force the door open", EOB1.Spots.DoorForceL2_2815),
                new QuestLocation("Take an object from the east alcove", EOB1.Spots.SilverKeyAlcove_80L2_3017),
                new QuestLocation("Obtain a Silver Key", EOB1.Spots.SilverKeyAlcove_84L2_2318),
                new QuestLocation("Unlock the east lock", EOB1.Spots.LockSilverKeyL2_1924),
                new QuestLocation("Take an object from the south alcove", EOB1.Spots.SilverKeyAlcove_94L2_2829),
                new QuestLocation("Push the west button", EOB1.Spots.ButtonRoomMoveL2_0921),
                new QuestLocation("Push the west button", EOB1.Spots.ButtonRoomMoveL2_0121),
                new QuestLocation("Force the door open", EOB1.Spots.DoorForceL2_0715),
                new QuestLocation("Take the Gold Key", EOB1.Spots.GoldKey_82L2_0315),
                new QuestLocation("Push the west button", EOB1.Spots.ButtonRoomMoveL2_0618),
                new QuestLocation("Unlock the north door with a Gold Key", EOB1.Spots.LockGoldKeyL2_2706));
            Descend3.Postrequisites.Add(new QuestLocation("Climb down the ladder", EOB1.Spots.LadderDownL2_2704));
            AddMainQuest(iMainOrder++, Descend3, quests);

            Descend4.AddLocations(
                new QuestLocation("Obtain a gem", EOB1.Spots.Gem_97L3_1003),
                new QuestLocation("Obtain a gem", EOB1.Spots.Gem_98L3_0306),
                new QuestLocation("Obtain a gem", EOB1.Spots.Gem_106L3_1310),
                new QuestLocation("Obtain a gem", EOB1.Spots.Gem_107L3_0613),
                new QuestLocation("Place a gem in the east socket", EOB1.Spots.Socket_L3_0605),
                new QuestLocation("Place a gem in the south socket", EOB1.Spots.Socket_L3_1106),
                new QuestLocation("Place a gem in the north socket", EOB1.Spots.Socket_L3_0510),
                new QuestLocation("Place a gem in the west socket", EOB1.Spots.Socket_L3_1011));
            Descend4.Postrequisites.Add(new QuestLocation("Open the way to the ladder", EOB1.Spots.LadderDownL3_0806));
            Special3.PreQuest.Add(AddMainQuest(iMainOrder++, Descend4, quests));

            Descend5.AddLocations(
                new QuestLocation("Obtain a Dwarven Key", EOB1.Spots.DwarvenKey_303L4_1508),
                new QuestLocation("Pull the west lever", EOB1.Spots.Lever_L4_1304),
                new QuestLocation("Pull the east lever", EOB1.Spots.Lever_L4_1414),
                new QuestLocation("Pull the east lever", EOB1.Spots.Lever_L4_2108));
            Descend5.Postrequisites.Add(new QuestLocation("Go down the stairs", EOB1.Spots.StairsDownL4_1124));
            AddMainQuest(iMainOrder++, Descend5, quests);

            Descend6.AddLocations(
                new QuestLocation("Obtain a Key [#4]", EOB1.Spots.Key_176L5_2826),
                new QuestLocation("Obtain a Key [#4]", EOB1.Spots.Key_179L5_1428),
                new QuestLocation("Use Key [#4] on the east lock", EOB1.Spots.Lock_L5_2328),
                new QuestLocation("Use Key [#4] on the south lock", EOB1.Spots.Lock_L5_2527),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2627),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2607),
                new QuestLocation("Pull the north lever", EOB1.Spots.Lever_L5_2106),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2206),
                new QuestLocation("Pull the west lever", EOB1.Spots.Lever_L5_2109),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2108),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2408),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2207),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2306),
                new QuestLocation("Use the teleporter", EOB1.Spots.Teleporter_L5_2404),
                new QuestLocation("Pull the west lever", EOB1.Spots.Lever_L5_2506));
            Descend6.Postrequisites.Add(new QuestLocation("Go down the stairs", EOB1.Spots.StairsDownL5_2501));
            AddMainQuest(iMainOrder++, Descend6, quests);

            Descend7.AddLocations(
                new QuestLocation("Leave a weapon on the ground", EOB1.Spots.Weapon_L6_0505),
                new QuestLocation("Leave a weapon on the ground", EOB1.Spots.Weapon_L6_0507),
                new QuestLocation("Open the door", EOB1.Spots.Door_L6_2911),
                new QuestLocation("Open the door", EOB1.Spots.Door_L6_2915),
                new QuestLocation("Obtain a Key [#4]", EOB1.Spots.Key_199L6_1922),
                new QuestLocation("Use Key [#4] on the west lock", EOB1.Spots.Lock_L6_2706),
                new QuestLocation("Obtain a Dwarven Key", EOB1.Spots.DwarvenKey_192L6_0106),
                new QuestLocation("Obtain a Dwarven Key", EOB1.Spots.DwarvenKey_193L6_0706),
                new QuestLocation("Collect darts", EOB1.Spots.DartPlate_L6_0723),
                new QuestLocation("Collect darts", EOB1.Spots.DartPlate_L6_0223),
                new QuestLocation("Collect darts", EOB1.Spots.DartPlate_L6_0225),
                new QuestLocation("Collect darts", EOB1.Spots.DartPlate_L6_0725),
                new QuestLocation("Place a dart in the east alcove", EOB1.Spots.Silverware_L6_0529),
                new QuestLocation("Place a dart in the east alcove", EOB1.Spots.Silverware_L6_0629),
                new QuestLocation("Obtain a Dwarven Key", EOB1.Spots.DwarvenKey_211L6_0829),
                new QuestLocation("Use Dwarven Key in south lock at (4,17)", EOB1.Spots.Lock_L6_0417),
                new QuestLocation("Use Dwarven Key in west lock at (6,19)", EOB1.Spots.Lock_L6_0619),
                new QuestLocation("Use Dwarven Key in north lock at (4,21)", EOB1.Spots.Lock_L6_0421));
            Descend7.Postrequisites.Add(new QuestLocation("Go down the stairs", EOB1.Spots.StairsDownL6_0506));
            AddMainQuest(iMainOrder++, Descend7, quests);

            Descend10.AddLocations(new QuestLocation("Attack or Bribe the Drow", EOB1.Spots.Drow_L7_0119),
                new QuestLocation("Obtain a Key [#4]", EOB1.Spots.Key4_L7_0426),
                new QuestLocation("Obtain a Key [#4]", EOB1.Spots.Key4_L7_3012),
                new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L7_1118),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_269L8_2020),
                new QuestLocation("Go up the stairs", EOB1.Spots.StairsUp_L8_1420),
                new QuestLocation("Use Drow Key on south lock", EOB1.Spots.DrowLock_L7_1320),
                new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L7_1520),
                new QuestLocation("Obtain a Jewelled Key", EOB1.Spots.JewelKey_L8_2121),
                new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L8_1816),
                new QuestLocation("Use Jewelled Key in west lock", EOB1.Spots.JewelLock_L9_1615),
                new QuestLocation("Go up the stairs", EOB1.Spots.StairsUp_L9_1813),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_L8_2014),
                new QuestLocation("Use Drow Key in east lock", EOB1.Spots.DrowLock_L9_2213),
                new QuestLocation("Put Gem in west alcove", EOB1.Spots.KeyForGem_L9_1918),
                new QuestLocation("Push the west button", EOB1.Spots.Button_L9_1919),
                new QuestLocation("Obtain Jewelled Key", EOB1.Spots.KeyForGem_L9_1918),
                new QuestLocation("Use Jewelled Key in east lock", EOB1.Spots.JewelLock_L9_2220),
                new QuestLocation("Go up the stairs", EOB1.Spots.StairsUp_L9_2423),
                new QuestLocation("Put a key in the east alcove", EOB1.Spots.GemForKey_L8_2719),
                new QuestLocation("Push the east button", EOB1.Spots.Button_L8_2718),
                new QuestLocation("Obtain a Gem", EOB1.Spots.GemForKey_L8_2719),
                new QuestLocation("Put Gem in north socket", EOB1.Spots.GemSocket_L8_2418),
                new QuestLocation("Obtain a Ruby Key", EOB1.Spots.RubyKey_L8_2516),
                new QuestLocation("Go up the stairs", EOB1.Spots.StairsUp_L8_2316),
                new QuestLocation("Open door", EOB1.Spots.Door_L7_2219),
                new QuestLocation("Open door", EOB1.Spots.Door_L7_2223),
                new QuestLocation("Push south button", EOB1.Spots.Button_L7_2226),
                new QuestLocation("Push north button", EOB1.Spots.Button_L7_1625),
                new QuestLocation("Drow Key", EOB1.Spots.DrowKey_L7_1425),
                new QuestLocation("Push south button", EOB1.Spots.Button_L7_1825),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_L7_1727),
                new QuestLocation("Obtain a Jewelled key", EOB1.Spots.JewelKey_L7_1923),
                new QuestLocation("Obtain a Jewelled key", EOB1.Spots.JewelKey_L7_2527),
                new QuestLocation("Obtain a Ruby Key", EOB1.Spots.RubyKey_L8_2727),
                new QuestLocation("Use Jewelled key on south lock", EOB1.Spots.JewelLock_L7_2230),
                new QuestLocation("Use Drow Key on south lock", EOB1.Spots.DrowLock_L7_2030),
                new QuestLocation("Use Drow Key on south lock", EOB1.Spots.DrowLock_L7_1830),
                new QuestLocation("Use Jewelled key on south lock", EOB1.Spots.JewelLock_L7_1630),
                new QuestLocation("Use Ruby key on north lock", EOB1.Spots.RubyLock_L7_1130),
                new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L7_0828),
                new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L8_2004),
                new QuestLocation("Obtain a Rock", EOB1.Spots.Rock_L9_2912),
                new QuestLocation("Obtain a Rock", EOB1.Spots.Rock_L9_2913),
                new QuestLocation("Use rock in east socket", EOB1.Spots.RockSocket_L9_2704),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_L9_3002),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_L9_0915),
                new QuestLocation("Use Drow Key on east lock", EOB1.Spots.DrowLock_L9_0719),
                new QuestLocation("Fire missile north", EOB1.Spots.Missile_L9_0315),
                new QuestLocation("Fire missile west", EOB1.Spots.Missile_L9_0315),
                new QuestLocation("Open door", EOB1.Spots.Door_L9_0319));
            Descend10.Postrequisites.Add(new QuestLocation("Go down the stairs", EOB1.Spots.StairsDown_L9_0112));
            AddMainQuest(iMainOrder++, Descend10, quests);

            Descend11.AddLocations(new QuestLocation("Pull the south lever", EOB1.Spots.LeverL10_1417),
                new QuestLocation("Pull the south lever again", EOB1.Spots.LeverL10_1417),
                new QuestLocation("Pull the west lever", EOB1.Spots.LeverL10_1314),
                new QuestLocation("Pull the west lever again", EOB1.Spots.LeverL10_1314));
            Descend11.Postrequisites.Add(new QuestLocation("Drop through the hole", EOB1.Spots.FloorHoleL11_1411));
            AddMainQuest(iMainOrder++, Descend11, quests);

            Descend12.AddLocations(new QuestLocation("Push the south button", EOB1.Spots.InnerButtonSouth_L11_1412),
                new QuestLocation("Push the west button", EOB1.Spots.InnerButtonWest_L11_1311),
                new QuestLocation("Push the north button", EOB1.Spots.InnerButtonNorth_L11_1410),
                new QuestLocation("Push the south button", EOB1.Spots.MidButtonSouth_L11_1415),
                new QuestLocation("Push the west button", EOB1.Spots.MidButtonWest_L11_1011),
                new QuestLocation("Push the north button", EOB1.Spots.MidButtonNorth_L11_1407),
                new QuestLocation("Push the north button", EOB1.Spots.OuterButtonNorth_L11_1405),
                new QuestLocation("Push the west button", EOB1.Spots.WestButton_L11_2815),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_324L11_2615),
                new QuestLocation("Obtain a Stone Orb", EOB1.Spots.StoneOrb_323L11_2615),
                new QuestLocation("Push the west button", EOB1.Spots.InnerButtonWest_L11_1311),
                new QuestLocation("Push the north button", EOB1.Spots.InnerButtonNorth_L11_1410),
                new QuestLocation("Push the east button", EOB1.Spots.InnerButtonEast_L11_1511),
                new QuestLocation("Push the west button", EOB1.Spots.MidButtonWest_L11_1011),
                new QuestLocation("Push the north button", EOB1.Spots.MidButtonNorth_L11_1407),
                new QuestLocation("Push the east button", EOB1.Spots.MidButtonEast_L11_1811),
                new QuestLocation("Push the east button", EOB1.Spots.OuterButtonEast_L11_2011),
                new QuestLocation("Push the west button", EOB1.Spots.WestButton_L11_2221),
                new QuestLocation("Obtain a Drow Key", EOB1.Spots.DrowKey_337L11_1624),
                new QuestLocation("Push the north button", EOB1.Spots.InnerButtonNorth_L11_1410),
                new QuestLocation("Push the east button", EOB1.Spots.InnerButtonEast_L11_1511),
                new QuestLocation("Push the south button", EOB1.Spots.InnerButtonSouth_L11_1412),
                new QuestLocation("Push the north button", EOB1.Spots.MidButtonNorth_L11_1407),
                new QuestLocation("Push the east button", EOB1.Spots.MidButtonEast_L11_1811),
                new QuestLocation("Push the south button", EOB1.Spots.MidButtonSouth_L11_1415),
                new QuestLocation("Push the south button", EOB1.Spots.OuterButtonSouth_L11_1417),
                new QuestLocation("Use Drow Key on south lock", EOB1.Spots.LockDrowKeyL11_0918),
                new QuestLocation("Push the south button", EOB1.Spots.Button_L11_0422),
                new QuestLocation("Use Drow Key on south lock", EOB1.Spots.LockDrowKeyL11_0325));
            Descend12.Postrequisites.Add(new QuestLocation("Use Stone Orb on south gateway", EOB1.Spots.GatewayL11_0427));
            AddMainQuest(iMainOrder++, Descend12, quests);

            Beholder.AddLocations(new QuestLocation("Turn the west torch", EOB1.Spots.TorchL12_1311),
                new QuestLocation("Push the east button", EOB1.Spots.Button_L12_0415));
            Beholder.Postrequisites.Add(new QuestLocation("Defeat Xanathar", EOB1.Spots.XanatharL12_2807));
            AddMainQuest(iMainOrder++, Beholder, quests);

            Armun.AddLocations(new QuestLocation("Obtain a Dwarven Key", EOB1.Spots.DwarvenKey_172L5_0425),
                new QuestLocation("Open one of the doors (Dwarven Key or lockpick)", EOB1.Spots.Lock_L5_0619),
                new QuestLocation("Open one of the doors (Dwarven Key or lockpick)", EOB1.Spots.Lock_L5_0919),
                new QuestLocation("Open one of the doors (Dwarven Key or lockpick)", EOB1.Spots.Lock_L5_1219),
                new QuestLocation("Agree to help Armun", EOB1.Spots.Armun_L5_1510),
                new QuestLocation("Descend to level 11 (see Main Story)", EOB1.Spots.Armun_L5_1510),
                new QuestLocation("Obtain Dwarven Healing Potion", EOB1.Spots.DwarvenHealingPotionAlcove_52L11_2417));
            Armun.Postrequisites.Add(new QuestLocation("Return to Armun", EOB1.Spots.Armun_L5_1510));
            Beholder.PreQuest.Add(AddSideQuest(Armun, quests, "Wand of Slivias"));

            Taghor.AddLocations(new QuestLocation("Find Taghor", EOB1.Spots.Taghor_L4_1613),
                new QuestLocation("Talk to Taghor", EOB1.Spots.Taghor_L4_1613),
                new QuestLocation("Tend Taghor's wounds", EOB1.Spots.Taghor_L4_1613),
                new QuestLocation("Abandon Taghor", EOB1.Spots.AbandonTaghor_L4_1621));
            AddSideQuest(Taghor, quests, "Taghor and/or equipment");

            Special1.AddLocations(new QuestLocation("Obtain a dagger (Kobolds carry them)", EOB1.Spots.None));
            Special1.Postrequisites.Add(new QuestLocation("Place a dagger in the north alcove", EOB1.Spots.GuinsooAlcoveL1_1225));
            AddSideQuest(Special1, quests, "Guinsoo");

            Special2.AddLocations(new QuestLocation("Obtain a dagger (Kobolds carry them)", EOB1.Spots.None),
                new QuestLocation("Place a dagger into the west carving", EOB1.Spots.DaggerCarvingL2_1429),
                new QuestLocation("Place a dagger into the south carving", EOB1.Spots.DaggerCarvingL2_2130),
                new QuestLocation("Place a dagger into the north carving", EOB1.Spots.DaggerCarvingL2_2216),
                new QuestLocation("Place a dagger into the south carving", EOB1.Spots.DaggerCarvingL2_0930));
            AddSideQuest(Special2, quests, "4 Rations");

            Special3.AddLocations(new QuestLocation("Remove the gem from the east socket", EOB1.Spots.Socket_L3_0605),
                new QuestLocation("Remove the gem from the south socket", EOB1.Spots.Socket_L3_1106),
                new QuestLocation("Remove the gem from the north socket", EOB1.Spots.Socket_L3_0510),
                new QuestLocation("Remove the gem from the west socket", EOB1.Spots.Socket_L3_1011));
            AddSideQuest(Special3, quests, "Potions: Heal, Str.");

            Special4.AddLocations(new QuestLocation("Remove all items from the square", EOB1.Spots.DwarvenKey_303L4_1508),
                new QuestLocation("Leave the square solid", EOB1.Spots.DwarvenKey_303L4_1508),
                new QuestLocation("Pull the south chain", EOB1.Spots.Chain_L4_1806));
            AddSideQuest(Special4, quests, "3000 XP");

            Special5.AddLocations(new QuestLocation(RationsCounter == -1 ? "Place 5 rations into the south alcove" :
                String.Format("Place 5 rations (currently: {0}) into the south alcove", RationsCounter), EOB1.Spots.RationsQuest_L5_1013));
            AddSideQuest(Special5, quests, "5 Iron Rations");

            Special6.AddLocations(new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_182L6_1105),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_183L6_1504),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_184L6_1505),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_185L6_1718),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_186L6_1505),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_187L6_2105),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_188L6_2908),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_189L6_1605),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_190L6_1705),
                new QuestLocation("Obtain a Kenku Egg", EOB1.Spots.KenkuEgg_191L6_1905),
                new QuestLocation(NestCounter == -1 ? "Place 10 Kenku Eggs into the square" :
                    String.Format("Place 10 Kenku Eggs (currently: {0}) into the square", NestCounter), EOB1.Spots.KenkuNest_L6_1317));
            AddSideQuest(Special6, quests, "Chieftan's Halberd");

            Special7.AddLocations(new QuestLocation("Obtain a stone item (see quest \"Collect Loot\")", EOB1.Spots.None),
                new QuestLocation("Place a stone item into the south alcove", EOB1.Spots.RockAlcove_248L7_1231),
                new QuestLocation("Place a stone item into the south alcove", EOB1.Spots.WandAlcove_249L7_1331),
                new QuestLocation("Place a stone item into the south alcove", EOB1.Spots.EmptyAlcove_L7_1431));
            AddSideQuest(Special7, quests, "Hints");

            Special8.AddLocations(new QuestLocation("Obtain a dart (see quest \"Collect Loot\")", EOB1.Spots.None),
                new QuestLocation("Place a dart into the north alcove", EOB1.Spots.DartAlcove_L8_2402),
                new QuestLocation("Place a dart into the north alcove", EOB1.Spots.DartAlcove_L8_2502),
                new QuestLocation("Place a dart into the north alcove", EOB1.Spots.DartAlcove_L8_2602),
                new QuestLocation("Place a dart into the north alcove", EOB1.Spots.DartAlcove_L8_2702),
                new QuestLocation("Place a dart into the north alcove", EOB1.Spots.DartAlcove_L8_2802),
                new QuestLocation("Place a dart into the east alcove", EOB1.Spots.DartAlcove_L8_2903),
                new QuestLocation("Place a dart into the east alcove", EOB1.Spots.DartAlcove_L8_2904),
                new QuestLocation("Place a dart into the south alcove", EOB1.Spots.DartAlcove_L8_2405),
                new QuestLocation("Place a dart into the south alcove", EOB1.Spots.DartAlcove_L8_2505),
                new QuestLocation("Place a dart into the south alcove", EOB1.Spots.DartAlcove_L8_2605),
                new QuestLocation("Place a dart into the south alcove", EOB1.Spots.DartAlcove_L8_2705),
                new QuestLocation("Place a dart into the south alcove", EOB1.Spots.DartAlcove_L8_2805));
            AddSideQuest(Special8, quests, "10 Adamantite Darts");

            Special9.AddLocations(new QuestLocation("Throw a small object east", EOB1.Spots.MissileWall_L9_2218));
            AddSideQuest(Special9, quests, "3 Orbs of Power");

            Special10.AddLocations(new QuestLocation("Obtain a Kenku Egg (see quest \"Special 6: Nest\")", EOB1.Spots.None),
                new QuestLocation("Place a Kenku Egg into the east alcove", EOB1.Spots.Kenku_L10_2629),
                new QuestLocation("Place a Kenku Egg into the east alcove", EOB1.Spots.Kenku_L10_2627),
                new QuestLocation("Place a Kenku Egg into the east alcove (must be last)", EOB1.Spots.Kenku_L10_2625));
            AddSideQuest(Special10, quests, "Ring of Sustenance, Ring of Feather Fall, 2 Ring +2");

            Special11.AddLocations(new QuestLocation("Place a scroll in the south alcove", EOB1.Spots.DwarvenHealingPotionAlcove_52L11_2417),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2409),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2410),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2411),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2412),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2413),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2414),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2415),
                new QuestLocation("Set the east lever to down", EOB1.Spots.Lever_L11_2416));
            AddSideQuest(Special11, quests, "10000 XP, Hint");

            WandOfFrost.AddLocations(new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2409),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2410),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2411),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2412),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2413),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2414),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2415),
                new QuestLocation("Set the east lever to up", EOB1.Spots.Lever_L11_2416));
            AddSideQuest(WandOfFrost, quests, "Wand of Frost");

            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate east grate", EOB1.Spots.GrateL1_1414));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate west grate", EOB1.Spots.GrateL1_1614));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate east rune", EOB1.Spots.RuneL1_1812));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate south drain", EOB1.Spots.DrainL1_2011));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate east grate", EOB1.Spots.GrateL1_1717));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate west grate", EOB1.Spots.GrateL1_1917));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate east grate", EOB1.Spots.GrateL1_2321));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate west grate", EOB1.Spots.GrateL1_2521));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate west pipe", EOB1.Spots.PipeL1_2023));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate south grate", EOB1.Spots.IgneousRockL1_1321));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate north grate", EOB1.Spots.IngeousRockL1_1323));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate east pipe", EOB1.Spots.PipeL1_0624));
            MiscXP.AddLocations(new QuestLocation("#3 (800): Activate west pipe", EOB1.Spots.PipeL1_0824));
            MiscXP.AddLocations(new QuestLocation("#5 (800): Push south button", EOB1.Spots.ButtonL1_0921));
            MiscXP.AddLocations(new QuestLocation("#5 (800): Push west button", EOB1.Spots.ButtonL1_0823));
            MiscXP.AddLocations(new QuestLocation("#5 (800): Push west button", EOB1.Spots.ButtonL1_1519));
            MiscXP.AddLocations(new QuestLocation("#5 (800): Push south button", EOB1.Spots.ButtonL1_2312));
            MiscXP.AddLocations(new QuestLocation("#6 (1600): Push east button", EOB1.Spots.ButtonL1_2521));
            MiscXP.AddLocations(new QuestLocation("#6 (1600): Push west button", EOB1.Spots.ButtonL1_1921));
            MiscXP.AddLocations(new QuestLocation("#7 (800): Activate pressure plate", EOB1.Spots.ButtonL1_2312));
            MiscXP.AddLocations(new QuestLocation("#7 (800): Activate pressure plate", EOB1.Spots.PlateL1_1318));
            MiscXP.AddLocations(new QuestLocation("#8 (1600): Enter the square", EOB1.Spots.ButtonL1_2312));
            MiscXP.AddLocations(new QuestLocation("#10 (1600): Enter the square", EOB1.Spots.ExitDoorL1_1723));

            AddSideQuest(MiscXP, quests);

            Bones.AddLocations(new QuestLocation("Obtain bones of Tod Uphill", EOB1.Spots.TodUphillL1_1414),
                new QuestLocation("Resurrect Tod Uphill", EOB1.Spots.DwarfCleric_L5_1907),
                new QuestLocation("Obtain bones of Anya", EOB1.Spots.HumanBones_338L3_0130),
                new QuestLocation("Resurrect Anya", EOB1.Spots.DwarfCleric_L5_1907),
                new QuestLocation("Obtain bones of Ileria", EOB1.Spots.HumanBones_339L7_2904),
                new QuestLocation("Resurrect Ileria", EOB1.Spots.DwarfCleric_L5_1907),
                new QuestLocation("Obtain bones of Beohram", EOB1.Spots.HumanBones_340L9_1111),
                new QuestLocation("Resurrect Beohram", EOB1.Spots.DwarfCleric_L5_1907),
                new QuestLocation("Obtain bones of Tyrra", EOB1.Spots.HumanBones_341L10_0312),
                new QuestLocation("Resurrect Tyrra", EOB1.Spots.DwarfCleric_L5_1907),
                new QuestLocation("Obtain bones of Kirath", EOB1.Spots.HumanBones_342L11_1627),
                new QuestLocation("Resurrect Kirath", EOB1.Spots.DwarfCleric_L5_1907));
            AddSideQuest(Bones, quests);

            Loot.AddLocations(new QuestLocation("Arrow [#73]", EOB1.Spots.Arrow_73L1_1624),
                new QuestLocation("Cleric Scroll of Bless [#70]", EOB1.Spots.ClericScrollOfBlessAlcove_70L1_1224),
                new QuestLocation("Dart +2 [#67]", EOB1.Spots.Dart_67L1_2518),
                new QuestLocation("Halfling Bones (Tod Uphill) [#64]", EOB1.Spots.HalflingBones_64L1_1414),
                new QuestLocation("Lock picks [#65]", EOB1.Spots.LockPicks_65L1_1414),
                new QuestLocation("Mage Scroll of Armor [#72]", EOB1.Spots.MageScrollOfArmorAlcove_72L1_1224),
                new QuestLocation("Rations [#63]", EOB1.Spots.Rations_63L1_2007),
                new QuestLocation("Rations [#68]", EOB1.Spots.Rations_68L1_1019),
                new QuestLocation("Rations [#69]", EOB1.Spots.Rations_69L1_1019),
                new QuestLocation("Rock [#377]", EOB1.Spots.Rock_377L1_1015),
                new QuestLocation("Rock [#378]", EOB1.Spots.Rock_378L1_1015),
                new QuestLocation("Rock [#66]", EOB1.Spots.Rock_66L1_2017),
                new QuestLocation("Rock [#71]", EOB1.Spots.Rock_71L1_2121),
                new QuestLocation("Shield [#74]", EOB1.Spots.Shield_74L1_1025),
                new QuestLocation("Arrow [#75]", EOB1.Spots.Arrow_75L2_3028),
                new QuestLocation("Arrow [#93]", EOB1.Spots.Arrow_93L2_3028),
                new QuestLocation("Bow [#85]", EOB1.Spots.Bow_85L2_0620),
                new QuestLocation("Gold Key (#2) [#82]", EOB1.Spots.GoldKey_82L2_0315),
                new QuestLocation("Leather boots [#78]", EOB1.Spots.LeatherBoots_78L2_1910),
                new QuestLocation("Mage Scroll of Invisibility [#88]", EOB1.Spots.MageScrollOfInvisibility_88L2_0624),
                new QuestLocation("Mage Scroll of Shield [#91]", EOB1.Spots.MageScrollOfShield_91L2_1527),
                new QuestLocation("Potion of Vitality [#446]", EOB1.Spots.Potion_446L2_2121),
                new QuestLocation("Potion of Extra Healing [#421]", EOB1.Spots.PotionOfExtraHealing_421L2_0730),
                new QuestLocation("Potion of Giant Strength [#81]", EOB1.Spots.PotionOfGiantStrengthAlcove_81L2_3017),
                new QuestLocation("Potion of Healing [#79]", EOB1.Spots.PotionOfHealing_79L2_0611),
                new QuestLocation("Potion of Healing [#95]", EOB1.Spots.PotionOfHealing_95L2_0230),
                new QuestLocation("Rations [#416]", EOB1.Spots.RationsAlcove_416L2_3017),
                new QuestLocation("Rations [#417]", EOB1.Spots.Rations_417L2_2318Alcove),
                new QuestLocation("Rations [#418]", EOB1.Spots.RationsAlcove_418L2_2829),
                new QuestLocation("Rations [#420]", EOB1.Spots.Rations_420L2_0730),
                new QuestLocation("Rations [#76]", EOB1.Spots.Rations_76L2_1306),
                new QuestLocation("Rations [#77]", EOB1.Spots.Rations_77L2_1910),
                new QuestLocation("Rations [#89]", EOB1.Spots.Rations_89L2_2524),
                new QuestLocation("Rations [#90]", EOB1.Spots.Rations_90L2_2826),
                new QuestLocation("Rock [#83]", EOB1.Spots.Rock_83L2_0916),
                new QuestLocation("Rock [#96]", EOB1.Spots.Rock_96L2_2130),
                new QuestLocation("Silver Key (#1) [#80]", EOB1.Spots.SilverKeyAlcove_80L2_3017),
                new QuestLocation("Silver Key (#1) [#84]", EOB1.Spots.SilverKeyAlcove_84L2_2318),
                new QuestLocation("Silver Key (#1) [#87]", EOB1.Spots.SilverKey_87L2_1923),
                new QuestLocation("Silver Key (#1) [#94]", EOB1.Spots.SilverKeyAlcove_94L2_2829),
                new QuestLocation("Sling [#92]", EOB1.Spots.Sling_92L2_1928),
                new QuestLocation("Stone Dagger (#4) [#86]", EOB1.Spots.StoneDagger_86L2_1321),
                new QuestLocation("Arrow [#102]", EOB1.Spots.ArrowAlcove_102L3_1007),
                new QuestLocation("Arrow [#108]", EOB1.Spots.Arrow_108L3_1513),
                new QuestLocation("Arrow [#123]", EOB1.Spots.Arrow_123L3_1724),
                new QuestLocation("Arrow [#373]", EOB1.Spots.Arrow_373L3_1724),
                new QuestLocation("Arrow [#374]", EOB1.Spots.Arrow_374L3_1724),
                new QuestLocation("Arrow [#375]", EOB1.Spots.Arrow_375L3_1724),
                new QuestLocation("Arrow [#99]", EOB1.Spots.Arrow_99L3_2106),
                new QuestLocation("'Backstabber' +3 [#112]", EOB1.Spots.Backstabber_112L3_1015),
                new QuestLocation("Chainmail [#100]", EOB1.Spots.Chainmail_100L3_0907),
                new QuestLocation("Cleric Scroll of Cause Light Wounds [#121]", EOB1.Spots.ClericScrollOfCauseLightWounds_121L3_1202),
                new QuestLocation("Cleric Scroll of Flame Blade [#118]", EOB1.Spots.ClericScrollOfFlameBlade_118L3_1122),
                new QuestLocation("Gem (#2) [#106]", EOB1.Spots.GemAlcove_106L3_1310),
                new QuestLocation("Gem (#2) [#107]", EOB1.Spots.GemAlcove_107L3_0613),
                new QuestLocation("Gem (#1) [#122]", EOB1.Spots.Gem_122L3_1015),
                new QuestLocation("Gem (#2) [#97]", EOB1.Spots.GemAlcove_97L3_1003),
                new QuestLocation("Gem (#2) [#98]", EOB1.Spots.GemAlcove_98L3_0306),
                new QuestLocation("Human Bones (Anya) [#338]", EOB1.Spots.HumanBones_338L3_0130),
                new QuestLocation("Iron Rations [#103]", EOB1.Spots.IronRationsAlcove_103L3_0609),
                new QuestLocation("Iron Rations [#104]", EOB1.Spots.IronRationsAlcove_104L3_0609),
                new QuestLocation("Iron Rations [#419]", EOB1.Spots.IronRationsAlcove_419L3_0609),
                new QuestLocation("Leather armor [#345]", EOB1.Spots.LeatherArmor_345L3_0130),
                new QuestLocation("Long Sword [#125]", EOB1.Spots.LongSword_125L3_0130),
                new QuestLocation("Mage Scroll of Detect Magic [#111]", EOB1.Spots.MageScrollOfDetectMagic_111L3_1214),
                new QuestLocation("Mage Scroll of Fireball [#120]", EOB1.Spots.MageScrollOfFireball_120L3_1522),
                new QuestLocation("Potion of Extra Healing [#110]", EOB1.Spots.PotionOfExtraHealing_110L3_2813),
                new QuestLocation("Potion of Healing [#109]", EOB1.Spots.PotionOfHealing_109L3_2813),
                new QuestLocation("Potion of Healing [#116]", EOB1.Spots.PotionOfHealing_116L3_0921),
                new QuestLocation("Potion of Speed [#372]", EOB1.Spots.PotionOfSpeed_372L3_1724),
                new QuestLocation("Rations [#113]", EOB1.Spots.Rations_113L3_0316),
                new QuestLocation("Rations [#115]", EOB1.Spots.Rations_115L3_1020),
                new QuestLocation("Rock [#117]", EOB1.Spots.Rock_117L3_0522),
                new QuestLocation("Rock [#119]", EOB1.Spots.Rock_119L3_1322),
                new QuestLocation("Rock [#124]", EOB1.Spots.Rock_124L3_2026),
                new QuestLocation("Shield [#101]", EOB1.Spots.Shield_101L3_0907),
                new QuestLocation("Shield [#114]", EOB1.Spots.Shield_114L3_1319),
                new QuestLocation("Silver Key (#1) [#105]", EOB1.Spots.SilverKey_105L3_2809),
                new QuestLocation("Spear [#346]", EOB1.Spots.Spear_346L3_0130),
                new QuestLocation("Wand of Magic Missile [#126]", EOB1.Spots.Wand_126L3_3030),
                new QuestLocation("Arrow [#127]", EOB1.Spots.Arrow_127L4_2604),
                new QuestLocation("Arrow [#131]", EOB1.Spots.Arrow_131L4_2910),
                new QuestLocation("Arrow [#152]", EOB1.Spots.Arrow_152L4_2025),
                new QuestLocation("Cleric Scroll of Slow Poison [#145]", EOB1.Spots.ClericScrollOfSlowPoison_145L4_0319),
                new QuestLocation("'Drow Cleaver' +3 [#140]", EOB1.Spots.DrowCleaver_140L4_1118),
                new QuestLocation("Dwarven Helmet [#149]", EOB1.Spots.DwarvenHelmet_149L4_1020),
                new QuestLocation("Dwarven Key (#3) [#130]", EOB1.Spots.DwarvenKey_130L4_0307),
                new QuestLocation("Dwarven Key (#3) [#153]", EOB1.Spots.DwarvenKey_153L4_2828),
                new QuestLocation("Dwarven Key (#3) [#303]", EOB1.Spots.DwarvenKey_303L4_1508),
                new QuestLocation("Dwarven Key (#3) [#376]", EOB1.Spots.DwarvenKey_376L4_0915),
                new QuestLocation("Dwarven Shield [#150]", EOB1.Spots.DwarvenShield_150L4_1020),
                new QuestLocation("Iron Rations [#146]", EOB1.Spots.IronRations_146L4_2019),
                new QuestLocation("Iron Rations [#147]", EOB1.Spots.IronRations_147L4_2019),
                new QuestLocation("Iron Rations [#148]", EOB1.Spots.IronRations_148L4_2019),
                new QuestLocation("Mace [#128]", EOB1.Spots.Mace_128L4_2206),
                new QuestLocation("Mace [#135]", EOB1.Spots.Mace_135L4_2113),
                new QuestLocation("Mage Scroll of Flame Arrow [#144]", EOB1.Spots.MageScrollOfFlameArrow_144L4_0319),
                new QuestLocation("Medallion [#138]", EOB1.Spots.Medallion_138L4_1017),
                new QuestLocation("Potion of Cure Poison [#136]", EOB1.Spots.PotionOfCurePoison_136L4_2415),
                new QuestLocation("Potion of Cure Poison [#137]", EOB1.Spots.PotionOfCurePoison_137L4_2415),
                new QuestLocation("Cure Poison Potion [#430]", EOB1.Spots.PotionOfCurePoisonAlcove_430L4_2103),
                new QuestLocation("Cure Poison Potion [#431]", EOB1.Spots.PotionOfCurePoisonAlcove_431L4_2103),
                new QuestLocation("Cure Poison Potion [#432]", EOB1.Spots.PotionOfCurePoisonAlcove_432L4_2104),
                new QuestLocation("Cure Poison Potion [#433]", EOB1.Spots.PotionOfCurePoisonAlcove_433L4_2104),
                new QuestLocation("Potion of Healing [#134]", EOB1.Spots.PotionOfHealing_134L4_0314),
                new QuestLocation("Potion of Healing [#143]", EOB1.Spots.PotionOfHealing_143L4_0319),
                new QuestLocation("Ring +3 [#129]", EOB1.Spots.Ring_129L4_2406),
                new QuestLocation("Ring of Adornment [#132]", EOB1.Spots.Ring_132L4_2911),
                new QuestLocation("Robe [#139]", EOB1.Spots.Robe_139L4_1017),
                new QuestLocation("Rock [#133]", EOB1.Spots.Rock_133L4_3012),
                new QuestLocation("Rock [#151]", EOB1.Spots.Rock_151L4_1924),
                new QuestLocation("Rock [#154]", EOB1.Spots.Rock_154L4_3028),
                new QuestLocation("Stone Scepter (#6) [#141]", EOB1.Spots.StoneScepter_141L4_2702),
                new QuestLocation("Cursed Axe -3 [#174]", EOB1.Spots.Axe_174L5_2025),
                new QuestLocation("Boots [#181]", EOB1.Spots.Boots_181L5_1930),
                new QuestLocation("Cleric Scroll of Aid [#159]", EOB1.Spots.ClericScrollOfAid_159L5_0506),
                new QuestLocation("Cleric Scroll of Detect Magic [#162]", EOB1.Spots.ClericScrollOfDetectMagic_162L5_2408),
                new QuestLocation("Cleric Scroll of Hold Person [#155]", EOB1.Spots.ClericScrollOfHoldPerson_155L5_0101),
                new QuestLocation("Cleric Scroll of Prayer [#180]", EOB1.Spots.ClericScrollOfPrayer_180L5_0629),
                new QuestLocation("Dwarven Key (#3) [#172]", EOB1.Spots.DwarvenKey_172L5_0425),
                new QuestLocation("Iron Rations [#156]", EOB1.Spots.IronRations_156L5_2102),
                new QuestLocation("Iron Rations [#161]", EOB1.Spots.IronRations_161L5_0807),
                new QuestLocation("Iron Rations [#163]", EOB1.Spots.IronRations_163L5_1210),
                new QuestLocation("Iron Rations [#165]", EOB1.Spots.IronRations_165L5_0712),
                new QuestLocation("Iron Rations [#168]", EOB1.Spots.IronRations_168L5_0614),
                new QuestLocation("Key (#4) [#176]", EOB1.Spots.Key_176L5_2826),
                new QuestLocation("Key (#4) [#179]", EOB1.Spots.Key_179L5_1428),
                new QuestLocation("Long Sword [#164]", EOB1.Spots.LongSword_164L5_2510),
                new QuestLocation("Mage Scroll of Dispel Magic [#169]", EOB1.Spots.MageScrollOfDispelMagic_169L5_0515),
                new QuestLocation("Mage Scroll of Haste [#160]", EOB1.Spots.MageScrollOfHaste_160L5_0606),
                new QuestLocation("Mage Scroll of Invisibility 10' Radius [#178]", EOB1.Spots.MageScrollOfInvisibility10_178L5_2527),
                new QuestLocation("Plate Mail [#171]", EOB1.Spots.PlateMail_171L5_2527),
                new QuestLocation("Potion of Poison [#167]", EOB1.Spots.PotionOfPoison_167L5_1013),
                new QuestLocation("Ring of Feather Fall [#177]", EOB1.Spots.Ring_177L5_2227),
                new QuestLocation("Rock [#170]", EOB1.Spots.Rock_170L5_2516),
                new QuestLocation("Rock [#302]", EOB1.Spots.Rock_302L5_2111),
                new QuestLocation("Scale Mail [#173]", EOB1.Spots.ScaleMail_173L5_0425),
                new QuestLocation("Cursed Sling -3 [#175]", EOB1.Spots.Sling_175L5_1926),
                new QuestLocation("Spear [#157]", EOB1.Spots.Spear_157L5_2102),
                new QuestLocation("Stone Necklace (#2) [#158]", EOB1.Spots.StoneNecklaceAlcove_158L5_1605),
                new QuestLocation("Wand of Frost [#142]", EOB1.Spots.Wand_142L5_2716),
                new QuestLocation("Adamantite Dart +4 [#380]", EOB1.Spots.AdamantiteDartAlcove_380L6_0328),
                new QuestLocation("Bracers [#209]", EOB1.Spots.Bracers_209L6_2628),
                new QuestLocation("Chieftain Halberd +5 [#215]", EOB1.Spots.ChieftainHalberd_215L6_1418),
                new QuestLocation("Cleric Scroll of Cure Serious Wounds [#198]", EOB1.Spots.ClericScrollOfCureSeriousWounds_198L6_1420),
                new QuestLocation("Cleric Scroll of Dispel Magic [#197]", EOB1.Spots.ClericScrollOfDispelMagic_197L6_1420),
                new QuestLocation("Cleric Scroll of Flame Blade [#213]", EOB1.Spots.ClericScrollOfFlameBlade_213L6_2230),
                new QuestLocation("Dagger [#381]", EOB1.Spots.DaggerAlcove_381L6_0328),
                new QuestLocation("Dwarven Key (#3) [#192]", EOB1.Spots.DwarvenKey_192L6_0106),
                new QuestLocation("Dwarven Key (#3) [#193]", EOB1.Spots.DwarvenKey_193L6_0706),
                new QuestLocation("Dwarven Key (#3) [#211]", EOB1.Spots.DwarvenKeyAlcove_211L6_0829),
                new QuestLocation("Dwarven Shield +1 [#206]", EOB1.Spots.DwarvenShield_206L6_0925),
                new QuestLocation("Kenku Egg (#20) [#182]", EOB1.Spots.KenkuEgg_182L6_1105),
                new QuestLocation("Kenku Egg (#30) [#183]", EOB1.Spots.KenkuEgg_183L6_1504),
                new QuestLocation("Kenku Egg (#40) [#184]", EOB1.Spots.KenkuEgg_184L6_1505),
                new QuestLocation("Kenku Egg (#10) [#185]", EOB1.Spots.KenkuEgg_185L6_1718),
                new QuestLocation("Kenku Egg (#20) [#186]", EOB1.Spots.KenkuEgg_186L6_1505),
                new QuestLocation("Kenku Egg (#30) [#187]", EOB1.Spots.KenkuEgg_187L6_2105),
                new QuestLocation("Kenku Egg (#40) [#188]", EOB1.Spots.KenkuEgg_188L6_2908),
                new QuestLocation("Kenku Egg (#10) [#189]", EOB1.Spots.KenkuEgg_189L6_1605),
                new QuestLocation("Kenku Egg (#20) [#190]", EOB1.Spots.KenkuEgg_190L6_1705),
                new QuestLocation("Kenku Egg (#30) [#191]", EOB1.Spots.KenkuEgg_191L6_1905),
                new QuestLocation("Key (#4) [#199]", EOB1.Spots.Key_199L6_1922),
                new QuestLocation("Mace +3 [#208]", EOB1.Spots.MacePlus3_208L6_1228),
                new QuestLocation("Mage Scroll of Hold Person [#194]", EOB1.Spots.MageScrollOfHoldPerson_194L6_2715),
                //new QuestLocation("NULL [#200]", EOB1.Spots.NULL_200L6_0222),
                //new QuestLocation("NULL [#201]", EOB1.Spots.NULL_201L6_0222),
                //new QuestLocation("NULL [#202]", EOB1.Spots.NULL_202L6_0823),
                //new QuestLocation("NULL [#203]", EOB1.Spots.NULL_203L6_0823),
                //new QuestLocation("NULL [#204]", EOB1.Spots.NULL_204L6_0125),
                //new QuestLocation("NULL [#205]", EOB1.Spots.NULL_205L6_0125),
                //new QuestLocation("NULL [#422]", EOB1.Spots.NULL_422L6_0122),
                //new QuestLocation("NULL [#423]", EOB1.Spots.NULL_423L6_0122),
                //new QuestLocation("NULL [#424]", EOB1.Spots.NULL_424L6_0923),
                //new QuestLocation("NULL [#425]", EOB1.Spots.NULL_425L6_0923),
                //new QuestLocation("NULL [#426]", EOB1.Spots.NULL_426L6_0025),
                //new QuestLocation("NULL [#427]", EOB1.Spots.NULL_427L6_0025),
                //new QuestLocation("NULL [#428]", EOB1.Spots.NULL_428L6_0824),
                //new QuestLocation("NULL [#429]", EOB1.Spots.NULL_429L6_0824),
                new QuestLocation("Potion of Extra Healing [#379]", EOB1.Spots.PotionOfExtraHealing_379L6_2715),
                new QuestLocation("Ring of Adornment [#212]", EOB1.Spots.Ring_212L6_1529),
                new QuestLocation("Rock [#196]", EOB1.Spots.Rock_196L6_1313),
                new QuestLocation("Rock [#207]", EOB1.Spots.Rock_207L6_1725),
                new QuestLocation("Stone Ring (#7) [#195]", EOB1.Spots.StoneRing_195L6_1711),
                new QuestLocation("Wand of Magic Missile [#210]", EOB1.Spots.WandAlcove_210L6_0629),
                new QuestLocation("Arrow [#219]", EOB1.Spots.Arrow_219L7_2208),
                new QuestLocation("Arrow [#227]", EOB1.Spots.Arrow_227L7_1216),
                new QuestLocation("Arrow [#228]", EOB1.Spots.Arrow_228L7_1216),
                new QuestLocation("Arrow [#229]", EOB1.Spots.Arrow_229L7_1216),
                new QuestLocation("Arrow [#230]", EOB1.Spots.Arrow_230L7_0722),
                new QuestLocation("Arrow [#237]", EOB1.Spots.Arrow_237L7_2723),
                new QuestLocation("Arrow [#238]", EOB1.Spots.Arrow_238L7_2723),
                new QuestLocation("Arrow [#239]", EOB1.Spots.Arrow_239L7_2723),
                new QuestLocation("Banded Armor [#236]", EOB1.Spots.BandedArmor_236L7_2523),
                new QuestLocation("Bracers +3 [#232]", EOB1.Spots.Bracers_232L7_2622),
                new QuestLocation("Cleric Scroll of Bless [#218]", EOB1.Spots.ClericScrollOfBless_218L7_1705),
                new QuestLocation("Cleric Scroll of Create Food & Water [#223]", EOB1.Spots.ClericScrollOfCreateFood_223L7_0612),
                new QuestLocation("Cleric Scroll of Cure Light Wounds [#245]", EOB1.Spots.ClericScrollOfCureLightWounds_245L7_1927),
                new QuestLocation("Cleric Scroll of Protection from Evil 10' Radius [#220]", EOB1.Spots.ClericScrollOfProtectEvil10_220L7_3011),
                new QuestLocation("Cleric Scroll of Remove Paralysis [#221]", EOB1.Spots.ClericScrollOfRemoveParalysis_221L7_3011),
                new QuestLocation("Cleric Scroll of Slow Poison [#222]", EOB1.Spots.ClericScrollOfSlowPoison_222L7_0312),
                new QuestLocation("Drow Key (#6) [#240]", EOB1.Spots.DrowKey_240L7_1425),
                new QuestLocation("Drow Key (#6) [#244]", EOB1.Spots.DrowKey_244L7_1727),
                new QuestLocation("Ruby Key (#8) [#247]", EOB1.Spots.RubyKey_247L7_2727),
                new QuestLocation("Holy Symbol [#434]", EOB1.Spots.HolySymbol_434L7_2904),
                new QuestLocation("Human Bones (Ileria) [#339]", EOB1.Spots.HumanBones_339L7_2904),
                new QuestLocation("Iron Rations [#216]", EOB1.Spots.IronRations_216L7_1705),
                new QuestLocation("Jeweled Key (#7) [#235]", EOB1.Spots.JeweledKey_235L7_1923),
                new QuestLocation("Jeweled Key (#7) [#246]", EOB1.Spots.JeweledKey_246L7_2527),
                new QuestLocation("Key (#4) [#224]", EOB1.Spots.Key_224L7_3012),
                new QuestLocation("Key (#4) [#242]", EOB1.Spots.Key_242L7_0426),
                new QuestLocation("Mage Scroll of Fear [#234]", EOB1.Spots.MageScrollOfFear_234L7_1723),
                new QuestLocation("Mage Scroll of Fireball [#214]", EOB1.Spots.MageScrollOfFireball_214L7_0203),
                new QuestLocation("Mage Scroll of Lightning Bolt [#241]", EOB1.Spots.MageScrollOfLightningBolt_241L7_3025),
                new QuestLocation("Medallion +1 [#225]", EOB1.Spots.Medallion_225L7_0314),
                new QuestLocation("Necklace [#217]", EOB1.Spots.Necklace_217L7_1705),
                new QuestLocation("Potion of Healing [#243]", EOB1.Spots.PotionOfHealing_243L7_0426),
                new QuestLocation("Ring +2 [#226]", EOB1.Spots.Ring_226L7_1415),
                new QuestLocation("Ring of Wizardry [#233]", EOB1.Spots.Ring_233L7_2822),
                new QuestLocation("Rock +1 [#248]", EOB1.Spots.RockAlcove_248L7_1231),
                new QuestLocation("Shield [#250]", EOB1.Spots.Shield_250L7_3014),
                new QuestLocation("'Slicer' +3 [#231]", EOB1.Spots.SlicerPlus3_231L7_2422),
                new QuestLocation("Wand of Nothing [#249]", EOB1.Spots.WandAlcove_249L7_1331),
                new QuestLocation("Cleric Scroll of Cure Critical Wounds [#253]", EOB1.Spots.ClericScrollOfCureCriticalWounds_253L8_2803),
                new QuestLocation("Cleric Scroll of Hold Person [#258]", EOB1.Spots.ClericScrollOfHoldPerson_258L8_1209),
                new QuestLocation("Cleric Scroll of Neutralize Poison [#252]", EOB1.Spots.ClericScrollOfNeutralPoison_252L8_2803),
                new QuestLocation("Cleric Scroll of Prayer [#251]", EOB1.Spots.ClericScrollOfPrayer_251L8_2803),
                new QuestLocation("Cleric Scroll of Protection from Evil [#264]", EOB1.Spots.ClericScrollOfProtectEvil_264L8_0515),
                new QuestLocation("Cleric Scroll of Raise Dead [#267]", EOB1.Spots.ClericScrollOfRaiseDead_267L8_2115),
                new QuestLocation("Drow Boots [#265]", EOB1.Spots.DrowBoots_265L8_0715),
                new QuestLocation("Drow Bow [#262]", EOB1.Spots.DrowBow_262L8_1514),
                new QuestLocation("Drow Key (#6) [#263]", EOB1.Spots.DrowKey_263L8_2014),
                new QuestLocation("Drow Key (#6) [#269]", EOB1.Spots.DrowKey_269L8_2020),
                new QuestLocation("Drow Key (#6) [#275]", EOB1.Spots.DrowKey_275L8_2823),
                new QuestLocation("Flail [#274]", EOB1.Spots.Flail_274L8_0823),
                new QuestLocation("Gem (#1) [#385]", EOB1.Spots.Gem_385L8_2020),
                new QuestLocation("Jeweled Key (#7) [#270]", EOB1.Spots.JeweledKey_270L8_2121),
                new QuestLocation("Lock picks [#279]", EOB1.Spots.LockPicks_279L8_1229),
                new QuestLocation("Mage Scroll of Ice Storm [#278]", EOB1.Spots.MageScrollOfIceStorm_278L8_1428),
                new QuestLocation("Mage Scroll of Invisibility 10' Radius [#261]", EOB1.Spots.MageScrollOfInvisibility10_261L8_0914),
                new QuestLocation("Mage Scroll of Shield [#271]", EOB1.Spots.MageScrollOfShield_271L8_2121),
                new QuestLocation("Mage Scroll of Vampiric Touch [#447]", EOB1.Spots.MageScrollOfVampiricTouch_447L8_1514),
                new QuestLocation("Medallion [#254]", EOB1.Spots.Medallion_254L8_0504),
                new QuestLocation("'Night Stalker' +3 [#257]", EOB1.Spots.NightStalker_257L8_1808),
                new QuestLocation("Plate Mail of Great Beauty -3 [#273]", EOB1.Spots.PlateMailOfGreatBeauty_273L8_0423),
                new QuestLocation("Potion of Extra Healing [#266]", EOB1.Spots.PotionOfExtraHealing_266L8_2115),
                new QuestLocation("Ring of Adornment [#255]", EOB1.Spots.Ring_255L8_0504),
                new QuestLocation("Ring of Sustenance [#256]", EOB1.Spots.RingAlcove_256L8_0405),
                new QuestLocation("Robe [#276]", EOB1.Spots.Robe_276L8_0128),
                new QuestLocation("Rock +1 [#259]", EOB1.Spots.Rock_259L8_2812),
                new QuestLocation("Ruby Key (#8) [#260]", EOB1.Spots.RubyKey_260L8_1513),
                new QuestLocation("Ruby Key (#8) [#268]", EOB1.Spots.RubyKey_268L8_2516),
                new QuestLocation("Scepter of Kingly Might [#277]", EOB1.Spots.ScepterOfKinglyMight_277L8_0128),
                new QuestLocation("Wand of Lightning Bolt [#272]", EOB1.Spots.WandAlcove_272L8_2922),
                new QuestLocation("Adamantite Dart +4 [#436]", EOB1.Spots.AdamantiteDart_436L9_0709),
                new QuestLocation("Adamantite Dart +4 [#437]", EOB1.Spots.AdamantiteDart_437L9_0809),
                new QuestLocation("Adamantite Dart +4 [#438]", EOB1.Spots.AdamantiteDart_438L9_0909),
                new QuestLocation("Adamantite Dart +4 [#439]", EOB1.Spots.AdamantiteDart_439L9_1009),
                new QuestLocation("Adamantite Dart +4 [#440]", EOB1.Spots.AdamantiteDart_440L9_1109),
                new QuestLocation("Adamantite Dart +4 [#441]", EOB1.Spots.AdamantiteDart_441L9_0713),
                new QuestLocation("Adamantite Dart +4 [#442]", EOB1.Spots.AdamantiteDart_442L9_0813),
                new QuestLocation("Adamantite Dart +4 [#443]", EOB1.Spots.AdamantiteDart_443L9_0913),
                new QuestLocation("Adamantite Dart +4 [#444]", EOB1.Spots.AdamantiteDart_444L9_1013),
                new QuestLocation("Adamantite Dart +4 [#445]", EOB1.Spots.AdamantiteDart_445L9_1113),
                new QuestLocation("Arrow [#284]", EOB1.Spots.Arrow_284L9_2514),
                new QuestLocation("Arrow [#285]", EOB1.Spots.Arrow_285L9_2514),
                new QuestLocation("Arrow [#286]", EOB1.Spots.Arrow_286L9_2514),
                new QuestLocation("Chainmail [#301]", EOB1.Spots.Chainmail_301L9_3029),
                new QuestLocation("Cleric Scroll of Cure Serious Wounds [#289]", EOB1.Spots.ClericScrollOfCureSeriousWounds_289L9_0216),
                new QuestLocation("Cleric Scroll of Detect Magic [#281]", EOB1.Spots.ClericScrollOfDetectMagic_281L9_1203),
                new QuestLocation("Cleric Scroll of Dispel Magic [#288]", EOB1.Spots.ClericScrollOfDispelMagic_288L9_0216),
                new QuestLocation("Cleric Scroll of Flame Blade [#291]", EOB1.Spots.ClericScrollOfFlameBlade_291L9_1319),
                new QuestLocation("Cleric Scroll of Protection from Evil 10' Radius [#292]", EOB1.Spots.ClericScrollOfProtectEvil10_292L9_0120),
                new QuestLocation("Cleric Scroll of Raise Dead [#295]", EOB1.Spots.ClericScrollOfRaiseDead_295L9_1221),
                new QuestLocation("Cleric Scroll of Raise Dead [#300]", EOB1.Spots.ClericScrollOfRaiseDead_300L9_0828),
                new QuestLocation("Drow Boots [#296]", EOB1.Spots.DrowBoots_296L9_2821),
                new QuestLocation("Drow Key (#6) [#280]", EOB1.Spots.DrowKey_280L9_3002),
                new QuestLocation("Drow Key (#6) [#287]", EOB1.Spots.DrowKey_287L9_0915),
                new QuestLocation("Drow Shield +3 [#294]", EOB1.Spots.DrowShield_294L9_2420),
                new QuestLocation("Helmet [#350]", EOB1.Spots.Helmet_350L9_1111),
                new QuestLocation("Human Bones (Beohram) [#340]", EOB1.Spots.HumanBones_340L9_1111),
                new QuestLocation("Mage Scroll of Armor [#293]", EOB1.Spots.MageScrollOfArmor_293L9_1420),
                new QuestLocation("Mage Scroll of Invisibility [#290]", EOB1.Spots.MageScrollOfInvisibility_290L9_2918),
                new QuestLocation("Mage Scroll of Stoneskin [#283]", EOB1.Spots.MageScrollOfStoneskin_283L9_3008),
                new QuestLocation("Orb of Power [#382]", EOB1.Spots.OrbOfPower_382L9_2418),
                new QuestLocation("Orb of Power [#383]", EOB1.Spots.OrbOfPower_383L9_2418),
                new QuestLocation("Orb of Power [#384]", EOB1.Spots.OrbOfPower_384L9_2418),
                new QuestLocation("Paladin Holy Symbol [#351]", EOB1.Spots.PaladinHolySymbol_351L9_1111),
                new QuestLocation("Plate Mail [#347]", EOB1.Spots.PlateMail_347L9_1111),
                new QuestLocation("Potion of Extra Healing [#297]", EOB1.Spots.PotionOfExtraHealing_297L9_0322),
                new QuestLocation("Potion of Poison [#282]", EOB1.Spots.PotionOfPoison_282L9_1506),
                new QuestLocation("'Severious' +5 [#349]", EOB1.Spots.Severious_349L9_1111),
                new QuestLocation("Shield [#348]", EOB1.Spots.Shield_348L9_1111),
                new QuestLocation("Spear [#298]", EOB1.Spots.Spear_298L9_1423),
                new QuestLocation("Wand of Fireball [#299]", EOB1.Spots.Wand_299L9_2026),
                new QuestLocation("Arrow [#311]", EOB1.Spots.Arrow_311L10_0411),
                new QuestLocation("Arrow [#312]", EOB1.Spots.Arrow_312L10_1524),
                new QuestLocation("Cleric Scroll of Cure Critical Wounds [#308]", EOB1.Spots.ClericScrollOfCureCriticalWoundsAlcove_308L10_2729),
                new QuestLocation("Cleric Scroll of Flame Blade [#307]", EOB1.Spots.ClericScrollOfFlameBladeAlcove_307L10_2729),
                new QuestLocation("Cleric Scroll of Flame Blade [#316]", EOB1.Spots.ClericScrollOfFlameBladeAlcove_316L10_1828),
                new QuestLocation("Cleric Scroll of Neutralize Poison [#318]", EOB1.Spots.ClericScrollOfNeutralPoison_318L10_1913),
                new QuestLocation("Cleric Scroll of Remove Paralysis [#317]", EOB1.Spots.ClericScrollOfRemoveParalysisAlcove_317L10_1828),
                new QuestLocation("Human Bones (Tyrra) [#341]", EOB1.Spots.HumanBones_341L10_0312),
                new QuestLocation("Mage Scroll of Cone of Cold [#319]", EOB1.Spots.MageScrollOfConeOfCold_319L10_2522),
                new QuestLocation("Plate Mail [#304]", EOB1.Spots.PlateMail_304L10_0228),
                new QuestLocation("Potion of Giant Strength [#315]", EOB1.Spots.PotionOfGiantStrengthAlcove_315L10_1211),
                new QuestLocation("Potion of Poison [#305]", EOB1.Spots.PotionOfPoisonAlcove_305L10_2725),
                new QuestLocation("Ring of Feather Fall [#314]", EOB1.Spots.RingAlcove_314L10_1611),
                new QuestLocation("Skull Key (#5) [#313]", EOB1.Spots.SkullKey_313L10_0312),
                new QuestLocation("Wand of Frost [#306]", EOB1.Spots.WandAlcove_306L10_2727),
                new QuestLocation("Wand of Nothing [#309]", EOB1.Spots.Wand_309L10_1427),
                new QuestLocation("Banded Armor +3 [#330]", EOB1.Spots.BandedArmor_330L11_0101),
                new QuestLocation("Bracers +2 [#344]", EOB1.Spots.Bracers_344L11_1627),
                new QuestLocation("Cleric Scroll of Cure Serious Wounds [#333]", EOB1.Spots.ClericScrollOfCureSeriousWounds_333L11_2316),
                new QuestLocation("Cleric Scroll of Raise Dead [#322]", EOB1.Spots.ClericScrollOfRaiseDead_322L11_0506),
                new QuestLocation("Cleric Scroll of Raise Dead [#326]", EOB1.Spots.ClericScrollOfRaiseDead_326L11_2501),
                new QuestLocation("Drow Key (#6) [#324]", EOB1.Spots.DrowKey_324L11_2615),
                new QuestLocation("Drow Key (#6) [#337]", EOB1.Spots.DrowKey_337L11_1624),
                new QuestLocation("'Flicka' +5 [#336]", EOB1.Spots.Flicka_336L11_1627),
                new QuestLocation("Human Bones (Kirath) [#342]", EOB1.Spots.HumanBones_342L11_1627),
                new QuestLocation("Mage Scroll of Hold Monster [#332]", EOB1.Spots.MageScrollOfHoldMonster_332L11_0117),
                new QuestLocation("Medallion +1 [#321]", EOB1.Spots.Medallion_321L11_0506),
                new QuestLocation("Orb of Power +1 [#325]", EOB1.Spots.OrbOfPower_325L11_2830),
                new QuestLocation("Ring of Adornment [#331]", EOB1.Spots.Ring_331L11_1204),
                new QuestLocation("Ring +2 [#343]", EOB1.Spots.Ring_343L11_1627),
                new QuestLocation("Robe +5 [#335]", EOB1.Spots.Robe_335L11_1627),
                new QuestLocation("Rock +2 [#327]", EOB1.Spots.Rock_327L11_2501),
                new QuestLocation("Rock +2 [#328]", EOB1.Spots.Rock_328L11_0911),
                new QuestLocation("'Slasher' +4 [#329]", EOB1.Spots.SlasherPlus4_329L11_0101),
                new QuestLocation("Spell Book [#435]", EOB1.Spots.SpellBook_435L11_1627),
                new QuestLocation("Stone Holy Symbol (#1) [#310]", EOB1.Spots.StoneHolySymbol_310L11_1727),
                new QuestLocation("Stone Orb (#3) [#323]", EOB1.Spots.StoneOrb_323L11_2615),
                new QuestLocation("Wand of Lightning Bolt [#320]", EOB1.Spots.Wand_320L11_0308),
                new QuestLocation("Dwarven Healing Potion [#52]", EOB1.Spots.DwarvenHealingPotionAlcove_52L11_2417),
                new QuestLocation("Iron Rations [#396]", EOB1.Spots.IronRations_396L12_2802),
                new QuestLocation("Iron Rations [#397]", EOB1.Spots.IronRations_397L12_2902),
                new QuestLocation("Iron Rations [#398]", EOB1.Spots.IronRations_398L12_2803),
                new QuestLocation("Iron Rations [#399]", EOB1.Spots.IronRations_399L12_2903),
                new QuestLocation("Necklace [#389]", EOB1.Spots.Necklace_389L12_0717),
                new QuestLocation("Orb of Power [#391]", EOB1.Spots.OrbOfPowerAlcove_391L12_2713),
                new QuestLocation("Orb of Power [#393]", EOB1.Spots.OrbOfPower_393L12_1302),
                new QuestLocation("Orb of Power [#394]", EOB1.Spots.OrbOfPower_394L12_1302),
                new QuestLocation("Orb of Power [#395]", EOB1.Spots.OrbOfPower_395L12_1302),
                new QuestLocation("Potion of Extra Healing [#386]", EOB1.Spots.PotionOfExtraHealing_386L12_0715),
                new QuestLocation("Potion of Extra Healing [#387]", EOB1.Spots.PotionOfExtraHealing_387L12_0717),
                new QuestLocation("Potion of Invisibility [#401]", EOB1.Spots.PotionOfInvisibilityAlcove_401L12_0923),
                new QuestLocation("Potion of Invisibility [#402]", EOB1.Spots.PotionOfInvisibilityAlcove_402L12_1123),
                new QuestLocation("Potion of Invisibility [#405]", EOB1.Spots.PotionOfInvisibility_405L12_1703),
                new QuestLocation("Potion of Invisibility [#406]", EOB1.Spots.PotionOfInvisibility_406L12_1703),
                new QuestLocation("Potion of Speed [#392]", EOB1.Spots.PotionOfSpeedAlcove_392L12_2709),
                new QuestLocation("Potion of Vitality [#403]", EOB1.Spots.PotionOfVitalityAlcove_403L12_0923),
                new QuestLocation("Potion of Vitality [#404]", EOB1.Spots.PotionOfVitalityAlcove_404L12_1123),
                new QuestLocation("Ring of Adornment [#388]", EOB1.Spots.Ring_388L12_0715),
                new QuestLocation("Skull Key (#5) [#400]", EOB1.Spots.SkullKey_400L12_0716),
                new QuestLocation("Stone Dagger (#4) [#410]", EOB1.Spots.StoneDaggerAlcove_410L12_2818),
                new QuestLocation("Stone Holy Symbol (#1) [#414]", EOB1.Spots.StoneHolySymbolAlcove_414L12_2818),
                new QuestLocation("Stone Medallion (#5) [#411]", EOB1.Spots.StoneMedallionAlcove_411L12_2818),
                new QuestLocation("Stone Necklace (#2) [#412]", EOB1.Spots.StoneNecklaceAlcove_412L12_2818),
                new QuestLocation("Stone Orb (#3) [#415]", EOB1.Spots.StoneOrbAlcove_415L12_2818),
                new QuestLocation("Stone Ring (#6) [#413]", EOB1.Spots.StoneRingAlcove_413L12_2818),
                new QuestLocation("Stone Scepter (#6) [#409]", EOB1.Spots.StoneScepterAlcove_409L12_2818),
                new QuestLocation("Wand of Fireball [#390]", EOB1.Spots.WandAlcove_390L12_1913),
                new QuestLocation("Wand of Magic Missile [#407]", EOB1.Spots.Wand_407L12_2302)
                );
            AddSideQuest(Loot, quests);

            return quests.ToArray();
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            EOB1QuestData eobData = data as EOB1QuestData;
            if (eobData == null)
                return;

            EOBPartyInfo party = data.Party as EOBPartyInfo;
            EOB1GameInfo gameInfo = data.Info as EOB1GameInfo;
            LocationInformation location = data.Location;
            EOBGameState state = eobData.State as EOBGameState;
            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;
            EOB1Character EOB1Char = EOBCharacter.Create(state.Game, null, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as EOB1Character;
            EOB1QuestData questData = data as EOB1QuestData;
            EOB1MapData mapData = questData.Map as EOB1MapData;
            QuestTotals totals = new QuestTotals(0, 0);
            CharName = EOB1Char.Name;
            CharAddress = iOverrideCharAddress;
            PartyLocation = eobData.Location.PrimaryCoordinates;
            MapXY spot = new MapXY(GameNames.EyeOfTheBeholder1, eobData.Location.MapIndex, PartyLocation.X, PartyLocation.Y);
            EOB1Map map = (EOB1Map)spot.Map;
            RationsCounter = questData.Effects.RationsCounter;

            // Add information to the QuestStatus objects to indicate their completion status
            Descend2.AddObj(
                DoorOpen(mapData, EOB1.Spots.DoorL1_1413, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_1611, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_1813, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_2213, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_2213, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_1418, Direction.Right),
                ItemOnGround(mapData, EOB1.Spots.PlateL1_2217),
                DoorOpen(mapData, EOB1.Spots.DoorL1_1918, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_2224, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorL1_0822, Direction.Down),
                ItemOnGround(mapData, EOB1.Spots.PlateL1_1623),
                DoorOpen(mapData, EOB1.Spots.ExitDoorL1_1723, Direction.Left));
            Descend2.AddPost(map > EOB1Map.Level1);
            Descend2.MarkAllWhenComplete = true;
            AddQuest(totals, Descend2);

            bool bPartySilverKey = AnyCharHasItemType(party, EOBItemIndex.Key, mapData.ItemList, 1);
            bool bPartyGoldKey = AnyCharHasItemType(party, EOBItemIndex.Key, mapData.ItemList, 2);
            bool bDoorSilver2123 = DoorOpen(mapData, EOB1.Spots.DoorSilverKeyL2_2123, Direction.Left);
            bool bDoorSilver1921 = DoorOpen(mapData, EOB1.Spots.DoorSilverKeyL2_1921, Direction.Down);
            bool bDoorSilver1925 = DoorOpen(mapData, EOB1.Spots.DoorSilverKeyL2_1925, Direction.Up);
            bool bDoor2018Open = DoorOpen(mapData, EOB1.Spots.DoorForceL2_2018, Direction.Left);
            bool bDoor1925Open = DoorOpen(mapData, EOB1.Spots.DoorSilverKeyL2_1925, Direction.Up);
            bool bInGoldKeyRoom = Global.PointInRects(PartyLocation, 3,15,7,3, 6,17,4,2);
            bool bGoldDoorOpen = DoorOpen(mapData, EOB1.Spots.GoldDoorL2_2805, Direction.Down);
            Descend3.AddObj(bPartySilverKey || bDoorSilver2123,
                bDoorSilver2123,
                !HasFloorHole(mapData, EOB1.Spots.FloorHoleL2_2323, Direction.Left),
                !HasFloorHole(mapData, EOB1.Spots.FloorHoleL2_2422, Direction.Down),
                IsOpen(mapData, EOB1.Spots.SilverWallL2_1820, Direction.Right),
                bPartySilverKey || bDoorSilver1921,
                bDoorSilver1921,
                bDoor2018Open || IsForceable(mapData, EOB1.Spots.DoorForceL2_2018, Direction.Left),
                bDoor2018Open,
                DoorOpen(mapData, EOB1.Spots.DoorDaggerL2_2316, Direction.Left),
                IsOpen(mapData, EOB1.Spots.WallRemovableL2_2416, Direction.Down),
                IsOpen(mapData, EOB1.Spots.WallRemovableL2_2614, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.DoorForceL2_2713, Direction.Left),
                DoorOpen(mapData, EOB1.Spots.DoorForceL2_2815, Direction.Up),
                IsOpen(mapData, EOB1.Spots.SilverWallL2_1720, Direction.Right),
                bPartySilverKey || bDoor1925Open,
                bDoor1925Open,
                IsOpen(mapData, EOB1.Spots.SilverWallL2_1620, Direction.Right),
                bGoldDoorOpen || bPartyGoldKey || Global.PointInRects(PartyLocation, 1,18,6,9) || bInGoldKeyRoom,
                bGoldDoorOpen || bPartyGoldKey || bInGoldKeyRoom,
                bGoldDoorOpen || DoorOpen(mapData, EOB1.Spots.DoorForceL2_0715, Direction.Right),
                bGoldDoorOpen || bPartyGoldKey,
                bGoldDoorOpen || (!bInGoldKeyRoom && bPartyGoldKey),
                bGoldDoorOpen);
            Descend3.AddPost(map > EOB1Map.Level2);
            Descend3.MarkAllWhenComplete = true;
            AddQuest(totals, Descend3);

            bool bGem97 = mapData.ItemInSpot(EOB1ItemTableDefault.Gem_97, EOB1.Spots.Gem_97L3_1003);
            bool bGem98 = mapData.ItemInSpot(EOB1ItemTableDefault.Gem_98, EOB1.Spots.Gem_98L3_0306);
            bool bGem106 = mapData.ItemInSpot(EOB1ItemTableDefault.Gem_106, EOB1.Spots.Gem_106L3_1310);
            bool bGem107 = mapData.ItemInSpot(EOB1ItemTableDefault.Gem_107, EOB1.Spots.Gem_107L3_0613);
            int iSocket1 = mapData.GetWallIndex(7, 5, Direction.Left);
            int iSocket2 = mapData.GetWallIndex(11, 7, Direction.Up);
            int iSocket3 = mapData.GetWallIndex(5, 9, Direction.Down);
            int iSocket4 = mapData.GetWallIndex(9, 11, Direction.Right);
            bool bOpenL3_1010S = mapData.GetWall(10, 10, Direction.Down).IsOpen;
            Descend4.AddObj(!bGem97, !bGem98, !bGem106, !bGem107,
                iSocket1 != 32 || bOpenL3_1010S,
                iSocket2 != 32 || bOpenL3_1010S,
                iSocket3 != 32 || bOpenL3_1010S,
                iSocket4 != 32 || bOpenL3_1010S);
            Descend4.AddPost(map > EOB1Map.Level3 || IsOpen(mapData, EOB1.Spots.Wall_L3_1010, Direction.Down));
            Descend4.MarkAllWhenComplete = true;
            AddQuest(totals, Descend4);

            Descend5.AddObj(AnyCharHasItemType(party, EOBItemIndex.Key, mapData.ItemList, 3) || map > EOB1Map.Level4,
                IsWallType(mapData, EOB1.Spots.Pit_L4_1203, Direction.Right, 0) || map > EOB1Map.Level4,
                IsWallType(mapData, EOB1.Spots.Pit_L4_1103, Direction.Right, 0) || map > EOB1Map.Level4,
                IsWallType(mapData, EOB1.Spots.Pit_L4_1003, Direction.Right, 0) || map > EOB1Map.Level4);
            Descend5.AddPost(map > EOB1Map.Level4);
            Descend6.MarkAllWhenComplete = true;
            AddQuest(totals, Descend5);

            bool bOver5 = map > EOB1Map.Level5;
            bool bL5Door1 = DoorOpen(mapData, EOB1.Spots.Door_L5_2802, Direction.Left);
            bool bL5Door2 = DoorOpen(mapData, EOB1.Spots.Door_L5_2906, Direction.Left);
            bool bL5Door3 = DoorOpen(mapData, EOB1.Spots.Door_L5_2805, Direction.Down);
            bool bL5Shortcut = IsOpen(mapData, EOB1.Spots.Wall_L5_1916, Direction.Right);
            bool bAfterKeyTeleporter = location.In(19, 1, 12, 4, 20, 5, 11, 13, 22, 18, 9, 6);
            bool bL6Area1 = location.In(19, 1, 12, 5, 21, 6, 4, 4, 25, 6, 2, 2);
            bool bL6Area2 = location.In(22, 7, 2, 2);
            bool bL6Area3 = location.PrimaryCoordinates == new Point(23, 7);
            bool bL6Area4 = location.In(19, 1, 5, 5);
            bool bL6Area5 = location.In(25, 1, 6, 6);
            Descend6.AddObj(!mapData.ItemInSpot(EOB1ItemTableDefault.Key_176, EOB1.Spots.Key_176L5_2826) || bL5Shortcut,
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_179, EOB1.Spots.Key_179L5_1428) || bL5Shortcut,
                DoorOpen(mapData, EOB1.Spots.Door_L5_2427, Direction.Left) || bL5Shortcut,
                IsOpen(mapData, EOB1.Spots.Teleporter_L5_2627, Direction.Left) || bL5Shortcut,
                bAfterKeyTeleporter || bL5Shortcut,
                location.In(21, 6, 1, 2) || bL5Door1,
                bL5Door1,
                location.PrimaryCoordinates == new Point(21,9) || bL5Door2,
                bL5Door2,
                (bAfterKeyTeleporter && !bL6Area1) || bL6Area2 || bL6Area3 || bL6Area4 || bL6Area5 || bL5Door3,
                bL6Area2 || bL6Area3 || bL6Area4 || bL6Area5 ||bL5Door3,
                bL6Area3 || bL6Area4 || bL6Area5 || bL5Door3,
                bL6Area4 || bL6Area5 || bL5Door3,
                bL6Area5 || bL5Door3,
                bL5Door3);
            Descend6.AddPost(bOver5);
            Descend6.MarkAllWhenComplete = true;
            AddQuest(totals, Descend6);

            bool bOver6 = map > EOB1Map.Level6;
            bool bL6CrossS = IsOpen(mapData, EOB1.Spots.LockWall_L6_0420, Direction.Down);
            bool bL6CrossE = IsOpen(mapData, EOB1.Spots.LockWall_L6_0519, Direction.Right);
            bool bL6CrossN = IsOpen(mapData, EOB1.Spots.LockWall_L6_0418, Direction.Up);
            bool bSilverware1 = IsOpen(mapData, EOB1.Spots.Silverware_L6_0629, Direction.Left);
            bool bSilverware2 = IsOpen(mapData, EOB1.Spots.Silverware_L6_0729, Direction.Left);
            bool bHasDarts = AnyCharHasItemType(party, EOBItemIndex.Dart, mapData.ItemList);
            bool bWeaponDoor = DoorOpen(mapData, EOB1.Spots.Door_L6_2604, Direction.Left);
            Descend7.AddObj(bWeaponDoor || ItemTypesOnGround(mapData, EOB1.Spots.Weapon_L6_2503, EOBItemIndex.Dagger, EOBItemIndex.ShortSword,
                    EOBItemIndex.Halberd, EOBItemIndex.MaceScepter, EOBItemIndex.Flail, EOBItemIndex.Axe, EOBItemIndex.Spear),
                bWeaponDoor || ItemTypesOnGround(mapData, EOB1.Spots.Weapon_L6_2505, EOBItemIndex.Dagger, EOBItemIndex.ShortSword,
                    EOBItemIndex.Halberd, EOBItemIndex.MaceScepter, EOBItemIndex.Flail, EOBItemIndex.Axe, EOBItemIndex.Spear),
                DoorOpen(mapData, EOB1.Spots.Door_L6_2911, Direction.Up),
                DoorOpen(mapData, EOB1.Spots.Door_L6_2915, Direction.Up),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_199, EOB1.Spots.Key_199L6_1922),
                DoorOpen(mapData, EOB1.Spots.Door_L6_2607, Direction.Right),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_192, EOB1.Spots.DwarvenKey_192L6_0106),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_193, EOB1.Spots.DwarvenKey_193L6_0706),
                bHasDarts,
                bHasDarts,
                bHasDarts,
                bHasDarts,
                bSilverware1,
                bSilverware2,
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_211, EOB1.Spots.DwarvenKey_211L6_0829),
                bL6CrossN || bL6CrossE || bL6CrossS,
                bL6CrossE || bL6CrossS,
                bL6CrossS);
            Descend7.AddPost(bOver6);
            Descend7.MarkAllWhenComplete = true;
            AddQuest(totals, Descend7);

            bool bJewelKey = AnyCharHasItemType(party, EOBItemIndex.Key, mapData.ItemList, 7);
            bool bGem = AnyCharHasItemType(party, EOBItemIndex.Gem, mapData.ItemList);
            bool bL8Area1 = map == EOB1Map.Level8 && location.In(13, 13, 3, 7, 16, 16, 4, 4, 20, 19, 1, 2);
            bool bL9Area1 = map == EOB1Map.Level9 && location.In(16, 13, 7, 4, 19, 17, 6, 3, 22, 20, 2, 4);
            bool bL7Area1 = map == EOB1Map.Level7 && location.In(12, 20, 4, 1);
            bool bL7Area2 = map == EOB1Map.Level8 && location.In(9, 25, 22, 6, 12, 21, 19, 10, 17, 19, 14, 2, 22, 16, 1, 3);
            bool bL8Area2 = map == EOB1Map.Level8 && location.In(15, 21, 8, 5);
            bool bL8Area3 = map == EOB1Map.Level8 && location.In(18, 13, 3, 2);
            bool bL8Area4 = map == EOB1Map.Level8 && location.In(23, 16, 5, 9);
            bool bL8Area5 = map == EOB1Map.Level8 && !location.In(12, 13, 12, 9, 14, 16, 15, 10);
            bool bL9Area2 = map == EOB1Map.Level9 && !location.In(15, 13, 9, 11, 13, 19, 2, 5, 24, 18, 1, 1);
            bool bHasRock = AnyCharHasItemType(party, EOBItemIndex.Rock, mapData.ItemList);
            bool bOver9 = map > EOB1Map.Level9;
            Descend10.AddObj(
                gameInfo.GlobalFlag(EOB1Bits.Game.L7_DealtWithDrow),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_242, EOB1.Spots.Key4_L7_0426),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_224, EOB1.Spots.Key4_L7_3012),
                bL8Area1,
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_269, EOB1.Spots.DrowKey_269L8_2020),
                bL7Area1,
                DoorOpen(mapData, EOB1.Spots.Door_L7_1420, Direction.Left),
                bL8Area2,
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_270, EOB1.Spots.JewelKey_L8_2121),
                bL9Area1,
                DoorOpen(mapData, EOB1.Spots.Door_L9_1614, Direction.Down),
                bL8Area3,
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_263, EOB1.Spots.DrowKey_263L8_2014),
                DoorOpen(mapData, EOB1.Spots.Door_L9_2214, Direction.Down),
                ItemTypeOnGround(mapData, EOBItemIndex.Gem, EOB1.Spots.GemAlcove_L9_1818),
                ItemTypeOnGround(mapData, EOBItemIndex.Key, EOB1.Spots.GemAlcove_L9_1818),
                bJewelKey,
                DoorOpen(mapData, EOB1.Spots.Door_L9_2221, Direction.Up),
                bL8Area4,
                ItemTypeOnGround(mapData, EOBItemIndex.Key, EOB1.Spots.KeyAlcove_L8_2819),
                ItemTypeOnGround(mapData, EOBItemIndex.Gem, EOB1.Spots.KeyAlcove_L8_2819),
                bGem,
                DoorOpen(mapData, EOB1.Spots.Door_L8_2517, Direction.Up),
                !mapData.ItemInSpot(EOB1ItemTableDefault.RubyKey_268, EOB1.Spots.RubyKey_268L8_2516),
                bL7Area2,
                DoorOpen(mapData, EOB1.Spots.Door_L7_2219, Direction.Up),
                DoorOpen(mapData, EOB1.Spots.Door_L7_2223, Direction.Up),
                IsOpen(mapData, EOB1.Spots.Wall_L7_2127, Direction.Up),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1525, Direction.Right),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_240, EOB1.Spots.DrowKey_240L7_1425),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1726, Direction.Up),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_244, EOB1.Spots.DrowKey_244L7_1727),
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_235, EOB1.Spots.JeweledKey_235L7_1923),
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_246, EOB1.Spots.JeweledKey_246L7_2527),
                !mapData.ItemInSpot(EOB1ItemTableDefault.RubyKey_247, EOB1.Spots.RubyKey_247L7_2727),
                DoorOpen(mapData, EOB1.Spots.Door_L7_2130, Direction.Right),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1930, Direction.Right),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1730, Direction.Right),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1530, Direction.Right),
                DoorOpen(mapData, EOB1.Spots.Door_L7_1029, Direction.Down),
                bL8Area5,
                bL9Area2,
                bHasRock,
                bHasRock,
                DoorOpen(mapData, EOB1.Spots.Door_L9_2803, Direction.Right),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_280, EOB1.Spots.DrowKey_280L9_3002),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_287, EOB1.Spots.DrowKey_287L9_0915),
                DoorOpen(mapData, EOB1.Spots.Door_L9_0619, Direction.Right),
                IsOpen(mapData, EOB1.Spots.Wall_L9_0115, Direction.Up),
                IsOpen(mapData, EOB1.Spots.Wall_L9_0419, Direction.Up),
                DoorOpen(mapData, EOB1.Spots.Door_L9_0319, Direction.Right)
            );
            Descend10.AddPost(bOver9);
            Descend10.MarkAllWhenComplete = true;
            AddQuest(totals, Descend10);

            bool bOpen1413 = IsOpen(mapData, EOB1.Spots.Wall_L10_1413, Direction.Up);
            bool bOpen1414 = IsOpen(mapData, EOB1.Spots.Wall_L10_1414, Direction.Up);
            bool bOpen1415 = IsOpen(mapData, EOB1.Spots.Wall_L10_1415, Direction.Up);
            bool bL10Area1 = map == EOB1Map.Level10 && location.In(13, 11, 3, 4);
            Descend11.AddObj(
                bOpen1415 ||  bL10Area1,
                (bOpen1415 && bOpen1414) || bL10Area1,
                bOpen1413 && bL10Area1,
                bOpen1413 && bOpen1414 && bL10Area1
            );
            Descend11.AddPost(map > EOB1Map.Level10);
            Descend11.MarkAllWhenComplete = true;
            AddQuest(totals, Descend11);

            bool bStarOpenInnerNorth = IsOpen(mapData, EOB1.Spots.InnerStarNorth_L11_1409, Direction.Up);
            bool bStarOpenInnerEast = IsOpen(mapData, EOB1.Spots.InnerStarEast_L11_1611, Direction.Right);
            bool bStarOpenInnerSouth = IsOpen(mapData, EOB1.Spots.InnerStarSouth_L11_1413, Direction.Down);
            bool bStarOpenInnerWest = IsOpen(mapData, EOB1.Spots.InnerStarWest_L11_1211, Direction.Left);
            bool bStarOpenMidNorth = IsOpen(mapData, EOB1.Spots.MidStarNorth_L11_1407, Direction.Up);
            bool bStarOpenMidEast = IsOpen(mapData, EOB1.Spots.MidStarEast_L11_1811, Direction.Right);
            bool bStarOpenMidSouth = IsOpen(mapData, EOB1.Spots.MidStarSouth_L11_1415, Direction.Down);
            bool bStarOpenMidWest = IsOpen(mapData, EOB1.Spots.MidStarWest_L11_1011, Direction.Left);
            bool bStarOpenOuterNorth = IsOpen(mapData, EOB1.Spots.OuterStarNorth_L11_1405, Direction.Up);
            bool bStarOpenOuterEast = IsOpen(mapData, EOB1.Spots.OuterStarEast_L11_2011, Direction.Right);
            bool bStarOpenOuterSouth = IsOpen(mapData, EOB1.Spots.OuterStarSouth_L11_1417, Direction.Down);
            //bool bStarOpenOuterWest = IsOpen(mapData, EOB1.Spots.OuterStarWest_L11_0811, Direction.Left);
            //bool bInL11Star = map == EOB1Map.Level11 && location.In(8, 5, 13, 13);
            //bool bInL11North = map == EOB1Map.Level11 && location.In(12, 1, 19, 2, 14, 3, 17, 2, 21, 5, 10, 6, 23, 11, 8, 6);
            //bool bInL11East = map == EOB1Map.Level11 && location.In(21, 11, 2, 6, 16, 17, 15, 14);
            bool bInL11South = map == EOB1Map.Level11 && location.In(1, 4, 5, 27, 6, 13, 3, 18, 9, 18, 7, 13);
            bool bHaveNorthKey = !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_324, EOB1.Spots.DrowKey_324L11_2615);
            bool bHaveNorthOrb = !mapData.ItemInSpot(EOB1ItemTableDefault.StoneOrb_323, EOB1.Spots.StoneOrb_323L11_2615);
            bool bKeyOrb = bHaveNorthKey && bHaveNorthOrb;
            bool bEastKey = !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_337, EOB1.Spots.DrowKey_337L11_1624);
            Descend12.AddObj(
                bKeyOrb || bStarOpenInnerNorth || bStarOpenInnerWest || bStarOpenInnerSouth,
                bKeyOrb || bStarOpenInnerNorth || bStarOpenInnerWest,
                bKeyOrb || bStarOpenInnerNorth,
                bKeyOrb || bStarOpenMidNorth || bStarOpenMidWest || bStarOpenMidSouth,
                bKeyOrb || bStarOpenMidNorth || bStarOpenMidWest,
                bKeyOrb || bStarOpenMidNorth,
                bKeyOrb || bStarOpenOuterNorth,
                IsOpen(mapData, EOB1.Spots.Wall_L11_2714, Direction.Up),
                bHaveNorthKey,
                bHaveNorthOrb,
                bEastKey || bStarOpenInnerWest || bStarOpenInnerNorth || bStarOpenInnerEast,
                bEastKey || bStarOpenInnerNorth || bStarOpenInnerEast,
                bEastKey || bStarOpenInnerEast,
                bEastKey || bStarOpenMidWest || bStarOpenMidNorth || bStarOpenMidEast,
                bEastKey || bStarOpenMidNorth || bStarOpenMidEast,
                bEastKey || bStarOpenMidEast,
                bEastKey || bStarOpenOuterEast,
                IsOpen(mapData, EOB1.Spots.Wall_L11_2223, Direction.Up),
                bEastKey,
                bInL11South || bStarOpenInnerNorth || bStarOpenInnerEast || bStarOpenInnerSouth,
                bInL11South || bStarOpenInnerEast || bStarOpenInnerSouth,
                bInL11South || bStarOpenInnerSouth,
                bInL11South || bStarOpenMidNorth || bStarOpenMidEast || bStarOpenMidSouth,
                bInL11South || bStarOpenMidEast || bStarOpenMidSouth,
                bInL11South || bStarOpenMidSouth,
                bInL11South || bStarOpenOuterSouth,
                DoorOpen(mapData, EOB1.Spots.Door_L11_0818, Direction.Left),
                IsOpen(mapData, EOB1.Spots.Wall_L11_0323, Direction.Up) || IsOpen(mapData, EOB1.Spots.Wall_L11_0828, Direction.Right),
                DoorOpen(mapData, EOB1.Spots.Door_L11_0426, Direction.Up)
            );
            Descend12.AddPost(map > EOB1Map.Level11);
            Descend12.MarkAllWhenComplete = true;
            AddQuest(totals, Descend12);

            Beholder.AddObj(
                IsOpen(mapData, EOB1.Spots.Wall_L12_1211, Direction.Right),
                IsOpen(mapData, EOB1.Spots.Wall_L12_0709, Direction.Right)
            );
            Beholder.AddPost(false);
            Beholder.MarkAllWhenComplete = true;
            AddQuest(totals, Beholder);

            bool bDwarvenHealing = AnyCharHasItemType(party, EOBItemIndex.DwarvenHealingPotion, mapData.ItemList);
            bool bGaveHealing = gameInfo.GlobalFlag(EOB1Bits.Game.L5_HelpedArmun);
            Armun.AddObj(AnyCharHasItemType(party, EOBItemIndex.Key, mapData.ItemList, 3) || bOver5,
                DoorOpen(mapData, EOB1.Spots.Door_L5_0718, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.Door_L5_1018, Direction.Down),
                DoorOpen(mapData, EOB1.Spots.Door_L5_1318, Direction.Down),
                gameInfo.GlobalFlag(EOB1Bits.Game.L5_AgreeHelpArmun),
                bGaveHealing || bDwarvenHealing || map == EOB1Map.Level11,
                bGaveHealing || bDwarvenHealing);
            Armun.AddPost(bGaveHealing);
            AddQuest(totals, Armun);

            Special1.AddObj(AnyCharHasItemType(party, EOBItemIndex.Dagger, mapData.ItemList));
            Special1.AddPost(gameInfo.GlobalFlag(EOB1Bits.Game.SQ1Guinsoo));
            Special1.MarkAllWhenComplete = true;
            AddQuest(totals, Special1);

            bool bTaghorFinished = gameInfo.GlobalFlag(EOB1Bits.Game.L4_HelpTaghor) ||
                gameInfo.LevelFlag(3, EOB1Bits.Level.L4_AbandonTaghor);
            bool bTaghorTalk = gameInfo.GlobalFlag(EOB1Bits.Game.L4_TalkTaghor);
            Taghor.AddObj(gameInfo.LevelFlag(3, EOB1Bits.Level.L4_EncounterTaghor) || bTaghorFinished,
                bTaghorTalk || bTaghorFinished,
                bTaghorFinished,
                bTaghorFinished);
            AddQuest(totals, Taghor);

            bool bL2Dagger1 = gameInfo.LevelFlag(1, EOB1Bits.Level.L2_1429Dagger);
            bool bL2Dagger2 = gameInfo.LevelFlag(1, EOB1Bits.Level.L2_2130Dagger);
            bool bL2Dagger3 = gameInfo.LevelFlag(1, EOB1Bits.Level.L2_2216Dagger);
            bool bL2Dagger4 = gameInfo.LevelFlag(1, EOB1Bits.Level.L2_0930Dagger);
            Special2.AddObj(AnyCharHasItemType(party, EOBItemIndex.Dagger, mapData.ItemList) ||
                (bL2Dagger1 && bL2Dagger2 && bL2Dagger3 && bL2Dagger4),
                bL2Dagger1, bL2Dagger2, bL2Dagger3, bL2Dagger4);
            AddQuest(totals, Special2);

            bool bSpecial3 = gameInfo.GlobalFlag(EOB1Bits.Game.SQ3Gems);
            Special3.AddObj((bOpenL3_1010S && iSocket1 == 32) || bSpecial3,
                (bOpenL3_1010S && iSocket2 == 32) || bSpecial3,
                (bOpenL3_1010S && iSocket3 == 32) || bSpecial3,
                (bOpenL3_1010S && iSocket4 == 32) || bSpecial3);
            AddQuest(totals, Special3);

            bool bSpecial4 = gameInfo.GlobalFlag(EOB1Bits.Game.SQ4Chain);
            Special4.AddObj(!ItemOnGround(mapData, EOB1.Spots.DwarvenKey_303L4_1508) || bSpecial4,
                !IsOpen(mapData, EOB1.Spots.DwarvenKey_303L4_1508, Direction.Down) || bSpecial4,
                bSpecial4);
            AddQuest(totals, Special4);

            Special5.AddObj(gameInfo.GlobalFlag(EOB1Bits.Game.SQ5Rations));
            AddQuest(totals, Special5);

            Special6.AddObj(party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_182, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_182, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_183, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_183, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_184, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_184, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_185, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_185, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_186, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_186, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_187, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_187, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_188, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_188, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_189, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_189, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_190, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_190, EOB1.Spots.KenkuNest_L6_1317),
                party.AnyCharHasItem(EOB1ItemTableDefault.KenkuEgg_191, mapData.ItemList) || mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_191, EOB1.Spots.KenkuNest_L6_1317),
                gameInfo.GlobalFlag(EOB1Bits.Game.SQ6Nest));
            AddQuest(totals, Special6);

            bool bPartyStone = AnyCharHasItemType(party, EOBItemIndex.Stone, mapData.ItemList);
            bool bSQ7Stone = gameInfo.GlobalFlag(EOB1Bits.Game.SQ7Stone);
            Special7.AddObj(bPartyStone || bSQ7Stone,
                ItemTypeOnGround(mapData, EOBItemIndex.Stone, EOB1.Spots.StoneAlcove_L7_1231) || bSQ7Stone,
                ItemTypeOnGround(mapData, EOBItemIndex.Stone, EOB1.Spots.StoneAlcove_L7_1331) || bSQ7Stone,
                ItemTypeOnGround(mapData, EOBItemIndex.Stone, EOB1.Spots.StoneAlcove_L7_1431) || bSQ7Stone);
            AddQuest(totals, Special7);

            bool bPartyDart = AnyCharHasItemType(party, EOBItemIndex.Dart, mapData.ItemList);
            bool bSQ8Darts = gameInfo.GlobalFlag(EOB1Bits.Game.SQ8Darts);
            Special8.AddObj(bPartyDart || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2402, Direction.Down, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2502, Direction.Down, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2602, Direction.Down, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2702, Direction.Down, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2802, Direction.Down, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2903, Direction.Left, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2904, Direction.Left, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2405, Direction.Up, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2505, Direction.Up, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2605, Direction.Up, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2705, Direction.Up, 32) || bSQ8Darts,
                IsWallType(mapData, EOB1.Spots.DartAlcove_L8_2805, Direction.Up, 32) || bSQ8Darts);
            AddQuest(totals, Special8);

            Special9.AddObj(gameInfo.GlobalFlag(EOB1Bits.Game.SQ9Orbs));
            AddQuest(totals, Special9);

            bool bPartyEgg = AnyCharHasItemType(party, EOBItemIndex.KenkuEgg, mapData.ItemList);
            bool bSQ10Eggs = gameInfo.GlobalFlag(EOB1Bits.Game.SQ10Eggs);
            Special10.AddObj(bPartyEgg || bSQ10Eggs,
                ItemTypeOnGround(mapData, EOBItemIndex.KenkuEgg, EOB1.Spots.KenkuAlcove_L10_2729) || bSQ10Eggs,
                ItemTypeOnGround(mapData, EOBItemIndex.KenkuEgg, EOB1.Spots.KenkuAlcove_L10_2727) || bSQ10Eggs,
                ItemTypeOnGround(mapData, EOBItemIndex.KenkuEgg, EOB1.Spots.KenkuAlcove_L10_2725) || bSQ10Eggs);
            AddQuest(totals, Special10);

            bool bSQ11Levers = gameInfo.GlobalFlag(EOB1Bits.Game.SQ11Levers);
            bool bSQ11Scroll = ItemTypeOnGround(mapData, EOBItemIndex.TextScroll, EOB1.Spots.DwarvenHealingPotion_52L11_2417) ||
                ItemTypeOnGround(mapData, EOBItemIndex.MageScroll, EOB1.Spots.DwarvenHealingPotion_52L11_2417) ||
                ItemTypeOnGround(mapData, EOBItemIndex.ClericScroll, EOB1.Spots.DwarvenHealingPotion_52L11_2417);
            Special11.AddObj(bSQ11Scroll || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2509, Direction.Left, 44) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2510, Direction.Left, 43) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2511, Direction.Left, 44) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2512, Direction.Left, 44) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2513, Direction.Left, 44) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2514, Direction.Left, 44) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2515, Direction.Left, 43) || bSQ11Levers,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2516, Direction.Left, 44) || bSQ11Levers);
            AddQuest(totals, Special11);

            bool bWandOfFrost = gameInfo.LevelFlag(10, EOB1Bits.Level.L11_WandOfFrost);
            WandOfFrost.AddObj(IsWallType(mapData, EOB1.Spots.LeverWall_L11_2509, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2510, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2511, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2512, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2513, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2514, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2515, Direction.Left, 43) || bWandOfFrost,
                IsWallType(mapData, EOB1.Spots.LeverWall_L11_2516, Direction.Left, 43) || bWandOfFrost);
            AddQuest(totals, WandOfFrost);

            bool bFlag3 = gameInfo.LevelFlag(0, EOB1Bits.Level.L1_MiscSewer);
            bool bFlag5 = gameInfo.LevelFlag(0, EOB1Bits.Level.L1_MiscButton);
            bool bFlag6 = gameInfo.LevelFlag(0, EOB1Bits.Level.L1_2521Button);
            bool bFlag7 = gameInfo.LevelFlag(0, EOB1Bits.Level.L1_MiscPlate);
            MiscXP.AddObj(bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3, bFlag3,
                bFlag5, bFlag5, bFlag5, bFlag5, bFlag6, bFlag6, bFlag7, bFlag7,
                gameInfo.LevelFlag(0, EOB1Bits.Level.L1_2220),
                gameInfo.LevelFlag(0, EOB1Bits.Level.L1_ExitDoor));
            AddQuest(totals, MiscXP);

            bool bTodUphill = !mapData.ItemInTable(EOB1ItemTableDefault.HalflingBones_64, EOBItemIndex.Bones);
            bool bAnya = !mapData.ItemInTable(EOB1ItemTableDefault.HumanBones_338, EOBItemIndex.Bones);
            bool bIleria = !mapData.ItemInTable(EOB1ItemTableDefault.HumanBones_339, EOBItemIndex.Bones);
            bool bBeohram = !mapData.ItemInTable(EOB1ItemTableDefault.HumanBones_340, EOBItemIndex.Bones);
            bool bTyrra = !mapData.ItemInTable(EOB1ItemTableDefault.HumanBones_341, EOBItemIndex.Bones);
            bool bKirath = !mapData.ItemInTable(EOB1ItemTableDefault.HumanBones_342, EOBItemIndex.Bones);
            Bones.AddObj(party.AnyCharHasItem(EOB1ItemTableDefault.HalflingBones_64, mapData.ItemList) || bTodUphill,
                bTodUphill,
                party.AnyCharHasItem(EOB1ItemTableDefault.HumanBones_338, mapData.ItemList) || bAnya,
                bAnya,
                party.AnyCharHasItem(EOB1ItemTableDefault.HumanBones_339, mapData.ItemList) || bIleria,
                bIleria,
                party.AnyCharHasItem(EOB1ItemTableDefault.HumanBones_340, mapData.ItemList) || bBeohram,
                bBeohram,
                party.AnyCharHasItem(EOB1ItemTableDefault.HumanBones_341, mapData.ItemList) || bTyrra,
                bTyrra,
                party.AnyCharHasItem(EOB1ItemTableDefault.HumanBones_342, mapData.ItemList) || bKirath,
                bKirath
                );
            AddQuest(totals, Bones);

            Loot.AddObj(!mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_73, EOB1.Spots.Arrow_73L1_1624),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfBless_70, EOB1.Spots.ClericScrollOfBless_70L1_1224),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Dart_67, EOB1.Spots.Dart_67L1_2518),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HalflingBones_64, EOB1.Spots.HalflingBones_64L1_1414),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LockPicks_65, EOB1.Spots.LockPicks_65L1_1414),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfArmor_72, EOB1.Spots.MageScrollOfArmor_72L1_1224),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_63, EOB1.Spots.Rations_63L1_2007),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_68, EOB1.Spots.Rations_68L1_1019),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_69, EOB1.Spots.Rations_69L1_1019),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_377, EOB1.Spots.Rock_377L1_1015),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_378, EOB1.Spots.Rock_378L1_1015),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_66, EOB1.Spots.Rock_66L1_2017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_71, EOB1.Spots.Rock_71L1_2121),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Shield_74, EOB1.Spots.Shield_74L1_1025),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_75, EOB1.Spots.Arrow_75L2_3028),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_93, EOB1.Spots.Arrow_93L2_3028),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Bow_85, EOB1.Spots.Bow_85L2_0620),
                !mapData.ItemInSpot(EOB1ItemTableDefault.GoldKey_82, EOB1.Spots.GoldKey_82L2_0315),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LeatherBoots_78, EOB1.Spots.LeatherBoots_78L2_1910),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfInvisibility_88, EOB1.Spots.MageScrollOfInvisibility_88L2_0624),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfShield_91, EOB1.Spots.MageScrollOfShield_91L2_1527),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Potion_446, EOB1.Spots.Potion_446L2_2121),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_421, EOB1.Spots.PotionOfExtraHealing_421L2_0730),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfGiantStrength_81, EOB1.Spots.PotionOfGiantStrength_81L2_3017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_79, EOB1.Spots.PotionOfHealing_79L2_0611),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_95, EOB1.Spots.PotionOfHealing_95L2_0230),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_416, EOB1.Spots.Rations_416L2_3017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_417, EOB1.Spots.Rations_417L2_2318),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_418, EOB1.Spots.Rations_418L2_2829),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_420, EOB1.Spots.Rations_420L2_0730),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_76, EOB1.Spots.Rations_76L2_1306),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_77, EOB1.Spots.Rations_77L2_1910),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_89, EOB1.Spots.Rations_89L2_2524),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_90, EOB1.Spots.Rations_90L2_2826),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_83, EOB1.Spots.Rock_83L2_0916),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_96, EOB1.Spots.Rock_96L2_2130),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SilverKey_80, EOB1.Spots.SilverKey_80L2_3017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SilverKey_84, EOB1.Spots.SilverKey_84L2_2318),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SilverKey_87, EOB1.Spots.SilverKey_87L2_1923),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SilverKey_94, EOB1.Spots.SilverKey_94L2_2829),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Sling_92, EOB1.Spots.Sling_92L2_1928),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneDagger_86, EOB1.Spots.StoneDagger_86L2_1321),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_102, EOB1.Spots.Arrow_102L3_1007),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_108, EOB1.Spots.Arrow_108L3_1513),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_123, EOB1.Spots.Arrow_123L3_1724),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_373, EOB1.Spots.Arrow_373L3_1724),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_374, EOB1.Spots.Arrow_374L3_1724),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_375, EOB1.Spots.Arrow_375L3_1724),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_99, EOB1.Spots.Arrow_99L3_2106),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Backstabber_112, EOB1.Spots.Backstabber_112L3_1015),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Chainmail_100, EOB1.Spots.Chainmail_100L3_0907),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCauseLightWounds_121, EOB1.Spots.ClericScrollOfCauseLightWounds_121L3_1202),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfFlameBlade_118, EOB1.Spots.ClericScrollOfFlameBlade_118L3_1122),
                !bGem106,
                !bGem107,
                !mapData.ItemInSpot(EOB1ItemTableDefault.Gem_122, EOB1.Spots.Gem_122L3_1015),
                !bGem97,
                !bGem98,
                !mapData.ItemInSpot(EOB1ItemTableDefault.HumanBones_338, EOB1.Spots.HumanBones_338L3_0130),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_103, EOB1.Spots.IronRations_103L3_0609),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_104, EOB1.Spots.IronRations_104L3_0609),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_419, EOB1.Spots.IronRations_419L3_0609),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LeatherArmor_345, EOB1.Spots.LeatherArmor_345L3_0130),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LongSword_125, EOB1.Spots.LongSword_125L3_0130),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfDetectMagic_111, EOB1.Spots.MageScrollOfDetectMagic_111L3_1214),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfFireball_120, EOB1.Spots.MageScrollOfFireball_120L3_1522),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_110, EOB1.Spots.PotionOfExtraHealing_110L3_2813),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_109, EOB1.Spots.PotionOfHealing_109L3_2813),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_116, EOB1.Spots.PotionOfHealing_116L3_0921),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfSpeed_372, EOB1.Spots.PotionOfSpeed_372L3_1724),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_113, EOB1.Spots.Rations_113L3_0316),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rations_115, EOB1.Spots.Rations_115L3_1020),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_117, EOB1.Spots.Rock_117L3_0522),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_119, EOB1.Spots.Rock_119L3_1322),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_124, EOB1.Spots.Rock_124L3_2026),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Shield_101, EOB1.Spots.Shield_101L3_0907),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Shield_114, EOB1.Spots.Shield_114L3_1319),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SilverKey_105, EOB1.Spots.SilverKey_105L3_2809),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Spear_346, EOB1.Spots.Spear_346L3_0130),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_126, EOB1.Spots.Wand_126L3_3030),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_127, EOB1.Spots.Arrow_127L4_2604),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_131, EOB1.Spots.Arrow_131L4_2910),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_152, EOB1.Spots.Arrow_152L4_2025),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfSlowPoison_145, EOB1.Spots.ClericScrollOfSlowPoison_145L4_0319),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowCleaver_140, EOB1.Spots.DrowCleaver_140L4_1118),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenHelmet_149, EOB1.Spots.DwarvenHelmet_149L4_1020),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_130, EOB1.Spots.DwarvenKey_130L4_0307),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_153, EOB1.Spots.DwarvenKey_153L4_2828),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_303, EOB1.Spots.DwarvenKey_303L4_1508),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_376, EOB1.Spots.DwarvenKey_376L4_0915),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenShield_150, EOB1.Spots.DwarvenShield_150L4_1020),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_146, EOB1.Spots.IronRations_146L4_2019),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_147, EOB1.Spots.IronRations_147L4_2019),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_148, EOB1.Spots.IronRations_148L4_2019),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Mace_128, EOB1.Spots.Mace_128L4_2206),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Mace_135, EOB1.Spots.Mace_135L4_2113),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfFlameArrow_144, EOB1.Spots.MageScrollOfFlameArrow_144L4_0319),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Medallion_138, EOB1.Spots.Medallion_138L4_1017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_136, EOB1.Spots.PotionOfCurePoison_136L4_2415),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_137, EOB1.Spots.PotionOfCurePoison_137L4_2415),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_430, EOB1.Spots.PotionOfCurePoison_430L4_2103),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_431, EOB1.Spots.PotionOfCurePoison_431L4_2103),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_432, EOB1.Spots.PotionOfCurePoison_432L4_2104),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfCurePoison_433, EOB1.Spots.PotionOfCurePoison_433L4_2104),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_134, EOB1.Spots.PotionOfHealing_134L4_0314),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_143, EOB1.Spots.PotionOfHealing_143L4_0319),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_129, EOB1.Spots.Ring_129L4_2406),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_132, EOB1.Spots.Ring_132L4_2911),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Robe_139, EOB1.Spots.Robe_139L4_1017),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_133, EOB1.Spots.Rock_133L4_3012),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_151, EOB1.Spots.Rock_151L4_1924),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_154, EOB1.Spots.Rock_154L4_3028),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneScepter_141, EOB1.Spots.StoneScepter_141L4_2702),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Axe_174, EOB1.Spots.Axe_174L5_2025),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Boots_181, EOB1.Spots.Boots_181L5_1930),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfAid_159, EOB1.Spots.ClericScrollOfAid_159L5_0506),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfDetectMagic_162, EOB1.Spots.ClericScrollOfDetectMagic_162L5_2408),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfHoldPerson_155, EOB1.Spots.ClericScrollOfHoldPerson_155L5_0101),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfPrayer_180, EOB1.Spots.ClericScrollOfPrayer_180L5_0629),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_172, EOB1.Spots.DwarvenKey_172L5_0425),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_156, EOB1.Spots.IronRations_156L5_2102),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_161, EOB1.Spots.IronRations_161L5_0807),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_163, EOB1.Spots.IronRations_163L5_1210),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_165, EOB1.Spots.IronRations_165L5_0712),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_168, EOB1.Spots.IronRations_168L5_0614),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_176, EOB1.Spots.Key_176L5_2826),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_179, EOB1.Spots.Key_179L5_1428),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LongSword_164, EOB1.Spots.LongSword_164L5_2510),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfDispelMagic_169, EOB1.Spots.MageScrollOfDispelMagic_169L5_0515),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfHaste_160, EOB1.Spots.MageScrollOfHaste_160L5_0606),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfInvisibility10_178, EOB1.Spots.MageScrollOfInvisibility10_178L5_2527),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PlateMail_171, EOB1.Spots.PlateMail_171L5_2527),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfPoison_167, EOB1.Spots.PotionOfPoison_167L5_1013),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_177, EOB1.Spots.Ring_177L5_2227),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_170, EOB1.Spots.Rock_170L5_2516),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_302, EOB1.Spots.Rock_302L5_2111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ScaleMail_173, EOB1.Spots.ScaleMail_173L5_0425),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Sling_175, EOB1.Spots.Sling_175L5_1926),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Spear_157, EOB1.Spots.Spear_157L5_2102),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneNecklace_158, EOB1.Spots.StoneNecklace_158L5_1605),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_142, EOB1.Spots.Wand_142L5_2716),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_380, EOB1.Spots.AdamantiteDart_380L6_0328),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Bracers_209, EOB1.Spots.Bracers_209L6_2628),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ChieftainHalberd_215, EOB1.Spots.ChieftainHalberd_215L6_1418),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureSeriousWounds_198, EOB1.Spots.ClericScrollOfCureSeriousWounds_198L6_1420),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfDispelMagic_197, EOB1.Spots.ClericScrollOfDispelMagic_197L6_1420),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfFlameBlade_213, EOB1.Spots.ClericScrollOfFlameBlade_213L6_2230),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Dagger_381, EOB1.Spots.Dagger_381L6_0328),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_192, EOB1.Spots.DwarvenKey_192L6_0106),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_193, EOB1.Spots.DwarvenKey_193L6_0706),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenKey_211, EOB1.Spots.DwarvenKey_211L6_0829),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenShield_206, EOB1.Spots.DwarvenShield_206L6_0925),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_182, EOB1.Spots.KenkuEgg_182L6_1105),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_183, EOB1.Spots.KenkuEgg_183L6_1504),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_184, EOB1.Spots.KenkuEgg_184L6_1505),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_185, EOB1.Spots.KenkuEgg_185L6_1718),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_186, EOB1.Spots.KenkuEgg_186L6_1505),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_187, EOB1.Spots.KenkuEgg_187L6_2105),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_188, EOB1.Spots.KenkuEgg_188L6_2908),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_189, EOB1.Spots.KenkuEgg_189L6_1605),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_190, EOB1.Spots.KenkuEgg_190L6_1705),
                !mapData.ItemInSpot(EOB1ItemTableDefault.KenkuEgg_191, EOB1.Spots.KenkuEgg_191L6_1905),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_199, EOB1.Spots.Key_199L6_1922),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MacePlus3_208, EOB1.Spots.MacePlus3_208L6_1228),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfHoldPerson_194, EOB1.Spots.MageScrollOfHoldPerson_194L6_2715),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_200, EOB1.Spots.NULL_200L6_0222),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_201, EOB1.Spots.NULL_201L6_0222),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_202, EOB1.Spots.NULL_202L6_0823),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_203, EOB1.Spots.NULL_203L6_0823),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_204, EOB1.Spots.NULL_204L6_0125),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_205, EOB1.Spots.NULL_205L6_0125),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_422, EOB1.Spots.NULL_422L6_0122),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_423, EOB1.Spots.NULL_423L6_0122),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_424, EOB1.Spots.NULL_424L6_0923),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_425, EOB1.Spots.NULL_425L6_0923),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_426, EOB1.Spots.NULL_426L6_0025),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_427, EOB1.Spots.NULL_427L6_0025),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_428, EOB1.Spots.NULL_428L6_0824),
                //!mapData.ItemInSpot(EOB1ItemTableDefault.NULL_429, EOB1.Spots.NULL_429L6_0824),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_379, EOB1.Spots.PotionOfExtraHealing_379L6_2715),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_212, EOB1.Spots.Ring_212L6_1529),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_196, EOB1.Spots.Rock_196L6_1313),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_207, EOB1.Spots.Rock_207L6_1725),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneRing_195, EOB1.Spots.StoneRing_195L6_1711),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_210, EOB1.Spots.Wand_210L6_0629),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_219, EOB1.Spots.Arrow_219L7_2208),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_227, EOB1.Spots.Arrow_227L7_1216),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_228, EOB1.Spots.Arrow_228L7_1216),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_229, EOB1.Spots.Arrow_229L7_1216),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_230, EOB1.Spots.Arrow_230L7_0722),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_237, EOB1.Spots.Arrow_237L7_2723),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_238, EOB1.Spots.Arrow_238L7_2723),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_239, EOB1.Spots.Arrow_239L7_2723),
                !mapData.ItemInSpot(EOB1ItemTableDefault.BandedArmor_236, EOB1.Spots.BandedArmor_236L7_2523),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Bracers_232, EOB1.Spots.Bracers_232L7_2622),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfBless_218, EOB1.Spots.ClericScrollOfBless_218L7_1705),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCreateFood_223, EOB1.Spots.ClericScrollOfCreateFood_223L7_0612),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureLightWounds_245, EOB1.Spots.ClericScrollOfCureLightWounds_245L7_1927),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfProtectEvil10_220, EOB1.Spots.ClericScrollOfProtectEvil10_220L7_3011),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRemoveParalysis_221, EOB1.Spots.ClericScrollOfRemoveParalysis_221L7_3011),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfSlowPoison_222, EOB1.Spots.ClericScrollOfSlowPoison_222L7_0312),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_240, EOB1.Spots.DrowKey_240L7_1425),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_244, EOB1.Spots.DrowKey_244L7_1727),
                !mapData.ItemInSpot(EOB1ItemTableDefault.RubyKey_247, EOB1.Spots.RubyKey_247L7_2727),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HolySymbol_434, EOB1.Spots.HolySymbol_434L7_2904),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HumanBones_339, EOB1.Spots.HumanBones_339L7_2904),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_216, EOB1.Spots.IronRations_216L7_1705),
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_235, EOB1.Spots.JeweledKey_235L7_1923),
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_246, EOB1.Spots.JeweledKey_246L7_2527),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_224, EOB1.Spots.Key_224L7_3012),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Key_242, EOB1.Spots.Key_242L7_0426),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfFear_234, EOB1.Spots.MageScrollOfFear_234L7_1723),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfFireball_214, EOB1.Spots.MageScrollOfFireball_214L7_0203),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfLightningBolt_241, EOB1.Spots.MageScrollOfLightningBolt_241L7_3025),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Medallion_225, EOB1.Spots.Medallion_225L7_0314),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Necklace_217, EOB1.Spots.Necklace_217L7_1705),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfHealing_243, EOB1.Spots.PotionOfHealing_243L7_0426),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_226, EOB1.Spots.Ring_226L7_1415),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_233, EOB1.Spots.Ring_233L7_2822),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_248, EOB1.Spots.Rock_248L7_1231),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Shield_250, EOB1.Spots.Shield_250L7_3014),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SlicerPlus3_231, EOB1.Spots.SlicerPlus3_231L7_2422),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_249, EOB1.Spots.Wand_249L7_1331),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureCriticalWounds_253, EOB1.Spots.ClericScrollOfCureCriticalWounds_253L8_2803),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfHoldPerson_258, EOB1.Spots.ClericScrollOfHoldPerson_258L8_1209),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfNeutralPoison_252, EOB1.Spots.ClericScrollOfNeutralPoison_252L8_2803),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfPrayer_251, EOB1.Spots.ClericScrollOfPrayer_251L8_2803),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfProtectEvil_264, EOB1.Spots.ClericScrollOfProtectEvil_264L8_0515),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRaiseDead_267, EOB1.Spots.ClericScrollOfRaiseDead_267L8_2115),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowBoots_265, EOB1.Spots.DrowBoots_265L8_0715),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowBow_262, EOB1.Spots.DrowBow_262L8_1514),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_263, EOB1.Spots.DrowKey_263L8_2014),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_269, EOB1.Spots.DrowKey_269L8_2020),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_275, EOB1.Spots.DrowKey_275L8_2823),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Flail_274, EOB1.Spots.Flail_274L8_0823),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Gem_385, EOB1.Spots.Gem_385L8_2020),
                !mapData.ItemInSpot(EOB1ItemTableDefault.JeweledKey_270, EOB1.Spots.JeweledKey_270L8_2121),
                !mapData.ItemInSpot(EOB1ItemTableDefault.LockPicks_279, EOB1.Spots.LockPicks_279L8_1229),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfIceStorm_278, EOB1.Spots.MageScrollOfIceStorm_278L8_1428),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfInvisibility10_261, EOB1.Spots.MageScrollOfInvisibility10_261L8_0914),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfShield_271, EOB1.Spots.MageScrollOfShield_271L8_2121),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfVampiricTouch_447, EOB1.Spots.MageScrollOfVampiricTouch_447L8_1514),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Medallion_254, EOB1.Spots.Medallion_254L8_0504),
                !mapData.ItemInSpot(EOB1ItemTableDefault.NightStalker_257, EOB1.Spots.NightStalker_257L8_1808),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PlateMailOfGreatBeauty_273, EOB1.Spots.PlateMailOfGreatBeauty_273L8_0423),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_266, EOB1.Spots.PotionOfExtraHealing_266L8_2115),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_255, EOB1.Spots.Ring_255L8_0504),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_256, EOB1.Spots.Ring_256L8_0405),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Robe_276, EOB1.Spots.Robe_276L8_0128),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_259, EOB1.Spots.Rock_259L8_2812),
                !mapData.ItemInSpot(EOB1ItemTableDefault.RubyKey_260, EOB1.Spots.RubyKey_260L8_1513),
                !mapData.ItemInSpot(EOB1ItemTableDefault.RubyKey_268, EOB1.Spots.RubyKey_268L8_2516),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ScepterOfKinglyMight_277, EOB1.Spots.ScepterOfKinglyMight_277L8_0128),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_272, EOB1.Spots.Wand_272L8_2922),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_436, EOB1.Spots.AdamantiteDart_436L9_0709),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_437, EOB1.Spots.AdamantiteDart_437L9_0809),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_438, EOB1.Spots.AdamantiteDart_438L9_0909),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_439, EOB1.Spots.AdamantiteDart_439L9_1009),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_440, EOB1.Spots.AdamantiteDart_440L9_1109),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_441, EOB1.Spots.AdamantiteDart_441L9_0713),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_442, EOB1.Spots.AdamantiteDart_442L9_0813),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_443, EOB1.Spots.AdamantiteDart_443L9_0913),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_444, EOB1.Spots.AdamantiteDart_444L9_1013),
                !mapData.ItemInSpot(EOB1ItemTableDefault.AdamantiteDart_445, EOB1.Spots.AdamantiteDart_445L9_1113),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_284, EOB1.Spots.Arrow_284L9_2514),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_285, EOB1.Spots.Arrow_285L9_2514),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_286, EOB1.Spots.Arrow_286L9_2514),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Chainmail_301, EOB1.Spots.Chainmail_301L9_3029),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureSeriousWounds_289, EOB1.Spots.ClericScrollOfCureSeriousWounds_289L9_0216),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfDetectMagic_281, EOB1.Spots.ClericScrollOfDetectMagic_281L9_1203),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfDispelMagic_288, EOB1.Spots.ClericScrollOfDispelMagic_288L9_0216),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfFlameBlade_291, EOB1.Spots.ClericScrollOfFlameBlade_291L9_1319),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfProtectEvil10_292, EOB1.Spots.ClericScrollOfProtectEvil10_292L9_0120),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRaiseDead_295, EOB1.Spots.ClericScrollOfRaiseDead_295L9_1221),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRaiseDead_300, EOB1.Spots.ClericScrollOfRaiseDead_300L9_0828),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowBoots_296, EOB1.Spots.DrowBoots_296L9_2821),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_280, EOB1.Spots.DrowKey_280L9_3002),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_287, EOB1.Spots.DrowKey_287L9_0915),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowShield_294, EOB1.Spots.DrowShield_294L9_2420),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Helmet_350, EOB1.Spots.Helmet_350L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HumanBones_340, EOB1.Spots.HumanBones_340L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfArmor_293, EOB1.Spots.MageScrollOfArmor_293L9_1420),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfInvisibility_290, EOB1.Spots.MageScrollOfInvisibility_290L9_2918),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfStoneskin_283, EOB1.Spots.MageScrollOfStoneskin_283L9_3008),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_382, EOB1.Spots.OrbOfPower_382L9_2418),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_383, EOB1.Spots.OrbOfPower_383L9_2418),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_384, EOB1.Spots.OrbOfPower_384L9_2418),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PaladinHolySymbol_351, EOB1.Spots.PaladinHolySymbol_351L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PlateMail_347, EOB1.Spots.PlateMail_347L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_297, EOB1.Spots.PotionOfExtraHealing_297L9_0322),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfPoison_282, EOB1.Spots.PotionOfPoison_282L9_1506),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Severious_349, EOB1.Spots.Severious_349L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Shield_348, EOB1.Spots.Shield_348L9_1111),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Spear_298, EOB1.Spots.Spear_298L9_1423),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_299, EOB1.Spots.Wand_299L9_2026),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_311, EOB1.Spots.Arrow_311L10_0411),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Arrow_312, EOB1.Spots.Arrow_312L10_1524),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureCriticalWounds_308, EOB1.Spots.ClericScrollOfCureCriticalWounds_308L10_2729),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfFlameBlade_307, EOB1.Spots.ClericScrollOfFlameBlade_307L10_2729),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfFlameBlade_316, EOB1.Spots.ClericScrollOfFlameBlade_316L10_1828),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfNeutralPoison_318, EOB1.Spots.ClericScrollOfNeutralPoison_318L10_1913),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRemoveParalysis_317, EOB1.Spots.ClericScrollOfRemoveParalysis_317L10_1828),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HumanBones_341, EOB1.Spots.HumanBones_341L10_0312),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfConeOfCold_319, EOB1.Spots.MageScrollOfConeOfCold_319L10_2522),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PlateMail_304, EOB1.Spots.PlateMail_304L10_0228),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfGiantStrength_315, EOB1.Spots.PotionOfGiantStrength_315L10_1211),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfPoison_305, EOB1.Spots.PotionOfPoison_305L10_2725),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_314, EOB1.Spots.Ring_314L10_1611),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SkullKey_313, EOB1.Spots.SkullKey_313L10_0312),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_306, EOB1.Spots.Wand_306L10_2727),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_309, EOB1.Spots.Wand_309L10_1427),
                !mapData.ItemInSpot(EOB1ItemTableDefault.BandedArmor_330, EOB1.Spots.BandedArmor_330L11_0101),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Bracers_344, EOB1.Spots.Bracers_344L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfCureSeriousWounds_333, EOB1.Spots.ClericScrollOfCureSeriousWounds_333L11_2316),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRaiseDead_322, EOB1.Spots.ClericScrollOfRaiseDead_322L11_0506),
                !mapData.ItemInSpot(EOB1ItemTableDefault.ClericScrollOfRaiseDead_326, EOB1.Spots.ClericScrollOfRaiseDead_326L11_2501),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_324, EOB1.Spots.DrowKey_324L11_2615),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DrowKey_337, EOB1.Spots.DrowKey_337L11_1624),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Flicka_336, EOB1.Spots.Flicka_336L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.HumanBones_342, EOB1.Spots.HumanBones_342L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.MageScrollOfHoldMonster_332, EOB1.Spots.MageScrollOfHoldMonster_332L11_0117),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Medallion_321, EOB1.Spots.Medallion_321L11_0506),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_325, EOB1.Spots.OrbOfPower_325L11_2830),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_331, EOB1.Spots.Ring_331L11_1204),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_343, EOB1.Spots.Ring_343L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Robe_335, EOB1.Spots.Robe_335L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_327, EOB1.Spots.Rock_327L11_2501),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Rock_328, EOB1.Spots.Rock_328L11_0911),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SlasherPlus4_329, EOB1.Spots.SlasherPlus4_329L11_0101),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SpellBook_435, EOB1.Spots.SpellBook_435L11_1627),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneHolySymbol_310, EOB1.Spots.StoneHolySymbol_310L11_1727),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneOrb_323, EOB1.Spots.StoneOrb_323L11_2615),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_320, EOB1.Spots.Wand_320L11_0308),
                !mapData.ItemInSpot(EOB1ItemTableDefault.DwarvenHealingPotion_52, EOB1.Spots.DwarvenHealingPotion_52L11_2417),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_396, EOB1.Spots.IronRations_396L12_2802),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_397, EOB1.Spots.IronRations_397L12_2902),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_398, EOB1.Spots.IronRations_398L12_2803),
                !mapData.ItemInSpot(EOB1ItemTableDefault.IronRations_399, EOB1.Spots.IronRations_399L12_2903),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Necklace_389, EOB1.Spots.Necklace_389L12_0717),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_391, EOB1.Spots.OrbOfPower_391L12_2713),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_393, EOB1.Spots.OrbOfPower_393L12_1302),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_394, EOB1.Spots.OrbOfPower_394L12_1302),
                !mapData.ItemInSpot(EOB1ItemTableDefault.OrbOfPower_395, EOB1.Spots.OrbOfPower_395L12_1302),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_386, EOB1.Spots.PotionOfExtraHealing_386L12_0715),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfExtraHealing_387, EOB1.Spots.PotionOfExtraHealing_387L12_0717),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfInvisibility_401, EOB1.Spots.PotionOfInvisibility_401L12_0923),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfInvisibility_402, EOB1.Spots.PotionOfInvisibility_402L12_1123),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfInvisibility_405, EOB1.Spots.PotionOfInvisibility_405L12_1703),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfInvisibility_406, EOB1.Spots.PotionOfInvisibility_406L12_1703),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfSpeed_392, EOB1.Spots.PotionOfSpeed_392L12_2709),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfVitality_403, EOB1.Spots.PotionOfVitality_403L12_0923),
                !mapData.ItemInSpot(EOB1ItemTableDefault.PotionOfVitality_404, EOB1.Spots.PotionOfVitality_404L12_1123),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Ring_388, EOB1.Spots.Ring_388L12_0715),
                !mapData.ItemInSpot(EOB1ItemTableDefault.SkullKey_400, EOB1.Spots.SkullKey_400L12_0716),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneDagger_410, EOB1.Spots.StoneDagger_410L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneHolySymbol_414, EOB1.Spots.StoneHolySymbol_414L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneMedallion_411, EOB1.Spots.StoneMedallion_411L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneNecklace_412, EOB1.Spots.StoneNecklace_412L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneOrb_415, EOB1.Spots.StoneOrb_415L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneRing_413, EOB1.Spots.StoneRing_413L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.StoneScepter_409, EOB1.Spots.StoneScepter_409L12_2818),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_390, EOB1.Spots.Wand_390L12_1913),
                !mapData.ItemInSpot(EOB1ItemTableDefault.Wand_407, EOB1.Spots.Wand_407L12_2302)
                );
            AddQuest(totals, Loot);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool DoorOpen(EOB1MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            DoorStatus door = data.GetWall(spot.X, spot.Y, dir).DoorType;
            return door.HasFlag(DoorStatus.PartlyOpen) || door.HasFlag(DoorStatus.Open);
        }

        private bool IsForceable(EOB1MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).DoorType.HasFlag(DoorStatus.Forceable);
        }

        private bool HasFloorHole(EOB1MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).IsFloorHole;
        }

        private bool IsOpen(EOB1MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).IsOpen;
        }

        private bool IsWallType(EOB1MapData data, MapXY spot, Direction dir, int iWallType)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWallIndex(spot.X, spot.Y, dir) == iWallType;
        }

        private bool ItemOnGround(EOB1MapData data, MapXY spot)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItem(spot.X, spot.Y);
        }

        private bool ItemTypeOnGround(EOB1MapData data, EOBItemIndex itemType, MapXY spot)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItemType(itemType, spot.X, spot.Y);
        }

        private bool ItemTypesOnGround(EOB1MapData data, MapXY spot, params EOBItemIndex[] itemTypes)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItemTypes(spot.X, spot.Y, itemTypes);
        }

        private bool AnyCharHasItemType(EOBPartyInfo party, EOBItemIndex index, byte[] itemList, int iModifier = -0x10000)
        {
            return party.AnyCharHasItemType((int)index, itemList, iModifier);
        }
    }
}
