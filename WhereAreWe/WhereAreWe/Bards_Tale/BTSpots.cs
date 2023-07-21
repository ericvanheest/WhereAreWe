using System.Drawing;
namespace WhereAreWe
{
    // These lists of quest-related locations keeps the GetQuests() functions from becoming 
    // even more ungainly than they are already.

    public class BT1Locations
    {
        // Bard's Tale 1
        public MapXY None = new MapXY(GameNames.BardsTale1, -1, 0, 0, -1);

        // Skara Brae
        public MapXY KylearanGateWest = new MapXY(BT1Map.SkaraBrae, 4, 26);
        public MapXY KylearanTower = new MapXY(BT1Map.SkaraBrae, 27, 27);
        public MapXY KylearanGateSouth = new MapXY(BT1Map.SkaraBrae, 27, 25);
        public MapXY GuardianStoneGiant1 = new MapXY(BT1Map.SkaraBrae, 4, 26);
        public MapXY GuardianStoneGiant2 = new MapXY(BT1Map.SkaraBrae, 22, 3);
        public MapXY GuardianStoneGiant3 = new MapXY(BT1Map.SkaraBrae, 21, 2);
        public MapXY GuardianGolem1 = new MapXY(BT1Map.SkaraBrae, 6, 26);
        public MapXY GuardianGreyDragon = new MapXY(BT1Map.SkaraBrae, 6, 24);
        public MapXY GuardianGolem2 = new MapXY(BT1Map.SkaraBrae, 6, 22);
        public MapXY GuardianOgreLord1 = new MapXY(BT1Map.SkaraBrae, 3, 14);
        public MapXY GuardianOgreLord2 = new MapXY(BT1Map.SkaraBrae, 3, 6);
        public MapXY GuardianOgreLord3 = new MapXY(BT1Map.SkaraBrae, 6, 6);
        public MapXY HarkynsCastle = new MapXY(BT1Map.SkaraBrae, 4, 24);
        public MapXY Garths = new MapXY(BT1Map.SkaraBrae, 26, 18);
        public MapXY Roscoes = new MapXY(BT1Map.SkaraBrae, 12, 21);
        public MapXY ReviewBoard = new MapXY(BT1Map.SkaraBrae, 23, 20);
        public MapXY SinisterInn = new MapXY(BT1Map.SkaraBrae, 23, 19);
        public MapXY DrawnbladeInn = new MapXY(BT1Map.SkaraBrae, 11, 18);
        public MapXY SkullTavern = new MapXY(BT1Map.SkaraBrae, 1, 8);
        public MapXY GreatGods1 = new MapXY(BT1Map.SkaraBrae, 14, 18);
        public MapXY GreatGods2 = new MapXY(BT1Map.SkaraBrae, 15, 18);
        public MapXY GreatGods3 = new MapXY(BT1Map.SkaraBrae, 16, 18);
        public MapXY GreatGods4 = new MapXY(BT1Map.SkaraBrae, 17, 18);
        public MapXY GreaterGods1 = new MapXY(BT1Map.SkaraBrae, 13, 12);
        public MapXY GreaterGods2 = new MapXY(BT1Map.SkaraBrae, 14, 12);
        public MapXY GreaterGods3 = new MapXY(BT1Map.SkaraBrae, 15, 12);
        public MapXY GreaterGods4 = new MapXY(BT1Map.SkaraBrae, 16, 12);
        public MapXY GreatestGods1 = new MapXY(BT1Map.SkaraBrae, 12, 17);
        public MapXY GreatestGods2 = new MapXY(BT1Map.SkaraBrae, 12, 16);
        public MapXY GreatestGods3 = new MapXY(BT1Map.SkaraBrae, 12, 15);
        public MapXY GreatestGods4 = new MapXY(BT1Map.SkaraBrae, 12, 14);
        public MapXY ThiefTemple = new MapXY(BT1Map.SkaraBrae, 26, 9);
        public MapXY MadGod1 = new MapXY(BT1Map.SkaraBrae, 18, 16);
        public MapXY MadGod2 = new MapXY(BT1Map.SkaraBrae, 18, 15);
        public MapXY MadGod3 = new MapXY(BT1Map.SkaraBrae, 18, 14);
        public MapXY MadGod4 = new MapXY(BT1Map.SkaraBrae, 18, 13);
        public MapXY CityGates = new MapXY(BT1Map.SkaraBrae, 0, 15);
        public MapXY MangarGateNorth = new MapXY(BT1Map.SkaraBrae, 2, 4);
        public MapXY MangarGateEast = new MapXY(BT1Map.SkaraBrae, 4, 2);
        public MapXY MangarsTower = new MapXY(BT1Map.SkaraBrae, 2, 2);
        public MapXY ScarletBard = new MapXY(BT1Map.SkaraBrae, 28, 5);
        public MapXY SewerEntrance = new MapXY(BT1Map.SkaraBrae, 1, 1);
        public MapXY TempleOfKiosk = new MapXY(BT1Map.SkaraBrae, 21, 3);
        public MapXY AdventurersGuild = new MapXY(BT1Map.SkaraBrae, 24, 15);
        public MapXY GameCredits = new MapXY(BT1Map.SkaraBrae, 26, 15);

        // Specific monster encounters
        public MapXY FESBStoneGiant1 = new MapXY(BT1Map.SkaraBrae, 4, 26);
        public MapXY FESBGreyDragon = new MapXY(BT1Map.SkaraBrae, 6, 24);
        public MapXY FESBStoneGolem1 = new MapXY(BT1Map.SkaraBrae, 6, 22);
        public MapXY FESBStoneGolem2 = new MapXY(BT1Map.SkaraBrae, 6, 26);
        public MapXY FESBOgreLord1 = new MapXY(BT1Map.SkaraBrae, 3, 14);
        public MapXY FESBOgreLord2 = new MapXY(BT1Map.SkaraBrae, 3, 6);
        public MapXY FESBOgreLord3 = new MapXY(BT1Map.SkaraBrae, 6, 6);
        public MapXY FESBStoneGiant2 = new MapXY(BT1Map.SkaraBrae, 22, 3);
        public MapXY FESBStoneGiant3 = new MapXY(BT1Map.SkaraBrae, 21, 2);
        public MapXY FESBSamurai = new MapXY(BT1Map.SkaraBrae, 27, 6);

