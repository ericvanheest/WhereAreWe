using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class BT1QuestData : BTQuestData
    {
        public BT1QuestData(BTPartyInfo party, LocationInformation location, BTGameState state, byte[] mapSpecials, byte[] townMap)
            : base(party, location, state, mapSpecials, townMap)
        {
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
        }
    }

    public class BT1Quest : BasicQuest
    {
        public BT1Quest()
        {
        }
    }

    public class BT1QuestInfo : QuestInfo<BT1Quest>
    {
        public QuestStatus MadGod = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn the name of the Mad God");
        public QuestStatus EyeOfTarjan = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Eye of Tarjan");
        public QuestStatus CrystalSword = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Crystal Sword");
        public QuestStatus SilverSquare = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Silver Square");
        public QuestStatus KylearansTower = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to Kylearan's Tower");
        public QuestStatus SilverTriangle = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Silver Triangle and Onyx Key");
        public QuestStatus MangarsTower = new QuestStatus(QuestStatus.Basic.NotStarted, "Gain access to Mangar's Tower");
        public QuestStatus DefeatMangar = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Mangar");
        public QuestStatus Monsters = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat various specific monsters");
        public QuestStatus Guardians = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Town Guardians");

        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { MadGod, EyeOfTarjan, CrystalSword, SilverSquare, KylearansTower,
            SilverTriangle, MangarsTower, DefeatMangar, Monsters, Guardians }; }

        public override BasicQuest[] GetQuests()
        {
            List<BT1Quest> quests = new List<BT1Quest>();

            int iMainIndex = 1;

            MadGod.AddLocations(new QuestLocation("Order wine", BT1.Spots.ScarletBard),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownC),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownSL1),
                new QuestLocation("Learn the name of the Mad God", BT1.Spots.Tarjan));
            AddSideQuest(MadGod, quests);

            EyeOfTarjan.AddLocations(new QuestLocation("Enter the Temple of the Mad God (\"Tarjan\")", BT1.Spots.MadGod1),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownCL1),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownCL2),
                new QuestLocation("Take the teleporter", BT1.Spots.CL3Teleport1));
            EyeOfTarjan.Postrequisites.Add(new QuestLocation("Defeat the Spectre and obtain the Eye", BT1.Spots.FECL3Spectre));
            KylearansTower.PreQuest.Add(AddMainQuest(iMainIndex++, EyeOfTarjan, quests));

            CrystalSword.AddLocations(new QuestLocation("Enter Harkyn's Castle", BT1.Spots.HarkynsCastle),
                new QuestLocation("Defeat the Jabberwock", BT1.Spots.FEHCL1Jabberwock));
            CrystalSword.Postrequisites.Add(new QuestLocation("Obtain the Crystal Sword", BT1.Spots.CrystalSword));
            SilverTriangle.PreQuest.Add(AddMainQuest(iMainIndex++, CrystalSword, quests));

            SilverSquare.AddLocations(new QuestLocation("Enter Harkyn's Castle", BT1.Spots.HarkynsCastle),
                new QuestLocation("Ascend the stairs", BT1.Spots.StairsUpHCL1),
                new QuestLocation("Take the teleporter", BT1.Spots.HCL2Teleport1));
            SilverSquare.Postrequisites.Add(new QuestLocation("Obtain Silver Square", BT1.Spots.SilverSquare));
            DefeatMangar.PreQuest.Add(AddMainQuest(iMainIndex++, SilverSquare, quests));

            KylearansTower.AddLocations(new QuestLocation("Enter Harkyn's Castle", BT1.Spots.HarkynsCastle),
                new QuestLocation("Ascend the stairs", BT1.Spots.StairsUpHCL1),
                new QuestLocation("Ascend the portal", BT1.Spots.AscendHCL2),
                new QuestLocation("Take the teleporter", BT1.Spots.HCL3Teleport1),
                new QuestLocation("Take the teleporter", BT1.Spots.HCL3Teleport2));
            KylearansTower.Postrequisites.Add(new QuestLocation("Approach the statue", BT1.Spots.HCL3MadGodStatue));
            AddMainQuest(iMainIndex++, KylearansTower, quests);

            SilverTriangle.AddLocations(new QuestLocation("Enter tower", BT1.Spots.KylearanTower),
                new QuestLocation("Take the teleporter", BT1.Spots.KTTeleport1),
                new QuestLocation("Take the teleporter", BT1.Spots.KTTeleport2),
                new QuestLocation("Answer the riddle (\"Stone Golem\")", BT1.Spots.KTRiddle1),
                new QuestLocation("Answer the riddle (\"Sinister\")", BT1.Spots.KTRiddle2),
                new QuestLocation("Defeat the Green Dragons", BT1.Spots.FEKTGreenDragon6),
                new QuestLocation("Obtain the Silver Triangle", BT1.Spots.SilverTriangle),
                new QuestLocation("Defeat the Crystal Golem", BT1.Spots.FEKTCrystalGolem),
                new QuestLocation("Take the teleporter", BT1.Spots.KTTeleport3),
                new QuestLocation("Meet with Kylearan", BT1.Spots.MeetKylearan));
            SilverTriangle.Postrequisites.Add(new QuestLocation("Exit the tower", BT1.Spots.ExitKylearan));
            DefeatMangar.PreQuest.Add(AddMainQuest(iMainIndex++, SilverTriangle, quests));

            MangarsTower.AddLocations(new QuestLocation("Order wine", BT1.Spots.ScarletBard),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownC),
                new QuestLocation("Descend the stairs", BT1.Spots.StairsDownSL1),
                new QuestLocation("Descend the portal", BT1.Spots.DescendSL2b),
                new QuestLocation("Equip the onyx key", BT1.Spots.None));
            MangarsTower.Postrequisites.Add(new QuestLocation("Ascend the stairs", BT1.Spots.StairsUpSL3));
            AddMainQuest(iMainIndex++, MangarsTower, quests);

            DefeatMangar.AddLocations(new QuestLocation("Enter Mangar's Tower", BT1.Spots.MangarsTower),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL1Teleport1),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL1Teleport2),
                new QuestLocation("Talk to the mouth", BT1.Spots.MTL1TeleportUp),
                new QuestLocation("Answer the riddle (\"Circle\")", BT1.Spots.SilverCircle),
                new QuestLocation("Ascend the stairs", BT1.Spots.StairsUpMTL2),
                new QuestLocation("Obtain the Master Key", BT1.Spots.MasterKey),
                new QuestLocation("Find the first word", BT1.Spots.SevenFirst),
                new QuestLocation("Find the second word", BT1.Spots.SevenSecond),
                new QuestLocation("Find the third word", BT1.Spots.SevenThird),
                new QuestLocation("Find the fourth word", BT1.Spots.SevenFourth),
                new QuestLocation("Find the fifth word", BT1.Spots.SevenFifth),
                new QuestLocation("Find the sixth word", BT1.Spots.SevenSixth),
                new QuestLocation("Find the seventh word", BT1.Spots.SevenSeventh),
                new QuestLocation("Talk to the Mouth", BT1.Spots.SpeakTheSeven),
                new QuestLocation("Ascend the stairs", BT1.Spots.StairsUpMTL3),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL4Teleport1),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL4Teleport2),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL4Teleport3),
                new QuestLocation("Swap walls and doors", BT1.Spots.SwapDoors),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL4Teleport4),
                new QuestLocation("Ascend the portal", BT1.Spots.StairsDownMTL4),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL5Teleport1),
                new QuestLocation("Take the teleporter", BT1.Spots.MTL5Teleport2),
                new QuestLocation("Dive into the pool", BT1.Spots.MTL5Teleport3));
            DefeatMangar.Postrequisites.Add(new QuestLocation("Defeat Mangar", BT1.Spots.Mangar));
            AddMainQuest(iMainIndex++, DefeatMangar, quests);

            Guardians.AddLocations(new QuestLocation("Defeat the Samurai", BT1.Spots.FESBSamurai),
                new QuestLocation("Defeat the Stone Golem", BT1.Spots.FESBStoneGolem1),
                new QuestLocation("Defeat the Stone Golem", BT1.Spots.FESBStoneGolem2),
                new QuestLocation("Defeat the Stone Giant", BT1.Spots.FESBStoneGiant1),
                new QuestLocation("Defeat the Stone Giant", BT1.Spots.FESBStoneGiant2),
                new QuestLocation("Defeat the Stone Giant", BT1.Spots.FESBStoneGiant3),
                new QuestLocation("Defeat the Ogre Lord", BT1.Spots.FESBOgreLord1),
                new QuestLocation("Defeat the Ogre Lord", BT1.Spots.FESBOgreLord2),
                new QuestLocation("Defeat the Ogre Lord", BT1.Spots.FESBOgreLord3),
                new QuestLocation("Defeat the Grey Dragon", BT1.Spots.FESBGreyDragon));
            AddSideQuest(Guardians, quests);

            Monsters.AddLocations(new QuestLocation("Defeat 6 Spiders", BT1.Spots.FESL1Spider6),
                new QuestLocation("Defeat 5 Spiders", BT1.Spots.FESL1Spider5),
                new QuestLocation("Defeat 7 Spiders", BT1.Spots.FESL1Spider7),
                new QuestLocation("Defeat 3 Black Widows", BT1.Spots.FESL1BlackWidow3a),
                new QuestLocation("Defeat a Spinner", BT1.Spots.FESL1Spinner),
                new QuestLocation("Defeat 3 Black Widows", BT1.Spots.FESL1BlackWidow3b),
                new QuestLocation("Defeat 4 Black Widows", BT1.Spots.FESL1BlackWidow4),
                new QuestLocation("Defeat 5 Black Widows", BT1.Spots.FESL1BlackWidow5),
                new QuestLocation("Defeat 13 Spiders", BT1.Spots.FESL2Spider13),
                new QuestLocation("Defeat 7 Black Widows", BT1.Spots.FESL2BlackWidow7a),
                new QuestLocation("Defeat 6 Black Widows", BT1.Spots.FESL2BlackWidow6a),
                new QuestLocation("Defeat 16 Spiders", BT1.Spots.FESL2Spider16),
                new QuestLocation("Defeat 6 Black Widows", BT1.Spots.FESL2BlackWidow6b),
                new QuestLocation("Defeat 10 Spiders", BT1.Spots.FESL2Spider10),
                new QuestLocation("Defeat 7 Black Widows", BT1.Spots.FESL2BlackWidow7b),
                new QuestLocation("Defeat 9 Spiders", BT1.Spots.FESL2Spider9),
                new QuestLocation("Defeat 3 Spinners", BT1.Spots.FESL3Spinner3a),
                new QuestLocation("Defeat 7 Black Widows", BT1.Spots.FESL3BlackWidow7),
                new QuestLocation("Defeat 4 Spinners", BT1.Spots.FESL3Spinner4a),
                new QuestLocation("Defeat 5 Black Widows", BT1.Spots.FESL3BlackWidow5),
                new QuestLocation("Defeat 2 Spinners", BT1.Spots.FESL3Spinner2),
                new QuestLocation("Defeat 4 Spinners", BT1.Spots.FESL3Spinner4b),
                new QuestLocation("Defeat 8 Black Widows", BT1.Spots.FESL3BlackWidow8),
                new QuestLocation("Defeat 3 Spinners", BT1.Spots.FESL3Spinner3b),
                new QuestLocation("Defeat 11 Wights", BT1.Spots.FECL1Wight11),
                new QuestLocation("Defeat 66 Skeletons", BT1.Spots.FECL1Skeleton66),
                new QuestLocation("Defeat 45 Zombies", BT1.Spots.FECL1Zombie45),
                new QuestLocation("Defeat 28 Zombies", BT1.Spots.FECL1Zombie28),
                new QuestLocation("Defeat 39 Skeletons", BT1.Spots.FECL1Skeleton39),
                new QuestLocation("Defeat 52 Skeletons", BT1.Spots.FECL1Skeleton52),
                new QuestLocation("Defeat 9 Wights", BT1.Spots.FECL1Wight9),
                new QuestLocation("Defeat 5 Ghouls", BT1.Spots.FECL1Ghoul5),
                new QuestLocation("Defeat a Grey Dragon", BT1.Spots.FECL2GreyDragon),
                new QuestLocation("Defeat a Master Sorcerer", BT1.Spots.FECL2MasterSorcerer),
                new QuestLocation("Defeat 49 Wights", BT1.Spots.FECL2Wight49),
                new QuestLocation("Defeat a Soul Sucker", BT1.Spots.FECL2SoulSucker),
                new QuestLocation("Defeat 8 Wraiths", BT1.Spots.FECL3Wraith8),
                new QuestLocation("Defeat an Ogre", BT1.Spots.FECL3Ogre),
                new QuestLocation("Defeat a Spectre", BT1.Spots.FECL3Spectre),
                new QuestLocation("Defeat 53 Zombies", BT1.Spots.FECL3Zombie53),
                new QuestLocation("Defeat 66 Zombies", BT1.Spots.FECL3Zombie66),
                new QuestLocation("Defeat 99 Skeletons", BT1.Spots.FECL3Skeleton99a),
                new QuestLocation("Defeat 99 Skeletons", BT1.Spots.FECL3Skeleton99b),
                new QuestLocation("Defeat 69 Wights", BT1.Spots.FECL3Wight69),
                new QuestLocation("Defeat 36 Ghouls", BT1.Spots.FECL3Ghoul36),
                new QuestLocation("Defeat 7 Wraiths", BT1.Spots.FECL3Wraith7),
                new QuestLocation("Defeat a Master Ninja", BT1.Spots.FEHCL1MasterNinja),
                new QuestLocation("Defeat 3 Wights", BT1.Spots.FEHCL1Wight3),
                new QuestLocation("Defeat a Golem", BT1.Spots.FEHCL1Golem),
                new QuestLocation("Defeat a Golem", BT1.Spots.FEHCL1Golema),
                new QuestLocation("Defeat a Golem", BT1.Spots.FEHCL1Golemb),
                new QuestLocation("Defeat a Golem", BT1.Spots.FEHCL1Golemc),
                new QuestLocation("Defeat 6 Berserkers", BT1.Spots.FEHCL1Berserker6),
                new QuestLocation("Defeat a Jabberwock", BT1.Spots.FEHCL1Jabberwock),
                new QuestLocation("Defeat a Master Sorcerer", BT1.Spots.FEHCL2MasterSorcerer),
                new QuestLocation("Defeat 8 Mangar Guards", BT1.Spots.FEHCL2MangarGuard8),
                new QuestLocation("Defeat 4 Lesser Demons", BT1.Spots.FEHCL3OldManLesserDemon4),
                new QuestLocation("Defeat 396 Berserkers", BT1.Spots.FEHCL3Berserker396),
                new QuestLocation("Defeat 6 Green Dragons", BT1.Spots.FEKTGreenDragon6),
                new QuestLocation("Defeat a Crystal Golem", BT1.Spots.FEKTCrystalGolem),
                new QuestLocation("Defeat 3 Maze Masters", BT1.Spots.FEMTL1MazeMaster3),
                new QuestLocation("Defeat 56 Dwarves", BT1.Spots.FEMTL1Dwarf56),
                new QuestLocation("Defeat 34 Evil Eyes", BT1.Spots.FEMTL1EvilEye34),
                new QuestLocation("Defeat 2 Master Magicians", BT1.Spots.FEMTL1MasterMagician2),
                new QuestLocation("Defeat 20 Ghosts", BT1.Spots.FEMTL1Ghost20),
                new QuestLocation("Defeat 96 Samurai", BT1.Spots.FEMTL1Samurai96),
                new QuestLocation("Defeat 68 Mercenaries", BT1.Spots.FEMTL1Mercenary68),
                new QuestLocation("Defeat 97 Hobbits", BT1.Spots.FEMTL2Hobbit97),
                new QuestLocation("Defeat 3 Basilisks", BT1.Spots.FEMTL2Basilisk3),
                new QuestLocation("Defeat a Mind Shadow", BT1.Spots.FEMTL2MindShadow),
                new QuestLocation("Defeat 2 Bandersnatches", BT1.Spots.FEMTL2Bandersnatch2),
                new QuestLocation("Defeat a Soul Sucker", BT1.Spots.FEMTL2SoulSucker),
                new QuestLocation("Defeat 67 Samurai", BT1.Spots.FEMTL2Samurai67),
                new QuestLocation("Defeat 32 Ghouls", BT1.Spots.FEMTL2Ghoul32),
                new QuestLocation("Defeat 7 Master Ninjas", BT1.Spots.FEMTL3MasterNinja7),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongoa),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongob),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongoc),
                new QuestLocation("Defeat 6 Wraiths", BT1.Spots.FEMTL3Wraith6OldMan),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongod),
                new QuestLocation("Defeat 7 Level 6 Conjurers", BT1.Spots.FEMTL3Lv6Conjurer7),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongoe),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongof),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongog),
                new QuestLocation("Defeat a Mongo", BT1.Spots.FEMTL3Mongoh),
                new QuestLocation("Defeat 50 Ghouls", BT1.Spots.FEMTL4Ghoul50),
                new QuestLocation("Defeat 9 Wraiths", BT1.Spots.FEMTL4Wraith9),
                new QuestLocation("Defeat 7 Lesser Demons", BT1.Spots.FEMTL4LesserDemon7),
                new QuestLocation("Defeat a Vampire Lord", BT1.Spots.FEMTL4VampireLord),
                new QuestLocation("Defeat 2 Red Dragons", BT1.Spots.FEMTL4RedDragon2),
                new QuestLocation("Defeat 4 Mercenaries", BT1.Spots.FEMTL4Mercenary4),
                new QuestLocation("Defeat a Spectre", BT1.Spots.FEMTL4Spectre),
                new QuestLocation("Defeat 6 Vampires", BT1.Spots.FEMTL4Vampire6),
                new QuestLocation("Defeat 2 Black Dragons", BT1.Spots.FEMTL5BlackDragon2),
                new QuestLocation("Defeat an Archmage", BT1.Spots.FEMTL5Archmage),
                new QuestLocation("Defeat 4 Spectres", BT1.Spots.FEMTL5Spectre4),
                new QuestLocation("Defeat 4 Balrogs", BT1.Spots.FEMTL5Balrog4),
                new QuestLocation("Defeat 3 Ancient Enemies", BT1.Spots.FEMTL5AncientEnemy3),
                new QuestLocation("Defeat 5 Storm Giants", BT1.Spots.FEMTL5StormGiant5));
            AddSideQuest(Monsters, quests);

            quests.Sort(CompareBT1Quests);
            return quests.ToArray();
        }

        public int CompareBT1Quests(BT1Quest quest1, BT1Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            BT1QuestData btData = data as BT1QuestData;
            if (btData == null)
                return;

            BTPartyInfo party = data.Party as BTPartyInfo;
            LocationInformation location = data.Location;
            BTGameState state = btData.State as BTGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            BTCharacter btChar = BTCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as BTCharacter;

            if (!(data is BT1QuestData))
                return;

            BT1QuestData questData = data as BT1QuestData;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = btChar.Name;
            CharAddress = iOverrideCharAddress;

            Point ptParty = btData.Location.PrimaryCoordinates;
            int iMainMap = btData.Location.MapIndex & 0xff00;
            int iSubMap = btData.Location.MapIndex & 0x00ff;
            int iMap = location.MapIndex;
            bool bEye = party.CurrentPartyHasItem(BT1ItemIndex.Eye);
            bool bSquare = party.CurrentPartyHasItem(BT1ItemIndex.SilverSquare);
            bool bCircle = party.CurrentPartyHasItem(BT1ItemIndex.SilverCircle);
            bool bTriangle = party.CurrentPartyHasItem(BT1ItemIndex.SilverTriangle);
            bool bSword = party.CurrentPartyHasItem(BT1ItemIndex.CrystalSword);
            bool bOnyx = party.CurrentPartyHasItem(BT1ItemIndex.OnyxKey);
            bool bThor = party.CurrentPartyHasItem(BT1ItemIndex.ThorFigurine);
            bool bMaster = party.CurrentPartyHasItem(BT1ItemIndex.MasterKey);
            bool bSkaraBrae = (iMainMap == ((int)BT1Map.SkaraBrae & 0xff00));
            bool bInMangars = (iMainMap == ((int)BT1Map.Mangar1 & 0xff00));
            int iMangars = bInMangars ? iSubMap : 0;
            bool bInKylearans = (iMainMap == ((int)BT1Map.Kylearan & 0xff00));
            int iKylearans = bInKylearans ? iSubMap : 0;
            bool bInHarkyns = (iMainMap == ((int)BT1Map.Harkyn1 & 0xff00));
            int iHarkyns = bInHarkyns ? iSubMap : 0;
            bool bInCatacombs = (iMainMap == ((int)BT1Map.Catacombs1 & 0xff00));
            int iCatacombs = bInCatacombs ? iSubMap : 0;
            bool bInSewers = (iMainMap == ((int)BT1Map.Sewers1 & 0xff00));
            int iSewers = bInSewers ? iSubMap : 0;

            MadGod.MarkAllWhenComplete = true;
            MadGod.AddObj(iSewers > 0, iSewers > 1, iSewers > 2, false);
            MadGod.AddPost(bEye || bInKylearans || bInHarkyns || bOnyx);
            AddQuest(totals, MadGod);

            EyeOfTarjan.MarkAllWhenComplete = true;
            EyeOfTarjan.AddObj(bInCatacombs, iCatacombs > 1, iCatacombs > 2,
                iCatacombs > 2 && Global.PointInRects(ptParty, new Rectangle(17, 13, 4, 6), new Rectangle(19, 19, 2, 2)));
            EyeOfTarjan.AddPost(bEye || bInKylearans || bInHarkyns || bOnyx);
            AddQuest(totals, EyeOfTarjan);

            CrystalSword.MarkAllWhenComplete = true;
            CrystalSword.AddObj(bInHarkyns, iHarkyns == 1 && btData.MapSpecial(BT1.Spots.FEHCL1Jabberwock) == 0);
            CrystalSword.AddPost(bSword);
            AddQuest(totals, CrystalSword);

            SilverSquare.MarkAllWhenComplete = true;
            SilverSquare.AddObj(iHarkyns > 0, iHarkyns > 1, 
                iHarkyns == 2 && Global.PointInRects(ptParty, new Rectangle(0,0,2,1), new Rectangle(0,1,1,1)));
            SilverSquare.AddPost(bSquare);
            AddQuest(totals, SilverSquare);

            bool bHarkynTeleport2 = Global.PointInRects(ptParty, new Rectangle(12, 0, 10, 9), new Rectangle(10, 5, 2, 4), new Rectangle(15, 9, 2, 2),
                new Rectangle(18, 9, 2, 2), new Rectangle(21, 9, 1, 4), new Rectangle(20, 12, 1, 1));
            bool bInKylearanGates = bSkaraBrae && Global.PointInRects(ptParty, new Rectangle(26, 26, 3, 3));
            KylearansTower.MarkAllWhenComplete = true;
            KylearansTower.AddObj(bInHarkyns, iHarkyns > 1, iHarkyns > 2,
                iHarkyns == 3 && (bHarkynTeleport2 || Global.PointInRects(ptParty, new Rectangle(10,20,1,1))),
                iHarkyns == 3 && bHarkynTeleport2);
            KylearansTower.AddPost(bOnyx || bMaster || bInKylearans || bInMangars || bInKylearanGates);
            AddQuest(totals, KylearansTower);

            bool bInKylearan1 = bInKylearans && Global.PointInRects(ptParty, new Rectangle(0, 0, 2, 4), new Rectangle(0, 21, 1, 1), new Rectangle(2, 0, 2, 2), new Rectangle(19, 0, 3, 2));
            bool bInKylearan2 = bInKylearans && Global.PointInRects(ptParty, new Rectangle(19, 19, 2, 3), new Rectangle(21, 21, 1, 1));
            bool bInKylearan3 = bInKylearans && Global.PointInRects(ptParty, new Rectangle(8, 9, 7, 7), new Rectangle(9, 16, 4, 2), new Rectangle(6, 10, 2, 4), new Rectangle(15, 11, 2, 4), new Rectangle(10, 7, 4, 2));
            bool bInKylearan5 = bInKylearans && Global.PointInRects(ptParty, new Rectangle(4, 0, 2, 2), new Rectangle(3, 20, 3, 2), new Rectangle(1, 21, 2, 1), new Rectangle(1, 18, 1, 3), new Rectangle(1, 4, 1, 1), new Rectangle(18, 2, 4, 17), new Rectangle(18, 0, 1, 2), new Rectangle(15, 1, 5, 3), new Rectangle(14, 16, 4, 3), new Rectangle(15, 15, 3, 1));
            bool bInKylearan6 = bInKylearans && Global.PointInRects(ptParty, new Rectangle(14, 1, 1, 8), new Rectangle(15, 3, 1, 8), new Rectangle(16, 4, 1, 7), new Rectangle(17, 4, 1, 11));
            bool bInKylearan4 = bInKylearans && !(bInKylearan1 || bInKylearan2 || bInKylearan3 || bInKylearan5 || bInKylearan6);
            bool bSinister = bInKylearans && btData.MapSpecial(BT1.Spots.KTRiddle2) == 0;
            bool bGreenDragons = bInKylearans && btData.MapSpecial(BT1.Spots.FEKTGreenDragon6) == 0;
            bool bGolem = bInKylearans && btData.MapSpecial(BT1.Spots.FEKTCrystalGolem) == 0;

            SilverTriangle.MarkAllWhenComplete = true;
            SilverTriangle.AddObj(bInKylearans,
                bInKylearans && !bInKylearan1,
                bInKylearans && !(bInKylearan2 || bInKylearan3),
                bInKylearan4 || bInKylearan5 || bInKylearan6,
                bSinister || bTriangle, bGreenDragons || bTriangle, bTriangle, bGolem || bOnyx, bInKylearan6 || bOnyx, bOnyx);
            SilverTriangle.AddPost(bTriangle && bOnyx);
            AddQuest(totals, SilverTriangle);

            bool bMangarInSB = bSkaraBrae && Global.PointInRects(ptParty, new Rectangle(1, 1, 3, 3));
            bool bOnyxEquipped = party.CurrentPartyHasEquipped(BT1ItemIndex.OnyxKey);
            MangarsTower.MarkAllWhenComplete = true;
            MangarsTower.AddObj(iSewers > 0, iSewers > 1, iSewers > 2, iSewers > 3, bOnyxEquipped);
            MangarsTower.AddPost(bMaster || bInMangars || (bOnyxEquipped && bMangarInSB));
            AddQuest(totals, MangarsTower);

            bool bM1Area1 = (iMangars == 1) && Global.PointInRects(ptParty, new Rectangle(0, 0, 8, 7));
            bool bM1Area2 = (iMangars == 1) && Global.PointInRects(ptParty, new Rectangle(0, 14, 8, 6), new Rectangle(3, 13, 2, 1), new Rectangle(0, 20, 2, 1), new Rectangle(6, 20, 2, 1));
            bool bM1Area3 = (iMangars == 1) && !(bM1Area1 || bM1Area2);
            bool bM4Area1 = (iMangars == 4) && Global.PointInRects(ptParty, new Rectangle(5, 6, 7, 5), new Rectangle(3, 8, 2, 2), new Rectangle(6, 4, 1, 9), 
                new Rectangle(8, 4, 1, 9), new Rectangle(10, 4, 1, 9), new Rectangle(10, 12, 8, 1), new Rectangle(17, 10, 1, 3), new Rectangle(5, 10, 9, 1),
                new Rectangle(5, 8, 9, 1), new Rectangle(5, 6, 9, 1));
            bool bM4Area2 = (iMangars == 4) && Global.PointInRects(ptParty, new Rectangle(15, 3, 4, 1), new Rectangle(18, 3, 1, 5), new Rectangle(16, 6, 6, 2),
                new Rectangle(21, 6, 1, 5), new Rectangle(20, 9, 2, 1), new Rectangle(0, 5, 2, 2));
            bool bM4Area3 = (iMangars == 4) && Global.PointInRects(ptParty, new Rectangle(20, 18, 1, 3), new Rectangle(8, 20, 13, 1), new Rectangle(13, 14, 1, 7),
                new Rectangle(11, 14, 5, 4), new Rectangle(8, 15, 1, 6), new Rectangle(2, 16, 7, 2), new Rectangle(2, 3, 1, 15), new Rectangle(0, 3, 2, 2));
            bool bM4Area4 = (iMangars == 4) && Global.PointInRects(ptParty, new Rectangle(2, 20, 2, 1));
            bool bM4Area5 = (iMangars == 4) && !Global.PointInRects(ptParty, new Rectangle(0, 19, 5, 3));
            bool bM5Area1 = (iMangars == 5) && Global.PointInRects(ptParty, new Rectangle(0, 0, 3, 1));
            bool bM5Area2 = (iMangars == 5) && Global.PointInRects(ptParty, new Rectangle(0, 1, 6, 6), new Rectangle(3, 0, 4, 4), new Rectangle(7, 0, 15, 2));
            bool bM5Area3 = (iMangars == 5) && Global.PointInRects(ptParty, new Rectangle(0, 7, 5, 10), new Rectangle(5, 7, 1, 9), new Rectangle(6, 4, 1, 4),
                new Rectangle(7, 2, 15, 3), new Rectangle(16, 2, 6, 20), new Rectangle(14, 5, 2, 2), new Rectangle(11, 11, 5, 5));
            bool bM5Area4 = (iMangars == 5) && !(bM5Area1 || bM5Area2 || bM5Area3);
            bool bSeven = iMangars > 3 || (iMangars == 3 && btData.MapSpecial(BT1.Spots.StairsUpMTL3) != 0);
            bool bSwapDoors = iMangars > 4 || (iMangars == 4 && btData.MapSpecial(BT1.Spots.SwapDoors) == 0);
            DefeatMangar.MarkAllWhenComplete = true;
            DefeatMangar.AddObj(bInMangars, bM1Area2 || bM1Area3 || iMangars > 1, bM1Area3 || iMangars > 1, iMangars > 1,
                bCircle, iMangars > 2, bMaster, bSeven, bSeven, bSeven, bSeven, bSeven, bSeven, bSeven, bSeven, iMangars > 3,
                bSwapDoors || bM4Area2 || bM4Area3 || bM4Area4 || iMangars > 4, bSwapDoors || bM4Area3 ||bM4Area4 || iMangars > 4, bSwapDoors || bM4Area4 || iMangars > 4, 
                bSwapDoors, bSwapDoors && bM4Area5 || iMangars > 4,
                iMangars > 4, iMangars > 4 && !bM5Area1, bM5Area3 || bM5Area4, bM5Area4);
            DefeatMangar.AddPost(false);
            AddQuest(totals, DefeatMangar);

            Guardians.AddObj(TownScriptsInactive(btData, BT1.Spots.FESBSamurai, BT1.Spots.FESBStoneGolem1, BT1.Spots.FESBStoneGolem2,
                BT1.Spots.FESBStoneGiant1, BT1.Spots.FESBStoneGiant2, BT1.Spots.FESBStoneGiant3,
                BT1.Spots.FESBOgreLord1, BT1.Spots.FESBOgreLord2, BT1.Spots.FESBOgreLord3, BT1.Spots.FESBGreyDragon));

            Monsters.AddObj(Defeated(iMap, btData, BT1.Spots.FESL1Spider6, BT1.Spots.FESL1Spider5,
                BT1.Spots.FESL1Spider7, BT1.Spots.FESL1BlackWidow3a, BT1.Spots.FESL1Spinner, BT1.Spots.FESL1BlackWidow3b, BT1.Spots.FESL1BlackWidow4, BT1.Spots.FESL1BlackWidow5,
                BT1.Spots.FESL2Spider13, BT1.Spots.FESL2BlackWidow7a, BT1.Spots.FESL2BlackWidow6a, BT1.Spots.FESL2Spider16, BT1.Spots.FESL2BlackWidow6b, BT1.Spots.FESL2Spider10,
                BT1.Spots.FESL2BlackWidow7b, BT1.Spots.FESL2Spider9, BT1.Spots.FESL3Spinner3a, BT1.Spots.FESL3BlackWidow7, BT1.Spots.FESL3Spinner4a, BT1.Spots.FESL3BlackWidow5,
                BT1.Spots.FESL3Spinner2, BT1.Spots.FESL3Spinner4b, BT1.Spots.FESL3BlackWidow8, BT1.Spots.FESL3Spinner3b, BT1.Spots.FECL1Wight11, BT1.Spots.FECL1Skeleton66,
                BT1.Spots.FECL1Zombie45, BT1.Spots.FECL1Zombie28, BT1.Spots.FECL1Skeleton39, BT1.Spots.FECL1Skeleton52, BT1.Spots.FECL1Wight9, BT1.Spots.FECL1Ghoul5, 
                BT1.Spots.FECL2GreyDragon, BT1.Spots.FECL2MasterSorcerer, BT1.Spots.FECL2Wight49, BT1.Spots.FECL2SoulSucker, BT1.Spots.FECL3Wraith8,BT1.Spots.FECL3Ogre,
                BT1.Spots.FECL3Spectre, BT1.Spots.FECL3Zombie53, BT1.Spots.FECL3Zombie66, BT1.Spots.FECL3Skeleton99a, BT1.Spots.FECL3Skeleton99b, BT1.Spots.FECL3Wight69,
                BT1.Spots.FECL3Ghoul36, BT1.Spots.FECL3Wraith7, BT1.Spots.FEHCL1MasterNinja, BT1.Spots.FEHCL1Wight3, BT1.Spots.FEHCL1Golem, BT1.Spots.FEHCL1Golema,
                BT1.Spots.FEHCL1Golemb, BT1.Spots.FEHCL1Golemc, BT1.Spots.FEHCL1Berserker6, BT1.Spots.FEHCL1Jabberwock, BT1.Spots.FEHCL2MasterSorcerer,
                BT1.Spots.FEHCL2MangarGuard8, BT1.Spots.FEHCL3OldManLesserDemon4, BT1.Spots.FEHCL3Berserker396, BT1.Spots.FEKTGreenDragon6, BT1.Spots.FEKTCrystalGolem, 
                BT1.Spots.FEMTL1MazeMaster3, BT1.Spots.FEMTL1Dwarf56, BT1.Spots.FEMTL1EvilEye34, BT1.Spots.FEMTL1MasterMagician2, BT1.Spots.FEMTL1Ghost20,
                BT1.Spots.FEMTL1Samurai96, BT1.Spots.FEMTL1Mercenary68, BT1.Spots.FEMTL2Hobbit97, BT1.Spots.FEMTL2Basilisk3, BT1.Spots.FEMTL2MindShadow,
                BT1.Spots.FEMTL2Bandersnatch2, BT1.Spots.FEMTL2SoulSucker, BT1.Spots.FEMTL2Samurai67, BT1.Spots.FEMTL2Ghoul32, BT1.Spots.FEMTL3MasterNinja7, BT1.Spots.FEMTL3Mongoa,
                BT1.Spots.FEMTL3Mongob, BT1.Spots.FEMTL3Mongoc, BT1.Spots.FEMTL3Wraith6OldMan, BT1.Spots.FEMTL3Mongod, BT1.Spots.FEMTL3Lv6Conjurer7, BT1.Spots.FEMTL3Mongoe,
                BT1.Spots.FEMTL3Mongof, BT1.Spots.FEMTL3Mongog, BT1.Spots.FEMTL3Mongoh, BT1.Spots.FEMTL4Ghoul50, BT1.Spots.FEMTL4Wraith9, BT1.Spots.FEMTL4LesserDemon7,
                BT1.Spots.FEMTL4VampireLord, BT1.Spots.FEMTL4RedDragon2, BT1.Spots.FEMTL4Mercenary4, BT1.Spots.FEMTL4Spectre, BT1.Spots.FEMTL4Vampire6,
                BT1.Spots.FEMTL5BlackDragon2, BT1.Spots.FEMTL5Archmage, BT1.Spots.FEMTL5Spectre4, BT1.Spots.FEMTL5Balrog4, BT1.Spots.FEMTL5AncientEnemy3, BT1.Spots.FEMTL5StormGiant5));
            AddQuest(totals, Monsters);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool[] TownScriptsInactive(BT1QuestData data, params MapXY[] spots)
        {
            bool[] results = new bool[spots.Length];
            for (int i = 0; i < spots.Length; i++)
                results[i] = data.TownMapByte(spots[i]) == 0;
            return results;
        }

        private bool[] Defeated(int iMap, BT1QuestData data, params MapXY[] spots)
        {
            bool[] results = new bool[spots.Length];
            for(int i = 0; i < spots.Length; i++)
                results[i] = iMap == spots[i].Map && data.MapSpecial(spots[i].Location) == 0;
            return results;
        }
    }
}