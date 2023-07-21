using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum MMOutdoorTile
    {
        Grass = 0,
        Mountain = 1,
        SnowMountain = 2,
        LightForest = 3,
        DenseForest = 4,
        FirePlain = 5,
        WaterPlain = 6,
        EarthPlain = 7,
        AirPlain = 8,
        Desert = 9,
        Water = 10,
        Swamp = 11,
        Tundra = 12,
        TundraRoadEW = 13,
        Volcano = 14,
        DeadZone = 15,
        RoadEW = 16,
        RoadNS = 17,
        RoadNW = 18,
        RoadSW = 19,
        RoadSE = 20,
        RoadNE = 21,
        RoadWSE = 22,
        RoadNSE = 23,
        RoadWNS = 24,
        Castle = 25,
        Town = 26,
        Cave = 27,
        Oasis = 28,
        Island = 29,
        Invalid = 30,
        Rock = 31,

        Flag1 = 0x20,
        Flag2 = 0x40,
        Flag3 = 0x80,

        AllFlags = 0xe0
    }

    [Flags]
    public enum MM45OutdoorTile
    {
        // Floors
        None = 0x00000000,
        Dirt = 0x00000001,
        Grass = 0x00000002,
        Snow = 0x00000003,
        Swamp = 0x00000004,
        Lava = 0x00000005,
        Desert = 0x00000006,
        Road = 0x00000007,
        Water = 0x00000008,
        TFlr = 0x00000009,
        Sky = 0x0000000A,
        CRoad = 0x0000000B,
        Sewer = 0x0000000C,
        Cloud = 0x0000000D,
        Scorch = 0x0000000E,
        Space = 0x0000000F,

        // Walls
        GrayMountain = 0x00000010,
        LightForest = 0x00000020,
        DenseForest = 0x00000030,
        Clearing = 0x00000040,
        LightPine = 0x00000050,
        DensePine = 0x00000060,
        SnowMountain = 0x00000070,
        LightForest2 = 0x00000080,
        SwampMountain = 0x00000090,
        Volcano = 0x000000A0,
        PalmTree = 0x000000B0,
        BrownMountain = 0x000000C0,
        BurnedTwigs = 0x000000D0,
        Twigs = 0x000000E0,
        SpaceWall = 0x000000F0,

        // Objects
        Tower = 0x00000100,
        Tent = 0x00000200,
        Cave = 0x00000300,
        Fountain = 0x00000400,
        Castle = 0x00000500,
        Wagon = 0x00000600,
        Pyramid = 0x00000700,
        Town = 0x00000800,
        StoneDoor = 0x00000900,
        Sphinx = 0x00000A00,
        Hut = 0x00000B00,
        MountainCave = 0x00000C00,
        Shrine = 0x00000D00,

        FloorMask = 0x0000000F,
        WallMask = 0x000000F0,
        ObjMask = 0x00000F00,
        OverMask = 0x0000F000,
        AttrMask = 0x00FF0000,

        // Attributes

        Monster1 = 0x00010000,
        Monster2 = 0x00020000,
        Monster3 = 0x00040000,
        Object = 0x00080000,
        Script = 0x00100000,
        Drain = 0x00200000,
        Dangerous = 0x00400000,
        Grate = 0x00800000,

        Unknown = -1,
        Invalid = -1,
    }

    [Flags]
    public enum MM45IndoorTile
    {
        FloorMask = 0x00070000,
        WallMask = 0x000000F0,
        ObjMask = 0x00000F00,
        OverMask = 0x0000F000,
        AttrMask = 0x00FF0000,

        Unknown = -1,
        Invalid = -1,

        // Floors

        Transparent = 0,
        Dirt = 1,
        Grass = 2,
        Snow1 = 3,
        Road4 = 4,
        Fire = 5,
        Desert = 6,
        Road1 = 7,
        PurpleGrid2 = 8,
        RedGrid = 9,
        Sky = 10,
        Road2 = 11,
        Sewer = 12,
        Clouds = 13,
        Scorch = 14,
        Space = 15,

        // Virtual tiles (for default tilesets)
        Water = 0x01000000,
        BlackTile = 0x02000000,
        BlueStone = 0x03000000,

        // Attributes

        Inside = 0x00080000,
        Script = 0x00100000,
        Dangerous = 0x00400000,
    }

    public enum MM3OutdoorTile
    {
        Unknown,
        Invalid,
        ShallowWater,
        Water,
        Grass,
        Mountain,
        LightForest,
        DenseForest,
        Swamp,
        Wagon,
        Dirt,
        Road,
        Town,
        Temple,
        Shrubs,
        Shack,
        Castle,
        Pyramid,
        Cavern,
        Grove,
        Dungeon,
        TundraForest,
        TundraMountain,
        LavaRock,
        Lava,
        SwampForest,
        SwampMountain,
        Desert
    }

    public class MMMaps : MapsBase
    {
        public MMMaps(MapSheet sheet)
        {
            m_sheet = sheet;
        }

        public void SetMMSquareFromGameBytes(Point ptGrid, MMMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            MapSquare square = Grid[ptGrid.X, ptGrid.Y];

            if (data.LiveOnly)
                m_sheet.RemoveLiveData(square);
            else
                square.Colors.Background = Color.White;

            MMAreaStyle style = data.Outside ? MMAreaStyle.MM1Surface : MMAreaStyle.Dungeon;
            if (data is MM2MapData)
                style = data.Outside ? MMAreaStyle.MM2Surface : MMAreaStyle.MM2Dungeon;
            else if (data is MM3MapData)
                style = data.Outside ? MMAreaStyle.MM3Surface : MMAreaStyle.MM3Dungeon;
            else if (data is MM45MapData)
                style = data.Outside ? MMAreaStyle.MM45Surface : MMAreaStyle.MM45Dungeon;
            if (data.Town)
                style = MMAreaStyle.Town;
            else if (data.Castle)
                style = MMAreaStyle.Castle;
            else if (data.Mixed)
                style = MMAreaStyle.Mixed;


            MMMapSquareInfo infoMain = MMMapSquareInfo.Create(data, vert, horiz);
            MMMapSquareInfo infoNorth = MMMapSquareInfo.Create(data, vert - iDeltaSouth, horiz);
            MMMapSquareInfo infoSouth = MMMapSquareInfo.Create(data, vert + iDeltaSouth, horiz);
            MMMapSquareInfo infoEast = MMMapSquareInfo.Create(data, vert, horiz + iDeltaEast);
            MMMapSquareInfo infoWest = MMMapSquareInfo.Create(data, vert, horiz - iDeltaEast);

            Color background = Color.White;

            switch (style)
            {
                case MMAreaStyle.Dungeon:
                case MMAreaStyle.MM2Dungeon:
                case MMAreaStyle.Town:
                case MMAreaStyle.Castle:
                case MMAreaStyle.Mixed:
                case MMAreaStyle.MM3Dungeon:
                    break;
                case MMAreaStyle.MM45Dungeon:
                    background = Color.Transparent;
                    if (data is MM45MapData)
                    {
                        background = MMMapSquareInfo.GetBackColor((MM45MapData)data, (MM45IndoorTile)infoMain.Tile);
                    }
                    else
                        background = MMMapSquareInfo.GetBackColor((MM45IndoorTile)infoMain.Tile);
                    square.Colors.Background = background;
                    break;
                case MMAreaStyle.MM45Surface:
                    background = MMMapSquareInfo.GetBackColor((MM45OutdoorTile)infoMain.Tile);
                    square.Colors.BackgroundStyle = MMMapSquareInfo.GetBackStyle((MM45OutdoorTile)infoMain.Tile);
                    square.Colors.Background = background;
                    break;
                case MMAreaStyle.MM3Surface:
                    background = MMMapSquareInfo.GetBackColor((MM3OutdoorTile)infoMain.Tile);
                    if (!data.LiveOnly)
                    {
                        switch ((MM3OutdoorTile)infoMain.Tile)
                        {
                            case MM3OutdoorTile.Temple:
                                square.Note = new MapNote("Temple", Color.Black, "Te", ptGrid);
                                break;
                            case MM3OutdoorTile.Wagon:
                                square.Note = new MapNote("Wagon", Color.Black, "Wa", ptGrid);
                                break;
                            case MM3OutdoorTile.Town:
                                square.Note = new MapNote("Town", Color.Black, "To", ptGrid);
                                break;
                            case MM3OutdoorTile.Shack:
                                square.Note = new MapNote("Shack", Color.Black, "Sh", ptGrid);
                                break;
                            case MM3OutdoorTile.Castle:
                                square.Note = new MapNote("Castle", Color.Black, "Ca", ptGrid);
                                break;
                            case MM3OutdoorTile.Pyramid:
                                square.Note = new MapNote("Pyramid", Color.Black, "Py", ptGrid);
                                break;
                            case MM3OutdoorTile.Dungeon:
                                square.Note = new MapNote("Dungeon", Color.Black, "Du", ptGrid);
                                break;
                            case MM3OutdoorTile.Cavern:
                                square.Note = new MapNote("Cavern", Color.Black, "Cv", ptGrid);
                                break;
                        }
                    }
                    square.Colors.Background = background;
                    break;
                case MMAreaStyle.MM2Surface:
                    background = MMMapSquareInfo.GetBackColor((MMOutdoorTile)infoMain.Tile);
                    if (!data.LiveOnly)
                    {
                        switch (((MMOutdoorTile)infoMain.Tile) & ~MMOutdoorTile.AllFlags)
                        {
                            case MMOutdoorTile.Castle:
                                square.Note = new MapNote("Castle", Color.Black, "Ca", ptGrid);
                                break;
                            case MMOutdoorTile.Town:
                                square.Note = new MapNote("Town", Color.Black, "To", ptGrid);
                                break;
                        }
                    }
                    square.Colors.Background = background;
                    break;
                case MMAreaStyle.MM1Surface:
                    square.Colors.Background = Color.White;
                    break;
                default:
                    break;
            }

            NoteInfo noteInfo = null;

            if (!data.LiveOnly)
            {
                if (data is MM345MapData && square.Note == null)
                {
                    MM345MapData mmData = data as MM345MapData;
                    Point ptSquare = new Point(horiz, vert);
                    // Scripts have priority, then objects, then squares
                    if (mmData.ScriptInfo != null && mmData.ScriptInfo.Scripts != null && mmData.ScriptInfo.Scripts.Scripts.ContainsKey(ptSquare))
                    {
                        noteInfo = mmData.ScriptInfo.Scripts.GetNoteInfo(mmData.ScriptInfo, ptSquare);
                        MapNote note = new MapNote(noteInfo.Text, noteInfo.Color, noteInfo.Symbol);

                        square.Note = note;
                        square.Note.Location = ptGrid;
                    }
                    else if (mmData.VisibleObjects != null && mmData.VisibleObjects.ContainsKey(ptSquare))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (MM345VisibleObject vo in mmData.VisibleObjects[ptSquare])
                        {
                            sb.AppendFormat(String.Format("I{0}, Ux{1:X8}", vo.Image, vo.Unknown));
                            sb.AppendLine();
                        }
                        square.Note = new MapNote(sb.ToString(), Color.Black, "o", ptGrid);
                    }
                    else if (mmData.SpecialSquares != null && mmData.SpecialSquares.ContainsKey(ptSquare))
                    {
                        List<MM345SpecialSquare> list = mmData.SpecialSquares[ptSquare];
                        if (list.Count > 0)
                        {
                            if (list[0].Facing == DirectionFlags.All)
                                square.Note = new MapNote(String.Format("All: "), Color.Black, "?", ptGrid);
                            else
                                square.Note = new MapNote(String.Format("{0}: ", Global.DirectionString(list[0].Facing)), Color.Black, "?", ptGrid);
                        }
                    }

                    if (square.Note != null && data is MM45MapData && data.Outside)
                    {
                        switch (((MM45OutdoorTile)infoMain.Tile & MM45OutdoorTile.ObjMask))
                        {
                            case MM45OutdoorTile.Tower: square.Note.Symbol = "Tw"; break;
                            case MM45OutdoorTile.Tent: square.Note.Symbol = "Te"; break;
                            case MM45OutdoorTile.Cave: square.Note.Symbol = "Ca"; break;
                            case MM45OutdoorTile.Fountain: square.Note.Symbol = "Fo"; break;
                            case MM45OutdoorTile.Castle: square.Note.Symbol = "Ca"; break;
                            case MM45OutdoorTile.Wagon: square.Note.Symbol = "Wa"; break;
                            case MM45OutdoorTile.Pyramid: square.Note.Symbol = "Py"; break;
                            case MM45OutdoorTile.Town: square.Note.Symbol = "To"; break;
                            case MM45OutdoorTile.StoneDoor: square.Note.Symbol = "Du"; break;
                            case MM45OutdoorTile.Sphinx: square.Note.Symbol = "Sp"; break;
                            case MM45OutdoorTile.Hut: square.Note.Symbol = "Hu"; break;
                            case MM45OutdoorTile.MountainCave: square.Note.Symbol = "Ca"; break;
                            case MM45OutdoorTile.Shrine: square.Note.Symbol = "Sh"; break;
                            default: break;
                        }
                    }
                }
            }

            if (infoMain.AntiMagic)
            {
                square.Colors.Background = Color.Red;
                if (infoMain.Dark)
                    square.Colors.BackgroundStyle = HatchStyle.Percent50;
                else
                    square.Colors.BackgroundStyle = HatchStyle.Percent25;
            }
            else if (infoMain.Dark)
            {
                square.Colors.Background = Color.Black;
                square.Colors.BackgroundStyle = HatchStyle.Percent25;
            }
            else if (infoMain.Dangerous && !data.Outside && !(data is MM45MapData))
            {
                square.Colors.Background = Color.Yellow;
                square.Colors.BackgroundStyle = HatchStyle.Percent50;
            }
            if (infoMain.Special && square.Note == null && !data.LiveOnly)
            {
                square.Note = new MapNote("Special Square", Color.Black, "?", ptGrid);
            }

            bool bUnimportant = false;

            if (style == MMAreaStyle.MM3Dungeon || style == MMAreaStyle.MM45Dungeon)
            {
                // Fill in a light gray if this square is typically inaccessible
                // Apparently a wall is bashable if the next square is not all 9s or 1s (solid or nonsolid walls)
                if (
                    infoMain.IsWall(Direction.Up) && infoWest.EastSolid &&
                    infoMain.IsWall(Direction.Down) && infoNorth.SouthSolid &&
                    infoMain.IsWall(Direction.Left) && infoEast.WestSolid &&
                    infoMain.IsWall(Direction.Right) && infoSouth.NorthSolid &&
                    !infoMain.Openable(Direction.Up) &&
                    !infoMain.Openable(Direction.Down) &&
                    !infoMain.Openable(Direction.Left) &&
                    !infoMain.Openable(Direction.Right) &&
                    !infoNorth.Openable(Direction.Down) &&
                    !infoSouth.Openable(Direction.Up) &&
                    !infoEast.Openable(Direction.Left) &&
                    !infoWest.Openable(Direction.Right))
                {
                    bUnimportant = true;
                    if ((square.Note == null || String.IsNullOrWhiteSpace(square.Note.Text)) &&
                        (square.Icons == null || square.Icons.Count == 0))
                    {
                        // Only fill in an unimportant square with the solid color if it has no notes or icons
                        square.Colors.Background = Global.SquareStyles.SolidColor;
                        square.Colors.BackgroundStyle = Global.SquareStyles.SolidPattern;
                    }
                }
            }

            // Find opposite square for each

            SquareDirParams sd = new SquareDirParams();
            sd.Square = square;
            sd.GridPoint = ptGrid;
            sd.Tile = infoMain.Tile;
            sd.Style = style;
            sd.Unimportant = bUnimportant;

            sd.Set(square.Top, infoNorth.Tile, infoMain.NorthAppearance, infoMain.NorthSolid, infoNorth.SouthAppearance, infoNorth.SouthSolid, infoNorth.Exitable, Direction.Up);
            square.Top = GetSquareDirection(sd);

            sd.Set(square.Right, infoEast.Tile, infoMain.EastAppearance, infoMain.EastSolid, infoEast.WestAppearance, infoEast.WestSolid, infoEast.Exitable, Direction.Right);
            square.Right = GetSquareDirection(sd);

            sd.Set(square.Bottom, infoSouth.Tile, infoMain.SouthAppearance, infoMain.SouthSolid, infoSouth.NorthAppearance, infoSouth.NorthSolid, infoSouth.Exitable, Direction.Down);
            square.Bottom = GetSquareDirection(sd);

            sd.Set(square.Left, infoWest.Tile, infoMain.WestAppearance, infoMain.WestSolid, infoWest.EastAppearance, infoWest.EastSolid, infoWest.Exitable, Direction.Left);
            square.Left = GetSquareDirection(sd);

            if (noteInfo != null && noteInfo.Icon != null)
            {
                // The MapIcon object knows its x,y coordinates, but those are for the game map at this point, not the grid
                noteInfo.Icon.Location = ptGrid;
                square.AddUniqueIcon(noteInfo.Icon);
                if (noteInfo.Icon.Name == IconName.StairsDown || noteInfo.Icon.Name == IconName.StairsUp)
                {
                    square.Note.Symbol = ".";
                    // Remove a half-door from this square, if it exists
                    square.RemoveIcons(IconName.DoorHalf);
                }
            }

            // Fill in large areas of the same passable walls (ocean, swamp, etc) with thin lines instead of thick ones
            if (style == MMAreaStyle.MM1Surface)
            {
                if (square.Top.Pattern == DashStyle.Dot &&
                    square.Left.Pattern == DashStyle.Dot &&
                    square.Right.Pattern == DashStyle.Dot &&
                    square.Bottom.Pattern == DashStyle.Dot)
                {
                    if (!infoNorth.NorthSolid && infoMain.NorthAppearance == infoNorth.NorthAppearance)
                        square.Lines.TopWidth = 1;
                    if (!infoEast.EastSolid && infoMain.EastAppearance == infoEast.EastAppearance)
                        square.Lines.RightWidth = 1;
                    if (!infoSouth.SouthSolid && infoMain.SouthAppearance == infoSouth.SouthAppearance)
                        square.Lines.BottomWidth = 1;
                    if (!infoWest.WestSolid && infoMain.WestAppearance == infoWest.WestAppearance)
                        square.Lines.LeftWidth = 1;
                }
            }

            // Check the map edges for connecting maps
            if (data is MM45MapData && style == MMAreaStyle.MM45Surface && !data.LiveOnly)
            {
                MM45MapData mm45MapData = (MM45MapData)data;

                if (mm45MapData.MapBytes != null)
                {
                    // Make the edges solid if there are no connecting maps
                    if (horiz == 15 && mm45MapData.MapBytes.MapEast == 0)
                        square.Lines.RightPattern = (byte)DashStyle.Solid;
                    else if (horiz == 0 && mm45MapData.MapBytes.MapWest == 0)
                        square.Lines.LeftPattern = (byte)DashStyle.Solid;
                    if (vert == 15 && mm45MapData.MapBytes.MapNorth == 0)
                        square.Lines.TopPattern = (byte)DashStyle.Solid;
                    else if (vert == 0 && mm45MapData.MapBytes.MapSouth == 0)
                        square.Lines.BottomPattern = (byte)DashStyle.Solid;
                }
            }
        }

        private void AddConnectingMap(int map, int x, int y, string symbol)
        {
            if (map > 255)
                AddConnectingMap((MM5Map)(map % 256), x, y, symbol);
            else
                AddConnectingMap((MM4Map)map, x, y, symbol);
        }

        private void AddConnectingMap(MM4Map map, int x, int y, string symbol)
        {
            if ((int)map != 0)
                Grid[x, y].Note = new MapNote(String.Format("{{map:{0}}}", MM45MemoryHacker.GetMapTitlePair((int)map, 0).Title), Color.Black, symbol, new Point(x, y));
        }

        private void AddConnectingMap(MM5Map map, int x, int y, string symbol)
        {
            if ((int)map != 0)
                Grid[x, y].Note = new MapNote(String.Format("{{map:{0}}}", MM45MemoryHacker.GetMapTitlePair((int)map, 1).Title), Color.Black, symbol, new Point(x, y));
        }

        private void AddDiagConnectingMap(int map1, int map2, int x, int y)
        {
            if (map1 > 255)
                AddDiagConnectingMap((MM5Map)(map1 % 256), (MM5Map)(map2 % 256), x, y);
            else
                AddDiagConnectingMap((MM4Map)map1, (MM4Map)map2, x, y);
        }

        private void AddDiagConnectingMap(MM5Map map1, MM5Map map2, int x, int y)
        {
            if ((int)map1 == 0 || (int)map2 == 0)
                return;

            AddDiagConnectingMap(MM45MemoryHacker.GetMapTitlePair((int)map1, 1).Title,
                                 MM45MemoryHacker.GetMapTitlePair((int)map2, 1).Title, x, y);

        }

        private void AddDiagConnectingMap(MM4Map map1, MM4Map map2, int x, int y)
        {
            if ((int)map1 == 0 || (int)map2 == 0)
                return;

            AddDiagConnectingMap(MM45MemoryHacker.GetMapTitlePair((int)map1, 0).Title,
                                 MM45MemoryHacker.GetMapTitlePair((int)map2, 0).Title, x, y);

        }

        private void AddDiagConnectingMap(string strMap1, string strMap2, int x, int y)
        {
            string strSuffix = "";
            string strPrefix = "";
            bool bCreateSuffix = false;

            if (strMap1.EndsWith("Darkside Surface"))
                strSuffix = "Darkside Surface";
            else if (strMap1.EndsWith("Cloudside Surface"))
                strSuffix = "Cloudside Surface";
            else if (strMap1.Contains("Skyroad"))
            {
                strPrefix = "Skyroad";
                bCreateSuffix = true;
            }
            else
                return; // Can only combine surface maps and skyroads

            string strNew, strSymbol;

            if (x == 0 && y == 0)   // Northwest
            {
                strSymbol = "⇖";
                strNew = String.Format("{0}-{1}, {2}{3}", strMap2[0], strMap1[2], strPrefix, bCreateSuffix ? String.Format(" {0}{1}", strMap2[0], strMap1[2]) : strSuffix);
            }
            else if (x == 0)        // Southwest
            {
                strSymbol = "⇙";
                strNew = String.Format("{0}-{1}, {2}{3}", strMap1[0], strMap2[2], strPrefix, bCreateSuffix ? String.Format(" {0}{1}", strMap1[0], strMap2[2]) : strSuffix);
            }
            else if (y == 0)        // Northeast
            {
                strSymbol = "⇗";
                strNew = String.Format("{0}-{1}, {2}{3}", strMap2[0], strMap1[2], strPrefix, bCreateSuffix ? String.Format(" {0}{1}", strMap2[0], strMap1[2]) : strSuffix);
            }
            else                    // Southeast
            {
                strSymbol = "⇘";
                strNew = String.Format("{0}-{1}, {2}{3}", strMap1[0], strMap2[2], strPrefix, bCreateSuffix ? String.Format(" {0}{1}", strMap1[0], strMap2[2]) : strSuffix);
            }

            // "A-1, Surface" & "B-2, Surface" = "B-1, Surface"
            Grid[x, y].Note = new MapNote(String.Format("{{map:{0}}}", strNew), Color.Black, strSymbol, new Point(x, y));
        }

        private MapLineInfo GetSquareDirection(SquareDirParams sd)
        {
            Color colorEdge = Color.Black;
            bool bDoor = false;
            switch (sd.Style)
            {
                case MMAreaStyle.MM45Dungeon:
                    switch (sd.Visible)
                    {
                        case 0:  // Open
                        case 2:  // Pillars
                            if (sd.Solid || sd.OppositeSolid)
                            {
                                sd.Info.Color = (sd.Visible == 0 && sd.OppositeVisible == 0) ? Color.Red : Color.Black;
                                sd.Info.Pattern = DashStyle.Solid;
                                sd.Info.Width = 2;
                                // MM4/5 dungeons don't connect to other maps, so "not solid" isn't meaningful in this case
                                //if (!bSolid)
                                //    square.Icons.Add(new MapIcon(IconName.ArrowHalf, dir, ptGrid));
                            }
                            break;
                        case 3:  // Bashed Opening (rendered as such so that "Live" squares will not behave oddly
                            sd.Info.Color = Color.Black;
                            sd.Info.Pattern = DashStyle.Solid;
                            sd.Info.Width = 2;
                            sd.Square.Icons.Add(new MapIcon(IconName.FragileHalf, sd.Dir, sd.GridPoint));
                            break;
                        case 4:   // Closed Door
                        case 6:   // Open Door with Switch
                        case 7:   // Large Closed Door
                        case 9:   // Closed Grate with Switch
                        case 13:  // Closed Door
                        case 1:   // Open Door
                        case 14:  // Open Door
                        case 15:  // Open Door
                            bDoor = true;
                            goto case 5;
                        case 5:   // Solid Wall
                        case 8:   // Solid Wall
                        case 10:  // Tapestry
                        case 11:  // Wall
                        case 12:  // Torch
                            sd.Info.Color = Color.Black;
                            sd.Info.Pattern = DashStyle.Solid;
                            sd.Info.Width = 2;

                            if (sd.Visible == 9) // Grate
                                sd.Square.Icons.Add(new MapIcon(IconName.GrateHalf, sd.Dir, sd.GridPoint));
                            else if (bDoor)  // Doors
                                sd.Square.Icons.Add(new MapIcon(IconName.DoorHalf, sd.Dir, sd.GridPoint));
                            else if (sd.OppositeExitable && sd.Solid)
                            {
                                // Don't bother marking unimportant squares as fragile (they all are, and the icons are distracting)
                                if (!Global.SquareStyles.IsSolid(sd.Square) && !sd.Unimportant)
                                    sd.Square.Icons.Add(new MapIcon(IconName.FragileHalf, sd.Dir, sd.GridPoint));
                            }
                            else if (!sd.Solid)
                            {
                                if (sd.OppositeSolid)
                                    sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                                else
                                {
                                    sd.Info.Pattern = DashStyle.Dot;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case MMAreaStyle.MM3Dungeon:
                    switch (sd.Visible)
                    {
                        case 0:  // Open
                        case 5:  // Bashed Opening
                        case 7:  // Pillars
                            if (sd.Solid || sd.OppositeSolid)
                            {
                                sd.Info.Color = (sd.Visible == 0 && sd.OppositeVisible == 0) ? Color.Red : Color.Black;
                                sd.Info.Pattern = DashStyle.Solid;
                                sd.Info.Width = 2;
                                if (!sd.Solid)
                                    sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                            }
                            break;

                        case 1:  // Wall
                        case 3:  // Torch
                        case 2:  // Closed Door
                        case 4:  // Grate
                        case 6:  // Open Door
                            sd.Info.Color = Color.Black;
                            sd.Info.Pattern = DashStyle.Solid;
                            sd.Info.Width = 2;

                            if (sd.Visible == 2 || sd.Visible == 6)  // Doors
                                sd.Square.Icons.Add(new MapIcon(IconName.DoorHalf, sd.Dir, sd.GridPoint));
                            else if (sd.Visible == 4) // Grate
                                sd.Square.Icons.Add(new MapIcon(IconName.GrateHalf, sd.Dir, sd.GridPoint));
                            else if (sd.OppositeExitable && sd.Solid)
                            {
                                // Don't bother marking unimportant squares as fragile (they all are, and the icons are distracting)
                                if (!Global.SquareStyles.IsSolid(sd.Square) && !sd.Unimportant)
                                    sd.Square.Icons.Add(new MapIcon(IconName.FragileHalf, sd.Dir, sd.GridPoint));
                            }
                            else if (!sd.Solid)
                            {
                                if (sd.OppositeSolid)
                                    sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                                else
                                {
                                    sd.Info.Pattern = DashStyle.Dot;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case MMAreaStyle.Town:
                case MMAreaStyle.Castle:
                case MMAreaStyle.Mixed:
                case MMAreaStyle.Dungeon:
                case MMAreaStyle.MM2Dungeon:
                    if (sd.Visible != 0)
                    {
                        sd.Info.Color = Color.Black;
                        sd.Info.Pattern = DashStyle.Solid;
                        sd.Info.Width = 2;
                        if (sd.IsDoor)
                            sd.Square.Icons.Add(new MapIcon(IconName.DoorHalf, sd.Dir, sd.GridPoint));
                        else if (!sd.Solid)
                        {
                            if (sd.OppositeSolid)
                                sd.Square.Icons.Add(new MapIcon(IconName.FragileHalf, sd.Dir, sd.GridPoint));
                            else
                            {
                                sd.Info.Pattern = DashStyle.Dot;
                            }
                        }
                    }
                    else if (!sd.Solid && !sd.OppositeSolid && sd.OppositeVisible != 0)
                    {
                        // Strange case where you can pass through both ways but one of them is a door and one is open passageway
                        sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                    }
                    else if (sd.Solid || sd.OppositeSolid)
                    {
                        sd.Info.Color = (sd.Visible == 0 && sd.OppositeVisible == 0) ? Color.Red : Color.Black;
                        sd.Info.Pattern = (byte)DashStyle.Solid;
                        sd.Info.Width = 2;
                        if (!sd.Solid)
                            sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                    }
                    break;
                case MMAreaStyle.MM45Surface:
                    // MM4/5 surface maps don't have the solid/nonsolid flags per se, so we just put basic lines
                    // around mountain/forest/water if the opposing tile isn't the same type.
                    colorEdge = MMMapSquareInfo.GetLineColor((MM45OutdoorTile)sd.Tile, (MM45OutdoorTile)sd.OppositeTile);
                    if (colorEdge != Color.Transparent)
                    {
                        sd.Info.Color = colorEdge;
                        sd.Info.Width = MMMapSquareInfo.GetLineWidth((MM45OutdoorTile)sd.Tile, (MM45OutdoorTile)sd.OppositeTile);
                        sd.Info.Pattern = MMMapSquareInfo.GetLineStyle((MM45OutdoorTile)sd.Tile, (MM45OutdoorTile)sd.OppositeTile);
                    }
                    break;
                case MMAreaStyle.MM3Surface:
                    colorEdge = MMMapSquareInfo.GetLineColor((MM3OutdoorTile)sd.Tile, (MM3OutdoorTile)sd.OppositeTile);
                    if (sd.OppositeSolid || sd.Solid)
                    {
                        sd.Info.Color = colorEdge;
                        sd.Info.Width = MMMapSquareInfo.GetLineWidth((MM3OutdoorTile)sd.Tile, (MM3OutdoorTile)sd.OppositeTile);
                        sd.Info.Pattern = DashStyle.Dot;
                        if (!sd.Solid)
                            sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                    }
                    break;
                case MMAreaStyle.MM2Surface:
                    colorEdge = MMMapSquareInfo.GetLineColor((MMOutdoorTile)sd.Tile, (MMOutdoorTile)sd.OppositeTile);
                    if (sd.OppositeSolid || sd.Solid)
                    {
                        sd.Info.Color = colorEdge;
                        sd.Info.Width = 2;
                        sd.Info.Pattern = DashStyle.Dot;
                        if (!sd.Solid)
                            sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                    }
                    break;
                case MMAreaStyle.MM1Surface:
                    switch (sd.Visible)
                    {
                        case 0:
                            sd.Info.Color = GridLines.Color;
                            break;
                        case 1:
                            sd.Info.Color = Color.Green;
                            break;
                        case 2:
                            sd.Info.Color = Color.Red;
                            break;
                        case 3:
                            sd.Info.Color = Color.Blue;
                            break;
                    }
                    if (sd.Visible != 0)
                    {
                        sd.Info.Pattern = DashStyle.Solid;
                        sd.Info.Width = 2;
                        if (!sd.Solid)
                        {
                            if (sd.OppositeSolid && !sd.IsEdge(new Rectangle(1, 1, 16, 16)))
                                sd.Square.Icons.Add(new MapIcon(IconName.FragileHalf, sd.Dir, sd.GridPoint));
                            else
                                sd.Info.Pattern = DashStyle.Dot;
                        }
                    }
                    else if (sd.Solid || sd.OppositeSolid)
                    {
                        sd.Info.Pattern = (byte)DashStyle.Solid;
                        sd.Info.Width = 2;
                        if (!sd.Solid && !sd.IsEdge(new Rectangle(1, 1, 16, 16)))
                            sd.Square.Icons.Add(new MapIcon(IconName.ArrowHalf, sd.Dir, sd.GridPoint));
                    }
                    break;
                default:
                    break;
            }

            return sd.Info;
        }

        public void CheckConnectingMaps(MapData data)
        {
            // Check the map edges for connecting maps
            if (data is MM45MapData && ((MM45MapData)data).Outside)
            {
                MM45MapData mm45MapData = (MM45MapData)data;
                if (mm45MapData.MapBytes != null)
                {
                    // Make the edges solid if there are no connecting maps
                    AddConnectingMap(mm45MapData.MapBytes.SideMapEast, GridWidth - 1, GridHeight / 2, "⇒");
                    AddConnectingMap(mm45MapData.MapBytes.SideMapWest, 0, GridHeight / 2, "⇐");
                    AddConnectingMap(mm45MapData.MapBytes.SideMapNorth, GridWidth / 2, 0, "⇑");
                    AddConnectingMap(mm45MapData.MapBytes.SideMapSouth, GridWidth / 2, GridHeight - 1, "⇓");
                    AddDiagConnectingMap(mm45MapData.MapBytes.SideMapNorth, mm45MapData.MapBytes.SideMapEast, GridWidth - 1, 0);
                    AddDiagConnectingMap(mm45MapData.MapBytes.SideMapNorth, mm45MapData.MapBytes.SideMapWest, 0, 0);
                    AddDiagConnectingMap(mm45MapData.MapBytes.SideMapWest, mm45MapData.MapBytes.SideMapSouth, 0, GridHeight - 1);
                    AddDiagConnectingMap(mm45MapData.MapBytes.SideMapEast, mm45MapData.MapBytes.SideMapSouth, GridWidth - 1, GridHeight - 1);
                }
            }
        }
    }

    public class MMMapSquareInfo
    {
        public bool Valid;
        public int NorthAppearance;
        public int EastAppearance;
        public int SouthAppearance;
        public int WestAppearance;
        public bool NorthSolid;
        public bool EastSolid;
        public bool SouthSolid;
        public bool WestSolid;
        public bool Special;
        public bool Dark;
        public bool Dangerous;
        public bool AntiMagic;
        public int Tile;

        public static MMMapSquareInfo Create(MMMapData data, int vert, int horiz)
        {
            if (data is MM3MapData)
                return new MM3MapSquareInfo(data as MM3MapData, vert, horiz);
            else if (data is MM45MapData)
                return new MM45MapSquareInfo(data as MM45MapData, vert, horiz);
            return new MMMapSquareInfo(data, vert, horiz);
        }

        protected void SetDefaults()
        {
            NorthAppearance = -1;
            EastAppearance = -1;
            SouthAppearance = -1;
            WestAppearance = -1;
            NorthSolid = true;
            SouthSolid = true;
            WestSolid = true;
            EastSolid = true;
            Valid = false;
        }

        public MMMapSquareInfo()
        {
            SetDefaults();
        }

        public MMMapSquareInfo(MMMapData data, int vert, int horiz)
        {
            if (vert < 0 || vert > data.Height - 1 || horiz < 0 || horiz > data.Width - 1)
            {
                SetDefaults();
                Tile = (int)MMOutdoorTile.Invalid;
                return;
            }

            SetInfo(data, vert * data.Width * data.BytesPerSquare + horiz * data.BytesPerSquare, vert * data.Width + horiz);
        }

        public virtual bool Exitable
        {
            get
            {
                if (!Valid)
                    return false;
                return !(NorthAppearance == 1 && SouthAppearance == 1 && EastAppearance == 1 && WestAppearance == 1 &&
                    NorthSolid && SouthSolid && EastSolid && WestSolid);
            }
        }

        public virtual bool IsWall(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return (NorthAppearance == 1);
                case Direction.Down: return (SouthAppearance == 1);
                case Direction.Left: return (WestAppearance == 1);
                case Direction.Right: return (EastAppearance == 1);
                default: return false;
            }
        }

        public virtual bool Openable(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return (NorthAppearance == 2 || NorthAppearance == 4 || NorthAppearance == 5 || NorthAppearance == 6);
                case Direction.Down: return (SouthAppearance == 2 || SouthAppearance == 4 || SouthAppearance == 5 || SouthAppearance == 6);
                case Direction.Left: return (WestAppearance == 2 || WestAppearance == 4 || WestAppearance == 5 || WestAppearance == 6);
                case Direction.Right: return (EastAppearance == 2 || EastAppearance == 4 || EastAppearance == 5 || EastAppearance == 6);
                default: return false;
            }
        }

        public MMMapSquareInfo(byte appearance, byte attributes)
        {
            SetInfo(appearance, attributes);
        }

        public void SetInfo(MMMapData data, int iAppearanceOffset, int iAttributesOffset)
        {
            if (data.BytesPerSquare == 1)
                SetInfo(data.Appearance[iAppearanceOffset], data.Attributes[iAttributesOffset], data is MM1MapData);
            else
            {
                byte[] bytesAppearance = new byte[data.BytesPerSquare];
                byte[] bytesAttributes = new byte[1];
                Buffer.BlockCopy(data.Appearance, iAppearanceOffset, bytesAppearance, 0, data.BytesPerSquare);
                if (data.Attributes != null)
                    Buffer.BlockCopy(data.Attributes, iAttributesOffset, bytesAttributes, 0, 1);
                if (data is MM3MapData)
                    SetInfoMM3(bytesAppearance, bytesAttributes, data.Index);
                else
                    SetInfoMM45(bytesAppearance, bytesAttributes, data as MM45MapData);
            }
        }

        public void SetInfo(byte appearance, byte attributes, bool bIgnoreTile = false)
        {
            NorthAppearance = (appearance & 0xc0) >> 6;
            EastAppearance = (appearance & 0x30) >> 4;
            SouthAppearance = (appearance & 0x0c) >> 2;
            WestAppearance = (appearance & 0x03);
            NorthSolid = ((attributes & 0x40) > 0);
            EastSolid = ((attributes & 0x10) > 0);
            SouthSolid = ((attributes & 0x04) > 0);
            WestSolid = ((attributes & 0x01) > 0);
            Special = ((attributes & 0x80) > 0);
            Dark = ((attributes & 0x20) > 0);
            Dangerous = ((attributes & 0x08) > 0);
            AntiMagic = ((attributes & 0x02) > 0);

            if (!bIgnoreTile)
                Tile = appearance;
            else
                Tile = (int)MMOutdoorTile.Invalid;
            Valid = true;
        }

        public void SetInfoMM3(byte[] bytesAppearance, byte[] bytesAttributes, int iMapIndex)
        {
            SouthAppearance = (bytesAppearance[0] & 0x70) >> 4;
            WestAppearance = (bytesAppearance[0] & 0x07);
            NorthAppearance = (bytesAppearance[1] & 0x70) >> 4;
            EastAppearance = (bytesAppearance[1] & 0x07);
            SouthSolid = (bytesAppearance[0] & 0x80) > 0;
            WestSolid = (bytesAppearance[0] & 0x08) > 0;
            NorthSolid = (bytesAppearance[1] & 0x80) > 0;
            EastSolid = (bytesAppearance[1] & 0x08) > 0;
            Special = false;
            Dark = false;
            Dangerous = false;
            AntiMagic = false;

            /*
                .000.000 .00..000 Water
                .101.000 .000.000 Grass
                .101.001 .000.000 Mountain
                .110.001 .000.000 Mountain
                .101.010 .000.000 Light Forest
                .101.011 0000.000 Dense Forest
                .101.100 .000.000 Swamp
                .101..00 1000.110 Wagon
                .110.000 .000.000 Dirt
                .111.000 0000.000 Road
             */

            int tile = (ushort)((bytesAppearance[0] << 8) | bytesAppearance[1]);
            if (tile == 0x7889 || tile == 0x7009 || tile == 0x3001)
                Tile = (int)MM3OutdoorTile.Town;
            else if (tile == 0xf882 || tile == 0xf00a)
                Tile = (int)MM3OutdoorTile.Castle;
            else if (tile == 0xe804 || tile == 0xe803)
                Tile = (int)MM3OutdoorTile.Cavern;
            else if (tile == 0xd803 || tile == 0xd80b)
                Tile = (int)MM3OutdoorTile.Temple;
            else if (tile == 0x5883)
                Tile = (int)MM3OutdoorTile.Dungeon;
            else switch (tile & 0x7777)
                {
                    case 0x0000:
                        Tile = (int)MM3OutdoorTile.Water;
                        break;
                    case 0x0010:
                        Tile = (int)MM3OutdoorTile.ShallowWater;
                        break;
                    case 0x3000:
                        switch (iMapIndex)
                        {
                            case (int)MM3Map.E2Surface:
                            case (int)MM3Map.E3Surface:
                            case (int)MM3Map.F2Surface:
                            case (int)MM3Map.F3Surface:
                                Tile = (int)MM3OutdoorTile.Swamp;
                                break;
                            default:
                                Tile = (int)MM3OutdoorTile.Lava;
                                break;
                        }
                        break;
                    case 0x3100:
                        Tile = (int)MM3OutdoorTile.SwampMountain;
                        break;
                    case 0x3200:
                        switch (iMapIndex)
                        {
                            case (int)MM3Map.E2Surface:
                            case (int)MM3Map.E3Surface:
                            case (int)MM3Map.F2Surface:
                            case (int)MM3Map.F3Surface:
                                Tile = (int)MM3OutdoorTile.SwampForest;
                                break;
                            default:
                                Tile = (int)MM3OutdoorTile.LavaRock;
                                break;
                        }
                        break;
                    case 0x5000:
                        Tile = (int)MM3OutdoorTile.Grass;
                        break;
                    case 0x5100:
                    case 0x6100:
                        Tile = (int)MM3OutdoorTile.Mountain;
                        break;
                    case 0x5200:
                        Tile = (int)MM3OutdoorTile.LightForest;
                        break;
                    case 0x5300:
                        Tile = (int)MM3OutdoorTile.DenseForest;
                        break;
                    case 0x5400:
                        Tile = (int)MM3OutdoorTile.Swamp;
                        break;
                    case 0x5006:
                    case 0x5406:
                        Tile = (int)MM3OutdoorTile.Wagon;
                        break;
                    case 0x6000:
                        Tile = (int)MM3OutdoorTile.Dirt;
                        break;
                    case 0x3007:
                    case 0x5007:
                    case 0x5207:
                    case 0x5307:
                    case 0x5407:
                    case 0x6007:
                    case 0x6107:
                        Tile = (int)MM3OutdoorTile.Shack;
                        break;
                    case 0x5005:
                        Tile = (int)MM3OutdoorTile.Pyramid;
                        break;
                    case 0x6200:
                        Tile = (int)MM3OutdoorTile.Shrubs;
                        break;
                    case 0x6300:
                        Tile = (int)MM3OutdoorTile.Grove;
                        break;
                    case 0x7000:
                        switch (iMapIndex)
                        {
                            case (int)MM3Map.E4Surface:
                            case (int)MM3Map.F4Surface:
                                Tile = (int)MM3OutdoorTile.Desert;
                                break;
                            default:
                                Tile = (int)MM3OutdoorTile.Road;
                                break;
                        }
                        break;
                    case 0x7500:
                        Tile = (int)MM3OutdoorTile.TundraMountain;
                        break;
                    case 0x7600:
                        Tile = (int)MM3OutdoorTile.TundraForest;
                        break;
                    default:
                        Tile = (int)MM3OutdoorTile.Unknown;
                        break;
                }

            Valid = true;
        }

        private bool IsMM45AppearanceSolid(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 6:
                    return false;
                default:
                    return true;
            }
        }

        public void SetInfoMM45(byte[] bytesAppearance, byte[] bytesAttributes, MM45MapData data)
        {
            Special = false;
            Dark = false;
            Dangerous = false;
            AntiMagic = false;

            Tile = BitConverter.ToUInt16(bytesAppearance, 0) | (bytesAttributes[0] << 16);

            if (data.Outside)
            {
                SouthAppearance = 0;
                WestAppearance = 0;
                NorthAppearance = 0;
                EastAppearance = 0;
                SouthSolid = false;
                WestSolid = false;
                NorthSolid = false;
                EastSolid = false;
            }
            else
            {
                SouthAppearance = (bytesAppearance[0] & 0xf0) >> 4;
                WestAppearance = (bytesAppearance[0] & 0x0f);
                NorthAppearance = (bytesAppearance[1] & 0xf0) >> 4;
                EastAppearance = (bytesAppearance[1] & 0x0f);
                SouthSolid = IsMM45AppearanceSolid(bytesAppearance[0] >> 4);
                WestSolid = IsMM45AppearanceSolid(bytesAppearance[0] & 0xf);
                NorthSolid = IsMM45AppearanceSolid(bytesAppearance[1] >> 4);
                EastSolid = IsMM45AppearanceSolid(bytesAppearance[1] & 0xf);
            }

            Valid = true;
        }

        public static Color GetLineColor(MMOutdoorTile tile)
        {
            switch (tile & ~MMOutdoorTile.AllFlags)
            {
                case MMOutdoorTile.Mountain:
                case MMOutdoorTile.SnowMountain:
                case MMOutdoorTile.Volcano:
                case MMOutdoorTile.Cave:
                    return Color.Red;
                case MMOutdoorTile.EarthPlain:
                case MMOutdoorTile.FirePlain:
                case MMOutdoorTile.AirPlain:
                    return Color.Black;
                case MMOutdoorTile.LightForest:
                case MMOutdoorTile.DenseForest:
                    return Color.Green;
                case MMOutdoorTile.Water:
                case MMOutdoorTile.WaterPlain:
                    return Color.Blue;
                default:
                    return Color.Black;
            }
        }

        public static Color GetLineColor(MM3OutdoorTile tile)
        {
            switch (tile)
            {
                case MM3OutdoorTile.ShallowWater:
                case MM3OutdoorTile.Water: return Color.Blue;
                case MM3OutdoorTile.LightForest:
                case MM3OutdoorTile.DenseForest:
                case MM3OutdoorTile.Swamp:
                case MM3OutdoorTile.Shrubs:
                case MM3OutdoorTile.Grove:
                case MM3OutdoorTile.Grass: return Color.Green;
                case MM3OutdoorTile.TundraMountain:
                case MM3OutdoorTile.Mountain: return Color.Red;
                case MM3OutdoorTile.Wagon:
                case MM3OutdoorTile.Dirt:
                case MM3OutdoorTile.Road:
                case MM3OutdoorTile.Town:
                case MM3OutdoorTile.Pyramid:
                case MM3OutdoorTile.Castle:
                case MM3OutdoorTile.Cavern:
                case MM3OutdoorTile.Dungeon:
                case MM3OutdoorTile.Temple: return Color.Black;
                default:
                    return Color.Black;
            }
        }

        public static bool IsEmptyWater(MM45OutdoorTile tile)
        {
            // "Dangerous" and no other attributes also seems to mean "water"
            return tile.HasFlag(MM45OutdoorTile.Dangerous) && IsFloorTile(tile, MM45OutdoorTile.None);
        }

        public static Color GetLineColor(MM45OutdoorTile tile)
        {
            if (tile == MM45OutdoorTile.Invalid)
                return Color.Transparent;

            if (IsEmptyWater(tile))
                return Color.Blue;

            MM45OutdoorTile floor = tile & MM45OutdoorTile.FloorMask;
            MM45OutdoorTile walls = tile & MM45OutdoorTile.WallMask;
            MM45OutdoorTile obj = tile & MM45OutdoorTile.ObjMask;

            // walls take precedence over floors
            switch (walls)
            {
                case MM45OutdoorTile.GrayMountain: return Color.Red;
                case MM45OutdoorTile.SnowMountain: return Color.Red;
                case MM45OutdoorTile.BrownMountain: return Color.Red;
                case MM45OutdoorTile.SwampMountain: return Color.Red;
                case MM45OutdoorTile.Volcano: return Color.Red;
                case MM45OutdoorTile.DenseForest: return Color.Green;
                case MM45OutdoorTile.DensePine: return Color.Green;
                default:
                    break;
            }

            switch (floor)
            {
                case MM45OutdoorTile.Water: return Color.Blue;
                case MM45OutdoorTile.Space: return Color.Indigo;
                default: return Color.Transparent;
            }
        }

        public enum WallColorOrder
        {
            Tranparent = 0,
            Black,
            Blue,
            Green,
            Red,
            Indigo
        }

        public static WallColorOrder GetLineColorOrder(Color c)
        {
            // Indigo > Red > Green > Blue > Black > Transparent

            if (c == Color.Transparent)
                return WallColorOrder.Tranparent;
            if (c == Color.Black)
                return WallColorOrder.Black;
            if (c == Color.Blue)
                return WallColorOrder.Blue;
            if (c == Color.Green)
                return WallColorOrder.Green;
            if (c == Color.Red)
                return WallColorOrder.Red;
            if (c == Color.Indigo)
                return WallColorOrder.Indigo;
            return 0;
        }

        public static Color GetLineColor(Color colorEdge, Color colorOpposite)
        {
            if (colorEdge == colorOpposite)
                return colorEdge;

            WallColorOrder wcoEdge = GetLineColorOrder(colorEdge);
            WallColorOrder wcoOpposite = GetLineColorOrder(colorOpposite);

            if (wcoEdge > wcoOpposite)
                return colorEdge;

            return colorOpposite;
        }

        public static byte GetLineWidth(MM3OutdoorTile tile, MM3OutdoorTile tileOpposite)
        {
            if (tile == MM3OutdoorTile.Invalid || tileOpposite == MM3OutdoorTile.Invalid)
                return 2;

            // Shallow water is width 1 if the opposite tile would otherwise have nothing
            if (tile == MM3OutdoorTile.ShallowWater)
            {
                Color c = GetLineColor(tileOpposite);
                if (c == Color.Green || c == Color.Red || c == Color.Blue)
                    return 2;
                return 1;
            }
            else if (tileOpposite == MM3OutdoorTile.ShallowWater)
            {
                Color c = GetLineColor(tile);
                if (c == Color.Green || c == Color.Red || c == Color.Blue)
                    return 2;
                return 1;
            }
            return 2;
        }

        public static byte GetLineWidth(MM45OutdoorTile tile, MM45OutdoorTile tileOpposite)
        {
            if (tile == MM45OutdoorTile.Invalid || tileOpposite == MM45OutdoorTile.Invalid)
                return 2;

            WallColorOrder wcoTile = GetLineColorOrder(GetLineColor(tile));
            WallColorOrder wcoOpposite = GetLineColorOrder(GetLineColor(tileOpposite));

            // Shallow water is width 1 if the opposite tile would otherwise have nothing
            if (IsEmptyWater(tile))
            {
                if (wcoOpposite >= WallColorOrder.Blue)
                    return 2;
                return 1;
            }
            else if (IsEmptyWater(tileOpposite))
            {
                if (wcoTile >= WallColorOrder.Blue)
                    return 2;
                return 1;
            }
            return 2;
        }

        public static DashStyle GetLineStyle(MM45OutdoorTile tile, MM45OutdoorTile tileOpposite)
        {
            if (tile == MM45OutdoorTile.Invalid || tileOpposite == MM45OutdoorTile.Invalid)
                return DashStyle.Dot;

            if (IsFloorTile(tile, MM45OutdoorTile.Space) || IsFloorTile(tileOpposite, MM45OutdoorTile.Space))
                return DashStyle.Solid;

            return DashStyle.Dot;
        }

        public static Color GetLineColor(MMOutdoorTile tile, MMOutdoorTile tileOpposite)
        {
            Color colorEdge = GetLineColor(tile);
            Color colorOpposite = GetLineColor(tileOpposite);

            return GetLineColor(colorEdge, colorOpposite);
        }

        public static Color GetLineColor(MM3OutdoorTile tile, MM3OutdoorTile tileOpposite)
        {
            Color colorEdge = GetLineColor(tile);
            Color colorOpposite = GetLineColor(tileOpposite);

            return GetLineColor(colorEdge, colorOpposite);
        }

        public static bool IsFloorTile(MM45OutdoorTile tile, MM45OutdoorTile match)
        {
            return ((tile & MM45OutdoorTile.FloorMask) == match);
        }

        public static bool IsWallTile(MM45OutdoorTile tile, MM45OutdoorTile match)
        {
            return ((tile & MM45OutdoorTile.WallMask) == match);
        }

        public static Color GetLineColor(MM45OutdoorTile tile, MM45OutdoorTile tileOpposite)
        {
            if (tileOpposite == MM45OutdoorTile.Invalid)
                return Color.Black;

            if (IsEmptyWater(tile) && IsEmptyWater(tileOpposite))
                return Color.Transparent;

            foreach (MM45OutdoorTile t in new MM45OutdoorTile[] { MM45OutdoorTile.GrayMountain, MM45OutdoorTile.BrownMountain, MM45OutdoorTile.SwampMountain,
                MM45OutdoorTile.SnowMountain, MM45OutdoorTile.DenseForest, MM45OutdoorTile.DensePine, MM45OutdoorTile.SpaceWall, MM45OutdoorTile.Volcano })
            {
                if (IsWallTile(tile, t) && IsWallTile(tileOpposite, t))
                    return Color.Transparent;
            }

            foreach (MM45OutdoorTile t in new MM45OutdoorTile[] { MM45OutdoorTile.Space, MM45OutdoorTile.Water })
            {
                if (IsFloorTile(tile, t) && IsFloorTile(tileOpposite, t))
                    return Color.Transparent;
            }

            Color colorEdge = GetLineColor(tile);
            Color colorOpposite = GetLineColor(tileOpposite);

            return GetLineColor(colorEdge, colorOpposite);
        }

        public static Color GetBackColor(MMOutdoorTile tile)
        {
            switch (tile & ~MMOutdoorTile.AllFlags)
            {
                case MMOutdoorTile.Grass: return Color.PaleGreen;
                case MMOutdoorTile.Mountain: return Color.Salmon;
                case MMOutdoorTile.SnowMountain: return Color.LightSalmon;
                case MMOutdoorTile.LightForest: return Color.LawnGreen;
                case MMOutdoorTile.DenseForest: return Color.LimeGreen;
                case MMOutdoorTile.FirePlain: return Color.DarkSalmon;
                case MMOutdoorTile.WaterPlain: return Color.LightBlue;
                case MMOutdoorTile.EarthPlain: return Color.Sienna;
                case MMOutdoorTile.AirPlain: return Color.LightSkyBlue;
                case MMOutdoorTile.Desert: return Color.FromArgb(0xff, 0xa5, 0x50);
                case MMOutdoorTile.Swamp: return Color.DarkSeaGreen;
                case MMOutdoorTile.Water: return Color.PowderBlue;
                case MMOutdoorTile.Tundra: return Color.LightGray;
                case MMOutdoorTile.TundraRoadEW: return Color.Peru;
                case MMOutdoorTile.Volcano: return Color.IndianRed;
                case MMOutdoorTile.DeadZone: return Color.CornflowerBlue;
                case MMOutdoorTile.RoadEW:
                case MMOutdoorTile.RoadNS:
                case MMOutdoorTile.RoadNW:
                case MMOutdoorTile.RoadSW:
                case MMOutdoorTile.RoadSE:
                case MMOutdoorTile.RoadNE:
                case MMOutdoorTile.RoadWSE:
                case MMOutdoorTile.RoadNSE:
                case MMOutdoorTile.RoadWNS: return Color.Chocolate;
                case MMOutdoorTile.Cave: return Color.Brown;
                case MMOutdoorTile.Oasis:
                case MMOutdoorTile.Island: return Color.GreenYellow;
                case MMOutdoorTile.Rock: return Color.Gray;
                case MMOutdoorTile.Castle: return Color.White;
                case MMOutdoorTile.Town: return Color.White;
                default: return Color.White;
            }
        }

        public static Color GetBackColor(MM3OutdoorTile tile)
        {
            switch (tile)
            {
                case MM3OutdoorTile.Water: return Color.SkyBlue;
                case MM3OutdoorTile.ShallowWater: return Color.PowderBlue;
                case MM3OutdoorTile.LightForest: return Color.LawnGreen;
                case MM3OutdoorTile.DenseForest: return Color.LimeGreen;
                case MM3OutdoorTile.Swamp: return Color.DarkSeaGreen;
                case MM3OutdoorTile.Grass: return Color.PaleGreen;
                case MM3OutdoorTile.Mountain: return Color.Salmon;
                case MM3OutdoorTile.Wagon: return Color.White;
                case MM3OutdoorTile.Dirt: return Color.Chocolate;
                case MM3OutdoorTile.Road: return Color.Silver;
                case MM3OutdoorTile.Town: return Color.White;
                case MM3OutdoorTile.Temple: return Color.White;
                case MM3OutdoorTile.Shrubs: return Color.Olive;
                case MM3OutdoorTile.Grove: return Color.OliveDrab;
                case MM3OutdoorTile.Shack: return Color.White;
                case MM3OutdoorTile.Castle: return Color.White;
                case MM3OutdoorTile.Pyramid: return Color.White;
                case MM3OutdoorTile.Cavern: return Color.White;
                case MM3OutdoorTile.Dungeon: return Color.White;
                case MM3OutdoorTile.TundraForest: return Color.MediumAquamarine;
                case MM3OutdoorTile.TundraMountain: return Color.PaleVioletRed;
                case MM3OutdoorTile.LavaRock: return Color.IndianRed;
                case MM3OutdoorTile.Lava: return Color.LightCoral;
                case MM3OutdoorTile.SwampForest: return Color.YellowGreen;
                case MM3OutdoorTile.SwampMountain: return Color.OliveDrab;
                case MM3OutdoorTile.Desert: return Color.FromArgb(0xff, 0xa5, 0x50);
                default: return Color.White;
            }
        }

        public static HatchStyle GetBackStyle(MM45OutdoorTile tile)
        {
            if (IsFloorTile(tile, MM45OutdoorTile.Space))
                return Global.SquareStyles.SpacePattern;
            if (IsFloorTile(tile, MM45OutdoorTile.Sky))
                return Global.SquareStyles.SkyPattern;

            return HatchStyle.Percent90;
        }

        public static Color GetBackColor(MM45OutdoorTile tile)
        {
            if (IsEmptyWater(tile))
                return Color.PowderBlue;

            MM45OutdoorTile floor = tile & MM45OutdoorTile.FloorMask;
            MM45OutdoorTile walls = tile & MM45OutdoorTile.WallMask;
            MM45OutdoorTile obj = tile & MM45OutdoorTile.ObjMask;

            // Any tile with an object on it is white, so that the note text is easier to see
            if (obj != MM45OutdoorTile.None)
                return Color.White;

            // walls take precedence over floors
            switch (walls)
            {
                case MM45OutdoorTile.GrayMountain: return Color.RosyBrown;
                case MM45OutdoorTile.Clearing: return Color.PaleGreen;
                case MM45OutdoorTile.BrownMountain: return Color.Salmon;
                case MM45OutdoorTile.Volcano: return Color.IndianRed;
                case MM45OutdoorTile.SnowMountain: return Color.PaleVioletRed;
                case MM45OutdoorTile.SwampMountain: return Color.OliveDrab;
                case MM45OutdoorTile.PalmTree: return Color.GreenYellow;
                case MM45OutdoorTile.BurnedTwigs: return Color.Tan;
                case MM45OutdoorTile.Twigs: return Color.YellowGreen;
                case MM45OutdoorTile.SpaceWall: return Color.MediumPurple;
                case MM45OutdoorTile.LightForest:
                case MM45OutdoorTile.LightForest2:
                case MM45OutdoorTile.LightPine:
                    switch (floor)
                    {
                        case MM45OutdoorTile.Dirt: return Color.YellowGreen;
                        case MM45OutdoorTile.Swamp: return Color.YellowGreen;
                        case MM45OutdoorTile.Snow: return Color.MediumAquamarine;
                        default: return Color.LawnGreen;
                    }
                case MM45OutdoorTile.DenseForest:
                case MM45OutdoorTile.DensePine:
                    switch (floor)
                    {
                        case MM45OutdoorTile.Dirt: return Global.Lighten(Color.ForestGreen, 60);
                        case MM45OutdoorTile.Swamp: return Color.YellowGreen;
                        case MM45OutdoorTile.Snow: return Color.MediumAquamarine;
                        default: return Color.LimeGreen;
                    }
                default:
                    break;
            }

            switch (floor)
            {
                case MM45OutdoorTile.Dirt: return Global.Lighten(Color.Peru, 10);
                case MM45OutdoorTile.Grass: return Color.PaleGreen;
                case MM45OutdoorTile.Snow: return Global.Lighten(Color.LightSteelBlue, 40);
                case MM45OutdoorTile.Swamp: return Color.DarkSeaGreen;
                case MM45OutdoorTile.Lava: return Color.LightCoral;
                case MM45OutdoorTile.Desert: return Global.Lighten(Color.FromArgb(0xff, 0xa5, 0x50), 20);
                case MM45OutdoorTile.Road: return Color.Silver;
                case MM45OutdoorTile.Water: return Color.SkyBlue;
                case MM45OutdoorTile.TFlr: return Color.White;
                case MM45OutdoorTile.Sky: return Global.SquareStyles.SkyColor;
                case MM45OutdoorTile.CRoad: return Color.AliceBlue;
                case MM45OutdoorTile.Sewer: return Color.DarkSeaGreen;
                case MM45OutdoorTile.Cloud: return Color.AliceBlue;
                case MM45OutdoorTile.Scorch: return Color.FromArgb(172, 173, 146);
                case MM45OutdoorTile.Space: return Global.SquareStyles.SpaceColor;
                default: return Color.PowderBlue;
            }
        }

        public static Color GetBackColor(MM45MapData data, MM45IndoorTile tile)
        {
            int iIndex = (int)(tile & MM45IndoorTile.FloorMask) >> 16;
            if (iIndex == 0) // Default: depends on tileset
            {
                switch (data.FloorTileSet)
                {
                    case 2: return GetBackColor(MM45IndoorTile.BlackTile);
                    case 3:
                    case 4: return GetBackColor(MM45IndoorTile.BlueStone);
                    default: return GetBackColor(MM45IndoorTile.Dirt);      // Dirt or weed-ridden cobblestones
                }
            }

            if (data.MapBytes != null && data.MapBytes.Floors != null && data.MapBytes.Floors.Length > iIndex)
            {
                MM45IndoorTile tileNew = (MM45IndoorTile)data.MapBytes.Floors[iIndex];
                if (tileNew == MM45IndoorTile.Transparent)
                    tileNew = MM45IndoorTile.Water;
                return MMMapSquareInfo.GetBackColor(tileNew);
            }
            return GetBackColor((MM45IndoorTile)iIndex);
        }

        public static Color GetBackColor(MM45IndoorTile tile)
        {
            // The indoor tile colors are generally lighter than the outdoor ones, because the map sizes
            // are usually 32x32 and darker colors make the note symbols harder to read.

            switch (tile)
            {
                case MM45IndoorTile.Water: return Color.SkyBlue;
                case MM45IndoorTile.BlackTile: return Color.Gainsboro;
                case MM45IndoorTile.BlueStone: return Global.Lighten(Color.Gainsboro, 10);
                case MM45IndoorTile.Grass: return Global.Lighten(Color.PaleGreen, 40);
                case MM45IndoorTile.RedGrid: return Global.Darken(Color.MistyRose, 10);
                case MM45IndoorTile.Road1:
                case MM45IndoorTile.Road2:
                case MM45IndoorTile.Road4: return Color.Gainsboro;
                case MM45IndoorTile.Sewer: return Global.Lighten(Color.DarkKhaki, 20);
                case MM45IndoorTile.PurpleGrid2: return Color.Thistle;
                case MM45IndoorTile.Dirt: return Color.FromArgb(224, 189, 160);
                case MM45IndoorTile.Snow1: return Global.Lighten(Color.LightSteelBlue, 40);
                case MM45IndoorTile.Fire: return Color.LightCoral;
                case MM45IndoorTile.Desert: return Global.Lighten(Color.Tan, 50);
                case MM45IndoorTile.Sky: return Global.SquareStyles.SkyColor;
                case MM45IndoorTile.Clouds: return Color.AliceBlue;
                case MM45IndoorTile.Scorch: return Global.Lighten(Color.FromArgb(172, 173, 146), 30);
                case MM45IndoorTile.Space: return Color.MediumPurple;
                default: return Color.White;
            }
        }
    }

    public class MM3MapSquareInfo : MMMapSquareInfo
    {
        public MM3MapSquareInfo(MM3MapData data, int vert, int horiz)
        {
            if (vert < 0 || vert > data.Height - 1 || horiz < 0 || horiz > data.Width - 1)
            {
                SetDefaults();
                Tile = (int)MM3OutdoorTile.Invalid;
                return;
            }

            SetInfo(data, vert * data.Width * data.BytesPerSquare + horiz * data.BytesPerSquare, vert * data.Width + horiz);
        }
    }

    public class MM45MapSquareInfo : MMMapSquareInfo
    {
        public MM45MapSquareInfo(MM45MapData data, int vert, int horiz)
        {
            if (vert < 0 || vert > data.Height - 1 || horiz < 0 || horiz > data.Width - 1)
            {
                Valid = false;
                Tile = (int)MM45OutdoorTile.Invalid;
                EastSolid = true;
                WestSolid = true;
                SouthSolid = true;
                NorthSolid = true;
                return;
            }

            SetInfo(data, vert * data.Width * data.BytesPerSquare + horiz * data.BytesPerSquare, vert * data.Width + horiz);
        }

        public override bool Exitable
        {
            get
            {
                if (!Valid)
                    return false;
                return !(NorthAppearance == 8 && SouthAppearance == 8 && EastAppearance == 8 && WestAppearance == 8 &&
                    NorthSolid && SouthSolid && EastSolid && WestSolid);
            }
        }

        public override bool Openable(Direction dir)
        {
            int iTest = -1;
            switch (dir)
            {
                case Direction.Up:
                    iTest = NorthAppearance;
                    break;
                case Direction.Down:
                    iTest = SouthAppearance;
                    break;
                case Direction.Left:
                    iTest = WestAppearance;
                    break;
                case Direction.Right:
                    iTest = EastAppearance;
                    break;
                default: return false;
            }

            switch (iTest)
            {
                case 3:  // Bashed Opening
                case 4:   // Closed Door
                case 6:   // Open Door with Switch
                case 7:   // Large Closed Door
                case 9:   // Closed Grate with Switch
                case 13:  // Closed Door
                          //case 14:  // Open Door (appearance only, not actually a passage)
                          //case 15:  // Open Door (appearance only, not actually a passage)
                    return true;
                default:
                    return false;
            }
        }

        public override bool IsWall(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return (NorthAppearance == 8);
                case Direction.Down: return (SouthAppearance == 8);
                case Direction.Left: return (WestAppearance == 8);
                case Direction.Right: return (EastAppearance == 8);
                default: return false;
            }
        }
    }

    public enum MMAreaStyle
    {
        Dungeon,
        MM2Surface,
        MM3Dungeon,
        MM3Surface,
        MM45Dungeon,
        MM45Surface,
        Town,
        Castle,
        Mixed,
        MM1Surface,
        MM2Dungeon
    }
}
