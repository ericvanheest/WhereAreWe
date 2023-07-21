using System.Drawing;
namespace WhereAreWe
{
    // These lists of quest-related locations keeps the GetQuests() functions from becoming 
    // even more ungainly than they are already.

    public class Ultima1Locations
    {
        // Ultima 1
        public MapXY None = new MapXY(GameNames.Ultima1, -1, 0, 0, -1);

        // Overworld
        public MapXY SpotTheDungeonOfPerinia = new MapXY(Ultima1Map.Overworld, 18, 13);
        public MapXY SpotPillarsOfProtection = new MapXY(Ultima1Map.Overworld, 36, 9);
        public MapXY SpotTheUnholyHole = new MapXY(Ultima1Map.Overworld, 48, 11);
        public MapXY SpotTowerOfKnowledge = new MapXY(Ultima1Map.Overworld, 69, 10);
        public MapXY SpotTheDungeonOfMontor = new MapXY(Ultima1Map.Overworld, 53, 22);
        public MapXY SpotCityOfGrey = new MapXY(Ultima1Map.Overworld, 64, 22);
        public MapXY SpotCastleOfTheLostKing = new MapXY(Ultima1Map.Overworld, 32, 27);
        public MapXY SpotCityOfPaws = new MapXY(Ultima1Map.Overworld, 46, 28);
        public MapXY SpotMinesOfMountDrash = new MapXY(Ultima1Map.Overworld, 59, 29);
        public MapXY SpotCityOfYew = new MapXY(Ultima1Map.Overworld, 18, 34);
        public MapXY SpotCityOfBritain = new MapXY(Ultima1Map.Overworld, 39, 39);
        public MapXY SpotCastleOfLordBritish = new MapXY(Ultima1Map.Overworld, 40, 38);
        public MapXY SpotCityOfMoon = new MapXY(Ultima1Map.Overworld, 66, 41);
        public MapXY SpotMondainsGateToHell = new MapXY(Ultima1Map.Overworld, 29, 37);
        public MapXY SpotTheLostCaverns = new MapXY(Ultima1Map.Overworld, 13, 43);
        public MapXY SpotTheDungeonOfDoubt = new MapXY(Ultima1Map.Overworld, 62, 49);
        public MapXY SpotCityOfFawn = new MapXY(Ultima1Map.Overworld, 25, 61);
        public MapXY SpotMinesOfMountDrash2 = new MapXY(Ultima1Map.Overworld, 39, 60);
        public MapXY SpotCityOfMontor = new MapXY(Ultima1Map.Overworld, 52, 63);
        public MapXY SpotCityOfTune = new MapXY(Ultima1Map.Overworld, 70, 63);
        public MapXY SpotDeathsAwakening = new MapXY(Ultima1Map.Overworld, 38, 68);
        public MapXY SpotSignPost = new MapXY(Ultima1Map.Overworld, 13, 89);
        public MapXY SpotCityOfPoor = new MapXY(Ultima1Map.Overworld, 25, 94);
        public MapXY SpotCityOfClearLagoon = new MapXY(Ultima1Map.Overworld, 44, 92);
        public MapXY SpotCityOfWealth = new MapXY(Ultima1Map.Overworld, 66, 88);
        public MapXY SpotTrampOfDoom = new MapXY(Ultima1Map.Overworld, 52, 96);
        public MapXY SpotTheVipersPit = new MapXY(Ultima1Map.Overworld, 32, 99);
        public MapXY SpotTheLongDeath = new MapXY(Ultima1Map.Overworld, 25, 105);
        public MapXY SpotTheEnd = new MapXY(Ultima1Map.Overworld, 14, 110);
        public MapXY SpotCityOfGauntlet = new MapXY(Ultima1Map.Overworld, 31, 112);
        public MapXY SpotCityOfOlympus = new MapXY(Ultima1Map.Overworld, 41, 118);
        public MapXY SpotCityOfNassau = new MapXY(Ultima1Map.Overworld, 42, 119);
        public MapXY SpotVipersPit2 = new MapXY(Ultima1Map.Overworld, 63, 119);
        public MapXY SpotTheSlowDeath = new MapXY(Ultima1Map.Overworld, 71, 120);
        public MapXY SpotSouthernSignPost = new MapXY(Ultima1Map.Overworld, 12, 122);
        public MapXY SpotBlackDragonsCastle = new MapXY(Ultima1Map.Overworld, 30, 126);
        public MapXY SpotTheGuildOfDeath = new MapXY(Ultima1Map.Overworld, 40, 129);
        public MapXY SpotCityOfStout = new MapXY(Ultima1Map.Overworld, 64, 133);
        public MapXY SpotTheMetalTwister = new MapXY(Ultima1Map.Overworld, 16, 140);
        public MapXY SpotCityOfPonder = new MapXY(Ultima1Map.Overworld, 37, 140);
        public MapXY SpotTrollsHole = new MapXY(Ultima1Map.Overworld, 46, 145);
        public MapXY SpotTheSavagePlace = new MapXY(Ultima1Map.Overworld, 130, 10);
        public MapXY SpotScorpionHole = new MapXY(Ultima1Map.Overworld, 100, 15);
        public MapXY SpotCityOfGerry = new MapXY(Ultima1Map.Overworld, 121, 15);
        public MapXY SpotCityOfHelen = new MapXY(Ultima1Map.Overworld, 148, 22);
        public MapXY SpotAdvarisHole = new MapXY(Ultima1Map.Overworld, 124, 26);
        public MapXY SpotCastleRondorin = new MapXY(Ultima1Map.Overworld, 114, 29);
        public MapXY SpotPillarsOfTheArgonauts = new MapXY(Ultima1Map.Overworld, 96, 33);
        public MapXY SpotCityOfArnold = new MapXY(Ultima1Map.Overworld, 126, 36);
        public MapXY SpotCastleBarataria = new MapXY(Ultima1Map.Overworld, 125, 37);
        public MapXY SpotTheHorrorOfTheHarpies = new MapXY(Ultima1Map.Overworld, 147, 36);
        public MapXY SpotTheDeadWarriorsFight = new MapXY(Ultima1Map.Overworld, 155, 35);
        public MapXY SpotTheLabyrinth = new MapXY(Ultima1Map.Overworld, 98, 45);
        public MapXY SpotCityOfOwen = new MapXY(Ultima1Map.Overworld, 115, 43);
        public MapXY SpotWhereHerculesDied = new MapXY(Ultima1Map.Overworld, 109, 50);
        public MapXY SpotCityOfJohn = new MapXY(Ultima1Map.Overworld, 150, 49);
        public MapXY SpotTheHorrorOfTheHarpies2 = new MapXY(Ultima1Map.Overworld, 116, 56);
        public MapXY SpotTheGorgonHole = new MapXY(Ultima1Map.Overworld, 136, 59);
        public MapXY SpotTheCityOfTheSnake = new MapXY(Ultima1Map.Overworld, 109, 61);
        public MapXY SpotCityOfLinda = new MapXY(Ultima1Map.Overworld, 128, 63);
        public MapXY SpotPillarOfOzymandias = new MapXY(Ultima1Map.Overworld, 97, 66);
        public MapXY SpotCityOfWolf = new MapXY(Ultima1Map.Overworld, 150, 67);
        public MapXY SpotGraveOfTheLostSoul = new MapXY(Ultima1Map.Overworld, 98, 88);
        public MapXY SpotTheSkullSmasher = new MapXY(Ultima1Map.Overworld, 119, 89);
        public MapXY SpotEasternSignPost = new MapXY(Ultima1Map.Overworld, 131, 87);
        public MapXY SpotTheSpineBreaker = new MapXY(Ultima1Map.Overworld, 149, 91);
        public MapXY SpotCityOfLostFriends = new MapXY(Ultima1Map.Overworld, 103, 100);
        public MapXY SpotTheDungeonOfDoom = new MapXY(Ultima1Map.Overworld, 114, 100);
        public MapXY SpotTheDeadCatsLife = new MapXY(Ultima1Map.Overworld, 108, 107);
        public MapXY SpotCityOfWheeler = new MapXY(Ultima1Map.Overworld, 121, 106);
        public MapXY SpotCastleOfShamino = new MapXY(Ultima1Map.Overworld, 135, 105);
        public MapXY SpotCityOfTheBrother = new MapXY(Ultima1Map.Overworld, 149, 112);
        public MapXY SpotTheMorbidAdventure = new MapXY(Ultima1Map.Overworld, 138, 115);
        public MapXY SpotWhiteDragonsCastle = new MapXY(Ultima1Map.Overworld, 127, 116);
        public MapXY SpotCityOfGorlab = new MapXY(Ultima1Map.Overworld, 128, 117);
        public MapXY SpotCityOfDextron = new MapXY(Ultima1Map.Overworld, 101, 119);
        public MapXY SpotFreeDeathHole = new MapXY(Ultima1Map.Overworld, 154, 121);
        public MapXY SpotDeadMansWalk = new MapXY(Ultima1Map.Overworld, 105, 127);
        public MapXY SpotTheDeadCatsLife2 = new MapXY(Ultima1Map.Overworld, 128, 138);
        public MapXY SpotCityOfMagic = new MapXY(Ultima1Map.Overworld, 142, 139);
        public MapXY SpotCityOfTurtle = new MapXY(Ultima1Map.Overworld, 97, 141);
        public MapXY SpotCityOfBulldozer = new MapXY(Ultima1Map.Overworld, 115, 141);
        public MapXY SpotTheHoleToHades = new MapXY(Ultima1Map.Overworld, 129, 146);
        public MapXY SpotCityOfImagination = new MapXY(Ultima1Map.Overworld, 66, 106);

        // Castles
        public MapXY SpotLostKingKing = new MapXY(Ultima1Map.TheCastleOfTheLostKing, 25, 5);
        public MapXY SpotLostKingPrincess = new MapXY(Ultima1Map.TheCastleOfTheLostKing, 31, 5);
        public MapXY SpotRondorinKing = new MapXY(Ultima1Map.TheCastleRondorin, 25, 5);
        public MapXY SpotBlackDragonKing = new MapXY(Ultima1Map.TheBlackDragonsCastle, 25, 5);
        public MapXY SpotShaminoKing = new MapXY(Ultima1Map.TheCastleOfShamino, 25, 5);
        public MapXY SpotShaminoPrincess = new MapXY(Ultima1Map.TheCastleOfShamino, 31, 5);

        public Ultima1Locations()
        {
        }
    }
}