        public MapXY FESL1Spider6 = new MapXY(BT1Map.Sewers1, 6, 21);
        public MapXY FESL1Spider5 = new MapXY(BT1Map.Sewers1, 17, 19);
        public MapXY FESL1Spider7 = new MapXY(BT1Map.Sewers1, 8, 16);
        public MapXY FESL1BlackWidow3a = new MapXY(BT1Map.Sewers1, 0, 13);
        public MapXY FESL1Spinner = new MapXY(BT1Map.Sewers1, 4, 9);
        public MapXY FESL1BlackWidow3b = new MapXY(BT1Map.Sewers1, 17, 4);
        public MapXY FESL1BlackWidow4 = new MapXY(BT1Map.Sewers1, 13, 1);
        public MapXY FESL1BlackWidow5 = new MapXY(BT1Map.Sewers1, 21, 10);
        public MapXY FESL2Spider13 = new MapXY(BT1Map.Sewers2, 21, 16);
        public MapXY FESL2BlackWidow7a = new MapXY(BT1Map.Sewers2, 10, 14);
        public MapXY FESL2BlackWidow6a = new MapXY(BT1Map.Sewers2, 6, 13);
        public MapXY FESL2Spider16 = new MapXY(BT1Map.Sewers2, 6, 10);
        public MapXY FESL2BlackWidow6b = new MapXY(BT1Map.Sewers2, 3, 3);
        public MapXY FESL2Spider10 = new MapXY(BT1Map.Sewers2, 15, 21);
        public MapXY FESL2BlackWidow7b = new MapXY(BT1Map.Sewers2, 20, 13);
        public MapXY FESL2Spider9 = new MapXY(BT1Map.Sewers2, 20, 0);
        public MapXY FESL3Spinner3a = new MapXY(BT1Map.Sewers3, 12, 17);
        public MapXY FESL3BlackWidow7 = new MapXY(BT1Map.Sewers3, 1, 15);
        public MapXY FESL3Spinner4a = new MapXY(BT1Map.Sewers3, 11, 9);
        public MapXY FESL3BlackWidow5 = new MapXY(BT1Map.Sewers3, 9, 8);
        public MapXY FESL3Spinner2 = new MapXY(BT1Map.Sewers3, 2, 1);
        public MapXY FESL3Spinner4b = new MapXY(BT1Map.Sewers3, 7, 1);
        public MapXY FESL3BlackWidow8 = new MapXY(BT1Map.Sewers3, 8, 20);
        public MapXY FESL3Spinner3b = new MapXY(BT1Map.Sewers3, 1, 6);
        public MapXY FECL1Wight11 = new MapXY(BT1Map.Catacombs1, 6, 16);
        public MapXY FECL1Skeleton66 = new MapXY(BT1Map.Catacombs1, 3, 12);
        public MapXY FECL1Zombie45 = new MapXY(BT1Map.Catacombs1, 21, 10);
        public MapXY FECL1Zombie28 = new MapXY(BT1Map.Catacombs1, 18, 9);
        public MapXY FECL1Skeleton39 = new MapXY(BT1Map.Catacombs1, 14, 6);
        public MapXY FECL1Skeleton52 = new MapXY(BT1Map.Catacombs1, 19, 2);
        public MapXY FECL1Wight9 = new MapXY(BT1Map.Catacombs1, 6, 0);
        public MapXY FECL1Ghoul5 = new MapXY(BT1Map.Catacombs1, 9, 0);
        public MapXY FECL2GreyDragon = new MapXY(BT1Map.Catacombs2, 6, 13);
        public MapXY FECL2MasterSorcerer = new MapXY(BT1Map.Catacombs2, 10, 12);
        public MapXY FECL2Wight49 = new MapXY(BT1Map.Catacombs2, 6, 11);
        public MapXY FECL2SoulSucker = new MapXY(BT1Map.Catacombs2, 21, 0);
        public MapXY FECL3Wraith8 = new MapXY(BT1Map.Catacombs3, 0, 21);
        public MapXY FECL3Ogre = new MapXY(BT1Map.Catacombs3, 16, 20);
        public MapXY FECL3Spectre = new MapXY(BT1Map.Catacombs3, 20, 19);
        public MapXY FECL3Zombie53 = new MapXY(BT1Map.Catacombs3, 14, 18);
        public MapXY FECL3Zombie66 = new MapXY(BT1Map.Catacombs3, 13, 16);
        public MapXY FECL3Skeleton99a = new MapXY(BT1Map.Catacombs3, 5, 8);
        public MapXY FECL3Skeleton99b = new MapXY(BT1Map.Catacombs3, 17, 7);
        public MapXY FECL3Wight69 = new MapXY(BT1Map.Catacombs3, 13, 4);
        public MapXY FECL3Ghoul36 = new MapXY(BT1Map.Catacombs3, 9, 3);
        public MapXY FECL3Wraith7 = new MapXY(BT1Map.Catacombs3, 0, 1);
        public MapXY FEHCL1MasterNinja = new MapXY(BT1Map.Harkyn1, 9, 18);
        public MapXY FEHCL1Wight3 = new MapXY(BT1Map.Harkyn1, 9, 14);
        public MapXY FEHCL1Golem = new MapXY(BT1Map.Harkyn1, 6, 10);
        public MapXY FEHCL1Golema = new MapXY(BT1Map.Harkyn1, 13, 10);
        public MapXY FEHCL1Golemb = new MapXY(BT1Map.Harkyn1, 6, 8);
        public MapXY FEHCL1Golemc = new MapXY(BT1Map.Harkyn1, 13, 8);
        public MapXY FEHCL1Berserker6 = new MapXY(BT1Map.Harkyn1, 16, 6);
        public MapXY FEHCL1Jabberwock = new MapXY(BT1Map.Harkyn1, 21, 2);
        public MapXY FEHCL2MasterSorcerer = new MapXY(BT1Map.Harkyn2, 9, 9);
        public MapXY FEHCL2MangarGuard8 = new MapXY(BT1Map.Harkyn2, 14, 4);
        public MapXY FEHCL3OldManLesserDemon4 = new MapXY(BT1Map.Harkyn3, 3, 5);
        public MapXY FEHCL3Berserker396 = new MapXY(BT1Map.Harkyn3, 12, 5);
        public MapXY FEKTGreenDragon6 = new MapXY(BT1Map.Kylearan, 6, 15);
        public MapXY FEKTCrystalGolem = new MapXY(BT1Map.Kylearan, 4, 1);
        public MapXY FEMTL1MazeMaster3 = new MapXY(BT1Map.Mangar1, 6, 21);
        public MapXY FEMTL1Dwarf56 = new MapXY(BT1Map.Mangar1, 0, 16);
        public MapXY FEMTL1EvilEye34 = new MapXY(BT1Map.Mangar1, 19, 11);
        public MapXY FEMTL1MasterMagician2 = new MapXY(BT1Map.Mangar1, 21, 8);
        public MapXY FEMTL1Ghost20 = new MapXY(BT1Map.Mangar1, 19, 7);
        public MapXY FEMTL1Samurai96 = new MapXY(BT1Map.Mangar1, 0, 3);
        public MapXY FEMTL1Mercenary68 = new MapXY(BT1Map.Mangar1, 4, 3);
        public MapXY FEMTL2Hobbit97 = new MapXY(BT1Map.Mangar2, 7, 21);
        public MapXY FEMTL2Basilisk3 = new MapXY(BT1Map.Mangar2, 13, 18);
        public MapXY FEMTL2MindShadow = new MapXY(BT1Map.Mangar2, 18, 9);
        public MapXY FEMTL2Bandersnatch2 = new MapXY(BT1Map.Mangar2, 9, 7);
        public MapXY FEMTL2SoulSucker = new MapXY(BT1Map.Mangar2, 6, 6);
        public MapXY FEMTL2Samurai67 = new MapXY(BT1Map.Mangar2, 15, 5);
        public MapXY FEMTL2Ghoul32 = new MapXY(BT1Map.Mangar2, 7, 3);
        public MapXY FEMTL3MasterNinja7 = new MapXY(BT1Map.Mangar3, 1, 21);
        public MapXY FEMTL3Mongoa = new MapXY(BT1Map.Mangar3, 20, 21);
        public MapXY FEMTL3Mongob = new MapXY(BT1Map.Mangar3, 15, 14);
        public MapXY FEMTL3Mongoc = new MapXY(BT1Map.Mangar3, 3, 12);
        public MapXY FEMTL3Wraith6OldMan = new MapXY(BT1Map.Mangar3, 19, 12);
        public MapXY FEMTL3Mongod = new MapXY(BT1Map.Mangar3, 14, 9);
        public MapXY FEMTL3Lv6Conjurer7 = new MapXY(BT1Map.Mangar3, 13, 8);
        public MapXY FEMTL3Mongoe = new MapXY(BT1Map.Mangar3, 20, 8);
        public MapXY FEMTL3Mongof = new MapXY(BT1Map.Mangar3, 20, 4);
        public MapXY FEMTL3Mongog = new MapXY(BT1Map.Mangar3, 11, 16);
        public MapXY FEMTL3Mongoh = new MapXY(BT1Map.Mangar3, 9, 18);
        public MapXY FEMTL4Ghoul50 = new MapXY(BT1Map.Mangar4, 6, 11);
        public MapXY FEMTL4Wraith9 = new MapXY(BT1Map.Mangar4, 8, 11);
        public MapXY FEMTL4LesserDemon7 = new MapXY(BT1Map.Mangar4, 12, 10);
        public MapXY FEMTL4VampireLord = new MapXY(BT1Map.Mangar4, 12, 8);
        public MapXY FEMTL4RedDragon2 = new MapXY(BT1Map.Mangar4, 21, 8);
        public MapXY FEMTL4Mercenary4 = new MapXY(BT1Map.Mangar4, 12, 6);
        public MapXY FEMTL4Spectre = new MapXY(BT1Map.Mangar4, 6, 5);
        public MapXY FEMTL4Vampire6 = new MapXY(BT1Map.Mangar4, 8, 5);
        public MapXY FEMTL5BlackDragon2 = new MapXY(BT1Map.Mangar5, 0, 17);
        public MapXY FEMTL5Archmage = new MapXY(BT1Map.Mangar5, 18, 13);
        public MapXY FEMTL5Spectre4 = new MapXY(BT1Map.Mangar5, 10, 10);
        public MapXY FEMTL5Balrog4 = new MapXY(BT1Map.Mangar5, 20, 5);
        public MapXY FEMTL5AncientEnemy3 = new MapXY(BT1Map.Mangar5, 17, 3);
        public MapXY FEMTL5StormGiant5 = new MapXY(BT1Map.Mangar5, 13, 0);

