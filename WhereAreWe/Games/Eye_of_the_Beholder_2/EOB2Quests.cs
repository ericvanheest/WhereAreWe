using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public static class EOB2Bits
    {
        public enum Scripts
        {
            Test1 = 0,
            None = -1,
        }

        public enum Game
        {
            None = -1,
        }

        public enum Level
        {
            None = -1,
        }

        public static bool IsSet(byte[] bytes, Scripts bit) { return bit == Scripts.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }

        public static BitDesc GlobalDescription(object val) { return GlobalBitDescription((Game)val); }

        private const string enter = "Enter the square";
        private const string active = "Activate the square";

        public static BitDesc Description(Scripts bit)
        {
            switch (bit)
            {
                case Scripts.Test1: return new BitDesc("Do something", EOB2.Spots.None, enter);

                default: return BitDesc.Empty;
            }
        }

        public static BitDesc GlobalBitDescription(Game bit)
        {
            EOB2Locations es = EOB2.Spots;

            switch (bit)
            {
                default: return BitDesc.Empty;
            }
        }
    }

    public class EOB2QuestData : EOBQuestData
    {
        public EOB2Effects Effects;
        public byte[] QuestBits;

        public EOB2QuestData(EOBPartyInfo party, LocationInformation location, EOBGameState state, byte[] questBits, EOB2MapData mapData, EOB2Effects effects, EOB2GameInfo gameInfo)
            : base(party, location, state, null, null, gameInfo)
        {
            Map = mapData;
            Effects = effects;
            QuestBits = questBits;
            Info = gameInfo;
        }

        public bool IsActive(Point pt)
        {
            return false;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            EOB2MapData data = Map as EOB2MapData;
            if (data != null)
            {
                // Record changes in the wall types and the item list head, but not
                // the number of monsters (changes frequently and isn't relevant to quests)
                // That means skipping byte index 4
                for (int iOffset = 0; iOffset < data.Squares.Length; iOffset += 9)
                {
                    stream.Write(data.Squares, iOffset, 4);
                    stream.Write(data.Squares, iOffset + 5, 2);
                }
            }
        }
    }

    public class EOB2Quest : BasicQuest
    {
        public EOB2Quest()
        {
        }
    }

    public class EOB2QuestInfo : QuestInfo<EOB2Quest>
    {
        // If all of the quests are not returned from the GetAllQuests() function, then the quest window will not
        // update any of the missing quests until a manual refresh is performed.
        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { }; }

        public override bool QuestsEqual(QuestInfo<EOB2Quest> info)
        {
            if (!base.QuestsEqual(info))
                return false;
            return true;
        }

        public override BasicQuest[] GetQuests()
        {
            List<EOB2Quest> quests = new List<EOB2Quest>();

            return quests.ToArray();
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            EOB2QuestData eobData = data as EOB2QuestData;
            if (eobData == null)
                return;

            EOBPartyInfo party = data.Party as EOBPartyInfo;
            EOB2GameInfo gameInfo = data.Info as EOB2GameInfo;
            LocationInformation location = data.Location;
            EOBGameState state = eobData.State as EOBGameState;
            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;
            EOB2Character EOB2Char = EOBCharacter.Create(state.Game, null, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as EOB2Character;
            EOB2QuestData questData = data as EOB2QuestData;
            EOB2MapData mapData = questData.Map as EOB2MapData;
            QuestTotals totals = new QuestTotals(0, 0);
            CharName = EOB2Char.Name;
            CharAddress = iOverrideCharAddress;
            PartyLocation = eobData.Location.PrimaryCoordinates;
            MapXY spot = new MapXY(GameNames.EyeOfTheBeholder2, eobData.Location.MapIndex, PartyLocation.X, PartyLocation.Y);
            EOB2Map map = (EOB2Map)spot.Map;

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool DoorOpen(EOB2MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            DoorStatus door = data.GetWall(spot.X, spot.Y, dir).DoorType;
            return door.HasFlag(DoorStatus.PartlyOpen) || door.HasFlag(DoorStatus.Open);
        }

        private bool IsForceable(EOB2MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).DoorType.HasFlag(DoorStatus.Forceable);
        }

        private bool HasFloorHole(EOB2MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).IsFloorHole;
        }

        private bool IsOpen(EOB2MapData data, MapXY spot, Direction dir)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWall(spot.X, spot.Y, dir).IsOpen;
        }

        private bool IsWallType(EOB2MapData data, MapXY spot, Direction dir, int iWallType)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.GetWallIndex(spot.X, spot.Y, dir) == iWallType;
        }

        private bool ItemOnGround(EOB2MapData data, MapXY spot)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItem(spot.X, spot.Y);
        }

        private bool ItemTypeOnGround(EOB2MapData data, EOBItemIndex itemType, MapXY spot)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItemType(itemType, spot.X, spot.Y);
        }

        private bool ItemTypesOnGround(EOB2MapData data, MapXY spot, params EOBItemIndex[] itemTypes)
        {
            if (data.Index != spot.Map)
                return false;   // Not the right map data for this spot
            return data.HasItemTypes(spot.X, spot.Y, itemTypes);
        }

        private bool AnyCharHasItemType(EOBPartyInfo party, EOBItemIndex index, byte[] itemList, int iModifier = -0x10000)
        {
            return party.AnyCharHasItemType((int)index, itemList, iModifier);
        }
    }
}
