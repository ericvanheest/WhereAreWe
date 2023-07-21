using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// These aliases make the SetQuests() function look slightly less awkward
using FileB = WhereAreWe.MM3FileQuestInfo.ByteIndex;
using PartyB = WhereAreWe.MM3Bits.Party;

namespace WhereAreWe
{
    public class MM3QuestData : QuestData
    {
        public byte[] PartyBits;
        public MM3FileQuestInfo FileQuestInfo;

        public MM3QuestData(MM3PartyInfo party, MM3GameInfo info, byte[] partyBits, MM3FileQuestInfo fqi)
        {
            Party = party;
            Info = info;
            PartyBits = partyBits;
            FileQuestInfo = fqi;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            Global.WriteBytes(stream, PartyBits);
        }
    }

    namespace MM3QuestStates
    {
        [Flags]
        public enum Main
        {
            NotStarted = 0x00,
            FoundFortressKeys = 0x01,
            FoundSequencingCards = 0x02,
            DeliveredPowerOrbs = 0x04,
            FoundPyramidKey = 0x08,
            UltimateAdventurer = 0x10,
            Completed = 0x80
        }

        [Flags]
        public enum FortressKeys
        {
            NotStarted = 0x0000,
            Yellow = 0x0001,
            Green = 0x0002,
            Blue = 0x0004,
            Red = 0x0008,
            Black = 0x0010,
            Gold = 0x0020,
            All = 0x003F,
        }

        [Flags]
        public enum SequencingCards
        {
            NotStarted = 0x0000,
            Card001 = 0x0001,
            Card002 = 0x0002,
            Card003 = 0x0004,
            Card004 = 0x0008,
            Card005 = 0x0010,
            Card006 = 0x0020,
            All = 0x003F,
        }

        [Flags]
        public enum Blessed
        {
            NotStarted = 0x0000,
            FountainHead = 0x0080,
            Baywatch = 0x0040,
            Wildabar = 0x0020,
            SwampTown = 0x0010,
            BlisteringHeights = 0x0008,
            AllTowns = 0x00F8,
            PrayedAtShrine = 0x0100,
            All = 0x01F8,
            Finished = 0x8000
        }

        [Flags]
        public enum Guilds
        {
            NotStarted = 0x0000,
            RavensGuild = 0x0001,
            AlbatrossGuild = 0x0002,
            FalconsGuild = 0x0004,
            BuzzardsGuild = 0x0008,
            EaglesGuild = 0x0010,
            All = 0x001F,
        }

        [Flags]
        public enum Skills
        {
            NotStarted = 0x00000000,
            Thievery = 0x00000001,
            ArmsMaster = 0x00000002,
            Astrologer = 0x00000004,
            BodyBuilder = 0x00000008,
            Cartographer = 0x00000010,
            Crusader = 0x00000020,
            DirectionSense = 0x00000040,
            Linguist = 0x00000080,
            Merchant = 0x00000100,
            Mountaineer = 0x00000200,
            Navigator = 0x00000400,
            PathFinder = 0x00000800,
            PrayerMaster = 0x00001000,
            Prestidigitator = 0x00002000,
            Swimmer = 0x00004000,
            Tracker = 0x00008000,
            SpotSecretDoors = 0x00010000,
            DangerSense = 0x00020000,
            All = 0x0003ffff
        }

        public class Orbs
        {
            public bool Started;
            public bool Completed;
            public bool FoundOrbHallsOfInsanity0318;
            public bool FoundOrbHallsOfInsanity2803;
            public bool FoundOrbCathedralOfCarnage2517;
            public bool FoundOrbCathedralOfCarnage2515;
            public bool FoundOrbDarkWarriorKeep3002;
            public bool FoundOrbDarkWarriorKeep3001;
            public bool FoundOrbDragonCavern2105;
            public bool FoundOrbDragonCavern2705;
            public bool FoundOrbDragonCavern1303;
            public bool FoundOrbDragonCavern0201;
            public bool FoundOrbTombOfTerror1206;
            public bool FoundOrbTombOfTerror1202;
            public bool FoundOrbTheMazeFromHell3031;
            public bool FoundOrbTheMazeFromHell0130;
            public bool FoundOrbTheMazeFromHell1919;
            public bool FoundOrbTheMazeFromHell0101;
            public bool FoundOrbAftStorageSector0114;
            public bool FoundOrbAftStorageSector0112;
            public bool FoundOrbAftStorageSector1408;
            public bool FoundOrbBetaEngineSector0115;
            public bool FoundOrbBetaEngineSector0107;
            public bool FoundOrbBetaEngineSector1407;
            public bool FoundOrbBetaEngineSector1001;
            public bool FoundOrbMainEngineSector0108;
            public bool FoundOrbMainEngineSector0808;
            public bool FoundOrbMainEngineSector1108;
            public bool FoundOrbMainEngineSector1408;
            public bool FoundOrbAlphaEngineSector0014;
            public bool FoundOrbAlphaEngineSector1509;
            public bool FoundOrbAlphaEngineSector0004;
            public bool FoundOrbAlphaEngineSector1501;
            public int OrbsInInventory;
            public int DeliveredGood;
            public int DeliveredNeutral;
            public int DeliveredEvil;
            public bool HasPassCard;
        }

        public class Skulls
        {
            public bool Started;
            public bool Completed;
            public int SkullsInInventory;
            public int SkullsDelivered;
            public bool FoundSkullA1AncientTempleOfMoo2715;
            public bool FoundSkullA1AncientTempleOfMoo1707;
            public bool FoundSkullA1AncientTempleOfMoo2706;
            public bool FoundSkullA1AncientTempleOfMoo0704;
            public bool FoundSkullB1CyclopsCavern1208;
            public bool FoundSkullB1CyclopsCavern1108;
            public bool FoundSkullA1FountainHeadCavern0014;
            public bool FoundSkullA1FountainHeadCavern0613;
            public bool FoundSkullA1FountainHeadCavern1511;
            public bool FoundSkullA1FountainHeadCavern1410;
            public bool FoundSkullA1FountainHeadCavern0706;
            public bool FoundSkullA1FountainHeadCavern0505;
            public bool FoundSkullA1FountainHeadCavern0703;
            public bool FoundSkullA1FountainHeadCavern1503;
            public bool FoundSkullA1FountainHeadCavern0102;
            public bool FoundSkullA1FountainHeadCavern1401;
            public bool FoundSkullA2BaywatchCavern0014;
            public bool FoundSkullA2BaywatchCavern0012;
            public bool FoundSkullA2BaywatchCavern0505;
            public bool FoundSkullA2BaywatchCavern0705;
        }

        public class Shells
        {
            public int ShellsInInventory;
            public int ShellsDelivered;
            public int CurrentDay;
        }

        public class Arena
        {
            public int ArenaWins;
        }

        public class Pearls
        {
            public int PearlsInInventory;
            public int PearlsDelivered;
            public int CurrentDay;
            public bool FoundPearlB1SlithercultStronghold0227;
            public bool FoundPearlB2FortressOfFear2401;
            public bool FoundPearlD1CursedColdCavern0928;
            public bool FoundPearlD1CursedColdCavern2219;
            public bool FoundPearlD3BlisteringHeightsCavern0713;
            public bool FoundPearlD3BlisteringHeightsCavern0913;
        }

        public class Artifacts
        {
            public bool MetPraythos;
            public bool MetChathos;
            public bool MetPathos;
            public int EvilInInventory;
            public int NeutralInInventory;
            public int GoodInInventory;
            public int EvilDelivered;
            public int NeutralDelivered;
            public int GoodDelivered;

            public bool FoundEvilA2CastleWhiteshield0006;
            public bool FoundEvilA2CastleWhiteshield0010;
            public bool FoundEvilA3HallsOfInsanity1026;
            public bool FoundEvilA3HallsOfInsanity1626;
            public bool FoundEvilB4CastleBloodReign0800;
            public bool FoundEvilD1CursedColdCavern1214;
            public bool FoundEvilD1CursedColdCavern1215;
            public bool FoundEvilD3BlisteringHeights0102;
            public bool FoundEvilE2SwampTown0901;
            public bool FoundEvilE2SwampTown1401;
            public bool FoundEvilE2SwampTownCavern0115;
            public bool FoundEvilE2SwampTownCavern0415;
            public bool FoundEvilF2TombOfTerror0802;
            public bool FoundEvilF2TombOfTerror0806;
            public bool FoundGoodB1CyclopsCavern0202;
            public bool FoundGoodB1CyclopsCavern2901;
            public bool FoundGoodB2FortressOfFear0214;
            public bool FoundGoodB2FortressOfFear2830;
            public bool FoundGoodB4CastleBloodReign0400;
            public bool FoundGoodB4CastleBloodReign0600;
            public bool FoundEvilB4CastleBloodReign1000;
            public bool FoundGoodD1CursedColdCavern2424;
            public bool FoundGoodD1CursedColdCavern2722;
            public bool FoundGoodD3BlisteringHeights0201;
            public bool FoundGoodF2TombOfTerror0509;
            public bool FoundGoodF2TombOfTerror1109;
            public bool FoundNeutB1CyclopsCavern0402;
            public bool FoundNeutB1CyclopsCavern2500;
            public bool FoundNeutB1SlithercultStronghold2828;
            public bool FoundNeutB2FortressOfFear0501;
            public bool FoundNeutB3DarkWarriorKeep1501;
            public bool FoundNeutB3DarkWarriorKeep1601;
            public bool FoundNeutD1CursedColdCavern1200;
            public bool FoundNeutD1CursedColdCavern2005;
            public bool FoundNeutD3BlisteringHeights0105;
            public bool FoundNeutE1CastleDragontooth1212;
            public bool FoundNeutE1CastleDragontooth1515;

            public bool Completed
            {
                get
                {
                    return EvilDelivered == 15 && GoodDelivered == 11 && NeutralDelivered == 11;
                }
            }

            public bool NotStarted
            {
                get
                {
                    return EvilDelivered == 0 &&
                            EvilInInventory == 0 &&
                            NeutralInInventory == 0 &&
                            NeutralDelivered == 0 &&
                            GoodInInventory == 0 &&
                            GoodDelivered == 0 &&
                            !MetPraythos &&
                            !MetChathos &&
                            !MetPathos &&
                            !FoundEvilA2CastleWhiteshield0006 &&
                            !FoundEvilA2CastleWhiteshield0010 &&
                            !FoundEvilA3HallsOfInsanity1026 &&
                            !FoundEvilA3HallsOfInsanity1626 &&
                            !FoundEvilB4CastleBloodReign0800 &&
                            !FoundEvilD1CursedColdCavern1214 &&
                            !FoundEvilD1CursedColdCavern1215 &&
                            !FoundEvilD3BlisteringHeights0102 &&
                            !FoundEvilE2SwampTown0901 &&
                            !FoundEvilE2SwampTown1401 &&
                            !FoundEvilE2SwampTownCavern0115 &&
                            !FoundEvilE2SwampTownCavern0415 &&
                            !FoundEvilF2TombOfTerror0802 &&
                            !FoundEvilF2TombOfTerror0806 &&
                            !FoundGoodB1CyclopsCavern0202 &&
                            !FoundGoodB1CyclopsCavern2901 &&
                            !FoundGoodB2FortressOfFear0214 &&
                            !FoundGoodB2FortressOfFear2830 &&
                            !FoundGoodB4CastleBloodReign0400 &&
                            !FoundGoodB4CastleBloodReign0600 &&
                            !FoundEvilB4CastleBloodReign1000 &&
                            !FoundGoodD1CursedColdCavern2424 &&
                            !FoundGoodD1CursedColdCavern2722 &&
                            !FoundGoodD3BlisteringHeights0201 &&
                            !FoundGoodF2TombOfTerror0509 &&
                            !FoundGoodF2TombOfTerror1109 &&
                            !FoundNeutB1CyclopsCavern0402 &&
                            !FoundNeutB1CyclopsCavern2500 &&
                            !FoundNeutB1SlithercultStronghold2828 &&
                            !FoundNeutB2FortressOfFear0501 &&
                            !FoundNeutB3DarkWarriorKeep1501 &&
                            !FoundNeutB3DarkWarriorKeep1601 &&
                            !FoundNeutD1CursedColdCavern1200 &&
                            !FoundNeutD1CursedColdCavern2005 &&
                            !FoundNeutD3BlisteringHeights0105 &&
                            !FoundNeutE1CastleDragontooth1212 &&
                            !FoundNeutE1CastleDragontooth1515;
                }
            }
        }

        public class Trueberry
        {
            public bool MetTrueberry;
            public int LoveNeeded;
            public int CharsInLove;
            public bool NotStarted { get { return !MetTrueberry && LoveNeeded > 0; } }
        }

        [Flags]
        public enum Alacorn
        {
            NotStarted = 0,
            HornInInventory = 0x0001,
            HornDelivered = 0x0002,
            MetBirds = 0x0004,
            Completed = 0x8000
        }

        [Flags]
        public enum Greywind
        {
            NotStarted = 0,
            SandTopNW = 0x0001,
            SandTopNE = 0x0002,
            SandTopSW = 0x0004,
            SandTopSE = 0x0008,
            EnteredCastle = 0x0010,
            AcceptedQuest = 0x0020,
            RungGong = 0x0040,
            RemovedWall = 0x0080,
            Completed = 0x8000
        }

        [Flags]
        public enum Blackwind
        {
            NotStarted = 0,
            SacrificedBloodMane = 0x0001,
            SacrificedTempestStorm = 0x0002,
            SacrificedHamonOthreute = 0x0004,
            MetBlackwind = 0x0008,
            Completed = 0x8000
        }

        public class MummyKing
        {
            public bool Started;
            public bool Completed;
            public bool HaveKey;
            public bool FoundKey;
            public bool HeadNorthWest;
            public bool HeadNorthEast;
            public bool HeadSouthWest;
            public bool HeadSouthEast;
            public bool FieldNorth;
            public bool FieldEast;
            public bool FieldSouth;
            public bool FieldWest;
            public bool FoundCard;
        }

        public class Generators
        {
            public bool OrcOutpostA1Surface0503;
            public bool GoblinWagonA1Surface1207;
            public bool OrcishShrineA2Surface0404;
            public bool OrcOutpostA2Surface0407;
            public bool GoblinHutA2Surface0805;
            public bool ScreamerWagonA3Surface0309;
            public bool VampireBatWagonA3Surface1504;
            public bool SpidersNestA4Surface0512;
            public bool MagicMantisPodsA4Surface1504;
            public bool LampreyB3Surface1210;
            public bool BugabooLarvaeB3Surface0303;
            public bool OgreMeetingHallB2Surface1104;
            public bool SpriteHutB2Surface0610;
            public bool WildFungusSporesB1Surface0503;
            public bool OhNoBugApiaryB1Surface1208;
            public bool CyclopsShackC1Surface0612;
            public bool SpriteHutC1Surface0604;
            public bool WerewolfShrineC1Surface1409;
            public bool MajorDevilPortalC2Surface1102;
            public bool HydraBreedingGroundsC3Surface0709;
            public bool MajorDemonHutD3Surface0608;
            public bool FireLizardHatcheryD2Surface01001;
            public bool FireStalkerLairD2Surface0510;
            public bool DeathLocustNestE2Surface0311;
            public bool RogueMeetingPlaceE2Surface0611;
            public bool BarbarianCompoundE4Surface0507;
            public bool DeathLocustNestE4Surface0908;

            public bool NotStarted
            {
                get
                {
                    return !OrcOutpostA1Surface0503 &&
                            !GoblinWagonA1Surface1207 &&
                            !OrcishShrineA2Surface0404 &&
                            !OrcOutpostA2Surface0407 &&
                            !GoblinHutA2Surface0805 &&
                            !ScreamerWagonA3Surface0309 &&
                            !VampireBatWagonA3Surface1504 &&
                            !SpidersNestA4Surface0512 &&
                            !MagicMantisPodsA4Surface1504 &&
                            !LampreyB3Surface1210 &&
                            !BugabooLarvaeB3Surface0303 &&
                            !OgreMeetingHallB2Surface1104 &&
                            !SpriteHutB2Surface0610 &&
                            !WildFungusSporesB1Surface0503 &&
                            !OhNoBugApiaryB1Surface1208 &&
                            !CyclopsShackC1Surface0612 &&
                            !SpriteHutC1Surface0604 &&
                            !WerewolfShrineC1Surface1409 &&
                            !MajorDevilPortalC2Surface1102 &&
                            !HydraBreedingGroundsC3Surface0709 &&
                            !MajorDemonHutD3Surface0608 &&
                            !FireLizardHatcheryD2Surface01001 &&
                            !FireStalkerLairD2Surface0510 &&
                            !DeathLocustNestE2Surface0311 &&
                            !RogueMeetingPlaceE2Surface0611 &&
                            !BarbarianCompoundE4Surface0507 &&
                            !DeathLocustNestE4Surface0908;
                }
            }

            public bool Completed
            {
                get
                {
                    return OrcOutpostA1Surface0503 &&
                            GoblinWagonA1Surface1207 &&
                            OrcishShrineA2Surface0404 &&
                            OrcOutpostA2Surface0407 &&
                            GoblinHutA2Surface0805 &&
                            ScreamerWagonA3Surface0309 &&
                            VampireBatWagonA3Surface1504 &&
                            SpidersNestA4Surface0512 &&
                            MagicMantisPodsA4Surface1504 &&
                            LampreyB3Surface1210 &&
                            BugabooLarvaeB3Surface0303 &&
                            OgreMeetingHallB2Surface1104 &&
                            SpriteHutB2Surface0610 &&
                            WildFungusSporesB1Surface0503 &&
                            OhNoBugApiaryB1Surface1208 &&
                            CyclopsShackC1Surface0612 &&
                            SpriteHutC1Surface0604 &&
                            WerewolfShrineC1Surface1409 &&
                            MajorDevilPortalC2Surface1102 &&
                            HydraBreedingGroundsC3Surface0709 &&
                            MajorDemonHutD3Surface0608 &&
                            FireLizardHatcheryD2Surface01001 &&
                            FireStalkerLairD2Surface0510 &&
                            DeathLocustNestE2Surface0311 &&
                            RogueMeetingPlaceE2Surface0611 &&
                            BarbarianCompoundE4Surface0507 &&
                            DeathLocustNestE4Surface0908;

                }
            }
        }

        public class GreekBrothers
        {
            public bool VisitedAlpha;
            public bool VisitedBeta;
            public bool VisitedGamma;
            public bool VisitedDelta;
            public bool VisitedZeta;
            public int CoinsInInventory;

            public bool NotStarted
            {
                get { return !VisitedAlpha && !VisitedBeta && !VisitedGamma && !VisitedDelta && !VisitedZeta && CoinsInInventory == 0; }
            }

            public bool Completed
            {
                get { return VisitedAlpha && VisitedBeta && VisitedGamma && VisitedDelta && VisitedZeta && CoinsInInventory == 0; }
            }
        }

        public class WellOfRemembrance
        {
            public bool Slayer;
            public bool Obeyer;
            public bool Purveyor;
            public bool Soothsayer;
            public bool Completed;

            public bool NotStarted { get { return !Slayer && !Obeyer && !Purveyor && !Soothsayer && !Completed; } }
        }

        public class Cathedral
        {
            public bool ProtoNorth;
            public bool BarytroWest;
            public bool DynatroNorth;
            public bool PenetroEast;
            public bool PositroSouth;
            public bool AnsweredWeeds;
            public bool DrankCup1;
            public bool DrankCup2;
            public bool DrankCup3;
            public bool DrankCup4;
            public bool DrankCup5;
            public bool DrankCup6;
            public bool Completed;

            public bool NotStarted
            {
                get
                {
                    // Skipping Proto and Dynatro because they start in the correct position anyway
                    return !BarytroWest && !PenetroEast && !PositroSouth && !AnsweredWeeds &&
                        !DrankCup1 && !DrankCup2 && !DrankCup3 && !DrankCup4 && !DrankCup5 && !DrankCup6 && !Completed;
                }
            }
        }

        public class Crystals
        {
            public bool Crystal1308;
            public bool Crystal1707;
            public bool Crystal1110;
            public bool Crystal0818;
            public bool Crystal1324;
            public bool Crystal2020;
            public bool Crystal2318;
            public bool Crystal1611;
            public bool Crystal1413;
            public bool Crystal1417;
            public bool Crystal1618;
            public bool Crystal1818;
            public bool Crystal2016;
            public bool AnsweredRiddle;

            public bool ReadyForReset
            {
                get
                {
                    return Crystal1308 && Crystal1707 && Crystal1110 && Crystal0818 && Crystal1324 && Crystal2020 &&
                           Crystal2318 && Crystal1611 && Crystal1413 && Crystal1417 && Crystal1618 && Crystal1818 && Crystal2016;
                }
            }
        }

        public class SunkenIsle
        {
            public bool PyramidKeycard;
            public bool UsedPassword;

            public bool Completed { get { return UsedPassword; } }
            public bool NotStarted { get { return !PyramidKeycard && !UsedPassword; } }
        }

        public class Jewelry
        {
            public bool JewelryA2BaywatchCavern0611;
            public bool JewelryA2BaywatchCavern0811;
            public bool JewelryA2BaywatchCavern0703;
            public bool JewelryA2BaywatchCavern1403;
            public bool JewelryE2SwampTown0112;
            public bool JewelryE2SwampTown1404;
            public bool JewelryE2SwampTown1101;
            public bool JewelryE2SwampTown1201;
            public bool JewelryA1AncientTempleOfMoo2627;
            public bool JewelryA1AncientTempleOfMoo1424;
            public bool JewelryB1CyclopsCavern2827;
            public bool JewelryB1CyclopsCavern0916;

            public bool Completed
            {
                get
                {
                    return JewelryA2BaywatchCavern0611 && JewelryA2BaywatchCavern0811 && JewelryA2BaywatchCavern0703 && JewelryA2BaywatchCavern1403 &&
                           JewelryE2SwampTown0112 && JewelryE2SwampTown1404 && JewelryE2SwampTown1101 && JewelryE2SwampTown1201 &&
                           JewelryA1AncientTempleOfMoo2627 && JewelryA1AncientTempleOfMoo1424 && JewelryB1CyclopsCavern2827 && JewelryB1CyclopsCavern0916;
                }
            }