        public MapXY StairsUpSL1 = new MapXY(BT1Map.Sewers1, 7, 18);
        public MapXY StairsDownSL1 = new MapXY(BT1Map.Sewers1, 17, 14);
        public MapXY StairsUpSL2 = new MapXY(BT1Map.Sewers2, 17, 14);
        public MapXY StairsUpSL3 = new MapXY(BT1Map.Sewers3, 17, 16);
        public MapXY StairsDownCL1 = new MapXY(BT1Map.Catacombs1, 16, 15);
        public MapXY StairsUpCL1 = new MapXY(BT1Map.Catacombs1, 0, 0);
        public MapXY StairsUpCL2 = new MapXY(BT1Map.Catacombs2, 16, 15);
        public MapXY StairsDownCL2 = new MapXY(BT1Map.Catacombs2, 11, 8);
        public MapXY StairsUpCL3 = new MapXY(BT1Map.Catacombs3, 11, 8);
        public MapXY StairsUpHCL1 = new MapXY(BT1Map.Harkyn1, 0, 19);
        public MapXY StairsDownHCL1 = new MapXY(BT1Map.Harkyn1, 0, 0);
        public MapXY StairsDownHCL2 = new MapXY(BT1Map.Harkyn2, 0, 19);
        public MapXY StairsDownKT = new MapXY(BT1Map.Kylearan, 0, 0);
        public MapXY StairsDownMTL1 = new MapXY(BT1Map.Mangar1, 0, 0);
        public MapXY StairsUpMTL2 = new MapXY(BT1Map.Mangar2, 11, 2);
        public MapXY StairsUpMTL3 = new MapXY(BT1Map.Mangar3, 3, 9);
        public MapXY StairsDownMTL3 = new MapXY(BT1Map.Mangar3, 11, 2);
        public MapXY StairsDownMTL4 = new MapXY(BT1Map.Mangar4, 3, 9);
        public MapXY StairsDownC = new MapXY(BT1Map.Cellars, 7, 18);
        public MapXY StairsUpC = new MapXY(BT1Map.Cellars, 0, 0);

