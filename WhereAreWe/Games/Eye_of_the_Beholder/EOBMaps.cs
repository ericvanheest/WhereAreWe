using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class EOBMaps : MapsBase
    {
        public EOBMaps(MapSheet sheet)
        {
            m_sheet = sheet;
        }

        private SquareDirInfo EOBLineInfo(int map, BasicWall wall, BasicWall opposite)
        {
            MapLineInfo mliWall = MapLineInfo.BlackLine2;
            MapLineInfo mliFalseWall = MapLineInfo.BlackDot2;
            MapLineInfo mliSmallMissile = MapLineInfo.BlackDash2;

            IconName icon = EOBIcon(map, wall, opposite);
            if ( wall.IsMissileWall || opposite.IsMissileWall )
                return new SquareDirInfo(mliSmallMissile, icon);
            if ( (wall.IsFalseWall && opposite.IsFalseWall) ||
                 (wall.IsFalseWall && opposite.IsOpen) ||
                 (wall.IsOpen && opposite.IsFalseWall) )
                return new SquareDirInfo(mliFalseWall, icon);
            if (opposite.IsSolid || wall.IsSolid)
                return new SquareDirInfo(mliWall, icon);
            return new SquareDirInfo(GridLines, icon);
        }

        private IconName EOBIcon(int map, BasicWall wall, BasicWall opposite)
        {
            if (wall.DoorType.HasFlag(DoorStatus.Portcullis | DoorStatus.Forceable))
                return IconName.GrateFull;
            if (wall.IsDoor)
            {
                if (wall.DoorType.HasFlag(DoorStatus.Closed))
                    return IconName.LockedFull;
                else
                    return IconName.DoorFull;
            }
            return IconName.None;
        }

        private void SetEOBSquare(MapSquare square, Point ptGrid, SquareDirInfo sdi, Direction dir)
        {
            square.Line(dir, sdi.Line);
            square.AddUniqueIcon(new MapIcon(sdi.Icon, dir, ptGrid));
        }

        private bool AllSolid(int map, params BasicWall[] walls)
        {
            return walls.All(w => w.IsSolid);
        }

        private string ActivateString(EOBMapSquareInfo info)
        {
            WallSpecials[] buttons = new WallSpecials[] { WallSpecials.Button, WallSpecials.ButtonPressed };
            WallSpecials[] levers = new WallSpecials[] { WallSpecials.SwitchUp, WallSpecials.SwitchDown };

            if (info.Any(buttons))
                return String.Format("Activate {0} button: ", Global.DirectionString(Global.Opposite(info.Which(buttons))).ToLower());
            if (info.Any(levers))
                return String.Format("Activate {0} lever: ", Global.DirectionString(Global.Opposite(info.Which(buttons))).ToLower());
            if (info.Any(WallSpecials.Writing))
                return String.Format("Activate {0} writing: ", Global.DirectionString(Global.Opposite(info.Which(WallSpecials.Writing))).ToLower());
            if (info.Any(WallSpecials.Rune))
                return String.Format("Activate {0} rune: ", Global.DirectionString(Global.Opposite(info.Which(WallSpecials.Rune))).ToLower());
            if (info.Any(WallSpecials.Decoration))
                return String.Format("Activate {0} wall: ", Global.DirectionString(Global.Opposite(info.Which(WallSpecials.Decoration))).ToLower());
            return "";
        }

        public MapSquareData SetEOBSquareFromGameBytes(Point ptGrid, EOBMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            GameNames game = GameNames.None;
            if (data is EOB1MapData)
                game = GameNames.EyeOfTheBeholder1;
            else if (data is EOB2MapData)
                game = GameNames.EyeOfTheBeholder2;

            int iMap = data.Index;
            MapSquare square = Grid[ptGrid.X, ptGrid.Y];
            Point ptGame = new Point(horiz, vert);
            if (data.LiveOnly)
            {
                // Live squares can only switch between certain background patterns; others need to stay as they are
                if (square.Colors.BackColorPattern.Equals(SquareStyleList.DefaultInaccessible) ||
                    square.Colors.BackColorPattern.Equals(SquareStyleList.DefaultSolid))
                {
                    square.Colors.BackColorPattern = SquareStyleList.DefaultEmpty;
                }
            }
            else
            {
                square.Colors.BackColorPattern = SquareStyleList.DefaultEmpty;
                square.RemoveIcons(IconName.StairsDown);
                square.RemoveIcons(IconName.StairsUp);
            }
            MapSquareData msd = null;
            square.RemoveIcons(IconName.DoorHalf);
            square.RemoveIcons(IconName.DoorFull);
            square.RemoveIcons(IconName.GrateFull);
            square.RemoveIcons(IconName.LockedFull);
            square.RemoveIcons(IconName.ArrowHalf);

            EOBMapSquareInfo infoMain = EOBMapSquareInfo.Create(game, iMap, data.Squares, vert, horiz);
            EOBMapSquareInfo infoNorth = EOBMapSquareInfo.Create(game, iMap, data.Squares, vert - iDeltaSouth, horiz);
            EOBMapSquareInfo infoSouth = EOBMapSquareInfo.Create(game, iMap, data.Squares, vert + iDeltaSouth, horiz);
            EOBMapSquareInfo infoEast = EOBMapSquareInfo.Create(game, iMap, data.Squares, vert, horiz + iDeltaEast);
            EOBMapSquareInfo infoWest = EOBMapSquareInfo.Create(game, iMap, data.Squares, vert, horiz - iDeltaEast);

            NoteInfo noteInfo = null;

            int iSquareOffset = (vert * 9 * 32) + (horiz * 9);
            int iItemIndex = BitConverter.ToInt16(data.Squares, iSquareOffset + 5);
            int iScriptIndex = BitConverter.ToInt16(data.Squares, iSquareOffset + 7);
            int iMonster = data.Squares[iSquareOffset + 4];
            bool bLoot = iItemIndex != 0 && data.ItemList != null;
            bool bScript = iScriptIndex != 0;
            bool bMonster = iMonster != 0;
            string strScriptSummary = "";

            EOBMapData eData = data as EOBMapData;
            if (eData == null || eData.Scripts == null)
                return msd;

            StringBuilder sb = new StringBuilder();
            const string strControl = "(This door is controlled by its {0})";
            string strSymbol = "";
            if (infoMain.North.IsButtonDoor && infoMain.South.IsButtonDoor)
                sb.AppendFormat(strControl, "north button");
            else if (!infoMain.North.IsButtonDoor && infoMain.South.IsButtonDoor)
                sb.AppendFormat(strControl, "south button");
            else if (infoMain.North.IsButtonDoor && infoMain.South.IsButtonDoor)
                sb.AppendFormat(strControl, "north and south buttons");
            if (infoMain.East.IsButtonDoor && !infoMain.West.IsButtonDoor)
                sb.AppendFormat(strControl, "east button");
            else if (!infoMain.East.IsButtonDoor && infoMain.West.IsButtonDoor)
                sb.AppendFormat(strControl, "west button");
            else if (infoMain.East.IsButtonDoor && infoMain.West.IsButtonDoor)
                sb.AppendFormat(strControl, "east and west buttons");
            else if (infoMain.Any(DoorStatus.Forceable))
                sb.Append("(This door may be forced open)");
            if (sb.Length > 0 && strSymbol == "")
                strSymbol = ".";

            string strActivate = ActivateString(infoMain);

            if (bLoot || bScript)
            {
                if (bLoot)
                    sb.AppendFormat("Loot: {0}\r\n", EOBItem.ItemListString(game, data.ItemList, iItemIndex));
                if (bScript)
                {
                    strScriptSummary = eData.Scripts.SummaryForSquare(horiz, vert, true, true);
                    sb.AppendFormat("Script: 0x{0:X4}: {1}{2}\r\n", iScriptIndex, strActivate, strScriptSummary);
                }

                if (bLoot && !bScript)
                    strSymbol = "L";
                else if (bScript && !bLoot)
                    strSymbol = "S";
                else
                    strSymbol = "?";
            }
            if (strSymbol != "")
                noteInfo = new NoteInfo(sb.ToString().Trim(), strSymbol, Color.Black);

            SquareDirParams sd = new SquareDirParams();
            sd.Square = square;
            sd.GridPoint = ptGrid;
            sd.Unimportant = false;

            SetEOBSquare(square, ptGrid, EOBLineInfo(iMap, infoMain.North, infoNorth == null ? infoMain.North : infoNorth.South), Direction.Up);
            SetEOBSquare(square, ptGrid, EOBLineInfo(iMap, infoMain.East, infoEast == null ? infoMain.East : infoEast.West), Direction.Right);
            SetEOBSquare(square, ptGrid, EOBLineInfo(iMap, infoMain.South, infoSouth == null ? infoMain.South : infoSouth.North), Direction.Down);
            SetEOBSquare(square, ptGrid, EOBLineInfo(iMap, infoMain.West, infoWest == null ? infoMain.West : infoWest.East), Direction.Left);

            if (AllSolid(iMap, infoMain.North, infoMain.East, infoMain.South, infoMain.West))
            {
                square.Colors.BackColorPattern = Global.SquareStyles.List[SquareStyleList.Name.Solid];
            }

            bool bUp = false;
            bool bDown = false;
            if (square.Lines.TopWidth == 1 && ptGrid.Y == 1)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Up, ptGrid));
            if (square.Lines.LeftWidth == 1 && ptGrid.X == 1)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Left, ptGrid));
            if (square.Lines.BottomWidth == 1 && ptGrid.Y == data.Bounds.Height)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Down, ptGrid));
            if (square.Lines.RightWidth == 1 && ptGrid.X == data.Bounds.Width)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Right, ptGrid));
            if (infoMain.Any(LevelChange.HoleDown))
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Down, ptGrid));
            if (!data.LiveOnly)
            {
                if (infoMain.Any(LevelChange.Up))
                    bUp = true;
                else if (infoMain.Any(LevelChange.Down))
                    bDown = true;
            }

            if (bUp || bDown)
            {
                if (bUp)
                    square.AddUniqueIcon(new MapIcon(IconName.StairsUp, Direction.Up, ptGrid));
                else
                    square.AddUniqueIcon(new MapIcon(IconName.StairsDown, Direction.Up, ptGrid));
                if (noteInfo == null)
                    noteInfo = new NoteInfo("", ".", Color.Black);
                noteInfo.Symbol = ".";

                Dictionary<Point, List<GameScript>> dict = eData?.Scripts?.Scripts?.Scripts;
                if (dict != null && dict.ContainsKey(ptGame))
                {
                    List<GameScript> list = dict[new Point(horiz, vert)];
                    if (list != null)
                    {
                        foreach (GameScript gs in list)
                        {
                            foreach (EOB1ScriptLine line in gs.Lines)
                            {
                                if (line.Opcode == EOB1Opcode.ChangeLevel)
                                {
                                    noteInfo.Text = String.Format("Going {0}... {{map:{1}}} ({2},{3}){4}", bUp ? "up" : "down",
                                        EOB1MemoryHacker.GetMapTitlePair(line.TargetValue).Title, line.TargetPoint.X, line.TargetPoint.Y,
                                    String.IsNullOrWhiteSpace(noteInfo.Text) ? "" : "\r\n" + noteInfo.Text);
                                }
                            }
                        }
                    }
                }
            }

            MapTitlePairDelegate titleFn = EOB1MemoryHacker.GetMapTitlePair;
            if (!data.LiveOnly)
            {

                if (noteInfo != null && square.Note == null)
                    square.Note = new MapNote(noteInfo, ptGrid);

                if (noteInfo != null && noteInfo.Icon != null)
                {
                    // The MapIcon object knows its x,y coordinates, but those are for the game map at this point, not the grid
                    noteInfo.Icon.Location = ptGrid;
                    square.AddUniqueIcon(noteInfo.Icon);
                    if (noteInfo.Icon.Name == IconName.StairsDown || noteInfo.Icon.Name == IconName.StairsUp)
                        square.Note.Symbol = ".";
                }
            }
            return msd;
        }
    }

    public class EOBMapSquareInfo
    {
        public BasicWall North;
        public BasicWall East;
        public BasicWall South;
        public BasicWall West;

        public EOBMapSquareInfo()
        {
        }

        public EOBMapSquareInfo(GameNames game, int iMap, byte north, byte east, byte south, byte west)
        {
            EOBGameGlobals eob = Games.GetEOBGlobals(game);
            West = eob.GetWall(iMap, west);
            East = eob.GetWall(iMap, east);
            South = eob.GetWall(iMap, south);
            North = eob.GetWall(iMap, north);
        }

        public bool Any(params WallSpecials[] specials)
        {
            foreach (WallSpecials special in specials)
                if (North.Specials.HasFlag(special) ||
                    East.Specials.HasFlag(special) ||
                    South.Specials.HasFlag(special) ||
                    West.Specials.HasFlag(special))
                    return true;
            return false;
        }

        public bool Any(params DoorStatus[] doors)
        {
            foreach (DoorStatus door in doors)
                if (North.DoorType.HasFlag(door) ||
                    East.DoorType.HasFlag(door) ||
                    South.DoorType.HasFlag(door) ||
                    West.DoorType.HasFlag(door))
                    return true;
            return false;
        }

        public bool Any(params LevelChange[] changes)
        {
            foreach (LevelChange change in changes)
                if (North.Transport.HasFlag(change) ||
                    East.Transport.HasFlag(change) ||
                    South.Transport.HasFlag(change) ||
                    West.Transport.HasFlag(change))
                    return true;
            return false;
        }

        public DirectionFlags Which(params WallSpecials[] specials)
        {
            DirectionFlags flags = DirectionFlags.None;
            foreach (WallSpecials special in specials)
            {
                if (West.Specials.HasFlag(special))
                    flags |= DirectionFlags.West;
                if (East.Specials.HasFlag(special))
                    flags |= DirectionFlags.East;
                if (North.Specials.HasFlag(special))
                    flags |= DirectionFlags.North;
                if (South.Specials.HasFlag(special))
                    flags |= DirectionFlags.South;
            }
            return flags;
        }

        public static EOBMapSquareInfo Create(GameNames game, int iMap, byte[] data, int vert, int horiz)
        {
            int iSquareSize = 9;
            switch (game)
            {
                case GameNames.EyeOfTheBeholder1:
                    iSquareSize = EOB1MapData.BytesPerSquare;
                    break;
                case GameNames.EyeOfTheBeholder2:
                    iSquareSize = EOB2MapData.BytesPerSquare;
                    break;
            }
            int iOffset = (vert * 32 * iSquareSize) + (horiz * iSquareSize);

            if (iOffset < 0 || iOffset > data.Length - iSquareSize)
                return null;

            return new EOBMapSquareInfo(game, iMap, data[iOffset], data[iOffset + 1], data[iOffset + 2], data[iOffset + 3]);
        }
    }
}
