using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// These aliases make the SetQuests() function look slightly less awkward
using Quest = WhereAreWe.MM45Bits.Quest;
using Cloud = WhereAreWe.MM45Bits.Clouds;
using Dark = WhereAreWe.MM45Bits.Dark;
using CharB = WhereAreWe.MM45Bits.CharBit;
using World = WhereAreWe.MM45Bits.World;
using FileB = WhereAreWe.MM45FileQuestInfo.ByteIndex;
using FileP = WhereAreWe.MM45FileQuestInfo.PointIndex;

namespace WhereAreWe
{
    namespace MM45QuestStates
    {
    }

    public class MM45QuestData : QuestData
    {
        public MM45FileQuestInfo FileQuestInfo;

        public MM45QuestData(MM45PartyInfo party, MM45GameInfo info, MM45FileQuestInfo fqi)
        {
            Party = party;
            Info = info;
            FileQuestInfo = fqi;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
        }
    }

    public static class MM45Bits
    {
        public enum Clouds
        {
            None = -1,
            BangGongAfterSpiritBonesW1301 = 0,
            ReportNoMonstersW0813 = 1,
            BangGongsTwiceW0613 = 2,
            BangGongAfterPolterFoolsW1301 = 3,
            BangGoneOnceW0609 = 4,
            BangGongTwiceW0609 = 5,
            BangGongOnceW0613 = 6,
            BangGongTwiceW0613 = 7,
            BangGongAfterGhostRidersW1301 = 8,
            FindJoesInvoice = 9,
            TurnInVertigoPlagueV1405 = 10,
            KillDwarfKingDMO3026 = 11,
            TurnInDwarfKingV1405 = 12,
            TurnSwitchATY0213 = 13,
            TurnSwitchATY0112 = 14,
            TurnSwitchATY0708 = 15,
            TurnSwitchATY1411 = 16,
            TurnSwitchATY2123 = 17,
            TurnSwitchATY2429 = 18,
            TurnSwitchATY1930 = 19,
            TurnSwitchATY1630 = 20,
            TurnSwitch2ATY0213 = 21,
            TurnSwitch2ATY0708 = 22,
            TurnSwitch2ATY2123 = 23,
            TurnSwitch2ATY1630 = 24,
            PullPlugCI1413 = 25,
            TakeTreasureCB0601 = 26,
            FreeCeliaD41515 = 32,
            WaterReflectorF41500 = 33,
            EarthReflectorA40000 = 34,
            AirReflectorF11515 = 35,
            FireReflectorF11515 = 36,
            EnterPagodaA31512 = 37,
            DestroyCyclopsA31000 = 38,
            DestroyTrollB40207 = 39,
            DestroyOgreC20500 = 40,
            DestroyBarbarianC20108 = 41,
            BuildWallCB10207 = 43,
            BuildKeepCB10207 = 44,
            BuyLandC41112 = 45,
            DestroyOrcE20902 = 47,
            DestroyOrcE31413 = 48,
            DestroyShrineE40913 = 49,
            TurnInOrothinF30906 = 50,
            SummonMonstersCI10114 = 51,
            SummonMonstersCI21513 = 52,
            SummonMonstersCI31314 = 53,
            CleanOutDungeonCB0207 = 54,
            CollectTaxesC41112 = 55,
            KillWaterDragonsD30013 = 56,
            SearchTreeV2829 = 57,
            SearchTreeV2921 = 58,
            EnterThroneRoomCB0806 = 59,
            TurnInPagodaA31512 = 63,
            GetHolyBookB41413 = 64,
            TurnInDestroyOgreC20901 = 65,
            TurnInFaeryWandC31415 = 66,
            TurnInLakeMonstersC31213 = 67,
            DestroySpriteD30301 = 68,
            GetFaeryWandD40814 = 69,
            SummonPolterFoolsW0813 = 72,
            SummonGhostRidersW0813 = 73,
            KillLordXeenCX40509 = 75,
            StartBarokQuestR2520 = 100,
            TurnInBarokQuestR2520 = 101,
            ResurrectMonstersN1501 = 103,
            DefeatDracoN0114 = 104,
            SundialTo1N0610 = 105,
            SundialTo2N0610 = 106,
            SundialTo3N0610 = 107,
            SundialTo4N0610 = 108,
            SundialTo5N0610 = 109,
            SundialTo6N0610 = 110,
            SundialTo7N0610 = 111,
            SundialTo8N0610 = 112,
            SundialTo9N0610 = 113,
            SundialTo10N0610 = 114,
            SundialTo11N0610 = 115,
            SundialTo12N0610 = 116,
            SundialTo1N0811 = 117,
            SundialTo2N0811 = 118,
            SundialTo3N0811 = 119,
            SundialTo4N0811 = 120,
            SundialTo5N0811 = 121,
            SundialTo6N0811 = 122,
            SundialTo7N0811 = 123,
            SundialTo8N0811 = 124,
            SundialTo9N0811 = 125,
            SundialTo10N0811 = 126,
            SundialTo11N0811 = 127,
            SundialTo12N0811 = 128,
            SundialTo1N1010 = 129,
            SundialTo2N1010 = 130,
            SundialTo3N1010 = 131,
            SundialTo4N1010 = 132,
            SundialTo5N1010 = 133,
            SundialTo6N1010 = 134,
            SundialTo7N1010 = 135,
            SundialTo8N1010 = 136,
            SundialTo9N1010 = 137,
            SundialTo10N1010 = 138,
            SundialTo11N1010 = 139,
            SundialTo12N1010 = 140,
            PedestalBlueA0702 = 141,
            PedestalBlueA0902 = 142,
            PedestalBlueA0904 = 143,
            PedestalBlueA0704 = 144,
            DestroyTransformerA0814 = 145,
            DestroyHarpyWC2228 = 146,
            DestroyHarpyWC2720 = 147,
            DestroyHarpyWC0807 = 148,
            DestroyHarpyWC0419 = 149,
            DestroyHarpyWC0427 = 150,
            BangDrumHMC2528 = 151,
            BangDrumHMC1610 = 152,
            BangDrumHMC0421 = 153,
            BangDrumHMC1724 = 154,
            EnterCloudsHMC2724 = 155,
            TakeGemsHMC0503 = 156,
            TakeGemsHMC0314 = 157,
            TakeGemsHMC0821 = 158,
            TakeGemsHMC1328 = 159,
            TakeGemsHMC2223 = 160,
            TakeGemsHMC2714 = 161,
            TakeGemsHMC2005 = 162,
            FlipSwitchGD1925 = 163,
            FlipSwitchGD0113 = 164,
            FlipSwitchGD0106 = 165,
            FlipSwitchGD0101 = 166,
            FlipSwitchGD0901 = 167,
            FlipSwitchGD0906 = 168,
            FlipSwitchGD0913 = 169,
            PushButton1GD0126 = 170,
            PushButton1GD0123 = 171,
            PushButton1GD0129 = 172,
            PushButton2GD0123 = 174,
            PushButton2GD0126 = 175,
            PushButton2GD0129 = 176,
            FlipSwitchGD2120 = 189,
            FlipSwitchGD2320 = 190,
            FlipSwitchGD2520 = 191,
            FlipSwitchGD2114 = 192,
            FlipSwitchGD2314 = 193,
            FlipSwitchGD2514 = 194,
            FlipSwitch2GD1925 = 195,
            SmashSkullVC0015 = 196,
            SmashSkullVC0909 = 197,
            FlipSwitchVC1313 = 198,
            FlipSwitchVC1113 = 199,
            FlipSwitchVC1311 = 200,
            FlipSwitchVC1111 = 201,
            DestroyMachineXC41111 = 202,
            DestroyGeneratorXC40301 = 203,
            DestroyGeneratorXC41201 = 204,
            DestroyGeneratorXC10114 = 205,
            DestroyGeneratorXC11414 = 206,
            PushButtonDT40608 = 207,
            PushButtonDT40808 = 208,
            XeenClouds1XC1502 = 209,
            XeenClouds2XC1502 = 210,
            XeenClouds3XC1502 = 211,
            XeenClouds4XC1502 = 212,
            XeenClouds5XC1502 = 213,
            XeenClouds6XC1502 = 214,
            XeenClouds7XC1502 = 215,
            FerryFromRivercityR1623 = 220,
            FerryToRivercityD31012 = 221,
            EnterCloudsWC2803 = 222,
            TakeGemsWC1816 = 223,
            TakeGemsWC1828 = 224,
            TakeGemsWC0711 = 225,
            TakeGemsWC1814 = 226,
            TakeGemsWC2207 = 227,
            TakeGemsWC3002 = 228,
            TakeGemsWC0630 = 229,
            TakeGemsWC1119 = 230,
            TreesInitializedV1500 = 231,
            SearchTreeV1302 = 232, // (F-3, Vertigo: 13,2) 
            SearchTreeV1306 = 233, // (F-3, Vertigo: 13,6) 
            SearchTreeV1408 = 234, // (F-3, Vertigo: 14,8) 
            SearchTreeV1409 = 235, // (F-3, Vertigo: 14,9) 
            SearchTreeV1411 = 236, // (F-3, Vertigo: 14,11)
            SearchTreeV1412 = 237, // (F-3, Vertigo: 14,12)
            SearchTreeV1413 = 238, // (F-3, Vertigo: 14,13)
            SearchTreeV1315 = 239, // (F-3, Vertigo: 13,15)
            SearchTreeV1319 = 240, // (F-3, Vertigo: 13,19)
            SearchTreeV1719 = 241, // (F-3, Vertigo: 17,19)
            SearchTreeV1715 = 242, // (F-3, Vertigo: 17,15)
            SearchTreeV1613 = 243, // (F-3, Vertigo: 16,13)
            SearchTreeV1612 = 244, // (F-3, Vertigo: 16,12)
            SearchTreeV1608 = 245, // (F-3, Vertigo: 16,8) 
            SearchTreeV1705 = 246, // (F-3, Vertigo: 17,5) 
            SearchTreeV1703 = 247, // (F-3, Vertigo: 17,3) 
            SearchTreeV2028 = 248, // (F-3, Vertigo: 20,28)
            SearchTreeV2221 = 249, // (F-3, Vertigo: 22,21)
            SearchTreeV2225 = 250, // (F-3, Vertigo: 22,25)
            SearchTreeV2327 = 251, // (F-3, Vertigo: 23,27)
            SearchTreeV2522 = 252, // (F-3, Vertigo: 25,22)
            SearchTreeV2625 = 253, // (F-3, Vertigo: 26,25)
            SearchTreeV2628 = 254, // (F-3, Vertigo: 26,28)
            SearchTreeV2723 = 255, // (F-3, Vertigo: 27,23)
            Air,
        }

        public enum Dark
        {
            None = -1,
            Mine1GM0810 = 3,
            Mine1GM0813 = 4,
            Mine1GM1607 = 5,
            Mine1GM2402 = 6,
            Mine1GM1131 = 7,
            Mine1GM1425 = 8,
            Mine1GM1522 = 9,
            Mine1GM1129 = 10,
            Mine2GM1129 = 11,
            Mine1GM2701 = 12,
            Mine2GM2701 = 13,
            Mine1GM2604 = 14,
            Mine2GM2604 = 15,
            Mine1GM3103 = 16,
            Mine2GM3103 = 17,
            Mine1GM3129 = 18,
            Mine2GM3129 = 19,
            Mine2GM1027 = 20,
            Mine3GM1027 = 21,
            Mine1GM1027 = 22,
            Mine2GM3100 = 23,
            Mine3GM3100 = 24,
            Mine1GM3100 = 25,
            Mine1GM0401 = 26,
            Mine1GM0003 = 27,
            Mine1GM1411 = 28,
            Mine1GM1515 = 29,
            Mine1GM1901 = 30,
            Mine1GM2002 = 31,
            Mine1GM2004 = 32,
            Mine1GM2509 = 33,
            Mine1GM1025 = 34,
            Mine1GM2027 = 35,
            Mine1GM2129 = 36,
            Mine1GM0403 = 37,
            Mine2GM0403 = 38,
            Mine1GM0406 = 39,
            Mine2GM0406 = 40,
            Mine1GM3112 = 41,
            Mine2GM3112 = 42,
            Mine1GM1022 = 43,
            Mine2GM1022 = 44,
            Mine1GM2917 = 45,
            Mine2GM2917 = 46,
            Mine2GM0006 = 47,
            Mine3GM0006 = 48,
            Mine1GM0006 = 49,
            Mine2GM3108 = 50,
            Mine3GM3108 = 51,
            Mine1GM3108 = 52,
            Mine2GM0617 = 53,
            Mine3GM0617 = 54,
            Mine1GM0617 = 55,
            Mine2GM3117 = 56,
            Mine3GM3117 = 57,
            Mine1GM3117 = 58,
            Mine1GM0014 = 59,
            Mine1GM1015 = 60,
            Mine1GM1604 = 61,
            Mine1GM1705 = 62,
            Mine1GM0418 = 63,
            Mine1GM1018 = 64,
            Mine1GM1531 = 65,
            Mine1GM1731 = 66,
            Mine1GM3123 = 67,
            Mine1GM3125 = 68,
            Mine1GM0412 = 69,
            Mine1GM0210 = 70,
            Mine2GM0210 = 71,
            Mine1GM0218 = 72,
            Mine2GM0218 = 73,
            Mine1GM0731 = 74,
            Mine2GM0731 = 75,
            Mine1GM3127 = 76,
            Mine2GM3127 = 77,
            Mine1GM3121 = 78,
            Mine2GM3121 = 79,
            Mine2GM0010 = 80,
            Mine3GM0010 = 81,
            Mine1GM0010 = 82,
            Mine2GM0018 = 83,
            Mine3GM0018 = 84,
            Mine1GM0018 = 85,
            Mine2GM0431 = 86,
            Mine3GM0431 = 87,
            Mine1GM0431 = 88,
            Mine2GM2531 = 89,
            Mine3GM2531 = 90,
            Mine1GM2531 = 91,
            Mine2GM3131 = 92,
            Mine3GM3131 = 93,
            Mine1GM3131 = 94,
            Mine1GM1316 = 95,
            Mine1GM1418 = 96,
            Mine1GM0021 = 97,
            Mine1GM0031 = 98,
            Mine1GM1527 = 99,
            Mine1GM1105 = 100,
            Mine2GM1105 = 101,
            Mine1GM1201 = 102,
            Mine2GM1201 = 103,
            Mine1GM0023 = 104,
            Mine2GM0023 = 105,
            Mine1GM0028 = 106,
            Mine2GM0028 = 107,
            Mine2GM0801 = 108,
            Mine3GM0801 = 109,
            Mine1GM0801 = 110,
            Mine2GM0026 = 111,
            Mine3GM0026 = 112,
            Mine1GM0026 = 113,
            DeliverOrbET40408 = 114,
            ActivateMirrorsCK30101 = 115,
            ChosenOnesGP40609 = 117,
            BoatSB31108 = 118,
            AnswerMerchantSD41203 = 119,
            AnswerMerchantSA20304 = 120,
            AnswerMerchantSC10212 = 121,
            FireSleeperEPF0312 = 123,
            AirSleeperEPA1212 = 124,
            EarthSleeperEPE1203 = 125,
            WaterSleeperEPW0303 = 126,
            WaterTestEPW0411 = 127,
            FireTestEPF1013 = 128,
            AirTestEPA1002 = 129,
            EarthTestEPE0304 = 130,
            OpenChestCS3026 = 131,
            AnswerRiddleNT10410 = 132,
            AnswerRiddleNT11010 = 133,
            AnswerRiddleNT20511 = 134,
            AnswerRiddleNT20911 = 135,
            AnswerRiddleNT30410 = 136,
            AnswerRiddleNT31010 = 137,
            AnswerRiddleNT31006 = 138,
            ChooseGoldB21202 = 140,
            DestroyBarbarianB31310 = 141,
            AnswerPaladinC21110 = 142,
            DestroyOgreD30307 = 143,
            FlipHourglassLSD11110 = 144,
            FlipHourglassLSD11409 = 145,
            FlipHourglassLSD10510 = 146,
            FlipHourglassLSD10209 = 147,
            PullLeverLSD30101 = 148,
            PullLeverLSD31402 = 149,
            PullLeverLSD31314 = 150,
            PullLeverLSD30110 = 151,
            DrinkWaterLSD24040 = 152,
            DeliverDisks2ET40408 = 153,
            DeliverDisks3ET40408 = 154,
            DeliverDisks4ET40408 = 155,
            RescueKalindra1CBD0101 = 156,
            TurnInSongbird1CK21015 = 157,
            LearnCombinationCBD0101 = 158,
            TurnInSandroN1008 = 159,
            BeginSaveQueenCBD0101 = 160,
            RescueKalindra2CBD0101 = 161,
            TurnInSongbird2CK21015 = 162,
            TurnInWesternTowerA30810 = 163,
            TurnInEnchantBridleB11205 = 164,
            YogEnemyB30612 = 165,
            YogFriendB30612 = 166,
            RescueSheewanaTB30612 = 167,
            DeliverDisks1ET40408 = 168,
            FreeCorakEP10408 = 169,
            DragonParaohGP40609 = 170,
            EnterBarkC40208 = 171,
            DestroyOgreD30908 = 172
        }

        public enum World
        {
            None = -1,
            ShrineElectricityD31504 = 1,
            ShrineColdA41214 = 2,
            ShrinePoisonF31406 = 3,
            ShrineMagicC31500 = 4,
            ShrineEnergyA10706 = 5,
            ElementalShrineE30914 = 6,
            WellMightD20308 = 7,
            WellIntellectB31504 = 8,
            WellPersonalityC30000 = 9,
            WellEnduranceC10204 = 10,
            WellSpeedE20304 = 11,
            WellAccuracyB30003 = 12,
            WishingWellF30107 = 13,
            OlympianFountainC31510 = 14,
            SagesFountainD30809 = 15,
            WatersPowerF30707 = 16,
            WatersGreatPowerA10412 = 17,
            WatersMagicE30806 = 18,
            WatersGreatMagicA40303 = 19,
            FountainProtectionF31212 = 20,
            FountainGreatProtectionA30314 = 21,
            FountainAbilityF30001 = 22,
            VertigoWellV1417 = 23,
            NightshadowWellN0707 = 24,
            RivercityWellR1418 = 25,
            AspWellA0803 = 26,
            WinterkillWellW0611 = 27,
            TurnedSeasonsE30314 = 28,
            PasswordGoluxSB0715 = 29,
            PasswordThereWolfCB20915 = 30,
            PasswordRosebudWT40410 = 31,
            PortalVertigoR2418 = 32,
            PortalNightshadowR2418 = 33,
            PortalRivercityR2418 = 34,
            PortalAspR2418 = 35,
            PortalWinterkillR2418 = 36,
            PortalWarzoneR2214 = 37,
            MineCodesDM10811 = 38,
            DwarvenCodeADM11119 = 39,
            DwarvenCodeLDM20603 = 40,
            DwarvenCodePDM30600 = 41,
            DwarvenCodeHDM40604 = 42,
            DwarvenCodeADM50308 = 43,
            TravelCodeAlphaR2218 = 44,
            TravelCodeThetaDMA3011 = 45,
            TravelCodeKappaDMT0615 = 46,
            TravelCodeOmegaDMK1910 = 47,
            ShrineFireE21303 = 48,
            StatueWC2709 = 49,
            StatueWC2725 = 50,
            StatueWC0325 = 51,
            StatueWC0309 = 52,
            StatueHMC2414 = 53,
            StatueHMC177 = 54,
            StatueHMC0705 = 55,
            StatueHMC0617 = 56,
            StatueCX1009 = 57,
            StatueCX0701 = 58,
            StatueCX2709 = 59,
            StatueCX2601 = 60,
            NewcastleWellNC10914 = 61,
            PasswordLaboratoryNC11214 = 62,
            NoteLANC10101 = 63,
            NoteBONC10614 = 64,
            NoteRANC11401 = 65,
            NoteTONC21008 = 66,
            NoteRYNC21106 = 67,
            GoluxSD1511 = 68,
            GoluxSD1509 = 69,
            GoluxSD1507 = 70,
            GoluxSD0005 = 71,
            GoluxSD0007 = 72,
            TrollHolesTH2027 = 73,
            TrollHolesTH3013 = 74,
            CliffNotesNT40909 = 75,
            CliffNotesNT41009 = 76,
            CliffNotesNT41007 = 77,
            CliffNotesNT41006 = 78,
            CliffNotesNT40406 = 79,
            CliffNotesNT40407 = 80,
            CliffNotesNT40410 = 81,
            FountainSuperResilienceA10213 = 82,
            FountaintheElementsB11413 = 83,
            PrayA30814 = 84,
            WellResilienceA30303 = 85,
            WellSpellsA30210 = 86,
            WellMightA40911 = 87,
            WellProtectionA40310 = 88,
            WishingWellB40202 = 89,
            ShrineGreatPowerC20108 = 90,
            FountainMightD10613 = 91,
            PrayD41204 = 92,
            WellElementalResistanceD40204 = 93,
            FountainMagicResistanceF20805 = 94,
            FountainGreatProtectionF41403 = 95,
            FountainYouthF40607 = 96,
            PlaqueO0101 = 97,
            PlaqueO0114 = 98,
            PlaqueO1414 = 99,
            PlaqueO1401 = 100,
            CombinationCK21115 = 101,
            CombinationCBD0101 = 102,
            TreasureE40013 = 103,
            LedgerST20406 = 104,
            FountainGreatMagicE10210 = 105,
            FountainGreatResilienceF11308 = 106,
            AlamarPathCAD0512 = 107,
            AlamarTimeCAD0808 = 108,
            DragonRunesDC0031 = 109,
            DragonRunesDC0000 = 110,
            DragonRunesDC3000 = 111,
            DragonRunesDC3031 = 112,
            PlaqueDT10406 = 113,
            PlaqueDT10410 = 114,
            PlaqueDT11010 = 115,
            PlaqueDT11006 = 116,
            PlaqueDT20406 = 117,
            PlaqueDT20410 = 118,
            PlaqueDT21010 = 119,
            PlaqueDT21006 = 120,
            PlaqueDT30406 = 121,
            PlaqueDT30410 = 122,
            PlaqueDT31010 = 123,
            PlaqueDT31006 = 124,
            HeadDT40406 = 125,
            HeadDT40410 = 126,
            HeadDT41010 = 127,
            HeadDT41006 = 128
        }

        public enum Quest
        {
            None = -1,
            SlayMadDwarfKing = 1,
            GatherPhirnaRoot = 2,
            FreeCelia = 3,
            RetrieveAlacorn = 4,
            FindOrothinsWhistle = 5,
            FindLigonosSkull = 6,
            GetBaroksPendant = 7,
            GetRoxannesTiara = 8,
            ReturnScarab = 9,
            ReturnCrystals = 10,
            StealElixir = 11,
            FindFaeryWand = 12,
            FindHolyBook = 13,
            DestroyTrollLair = 14,
            DestroyOgreLair = 15,
            ReclaimPagoda = 16,
            SlayLakeMonsters = 17,
            SaveWinterkill = 18,
            DestroyCyclopsLair = 19,
            FindEverhotRock = 20,
            RetrieveScrollOfInsight = 21,
            FreeCrodo = 22,
            ClimbDarzogsTower = 23,
            FindMirror = 24,
            TakeLastFlower = 25,
            TakeLastLeaf = 26,
            TakeLastSnowflake = 27,
            TakeLastRaindrop = 28,
            RidVertigoOfPests = 29,
            FindWesternTowerKey = 30,
            CollectRubies = 31,
            CollectEmeralds = 32,
            CollectSapphires = 33,
            CollectDiamonds = 34,
            FindStatuettes = 35,
            EnchantBridle = 36,
            DestroyOgres = 37,
            FindVesparsHandle = 38,
            BringMelons = 39,
            RescueSprite = 40,
            FindChalice = 41,
            FindEctorsRing = 42,
            FindCalebsGlass = 43,
            FindJewelOfAges = 44,
            StopGettlewaith = 45,
            ReleaseJethrosBrother = 46,
            FindNadiasNecklace = 47,
            EndXenocsReign = 48,
            FindSandrosHeart = 49,
            SaveKalindra = 50,
            TalkToAmbrose = 51,
            FindSongbird = 52,
            FindEnergyDisks = 53,
            FindDragonEgg = 54,
            LastStandardQuest = 54,
        }

        public enum CharBit
        {
            None = -1,
            DepressionThrone = 1,
            ConfusionThrone = 2,
            HeartbreakingThrone = 3,
            InsanityThrone = 4,
            EuphoriaThrone = 5,
            BookOfTheDeadVol1 = 6,
            BookOfTheDeadVol2 = 7,
            BookOfTheDeadVol3 = 8,
            BookOfTheDeadVol4 = 9,
            BookOfTheDeadVol5 = 10,
            BookOfTheDeadVol6 = 11,
            BookOfTheDeadVol7 = 12,
            BookOfTheDeadVol8 = 13,
            BookOfTheDeadVol9 = 14,
            ManualOfMasterThievery = 15,
            FountainOfLife = 16,
            BookOfFantasticKnowledge = 17,
            BookOfGreatPower = 18,
            PrinceBook = 19,
            BookOfFire = 20,
            BookOfElectricity = 21,
            TomeOfGreatExperience1 = 22,
            TomeOfGreatExperience2 = 23,
            BookOfDirectives = 24
        }

        public static bool IsSet(byte[] bytes, Quest bit) { return bit == Quest.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }
        public static bool IsSet(byte[] bytes, Dark bit) { return bit == Dark.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }
        public static bool IsSet(byte[] bytes, World bit) { return bit == World.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }
        public static bool IsSet(byte[] bytes, Clouds bit) { return bit == Cloud.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }
        public static bool IsSet(byte[] bytes, int iCharIndex, CharBit bit) { return bit == CharBit.None ? false : (Global.GetBit(bytes, iCharIndex * 32 + (int)bit) == 1); }

        public static BitDesc CloudsDescription(object val) { return Description((Clouds)val); }
        public static BitDesc DarkDescription(object val) { return Description((Dark)val); }
        public static BitDesc WorldDescription(object val) { return Description((World)val); }
        public static BitDesc QuestDescription(object val) { return Description((Quest)val); }
        public static BitDesc CharDescription(object val) { return Description((int)val / 32, (CharBit)((int)val % 32)); }

        public static string QuestItemDescription(int index)
        {
            if (index == -1)
                return "Quest Items";

            MM45QuestItemIndex qi = (MM45QuestItemIndex)(index+1);
            if (qi > MM45QuestItemIndex.None && qi < MM45QuestItemIndex.Invalid)
                return MM45Item.GetItemName(qi);
            return String.Empty;
        }

        public static string AwardDescription(int index)
        {
            if (index == -1)
                return "Awards";

            MM45AwardIndex ai = (MM45AwardIndex)index;
            if (ai >= MM45AwardIndex.VertigoGuildMember && ai < MM45AwardIndex.Last)
                return MM45Character.AwardString(ai);
            return String.Empty;
        }

        public static BitDesc Description(CharBit bit)
        {
            switch (bit)
            {
                case CharBit.DepressionThrone: return new BitDesc("Use the Depression throne", MM5Map.D1NorthernTowerLevel4, 6, 8,
                    "Use the Euphoria throne", MM5Map.D1NorthernTowerLevel4, 7, 6);
                case CharBit.ConfusionThrone: return new BitDesc("Use the Confusion throne", MM5Map.D1NorthernTowerLevel4, 6, 7,
                    "Use the Euphoria throne", MM5Map.D1NorthernTowerLevel4, 7, 6);
                case CharBit.HeartbreakingThrone: return new BitDesc("Use the Heartbreaking throne", MM5Map.D1NorthernTowerLevel4, 8, 8,
                    "Use the Euphoria throne", MM5Map.D1NorthernTowerLevel4, 7, 6);
                case CharBit.InsanityThrone: return new BitDesc("Use the Insanity throne", MM5Map.D1NorthernTowerLevel4, 8, 7,
                    "Use the Euphoria throne", MM5Map.D1NorthernTowerLevel4, 7, 6);
                case CharBit.EuphoriaThrone: return new BitDesc("Use the Euphoria Throne", MM5Map.D1NorthernTowerLevel4, 7, 6).AddChecked();
                case CharBit.BookOfTheDeadVol1: return new BitDesc("Use the Book of the Dead Vol. 1", MM5Map.B2Necropolis, 14, 10).AddChecked();
                case CharBit.BookOfTheDeadVol2: return new BitDesc("Use the Book of the Dead Vol. 2", MM5Map.B2Necropolis, 14, 6).AddChecked();
                case CharBit.BookOfTheDeadVol3: return new BitDesc("Use the Book of the Dead Vol. 3", MM5Map.B2Necropolis, 12, 6).AddChecked();
                case CharBit.BookOfTheDeadVol4: return new BitDesc("Use the Book of the Dead Vol. 4", MM5Map.B2Necropolis, 12, 10).AddChecked();
                case CharBit.BookOfTheDeadVol5: return new BitDesc("Use the Book of the Dead Vol. 5", MM5Map.B2Necropolis, 4, 5).AddChecked();
                case CharBit.BookOfTheDeadVol6: return new BitDesc("Use the Book of the Dead Vol. 6", MM5Map.B2Necropolis, 6, 5).AddChecked();
                case CharBit.BookOfTheDeadVol7: return new BitDesc("Use the Book of the Dead Vol. 7", MM5Map.B2Necropolis, 5, 14).AddChecked();
                case CharBit.BookOfTheDeadVol8: return new BitDesc("Use the Book of the Dead Vol. 8", MM5Map.B2Necropolis, 3, 14).AddChecked();
                case CharBit.BookOfTheDeadVol9: return new BitDesc("Use the Book of the Dead Vol. 9", MM5Map.B2Necropolis, 1, 14).AddChecked();
                case CharBit.ManualOfMasterThievery: return new BitDesc("Use the Manual of Master Thievery", MM5Map.D4SouthernTowerLevel4, 5, 11).AddChecked();
                case CharBit.FountainOfLife: return new BitDesc("Use the Fountain of Life", MM5Map.F3EasternTowerLevel4, 10, 10).AddChecked();
                case CharBit.BookOfFantasticKnowledge: return new BitDesc("Use the Book of Fantastic Knowledge", MM5Map.F3EasternTowerLevel4, 11, 8).AddChecked();
                case CharBit.BookOfGreatPower: return new BitDesc("Use the Book of Great Power", MM5Map.F3EasternTowerLevel4, 7, 4).AddChecked();
                case CharBit.PrinceBook: return new BitDesc("Use the Prince Book", MM5Map.D4SouthernTowerLevel4, 9, 11).AddChecked();
                case CharBit.BookOfFire: return new BitDesc("Use the Book of Fire", MM5Map.A4EllingersTowerLevel2, 9, 5).AddChecked();
                case CharBit.BookOfElectricity: return new BitDesc("Use the Book of Electricity", MM5Map.A4EllingersTowerLevel2, 9, 11).AddChecked();
                case CharBit.TomeOfGreatExperience1: return new BitDesc("Use the Tome of Great Experience", MM5Map.D1DragonTowerLevel4, 4, 6).AddChecked();
                case CharBit.TomeOfGreatExperience2: return new BitDesc("Use the Tome of Great Experience", MM5Map.D1DragonTowerLevel4, 10, 6).AddChecked();
                case CharBit.BookOfDirectives: return new BitDesc("Use the Book of Directives", MM5Map.D2TheGreatPyramidLevel4, 5, 5).AddChecked();
                default: return BitDesc.Empty;
            }
        }

        public static BitDesc Description(int iCharacter, CharBit bit)
        {
            if (iCharacter > 19)
                return BitDesc.Empty;

            BitDesc bd = Description(bit);
            if (bd.IsEmpty)
                return bd;

            bd.Set[0].When = String.Format("Character #{0} {1}", iCharacter, bd.Set[0].When.Replace("Use ", "uses "));
            return bd;
        }

        public static MapXY[] arrResetTrees { get { return MapXY.Array(MM4Map.F3Vertigo, 14, 10, 24, 5, 15, 0); } }
        private const string enter = "Enter the location";

        public static BitDesc Description(Clouds bit)
        {
            BitDesc bd;
            const string sundial = "Turn the sundial";

            switch (bit)
            {
                case Clouds.BangGongAfterSpiritBonesW1301:
                    bd = new BitDesc("Kill Spirit Bones and bang the gong", MM4Map.A3Winterkill, 13, 1);
                    bd.AddChecked("Talk to Randon", MM45.Spots.Randon);
                    bd.AddChecked("Bang the gong", MM4Map.A3Winterkill, 13, 1);
                    return bd;
                case Clouds.ReportNoMonstersW0813: return new BitDesc("Complete Randon's quest", MM45.Spots.Randon, "Talk to Randon");
                case Clouds.BangGongsTwiceW0613:
                    bd = new BitDesc("Bang the gongs twice", MapXY.Array(MM4Map.A3Winterkill, 6, 13, 6, 9));
                    bd.AddChecked("Drink from the well", MM4Map.A3Winterkill, 6, 11);
                    return bd;
                case Clouds.BangGongAfterPolterFoolsW1301:
                    bd = new BitDesc("Kill Polter Fools and bang the gong", MM4Map.A3Winterkill, 13, 1);
                    bd.AddChecked("Talk to Randon", MM45.Spots.Randon);
                    bd.AddChecked("Bang the gong", MM4Map.A3Winterkill, 13, 1);
                    return bd;
                case Clouds.BangGoneOnceW0609: return new BitDesc("Bang the gong once", MM4Map.A3Winterkill, 6, 9, "Bang the gong");
                case Clouds.BangGongTwiceW0609: return new BitDesc("Bang the gong twice", MM4Map.A3Winterkill, 6, 9).AddChecked("Bang the gong", MM4Map.A3Winterkill, 6, 13);
                case Clouds.BangGongOnceW0613: return new BitDesc("Bang the gong once", MM4Map.A3Winterkill, 6, 13, "Bang the gong");
                case Clouds.BangGongTwiceW0613: return new BitDesc("Bang the gong twice", MM4Map.A3Winterkill, 6, 13).AddChecked("Bang the gong", MM4Map.A3Winterkill, 6, 9);
                case Clouds.BangGongAfterGhostRidersW1301:
                    bd = new BitDesc("Kill Ghost Riders and bang the gong", MM4Map.A3Winterkill, 13, 1);
                    bd.AddChecked("Talk to Randon", MM45.Spots.Randon);
                    bd.AddChecked("Bang the gong", MM4Map.A3Winterkill, 13, 1);
                    return bd;
                case Clouds.FindJoesInvoice:
                    bd = new BitDesc("Find Joe's Invoice", MM4Map.F3Vertigo, 9, 22);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.F3Vertigo, 11, 18, 8, 18));
                    bd.AddChecked("Drink from the well", MM4Map.F3Vertigo, 14, 17);
                    bd.AddChecked("Talk to Gunther", MM45.Spots.Gunther);
                    bd.AddChecked("Leave Vertigo", MM4Map.F3Vertigo, 15, 0);
                    return bd;
                case Clouds.TurnInVertigoPlagueV1405: return new BitDesc("Complete Gunther's first quest", MM45.Spots.Gunther, "Talk to Gunther");
                case Clouds.KillDwarfKingDMO3026: return new BitDesc("Defeat the Clan King", MM45.Spots.ClanKing, "Talk to Gunther", MM45.Spots.Gunther);
                case Clouds.TurnInDwarfKingV1405: return new BitDesc("Complete Gunther's second quest", MM45.Spots.Gunther, "Talk to Gunther");
                case Clouds.TurnSwitchATY0213: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 2, 13).AddCZ();
                case Clouds.TurnSwitchATY0112: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 1, 12).AddChecked(1, 12, 2, 13).AddZero(1, 12);
                case Clouds.TurnSwitchATY0708: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 7, 8).AddCZ();
                case Clouds.TurnSwitchATY1411: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 14, 11).AddChecked(7, 8, 14, 11).AddZero(14, 11);
                case Clouds.TurnSwitchATY2123: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 21, 23).AddCZ();
                case Clouds.TurnSwitchATY2429:
                    bd = new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 24, 29).AddZero().AddChecked(16, 30, 24, 29);
                    bd.AddSet(enter, new MapXY(MM4Map.E4AncientTempleOfYak, 6, 18));
                    return bd;
                case Clouds.TurnSwitchATY1930: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 19, 30).AddZero().AddChecked(16, 30, 19, 30);
                case Clouds.TurnSwitchATY1630: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 16, 30).AddCZ();
                case Clouds.TurnSwitch2ATY0213: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 2, 13).AddCZ();
                case Clouds.TurnSwitch2ATY0708: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 7, 8).AddCZ();
                case Clouds.TurnSwitch2ATY2123: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 21, 23).AddCZ();
                case Clouds.TurnSwitch2ATY1630: return new BitDesc("Turn the switch", MM4Map.E4AncientTempleOfYak, 16, 30).AddCZ();
                case Clouds.PullPlugCI1413: 
                    bd =  new BitDesc("Pull the plug", MM4Map.B4CaveOfIllusionLevel4, 14, 13);
                    bd.AddChecked(enter, MapXY.Combine(
                        MapXY.Array(MM4Map.B4CaveOfIllusionLevel1, 1, 14, 11, 8, 12, 6, 15, 0, 4, 12, 5, 12, 6, 12, 9, 12),
                        MapXY.Array(MM4Map.B4CaveOfIllusionLevel2, 10, 8, 11, 8, 12, 8, 13, 1, 15, 3, 2, 1, 5, 11, 5, 5, 7, 8),
                        MapXY.Array(MM4Map.B4CaveOfIllusionLevel3, 10, 10, 11, 10, 12, 9, 13, 11, 13, 14, 14, 1, 14, 13, 5, 3, 8, 2, 9, 10),
                        MapXY.Array(MM4Map.B4CaveOfIllusionLevel4, 11, 12, 11, 9, 13, 6, 15, 0, 3, 12, 3, 9, 5, 11, 7, 10, 7, 12, 8, 0, 9, 11)));
                    return bd;
                case Clouds.TakeTreasureCB0601: 
                    bd = new BitDesc("Take the treasure", MapXY.Array(MM4Map.D2CastleBurlockLevel3, 6, 1, 10, 1));
                    bd.AddChecked(enter, new MapXY[] { new MapXY(MM4Map.D2CastleBurlockLevel3, 8, 1), 
                        new MapXY(MM4Map.D2CastleBurlockLevel2, 10, 1) });
                    return bd;
                case Clouds.FreeCeliaD41515: return new BitDesc("Free Celia", MM45.Spots.Celia, "Talk to Derek", MM45.Spots.Derek);
                case Clouds.WaterReflectorF41500: return new BitDesc("Activate the Water Reflector", MM4Map.F4Surface, 15, 0);
                case Clouds.EarthReflectorA40000: return new BitDesc("Activate the Earth Reflector", MM4Map.A4Surface, 0, 0);
                case Clouds.AirReflectorF11515: return new BitDesc("Activate the Air Reflector", MM4Map.A1Surface, 0, 15);
                case Clouds.FireReflectorF11515: return new BitDesc("Activate the Fire Reflector", MM4Map.F1Surface, 15, 15);
                case Clouds.EnterPagodaA31512: return new BitDesc("Enter the pagoda", MM45.Spots.Pagoda, "Talk to Kai Wu", MM45.Spots.KaiWu);
                case Clouds.DestroyCyclopsA31000: return new BitDesc("Destroy the Cyclops Outpost", MM45.Spots.CyclopsOutpost, "Talk to Glom", MM45.Spots.Glom);
                case Clouds.DestroyTrollB40207: return new BitDesc("Destroy the Troll Lair", MM45.Spots.TrollLair, "Talk to Thickbark", MM45.Spots.Thickbark);
                case Clouds.DestroyOgreC20500: return new BitDesc("Destroy the Ogre Lair", MM45.Spots.OgreLair, "Talk to Nystor", MM45.Spots.Nystor);
                case Clouds.DestroyBarbarianC20108: return new BitDesc("Destroy the Barbarian Encampment", MM4Map.C2Surface, 1, 8);
                case Clouds.BuildWallCB10207: return new BitDesc("Build a wall around your castle", MM4Map.D2CastleBurlockLevel1, 2, 7, "Enter your castle", MM4Map.C4Surface, 11, 12);
                case Clouds.BuildKeepCB10207: return new BitDesc("Build a keep in your castle", MM4Map.D2CastleBurlockLevel1, 2, 7, "Enter your castle", MM4Map.C4Surface, 11, 12);
                case Clouds.BuyLandC41112: return new BitDesc("Buy your castle's land", MM4Map.C4Surface, 11, 12, "Enter your castle", MM4Map.C4Surface, 11, 12);
                case Clouds.DestroyOrcE20902: return new BitDesc("Destroy the Orc Outpost", MM4Map.E2Surface, 9, 2);
                case Clouds.DestroyOrcE31413: return new BitDesc("Destroy the Orc Observation Post", MM4Map.E3Surface, 14, 13);
                case Clouds.DestroyShrineE40913: return new BitDesc("Destroy the Undead Shrine", MM4Map.E4Surface, 9, 14);
                case Clouds.TurnInOrothinF30906:
                    bd = new BitDesc("Complete Orothin's quest", MM45.Spots.Orothin);
                    bd.AddChecked("Touch the statue", MapXY.Array(MM4Map.F3Surface, 12, 2, 12, 8));
                    return bd;
                case Clouds.SummonMonstersCI10114:
                    bd = new BitDesc(enter, MM4Map.B4CaveOfIllusionLevel1, 1, 14);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.B4CaveOfIllusionLevel1, 1, 14, 15, 0));
                    return bd;
                case Clouds.SummonMonstersCI21513:
                    bd = new BitDesc(enter, MM4Map.B4CaveOfIllusionLevel2, 15, 3);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.B4CaveOfIllusionLevel2, 13, 1, 15, 3));
                    return bd;
                case Clouds.SummonMonstersCI31314:
                    bd = new BitDesc(enter, MapXY.Array(MM4Map.B4CaveOfIllusionLevel3, 14, 1, 13, 14));
                    bd.AddChecked(enter, MapXY.Array(MM4Map.B4CaveOfIllusionLevel3, 13, 14, 14, 1));
                    return bd;
                case Clouds.CleanOutDungeonCB0207: 
                    bd = new BitDesc("Clean out your dungeon", MM4Map.D2CastleBurlockLevel1, 2, 7);
                    bd.AddChecked("Enter your castle", MM4Map.C4Surface, 11, 12);
                    bd.AddChecked(enter, MM4Map.C4Surface, 11, 12);
                    return bd;
                case Clouds.CollectTaxesC41112: return new BitDesc("Enter your castle", MM4Map.C4Surface, 11, 12);
                case Clouds.KillWaterDragonsD30013: 
                    bd = new BitDesc("Defeat the Water Dragons", MapXY.Array(MM4Map.D3Surface, 0, 13, 5, 11, 4, 15));
                    bd.AddChecked("Talk to Medin", MM45.Spots.Medin);
                    return bd;
                case Clouds.SearchTreeV2829: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 28, 29).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2921: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 29, 21).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.EnterThroneRoomCB0806:
                    bd = new BitDesc("Enter the Throne Room", MapXY.Combine(MapXY.Array(
                        MM4Map.D2CastleBurlockLevel1, 6, 1, 8, 6, 10, 1), MapXY.Array(MM4Map.D2CastleBurlockLevel2, 6, 1, 8, 1, 10, 1, 8, 3)));
                    bd.AddCZ(enter, new MapXY(MM4Map.D2CastleBurlockLevel2, 8, 3));
                    return bd;
                case Clouds.TurnInPagodaA31512: return new BitDesc("Complete Kai Wu's quest", MM45.Spots.KaiWu);
                case Clouds.GetHolyBookB41413: return new BitDesc("Take the Holy Book of Elvenkind", MM4Map.B4Surface, 14, 13);
                case Clouds.TurnInDestroyOgreC20901: return new BitDesc("Complete Nystor's quest", MM45.Spots.Nystor);
                case Clouds.TurnInFaeryWandC31415: return new BitDesc("Complete Danulf's quest", MM45.Spots.Danulf);
                case Clouds.TurnInLakeMonstersC31213: return new BitDesc("Complete Medin's quest", MM45.Spots.Medin);
                case Clouds.DestroySpriteD30301: return new BitDesc("Destroy the Sprite Egg Cave", MM4Map.D3Surface, 3, 1);
                case Clouds.GetFaeryWandD40814: return new BitDesc("Take the Faery Wand", MM4Map.D4Surface, 8, 14);
                case Clouds.SummonPolterFoolsW0813: return new BitDesc("Summon Polter Fools", MM45.Spots.Randon);
                case Clouds.SummonGhostRidersW0813: return new BitDesc("Summon Ghost Riders", MM45.Spots.Randon);
                case Clouds.KillLordXeenCX40509: return new BitDesc("Defeat Lord Xeen", MM4Map.D3XeensCastleLevel4, 5, 9, enter, MM4Map.D3CloudsOfXeen, 16, 29);
                case Clouds.StartBarokQuestR2520: return new BitDesc("Start Barok's quest", MM45.Spots.Barok, "Talk to Barok");
                case Clouds.TurnInBarokQuestR2520: 
                    bd = new BitDesc("Complete Barok's quest", MM45.Spots.Barok);
                    bd.AddChecked("Talk to Barok", MM45.Spots.Barok);
                    bd.AddChecked("Drink from the well", MM4Map.C3Rivercity, 14, 18);
                    return bd;
                case Clouds.ResurrectMonstersN1501: bd = BitDesc.Empty;
                    bd.AddChecked(enter, MM4Map.D4Nightshadow, 15, 1);
                    return bd;
                case Clouds.DefeatDracoN0114: return new BitDesc("Defeat Count Draco", MM4Map.D4Nightshadow, 1, 14, "Drink from the well", MM4Map.D4Nightshadow, 7, 7);
                case Clouds.SundialTo1N0610: return new BitDesc("Set the sundial to 1", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo2N0610: return new BitDesc("Set the sundial to 2", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo3N0610: return new BitDesc("Set the sundial to 3", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo4N0610: return new BitDesc("Set the sundial to 4", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo5N0610: return new BitDesc("Set the sundial to 5", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo6N0610: return new BitDesc("Set the sundial to 6", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo7N0610: return new BitDesc("Set the sundial to 7", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo8N0610: return new BitDesc("Set the sundial to 8", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo9N0610:
                    bd = new BitDesc("Set the sundial to 9", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                    bd.AddChecked("Open the coffin", MM4Map.D4Nightshadow, 1, 14);
                    return bd;
                case Clouds.SundialTo10N0610: return new BitDesc("Set the sundial to 10", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo11N0610: return new BitDesc("Set the sundial to 11", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo12N0610: return new BitDesc("Set the sundial to 12", MM4Map.D4Nightshadow, 6, 10, sundial, sundial);
                case Clouds.SundialTo1N0811: return new BitDesc("Set the sundial to 1", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo2N0811: return new BitDesc("Set the sundial to 2", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo3N0811: return new BitDesc("Set the sundial to 3", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo4N0811: return new BitDesc("Set the sundial to 4", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo5N0811: return new BitDesc("Set the sundial to 5", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo6N0811: return new BitDesc("Set the sundial to 6", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo7N0811: return new BitDesc("Set the sundial to 7", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo8N0811: return new BitDesc("Set the sundial to 8", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo9N0811:
                    bd = new BitDesc("Set the sundial to 9", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                    bd.AddChecked("Open the coffin", MM4Map.D4Nightshadow, 1, 14);
                    return bd;
                case Clouds.SundialTo10N0811: return new BitDesc("Set the sundial to 10", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo11N0811: return new BitDesc("Set the sundial to 11", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo12N0811: return new BitDesc("Set the sundial to 12", MM4Map.D4Nightshadow, 8, 11, sundial, sundial);
                case Clouds.SundialTo1N1010: return new BitDesc("Set the sundial to 1", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo2N1010: return new BitDesc("Set the sundial to 2", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo3N1010: return new BitDesc("Set the sundial to 3", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo4N1010: return new BitDesc("Set the sundial to 4", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo5N1010: return new BitDesc("Set the sundial to 5", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo6N1010: return new BitDesc("Set the sundial to 6", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo7N1010: return new BitDesc("Set the sundial to 7", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo8N1010: return new BitDesc("Set the sundial to 8", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo9N1010:
                    bd = new BitDesc("Set the sundial to 9", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                    bd.AddChecked("Open the coffin", MM4Map.D4Nightshadow, 1, 14);
                    return bd;
                case Clouds.SundialTo10N1010: return new BitDesc("Set the sundial to 10", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo11N1010: return new BitDesc("Set the sundial to 11", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.SundialTo12N1010: return new BitDesc("Set the sundial to 12", MM4Map.D4Nightshadow, 10, 10, sundial, sundial);
                case Clouds.PedestalBlueA0702:
                    bd = new BitDesc("Change the pedestal to blue", MM4Map.C2Asp, 07, 02, "Touch the pedestal", "Touch the pedestal");
                    bd.AddChecked(enter, MM4Map.C2Asp, 8, 14);
                    return bd;
                case Clouds.PedestalBlueA0902:
                    bd = new BitDesc("Change the pedestal to blue", MM4Map.C2Asp, 09, 02, "Touch the pedestal", "Touch the pedestal");
                    bd.AddChecked(enter, MM4Map.C2Asp, 8, 14);
                    return bd;
                case Clouds.PedestalBlueA0904:
                    bd = new BitDesc("Change the pedestal to blue", MM4Map.C2Asp, 09, 04, "Touch the pedestal", "Touch the pedestal");
                    bd.AddChecked(enter, MM4Map.C2Asp, 8, 14);
                    return bd;
                case Clouds.PedestalBlueA0704:
                    bd = new BitDesc("Change the pedestal to blue", MM4Map.C2Asp, 07, 04, "Touch the pedestal", "Touch the pedestal");
                    bd.AddChecked(enter, MM4Map.C2Asp, 8, 14);
                    return bd;
                case Clouds.DestroyTransformerA0814:
                    bd = new BitDesc("Destroy the Snake Transformer", MM4Map.C2Asp, 08, 14);
                    bd.AddChecked("Leave Asp", MM4Map.C2Asp, 8, 0);
                    bd.AddChecked("Talk to Adam", MM4Map.C2Asp, 4, 1);
                    bd.AddChecked("Talk to Eve", MM4Map.C2Asp, 4, 3);
                    bd.AddChecked("Drink from the well", MM4Map.C2Asp, 8, 3);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.C2Asp, 8, 11, 8, 14));
                    return bd;
                case Clouds.DestroyHarpyWC2228: return new BitDesc("Destroy the Harpy Nest", MM4Map.F4WitchClouds, 22, 28, enter, MM4Map.F4WitchClouds, 28, 3);
                case Clouds.DestroyHarpyWC2720: return new BitDesc("Destroy the Harpy Nest", MM4Map.F4WitchClouds, 27, 20, enter, MM4Map.F4WitchClouds, 28, 3);
                case Clouds.DestroyHarpyWC0807: return new BitDesc("Destroy the Harpy Nest", MM4Map.F4WitchClouds, 08, 07, enter, MM4Map.F4WitchClouds, 28, 3);
                case Clouds.DestroyHarpyWC0419: return new BitDesc("Destroy the Harpy Nest", MM4Map.F4WitchClouds, 04, 19, enter, MM4Map.F4WitchClouds, 28, 3);
                case Clouds.DestroyHarpyWC0427: return new BitDesc("Destroy the Harpy Nest", MM4Map.F4WitchClouds, 04, 27, enter, MM4Map.F4WitchClouds, 28, 3);
                case Clouds.BangDrumHMC2528: 
                    bd = new BitDesc("Bang the drum", MM4Map.C4HighMagicClouds, 25, 28);
                    bd.AddCZ("Search the barrel", new MapXY(MM4Map.C4Surface, 4, 15));
                    return bd;
                case Clouds.BangDrumHMC1610:
                    bd = new BitDesc("Bang the drum", MM4Map.C4HighMagicClouds, 16, 10);
                    bd.AddCZ("Search the barrel", new MapXY(MM4Map.C4Surface, 5, 15));
                    return bd;
                case Clouds.BangDrumHMC0421:
                    bd = new BitDesc("Bang the drum", MM4Map.C4HighMagicClouds, 04, 21);
                    bd.AddCZ("Search the barrel", new MapXY(MM4Map.C4Surface, 8, 15));
                    return bd;
                case Clouds.BangDrumHMC1724:
                    bd = new BitDesc("Bang the drum", MM4Map.C4HighMagicClouds, 17, 24);
                    bd.AddCZ("Search the barrel", new MapXY(MM4Map.C4Surface, 9, 15));
                    return bd;
                case Clouds.EnterCloudsHMC2724: return new BitDesc(enter, MM4Map.C4HighMagicClouds, 27, 24).AddChecked();
                case Clouds.TakeGemsHMC0503: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 05, 03).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC0314: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 03, 14).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC0821: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 08, 21).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC1328: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 13, 28).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC2223: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 22, 23).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC2714: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 27, 14).AddChecked().AddZero(27, 24);
                case Clouds.TakeGemsHMC2005: return new BitDesc("Take the gems", MM4Map.C4HighMagicClouds, 20, 05).AddChecked().AddZero(27, 24);
                case Clouds.FlipSwitchGD1925: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 19, 25, enter, MM4Map.B4GolemDungeon, 22, 25).AddZero(19, 25);
                case Clouds.FlipSwitchGD0113: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 01, 13).AddChecked(1, 13, 19, 25).AddZero(1, 13);
                case Clouds.FlipSwitchGD0106: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 01, 16).AddChecked(1, 16, 19, 25).AddZero(1, 6);
                case Clouds.FlipSwitchGD0101: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 01, 01).AddChecked(1, 1, 19, 25).AddZero(1, 1);
                case Clouds.FlipSwitchGD0901: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 09, 01).AddChecked(9, 1, 19, 25).AddZero(9, 1);
                case Clouds.FlipSwitchGD0906: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 09, 06).AddChecked(9, 6, 19, 25).AddZero(9, 6);
                case Clouds.FlipSwitchGD0913: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 09, 13).AddChecked(9, 13, 19, 25).AddZero(9, 13);
                case Clouds.PushButton1GD0126: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 26).AddChecked(19, 25).AddZero(1, 26);
                case Clouds.PushButton1GD0123: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 23).AddChecked(19, 25).AddZero(1, 23);
                case Clouds.PushButton1GD0129: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 29).AddChecked(19, 25).AddZero(1, 29);
                case Clouds.PushButton2GD0123: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 23).AddCZ();
                case Clouds.PushButton2GD0126: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 26).AddCZ();
                case Clouds.PushButton2GD0129: return new BitDesc("Push the button", MM4Map.B4GolemDungeon, 01, 29).AddCZ();
                case Clouds.FlipSwitchGD2120: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 21, 20).AddCZ();
                case Clouds.FlipSwitchGD2320: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 23, 20).AddCZ();
                case Clouds.FlipSwitchGD2520: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 25, 20).AddCZ();
                case Clouds.FlipSwitchGD2114: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 21, 14).AddCZ();
                case Clouds.FlipSwitchGD2314: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 23, 14).AddCZ();
                case Clouds.FlipSwitchGD2514: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 25, 14).AddCZ();
                case Clouds.FlipSwitch2GD1925: return new BitDesc("Flip the switch", MM4Map.B4GolemDungeon, 19, 25).AddCZ();
                case Clouds.SmashSkullVC0015:
                    bd = new BitDesc("Smash the skull", MM4Map.E1VolcanoCaveLevel1, 00, 15);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.E1VolcanoCaveLevel2, 12, 14, 3, 0));
                    bd.AddChecked(enter, MM4Map.E1VolcanoCaveLevel1, 12, 0);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.E1VolcanoCaveLevel3, 14, 14, 9, 10));
                    return bd;
                case Clouds.SmashSkullVC0909:
                    bd = new BitDesc("Smash the skull", MM4Map.E1VolcanoCaveLevel1, 09, 09);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.E1VolcanoCaveLevel3, 14, 14, 9, 10));
                    bd.AddChecked(enter, MM4Map.E1VolcanoCaveLevel1, 12, 0);
                    bd.AddChecked(enter, MapXY.Array(MM4Map.E1VolcanoCaveLevel2, 12, 14, 3, 0));
                    return bd;
                case Clouds.FlipSwitchVC1313: return new BitDesc("Flip the switch", MM4Map.E1VolcanoCaveLevel2, 13, 13).AddCZ();
                case Clouds.FlipSwitchVC1113: return new BitDesc("Flip the switch", MM4Map.E1VolcanoCaveLevel2, 11, 13).AddCZ();
                case Clouds.FlipSwitchVC1311: return new BitDesc("Flip the switch", MM4Map.E1VolcanoCaveLevel2, 13, 11).AddCZ();
                case Clouds.FlipSwitchVC1111: return new BitDesc("Flip the switch", MM4Map.E1VolcanoCaveLevel2, 11, 11).AddCZ();
                case Clouds.DestroyMachineXC41111: 
                    bd = new BitDesc("Destroy the machine", MM4Map.D3XeensCastleLevel4, 11, 11);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.D3XeensCastleLevel1, 6, 0),
                    MapXY.Array(MM4Map.D3XeensCastleLevel2, 1, 2, 12, 2, 14, 2, 3, 2),
                    MapXY.Array(MM4Map.D3XeensCastleLevel3, 1, 2, 12, 2, 14, 2, 3, 2),
                    MapXY.Array(MM4Map.D3XeensCastleLevel4, 1, 2, 14, 2, 9, 8)));
                    return bd;
                case Clouds.DestroyGeneratorXC40301:
                    bd = new BitDesc("Destroy the generator", MM4Map.D3XeensCastleLevel4, 03, 01);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.D3XeensCastleLevel1, 10, 9, 11, 10, 6, 8, 8, 9),
                    MapXY.Array(MM4Map.D3XeensCastleLevel2, 11, 10, 4, 10, 7, 8),
                    MapXY.Array(MM4Map.D3XeensCastleLevel3, 11, 8, 4, 8)));
                    return bd;
                case Clouds.DestroyGeneratorXC41201:
                    bd = new BitDesc("Destroy the generator", MM4Map.D3XeensCastleLevel4, 12, 01);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.D3XeensCastleLevel1, 10, 8, 6, 9, 8, 10, 9, 8),
                    MapXY.Array(MM4Map.D3XeensCastleLevel2, 10, 10, 4, 8, 9, 8),
                    MapXY.Array(MM4Map.D3XeensCastleLevel3, 6, 10)));
                    return bd;
                case Clouds.DestroyGeneratorXC10114:
                    bd = new BitDesc("Destroy the generator", MM4Map.D3XeensCastleLevel1, 01, 14);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.D3XeensCastleLevel1, 10, 10, 10, 7, 5, 7, 7, 8),
                    MapXY.Array(MM4Map.D3XeensCastleLevel2, 6, 10, 9, 10),
                    MapXY.Array(MM4Map.D3XeensCastleLevel3, 8, 10, 8, 7)));
                    return bd;
                case Clouds.DestroyGeneratorXC11414:
                    bd = new BitDesc("Destroy the generator", MM4Map.D3XeensCastleLevel1, 14, 14);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.D3XeensCastleLevel1, 4, 10, 5, 9, 8, 7),
                    MapXY.Array(MM4Map.D3XeensCastleLevel2, 8, 10),
                    MapXY.Array(MM4Map.D3XeensCastleLevel3, 10, 10, 11, 10, 4, 10, 7, 8)));
                    return bd;
                case Clouds.PushButtonDT40608: return new BitDesc("Push the button", MM4Map.D3DarzogsTowerLevel4, 06, 08).AddCZ();
                case Clouds.PushButtonDT40808: return new BitDesc("Push the button", MM4Map.D3DarzogsTowerLevel4, 08, 08).AddCZ();
                case Clouds.XeenClouds1XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds2XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds3XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds4XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds5XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds6XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.XeenClouds7XC1502: return new BitDesc("", null, enter, new MapXY(MM4Map.D3CloudsOfXeen, 15, 02));
                case Clouds.FerryFromRivercityR1623:
                    bd = new BitDesc("Ride the ferry", MM4Map.C3Rivercity, 16, 23);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.C3Surface, 11, 8, 12, 7, 12, 8, 13, 7, 14, 7, 15, 7),
                        MapXY.Array(MM4Map.D3Surface, 0, 6, 0, 7, 1, 6, 10, 6, 10, 7, 11, 11, 11, 7, 11, 8, 11, 9, 12, 10, 12, 11, 
                            12, 9, 2, 6, 3, 6, 4, 5, 4, 6, 5, 5, 6, 4, 6, 5, 7, 4, 8, 4, 8, 5, 8, 6, 9, 6),
                        MapXY.Array(MM4Map.C3Rivercity, 16, 24, 16, 25, 16, 26, 16, 27, 17, 27, 17, 28, 17, 29, 17, 30, 17, 31)));
                    bd.AddZero(enter, MapXY.Combine(MapXY.Array(MM4Map.C3Rivercity, 16, 23, 17, 28),
                        MapXY.Array(MM4Map.D3Surface, 10, 12, 11, 11)));
                    return bd;
                case Clouds.FerryToRivercityD31012:
                    bd = new BitDesc("Ride the ferry", MM4Map.D3Surface, 10, 12);
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM4Map.C3Surface, 11, 8, 12, 7, 12, 8, 13, 7, 14, 7, 15, 7),
                        MapXY.Array(MM4Map.C3Rivercity, 16, 24, 16, 25, 16, 26, 17, 27, 17, 29, 17, 30, 16, 27, 17, 28, 17, 31),
                        MapXY.Array(MM4Map.D3Surface, 0, 6, 0, 7, 10, 6, 10, 7, 11, 7, 11, 9, 12, 11, 12, 9, 4, 5, 4, 6, 6, 4,
                            6, 5, 8, 4, 8, 6, 1, 6, 11, 8, 12, 10, 2, 6, 3, 6, 5, 5, 7, 4, 8, 5, 9, 6, 11, 11, 11, 12)));
                    bd.AddZero(enter, MapXY.Combine(MapXY.Array(MM4Map.C3Rivercity, 16, 23, 17, 28),
                        MapXY.Array(MM4Map.D3Surface, 10, 12)));
                    return bd;
                case Clouds.EnterCloudsWC2803: return new BitDesc(enter, MM4Map.F4WitchClouds, 28, 03).AddChecked();
                case Clouds.TakeGemsWC1816: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 18, 16).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC1828: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 18, 28).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC0711: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 07, 11).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC1814: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 18, 14).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC2207: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 22, 07).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC3002: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 30, 02).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC0630: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 06, 30).AddChecked().AddZero(28, 3);
                case Clouds.TakeGemsWC1119: return new BitDesc("Take the gems", MM4Map.F4WitchClouds, 11, 19).AddChecked().AddZero(28, 3);
                case Clouds.TreesInitializedV1500: return new BitDesc(enter, MapXY.Array(MM4Map.F3Vertigo, 14, 10, 24, 5, 15, 0)).AddChecked();
                case Clouds.SearchTreeV1302: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 13, 02).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1306: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 13, 06).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1408: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 14, 08).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1409: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 14, 09).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1411: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 14, 11).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1412: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 14, 12).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1413: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 14, 13).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1315: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 13, 15).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1319: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 13, 19).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1719: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 17, 19).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1715: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 17, 15).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1613: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 16, 13).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1612: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 16, 12).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1608: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 16, 08).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1705: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 17, 05).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV1703: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 17, 03).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2028: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 20, 28).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2221: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 22, 21).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2225: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 22, 25).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2327: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 23, 27).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2522: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 25, 22).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2625: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 26, 25).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2628: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 26, 28).AddChecked().AddZero(enter, arrResetTrees);
                case Clouds.SearchTreeV2723: return new BitDesc("Search the tree", MM4Map.F3Vertigo, 27, 23).AddChecked().AddZero(enter, arrResetTrees);
                default: return BitDesc.Empty;
            }
        }

        public static BitDesc MineBitDesc(int iTimes, int x, int y, bool bSelfZero = false)
        {
            string strNum = (iTimes == 1 ? "once" : (iTimes == 2 ? "twice" : "three times"));
            return new BitDesc("Mine " + strNum, MM5Map.B2GemstoneMines, x, y, "Mine the vein").AddZero("Pay the God of Minerals", new MapXY(MM5Map.B2GemstoneMines, 11, 12));
        }

        public static BitDesc Description(Dark bit)
        {
            BitDesc bd;
            const MM5Map ls1 = MM5Map.F2LostSoulsDungeonLevel1;
            const MM5Map ls2 = MM5Map.F2LostSoulsDungeonLevel2;
            const MM5Map ls3 = MM5Map.F2LostSoulsDungeonLevel3;
            const MM5Map ls4 = MM5Map.F2LostSoulsDungeonLevel4;
            const string stairs = "Climb the stairs";

            switch(bit)
            {
                case Dark.Mine1GM0810: return MineBitDesc(1, 8, 10);
                case Dark.Mine1GM0813: return MineBitDesc(1, 8, 13);
                case Dark.Mine1GM1607: return MineBitDesc(1, 16, 7);
                case Dark.Mine1GM2402: return MineBitDesc(1, 24, 2);
                case Dark.Mine1GM1131: return MineBitDesc(1, 11, 31);
                case Dark.Mine1GM1425: return MineBitDesc(1, 14, 25);
                case Dark.Mine1GM1522: return MineBitDesc(1, 15, 22);
                case Dark.Mine1GM1129: return MineBitDesc(1, 11, 29, true);
                case Dark.Mine2GM1129: return MineBitDesc(2, 11, 29);
                case Dark.Mine1GM2701: return MineBitDesc(1, 27, 1, true);
                case Dark.Mine2GM2701: return MineBitDesc(2, 27, 1);
                case Dark.Mine1GM2604: return MineBitDesc(1, 26, 4, true);
                case Dark.Mine2GM2604: return MineBitDesc(2, 26, 4);
                case Dark.Mine1GM3103: return MineBitDesc(1, 31, 3, true);
                case Dark.Mine2GM3103: return MineBitDesc(2, 31, 3);
                case Dark.Mine1GM3129: return MineBitDesc(1, 31, 29, true);
                case Dark.Mine2GM3129: return MineBitDesc(2, 31, 29);
                case Dark.Mine2GM1027: return MineBitDesc(2, 10, 27, true);
                case Dark.Mine3GM1027: return MineBitDesc(3, 10, 27);
                case Dark.Mine1GM1027: return MineBitDesc(1, 10, 27);
                case Dark.Mine2GM3100: return MineBitDesc(2, 31, 0, true);
                case Dark.Mine3GM3100: return MineBitDesc(3, 31, 0);
                case Dark.Mine1GM3100: return MineBitDesc(1, 31, 0);
                case Dark.Mine1GM0401: return MineBitDesc(1, 4, 1);
                case Dark.Mine1GM0003: return MineBitDesc(1, 0, 3);
                case Dark.Mine1GM1411: return MineBitDesc(1, 14, 11);
                case Dark.Mine1GM1515: return MineBitDesc(1, 15, 15);
                case Dark.Mine1GM1901: return MineBitDesc(1, 19, 1);
                case Dark.Mine1GM2002: return MineBitDesc(1, 20, 2);
                case Dark.Mine1GM2004: return MineBitDesc(1, 20, 4);
                case Dark.Mine1GM2509: return MineBitDesc(1, 25, 9);
                case Dark.Mine1GM1025: return MineBitDesc(1, 10, 25);
                case Dark.Mine1GM2027: return MineBitDesc(1, 20, 27);
                case Dark.Mine1GM2129: return MineBitDesc(1, 21, 29);
                case Dark.Mine1GM0403: return MineBitDesc(1, 4, 3, true);
                case Dark.Mine2GM0403: return MineBitDesc(2, 4, 3);
                case Dark.Mine1GM0406: return MineBitDesc(1, 4, 6, true);
                case Dark.Mine2GM0406: return MineBitDesc(2, 4, 6);
                case Dark.Mine1GM3112: return MineBitDesc(1, 31, 12, true);
                case Dark.Mine2GM3112: return MineBitDesc(2, 31, 12);
                case Dark.Mine1GM1022: return MineBitDesc(1, 10, 22, true);
                case Dark.Mine2GM1022: return MineBitDesc(2, 10, 22);
                case Dark.Mine1GM2917: return MineBitDesc(1, 29, 17, true);
                case Dark.Mine2GM2917: return MineBitDesc(2, 29, 17);
                case Dark.Mine2GM0006: return MineBitDesc(2, 0, 6, true);
                case Dark.Mine3GM0006: return MineBitDesc(3, 0, 6);
                case Dark.Mine1GM0006: return MineBitDesc(1, 0, 6);
                case Dark.Mine2GM3108: return MineBitDesc(2, 31, 8, true);
                case Dark.Mine3GM3108: return MineBitDesc(3, 31, 8);
                case Dark.Mine1GM3108: return MineBitDesc(1, 31, 8);
                case Dark.Mine2GM0617: return MineBitDesc(2, 6, 17, true);
                case Dark.Mine3GM0617: return MineBitDesc(3, 6, 17);
                case Dark.Mine1GM0617: return MineBitDesc(1, 6, 17);
                case Dark.Mine2GM3117: return MineBitDesc(2, 31, 17, true);
                case Dark.Mine3GM3117: return MineBitDesc(3, 31, 17);
                case Dark.Mine1GM3117: return MineBitDesc(1, 31, 17);
                case Dark.Mine1GM0014: return MineBitDesc(1, 0, 14);
                case Dark.Mine1GM1015: return MineBitDesc(1, 10, 15);
                case Dark.Mine1GM1604: return MineBitDesc(1, 16, 4);
                case Dark.Mine1GM1705: return MineBitDesc(1, 17, 5);
                case Dark.Mine1GM0418: return MineBitDesc(1, 4, 18);
                case Dark.Mine1GM1018: return MineBitDesc(1, 10, 18);
                case Dark.Mine1GM1531: return MineBitDesc(1, 15, 31);
                case Dark.Mine1GM1731: return MineBitDesc(1, 17, 31);
                case Dark.Mine1GM3123: return MineBitDesc(1, 31, 23);
                case Dark.Mine1GM3125: return MineBitDesc(1, 31, 25);
                case Dark.Mine1GM0412: return MineBitDesc(1, 4, 12);
                case Dark.Mine1GM0210: return MineBitDesc(1, 2, 10, true);
                case Dark.Mine2GM0210: return MineBitDesc(2, 2, 10);
                case Dark.Mine1GM0218: return MineBitDesc(1, 2, 18, true);
                case Dark.Mine2GM0218: return MineBitDesc(2, 2, 18);
                case Dark.Mine1GM0731: return MineBitDesc(1, 7, 31, true);
                case Dark.Mine2GM0731: return MineBitDesc(2, 7, 31);
                case Dark.Mine1GM3127: return MineBitDesc(1, 31, 27, true);
                case Dark.Mine2GM3127: return MineBitDesc(2, 31, 27);
                case Dark.Mine1GM3121: return MineBitDesc(1, 31, 21, true);
                case Dark.Mine2GM3121: return MineBitDesc(2, 31, 21);
                case Dark.Mine2GM0010: return MineBitDesc(2, 0, 10, true);
                case Dark.Mine3GM0010: return MineBitDesc(3, 0, 10);
                case Dark.Mine1GM0010: return MineBitDesc(1, 0, 10);
                case Dark.Mine2GM0018: return MineBitDesc(2, 0, 18, true);
                case Dark.Mine3GM0018: return MineBitDesc(3, 0, 18);
                case Dark.Mine1GM0018: return MineBitDesc(1, 0, 18);
                case Dark.Mine2GM0431: return MineBitDesc(2, 4, 31, true);
                case Dark.Mine3GM0431: return MineBitDesc(3, 4, 31);
                case Dark.Mine1GM0431: return MineBitDesc(1, 4, 31);
                case Dark.Mine2GM2531: return MineBitDesc(2, 25, 31, true);
                case Dark.Mine3GM2531: return MineBitDesc(3, 25, 31);
                case Dark.Mine1GM2531: return MineBitDesc(1, 25, 31);
                case Dark.Mine2GM3131: return MineBitDesc(2, 31, 31, true);
                case Dark.Mine3GM3131: return MineBitDesc(3, 31, 31);
                case Dark.Mine1GM3131: return MineBitDesc(1, 31, 31);
                case Dark.Mine1GM1316: return MineBitDesc(1, 13, 16);
                case Dark.Mine1GM1418: return MineBitDesc(1, 14, 18);
                case Dark.Mine1GM0021: return MineBitDesc(1, 0, 21);
                case Dark.Mine1GM0031: return MineBitDesc(1, 0, 31);
                case Dark.Mine1GM1527: return MineBitDesc(1, 15, 27);
                case Dark.Mine1GM1105: return MineBitDesc(1, 11, 5, true);
                case Dark.Mine2GM1105: return MineBitDesc(2, 11, 5);
                case Dark.Mine1GM1201: return MineBitDesc(1, 12, 1, true);
                case Dark.Mine2GM1201: return MineBitDesc(2, 12, 1);
                case Dark.Mine1GM0023: return MineBitDesc(1, 0, 23, true);
                case Dark.Mine2GM0023: return MineBitDesc(2, 0, 23);
                case Dark.Mine1GM0028: return MineBitDesc(1, 0, 28, true);
                case Dark.Mine2GM0028: return MineBitDesc(2, 0, 28);
                case Dark.Mine2GM0801: return MineBitDesc(2, 8, 1, true);
                case Dark.Mine3GM0801: return MineBitDesc(3, 8, 1);
                case Dark.Mine1GM0801: return MineBitDesc(1, 8, 1);
                case Dark.Mine2GM0026: return MineBitDesc(2, 0, 26);
                case Dark.Mine3GM0026: return MineBitDesc(3, 0, 26);
                case Dark.Mine1GM0026: return MineBitDesc(1, 0, 26);
                case Dark.DeliverOrbET40408: 
                    bd = new BitDesc("Talk to Ellinger", MM45.Spots.Ellinger);
                    bd.AddChecked("Talk to Oorla", MM5Map.A4Castleview, 17, 10);
                    bd.AddChecked(enter, MM5Map.A4Castleview, 3, 2);
                    return bd;
                case Dark.ActivateMirrorsCK30101:
                    bd = new BitDesc("Activate the portals", MM5Map.A4CastleKalindraLevel3, 1, 1);
                    bd.AddChecked("Use the mirror portal", new MapXY[] { new MapXY(MM5Map.A4EllingersTowerLevel1, 9, 8), new MapXY(MM5Map.A3WesternTowerLevel1, 7, 12), new MapXY(MM5Map.D4SouthernTowerLevel1, 9, 11), new MapXY(MM5Map.F3EasternTowerLevel2, 10, 8), new MapXY(MM5Map.D1NorthernTowerLevel1, 5, 5) });
                    return bd;
                case Dark.ChosenOnesGP40609: 
                    bd = new BitDesc("Tell Pharaoh about Corak", MM5Map.D2TheGreatPyramidLevel4, 6, 9);
                    bd.AddChecked("Talk to Lucio", MapXY.Combine(MapXY.Array(MM5Map.B3SkyroadB3, 11, 8), MapXY.Array(MM5Map.C2SkyroadC2, 14, 5, 3, 5, 4, 8)));
                    return bd;
                case Dark.BoatSB31108: bd = new BitDesc("Board the boat", new MapXY[] { new MapXY(MM5Map.B3SkyroadB3, 11, 8),
                    new MapXY(MM5Map.C2SkyroadC2, 14, 5), new MapXY(MM5Map.C2SkyroadC2, 3, 5),
                    new MapXY(MM5Map.C2SkyroadC2, 4, 8), new MapXY(MM5Map.F4SkyroadF4, 6, 9) } );
                    bd.AddChecked(enter, MapXY.Combine(MapXY.Array(MM5Map.B3SkyroadB3, 11, 10, 11, 11, 11, 12, 11, 13, 11, 9, 12, 10, 12, 11, 12, 12,
                            12, 13, 12, 6, 12, 7, 12, 8, 12, 9, 13, 12, 13, 13, 14, 12, 14, 13, 15, 12, 15, 13),
                        MapXY.Array(MM5Map.C2SkyroadC2, 10, 5, 10, 6, 10, 7, 10, 8, 11, 5, 11, 6, 12, 5, 12, 6, 13, 5, 13, 6, 14, 6, 2, 0, 2, 1,
                            2, 2, 2, 3, 2, 4, 3, 0, 3, 1, 3, 2, 3, 3, 3, 4, 5, 7, 5, 8, 6, 7, 6, 8, 7, 7, 7, 8, 8, 7, 8, 8, 9, 5, 9, 6, 9, 7, 9, 8),
                        MapXY.Array(MM5Map.C3SkyroadC3, 0, 12, 0, 13, 1, 12, 1, 13, 2, 12, 2, 13, 2, 14, 2, 15, 3, 12, 3, 13, 3, 14, 3, 15), 
                        MapXY.Array(MM5Map.E4SkyroadE4, 11, 10, 11, 11, 11, 12, 11, 13, 11, 14, 11, 15, 11, 9, 12, 9, 13, 9, 14, 9, 15, 9),
                        MapXY.Array(MM5Map.F4SkyroadF4, 0, 9, 1, 9, 2, 9, 3, 9, 4, 9, 5, 9)));
                    bd.AddZero("Finish boat ride", MapXY.Combine(MapXY.Array(MM5Map.B3SkyroadB3, 12, 6), 
                        MapXY.Array(MM5Map.C2SkyroadC2, 14, 6, 2, 4, 5, 7), MapXY.Array(MM5Map.E4SkyroadE4, 11, 15)));
                    return bd;
                case Dark.AnswerMerchantSD41203:
                    bd = new BitDesc("Answer \"steam\"", MM5Map.D4SkyroadD4, 12, 3);
                    bd.AddSet("Answer \"mud\"", new MapXY(MM5Map.F2SkyroadF2, 12, 9));
                    return bd;
                case Dark.AnswerMerchantSA20304: return new BitDesc("Answer \"smoke\"", MM5Map.A2SkyroadA2, 3, 4);
                case Dark.AnswerMerchantSC10212: return new BitDesc("Answer \"dust\"", MM5Map.C1SkyroadC1, 2, 12);
                case Dark.FireSleeperEPF0312: return new BitDesc("Wake the Fire Sleeper", MM5Map.A1ElementalPlaneOfFire, 3, 12);
                case Dark.AirSleeperEPA1212: return new BitDesc("Wake the Air Sleeper", MM5Map.F1ElementalPlaneOfAir, 12, 12);
                case Dark.EarthSleeperEPE1203: return new BitDesc("Wake the Earth Sleeper", MM5Map.F4ElementalPlaneOfEarth, 12, 3);
                case Dark.WaterSleeperEPW0303: return new BitDesc("Wake the Water Sleeper", MM5Map.A4ElementalPlaneOfWater, 3, 3);
                case Dark.WaterTestEPW0411: return new BitDesc("Accept the test", MM5Map.A4ElementalPlaneOfWater, 4, 11, "Open the chest", MM5Map.A4ElementalPlaneOfWater, 9, 1);
                case Dark.FireTestEPF1013: return new BitDesc("Accept the test", MM5Map.A1ElementalPlaneOfFire, 10, 13, "Open the chest", MM5Map.A1ElementalPlaneOfFire, 2, 4);
                case Dark.AirTestEPA1002: return new BitDesc("Accept the test", MM5Map.F1ElementalPlaneOfAir, 10, 2, "Open the chest", MM5Map.F1ElementalPlaneOfAir, 5, 13);
                case Dark.EarthTestEPE0304: return new BitDesc("Accept the test", MM5Map.F4ElementalPlaneOfEarth, 3, 4, "Open the chest", MM5Map.F4ElementalPlaneOfEarth, 13, 12);
                case Dark.OpenChestCS3026: return new BitDesc("Open the chest", MM5Map.A4CastleviewSewer, 30, 26, "Search the bed", MM5Map.A4CastleviewSewer, 31, 26);
                case Dark.AnswerRiddleNT10410: return new BitDesc("Answer \"eeeioie\"", MM5Map.D1NorthernTowerLevel1, 4, 10, enter);
                case Dark.AnswerRiddleNT11010: return new BitDesc("Answer \"eoauaaue\"", MM5Map.D1NorthernTowerLevel1, 10, 10, enter);
                case Dark.AnswerRiddleNT20511: return new BitDesc("Answer \"eeeoeaoueieeeoee\"", MM5Map.D1NorthernTowerLevel2, 5, 11, enter);
                case Dark.AnswerRiddleNT20911: return new BitDesc("Answer \"oeooeieoooaei\"", MM5Map.D1NorthernTowerLevel2, 9, 11, enter);
                case Dark.AnswerRiddleNT30410: return new BitDesc("Answer \"oooaioeieou\"", MM5Map.D1NorthernTowerLevel3, 4, 10, enter);
                case Dark.AnswerRiddleNT31010: return new BitDesc("Answer \"aooaioeaeooae\"", MM5Map.D1NorthernTowerLevel3, 10, 10, enter);
                case Dark.AnswerRiddleNT31006: return new BitDesc("Answer \"iieeeoeeeouie\"", MM5Map.D1NorthernTowerLevel3, 10, 6, enter);
                case Dark.ChooseGoldB21202: return new BitDesc("Choose 100,000 gold", MM5Map.B2Surface, 12, 2, enter, MM5Map.B2Surface, 8, 11);
                case Dark.DestroyBarbarianB31310: return new BitDesc("Destroy the Barbarian Camp", MM5Map.B3Surface, 13, 10);
                case Dark.AnswerPaladinC21110:
                    bd = new BitDesc("Answer \"Paladin\"", MM5Map.C2Surface, 11, 10);
                    bd.AddChecked("Search the boulder", MapXY.Combine(MapXY.Array(MM5Map.C2Surface, 11, 0, 11, 5), 
                        MapXY.Array(MM5Map.D2Surface, 0, 0, 0, 10, 5, 0, 5, 10, 5, 5)));
                    return bd;
                case Dark.DestroyOgreD30307: return new BitDesc("Destroy the Ogre Fort", MM45.Spots.OgreFort2, "Talk to Kramer", MM45.Spots.Kramer);
                case Dark.FlipHourglassLSD11110: return new BitDesc("Flip the hourglass", ls1, 11, 10, stairs, ls2, 7, 1);
                case Dark.FlipHourglassLSD11409: return new BitDesc("Flip the hourglass", ls1, 14, 9, stairs, ls2, 7, 1);
                case Dark.FlipHourglassLSD10510: return new BitDesc("Flip the hourglass", ls1, 5, 10, stairs, ls2, 7, 1);
                case Dark.FlipHourglassLSD10209: return new BitDesc("Flip the hourglass", ls1, 2, 9, stairs, ls2, 7, 1);
                case Dark.PullLeverLSD30101: return new BitDesc("Pull the lever", ls3, 1, 1, stairs, ls3, 7, 14);
                case Dark.PullLeverLSD31402: return new BitDesc("Pull the lever", ls3, 14, 2, stairs, ls3, 7, 14);
                case Dark.PullLeverLSD31314: return new BitDesc("Pull the lever", ls3, 13, 14, stairs, ls3, 7, 14);
                case Dark.PullLeverLSD30110: return new BitDesc("Pull the lever", ls3, 1, 10, stairs, ls3, 7, 14);
                case Dark.DrinkWaterLSD24040: return new BitDesc("Drink the water", MapXY.Combine(MapXY.Array(ls2, 7, 6, 7, 9, 7, 12), MapXY.Array(ls4, 1, 5)));
                case Dark.DeliverDisks2ET40408:
                    bd = new BitDesc("Deliver 10 Energy Disks", MM45.Spots.Ellinger);
                    bd.AddChecked(stairs, MapXY.Array(MM5Map.A4CastleKalindraLevel1, 0, 7, 15, 0, 15, 15, 6, 3, 6, 5));
                    return bd;
                case Dark.DeliverDisks3ET40408:
                    bd = new BitDesc("Deliver 15 Energy Disks", MM45.Spots.Ellinger);
                    bd.AddChecked(stairs, MapXY.Array(MM5Map.A4CastleKalindraLevel1, 13, 15, 4, 3));
                    return bd;
                case Dark.DeliverDisks4ET40408:
                    bd = new BitDesc("Deliver 20 Energy Disks", MM45.Spots.Ellinger);
                    bd.AddChecked(stairs, MapXY.Combine(MapXY.Array(MM5Map.A4CastleKalindraLevel2, 15, 10, 15, 5, 4, 3, 4, 7, 5, 5),
                        MapXY.Array(MM5Map.A4SkyroadA4, 5, 14)));
                    return bd;
                case Dark.RescueKalindra1CBD0101:
                    bd = new BitDesc("Complete Kalindra's quest", MM45.Spots.Kalindra);
                    bd.AddChecked("Talk to Kalindra", MM5Map.A4CastleKalindraLevel1, 2, 7);
                    bd.AddChecked("Talk to Oorla", MM5Map.A4Castleview, 17, 10);
                    return bd;
                case Dark.TurnInSongbird1CK21015:
                    bd = new BitDesc("Complete Megan's quest", MM5Map.A4CastleKalindraLevel2, 10, 15);
                    bd.AddChecked("Open the safe", MapXY.Array(MM5Map.A4CastleKalindraLevel2, 0, 10, 0, 12, 0, 15, 2, 15, 4, 15, 8, 15));
                    return bd;
                case Dark.LearnCombinationCBD0101: return new BitDesc("Learn the combination 3-31-62", MM45.Spots.Kalindra,
                    "Open the safe", MM45.Spots.KalindraSafe);
                case Dark.TurnInSandroN1008: return new BitDesc("Complete Sandro's quest", MM45.Spots.Sandro, 
                    "Take the statuette", MM45.Spots.GriffinStatuette);
                case Dark.BeginSaveQueenCBD0101: return new BitDesc("Begin Kalindra's quest", MM45.Spots.Kalindra,
                    "Talk to Dimitri", MM45.Spots.Dimitri);
                case Dark.RescueKalindra2CBD0101:
                    bd = new BitDesc("Complete Kalindra's quest", MM45.Spots.Kalindra);
                    bd.AddChecked("Talk to Dimitri", MM5Map.A4CastleKalindraLevel2, 10, 15);
                    bd.AddChecked("Talk to Kalindra", MM5Map.A4CastleKalindraLevel1, 2, 7);
                    return bd;
                case Dark.TurnInSongbird2CK21015:
                    bd = new BitDesc("Complete Megan's quest", MM5Map.A4CastleKalindraLevel2, 10, 15);
                    bd.AddChecked("Talk to Megan", MM45.Spots.Megan);
                    bd.AddChecked("Talk to Kalindra", MM45.Spots.Kalindra);
                    return bd;
                case Dark.TurnInWesternTowerA30810: return new BitDesc("Complete Dreyfus' quest", MM45.Spots.Dreyfus, "Talk to Dreyfus", MM45.Spots.Dreyfus2);
                case Dark.TurnInEnchantBridleB11205: return new BitDesc("Complete Ambrose' quest", MM45.Spots.Ambrose, 
                    "Talk to Ambrose", MM45.Spots.Ambrose2);
                case Dark.YogEnemyB30612: return new BitDesc("", null, "Talk to Yog", new MapXY(MM5Map.B3Surface, 6, 12));
                case Dark.YogFriendB30612: return new BitDesc("", null, "Talk to Yog", new MapXY(MM5Map.B3Surface, 6, 12));
                case Dark.RescueSheewanaTB30612:
                    bd = new BitDesc("Rescue Sheewana", MM5Map.C4TempleOfBarkLevel3, 6, 12);
                    bd.AddChecked("Talk to Sharla", MM45.Spots.Sharla);
                    bd.AddChecked("Talk to Gurodel", MM5Map.D1Surface, 10, 5);
                    return bd;
                case Dark.DeliverDisks1ET40408: return new BitDesc("Deliver 5 Energy Disks", MM45.Spots.Ellinger,
                    "Enter the castle", MM45.Spots.CastleKalindra);
                case Dark.FreeCorakEP10408: return new BitDesc("Free Corak", MM5Map.B2EscapePod1, 4, 8,
                    "Talk to Dragon Pharaoh", MM5Map.D2TheGreatPyramidLevel4, 6, 9);
                case Dark.DragonParaohGP40609:
                    bd = new BitDesc("Talk to Dragon Pharaoh", MM5Map.D2TheGreatPyramidLevel4, 6, 9);
                    bd.AddChecked("Enter Escape Pod", MM5Map.B2Surface, 3, 8);
                    bd.AddChecked("Talk to Oorla", MM5Map.A4Castleview, 17, 10);
                    return bd;
                case Dark.EnterBarkC40208: return new BitDesc("Attempt to enter", MM45.Spots.TempleOfBark, "Talk to Nibbler", MM45.Spots.Nibbler);
                case Dark.DestroyOgreD30908: return new BitDesc("Destroy the Ogre Fort", MM45.Spots.OgreFort1, "Talk to Kramer", MM45.Spots.Kramer);
            default: return BitDesc.Empty;
            }
           
        }

        public static BitDesc Description(World bit)
        {
            // These bits are used for the auto-notes
            switch(bit)
            {
                case World.ShrineElectricityD31504: return new BitDesc("Use the Shrine of Electricity (Elec Res +50)", MM4Map.D3Surface, 15, 4);
                case World.ShrineColdA41214: return new BitDesc("Use the Shrine of Cold (Cold Res +50)", MM4Map.A4Surface, 12, 14);
                case World.ShrinePoisonF31406: return new BitDesc("Use the Shrine of Poison (Poison Res +50)", MM4Map.F3Surface, 14, 6);
                case World.ShrineMagicC31500: return new BitDesc("Use the Shrine of Magic (Magic Res +50)", MM4Map.C3Surface, 15, 0);
                case World.ShrineEnergyA10706: return new BitDesc("Use the Shrine of Energy (Energy Res +50)", MM4Map.A1Surface, 7, 6);
                case World.ElementalShrineE30914: return new BitDesc("Use the Elemental Shrine (Elemental Res +20)", MM4Map.E3Surface, 9, 14);
                case World.WellMightD20308: return new BitDesc("Use the Well of Might (Might +50)", MM4Map.D2Surface, 3, 8);
                case World.WellIntellectB31504: return new BitDesc("Use the Well of Intellect (Intellect +50)", MM4Map.B3Surface, 15, 4);
                case World.WellPersonalityC30000: return new BitDesc("Use the Well of Personality (Personality +50)", MM4Map.C3Surface, 0, 0);
                case World.WellEnduranceC10204: return new BitDesc("Use the Well of Endurance (Endurance +50)", MM4Map.C1Surface, 2, 4);
                case World.WellSpeedE20304: return new BitDesc("Use the Well of Speed (Speed +50)", MM4Map.E2Surface, 3, 4);
                case World.WellAccuracyB30003: return new BitDesc("Use the Well of Accuracy (Accuracy +50)", MM4Map.B3Surface, 0, 3);
                case World.WishingWellF30107: return new BitDesc("Use the Wishing Well (Luck +60)", MM4Map.F3Surface, 1, 7);
                case World.OlympianFountainC31510: return new BitDesc("Use the Olympian Fountain (Might/Endurance/Speed/Accuracy +10)", MM4Map.C3Surface, 15, 10);
                case World.SagesFountainD30809: return new BitDesc("Use the Sage's Fountain (Intellect/Personality +10)", MM4Map.D3Surface, 8, 9);
                case World.WatersPowerF30707: return new BitDesc("Use the Waters of Power (HP +25)", MM4Map.F3Surface, 7, 7);
                case World.WatersGreatPowerA10412: return new BitDesc("Use the Waters of Great Power (HP +250)", MM4Map.A1Surface, 4, 12);
                case World.WatersMagicE30806: return new BitDesc("Use the Waters of Magic (SP +25)", MM4Map.E3Surface, 8, 6);
                case World.WatersGreatMagicA40303: return new BitDesc("Use the Waters of Great Magic (SP +250)", MM4Map.A4Surface, 3, 3);
                case World.FountainProtectionF31212: return new BitDesc("Use the Fountain of Protection (AC +5)", MM4Map.F3Surface, 12, 12);
                case World.FountainGreatProtectionA30314: return new BitDesc("Use the Fountain of Great Protection (AC +30)", MM4Map.A3Surface, 3, 14);
                case World.FountainAbilityF30001: return new BitDesc("Use the Fountain of Ability (Level +5)", MM4Map.F3Surface, 0, 1);
                case World.VertigoWellV1417: return new BitDesc("Use the Vertigo Well (HP Restored)", MM4Map.F3Vertigo, 14, 17);
                case World.NightshadowWellN0707: return new BitDesc("Use the Nightshadow Well (Level +10)", MM4Map.D4Nightshadow, 7, 7);
                case World.RivercityWellR1418: return new BitDesc("Use the Rivercity Well (SP +100)", MM4Map.C3Rivercity, 14, 18);
                case World.AspWellA0803: return new BitDesc("Use the Asp Well (HP +100)", MM4Map.C2Asp, 8, 3);
                case World.WinterkillWellW0611: return new BitDesc("Use the Winterkill Well (Might +50)", MM4Map.A3Winterkill, 6, 11);
                case World.TurnedSeasonsE30314: return new BitDesc("Turn the seasons (Remove Aging)", MM4Map.B1SphinxBody, 7, 15);
                case World.PasswordGoluxSB0715: return new BitDesc("Learn the password \"golux\"", MM4Map.A1BasenjiDungeon, 14, 12);
                case World.PasswordThereWolfCB20915: return new BitDesc("Learn the password \"There Wolf\"", MM4Map.A1CastleBasenjiLevel2, 9, 15);
                case World.PasswordRosebudWT40410: return new BitDesc("Learn the password \"Rosebud\"", MapXY.Array(MM4Map.F4WitchTowerLevel4, 10, 6, 4, 10));
                case World.PortalVertigoR2418: return new BitDesc("Learn the \"Vertigo\" portal keyword", MM4Map.C3Rivercity, 24, 18);
                case World.PortalNightshadowR2418: return new BitDesc("Learn the \"Nightshadow\" portal keyword", MM4Map.C3Rivercity, 24, 18);
                case World.PortalRivercityR2418: return new BitDesc("Learn the \"Rivercity\" portal keyword", MM4Map.C3Rivercity, 24, 18);
                case World.PortalAspR2418: return new BitDesc("Learn the \"Asp\" portal keyword", MM4Map.C3Rivercity, 24, 18);
                case World.PortalWinterkillR2418: return new BitDesc("Learn the \"Winterkill\" portal keyword", MM4Map.C3Rivercity, 24, 18);
                case World.PortalWarzoneR2214: return new BitDesc("Learn the \"Warzone\" portal keyword", MM4Map.C3Rivercity, 22, 14);
                case World.MineCodesDM10811: return new BitDesc("Learn the travel codes \"Mine 1\" through \"Mine 5\"",
                    MapXY.Combine(MapXY.Array(MM4Map.F3DwarfMine1, 8, 11), MapXY.Array(MM4Map.F3DwarfMine2, 8, 29), MapXY.Array(MM4Map.E2DwarfMine3, 5, 13),
                    MapXY.Array(MM4Map.E2DwarfMine4, 6, 5), MapXY.Array(MM4Map.D2DwarfMine5, 7, 3)));
                case World.DwarvenCodeADM11119: return new BitDesc("Learn the Dwarven travel code \"A----\"", MM4Map.F3DwarfMine1, 11, 19);
                case World.DwarvenCodeLDM20603: return new BitDesc("Learn the Dwarven travel code \"-L---\"", MM4Map.F3DwarfMine2, 6, 3);
                case World.DwarvenCodePDM30600: return new BitDesc("Learn the Dwarven travel code \"--P--\"", MM4Map.E2DwarfMine3, 6, 0);
                case World.DwarvenCodeHDM40604: return new BitDesc("Learn the Dwarven travel code \"---H-\"", MapXY.Array(MM4Map.E2DwarfMine4, 1, 4, 6, 4));
                case World.DwarvenCodeADM50308: return new BitDesc("Learn the Dwarven travel code \"----A\"", MM4Map.D2DwarfMine5, 3, 8);
                case World.TravelCodeAlphaR2218: return new BitDesc("Learn the travel code \"Alpha\"", MM4Map.C3Rivercity, 22, 18);
                case World.TravelCodeThetaDMA3011: return new BitDesc("Learn the travel code \"Theta\"", MM4Map.DeepMineAlpha, 30, 11);
                case World.TravelCodeKappaDMT0615: return new BitDesc("Learn the travel code \"Kappa\"", MM4Map.DeepMineTheta, 6, 15);
                case World.TravelCodeOmegaDMK1910: return new BitDesc("Learn the travel code \"Omega\"", MM4Map.DeepMineKappa, 19,10);
                case World.ShrineFireE21303: return new BitDesc("Use the Shrine of Fire (Fire Res +50)", MM4Map.E2Surface, 13, 3);
                case World.StatueWC2709: return new BitDesc("Learn \"Golem, MM4Map.Terror, and Yak it's told have credits for King to hold\"", MM4Map.F4WitchClouds, 27, 9);
                case World.StatueWC2725: return new BitDesc("Learn \"The Clerics of Yak you must outwit; taxman then must have his bit.\"", MM4Map.F4WitchClouds, 27, 25);
                case World.StatueWC0325: return new BitDesc("Learn \"Five for your keep is not too dear since Taxman gives stone to fear.\"", MM4Map.F4WitchClouds, 3, 25);
                case World.StatueWC0309: return new BitDesc("Learn \"Conquer terror and then you'll see illusion holds Magic's key.\"", MM4Map.F4WitchClouds, 3, 9);
                case World.StatueHMC2414: return new BitDesc("Learn \"Five more will raise your castle's walls Taxman opens Golem's halls.\"", MM4Map.C4HighMagicClouds, 24, 14);
                case World.StatueHMC177: return new BitDesc("Learn \"Highest magic holds solution Darzog's dark convolution.\"", MM4Map.C4HighMagicClouds, 17, 7);
                case World.StatueHMC0705: return new BitDesc("Learn \"A secret room in tower drear holds the land's lost overseer.\"", MM4Map.C4HighMagicClouds, 7, 5);
                case World.StatueHMC0617: return new BitDesc("Learn \"Though evil wizard has been beat you can't yet Lord Xeen defeat.\"", MM4Map.C4HighMagicClouds, 6, 17);
                case World.StatueCX1009: return new BitDesc("Learn \"Advisor will a dig permit five coins more you must submit.\"", MM4Map.D3CloudsOfXeen, 10, 9);
                case World.StatueCX0701: return new BitDesc("Learn \"Taxman becomes a faithful vassal Xeen's bane lies under castle.\"", MM4Map.D3CloudsOfXeen, 7, 1);
                case World.StatueCX2709: return new BitDesc("Learn \"In clouds above island tower waits Lord Xeen his final hour.\"", MM4Map.D3CloudsOfXeen, 27, 9);
                case World.StatueCX2601: return new BitDesc("Learn \"Only the sword Xeen's life can take All his plans of war unmake.\"", MM4Map.D3CloudsOfXeen, 26, 1);
                case World.NewcastleWellNC10914: return new BitDesc("Use Your Well (AC +20)", MM4Map.C4NewcastleLevel1, 9, 14);
                case World.PasswordLaboratoryNC11214: return new BitDesc("Learn the password \"laboratory\"", MM4Map.C4NewcastleLevel1, 12, 14);
                case World.NoteLANC10101: return new BitDesc("Learn \"la\"", MM4Map.C4NewcastleLevel1, 1, 1);
                case World.NoteBONC10614: return new BitDesc("Learn \"bo\"", MM4Map.C4NewcastleLevel1, 6, 14);
                case World.NoteRANC11401: return new BitDesc("Learn \"ra\"", MM4Map.C4NewcastleLevel1, 14, 1);
                case World.NoteTONC21008: return new BitDesc("Learn \"to\"", MM4Map.C4NewcastleLevel2, 10, 8);
                case World.NoteRYNC21106: return new BitDesc("Learn \"ry\"", MM4Map.C4NewcastleLevel2, 11, 6);
                case World.GoluxSD1511: return new BitDesc("Learn \"My first is in giant but not in defiant.\"", MM4Map.B1SphinxDungeon, 15, 11);
                case World.GoluxSD1509: return new BitDesc("Learn \"My second is in mole but not in mule.\"", MM4Map.B1SphinxDungeon, 15, 9);
                case World.GoluxSD1507: return new BitDesc("Learn \"My third is in lich but not in stitch.\"", MM4Map.B1SphinxDungeon, 15, 7);
                case World.GoluxSD0005: return new BitDesc("Learn \"My fourth is in guard but not in Girard.\"", MM4Map.B1SphinxDungeon, 0, 5);
                case World.GoluxSD0007: return new BitDesc("Learn \"My fifth is in XEEN but not in Queen.\"", MM4Map.B1SphinxDungeon, 0, 7);
                case World.TrollHolesTH2027: return new BitDesc("Learn the first set of Troll Holes locations", MapXY.Array(MM5Map.E4TrollHoles, 20, 27, 29, 1, 4, 12));
                case World.TrollHolesTH3013: return new BitDesc("Learn the second set of Troll Holes locations", MapXY.Array(MM5Map.E4TrollHoles, 15, 20, 20, 23, 30, 13));
                case World.CliffNotesNT40909: return new BitDesc("Learn Cliff Notes Lesson 1 (EEEIOIE)", MM5Map.D1NorthernTowerLevel4, 9, 9);
                case World.CliffNotesNT41009: return new BitDesc("Learn Cliff Notes Lesson 2 (EOAUAAUE)", MM5Map.D1NorthernTowerLevel4, 10, 9);
                case World.CliffNotesNT41007: return new BitDesc("Learn Cliff Notes Lesson 3 (EEEOEAOUEIEEEOEE)", MM5Map.D1NorthernTowerLevel4, 10, 7);
                case World.CliffNotesNT41006: return new BitDesc("Learn Cliff Notes Lesson 4 (OEOOEIEOOOAEI)", MM5Map.D1NorthernTowerLevel4, 10, 6);
                case World.CliffNotesNT40406: return new BitDesc("Learn Cliff Notes Lesson 5 (OOOAIOEIEOU)", MM5Map.D1NorthernTowerLevel4, 4, 6);
                case World.CliffNotesNT40407: return new BitDesc("Learn Cliff Notes Lesson 6 (AOOAIOEAEOOAE)", MM5Map.D1NorthernTowerLevel4, 4, 7);
                case World.CliffNotesNT40410: return new BitDesc("Learn Cliff Notes Lesson 7 (IIEEEOEEEOUIE)", MM5Map.D1NorthernTowerLevel4, 4, 10);
                case World.FountainSuperResilienceA10213: return new BitDesc("Use the Fountain of Super Resilience (HP +2500)", MM5Map.A1Surface, 2, 13);
                case World.FountaintheElementsB11413: return new BitDesc("Use the Fountain of the Elements (All Res +100)", MM5Map.B1Surface, 14, 13);
                case World.PrayA30814: return new BitDesc("Pray at the shrine (Level +3)", MM5Map.A3Surface, 8, 14);
                case World.WellResilienceA30303: return new BitDesc("Use the Well of Resilience (HP +50)", MM5Map.A3Surface, 3, 3);
                case World.WellSpellsA30210: return new BitDesc("Use the Well of Spells (SP +50)", MM5Map.A3Surface, 2, 10);
                case World.WellMightA40911: return new BitDesc("Use the Well of Might (Might +25)", MM5Map.A4Surface, 9, 11);
                case World.WellProtectionA40310: return new BitDesc("Use the Well of Protection (AC +10)", MM5Map.A4Surface, 3, 10);
                case World.WishingWellB40202: return new BitDesc("Use the Wishing Well (Luck +100)", MM5Map.B4Surface, 2, 2);
                case World.ShrineGreatPowerC20108: return new BitDesc("Use the Shrine of Great Power (Level +15)", MM5Map.C2Surface, 1, 8);
                case World.FountainMightD10613: return new BitDesc("Use the Fountain of Might (Might +100)", MM5Map.D1Surface, 6, 13);
                case World.PrayD41204: return new BitDesc("Pray at the shrine (All Stats +10)", MM5Map.D4Surface, 12, 4);
                case World.WellElementalResistanceD40204: return new BitDesc("Use Well of Elemental Resistance (Elemental Res +50)", MM5Map.D4Surface, 2, 4);
                case World.FountainMagicResistanceF20805: return new BitDesc("Use the Fountain of Magic Resistance (Magic Res +50)", MM5Map.F2Surface, 8, 5);
                case World.FountainGreatProtectionF41403: return new BitDesc("Use the Fountain of Great Protection (Ac +50)", MM5Map.F4Surface, 14, 3);
                case World.FountainYouthF40607: return new BitDesc("Use the Fountain of Youth (Remove Aging)", MM45.Spots.Thaddeus);
                case World.PlaqueO0101: return new BitDesc("Learn \"If Pharaoh's realm should take a fall...\"", MM5Map.C2Olympus, 1, 1);
                case World.PlaqueO0114: return new BitDesc("Learn \"A Golden bird will sorrow ease...\"", MM5Map.C2Olympus, 1, 14);
                case World.PlaqueO1414: return new BitDesc("Learn \"Dragon's orb the world shall roam...\"", MM5Map.C2Olympus, 14, 14);
                case World.PlaqueO1401: return new BitDesc("Learn \"Dragon once more will get you by...\"", MM5Map.C2Olympus, 14, 1);
                case World.CombinationCK21115: return new BitDesc("Learn Dimitri's combination 64,52,31", MM5Map.A4CastleKalindraLevel2, 10, 15);
                case World.CombinationCBD0101: return new BitDesc("Learn Kalindra's combination 3,31,62", MM45.Spots.Kalindra);
                case World.TreasureE40013: return new BitDesc("Learn about the treasure at E4 0,13", MM5Map.E2Surface, 8, 15).AddZero(
                    "Collect the treasure", new MapXY(MM5Map.E4Surface, 0, 13));
                case World.LedgerST20406: return new BitDesc("Read the ledger", MM5Map.D4SouthernTowerLevel2, 4, 6);
                case World.FountainGreatMagicE10210: return new BitDesc("Use the Fountain of Great Magic (SP +1000)", MM5Map.E1Surface, 2, 10);
                case World.FountainGreatResilienceF11308: return new BitDesc("Use the Fountain of Great Resiliencea (HP +500)", MM5Map.F1Surface, 13, 8);
                case World.AlamarPathCAD0512: return new BitDesc("Learn Alamar's Path (FEWAEEFFWAEFAWEEWWEFAW)", MM5Map.A1CastleAlamarDungeon, 5, 12);
                case World.AlamarTimeCAD0808: return new BitDesc("Learn \"Nine is the time.\"", MM5Map.A1CastleAlamarDungeon, 8, 8);
                case World.DragonRunesDC0031: return new BitDesc("Learn the Dragon Runes \"IN\"", MM5Map.D1DragonClouds, 0, 31);
                case World.DragonRunesDC0000: return new BitDesc("Learn the Dragon Runes \"FI\"", MM5Map.D1DragonClouds, 0, 0);
                case World.DragonRunesDC3000: return new BitDesc("Learn the Dragon Runes \"NI\"", MM5Map.D1DragonClouds, 30, 0);
                case World.DragonRunesDC3031: return new BitDesc("Learn the Dragon Runes \"TY\"", MM5Map.D1DragonClouds, 30, 31);
                case World.PlaqueDT10406: return new BitDesc("Learn \"NWDA---Y-HNA\"", MM5Map.B3DarkstoneTowerLevel1, 4, 6);
                case World.PlaqueDT10410: return new BitDesc("Learn \"EO-NYFPIMTDG\"", MM5Map.B3DarkstoneTowerLevel1, 4, 10);
                case World.PlaqueDT11010: return new BitDesc("Learn \"WRTKOOLNI--I\"", MM5Map.B3DarkstoneTowerLevel1, 10, 10);
                case World.PlaqueDT11006: return new BitDesc("Learn \"-LHSURAGGAMC\"", MM5Map.B3DarkstoneTowerLevel1, 10, 6);
                case World.PlaqueDT20406: return new BitDesc("Learn \"SNTS-CKDEM\"", MM5Map.B3DarkstoneTowerLevel2, 4, 6);
                case World.PlaqueDT20410: return new BitDesc("Learn \"ODHAOO--L!\"", MM5Map.B3DarkstoneTowerLevel2, 4, 10);
                case World.PlaqueDT21010: return new BitDesc("Learn \"-SEGFRAST-\"", MM5Map.B3DarkstoneTowerLevel2, 10, 10);
                case World.PlaqueDT21006: return new BitDesc("Learn \"E--A-ANHE-\"", MM5Map.B3DarkstoneTowerLevel2, 10, 6);
                case World.PlaqueDT30406: return new BitDesc("Learn \"1-ITN-HNA-A\"", MM5Map.B3DarkstoneTowerLevel3, 4, 6);
                case World.PlaqueDT30410: return new BitDesc("Learn \"9BNHEMTDGS!\"", MM5Map.B3DarkstoneTowerLevel3, 4, 10);
                case World.PlaqueDT31010: return new BitDesc("Learn \"9ESEXI--IA-\"", MM5Map.B3DarkstoneTowerLevel3, 10, 10);
                case World.PlaqueDT31006: return new BitDesc("Learn \"4G--TGAMCG-\"", MM5Map.B3DarkstoneTowerLevel3, 10, 6);
                case World.HeadDT40406: return new BitDesc("Learn \"T-MEOE-FENN-TEAO\"", MM5Map.B3DarkstoneTowerLevel4, 4, 6);
                case World.HeadDT40410: return new BitDesc("Learn \"Y\"PRNV4--G-DHE-N\"", MM5Map.B3DarkstoneTowerLevel4, 4, 10);
                case World.HeadDT41010: return new BitDesc("Learn \"PCU\"-E-TDEOE--S!\"", MM5Map.B3DarkstoneTowerLevel4, 10, 10);
                case World.HeadDT41006: return new BitDesc("Learn \"EOT-LLOHUOFASYO-\"", MM5Map.B3DarkstoneTowerLevel4, 10, 6);
                default: return BitDesc.Empty;
            }
        }

        public static BitDesc QuestBitDesc(string giver, string name, MapXY map, string receiver = null, MapXY map2 = null)
        {
            string strPossessive = giver + (giver.EndsWith("s") ? "'" : "'s");
            BitDesc bd = new BitDesc(String.Format("Start {0} \"{1}\" quest", strPossessive, name), map);
            if (receiver == null || map2 == null)
                bd.AddZero("Talk to " + giver, new MapXY[] { map });
            else
                bd.AddZero("Talk to " + receiver, new MapXY[] { map2 });
            return bd;
        }

        public static BitDesc Description(Quest bit)
        {
            switch (bit)
            {
                case Quest.SlayMadDwarfKing: return QuestBitDesc("Mayor Gunther", "Slay the king of the Mad Dwarf Clan", MM45.Spots.Gunther);
                case Quest.GatherPhirnaRoot: return QuestBitDesc("Myra the Herbalist", "Gather Phirna Roots", MM45.Spots.Myra);
                case Quest.FreeCelia: return QuestBitDesc("Derek", "Free Celia from the zombies", MM45.Spots.Derek);
                case Quest.RetrieveAlacorn: return QuestBitDesc("Valia", "Retrieve the Alacorn of Falista", MM45.Spots.Valia);
                case Quest.FindOrothinsWhistle: return QuestBitDesc("Orothin", "Find Orothin's bone whistle", MM45.Spots.Orothin);
                case Quest.FindLigonosSkull: return QuestBitDesc("Ligono", "Find Ligono's skull", MM45.Spots.Ligono);
                case Quest.GetBaroksPendant: return QuestBitDesc("Barok", "Get Barok's pendant", MM45.Spots.Barok);
                case Quest.GetRoxannesTiara: return QuestBitDesc("Princess Roxanne", "Get Princess Roxanne's tiara", MM45.Spots.Roxanne);
                case Quest.ReturnScarab: return QuestBitDesc("Carlawna", "Return the Scarab of Imaging", MM45.Spots.Carlawna);
                case Quest.ReturnCrystals: return QuestBitDesc("Falagar", "Return the Crystals of Piezoelectricity", MM45.Spots.Falagar);
                case Quest.StealElixir: return QuestBitDesc("Mirabeth", "Steal the Elixir of Restoration", MM45.Spots.Mirabeth);
                case Quest.FindFaeryWand: return QuestBitDesc("Danulf", "Find the Faery Wand", MM45.Spots.Danulf);
                case Quest.FindHolyBook: return QuestBitDesc("Tito", "Find the Holy Book of Elvenkind", MM45.Spots.Tito);
                case Quest.DestroyTrollLair: return QuestBitDesc("Thickbark", "Destroy the Lair of the Trolls", MM45.Spots.Thickbark);
                case Quest.DestroyOgreLair: return QuestBitDesc("Captain Nystor", "Destroy the Lair of the Ogres", MM45.Spots.Nystor);
                case Quest.ReclaimPagoda: return QuestBitDesc("Kai Wu", "Reclaim Kai Wu's Pagoda", MM45.Spots.KaiWu);
                case Quest.SlayLakeMonsters: return QuestBitDesc("Medin", "Slay the monsters of the lake", MM45.Spots.Medin);
                case Quest.SaveWinterkill: return QuestBitDesc("Randon", "Save Winterkill from it's curse", MM45.Spots.Randon);
                case Quest.DestroyCyclopsLair: return QuestBitDesc("Glom", "Destroy the lair of the Cyclops", MM45.Spots.Glom);
                case Quest.FindEverhotRock: return QuestBitDesc("Halon", "Find the Everhot Lava Rock", MM45.Spots.Halon);
                case Quest.RetrieveScrollOfInsight: return QuestBitDesc("Arie", "Retrieve the Scroll of Insight", MM45.Spots.Arie);
                case Quest.FreeCrodo: return QuestBitDesc("Artemus", "Free Crodo from Darzog", MM45.Spots.Artemus);
                case Quest.ClimbDarzogsTower: return new BitDesc("Start Crodo's \"Slay Lord Xeen\" quest", MM45.Spots.Crodo).AddZero("Defeat Lord Xeen", MM45.Spots.LordXeen);
                case Quest.FindMirror: return QuestBitDesc("King Burlock", "Find the 6th mirror", MM45.Spots.Burlock);
                case Quest.TakeLastFlower: return QuestBitDesc("the Summer Druid", "Last Flower of Summer", MM45.Spots.SummerDruid);
                case Quest.TakeLastLeaf: return QuestBitDesc("the Autumn Druid", "Last Fallen leaf of Autumn", MM45.Spots.AutumnDruid);
                case Quest.TakeLastSnowflake: return QuestBitDesc("the Winter Druid", "Last Snowflake of Winter", MM45.Spots.WinterDruid);
                case Quest.TakeLastRaindrop: return QuestBitDesc("the Spring Druid", "Last Raindrop of Spring", MM45.Spots.SpringDruid);
                case Quest.RidVertigoOfPests: return QuestBitDesc("Mayor Gunther", "Rid Vertigo of the plague of pests", MM45.Spots.Gunther);
                case Quest.FindWesternTowerKey: return QuestBitDesc("Dreyfus", "Find the key to the Western Tower", MM45.Spots.Dreyfus);
                case Quest.CollectRubies: return QuestBitDesc("Linus", "Collect rubies from the mines", MM45.Spots.Linus);
                case Quest.CollectEmeralds: return QuestBitDesc("Simon", "Collect emeralds from the mines", MM45.Spots.Simon);
                case Quest.CollectSapphires: return QuestBitDesc("Toby", "Collect Sapphires from the mines", MM45.Spots.Toby);
                case Quest.CollectDiamonds: return QuestBitDesc("Hector", "Collect Diamonds from the mines", MM45.Spots.Hector);
                case Quest.FindStatuettes: return QuestBitDesc("Luna", "Find the three golden statuettes", MM45.Spots.Luna);
                case Quest.EnchantBridle: return QuestBitDesc("Ambrose", "Enchant Ambrose's bridle", MM45.Spots.Ambrose);
                case Quest.DestroyOgres: return QuestBitDesc("Kramer", "Destroy the Ogres near Ogre Pass", MM45.Spots.Kramer);
                case Quest.FindVesparsHandle: return QuestBitDesc("Vespar", "Find Vespar's emerald handle", MM45.Spots.Vespar);
                case Quest.BringMelons: return QuestBitDesc("Nibbler", "Bring Monga Melons to Nibbler", MM45.Spots.Nibbler);
                case Quest.RescueSprite: return QuestBitDesc("Sharla", "Rescue Sheewana the Sprite from the Orcs", MM45.Spots.Sharla);
                case Quest.FindChalice: return QuestBitDesc("Bosco", "Find the Chalice of Protection", MM45.Spots.Bosco);
                case Quest.FindEctorsRing: return QuestBitDesc("Ector", "Find Ector's ring", MM45.Spots.Ector);
                case Quest.FindCalebsGlass: return QuestBitDesc("Caleb", "Find Caleb's magnifying glass", MM45.Spots.Caleb);
                case Quest.FindJewelOfAges: return QuestBitDesc("Thaddeus", "Find the Jewel of Ages", MM45.Spots.Thaddeus);
                case Quest.StopGettlewaith: return QuestBitDesc("Mayor Snarfblad", "Stop Gettlewaith the gremlin", MM45.Spots.Snarfblad);
                case Quest.ReleaseJethrosBrother: return QuestBitDesc("Jethro", "Release Jethro's brother from jail", MM45.Spots.Jethro);
                case Quest.FindNadiasNecklace: return QuestBitDesc("Nadia", "Find and return Nadia's necklace", MM45.Spots.Nadia);
                case Quest.EndXenocsReign: return QuestBitDesc("Astra", "End Xenoc and Morgana's reign of terror", MM45.Spots.Astra);
                case Quest.FindSandrosHeart: return QuestBitDesc("Sandro", "Find Sandro's heart", MM45.Spots.Sandro);
                case Quest.SaveKalindra: return QuestBitDesc("Dimitri", "Save Queen Kalindra", MM45.Spots.Kalindra);
                case Quest.TalkToAmbrose: return QuestBitDesc("Dimitri", "Talk to Ambrose, the Queens Knight", MM45.Spots.Dimitri);
                case Quest.FindSongbird: return QuestBitDesc("Megan", "Find the Songbird of Serenity", MM45.Spots.Megan);
                case Quest.FindEnergyDisks: return QuestBitDesc("Ellinger", "Find energy disks", MM45.Spots.Ellinger);
                case Quest.FindDragonEgg: return QuestBitDesc("Alister", "Find a Dragon Egg", MM45.Spots.Alister);
                default: return BitDesc.Empty;
            }
        }

        public static BitDesc MapFlagsDescription(object val)
        {
            switch ((int) val)
            {
                case 1: return new BitDesc("The Etherealize spell is forbidden on this map");
                case 15: return new BitDesc("The Town Portal spell is forbidden on this map");
                case 14: return new BitDesc("The Super Shelter spell is forbidden on this map");
                case 13: return new BitDesc("The Time Distortion spell is forbidden on this map");
                case 12: return new BitDesc("The Lloyd's Beacon spell is forbidden on this map");
                case 11: return new BitDesc("The Teleport spell is forbidden on this map");
                case 9: return new BitDesc("It is too dangerous to rest on this map");
                case 8: return new BitDesc("Saving the game is forbidden on this map");
                case 25: return new BitDesc("This map is dark");
                case 24: return new BitDesc("This is an outdoor map");
                default: return BitDesc.Empty;
            }
        }
    }

    public class MM45Quest : BasicQuest
    {
        public MM45Quest() { }

        public MM45Quest(BasicQuestType type, string name, string giver, string reward)
        {
            Init(GameNames.MightAndMagic45, type, name, giver, reward);
        }

        public MM45Quest(BasicQuestType type, string name, string giver)
        {
            Init(GameNames.MightAndMagic45, type, name, giver, String.Empty);
        }

        public MM45Quest(BasicQuestType type, string name)
        {
            Init(GameNames.MightAndMagic45, type, name, String.Empty, String.Empty);
        }

        public static string GetShortName(MM45Bits.Quest bit)
        {
            switch (bit)
            {
                case MM45Bits.Quest.SlayMadDwarfKing: return "Defeat the Clan King";
                case MM45Bits.Quest.GatherPhirnaRoot: return "Gather Phirna roots";
                case MM45Bits.Quest.FreeCelia: return "Free Celia from the zombies";
                case MM45Bits.Quest.RetrieveAlacorn: return "Retrieve the Alacorn of Falista";
                case MM45Bits.Quest.FindOrothinsWhistle: return "Retrieve Orothin's bone whistle";
                case MM45Bits.Quest.FindLigonosSkull: return "Retrieve Ligono's skull";
                case MM45Bits.Quest.GetBaroksPendant: return "Retrieve Barok's pendant";
                case MM45Bits.Quest.GetRoxannesTiara: return "Retrieve Princess Roxanne's tiara";
                case MM45Bits.Quest.ReturnScarab: return "Retrieve the Scarab of Imaging";
                case MM45Bits.Quest.ReturnCrystals: return "Retrieve the Crystals of Piezoelectricity";
                case MM45Bits.Quest.StealElixir: return "Retrieve the Elixir of Restoration";
                case MM45Bits.Quest.FindFaeryWand: return "Retrieve the Faery Wand";
                case MM45Bits.Quest.FindHolyBook: return "Retrieve the Holy Book of Elvenkind";
                case MM45Bits.Quest.DestroyTrollLair: return "Destroy the Lair of the Trolls";
                case MM45Bits.Quest.DestroyOgreLair: return "Destroy the Lair of the Ogres";
                case MM45Bits.Quest.ReclaimPagoda: return "Reclaim Kai Wu's Pagoda";
                case MM45Bits.Quest.SlayLakeMonsters: return "Slay the monsters of the lake";
                case MM45Bits.Quest.SaveWinterkill: return "Save Winterkill from it's curse";
                case MM45Bits.Quest.DestroyCyclopsLair: return "Destroy the lair of the Cyclops";
                case MM45Bits.Quest.FindEverhotRock: return "Retrieve the Everhot Lava Rock";
                case MM45Bits.Quest.RetrieveScrollOfInsight: return "Retrieve the Scroll of Insight";
                case MM45Bits.Quest.FreeCrodo: return "Free Crodo from the evil wizard Darzog";
                case MM45Bits.Quest.ClimbDarzogsTower: return "Defeat Lord Xeen";
                case MM45Bits.Quest.FindMirror: return "Find the 6th mirror";
                case MM45Bits.Quest.TakeLastFlower: return "Retrieve the Last Flower of Summer";
                case MM45Bits.Quest.TakeLastLeaf: return "Retrieve the Last Fallen leaf of Autumn";
                case MM45Bits.Quest.TakeLastSnowflake: return "Retrieve the Last Snowflake of Winter";
                case MM45Bits.Quest.TakeLastRaindrop: return "Retrieve the Last Raindrop of Spring";
                case MM45Bits.Quest.RidVertigoOfPests: return "Rid Vertigo of the plague of pests";
                case MM45Bits.Quest.FindWesternTowerKey: return "Retrieve the key to the Western Tower";
                case MM45Bits.Quest.CollectRubies: return "Collect rubies";
                case MM45Bits.Quest.CollectEmeralds: return "Collect emeralds";
                case MM45Bits.Quest.CollectSapphires: return "Collect sapphires";
                case MM45Bits.Quest.CollectDiamonds: return "Collect diamonds";
                case MM45Bits.Quest.FindStatuettes: return "Retrieve the three golden statuettes";
                case MM45Bits.Quest.EnchantBridle: return "Enchant Ambrose's bridle";
                case MM45Bits.Quest.DestroyOgres: return "Destroy the Ogres near Ogre Pass";
                case MM45Bits.Quest.FindVesparsHandle: return "Retrieve Vespar's emerald handle";
                case MM45Bits.Quest.BringMelons: return "Find Monga Melons";
                case MM45Bits.Quest.RescueSprite: return "Rescue Sheewana the Sprite";
                case MM45Bits.Quest.FindChalice: return "Retrieve the Chalice of Protection";
                case MM45Bits.Quest.FindEctorsRing: return "Retrieve Ector's ring";
                case MM45Bits.Quest.FindCalebsGlass: return "Retrieve Caleb's magnifying glass";
                case MM45Bits.Quest.FindJewelOfAges: return "Retrieve the Jewel of Ages";
                case MM45Bits.Quest.StopGettlewaith: return "Stop Gettlewaith the gremlin";
                case MM45Bits.Quest.ReleaseJethrosBrother: return "Release Jethro's brother from jail";
                case MM45Bits.Quest.FindNadiasNecklace: return "Retrieve Nadia's onyx necklace";
                case MM45Bits.Quest.EndXenocsReign: return "Defeat Xenoc and Morgana";
                case MM45Bits.Quest.FindSandrosHeart: return "Retrieve Sandro's heart";
                case MM45Bits.Quest.SaveKalindra: return "Save Queen Kalindra";
                case MM45Bits.Quest.TalkToAmbrose: return "Talk to Ambrose in Griffin Pass";
                case MM45Bits.Quest.FindSongbird: return "Retrieve the Songbird of Serenity";
                case MM45Bits.Quest.FindEnergyDisks: return "Find energy disks";
                case MM45Bits.Quest.FindDragonEgg: return "Find a Dragon Egg";
                default: return String.Format("Unknown Quest #{0}", (int) bit);
            }
        }

        public static string GetGiver(MM45Bits.Quest bit)
        {
            switch(bit)
            {
                case MM45Bits.Quest.SlayMadDwarfKing: return "Mayor Gunther";
                case MM45Bits.Quest.GatherPhirnaRoot: return "Myra the Herbalist";
                case MM45Bits.Quest.FreeCelia: return "Derek";
                case MM45Bits.Quest.RetrieveAlacorn: return "Valia";
                case MM45Bits.Quest.FindOrothinsWhistle: return "Orothin";
                case MM45Bits.Quest.FindLigonosSkull: return "Ligono";
                case MM45Bits.Quest.GetBaroksPendant: return "Barok";
                case MM45Bits.Quest.GetRoxannesTiara: return "Princess Roxanne";
                case MM45Bits.Quest.ReturnScarab: return "Carlawna";
                case MM45Bits.Quest.ReturnCrystals: return "Falagar";
                case MM45Bits.Quest.StealElixir: return "Mirabeth";
                case MM45Bits.Quest.FindFaeryWand: return "Danulf";
                case MM45Bits.Quest.FindHolyBook: return "Tito";
                case MM45Bits.Quest.DestroyTrollLair: return "Thickbark";
                case MM45Bits.Quest.DestroyOgreLair: return "Captain Nystor";
                case MM45Bits.Quest.ReclaimPagoda: return "Kai Wu";
                case MM45Bits.Quest.SlayLakeMonsters: return "Medin";
                case MM45Bits.Quest.SaveWinterkill: return "Randon";
                case MM45Bits.Quest.DestroyCyclopsLair: return "Glom";
                case MM45Bits.Quest.FindEverhotRock: return "Halon";
                case MM45Bits.Quest.RetrieveScrollOfInsight: return "Arie";
                case MM45Bits.Quest.FreeCrodo: return "Artemus";
                case MM45Bits.Quest.ClimbDarzogsTower: return "Crodo";
                case MM45Bits.Quest.FindMirror: return "King Burlock";
                case MM45Bits.Quest.TakeLastFlower: return "the Summer Druid";
                case MM45Bits.Quest.TakeLastLeaf: return "the Autumn Druid";
                case MM45Bits.Quest.TakeLastSnowflake: return "the Winter Druid";
                case MM45Bits.Quest.TakeLastRaindrop: return "the Spring Druid";
                case MM45Bits.Quest.RidVertigoOfPests: return "Mayor Gunther";
                case MM45Bits.Quest.FindWesternTowerKey: return "Dreyfus";
                case MM45Bits.Quest.CollectRubies: return "Linus";
                case MM45Bits.Quest.CollectEmeralds: return "Simon";
                case MM45Bits.Quest.CollectSapphires: return "Toby";
                case MM45Bits.Quest.CollectDiamonds: return "Hector";
                case MM45Bits.Quest.FindStatuettes: return "Luna";
                case MM45Bits.Quest.EnchantBridle: return "Ambrose";
                case MM45Bits.Quest.DestroyOgres: return "Kramer";
                case MM45Bits.Quest.FindVesparsHandle: return "Vespar";
                case MM45Bits.Quest.BringMelons: return "Nibbler";
                case MM45Bits.Quest.RescueSprite: return "Sharla";
                case MM45Bits.Quest.FindChalice: return "Bosco";
                case MM45Bits.Quest.FindEctorsRing: return "Ector";
                case MM45Bits.Quest.FindCalebsGlass: return "Caleb";
                case MM45Bits.Quest.FindJewelOfAges: return "Thaddeus";
                case MM45Bits.Quest.StopGettlewaith: return "Mayor Snarfblad";
                case MM45Bits.Quest.ReleaseJethrosBrother: return "Jethro";
                case MM45Bits.Quest.FindNadiasNecklace: return "Nadia";
                case MM45Bits.Quest.EndXenocsReign: return "Astra";
                case MM45Bits.Quest.FindSandrosHeart: return "Sandro";
                case MM45Bits.Quest.SaveKalindra: return "Dimitri";
                case MM45Bits.Quest.TalkToAmbrose: return "Dimitri";
                case MM45Bits.Quest.FindSongbird: return "Megan";
                case MM45Bits.Quest.FindEnergyDisks: return "Ellinger";
                case MM45Bits.Quest.FindDragonEgg: return "Alister";
                default: return String.Format("Unknown Quest #{0}", (int)bit);
            }
        }
    }

    public class MiningQuestStatus : QuestStatus
    {
        public MapXY[] Mine1;
        public MapXY[] Mine2;
        public MapXY[] Mine3;

        public MiningQuestStatus(string strGems, MapXY[] mine1, MapXY[] mine2, MapXY[] mine3)
            : base(Basic.NotStarted, String.Format("Mine for {0}", strGems))
        {
            m_strVerb = "Mine";
            

            foreach (MapXY map in mine1)
                MainObjectives.Add(new QuestLocation("Mine the vein", map));
            foreach (MapXY map in mine2)
            {
                MainObjectives.Add(new QuestLocation("Mine the vein", map));
                MainObjectives.Add(new QuestLocation("Mine the vein again", map));
            }
            foreach (MapXY map in mine3)
            {
                MainObjectives.Add(new QuestLocation("Mine the vein", map));
                MainObjectives.Add(new QuestLocation("Mine the vein again", map));
                MainObjectives.Add(new QuestLocation("Mine the vein a third time", map));
            }

            ReturnToGiver = false;

            Postrequisites.Add(new QuestLocation("Pay the God of Minerals 250,000 Gold", MM45.Spots.GodOfMinerals));
        }

        public void AddPrePost(QuestGoal quest)
        {
            AddPre(quest);
            AddPost(QuestGoal.Incomplete);  // quest is repeatable, so the last step is never really "complete"
        }

        public void AddSingles(MM45QuestInfo.Bits bits, params Dark[] darkBits)
        {
            foreach (Dark bit in darkBits)
                AddObj(bits.Dark(bit));
        }

        public void AddDoubles(MM45QuestInfo.Bits bits, params Dark[] darkBits)
        {
            for (int i = 0; i < darkBits.Length; i += 2)
            {
                AddObj(Or(bits.Dark(darkBits[i]), bits.Dark(darkBits[i + 1])));
                AddObj(bits.Dark(darkBits[i + 1]));
            }
        }

        public void AddTriples(MM45QuestInfo.Bits bits, params Dark[] darkBits)
        {
            for (int i = 0; i < darkBits.Length; i += 3)
            {
                AddObj(bits.Dark(darkBits[i]));
                AddObj(Or(bits.Dark(darkBits[i + 1]), bits.Dark(darkBits[i + 2])));
                AddObj(bits.Dark(darkBits[i + 2]));
            }
        }
    }

    public class MM45QuestInfo : QuestInfo
    {
        public override bool NeedsFiles { get { return true; } }

        // Official quests (with quest bits)
        public DefeatQuestStatus DefeatDwarfKing = new DefeatQuestStatus("Slay the Dwarf King for Mayor Gunther");
        public RetrieveQuestStatus GatherPhirnaRoots = new RetrieveQuestStatus("Deliver Phirna Roots to Myra the Herbalist");
        public LiberateQuestStatus FreeCelia = new LiberateQuestStatus("Free Celia from the Zombies");
        public RetrieveQuestStatus RetrieveAlacorn = new RetrieveQuestStatus("Return the Alacorn of Falista");
        public RetrieveQuestStatus FindOrothinsWhistle = new RetrieveQuestStatus("Return Orothin's bone whistle");
        public RetrieveQuestStatus FindLigonosSkull = new RetrieveQuestStatus("Return Ligono's skull");
        public RetrieveQuestStatus GetBaroksPendant = new RetrieveQuestStatus("Return Barok's pendant");
        public RetrieveQuestStatus GetRoxannesTiara = new RetrieveQuestStatus("Return Princess Roxanne's tiara");
        public RetrieveQuestStatus ReturnScarab = new RetrieveQuestStatus("Return the Scarab of Imaging");
        public RetrieveQuestStatus ReturnCrystals = new RetrieveQuestStatus("Return the Crystals of Piezoelectricity");
        public RetrieveQuestStatus StealElixir = new RetrieveQuestStatus("Deliver the Elixir of Restoration");
        public RetrieveQuestStatus FindFaeryWand = new RetrieveQuestStatus("Return the Faery Wand");
        public RetrieveQuestStatus FindHolyBook = new RetrieveQuestStatus("Return the Holy Book of Elvenkind");
        public DestroyQuestStatus DestroyTrollLair = new DestroyQuestStatus("Destroy the Lair of the Trolls");
        public DestroyQuestStatus DestroyOgreLair = new DestroyQuestStatus("Destroy the Lair of the Ogres");
        public LocateQuestStatus ReclaimPagoda = new LocateQuestStatus("Reclaim Kai Wu's Pagoda");
        public DestroyQuestStatus SlayLakeMonsters = new DestroyQuestStatus("Slay the monsters of the lake");
        public DestroyQuestStatus SaveWinterkill = new DestroyQuestStatus("Destroy the undead monsters of Winterkill");
        public DestroyQuestStatus DestroyCyclopsLair = new DestroyQuestStatus("Destroy the lair of the Cyclops");
        public RetrieveQuestStatus FindEverhotRock = new RetrieveQuestStatus("Deliver the Everhot Lava Rock");
        public RetrieveQuestStatus RetrieveScrollOfInsight = new RetrieveQuestStatus("Deliver the Scroll of Insight");
        public LiberateQuestStatus FreeCrodo = new LiberateQuestStatus("Free Crodo from Darzog");
        public RetrieveQuestStatus FindXeenSlayer = new RetrieveQuestStatus("Find the Xeen Slayer Sword");
        public DefeatQuestStatus SlayLordXeen = new DefeatQuestStatus("Slay Lord Xeen");
        public LocateQuestStatus FindMirror = new LocateQuestStatus("Locate the 6th mirror");
        public QuestStatus TurnSeasons = new QuestStatus(QuestStatus.Basic.NotStarted, "Turn the seasons");
        public LocateQuestStatus RidVertigoOfPests = new LocateQuestStatus("Expose Joe's treachery");
        public RetrieveQuestStatus FindWesternTowerKey = new RetrieveQuestStatus("Find the key to the Western Tower");
        public MiningQuestStatus CollectRubies = new MiningQuestStatus("Rubies", MM45.Spots.RubyRocks1, MM45.Spots.RubyRocks2, MM45.Spots.RubyRocks3);
        public MiningQuestStatus CollectEmeralds = new MiningQuestStatus("Emeralds", MM45.Spots.EmeraldRocks1, MM45.Spots.EmeraldRocks2, MM45.Spots.EmeraldRocks3);
        public MiningQuestStatus CollectSapphires = new MiningQuestStatus("Sapphires", MM45.Spots.SapphireRocks1, MM45.Spots.SapphireRocks2, MM45.Spots.SapphireRocks3);
        public MiningQuestStatus CollectDiamonds = new MiningQuestStatus("Diamonds", MM45.Spots.DiamondRocks1, MM45.Spots.DiamondRocks2, MM45.Spots.DiamondRocks3);
        public RetrieveQuestStatus FindStatuettes = new RetrieveQuestStatus("Return the three golden statuettes");
        public QuestStatus EnterBlackfang = new TalkQuestStatus("Find a way into Castle Blackfang");
        public DestroyQuestStatus DestroyOgres = new DestroyQuestStatus("Destroy the Ogre Forts near Ogre Pass");
        public RetrieveQuestStatus FindVesparsHandle = new RetrieveQuestStatus("Return Vespar's emerald handle");
        public RetrieveQuestStatus BringMelons = new RetrieveQuestStatus("Bring Monga Melons to Nibbler");
        public LiberateQuestStatus RescueSprite = new LiberateQuestStatus("Rescue Sheewana the Sprite from the Orcs");
        public RetrieveQuestStatus FindChalice = new RetrieveQuestStatus("Deliver the Chalice of Protection");
        public RetrieveQuestStatus FindEctorsRing = new RetrieveQuestStatus("Return Ector's ring");
        public RetrieveQuestStatus FindCalebsGlass = new RetrieveQuestStatus("Return Caleb's magnifying glass");
        public RetrieveQuestStatus FindJewelOfAges = new RetrieveQuestStatus("Return the Jewel of Ages");
        public DefeatQuestStatus StopGettlewaith = new DefeatQuestStatus("Stop Gettlewaith the gremlin");
        public LiberateQuestStatus ReleaseJethrosBrother = new LiberateQuestStatus("Release Jethro's brother from jail");
        public RetrieveQuestStatus FindNadiasNecklace = new RetrieveQuestStatus("Return Nadia's necklace");
        public DefeatQuestStatus EndXenocsReign = new DefeatQuestStatus("Defeat Xenoc and Morgana");
        public RetrieveQuestStatus FindSandrosHeart = new RetrieveQuestStatus("Return Sandro's heart");
        public LiberateQuestStatus SaveKalindra = new LiberateQuestStatus("Save Queen Kalindra");
        public TalkQuestStatus TalkToAmbrose = new TalkQuestStatus("Talk to Ambrose");
        public RetrieveQuestStatus FindSongbird = new RetrieveQuestStatus("Return the Songbird of Serenity");
        public RetrieveQuestStatus FindEnergyDisks = new RetrieveQuestStatus("Deliver energy disks");
        public RetrieveQuestStatus FindDragonEgg = new RetrieveQuestStatus("Deliver a Dragon Egg");

        // Unofficial quests (no quest bit)
        public QuestStatus DefeatAlamar = new QuestStatus(QuestStatus.Basic.Accepted, "Defeat Lord Alamar");
        public QuestStatus UniteWorlds = new QuestStatus(QuestStatus.Basic.Accepted, "Unite the two sides of Xeen");
        public QuestStatus MineGold = new QuestStatus(QuestStatus.Basic.NotStarted, "Mine the gold veins");
        public QuestStatus DestroyThings = new QuestStatus(QuestStatus.Basic.NotStarted, "Destroy, Pillage, Burn!");
        public QuestStatus XeenDoll = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Lord Xeen Cupie Doll");
        public QuestStatus IllusionKey = new QuestStatus(QuestStatus.Basic.Accepted, "Obtain the key to the Tower of High Magic");
        public QuestStatus NightshadowWell = new QuestStatus(QuestStatus.Basic.NotStarted, "Remove the curse from the Nightshadow well");
        public QuestStatus AspWell = new QuestStatus(QuestStatus.Basic.NotStarted, "Remove the curse from the Asp well");
        public QuestStatus AlamarClock = new QuestStatus(QuestStatus.Basic.Accepted, "Solve the Alamar Castle clock room puzzle");
        public QuestStatus BarkmanSkulls = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the treasures of Barkman");
        public QuestStatus PyramidTorches = new QuestStatus(QuestStatus.Basic.Accepted, "Light the torches in the Great Pyramid");
        public QuestStatus PyramidNumbers = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the Great Pyramid number puzzle");
        public QuestStatus LostSouls = new QuestStatus(QuestStatus.Basic.Accepted, "Solve the puzzles in the Lost Souls Dungeon");
        public QuestStatus TreasureTrees = new QuestStatus(QuestStatus.Basic.NotStarted, "Collect treasures from the trees");

        // Puzzles (no quest bit)
        public QuestStatus Pitchfork = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the first Castleview chest puzzle");
        public QuestStatus NumberChests = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the second Castleview chest puzzle");
        public QuestStatus GolemDungeon = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the puzzles of the Golem Dungeon");
        public QuestStatus Crossword = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the crossword puzzle");
        public QuestStatus TreasureTeleport = new QuestStatus(QuestStatus.Basic.NotStarted, "Solve the treasure teleport maze");

        // Stats
        public QuestStatus FireRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Fire Resistance");
        public QuestStatus ElecRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Electrical Resistance");
        public QuestStatus ColdRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Cold Resistance");
        public QuestStatus PoisonRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Poison Resistance");
        public QuestStatus EnergyRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Energy Resistance");
        public QuestStatus MagicRes = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Magic Resistance");
        public QuestStatus Might = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Might");
        public QuestStatus Intellect = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Intellect");
        public QuestStatus Personality = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Personality");
        public QuestStatus Endurance = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Endurance");
        public QuestStatus Speed = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Speed");
        public QuestStatus Accuracy = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Accuracy");
        public QuestStatus Luck = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Luck");
        public QuestStatus Level = new QuestStatus(QuestStatus.Basic.Accepted, "Raise your Level");
        public QuestStatus MultiStat = new QuestStatus(QuestStatus.Basic.Accepted, "Raise multiple statistics");
        public QuestStatus TempStats = new QuestStatus(QuestStatus.Basic.Accepted, "Find temporary statistic bonuses");

        public QuestStatus Guilds = new QuestStatus(QuestStatus.Basic.NotStarted, "Join the Guilds");
        public QuestStatus Thief = new QuestStatus(QuestStatus.Basic.NotStarted, "Become a Convicted Thief");
        public QuestStatus Bark = new QuestStatus(QuestStatus.Basic.NotStarted, "Become a Disciple of Bark");
        public QuestStatus Drawkcab = new QuestStatus(QuestStatus.Basic.NotStarted, "Become a Drawkcab Extraordinaire");
        public QuestStatus RatQueen = new QuestStatus(QuestStatus.Basic.NotStarted, "Exterminate the Rat Queen");
        public QuestStatus Paladin = new QuestStatus(QuestStatus.Basic.NotStarted, "Become the Paladin's Friend");
        public QuestStatus Awards = new QuestStatus(QuestStatus.Basic.Accepted, "Receive various awards");
        public QuestStatus Experience = new QuestStatus(QuestStatus.Basic.NotStarted, "Read the Experience Books");
        public QuestStatus XeenTraps = new QuestStatus(QuestStatus.Basic.NotStarted, "Disable the traps in Xeen's Castle");
        public QuestStatus Lamps = new QuestStatus(QuestStatus.Basic.NotStarted, "Release the genies from the lamps");
        public QuestStatus Level7Items = new QuestStatus(QuestStatus.Basic.NotStarted, "Find treasure containing level 7 items");

        // Spells
        public QuestStatus SpellClericAcidSpray = new QuestStatus();
        public QuestStatus SpellClericAwaken = new QuestStatus();
        public QuestStatus SpellClericBeastMaster = new QuestStatus();
        public QuestStatus SpellClericBless = new QuestStatus();
        public QuestStatus SpellClericColdRay = new QuestStatus();
        public QuestStatus SpellClericCreateFood = new QuestStatus();
        public QuestStatus SpellClericCureDisease = new QuestStatus();
        public QuestStatus SpellClericCureParalysis = new QuestStatus();
        public QuestStatus SpellClericCurePoison = new QuestStatus();
        public QuestStatus SpellClericCureWounds = new QuestStatus();
        public QuestStatus SpellClericDayOfProtection = new QuestStatus();
        public QuestStatus SpellClericDeadlySwarm = new QuestStatus();
        public QuestStatus SpellClericDivineIntervention = new QuestStatus();
        public QuestStatus SpellClericFieryFlail = new QuestStatus();
        public QuestStatus SpellClericFirstAid = new QuestStatus();
        public QuestStatus SpellClericFlyingFist = new QuestStatus();
        public QuestStatus SpellClericFrostBite = new QuestStatus();
        public QuestStatus SpellClericHeroism = new QuestStatus();
        public QuestStatus SpellClericHolyBonus = new QuestStatus();
        public QuestStatus SpellClericHolyWord = new QuestStatus();
        public QuestStatus SpellClericHypnotize = new QuestStatus();
        public QuestStatus SpellClericLight = new QuestStatus();
        public QuestStatus SpellClericMassDistortion = new QuestStatus();
        public QuestStatus SpellClericMoonRay = new QuestStatus();
        public QuestStatus SpellClericNaturesCure = new QuestStatus();
        public QuestStatus SpellClericPain = new QuestStatus();
        public QuestStatus SpellClericPowerCure = new QuestStatus();
        public QuestStatus SpellClericProtFromElements = new QuestStatus();
        public QuestStatus SpellClericRaiseDead = new QuestStatus();
        public QuestStatus SpellClericResurrect = new QuestStatus();
        public QuestStatus SpellClericRevitalize = new QuestStatus();
        public QuestStatus SpellClericSparks = new QuestStatus();
        public QuestStatus SpellClericStoneToFlesh = new QuestStatus();
        public QuestStatus SpellClericSunRay = new QuestStatus();
        public QuestStatus SpellClericSuppressDisease = new QuestStatus();
        public QuestStatus SpellClericSuppressPoison = new QuestStatus();
        public QuestStatus SpellClericTownPortal = new QuestStatus();
        public QuestStatus SpellClericTurnUndead = new QuestStatus();
        public QuestStatus SpellClericWalkOnWater = new QuestStatus();

        public QuestStatus SpellArcaneAwaken = new QuestStatus();
        public QuestStatus SpellArcaneClairvoyance = new QuestStatus();
        public QuestStatus SpellArcaneDancingSword = new QuestStatus();
        public QuestStatus SpellArcaneDayOfSorcery = new QuestStatus();
        public QuestStatus SpellArcaneDetectMonster = new QuestStatus();
        public QuestStatus SpellArcaneDragonSleep = new QuestStatus();
        public QuestStatus SpellArcaneElementalStorm = new QuestStatus();
        public QuestStatus SpellArcaneEnchantItem = new QuestStatus();
        public QuestStatus SpellArcaneEnergyBlast = new QuestStatus();
        public QuestStatus SpellArcaneEtherealize = new QuestStatus();
        public QuestStatus SpellArcaneFantasticFreeze = new QuestStatus();
        public QuestStatus SpellArcaneFingerOfDeath = new QuestStatus();
        public QuestStatus SpellArcaneFireball = new QuestStatus();
        public QuestStatus SpellArcaneGolemStopper = new QuestStatus();
        public QuestStatus SpellArcaneIdentifyMonster = new QuestStatus();
        public QuestStatus SpellArcaneImplosion = new QuestStatus();
        public QuestStatus SpellArcaneIncinerate = new QuestStatus();
        public QuestStatus SpellArcaneInferno = new QuestStatus();
        public QuestStatus SpellArcaneInsectSpray = new QuestStatus();
        public QuestStatus SpellArcaneItemToGold = new QuestStatus();
        public QuestStatus SpellArcaneJump = new QuestStatus();
        public QuestStatus SpellArcaneLevitate = new QuestStatus();
        public QuestStatus SpellArcaneLight = new QuestStatus();
        public QuestStatus SpellArcaneLightningBolt = new QuestStatus();
        public QuestStatus SpellArcaneLloydsBeacon = new QuestStatus();
        public QuestStatus SpellArcaneMagicArrow = new QuestStatus();
        public QuestStatus SpellArcaneMegaVolts = new QuestStatus();
        public QuestStatus SpellArcanePoisonVolley = new QuestStatus();
        public QuestStatus SpellArcanePowerShield = new QuestStatus();
        public QuestStatus SpellArcanePrismaticLight = new QuestStatus();
        public QuestStatus SpellArcaneRechargeItem = new QuestStatus();
        public QuestStatus SpellArcaneShrapmetal = new QuestStatus();
        public QuestStatus SpellArcaneSleep = new QuestStatus();
        public QuestStatus SpellArcaneStarBurst = new QuestStatus();
        public QuestStatus SpellArcaneSuperShelter = new QuestStatus();
        public QuestStatus SpellArcaneTeleport = new QuestStatus();
        public QuestStatus SpellArcaneTimeDistortion = new QuestStatus();
        public QuestStatus SpellArcaneToxicCloud = new QuestStatus();
        public QuestStatus SpellArcaneWizardEye = new QuestStatus();

        // Skills
        public QuestStatus SkillArmsMaster = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Arms Master\" skill");
        public QuestStatus SkillAstrologer = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Astrologer\" skill");
        public QuestStatus SkillBodyBuilder = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Body Builder\" skill");
        public QuestStatus SkillCartographer = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Cartographer\" skill");
        public QuestStatus SkillCrusader = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Crusader\" skill");
        public QuestStatus SkillDirectionSense = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Direction Sense\" skill");
        public QuestStatus SkillLinguist = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Linguist\" skill");
        public QuestStatus SkillMerchant = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Merchant\" skill");
        public QuestStatus SkillMountaineer = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Mountaineer\" skill");
        public QuestStatus SkillNavigator = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Navigator\" skill");
        public QuestStatus SkillPathFinder = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Path Finder\" skill");
        public QuestStatus SkillPrayerMaster = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Prayer Master\" skill");
        public QuestStatus SkillPrestidigitator = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Prestidigitator\" skill");
        public QuestStatus SkillSwimmer = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Swimmer\" skill");
        public QuestStatus SkillSpotSecretDoors = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Spot Secret Door\" skill");
        public QuestStatus SkillDangerSense = new QuestStatus(QuestStatus.Basic.Accepted, "Learn the \"Danger Sense\" skill");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { DefeatDwarfKing, GatherPhirnaRoots, FreeCelia, RetrieveAlacorn, FindOrothinsWhistle, FindLigonosSkull, GetBaroksPendant,
                GetRoxannesTiara, ReturnScarab, ReturnCrystals, StealElixir, FindFaeryWand, FindHolyBook, DestroyTrollLair, DestroyOgreLair, ReclaimPagoda,
                SlayLakeMonsters, SaveWinterkill, DestroyCyclopsLair, FindEverhotRock, RetrieveScrollOfInsight, FreeCrodo, FindXeenSlayer, SlayLordXeen, FindMirror,
                TurnSeasons, RidVertigoOfPests, FindWesternTowerKey, CollectRubies, CollectEmeralds, CollectSapphires, CollectDiamonds, FindStatuettes, EnterBlackfang,
                DestroyOgres, FindVesparsHandle, BringMelons, RescueSprite, FindChalice, FindEctorsRing, FindCalebsGlass, FindJewelOfAges, StopGettlewaith,
                ReleaseJethrosBrother, FindNadiasNecklace, EndXenocsReign, FindSandrosHeart, SaveKalindra, TalkToAmbrose, FindSongbird, FindEnergyDisks, FindDragonEgg,
                DefeatAlamar, UniteWorlds, MineGold, DestroyThings, XeenDoll, IllusionKey, NightshadowWell, AspWell, AlamarClock, BarkmanSkulls, PyramidTorches,
                PyramidNumbers, LostSouls, TreasureTrees, Pitchfork, NumberChests, GolemDungeon, Crossword, TreasureTeleport, FireRes, ElecRes, ColdRes, PoisonRes,
                EnergyRes, MagicRes, Might, Intellect, Personality, Endurance, Speed, Accuracy, Luck, Level, MultiStat, TempStats, Guilds, Thief, Bark, Drawkcab,
                RatQueen, Paladin, Awards, Experience, XeenTraps, Lamps, Level7Items, SpellClericAcidSpray, SpellClericAwaken, SpellClericBeastMaster, SpellClericBless,
                SpellClericColdRay, SpellClericCreateFood, SpellClericCureDisease, SpellClericCureParalysis, SpellClericCurePoison, SpellClericCureWounds,
                SpellClericDayOfProtection, SpellClericDeadlySwarm, SpellClericDivineIntervention, SpellClericFieryFlail, SpellClericFirstAid, SpellClericFlyingFist,
                SpellClericFrostBite, SpellClericHeroism, SpellClericHolyBonus, SpellClericHolyWord, SpellClericHypnotize, SpellClericLight, SpellClericMassDistortion,
                SpellClericMoonRay, SpellClericNaturesCure, SpellClericPain, SpellClericPowerCure, SpellClericProtFromElements, SpellClericRaiseDead,
                SpellClericResurrect, SpellClericRevitalize, SpellClericSparks, SpellClericStoneToFlesh, SpellClericSunRay, SpellClericSuppressDisease, 
                SpellClericSuppressPoison, SpellClericTownPortal, SpellClericTurnUndead, SpellClericWalkOnWater, SpellArcaneAwaken, SpellArcaneClairvoyance,
                SpellArcaneDancingSword, SpellArcaneDayOfSorcery, SpellArcaneDetectMonster, SpellArcaneDragonSleep, SpellArcaneElementalStorm, SpellArcaneEnchantItem,
                SpellArcaneEnergyBlast, SpellArcaneEtherealize, SpellArcaneFantasticFreeze, SpellArcaneFingerOfDeath, SpellArcaneFireball, SpellArcaneGolemStopper,
                SpellArcaneIdentifyMonster, SpellArcaneImplosion, SpellArcaneIncinerate, SpellArcaneInferno, SpellArcaneInsectSpray, SpellArcaneItemToGold,
                SpellArcaneJump, SpellArcaneLevitate, SpellArcaneLight, SpellArcaneLightningBolt, SpellArcaneLloydsBeacon, SpellArcaneMagicArrow, SpellArcaneMegaVolts,
                SpellArcanePoisonVolley, SpellArcanePowerShield, SpellArcanePrismaticLight, SpellArcaneRechargeItem, SpellArcaneShrapmetal, SpellArcaneSleep,
                SpellArcaneStarBurst, SpellArcaneSuperShelter, SpellArcaneTeleport, SpellArcaneTimeDistortion, SpellArcaneToxicCloud, SpellArcaneWizardEye,
                SkillArmsMaster, SkillAstrologer, SkillBodyBuilder, SkillCartographer, SkillCrusader, SkillDirectionSense, SkillLinguist, SkillMerchant,
                SkillMountaineer, SkillNavigator, SkillPathFinder, SkillPrayerMaster, SkillPrestidigitator, SkillSwimmer, SkillSpotSecretDoors, SkillDangerSense };
        }

        private QuestStatus AddSpellQuest(QuestTotals totals, MM45Character mm45Char, MM45SpellIndex spell)
        {
            MM45Spell mm45Spell = MM45.Spells[spell];
            bool bKnown = mm45Char.Spells.IsKnown((int) mm45Spell.InternalIndex, mm45Char.BasicClass);
            QuestStatus status = GetQuestState(true, bKnown, mm45Char.CanCast(mm45Spell.InternalIndex));
            if (status.InvalidClass)
                return status;
            totals.All++;
            if (bKnown)
                totals.Completed++;
            return status;
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<MM45Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        private MM45Quest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, string path, params QuestLocation[] locations)
        {
            if (locations == null || locations.Length < 1)
                return null;

            MM45Quest quest = new MM45Quest(type, name, giver, reward);
            quest.Bits = new QuestBits(bits);
            quest.Status = status;

            bool bPrimarySet = false;
            foreach (QuestLocation location in locations)
            {
                if (!bPrimarySet)
                {
                    quest.Primary = location;
                    bPrimarySet = true;
                    continue;
                }
                quest.Secondary.Add(location);
            }

            if (String.IsNullOrWhiteSpace(path))
                quest.Path = GetPath(type);
            else
                quest.Path = path;

            return quest;
        }

        public static MapXY GuildLocation(MM45Guild guild)
        {
            switch (guild)
            {
                case MM45Guild.Vertigo: return new MapXY(MM4Map.F3Vertigo, 20, 13);
                case MM45Guild.Nightshadow: return new MapXY(MM4Map.D4Nightshadow, 12, 4);
                case MM45Guild.Rivercity: return new MapXY(MM4Map.C3Rivercity, 6, 30);
                case MM45Guild.Asp: return new MapXY(MM4Map.C2Asp, 2, 1);
                case MM45Guild.Winterkill: return new MapXY(MM4Map.A3Winterkill, 7, 1);
                case MM45Guild.Castleview: return new MapXY(MM5Map.A4Castleview, 3, 27);
                case MM45Guild.Sandcaster: return new MapXY(MM5Map.E3Sandcaster, 18, 22);
                case MM45Guild.Lakeside: return new MapXY(MM5Map.F2Lakeside, 1, 9);
                case MM45Guild.Necropolis: return new MapXY(MM5Map.B2Necropolis, 5, 1);
                case MM45Guild.Olympus: return new MapXY(MM5Map.C2Olympus, 7, 9);
                case MM45Guild.ShangriLa: return new MapXY(MM4Map.E1ShangriLa, 0, 7);
                default: return null;
            }
        }

        public static string GuildName(MM45Guild guild) { return GuildMap(guild) + " Guild"; }

        public static string GuildMap(MM45Guild guild)
        {
            switch (guild)
            {
                case MM45Guild.Vertigo: return "Vertigo";
                case MM45Guild.Nightshadow: return "Nightshadow";
                case MM45Guild.Rivercity: return "Rivercity";
                case MM45Guild.Asp: return "Asp";
                case MM45Guild.Winterkill: return "Winterkill";
                case MM45Guild.Castleview: return "Castleview";
                case MM45Guild.Sandcaster: return "Sandcaster";
                case MM45Guild.Lakeside: return "Lakeside";
                case MM45Guild.Necropolis: return "Necropolis";
                case MM45Guild.Olympus: return "Olympus";
                case MM45Guild.ShangriLa: return "Shangri-La";
                default: return null;
            }
        }

        private MM45Quest GetSpellQuest(QuestStatus status, MM45SpellIndex index, params QuestLocation[] locations)
        {
            MM45Spell spell = MM45.Spells[index];
            string strTask = String.Format("Learn \"{0}\"", spell.Name);
            string strReward = String.Format("{0} Spell \"{1}\"", MM45Spell.TypeString(spell.Type), spell.Name);
            List<QuestLocation> newLocations = new List<QuestLocation>(locations);
            if (locations.Length == 0)
                newLocations.Add(new QuestLocation("Learn the spell from a guild", MM4Map.None, -1, -1));
            else
                newLocations.Insert(0, new QuestLocation("Learn the spell from a scroll or a guild", MM4Map.None, -1, -1));

            string strPurchase = String.Format("Purchase spell for {0} Gold at the ", MM45SpellList.GetSpellPurchasePrice(spell.InternalIndex, CharClass));
            foreach (MM45Guild guild in MM45SpellList.GetSaleLocations(index))
            {
                MapXY map = GuildLocation(guild);
                newLocations.Add(new QuestLocation(strPurchase + GuildName(guild), map));
            }

            return GetQuest(status, BasicQuestType.Side, index, strTask, String.Empty, strReward, "Spells", newLocations.ToArray());
        }

        public BasicQuest AddSpellQuest(QuestStatus status, List<MM45Quest> quests, MM45SpellIndex index, params QuestLocation[] locations)
        {
            MM45Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetSpellQuest(status, index, locations);
                quests.Add(quest);
            }
            return quest;
        }

        public BasicQuest AddSkillQuest(QuestStatus status, List<MM45Quest> quests, string str1, string str2, string strCost1, string strCost2, MapXY map1, MapXY map2)
        {
            status.Postrequisites.Add(new QuestLocation(String.Format(
                "Learn from {0}{1}", str1, String.IsNullOrWhiteSpace(strCost1) ? "" : String.Format(" ({0})", strCost1)), map1));
            if (!String.IsNullOrWhiteSpace(str2))
                status.Postrequisites.Add(new QuestLocation(String.Format(
                    "Learn from {0}{1}", str2, String.IsNullOrWhiteSpace(strCost2) ? "" : String.Format(" ({0})", strCost2)), map2));
            status.Postrequisites.Add(new QuestLocation("Learn from Jack Alltrades (100000 Gold)", MM45.Spots.AllSkills));
            BasicQuest quest = AddSideQuest(status, quests, String.Empty);
            quest.Path = "Secondary Skills";
            return quest;
        }

        public MM45Quest GetQuest(QuestStatus status, BasicQuestType type, MM45Bits.Quest bit, string strTarget, MapXY[] targets, string strReward)
        {
            status.Main = QuestStatus.Basic.NotStarted;
            string strName = String.Empty;
            string strGiver = String.Empty;
            MapXY mapReturn = null;
            MapXY mapGiver = null;
            int iPriority = 1;
            if (bit != MM45Bits.Quest.None)
            {
                BitDesc bd = MM45Bits.QuestDescription(bit);
                strName = MM45Quest.GetShortName(bit);
                strGiver = MM45Quest.GetGiver(bit);
                mapReturn = bd.Cleared[0].Where;
                mapGiver = bd.Set[0].Where;
            }
            else
            {
                strGiver = strTarget;
                strName = status.PrimaryObjective;
                status.ReturnToGiver = false;
            }

            int iPreCompleted = 0;
            List<QuestLocation> locations = new List<QuestLocation>();
            locations.Add(new QuestLocation(status.PrimaryObjective));
            if (bit != MM45Bits.Quest.None)
            {
                QuestLocation talk = new QuestLocation(String.Format("Talk to {0}", strGiver), mapGiver, 0);
                talk.Priority = iPriority++;
                talk.Active = status.StatePre(iPreCompleted++);
                locations.Add(talk);
            }

            foreach (QuestLocation pre in status.Prerequisites)
            {
                pre.Priority = iPriority++;
                pre.Active = status.StatePre(iPreCompleted++);
                locations.Add(pre);
            }
            if (targets != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    string strItem = status.ItemsOverride == null ? strTarget : status.ItemsOverride[i];
                    QuestLocation loc = new QuestLocation(String.Format("{0} {1}", status.Verb, strItem), targets[i]);
                    loc.Active = status.StateMain(i);
                    loc.Priority = iPriority;
                    locations.Add(loc);
                }
            }
            else
            {
                for(int i = 0; i < status.MainObjectives.Count; i++)
                {
                    QuestLocation loc = status.MainObjectives[i];
                    loc.Active = status.StateMain(i);
                    loc.Priority = iPriority;
                    locations.Add(loc);
                }
            }

            int iPostCompleted = 0;
            if (status.ReturnToGiver)
            {
                QuestLocation talk = new QuestLocation(String.Format("Return to {0}", strGiver), mapReturn, iPriority++);
                talk.Priority = iPriority++;
                talk.Active = status.StatePost(iPostCompleted++);
                locations.Add(talk);
            }

            foreach (QuestLocation post in status.Postrequisites)
            {
                post.Priority = iPriority++;
                post.Active = status.StatePost(iPostCompleted++);
                locations.Add(post);
            }

            if (status.AnyUnachievable)
                status.Main = QuestStatus.Basic.Unachievable;
            else if (status.AnyNoFile)
                status.Main = QuestStatus.Basic.NoFile;
            else if (status.CompletedPost())
                status.Main = QuestStatus.Basic.Completed;
            else if (status.CompletedPre())
                status.Main = QuestStatus.Basic.Accepted;

            return GetQuest(status, type, bit, strName, strGiver, strReward, locations.ToArray()) as MM45Quest;
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM45Quest> quests, MM45Bits.Quest bit,
            string strTarget, MapXY[] targets, string strReward = "", string strPath = "")
        {
            return AddQuest(status, quests, BasicQuestType.Side, bit, strTarget, targets, strReward, strPath);
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<MM45Quest> quests, MM45Bits.Quest bit,
            string strTarget, MapXY[] targets, string strReward = "")
        {
            return AddQuest(status, quests, BasicQuestType.Primary, bit, strTarget, targets, strReward);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<MM45Quest> quests, MM45Bits.Quest bit, string strTarget, MapXY mapTarget, string strReward = "")
        {
            BasicQuest quest = AddMainQuest(status, quests, bit, strTarget, new MapXY[] { mapTarget }, strReward);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(QuestStatus status, List<MM45Quest> quests, BasicQuestType type, MM45Bits.Quest bit,
            string strTarget, MapXY[] targets, string strReward = "", string strPath = "")
        {
            MM45Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bit, strTarget, targets, strReward);
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM45Quest> quests, MM45Bits.Quest bit, string strTarget, MapXY mapTarget, string strReward = "", string strPath = "")
        {
            return AddSideQuest(status, quests, bit, strTarget, new MapXY[] { mapTarget }, strReward, strPath);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM45Quest> quests, string strGiver, string strReward = "", string strPath = "")
        {
            MM45Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, BasicQuestType.Side, MM45Bits.Quest.None, strGiver, null, strReward);
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<MM45Quest> quests, string strGiver = "", string strReward = "")
        {
            MM45Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, BasicQuestType.Primary, MM45Bits.Quest.None, strGiver, null, strReward);
                quest.SortOrder = iSortOrder;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            const string statistics = "Permanent Statistics";
            const string awards = "Character Awards";
            const string keys = "Find Keys";
            const string fetch = "Retrieve Items";
            const string rescue = "Rescue Friends";
            const string kill = "Kill Enemies";
            const string destroy = "Destroy Places";
            const string mining = "Mine Gems and Gold";
            const string puzzles = "Solve Puzzles";

            List<MM45Quest> quests = new List<MM45Quest>();

            if (SaveWinterkill.LocationsOverride == null)
                return quests.ToArray();

            // Primary Statistic quests

            FireRes.AddLocations(new QuestLocation("(+10 Fire Res, +5000 Exp) Read the scroll", MM45.Spots.FireScroll1),
                new QuestLocation("(+10 Fire Res, +5000 Exp) Read the scroll", MM45.Spots.FireScroll2),
                new QuestLocation("(+10 Fire Res) Drink the brew", MM45.Spots.FireBrew1),
                new QuestLocation("(+10 Fire Res) Drink the brew", MM45.Spots.FireBrew2),
                new QuestLocation("(+50 Fire Res) Drink from the fountain", MM45.Spots.FireFountain1),
                new QuestLocation("(+10 Fire Res) Drink from the fountain", MM45.Spots.FireFountain2),
                new QuestLocation("(+20 Fire Res) Read the book", MM45.Spots.FireBook));
            AddSideQuest(FireRes, quests, String.Empty, String.Empty, statistics);

            ElecRes.AddLocations(new QuestLocation("(+10 Elec. Res, +5000 Exp) Read the scroll", MM45.Spots.ElectricityScroll1),
                new QuestLocation("(+10 Elec. Res, +5000 Exp) Read the scroll", MM45.Spots.ElectricityScroll2),
                new QuestLocation("(+10 Elec. Res) Drink the brew", MM45.Spots.ElectricBrew1),
                new QuestLocation("(+10 Elec. Res) Drink the brew", MM45.Spots.ElectricBrew2),
                new QuestLocation("(+50 Elec. Res) Drink from the fountain", MM45.Spots.ElectricityFountain1),
                new QuestLocation("(+10 Elec. Res) Drink from the fountain", MM45.Spots.ElectricityFountain2),
                new QuestLocation("(+20 Elec. Res) Read the book", MM45.Spots.ElectricityBook));
            AddSideQuest(ElecRes, quests, String.Empty, String.Empty, statistics);

            ColdRes.AddLocations(new QuestLocation("(+10 Cold Res) Drink the brew", MM45.Spots.ColdBrew1),
                new QuestLocation("(+10 Cold Res) Drink the brew", MM45.Spots.ColdBrew2),
                new QuestLocation("(+50 Cold Res) Drink from the fountain", MM45.Spots.ColdFountain1),
                new QuestLocation("(+10 Cold Res) Drink from the fountain", MM45.Spots.ColdFountain2));
            AddSideQuest(ColdRes, quests, String.Empty, String.Empty, statistics);

            PoisonRes.AddLocations(new QuestLocation("(+10 Poison Res) Drink the brew", MM45.Spots.PoisonBrew1),
                new QuestLocation("(+10 Poison Res) Drink the brew", MM45.Spots.PoisonBrew2),
                new QuestLocation("(+10 Poison Res) Drink the brew", MM45.Spots.PoisonBrew3),
                new QuestLocation("(+50 Poison Res) Drink from the fountain", MM45.Spots.PoisonFountain1),
                new QuestLocation("(+10 Poison Res) Drink from the fountain", MM45.Spots.PoisonFountain2));
            AddSideQuest(PoisonRes, quests, String.Empty, String.Empty, statistics);

            EnergyRes.AddLocations(new QuestLocation("(+10 Energy Res, +5000 Exp) Read the scroll", MM45.Spots.EnergyScroll1),
                new QuestLocation("(+10 Energy Res, +5000 Exp) Read the scroll", MM45.Spots.EnergyScroll2),
                new QuestLocation("(+10 Energy Res) Drink the brew", MM45.Spots.EnergyBrew1));
            AddSideQuest(EnergyRes, quests, String.Empty, String.Empty, statistics);

            MagicRes.AddLocations(new QuestLocation("(+10 Magic Res, +5000 Exp) Read the scroll", MM45.Spots.MagicScroll1),
                new QuestLocation("(+10 Magic Res, +5000 Exp) Read the scroll", MM45.Spots.MagicScroll2),
                new QuestLocation("(+10 Magic Res) Drink the brew", MM45.Spots.MagicBrew1));
            AddSideQuest(MagicRes, quests, String.Empty, String.Empty, statistics);

            Might.AddLocations(new QuestLocation("(+10 Might) Drink the liquid", MM45.Spots.MightBarrel),
                 new QuestLocation("(+2 Might) Pay 10 Gems", MM45.Spots.MightSkull1),
                 new QuestLocation("(+2 Might) Pay 10 Gems", MM45.Spots.MightSkull2),
                 new QuestLocation("(+3 Might) Pay 20 Gems", MM45.Spots.MightSkull3),
                 new QuestLocation("(+5 Might) Pay 50 Gems", MM45.Spots.MightSkull4),
                 new QuestLocation("(+10 Might) Pay 200 Gems", MM45.Spots.MightSkull5),
                 new QuestLocation("(+5 Might) Drink the juice", MM45.Spots.RedJuice1),
                 new QuestLocation("(+5 Might) Drink the juice", MM45.Spots.RedJuice2),
                 new QuestLocation("(+5 Might) Drink the juice", MM45.Spots.RedJuice3),
                 new QuestLocation("(+5 Might) Drink the juice", MM45.Spots.RedJuice4),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid1),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid2),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid3),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid4),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid5),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid6),
                 new QuestLocation("(+2 Might) Drink the liquid", MM45.Spots.RedLiquid7),
                 new QuestLocation("(+20 Might) Read the Book", MM45.Spots.MightBook),
                 new QuestLocation("(+10 Might) Drink the brew", MM45.Spots.KnightBrew1),
                 new QuestLocation("(+10 Might) Drink the brew", MM45.Spots.KnightBrew2),
                 new QuestLocation("(+10 Might) Drink the brew", MM45.Spots.KnightBrew3),
                 new QuestLocation("(+10 Might) Drink the brew", MM45.Spots.KnightBrew4),
                 new QuestLocation("(+10 Might) Drink the brew", MM45.Spots.KnightBrew5),
                 new QuestLocation("(+5 Might) Drink the potion once", MM45.Spots.RedPotion1),
                 new QuestLocation("(+5 Might) Drink the potion twice", MM45.Spots.RedPotion1),
                 new QuestLocation("(+5 Might) Drink the potion three times", MM45.Spots.RedPotion1),
                 new QuestLocation("(+5 Might) Drink the potion once", MM45.Spots.RedPotion2),
                 new QuestLocation("(+5 Might) Drink the potion twice", MM45.Spots.RedPotion2),
                 new QuestLocation("(+5 Might) Drink the potion three times", MM45.Spots.RedPotion2),
                 new QuestLocation("(+6 Might, all) Free the magpie", MM45.Spots.MightMagpie1),
                 new QuestLocation("(+6 Might, all) Free the magpie", MM45.Spots.MightMagpie2),
                 new QuestLocation("(+10 Might) Eat the apple", MM45.Spots.Apple),
                 new QuestLocation("(+5 Might) Drink the liquid", MM45.Spots.RedLiquid8),
                 new QuestLocation("(+5 Might) Drink the liquid", MM45.Spots.RedLiquid9),
                 new QuestLocation("(+5 Might) Drink the liquid", MM45.Spots.RedLiquid10),
                 new QuestLocation("(+5 Might) Drink the liquid", MM45.Spots.RedLiquid11),
                 new QuestLocation("(+10 Might) Drink the brew once", MM45.Spots.ProteinBrew),
                 new QuestLocation("(+10 Might) Drink the brew twice", MM45.Spots.ProteinBrew),
                 new QuestLocation("(+10 Might) Drink the brew three times", MM45.Spots.ProteinBrew),
                 new QuestLocation("(+10 Might) Drink the brew four times", MM45.Spots.ProteinBrew),
                 new QuestLocation("(+10 Might) Drink the brew five times", MM45.Spots.ProteinBrew),
                 new QuestLocation("(+10 Might) Drink the brew six times", MM45.Spots.ProteinBrew));
            AddSideQuest(Might, quests, String.Empty, String.Empty, statistics);

            Intellect.AddLocations(new QuestLocation("(+5 Intellect, +5000 Exp) Read the scroll", MM45.Spots.IntellectScroll1),
                new QuestLocation("(+5 Intellect, +5000 Exp) Read the scroll", MM45.Spots.IntellectScroll2),
                new QuestLocation("(+2 Intellect) Pay 10 Gems", MM45.Spots.IntellectSkull1),
                new QuestLocation("(+2 Intellect) Pay 10 Gems", MM45.Spots.IntellectSkull2),
                new QuestLocation("(+3 Intellect) Pay 20 Gems", MM45.Spots.IntellectSkull3),
                new QuestLocation("(+5 Intellect) Pay 50 Gems", MM45.Spots.IntellectSkull4),
                new QuestLocation("(+5 Intellect) Pay 50 Gems", MM45.Spots.IntellectSkull5),
                new QuestLocation("(+5 Intellect) Drink the juice", MM45.Spots.OrangeJuice1),
                new QuestLocation("(+5 Intellect) Drink the juice", MM45.Spots.OrangeJuice2),
                new QuestLocation("(+2 Intellect) Drink the liquid", MM45.Spots.OrangeLiquid1),
                new QuestLocation("(+2 Intellect) Drink the liquid", MM45.Spots.OrangeLiquid2),
                new QuestLocation("(+2 Intellect) Drink the liquid", MM45.Spots.OrangeLiquid3),
                new QuestLocation("(+20 Intellect) Read the Book", MM45.Spots.IntellectBook),
                new QuestLocation("(+5 Intellect) Drink the potion once", MM45.Spots.OrangePotion1),
                new QuestLocation("(+5 Intellect) Drink the potion twice", MM45.Spots.OrangePotion1),
                new QuestLocation("(+5 Intellect) Drink the potion three times", MM45.Spots.OrangePotion1),
                new QuestLocation("(+5 Intellect) Drink the potion once", MM45.Spots.OrangePotion2),
                new QuestLocation("(+5 Intellect) Drink the potion twice", MM45.Spots.OrangePotion2),
                new QuestLocation("(+5 Intellect) Drink the potion three times", MM45.Spots.OrangePotion2),
                new QuestLocation("Eat the orange (+10 Intellect),", MM45.Spots.Orange),
                new QuestLocation("(+10 Intellect) Drink the potion once", MM45.Spots.OrangePotion3),
                new QuestLocation("(+10 Intellect) Drink the potion twice", MM45.Spots.OrangePotion3),
                new QuestLocation("(+10 Intellect) Drink the potion three times", MM45.Spots.OrangePotion3),
                new QuestLocation("(+10 Intellect) Drink the potion once", MM45.Spots.OrangePotion4),
                new QuestLocation("(+10 Intellect) Drink the potion twice", MM45.Spots.OrangePotion4),
                new QuestLocation("(+10 Intellect) Drink the potion three times", MM45.Spots.OrangePotion4),
                new QuestLocation("(+10 Intellect) Drink the potion once", MM45.Spots.OrangePotion5),
                new QuestLocation("(+10 Intellect) Drink the potion twice", MM45.Spots.OrangePotion5),
                new QuestLocation("(+10 Intellect) Drink the potion three times", MM45.Spots.OrangePotion5),
                new QuestLocation("(+25 Intellect) Read the book", MM45.Spots.SorcererKnowledge),
                new QuestLocation("(+50 Intellect) Read the book", MM45.Spots.FantasticKnowledgeBook));
            AddSideQuest(Intellect, quests, String.Empty, String.Empty, statistics);

            Personality.AddLocations(new QuestLocation("(+10 Personality, +5000 Exp) Read the scroll", MM45.Spots.PersonalityScroll1),
                new QuestLocation("(+10 Personality, +5000 Exp) Read the scroll", MM45.Spots.PersonalityScroll2),
                new QuestLocation("(+2 Personality) Pay 10 Gems", MM45.Spots.PersonalitySkull1),
                new QuestLocation("(+2 Personality) Pay 10 Gems", MM45.Spots.PersonalitySkull2),
                new QuestLocation("(+3 Personality) Pay 20 Gems", MM45.Spots.PersonalitySkull3),
                new QuestLocation("(+5 Personality) Pay 50 Gems", MM45.Spots.PersonalitySkull4),
                new QuestLocation("(+5 Personality) Pay 50 Gems", MM45.Spots.PersonalitySkull5),
                new QuestLocation("(+5 Personality) Drink the juice", MM45.Spots.BlueJuice1),
                new QuestLocation("(+5 Personality) Drink the juice", MM45.Spots.BlueJuice2),
                new QuestLocation("(+2 Personality) Drink the liquid", MM45.Spots.BlueLiquid1),
                new QuestLocation("(+2 Personality) Drink the liquid", MM45.Spots.BlueLiquid2),
                new QuestLocation("(+2 Personality) Drink the liquid", MM45.Spots.BlueLiquid3),
                new QuestLocation("(+20 Personality) Read the Book", MM45.Spots.PersonalityBook),
                new QuestLocation("(+10 Personality) Drink the brew", MM45.Spots.QueenBrew1),
                new QuestLocation("(+10 Personality) Drink the brew", MM45.Spots.QueenBrew2),
                new QuestLocation("(+10 Personality) Drink the brew", MM45.Spots.QueenBrew3),
                new QuestLocation("(+5 Personality) Drink the potion once", MM45.Spots.BluePotion1),
                new QuestLocation("(+5 Personality) Drink the potion twice", MM45.Spots.BluePotion1),
                new QuestLocation("(+5 Personality) Drink the potion three times", MM45.Spots.BluePotion1),
                new QuestLocation("(+5 Personality) Drink the potion once", MM45.Spots.BluePotion2),
                new QuestLocation("(+5 Personality) Drink the potion twice", MM45.Spots.BluePotion2),
                new QuestLocation("(+5 Personality) Drink the potion three times", MM45.Spots.BluePotion2),
                new QuestLocation("(+6 Personality, all) Free the parakeet", MM45.Spots.PersonalityParakeet1),
                new QuestLocation("(+6 Personality, all) Free the parakeet", MM45.Spots.PersonalityParakeet2),
                new QuestLocation("(+10 Personality) Eat the blueberries", MM45.Spots.Blueberries),
                new QuestLocation("(+25 Personality) Drink from the cauldron", MM45.Spots.PersonalityCauldron),
                new QuestLocation("(+5 Personality) Bathe in the pool", MM45.Spots.YakPersonality));
            AddSideQuest(Personality, quests, String.Empty, String.Empty, statistics);

            Endurance.AddLocations(new QuestLocation("(+2 Endurance) Pay 10 Gems", MM45.Spots.EnduranceSkull1),
                new QuestLocation("(+2 Endurance) Pay 10 Gems", MM45.Spots.EnduranceSkull2),
                new QuestLocation("(+5 Endurance) Pay 50 Gems", MM45.Spots.EnduranceSkull3),
                new QuestLocation("(+10 Endurance) Pay 200 Gems", MM45.Spots.EnduranceSkull4),
                new QuestLocation("(+10 Endurance) Pay 200 Gems", MM45.Spots.EnduranceSkull5),
                new QuestLocation("(+5 Endurance) Drink the juice", MM45.Spots.GreenJuice1),
                new QuestLocation("(+5 Endurance) Drink the juice", MM45.Spots.GreenJuice2),
                new QuestLocation("(+5 Endurance) Drink the juice", MM45.Spots.GreenJuice3),
                new QuestLocation("(+2 Endurance) Drink the liquid", MM45.Spots.GreenLiquid1),
                new QuestLocation("(+2 Endurance) Drink the liquid", MM45.Spots.GreenLiquid2),
                new QuestLocation("(+2 Endurance) Drink the liquid", MM45.Spots.GreenLiquid3),
                new QuestLocation("(+2 Endurance) Drink the liquid", MM45.Spots.GreenLiquid4),
                new QuestLocation("(+40 Endurance) Drink the potion once", MM45.Spots.EndurancePotion1),
                new QuestLocation("(+40 Endurance) Drink the potion twice", MM45.Spots.EndurancePotion1),
                new QuestLocation("(+40 Endurance) Drink the potion three times", MM45.Spots.EndurancePotion1),
                new QuestLocation("(+40 Endurance) Drink the potion four times", MM45.Spots.EndurancePotion1),
                new QuestLocation("(+40 Endurance) Drink the potion once", MM45.Spots.EndurancePotion2),
                new QuestLocation("(+40 Endurance) Drink the potion twice", MM45.Spots.EndurancePotion2),
                new QuestLocation("(+40 Endurance) Drink the potion three times", MM45.Spots.EndurancePotion2),
                new QuestLocation("(+40 Endurance) Drink the potion four times", MM45.Spots.EndurancePotion2),
                new QuestLocation("(+20 Endurance) Read the Book", MM45.Spots.EnduranceBook),
                new QuestLocation("(+5 Endurance) Drink the potion once", MM45.Spots.GreenPotion1),
                new QuestLocation("(+5 Endurance) Drink the potion twice", MM45.Spots.GreenPotion1),
                new QuestLocation("(+5 Endurance) Drink the potion three times", MM45.Spots.GreenPotion1),
                new QuestLocation("(+5 Endurance) Drink the potion once", MM45.Spots.GreenPotion2),
                new QuestLocation("(+5 Endurance) Drink the potion twice", MM45.Spots.GreenPotion2),
                new QuestLocation("(+5 Endurance) Drink the potion three times", MM45.Spots.GreenPotion2),
                new QuestLocation("(+6 Endurance, all) Free the eagle", MM45.Spots.EnduranceEagle1),
                new QuestLocation("(+6 Endurance, all) Free the eagle", MM45.Spots.EnduranceEagle2),
                new QuestLocation("(+6 Endurance, all) Free the eagle", MM45.Spots.EnduranceEagle3),
                new QuestLocation("(+10 Endurance) Eat the pear", MM45.Spots.Pear),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid5),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid6),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid7),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid8),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid9),
                new QuestLocation("(+5 Endurance) Drink the liquid", MM45.Spots.GreenLiquid10),
                new QuestLocation("(+10 Endurance) Drink the brew once", MM45.Spots.VitaminBrew),
                new QuestLocation("(+10 Endurance) Drink the brew twice", MM45.Spots.VitaminBrew),
                new QuestLocation("(+10 Endurance) Drink the brew three times", MM45.Spots.VitaminBrew),
                new QuestLocation("(+10 Endurance) Drink the brew four times", MM45.Spots.VitaminBrew),
                new QuestLocation("(+10 Endurance) Drink the brew five times", MM45.Spots.VitaminBrew),
                new QuestLocation("(+10 Endurance) Drink the brew six times", MM45.Spots.VitaminBrew),
                new QuestLocation("(+25 Endurance) Drink from the cauldron", MM45.Spots.EnduranceCauldron),
                new QuestLocation("(+5 Endurance) Bathe in the pool", MM45.Spots.YakEndurance));
            AddSideQuest(Endurance, quests, String.Empty, String.Empty, statistics);

            Speed.AddLocations(new QuestLocation("(+5 Speed, +5000 Exp) Read the scroll", MM45.Spots.SpeedScroll1),
                new QuestLocation("(+5 Speed, +5000 Exp) Read the scroll", MM45.Spots.SpeedScroll2),
                new QuestLocation("(+2 Speed) Pay 10 Gems", MM45.Spots.SpeedSkull1),
                new QuestLocation("(+2 Speed) Pay 10 Gems", MM45.Spots.SpeedSkull2),
                new QuestLocation("(+3 Speed) Pay 20 Gems", MM45.Spots.SpeedSkull3),
                new QuestLocation("(+5 Speed) Pay 50 Gems", MM45.Spots.SpeedSkull4),
                new QuestLocation("(+5 Speed) Drink the juice", MM45.Spots.PurpleJuice1),
                new QuestLocation("(+5 Speed) Drink the juice", MM45.Spots.PurpleJuice2),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid1),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid2),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid3),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid4),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid5),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid6),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid7),
                new QuestLocation("(+2 Speed) Drink the liquid", MM45.Spots.PurpleLiquid8),
                new QuestLocation("(+20 Speed) Read the Book", MM45.Spots.SpeedBook),
                new QuestLocation("(+5 Speed) Drink the potion once", MM45.Spots.PurplePotion1),
                new QuestLocation("(+5 Speed) Drink the potion twice", MM45.Spots.PurplePotion1),
                new QuestLocation("(+5 Speed) Drink the potion three times", MM45.Spots.PurplePotion1),
                new QuestLocation("(+5 Speed) Drink the potion once", MM45.Spots.PurplePotion2),
                new QuestLocation("(+5 Speed) Drink the potion twice", MM45.Spots.PurplePotion2),
                new QuestLocation("(+5 Speed) Drink the potion three times", MM45.Spots.PurplePotion2),
                new QuestLocation("(+6 Speed, all) Free the sparrow", MM45.Spots.SpeedSparrow1),
                new QuestLocation("(+6 Speed, all) Free the sparrow", MM45.Spots.SpeedSparrow2),
                new QuestLocation("(+10 Speed) Eat the plum", MM45.Spots.Plum),
                new QuestLocation("(+10 Speed) Drink the potion once", MM45.Spots.PurplePotion3),
                new QuestLocation("(+10 Speed) Drink the potion twice", MM45.Spots.PurplePotion3),
                new QuestLocation("(+10 Speed) Drink the potion three times", MM45.Spots.PurplePotion3),
                new QuestLocation("(+10 Speed) Drink the potion once", MM45.Spots.PurplePotion4),
                new QuestLocation("(+10 Speed) Drink the potion twice", MM45.Spots.PurplePotion4),
                new QuestLocation("(+10 Speed) Drink the potion three times", MM45.Spots.PurplePotion4),
                new QuestLocation("(+10 Speed) Drink the potion once", MM45.Spots.PurplePotion5),
                new QuestLocation("(+10 Speed) Drink the potion twice", MM45.Spots.PurplePotion5),
                new QuestLocation("(+10 Speed) Drink the potion three times", MM45.Spots.PurplePotion5),
                new QuestLocation("(+25 Speed) Drink from the cauldron", MM45.Spots.SpeedCauldron1),
                new QuestLocation("(+25 Speed) Drink from the cauldron", MM45.Spots.SpeedCauldron2));
            AddSideQuest(Speed, quests, String.Empty, String.Empty, statistics);

            Accuracy.AddLocations(new QuestLocation("(+2 Accuracy) Pay 10 Gems", MM45.Spots.AccuracySkull1),
                new QuestLocation("(+2 Accuracy) Pay 10 Gems", MM45.Spots.AccuracySkull2),
                new QuestLocation("(+3 Accuracy) Pay 20 Gems", MM45.Spots.AccuracySkull3),
                new QuestLocation("(+5 Accuracy) Pay 50 Gems", MM45.Spots.AccuracySkull4),
                new QuestLocation("(+10 Accuracy) Pay 200 Gems", MM45.Spots.AccuracySkull5),
                new QuestLocation("(+10 Accuracy) Pay 200 Gems", MM45.Spots.AccuracySkull6),
                new QuestLocation("(+5 Accuracy) Drink the juice", MM45.Spots.YellowJuice1),
                new QuestLocation("(+5 Accuracy) Drink the juice", MM45.Spots.YellowJuice2),
                new QuestLocation("(+5 Accuracy) Drink the juice", MM45.Spots.YellowJuice3),
                new QuestLocation("(+5 Accuracy) Drink the juice", MM45.Spots.YellowJuice4),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid1),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid2),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid3),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid4),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid5),
                new QuestLocation("(+2 Accuracy) Drink the liquid", MM45.Spots.YellowLiquid6),
                new QuestLocation("(+20 Accuracy) Read the Book", MM45.Spots.AccuracyBook),
                new QuestLocation("(+5 Accuracy) Drink the potion once", MM45.Spots.YellowPotion1),
                new QuestLocation("(+5 Accuracy) Drink the potion twice", MM45.Spots.YellowPotion1),
                new QuestLocation("(+5 Accuracy) Drink the potion three times", MM45.Spots.YellowPotion1),
                new QuestLocation("(+5 Accuracy) Drink the potion once", MM45.Spots.YellowPotion2),
                new QuestLocation("(+5 Accuracy) Drink the potion twice", MM45.Spots.YellowPotion2),
                new QuestLocation("(+5 Accuracy) Drink the potion three times", MM45.Spots.YellowPotion2),
                new QuestLocation("(+6 Accuracy, all) Free the albatross", MM45.Spots.AccuracyAlbatross1),
                new QuestLocation("(+6 Accuracy, all) Free the albatross", MM45.Spots.AccuracyAlbatross2),
                new QuestLocation("(+10 Accuracy) Eat the banana", MM45.Spots.Banana));
            AddSideQuest(Accuracy, quests, String.Empty, String.Empty, statistics);

            Luck.AddLocations(new QuestLocation("(+2 Luck) Pay 10 Gems", MM45.Spots.LuckSkull1),
                new QuestLocation("(+2 Luck) Pay 10 Gems", MM45.Spots.LuckSkull2),
                new QuestLocation("(+5 Luck) Pay 50 Gems", MM45.Spots.LuckSkull3),
                new QuestLocation("(+5 Luck) Pay 50 Gems", MM45.Spots.LuckSkull4),
                new QuestLocation("(+5 Luck) Pay 50 Gems", MM45.Spots.LuckSkull5),
                new QuestLocation("(+5 Luck) Pay 50 Gems", MM45.Spots.LuckSkull6),
                new QuestLocation("(+5 Luck) Drink the juice", MM45.Spots.WhiteJuice1),
                new QuestLocation("(+5 Luck) Drink the juice", MM45.Spots.WhiteJuice2),
                new QuestLocation("(+2 Luck) Drink the liquid", MM45.Spots.WhiteLiquid1),
                new QuestLocation("(+2 Luck) Drink the liquid", MM45.Spots.WhiteLiquid2),
                new QuestLocation("(+2 Luck) Drink the liquid", MM45.Spots.WhiteLiquid3),
                new QuestLocation("(+2 Luck) Drink the liquid", MM45.Spots.WhiteLiquid4),
                new QuestLocation("(+2 Luck) Drink the liquid", MM45.Spots.WhiteLiquid5),
                new QuestLocation("(+5 Luck) Drink the potion once", MM45.Spots.WhitePotion1),
                new QuestLocation("(+5 Luck) Drink the potion twice", MM45.Spots.WhitePotion1),
                new QuestLocation("(+5 Luck) Drink the potion three times", MM45.Spots.WhitePotion1),
                new QuestLocation("(+6 Luck, all) Free the lark", MM45.Spots.LuckLark1),
                new QuestLocation("(+6 Luck, all) Free the lark", MM45.Spots.LuckLark2),
                new QuestLocation("(+10 Luck) Eat the coconut", MM45.Spots.Coconut));
            AddSideQuest(Luck, quests, String.Empty, String.Empty, statistics);

            Level.AddLocations(new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower1),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower2),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower3),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower4),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower5),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower6),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower7),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower8),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower9),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower10),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower11),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower12),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower13),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower14),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower15),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower16),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower17),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower18),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower19),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower20),
                new QuestLocation("(3-5 Levels) Touch the crystal", MM45.Spots.DragonPower21),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer1),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer2),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer3),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer4),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer5),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer6),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer7),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer8),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer9),
                new QuestLocation("(+5 Levels) Drink the embalmer", MM45.Spots.Embalmer10),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice1),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice1),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice2),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice2),
                new QuestLocation("(+5 Levels) Drink the juice three times", MM45.Spots.XeenJuice2),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice3),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice3),
                new QuestLocation("(+5 Levels) Drink the juice three times", MM45.Spots.XeenJuice3),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice4),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice4),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice5),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice5),
                new QuestLocation("(+5 Levels) Drink the juice once", MM45.Spots.XeenJuice6),
                new QuestLocation("(+5 Levels) Drink the juice twice", MM45.Spots.XeenJuice6),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood1),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood2),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood3),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood4),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood5),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood6),
                new QuestLocation("(+1 Level) Eat the food", MM45.Spots.DeadFood7),
                new QuestLocation("(+5 Levels) Read the book", MM45.Spots.GreatPowerBook),
                new QuestLocation("(+1 Level) Drink from the fountain", MM45.Spots.FountainOfLife),
                new QuestLocation("(+5 Levels) Read the book", MM45.Spots.PrinceBook),
                new QuestLocation("(+1 Level) Drink from the well", MM45.Spots.ShangriLaWell),
                new QuestLocation("(+1 Level) Become a Master of Golems", MM45.Spots.GolemMaster),
                new QuestLocation("(+1 Level) Invoke the statue", MM45.Spots.SuperExplorer));
            AddSideQuest(Level, quests, String.Empty, String.Empty, statistics);

            MultiStat.AddLocations(new QuestLocation("(+3 Stats) Try the sludge", MM45.Spots.BlackBarrel1),
                new QuestLocation("(+3 Stats) Try the sludge", MM45.Spots.BlackBarrel2),
                new QuestLocation("(+3 Stats) Try the sludge", MM45.Spots.BlackBarrel3),
                new QuestLocation("(+3 Stats) Try the sludge", MM45.Spots.BlackBarrel4),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice1),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice2),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice3),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice4),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice5),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice6),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice7),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice8),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice9),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice10),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice11),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice12),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice13),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice14),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice15),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice16),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice17),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice18),
                new QuestLocation("(+1 Stats) Drink the juice", MM45.Spots.TrollJuice19),
                new QuestLocation("(+2 Levels, +5 Stats) Sit on the throne", MM45.Spots.EuphoriaThrone),
                new QuestLocation("(+50 Stats) Use the device", MM45.Spots.EnergyDevice1),
                new QuestLocation("(+50 Stats) Use the device", MM45.Spots.EnergyDevice2));
            AddSideQuest(MultiStat, quests, String.Empty, String.Empty, statistics);
            AddSideQuest(StealElixir, quests, MM45Bits.Quest.StealElixir, "the elixir", MM45.Spots.RestorationElixir, "+5 Personality, 250000 Exp", statistics);

            // Puzzle quests

            Pitchfork.AddLocations("Open the chest labeled \"P\" (closes I)", MM45.Spots.ChestP, "Open the chest labeled \"K\"", MM45.Spots.ChestK,
                new QuestLocation("Open the chest labeled \"I\" (closes T)", MM45.Spots.ChestI),
                new QuestLocation("Open the chest labeled \"T\" (closes C)", MM45.Spots.ChestT),
                new QuestLocation("Open the chest labeled \"C\" (closes H)", MM45.Spots.ChestC),
                new QuestLocation("Open the chest labeled \"H\" (closes F)", MM45.Spots.ChestH),
                new QuestLocation("Open the chest labeled \"F\" (closes O)", MM45.Spots.ChestF),
                new QuestLocation("Open the chest labeled \"O\" (closes R)", MM45.Spots.ChestO),
                new QuestLocation("Open the chest labeled \"R\"", MM45.Spots.ChestR));
            AddSideQuest(Pitchfork, quests, String.Empty, "250000 Exp", puzzles);

            NumberChests.AddLocations("Close the chest labeled \"1\" (opens 2)", MM45.Spots.Chest1, "Close the chest labeled \"9\"", MM45.Spots.Chest9,
                new QuestLocation("Close the chest labeled \"2\" (opens 3)", MM45.Spots.Chest2),
                new QuestLocation("Close the chest labeled \"3\" (opens 4)", MM45.Spots.Chest3),
                new QuestLocation("Close the chest labeled \"4\" (opens 5)", MM45.Spots.Chest4),
                new QuestLocation("Close the chest labeled \"5\" (opens 6)", MM45.Spots.Chest5),
                new QuestLocation("Close the chest labeled \"6\" (opens 7)", MM45.Spots.Chest6),
                new QuestLocation("Close the chest labeled \"7\" (opens 8)", MM45.Spots.Chest7),
                new QuestLocation("Close the chest labeled \"8\"", MM45.Spots.Chest8));
            AddSideQuest(NumberChests, quests, String.Empty, "50000 Exp", puzzles);

            GolemDungeon.AddLocations(new QuestLocation("Flip the switch", MM45.Spots.GolemTree1),
                new QuestLocation("Flip the switch", MM45.Spots.GolemTree2),
                new QuestLocation("Flip the switch", MM45.Spots.GolemTree3),
                new QuestLocation("Flip the switch", MM45.Spots.GolemTree4),
                new QuestLocation("Flip the switch", MM45.Spots.GolemTree5),
                new QuestLocation("Flip the switch", MM45.Spots.GolemTree6),
                new QuestLocation("Push the button once", MM45.Spots.GolemStone1),
                new QuestLocation("Push the button once", MM45.Spots.GolemStone2),
                new QuestLocation("Push the button once", MM45.Spots.GolemStone3),
                new QuestLocation("Push the button twice", MM45.Spots.GolemStone1),
                new QuestLocation("Push the button twice", MM45.Spots.GolemStone3));
            GolemDungeon.Postrequisites.Add(new QuestLocation("Flip the switch", MM45.Spots.GolemMainSwitch));
            AddSideQuest(GolemDungeon, quests, String.Empty, String.Empty, puzzles);

            Crossword.AddLocations(new QuestLocation("Answer 1 Across: Scepter", MM45.Spots.CWScepter),
                new QuestLocation("Answer 2 Across: Teleportation", MM45.Spots.CWTeleportation),
                new QuestLocation("Answer 3 Across: Distortion", MM45.Spots.CWDistortion),
                new QuestLocation("Answer 4 Across: Leprechaun", MM45.Spots.CWLeprechaun),
                new QuestLocation("Answer 5 Across: Vampire", MM45.Spots.CWVampire),
                new QuestLocation("Answer 6 Across: Kalindra", MM45.Spots.CWKalindra),
                new QuestLocation("Answer 7 Across: Magic", MM45.Spots.CWMagic),
                new QuestLocation("Answer 8 Across: Nova", MM45.Spots.CWNova),
                new QuestLocation("Answer 9 Across: Revitalize", MM45.Spots.CWRevitalize),
                new QuestLocation("Answer 10 Across: Seer", MM45.Spots.CWSeer),
                new QuestLocation("Answer 11 Across: Iguana", MM45.Spots.CWIguana),
                new QuestLocation("Answer 12 Across: Error", MM45.Spots.CWError),
                new QuestLocation("Answer 13 Across: Corak", MM45.Spots.CWCorak),
                new QuestLocation("Answer 14 Across: Acid", MM45.Spots.CWAcid),
                new QuestLocation("Answer 15 Across: Arena", MM45.Spots.CWArena),
                new QuestLocation("Answer 16 Across: Archer", MM45.Spots.CWArcher),
                new QuestLocation("Answer 17 Across: Tito", MM45.Spots.CWTito),
                new QuestLocation("Answer 18 Across: Sorceress", MM45.Spots.CWSorceress),
                new QuestLocation("Answer 19 Across: Resurrection", MM45.Spots.CWResurrection),
                new QuestLocation("Answer 20 Across: Ranger", MM45.Spots.CWRanger),
                new QuestLocation("Answer 21 Across: Alamar", MM45.Spots.CWAlamar),
                new QuestLocation("Answer 22 Across: Severe", MM45.Spots.CWSevere),
                new QuestLocation("Answer 23 Across: Therewolf", MM45.Spots.CWTherewolf),
                new QuestLocation("Answer 24 Across: Necropolis", MM45.Spots.CWNecropolis),
                new QuestLocation("Answer 25 Across: Snout", MM45.Spots.CWSnout),
                new QuestLocation("Answer 26 Across: Knowledge", MM45.Spots.CWKnowledge),
                new QuestLocation("Answer 27 Across: Flamberge", MM45.Spots.CWFlamberge),
                new QuestLocation("Answer 28 Across: Spell", MM45.Spots.CWSpell),
                new QuestLocation("Answer 29 Across: Use", MM45.Spots.CWUse),
                new QuestLocation("Answer 30 Across: Prestidigitator", MM45.Spots.CWPrestidigitator),
                new QuestLocation("Answer 31 Across: Vulture", MM45.Spots.CWVulture),
                new QuestLocation("Answer 32 Across: Gold", MM45.Spots.CWGold),
                new QuestLocation("Answer 33 Across: Agile", MM45.Spots.CWAgile),
                new QuestLocation("Answer 34 Across: Fable", MM45.Spots.CWFable),
                new QuestLocation("Answer 35 Across: Fisherman", MM45.Spots.CWFisherman),
                new QuestLocation("Answer 36 Across: Clairvoyance", MM45.Spots.CWClairvoyance),
                new QuestLocation("Answer 37 Across: Trident", MM45.Spots.CWTrident),
                new QuestLocation("Answer 38 Across: Swine", MM45.Spots.CWSwine),
                new QuestLocation("Answer 39 Across: Skeleton", MM45.Spots.CWSkeleton),
                new QuestLocation("Answer 40 Across: Sandcaster", MM45.Spots.CWSandcaster),
                new QuestLocation("Answer 41 Across: Energy", MM45.Spots.CWEnergy),
                new QuestLocation("Answer 42 Down: Darkstone", MM45.Spots.CWDarkstone),
                new QuestLocation("Answer 43 Down: Dragon", MM45.Spots.CWDragon),
                new QuestLocation("Answer 44 Down: Castleview", MM45.Spots.CWCastleview),
                new QuestLocation("Answer 45 Down: Winterkill", MM45.Spots.CWWinterkill),
                new QuestLocation("Answer 46 Down: Tether", MM45.Spots.CWTether),
                new QuestLocation("Answer 47 Down: Sphinx", MM45.Spots.CWSphinx),
                new QuestLocation("Answer 48 Down: Beast", MM45.Spots.CWBeast),
                new QuestLocation("Answer 49 Down: Might", MM45.Spots.CWMight),
                new QuestLocation("Answer 50 Down: Disease", MM45.Spots.CWDisease),
                new QuestLocation("Answer 51 Down: Page", MM45.Spots.CWPage),
                new QuestLocation("Answer 52 Down: Criminal", MM45.Spots.CWCriminal),
                new QuestLocation("Answer 53 Down: Druid", MM45.Spots.CWDruid),
                new QuestLocation("Answer 54 Down: Octopus", MM45.Spots.CWOctopus),
                new QuestLocation("Answer 55 Down: Arachnoid", MM45.Spots.CWArachnoid),
                new QuestLocation("Answer 56 Down: Wizard", MM45.Spots.CWWizard),
                new QuestLocation("Answer 57 Down: Griffin", MM45.Spots.CWGriffin),
                new QuestLocation("Answer 58 Down: Insect", MM45.Spots.CWInsect),
                new QuestLocation("Answer 59 Down: Moat", MM45.Spots.CWMoat),
                new QuestLocation("Answer 60 Down: Fear", MM45.Spots.CWFear),
                new QuestLocation("Answer 61 Down: Accuracy", MM45.Spots.CWAccuracy),
                new QuestLocation("Answer 62 Down: Falista", MM45.Spots.CWFalista),
                new QuestLocation("Answer 63 Down: Eerie", MM45.Spots.CWEerie),
                new QuestLocation("Answer 64 Down: Centipede", MM45.Spots.CWCentipede),
                new QuestLocation("Answer 65 Down: Mountaineer", MM45.Spots.CWMountaineer),
                new QuestLocation("Answer 66 Down: Alligator", MM45.Spots.CWAlligator),
                new QuestLocation("Answer 67 Down: Dungeon", MM45.Spots.CWDungeon),
                new QuestLocation("Answer 68 Down: Rat", MM45.Spots.CWRat),
                new QuestLocation("Answer 69 Down: Gargoyle", MM45.Spots.CWGargoyle),
                new QuestLocation("Answer 70 Down: Startle", MM45.Spots.CWStartle),
                new QuestLocation("Answer 71 Down: Armadillo", MM45.Spots.CWArmadillo),
                new QuestLocation("Answer 72 Down: Goblin", MM45.Spots.CWGoblin),
                new QuestLocation("Answer 73 Down: Mummy", MM45.Spots.CWMummy),
                new QuestLocation("Answer 74 Down: Undead", MM45.Spots.CWUndead),
                new QuestLocation("Answer 75 Down: Venom", MM45.Spots.CWVenom),
                new QuestLocation("Answer 76 Down: Mirror", MM45.Spots.CWMirror),
                new QuestLocation("Answer 77 Down: Xeen", MM45.Spots.CWXeen),
                new QuestLocation("Answer 78 Down: Barbarian", MM45.Spots.CWBarbarian),
                new QuestLocation("Answer 79 Down: Pegasus", MM45.Spots.CWPegasus),
                new QuestLocation("Answer 80 Down: Crown", MM45.Spots.CWCrown),
                new QuestLocation("Answer 81 Down: JVC", MM45.Spots.CWJVC),
                new QuestLocation("Answer 82 Down: Prisoner", MM45.Spots.CWPrisoner),
                new QuestLocation("Answer 83 Down: Orb", MM45.Spots.CWOrb),
                new QuestLocation("Answer 84 Down: Cartographer", MM45.Spots.CWCartographer),
                new QuestLocation("Answer 85 Down: Sewer", MM45.Spots.CWSewer),
                new QuestLocation("Answer 86 Down: Stumble", MM45.Spots.CWStumble),
                new QuestLocation("Answer 87 Down: Minotaur", MM45.Spots.CWMinotaur),
                new QuestLocation("Answer 88 Down: Crusader", MM45.Spots.CWCrusader),
                new QuestLocation("Answer 89 Down: Elements", MM45.Spots.CWElements));
            Crossword.Postrequisites.Add(new QuestLocation("Descend the stairs", MM45.Spots.WordMaster));
            AddSideQuest(Crossword, quests, String.Empty, "+5 Levels", puzzles);

            TreasureTeleport.AddLocations(new QuestLocation("Ring the gong", MM45.Spots.TTGong1),
                new QuestLocation("Ring the gong", MM45.Spots.TTGong2),
                new QuestLocation("Ring the gong", MM45.Spots.TTGong3),
                new QuestLocation("Ring the gong", MM45.Spots.TTGong4));
            TreasureTeleport.Postrequisites.Add(new QuestLocation("Pull the lever", MM45.Spots.TTLever));
            AddSideQuest(TreasureTeleport, quests, String.Empty, String.Empty, puzzles);

            NightshadowWell.AddLocations(new QuestLocation("Set the sundial to 9", MM45.Spots.Sundial1),
                new QuestLocation("Set the sundial to 9", MM45.Spots.Sundial2),
                new QuestLocation("Set the sundial to 9", MM45.Spots.Sundial3),
                new QuestLocation("Open the coffin between 9 PM and 5 AM", MM45.Spots.CountDraco),
                new QuestLocation("Defeat Count Draco", NightshadowWell.Location(0)));
            NightshadowWell.Postrequisites.Add(new QuestLocation("Drink from the clean well", MM45.Spots.NightshadowWell));
            AddSideQuest(NightshadowWell, quests, String.Empty, "50000 Exp, 99999 Gold, 3 L5 Items", puzzles);

            PyramidNumbers.AddLocations(new QuestLocation("Answer \"Three\"", MM45.Spots.PyramidNum3),
                new QuestLocation("Answer \"Four\"", MM45.Spots.PyramidNum4),
                new QuestLocation("Answer \"Five\"", MM45.Spots.PyramidNum5),
                new QuestLocation("Answer \"Six\"", MM45.Spots.PyramidNum6),
                new QuestLocation("Answer \"Seven\"", MM45.Spots.PyramidNum7),
                new QuestLocation("Answer \"Eight\"", MM45.Spots.PyramidNum8),
                new QuestLocation("Answer \"Nine\"", MM45.Spots.PyramidNum9),
                new QuestLocation("Answer \"Ten\"", MM45.Spots.PyramidNum10),
                new QuestLocation("Pull the lever", MM45.Spots.PyramidLever));
            PyramidNumbers.Postrequisites.Add(new QuestLocation("Collect the treasure", MM45.Spots.PyramidNumTreasure));
            AddSideQuest(PyramidNumbers, quests, String.Empty, "2500000 Gold, 5000 Gems, 5 L7 Items", puzzles);

            // Spell learning quests

            AddSpellQuest(SpellClericAcidSpray, quests, MM45SpellIndex.AcidSpray);
            AddSpellQuest(SpellClericAwaken, quests, MM45SpellIndex.Awaken);
            AddSpellQuest(SpellClericBeastMaster, quests, MM45SpellIndex.BeastMaster);
            AddSpellQuest(SpellClericBless, quests, MM45SpellIndex.Bless);
            AddSpellQuest(SpellClericColdRay, quests, MM45SpellIndex.ColdRay);
            AddSpellQuest(SpellClericCreateFood, quests, MM45SpellIndex.CreateFood);
            AddSpellQuest(SpellClericCureDisease, quests, MM45SpellIndex.CureDisease);
            AddSpellQuest(SpellClericCureParalysis, quests, MM45SpellIndex.CureParalysis);
            AddSpellQuest(SpellClericCurePoison, quests, MM45SpellIndex.CurePoison);
            AddSpellQuest(SpellClericCureWounds, quests, MM45SpellIndex.CureWounds);
            AddSpellQuest(SpellClericDayOfProtection, quests, MM45SpellIndex.DayOfProtection);
            AddSpellQuest(SpellClericDeadlySwarm, quests, MM45SpellIndex.DeadlySwarm);
            AddSpellQuest(SpellClericDivineIntervention, quests, MM45SpellIndex.DivineIntervention);
            AddSpellQuest(SpellClericFieryFlail, quests, MM45SpellIndex.FieryFlail);
            AddSpellQuest(SpellClericFirstAid, quests, MM45SpellIndex.FirstAid);
            AddSpellQuest(SpellClericFlyingFist, quests, MM45SpellIndex.FlyingFist);
            AddSpellQuest(SpellClericFrostBite, quests, MM45SpellIndex.FrostBite);
            AddSpellQuest(SpellClericHeroism, quests, MM45SpellIndex.Heroism);
            AddSpellQuest(SpellClericHolyBonus, quests, MM45SpellIndex.HolyBonus);
            AddSpellQuest(SpellClericHolyWord, quests, MM45SpellIndex.HolyWord);
            AddSpellQuest(SpellClericHypnotize, quests, MM45SpellIndex.Hypnotize);
            AddSpellQuest(SpellClericLight, quests, MM45SpellIndex.Light);
            AddSpellQuest(SpellClericMassDistortion, quests, MM45SpellIndex.MassDistortion);
            AddSpellQuest(SpellClericMoonRay, quests, MM45SpellIndex.MoonRay);
            AddSpellQuest(SpellClericNaturesCure, quests, MM45SpellIndex.NaturesCure);
            AddSpellQuest(SpellClericPain, quests, MM45SpellIndex.Pain);
            AddSpellQuest(SpellClericPowerCure, quests, MM45SpellIndex.PowerCure);
            AddSpellQuest(SpellClericProtFromElements, quests, MM45SpellIndex.ProtFromElements);
            AddSpellQuest(SpellClericRaiseDead, quests, MM45SpellIndex.RaiseDead);
            AddSpellQuest(SpellClericResurrect, quests, MM45SpellIndex.Resurrect);
            AddSpellQuest(SpellClericRevitalize, quests, MM45SpellIndex.Revitalize);
            AddSpellQuest(SpellClericSparks, quests, MM45SpellIndex.Sparks);
            AddSpellQuest(SpellClericStoneToFlesh, quests, MM45SpellIndex.StoneToFlesh);
            AddSpellQuest(SpellClericSunRay, quests, MM45SpellIndex.SunRay);
            AddSpellQuest(SpellClericSuppressDisease, quests, MM45SpellIndex.SuppressDisease);
            AddSpellQuest(SpellClericSuppressPoison, quests, MM45SpellIndex.SuppressPoison);
            AddSpellQuest(SpellClericTownPortal, quests, MM45SpellIndex.TownPortal);
            AddSpellQuest(SpellClericTurnUndead, quests, MM45SpellIndex.TurnUndead);
            AddSpellQuest(SpellClericWalkOnWater, quests, MM45SpellIndex.WalkOnWater);

            AddSpellQuest(SpellArcaneAwaken, quests, MM45SpellIndex.AwakenArcane);
            AddSpellQuest(SpellArcaneClairvoyance, quests, MM45SpellIndex.Clairvoyance);
            AddSpellQuest(SpellArcaneDancingSword, quests, MM45SpellIndex.DancingSword);
            AddSpellQuest(SpellArcaneDayOfSorcery, quests, MM45SpellIndex.DayOfSorcery);
            AddSpellQuest(SpellArcaneDetectMonster, quests, MM45SpellIndex.DetectMonster);
            AddSpellQuest(SpellArcaneDragonSleep, quests, MM45SpellIndex.DragonSleep);
            AddSpellQuest(SpellArcaneElementalStorm, quests, MM45SpellIndex.ElementalStorm);
            AddSpellQuest(SpellArcaneEnchantItem, quests, MM45SpellIndex.EnchantItem);
            AddSpellQuest(SpellArcaneEnergyBlast, quests, MM45SpellIndex.EnergyBlast);
            AddSpellQuest(SpellArcaneEtherealize, quests, MM45SpellIndex.Etherealize);
            AddSpellQuest(SpellArcaneFantasticFreeze, quests, MM45SpellIndex.FantasticFreeze);
            AddSpellQuest(SpellArcaneFingerOfDeath, quests, MM45SpellIndex.FingerOfDeath);
            AddSpellQuest(SpellArcaneFireball, quests, MM45SpellIndex.Fireball);
            AddSpellQuest(SpellArcaneGolemStopper, quests, MM45SpellIndex.GolemStopper);
            AddSpellQuest(SpellArcaneIdentifyMonster, quests, MM45SpellIndex.IdentifyMonster);
            AddSpellQuest(SpellArcaneImplosion, quests, MM45SpellIndex.Implosion);
            AddSpellQuest(SpellArcaneIncinerate, quests, MM45SpellIndex.Incinerate);
            AddSpellQuest(SpellArcaneInferno, quests, MM45SpellIndex.Inferno);
            AddSpellQuest(SpellArcaneInsectSpray, quests, MM45SpellIndex.InsectSpray);
            AddSpellQuest(SpellArcaneItemToGold, quests, MM45SpellIndex.ItemToGold);
            AddSpellQuest(SpellArcaneJump, quests, MM45SpellIndex.Jump);
            AddSpellQuest(SpellArcaneLevitate, quests, MM45SpellIndex.Levitate);
            AddSpellQuest(SpellArcaneLight, quests, MM45SpellIndex.LightArcane);
            AddSpellQuest(SpellArcaneLightningBolt, quests, MM45SpellIndex.LightningBolt);
            AddSpellQuest(SpellArcaneLloydsBeacon, quests, MM45SpellIndex.LloydsBeacon);
            AddSpellQuest(SpellArcaneMagicArrow, quests, MM45SpellIndex.MagicArrow);
            AddSpellQuest(SpellArcaneMegaVolts, quests, MM45SpellIndex.MegaVolts);
            AddSpellQuest(SpellArcanePoisonVolley, quests, MM45SpellIndex.PoisonVolley);
            AddSpellQuest(SpellArcanePowerShield, quests, MM45SpellIndex.PowerShield);
            AddSpellQuest(SpellArcanePrismaticLight, quests, MM45SpellIndex.PrismaticLight);
            AddSpellQuest(SpellArcaneRechargeItem, quests, MM45SpellIndex.RechargeItem);
            AddSpellQuest(SpellArcaneShrapmetal, quests, MM45SpellIndex.Shrapmetal);
            AddSpellQuest(SpellArcaneSleep, quests, MM45SpellIndex.Sleep);
            AddSpellQuest(SpellArcaneStarBurst, quests, MM45SpellIndex.StarBurst);
            AddSpellQuest(SpellArcaneSuperShelter, quests, MM45SpellIndex.SuperShelter);
            AddSpellQuest(SpellArcaneTeleport, quests, MM45SpellIndex.Teleport);
            AddSpellQuest(SpellArcaneTimeDistortion, quests, MM45SpellIndex.TimeDistortion);
            AddSpellQuest(SpellArcaneToxicCloud, quests, MM45SpellIndex.ToxicCloud);
            AddSpellQuest(SpellArcaneWizardEye, quests, MM45SpellIndex.WizardEye);

            // Secondary Skill quests

            AddSkillQuest(SkillArmsMaster, quests, "Flint the Armsmaster", "Errol the Agile", "300 Gold", "", MM45.Spots.ArmsMaster1, MM45.Spots.ArmsMaster2);
            AddSkillQuest(SkillAstrologer, quests, "the Way of Astrology", "Book of Astrology", "", "", MM45.Spots.Astrologer1, MM45.Spots.Astrologer2);
            AddSkillQuest(SkillBodyBuilder, quests, "Arnold the Strong", "Art of Bodybuilding", "1000 Gold", "50000 Gold", MM45.Spots.BodyBuilder1, MM45.Spots.BodyBuilder2);
            AddSkillQuest(SkillCartographer, quests, "Mylo the Mapmaker", "Lydia the Mapmaker", "100 Gold", "10 Gold", MM45.Spots.Cartographer1, MM45.Spots.Cartographer2);
            AddSkillQuest(SkillCrusader, quests, "Valia", "", "", "", MM45.Spots.Crusader, null);
            AddSkillQuest(SkillDirectionSense, quests, "Felix the Tinker", "the bones", "1000 Gold", "", MM45.Spots.DirectionSense1, MM45.Spots.DirectionSense2);
            AddSkillQuest(SkillLinguist, quests, "Natalie the Linguist", "the Book of Languages", "25000 Gold", "", MM45.Spots.Linguist1, MM45.Spots.Linguist2);
            AddSkillQuest(SkillMerchant, quests, "Joe the Merchant", "Morgan the Merchant", "6000 Gold", "5000 Gold", MM45.Spots.Merchant1, MM45.Spots.Merchant2);
            AddSkillQuest(SkillMountaineer, quests, "Martin the Mountaineer", "Freeda the Mountain Woman", "5000 Gold", "5000 Gold", MM45.Spots.Mountaineer1, MM45.Spots.Mountaineer2);
            AddSkillQuest(SkillNavigator, quests, "Gregor the Navigator", "Magneto the Navigator", "10000 Gold", "2000 Gold", MM45.Spots.Navigator1, MM45.Spots.Navigator2);
            AddSkillQuest(SkillPathFinder, quests, "Goldur the Ranger", "Rialdo the Ranger", "1500 Gold", "2000 Gold", MM45.Spots.PathFinder1, MM45.Spots.PathFinder2);
            AddSkillQuest(SkillPrayerMaster, quests, "Joseph the Prayermaster", "Saul the Prayermaster", "10000 Gold", "10000 Gold", MM45.Spots.PrayerMaster1, MM45.Spots.PrayerMaster2);
            AddSkillQuest(SkillPrestidigitator, quests, "the Art of Magic", "Tibora the Magician", "", "1000 Gems", MM45.Spots.Prestidigitator1, MM45.Spots.Prestidigitator2);
            AddSkillQuest(SkillSwimmer, quests, "Darlene the Swimmer", "Ordana the Swimming Instructor", "100 Gold", "250 Gold", MM45.Spots.Swimmer1, MM45.Spots.Swimmer2);
            AddSkillQuest(SkillSpotSecretDoors, quests, "Oslo the Observant", "Cornelius The Leprechaun", "500 Gold", "500 Gold", MM45.Spots.SpotSecretDoors1, MM45.Spots.SpotSecretDoors2);
            AddSkillQuest(SkillDangerSense, quests, "the bones", "Gem the Empath", "", "", MM45.Spots.DangerSense1, MM45.Spots.DangerSense2);

            // Character Award quests

            foreach (MapXY map in MM45.Spots.AwardConvictedThief)
                Thief.MainObjectives.Add(new QuestLocation("Have a non-thief steal from the display case", map));
            AddSideQuest(Thief, quests, String.Empty, String.Empty, awards);

            Drawkcab.Prerequisites.Add(new QuestLocation("Visit Brother Bob", MM45.Spots.MonkBob));
            Drawkcab.AddLocations(new QuestLocation("Visit Brother Otto", MM45.Spots.MonkBob),
                new QuestLocation("Visit Brother Pip", MM45.Spots.MonkBob),
                new QuestLocation("Visit Brother Tinit", MM45.Spots.MonkTinit));
            Drawkcab.Postrequisites.Add(new QuestLocation("Tell Brother Reger \"Palindrome\"", MM45.Spots.MonkReger));
            AddSideQuest(Drawkcab, quests, String.Empty, "500000 Exp", awards);

            RatQueen.Prerequisites.Add(new QuestLocation("Talk to Valio", MM45.Spots.Valio));
            RatQueen.MainObjectives.Add(new QuestLocation("Kill Rooka", RatQueen.Location(0)));
            RatQueen.Postrequisites.Add(new QuestLocation("Return to Valio", MM45.Spots.Valio));
            AddSideQuest(RatQueen, quests, "Valio", "25000 Exp, L4 Item", awards);

            Paladin.Prerequisites.Add(new QuestLocation("Answer \"Paladin\" to the riddle", MM45.Spots.Paladin1));
            Paladin.AddLocations(new QuestLocation("Collect the Obsidian Plate Mail", MM45.Spots.Paladin2),
                new QuestLocation("Collect the Obsidian Helm", MM45.Spots.Paladin3),
                new QuestLocation("Collect the Obsidian Shield", MM45.Spots.Paladin4),
                new QuestLocation("Collect the Obsidian Long Sword", MM45.Spots.Paladin5),
                new QuestLocation("Collect the Obsidian Gauntlets", MM45.Spots.Paladin6),
                new QuestLocation("Collect the Obsidian Boots", MM45.Spots.Paladin7),
                new QuestLocation("Collect the Obsidian Cape", MM45.Spots.Paladin8));
            AddSideQuest(Paladin, quests, String.Empty, "1000000 Exp, Obsidian Items", awards);

            Awards.AddLocations(new QuestLocation("Answer \"Sandcaster\" to the Cartographer's Challenge", MM45.Spots.AwardCartographersChallenge),
                new QuestLocation("Drink from the Shangri-La well", MM45.Spots.AwardFoundShangriLa),
                new QuestLocation("Read Dragon Lore Volume 1", MM45.Spots.AwardLoremasterOfWorms),
                new QuestLocation("Read Dragon Lore Volume 2", MM45.Spots.AwardLoremasterOfLizards),
                new QuestLocation("Read Dragon Lore Volume 3", MM45.Spots.AwardLoremasterOfSerpents),
                new QuestLocation("Read Dragon Lore Volume 4", MM45.Spots.AwardLoremasterOfDrakes),
                new QuestLocation("Read Dragon Lore Volume 5", MM45.Spots.AwardLoremasterOfDragons),
                new QuestLocation("Read The Art of Taxation", MM45.Spots.AwardTaxmanEmeritus),
                new QuestLocation("Answer \"100\" to the Merchant's Challenge", MM45.Spots.AwardMerchantsChallenge),
                new QuestLocation(String.Format("Sit on the {0} Throne", Awards.Item(0)), Awards.Location(0)),
                new QuestLocation("Sit on the Thieves Throne", MM45.Spots.AwardPrinceOfThieves),
                new QuestLocation("Enter \"Computer\" at the terminal", MM45.Spots.AwardSuperGoober),
                new QuestLocation("Enter anything except \"Computer\" at the terminal", MM45.Spots.AwardSuperGoober),
                new QuestLocation("Answer \"3\" to Edmund's challenge", MM45.Spots.AwardSuperiorIntellect));
            AddSideQuest(Awards, quests, String.Empty, String.Empty, awards);
            
            // Quests with keys as rewards

            BringMelons.Prerequisites.Add(new QuestLocation("Talk to Nibbler", MM45.Spots.Nibbler));
            foreach (MapXY map in MM45.Spots.Melons)
                BringMelons.MainObjectives.Add(new QuestLocation("Pick a Monga Melon", map));
            BringMelons.MainObjectives.Add(new QuestLocation("Bring a Monga Melon to Nibbler", MM45.Spots.Nibbler));
            BringMelons.MainObjectives.Add(new QuestLocation("Visit the Temple of Bark", MM45.Spots.TempleOfBark));
            BringMelons.Postrequisites.Add(new QuestLocation("Bring a Monga Melon to Nibbler", MM45.Spots.Nibbler));
            MM45Quest questMelons = AddMainQuest(0, BringMelons, quests, "Nibbler", "Key to Temple of Bark, 20000 Exp") as MM45Quest;

            FindWesternTowerKey.AddLocations("Talk to Dreyfus", MM45.Spots.Dreyfus, "Talk to Dreyfus", MM45.Spots.Dreyfus2,
                new QuestLocation("Find the Key to the Great Western Tower", MM45.Spots.WesternKey),
                new QuestLocation("Return to Dreyfus", MM45.Spots.Dreyfus));
            AddSideQuest(FindWesternTowerKey, quests, "Dreyfus", "500000 Exp, 10000 Gold, 200 Gems", keys);

            FindStatuettes.PreQuest.Add(AddSideQuest(
                FindSandrosHeart, quests, MM45Bits.Quest.FindSandrosHeart, "Sandro's heart", MM45.Spots.SandrosHeart, "Key to Dungeon of Death, 2000000 Exp", keys));
            FindStatuettes.SetItems("pegasus statuette", "dragon statuette", "griffin statuette");
            AddSideQuest(FindStatuettes, quests, MM45Bits.Quest.FindStatuettes, "the statuette", MM45.Spots.Statuettes, "5 Levels", statistics);

            FindJewelOfAges.SetItems("the Key to the Great Eastern Tower", "the Jewel of the Ages");
            AddSideQuest(FindJewelOfAges, quests, MM45Bits.Quest.FindJewelOfAges, "the jewel", new MapXY[] { MM45.Spots.EasternKey, MM45.Spots.JewelOfAges }, "Rejuvenation, 1000000 Exp", keys);

            IllusionKey.Postrequisites.Add(new QuestLocation("Pay 300 Gems", MM45.Spots.HighMagicKey));
            AddSideQuest(IllusionKey, quests, String.Empty, "Enchanted Key to Tower of High Magic", keys);

            // Mining quests

            MineGold.AddLocations(QuestLocation.Multi(1, MM45.Spots.Gold1a), QuestLocation.Multi(1, MM45.Spots.Gold1b), QuestLocation.Multi(1, MM45.Spots.Gold1c),
                QuestLocation.Multi(2, MM45.Spots.Gold2a), QuestLocation.Multi(2, MM45.Spots.Gold2b), QuestLocation.Multi(1, MM45.Spots.Gold1d),
                QuestLocation.Multi(2, MM45.Spots.Gold2c), QuestLocation.Multi(2, MM45.Spots.Gold2d), QuestLocation.Multi(2, MM45.Spots.Gold2e),
                QuestLocation.Multi(3, MM45.Spots.Gold3a), QuestLocation.Multi(1, MM45.Spots.Gold1e), QuestLocation.Multi(1, MM45.Spots.Gold1f),
                QuestLocation.Multi(1, MM45.Spots.Gold1g), QuestLocation.Multi(1, MM45.Spots.Gold1i), QuestLocation.Multi(1, MM45.Spots.Gold1k),
                QuestLocation.Multi(2, MM45.Spots.Gold2f), QuestLocation.Multi(3, MM45.Spots.Gold3b), QuestLocation.Multi(3, MM45.Spots.Gold3c),
                QuestLocation.Multi(3, MM45.Spots.Gold3d), QuestLocation.Multi(3, MM45.Spots.Gold3e), QuestLocation.Multi(4, MM45.Spots.Gold4a),
                QuestLocation.Multi(3, MM45.Spots.Gold3f), QuestLocation.Multi(4, MM45.Spots.Gold4b), QuestLocation.Multi(3, MM45.Spots.Gold3g),
                QuestLocation.Multi(3, MM45.Spots.Gold3h), QuestLocation.Multi(4, MM45.Spots.Gold4c), QuestLocation.Multi(2, MM45.Spots.Gold2g),
                QuestLocation.Multi(2, MM45.Spots.Gold2h), QuestLocation.Multi(2, MM45.Spots.Gold2i), QuestLocation.Multi(2, MM45.Spots.Gold2j),
                QuestLocation.Multi(2, MM45.Spots.Gold2k), QuestLocation.Multi(2, MM45.Spots.Gold2l), QuestLocation.Multi(3, MM45.Spots.Gold3i),
                QuestLocation.Multi(3, MM45.Spots.Gold3j), QuestLocation.Multi(3, MM45.Spots.Gold3k), QuestLocation.Multi(3, MM45.Spots.Gold3l),
                QuestLocation.Multi(3, MM45.Spots.Gold3m), QuestLocation.Multi(3, MM45.Spots.Gold3n), QuestLocation.Multi(3, MM45.Spots.Gold3o),
                QuestLocation.Multi(4, MM45.Spots.Gold4d), QuestLocation.Multi(4, MM45.Spots.Gold4e), QuestLocation.Multi(4, MM45.Spots.Gold4f),
                QuestLocation.Multi(3, MM45.Spots.Gold3p), QuestLocation.Multi(3, MM45.Spots.Gold3q), QuestLocation.Multi(3, MM45.Spots.Gold3r),
                QuestLocation.Multi(3, MM45.Spots.Gold3s), QuestLocation.Multi(4, MM45.Spots.Gold4g), QuestLocation.Multi(4, MM45.Spots.Gold4h),
                QuestLocation.Multi(4, MM45.Spots.Gold4i), QuestLocation.Multi(4, MM45.Spots.Gold4j), QuestLocation.Multi(5, MM45.Spots.Gold5a),
                QuestLocation.Multi(5, MM45.Spots.Gold5f), QuestLocation.Multi(5, MM45.Spots.Gold5b), QuestLocation.Multi(5, MM45.Spots.Gold5c),
                QuestLocation.Multi(5, MM45.Spots.Gold5d), QuestLocation.Multi(5, MM45.Spots.Gold5e));
            AddSideQuest(MineGold, quests, String.Empty, "59540-81000 Gold", mining);
            CollectRubies.Prerequisites.Add(new QuestLocation("Talk to Linus", MM45.Spots.Linus));
            AddSideQuest(CollectRubies, quests, "Linus", "20-59 Ruby Rocks, 0-5300 Gems", mining);
            CollectEmeralds.Prerequisites.Add(new QuestLocation("Talk to Simon", MM45.Spots.Simon));
            AddSideQuest(CollectEmeralds, quests, "Simon", "21-76 Emerald Rocks, 0-6500 Gems", mining);
            CollectSapphires.Prerequisites.Add(new QuestLocation("Talk to Toby", MM45.Spots.Toby));
            AddSideQuest(CollectSapphires, quests, "Toby", "11-39 Sapphire Rocks, 0-2800 Gems", mining);
            CollectDiamonds.Prerequisites.Add(new QuestLocation("Talk to Hector", MM45.Spots.Hector));
            AddSideQuest(CollectDiamonds, quests, "Hector", "14-41 Diamond Rocks, 0-2900 Gems", mining);

            // Defeat enemies quests

            AddSideQuest(DefeatDwarfKing, quests, MM45Bits.Quest.SlayMadDwarfKing, "the Clan King", MM45.Spots.ClanKing, "50000 Exp", kill);
            SaveWinterkill.Prerequisites.Add(new QuestLocation("Talk to Randon", MM45.Spots.Randon));
            for (int i = 0; i < 20; i++)
                SaveWinterkill.MainObjectives.Add(new QuestLocation("Kill the Spirit Bones", SaveWinterkill.Location(i)));
            SaveWinterkill.MainObjectives.Add(new QuestLocation("Bang the Gong", SaveWinterkill.Location(20)));
            SaveWinterkill.MainObjectives.Add(new QuestLocation("Talk to Randon", MM45.Spots.Randon));
            for (int i = 22; i < 42; i++)
                SaveWinterkill.MainObjectives.Add(new QuestLocation("Kill the Polter-Fool", SaveWinterkill.Location(i)));
            SaveWinterkill.MainObjectives.Add(new QuestLocation("Bang the Gong", SaveWinterkill.Location(42)));
            SaveWinterkill.MainObjectives.Add(new QuestLocation("Talk to Randon", MM45.Spots.Randon));
            for (int i = 44; i < 64; i++)
                SaveWinterkill.MainObjectives.Add(new QuestLocation("Kill the Ghost Rider", SaveWinterkill.Location(i)));
            SaveWinterkill.MainObjectives.Add(new QuestLocation("Bang the Gong", SaveWinterkill.Location(64)));
            SaveWinterkill.Postrequisites.Add(new QuestLocation("Report to Randon", MM45.Spots.Randon));
            AddSideQuest(SaveWinterkill, quests, "Randon", "500000 Exp", kill);
            DefeatDwarfKing.PreQuest.Add(AddSideQuest(RidVertigoOfPests, quests, MM45Bits.Quest.RidVertigoOfPests, "Joe's invoice", MM45.Spots.JoesInvoice, "5000 Exp, 4000 Gold, 50 Gems", fetch));
            StopGettlewaith.AddLocations("Talk to Mayor Snarfblad", MM45.Spots.Snarfblad, "Return to Mayor Snarfblad", MM45.Spots.Snarfblad,
                new QuestLocation("Talk to Gettlewaith", MM45.Spots.Gettlewaith),
                new QuestLocation("Defeat Gettlewaith", StopGettlewaith.Location(0)));
            AddSideQuest(StopGettlewaith, quests, "Mayor Snarfblad", "50000 Exp, 10000 Gold", kill);

            // Rescue people quests

            AddSideQuest(FreeCelia, quests, MM45Bits.Quest.FreeCelia, "Celia", MM45.Spots.Celia, "25000 Exp, 2000 Gold", rescue);
            RescueSprite.PreQuest.Add(questMelons);
            AddSideQuest(RescueSprite, quests, MM45Bits.Quest.RescueSprite, "Sheewana", MM45.Spots.Sheewana, "2 Energy Disks, 250000 Exp, 40000 Gold, 200 Gems", rescue);
            AddSideQuest(ReleaseJethrosBrother, quests, MM45Bits.Quest.ReleaseJethrosBrother, "Jasper", MM45.Spots.Jasper, "10000 Exp, 6500 Gold", rescue);

            // Retrieve an item (or items) quests

            GatherPhirnaRoots.ReturnToGiver = false;
            AddSideQuest(GatherPhirnaRoots, quests, MM45Bits.Quest.GatherPhirnaRoot, "a Phirna Root", MM45.Spots.PhirnaRoots, "50 Potions of Antidotes", fetch);
            GatherPhirnaRoots.MainObjectives.Add(new QuestLocation("Give the Phirna Root to Myra", MM45.Spots.Myra));
            SkillCrusader.PreQuest.Add(AddSideQuest(RetrieveAlacorn, quests, MM45Bits.Quest.RetrieveAlacorn, "the Alacorn", MM45.Spots.Alacorn, "Crusader skill, 60000 Exp", fetch));
            AddSideQuest(FindOrothinsWhistle, quests, MM45Bits.Quest.FindOrothinsWhistle, "the bone whistle", MM45.Spots.OrothinsWhistle, "Cure Disease/Poison spells, 15000 Exp", fetch);
            AddSideQuest(FindLigonosSkull, quests, MM45Bits.Quest.FindLigonosSkull, "Ligono's skull", MM45.Spots.LigonosSkull, "Recharge Item spell, 40000 Exp", fetch);
            AddSideQuest(GetBaroksPendant, quests, MM45Bits.Quest.GetBaroksPendant, "the pendant", MM45.Spots.BaroksPendant, "Enchant Item spell, 80000 Exp", fetch);
            AddSideQuest(GetRoxannesTiara, quests, MM45Bits.Quest.GetRoxannesTiara, "the tiara", MM45.Spots.RoxannesTiara, "200000 Exp, 12325 Gold, 192 Gems, 4 L3 Items", fetch);
            AddSideQuest(ReturnScarab, quests, MM45Bits.Quest.ReturnScarab, "the scarab", MM45.Spots.ImagingScarab, "Moon Ray spell, 75000 Exp", fetch);

            AspWell.Prerequisites.Add(new QuestLocation("Make the pedestal blue", MM45.Spots.AspPedestal1));
            AspWell.AddLocations(new QuestLocation("Make the pedestal red", MM45.Spots.AspPedestal2),
                new QuestLocation("Make the pedestal blue", MM45.Spots.AspPedestal4),
                new QuestLocation("Make the pedestal red", MM45.Spots.AspPedestal3),
                new QuestLocation("Destroy the transformer", MM45.Spots.SnakeTransformer));
            AspWell.Postrequisites.Add(new QuestLocation("Restore HP using the clean well", MM45.Spots.AspWell));
            ReturnCrystals.PreQuest.Add(AddSideQuest(AspWell, quests, String.Empty, String.Empty, puzzles));
            AddSideQuest(ReturnCrystals, quests, MM45Bits.Quest.ReturnCrystals, "the crystals", MM45.Spots.PiezoCrystals, "Mega Volts spell, 75000 Exp", fetch);

            AddSideQuest(FindFaeryWand, quests, MM45Bits.Quest.FindFaeryWand, "the wand", MM45.Spots.FaeryWand, "40000 Exp, 25000 Gold", fetch);
            AddSideQuest(FindHolyBook, quests, MM45Bits.Quest.FindHolyBook, "the book ", MM45.Spots.HolyBook, "60000 Exp, 25000 Gold", fetch);
            AddSideQuest(FindDragonEgg, quests, MM45Bits.Quest.FindDragonEgg, "the dragon egg", MM45.Spots.DragonEgg, "1000000 Exp", fetch);
            AddSideQuest(FindVesparsHandle, quests, MM45Bits.Quest.FindVesparsHandle, "the handle", MM45.Spots.VesparsHandle, "Pass to Sandcaster, 200000 Exp", fetch);
            AddSideQuest(FindChalice, quests, MM45Bits.Quest.FindChalice, "the chalice", MM45.Spots.ProtectionChalice, "1000000 Exp, 100000 Gold", fetch);
            AddSideQuest(FindEctorsRing, quests, MM45Bits.Quest.FindEctorsRing, "the ring", MM45.Spots.EctorsRing, "Obsidian Battle Axe, 500000 Exp", fetch);
            AddSideQuest(FindCalebsGlass, quests, MM45Bits.Quest.FindCalebsGlass, "the glass", MM45.Spots.CalebsGlass, "500000 Exp, 50000 Gold", fetch);

            // Destroy a location quests

            AddSideQuest(DestroyTrollLair, quests, MM45Bits.Quest.DestroyTrollLair, "the lair", MM45.Spots.TrollLair, "40000 Exp, 20000 Gold", destroy);
            AddSideQuest(DestroyOgreLair, quests, MM45Bits.Quest.DestroyOgreLair, "the lair", MM45.Spots.OgreLair, "Super Shelter spell, 75000 Exp", destroy);
            AddSideQuest(SlayLakeMonsters, quests, MM45Bits.Quest.SlayLakeMonsters, "the Water Dragon", SlayLakeMonsters.LocationsOverride, "100000 Exp, L6 Item", destroy);
            AddSideQuest(DestroyCyclopsLair, quests, MM45Bits.Quest.DestroyCyclopsLair, "the lair", MM45.Spots.CyclopsOutpost, "100000 Exp, 7 L2 Items", destroy);
            AddSideQuest(RetrieveScrollOfInsight, quests, MM45Bits.Quest.RetrieveScrollOfInsight, "the scroll", MM45.Spots.InsightScroll, "Jeweled Amulet of the Northern Sphinx, 750000 Exp", fetch);
            AddSideQuest(DestroyOgres, quests, MM45Bits.Quest.DestroyOgres, "the fort", MM45.Spots.OgreForts, "150000 Exp, 150000 Gold", destroy);

            // Main quests

            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Purchase NewCastle (50000 Gold)", MM45.Spots.NewCastle));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Obtain the Yak Stone of Opening", MM45.Spots.Mirabeth));
            foreach(MapXY map in MM45.Spots.YakMegaCredits)
                FindXeenSlayer.MainObjectives.Add(new QuestLocation("Steal a King's MegaCredit", map));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Build a wall around NewCastle", MM45.Spots.Emerson));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Obtain the Stone of a Thousand Terrors", MM45.Spots.NewCastle));
            foreach (MapXY map in MM45.Spots.TombMegaCredits)
                FindXeenSlayer.MainObjectives.Add(new QuestLocation("Retrieve a King's MegaCredit", map));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Build a keep in NewCastle", MM45.Spots.Emerson));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Obtain the Golem Stone of Admittance", MM45.Spots.NewCastle));
            foreach (MapXY map in MM45.Spots.GolemMegaCredits)
                FindXeenSlayer.MainObjectives.Add(new QuestLocation("Retrieve a King's MegaCredit", map));
            FindXeenSlayer.MainObjectives.Add(new QuestLocation("Clean out the NewCastle dungeon", MM45.Spots.Emerson));
            FindXeenSlayer.Postrequisites.Add(new QuestLocation("Retrieve the Xeen Slayer Sword", MM45.Spots.XeenSlayerSword));

            XeenDoll.AddLocations(new QuestLocation("Obtain a Might Doll (100 Gold/chance)", MM45.Spots.MightDoll),
                new QuestLocation("Obtain a Speed Doll (1000 Gold/chance)", MM45.Spots.SpeedDoll),
                new QuestLocation("Obtain an Endurance Doll (10 Gems/chance)", MM45.Spots.EnduranceDoll),
                new QuestLocation("Obtain an Accuracy Doll (100 Gems/chance)", MM45.Spots.AccuracyDoll));
            XeenDoll.Postrequisites.Add(new QuestLocation("Obtain a Lord Xeen Cupie Doll", MM45.Spots.XeenDoll));

            int iMainSort = 1;

            FindXeenSlayer.PreQuest.Add(AddMainQuest(iMainSort++, FreeCrodo, quests, MM45Bits.Quest.FreeCrodo, "Crodo", MM45.Spots.Crodo, "Excavation Permit"));
            SlayLordXeen.PreQuest.Add(AddMainQuest(iMainSort++, FindXeenSlayer, quests, "Crodo"));
            SlayLordXeen.PreQuest.Add(AddMainQuest(iMainSort++, XeenDoll, quests));
            SlayLordXeen.Postrequisites.Add(new QuestLocation("Defeat Lord Xeen", SlayLordXeen.Location(0)));
            BasicQuest slayXeen = AddMainQuest(iMainSort++, SlayLordXeen, quests, "Crodo");
            FindMirror.PreQuest.Add(slayXeen);
            AddSideQuest(FindMirror, quests, MM45Bits.Quest.FindMirror, "the Sixth Mirror", MM45.Spots.SixthMirror);

            EnterBlackfang.AddLocations("Talk to Dimitri", MM45.Spots.Dimitri, "Return to Ambrose", MM45.Spots.Ambrose,
                new QuestLocation("Tell Ambrose that \"Dimitri\" sent you", MM45.Spots.Ambrose),
                new QuestLocation("Pay the enchantress to enchant the bridle (50000 Gold)", MM45.Spots.Enchantress));

            LostSouls.Prerequisites.Add(new QuestLocation("Obtain the key to the Dungeon of Lost Souls", MM45.Spots.Megan));
            foreach (MapXY map in MM45.Spots.SoulGlasses)
                LostSouls.MainObjectives.Add(new QuestLocation("Flip the hourglass", map));
            foreach (MapXY map in MM45.Spots.SoulWater)
                LostSouls.MainObjectives.Add(new QuestLocation("Drink from any of the central fountains", map));
            foreach (MapXY map in MM45.Spots.SoulLevers)
                LostSouls.MainObjectives.Add(new QuestLocation("Pull the lever", map));
            LostSouls.AddLocations(new QuestLocation("Pull the lever", MM45.Spots.SoulLever7));
            LostSouls.AddLocations(new QuestLocation("Pull the lever", MM45.Spots.SoulLever11));
            LostSouls.AddLocations(new QuestLocation("Pay 266000 Gold", MM45.Spots.SoulPay));

            FindSongbird.AddLocations("Talk to Megan", MM45.Spots.Megan, "Return to Megan", MM45.Spots.Megan,
                new QuestLocation("Release the Songbird", MM45.Spots.Songbird),
                new QuestLocation("Bring the Songbird to Dimitri", MM45.Spots.Dimitri));
            FindSongbird.PreQuest.Add(AddMainQuest(iMainSort++, LostSouls, quests, String.Empty, String.Empty));
            BasicQuest questSongbird = AddMainQuest(iMainSort++, FindSongbird, quests, "Megan", "3000000 Exp, 1000 Gold");

            EnterBlackfang.PreQuest.Add(questSongbird);
            SaveKalindra.PreQuest.Add(AddMainQuest(iMainSort++, EnterBlackfang, quests, "Dimitri", "500000 Exp, 50000 Gold"));
            SaveKalindra.Prerequisites.Add(new QuestLocation("Talk to Queen Kalindra", MM45.Spots.Kalindra));
            SaveKalindra.MainObjectives.Add(new QuestLocation("Open Kalindra's safe", MM45.Spots.KalindraSafe));
            SaveKalindra.Postrequisites.Add(new QuestLocation("Give the crown to Queen Kalindra", MM45.Spots.Kalindra));
            DefeatAlamar.PreQuest.Add(AddMainQuest(iMainSort++, SaveKalindra, quests, "Dimitri", "Key to Ancient Pyramid, 5000000 Exp"));

            AlamarClock.AddLocations(new QuestLocation("Set the Fire Time sundial to 9", MM45.Spots.FireTime),
                new QuestLocation("Set the Air Time sundial to 9", MM45.Spots.AirTime),
                new QuestLocation("Set the Earth Time sundial to 9", MM45.Spots.EarthTime),
                new QuestLocation("Set the Water Time sundial to 9", MM45.Spots.WaterTime));
            AlamarClock.Postrequisites.Add(new QuestLocation("Answer \"Sheltem\"", MM45.Spots.TimeSkull));

            questMelons.SortOrder = iMainSort++;
            FindEnergyDisks.PreQuest.Add(AddMainQuest(iMainSort++, FindNadiasNecklace, quests, MM45Bits.Quest.FindNadiasNecklace, 
                "the necklace", MM45.Spots.NadiasNecklace, "Key to Ellinger's Tower, 100000 Exp"));
            EndXenocsReign.SetItems("Xenoc", "Morgana");
            AddSideQuest(EndXenocsReign, quests, MM45Bits.Quest.EndXenocsReign, "the monster", EndXenocsReign.LocationsOverride, "2000000 Exp, 1000 Gems", kill);

            FindEnergyDisks.PreQuest.Add(questSongbird);
            FindEnergyDisks.PreQuest.Add(questMelons);
            FindEnergyDisks.SetItems("an Energy Disk", 16, "2 Energy Disks", "2 Energy Disks", "2 Energy Disks", "3 Energy Disks");
            FindEnergyDisks.Prerequisites.Add(new QuestLocation("Talk to Ellinger", MM45.Spots.Ellinger));
            for (int i = 0; i < 16; i++)
                FindEnergyDisks.MainObjectives.Add(new QuestLocation("Retrieve an Energy Disk", MM45.Spots.EnergyDisks[i]));
            for (int i = 16; i < 19; i++)
                FindEnergyDisks.MainObjectives.Add(new QuestLocation("Retrieve 2 Energy Disks", MM45.Spots.EnergyDisks[i]));
            FindEnergyDisks.MainObjectives.Add(new QuestLocation("Retrieve 3 Energy Disks", MM45.Spots.EnergyDisks[19]));
            FindEnergyDisks.MainObjectives.Add(new QuestLocation("Deliever the first five energy disks", MM45.Spots.Ellinger));
            FindEnergyDisks.MainObjectives.Add(new QuestLocation("Deliever the second five energy disks", MM45.Spots.Ellinger));
            FindEnergyDisks.MainObjectives.Add(new QuestLocation("Deliever the third five energy disks", MM45.Spots.Ellinger));
            FindEnergyDisks.Postrequisites.Add(new QuestLocation("Deliever the last five energy disks", MM45.Spots.Ellinger));
            AddMainQuest(iMainSort++, FindEnergyDisks, quests, "Ellinger", "Access to Castle Kalindra");

            foreach (MapXY map in MM45.Spots.PyramidTorches)
                PyramidTorches.MainObjectives.Add(new QuestLocation("Pull the lever", map));

            DefeatAlamar.AddLocations("Talk to Dragon Pharaoh", MM45.Spots.DragonPharaoh, "Confront Lord Alamar", MM45.Spots.Alamar,
                new QuestLocation("Talk to Corak", MM45.Spots.Corak),
                new QuestLocation("Report back to Dragon Pharaoh", MM45.Spots.DragonPharaoh),
                new QuestLocation("Retrieve the Soul Box", MM45.Spots.SoulBox),
                new QuestLocation("Place Corak's Soul in the Soul Box", MM45.Spots.Corak));
            DefeatAlamar.PreQuest.Add(AddMainQuest(iMainSort++, AlamarClock, quests, String.Empty, String.Empty));
            UniteWorlds.PreQuest.Add(AddMainQuest(iMainSort++, DefeatAlamar, quests, "Dragon Pharaoh"));
            UniteWorlds.PreQuest.Add(AddSideQuest(PyramidTorches, quests, String.Empty, String.Empty, puzzles));

            UniteWorlds.PreQuest.Add(AddMainQuest(iMainSort++, FindEverhotRock, quests, MM45Bits.Quest.FindEverhotRock, "the rock", MM45.Spots.EverhotRock, "Widget, 150000 Exp"));

            UniteWorlds.PreQuest.Add(slayXeen);
            UniteWorlds.AddLocations("Talk to Dragon Pharaoh", MM45.Spots.DragonPharaoh, "Talk to Dragon Pharaoh", MM45.Spots.DragonPharaoh2,
                new QuestLocation("Wake the Water Sleeper", MM45.Spots.WaterSleeper),
                new QuestLocation("Wake the Air Sleeper", MM45.Spots.AirSleeper),
                new QuestLocation("Wake the Fire Sleeper", MM45.Spots.FireSleeper),
                new QuestLocation("Wake the Earth Sleeper", MM45.Spots.EarthSleeper),
                new QuestLocation("Activate the Water Reflector", MM45.Spots.WaterReflector),
                new QuestLocation("Activate the Air Reflector", MM45.Spots.AirReflector),
                new QuestLocation("Activate the Fire Reflector", MM45.Spots.FireReflector),
                new QuestLocation("Activate the Earth Reflector", MM45.Spots.EarthReflector),
                new QuestLocation("Obtain the Key to Dragon Tower", MM45.Spots.DragonTowerKey),
                new QuestLocation("Obtain the Silver ID Card", MM45.Spots.SilverID),
                new QuestLocation("Obtain the Key to Darkstone Tower", MM45.Spots.DarkstoneTowerKey),
                new QuestLocation("Obtain the Gold ID Card", MM45.Spots.GoldID),
                new QuestLocation("Free Roland", MM45.Spots.Roland),
                new QuestLocation("Obtain the Chime of Opening", MM45.Spots.Chime));
            AddMainQuest(iMainSort++, UniteWorlds, quests, "Dragon Pharaoh");

            // Uncategorized quests


            Guilds.AddLocations(new QuestLocation("Join the Asp Guild", MM45.Spots.AwardAspGuildMember),
                new QuestLocation("Join the Castleview Guild", MM45.Spots.AwardCastleviewGuildMember),
                new QuestLocation("Join the Lakeside Guild", MM45.Spots.AwardLakesideGuildMember),
                new QuestLocation("Join the Necropolis Guild", MM45.Spots.AwardNecropolisGuildMember),
                new QuestLocation("Join the Nightshadow Guild", MM45.Spots.AwardNightshadowGuildMember),
                new QuestLocation("Join the Olympus Guild", MM45.Spots.AwardOlympusGuildMember),
                new QuestLocation("Join the Rivercity Guild", MM45.Spots.AwardRivercityGuildMember),
                new QuestLocation("Join the Sandcaster Guild", MM45.Spots.AwardSandcasterGuildMember),
                new QuestLocation("Join the ShangriLa Guild", MM45.Spots.AwardShangriLaGuildMember),
                new QuestLocation("Join the Vertigo Guild", MM45.Spots.AwardVertigoGuildMember),
                new QuestLocation("Join the Winterkill Guild", MM45.Spots.AwardWinterkillGuildMember));
            AddSideQuest(Guilds, quests, String.Empty);

            TempStats.AddLocations(new QuestLocation("(+50 Level, no note) Drink from the Well", MM45.Spots.WellFantasticSkill),
                new QuestLocation("(+50 Energy Res) Pray at the Shrine", MM45.Spots.ShrineEnergy),
                new QuestLocation("(+30 AC) Drink from the Fountain", MM45.Spots.FountainGreatProtection1),
                new QuestLocation("(+50 Cold Res) Pray at the Shrine", MM45.Spots.ShrineCold),
                new QuestLocation("(+50 Intellect) Drink from the Well", MM45.Spots.WellIntellect),
                new QuestLocation("(+50 Accuracy) Drink from the Well", MM45.Spots.WellAccuracy),
                new QuestLocation("(+50 Endurance) Drink from the Well", MM45.Spots.WellEndurance),
                new QuestLocation("(+10 Mgt/End/Spd/Acy) Drink from the Fountain", MM45.Spots.OlympianFountain),
                new QuestLocation("(+50 Personality) Drink from the Personality", MM45.Spots.WellPersonality),
                new QuestLocation("(+50 Magic Res) Pray at the Shrine", MM45.Spots.ShrineMagic),
                new QuestLocation("(+50 Might) Drink from the Well", MM45.Spots.WellMight1),
                new QuestLocation("(+10 Int/Per) Drink from the Fountain", MM45.Spots.SageFountain),
                new QuestLocation("(+50 Electrical Res) Pray at the Shrine", MM45.Spots.ShrineElectricity),
                new QuestLocation("(+50 Speed) Drink from the Well", MM45.Spots.WellSpeed),
                new QuestLocation("Pray at the Shrine (+50 Fire Res", MM45.Spots.ShrineFire),
                new QuestLocation("(+20 Elemental Res) Pray at the Shrine", MM45.Spots.ElementalShrine),
                new QuestLocation("(+50 Elemental Res) Drink from the Well", MM45.Spots.ElementalWell),
                new QuestLocation("(+100 Elemental Res) Bathe in the Fountain", MM45.Spots.ElementalFountain),
                new QuestLocation("(+5 AC) Drink from the Fountain", MM45.Spots.FountainProtection),
                new QuestLocation("(+60 Luck) Toss a coin in the Well", MM45.Spots.WishingWell1),
                new QuestLocation("(+50 Poison Res) Pray at the Shrine", MM45.Spots.ShrinePoison),
                new QuestLocation("(+5 Level) Drink from the Fountain", MM45.Spots.FountainAbility),
                new QuestLocation("(+10 Level) Drink from the Well", MM45.Spots.NightshadowWell),
                new QuestLocation("(+50 Might) Drink from the Well", MM45.Spots.WinterkillWell),
                new QuestLocation("(+3 Level) Pray at the Shrine", MM45.Spots.LevelShrine),
                new QuestLocation("(+25 Might) Drink from the Well", MM45.Spots.WellMight2),
                new QuestLocation("(+10 AC) Drink from the Well", MM45.Spots.WellProtection),
                new QuestLocation("(+100 Luck) Toss a coin in the Wishing", MM45.Spots.WishingWell2),
                new QuestLocation("(+50 Energy Res, no note) Drink from the Fountain", MM45.Spots.FountainEnergyResistance),
                new QuestLocation("(+15 Level) Pray at the Shrine", MM45.Spots.ShrineGreatPower),
                new QuestLocation("(+100 Might) Bathe in the Fountain", MM45.Spots.FountainMight),
                new QuestLocation("(+50 Magic Res) Drink from the Fountain", MM45.Spots.FountainMagicResistance),
                new QuestLocation("(+50 AC) Bathe in the Fountain", MM45.Spots.FountainGreatProtection2),
                new QuestLocation("(+250 HP) Drink from the Waters", MM45.Spots.WatersGreatPower),
                new QuestLocation("(+25 HP) Drink from the Waters", MM45.Spots.WatersPower),
                new QuestLocation("(+100 HP) Drink from the Well", MM45.Spots.AspWell),
                new QuestLocation("(+2500 HP) Drink from the Fountain", MM45.Spots.FountainSuperResilience),
                new QuestLocation("(+50 HP) Drink from the Well", MM45.Spots.WellResilience),
                new QuestLocation("(+500 HP) Drink from the Fountain", MM45.Spots.FountainGreatResilience),
                new QuestLocation("(+250 SP) Drink from the Waters", MM45.Spots.WatersGreatMagic),
                new QuestLocation("(+25 SP) Drink from the Waters", MM45.Spots.WatersMagic),
                new QuestLocation("(+100 SP) Drink from the Well", MM45.Spots.RivercityWell),
                new QuestLocation("(+50 SP) Drink from the Well", MM45.Spots.WellSpells),
                new QuestLocation("(+1000 SP) Bathe in the Fountain", MM45.Spots.FountainGreatMagic),
                new QuestLocation("(+1800 SP, no note) Face south at 12:00 PM", MM45.Spots.SandcasterSpells));
            AddSideQuest(TempStats, quests, String.Empty);

            DestroyThings.AddLocations(new QuestLocation("Burn the Barbarian Archers' camp", MM45.Spots.BarbarianCamp1),
                new QuestLocation("Burn the Barbarian camp", MM45.Spots.BarbarianCamp2),
                new QuestLocation("Burn the Cyclops' outpost", MM45.Spots.CyclopsOutpost),
                new QuestLocation("Destroy the Barbarian camp", MM45.Spots.BarbarianCamp3),
                new QuestLocation("Destroy the Barbarian camp", MM45.Spots.BarbarianCamp4),
                new QuestLocation("Destroy the Troll Lair", MM45.Spots.TrollLair),
                new QuestLocation("Burn the Archer's camp", MM45.Spots.ArcherCamp),
                new QuestLocation("Destroy the Giant's lair", MM45.Spots.GiantLair1),
                new QuestLocation("Burn the Barbarian encampment", MM45.Spots.BarbarianEncampment),
                new QuestLocation("Destroy the Ogre lair", MM45.Spots.OgreLair),
                new QuestLocation("Destroy the Evil Ranger camp", MM45.Spots.RangerCamp),
                new QuestLocation("Destroy the Giant's lair", MM45.Spots.GiantLair2),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest1),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest2),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest3),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest4),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest5),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest6),
                new QuestLocation("Destroy the Roc Nest", MM45.Spots.RocNest7),
                new QuestLocation("Burn the Ogre fort", MM45.Spots.OgreFort1),
                new QuestLocation("Burn the Ogre fort", MM45.Spots.OgreFort2),
                new QuestLocation("Smash the demon skull", MM45.Spots.DemonSkull),
                new QuestLocation("Smash the devil skull", MM45.Spots.DevilSkull),
                new QuestLocation("Burn the Orc outpost", MM45.Spots.OrcOutpost1),
                new QuestLocation("Destroy the Orc observation post", MM45.Spots.OrcPost),
                new QuestLocation("Destroy the Shrine of Great Evil", MM45.Spots.ShrineGreat),
                new QuestLocation("Destroy the Shrine of Lesser Evil", MM45.Spots.ShrineLesser),
                new QuestLocation("Destroy the Shrine of Minor Evil", MM45.Spots.ShrineMinor),
                new QuestLocation("Destroy the Lord Xeen Machine", MM45.Spots.XeenMachine1),
                new QuestLocation("Destroy the Lord Xeen Machine", MM45.Spots.XeenMachine2),
                new QuestLocation("Destroy the Lord Xeen Machine", MM45.Spots.XeenMachine3),
                new QuestLocation("Destroy the Lord Xeen Machine", MM45.Spots.XeenMachine4),
                new QuestLocation("Destroy the Altar", MM45.Spots.Altar1),
                new QuestLocation("Destroy the Altar", MM45.Spots.Altar2),
                new QuestLocation("Destroy the Altar", MM45.Spots.Altar3),
                new QuestLocation("Destroy the Altar", MM45.Spots.Altar4),
                new QuestLocation("Desecrate the Undead shrine", MM45.Spots.UndeadShrine),
                new QuestLocation("Destroy the Gargoyle lair", MM45.Spots.GargoyleLair),
                new QuestLocation("Pillage the Orc cave", MM45.Spots.OrcCave1),
                new QuestLocation("Pillage the Orc cave", MM45.Spots.OrcCave2),
                new QuestLocation("Burn the Orc outpost", MM45.Spots.OrcOutpost2),
                new QuestLocation("Destroy the Harpy nest", MM45.Spots.HarpyNest1),
                new QuestLocation("Destroy the Harpy nest", MM45.Spots.HarpyNest2),
                new QuestLocation("Destroy the Harpy nest", MM45.Spots.HarpyNest3),
                new QuestLocation("Destroy the Harpy nest", MM45.Spots.HarpyNest4),
                new QuestLocation("Destroy the Harpy nest", MM45.Spots.HarpyNest5));
            AddSideQuest(DestroyThings, quests, String.Empty, "29673000 Exp, 17900 Gold, 50 Gems, 15/9/6 L1/3/4 Items");

            Experience.AddLocations(new QuestLocation("Read the Book of the Dead Vol. 1", MM45.Spots.DeadBook1),
                new QuestLocation("Read the Book of the Dead Vol. 2", MM45.Spots.DeadBook2),
                new QuestLocation("Read the Book of the Dead Vol. 3", MM45.Spots.DeadBook3),
                new QuestLocation("Read the Book of the Dead Vol. 4", MM45.Spots.DeadBook4),
                new QuestLocation("Read the Book of the Dead Vol. 5", MM45.Spots.DeadBook5),
                new QuestLocation("Read the Book of the Dead Vol. 6", MM45.Spots.DeadBook6),
                new QuestLocation("Read the Book of the Dead Vol. 7", MM45.Spots.DeadBook7),
                new QuestLocation("Read the Book of the Dead Vol. 8", MM45.Spots.DeadBook8),
                new QuestLocation("Read the Book of the Dead Vol. 9", MM45.Spots.DeadBook9),
                new QuestLocation("Read the Manual of Master Thievery", MM45.Spots.MasterThievery),
                new QuestLocation("Read the Tome of Great Experience", MM45.Spots.ExpTome1),
                new QuestLocation("Read the Tome of Great Experience", MM45.Spots.ExpTome2),
                new QuestLocation("Read the Book of Directives", MM45.Spots.Directives));
            AddSideQuest(Experience, quests, String.Empty, "39499991 Exp");

            XeenTraps.AddLocations(new QuestLocation("Destroy the Poison Generator", MM45.Spots.PoisonGenerator),
                new QuestLocation("Destroy the Cold Generator", MM45.Spots.ColdGenerator),
                new QuestLocation("Destroy the Fire Generator", MM45.Spots.FireGenerator),
                new QuestLocation("Destroy the Electricity Generator", MM45.Spots.ElectricityGenerator),
                new QuestLocation("Destroy the Guard Making Machine", MM45.Spots.GuardGenerator));
            AddSideQuest(XeenTraps, quests, String.Empty, "1000000 Exp");

            Lamps.AddLocations(new QuestLocation("Rub the lamp", MM45.Spots.LampA2SA21214),
                new QuestLocation("Rub the lamp", MM45.Spots.LampB1SB10507),
                new QuestLocation("Rub the lamp", MM45.Spots.LampB2SB20514),
                new QuestLocation("Rub the lamp", MM45.Spots.LampC3SC30700),
                new QuestLocation("Rub the lamp", MM45.Spots.LampE1SE11201),
                new QuestLocation("Rub the lamp", MM45.Spots.LampE2SE20812),
                new QuestLocation("Rub the lamp", MM45.Spots.LampE2SE20308),
                new QuestLocation("Rub the lamp", MM45.Spots.LampE2SE21208),
                new QuestLocation("Rub the lamp", MM45.Spots.LampE2SE20803),
                new QuestLocation("Rub the lamp", MM45.Spots.LampF1SF10303),
                new QuestLocation("Rub the lamp", MM45.Spots.LampF2SF20112),
                new QuestLocation("Rub the lamp", MM45.Spots.LampB2DS1309),
                new QuestLocation("Rub the lamp", MM45.Spots.LampB2DS1202),
                new QuestLocation("Rub the lamp", MM45.Spots.LampC2DS0315),
                new QuestLocation("Rub the lamp", MM45.Spots.LampC2DS0611),
                new QuestLocation("Rub the lamp", MM45.Spots.LampC2DS0206),
                new QuestLocation("Rub the lamp", MM45.Spots.LampD2DS0015),
                new QuestLocation("Rub the lamp", MM45.Spots.LampD3DS0712));
            AddSideQuest(Lamps, quests, String.Empty, "Misc Gold/Gems/Exp");

            Level7Items.AddLocations(new QuestLocation("Collect the treasure (2 items)", MM45.Spots.L7ItemE1DC3100),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL10518),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL10516),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL10514),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL10201),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL10401),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL11001),
                new QuestLocation("Open the sarcophagus (1 item)", MM45.Spots.L7ItemA2SSL11201),
                new QuestLocation("Collect the treasure (10 items)", MM45.Spots.L7ItemA2SSL30615),
                new QuestLocation("Search the coffin (5 items)", MM45.Spots.L7ItemE3DDL22613),
                new QuestLocation("Search the coffin (5 items)", MM45.Spots.L7ItemE3DDL23013),
                new QuestLocation("Open the coffin (5 items)", MM45.Spots.L7ItemE3DDL20704),
                new QuestLocation("Open the coffin (5 items)", MM45.Spots.L7ItemE3DDL20904),
                new QuestLocation("Open the coffin (5 items)", MM45.Spots.L7ItemE3DDL21104),
                new QuestLocation("Open the coffin (5 items)", MM45.Spots.L7ItemE3DDL21304),
                new QuestLocation("Open the coffin (5 items)", MM45.Spots.L7ItemE3DDL21504),
                new QuestLocation("Open the chest (3 items)", MM45.Spots.L7ItemD1DTL40509),
                new QuestLocation("Open the chest (3 items)", MM45.Spots.L7ItemD1DTL40909),
                new QuestLocation("Collect the treasure (2 items)", MM45.Spots.L7ItemD1DTL40308),
                new QuestLocation("Collect the treasure (2 items)", MM45.Spots.L7ItemD1DTL41108),
                new QuestLocation("Open the chest (10 items)", MM45.Spots.L7ItemD1DTL40706),
                new QuestLocation("Open the chest (3 items)", MM45.Spots.L7ItemC4TBL51611),
                new QuestLocation("Collect the treasure (5 items)", MM45.Spots.L7ItemD2TGPL12315),
                new QuestLocation("Defeat Hobstatd (2 items)", MM45.Spots.L7ItemE4TH0724),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N0105),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N0205),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1204),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1404),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1203),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1403),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1002),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1402),
                new QuestLocation("Open the coffin (1 item)", MM45.Spots.L7ItemB2N1401),
                new QuestLocation("Search the sewer drain (1 item)", MM45.Spots.L7ItemB2NS0610),
                new QuestLocation("Collect the Mummy's Secret Treasure (5 items)", MM45.Spots.L7ItemB2NS0106),
                new QuestLocation("Search the sewer drain (1 item)", MM45.Spots.L7ItemB2NS0102),
                new QuestLocation("Search the sewer drain (1 item)", MM45.Spots.L7ItemB2NS1001));
            AddSideQuest(Level7Items, quests, String.Empty, "101 Level 7 Items");

            AddSideQuest(ReclaimPagoda, quests, MM45Bits.Quest.ReclaimPagoda, "the pagoda", MM45.Spots.Pagoda, "75000 Exp, 40 Food)");

            TurnSeasons.AddLocations("Talk to the Summer Druid", MM45.Spots.SummerDruid,
                new QuestLocation("Give the flower to the Autumn Druid", MM45.Spots.AutumnDruid),
                new QuestLocation("Give the leaf to the Winter Druid", MM45.Spots.WinterDruid),
                new QuestLocation("Give the snowflake to the Spring Druid", MM45.Spots.SpringDruid),
                new QuestLocation("Give the raindrop to the Summer Druid", MM45.Spots.SummerDruid));
            AddSideQuest(TurnSeasons, quests, "Druids", "150000 Exp, Rejuvenation");

            foreach (MapXY map in MM45.Spots.GoldTrees)
                TreasureTrees.MainObjectives.Add(new QuestLocation("Search the tree (1-10 Gold)", map));
            foreach (MapXY map in MM45.Spots.GemTrees)
                TreasureTrees.MainObjectives.Add(new QuestLocation("Search the tree (10 Gems", map));
            foreach (MapXY map in MM45.Spots.FoodTrees)
                TreasureTrees.MainObjectives.Add(new QuestLocation("Search the tree (1-6 Food)", map));
            foreach (MapXY map in MM45.Spots.ItemTrees)
                TreasureTrees.MainObjectives.Add(new QuestLocation("Search the tree (L1 Item)", map));
            AddSideQuest(TreasureTrees, quests, String.Empty, "26-260 Gold, 60 Gems, 12-72 Food, 13 L1 Items");

            foreach (MapXY map in MM45.Spots.BarkSkulls)
                foreach (string str in new string[] { "once", "twice", "three times", "four times", "five times" })
                    BarkmanSkulls.MainObjectives.Add(new QuestLocation("Feed the skull " + str, map));
            BarkmanSkulls.MainObjectives.Add(new QuestLocation("Defeat Barkman", BarkmanSkulls.Location(0)));
            foreach (MapXY map in MM45.Spots.BarkTreasures)
                BarkmanSkulls.Postrequisites.Add(new QuestLocation("Collect the treasure", map));

            Bark.PreQuest.Add(questMelons);
            Bark.Prerequisites.Add(new QuestLocation("Obtain the Key to the Temple of Bark", MM45.Spots.Nibbler));
            MapXY[] dials = MM45.Spots.BarkDials;
            for (int i = 0; i < 6; i++)
                Bark.MainObjectives.Add(new QuestLocation(String.Format("Set the dial to {0}", i / 2 + 1), dials[i]));
            Bark.AddLocations(new QuestLocation("Pull the Lever", MM45.Spots.BarkLever),
                new QuestLocation("Drink from the Dark Water", MM45.Spots.BarkPool));
            BarkmanSkulls.PreQuest.Add(AddSideQuest(Bark, quests, String.Empty, "+19 to Primary Stats", statistics));
            AddSideQuest(BarkmanSkulls, quests, String.Empty, "2000000 Gold, 25000 Gems, 3 L7 Items");

            quests.Sort(CompareMM45Quests);
            return quests.ToArray();
        }

        public int CompareMM45Quests(MM45Quest quest1, MM45Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public class Bits
        {
            private MM45PartyBytes m_party;
            public QuestTotals Totals;

            public Bits(MM45PartyBytes party)
            {
                m_party = party;
                Totals = new QuestTotals(0, 0);
            }

            private QuestGoal Goal(bool b) { return b ? QuestGoal.Complete : QuestGoal.Incomplete; }
            private QuestGoal Goal(bool b, QuestGoal qgFalse) { return b ? QuestGoal.Complete : qgFalse; }

            public QuestGoal Quest(MM45Bits.Quest bit) { return Goal(MM45Bits.IsSet(m_party.QuestBits, bit)); }
            public QuestGoal Cloud(MM45Bits.Clouds bit) { return Goal(MM45Bits.IsSet(m_party.PartyBits1, bit)); }
            public QuestGoal Dark(MM45Bits.Dark bit) { return Goal(MM45Bits.IsSet(m_party.PartyBits2, bit)); }
            public QuestGoal World(MM45Bits.World bit) { return Goal(MM45Bits.IsSet(m_party.WorldBits, bit)); }
            public QuestGoal Item(MM45QuestItemIndex item) { return Goal(m_party.HasQuestItem(item)); }

            public QuestGoal Cloud(MM45Bits.Clouds bit, QuestGoal qgFalse) { return Goal(MM45Bits.IsSet(m_party.PartyBits1, bit), qgFalse); }

            public QuestGoal[] Clouds(params MM45Bits.Clouds[] bits)
            {
                QuestGoal[] goals = new QuestGoal[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    goals[i] = Goal(MM45Bits.IsSet(m_party.PartyBits1, bits[i]));
                return goals;
            }

            public QuestGoal[] Darks(params MM45Bits.Dark[] bits)
            {
                QuestGoal[] goals = new QuestGoal[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    goals[i] = Goal(MM45Bits.IsSet(m_party.PartyBits2, bits[i]));
                return goals;
            }

            public QuestGoal[] Worlds(params MM45Bits.World[] bits)
            {
                QuestGoal[] goals = new QuestGoal[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    goals[i] = Goal(MM45Bits.IsSet(m_party.WorldBits, bits[i]));
                return goals;
            }

            public QuestGoal Char(int iCharAddress, MM45Bits.CharBit bit)
            {
                if (iCharAddress >= 0 && iCharAddress < m_party.arrayPartyMembers.Length)
                    return Goal(MM45Bits.IsSet(m_party.CharBits, m_party.arrayPartyMembers[iCharAddress], bit));
                return QuestGoal.BadFile;
            }
        }

        private void StandardQuest(QuestTotals totals, QuestStatus status, QuestGoal bQuestActive, QuestGoal bMainObjective, QuestGoal bComplete)
        {
            status.AddStdQuest(bQuestActive, bComplete, bMainObjective);
            AddQuest(totals, status);
        }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, Cloud mainBit, Cloud completeBit) {
            StandardQuest(bits.Totals, status, bits.Quest(questBit), bits.Cloud(mainBit), bits.Cloud(completeBit)); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, Dark mainBit, Dark completeBit) {
            StandardQuest(bits.Totals, status, bits.Quest(questBit), bits.Dark(mainBit), bits.Dark(completeBit)); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, Dark mainBit, QuestGoal bComplete) {
            StandardQuest(bits.Totals, status, bits.Quest(questBit), bits.Dark(mainBit), bComplete); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, Cloud mainBit, QuestGoal bComplete) {
            StandardQuest(bits.Totals, status, bits.Quest(questBit), bits.Cloud(mainBit), bComplete); }

        private void StandardItemQuest(Bits bits, QuestStatus status, QuestGoal bQuest, QuestGoal bHaveItem, QuestGoal bComplete) {
            StandardQuest(bits.Totals, status, bQuest, QuestStatus.Or(bHaveItem, bComplete), bComplete); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, MM45QuestItemIndex item, QuestGoal bComplete) {
            StandardItemQuest(bits, status, bits.Quest(questBit), bits.Item(item), bComplete); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, MM45QuestItemIndex item, MM45QuestItemIndex itemComplete) {
            StandardItemQuest(bits, status, bits.Quest(questBit), bits.Item(item), bits.Item(itemComplete)); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, MM45QuestItemIndex item, Cloud completeBit) {
            StandardItemQuest(bits, status, bits.Quest(questBit), bits.Item(item), bits.Cloud(completeBit)); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, MM45QuestItemIndex item, Dark completeBit) {
            StandardItemQuest(bits, status, bits.Quest(questBit), bits.Item(item), bits.Dark(completeBit)); }

        private void StandardQuest(Bits bits, QuestStatus status, Quest questBit, QuestGoal bMain, QuestGoal bComplete) {
            StandardQuest(bits.Totals, status, bits.Quest(questBit), bMain, bComplete); }

        private QuestGoal Goal(bool b, bool bValid = true) { return bValid ? (b ? QuestGoal.Complete : QuestGoal.Incomplete) : QuestGoal.BadFile; }

        private QuestStatus.Single InvalidGoal(bool b, string strInvalid) { return b ? QuestStatus.Single.Invalid(strInvalid) : QuestStatus.Single.Incomplete; }

        private QuestStatus.Single InvalidGoal(bool b1, string strInvalid1, bool b2, string strInvalid2)
        {
            if (b1)
                return QuestStatus.Single.Invalid(strInvalid1);
            if (b2)
                return QuestStatus.Single.Invalid(strInvalid2);
            return QuestStatus.Single.Incomplete;
        }

        private void SkillQuest(QuestTotals totals, QuestStatus qsSkill, byte bSkill)
        {
            qsSkill.AddPost(3, Goal(bSkill > 0));
            AddQuest(totals, qsSkill);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress = -1)
        {
            MM45PartyInfo party = data.Party as MM45PartyInfo;
            MM45GameInfo info = data.Info as MM45GameInfo;

            MM45QuestData mm45Quest = data as MM45QuestData;
            if (mm45Quest == null)
                return;

            MM45FileQuestInfo fqi = mm45Quest.FileQuestInfo;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            MM45Character mm45Char = MM45Character.Create(party.Bytes, iOverrideCharAddress * MM45Character.SizeInBytes, info);

            if (mm45Char == null || mm45Char.Awards == null)
                return;

            CharName = mm45Char.CharName;
            CharAddress = iOverrideCharAddress;
            CharClass = mm45Char.BasicClass;

            Bits bits = new Bits(info.Party);

            MM45Awards awards = mm45Char.Awards;

            StandardQuest(bits, DefeatDwarfKing, Quest.SlayMadDwarfKing, Cloud.KillDwarfKingDMO3026, Cloud.TurnInDwarfKingV1405);
            StandardQuest(bits, RidVertigoOfPests, Quest.RidVertigoOfPests, Cloud.FindJoesInvoice, Cloud.TurnInVertigoPlagueV1405);

            GatherPhirnaRoots.AddPre(bits.Quest(Quest.GatherPhirnaRoot));
            GatherPhirnaRoots.AddObj(
                fqi.Goal(FileB.Phirna1), fqi.Goal(FileB.Phirna2), fqi.Goal(FileB.Phirna3), fqi.Goal(FileB.Phirna4), fqi.Goal(FileB.Phirna5), fqi.Goal(FileB.Phirna6), fqi.Goal(FileB.Phirna7), fqi.Goal(FileB.Phirna8), fqi.Goal(FileB.Phirna9), fqi.Goal(FileB.Phirna10));
            GatherPhirnaRoots.AddObj(QuestStatus.Not(bits.Item(MM45QuestItemIndex.PhirnaRoot)));
            AddQuest(bits.Totals, GatherPhirnaRoots);

            StandardQuest(bits, FreeCelia, Quest.FreeCelia, Cloud.FreeCeliaD41515, fqi.Goal(FileB.HelpedDerek));
            StandardQuest(bits, RetrieveAlacorn, Quest.RetrieveAlacorn, MM45QuestItemIndex.AlacornOfFalista, fqi.Goal(FileB.ReturnedAlacorn));
            StandardQuest(bits, FindOrothinsWhistle, Quest.FindOrothinsWhistle, MM45QuestItemIndex.OrothinsBoneWhistle, Cloud.TurnInOrothinF30906);
            StandardQuest(bits, FindLigonosSkull, Quest.FindLigonosSkull, MM45QuestItemIndex.LigonosMissingSkull, fqi.Goal(FileB.Skull));
            StandardQuest(bits, GetBaroksPendant, Quest.GetBaroksPendant, MM45QuestItemIndex.BaroksMagicPendant, Cloud.TurnInBarokQuestR2520);
            StandardQuest(bits, GetRoxannesTiara, Quest.GetRoxannesTiara, MM45QuestItemIndex.PrincessRoxannesTiara, fqi.Goal(FileB.Tiara));
            StandardQuest(bits, ReturnScarab, Quest.ReturnScarab, MM45QuestItemIndex.ScarabOfImaging, fqi.Goal(FileB.Scarab));
            StandardQuest(bits, ReturnCrystals, Quest.ReturnScarab, MM45QuestItemIndex.CrystalsOfPiezoelectricity, fqi.Goal(FileB.Crystals));
            StandardQuest(bits, StealElixir, Quest.StealElixir, MM45QuestItemIndex.ElixirOfRestoration, fqi.Goal(FileB.Elixir));
            StandardQuest(bits, FindFaeryWand, Quest.FindFaeryWand, MM45QuestItemIndex.WandOfFaeryMagic, Cloud.TurnInFaeryWandC31415);
            StandardQuest(bits, FindHolyBook, Quest.FindHolyBook, MM45QuestItemIndex.HolyBookOfElvenkind, fqi.Goal(FileB.Book));
            StandardQuest(bits, DestroyTrollLair, Quest.DestroyTrollLair, Cloud.DestroyTrollB40207, fqi.Goal(FileB.Troll));
            StandardQuest(bits, DestroyOgreLair, Quest.DestroyOgreLair, Cloud.DestroyOgreC20500, Cloud.TurnInDestroyOgreC20901);
            StandardQuest(bits, ReclaimPagoda, Quest.ReclaimPagoda, Cloud.EnterPagodaA31512, Cloud.TurnInPagodaA31512);

            SlayLakeMonsters.AddMonsterQuest(bits.Quest(Quest.SlayLakeMonsters), bits.Cloud(Cloud.TurnInLakeMonstersC31213), MapXY.Array(MM4Map.D3Surface, fqi.WaterDragons));
            AddQuest(bits.Totals, SlayLakeMonsters);

            SaveWinterkill.AddPre(QuestStatus.Or(bits.Quest(Quest.SaveWinterkill), bits.Cloud(Cloud.ReportNoMonstersW0813)));
            SaveWinterkill.AddMonsterObj(fqi.SpiritBones);
            SaveWinterkill.AddObj(bits.Cloud(Cloud.BangGongAfterSpiritBonesW1301));
            SaveWinterkill.AddObj(bits.Cloud(Cloud.SummonPolterFoolsW0813));
            SaveWinterkill.AddMonsterObj(bits.Cloud(Cloud.SummonPolterFoolsW0813, QuestGoal.NotStarted), fqi.PolterFools);
            SaveWinterkill.AddObj(bits.Cloud(Cloud.BangGongAfterPolterFoolsW1301));
            SaveWinterkill.AddObj(bits.Cloud(Cloud.SummonGhostRidersW0813));
            SaveWinterkill.AddMonsterObj(bits.Cloud(Cloud.SummonGhostRidersW0813, QuestGoal.NotStarted), fqi.GhostRiders);
            SaveWinterkill.AddObj(bits.Cloud(Cloud.BangGongAfterGhostRidersW1301));
            SaveWinterkill.LocationsOverride = MapXY.Combine(
                MapXY.Array(MM4Map.A3Winterkill, fqi.SpiritBones),
                MapXY.Array(MM4Map.A3Winterkill, 13, 1),
                MapXY.Array(MM4Map.A3Winterkill, 8, 13),
                MapXY.Array(MM4Map.A3Winterkill, fqi.PolterFools),
                MapXY.Array(MM4Map.A3Winterkill, 13, 1),
                MapXY.Array(MM4Map.A3Winterkill, 8, 13),
                MapXY.Array(MM4Map.A3Winterkill, fqi.GhostRiders),
                MapXY.Array(MM4Map.A3Winterkill, 13, 1)
                );
            SaveWinterkill.AddPost(bits.Cloud(Cloud.ReportNoMonstersW0813));
            AddQuest(bits.Totals, SaveWinterkill);

            StandardQuest(bits, DestroyCyclopsLair, Quest.DestroyCyclopsLair, Cloud.DestroyCyclopsA31000, fqi.Goal(FileB.Cyclops));
            StandardQuest(bits, FindEverhotRock, Quest.FindEverhotRock, MM45QuestItemIndex.EverHotLavaRock, fqi.Goal(FileB.Rock));
            StandardQuest(bits, RetrieveScrollOfInsight, Quest.RetrieveScrollOfInsight, MM45QuestItemIndex.ScrollOfInsight, fqi.Goal(FileB.Scroll));
            StandardQuest(bits, FreeCrodo, Quest.FreeCrodo, Goal(awards.IsSet(MM45AwardIndex.RescuedCrodo)), bits.Item(MM45QuestItemIndex.ExcavationPermit));

            FindXeenSlayer.AddObj(bits.Cloud(Cloud.BuyLandC41112));
            FindXeenSlayer.AddObj(bits.Item(MM45QuestItemIndex.YakStoneOfOpening));
            FindXeenSlayer.AddObj(fqi.YakCredits);
            FindXeenSlayer.AddObj(bits.Cloud(Cloud.BuildWallCB10207));
            FindXeenSlayer.AddObj(bits.Item(MM45QuestItemIndex.StoneOfAThousandTerrors));
            FindXeenSlayer.AddObj(fqi.TombCredits);
            FindXeenSlayer.AddObj(bits.Cloud(Cloud.BuildKeepCB10207));
            FindXeenSlayer.AddObj(bits.Item(MM45QuestItemIndex.GolemStoneOfAdmittance));
            FindXeenSlayer.AddObj(fqi.GolemCredits);
            FindXeenSlayer.AddObj(bits.Cloud(Cloud.CleanOutDungeonCB0207));
            FindXeenSlayer.AddPost(fqi.Goal(FileB.XeenSlayer));
            AddQuest(bits.Totals, FindXeenSlayer);

            QuestGoal cupie = bits.Item(MM45QuestItemIndex.CupieDoll);
            XeenDoll.AddObj(QuestStatus.Or(bits.Item(MM45QuestItemIndex.MightDoll), cupie),
                QuestStatus.Or(bits.Item(MM45QuestItemIndex.SpeedDoll), cupie),
                QuestStatus.Or(bits.Item(MM45QuestItemIndex.EnduranceDoll), cupie),
                QuestStatus.Or(bits.Item(MM45QuestItemIndex.AccuracyDoll), cupie));
            XeenDoll.AddPost(cupie);
            AddQuest(bits.Totals, XeenDoll);

            SlayLordXeen.LocationsOverride = MapXY.Array(MM4Map.D3XeensCastleLevel4, fqi.Point(FileP.LordXeen));
            SlayLordXeen.AddPost(bits.Item(MM45QuestItemIndex.XeensScepterOfTemporalDistortion));
            AddQuest(bits.Totals, SlayLordXeen);

            StandardQuest(bits, FindMirror, Quest.FindMirror, MM45QuestItemIndex.XeensScepterOfTemporalDistortion, fqi.Goal(FileB.Mirror));

            QuestGoal rain = bits.Item(MM45QuestItemIndex.LastRaindropOfSpring);
            QuestGoal snow = bits.Item(MM45QuestItemIndex.LastSnowflakeOfWinter);
            QuestGoal leaf = bits.Item(MM45QuestItemIndex.LastLeafOfAutumn);
            TurnSeasons.AddPre(QuestStatus.Or(bits.Item(MM45QuestItemIndex.LastFlowerOfSummer), leaf, snow, rain));
            TurnSeasons.AddObj(QuestStatus.Or(leaf, snow, rain));
            TurnSeasons.AddObj(QuestStatus.Or(snow, rain));
            TurnSeasons.AddObj(rain);
            TurnSeasons.AddObj(QuestGoal.Incomplete);  // quest is repeatable, so the last step is never really "complete"
            AddQuest(bits.Totals, TurnSeasons);

            QuestGoal bHaveWTKey = bits.Item(MM45QuestItemIndex.KeyToGreatWesternTower);
            FindWesternTowerKey.AddStdQuest(QuestStatus.OrAndNot(bits.Quest(Quest.FindWesternTowerKey), bHaveWTKey, fqi.Goal(FileB.Dreyfus)), fqi.Goal(FileB.Dreyfus),
                bHaveWTKey, bits.Dark(Dark.TurnInWesternTowerA30810));
            AddQuest(bits.Totals, FindWesternTowerKey);

            CollectRubies.AddPrePost(bits.Quest(Quest.CollectRubies));
            CollectRubies.AddSingles(bits, Dark.Mine1GM2027, Dark.Mine1GM2509, Dark.Mine1GM1025, Dark.Mine1GM2129, Dark.Mine1GM1411, Dark.Mine1GM2004,
                Dark.Mine1GM0003, Dark.Mine1GM1901, Dark.Mine1GM1515, Dark.Mine1GM2002, Dark.Mine1GM0401);
            CollectRubies.AddDoubles(bits, Dark.Mine1GM0403, Dark.Mine2GM0403, Dark.Mine1GM0406, Dark.Mine2GM0406, Dark.Mine1GM2917, Dark.Mine2GM2917,
                Dark.Mine1GM3112, Dark.Mine2GM3112, Dark.Mine1GM1022, Dark.Mine2GM1022);
            CollectRubies.AddTriples(bits, Dark.Mine1GM0617, Dark.Mine2GM0617, Dark.Mine3GM0617, Dark.Mine1GM0006, Dark.Mine2GM0006, Dark.Mine3GM0006,
                Dark.Mine1GM3108, Dark.Mine2GM3108, Dark.Mine3GM3108, Dark.Mine1GM3117, Dark.Mine2GM3117, Dark.Mine3GM3117);
            AddQuest(bits.Totals, CollectRubies);

            CollectEmeralds.AddPrePost(bits.Quest(Quest.CollectEmeralds));
            CollectEmeralds.AddSingles(bits, Dark.Mine1GM1705, Dark.Mine1GM1604, Dark.Mine1GM0014, Dark.Mine1GM1531, Dark.Mine1GM3125, Dark.Mine1GM1731,
                Dark.Mine1GM0418, Dark.Mine1GM3123, Dark.Mine1GM1015, Dark.Mine1GM1018, Dark.Mine1GM0412);
            CollectEmeralds.AddDoubles(bits, Dark.Mine1GM0731, Dark.Mine2GM0731, Dark.Mine1GM3121, Dark.Mine2GM3121, Dark.Mine1GM0218, Dark.Mine2GM0218,
                Dark.Mine1GM0210, Dark.Mine2GM0210, Dark.Mine1GM3127, Dark.Mine2GM3127);
            CollectEmeralds.AddTriples(bits, Dark.Mine1GM0431, Dark.Mine2GM0431, Dark.Mine3GM0431, Dark.Mine1GM2531, Dark.Mine2GM2531, Dark.Mine3GM2531,
                Dark.Mine1GM3131, Dark.Mine2GM3131, Dark.Mine3GM3131, Dark.Mine1GM0010, Dark.Mine2GM0010, Dark.Mine3GM0010,
                Dark.Mine1GM0018, Dark.Mine2GM0018, Dark.Mine3GM0018);
            AddQuest(bits.Totals, CollectEmeralds);

            CollectSapphires.AddPrePost(bits.Quest(Quest.CollectSapphires));
            CollectSapphires.AddSingles(bits, Dark.Mine1GM1418, Dark.Mine1GM0021, Dark.Mine1GM1527, Dark.Mine1GM0031, Dark.Mine1GM1316);
            CollectSapphires.AddDoubles(bits, Dark.Mine1GM0023, Dark.Mine2GM0023, Dark.Mine1GM1105, Dark.Mine2GM1105,
                Dark.Mine1GM1201, Dark.Mine2GM1201, Dark.Mine1GM0028, Dark.Mine2GM0028);
            CollectSapphires.AddTriples(bits, Dark.Mine1GM0026, Dark.Mine2GM0026, Dark.Mine3GM0026, Dark.Mine1GM0801, Dark.Mine2GM0801, Dark.Mine3GM0801);
            AddQuest(bits.Totals, CollectSapphires);

            CollectDiamonds.AddPrePost(bits.Quest(Quest.CollectDiamonds));
            CollectDiamonds.AddSingles(bits, Dark.Mine1GM1131, Dark.Mine1GM1522, Dark.Mine1GM1425,
                Dark.Mine1GM0813, Dark.Mine1GM0810, Dark.Mine1GM2402, Dark.Mine1GM1607);
            CollectDiamonds.AddDoubles(bits, Dark.Mine1GM3103, Dark.Mine2GM3103, Dark.Mine1GM2701, Dark.Mine2GM2701, Dark.Mine1GM3129, Dark.Mine2GM3129,
                Dark.Mine1GM2604, Dark.Mine2GM2604, Dark.Mine1GM1129, Dark.Mine2GM1129);
            CollectDiamonds.AddTriples(bits, Dark.Mine1GM1027, Dark.Mine2GM1027, Dark.Mine3GM1027, Dark.Mine1GM3100, Dark.Mine2GM3100, Dark.Mine3GM3100);
            AddQuest(bits.Totals, CollectDiamonds);

            FindStatuettes.AddStdQuest(bits.Quest(Quest.FindStatuettes), fqi.Goal(FileB.Luna), bits.Item(MM45QuestItemIndex.GoldenPegasusStatuette),
                bits.Item(MM45QuestItemIndex.GoldenDragonStatuette), bits.Item(MM45QuestItemIndex.GoldenGriffinStatuette));
            AddQuest(bits.Totals, FindStatuettes);

            FindSongbird.AddStdQuest(bits.Quest(Quest.FindSongbird), fqi.Goal(FileB.Songbird),
                QuestStatus.Or(bits.Item(MM45QuestItemIndex.SongbirdOfSerenity), bits.Dark(Dark.TurnInSongbird2CK21015)), bits.Dark(Dark.TurnInSongbird2CK21015));
            AddQuest(bits.Totals, FindSongbird);

            EnterBlackfang.AddPre(QuestStatus.Or(bits.Quest(Quest.TalkToAmbrose), bits.Quest(Quest.EnchantBridle), bits.Dark(Dark.TurnInEnchantBridleB11205)));
            EnterBlackfang.AddObj(QuestStatus.Or(bits.Item(MM45QuestItemIndex.Bridle), bits.Item(MM45QuestItemIndex.EnchantedBridle), bits.Dark(Dark.TurnInEnchantBridleB11205)));
            EnterBlackfang.AddObj(QuestStatus.Or(bits.Item(MM45QuestItemIndex.EnchantedBridle), bits.Dark(Dark.TurnInEnchantBridleB11205)));
            EnterBlackfang.AddPost(bits.Dark(Dark.TurnInEnchantBridleB11205));
            AddQuest(bits.Totals, EnterBlackfang);

            StandardQuest(bits, SaveKalindra, Quest.SaveKalindra, MM45QuestItemIndex.QueenKalindrasCrown, Dark.RescueKalindra2CBD0101);

            DestroyOgres.AddStdQuest(bits.Quest(Quest.DestroyOgres), fqi.Goal(FileB.Ogres), bits.Dark(Dark.DestroyOgreD30908), bits.Dark(Dark.DestroyOgreD30307));
            AddQuest(bits.Totals, DestroyOgres);

            StandardQuest(bits, FindVesparsHandle, Quest.FindVesparsHandle, MM45QuestItemIndex.VesparsEmeraldHandle, fqi.Goal(FileB.Handle));

            BringMelons.AddPre(QuestStatus.Or(bits.Quest(Quest.BringMelons), fqi.Goal(FileB.Melon2)));
            BringMelons.AddObj(fqi.Goal(FileB.FoundMelon1));
            BringMelons.AddObj(fqi.Goal(FileB.FoundMelon2));
            BringMelons.AddObj(fqi.Goal(FileB.FoundMelon3));
            BringMelons.AddObj(fqi.Goal(FileB.FoundMelon4));
            BringMelons.AddObj(fqi.Goal(FileB.Melon1));
            BringMelons.AddObj(bits.Dark(Dark.EnterBarkC40208));
            BringMelons.AddPost(fqi.Goal(FileB.Melon2));
            AddQuest(bits.Totals, BringMelons);

            StandardQuest(bits, RescueSprite, Quest.RescueSprite, Dark.RescueSheewanaTB30612, fqi.Goal(FileB.Sheewana));
            StandardQuest(bits, FindChalice, Quest.FindChalice, MM45QuestItemIndex.ChaliceOfProtection, fqi.Goal(FileB.Chalice));
            StandardQuest(bits, FindEctorsRing, Quest.FindEctorsRing, MM45QuestItemIndex.EctorsRing, fqi.Goal(FileB.Ring));
            StandardQuest(bits, FindCalebsGlass, Quest.FindCalebsGlass, MM45QuestItemIndex.CalebsMagnifyingGlass, fqi.Goal(FileB.Glass));

            FindJewelOfAges.AddStdQuest(bits.Quest(Quest.FindJewelOfAges), fqi.Goal(FileB.Jewel), 
                bits.Item(MM45QuestItemIndex.KeyToGreatEasternTower), bits.Item(MM45QuestItemIndex.JewelOfAges));
            AddQuest(bits.Totals, FindJewelOfAges);

            IllusionKey.AddPost(bits.Item(MM45QuestItemIndex.EnchantedKeyToTowerOfHighMagic));
            AddQuest(bits.Totals, IllusionKey);

            StopGettlewaith.AddObj(Goal(fqi.Point(FileP.Gettlewaith) != new Point(31, 0), fqi.Valid));
            StopGettlewaith.AddMonsterQuest(bits.Quest(Quest.StopGettlewaith), fqi.Goal(FileB.KilledGettle), MapXY.Array(MM5Map.A4Castleview, fqi.Point(FileP.Gettlewaith)));
            StopGettlewaith.MarkAllWhenComplete = true;
            AddQuest(bits.Totals, StopGettlewaith);

            StandardQuest(bits, ReleaseJethrosBrother, Quest.ReleaseJethrosBrother, fqi.Dead(FileP.Jasper), fqi.Goal(FileB.Jethro));
            StandardQuest(bits, FindNadiasNecklace, Quest.FindNadiasNecklace, MM45QuestItemIndex.OnyxNecklace, MM45QuestItemIndex.KeyToEllingersTower);

            EndXenocsReign.AddMonsterQuest(bits.Quest(Quest.EndXenocsReign), fqi.Goal(FileB.Astra),
                MapXY.Array(MM5Map.E3Sandcaster, fqi.Point(FileP.Xenoc), fqi.Point(FileP.Morgana)));
            AddQuest(bits.Totals, EndXenocsReign);

            FindEnergyDisks.AddStdQuest(bits.Quest(Quest.FindEnergyDisks), bits.Dark(Dark.DeliverDisks4ET40408), fqi.EnergyDisks);
            FindEnergyDisks.AddObj(bits.Dark(Dark.DeliverDisks1ET40408), bits.Dark(Dark.DeliverDisks2ET40408), bits.Dark(Dark.DeliverDisks3ET40408));
            AddQuest(bits.Totals, FindEnergyDisks);

            StandardQuest(bits, FindSandrosHeart, Quest.FindSandrosHeart, MM45QuestItemIndex.SandrosHeart, Dark.TurnInSandroN1008);
            StandardQuest(bits, FindDragonEgg, Quest.FindDragonEgg, MM45QuestItemIndex.DragonEgg, fqi.Goal(FileB.Egg));

            QuestGoal cube = bits.Item(MM45QuestItemIndex.CubeOfPower);
            DefeatAlamar.AddPre(bits.Dark(Dark.DragonParaohGP40609));
            DefeatAlamar.AddObj(bits.Dark(Dark.FreeCorakEP10408), bits.Dark(Dark.ChosenOnesGP40609),
                QuestStatus.Or(cube, bits.Item(MM45QuestItemIndex.SoulBox), bits.Item(MM45QuestItemIndex.SoulBoxWithCorakInside)),
                QuestStatus.Or(cube, bits.Item(MM45QuestItemIndex.SoulBoxWithCorakInside)));
            DefeatAlamar.AddPost(cube);
            AddQuest(bits.Totals, DefeatAlamar);

            UniteWorlds.AddPre(bits.Dark(Dark.DragonParaohGP40609));
            UniteWorlds.AddObj(bits.Dark(Dark.WaterSleeperEPW0303),
                bits.Dark(Dark.AirSleeperEPA1212),
                bits.Dark(Dark.FireSleeperEPF0312),
                bits.Dark(Dark.EarthSleeperEPE1203),
                bits.Cloud(Cloud.WaterReflectorF41500),
                bits.Cloud(Cloud.AirReflectorF11515),
                bits.Cloud(Cloud.FireReflectorF11515),
                bits.Cloud(Cloud.EarthReflectorA40000),
                bits.Item(MM45QuestItemIndex.KeyToDragonTower),
                bits.Item(MM45QuestItemIndex.SilverIDCard),
                bits.Item(MM45QuestItemIndex.KeyToDarkstoneTower),
                bits.Item(MM45QuestItemIndex.GoldIDCard),
                bits.Item(MM45QuestItemIndex.AmuletOfTheSouthernSphinx),
                bits.Item(MM45QuestItemIndex.ChimeOfOpening));
            UniteWorlds.AddPost(QuestGoal.Incomplete); // The last step of this final quest is never saved anywhere, so it is always "incomplete"
            AddQuest(bits.Totals, UniteWorlds);

            // Add the proper Light/Awaken spell based on the class
            if (mm45Char.IsHealer)
            {
                SpellClericAwaken = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Awaken);
                SpellClericLight = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Light);
                SpellArcaneAwaken.Set(QuestStatus.Basic.InvalidClass);
                SpellArcaneLight.Set(QuestStatus.Basic.InvalidClass);
            }
            else
            {
                SpellArcaneAwaken = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.AwakenArcane);
                SpellArcaneLight = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.LightArcane);
                SpellClericAwaken.Set(QuestStatus.Basic.InvalidClass);
                SpellClericLight.Set(QuestStatus.Basic.InvalidClass);
            }

            SpellClericAcidSpray = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.AcidSpray);
            SpellClericBeastMaster = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.BeastMaster);
            SpellClericBless = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Bless);
            SpellClericColdRay = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.ColdRay);
            SpellClericCreateFood = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.CreateFood);
            SpellClericCureDisease = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.CureDisease);
            SpellClericCureParalysis = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.CureParalysis);
            SpellClericCurePoison = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.CurePoison);
            SpellClericCureWounds = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.CureWounds);
            SpellClericDayOfProtection = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DayOfProtection);
            SpellClericDeadlySwarm = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DeadlySwarm);
            SpellClericDivineIntervention = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DivineIntervention);
            SpellClericFieryFlail = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FieryFlail);
            SpellClericFirstAid = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FirstAid);
            SpellClericFlyingFist = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FlyingFist);
            SpellClericFrostBite = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FrostBite);
            SpellClericHeroism = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Heroism);
            SpellClericHolyBonus = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.HolyBonus);
            SpellClericHolyWord = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.HolyWord);
            SpellClericHypnotize = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Hypnotize);
            SpellClericMassDistortion = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.MassDistortion);
            SpellClericMoonRay = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.MoonRay);
            SpellClericNaturesCure = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.NaturesCure);
            SpellClericPain = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Pain);
            SpellClericPowerCure = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.PowerCure);
            SpellClericProtFromElements = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.ProtFromElements);
            SpellClericRaiseDead = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.RaiseDead);
            SpellClericResurrect = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Resurrect);
            SpellClericRevitalize = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Revitalize);
            SpellClericSparks = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Sparks);
            SpellClericStoneToFlesh = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.StoneToFlesh);
            SpellClericSunRay = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.SunRay);
            SpellClericSuppressDisease = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.SuppressDisease);
            SpellClericSuppressPoison = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.SuppressPoison);
            SpellClericTownPortal = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.TownPortal);
            SpellClericTurnUndead = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.TurnUndead);
            SpellClericWalkOnWater = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.WalkOnWater);

            SpellArcaneClairvoyance = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Clairvoyance);
            SpellArcaneDancingSword = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DancingSword);
            SpellArcaneDayOfSorcery = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DayOfSorcery);
            SpellArcaneDetectMonster = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DetectMonster);
            SpellArcaneDragonSleep = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.DragonSleep);
            SpellArcaneElementalStorm = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.ElementalStorm);
            SpellArcaneEnchantItem = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.EnchantItem);
            SpellArcaneEnergyBlast = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.EnergyBlast);
            SpellArcaneEtherealize = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Etherealize);
            SpellArcaneFantasticFreeze = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FantasticFreeze);
            SpellArcaneFingerOfDeath = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.FingerOfDeath);
            SpellArcaneFireball = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Fireball);
            SpellArcaneGolemStopper = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.GolemStopper);
            SpellArcaneIdentifyMonster = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.IdentifyMonster);
            SpellArcaneImplosion = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Implosion);
            SpellArcaneIncinerate = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Incinerate);
            SpellArcaneInferno = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Inferno);
            SpellArcaneInsectSpray = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.InsectSpray);
            SpellArcaneItemToGold = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.ItemToGold);
            SpellArcaneJump = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Jump);
            SpellArcaneLevitate = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Levitate);
            SpellArcaneLightningBolt = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.LightningBolt);
            SpellArcaneLloydsBeacon = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.LloydsBeacon);
            SpellArcaneMagicArrow = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.MagicArrow);
            SpellArcaneMegaVolts = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.MegaVolts);
            SpellArcanePoisonVolley = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.PoisonVolley);
            SpellArcanePowerShield = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.PowerShield);
            SpellArcanePrismaticLight = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.PrismaticLight);
            SpellArcaneRechargeItem = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.RechargeItem);
            SpellArcaneShrapmetal = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Shrapmetal);
            SpellArcaneSleep = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Sleep);
            SpellArcaneStarBurst = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.StarBurst);
            SpellArcaneSuperShelter = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.SuperShelter);
            SpellArcaneTeleport = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.Teleport);
            SpellArcaneTimeDistortion = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.TimeDistortion);
            SpellArcaneToxicCloud = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.ToxicCloud);
            SpellArcaneWizardEye = AddSpellQuest(bits.Totals, mm45Char, MM45SpellIndex.WizardEye);

            SkillQuest(bits.Totals, SkillArmsMaster, mm45Char.Skills.ArmsMaster);
            SkillQuest(bits.Totals, SkillAstrologer, mm45Char.Skills.Astrologer);
            SkillQuest(bits.Totals, SkillBodyBuilder, mm45Char.Skills.BodyBuilder);
            SkillQuest(bits.Totals, SkillCartographer, mm45Char.Skills.Cartographer);
            SkillQuest(bits.Totals, SkillCrusader, mm45Char.Skills.Crusader);
            SkillQuest(bits.Totals, SkillDirectionSense, mm45Char.Skills.DirectionSense);
            SkillQuest(bits.Totals, SkillLinguist, mm45Char.Skills.Linguist);
            SkillQuest(bits.Totals, SkillMerchant, mm45Char.Skills.Merchant);
            SkillQuest(bits.Totals, SkillMountaineer, mm45Char.Skills.Mountaineer);
            SkillQuest(bits.Totals, SkillNavigator, mm45Char.Skills.Navigator);
            SkillQuest(bits.Totals, SkillPathFinder, mm45Char.Skills.PathFinder);
            SkillQuest(bits.Totals, SkillPrayerMaster, mm45Char.Skills.PrayerMaster);
            SkillQuest(bits.Totals, SkillPrestidigitator, mm45Char.Skills.Prestidigitator);
            SkillQuest(bits.Totals, SkillSwimmer, mm45Char.Skills.Swimmer);
            SkillQuest(bits.Totals, SkillSpotSecretDoors, mm45Char.Skills.SpotSecretDoors);
            SkillQuest(bits.Totals, SkillDangerSense, mm45Char.Skills.DangerSense);

            Pitchfork.AddPre(fqi.Dead(FileP.ChestP));
            Pitchfork.AddObj(fqi.Dead(FileP.ChestI), fqi.Dead(FileP.ChestT), fqi.Dead(FileP.ChestC), fqi.Dead(FileP.ChestH),
                fqi.Dead(FileP.ChestF), fqi.Dead(FileP.ChestO), fqi.Dead(FileP.ChestR));
            Pitchfork.MarkAllWhenComplete = true;
            Pitchfork.AddPost(fqi.Dead(FileP.ChestK));
            AddQuest(bits.Totals, Pitchfork);

            NumberChests.AddPre(fqi.Dead(FileP.Chest1));
            NumberChests.AddObj(fqi.Dead(FileP.Chest2), fqi.Dead(FileP.Chest3), fqi.Dead(FileP.Chest4), fqi.Dead(FileP.Chest5),
                fqi.Dead(FileP.Chest6), fqi.Dead(FileP.Chest7), fqi.Dead(FileP.Chest8));
            NumberChests.MarkAllWhenComplete = true;
            NumberChests.AddPost(fqi.Dead(FileP.Chest9));
            AddQuest(bits.Totals, NumberChests);

            QuestGoal golem1 = QuestStatus.AndNot(bits.Cloud(Cloud.PushButton1GD0123), bits.Cloud(Cloud.PushButton2GD0123));
            QuestGoal golem2 = QuestStatus.AndNot(bits.Cloud(Cloud.PushButton1GD0129), bits.Cloud(Cloud.PushButton2GD0129));
            GolemDungeon.AddObj(bits.Cloud(Cloud.FlipSwitchGD0113), bits.Cloud(Cloud.FlipSwitchGD0913), bits.Cloud(Cloud.FlipSwitchGD0106),
                bits.Cloud(Cloud.FlipSwitchGD0906), bits.Cloud(Cloud.FlipSwitchGD0101), bits.Cloud(Cloud.FlipSwitchGD0901),
                QuestStatus.Or(bits.Cloud(Cloud.PushButton2GD0123), golem1), bits.Cloud(Cloud.PushButton2GD0126), 
                QuestStatus.Or(bits.Cloud(Cloud.PushButton2GD0129), golem2), golem1, golem2);
            GolemDungeon.AddPost(bits.Cloud(Cloud.FlipSwitchGD1925));
            AddQuest(bits.Totals, GolemDungeon);

            for(FileP word = FileP.CWScepter; word <= FileP.CWElements; word++)
                Crossword.AddObj(fqi.Dead(word));
            Crossword.AddPost(fqi.Goal(FileB.WordMaster));
            AddQuest(bits.Totals, Crossword);

            for(FileP gong = FileP.TTGong1; gong <= FileP.TTGong4; gong++)
                TreasureTeleport.AddObj(fqi.Dead(gong));
            TreasureTeleport.AddPost(fqi.Goal(FileB.TTLever));
            AddQuest(bits.Totals, TreasureTeleport);

            for(FileB gold = FileB.Gold1; gold <= FileB.Gold160; gold++)
                MineGold.AddObj(fqi.Goal(gold));
            AddQuest(bits.Totals, MineGold);

            for (FileB destroy = FileB.Destroy1; destroy <= FileB.Destroy46; destroy++)
                DestroyThings.AddObj(fqi.Goal(destroy));
            AddQuest(bits.Totals, DestroyThings);

            FireRes.AddObj(fqi.Goals(FileB.Fire1, FileB.Fire4));
            FireRes.Obj.Add(InvalidGoal(mm45Char.FireResist.Permanent >= 50, "Your Fire Resistance is already over 49"));
            FireRes.Obj.Add(InvalidGoal(mm45Char.FireResist.Permanent > 20, "Your Fire Resistance is already over 20"));
            FireRes.AddObj(bits.Char(iOverrideCharAddress, CharB.BookOfFire));
            ElecRes.AddObj(fqi.Goals(FileB.Elec1, FileB.Elec4));
            ElecRes.Obj.Add(InvalidGoal(mm45Char.ElecResist.Permanent >= 50, "Your Electrical Resistance is already over 49"));
            ElecRes.Obj.Add(InvalidGoal(mm45Char.ElecResist.Permanent > 20, "Your Electrical Resistance is already over 20"));
            ElecRes.AddObj(bits.Char(iOverrideCharAddress, CharB.BookOfElectricity));
            ColdRes.AddObj(fqi.Goal(FileB.Cold1), fqi.Goal(FileB.Cold2));
            ColdRes.Obj.Add(InvalidGoal(mm45Char.ColdResist.Permanent >= 50, "Your Cold Resistance is already over 49"));
            ColdRes.Obj.Add(InvalidGoal(mm45Char.ColdResist.Permanent > 20, "Your Cold Resistance is already over 20"));
            PoisonRes.AddObj(fqi.Goals(FileB.Poison1, FileB.Poison3));
            PoisonRes.Obj.Add(InvalidGoal(mm45Char.PoisonResist.Permanent >= 50, "Your Poison Resistance is already over 49"));
            PoisonRes.Obj.Add(InvalidGoal(mm45Char.PoisonResist.Permanent > 20, "Your Poison Resistance is already over 20"));
            EnergyRes.AddObj(fqi.Goals(FileB.Energy1, FileB.Energy3));
            MagicRes.AddObj(fqi.Goals(FileB.Magic1, FileB.Magic3));
            Might.AddObj(fqi.Goals(FileB.Might1, FileB.Might42));
            Intellect.AddObj(fqi.Goals(FileB.Int1, FileB.Int29));
            Intellect.Obj.Add(InvalidGoal(mm45Char.Class != MM345Class.Sorcerer, "You are not a Sorcerer", mm45Char.Intellect.Permanent > 50, "Your Intellect is already over 50"));
            if (bits.Char(iOverrideCharAddress, CharB.BookOfFantasticKnowledge) == QuestGoal.Complete)
                Intellect.AddObj(QuestGoal.Complete);
            else
                Intellect.Obj.Add(InvalidGoal(!(mm45Char.Class == MM345Class.Sorcerer ||
                    mm45Char.Class == MM345Class.Ranger || mm45Char.Class == MM345Class.Archer), "You are not a Sorcerer, Ranger or Archer"));
            Personality.AddObj(fqi.Goals(FileB.Per1, FileB.Per26));
            Personality.Obj.Add(InvalidGoal(mm45Char.Personality.Permanent > 20, "Your Personality is already over 20"));
            Endurance.AddObj(fqi.Goals(FileB.End1, FileB.End44));
            Endurance.Obj.Add(InvalidGoal(mm45Char.Endurance.Permanent > 20, "Your Endurance is already over 20"));
            Speed.AddObj(fqi.Goals(FileB.Spd1, FileB.Spd37));
            Accuracy.AddObj(fqi.Goals(FileB.Acy1, FileB.Acy26));
            Luck.AddObj(fqi.Goals(FileB.Lck1, FileB.Lck19));
            Level.AddObj(fqi.Goals(FileB.Lev1, FileB.Lev52));
            Level.AddObj(bits.Char(iOverrideCharAddress, CharB.BookOfGreatPower));
            Level.AddObj(bits.Char(iOverrideCharAddress, CharB.FountainOfLife));
            if (bits.Char(iOverrideCharAddress, CharB.PrinceBook) == QuestGoal.Complete)
                Level.AddObj(QuestGoal.Complete);
            else
                Level.Obj.Add(InvalidGoal(mm45Char.Class != MM345Class.Robber, "You are not a Robber"));
            Level.AddObj(Goal(awards.IsSet(MM45AwardIndex.FoundShangriLa)));
            Level.AddObj(Goal(awards.IsSet(MM45AwardIndex.MasterOfGolems)));
            Level.AddObj(Goal(awards.IsSet(MM45AwardIndex.SuperExplorer)));
            MultiStat.AddObj(fqi.Goals(FileB.Stat1, FileB.Stat23));
            MultiStat.AddObj(bits.Char(iOverrideCharAddress, CharB.EuphoriaThrone));
            MultiStat.Obj.Add(InvalidGoal(mm45Char.PrimaryStats.All(s => s.Permanent >= 50), "Your Primary Statistics are all already at least 50"));
            MultiStat.Obj.Add(InvalidGoal(mm45Char.PrimaryStats.All(s => s.Permanent >= 50), "Your Primary Statistics are all already at least 50"));

            TempStats.AddObj(bits.Worlds(World.None, World.ShrineEnergyA10706, World.FountainGreatProtectionA30314, World.ShrineColdA41214,
                World.WellIntellectB31504, World.WellAccuracyB30003, World.WellEnduranceC10204, World.OlympianFountainC31510, World.WellPersonalityC30000,
                World.ShrineMagicC31500, World.WellMightD20308, World.SagesFountainD30809, World.ShrineElectricityD31504, World.WellSpeedE20304,
                World.ShrineFireE21303, World.ElementalShrineE30914, World.WellElementalResistanceD40204, World.FountaintheElementsB11413, World.FountainProtectionF31212,
                World.WishingWellF30107, World.ShrinePoisonF31406, World.FountainAbilityF30001, World.NightshadowWellN0707, World.WinterkillWellW0611, World.PrayA30814,
                World.WellMightA40911, World.WellProtectionA40310, World.WishingWellB40202, World.None, World.ShrineGreatPowerC20108, World.FountainMightD10613,
                World.FountainMagicResistanceF20805, World.FountainGreatProtectionF41403, World.WatersGreatPowerA10412, World.WatersPowerF30707,
                World.AspWellA0803, World.FountainSuperResilienceA10213, World.WellResilienceA30303, World.FountainGreatResilienceF11308, 
                World.WatersGreatMagicA40303, World.WatersMagicE30806, World.RivercityWellR1418, World.WellSpellsA30210, World.FountainGreatMagicE10210, World.None));

            foreach(QuestStatus qs in new QuestStatus[] { FireRes, ElecRes, ColdRes, PoisonRes, EnergyRes, MagicRes, 
                Might, Intellect, Personality, Endurance, Speed, Luck, Level, MultiStat, TempStats })
                AddQuest(bits.Totals, qs);

            Guilds.AddObj(Goal(awards.IsSet(MM45AwardIndex.AspGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.CastleviewGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.LakesideGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.NecropolisGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.NightshadowGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.OlympusGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.RivercityGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.SandcasterGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.ShangriLaGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.VertigoGuildMember)),
                Goal(awards.IsSet(MM45AwardIndex.WinterkillGuildMember)));

            Thief.AddObj(8, Goal(awards.IsSet(MM45AwardIndex.ConvictedThief)));
            for (FileB b = FileB.Case1; b <= FileB.Case8; b++)
            {
                if (fqi.Goal(b) == QuestGoal.Complete)
                    Thief.Obj[(int)(b - FileB.Case1)] = QuestStatus.Single.Invalid("The item has already been stolen.");
            }
            AddQuest(bits.Totals, Thief);

            Bark.AddPre(bits.Item(MM45QuestItemIndex.KeyToTempleOfBark));
            Bark.AddObj(fqi.Dead(FileP.BarkDial1), fqi.Dead(FileP.BarkDial2), fqi.Dead(FileP.BarkDial3),
                fqi.Dead(FileP.BarkDial4), fqi.Dead(FileP.BarkDial5), fqi.Dead(FileP.BarkDial6),
                fqi.Dead(FileP.BarkLever), Goal(awards.IsSet(MM45AwardIndex.DiscipleOfBark)));
            AddQuest(bits.Totals, Bark);

            QuestGoal monk = Goal(awards.IsSet(MM45AwardIndex.MemberDrawkcabBrotherhood));
            Drawkcab.AddPre(QuestStatus.Or(fqi.Dead(FileP.Monk1), monk));
            Drawkcab.AddObj(QuestStatus.Or(fqi.Dead(FileP.Monk2), monk), QuestStatus.Or(fqi.Dead(FileP.Monk3), monk), monk);
            Drawkcab.AddPost(Goal(awards.IsSet(MM45AwardIndex.DrawkcabExtraordinaire)));
            AddQuest(bits.Totals, Drawkcab);

            RatQueen.AddPre(fqi.Goal(FileB.TalkValio));
            if (bits.Dark(Dark.OpenChestCS3026) == QuestGoal.Complete && RatQueen.Pre[0].State != QuestStatus.Basic.Completed)
                RatQueen.Pre[0] = new QuestStatus.Single(QuestStatus.Basic.Unachievable, "Valio is dead");
            RatQueen.AddObj(fqi.Dead(FileP.Rooka));
            RatQueen.LocationsOverride = MapXY.Array(MM5Map.A4CastleviewSewer, fqi.Point(FileP.Rooka));
            RatQueen.AddPost(fqi.Goal(FileB.ReturnValio));
            if (bits.Dark(Dark.OpenChestCS3026) == QuestGoal.Complete && RatQueen.Post[0].State != QuestStatus.Basic.Completed)
                RatQueen.Post[0] = new QuestStatus.Single(QuestStatus.Basic.Unachievable, "Valio is dead");
            AddQuest(bits.Totals, RatQueen);

            Paladin.AddPre(bits.Dark(Dark.AnswerPaladinC21110));
            Paladin.AddObj(fqi.Goal(FileB.Paladin2), fqi.Goal(FileB.Paladin3), fqi.Goal(FileB.Paladin4), fqi.Goal(FileB.Paladin5),
                fqi.Goal(FileB.Paladin6), fqi.Goal(FileB.Paladin7), fqi.Goal(FileB.Paladin8));
            AddQuest(bits.Totals, Paladin);

            switch (mm45Char.Race)
            {
                case MM45Race.Dwarf:
                    Awards.LocationsOverride = new MapXY[] { MM45.Spots.AwardLegendaryDwarf };
                    break;
                case MM45Race.Elf:
                    Awards.LocationsOverride = new MapXY[] { MM45.Spots.AwardLegendaryElf };
                    break;
                case MM45Race.Gnome:
                    Awards.LocationsOverride = new MapXY[] { MM45.Spots.AwardLegendaryGnome };
                    break;
                case MM45Race.HalfOrc:
                    Awards.LocationsOverride = new MapXY[] { MM45.Spots.AwardLegendaryOrc };
                    break;
                default:
                    Awards.LocationsOverride = new MapXY[] { MM45.Spots.AwardLegendaryHuman };
                    break;
            }
            Awards.ItemsOverride = new string[] { Race.RaceString(mm45Char.BasicRace) };

            Awards.AddObj(Goal(awards.IsSet(MM45AwardIndex.CartographersChallenge)),
                Goal(awards.IsSet(MM45AwardIndex.FoundShangriLa)),
                Goal(awards.IsSet(MM45AwardIndex.LoremasterOfWorms)),
                Goal(awards.IsSet(MM45AwardIndex.LoremasterOfLizards)),
                Goal(awards.IsSet(MM45AwardIndex.LoremasterOfSerpents)),
                Goal(awards.IsSet(MM45AwardIndex.LoremasterOfDrakes)),
                Goal(awards.IsSet(MM45AwardIndex.LoremasterOfDragons)),
                Goal(awards.IsSet(MM45AwardIndex.TaxmanEmeritus)),
                Goal(awards.IsSet(MM45AwardIndex.MerchantsChallenge)),
                Goal(awards.IsSet(MM45AwardIndex.Legendary)),
                Goal(awards.IsSet(MM45AwardIndex.PrinceOfThieves)),
                Goal(awards.IsSet(MM45AwardIndex.SuperGoober)),
                Goal(awards.IsSet(MM45AwardIndex.Goober)),
                Goal(awards.IsSet(MM45AwardIndex.SuperiorIntellect)));

            if (mm45Char.Class != MM345Class.Robber)
                Awards.Obj[10] = QuestStatus.Single.Invalid("You are not a Robber");
            AddQuest(bits.Totals, Awards);

            Experience.AddObj(bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol1),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol2),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol3),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol4),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol5),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol6),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol7),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol8),
                bits.Char(iOverrideCharAddress, CharB.BookOfTheDeadVol9),
                bits.Char(iOverrideCharAddress, CharB.ManualOfMasterThievery),
                bits.Char(iOverrideCharAddress, CharB.TomeOfGreatExperience1),
                bits.Char(iOverrideCharAddress, CharB.TomeOfGreatExperience2),
                bits.Char(iOverrideCharAddress, CharB.BookOfDirectives));

            if (mm45Char.Class != MM345Class.Robber && mm45Char.Class != MM345Class.Ninja)
                Experience.Obj[9] = QuestStatus.Single.Invalid("You are not a Robber or Ninja");
            AddQuest(bits.Totals, Experience);

            XeenTraps.AddObj(bits.Cloud(Cloud.DestroyGeneratorXC11414), bits.Cloud(Cloud.DestroyGeneratorXC10114),
                bits.Cloud(Cloud.DestroyGeneratorXC40301), bits.Cloud(Cloud.DestroyGeneratorXC41201), bits.Cloud(Cloud.DestroyMachineXC41111));
            AddQuest(bits.Totals, XeenTraps);

            for(FileB lamp = FileB.Lamp1; lamp <= FileB.Lamp18; lamp++)
                Lamps.AddObj(fqi.Goal(lamp));
            AddQuest(bits.Totals, Lamps);

            for (FileB item = FileB.L7Item1; item <= FileB.L7Item37; item++)
                Level7Items.AddObj(fqi.Goal(item));
            AddQuest(bits.Totals, Level7Items);

            NightshadowWell.AddObj(bits.Clouds(Cloud.SundialTo9N0610, Cloud.SundialTo9N0811, Cloud.SundialTo9N1010));
            NightshadowWell.AddObj(fqi.Goal(FileB.Draco), bits.Cloud(Cloud.DefeatDracoN0114));
            NightshadowWell.AddPost(bits.World(World.NightshadowWellN0707));
            NightshadowWell.LocationsOverride = MapXY.Array(MM4Map.D4Nightshadow, fqi.Point(FileP.Draco));
            AddQuest(bits.Totals, NightshadowWell);

            for (FileP num = FileP.PyNum3; num <= FileP.PyNum10; num++)
                PyramidNumbers.AddObj(fqi.Dead(num));
            PyramidNumbers.AddObj(fqi.Goal(FileB.PyNumLev));
            PyramidNumbers.AddPost(fqi.Goal(FileB.PyNumTreas));
            AddQuest(bits.Totals, PyramidNumbers);

            TreasureTrees.AddObj(bits.Clouds(Cloud.SearchTreeV2829, Cloud.SearchTreeV2028, Cloud.SearchTreeV2628, Cloud.SearchTreeV2327,
                Cloud.SearchTreeV2225, Cloud.SearchTreeV2625, Cloud.SearchTreeV2723, Cloud.SearchTreeV2522, Cloud.SearchTreeV2221, Cloud.SearchTreeV2921,
                Cloud.SearchTreeV1319, Cloud.SearchTreeV1719, Cloud.SearchTreeV1315, Cloud.SearchTreeV1715, Cloud.SearchTreeV1413, Cloud.SearchTreeV1613,
                Cloud.SearchTreeV1412, Cloud.SearchTreeV1612, Cloud.SearchTreeV1411, Cloud.SearchTreeV1409, Cloud.SearchTreeV1408, Cloud.SearchTreeV1608,
                Cloud.SearchTreeV1306, Cloud.SearchTreeV1705, Cloud.SearchTreeV1703, Cloud.SearchTreeV1302));
            for (FileB tree = FileB.Tree1; tree <= FileB.Tree31; tree++)
                TreasureTrees.AddObj(fqi.Goal(tree));
            AddQuest(bits.Totals, TreasureTrees);

            FileP[] barkSkulls = new FileP[] { FileP.BSkull1, FileP.BSkull2, FileP.BSkull3, FileP.BSkull4, FileP.BSkull6, FileP.BSkull5, FileP.BSkull7, FileP.BSkull8 };
            int iSkull = 0;
            for (FileB bark = FileB.Bark1; bark <= FileB.Bark32; bark += 4)
            {
                BarkmanSkulls.AddObj(fqi.Goal(bark), fqi.Goal(bark+1), fqi.Goal(bark+2), fqi.Goal(bark+3));
                BarkmanSkulls.AddObj(fqi.Dead(barkSkulls[iSkull++]));
            }
            BarkmanSkulls.AddObj(fqi.Dead(FileP.Barkman));
            for (FileB bark = FileB.BarkTr1; bark <= FileB.BarkTr3; bark++)
                BarkmanSkulls.AddPost(fqi.Goal(bark));
            BarkmanSkulls.LocationsOverride = MapXY.Array(MM5Map.C4TempleOfBarkLevel5, fqi.Point(FileP.Barkman));
            AddQuest(bits.Totals, BarkmanSkulls);

            AspWell.AddPre(bits.Cloud(Cloud.PedestalBlueA0704));
            AspWell.AddObj(QuestStatus.Not(bits.Cloud(Cloud.PedestalBlueA0904)),
                bits.Cloud(Cloud.PedestalBlueA0902), QuestStatus.Not(bits.Cloud(Cloud.PedestalBlueA0702)), bits.Cloud(Cloud.DestroyTransformerA0814));
            AspWell.AddPost(bits.World(World.AspWellA0803));
            AddQuest(bits.Totals, AspWell);

            LostSouls.AddPre(bits.Item(MM45QuestItemIndex.KeyToDungeonOfLostSouls));
            LostSouls.AddObj(bits.Darks(Dark.FlipHourglassLSD10209, Dark.FlipHourglassLSD10510, Dark.FlipHourglassLSD11110, Dark.FlipHourglassLSD11409,
                Dark.DrinkWaterLSD24040, Dark.DrinkWaterLSD24040, Dark.DrinkWaterLSD24040, Dark.PullLeverLSD30110, Dark.PullLeverLSD31314,
                Dark.PullLeverLSD30101, Dark.PullLeverLSD31402));
            LostSouls.AddObj(fqi.Dead(FileP.LeverLSD1), fqi.Dead(FileP.LeverLSD2), fqi.Goal(FileB.PayLSD));
            AddQuest(bits.Totals, LostSouls);

            AlamarClock.AddObj(fqi.MultiDead(FileP.Time1, FileP.Time2, FileP.Time3, FileP.Time4));
            AlamarClock.AddPost(fqi.Dead(FileP.Time5));
            AddQuest(bits.Totals, AlamarClock);

            PyramidTorches.AddObj(fqi.MultiDead(FileP.Torch4, FileP.Torch5, FileP.Torch6, FileP.Torch3, FileP.Torch2, FileP.Torch1));
            AddQuest(bits.Totals, PyramidTorches);

            CompletedQuests = bits.Totals.Completed;
            TotalQuests = bits.Totals.All;
        }
    }
}