        public MapXY AscendSL3a = new MapXY(BT1Map.Sewers3, 5, 21);
        public MapXY AscendSL3b = new MapXY(BT1Map.Sewers3, 21, 11);
        public MapXY AscendHCL2 = new MapXY(BT1Map.Harkyn2, 19, 19);
        public MapXY AscendMTL4 = new MapXY(BT1Map.Mangar4, 0, 0);

        public MapXY DescendSL2a = new MapXY(BT1Map.Sewers2, 5, 21);
        public MapXY DescendSL2b = new MapXY(BT1Map.Sewers2, 21, 11);
        public MapXY DescendHCL3 = new MapXY(BT1Map.Harkyn3, 19, 19);
        public MapXY DescendMTL2 = new MapXY(BT1Map.Mangar2, 21, 17);
        public MapXY DescendMTL5 = new MapXY(BT1Map.Mangar5, 0, 0);

        public MapXY CrystalSword = new MapXY(BT1Map.Harkyn1, 19, 0);
        public MapXY YbarraShield = new MapXY(BT1Map.Harkyn2, 19, 0);
        public MapXY SilverTriangle = new MapXY(BT1Map.Kylearan, 2, 20);
        public MapXY SilverCircle = new MapXY(BT1Map.Mangar2, 4, 15);
        public MapXY SilverSquare = new MapXY(BT1Map.Harkyn2, 0, 0);
        public MapXY MasterKey = new MapXY(BT1Map.Mangar3, 19, 12);
        public MapXY ThorFigurine = new MapXY(BT1Map.Mangar4, 20, 9);

        public MapXY SpeakTheSeven = new MapXY(BT1Map.Mangar3, 10, 4);
        public MapXY SevenFirst = new MapXY(BT1Map.Mangar3, 9, 7);
        public MapXY SevenSecond = new MapXY(BT1Map.Mangar3, 13, 8);
        public MapXY SevenThird = new MapXY(BT1Map.Mangar3, 21, 15);
        public MapXY SevenFourth = new MapXY(BT1Map.Mangar3, 19, 9);
        public MapXY SevenFifth = new MapXY(BT1Map.Mangar3, 11, 12);
        public MapXY SevenSixth = new MapXY(BT1Map.Mangar3, 1, 19);
        public MapXY SevenSeventh = new MapXY(BT1Map.Mangar3, 0, 21);

        public MapXY SwapDoors = new MapXY(BT1Map.Mangar4, 3, 20);
        public MapXY MTL4Teleport1 = new MapXY(BT1Map.Mangar4, 17, 10);
        public MapXY MTL4Teleport2 = new MapXY(BT1Map.Mangar4, 21, 10);
        public MapXY MTL4Teleport3 = new MapXY(BT1Map.Mangar4, 11, 16);
        public MapXY MTL4Teleport4 = new MapXY(BT1Map.Mangar4, 1, 20);

        public MapXY Mangar = new MapXY(BT1Map.Mangar5, 10, 20);
        public MapXY ThreeShapes = new MapXY(BT1Map.Mangar5, 10, 15);

        public MapXY MTL5Teleport1 = new MapXY(BT1Map.Mangar5, 2, 0);
        public MapXY MTL5Teleport2 = new MapXY(BT1Map.Mangar5, 21, 2);
        public MapXY MTL5Teleport3 = new MapXY(BT1Map.Mangar5, 21, 10);
        public MapXY CL3Teleport1 = new MapXY(BT1Map.Catacombs3, 15, 21);
        public MapXY HCL2Teleport1 = new MapXY(BT1Map.Harkyn2, 10, 9);
        public MapXY HCL3Teleport1 = new MapXY(BT1Map.Harkyn3, 0, 6);
        public MapXY HCL3Teleport2 = new MapXY(BT1Map.Harkyn3, 10, 21);
        public MapXY HCL3MadGodStatue = new MapXY(BT1Map.Harkyn3, 21, 1);

        public MapXY KTTeleport1 = new MapXY(BT1Map.Kylearan, 19, 19);
        public MapXY KTTeleport2 = new MapXY(BT1Map.Kylearan, 19, 1);
        public MapXY KTTeleport3 = new MapXY(BT1Map.Kylearan, 14, 18);
        public MapXY KTTeleport4 = new MapXY(BT1Map.Kylearan, 17, 14);
        public MapXY ExitKylearan = new MapXY(BT1Map.Kylearan, 0, 0);
        public MapXY MeetKylearan = new MapXY(BT1Map.Kylearan, 17, 13);
        public MapXY KTRiddle1 = new MapXY(BT1Map.Kylearan, 13, 10);
        public MapXY KTRiddle2 = new MapXY(BT1Map.Kylearan, 12, 2);
        public MapXY Tarjan = new MapXY(BT1Map.Sewers2, 3, 4);

