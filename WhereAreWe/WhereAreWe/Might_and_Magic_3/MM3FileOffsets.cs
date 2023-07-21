using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class MM3FileOffsets : FileOffsets
    {
        public const int CartographyOffset = 0x320;

        private static uint[] m_Scripts = new uint[] { 0,  // Map 0
            0x1b699, 0x1bf9d, 0x1c5e8, 0x1c9f7, 0x1d077, 0x1d4a1, 0x1da0d, 0x1dfad, 0x1e899, 0x1ed09,   // Maps 1-10
            0x1f836, 0x20856, 0x215fd, 0x21cd2, 0x2250d, 0x22ce2, 0x2329b, 0x23a2c, 0x243a6, 0x24a50,   // Maps 11-20
            0x2507b, 0x25a8d, 0x260bb, 0x263c4, 0x26dca, 0x27905, 0x283d8, 0x2883c, 0x28e82, 0x291db,   // Maps 21-30
            0x29545, 0x2996d, 0x29ed9, 0x2a2ba, 0x2a554, 0x2a908, 0x2ab83, 0x2ad60, 0x2b110, 0x2b55b,   // Maps 31-40
            0x2b7d9, 0x2bf82, 0x2c605, 0x2cc85, 0x2d1f6, 0x2d996, 0x2dfad, 0x2e6e8, 0x2e891, 0x2ed18,   // Maps 41-50
            0x2f00c, 0x2f575, 0x2f913, 0x2fb1d, 0x2ff4b, 0x3060b, 0x30a36, 0x30d1e, 0x31517, 0x3188e,   // Maps 51-60
            0x31da0, 0x32047, 0x323ed, 0x32659, 0, 0, 0, 0, 0, 0,                                       // Maps 61-70
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,                                                               // Maps 71-80
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,                                                               // Maps 81-90
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,                                                               // Maps 91-100
            0, 0, 0, 0, 0x1f10f, 0x1f819, 0, 0, 0, 0                                                    // Maps 101-110
        };

        public static uint[] StaticMaps = new uint[] {
            0, 0, 0, 0,                          // Map 0
            0x06159, 0, 0, 0,                    // Map 1  
            0x06499, 0, 0, 0,                    // Map 2  
            0x067d9, 0, 0, 0,                    // Map 3  
            0x06b19, 0, 0, 0,                    // Map 4  
            0x06e59, 0, 0, 0,                    // Map 5  
            0x07199, 0, 0, 0,                    // Map 6  
            0x074d9, 0, 0, 0,                    // Map 7  
            0x07819, 0, 0, 0,                    // Map 8  
            0x07b59, 0, 0, 0,                    // Map 9  
            0x07e99, 0, 0, 0,                    // Map 10 
            0x09559, 0x08519, 0x08859, 0x08b99,  // Map 11 
            0x09899, 0x144d9, 0x14819, 0x15b59,  // Map 12 
            0x09bd9, 0x14e99, 0x151d9, 0x15519,  // Map 13 
            0x09f19, 0x15859, 0x15b99, 0x15ed9,  // Map 14 
            0x0a259, 0x16219, 0x16559, 0x16899,  // Map 15 
            0x0a599, 0x16bd9, 0x16f19, 0x17259,  // Map 16 
            0x0a8d9, 0x17599, 0x178d9, 0x17c19,  // Map 17 
            0x0ac19, 0x17f59, 0x18299, 0x185d9,  // Map 18 
            0x0af59, 0x18919, 0x18c59, 0x18f99,  // Map 19 
            0x0b299, 0x192d9, 0x19619, 0x19959,  // Map 20 
            0x0b5d9, 0x19c99, 0x19fd9, 0x1a319,  // Map 21 
            0x0b919, 0x1a659, 0x1a999, 0x1acd9,  // Map 22 
            0x0bc59, 0x1b019, 0x1b359, 0x081d9,  // Map 23 
            0x0bf99, 0, 0, 0,                    // Map 24 
            0x0c2d9, 0, 0, 0,                    // Map 25 
            0x0c619, 0, 0, 0,                    // Map 26 
            0x0c959, 0, 0, 0,                    // Map 27 
            0x0cc99, 0, 0, 0,                    // Map 28 
            0x0cfd9, 0, 0, 0,                    // Map 29 
            0x0d319, 0, 0, 0,                    // Map 30 
            0x0d659, 0, 0, 0,                    // Map 31 
            0x0d999, 0, 0, 0,                    // Map 32 
            0x0dcd9, 0, 0, 0,                    // Map 33 
            0x0e019, 0, 0, 0,                    // Map 34 
            0x0e359, 0, 0, 0,                    // Map 35 
            0x0e699, 0, 0, 0,                    // Map 36 
            0x0e9d9, 0, 0, 0,                    // Map 37 
            0x0ed19, 0, 0, 0,                    // Map 38 
            0x0f059, 0, 0, 0,                    // Map 39 
            0x0f399, 0, 0, 0,                    // Map 40 
            0x0f6d9, 0, 0, 0,                    // Map 41 
            0x0fa19, 0, 0, 0,                    // Map 42 
            0x0fd59, 0, 0, 0,                    // Map 43 
            0x10099, 0, 0, 0,                    // Map 44 
            0x103d9, 0, 0, 0,                    // Map 45 
            0x10719, 0, 0, 0,                    // Map 46 
            0x10a59, 0, 0, 0,                    // Map 47 
            0x10d99, 0, 0, 0,                    // Map 48 
            0x110d9, 0, 0, 0,                    // Map 49 
            0x11419, 0, 0, 0,                    // Map 50 
            0x11759, 0, 0, 0,                    // Map 51 
            0x11a99, 0, 0, 0,                    // Map 52 
            0x11dd9, 0, 0, 0,                    // Map 53 
            0x12119, 0, 0, 0,                    // Map 54 
            0x12459, 0, 0, 0,                    // Map 55 
            0x12799, 0, 0, 0,                    // Map 56 
            0x12ad9, 0, 0, 0,                    // Map 57 
            0x12e19, 0, 0, 0,                    // Map 58 
            0x13159, 0, 0, 0,                    // Map 59 
            0x13499, 0, 0, 0,                    // Map 60 
            0x137d9, 0, 0, 0,                    // Map 61 
            0x13b19, 0, 0, 0,                    // Map 62 
            0x13e59, 0, 0, 0,                    // Map 63 
            0x14199, 0, 0, 0,                    // Map 64 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 65-69
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 70-74
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 75-79
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 80-84
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 85-89
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 90-94
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  // Maps 95-99
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,              // Maps 100-104
            0x08ed9, 0, 0, 0,                    // Map 105
            0x09219, 0, 0, 0                     // Map 106
        };

        private uint[] m_Monsters = new uint[] { };

        public override uint[] Maps { get { return StaticMaps; } }
        public override uint[] Scripts { get { return m_Scripts; } }
        public override uint[] Monsters { get { return m_Monsters; } }

        public const int OrbHallsOfInsanity0318 = 261; // 20=Available
        public const int OrbHallsOfInsanity2803 = 205; // 20=Available
        public const int OrbCathedralOfCarnage2517 = 186; // 20=Available
        public const int OrbCathedralOfCarnage2515 = 121; // 20=Available
        public const int OrbDarkWarriorKeep3002 = 1481;  // 25=Available
        public const int OrbDarkWarriorKeep3001 = 1496;  // 25=Available
        public const int OrbDragonCavern2105 = 111; // 25=Available
        public const int OrbDragonCavern2705 = 126; // 25=Available
        public const int OrbDragonCavern1303 = 81; // 25=Available
        public const int OrbDragonCavern0201 = 96; // 25=Available
        public const int OrbTombOfTerror1206 = 160; // 20=Available
        public const int OrbTombOfTerror1202 = 126; // 20=Available
        public const int OrbTheMazeFromHell3031 = 202; // 25=Available
        public const int OrbTheMazeFromHell0130 = 184; // 25=Available
        public const int OrbTheMazeFromHell1919 = 193; // 25=Available
        public const int OrbTheMazeFromHell0101 = 175; // 25=Available
        public const int OrbAftStorageSector0114 = 75; // 25=Available
        public const int OrbAftStorageSector0112 = 60; // 25=Available
        public const int OrbAftStorageSector1408 = 45; // 25=Available
        public const int OrbBetaEngineSector0115 = 90; // 25=Available
        public const int OrbBetaEngineSector0107 = 75; // 25=Available
        public const int OrbBetaEngineSector1407 = 60; // 25=Available
        public const int OrbBetaEngineSector1001 = 45; // 25=Available
        public const int OrbMainEngineSector0108 = 127; // 25=Available
        public const int OrbMainEngineSector0808 = 82; // 25=Available
        public const int OrbMainEngineSector1108 = 97; // 25=Available
        public const int OrbMainEngineSector1408 = 112; // 25=Available
        public const int OrbAlphaEngineSector0014 = 97; // 25=Available
        public const int OrbAlphaEngineSector1509 = 82; // 25=Available
        public const int OrbAlphaEngineSector0004 = 67; // 25=Available
        public const int OrbAlphaEngineSector1501 = 52; // 25=Available
        public const int SkullA1AncientTempleOfMoo2715 = 783; // 20=Available
        public const int SkullA1AncientTempleOfMoo1707 = 729; // 20=Available
        public const int SkullA1AncientTempleOfMoo2706 = 756; // 20=Available
        public const int SkullA1AncientTempleOfMoo0704 = 702; // 20=Available
        public const int SkullB1CyclopsCavern1208 = 2881; // 20=Available
        public const int SkullB1CyclopsCavern1108 = 2801; // 20=Available
        public const int SkullA1FountainHeadCavern0014 = 962; // 20=Available
        public const int SkullA1FountainHeadCavern0613 = 342; // 20=Available
        public const int SkullA1FountainHeadCavern1511 = 448; // 20=Available
        public const int SkullA1FountainHeadCavern1410 = 494; // 20=Available
        public const int SkullA1FountainHeadCavern0706 = 672; // 20=Available
        public const int SkullA1FountainHeadCavern0505 = 810; // 20=Available
        public const int SkullA1FountainHeadCavern0703 = 856; // 20=Available
        public const int SkullA1FountainHeadCavern1503 = 718; // 20=Available
        public const int SkullA1FountainHeadCavern0102 = 989; // 20=Available
        public const int SkullA1FountainHeadCavern1401 = 764; // 20=Available
        public const int SkullA2BaywatchCavern0014 = 636; // 20=Available
        public const int SkullA2BaywatchCavern0012 = 602; // 20=Available
        public const int SkullA2BaywatchCavern0505 = 466; // 20=Available
        public const int SkullA2BaywatchCavern0705 = 432; // 20=Available
        public const int HologramSequencingCard001 = 237; // 20=Available
        public const int HologramSequencingCard002 = 1056; // 20=Available
        public const int HologramSequencingCard003 = 1189; // 20=Available
        // Card004 is party bit 46
        public const int HologramSequencingCard005 = 880; // 20=Available
        public const int HologramSequencingCard006 = 491; // 20=Available
        public const int YellowFortressKey = 3016; // 20=Available
        public const int GreenEyeballKey = 2481; // 20=Available
        public const int BlackTerrorKey = 195; // 8=Available
        public const int BlueUnholyKey = 3251; // 20=Available
        public const int GoldMasterKey = 811; // 20=Available
        public const int RedWarriorsKey = 2641; // 20=Available
        public const int GoldenPyramidKeyCard = 70; // 8=Available
        public const int InsectShrine = 728; // 20=Available
        public const int EvilArtifactA2CastleWhiteshield0006 = 1366;
        public const int EvilArtifactA2CastleWhiteshield0010 = 1390;
        public const int EvilArtifactA3HallsOfInsanity1026 = 1093;
        public const int EvilArtifactA3HallsOfInsanity1626 = 1179;
        public const int EvilArtifactB4CastleBloodReign0800 = 458; // 8
        public const int EvilArtifactD1CursedColdCavern1214 = 1263;
        public const int GoodArtifactD1CursedColdCavern1215 = 1239;
        public const int EvilArtifactD3BlisteringHeights0102 = 752;
        public const int EvilArtifactE2SwampTown0901 = 958; // 8
        public const int EvilArtifactE2SwampTown1401 = 1099; // 8
        public const int EvilArtifactE2SwampTownCavern0115 = 809;
        public const int EvilArtifactE2SwampTownCavern0415 = 603;
        public const int EvilArtifactF2TombOfTerror0802 = 770;
        public const int EvilArtifactF2TombOfTerror0806 = 721;
        public const int GoodArtifactB1CyclopsCavern0202 = 3118;
        public const int GoodArtifactB1CyclopsCavern2901 = 3416;
        public const int GoodArtifactB2FortressOfFear0214 = 258;
        public const int GoodArtifactB2FortressOfFear2830 = 282;
        public const int GoodArtifactB4CastleBloodReign0400 = 210; // 8
        public const int GoodArtifactB4CastleBloodReign0600 = 334; // 8
        public const int EvilArtifactB4CastleBloodReign1000 = 582; // 8
        public const int GoodArtifactD1CursedColdCavern2424 = 1191;
        public const int GoodArtifactD1CursedColdCavern2722 = 1215;
        public const int GoodArtifactD3BlisteringHeights0201 = 721;
        public const int GoodArtifactF2TombOfTerror0509 = 672;
        public const int GoodArtifactF2TombOfTerror1109 = 623;
        public const int NeutArtifactB1CyclopsCavern0402 = 3148;
        public const int NeutArtifactB1CyclopsCavern2500 = 3446;
        public const int NeutArtifactB1SlithercultStronghold2828 = 1049; // 8
        public const int NeutArtifactB2FortressOfFear0501 = 306;
        public const int NeutArtifactB3DarkWarriorKeep1501 = 1141;
        public const int NeutArtifactB3DarkWarriorKeep1601 = 1165;
        public const int NeutArtifactD1CursedColdCavern1200 = 1287;
        public const int NeutArtifactD1CursedColdCavern2005 = 1311;
        public const int NeutArtifactD3BlisteringHeights0105 = 783;
        public const int NeutArtifactE1CastleDragontooth1212 = 858; // 8
        public const int NeutArtifactE1CastleDragontooth1515 = 1106; // 8
        public const int PearlB1SlithercultStronghold0227 = 979; // 8
        public const int PearlB2FortressOfFear2401 = 182;
        public const int PearlD1CursedColdCavern0928 = 370; // 8
        public const int PearlD1CursedColdCavern2219 = 479; // 8
        public const int PearlD3BlisteringHeightsCavern0713 = 308;
        public const int PearlD3BlisteringHeightsCavern0913 = 351;
        public const int AcceptGreywindQuest = 375;

        public const int GreywindWalls0114 = 450;

        public const int OrcOutpostA1Surface0503 = 946;
        public const int GoblinWagonA1Surface1207 = 1654;
        public const int OrcishShrineA2Surface0404 = 775;
        public const int OrcOutpostA2Surface0407 = 281;
        public const int GoblinHutA2Surface0805 = 401;
        public const int ScreamerWagonA3Surface0309 = 262;
        public const int VampireBatWagonA3Surface1504 = 909;
        public const int SpidersNestA4Surface0512 = 519;
        public const int MagicMantisPodsA4Surface1504 = 419;
        public const int LampreyB3Surface1210 = 1134;
        public const int BugabooLarvaeB3Surface0303 = 1239;
        public const int OgreMeetingHallB2Surface1104 = 217;
        public const int SpriteHutB2Surface0610 = 488;
        public const int WildFungusSporesB1Surface0503 = 920;
        public const int OhNoBugApiaryB1Surface1208 = 845;
        public const int CyclopsShackC1Surface0612 = 252;
        public const int SpriteHutC1Surface0604 = 391;
        public const int WerewolfShrineC1Surface1409 = 204;
        public const int MajorDevilPortalC2Surface1102 = 167;
        public const int HydraBreedingGroundsC3Surface0709 = 96;
        public const int MajorDemonHutD3Surface0608 = 351;
        public const int FireLizardHatcheryD2Surface01001 = 80;
        public const int FireStalkerLairD2Surface0510 = 165;
        public const int DeathLocustNestE2Surface0311 = 1224;
        public const int RogueMeetingPlaceE2Surface0611 = 759;
        public const int BarbarianCompoundE4Surface0507 = 353;
        public const int DeathLocustNestE4Surface0908 = 257;

        public const int WellOfRemembrance = 397;
        public const int LordMight = 1614;

        public const int JewelryA2BaywatchCavern0611 = 670;
        public const int JewelryA2BaywatchCavern0811 = 704;
        public const int JewelryA2BaywatchCavern0703 = 262;
        public const int JewelryA2BaywatchCavern1403 = 296;
        public const int JewelryE2SwampTown0112 = 408;
        public const int JewelryE2SwampTown1404 = 1146;
        public const int JewelryE2SwampTown1101 = 1005;
        public const int JewelryE2SwampTown1201 = 1052;
        public const int JewelryA1AncientTempleOfMoo2627 = 810;
        public const int JewelryA1AncientTempleOfMoo1424 = 837;
        public const int JewelryB1CyclopsCavern2827 = 2561;
        public const int JewelryB1CyclopsCavern0916 = 2721;

        public const int L6Items1E1CD1215 = 728; // MM3Map.E1CastleDragontooth,12,15
        public const int L6Items2E1CD1307 = 1788; // MM3Map.E1CastleDragontooth,13,7
        public const int L6Items3D1CCC1503 = 824; // MM3Map.D1CursedColdCavern,15,3
        public const int L6Items4F1DC2204 = 1737; // MM3Map.F1DragonCavern,22,4
        public const int L6Items5F1DC2504 = 1752; // MM3Map.F1DragonCavern,25,4
        public const int L6Items6F1DC0302 = 1707; // MM3Map.F1DragonCavern,3,2
        public const int L6Items7F1DC1201 = 1722; // MM3Map.F1DragonCavern,12,1
        public const int L6Items8F1DC2301 = 1776; // MM3Map.F1DragonCavern,23,1
        public const int L6Items9F2TT1806 = 819; // MM3Map.F2TombOfTerror,18,6
        public const int L6Items10F2TT3004 = 929; // MM3Map.F2TombOfTerror,30,4
        public const int L6Items11F3TMFH0725 = 437; // MM3Map.F3TheMazeFromHell,7,25
        public const int L6Items12F3TMFH0815 = 452; // MM3Map.F3TheMazeFromHell,8,15
        public const int L6Items13F3TMFH1501 = 467; // MM3Map.F3TheMazeFromHell,15,1
        public const int L6Items14C2CCS1113 = 422; // MM3Map.C2CentralControlSector,11,13
        public const int L6Items15C2CCS0112 = 678; // MM3Map.C2CentralControlSector,1,12
        public const int L6Items16B3S1111 = 787; // MM3Map.B3Surface,11,11
        public const int L6Items17B4S0814 = 316; // MM3Map.B4Surface,8,14
        public const int L6Items18B4S1011 = 289; // MM3Map.B4Surface,10,11
        public const int L6Items19B4S1209 = 262; // MM3Map.B4Surface,12,9
        public const int L6Items20C2S0213 = 327; // MM3Map.C2Surface,2,13
        public const int L6Items21C2S1212 = 407; // MM3Map.C2Surface,12,12
        public const int L6Items22C2S0406 = 247; // MM3Map.C2Surface,4,6
        public const int L6Items23C3S0213 = 293; // MM3Map.C3Surface,2,13
        public const int L6Items24C3S0709 = 109; // MM3Map.C3Surface,7,9
        public const int L6Items25C3S0204 = 248; // MM3Map.C3Surface,2,4
        public const int L6Items26C3S1102 = 203; // MM3Map.C3Surface,11,2
        public const int L6Items27D3S1407 = 446; // MM3Map.D3Surface,14,7
        public const int L6Items28D3S0104 = 516; // MM3Map.D3Surface,1,4
        public const int L6Items29D3S1203 = 481; // MM3Map.D3Surface,12,3
        public const int L6Items30E2S0808 = 897; // MM3Map.E2Surface,8,8
        public const int L6Items31E3S1011 = 575; // MM3Map.E3Surface,10,11
        public const int L6Items32E3S0609 = 606; // MM3Map.E3Surface,6,9
        public const int L6Items33E3S0905 = 637; // MM3Map.E3Surface,9,5
        public const int L6Items34E4S0412 = 653; // MM3Map.E4Surface,4,12
        public const int L6Items35E4S1011 = 455; // MM3Map.E4Surface,10,11
        public const int L6Items36E4S0806 = 587; // MM3Map.E4Surface,8,6
        public const int L6Items37E4S1306 = 517; // MM3Map.E4Surface,13,6
        public const int L6Items38F2S1102 = 352; // MM3Map.F2Surface,11,2
        public const int L6Items39F4S0710 = 473; // MM3Map.F4Surface,7,10
        public const int L6Items40F4S1309 = 605; // MM3Map.F4Surface,13,9
        public const int L6Items41F4S0206 = 535; // MM3Map.F4Surface,2,6

        public const int PermRes20A2WD1107 = 714; // MM3Map.A2WhiteshieldDungeon,11,7
        public const int PermStats10AA2WD0903 = 540; // MM3Map.A2WhiteshieldDungeon,9,3
        public const int PermStats10BA2WD0901 = 627; // MM3Map.A2WhiteshieldDungeon,9,1
        public const int PermLevel5AA2WD1501 = 789; // MM3Map.A2WhiteshieldDungeon,15,1
        public const int PermAcy50AB4BRD0615 = 782; // MM3Map.B4BloodreignDungeon,6,15
        public const int PermSpd50B4BRD1515 = 845; // MM3Map.B4BloodreignDungeon,15,15
        public const int PermLck50B4BRD0005 = 719; // MM3Map.B4BloodreignDungeon,0,5
        // Day 50 throne - PermStats10C; 
        public const int PermLevel5BE1DD0508 = 265; // MM3Map.E1DragontoothDungeon,5,8
        public const int PermInt50AE1DD0708 = 419; // MM3Map.E1DragontoothDungeon,7,8
        public const int PermEnd50AE1DD0507 = 342; // MM3Map.E1DragontoothDungeon,5,7
        public const int PermPer50AE1DD0707 = 496; // MM3Map.E1DragontoothDungeon,7,7
        public const int PermMagRes30B1CC1030 = 853; // MM3Map.B1CyclopsCavern,10,30
        public const int PermElecRes20AB1CC2729 = 3942; // MM3Map.B1CyclopsCavern,27,29
        public const int PermEnergyRes30B1CC0227 = 978; // MM3Map.B1CyclopsCavern,2,27
        public const int PermElecRes20BB1CC0225 = 305; // MM3Map.B1CyclopsCavern,2,25
        public const int PermPer50BB1CC0312 = 1205; // MM3Map.B1CyclopsCavern,3,12
        public const int PermAcy50BB1CC2910 = 1455; // MM3Map.B1CyclopsCavern,29,10
        public const int PermInt50BB1CC2308 = 1330; // MM3Map.B1CyclopsCavern,23,8
        public const int PermEnd50BB1CC2803 = 1580; // MM3Map.B1CyclopsCavern,28,3
        // Quatloo - PermMight5A
        // Quatloo - PermEnd5A
        // Quatloo - PermAcy5A
        public const int PermLevel2B1SS0208 = 434; // MM3Map.B1SlithercultStronghold,2,8
        public const int PermPoisonRes25AQB1SS2207 = 515; // MM3Map.B1SlithercultStronghold,22,7
        public const int PermPoisonRes25BAB1SS2204 = 601; // MM3Map.B1SlithercultStronghold,22,4
        public const int PermLevel5CBB3CC1311 = 1874; // MM3Map.B3CathedralOfCarnage,13,11
        public const int PermLevel5DCB3CC1303 = 1889; // MM3Map.B3CathedralOfCarnage,13,3
        public const int PermMight10ADB3DWK2330 = 711; // MM3Map.B3DarkWarriorKeep,23,30
        public const int PermMight10BAB3DWK0524 = 588; // MM3Map.B3DarkWarriorKeep,5,24
        public const int PermEnd20ABB3DWK3022 = 752; // MM3Map.B3DarkWarriorKeep,30,22
        public const int PermSpeed20AAB3DWK3011 = 793; // MM3Map.B3DarkWarriorKeep,30,11
        public const int PermEnd10AAB3DWK0110 = 629; // MM3Map.B3DarkWarriorKeep,1,10
        public const int PermMight25AB3DWK0101 = 670; // MM3Map.B3DarkWarriorKeep,1,1
        public const int PermLevel2BB3DWK1201 = 834; // MM3Map.B3DarkWarriorKeep,12,1
        // bit 28 - PermLevel2B - PermInt5A
        // bit 29 - PermInt5A - PermPer5A
        // bit 27 - PermPer5A - PermPer5B
        // bit 34 - PermPer5B - PermAcy10A
        // bit 35 - PermAcy10A - PermLuck10A
        // bit 30 - PermLuck10A - PermInt5B
        // bit 33 - PermInt5B - PermPer10
        // bit 36 - PermPer10 - PermLuck10B
        // bit 32 - PermLuck10B - PermInt5C
        // bit 31 - PermInt5C - PermAcy10B
        // bit 26 - PermAcy10B - PermInt5D
        // bit 24 - PermInt5D - PermPer5C
        // bit 25 - PermPer5C - PermInt5E
        public const int PermMagicRes20AED1CCC0729 = 245; // MM3Map.D1CursedColdCavern,7,29
        public const int PermMagicRes20BAD1CCC2221 = 420; // MM3Map.D1CursedColdCavern,22,21
        public const int PermLevel1ABD1CCC1415 = 529; // MM3Map.D1CursedColdCavern,14,15
        public const int PermLevel1BAD1CCC1615 = 588; // MM3Map.D1CursedColdCavern,16,15
        public const int PermLevel1CBD1CCC1413 = 647; // MM3Map.D1CursedColdCavern,14,13
        public const int PermLevel1DCD1CCC1613 = 706; // MM3Map.D1CursedColdCavern,16,13
        public const int PermMagicRes20CDD1CCC1202 = 765; // MM3Map.D1CursedColdCavern,12,2
        public const int PermInt10ACE4TMC0228 = 1206; // MM3Map.E4TheMagicCavern,2,28
        public const int PermSpeed10AAE4TMC0927 = 1588; // MM3Map.E4TheMagicCavern,9,27
        public const int PermInt10BAE4TMC2827 = 1294; // MM3Map.E4TheMagicCavern,28,27
        public const int PermInt10CBE4TMC2224 = 1250; // MM3Map.E4TheMagicCavern,22,24
        public const int PermSpeed10BCE4TMC0219 = 1544; // MM3Map.E4TheMagicCavern,2,19
        public const int PermInt10DBE4TMC0919 = 1162; // MM3Map.E4TheMagicCavern,9,19
        public const int PermSpeed10CDE4TMC0817 = 1500; // MM3Map.E4TheMagicCavern,8,17
        public const int PermSpeed10DCE4TMC2013 = 1456; // MM3Map.E4TheMagicCavern,20,13
        public const int PermSpeed10EDE4TMC2005 = 1412; // MM3Map.E4TheMagicCavern,20,5
        public const int PermInt10EEE4TMC0502 = 1076; // MM3Map.E4TheMagicCavern,5,2
        public const int PermLevel3Stats20AEF2TT2806 = 1035; // MM3Map.F2TombOfTerror,28,6
        public const int PermLevel3Stats20BAF2TT2906 = 1204; // MM3Map.F2TombOfTerror,29,6
        public const int PermLevel3Stats20CBF2TT2802 = 1285; // MM3Map.F2TombOfTerror,28,2
        public const int PermLevel3Stats20DCF2TT2902 = 1364; // MM3Map.F2TombOfTerror,29,2
        public const int PermMight20ADA2FSS1510 = 389; // MM3Map.A2ForwardStorageSector,15,10
        public const int PermInt20AA2FSS1508 = 461; // MM3Map.A2ForwardStorageSector,15,8
        public const int PermPer20A2FSS1506 = 533; // MM3Map.A2ForwardStorageSector,15,6
        public const int PermLuck20A2FSS1404 = 821; // MM3Map.A2ForwardStorageSector,14,4
        public const int PermEnd20BA2FSS0503 = 605; // MM3Map.A2ForwardStorageSector,5,3
        public const int PermLevel2CBA2FSS1403 = 893; // MM3Map.A2ForwardStorageSector,14,3
        public const int PermAcy20CA2FSS0502 = 677; // MM3Map.A2ForwardStorageSector,5,2
        public const int PermSpeed20BA2FSS0501 = 749; // MM3Map.A2ForwardStorageSector,5,1
        // Knight, End/Might < 25 - PermMight10End10
        // Accuracy < 25 - PermAcy1
        // Endurance < 50 - PermEnd1
        // Druid, Stats < 25 - PermStats10
        // no limit, wraps around to 0! - PermPoisonRes2
        // Might < 255 - PermMight2
        public const int PermEnd5BA1FHC0014 = 962; // MM3Map.A1FountainHeadCavern,0,14
        public const int PermMight5BBA1FHC0613 = 342; // MM3Map.A1FountainHeadCavern,6,13
        public const int PermInt5FBA1FHC1511 = 448; // MM3Map.A1FountainHeadCavern,15,11
        public const int PermPer5DFA1FHC1410 = 494; // MM3Map.A1FountainHeadCavern,14,10
        public const int PermAcy5BDA1FHC0706 = 672; // MM3Map.A1FountainHeadCavern,7,6
        public const int PermEnd5CBA1FHC0505 = 810; // MM3Map.A1FountainHeadCavern,5,5
        public const int PermEnd5DCA1FHC0703 = 856; // MM3Map.A1FountainHeadCavern,7,3
        public const int PermSpeed5DA1FHC1503 = 718; // MM3Map.A1FountainHeadCavern,15,3
        public const int PermLuck5AA1FHC0102 = 989; // MM3Map.A1FountainHeadCavern,1,2
        public const int PermEnd5EAA1FHC1401 = 764; // MM3Map.A1FountainHeadCavern,14,1
        public const int PermEnd10BEB4WC0515 = 754; // MM3Map.B4WildabarCavern,5,15
        public const int PermSpeed10BB4WC1115 = 795; // MM3Map.B4WildabarCavern,11,15
        public const int PermAcy5CB4WC1007 = 590; // MM3Map.B4WildabarCavern,10,7
        public const int PermPer5ECB4WC1207 = 549; // MM3Map.B4WildabarCavern,12,7
        public const int PermMight10CEB4WC0105 = 247; // MM3Map.B4WildabarCavern,1,5
        public const int PermLuck5BCB4WC1005 = 672; // MM3Map.B4WildabarCavern,10,5
        public const int PermInt5GBB4WC0903 = 303; // MM3Map.B4WildabarCavern,9,3
        public const int PermEnd5FGB4WC1503 = 508; // MM3Map.B4WildabarCavern,15,3
        // Might < 50 - PermMight20BPermMight20B
        // Endurance < 50 - PermEnd25PermEnd25
        // Fire Res < 25 - PermFireRes25PermFireRes25
        // Elec Res < 25 - PermElecRes25PermElecRes25
        // Cold Res < 25 - PermColdRes25PermColdRes25
        // Poison Res < 30 - PermPoisonRes30PermPoisonRes30
        // Energy Res < 20 - PermEnergyRes20PermEnergyRes20
        // Magic Res < 25 - PermMagicRes25PermMagicRes25

        public const int SonOfAbuB4BRD1403 = 366; // MM3Map.B4BloodreignDungeon,14,3
        public const int CharityB4BRD1400 = 438; // MM3Map.B4BloodreignDungeon,14,0
        public const int DarlanaA2BC1501 = 1102; // MM3Map.A2BaywatchCavern,15,1
        public const int SirGalantA2BC1500 = 1163; // MM3Map.A2BaywatchCavern,15,0
        public const int LoneWolfB4WC0013 = 1067; // MM3Map.B4WildabarCavern,0,13
        public const int WartowsanB4WC0407 = 1125; // MM3Map.B4WildabarCavern,4,7
    }

    public class MM3FileQuestInfo : FileQuestInfo
    {
        private byte[] m_bytes = new byte[(int)(ByteIndex.Last - ByteIndex.First)];

        public enum ByteIndex
        {
            First = 0,
            Orb1 = 0, Orb2, Orb3, Orb4, Orb5, Orb6, Orb7, Orb8, Orb9, Orb10, Orb11, Orb12, Orb13, Orb14, Orb15, Orb16, Orb17, Orb18, Orb19,
            Orb20, Orb21, Orb22, Orb23, Orb24, Orb25, Orb26, Orb27, Orb28, Orb29, Orb30, Orb31, Skull1, Skull2, Skull3, Skull4, Skull5, Skull6,
            Skull7, Skull8, Skull9, Skull10, Skull11, Skull12, Skull13, Skull14, Skull15, Skull16, Skull17, Skull18, Skull19, Skull20, Card1, Card2,
            Card3, Card5, Card6, YellowKey, GreenKey, BlackKey, BlueKey, GoldKey, RedKey, KeyCard, Insect, Evil1, Evil2, Evil3, Evil4, Evil5, Evil6,
            Evil7, Evil8, Evil9, Evil10, Evil11, Evil12, Evil13, Evil14, Evil15, Good1, Good2, Good3, Good4, Good5, Good6, Good7, Good8, Good9, Good10,
            Good11, Neut1, Neut2, Neut3, Neut4, Neut5, Neut6, Neut7, Neut8, Neut9, Neut10, Neut11, Pearl1, Pearl2, Pearl3, Pearl4, Pearl5,
            Pearl6, GreyAccept, GreyWall, Destroy1, Destroy2, Destroy3, Destroy4, Destroy5, Destroy6, Destroy7, Destroy8, Destroy9, Destroy10,
            Destroy11, Destroy12, Destroy13, Destroy14, Destroy15, Destroy16, Destroy17, Destroy18, Destroy19, Destroy20, Destroy21, Destroy22,
            Destroy23, Destroy24, Destroy25, Destroy26, Destroy27, Well, LordMight, Jewelry1, Jewelry2, Jewelry3, Jewelry4, Jewelry5, Jewelry6,
            Jewelry7, Jewelry8, Jewelry9, Jewelry10, Jewelry11, Jewelry12, L6Items1, L6Items2, L6Items3, L6Items4, L6Items5, L6Items6, L6Items7, L6Items8,
            L6Items9, L6Items10, L6Items11, L6Items12, L6Items13, L6Items14, L6Items15, L6Items16, L6Items17, L6Items18, L6Items19, L6Items20, L6Items21,
            L6Items22, L6Items23, L6Items24, L6Items25, L6Items26, L6Items27, L6Items28, L6Items29, L6Items30, L6Items31, L6Items32, L6Items33, L6Items34,
            L6Items35, L6Items36, L6Items37, L6Items38, L6Items39, L6Items40, L6Items41, Perm1, Perm2, Perm3, Perm4, Perm5, Perm6, Perm7, Perm8, Perm9,
            Perm10, Perm11, Perm12, Perm13, Perm14, Perm15, Perm16, Perm17, Perm18, Perm19, Perm20, Perm21, Perm22, Perm23, Perm24, Perm25, Perm26, Perm27,
            Perm28, Perm29, Perm30, Perm31, Perm32, Perm33, Perm34, Perm35, Perm36, Perm37, Perm38, Perm39, Perm40, Perm41, Perm42, Perm43, Perm44, Perm45,
            Perm46, Perm47, Perm48, Perm49, Perm50, Perm51, Perm52, Perm53, Perm54, Perm55, Perm56, Perm57, Perm58, Perm59, Perm60, Perm61, Perm62, Perm63,
            Perm64, Perm65, Perm66, Perm67, Perm68, Perm69, Perm70, Perm71, Perm72, Perm73, Perm74, Perm75, Perm76, Perm77, Perm78, Hireling1, Hireling2,
            Hireling3, Hireling4, Hireling5, Hireling6,
            Last
        };

        public MM3FileQuestInfo()
        {
            Valid = false;
        }

        public QuestGoal Goal(ByteIndex idx, byte compare = 0) { return Goal(m_bytes[(int)idx], compare); }

        private byte MapScriptByte(MM3Map map, uint offset, FileAndMemoryInfo info, bool bScript = true)
        {
            return MapScriptByte(new MM3FileOffsets(), (int)map, offset, info, bScript);
        }

        public void AddObjRange(QuestStatus status, ByteIndex start, ByteIndex end)
        {
            for (ByteIndex b = start; b <= end; b++)
                status.AddObj(Goal(b));
        }
        
        public void SetInfo(byte[] bytesFile, byte[] bytesMemoryScript, byte[] bytesMemoryMonsters, List<MemoryBytes> bytesMemoryMap, int iMemoryMap)
        {
            FileAndMemoryInfo info = new FileAndMemoryInfo(bytesFile, bytesMemoryScript, bytesMemoryMonsters, bytesMemoryMap, iMemoryMap);

            m_bytes[(int)ByteIndex.Orb1] = MapScriptByte(MM3Map.A3HallsOfInsanity, MM3FileOffsets.OrbHallsOfInsanity0318, info);
            m_bytes[(int)ByteIndex.Orb2] = MapScriptByte(MM3Map.A3HallsOfInsanity, MM3FileOffsets.OrbHallsOfInsanity2803, info);
            m_bytes[(int)ByteIndex.Orb3] = MapScriptByte(MM3Map.B3CathedralOfCarnage, MM3FileOffsets.OrbCathedralOfCarnage2517, info);
            m_bytes[(int)ByteIndex.Orb4] = MapScriptByte(MM3Map.B3CathedralOfCarnage, MM3FileOffsets.OrbCathedralOfCarnage2515, info);
            m_bytes[(int)ByteIndex.Orb5] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.OrbDarkWarriorKeep3002, info);
            m_bytes[(int)ByteIndex.Orb6] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.OrbDarkWarriorKeep3001, info);
            m_bytes[(int)ByteIndex.Orb7] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.OrbDragonCavern2105, info);
            m_bytes[(int)ByteIndex.Orb8] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.OrbDragonCavern2705, info);
            m_bytes[(int)ByteIndex.Orb9] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.OrbDragonCavern1303, info);
            m_bytes[(int)ByteIndex.Orb10] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.OrbDragonCavern0201, info);
            m_bytes[(int)ByteIndex.Orb11] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.OrbTombOfTerror1206, info);
            m_bytes[(int)ByteIndex.Orb12] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.OrbTombOfTerror1202, info);
            m_bytes[(int)ByteIndex.Orb13] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.OrbTheMazeFromHell3031, info);
            m_bytes[(int)ByteIndex.Orb14] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.OrbTheMazeFromHell0130, info);
            m_bytes[(int)ByteIndex.Orb15] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.OrbTheMazeFromHell1919, info);
            m_bytes[(int)ByteIndex.Orb16] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.OrbTheMazeFromHell0101, info);
            m_bytes[(int)ByteIndex.Orb17] = MapScriptByte(MM3Map.A2AftStorageSector, MM3FileOffsets.OrbAftStorageSector0114, info);
            m_bytes[(int)ByteIndex.Orb18] = MapScriptByte(MM3Map.A2AftStorageSector, MM3FileOffsets.OrbAftStorageSector0112, info);
            m_bytes[(int)ByteIndex.Orb19] = MapScriptByte(MM3Map.A2AftStorageSector, MM3FileOffsets.OrbAftStorageSector1408, info);
            m_bytes[(int)ByteIndex.Orb20] = MapScriptByte(MM3Map.F1BetaEngineSector, MM3FileOffsets.OrbBetaEngineSector0115, info);
            m_bytes[(int)ByteIndex.Orb21] = MapScriptByte(MM3Map.F1BetaEngineSector, MM3FileOffsets.OrbBetaEngineSector0107, info);
            m_bytes[(int)ByteIndex.Orb22] = MapScriptByte(MM3Map.F1BetaEngineSector, MM3FileOffsets.OrbBetaEngineSector1407, info);
            m_bytes[(int)ByteIndex.Orb23] = MapScriptByte(MM3Map.F1BetaEngineSector, MM3FileOffsets.OrbBetaEngineSector1001, info);
            m_bytes[(int)ByteIndex.Orb24] = MapScriptByte(MM3Map.F2MainEngineSector, MM3FileOffsets.OrbMainEngineSector0108, info);
            m_bytes[(int)ByteIndex.Orb25] = MapScriptByte(MM3Map.F2MainEngineSector, MM3FileOffsets.OrbMainEngineSector0808, info);
            m_bytes[(int)ByteIndex.Orb26] = MapScriptByte(MM3Map.F2MainEngineSector, MM3FileOffsets.OrbMainEngineSector1108, info);
            m_bytes[(int)ByteIndex.Orb27] = MapScriptByte(MM3Map.F2MainEngineSector, MM3FileOffsets.OrbMainEngineSector1408, info);
            m_bytes[(int)ByteIndex.Orb28] = MapScriptByte(MM3Map.F4AlphaEngineSector, MM3FileOffsets.OrbAlphaEngineSector0014, info);
            m_bytes[(int)ByteIndex.Orb29] = MapScriptByte(MM3Map.F4AlphaEngineSector, MM3FileOffsets.OrbAlphaEngineSector1509, info);
            m_bytes[(int)ByteIndex.Orb30] = MapScriptByte(MM3Map.F4AlphaEngineSector, MM3FileOffsets.OrbAlphaEngineSector0004, info);
            m_bytes[(int)ByteIndex.Orb31] = MapScriptByte(MM3Map.F4AlphaEngineSector, MM3FileOffsets.OrbAlphaEngineSector1501, info);
            m_bytes[(int)ByteIndex.Skull1] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.SkullA1AncientTempleOfMoo2715, info);
            m_bytes[(int)ByteIndex.Skull2] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.SkullA1AncientTempleOfMoo1707, info);
            m_bytes[(int)ByteIndex.Skull3] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.SkullA1AncientTempleOfMoo2706, info);
            m_bytes[(int)ByteIndex.Skull4] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.SkullA1AncientTempleOfMoo0704, info);
            m_bytes[(int)ByteIndex.Skull5] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.SkullB1CyclopsCavern1208, info);
            m_bytes[(int)ByteIndex.Skull6] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.SkullB1CyclopsCavern1108, info);
            m_bytes[(int)ByteIndex.Skull7] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0014, info);
            m_bytes[(int)ByteIndex.Skull8] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0613, info);
            m_bytes[(int)ByteIndex.Skull9] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern1511, info);
            m_bytes[(int)ByteIndex.Skull10] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern1410, info);
            m_bytes[(int)ByteIndex.Skull11] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0706, info);
            m_bytes[(int)ByteIndex.Skull12] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0505, info);
            m_bytes[(int)ByteIndex.Skull13] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0703, info);
            m_bytes[(int)ByteIndex.Skull14] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern1503, info);
            m_bytes[(int)ByteIndex.Skull15] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern0102, info);
            m_bytes[(int)ByteIndex.Skull16] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.SkullA1FountainHeadCavern1401, info);
            m_bytes[(int)ByteIndex.Skull17] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.SkullA2BaywatchCavern0014, info);
            m_bytes[(int)ByteIndex.Skull18] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.SkullA2BaywatchCavern0012, info);
            m_bytes[(int)ByteIndex.Skull19] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.SkullA2BaywatchCavern0505, info);
            m_bytes[(int)ByteIndex.Skull20] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.SkullA2BaywatchCavern0705, info);
            m_bytes[(int)ByteIndex.Card1] = MapScriptByte(MM3Map.B2FortressOfFear, MM3FileOffsets.HologramSequencingCard001, info);
            m_bytes[(int)ByteIndex.Card2] = MapScriptByte(MM3Map.A3HallsOfInsanity, MM3FileOffsets.HologramSequencingCard002, info);
            m_bytes[(int)ByteIndex.Card3] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.HologramSequencingCard003, info);
            m_bytes[(int)ByteIndex.Card5] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.HologramSequencingCard005, info);
            m_bytes[(int)ByteIndex.Card6] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.HologramSequencingCard006, info);
            m_bytes[(int)ByteIndex.YellowKey] = MapScriptByte(MM3Map.B4ArachnoidCavern, MM3FileOffsets.YellowFortressKey, info);
            m_bytes[(int)ByteIndex.GreenKey] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.GreenEyeballKey, info);
            m_bytes[(int)ByteIndex.BlackKey] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.BlackTerrorKey, info);
            m_bytes[(int)ByteIndex.BlueKey] = MapScriptByte(MM3Map.B4ArachnoidCavern, MM3FileOffsets.BlueUnholyKey, info);
            m_bytes[(int)ByteIndex.GoldKey] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.GoldMasterKey, info);
            m_bytes[(int)ByteIndex.RedKey] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.RedWarriorsKey, info);
            m_bytes[(int)ByteIndex.KeyCard] = MapScriptByte(MM3Map.A4Surface, MM3FileOffsets.GoldenPyramidKeyCard, info);
            m_bytes[(int)ByteIndex.Insect] = MapScriptByte(MM3Map.B2Surface, MM3FileOffsets.InsectShrine, info);
            m_bytes[(int)ByteIndex.Evil1] = MapScriptByte(MM3Map.A2CastleWhiteshield, MM3FileOffsets.EvilArtifactA2CastleWhiteshield0006, info);
            m_bytes[(int)ByteIndex.Evil2] = MapScriptByte(MM3Map.A2CastleWhiteshield, MM3FileOffsets.EvilArtifactA2CastleWhiteshield0010, info);
            m_bytes[(int)ByteIndex.Evil3] = MapScriptByte(MM3Map.A3HallsOfInsanity, MM3FileOffsets.EvilArtifactA3HallsOfInsanity1026, info);
            m_bytes[(int)ByteIndex.Evil4] = MapScriptByte(MM3Map.A3HallsOfInsanity, MM3FileOffsets.EvilArtifactA3HallsOfInsanity1626, info);
            m_bytes[(int)ByteIndex.Evil5] = MapScriptByte(MM3Map.B4CastleBloodReign, MM3FileOffsets.EvilArtifactB4CastleBloodReign0800, info);
            m_bytes[(int)ByteIndex.Evil6] = MapScriptByte(MM3Map.B4CastleBloodReign, MM3FileOffsets.EvilArtifactB4CastleBloodReign1000, info);
            m_bytes[(int)ByteIndex.Evil7] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.EvilArtifactD1CursedColdCavern1214, info);
            m_bytes[(int)ByteIndex.Evil8] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.GoodArtifactD1CursedColdCavern1215, info);
            m_bytes[(int)ByteIndex.Evil9] = MapScriptByte(MM3Map.D3BlisteringHeights, MM3FileOffsets.EvilArtifactD3BlisteringHeights0102, info);
            m_bytes[(int)ByteIndex.Evil10] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.EvilArtifactE2SwampTown0901, info);
            m_bytes[(int)ByteIndex.Evil11] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.EvilArtifactE2SwampTown1401, info);
            m_bytes[(int)ByteIndex.Evil12] = MapScriptByte(MM3Map.E2SwampTownCavern, MM3FileOffsets.EvilArtifactE2SwampTownCavern0115, info);
            m_bytes[(int)ByteIndex.Evil13] = MapScriptByte(MM3Map.E2SwampTownCavern, MM3FileOffsets.EvilArtifactE2SwampTownCavern0415, info);
            m_bytes[(int)ByteIndex.Evil14] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.EvilArtifactF2TombOfTerror0802, info);
            m_bytes[(int)ByteIndex.Evil15] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.EvilArtifactF2TombOfTerror0806, info);
            m_bytes[(int)ByteIndex.Good1] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.GoodArtifactB1CyclopsCavern0202, info);
            m_bytes[(int)ByteIndex.Good2] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.GoodArtifactB1CyclopsCavern2901, info);
            m_bytes[(int)ByteIndex.Good3] = MapScriptByte(MM3Map.B2FortressOfFear, MM3FileOffsets.GoodArtifactB2FortressOfFear0214, info);
            m_bytes[(int)ByteIndex.Good4] = MapScriptByte(MM3Map.B2FortressOfFear, MM3FileOffsets.GoodArtifactB2FortressOfFear2830, info);
            m_bytes[(int)ByteIndex.Good5] = MapScriptByte(MM3Map.B4CastleBloodReign, MM3FileOffsets.GoodArtifactB4CastleBloodReign0400, info);
            m_bytes[(int)ByteIndex.Good6] = MapScriptByte(MM3Map.B4CastleBloodReign, MM3FileOffsets.GoodArtifactB4CastleBloodReign0600, info);
            m_bytes[(int)ByteIndex.Good7] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.GoodArtifactD1CursedColdCavern2424, info);
            m_bytes[(int)ByteIndex.Good8] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.GoodArtifactD1CursedColdCavern2722, info);
            m_bytes[(int)ByteIndex.Good9] = MapScriptByte(MM3Map.D3BlisteringHeights, MM3FileOffsets.GoodArtifactD3BlisteringHeights0201, info);
            m_bytes[(int)ByteIndex.Good10] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.GoodArtifactF2TombOfTerror0509, info);
            m_bytes[(int)ByteIndex.Good11] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.GoodArtifactF2TombOfTerror1109, info);
            m_bytes[(int)ByteIndex.Neut1] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.NeutArtifactB1CyclopsCavern0402, info);
            m_bytes[(int)ByteIndex.Neut2] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.NeutArtifactB1CyclopsCavern2500, info);
            m_bytes[(int)ByteIndex.Neut3] = MapScriptByte(MM3Map.B1SlithercultStronghold, MM3FileOffsets.NeutArtifactB1SlithercultStronghold2828, info);
            m_bytes[(int)ByteIndex.Neut4] = MapScriptByte(MM3Map.B2FortressOfFear, MM3FileOffsets.NeutArtifactB2FortressOfFear0501, info);
            m_bytes[(int)ByteIndex.Neut5] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.NeutArtifactB3DarkWarriorKeep1501, info);
            m_bytes[(int)ByteIndex.Neut6] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.NeutArtifactB3DarkWarriorKeep1601, info);
            m_bytes[(int)ByteIndex.Neut7] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.NeutArtifactD1CursedColdCavern1200, info);
            m_bytes[(int)ByteIndex.Neut8] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.NeutArtifactD1CursedColdCavern2005, info);
            m_bytes[(int)ByteIndex.Neut9] = MapScriptByte(MM3Map.D3BlisteringHeights, MM3FileOffsets.NeutArtifactD3BlisteringHeights0105, info);
            m_bytes[(int)ByteIndex.Neut10] = MapScriptByte(MM3Map.E1CastleDragontooth, MM3FileOffsets.NeutArtifactE1CastleDragontooth1212, info);
            m_bytes[(int)ByteIndex.Neut11] = MapScriptByte(MM3Map.E1CastleDragontooth, MM3FileOffsets.NeutArtifactE1CastleDragontooth1515, info);
            m_bytes[(int)ByteIndex.Pearl1] = MapScriptByte(MM3Map.B1SlithercultStronghold, MM3FileOffsets.PearlB1SlithercultStronghold0227, info);
            m_bytes[(int)ByteIndex.Pearl2] = MapScriptByte(MM3Map.B2FortressOfFear, MM3FileOffsets.PearlB2FortressOfFear2401, info);
            m_bytes[(int)ByteIndex.Pearl3] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PearlD1CursedColdCavern0928, info);
            m_bytes[(int)ByteIndex.Pearl4] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PearlD1CursedColdCavern2219, info);
            m_bytes[(int)ByteIndex.Pearl5] = MapScriptByte(MM3Map.D3BlisteringHeightsCavern, MM3FileOffsets.PearlD3BlisteringHeightsCavern0713, info);
            m_bytes[(int)ByteIndex.Pearl6] = MapScriptByte(MM3Map.D3BlisteringHeightsCavern, MM3FileOffsets.PearlD3BlisteringHeightsCavern0913, info);
            m_bytes[(int)ByteIndex.GreyAccept] = MapScriptByte(MM3Map.C4CastleGreywind, MM3FileOffsets.AcceptGreywindQuest, info);
            m_bytes[(int)ByteIndex.GreyWall] = MapScriptByte(MM3Map.C4GreywindDungeon, MM3FileOffsets.GreywindWalls0114, info, false);
            m_bytes[(int)ByteIndex.Destroy1] = MapScriptByte(MM3Map.A1Surface, MM3FileOffsets.OrcOutpostA1Surface0503, info);
            m_bytes[(int)ByteIndex.Destroy2] = MapScriptByte(MM3Map.A1Surface, MM3FileOffsets.GoblinWagonA1Surface1207, info);
            m_bytes[(int)ByteIndex.Destroy3] = MapScriptByte(MM3Map.A2Surface, MM3FileOffsets.OrcishShrineA2Surface0404, info);
            m_bytes[(int)ByteIndex.Destroy4] = MapScriptByte(MM3Map.A2Surface, MM3FileOffsets.OrcOutpostA2Surface0407, info);
            m_bytes[(int)ByteIndex.Destroy5] = MapScriptByte(MM3Map.A2Surface, MM3FileOffsets.GoblinHutA2Surface0805, info);
            m_bytes[(int)ByteIndex.Destroy6] = MapScriptByte(MM3Map.A3Surface, MM3FileOffsets.ScreamerWagonA3Surface0309, info);
            m_bytes[(int)ByteIndex.Destroy7] = MapScriptByte(MM3Map.A3Surface, MM3FileOffsets.VampireBatWagonA3Surface1504, info);
            m_bytes[(int)ByteIndex.Destroy8] = MapScriptByte(MM3Map.A4Surface, MM3FileOffsets.SpidersNestA4Surface0512, info);
            m_bytes[(int)ByteIndex.Destroy9] = MapScriptByte(MM3Map.A4Surface, MM3FileOffsets.MagicMantisPodsA4Surface1504, info);
            m_bytes[(int)ByteIndex.Destroy10] = MapScriptByte(MM3Map.B3Surface, MM3FileOffsets.LampreyB3Surface1210, info);
            m_bytes[(int)ByteIndex.Destroy11] = MapScriptByte(MM3Map.B3Surface, MM3FileOffsets.BugabooLarvaeB3Surface0303, info);
            m_bytes[(int)ByteIndex.Destroy12] = MapScriptByte(MM3Map.B2Surface, MM3FileOffsets.OgreMeetingHallB2Surface1104, info);
            m_bytes[(int)ByteIndex.Destroy13] = MapScriptByte(MM3Map.B2Surface, MM3FileOffsets.SpriteHutB2Surface0610, info);
            m_bytes[(int)ByteIndex.Destroy14] = MapScriptByte(MM3Map.B1Surface, MM3FileOffsets.WildFungusSporesB1Surface0503, info);
            m_bytes[(int)ByteIndex.Destroy15] = MapScriptByte(MM3Map.B1Surface, MM3FileOffsets.OhNoBugApiaryB1Surface1208, info);
            m_bytes[(int)ByteIndex.Destroy16] = MapScriptByte(MM3Map.C1Surface, MM3FileOffsets.CyclopsShackC1Surface0612, info);
            m_bytes[(int)ByteIndex.Destroy17] = MapScriptByte(MM3Map.C1Surface, MM3FileOffsets.SpriteHutC1Surface0604, info);
            m_bytes[(int)ByteIndex.Destroy18] = MapScriptByte(MM3Map.C1Surface, MM3FileOffsets.WerewolfShrineC1Surface1409, info);
            m_bytes[(int)ByteIndex.Destroy19] = MapScriptByte(MM3Map.C2Surface, MM3FileOffsets.MajorDevilPortalC2Surface1102, info);
            m_bytes[(int)ByteIndex.Destroy20] = MapScriptByte(MM3Map.C3Surface, MM3FileOffsets.HydraBreedingGroundsC3Surface0709, info);
            m_bytes[(int)ByteIndex.Destroy21] = MapScriptByte(MM3Map.D3Surface, MM3FileOffsets.MajorDemonHutD3Surface0608, info);
            m_bytes[(int)ByteIndex.Destroy22] = MapScriptByte(MM3Map.D2Surface, MM3FileOffsets.FireLizardHatcheryD2Surface01001, info);
            m_bytes[(int)ByteIndex.Destroy23] = MapScriptByte(MM3Map.D2Surface, MM3FileOffsets.FireStalkerLairD2Surface0510, info);
            m_bytes[(int)ByteIndex.Destroy24] = MapScriptByte(MM3Map.E2Surface, MM3FileOffsets.DeathLocustNestE2Surface0311, info);
            m_bytes[(int)ByteIndex.Destroy25] = MapScriptByte(MM3Map.E2Surface, MM3FileOffsets.RogueMeetingPlaceE2Surface0611, info);
            m_bytes[(int)ByteIndex.Destroy26] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.BarbarianCompoundE4Surface0507, info);
            m_bytes[(int)ByteIndex.Destroy27] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.DeathLocustNestE4Surface0908, info);
            m_bytes[(int)ByteIndex.Well] = MapScriptByte(MM3Map.F4Surface, MM3FileOffsets.WellOfRemembrance, info);
            m_bytes[(int)ByteIndex.LordMight] = MapScriptByte(MM3Map.B4ArachnoidCavern, MM3FileOffsets.LordMight, info);
            m_bytes[(int)ByteIndex.Jewelry1] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.JewelryA2BaywatchCavern0611, info);
            m_bytes[(int)ByteIndex.Jewelry2] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.JewelryA2BaywatchCavern0811, info);
            m_bytes[(int)ByteIndex.Jewelry3] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.JewelryA2BaywatchCavern0703, info);
            m_bytes[(int)ByteIndex.Jewelry4] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.JewelryA2BaywatchCavern1403, info);
            m_bytes[(int)ByteIndex.Jewelry5] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.JewelryE2SwampTown0112, info);
            m_bytes[(int)ByteIndex.Jewelry6] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.JewelryE2SwampTown1404, info);
            m_bytes[(int)ByteIndex.Jewelry7] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.JewelryE2SwampTown1101, info);
            m_bytes[(int)ByteIndex.Jewelry8] = MapScriptByte(MM3Map.E2SwampTown, MM3FileOffsets.JewelryE2SwampTown1201, info);
            m_bytes[(int)ByteIndex.Jewelry9] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.JewelryA1AncientTempleOfMoo2627, info);
            m_bytes[(int)ByteIndex.Jewelry10] = MapScriptByte(MM3Map.A1AncientTempleOfMoo, MM3FileOffsets.JewelryA1AncientTempleOfMoo1424, info);
            m_bytes[(int)ByteIndex.Jewelry11] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.JewelryB1CyclopsCavern2827, info);
            m_bytes[(int)ByteIndex.Jewelry12] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.JewelryB1CyclopsCavern0916, info);
            m_bytes[(int)ByteIndex.L6Items1] = MapScriptByte(MM3Map.E1CastleDragontooth, MM3FileOffsets.L6Items1E1CD1215, info);
            m_bytes[(int)ByteIndex.L6Items2] = MapScriptByte(MM3Map.E1CastleDragontooth, MM3FileOffsets.L6Items2E1CD1307, info);
            m_bytes[(int)ByteIndex.L6Items3] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.L6Items3D1CCC1503, info);
            m_bytes[(int)ByteIndex.L6Items4] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.L6Items4F1DC2204, info);
            m_bytes[(int)ByteIndex.L6Items5] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.L6Items5F1DC2504, info);
            m_bytes[(int)ByteIndex.L6Items6] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.L6Items6F1DC0302, info);
            m_bytes[(int)ByteIndex.L6Items7] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.L6Items7F1DC1201, info);
            m_bytes[(int)ByteIndex.L6Items8] = MapScriptByte(MM3Map.F1DragonCavern, MM3FileOffsets.L6Items8F1DC2301, info);
            m_bytes[(int)ByteIndex.L6Items9] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.L6Items9F2TT1806, info);
            m_bytes[(int)ByteIndex.L6Items10] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.L6Items10F2TT3004, info);
            m_bytes[(int)ByteIndex.L6Items11] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.L6Items11F3TMFH0725, info);
            m_bytes[(int)ByteIndex.L6Items12] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.L6Items12F3TMFH0815, info);
            m_bytes[(int)ByteIndex.L6Items13] = MapScriptByte(MM3Map.F3TheMazeFromHell, MM3FileOffsets.L6Items13F3TMFH1501, info);
            m_bytes[(int)ByteIndex.L6Items14] = MapScriptByte(MM3Map.C2CentralControlSector, MM3FileOffsets.L6Items14C2CCS1113, info);
            m_bytes[(int)ByteIndex.L6Items15] = MapScriptByte(MM3Map.C2CentralControlSector, MM3FileOffsets.L6Items15C2CCS0112, info);
            m_bytes[(int)ByteIndex.L6Items16] = MapScriptByte(MM3Map.B3Surface, MM3FileOffsets.L6Items16B3S1111, info);
            m_bytes[(int)ByteIndex.L6Items17] = MapScriptByte(MM3Map.B4Surface, MM3FileOffsets.L6Items17B4S0814, info);
            m_bytes[(int)ByteIndex.L6Items18] = MapScriptByte(MM3Map.B4Surface, MM3FileOffsets.L6Items18B4S1011, info);
            m_bytes[(int)ByteIndex.L6Items19] = MapScriptByte(MM3Map.B4Surface, MM3FileOffsets.L6Items19B4S1209, info);
            m_bytes[(int)ByteIndex.L6Items20] = MapScriptByte(MM3Map.C2Surface, MM3FileOffsets.L6Items20C2S0213, info);
            m_bytes[(int)ByteIndex.L6Items21] = MapScriptByte(MM3Map.C2Surface, MM3FileOffsets.L6Items21C2S1212, info);
            m_bytes[(int)ByteIndex.L6Items22] = MapScriptByte(MM3Map.C2Surface, MM3FileOffsets.L6Items22C2S0406, info);
            m_bytes[(int)ByteIndex.L6Items23] = MapScriptByte(MM3Map.C3Surface, MM3FileOffsets.L6Items23C3S0213, info);
            m_bytes[(int)ByteIndex.L6Items24] = MapScriptByte(MM3Map.C3Surface, MM3FileOffsets.L6Items24C3S0709, info);
            m_bytes[(int)ByteIndex.L6Items25] = MapScriptByte(MM3Map.C3Surface, MM3FileOffsets.L6Items25C3S0204, info);
            m_bytes[(int)ByteIndex.L6Items26] = MapScriptByte(MM3Map.C3Surface, MM3FileOffsets.L6Items26C3S1102, info);
            m_bytes[(int)ByteIndex.L6Items27] = MapScriptByte(MM3Map.D3Surface, MM3FileOffsets.L6Items27D3S1407, info);
            m_bytes[(int)ByteIndex.L6Items28] = MapScriptByte(MM3Map.D3Surface, MM3FileOffsets.L6Items28D3S0104, info);
            m_bytes[(int)ByteIndex.L6Items29] = MapScriptByte(MM3Map.D3Surface, MM3FileOffsets.L6Items29D3S1203, info);
            m_bytes[(int)ByteIndex.L6Items30] = MapScriptByte(MM3Map.E2Surface, MM3FileOffsets.L6Items30E2S0808, info);
            m_bytes[(int)ByteIndex.L6Items31] = MapScriptByte(MM3Map.E3Surface, MM3FileOffsets.L6Items31E3S1011, info);
            m_bytes[(int)ByteIndex.L6Items32] = MapScriptByte(MM3Map.E3Surface, MM3FileOffsets.L6Items32E3S0609, info);
            m_bytes[(int)ByteIndex.L6Items33] = MapScriptByte(MM3Map.E3Surface, MM3FileOffsets.L6Items33E3S0905, info);
            m_bytes[(int)ByteIndex.L6Items34] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.L6Items34E4S0412, info);
            m_bytes[(int)ByteIndex.L6Items35] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.L6Items35E4S1011, info);
            m_bytes[(int)ByteIndex.L6Items36] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.L6Items36E4S0806, info);
            m_bytes[(int)ByteIndex.L6Items37] = MapScriptByte(MM3Map.E4Surface, MM3FileOffsets.L6Items37E4S1306, info);
            m_bytes[(int)ByteIndex.L6Items38] = MapScriptByte(MM3Map.F2Surface, MM3FileOffsets.L6Items38F2S1102, info);
            m_bytes[(int)ByteIndex.L6Items39] = MapScriptByte(MM3Map.F4Surface, MM3FileOffsets.L6Items39F4S0710, info);
            m_bytes[(int)ByteIndex.L6Items40] = MapScriptByte(MM3Map.F4Surface, MM3FileOffsets.L6Items40F4S1309, info);
            m_bytes[(int)ByteIndex.L6Items41] = MapScriptByte(MM3Map.F4Surface, MM3FileOffsets.L6Items41F4S0206, info);
            m_bytes[(int)ByteIndex.Perm1] = MapScriptByte(MM3Map.A2WhiteshieldDungeon, MM3FileOffsets.PermRes20A2WD1107, info);
            m_bytes[(int)ByteIndex.Perm2] = MapScriptByte(MM3Map.A2WhiteshieldDungeon, MM3FileOffsets.PermStats10AA2WD0903, info);
            m_bytes[(int)ByteIndex.Perm3] = MapScriptByte(MM3Map.A2WhiteshieldDungeon, MM3FileOffsets.PermStats10BA2WD0901, info);
            m_bytes[(int)ByteIndex.Perm4] = MapScriptByte(MM3Map.A2WhiteshieldDungeon, MM3FileOffsets.PermLevel5AA2WD1501, info);
            m_bytes[(int)ByteIndex.Perm5] = MapScriptByte(MM3Map.B4BloodreignDungeon, MM3FileOffsets.PermAcy50AB4BRD0615, info);
            m_bytes[(int)ByteIndex.Perm6] = MapScriptByte(MM3Map.B4BloodreignDungeon, MM3FileOffsets.PermSpd50B4BRD1515, info);
            m_bytes[(int)ByteIndex.Perm7] = MapScriptByte(MM3Map.B4BloodreignDungeon, MM3FileOffsets.PermLck50B4BRD0005, info);
            m_bytes[(int)ByteIndex.Perm8] = MapScriptByte(MM3Map.E1DragontoothDungeon, MM3FileOffsets.PermLevel5BE1DD0508, info);
            m_bytes[(int)ByteIndex.Perm9] = MapScriptByte(MM3Map.E1DragontoothDungeon, MM3FileOffsets.PermInt50AE1DD0708, info);
            m_bytes[(int)ByteIndex.Perm10] = MapScriptByte(MM3Map.E1DragontoothDungeon, MM3FileOffsets.PermEnd50AE1DD0507, info);
            m_bytes[(int)ByteIndex.Perm11] = MapScriptByte(MM3Map.E1DragontoothDungeon, MM3FileOffsets.PermPer50AE1DD0707, info);
            m_bytes[(int)ByteIndex.Perm12] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermMagRes30B1CC1030, info);
            m_bytes[(int)ByteIndex.Perm13] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermElecRes20AB1CC2729, info);
            m_bytes[(int)ByteIndex.Perm14] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermEnergyRes30B1CC0227, info);
            m_bytes[(int)ByteIndex.Perm15] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermElecRes20BB1CC0225, info);
            m_bytes[(int)ByteIndex.Perm16] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermPer50BB1CC0312, info);
            m_bytes[(int)ByteIndex.Perm17] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermAcy50BB1CC2910, info);
            m_bytes[(int)ByteIndex.Perm18] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermInt50BB1CC2308, info);
            m_bytes[(int)ByteIndex.Perm19] = MapScriptByte(MM3Map.B1CyclopsCavern, MM3FileOffsets.PermEnd50BB1CC2803, info);
            m_bytes[(int)ByteIndex.Perm20] = MapScriptByte(MM3Map.B1SlithercultStronghold, MM3FileOffsets.PermLevel2B1SS0208, info);
            m_bytes[(int)ByteIndex.Perm21] = MapScriptByte(MM3Map.B1SlithercultStronghold, MM3FileOffsets.PermPoisonRes25AQB1SS2207, info);
            m_bytes[(int)ByteIndex.Perm22] = MapScriptByte(MM3Map.B1SlithercultStronghold, MM3FileOffsets.PermPoisonRes25BAB1SS2204, info);
            m_bytes[(int)ByteIndex.Perm23] = MapScriptByte(MM3Map.B3CathedralOfCarnage, MM3FileOffsets.PermLevel5CBB3CC1311, info);
            m_bytes[(int)ByteIndex.Perm24] = MapScriptByte(MM3Map.B3CathedralOfCarnage, MM3FileOffsets.PermLevel5DCB3CC1303, info);
            m_bytes[(int)ByteIndex.Perm25] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermMight10ADB3DWK2330, info);
            m_bytes[(int)ByteIndex.Perm26] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermMight10BAB3DWK0524, info);
            m_bytes[(int)ByteIndex.Perm27] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermEnd20ABB3DWK3022, info);
            m_bytes[(int)ByteIndex.Perm28] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermSpeed20AAB3DWK3011, info);
            m_bytes[(int)ByteIndex.Perm29] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermEnd10AAB3DWK0110, info);
            m_bytes[(int)ByteIndex.Perm30] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermMight25AB3DWK0101, info);
            m_bytes[(int)ByteIndex.Perm31] = MapScriptByte(MM3Map.B3DarkWarriorKeep, MM3FileOffsets.PermLevel2BB3DWK1201, info);
            m_bytes[(int)ByteIndex.Perm32] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermMagicRes20AED1CCC0729, info);
            m_bytes[(int)ByteIndex.Perm33] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermMagicRes20BAD1CCC2221, info);
            m_bytes[(int)ByteIndex.Perm34] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermLevel1ABD1CCC1415, info);
            m_bytes[(int)ByteIndex.Perm35] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermLevel1BAD1CCC1615, info);
            m_bytes[(int)ByteIndex.Perm36] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermLevel1CBD1CCC1413, info);
            m_bytes[(int)ByteIndex.Perm37] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermLevel1DCD1CCC1613, info);
            m_bytes[(int)ByteIndex.Perm38] = MapScriptByte(MM3Map.D1CursedColdCavern, MM3FileOffsets.PermMagicRes20CDD1CCC1202, info);
            m_bytes[(int)ByteIndex.Perm39] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermInt10ACE4TMC0228, info);
            m_bytes[(int)ByteIndex.Perm40] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermSpeed10AAE4TMC0927, info);
            m_bytes[(int)ByteIndex.Perm41] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermInt10BAE4TMC2827, info);
            m_bytes[(int)ByteIndex.Perm42] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermInt10CBE4TMC2224, info);
            m_bytes[(int)ByteIndex.Perm43] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermSpeed10BCE4TMC0219, info);
            m_bytes[(int)ByteIndex.Perm44] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermInt10DBE4TMC0919, info);
            m_bytes[(int)ByteIndex.Perm45] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermSpeed10CDE4TMC0817, info);
            m_bytes[(int)ByteIndex.Perm46] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermSpeed10DCE4TMC2013, info);
            m_bytes[(int)ByteIndex.Perm47] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermSpeed10EDE4TMC2005, info);
            m_bytes[(int)ByteIndex.Perm48] = MapScriptByte(MM3Map.E4TheMagicCavern, MM3FileOffsets.PermInt10EEE4TMC0502, info);
            m_bytes[(int)ByteIndex.Perm49] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.PermLevel3Stats20AEF2TT2806, info);
            m_bytes[(int)ByteIndex.Perm50] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.PermLevel3Stats20BAF2TT2906, info);
            m_bytes[(int)ByteIndex.Perm51] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.PermLevel3Stats20CBF2TT2802, info);
            m_bytes[(int)ByteIndex.Perm52] = MapScriptByte(MM3Map.F2TombOfTerror, MM3FileOffsets.PermLevel3Stats20DCF2TT2902, info);
            m_bytes[(int)ByteIndex.Perm53] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermMight20ADA2FSS1510, info);
            m_bytes[(int)ByteIndex.Perm54] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermInt20AA2FSS1508, info);
            m_bytes[(int)ByteIndex.Perm55] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermPer20A2FSS1506, info);
            m_bytes[(int)ByteIndex.Perm56] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermLuck20A2FSS1404, info);
            m_bytes[(int)ByteIndex.Perm57] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermEnd20BA2FSS0503, info);
            m_bytes[(int)ByteIndex.Perm58] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermLevel2CBA2FSS1403, info);
            m_bytes[(int)ByteIndex.Perm59] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermAcy20CA2FSS0502, info);
            m_bytes[(int)ByteIndex.Perm60] = MapScriptByte(MM3Map.A2ForwardStorageSector, MM3FileOffsets.PermSpeed20BA2FSS0501, info);
            m_bytes[(int)ByteIndex.Perm61] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermEnd5BA1FHC0014, info);
            m_bytes[(int)ByteIndex.Perm62] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermMight5BBA1FHC0613, info);
            m_bytes[(int)ByteIndex.Perm63] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermInt5FBA1FHC1511, info);
            m_bytes[(int)ByteIndex.Perm64] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermPer5DFA1FHC1410, info);
            m_bytes[(int)ByteIndex.Perm65] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermAcy5BDA1FHC0706, info);
            m_bytes[(int)ByteIndex.Perm66] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermEnd5CBA1FHC0505, info);
            m_bytes[(int)ByteIndex.Perm67] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermEnd5DCA1FHC0703, info);
            m_bytes[(int)ByteIndex.Perm68] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermSpeed5DA1FHC1503, info);
            m_bytes[(int)ByteIndex.Perm69] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermLuck5AA1FHC0102, info);
            m_bytes[(int)ByteIndex.Perm70] = MapScriptByte(MM3Map.A1FountainHeadCavern, MM3FileOffsets.PermEnd5EAA1FHC1401, info);
            m_bytes[(int)ByteIndex.Perm71] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermEnd10BEB4WC0515, info);
            m_bytes[(int)ByteIndex.Perm72] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermSpeed10BB4WC1115, info);
            m_bytes[(int)ByteIndex.Perm73] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermAcy5CB4WC1007, info);
            m_bytes[(int)ByteIndex.Perm74] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermPer5ECB4WC1207, info);
            m_bytes[(int)ByteIndex.Perm75] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermMight10CEB4WC0105, info);
            m_bytes[(int)ByteIndex.Perm76] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermLuck5BCB4WC1005, info);
            m_bytes[(int)ByteIndex.Perm77] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermInt5GBB4WC0903, info);
            m_bytes[(int)ByteIndex.Perm78] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.PermEnd5FGB4WC1503, info);
            m_bytes[(int)ByteIndex.Hireling1] = MapScriptByte(MM3Map.B4BloodreignDungeon, MM3FileOffsets.SonOfAbuB4BRD1403, info);
            m_bytes[(int)ByteIndex.Hireling2] = MapScriptByte(MM3Map.B4BloodreignDungeon, MM3FileOffsets.CharityB4BRD1400, info);
            m_bytes[(int)ByteIndex.Hireling3] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.DarlanaA2BC1501, info);
            m_bytes[(int)ByteIndex.Hireling4] = MapScriptByte(MM3Map.A2BaywatchCavern, MM3FileOffsets.SirGalantA2BC1500, info);
            m_bytes[(int)ByteIndex.Hireling5] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.LoneWolfB4WC0013, info);
            m_bytes[(int)ByteIndex.Hireling6] = MapScriptByte(MM3Map.B4WildabarCavern, MM3FileOffsets.WartowsanB4WC0407, info);
            Valid = true;
        }

        public override byte[] GetBytes()
        {
            return m_bytes;
        }
    }

}