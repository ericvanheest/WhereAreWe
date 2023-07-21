using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum UltimaOutdoorTile
    {
        Border = -1,
        Water = 0,
        Grass = 1,
        Forest = 2,
        Mountain = 3,
        Castle = 4,
        Sign = 5,
        City = 6,
        Dungeon1 = 7,
        Dungeon2 = 8,
        Player = 9,
        Horse = 10,
        Cart = 11,
        Raft = 12,
        Frigate1 = 13,
        Frigate2 = 14,
        AirCar = 15,
    }

    public enum UltimaTownTile
    {
        Border = -1,
        WhiteSquare = 0,
        BlackSquare = 1,
        BlueSquare = 2,
        BlueCircleUpperLeft = 3,
        BlueCircleLowerLeft = 4,
        BlueCircleUpperRight = 5,
        BlueCircleLowerRight = 6,
        DiagonalBlackBlue = 7,
        DiagonalBlueBlack = 8,
        SmallTree = 9,
        LargeTree = 10,
        DeskHoriz = 11,
        DeskVert = 12,
        DeskTop = 13,
        DeskBottom = 14,
        DeskRight = 15,
        DeskLeft = 16,
        Guard = 17,
        Player = 18,
        YellowChar = 19,
        King = 20,
        Shopkeeper = 21,
        Princess = 22,
        Brick = 23,
        LetterA = 24,
        LetterB = 25,
        LetterC = 26,
        LetterD = 27,
        LetterE = 28,
        LetterF = 29,
        LetterG = 30,
        LetterH = 31,
        LetterI = 32,
        LetterJ = 33,
        LetterK = 34,
        LetterL = 35,
        LetterM = 36,
        LetterN = 37,
        LetterO = 38,
        LetterP = 39,
        LetterQ = 40,
        LetterR = 41,
        LetterS = 42,
        LetterT = 43,
        LetterU = 44,
        LetterV = 45,
        LetterW = 46,
        LetterX = 47,
        LetterY = 48,
        LetterZ = 49,
        GreenChar = 50,
        Beach1 = 51,
        Beach2 = 52,
        Beach3 = 53,
        TransactArmor = 54,
        StealArmor = 55,
        TransactFood = 56,
        StealPub = 57,
        TransactWeapon = 58,
        StealWeapon = 59,
        TransactMagic = 60,
        TransactPub = 61,
        TransactTransport = 62,
        BlockNPC = 63,
    }

    public enum UltimaIndoorTile
    {
        Open = 0,
        Solid = 1,
        Solid2 = 2,
        Door = 3,
        Open4 = 4,
        Open5 = 5,
        LadderDown = 6,
        LadderUp = 7,
        Barrier = 8
    }

    public class UltimaMapSquareInfo
    {
        public UltimaIndoorTile Tile;

        public UltimaMapSquareInfo(UltimaIndoorTile tile)
        {
            Tile = tile;
        }

        public bool Divider => Tile == UltimaIndoorTile.Door;
        public bool Door => Tile == UltimaIndoorTile.Door;
        public bool Passable => Viewable && Tile != UltimaIndoorTile.Barrier;
        public bool Viewable => Tile != UltimaIndoorTile.Solid && Tile != UltimaIndoorTile.Solid2;
    }

    public class UltimaMaps : MapsBase
    {
        public UltimaMaps(MapSheet sheet)
        {
            m_sheet = sheet;
        }

        public MapSquareData SetUltimaSquareFromGameBytes(Point ptGrid, UltimaMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            if (!data.Bounds.Contains(horiz, vert))
                return null;
            int iMap = data.Index;
            MapSquare square = Grid[ptGrid.X, ptGrid.Y];
            Point ptGame = new Point(horiz, vert);
            square.Colors.BackColorPattern = SquareStyleList.DefaultEmpty;
            square.RemoveIcons(IconName.StairsDown);
            square.RemoveIcons(IconName.StairsUp);

            square.RemoveIcons(IconName.DoorHalf);
            square.RemoveIcons(IconName.DoorFull);
            square.RemoveIcons(IconName.GrateFull);
            square.RemoveIcons(IconName.LockedFull);
            square.RemoveIcons(IconName.ArrowHalf);

            if (data.Index == 0)
                return SetOverworldSquare(ptGrid, ptGame, square, data, horiz, vert);
            if ((data.Index & 0xff) <= (int)Ultima1Map.ThePillarsOfProtection)
                return SetTownSquare(ptGrid, ptGame, square, data, horiz, vert);
            return SetUndergroundSquare(ptGrid, ptGame, square, data, horiz, vert, iDeltaEast, iDeltaSouth);
        }

        private MapSquareData SetTownSquare(Point ptGrid, Point ptGame, MapSquare square, UltimaMapData data, int horiz, int vert)
        {
            MapSquareData msd = null;
            UltimaTownTile tile = data.GetTownValue(horiz, vert);
            UltimaTownTile tileEast = data.GetTownValue(horiz + 1, vert);
            UltimaTownTile tileWest = data.GetTownValue(horiz - 1, vert);
            UltimaTownTile tileNorth = data.GetTownValue(horiz, vert - 1);
            UltimaTownTile tileSouth = data.GetTownValue(horiz, vert + 1);

            square.Colors.BackColorPattern = new ColorPattern(TownColor(tile), TownStyle(tile));
            Ultima1Map map = Ultima1MemoryHacker.MapFromOverworldPoint(ptGame);

            if (tile >= UltimaTownTile.LetterA && tile <= UltimaTownTile.LetterZ)
                square.Note = new MapNote(String.Format("{0}", (int)tile), Color.Black, String.Format("{0}", (char) ('A' + (tile - UltimaTownTile.LetterA))), ptGrid);
            // square.Note = new MapNote(String.Format("{0}", (int)tile), Color.Black, String.Format("{0}", (int)tile));

            if (IsOpen(tile))
            {
                if (vert >= data.Height - 1 || vert == 0 || horiz == 0 || horiz >= data.Width - 1)
                {
                    square.AddUniqueIcon(new MapIcon(IconName.Exit, Direction.Up, ptGrid));
                    square.Note = new MapNote(String.Format("{{map:{0}}} {1},{2}", Ultima1MemoryHacker.MapName(Ultima1Map.Overworld), data.OverworldLocation.X, data.OverworldLocation.Y), Color.Black, ".", ptGrid);
                }
            }

            MapLineInfo mliSolid = new MapLineInfo(Color.Black, DashStyle.Solid, 2);

            if (!IsOpen(tile))
                LineIfNot(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, c => !IsOpen((UltimaTownTile) c));
            else
                LineIfNot(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, c => IsOpen((UltimaTownTile)c));

            return msd;
        }

        public static string OutdoorTileName(UltimaOutdoorTile tile)
        {
            switch (tile)
            {
                case UltimaOutdoorTile.Border: return "Border";
                case UltimaOutdoorTile.Water: return "Water";
                case UltimaOutdoorTile.Grass: return "Grass";
                case UltimaOutdoorTile.Forest: return "Forest";
                case UltimaOutdoorTile.Mountain: return "Mountain";
                case UltimaOutdoorTile.Castle: return "Castle";
                case UltimaOutdoorTile.Sign: return "Sign";
                case UltimaOutdoorTile.City: return "City";
                case UltimaOutdoorTile.Dungeon1: return "Dungeon 1";
                case UltimaOutdoorTile.Dungeon2: return "Dungeon 2";
                case UltimaOutdoorTile.Player: return "Player";
                case UltimaOutdoorTile.Horse: return "Horse";
                case UltimaOutdoorTile.Cart: return "Cart";
                case UltimaOutdoorTile.Raft: return "Raft";
                case UltimaOutdoorTile.Frigate1: return "Frigate 1";
                case UltimaOutdoorTile.Frigate2: return "Frigate 2";
                case UltimaOutdoorTile.AirCar: return "Air Car";
                default: return String.Format("Unknown({0})", (int)tile);
            }
        }

        private bool IsOpen(UltimaTownTile tile)
        {
            switch (tile)
            {
                case UltimaTownTile.WhiteSquare:
                case UltimaTownTile.BlueSquare:
                case UltimaTownTile.BlueCircleUpperLeft:
                case UltimaTownTile.BlueCircleLowerLeft:
                case UltimaTownTile.BlueCircleUpperRight:
                case UltimaTownTile.BlueCircleLowerRight:
                case UltimaTownTile.DiagonalBlackBlue:
                case UltimaTownTile.DiagonalBlueBlack:
                case UltimaTownTile.SmallTree:
                case UltimaTownTile.LargeTree:
                case UltimaTownTile.Brick:
                case UltimaTownTile.DeskBottom:
                case UltimaTownTile.DeskHoriz:
                case UltimaTownTile.DeskLeft:
                case UltimaTownTile.DeskRight:
                case UltimaTownTile.DeskTop:
                case UltimaTownTile.DeskVert:
                case UltimaTownTile.LetterA:
                case UltimaTownTile.LetterB:
                case UltimaTownTile.LetterC:
                case UltimaTownTile.LetterD:
                case UltimaTownTile.LetterE:
                case UltimaTownTile.LetterF:
                case UltimaTownTile.LetterG:
                case UltimaTownTile.LetterH:
                case UltimaTownTile.LetterI:
                case UltimaTownTile.LetterJ:
                case UltimaTownTile.LetterK:
                case UltimaTownTile.LetterL:
                case UltimaTownTile.LetterM:
                case UltimaTownTile.LetterN:
                case UltimaTownTile.LetterO:
                case UltimaTownTile.LetterP:
                case UltimaTownTile.LetterQ:
                case UltimaTownTile.LetterR:
                case UltimaTownTile.LetterS:
                case UltimaTownTile.LetterT:
                case UltimaTownTile.LetterU:
                case UltimaTownTile.LetterV:
                case UltimaTownTile.LetterW:
                case UltimaTownTile.LetterX:
                case UltimaTownTile.LetterY:
                case UltimaTownTile.LetterZ:
                    return false;
                default:
                    return true;
            }
        }

        private HatchStyle TownStyle(UltimaTownTile tile)
        {
            switch (tile)
            {
                case UltimaTownTile.TransactArmor:
                case UltimaTownTile.TransactFood:
                case UltimaTownTile.TransactWeapon:
                case UltimaTownTile.TransactTransport:
                case UltimaTownTile.TransactMagic:
                case UltimaTownTile.TransactPub:
                case UltimaTownTile.StealArmor:
                case UltimaTownTile.StealPub:
                case UltimaTownTile.StealWeapon:
                    return HatchStyle.Percent50;
                case UltimaTownTile.BlockNPC:
                    return HatchStyle.Percent50;
                default: return HatchStyle.Percent90;
            }
        }

        private Color TownColor(UltimaTownTile tile)
        {
            switch (tile)
            {
                case UltimaTownTile.BlackSquare:
                    return Color.White;
                case UltimaTownTile.StealArmor:
                case UltimaTownTile.StealPub:
                case UltimaTownTile.StealWeapon:
                    return Color.Plum;
                case UltimaTownTile.TransactArmor:
                case UltimaTownTile.TransactFood:
                case UltimaTownTile.TransactWeapon:
                case UltimaTownTile.TransactTransport:
                case UltimaTownTile.TransactMagic:
                case UltimaTownTile.TransactPub:
                    return Color.Gold;
                case UltimaTownTile.BlueSquare:
                    return Color.SkyBlue;
                case UltimaTownTile.BlueCircleUpperLeft:
                case UltimaTownTile.BlueCircleLowerLeft:
                case UltimaTownTile.BlueCircleUpperRight:
                case UltimaTownTile.BlueCircleLowerRight:
                case UltimaTownTile.DiagonalBlackBlue:
                case UltimaTownTile.DiagonalBlueBlack:
                    return Color.PowderBlue;
                case UltimaTownTile.SmallTree:
                    return Color.LawnGreen;
                case UltimaTownTile.LargeTree:
                    return Color.LimeGreen;
                case UltimaTownTile.Beach1:
                case UltimaTownTile.Beach2:
                case UltimaTownTile.Beach3:
                    return Color.SeaShell;
                case UltimaTownTile.Brick:
                    return Color.LightSlateGray;
                case UltimaTownTile.DeskBottom:
                case UltimaTownTile.DeskHoriz:
                case UltimaTownTile.DeskLeft:
                case UltimaTownTile.DeskRight:
                case UltimaTownTile.DeskTop:
                case UltimaTownTile.DeskVert:
                    return Color.Chocolate;
                case UltimaTownTile.BlockNPC:
                    return Color.Pink;
                default: return Color.LightSteelBlue;
            }
        }

        private int[] ToIntArray(UltimaOutdoorTile[] tiles)
        {
            int[] array = new int[tiles.Length];
            for (int i = 0; i < tiles.Length; i++)
                array[i] = (int) tiles[i];
            return array;
        }

        private void LineIf(MapSquare square, 
            UltimaOutdoorTile tileNorth, UltimaOutdoorTile tileEast, UltimaOutdoorTile tileSouth, UltimaOutdoorTile tileWest,
            MapLineInfo mli, params UltimaOutdoorTile[] tilesCompare) =>
            LineIfInt(false, square, (int)tileNorth, (int)tileEast, (int)tileSouth, (int)tileWest, mli, ToIntArray(tilesCompare));

        private void LineIfNot(MapSquare square,
            UltimaOutdoorTile tileNorth, UltimaOutdoorTile tileEast, UltimaOutdoorTile tileSouth, UltimaOutdoorTile tileWest,
            MapLineInfo mli, params UltimaOutdoorTile[] tilesCompare) =>
            LineIfInt(true, square, (int)tileNorth, (int)tileEast, (int)tileSouth, (int)tileWest, mli, ToIntArray(tilesCompare));

        private void LineIfNot(MapSquare square,
            UltimaTownTile tileNorth, UltimaTownTile tileEast, UltimaTownTile tileSouth, UltimaTownTile tileWest,
            MapLineInfo mli, Func<int, bool> testFunc) =>
            LineIfInt(true, square, (int)tileNorth, (int)tileEast, (int)tileSouth, (int)tileWest, mli, testFunc);

        private int[] ToIntArray(UltimaTownTile[] tiles)
        {
            int[] array = new int[tiles.Length];
            for (int i = 0; i < tiles.Length; i++)
                array[i] = (int)tiles[i];
            return array;
        }

        private void LineIf(MapSquare square,
            UltimaTownTile tileNorth, UltimaTownTile tileEast, UltimaTownTile tileSouth, UltimaTownTile tileWest,
            MapLineInfo mli, params UltimaTownTile[] tilesCompare) =>
            LineIfInt(false, square, (int)tileNorth, (int)tileEast, (int)tileSouth, (int)tileWest, mli, ToIntArray(tilesCompare));

        private void LineIfNot(MapSquare square,
            UltimaTownTile tileNorth, UltimaTownTile tileEast, UltimaTownTile tileSouth, UltimaTownTile tileWest,
            MapLineInfo mli, params UltimaTownTile[] tilesCompare) =>
            LineIfInt(true, square, (int)tileNorth, (int)tileEast, (int)tileSouth, (int)tileWest, mli, ToIntArray(tilesCompare));

        private void LineIfInt(bool bNot, MapSquare square, int north, int east, int south, int west, MapLineInfo mli, Func<int, bool> testFunc)
        {
            if ((testFunc(north) && !bNot) || (!testFunc(north) && bNot))
                square.Line(Direction.Up, mli);
            if ((testFunc(south) && !bNot) || (!testFunc(south) && bNot))
                square.Line(Direction.Down, mli);
            if ((testFunc(east) && !bNot) || (!testFunc(east) && bNot))
                square.Line(Direction.Right, mli);
            if ((testFunc(west) && !bNot) || (!testFunc(west) && bNot))
                square.Line(Direction.Left, mli);
        }

        private void LineIfInt(bool bNot, MapSquare square, int north, int east, int south, int west, MapLineInfo mli, params int[] compare) =>
            LineIfInt(bNot, square, north, east, south, west, mli, c => compare.Contains(c));

        private MapSquareData SetOverworldSquare(Point ptGrid, Point ptGame, MapSquare square, UltimaMapData data, int horiz, int vert)
        {
            MapSquareData msd = null;
            UltimaOutdoorTile tile = data.GetOverworldValue(horiz, vert);
            UltimaOutdoorTile tileEast = data.GetOverworldValue(horiz + 1, vert);
            UltimaOutdoorTile tileWest = data.GetOverworldValue(horiz - 1, vert);
            UltimaOutdoorTile tileNorth = data.GetOverworldValue(horiz, vert - 1);
            UltimaOutdoorTile tileSouth = data.GetOverworldValue(horiz, vert + 1);

            square.Colors.BackColorPattern = new ColorPattern(OverworldColor(tile), HatchStyle.Percent90);
            Ultima1Map map = Ultima1MemoryHacker.MapFromOverworldPoint(ptGame);
            string strNote = "(unknown map)"; 
            if (map != Ultima1Map.Legend)
                strNote = Ultima1MemoryHacker.MapName(map);
            MapLineInfo mliSolid = new MapLineInfo(Color.Black, DashStyle.Solid, 1);
            MapLineInfo mliWater = new MapLineInfo(Color.Blue, DashStyle.Dot, 1);
            MapLineInfo mliForest = new MapLineInfo(Color.Green, DashStyle.Dot, 1);

            switch (tile)
            {
                case UltimaOutdoorTile.Dungeon1:
                case UltimaOutdoorTile.Dungeon2:
                    square.Note = new MapNote(strNote, Color.Black, "Du");
                    break;
                case UltimaOutdoorTile.Castle:
                    square.Note = new MapNote(strNote, Color.Black, "Ca");
                    break;
                case UltimaOutdoorTile.City:
                    square.Note = new MapNote("City of " + strNote, Color.Black, "Ci");
                    break;
                case UltimaOutdoorTile.Sign:
                    square.Note = new MapNote(strNote, Color.Black, "Si");
                    break;
                default:
                    break;
            }

            switch (tile)
            {
                case UltimaOutdoorTile.Forest:
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, UltimaOutdoorTile.Mountain);
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliWater, UltimaOutdoorTile.Water);
                    LineIfNot(square, tileNorth, tileEast, tileSouth, tileWest, mliForest, UltimaOutdoorTile.Forest, UltimaOutdoorTile.Water, UltimaOutdoorTile.Mountain);
                    break;
                case UltimaOutdoorTile.Water:
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, UltimaOutdoorTile.Mountain);
                    LineIfNot(square, tileNorth, tileEast, tileSouth, tileWest, mliWater, UltimaOutdoorTile.Mountain, UltimaOutdoorTile.Water);
                    break;
                case UltimaOutdoorTile.Mountain:
                    LineIfNot(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, UltimaOutdoorTile.Mountain);
                    break;
                default:
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliSolid, UltimaOutdoorTile.Mountain);
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliWater, UltimaOutdoorTile.Water);
                    LineIf(square, tileNorth, tileEast, tileSouth, tileWest, mliForest, UltimaOutdoorTile.Forest);
                    break;
            }

            if (square.Note != null)
            {
                if (map == Ultima1Map.Legend)
                    square.Note.Symbol = "??";
                square.Note.Location = ptGrid;
            }

            return msd;
        }

        private Color OverworldColor(UltimaOutdoorTile tile)
        {
            switch (tile)
            {
                case UltimaOutdoorTile.Water: return Color.PowderBlue;
                case UltimaOutdoorTile.Grass: return Color.PaleGreen;
                case UltimaOutdoorTile.Mountain: return Color.Silver;
                case UltimaOutdoorTile.Forest: return Color.LawnGreen;
                case UltimaOutdoorTile.Castle: return Color.MediumOrchid;
                case UltimaOutdoorTile.City: return Color.DodgerBlue;
                case UltimaOutdoorTile.Dungeon1:
                case UltimaOutdoorTile.Dungeon2: return Color.Tomato;
                default: return Color.White;
            }
        }

        private ColorPattern IndoorColor(UltimaIndoorTile tile)
        {
            switch (tile)
            {
                case UltimaIndoorTile.Solid: return SquareStyleList.DefaultSolid;
                case UltimaIndoorTile.Solid2: return SquareStyleList.DefaultSolid;
                case UltimaIndoorTile.Barrier: return new ColorPattern(Color.Red, HatchStyle.ZigZag);
                default: return new ColorPattern(Color.White, HatchStyle.Percent90);
            }
        }

        private MapSquareData SetUndergroundSquare(Point ptGrid, Point ptGame, MapSquare square, UltimaMapData data, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            MapSquareData msd = null;
            UltimaIndoorTile tile = data.GetUnderworldValue(horiz, vert);

            UltimaMapSquareInfo infoNorth = new UltimaMapSquareInfo(data.GetUnderworldValue(horiz, vert - iDeltaSouth));
            UltimaMapSquareInfo infoSouth = new UltimaMapSquareInfo(data.GetUnderworldValue(horiz, vert + iDeltaSouth));
            UltimaMapSquareInfo infoEast = new UltimaMapSquareInfo(data.GetUnderworldValue(horiz + iDeltaEast, vert));
            UltimaMapSquareInfo infoWest = new UltimaMapSquareInfo(data.GetUnderworldValue(horiz - iDeltaEast, vert));

            MapLineInfo mliSolid = new MapLineInfo(Color.Black, DashStyle.Solid, 2);

            square.Colors.BackColorPattern = IndoorColor(tile);
            square.Line(Direction.Up, m_sheet.GridLines);
            square.Line(Direction.Down, m_sheet.GridLines);
            square.Line(Direction.Left, m_sheet.GridLines);
            square.Line(Direction.Right, m_sheet.GridLines);


            // Add walls/doors
            switch (tile)
            {
                case UltimaIndoorTile.Solid:
                case UltimaIndoorTile.Solid2:
                    if (infoNorth.Viewable)
                        square.Line(Direction.Up, mliSolid);
                    if (infoSouth.Viewable)
                        square.Line(Direction.Down, mliSolid);
                    if (infoWest.Viewable)
                        square.Line(Direction.Left, mliSolid);
                    if (infoEast.Viewable)
                        square.Line(Direction.Right, mliSolid);
                    break;
                case UltimaIndoorTile.Door:
                    // Door-squares in Ultima are odd in that a door next to a door is a solid wall
                    if (infoNorth.Viewable && !infoNorth.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Up, ptGrid));
                    if (infoEast.Viewable && !infoEast.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Right, ptGrid));
                    if (infoSouth.Viewable && !infoSouth.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Down, ptGrid));
                    if (infoWest.Viewable && !infoWest.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Left, ptGrid));
                    square.Line(Direction.Up, mliSolid);
                    square.Line(Direction.Down, mliSolid);
                    square.Line(Direction.Left, mliSolid);
                    square.Line(Direction.Right, mliSolid);
                    break;
                case UltimaIndoorTile.LadderDown:
                case UltimaIndoorTile.LadderUp:
                case UltimaIndoorTile.Barrier:
                case UltimaIndoorTile.Open:
                case UltimaIndoorTile.Open4:
                case UltimaIndoorTile.Open5:
                    if (!infoNorth.Viewable || infoNorth.Divider)
                        square.Line(Direction.Up, mliSolid);
                    if (!infoSouth.Viewable || infoSouth.Divider)
                        square.Line(Direction.Down, mliSolid);
                    if (!infoWest.Viewable || infoWest.Divider)
                        square.Line(Direction.Left, mliSolid);
                    if (!infoEast.Viewable || infoEast.Divider)
                        square.Line(Direction.Right, mliSolid);
                    if (infoNorth.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Up, ptGrid));
                    if (infoEast.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Right, ptGrid));
                    if (infoSouth.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Down, ptGrid));
                    if (infoWest.Door)
                        square.AddUniqueIcon(new MapIcon(IconName.DoorHalf, Direction.Left, ptGrid));
                    break;
                default:
                    break;
            }

            // Add icons
            switch (tile)
            {
                case UltimaIndoorTile.LadderDown:
                    square.AddUniqueIcon(new MapIcon(IconName.StairsDown, Direction.Up, ptGrid));
                    square.Note = new MapNote(String.Format("{{map:{0}, Level {1}}}", Ultima1MemoryHacker.MapName((Ultima1Map) (data.Index % 256)), (data.Index >> 8) + 1), Color.Black, ".", ptGrid);
                    break;
                case UltimaIndoorTile.LadderUp:
                    square.AddUniqueIcon(new MapIcon(IconName.StairsUp, Direction.Up, ptGrid));
                    if (data.Index % 256 > 1)
                        square.Note = new MapNote(String.Format("{{map:{0}, Level {1}}}", Ultima1MemoryHacker.MapName((Ultima1Map)(data.Index % 256)), (data.Index >> 8) - 1), Color.Black, ".", ptGrid);
                    else
                        square.Note = new MapNote(String.Format("{{map:{0}}} {1},{2}", Ultima1MemoryHacker.MapName(Ultima1Map.Overworld), data.OverworldLocation.X, data.OverworldLocation.Y), Color.Black, ".", ptGrid);
                    break;
                default:
                    break;
            }

            return msd;
        }
    }
}