        public MapXY MTL1Teleport1 = new MapXY(BT1Map.Mangar1, 7, 6);
        public MapXY MTL1Teleport2 = new MapXY(BT1Map.Mangar1, 0, 18);
        public MapXY MTL1TeleportUp = new MapXY(BT1Map.Mangar1, 20, 13);

        public BT1Locations()
        {
        }
    }

    public class BT2Locations
    {
        // Bard's Tale 2
        public MapXY None = new MapXY(GameNames.BardsTale2, -1, 0, 0, -1);

        public MapXY Ephesus = new MapXY(BT2Map.Wilderness, 15, 41);
        public MapXY Colosse = new MapXY(BT2Map.Wilderness, 22, 32);
        public MapXY Tangramayne = new MapXY(BT2Map.Wilderness, 3, 23);
        public MapXY Philippi = new MapXY(BT2Map.Wilderness, 15, 14);
        public MapXY Corinth = new MapXY(BT2Map.Wilderness, 4, 5);
        public MapXY Thessalonica = new MapXY(BT2Map.Wilderness, 25, 7);

        public MapXY EphesusGuild = new MapXY(BT2Map.Ephesus, 8, 14);
        public MapXY ColosseGuild = new MapXY(BT2Map.Colosse, 9, 7);
        public MapXY TangramayneGuild = new MapXY(BT2Map.Tangramayne, 2, 8);
        public MapXY PhilippiGuild = new MapXY(BT2Map.Philippi, 7, 3);
        public MapXY CorinthGuild = new MapXY(BT2Map.Corinth, 3, 2);
        public MapXY ThessalonicaGuild = new MapXY(BT2Map.Thessalonica, 10, 8);

        public MapXY GreyCrypt = new MapXY(BT2Map.Wilderness, 8, 31);
        public MapXY FanskarsCastle = new MapXY(BT2Map.Wilderness, 17, 26);
        public MapXY Kazdek = new MapXY(BT2Map.Wilderness, 25, 19);
        public MapXY TempleOfNarn = new MapXY(BT2Map.Wilderness, 9, 2);
        public MapXY SagesHut = new MapXY(BT2Map.Wilderness, 0, 0);

        public MapXY EphesusExit = new MapXY(BT2Map.Ephesus, 4, 0);
        public MapXY ColosseExit = new MapXY(BT2Map.Colosse, 0, 9);
        public MapXY TangramayneExit = new MapXY(BT2Map.Tangramayne, 0, 7);
        public MapXY PhilippiExit = new MapXY(BT2Map.Philippi, 7, 15);
        public MapXY CorinthExit = new MapXY(BT2Map.Corinth, 6, 15);
        public MapXY ThessalonicaExit = new MapXY(BT2Map.Thessalonica, 15, 7);

        public MapXY FinishPrincess = new MapXY(BT2Map.Tangramayne, 14, 8);
        public MapXY DarkDomain = new MapXY(BT2Map.Tangramayne, 15, 8);
        public MapXY DDL1Up = new MapXY(BT2Map.DarkDomain1, 0, 0);
        public MapXY DDL1Down = new MapXY(BT2Map.DarkDomain1, 14, 21);
        public MapXY DDL2Up = new MapXY(BT2Map.DarkDomain2, 14, 21);
        public MapXY DDL2Down = new MapXY(BT2Map.DarkDomain2, 21, 21);
        public MapXY DDL2WingedCreature = new MapXY(BT2Map.DarkDomain2, 12, 5);
        public MapXY DDL3Up = new MapXY(BT2Map.DarkDomain3, 21, 21);
        public MapXY DDL3Down = new MapXY(BT2Map.DarkDomain3, 0, 18);
        public MapXY DDL3RiddleMangar = new MapXY(BT2Map.DarkDomain3, 14, 3);
        public MapXY DDL3RiddlePass = new MapXY(BT2Map.DarkDomain3, 1, 18);
        public MapXY DDL4Up = new MapXY(BT2Map.DarkDomain4, 0, 18);
        public MapXY DDL4ChasmSouth = new MapXY(BT2Map.DarkDomain4, 9, 10);
        public MapXY DDL4ChasmNorth = new MapXY(BT2Map.DarkDomain4, 9, 13);
        public MapXY DDL4DoorsWest = new MapXY(BT2Map.DarkDomain4, 9, 19);
        public MapXY DDL4DoorsEast = new MapXY(BT2Map.DarkDomain4, 10, 19);
        public MapXY DDL4DarkLord = new MapXY(BT2Map.DarkDomain4, 10, 21);
        public MapXY DDL4Princess = new MapXY(BT2Map.DarkDomain4, 11, 21);
        public MapXY DDL4Teleport = new MapXY(BT2Map.DarkDomain4, 21, 21);

        public MapXY TempleOfDarkness = new MapXY(BT2Map.Ephesus, 8, 7);
        public MapXY TTL1Up = new MapXY(BT2Map.Tombs1, 0, 0);
        public MapXY TTL1Down = new MapXY(BT2Map.Tombs1, 19, 20);
        public MapXY TTL1Teleport1 = new MapXY(BT2Map.Tombs1, 12, 12);
        public MapXY TTL1Teleport2 = new MapXY(BT2Map.Tombs1, 11, 2);
        public MapXY TTL2Up = new MapXY(BT2Map.Tombs2, 0, 21);
        public MapXY TTL2Down = new MapXY(BT2Map.Tombs2, 10, 16);
        public MapXY TTL2KeyMaster = new MapXY(BT2Map.Tombs2, 8, 10);
        public MapXY TTL3Snare = new MapXY(BT2Map.Tombs3, 3, 10);
        public MapXY TTL3Segment1 = new MapXY(BT2Map.Tombs3, 8, 10);
        public MapXY TTL3OldWarrior = new MapXY(BT2Map.Tombs3, 10, 11);
        public MapXY TTL3Fountain = new MapXY(BT2Map.Tombs3, 12, 9);
        public MapXY TTL3ToxicGiant = new MapXY(BT2Map.Tombs3, 10, 7);
        public MapXY TTL3TeleportSnare = new MapXY(BT2Map.Tombs3, 21, 4);
        public MapXY TTL3CreateDoor = new MapXY(BT2Map.Tombs3, 9, 10);

