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
        // Bard's Tale 3
        public MapXY None = new MapXY(GameNames.BardsTale3, -1, 0, 0, -1);

        public MapXY W_SkaraBrae = new MapXY(BT3Map.Wilderness, 10, 15);
        public MapXY W_ToGelidia = new MapXY(BT3Map.Wilderness, 0, 19);
        public MapXY W_ToLucencia = new MapXY(BT3Map.Wilderness, 17, 17);
        public MapXY W_RefugeeCamp = new MapXY(BT3Map.Wilderness, 15, 12);
        public MapXY W_Tavern = new MapXY(BT3Map.Wilderness, 17, 12);
        public MapXY W_ToTarmitia = new MapXY(BT3Map.Wilderness, 10, 10);
        public MapXY W_ToTenabrosia = new MapXY(BT3Map.Wilderness, 19, 9);
        public MapXY W_Temple = new MapXY(BT3Map.Wilderness, 1, 8);
        public MapXY W_ToArboria = new MapXY(BT3Map.Wilderness, 6, 5);
        public MapXY W_ToKinestia = new MapXY(BT3Map.Wilderness, 2, 3);
        public MapXY W_ToMalefia = new MapXY(BT3Map.Wilderness, 18, 1);

        public MapXY A_Fisherman = new MapXY(BT3Map.Arboria, 1, 11);
        public MapXY A_ToCrystalPalace = new MapXY(BT3Map.Arboria, 1, 10);
        public MapXY A_ToWilderness = new MapXY(BT3Map.Arboria, 10, 11);
        public MapXY A_Arefolia = new MapXY(BT3Map.Arboria, 10, 9);
        public MapXY A_ToCieraBrannia = new MapXY(BT3Map.Arboria, 6, 6);
        public MapXY A_ToValariansTower = new MapXY(BT3Map.Arboria, 1, 4);
        public MapXY A_Acorn = new MapXY(BT3Map.Arboria, 4, 3);
        public MapXY A_ToFesteringPit1 = new MapXY(BT3Map.Arboria, 9, 3);

        public MapXY CB_ToWilderness = new MapXY(BT3Map.CieraBrannia, 8, 15);
        public MapXY CB_KingsCourt = new MapXY(BT3Map.CieraBrannia, 8, 11);
        public MapXY CB_Temple = new MapXY(BT3Map.CieraBrannia, 4, 10);
        public MapXY CB_SacredGrove = new MapXY(BT3Map.CieraBrannia, 7, 9);
        public MapXY CB_Tavern1 = new MapXY(BT3Map.CieraBrannia, 12, 7);
        public MapXY CB_Tavern2 = new MapXY(BT3Map.CieraBrannia, 3, 2);
        public MapXY CB_WizardGuild = new MapXY(BT3Map.CieraBrannia, 6, 5);

        public MapXY CEB_ToLucencia = new MapXY(BT3Map.CelariaBree, 0, 7);
        public MapXY CEB_Tavern = new MapXY(BT3Map.CelariaBree, 7, 13);
        public MapXY CEB_BardsHall = new MapXY(BT3Map.CelariaBree, 9, 13);
        public MapXY CEB_WizardGuild = new MapXY(BT3Map.CelariaBree, 5, 4);
        public MapXY CEB_Temple = new MapXY(BT3Map.CelariaBree, 7, 2);

        public MapXY FP1_ToArboria = new MapXY(BT3Map.FesteringPit1, 0, 0);
        public MapXY FP1_ToFesteringPit2A = new MapXY(BT3Map.FesteringPit1, 14, 14);
        public MapXY FP1_ToFesteringPit2B = new MapXY(BT3Map.FesteringPit1, 10, 6);
        public MapXY FP1_ToFesteringPit2C = new MapXY(BT3Map.FesteringPit1, 9, 4);
        public MapXY FP2_ToFesteringPit1A = new MapXY(BT3Map.FesteringPit2, 11, 11);
        public MapXY FP2_ToFesteringPit1B = new MapXY(BT3Map.FesteringPit2, 8, 3);
        public MapXY FP2_ToFesteringPit1C = new MapXY(BT3Map.FesteringPit2, 7, 1);
        public MapXY FP2_TslothaGarnath = new MapXY(BT3Map.FesteringPit2, 2, 11);

        public MapXY CP_ToArboria = new MapXY(BT3Map.CrystalPalace, 14, 4);
        public MapXY CP_WaterOfLife = new MapXY(BT3Map.CrystalPalace, 2, 6);

        public MapXY VT1_ToArboria = new MapXY(BT3Map.ValariansTower1, 0, 2);
        public MapXY VT1_ToValariansTower2 = new MapXY(BT3Map.ValariansTower1, 3, 3);

        public MapXY VT2_ToValariansTower1 = new MapXY(BT3Map.ValariansTower2, 3, 3);
        public MapXY VT2_ToValariansTower3 = new MapXY(BT3Map.ValariansTower2, 0, 2);

        public MapXY VT3_ToValariansTower2 = new MapXY(BT3Map.ValariansTower3, 0, 2);
        public MapXY VT3_ToValariansTower4 = new MapXY(BT3Map.ValariansTower3, 2, 2);
        public MapXY VT3_StoneDisk = new MapXY(BT3Map.ValariansTower3, 1, 2);

        public MapXY VT4_ToValariansTower3 = new MapXY(BT3Map.ValariansTower4, 2, 2);
        public MapXY VT4_Nightspear = new MapXY(BT3Map.ValariansTower4, 3, 4);

        public MapXY SG_ToCieraBrannia = new MapXY(BT3Map.SacredGrove, 0, 9);
        public MapXY SG_Valarian = new MapXY(BT3Map.SacredGrove, 4, 3);
        public MapXY SG_ValarianOuterChamber = new MapXY(BT3Map.SacredGrove, 4, 4);
        public MapXY SG_ValariansBow = new MapXY(BT3Map.SacredGrove, 9, 0);

        public MapXY G_ToWilderness = new MapXY(BT3Map.Gelidia, 9, 14);
        public MapXY G_Outpost = new MapXY(BT3Map.Gelidia, 5, 12);
        public MapXY G_ToIceKeep1 = new MapXY(BT3Map.Gelidia, 10, 6);

        public MapXY WT1_ToIceKeep1 = new MapXY(BT3Map.WhiteTower1, 0, 0);
        public MapXY WT1_ToWhiteTower2 = new MapXY(BT3Map.WhiteTower1, 4, 4);
        public MapXY WT2_UpDown = new MapXY(BT3Map.WhiteTower2, 4, 4);
        public MapXY WT3_UpDown = new MapXY(BT3Map.WhiteTower3, 4, 4);
        public MapXY WT4_ToWhiteTower3 = new MapXY(BT3Map.WhiteTower4, 4, 4);
        public MapXY WT4_CrystalLens = new MapXY(BT3Map.WhiteTower4, 4, 0);

        public MapXY GT1_ToIceKeep1 = new MapXY(BT3Map.GreyTower1, 4, 0);
        public MapXY GT1_ToGreyTower2 = new MapXY(BT3Map.GreyTower1, 2, 2);
        public MapXY GT2_ToGreyTower1 = new MapXY(BT3Map.GreyTower2, 2, 2);
        public MapXY GT2_ToGreyTower3 = new MapXY(BT3Map.GreyTower2, 4, 4);
        public MapXY GT3_ToGreyTower2 = new MapXY(BT3Map.GreyTower3, 4, 4);
        public MapXY GT3_ToGreyTower4 = new MapXY(BT3Map.GreyTower3, 0, 0);
        public MapXY GT4_ToGreyTower3 = new MapXY(BT3Map.GreyTower4, 0, 0);
        public MapXY GT4_SmokeyLens = new MapXY(BT3Map.GreyTower4, 2, 3);

        public MapXY BT1_ToIceKeep1 = new MapXY(BT3Map.BlackTower1, 0, 4);
        public MapXY BT1_ToBlackTower2 = new MapXY(BT3Map.BlackTower1, 4, 2);
        public MapXY BT2_ToBlackTower1 = new MapXY(BT3Map.BlackTower2, 4, 2);
        public MapXY BT2_ToBlackTower3 = new MapXY(BT3Map.BlackTower2, 0, 2);
        public MapXY BT3_ToBlackTower2 = new MapXY(BT3Map.BlackTower3, 0, 2);
        public MapXY BT3_ToBlackTower4 = new MapXY(BT3Map.BlackTower3, 3, 3);
        public MapXY BT4_ToBlackTower3 = new MapXY(BT3Map.BlackTower4, 3, 3);
        public MapXY BT4_BlackLens = new MapXY(BT3Map.BlackTower4, 3, 4);

        public MapXY ID1_ToIceKeep1 = new MapXY(BT3Map.IceDungeon1, 2, 8);
        public MapXY ID1_ToIceDungeon2 = new MapXY(BT3Map.IceDungeon1, 8, 8);
        public MapXY ID2_ToIceDungeon1 = new MapXY(BT3Map.IceDungeon2, 0, 4);
        public MapXY ID2_RiddleCala = new MapXY(BT3Map.IceDungeon2, 4, 3);
        public MapXY ID2_LanatirDoor = new MapXY(BT3Map.IceDungeon2, 4, 3);
        public MapXY ID2_SphereOfLanatir = new MapXY(BT3Map.IceDungeon2, 4, 0);

        public MapXY IK1_ToGelidia = new MapXY(BT3Map.IceKeep1, 1, 0);
        public MapXY IK1_ToIceKeep2A = new MapXY(BT3Map.IceKeep1, 6, 9);
        public MapXY IK1_ToIceKeep2B = new MapXY(BT3Map.IceKeep1, 3, 0);
        public MapXY IK1_ToIceKeep2C = new MapXY(BT3Map.IceKeep1, 11, 2);
        public MapXY IK1_ToIceDungeon = new MapXY(BT3Map.IceKeep1, 5, 9);
        public MapXY IK1_ToGreyTower = new MapXY(BT3Map.IceKeep1, 0, 9);
        public MapXY IK1_ToWhiteTower = new MapXY(BT3Map.IceKeep1, 11, 9);
        public MapXY IK1_ToBlackTower = new MapXY(BT3Map.IceKeep1, 11, 0);

        public MapXY IK2_ToIceKeep1A = new MapXY(BT3Map.IceKeep2, 6, 9);
        public MapXY IK2_ToIceKeep1B = new MapXY(BT3Map.IceKeep2, 3, 0);
        public MapXY IK2_ToIceKeep1C = new MapXY(BT3Map.IceKeep2, 11, 2);

        public MapXY L_ToMountain1 = new MapXY(BT3Map.Lucencia, 1, 10);
        public MapXY L_WhiteRose = new MapXY(BT3Map.Lucencia, 3, 10);
        public MapXY L_ToWilderness = new MapXY(BT3Map.Lucencia, 8, 11);
        public MapXY L_RainbowRose = new MapXY(BT3Map.Lucencia, 6, 8);
        public MapXY L_ToCelariaBree = new MapXY(BT3Map.Lucencia, 9, 6);
        public MapXY L_ToAlliriasTomb1 = new MapXY(BT3Map.Lucencia, 1, 6);
        public MapXY L_BlueRose = new MapXY(BT3Map.Lucencia, 1, 5);
        public MapXY L_YellowRose = new MapXY(BT3Map.Lucencia, 8, 2);
        public MapXY L_RedRose = new MapXY(BT3Map.Lucencia, 3, 1);
        public MapXY L_CyanisTower1 = new MapXY(BT3Map.Lucencia, 4, 1);

        public MapXY M1_ToLucencia = new MapXY(BT3Map.Mountain1, 9, 0);
        public MapXY M1_ToMountain2 = new MapXY(BT3Map.Mountain1, 8, 6);
        public MapXY M2_ToMountain1 = new MapXY(BT3Map.Mountain2, 0, 10);
        public MapXY M2_CrystalKey = new MapXY(BT3Map.Mountain2, 5, 0);

        public MapXY CT1_ToLucencia = new MapXY(BT3Map.CyanisTower1, 0, 0);
        public MapXY CT1_CrystalLock = new MapXY(BT3Map.CyanisTower1, 0, 1);
        public MapXY CT1_ToCyanisTower2 = new MapXY(BT3Map.CyanisTower1, 3, 3);
        public MapXY CT2_ToCyanisTower1 = new MapXY(BT3Map.CyanisTower2, 3, 3);
        public MapXY CT2_ToCyanisTower3 = new MapXY(BT3Map.CyanisTower2, 3, 5);
        public MapXY CT3_ToCyanisTower2 = new MapXY(BT3Map.CyanisTower3, 3, 5);
        public MapXY CT3_Cyanis = new MapXY(BT3Map.CyanisTower3, 3, 2);

        public MapXY AT1_ToLucencia = new MapXY(BT3Map.AlliriasTomb1, 0, 6);
        public MapXY AT1_BlackCrystal = new MapXY(BT3Map.AlliriasTomb1, 6, 15);
        public MapXY AT1_ToAlliriasTomb2 = new MapXY(BT3Map.AlliriasTomb1, 6, 16);

        public MapXY AT2_ToAlliriasTomb1 = new MapXY(BT3Map.AlliriasTomb2, 0, 0);
        public MapXY AT2_FlowerOfTruth = new MapXY(BT3Map.AlliriasTomb2, 4, 0);
        public MapXY AT2_FlowerOfNature = new MapXY(BT3Map.AlliriasTomb2, 10, 0);
        public MapXY AT2_FlowerOfAlliria = new MapXY(BT3Map.AlliriasTomb2, 10, 4);
        public MapXY AT2_BeltOfAlliria = new MapXY(BT3Map.AlliriasTomb2, 9, 5);

        public MapXY W_ToHiroshimaRiddle = new MapXY(BT3Map.Wasteland, 2, 15);
        public MapXY W_ToBerlin = new MapXY(BT3Map.Wasteland, 4, 13);
        public MapXY W_ToNottingham = new MapXY(BT3Map.Wasteland, 8, 8);
        public MapXY W_ToKunWang = new MapXY(BT3Map.Wasteland, 1, 4);
        public MapXY W_LearnMars = new MapXY(BT3Map.Wasteland, 1, 1);
        public MapXY TA_ToWilderness = new MapXY(BT3Map.Tarmitia, 11, 11);
        public MapXY TA_Werra1 = new MapXY(BT3Map.Tarmitia, 0, 0);
        public MapXY TA_Werra2 = new MapXY(BT3Map.Tarmitia, 0, 1);
        public MapXY B_ToRome = new MapXY(BT3Map.Berlin, 1, 10);
        public MapXY B_ToWasteland = new MapXY(BT3Map.Berlin, 10, 6);
        public MapXY B_ToNottingham = new MapXY(BT3Map.Berlin, 5, 5);
        public MapXY B_ToTarmitiaRiddle = new MapXY(BT3Map.Berlin, 7, 4);
        public MapXY B_ToWilderness = new MapXY(BT3Map.Berlin, 0, 0);
        public MapXY B_LearnAres = new MapXY(BT3Map.Berlin, 5, 1);
        public MapXY S_ToNottingham = new MapXY(BT3Map.Stalingrad, 9, 11);
        public MapXY S_ToHiroshima = new MapXY(BT3Map.Stalingrad, 3, 6);
        public MapXY S_ToKunWang = new MapXY(BT3Map.Stalingrad, 11, 2);
        public MapXY S_ToRomeRiddle = new MapXY(BT3Map.Stalingrad, 9, 0);
        public MapXY S_LearnSdiabm = new MapXY(BT3Map.Stalingrad, 9, 8);
        public MapXY H_ToStalingrad = new MapXY(BT3Map.Hiroshima, 5, 10);
        public MapXY H_ToTroy = new MapXY(BT3Map.Hiroshima, 7, 8);
        public MapXY H_ToRome = new MapXY(BT3Map.Hiroshima, 10, 3);
        public MapXY H_ToTroyRiddle = new MapXY(BT3Map.Hiroshima, 10, 1);
        public MapXY H_LearnTyr = new MapXY(BT3Map.Hiroshima, 6, 8);
        public MapXY T_ToKunWang = new MapXY(BT3Map.Troy, 4, 9);
        public MapXY T_ToRome = new MapXY(BT3Map.Troy, 7, 5);
        public MapXY T_ToNottinghamRiddle = new MapXY(BT3Map.Troy, 2, 4);
        public MapXY T_ToHiroshima = new MapXY(BT3Map.Troy, 4, 2);
        public MapXY T_LearnSvarazic = new MapXY(BT3Map.Troy, 5, 11);
        public MapXY R_ToBerlin = new MapXY(BT3Map.Rome, 9, 8);
        public MapXY R_ToHiroshima = new MapXY(BT3Map.Rome, 2, 6);
        public MapXY R_ToKunWangRiddle = new MapXY(BT3Map.Rome, 6, 6);
        public MapXY R_LearnStGeorge = new MapXY(BT3Map.Rome, 1, 0);
        public MapXY R_ToTroy = new MapXY(BT3Map.Rome, 3, 0);
        public MapXY N_LearnYenLoWang = new MapXY(BT3Map.Nottingham, 9, 8);
        public MapXY N_ToStalingrad = new MapXY(BT3Map.Nottingham, 1, 8);
        public MapXY N_ToStalingradRiddle = new MapXY(BT3Map.Nottingham, 6, 6);
        public MapXY N_ToWasteland = new MapXY(BT3Map.Nottingham, 2, 1);
        public MapXY N_ToBerlin = new MapXY(BT3Map.Nottingham, 10, 1);
        public MapXY KW_ToTroy = new MapXY(BT3Map.KunWang, 4, 11);
        public MapXY KW_ToStalingrad = new MapXY(BT3Map.KunWang, 1, 8);
        public MapXY KW_ToWasteland = new MapXY(BT3Map.KunWang, 10, 6);
        public MapXY KW_ToWastelandRiddle = new MapXY(BT3Map.KunWang, 4, 3);
        public MapXY KW_LearnSusaNoO = new MapXY(BT3Map.KunWang, 3, 1);

        public MapXY M1_ToWilderness = new MapXY(BT3Map.Malefia1, 10, 0);
        public MapXY M1_Alliria = new MapXY(BT3Map.Malefia1, 3, 16);
        public MapXY M2_Lanatir = new MapXY(BT3Map.Malefia2, 9, 12);
        public MapXY M2_Valarian = new MapXY(BT3Map.Malefia2, 18, 16);
        public MapXY M3_Sceadu = new MapXY(BT3Map.Malefia3, 15, 20);
        public MapXY M3_Werra = new MapXY(BT3Map.Malefia3, 12, 5);
        public MapXY M3_Ferofist = new MapXY(BT3Map.Malefia3, 21, 2);
        public MapXY M3_Doorway = new MapXY(BT3Map.Malefia3, 11, 7);
        public MapXY M3_HighPriestess = new MapXY(BT3Map.Malefia3, 11, 8);
        public MapXY M3_Redbeard = new MapXY(BT3Map.Malefia3, 10, 11);
        public MapXY M3_ToTarjan = new MapXY(BT3Map.Malefia3, 11, 11);
        public MapXY T_ToWilderness = new MapXY(BT3Map.Tarjan, 0, 0);
        public MapXY T_NearTarjan = new MapXY(BT3Map.Tarjan, 3, 3);
        public MapXY T_Tarjan = new MapXY(BT3Map.Tarjan, 3, 2);

        public MapXY B_ToFerofists = new MapXY(BT3Map.Barracks, 11, 14);
        public MapXY B_RightKey = new MapXY(BT3Map.Barracks, 11, 2);
        public MapXY F_ToKinestia = new MapXY(BT3Map.Ferofists, 0, 0);
        public MapXY F_ToWorkshop = new MapXY(BT3Map.Ferofists, 13, 17);
        public MapXY F_ToPrivateQuarter1 = new MapXY(BT3Map.Ferofists, 17, 11);
        public MapXY F_ToPrivateQuarter2 = new MapXY(BT3Map.Ferofists, 17, 4);
        public MapXY F_ToBarracks = new MapXY(BT3Map.Ferofists, 11, 0);
        public MapXY F_Hawkslayer = new MapXY(BT3Map.Ferofists, 3, 2);
        public MapXY PQ_Ferofist = new MapXY(BT3Map.PrivateQuarter, 2, 15);
        public MapXY PQ_ToFerofists1 = new MapXY(BT3Map.PrivateQuarter, 0, 10);
        public MapXY PQ_ToFerofists2 = new MapXY(BT3Map.PrivateQuarter, 0, 3);
        public MapXY PQ_LeftKey = new MapXY(BT3Map.PrivateQuarter, 8, 11);
        public MapXY W_ToFerofists = new MapXY(BT3Map.Workshop, 6, 0);
        public MapXY W_ToUrmechsParadise = new MapXY(BT3Map.Workshop, 2, 2);
        public MapXY UP_ToWorkshop = new MapXY(BT3Map.UrmechsParadise, 7, 0);
        public MapXY UP_ToViscousPlane = new MapXY(BT3Map.UrmechsParadise, 7, 3);
        public MapXY VP_ToUrmechsParadise = new MapXY(BT3Map.ViscousPlane, 7, 8);
        public MapXY VP_ToSanctum = new MapXY(BT3Map.ViscousPlane, 0, 0);
        public MapXY S_Urmech = new MapXY(BT3Map.Sanctum, 6, 7);
        public MapXY S_FerofistsHelm = new MapXY(BT3Map.Sanctum, 7, 3);
        public MapXY S_Geomancer = new MapXY(BT3Map.Sanctum, 6, 2);
        public MapXY S_ToViscousPlane = new MapXY(BT3Map.Sanctum, 12, 3);

        public MapXY SB_ToWilderness = new MapXY(BT3Map.SkaraBraeRuins, 0, 10);
        public MapXY SB_Storage = new MapXY(BT3Map.SkaraBraeRuins, 3, 10);
        public MapXY SB_Temple = new MapXY(BT3Map.SkaraBraeRuins, 11, 12);
        public MapXY SB_Credits = new MapXY(BT3Map.SkaraBraeRuins, 13, 14);
        public MapXY SB_ReviewBoard = new MapXY(BT3Map.SkaraBraeRuins, 15, 14);
        public MapXY C_ToSkaraBrae = new MapXY(BT3Map.Catacombs1, 0, 0);
        public MapXY C_ToTunnels = new MapXY(BT3Map.Catacombs1, 2, 12);
        public MapXY T_ToCatacombs = new MapXY(BT3Map.Catacombs2, 0, 0);
        public MapXY T_LearnChaos = new MapXY(BT3Map.Catacombs2, 20, 0);
        public MapXY U1_ToSkaraBrae = new MapXY(BT3Map.Unterbrae1, 14, 0);
        public MapXY U1_RiddleBlue = new MapXY(BT3Map.Unterbrae1, 11, 14);
        public MapXY U1_ToUnterbrae2 = new MapXY(BT3Map.Unterbrae1, 12, 14);
        public MapXY U2_ToUnterbrae1 = new MapXY(BT3Map.Unterbrae2, 12, 14);
        public MapXY U2_ToUnterbrae3 = new MapXY(BT3Map.Unterbrae2, 0, 5);
        public MapXY U2_RiddleShadow = new MapXY(BT3Map.Unterbrae2, 1, 3);
        public MapXY U3_ToUnterbrae2 = new MapXY(BT3Map.Unterbrae3, 0, 5);
        public MapXY U3_RiddleSword = new MapXY(BT3Map.Unterbrae3, 2, 0);
        public MapXY U3_ToUnterbrae4 = new MapXY(BT3Map.Unterbrae3, 0, 0);
        public MapXY U4_ToUnterbrae3 = new MapXY(BT3Map.Unterbrae4, 0, 0);
        public MapXY U4_Brilhasti = new MapXY(BT3Map.Unterbrae4, 1, 20);

        public MapXY N_ToTenabrosia = new MapXY(BT3Map.Nowhere, 5, 10);
        public MapXY N_ToBlackScar = new MapXY(BT3Map.Nowhere, 8, 8);
        public MapXY N_ToTarQuarry = new MapXY(BT3Map.Nowhere, 2, 7);
        public MapXY N_ToSceadusDemesne = new MapXY(BT3Map.Nowhere, 5, 5);
        public MapXY N_ToDarkCopse = new MapXY(BT3Map.Nowhere, 1, 3);
        public MapXY N_ToShadowCanyon = new MapXY(BT3Map.Nowhere, 7, 2);
        public MapXY SC_WeakWall1 = new MapXY(BT3Map.Nowhere, 2, 9
            );
        public MapXY DC_ToNowhere = new MapXY(BT3Map.DarkCopse, 5, 10);
        public MapXY DC_ShadowDoor = new MapXY(BT3Map.DarkCopse, 5, 5);

        public MapXY BS_ToNowhere = new MapXY(BT3Map.BlackScar, 1, 15);
        public MapXY BS_Tavern1 = new MapXY(BT3Map.BlackScar, 9, 15);
        public MapXY BS_Tavern2 = new MapXY(BT3Map.BlackScar, 15, 9);
        public MapXY BS_Tavern3 = new MapXY(BT3Map.BlackScar, 11, 8);
        public MapXY BS_Tavern4 = new MapXY(BT3Map.BlackScar, 2, 5);
        public MapXY BS_Tavern5 = new MapXY(BT3Map.BlackScar, 8, 4);
        public MapXY BS_Temple1 = new MapXY(BT3Map.BlackScar, 0, 10);
        public MapXY BS_Temple2 = new MapXY(BT3Map.BlackScar, 8, 9);
        public MapXY BS_Temple3 = new MapXY(BT3Map.BlackScar, 4, 8);
        public MapXY BS_BardHall = new MapXY(BT3Map.BlackScar, 6, 7);
        public MapXY BS_WizardGuild = new MapXY(BT3Map.BlackScar, 9, 7);
        public MapXY TQ_ToNowhere = new MapXY(BT3Map.TarQuarry, 10, 16);
        public MapXY TQ_MoltenTar = new MapXY(BT3Map.TarQuarry, 5, 9);
        public MapXY SC_ToNowhere = new MapXY(BT3Map.ShadowCanyon, 3, 21);
        public MapXY SC_ShadowLock = new MapXY(BT3Map.ShadowCanyon, 3, 7);
        public MapXY SD1_ToNowhere = new MapXY(BT3Map.SceadusDemesne1, 1, 1);
        public MapXY SD1_ToSceadusDemesne2 = new MapXY(BT3Map.SceadusDemesne1, 9, 6);
        public MapXY SD2_ToSceadusDemesne1 = new MapXY(BT3Map.SceadusDemesne2, 0, 0);
        public MapXY SD2_PhasableDoor = new MapXY(BT3Map.SceadusDemesne2, 7, 10);
        public MapXY SD2_Sceadu = new MapXY(BT3Map.SceadusDemesne2, 5, 13);

        // Quest-bit related
        public MapXY A_1011_Hawkslayer = new MapXY(BT3Map.Arboria, 10, 11);               // Bit 42 (check), Bit 26 (check), Bit 26 (set)
        public MapXY CB_0811_King = new MapXY(BT3Map.CieraBrannia, 8, 11);                // Bit 7 (check), Bit 20 (check), Bit 20 (set), Bit 7 (set)
        public MapXY CB_0709_King = new MapXY(BT3Map.CieraBrannia, 7, 9);                 // Bit 7 (check)
        public MapXY FP2_0211_Tslotha = new MapXY(BT3Map.FesteringPit2, 2, 11);           // Bit 4 (check), Bit 4 (set), Bit 5 (check), Bit 5 (set), Bit 6 (check), Bit 6 (set)
        public MapXY VT3_0102_Acorn = new MapXY(BT3Map.ValariansTower3, 1, 2);            // Bit 1 (check), Bit 0 (check), Bit 1 (set), Bit 0 (set)
        public MapXY VT4_0304_Nightspear = new MapXY(BT3Map.ValariansTower4, 3, 4);       // Bit 2 (check), Bit 2 (set)
        public MapXY SG_0403_Valarian = new MapXY(BT3Map.SacredGrove, 4, 3);              // Bit 24 (check), Bit 23 (check), Bit 24 (set), Bit 23 (set)
        public MapXY SG_0900_Chamber = new MapXY(BT3Map.SacredGrove, 9, 0);               // Bit 21 (check), Bit 21 (set), Bit 22 (check), Bit 22 (set)
        public MapXY WT4_0400_CrystalLens = new MapXY(BT3Map.WhiteTower4, 4, 0);          // Bit 48 (check), Bit 49 (check), Bit 48 (set), Bit 49 (set)
        public MapXY GT4_0203_SmokeyLens = new MapXY(BT3Map.GreyTower4, 2, 3);            // Bit 50 (check), Bit 51 (check), Bit 50 (set), Bit 51 (set)
        public MapXY BT4_0304_BlackLens = new MapXY(BT3Map.BlackTower4, 3, 4);            // Bit 52 (check), Bit 53 (check), Bit 52 (set), Bit 53 (set)
        public MapXY ID2_0400_Lanatir = new MapXY(BT3Map.IceDungeon2, 4, 0);              // Bit 60 (check), Bit 61 (check), Bit 60 (set), Bit 61 (set)
        public MapXY IK1_0009_GreyTowerDoor = new MapXY(BT3Map.IceKeep1, 0, 9);           // Bit 11 (check), Bit 12 (check), Bit 13 (check), Bit 14 (check), Bit 11 (set), Bit 12 (set), Bit 13 (set), Bit 14 (set)
        public MapXY IK1_0509_IceDungeonEnt = new MapXY(BT3Map.IceKeep1, 5, 9);           // Bit 80 (check), Bit 81 (check), Bit 82 (check), Bit 80 (set), Bit 81 (set), Bit 82 (set)
        public MapXY IK1_1109_WhiteTowerDoor = new MapXY(BT3Map.IceKeep1, 11, 9);         // Bit 8 (check), Bit 9 (check), Bit 10 (check), Bit 8 (set), Bit 9 (set), Bit 10 (set)
        public MapXY IK1_1100_BlackTowerDoor = new MapXY(BT3Map.IceKeep1, 11, 0);         // Bit 15 (check), Bit 16 (check), Bit 17 (check), Bit 18 (check), Bit 19 (check), Bit 15 (set), Bit 16 (set), Bit 17 (set), Bit 18 (set), Bit 19 (set)
        public MapXY L_0608_RainbowRose = new MapXY(BT3Map.Lucencia, 6, 8);               // Bit 32 (check), Bit 32 (set), Bit 32 (reset)
        public MapXY M2_0500_DragonBlood = new MapXY(BT3Map.Mountain2, 5, 0);             // Bit 27 (check), Bit 28 (check), Bit 27 (set), Bit 28 (set)
        public MapXY CT3_0005_Figures = new MapXY(BT3Map.CyanisTower3, 0, 5);             // Bit 29 (check), Bit 30 (check)
        public MapXY CT3_0302_Cyanis = new MapXY(BT3Map.CyanisTower3, 3, 2);              // Bit 29 (check), Bit 30 (check), Bit 31 (check), Bit 29 (set), Bit 30 (set), Bit 31 (set)
        public MapXY AT1_0615_Crystal = new MapXY(BT3Map.AlliriasTomb1, 6, 15);           // Bit 40 (check), Bit 40 (set)
        public MapXY AT2_0508_Teleport = new MapXY(BT3Map.AlliriasTomb2, 5, 8);           // Bit 35 (check)
        public MapXY AT2_0608_RedRose = new MapXY(BT3Map.AlliriasTomb2, 6, 8);            // Bit 35 (set)
        public MapXY AT2_0905_Alliria = new MapXY(BT3Map.AlliriasTomb2, 9, 5);            // Bit 38 (check), Bit 39 (check), Bit 38 (set), Bit 39 (set)
        public MapXY AT2_1005_Teleport = new MapXY(BT3Map.AlliriasTomb2, 10, 5);          // Bit 37 (check)
        public MapXY AT2_0004_Teleport = new MapXY(BT3Map.AlliriasTomb2, 0, 4);           // Bit 34 (check)
        public MapXY AT2_0005_BlueRose = new MapXY(BT3Map.AlliriasTomb2, 0, 5);           // Bit 34 (set)
        public MapXY AT2_1004_RainbowRose = new MapXY(BT3Map.AlliriasTomb2, 10, 4);       // Bit 37 (check), Bit 37 (set)
        public MapXY AT2_0400_WhiteRose = new MapXY(BT3Map.AlliriasTomb2, 4, 0);          // Bit 33 (check), Bit 33 (set)
        public MapXY AT2_0500_Teleport = new MapXY(BT3Map.AlliriasTomb2, 5, 0);           // Bit 33 (check)
        public MapXY AT2_1000_YellowRose = new MapXY(BT3Map.AlliriasTomb2, 10, 0);        // Bit 36 (check), Bit 36 (set)
        public MapXY AT2_1100_Teleport = new MapXY(BT3Map.AlliriasTomb2, 11, 0);          // Bit 36 (check)
        public MapXY T_0001_Shield = new MapXY(BT3Map.Tarmitia, 0, 1);                    // Bit 68 (check), Bit 69 (check), Bit 68 (set), Bit 69 (set)
        public MapXY T_0000_Werra = new MapXY(BT3Map.Tarmitia, 0, 0);                     // Bit 68 (check), Bit 68 (set)
        public MapXY M1_0316_Belt = new MapXY(BT3Map.Malefia1, 3, 16);                    // Bit 74 (check), Bit 74 (set)
        public MapXY M1_1002_Strifespear = new MapXY(BT3Map.Malefia1, 10, 2);             // Bit 41 (check), Bit 41 (set)
        public MapXY M2_1816_Bow = new MapXY(BT3Map.Malefia2, 18, 16);                    // Bit 76 (check), Bit 76 (set)
        public MapXY M2_0912_Sphere = new MapXY(BT3Map.Malefia2, 9, 12);                  // Bit 73 (check), Bit 73 (set)
        public MapXY M3_1520_Cloak = new MapXY(BT3Map.Malefia3, 15, 20);                  // Bit 75 (check), Bit 75 (set)
        public MapXY M3_1107_Door = new MapXY(BT3Map.Malefia3, 11, 7);                    // Bit 72 (check), Bit 73 (check), Bit 74 (check), Bit 75 (check), Bit 76 (check), Bit 77 (check)
        public MapXY M3_1205_Shield = new MapXY(BT3Map.Malefia3, 12, 5);                  // Bit 77 (check), Bit 77 (set)
        public MapXY M3_2102_Helm = new MapXY(BT3Map.Malefia3, 21, 2);                    // Bit 72 (check), Bit 72 (set)
        public MapXY T_0302_Tarjan = new MapXY(BT3Map.Tarjan, 3, 2);                      // Bit 79 (set), Bit 78 (set)
        public MapXY B_1102_RightKey = new MapXY(BT3Map.Barracks, 11, 2);                 // Bit 47 (check), Bit 47 (set)
        public MapXY F_0302_Hawkslayer = new MapXY(BT3Map.Ferofists, 3, 2);               // Bit 44 (check), Bit 43 (check), Bit 44 (set), Bit 43 (set)
        public MapXY PQ_0215_Ferofist = new MapXY(BT3Map.PrivateQuarter, 2, 15);          // Bit 45 (check), Bit 45 (set)
        public MapXY PQ_0811_LeftKey = new MapXY(BT3Map.PrivateQuarter, 8, 11);           // Bit 46 (check), Bit 46 (set)
        public MapXY W_0202_Portal = new MapXY(BT3Map.Workshop, 2, 2);                    // Bit 58 (check), Bit 59 (check), Bit 58 (set), Bit 59 (set), Bit 58 (reset), Bit 59 (reset)
        public MapXY S_0607_Urmech = new MapXY(BT3Map.Sanctum, 6, 7);                     // Bit 64 (check), Bit 65 (check), Bit 64 (set), Bit 65 (set)
        public MapXY S_0703_Chest = new MapXY(BT3Map.Sanctum, 7, 3);                      // Bit 66 (check), Bit 67 (check), Bit 66 (set), Bit 67 (set)
        public MapXY S_0602_Geomancer = new MapXY(BT3Map.Sanctum, 6, 2);                  // Bit 65 (check)
        public MapXY N_0505_MagicDoor = new MapXY(BT3Map.Nowhere, 5, 5);                  // Bit 57 (check), Bit 56 (check), Bit 57 (set), Bit 56 (set)
        public MapXY DC_0505_ShadowDoor = new MapXY(BT3Map.DarkCopse, 5, 5);              // Bit 55 (check), Bit 55 (set)
        public MapXY SC_0307_ShadowLock = new MapXY(BT3Map.ShadowCanyon, 3, 7);           // Bit 54 (check), Bit 54 (set)
        public MapXY SD2_0513_Sceadu = new MapXY(BT3Map.SceadusDemesne2, 5, 13);          // Bit 83 (check), Bit 84 (check), Bit 85 (check), Bit 83 (set), Bit 84 (set), Bit 85 (set)

        public BT3Locations()
        {
        }
    }
}