using System.Drawing;
namespace WhereAreWe
{
    // These lists of quest-related locations keeps the GetQuests() functions from becoming 
    // even more ungainly than they are already.

    public class Wiz1Locations
    {
        // Wizardry 1

        public MapXY SilverKey { get { return new MapXY(Wiz1Map.MazeLevel1, 13, 18); } }
        public MapXY BronzeKey { get { return new MapXY(Wiz1Map.MazeLevel1, 13, 3); } }
        public MapXY MurphysGhost { get { return new MapXY(Wiz1Map.MazeLevel1, 13, 5); } }
        public MapXY GoldKey { get { return new MapXY(Wiz1Map.MazeLevel2, 4, 16); } }
        public MapXY BearDoor { get { return new MapXY(Wiz1Map.MazeLevel2, 4, 11); } }
        public MapXY FrogDoor { get { return new MapXY(Wiz1Map.MazeLevel2, 4, 12); } }
        public MapXY SilverFog { get { return new MapXY(Wiz1Map.MazeLevel2, 8, 12); } }
        public MapXY BronzeFog { get { return new MapXY(Wiz1Map.MazeLevel2, 8, 7); } }
        public MapXY FrogStatue { get { return new MapXY(Wiz1Map.MazeLevel2, 12, 4); } }
        public MapXY BearStatue { get { return new MapXY(Wiz1Map.MazeLevel2, 9, 18); } }
        public MapXY BlueRibbon { get { return new MapXY(Wiz1Map.MazeLevel4, 11, 10); } }
        public MapXY FireDragon { get { return new MapXY(Wiz1Map.MazeLevel7, 2, 9); } }
        public MapXY Chute { get { return new MapXY(Wiz1Map.MazeLevel9, 8, 2); } }

        public MapXY Guardian1 { get { return new MapXY(Wiz1Map.MazeLevel10, 1, 8); } }
        public MapXY Guardian2 { get { return new MapXY(Wiz1Map.MazeLevel10, 1, 18); } }
        public MapXY Guardian3 { get { return new MapXY(Wiz1Map.MazeLevel10, 10, 18); } }
        public MapXY Guardian4 { get { return new MapXY(Wiz1Map.MazeLevel10, 9, 12); } }
        public MapXY Guardian5 { get { return new MapXY(Wiz1Map.MazeLevel10, 18, 13); } }
        public MapXY Guardian6 { get { return new MapXY(Wiz1Map.MazeLevel10, 16, 10); } }
        public MapXY Werdna { get { return new MapXY(Wiz1Map.MazeLevel10, 17, 3); } }

        public MapXY GuardianTeleport1 { get { return new MapXY(Wiz1Map.MazeLevel10, 0, 10); } }
        public MapXY GuardianTeleport2 { get { return new MapXY(Wiz1Map.MazeLevel10, 0, 19); } }
        public MapXY GuardianTeleport3 { get { return new MapXY(Wiz1Map.MazeLevel10, 10, 19); } }
        public MapXY GuardianTeleport4 { get { return new MapXY(Wiz1Map.MazeLevel10, 10, 11); } }
        public MapXY GuardianTeleport5 { get { return new MapXY(Wiz1Map.MazeLevel10, 19, 13); } }
        public MapXY GuardianTeleport6 { get { return new MapXY(Wiz1Map.MazeLevel10, 14, 11); } }

        public Wiz1Locations()
        {
        }
    }

    public class Wiz2Locations
    {
        // Wizardry 2

        public MapXY None = new MapXY(GameNames.Wizardry2, -1, 0, 0, -1);
        public MapXY Castle = new MapXY(Wiz2Map.Castle, 0, 0);
        public MapXY Apparition1 = new MapXY(Wiz2Map.MazeLevel1, 9, 10);
        public MapXY MagicArmor = new MapXY(Wiz2Map.MazeLevel1, 15, 2);
        public MapXY RiddleShield = new MapXY(Wiz2Map.MazeLevel2, 3, 6);
        public MapXY MagicShield = new MapXY(Wiz2Map.MazeLevel2, 2, 6);
        public MapXY SagePay100000 = new MapXY(Wiz2Map.MazeLevel2, 18, 10);
        public MapXY SageRevealInfo = new MapXY(Wiz2Map.MazeLevel2, 18, 9);
        public MapXY Hrathnir = new MapXY(Wiz2Map.MazeLevel3, 14, 13);
        public MapXY MagicHelm = new MapXY(Wiz2Map.MazeLevel4, 10, 4);
        public MapXY BlueFountain = new MapXY(Wiz2Map.MazeLevel4, 16, 7);
        public MapXY MagicGauntlets = new MapXY(Wiz2Map.MazeLevel5, 10, 9);
        public MapXY RiddleTKOD = new MapXY(Wiz2Map.MazeLevel6, 1, 2);
        public MapXY Apparition2 = new MapXY(Wiz2Map.MazeLevel6, 1, 3);
        public MapXY StaffOfLight = new MapXY(Wiz2Map.MazeLevel6, 5, 14);

        public Wiz2Locations()
        {
        }
    }

    public class Wiz3Locations
    {
        // Wizardry 3

        public MapXY None = new MapXY(GameNames.Wizardry3, -1, 0, 0, -1);
        public MapXY Castle = new MapXY(Wiz3Map.Castle, 0, 0);
        public MapXY StairsTo2 = new MapXY(Wiz3Map.TowerLevel1, 19, 13);
        public MapXY StairsTo3 = new MapXY(Wiz3Map.TowerLevel1, 19, 14);
        public MapXY StairsTo4 = new MapXY(Wiz3Map.TowerLevel2, 0, 19);
        public MapXY StairsTo5 = new MapXY(Wiz3Map.TowerLevel3, 0, 1);
        public MapXY RiddleAir = new MapXY(Wiz3Map.TowerLevel2, 4, 19);
        public MapXY AmuletOfAir = new MapXY(Wiz3Map.TowerLevel2, 11, 19);
        public MapXY UsePassword = new MapXY(Wiz3Map.TowerLevel2, 7, 11);
        public MapXY StaffOfEarth = new MapXY(Wiz3Map.TowerLevel2, 7, 1);
        public MapXY HolyWater = new MapXY(Wiz3Map.TowerLevel3, 10, 19);
        public MapXY Medallion = new MapXY(Wiz3Map.TowerLevel3, 7, 7);
        public MapXY CrystalOfEvil = new MapXY(Wiz3Map.TowerLevel4, 7, 17);
        public MapXY LearnPassword = new MapXY(Wiz3Map.TowerLevel4, 12, 12);
        public MapXY Wade = new MapXY(Wiz3Map.TowerLevel4, 10, 5);
        public MapXY RiddleFire = new MapXY(Wiz3Map.TowerLevel4, 18, 11);
        public MapXY RiddleChariot = new MapXY(Wiz3Map.TowerLevel5, 5, 13);
        public MapXY PayRod = new MapXY(Wiz3Map.TowerLevel5, 4, 8);
        public MapXY RodOfFire = new MapXY(Wiz3Map.TowerLevel5, 4, 7);
        public MapXY CrystalOfGood = new MapXY(Wiz3Map.TowerLevel5, 12, 10);
        public MapXY LKBreth = new MapXY(Wiz3Map.TowerLevel6, 10, 4);
        public MapXY RiddleWheel = new MapXY(Wiz3Map.TowerLevel6, 11, 4);
        public MapXY Xeno = new MapXY(Wiz3Map.TowerLevel6, 4, 4);
        public MapXY OrbEarithin = new MapXY(Wiz3Map.TowerLevel6, 17, 17);
        public MapXY OrbMhuuzfes = new MapXY(Wiz3Map.TowerLevel6, 13, 2);
        public MapXY Fiend = new MapXY(Wiz3Map.TowerLevel2, 3, 1);

