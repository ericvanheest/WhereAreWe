using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class BT2QuestData : BTQuestData
    {
        public MapBytes CurrentMapBytes;
        public BT2Effects Effects;

        public BT2QuestData(BTPartyInfo party, LocationInformation location, BTGameState state, byte[] mapSpecials, byte[] townMap, MapBytes mapData, BT2Effects effects)
            : base(party, location, state, mapSpecials, townMap)
        {
            CurrentMapBytes = mapData;
            Effects = effects;
        }

        public bool HasDoor(int x, int y, Direction dir)
        {
            if (CurrentMapBytes == null || x >= CurrentMapBytes.Size.Width || y >= CurrentMapBytes.Size.Height)
                return false;
            byte bSquare = CurrentMapBytes.Bytes[y * CurrentMapBytes.Size.Width + x];
            // 00 = open, 01 = wall, 10 = door
            switch (dir)
            {
                case Direction.Left: return (bSquare & 0xC0) == 0x80;
                case Direction.Right: return (bSquare & 0x30) == 0x20;
                case Direction.Down: return (bSquare & 0x0C) == 0x08;
                case Direction.Up: return (bSquare & 0x03) == 0x02;
                default: return false;
            }
        }

        public bool IsActive(Point pt) { return IsActive(pt.X, pt.Y); }

        public bool IsActive(int x, int y)
        {
            int iOffset = y * 22 + x;
            if (MapSpecials == null || MapSpecials.Length <= iOffset)
                return false;
            return MapSpecials[iOffset] > 0 && MapSpecials[iOffset] != 8;   // 8 is just darkness; not really "active"
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            stream.WriteByte(Party.NumChars);
            Global.WriteBytes(stream, CurrentMapBytes.Bytes);
            Global.WriteBytes(stream, Effects.Bytes);
            for (int i = 0; i < 7; i++)
            {
                if (Party.Bytes != null && Party.CharacterSize > 0 && Party.Bytes.Length >= Party.CharacterSize * (i+1))
                {
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT2.Offsets.Class]);
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT2.Offsets.Condition]);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT2.Offsets.Inventory, BT2.Offsets.InventoryLength);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT2.Offsets.Name, BT2.Offsets.NameLength);
                }
            }
        }
    }

    public class BT2Quest : BasicQuest
    {
        public BT2Quest()
        {
        }
    }

    public class BT2QuestInfo : QuestInfo<BT2Quest>
    {
        public QuestStatus RescuePrincess = new QuestStatus(QuestStatus.Basic.NotStarted, "Rescue the Princess");
        public QuestStatus MasterKey = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Master Key");
        public QuestStatus Segment1 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 1");
        public QuestStatus Segment2 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 2");
        public QuestStatus Segment3 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 3");
        public QuestStatus Segment4 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 4");
        public QuestStatus Segment5 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 5");
        public QuestStatus Segment6 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 6");
        public QuestStatus Segment7 = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Scepter Segment 7");
        public QuestStatus LagothZanta = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Lagoth Zanta");
        public QuestStatus Monsters = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat various specific monsters");

        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { RescuePrincess, Segment1, MasterKey, Segment2, Segment3, Segment4,
            Segment5, Segment6, Segment7, LagothZanta, Monsters }; }

        public override BasicQuest[] GetQuests()
        {
            List<BT2Quest> quests = new List<BT2Quest>();

            int iMainIndex = 1;

            RescuePrincess.AddLocations(new QuestLocation("Go to Tangramayne", BT2.Spots.Tangramayne),
                new QuestLocation("Recruit a Bard", BT2.Spots.TangramayneGuild),
                new QuestLocation("Recruit 6 or fewer party members", BT2.Spots.TangramayneGuild),
                new QuestLocation("Enter the Dark Domain", BT2.Spots.DarkDomain),
                new QuestLocation("Descend to level 2", BT2.Spots.DDL1Down),
                new QuestLocation("Recruit the Winged Creature", BT2.Spots.DDL2WingedCreature),
                new QuestLocation("Descend to level 3", BT2.Spots.DDL2Down),
                new QuestLocation("Answer the riddle (Mangar)", BT2.Spots.DDL3RiddleMangar),
                new QuestLocation("Answer the riddle (Pass)", BT2.Spots.DDL3RiddlePass),
                new QuestLocation("Descend to level 4", BT2.Spots.DDL3Down),
                new QuestLocation("Use the teleporter", BT2.Spots.DDL4Teleport),
                new QuestLocation("Bring the winged creature to the chasm", BT2.Spots.DDL4ChasmSouth),
                new QuestLocation("Play the Watchwood Melody", BT2.Spots.DDL4DoorsEast),
                new QuestLocation("Defeat the Dark Lord", BT2.Spots.DDL4DarkLord),
                new QuestLocation("Recruit the Princess", BT2.Spots.DDL4Princess),
                new QuestLocation("Return to the Magician", BT2.Spots.FinishPrincess));
            AddSideQuest(RescuePrincess, quests, "Exp to level 12");

            Segment1.AddLocations(new QuestLocation("Go to Ephesus", BT2.Spots.Ephesus),
                new QuestLocation("Recruit 6 or fewer party members", BT2.Spots.EphesusGuild),
                new QuestLocation("Enter the Temple of Darkness", BT2.Spots.TempleOfDarkness),
                new QuestLocation("Take the teleporter", BT2.Spots.TTL1Teleport1),
                new QuestLocation("Take the teleporter", BT2.Spots.TTL1Teleport2),
                new QuestLocation("Descend to level 2", BT2.Spots.TTL1Down),
                new QuestLocation("Descend to level 3", BT2.Spots.TTL2Down),
                new QuestLocation("Take the teleporter", BT2.Spots.TTL3TeleportSnare),
                new QuestLocation("Have all characters drink from the fountain", BT2.Spots.TTL3Fountain),
                new QuestLocation("Defeat the Toxic Giant", BT2.Spots.TTL3ToxicGiant),
                new QuestLocation("Recruit the Old Warrior", BT2.Spots.TTL3OldWarrior),
                new QuestLocation("Move the Old Warrior to party position 1", BT2.Spots.TTL3CreateDoor),
                new QuestLocation("Give the Torch! to the Old Warrior", BT2.Spots.TTL3Snare));
            Segment1.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 1", BT2.Spots.TTL3Segment1));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment1, quests));

            MasterKey.AddLocations(new QuestLocation("Go to Ephesus", BT2.Spots.Ephesus),
                new QuestLocation("Enter the Temple of Darkness", BT2.Spots.TempleOfDarkness),
                new QuestLocation("Take the teleporter", BT2.Spots.TTL1Teleport1),
                new QuestLocation("Take the teleporter", BT2.Spots.TTL1Teleport2),
                new QuestLocation("Descend to level 2", BT2.Spots.TTL1Down));
            MasterKey.Postrequisites.Add(new QuestLocation("Purchase the Master Key (50000 gold)", BT2.Spots.TTL2KeyMaster));
            BasicQuest bqMasterKey = AddMainQuest(iMainIndex++, MasterKey, quests);
            Segment2.PreQuest.Add(bqMasterKey);
            Segment3.PreQuest.Add(bqMasterKey);
            Segment5.PreQuest.Add(bqMasterKey);

            Segment2.AddLocations(new QuestLocation("Enter Fanskar's Castle", BT2.Spots.FanskarsCastle),
                new QuestLocation("Take the teleporter", BT2.Spots.FCTeleportNorthwest),
                new QuestLocation("Defeat Fanskar", BT2.Spots.FCFanskar),
                new QuestLocation("Take the teleporter", BT2.Spots.FCFanskarTeleport),
                new QuestLocation("Take the teleporter", BT2.Spots.FCSnareTeleport));
            Segment2.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 2", BT2.Spots.FCSegment2));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment2, quests));

            Segment3.AddLocations(new QuestLocation("Go to Philippi", BT2.Spots.Philippi),
                new QuestLocation("Enter Dargoth's Tower", BT2.Spots.DargothsTower),
                new QuestLocation("Take the teleporter", BT2.Spots.DTL1Teleport),
                new QuestLocation("Ascend to level 2", BT2.Spots.DTL1Down),
                new QuestLocation("Take the teleporter", BT2.Spots.DTL2TeleportSoutheast),
                new QuestLocation("Take the teleporter", BT2.Spots.DTL2TeleportSouth),
                new QuestLocation("Ascend to level 3", BT2.Spots.DTL2Up),
                new QuestLocation("Answer the riddle (Earth, Compassed, Fountain)", BT2.Spots.DTL3RiddleEarth),
                new QuestLocation("Ascend to level 4", BT2.Spots.DTL3Up),
                new QuestLocation("Ascend to level 5", BT2.Spots.DTL4Up),
                new QuestLocation("Answer the riddle (see map)", BT2.Spots.DTL5RiddleTenWords),
                new QuestLocation("Ensure all characters have a backpack spot free", BT2.Spots.DTL5RiddleHavok),
                new QuestLocation("Answer the riddle (Havok)", BT2.Spots.DTL5RiddleHavok),
                new QuestLocation("Ensure all characters have the item \"Dagger!\"", BT2.Spots.DTL5CreateDoor));
            Segment3.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 3", BT2.Spots.DTL5Segment3));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment3, quests));

            Segment4.AddLocations(new QuestLocation("Go to Thessalonica", BT2.Spots.Thessalonica),
                new QuestLocation("Enter the Maze of Dread", BT2.Spots.MazeOfDread),
                new QuestLocation("Descend to level 3", BT2.Spots.MDL1Elevator),
                new QuestLocation("Take the teleporter", BT2.Spots.MDL3TeleportSnare),
                new QuestLocation("Answer the riddle (Endurable)", BT2.Spots.MDL3RiddleEndurable),
                new QuestLocation("Take the teleporter", BT2.Spots.MDL3TeleportSegment));
            Segment4.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 4", BT2.Spots.MDL3Segment4));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment4, quests));

            Segment5.AddLocations(new QuestLocation("Talk to Kazdek", BT2.Spots.Kazdek),
                new QuestLocation("Go to Corinth", BT2.Spots.Corinth),
                new QuestLocation("Recruit 4 or fewer party members", BT2.Spots.CorinthGuild),
                new QuestLocation("Enter Oscon's Fortress", BT2.Spots.OsconsFortress),
                new QuestLocation("Take the teleporter", BT2.Spots.OFL1TeleportNortheast),
                new QuestLocation("Take the teleporter", BT2.Spots.OFL1TeleportSouth),
                new QuestLocation("Answer the riddle (Fire, Krill, Silence)", BT2.Spots.OFL1RiddleFire),
                new QuestLocation("Take the teleporter", BT2.Spots.OFL1TeleportSouthwest),
                new QuestLocation("Ascend to level 2", BT2.Spots.OFL1Up),
                new QuestLocation("Answer the riddle (Dervak)", BT2.Spots.OFL2RiddleDervak),
                new QuestLocation("Ascend to level 3", BT2.Spots.OFL2Up),
                new QuestLocation("Answer the riddle (Still)", BT2.Spots.OFL3RiddleStill),
                new QuestLocation("Ascend to level 4", BT2.Spots.OFL3Up1),
                new QuestLocation("Take the teleporter", BT2.Spots.OFL4TeleportSnare),
                new QuestLocation("Recruit Rock", BT2.Spots.OFL4Rock),
                new QuestLocation("Recruit Paper", BT2.Spots.OFL4Paper),
                new QuestLocation("Recruit Scissor", BT2.Spots.OFL4Scissor),
                new QuestLocation("Move Rock, Paper, Scissor to party positions 1-3", BT2.Spots.OFL4RiddleRPS),
                new QuestLocation("Speak to the Magic Mouth", BT2.Spots.OFL4RiddleRPS));
            Segment5.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 5", BT2.Spots.OFL4Segment5));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment5, quests));

            Segment6.AddLocations(new QuestLocation("Enter the Grey Crypt", BT2.Spots.GreyCrypt),
                new QuestLocation("Take the teleporter", BT2.Spots.GCL1TeleportSouth),
                new QuestLocation("Take the teleporter", BT2.Spots.GCL1TeleportNorth),
                new QuestLocation("Answer the riddle (Wize One)", BT2.Spots.GCL1RiddleWizeOne),
                new QuestLocation("Take the teleporter again", BT2.Spots.GCL1TeleportSouth),
                new QuestLocation("Descend to level 2", BT2.Spots.GCL1Down),
                new QuestLocation("Defeat the Vampire Dragon", BT2.Spots.GCL2VampireDragon),
                new QuestLocation("Take the teleporter", BT2.Spots.GCL2TeleportSnare),
                // First pass
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southwest mouth", BT2.Spots.GCL2MouthSouthwest),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe),
                new QuestLocation("Talk to the northwest mouth", BT2.Spots.GCL2MouthNorthwest),
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southeast mouth", BT2.Spots.GCL2MouthSoutheast),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe),
                new QuestLocation("Talk to the northeast mouth", BT2.Spots.GCL2MouthNortheast),
                // Second pass
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southwest mouth", BT2.Spots.GCL2MouthSouthwest),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe),
                new QuestLocation("Talk to the northwest mouth", BT2.Spots.GCL2MouthNorthwest),
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southeast mouth", BT2.Spots.GCL2MouthSoutheast),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe),
                new QuestLocation("Talk to the northeast mouth", BT2.Spots.GCL2MouthNortheast),
                // Third pass
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southwest mouth", BT2.Spots.GCL2MouthSouthwest),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe),
                new QuestLocation("Talk to the northwest mouth", BT2.Spots.GCL2MouthNorthwest),
                new QuestLocation("Talk to the grey-robed mage", BT2.Spots.GCL2GreyRobe),
                new QuestLocation("Talk to the southeast mouth", BT2.Spots.GCL2MouthSoutheast),
                new QuestLocation("Talk to the blue-robed mage", BT2.Spots.GCL2BlueRobe));
            Segment6.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 6", BT2.Spots.GCL2MouthNortheast));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment6, quests));

            Segment7.AddLocations(new QuestLocation("Go to Colosse", BT2.Spots.Corinth),
                new QuestLocation("Answer the riddle (Freeze, Please)", BT2.Spots.DestinyStone),
                new QuestLocation("Take the teleporter", BT2.Spots.DSL1TeleportWest),
                new QuestLocation("Answer the riddle (Near)", BT2.Spots.DSL1RiddleNear),
                new QuestLocation("Descend to level 2", BT2.Spots.DSL1Down),
                new QuestLocation("Defeat D'Artagnon", BT2.Spots.DSL2Dartagnon),
                new QuestLocation("Descend to level 3", BT2.Spots.DSL2Down),
                new QuestLocation("Take the teleporter", BT2.Spots.DSL3TeleportSnare),
                new QuestLocation("Answer the riddle (Storm Fists)", BT2.Spots.DSL3RiddleStormFists),
                new QuestLocation("Answer the riddle (Gale)", BT2.Spots.DSL3RiddleGale),
                new QuestLocation("Go to the end of the maze (first time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (second time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (third time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (fourth time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (fifth time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (sixth time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Go to the end of the maze (seventh time)", BT2.Spots.DSL3MazeGoal),
                new QuestLocation("Answer the riddle (Arkast)", BT2.Spots.DSL3RiddleArkast));
            Segment7.Postrequisites.Add(new QuestLocation("Obtain Scepter Segment 7", BT2.Spots.DSL3Segment7));
            LagothZanta.PreQuest.Add(AddMainQuest(iMainIndex++, Segment7, quests));

            LagothZanta.AddLocations(new QuestLocation("Recruit an Archmage", BT2.Spots.TangramayneGuild),
                new QuestLocation("Give the seven scepter segments to the Archmage", BT2.Spots.TempleOfNarn),
                new QuestLocation("Approach the temple", BT2.Spots.DSL1TeleportWest));
            LagothZanta.Postrequisites.Add(new QuestLocation("Defeat Lagoth Zanta", BT2.Spots.SagesHut));
            AddMainQuest(iMainIndex++, LagothZanta, quests);

            Monsters.AddLocations(new QuestLocation("Defeat Fanskar", BT2.Spots.EncounterFanskar),
                new QuestLocation("Defeat a Bodyguard", BT2.Spots.EncounterBodyguard),
                new QuestLocation("Defeat a Vampire Dragon", BT2.Spots.EncounterVampireDragon),
                new QuestLocation("Defeat Dethadren", BT2.Spots.EncounterDethadren),
                new QuestLocation("Defeat Dartagnon", BT2.Spots.EncounterDartagnon),
                new QuestLocation("Defeat Grandravalk", BT2.Spots.EncounterGrandravalk),
                new QuestLocation("Defeat a Basilisk", BT2.Spots.EncounterBasilisk),
                new QuestLocation("Defeat a Massacre Mage", BT2.Spots.EncounterMassacreMage),
                new QuestLocation("Defeat Fred the Dop", BT2.Spots.EncounterFredTheDop),
                new QuestLocation("Defeat Troy the Dop", BT2.Spots.EncounterTroyTheDop),
                new QuestLocation("Defeat Matt the Dop", BT2.Spots.EncounterMattTheDop),
                new QuestLocation("Defeat Steve the Dop", BT2.Spots.EncounterSteveTheDop),
                new QuestLocation("Defeat Marvin the Dop", BT2.Spots.EncounterMarvinTheDop),
                new QuestLocation("Defeat Oscon", BT2.Spots.EncounterOscon),
                new QuestLocation("Defeat the Dead King", BT2.Spots.EncounterDeadKing),
                new QuestLocation("Defeat an Old Warrior", BT2.Spots.EncounterOldWarrior),
                new QuestLocation("Defeat a Toxic Giant", BT2.Spots.EncounterToxicGiant),
                new QuestLocation("Defeat a Burner", BT2.Spots.EncounterBurner),
                new QuestLocation("Defeat Dargoth", BT2.Spots.EncounterDargoth),
                new QuestLocation("Defeat 2 Web Dragons", BT2.Spots.EncounterWebDragon),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian1),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian2),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian3),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian4),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian5),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian6),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian7),
                new QuestLocation("Defeat a Guardian", BT2.Spots.EncounterGuardian8),
                new QuestLocation("Defeat a Medusa", BT2.Spots.EncounterMedusa1),
                new QuestLocation("Defeat a Medusa", BT2.Spots.EncounterMedusa2),
                new QuestLocation("Defeat a Winged Creature", BT2.Spots.EncounterWingedCreature),
                new QuestLocation("Defeat The Dark Lord", BT2.Spots.EncounterTheDarkLord),
                new QuestLocation("Defeat The Princess", BT2.Spots.EncounterPrincess),
                new QuestLocation("Defeat the Arms Master", BT2.Spots.EncounterArmsMaster),
                new QuestLocation("Defeat the Graphnar Lord", BT2.Spots.EncounterGraphnarLord));
            AddSideQuest(Monsters, quests);

            quests.Sort(CompareBT2Quests);
            return quests.ToArray();
        }

        public int CompareBT2Quests(BT2Quest quest1, BT2Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            BT2QuestData btData = data as BT2QuestData;
            if (btData == null)
                return;

            BTPartyInfo party = data.Party as BTPartyInfo;
            LocationInformation location = data.Location;
            BTGameState state = btData.State as BTGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            BTCharacter btChar = BTCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as BTCharacter;

            if (!(data is BT2QuestData))
                return;

            BT2QuestData questData = data as BT2QuestData;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = btChar.Name;
            CharAddress = iOverrideCharAddress;

            PartyLocation = btData.Location.PrimaryCoordinates;
            int iMainMap = btData.Location.MapIndex & 0xff00;
            int iSubMap = btData.Location.MapIndex & 0x00ff;
            bool bInDungeon = (iSubMap != 0);
            bool bDarkDomain = (iMainMap == (int)BT2Map.Tangramayne && bInDungeon);
            BT2Map map = (BT2Map) location.MapIndex;
            bool bGreyCrypt = (map == BT2Map.GreyCrypt1 || map == BT2Map.GreyCrypt2);
            bool bCastle = (map == BT2Map.FanskarsCastle);
            bool bTombs = (iMainMap == (int)BT2Map.Ephesus && bInDungeon);
            bool bTower = (iMainMap == (int)BT2Map.Philippi && bInDungeon);
            bool bStone = (iMainMap == (int)BT2Map.Colosse && bInDungeon);
            bool bOscon = (iMainMap == (int)BT2Map.Corinth && bInDungeon);
            bool bMaze = (iMainMap == (int)BT2Map.Thessalonica && bInDungeon);
            bool bDarkDomain3 = (bDarkDomain && iSubMap == 3);
            bool bDarkDomain4 = (bDarkDomain && iSubMap > 3);
            bool bWinged = party.CurrentPartyHasCharacter("Winged Creature");
            bool bPrincess = party.CurrentPartyHasCharacter("The Princess");
            bool bDomainTop = bDarkDomain4 && PartyIn(7,20,6,2, 8,16,4,4, 9,14,2,2, 8,10,4,4);
            bool bDomainBottom = bDarkDomain4 && PartyIn(9,7,1,3, 7,5,5,2, 11,3,1,2);
            bool bSeg1 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment1);
            bool bSeg2 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment2);
            bool bSeg3 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment3);
            bool bSeg4 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment4);
            bool bSeg5 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment5);
            bool bSeg6 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment6);
            bool bSeg7 = party.CurrentPartyHasItem(BT2ItemIndex.WandSegment7);

            RescuePrincess.AddObj(
                bPrincess || (iMainMap == (int)BT2Map.Tangramayne),
                bPrincess || (party.CurrentPartyHasClass(GenericClass.Bard)),
                bPrincess || party.NumChars < 7,
                bPrincess || bDarkDomain,
                bPrincess || (bDarkDomain && iSubMap > 1),
                bPrincess || bWinged,
                bPrincess || (bDarkDomain && iSubMap > 2),
                bPrincess || (bDarkDomain4 || (bDarkDomain3 && btData.HasDoor(1, 19, Direction.Right))),
                bPrincess || (bDarkDomain4 || (bDarkDomain3 && !btData.IsActive(1, 18))),
                bPrincess || bDarkDomain4,
                bPrincess || bDomainTop || bDomainBottom,
                bPrincess || (bWinged && bDomainTop),
                bPrincess || btData.Effects.AdventuringSong == 6,
                bPrincess || (bDarkDomain4 && !btData.IsActive(10, 21)),
                bPrincess,
                false); // Completing the last goal resets the quest, so it's never really "complete"
            AddQuest(totals, RescuePrincess);

            bool bTomb1 = map == BT2Map.Tombs1;
            bool bTomb1Section1 = bTomb1 && PartyIn(2,9,14,7, 0,12,2,1, 0,0,1,13, 0,0,4,2, 0,21,2,1, 21,21,1,1, 21,0,1,2);
            bool bTomb1Section3 = bTomb1 && PartyIn(0,17,22,4, 2,21,19,1);
            bool bTomb2 = map == BT2Map.Tombs2;
            bool bTomb3 = map == BT2Map.Tombs3;
            bool bTomb23 = bTomb2 || bTomb3;
            bool bSnare2 = bTomb3 && PartyIn(3,9,2,2, 3,9,10,1, 9,8,3,3, 10,7,1,5, 7,10,2,1);
            bool bWarrior = party.CurrentPartyHasCharacter("Old Warrior");
            bool bWarrior1 = party.CharacterName(party.GetAddress(0)) == "Old Warrior";
            bool bTorchEx = party.CurrentPartyHasItem(BT2ItemIndex.TorchEx);
            bool bScepter = party.CurrentPartyHasItem(BT2ItemIndex.TheScepter);

            Segment1.MarkAllWhenComplete = true;
            Segment1.AddObj(
                iMainMap == (int)BT2Map.Ephesus,
                party.NumChars < 7,
                bTombs,
                bTomb23 || (bTomb1 && !bTomb1Section1),
                bTomb23 || (bTomb1 && bTomb1Section3),
                bTomb23,
                bTomb3,
                bSnare2,
                bTorchEx || party.CurrentPartyHasCondition(BTCondition.Poison, true),
                bTorchEx,
                bWarrior,
                bWarrior1,
                bWarrior1 && party.CharacterHasItem(party.GetAddress(0), BT2ItemIndex.TorchEx),
                false);
            Segment1.AddPost(bSeg1 || bScepter);
            AddQuest(totals, Segment1);

            MasterKey.MarkAllWhenComplete = true;
            MasterKey.AddObj(
                iMainMap == (int)BT2Map.Ephesus,
                bTombs,
                bTomb23 || (bTomb1 && !bTomb1Section1),
                bTomb23 || (bTomb1 && bTomb1Section3),
                bTomb2);
            MasterKey.AddPost(party.CurrentPartyHasItem(BT2ItemIndex.MasterKey));
            AddQuest(totals, MasterKey);

            bool bFanskar = (map == BT2Map.FanskarsCastle);
            bool bFanskarArea1 = bFanskar && PartyIn(5,15,17,7);
            bool bFanskarArea2 = bFanskar && PartyIn(10,6,4,7);
            bool bFanskarArea3 = bFanskar && PartyIn(10,5,4,1);

            Segment2.MarkAllWhenComplete = true;
            Segment2.AddObj(
                bFanskar,
                bFanskarArea1 || bFanskarArea2 || bFanskarArea3,
                bFanskar && !btData.IsActive(20, 21),
                bFanskarArea2 || bFanskarArea3,
                bFanskarArea3);
            Segment2.AddPost(bSeg2 || bScepter);
            AddQuest(totals, Segment2);

            int iDargothLevel = bTower ? iSubMap : 0;
            bool bTower1 = map == BT2Map.DargothsTower1;
            bool bTower2 = map == BT2Map.DargothsTower2;
            bool bTower3 = map == BT2Map.DargothsTower3;
            bool bTower4 = map == BT2Map.DargothsTower4;
            bool bTower5 = map == BT2Map.DargothsTower5;
            bool bDaggers = party.AllCharactersHaveItem(GameNames.BardsTale2, (int)BT2ItemIndex.DaggerEx);

            Segment3.MarkAllWhenComplete = true;
            Segment3.AddObj(
                iMainMap == (int)BT2Map.Philippi,
                bTower,
                iDargothLevel > 1 || (bTower1 && PartyIn(21,17,1,2)),
                iDargothLevel > 1,
                iDargothLevel > 2 || (bTower2 && PartyIn(7,1,8,3, 7,18,8,3)),
                iDargothLevel > 2 || (bTower2 && PartyIn(7,18,8,3)),
                iDargothLevel > 2,
                iDargothLevel > 3 || (bTower3 && btData.IsActive(2,9)),
                iDargothLevel > 3,
                iDargothLevel > 4,
                bDaggers || (bTower5 && PartyIn(20,1,2,8, 18,5,2,2, 0,6,7,1, 0,5,1,1, 0,1,3,3, 0,0,2,1)),
                bDaggers || party.AllCharactersHaveBackpackSpace(),
                party.CurrentPartyHasItem(BT2ItemIndex.DaggerEx),
                bDaggers);
            Segment3.AddPost(bSeg3 || bScepter);
            AddQuest(totals, Segment3);

            int iMazeLevel = bMaze ? iSubMap : 0;
            bool bMaze3 = map == BT2Map.MazeOfDread3;
            bool bMazeScepterRoom = bMaze3 && PartyLocation == BT2.Spots.MDL3Segment4.Location;

            Segment4.MarkAllWhenComplete = true;
            Segment4.AddObj(
                iMainMap == (int)BT2Map.Thessalonica,
                bMaze,
                bMaze3,
                bMazeScepterRoom || (bMaze3 && PartyIn(0,15,10,7)),
                bMazeScepterRoom || (bMaze3 && btData.IsActive(7, 19)),
                bMazeScepterRoom);
            Segment4.AddPost(bSeg4 || bScepter);
            AddQuest(totals, Segment4);

            int iOsconLevel = bOscon ? iSubMap : 0;
            bool bOsconArea1 = (iOsconLevel == 1 && PartyIn(11,7,2,1, 9,5,6,2, 8,3,8,2, 9,1,8,2, 10,0,6,1));
            bool bOsconArea2 = (iOsconLevel == 1 && PartyIn(1,9,10,1, 13,9,9,1, 1,2,21,7, 4,0,15,2));
            bool bOsconArea3 = (iOsconLevel == 1 && PartyIn(2,21,8,1, 2,19,6,2, 1,11,6,8, 7,11,1,6, 8,11,1,4, 9,12,2,1));

            Segment5.MarkAllWhenComplete = true;
            Segment5.AddObj(
                party.CurrentPartyHasItem(BT2ItemIndex.ItemOfK),
                iMainMap == (int)BT2Map.Corinth,
                party.NumChars < 5,
                bOscon,
                iOsconLevel > 1 || bOsconArea1 || bOsconArea2 || bOsconArea3,
                iOsconLevel > 1 || (bOsconArea2 && !bOsconArea1) || bOsconArea3,
                iOsconLevel > 1 || (iOsconLevel == 1 && btData.IsActive(10,12)),
                iOsconLevel > 1 || (bOsconArea3 && btData.IsActive(10, 12)),
                iOsconLevel > 1,
                iOsconLevel > 2 || (iOsconLevel == 2 && btData.IsActive(0, 21)),
                iOsconLevel > 2,
                iOsconLevel > 3 || (iOsconLevel == 3 && btData.IsActive(1, 14)),
                iOsconLevel > 3,
                iOsconLevel == 4 && PartyIn(11,10,1,8, 7,14,9,1, 9,12,5,5),
                party.CurrentPartyHasCharacter("ROCK"),
                party.CurrentPartyHasCharacter("PAPER"),
                party.CurrentPartyHasCharacter("SCISSOR"),
                party.CharacterName(party.GetAddress(0)) == "ROCK" &&
                    party.CharacterName(party.GetAddress(1)) == "PAPER" && 
                    party.CharacterName(party.GetAddress(2)) == "SCISSOR",
                iOsconLevel == 4 && !btData.IsActive(11, 17)
                );
            Segment5.AddPost(bSeg5 || bScepter);
            AddQuest(totals, Segment5);

            bool bGrey1 = map == BT2Map.GreyCrypt1;
            bool bGrey2 = map == BT2Map.GreyCrypt2;
            bool bGreyWize = bGrey1 && btData.IsActive(0, 18);
            bool bGreySnare = bGrey2 && PartyIn(11, 0, 11, 9);
            bool bGreyDoor1 = bGreySnare && btData.HasDoor(14, 4, Direction.Down);
            bool bGreyDoor2 = bGreySnare && btData.HasDoor(18, 4, Direction.Up);

            Segment6.AddObj(
                bGreyCrypt,
                bGrey2 || bGreyWize || (bGrey1 && !PartyIn(0, 0, 22, 7)),
                bGrey2 || bGreyWize || (bGrey1 && PartyIn(0, 7, 22, 5)),
                bGrey2 || bGreyWize,
                bGrey2 || bGreyWize && PartyIn(0, 12, 22, 10),
                bGrey2,
                bGrey2 && !btData.IsActive(1, 5),
                bGreySnare,
                bGreyDoor1 || btData.Effects.Counter1 > 0,
                btData.Effects.Counter1 > 0,
                (bGreyDoor2 && btData.Effects.Counter1 == 1) || btData.Effects.Counter1 > 1,
                btData.Effects.Counter1 > 1,
                (bGreyDoor1 && btData.Effects.Counter1 == 2) || btData.Effects.Counter1 > 2,
                btData.Effects.Counter1 > 2,
                (bGreyDoor2 && btData.Effects.Counter1 == 3) || btData.Effects.Counter1 > 3,
                btData.Effects.Counter1 > 3,
                (bGreyDoor1 && btData.Effects.Counter1 == 4) || btData.Effects.Counter1 > 4,
                btData.Effects.Counter1 > 4,
                (bGreyDoor2 && btData.Effects.Counter1 == 5) || btData.Effects.Counter1 > 5,
                btData.Effects.Counter1 > 5,
                (bGreyDoor1 && btData.Effects.Counter1 == 6) || btData.Effects.Counter1 > 6,
                btData.Effects.Counter1 > 6,
                (bGreyDoor2 && btData.Effects.Counter1 == 7) || btData.Effects.Counter1 > 7,
                btData.Effects.Counter1 > 7,
                (bGreyDoor1 && btData.Effects.Counter1 == 8) || btData.Effects.Counter1 > 8,
                btData.Effects.Counter1 > 8,
                (bGreyDoor2 && btData.Effects.Counter1 == 9) || btData.Effects.Counter1 > 9,
                btData.Effects.Counter1 > 9,
                (bGreyDoor1 && btData.Effects.Counter1 == 10) || btData.Effects.Counter1 > 10,
                btData.Effects.Counter1 > 10,
                (bGreyDoor2 && btData.Effects.Counter1 == 11) || btData.Effects.Counter1 > 11);
            Segment6.AddPost(bSeg6 || bScepter);
            AddQuest(totals, Segment6);

            bool bStone1 = map == BT2Map.DestinyStone1;
            bool bStone2 = map == BT2Map.DestinyStone2;
            bool bStone3 = map == BT2Map.DestinyStone3;
            bool bStone1Area1 = bStone1 && PartyIn(8,16,14,6, 10,14,12,2);
            bool bStone1Area2 = bStone1 && PartyIn(8,0,14,14);
            bool bStoneSnare = bStone3 && PartyIn(0,4,22,11);
            bool bStoneSnare1 = bStone3 && PartyIn(1,4,7,10, 8,4,1,6, 9,4,1,5);
            bool bStoneSnare2 = bStone3 && PartyIn(15,13,1,2, 16,11,1,3);

            Segment7.MarkAllWhenComplete = true;
            Segment7.AddObj(
                iMainMap == (int)BT2Map.Colosse,
                bStone,
                bStone2 || bStone3 || bStone1Area1 || bStone1Area2,
                bStone2 || bStone3 || bStone1Area2,
                bStone2 || bStone3,
                bStone3 || (bStone2 || btData.IsActive(18,3)),
                bStone3,
                bStoneSnare,
                btData.Effects.Counter1 > 0 || (bStoneSnare && !bStoneSnare1),
                bStoneSnare2 || party.CurrentPartyHasItem(BT2ItemIndex.TheRing),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 0),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 1),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 2),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 3),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 4),
                bStoneSnare2 || (bStoneSnare && btData.Effects.Counter1 > 5),
                bStoneSnare2,
                bStoneSnare2 && btData.HasDoor(15, 13, Direction.Up));
            Segment7.AddPost(bSeg7 || bScepter);
            AddQuest(totals, Segment7);

            int iArchmage = party.CurrentPartyClassIndex(GenericClass.Archmage);

            LagothZanta.MarkAllWhenComplete = true;
            LagothZanta.AddObj(
                iArchmage != -1,
                iArchmage != -1 && (bScepter || party.CharacterHasItems(GameNames.BardsTale2, party.GetAddress(iArchmage),
                    (int)BT2ItemIndex.WandSegment1, (int)BT2ItemIndex.WandSegment2, (int)BT2ItemIndex.WandSegment3, (int)BT2ItemIndex.WandSegment4,
                    (int)BT2ItemIndex.WandSegment5, (int)BT2ItemIndex.WandSegment6, (int)BT2ItemIndex.WandSegment7)),
                bScepter);
            LagothZanta.AddPost(party.AnyCharacterHasCondition(BTCondition.FinishGame));
            AddQuest(totals, LagothZanta);

            Monsters.Main = QuestStatus.Basic.Accepted;
            Monsters.AddObj(
                IsInactive(btData, map, BT2.Spots.EncounterFanskar),
                IsInactive(btData, map, BT2.Spots.EncounterBodyguard),
                IsInactive(btData, map, BT2.Spots.EncounterVampireDragon),
                IsInactive(btData, map, BT2.Spots.EncounterDethadren),
                IsInactive(btData, map, BT2.Spots.EncounterDartagnon),
                IsInactive(btData, map, BT2.Spots.EncounterGrandravalk),
                IsInactive(btData, map, BT2.Spots.EncounterBasilisk),
                IsInactive(btData, map, BT2.Spots.EncounterMassacreMage),
                IsInactive(btData, map, BT2.Spots.EncounterFredTheDop),
                IsInactive(btData, map, BT2.Spots.EncounterTroyTheDop),
                IsInactive(btData, map, BT2.Spots.EncounterMattTheDop),
                IsInactive(btData, map, BT2.Spots.EncounterSteveTheDop),
                IsInactive(btData, map, BT2.Spots.EncounterMarvinTheDop),
                IsInactive(btData, map, BT2.Spots.EncounterOscon),
                IsInactive(btData, map, BT2.Spots.EncounterDeadKing),
                IsInactive(btData, map, BT2.Spots.EncounterOldWarrior),
                IsInactive(btData, map, BT2.Spots.EncounterToxicGiant),
                IsInactive(btData, map, BT2.Spots.EncounterBurner),
                IsInactive(btData, map, BT2.Spots.EncounterDargoth),
                IsInactive(btData, map, BT2.Spots.EncounterWebDragon),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian1),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian2),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian3),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian4),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian5),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian6),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian7),
                IsInactive(btData, map, BT2.Spots.EncounterGuardian8),
                IsInactive(btData, map, BT2.Spots.EncounterMedusa1),
                IsInactive(btData, map, BT2.Spots.EncounterMedusa2),
                IsInactive(btData, map, BT2.Spots.EncounterWingedCreature),
                IsInactive(btData, map, BT2.Spots.EncounterTheDarkLord),
                IsInactive(btData, map, BT2.Spots.EncounterPrincess),
                IsInactive(btData, map, BT2.Spots.EncounterArmsMaster),
                IsInactive(btData, map, BT2.Spots.EncounterGraphnarLord));
            AddQuest(totals, Monsters);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool IsActive(BT2QuestData data, BT2Map map, MapXY spot) { return (int)map == spot.Map && data.IsActive(spot.Location); }
        private bool IsInactive(BT2QuestData data, BT2Map map, MapXY spot) { return (int)map == spot.Map && !data.IsActive(spot.Location); }
    }
}