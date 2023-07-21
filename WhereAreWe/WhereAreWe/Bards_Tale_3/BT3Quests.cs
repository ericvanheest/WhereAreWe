using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public static class BT3Bits
    {
        public enum Scripts
        {
            None = -1,
        }

        public static bool IsSet(byte[] bytes, Scripts bit) { return bit == Scripts.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }

        public static BitDesc ScriptsDescription(object val) { return Description((Scripts)val); }

        private const string enter = "Enter the location";

        public static BitDesc Description(Scripts bit)
        {
            switch (bit)
            {
                default: return BitDesc.Empty;
            }
        }
    }

    public class BT3QuestData : BTQuestData
    {
        public MapBytes CurrentMapBytes;
        public BT3Effects Effects;

        public BT3QuestData(BTPartyInfo party, LocationInformation location, BTGameState state, byte[] mapSpecials, byte[] townMap, MapBytes mapData, BT3Effects effects)
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
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT3.Offsets.Class]);
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT3.Offsets.Condition]);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT3.Offsets.Inventory, BT3.Offsets.InventoryLength);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT3.Offsets.Name, BT3.Offsets.NameLength);
                }
            }
        }
    }

    public class BT3Quest : BasicQuest
    {
        public BT3Quest()
        {
        }
    }

    public class BT3QuestInfo : QuestInfo
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

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<BT3Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<BT3Quest> quests, string strReward = "", string strPath = "")
        {
            BT3Quest quest = GetQuest(status, BasicQuestType.Side, null, null, strReward) as BT3Quest;
            if (!String.IsNullOrWhiteSpace(strPath))
                quest.Path = strPath;
            quests.Add(quest);
            return quest;
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<BT3Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<BT3Quest> quests, string strReward = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<BT3Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            BT3Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as BT3Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<BT3Quest> quests = new List<BT3Quest>();

            quests.Sort(CompareBT3Quests);
            return quests.ToArray();
        }

        public int CompareBT3Quests(BT3Quest quest1, BT3Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            BT3QuestData btData = data as BT3QuestData;
            if (btData == null)
                return;

            BTPartyInfo party = data.Party as BTPartyInfo;
            LocationInformation location = data.Location;
            BTGameState state = btData.State as BTGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            BTCharacter btChar = BTCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as BTCharacter;

            if (!(data is BT3QuestData))
                return;

            BT3QuestData questData = data as BT3QuestData;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = btChar.Name;
            CharAddress = iOverrideCharAddress;

            PartyLocation = btData.Location.PrimaryCoordinates;

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool IsActive(BT3QuestData data, BT3Map map, MapXY spot) { return (int)map == spot.Map && data.IsActive(spot.Location); }
        private bool IsInactive(BT3QuestData data, BT3Map map, MapXY spot) { return (int)map == spot.Map && !data.IsActive(spot.Location); }
    }
}