        public MapXY FCDown = new MapXY(BT2Map.FanskarsCastle, 0, 0);
        public MapXY FCFanskar = new MapXY(BT2Map.FanskarsCastle, 20, 21);
        public MapXY FCTeleportNorthwest = new MapXY(BT2Map.FanskarsCastle, 4, 17);
        public MapXY FCFanskarTeleport = new MapXY(BT2Map.FanskarsCastle, 21, 21);
        public MapXY FCSnare = new MapXY(BT2Map.FanskarsCastle, 11, 9);
        public MapXY FCSnareTeleport = new MapXY(BT2Map.FanskarsCastle, 13, 10);
        public MapXY FCSegment2 = new MapXY(BT2Map.FanskarsCastle, 10, 5);

        public MapXY DargothsTower = new MapXY(BT2Map.Philippi, 13, 2);
        public MapXY DTL1Teleport = new MapXY(BT2Map.DargothsTower1, 12, 4);
        public MapXY DTL1Down = new MapXY(BT2Map.DargothsTower1, 0, 0);
        public MapXY DTL1Up = new MapXY(BT2Map.DargothsTower1, 21, 18);
        public MapXY DTL2Up = new MapXY(BT2Map.DargothsTower2, 13, 20);
        public MapXY DTL2Down = new MapXY(BT2Map.DargothsTower2, 21, 18);
        public MapXY DTL2Battletest = new MapXY(BT2Map.DargothsTower2, 12, 13);
        public MapXY DTL2TeleportSouth = new MapXY(BT2Map.DargothsTower2, 7, 1);
        public MapXY DTL2TeleportSoutheast = new MapXY(BT2Map.DargothsTower2, 16, 5);
        public MapXY DTL3Down = new MapXY(BT2Map.DargothsTower3, 13, 20);
        public MapXY DTL3Up = new MapXY(BT2Map.DargothsTower3, 2, 9);
        public MapXY DTL3RiddleEarth = new MapXY(BT2Map.DargothsTower3, 2, 6);
        public MapXY DTL4Up = new MapXY(BT2Map.DargothsTower4, 18, 19);
        public MapXY DTL4Down = new MapXY(BT2Map.DargothsTower4, 2, 9);
        public MapXY DTL5Down = new MapXY(BT2Map.DargothsTower5, 18, 19);
        public MapXY DTL5Dargoth = new MapXY(BT2Map.DargothsTower5, 0, 20);
        public MapXY DTL5RiddleTenWords = new MapXY(BT2Map.DargothsTower5, 9, 12);
        public MapXY DTL5Snare = new MapXY(BT2Map.DargothsTower5, 20, 5);
        public MapXY DTL5Segment3 = new MapXY(BT2Map.DargothsTower5, 17, 5);
        public MapXY DTL5SixMessages = new MapXY(BT2Map.DargothsTower5, 6, 6);
        public MapXY DTL5CreateDoor = new MapXY(BT2Map.DargothsTower5, 18, 5);
        public MapXY DTL5RiddleHavok = new MapXY(BT2Map.DargothsTower5, 1, 0);

        public MapXY MazeOfDread = new MapXY(BT2Map.Thessalonica, 11, 14);
        public MapXY MDL1Up = new MapXY(BT2Map.MazeOfDread1, 0, 0);
        public MapXY MDL1Down1 = new MapXY(BT2Map.MazeOfDread1, 21, 21);
        public MapXY MDL1Elevator = new MapXY(BT2Map.MazeOfDread1, 21, 1);
        public MapXY MDL1Down2 = new MapXY(BT2Map.MazeOfDread1, 3, 16);
        public MapXY MDL1Down3 = new MapXY(BT2Map.MazeOfDread1, 21, 10);
        public MapXY MDL2Up1 = new MapXY(BT2Map.MazeOfDread2, 21, 21);
        public MapXY MDL2Up2 = new MapXY(BT2Map.MazeOfDread2, 3, 16);
        public MapXY MDL2Up3 = new MapXY(BT2Map.MazeOfDread2, 21, 10);
        public MapXY MDL2RiddleDer = new MapXY(BT2Map.MazeOfDread2, 10, 15);
        public MapXY MDL2Elevator = new MapXY(BT2Map.MazeOfDread2, 21, 1);
        public MapXY MDL3Elevator = new MapXY(BT2Map.MazeOfDread3, 21, 1);
        public MapXY MDL3ArmsMaster = new MapXY(BT2Map.MazeOfDread3, 10, 11);
        public MapXY MDL3GraphnarLord = new MapXY(BT2Map.MazeOfDread3, 4, 8);
        public MapXY MDL3TeleportSnare = new MapXY(BT2Map.MazeOfDread3, 17, 8);
        public MapXY MDL3Snare = new MapXY(BT2Map.MazeOfDread3, 4, 16);
        public MapXY MDL3RiddleEndurable = new MapXY(BT2Map.MazeOfDread3, 7, 17);
        public MapXY MDL3EndurableMessage = new MapXY(BT2Map.MazeOfDread3, 2, 19);
        public MapXY MDL3KeepFaith = new MapXY(BT2Map.MazeOfDread3, 4, 21);
        public MapXY MDL3Alchemist = new MapXY(BT2Map.MazeOfDread3, 6, 21);
        public MapXY MDL3TeleportSegment = new MapXY(BT2Map.MazeOfDread3, 7, 19);
        public MapXY MDL3Segment4 = new MapXY(BT2Map.MazeOfDread3, 10, 19);

