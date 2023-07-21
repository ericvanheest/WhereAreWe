using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class WizMaps : MapsBase
    {
        public WizMaps(MapSheet sheet)
        {
            m_sheet = sheet;
        }

        private CheckBarrierResult CheckBarrier(MapSquare square, Point ptGrid, Wiz5SpecialSquares special, WizWall wwFore, WizWall wwReverse,
            int iValue, int iXOpposite, int iYOpposite, Direction dir)
        {
            if (wwFore == WizWall.Dependent)
                wwFore = DependentCopy(wwReverse);
            else if (wwReverse == WizWall.Dependent)
                wwReverse = DependentCopy(wwFore);

            // Check locked doors
            if (wwFore == WizWall.Door && (iValue == 1 || iValue == 2))
            {
                if (square.Icons == null)
                    square.Icons = new List<MapIcon>(1);

                for (int i = 0; i < square.Icons.Count; i++)
                {
                    if (square.Icons[i].Name == IconName.DoorHalf && square.Icons[i].Orientation == dir)
                    {
                        square.Icons[i].Name = IconName.LockedHalf;
                        return CheckBarrierResult.Lock;
                    }
                }

                square.Icons.Add(new MapIcon(IconName.LockedHalf, dir, ptGrid));

                return CheckBarrierResult.Lock;
            }

            if (wwFore == WizWall.Door && iValue == 3)
            {
                square.RemoveIcons(IconName.DoorHalf, Direction.Left);
                return CheckBarrierResult.RemoveDoor;
            }

            // Check invisible barriers
            if (wwFore != WizWall.Open || wwReverse != WizWall.Open || iValue != 3)
                return CheckBarrierResult.None;

            square.Line(dir, MapLineInfo.RedLine2);

            if (wwReverse != WizWall.Open)
                return CheckBarrierResult.None;

            Point ptOpposite = new Point(iXOpposite, iYOpposite);
            MapSquare squareOpposite = m_sheet.GetSquareAtGridPoint(ptOpposite);
            if (squareOpposite == null)
                return CheckBarrierResult.None;

            Direction dirOpposite = Global.Opposite(dir);

            squareOpposite.Line(dirOpposite, MapLineInfo.RedLine2);
            if (squareOpposite.Icons != null && squareOpposite.Icons.Any(i => i.Name == IconName.ArrowHalf && i.Orientation == dirOpposite))
                return CheckBarrierResult.Barrier;

            squareOpposite.Icons.Add(new MapIcon(IconName.ArrowHalf, dirOpposite, ptOpposite));
            return CheckBarrierResult.Barrier;
        }

        public void SetWizSquareFromGameBytes(MapBook book, Point ptGrid, WizardryMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            if (!m_sheet.PointInGrid(ptGrid))
                return;

            bool bWiz4 = (data.Game == GameNames.Wizardry4);
            bool bWiz5 = (data.Game == GameNames.Wizardry5);
            Point ptGame = new Point(horiz, vert);
            MapSquare square = Grid[ptGrid.X, ptGrid.Y];

            if (data.LiveOnly)
                m_sheet.RemoveLiveData(square);

            WizWall wwWest = data.GetWest(horiz, vert);
            WizWall wwNorth = data.GetNorth(horiz, vert);
            WizWall wwEast = data.GetEast(horiz, vert);
            WizWall wwSouth = data.GetSouth(horiz, vert);

            WizWall wwWestOpp = data.GetEast(horiz - iDeltaEast, vert);
            WizWall wwNorthOpp = data.GetSouth(horiz, vert - iDeltaSouth);
            WizWall wwEastOpp = data.GetWest(horiz + iDeltaEast, vert);
            WizWall wwSouthOpp = data.GetNorth(horiz, vert + iDeltaSouth);

            WizWall[] walls = new WizWall[] { wwWest, wwNorth, wwEast, wwSouth };

            SetWizWall(ptGrid, ptGame, square, Direction.Left, wwWest, wwWestOpp, !bWiz5);
            SetWizWall(ptGrid, ptGame, square, Direction.Up, wwNorth, wwNorthOpp, !bWiz5);
            SetWizWall(ptGrid, ptGame, square, Direction.Right, wwEast, wwEastOpp, !bWiz5);
            SetWizWall(ptGrid, ptGame, square, Direction.Down, wwSouth, wwSouthOpp, !bWiz5);

            if (walls.Count(w => (w == WizWall.SparseRock || w == WizWall.Dependent)) > 2)
                square.Colors.BackColorPattern = Global.SquareStyles.List[SquareStyleList.Name.Solid];
            else if (!data.LiveOnly)
                square.Colors.Background = Color.White;

            if (data.LiveOnly || data.Fights == null)
                return;

            if (data is Wiz5MapData)
            {
                Wiz5MapData w5Map = (Wiz5MapData)data;
                if (w5Map.Sections != null)
                {
                    foreach (MapSection section in w5Map.Sections)
                    {
                        if (!section.External && section.Source.Contains(new Point(ptGame.X - data.Bounds.Left, ptGame.Y)))
                        {
                            ptGame = new Point(section.Target.X + (ptGame.X - (data.Bounds.Left + section.Source.Left)), section.Target.Y + (ptGame.Y - section.Source.Y));
                            break;
                        }
                    }
                }

                if (w5Map.East is Wiz5MapData.Wiz5WallSet && ((Wiz5MapData.Wiz5WallSet)w5Map.East).GetDark(horiz, vert))
                    square.Colors.BackColorPattern = SquareStyleList.DefaultDark;
                else if (w5Map.East is Wiz5MapData.Wiz5WallSet && ((Wiz5MapData.Wiz5WallSet)w5Map.East).GetInaccessible(horiz, vert))
                    square.Colors.BackColorPattern = SquareStyleList.DefaultInaccessible;

                if (w5Map.Special == null || !w5Map.Special.Squares.ContainsKey(ptGame))
                    return;

                foreach (Wiz5SpecialSquare w5Spec in w5Map.Special.Squares[ptGame])
                {
                    string strRaw = String.Empty;
                    string strVal3 = String.Empty;
                    string strVal1 = String.Empty;
                    int iVal3Quad3 = (w5Spec.Value3 & 0xf000) >> 12;
                    int iVal3Quad2 = (w5Spec.Value3 & 0x0f00) >> 8;
                    int iVal3Quad1 = (w5Spec.Value3 & 0x00f0) >> 4;
                    int iVal3Quad0 = w5Spec.Value3 & 0x000f;

                    if (w5Spec.Type != Wiz5SquareType.None)
                    {
                        strRaw = String.Format("[{0:X1},{1:X2},{2:X4},{3:X2},{4:X4}] ", (int)w5Spec.Type, w5Spec.Value0, w5Spec.Value1, w5Spec.Value2, w5Spec.Value3);
                        strVal1 = String.Format("{0} ({1}, {2})", w5Spec.Value1, (w5Spec.Value1 & 0xff00) >> 8, w5Spec.Value1 & 0x00ff);
                        strVal3 = String.Format("{0} ({1}, {2})", w5Spec.Value3, (w5Spec.Value3 & 0xff00) >> 8, w5Spec.Value3 & 0x00ff);
                    }
                    switch (w5Spec.Type)
                    {
                        case Wiz5SquareType.AlterMap:
                            CheckBarrierResult[] cbNESW = new CheckBarrierResult[4];
                            cbNESW[0] = CheckBarrier(square, ptGrid, w5Map.Special, wwNorth, wwNorthOpp, w5Spec.Value3 & 0x0003, ptGrid.X, ptGrid.Y - 1, Direction.Up);
                            cbNESW[1] = CheckBarrier(square, ptGrid, w5Map.Special, wwEast, wwEastOpp, (w5Spec.Value3 & 0x000C) >> 2, ptGrid.X + 1, ptGrid.Y, Direction.Right);
                            cbNESW[2] = CheckBarrier(square, ptGrid, w5Map.Special, wwSouth, wwSouthOpp, (w5Spec.Value3 & 0x0030) >> 4, ptGrid.X, ptGrid.Y + 1, Direction.Down);
                            cbNESW[3] = CheckBarrier(square, ptGrid, w5Map.Special, wwWest, wwWestOpp, (w5Spec.Value3 & 0x00C0) >> 6, ptGrid.X - 1, ptGrid.Y, Direction.Left);
                            if (cbNESW.All(cb => cb == CheckBarrierResult.None))
                                square.AddNoteLine(strRaw + "Remove locks and barriers", "?", ptGrid);
                            else
                            {
                                StringBuilder sbAlter = new StringBuilder();
                                for (int i = 0; i < 4; i++)
                                {
                                    if (cbNESW[i].HasFlag(CheckBarrierResult.Lock))
                                        sbAlter.AppendFormat("Lock {0} door, ", Global.NESW[i]);
                                    if (cbNESW[i].HasFlag(CheckBarrierResult.RemoveDoor))
                                        sbAlter.AppendFormat("Remove {0} door, ", Global.NESW[i]);
                                    if (cbNESW[i].HasFlag(CheckBarrierResult.Barrier))
                                        sbAlter.AppendFormat("Create {0} barrier, ", Global.NESW[i]);
                                }
                                Global.Trim(sbAlter);
                                square.AddNoteLine(strRaw + sbAlter, "?", ptGrid);
                            }
                            break;
                        case Wiz5SquareType.Encounter:
                            square.AddNoteLine(strRaw + String.Format("Forced Encounter with {0}", Wiz5.MonsterName(w5Spec.Value2)), "E", ptGrid);
                            break;
                        case Wiz5SquareType.Receive:
                            square.AddNoteLine(strRaw + String.Format("+Item \"{0}\"", Wiz5.ItemName(w5Spec.Value2)), "?", ptGrid);
                            break;
                        case Wiz5SquareType.CheckItem:
                            square.AddNoteLine(strRaw + String.Format("If you have \"{0}\": ?", Wiz5.ItemName(w5Spec.Value2)), "?", ptGrid);
                            break;
                        case Wiz5SquareType.Trap:
                            if (iVal3Quad3 == 15 && iVal3Quad2 == 0)
                                square.AddNoteLine(strRaw + String.Format("Trap: {0}d{1} damage", iVal3Quad1, iVal3Quad0), "?", ptGrid);
                            else if (iVal3Quad3 == 15 && iVal3Quad0 == 0)
                                square.AddNoteLine(strRaw + String.Format("Trap: {0}d{1} damage", iVal3Quad2, iVal3Quad1), "?", ptGrid);
                            else
                                square.AddNoteLine(strRaw + String.Format("Trap: {0}; {1}", strVal1, strVal3), "?", ptGrid);
                            if (w5Map.Special.Squares[ptGame].Count == 1)
                                square.Note.Symbol = "t";
                            break;
                        case Wiz5SquareType.SwitchMap:
                            Wiz5LocationInt li = new Wiz5LocationInt(w5Spec.Value3);
                            square.AddNoteLine(strRaw + String.Format("{{map:Maze Level {0}}} ({1},{2},{3})", li.Level, li.Section, li.X, li.Y), "?", ptGrid);
                            IconName icon = (li.Level < data.Index ? IconName.StairsUp : li.Level > data.Index ? IconName.StairsDown : IconName.None);
                            if (icon != IconName.None)
                            {
                                if (square.Icons == null)
                                    square.Icons = new List<MapIcon>(1);
                                if (!square.Icons.Any(i => i.Name == icon))
                                    square.Icons.Add(new MapIcon(icon, Direction.Up, ptGrid));
                            }
                            if (w5Map.Special.Squares[ptGame].Count == 1)
                                square.Note.Symbol = icon == IconName.None ? "T" : ".";
                            break;
                        case Wiz5SquareType.Move:
                            string strSquares = Global.Plural(w5Spec.Value3 & 0xff, "square");
                            if ((w5Spec.Value3 & 0x0100) > 0)
                                square.AddNoteLine(strRaw + String.Format("Move north {0}", strSquares), "↑", ptGrid);
                            if ((w5Spec.Value3 & 0x0200) > 0)
                                square.AddNoteLine(strRaw + String.Format("Move east {0}", strSquares), "→", ptGrid);
                            if ((w5Spec.Value3 & 0x0400) > 0)
                                square.AddNoteLine(strRaw + String.Format("Move south {0}", strSquares), "↓", ptGrid);
                            if ((w5Spec.Value3 & 0x0800) > 0)
                                square.AddNoteLine(strRaw + String.Format(" Move west {0}", strSquares), "←", ptGrid);
                            break;
                        case Wiz5SquareType.Message:
                            square.AddNoteLine(strRaw + String.Format("Inspect Hidden Items: {0}, {1}", w5Spec.Value2, strVal3), "?", ptGrid);
                            break;
                        case Wiz5SquareType.BlueMessage:
                            square.AddNoteLine(strRaw + String.Format("BlueMessage: {0}, {1}", w5Spec.Value2, strVal3), "?", ptGrid);
                            break;
                        case Wiz5SquareType.None:
                            break;
                        default:
                            square.AddNoteLine(strRaw + String.Format("Event {0}: {1}, {2}", (int)w5Spec.Type, w5Spec.Value2, strVal3), "?", ptGrid);
                            break;
                    }
                }

                return;
            }

            if (data.Fights[horiz, vert])
            {
                if (bWiz4)
                    square.Note = new MapNote("Encounter Square (battle with one of the fixed 6-character parties, or lesser parties if those are all defeated)",
                        Color.Black, "e", ptGrid);
                else
                    square.Note = new MapNote("Encounter Square (battles here provide items the first time and double gold otherwise)",
                        Color.Black, "e", ptGrid);
            }

            StringBuilder sbNote = new StringBuilder();
            string strMonster = String.Empty;
            int iExtra = data.GetExtra(horiz, vert);
            Point ptTeleport = Point.Empty;
            int iNewMap = -1;
            if (iExtra < 16 && iExtra >= 0)
            {
                int aux0 = data.Aux0[iExtra];
                int aux1 = data.Aux1[iExtra];
                int aux2 = data.Aux2[iExtra];
                switch (data.Types[iExtra])
                {
                    case WizSquare.Dark:
                        if (aux2 == 1)
                        {
                            if (bWiz4)
                            {
                                // Doors in this mode are fake
                                foreach (MapIcon icon in square.Icons)
                                {
                                    if (icon.Name == IconName.DoorHalf)
                                        icon.Name = IconName.GrateHalf;
                                }
                            }
                            else
                                square.Colors.BackColorPattern = SquareStyleList.DefaultDim;
                        }
                        else
                            square.Colors.BackColorPattern = SquareStyleList.DefaultDark;
                        break;
                    case WizSquare.Stairs:
                        bool bUp = aux0 < data.Index;
                        if (data.Game == GameNames.Wizardry3)
                            bUp = !bUp;
                        square.Note = new MapNote(String.Format("Stairs going {0}: {{map:{1}}} ({2},{3})", bUp ? "up" : "down",
                            Games.GetMapTitle(data.Game, aux0), aux2, aux1), Color.Black, ".", ptGrid);
                        square.Icons.Add(new MapIcon(bUp ? IconName.StairsUp : IconName.StairsDown, Direction.Up, ptGrid));
                        break;
                    case WizSquare.Ouchy:
                    case WizSquare.Pit:
                        if (bWiz4)
                        {
                            square.Colors.BackColorPattern = SquareStyleList.DefaultAntiMagic;
                            square.Note = new MapNote(String.Format("Trap: {0} damage.",
                                aux1 == 1 && aux2 == 1 ? (aux0 + 1).ToString() :
                                new DamageDice(aux1, aux2, aux0).ToString()),
                                Color.Black, "t", ptGrid);
                        }
                        else
                            square.Note = new MapNote(String.Format("{0}: {1} damage.  Chance to avoid the pit: 4% for each point of Agility{2}",
                                data.Types[iExtra] == WizSquare.Ouchy ? "Ouch!!" : "Pit",
                                new DamageDice(aux1, aux2, aux0).ToString(), data.Index > 1 ? String.Format(" over {0}", data.Index - 1) : ""),
                                Color.Black, "t", ptGrid);
                        break;
                    case WizSquare.ScenarioMessage:
                        switch (aux2)
                        {
                            case 2:
                                sbNote.AppendFormat("Message {0}\r\nReceive \"{1}\" (unless you already have one)", aux1, Games.GetItemName(data.Game, aux0));
                                break;
                            case 4:
                                if (aux0 >= 0)
                                    sbNote.AppendFormat("Message {0}\r\nSearch: Forced Encounter with {1}", aux1, Games.GetMonsterName(data.Game, aux0));
                                else
                                    sbNote.AppendFormat("Message {0}\r\nSearch: Receive \"{1}\" (unless you already have one)", aux1, Games.GetItemName(data.Game, -aux0 % 1000));
                                break;
                            case 5:
                                sbNote.AppendFormat("Message {0}\r\nIf you do not have \"{1}\": (Teleport back one square)", aux1, Games.GetItemName(data.Game, aux0));
                                break;
                            case 9:
                                sbNote.AppendFormat("Message {0}\r\n(Sets the {1}x{1} area surrounding this square to forced encounters)", aux1, aux0 * 2 + 1);
                                break;
                            default:
                                sbNote.AppendFormat("Message {0}", aux1);
                                break;
                        }
                        switch (aux1)
                        {
                            case 3:
                                sbNote.Append(" (Teleport to the Castle)");
                                break;
                            default:
                                break;
                        }
                        sbNote.AppendFormat("\r\n[{0}, {1}, {2}]", aux0, aux1, aux2);
                        square.Note = new MapNote(sbNote.ToString(), Color.Black, String.Format("m{0:X}", iExtra), ptGrid);
                        break;
                    case WizSquare.Chute:
                        ptTeleport = new Point(aux2, aux1);
                        iNewMap = aux0;
                        square.Note = new MapNote(String.Format("Chute: {{map:{0}}} {1},{2}",
                            Wiz1MemoryHacker.GetMapTitlePair(iNewMap).Title, ptTeleport.X, ptTeleport.Y), Color.Black, "Ch", ptGrid);
                        break;
                    case WizSquare.Transfer:
                        ptTeleport = new Point(aux2, aux1);
                        iNewMap = aux0;
                        if (iNewMap == data.Index)
                        {
                            Point ptTeleportGrid = book.TranslateLocationToMap(ptTeleport, m_sheet);
                            square.Note = new MapNote(String.Format("(Teleport to {0},{1})", ptTeleport.X, ptTeleport.Y), Color.Black, "T", ptGrid);
                            MapSquare squareTarget = m_sheet.GetSquareAtGridPoint(ptTeleportGrid);
                            if (squareTarget != null)
                            {
                                string strNote = String.Format("(The teleporter at {0},{1} sends you here)", horiz, vert);
                                MapNote note = squareTarget.Note;
                                if (note == null)
                                    squareTarget.Note = new MapNote(strNote, Color.Black, "n", ptTeleportGrid);
                                else
                                    note.Text = note.Text + ("\r\n" + strNote);
                            }
                        }
                        else
                        {
                            square.Note = new MapNote(String.Format("(Teleport to {{map:{0}}} {1},{2})",
                                Games.GetMapTitle(data.Game, iNewMap), ptTeleport.X, ptTeleport.Y), Color.Black, "T", ptGrid);
                        }
                        break;
                    case WizSquare.Spinner:
                        if (bWiz4)
                        {
                            string strRotateSym = (aux1 == 3 ? "↺" : "↻");
                            string strRotate = (aux1 == 3 ? "counter-clockwise" : "clockwise");
                            if (aux0 == -1 && aux2 == 1)
                                square.Note = new MapNote(String.Format("(Entering or camping in the square rotates the walls {0})", strRotate), Color.Black, strRotateSym, ptGrid);
                            else if (aux0 == -1 && aux2 == 0)
                                square.Note = new MapNote(String.Format("(Leaving this square rotates the walls {0})", strRotate), Color.Black, strRotateSym, ptGrid);
                            else if (aux0 == -1 && aux1 == 3 && aux2 == 4)
                                square.Note = new MapNote("(Sets the facing direction randomly)", Color.Black, "↻", ptGrid);
                            else
                                square.Note = new MapNote("(This square has the \"Spinner\" flags set but does not appear to actually do anything in the game)", Color.Black, "?", ptGrid);
                        }
                        else
                            square.Note = new MapNote("(Sets the facing direction randomly)", Color.Black, "↻", ptGrid);
                        break;
                    case WizSquare.Elevator:
                        square.Note = new MapNote(String.Format("Elevator: Moves the party between levels {0} and {1} of the Maze{2}",
                            aux2, aux1, aux0 > 0 ? " (and teleports the party to a random square on that level)" : ""), Color.Black, "↕", ptGrid);
                        break;
                    case WizSquare.Encounter:
                        if (aux1 < 2)
                            strMonster = Games.GetMonsterName(data.Game, aux2);
                        else
                            strMonster = String.Format("(#{0} to #{1})", aux2, aux2 + aux1 - 1);
                        square.Note = new MapNote(String.Format("Forced Encounter with {0}{1}", strMonster,
                            aux0 > 0 ? String.Format(" ({0} times)", aux0) : ""), Color.Black, "E", ptGrid);
                        break;
                    case WizSquare.Fizzle:
                        square.Colors.BackColorPattern = SquareStyleList.DefaultAntiMagic;
                        break;
                    case WizSquare.SolidRock:
                        square.Colors.Background = Global.SquareStyles.SolidColor;
                        square.Colors.BackgroundStyle = Global.SquareStyles.SolidPattern;
                        break;
                }
            }
        }

        private WizWall DependentCopy(WizWall wall)
        {
            switch (wall)
            {
                case WizWall.OffMap: return WizWall.SparseRock;
                default: return wall;
            }
        }

        private void SetWizWall(Point ptGrid, Point ptGame, MapSquare square, Direction dir, WizWall wall, WizWall wallOpposite, bool bWrap = true)
        {
            MapSquare squareOpposite = m_sheet.GetNextSquare(ptGrid, dir);

            if (wall == WizWall.Dependent)
                wall = DependentCopy(wallOpposite);
            else if (wallOpposite == WizWall.Dependent)
                wallOpposite = DependentCopy(wall);

            switch (wall)
            {
                case WizWall.Open:
                    switch (wallOpposite)
                    {
                        case WizWall.Door:
                        case WizWall.SolidWall:
                            square.Line(dir, MapLineInfo.BlackLine2);
                            square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            break;
                        case WizWall.SparseRock:
                            square.Line(dir, MapLineInfo.BlackLine2);
                            break;
                        case WizWall.HiddenDoor:
                            square.Line(dir, MapLineInfo.BlackDot2);
                            break;
                        case WizWall.OffMap:
                            if (bWrap)
                                square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            else
                                square.Line(dir, MapLineInfo.BlackLine2);
                            if (squareOpposite != null)
                                squareOpposite.Line(Global.Opposite(dir), bWrap ? GridLines : MapLineInfo.BlackLine2);
                            break;
                    }
                    break;
                case WizWall.Door:
                    square.Line(dir, MapLineInfo.BlackLine2);
                    square.Icons.Add(new MapIcon(IconName.DoorHalf, dir, ptGrid));
                    break;
                case WizWall.HiddenDoor:
                    switch (wallOpposite)
                    {
                        case WizWall.Door:
                            square.Line(dir, MapLineInfo.BlackLine2);
                            square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            break;
                        case WizWall.Open:
                        case WizWall.HiddenDoor:
                            square.Line(dir, MapLineInfo.BlackDot2);
                            break;
                        case WizWall.SolidWall:
                        case WizWall.SparseRock:
                            square.Line(dir, MapLineInfo.BlackLine2);
                            square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            break;
                        case WizWall.OffMap:
                            square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            if (squareOpposite != null)
                                squareOpposite.Line(Global.Opposite(dir), GridLines);
                            break;
                    }
                    break;
                case WizWall.SolidWall:
                case WizWall.SparseRock:
                    square.Line(dir, MapLineInfo.BlackLine2);
                    break;
            }
        }
    }
}
