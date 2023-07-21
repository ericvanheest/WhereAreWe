using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class Wiz2Quest : BasicQuest
    {
        public Wiz2Quest()
        {
        }
    }

    public class Wiz2QuestInfo : QuestInfo
    {
        public bool DefeatedSword;
        public bool DefeatedHelm;
        public bool DefeatedShield;
        public bool DefeatedGauntlets;
        public bool DefeatedArmor;

        public QuestStatus KodSword = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Magic Sword");
        public QuestStatus KodHelm = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Magic Helm");
        public QuestStatus KodShield = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Magic Shield");
        public QuestStatus KodGauntlets = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Magic Gauntlets");
        public QuestStatus KodArmor = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat the Magic Armor");
        public QuestStatus StaffOfLight = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Light");
        public QuestStatus StaffOfGnilda = new QuestStatus(QuestStatus.Basic.NotStarted, "Obtain the Staff of Gnilda");

        public override QuestStatus[] GetAllQuests()
        {
            return new QuestStatus[] { KodSword, KodHelm, KodShield, KodGauntlets, KodArmor, StaffOfLight, StaffOfGnilda };
        }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<Wiz2Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<Wiz2Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Side, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<Wiz2Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<Wiz2Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            Wiz2Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as Wiz2Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<Wiz2Quest> quests = new List<Wiz2Quest>();

            KodSword.AddLocations(new QuestLocation("Encounter the Magic Sword", Wiz2.Spots.Hrathnir));
            KodHelm.AddLocations(new QuestLocation("Encounter the Magic Helm", Wiz2.Spots.MagicHelm));
            KodShield.AddLocations(new QuestLocation("Encounter the Magic Shield", Wiz2.Spots.MagicShield));
            KodGauntlets.AddLocations(new QuestLocation("Encounter the Magic Gauntlets", Wiz2.Spots.MagicGauntlets));
            KodArmor.AddLocations(new QuestLocation("Encounter the Magic Armor", Wiz2.Spots.MagicArmor));
            StaffOfLight.AddLocations(new QuestLocation("Search the statue", Wiz2.Spots.StaffOfLight));

            AddSideQuest(StaffOfLight, quests, "Staff of Light");

            StaffOfGnilda.PreQuest.Add(AddMainQuest(KodArmor, quests, "Kod's Armor, 33333 Exp"));
            StaffOfGnilda.PreQuest.Add(AddMainQuest(KodShield, quests, "Kod's Shield, 44444 Exp"));
            StaffOfGnilda.PreQuest.Add(AddMainQuest(KodSword, quests, "Hrathnir, 66666 Exp"));
            StaffOfGnilda.PreQuest.Add(AddMainQuest(KodHelm, quests, "Kod's Helm, 88888 Exp"));
            StaffOfGnilda.PreQuest.Add(AddMainQuest(KodGauntlets, quests, "Kod's Gauntlets, 199998 Exp"));
            StaffOfGnilda.AddLocations(new QuestLocation("Remove all but one character from the party", Wiz2.Spots.None),
                new QuestLocation("Equip Kod's Armor", Wiz2.Spots.None),
                new QuestLocation("Equip Kod's Shield", Wiz2.Spots.None),
                new QuestLocation("Equip Hrathnir", Wiz2.Spots.None),
                new QuestLocation("Equip Kod's Helm", Wiz2.Spots.None),
                new QuestLocation("Equip Kod's Gauntlets", Wiz2.Spots.None),
                new QuestLocation("Answer the apparition's riddle", Wiz2.Spots.Apparition1));
            StaffOfGnilda.Postrequisites.Add(new QuestLocation("Return to the Castle", Wiz2.Spots.Castle));
            StaffOfGnilda.MarkAllWhenComplete = true;

            AddMainQuest(StaffOfGnilda, quests);

            quests.Sort(CompareWiz2Quests);
            return quests.ToArray();
        }

        public int CompareWiz2Quests(Wiz2Quest quest1, Wiz2Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            WizQuestData wizData = data as WizQuestData;
            if (wizData == null)
                return;

            WizPartyInfo party = wizData.Party as WizPartyInfo;
            LocationInformation location = wizData.Location;
            byte[] fights = wizData.Fights;
            WizGameState state = wizData.State as WizGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            byte[] bytesKod = data.GetBytes();

            WizCharacter wiz2Char = WizCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize, null, false);
            QuestTotals totals = new QuestTotals(0, 0);

            CharName = wiz2Char.Name;
            CharAddress = iOverrideCharAddress;

            DefeatedSword = (BitConverter.ToInt16(bytesKod, 0) != 0);
            DefeatedHelm = (BitConverter.ToInt16(bytesKod, 2) != 0);
            DefeatedShield = (BitConverter.ToInt16(bytesKod, 4) != 0);
            DefeatedGauntlets = (BitConverter.ToInt16(bytesKod, 6) != 0);
            DefeatedArmor = (BitConverter.ToInt16(bytesKod, 8) != 0);

            KodSword.AddObj(DefeatedSword);
            KodHelm.AddObj(DefeatedHelm);
            KodShield.AddObj(DefeatedShield);
            KodArmor.AddObj(DefeatedArmor);
            KodGauntlets.AddObj(DefeatedGauntlets);
            StaffOfLight.AddObj(party.CurrentPartyHasItem(Wiz2ItemIndex.StaffOfLight));
            bool bGnilda = party.CurrentPartyHasItem(Wiz2ItemIndex.StaffOfGnilda);
            StaffOfGnilda.AddObj(bGnilda || party.NumChars == 1,
                bGnilda || party.CurrentPartyHasEquipped(Wiz2ItemIndex.KodsArmor),
                bGnilda || party.CurrentPartyHasEquipped(Wiz2ItemIndex.KodsShield),
                bGnilda || party.CurrentPartyHasEquipped(Wiz2ItemIndex.Hrathnir),
                bGnilda || party.CurrentPartyHasEquipped(Wiz2ItemIndex.KodsHelm),
                bGnilda || party.CurrentPartyHasEquipped(Wiz2ItemIndex.KodsGauntlets),
                bGnilda
                );
            StaffOfGnilda.AddPost((wiz2Char.Honors & 0x4800) != 0);

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }
    }
}