        public MapXY OsconsFortress = new MapXY(BT2Map.Corinth, 8, 13);
        public MapXY OFL1Down = new MapXY(BT2Map.OsconsFortress1, 0, 0);
        public MapXY OFL1Up = new MapXY(BT2Map.OsconsFortress1, 10, 12);
        public MapXY OFL1RiddleFire = new MapXY(BT2Map.OsconsFortress1, 3, 7);
        public MapXY OFL1TeleportWest = new MapXY(BT2Map.OsconsFortress1, 0, 18);
        public MapXY OFL1TeleportNorth = new MapXY(BT2Map.OsconsFortress1, 12, 18);
        public MapXY OFL1TeleportSouth = new MapXY(BT2Map.OsconsFortress1, 14, 0);
        public MapXY OFL1TeleportSouthwest = new MapXY(BT2Map.OsconsFortress1, 1, 8);
        public MapXY OFL1TeleportNortheast = new MapXY(BT2Map.OsconsFortress1, 15, 16);
        public MapXY OFL2Down = new MapXY(BT2Map.OsconsFortress2, 10, 12);
        public MapXY OFL2RiddleDervak = new MapXY(BT2Map.OsconsFortress2, 21, 20);
        public MapXY OFL2Up = new MapXY(BT2Map.OsconsFortress2, 0, 21);
        public MapXY OFL3Down1 = new MapXY(BT2Map.OsconsFortress3, 0, 21);
        public MapXY OFL3Down2 = new MapXY(BT2Map.OsconsFortress3, 6, 17);
        public MapXY OFL3Down3 = new MapXY(BT2Map.OsconsFortress3, 7, 17);
        public MapXY OFL3Up1 = new MapXY(BT2Map.OsconsFortress3, 1, 14);
        public MapXY OFL3Up2 = new MapXY(BT2Map.OsconsFortress3, 12, 20);
        public MapXY OFL3Up3 = new MapXY(BT2Map.OsconsFortress3, 12, 19);
        public MapXY OFL3UpDown1 = new MapXY(BT2Map.OsconsFortress3, 6, 19);
        public MapXY OFL3UpDown2 = new MapXY(BT2Map.OsconsFortress3, 7, 19);
        public MapXY OFL3UpDown3 = new MapXY(BT2Map.OsconsFortress3, 8, 19);
        public MapXY OFL3RiddleStill = new MapXY(BT2Map.OsconsFortress3, 2, 12);
        public MapXY OFL4Down = new MapXY(BT2Map.OsconsFortress4, 1, 14);
        public MapXY OFL4TeleportSnare = new MapXY(BT2Map.OsconsFortress4, 10, 10);
        public MapXY OFL4Snare = new MapXY(BT2Map.OsconsFortress4, 11, 14);
        public MapXY OFL4RiddleRPS = new MapXY(BT2Map.OsconsFortress4, 11, 17);
        public MapXY OFL4Paper = new MapXY(BT2Map.OsconsFortress4, 8, 14);
        public MapXY OFL4Rock = new MapXY(BT2Map.OsconsFortress4, 11, 11);
        public MapXY OFL4Scissor = new MapXY(BT2Map.OsconsFortress4, 14, 14);
        public MapXY OFL4Segment5 = new MapXY(BT2Map.OsconsFortress4, 11, 10);
        public MapXY OFL4Oscon = new MapXY(BT2Map.OsconsFortress4, 20, 5);

        public MapXY GCL1Up = new MapXY(BT2Map.GreyCrypt1, 0, 0);
        public MapXY GCL1Down = new MapXY(BT2Map.GreyCrypt1, 0, 18);
        public MapXY GCL1RiddleWizeOne = new MapXY(BT2Map.GreyCrypt1, 1, 7);
        public MapXY GCL1TeleportSouth = new MapXY(BT2Map.GreyCrypt1, 3, 5);
        public MapXY GCL1TeleportNorth = new MapXY(BT2Map.GreyCrypt1, 21, 15);
        public MapXY GCL2VampireDragon = new MapXY(BT2Map.GreyCrypt2, 1, 5);
        public MapXY GCL2Up = new MapXY(BT2Map.GreyCrypt2, 0, 18);
        public MapXY GCL2Snare = new MapXY(BT2Map.GreyCrypt2, 11, 8);
        public MapXY GCL2TeleportSnare = new MapXY(BT2Map.GreyCrypt2, 0, 6);
        public MapXY GCL2BlueRobe = new MapXY(BT2Map.GreyCrypt2, 11, 4);
        public MapXY GCL2GreyRobe = new MapXY(BT2Map.GreyCrypt2, 21, 4);
        public MapXY GCL2MouthNorthwest = new MapXY(BT2Map.GreyCrypt2, 14, 6);
        public MapXY GCL2MouthSouthwest = new MapXY(BT2Map.GreyCrypt2, 14, 2);
        public MapXY GCL2MouthNortheast = new MapXY(BT2Map.GreyCrypt2, 18, 6);
        public MapXY GCL2MouthSoutheast = new MapXY(BT2Map.GreyCrypt2, 18, 2);
        public MapXY GCL2SpinnerDisable = new MapXY(BT2Map.GreyCrypt2, 13, 0);

