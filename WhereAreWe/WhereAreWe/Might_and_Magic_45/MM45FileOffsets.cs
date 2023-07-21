using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public static class MM45Bytes
    {
        public const int PhirnaF3CS0802 = 1108;
        public const int PhirnaF4CS1214 = 1088;
        public const int PhirnaF4CS0512 = 833;
        public const int PhirnaF4CS0712 = 918;
        public const int PhirnaF4CS1312 = 1003;
        public const int PhirnaF4CS0607 = 493;
        public const int PhirnaF4CS0807 = 578;
        public const int PhirnaF4CS1207 = 663;
        public const int PhirnaF4CS1204 = 748;
        public const int PhirnaF4CS0702 = 408;
        public const int AlacornF4WT40704 = 588;
        public const int ReturnedAlacornF4CS0903 = 218;
        public const int WhistleE4CS0514 = 36;
        public const int ReturnedSkullD3CS1208 = 533;
        public const int ReturnedTiaraD2CBL30211 = 116;
        public const int ReturnedScarabC2CS1006 = 358;
        public const int ReturnedCrystalsC2CS0811 = 658;
        public const int DeliveredElixirD4CS1203 = 520;
        public const int DeliveredBookC3CS0308 = 325;
        public const int DeliveredRockB3CS0906 = 107;
        public const int DeliveredScrollA1CS1105 = 176;
        public const int TurnInCyclopsA3CS1000 = 442;
        public const int TurnInTrollLairB3CS0603 = 335;
        public const int FoundXeenSlayerSwordC4ND0704 = 557;
        public const int TurnInMirrorD2CBL10801 = 1723;

        public const int TurnInDreyfusA3WTL40410 = 525;
        public const int TurnInLunaA4DS1315 = 212;
        public const int TalkedToAmbroseB1DS1205 = 5;
        public const int TurnInSongbirdA4CKL21115 = 697;
        public const int TurnInOgresB3DS1104 = 709;
        public const int TurnInVesparB3DS0701 = 879;
        public const int BringMelon1B4DS0312 = 126;
        public const int BringMelon2B4DS0312 = 144;
        public const int FoundMelon1A4DS0804 = 150;
        public const int FoundMelon2A4DS1403 = 185;
        public const int FoundMelon3A4DS0301 = 115;
        public const int FoundMelon4B4DS0104 = 22;
        public const int TurnInSheewanaC4DS0107 = 935;
        public const int TurnInChaliceD1DS0108 = 14;
        public const int TurnInEctorE2DS0312 = 170;
        public const int TurnInCalebE3DS1313 = 626;
        public const int TurnInJewelF4DS0607 = 14;
        public const int TurnInGettleA4C2327 = 1298;
        public const int TurnInJethroA4C2224 = 1519;
        public const int TurnInAstraE3S2014 = 4875;
        public const int EnergyDiskA3WTL10608 = 145;
        public const int EnergyDiskA3WTL10808 = 213;
        public const int EnergyDiskD1NTL40308 = 39;
        public const int EnergyDiskD1NTL41108 = 107;
        public const int EnergyDiskD4STL30505 = 319;
        public const int EnergyDiskD4STL30905 = 354;
        public const int EnergyDiskF3ETL31108 = 328;
        public const int EnergyDiskF3ETL30704 = 260;
        public const int EnergyDiskA4CKL20015 = 1891;
        public const int EnergyDiskA4CKL20215 = 2195;
        public const int EnergyDiskA4CKL20415 = 2499;
        public const int EnergyDiskA4CKL20815 = 2803;
        public const int EnergyDiskA4CKL20012 = 1587;
        public const int EnergyDiskA4CKL20010 = 1283;
        public const int EnergyDiskC4TBL50018 = 2023;
        public const int EnergyDiskC4TBL53117 = 2091;
        public const int EnergyDiskC4DS0107 = 935;
        public const int EnergyDiskD1DS1005 = 703;
        public const int EnergyDiskD3DS1105 = 168;
        public const int EnergyDiskA4C2913 = 422;
        public const int TurnInDisksA4ETL40408 = 1003;
        public const int TurnInEggD1DTL10605 = 164;

        public const int FireScroll1A1CBL10313 = 1183;
        public const int FireScroll2A1CBL10613 = 1292;
        public const int FireBrew1C3THML10505 = 61;
        public const int FireBrew2C3THML20505 = 564;
        public const int ElectricityScroll1A1CBL10309 = 1401;
        public const int ElectricityScroll2A1CBL10609 = 1510;
        public const int ElectricBrew1C3THML10511 = 136;
        public const int ElectricBrew2C3THML20406 = 639;
        public const int ColdBrew1C3THML10911 = 211;
        public const int ColdBrew2C3THML20410 = 714;
        public const int PoisonBrew1C3THML20511 = 789;
        public const int PoisonBrew2C3THML30410 = 114;
        public const int PoisonBrew3C3THML31010 = 39;
        public const int EnergyScroll1A1CBL31102 = 1168;
        public const int EnergyScroll2A1CBL31201 = 1059;
        public const int EnergyBrew1C3THML30406 = 189;
        public const int MagicScroll1A1CBL21104 = 732;
        public const int MagicScroll2A1CBL31404 = 950;
        public const int MagicBrew1C3THML31006 = 264;

        public const int RedLiquid1D2BD1103 = 71; // MM4Map.D2BurlockDungeon
        public const int MightSkull1B4CIL11113 = 1060; // MM4Map.B4CaveOfIllusionLevel1
        public const int MightSkull2B4CIL11201 = 550; // MM4Map.B4CaveOfIllusionLevel1
        public const int MightSkull3B4CIL20314 = 380; // MM4Map.B4CaveOfIllusionLevel2
        public const int MightSkull4B4CIL30114 = 380; // MM4Map.B4CaveOfIllusionLevel3
        public const int MightSkull5B4CIL40114 = 380; // MM4Map.B4CaveOfIllusionLevel4
        public const int MightJuice1C4TTT0930 = 1124; // MM4Map.C4TombOfAThousandTerrors
        public const int MightJuice2C4TTT1330 = 1190; // MM4Map.C4TombOfAThousandTerrors
        public const int MightJuice3C4TTT0126 = 1256; // MM4Map.C4TombOfAThousandTerrors
        public const int MightJuice4C4TTT0726 = 1322; // MM4Map.C4TombOfAThousandTerrors
        public const int MightLiquid2F3DM10507 = 1800; // MM4Map.F3DwarfMine1
        public const int MightLiquid3F3DM10304 = 1740; // MM4Map.F3DwarfMine1
        public const int MightLiquid4F3DM21026 = 2080; // MM4Map.F3DwarfMine2
        public const int MightLiquid5E2DM30201 = 1613; // MM4Map.E2DwarfMine3
        public const int MightLiquid6E2DM30301 = 1679; // MM4Map.E2DwarfMine3
        public const int MightLiquid7E2DM30501 = 1745; // MM4Map.E2DwarfMine3
        public const int MightLiquid8E2DM30601 = 1811; // MM4Map.E2DwarfMine3
        public const int MightBookD3DTL21006 = 255; // MM4Map.D3DarzogsTowerLevel2
        public const int MightBrew1A4CKL30802 = 886; // MM5Map.A4CastleKalindraLevel3
        public const int MightBrew2A4CKL30902 = 828; // MM5Map.A4CastleKalindraLevel3
        public const int MightBrew3A4CKL30801 = 944; // MM5Map.A4CastleKalindraLevel3
        public const int MightBrew4A4CKL30901 = 770; // MM5Map.A4CastleKalindraLevel3
        public const int MightBrew5A4CKL30900 = 712; // MM5Map.A4CastleKalindraLevel3
        public const int MightPotion1aC4TBL10215 = 1047; // MM5Map.C4TempleOfBarkLevel1
        public const int MightPotion1bC4TBL10215 = 1056; // MM5Map.C4TempleOfBarkLevel1
        public const int MightPotion1cC4TBL10215 = 1065; // MM5Map.C4TempleOfBarkLevel1
        public const int MightPotion2aC4TBL21106 = 1306; // MM5Map.C4TempleOfBarkLevel2
        public const int MightPotion2bC4TBL21106 = 1315; // MM5Map.C4TempleOfBarkLevel2
        public const int MightPotion2cC4TBL21106 = 1324; // MM5Map.C4TempleOfBarkLevel2
        public const int MightMagpie1F2LSDL53125 = 1141; // MM5Map.F2LostSoulsDungeonLevel5
        public const int MightMagpie2F2LSDL52917 = 1244; // MM5Map.F2LostSoulsDungeonLevel5
        public const int MightAppleE4DS1313 = 597; // MM5Map.E4Surface
        public const int MightLiquid9A4CS0226 = 2593; // MM5Map.A4CastleviewSewer
        public const int MightLiquid10A4CS0426 = 2679; // MM5Map.A4CastleviewSewer
        public const int MightLiquid11A4CS0225 = 2636; // MM5Map.A4CastleviewSewer
        public const int MightLiquid12A4CS0425 = 2722; // MM5Map.A4CastleviewSewer
        public const int MightBrew6aE3SS3004 = 1194; // MM5Map.E3SandcasterSewer
        public const int MightBrew6bE3SS3004 = 1203; // MM5Map.E3SandcasterSewer
        public const int MightBrew6cE3SS3004 = 1212; // MM5Map.E3SandcasterSewer
        public const int MightBrew6dE3SS3004 = 1221; // MM5Map.E3SandcasterSewer
        public const int MightBrew6eE3SS3004 = 1230; // MM5Map.E3SandcasterSewer
        public const int MightBrew6fE3SS3004 = 1248; // MM5Map.E3SandcasterSewer

        public const int IntellectScroll1A1CBL20508 = 78; // MM4Map.A1CastleBasenjiLevel2
        public const int IntellectScroll2A1CBL20708 = 187; // MM4Map.A1CastleBasenjiLevel2
        public const int IntellectSkull1B4CIL11413 = 975; // MM4Map.B4CaveOfIllusionLevel1
        public const int IntellectSkull2B4CIL10400 = 465; // MM4Map.B4CaveOfIllusionLevel1
        public const int IntellectSkull3B4CIL20613 = 40; // MM4Map.B4CaveOfIllusionLevel2
        public const int IntellectSkull4B4CIL30407 = 465; // MM4Map.B4CaveOfIllusionLevel3
        public const int IntellectSkull5B4CIL31003 = 890; // MM4Map.B4CaveOfIllusionLevel3
        public const int IntellectJuice1C4TTT1219 = 1520; // MM4Map.C4TombOfAThousandTerrors
        public const int IntellectJuice2C4TTT1513 = 1586; // MM4Map.C4TombOfAThousandTerrors
        public const int IntellectLiquid1F3DM10823 = 1980; // MM4Map.F3DwarfMine1
        public const int IntellectLiquid2F3DM10722 = 2040; // MM4Map.F3DwarfMine1
        public const int IntellectLiquid3F3DM21123 = 2212; // MM4Map.F3DwarfMine2
        public const int IntellectBook1D3DTL20905 = 314; // MM4Map.D3DarzogsTowerLevel2
        public const int IntellectPotion1aC4TBL21015 = 1678; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectPotion1bC4TBL21015 = 1687; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectPotion1cC4TBL21015 = 1696; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectPotion2aC4TBL21404 = 1554; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectPotion2bC4TBL21404 = 1563; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectPotion2cC4TBL21404 = 1572; // MM5Map.C4TempleOfBarkLevel2
        public const int IntellectOrangeC4DS0614 = 1459; // MM5Map.C4Surface
        public const int IntellectPotion3aE3S2310 = 3612; // MM5Map.E3Sandcaster
        public const int IntellectPotion3bE3S2310 = 3621; // MM5Map.E3Sandcaster
        public const int IntellectPotion3cE3S2310 = 3573; // MM5Map.E3Sandcaster
        public const int IntellectPotion4aE3S1305 = 3760; // MM5Map.E3Sandcaster
        public const int IntellectPotion4bE3S1305 = 3769; // MM5Map.E3Sandcaster
        public const int IntellectPotion4cE3S1305 = 3721; // MM5Map.E3Sandcaster
        public const int IntellectPotion5aE3S1303 = 3908; // MM5Map.E3Sandcaster
        public const int IntellectPotion5bE3S1303 = 3917; // MM5Map.E3Sandcaster
        public const int IntellectPotion5cE3S1303 = 3869; // MM5Map.E3Sandcaster

        public const int PersonalityScroll1A1CBL20514 = 296; // MM4Map.A1CastleBasenjiLevel2
        public const int PersonalityScroll2A1CBL20714 = 405; // MM4Map.A1CastleBasenjiLevel2
        public const int PersonalitySkull1B4CIL11506 = 890; // MM4Map.B4CaveOfIllusionLevel1
        public const int PersonalitySkull2B4CIL10101 = 380; // MM4Map.B4CaveOfIllusionLevel1
        public const int PersonalitySkull3B4CIL20109 = 125; // MM4Map.B4CaveOfIllusionLevel2
        public const int PersonalitySkull4B4CIL30103 = 550; // MM4Map.B4CaveOfIllusionLevel3
        public const int PersonalitySkull5B4CIL31000 = 975; // MM4Map.B4CaveOfIllusionLevel3
        public const int PersonalityJuice1C4TTT1105 = 1388; // MM4Map.C4TombOfAThousandTerrors
        public const int PersonalityJuice2C4TTT1505 = 1454; // MM4Map.C4TombOfAThousandTerrors
        public const int PersonalityLiquid1F3DM10621 = 1920; // MM4Map.F3DwarfMine1
        public const int PersonalityLiquid2F3DM10620 = 1860; // MM4Map.F3DwarfMine1
        public const int PersonalityLiquid3F3DM21125 = 2146; // MM4Map.F3DwarfMine2
        public const int PersonalityBookD3DTL20505 = 353; // MM4Map.D3DarzogsTowerLevel2
        public const int PersonalityBrew1A4CKL30815 = 538; // MM5Map.A4CastleKalindraLevel3
        public const int PersonalityBrew2A4CKL30915 = 596; // MM5Map.A4CastleKalindraLevel3
        public const int PersonalityBrew3A4CKL30914 = 654; // MM5Map.A4CastleKalindraLevel3
        public const int PersonalityPotion1aC4TBL10211 = 1181; // MM5Map.C4TempleOfBarkLevel1
        public const int PersonalityPotion1bC4TBL10211 = 1190; // MM5Map.C4TempleOfBarkLevel1
        public const int PersonalityPotion1cC4TBL10211 = 1199; // MM5Map.C4TempleOfBarkLevel1
        public const int PersonalityPotion2aC4TBL21204 = 1430; // MM5Map.C4TempleOfBarkLevel2
        public const int PersonalityPotion2bC4TBL21204 = 1439; // MM5Map.C4TempleOfBarkLevel2
        public const int PersonalityPotion2cC4TBL21204 = 1448; // MM5Map.C4TempleOfBarkLevel2
        public const int PersonalityParakeet1F2LSDL52024 = 1038; // MM5Map.F2LostSoulsDungeonLevel5
        public const int PersonalityParakeet2F2LSDL52011 = 935; // MM5Map.F2LostSoulsDungeonLevel5
        public const int PersonalityBlueberriesD4DS0612 = 311; // MM5Map.D4Surface
        public const int PersonalityCauldronF2L1401 = 1361; // MM5Map.F2Lakeside

        public const int EnduranceSkull1B4CIL10306 = 210; // MM4Map.B4CaveOfIllusionLevel1
        public const int EnduranceSkull2B4CIL10805 = 805; // MM4Map.B4CaveOfIllusionLevel1
        public const int EnduranceSkull3B4CIL30202 = 635; // MM4Map.B4CaveOfIllusionLevel3
        public const int EnduranceSkull4B4CIL41410 = 40; // MM4Map.B4CaveOfIllusionLevel4
        public const int EnduranceSkull5B4CIL41103 = 125; // MM4Map.B4CaveOfIllusionLevel4
        public const int EnduranceJuice1C4TTT0122 = 1652; // MM4Map.C4TombOfAThousandTerrors
        public const int EnduranceJuice2C4TTT0722 = 2312; // MM4Map.C4TombOfAThousandTerrors
        public const int EnduranceJuice3C4TTT0817 = 1718; // MM4Map.C4TombOfAThousandTerrors
        public const int EnduranceLiquid1F3DM10817 = 2100; // MM4Map.F3DwarfMine1
        public const int EnduranceLiquid2F3DM10917 = 2160; // MM4Map.F3DwarfMine1
        public const int EnduranceLiquid3F3DM11017 = 2220; // MM4Map.F3DwarfMine1
        public const int EnduranceLiquid4F3DM21022 = 2278; // MM4Map.F3DwarfMine2
        public const int EndurancePotion1aD1DTL30511 = 189; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion1bD1DTL30511 = 198; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion1cD1DTL30511 = 207; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion1dD1DTL30511 = 216; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion2aD1DTL30911 = 362; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion2bD1DTL30911 = 371; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion2cD1DTL30911 = 380; // MM5Map.D1DragonTowerLevel3
        public const int EndurancePotion2dD1DTL30911 = 389; // MM5Map.D1DragonTowerLevel3
        public const int EnduranceBookD3DTL20406 = 402; // MM4Map.D3DarzogsTowerLevel2
        public const int EndurancePotion3aC4TBL21515 = 1802; // MM5Map.C4TempleOfBarkLevel2
        public const int EndurancePotion3bC4TBL21515 = 1811; // MM5Map.C4TempleOfBarkLevel2
        public const int EndurancePotion3cC4TBL21515 = 1820; // MM5Map.C4TempleOfBarkLevel2
        public const int EndurancePotion4aC4TBL21503 = 1926; // MM5Map.C4TempleOfBarkLevel2
        public const int EndurancePotion4bC4TBL21503 = 1935; // MM5Map.C4TempleOfBarkLevel2
        public const int EndurancePotion4cC4TBL21503 = 1944; // MM5Map.C4TempleOfBarkLevel2
        public const int EnduranceEagle1F2LSDL52731 = 626; // MM5Map.F2LostSoulsDungeonLevel5
        public const int EnduranceEagle2F2LSDL52615 = 523; // MM5Map.F2LostSoulsDungeonLevel5
        public const int EnduranceEagle3F2LSDL53115 = 420; // MM5Map.F2LostSoulsDungeonLevel5
        public const int EndurancePearC4DS1304 = 1181; // MM5Map.C4Surface
        public const int EnduranceLiquid5A4CS2008 = 2371; // MM5Map.A4CastleviewSewer
        public const int EnduranceLiquid6A4CS2208 = 2500; // MM5Map.A4CastleviewSewer
        public const int EnduranceLiquid7A4CS2007 = 2328; // MM5Map.A4CastleviewSewer
        public const int EnduranceLiquid8A4CS2207 = 2457; // MM5Map.A4CastleviewSewer
        public const int EnduranceLiquid9A4CS2006 = 2285; // MM5Map.A4CastleviewSewer
        public const int EnduranceLiquid10A4CS2206 = 2414; // MM5Map.A4CastleviewSewer
        public const int EnduranceBrewaE3SS2903 = 1034; // MM5Map.E3SandcasterSewer
        public const int EnduranceBrewbE3SS2903 = 1043; // MM5Map.E3SandcasterSewer
        public const int EnduranceBrewcE3SS2903 = 1052; // MM5Map.E3SandcasterSewer
        public const int EnduranceBrewdE3SS2903 = 1061; // MM5Map.E3SandcasterSewer
        public const int EnduranceBreweE3SS2903 = 1070; // MM5Map.E3SandcasterSewer
        public const int EnduranceBrewfE3SS2903 = 1088; // MM5Map.E3SandcasterSewer
        public const int EnduranceCauldronF2L0905 = 1421; // MM5Map.F2Lakeside

        public const int SpeedScroll1A1CBL20712 = 623; // MM4Map.A1CastleBasenjiLevel2
        public const int SpeedScroll2A1CBL20710 = 514; // MM4Map.A1CastleBasenjiLevel2
        public const int SpeedSkull1B4CIL10908 = 1145; // MM4Map.B4CaveOfIllusionLevel1
        public const int SpeedSkull2B4CIL10506 = 295; // MM4Map.B4CaveOfIllusionLevel1
        public const int SpeedSkull3B4CIL20600 = 295; // MM4Map.B4CaveOfIllusionLevel2
        public const int SpeedSkull4B4CIL30500 = 805; // MM4Map.B4CaveOfIllusionLevel3
        public const int SpeedJuice1C4TTT1619 = 2048; // MM4Map.C4TombOfAThousandTerrors
        public const int SpeedJuice2C4TTT1819 = 2114; // MM4Map.C4TombOfAThousandTerrors
        public const int SpeedLiquid1F3DM10921 = 2460; // MM4Map.F3DwarfMine1
        public const int SpeedLiquid2F3DM10920 = 2400; // MM4Map.F3DwarfMine1
        public const int SpeedLiquid3F3DM20403 = 2542; // MM4Map.F3DwarfMine2
        public const int SpeedLiquid4F3DM20402 = 2476; // MM4Map.F3DwarfMine2
        public const int SpeedLiquid5E2DM30200 = 1877; // MM4Map.E2DwarfMine3
        public const int SpeedLiquid6E2DM30300 = 1943; // MM4Map.E2DwarfMine3
        public const int SpeedLiquid7D2DM50807 = 1275; // MM4Map.D2DwarfMine5
        public const int SpeedLiquid8D2DM50503 = 1209; // MM4Map.D2DwarfMine5
        public const int SpeedBookD3DTL20410 = 451; // MM4Map.D3DarzogsTowerLevel2
        public const int SpeedPotion1aC4TBL21407 = 2298; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedPotion1bC4TBL21407 = 2307; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedPotion1cC4TBL21407 = 2316; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedPotion2aC4TBL21201 = 2422; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedPotion2bC4TBL21201 = 2431; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedPotion2cC4TBL21201 = 2440; // MM5Map.C4TempleOfBarkLevel2
        public const int SpeedSparrow1F2LSDL52431 = 729; // MM5Map.F2LostSoulsDungeonLevel5
        public const int SpeedSparrow2F2LSDL53110 = 832; // MM5Map.F2LostSoulsDungeonLevel5
        public const int SpeedPlumC4DS0612 = 1353; // MM5Map.C4Surface
        public const int SpeedPotion3aE3S0110 = 4056; // MM5Map.E3Sandcaster
        public const int SpeedPotion3bE3S0110 = 4065; // MM5Map.E3Sandcaster
        public const int SpeedPotion3cE3S0110 = 4017; // MM5Map.E3Sandcaster
        public const int SpeedPotion4aE3S2510 = 4352; // MM5Map.E3Sandcaster
        public const int SpeedPotion4bE3S2510 = 4361; // MM5Map.E3Sandcaster
        public const int SpeedPotion4cE3S2510 = 4313; // MM5Map.E3Sandcaster
        public const int SpeedPotion5aE3S0801 = 4204; // MM5Map.E3Sandcaster
        public const int SpeedPotion5bE3S0801 = 4213; // MM5Map.E3Sandcaster
        public const int SpeedPotion5cE3S0801 = 4165; // MM5Map.E3Sandcaster
        public const int SpeedCauldron1F2L0612 = 1481; // MM5Map.F2Lakeside
        public const int SpeedCauldron2F2L1404 = 1541; // MM5Map.F2Lakeside

        public const int AccuracySkull1B4CIL10011 = 125; // MM4Map.B4CaveOfIllusionLevel1
        public const int AccuracySkull2B4CIL11004 = 720; // MM4Map.B4CaveOfIllusionLevel1
        public const int AccuracySkull3B4CIL20305 = 210; // MM4Map.B4CaveOfIllusionLevel2
        public const int AccuracySkull4B4CIL30401 = 720; // MM4Map.B4CaveOfIllusionLevel3
        public const int AccuracySkull5B4CIL40110 = 295; // MM4Map.B4CaveOfIllusionLevel4
        public const int AccuracySkull6B4CIL41303 = 210; // MM4Map.B4CaveOfIllusionLevel4
        public const int AccuracyJuice1C4TTT0124 = 1916; // MM4Map.C4TombOfAThousandTerrors
        public const int AccuracyJuice2C4TTT0724 = 1982; // MM4Map.C4TombOfAThousandTerrors
        public const int AccuracyJuice3C4TTT0811 = 1850; // MM4Map.C4TombOfAThousandTerrors
        public const int AccuracyJuice4C4TTT0807 = 1784; // MM4Map.C4TombOfAThousandTerrors
        public const int AccuracyLiquid1F3DM11023 = 2280; // MM4Map.F3DwarfMine1
        public const int AccuracyLiquid2F3DM11120 = 2340; // MM4Map.F3DwarfMine1
        public const int AccuracyLiquid3F3DM20401 = 2344; // MM4Map.F3DwarfMine2
        public const int AccuracyLiquid4F3DM20501 = 2410; // MM4Map.F3DwarfMine2
        public const int AccuracyLiquid5D2DM50602 = 1077; // MM4Map.D2DwarfMine5
        public const int AccuracyLiquid6D2DM50702 = 1143; // MM4Map.D2DwarfMine5
        public const int AccuracyBookD3DTL20511 = 500; // MM4Map.D3DarzogsTowerLevel2
        public const int AccuracyPotion1aC4TBL21409 = 2174; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyPotion1bC4TBL21409 = 2183; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyPotion1cC4TBL21409 = 2192; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyPotion2aC4TBL21401 = 2050; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyPotion2bC4TBL21401 = 2059; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyPotion2cC4TBL21401 = 2068; // MM5Map.C4TempleOfBarkLevel2
        public const int AccuracyAlbatross1F2LSDL52630 = 1347; // MM5Map.F2LostSoulsDungeonLevel5
        public const int AccuracyAlbatross2F2LSDL52919 = 1450; // MM5Map.F2LostSoulsDungeonLevel5
        public const int AccuracyBananaE4DS0504 = 834; // MM5Map.E4Surface

        public const int LuckSkull1B4CIL10014 = 40; // MM4Map.B4CaveOfIllusionLevel1
        public const int LuckSkull2B4CIL11304 = 635; // MM4Map.B4CaveOfIllusionLevel1
        public const int LuckSkull3B4CIL31511 = 295; // MM4Map.B4CaveOfIllusionLevel3
        public const int LuckSkull4B4CIL31405 = 210; // MM4Map.B4CaveOfIllusionLevel3
        public const int LuckSkull5B4CIL31503 = 40; // MM4Map.B4CaveOfIllusionLevel3
        public const int LuckSkull6B4CIL31202 = 125; // MM4Map.B4CaveOfIllusionLevel3
        public const int LuckJuice1C4TTT2408 = 2180; // MM4Map.C4TombOfAThousandTerrors
        public const int LuckJuice2C4TTT2608 = 2246; // MM4Map.C4TombOfAThousandTerrors
        public const int LuckLiquid1F3DM10207 = 2580; // MM4Map.F3DwarfMine1
        public const int LuckLiquid2F3DM10206 = 2520; // MM4Map.F3DwarfMine1
        public const int LuckLiquid3F3DM20606 = 2608; // MM4Map.F3DwarfMine2
        public const int LuckLiquid4E2DM40805 = 1451; // MM4Map.E2DwarfMine4
        public const int LuckLiquid5E2DM40804 = 1385; // MM4Map.E2DwarfMine4
        public const int LuckPotionaC4TBL21306 = 2546; // MM5Map.C4TempleOfBarkLevel2
        public const int LuckPotionbC4TBL21306 = 2555; // MM5Map.C4TempleOfBarkLevel2
        public const int LuckPotioncC4TBL21306 = 2564; // MM5Map.C4TempleOfBarkLevel2
        public const int LuckLark1F2LSDL52628 = 214; // MM5Map.F2LostSoulsDungeonLevel5
        public const int LuckLark2F2LSDL52915 = 317; // MM5Map.F2LostSoulsDungeonLevel5
        public const int LuckCoconutF4DS0215 = 394; // MM5Map.F4Surface

        public const int LevelCrystal1D1DC1327 = 802; // MM5Map.D1DragonClouds
        public const int LevelCrystal2D1DC1325 = 787; // MM5Map.D1DragonClouds
        public const int LevelCrystal3D1DC1323 = 772; // MM5Map.D1DragonClouds
        public const int LevelCrystal4D1DC2423 = 847; // MM5Map.D1DragonClouds
        public const int LevelCrystal5D1DC0622 = 727; // MM5Map.D1DragonClouds
        public const int LevelCrystal6D1DC1321 = 757; // MM5Map.D1DragonClouds
        public const int LevelCrystal7D1DC1721 = 817; // MM5Map.D1DragonClouds
        public const int LevelCrystal8D1DC0217 = 907; // MM5Map.D1DragonClouds
        public const int LevelCrystal9D1DC0817 = 742; // MM5Map.D1DragonClouds
        public const int LevelCrystal10D1DC2217 = 832; // MM5Map.D1DragonClouds
        public const int LevelCrystal11D1DC2817 = 877; // MM5Map.D1DragonClouds
        public const int LevelCrystal12D1DC0216 = 892; // MM5Map.D1DragonClouds
        public const int LevelCrystal13D1DC2816 = 862; // MM5Map.D1DragonClouds
        public const int LevelCrystal14D1DC0215 = 937; // MM5Map.D1DragonClouds
        public const int LevelCrystal15D1DC2815 = 982; // MM5Map.D1DragonClouds
        public const int LevelCrystal16D1DC0214 = 952; // MM5Map.D1DragonClouds
        public const int LevelCrystal17D1DC2814 = 997; // MM5Map.D1DragonClouds
        public const int LevelCrystal18D1DC0213 = 967; // MM5Map.D1DragonClouds
        public const int LevelCrystal19D1DC2813 = 1012; // MM5Map.D1DragonClouds
        public const int LevelCrystal20D1DC2410 = 1027; // MM5Map.D1DragonClouds
        public const int LevelCrystal21D1DC0709 = 922; // MM5Map.D1DragonClouds
        public const int LevelEmbalmer1A2SSL30015 = 1304; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer2A2SSL31315 = 1140; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer3A2SSL31515 = 1058; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer4A2SSL30013 = 1222; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer5A2SSL31210 = 976; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer6A2SSL31405 = 894; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer7A2SSL31502 = 812; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer8A2SSL30201 = 648; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer9A2SSL30000 = 566; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelEmbalmer10A2SSL31500 = 730; // MM5Map.A2SouthernSphinxLevel3
        public const int LevelJuice1aA1CAD1015 = 357; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice1bA1CAD1015 = 366; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice2aA1CAD1113 = 201; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice2bA1CAD1113 = 210; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice2cA1CAD1113 = 219; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice3aA1CAD1513 = 45; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice3bA1CAD1513 = 54; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice3cA1CAD1513 = 63; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice4aA1CAD1011 = 496; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice4bA1CAD1011 = 505; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice5aA1CAD1007 = 635; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice5bA1CAD1007 = 644; // MM5Map.A1CastleAlamarDungeon
        public const int LevelJuice6aA1CAL30410 = 31; // MM5Map.A1CastleAlamarLevel3
        public const int LevelJuice6bA1CAL30410 = 48; // MM5Map.A1CastleAlamarLevel3
        public const int LevelFood1B2NS1414 = 2592; // MM5Map.B2NecropolisSewer
        public const int LevelFood2B2NS0911 = 2232; // MM5Map.B2NecropolisSewer
        public const int LevelFood3B2NS1011 = 2412; // MM5Map.B2NecropolisSewer
        public const int LevelFood4B2NS0910 = 2052; // MM5Map.B2NecropolisSewer
        public const int LevelFood5B2NS1102 = 1872; // MM5Map.B2NecropolisSewer
        public const int LevelFood6B2NS0201 = 1512; // MM5Map.B2NecropolisSewer
        public const int LevelFood7B2NS0801 = 1692; // MM5Map.B2NecropolisSewer

        public const int StatsSludge1C4TBL20114 = 1073; // MM5Map.C4TempleOfBarkLevel2
        public const int StatsSludge2C4TBL20214 = 1000; // MM5Map.C4TempleOfBarkLevel2
        public const int StatsSludge3C4TBL20113 = 854; // MM5Map.C4TempleOfBarkLevel2
        public const int StatsSludge4C4TBL20213 = 927; // MM5Map.C4TempleOfBarkLevel2
        public const int StatsJuice1E4TH2031 = 1054; // MM5Map.E4TrollHoles
        public const int StatsJuice2E4TH2131 = 1021; // MM5Map.E4TrollHoles
        public const int StatsJuice3E4TH2431 = 922; // MM5Map.E4TrollHoles
        public const int StatsJuice4E4TH2531 = 955; // MM5Map.E4TrollHoles
        public const int StatsJuice5E4TH2030 = 1153; // MM5Map.E4TrollHoles
        public const int StatsJuice6E4TH2530 = 988; // MM5Map.E4TrollHoles
        public const int StatsJuice7E4TH0722 = 691; // MM5Map.E4TrollHoles
        public const int StatsJuice8E4TH0822 = 724; // MM5Map.E4TrollHoles
        public const int StatsJuice9E4TH1418 = 1120; // MM5Map.E4TrollHoles
        public const int StatsJuice10E4TH1417 = 1087; // MM5Map.E4TrollHoles
        public const int StatsJuice11E4TH2511 = 658; // MM5Map.E4TrollHoles
        public const int StatsJuice12E4TH0110 = 757; // MM5Map.E4TrollHoles
        public const int StatsJuice13E4TH0210 = 790; // MM5Map.E4TrollHoles
        public const int StatsJuice14E4TH0910 = 823; // MM5Map.E4TrollHoles
        public const int StatsJuice15E4TH1010 = 856; // MM5Map.E4TrollHoles
        public const int StatsJuice16E4TH2510 = 625; // MM5Map.E4TrollHoles
        public const int StatsJuice17E4TH1509 = 889; // MM5Map.E4TrollHoles
        public const int StatsJuice18E4TH1609 = 559; // MM5Map.E4TrollHoles
        public const int StatsJuice19E4TH1709 = 592; // MM5Map.E4TrollHoles
        public const int TalkValioA4CS3126 = 1947;
        public const int ReturnValioA4CS3126 = 2013;
        public const int PaladinC2DS1105 = 115; // MM5Map.C2Surface,11,5
        public const int PaladinC2DS1100 = 52; // MM5Map.C2Surface,11,0
        public const int PaladinD2DS0010 = 122; // MM5Map.D2Surface,0,10
        public const int PaladinD2DS0000 = 374; // MM5Map.D2Surface,0,0
        public const int PaladinD2DS0510 = 185; // MM5Map.D2Surface,5,10
        public const int PaladinD2DS0505 = 248; // MM5Map.D2Surface,5,5
        public const int PaladinD2DS0500 = 311; // MM5Map.D2Surface,5,0
        public const int Case1F3V1003 = 3233; //  MM4Map.F3Vertigo,10,3
        public const int Case2F3V1005 = 3656; //  MM4Map.F3Vertigo,10,5
        public const int Case3F3V1103 = 3374; //  MM4Map.F3Vertigo,11,3
        public const int Case4F3V1105 = 3797; //  MM4Map.F3Vertigo,11,5
        public const int Case5F3V1212 = 2810; //  MM4Map.F3Vertigo,12,12
        public const int Case6F3V0812 = 2951; //  MM4Map.F3Vertigo,8,12
        public const int Case7F3V0903 = 3092; //  MM4Map.F3Vertigo,9,3
        public const int Case8F3V0905 = 3515; //  MM4Map.F3Vertigo,9,5
        public const int WordMasterE3DOD12000 = 96; // MM5Map.E3DungeonOfDeathLevel1,20,0
        public const int TTLeverE3DOD31703 = 139; // MM5Map.E3DungeonOfDeathLevel3,17,3

        public const int GoldF3DM10230 = 2640; // MM4Map.F3DwarfMine1,2,30
        public const int GoldF3DM10930 = 2934; // MM4Map.F3DwarfMine1,9,30
        public const int GoldF3DM10525 = 2787; // MM4Map.F3DwarfMine1,5,25
        public const int GoldF3DM11430 = 3386; // MM4Map.F3DwarfMine1,14,30
        public const int Gold2F3DM11430 = 3271; // MM4Map.F3DwarfMine1,14,30
        public const int GoldF3DM10529 = 3196; // MM4Map.F3DwarfMine1,5,29
        public const int Gold2F3DM10529 = 3081; // MM4Map.F3DwarfMine1,5,29
        public const int GoldF3DM20112 = 2743; // MM4Map.F3DwarfMine2,1,12
        public const int GoldF3DM21217 = 3290; // MM4Map.F3DwarfMine2,12,17
        public const int Gold2F3DM21217 = 3230; // MM4Map.F3DwarfMine2,12,17
        public const int GoldF3DM20103 = 2934; // MM4Map.F3DwarfMine2,1,3
        public const int Gold2F3DM20103 = 2874; // MM4Map.F3DwarfMine2,1,3
        public const int GoldF3DM21301 = 3112; // MM4Map.F3DwarfMine2,13,1
        public const int Gold2F3DM21301 = 3052; // MM4Map.F3DwarfMine2,13,1
        public const int GoldF3DM21414 = 3515; // MM4Map.F3DwarfMine2,14,14
        public const int Gold2F3DM21414 = 3468; // MM4Map.F3DwarfMine2,14,14
        public const int Gold3F3DM21414 = 3408; // MM4Map.F3DwarfMine2,14,14
        public const int GoldE2DM31114 = 2427; // MM4Map.E2DwarfMine3,11,14
        public const int GoldE2DM31714 = 2733; // MM4Map.E2DwarfMine3,17,14
        public const int GoldE2DM32010 = 2886; // MM4Map.E2DwarfMine3,20,10
        public const int GoldE2DM31206 = 2580; // MM4Map.E2DwarfMine3,12,6
        public const int GoldE2DM31202 = 2352; // MM4Map.E2DwarfMine3,12,2
        public const int GoldE2DM31414 = 3356; // MM4Map.E2DwarfMine3,14,14
        public const int Gold2E2DM31414 = 3235; // MM4Map.E2DwarfMine3,14,14
        public const int GoldE2DM33014 = 4508; // MM4Map.E2DwarfMine3,30,14
        public const int Gold2E2DM33014 = 4543; // MM4Map.E2DwarfMine3,30,14
        public const int Gold3E2DM33014 = 4387; // MM4Map.E2DwarfMine3,30,14
        public const int GoldE2DM31807 = 3552; // MM4Map.E2DwarfMine3,18,7
        public const int Gold2E2DM31807 = 3587; // MM4Map.E2DwarfMine3,18,7
        public const int Gold3E2DM31807 = 3431; // MM4Map.E2DwarfMine3,18,7
        public const int GoldE2DM32907 = 4269; // MM4Map.E2DwarfMine3,29,7
        public const int Gold2E2DM32907 = 4304; // MM4Map.E2DwarfMine3,29,7
        public const int Gold3E2DM32907 = 4148; // MM4Map.E2DwarfMine3,29,7
        public const int GoldE2DM32902 = 4030; // MM4Map.E2DwarfMine3,29,2
        public const int Gold2E2DM32902 = 4065; // MM4Map.E2DwarfMine3,29,2
        public const int Gold3E2DM32902 = 3909; // MM4Map.E2DwarfMine3,29,2
        public const int GoldE2DM33009 = 4753; // MM4Map.E2DwarfMine3,30,9
        public const int Gold2E2DM33009 = 4788; // MM4Map.E2DwarfMine3,30,9
        public const int Gold3E2DM33009 = 4823; // MM4Map.E2DwarfMine3,30,9
        public const int Gold4E2DM33009 = 4626; // MM4Map.E2DwarfMine3,30,9
        public const int GoldE2DM40414 = 1644; // MM4Map.E2DwarfMine4,4,14
        public const int Gold2E2DM40414 = 1679; // MM4Map.E2DwarfMine4,4,14
        public const int Gold3E2DM40414 = 1517; // MM4Map.E2DwarfMine4,4,14
        public const int GoldE2DM40510 = 1895; // MM4Map.E2DwarfMine4,5,10
        public const int Gold2E2DM40510 = 1930; // MM4Map.E2DwarfMine4,5,10
        public const int Gold3E2DM40510 = 1965; // MM4Map.E2DwarfMine4,5,10
        public const int Gold4E2DM40510 = 1768; // MM4Map.E2DwarfMine4,5,10
        public const int GoldD2DM50114 = 1770; // MM4Map.D2DwarfMine5,1,14
        public const int Gold2D2DM50114 = 1805; // MM4Map.D2DwarfMine5,1,14
        public const int Gold3D2DM50114 = 1649; // MM4Map.D2DwarfMine5,1,14
        public const int GoldD2DM51002 = 1531; // MM4Map.D2DwarfMine5,10,2
        public const int Gold2D2DM51002 = 1566; // MM4Map.D2DwarfMine5,10,2
        public const int Gold3D2DM51002 = 1410; // MM4Map.D2DwarfMine5,10,2
        public const int GoldD2DM51010 = 2009; // MM4Map.D2DwarfMine5,10,10
        public const int Gold2D2DM51010 = 2044; // MM4Map.D2DwarfMine5,10,10
        public const int Gold3D2DM51010 = 2079; // MM4Map.D2DwarfMine5,10,10
        public const int Gold4D2DM51010 = 1888; // MM4Map.D2DwarfMine5,10,10
        public const int GoldDMA3129 = 1630; // MM4Map.DeepMineAlpha,31,29
        public const int Gold2DMA3129 = 1509; // MM4Map.DeepMineAlpha,31,29
        public const int GoldDMA1521 = 1826; // MM4Map.DeepMineAlpha,15,21
        public const int Gold2DMA1521 = 1705; // MM4Map.DeepMineAlpha,15,21
        public const int GoldDMA2915 = 1434; // MM4Map.DeepMineAlpha,29,15
        public const int Gold2DMA2915 = 1313; // MM4Map.DeepMineAlpha,29,15
        public const int GoldDMA2906 = 1042; // MM4Map.DeepMineAlpha,29,6
        public const int Gold2DMA2906 = 915; // MM4Map.DeepMineAlpha,29,6
        public const int GoldDMA0202 = 1238; // MM4Map.DeepMineAlpha,2,2
        public const int Gold2DMA0202 = 1117; // MM4Map.DeepMineAlpha,2,2
        public const int GoldDMA1202 = 2022; // MM4Map.DeepMineAlpha,12,2
        public const int Gold2DMA1202 = 1901; // MM4Map.DeepMineAlpha,12,2
        public const int GoldDMA1931 = 3174; // MM4Map.DeepMineAlpha,19,31
        public const int Gold2DMA1931 = 3209; // MM4Map.DeepMineAlpha,19,31
        public const int Gold3DMA1931 = 3053; // MM4Map.DeepMineAlpha,19,31
        public const int GoldDMA0130 = 3652; // MM4Map.DeepMineAlpha,1,30
        public const int Gold2DMA0130 = 3687; // MM4Map.DeepMineAlpha,1,30
        public const int Gold3DMA0130 = 3531; // MM4Map.DeepMineAlpha,1,30
        public const int GoldDMA1530 = 3413; // MM4Map.DeepMineAlpha,15,30
        public const int Gold2DMA1530 = 3448; // MM4Map.DeepMineAlpha,15,30
        public const int Gold3DMA1530 = 3292; // MM4Map.DeepMineAlpha,15,30
        public const int GoldDMA3020 = 2696; // MM4Map.DeepMineAlpha,30,20
        public const int Gold2DMA3020 = 2731; // MM4Map.DeepMineAlpha,30,20
        public const int Gold3DMA3020 = 2575; // MM4Map.DeepMineAlpha,30,20
        public const int GoldDMA2217 = 2935; // MM4Map.DeepMineAlpha,22,17
        public const int Gold2DMA2217 = 2970; // MM4Map.DeepMineAlpha,22,17
        public const int Gold3DMA2217 = 2814; // MM4Map.DeepMineAlpha,22,17
        public const int GoldDMA0108 = 2457; // MM4Map.DeepMineAlpha,1,8
        public const int Gold2DMA0108 = 2492; // MM4Map.DeepMineAlpha,1,8
        public const int Gold3DMA0108 = 2336; // MM4Map.DeepMineAlpha,1,8
        public const int GoldDMA3003 = 2218; // MM4Map.DeepMineAlpha,30,3
        public const int Gold2DMA3003 = 2253; // MM4Map.DeepMineAlpha,30,3
        public const int Gold3DMA3003 = 2097; // MM4Map.DeepMineAlpha,30,3
        public const int GoldDMA0125 = 3891; // MM4Map.DeepMineAlpha,1,25
        public const int Gold2DMA0125 = 3926; // MM4Map.DeepMineAlpha,1,25
        public const int Gold3DMA0125 = 3961; // MM4Map.DeepMineAlpha,1,25
        public const int Gold4DMA0125 = 3770; // MM4Map.DeepMineAlpha,1,25
        public const int GoldDMA0523 = 4173; // MM4Map.DeepMineAlpha,5,23
        public const int Gold2DMA0523 = 4208; // MM4Map.DeepMineAlpha,5,23
        public const int Gold3DMA0523 = 4243; // MM4Map.DeepMineAlpha,5,23
        public const int Gold4DMA0523 = 4052; // MM4Map.DeepMineAlpha,5,23
        public const int GoldDMA0411 = 4455; // MM4Map.DeepMineAlpha,4,11
        public const int Gold2DMA0411 = 4490; // MM4Map.DeepMineAlpha,4,11
        public const int Gold3DMA0411 = 4525; // MM4Map.DeepMineAlpha,4,11
        public const int Gold4DMA0411 = 4334; // MM4Map.DeepMineAlpha,4,11
        public const int GoldDMK1531 = 1484; // MM4Map.DeepMineKappa,15,31
        public const int Gold2DMK1531 = 1519; // MM4Map.DeepMineKappa,15,31
        public const int Gold3DMK1531 = 1363; // MM4Map.DeepMineKappa,15,31
        public const int GoldDMK1623 = 1006; // MM4Map.DeepMineKappa,16,23
        public const int Gold2DMK1623 = 1041; // MM4Map.DeepMineKappa,16,23
        public const int Gold3DMK1623 = 885; // MM4Map.DeepMineKappa,16,23
        public const int GoldDMK0813 = 770; // MM4Map.DeepMineKappa,8,13
        public const int Gold2DMK0813 = 805; // MM4Map.DeepMineKappa,8,13
        public const int Gold3DMK0813 = 649; // MM4Map.DeepMineKappa,8,13
        public const int GoldDMK2712 = 1245; // MM4Map.DeepMineKappa,27,12
        public const int Gold2DMK2712 = 1280; // MM4Map.DeepMineKappa,27,12
        public const int Gold3DMK2712 = 1124; // MM4Map.DeepMineKappa,27,12
        public const int GoldDMK1526 = 1723; // MM4Map.DeepMineKappa,15,26
        public const int Gold2DMK1526 = 1758; // MM4Map.DeepMineKappa,15,26
        public const int Gold3DMK1526 = 1793; // MM4Map.DeepMineKappa,15,26
        public const int Gold4DMK1526 = 1602; // MM4Map.DeepMineKappa,15,26
        public const int GoldDMK3026 = 2005; // MM4Map.DeepMineKappa,30,26
        public const int Gold2DMK3026 = 2040; // MM4Map.DeepMineKappa,30,26
        public const int Gold3DMK3026 = 2075; // MM4Map.DeepMineKappa,30,26
        public const int Gold4DMK3026 = 1884; // MM4Map.DeepMineKappa,30,26
        public const int GoldDMK2819 = 2287; // MM4Map.DeepMineKappa,28,19
        public const int Gold2DMK2819 = 2322; // MM4Map.DeepMineKappa,28,19
        public const int Gold3DMK2819 = 2357; // MM4Map.DeepMineKappa,28,19
        public const int Gold4DMK2819 = 2166; // MM4Map.DeepMineKappa,28,19
        public const int GoldDMK0414 = 2569; // MM4Map.DeepMineKappa,4,14
        public const int Gold2DMK0414 = 2604; // MM4Map.DeepMineKappa,4,14
        public const int Gold3DMK0414 = 2639; // MM4Map.DeepMineKappa,4,14
        public const int Gold4DMK0414 = 2448; // MM4Map.DeepMineKappa,4,14
        public const int GoldDMK0231 = 2851; // MM4Map.DeepMineKappa,2,31
        public const int Gold2DMK0231 = 2886; // MM4Map.DeepMineKappa,2,31
        public const int Gold3DMK0231 = 2921; // MM4Map.DeepMineKappa,2,31
        public const int Gold4DMK0231 = 2956; // MM4Map.DeepMineKappa,2,31
        public const int Gold5DMK0231 = 2730; // MM4Map.DeepMineKappa,2,31
        public const int GoldDMT3107 = 874; // MM4Map.DeepMineTheta,31,7
        public const int Gold2DMT3107 = 909; // MM4Map.DeepMineTheta,31,7
        public const int Gold3DMT3107 = 944; // MM4Map.DeepMineTheta,31,7
        public const int Gold4DMT3107 = 979; // MM4Map.DeepMineTheta,31,7
        public const int Gold5DMT3107 = 753; // MM4Map.DeepMineTheta,31,7
        public const int GoldDMO3024 = 657; // MM4Map.DeepMineOmega,30,24
        public const int Gold2DMO3024 = 692; // MM4Map.DeepMineOmega,30,24
        public const int Gold3DMO3024 = 727; // MM4Map.DeepMineOmega,30,24
        public const int Gold4DMO3024 = 762; // MM4Map.DeepMineOmega,30,24
        public const int Gold5DMO3024 = 536; // MM4Map.DeepMineOmega,30,24
        public const int GoldDMO0515 = 982; // MM4Map.DeepMineOmega,5,15
        public const int Gold2DMO0515 = 1017; // MM4Map.DeepMineOmega,5,15
        public const int Gold3DMO0515 = 1052; // MM4Map.DeepMineOmega,5,15
        public const int Gold4DMO0515 = 1087; // MM4Map.DeepMineOmega,5,15
        public const int Gold5DMO0515 = 861; // MM4Map.DeepMineOmega,5,15
        public const int GoldDMO2114 = 1632; // MM4Map.DeepMineOmega,21,14
        public const int Gold2DMO2114 = 1667; // MM4Map.DeepMineOmega,21,14
        public const int Gold3DMO2114 = 1702; // MM4Map.DeepMineOmega,21,14
        public const int Gold4DMO2114 = 1737; // MM4Map.DeepMineOmega,21,14
        public const int Gold5DMO2114 = 1511; // MM4Map.DeepMineOmega,21,14
        public const int GoldDMO0506 = 1307; // MM4Map.DeepMineOmega,5,6
        public const int Gold2DMO0506 = 1342; // MM4Map.DeepMineOmega,5,6
        public const int Gold3DMO0506 = 1377; // MM4Map.DeepMineOmega,5,6
        public const int Gold4DMO0506 = 1412; // MM4Map.DeepMineOmega,5,6
        public const int Gold5DMO0506 = 1186; // MM4Map.DeepMineOmega,5,6
        public const int DestroyA2DS0702 = 215; // MM5Map.A2Surface,7,2
        public const int DestroyA3CS0814 = 130; // MM4Map.A3Surface,8,14
        public const int DestroyA4CS1008 = 254; // MM4Map.A4Surface,10,8
        public const int DestroyB2DS0002 = 555; // MM5Map.B2Surface,0,2
        public const int DestroyB3DS1310 = 1579; // MM5Map.B3Surface,13,10
        public const int DestroyB4CS0207 = 161; // MM4Map.B4Surface,2,7
        public const int DestroyB4CS1012 = 64; // MM4Map.B4Surface,10,12
        public const int DestroyC1DS0911 = 101; // MM5Map.C1Surface,9,11
        public const int DestroyC2CS0108 = 164; // MM4Map.C2Surface,1,8
        public const int DestroyC2CS0500 = 50; // MM4Map.C2Surface,5,0
        public const int DestroyC4CS0111 = 461; // MM4Map.C4Surface,1,11
        public const int DestroyD1DS0012 = 827; // MM5Map.D1Surface,0,12
        public const int DestroyD3CX0505 = 920; // MM4Map.D3CloudsOfXeen,5,5
        public const int DestroyD3CX2505 = 840; // MM4Map.D3CloudsOfXeen,25,5
        public const int DestroyD3CX2729 = 760; // MM4Map.D3CloudsOfXeen,27,29
        public const int DestroyD3CX2827 = 600; // MM4Map.D3CloudsOfXeen,28,27
        public const int DestroyD3CX2830 = 680; // MM4Map.D3CloudsOfXeen,28,30
        public const int DestroyD3CX2928 = 520; // MM4Map.D3CloudsOfXeen,29,28
        public const int DestroyD3CX3030 = 440; // MM4Map.D3CloudsOfXeen,30,30
        public const int DestroyD3DS0908 = 776; // MM5Map.D3Surface,9,8
        public const int DestroyD3DS0307 = 702; // MM5Map.D3Surface,3,7
        public const int DestroyE1VCL10015 = 171; // MM4Map.E1VolcanoCaveLevel1,0,15
        public const int DestroyE1VCL10909 = 294; // MM4Map.E1VolcanoCaveLevel1,9,9
        public const int DestroyE2CS0902 = 159; // MM4Map.E2Surface,9,2
        public const int DestroyE3CS1413 = 228; // MM4Map.E3Surface,14,13
        public const int DestroyE3DDL20420 = 1711; // MM5Map.E3DungeonOfDeathLevel2,4,20
        public const int DestroyE3DDL20620 = 1775; // MM5Map.E3DungeonOfDeathLevel2,6,20
        public const int DestroyE3DDL20820 = 1839; // MM5Map.E3DungeonOfDeathLevel2,8,20
        public const int DestroyE3DDL40203 = 74; // MM5Map.E3DungeonOfDeathLevel4,2,3
        public const int DestroyE3DDL40329 = 145; // MM5Map.E3DungeonOfDeathLevel4,3,29
        public const int DestroyE3DDL42703 = 287; // MM5Map.E3DungeonOfDeathLevel4,27,3
        public const int DestroyE3DDL42829 = 216; // MM5Map.E3DungeonOfDeathLevel4,28,29
        public const int DestroyE4ATY0206 = 3411; // MM4Map.E4AncientTempleOfYak,2,6
        public const int DestroyE4ATY0217 = 3477; // MM4Map.E4AncientTempleOfYak,2,17
        public const int DestroyE4ATY2507 = 3345; // MM4Map.E4AncientTempleOfYak,25,7
        public const int DestroyE4ATY2725 = 3543; // MM4Map.E4AncientTempleOfYak,27,25
        public const int DestroyE4CS0914 = 98; // MM4Map.E4Surface,9,14
        public const int DestroyF1DS1000 = 224; // MM5Map.F1Surface,10,0
        public const int DestroyF2CS1205 = 57; // MM4Map.F2Surface,12,5
        public const int DestroyF2CS1303 = 155; // MM4Map.F2Surface,13,3
        public const int DestroyF3CS1214 = 1229; // MM4Map.F3Surface,12,14
        public const int DestroyF4WC0419 = 1951; // MM4Map.F4WitchClouds,4,19
        public const int DestroyF4WC0427 = 2058; // MM4Map.F4WitchClouds,4,27
        public const int DestroyF4WC0807 = 1844; // MM4Map.F4WitchClouds,8,7
        public const int DestroyF4WC2228 = 1630; // MM4Map.F4WitchClouds,22,28
        public const int DestroyF4WC2720 = 1737; // MM4Map.F4WitchClouds,27,20
        public const int LampA2SA21214 = 215; // MM5Map.A2SkyroadA2,12,14
        public const int LampB1SB10507 = 192; // MM5Map.B1SkyroadB1,5,7
        public const int LampB2SB20514 = 12; // MM5Map.B2SkyroadB2,5,14
        public const int LampC3SC30700 = 5; // MM5Map.C3SkyroadC3,7,0
        public const int LampE1SE11201 = 102; // MM5Map.E1SkyroadE1,12,1
        public const int LampE2SE20812 = 217; // MM5Map.E2SkyroadE2,8,12
        public const int LampE2SE20308 = 5; // MM5Map.E2SkyroadE2,3,8
        public const int LampE2SE21208 = 323; // MM5Map.E2SkyroadE2,12,8
        public const int LampE2SE20803 = 111; // MM5Map.E2SkyroadE2,8,3
        public const int LampF1SF10303 = 215; // MM5Map.F1SkyroadF1,3,3
        public const int LampF2SF20112 = 298; // MM5Map.F2SkyroadF2,1,12
        public const int LampB2DS1309 = 144; // MM5Map.B2Surface,13,9
        public const int LampB2DS1202 = 250; // MM5Map.B2Surface,12,2
        public const int LampC2DS0315 = 437; // MM5Map.C2Surface,3,15
        public const int LampC2DS0611 = 225; // MM5Map.C2Surface,6,11
        public const int LampC2DS0206 = 331; // MM5Map.C2Surface,2,6
        public const int LampD2DS0015 = 390; // MM5Map.D2Surface,0,15
        public const int LampD3DS0712 = 551; // MM5Map.D3Surface,7,12
        public const int L7ItemE1DC3100 = 2104; // MM4Map.E1DragonCave,31,0
        public const int L7ItemA2SSL10518 = 1408; // MM5Map.A2SouthernSphinxLevel1,5,18
        public const int L7ItemA2SSL10516 = 1231; // MM5Map.A2SouthernSphinxLevel1,5,16
        public const int L7ItemA2SSL10514 = 1054; // MM5Map.A2SouthernSphinxLevel1,5,14
        public const int L7ItemA2SSL10201 = 346; // MM5Map.A2SouthernSphinxLevel1,2,1
        public const int L7ItemA2SSL10401 = 523; // MM5Map.A2SouthernSphinxLevel1,4,1
        public const int L7ItemA2SSL11001 = 700; // MM5Map.A2SouthernSphinxLevel1,10,1
        public const int L7ItemA2SSL11201 = 877; // MM5Map.A2SouthernSphinxLevel1,12,1
        public const int L7ItemA2SSL30615 = 1464; // MM5Map.A2SouthernSphinxLevel3,6,15
        public const int L7ItemE3DDL22613 = 142; // MM5Map.E3DungeonOfDeathLevel2,26,13
        public const int L7ItemE3DDL23013 = 180; // MM5Map.E3DungeonOfDeathLevel2,30,13
        public const int L7ItemE3DDL20704 = 683; // MM5Map.E3DungeonOfDeathLevel2,7,4
        public const int L7ItemE3DDL20904 = 807; // MM5Map.E3DungeonOfDeathLevel2,9,4
        public const int L7ItemE3DDL21104 = 931; // MM5Map.E3DungeonOfDeathLevel2,11,4
        public const int L7ItemE3DDL21304 = 1055; // MM5Map.E3DungeonOfDeathLevel2,13,4
        public const int L7ItemE3DDL21504 = 1179; // MM5Map.E3DungeonOfDeathLevel2,15,4
        public const int L7ItemD1DTL40509 = 519; // MM5Map.D1DragonTowerLevel4,5,9
        public const int L7ItemD1DTL40909 = 469; // MM5Map.D1DragonTowerLevel4,9,9
        public const int L7ItemD1DTL40308 = 419; // MM5Map.D1DragonTowerLevel4,3,8
        public const int L7ItemD1DTL41108 = 440; // MM5Map.D1DragonTowerLevel4,11,8
        public const int L7ItemD1DTL40706 = 569; // MM5Map.D1DragonTowerLevel4,7,6
        public const int L7ItemC4TBL51611 = 2423; // MM5Map.C4TempleOfBarkLevel5,16,11
        public const int L7ItemD2TGPL12315 = 224; // MM5Map.D2TheGreatPyramidLevel1,23,15
        public const int L7ItemE4TH0724 = 3246; // MM5Map.E4TrollHoles,7,24
        public const int L7ItemB2N0105 = 2556; // MM5Map.B2Necropolis,1,5
        public const int L7ItemB2N0205 = 2672; // MM5Map.B2Necropolis,2,5
        public const int L7ItemB2N1204 = 1976; // MM5Map.B2Necropolis,12,4
        public const int L7ItemB2N1404 = 2440; // MM5Map.B2Necropolis,14,4
        public const int L7ItemB2N1203 = 1860; // MM5Map.B2Necropolis,12,3
        public const int L7ItemB2N1403 = 2324; // MM5Map.B2Necropolis,14,3
        public const int L7ItemB2N1002 = 1744; // MM5Map.B2Necropolis,10,2
        public const int L7ItemB2N1402 = 2208; // MM5Map.B2Necropolis,14,2
        public const int L7ItemB2N1401 = 2092; // MM5Map.B2Necropolis,14,1
        public const int L7ItemB2NS0610 = 1093; // MM5Map.B2NecropolisSewer,6,10
        public const int L7ItemB2NS0106 = 2707; // MM5Map.B2NecropolisSewer,1,6
        public const int L7ItemB2NS0102 = 861; // MM5Map.B2NecropolisSewer,1,2
        public const int L7ItemB2NS1001 = 977; // MM5Map.B2NecropolisSewer,10,1
        public const int OpenCoffinD4N0114 = 4221; // MM4Map.D4Nightshadow,1,14
        public const int LeverD2TGPL12520 = 1770; // MM5Map.D2TheGreatPyramidLevel1,25,20
        public const int TreasureD2TGPL12315 = 224; // MM5Map.D2TheGreatPyramidLevel1,23,15

        public const int TreeD4N0111 = 846; // MM4Map.D4Nightshadow,1,11
        public const int TreeD4N0210 = 881; // MM4Map.D4Nightshadow,2,10
        public const int TreeD4N1008 = 916; // MM4Map.D4Nightshadow,10,8
        public const int TreeD4N0107 = 811; // MM4Map.D4Nightshadow,1,7
        public const int TreeD4N0902 = 741; // MM4Map.D4Nightshadow,9,2
        public const int TreeD4N1102 = 776; // MM4Map.D4Nightshadow,11,2
        public const int TreeA4C1328 = 6833; // MM5Map.A4Castleview,13,28
        public const int TreeA4C1728 = 6923; // MM5Map.A4Castleview,17,28
        public const int TreeA4C1326 = 6848; // MM5Map.A4Castleview,13,26
        public const int TreeA4C1726 = 6908; // MM5Map.A4Castleview,17,26
        public const int TreeA4C0625 = 6818; // MM5Map.A4Castleview,6,25
        public const int TreeA4C1319 = 6863; // MM5Map.A4Castleview,13,19
        public const int TreeA4C1617 = 6893; // MM5Map.A4Castleview,16,17
        public const int TreeA4C0814 = 6773; // MM5Map.A4Castleview,8,14
        public const int TreeA4C0914 = 6788; // MM5Map.A4Castleview,9,14
        public const int TreeA4C1014 = 6803; // MM5Map.A4Castleview,10,14
        public const int TreeA4C1408 = 6878; // MM5Map.A4Castleview,14,8
        public const int TreeA4C1608 = 6938; // MM5Map.A4Castleview,16,8
        public const int TreeC3R3028 = 5000; // MM4Map.C3Rivercity,30,28
        public const int TreeC3R0126 = 4828; // MM4Map.C3Rivercity,1,26
        public const int TreeC3R0326 = 4871; // MM4Map.C3Rivercity,3,26
        public const int TreeC3R3023 = 4957; // MM4Map.C3Rivercity,30,23
        public const int TreeC3R2720 = 4914; // MM4Map.C3Rivercity,27,20
        public const int TreeC3R0908 = 4785; // MM4Map.C3Rivercity,9,8
        public const int TreeC3R1206 = 4742; // MM4Map.C3Rivercity,12,6
        public const int TreeC3R0905 = 4656; // MM4Map.C3Rivercity,9,5
        public const int TreeC3R1205 = 4699; // MM4Map.C3Rivercity,12,5
        public const int TreeC3R2105 = 5043; // MM4Map.C3Rivercity,21,5
        public const int TreeC3R1303 = 4570; // MM4Map.C3Rivercity,13,3
        public const int TreeC3R1403 = 4613; // MM4Map.C3Rivercity,14,3
        public const int TreeC3R0902 = 4527; // MM4Map.C3Rivercity,9,2

        public const int Feed1C4TBL50000 = 557; // MM5Map.C4TempleOfBarkLevel5,0,0
        public const int Feed2C4TBL50000 = 566; // MM5Map.C4TempleOfBarkLevel5,0,0
        public const int Feed3C4TBL50000 = 575; // MM5Map.C4TempleOfBarkLevel5,0,0
        public const int Feed4C4TBL50000 = 584; // MM5Map.C4TempleOfBarkLevel5,0,0
        public const int Feed1C4TBL50700 = 739; // MM5Map.C4TempleOfBarkLevel5,7,0
        public const int Feed2C4TBL50700 = 748; // MM5Map.C4TempleOfBarkLevel5,7,0
        public const int Feed3C4TBL50700 = 757; // MM5Map.C4TempleOfBarkLevel5,7,0
        public const int Feed4C4TBL50700 = 766; // MM5Map.C4TempleOfBarkLevel5,7,0
        public const int Feed1C4TBL52200 = 921; // MM5Map.C4TempleOfBarkLevel5,22,0
        public const int Feed2C4TBL52200 = 930; // MM5Map.C4TempleOfBarkLevel5,22,0
        public const int Feed3C4TBL52200 = 939; // MM5Map.C4TempleOfBarkLevel5,22,0
        public const int Feed4C4TBL52200 = 948; // MM5Map.C4TempleOfBarkLevel5,22,0
        public const int Feed1C4TBL53100 = 1103; // MM5Map.C4TempleOfBarkLevel5,31,0
        public const int Feed2C4TBL53100 = 1112; // MM5Map.C4TempleOfBarkLevel5,31,0
        public const int Feed3C4TBL53100 = 1121; // MM5Map.C4TempleOfBarkLevel5,31,0
        public const int Feed4C4TBL53100 = 1130; // MM5Map.C4TempleOfBarkLevel5,31,0
        public const int Feed1C4TBL50028 = 1506; // MM5Map.C4TempleOfBarkLevel5,0,28
        public const int Feed2C4TBL50028 = 1515; // MM5Map.C4TempleOfBarkLevel5,0,28
        public const int Feed3C4TBL50028 = 1524; // MM5Map.C4TempleOfBarkLevel5,0,28
        public const int Feed4C4TBL50028 = 1533; // MM5Map.C4TempleOfBarkLevel5,0,28
        public const int Feed1C4TBL50121 = 1316; // MM5Map.C4TempleOfBarkLevel5,1,21
        public const int Feed2C4TBL50121 = 1325; // MM5Map.C4TempleOfBarkLevel5,1,21
        public const int Feed3C4TBL50121 = 1334; // MM5Map.C4TempleOfBarkLevel5,1,21
        public const int Feed4C4TBL50121 = 1343; // MM5Map.C4TempleOfBarkLevel5,1,21
        public const int Feed1C4TBL53130 = 1696; // MM5Map.C4TempleOfBarkLevel5,31,30
        public const int Feed2C4TBL53130 = 1705; // MM5Map.C4TempleOfBarkLevel5,31,30
        public const int Feed3C4TBL53130 = 1714; // MM5Map.C4TempleOfBarkLevel5,31,30
        public const int Feed4C4TBL53130 = 1723; // MM5Map.C4TempleOfBarkLevel5,31,30
        public const int Feed1C4TBL53121 = 1886; // MM5Map.C4TempleOfBarkLevel5,31,21
        public const int Feed2C4TBL53121 = 1895; // MM5Map.C4TempleOfBarkLevel5,31,21
        public const int Feed3C4TBL53121 = 1904; // MM5Map.C4TempleOfBarkLevel5,31,21
        public const int Feed4C4TBL53121 = 1913; // MM5Map.C4TempleOfBarkLevel5,31,21
        public const int TreasureC4TBL51611 = 2423; // MM5Map.C4TempleOfBarkLevel5,16,11
        public const int TreasureC4TBL50124 = 2307; // MM5Map.C4TempleOfBarkLevel5,1,24
        public const int TreasureC4TBL53025 = 2364; // MM5Map.C4TempleOfBarkLevel5,30,25
        public const int HelpedDerekF3CS0405 = 127; // MM4Map.F3Surface,4,5

        public const int PayGoldF2LSDL41402 = 1934; // MM5Map.F2LostSoulsDungeonLevel4,14,2

        public static uint[] WaterDragons = new uint[] { 0, 1, 2 };
        public static uint[] SpiritBones = new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        public static uint[] PolterFool = new uint[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
        public static uint[] GhostRider = new uint[] { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };
        public static uint[] YakCreditOffsets = new uint[] { 2204, 2171, 2237, 2270, 2303, 2336, 2369, 2402 };
        public static uint[] TombCreditOffsets = new uint[] { 1016, 917, 983, 950, 884, 1082, 851, 1049 };
        public static uint[] GolemCreditOffsets = new uint[] { 563, 596, 497, 530, 464, 398, 431, 365, 299, 332, 233, 266, 167, 200 };

        public class MapAndOffset
        {
            public MM4Map Map;
            public uint Offset;

            public MapAndOffset(MM4Map map, uint offset)
            {
                Map = map;
                Offset = offset;
            }
        }

        public static MapAndOffset[] YakMegaCredits = CreateMegaCredits(MM45.Spots.YakMegaCredits, YakCreditOffsets);
        public static MapAndOffset[] TombMegaCredits = CreateMegaCredits(MM45.Spots.TombMegaCredits, TombCreditOffsets);
        public static MapAndOffset[] GolemMegaCredits = CreateMegaCredits(MM45.Spots.GolemMegaCredits, GolemCreditOffsets);

        private static MapAndOffset[] CreateMegaCredits(MapXY[] maps, uint[] offsets)
        {
            MapAndOffset[] credits = new MapAndOffset[maps.Length];
            for (int i = 0; i < credits.Length; i++)
                credits[i] = new MapAndOffset((MM4Map)maps[i].Map, offsets[i]);
            return credits;
        }
    }

    public class MM4FileOffsets : FileOffsets
    {
        public const int Characters = 0x989;
        public const int CartographyOffset = 860;
        public const int CurrentParty = 0x3307;

        public static uint MainMap(MM4Map map) { return 0x29318 + (0x37C * (uint)map); }
        public static uint SideMap(MM4Map map) { return 0x29318 + (0x37C * ((uint)map - 15)); }
        public static uint[] Offsets(params MM4Map[] maps)
        {
            uint[] uints = new uint[maps.Length];
            for (int i = 0; i < maps.Length; i++)
                uints[i] = (i == 0 ? MainMap(maps[i]) : SideMap(maps[i]));
            return uints;
        }

        public static uint[] StaticMaps = new uint[] {
            0, 0, 0, 0,                           // Map 0: Invalid
            0x29694, 0, 0, 0,                     // Map 1: A-1, Cloudside Surface
            0x29A10, 0, 0, 0,                     // Map 2: A-2, Cloudside Surface
            0x29D8C, 0, 0, 0,                     // Map 3: A-3, Cloudside Surface
            0x2A108, 0, 0, 0,                     // Map 4: A-4, Cloudside Surface
            0x2A484, 0, 0, 0,                     // Map 5: B-1, Cloudside Surface
            0x2A800, 0, 0, 0,                     // Map 6: B-2, Cloudside Surface
            0x2AB7C, 0, 0, 0,                     // Map 7: B-3, Cloudside Surface
            0x2AEF8, 0, 0, 0,                     // Map 8: B-4, Cloudside Surface
            0x2B274, 0, 0, 0,                     // Map 9: C-1, Cloudside Surface
            0x2B5F0, 0, 0, 0,                     // Map 10: C-2, Cloudside Surface
            0x2B96C, 0, 0, 0,                     // Map 11: C-3, Cloudside Surface
            0x2BCE8, 0, 0, 0,                     // Map 12: C-4, Cloudside Surface
            0x2C064, 0, 0, 0,                     // Map 13: D-1, Cloudside Surface
            0x2C3E0, 0, 0, 0,                     // Map 14: D-2, Cloudside Surface
            0x2C75C, 0, 0, 0,                     // Map 15: D-3, Cloudside Surface
            0x2CAD8, 0, 0, 0,                     // Map 16: D-4, Cloudside Surface
            0x2CE54, 0, 0, 0,                     // Map 17: E-1, Cloudside Surface
            0x2D1D0, 0, 0, 0,                     // Map 18: E-2, Cloudside Surface
            0x2D54C, 0, 0, 0,                     // Map 19: E-3, Cloudside Surface
            0x2D8C8, 0, 0, 0,                     // Map 20: E-4, Cloudside Surface
            0x2DC44, 0, 0, 0,                     // Map 21: F-1, Cloudside Surface
            0x2DFC0, 0, 0, 0,                     // Map 22: F-2, Cloudside Surface
            0x2E33C, 0, 0, 0,                     // Map 23: F-3, Cloudside Surface
            0x2E6B8, 0, 0, 0,                     // Map 24: F-4, Cloudside Surface
            0x2EA34, 0x3BEC0, 0x3BB44, 0x3C23C,   // Map 25: F-4, Witch Clouds
            0x2EDB0, 0x3C934, 0x3C5B8, 0x3CCB0,   // Map 26: C-4, High Magic Clouds
            0x2F12C, 0x3D3A8, 0x3D02C, 0x3D724,   // Map 27: D-3, Clouds of Xeen
            0x2F4A8, 0x3DE1C, 0x3DAA0, 0x3E198,   // Map 28: F-3, Vertigo
            0x2F824, 0, 0, 0,                     // Map 29: D-4, Nightshadow
            0x2FBA0, 0x3E890, 0x3E514, 0x3EC0C,   // Map 30: C-3, Rivercity
            0x2FF1C, 0, 0, 0,                     // Map 31: C-2, Asp
            0x30298, 0, 0, 0,                     // Map 32: A-3, Winterkill
            0x30614, 0, 0, 0,                     // Map 33: F-3, Dwarf Mine 1
            0x30990, 0, 0, 0,                     // Map 34: F-3, Dwarf Mine 2
            0x30D0C, 0, 0, 0,                     // Map 35: E-2, Dwarf Mine 3
            0x31088, 0, 0, 0,                     // Map 36: E-2, Dwarf Mine 4
            0x31404, 0, 0, 0,                     // Map 37: D-2, Dwarf Mine 5
            0x31780, 0x3FD78, 0x3F9FC, 0x400F4,   // Map 38: Deep Mine Alpha
            0x31AFC, 0x407EC, 0x40470, 0x40B68,   // Map 39: Deep Mine Theta
            0x31E78, 0x41260, 0x40EE4, 0x415DC,   // Map 40: Deep Mine Kappa
            0x321F4, 0x41CD4, 0x41958, 0x42050,   // Map 41: Deep Mine Omega
            0x32570, 0, 0, 0,                     // Map 42: B-4, Cave of Illusion Level 1
            0x328EC, 0, 0, 0,                     // Map 43: B-4, Cave of Illusion Level 2
            0x32C68, 0, 0, 0,                     // Map 44: B-4, Cave of Illusion Level 3
            0x32FE4, 0, 0, 0,                     // Map 45: B-4, Cave of Illusion Level 4
            0x33360, 0, 0, 0,                     // Map 46: E-1, Volcano Cave Level 1
            0x336DC, 0, 0, 0,                     // Map 47: E-1, Volcano Cave Level 2
            0x33A58, 0, 0, 0,                     // Map 48: E-1, Volcano Cave Level 3
            0x33DD4, 0, 0, 0,                     // Map 49: E-1, Shangri-La
            0x34150, 0x42748, 0x423CC, 0x42AC4,   // Map 50: E-1, Dragon Cave
            0x344CC, 0, 0, 0,                     // Map 51: F-4, Witch Tower Level 1
            0x34848, 0, 0, 0,                     // Map 52: F-4, Witch Tower Level 2
            0x34BC4, 0, 0, 0,                     // Map 53: F-4, Witch Tower Level 3
            0x34F40, 0, 0, 0,                     // Map 54: F-4, Witch Tower Level 4
            0x352BC, 0, 0, 0,                     // Map 55: C-3, Tower of High Magic Level 1
            0x35638, 0, 0, 0,                     // Map 56: C-3, Tower of High Magic Level 2
            0x359B4, 0, 0, 0,                     // Map 57: C-3, Tower of High Magic Level 3
            0x35D30, 0, 0, 0,                     // Map 58: C-3, Tower of High Magic Level 4
            0x360AC, 0, 0, 0,                     // Map 59: D-3, Darzog's Tower Level 1
            0x36428, 0, 0, 0,                     // Map 60: D-3, Darzog's Tower Level 2
            0x367A4, 0, 0, 0,                     // Map 61: D-3, Darzog's Tower Level 3
            0x36B20, 0, 0, 0,                     // Map 62: D-3, Darzog's Tower Level 4
            0x36E9C, 0, 0, 0,                     // Map 63: D-2, Burlock Dungeon
            0x37218, 0, 0, 0,                     // Map 64: D-2, Castle Burlock Level 1
            0x37594, 0, 0, 0,                     // Map 65: D-2, Castle Burlock Level 2
            0x37910, 0, 0, 0,                     // Map 66: D-2, Castle Burlock Level 3
            0x37C8C, 0, 0, 0,                     // Map 67: A-1, Basenji Dungeon
            0x38008, 0, 0, 0,                     // Map 68: A-1, Castle Basenji Level 1
            0x38384, 0, 0, 0,                     // Map 69: A-1, Castle Basenji Level 2
            0x38700, 0, 0, 0,                     // Map 70: A-1, Castle Basenji Level 3
            0x38A7C, 0, 0, 0,                     // Map 71: C-4, Newcastle Dungeon
            0x38DF8, 0, 0, 0,                     // Map 72: C-4, Newcastle Foundation
            0x39174, 0, 0, 0,                     // Map 73: C-4, Newcastle Level 1
            0x394F0, 0, 0, 0,                     // Map 74: C-4, Newcastle Level 2
            0x3986C, 0, 0, 0,                     // Map 75: D-3, Xeen's Castle Level 1
            0x39BE8, 0, 0, 0,                     // Map 76: D-3, Xeen's Castle Level 2
            0x39F64, 0, 0, 0,                     // Map 77: D-3, Xeen's Castle Level 3
            0x3A2E0, 0, 0, 0,                     // Map 78: D-3, Xeen's Castle Level 4
            0x3A65C, 0x431BC, 0x42E40, 0x43538,   // Map 79: E-4, Ancient Temple of Yak
            0x3A9D8, 0x43C30, 0x438B4, 0x43FAC,   // Map 80: C-4, Tomb of a Thousand Terrors
            0x3AD54, 0x446A4, 0x44328, 0x44A20,   // Map 81: B-4, Golem Dungeon
            0x3B0D0, 0x44D9C, 0, 0,               // Map 82: B-1, Sphinx Body
            0x3B44C, 0, 0, 0,                     // Map 83: B-1, Sphinx Head
            0x3B7C8, 0, 0, 0,                     // Map 84: B-1, Sphinx Dungeon
            0x3BB44, 0, 0, 0,                     // Map 85: B-2, The Warzone
        };

        private static uint[] m_Scripts = new uint[] { 0,
            0x08821, 0x08A13, 0x08A4A, 0x08D7C, 0x08EBD, 0x08EC4, 0x08F80, 0x091F9, 0x09300, 0x0938D,  // Maps 1-10
            0x09841, 0x09CFE, 0x09FEC, 0x09FF3, 0x0A0CF, 0x0A86D, 0x0AA8A, 0x0AAD8, 0x0AC1C, 0x0AEB0,  // Maps 11-20
            0x0AF30, 0x0AF85, 0x0B049, 0x0B5E9, 0x0BA4A, 0x0C462, 0x0CD5C, 0x0D9E6, 0x0F668, 0x107F6,  // Maps 21-30
            0x11FDD, 0x12898, 0x139F2, 0x1488B, 0x15725, 0x16B48, 0x1741B, 0x17D5B, 0x19047, 0x19566,  // Maps 31-40
            0x1A1F0, 0x1A976, 0x1B12A, 0x1B54E, 0x1BC20, 0x1CA58, 0x1CE76, 0x1D0D0, 0x1D1BA, 0x1D499,  // Maps 41-50
            0x1DCEA, 0x1DFC8, 0x1E34E, 0x1E93B, 0x1ED18, 0x1F05C, 0x1F57C, 0x1FA52, 0x1FD1E, 0x1FF19,  // Maps 51-60
            0x20211, 0x203A9, 0x2060A, 0x20758, 0x2101E, 0x211DB, 0x21459, 0x21B7F, 0x221DF, 0x2262B,  // Maps 61-70
            0x22B84, 0x22E1F, 0x23305, 0x2354C, 0x235C1, 0x23A3B, 0x23D1A, 0x24038, 0x242A2, 0x2649E,  // Maps 71-80
            0x27615, 0x28718, 0x28EC5, 0x291D5, 0x294F4                                                // Maps 81-85
        };

        private static uint[] m_Monsters = new uint[] { 0,
            0x03681, 0x036F9, 0x03795, 0x03865, 0x038E1, 0x03951, 0x039DD, 0x03AA1, 0x03B25, 0x03BF1, // Maps 1-10
            0x03CB1, 0x03D65, 0x03E05, 0x03E85, 0x03F2D, 0x03FC9, 0x04061, 0x040DD, 0x0417D, 0x04249, // Maps 11-20
            0x04305, 0x04399, 0x04499, 0x04565, 0x04665, 0x04789, 0x048A5, 0x04B8D, 0x04D4D, 0x05069, // Maps 21-30
            0x052C1, 0x05419, 0x05635, 0x0580D, 0x0599D, 0x05B59, 0x05C75, 0x05D69, 0x05F29, 0x06055, // Maps 31-40
            0x06199, 0x06295, 0x06379, 0x06425, 0x0652D, 0x0660D, 0x06685, 0x066FD, 0x067D5, 0x068FD, // Maps 41-50
            0x069E1, 0x06A81, 0x06B45, 0x06BE9, 0x06C99, 0x06D41, 0x06E05, 0x06EBD, 0x06F75, 0x07019, // Maps 51-60
            0x070B5, 0x07135, 0x07209, 0x072E9, 0x073FD, 0x07489, 0x0756D, 0x0767D, 0x0776D, 0x07831, // Maps 61-70
            0x078F9, 0x079DD, 0x07A91, 0x07B09, 0x07B65, 0x07BED, 0x07C59, 0x07CC5, 0x07EA1, 0x08201, // Maps 71-80
            0x083C5, 0x085AD, 0x086A1, 0x08715, 0x087c5 // Maps 81-85
        };

        public override uint[] Maps { get { return StaticMaps; } }
        public override uint[] Scripts { get { return m_Scripts; } }
        public override uint[] Monsters { get { return m_Monsters; } }

        public static uint[] MapOffsets(MM4Map map)
        {
            switch (map)
            {
                case MM4Map.E1DragonCave: return Offsets(map, MM4Map.E1DragonCaveEast, MM4Map.E1DragonCaveNorth, MM4Map.E1DragonCaveNorthEast);
                case MM4Map.F4WitchClouds: return Offsets(map, MM4Map.F4WitchCloudsEast, MM4Map.F4WitchCloudsNorth, MM4Map.F4WitchCloudsNorthEast);
                case MM4Map.C4HighMagicClouds: return Offsets(map, MM4Map.C4HighMagicCloudsEast, MM4Map.C4HighMagicCloudsNorth, MM4Map.C4HighMagicCloudsNorthEast);
                case MM4Map.D3CloudsOfXeen: return Offsets(map, MM4Map.D3CloudsOfXeenEast, MM4Map.D3CloudsOfXeenNorth, MM4Map.D3CloudsOfXeenNorthEast);
                case MM4Map.F3Vertigo: return Offsets(map, MM4Map.F3VertigoEast, MM4Map.F3VertigoNorth, MM4Map.F3VertigoNorthEast);
                case MM4Map.C3Rivercity: return Offsets(map, MM4Map.C3RivercityEast, MM4Map.C3RivercityNorth, MM4Map.C3RivercityNorthEast);
                case MM4Map.F3DwarfMine1North: return Offsets(map, MM4Map.F3DwarfMine1North);
                case MM4Map.F3DwarfMine2North: return Offsets(map, MM4Map.F3DwarfMine2North);
                case MM4Map.E2DwarfMine3East: return Offsets(map, MM4Map.E2DwarfMine3East);
                case MM4Map.B1SphinxBody: return Offsets(map, MM4Map.B1SphinxBodyNorth);
                case MM4Map.DeepMineAlpha: return Offsets(map, MM4Map.DeepMineAlphaEast, MM4Map.DeepMineAlphaNorth, MM4Map.DeepMineAlphaNorthEast);
                case MM4Map.DeepMineTheta: return Offsets(map, MM4Map.DeepMineThetaEast, MM4Map.DeepMineThetaNorth, MM4Map.DeepMineThetaNorthEast);
                case MM4Map.DeepMineKappa: return Offsets(map, MM4Map.DeepMineKappaEast, MM4Map.DeepMineKappaNorth, MM4Map.DeepMineKappaNorthEast);
                case MM4Map.DeepMineOmega: return Offsets(map, MM4Map.DeepMineOmegaEast, MM4Map.DeepMineOmegaNorth, MM4Map.DeepMineOmegaNorthEast);
                case MM4Map.E4AncientTempleOfYak: return Offsets(map, MM4Map.E4AncientTempleofYakEast, MM4Map.E4AncientTempleofYakNorth, MM4Map.E4AncientTempleofYakNorthEast);
                case MM4Map.C4TombOfAThousandTerrors: return Offsets(map, MM4Map.C4TombOfAThousandTerrorsEast, MM4Map.C4TombOfAThousandTerrorsNorth, MM4Map.C4TombOfAThousandTerrorsNorthEast);
                case MM4Map.B4GolemDungeon: return Offsets(map, MM4Map.B4GolemDungeonEast, MM4Map.B4GolemDungeonNorth, MM4Map.B4GolemDungeonNorthEast);
                default: return Offsets(map);
            }
        }

        public static int Character(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 20)
                return 0;

            return Characters + (iRosterPosition * MM45Character.SizeInBytes);
        }
    }

    public class MM5FileOffsets : FileOffsets
    {
        public const int Characters = 0xE01;
        public const int CartographyOffset = 860;
        public const int CurrentParty = 0x377F;

        public static uint[] StaticMaps = new uint[] {
            0, 0, 0, 0,                           // Map 0: Invalid
            0x03D75, 0, 0, 0,                     // Map 257: A-1, Darkside Surface
            0x0426D, 0, 0, 0,                     // Map 258: A-2, Darkside Surface
            0x04790, 0, 0, 0,                     // Map 259: A-3, Darkside Surface
            0x05493, 0, 0, 0,                     // Map 260: A-4, Darkside Surface
            0x05CE5, 0, 0, 0,                     // Map 261: B-1, Darkside Surface
            0x06334, 0, 0, 0,                     // Map 262: B-2, Darkside Surface
            0x069EE, 0, 0, 0,                     // Map 263: B-3, Darkside Surface
            0x07523, 0, 0, 0,                     // Map 264: B-4, Darkside Surface
            0x07C39, 0, 0, 0,                     // Map 265: C-1, Darkside Surface
            0x080F9, 0, 0, 0,                     // Map 266: C-2, Darkside Surface
            0x087C3, 0, 0, 0,                     // Map 267: C-3, Darkside Surface
            0x08CC8, 0, 0, 0,                     // Map 268: C-4, Darkside Surface
            0x09738, 0, 0, 0,                     // Map 269: D-1, Darkside Surface
            0x09F20, 0, 0, 0,                     // Map 270: D-2, Darkside Surface
            0x0A564, 0, 0, 0,                     // Map 271: D-3, Darkside Surface
            0x0AD24, 0, 0, 0,                     // Map 272: D-4, Darkside Surface
            0x0B44B, 0, 0, 0,                     // Map 273: E-1, Darkside Surface
            0x0BA2D, 0, 0, 0,                     // Map 274: E-2, Darkside Surface
            0x0BFEE, 0, 0, 0,                     // Map 275: E-3, Darkside Surface
            0x0C6B0, 0, 0, 0,                     // Map 276: E-4, Darkside Surface
            0x0CEE4, 0, 0, 0,                     // Map 277: F-1, Darkside Surface
            0x0D61A, 0, 0, 0,                     // Map 278: F-2, Darkside Surface
            0x0DB05, 0, 0, 0,                     // Map 279: F-3, Darkside Surface
            0x0E0EF, 0, 0, 0,                     // Map 280: F-4, Darkside Surface
            0x0E7CA, 0, 0, 0,                     // Map 281: A-1, Elemental Plane of Fire
            0x0ED30, 0, 0, 0,                     // Map 282: F-1, Elemental Plane of Air
            0x0F296, 0, 0, 0,                     // Map 283: F-4, Elemental Plane of Earth
            0x0F7FC, 0, 0, 0,                     // Map 284: A-4, Elemental Plane of Water
            0x0FD62, 0x51A3B, 0x516BF, 0x51DB7,   // Map 285: A-4, Castleview
            0x1229E, 0x524AF, 0x52133, 0x5282B,   // Map 286: A-4, Castleview Sewer
            0x13376, 0x52F23, 0x52BA7, 0x5329F,   // Map 287: E-3, Sandcaster
            0x14E64, 0x53997, 0x5361B, 0x53D13,   // Map 288: E-3, Sandcaster Sewer
            0x1599F, 0, 0, 0,                     // Map 289: F-2, Lakeside
            0x1649F, 0, 0, 0,                     // Map 290: F-2, Lakeside Sewer
            0x16C4B, 0, 0, 0,                     // Map 291: B-2, Necropolis
            0x17D5F, 0, 0, 0,                     // Map 292: B-2, Necropolis Sewer
            0x18D90, 0, 0, 0,                     // Map 293: C-2, Olympus
            0x194B2, 0, 0, 0,                     // Map 294: C-2, Olympus Sewer
            0x19A7A, 0x5440B, 0x5408F, 0x54787,   // Map 295: B-2, Gemstone Mines
            0x1DBA9, 0x54E7F, 0x54B03, 0x551FB,   // Map 296: E-4, Troll Holes
            0x1F147, 0, 0, 0,                     // Map 297: A-4, Castle Kalindra Level 1
            0x205CB, 0, 0, 0,                     // Map 298: A-4, Castle Kalindra Level 2
            0x21617, 0, 0, 0,                     // Map 299: A-4, Castle Kalindra Level 3
            0x21FB6, 0, 0, 0,                     // Map 300: A-4, Castle Kalindra Dungeon
            0x23286, 0, 0, 0,                     // Map 301: F-1, Castle Blackfang Level 1
            0x23EA3, 0, 0, 0,                     // Map 302: F-1, Castle Blackfang Level 2
            0x2477A, 0, 0, 0,                     // Map 303: F-1, Castle Blackfang Level 3
            0x25270, 0, 0, 0,                     // Map 304: F-1, Castle Blackfang Dungeon
            0x25732, 0, 0, 0,                     // Map 305: A-1, Castle Alamar Level 1
            0x26067, 0, 0, 0,                     // Map 306: A-1, Castle Alamar Level 2
            0x27CF2, 0, 0, 0,                     // Map 307: A-1, Castle Alamar Level 3
            0x284D9, 0, 0, 0,                     // Map 308: A-1, Castle Alamar Dungeon
            0x28F7B, 0, 0, 0,                     // Map 309: A-4, Ellinger's Tower Level 1
            0x297EA, 0, 0, 0,                     // Map 310: A-4, Ellinger's Tower Level 2
            0x2A1CD, 0, 0, 0,                     // Map 311: A-4, Ellinger's Tower Level 3
            0x2A8FC, 0, 0, 0,                     // Map 312: A-4, Ellinger's Tower Level 4
            0x2B533, 0, 0, 0,                     // Map 313: A-3, Western Tower Level 1
            0x2BAA0, 0, 0, 0,                     // Map 314: A-3, Western Tower Level 2
            0x2C05E, 0, 0, 0,                     // Map 315: A-3, Western Tower Level 3
            0x2C5FD, 0, 0, 0,                     // Map 316: A-3, Western Tower Level 4
            0x2CC9B, 0, 0, 0,                     // Map 317: D-4, Southern Tower Level 1
            0x2D61B, 0, 0, 0,                     // Map 318: D-4, Southern Tower Level 2
            0x2DB55, 0, 0, 0,                     // Map 319: D-4, Southern Tower Level 3
            0x2E260, 0, 0, 0,                     // Map 320: D-4, Southern Tower Level 4
            0x2E873, 0, 0, 0,                     // Map 321: F-3, Eastern Tower Level 1
            0x2ED81, 0, 0, 0,                     // Map 322: F-3, Eastern Tower Level 2
            0x2F2A4, 0, 0, 0,                     // Map 323: F-3, Eastern Tower Level 3
            0x2F817, 0, 0, 0,                     // Map 324: F-3, Eastern Tower Level 4
            0x30001, 0, 0, 0,                     // Map 325: D-1, Northern Tower Level 1
            0x3073D, 0, 0, 0,                     // Map 326: D-1, Northern Tower Level 2
            0x30E19, 0, 0, 0,                     // Map 327: D-1, Northern Tower Level 3
            0x315DB, 0, 0, 0,                     // Map 328: D-1, Northern Tower Level 4
            0x31E60, 0, 0, 0,                     // Map 329: C-4, Temple of Bark Level 1
            0x32B71, 0, 0, 0,                     // Map 330: C-4, Temple of Bark Level 2
            0x33D3A, 0, 0, 0,                     // Map 331: C-4, Temple of Bark Level 3
            0x34807, 0, 0, 0,                     // Map 332: C-4, Temple of Bark Level 4
            0x3565B, 0x558F3, 0x55577, 0x55C6F,   // Map 333: C-4, Temple of Bark Level 5
            0x364C1, 0, 0, 0,                     // Map 334: F-2, Lost Souls Dungeon Level 1
            0x36A36, 0, 0, 0,                     // Map 335: F-2, Lost Souls Dungeon Level 3
            0x370F6, 0, 0, 0,                     // Map 336: F-2, Lost Souls Dungeon Level 2
            0x38392, 0, 0, 0,                     // Map 337: F-2, Lost Souls Dungeon Level 4
            0x395F1, 0x56367, 0x55FEB, 0x566E3,   // Map 338: F-2, Lost Souls Dungeon Level 5
            0x3A5EA, 0x56DDB, 0x56A5F, 0x57157,   // Map 339: D-2, The Great Pyramid Level 1
            0x3B208, 0x5784F, 0x574D3, 0x57BCB,   // Map 340: D-2, The Great Pyramid Level 2
            0x3B692, 0x582C3, 0x57F47, 0x5863F,   // Map 341: D-2, The Great Pyramid Level 3
            0x3BAB8, 0, 0, 0,                     // Map 342: D-2, The Great Pyramid Level 4
            0x3C2AB, 0x589BB, 0, 0,               // Map 343: B-2, Escape Pod 1
            0x3CBCC, 0x58D37, 0, 0,               // Map 344: B-1, Escape Pod 2
            0x3D148, 0, 0, 0,                     // Map 345: A-1, Skyroad A1
            0x3D611, 0, 0, 0,                     // Map 346: A-2, Skyroad A2
            0x3DB35, 0, 0, 0,                     // Map 347: A-3, Skyroad A3
            0x3DFF8, 0, 0, 0,                     // Map 348: A-4, Skyroad A4
            0x3E4E8, 0, 0, 0,                     // Map 349: B-1, Skyroad B1
            0x3EA01, 0, 0, 0,                     // Map 350: B-2, Skyroad B2
            0x3EE77, 0, 0, 0,                     // Map 351: B-3, Skyroad B3
            0x3F4E6, 0, 0, 0,                     // Map 352: B-4, Skyroad B4
            0x3F91F, 0, 0, 0,                     // Map 353: C-1, Skyroad C1
            0x3FE28, 0, 0, 0,                     // Map 354: C-2, Skyroad C2
            0x40643, 0, 0, 0,                     // Map 355: C-3, Skyroad C3
            0x40BDA, 0, 0, 0,                     // Map 356: C-4, Skyroad C4
            0x41017, 0, 0, 0,                     // Map 357: D-1, Skyroad D1
            0x4147B, 0, 0, 0,                     // Map 358: D-2, Skyroad D2
            0x41896, 0, 0, 0,                     // Map 359: D-3, Skyroad D3
            0x41CAE, 0, 0, 0,                     // Map 360: D-4, Skyroad D4
            0x4217C, 0, 0, 0,                     // Map 361: E-1, Skyroad E1
            0x42627, 0, 0, 0,                     // Map 362: E-2, Skyroad E2
            0x42BCF, 0, 0, 0,                     // Map 363: E-3, Skyroad E3
            0x4309F, 0, 0, 0,                     // Map 364: E-4, Skyroad E4
            0x43648, 0, 0, 0,                     // Map 365: F-1, Skyroad F1
            0x43BD5, 0, 0, 0,                     // Map 366: F-2, Skyroad F2
            0x44140, 0, 0, 0,                     // Map 367: F-3, Skyroad F3
            0x445AC, 0, 0, 0,                     // Map 368: F-4, Skyroad F4
            0x44B02, 0, 0, 0,                     // Map 369: B-3, Darkstone Tower Level 1
            0x4501E, 0, 0, 0,                     // Map 370: B-3, Darkstone Tower Level 2
            0x454E3, 0, 0, 0,                     // Map 371: B-3, Darkstone Tower Level 3
            0x45A22, 0, 0, 0,                     // Map 372: B-3, Darkstone Tower Level 4
            0x45FB1, 0, 0, 0,                     // Map 373: D-1, Dragon Tower Level 1
            0x4645F, 0, 0, 0,                     // Map 374: D-1, Dragon Tower Level 2
            0x46A21, 0, 0, 0,                     // Map 375: D-1, Dragon Tower Level 3
            0x470E7, 0, 0, 0,                     // Map 376: D-1, Dragon Tower Level 4
            0x4778D, 0x5942F, 0x590B3, 0x597AB,   // Map 377: E-3, Dungeon of Death Level 1
            0x4B178, 0x59EA3, 0x59B27, 0x5A21F,   // Map 378: E-3, Dungeon of Death Level 2
            0x4D078, 0x5A917, 0x5A59B, 0x5AC93,   // Map 379: E-3, Dungeon of Death Level 3
            0x4D7FE, 0x5B38B, 0x5B00F, 0x5B707,   // Map 380: E-3, Dungeon of Death Level 4
            0x4E287, 0x5BA83, 0, 0,               // Map 381: A-2, Southern Sphinx Level 1
            0x4F6C0, 0, 0, 0,                     // Map 382: A-2, Southern Sphinx Level 2
            0x4FD55, 0, 0, 0,                     // Map 383: A-2, Southern Sphinx Level 3
            0x507A5, 0x5C17B, 0x5BDFF, 0x5C4F7,   // Map 384: D-1, Dragon Clouds
            0x5117D, 0x5CBEF, 0x5C873, 0x5CF6B,   // Map 385: B-3, Clouds of the Ancients
        };

        public static uint MapOffset(MM5Map map) { return MapFileOffsets[(int)map - 1]; }

        public static uint[] Offsets(params MM5Map[] maps)
        {
            uint[] uints = new uint[maps.Length];
            for (int i = 0; i < maps.Length; i++)
                uints[i] = MapOffset(maps[i]);
            return uints;
        }

        public static uint[] MapFileOffsets = new uint[]
        {
            0x03D75, 0x0426D, 0x04790, 0x05493, 0x05CE5, 0x06334, 0x069EE, 0x07523,
            0x07C39, 0x080F9, 0x087C3, 0x08CC8, 0x09738, 0x09F20, 0x0A564, 0x0AD24,
            0x0B44B, 0x0BA2D, 0x0BFEE, 0x0C6B0, 0x0CEE4, 0x0D61A, 0x0DB05, 0x0E0EF,
            0x0E7CA, 0x0ED30, 0x0F296, 0x0F7FC, 0x0FD62, 0x1229E, 0x13376, 0x14E64,
            0x1599F, 0x1649F, 0x16C4B, 0x17D5F, 0x18D90, 0x194B2, 0x19A7A, 0x1DBA9,
            0x1F147, 0x205CB, 0x21617, 0x21FB6, 0x23286, 0x23EA3, 0x2477A, 0x25270,
            0x25732, 0x26067, 0x27CF2, 0x284D9, 0x28F7B, 0x297EA, 0x2A1CD, 0x2A8FC,
            0x2B533, 0x2BAA0, 0x2C05E, 0x2C5FD, 0x2CC9B, 0x2D61B, 0x2DB55, 0x2E260,
            0x2E873, 0x2ED81, 0x2F2A4, 0x2F817, 0x30001, 0x3073D, 0x30E19, 0x315DB,
            0x31E60, 0x32B71, 0x33D3A, 0x34807, 0x3565B, 0x364C1, 0x36A36, 0x370F6,
            0x38392, 0x395F1, 0x3A5EA, 0x3B208, 0x3B692, 0x3BAB8, 0x3C2AB, 0x3CBCC,
            0x3D148, 0x3D611, 0x3DB35, 0x3DFF8, 0x3E4E8, 0x3EA01, 0x3EE77, 0x3F4E6,
            0x3F91F, 0x3FE28, 0x40643, 0x40BDA, 0x41017, 0x4147B, 0x41896, 0x41CAE,
            0x4217C, 0x42627, 0x42BCF, 0x4309F, 0x43648, 0x43BD5, 0x44140, 0x445AC,
            0x44B02, 0x4501E, 0x454E3, 0x45A22, 0x45FB1, 0x4645F, 0x46A21, 0x470E7,
            0x4778D, 0x4B178, 0x4D078, 0x4D7FE, 0x4E287, 0x4F6C0, 0x4FD55, 0x507A5,
            0x5117D, 0x516BF, 0x51A3B, 0x51DB7, 0x52133, 0x524AF, 0x5282B, 0x52BA7,
            0x52F23, 0x5329F, 0x5361B, 0x53997, 0x53D13, 0x5408F, 0x5440B, 0x54787,
            0x54B03, 0x54E7F, 0x551FB, 0x55577, 0x558F3, 0x55C6F, 0x55FEB, 0x56367,
            0x566E3, 0x56A5F, 0x56DDB, 0x57157, 0x574D3, 0x5784F, 0x57BCB, 0x57F47,
            0x582C3, 0x5863F, 0x589BB, 0x58D37, 0x590B3, 0x5942F, 0x597AB, 0x59B27,
            0x59EA3, 0x5A21F, 0x5A59B, 0x5A917, 0x5AC93, 0x5B00F, 0x5B38B, 0x5B707,
            0x5BA83, 0x5BDFF, 0x5C17B, 0x5C4F7, 0x5C873, 0x5CBEF, 0x5CF6B
        };

        private static uint[] m_Scripts = new uint[] { 0,
            0x040F1, 0x045E9, 0x04B0C, 0x0580F, 0x06061, 0x066B0, 0x06D6A, 0x0789F, 0x07FB5, 0x08475,  // Maps 1-10
            0x08B3F, 0x09044, 0x09AB4, 0x0A29C, 0x0A8E0, 0x0B0A0, 0x0B7C7, 0x0BDA9, 0x0C36A, 0x0CA2C,  // Maps 11-20
            0x0D260, 0x0D996, 0x0DE81, 0x0E46B, 0x0EB46, 0x0F0AC, 0x0F612, 0x0FB78, 0x100DE, 0x1261A,  // Maps 21-30
            0x136F2, 0x151E0, 0x15D1B, 0x1681B, 0x16FC7, 0x180DB, 0x1910C, 0x1982E, 0x19DF6, 0x1DF25,  // Maps 31-40
            0x1F4C3, 0x20947, 0x21993, 0x22332, 0x23602, 0x2421F, 0x24AF6, 0x255EC, 0x25AAE, 0x263E3,  // Maps 41-50
            0x2806E, 0x28855, 0x292F7, 0x29B66, 0x2A549, 0x2AC78, 0x2B8AF, 0x2BE1C, 0x2C3DA, 0x2C979,  // Maps 51-60
            0x2D017, 0x2D997, 0x2DED1, 0x2E5DC, 0x2EBEF, 0x2F0FD, 0x2F620, 0x2FB93, 0x3037D, 0x30AB9,  // Maps 61-70
            0x31195, 0x31957, 0x321DC, 0x32EED, 0x340B6, 0x34B83, 0x359D7, 0x3683D, 0x36DB2, 0x37472,  // Maps 71-80
            0x3870E, 0x3996D, 0x3A966, 0x3B584, 0x3BA0E, 0x3BE34, 0x3C627, 0x3CF48, 0x3D4C4, 0x3D98D,  // Maps 81-90
            0x3DEB1, 0x3E374, 0x3E864, 0x3ED7D, 0x3F1F3, 0x3F862, 0x3FC9B, 0x401A4, 0x409BF, 0x40F56,  // Maps 91-100
            0x41393, 0x417F7, 0x41C12, 0x4202A, 0x424F8, 0x429A3, 0x42F4B, 0x4341B, 0x439C4, 0x43F51,  // Maps 101-110
            0x444BC, 0x44928, 0x44E7E, 0x4539A, 0x4585F, 0x45D9E, 0x4632D, 0x467DB, 0x46D9D, 0x47463,  // Maps 111-120
            0x47B09, 0x4B4F4, 0x4D3F4, 0x4DB7A, 0x4E603, 0x4FA3C, 0x500D1, 0x50B21, 0x514F9            // Maps 121-129
        };

        private static uint[] m_Monsters = new uint[] { 0,
            0x041FD, 0x04750, 0x0545B, 0x05CB9, 0x062E4, 0x069B6, 0x07483, 0x07BF5, 0x080B9, 0x0878B, // Maps 1-10 
            0x08C68, 0x096B4, 0x09EC4, 0x0A524, 0x0AC5C, 0x0B403, 0x0B9F5, 0x0BFAE, 0x0C680, 0x0CE9C, // Maps 11-20
            0x0D5DE, 0x0DAD1, 0x0E0AB, 0x0E776, 0x0ED04, 0x0F26A, 0x0F7D0, 0x0FD36, 0x12112, 0x131E2, // Maps 21-30
            0x14D50, 0x1584B, 0x16437, 0x16BEF, 0x17CAF, 0x18D20, 0x1948E, 0x19A56, 0x1D971, 0x1F03B, // Maps 31-40
            0x2054B, 0x215BB, 0x21F8E, 0x23216, 0x23E87, 0x2473A, 0x25204, 0x25712, 0x2603F, 0x27C1E, // Maps 41-50
            0x2605F, 0x28EE3, 0x297AA, 0x2A15D, 0x2A8D8, 0x2B517, 0x2BA80, 0x2C032, 0x2C5C9, 0x2CC57, // Maps 51-60
            0x2D567, 0x2DB0D, 0x2E244, 0x2E80F, 0x2ED65, 0x2F27C, 0x2F7DF, 0x2FFAD, 0x30701, 0x30DCD, // Maps 61-70
            0x31567, 0x31E30, 0x32ABD, 0x33CB2, 0x3476F, 0x35547, 0x36445, 0x369EE, 0x3708E, 0x3825A, // Maps 71-80
            0x39569, 0x3A4DE, 0x3B108, 0x3B682, 0x3BAA8, 0x3C287, 0x3B108, 0x3B682, 0x3D5F5, 0x3DB09, // Maps 81-90
            0x3DFD4, 0x3E4CC, 0x3E9C9, 0x3E9F9, 0x3F4C2, 0x3F903, 0x3FDFC, 0x4061B, 0x4063B, 0x40FF3, // Maps 91-100
            0x41457, 0x4185A, 0x4188E, 0x42158, 0x425FF, 0x42B8F, 0x42BC7, 0x43628, 0x43B81, 0x44120, // Maps 101-110
            0x44584, 0x44ADE, 0x44FF6, 0x454C3, 0x459EA, 0x45F89, 0x4644F, 0x469E5, 0x47097, 0x47749, // Maps 111-120
            0x4AEA0, 0x4CDC0, 0x4D762, 0x4E167, 0x4F63C, 0x4FD21, 0x5070D, 0x5109D, 0x516AF           // Maps 121-129
        };

        public override uint[] Maps { get { return StaticMaps; } }
        public override uint[] Scripts { get { return m_Scripts; } }
        public override uint[] Monsters { get { return m_Monsters; } }

        public static uint[] MapOffsets(MM5Map map)
        {
            switch (map)
            {
                case MM5Map.A4Castleview: return Offsets(map, MM5Map.A4CastleviewEast, MM5Map.A4CastleviewNorth, MM5Map.A4CastleviewNorthEast);
                case MM5Map.A4CastleviewSewer: return Offsets(map, MM5Map.A4CastleviewSewerEast, MM5Map.A4CastleviewSewerNorth, MM5Map.A4CastleviewSewerNorthEast);
                case MM5Map.E3Sandcaster: return Offsets(map, MM5Map.E3SandcasterEast, MM5Map.E3SandcasterNorth, MM5Map.E3SandcasterNorthEast);
                case MM5Map.E3SandcasterSewer: return Offsets(map, MM5Map.E3SandcasterSewerEast, MM5Map.E3SandcasterSewerNorth, MM5Map.E3SandcasterSewerNorthEast);
                case MM5Map.B2GemstoneMines: return Offsets(map, MM5Map.B2GemstoneMinesEast, MM5Map.B2GemstoneMinesNorth, MM5Map.B2GemstoneMinesNorthEast);
                case MM5Map.E4TrollHoles: return Offsets(map, MM5Map.E4TrollHolesEast, MM5Map.E4TrollHolesNorth, MM5Map.E4TrollHolesNorthEast);
                case MM5Map.C4TempleOfBarkLevel5: return Offsets(map, MM5Map.C4TempleOfBarkLevel5East, MM5Map.C4TempleOfBarkLevel5North, MM5Map.C4TempleOfBarkLevel5NorthEast);
                case MM5Map.F2LostSoulsDungeonLevel5: return Offsets(map, MM5Map.F2LostSoulsDungeonLevel5East, MM5Map.F2LostSoulsDungeonLevel5North, MM5Map.F2LostSoulsDungeonLevel5NorthEast);
                case MM5Map.D2TheGreatPyramidLevel1: return Offsets(map, MM5Map.D2TheGreatPyramidLevel1East, MM5Map.D2TheGreatPyramidLevel1North, MM5Map.D2TheGreatPyramidLevel1NorthEast);
                case MM5Map.D2TheGreatPyramidLevel2: return Offsets(map, MM5Map.D2TheGreatPyramidLevel2East, MM5Map.D2TheGreatPyramidLevel2North, MM5Map.D2TheGreatPyramidLevel2NorthEast);
                case MM5Map.D2TheGreatPyramidLevel3: return Offsets(map, MM5Map.D2TheGreatPyramidLevel3East, MM5Map.D2TheGreatPyramidLevel3North, MM5Map.D2TheGreatPyramidLevel3NorthEast);
                case MM5Map.E3DungeonOfDeathLevel1: return Offsets(map, MM5Map.E3DungeonOfDeathLevel1East, MM5Map.E3DungeonOfDeathLevel1North, MM5Map.E3DungeonOfDeathLevel1NorthEast);
                case MM5Map.E3DungeonOfDeathLevel2: return Offsets(map, MM5Map.E3DungeonOfDeathLevel2East, MM5Map.E3DungeonOfDeathLevel2North, MM5Map.E3DungeonOfDeathLevel2NorthEast);
                case MM5Map.E3DungeonOfDeathLevel3: return Offsets(map, MM5Map.E3DungeonOfDeathLevel3East, MM5Map.E3DungeonOfDeathLevel3North, MM5Map.E3DungeonOfDeathLevel3NorthEast);
                case MM5Map.E3DungeonOfDeathLevel4: return Offsets(map, MM5Map.E3DungeonOfDeathLevel4East, MM5Map.E3DungeonOfDeathLevel4North, MM5Map.E3DungeonOfDeathLevel4NorthEast);
                case MM5Map.D1DragonClouds: return Offsets(map, MM5Map.D1DragonCloudsEast, MM5Map.D1DragonCloudsNorth, MM5Map.D1DragonCloudsNorthEast);
                case MM5Map.B3CloudsOfTheAncients: return Offsets(map, MM5Map.B3CloudsOfTheAncientsEast, MM5Map.B3CloudsOfTheAncientsNorth, MM5Map.B3CloudsOfTheAncientsNorthEast);
                case MM5Map.B1EscapePod2: return Offsets(map, MM5Map.B1EscapePod2East);
                case MM5Map.B2EscapePod1: return Offsets(map, MM5Map.B2EscapePod1East);
                case MM5Map.A2SouthernSphinxLevel1: return Offsets(map, MM5Map.A2SouthernSphinxLevel1North);
                default: return Offsets(map);
            }
        }

        public static int Character(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 20)
                return 0;

            return Characters + (iRosterPosition * MM45Character.SizeInBytes);
        }
    }

    public enum QuestGoal
    {
        Complete,
        Incomplete,
        NotStarted,
        BadFile
    }

    public class MM45FileQuestInfo : FileQuestInfo
    {
        private byte[] m_bytes = new byte[(int) (ByteIndex.Last - ByteIndex.First)];
        private Point[] m_points = new Point[(int)(PointIndex.Last - PointIndex.First)];

        public enum ByteIndex { First = 0,
            Phirna1 = 0, Phirna2, Phirna3, Phirna4, Phirna5, Phirna6, Phirna7, Phirna8, Phirna9, Phirna10,
            FoundAlacorn, Whistle, ReturnedAlacorn, Skull, Tiara, Scarab, Crystals, Elixir, Book, Rock, Scroll, Cyclops,
            Mirror, Troll, Dreyfus, XeenSlayer, Luna, Ambrose, Songbird, Ogres, Handle, Melon1, Melon2,
            FoundMelon1, FoundMelon2, FoundMelon3, FoundMelon4, Sheewana, Chalice, Ring, Glass, Jewel, KilledGettle, Jethro,
            Astra, Disk01, Disk02, Disk03, Disk04, Disk05, Disk06, Disk07, Disk08, Disk09, Disk10, Disk11, Disk12, Disk13,
            Disk14, Disk15, Disk16, Disk17, Disk18, Disk19, Disk20, TurnInDisks, Egg, Fire1, Fire2, Fire3, Fire4, Elec1, Elec2, Elec3, Elec4,
            Cold1, Cold2, Poison1, Poison2, Poison3, Energy1, Energy2, Energy3, Magic1, Magic2, Magic3, Might1, Might2, Might3, Might4, Might5,
            Might6, Might7, Might8, Might9, Might10, Might11, Might12, Might13, Might14, Might15, Might16, Might17, Might18, Might19, Might20,
            Might21, Might22, Might23, Might24, Might25, Might26, Might27, Might28, Might29, Might30, Might31, Might32, Might33, Might34, Might35,
            Might36, Might37, Might38, Might39, Might40, Might41, Might42, Int1, Int2, Int3, Int4, Int5, Int6, Int7, Int8, Int9, Int10, Int11,
            Int12, Int13, Int14, Int15, Int16, Int17, Int18, Int19, Int20, Int21, Int22, Int23, Int24, Int25, Int26, Int27, Int28, Int29, Per1,
            Per2, Per3, Per4, Per5, Per6, Per7, Per8, Per9, Per10, Per11, Per12, Per13, Per14, Per15, Per16, Per17, Per18, Per19, Per20, Per21,
            Per22, Per23, Per24, Per25, Per26, End1, End2, End3, End4, End5, End6, End7, End8, End9, End10, End11, End12, End13, End14, End15,
            End16, End17, End18, End19, End20, End21, End22, End23, End24, End25, End26, End27, End28, End29, End30, End31, End32, End33, End34,
            End35, End36, End37, End38, End39, End40, End41, End42, End43, End44, Spd1, Spd2, Spd3, Spd4, Spd5, Spd6, Spd7, Spd8, Spd9, Spd10,
            Spd11, Spd12, Spd13, Spd14, Spd15, Spd16, Spd17, Spd18, Spd19, Spd20, Spd21, Spd22, Spd23, Spd24, Spd25, Spd26, Spd27, Spd28, Spd29,
            Spd30, Spd31, Spd32, Spd33, Spd34, Spd35, Spd36, Spd37, Acy1, Acy2, Acy3, Acy4, Acy5, Acy6, Acy7, Acy8, Acy9, Acy10, Acy11, Acy12,
            Acy13, Acy14, Acy15, Acy16, Acy17, Acy18, Acy19, Acy20, Acy21, Acy22, Acy23, Acy24, Acy25, Acy26, Lck1, Lck2, Lck3, Lck4, Lck5, Lck6,
            Lck7, Lck8, Lck9, Lck10, Lck11, Lck12, Lck13, Lck14, Lck15, Lck16, Lck17, Lck18, Lck19, Lev1, Lev2, Lev3, Lev4, Lev5, Lev6, Lev7,
            Lev8, Lev9, Lev10, Lev11, Lev12, Lev13, Lev14, Lev15, Lev16, Lev17, Lev18, Lev19, Lev20, Lev21, Lev22, Lev23, Lev24, Lev25, Lev26,
            Lev27, Lev28, Lev29, Lev30, Lev31, Lev32, Lev33, Lev34, Lev35, Lev36, Lev37, Lev38, Lev39, Lev40, Lev41, Lev42, Lev43, Lev44, Lev45,
            Lev46, Lev47, Lev48, Lev49, Lev50, Lev51, Lev52, Stat1, Stat2, Stat3, Stat4, Stat5, Stat6, Stat7, Stat8, Stat9, Stat10, Stat11, Stat12,
            Stat13, Stat14, Stat15, Stat16, Stat17, Stat18, Stat19, Stat20, Stat21, Stat22, Stat23, TalkValio, ReturnValio, Paladin2, Paladin3,
            Paladin4, Paladin5, Paladin6, Paladin7, Paladin8, Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8, WordMaster, TTLever,
            Gold1, Gold2, Gold3, Gold4, Gold5, Gold6, Gold7, Gold8, Gold9, Gold10, Gold11, Gold12, Gold13, Gold14, Gold15, Gold16, Gold17, Gold18,
            Gold19, Gold20, Gold21, Gold22, Gold23, Gold24, Gold25, Gold26, Gold27, Gold28, Gold29, Gold30, Gold31, Gold32, Gold33, Gold34, Gold35,
            Gold36, Gold37, Gold38, Gold39, Gold40, Gold41, Gold42, Gold43, Gold44, Gold45, Gold46, Gold47, Gold48, Gold49, Gold50, Gold51, Gold52,
            Gold53, Gold54, Gold55, Gold56, Gold57, Gold58, Gold59, Gold60, Gold61, Gold62, Gold63, Gold64, Gold65, Gold66, Gold67, Gold68, Gold69,
            Gold70, Gold71, Gold72, Gold73, Gold74, Gold75, Gold76, Gold77, Gold78, Gold79, Gold80, Gold81, Gold82, Gold83, Gold84, Gold85, Gold86,
            Gold87, Gold88, Gold89, Gold90, Gold91, Gold92, Gold93, Gold94, Gold95, Gold96, Gold97, Gold98, Gold99, Gold100, Gold101, Gold102, Gold103,
            Gold104, Gold105, Gold106, Gold107, Gold108, Gold109, Gold110, Gold111, Gold112, Gold113, Gold114, Gold115, Gold116, Gold117, Gold118, Gold119,
            Gold120, Gold121, Gold122, Gold123, Gold124, Gold125, Gold126, Gold127, Gold128, Gold129, Gold130, Gold131, Gold132, Gold133, Gold134, Gold135,
            Gold136, Gold137, Gold138, Gold139, Gold140, Gold141, Gold142, Gold143, Gold144, Gold145, Gold146, Gold147, Gold148, Gold149, Gold150, Gold151,
            Gold152, Gold153, Gold154, Gold155, Gold156, Gold157, Gold158, Gold159, Gold160, Destroy1, Destroy2, Destroy3, Destroy4, Destroy5, Destroy6,
            Destroy7, Destroy8, Destroy9, Destroy10, Destroy11, Destroy12, Destroy13, Destroy14, Destroy15, Destroy16, Destroy17, Destroy18, Destroy19,
            Destroy20, Destroy21, Destroy22, Destroy23, Destroy24, Destroy25, Destroy26, Destroy27, Destroy28, Destroy29, Destroy30, Destroy31, Destroy32,
            Destroy33, Destroy34, Destroy35, Destroy36, Destroy37, Destroy38, Destroy39, Destroy40, Destroy41, Destroy42, Destroy43, Destroy44, Destroy45,
            Destroy46, Destroy47, Lamp1, Lamp2, Lamp3, Lamp4, Lamp5, Lamp6, Lamp7, Lamp8, Lamp9, Lamp10, Lamp11, Lamp12, Lamp13, Lamp14, Lamp15,
            Lamp16, Lamp17, Lamp18, L7Item1, L7Item2, L7Item3, L7Item4, L7Item5, L7Item6, L7Item7, L7Item8, L7Item9, L7Item10, L7Item11, L7Item12, L7Item13,
            L7Item14, L7Item15, L7Item16, L7Item17, L7Item18, L7Item19, L7Item20, L7Item21, L7Item22, L7Item23, L7Item24, L7Item25, L7Item26, L7Item27,
            L7Item28, L7Item29, L7Item30, L7Item31, L7Item32, L7Item33, L7Item34, L7Item35, L7Item36, L7Item37, Draco, PyNumLev, PyNumTreas,
            Tree1, Tree2, Tree3, Tree4, Tree5, Tree6, Tree7, Tree8, Tree9, Tree10, Tree11, Tree12, Tree13, Tree14, Tree15, Tree16, Tree17, Tree18,
            Tree19, Tree20, Tree21, Tree22, Tree23, Tree24, Tree25, Tree26, Tree27, Tree28, Tree29, Tree30, Tree31, Bark1, Bark2, Bark3, Bark4, Bark5, Bark6, 
            Bark7, Bark8, Bark9, Bark10, Bark11, Bark12, Bark13, Bark14, Bark15, Bark16, Bark17, Bark18, Bark19, Bark20, Bark21, Bark22, Bark23, Bark24,
            Bark25, Bark26, Bark27, Bark28, Bark29, Bark30, Bark31, Bark32, BarkTr1, BarkTr2, BarkTr3, PayLSD, HelpedDerek,
            Last };

        public enum PointIndex { First = 0, Jasper = 0, LordXeen, Gettlewaith, Morgana, Xenoc, ChestP, ChestI, ChestT, ChestC,
            ChestH, ChestF, ChestO, ChestR, ChestK, Chest1, Chest2, Chest3, Chest4, Chest5, Chest6, Chest7, Chest8, Chest9,
            BarkDial1, BarkDial2, BarkDial3, BarkDial4, BarkDial5, BarkDial6, BarkLever, Monk1, Monk2, Monk3, Rooka, CWScepter, CWTeleportation,
            CWDistortion, CWLeprechaun, CWVampire, CWKalindra, CWMagic, CWNova, CWRevitalize, CWSeer, CWIguana, CWError, CWCorak, CWAcid, CWArena,
            CWArcher, CWTito, CWSorceress, CWResurrection, CWRanger, CWAlamar, CWSevere, CWTherewolf, CWNecropolis, CWSnout, CWKnowledge, CWFlamberge,
            CWSpell, CWUse, CWPrestidigitator, CWVulture, CWGold, CWAgile, CWFable, CWFisherman, CWClairvoyance, CWTrident, CWSwine, CWSkeleton,
            CWSandcaster, CWEnergy, CWDarkstone, CWDragon, CWCastleview, CWWinterkill, CWTether, CWSphinx, CWBeast, CWMight, CWDisease, CWPage,
            CWCriminal, CWDruid, CWOctopus, CWArachnoid, CWWizard, CWGriffin, CWInsect, CWMoat, CWFear, CWAccuracy, CWFalista, CWEerie, CWCentipede,
            CWMountaineer, CWAlligator, CWDungeon, CWRat, CWGargoyle, CWStartle, CWArmadillo, CWGoblin, CWMummy, CWUndead, CWVenom, CWMirror, CWXeen,
            CWBarbarian, CWPegasus, CWCrown, CWJVC, CWPrisoner, CWOrb, CWCartographer, CWSewer, CWStumble, CWMinotaur, CWCrusader, CWElements,
            TTGong1, TTGong2, TTGong3, TTGong4, Draco, PyNum3, PyNum4, PyNum5, PyNum6, PyNum7, PyNum8, PyNum9, PyNum10, Barkman,
            BSkull1, BSkull2, BSkull3, BSkull4, BSkull5, BSkull6, BSkull7, BSkull8, LeverLSD1, LeverLSD2, Time1, Time2, Time3, Time4, Time5,
            Torch1, Torch2, Torch3, Torch4, Torch5, Torch6,
            Last };
        
        public QuestGoal Goal(ByteIndex idx) { return Goal(m_bytes[(int)idx]); }
        public QuestGoal Alive(PointIndex idx) { return Goal(m_points[(int)idx].X < 32); }
        public QuestGoal Dead(PointIndex idx) { return Goal(m_points[(int)idx].X > 31); }
        public Point Point(PointIndex idx) { return m_points[(int)idx]; }

        public QuestGoal[] MultiDead(params PointIndex[] indices)
        {
            QuestGoal[] goals = new QuestGoal[indices.Length];
            for (int i = 0; i < indices.Length; i++)
                goals[i] = Dead(indices[i]);
            return goals;
        }

        public QuestGoal[] Goals(ByteIndex first, ByteIndex last)
        {
            QuestGoal[] goals = new QuestGoal[(int)last - (int)first + 1];
            for (ByteIndex bi = first; bi <= last; bi++)
                goals[(int)bi - (int)first] = Goal(m_bytes[(int)bi]);
            return goals;
        }

        public Point[] WaterDragons = new Point[MM45Bytes.WaterDragons.Length];
        public Point[] PolterFools = new Point[MM45Bytes.PolterFool.Length];
        public Point[] SpiritBones = new Point[MM45Bytes.SpiritBones.Length];
        public Point[] GhostRiders = new Point[MM45Bytes.GhostRider.Length];

        public byte[] YakMegaCredits = new byte[MM45.Spots.YakMegaCredits.Length];
        public byte[] TombMegaCredits = new byte[MM45.Spots.TombMegaCredits.Length];
        public byte[] GolemMegaCredits = new byte[MM45.Spots.GolemMegaCredits.Length];

        public QuestGoal[] YakCredits { get { return GoalsFromBytes(YakMegaCredits, 0); } }
        public QuestGoal[] TombCredits { get { return GoalsFromBytes(TombMegaCredits, 0); } }
        public QuestGoal[] GolemCredits { get { return GoalsFromBytes(GolemMegaCredits, 0); } }
        public QuestGoal[] EnergyDisks
        {
            get
            {
                QuestGoal[] disks = new QuestGoal[20];
                for (ByteIndex idx = ByteIndex.Disk01; idx < ByteIndex.Disk20; idx++)
                    disks[(int)(idx - ByteIndex.Disk01)] = Goal(idx);
                return disks;
            }
        }

        public QuestGoal[] GoalsFromBytes(byte[] bytes, byte compare)
        {
            QuestGoal[] goals = new QuestGoal[bytes.Length];
            for (int i = 0; i < goals.Length; i++)
                goals[i] = Valid ? (bytes[i] == compare ? QuestGoal.Complete : QuestGoal.Incomplete) : QuestGoal.BadFile;
            return goals;
        }

        public MM45FileQuestInfo()
        {
            NullPoints(WaterDragons);
            NullPoints(PolterFools);
            NullPoints(SpiritBones);
            NullPoints(GhostRiders);
            Valid = false;
        }

        private byte MapScriptByte(MM4Map map, uint offset, FileAndMemoryInfo info, bool bScript = true)
        {
            return MapScriptByte(new MM4FileOffsets(), (int)map, offset, info, bScript);
        }

        private Point GetMonsterLocation(MM4Map map, uint offset, FileAndMemoryInfo info)
        {
            return GetMonsterLocation(new MM4FileOffsets(), (int)map, offset * 4, info);
        }

        private byte MapScriptByte(MM5Map map, uint offset, FileAndMemoryInfo info, bool bScript = true)
        {
            return MapScriptByte(new MM5FileOffsets(), (int)map + 256, offset, info, bScript);
        }

        private Point GetMonsterLocation(MM5Map map, uint offset, FileAndMemoryInfo info)
        {
            return GetMonsterLocation(new MM5FileOffsets(), (int)map + 256, offset * 4, info);
        }

        public void SetInfo(byte[] bytesXeen, byte[] bytesDark, byte[] bytesMemoryScript, byte[] bytesMemoryMonsters, List<MemoryBytes> bytesMemoryMap, int iMemoryMap)
        {
            FileAndMemoryInfo infoXeen = new FileAndMemoryInfo(bytesXeen, bytesMemoryScript, bytesMemoryMonsters, bytesMemoryMap, iMemoryMap);
            FileAndMemoryInfo infoDark = new FileAndMemoryInfo(bytesDark, bytesMemoryScript, bytesMemoryMonsters, bytesMemoryMap, iMemoryMap);

            m_bytes[(int)ByteIndex.Phirna1] = MapScriptByte(MM4Map.F3Surface, MM45Bytes.PhirnaF3CS0802, infoXeen);
            m_bytes[(int)ByteIndex.Phirna2] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1214, infoXeen);
            m_bytes[(int)ByteIndex.Phirna3] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0512, infoXeen);
            m_bytes[(int)ByteIndex.Phirna4] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0712, infoXeen);
            m_bytes[(int)ByteIndex.Phirna5] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1312, infoXeen);
            m_bytes[(int)ByteIndex.Phirna6] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0607, infoXeen);
            m_bytes[(int)ByteIndex.Phirna7] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0807, infoXeen);
            m_bytes[(int)ByteIndex.Phirna8] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1207, infoXeen);
            m_bytes[(int)ByteIndex.Phirna9] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1204, infoXeen);
            m_bytes[(int)ByteIndex.Phirna10] = MapScriptByte(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0702, infoXeen);
            m_bytes[(int)ByteIndex.FoundAlacorn] = MapScriptByte(MM4Map.F4WitchTowerLevel4, MM45Bytes.AlacornF4WT40704, infoXeen);
            m_bytes[(int)ByteIndex.Whistle] = MapScriptByte(MM4Map.E4Surface, MM45Bytes.WhistleE4CS0514, infoXeen);
            m_bytes[(int)ByteIndex.ReturnedAlacorn] = MapScriptByte(MM4Map.E4Surface, MM45Bytes.ReturnedAlacornF4CS0903, infoXeen);
            m_bytes[(int)ByteIndex.Skull] = MapScriptByte(MM4Map.D3Surface, MM45Bytes.ReturnedSkullD3CS1208, infoXeen);
            m_bytes[(int)ByteIndex.Tiara] = MapScriptByte(MM4Map.D2CastleBurlockLevel3, MM45Bytes.ReturnedTiaraD2CBL30211, infoXeen);
            m_bytes[(int)ByteIndex.Scarab] = MapScriptByte(MM4Map.C2Surface, MM45Bytes.ReturnedScarabC2CS1006, infoXeen);
            m_bytes[(int)ByteIndex.Crystals] = MapScriptByte(MM4Map.C2Surface, MM45Bytes.ReturnedCrystalsC2CS0811, infoXeen);
            m_bytes[(int)ByteIndex.Elixir] = MapScriptByte(MM4Map.D4Surface, MM45Bytes.DeliveredElixirD4CS1203, infoXeen);
            m_bytes[(int)ByteIndex.Book] = MapScriptByte(MM4Map.C3Surface, MM45Bytes.DeliveredBookC3CS0308, infoXeen);
            m_bytes[(int)ByteIndex.Rock] = MapScriptByte(MM4Map.B3Surface, MM45Bytes.DeliveredRockB3CS0906, infoXeen);
            m_bytes[(int)ByteIndex.Scroll] = MapScriptByte(MM4Map.A1Surface, MM45Bytes.DeliveredScrollA1CS1105, infoXeen);
            m_bytes[(int)ByteIndex.Cyclops] = MapScriptByte(MM4Map.A3Surface, MM45Bytes.TurnInCyclopsA3CS1000, infoXeen);
            m_bytes[(int)ByteIndex.Dreyfus] = MapScriptByte(MM5Map.A3WesternTowerLevel4, MM45Bytes.TurnInDreyfusA3WTL40410, infoDark);
            m_bytes[(int)ByteIndex.Troll] = MapScriptByte(MM4Map.B3Surface, MM45Bytes.TurnInTrollLairB3CS0603, infoXeen);
            m_bytes[(int)ByteIndex.Mirror] = MapScriptByte(MM4Map.D2CastleBurlockLevel1, MM45Bytes.TurnInMirrorD2CBL10801, infoXeen);
            m_bytes[(int)ByteIndex.XeenSlayer] = MapScriptByte(MM4Map.C4NewcastleDungeon, MM45Bytes.FoundXeenSlayerSwordC4ND0704, infoXeen);
            m_bytes[(int)ByteIndex.Luna] = MapScriptByte(MM5Map.A4Surface, MM45Bytes.TurnInLunaA4DS1315, infoDark);
            m_bytes[(int)ByteIndex.Ambrose] = MapScriptByte(MM5Map.B1Surface, MM45Bytes.TalkedToAmbroseB1DS1205, infoDark);
            m_bytes[(int)ByteIndex.Songbird] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.TurnInSongbirdA4CKL21115, infoDark);
            m_bytes[(int)ByteIndex.Ogres] = MapScriptByte(MM5Map.B3Surface, MM45Bytes.TurnInOgresB3DS1104, infoDark);
            m_bytes[(int)ByteIndex.Handle] = MapScriptByte(MM5Map.B3Surface, MM45Bytes.TurnInVesparB3DS0701, infoDark);
            m_bytes[(int)ByteIndex.Melon1] = MapScriptByte(MM5Map.B4Surface, MM45Bytes.BringMelon1B4DS0312, infoDark);
            m_bytes[(int)ByteIndex.Melon2] = MapScriptByte(MM5Map.B4Surface, MM45Bytes.BringMelon2B4DS0312, infoDark);
            m_bytes[(int)ByteIndex.FoundMelon1] = MapScriptByte(MM5Map.A4Surface, MM45Bytes.FoundMelon1A4DS0804, infoDark);
            m_bytes[(int)ByteIndex.FoundMelon2] = MapScriptByte(MM5Map.A4Surface, MM45Bytes.FoundMelon2A4DS1403, infoDark);
            m_bytes[(int)ByteIndex.FoundMelon3] = MapScriptByte(MM5Map.A4Surface, MM45Bytes.FoundMelon3A4DS0301, infoDark);
            m_bytes[(int)ByteIndex.FoundMelon4] = MapScriptByte(MM5Map.B4Surface, MM45Bytes.FoundMelon4B4DS0104, infoDark);
            m_bytes[(int)ByteIndex.Sheewana] = MapScriptByte(MM5Map.C4Surface, MM45Bytes.TurnInSheewanaC4DS0107, infoDark);
            m_bytes[(int)ByteIndex.Chalice] = MapScriptByte(MM5Map.D1Surface, MM45Bytes.TurnInChaliceD1DS0108, infoDark);
            m_bytes[(int)ByteIndex.Ring] = MapScriptByte(MM5Map.E2Surface, MM45Bytes.TurnInEctorE2DS0312, infoDark);
            m_bytes[(int)ByteIndex.Glass] = MapScriptByte(MM5Map.E3Surface, MM45Bytes.TurnInCalebE3DS1313, infoDark);
            m_bytes[(int)ByteIndex.Jewel] = MapScriptByte(MM5Map.F4Surface, MM45Bytes.TurnInJewelF4DS0607, infoDark);
            m_bytes[(int)ByteIndex.KilledGettle] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TurnInGettleA4C2327, infoDark);
            m_bytes[(int)ByteIndex.Jethro] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TurnInJethroA4C2224, infoDark);
            m_bytes[(int)ByteIndex.Astra] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.TurnInAstraE3S2014, infoDark);
            m_bytes[(int)ByteIndex.Disk01] = MapScriptByte(MM5Map.A3WesternTowerLevel1, MM45Bytes.EnergyDiskA3WTL10608, infoDark);
            m_bytes[(int)ByteIndex.Disk02] = MapScriptByte(MM5Map.A3WesternTowerLevel1, MM45Bytes.EnergyDiskA3WTL10808, infoDark);
            m_bytes[(int)ByteIndex.Disk03] = MapScriptByte(MM5Map.D1NorthernTowerLevel4, MM45Bytes.EnergyDiskD1NTL40308, infoDark);
            m_bytes[(int)ByteIndex.Disk04] = MapScriptByte(MM5Map.D1NorthernTowerLevel4, MM45Bytes.EnergyDiskD1NTL41108, infoDark);
            m_bytes[(int)ByteIndex.Disk05] = MapScriptByte(MM5Map.D4SouthernTowerLevel3, MM45Bytes.EnergyDiskD4STL30505, infoDark);
            m_bytes[(int)ByteIndex.Disk06] = MapScriptByte(MM5Map.D4SouthernTowerLevel3, MM45Bytes.EnergyDiskD4STL30905, infoDark);
            m_bytes[(int)ByteIndex.Disk07] = MapScriptByte(MM5Map.F3EasternTowerLevel3, MM45Bytes.EnergyDiskF3ETL31108, infoDark);
            m_bytes[(int)ByteIndex.Disk08] = MapScriptByte(MM5Map.F3EasternTowerLevel3, MM45Bytes.EnergyDiskF3ETL30704, infoDark);
            m_bytes[(int)ByteIndex.Disk09] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20015, infoDark);
            m_bytes[(int)ByteIndex.Disk10] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20215, infoDark);
            m_bytes[(int)ByteIndex.Disk11] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20415, infoDark);
            m_bytes[(int)ByteIndex.Disk12] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20815, infoDark);
            m_bytes[(int)ByteIndex.Disk13] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20012, infoDark);
            m_bytes[(int)ByteIndex.Disk14] = MapScriptByte(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20010, infoDark);
            m_bytes[(int)ByteIndex.Disk15] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.EnergyDiskC4TBL50018, infoDark);
            m_bytes[(int)ByteIndex.Disk16] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.EnergyDiskC4TBL53117, infoDark);
            m_bytes[(int)ByteIndex.Disk17] = MapScriptByte(MM5Map.C4Surface, MM45Bytes.EnergyDiskC4DS0107, infoDark);
            m_bytes[(int)ByteIndex.Disk18] = MapScriptByte(MM5Map.D1Surface, MM45Bytes.EnergyDiskD1DS1005, infoDark);
            m_bytes[(int)ByteIndex.Disk19] = MapScriptByte(MM5Map.D3Surface, MM45Bytes.EnergyDiskD3DS1105, infoDark);
            m_bytes[(int)ByteIndex.Disk20] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.EnergyDiskA4C2913, infoDark);
            m_bytes[(int)ByteIndex.TurnInDisks] = MapScriptByte(MM5Map.A4EllingersTowerLevel4, MM45Bytes.TurnInDisksA4ETL40408, infoDark);
            m_bytes[(int)ByteIndex.Egg] = MapScriptByte(MM5Map.D1DragonTowerLevel1, MM45Bytes.TurnInEggD1DTL10605, infoDark);
            m_bytes[(int)ByteIndex.Fire1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.FireScroll1A1CBL10313, infoXeen);
            m_bytes[(int)ByteIndex.Fire2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.FireScroll2A1CBL10613, infoXeen);
            m_bytes[(int)ByteIndex.Fire3] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.FireBrew1C3THML10505, infoXeen);
            m_bytes[(int)ByteIndex.Fire4] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.FireBrew2C3THML20505, infoXeen);
            m_bytes[(int)ByteIndex.Elec1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.ElectricityScroll1A1CBL10309, infoXeen);
            m_bytes[(int)ByteIndex.Elec2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.ElectricityScroll2A1CBL10609, infoXeen);
            m_bytes[(int)ByteIndex.Elec3] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.ElectricBrew1C3THML10511, infoXeen);
            m_bytes[(int)ByteIndex.Elec4] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.ElectricBrew2C3THML20406, infoXeen);
            m_bytes[(int)ByteIndex.Cold1] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.ColdBrew1C3THML10911, infoXeen);
            m_bytes[(int)ByteIndex.Cold2] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.ColdBrew2C3THML20410, infoXeen);
            m_bytes[(int)ByteIndex.Poison1] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.PoisonBrew1C3THML20511, infoXeen);
            m_bytes[(int)ByteIndex.Poison2] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.PoisonBrew2C3THML30410, infoXeen);
            m_bytes[(int)ByteIndex.Poison3] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.PoisonBrew3C3THML31010, infoXeen);
            m_bytes[(int)ByteIndex.Energy1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.EnergyScroll1A1CBL31102, infoXeen);
            m_bytes[(int)ByteIndex.Energy2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.EnergyScroll2A1CBL31201, infoXeen);
            m_bytes[(int)ByteIndex.Energy3] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.EnergyBrew1C3THML30406, infoXeen);
            m_bytes[(int)ByteIndex.Magic1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.MagicScroll1A1CBL21104, infoXeen);
            m_bytes[(int)ByteIndex.Magic2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.MagicScroll2A1CBL31404, infoXeen);
            m_bytes[(int)ByteIndex.Magic3] = MapScriptByte(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.MagicBrew1C3THML31006, infoXeen);
            m_bytes[(int)ByteIndex.Might1] = MapScriptByte(MM4Map.D2BurlockDungeon, MM45Bytes.RedLiquid1D2BD1103, infoXeen);
            m_bytes[(int)ByteIndex.Might2] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.MightSkull1B4CIL11113, infoXeen);
            m_bytes[(int)ByteIndex.Might3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.MightSkull2B4CIL11201, infoXeen);
            m_bytes[(int)ByteIndex.Might4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.MightSkull3B4CIL20314, infoXeen);
            m_bytes[(int)ByteIndex.Might5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.MightSkull4B4CIL30114, infoXeen);
            m_bytes[(int)ByteIndex.Might6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.MightSkull5B4CIL40114, infoXeen);
            m_bytes[(int)ByteIndex.Might7] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice1C4TTT0930, infoXeen);
            m_bytes[(int)ByteIndex.Might8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice2C4TTT1330, infoXeen);
            m_bytes[(int)ByteIndex.Might9] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice3C4TTT0126, infoXeen);
            m_bytes[(int)ByteIndex.Might10] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice4C4TTT0726, infoXeen);
            m_bytes[(int)ByteIndex.Might11] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.MightLiquid2F3DM10507, infoXeen);
            m_bytes[(int)ByteIndex.Might12] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.MightLiquid3F3DM10304, infoXeen);
            m_bytes[(int)ByteIndex.Might13] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.MightLiquid4F3DM21026, infoXeen);
            m_bytes[(int)ByteIndex.Might14] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid5E2DM30201, infoXeen);
            m_bytes[(int)ByteIndex.Might15] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid6E2DM30301, infoXeen);
            m_bytes[(int)ByteIndex.Might16] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid7E2DM30501, infoXeen);
            m_bytes[(int)ByteIndex.Might17] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid8E2DM30601, infoXeen);
            m_bytes[(int)ByteIndex.Might18] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.MightBookD3DTL21006, infoXeen);
            m_bytes[(int)ByteIndex.Might19] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew1A4CKL30802, infoDark);
            m_bytes[(int)ByteIndex.Might20] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew2A4CKL30902, infoDark);
            m_bytes[(int)ByteIndex.Might21] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew3A4CKL30801, infoDark);
            m_bytes[(int)ByteIndex.Might22] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew4A4CKL30901, infoDark);
            m_bytes[(int)ByteIndex.Might23] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew5A4CKL30900, infoDark);
            m_bytes[(int)ByteIndex.Might24] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1aC4TBL10215, infoDark);
            m_bytes[(int)ByteIndex.Might25] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1bC4TBL10215, infoDark);
            m_bytes[(int)ByteIndex.Might26] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1cC4TBL10215, infoDark);
            m_bytes[(int)ByteIndex.Might27] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2aC4TBL21106, infoDark);
            m_bytes[(int)ByteIndex.Might28] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2bC4TBL21106, infoDark);
            m_bytes[(int)ByteIndex.Might29] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2cC4TBL21106, infoDark);
            m_bytes[(int)ByteIndex.Might30] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.MightMagpie1F2LSDL53125, infoDark);
            m_bytes[(int)ByteIndex.Might31] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.MightMagpie2F2LSDL52917, infoDark);
            m_bytes[(int)ByteIndex.Might32] = MapScriptByte(MM5Map.E4Surface, MM45Bytes.MightAppleE4DS1313, infoDark);
            m_bytes[(int)ByteIndex.Might33] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid9A4CS0226, infoDark);
            m_bytes[(int)ByteIndex.Might34] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid10A4CS0426, infoDark);
            m_bytes[(int)ByteIndex.Might35] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid11A4CS0225, infoDark);
            m_bytes[(int)ByteIndex.Might36] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid12A4CS0425, infoDark);
            m_bytes[(int)ByteIndex.Might37] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6aE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Might38] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6bE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Might39] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6cE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Might40] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6dE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Might41] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6eE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Might42] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6fE3SS3004, infoDark);
            m_bytes[(int)ByteIndex.Int1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.IntellectScroll1A1CBL20508, infoXeen);
            m_bytes[(int)ByteIndex.Int2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.IntellectScroll2A1CBL20708, infoXeen);
            m_bytes[(int)ByteIndex.Int3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.IntellectSkull1B4CIL11413, infoXeen);
            m_bytes[(int)ByteIndex.Int4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.IntellectSkull2B4CIL10400, infoXeen);
            m_bytes[(int)ByteIndex.Int5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.IntellectSkull3B4CIL20613, infoXeen);
            m_bytes[(int)ByteIndex.Int6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.IntellectSkull4B4CIL30407, infoXeen);
            m_bytes[(int)ByteIndex.Int7] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.IntellectSkull5B4CIL31003, infoXeen);
            m_bytes[(int)ByteIndex.Int8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.IntellectJuice1C4TTT1219, infoXeen);
            m_bytes[(int)ByteIndex.Int9] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.IntellectJuice2C4TTT1513, infoXeen);
            m_bytes[(int)ByteIndex.Int10] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.IntellectLiquid1F3DM10823, infoXeen);
            m_bytes[(int)ByteIndex.Int11] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.IntellectLiquid2F3DM10722, infoXeen);
            m_bytes[(int)ByteIndex.Int12] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.IntellectLiquid3F3DM21123, infoXeen);
            m_bytes[(int)ByteIndex.Int13] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.IntellectBook1D3DTL20905, infoXeen);
            m_bytes[(int)ByteIndex.Int14] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1aC4TBL21015, infoDark);
            m_bytes[(int)ByteIndex.Int15] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1bC4TBL21015, infoDark);
            m_bytes[(int)ByteIndex.Int16] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1cC4TBL21015, infoDark);
            m_bytes[(int)ByteIndex.Int17] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2aC4TBL21404, infoDark);
            m_bytes[(int)ByteIndex.Int18] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2bC4TBL21404, infoDark);
            m_bytes[(int)ByteIndex.Int19] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2cC4TBL21404, infoDark);
            m_bytes[(int)ByteIndex.Int20] = MapScriptByte(MM5Map.C4Surface, MM45Bytes.IntellectOrangeC4DS0614, infoDark);
            m_bytes[(int)ByteIndex.Int21] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3aE3S2310, infoDark);
            m_bytes[(int)ByteIndex.Int22] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3bE3S2310, infoDark);
            m_bytes[(int)ByteIndex.Int23] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3cE3S2310, infoDark);
            m_bytes[(int)ByteIndex.Int24] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4aE3S1305, infoDark);
            m_bytes[(int)ByteIndex.Int25] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4bE3S1305, infoDark);
            m_bytes[(int)ByteIndex.Int26] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4cE3S1305, infoDark);
            m_bytes[(int)ByteIndex.Int27] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5aE3S1303, infoDark);
            m_bytes[(int)ByteIndex.Int28] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5bE3S1303, infoDark);
            m_bytes[(int)ByteIndex.Int29] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5cE3S1303, infoDark);
            m_bytes[(int)ByteIndex.Per1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.PersonalityScroll1A1CBL20514, infoXeen);
            m_bytes[(int)ByteIndex.Per2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.PersonalityScroll2A1CBL20714, infoXeen);
            m_bytes[(int)ByteIndex.Per3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.PersonalitySkull1B4CIL11506, infoXeen);
            m_bytes[(int)ByteIndex.Per4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.PersonalitySkull2B4CIL10101, infoXeen);
            m_bytes[(int)ByteIndex.Per5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.PersonalitySkull3B4CIL20109, infoXeen);
            m_bytes[(int)ByteIndex.Per6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.PersonalitySkull4B4CIL30103, infoXeen);
            m_bytes[(int)ByteIndex.Per7] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.PersonalitySkull5B4CIL31000, infoXeen);
            m_bytes[(int)ByteIndex.Per8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.PersonalityJuice1C4TTT1105, infoXeen);
            m_bytes[(int)ByteIndex.Per9] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.PersonalityJuice2C4TTT1505, infoXeen);
            m_bytes[(int)ByteIndex.Per10] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.PersonalityLiquid1F3DM10621, infoXeen);
            m_bytes[(int)ByteIndex.Per11] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.PersonalityLiquid2F3DM10620, infoXeen);
            m_bytes[(int)ByteIndex.Per12] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.PersonalityLiquid3F3DM21125, infoXeen);
            m_bytes[(int)ByteIndex.Per13] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.PersonalityBookD3DTL20505, infoXeen);
            m_bytes[(int)ByteIndex.Per14] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew1A4CKL30815, infoDark);
            m_bytes[(int)ByteIndex.Per15] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew2A4CKL30915, infoDark);
            m_bytes[(int)ByteIndex.Per16] = MapScriptByte(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew3A4CKL30914, infoDark);
            m_bytes[(int)ByteIndex.Per17] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1aC4TBL10211, infoDark);
            m_bytes[(int)ByteIndex.Per18] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1bC4TBL10211, infoDark);
            m_bytes[(int)ByteIndex.Per19] = MapScriptByte(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1cC4TBL10211, infoDark);
            m_bytes[(int)ByteIndex.Per20] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2aC4TBL21204, infoDark);
            m_bytes[(int)ByteIndex.Per21] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2bC4TBL21204, infoDark);
            m_bytes[(int)ByteIndex.Per22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2cC4TBL21204, infoDark);
            m_bytes[(int)ByteIndex.Per23] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.PersonalityParakeet1F2LSDL52024, infoDark);
            m_bytes[(int)ByteIndex.Per24] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.PersonalityParakeet2F2LSDL52011, infoDark);
            m_bytes[(int)ByteIndex.Per25] = MapScriptByte(MM5Map.D4Surface, MM45Bytes.PersonalityBlueberriesD4DS0612, infoDark);
            m_bytes[(int)ByteIndex.Per26] = MapScriptByte(MM5Map.F2Lakeside, MM45Bytes.PersonalityCauldronF2L1401, infoDark);
            m_bytes[(int)ByteIndex.End1] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.EnduranceSkull1B4CIL10306, infoXeen);
            m_bytes[(int)ByteIndex.End2] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.EnduranceSkull2B4CIL10805, infoXeen);
            m_bytes[(int)ByteIndex.End3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.EnduranceSkull3B4CIL30202, infoXeen);
            m_bytes[(int)ByteIndex.End4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.EnduranceSkull4B4CIL41410, infoXeen);
            m_bytes[(int)ByteIndex.End5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.EnduranceSkull5B4CIL41103, infoXeen);
            m_bytes[(int)ByteIndex.End6] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice1C4TTT0122, infoXeen);
            m_bytes[(int)ByteIndex.End7] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice2C4TTT0722, infoXeen);
            m_bytes[(int)ByteIndex.End8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice3C4TTT0817, infoXeen);
            m_bytes[(int)ByteIndex.End9] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid1F3DM10817, infoXeen);
            m_bytes[(int)ByteIndex.End10] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid2F3DM10917, infoXeen);
            m_bytes[(int)ByteIndex.End11] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid3F3DM11017, infoXeen);
            m_bytes[(int)ByteIndex.End12] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.EnduranceLiquid4F3DM21022, infoXeen);
            m_bytes[(int)ByteIndex.End13] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1aD1DTL30511, infoDark);
            m_bytes[(int)ByteIndex.End14] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1bD1DTL30511, infoDark);
            m_bytes[(int)ByteIndex.End15] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1cD1DTL30511, infoDark);
            m_bytes[(int)ByteIndex.End16] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1dD1DTL30511, infoDark);
            m_bytes[(int)ByteIndex.End17] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2aD1DTL30911, infoDark);
            m_bytes[(int)ByteIndex.End18] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2bD1DTL30911, infoDark);
            m_bytes[(int)ByteIndex.End19] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2cD1DTL30911, infoDark);
            m_bytes[(int)ByteIndex.End20] = MapScriptByte(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2dD1DTL30911, infoDark);
            m_bytes[(int)ByteIndex.End21] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.EnduranceBookD3DTL20406, infoXeen);
            m_bytes[(int)ByteIndex.End22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3aC4TBL21515, infoDark);
            m_bytes[(int)ByteIndex.End23] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3bC4TBL21515, infoDark);
            m_bytes[(int)ByteIndex.End24] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3cC4TBL21515, infoDark);
            m_bytes[(int)ByteIndex.End25] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4aC4TBL21503, infoDark);
            m_bytes[(int)ByteIndex.End26] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4bC4TBL21503, infoDark);
            m_bytes[(int)ByteIndex.End27] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4cC4TBL21503, infoDark);
            m_bytes[(int)ByteIndex.End28] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle1F2LSDL52731, infoDark);
            m_bytes[(int)ByteIndex.End29] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle2F2LSDL52615, infoDark);
            m_bytes[(int)ByteIndex.End30] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle3F2LSDL53115, infoDark);
            m_bytes[(int)ByteIndex.End31] = MapScriptByte(MM5Map.C4Surface, MM45Bytes.EndurancePearC4DS1304, infoDark);
            m_bytes[(int)ByteIndex.End32] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid5A4CS2008, infoDark);
            m_bytes[(int)ByteIndex.End33] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid6A4CS2208, infoDark);
            m_bytes[(int)ByteIndex.End34] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid7A4CS2007, infoDark);
            m_bytes[(int)ByteIndex.End35] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid8A4CS2207, infoDark);
            m_bytes[(int)ByteIndex.End36] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid9A4CS2006, infoDark);
            m_bytes[(int)ByteIndex.End37] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid10A4CS2206, infoDark);
            m_bytes[(int)ByteIndex.End38] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewaE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End39] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewbE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End40] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewcE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End41] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewdE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End42] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBreweE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End43] = MapScriptByte(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewfE3SS2903, infoDark);
            m_bytes[(int)ByteIndex.End44] = MapScriptByte(MM5Map.F2Lakeside, MM45Bytes.EnduranceCauldronF2L0905, infoDark);
            m_bytes[(int)ByteIndex.Spd1] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.SpeedScroll1A1CBL20712, infoXeen);
            m_bytes[(int)ByteIndex.Spd2] = MapScriptByte(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.SpeedScroll2A1CBL20710, infoXeen);
            m_bytes[(int)ByteIndex.Spd3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.SpeedSkull1B4CIL10908, infoXeen);
            m_bytes[(int)ByteIndex.Spd4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.SpeedSkull2B4CIL10506, infoXeen);
            m_bytes[(int)ByteIndex.Spd5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.SpeedSkull3B4CIL20600, infoXeen);
            m_bytes[(int)ByteIndex.Spd6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.SpeedSkull4B4CIL30500, infoXeen);
            m_bytes[(int)ByteIndex.Spd7] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.SpeedJuice1C4TTT1619, infoXeen);
            m_bytes[(int)ByteIndex.Spd8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.SpeedJuice2C4TTT1819, infoXeen);
            m_bytes[(int)ByteIndex.Spd9] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.SpeedLiquid1F3DM10921, infoXeen);
            m_bytes[(int)ByteIndex.Spd10] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.SpeedLiquid2F3DM10920, infoXeen);
            m_bytes[(int)ByteIndex.Spd11] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.SpeedLiquid3F3DM20403, infoXeen);
            m_bytes[(int)ByteIndex.Spd12] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.SpeedLiquid4F3DM20402, infoXeen);
            m_bytes[(int)ByteIndex.Spd13] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.SpeedLiquid5E2DM30200, infoXeen);
            m_bytes[(int)ByteIndex.Spd14] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.SpeedLiquid6E2DM30300, infoXeen);
            m_bytes[(int)ByteIndex.Spd15] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.SpeedLiquid7D2DM50807, infoXeen);
            m_bytes[(int)ByteIndex.Spd16] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.SpeedLiquid8D2DM50503, infoXeen);
            m_bytes[(int)ByteIndex.Spd17] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.SpeedBookD3DTL20410, infoXeen);
            m_bytes[(int)ByteIndex.Spd18] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1aC4TBL21407, infoDark);
            m_bytes[(int)ByteIndex.Spd19] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1bC4TBL21407, infoDark);
            m_bytes[(int)ByteIndex.Spd20] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1cC4TBL21407, infoDark);
            m_bytes[(int)ByteIndex.Spd21] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2aC4TBL21201, infoDark);
            m_bytes[(int)ByteIndex.Spd22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2bC4TBL21201, infoDark);
            m_bytes[(int)ByteIndex.Spd23] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2cC4TBL21201, infoDark);
            m_bytes[(int)ByteIndex.Spd24] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.SpeedSparrow1F2LSDL52431, infoDark);
            m_bytes[(int)ByteIndex.Spd25] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.SpeedSparrow2F2LSDL53110, infoDark);
            m_bytes[(int)ByteIndex.Spd26] = MapScriptByte(MM5Map.C4Surface, MM45Bytes.SpeedPlumC4DS0612, infoDark);
            m_bytes[(int)ByteIndex.Spd27] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3aE3S0110, infoDark);
            m_bytes[(int)ByteIndex.Spd28] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3bE3S0110, infoDark);
            m_bytes[(int)ByteIndex.Spd29] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3cE3S0110, infoDark);
            m_bytes[(int)ByteIndex.Spd30] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4aE3S2510, infoDark);
            m_bytes[(int)ByteIndex.Spd31] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4bE3S2510, infoDark);
            m_bytes[(int)ByteIndex.Spd32] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4cE3S2510, infoDark);
            m_bytes[(int)ByteIndex.Spd33] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5aE3S0801, infoDark);
            m_bytes[(int)ByteIndex.Spd34] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5bE3S0801, infoDark);
            m_bytes[(int)ByteIndex.Spd35] = MapScriptByte(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5cE3S0801, infoDark);
            m_bytes[(int)ByteIndex.Spd36] = MapScriptByte(MM5Map.F2Lakeside, MM45Bytes.SpeedCauldron1F2L0612, infoDark);
            m_bytes[(int)ByteIndex.Spd37] = MapScriptByte(MM5Map.F2Lakeside, MM45Bytes.SpeedCauldron2F2L1404, infoDark);
            m_bytes[(int)ByteIndex.Acy1] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.AccuracySkull1B4CIL10011, infoXeen);
            m_bytes[(int)ByteIndex.Acy2] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.AccuracySkull2B4CIL11004, infoXeen);
            m_bytes[(int)ByteIndex.Acy3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.AccuracySkull3B4CIL20305, infoXeen);
            m_bytes[(int)ByteIndex.Acy4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.AccuracySkull4B4CIL30401, infoXeen);
            m_bytes[(int)ByteIndex.Acy5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.AccuracySkull5B4CIL40110, infoXeen);
            m_bytes[(int)ByteIndex.Acy6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.AccuracySkull6B4CIL41303, infoXeen);
            m_bytes[(int)ByteIndex.Acy7] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice1C4TTT0124, infoXeen);
            m_bytes[(int)ByteIndex.Acy8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice2C4TTT0724, infoXeen);
            m_bytes[(int)ByteIndex.Acy9] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice3C4TTT0811, infoXeen);
            m_bytes[(int)ByteIndex.Acy10] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice4C4TTT0807, infoXeen);
            m_bytes[(int)ByteIndex.Acy11] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.AccuracyLiquid1F3DM11023, infoXeen);
            m_bytes[(int)ByteIndex.Acy12] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.AccuracyLiquid2F3DM11120, infoXeen);
            m_bytes[(int)ByteIndex.Acy13] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.AccuracyLiquid3F3DM20401, infoXeen);
            m_bytes[(int)ByteIndex.Acy14] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.AccuracyLiquid4F3DM20501, infoXeen);
            m_bytes[(int)ByteIndex.Acy15] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.AccuracyLiquid5D2DM50602, infoXeen);
            m_bytes[(int)ByteIndex.Acy16] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.AccuracyLiquid6D2DM50702, infoXeen);
            m_bytes[(int)ByteIndex.Acy17] = MapScriptByte(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.AccuracyBookD3DTL20511, infoXeen);
            m_bytes[(int)ByteIndex.Acy18] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1aC4TBL21409, infoDark);
            m_bytes[(int)ByteIndex.Acy19] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1bC4TBL21409, infoDark);
            m_bytes[(int)ByteIndex.Acy20] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1cC4TBL21409, infoDark);
            m_bytes[(int)ByteIndex.Acy21] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2aC4TBL21401, infoDark);
            m_bytes[(int)ByteIndex.Acy22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2bC4TBL21401, infoDark);
            m_bytes[(int)ByteIndex.Acy23] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2cC4TBL21401, infoDark);
            m_bytes[(int)ByteIndex.Acy24] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.AccuracyAlbatross1F2LSDL52630, infoDark);
            m_bytes[(int)ByteIndex.Acy25] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.AccuracyAlbatross2F2LSDL52919, infoDark);
            m_bytes[(int)ByteIndex.Acy26] = MapScriptByte(MM5Map.E4Surface, MM45Bytes.AccuracyBananaE4DS0504, infoDark);
            m_bytes[(int)ByteIndex.Lck1] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.LuckSkull1B4CIL10014, infoXeen);
            m_bytes[(int)ByteIndex.Lck2] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.LuckSkull2B4CIL11304, infoXeen);
            m_bytes[(int)ByteIndex.Lck3] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull3B4CIL31511, infoXeen);
            m_bytes[(int)ByteIndex.Lck4] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull4B4CIL31405, infoXeen);
            m_bytes[(int)ByteIndex.Lck5] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull5B4CIL31503, infoXeen);
            m_bytes[(int)ByteIndex.Lck6] = MapScriptByte(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull6B4CIL31202, infoXeen);
            m_bytes[(int)ByteIndex.Lck7] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.LuckJuice1C4TTT2408, infoXeen);
            m_bytes[(int)ByteIndex.Lck8] = MapScriptByte(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.LuckJuice2C4TTT2608, infoXeen);
            m_bytes[(int)ByteIndex.Lck9] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.LuckLiquid1F3DM10207, infoXeen);
            m_bytes[(int)ByteIndex.Lck10] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.LuckLiquid2F3DM10206, infoXeen);
            m_bytes[(int)ByteIndex.Lck11] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.LuckLiquid3F3DM20606, infoXeen);
            m_bytes[(int)ByteIndex.Lck12] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.LuckLiquid4E2DM40805, infoXeen);
            m_bytes[(int)ByteIndex.Lck13] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.LuckLiquid5E2DM40804, infoXeen);
            m_bytes[(int)ByteIndex.Lck14] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotionaC4TBL21306, infoDark);
            m_bytes[(int)ByteIndex.Lck15] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotionbC4TBL21306, infoDark);
            m_bytes[(int)ByteIndex.Lck16] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotioncC4TBL21306, infoDark);
            m_bytes[(int)ByteIndex.Lck17] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.LuckLark1F2LSDL52628, infoDark);
            m_bytes[(int)ByteIndex.Lck18] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.LuckLark2F2LSDL52915, infoDark);
            m_bytes[(int)ByteIndex.Lck19] = MapScriptByte(MM5Map.F4Surface, MM45Bytes.LuckCoconutF4DS0215, infoDark);
            m_bytes[(int)ByteIndex.Lev1] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal1D1DC1327, infoDark);
            m_bytes[(int)ByteIndex.Lev2] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal2D1DC1325, infoDark);
            m_bytes[(int)ByteIndex.Lev3] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal3D1DC1323, infoDark);
            m_bytes[(int)ByteIndex.Lev4] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal4D1DC2423, infoDark);
            m_bytes[(int)ByteIndex.Lev5] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal5D1DC0622, infoDark);
            m_bytes[(int)ByteIndex.Lev6] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal6D1DC1321, infoDark);
            m_bytes[(int)ByteIndex.Lev7] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal7D1DC1721, infoDark);
            m_bytes[(int)ByteIndex.Lev8] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal8D1DC0217, infoDark);
            m_bytes[(int)ByteIndex.Lev9] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal9D1DC0817, infoDark);
            m_bytes[(int)ByteIndex.Lev10] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal10D1DC2217, infoDark);
            m_bytes[(int)ByteIndex.Lev11] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal11D1DC2817, infoDark);
            m_bytes[(int)ByteIndex.Lev12] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal12D1DC0216, infoDark);
            m_bytes[(int)ByteIndex.Lev13] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal13D1DC2816, infoDark);
            m_bytes[(int)ByteIndex.Lev14] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal14D1DC0215, infoDark);
            m_bytes[(int)ByteIndex.Lev15] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal15D1DC2815, infoDark);
            m_bytes[(int)ByteIndex.Lev16] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal16D1DC0214, infoDark);
            m_bytes[(int)ByteIndex.Lev17] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal17D1DC2814, infoDark);
            m_bytes[(int)ByteIndex.Lev18] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal18D1DC0213, infoDark);
            m_bytes[(int)ByteIndex.Lev19] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal19D1DC2813, infoDark);
            m_bytes[(int)ByteIndex.Lev20] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal20D1DC2410, infoDark);
            m_bytes[(int)ByteIndex.Lev21] = MapScriptByte(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal21D1DC0709, infoDark);
            m_bytes[(int)ByteIndex.Lev22] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer1A2SSL30015, infoDark);
            m_bytes[(int)ByteIndex.Lev23] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer2A2SSL31315, infoDark);
            m_bytes[(int)ByteIndex.Lev24] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer3A2SSL31515, infoDark);
            m_bytes[(int)ByteIndex.Lev25] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer4A2SSL30013, infoDark);
            m_bytes[(int)ByteIndex.Lev26] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer5A2SSL31210, infoDark);
            m_bytes[(int)ByteIndex.Lev27] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer6A2SSL31405, infoDark);
            m_bytes[(int)ByteIndex.Lev28] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer7A2SSL31502, infoDark);
            m_bytes[(int)ByteIndex.Lev29] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer8A2SSL30201, infoDark);
            m_bytes[(int)ByteIndex.Lev30] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer9A2SSL30000, infoDark);
            m_bytes[(int)ByteIndex.Lev31] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer10A2SSL31500, infoDark);
            m_bytes[(int)ByteIndex.Lev32] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice1aA1CAD1015, infoDark);
            m_bytes[(int)ByteIndex.Lev33] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice1bA1CAD1015, infoDark);
            m_bytes[(int)ByteIndex.Lev34] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2aA1CAD1113, infoDark);
            m_bytes[(int)ByteIndex.Lev35] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2bA1CAD1113, infoDark);
            m_bytes[(int)ByteIndex.Lev36] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2cA1CAD1113, infoDark);
            m_bytes[(int)ByteIndex.Lev37] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3aA1CAD1513, infoDark);
            m_bytes[(int)ByteIndex.Lev38] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3bA1CAD1513, infoDark);
            m_bytes[(int)ByteIndex.Lev39] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3cA1CAD1513, infoDark);
            m_bytes[(int)ByteIndex.Lev40] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice4aA1CAD1011, infoDark);
            m_bytes[(int)ByteIndex.Lev41] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice4bA1CAD1011, infoDark);
            m_bytes[(int)ByteIndex.Lev42] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice5aA1CAD1007, infoDark);
            m_bytes[(int)ByteIndex.Lev43] = MapScriptByte(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice5bA1CAD1007, infoDark);
            m_bytes[(int)ByteIndex.Lev44] = MapScriptByte(MM5Map.A1CastleAlamarLevel3, MM45Bytes.LevelJuice6aA1CAL30410, infoDark);
            m_bytes[(int)ByteIndex.Lev45] = MapScriptByte(MM5Map.A1CastleAlamarLevel3, MM45Bytes.LevelJuice6bA1CAL30410, infoDark);
            m_bytes[(int)ByteIndex.Lev46] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood1B2NS1414, infoDark);
            m_bytes[(int)ByteIndex.Lev47] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood2B2NS0911, infoDark);
            m_bytes[(int)ByteIndex.Lev48] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood3B2NS1011, infoDark);
            m_bytes[(int)ByteIndex.Lev49] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood4B2NS0910, infoDark);
            m_bytes[(int)ByteIndex.Lev50] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood5B2NS1102, infoDark);
            m_bytes[(int)ByteIndex.Lev51] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood6B2NS0201, infoDark);
            m_bytes[(int)ByteIndex.Lev52] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood7B2NS0801, infoDark);
            m_bytes[(int)ByteIndex.Stat1] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge1C4TBL20114, infoDark);
            m_bytes[(int)ByteIndex.Stat2] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge2C4TBL20214, infoDark);
            m_bytes[(int)ByteIndex.Stat3] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge3C4TBL20113, infoDark);
            m_bytes[(int)ByteIndex.Stat4] = MapScriptByte(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge4C4TBL20213, infoDark);
            m_bytes[(int)ByteIndex.Stat5] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice1E4TH2031, infoDark);
            m_bytes[(int)ByteIndex.Stat6] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice2E4TH2131, infoDark);
            m_bytes[(int)ByteIndex.Stat7] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice3E4TH2431, infoDark);
            m_bytes[(int)ByteIndex.Stat8] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice4E4TH2531, infoDark);
            m_bytes[(int)ByteIndex.Stat9] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice5E4TH2030, infoDark);
            m_bytes[(int)ByteIndex.Stat10] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice6E4TH2530, infoDark);
            m_bytes[(int)ByteIndex.Stat11] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice7E4TH0722, infoDark);
            m_bytes[(int)ByteIndex.Stat12] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice8E4TH0822, infoDark);
            m_bytes[(int)ByteIndex.Stat13] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice9E4TH1418, infoDark);
            m_bytes[(int)ByteIndex.Stat14] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice10E4TH1417, infoDark);
            m_bytes[(int)ByteIndex.Stat15] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice11E4TH2511, infoDark);
            m_bytes[(int)ByteIndex.Stat16] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice12E4TH0110, infoDark);
            m_bytes[(int)ByteIndex.Stat17] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice13E4TH0210, infoDark);
            m_bytes[(int)ByteIndex.Stat18] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice14E4TH0910, infoDark);
            m_bytes[(int)ByteIndex.Stat19] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice15E4TH1010, infoDark);
            m_bytes[(int)ByteIndex.Stat20] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice16E4TH2510, infoDark);
            m_bytes[(int)ByteIndex.Stat21] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice17E4TH1509, infoDark);
            m_bytes[(int)ByteIndex.Stat22] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice18E4TH1609, infoDark);
            m_bytes[(int)ByteIndex.Stat23] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice19E4TH1709, infoDark);
            m_bytes[(int)ByteIndex.TalkValio] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.TalkValioA4CS3126, infoDark);
            m_bytes[(int)ByteIndex.ReturnValio] = MapScriptByte(MM5Map.A4CastleviewSewer, MM45Bytes.ReturnValioA4CS3126, infoDark);
            m_bytes[(int)ByteIndex.Paladin2] = MapScriptByte(MM5Map.C2Surface, MM45Bytes.PaladinC2DS1105, infoDark);
            m_bytes[(int)ByteIndex.Paladin3] = MapScriptByte(MM5Map.C2Surface, MM45Bytes.PaladinC2DS1100, infoDark);
            m_bytes[(int)ByteIndex.Paladin4] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0010, infoDark);
            m_bytes[(int)ByteIndex.Paladin5] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0000, infoDark);
            m_bytes[(int)ByteIndex.Paladin6] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0510, infoDark);
            m_bytes[(int)ByteIndex.Paladin7] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0505, infoDark);
            m_bytes[(int)ByteIndex.Paladin8] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0500, infoDark);
            m_bytes[(int)ByteIndex.Case1] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case1F3V1003, infoXeen);
            m_bytes[(int)ByteIndex.Case2] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case2F3V1005, infoXeen);
            m_bytes[(int)ByteIndex.Case3] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case3F3V1103, infoXeen);
            m_bytes[(int)ByteIndex.Case4] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case4F3V1105, infoXeen);
            m_bytes[(int)ByteIndex.Case5] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case5F3V1212, infoXeen);
            m_bytes[(int)ByteIndex.Case6] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case6F3V0812, infoXeen);
            m_bytes[(int)ByteIndex.Case7] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case7F3V0903, infoXeen);
            m_bytes[(int)ByteIndex.Case8] = MapScriptByte(MM4Map.F3Vertigo, MM45Bytes.Case8F3V0905, infoXeen);
            m_bytes[(int)ByteIndex.WordMaster] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel1, MM45Bytes.WordMasterE3DOD12000, infoDark);
            m_bytes[(int)ByteIndex.TTLever] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel3, MM45Bytes.TTLeverE3DOD31703, infoDark);
            m_bytes[(int)ByteIndex.Gold1] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10230, infoXeen);
            m_bytes[(int)ByteIndex.Gold2] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10930, infoXeen);
            m_bytes[(int)ByteIndex.Gold3] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10525, infoXeen);
            m_bytes[(int)ByteIndex.Gold4] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM11430, infoXeen);
            m_bytes[(int)ByteIndex.Gold5] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.Gold2F3DM11430, infoXeen);
            m_bytes[(int)ByteIndex.Gold6] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10529, infoXeen);
            m_bytes[(int)ByteIndex.Gold7] = MapScriptByte(MM4Map.F3DwarfMine1, MM45Bytes.Gold2F3DM10529, infoXeen);
            m_bytes[(int)ByteIndex.Gold8] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM20112, infoXeen);
            m_bytes[(int)ByteIndex.Gold9] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21217, infoXeen);
            m_bytes[(int)ByteIndex.Gold10] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21217, infoXeen);
            m_bytes[(int)ByteIndex.Gold11] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM20103, infoXeen);
            m_bytes[(int)ByteIndex.Gold12] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM20103, infoXeen);
            m_bytes[(int)ByteIndex.Gold13] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21301, infoXeen);
            m_bytes[(int)ByteIndex.Gold14] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21301, infoXeen);
            m_bytes[(int)ByteIndex.Gold15] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21414, infoXeen);
            m_bytes[(int)ByteIndex.Gold16] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21414, infoXeen);
            m_bytes[(int)ByteIndex.Gold17] = MapScriptByte(MM4Map.F3DwarfMine2, MM45Bytes.Gold3F3DM21414, infoXeen);
            m_bytes[(int)ByteIndex.Gold18] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31114, infoXeen);
            m_bytes[(int)ByteIndex.Gold19] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31714, infoXeen);
            m_bytes[(int)ByteIndex.Gold20] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32010, infoXeen);
            m_bytes[(int)ByteIndex.Gold21] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31206, infoXeen);
            m_bytes[(int)ByteIndex.Gold22] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31202, infoXeen);
            m_bytes[(int)ByteIndex.Gold23] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31414, infoXeen);
            m_bytes[(int)ByteIndex.Gold24] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM31414, infoXeen);
            m_bytes[(int)ByteIndex.Gold25] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM33014, infoXeen);
            m_bytes[(int)ByteIndex.Gold26] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM33014, infoXeen);
            m_bytes[(int)ByteIndex.Gold27] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM33014, infoXeen);
            m_bytes[(int)ByteIndex.Gold28] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31807, infoXeen);
            m_bytes[(int)ByteIndex.Gold29] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM31807, infoXeen);
            m_bytes[(int)ByteIndex.Gold30] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM31807, infoXeen);
            m_bytes[(int)ByteIndex.Gold31] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32907, infoXeen);
            m_bytes[(int)ByteIndex.Gold32] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM32907, infoXeen);
            m_bytes[(int)ByteIndex.Gold33] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM32907, infoXeen);
            m_bytes[(int)ByteIndex.Gold34] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32902, infoXeen);
            m_bytes[(int)ByteIndex.Gold35] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM32902, infoXeen);
            m_bytes[(int)ByteIndex.Gold36] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM32902, infoXeen);
            m_bytes[(int)ByteIndex.Gold37] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM33009, infoXeen);
            m_bytes[(int)ByteIndex.Gold38] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM33009, infoXeen);
            m_bytes[(int)ByteIndex.Gold39] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM33009, infoXeen);
            m_bytes[(int)ByteIndex.Gold40] = MapScriptByte(MM4Map.E2DwarfMine3, MM45Bytes.Gold4E2DM33009, infoXeen);
            m_bytes[(int)ByteIndex.Gold41] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.GoldE2DM40414, infoXeen);
            m_bytes[(int)ByteIndex.Gold42] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.Gold2E2DM40414, infoXeen);
            m_bytes[(int)ByteIndex.Gold43] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.Gold3E2DM40414, infoXeen);
            m_bytes[(int)ByteIndex.Gold44] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.GoldE2DM40510, infoXeen);
            m_bytes[(int)ByteIndex.Gold45] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.Gold2E2DM40510, infoXeen);
            m_bytes[(int)ByteIndex.Gold46] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.Gold3E2DM40510, infoXeen);
            m_bytes[(int)ByteIndex.Gold47] = MapScriptByte(MM4Map.E2DwarfMine4, MM45Bytes.Gold4E2DM40510, infoXeen);
            m_bytes[(int)ByteIndex.Gold48] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM50114, infoXeen);
            m_bytes[(int)ByteIndex.Gold49] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM50114, infoXeen);
            m_bytes[(int)ByteIndex.Gold50] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM50114, infoXeen);
            m_bytes[(int)ByteIndex.Gold51] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM51002, infoXeen);
            m_bytes[(int)ByteIndex.Gold52] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM51002, infoXeen);
            m_bytes[(int)ByteIndex.Gold53] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM51002, infoXeen);
            m_bytes[(int)ByteIndex.Gold54] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM51010, infoXeen);
            m_bytes[(int)ByteIndex.Gold55] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM51010, infoXeen);
            m_bytes[(int)ByteIndex.Gold56] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM51010, infoXeen);
            m_bytes[(int)ByteIndex.Gold57] = MapScriptByte(MM4Map.D2DwarfMine5, MM45Bytes.Gold4D2DM51010, infoXeen);
            m_bytes[(int)ByteIndex.Gold58] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3129, infoXeen);
            m_bytes[(int)ByteIndex.Gold59] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3129, infoXeen);
            m_bytes[(int)ByteIndex.Gold60] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1521, infoXeen); 
            m_bytes[(int)ByteIndex.Gold61] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1521, infoXeen); 
            m_bytes[(int)ByteIndex.Gold62] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2915, infoXeen); 
            m_bytes[(int)ByteIndex.Gold63] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2915, infoXeen); 
            m_bytes[(int)ByteIndex.Gold64] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2906, infoXeen); 
            m_bytes[(int)ByteIndex.Gold65] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2906, infoXeen);
            m_bytes[(int)ByteIndex.Gold66] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0202, infoXeen);
            m_bytes[(int)ByteIndex.Gold67] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0202, infoXeen);
            m_bytes[(int)ByteIndex.Gold68] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1202, infoXeen);
            m_bytes[(int)ByteIndex.Gold69] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1202, infoXeen);
            m_bytes[(int)ByteIndex.Gold70] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1931, infoXeen);
            m_bytes[(int)ByteIndex.Gold71] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1931, infoXeen);
            m_bytes[(int)ByteIndex.Gold72] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA1931, infoXeen);
            m_bytes[(int)ByteIndex.Gold73] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0130, infoXeen);
            m_bytes[(int)ByteIndex.Gold74] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0130, infoXeen);
            m_bytes[(int)ByteIndex.Gold75] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0130, infoXeen);
            m_bytes[(int)ByteIndex.Gold76] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1530, infoXeen);
            m_bytes[(int)ByteIndex.Gold77] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1530, infoXeen);
            m_bytes[(int)ByteIndex.Gold78] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA1530, infoXeen);
            m_bytes[(int)ByteIndex.Gold79] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3020, infoXeen);
            m_bytes[(int)ByteIndex.Gold80] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3020, infoXeen);
            m_bytes[(int)ByteIndex.Gold81] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA3020, infoXeen);
            m_bytes[(int)ByteIndex.Gold82] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2217, infoXeen);
            m_bytes[(int)ByteIndex.Gold83] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2217, infoXeen);
            m_bytes[(int)ByteIndex.Gold84] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA2217, infoXeen);
            m_bytes[(int)ByteIndex.Gold85] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0108, infoXeen);
            m_bytes[(int)ByteIndex.Gold86] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0108, infoXeen);
            m_bytes[(int)ByteIndex.Gold87] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0108, infoXeen);
            m_bytes[(int)ByteIndex.Gold88] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3003, infoXeen);
            m_bytes[(int)ByteIndex.Gold89] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3003, infoXeen);
            m_bytes[(int)ByteIndex.Gold90] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA3003, infoXeen);
            m_bytes[(int)ByteIndex.Gold91] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0125, infoXeen);
            m_bytes[(int)ByteIndex.Gold92] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0125, infoXeen);
            m_bytes[(int)ByteIndex.Gold93] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0125, infoXeen);
            m_bytes[(int)ByteIndex.Gold94] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0125, infoXeen);
            m_bytes[(int)ByteIndex.Gold95] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0523, infoXeen);
            m_bytes[(int)ByteIndex.Gold96] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0523, infoXeen);
            m_bytes[(int)ByteIndex.Gold97] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0523, infoXeen);
            m_bytes[(int)ByteIndex.Gold98] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0523, infoXeen);
            m_bytes[(int)ByteIndex.Gold99] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0411, infoXeen);
            m_bytes[(int)ByteIndex.Gold100] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0411, infoXeen);
            m_bytes[(int)ByteIndex.Gold101] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0411, infoXeen);
            m_bytes[(int)ByteIndex.Gold102] = MapScriptByte(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0411, infoXeen);
            m_bytes[(int)ByteIndex.Gold103] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1531, infoXeen);
            m_bytes[(int)ByteIndex.Gold104] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1531, infoXeen);
            m_bytes[(int)ByteIndex.Gold105] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1531, infoXeen);
            m_bytes[(int)ByteIndex.Gold106] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1623, infoXeen);
            m_bytes[(int)ByteIndex.Gold107] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1623, infoXeen);
            m_bytes[(int)ByteIndex.Gold108] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1623, infoXeen);
            m_bytes[(int)ByteIndex.Gold109] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0813, infoXeen);
            m_bytes[(int)ByteIndex.Gold110] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0813, infoXeen);
            m_bytes[(int)ByteIndex.Gold111] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0813, infoXeen);
            m_bytes[(int)ByteIndex.Gold112] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK2712, infoXeen);
            m_bytes[(int)ByteIndex.Gold113] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK2712, infoXeen);
            m_bytes[(int)ByteIndex.Gold114] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK2712, infoXeen);
            m_bytes[(int)ByteIndex.Gold115] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1526, infoXeen);
            m_bytes[(int)ByteIndex.Gold116] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1526, infoXeen);
            m_bytes[(int)ByteIndex.Gold117] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1526, infoXeen);
            m_bytes[(int)ByteIndex.Gold118] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK1526, infoXeen);
            m_bytes[(int)ByteIndex.Gold119] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK3026, infoXeen);
            m_bytes[(int)ByteIndex.Gold120] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK3026, infoXeen);
            m_bytes[(int)ByteIndex.Gold121] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK3026, infoXeen);
            m_bytes[(int)ByteIndex.Gold122] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK3026, infoXeen);
            m_bytes[(int)ByteIndex.Gold123] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK2819, infoXeen);
            m_bytes[(int)ByteIndex.Gold124] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK2819, infoXeen);
            m_bytes[(int)ByteIndex.Gold125] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK2819, infoXeen);
            m_bytes[(int)ByteIndex.Gold126] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK2819, infoXeen);
            m_bytes[(int)ByteIndex.Gold127] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0414, infoXeen);
            m_bytes[(int)ByteIndex.Gold128] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0414, infoXeen);
            m_bytes[(int)ByteIndex.Gold129] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0414, infoXeen);
            m_bytes[(int)ByteIndex.Gold130] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK0414, infoXeen);
            m_bytes[(int)ByteIndex.Gold131] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0231, infoXeen);
            m_bytes[(int)ByteIndex.Gold132] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0231, infoXeen);
            m_bytes[(int)ByteIndex.Gold133] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0231, infoXeen);
            m_bytes[(int)ByteIndex.Gold134] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK0231, infoXeen);
            m_bytes[(int)ByteIndex.Gold135] = MapScriptByte(MM4Map.DeepMineKappa, MM45Bytes.Gold5DMK0231, infoXeen);
            m_bytes[(int)ByteIndex.Gold136] = MapScriptByte(MM4Map.DeepMineTheta, MM45Bytes.GoldDMT3107, infoXeen);
            m_bytes[(int)ByteIndex.Gold137] = MapScriptByte(MM4Map.DeepMineTheta, MM45Bytes.Gold2DMT3107, infoXeen);
            m_bytes[(int)ByteIndex.Gold138] = MapScriptByte(MM4Map.DeepMineTheta, MM45Bytes.Gold3DMT3107, infoXeen);
            m_bytes[(int)ByteIndex.Gold139] = MapScriptByte(MM4Map.DeepMineTheta, MM45Bytes.Gold4DMT3107, infoXeen);
            m_bytes[(int)ByteIndex.Gold140] = MapScriptByte(MM4Map.DeepMineTheta, MM45Bytes.Gold5DMT3107, infoXeen);
            m_bytes[(int)ByteIndex.Gold141] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO3024, infoXeen);
            m_bytes[(int)ByteIndex.Gold142] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO3024, infoXeen);
            m_bytes[(int)ByteIndex.Gold143] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO3024, infoXeen);
            m_bytes[(int)ByteIndex.Gold144] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO3024, infoXeen);
            m_bytes[(int)ByteIndex.Gold145] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO3024, infoXeen);
            m_bytes[(int)ByteIndex.Gold146] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO0515, infoXeen);
            m_bytes[(int)ByteIndex.Gold147] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO0515, infoXeen);
            m_bytes[(int)ByteIndex.Gold148] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO0515, infoXeen);
            m_bytes[(int)ByteIndex.Gold149] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO0515, infoXeen);
            m_bytes[(int)ByteIndex.Gold150] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO0515, infoXeen);
            m_bytes[(int)ByteIndex.Gold151] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO2114, infoXeen);
            m_bytes[(int)ByteIndex.Gold152] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO2114, infoXeen);
            m_bytes[(int)ByteIndex.Gold153] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO2114, infoXeen);
            m_bytes[(int)ByteIndex.Gold154] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO2114, infoXeen);
            m_bytes[(int)ByteIndex.Gold155] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO2114, infoXeen);
            m_bytes[(int)ByteIndex.Gold156] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO0506, infoXeen);
            m_bytes[(int)ByteIndex.Gold157] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO0506, infoXeen);
            m_bytes[(int)ByteIndex.Gold158] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO0506, infoXeen);
            m_bytes[(int)ByteIndex.Gold159] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO0506, infoXeen);
            m_bytes[(int)ByteIndex.Gold160] = MapScriptByte(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO0506, infoXeen);
            m_bytes[(int)ByteIndex.Destroy1] = MapScriptByte(MM5Map.A2Surface, MM45Bytes.DestroyA2DS0702, infoDark);
            m_bytes[(int)ByteIndex.Destroy2] = MapScriptByte(MM4Map.A3Surface, MM45Bytes.DestroyA3CS0814, infoXeen);
            m_bytes[(int)ByteIndex.Destroy3] = MapScriptByte(MM4Map.A4Surface, MM45Bytes.DestroyA4CS1008, infoXeen);
            m_bytes[(int)ByteIndex.Destroy4] = MapScriptByte(MM5Map.B2Surface, MM45Bytes.DestroyB2DS0002, infoDark);
            m_bytes[(int)ByteIndex.Destroy5] = MapScriptByte(MM5Map.B3Surface, MM45Bytes.DestroyB3DS1310, infoDark);
            m_bytes[(int)ByteIndex.Destroy6] = MapScriptByte(MM4Map.B4Surface, MM45Bytes.DestroyB4CS0207, infoXeen);
            m_bytes[(int)ByteIndex.Destroy7] = MapScriptByte(MM4Map.B4Surface, MM45Bytes.DestroyB4CS1012, infoXeen);
            m_bytes[(int)ByteIndex.Destroy8] = MapScriptByte(MM5Map.C1Surface, MM45Bytes.DestroyC1DS0911, infoDark);
            m_bytes[(int)ByteIndex.Destroy9] = MapScriptByte(MM4Map.C2Surface, MM45Bytes.DestroyC2CS0108, infoXeen);
            m_bytes[(int)ByteIndex.Destroy10] = MapScriptByte(MM4Map.C2Surface, MM45Bytes.DestroyC2CS0500, infoXeen);
            m_bytes[(int)ByteIndex.Destroy11] = MapScriptByte(MM4Map.C4Surface, MM45Bytes.DestroyC4CS0111, infoXeen);
            m_bytes[(int)ByteIndex.Destroy12] = MapScriptByte(MM5Map.D1Surface, MM45Bytes.DestroyD1DS0012, infoDark);
            m_bytes[(int)ByteIndex.Destroy13] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX0505, infoXeen);
            m_bytes[(int)ByteIndex.Destroy14] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2505, infoXeen);
            m_bytes[(int)ByteIndex.Destroy15] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2729, infoXeen);
            m_bytes[(int)ByteIndex.Destroy16] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2827, infoXeen);
            m_bytes[(int)ByteIndex.Destroy17] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2830, infoXeen);
            m_bytes[(int)ByteIndex.Destroy18] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2928, infoXeen);
            m_bytes[(int)ByteIndex.Destroy19] = MapScriptByte(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX3030, infoXeen);
            m_bytes[(int)ByteIndex.Destroy20] = MapScriptByte(MM5Map.D3Surface, MM45Bytes.DestroyD3DS0908, infoDark);
            m_bytes[(int)ByteIndex.Destroy21] = MapScriptByte(MM5Map.D3Surface, MM45Bytes.DestroyD3DS0307, infoDark);
            m_bytes[(int)ByteIndex.Destroy22] = MapScriptByte(MM4Map.E1VolcanoCaveLevel1, MM45Bytes.DestroyE1VCL10015, infoXeen);
            m_bytes[(int)ByteIndex.Destroy23] = MapScriptByte(MM4Map.E1VolcanoCaveLevel1, MM45Bytes.DestroyE1VCL10909, infoXeen);
            m_bytes[(int)ByteIndex.Destroy24] = MapScriptByte(MM4Map.E2Surface, MM45Bytes.DestroyE2CS0902, infoXeen);
            m_bytes[(int)ByteIndex.Destroy25] = MapScriptByte(MM4Map.E3Surface, MM45Bytes.DestroyE3CS1413, infoXeen);
            m_bytes[(int)ByteIndex.Destroy26] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20420, infoDark);
            m_bytes[(int)ByteIndex.Destroy27] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20620, infoDark);
            m_bytes[(int)ByteIndex.Destroy28] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20820, infoDark);
            m_bytes[(int)ByteIndex.Destroy29] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL40203, infoDark);
            m_bytes[(int)ByteIndex.Destroy30] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL40329, infoDark);
            m_bytes[(int)ByteIndex.Destroy31] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL42703, infoDark);
            m_bytes[(int)ByteIndex.Destroy32] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL42829, infoDark);
            m_bytes[(int)ByteIndex.Destroy33] = MapScriptByte(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY0206, infoXeen);
            m_bytes[(int)ByteIndex.Destroy34] = MapScriptByte(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY0217, infoXeen);
            m_bytes[(int)ByteIndex.Destroy35] = MapScriptByte(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY2507, infoXeen);
            m_bytes[(int)ByteIndex.Destroy36] = MapScriptByte(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY2725, infoXeen);
            m_bytes[(int)ByteIndex.Destroy37] = MapScriptByte(MM4Map.E4Surface, MM45Bytes.DestroyE4CS0914, infoXeen);
            m_bytes[(int)ByteIndex.Destroy38] = MapScriptByte(MM5Map.F1Surface, MM45Bytes.DestroyF1DS1000, infoDark);
            m_bytes[(int)ByteIndex.Destroy39] = MapScriptByte(MM4Map.F2Surface, MM45Bytes.DestroyF2CS1205, infoXeen);
            m_bytes[(int)ByteIndex.Destroy40] = MapScriptByte(MM4Map.F2Surface, MM45Bytes.DestroyF2CS1303, infoXeen);
            m_bytes[(int)ByteIndex.Destroy41] = MapScriptByte(MM4Map.F3Surface, MM45Bytes.DestroyF3CS1214, infoXeen);
            m_bytes[(int)ByteIndex.Destroy42] = MapScriptByte(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0419, infoXeen);
            m_bytes[(int)ByteIndex.Destroy43] = MapScriptByte(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0427, infoXeen);
            m_bytes[(int)ByteIndex.Destroy44] = MapScriptByte(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0807, infoXeen);
            m_bytes[(int)ByteIndex.Destroy45] = MapScriptByte(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC2228, infoXeen);
            m_bytes[(int)ByteIndex.Destroy46] = MapScriptByte(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC2720, infoXeen);
            m_bytes[(int)ByteIndex.Lamp1] = MapScriptByte(MM5Map.A2SkyroadA2, MM45Bytes.LampA2SA21214, infoDark);
            m_bytes[(int)ByteIndex.Lamp2] = MapScriptByte(MM5Map.B1SkyroadB1, MM45Bytes.LampB1SB10507, infoDark);
            m_bytes[(int)ByteIndex.Lamp3] = MapScriptByte(MM5Map.B2SkyroadB2, MM45Bytes.LampB2SB20514, infoDark);
            m_bytes[(int)ByteIndex.Lamp4] = MapScriptByte(MM5Map.C3SkyroadC3, MM45Bytes.LampC3SC30700, infoDark);
            m_bytes[(int)ByteIndex.Lamp5] = MapScriptByte(MM5Map.E1SkyroadE1, MM45Bytes.LampE1SE11201, infoDark);
            m_bytes[(int)ByteIndex.Lamp6] = MapScriptByte(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20812, infoDark);
            m_bytes[(int)ByteIndex.Lamp7] = MapScriptByte(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20308, infoDark);
            m_bytes[(int)ByteIndex.Lamp8] = MapScriptByte(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE21208, infoDark);
            m_bytes[(int)ByteIndex.Lamp9] = MapScriptByte(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20803, infoDark);
            m_bytes[(int)ByteIndex.Lamp10] = MapScriptByte(MM5Map.F1SkyroadF1, MM45Bytes.LampF1SF10303, infoDark);
            m_bytes[(int)ByteIndex.Lamp11] = MapScriptByte(MM5Map.F2SkyroadF2, MM45Bytes.LampF2SF20112, infoDark);
            m_bytes[(int)ByteIndex.Lamp12] = MapScriptByte(MM5Map.B2Surface, MM45Bytes.LampB2DS1309, infoDark);
            m_bytes[(int)ByteIndex.Lamp13] = MapScriptByte(MM5Map.B2Surface, MM45Bytes.LampB2DS1202, infoDark);
            m_bytes[(int)ByteIndex.Lamp14] = MapScriptByte(MM5Map.C2Surface, MM45Bytes.LampC2DS0315, infoDark);
            m_bytes[(int)ByteIndex.Lamp15] = MapScriptByte(MM5Map.C2Surface, MM45Bytes.LampC2DS0611, infoDark);
            m_bytes[(int)ByteIndex.Lamp16] = MapScriptByte(MM5Map.C2Surface, MM45Bytes.LampC2DS0206, infoDark);
            m_bytes[(int)ByteIndex.Lamp17] = MapScriptByte(MM5Map.D2Surface, MM45Bytes.LampD2DS0015, infoDark);
            m_bytes[(int)ByteIndex.Lamp18] = MapScriptByte(MM5Map.D3Surface, MM45Bytes.LampD3DS0712, infoDark);
            m_bytes[(int)ByteIndex.L7Item1] = MapScriptByte(MM4Map.E1DragonCave, MM45Bytes.L7ItemE1DC3100, infoXeen);
            m_bytes[(int)ByteIndex.L7Item2] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10518, infoDark);
            m_bytes[(int)ByteIndex.L7Item3] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10516, infoDark);
            m_bytes[(int)ByteIndex.L7Item4] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10514, infoDark);
            m_bytes[(int)ByteIndex.L7Item5] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10201, infoDark);
            m_bytes[(int)ByteIndex.L7Item6] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10401, infoDark);
            m_bytes[(int)ByteIndex.L7Item7] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL11001, infoDark);
            m_bytes[(int)ByteIndex.L7Item8] = MapScriptByte(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL11201, infoDark);
            m_bytes[(int)ByteIndex.L7Item9] = MapScriptByte(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.L7ItemA2SSL30615, infoDark);
            m_bytes[(int)ByteIndex.L7Item10] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL22613, infoDark);
            m_bytes[(int)ByteIndex.L7Item11] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL23013, infoDark);
            m_bytes[(int)ByteIndex.L7Item12] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL20704, infoDark);
            m_bytes[(int)ByteIndex.L7Item13] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL20904, infoDark);
            m_bytes[(int)ByteIndex.L7Item14] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21104, infoDark);
            m_bytes[(int)ByteIndex.L7Item15] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21304, infoDark);
            m_bytes[(int)ByteIndex.L7Item16] = MapScriptByte(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21504, infoDark);
            m_bytes[(int)ByteIndex.L7Item17] = MapScriptByte(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40509, infoDark);
            m_bytes[(int)ByteIndex.L7Item18] = MapScriptByte(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40909, infoDark);
            m_bytes[(int)ByteIndex.L7Item19] = MapScriptByte(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40308, infoDark);
            m_bytes[(int)ByteIndex.L7Item20] = MapScriptByte(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL41108, infoDark);
            m_bytes[(int)ByteIndex.L7Item21] = MapScriptByte(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40706, infoDark);
            m_bytes[(int)ByteIndex.L7Item22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.L7ItemC4TBL51611, infoDark);
            m_bytes[(int)ByteIndex.L7Item23] = MapScriptByte(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.L7ItemD2TGPL12315, infoDark);
            m_bytes[(int)ByteIndex.L7Item24] = MapScriptByte(MM5Map.E4TrollHoles, MM45Bytes.L7ItemE4TH0724, infoDark);
            m_bytes[(int)ByteIndex.L7Item25] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N0105, infoDark);
            m_bytes[(int)ByteIndex.L7Item26] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N0205, infoDark);
            m_bytes[(int)ByteIndex.L7Item27] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1204, infoDark);
            m_bytes[(int)ByteIndex.L7Item28] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1404, infoDark);
            m_bytes[(int)ByteIndex.L7Item29] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1203, infoDark);
            m_bytes[(int)ByteIndex.L7Item30] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1403, infoDark);
            m_bytes[(int)ByteIndex.L7Item31] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1002, infoDark);
            m_bytes[(int)ByteIndex.L7Item32] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1402, infoDark);
            m_bytes[(int)ByteIndex.L7Item33] = MapScriptByte(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1401, infoDark);
            m_bytes[(int)ByteIndex.L7Item34] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0610, infoDark);
            m_bytes[(int)ByteIndex.L7Item35] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0106, infoDark);
            m_bytes[(int)ByteIndex.L7Item36] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0102, infoDark);
            m_bytes[(int)ByteIndex.L7Item37] = MapScriptByte(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS1001, infoDark);
            m_bytes[(int)ByteIndex.Draco] = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.OpenCoffinD4N0114, infoXeen);
            m_bytes[(int)ByteIndex.PyNumLev] = MapScriptByte(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.LeverD2TGPL12520, infoDark);
            m_bytes[(int)ByteIndex.PyNumTreas] = MapScriptByte(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.TreasureD2TGPL12315, infoDark);
            m_bytes[(int)ByteIndex.Tree1]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0111, infoXeen);
            m_bytes[(int)ByteIndex.Tree2]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0210, infoXeen);
            m_bytes[(int)ByteIndex.Tree3]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N1008, infoXeen);
            m_bytes[(int)ByteIndex.Tree4]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0107, infoXeen);
            m_bytes[(int)ByteIndex.Tree5]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0902, infoXeen);
            m_bytes[(int)ByteIndex.Tree6]  = MapScriptByte(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N1102, infoXeen);
            m_bytes[(int)ByteIndex.Tree7]  = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1328, infoDark);
            m_bytes[(int)ByteIndex.Tree8]  = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1728, infoDark);
            m_bytes[(int)ByteIndex.Tree9]  = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1326, infoDark);
            m_bytes[(int)ByteIndex.Tree10] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1726, infoDark);
            m_bytes[(int)ByteIndex.Tree11] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C0625, infoDark);
            m_bytes[(int)ByteIndex.Tree12] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1319, infoDark);
            m_bytes[(int)ByteIndex.Tree13] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1617, infoDark);
            m_bytes[(int)ByteIndex.Tree14] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C0814, infoDark);
            m_bytes[(int)ByteIndex.Tree15] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C0914, infoDark);
            m_bytes[(int)ByteIndex.Tree16] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1014, infoDark);
            m_bytes[(int)ByteIndex.Tree17] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1408, infoDark);
            m_bytes[(int)ByteIndex.Tree18] = MapScriptByte(MM5Map.A4Castleview, MM45Bytes.TreeA4C1608, infoDark);
            m_bytes[(int)ByteIndex.Tree19] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R3028, infoXeen);
            m_bytes[(int)ByteIndex.Tree20] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0126, infoXeen);
            m_bytes[(int)ByteIndex.Tree21] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0326, infoXeen);
            m_bytes[(int)ByteIndex.Tree22] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R3023, infoXeen);
            m_bytes[(int)ByteIndex.Tree23] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R2720, infoXeen);
            m_bytes[(int)ByteIndex.Tree24] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0908, infoXeen);
            m_bytes[(int)ByteIndex.Tree25] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1206, infoXeen);
            m_bytes[(int)ByteIndex.Tree26] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0905, infoXeen);
            m_bytes[(int)ByteIndex.Tree27] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1205, infoXeen);
            m_bytes[(int)ByteIndex.Tree28] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R2105, infoXeen);
            m_bytes[(int)ByteIndex.Tree29] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1303, infoXeen);
            m_bytes[(int)ByteIndex.Tree30] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1403, infoXeen);
            m_bytes[(int)ByteIndex.Tree31] = MapScriptByte(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0902, infoXeen);
            m_bytes[(int)ByteIndex.Bark1] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50000, infoDark);
            m_bytes[(int)ByteIndex.Bark2] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50000, infoDark);
            m_bytes[(int)ByteIndex.Bark3] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50000, infoDark);
            m_bytes[(int)ByteIndex.Bark4] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50000, infoDark);
            m_bytes[(int)ByteIndex.Bark5] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50700, infoDark);
            m_bytes[(int)ByteIndex.Bark6] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50700, infoDark);
            m_bytes[(int)ByteIndex.Bark7] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50700, infoDark);
            m_bytes[(int)ByteIndex.Bark8] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50700, infoDark);
            m_bytes[(int)ByteIndex.Bark9] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL52200, infoDark);
            m_bytes[(int)ByteIndex.Bark10] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL52200, infoDark);
            m_bytes[(int)ByteIndex.Bark11] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL52200, infoDark);
            m_bytes[(int)ByteIndex.Bark12] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL52200, infoDark);
            m_bytes[(int)ByteIndex.Bark13] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53100, infoDark);
            m_bytes[(int)ByteIndex.Bark14] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53100, infoDark);
            m_bytes[(int)ByteIndex.Bark15] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53100, infoDark);
            m_bytes[(int)ByteIndex.Bark16] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53100, infoDark);
            m_bytes[(int)ByteIndex.Bark17] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50028, infoDark);
            m_bytes[(int)ByteIndex.Bark18] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50028, infoDark);
            m_bytes[(int)ByteIndex.Bark19] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50028, infoDark);
            m_bytes[(int)ByteIndex.Bark20] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50028, infoDark);
            m_bytes[(int)ByteIndex.Bark21] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50121, infoDark);
            m_bytes[(int)ByteIndex.Bark22] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50121, infoDark);
            m_bytes[(int)ByteIndex.Bark23] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50121, infoDark);
            m_bytes[(int)ByteIndex.Bark24] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50121, infoDark);
            m_bytes[(int)ByteIndex.Bark25] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53130, infoDark);
            m_bytes[(int)ByteIndex.Bark26] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53130, infoDark);
            m_bytes[(int)ByteIndex.Bark27] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53130, infoDark);
            m_bytes[(int)ByteIndex.Bark28] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53130, infoDark);
            m_bytes[(int)ByteIndex.Bark29] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53121, infoDark);
            m_bytes[(int)ByteIndex.Bark30] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53121, infoDark);
            m_bytes[(int)ByteIndex.Bark31] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53121, infoDark);
            m_bytes[(int)ByteIndex.Bark32] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53121, infoDark);
            m_bytes[(int)ByteIndex.BarkTr1] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL51611, infoDark);
            m_bytes[(int)ByteIndex.BarkTr2] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL50124, infoDark);
            m_bytes[(int)ByteIndex.BarkTr3] = MapScriptByte(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL53025, infoDark);
            m_bytes[(int)ByteIndex.PayLSD] = MapScriptByte(MM5Map.F2LostSoulsDungeonLevel4, MM45Bytes.PayGoldF2LSDL41402, infoDark);
            m_bytes[(int)ByteIndex.HelpedDerek] = MapScriptByte(MM4Map.F3Surface, MM45Bytes.HelpedDerekF3CS0405, infoXeen);

            m_points[(int)PointIndex.Gettlewaith] = GetMonsterLocation(MM5Map.A4Castleview, 0, infoDark);
            m_points[(int)PointIndex.Jasper] = GetMonsterLocation(MM5Map.A4Castleview, 37, infoDark);
            m_points[(int)PointIndex.Xenoc] = GetMonsterLocation(MM5Map.E3Sandcaster, 14, infoDark);
            m_points[(int)PointIndex.Morgana] = GetMonsterLocation(MM5Map.E3Sandcaster, 13, infoDark);
            m_points[(int)PointIndex.ChestP] = GetMonsterLocation(MM5Map.A4Castleview, 20, infoDark);
            m_points[(int)PointIndex.ChestI] = GetMonsterLocation(MM5Map.A4Castleview, 21, infoDark);
            m_points[(int)PointIndex.ChestT] = GetMonsterLocation(MM5Map.A4Castleview, 22, infoDark);
            m_points[(int)PointIndex.ChestC] = GetMonsterLocation(MM5Map.A4Castleview, 23, infoDark);
            m_points[(int)PointIndex.ChestH] = GetMonsterLocation(MM5Map.A4Castleview, 24, infoDark);
            m_points[(int)PointIndex.ChestF] = GetMonsterLocation(MM5Map.A4Castleview, 25, infoDark);
            m_points[(int)PointIndex.ChestO] = GetMonsterLocation(MM5Map.A4Castleview, 26, infoDark);
            m_points[(int)PointIndex.ChestR] = GetMonsterLocation(MM5Map.A4Castleview, 27, infoDark);
            m_points[(int)PointIndex.ChestK] = GetMonsterLocation(MM5Map.A4Castleview, 29, infoDark);
            m_points[(int)PointIndex.Chest1] = GetMonsterLocation(MM5Map.A4Castleview, 10, infoDark);
            m_points[(int)PointIndex.Chest2] = GetMonsterLocation(MM5Map.A4Castleview, 11, infoDark);
            m_points[(int)PointIndex.Chest3] = GetMonsterLocation(MM5Map.A4Castleview, 12, infoDark);
            m_points[(int)PointIndex.Chest4] = GetMonsterLocation(MM5Map.A4Castleview, 13, infoDark);
            m_points[(int)PointIndex.Chest5] = GetMonsterLocation(MM5Map.A4Castleview, 14, infoDark);
            m_points[(int)PointIndex.Chest6] = GetMonsterLocation(MM5Map.A4Castleview, 15, infoDark);
            m_points[(int)PointIndex.Chest7] = GetMonsterLocation(MM5Map.A4Castleview, 16, infoDark);
            m_points[(int)PointIndex.Chest8] = GetMonsterLocation(MM5Map.A4Castleview, 17, infoDark);
            m_points[(int)PointIndex.Chest9] = GetMonsterLocation(MM5Map.A4Castleview, 19, infoDark);
            m_points[(int)PointIndex.BarkDial1] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 1, infoDark);
            m_points[(int)PointIndex.BarkDial2] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 5, infoDark);
            m_points[(int)PointIndex.BarkDial3] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 10, infoDark);
            m_points[(int)PointIndex.BarkDial4] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 14, infoDark);
            m_points[(int)PointIndex.BarkDial5] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 19, infoDark);
            m_points[(int)PointIndex.BarkDial6] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 23, infoDark);
            m_points[(int)PointIndex.BarkLever] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel4, 0, infoDark);
            m_points[(int)PointIndex.Monk1] = GetMonsterLocation(MM5Map.A4Castleview, 36, infoDark);
            m_points[(int)PointIndex.Monk2] = GetMonsterLocation(MM5Map.A4Castleview, 35, infoDark);
            m_points[(int)PointIndex.Monk3] = GetMonsterLocation(MM5Map.A4Castleview, 34, infoDark);
            m_points[(int)PointIndex.Rooka] = GetMonsterLocation(MM5Map.A4CastleviewSewer, 84, infoDark);
            m_points[(int)PointIndex.Draco] = GetMonsterLocation(MM4Map.D4Nightshadow, 27, infoXeen);
            m_points[(int)PointIndex.Barkman] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel5, 0, infoDark);
            m_points[(int)PointIndex.LeverLSD1] = GetMonsterLocation(MM5Map.F2LostSoulsDungeonLevel4, 2, infoDark);
            m_points[(int)PointIndex.LeverLSD2] = GetMonsterLocation(MM5Map.F2LostSoulsDungeonLevel4, 6, infoDark);
            m_points[(int)PointIndex.Time1] = GetMonsterLocation(MM5Map.A1CastleAlamarLevel2, 9, infoDark);
            m_points[(int)PointIndex.Time2] = GetMonsterLocation(MM5Map.A1CastleAlamarLevel2, 21, infoDark);
            m_points[(int)PointIndex.Time3] = GetMonsterLocation(MM5Map.A1CastleAlamarLevel2, 33, infoDark);
            m_points[(int)PointIndex.Time4] = GetMonsterLocation(MM5Map.A1CastleAlamarLevel2, 45, infoDark);
            m_points[(int)PointIndex.Time5] = GetMonsterLocation(MM5Map.A1CastleAlamarLevel2, 0, infoDark);

            for (PointIndex pi = PointIndex.Torch1; pi <= PointIndex.Torch6; pi++)
                m_points[(int)pi] = GetMonsterLocation(MM5Map.D2TheGreatPyramidLevel1, (uint)(pi - PointIndex.Torch1), infoDark);
            for (PointIndex pi = PointIndex.BSkull1; pi <= PointIndex.BSkull8; pi++)
                m_points[(int)pi] = GetMonsterLocation(MM5Map.C4TempleOfBarkLevel5, (uint)(pi - PointIndex.BSkull1) + 1, infoDark);
            for (PointIndex pi = PointIndex.PyNum3; pi <= PointIndex.PyNum10; pi++)
                m_points[(int)pi] = GetMonsterLocation(MM5Map.D2TheGreatPyramidLevel1, (uint)(pi - PointIndex.PyNum3) + 6, infoDark);
            for (PointIndex pi = PointIndex.CWScepter; pi <= PointIndex.CWElements; pi++)
                m_points[(int)pi] = GetMonsterLocation(MM5Map.E3DungeonOfDeathLevel1, (uint) (pi - PointIndex.CWScepter) + 1, infoDark);
            for (PointIndex pi = PointIndex.TTGong1; pi <= PointIndex.TTGong4; pi++)
                m_points[(int)pi] = GetMonsterLocation(MM5Map.E3DungeonOfDeathLevel3, (uint)(pi - PointIndex.TTGong1), infoDark);

            for (int i = 0; i < WaterDragons.Length; i++)
                WaterDragons[i] = GetMonsterLocation(MM4Map.D3Surface, MM45Bytes.WaterDragons[i], infoXeen);
            for (int i = 0; i < SpiritBones.Length; i++)
                SpiritBones[i] = GetMonsterLocation(MM4Map.A3Winterkill, MM45Bytes.SpiritBones[i], infoXeen);
            for (int i = 0; i < PolterFools.Length; i++)
                PolterFools[i] = GetMonsterLocation(MM4Map.A3Winterkill, MM45Bytes.PolterFool[i], infoXeen);
            for (int i = 0; i < GhostRiders.Length; i++)
                GhostRiders[i] = GetMonsterLocation(MM4Map.A3Winterkill, MM45Bytes.GhostRider[i], infoXeen);
            m_points[(int)PointIndex.LordXeen] = GetMonsterLocation(MM4Map.D3XeensCastleLevel4, 8, infoXeen);
            for (int i = 0; i < YakMegaCredits.Length; i++)
                YakMegaCredits[i] = MapScriptByte(MM45Bytes.YakMegaCredits[i].Map, MM45Bytes.YakMegaCredits[i].Offset, infoXeen);
            for (int i = 0; i < TombMegaCredits.Length; i++)
                TombMegaCredits[i] = MapScriptByte(MM45Bytes.TombMegaCredits[i].Map, MM45Bytes.TombMegaCredits[i].Offset, infoXeen);
            for (int i = 0; i < GolemMegaCredits.Length; i++)
                GolemMegaCredits[i] = MapScriptByte(MM45Bytes.GolemMegaCredits[i].Map, MM45Bytes.GolemMegaCredits[i].Offset, infoXeen);
            Valid = true;
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(256);

            ms.Write(m_bytes, 0, m_bytes.Length);
            Global.WritePoints(ms, m_points);
            Global.WritePoints(ms, WaterDragons);
            Global.WritePoints(ms, PolterFools);
            Global.WritePoints(ms, SpiritBones);
            Global.WritePoints(ms, GhostRiders);
            ms.Write(YakMegaCredits, 0, YakMegaCredits.Length);
            ms.Write(TombMegaCredits, 0, TombMegaCredits.Length);
            ms.Write(GolemMegaCredits, 0, GolemMegaCredits.Length);

            return ms.ToArray();
        }
    }
}