        public Wiz3Locations()
        {
        }
    }

    public class Wiz4Locations
    {
        // Wizardry 4

        public MapXY None = new MapXY(GameNames.Wizardry4, -1, 0, 0, -1);
        public MapXY StartingPosition = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 10, 9);
        public MapXY CellDoor = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 9, 10);
        public MapXY Pentagram10A = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 10, 10);
        public MapXY Pentagram10B = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 2, 6);
        public MapXY InnerGuardian = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 9, 5);
        public MapXY MiddleGuardian = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 3, 9);
        public MapXY OuterGuardian = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 18, 9);
        public MapXY Bloodstone = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 9, 19);
        public MapXY PyramidGuard = new MapXY(Wiz4Map.L10PyramidOfEntrapment, 11, 19);
        public MapXY Pentagram09 = new MapXY(Wiz4Map.L9TheCatacombs, 0, 6);
        public MapXY LandersTurquoise = new MapXY(Wiz4Map.L9TheCatacombs, 18, 0);
        public MapXY GatesOfHell = new MapXY(Wiz4Map.L9TheCatacombs, 9, 10);
        public MapXY SecretPassage = new MapXY(Wiz4Map.L9TheCatacombs, 13, 15);
        public MapXY Pentagram08 = new MapXY(Wiz4Map.L8LandOf1000Cuts, 18, 15);
        public MapXY GoldenPyrite = new MapXY(Wiz4Map.L8LandOf1000Cuts, 0, 9);
        public MapXY AmberDragon = new MapXY(Wiz4Map.L8LandOf1000Cuts, 0, 5);
        public MapXY WitchingRod = new MapXY(Wiz4Map.L8LandOf1000Cuts, 14, 1);
        public MapXY BlackBox = new MapXY(Wiz4Map.L8LandOf1000Cuts, 1, 1);
        public MapXY Pentagram07 = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 4, 1);
        public MapXY HopOverWall = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 5, 8);
        public MapXY Dreampainter = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 7, 8);
        public MapXY HopalongCarrot = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 6, 18);
        public MapXY Blimp = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 17, 14);
        public MapXY Altar = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, 9, 19);
        public MapXY Pentagram06A = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 10, 18);
        public MapXY Pentagram06B = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 18, 13);
        public MapXY Pentagram06C = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 1, 7);
        public MapXY GoodHopeCape = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 13, 13);
        public MapXY JesseTheSmith = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 13, 2);
        public MapXY WingedBoots1 = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 7, 1);
        public MapXY WingedBoots2 = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 0, 17);
        public MapXY OxygenMask = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 9, 18);
        public MapXY Pentagram05 = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 5, 5);
        public MapXY Teleport05 = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 13, 8);
        public MapXY NeutralPool = new MapXY(Wiz4Map.L4MazeOfWandering, 6, 5);
        public MapXY Pentagram04A = new MapXY(Wiz4Map.L4MazeOfWandering, 16, 16);
        public MapXY Pentagram04B = new MapXY(Wiz4Map.L4MazeOfWandering, 1, 7);
        public MapXY Pentagram04C = new MapXY(Wiz4Map.L4MazeOfWandering, 12, 0);
        public MapXY Witch = new MapXY(Wiz4Map.L4MazeOfWandering, 5, 14);
        public MapXY AromaticBall = new MapXY(Wiz4Map.L4MazeOfWandering, 14, 9);
        public MapXY CleansingOil = new MapXY(Wiz4Map.L2CosmicCube, 3, 14);
        public MapXY Pentagram03A = new MapXY(Wiz4Map.L3CosmicCube, 17, 19);
        public MapXY Pentagram03B = new MapXY(Wiz4Map.L3CosmicCube, 5, 14);
        public MapXY Pentagram03C = new MapXY(Wiz4Map.L3CosmicCube, 16, 13);
        public MapXY Pentagram03D = new MapXY(Wiz4Map.L3CosmicCube, 2, 7);
        public MapXY Pentagram03E = new MapXY(Wiz4Map.L3CosmicCube, 14, 4);
        public MapXY Pentagram03F = new MapXY(Wiz4Map.L3CosmicCube, 17, 4);
        public MapXY Pentagram02A = new MapXY(Wiz4Map.L2CosmicCube, 14, 18);
        public MapXY Pentagram02B = new MapXY(Wiz4Map.L2CosmicCube, 6, 13);
        public MapXY Pentagram02C = new MapXY(Wiz4Map.L2CosmicCube, 8, 9);
        public MapXY Pentagram02D = new MapXY(Wiz4Map.L2CosmicCube, 0, 4);
        public MapXY Pentagram02E = new MapXY(Wiz4Map.L2CosmicCube, 16, 4);
        public MapXY Pentagram02F = new MapXY(Wiz4Map.L2CosmicCube, 2, 0);
        public MapXY Pentagram01A = new MapXY(Wiz4Map.L1CosmicCube, 1, 14);
        public MapXY Pentagram01B = new MapXY(Wiz4Map.L1CosmicCube, 16, 14);
        public MapXY Pentagram01C = new MapXY(Wiz4Map.L1CosmicCube, 5, 11);
        public MapXY Pentagram01D = new MapXY(Wiz4Map.L1CosmicCube, 8, 9);
        public MapXY Pentagram01E = new MapXY(Wiz4Map.L1CosmicCube, 2, 3);
        public MapXY Pentagram01F = new MapXY(Wiz4Map.L1CosmicCube, 16, 3);
        public MapXY Pentagram01G = new MapXY(Wiz4Map.L1CosmicCube, 19, 4);
        public MapXY VonHalsternChivalry = new MapXY(Wiz4Map.CastleFirstFloor, 13, 9);
        public MapXY Boltac = new MapXY(Wiz4Map.CastleFirstFloor, 12, 4);
        public MapXY Training1 = new MapXY(Wiz4Map.CastleFirstFloor, 9, 3);
        public MapXY Training2 = new MapXY(Wiz4Map.CastleFirstFloor, 9, 5);
        public MapXY Training3 = new MapXY(Wiz4Map.CastleFirstFloor, 9, 7);
        public MapXY Inn = new MapXY(Wiz4Map.CastleFirstFloor, 16, 9);
        public MapXY EvilFountain = new MapXY(Wiz4Map.CastleFirstFloor, 13, 16);
        public MapXY GoodFountain = new MapXY(Wiz4Map.CastleFirstFloor, 5, 16);
        public MapXY Elevator1A = new MapXY(Wiz4Map.CastleFirstFloor, 12, 9);
        public MapXY Elevator1B = new MapXY(Wiz4Map.CastleSecondFloor, 12, 9);
        public MapXY Elevator2B = new MapXY(Wiz4Map.CastleSecondFloor, 7, 9);
        public MapXY Elevator2C = new MapXY(Wiz4Map.CastleThirdFloor, 7, 9);
        public MapXY CaptainsCouncil = new MapXY(Wiz4Map.CastleFirstFloor, 9, 16);
        public MapXY SoftalkAllStars = new MapXY(Wiz4Map.CastleSecondFloor, 8, 9);
        public MapXY CouncilOfBarons = new MapXY(Wiz4Map.CastleSecondFloor, 9, 16);
        public MapXY TygersCubs = new MapXY(Wiz4Map.CastleSecondFloor, 14, 13);
        public MapXY OrderOfThePelican = new MapXY(Wiz4Map.CastleSecondFloor, 15, 13);
        public MapXY OrderOfTheLaurel = new MapXY(Wiz4Map.CastleSecondFloor, 14, 11);
        public MapXY LadiesOfTheRose = new MapXY(Wiz4Map.CastleSecondFloor, 15, 11);
        public MapXY MythrilGauntlets = new MapXY(Wiz4Map.CastleThirdFloor, 9, 17);
        public MapXY Innkeyper = new MapXY(Wiz4Map.CastleThirdFloor, 17, 17);
        public MapXY PackratNest = new MapXY(Wiz4Map.CastleThirdFloor, 11, 9);
        public MapXY Kadorto = new MapXY(Wiz4Map.CastleThirdFloor, 6, 13);
        public MapXY Hawkwind = new MapXY(Wiz4Map.CastleThirdFloor, 4, 12);
        public MapXY TemplePriests = new MapXY(Wiz4Map.CastleThirdFloor, 5, 11);
        public MapXY DukesCouncil = new MapXY(Wiz4Map.CastleThirdFloor, 9, 15);
        public MapXY KrisOfTruth = new MapXY(Wiz4Map.L11Grandmaster, 10, 14);
        public MapXY TreborRump = new MapXY(Wiz4Map.L1CosmicCube, 13, 2);
        public MapXY Hellhound = new MapXY(Wiz4Map.L9TheCatacombs, 9, 8);
        public MapXY WalkingWounded = new MapXY(Wiz4Map.CastleFirstFloor, 16, 11);
        public MapXY Level6StairsUp = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, 17, 4);
        public MapXY Level5TeleporterOut = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 13, 8);
        public MapXY Level5StairsUp = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, 5, 17);
        public MapXY Cube01Start = new MapXY(Wiz4Map.L1CosmicCube, 15, 11);
        public MapXY Cube02Teleport = new MapXY(Wiz4Map.L1CosmicCube, 15, 14);
        public MapXY Cube03StairsUp = new MapXY(Wiz4Map.L3CosmicCube, 18, 17);
        public MapXY Cube04ChuteDown = new MapXY(Wiz4Map.L1CosmicCube, 15, 17);
        public MapXY Cube05ChuteUp = new MapXY(Wiz4Map.L2CosmicCube, 11, 15);
        public MapXY Cube06ChuteDown = new MapXY(Wiz4Map.L1CosmicCube, 3, 12);
        public MapXY Cube07ChuteUp = new MapXY(Wiz4Map.L3CosmicCube, 14, 13);
        public MapXY Cube08Teleport = new MapXY(Wiz4Map.L2CosmicCube, 1, 18);
        public MapXY Cube09Stairs = new MapXY(Wiz4Map.L2CosmicCube, 10, 9);
        public MapXY Cube10ChuteUp = new MapXY(Wiz4Map.L2CosmicCube, 6, 16);
        public MapXY Cube11ChuteDown = new MapXY(Wiz4Map.L1CosmicCube, 11, 15);
        public MapXY Cube12ChuteUp = new MapXY(Wiz4Map.L3CosmicCube, 12, 11);
        public MapXY Cube13ChuteUp = new MapXY(Wiz4Map.L2CosmicCube, 14, 8);
        public MapXY Cube14StairsDown = new MapXY(Wiz4Map.L1CosmicCube, 12, 7);
        public MapXY Cube15ChuteUp = new MapXY(Wiz4Map.L3CosmicCube, 16, 1);
        public MapXY Cube16Chute = new MapXY(Wiz4Map.L2CosmicCube, 5, 7);
        public MapXY Cube17StairsDown = new MapXY(Wiz4Map.L2CosmicCube, 10, 8);
        public MapXY Cube18Teleport = new MapXY(Wiz4Map.L3CosmicCube, 8, 17);
        public MapXY Cube19Teleport = new MapXY(Wiz4Map.L3CosmicCube, 1, 16);
        public MapXY Cube20Teleport = new MapXY(Wiz4Map.L3CosmicCube, 9, 12);
        public MapXY Cube21StairsUp = new MapXY(Wiz4Map.L3CosmicCube, 19, 17);
        public MapXY Cube22ChuteDown = new MapXY(Wiz4Map.L1CosmicCube, 8, 2);
        public MapXY Cube23StairsUp = new MapXY(Wiz4Map.L2CosmicCube, 18, 2);
        public MapXY Cube24StairsDown = new MapXY(Wiz4Map.L1CosmicCube, 9, 9);
        public MapXY Cube25Teleport = new MapXY(Wiz4Map.L3CosmicCube, 10, 2);
        public MapXY Cube26StairsDown = new MapXY(Wiz4Map.L1CosmicCube, 8, 5);
        public MapXY Cube27TeleportUp = new MapXY(Wiz4Map.L3CosmicCube, 1, 2);
        public MapXY Cube28TeleportUp = new MapXY(Wiz4Map.L2CosmicCube, 8, 2);
        public MapXY Cube29UseHHG = new MapXY(Wiz4Map.L1CosmicCube, 15, 15);
        public MapXY Cube30EscapeHHG = new MapXY(Wiz4Map.L1CosmicCube, 16, 15);
        public MapXY Cube31StairsUp = new MapXY(Wiz4Map.L1CosmicCube, 15, 16);
        public MapXY Level1 = new MapXY(Wiz4Map.L1CosmicCube, -1, -1);
        public MapXY Level2 = new MapXY(Wiz4Map.L2CosmicCube, -1, -1);
        public MapXY Level3 = new MapXY(Wiz4Map.L3CosmicCube, -1, -1);
        public MapXY Level4 = new MapXY(Wiz4Map.L4MazeOfWandering, -1, -1);
        public MapXY Level5 = new MapXY(Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness, -1, -1);
        public MapXY Level6 = new MapXY(Wiz4Map.L6RealmOfTheWhirlingDervish, -1, -1);
        public MapXY Level7 = new MapXY(Wiz4Map.L7TempleOfTheDreampainter, -1, -1);
        public MapXY Level8 = new MapXY(Wiz4Map.L8LandOf1000Cuts, -1, -1);
        public MapXY Level9 = new MapXY(Wiz4Map.L9TheCatacombs, -1, -1);
        public MapXY Level10 = new MapXY(Wiz4Map.L10PyramidOfEntrapment, -1, -1);
        public MapXY Level11 = new MapXY(Wiz4Map.L11Grandmaster, -1, -1);

        public MapXY[] Pentagrams;

        public MapXY Level(int i)
        {
            switch (i)
            {
                case 1: return Level1;
                case 2: return Level2;
                case 3: return Level3;
                case 4: return Level4;
                case 5: return Level5;
                case 6: return Level6;
                case 7: return Level7;
                case 8: return Level8;
                case 9: return Level9;
                case 10: return Level10;
                case 11: return Level11;
                default: return None;
            }
        }

        public Wiz4Locations()
        {
            Pentagrams = new MapXY[] { Pentagram10A, Pentagram10B, Pentagram09, Pentagram08, Pentagram07, Pentagram06A, Pentagram06B,
                Pentagram06C, Pentagram05, Pentagram04A, Pentagram04B, Pentagram04C, Pentagram03A, Pentagram03B, Pentagram03C, Pentagram03D, Pentagram03E,
                Pentagram03F, Pentagram02A, Pentagram02B, Pentagram02C, Pentagram02D, Pentagram02E, Pentagram02F, Pentagram01A, Pentagram01B, Pentagram01C,
                Pentagram01D, Pentagram01E, Pentagram01F, Pentagram01G };
        }

        public bool IsPentagram(int iMap, Point pt)
        {
            foreach (MapXY map in Pentagrams)
                if (map.Map == iMap && map.Location == pt)
                    return true;
            return false;
        }
    }

    public class Wiz5Locations
    {
        // Wizardry 5

        public MapXY None = new MapXY(GameNames.Wizardry5, -1, 0, 0, -1);
        public MapXY Castle = new MapXY(Wiz5Map.Castle, 0, 0);

        public MapXY L1_1818_Lala = new MapXY(Wiz5Map.MazeLevel1, -105, -105);
        public MapXY L1_8180_Sign = new MapXY(Wiz5Map.MazeLevel1, 0, -1);
        public MapXY L1_8181_Up = new MapXY(Wiz5Map.MazeLevel1, 0, 0);
        public MapXY L1_8384_Locked = new MapXY(Wiz5Map.MazeLevel1, 2, 3);
        public MapXY L1_8585_Zombie = new MapXY(Wiz5Map.MazeLevel1, 4, 4);
        public MapXY L1_869C_Silver = new MapXY(Wiz5Map.MazeLevel1, 5, 27);
        public MapXY L1_8782_Conveyor = new MapXY(Wiz5Map.MazeLevel1, 6, 1);
        public MapXY L1_8783_Sign = new MapXY(Wiz5Map.MazeLevel1, 6, 2);
        public MapXY L1_8784_MaintDoor = new MapXY(Wiz5Map.MazeLevel1, 6, 3);
        public MapXY L1_8785_Motor = new MapXY(Wiz5Map.MazeLevel1, 6, 4);
        public MapXY L1_879C_Silver = new MapXY(Wiz5Map.MazeLevel1, 6, 27);
        public MapXY L1_889D_Down = new MapXY(Wiz5Map.MazeLevel1, 7, 28);
        public MapXY L1_898A_Sign = new MapXY(Wiz5Map.MazeLevel1, 8, 9);
        public MapXY L1_8992_Temple = new MapXY(Wiz5Map.MazeLevel1, 8, 17);
        public MapXY L1_8999_Gbli = new MapXY(Wiz5Map.MazeLevel1, 8, 24);
        public MapXY L1_8A86_Down = new MapXY(Wiz5Map.MazeLevel1, 9, 5);
        public MapXY L1_8C87_OneWay = new MapXY(Wiz5Map.MazeLevel1, 11, 6);
        public MapXY L1_8D82_Chute = new MapXY(Wiz5Map.MazeLevel1, 12, 1);
        public MapXY L1_8D85_Portal = new MapXY(Wiz5Map.MazeLevel1, 12, 4);
        public MapXY L1_8E8C_Unlock = new MapXY(Wiz5Map.MazeLevel1, 13, 11);
        public MapXY L1_8E98_Sign = new MapXY(Wiz5Map.MazeLevel1, 13, 23);
        public MapXY L1_8F8C_Locked = new MapXY(Wiz5Map.MazeLevel1, 14, 11);
        public MapXY L1_9082_Locked = new MapXY(Wiz5Map.MazeLevel1, 15, 1);
        public MapXY L1_908A_Portal = new MapXY(Wiz5Map.MazeLevel1, 15, 9);
        public MapXY L1_9098_Golem = new MapXY(Wiz5Map.MazeLevel1, 15, 23);
        public MapXY L1_9194_Locked = new MapXY(Wiz5Map.MazeLevel1, 16, 19);
        public MapXY L1_937B_Down = new MapXY(Wiz5Map.MazeLevel1, 18, -6);
        public MapXY L1_938A_Orb = new MapXY(Wiz5Map.MazeLevel1, 18, 9);
        public MapXY L1_947B_Sign = new MapXY(Wiz5Map.MazeLevel1, 19, -6);
        public MapXY L1_959C_Vampire = new MapXY(Wiz5Map.MazeLevel1, 20, 27);
        public MapXY L1_968C_Locked = new MapXY(Wiz5Map.MazeLevel1, 21, 11);
        public MapXY L1_969C_Ironose = new MapXY(Wiz5Map.MazeLevel1, 21, 27);
        public MapXY L1_979F_Kettle = new MapXY(Wiz5Map.MazeLevel1, 22, 30);
        public MapXY L1_9B8B_Trap = new MapXY(Wiz5Map.MazeLevel1, 26, 10);
        public MapXY L1_9C8F_SilverKey = new MapXY(Wiz5Map.MazeLevel1, 27, 14);
        public MapXY L1_9D8E_Locked = new MapXY(Wiz5Map.MazeLevel1, 28, 13);
        public MapXY L1_9D8F_Sign = new MapXY(Wiz5Map.MazeLevel1, 28, 14);

        public MapXY L2_7080_Trap = new MapXY(Wiz5Map.MazeLevel2, -17, -1);
        public MapXY L2_7175_Pool = new MapXY(Wiz5Map.MazeLevel2, -16, -12);
        public MapXY L2_717B_Trap = new MapXY(Wiz5Map.MazeLevel2, -16, -6);
        public MapXY L2_7488_Trap = new MapXY(Wiz5Map.MazeLevel2, -13, 7);
        public MapXY L2_7773_Trap = new MapXY(Wiz5Map.MazeLevel2, -10, -14);
        public MapXY L2_7778_Sign = new MapXY(Wiz5Map.MazeLevel2, -10, -9);
        public MapXY L2_7889_Up = new MapXY(Wiz5Map.MazeLevel2, -9, 8);
        public MapXY L2_7A84_Ruby = new MapXY(Wiz5Map.MazeLevel2, -7, 3);
        public MapXY L2_7C83_Up = new MapXY(Wiz5Map.MazeLevel2, -5, 2);
        public MapXY L2_7E73_Rum = new MapXY(Wiz5Map.MazeLevel2, -3, -14);
        public MapXY L2_8187_Locked = new MapXY(Wiz5Map.MazeLevel2, 0, 6);
        public MapXY L2_8279_Portal = new MapXY(Wiz5Map.MazeLevel2, 1, -8);
        public MapXY L2_8372_Chains = new MapXY(Wiz5Map.MazeLevel2, 2, -15);
        public MapXY L2_8381_Sign = new MapXY(Wiz5Map.MazeLevel2, 2, 0);
        public MapXY L2_8472_Chains = new MapXY(Wiz5Map.MazeLevel2, 3, -15);
        public MapXY L2_8575_Sign = new MapXY(Wiz5Map.MazeLevel2, 4, -12);
        public MapXY L2_8581_Scepter = new MapXY(Wiz5Map.MazeLevel2, 4, 0);
        public MapXY L2_8584_Hacksaw = new MapXY(Wiz5Map.MazeLevel2, 4, 3);
        public MapXY L2_887D_Elevator = new MapXY(Wiz5Map.MazeLevel2, 7, -4);
        public MapXY L2_888B_Locked = new MapXY(Wiz5Map.MazeLevel2, 7, 10);
        public MapXY L2_8975_Trap = new MapXY(Wiz5Map.MazeLevel2, 8, -12);
        public MapXY L2_8981_SpiritAway = new MapXY(Wiz5Map.MazeLevel2, 8, 0);
        public MapXY L2_8983_Locked = new MapXY(Wiz5Map.MazeLevel2, 8, 2);
        public MapXY L2_8988_Guardian = new MapXY(Wiz5Map.MazeLevel2, 8, 7);
        public MapXY L2_8A8B_Duck = new MapXY(Wiz5Map.MazeLevel2, 9, 10);
        public MapXY L2_8D73_Down = new MapXY(Wiz5Map.MazeLevel2, 12, -14);
        public MapXY L2_8E8A_Flagon = new MapXY(Wiz5Map.MazeLevel2, 13, 9);
        public MapXY L2_8E8B_Sign = new MapXY(Wiz5Map.MazeLevel2, 13, 10);
        public MapXY L2_8E8E_Unknown = new MapXY(Wiz5Map.MazeLevel2, 13, 13);

        public MapXY L3_7F73_Fountain = new MapXY(Wiz5Map.MazeLevel3, -2, -14);
        public MapXY L3_8173_Sign = new MapXY(Wiz5Map.MazeLevel3, 0, -14);
        public MapXY L3_8273_Locked = new MapXY(Wiz5Map.MazeLevel3, 1, -14);
        public MapXY L3_846A_Moat = new MapXY(Wiz5Map.MazeLevel3, 3, -23);
        public MapXY L3_8676_Trap = new MapXY(Wiz5Map.MazeLevel3, 5, -11);
        public MapXY L3_876E_Trap = new MapXY(Wiz5Map.MazeLevel3, 6, -19);
        public MapXY L3_8772_Stomper = new MapXY(Wiz5Map.MazeLevel3, 6, -15);
        public MapXY L3_8785_Sign = new MapXY(Wiz5Map.MazeLevel3, 6, 4);
        public MapXY L3_8789_Sign = new MapXY(Wiz5Map.MazeLevel3, 6, 8);
        public MapXY L3_878A_Timeless = new MapXY(Wiz5Map.MazeLevel3, 6, 9);
        public MapXY L3_887D_Elevator = new MapXY(Wiz5Map.MazeLevel3, 7, -4);
        public MapXY L3_8882_Teleport = new MapXY(Wiz5Map.MazeLevel3, 7, 1);
        public MapXY L3_8887_Teleport = new MapXY(Wiz5Map.MazeLevel3, 7, 6);
        public MapXY L3_8C79_Stomper = new MapXY(Wiz5Map.MazeLevel3, 11, -8);
        public MapXY L3_8D65_Down = new MapXY(Wiz5Map.MazeLevel3, 12, -28);
        public MapXY L3_8D67_Sign = new MapXY(Wiz5Map.MazeLevel3, 12, -26);
        public MapXY L3_8D68_Candle = new MapXY(Wiz5Map.MazeLevel3, 12, -25);
        public MapXY L3_8D73_Up = new MapXY(Wiz5Map.MazeLevel3, 12, -14);
        public MapXY L3_8D82_Trap = new MapXY(Wiz5Map.MazeLevel3, 12, 1);
        public MapXY L3_8D86_Hienmitey = new MapXY(Wiz5Map.MazeLevel3, 12, 5);
        public MapXY L3_8D89_Dejin = new MapXY(Wiz5Map.MazeLevel3, 12, 8);
        public MapXY L3_8E6D_Stomper = new MapXY(Wiz5Map.MazeLevel3, 13, -20);
        public MapXY L3_8E78_Trap = new MapXY(Wiz5Map.MazeLevel3, 13, -9);
        public MapXY L3_917B_Trap = new MapXY(Wiz5Map.MazeLevel3, 16, -6);
        public MapXY L3_9274_Trap = new MapXY(Wiz5Map.MazeLevel3, 17, -13);
        public MapXY L3_9282_Teleport = new MapXY(Wiz5Map.MazeLevel3, 17, 1);
        public MapXY L3_9287_Teleport = new MapXY(Wiz5Map.MazeLevel3, 17, 6);
        public MapXY L3_9374_Stomper = new MapXY(Wiz5Map.MazeLevel3, 18, -13);
        public MapXY L3_9385_Sign = new MapXY(Wiz5Map.MazeLevel3, 18, 4);
        public MapXY L3_938A_Trap = new MapXY(Wiz5Map.MazeLevel3, 18, 9);
        public MapXY L3_966A_Trap = new MapXY(Wiz5Map.MazeLevel3, 21, -23);
        public MapXY L3_9873_Locked = new MapXY(Wiz5Map.MazeLevel3, 23, -14);
        public MapXY L3_9973_Sign = new MapXY(Wiz5Map.MazeLevel3, 24, -14);
        public MapXY L3_9B73_Pool = new MapXY(Wiz5Map.MazeLevel3, 26, -14);

        public MapXY L4_705F_Access = new MapXY(Wiz5Map.MazeLevel4, -17, -34);
        public MapXY L4_7065_Access = new MapXY(Wiz5Map.MazeLevel4, -17, -28);
        public MapXY L4_7163_Access = new MapXY(Wiz5Map.MazeLevel4, -16, -30);
        public MapXY L4_7261_Discs = new MapXY(Wiz5Map.MazeLevel4, -15, -32);
        public MapXY L4_7264_Discs = new MapXY(Wiz5Map.MazeLevel4, -15, -29);
        public MapXY L4_7267_Discs = new MapXY(Wiz5Map.MazeLevel4, -15, -26);
        public MapXY L4_726C_Gold = new MapXY(Wiz5Map.MazeLevel4, -15, -21);
        public MapXY L4_7463_IronGloves = new MapXY(Wiz5Map.MazeLevel4, -13, -30);
        public MapXY L4_7465_Encounter = new MapXY(Wiz5Map.MazeLevel4, -13, -28);
        public MapXY L4_7469_Frozz = new MapXY(Wiz5Map.MazeLevel4, -13, -24);
        public MapXY L4_7565_Access = new MapXY(Wiz5Map.MazeLevel4, -12, -28);
        public MapXY L4_7568_Encounter = new MapXY(Wiz5Map.MazeLevel4, -12, -25);
        public MapXY L4_7660_Access = new MapXY(Wiz5Map.MazeLevel4, -11, -33);
        public MapXY L4_775E_Sign = new MapXY(Wiz5Map.MazeLevel4, -10, -35);
        public MapXY L4_7761_Discs = new MapXY(Wiz5Map.MazeLevel4, -10, -32);
        public MapXY L4_7764_Discs = new MapXY(Wiz5Map.MazeLevel4, -10, -29);
        public MapXY L4_7767_Discs = new MapXY(Wiz5Map.MazeLevel4, -10, -26);
        public MapXY L4_785F_Encounter = new MapXY(Wiz5Map.MazeLevel4, -9, -34);
        public MapXY L4_7862_Access = new MapXY(Wiz5Map.MazeLevel4, -9, -31);
        public MapXY L4_7965_Crested = new MapXY(Wiz5Map.MazeLevel4, -8, -28);
        public MapXY L4_7968_Access = new MapXY(Wiz5Map.MazeLevel4, -8, -25);
        public MapXY L4_796C_Statues = new MapXY(Wiz5Map.MazeLevel4, -8, -21);
        public MapXY L4_7B6D_Up = new MapXY(Wiz5Map.MazeLevel4, -6, -20);
        public MapXY L4_7B70_Sign = new MapXY(Wiz5Map.MazeLevel4, -6, -17);
        public MapXY L4_7C70_Barrier = new MapXY(Wiz5Map.MazeLevel4, -5, -17);
        public MapXY L4_7C71_Barrier = new MapXY(Wiz5Map.MazeLevel4, -5, -16);
        public MapXY L4_7C72_Barrier = new MapXY(Wiz5Map.MazeLevel4, -5, -15);
        public MapXY L4_7C73_Barrier = new MapXY(Wiz5Map.MazeLevel4, -5, -14);
        public MapXY L4_7D71_Barrier = new MapXY(Wiz5Map.MazeLevel4, -4, -16);
        public MapXY L4_7E6D_Sign = new MapXY(Wiz5Map.MazeLevel4, -3, -20);
        public MapXY L4_7E6F_Barrier = new MapXY(Wiz5Map.MazeLevel4, -3, -18);
        public MapXY L4_7E72_Barrier = new MapXY(Wiz5Map.MazeLevel4, -3, -15);
        public MapXY L4_7E74_Barrier = new MapXY(Wiz5Map.MazeLevel4, -3, -13);
        public MapXY L4_7F74_Teleport = new MapXY(Wiz5Map.MazeLevel4, -2, -13);
        public MapXY L4_8069_Time = new MapXY(Wiz5Map.MazeLevel4, -1, -24);
        public MapXY L4_826E_Sign = new MapXY(Wiz5Map.MazeLevel4, 1, -19);
        public MapXY L4_8369_Loon = new MapXY(Wiz5Map.MazeLevel4, 2, -24);
        public MapXY L4_8569_Skeleton = new MapXY(Wiz5Map.MazeLevel4, 4, -24);
        public MapXY L4_8573_Unknown = new MapXY(Wiz5Map.MazeLevel4, 4, -14);
        public MapXY L4_8674_Down = new MapXY(Wiz5Map.MazeLevel4, 5, -13);
        public MapXY L4_8770_Demon = new MapXY(Wiz5Map.MazeLevel4, 6, -17);
        public MapXY L4_8784_Portal = new MapXY(Wiz5Map.MazeLevel4, 6, 3);
        public MapXY L4_887D_Elevator = new MapXY(Wiz5Map.MazeLevel4, 7, -4);
        public MapXY L4_897D_Sign = new MapXY(Wiz5Map.MazeLevel4, 8, -4);
        public MapXY L4_8983_Unknown = new MapXY(Wiz5Map.MazeLevel4, 8, 2);
        public MapXY L4_8A78_Pool = new MapXY(Wiz5Map.MazeLevel4, 9, -9);
        public MapXY L4_8B7F_Den = new MapXY(Wiz5Map.MazeLevel4, 10, -2);
        public MapXY L4_8B80_Sign = new MapXY(Wiz5Map.MazeLevel4, 10, -1);
        public MapXY L4_8C71_Jack = new MapXY(Wiz5Map.MazeLevel4, 11, -16);
        public MapXY L4_8C7A_Battery = new MapXY(Wiz5Map.MazeLevel4, 11, -7);
        public MapXY L4_9379_Trap = new MapXY(Wiz5Map.MazeLevel4, 18, -8);
        public MapXY L4_937A_Trap = new MapXY(Wiz5Map.MazeLevel4, 18, -7);

        public MapXY L5_6D80_Fountain = new MapXY(Wiz5Map.MazeLevel5, -20, -1);
        public MapXY L5_6D82_Locked = new MapXY(Wiz5Map.MazeLevel5, -20, 1);
        public MapXY L5_7480_Down = new MapXY(Wiz5Map.MazeLevel5, -13, -1);
        public MapXY L5_7983_Sign = new MapXY(Wiz5Map.MazeLevel5, -8, 2);
        public MapXY L5_7B62_Down = new MapXY(Wiz5Map.MazeLevel5, -6, -31);
        public MapXY L5_7B80_Teleport = new MapXY(Wiz5Map.MazeLevel5, -6, -1);
        public MapXY L5_7B81_OneWay = new MapXY(Wiz5Map.MazeLevel5, -6, 0);
        public MapXY L5_7C83_Teleport = new MapXY(Wiz5Map.MazeLevel5, -5, 2);
        public MapXY L5_7D7F_OneWay = new MapXY(Wiz5Map.MazeLevel5, -4, -2);
        public MapXY L5_7D81_Spinner = new MapXY(Wiz5Map.MazeLevel5, -4, 0);
        public MapXY L5_7D83_OneWay = new MapXY(Wiz5Map.MazeLevel5, -4, 2);
        public MapXY L5_7E6B_Locked = new MapXY(Wiz5Map.MazeLevel5, -3, -22);
        public MapXY L5_7E7F_Teleport = new MapXY(Wiz5Map.MazeLevel5, -3, -2);
        public MapXY L5_7F81_OneWay = new MapXY(Wiz5Map.MazeLevel5, -2, 0);
        public MapXY L5_7F82_Teleport = new MapXY(Wiz5Map.MazeLevel5, -2, 1);
        public MapXY L5_8080_Mystery = new MapXY(Wiz5Map.MazeLevel5, -1, -1);
        public MapXY L5_8174_Slide = new MapXY(Wiz5Map.MazeLevel5, 0, -13);
        public MapXY L5_8179_Unknown = new MapXY(Wiz5Map.MazeLevel5, 0, -8);
        public MapXY L5_8273_Trap = new MapXY(Wiz5Map.MazeLevel5, 1, -14);
        public MapXY L5_8275_Werewolf = new MapXY(Wiz5Map.MazeLevel5, 1, -12);
        public MapXY L5_8372_Slide = new MapXY(Wiz5Map.MazeLevel5, 2, -15);
        public MapXY L5_8374_Teleport = new MapXY(Wiz5Map.MazeLevel5, 2, -13);
        public MapXY L5_8376_Slide = new MapXY(Wiz5Map.MazeLevel5, 2, -11);
        public MapXY L5_8379_Sign = new MapXY(Wiz5Map.MazeLevel5, 2, -8);
        public MapXY L5_8382_Lord = new MapXY(Wiz5Map.MazeLevel5, 2, 1);
        public MapXY L5_8383_Sign = new MapXY(Wiz5Map.MazeLevel5, 2, 2);
        public MapXY L5_8387_Sign = new MapXY(Wiz5Map.MazeLevel5, 2, 6);
        public MapXY L5_8473_Werewolf = new MapXY(Wiz5Map.MazeLevel5, 3, -14);
        public MapXY L5_8475_Trap = new MapXY(Wiz5Map.MazeLevel5, 3, -12);
        public MapXY L5_8574_Slide = new MapXY(Wiz5Map.MazeLevel5, 4, -13);
        public MapXY L5_8675_Snatch = new MapXY(Wiz5Map.MazeLevel5, 5, -12);
        public MapXY L5_8865_Sign = new MapXY(Wiz5Map.MazeLevel5, 7, -28);
        public MapXY L5_887D_Elevator = new MapXY(Wiz5Map.MazeLevel5, 7, -4);
        public MapXY L5_8880_BigMax = new MapXY(Wiz5Map.MazeLevel5, 7, -1);
        public MapXY L5_8B80_Sign = new MapXY(Wiz5Map.MazeLevel5, 10, -1);
        public MapXY L5_8D80_OneWay = new MapXY(Wiz5Map.MazeLevel5, 12, -1);
        public MapXY L5_8D8E_Sign = new MapXY(Wiz5Map.MazeLevel5, 12, 13);
        public MapXY L5_8D8F_Lady = new MapXY(Wiz5Map.MazeLevel5, 12, 14);
        public MapXY L5_8D90_Unknown = new MapXY(Wiz5Map.MazeLevel5, 12, 15);
        public MapXY L5_9079_Chute = new MapXY(Wiz5Map.MazeLevel5, 15, -8);

        public MapXY L6_747E_Up = new MapXY(Wiz5Map.MazeLevel6, -13, -3);
        public MapXY L6_767F_Locked = new MapXY(Wiz5Map.MazeLevel6, -11, -2);
        public MapXY L6_786E_Trap = new MapXY(Wiz5Map.MazeLevel6, -9, -19);
        public MapXY L6_7978_Well = new MapXY(Wiz5Map.MazeLevel6, -8, -9);
        public MapXY L6_797F_EvilEyes = new MapXY(Wiz5Map.MazeLevel6, -8, -2);
        public MapXY L6_7A6B_Trap = new MapXY(Wiz5Map.MazeLevel6, -7, -22);
        public MapXY L6_7B60_Up = new MapXY(Wiz5Map.MazeLevel6, -6, -33);
        public MapXY L6_817F_IceKey = new MapXY(Wiz5Map.MazeLevel6, 0, -2);
        public MapXY L6_8271_Slide = new MapXY(Wiz5Map.MazeLevel6, 1, -16);
        public MapXY L6_8273_Sign = new MapXY(Wiz5Map.MazeLevel6, 1, -14);
        public MapXY L6_8468_Slide = new MapXY(Wiz5Map.MazeLevel6, 3, -25);
        public MapXY L6_8470_Slide = new MapXY(Wiz5Map.MazeLevel6, 3, -17);
        public MapXY L6_8668_Cavern = new MapXY(Wiz5Map.MazeLevel6, 5, -25);
        public MapXY L6_866D_Slide = new MapXY(Wiz5Map.MazeLevel6, 5, -20);
        public MapXY L6_8670_Slide = new MapXY(Wiz5Map.MazeLevel6, 5, -17);
        public MapXY L6_876A_DemonOut = new MapXY(Wiz5Map.MazeLevel6, 6, -23);
        public MapXY L6_886D_Slide = new MapXY(Wiz5Map.MazeLevel6, 7, -20);
        public MapXY L6_886F_IceCapades = new MapXY(Wiz5Map.MazeLevel6, 7, -18);
        public MapXY L6_8883_Chute = new MapXY(Wiz5Map.MazeLevel6, 7, 2);
        public MapXY L6_8968_King = new MapXY(Wiz5Map.MazeLevel6, 8, -25);
        public MapXY L6_8B66_Down = new MapXY(Wiz5Map.MazeLevel6, 10, -27);
        public MapXY L6_8D78_Trap = new MapXY(Wiz5Map.MazeLevel6, 12, -9);
        public MapXY L6_8D7C_Locked = new MapXY(Wiz5Map.MazeLevel6, 12, -5);
        public MapXY L6_8F76_Trap = new MapXY(Wiz5Map.MazeLevel6, 14, -11);
        public MapXY L6_8F7C_MightyYog = new MapXY(Wiz5Map.MazeLevel6, 14, -5);
        public MapXY L6_9880_Bog = new MapXY(Wiz5Map.MazeLevel6, 23, -1);

        public MapXY L7_7272_Chute = new MapXY(Wiz5Map.MazeLevel7, -15, -15);
        public MapXY L7_728F_Chute = new MapXY(Wiz5Map.MazeLevel7, -15, 14);
        public MapXY L7_7480_Wind = new MapXY(Wiz5Map.MazeLevel7, -13, -1);
        public MapXY L7_7481_Wind = new MapXY(Wiz5Map.MazeLevel7, -13, 0);
        public MapXY L7_757A_AirStaff = new MapXY(Wiz5Map.MazeLevel7, -12, -7);
        public MapXY L7_7587_Trap = new MapXY(Wiz5Map.MazeLevel7, -12, 6);
        public MapXY L7_7B7B_LordDiamonds = new MapXY(Wiz5Map.MazeLevel7, -6, -6);
        public MapXY L7_7B86_LordSpades = new MapXY(Wiz5Map.MazeLevel7, -6, 5);
        public MapXY L7_7C74_EarthStaff = new MapXY(Wiz5Map.MazeLevel7, -5, -13);
        public MapXY L7_8074_Monkey = new MapXY(Wiz5Map.MazeLevel7, -1, -13);
        public MapXY L7_8080_Blue = new MapXY(Wiz5Map.MazeLevel7, -1, -1);
        public MapXY L7_8081_White = new MapXY(Wiz5Map.MazeLevel7, -1, 0);
        public MapXY L7_8174_Monkey = new MapXY(Wiz5Map.MazeLevel7, 0, -13);
        public MapXY L7_8180_Red = new MapXY(Wiz5Map.MazeLevel7, 0, -1);
        public MapXY L7_8181_Yellow = new MapXY(Wiz5Map.MazeLevel7, 0, 0);
        public MapXY L7_867B_LordClubs = new MapXY(Wiz5Map.MazeLevel7, 5, -6);
        public MapXY L7_8686_LordHearts = new MapXY(Wiz5Map.MazeLevel7, 5, 5);
        public MapXY L7_8B8D_Pool = new MapXY(Wiz5Map.MazeLevel7, 10, 12);
        public MapXY L7_8E78_FireStaff = new MapXY(Wiz5Map.MazeLevel7, 13, -9);
        public MapXY L7_8E7D_Lightning = new MapXY(Wiz5Map.MazeLevel7, 13, -4);
        public MapXY L7_8E84_Trap = new MapXY(Wiz5Map.MazeLevel7, 13, 3);
        public MapXY L7_8E89_Kanzi = new MapXY(Wiz5Map.MazeLevel7, 13, 8);
        public MapXY L7_8F72_Up = new MapXY(Wiz5Map.MazeLevel7, 14, -15);
        public MapXY L7_8F8F_Chute = new MapXY(Wiz5Map.MazeLevel7, 14, 14);

        public MapXY L8_090D_Sign = new MapXY(Wiz5Map.MazeLevel8, -120, -116);
        public MapXY L8_0B0F_Trap = new MapXY(Wiz5Map.MazeLevel8, -118, -114);
        public MapXY L8_0E19_Trap = new MapXY(Wiz5Map.MazeLevel8, -115, -104);
        public MapXY L8_1015_Up = new MapXY(Wiz5Map.MazeLevel8, -113, -108);
        public MapXY L8_7681_Portal = new MapXY(Wiz5Map.MazeLevel8, -11, 0);
        public MapXY L8_7B81_Clones = new MapXY(Wiz5Map.MazeLevel8, -6, 0);
        public MapXY L8_7D81_Red = new MapXY(Wiz5Map.MazeLevel8, -4, 0);
        public MapXY L8_8176_Portal = new MapXY(Wiz5Map.MazeLevel8, 0, -11);
        public MapXY L8_817B_Clones = new MapXY(Wiz5Map.MazeLevel8, 0, -6);
        public MapXY L8_817D_Blue = new MapXY(Wiz5Map.MazeLevel8, 0, -4);
        public MapXY L8_8181_Sorn = new MapXY(Wiz5Map.MazeLevel8, 0, 0);
        public MapXY L8_8185_White = new MapXY(Wiz5Map.MazeLevel8, 0, 4);
        public MapXY L8_8187_Clones = new MapXY(Wiz5Map.MazeLevel8, 0, 6);
        public MapXY L8_818C_Portal = new MapXY(Wiz5Map.MazeLevel8, 0, 11);
        public MapXY L8_8581_Yellow = new MapXY(Wiz5Map.MazeLevel8, 4, 0);
        public MapXY L8_8781_Clones = new MapXY(Wiz5Map.MazeLevel8, 6, 0);
        public MapXY L8_8C81_Portal = new MapXY(Wiz5Map.MazeLevel8, 11, 0);
        public MapXY L8_94DF_Up = new MapXY(Wiz5Map.MazeLevel8, 19, 94);
        public MapXY L8_98DB_Trap = new MapXY(Wiz5Map.MazeLevel8, 23, 90);
        public MapXY L8_9CDF_Trap = new MapXY(Wiz5Map.MazeLevel8, 27, 94);
        public MapXY L8_9EE2_Sign = new MapXY(Wiz5Map.MazeLevel8, 29, 97);
        public MapXY L8_A3DA_Trap = new MapXY(Wiz5Map.MazeLevel8, 34, 89);
        public MapXY L8_A5DC_Sign = new MapXY(Wiz5Map.MazeLevel8, 36, 91);
        public MapXY L8_A8DC_Teleport = new MapXY(Wiz5Map.MazeLevel8, 39, 91);
        public MapXY L8_CD42_Sign = new MapXY(Wiz5Map.MazeLevel8, 76, -63);
        public MapXY L8_CE45_Up = new MapXY(Wiz5Map.MazeLevel8, 77, -60);
        public MapXY L8_D042_Manfretti = new MapXY(Wiz5Map.MazeLevel8, 79, -63);
        public MapXY L8_D342_Trap = new MapXY(Wiz5Map.MazeLevel8, 82, -63);
        public MapXY L8_DB40_Trap = new MapXY(Wiz5Map.MazeLevel8, 90, -65);
        public MapXY L8_DB43_Sign = new MapXY(Wiz5Map.MazeLevel8, 90, -62);

        public Wiz5Locations()
        {

        }
    }
}