        public MapXY DestinyStone = new MapXY(BT2Map.Colosse, 2, 13);
        public MapXY DSL1RiddleNear = new MapXY(BT2Map.DestinyStone1, 8, 16);
        public MapXY DSL1TeleportWest = new MapXY(BT2Map.DestinyStone1, 6, 20);
        public MapXY DSL1Down = new MapXY(BT2Map.DestinyStone1, 17, 3);
        public MapXY DSL1Up = new MapXY(BT2Map.DestinyStone1, 0, 0);
        public MapXY DSL2Dethadren = new MapXY(BT2Map.DestinyStone2, 16, 4);
        public MapXY DSL2Gandravalk = new MapXY(BT2Map.DestinyStone2, 16, 2);
        public MapXY DSL2Dartagnon = new MapXY(BT2Map.DestinyStone2, 18, 4);
        public MapXY DSL2Basilisk = new MapXY(BT2Map.DestinyStone2, 18, 2);
        public MapXY DSL2Down = new MapXY(BT2Map.DestinyStone2, 18, 3);
        public MapXY DSL3TeleportSnare = new MapXY(BT2Map.DestinyStone3, 19, 16);
        public MapXY DSL3Snare = new MapXY(BT2Map.DestinyStone3, 9, 4);
        public MapXY DSL3RiddleStormFists = new MapXY(BT2Map.DestinyStone3, 1, 13);
        public MapXY DSL3RiddleZenMaster = new MapXY(BT2Map.DestinyStone3, 8, 10);
        public MapXY DSL3RiddleGale = new MapXY(BT2Map.DestinyStone3, 18, 12);
        public MapXY DSL3RiddleArkast = new MapXY(BT2Map.DestinyStone3, 15, 13);
        public MapXY DSL3MazeGoal = new MapXY(BT2Map.DestinyStone3, 16, 9);
        public MapXY DSL3TeleportDark = new MapXY(BT2Map.DestinyStone3, 13, 10);
        public MapXY DSL3Segment7 = new MapXY(BT2Map.DestinyStone3, 15, 14);

        public MapXY EncounterFanskar = new MapXY(BT2Map.FanskarsCastle, 20, 21);
        public MapXY EncounterBodyguard = new MapXY(BT2Map.FanskarsCastle, 17, 12);
        public MapXY EncounterVampireDragon = new MapXY(BT2Map.GreyCrypt2, 1, 5);
        public MapXY EncounterDethadren = new MapXY(BT2Map.DestinyStone2, 16, 4);
        public MapXY EncounterDartagnon = new MapXY(BT2Map.DestinyStone2, 18, 4);
        public MapXY EncounterGrandravalk = new MapXY(BT2Map.DestinyStone2, 16, 2);
        public MapXY EncounterBasilisk = new MapXY(BT2Map.DestinyStone2, 18, 2);
        public MapXY EncounterMassacreMage = new MapXY(BT2Map.OsconsFortress2, 21, 20);
        public MapXY EncounterFredTheDop = new MapXY(BT2Map.OsconsFortress2, 11, 3);
        public MapXY EncounterTroyTheDop = new MapXY(BT2Map.OsconsFortress2, 8, 2);
        public MapXY EncounterMattTheDop = new MapXY(BT2Map.OsconsFortress2, 12, 2);
        public MapXY EncounterSteveTheDop = new MapXY(BT2Map.OsconsFortress2, 12, 1);
        public MapXY EncounterMarvinTheDop = new MapXY(BT2Map.OsconsFortress2, 8, 0);
        public MapXY EncounterOscon = new MapXY(BT2Map.OsconsFortress4, 20, 5);
        public MapXY EncounterDeadKing = new MapXY(BT2Map.Tombs2, 8, 6);
        public MapXY EncounterOldWarrior = new MapXY(BT2Map.Tombs3, 10, 11);
        public MapXY EncounterToxicGiant = new MapXY(BT2Map.Tombs3, 10, 7);
        public MapXY EncounterBurner = new MapXY(BT2Map.DargothsTower1, 16, 13);
        public MapXY EncounterDargoth = new MapXY(BT2Map.DargothsTower5, 0, 20);
        public MapXY EncounterWebDragon = new MapXY(BT2Map.DargothsTower5, 21, 7);
        public MapXY EncounterGuardian1 = new MapXY(BT2Map.DarkDomain1, 15, 21);
        public MapXY EncounterGuardian2 = new MapXY(BT2Map.DarkDomain1, 12, 20);
        public MapXY EncounterGuardian3 = new MapXY(BT2Map.DarkDomain1, 16, 20);
        public MapXY EncounterGuardian4 = new MapXY(BT2Map.DarkDomain1, 12, 19);
        public MapXY EncounterGuardian5 = new MapXY(BT2Map.DarkDomain1, 16, 19);
        public MapXY EncounterGuardian6 = new MapXY(BT2Map.DarkDomain1, 12, 18);
        public MapXY EncounterGuardian7 = new MapXY(BT2Map.DarkDomain1, 16, 18);
        public MapXY EncounterGuardian8 = new MapXY(BT2Map.DarkDomain1, 0, 13);
        public MapXY EncounterMedusa1 = new MapXY(BT2Map.DarkDomain2, 0, 13);
        public MapXY EncounterMedusa2 = new MapXY(BT2Map.DarkDomain2, 13, 10);
        public MapXY EncounterWingedCreature = new MapXY(BT2Map.DarkDomain2, 12, 5);
        public MapXY EncounterTheDarkLord = new MapXY(BT2Map.DarkDomain4, 10, 21);
        public MapXY EncounterPrincess = new MapXY(BT2Map.DarkDomain4, 11, 21);
        public MapXY EncounterArmsMaster = new MapXY(BT2Map.MazeOfDread3, 10, 11);
        public MapXY EncounterGraphnarLord = new MapXY(BT2Map.MazeOfDread3, 4, 8);

        public MapXY[] Towns, Dungeons;

        public BT2Locations()
        {
            Towns = new MapXY[] { Ephesus, Colosse, Tangramayne, Philippi, Corinth, Thessalonica };
            Dungeons = new MapXY[] { GreyCrypt, FanskarsCastle, Kazdek, TempleOfNarn, SagesHut };
        }
    }

    public class BT3Locations
    {
        // Bard's Tale 2
        public MapXY None = new MapXY(GameNames.BardsTale3, -1, 0, 0, -1);

        public MapXY[] Towns, Dungeons;

        public BT3Locations()
        {
            Towns = new MapXY[] {  };
            Dungeons = new MapXY[] {  };
        }
    }
}