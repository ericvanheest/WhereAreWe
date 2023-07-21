using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Ultima1QuestData : QuestData
    {
        public Ultima1QuestData(UltimaPartyInfo party, LocationInformation location, UltimaGameState state, Ultima1MapData mapData, Ultima1GameInfo gameInfo)
        {
            Map = mapData;
            Info = gameInfo;
            Party = party;
            Location = location;
            State = state;
        }
    }

    public class Ultima1Quest : BasicQuest
    {
        public Ultima1Quest()
        {
        }
    }

    public class Ultima1QuestInfo : QuestInfo<Ultima1Quest>
    {
        // Main Quests
        public QuestStatus RedGem = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Red Gem");  // Kill a Gelatinous Cube - Lost King
        public QuestStatus GreenGem = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Green Gem");  // Kill a Carrion Creeper - Rondorin
        public QuestStatus BlueGem = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a Blue Gem");  // Kill a Lich - Black Dragon
        public QuestStatus WhiteGem = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain a White Gem");  // Kill a Balron - Shamino
        public QuestStatus SpaceAce = new QuestStatus(QuestStatus.Basic.NotStarted, "Become a Space Ace");
        public QuestStatus RescuePrincess = new QuestStatus(QuestStatus.Basic.NotStarted, "Rescue a princess");
        public QuestStatus DefeatMondain = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Mondain");

        // Side Quests
        public QuestStatus FindGrave = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Grave of the Lost Soul");
        public QuestStatus FindSign = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Southern Sign Post");
        public QuestStatus FindPillar = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Pillar of Ozymandias");
        public QuestStatus FindTower = new QuestStatus(QuestStatus.Basic.NotStarted, "Find the Tower of Knowledge");

        // If all of the quests are not returned from the GetAllQuests() function, then the quest window will not
        // update any of the missing quests until a manual refresh is performed.
        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { RedGem, GreenGem, BlueGem, WhiteGem, SpaceAce, RescuePrincess, DefeatMondain,
            FindGrave, FindSign, FindPillar, FindTower }; }

        public override BasicQuest[] GetQuests()
        {
            List<Ultima1Quest> quests = new List<Ultima1Quest>();
            int iMainOrder = 0;

            // Add locations to the QuestStatus objects, and add the objects themselves to the main/side quest lists
            RedGem.AddLocations(new QuestLocation("Go to the Castle of the Lost King", Ultima1.Spots.SpotCastleOfTheLostKing),
                new QuestLocation("Offer your services to the Lost King", Ultima1.Spots.SpotLostKingKing),
                new QuestLocation("Go to level 3 or 4 of any dungeon", Ultima1.Spots.None),
                new QuestLocation("Kill a Gelatinous Cube", Ultima1.Spots.None),
                new QuestLocation("Go to the Castle of the Lost King", Ultima1.Spots.SpotCastleOfTheLostKing)
            );
            RedGem.Postrequisites.Add(new QuestLocation("Talk to the Lost King", Ultima1.Spots.SpotLostKingKing));
            AddMainQuest(iMainOrder++, RedGem, quests);

            // Add locations to the QuestStatus objects, and add the objects themselves to the main/side quest lists
            GreenGem.AddLocations(new QuestLocation("Go to Castle Rondorin", Ultima1.Spots.SpotCastleRondorin),
                new QuestLocation("Offer your services to the King", Ultima1.Spots.SpotRondorinKing),
                new QuestLocation("Go to level 5 or 6 of any dungeon", Ultima1.Spots.None),
                new QuestLocation("Kill a Carrion Creature", Ultima1.Spots.None),
                new QuestLocation("Go to Castle Rondorin", Ultima1.Spots.SpotCastleRondorin)
            );
            GreenGem.Postrequisites.Add(new QuestLocation("Talk to the King", Ultima1.Spots.SpotRondorinKing));
            AddMainQuest(iMainOrder++, GreenGem, quests);

            // Add locations to the QuestStatus objects, and add the objects themselves to the main/side quest lists
            BlueGem.AddLocations(new QuestLocation("Go to the Black Dragon's Castle", Ultima1.Spots.SpotBlackDragonsCastle),
                new QuestLocation("Offer your services to the King", Ultima1.Spots.SpotBlackDragonKing),
                new QuestLocation("Go to level 7 or 8 of any dungeon", Ultima1.Spots.None),
                new QuestLocation("Kill a Lich", Ultima1.Spots.None),
                new QuestLocation("Go to the Black Dragon's Castle", Ultima1.Spots.SpotBlackDragonsCastle)
            );
            BlueGem.Postrequisites.Add(new QuestLocation("Talk to the King", Ultima1.Spots.SpotBlackDragonKing));
            AddMainQuest(iMainOrder++, BlueGem, quests);

            // Add locations to the QuestStatus objects, and add the objects themselves to the main/side quest lists
            WhiteGem.AddLocations(new QuestLocation("Go to Castle Shamino", Ultima1.Spots.SpotCastleOfShamino),
                new QuestLocation("Offer your services to the King", Ultima1.Spots.SpotShaminoKing),
                new QuestLocation("Go to level 9 or 10 of any dungeon", Ultima1.Spots.None),
                new QuestLocation("Kill a Balron", Ultima1.Spots.None),
                new QuestLocation("Go to Castle Shamino", Ultima1.Spots.SpotCastleOfShamino)
            );
            WhiteGem.Postrequisites.Add(new QuestLocation("Talk to the King", Ultima1.Spots.SpotShaminoKing));
            AddMainQuest(iMainOrder++, WhiteGem, quests);

            return quests.ToArray();
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            Ultima1QuestData ultimaData = data as Ultima1QuestData;
            if (ultimaData == null)
                return;

            UltimaPartyInfo party = data.Party as UltimaPartyInfo;
            if (party == null)
                return; // No party, no quests
            Ultima1Character uChar = UltimaCharacter.Create(party.Game, party.Bytes, 0) as Ultima1Character;


            bool bRedGem = false;
            bool bGreenGem = false;
            bool bBlueGem = false;
            bool bWhiteGem = false;
            foreach (UltimaItem item in uChar.Inventory.Items)
            {
                switch ((UltimaItemIndex) item.Index)
                {
                    case UltimaItemIndex.RedGem:
                        bRedGem = true;
                        break;
                    case UltimaItemIndex.GreenGem:
                        bGreenGem = true;
                        break;
                    case UltimaItemIndex.BlueGem:
                        bBlueGem = true;
                        break;
                    case UltimaItemIndex.WhiteGem:
                        bWhiteGem = true;
                        break;
                }
            }

            QuestTotals totals = new QuestTotals(0, 0);
            int iDungeonLevel = data.Map.Index >> 8;
            int iKillCube = uChar.CastleQuest(Ultima1CastleQuests.KillCube);
            int iKillCreeper = uChar.CastleQuest(Ultima1CastleQuests.KillCreeper);
            int iKillLich = uChar.CastleQuest(Ultima1CastleQuests.KillLich);
            int iKillBalron = uChar.CastleQuest(Ultima1CastleQuests.KillBalron);

            // Add information to the QuestStatus objects to indicate their completion status
            RedGem.AddObj(
                iKillCube != 0 || data.Map.Index == (int) Ultima1Map.TheCastleOfTheLostKing,
                iKillCube != 0,
                (iDungeonLevel == 3 || iDungeonLevel == 4) || iKillCube == 1,
                iKillCube == 1,
                iKillCube == 1 && data.Map.Index == (int)Ultima1Map.TheCastleOfTheLostKing
                );
            RedGem.AddPost(bRedGem);
            RedGem.MarkAllWhenComplete = true;
            AddQuest(totals, GreenGem);

            GreenGem.AddObj(
                iKillCreeper != 0 || data.Map.Index == (int)Ultima1Map.TheCastleRondorin,
                iKillCreeper != 0,
                (iDungeonLevel == 5 || iDungeonLevel == 6) || iKillCreeper == 1,
                iKillCreeper == 1,
                iKillCreeper == 1 && data.Map.Index == (int)Ultima1Map.TheCastleRondorin
                );
            GreenGem.AddPost(bGreenGem);
            GreenGem.MarkAllWhenComplete = true;
            AddQuest(totals, GreenGem);

            BlueGem.AddObj(
                iKillLich != 0 || data.Map.Index == (int)Ultima1Map.TheBlackDragonsCastle,
                iKillLich != 0,
                (iDungeonLevel == 7 || iDungeonLevel == 8) || iKillLich == 1,
                iKillLich == 1,
                iKillLich == 1 && data.Map.Index == (int)Ultima1Map.TheBlackDragonsCastle
                );
            BlueGem.AddPost(bBlueGem);
            BlueGem.MarkAllWhenComplete = true;
            AddQuest(totals, BlueGem);

            WhiteGem.AddObj(
                iKillBalron != 0 || data.Map.Index == (int)Ultima1Map.TheCastleOfShamino,
                iKillBalron != 0,
                (iDungeonLevel == 9 || iDungeonLevel == 10) || iKillBalron == 1,
                iKillBalron == 1,
                iKillBalron == 1 && data.Map.Index == (int)Ultima1Map.TheCastleOfShamino
                );
            WhiteGem.AddPost(bWhiteGem);
            WhiteGem.MarkAllWhenComplete = true;
            AddQuest(totals, WhiteGem);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

    }
}
