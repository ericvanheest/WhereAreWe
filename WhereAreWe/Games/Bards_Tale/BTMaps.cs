using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class BTMaps : MapsBase
    {
        public BTMaps(MapSheet sheet)
        {
            m_sheet = sheet;
        }

        private SquareDirInfo BTLineInfo(BTWall wall, BTWall opposite)
        {
            MapLineInfo mliWall = MapLineInfo.BlackLine2;
            MapLineInfo mliFalseWall = MapLineInfo.BlackDot2;

            if (wall.HasFlag(BTWall.NoPhase) || opposite.HasFlag(BTWall.NoPhase))
            {
                mliWall = MapLineInfo.BlueLine2;
                mliFalseWall = MapLineInfo.BlueDot2;
            }

            IconName icon = BTIcon(wall, opposite);
            if (wall == BTWall.Open)
            {
                switch (opposite)
                {
                    case BTWall.Open: return new SquareDirInfo(GridLines, icon);
                    case BTWall.FalseOpen: return new SquareDirInfo(MapLineInfo.RedLine2, icon);
                    default:
                        if (opposite.HasFlag(BTWall.FalseWall))
                            return new SquareDirInfo(mliFalseWall, icon);
                        return new SquareDirInfo(mliWall, icon);
                }
            }
            else if (wall.HasFlag(BTWall.FalseWall))
                return new SquareDirInfo(opposite == BTWall.Wall ? mliWall : mliFalseWall, icon);
            else if (wall == BTWall.FalseOpen)
                return new SquareDirInfo(MapLineInfo.RedLine2, icon); // barriers
            else if (wall.HasFlag(BTWall.Blue) && !wall.HasFlag(BTWall.NoPhase))
                return new SquareDirInfo(opposite == BTWall.Wall ? MapLineInfo.BlueLine2 : MapLineInfo.BlueDot2, icon); // phasable blue walls are rare; treat like false walls
            return new SquareDirInfo(mliWall, icon); // walls, doors
        }

        private IconName BTIcon(BTWall wall, BTWall opposite)
        {
            if (wall == BTWall.Open)
            {
                switch (opposite)
                {
                    case BTWall.FalseWall:
                    case BTWall.FalseOpen:
                        return IconName.ArrowHalf;
                    default:
                        if (opposite.HasFlag(BTWall.Wall))
                            return IconName.ArrowHalf;
                        return IconName.None;
                }
            }

            if (wall.HasFlag(BTWall.FalseWall))
                return opposite == BTWall.Wall ? IconName.ArrowHalf : IconName.None;
            else if (wall.HasFlag(BTWall.Door))
                return IconName.DoorHalf;
            return IconName.None;  // open, wall
        }

        private void SetBTSquare(MapSquare square, Point ptGrid, SquareDirInfo sdi, Direction dir)
        {
            square.Line(dir, sdi.Line);
            square.AddUniqueIcon(new MapIcon(sdi.Icon, dir, ptGrid));
        }

        private MapSquareData SetBT3SquareFromGameBytes(Point ptGrid, BT3MapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            Point ptGame = new Point(horiz, vert);
            if (Math.Sign(iDeltaSouth) == -1)
                vert = data.Height - vert - 1;
            MapSquare square = Grid[ptGrid.X, ptGrid.Y];
            MapSquareData msd = null;
            square.RemoveIcons(IconName.DoorHalf);
            square.RemoveIcons(IconName.ArrowHalf);

            StringBuilder sbNotes = new StringBuilder();
            string strSymbol = "";
            bool bEncounter = false;
            bool bSpecial = false;
            bool bSilence = false;
            bool bTrap = false;
            bool bDrain = false;
            bool bSpinner = false;

            bool bAntimagic = false;
            bool bNoTeleport = false;
            bool bRestoreSP = false;
            bool bRestoreHP = false;
            bool bDark = false;

            NoteInfo noteInfo = null;

            if (data.BytesPerSquare == 1)
            {
                // Wilderness/Town style
                int iSquare = data.Squares[vert * data.Width + horiz];
                bool bWilderness = (data.Name == "Wilderness");
                switch (iSquare)
                {
                    case 0x00:
                        square.Colors.Background = Color.White;
                        square.Colors.BackColorPattern = ColorPattern.Empty;
                        break;
                    case 0x04:
                        square.Colors.Background = Color.LightGray;
                        break;
                    case 0xE2:
                        square.Colors.Background = Color.Gray;
                        break;
                    case 0xE3:
                        square.Colors.Background = Color.ForestGreen;
                        break;
                    case 0xE4:
                        square.Colors.Background = Color.DarkGreen;
                        break;
                    case 0xF2:
                        if (bWilderness)
                            square.Colors.Background = Color.DarkGray;
                        else
                            square.Colors.Background = Color.FromArgb(192, 255, 196);
                        break;
                    case 0x41:
                    case 0x43:
                        square.Colors.Background = Color.Gray;
                        break;
                    case 0x15:
                    case 0x21:
                    case 0x31:
                        square.Colors.Background = Color.Goldenrod;
                        break;
                    default:
                        square.Colors.Background = Color.FromArgb(192, 255, 196);
                        break;
                }
            }
            else
            {
                // Dungeon style
                BTMapSquareInfo infoMain = BTMapSquareInfo.CreateBT3(data, vert, horiz);
                BTMapSquareInfo infoNorth = BTMapSquareInfo.CreateBT3(data, vert + iDeltaSouth, horiz);
                BTMapSquareInfo infoSouth = BTMapSquareInfo.CreateBT3(data, vert - iDeltaSouth, horiz);
                BTMapSquareInfo infoEast = BTMapSquareInfo.CreateBT3(data, vert, horiz + iDeltaEast);
                BTMapSquareInfo infoWest = BTMapSquareInfo.CreateBT3(data, vert, horiz - iDeltaEast);

                if (!data.LiveOnly)
                    square.Colors.Background = Color.White;

                SquareDirParams sd = new SquareDirParams();
                sd.Square = square;
                sd.GridPoint = ptGrid;
                sd.Unimportant = false;

                SetBTSquare(square, ptGrid, BTLineInfo(infoMain.North, infoNorth == null ? infoMain.North : infoNorth.South), Direction.Up);
                SetBTSquare(square, ptGrid, BTLineInfo(infoMain.East, infoEast == null ? infoMain.East : infoEast.West), Direction.Right);
                SetBTSquare(square, ptGrid, BTLineInfo(infoMain.South, infoSouth == null ? infoMain.South : infoSouth.North), Direction.Down);
                SetBTSquare(square, ptGrid, BTLineInfo(infoMain.West, infoWest == null ? infoMain.West : infoWest.East), Direction.Left);

                if (square.Lines.TopWidth == 1 && ptGrid.Y == 1)
                    square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Up, ptGrid));
                if (square.Lines.LeftWidth == 1 && ptGrid.X == 1)
                    square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Left, ptGrid));
                if (square.Lines.BottomWidth == 1 && ptGrid.Y == data.Bounds.Height)
                    square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Down, ptGrid));
                if (square.Lines.RightWidth == 1 && ptGrid.X == data.Bounds.Width)
                    square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Right, ptGrid));
            }

            if (!data.LiveOnly)
            {
                int iUnknownFlags = 0; 
                if (data.BytesPerSquare == 5)
                {
                    int iSquareOffset = (vert * data.Width * data.BytesPerSquare) + (horiz * data.BytesPerSquare);
                    BT3MapSpecials flags = BT3MapSpecials.None;
                    if (iSquareOffset <= data.Squares.Length - 5)
                        flags = (BT3MapSpecials)((data.Squares[iSquareOffset + 2] << 16) | (data.Squares[iSquareOffset + 3] << 8) | data.Squares[iSquareOffset + 4]);
                    iUnknownFlags = (int)(flags & BT3MapSpecials.UnknownFlags);
                    bSpecial = (flags & BT3MapSpecials.Special) != BT3MapSpecials.None;
                    if (flags.HasFlag(BT3MapSpecials.Dark))
                        bDark = true;
                    if (flags.HasFlag(BT3MapSpecials.AntiMagic))
                        bAntimagic = true;
                    if (flags.HasFlag(BT3MapSpecials.NoTeleport))
                        bNoTeleport = true;
                    //if (flags.HasFlag(BT3MapSpecials.Stairs))
                    //{
                    //    strSymbol = ".";
                    //    bool bDown = false;
                    //    sbNotes.AppendFormat("There are stairs here, going {0}.  Do you with to take them? (Yes/No)\r\nY: {{map:{1}}} ({2},{3})\r\n", bDown ? "?" : "?", "Unknown", 0, 0);
                    //    square.AddUniqueIcon(new MapIcon(bDown ? IconName.StairsDown : IconName.StairsUp));
                    //}
                    if (flags.HasFlag(BT3MapSpecials.PortalDown) || flags.HasFlag(BT3MapSpecials.PortalUp))
                    {
                        strSymbol = ".";
                        bool bDown = flags.HasFlag(BT3MapSpecials.PortalDown);
                        MapXY mapNext = data.NextLevel(bDown, ptGame);
                        sbNotes.AppendFormat("There is a portal {0}.  {{map:{1}}} ({2},{3})\r\n",
                            bDown ? "below" : "above you", BT3MemoryHacker.GetMapTitlePair(mapNext.Map).Title, mapNext.X, mapNext.Y);
                        square.AddUniqueIcon(new MapIcon(bDown ? IconName.StairsDown : IconName.StairsUp, Direction.Up, ptGrid));
                    }
                    if (flags.HasFlag(BT3MapSpecials.Stuck))
                    {
                        sbNotes.Append("(Sticky: leaving this square may require several attempts)\r\n");
                        strSymbol = "x"; // "↹";
                    }
                    if (flags.HasFlag(BT3MapSpecials.DrainHP) || flags.HasFlag(BT3MapSpecials.Explosion))
                    {
                        sbNotes.Append("(-1 HP)\r\n");
                        bDrain = true;
                    }
                    if (flags.HasFlag(BT3MapSpecials.Spinner))
                    {
                        sbNotes.Append("(Rotates the party to face a random direction)\r\n");
                        bSpinner = true;
                    }
                    if (flags.HasFlag(BT3MapSpecials.Trap))
                    {
                        sbNotes.Append("(random trap)\r\n");
                        bTrap = true;
                    }
                    if (flags.HasFlag(BT3MapSpecials.DrainSP))
                    {
                        sbNotes.Append("(-1d4 SP)\r\n");
                        bDrain = true;
                    }
                    if (flags.HasFlag(BT3MapSpecials.RestoreSP))
                        bRestoreSP = true;
                    if (flags.HasFlag(BT3MapSpecials.AlwaysEncounter))
                    {
                        sbNotes.Append("95% chance: Encounter\r\n");
                        strSymbol = "e!";
                    }
                    if (flags.HasFlag(BT3MapSpecials.HPRegen))
                        bRestoreHP = true;
                    else if (flags.HasFlag(BT3MapSpecials.Encounter))
                        bEncounter = true;
                    if (flags.HasFlag(BT3MapSpecials.Silence))
                        bSilence = true;
                    if ((flags & BT3MapSpecials.Unknown) != BT3MapSpecials.None)
                    {
                        sbNotes.AppendFormat("Unknown: {0:X6}\r\n", (int)(flags & BT3MapSpecials.Unknown));
                        strSymbol = "??";
                    }

                    if ((bTrap || bDrain) && (strSymbol == "" || strSymbol == "x"))
                    {
                        if (bDrain)
                            strSymbol += "d";
                        if (bTrap)
                            strSymbol += "t";
                        if (strSymbol.Length > 2)
                            strSymbol = strSymbol.Substring(strSymbol.Length - 2, 2);
                    }

                    if (bSpinner && strSymbol.Length == 1 && Char.IsDigit(strSymbol[0]))
                        strSymbol = "↺" + strSymbol;
                    else if (bSpinner && strSymbol.Length < 2)
                        strSymbol += "↺";

                    if (bEncounter)
                    {
                        sbNotes.Append("50% chance: Encounter\r\n");
                        if (strSymbol.Length < 2)
                            strSymbol += "e";
                    }
                }

                if (bRestoreSP && (bAntimagic || bSilence))
                {
                    // Antimagic/Silence will take over the square color, so leave this note instead
                    strSymbol = "SP";
                }

                List<string> skipSymbols = new List<string>();
                List<NoteInfo> scriptNotes = new List<NoteInfo>();
                BT3ScriptInfo info = data.GetScriptInfo();
                if (info.Scripts != null && info.Scripts.Scripts.ContainsKey(ptGame))
                {
                    List<GameScript> list = info.Scripts.Scripts[ptGame];
                    foreach (BT3Script script in list)
                    {
                        scriptNotes.Add(script.Summary(info, false, true));
                        foreach (BT3ScriptLine line in script.Lines)
                        {
                            if (line.IsTeleportCommand)
                            {
                                if (line.TeleportMapIndex == info.Map.Map)
                                {
                                    int iDeltaX = line.TeleportLocation.X - ptGame.X;
                                    int iDeltaY = line.TeleportLocation.Y - ptGame.Y;
                                    Point ptGridTo = new Point(ptGrid.X + (iDeltaX * iDeltaEast), ptGrid.Y + (iDeltaY * iDeltaSouth));
                                    MapSquare squareTo = m_sheet.GetSquareAtGridPoint(ptGridTo);
                                    if (squareTo != null)
                                        squareTo.AddNoteLine(String.Format("(The teleporter at {0},{1} sends the party here)", ptGame.X, ptGame.Y), "n", ptGridTo);
                                    if (strSymbol == "")
                                        strSymbol = "T";
                                }
                                else
                                {
                                    if (strSymbol == "" || strSymbol == "?")
                                        strSymbol = ".";
                                    square.AddUniqueIcon(new MapIcon(IconName.Exit, Direction.Up, ptGrid));
                                    skipSymbols.AddRange(new string[] { "T", "?" });
                                }
                            }
                        }
                    }
                }

                foreach (NoteInfo ni in scriptNotes)
                    sbNotes.AppendFormat("{0}\r\n", ni.Text);

                strSymbol = BT3Script.BestSingleSymbol(scriptNotes, strSymbol, skipSymbols);

                if (iUnknownFlags != 0)
                {
                    string strUnknown = String.Format("{0:X6}", iUnknownFlags);
                    sbNotes.AppendFormat("(Unknown flags: {0})\r\n", strUnknown);
                    strSymbol = (strSymbol + strUnknown.Replace("0", "") + "  ").Substring(0, 2).Trim();
                }

                if (bRestoreHP)
                {
                    sbNotes.Append("(This square has the \"HP Regen\" bit set but that does not appear to do anything in-game)\r\n");
                    if (strSymbol == "")
                        strSymbol = "HP";
                }

                if (sbNotes.Length > 0 && strSymbol == "")
                    strSymbol = "?";

                MapIcon iconFromScript = BT3Script.BestSingleIcon(scriptNotes);

                if (sbNotes.Length == 0 && (bAntimagic || bNoTeleport))
                {
                    // Silence and SP-Restore would normally be colored squares, but if they are also
                    // anti-magic or no-teleport squares (which have higher priority), use a normal
                    // note and symbol instead.
                    if (bNoTeleport && bAntimagic)
                    {
                        sbNotes.Append("(This square may not be the target of the Apport Arcane spell)\r\n");
                        if (strSymbol == "")
                            strSymbol = "n";
                    }
                    if (bSilence)
                    {
                        sbNotes.Append("(Any active Bard songs are silenced)\r\n");
                        if (strSymbol == "")
                            strSymbol = "si";
                    }
                    if (bRestoreSP)
                    {
                        sbNotes.Append("(The party regains 1 SP every 15 minutes while in this square)");
                        if (strSymbol == "")
                            strSymbol = "SP";
                    }
                }
                else if (sbNotes.Length > 0)
                {
                    // Reminder text, if the square already happens to have a note in it
                    if (bSilence)
                        sbNotes.Append("(Any active Bard songs are silenced)\r\n");
                    if (bRestoreSP)
                        sbNotes.Append("(The party regains 1 SP every 15 minutes while in this square)");
                }

                if (sbNotes.Length > 0)
                {
                    noteInfo = new NoteInfo(Global.Trim(sbNotes).ToString().Trim(), strSymbol, Color.Black);
                    if (iconFromScript != null)
                        noteInfo.Icon = iconFromScript;
                }

                if (bAntimagic)
                    square.Colors.BackColorPattern = bDark ? SquareStyleList.DefaultAntiMagicDark : SquareStyleList.DefaultAntiMagic;
                else if (bNoTeleport)
                    square.Colors.BackColorPattern = bDark ? SquareStyleList.DefaultNoTeleportDark : SquareStyleList.DefaultNoTeleport;
                else if (bSilence)
                    square.Colors.BackColorPattern = bDark ? SquareStyleList.DefaultDangerousDark : SquareStyleList.DefaultDangerous;
                else if (bRestoreSP)
                    square.Colors.BackColorPattern = bDark ? SquareStyleList.DefaultRegenDark : SquareStyleList.DefaultRegen;
                else if (bDark)
                    square.Colors.BackColorPattern = SquareStyleList.DefaultDark;

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

        public MapSquareData SetBTSquareFromGameBytes(Point ptGrid, BTMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            if (data is BT3MapData)
                return SetBT3SquareFromGameBytes(ptGrid, data as BT3MapData, horiz, vert, iDeltaEast, iDeltaSouth);

            MapSquare square = Grid[ptGrid.X, ptGrid.Y];
            MapSquareData msd = null;
            square.RemoveIcons(IconName.DoorHalf);
            square.RemoveIcons(IconName.ArrowHalf);


            if (data is BT2MapData && data.Width != 22)
            {
                bool bWilderness = (data.Index == (int)BT2Map.Wilderness);
                vert = data.Height - 1 - vert;
                int iSquare = data.Squares[vert * data.Width + horiz];

                if (bWilderness)
                {
                    MapXY match = null;
                    switch (iSquare)
                    {
                        case 0:
                            square.Colors.Background = Global.Lighten(Color.PaleGreen, 20);
                            break;
                        case 1:
                            square.Colors.Background = Color.Goldenrod;
                            break;
                        case 2:
                            square.Colors.Background = Color.Gray;
                            break;
                        case 3:
                            square.Colors.Background = Color.ForestGreen;
                            break;
                        case 4:
                            square.Colors.Background = Color.DarkGreen;
                            break;
                        case 5:
                            square.Colors.Background = Color.LightGray;
                            match = MapXY.Match(horiz, vert, data.Index, BT2.Spots.Towns);
                            if (match == BT2.Spots.Thessalonica)
                                square.Note = new MapNote("{map:Thessalonica} " + $"({BT2.Spots.ThessalonicaExit.X},{BT2.Spots.ThessalonicaExit.Y})", Color.Black, "Th", ptGrid);
                            else if (match == BT2.Spots.Colosse)
                                square.Note = new MapNote("{map: Colosse} " + $"({BT2.Spots.ColosseExit.X},{BT2.Spots.ColosseExit.Y})", Color.Black, "Cl", ptGrid);
                            else if (match == BT2.Spots.Tangramayne)
                                square.Note = new MapNote("{map:Tangramayne} " + $"({BT2.Spots.TangramayneExit.X},{BT2.Spots.TangramayneExit.Y})", Color.Black, "Ta", ptGrid);
                            else if (match == BT2.Spots.Philippi)
                                square.Note = new MapNote("{map:Philippi} " + $"({BT2.Spots.PhilippiExit.X},{BT2.Spots.PhilippiExit.Y})", Color.Black, "Ph", ptGrid);
                            else if (match == BT2.Spots.Corinth)
                                square.Note = new MapNote("{map:Corinth} " + $"({BT2.Spots.CorinthExit.X},{BT2.Spots.CorinthExit.Y})", Color.Black, "Co", ptGrid);
                            else if (match == BT2.Spots.Ephesus)
                                square.Note = new MapNote("{map:Ephesus} " + $"({BT2.Spots.EphesusExit.X},{BT2.Spots.EphesusExit.Y})", Color.Black, "Ep", ptGrid);
                            break;
                        default:
                            square.Colors.Background = Color.White;
                            match = MapXY.Match(horiz, vert, data.Index, BT2.Spots.Dungeons);
                            if (match == BT2.Spots.GreyCrypt)
                                square.Note = new MapNote("Grey Crypt", Color.Black, "Gr", ptGrid);
                            else if (match == BT2.Spots.FanskarsCastle)
                                square.Note = new MapNote("Fanskar's Castle", Color.Black, "Fa", ptGrid);
                            else if (match == BT2.Spots.Kazdek)
                                square.Note = new MapNote("Kazdek", Color.Black, "Ka", ptGrid);
                            else if (match == BT2.Spots.TempleOfNarn)
                                square.Note = new MapNote("Temple of Narn", Color.Black, "Na", ptGrid);
                            else if (match == BT2.Spots.SagesHut)
                                square.Note = new MapNote("Sage's Hut", Color.Black, "Sa", ptGrid);
                            else
                                square.Note = new MapNote($"[Special #{iSquare}]", Color.Black, "?", ptGrid);
                            break;
                    }
                }
                else
                {
                    // This is a town-style map; no walls
                    switch (iSquare)
                    {
                        case 0:
                            square.Colors.BackColorPattern = ColorPattern.Empty;
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            square.Colors.Background = Color.Gray;
                            break;
                        default:
                            square.Colors.Background = Color.FromArgb(192, 255, 196);
                            string strSpecial = BT2MemoryHacker.GetTownSpecial(iSquare);
                            square.Note = new MapNote(strSpecial, Color.Black, strSpecial.Substring(0, 2), ptGrid);
                            break;
                    }
                }
                return msd;
            }

            BTMapSquareInfo infoMain = BTMapSquareInfo.Create(data.Squares, vert, horiz, data.SwapWallsDoors > 0);
            BTMapSquareInfo infoNorth = BTMapSquareInfo.Create(data.Squares, vert - iDeltaSouth, horiz, data.SwapWallsDoors > 0);
            BTMapSquareInfo infoSouth = BTMapSquareInfo.Create(data.Squares, vert + iDeltaSouth, horiz, data.SwapWallsDoors > 0);
            BTMapSquareInfo infoEast = BTMapSquareInfo.Create(data.Squares, vert, horiz + iDeltaEast, data.SwapWallsDoors > 0);
            BTMapSquareInfo infoWest = BTMapSquareInfo.Create(data.Squares, vert, horiz - iDeltaEast, data.SwapWallsDoors > 0);

            NoteInfo noteInfo = null;

            SquareDirParams sd = new SquareDirParams();
            sd.Square = square;
            sd.GridPoint = ptGrid;
            sd.Unimportant = false;

            SetBTSquare(square, ptGrid, BTLineInfo(infoMain.North, infoNorth == null ? infoMain.North : infoNorth.South), Direction.Up);
            SetBTSquare(square, ptGrid, BTLineInfo(infoMain.East, infoEast == null ? infoMain.East : infoEast.West), Direction.Right);
            SetBTSquare(square, ptGrid, BTLineInfo(infoMain.South, infoSouth == null ? infoMain.South : infoSouth.North), Direction.Down);
            SetBTSquare(square, ptGrid, BTLineInfo(infoMain.West, infoWest == null ? infoMain.West : infoWest.East), Direction.Left);

            if (square.Lines.TopWidth == 1 && ptGrid.Y == 1)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Up, ptGrid));
            if (square.Lines.LeftWidth == 1 && ptGrid.X == 1)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Left, ptGrid));
            if (square.Lines.BottomWidth == 1 && ptGrid.Y == data.Bounds.Height)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Down, ptGrid));
            if (square.Lines.RightWidth == 1 && ptGrid.X == data.Bounds.Width)
                square.AddUniqueIcon(new MapIcon(IconName.ArrowHalf, Direction.Right, ptGrid));

            bool bBT2 = (data is BT2MapData);
            MapTitlePairDelegate titleFn = BT1MemoryHacker.GetMapTitlePair;
            string strEncounter = "Encounter";
            if (bBT2)
            {
                titleFn = BT2MemoryHacker.GetMapTitlePair;
                strEncounter = "25% chance: Encounter";
            }

            int iSpecial = 0;
            if (!data.LiveOnly)
            {
                bool bDark = false;
                if (data.Specials != null && data.Specials.Length > (vert * 22 + horiz) && square.Note == null)
                {
                    iSpecial = data.Specials[vert * 22 + horiz];
                    if ((iSpecial & 0x08) == 0x08)
                    {
                        bDark = true;
                        square.Colors.BackColorPattern = SquareStyleList.DefaultDark;
                    }
                    iSpecial &= ~0x08;
                    Point ptStairs = new Point(horiz, vert);
                    bool bSwapUpDown = (bBT2 && iSpecial != 0 && (
                        (data.Index & 0xff00) == 0x0300 || (data.Index & 0xff00) == 0x0500 || data.Index == (int)BT2Map.FanskarsCastle));
                    string strUp = bSwapUpDown ? "down" : "up";
                    string strDown = bSwapUpDown ? "up" : "down";
                    IconName iconUp = bSwapUpDown ? IconName.StairsDown : IconName.StairsUp;
                    IconName iconDown = bSwapUpDown ? IconName.StairsUp : IconName.StairsDown;
                    string strStairs = "There are stairs here, going {0}.  Do you wish to take them? (Yes/No)\r\nY: {{map:{1}}} ({2},{3})";
                    string strSymbol = "";

                    string strMessage = "";
                    if ((iSpecial & 0x04) > 0)
                    {
                        int iTeleport = data.GetTeleportIndex(horiz, vert);
                        if (iTeleport != -1)
                        {
                            Point ptTeleport = new Point(data.Teleport[iTeleport * 2 + 17] & 0x7F, data.Teleport[iTeleport * 2 + 16] & 0x7F);
                            strMessage = String.Format("Teleport to ({0},{1})", ptTeleport.X, ptTeleport.Y);
                            strSymbol = Global.Symbol(strSymbol, "T");
                            msd = new MapSquareData(ptTeleport, "n", String.Format("(The teleporter at {0},{1} sends the party here)", horiz, vert));
                        }
                        else
                        {
                            int iCustomIndex = data.GetCustomIndex(horiz, vert);
                            int iFixedIndex = data.GetFixedIndex(horiz, vert);
                            if (iFixedIndex == -1)
                            {
                                strMessage = String.Format("Message [{0}]", iCustomIndex);
                                strSymbol = Global.Symbol(strSymbol, "s");
                            }
                            else
                            {
                                strSymbol = Global.Symbol(strSymbol, "E");
                                int iMonster = data.FixedEncounters[iFixedIndex * 2 + 16];
                                int iCount = data.FixedEncounters[iFixedIndex * 2 + 17];
                                if (iCount > 0)
                                    strMessage = String.Format("Fixed Encounter with {0}", BT2MemoryHacker.MonsterCount(data.Monsters, iMonster, iCount));
                                else
                                    strMessage = "Unknown Encounter";
                            }
                        }
                    }

                    switch (iSpecial)
                    {
                        case 0x00:
                            break;
                        case 0x01:
                            noteInfo = new NoteInfo(String.Format(strStairs, strUp, titleFn(data.Index - 1).Title, ptStairs.X, ptStairs.Y), ".", Color.Black, new MapIcon(iconUp));
                            break;
                        case 0x02:
                            noteInfo = new NoteInfo(String.Format(strStairs, strDown, titleFn(data.Index + 1).Title, ptStairs.X, ptStairs.Y), ".", Color.Black, new MapIcon(iconDown));
                            break;
                        case 0x04:
                            noteInfo = new NoteInfo(strMessage, strSymbol, Color.Black);
                            break;
                        case 0x10:
                            noteInfo = new NoteInfo("25% chance: Random trap", "t", Color.Black);
                            break;
                        case 0x90:
                            noteInfo = new NoteInfo(strEncounter + " or random trap", "te", Color.Black);
                            break;
                        case 0x40:
                            noteInfo = new NoteInfo(String.Format("Ascend: {{map:{0}}} ({1},{2})",
                                titleFn(data.Index - 1).Title, ptStairs.X, ptStairs.Y), ".", Color.Black, new MapIcon(IconName.StairsUp));
                            break;
                        case 0x20:
                            noteInfo = new NoteInfo(String.Format("Descend: {{map:{0}}} ({1},{2})",
                                titleFn(data.Index + 1).Title, ptStairs.X, ptStairs.Y), ".", Color.Black, new MapIcon(IconName.StairsDown));
                            break;
                        case 0x80:
                            noteInfo = new NoteInfo(strEncounter, "e", Color.Black);
                            break;
                        default:
                            StringBuilder sb = new StringBuilder();
                            if ((iSpecial & 0x80) > 0)
                            {
                                int iFixedIndex = data.GetFixedIndex(horiz, vert);
                                if (iFixedIndex == -1)
                                {
                                    int iMonster = data.FixedEncounters[iFixedIndex * 2 + 16];
                                    int iCount = data.FixedEncounters[iFixedIndex * 2 + 17];
                                    if (iCount > 0)
                                        sb.AppendFormat("Fixed Encounter with {0}\r\n", BT2MemoryHacker.MonsterCount(data.Monsters, iMonster, iCount));
                                    else
                                        sb.AppendFormat("{0}\r\n", strEncounter);
                                }
                                else
                                    sb.AppendFormat("{0}\r\n", strEncounter);
                            }
                            if ((iSpecial & 0x04) > 0)
                                sb.AppendFormat("{0}\r\n", strMessage);
                            if ((iSpecial & 0x10) > 0)
                                sb.AppendFormat("25% chance: random trap\r\n");
                            if ((iSpecial & 0x01) > 0)
                                sb.AppendFormat(strStairs + "\r\n", strUp, titleFn(data.Index - 1).Title, ptStairs.X, ptStairs.Y);
                            if ((iSpecial & 0x02) > 0)
                                sb.AppendFormat(strStairs + "\r\n", strDown, titleFn(data.Index - 1).Title, ptStairs.X, ptStairs.Y);
                            if ((iSpecial & 0x20) > 0)
                                sb.AppendFormat("Descend: {{map:{0}}} ({1},{2})\r\n", titleFn(data.Index + 1).Title, ptStairs.X, ptStairs.Y);
                            if ((iSpecial & 0x40) > 0)
                                sb.AppendFormat("Ascend: {{map:{0}}} ({1},{2})\r\n", titleFn(data.Index - 1).Title, ptStairs.X, ptStairs.Y);
                            if (sb.Length > 2)
                                sb.Remove(sb.Length - 2, 2);
                            square.Note = new MapNote(sb.ToString(), Color.Black, "?", ptGrid);
                            break;
                    }
                }

                if (data.Specials != null && data.Specials.Length > (22 * 22 + vert * 22 + horiz))
                {
                    iSpecial = data.Specials[22 * 22 + vert * 22 + horiz];
                    bool bSpinner = (iSpecial & 0x01) > 0;
                    bool bAntimagic = (iSpecial & 0x02) > 0;
                    bool bLowerHP = (iSpecial & 0x04) > 0;
                    bool bSilence = (iSpecial & 0x10) > 0;
                    bool bRaiseSP = (iSpecial & 0x20) > 0;
                    bool bLowerSP = (iSpecial & 0x40) > 0;

                    string sSymbol = noteInfo == null ? "" : noteInfo.Symbol;

                    if (iSpecial > 0)
                    {
                        if (bAntimagic)
                        {
                            if (!bDark)
                                square.Colors.BackColorPattern = SquareStyleList.DefaultAntiMagic;
                            else
                                square.Colors.BackColorPattern = SquareStyleList.DefaultAntiMagicDark;
                        }

                        StringBuilder sbExtra = new StringBuilder();
                        if (noteInfo != null && !String.IsNullOrWhiteSpace(noteInfo.Text))
                            sbExtra.Append("\r\n");

                        if (bSilence)
                        {
                            if (!bAntimagic && !bDark)
                                square.Colors.BackColorPattern = SquareStyleList.DefaultDangerous;
                            else
                                sbExtra.Append("(Any active Bard songs are silenced)\r\n");
                        }

                        if (bSpinner)
                        {
                            sSymbol = Global.Symbol(sSymbol, "↺");
                            sbExtra.Append("(Rotates the party to face a random direction)\r\n");
                        }
                        if (bLowerHP)
                        {
                            sSymbol = Global.Symbol(sSymbol, "t");
                            sbExtra.Append("(-3 HP)\r\n");
                        }
                        if (bRaiseSP)
                            sbExtra.Append("(The party slowly regains SP while in this square)\r\n");
                        if (bLowerSP)
                        {
                            sSymbol = Global.Symbol(sSymbol, "t");
                            sbExtra.Append("(-2 SP)\r\n");
                        }

                        Global.Trim(sbExtra);
                        if (noteInfo == null)
                            noteInfo = new NoteInfo(sbExtra.ToString(), sSymbol, Color.Black);
                        else
                            noteInfo.Text += sbExtra.ToString();
                    }
                }

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

    [Flags]
    public enum BTWall
    {
        Open =    0x00,
        Wall =    0x01,
        Door =    0x02,
        False =   0x10,
        NoPhase = 0x20,
        Blue =    0x40,

        BlueWall = Blue | Wall,
        BlueDoor = Blue | Door,
        FalseWall = False | Wall,
        FalseOpen = False,
        NoPhaseWall = NoPhase | Wall,
        NoPhaseDoor = NoPhase | Door,
        NoPhaseBlueWall = NoPhase | Blue | Wall,
        NoPhaseBlueDoor = NoPhase | Blue | Door,
    }

    public class BTMapSquareInfo
    {
        public BTWall North;
        public BTWall East;
        public BTWall South;
        public BTWall West;

        public BTMapSquareInfo()
        {
        }

        public static BTWall BT12Wall(int i)
        {
            switch (i)
            {
                case 0: return BTWall.Open;
                case 1: return BTWall.Wall;
                case 2: return BTWall.Door;
                case 3: return BTWall.FalseWall;
                default: return BTWall.Open;
            }
        }

        public BTMapSquareInfo(byte b, bool bSwapWallsDoors)
        {
            West = Swap(bSwapWallsDoors, BT12Wall((b & 0xC0) >> 6));
            East = Swap(bSwapWallsDoors, BT12Wall((b & 0x30) >> 4));
            South = Swap(bSwapWallsDoors, BT12Wall((b & 0x0C) >> 2));
            North = Swap(bSwapWallsDoors, BT12Wall(b & 0x03));
        }

        public static BTWall BT3Wall(int i)
        {
            // 9-15 can't be altered by Phase Door 
            // 6-12, 14, 15 are blue (others are green)
            switch (i)
            {
                case 13:
                case 5: return BTWall.FalseOpen;
                case 1:
                case 6: return BTWall.Wall;
                case 8: return BTWall.BlueWall;
                case 9:
                case 14: return BTWall.NoPhaseBlueWall;
                case 2:
                case 3:
                case 4: return BTWall.Door;
                case 7: return BTWall.BlueDoor;
                case 11:
                case 10:
                case 12:
                case 15: return BTWall.NoPhaseBlueDoor;
                default: return BTWall.Open;
            }
        }

        public static BT3MapSpecials GetBT3Specials(BT3MapData data, int vert, int horiz)
        {
            if (data == null || vert < 0 || horiz < 0 || vert >= data.Height || horiz >= data.Width)
                return BT3MapSpecials.None;
            int iOffset = vert * data.Width * data.BytesPerSquare + (horiz * data.BytesPerSquare);
            if (iOffset < 0 || iOffset >= data.Squares.Length - 1)
                return BT3MapSpecials.None;

            return (BT3MapSpecials)((data.Squares[iOffset + 2] << 16) | (data.Squares[iOffset + 3] << 8) | data.Squares[iOffset + 4]);
        }

        public static BTMapSquareInfo CreateBT3(BT3MapData data, int vert, int horiz)
        {
            if (vert < 0 || horiz < 0 || vert >= data.Height || horiz >= data.Width)
                return null;
            BTMapSquareInfo info = new BTMapSquareInfo();
            int iOffset = vert * data.Width * data.BytesPerSquare + (horiz * data.BytesPerSquare);
            if (iOffset < 0 || iOffset >= data.Squares.Length - 1)
                return null;

            info.North = BT3Wall(data.Squares[iOffset] >> 4);
            info.East = BT3Wall(data.Squares[iOffset] & 0x0f);
            info.South = BT3Wall(data.Squares[iOffset + 1] >> 4);
            info.West = BT3Wall(data.Squares[iOffset + 1] & 0x0f);
            return info;
        }

        public static BTMapSquareInfo Create(byte[] data, int vert, int horiz, bool bSwapWallsDoors)
        {
            int iByte = vert * 22 + horiz;

            if (iByte < 0 || iByte >= data.Length)
                return null;

            return new BTMapSquareInfo(data[iByte], bSwapWallsDoors);
        }

        public static BTWall Swap(bool bSwapWallsDoors, BTWall wall)
        {
            if (!bSwapWallsDoors)
                return wall;
            switch (wall)
            {
                case BTWall.Door: return BTWall.Wall;
                case BTWall.Wall: return BTWall.Door;
                default: return wall;
            }
        }
    }
}