            public bool NotStarted
            {
                get
                {
                    return !JewelryA2BaywatchCavern0611 && !JewelryA2BaywatchCavern0811 && !JewelryA2BaywatchCavern0703 && !JewelryA2BaywatchCavern1403 &&
                           !JewelryE2SwampTown0112 && !JewelryE2SwampTown1404 && !JewelryE2SwampTown1101 && !JewelryE2SwampTown1201 &&
                           !JewelryA1AncientTempleOfMoo2627 && !JewelryA1AncientTempleOfMoo1424 && !JewelryB1CyclopsCavern2827 && !JewelryB1CyclopsCavern0916;
                }
            }
        }
    }

    public static class MM3Bits
    {
        public enum Party
        {
            Bit0 = 0,
            VisitedBrotherAlpha = 1,                 // Set when Brother Alpha is visited (A-2, Baywatch 11,14)
            VisitedBrotherBeta = 2,                  // Set when Brother Beta is visited (A-2, Baywatch Cavern 15,9)
            VisitedBrotherGamma = 3,                 // Set when Brother Gamma is visited (B-4, Wildabar 8,1)
            AcceptedKranionQuest = 4,                // Set when you accept Kranion's quest (A-1, Fountain Head 14,5)
            OneSkullToKranion = 5,                   // Set when you give the first skull to Kranion (A-1, Fountain Head 14,5)
            TwoSkullsToKranion = 6,                  // Set when you give the second skull to Kranion (A-1, Fountain Head 14,5)
            ThreeSkullsToKranion = 7,                // Set when you give the third skull to Kranion (A-1, Fountain Head 14,5)
            FourSkullsToKranion = 8,                 // Set when you give the fourth skull to Kranion (A-1, Fountain Head 14,5)
            FiveSkullsToKranion = 9,                 // Set when you give the fifth skull to Kranion (A-1, Fountain Head 14,5)
            VisitedBrotherDelta = 10,                // Set when Brother Delta is Visited (B-4, Wildabar Cavern 15,12)
            LeverB4WildabarCavern0703 = 12,          // Set when lever pulled (B-4, Wildabar Cavern 7,3)
            LeverB4WildabarCavern1103 = 13,          // Set when lever pulled (B-4, Wildabar Cavern 11,3)
            LeverB4WildabarCavern0411 = 14,          // Set when lever pulled (B-4, Wildabar Cavern 4,11)
            LeverB4WildabarCavern1509 = 15,          // Set when lever pulled (B-4, Wildabar Cavern 15,9)
            LeverB2FortressOfFear0230And0521 = 16,   // Set via lever at (2,30), Zero via lever at (5,21) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear0729And1029 = 17,   // Set via lever at (7,29), Zero via lever at (10,29) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear1429And1822 = 18,   // Set via lever at (14,29), Zero via lever at (18,22) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear0117And0118 = 19,   // Set via lever at (1,17), Zero via lever at (1,18) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear2108And2430 = 20,   // Set via lever at (21,8), Zero via lever at (24,30) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear1009And0612 = 21,   // Set via lever at (10,9), Zero via lever at (6,12) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear1108And2107 = 22,   // Set via lever at (11,8), Zero via lever at (21,7) (B-2, Fortress Of Fear)
            LeverB2FortressOfFear1209And1512 = 23,   // Set via lever at (12,9), Zero via lever at (15,2) (B-2, Fortress Of Fear)
            CrystalB4ArachnoidCavern1308 = 24,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 13,8)
            CrystalB4ArachnoidCavern1707 = 25,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 17,7)
            CrystalB4ArachnoidCavern1110 = 26,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 11,10)
            CrystalB4ArachnoidCavern0818 = 27,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 8,18)
            CrystalB4ArachnoidCavern1324 = 28,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 13,24)
            CrystalB4ArachnoidCavern2020 = 29,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 20,20)
            CrystalB4ArachnoidCavern2318 = 30,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 23,18)
            CrystalB4ArachnoidCavern1611 = 31,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 16,11)
            CrystalB4ArachnoidCavern1413 = 32,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 14,13)
            CrystalB4ArachnoidCavern1417 = 33,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 14,17)
            CrystalB4ArachnoidCavern1618 = 34,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 16,18)
            CrystalB4ArachnoidCavern1818 = 35,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 18,18)
            CrystalB4ArachnoidCavern2016 = 36,       // Set when you interact with the crystal (B-4, Arachnoid Cavern 20,16)
            RiddleTears = 37,                        // Set if you answer "tears" (A-3, Halls Of Insanity 11,12)
            RiddleEyes = 38,                         // Set if you answer "eyes" (A-3, Halls Of Insanity 17,12)
            RiddleBlink = 39,                        // Set if you answer "blink" (A-3, Halls Of Insanity 14,9)
            SolvedCathedralOfCarnage = 40,           // Set if you solve the puzzles (B-3, Cathedral Of Carnage 25,19)
            PositroNorth = 42,                       // Set if Altar Of Positro is turned to face North (B-3, Cathedral Of Carnage 25,28)
            PositroEast = 43,                        // Set if Altar Of Positro is turned to face East (B-3, Cathedral Of Carnage 25,28)
            PositroSouth = 44,                       // Set if Altar Of Positro is turned to face South (B-3, Cathedral Of Carnage 25,28)
            PositroWest = 45,                        // Set if Altar Of Positro is turned to face West (B-3, Cathedral Of Carnage 25,28)
            RiddleWeeds = 46,                        // Set if you answer "weeds" (B-3, Cathedral Of Carnage 1,26)
            ChaliceB3CathedralOfCarnage1014 = 47,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 10,14)
            ChaliceB3CathedralOfCarnage1016 = 48,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 10,16)
            ChaliceB3CathedralOfCarnage0714 = 49,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 7,14)
            ChaliceB3CathedralOfCarnage0716 = 50,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 7,16)
            ChaliceB3CathedralOfCarnage0414 = 51,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 4,14)
            ChaliceB3CathedralOfCarnage0416 = 52,    // Set if you drink from the chalice (B-3, Cathedral Of Carnage 4,16)
            UltimateAdventurer = 53,                 // Set if you drink from the water (F-3, The Maze from Hell 14,19)
            AnyOrbsToZealot = 54,                    // Set in unreachable code (A-2, Castle Whiteshield, 37, 1)
            WhiteshieldDestroyed = 65,               // Set if Castle Whiteshield is destroyed (B-4, Castle Blood Reign 7,5)
            WhiteshieldBoxOpened = 66,               // Set in A-2, Castle Whiteshield when opening boxes (2,8) (7,3) (7,13) (8,3) (8,13)
            BloodReignDestroyed = 78,                // Set if Castle Blood Reign is destroyed (A-2, Castle Whiteshield 5,8)
            DragontoothDestroyed = 91,               // Set if Castle Dragontooth is destroyed (A-2, Castle Whiteshield 5,8)
            GreywindTasksCompleted = 93,             // Set when you have completed Greywind's tasks (C-4, Greywind Dungeon 1,11)
            BlackwindReleased = 96,                  // Set when you release Blackwind (D-4, Castle Blackwind 8,0)
            StatueHamonOthreute = 97,                // Set when you activate Hamon Othreute statue (D-4, Blackwind Dungeon 13,0)
            StatueTempestStorm = 98,                 // Set when you activate Tempest Storm statue (D-4, Blackwind Dungeon 0,0)
            StatueBloodMane = 99,                    // Set when you activate Blood Mane statue (D-4, Blackwind Dungeon 0,15)
            HourglassC4GreywindDungeon0101 = 100,    // Set if the sand is at the top Of the hourglass (C-4, Greywind Dungeon 1,1)
            HourglassC4GreywindDungeon1401 = 101,    // Set if the sand is at the top Of the hourglass (C-4, Greywind Dungeon 14,1)
            HourglassC4GreywindDungeon1414 = 102,    // Set if the sand is at the top Of the hourglass (C-4, Greywind Dungeon 14,14)
            HourglassC4GreywindDungeon0114 = 103,    // Set if the sand is at the top Of the hourglass (C-4, Greywind Dungeon 1,14)
            EncounterBlackwind = 104,                // Set when you first talk to Blackwind (D-4, Castle Blackwind 8,0)
            RaiseSunkenIsle = 110,                   // Set when you raise the sunken isle (A-2, Forward Storage Sector 9,11)
            DestroyOrcOutpostA1 = 120,               // Set when you destroy the Orc outpost (A-1, Surface: 5,3)
            DestroyGoblinWagonA1 = 121,              // Set when you destroy the Goblin wagon (A-1, Surface: 12,7)
            DestroyOrcOutpostA2 = 122,               // Set when you destroy the Orc outpost (A-2, Surface: 4,7)
            DestroyGoblinHutA2 = 123,                // Set when you destroy the Goblin hut (A-2, Surface: 8,5)
            DestroyScreamerPodsA3 = 124,             // Set when you destroy the Screamer pods (A-3, Surface: 3,9)
            DestroyBatWagonA3 = 125,                 // Set when you destroy the Vampire Bat wagon (A-3, Surface: 15,4)
            DestroyMantisPodsA4 = 126,               // Set when you destroy the Magic Mantis pods (A-4, Surface: 15,4)
            ResurrectSpidersA4 = 127,                // Never set; Checked when you enter the square (A-4, Surface: 15,6)
            DestroyApiaryB1 = 128,                   // Set when you destroy the Oh No Bug apiary (B-1, Surface: 12,8)
            DestroySporesB1 = 129,                   // Set when you destroy the Wild Fungus spores (B-1, Surface: 5,3)
            DestroySpriteHut = 130,                  // Set when you destroy the Sprite hut (B-2, Surface 3,11)
            ResurrectSpritesB2 = 131,                // Never set; Checked when you enter the square (B-2, Surface: 5,12)
            DestroyLampreyB3 = 132,                  // Set when you kill the Lamprey (B-3, Surface: 12,10)
            DestroyGrubsB3 = 133,                    // Set when you destroy the Bugaboo grubs (B-3, Surface: 3,3)
            DestroyCyclopsShackC1 = 134,             // Set when you destroy the Cyclops shack (C-1, Surface: 6,12)
            DestroySpriteHutC1 = 135,                // Set when you destroy the Sprite hut (C-1, Surface: 6,4)
            DesecratedShrine = 136,                  // Desecrated shrine in C-1, Surface 14,9, checked in D-1, Surface 0,8
            DestroyLocustNestE4 = 137,               // Set when you destroy the Death Locust nest (E-4, Surface: 9,8)
            DestroyBarbarianE4 = 138,                // Set when you destroy the Barbarian compound (E-4, Surface: 5,7)
            RememberedPurveyor = 139,                // Set by Purveyor (F-3, Surface 4,15), Removed by Betrayer (F-3, Surface 3,11)
            RememberedSoothsayer = 140,              // Set by Soothdayer (F-3, Surface 9,14), Removed by Betrayer (F-3, Surface 3,11)
            RememberedSlayer = 141,                  // Set by Slayer (F-3, Surface 6,6), Removed by Betrayer (F-3, Surface 3,11)
            RememberedObeyer = 142,                  // Set by Obeyer (F-3, Surface 2,5), Removed by Betrayer (F-3, Surface 3,11)
            DestroyedRogueHut = 143,                 // Set when you destroy Rogue Hut (E-2, Surface 6,11)
            DestroyedDeathLocustNest = 144,          // Set when you destroy Death Locust nest (E-22, Surface 3,11)
            OneLoveToTrueberry = 145,                // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            TwoLoveToTrueberry = 146,                // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            ThreeLoveToTrueberry = 147,              // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            FourLoveToTrueberry = 148,               // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            FiveLoveToTrueberry = 149,               // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            SixLoveToTrueberry = 150,                // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            SevenLoveToTrueberry = 151,              // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            EightLoveToTrueberry = 152,              // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            NineLoveToTrueberry = 153,               // Set when you bring "In Love" to Princess Trueberry (E-2, Surface 4,5)
            FreedTrueberry = 155,                    // Set when you free Princess Trueberry (E-2, Surface 4,5)
            StartleAthea = 157,                      // Set when you startle Athea (A-4, Surface: 0,0)
            EncounterPirateQueen = 158,              // Set when you encounter the Pirate Queen (D-2, Surface 9,14 11,12 13,13 14,10)
            EncounterTrueberry = 159,                // Set when you encounter Princess Trueberry (E-2, Surface 4,5)
            EncounterIcarusBirds = 160,              // Set whn you encounter the Birds of Icarus (A-2, Surface 9,2)
            EnterCastleGreywind = 161,               // Set the first time you enter Castle Greywind (C-4, Surface 5,8)
            EnterCastleBlackwind = 162,              // Set the first time you enter Castle Blackwind (D-4, Surface 6,8)
            EncounterPraythos = 163,                 // Set the first time you talk to Praythos (A-2, Castle Whiteshield 14,10)
            EncounterChathos = 164,                  // Set the first time you talk to Chathos (B-4, Castle Blood Reign 5,15)
            EncounterPathos = 165,                   // Set the first time you talk to Pathos (E-1, Castle Dragontooth 12,4)
            EncounterZealot = 166,                   // Set the first time you talk to Zealot (A-2, Castle Whiteshield, 5,8)
            EncounterTumult = 167,                   // Set the first time you talk to Tumult (B-4, Castle Blood Reign 7,5)
            EncounterMalefactor = 168,               // Set the first time you talk to Malefactor (E-1, Castle Dragontooth 14,8)
            RescuedMorphose = 170,                   // Set when rescuing Morphose (A-1, Fountain Head, 1,12)
            TravelingOnSquibShip = 171,              // Traveling on Captain Squib's ship (C-3, Surface 3,7)
            SummoningBubbleMen = 172,                // Set when tossing coin in ooze (A-1, Fountain Head, 13,2)
            PreventCuringEradication = 174,          // Set if character is eradicated (E-4, Surface 6,5)
            PenetroNorth = 175,                      // Set if Altar Of Penetro is turned to face North (B-3, Cathedral Of Carnage 25,28)
            PenetroEast = 176,                       // Set if Altar Of Penetro is turned to face East (B-3, Cathedral Of Carnage 25,28)
            PenetroSouth = 177,                      // Set if Altar Of Penetro is turned to face South (B-3, Cathedral Of Carnage 25,28)
            PenetroWest = 178,                       // Set if Altar Of Penetro is turned to face West (B-3, Cathedral Of Carnage 25,28)
            DynatroNorth = 179,                      // Set if Altar Of Dynatro is turned to face North (B-3, Cathedral Of Carnage 23,28)
            DynatroEast = 180,                       // Set if Altar Of Dynatro is turned to face East (B-3, Cathedral Of Carnage 23,28)
            DynatroSouth = 181,                      // Set if Altar Of Dynatro is turned to face South (B-3, Cathedral Of Carnage 23,28)
            DynatroWest = 182,                       // Set if Altar Of Dynatro is turned to face West (B-3, Cathedral Of Carnage 23,28)
            BarytroNorth = 183,                      // Set if Altar Of Barytro is turned to face North (B-3, Cathedral Of Carnage 21,28)
            BarytroEast = 184,                       // Set if Altar Of Barytro is turned to face East (B-3, Cathedral Of Carnage 21,28)
            BarytroSouth = 185,                      // Set if Altar Of Barytro is turned to face South (B-3, Cathedral Of Carnage 21,28)
            BarytroWest = 186,                       // Set if Altar Of Barytro is turned to face West (B-3, Cathedral Of Carnage 21,28)
            ProtoNorth = 187,                        // Set if Altar Of Proto is turned to face North (B-3, Cathedral Of Carnage 19,28)
            ProtoEast = 188,                         // Set if Altar Of Proto is turned to face East (B-3, Cathedral Of Carnage 19,28)
            ProtoSouth = 189,                        // Set if Altar Of Proto is turned to face South (B-3, Cathedral Of Carnage 19,28)
            ProtoWest = 190,                         // Set if Altar Of Proto is turned to face West (B-3, Cathedral Of Carnage 19,28)
            Bit255 = 255
        }

        public static BitDesc PartyDescription(object val) { return Description((Party)val); }

        private static BitDesc CrystalDesc(int x, int y)
        {
            return new BitDesc("Touch the crystal", MM3Map.B4ArachnoidCavern, x, y).AddChecked().AddZero("Pay Lord Might", MM3.Spots.LordMight);
        }

        public static BitDesc Description(Party bit)
        {
            BitDesc bd = null;
            const string enter = "Enter the location";
            const string lever = "Pull the lever";
            const MM3Map fear = MM3Map.B2FortressOfFear;
            const MM3Map wildcav = MM3Map.B4WildabarCavern;
            const MM3Map carnage = MM3Map.B3CathedralOfCarnage;

            switch (bit)
            {
                case Party.VisitedBrotherAlpha:
                    bd = new BitDesc("Talk to Brother Alpha", MM3.Spots.Alpha).AddChecked();
                    bd.AddChecked("Talk to Brother Beta", MM3.Spots.Beta);
                    return bd.AddZero("Talk to Brother Zeta", MM3.Spots.Zeta);
                case Party.VisitedBrotherBeta:
                    bd = new BitDesc("Talk to Brother Beta", MM3.Spots.Beta).AddChecked();
                    bd.AddChecked("Talk to Brother Alpha", MM3.Spots.Alpha);
                    bd.AddChecked("Talk to Brother Gamma", MM3.Spots.Gamma);
                    return bd.AddZero("Talk to Brother Zeta", MM3.Spots.Zeta);
                case Party.VisitedBrotherGamma:
                    bd = new BitDesc("Talk to Brother Gamma", MM3.Spots.Gamma).AddChecked();
                    bd.AddChecked("Talk to Brother Beta", MM3.Spots.Beta);
                    bd.AddChecked("Talk to Brother Delta", wildcav, 15, 12);
                    return bd.AddZero("Talk to Brother Zeta", MM3.Spots.Zeta);
                case Party.AcceptedKranionQuest: return new BitDesc("Accept Kranion's quest", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.OneSkullToKranion: return new BitDesc("Give first skull to Kranion", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.TwoSkullsToKranion: return new BitDesc("Give second skulls to Kranion", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.ThreeSkullsToKranion: return new BitDesc("Give third skull to Kranion", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.FourSkullsToKranion: return new BitDesc("Give fourth skull to Kranion", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.FiveSkullsToKranion: return new BitDesc("Give fifth skull to Kranion", MM3.Spots.Kranion, "Talk to Kranion");
                case Party.VisitedBrotherDelta:
                    bd = new BitDesc("Talk to Brother Delta", wildcav, 15, 12).AddChecked();
                    bd.AddChecked("Talk to Brother Gamma", MM3.Spots.Gamma);
                    bd.AddChecked("Talk to Brother Zeta", MM3.Spots.Zeta);
                    return bd.AddZero("Talk to Brother Zeta", MM3.Spots.Zeta);
                case Party.LeverB4WildabarCavern0703: return new BitDesc(lever, wildcav, 7, 3).AddChecked(enter, MapXY.Array(wildcav, 1, 2, 6, 3));
                case Party.LeverB4WildabarCavern1103: return new BitDesc(lever, wildcav, 11, 3).AddChecked(enter, MapXY.Array(wildcav, 12, 3, 12, 6));
                case Party.LeverB4WildabarCavern0411: return new BitDesc(lever, wildcav, 4, 11, enter, wildcav, 3, 11);
                case Party.LeverB4WildabarCavern1509: return new BitDesc(lever, wildcav, 15, 9).AddChecked(enter, MapXY.Array(wildcav, 13, 9, 8, 9, 9, 12));
                case Party.LeverB2FortressOfFear0230And0521:
                    return new BitDesc(lever, fear, 2, 30).AddChecked().AddCZ(5, 21).AddChecked(enter, MapXY.Array(fear, 11, 18, 12, 17));
                case Party.LeverB2FortressOfFear0729And1029:
                    return new BitDesc(lever, fear, 7, 29).AddChecked().AddCZ(10, 29).AddChecked(enter, MapXY.Array(fear, 12, 17, 12, 18));
                case Party.LeverB2FortressOfFear1429And1822:
                    return new BitDesc(lever, fear, 14, 29).AddChecked().AddCZ(18, 22).AddChecked(enter, MapXY.Array(fear, 12, 17, 13, 18));
                case Party.LeverB2FortressOfFear0117And0118:
                    return new BitDesc(lever, fear, 1, 18).AddChecked().AddCZ(1, 17).AddChecked(enter, MapXY.Array(fear, 11, 17, 12, 17));
                case Party.LeverB2FortressOfFear2108And2430:
                    return new BitDesc(lever, fear, 21, 8).AddChecked().AddCZ(24, 30).AddChecked(enter, MapXY.Array(fear, 12, 17, 13, 17));
                case Party.LeverB2FortressOfFear1009And0612:
                    return new BitDesc(lever, fear, 10, 9).AddChecked().AddCZ(6, 12).AddChecked(enter, MapXY.Array(fear, 11, 16, 12, 17));
                case Party.LeverB2FortressOfFear1108And2107:
                    return new BitDesc(lever, fear, 11, 8).AddChecked().AddCZ(21, 7).AddChecked(enter, MapXY.Array(fear, 12, 16, 12, 17));
                case Party.LeverB2FortressOfFear1209And1512:
                    return new BitDesc(lever, fear, 12, 9).AddChecked().AddCZ(15, 2).AddChecked(enter, MapXY.Array(fear, 12, 17, 13, 16));
                case Party.CrystalB4ArachnoidCavern1308: return CrystalDesc(13, 8);
                case Party.CrystalB4ArachnoidCavern1707: return CrystalDesc(17, 7);
                case Party.CrystalB4ArachnoidCavern1110: return CrystalDesc(11, 10);
                case Party.CrystalB4ArachnoidCavern0818: return CrystalDesc(8, 18);
                case Party.CrystalB4ArachnoidCavern1324: return CrystalDesc(13, 24);
                case Party.CrystalB4ArachnoidCavern2020: return CrystalDesc(20, 20);
                case Party.CrystalB4ArachnoidCavern2318: return CrystalDesc(23, 18);
                case Party.CrystalB4ArachnoidCavern1611: return CrystalDesc(16, 11);
                case Party.CrystalB4ArachnoidCavern1413: return CrystalDesc(14, 13);
                case Party.CrystalB4ArachnoidCavern1417: return CrystalDesc(14, 17);
                case Party.CrystalB4ArachnoidCavern1618: return CrystalDesc(16, 18);
                case Party.CrystalB4ArachnoidCavern1818: return CrystalDesc(18, 18);
                case Party.CrystalB4ArachnoidCavern2016: return CrystalDesc(20, 16);
                case Party.RiddleTears: return new BitDesc("Answer \"tears\"", MM3Map.A3HallsOfInsanity, 11, 12, enter, MM3Map.A3HallsOfInsanity, 9, 12);
                case Party.RiddleEyes: return new BitDesc("Answer \"eyes\"", MM3Map.A3HallsOfInsanity, 17, 12, enter, MM3Map.A3HallsOfInsanity, 19, 12);
                case Party.RiddleBlink: return new BitDesc("Answer \"blink\"", MM3Map.A3HallsOfInsanity, 14, 9, enter, MM3Map.A3HallsOfInsanity, 14, 7);
                case Party.SolvedCathedralOfCarnage: return new BitDesc("Answer \"jvc\"", carnage, 25, 19).AddChecked().AddChecked(enter, MapXY.Array(carnage, 25, 15, 25, 17));
                case Party.PositroNorth: return new BitDesc("Turn altar north", carnage, 27, 28).AddCZ();
                case Party.PositroEast: return new BitDesc("Turn altar east", carnage, 27, 28).AddCZ();
                case Party.PositroSouth: return new BitDesc("Turn altar south", carnage, 27, 28).AddCZ().AddChecked(enter, carnage, 25, 19);
                case Party.PositroWest: return new BitDesc("Turn altar west", carnage, 27, 28).AddCZ();
                case Party.RiddleWeeds: return new BitDesc("Answer \"weeds\"", carnage, 1, 26).AddChecked().AddChecked(enter, carnage, 25, 19);
                case Party.ChaliceB3CathedralOfCarnage1014: return new BitDesc("Drink from the chalice", carnage, 10, 14).AddChecked(enter, carnage, 25, 19);
                case Party.ChaliceB3CathedralOfCarnage1016: return new BitDesc("Drink from the chalice", carnage, 10, 16).AddChecked(enter, carnage, 25, 19);
                case Party.ChaliceB3CathedralOfCarnage0714: return new BitDesc("Drink from the chalice", carnage, 7, 14).AddChecked(enter, carnage, 25, 19);
                case Party.ChaliceB3CathedralOfCarnage0716: return new BitDesc("Drink from the chalice", carnage, 7, 16).AddChecked(enter, carnage, 25, 19);
                case Party.ChaliceB3CathedralOfCarnage0414:
                    return new BitDesc("Drink from the chalice", carnage, 4, 14).AddChecked(enter, carnage, 25, 19).AddChecked(enter, MM3Map.A3Surface, 12, 15);
                case Party.ChaliceB3CathedralOfCarnage0416: return new BitDesc("Drink from the chalice", carnage, 4, 16).AddChecked(enter, carnage, 25, 19);
                case Party.UltimateAdventurer: return new BitDesc("Drink the water", MM3.Spots.UltimateAdv).AddChecked();
                case Party.AnyOrbsToZealot: return new BitDesc("(unreachable code)", MM3Map.A2CastleWhiteshield, 37, 1);
                case Party.WhiteshieldDestroyed:
                    bd = new BitDesc("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    bd.AddSet("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                    return bd.AddChecked(enter, MM3Map.A2Surface, 4, 15);
                case Party.WhiteshieldBoxOpened: return new BitDesc("Open the box", MapXY.Array(MM3Map.A2CastleWhiteshield, 2, 8, 7, 3, 7, 13, 8, 3, 8, 13)).AddChecked();
                case Party.BloodReignDestroyed:
                    bd = new BitDesc("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    bd.AddSet("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                    return bd.AddChecked(enter, MM3Map.B4Surface, 4, 11);
                case Party.DragontoothDestroyed:
                    bd = new BitDesc("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    bd.AddSet("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    return bd.AddChecked(enter, MM3Map.E1Surface, 10, 5);
                case Party.GreywindTasksCompleted: return new BitDesc("Strike the gong", MM3.Spots.Gong).AddChecked("Talk to Greywind", MM3.Spots.Greywind);
                case Party.BlackwindReleased: return new BitDesc("Release Blackwind", MM3.Spots.Blackwind);
                case Party.StatueHamonOthreute:
                    bd = new BitDesc("Pay 100000 Gold", MM3.Spots.HamonOthreute);
                    bd.AddChecked(enter, MM3.Spots.BlackwindStatues);
                    return bd.AddChecked(enter, MM3.Spots.Blackwind);
                case Party.StatueTempestStorm:
                    bd = new BitDesc("Pay 1000 Gems", MM3.Spots.TempestStorm);
                    bd.AddChecked(enter, MM3.Spots.BlackwindStatues);
                    return bd.AddChecked(enter, MM3.Spots.Blackwind);
                case Party.StatueBloodMane:
                    bd = new BitDesc("Sacrifice a character", MM3.Spots.BloodMane);
                    bd.AddChecked(enter, MM3.Spots.BlackwindStatues);
                    return bd.AddChecked(enter, MM3.Spots.Blackwind);
                case Party.HourglassC4GreywindDungeon0101: return new BitDesc("Turn the hourglass", MM3.Spots.GlassSW, enter).AddZero().AddChecked("Strike the gong", MM3.Spots.Gong);
                case Party.HourglassC4GreywindDungeon1401: return new BitDesc("Turn the hourglass", MM3.Spots.GlassSE, enter).AddZero().AddChecked("Strike the gong", MM3.Spots.Gong);
                case Party.HourglassC4GreywindDungeon1414: return new BitDesc("Turn the hourglass", MM3.Spots.GlassNE, enter).AddZero().AddChecked("Strike the gong", MM3.Spots.Gong);
                case Party.HourglassC4GreywindDungeon0114: return new BitDesc("Turn the hourglass", MM3.Spots.GlassNW, enter).AddZero().AddChecked("Strike the gong", MM3.Spots.Gong);
                case Party.EncounterBlackwind: return new BitDesc("Talk to Blackwind", MM3.Spots.Blackwind).AddChecked();
                case Party.RaiseSunkenIsle: return new BitDesc("Answer \"youth\"", MM3.Spots.Youth, enter).AddChecked(enter, MM3Map.B2Surface, 5, 2);

                case Party.DestroyOrcOutpostA1: return new BitDesc("Destroy the Orc Outpost", MM3.Spots.Destroy1, enter, MM3.Spots.Spawn1);
                case Party.DestroyGoblinWagonA1: return new BitDesc("Destroy the Goblin Wagon", MM3.Spots.Destroy2, enter, MM3.Spots.Spawn2);
                case Party.DestroyOrcOutpostA2: return new BitDesc("Destroy the Orc Outpost", MM3.Spots.Destroy4, enter, MM3.Spots.Spawn4);
                case Party.DestroyGoblinHutA2: return new BitDesc("Destroy the Goblin Hut", MM3.Spots.Destroy5, enter, MM3.Spots.Spawn5);
                case Party.DestroyScreamerPodsA3: return new BitDesc("Destroy the Screamer Pods", MM3.Spots.Destroy6, enter, MM3.Spots.Spawn6);
                case Party.DestroyBatWagonA3: return new BitDesc("Destroy the Vampire Bat Wagon", MM3.Spots.Destroy7);
                case Party.DestroyMantisPodsA4: return new BitDesc("Destroy the Magic Mantis Pods", MM3.Spots.Destroy9, enter, MM3.Spots.Spawn9);
                case Party.ResurrectSpidersA4: return new BitDesc().AddChecked(enter, MM3Map.A4Surface, 15, 6);
                case Party.DestroyApiaryB1: return new BitDesc("Destroy the Oh No Bug Apiary", MM3.Spots.Destroy15, enter, MM3.Spots.Spawn15);
                case Party.DestroySporesB1: return new BitDesc("Destroy the Wild Fungus Spores", MM3.Spots.Destroy14, enter, MM3.Spots.Spawn14);
                case Party.DestroySpriteHut:
                    bd = new BitDesc("Destroy the Sprite Hut", MM3.Spots.Destroy13);
                    bd.AddSet("Destroy the Ogre Hut", MM3.Spots.Destroy12);
                    return bd.AddChecked(enter, MM3.Spots.Spawn12);
                case Party.ResurrectSpritesB2: return new BitDesc().AddChecked(enter, MM3Map.B2Surface, 5, 12);
                case Party.DestroyLampreyB3: return new BitDesc("Kill the Lamprey", MM3.Spots.Destroy10, enter, MM3.Spots.Spawn10);
                case Party.DestroyGrubsB3: return new BitDesc("Destroy the Bugaboo Grubs", MM3.Spots.Destroy11, enter, MM3.Spots.Spawn11);
                case Party.DestroyCyclopsShackC1: return new BitDesc("Destroy the Cyclops Shack", MM3.Spots.Destroy16, enter, MM3.Spots.Spawn16);
                case Party.DestroySpriteHutC1: return new BitDesc("Destroy the Sprite Hut", MM3.Spots.Destroy17, enter, MM3.Spots.Spawn17);
                case Party.DesecratedShrine: return new BitDesc("Desecrate the Shrine", MM3.Spots.Destroy18, enter, MM3.Spots.Spawn18);
                case Party.DestroyLocustNestE4: return new BitDesc("Destroy the Death Locust Nest", MM3.Spots.Destroy27, enter, MM3.Spots.Spawn27);
                case Party.DestroyBarbarianE4:
                    bd = new BitDesc("Destroy the Barbarian Compound", MM3.Spots.Destroy26);
                    return bd.AddChecked(enter, new MapXY[] { new MapXY(MM3Map.E4Surface, 5, 10), new MapXY(MM3Map.F4Surface, 1, 6) });
                case Party.RememberedPurveyor:
                    bd = new BitDesc("Pay 1000000 Gold", MM3.Spots.Purveyor, enter).AddChecked(enter, MM3Map.F4Surface, 4, 8);
                    return bd.AddChecked("Throw in a coin", MM3.Spots.Remembrance).AddZero("Pay 10000 Gold", MM3.Spots.Betrayer);
                case Party.RememberedSoothsayer:
                    bd = new BitDesc("Pay 500000 Gold", MM3.Spots.Soothsayer, enter);
                    return bd.AddChecked("Throw in a coin", MM3.Spots.Remembrance).AddZero("Pay 10000 Gold", MM3.Spots.Betrayer);
                case Party.RememberedSlayer:
                    bd = new BitDesc("Pay 200000 Gold", MM3.Spots.Slayer, enter);
                    return bd.AddChecked("Throw in a coin", MM3.Spots.Remembrance).AddZero("Pay 10000 Gold", MM3.Spots.Betrayer);
                case Party.RememberedObeyer:
                    bd = new BitDesc("Pay 100000 Gold", MM3.Spots.Obeyer, enter);
                    return bd.AddChecked("Throw in a coin", MM3.Spots.Remembrance).AddZero("Pay 10000 Gold", MM3.Spots.Betrayer);
                case Party.DestroyedRogueHut:
                    bd = new BitDesc("Destroy the Rogue Hut", MM3.Spots.Destroy25);
                    bd.AddChecked(enter, MM3Map.E2Surface, 6, 1).AddChecked(enter, MapXY.Array(MM3Map.E3Surface, 12, 5, 13, 12, 3, 8));
                    return bd.AddChecked(enter, MapXY.Array(MM3Map.F2Surface, 11, 6, 4, 2));
                case Party.DestroyedDeathLocustNest:
                    return new BitDesc("Destroy the Death Locust Nest", MM3.Spots.Destroy24).AddChecked(enter, MM3Map.E2Surface, 7, 0);
                case Party.OneLoveToTrueberry: return new BitDesc("Bring first love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.TwoLoveToTrueberry: return new BitDesc("Bring second love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.ThreeLoveToTrueberry: return new BitDesc("Bring third love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.FourLoveToTrueberry: return new BitDesc("Bring fourth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.FiveLoveToTrueberry: return new BitDesc("Bring fifth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.SixLoveToTrueberry: return new BitDesc("Bring sixth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.SevenLoveToTrueberry: return new BitDesc("Bring seventh love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.EightLoveToTrueberry: return new BitDesc("Bring eighth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.NineLoveToTrueberry: return new BitDesc("Bring ninth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                case Party.FreedTrueberry:
                    bd = new BitDesc("Bring tenth love to Trueberry", MM3.Spots.Trueberry, "Talk to Trueberry");
                    bd.AddSet(lever, new MapXY(MM3Map.ItsASecret, 15, 4)).AddCZ();
                    return bd.AddChecked(enter, MM3Map.ItsASecret, 7, 1);
                case Party.StartleAthea: return new BitDesc("Scare away Athea", MM3.Spots.Athea);
                case Party.EncounterPirateQueen: return new BitDesc("Talk to Pirate Queen", MapXY.Array(MM3Map.D2Surface, 9, 14, 11, 12, 13, 13, 14, 10));
                case Party.EncounterTrueberry:
                    return new BitDesc("Talk to Princess Trueberry", MM3.Spots.Trueberry).AddZero("Free Princess Trueberry", MM3.Spots.Trueberry);
                case Party.EncounterIcarusBirds: return new BitDesc("Talk to Icarus' Sparrows", MM3.Spots.Icarus).AddChecked().AddZero("Resurrect Icarus");
                case Party.EnterCastleGreywind:
                    return new BitDesc("Enter Castle Greywind", MM3.Spots.GreywindCastle).AddChecked().AddZero("Free Greywind", MM3.Spots.Greywind);
                case Party.EnterCastleBlackwind:
                    return new BitDesc("Enter Castle Blackwind", MM3Map.D4Surface, 6, 8).AddChecked().AddZero("Free Blackwind", MM3.Spots.Blackwind);
                case Party.EncounterPraythos:
                    bd = new BitDesc("Talk to Praythos", MM3.Spots.Praythos);
                    bd.AddZero("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    return bd.AddZero("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                case Party.EncounterChathos:
                    bd = new BitDesc("Talk to Chathos", MM3.Spots.Chathos);
                    bd.AddZero("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    return bd.AddZero("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                case Party.EncounterPathos:
                    bd = new BitDesc("Talk to Pathos", MM3.Spots.Pathos);
                    bd.AddZero("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    return bd.AddZero("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                case Party.EncounterZealot:
                    bd = new BitDesc("Talk to Zealot", MM3.Spots.Zealot);
                    bd.AddZero("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    bd.AddZero("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    return bd.AddZero("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                case Party.EncounterTumult:
                    bd = new BitDesc("Talk to Tumult", MM3.Spots.Tumult);
                    bd.AddZero("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    bd.AddZero("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    return bd.AddZero("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                case Party.EncounterMalefactor:
                    bd = new BitDesc("Talk to Malefactor", MM3.Spots.Malefactor);
                    bd.AddZero("Give eleventh orb to Tumult", MM3.Spots.Tumult);
                    bd.AddZero("Give eleventh orb to Zealot", MM3.Spots.Zealot);
                    return bd.AddZero("Give eleventh orb to Malefactor", MM3.Spots.Malefactor);
                case Party.RescuedMorphose: return new BitDesc("Rescuing Morphose", MM3.Spots.Morphose);
                case Party.TravelingOnSquibShip:
                    bd = new BitDesc("Board Captain Squib's ship", MM3Map.B3Surface, 3, 7);
                    bd.AddChecked(enter, MapXY.Array(MM3Map.B3Surface, 6, 8, 6, 9, 6, 10, 6, 11, 6, 12, 6, 12, 7, 12, 8, 12, 9, 12, 9, 12, 9, 13, 9, 13, 10, 13,
                        11, 13, 12, 13, 12, 13, 13, 13, 13, 13, 14, 13, 15, 13));
                    bd.AddChecked(enter, MapXY.Array(MM3Map.C3Surface, 0, 13, 0, 12, 0, 11, 0, 10, 0, 9, 0, 8, 0, 7, 0, 6, 0, 5, 0, 4, 0, 3, 0, 2, 1, 2, 2, 2, 2, 1,
                        2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 8, 0, 9, 0, 10, 0));
                    bd.AddChecked(enter, MapXY.Array(MM3Map.C4Surface, 10, 15, 10, 14, 10, 13, 11, 13, 11, 12, 11, 11, 11, 10, 12, 10, 13, 10, 14, 10, 15, 10));
                    bd.AddChecked(enter, MapXY.Array(MM3Map.D3Surface, 5, 0, 5, 1, 6, 1, 7, 1, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 14, 2, 14, 3, 14,
                        4, 14, 5, 14, 6, 15, 6, 15, 7, 15, 8, 14, 8, 14, 9, 14, 10, 14, 11, 14, 12, 14, 13, 15, 13));
                    bd.AddChecked(enter, MapXY.Array(MM3Map.D4Surface, 0, 10, 0, 11, 1, 11, 1, 12, 2, 12, 3, 12, 3, 13, 3, 14, 3, 15, 4, 15, 5, 15));
                    bd.AddChecked(enter, MapXY.Array(MM3Map.E2Surface, 2, 0, 2, 1, 3, 1, 4, 1, 5, 1, 0, 13, 1, 13, 2, 13, 2, 14, 2, 15));
                    return bd.AddZero(enter, MM3Map.E2Surface, 5, 1);
                case Party.SummoningBubbleMen: return new BitDesc("Toss a coin", MM3Map.A1FountainHead, 13, 2).AddCZ(enter, new MapXY(MM3Map.A1FountainHead, 12, 2));
                case Party.PreventCuringEradication: return new BitDesc("Drink from the well", MM3Map.E4Surface, 6, 5).AddCZ();
                case Party.PenetroNorth: return new BitDesc("Turn altar north", carnage, 25, 28).AddCZ();
                case Party.PenetroEast: return new BitDesc("Turn altar east", carnage, 25, 28).AddCZ().AddChecked(enter, carnage, 25, 19);
                case Party.PenetroSouth: return new BitDesc("Turn altar south", carnage, 25, 28).AddCZ();
                case Party.PenetroWest: return new BitDesc("Turn altar west", carnage, 25, 28).AddCZ();
                case Party.DynatroNorth: return new BitDesc("Turn altar north", carnage, 23, 28).AddCZ().AddChecked(enter, carnage, 25, 19);
                case Party.DynatroEast: return new BitDesc("Turn altar east", carnage, 23, 28).AddCZ();
                case Party.DynatroSouth: return new BitDesc("Turn altar south", carnage, 23, 28).AddCZ();
                case Party.DynatroWest: return new BitDesc("Turn altar west", carnage, 23, 28).AddCZ();
                case Party.BarytroNorth: return new BitDesc("Turn altar north", carnage, 21, 28).AddCZ();
                case Party.BarytroEast: return new BitDesc("Turn altar east", carnage, 21, 28).AddCZ();
                case Party.BarytroSouth: return new BitDesc("Turn altar south", carnage, 21, 28).AddCZ();
                case Party.BarytroWest: return new BitDesc("Turn altar west", carnage, 21, 28).AddCZ().AddChecked(enter, carnage, 25, 19);
                case Party.ProtoNorth: return new BitDesc("Turn altar north", carnage, 19, 28).AddCZ().AddChecked(enter, carnage, 25, 19);
                case Party.ProtoEast: return new BitDesc("Turn altar east", carnage, 19, 28).AddCZ();
                case Party.ProtoSouth: return new BitDesc("Turn altar south", carnage, 19, 28).AddCZ();
                case Party.ProtoWest: return new BitDesc("Turn altar west", carnage, 19, 28).AddCZ();
                default: return BitDesc.Empty;
            }
        }

        public static string AwardDescription(int index)
        {
            if (index == -1)
                return "Awards";

            MM3AwardIndex ai = (MM3AwardIndex)index;
            if (ai >= MM3AwardIndex.RavensGuildMember && ai < MM3AwardIndex.Last)
                return MM3Character.AwardString(ai);
            return String.Empty;
        }
    }

    public class MM3Quest : BasicQuest
    {
        public MM3Quest() { }

        public MM3Quest(BasicQuestType type, string name, string giver, string reward)
        {
            Init(GameNames.MightAndMagic3, type, name, giver, reward);
        }

        public MM3Quest(BasicQuestType type, string name, string giver)
        {
            Init(GameNames.MightAndMagic3, type, name, giver, String.Empty);
        }

        public MM3Quest(BasicQuestType type, string name)
        {
            Init(GameNames.MightAndMagic3, type, name, String.Empty, String.Empty);
        }
    }

    public class MM3QuestInfo : QuestInfo
    {
        public override bool NeedsFiles { get { return true; } }

        public QuestStatus HirelingSonOfAbu = new QuestStatus();
        public QuestStatus HirelingCharity = new QuestStatus();
        public QuestStatus HirelingDarlana = new QuestStatus();
        public QuestStatus HirelingSirGalant = new QuestStatus();
        public QuestStatus HirelingLoneWolf = new QuestStatus();
        public QuestStatus HirelingWartowsan = new QuestStatus();

        public MM3QuestStates.Skills Skills;
        public MM3QuestStates.Guilds Guilds;
        public MM3QuestStates.MummyKing MummyKing;

        public QuestStatus CorakSheltem = new QuestStatus(QuestStatus.Basic.Accepted, "Find Corak and Sheltem");
        public QuestStatus FindFortressKeys = new QuestStatus(QuestStatus.Basic.Accepted, "Find the colored Fortress Keys");
        public QuestStatus FindHologramCards = new QuestStatus(QuestStatus.Basic.Accepted, "Find the hologram sequencing cards");
        public QuestStatus DeliverPowerOrbs = new QuestStatus(QuestStatus.Basic.Accepted, "Deliver the King's Ultimate Power Orbs");
        public QuestStatus MummyPuzzle = new QuestStatus(QuestStatus.Basic.Accepted, "Solve the Mummy King's Puzzle");
        public QuestStatus SaveFountainHead = new QuestStatus(QuestStatus.Basic.NotStarted, "Save Fountain Head");
        public QuestStatus ReceiveBlessing = new QuestStatus(QuestStatus.Basic.NotStarted, "Receive the Blessing of the Forces");
        public QuestStatus RecoverArtifacts = new QuestStatus(QuestStatus.Basic.NotStarted, "Recover the Ancient Artifacts");
        public QuestStatus DeliverShells = new QuestStatus(QuestStatus.Basic.NotStarted, "Deliver Sea Shells of Serenity to Athea");
        public QuestStatus DeliverPearls = new QuestStatus(QuestStatus.Basic.NotStarted, "Deliver Pearls to the Pirate Queen");
        public QuestStatus VisitBrothers = new QuestStatus(QuestStatus.Basic.NotStarted, "Visit the Greek Brothers");
        public QuestStatus DestroyLairs = new QuestStatus(QuestStatus.Basic.NotStarted, "Destroy the Monster Lairs");
        public QuestStatus UseWell = new QuestStatus(QuestStatus.Basic.NotStarted, "Use the Well of Remembrance");
        public QuestStatus ReleaseOrbs = new QuestStatus(QuestStatus.Basic.NotStarted, "Release the Orbs in the Cathedral of Carnage");
        public QuestStatus UseCrystals = new QuestStatus(QuestStatus.Basic.NotStarted, "Use the Crystals in the Arachnoid Cavern");
        public QuestStatus RaiseIsland = new QuestStatus(QuestStatus.Basic.NotStarted, "Raise the Sunken Island");
        public QuestStatus FindJewelry = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Jewelry");
        public QuestStatus ReleaseGreywind = new QuestStatus(QuestStatus.Basic.NotStarted, "Release Greywind's Ghost");
        public QuestStatus ReleaseBlackwind = new QuestStatus(QuestStatus.Basic.NotStarted, "Release Blackwind's Ghost");
        public QuestStatus DeliverSkulls = new QuestStatus(QuestStatus.Basic.NotStarted, "Deliver Silver Skulls to Kranion");
        public QuestStatus ResurrectIcarus = new QuestStatus(QuestStatus.Basic.NotStarted, "Resurrect Icarus the Golden Unicorn");
        public QuestStatus JoinGuilds = new QuestStatus(QuestStatus.Basic.NotStarted, "Join the Guilds");
        public QuestStatus DefeatArena = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Dragon Lord in the Arena");
        public QuestStatus LearnSkills = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn the secondary skills");
        public QuestStatus Level6Items = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain level 6 items");
        public QuestStatus TempStats = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your stats temporarily");
        public QuestStatus PermStats = new QuestStatus(QuestStatus.Basic.NotStarted, "Increase your stats permanently");
        public QuestStatus Hirelings = new QuestStatus(QuestStatus.Basic.NotStarted, "Free the trapped hirelings");

        public MM3QuestStates.Orbs Orbs;
        public MM3QuestStates.Pearls Pearls;
        public MM3QuestStates.Shells Shells;
        public MM3QuestStates.Skulls Skulls;
        public MM3QuestStates.Arena Arena;
        public MM3QuestStates.Trueberry Trueberry;
        public MM3QuestStates.Artifacts Artifacts;
        public MM3QuestStates.GreekBrothers Brothers;

        public QuestStatus SpellClericLight;
        public QuestStatus SpellClericAwaken;
        public QuestStatus SpellClericFirstAid;
        public QuestStatus SpellClericFlyingFist;
        public QuestStatus SpellClericRevitalize;
        public QuestStatus SpellClericCureWounds;
        public QuestStatus SpellClericSparks;
        public QuestStatus SpellClericProtectionfromElements;
        public QuestStatus SpellClericPain;
        public QuestStatus SpellClericSuppressPoison;
        public QuestStatus SpellClericSuppressDisease;
        public QuestStatus SpellClericTurnUndead;
        public QuestStatus SpellClericSilence;
        public QuestStatus SpellClericBlessed;
        public QuestStatus SpellClericHolyBonus;
        public QuestStatus SpellClericPowerCure;
        public QuestStatus SpellClericHeroism;
        public QuestStatus SpellClericImmobilize;
        public QuestStatus SpellClericColdRay;
        public QuestStatus SpellClericCurePoison;
        public QuestStatus SpellClericAcidSpray;
        public QuestStatus SpellClericCureDisease;
        public QuestStatus SpellClericCureParalysis;
        public QuestStatus SpellClericParalyze;
        public QuestStatus SpellClericCreateFood;
        public QuestStatus SpellClericFieryFlail;
        public QuestStatus SpellClericTownPortal;
        public QuestStatus SpellClericStonetoFlesh;
        public QuestStatus SpellClericHalfforMe;
        public QuestStatus SpellClericRaiseDead;
        public QuestStatus SpellClericMoonRay;
        public QuestStatus SpellClericMassDistortion;
        public QuestStatus SpellClericHolyWord;
        public QuestStatus SpellClericResurrect;
        public QuestStatus SpellClericSunRay;
        public QuestStatus SpellClericDivineIntervention;
        public QuestStatus SpellSorcererLight;
        public QuestStatus SpellSorcererAwaken;
        public QuestStatus SpellSorcererDetectMagic;
        public QuestStatus SpellSorcererElementalArrow;
        public QuestStatus SpellSorcererEnergyBlast;
        public QuestStatus SpellSorcererSleep;
        public QuestStatus SpellSorcererCreateRope;
        public QuestStatus SpellSorcererToxicCloud;
        public QuestStatus SpellSorcererJump;
        public QuestStatus SpellSorcererAcidStream;
        public QuestStatus SpellSorcererLevitate;
        public QuestStatus SpellSorcererWizardEye;
        public QuestStatus SpellSorcererIdentifyMonster;
        public QuestStatus SpellSorcererLightningBolt;
        public QuestStatus SpellSorcererLloydsBeacon;
        public QuestStatus SpellSorcererPowerShield;
        public QuestStatus SpellSorcererDetectMonster;
        public QuestStatus SpellSorcererFireball;
        public QuestStatus SpellSorcererTimeDistortion;
        public QuestStatus SpellSorcererFeebleMind;
        public QuestStatus SpellSorcererTeleport;
        public QuestStatus SpellSorcererFingerofDeath;
        public QuestStatus SpellSorcererSuperShelter;
        public QuestStatus SpellSorcererDragonBreath;
        public QuestStatus SpellSorcererRechargeItem;
        public QuestStatus SpellSorcererFantasticFreeze;
        public QuestStatus SpellSorcererDuplication;
        public QuestStatus SpellSorcererDisintegrate;
        public QuestStatus SpellSorcererEtherealize;
        public QuestStatus SpellSorcererDancingSword;
        public QuestStatus SpellSorcererEnchantItem;
        public QuestStatus SpellSorcererIncinerate;
        public QuestStatus SpellSorcererMegaVolts;
        public QuestStatus SpellSorcererInferno;
        public QuestStatus SpellSorcererImplosion;
        public QuestStatus SpellSorcererStarBurst;
        public QuestStatus SpellDruidLight;
        public QuestStatus SpellDruidAwaken;
        public QuestStatus SpellDruidFirstAid;
        public QuestStatus SpellDruidDetectMagic;
        public QuestStatus SpellDruidElementalArrow;
        public QuestStatus SpellDruidRevitalize;
        public QuestStatus SpellDruidCreateRope;
        public QuestStatus SpellDruidSleep;
        public QuestStatus SpellDruidProtectionfromElements;
        public QuestStatus SpellDruidSuppressPoison;
        public QuestStatus SpellDruidSuppressDisease;
        public QuestStatus SpellDruidIdentifyMonster;
        public QuestStatus SpellDruidNaturesCure;
        public QuestStatus SpellDruidImmobilize;
        public QuestStatus SpellDruidWalkonWater;
        public QuestStatus SpellDruidFrostBite;
        public QuestStatus SpellDruidLightningBolt;
        public QuestStatus SpellDruidAcidSpray;
        public QuestStatus SpellDruidColdRay;
        public QuestStatus SpellDruidNaturesGate;
        public QuestStatus SpellDruidFireball;
        public QuestStatus SpellDruidDeadlySwarm;
        public QuestStatus SpellDruidCureParalysis;
        public QuestStatus SpellDruidParalyze;
        public QuestStatus SpellDruidCreateFood;
        public QuestStatus SpellDruidStonetoFlesh;
        public QuestStatus SpellDruidRaiseDead;
        public QuestStatus SpellDruidPrismaticLight;
        public QuestStatus SpellDruidElementalStorm;

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { HirelingSonOfAbu, HirelingCharity, HirelingDarlana, HirelingSirGalant, HirelingLoneWolf, HirelingWartowsan, CorakSheltem,
                FindFortressKeys, FindHologramCards, DeliverPowerOrbs, MummyPuzzle, SaveFountainHead, ReceiveBlessing, RecoverArtifacts, DeliverShells,
                DeliverPearls, VisitBrothers, DestroyLairs, UseWell, ReleaseOrbs, UseCrystals, RaiseIsland, FindJewelry, ReleaseGreywind, ReleaseBlackwind,
                DeliverSkulls, ResurrectIcarus, JoinGuilds, DefeatArena, LearnSkills, Level6Items, TempStats, PermStats, Hirelings, SpellClericLight,
                SpellClericAwaken, SpellClericFirstAid, SpellClericFlyingFist, SpellClericRevitalize, SpellClericCureWounds, SpellClericSparks,
                SpellClericProtectionfromElements, SpellClericPain, SpellClericSuppressPoison, SpellClericSuppressDisease, SpellClericTurnUndead, SpellClericSilence,
                SpellClericBlessed, SpellClericHolyBonus, SpellClericPowerCure, SpellClericHeroism, SpellClericImmobilize, SpellClericColdRay, SpellClericCurePoison,
                SpellClericAcidSpray, SpellClericCureDisease, SpellClericCureParalysis, SpellClericParalyze, SpellClericCreateFood, SpellClericFieryFlail,
                SpellClericTownPortal, SpellClericStonetoFlesh, SpellClericHalfforMe, SpellClericRaiseDead, SpellClericMoonRay, SpellClericMassDistortion,
                SpellClericHolyWord, SpellClericResurrect, SpellClericSunRay, SpellClericDivineIntervention, SpellSorcererLight, SpellSorcererAwaken,
                SpellSorcererDetectMagic, SpellSorcererElementalArrow, SpellSorcererEnergyBlast, SpellSorcererSleep, SpellSorcererCreateRope,
                SpellSorcererToxicCloud, SpellSorcererJump, SpellSorcererAcidStream, SpellSorcererLevitate, SpellSorcererWizardEye, SpellSorcererIdentifyMonster,
                SpellSorcererLightningBolt, SpellSorcererLloydsBeacon, SpellSorcererPowerShield, SpellSorcererDetectMonster, SpellSorcererFireball, 
                SpellSorcererTimeDistortion, SpellSorcererFeebleMind, SpellSorcererTeleport, SpellSorcererFingerofDeath, SpellSorcererSuperShelter,
                SpellSorcererDragonBreath, SpellSorcererRechargeItem, SpellSorcererFantasticFreeze, SpellSorcererDuplication, SpellSorcererDisintegrate, 
                SpellSorcererEtherealize, SpellSorcererDancingSword, SpellSorcererEnchantItem, SpellSorcererIncinerate, SpellSorcererMegaVolts, 
                SpellSorcererInferno, SpellSorcererImplosion, SpellSorcererStarBurst, SpellDruidLight, SpellDruidAwaken, SpellDruidFirstAid, SpellDruidDetectMagic,
                SpellDruidElementalArrow, SpellDruidRevitalize, SpellDruidCreateRope, SpellDruidSleep, SpellDruidProtectionfromElements, SpellDruidSuppressPoison,
                SpellDruidSuppressDisease, SpellDruidIdentifyMonster, SpellDruidNaturesCure, SpellDruidImmobilize, SpellDruidWalkonWater, SpellDruidFrostBite,
                SpellDruidLightningBolt, SpellDruidAcidSpray, SpellDruidColdRay, SpellDruidNaturesGate, SpellDruidFireball, SpellDruidDeadlySwarm, SpellDruidCureParalysis,
                SpellDruidParalyze, SpellDruidCreateFood, SpellDruidStonetoFlesh, SpellDruidRaiseDead, SpellDruidPrismaticLight, SpellDruidElementalStorm };
        }


        public class Bits
        {
            private byte[] m_party;

            public Bits(byte[] party)
            {
                m_party = party;
            }

            private QuestGoal Goal(bool b) { return b ? QuestGoal.Complete : QuestGoal.Incomplete; }
            public QuestGoal Set(MM3Bits.Party bit) { return Goal(Global.IsBitSet(m_party, bit)); }
            public QuestGoal NotSet(MM3Bits.Party bit) { return Goal(!Global.IsBitSet(m_party, bit)); }
        }

        private QuestStatus AddSpellQuest(QuestTotals totals, MM3Character mm3Char, MM3SpellIndex spell)
        {
            SpellType type = MM3.Spells[spell].Type;
            bool bKnown = mm3Char.Spells.IsKnown((int)spell, type);
            QuestStatus qs = GetQuestState(true, bKnown, mm3Char.CasterType == type);
            qs.AddObj(7, Goal(bKnown));
            AddQuest(totals, qs);
            return qs;
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<MM3Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        private MM3Quest GetQuest(QuestStatus.Basic state, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest(state, type, bits, name, giver, reward, null, locations);
        }

        private MM3Quest GetQuest(QuestStatus.Basic state, BasicQuestType type, object bits, string name, string giver, string reward, string path, params QuestLocation[] locations)
        {
            if (locations == null || locations.Length < 1)
                return null;

            MM3Quest quest = new MM3Quest(type, name, giver, reward);
            quest.Bits = new QuestBits(bits);
            quest.Status.Set(state);
            foreach (QuestLocation location in locations)
                quest.Secondary.Add(location);

            if (String.IsNullOrWhiteSpace(path))
                quest.Path = GetPath(type);
            else
                quest.Path = path;

            return quest;
        }

        private string SpellTypeName(SpellType type)
        {
            switch (type)
            {
                case SpellType.Sorcerer: return "arcane";
                case SpellType.Cleric: return "cleric";
                case SpellType.Druid: return "druid";
                default: return "";
            }
        }

        public void AddSpellQuest(QuestStatus status, List<MM3Quest> quests, MM3SpellIndex index, int iGuildMinimum, params QuestLocation[] locations)
        {
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                MM3Spell spell = MM3.Spells[index];
                string strTask = String.Format("Learn the {0} spell \"{1}\"", SpellTypeName(spell.Type), spell.Name);
                string strReward = String.Format("{0} Spell \"{1}\"", MM3Spell.TypeString(spell.Type), spell.Name);
                status.PrimaryObjective = strTask;
                List<QuestLocation> newLocations = new List<QuestLocation>(locations);

                string strPurchase = String.Format("Purchase spell at level {0} for {1} Gold at ", spell.Level, MM3SpellList.GetSpellPurchasePrice(spell.InternalIndex, CharClass));
                if (iGuildMinimum < 1)
                    newLocations.Add(new QuestLocation(strPurchase + "Raven's Guild", MM3.Spots.RavenGuild));
                if (iGuildMinimum < 2)
                    newLocations.Add(new QuestLocation(strPurchase + "Albatross Guild", MM3.Spots.AlbatrossGuild));
                if (iGuildMinimum < 3)
                    newLocations.Add(new QuestLocation(strPurchase + "Falcon's Guild", MM3.Spots.FalconGuild));
                if (iGuildMinimum < 4)
                    newLocations.Add(new QuestLocation(strPurchase + "Buzzard's Guild", MM3.Spots.BuzzardGuild));
                if (iGuildMinimum < 5)
                    newLocations.Add(new QuestLocation(strPurchase + "Eagle's Guild", MM3.Spots.EagleGuild));

                status.AddLocations(newLocations.ToArray());
                status.MarkAllWhenComplete = true;
                AddSideQuest(status, quests, String.Empty, String.Empty, "Spells");
            }
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<MM3Quest> quests, string strGiver, string strReward, string strPath)
        {
            MM3Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, null, strGiver, strReward) as MM3Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<MM3Quest> quests, string strGiver = "", string strReward = "", string strPath = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, strGiver, strReward, strPath);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<MM3Quest> quests, string strGiver = "", string strReward = "", string strPath = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, strGiver, strReward, strPath);
        }

        public override BasicQuest[] GetQuests()
        {
            List<MM3Quest> quests = new List<MM3Quest>(90);

            if (Orbs == null)
                return quests.ToArray();

            CorakSheltem.AddLocations(new QuestLocation("Obtain the Golden Pyramid Key Card", MM3.Spots.KeyCard),
                new QuestLocation("Become an Ultimate Adventurer", MM3.Spots.UltimateAdv),
            new QuestLocation("Enter the Golden Pyramid", MM3.Spots.Pyramid));
            CorakSheltem.Postrequisites.Add(new QuestLocation("Use the transport tube in the Main Control Sector", MM3.Spots.TransportTube));

            FindFortressKeys.AddLocations(new QuestLocation("Obtain the Yellow Fortress Key", MM3.Spots.YellowKey),
                new QuestLocation("Obtain the Green Eyeball Key", MM3.Spots.GreenKey),
                new QuestLocation("Obtain the Blue Unholy Key", MM3.Spots.BlueKey),
                new QuestLocation("Obtain the Red Warriors Key", MM3.Spots.RedKey),
                new QuestLocation("Obtain the Black Terror Key", MM3.Spots.BlackKey),
                new QuestLocation("Obtain the Gold master Key", MM3.Spots.GoldKey));
            CorakSheltem.PreQuest.Add(AddMainQuest(1, FindFortressKeys, quests, String.Empty));

            FindHologramCards.AddLocations(new QuestLocation("Obtain Hologram Sequencing Card 002", MM3.Spots.Card002),
                new QuestLocation("Obtain Hologram Sequencing Card 003", MM3.Spots.Card003),
                new QuestLocation("Obtain Hologram Sequencing Card 004", MM3.Spots.Card004),
                new QuestLocation("Obtain Hologram Sequencing Card 005", MM3.Spots.Card005),
                new QuestLocation("Obtain Hologram Sequencing Card 006", MM3.Spots.Card006));

            MummyPuzzle.AddLocations(new QuestLocation("Place a head in the northwest square", MM3.Spots.HeadNW),
                new QuestLocation("Place a head in the northeast square", MM3.Spots.HeadNE),
                new QuestLocation("Place a head in the southwest square", MM3.Spots.HeadSW),
                new QuestLocation("Place a head in the southeast square", MM3.Spots.HeadSE),
                new QuestLocation("Place a field in the north square", MM3.Spots.FieldN),
                new QuestLocation("Place a field in the east square", MM3.Spots.FieldE),
                new QuestLocation("Place a field in the south square", MM3.Spots.FieldS),
                new QuestLocation("Place a field in the west square", MM3.Spots.FieldW),
                new QuestLocation("Collect Hologram Sequencing Card 001", MM3.Spots.Card001));

            DeliverPowerOrbs.AddInformation(new QuestLocation(String.Format("Power Orbs in inventory: {0}", Orbs.OrbsInInventory), MM3Map.Unknown, -1, -1));
            DeliverPowerOrbs.AddLocations(new QuestLocation(String.Format("Orbs delivered to King Zealot: {0}", Orbs.DeliveredGood), MM3.Spots.Zealot),
                new QuestLocation(String.Format("Orbs delivered to King Tumult: {0}", Orbs.DeliveredNeutral), MM3.Spots.Tumult),
                new QuestLocation(String.Format("Orbs delivered to King Malefactor: {0}", Orbs.DeliveredEvil), MM3.Spots.Malefactor),
                new QuestLocation("Find Orb #1", MM3.Spots.Orb1),
                new QuestLocation("Find Orb #2", MM3.Spots.Orb2),
                new QuestLocation("Find Orb #3", MM3.Spots.Orb3),
                new QuestLocation("Find Orb #4", MM3.Spots.Orb4),
                new QuestLocation("Find Orb #5", MM3.Spots.Orb5),
                new QuestLocation("Find Orb #6", MM3.Spots.Orb6),
                new QuestLocation("Find Orb #7", MM3.Spots.Orb7),
                new QuestLocation("Find Orb #8", MM3.Spots.Orb8),
                new QuestLocation("Find Orb #9", MM3.Spots.Orb9),
                new QuestLocation("Find Orb #10", MM3.Spots.Orb10),
                new QuestLocation("Find Orb #11", MM3.Spots.Orb11),
                new QuestLocation("Find Orb #12", MM3.Spots.Orb12),
                new QuestLocation("Find Orb #13", MM3.Spots.Orb13),
                new QuestLocation("Find Orb #14", MM3.Spots.Orb14),
                new QuestLocation("Find Orb #15", MM3.Spots.Orb15),
                new QuestLocation("Find Orb #16", MM3.Spots.Orb16),
                new QuestLocation("Find Orb #17", MM3.Spots.Orb17),
                new QuestLocation("Find Orb #18", MM3.Spots.Orb18),
                new QuestLocation("Find Orb #19", MM3.Spots.Orb19),
                new QuestLocation("Find Orb #20", MM3.Spots.Orb20),
                new QuestLocation("Find Orb #21", MM3.Spots.Orb21),
                new QuestLocation("Find Orb #22", MM3.Spots.Orb22),
                new QuestLocation("Find Orb #23", MM3.Spots.Orb23),
                new QuestLocation("Find Orb #24", MM3.Spots.Orb24),
                new QuestLocation("Find Orb #25", MM3.Spots.Orb25),
                new QuestLocation("Find Orb #26", MM3.Spots.Orb26),
                new QuestLocation("Find Orb #27", MM3.Spots.Orb27),
                new QuestLocation("Find Orb #28", MM3.Spots.Orb28),
                new QuestLocation("Find Orb #29", MM3.Spots.Orb29),
                new QuestLocation("Find Orb #30", MM3.Spots.Orb30),
                new QuestLocation("Find Orb #31", MM3.Spots.Orb31));
            DeliverPowerOrbs.Postrequisites.Add(new QuestLocation("Deliver at least 11 Orbs to a single king", MM3Map.Unknown, -1, -1));

            FindHologramCards.PreQuest.Add(AddMainQuest(2, MummyPuzzle, quests, String.Empty));
            CorakSheltem.PreQuest.Add(AddMainQuest(3, FindHologramCards, quests, String.Empty));
            CorakSheltem.PreQuest.Add(AddMainQuest(4, DeliverPowerOrbs, quests, String.Empty));
            AddMainQuest(5, CorakSheltem, quests, String.Empty);

            SaveFountainHead.Postrequisites.Add(new QuestLocation("Release Morphose", MM3.Spots.Morphose));
            AddSideQuest(SaveFountainHead, quests, String.Empty, "25000 Exp, 2500 Gold, 6 Items");

            ReceiveBlessing.AddLocations(new QuestLocation("Donate at the Fountain Head temple (10 Gold)", MM3.Spots.Temple1),
                new QuestLocation("Donate at the Baywatch temple (25 Gold)", MM3.Spots.Temple2),
                new QuestLocation("Donate at the Wildabar temple (50 Gold)", MM3.Spots.Temple3),
                new QuestLocation("Donate at the Swamp Town temple (100 Gold)", MM3.Spots.Temple4),
                new QuestLocation("Donate at the Blistering Heights temple (200 Gold)", MM3.Spots.Temple5));
            ReceiveBlessing.Postrequisites.Add(new QuestLocation("Pray at the insect shrine", MM3.Spots.Insect));
            AddSideQuest(ReceiveBlessing, quests, String.Empty, "5 Level 2 Items");

            RecoverArtifacts.AddInformation(new QuestLocation(String.Format("Artifacts of Good in inventory: {0}", Artifacts.GoodInInventory), MM3Map.Unknown, -1, -1),
                new QuestLocation(String.Format("Artifacts of Neutrality in inventory: {0}", Artifacts.NeutralInInventory), MM3Map.Unknown, -1, -1),
                new QuestLocation(String.Format("Artifacts of Evil in inventory: {0}", Artifacts.EvilInInventory), MM3Map.Unknown, -1, -1));
            RecoverArtifacts.AddLocations(new QuestLocation(String.Format("Artifacts of Good delivered: {0}", Artifacts.GoodDelivered), MM3.Spots.Praythos),
                new QuestLocation(String.Format("Artifacts of Neutrality delivered: {0}", Artifacts.NeutralDelivered), MM3.Spots.Chathos),
                new QuestLocation(String.Format("Artifacts of Evil delivered: {0}", Artifacts.EvilDelivered), MM3.Spots.Pathos),
                new QuestLocation("Talk to Praythos", MM3.Spots.Praythos),
                new QuestLocation("Talk to Chathos", MM3.Spots.Chathos),
                new QuestLocation("Talk to Pathos", MM3.Spots.Pathos),
                new QuestLocation("Find Artifact of Evil #1", MM3.Spots.Evil1),
                new QuestLocation("Find Artifact of Evil #2", MM3.Spots.Evil2),
                new QuestLocation("Find Artifact of Evil #3", MM3.Spots.Evil3),
                new QuestLocation("Find Artifact of Evil #4", MM3.Spots.Evil4),
                new QuestLocation("Find Artifact of Evil #5", MM3.Spots.Evil5),
                new QuestLocation("Find Artifact of Evil #6", MM3.Spots.Evil6),
                new QuestLocation("Find Artifact of Evil #7", MM3.Spots.Evil7),
                new QuestLocation("Find Artifact of Evil #8", MM3.Spots.Evil8),
                new QuestLocation("Find Artifact of Evil #9", MM3.Spots.Evil9),
                new QuestLocation("Find Artifact of Evil #10", MM3.Spots.Evil10),
                new QuestLocation("Find Artifact of Evil #11", MM3.Spots.Evil11),
                new QuestLocation("Find Artifact of Evil #12", MM3.Spots.Evil12),
                new QuestLocation("Find Artifact of Evil #13", MM3.Spots.Evil13),
                new QuestLocation("Find Artifact of Evil #14", MM3.Spots.Evil14),
                new QuestLocation("Find Artifact of Evil #15", MM3.Spots.Evil15),
                new QuestLocation("Find Artifact of Good #1", MM3.Spots.Good1),
                new QuestLocation("Find Artifact of Good #2", MM3.Spots.Good2),
                new QuestLocation("Find Artifact of Good #3", MM3.Spots.Good3),
                new QuestLocation("Find Artifact of Good #4", MM3.Spots.Good4),
                new QuestLocation("Find Artifact of Good #5", MM3.Spots.Good5),
                new QuestLocation("Find Artifact of Good #6", MM3.Spots.Good6),
                new QuestLocation("Find Artifact of Good #7", MM3.Spots.Good7),
                new QuestLocation("Find Artifact of Good #8", MM3.Spots.Good8),
                new QuestLocation("Find Artifact of Good #9", MM3.Spots.Good9),
                new QuestLocation("Find Artifact of Good #10", MM3.Spots.Good10),
                new QuestLocation("Find Artifact of Good #11", MM3.Spots.Good11),
                new QuestLocation("Find Artifact of Neutrality #1", MM3.Spots.Neut1),
                new QuestLocation("Find Artifact of Neutrality #2", MM3.Spots.Neut2),
                new QuestLocation("Find Artifact of Neutrality #3", MM3.Spots.Neut3),
                new QuestLocation("Find Artifact of Neutrality #4", MM3.Spots.Neut4),
                new QuestLocation("Find Artifact of Neutrality #5", MM3.Spots.Neut5),
                new QuestLocation("Find Artifact of Neutrality #6", MM3.Spots.Neut6),
                new QuestLocation("Find Artifact of Neutrality #7", MM3.Spots.Neut7),
                new QuestLocation("Find Artifact of Neutrality #8", MM3.Spots.Neut8),
                new QuestLocation("Find Artifact of Neutrality #9", MM3.Spots.Neut9),
                new QuestLocation("Find Artifact of Neutrality #10", MM3.Spots.Neut10),
                new QuestLocation("Find Artifact of Neutrality #11", MM3.Spots.Neut11));
            AddSideQuest(RecoverArtifacts, quests, "Three Protectors", "100000 or 500000 Exp based on alignment");

            DeliverShells.AddLocations(new QuestLocation(String.Format("Wait until day 99 (currently day {0})", Shells.CurrentDay), MM3Map.Unknown, -1, -1),
                new QuestLocation(String.Format("Obtain a Sea Shell of Serenity ({0} in inventory)", Shells.ShellsInInventory), MM3.Spots.Seashell),
                new QuestLocation(String.Format("Give shells to Athea ({0} delivered)", Shells.ShellsDelivered), MM3.Spots.Athea));
            AddSideQuest(DeliverShells, quests, "Greek Brothers", "250000 Exp, 250000 Gold", Global.RepeatableQuest);

            DeliverPearls.AddInformation(new QuestLocation(String.Format("Precious Pearls in inventory: {0}", Pearls.PearlsInInventory), MM3Map.Unknown, -1, -1),
                new QuestLocation(String.Format("Pearls delivered to the Pirate Queen: {0}", Pearls.PearlsDelivered), MM3.Spots.Pirate));
            DeliverPearls.AddLocations(new QuestLocation("Find Precious Pearl #1", MM3.Spots.Pearl1),
                new QuestLocation("Find Precious Pearl #2", MM3.Spots.Pearl2),
                new QuestLocation("Find Precious Pearl #3", MM3.Spots.Pearl3),
                new QuestLocation("Find Precious Pearl #4", MM3.Spots.Pearl4),
                new QuestLocation("Find Precious Pearl #5", MM3.Spots.Pearl5),
                new QuestLocation("Find Precious Pearl #6", MM3.Spots.Pearl6),
                new QuestLocation(String.Format("Sit on Throne on day 60 (currently day {0})", Pearls.CurrentDay), MM3.Spots.PearlThrone));
            AddSideQuest(DeliverPearls, quests, "Pirate Queen", "100000 Exp, 25000 Gold", Global.RepeatableQuest);

            VisitBrothers.Prerequisites.Add(new QuestLocation("Visit Brother Alpha", MM3.Spots.Alpha));
            VisitBrothers.AddLocations(new QuestLocation("Visit Brother Beta", MM3.Spots.Beta),
                new QuestLocation("Visit Brother Gamma", MM3.Spots.Gamma),
                new QuestLocation("Visit Brother Delta", MM3.Spots.Delta));
            VisitBrothers.Postrequisites.Add(new QuestLocation("Visit Brother Zeta", MM3.Spots.Zeta));
            VisitBrothers.AddInformation(new QuestLocation(String.Format("Spend your {0}", Global.Plural(Brothers.CoinsInInventory, "Quatloo Coin")), MM3.Spots.SpendCoins));
            AddSideQuest(VisitBrothers, quests, String.Empty, "5 Quatloo Coins", Global.RepeatableQuest);

            DestroyLairs.AddLocations(new QuestLocation("Destroy the Orc Outpost", MM3.Spots.Destroy1),
                new QuestLocation("Destroy the Goblin Wagon", MM3.Spots.Destroy2),
                new QuestLocation("Destroy the Orcish Shrine", MM3.Spots.Destroy3),
                new QuestLocation("Destroy the Orc Outpost", MM3.Spots.Destroy4),
                new QuestLocation("Destroy the Goblin Hut", MM3.Spots.Destroy5),
                new QuestLocation("Destroy the Screamer Wagon", MM3.Spots.Destroy6),
                new QuestLocation("Destroy the Vampire Bat Wagon", MM3.Spots.Destroy7),
                new QuestLocation("Destroy the Spider's Nest", MM3.Spots.Destroy8),
                new QuestLocation("Destroy the Magic Mantis Pods", MM3.Spots.Destroy9),
                new QuestLocation("Destroy the Lamprey", MM3.Spots.Destroy10),
                new QuestLocation("Destroy the Bugaboo Larvae", MM3.Spots.Destroy11),
                new QuestLocation("Destroy the Ogre Meeting Hall", MM3.Spots.Destroy12),
                new QuestLocation("Destroy the Sprite Hut", MM3.Spots.Destroy13),
                new QuestLocation("Destroy the Wild Fungus Spores", MM3.Spots.Destroy14),
                new QuestLocation("Destroy the Oh No Bug Apiary", MM3.Spots.Destroy15),
                new QuestLocation("Destroy the Cyclops Shack", MM3.Spots.Destroy16),
                new QuestLocation("Destroy the Sprite Hut", MM3.Spots.Destroy17),
                new QuestLocation("Desecrate the Werewolf Shrine", MM3.Spots.Destroy18),
                new QuestLocation("Destroy the Major Devil Portal", MM3.Spots.Destroy19),
                new QuestLocation("Destroy the Hydra Breeding Grounds", MM3.Spots.Destroy20),
                new QuestLocation("Destroy the Major Demon Hut", MM3.Spots.Destroy21),
                new QuestLocation("Destroy the Fire Lizard Hatchery", MM3.Spots.Destroy22),
                new QuestLocation("Destroy the Fire Stalker Lair", MM3.Spots.Destroy23),
                new QuestLocation("Destroy the Death Locust Nest", MM3.Spots.Destroy24),
                new QuestLocation("Destroy the Rogue Meeting Place", MM3.Spots.Destroy25),
                new QuestLocation("Destroy the Barbarian Compound", MM3.Spots.Destroy26),
                new QuestLocation("Destroy the Death Locust Nest", MM3.Spots.Destroy27));
            AddSideQuest(DestroyLairs, quests, String.Empty, String.Empty);

            UseWell.AddLocations(new QuestLocation("Pay Obeyer to remember (100000 Gold)", MM3.Spots.Obeyer),
                new QuestLocation("Pay Slayer to remember (200000 Gold)", MM3.Spots.Slayer),
                new QuestLocation("Pay Soothsayer to remember (500000 Gold)", MM3.Spots.Soothsayer),
                new QuestLocation("Pay Purveyor to remember (1000000 Gold)", MM3.Spots.Purveyor));
            UseWell.Postrequisites.Add(new QuestLocation("Visit the Well of Remembrance", MM3.Spots.Remembrance));
            AddSideQuest(UseWell, quests, String.Empty, "10 Level 6 Items");

            ReleaseOrbs.AddLocations(new QuestLocation("Turn the altar of Proto to face north", MM3.Spots.Proto),
                new QuestLocation("Turn the altar of Barytro to face west", MM3.Spots.Barytro),
                new QuestLocation("Turn the altar of Dynatro to face north", MM3.Spots.Dynatro),
                new QuestLocation("Turn the altar of Penetro to face east", MM3.Spots.Penetro),
                new QuestLocation("Turn the altar of Positro to face south", MM3.Spots.Positro),
                new QuestLocation("Answer \"weeds\" to the riddle", MM3.Spots.Card004),
                new QuestLocation("Drink from the first chalice of death", MM3.Spots.Death1),
                new QuestLocation("Drink from the second chalice of death", MM3.Spots.Death2),
                new QuestLocation("Drink from the first chalice of stone", MM3.Spots.Stone1),
                new QuestLocation("Drink from the second chalice of stone", MM3.Spots.Stone2),
                new QuestLocation("Drink from the first chalice of eradication", MM3.Spots.Erad1),
                new QuestLocation("Drink from the second chalice of eradication", MM3.Spots.Erad2));
            ReleaseOrbs.Postrequisites.Add(new QuestLocation("Enter the field deactivation code \"JVC\"", MM3.Spots.JVC));
            AddSideQuest(ReleaseOrbs, quests, String.Empty, String.Empty);

            UseCrystals.AddLocations(new QuestLocation("Gain intellect at crystal #1", MM3.Spots.Int1),
                new QuestLocation("Gain intellect at crystal #2", MM3.Spots.Int2),
                new QuestLocation("Gain intellect at crystal #3", MM3.Spots.Int3),
                new QuestLocation("Gain intellect at crystal #4", MM3.Spots.Int4),
                new QuestLocation("Gain intellect at crystal #5", MM3.Spots.Int5),
                new QuestLocation("Gain personality at crystal #6", MM3.Spots.Pers1),
                new QuestLocation("Gain personality at crystal #7", MM3.Spots.Pers2),
                new QuestLocation("Gain personality at crystal #8", MM3.Spots.Pers3),
                new QuestLocation("Gain personality at crystal #9", MM3.Spots.Pers4),
                new QuestLocation("Gain accuracy at crystal #10", MM3.Spots.Acy1),
                new QuestLocation("Gain accuracy at crystal #11", MM3.Spots.Acy2),
                new QuestLocation("Gain luck at crystal #12", MM3.Spots.Luck1),
                new QuestLocation("Gain luck at crystal #13", MM3.Spots.Luck2),
                new QuestLocation("Answer \"20301\" to Lord Might", MM3.Spots.LordMight));
            UseCrystals.Postrequisites.Add(new QuestLocation("Pay Lord Might to reset the crystals (5000 Gems)", MM3.Spots.LordMight));
            AddSideQuest(UseCrystals, quests, String.Empty, "+25 Int, +25 Per, +20 Acy, +20 Lck", Global.RepeatableQuest);

            RaiseIsland.AddLocations(new QuestLocation("Obtain the Golden Pyramid Key Card", MM3.Spots.KeyCard));
            RaiseIsland.Postrequisites.Add(new QuestLocation("Use the password \"youth\" to raise the island", MM3.Spots.Youth));
            AddSideQuest(RaiseIsland, quests, String.Empty, String.Empty);

            FindJewelry.AddLocations(new QuestLocation("Find Jewelry #1", MM3.Spots.Jewelry1),
                new QuestLocation("Find Jewelry #2", MM3.Spots.Jewelry2),
                new QuestLocation("Find Jewelry #3", MM3.Spots.Jewelry3),
                new QuestLocation("Find Jewelry #4", MM3.Spots.Jewelry4),
                new QuestLocation("Find Ancient Jewelry #1", MM3.Spots.AJewelry1),
                new QuestLocation("Find Ancient Jewelry #2", MM3.Spots.AJewelry2),
                new QuestLocation("Find Ancient Jewelry #3", MM3.Spots.AJewelry3),
                new QuestLocation("Find Ancient Jewelry #4", MM3.Spots.AJewelry4),
                new QuestLocation("Find Ancient Jewelry #5", MM3.Spots.AJewelry5),
                new QuestLocation("Find Ancient Jewelry #6", MM3.Spots.AJewelry6),
                new QuestLocation("Find Ancient Jewelry #7", MM3.Spots.AJewelry7),
                new QuestLocation("Find Ancient Jewelry #8", MM3.Spots.AJewelry8));
            AddSideQuest(FindJewelry, quests, String.Empty, String.Empty);

            ReleaseGreywind.Prerequisites.Add(new QuestLocation("Find Castle Greywind", MM3.Spots.GreywindCastle));
            ReleaseGreywind.Prerequisites.Add(new QuestLocation("Speak to the Ghost of Greywind", MM3.Spots.Greywind));
            ReleaseGreywind.AddLocations(new QuestLocation("Turn the northeast hourglass sand-up", MM3.Spots.GlassNE),
                new QuestLocation("Turn the southwest hourglass sand-up", MM3.Spots.GlassSW),
                new QuestLocation("Turn the southeast hourglass sand-up", MM3.Spots.GlassSE),
                new QuestLocation("Ring the gong", MM3.Spots.Gong),
                new QuestLocation("Turn the northwest hourglass sand-down", MM3.Spots.GlassNW),
                new QuestLocation("Turn the northeast hourglass sand-down", MM3.Spots.GlassNE),
                new QuestLocation("Turn the southwest hourglass sand-down", MM3.Spots.GlassSW),
                new QuestLocation("Turn the southeast hourglass sand-down", MM3.Spots.GlassSE),
                new QuestLocation("Ring the gong", MM3.Spots.Gong));
            ReleaseGreywind.Postrequisites.Add(new QuestLocation("Return to the ghost of Greywind", MM3.Spots.Greywind));
            AddSideQuest(ReleaseGreywind, quests, "Ghost of Greywind", "2000000 Exp");

            ReleaseBlackwind.Prerequisites.Add(new QuestLocation("Visit the ghost of Blackwind", MM3.Spots.Blackwind));
            ReleaseBlackwind.AddLocations(new QuestLocation("Sacrifice a party member to Blood Mane", MM3.Spots.BloodMane),
                new QuestLocation("Sacrifice 100,000 Gold to Hamon Othreute", MM3.Spots.HamonOthreute),
                new QuestLocation("Sacrifice 1000 Gems to Tempest Storm", MM3.Spots.TempestStorm));
            ReleaseBlackwind.Postrequisites.Add(new QuestLocation("Return to the ghost of Blackwind", MM3.Spots.Blackwind));
            AddSideQuest(ReleaseBlackwind, quests, "Ghost of Blackwind", "2000000 Exp");

            DeliverSkulls.Prerequisites.Add(new QuestLocation("Visit Kranion, Priest of the Five Forces", MM3.Spots.Kranion));
            DeliverSkulls.AddInformation(new QuestLocation(String.Format("Sacred Silver Skulls in inventory: {0}", Skulls.SkullsInInventory), MM3Map.Unknown, -1, -1));
            DeliverSkulls.AddLocations(new QuestLocation(String.Format("Deliver Silver Skulls ({0} delivered)", Skulls.SkullsDelivered), MM3.Spots.Kranion),
                new QuestLocation("Find Skull #1", MM3.Spots.Skull1),
                new QuestLocation("Find Skull #2", MM3.Spots.Skull2),
                new QuestLocation("Find Skull #3", MM3.Spots.Skull3),
                new QuestLocation("Find Skull #4", MM3.Spots.Skull4),
                new QuestLocation("Find Skull #5", MM3.Spots.Skull5),
                new QuestLocation("Find Skull #6", MM3.Spots.Skull6),
                new QuestLocation("Find Skull #7", MM3.Spots.Skull7),
                new QuestLocation("Find Skull #8", MM3.Spots.Skull8),
                new QuestLocation("Find Skull #9", MM3.Spots.Skull9),
                new QuestLocation("Find Skull #10", MM3.Spots.Skull10),
                new QuestLocation("Find Skull #11", MM3.Spots.Skull11),
                new QuestLocation("Find Skull #12", MM3.Spots.Skull12),
                new QuestLocation("Find Skull #13", MM3.Spots.Skull13),
                new QuestLocation("Find Skull #14", MM3.Spots.Skull14),
                new QuestLocation("Find Skull #15", MM3.Spots.Skull15),
                new QuestLocation("Find Skull #16", MM3.Spots.Skull16),
                new QuestLocation("Find Skull #17", MM3.Spots.Skull17),
                new QuestLocation("Find Skull #18", MM3.Spots.Skull18),
                new QuestLocation("Find Skull #19", MM3.Spots.Skull19),
                new QuestLocation("Find Skull #20", MM3.Spots.Skull20));
            AddSideQuest(DeliverSkulls, quests, "Priest Kranion", "5000 Exp, 20000 Gold");

            ResurrectIcarus.Prerequisites.Add(new QuestLocation("Meet Princess Trueberry", MM3.Spots.Trueberry));
            ResurrectIcarus.AddLocations(new QuestLocation("Bring male characters to Athea", MM3.Spots.Athea),
                new QuestLocation(String.Format("Bring {0} to Trueberry", Global.Plural(Trueberry.LoveNeeded, "in-love character")), MM3.Spots.Trueberry),
                new QuestLocation("Visit the tomb of Icarus", MM3.Spots.Icarus),
                new QuestLocation("Receive the Alacorn from Princess Trueberry", MM3.Spots.Trueberry));
            ResurrectIcarus.Postrequisites.Add(new QuestLocation("Bring the Alacorn to Icarus", MM3.Spots.Icarus));
            AddSideQuest(ResurrectIcarus, quests, String.Empty, "2000000 Exp");

            JoinGuilds.AddLocations(new QuestLocation("Join the Raven's Guild (50 Gold)", MM3.Spots.JoinRaven),
                new QuestLocation("Join the Albatross Guild (100 Gold)", MM3.Spots.JoinAlbatross),
                new QuestLocation("Join the Falcon's Guild (1000 Gold)", MM3.Spots.JoinFalcon),
                new QuestLocation("Join the Buzzard's Guild (5000 Gold)", MM3.Spots.JoinBuzzard),
                new QuestLocation("Join the Eagle's Guild", MM3.Spots.JoinEagle));
            AddSideQuest(JoinGuilds, quests, String.Empty);

            DefeatArena.AddLocations(new QuestLocation(String.Format("Win 76 battle in the Arena ({0} won)", Global.Plural(Arena.ArenaWins, "battle")), MM3.Spots.Arena));
            AddSideQuest(DefeatArena, quests, String.Empty, "2850000 Exp");

            LearnSkills.AddLocations(new QuestLocation("Learn the Thievery skill from the Statue of Golden Mane (100000 Gold)", MM3.Spots.Tracker),
                new QuestLocation("Learn the Arms Master skill from Kelzen (500 Gold)", MM3.Spots.ArmsMaster),
                new QuestLocation("Learn the Astrologer skill from the granite head (5000 Gold)", MM3.Spots.Astrologer),
                new QuestLocation("Learn the Body Builder skill from Tsabu the Strong (200 Gold)", MM3.Spots.BodyBuilder),
                new QuestLocation("Learn the Cartographer skill from Cypher the Chart Maker (25 Gold)", MM3.Spots.Cartographer),
                new QuestLocation("Learn the Crusader skill from the Statue of Fire Mane", MM3.Spots.Crusader),
                new QuestLocation("Learn the Direction Sense skill from the Altar of Shuji (100 Gold)", MM3.Spots.Direction),
                new QuestLocation("Learn the Linguist skill from Lord Word (50 Gems)", MM3.Spots.Linguist),
                new QuestLocation("Learn the Merchant skill from the Golden Scale (5000 Gold)", MM3.Spots.Merchant),
                new QuestLocation("Learn the Mountaineer skill from Oro the Ranger (5000 Gold)", MM3.Spots.Mountaineer),
                new QuestLocation("Learn the Navigator skill from Shoron the Sailor (3000 Gold)", MM3.Spots.Navigator),
                new QuestLocation("Learn the Path Finder skill from Darek the Explorer (2500 Gold)", MM3.Spots.Path),
                new QuestLocation("Learn the Prayer Master skill from Lord Prayer (500 Gems)", MM3.Spots.Prayer),
                new QuestLocation("Learn the Prestidigitator skill from Lord Magic (500 Gold)", MM3.Spots.Prestidigitator),
                new QuestLocation("Learn the Swimmer skill from the Altar of Sufe (200 Gold)", MM3.Spots.Swimmer),
                new QuestLocation("Learn the Tracker skill from the Statue of Golden Mane (100000 Gold)", MM3.Spots.Tracker),
                new QuestLocation("Learn the Spot Secret Doors skill from the Altar of Eber (250 Gold)", MM3.Spots.Spot),
                new QuestLocation("Learn the Danger Sense skill from Altar of Yu'ude (500 Gold)", MM3.Spots.Danger));
            AddSideQuest(LearnSkills, quests, String.Empty);

            Level6Items.AddLocations(new QuestLocation("(6 Items) Sit on the throne on day 50", MM3.Spots.L6Throne1),
                new QuestLocation("(6 Items) Sit on the throne on day 60", MM3.Spots.L6Throne2),
                new QuestLocation("(4 Items) Bathe in the acid", MM3.Spots.L6Items1),
                new QuestLocation("(5 Items) Answer with passcode \"11\"", MM3.Spots.L6Items2),
                new QuestLocation("(1 Item) Search the cauldron", MM3.Spots.L6Items3),
                new QuestLocation("(4 Items) Take the treasure", MM3.Spots.L6Items4),
                new QuestLocation("(4 Items) Take the treasure", MM3.Spots.L6Items5),
                new QuestLocation("(4 Items) Take the treasure", MM3.Spots.L6Items6),
                new QuestLocation("(4 Items) Take the treasure", MM3.Spots.L6Items7),
                new QuestLocation("(10 Items) Take the treasure", MM3.Spots.L6Items8),
                new QuestLocation("(6 Items) Open the tomb", MM3.Spots.L6Items9),
                new QuestLocation("(6 Items) Open the tomb", MM3.Spots.L6Items10),
                new QuestLocation("(6 Items) Open the box", MM3.Spots.L6Items11),
                new QuestLocation("(6 Items) Open the box", MM3.Spots.L6Items12),
                new QuestLocation("(6 Items) Open the box", MM3.Spots.L6Items13),
                new QuestLocation("(5 Items) Drink the liquid", MM3.Spots.L6Items23),
                new QuestLocation("(5 Items) Drink the liquid", MM3.Spots.L6Items24),
                new QuestLocation("(1 Item) Enter the shack with 0 SP", MM3.Spots.L6Items25),
                new QuestLocation("(1 Item) Search the alcove", MM3.Spots.L6Items26),
                new QuestLocation("(1 Item) Search the alcove", MM3.Spots.L6Items27),
                new QuestLocation("(1 Item) Search the alcove", MM3.Spots.L6Items28),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items29),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items30),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items31),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items32),
                new QuestLocation("(10 Items) Destroy the hut", MM3.Spots.L6Items33),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items34),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items35),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items36),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items37),
                new QuestLocation("(2 Items) Search the crate", MM3.Spots.L6Items38),
                new QuestLocation("(5 Items) Enter with Ancient Fizbin", MM3.Spots.L6Items39),
                new QuestLocation("(1 Item) Retrieve the treasure", MM3.Spots.L6Items40),
                new QuestLocation("(1 Item) Retrieve the treasure", MM3.Spots.L6Items41),
                new QuestLocation("(1 Item) Retrieve the treasure", MM3.Spots.L6Items42),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items43),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items44),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items45),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items46),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items47),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items48),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items49),
                new QuestLocation("(2 Items) Dig up the treasure", MM3.Spots.L6Items50));
            AddSideQuest(Level6Items, quests, String.Empty, "144 Level 6 Items", Global.RepeatableQuest);

            TempStats.AddLocations(new QuestLocation("(+20 AC) Drink from the well", MM3.Spots.TempAC),
                new QuestLocation("(+20 Personality) Drink from the well", MM3.Spots.TempPers),
                new QuestLocation("(+20 Intellect) Drink from the well", MM3.Spots.TempInt),
                new QuestLocation("(+25 Magic Res) Pay 100 gems", MM3.Spots.TempMagic),
                new QuestLocation("(+50 Poison Res) Drink from the well", MM3.Spots.TempPoison),
                new QuestLocation("(+30 Might) Drink from the fountain", MM3.Spots.TempMight30),
                new QuestLocation("(+30 Speed) Drink from the fountain", MM3.Spots.TempSpeed30),
                new QuestLocation("(+50 Cold Res) Pay 100 gold", MM3.Spots.TempCold50),
                new QuestLocation("(+20 Level) Drink from the fountain", MM3.Spots.TempLevel20),
                new QuestLocation("(+50 Fire Res) Donate 100 gold", MM3.Spots.TempFire50),
                new QuestLocation("(+50 AC if evil) Pray at the shrine", MM3.Spots.TempACEvil),
                new QuestLocation("(+60 Accuracy) Drink from the well", MM3.Spots.TempAcy),
                new QuestLocation("(+60 Endurance) Drink from the well", MM3.Spots.TempEnd60),
                new QuestLocation("(+100 All Stats) Drink from the fountain", MM3.Spots.TempStats),
                new QuestLocation("(+50 All Resistances) Enter the hut", MM3.Spots.TempElemental),
                new QuestLocation("(+100 Endurance) Donate 10000 Gold", MM3.Spots.TempEnd100),
                new QuestLocation("(+100 Speed) Donate 15000 gold", MM3.Spots.TempSpeed100),
                new QuestLocation("(+100 Accuracy) Donate 20000 gold", MM3.Spots.TempAcy100),
                new QuestLocation("(+100 Might) Donate 10000 gold", MM3.Spots.TempMight100),
                new QuestLocation("(+50 Level) Pay 10000 Gold", MM3.Spots.TempLevel50),
                new QuestLocation("(+100 Luck, 17% chance) Throw in a coin", MM3.Spots.TempLuck100),
                new QuestLocation("(+60 Cold Res) Read the inscription", MM3.Spots.TempCold60),
                new QuestLocation("(+60 Elec Res) Read the inscription", MM3.Spots.TempElec60),
                new QuestLocation("(+60 Fire Res) Read the inscription", MM3.Spots.TempFire60),
                new QuestLocation("(+60 Poison Res) Read the inscription", MM3.Spots.TempPoison60));
            AddSideQuest(TempStats, quests, String.Empty, String.Empty, Global.RepeatableQuest);

            PermStats.AddLocations(new QuestLocation("(+20 Resistances) Drink the Elixer", MM3.Spots.PermRes20),
                new QuestLocation("(+10 All Stats) Drink the Elixer", MM3.Spots.PermStats10A),
                new QuestLocation("(+10 All Stats) Drink the Elixer", MM3.Spots.PermStats10B),
                new QuestLocation("(+5 Level) Drink the Elixer", MM3.Spots.PermLevel5A),
                new QuestLocation("(+50 Accuracy) Drink the liquid", MM3.Spots.PermAcy50A),
                new QuestLocation("(+50 Speed) Touch the crystal", MM3.Spots.PermSpd50),
                new QuestLocation("(+50 Luck) Touch the shard", MM3.Spots.PermLck50),
                new QuestLocation("(+10 Stats) Sit in the throne on day 50", MM3.Spots.PermStats10C),
                new QuestLocation("(+5 Level) Drink the concoction", MM3.Spots.PermLevel5B),
                new QuestLocation("(+50 Intellect) Drink the concoction", MM3.Spots.PermInt50A),
                new QuestLocation("(+50 Endurance) Drink the concoction", MM3.Spots.PermEnd50A),
                new QuestLocation("(+50 Personality) Drink the concoction", MM3.Spots.PermPer50A),
                new QuestLocation("(+30 Magic Res) Search the tide", MM3.Spots.PermMagRes30),
                new QuestLocation("(+20 Elec Res) Search the pool", MM3.Spots.PermElecRes20A),
                new QuestLocation("(+30 Energy Res) Search the tide", MM3.Spots.PermEnergyRes30),
                new QuestLocation("(+20 Elec Res) Search the tide", MM3.Spots.PermElecRes20B),
                new QuestLocation("(+50 Personality) Search the tide", MM3.Spots.PermPer50B),
                new QuestLocation("(+50 Accuracy) Search the tide", MM3.Spots.PermAcy50B),
                new QuestLocation("(+50 Intellect) Search the tide", MM3.Spots.PermInt50B),
                new QuestLocation("(+50 Endurance) Search the tide", MM3.Spots.PermEnd50B),
                new QuestLocation("(+5 Might) Insert a Quatloo coin", MM3.Spots.PermMight5A),
                new QuestLocation("(+5 Endurance) Insert a Quatloo coin", MM3.Spots.PermEnd5A),
                new QuestLocation("(+5 Accuracy) Insert a Quatloo coin", MM3.Spots.PermAcy5A),
                new QuestLocation("(+2 Level) Search the spring", MM3.Spots.PermLevel2Q),
                new QuestLocation("(+25 Poison Res) Search the spring", MM3.Spots.PermPoisonRes25A),
                new QuestLocation("(+25 Poison Res) Search the spring", MM3.Spots.PermPoisonRes25B),
                new QuestLocation("(+5 Level) Drink the juice", MM3.Spots.PermLevel5C),
                new QuestLocation("(+5 Level) Drink the juice", MM3.Spots.PermLevel5D),
                new QuestLocation("(+10 Might) Search the remains", MM3.Spots.PermMight10A),
                new QuestLocation("(+10 Might) Search the remains", MM3.Spots.PermMight10B),
                new QuestLocation("(+20 Endurance) Search the remains", MM3.Spots.PermEnd20A),
                new QuestLocation("(+20 Speed) Examine the skeleton", MM3.Spots.PermSpeed20A),
                new QuestLocation("(+10 Endurance) Examine the skeleton", MM3.Spots.PermEnd10A),
                new QuestLocation("(+25 Might) Search the remains", MM3.Spots.PermMight25),
                new QuestLocation("(+2 Level) Examine the skeleton", MM3.Spots.PermLevel2B),
                new QuestLocation("(+5 Intellect) Touch the shard", MM3.Spots.PermInt5A),
                new QuestLocation("(+5 Personality) Touch the crystal", MM3.Spots.PermPer5A),
                new QuestLocation("(+5 Personality) Touch the crystal", MM3.Spots.PermPer5B),
                new QuestLocation("(+10 Accuracy) Drink the liquid", MM3.Spots.PermAcy10A),
                new QuestLocation("(+10 Luck) Touch the shard", MM3.Spots.PermLuck10A),
                new QuestLocation("(+5 Intellect) Touch the shard", MM3.Spots.PermInt5B),
                new QuestLocation("(+10 Personality) Touch the crystal", MM3.Spots.PermPer10),
                new QuestLocation("(+10 Luck) Touch the shard", MM3.Spots.PermLuck10B),
                new QuestLocation("(+5 Intellect) Touch the shard", MM3.Spots.PermInt5C),
                new QuestLocation("(+10 Accuracy) Drink the liquid", MM3.Spots.PermAcy10B),
                new QuestLocation("(+5 Intellect) Touch the shard", MM3.Spots.PermInt5D),
                new QuestLocation("(+5 Personality) Touch the crystal", MM3.Spots.PermPer5C),
                new QuestLocation("(+5 Intellect) Touch the shard", MM3.Spots.PermInt5E),
                new QuestLocation("(+20 Magic Res) Dip your hand in the brew", MM3.Spots.PermMagicRes20A),
                new QuestLocation("(+20 Magic Res) Dip your hand in the brew", MM3.Spots.PermMagicRes20B),
                new QuestLocation("(+1 Level) Search the bowl", MM3.Spots.PermLevel1A),
                new QuestLocation("(+1 Level) Search the bowl", MM3.Spots.PermLevel1B),
                new QuestLocation("(+1 Level) Search the bowl", MM3.Spots.PermLevel1C),
                new QuestLocation("(+1 Level) Search the bowl", MM3.Spots.PermLevel1D),
                new QuestLocation("(+20 Magic Res) Dip your hand in the brew", MM3.Spots.PermMagicRes20C),
                new QuestLocation("(+10 Intellect) Wade in the pool", MM3.Spots.PermInt10A),
                new QuestLocation("(+10 Speed) Search the well", MM3.Spots.PermSpeed10A),
                new QuestLocation("(+10 Intellect) Wade in the pool", MM3.Spots.PermInt10B),
                new QuestLocation("(+10 Intellect) Wade in the pool", MM3.Spots.PermInt10C),
                new QuestLocation("(+10 Speed) Search the well", MM3.Spots.PermSpeed10B),
                new QuestLocation("(+10 Intellect) Wade in the pool", MM3.Spots.PermInt10D),
                new QuestLocation("(+10 Speed) Wade in the well", MM3.Spots.PermSpeed10C),
                new QuestLocation("(+10 Speed) Wade in the well", MM3.Spots.PermSpeed10D),
                new QuestLocation("(+10 Speed) Wade in the well", MM3.Spots.PermSpeed10E),
                new QuestLocation("(+10 Intellect) Wade in the well", MM3.Spots.PermInt10E),
                new QuestLocation("(+20 Level, 3 Stats) Sit on the throne", MM3.Spots.PermLevel3Stats20A),
                new QuestLocation("(+20 Level, 3 Stats) Sit on the throne", MM3.Spots.PermLevel3Stats20B),
                new QuestLocation("(+20 Level, 3 Stats) Sit on the throne", MM3.Spots.PermLevel3Stats20C),
                new QuestLocation("(+20 Level, 3 Stats) Sit on the throne", MM3.Spots.PermLevel3Stats20D),
                new QuestLocation("(+20 Might) Touch the gem", MM3.Spots.PermMight20A),
                new QuestLocation("(+20 Intellect) Touch the gem", MM3.Spots.PermInt20),
                new QuestLocation("(+20 Personality) Touch the gem", MM3.Spots.PermPer20),
                new QuestLocation("(+20 Luck) Touch the gem", MM3.Spots.PermLuck20),
                new QuestLocation("(+20 Endurance) Touch the gem", MM3.Spots.PermEnd20B),
                new QuestLocation("(+2 Level) Touch the gem", MM3.Spots.PermLevel2C),
                new QuestLocation("(+20 Accuracy) Touch the gem", MM3.Spots.PermAcy20),
                new QuestLocation("(+20 Speed) Touch the gem", MM3.Spots.PermSpeed20B),
                new QuestLocation("(+10 Might, +10 Endurance) Pay 10000 gold", MM3.Spots.PermMight10End10),
                new QuestLocation("(+1 Accuracy) Pay 1000 Gold", MM3.Spots.PermAcy1),
                new QuestLocation("(+1 Endurance, 10% chance) Drink from the well", MM3.Spots.PermEnd1),
                new QuestLocation("(+10 Stats) Pay 1000 gems", MM3.Spots.PermStats10),
                new QuestLocation("(+2 Poison Res, 10% chance) Drink from the well", MM3.Spots.PermPoisonRes2),
                new QuestLocation("(+2 Might, 10% chance) Drink from the well", MM3.Spots.PermMight2),
                new QuestLocation("(+5 Endurance) Search the barrel", MM3.Spots.PermEnd5B),
                new QuestLocation("(+5 Might) Search the barrel", MM3.Spots.PermMight5B),
                new QuestLocation("(+5 Intellect) Search the barrel", MM3.Spots.PermInt5F),
                new QuestLocation("(+5 Personality) Search the barrel", MM3.Spots.PermPer5D),
                new QuestLocation("(+5 Accuracy) Search the barrel", MM3.Spots.PermAcy5B),
                new QuestLocation("(+5 Endurance) Search the barrel", MM3.Spots.PermEnd5C),
                new QuestLocation("(+5 Endurance) Search the barrel", MM3.Spots.PermEnd5D),
                new QuestLocation("(+5 Speed) Search the barrel", MM3.Spots.PermSpeed5),
                new QuestLocation("(+5 Luck) Search the barrel", MM3.Spots.PermLuck5A),
                new QuestLocation("(+5 Endurance) Search the barrel", MM3.Spots.PermEnd5E),
                new QuestLocation("(+10 Endurance) Drink the brew", MM3.Spots.PermEnd10B),
                new QuestLocation("(+10 Speed) Drink the brew", MM3.Spots.PermSpeed10),
                new QuestLocation("(+5 Accuracy) Drink the brew", MM3.Spots.PermAcy5C),
                new QuestLocation("(+5 Personality) Drink the brew", MM3.Spots.PermPer5E),
                new QuestLocation("(+10 Might) Drink the brew", MM3.Spots.PermMight10C),
                new QuestLocation("(+5 Luck) Drink the brew", MM3.Spots.PermLuck5B),
                new QuestLocation("(+5 Intellect) Drink the brew", MM3.Spots.PermInt5G),
                new QuestLocation("(+5 Endurance) Drink the brew", MM3.Spots.PermEnd5F),
                new QuestLocation("(+20 Might) Step close to the altar", MM3.Spots.PermMight20B),
                new QuestLocation("(+25 Endurance) Step close to the altar", MM3.Spots.PermEnd25),
                new QuestLocation("(+25 Fire Res) Step close to the gem", MM3.Spots.PermFireRes25),
                new QuestLocation("(+25 Elec Res) Step close to the gem", MM3.Spots.PermElecRes25),
                new QuestLocation("(+25 Cold Res) Step close to the gem", MM3.Spots.PermColdRes25),
                new QuestLocation("(+30 Poison Res) Step close to the gem", MM3.Spots.PermPoisonRes30),
                new QuestLocation("(+20 Energy Res) Step close to the gem", MM3.Spots.PermEnergyRes20),
                new QuestLocation("(+25 Magic Res) Step close to the gem", MM3.Spots.PermMagicRes25));
            AddSideQuest(PermStats, quests, String.Empty, String.Empty, Global.RepeatableQuest);

            Hirelings.AddLocations(new QuestLocation("Free Son of Abu from the shackles", MM3.Spots.SonOfAbu),
                new QuestLocation("Free Charity from the shackles", MM3.Spots.Charity),
                new QuestLocation("Free Darlana from the shackles", MM3.Spots.Darlana),
                new QuestLocation("Free Sir Galant from the bonds", MM3.Spots.SirGalant),
                new QuestLocation("Free Lone Wolf from the barrel", MM3.Spots.LoneWolf),
                new QuestLocation("Free Wartowsan from the barrel", MM3.Spots.Wartowsan));
            AddSideQuest(Hirelings, quests);

            if (CharClass == GenericClass.Cleric || CharClass == GenericClass.Paladin)
            {
                AddSpellQuest(SpellClericLight, quests, MM3SpellIndex.LightCleric, 0,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.Light));
                AddSpellQuest(SpellClericAwaken, quests, MM3SpellIndex.AwakenCleric, 0);
                AddSpellQuest(SpellClericFirstAid, quests, MM3SpellIndex.FirstAid, 0);
                AddSpellQuest(SpellClericFlyingFist, quests, MM3SpellIndex.FlyingFist, 0);
                AddSpellQuest(SpellClericRevitalize, quests, MM3SpellIndex.Revitalize, 0);
                AddSpellQuest(SpellClericCureWounds, quests, MM3SpellIndex.CureWounds, 0);
                AddSpellQuest(SpellClericSparks, quests, MM3SpellIndex.Sparks, 0);
                AddSpellQuest(SpellClericProtectionfromElements, quests, MM3SpellIndex.ProtectionFromElements, 0);
                AddSpellQuest(SpellClericPain, quests, MM3SpellIndex.Pain, 0,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.Pain));
                AddSpellQuest(SpellClericSuppressPoison, quests, MM3SpellIndex.SuppressPoison, 0,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.SuppressPoison));
                AddSpellQuest(SpellClericSuppressDisease, quests, MM3SpellIndex.SuppressDisease, 1,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.SuppressDisease));
                AddSpellQuest(SpellClericTurnUndead, quests, MM3SpellIndex.TurnUndead, 1,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.TurnUndead1),
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.TurnUndead2));
                AddSpellQuest(SpellClericSilence, quests, MM3SpellIndex.Silence, 1,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.Silence));
                AddSpellQuest(SpellClericBlessed, quests, MM3SpellIndex.Blessed, 01,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Blessed));
                AddSpellQuest(SpellClericHolyBonus, quests, MM3SpellIndex.HolyBonus, 1,
                    new QuestLocation("Retrieve the scroll from the grave", MM3.Spots.HolyBonus));
                AddSpellQuest(SpellClericPowerCure, quests, MM3SpellIndex.PowerCure, 1,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.PowerCure));
                AddSpellQuest(SpellClericHeroism, quests, MM3SpellIndex.Heroism, 2,
                    new QuestLocation("Retrieve the scroll from the grave", MM3.Spots.Heroism));
                AddSpellQuest(SpellClericImmobilize, quests, MM3SpellIndex.Immobilize, 2,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.Immobilize));
                AddSpellQuest(SpellClericColdRay, quests, MM3SpellIndex.ColdRay, 2,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.ColdRay));
                AddSpellQuest(SpellClericCurePoison, quests, MM3SpellIndex.CurePoison, 2,
                    new QuestLocation("Retrieve the scroll from the tree", MM3.Spots.CurePoison1),
                    new QuestLocation("Retrieve the scroll from the bag", MM3.Spots.CurePoison2),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.CurePoison3));
                AddSpellQuest(SpellClericAcidSpray, quests, MM3SpellIndex.AcidSpray, 2,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.AcidSpray));
                AddSpellQuest(SpellClericCureDisease, quests, MM3SpellIndex.CureDisease, 2,
                    new QuestLocation("Retrieve the scroll from the bag", MM3.Spots.CureDisease));
                AddSpellQuest(SpellClericCureParalysis, quests, MM3SpellIndex.CureParalysis, 3,
                    new QuestLocation("Retrieve the scroll from the grave", MM3.Spots.CureParalysis));
                AddSpellQuest(SpellClericParalyze, quests, MM3SpellIndex.Paralyze, 3,
                    new QuestLocation("Retrieve the scroll from gem", MM3.Spots.Paralyze));
                AddSpellQuest(SpellClericCreateFood, quests, MM3SpellIndex.CreateFood, 3,
                    new QuestLocation("Retrieve the scroll from the coffin", MM3.Spots.CreateFood1),
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.CreateFood2));
                AddSpellQuest(SpellClericFieryFlail, quests, MM3SpellIndex.FieryFlail, 3,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.FieryFlail));
                AddSpellQuest(SpellClericTownPortal, quests, MM3SpellIndex.TownPortal, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.TownPortal));
                AddSpellQuest(SpellClericStonetoFlesh, quests, MM3SpellIndex.StoneToFlesh, 3,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.StonetoFlesh));
                AddSpellQuest(SpellClericHalfforMe, quests, MM3SpellIndex.HalfForMe, 3,
                    new QuestLocation("Retrieve the scroll from bones", MM3.Spots.HalfforMe));
                AddSpellQuest(SpellClericRaiseDead, quests, MM3SpellIndex.RaiseDead, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.RaiseDead1),
                    new QuestLocation("Retrieve the scroll from the chest", MM3.Spots.RaiseDead2));
                AddSpellQuest(SpellClericMoonRay, quests, MM3SpellIndex.MoonRay, 4,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.MoonRay));
                AddSpellQuest(SpellClericMassDistortion, quests, MM3SpellIndex.MassDistortion, 4,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.MassDistortion));
                AddSpellQuest(SpellClericHolyWord, quests, MM3SpellIndex.HolyWord, 4,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.HolyWord1),
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.HolyWord2));
                AddSpellQuest(SpellClericResurrect, quests, MM3SpellIndex.Resurrection, 4,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Resurrect));
                AddSpellQuest(SpellClericSunRay, quests, MM3SpellIndex.SunRay, 4,
                    new QuestLocation("Retrieve the scroll from the tomb", MM3.Spots.SunRay));
                AddSpellQuest(SpellClericDivineIntervention, quests, MM3SpellIndex.DivineIntervention, 4,
                    new QuestLocation("Retrieve the scroll from the tomb", MM3.Spots.DivineIntervention1),
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.DivineIntervention2));
            }
            if (CharClass == GenericClass.Sorcerer || CharClass == GenericClass.Archer)
            {
                AddSpellQuest(SpellSorcererLight, quests, MM3SpellIndex.LightSorcerer, 0,
                    new QuestLocation("Retrieve scroll from skeleton", MM3.Spots.Light));
                AddSpellQuest(SpellSorcererAwaken, quests, MM3SpellIndex.AwakenSorcerer, 0);
                AddSpellQuest(SpellSorcererDetectMagic, quests, MM3SpellIndex.DetectMagic, 0);
                AddSpellQuest(SpellSorcererElementalArrow, quests, MM3SpellIndex.ElementalArrow, 0);
                AddSpellQuest(SpellSorcererEnergyBlast, quests, MM3SpellIndex.EnergyBlast, 0);
                AddSpellQuest(SpellSorcererSleep, quests, MM3SpellIndex.Sleep, 0);
                AddSpellQuest(SpellSorcererCreateRope, quests, MM3SpellIndex.CreateRope, 0);
                AddSpellQuest(SpellSorcererToxicCloud, quests, MM3SpellIndex.ToxicCloud, 0);
                AddSpellQuest(SpellSorcererJump, quests, MM3SpellIndex.Jump, 1,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.Jump));
                AddSpellQuest(SpellSorcererAcidStream, quests, MM3SpellIndex.AcidStream, 1,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.AcidStream));
                AddSpellQuest(SpellSorcererLevitate, quests, MM3SpellIndex.Levitate, 1,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.Levitate));
                AddSpellQuest(SpellSorcererWizardEye, quests, MM3SpellIndex.WizardEye, 1,
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.WizardEye));
                AddSpellQuest(SpellSorcererIdentifyMonster, quests, MM3SpellIndex.IdentifyMonster, 1,
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.IdentifyMonster));
                AddSpellQuest(SpellSorcererLightningBolt, quests, MM3SpellIndex.LightningBolt, 1,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.LightningBolt1),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.LightningBolt2));
                AddSpellQuest(SpellSorcererLloydsBeacon, quests, MM3SpellIndex.LloydsBeacon, 2,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.LloydsBeacon));
                AddSpellQuest(SpellSorcererPowerShield, quests, MM3SpellIndex.PowerShield, 2,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.PowerShield));
                AddSpellQuest(SpellSorcererDetectMonster, quests, MM3SpellIndex.DetectMonster, 2,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.DetectMonster));
                AddSpellQuest(SpellSorcererFireball, quests, MM3SpellIndex.Fireball, 2,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.Fireball1),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.Fireball2),
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Fireball3));
                AddSpellQuest(SpellSorcererTimeDistortion, quests, MM3SpellIndex.TimeDistortion, 2,
                    new QuestLocation("Retrieve the scroll from the chest", MM3.Spots.TimeDistortion1),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.TimeDistortion2),
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.TimeDistortion3));
                AddSpellQuest(SpellSorcererFeebleMind, quests, MM3SpellIndex.FeebleMind, 2,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.FeebleMind));
                AddSpellQuest(SpellSorcererTeleport, quests, MM3SpellIndex.Teleport, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Teleport1),
                    new QuestLocation("Retrieve the scroll from the alcove", MM3.Spots.Teleport2));
                AddSpellQuest(SpellSorcererFingerofDeath, quests, MM3SpellIndex.FingerOfDeath, 3,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.FingerofDeath));
                AddSpellQuest(SpellSorcererSuperShelter, quests, MM3SpellIndex.SuperShelter, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.SuperShelter));
                AddSpellQuest(SpellSorcererDragonBreath, quests, MM3SpellIndex.DragonBreath, 3,
                    new QuestLocation("Retrieve the scroll from the chest", MM3.Spots.DragonBreath1),
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.DragonBreath2),
                    new QuestLocation("Retrieve the scroll from the coffin", MM3.Spots.DragonBreath3));
                AddSpellQuest(SpellSorcererRechargeItem, quests, MM3SpellIndex.RechargeItem, 3,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.RechargeItem));
                AddSpellQuest(SpellSorcererFantasticFreeze, quests, MM3SpellIndex.FantasticFreeze, 3,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.FantasticFreeze));
                AddSpellQuest(SpellSorcererDuplication, quests, MM3SpellIndex.Duplication, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Duplication));
                AddSpellQuest(SpellSorcererDisintegrate, quests, MM3SpellIndex.Disintegration, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Disintegrate));
                AddSpellQuest(SpellSorcererEtherealize, quests, MM3SpellIndex.Etherealize, 4,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Etherealize1),
                    new QuestLocation("Retrieve the scroll from the alcove", MM3.Spots.Etherealize2));
                AddSpellQuest(SpellSorcererDancingSword, quests, MM3SpellIndex.DancingSword, 4,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.DancingSword));
                AddSpellQuest(SpellSorcererEnchantItem, quests, MM3SpellIndex.EnchantItem, 4,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.EnchantItem1),
                    new QuestLocation("Retrieve the scroll from the alcove", MM3.Spots.EnchantItem2));
                AddSpellQuest(SpellSorcererIncinerate, quests, MM3SpellIndex.Incinerate, 4,
                    new QuestLocation("Retrieve the scroll from the chest", MM3.Spots.Incinerate));
                AddSpellQuest(SpellSorcererMegaVolts, quests, MM3SpellIndex.MegaVolts, 4,
                    new QuestLocation("Retrieve the scroll from altar", MM3.Spots.MegaVolts));
                AddSpellQuest(SpellSorcererInferno, quests, MM3SpellIndex.Inferno, 4,
                    new QuestLocation("Retrieve the scroll from altar", MM3.Spots.Inferno));
                AddSpellQuest(SpellSorcererImplosion, quests, MM3SpellIndex.Implosion, 4,
                    new QuestLocation("Retrieve the scroll from altar", MM3.Spots.Implosion));
                AddSpellQuest(SpellSorcererStarBurst, quests, MM3SpellIndex.StarBurst, 4,
                    new QuestLocation("Retrieve the scroll from altar", MM3.Spots.StarBurst));
            }
            if (CharClass == GenericClass.Druid || CharClass == GenericClass.Ranger)
            {
                AddSpellQuest(SpellDruidLight, quests, MM3SpellIndex.LightDruid, 0,
                    new QuestLocation("Retrieve scroll from skeleton", MM3.Spots.Light));
                AddSpellQuest(SpellDruidAwaken, quests, MM3SpellIndex.AwakenDruid, 0);
                AddSpellQuest(SpellDruidFirstAid, quests, MM3SpellIndex.FirstAidDruid, 0);
                AddSpellQuest(SpellDruidDetectMagic, quests, MM3SpellIndex.DetectMagicDruid, 0);
                AddSpellQuest(SpellDruidElementalArrow, quests, MM3SpellIndex.ElementalArrowDruid, 0);
                AddSpellQuest(SpellDruidRevitalize, quests, MM3SpellIndex.RevitalizeDruid, 0);
                AddSpellQuest(SpellDruidCreateRope, quests, MM3SpellIndex.CreateRopeDruid, 0);
                AddSpellQuest(SpellDruidSleep, quests, MM3SpellIndex.SleepDruid, 0);
                AddSpellQuest(SpellDruidProtectionfromElements, quests, MM3SpellIndex.ProtectionFromElementsDruid, 0);
                AddSpellQuest(SpellDruidSuppressPoison, quests, MM3SpellIndex.SuppressPoisonDruid, 0,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.SuppressPoison));
                AddSpellQuest(SpellDruidSuppressDisease, quests, MM3SpellIndex.SuppressDiseaseDruid, 1,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.SuppressDisease));
                AddSpellQuest(SpellDruidIdentifyMonster, quests, MM3SpellIndex.IdentifyMonsterDruid, 1,
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.IdentifyMonster));
                AddSpellQuest(SpellDruidNaturesCure, quests, MM3SpellIndex.NaturesCure, 1,
                    new QuestLocation("Retrieve the scroll from the bag", MM3.Spots.NaturesCure));
                AddSpellQuest(SpellDruidImmobilize, quests, MM3SpellIndex.ImmobilizeDruid, 1,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.Immobilize));
                AddSpellQuest(SpellDruidWalkonWater, quests, MM3SpellIndex.WalkOnWater, 2,
                    new QuestLocation("Retrieve the scroll from the bag", MM3.Spots.WalkonWater));
                AddSpellQuest(SpellDruidFrostBite, quests, MM3SpellIndex.FrostBite, 2);
                AddSpellQuest(SpellDruidLightningBolt, quests, MM3SpellIndex.LightningBoltDruid, 2,
                    new QuestLocation("Retrieve the scroll from the remains", MM3.Spots.LightningBolt1),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.LightningBolt2));
                AddSpellQuest(SpellDruidAcidSpray, quests, MM3SpellIndex.AcidSprayDruid, 2,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.AcidSpray));
                AddSpellQuest(SpellDruidColdRay, quests, MM3SpellIndex.ColdRayDruid, 2,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.ColdRay));
                AddSpellQuest(SpellDruidNaturesGate, quests, MM3SpellIndex.NaturesGate, 2,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.NaturesGate));
                AddSpellQuest(SpellDruidFireball, quests, MM3SpellIndex.FireballDruid, 3,
                    new QuestLocation("Retrieve the scroll from the skeleton", MM3.Spots.Fireball1),
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.Fireball2),
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.Fireball3));
                AddSpellQuest(SpellDruidDeadlySwarm, quests, MM3SpellIndex.DeadlySwarm, 3,
                    new QuestLocation("Retrieve the scroll from the bones", MM3.Spots.DeadlySwarm));
                AddSpellQuest(SpellDruidCureParalysis, quests, MM3SpellIndex.CureParalysisDruid, 3,
                    new QuestLocation("Retrieve the scroll from the grave", MM3.Spots.CureParalysis));
                AddSpellQuest(SpellDruidParalyze, quests, MM3SpellIndex.ParalyzeDruid, 3,
                    new QuestLocation("Retrieve the scroll from gem", MM3.Spots.Paralyze));
                AddSpellQuest(SpellDruidCreateFood, quests, MM3SpellIndex.CreateFoodDruid, 3,
                    new QuestLocation("Retrieve the scroll from the coffin", MM3.Spots.CreateFood1),
                    new QuestLocation("Retrieve the scroll from the ground", MM3.Spots.CreateFood2));
                AddSpellQuest(SpellDruidStonetoFlesh, quests, MM3SpellIndex.StoneToFleshDruid, 3,
                    new QuestLocation("Retrieve the scroll from the gem", MM3.Spots.StonetoFlesh));
                AddSpellQuest(SpellDruidRaiseDead, quests, MM3SpellIndex.RaiseDeadDruid, 3,
                    new QuestLocation("Retrieve the scroll from the altar", MM3.Spots.RaiseDead1),
                    new QuestLocation("Retrieve the scroll from the chest", MM3.Spots.RaiseDead2));
                AddSpellQuest(SpellDruidPrismaticLight, quests, MM3SpellIndex.PrismaticLight, 4);
                AddSpellQuest(SpellDruidElementalStorm, quests, MM3SpellIndex.ElementalStorm, 4);
            }

            quests.Sort(CompareMM3Quests);
            return quests.ToArray();
        }

        public int CompareMM3Quests(MM3Quest quest1, MM3Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress = -1)
        {
            MM3PartyInfo party = data.Party as MM3PartyInfo;
            MM3GameInfo info = data.Info as MM3GameInfo;

            MM3QuestData mm3Quest = data as MM3QuestData;
            if (mm3Quest == null)
                return;

            byte[] partyBits = mm3Quest.PartyBits;
            MM3FileQuestInfo fqi = mm3Quest.FileQuestInfo;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            Bits bits = new Bits(partyBits);

            MM3Character mm3Char = MM3Character.Create(party.Bytes, iOverrideCharAddress * MM3Character.SizeInBytes, info);

            if (mm3Char == null || mm3Char.Awards == null)
                return;

            CharName = mm3Char.CharName;
            CharAddress = iOverrideCharAddress;
            CharClass = mm3Char.BasicClass;
            QuestTotals totals = new QuestTotals(0, 0);

            bool bInPyramid = false;
            switch ((MM3Map) info.Map.CurrentMapIndex)
            {
                case MM3Map.A2AftStorageSector:
                case MM3Map.A2ForwardStorageSector:
                case MM3Map.C2CentralControlSector:
                case MM3Map.C2MainControlSector:
                case MM3Map.F1BetaEngineSector:
                case MM3Map.F2MainEngineSector:
                case MM3Map.F4AlphaEngineSector:
                    bInPyramid = true;
                    break;
            }

            FindFortressKeys.AddObj(fqi.Goal(FileB.YellowKey),
                fqi.Goal(FileB.GreenKey),
                fqi.Goal(FileB.BlueKey),
                fqi.Goal(FileB.RedKey),
                fqi.Goal(FileB.BlackKey),
                fqi.Goal(FileB.GoldKey));
            AddQuest(totals, FindFortressKeys);

            FindHologramCards.AddObj(fqi.Goal(FileB.Card2),
                fqi.Goal(FileB.Card3),
                bits.Set(PartyB.RiddleWeeds),
                fqi.Goal(FileB.Card5),
                fqi.Goal(FileB.Card6));
            AddQuest(totals, FindHologramCards);

            Orbs = new MM3QuestStates.Orbs();
            Orbs.OrbsInInventory = party.ItemCount(MM3ItemIndex.KingsUltimatePowerOrb);
            Orbs.DeliveredEvil = mm3Char.Awards.OrbsGivenToMalefactor;
            Orbs.DeliveredGood = mm3Char.Awards.OrbsGivenToZealot;
            Orbs.DeliveredNeutral = mm3Char.Awards.OrbsGivenToTumult;
            Orbs.HasPassCard = party.HasItem(MM3ItemIndex.BluePriorityPassCard);
            Orbs.Completed = (Orbs.HasPassCard || Orbs.DeliveredEvil > 10 || Orbs.DeliveredGood > 10 || Orbs.DeliveredNeutral > 10);

            DeliverPowerOrbs.AddObj(3, Goal(Orbs.Completed));
            fqi.AddObjRange(DeliverPowerOrbs, FileB.Orb1, FileB.Orb31);
            DeliverPowerOrbs.AddPost(Goal(Orbs.Completed));
            if (Orbs.Completed)
                DeliverPowerOrbs.Main = QuestStatus.Basic.Completed;
            AddQuest(totals, DeliverPowerOrbs);

            CorakSheltem.AddObj(party.HasItem(MM3ItemIndex.GoldenPyramidKeyCard),
                party.HasAward(MM3AwardIndex.UltimateAdventurer),
                bInPyramid);
            CorakSheltem.AddPost(false);
            AddQuest(totals, CorakSheltem);

            ReceiveBlessing.AddObj(mm3Char.Donations.HasFlag(MM3DonationFlags.FountainHead),
                mm3Char.Donations.HasFlag(MM3DonationFlags.Baywatch),
                mm3Char.Donations.HasFlag(MM3DonationFlags.Wildabar),
                mm3Char.Donations.HasFlag(MM3DonationFlags.SwampTown),
                mm3Char.Donations.HasFlag(MM3DonationFlags.BlisteringHeights));
            ReceiveBlessing.AddPost(fqi.Goal(FileB.Insect));
            AddQuest(totals, ReceiveBlessing);

            Pearls = new MM3QuestStates.Pearls();
            Pearls.PearlsInInventory = party.ItemCount(MM3ItemIndex.PreciousPearlofYouthandBeauty);
            Pearls.CurrentDay = info.Party.Day;
            Pearls.PearlsDelivered = mm3Char.Awards.PearlsToPirateQueen;

            fqi.AddObjRange(DeliverPearls, FileB.Pearl1, FileB.Pearl6);
            DeliverPearls.AddObj(false);    // Throne is repeatable every year
            AddQuest(totals, DeliverPearls);

            Skulls = new MM3QuestStates.Skulls();
            Skulls.SkullsInInventory = party.ItemCount(MM3ItemIndex.SacredSilverSkull);
            Skulls.SkullsDelivered = mm3Char.Awards.SkullsGivenToKranion;

            DeliverSkulls.AddPre(bits.Set(PartyB.AcceptedKranionQuest));
            DeliverSkulls.AddObj(mm3Char.Awards.SkullsGivenToKranion >= 20);
            fqi.AddObjRange(DeliverSkulls, FileB.Skull1, FileB.Skull20);
            AddQuest(totals, DeliverSkulls);

            MummyPuzzle.AddObj(bits.NotSet(PartyB.LeverB2FortressOfFear0230And0521),
                bits.NotSet(PartyB.LeverB2FortressOfFear1429And1822),
                bits.NotSet(PartyB.LeverB2FortressOfFear1009And0612),
                bits.NotSet(PartyB.LeverB2FortressOfFear1209And1512),
                bits.Set(PartyB.LeverB2FortressOfFear0729And1029),
                bits.Set(PartyB.LeverB2FortressOfFear2108And2430),
                bits.Set(PartyB.LeverB2FortressOfFear1108And2107),
                bits.Set(PartyB.LeverB2FortressOfFear0117And0118),
                fqi.Goal(FileB.Card1));
            AddQuest(totals, MummyPuzzle);

            Shells = new MM3QuestStates.Shells();
            Shells.ShellsInInventory = party.ItemCount(MM3ItemIndex.SeaShellofSerenity);
            Shells.ShellsDelivered = mm3Char.Awards.ShellsGivenToAthea;
            Shells.CurrentDay = info.Party.Day;

            DeliverShells.AddObj(info.Party.Day == 99,
                false,
                false);
            AddQuest(totals, DeliverShells);

            Arena = new MM3QuestStates.Arena();
            Arena.ArenaWins = mm3Char.Awards.ArenaWins;

            DefeatArena.AddObj(Arena.ArenaWins > 75);
            AddQuest(totals, DefeatArena);

            Trueberry = new MM3QuestStates.Trueberry();
            Trueberry.CharsInLove = party.CharsWithStatus(BasicConditionFlags.InLove);
            Trueberry.MetTrueberry = Global.IsBitSet(partyBits, PartyB.EncounterTrueberry);
            if (Global.IsBitSet(partyBits, PartyB.FreedTrueberry))
                Trueberry.LoveNeeded = 0;
            else if (Global.IsBitSet(partyBits, PartyB.NineLoveToTrueberry))
                Trueberry.LoveNeeded = 1;
            else if (Global.IsBitSet(partyBits, PartyB.EightLoveToTrueberry))
                Trueberry.LoveNeeded = 2;
            else if (Global.IsBitSet(partyBits, PartyB.SevenLoveToTrueberry))
                Trueberry.LoveNeeded = 3;
            else if (Global.IsBitSet(partyBits, PartyB.SixLoveToTrueberry))
                Trueberry.LoveNeeded = 4;
            else if (Global.IsBitSet(partyBits, PartyB.FiveLoveToTrueberry))
                Trueberry.LoveNeeded = 5;
            else if (Global.IsBitSet(partyBits, PartyB.FourLoveToTrueberry))
                Trueberry.LoveNeeded = 6;
            else if (Global.IsBitSet(partyBits, PartyB.ThreeLoveToTrueberry))
                Trueberry.LoveNeeded = 7;
            else if (Global.IsBitSet(partyBits, PartyB.TwoLoveToTrueberry))
                Trueberry.LoveNeeded = 8;
            else if (Global.IsBitSet(partyBits, PartyB.OneLoveToTrueberry))
                Trueberry.LoveNeeded = 9;
            else
                Trueberry.LoveNeeded = 10;

            bool bIcarus = mm3Char.Awards.IcarusResurrected > 0;
            bool bAlacorn = party.HasItem(MM3ItemIndex.AlacornofIcarus);
            ResurrectIcarus.AddPre(Global.IsBitSet(partyBits, PartyB.EncounterTrueberry) || bAlacorn || bIcarus);
            ResurrectIcarus.AddObj(bIcarus || party.CharsWithStatus(BasicConditionFlags.InLove) == 0,
                bIcarus || Trueberry.LoveNeeded == 0,
                bIcarus || Global.IsBitSet(partyBits, PartyB.EncounterIcarusBirds),
                bIcarus || bAlacorn);
            ResurrectIcarus.AddPost(mm3Char.Awards.IcarusResurrected > 0);
            AddQuest(totals, ResurrectIcarus);

            SaveFountainHead.AddPost(bits.Set(PartyB.RescuedMorphose));
            AddQuest(totals, SaveFountainHead);

            ReleaseBlackwind.AddPre(bits.Set(PartyB.EncounterBlackwind));
            ReleaseBlackwind.AddObj(bits.Set(PartyB.StatueBloodMane),
                bits.Set(PartyB.StatueHamonOthreute),
                bits.Set(PartyB.StatueTempestStorm));
            ReleaseBlackwind.AddPost(mm3Char.Awards.BlackwindReleased231 > 0);
            AddQuest(totals, ReleaseBlackwind);

            bool bReleasedGreywind = mm3Char.Awards.GreywindReleased645 > 0;
            ReleaseGreywind.AddPre(bReleasedGreywind || Global.IsBitSet(partyBits, PartyB.EnterCastleGreywind));
            ReleaseGreywind.AddPre(fqi.Goal(FileB.GreyAccept));
            QuestGoal sandUpNW = bits.NotSet(PartyB.HourglassC4GreywindDungeon0114);
            QuestGoal sandUpNE = bits.Set(PartyB.HourglassC4GreywindDungeon1414);
            QuestGoal sandUpSW = bits.Set(PartyB.HourglassC4GreywindDungeon0101);
            QuestGoal sandUpSE = bits.Set(PartyB.HourglassC4GreywindDungeon1401);
            QuestGoal greywindWall = fqi.Goal(FileB.GreyWall, 0x70);
            ReleaseGreywind.AddObj(QuestStatus.Or(greywindWall, sandUpNE),
                QuestStatus.Or(greywindWall, sandUpSW),
                QuestStatus.Or(greywindWall, sandUpSE),
                greywindWall,
                QuestStatus.AndNot(greywindWall, sandUpNW),
                QuestStatus.AndNot(greywindWall, sandUpNE),
                QuestStatus.AndNot(greywindWall, sandUpSW),
                QuestStatus.AndNot(greywindWall, sandUpSE),
                bits.Set(PartyB.GreywindTasksCompleted));
            ReleaseGreywind.AddPost(bReleasedGreywind);
            AddQuest(totals, ReleaseGreywind);

            Artifacts = new MM3QuestStates.Artifacts();
            Artifacts.EvilDelivered = mm3Char.Awards.EvilArtifactsRecovered;
            Artifacts.NeutralDelivered = mm3Char.Awards.NeutralArtifactsRecovered;
            Artifacts.GoodDelivered = mm3Char.Awards.GoodArtifactsRecovered;
            Artifacts.EvilInInventory = party.ItemCount(MM3ItemIndex.AncientArtifactofEvil);
            Artifacts.NeutralInInventory = party.ItemCount(MM3ItemIndex.AncientArtifactofNeutrality);
            Artifacts.GoodInInventory = party.ItemCount(MM3ItemIndex.AncientArtifactofGood);

            RecoverArtifacts.AddObj(mm3Char.Awards.GoodArtifactsRecovered >= 11,
                mm3Char.Awards.NeutralArtifactsRecovered >= 11,
                mm3Char.Awards.EvilArtifactsRecovered >= 15);
            RecoverArtifacts.AddObj(bits.Set(PartyB.EncounterPraythos),
                bits.Set(PartyB.EncounterChathos),
                bits.Set(PartyB.EncounterPathos));
            fqi.AddObjRange(RecoverArtifacts, FileB.Evil1, FileB.Neut11);
            AddQuest(totals, RecoverArtifacts);

            Brothers = new MM3QuestStates.GreekBrothers();
            Brothers.CoinsInInventory = party.ItemCount(MM3ItemIndex.QuatlooCoin);

            VisitBrothers.AddPre(bits.Set(PartyB.VisitedBrotherAlpha));
            VisitBrothers.AddObj(bits.Set(PartyB.VisitedBrotherBeta),
                bits.Set(PartyB.VisitedBrotherGamma),
                bits.Set(PartyB.VisitedBrotherDelta));
            VisitBrothers.AddPost(false);
            AddQuest(totals, VisitBrothers);

            fqi.AddObjRange(DestroyLairs, FileB.Destroy1, FileB.Destroy27);
            AddQuest(totals, DestroyLairs);

            UseWell.AddObj(bits.Set(PartyB.RememberedObeyer),
                bits.Set(PartyB.RememberedSlayer),
                bits.Set(PartyB.RememberedSoothsayer),
                bits.Set(PartyB.RememberedPurveyor));
            UseWell.AddPost(fqi.Goal(FileB.Well));
            AddQuest(totals, UseWell);

            ReleaseOrbs.AddObj(bits.Set(PartyB.ProtoNorth),
                bits.Set(PartyB.BarytroWest),
                bits.Set(PartyB.DynatroNorth),
                bits.Set(PartyB.PenetroEast),
                bits.Set(PartyB.PositroSouth),
                bits.Set(PartyB.RiddleWeeds),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage1016),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage1014),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage0716),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage0714),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage0416),
                bits.Set(PartyB.ChaliceB3CathedralOfCarnage0414));
            ReleaseOrbs.AddPost(bits.Set(PartyB.SolvedCathedralOfCarnage));
            AddQuest(totals, ReleaseOrbs);

            UseCrystals.AddObj(bits.Set(PartyB.CrystalB4ArachnoidCavern1110),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1324),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1413),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1707),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2318),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2020),
                bits.Set(PartyB.CrystalB4ArachnoidCavern0818),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1308),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1417),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1611),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1618),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1818),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2016),
                fqi.Goal(FileB.LordMight));
            UseCrystals.AddPost(false);
            AddQuest(totals, UseCrystals);

            RaiseIsland.AddObj(fqi.Goal(FileB.KeyCard));
            RaiseIsland.AddPost(bits.Set(PartyB.RaiseSunkenIsle));
            AddQuest(totals, RaiseIsland);

            LearnSkills.AddObj(mm3Char.Skills.Thievery > 0,
                mm3Char.Skills.ArmsMaster > 0,
                mm3Char.Skills.Astrologer > 0,
                mm3Char.Skills.BodyBuilder > 0,
                mm3Char.Skills.Cartographer > 0,
                mm3Char.Skills.Crusader > 0,
                mm3Char.Skills.DirectionSense > 0,
                mm3Char.Skills.Linguist > 0,
                mm3Char.Skills.Merchant > 0,
                mm3Char.Skills.Mountaineer > 0,
                mm3Char.Skills.Navigator > 0,
                mm3Char.Skills.PathFinder > 0,
                mm3Char.Skills.PrayerMaster > 0,
                mm3Char.Skills.Prestidigitator > 0,
                mm3Char.Skills.Swimmer > 0,
                mm3Char.Skills.Tracker > 0,
                mm3Char.Skills.SpotSecretDoors > 0,
                mm3Char.Skills.DangerSense > 0);
            AddQuest(totals, LearnSkills);

            Level6Items.AddObj(false, false);
            fqi.AddObjRange(Level6Items, FileB.L6Items1, FileB.L6Items41);
            AddQuest(totals, Level6Items);

            TempStats.AddObj(mm3Char.ACModifier > 0,
                mm3Char.Personality.Modifier > 0,
                mm3Char.Intellect.Modifier > 0,
                mm3Char.MagicResist.Modifier > 0,
                mm3Char.PoisonResist.Modifier > 0,
                mm3Char.Might.Modifier > 0,
                mm3Char.Speed.Modifier > 0,
                mm3Char.ColdResist.Modifier > 0,
                mm3Char.Level.Modifier > 0,
                mm3Char.FireResist.Modifier > 0,
                mm3Char.ACModifier > 0,
                mm3Char.Accuracy.Modifier > 0,
                mm3Char.Endurance.Modifier > 0,
                mm3Char.AllTempStats(0),
                mm3Char.AllTempRes(0),
                mm3Char.Endurance.Modifier > 0,
                mm3Char.Speed.Modifier > 0,
                mm3Char.Accuracy.Modifier > 0,
                mm3Char.Might.Modifier > 0,
                mm3Char.Level.Modifier > 200,
                mm3Char.Luck.Modifier > 0,
                mm3Char.ColdResist.Modifier > 0,
                mm3Char.ElecResist.Modifier > 0,
                mm3Char.FireResist.Modifier > 0,
                mm3Char.PoisonResist.Modifier > 0);
            AddQuest(totals, TempStats);

            fqi.AddObjRange(PermStats, FileB.Perm1, FileB.Perm7);
            PermStats.AddObj(false);        // Day 50 throne
            fqi.AddObjRange(PermStats, FileB.Perm8, FileB.Perm19);
            PermStats.AddObj(3, Goal(!party.HasItem(MM3ItemIndex.QuatlooCoin)));
            fqi.AddObjRange(PermStats, FileB.Perm20, FileB.Perm31);
            PermStats.AddObj(bits.Set(PartyB.CrystalB4ArachnoidCavern1324),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2020),
                bits.Set(PartyB.CrystalB4ArachnoidCavern0818),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1618),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1818),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2318),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1417),
                bits.Set(PartyB.CrystalB4ArachnoidCavern2016),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1413),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1611),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1110),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1308),
                bits.Set(PartyB.CrystalB4ArachnoidCavern1707));
            fqi.AddObjRange(PermStats, FileB.Perm32, FileB.Perm60);
            PermStats.Obj.Add(GetSingle(true, mm3Char.Endurance.Permanent > 24 && mm3Char.Might.Permanent > 24, mm3Char.Class == MM345Class.Knight, "You are not a Knight"));
            PermStats.AddObj(mm3Char.Accuracy.Permanent > 24, mm3Char.Endurance.Permanent > 49);
            PermStats.Obj.Add(GetSingle(true, mm3Char.AllPermStats(25), mm3Char.Class == MM345Class.Druid, "You are not a Druid"));
            PermStats.AddObj(mm3Char.PoisonResist.Permanent > 253);    // Can wrap around to 0 but 254/255 is more or less "completed" for this
            PermStats.AddObj(mm3Char.Might.Permanent > 254);
            fqi.AddObjRange(PermStats, FileB.Perm61, FileB.Perm78);
            PermStats.AddObj(mm3Char.Might.Permanent > 49);
            PermStats.AddObj(mm3Char.Endurance.Permanent > 49);
            PermStats.AddObj(mm3Char.FireResist.Permanent > 24);
            PermStats.AddObj(mm3Char.ElecResist.Permanent > 24);
            PermStats.AddObj(mm3Char.ColdResist.Permanent > 24);
            PermStats.AddObj(mm3Char.PoisonResist.Permanent > 24);
            PermStats.AddObj(mm3Char.EnergyResist.Permanent > 24);
            PermStats.AddObj(mm3Char.MagicResist.Permanent > 24);
            AddQuest(totals, PermStats);

            fqi.AddObjRange(Hirelings, FileB.Hireling1, FileB.Hireling6);
            AddQuest(totals, Hirelings);

            JoinGuilds.AddObj(mm3Char.Awards.RavensGuildMember > 0,
                mm3Char.Awards.AlbatrossGuildMember > 0,
                mm3Char.Awards.FalconsGuildMember > 0,
                mm3Char.Awards.BuzzardsGuildMember > 0,
                mm3Char.Awards.EaglesGuildMember > 0);
            AddQuest(totals, JoinGuilds);

            fqi.AddObjRange(FindJewelry, FileB.Jewelry1, FileB.Jewelry12);
            AddQuest(totals, FindJewelry);

            SpellClericLight = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LightCleric);
            SpellClericAwaken = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AwakenCleric);
            SpellClericFirstAid = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FirstAid);
            SpellClericFlyingFist = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FlyingFist);
            SpellClericRevitalize = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Revitalize);
            SpellClericCureWounds = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CureWounds);
            SpellClericSparks = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Sparks);
            SpellClericProtectionfromElements = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ProtectionFromElements);
            SpellClericPain = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Pain);
            SpellClericSuppressPoison = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SuppressPoison);
            SpellClericSuppressDisease = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SuppressDisease);
            SpellClericTurnUndead = AddSpellQuest(totals, mm3Char, MM3SpellIndex.TurnUndead);
            SpellClericSilence = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Silence);
            SpellClericBlessed = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Blessed);
            SpellClericHolyBonus = AddSpellQuest(totals, mm3Char, MM3SpellIndex.HolyBonus);
            SpellClericPowerCure = AddSpellQuest(totals, mm3Char, MM3SpellIndex.PowerCure);
            SpellClericHeroism = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Heroism);
            SpellClericImmobilize = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Immobilize);
            SpellClericColdRay = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ColdRay);
            SpellClericCurePoison = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CurePoison);
            SpellClericAcidSpray = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AcidSpray);
            SpellClericCureDisease = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CureDisease);
            SpellClericCureParalysis = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CureParalysis);
            SpellClericParalyze = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Paralyze);
            SpellClericCreateFood = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CreateFood);
            SpellClericFieryFlail = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FieryFlail);
            SpellClericTownPortal = AddSpellQuest(totals, mm3Char, MM3SpellIndex.TownPortal);
            SpellClericStonetoFlesh = AddSpellQuest(totals, mm3Char, MM3SpellIndex.StoneToFlesh);
            SpellClericHalfforMe = AddSpellQuest(totals, mm3Char, MM3SpellIndex.HalfForMe);
            SpellClericRaiseDead = AddSpellQuest(totals, mm3Char, MM3SpellIndex.RaiseDead);
            SpellClericMoonRay = AddSpellQuest(totals, mm3Char, MM3SpellIndex.MoonRay);
            SpellClericMassDistortion = AddSpellQuest(totals, mm3Char, MM3SpellIndex.MassDistortion);
            SpellClericHolyWord = AddSpellQuest(totals, mm3Char, MM3SpellIndex.HolyWord);
            SpellClericResurrect = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Resurrection);
            SpellClericSunRay = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SunRay);
            SpellClericDivineIntervention = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DivineIntervention);
            SpellSorcererLight = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LightSorcerer);
            SpellSorcererAwaken = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AwakenSorcerer);
            SpellSorcererDetectMagic = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DetectMagic);
            SpellSorcererElementalArrow = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ElementalArrow);
            SpellSorcererEnergyBlast = AddSpellQuest(totals, mm3Char, MM3SpellIndex.EnergyBlast);
            SpellSorcererSleep = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Sleep);
            SpellSorcererCreateRope = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CreateRope);
            SpellSorcererToxicCloud = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ToxicCloud);
            SpellSorcererJump = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Jump);
            SpellSorcererAcidStream = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AcidStream);
            SpellSorcererLevitate = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Levitate);
            SpellSorcererWizardEye = AddSpellQuest(totals, mm3Char, MM3SpellIndex.WizardEye);
            SpellSorcererIdentifyMonster = AddSpellQuest(totals, mm3Char, MM3SpellIndex.IdentifyMonster);
            SpellSorcererLightningBolt = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LightningBolt);
            SpellSorcererLloydsBeacon = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LloydsBeacon);
            SpellSorcererPowerShield = AddSpellQuest(totals, mm3Char, MM3SpellIndex.PowerShield);
            SpellSorcererDetectMonster = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DetectMonster);
            SpellSorcererFireball = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Fireball);
            SpellSorcererTimeDistortion = AddSpellQuest(totals, mm3Char, MM3SpellIndex.TimeDistortion);
            SpellSorcererFeebleMind = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FeebleMind);
            SpellSorcererTeleport = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Teleport);
            SpellSorcererFingerofDeath = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FingerOfDeath);
            SpellSorcererSuperShelter = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SuperShelter);
            SpellSorcererDragonBreath = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DragonBreath);
            SpellSorcererRechargeItem = AddSpellQuest(totals, mm3Char, MM3SpellIndex.RechargeItem);
            SpellSorcererFantasticFreeze = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FantasticFreeze);
            SpellSorcererDuplication = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Duplication);
            SpellSorcererDisintegrate = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Disintegration);
            SpellSorcererEtherealize = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Etherealize);
            SpellSorcererDancingSword = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DancingSword);
            SpellSorcererEnchantItem = AddSpellQuest(totals, mm3Char, MM3SpellIndex.EnchantItem);
            SpellSorcererIncinerate = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Incinerate);
            SpellSorcererMegaVolts = AddSpellQuest(totals, mm3Char, MM3SpellIndex.MegaVolts);
            SpellSorcererInferno = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Inferno);
            SpellSorcererImplosion = AddSpellQuest(totals, mm3Char, MM3SpellIndex.Implosion);
            SpellSorcererStarBurst = AddSpellQuest(totals, mm3Char, MM3SpellIndex.StarBurst);
            SpellDruidLight = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LightDruid);
            SpellDruidAwaken = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AwakenDruid);
            SpellDruidFirstAid = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FirstAidDruid);
            SpellDruidDetectMagic = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DetectMagicDruid);
            SpellDruidElementalArrow = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ElementalArrowDruid);
            SpellDruidRevitalize = AddSpellQuest(totals, mm3Char, MM3SpellIndex.RevitalizeDruid);
            SpellDruidCreateRope = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CreateRopeDruid);
            SpellDruidSleep = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SleepDruid);
            SpellDruidProtectionfromElements = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ProtectionFromElementsDruid);
            SpellDruidSuppressPoison = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SuppressPoisonDruid);
            SpellDruidSuppressDisease = AddSpellQuest(totals, mm3Char, MM3SpellIndex.SuppressDiseaseDruid);
            SpellDruidIdentifyMonster = AddSpellQuest(totals, mm3Char, MM3SpellIndex.IdentifyMonsterDruid);
            SpellDruidNaturesCure = AddSpellQuest(totals, mm3Char, MM3SpellIndex.NaturesCure);
            SpellDruidImmobilize = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ImmobilizeDruid);
            SpellDruidWalkonWater = AddSpellQuest(totals, mm3Char, MM3SpellIndex.WalkOnWater);
            SpellDruidFrostBite = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FrostBite);
            SpellDruidLightningBolt = AddSpellQuest(totals, mm3Char, MM3SpellIndex.LightningBoltDruid);
            SpellDruidAcidSpray = AddSpellQuest(totals, mm3Char, MM3SpellIndex.AcidSprayDruid);
            SpellDruidColdRay = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ColdRayDruid);
            SpellDruidNaturesGate = AddSpellQuest(totals, mm3Char, MM3SpellIndex.NaturesGate);
            SpellDruidFireball = AddSpellQuest(totals, mm3Char, MM3SpellIndex.FireballDruid);
            SpellDruidDeadlySwarm = AddSpellQuest(totals, mm3Char, MM3SpellIndex.DeadlySwarm);
            SpellDruidCureParalysis = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CureParalysisDruid);
            SpellDruidParalyze = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ParalyzeDruid);
            SpellDruidCreateFood = AddSpellQuest(totals, mm3Char, MM3SpellIndex.CreateFoodDruid);
            SpellDruidStonetoFlesh = AddSpellQuest(totals, mm3Char, MM3SpellIndex.StoneToFleshDruid);
            SpellDruidRaiseDead = AddSpellQuest(totals, mm3Char, MM3SpellIndex.RaiseDeadDruid);
            SpellDruidPrismaticLight = AddSpellQuest(totals, mm3Char, MM3SpellIndex.PrismaticLight);
            SpellDruidElementalStorm = AddSpellQuest(totals, mm3Char, MM3SpellIndex.ElementalStorm);

            CompletedQuests = totals.Completed;
            TotalQuests = totals.All;
        }
    }
}